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
        private SouthIndianChartControl m_lagnaChart;
        private SouthIndianChartControl m_kpChart;

        public ChartResultsUserControl()
        {
            InitializeComponent();

            m_lagnaChart = new SouthIndianChartControl();
            m_lagnaChart.Dock = DockStyle.Fill;
            m_lagnaChart.ChartType = SouthIndianChartType.Lagna;
            panelLagnaChart.Controls.Add(m_lagnaChart);

            m_kpChart = new SouthIndianChartControl();
            m_kpChart.Dock = DockStyle.Fill;
            m_kpChart.ChartType = SouthIndianChartType.KP;
            panelKpChart.Controls.Add(m_kpChart);
        }

        private class ChartEntry
        {
            public string Name;
            public bool IsCusp;
        }

        public void LoadChartData(AstroChartData chartData)
        {
            m_chartData = chartData;
            if (m_chartData == null) return;

            lblTitle.Text = string.Format("KP Astrology Chart - {0}, {1}", m_chartData.Name, m_chartData.Sex);

            m_lagnaChart.SetChartData(BuildLagnaEntries(), FindLagnaSign());
            m_kpChart.SetChartData(BuildKpEntries(), FindLagnaSign());

            SetupPlanetTable();
            SetupCuspTable();
            SetupSignificationTable();
            SetupNadiTable();
            SetupPlanetLegend();
        }

        private List<SouthIndianChartEntry>[] BuildLagnaEntries()
        {
            List<SouthIndianChartEntry>[] all = new List<SouthIndianChartEntry>[12];
            int lagnaSign = FindLagnaSign();

            for (int i = 0; i < 12; i++)
            {
                List<SouthIndianChartEntry> list = new List<SouthIndianChartEntry>();
                foreach (ChartEntry ce in GetCellEntries(i))
                {
                    if (!ce.IsCusp)
                        list.Add(new SouthIndianChartEntry { Text = ce.Name, Kind = SouthIndianChartEntryKind.Planet });
                }
                if (i == lagnaSign)
                    list.Add(new SouthIndianChartEntry { Text = "As", Kind = SouthIndianChartEntryKind.Ascendant });
                all[i] = list;
            }
            return all;
        }

        private List<SouthIndianChartEntry>[] BuildKpEntries()
        {
            List<SouthIndianChartEntry>[] all = new List<SouthIndianChartEntry>[12];

            for (int i = 0; i < 12; i++)
            {
                List<SouthIndianChartEntry> list = new List<SouthIndianChartEntry>();
                foreach (ChartEntry ce in GetCellEntries(i))
                {
                    SouthIndianChartEntryKind kind = ce.IsCusp ? SouthIndianChartEntryKind.Cusp : SouthIndianChartEntryKind.Planet;
                    list.Add(new SouthIndianChartEntry { Text = ce.Name, Kind = kind });
                }
                all[i] = list;
            }
            return all;
        }

        private List<ChartEntry> GetCellEntries(int signIdx)
        {
            List<ChartEntry> result = new List<ChartEntry>();
            string rasiData = m_chartData.RasiDataArray[signIdx];
            if (string.IsNullOrEmpty(rasiData)) return result;

            string[] rawEntries = rasiData.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string raw in rawEntries)
            {
                string[] parts = raw.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) continue;

                string name = parts[0].Trim();
                bool isCusp = name.StartsWith("~");
                if (isCusp) name = name.Substring(1).Trim();
                while (name.Contains("  "))
                    name = name.Replace("  ", " ");
                if (name.Length == 0) continue;

                result.Add(new ChartEntry { Name = name, IsCusp = isCusp });
            }
            return result;
        }

        private int FindLagnaSign()
        {
            for (int i = 0; i < 12; i++)
            {
                foreach (ChartEntry ce in GetCellEntries(i))
                {
                    if (ce.IsCusp && string.Compare(ce.Name, "Lag", StringComparison.OrdinalIgnoreCase) == 0)
                        return i;
                }
            }
            return -1;
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
