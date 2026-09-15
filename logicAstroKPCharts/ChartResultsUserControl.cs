using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using srlWebCom.Astro.AstroObjects;

namespace logicAstroKPCharts
{
    public partial class ChartResultsUserControl : UserControl
    {
        private AstroChartData m_chartData;
        private AstroChartData m_originalChartData;
        private DateTime m_originalBirthTime;
        private DateTime m_currentBirthTime;
        private bool m_btrActive;
        private Func<DateTime, AstroChartData> m_btrEngine;
        private Dictionary<string, HouseSignificationData> m_nadiSignifications;
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

            btnBTRMinusDay.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromDays(-1));
            btnBTRPlusDay.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromDays(1));
            btnBTRMinusHour.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromHours(-1));
            btnBTRPlusHour.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromHours(1));
            btnBTRMinusMin.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromMinutes(-1));
            btnBTRPlusMin.Click += (s, e) => ApplyBtrDelta(TimeSpan.FromMinutes(1));
            btnBTRReset.Click += (s, e) => ApplyBtrTime(m_originalBirthTime);
        }

        private class ChartEntry
        {
            public string Name;
            public bool IsCusp;
        }

        public void LoadChartData(AstroChartData chartData)
        {
            if (chartData == null) return;

            m_originalChartData = chartData;
            m_chartData = chartData;
            m_btrActive = false;
            m_originalBirthTime = ParseBirthTime(chartData.DateTimeOfBirth);
            m_currentBirthTime = m_originalBirthTime;

            UpdateHeader();
            RenderAll();
        }

        public void SetBtrEngine(Func<DateTime, AstroChartData> btrEngine)
        {
            m_btrEngine = btrEngine;
        }

        private DateTime ParseBirthTime(string strDateTimeOfBirth)
        {
            if (string.IsNullOrEmpty(strDateTimeOfBirth)) return DateTime.MinValue;

            string strPart = strDateTimeOfBirth;
            int idx = strPart.IndexOf('(');
            if (idx >= 0) strPart = strPart.Substring(0, idx);
            while (strPart.Contains("  "))
                strPart = strPart.Replace("  ", " ");
            strPart = strPart.Trim();

            DateTime dt;
            if (DateTime.TryParseExact(strPart, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                return dt;

            DateTime.TryParse(strPart, CultureInfo.CreateSpecificCulture("ta-IN"), DateTimeStyles.None, out dt);
            return dt;
        }

        private string GetTimeZonePart(string strDateTimeOfBirth)
        {
            if (string.IsNullOrEmpty(strDateTimeOfBirth)) return "";
            int idx = strDateTimeOfBirth.IndexOf('(');
            if (idx < 0) return "";
            return strDateTimeOfBirth.Substring(idx).Trim();
        }

        private void UpdateHeader()
        {
            if (m_originalChartData == null) return;

            lblTitle.Text = string.Format("KP Astrology Chart - {0}, {1}", m_originalChartData.Name, m_originalChartData.Sex);

            lblDetails.Text = string.Format("DOB: {0}  |  Place: {1}  |  Long: {2}  |  Lat: {3}",
                m_originalChartData.DateTimeOfBirth, m_originalChartData.PlaceOfBirth,
                m_originalChartData.Longitude, m_originalChartData.Latitude);

            if (m_btrActive)
            {
                TimeSpan offset = m_currentBirthTime - m_originalBirthTime;
                string strDirection = offset >= TimeSpan.Zero ? "ahead" : "behind";
                lblBtrInfo.Text = string.Format("BTR: {0} {1}  [{2} from main - {3}]",
                    m_currentBirthTime.ToString("dd-MM-yyyy  HH:mm:ss"), GetTimeZonePart(m_originalChartData.DateTimeOfBirth),
                    FormatOffset(offset), strDirection);
                lblBtrInfo.Visible = true;
                lblBtrInfo.Location = new Point(lblDetails.Right + 10, lblDetails.Top);
            }
            else
            {
                lblBtrInfo.Visible = false;
                lblBtrInfo.Text = "";
            }
        }

        private string FormatOffset(TimeSpan ts)
        {
            string sign = ts < TimeSpan.Zero ? "-" : "+";
            TimeSpan abs = ts.Duration();
            string strResult = sign;
            if (abs.Days > 0)
                strResult += string.Format("{0}D ", abs.Days);
            strResult += string.Format("{0}H{1:00}M", abs.Hours, abs.Minutes);
            return strResult;
        }

        private void RenderAll()
        {
            if (m_chartData == null) return;

            m_lagnaChart.SetChartData(BuildLagnaEntries(), FindLagnaSign());
            m_kpChart.SetChartData(BuildKpEntries(), FindLagnaSign());

            SetupPlanetTable();
            SetupCuspTable();
            SetupSignificationTable();
            SetupNadiTable();
            SetupSPKhullarTable();
            SetupPlanetLegend();
        }

        private void ApplyBtrDelta(TimeSpan delta)
        {
            if (m_btrEngine == null || m_originalChartData == null) return;
            ApplyBtrTime(m_currentBirthTime.Add(delta));
        }

        private void ApplyBtrTime(DateTime newTime)
        {
            if (m_btrEngine == null || m_originalChartData == null) return;

            Cursor oldCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                AstroChartData newData = m_btrEngine(newTime);
                if (newData == null) return;

                m_chartData = newData;
                m_currentBirthTime = newTime;
                m_btrActive = m_currentBirthTime != m_originalBirthTime;

                RenderAll();
                UpdateHeader();
            }
            finally
            {
                Cursor = oldCursor;
            }
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
            if (string.IsNullOrEmpty(strLord) || m_nadiSignifications == null)
            {
                return "";
            }

            HouseSignificationData hsd;
            if (m_nadiSignifications.TryGetValue(strLord.Trim(), out hsd))
            {
                bool isNode = (strLord.Trim().Length == 2)
                    && (strLord.Trim().ToUpperInvariant() == "RA" || strLord.Trim().ToUpperInvariant() == "KE");
                return NadiCalculationService.ResolveNadiCoordinates(hsd.D3, hsd.D4, hsd.D7, hsd.D8, isNode);
            }

            return "";
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

            m_nadiSignifications = new Dictionary<string, HouseSignificationData>(StringComparer.OrdinalIgnoreCase);
            foreach (HouseSignificationData hsd in m_chartData.HouseSignificationList)
            {
                string planet = (hsd.Planet ?? "").Replace("#", "").Replace("*", "").Trim();
                if (planet.Length == 0) continue;
                if (!m_nadiSignifications.ContainsKey(planet))
                    m_nadiSignifications[planet] = hsd;
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

        private string GetSPKhullarCoordinates(string strLord)
        {
            if (string.IsNullOrEmpty(strLord) || m_nadiSignifications == null)
            {
                return "";
            }

            HouseSignificationData hsd;
            if (!m_nadiSignifications.TryGetValue(strLord.Trim(), out hsd))
            {
                return "";
            }

            return KhullarCalculationService.GetSPKhullarCoordinateStringWithMarkers(
                strLord, hsd, m_chartData.PlanetList);
        }

        private void SetupSPKhullarTable()
        {
            dgvSPKhullar.Columns.Clear();
            dgvSPKhullar.Columns.Add("Planet", "Planet");
            dgvSPKhullar.Columns.Add("SPKhullar", "SP Khullar Nadi Coordinates");

            ConfigureFillColumn(dgvSPKhullar.Columns[0], 1F, 70);
            ConfigureFillColumn(dgvSPKhullar.Columns[1], 3F, 240);

            foreach (DataGridViewColumn col in dgvSPKhullar.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            m_nadiSignifications = new Dictionary<string, HouseSignificationData>(StringComparer.OrdinalIgnoreCase);
            foreach (HouseSignificationData hsd in m_chartData.HouseSignificationList)
            {
                string planet = (hsd.Planet ?? "").Replace("#", "").Replace("*", "").Trim();
                if (planet.Length == 0) continue;
                if (!m_nadiSignifications.ContainsKey(planet))
                    m_nadiSignifications[planet] = hsd;
            }

            PopulateCuspalLordshipFields();

            foreach (PlanetData pd in m_chartData.PlanetList)
            {
                string displayName = pd.Name;
                if (!string.IsNullOrEmpty(pd.Strength))
                    displayName = pd.Name + pd.Strength;

                int rowIdx = dgvSPKhullar.Rows.Add(displayName, GetSPKhullarCoordinates(pd.Name));
                DataGridViewRow row = dgvSPKhullar.Rows[rowIdx];

                row.Cells[0].Style.ForeColor = Color.FromArgb(0, 0, 204);
                row.Cells[0].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                row.Cells[1].Style.BackColor = Color.FromArgb(240, 233, 216);
            }

            DisableSorting(dgvSPKhullar);
        }

        private void PopulateCuspalLordshipFields()
        {
            if (m_chartData == null || m_chartData.CuspList == null) return;

            foreach (HouseSignificationData hsd in m_chartData.HouseSignificationList)
            {
                string planetName = (hsd.Planet ?? "").Replace("#", "").Replace("*", "").Trim();
                if (planetName.Length == 0) continue;

                StringBuilder signLordCusps = new StringBuilder();
                StringBuilder starLordCusps = new StringBuilder();
                StringBuilder subLordCusps = new StringBuilder();
                StringBuilder sslordCusps = new StringBuilder();

                foreach (CuspData cd in m_chartData.CuspList)
                {
                    if (string.Equals((cd.SignLord ?? "").Trim(), planetName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (signLordCusps.Length > 0) signLordCusps.Append(',');
                        signLordCusps.Append(cd.HouseNo);
                    }
                    if (string.Equals((cd.StarLord ?? "").Trim(), planetName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (starLordCusps.Length > 0) starLordCusps.Append(',');
                        starLordCusps.Append(cd.HouseNo);
                    }
                    if (string.Equals((cd.SubLord ?? "").Trim(), planetName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (subLordCusps.Length > 0) subLordCusps.Append(',');
                        subLordCusps.Append(cd.HouseNo);
                    }
                    if (string.Equals((cd.SSLord ?? "").Trim(), planetName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (sslordCusps.Length > 0) sslordCusps.Append(',');
                        sslordCusps.Append(cd.HouseNo);
                    }
                }

                hsd.CuspalSignLord = signLordCusps.ToString();
                hsd.CuspalStarLord = starLordCusps.ToString();
                hsd.CuspalSubLord = subLordCusps.ToString();
                hsd.CuspalSSLord = sslordCusps.ToString();
            }
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
