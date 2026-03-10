namespace QWMS
{
    partial class IQCBorrowMaterials
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLifnr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.dtpIndat = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdQuery = new System.Windows.Forms.RadioButton();
            this.rdbRT = new System.Windows.Forms.RadioButton();
            this.rdbBR = new System.Windows.Forms.RadioButton();
            this.lblWHCheck = new System.Windows.Forms.Label();
            this.lblIQCCheck = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBorrowWHID = new System.Windows.Forms.TextBox();
            this.txtBorrowIQCID = new System.Windows.Forms.TextBox();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbHeader.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(141, 21);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(170, 27);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 224);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1022, 335);
            this.panel2.TabIndex = 34;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(394, 283);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(112, 38);
            this.btnDownload.TabIndex = 33;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(31, 4);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(81, 17);
            this.lblCount.TabIndex = 32;
            this.lblCount.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.Location = new System.Drawing.Point(31, 25);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(960, 250);
            this.dgvData.TabIndex = 11;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(274, 282);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 38);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(152, 282);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 38);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(31, 282);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 38);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(319, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "In Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.chkSelect);
            this.gbHeader.Controls.Add(this.label8);
            this.gbHeader.Controls.Add(this.dtpToDate);
            this.gbHeader.Controls.Add(this.txtMatnr);
            this.gbHeader.Controls.Add(this.txtMblnr);
            this.gbHeader.Controls.Add(this.label11);
            this.gbHeader.Controls.Add(this.label7);
            this.gbHeader.Controls.Add(this.cmbStatus);
            this.gbHeader.Controls.Add(this.label6);
            this.gbHeader.Controls.Add(this.txtLifnr);
            this.gbHeader.Controls.Add(this.label5);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.label4);
            this.gbHeader.Controls.Add(this.txtLocat);
            this.gbHeader.Controls.Add(this.dtpIndat);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.btnConfirm);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(432, 8);
            this.gbHeader.Margin = new System.Windows.Forms.Padding(4);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Padding = new System.Windows.Forms.Padding(4);
            this.gbHeader.Size = new System.Drawing.Size(586, 212);
            this.gbHeader.TabIndex = 29;
            this.gbHeader.TabStop = false;
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Enabled = false;
            this.chkSelect.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSelect.Location = new System.Drawing.Point(322, 179);
            this.chkSelect.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(130, 21);
            this.chkSelect.TabIndex = 69;
            this.chkSelect.Text = "Select All";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.Visible = false;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(391, 62);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 19);
            this.label8.TabIndex = 19;
            this.label8.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpToDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(428, 62);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(143, 26);
            this.dtpToDate.TabIndex = 18;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMatnr.Location = new System.Drawing.Point(141, 169);
            this.txtMatnr.Margin = new System.Windows.Forms.Padding(4);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(170, 26);
            this.txtMatnr.TabIndex = 17;
            this.txtMatnr.DoubleClick += new System.EventHandler(this.txtMblnr_DoubleClick);
            this.txtMatnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatnr_KeyPress);
            // 
            // txtMblnr
            // 
            this.txtMblnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMblnr.Location = new System.Drawing.Point(141, 131);
            this.txtMblnr.Margin = new System.Windows.Forms.Padding(4);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(170, 26);
            this.txtMblnr.TabIndex = 17;
            this.txtMblnr.DoubleClick += new System.EventHandler(this.txtMblnr_DoubleClick);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(76, 172);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 19);
            this.label11.TabIndex = 16;
            this.label11.Text = "料号";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 135);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 19);
            this.label7.TabIndex = 16;
            this.label7.Text = "SAP单据";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All",
            "已还",
            "未还"});
            this.cmbStatus.Location = new System.Drawing.Point(428, 131);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(143, 27);
            this.cmbStatus.TabIndex = 15;
            this.cmbStatus.Text = "All";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "状态";
            // 
            // txtLifnr
            // 
            this.txtLifnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLifnr.Location = new System.Drawing.Point(428, 99);
            this.txtLifnr.Margin = new System.Windows.Forms.Padding(4);
            this.txtLifnr.Name = "txtLifnr";
            this.txtLifnr.Size = new System.Drawing.Size(143, 26);
            this.txtLifnr.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(325, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "厂商代码";
            // 
            // txtLocat
            // 
            this.txtLocat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Location = new System.Drawing.Point(141, 96);
            this.txtLocat.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(170, 26);
            this.txtLocat.TabIndex = 4;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            // 
            // dtpIndat
            // 
            this.dtpIndat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpIndat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpIndat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIndat.Location = new System.Drawing.Point(428, 21);
            this.dtpIndat.Margin = new System.Windows.Forms.Padding(4);
            this.dtpIndat.Name = "dtpIndat";
            this.dtpIndat.Size = new System.Drawing.Size(143, 26);
            this.dtpIndat.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Location = new System.Drawing.Point(459, 169);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(112, 38);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(32, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(32, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(141, 59);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(170, 27);
            this.cmbLgort.TabIndex = 3;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1022, 224);
            this.panel4.TabIndex = 31;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdQuery);
            this.groupBox1.Controls.Add(this.rdbRT);
            this.groupBox1.Controls.Add(this.rdbBR);
            this.groupBox1.Controls.Add(this.lblWHCheck);
            this.groupBox1.Controls.Add(this.lblIQCCheck);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtBorrowWHID);
            this.groupBox1.Controls.Add(this.txtBorrowIQCID);
            this.groupBox1.Location = new System.Drawing.Point(15, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(404, 212);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // rdQuery
            // 
            this.rdQuery.AutoSize = true;
            this.rdQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdQuery.Location = new System.Drawing.Point(243, 21);
            this.rdQuery.Margin = new System.Windows.Forms.Padding(4);
            this.rdQuery.Name = "rdQuery";
            this.rdQuery.Size = new System.Drawing.Size(68, 23);
            this.rdQuery.TabIndex = 22;
            this.rdQuery.TabStop = true;
            this.rdQuery.Text = "查询";
            this.rdQuery.UseVisualStyleBackColor = true;
            this.rdQuery.CheckedChanged += new System.EventHandler(this.rdQuery_CheckedChanged);
            // 
            // rdbRT
            // 
            this.rdbRT.AutoSize = true;
            this.rdbRT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbRT.Location = new System.Drawing.Point(151, 21);
            this.rdbRT.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRT.Name = "rdbRT";
            this.rdbRT.Size = new System.Drawing.Size(68, 23);
            this.rdbRT.TabIndex = 21;
            this.rdbRT.TabStop = true;
            this.rdbRT.Text = "还料";
            this.rdbRT.UseVisualStyleBackColor = true;
            this.rdbRT.CheckedChanged += new System.EventHandler(this.rdbRT_CheckedChanged);
            // 
            // rdbBR
            // 
            this.rdbBR.AutoSize = true;
            this.rdbBR.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbBR.Location = new System.Drawing.Point(54, 21);
            this.rdbBR.Margin = new System.Windows.Forms.Padding(4);
            this.rdbBR.Name = "rdbBR";
            this.rdbBR.Size = new System.Drawing.Size(68, 23);
            this.rdbBR.TabIndex = 20;
            this.rdbBR.TabStop = true;
            this.rdbBR.Text = "借料";
            this.rdbBR.UseVisualStyleBackColor = true;
            this.rdbBR.CheckedChanged += new System.EventHandler(this.rdbBR_CheckedChanged);
            // 
            // lblWHCheck
            // 
            this.lblWHCheck.AutoSize = true;
            this.lblWHCheck.ForeColor = System.Drawing.Color.Red;
            this.lblWHCheck.Location = new System.Drawing.Point(136, 181);
            this.lblWHCheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWHCheck.Name = "lblWHCheck";
            this.lblWHCheck.Size = new System.Drawing.Size(83, 15);
            this.lblWHCheck.TabIndex = 5;
            this.lblWHCheck.Text = "验证WH身份";
            // 
            // lblIQCCheck
            // 
            this.lblIQCCheck.AutoSize = true;
            this.lblIQCCheck.ForeColor = System.Drawing.Color.Red;
            this.lblIQCCheck.Location = new System.Drawing.Point(136, 109);
            this.lblIQCCheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIQCCheck.Name = "lblIQCCheck";
            this.lblIQCCheck.Size = new System.Drawing.Size(91, 15);
            this.lblIQCCheck.TabIndex = 4;
            this.lblIQCCheck.Text = "验证IQC身份";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(48, 149);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 19);
            this.label10.TabIndex = 3;
            this.label10.Text = "WH刷卡";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(48, 81);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "IQC刷卡";
            // 
            // txtBorrowWHID
            // 
            this.txtBorrowWHID.Location = new System.Drawing.Point(136, 146);
            this.txtBorrowWHID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBorrowWHID.Name = "txtBorrowWHID";
            this.txtBorrowWHID.Size = new System.Drawing.Size(248, 25);
            this.txtBorrowWHID.TabIndex = 1;
            this.txtBorrowWHID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBorrowWHID_KeyPress);
            // 
            // txtBorrowIQCID
            // 
            this.txtBorrowIQCID.Location = new System.Drawing.Point(136, 75);
            this.txtBorrowIQCID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBorrowIQCID.Name = "txtBorrowIQCID";
            this.txtBorrowIQCID.Size = new System.Drawing.Size(248, 25);
            this.txtBorrowIQCID.TabIndex = 0;
            this.txtBorrowIQCID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBorrowIQCID_KeyPress);
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 559);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1022, 32);
            this.stbStatus.TabIndex = 32;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1022, 224);
            this.panel1.TabIndex = 33;
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "BorrowMaterials.xls";
            this.sfdSaveFile.Filter = "Text Files (*.xls)|*.xls";
            // 
            // IQCBorrowMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1022, 591);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IQCBorrowMaterials";
            this.Text = "IQCBorrowMaterials";
            this.Resize += new System.EventHandler(this.IQCBorrowMaterials_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.DateTimePicker dtpIndat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel4;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLifnr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBorrowWHID;
        private System.Windows.Forms.TextBox txtBorrowIQCID;
        private System.Windows.Forms.Label lblWHCheck;
        private System.Windows.Forms.Label lblIQCCheck;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.RadioButton rdbRT;
        private System.Windows.Forms.RadioButton rdbBR;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.RadioButton rdQuery;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
    }
}