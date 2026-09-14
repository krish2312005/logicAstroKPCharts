namespace logicAstroKPCharts
{
    partial class ChartResultsUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLagna = new System.Windows.Forms.Panel();
            this.lblLagnaHeader = new System.Windows.Forms.Label();
            this.panelLagnaChart = new System.Windows.Forms.Panel();
            this.pnlKp = new System.Windows.Forms.Panel();
            this.lblKpHeader = new System.Windows.Forms.Label();
            this.panelKpChart = new System.Windows.Forms.Panel();
            this.pnlCenter = new System.Windows.Forms.Panel();
            this.tlpCenter = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCusps = new System.Windows.Forms.Panel();
            this.dgvCusps = new System.Windows.Forms.DataGridView();
            this.lblCuspHeader = new System.Windows.Forms.Label();
            this.pnlPlanets = new System.Windows.Forms.Panel();
            this.dgvPlanets = new System.Windows.Forms.DataGridView();
            this.pnlPlanetLegend = new System.Windows.Forms.Panel();
            this.lblPlanetLegend = new System.Windows.Forms.Label();
            this.lblPlanetHeader = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSig = new System.Windows.Forms.Panel();
            this.dgvSignification = new System.Windows.Forms.DataGridView();
            this.lblLegend = new System.Windows.Forms.Label();
            this.lblSigHeader = new System.Windows.Forms.Label();
            this.pnlNadi = new System.Windows.Forms.Panel();
            this.dgvNadi = new System.Windows.Forms.DataGridView();
            this.lblNadiHeader = new System.Windows.Forms.Label();

            this.tlpRoot.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            this.pnlLagna.SuspendLayout();
            this.pnlKp.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            this.tlpCenter.SuspendLayout();
            this.pnlCusps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusps)).BeginInit();
            this.pnlPlanets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).BeginInit();
            this.pnlPlanetLegend.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.pnlSig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignification)).BeginInit();
            this.pnlNadi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNadi)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1600, 48);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KP Astrology Chart";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // tlpRoot
            //
            this.tlpRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tlpRoot.ColumnCount = 3;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tlpRoot.Controls.Add(this.tlpLeft, 0, 0);
            this.tlpRoot.Controls.Add(this.pnlCenter, 1, 0);
            this.tlpRoot.Controls.Add(this.pnlRight, 2, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 48);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(1600, 852);
            this.tlpRoot.TabIndex = 1;
            //
            // tlpLeft
            //
            this.tlpLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.Controls.Add(this.pnlLagna, 0, 0);
            this.tlpLeft.Controls.Add(this.pnlKp, 0, 1);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(6, 6);
            this.tlpLeft.Margin = new System.Windows.Forms.Padding(6);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 2;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.Size = new System.Drawing.Size(388, 848);
            this.tlpLeft.TabIndex = 0;
            //
            // pnlLagna
            //
            this.pnlLagna.BackColor = System.Drawing.Color.White;
            this.pnlLagna.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLagna.Controls.Add(this.panelLagnaChart);
            this.pnlLagna.Controls.Add(this.lblLagnaHeader);
            this.pnlLagna.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLagna.Location = new System.Drawing.Point(0, 0);
            this.pnlLagna.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.pnlLagna.Name = "pnlLagna";
            this.pnlLagna.Size = new System.Drawing.Size(388, 418);
            this.pnlLagna.TabIndex = 0;
            //
            // lblLagnaHeader
            //
            this.lblLagnaHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblLagnaHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLagnaHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblLagnaHeader.ForeColor = System.Drawing.Color.White;
            this.lblLagnaHeader.Location = new System.Drawing.Point(0, 0);
            this.lblLagnaHeader.Name = "lblLagnaHeader";
            this.lblLagnaHeader.Size = new System.Drawing.Size(386, 26);
            this.lblLagnaHeader.TabIndex = 0;
            this.lblLagnaHeader.Text = "LAGNA CHART";
            this.lblLagnaHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panelLagnaChart
            //
            this.panelLagnaChart.BackColor = System.Drawing.Color.White;
            this.panelLagnaChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLagnaChart.Location = new System.Drawing.Point(0, 26);
            this.panelLagnaChart.Margin = new System.Windows.Forms.Padding(4);
            this.panelLagnaChart.Name = "panelLagnaChart";
            this.panelLagnaChart.Size = new System.Drawing.Size(378, 388);
            this.panelLagnaChart.TabIndex = 1;
            //
            // pnlKp
            //
            this.pnlKp.BackColor = System.Drawing.Color.White;
            this.pnlKp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKp.Controls.Add(this.panelKpChart);
            this.pnlKp.Controls.Add(this.lblKpHeader);
            this.pnlKp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKp.Location = new System.Drawing.Point(0, 424);
            this.pnlKp.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlKp.Name = "pnlKp";
            this.pnlKp.Size = new System.Drawing.Size(388, 418);
            this.pnlKp.TabIndex = 1;
            //
            // lblKpHeader
            //
            this.lblKpHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblKpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblKpHeader.ForeColor = System.Drawing.Color.White;
            this.lblKpHeader.Location = new System.Drawing.Point(0, 0);
            this.lblKpHeader.Name = "lblKpHeader";
            this.lblKpHeader.Size = new System.Drawing.Size(386, 26);
            this.lblKpHeader.TabIndex = 0;
            this.lblKpHeader.Text = "KP CHART";
            this.lblKpHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // panelKpChart
            //
            this.panelKpChart.BackColor = System.Drawing.Color.White;
            this.panelKpChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpChart.Location = new System.Drawing.Point(0, 26);
            this.panelKpChart.Margin = new System.Windows.Forms.Padding(4);
            this.panelKpChart.Name = "panelKpChart";
            this.panelKpChart.Size = new System.Drawing.Size(378, 388);
            this.panelKpChart.TabIndex = 1;
            //
            // pnlCenter
            //
            this.pnlCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlCenter.Controls.Add(this.tlpCenter);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(406, 6);
            this.pnlCenter.Margin = new System.Windows.Forms.Padding(6);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Size = new System.Drawing.Size(588, 848);
            this.pnlCenter.TabIndex = 1;
            //
            // tlpCenter
            //
            this.tlpCenter.ColumnCount = 1;
            this.tlpCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCenter.Controls.Add(this.pnlCusps, 0, 0);
            this.tlpCenter.Controls.Add(this.pnlPlanets, 0, 1);
            this.tlpCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCenter.Location = new System.Drawing.Point(0, 0);
            this.tlpCenter.Name = "tlpCenter";
            this.tlpCenter.RowCount = 2;
            this.tlpCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tlpCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tlpCenter.Size = new System.Drawing.Size(588, 848);
            this.tlpCenter.TabIndex = 0;
            //
            // pnlCusps
            //
            this.pnlCusps.BackColor = System.Drawing.Color.White;
            this.pnlCusps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCusps.Controls.Add(this.dgvCusps);
            this.pnlCusps.Controls.Add(this.lblCuspHeader);
            this.pnlCusps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCusps.Location = new System.Drawing.Point(0, 0);
            this.pnlCusps.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlCusps.Name = "pnlCusps";
            this.pnlCusps.Size = new System.Drawing.Size(588, 433);
            this.pnlCusps.TabIndex = 0;
            //
            // lblCuspHeader
            //
            this.lblCuspHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblCuspHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCuspHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblCuspHeader.ForeColor = System.Drawing.Color.White;
            this.lblCuspHeader.Location = new System.Drawing.Point(0, 0);
            this.lblCuspHeader.Name = "lblCuspHeader";
            this.lblCuspHeader.Size = new System.Drawing.Size(586, 26);
            this.lblCuspHeader.TabIndex = 0;
            this.lblCuspHeader.Text = "CUSPAL HOUSE POSITIONS";
            this.lblCuspHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // dgvCusps
            //
            this.dgvCusps.AllowUserToAddRows = false;
            this.dgvCusps.AllowUserToDeleteRows = false;
            this.dgvCusps.AllowUserToResizeRows = false;
            this.dgvCusps.BackgroundColor = System.Drawing.Color.White;
            this.dgvCusps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvCusps.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCusps.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCusps.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvCusps.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvCusps.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCusps.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvCusps.ColumnHeadersHeight = 26;
            this.dgvCusps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCusps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCusps.EnableHeadersVisualStyles = false;
            this.dgvCusps.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCusps.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvCusps.Location = new System.Drawing.Point(0, 26);
            this.dgvCusps.Name = "dgvCusps";
            this.dgvCusps.ReadOnly = true;
            this.dgvCusps.RowHeadersVisible = false;
            this.dgvCusps.RowTemplate.Height = 23;
            this.dgvCusps.Size = new System.Drawing.Size(586, 405);
            this.dgvCusps.TabIndex = 1;
            this.dgvCusps.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(253)))), ((int)(((byte)(246)))));
            //
            // pnlPlanets
            //
            this.pnlPlanets.BackColor = System.Drawing.Color.White;
            this.pnlPlanets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPlanets.Controls.Add(this.dgvPlanets);
            this.pnlPlanets.Controls.Add(this.pnlPlanetLegend);
            this.pnlPlanets.Controls.Add(this.lblPlanetHeader);
            this.pnlPlanets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPlanets.Location = new System.Drawing.Point(0, 441);
            this.pnlPlanets.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPlanets.Name = "pnlPlanets";
            this.pnlPlanets.Size = new System.Drawing.Size(588, 407);
            this.pnlPlanets.TabIndex = 1;
            //
            // lblPlanetHeader
            //
            this.lblPlanetHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblPlanetHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlanetHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblPlanetHeader.ForeColor = System.Drawing.Color.White;
            this.lblPlanetHeader.Location = new System.Drawing.Point(0, 0);
            this.lblPlanetHeader.Name = "lblPlanetHeader";
            this.lblPlanetHeader.Size = new System.Drawing.Size(586, 26);
            this.lblPlanetHeader.TabIndex = 0;
            this.lblPlanetHeader.Text = "PLANET POSITIONS";
            this.lblPlanetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlPlanetLegend
            //
            this.pnlPlanetLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.pnlPlanetLegend.Controls.Add(this.lblPlanetLegend);
            this.pnlPlanetLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlanetLegend.Location = new System.Drawing.Point(0, 353);
            this.pnlPlanetLegend.Name = "pnlPlanetLegend";
            this.pnlPlanetLegend.Size = new System.Drawing.Size(586, 52);
            this.pnlPlanetLegend.TabIndex = 2;
            //
            // lblPlanetLegend
            //
            this.lblPlanetLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanetLegend.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPlanetLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPlanetLegend.Location = new System.Drawing.Point(0, 0);
            this.lblPlanetLegend.Name = "lblPlanetLegend";
            this.lblPlanetLegend.Size = new System.Drawing.Size(586, 52);
            this.lblPlanetLegend.TabIndex = 0;
            this.lblPlanetLegend.Text = "";
            this.lblPlanetLegend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // dgvPlanets
            //
            this.dgvPlanets.AllowUserToAddRows = false;
            this.dgvPlanets.AllowUserToDeleteRows = false;
            this.dgvPlanets.AllowUserToResizeRows = false;
            this.dgvPlanets.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlanets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPlanets.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPlanets.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPlanets.ColumnHeadersHeight = 26;
            this.dgvPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPlanets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanets.EnableHeadersVisualStyles = false;
            this.dgvPlanets.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPlanets.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvPlanets.Location = new System.Drawing.Point(0, 26);
            this.dgvPlanets.Name = "dgvPlanets";
            this.dgvPlanets.ReadOnly = true;
            this.dgvPlanets.RowHeadersVisible = false;
            this.dgvPlanets.RowTemplate.Height = 23;
            this.dgvPlanets.Size = new System.Drawing.Size(586, 327);
            this.dgvPlanets.TabIndex = 1;
            this.dgvPlanets.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(253)))), ((int)(((byte)(246)))));
            //
            // pnlRight
            //
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlRight.Controls.Add(this.tlpRight);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(1006, 6);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(6);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(588, 848);
            this.pnlRight.TabIndex = 2;
            //
            // tlpRight
            //
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.pnlSig, 0, 0);
            this.tlpRight.Controls.Add(this.pnlNadi, 0, 1);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(0, 0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 2;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRight.Size = new System.Drawing.Size(588, 848);
            this.tlpRight.TabIndex = 0;
            //
            // pnlSig
            //
            this.pnlSig.BackColor = System.Drawing.Color.White;
            this.pnlSig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSig.Controls.Add(this.dgvSignification);
            this.pnlSig.Controls.Add(this.lblLegend);
            this.pnlSig.Controls.Add(this.lblSigHeader);
            this.pnlSig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSig.Location = new System.Drawing.Point(0, 0);
            this.pnlSig.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlSig.Name = "pnlSig";
            this.pnlSig.Size = new System.Drawing.Size(588, 416);
            this.pnlSig.TabIndex = 0;
            //
            // lblSigHeader
            //
            this.lblSigHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblSigHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSigHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSigHeader.ForeColor = System.Drawing.Color.White;
            this.lblSigHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSigHeader.Name = "lblSigHeader";
            this.lblSigHeader.Size = new System.Drawing.Size(586, 26);
            this.lblSigHeader.TabIndex = 0;
            this.lblSigHeader.Text = "HOUSE SIGNIFICATION";
            this.lblSigHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblLegend
            //
            this.lblLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLegend.Location = new System.Drawing.Point(0, 389);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(586, 25);
            this.lblLegend.TabIndex = 2;
            this.lblLegend.Text = "# Self star  * No planets  R Retrograde  Blue=Owner  Red=Deposited";
            this.lblLegend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // dgvSignification
            //
            this.dgvSignification.AllowUserToAddRows = false;
            this.dgvSignification.AllowUserToDeleteRows = false;
            this.dgvSignification.AllowUserToResizeRows = false;
            this.dgvSignification.BackgroundColor = System.Drawing.Color.White;
            this.dgvSignification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvSignification.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSignification.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSignification.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvSignification.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvSignification.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvSignification.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSignification.ColumnHeadersHeight = 26;
            this.dgvSignification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSignification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSignification.EnableHeadersVisualStyles = false;
            this.dgvSignification.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvSignification.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvSignification.Location = new System.Drawing.Point(0, 26);
            this.dgvSignification.Name = "dgvSignification";
            this.dgvSignification.ReadOnly = true;
            this.dgvSignification.RowHeadersVisible = false;
            this.dgvSignification.RowTemplate.Height = 23;
            this.dgvSignification.Size = new System.Drawing.Size(586, 363);
            this.dgvSignification.TabIndex = 1;
            this.dgvSignification.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(253)))), ((int)(((byte)(246)))));
            //
            // pnlNadi
            //
            this.pnlNadi.BackColor = System.Drawing.Color.White;
            this.pnlNadi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNadi.Controls.Add(this.dgvNadi);
            this.pnlNadi.Controls.Add(this.lblNadiHeader);
            this.pnlNadi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNadi.Location = new System.Drawing.Point(0, 424);
            this.pnlNadi.Margin = new System.Windows.Forms.Padding(0);
            this.pnlNadi.Name = "pnlNadi";
            this.pnlNadi.Size = new System.Drawing.Size(588, 424);
            this.pnlNadi.TabIndex = 1;
            //
            // lblNadiHeader
            //
            this.lblNadiHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblNadiHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNadiHeader.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNadiHeader.ForeColor = System.Drawing.Color.White;
            this.lblNadiHeader.Location = new System.Drawing.Point(0, 0);
            this.lblNadiHeader.Name = "lblNadiHeader";
            this.lblNadiHeader.Size = new System.Drawing.Size(586, 26);
            this.lblNadiHeader.TabIndex = 0;
            this.lblNadiHeader.Text = "NADI COORDINATES";
            this.lblNadiHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // dgvNadi
            //
            this.dgvNadi.AllowUserToAddRows = false;
            this.dgvNadi.AllowUserToDeleteRows = false;
            this.dgvNadi.AllowUserToResizeRows = false;
            this.dgvNadi.BackgroundColor = System.Drawing.Color.White;
            this.dgvNadi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvNadi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNadi.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvNadi.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvNadi.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvNadi.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvNadi.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvNadi.ColumnHeadersHeight = 26;
            this.dgvNadi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNadi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNadi.EnableHeadersVisualStyles = false;
            this.dgvNadi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvNadi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvNadi.Location = new System.Drawing.Point(0, 26);
            this.dgvNadi.Name = "dgvNadi";
            this.dgvNadi.ReadOnly = true;
            this.dgvNadi.RowHeadersVisible = false;
            this.dgvNadi.RowTemplate.Height = 23;
            this.dgvNadi.Size = new System.Drawing.Size(586, 396);
            this.dgvNadi.TabIndex = 1;
            this.dgvNadi.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(253)))), ((int)(((byte)(246)))));
            //
            // ChartResultsUserControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.tlpRoot);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChartResultsUserControl";
            this.Size = new System.Drawing.Size(1600, 900);
            this.tlpRoot.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            this.pnlLagna.ResumeLayout(false);
            this.pnlKp.ResumeLayout(false);
            this.pnlCenter.ResumeLayout(false);
            this.tlpCenter.ResumeLayout(false);
            this.pnlCusps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusps)).EndInit();
            this.pnlPlanets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).EndInit();
            this.pnlPlanetLegend.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.tlpRight.ResumeLayout(false);
            this.pnlSig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignification)).EndInit();
            this.pnlNadi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNadi)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.Panel pnlLagna;
        private System.Windows.Forms.Label lblLagnaHeader;
        private System.Windows.Forms.Panel panelLagnaChart;
        private System.Windows.Forms.Panel pnlKp;
        private System.Windows.Forms.Label lblKpHeader;
        private System.Windows.Forms.Panel panelKpChart;
        private System.Windows.Forms.Panel pnlCenter;
        private System.Windows.Forms.TableLayoutPanel tlpCenter;
        private System.Windows.Forms.Panel pnlCusps;
        private System.Windows.Forms.DataGridView dgvCusps;
        private System.Windows.Forms.Label lblCuspHeader;
        private System.Windows.Forms.Panel pnlPlanets;
        private System.Windows.Forms.DataGridView dgvPlanets;
        private System.Windows.Forms.Label lblPlanetHeader;
        private System.Windows.Forms.Panel pnlPlanetLegend;
        private System.Windows.Forms.Label lblPlanetLegend;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.Panel pnlSig;
        private System.Windows.Forms.DataGridView dgvSignification;
        private System.Windows.Forms.Label lblSigHeader;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.Panel pnlNadi;
        private System.Windows.Forms.DataGridView dgvNadi;
        private System.Windows.Forms.Label lblNadiHeader;
    }
}