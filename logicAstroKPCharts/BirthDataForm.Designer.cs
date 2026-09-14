namespace logicAstroKPCharts
{
    partial class BirthDataForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblDOB = new System.Windows.Forms.Label();
            this.txtDD = new System.Windows.Forms.TextBox();
            this.lblSepChar1 = new System.Windows.Forms.Label();
            this.txtMM = new System.Windows.Forms.TextBox();
            this.lblSepChar2 = new System.Windows.Forms.Label();
            this.txtYYYY = new System.Windows.Forms.TextBox();
            this.lblDateFormat = new System.Windows.Forms.Label();
            this.lblTOB = new System.Windows.Forms.Label();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.lblSepChar3 = new System.Windows.Forms.Label();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.lblSepChar4 = new System.Windows.Forms.Label();
            this.txtSec = new System.Windows.Forms.TextBox();
            this.lblTimeFormat = new System.Windows.Forms.Label();
            this.lblTimeZone = new System.Windows.Forms.Label();
            this.cmbTimeZone = new System.Windows.Forms.ComboBox();
            this.lblPOB = new System.Windows.Forms.Label();
            this.txtPOB = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtSOB = new System.Windows.Forms.TextBox();
            this.labelCountry = new System.Windows.Forms.Label();
            this.txtCOB = new System.Windows.Forms.TextBox();
            this.lblLonLatWarning = new System.Windows.Forms.Label();
            this.btnWhyLonLat = new System.Windows.Forms.Button();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.cmbLonDirection = new System.Windows.Forms.ComboBox();
            this.txtLonDegrees = new System.Windows.Forms.TextBox();
            this.lblSepChar5 = new System.Windows.Forms.Label();
            this.txtLonMinutes = new System.Windows.Forms.TextBox();
            this.lblLonFormat = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.cmbLatDirection = new System.Windows.Forms.ComboBox();
            this.txtLatDegrees = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLatMinutes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAyanamsa = new System.Windows.Forms.Label();
            this.cmbAyanamsa = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNewChart = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.btnNow = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblName.Location = new System.Drawing.Point(30, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 25);
            this.txtName.TabIndex = 1;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameValue_KeyPressed);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblGender.Location = new System.Drawing.Point(400, 20);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(55, 17);
            this.lblGender.TabIndex = 2;
            this.lblGender.Text = "Gender:";
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Items.AddRange(new object[] { "Male", "Female", "Transgender", "Unknown" });
            this.cmbGender.Location = new System.Drawing.Point(470, 17);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(100, 25);
            this.cmbGender.TabIndex = 3;
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblDOB.Location = new System.Drawing.Point(30, 58);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(78, 17);
            this.lblDOB.TabIndex = 4;
            this.lblDOB.Text = "Date Of Birth:";
            // 
            // txtDD
            // 
            this.txtDD.Location = new System.Drawing.Point(135, 55);
            this.txtDD.MaxLength = 2;
            this.txtDD.Name = "txtDD";
            this.txtDD.Size = new System.Drawing.Size(35, 25);
            this.txtDD.TabIndex = 5;
            this.txtDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DayValue_KeyPressed);
            // 
            // lblSepChar1
            // 
            this.lblSepChar1.AutoSize = true;
            this.lblSepChar1.Location = new System.Drawing.Point(173, 58);
            this.lblSepChar1.Name = "lblSepChar1";
            this.lblSepChar1.Size = new System.Drawing.Size(12, 17);
            this.lblSepChar1.TabIndex = 6;
            this.lblSepChar1.Text = "/";
            // 
            // txtMM
            // 
            this.txtMM.Location = new System.Drawing.Point(190, 55);
            this.txtMM.MaxLength = 2;
            this.txtMM.Name = "txtMM";
            this.txtMM.Size = new System.Drawing.Size(35, 25);
            this.txtMM.TabIndex = 7;
            this.txtMM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MonthValue_KeyPressed);
            // 
            // lblSepChar2
            // 
            this.lblSepChar2.AutoSize = true;
            this.lblSepChar2.Location = new System.Drawing.Point(228, 58);
            this.lblSepChar2.Name = "lblSepChar2";
            this.lblSepChar2.Size = new System.Drawing.Size(12, 17);
            this.lblSepChar2.TabIndex = 8;
            this.lblSepChar2.Text = "/";
            // 
            // txtYYYY
            // 
            this.txtYYYY.Location = new System.Drawing.Point(245, 55);
            this.txtYYYY.MaxLength = 4;
            this.txtYYYY.Name = "txtYYYY";
            this.txtYYYY.Size = new System.Drawing.Size(60, 25);
            this.txtYYYY.TabIndex = 9;
            this.txtYYYY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YearValue_KeyPressed);
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.AutoSize = true;
            this.lblDateFormat.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblDateFormat.ForeColor = System.Drawing.Color.Gray;
            this.lblDateFormat.Location = new System.Drawing.Point(311, 58);
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(80, 13);
            this.lblDateFormat.TabIndex = 10;
            this.lblDateFormat.Text = "(DD/MM/YYYY)";
            // 
            // lblTOB
            // 
            this.lblTOB.AutoSize = true;
            this.lblTOB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTOB.Location = new System.Drawing.Point(30, 96);
            this.lblTOB.Name = "lblTOB";
            this.lblTOB.Size = new System.Drawing.Size(78, 17);
            this.lblTOB.TabIndex = 11;
            this.lblTOB.Text = "Time Of Birth:";
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(135, 93);
            this.txtHour.MaxLength = 2;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(35, 25);
            this.txtHour.TabIndex = 12;
            this.txtHour.Text = "00";
            this.txtHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HoursValue_KeyPressed);
            // 
            // lblSepChar3
            // 
            this.lblSepChar3.AutoSize = true;
            this.lblSepChar3.Location = new System.Drawing.Point(173, 96);
            this.lblSepChar3.Name = "lblSepChar3";
            this.lblSepChar3.Size = new System.Drawing.Size(13, 17);
            this.lblSepChar3.TabIndex = 13;
            this.lblSepChar3.Text = "--";
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(190, 93);
            this.txtMin.MaxLength = 2;
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(35, 25);
            this.txtMin.TabIndex = 14;
            this.txtMin.Text = "00";
            this.txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MinutesValue_KeyPressed);
            // 
            // lblSepChar4
            // 
            this.lblSepChar4.AutoSize = true;
            this.lblSepChar4.Location = new System.Drawing.Point(228, 96);
            this.lblSepChar4.Name = "lblSepChar4";
            this.lblSepChar4.Size = new System.Drawing.Size(13, 17);
            this.lblSepChar4.TabIndex = 15;
            this.lblSepChar4.Text = "--";
            // 
            // txtSec
            // 
            this.txtSec.Location = new System.Drawing.Point(245, 93);
            this.txtSec.MaxLength = 2;
            this.txtSec.Name = "txtSec";
            this.txtSec.Size = new System.Drawing.Size(35, 25);
            this.txtSec.TabIndex = 16;
            this.txtSec.Text = "00";
            this.txtSec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecondsValue_KeyPressed);
            // 
            // lblTimeFormat
            // 
            this.lblTimeFormat.AutoSize = true;
            this.lblTimeFormat.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblTimeFormat.ForeColor = System.Drawing.Color.Gray;
            this.lblTimeFormat.Location = new System.Drawing.Point(286, 96);
            this.lblTimeFormat.Name = "lblTimeFormat";
            this.lblTimeFormat.Size = new System.Drawing.Size(115, 13);
            this.lblTimeFormat.TabIndex = 17;
            this.lblTimeFormat.Text = "(HH--MM--SS / 24 hr)";
            // 
            // btnNow
            // 
            this.btnNow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNow.Location = new System.Drawing.Point(415, 91);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(60, 28);
            this.btnNow.TabIndex = 18;
            this.btnNow.Text = "Now";
            this.btnNow.UseVisualStyleBackColor = true;
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // lblTimeZone
            // 
            this.lblTimeZone.AutoSize = true;
            this.lblTimeZone.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTimeZone.Location = new System.Drawing.Point(30, 134);
            this.lblTimeZone.Name = "lblTimeZone";
            this.lblTimeZone.Size = new System.Drawing.Size(69, 17);
            this.lblTimeZone.TabIndex = 19;
            this.lblTimeZone.Text = "Time Zone:";
            // 
            // cmbTimeZone
            // 
            this.cmbTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeZone.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbTimeZone.FormattingEnabled = true;
            this.cmbTimeZone.Location = new System.Drawing.Point(135, 131);
            this.cmbTimeZone.Name = "cmbTimeZone";
            this.cmbTimeZone.Size = new System.Drawing.Size(460, 25);
            this.cmbTimeZone.TabIndex = 20;
            // 
            // lblPOB
            // 
            this.lblPOB.AutoSize = true;
            this.lblPOB.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblPOB.Location = new System.Drawing.Point(30, 172);
            this.lblPOB.Name = "lblPOB";
            this.lblPOB.Size = new System.Drawing.Size(79, 17);
            this.lblPOB.TabIndex = 21;
            this.lblPOB.Text = "Place Of Birth:";
            // 
            // txtPOB
            // 
            this.txtPOB.Location = new System.Drawing.Point(135, 169);
            this.txtPOB.Name = "txtPOB";
            this.txtPOB.Size = new System.Drawing.Size(240, 25);
            this.txtPOB.TabIndex = 22;
            this.txtPOB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.POBValue_KeyPressed);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblState.Location = new System.Drawing.Point(400, 172);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(40, 17);
            this.lblState.TabIndex = 23;
            this.lblState.Text = "State:";
            // 
            // txtSOB
            // 
            this.txtSOB.Location = new System.Drawing.Point(450, 169);
            this.txtSOB.Name = "txtSOB";
            this.txtSOB.Size = new System.Drawing.Size(150, 25);
            this.txtSOB.TabIndex = 24;
            // 
            // labelCountry
            // 
            this.labelCountry.AutoSize = true;
            this.labelCountry.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelCountry.Location = new System.Drawing.Point(30, 210);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(58, 17);
            this.labelCountry.TabIndex = 25;
            this.labelCountry.Text = "Country:";
            // 
            // txtCOB
            // 
            this.txtCOB.Location = new System.Drawing.Point(135, 207);
            this.txtCOB.Name = "txtCOB";
            this.txtCOB.Size = new System.Drawing.Size(180, 25);
            this.txtCOB.TabIndex = 26;
            // 
            // lblLonLatWarning
            // 
            this.lblLonLatWarning.AutoSize = true;
            this.lblLonLatWarning.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLonLatWarning.ForeColor = System.Drawing.Color.Red;
            this.lblLonLatWarning.Location = new System.Drawing.Point(120, 252);
            this.lblLonLatWarning.Name = "lblLonLatWarning";
            this.lblLonLatWarning.Size = new System.Drawing.Size(285, 19);
            this.lblLonLatWarning.TabIndex = 27;
            this.lblLonLatWarning.Text = "Enter Longitude - Latitude Values Manually";
            // 
            // btnWhyLonLat
            // 
            this.btnWhyLonLat.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnWhyLonLat.Location = new System.Drawing.Point(415, 249);
            this.btnWhyLonLat.Name = "btnWhyLonLat";
            this.btnWhyLonLat.Size = new System.Drawing.Size(50, 25);
            this.btnWhyLonLat.TabIndex = 28;
            this.btnWhyLonLat.Text = "Why?";
            this.btnWhyLonLat.UseVisualStyleBackColor = true;
            this.btnWhyLonLat.Click += new System.EventHandler(this.btnWhyLonLat_Click);
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblLongitude.Location = new System.Drawing.Point(30, 285);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(68, 17);
            this.lblLongitude.TabIndex = 29;
            this.lblLongitude.Text = "Longitude:";
            // 
            // cmbLonDirection
            // 
            this.cmbLonDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLonDirection.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbLonDirection.FormattingEnabled = true;
            this.cmbLonDirection.Items.AddRange(new object[] { "EAST", "WEST" });
            this.cmbLonDirection.Location = new System.Drawing.Point(110, 282);
            this.cmbLonDirection.Name = "cmbLonDirection";
            this.cmbLonDirection.Size = new System.Drawing.Size(85, 25);
            this.cmbLonDirection.TabIndex = 30;
            // 
            // txtLonDegrees
            // 
            this.txtLonDegrees.Location = new System.Drawing.Point(205, 282);
            this.txtLonDegrees.MaxLength = 3;
            this.txtLonDegrees.Name = "txtLonDegrees";
            this.txtLonDegrees.Size = new System.Drawing.Size(45, 25);
            this.txtLonDegrees.TabIndex = 31;
            this.txtLonDegrees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonDegreesValue_KeyPressed);
            // 
            // lblSepChar5
            // 
            this.lblSepChar5.AutoSize = true;
            this.lblSepChar5.Location = new System.Drawing.Point(253, 285);
            this.lblSepChar5.Name = "lblSepChar5";
            this.lblSepChar5.Size = new System.Drawing.Size(13, 17);
            this.lblSepChar5.TabIndex = 32;
            this.lblSepChar5.Text = "--";
            // 
            // txtLonMinutes
            // 
            this.txtLonMinutes.Location = new System.Drawing.Point(270, 282);
            this.txtLonMinutes.MaxLength = 2;
            this.txtLonMinutes.Name = "txtLonMinutes";
            this.txtLonMinutes.Size = new System.Drawing.Size(45, 25);
            this.txtLonMinutes.TabIndex = 33;
            this.txtLonMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LonMinutesValue_KeyPressed);
            // 
            // lblLonFormat
            // 
            this.lblLonFormat.AutoSize = true;
            this.lblLonFormat.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblLonFormat.ForeColor = System.Drawing.Color.Gray;
            this.lblLonFormat.Location = new System.Drawing.Point(321, 285);
            this.lblLonFormat.Name = "lblLonFormat";
            this.lblLonFormat.Size = new System.Drawing.Size(95, 13);
            this.lblLonFormat.TabIndex = 34;
            this.lblLonFormat.Text = "(000 Deg - 00 Min)";
            // 
            // lblLatitude
            // 
            this.lblLatitude.AutoSize = true;
            this.lblLatitude.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblLatitude.Location = new System.Drawing.Point(30, 323);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(60, 17);
            this.lblLatitude.TabIndex = 35;
            this.lblLatitude.Text = "Latitude:";
            // 
            // cmbLatDirection
            // 
            this.cmbLatDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLatDirection.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbLatDirection.FormattingEnabled = true;
            this.cmbLatDirection.Items.AddRange(new object[] { "NORTH", "SOUTH" });
            this.cmbLatDirection.Location = new System.Drawing.Point(110, 320);
            this.cmbLatDirection.Name = "cmbLatDirection";
            this.cmbLatDirection.Size = new System.Drawing.Size(85, 25);
            this.cmbLatDirection.TabIndex = 36;
            // 
            // txtLatDegrees
            // 
            this.txtLatDegrees.Location = new System.Drawing.Point(205, 320);
            this.txtLatDegrees.MaxLength = 2;
            this.txtLatDegrees.Name = "txtLatDegrees";
            this.txtLatDegrees.Size = new System.Drawing.Size(45, 25);
            this.txtLatDegrees.TabIndex = 37;
            this.txtLatDegrees.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatDegreesValue_KeyPressed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 323);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 17);
            this.label2.TabIndex = 38;
            this.label2.Text = "--";
            // 
            // txtLatMinutes
            // 
            this.txtLatMinutes.Location = new System.Drawing.Point(270, 320);
            this.txtLatMinutes.MaxLength = 2;
            this.txtLatMinutes.Name = "txtLatMinutes";
            this.txtLatMinutes.Size = new System.Drawing.Size(45, 25);
            this.txtLatMinutes.TabIndex = 39;
            this.txtLatMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LatMinutesValue_KeyPressed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(321, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "(00 Deg - 00 Min)";
            // 
            // lblAyanamsa
            // 
            this.lblAyanamsa.AutoSize = true;
            this.lblAyanamsa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblAyanamsa.Location = new System.Drawing.Point(30, 361);
            this.lblAyanamsa.Name = "lblAyanamsa";
            this.lblAyanamsa.Size = new System.Drawing.Size(67, 17);
            this.lblAyanamsa.TabIndex = 41;
            this.lblAyanamsa.Text = "Ayanamsa:";
            // 
            // cmbAyanamsa
            // 
            this.cmbAyanamsa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAyanamsa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbAyanamsa.FormattingEnabled = true;
            this.cmbAyanamsa.Items.AddRange(new object[] {
            "KP-SE-NewComb",
            "Lahiri",
            "Suryasiddhanta",
            "True Pushya (PVRN Rao)",
            "True Mula (Chandra Hari)",
            "Fagan/Bradley",
            "Tropical"});
            this.cmbAyanamsa.Location = new System.Drawing.Point(110, 358);
            this.cmbAyanamsa.Name = "cmbAyanamsa";
            this.cmbAyanamsa.Size = new System.Drawing.Size(220, 25);
            this.cmbAyanamsa.TabIndex = 42;
            // 
            // Bottom action buttons
            // 
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOpen.Location = new System.Drawing.Point(30, 420);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(80, 35);
            this.btnOpen.TabIndex = 43;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(120, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            this.btnNewChart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnNewChart.Location = new System.Drawing.Point(210, 420);
            this.btnNewChart.Name = "btnNewChart";
            this.btnNewChart.Size = new System.Drawing.Size(90, 35);
            this.btnNewChart.TabIndex = 45;
            this.btnNewChart.Text = "New Chart";
            this.btnNewChart.UseVisualStyleBackColor = true;
            this.btnNewChart.Click += new System.EventHandler(this.btnNewChart_Click);
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(340, 420);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(120, 35);
            this.btnGenerate.TabIndex = 46;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            this.btnPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPDF.ForeColor = System.Drawing.Color.White;
            this.btnPDF.Location = new System.Drawing.Point(480, 420);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(90, 35);
            this.btnPDF.TabIndex = 47;
            this.btnPDF.Text = "PDF";
            this.btnPDF.UseVisualStyleBackColor = false;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // BirthDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 480);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.lblDOB);
            this.Controls.Add(this.txtDD);
            this.Controls.Add(this.lblSepChar1);
            this.Controls.Add(this.txtMM);
            this.Controls.Add(this.lblSepChar2);
            this.Controls.Add(this.txtYYYY);
            this.Controls.Add(this.lblDateFormat);
            this.Controls.Add(this.lblTOB);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.lblSepChar3);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.lblSepChar4);
            this.Controls.Add(this.txtSec);
            this.Controls.Add(this.lblTimeFormat);
            this.Controls.Add(this.btnNow);
            this.Controls.Add(this.lblTimeZone);
            this.Controls.Add(this.cmbTimeZone);
            this.Controls.Add(this.lblPOB);
            this.Controls.Add(this.txtPOB);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.txtSOB);
            this.Controls.Add(this.labelCountry);
            this.Controls.Add(this.txtCOB);
            this.Controls.Add(this.lblLonLatWarning);
            this.Controls.Add(this.btnWhyLonLat);
            this.Controls.Add(this.lblLongitude);
            this.Controls.Add(this.cmbLonDirection);
            this.Controls.Add(this.txtLonDegrees);
            this.Controls.Add(this.lblSepChar5);
            this.Controls.Add(this.txtLonMinutes);
            this.Controls.Add(this.lblLonFormat);
            this.Controls.Add(this.lblLatitude);
            this.Controls.Add(this.cmbLatDirection);
            this.Controls.Add(this.txtLatDegrees);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLatMinutes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAyanamsa);
            this.Controls.Add(this.cmbAyanamsa);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNewChart);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnPDF);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BirthDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Birth Data";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.TextBox txtDD;
        private System.Windows.Forms.Label lblSepChar1;
        private System.Windows.Forms.TextBox txtMM;
        private System.Windows.Forms.Label lblSepChar2;
        private System.Windows.Forms.TextBox txtYYYY;
        private System.Windows.Forms.Label lblDateFormat;
        private System.Windows.Forms.Label lblTOB;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label lblSepChar3;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Label lblSepChar4;
        private System.Windows.Forms.TextBox txtSec;
        private System.Windows.Forms.Label lblTimeFormat;
        private System.Windows.Forms.Label lblTimeZone;
        private System.Windows.Forms.ComboBox cmbTimeZone;
        private System.Windows.Forms.Label lblPOB;
        private System.Windows.Forms.TextBox txtPOB;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtSOB;
        private System.Windows.Forms.Label labelCountry;
        private System.Windows.Forms.TextBox txtCOB;
        private System.Windows.Forms.Label lblLonLatWarning;
        private System.Windows.Forms.Button btnWhyLonLat;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.ComboBox cmbLonDirection;
        private System.Windows.Forms.TextBox txtLonDegrees;
        private System.Windows.Forms.Label lblSepChar5;
        private System.Windows.Forms.TextBox txtLonMinutes;
        private System.Windows.Forms.Label lblLonFormat;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.ComboBox cmbLatDirection;
        private System.Windows.Forms.TextBox txtLatDegrees;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLatMinutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAyanamsa;
        private System.Windows.Forms.ComboBox cmbAyanamsa;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNewChart;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Button btnNow;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
