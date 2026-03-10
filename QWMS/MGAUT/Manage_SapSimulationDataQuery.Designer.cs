namespace QWMS
{
    partial class Manage_SapSimulationDataQuery
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
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.gbDoc = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkStock = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbTimeTo = new System.Windows.Forms.ComboBox();
            this.cmbTimeFrom = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnQueryDoc = new System.Windows.Forms.Button();
            this.dtpDocCrdat = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.sfdSaveFile1 = new System.Windows.Forms.SaveFileDialog();
            this.chkShortage = new System.Windows.Forms.CheckBox();
            this.cmbGrpid = new System.Windows.Forms.ComboBox();
            this.btnQueryId = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpIdCrdat = new System.Windows.Forms.DateTimePicker();
            this.gbId = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.gbOrderBy = new System.Windows.Forms.GroupBox();
            this.rdoLocat = new System.Windows.Forms.RadioButton();
            this.rdoMatnr = new System.Windows.Forms.RadioButton();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.chkDateCode = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDocDownload = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnDateCode = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.gbId.SuspendLayout();
            this.gbOrderBy.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(34, 266);
            this.lblData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(96, 25);
            this.lblData.TabIndex = 45;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(34, 297);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(950, 326);
            this.dgvData.TabIndex = 44;
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            // 
            // gbDoc
            // 
            this.gbDoc.Controls.Add(this.label10);
            this.gbDoc.Controls.Add(this.checkStock);
            this.gbDoc.Controls.Add(this.label9);
            this.gbDoc.Controls.Add(this.cmbTimeTo);
            this.gbDoc.Controls.Add(this.cmbTimeFrom);
            this.gbDoc.Controls.Add(this.label8);
            this.gbDoc.Controls.Add(this.label7);
            this.gbDoc.Controls.Add(this.btnQueryDoc);
            this.gbDoc.Controls.Add(this.dtpDocCrdat);
            this.gbDoc.Controls.Add(this.label3);
            this.gbDoc.Controls.Add(this.label4);
            this.gbDoc.Controls.Add(this.txtMblnr);
            this.gbDoc.Enabled = false;
            this.gbDoc.Location = new System.Drawing.Point(210, -8);
            this.gbDoc.Margin = new System.Windows.Forms.Padding(4);
            this.gbDoc.Name = "gbDoc";
            this.gbDoc.Padding = new System.Windows.Forms.Padding(4);
            this.gbDoc.Size = new System.Drawing.Size(492, 268);
            this.gbDoc.TabIndex = 43;
            this.gbDoc.TabStop = false;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(69, 105);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 34);
            this.label10.TabIndex = 69;
            this.label10.Text = "From";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkStock
            // 
            this.checkStock.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkStock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkStock.Location = new System.Drawing.Point(333, 147);
            this.checkStock.Margin = new System.Windows.Forms.Padding(4);
            this.checkStock.Name = "checkStock";
            this.checkStock.Size = new System.Drawing.Size(153, 38);
            this.checkStock.TabIndex = 68;
            this.checkStock.Text = "發料不足";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(93, 148);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 34);
            this.label9.TabIndex = 65;
            this.label9.Text = "To";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTimeTo
            // 
            this.cmbTimeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTimeTo.ItemHeight = 24;
            this.cmbTimeTo.Location = new System.Drawing.Point(148, 148);
            this.cmbTimeTo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTimeTo.Name = "cmbTimeTo";
            this.cmbTimeTo.Size = new System.Drawing.Size(156, 32);
            this.cmbTimeTo.TabIndex = 64;
            // 
            // cmbTimeFrom
            // 
            this.cmbTimeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTimeFrom.ItemHeight = 24;
            this.cmbTimeFrom.Items.AddRange(new object[] {
            "00:00",
            "01:00",
            "02:00",
            "03:00",
            "04:00",
            "05:00",
            "06:00",
            "07:00",
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00"});
            this.cmbTimeFrom.Location = new System.Drawing.Point(148, 104);
            this.cmbTimeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTimeFrom.Name = "cmbTimeFrom";
            this.cmbTimeFrom.Size = new System.Drawing.Size(156, 32);
            this.cmbTimeFrom.TabIndex = 63;
            this.cmbTimeFrom.SelectedIndexChanged += new System.EventHandler(this.cmbTimeFrom_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 70);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 34);
            this.label8.TabIndex = 62;
            this.label8.Text = "時間區間";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 34);
            this.label7.TabIndex = 58;
            this.label7.Text = "Create Date";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnQueryDoc
            // 
            this.btnQueryDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQueryDoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueryDoc.Location = new System.Drawing.Point(332, 98);
            this.btnQueryDoc.Margin = new System.Windows.Forms.Padding(4);
            this.btnQueryDoc.Name = "btnQueryDoc";
            this.btnQueryDoc.Size = new System.Drawing.Size(124, 48);
            this.btnQueryDoc.TabIndex = 61;
            this.btnQueryDoc.Text = "Query Doc";
            this.btnQueryDoc.Click += new System.EventHandler(this.btnQueryDoc_Click);
            // 
            // dtpDocCrdat
            // 
            this.dtpDocCrdat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpDocCrdat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocCrdat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDocCrdat.Location = new System.Drawing.Point(148, 27);
            this.dtpDocCrdat.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDocCrdat.Name = "dtpDocCrdat";
            this.dtpDocCrdat.Size = new System.Drawing.Size(156, 30);
            this.dtpDocCrdat.TabIndex = 57;
            this.dtpDocCrdat.ValueChanged += new System.EventHandler(this.dtpDocCrdat_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(144, 226);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 34);
            this.label3.TabIndex = 27;
            this.label3.Text = "(輸入*可查詢所有的虛擬單據)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 188);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 34);
            this.label4.TabIndex = 19;
            this.label4.Text = "Document";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMblnr
            // 
            this.txtMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMblnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMblnr.Location = new System.Drawing.Point(148, 189);
            this.txtMblnr.Margin = new System.Windows.Forms.Padding(4);
            this.txtMblnr.MaxLength = 100;
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(306, 30);
            this.txtMblnr.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 147);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 34);
            this.label6.TabIndex = 55;
            this.label6.Text = "SMT/FINAL";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.ItemHeight = 24;
            this.cmbType.Items.AddRange(new object[] {
            "SMT",
            "FINAL"});
            this.cmbType.Location = new System.Drawing.Point(34, 186);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(169, 32);
            this.cmbType.TabIndex = 54;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 34);
            this.label1.TabIndex = 20;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWerks.ItemHeight = 24;
            this.cmbWerks.Location = new System.Drawing.Point(88, 32);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(115, 32);
            this.cmbWerks.TabIndex = 18;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 731);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1052, 33);
            this.stbStatus.TabIndex = 46;
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
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "QueryGroupIdData.xls";
            this.sfdSaveFile.Filter = "All Files (*.*)|*.*";
            // 
            // sfdSaveFile1
            // 
            this.sfdSaveFile1.FileName = "QueryDocumentData.xls";
            this.sfdSaveFile1.Filter = "All Files (*.*)|*.*";
            // 
            // chkShortage
            // 
            this.chkShortage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkShortage.Enabled = false;
            this.chkShortage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShortage.Location = new System.Drawing.Point(212, 134);
            this.chkShortage.Margin = new System.Windows.Forms.Padding(4);
            this.chkShortage.Name = "chkShortage";
            this.chkShortage.Size = new System.Drawing.Size(104, 36);
            this.chkShortage.TabIndex = 67;
            this.chkShortage.Text = "缺料";
            // 
            // cmbGrpid
            // 
            this.cmbGrpid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrpid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGrpid.ItemHeight = 24;
            this.cmbGrpid.Location = new System.Drawing.Point(78, 87);
            this.cmbGrpid.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGrpid.Name = "cmbGrpid";
            this.cmbGrpid.Size = new System.Drawing.Size(247, 32);
            this.cmbGrpid.TabIndex = 66;
            this.cmbGrpid.SelectedValueChanged += new System.EventHandler(this.cmbGrpid_SelectedIndexChanged);
            // 
            // btnQueryId
            // 
            this.btnQueryId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQueryId.Enabled = false;
            this.btnQueryId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueryId.Location = new System.Drawing.Point(78, 134);
            this.btnQueryId.Margin = new System.Windows.Forms.Padding(4);
            this.btnQueryId.Name = "btnQueryId";
            this.btnQueryId.Size = new System.Drawing.Size(124, 48);
            this.btnQueryId.TabIndex = 65;
            this.btnQueryId.Text = "Query Id";
            this.btnQueryId.Click += new System.EventHandler(this.btnQueryId_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 50);
            this.label2.TabIndex = 64;
            this.label2.Text = "Group id";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 34);
            this.label5.TabIndex = 63;
            this.label5.Text = "Date";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpIdCrdat
            // 
            this.dtpIdCrdat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpIdCrdat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpIdCrdat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIdCrdat.Location = new System.Drawing.Point(78, 32);
            this.dtpIdCrdat.Margin = new System.Windows.Forms.Padding(4);
            this.dtpIdCrdat.Name = "dtpIdCrdat";
            this.dtpIdCrdat.Size = new System.Drawing.Size(156, 30);
            this.dtpIdCrdat.TabIndex = 62;
            this.dtpIdCrdat.ValueChanged += new System.EventHandler(this.dtpIdCrdat_ValueChanged);
            // 
            // gbId
            // 
            this.gbId.Controls.Add(this.label12);
            this.gbId.Controls.Add(this.txtDoc);
            this.gbId.Controls.Add(this.label2);
            this.gbId.Controls.Add(this.chkShortage);
            this.gbId.Controls.Add(this.cmbGrpid);
            this.gbId.Controls.Add(this.btnQueryId);
            this.gbId.Controls.Add(this.label5);
            this.gbId.Controls.Add(this.dtpIdCrdat);
            this.gbId.Controls.Add(this.gbOrderBy);
            this.gbId.Enabled = false;
            this.gbId.Location = new System.Drawing.Point(705, -8);
            this.gbId.Margin = new System.Windows.Forms.Padding(4);
            this.gbId.Name = "gbId";
            this.gbId.Padding = new System.Windows.Forms.Padding(4);
            this.gbId.Size = new System.Drawing.Size(356, 296);
            this.gbId.TabIndex = 0;
            this.gbId.TabStop = false;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 210);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 34);
            this.label12.TabIndex = 69;
            this.label12.Text = "Document";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDoc
            // 
            this.txtDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoc.Location = new System.Drawing.Point(10, 254);
            this.txtDoc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDoc.MaxLength = 100;
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.Size = new System.Drawing.Size(314, 30);
            this.txtDoc.TabIndex = 68;
            this.txtDoc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDoc_KeyDown);
            // 
            // gbOrderBy
            // 
            this.gbOrderBy.Controls.Add(this.rdoLocat);
            this.gbOrderBy.Controls.Add(this.rdoMatnr);
            this.gbOrderBy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOrderBy.Location = new System.Drawing.Point(112, 186);
            this.gbOrderBy.Margin = new System.Windows.Forms.Padding(4);
            this.gbOrderBy.Name = "gbOrderBy";
            this.gbOrderBy.Padding = new System.Windows.Forms.Padding(4);
            this.gbOrderBy.Size = new System.Drawing.Size(232, 75);
            this.gbOrderBy.TabIndex = 70;
            this.gbOrderBy.TabStop = false;
            this.gbOrderBy.Text = "Order by";
            // 
            // rdoLocat
            // 
            this.rdoLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLocat.Location = new System.Drawing.Point(117, 22);
            this.rdoLocat.Margin = new System.Windows.Forms.Padding(4);
            this.rdoLocat.Name = "rdoLocat";
            this.rdoLocat.Size = new System.Drawing.Size(111, 36);
            this.rdoLocat.TabIndex = 1;
            this.rdoLocat.Text = "Location";
            // 
            // rdoMatnr
            // 
            this.rdoMatnr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMatnr.Location = new System.Drawing.Point(16, 22);
            this.rdoMatnr.Margin = new System.Windows.Forms.Padding(4);
            this.rdoMatnr.Name = "rdoMatnr";
            this.rdoMatnr.Size = new System.Drawing.Size(106, 36);
            this.rdoMatnr.TabIndex = 0;
            this.rdoMatnr.Text = "Part No";
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.chkDateCode);
            this.gbHeader.Controls.Add(this.label11);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.label6);
            this.gbHeader.Controls.Add(this.cmbType);
            this.gbHeader.Location = new System.Drawing.Point(0, -8);
            this.gbHeader.Margin = new System.Windows.Forms.Padding(4);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Padding = new System.Windows.Forms.Padding(4);
            this.gbHeader.Size = new System.Drawing.Size(208, 268);
            this.gbHeader.TabIndex = 50;
            this.gbHeader.TabStop = false;
            // 
            // chkDateCode
            // 
            this.chkDateCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDateCode.Enabled = false;
            this.chkDateCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateCode.Location = new System.Drawing.Point(69, 225);
            this.chkDateCode.Margin = new System.Windows.Forms.Padding(4);
            this.chkDateCode.Name = "chkDateCode";
            this.chkDateCode.Size = new System.Drawing.Size(134, 36);
            this.chkDateCode.TabIndex = 56;
            this.chkDateCode.Text = "DateCode";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 94);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 34);
            this.label11.TabIndex = 21;
            this.label11.Text = "Storage";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLgort.ItemHeight = 24;
            this.cmbLgort.Location = new System.Drawing.Point(88, 93);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(115, 32);
            this.cmbLgort.TabIndex = 19;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDocDownload);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnReprint);
            this.panel1.Controls.Add(this.btnSummary);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnDateCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 631);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1052, 100);
            this.panel1.TabIndex = 74;
            // 
            // btnDocDownload
            // 
            this.btnDocDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDocDownload.Enabled = false;
            this.btnDocDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDocDownload.Location = new System.Drawing.Point(511, 13);
            this.btnDocDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDocDownload.Name = "btnDocDownload";
            this.btnDocDownload.Size = new System.Drawing.Size(126, 74);
            this.btnDocDownload.TabIndex = 79;
            this.btnDocDownload.Text = "Doc Download";
            this.btnDocDownload.Click += new System.EventHandler(this.btnDocDownload_Click);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReport.Enabled = false;
            this.btnReport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(96, 26);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(92, 48);
            this.btnReport.TabIndex = 78;
            this.btnReport.Text = "Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(196, 26);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(128, 48);
            this.btnDownload.TabIndex = 77;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(332, 26);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(93, 48);
            this.btnRefresh.TabIndex = 75;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(433, 26);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 48);
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(11, 26);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(77, 48);
            this.btnPrint.TabIndex = 74;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReprint.Enabled = false;
            this.btnReprint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprint.Location = new System.Drawing.Point(722, 26);
            this.btnReprint.Margin = new System.Windows.Forms.Padding(4);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(73, 48);
            this.btnReprint.TabIndex = 81;
            this.btnReprint.Text = "补印";
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSummary.Enabled = false;
            this.btnSummary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummary.Location = new System.Drawing.Point(925, 13);
            this.btnSummary.Margin = new System.Windows.Forms.Padding(4);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(112, 74);
            this.btnSummary.TabIndex = 83;
            this.btnSummary.Text = "补印Summary";
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Enabled = false;
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(645, 26);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(69, 48);
            this.btnQuery.TabIndex = 80;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnDateCode
            // 
            this.btnDateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDateCode.Enabled = false;
            this.btnDateCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDateCode.Location = new System.Drawing.Point(803, 13);
            this.btnDateCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnDateCode.Name = "btnDateCode";
            this.btnDateCode.Size = new System.Drawing.Size(112, 74);
            this.btnDateCode.TabIndex = 82;
            this.btnDateCode.Text = "补印DateCode";
            this.btnDateCode.Click += new System.EventHandler(this.btnDateCode_Click);
            // 
            // Manage_SapSimulationDataQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 764);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbHeader);
            this.Controls.Add(this.gbId);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.gbDoc);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Manage_SapSimulationDataQuery";
            this.Text = "Manage_SapSimulationDataQuery";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbDoc.ResumeLayout(false);
            this.gbDoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.gbId.ResumeLayout(false);
            this.gbId.PerformLayout();
            this.gbOrderBy.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox gbDoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.ComboBox cmbWerks;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDocCrdat;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile1;
        private System.Windows.Forms.ComboBox cmbTimeFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbTimeTo;
        private System.Windows.Forms.CheckBox checkStock;
        private System.Windows.Forms.Button btnQueryDoc;
        private System.Windows.Forms.CheckBox chkShortage;
        private System.Windows.Forms.ComboBox cmbGrpid;
        private System.Windows.Forms.Button btnQueryId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpIdCrdat;
        private System.Windows.Forms.GroupBox gbId;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.GroupBox gbOrderBy;
        private System.Windows.Forms.RadioButton rdoLocat;
        private System.Windows.Forms.RadioButton rdoMatnr;
        private System.Windows.Forms.CheckBox chkDateCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDocDownload;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnDateCode;
    }
}