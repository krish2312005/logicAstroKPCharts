using System;
using System.Drawing;
using System.Windows.Forms;
using srlWebCom.Astro.AstroObjects;

namespace logicAstroKPCharts
{
    public partial class ResultsForm : Form
    {
        private AstroChartData m_chartData;
        private bool m_bFullScreen = false;
        private FormWindowState m_prevWindowState;
        private FormBorderStyle m_prevBorderStyle;

        public event EventHandler OnPDFRequested;

        public ResultsForm(AstroChartData chartData)
        {
            InitializeComponent();
            m_chartData = chartData;

            this.KeyPreview = true;
            this.KeyDown += ResultsForm_KeyDown;

            btnPDF.Click += btnPDF_Click;
            btnFullScreen.Click += btnFullScreen_Click;
            btnClose.Click += btnClose_Click;
            this.DoubleClick += ResultsForm_DoubleClick;

            LoadData();
        }

        private void ResultsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                ToggleFullScreen();
            else if (e.KeyCode == Keys.Escape && m_bFullScreen)
                ToggleFullScreen();
        }

        private void ResultsForm_DoubleClick(object sender, EventArgs e)
        {
            if (m_bFullScreen)
                ToggleFullScreen();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (OnPDFRequested != null)
                OnPDFRequested(this, EventArgs.Empty);
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToggleFullScreen()
        {
            if (m_bFullScreen)
            {
                this.WindowState = m_prevWindowState;
                this.FormBorderStyle = m_prevBorderStyle;
                this.TopMost = false;
                m_bFullScreen = false;
                btnFullScreen.Text = "Full Screen";
            }
            else
            {
                m_prevWindowState = this.WindowState;
                m_prevBorderStyle = this.FormBorderStyle;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                m_bFullScreen = true;
                btnFullScreen.Text = "Normal";
            }
        }

        private void LoadData()
        {
            if (m_chartData == null) return;

            lblTitle.Text = string.Format("KP Astrology Chart - {0}, {1}", m_chartData.Name, m_chartData.Sex);
            lblDetails.Text = string.Format("DOB: {0}  |  Place: {1}  |  Long: {2}  |  Lat: {3}",
                m_chartData.DateTimeOfBirth, m_chartData.PlaceOfBirth, m_chartData.Longitude, m_chartData.Latitude);

            DrawRasiChart();
            SetupPlanetTable();
            SetupCuspTable();
            SetupSignificationTable();
            SetupPlanetLegend();
        }

        private void DrawRasiChart()
        {
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
                            DrawMergedInfo(mergedPanel);
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
                    signLabel.Height = 18;
                    signLabel.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
                    signLabel.ForeColor = Color.FromArgb(100, 100, 100);
                    signLabel.TextAlign = ContentAlignment.MiddleCenter;
                    signLabel.BackColor = Color.FromArgb(240, 248, 255);
                    cellPanel.Controls.Add(signLabel);

                    string rasiData = m_chartData.RasiDataArray[idx];
                    if (!string.IsNullOrEmpty(rasiData))
                    {
                        string[] entries = rasiData.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        float yPos = 20;

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

                                Label planetLabel = new Label();
                                planetLabel.AutoSize = true;
                                planetLabel.Location = new Point(4, (int)yPos);
                                planetLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                                planetLabel.BackColor = Color.Transparent;
                                planetLabel.ForeColor = isCusp ? Color.FromArgb(204, 0, 0) : Color.FromArgb(0, 0, 204);
                                planetLabel.Text = planetName;
                                cellPanel.Controls.Add(planetLabel);

                                Label posLabel = new Label();
                                posLabel.AutoSize = true;
                                posLabel.Location = new Point(34, (int)yPos);
                                posLabel.Font = new Font("Segoe UI", 8F);
                                posLabel.ForeColor = Color.Black;
                                posLabel.BackColor = Color.Transparent;
                                posLabel.Text = position;
                                cellPanel.Controls.Add(posLabel);

                                yPos += 16;
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

        private void DrawMergedInfo(Panel p)
        {
            float y = 5;
            float leftCol = 5;
            float rightCol = p.Width / 2 + 5;
            Label lbl;

            lbl = MakeInfoLabel(m_chartData.Name + ", " + m_chartData.Sex, 8F, FontStyle.Bold, Color.FromArgb(0, 0, 153));
            lbl.Location = new Point((int)leftCol, (int)y); lbl.AutoSize = true; p.Controls.Add(lbl);
            y += 17;

            lbl = MakeInfoLabel("DOB: " + m_chartData.DateTimeOfBirth, 7.5F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)leftCol, (int)y); lbl.AutoSize = true; p.Controls.Add(lbl);
            y += 16;

            lbl = MakeInfoLabel("Place: " + m_chartData.PlaceOfBirth, 7.5F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)leftCol, (int)y); lbl.AutoSize = true; p.Controls.Add(lbl);
            y += 16;

            lbl = MakeInfoLabel("Star: " + m_chartData.MoonStarInfo, 7.5F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)leftCol, (int)y); lbl.AutoSize = true; p.Controls.Add(lbl);
            y += 16;

            lbl = MakeInfoLabel("Dasa: " + m_chartData.DasaBalance, 7.5F, FontStyle.Bold, Color.Black);
            lbl.Location = new Point((int)leftCol, (int)y); lbl.AutoSize = true; p.Controls.Add(lbl);
            y += 18;

            float y2 = 5;
            lbl = MakeInfoLabel("Long: " + m_chartData.Longitude, 7.5F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)rightCol, (int)y2); lbl.AutoSize = true; p.Controls.Add(lbl);
            y2 += 16;

            lbl = MakeInfoLabel("Lat: " + m_chartData.Latitude, 7.5F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)rightCol, (int)y2); lbl.AutoSize = true; p.Controls.Add(lbl);
            y2 += 18;

            lbl = MakeInfoLabel("Ayanamsa: " + m_chartData.Ayanamsa, 7F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)rightCol, (int)y2); lbl.AutoSize = true; p.Controls.Add(lbl);
            y2 += 16;

            lbl = MakeInfoLabel("Sidereal: " + m_chartData.SiderealTime, 7F, FontStyle.Regular, Color.Black);
            lbl.Location = new Point((int)rightCol, (int)y2); lbl.AutoSize = true; p.Controls.Add(lbl);
        }

        private Label MakeInfoLabel(string text, float fontSize, FontStyle style, Color foreColor)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", fontSize, style);
            lbl.ForeColor = foreColor;
            lbl.BackColor = Color.Transparent;
            return lbl;
        }

        private void DisableSorting(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetupPlanetTable()
        {
            dgvPlanets.Columns.Clear();
            dgvPlanets.Columns.Add("Planet", "Planet");
            dgvPlanets.Columns.Add("SignLord", "Sgn");
            dgvPlanets.Columns.Add("StarLord", "Str");
            dgvPlanets.Columns.Add("SubLord", "Sub");
            dgvPlanets.Columns.Add("SSLord", "SS");

            dgvPlanets.Columns[0].Width = 55;
            dgvPlanets.Columns[1].Width = 40;
            dgvPlanets.Columns[2].Width = 40;
            dgvPlanets.Columns[3].Width = 40;
            dgvPlanets.Columns[4].Width = 40;

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

            dgvCusps.Columns[0].Width = 55;
            dgvCusps.Columns[1].Width = 40;
            dgvCusps.Columns[2].Width = 40;
            dgvCusps.Columns[3].Width = 40;
            dgvCusps.Columns[4].Width = 40;

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

            dgvSignification.Columns[0].Width = 250;
            dgvSignification.Columns[1].Width = 55;
            dgvSignification.Columns[2].Width = 55;
            dgvSignification.Columns[3].Width = 55;
            dgvSignification.Columns[4].Width = 250;

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
