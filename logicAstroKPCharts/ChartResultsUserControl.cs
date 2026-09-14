using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using srlWebCom.Astro.AstroObjects;

namespace logicAstroKPCharts
{
    public partial class ChartResultsUserControl : UserControl
    {
        private AstroChartData m_chartData;
        private int m_lastChartWidth = 0;
        private int m_lastChartHeight = 0;

        public ChartResultsUserControl()
        {
            InitializeComponent();
            panelChart.Resize += panelChart_Resize;
        }

        private void panelChart_Resize(object sender, EventArgs e)
        {
            if (m_chartData == null) return;
            if (Math.Abs(panelChart.Width - m_lastChartWidth) < 8 && Math.Abs(panelChart.Height - m_lastChartHeight) < 8) return;
            DrawRasiChart();
        }

        private class InfoLine
        {
            public string Text;
            public float FontSize;
            public FontStyle Style;
            public Color ForeColor;

            public InfoLine(string text, float fontSize, FontStyle style, Color foreColor)
            {
                Text = text;
                FontSize = fontSize;
                Style = style;
                ForeColor = foreColor;
            }
        }

        public void LoadChartData(AstroChartData chartData)
        {
            m_chartData = chartData;
            if (m_chartData == null) return;

            lblTitle.Text = string.Format("KP Astrology Chart - {0}, {1}", m_chartData.Name, m_chartData.Sex);

            DrawRasiChart();
            SetupPlanetTable();
            SetupCuspTable();
            SetupSignificationTable();
            SetupNadiTable();
            SetupPlanetLegend();
        }

        private void DrawRasiChart()
        {
            if (m_chartData == null) return;

            int chartW = panelChart.Width;
            int chartH = panelChart.Height;
            if (chartW <= 0 || chartH <= 0) return;

            m_lastChartWidth = chartW;
            m_lastChartHeight = chartH;

            panelChart.SuspendLayout();
            panelChart.Controls.Clear();

            string[] signNames = {
                "Meenam", "Mesham", "Rishabam", "Mithunam",
                "Katakam", "Simham", "Kanni", "Thulam",
                "Viruchikam", "Dhanusu", "Makaram", "Kumbam"
            };

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 4;
            tlp.RowCount = 4;
            tlp.ColumnStyles.Clear();
            tlp.RowStyles.Clear();

            for (int c = 0; c < 4; c++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int r = 0; r < 4; r++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            tlp.Padding = new Padding(0);
            tlp.Margin = new Padding(0);

            int[,] cellMap = {
                { 11, 0, 1, 2 },
                { 10, -1, -1, 3 },
                { 9, -1, -1, 4 },
                { 8, 7, 6, 5 }
            };

            int cellW = chartW / 4;
            int cellH = chartH / 4;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int idx = cellMap[row, col];

                    if (idx == -1)
                    {
                        if (row == 1 && col == 1)
                        {
                            Panel mergedPanel = new Panel();
                            mergedPanel.Dock = DockStyle.Fill;
                            mergedPanel.Margin = new Padding(1);
                            mergedPanel.BorderStyle = BorderStyle.FixedSingle;
                            mergedPanel.BackColor = Color.FromArgb(255, 255, 245);
                            DrawMergedInfo(mergedPanel, cellW * 2, cellH * 2);
                            tlp.Controls.Add(mergedPanel, 1, 1);
                            tlp.SetColumnSpan(mergedPanel, 2);
                            tlp.SetRowSpan(mergedPanel, 2);
                        }
                        continue;
                    }

                    Panel cellPanel = new Panel();
                    cellPanel.Dock = DockStyle.Fill;
                    cellPanel.Margin = new Padding(1);
                    cellPanel.BorderStyle = BorderStyle.FixedSingle;
                    cellPanel.BackColor = Color.White;

                    Label signLabel = new Label();
                    signLabel.Text = signNames[idx];
                    signLabel.Dock = DockStyle.Top;
                    signLabel.Height = 20;
                    signLabel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                    signLabel.ForeColor = Color.FromArgb(90, 90, 95);
                    signLabel.TextAlign = ContentAlignment.MiddleCenter;
                    signLabel.BackColor = Color.FromArgb(240, 248, 255);
                    signLabel.Padding = new Padding(0);
                    cellPanel.Controls.Add(signLabel);

                    string rasiData = m_chartData.RasiDataArray[idx];
                    if (!string.IsNullOrEmpty(rasiData))
                    {
                        string[] entries = rasiData.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        int entryCount = entries.Length;
                        int signHeaderH = 22;
                        int availH = Math.Max(cellH - signHeaderH - 6, entryCount * 14);
                        float step = entryCount > 0 ? (float)availH / entryCount : 0F;
                        if (step > 20F) step = 20F;

                        float yPos = signHeaderH + 2;

                        foreach (string entry in entries)
                        {
                            string[] parts = entry.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 2)
                            {
                                string planetName = parts[0].Trim();
                                string position = parts[1].Trim();
                                bool isCusp = planetName.StartsWith("~");

                                if (isCusp)
                                    planetName = planetName.Substring(1);

                                Font pFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                                Font posFont = new Font("Segoe UI", 8F);

                                Label planetLabel = new Label();
                                planetLabel.AutoSize = true;
                                planetLabel.Location = new Point(6, (int)yPos);
                                planetLabel.Font = pFont;
                                planetLabel.BackColor = Color.Transparent;
                                planetLabel.ForeColor = isCusp ? Color.FromArgb(204, 0, 0) : Color.FromArgb(0, 0, 204);
                                planetLabel.Text = planetName;
                                cellPanel.Controls.Add(planetLabel);

                                Label posLabel = new Label();
                                posLabel.Text = position;
                                posLabel.AutoSize = true;
                                posLabel.Font = posFont;
                                posLabel.ForeColor = Color.FromArgb(60, 60, 60);
                                posLabel.BackColor = Color.Transparent;
                                Size nameSize = TextRenderer.MeasureText(planetName, pFont);
                                Size posSize = TextRenderer.MeasureText(position, posFont);
                                posLabel.Location = new Point(Math.Max(6 + nameSize.Width + 8, cellW - posSize.Width - 6), (int)yPos);
                                cellPanel.Controls.Add(posLabel);

                                yPos += step;
                            }
                        }
                    }

                    tlp.Controls.Add(cellPanel, col, row);
                }
            }

            panelChart.Controls.Add(tlp);
            tlp.BringToFront();
            panelChart.ResumeLayout(false);
        }

        private void DrawMergedInfo(Panel p, int w, int h)
        {
            List<InfoLine> left = new List<InfoLine>();
            left.Add(new InfoLine(m_chartData.Name + ", " + m_chartData.Sex, 9F, FontStyle.Bold, Color.FromArgb(0, 0, 153)));
            left.Add(new InfoLine("DOB : " + m_chartData.DateTimeOfBirth, 8F, FontStyle.Regular, Color.Black));
            left.Add(new InfoLine("Place : " + m_chartData.PlaceOfBirth, 8F, FontStyle.Regular, Color.Black));
            left.Add(new InfoLine("Star : " + m_chartData.MoonStarInfo, 8F, FontStyle.Regular, Color.Black));
            left.Add(new InfoLine("Dasa : " + m_chartData.DasaBalance, 8.5F, FontStyle.Bold, Color.Black));

            List<InfoLine> right = new List<InfoLine>();
            right.Add(new InfoLine("Long : " + m_chartData.Longitude, 8F, FontStyle.Regular, Color.Black));
            right.Add(new InfoLine("Lat : " + m_chartData.Latitude, 8F, FontStyle.Regular, Color.Black));
            right.Add(new InfoLine("Ayanamsa : " + m_chartData.Ayanamsa, 8F, FontStyle.Regular, Color.Black));
            right.Add(new InfoLine("Sidereal : " + m_chartData.SiderealTime, 8F, FontStyle.Regular, Color.Black));

            float lineH = 19F;

            if (w >= 400)
            {
                int maxLines = Math.Max(left.Count, right.Count);
                float totalH = maxLines * lineH + 10;
                float y0 = Math.Max(6, (h - totalH) / 2);
                DrawInfoColumn(p, left, 10, y0, lineH);
                DrawInfoColumn(p, right, w * 0.5F + 8, y0, lineH);
            }
            else
            {
                List<InfoLine> all = new List<InfoLine>(left);
                all.AddRange(right);
                float totalH = all.Count * lineH + 10;
                float y0 = Math.Max(4, (h - totalH) / 2);
                DrawInfoColumn(p, all, 8, y0, lineH);
            }
        }

        private void DrawInfoColumn(Panel p, List<InfoLine> lines, float x, float y0, float lineH)
        {
            float y = y0;
            foreach (InfoLine il in lines)
            {
                Label lbl = new Label();
                lbl.Text = il.Text;
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", il.FontSize, il.Style);
                lbl.ForeColor = il.ForeColor;
                lbl.BackColor = Color.Transparent;
                lbl.Location = new Point((int)x, (int)y);
                p.Controls.Add(lbl);
                y += lineH;
            }
        }

        private void DisableSorting(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ConfigureFillColumn(DataGridViewColumn col, float fillWeight, int minWidth)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col.FillWeight = fillWeight;
            col.MinimumWidth = minWidth;
        }

        private void SetupPlanetTable()
        {
            dgvPlanets.Columns.Clear();
            dgvPlanets.Columns.Add("Planet", "Planet");
            dgvPlanets.Columns.Add("SignLord", "Sgn");
            dgvPlanets.Columns.Add("StarLord", "Str");
            dgvPlanets.Columns.Add("SubLord", "Sub");
            dgvPlanets.Columns.Add("SSLord", "SS");

            ConfigureFillColumn(dgvPlanets.Columns[0], 2.4F, 62);
            ConfigureFillColumn(dgvPlanets.Columns[1], 1F, 40);
            ConfigureFillColumn(dgvPlanets.Columns[2], 1F, 40);
            ConfigureFillColumn(dgvPlanets.Columns[3], 1F, 40);
            ConfigureFillColumn(dgvPlanets.Columns[4], 1F, 40);

            foreach (DataGridViewColumn col in dgvPlanets.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (PlanetData pd in m_chartData.PlanetList)
            {
                string displayName = pd.Name;
                if (!string.IsNullOrEmpty(pd.Strength))
                    displayName = pd.Name + pd.Strength;

                int rowIdx = dgvPlanets.Rows.Add(displayName, pd.SignLord, pd.StarLord, pd.SubLord, pd.SSLord);
                DataGridViewRow row = dgvPlanets.Rows[rowIdx];

                if (!string.IsNullOrEmpty(pd.Strength))
                    row.Cells[0].Style.ForeColor = Color.FromArgb(246, 4, 4);
                else
                    row.Cells[0].Style.ForeColor = Color.FromArgb(0, 0, 204);

                row.Cells[0].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                row.Cells[3].Style.BackColor = Color.FromArgb(240, 233, 216);
            }

            DisableSorting(dgvPlanets);
        }

        private void SetupCuspTable()
        {
            dgvCusps.Columns.Clear();
            dgvCusps.Columns.Add("Cusp", "Cusp");
            dgvCusps.Columns.Add("SignLord", "Sgn");
            dgvCusps.Columns.Add("StarLord", "Str");
            dgvCusps.Columns.Add("SubLord", "Sub");
            dgvCusps.Columns.Add("SSLord", "SS");

            ConfigureFillColumn(dgvCusps.Columns[0], 1.8F, 55);
            ConfigureFillColumn(dgvCusps.Columns[1], 1F, 40);
            ConfigureFillColumn(dgvCusps.Columns[2], 1F, 40);
            ConfigureFillColumn(dgvCusps.Columns[3], 1F, 40);
            ConfigureFillColumn(dgvCusps.Columns[4], 1F, 40);

            foreach (DataGridViewColumn col in dgvCusps.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (CuspData cd in m_chartData.CuspList)
            {
                string cuspDisplay = ConvertToRomanLetters(cd.HouseNo.ToString());
                string subDisplay = cd.SubLord;
                if (!string.IsNullOrEmpty(cd.SubStrength))
                    subDisplay = cd.SubLord + cd.SubStrength;

                int rowIdx = dgvCusps.Rows.Add(cuspDisplay, cd.SignLord, cd.StarLord, subDisplay, cd.SSLord);
                DataGridViewRow row = dgvCusps.Rows[rowIdx];

                row.Cells[0].Style.ForeColor = Color.FromArgb(204, 0, 0);
                row.Cells[0].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                if (!string.IsNullOrEmpty(cd.SubStrength))
                {
                    row.Cells[3].Style.ForeColor = Color.FromArgb(246, 4, 4);
                    row.Cells[3].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                row.Cells[3].Style.BackColor = Color.FromArgb(240, 233, 216);
            }

            DisableSorting(dgvCusps);
        }

        private void SetupSignificationTable()
        {
            dgvSignification.Columns.Clear();
            dgvSignification.Columns.Add("StarWise", "Star-Wise Significations");
            dgvSignification.Columns.Add("StarLord", "Star");
            dgvSignification.Columns.Add("Planet", "Planet");
            dgvSignification.Columns.Add("SubLord", "Sub");
            dgvSignification.Columns.Add("SubWise", "Sub-Wise Significations");

            ConfigureFillColumn(dgvSignification.Columns[0], 3.2F, 120);
            ConfigureFillColumn(dgvSignification.Columns[1], 0.9F, 40);
            ConfigureFillColumn(dgvSignification.Columns[2], 1.1F, 48);
            ConfigureFillColumn(dgvSignification.Columns[3], 0.9F, 40);
            ConfigureFillColumn(dgvSignification.Columns[4], 3.2F, 120);

            foreach (DataGridViewColumn col in dgvSignification.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvSignification.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSignification.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            foreach (HouseSignificationData hsd in m_chartData.HouseSignificationList)
            {
                int rowIdx = dgvSignification.Rows.Add(hsd.StarWise, hsd.StarLord, hsd.Planet, hsd.SubLord, hsd.SubWise);
                DataGridViewRow row = dgvSignification.Rows[rowIdx];

                row.Cells[1].Style.Font = new Font("Segoe UI", 9F);
                row.Cells[2].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                if (hsd.Planet.Contains("#") || hsd.Planet.Contains("*"))
                    row.Cells[2].Style.ForeColor = Color.FromArgb(246, 4, 4);
                else
                    row.Cells[2].Style.ForeColor = Color.FromArgb(0, 0, 204);

                row.Cells[0].Style.Font = new Font("Segoe UI", 8.5F);
                row.Cells[0].Style.ForeColor = Color.Black;
                row.Cells[4].Style.Font = new Font("Segoe UI", 8.5F);
                row.Cells[4].Style.ForeColor = Color.Black;
            }

            DisableSorting(dgvSignification);
        }

        private string GetNadiCoordinates(string strLord)
        {
            switch (strLord.Trim().ToUpper())
            {
                case "SU": return "1, 9";
                case "MO": return "7, 8";
                case "MA": return "5, 12";
                case "RA": return "4, 5, 12";
                case "JU": return "1, 4, 10";
                case "SA": return "2, 3, 7";
                case "ME": return "1, 7, 10";
                case "KE": return "1, 6, 10, 11";
                case "VE": return "1, 6, 11";
                default: return "";
            }
        }

        private void SetupNadiTable()
        {
            dgvNadi.Columns.Clear();
            dgvNadi.Columns.Add("Position", "Position");
            dgvNadi.Columns.Add("SignLord", "Sgn");
            dgvNadi.Columns.Add("SignNadi", "Sgn Nadi");
            dgvNadi.Columns.Add("StarLord", "Str");
            dgvNadi.Columns.Add("StarNadi", "Str Nadi");
            dgvNadi.Columns.Add("SubLord", "Sub");
            dgvNadi.Columns.Add("SubNadi", "Sub Nadi");

            ConfigureFillColumn(dgvNadi.Columns[0], 0.95F, 56);
            ConfigureFillColumn(dgvNadi.Columns[1], 0.7F, 30);
            ConfigureFillColumn(dgvNadi.Columns[2], 1.5F, 66);
            ConfigureFillColumn(dgvNadi.Columns[3], 0.7F, 30);
            ConfigureFillColumn(dgvNadi.Columns[4], 1.5F, 66);
            ConfigureFillColumn(dgvNadi.Columns[5], 0.7F, 30);
            ConfigureFillColumn(dgvNadi.Columns[6], 1.5F, 66);

            foreach (DataGridViewColumn col in dgvNadi.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (PlanetData pd in m_chartData.PlanetList)
            {
                string displayName = pd.Name;
                if (!string.IsNullOrEmpty(pd.Strength))
                    displayName = pd.Name + pd.Strength;

                int rowIdx = dgvNadi.Rows.Add(displayName, pd.Name, GetNadiCoordinates(pd.Name),
                    pd.StarLord, GetNadiCoordinates(pd.StarLord), pd.SubLord, GetNadiCoordinates(pd.SubLord));
                DataGridViewRow row = dgvNadi.Rows[rowIdx];

                row.Cells[0].Style.ForeColor = Color.FromArgb(0, 0, 204);
                row.Cells[0].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                row.Cells[1].Style.ForeColor = Color.FromArgb(246, 4, 4);
                row.Cells[1].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                row.Cells[2].Style.BackColor = Color.FromArgb(240, 233, 216);
                row.Cells[4].Style.BackColor = Color.FromArgb(240, 248, 255);
                row.Cells[6].Style.BackColor = Color.FromArgb(240, 233, 216);
            }

            DisableSorting(dgvNadi);
        }

        private void SetupPlanetLegend()
        {
            lblPlanetLegend.Text = "# = Planet in its own star\n* = No planets in its star(s)\nR = Retrograde\nBlue = Planet  Red = Cusp";
            lblPlanetLegend.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            lblPlanetLegend.ForeColor = Color.FromArgb(80, 80, 80);
            lblPlanetLegend.TextAlign = ContentAlignment.MiddleCenter;
        }

        private string ConvertToRomanLetters(string strHouseNo)
        {
            switch (strHouseNo.Trim())
            {
                case "1": return "Lag";
                case "2": return "II";
                case "3": return "III";
                case "4": return "IV";
                case "5": return "V";
                case "6": return "VI";
                case "7": return "VII";
                case "8": return "VIII";
                case "9": return "IX";
                case "10": return "X";
                case "11": return "XI";
                case "12": return "XII";
                default: return strHouseNo;
            }
        }
    }
}
