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
            this.splitAB = new System.Windows.Forms.SplitContainer();
            this.splitCD = new System.Windows.Forms.SplitContainer();
            this.panelChart = new System.Windows.Forms.Panel();
            this.dgvPlanets = new System.Windows.Forms.DataGridView();
            this.lblPlanetHeader = new System.Windows.Forms.Label();
            this.pnlPlanetLegend = new System.Windows.Forms.Panel();
            this.lblPlanetLegend = new System.Windows.Forms.Label();
            this.splitEF = new System.Windows.Forms.SplitContainer();
            this.dgvCusps = new System.Windows.Forms.DataGridView();
            this.lblCuspHeader = new System.Windows.Forms.Label();
            this.dgvSignification = new System.Windows.Forms.DataGridView();
            this.lblSigHeader = new System.Windows.Forms.Label();
            this.lblLegend = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitAB)).BeginInit();
            this.splitAB.Panel1.SuspendLayout();
            this.splitAB.Panel2.SuspendLayout();
            this.splitAB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCD)).BeginInit();
            this.splitCD.Panel1.SuspendLayout();
            this.splitCD.Panel2.SuspendLayout();
            this.splitCD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitEF)).BeginInit();
            this.splitEF.Panel1.SuspendLayout();
            this.splitEF.Panel2.SuspendLayout();
            this.splitEF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignification)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KP Astrology Chart";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitAB
            // 
            this.splitAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAB.Location = new System.Drawing.Point(0, 40);
            this.splitAB.Name = "splitAB";
            // 
            // splitAB.Panel1
            // 
            this.splitAB.Panel1.Controls.Add(this.splitCD);
            // 
            // splitAB.Panel2
            // 
            this.splitAB.Panel2.Controls.Add(this.splitEF);
            this.splitAB.Size = new System.Drawing.Size(900, 510);
            this.splitAB.SplitterDistance = 450;
            // 
            // splitCD
            // 
            this.splitCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCD.Location = new System.Drawing.Point(0, 0);
            this.splitCD.Name = "splitCD";
            // 
            // splitCD.Panel1
            // 
            this.splitCD.Panel1.Controls.Add(this.panelChart);
            // 
            // splitCD.Panel2
            // 
            this.splitCD.Panel2.Controls.Add(this.dgvPlanets);
            this.splitCD.Panel2.Controls.Add(this.pnlPlanetLegend);
            this.splitCD.Panel2.Controls.Add(this.lblPlanetHeader);
            this.splitCD.Size = new System.Drawing.Size(450, 510);
            this.splitCD.SplitterDistance = 200;
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 0);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(200, 510);
            this.panelChart.TabIndex = 0;
            // 
            // lblPlanetHeader
            // 
            this.lblPlanetHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblPlanetHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlanetHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPlanetHeader.ForeColor = System.Drawing.Color.White;
            this.lblPlanetHeader.Location = new System.Drawing.Point(0, 0);
            this.lblPlanetHeader.Name = "lblPlanetHeader";
            this.lblPlanetHeader.Size = new System.Drawing.Size(246, 28);
            this.lblPlanetHeader.TabIndex = 0;
            this.lblPlanetHeader.Text = "Planet Positions";
            this.lblPlanetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPlanetLegend
            // 
            this.pnlPlanetLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.pnlPlanetLegend.Controls.Add(this.lblPlanetLegend);
            this.pnlPlanetLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlanetLegend.Location = new System.Drawing.Point(0, 460);
            this.pnlPlanetLegend.Name = "pnlPlanetLegend";
            this.pnlPlanetLegend.Size = new System.Drawing.Size(246, 50);
            this.pnlPlanetLegend.TabIndex = 2;
            // 
            // lblPlanetLegend
            // 
            this.lblPlanetLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanetLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPlanetLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPlanetLegend.Location = new System.Drawing.Point(0, 0);
            this.lblPlanetLegend.Name = "lblPlanetLegend";
            this.lblPlanetLegend.Size = new System.Drawing.Size(246, 50);
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
            this.dgvPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPlanets.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPlanets.ColumnHeadersHeight = 30;
            this.dgvPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPlanets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanets.EnableHeadersVisualStyles = false;
            this.dgvPlanets.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvPlanets.Location = new System.Drawing.Point(0, 28);
            this.dgvPlanets.Name = "dgvPlanets";
            this.dgvPlanets.ReadOnly = true;
            this.dgvPlanets.RowHeadersVisible = false;
            this.dgvPlanets.RowTemplate.Height = 26;
            this.dgvPlanets.Size = new System.Drawing.Size(246, 432);
            this.dgvPlanets.TabIndex = 1;
            // 
            // splitEF
            // 
            this.splitEF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitEF.Location = new System.Drawing.Point(0, 0);
            this.splitEF.Name = "splitEF";
            // 
            // splitEF.Panel1
            // 
            this.splitEF.Panel1.Controls.Add(this.dgvCusps);
            this.splitEF.Panel1.Controls.Add(this.lblCuspHeader);
            // 
            // splitEF.Panel2
            // 
            this.splitEF.Panel2.Controls.Add(this.dgvSignification);
            this.splitEF.Panel2.Controls.Add(this.lblSigHeader);
            this.splitEF.Panel2.Controls.Add(this.lblLegend);
            this.splitEF.Size = new System.Drawing.Size(446, 510);
            this.splitEF.SplitterDistance = 220;
            // 
            // lblCuspHeader
            // 
            this.lblCuspHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblCuspHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCuspHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCuspHeader.ForeColor = System.Drawing.Color.White;
            this.lblCuspHeader.Location = new System.Drawing.Point(0, 0);
            this.lblCuspHeader.Name = "lblCuspHeader";
            this.lblCuspHeader.Size = new System.Drawing.Size(220, 28);
            this.lblCuspHeader.TabIndex = 0;
            this.lblCuspHeader.Text = "Cuspal House Positions";
            this.lblCuspHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCusps
            // 
            this.dgvCusps.AllowUserToAddRows = false;
            this.dgvCusps.AllowUserToDeleteRows = false;
            this.dgvCusps.AllowUserToResizeRows = false;
            this.dgvCusps.BackgroundColor = System.Drawing.Color.White;
            this.dgvCusps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCusps.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvCusps.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCusps.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCusps.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvCusps.ColumnHeadersHeight = 30;
            this.dgvCusps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCusps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCusps.EnableHeadersVisualStyles = false;
            this.dgvCusps.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvCusps.Location = new System.Drawing.Point(0, 28);
            this.dgvCusps.Name = "dgvCusps";
            this.dgvCusps.ReadOnly = true;
            this.dgvCusps.RowHeadersVisible = false;
            this.dgvCusps.RowTemplate.Height = 26;
            this.dgvCusps.Size = new System.Drawing.Size(220, 482);
            this.dgvCusps.TabIndex = 1;
            // 
            // lblSigHeader
            // 
            this.lblSigHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblSigHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSigHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSigHeader.ForeColor = System.Drawing.Color.White;
            this.lblSigHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSigHeader.Name = "lblSigHeader";
            this.lblSigHeader.Size = new System.Drawing.Size(222, 28);
            this.lblSigHeader.TabIndex = 0;
            this.lblSigHeader.Text = "House Signification";
            this.lblSigHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSignification
            // 
            this.dgvSignification.AllowUserToAddRows = false;
            this.dgvSignification.AllowUserToDeleteRows = false;
            this.dgvSignification.AllowUserToResizeRows = false;
            this.dgvSignification.BackgroundColor = System.Drawing.Color.White;
            this.dgvSignification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSignification.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(230)))), ((int)(((byte)(99)))));
            this.dgvSignification.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvSignification.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvSignification.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSignification.ColumnHeadersHeight = 30;
            this.dgvSignification.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSignification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSignification.EnableHeadersVisualStyles = false;
            this.dgvSignification.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvSignification.Location = new System.Drawing.Point(0, 28);
            this.dgvSignification.Name = "dgvSignification";
            this.dgvSignification.ReadOnly = true;
            this.dgvSignification.RowHeadersVisible = false;
            this.dgvSignification.RowTemplate.Height = 26;
            this.dgvSignification.Size = new System.Drawing.Size(222, 457);
            this.dgvSignification.TabIndex = 1;
            // 
            // lblLegend
            // 
            this.lblLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLegend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLegend.Location = new System.Drawing.Point(0, 485);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(222, 25);
            this.lblLegend.TabIndex = 2;
            this.lblLegend.Text = "# Self star  * No planets  R Retrograde  Blue=Owner  Red=Deposited";
            this.lblLegend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChartResultsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.splitAB);
            this.Controls.Add(this.lblTitle);
            this.Name = "ChartResultsUserControl";
            this.Size = new System.Drawing.Size(900, 550);
            this.splitAB.Panel1.ResumeLayout(false);
            this.splitAB.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAB)).EndInit();
            this.splitAB.ResumeLayout(false);
            this.splitCD.Panel1.ResumeLayout(false);
            this.splitCD.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCD)).EndInit();
            this.splitCD.ResumeLayout(false);
            this.pnlPlanetLegend.ResumeLayout(false);
            this.splitEF.Panel1.ResumeLayout(false);
            this.splitEF.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitEF)).EndInit();
            this.splitEF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCusps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignification)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitAB;
        private System.Windows.Forms.SplitContainer splitCD;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataGridView dgvPlanets;
        private System.Windows.Forms.DataGridView dgvCusps;
        private System.Windows.Forms.DataGridView dgvSignification;
        private System.Windows.Forms.Label lblPlanetHeader;
        private System.Windows.Forms.Label lblCuspHeader;
        private System.Windows.Forms.Label lblSigHeader;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.Panel pnlPlanetLegend;
        private System.Windows.Forms.Label lblPlanetLegend;
        private System.Windows.Forms.SplitContainer splitEF;
    }
}
