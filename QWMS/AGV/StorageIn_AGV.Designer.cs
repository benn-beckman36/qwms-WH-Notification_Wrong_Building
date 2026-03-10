
namespace QWMS
{
    partial class StorageIn_AGV
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbBasicOption = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbShelfsize = new System.Windows.Forms.ComboBox();
            this.cmbStoragetype = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.btnStartJobs = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbWorkstation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShelf = new System.Windows.Forms.TextBox();
            this.gbAGVOption = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEndJobs = new System.Windows.Forms.Button();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.btnFlip = new System.Windows.Forms.Button();
            this.btnCallOff = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AGV指令 = new System.Windows.Forms.TabPage();
            this.dgvAGVOrder = new System.Windows.Forms.DataGridView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvDocumentInfo = new System.Windows.Forms.DataGridView();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvBarCodeInfo = new System.Windows.Forms.DataGridView();
            this.txtShelftype = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbBasicOption.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbAGVOption.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AGV指令.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAGVOrder)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentInfo)).BeginInit();
            this.tabControl3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarCodeInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 919);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1488, 30);
            this.stbStatus.TabIndex = 37;
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
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(17, 18, 17, 18);
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1488, 919);
            this.tableLayoutPanel4.TabIndex = 39;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel4.SetColumnSpan(this.tableLayoutPanel3, 4);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel3.Controls.Add(this.gbBasicOption, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbAGVOption, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.splitContainer1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tabControl3, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(20, 22);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1448, 875);
            this.tableLayoutPanel3.TabIndex = 37;
            // 
            // gbBasicOption
            // 
            this.gbBasicOption.Controls.Add(this.tableLayoutPanel2);
            this.gbBasicOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBasicOption.Location = new System.Drawing.Point(3, 4);
            this.gbBasicOption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbBasicOption.Name = "gbBasicOption";
            this.gbBasicOption.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbBasicOption.Size = new System.Drawing.Size(645, 292);
            this.gbBasicOption.TabIndex = 1;
            this.gbBasicOption.TabStop = false;
            this.gbBasicOption.Text = "基础信息";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cmbShelfsize, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbStoragetype, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtMblnr, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnStartJobs, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbLgort, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbWerks, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbWorkstation, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtShelf, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtShelftype, 3, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(639, 263);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cmbShelfsize
            // 
            this.cmbShelfsize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbShelfsize.FormattingEnabled = true;
            this.cmbShelfsize.Location = new System.Drawing.Point(510, 65);
            this.cmbShelfsize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbShelfsize.Name = "cmbShelfsize";
            this.cmbShelfsize.Size = new System.Drawing.Size(151, 29);
            this.cmbShelfsize.TabIndex = 18;
            // 
            // cmbStoragetype
            // 
            this.cmbStoragetype.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbStoragetype.FormattingEnabled = true;
            this.cmbStoragetype.Location = new System.Drawing.Point(510, 13);
            this.cmbStoragetype.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbStoragetype.Name = "cmbStoragetype";
            this.cmbStoragetype.Size = new System.Drawing.Size(151, 29);
            this.cmbStoragetype.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(418, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 21);
            this.label12.TabIndex = 18;
            this.label12.Text = "料架属性";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(456, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 21);
            this.label11.TabIndex = 18;
            this.label11.Text = "尺寸";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(418, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 21);
            this.label10.TabIndex = 18;
            this.label10.Text = "入库类型";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(99, 171);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 21);
            this.label15.TabIndex = 19;
            this.label15.Text = "单据号";
            // 
            // txtMblnr
            // 
            this.txtMblnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMblnr.Location = new System.Drawing.Point(172, 168);
            this.txtMblnr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(151, 28);
            this.txtMblnr.TabIndex = 20;
            this.txtMblnr.DoubleClick += new System.EventHandler(this.txtMblnr_DoubleClick);
            // 
            // btnStartJobs
            // 
            this.btnStartJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartJobs.Location = new System.Drawing.Point(54, 212);
            this.btnStartJobs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStartJobs.Name = "btnStartJobs";
            this.btnStartJobs.Size = new System.Drawing.Size(112, 43);
            this.btnStartJobs.TabIndex = 8;
            this.btnStartJobs.Text = "开始作业";
            this.btnStartJobs.UseVisualStyleBackColor = true;
            this.btnStartJobs.Click += new System.EventHandler(this.btnStartJobs_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(118, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 21);
            this.label8.TabIndex = 18;
            this.label8.Text = "仓别";
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(172, 117);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(151, 29);
            this.cmbLgort.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(118, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 21);
            this.label7.TabIndex = 18;
            this.label7.Text = "厂区";
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(172, 65);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(151, 29);
            this.cmbWerks.TabIndex = 18;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "工作站";
            // 
            // cmbWorkstation
            // 
            this.cmbWorkstation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbWorkstation.FormattingEnabled = true;
            this.cmbWorkstation.Location = new System.Drawing.Point(172, 13);
            this.cmbWorkstation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbWorkstation.Name = "cmbWorkstation";
            this.cmbWorkstation.Size = new System.Drawing.Size(151, 29);
            this.cmbWorkstation.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 21);
            this.label1.TabIndex = 21;
            this.label1.Text = "料架编号";
            // 
            // txtShelf
            // 
            this.txtShelf.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtShelf.Location = new System.Drawing.Point(510, 168);
            this.txtShelf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShelf.Name = "txtShelf";
            this.txtShelf.Size = new System.Drawing.Size(151, 28);
            this.txtShelf.TabIndex = 22;
            // 
            // gbAGVOption
            // 
            this.gbAGVOption.Controls.Add(this.tableLayoutPanel5);
            this.gbAGVOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAGVOption.Location = new System.Drawing.Point(3, 304);
            this.gbAGVOption.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbAGVOption.Name = "gbAGVOption";
            this.gbAGVOption.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbAGVOption.Size = new System.Drawing.Size(645, 230);
            this.gbAGVOption.TabIndex = 2;
            this.gbAGVOption.TabStop = false;
            this.gbAGVOption.Text = "AGV 作业";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.btnEndJobs, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtBarCode, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label14, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtLocat, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.btnFlip, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnCallOff, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(639, 201);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // btnEndJobs
            // 
            this.btnEndJobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndJobs.Location = new System.Drawing.Point(54, 154);
            this.btnEndJobs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEndJobs.Name = "btnEndJobs";
            this.btnEndJobs.Size = new System.Drawing.Size(112, 43);
            this.btnEndJobs.TabIndex = 11;
            this.btnEndJobs.Text = "结束作业";
            this.btnEndJobs.UseVisualStyleBackColor = true;
            this.btnEndJobs.Click += new System.EventHandler(this.btnEndJobs_Click);
            // 
            // txtBarCode
            // 
            this.txtBarCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel5.SetColumnSpan(this.txtBarCode, 3);
            this.txtBarCode.Location = new System.Drawing.Point(172, 61);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(460, 28);
            this.txtBarCode.TabIndex = 13;
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(118, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 21);
            this.label13.TabIndex = 6;
            this.label13.Text = "条码";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(118, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 21);
            this.label14.TabIndex = 5;
            this.label14.Text = "储位";
            // 
            // txtLocat
            // 
            this.txtLocat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtLocat.Location = new System.Drawing.Point(172, 111);
            this.txtLocat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(151, 28);
            this.txtLocat.TabIndex = 12;
            this.txtLocat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            // 
            // btnFlip
            // 
            this.btnFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlip.Location = new System.Drawing.Point(54, 4);
            this.btnFlip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(112, 42);
            this.btnFlip.TabIndex = 9;
            this.btnFlip.Text = "翻面";
            this.btnFlip.UseVisualStyleBackColor = true;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // btnCallOff
            // 
            this.btnCallOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallOff.Location = new System.Drawing.Point(223, 4);
            this.btnCallOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCallOff.Name = "btnCallOff";
            this.btnCallOff.Size = new System.Drawing.Size(112, 42);
            this.btnCallOff.TabIndex = 10;
            this.btnCallOff.Text = "下一辆";
            this.btnCallOff.UseVisualStyleBackColor = true;
            this.btnCallOff.Click += new System.EventHandler(this.btnCallOff_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(654, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.tableLayoutPanel3.SetRowSpan(this.splitContainer1, 2);
            this.splitContainer1.Size = new System.Drawing.Size(791, 530);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AGV指令);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(791, 207);
            this.tabControl1.TabIndex = 0;
            // 
            // AGV指令
            // 
            this.AGV指令.Controls.Add(this.dgvAGVOrder);
            this.AGV指令.Location = new System.Drawing.Point(4, 30);
            this.AGV指令.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AGV指令.Name = "AGV指令";
            this.AGV指令.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AGV指令.Size = new System.Drawing.Size(783, 173);
            this.AGV指令.TabIndex = 0;
            this.AGV指令.Text = "AGV 指令";
            this.AGV指令.UseVisualStyleBackColor = true;
            // 
            // dgvAGVOrder
            // 
            this.dgvAGVOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAGVOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAGVOrder.Location = new System.Drawing.Point(3, 4);
            this.dgvAGVOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvAGVOrder.Name = "dgvAGVOrder";
            this.dgvAGVOrder.RowHeadersWidth = 51;
            this.dgvAGVOrder.RowTemplate.Height = 27;
            this.dgvAGVOrder.Size = new System.Drawing.Size(777, 165);
            this.dgvAGVOrder.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(791, 318);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvDocumentInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(783, 284);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "单据信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvDocumentInfo
            // 
            this.dgvDocumentInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocumentInfo.Location = new System.Drawing.Point(3, 4);
            this.dgvDocumentInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDocumentInfo.Name = "dgvDocumentInfo";
            this.dgvDocumentInfo.RowHeadersWidth = 51;
            this.dgvDocumentInfo.RowTemplate.Height = 27;
            this.dgvDocumentInfo.Size = new System.Drawing.Size(777, 276);
            this.dgvDocumentInfo.TabIndex = 0;
            // 
            // tabControl3
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.tabControl3, 2);
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(3, 542);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1442, 418);
            this.tabControl3.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvBarCodeInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1434, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "刷入信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvBarCodeInfo
            // 
            this.dgvBarCodeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarCodeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBarCodeInfo.Location = new System.Drawing.Point(3, 4);
            this.dgvBarCodeInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvBarCodeInfo.Name = "dgvBarCodeInfo";
            this.dgvBarCodeInfo.RowHeadersWidth = 51;
            this.dgvBarCodeInfo.RowTemplate.Height = 27;
            this.dgvBarCodeInfo.Size = new System.Drawing.Size(1428, 376);
            this.dgvBarCodeInfo.TabIndex = 0;
            // 
            // txtShelftype
            // 
            this.txtShelftype.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtShelftype.Enabled = false;
            this.txtShelftype.Location = new System.Drawing.Point(510, 116);
            this.txtShelftype.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShelftype.Name = "txtShelftype";
            this.txtShelftype.Size = new System.Drawing.Size(151, 28);
            this.txtShelftype.TabIndex = 23;
            // 
            // StorageIn_AGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 949);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.stbStatus);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StorageIn_AGV";
            this.Text = "StorageIn_AGV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageIn_AGV_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gbBasicOption.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbAGVOption.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.AGV指令.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAGVOrder)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentInfo)).EndInit();
            this.tabControl3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarCodeInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gbBasicOption;
        private System.Windows.Forms.GroupBox gbAGVOption;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AGV指令;
        private System.Windows.Forms.DataGridView dgvAGVOrder;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvDocumentInfo;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvBarCodeInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnStartJobs;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Button btnCallOff;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnEndJobs;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cmbShelfsize;
        private System.Windows.Forms.ComboBox cmbStoragetype;
        private System.Windows.Forms.ComboBox cmbWorkstation;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShelf;
        private System.Windows.Forms.TextBox txtShelftype;
    }
}