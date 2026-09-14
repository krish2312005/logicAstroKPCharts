using System;
using System.Drawing;
using System.Windows.Forms;

namespace logicAstroKPCharts
{
    /// <summary>
    /// Renders a DataGridView cell that contains a "primary" text segment plus an
    /// optional "hidden houses" segment displayed in green brackets. The hidden
    /// segment is carried inside the cell value using a private separator so the
    /// grid can still store, sort-disabled, and recopy a single value string.
    ///
    /// Cells that do not contain the separator are painted by the grid's normal
    /// painting path, so charts without hidden houses render exactly as before.
    /// </summary>
    internal static class GridRichTextPainter
    {
        private const string SEPARATOR = "\u0001";

        public static readonly Color HiddenHouseGreen = Color.FromArgb(0, 128, 0);

        /// <summary>Returns the cell value carrying the hidden segment next to the base text.</summary>
        public static string Encode(string baseText, string hiddenBrackets)
        {
            if (string.IsNullOrEmpty(hiddenBrackets))
                return baseText;
            return baseText + SEPARATOR + hiddenBrackets;
        }

        /// <summary>
        /// Splits an encoded cell value back into its base text and hidden-bracket
        /// segment. Returns true when the value carries a hidden segment.
        /// </summary>
        public static bool TrySplit(string value, out string baseText, out string hiddenText)
        {
            baseText = value ?? "";
            hiddenText = "";
            if (string.IsNullOrEmpty(value))
                return false;

            int idx = value.IndexOf(SEPARATOR, StringComparison.Ordinal);
            if (idx < 0)
                return false;

            baseText = value.Substring(0, idx);
            hiddenText = value.Substring(idx + SEPARATOR.Length);
            return !string.IsNullOrEmpty(hiddenText);
        }

        /// <summary>
        /// Paints the cell background, the base text in the cell's normal colours,
        /// and the hidden segment in green directly after the base text, honouring
        /// the cell's alignment. Callers must set e.Handled = true afterwards.
        /// </summary>
        public static void PaintCell(DataGridViewCellPaintingEventArgs e, string baseText, string hiddenText)
        {
            Color backColor = (e.State & DataGridViewElementStates.Selected) != 0
                ? e.CellStyle.SelectionBackColor
                : e.CellStyle.BackColor;
            Color baseColor = (e.State & DataGridViewElementStates.Selected) != 0
                ? e.CellStyle.SelectionForeColor
                : e.CellStyle.ForeColor;

            using (Brush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }

            string combined = baseText + (string.IsNullOrEmpty(hiddenText) ? "" : " " + hiddenText);
            if (combined.Length == 0)
                return;

            Font font = e.CellStyle.Font;
            Size entireSize = TextRenderer.MeasureText(e.Graphics, combined, font,
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
            Size baseSize = TextRenderer.MeasureText(e.Graphics, baseText, font,
                new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);

            int nX = 0;
            switch (e.CellStyle.Alignment)
            {
                case DataGridViewContentAlignment.TopRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.BottomRight:
                    nX = e.CellBounds.Right - entireSize.Width;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.BottomCenter:
                    nX = e.CellBounds.Left + ((e.CellBounds.Width - entireSize.Width) / 2);
                    break;
                default:
                    nX = e.CellBounds.Left;
                    break;
            }

            int nY = 0;
            switch (e.CellStyle.Alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.TopRight:
                    nY = e.CellBounds.Top;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.BottomRight:
                    nY = e.CellBounds.Bottom - font.Height;
                    break;
                default:
                    nY = e.CellBounds.Top + ((e.CellBounds.Height - font.Height) / 2);
                    break;
            }

            Region previousClip = e.Graphics.Clip;
            try
            {
                e.Graphics.SetClip(e.CellBounds);

                if (baseText.Length > 0)
                {
                    TextRenderer.DrawText(e.Graphics, baseText, font, new Point(nX, nY), baseColor,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                }

                if (!string.IsNullOrEmpty(hiddenText))
                {
                    TextRenderer.DrawText(e.Graphics, hiddenText, font,
                        new Point(nX + baseSize.Width + TextRenderer.MeasureText(e.Graphics, " ", font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Width, nY),
                        HiddenHouseGreen,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                }
            }
            finally
            {
                if (previousClip != null)
                    e.Graphics.Clip = previousClip;
                else
                    e.Graphics.ResetClip();
            }
        }
    }
}