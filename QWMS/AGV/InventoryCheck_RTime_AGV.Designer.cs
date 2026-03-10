namespace QWMS.AGV
{
    partial class InventoryCheck_RTime_AGV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnFlip = new System.Windows.Forms.Button();
            this.btnCallAGV = new System.Windows.Forms.Button();
            this.lblCount2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtCurrentShelf = new System.Windows.Forms.TextBox();
            this.lbShelfNo = new System.Windows.Forms.Label();
            this.cmbWorkStation = new System.Windows.Forms.ComboBox();
            this.lbWorkStation = new System.Windows.Forms.Label();
            this.rdoSN = new System.Windows.Forms.RadioButton();
            this.rdoBoxid = new System.Windows.Forms.RadioButton();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblitm = new System.Windows.Forms.Label();
            this.lblQR = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmbInvNo = new System.Windows.Forms.ComboBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStroage = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblLocat = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblPlant = new System.Windows.Forms.Label();
            this.lblInvNo = new System.Windows.Forms.Label();
            this.lblCount1 = new System.Windows.Forms.Label();
            this.dgvError = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 611);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1292, 29);
            this.statusStrip1.TabIndex = 55;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(40, 22);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(40, 22);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 22);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 22);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 22);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnEnd);
            this.panel4.Controls.Add(this.btnFlip);
            this.panel4.Controls.Add(this.btnCallAGV);
            this.panel4.Controls.Add(this.lblCount2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.lblCount1);
            this.panel4.Controls.Add(this.dgvError);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.dgvData);
            this.panel4.Controls.Add(this.btnRefresh);
            this.panel4.Controls.Add(this.statusStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1296, 644);
            this.panel4.TabIndex = 58;
            // 
            // btnEnd
            // 
            this.btnEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnd.Location = new System.Drawing.Point(414, 566);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(130, 36);
            this.btnEnd.TabIndex = 67;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnFlip
            // 
            this.btnFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFlip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnFlip.Location = new System.Drawing.Point(247, 566);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(130, 36);
            this.btnFlip.TabIndex = 66;
            this.btnFlip.Text = "翻面";
            this.btnFlip.UseVisualStyleBackColor = true;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // btnCallAGV
            // 
            this.btnCallAGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCallAGV.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnCallAGV.Location = new System.Drawing.Point(80, 566);
            this.btnCallAGV.Name = "btnCallAGV";
            this.btnCallAGV.Size = new System.Drawing.Size(130, 36);
            this.btnCallAGV.TabIndex = 65;
            this.btnCallAGV.Text = "呼叫小车";
            this.btnCallAGV.UseVisualStyleBackColor = true;
            this.btnCallAGV.Click += new System.EventHandler(this.btnCallAGV_Click);
            // 
            // lblCount2
            // 
            this.lblCount2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount2.AutoSize = true;
            this.lblCount2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount2.Location = new System.Drawing.Point(23, 346);
            this.lblCount2.Name = "lblCount2";
            this.lblCount2.Size = new System.Drawing.Size(100, 23);
            this.lblCount2.TabIndex = 64;
            this.lblCount2.Text = "0 Records";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1292, 134);
            this.panel1.TabIndex = 54;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtCurrentShelf);
            this.splitContainer1.Panel1.Controls.Add(this.lbShelfNo);
            this.splitContainer1.Panel1.Controls.Add(this.cmbWorkStation);
            this.splitContainer1.Panel1.Controls.Add(this.lbWorkStation);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rdoSN);
            this.splitContainer1.Panel2.Controls.Add(this.rdoBoxid);
            this.splitContainer1.Panel2.Controls.Add(this.txtCode);
            this.splitContainer1.Panel2.Controls.Add(this.lblitm);
            this.splitContainer1.Panel2.Controls.Add(this.lblQR);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.txtQty);
            this.splitContainer1.Panel2.Controls.Add(this.cmbInvNo);
            this.splitContainer1.Panel2.Controls.Add(this.lblQty);
            this.splitContainer1.Panel2.Controls.Add(this.cmbLgort);
            this.splitContainer1.Panel2.Controls.Add(this.dtpStartDate);
            this.splitContainer1.Panel2.Controls.Add(this.lblStroage);
            this.splitContainer1.Panel2.Controls.Add(this.txtLocation);
            this.splitContainer1.Panel2.Controls.Add(this.lblDate);
            this.splitContainer1.Panel2.Controls.Add(this.lblLocat);
            this.splitContainer1.Panel2.Controls.Add(this.cmbWerks);
            this.splitContainer1.Panel2.Controls.Add(this.dtpEndDate);
            this.splitContainer1.Panel2.Controls.Add(this.lblPlant);
            this.splitContainer1.Panel2.Controls.Add(this.lblInvNo);
            this.splitContainer1.Size = new System.Drawing.Size(1292, 134);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 59;
            // 
            // txtCurrentShelf
            // 
            this.txtCurrentShelf.Location = new System.Drawing.Point(118, 77);
            this.txtCurrentShelf.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtCurrentShelf.Name = "txtCurrentShelf";
            this.txtCurrentShelf.Size = new System.Drawing.Size(138, 28);
            this.txtCurrentShelf.TabIndex = 41;
            // 
            // lbShelfNo
            // 
            this.lbShelfNo.AutoSize = true;
            this.lbShelfNo.Location = new System.Drawing.Point(42, 80);
            this.lbShelfNo.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbShelfNo.Name = "lbShelfNo";
            this.lbShelfNo.Size = new System.Drawing.Size(46, 21);
            this.lbShelfNo.TabIndex = 40;
            this.lbShelfNo.Text = "料架";
            this.lbShelfNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbWorkStation
            // 
            this.cmbWorkStation.FormattingEnabled = true;
            this.cmbWorkStation.Location = new System.Drawing.Point(118, 25);
            this.cmbWorkStation.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbWorkStation.Name = "cmbWorkStation";
            this.cmbWorkStation.Size = new System.Drawing.Size(138, 29);
            this.cmbWorkStation.TabIndex = 38;
            this.cmbWorkStation.SelectedIndexChanged += new System.EventHandler(this.cmbWorkStation_SelectedIndexChanged);
            // 
            // lbWorkStation
            // 
            this.lbWorkStation.AutoSize = true;
            this.lbWorkStation.Location = new System.Drawing.Point(33, 28);
            this.lbWorkStation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbWorkStation.Name = "lbWorkStation";
            this.lbWorkStation.Size = new System.Drawing.Size(64, 21);
            this.lbWorkStation.TabIndex = 37;
            this.lbWorkStation.Text = "工作站";
            this.lbWorkStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdoSN
            // 
            this.rdoSN.AutoSize = true;
            this.rdoSN.Enabled = false;
            this.rdoSN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.rdoSN.Location = new System.Drawing.Point(922, 54);
            this.rdoSN.Name = "rdoSN";
            this.rdoSN.Size = new System.Drawing.Size(60, 25);
            this.rdoSN.TabIndex = 77;
            this.rdoSN.TabStop = true;
            this.rdoSN.Text = "SN";
            this.rdoSN.UseVisualStyleBackColor = true;
            this.rdoSN.Visible = false;
            // 
            // rdoBoxid
            // 
            this.rdoBoxid.AutoSize = true;
            this.rdoBoxid.Enabled = false;
            this.rdoBoxid.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.rdoBoxid.Location = new System.Drawing.Point(828, 53);
            this.rdoBoxid.Name = "rdoBoxid";
            this.rdoBoxid.Size = new System.Drawing.Size(88, 25);
            this.rdoBoxid.TabIndex = 76;
            this.rdoBoxid.TabStop = true;
            this.rdoBoxid.Text = "BoxID";
            this.rdoBoxid.UseVisualStyleBackColor = true;
            this.rdoBoxid.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtCode.Location = new System.Drawing.Point(667, 52);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(155, 28);
            this.txtCode.TabIndex = 75;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // lblitm
            // 
            this.lblitm.AutoSize = true;
            this.lblitm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblitm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblitm.Location = new System.Drawing.Point(379, 103);
            this.lblitm.Name = "lblitm";
            this.lblitm.Size = new System.Drawing.Size(73, 23);
            this.lblitm.TabIndex = 71;
            this.lblitm.Text = "总笔数:";
            this.lblitm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQR
            // 
            this.lblQR.AutoSize = true;
            this.lblQR.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblQR.Location = new System.Drawing.Point(578, 55);
            this.lblQR.Name = "lblQR";
            this.lblQR.Size = new System.Drawing.Size(67, 21);
            this.lblQR.TabIndex = 74;
            this.lblQR.Text = "识别码:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(277, 73);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 21);
            this.label12.TabIndex = 69;
            this.label12.Text = "-";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(667, 88);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(113, 28);
            this.txtQty.TabIndex = 73;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // cmbInvNo
            // 
            this.cmbInvNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbInvNo.FormattingEnabled = true;
            this.cmbInvNo.Location = new System.Drawing.Point(141, 101);
            this.cmbInvNo.Name = "cmbInvNo";
            this.cmbInvNo.Size = new System.Drawing.Size(232, 29);
            this.cmbInvNo.TabIndex = 66;
            this.cmbInvNo.SelectedIndexChanged += new System.EventHandler(this.cmbInvNo_SelectedIndexChanged);
            this.cmbInvNo.Click += new System.EventHandler(this.cmbInvNo_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblQty.Location = new System.Drawing.Point(592, 91);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(53, 21);
            this.lblQty.TabIndex = 72;
            this.lblQty.Text = "数量:";
            // 
            // cmbLgort
            // 
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(141, 38);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(113, 29);
            this.cmbLgort.TabIndex = 63;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "";
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(141, 70);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(132, 28);
            this.dtpStartDate.TabIndex = 68;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStroage
            // 
            this.lblStroage.AutoSize = true;
            this.lblStroage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblStroage.Location = new System.Drawing.Point(73, 41);
            this.lblStroage.Name = "lblStroage";
            this.lblStroage.Size = new System.Drawing.Size(49, 21);
            this.lblStroage.TabIndex = 62;
            this.lblStroage.Text = "仓别:";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtLocation.Location = new System.Drawing.Point(667, 19);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(113, 28);
            this.txtLocation.TabIndex = 65;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblDate.Location = new System.Drawing.Point(66, 76);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(56, 21);
            this.lblDate.TabIndex = 67;
            this.lblDate.Text = "日期：";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocat
            // 
            this.lblLocat.AutoSize = true;
            this.lblLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocat.Location = new System.Drawing.Point(592, 22);
            this.lblLocat.Name = "lblLocat";
            this.lblLocat.Size = new System.Drawing.Size(53, 21);
            this.lblLocat.TabIndex = 64;
            this.lblLocat.Text = "储位:";
            // 
            // cmbWerks
            // 
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(141, 6);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(113, 29);
            this.cmbWerks.TabIndex = 61;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "";
            this.dtpEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(298, 70);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(127, 28);
            this.dtpEndDate.TabIndex = 70;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlant.Location = new System.Drawing.Point(66, 9);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(56, 21);
            this.lblPlant.TabIndex = 60;
            this.lblPlant.Text = "厂区：";
            // 
            // lblInvNo
            // 
            this.lblInvNo.AutoSize = true;
            this.lblInvNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvNo.Location = new System.Drawing.Point(30, 105);
            this.lblInvNo.Name = "lblInvNo";
            this.lblInvNo.Size = new System.Drawing.Size(92, 21);
            this.lblInvNo.TabIndex = 59;
            this.lblInvNo.Text = "盘点票号：";
            // 
            // lblCount1
            // 
            this.lblCount1.AutoSize = true;
            this.lblCount1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount1.Location = new System.Drawing.Point(23, 149);
            this.lblCount1.Name = "lblCount1";
            this.lblCount1.Size = new System.Drawing.Size(100, 23);
            this.lblCount1.TabIndex = 63;
            this.lblCount1.Text = "0 Records";
            // 
            // dgvError
            // 
            this.dgvError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvError.ColumnHeadersHeight = 29;
            this.dgvError.Location = new System.Drawing.Point(23, 175);
            this.dgvError.Name = "dgvError";
            this.dgvError.RowHeadersWidth = 51;
            this.dgvError.RowTemplate.Height = 30;
            this.dgvError.Size = new System.Drawing.Size(1249, 168);
            this.dgvError.TabIndex = 62;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(581, 566);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 36);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeight = 29;
            this.dgvData.Location = new System.Drawing.Point(23, 372);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 30;
            this.dgvData.Size = new System.Drawing.Size(1249, 188);
            this.dgvData.TabIndex = 54;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(740, 566);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(131, 36);
            this.btnRefresh.TabIndex = 58;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // InventoryCheck_RTime_AGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 644);
            this.Controls.Add(this.panel4);
            this.Name = "InventoryCheck_RTime_AGV";
            this.Text = "InventoryCheck_RTime_AGV";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton rdoSN;
        private System.Windows.Forms.RadioButton rdoBoxid;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblitm;
        private System.Windows.Forms.Label lblQR;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ComboBox cmbInvNo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStroage;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLocat;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.Label lblInvNo;
        private System.Windows.Forms.Label lbWorkStation;
        private System.Windows.Forms.ComboBox cmbWorkStation;
        private System.Windows.Forms.Label lbShelfNo;
        private System.Windows.Forms.TextBox txtCurrentShelf;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Button btnCallAGV;
        private System.Windows.Forms.Label lblCount2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCount1;
        private System.Windows.Forms.DataGridView dgvError;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnRefresh;
    }
}