namespace logicAstroKPCharts
{
    partial class ResultsForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.btnPDF = new System.Windows.Forms.Button();
            this.btnFullScreen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
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

            this.pnlTop.SuspendLayout();
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
            // pnlTop
            //
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlTop.Controls.Add(this.lblDetails);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.btnPDF);
            this.pnlTop.Controls.Add(this.btnFullScreen);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1600, 62);
            this.pnlTop.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(153)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(700, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KP Astrology Chart";
            //
            // lblDetails
            //
            this.lblDetails.AutoSize = true;
            this.lblDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblDetails.ForeColor = System.Drawing.Color.Black;
            this.lblDetails.Location = new System.Drawing.Point(12, 35);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(200, 15);
            this.lblDetails.TabIndex = 4;
            this.lblDetails.Text = "";
            //
            // btnPDF
            //
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPDF.ForeColor = System.Drawing.Color.White;
            this.btnPDF.Location = new System.Drawing.Point(1320, 8);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(90, 30);
            this.btnPDF.TabIndex = 1;
            this.btnPDF.Text = "Save PDF";
            this.btnPDF.UseVisualStyleBackColor = false;
            //
            // btnFullScreen
            //
            this.btnFullScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFullScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(51)))));
            this.btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullScreen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFullScreen.ForeColor = System.Drawing.Color.White;
            this.btnFullScreen.Location = new System.Drawing.Point(1420, 8);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(90, 30);
            this.btnFullScreen.TabIndex = 2;
            this.btnFullScreen.Text = "Full Screen";
            this.btnFullScreen.UseVisualStyleBackColor = false;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1520, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            //
            // splitAB
            //
            this.splitAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAB.Location = new System.Drawing.Point(0, 62);
            this.splitAB.Name = "splitAB";
            //
            // splitAB.Panel1
            //
            this.splitAB.Panel1.Controls.Add(this.splitCD);
            //
            // splitAB.Panel2
            //
            this.splitAB.Panel2.Controls.Add(this.splitEF);
            this.splitAB.Size = new System.Drawing.Size(1600, 788);
            this.splitAB.SplitterDistance = 800;
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
            this.splitCD.Size = new System.Drawing.Size(800, 805);
            this.splitCD.SplitterDistance = 250;
            //
            // panelChart
            //
            this.panelChart.BackColor = System.Drawing.Color.White;
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 0);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(250, 805);
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
            this.lblPlanetHeader.Size = new System.Drawing.Size(550, 28);
            this.lblPlanetHeader.TabIndex = 0;
            this.lblPlanetHeader.Text = "Planet Positions";
            this.lblPlanetHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlPlanetLegend
            //
            this.pnlPlanetLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.pnlPlanetLegend.Controls.Add(this.lblPlanetLegend);
            this.pnlPlanetLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlanetLegend.Location = new System.Drawing.Point(0, 755);
            this.pnlPlanetLegend.Name = "pnlPlanetLegend";
            this.pnlPlanetLegend.Size = new System.Drawing.Size(550, 50);
            this.pnlPlanetLegend.TabIndex = 2;
            //
            // lblPlanetLegend
            //
            this.lblPlanetLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlanetLegend.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPlanetLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPlanetLegend.Location = new System.Drawing.Point(0, 0);
            this.lblPlanetLegend.Name = "lblPlanetLegend";
            this.lblPlanetLegend.Size = new System.Drawing.Size(550, 50);
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
            this.dgvPlanets.Size = new System.Drawing.Size(550, 727);
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
            this.splitEF.Size = new System.Drawing.Size(800, 805);
            this.splitEF.SplitterDistance = 400;
            //
            // lblCuspHeader
            //
            this.lblCuspHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(140)))), ((int)(((byte)(8)))));
            this.lblCuspHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCuspHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCuspHeader.ForeColor = System.Drawing.Color.White;
            this.lblCuspHeader.Location = new System.Drawing.Point(0, 0);
            this.lblCuspHeader.Name = "lblCuspHeader";
            this.lblCuspHeader.Size = new System.Drawing.Size(400, 28);
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
            this.dgvCusps.Size = new System.Drawing.Size(400, 777);
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
            this.lblSigHeader.Size = new System.Drawing.Size(396, 28);
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
            this.dgvSignification.Size = new System.Drawing.Size(396, 752);
            this.dgvSignification.TabIndex = 1;
            //
            // lblLegend
            //
            this.lblLegend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.lblLegend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLegend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLegend.Location = new System.Drawing.Point(0, 780);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(396, 25);
            this.lblLegend.TabIndex = 2;
            this.lblLegend.Text = "# Self star  * No planets  R Retrograde  Blue=Owner  Red=Deposited";
            this.lblLegend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ResultsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1600, 850);
            this.Controls.Add(this.splitAB);
            this.Controls.Add(this.pnlTop);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "ResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KP Astrology Results";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
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

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitAB;
        private System.Windows.Forms.SplitContainer splitCD;
        private System.Windows.Forms.SplitContainer splitEF;
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
    }
}
