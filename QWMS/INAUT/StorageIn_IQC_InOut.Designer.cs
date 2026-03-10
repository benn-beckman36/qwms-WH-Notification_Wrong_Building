namespace QWMS
{
    partial class StorageIn_IQC_InOut
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
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.lblLocat = new System.Windows.Forms.Label();
            this.lblLgort = new System.Windows.Forms.Label();
            this.lblWerks = new System.Windows.Forms.Label();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtUsr = new System.Windows.Forms.TextBox();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rdoQuery = new System.Windows.Forms.RadioButton();
            this.rdoGI = new System.Windows.Forms.RadioButton();
            this.rdoGR = new System.Windows.Forms.RadioButton();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbHeaderTo = new System.Windows.Forms.GroupBox();
            this.txtUsrTo = new System.Windows.Forms.TextBox();
            this.txtLocatTo = new System.Windows.Forms.TextBox();
            this.cmbLgortTo = new System.Windows.Forms.ComboBox();
            this.cmbWerksTo = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtMatnrSelect = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbHeaderQuery = new System.Windows.Forms.GroupBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.txtEndMatnr = new System.Windows.Forms.TextBox();
            this.lblToPartNo = new System.Windows.Forms.Label();
            this.txtUsrQry = new System.Windows.Forms.TextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtStartMatnr = new System.Windows.Forms.TextBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.lblQctyp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cmbQCtyp = new System.Windows.Forms.ComboBox();
            this.lblMatnr = new System.Windows.Forms.Label();
            this.lblUsr = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.gbHeader.SuspendLayout();
            this.gbFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbHeaderTo.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbHeaderQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 430);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(738, 22);
            this.stbStatus.TabIndex = 34;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // lblLocat
            // 
            this.lblLocat.AutoSize = true;
            this.lblLocat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLocat.Location = new System.Drawing.Point(153, 93);
            this.lblLocat.Name = "lblLocat";
            this.lblLocat.Size = new System.Drawing.Size(63, 16);
            this.lblLocat.TabIndex = 39;
            this.lblLocat.Text = "Location";
            this.lblLocat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLgort
            // 
            this.lblLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLgort.Location = new System.Drawing.Point(159, 64);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(57, 23);
            this.lblLgort.TabIndex = 35;
            this.lblLgort.Text = "Storage";
            this.lblLgort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWerks
            // 
            this.lblWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblWerks.Location = new System.Drawing.Point(166, 32);
            this.lblWerks.Name = "lblWerks";
            this.lblWerks.Size = new System.Drawing.Size(47, 23);
            this.lblWerks.TabIndex = 34;
            this.lblWerks.Text = "Plant";
            this.lblWerks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.txtUsr);
            this.gbHeader.Controls.Add(this.txtLocat);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Enabled = false;
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(220, 9);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(105, 141);
            this.gbHeader.TabIndex = 30;
            this.gbHeader.TabStop = false;
            this.gbHeader.Text = "From";
            // 
            // txtUsr
            // 
            this.txtUsr.Enabled = false;
            this.txtUsr.Location = new System.Drawing.Point(12, 112);
            this.txtUsr.Name = "txtUsr";
            this.txtUsr.Size = new System.Drawing.Size(84, 22);
            this.txtUsr.TabIndex = 26;
            this.txtUsr.DoubleClick += new System.EventHandler(this.txtUsr_DoubleClick);
            // 
            // txtLocat
            // 
            this.txtLocat.Location = new System.Drawing.Point(11, 84);
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(85, 22);
            this.txtLocat.TabIndex = 38;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            this.txtLocat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.ItemHeight = 16;
            this.cmbLgort.Location = new System.Drawing.Point(11, 54);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(85, 24);
            this.cmbLgort.TabIndex = 37;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Enabled = false;
            this.cmbWerks.Location = new System.Drawing.Point(12, 24);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(84, 24);
            this.cmbWerks.TabIndex = 36;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.lblInfo2);
            this.gbFunction.Controls.Add(this.lblInfo);
            this.gbFunction.Controls.Add(this.rdoQuery);
            this.gbFunction.Controls.Add(this.rdoGI);
            this.gbFunction.Controls.Add(this.rdoGR);
            this.gbFunction.Font = new System.Drawing.Font("新細明體", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gbFunction.Location = new System.Drawing.Point(8, 5);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(146, 144);
            this.gbFunction.TabIndex = 33;
            this.gbFunction.TabStop = false;
            // 
            // lblInfo2
            // 
            this.lblInfo2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblInfo2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblInfo2.Location = new System.Drawing.Point(14, 83);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(89, 23);
            this.lblInfo2.TabIndex = 64;
            this.lblInfo2.Text = "(IQC→W/H)";
            this.lblInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblInfo.Location = new System.Drawing.Point(15, 32);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(89, 23);
            this.lblInfo.TabIndex = 63;
            this.lblInfo.Text = "(W/H→IQC)";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdoQuery
            // 
            this.rdoQuery.AutoSize = true;
            this.rdoQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoQuery.Location = new System.Drawing.Point(11, 116);
            this.rdoQuery.Name = "rdoQuery";
            this.rdoQuery.Size = new System.Drawing.Size(127, 20);
            this.rdoQuery.TabIndex = 43;
            this.rdoQuery.Text = "Inventory Query";
            this.rdoQuery.UseVisualStyleBackColor = true;
            this.rdoQuery.CheckedChanged += new System.EventHandler(this.rdoQuery_CheckedChanged);
            // 
            // rdoGI
            // 
            this.rdoGI.AutoSize = true;
            this.rdoGI.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoGI.Location = new System.Drawing.Point(10, 15);
            this.rdoGI.Name = "rdoGI";
            this.rdoGI.Size = new System.Drawing.Size(102, 20);
            this.rdoGI.TabIndex = 31;
            this.rdoGI.Text = "Goods Issue";
            this.rdoGI.UseVisualStyleBackColor = true;
            this.rdoGI.CheckedChanged += new System.EventHandler(this.rdoReturn_CheckedChanged);
            // 
            // rdoGR
            // 
            this.rdoGR.AutoSize = true;
            this.rdoGR.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoGR.Location = new System.Drawing.Point(10, 65);
            this.rdoGR.Name = "rdoGR";
            this.rdoGR.Size = new System.Drawing.Size(118, 20);
            this.rdoGR.TabIndex = 30;
            this.rdoGR.Text = "Goods Receipt";
            this.rdoGR.UseVisualStyleBackColor = true;
            this.rdoGR.CheckedChanged += new System.EventHandler(this.rdoReq_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnQuery.Location = new System.Drawing.Point(621, 122);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(91, 34);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(16, 164);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(50, 14);
            this.lblCount.TabIndex = 38;
            this.lblCount.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(16, 180);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(710, 200);
            this.dgvData.TabIndex = 39;
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(122, 389);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 38);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(226, 389);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 38);
            this.btnRefresh.TabIndex = 41;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(330, 389);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 38);
            this.btnExit.TabIndex = 42;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gbHeaderTo
            // 
            this.gbHeaderTo.Controls.Add(this.txtUsrTo);
            this.gbHeaderTo.Controls.Add(this.txtLocatTo);
            this.gbHeaderTo.Controls.Add(this.cmbLgortTo);
            this.gbHeaderTo.Controls.Add(this.cmbWerksTo);
            this.gbHeaderTo.Enabled = false;
            this.gbHeaderTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeaderTo.Location = new System.Drawing.Point(331, 9);
            this.gbHeaderTo.Name = "gbHeaderTo";
            this.gbHeaderTo.Size = new System.Drawing.Size(103, 141);
            this.gbHeaderTo.TabIndex = 39;
            this.gbHeaderTo.TabStop = false;
            this.gbHeaderTo.Text = "To";
            // 
            // txtUsrTo
            // 
            this.txtUsrTo.Enabled = false;
            this.txtUsrTo.Location = new System.Drawing.Point(11, 112);
            this.txtUsrTo.Name = "txtUsrTo";
            this.txtUsrTo.Size = new System.Drawing.Size(84, 22);
            this.txtUsrTo.TabIndex = 26;
            // 
            // txtLocatTo
            // 
            this.txtLocatTo.Location = new System.Drawing.Point(10, 84);
            this.txtLocatTo.Name = "txtLocatTo";
            this.txtLocatTo.Size = new System.Drawing.Size(85, 22);
            this.txtLocatTo.TabIndex = 38;
            this.txtLocatTo.DoubleClick += new System.EventHandler(this.txtLocatTo_DoubleClick);
            // 
            // cmbLgortTo
            // 
            this.cmbLgortTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgortTo.ItemHeight = 16;
            this.cmbLgortTo.Location = new System.Drawing.Point(10, 54);
            this.cmbLgortTo.Name = "cmbLgortTo";
            this.cmbLgortTo.Size = new System.Drawing.Size(85, 24);
            this.cmbLgortTo.TabIndex = 37;
            this.cmbLgortTo.SelectedIndexChanged += new System.EventHandler(this.cmbLgortTo_SelectedIndexChanged);
            // 
            // cmbWerksTo
            // 
            this.cmbWerksTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerksTo.Enabled = false;
            this.cmbWerksTo.Location = new System.Drawing.Point(10, 24);
            this.cmbWerksTo.Name = "cmbWerksTo";
            this.cmbWerksTo.Size = new System.Drawing.Size(85, 24);
            this.cmbWerksTo.TabIndex = 36;
            this.cmbWerksTo.SelectedIndexChanged += new System.EventHandler(this.cmbWerksTo_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtMatnrSelect);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.gbHeaderQuery);
            this.panel4.Controls.Add(this.lblUsr);
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Controls.Add(this.btnReport);
            this.panel4.Controls.Add(this.gbHeaderTo);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.btnRefresh);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.dgvData);
            this.panel4.Controls.Add(this.lblCount);
            this.panel4.Controls.Add(this.btnQuery);
            this.panel4.Controls.Add(this.gbFunction);
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Controls.Add(this.lblWerks);
            this.panel4.Controls.Add(this.lblLgort);
            this.panel4.Controls.Add(this.lblLocat);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(738, 430);
            this.panel4.TabIndex = 33;
            // 
            // txtMatnrSelect
            // 
            this.txtMatnrSelect.Location = new System.Drawing.Point(219, 154);
            this.txtMatnrSelect.Name = "txtMatnrSelect";
            this.txtMatnrSelect.Size = new System.Drawing.Size(97, 22);
            this.txtMatnrSelect.TabIndex = 70;
            this.txtMatnrSelect.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatnrSelect_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(157, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "Part No.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbHeaderQuery
            // 
            this.gbHeaderQuery.Controls.Add(this.chkDate);
            this.gbHeaderQuery.Controls.Add(this.txtEndMatnr);
            this.gbHeaderQuery.Controls.Add(this.lblToPartNo);
            this.gbHeaderQuery.Controls.Add(this.txtUsrQry);
            this.gbHeaderQuery.Controls.Add(this.dtpEndDate);
            this.gbHeaderQuery.Controls.Add(this.dtpStartDate);
            this.gbHeaderQuery.Controls.Add(this.txtStartMatnr);
            this.gbHeaderQuery.Controls.Add(this.lblDateTo);
            this.gbHeaderQuery.Controls.Add(this.lblQctyp);
            this.gbHeaderQuery.Controls.Add(this.label1);
            this.gbHeaderQuery.Controls.Add(this.lblDate);
            this.gbHeaderQuery.Controls.Add(this.cmbQCtyp);
            this.gbHeaderQuery.Controls.Add(this.lblMatnr);
            this.gbHeaderQuery.Enabled = false;
            this.gbHeaderQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeaderQuery.Location = new System.Drawing.Point(440, 9);
            this.gbHeaderQuery.Name = "gbHeaderQuery";
            this.gbHeaderQuery.Size = new System.Drawing.Size(286, 108);
            this.gbHeaderQuery.TabIndex = 40;
            this.gbHeaderQuery.TabStop = false;
            this.gbHeaderQuery.Text = "Query";
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(5, 52);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(15, 14);
            this.chkDate.TabIndex = 70;
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Location = new System.Drawing.Point(178, 19);
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(99, 22);
            this.txtEndMatnr.TabIndex = 68;
            // 
            // lblToPartNo
            // 
            this.lblToPartNo.AutoSize = true;
            this.lblToPartNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblToPartNo.Location = new System.Drawing.Point(153, 21);
            this.lblToPartNo.Name = "lblToPartNo";
            this.lblToPartNo.Size = new System.Drawing.Size(23, 16);
            this.lblToPartNo.TabIndex = 65;
            this.lblToPartNo.Text = "To";
            this.lblToPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsrQry
            // 
            this.txtUsrQry.Enabled = false;
            this.txtUsrQry.Location = new System.Drawing.Point(60, 76);
            this.txtUsrQry.Name = "txtUsrQry";
            this.txtUsrQry.Size = new System.Drawing.Size(80, 22);
            this.txtUsrQry.TabIndex = 39;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(179, 46);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(98, 22);
            this.dtpEndDate.TabIndex = 64;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Enabled = false;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(59, 47);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(89, 22);
            this.dtpStartDate.TabIndex = 62;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.Location = new System.Drawing.Point(59, 19);
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(89, 22);
            this.txtStartMatnr.TabIndex = 39;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDateTo.Location = new System.Drawing.Point(154, 50);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(23, 16);
            this.lblDateTo.TabIndex = 66;
            this.lblDateTo.Text = "To";
            this.lblDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblQctyp
            // 
            this.lblQctyp.AutoSize = true;
            this.lblQctyp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblQctyp.Location = new System.Drawing.Point(139, 78);
            this.lblQctyp.Name = "lblQctyp";
            this.lblQctyp.Size = new System.Drawing.Size(38, 16);
            this.lblQctyp.TabIndex = 61;
            this.lblQctyp.Text = "Type";
            this.lblQctyp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(7, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "User ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDate.Location = new System.Drawing.Point(20, 49);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 16);
            this.lblDate.TabIndex = 67;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbQCtyp
            // 
            this.cmbQCtyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQCtyp.Enabled = false;
            this.cmbQCtyp.Location = new System.Drawing.Point(181, 74);
            this.cmbQCtyp.Name = "cmbQCtyp";
            this.cmbQCtyp.Size = new System.Drawing.Size(96, 24);
            this.cmbQCtyp.TabIndex = 39;
            // 
            // lblMatnr
            // 
            this.lblMatnr.AutoSize = true;
            this.lblMatnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMatnr.Location = new System.Drawing.Point(1, 22);
            this.lblMatnr.Name = "lblMatnr";
            this.lblMatnr.Size = new System.Drawing.Size(59, 16);
            this.lblMatnr.TabIndex = 59;
            this.lblMatnr.Text = "Part No.";
            this.lblMatnr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUsr
            // 
            this.lblUsr.AutoSize = true;
            this.lblUsr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUsr.Location = new System.Drawing.Point(160, 122);
            this.lblUsr.Name = "lblUsr";
            this.lblUsr.Size = new System.Drawing.Size(53, 16);
            this.lblUsr.TabIndex = 63;
            this.lblUsr.Text = "User ID";
            this.lblUsr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(19, 389);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 38);
            this.btnAdd.TabIndex = 60;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(434, 389);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(97, 38);
            this.btnReport.TabIndex = 58;
            this.btnReport.Text = "Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "StorageIn_IQC_InOut.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // StorageIn_IQC_InOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 452);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.stbStatus);
            this.Name = "StorageIn_IQC_InOut";
            this.Text = "StorageIn_IQC_InOut";
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbHeaderTo.ResumeLayout(false);
            this.gbHeaderTo.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbHeaderQuery.ResumeLayout(false);
            this.gbHeaderQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Label lblLocat;
        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.Label lblWerks;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.TextBox txtUsr;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoGI;
        private System.Windows.Forms.RadioButton rdoGR;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox gbHeaderTo;
        private System.Windows.Forms.TextBox txtUsrTo;
        private System.Windows.Forms.TextBox txtLocatTo;
        private System.Windows.Forms.ComboBox cmbLgortTo;
        private System.Windows.Forms.ComboBox cmbWerksTo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdoQuery;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblMatnr;
        private System.Windows.Forms.TextBox txtStartMatnr;
        private System.Windows.Forms.ComboBox cmbQCtyp;
        private System.Windows.Forms.Label lblQctyp;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblUsr;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label lblToPartNo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.TextBox txtUsrQry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEndMatnr;
        private System.Windows.Forms.GroupBox gbHeaderQuery;
        private System.Windows.Forms.TextBox txtMatnrSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDate;


    }
}