using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace logicAstroKPCharts
{
    public enum SouthIndianChartType
    {
        Lagna,
        KP
    }

    public enum SouthIndianChartEntryKind
    {
        Planet,
        Cusp,
        Ascendant
    }

    public class SouthIndianChartEntry
    {
        public string Text;
        public SouthIndianChartEntryKind Kind;
    }

    public class SouthIndianChartControl : Control
    {
        public SouthIndianChartType ChartType = SouthIndianChartType.Lagna;
        public List<SouthIndianChartEntry>[] CellEntries;
        public int LagnaSignIndex = -1;

        // Sign index matches the engine's RasiDataArray ordering:
        // 0=Mesham 1=Rishabam 2=Mithunam 3=Katakam 4=Simham 5=Kanni
        // 6=Thulam 7=Viruchikam 8=Dhanusu 9=Makaram 10=Kumbam 11=Meenam
        private static readonly string[] m_sgnNames = {
            "Mesham", "Rishabam", "Mithunam", "Katakam",
            "Simham", "Kanni", "Thulam", "Viruchikam",
            "Dhanusu", "Makaram", "Kumbam", "Meenam"
        };

        // South Indian house template (0 = empty centre cells):
        // the LAGNA house is always the bottom-left cell; houses then run
        // 1,2,3... from bottom-left, up the left column, right across the top,
        // down the right column and left along the bottom.
        // The zodiac sign shown in a cell is derived from the Lagna sign using
        // that house number, so the whole wheel rotates with the ascendant.
        private static readonly int[,] m_houseMap = {
            {  4,  5,  6,  7 },
            {  3,  0,  0,  8 },
            {  2,  0,  0,  9 },
            {  1, 12, 11, 10 }
        };

        private static readonly Color PlanetColor = Color.FromArgb(0, 0, 204);
        private static readonly Color CuspColor = Color.FromArgb(204, 0, 0);
        private static readonly Color AscColor = Color.FromArgb(153, 0, 0);
        private static readonly Color GridColor = Color.FromArgb(145, 145, 145);
        private static readonly Color SignBackColor = Color.FromArgb(240, 248, 255);
        private static readonly Color LagnaBackColor = Color.FromArgb(255, 235, 160);

        public SouthIndianChartControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        public void SetChartData(List<SouthIndianChartEntry>[] entries, int lagnaSignIndex)
        {
            CellEntries = entries;
            LagnaSignIndex = lagnaSignIndex;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(BackColor);

            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            int pad = 6;
            int side = Math.Min(ClientSize.Width, ClientSize.Height) - pad * 2;
            if (side < 40) return;

            int ox = (ClientSize.Width - side) / 2;
            int oy = (ClientSize.Height - side) / 2;
            int cell = side / 4;
            int rem = side - cell * 4;

            Font signFont = new Font("Segoe UI", cell >= 140 ? 10F : (cell >= 90 ? 9F : 8F), FontStyle.Bold);
            Font entryFont = new Font("Segoe UI", cell >= 140 ? 11F : (cell >= 90 ? 9.5F : 8F), FontStyle.Bold);

            try
            {
                using (Pen gridPen = new Pen(GridColor))
                using (Pen lagnaPen = new Pen(AscColor, 2F))
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                using (SolidBrush lagnaBrush = new SolidBrush(LagnaBackColor))
                {
                    for (int row = 0; row < 4; row++)
                    {
                        for (int col = 0; col < 4; col++)
                        {
                            int house = m_houseMap[row, col];

                            int w = cell + (col == 3 ? rem : 0);
                            int h = cell + (row == 3 ? rem : 0);
                            Rectangle rect = new Rectangle(ox + col * cell, oy + row * cell, w, h);

                            int idx = -1;
                            if (house > 0)
                                idx = (Math.Max(LagnaSignIndex, 0) + house - 1) % 12;

                            bool isLagna = (idx == LagnaSignIndex && ChartType == SouthIndianChartType.Lagna);

                            g.FillRectangle(isLagna ? lagnaBrush : (Brush)whiteBrush, rect);

                            if (idx == -1)
                            {
                                g.DrawRectangle(gridPen, rect);
                                continue;
                            }

                            int stripH = Math.Max(16, cell / 5);
                            Rectangle stripRect = new Rectangle(rect.X, rect.Y, rect.Width, stripH);
                            using (SolidBrush stripBrush = new SolidBrush(isLagna ? LagnaBackColor : SignBackColor))
                                g.FillRectangle(stripBrush, stripRect);

                            Rectangle textRect = new Rectangle(rect.X + 2, rect.Y, rect.Width - 4, stripH);
                            Color signColor = isLagna ? AscColor : Color.FromArgb(80, 80, 85);
                            TextRenderer.DrawText(g, m_sgnNames[idx], signFont, textRect, signColor,
                                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);

                            if (CellEntries != null && idx >= 0 && idx < CellEntries.Length && CellEntries[idx] != null)
                            {
                                List<SouthIndianChartEntry> entries = CellEntries[idx];
                                if (entries.Count > 0)
                                {
                                    int contentTop = rect.Y + stripH;
                                    int avail = rect.Height - stripH - 4;
                                    int lineH = 15;
                                    int maxLines = Math.Max(1, avail / lineH);
                                    bool twoCols = entries.Count > maxLines;
                                    int perCol = twoCols ? (entries.Count + 1) / 2 : entries.Count;
                                    int blockH = Math.Min(avail, perCol * lineH);
                                    int y0 = contentTop + (avail - blockH) / 2;

                                    for (int k = 0; k < entries.Count; k++)
                                    {
                                        SouthIndianChartEntry ce = entries[k];
                                        int colIdx = (twoCols && k >= perCol) ? 1 : 0;
                                        int lineIdx = (twoCols && k >= perCol) ? (k - perCol) : k;

                                        Color fg = Color.Black;
                                        switch (ce.Kind)
                                        {
                                            case SouthIndianChartEntryKind.Planet:
                                                fg = PlanetColor;
                                                break;
                                            case SouthIndianChartEntryKind.Cusp:
                                                fg = CuspColor;
                                                break;
                                            case SouthIndianChartEntryKind.Ascendant:
                                                fg = AscColor;
                                                break;
                                        }

                                        int colW = twoCols ? rect.Width / 2 : rect.Width;
                                        Rectangle entryRect = new Rectangle(rect.X + 4 + colIdx * (rect.Width / 2), y0 + lineIdx * lineH, Math.Max(10, colW - 8), lineH);
                                        TextRenderer.DrawText(g, ce.Text, entryFont, entryRect, fg,
                                            TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
                                    }
                                }
                            }

                            g.DrawRectangle(isLagna ? lagnaPen : (Pen)gridPen, rect);
                        }
                    }
                }
            }
            finally
            {
                signFont.Dispose();
                entryFont.Dispose();
            }
        }
    }
}