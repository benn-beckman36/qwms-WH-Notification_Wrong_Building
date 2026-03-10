namespace QWMS.OTAUT
{
    partial class StorageOut_SimulationScan_AGV
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnFlip = new System.Windows.Forms.Button();
            this.btnCallAGV = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbSendID = new System.Windows.Forms.Label();
            this.cmbGrpID = new System.Windows.Forms.ComboBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lbLocation = new System.Windows.Forms.Label();
            this.lbCurrentShelf = new System.Windows.Forms.Label();
            this.txtCurrentShelf = new System.Windows.Forms.TextBox();
            this.lbBarCode = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.lbStorage = new System.Windows.Forms.Label();
            this.lbPlant = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lbWorkStation = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWorkStation = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabCurrentInfo = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvCurrentShelfInfo = new System.Windows.Forms.DataGridView();
            this.tabTotalInfo = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSendIDInfo = new System.Windows.Forms.DataGridView();
            this.tabAGVInfo = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvAGVInfo = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTaskNo = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabCurrentInfo.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentShelfInfo)).BeginInit();
            this.tabTotalInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendIDInfo)).BeginInit();
            this.tabAGVInfo.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAGVInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 805);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1333, 28);
            this.statusStrip1.TabIndex = 45;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 21);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 21);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 21);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 21);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 21);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1333, 805);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 46;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.btnFlip);
            this.panel1.Controls.Add(this.btnCallAGV);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1333, 49);
            this.panel1.TabIndex = 42;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1024, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(99, 36);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(296, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(99, 36);
            this.btnQuery.TabIndex = 48;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(842, 9);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(99, 36);
            this.btnEnd.TabIndex = 46;
            this.btnEnd.Text = "结束任务";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnFlip
            // 
            this.btnFlip.Location = new System.Drawing.Point(660, 9);
            this.btnFlip.Name = "btnFlip";
            this.btnFlip.Size = new System.Drawing.Size(99, 36);
            this.btnFlip.TabIndex = 44;
            this.btnFlip.Text = "翻面";
            this.btnFlip.UseVisualStyleBackColor = true;
            this.btnFlip.Click += new System.EventHandler(this.btnFlip_Click);
            // 
            // btnCallAGV
            // 
            this.btnCallAGV.Location = new System.Drawing.Point(478, 9);
            this.btnCallAGV.Name = "btnCallAGV";
            this.btnCallAGV.Size = new System.Drawing.Size(99, 36);
            this.btnCallAGV.TabIndex = 42;
            this.btnCallAGV.Text = "呼叫小车";
            this.btnCallAGV.UseVisualStyleBackColor = true;
            this.btnCallAGV.Click += new System.EventHandler(this.btnCallAGV_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 11;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.967865F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.40015F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.125287F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.68144F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.805959F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.15355F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.653171F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.500382F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.55691F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lbSendID, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtLocation, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbLocation, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbCurrentShelf, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtCurrentShelf, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbBarCode, 4, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtBarCode, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbStorage, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbPlant, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbWerks, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbWorkStation, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbLgort, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbWorkStation, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbGrpID, 7, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 8, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbTaskNo, 9, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1333, 105);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lbSendID
            // 
            this.lbSendID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSendID.AutoSize = true;
            this.lbSendID.Location = new System.Drawing.Point(672, 28);
            this.lbSendID.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbSendID.Name = "lbSendID";
            this.lbSendID.Size = new System.Drawing.Size(68, 20);
            this.lbSendID.TabIndex = 39;
            this.lbSendID.Text = "Send ID";
            this.lbSendID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbGrpID
            // 
            this.cmbGrpID.FormattingEnabled = true;
            this.cmbGrpID.Location = new System.Drawing.Point(746, 26);
            this.cmbGrpID.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbGrpID.Name = "cmbGrpID";
            this.cmbGrpID.Size = new System.Drawing.Size(232, 28);
            this.cmbGrpID.TabIndex = 40;
            this.cmbGrpID.SelectedIndexChanged += new System.EventHandler(this.cmbGrpID_SelectedIndexChanged);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(284, 68);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(160, 26);
            this.txtLocation.TabIndex = 29;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            // 
            // lbLocation
            // 
            this.lbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(237, 70);
            this.lbLocation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(41, 20);
            this.lbLocation.TabIndex = 28;
            this.lbLocation.Text = "储位";
            this.lbLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCurrentShelf
            // 
            this.lbCurrentShelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentShelf.AutoSize = true;
            this.lbCurrentShelf.Location = new System.Drawing.Point(18, 70);
            this.lbCurrentShelf.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbCurrentShelf.Name = "lbCurrentShelf";
            this.lbCurrentShelf.Size = new System.Drawing.Size(57, 35);
            this.lbCurrentShelf.TabIndex = 0;
            this.lbCurrentShelf.Text = "工作料架";
            this.lbCurrentShelf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCurrentShelf
            // 
            this.txtCurrentShelf.Enabled = false;
            this.txtCurrentShelf.Location = new System.Drawing.Point(81, 68);
            this.txtCurrentShelf.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtCurrentShelf.Name = "txtCurrentShelf";
            this.txtCurrentShelf.Size = new System.Drawing.Size(143, 26);
            this.txtCurrentShelf.TabIndex = 7;
            // 
            // lbBarCode
            // 
            this.lbBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbBarCode.AutoSize = true;
            this.lbBarCode.Location = new System.Drawing.Point(457, 70);
            this.lbBarCode.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Size = new System.Drawing.Size(63, 35);
            this.lbBarCode.TabIndex = 30;
            this.lbBarCode.Text = "BarCode";
            this.lbBarCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBarCode
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtBarCode, 5);
            this.txtBarCode.Location = new System.Drawing.Point(526, 68);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(516, 26);
            this.txtBarCode.TabIndex = 31;
            this.txtBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarCode_KeyDown);
            // 
            // lbStorage
            // 
            this.lbStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStorage.AutoSize = true;
            this.lbStorage.Location = new System.Drawing.Point(483, 28);
            this.lbStorage.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbStorage.Name = "lbStorage";
            this.lbStorage.Size = new System.Drawing.Size(37, 20);
            this.lbStorage.TabIndex = 34;
            this.lbStorage.Text = "仓别";
            this.lbStorage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPlant
            // 
            this.lbPlant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlant.AutoSize = true;
            this.lbPlant.Location = new System.Drawing.Point(237, 28);
            this.lbPlant.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbPlant.Name = "lbPlant";
            this.lbPlant.Size = new System.Drawing.Size(41, 20);
            this.lbPlant.TabIndex = 32;
            this.lbPlant.Text = "厂区";
            this.lbPlant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(284, 26);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(160, 28);
            this.cmbWerks.TabIndex = 33;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lbWorkStation
            // 
            this.lbWorkStation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbWorkStation.AutoSize = true;
            this.lbWorkStation.Location = new System.Drawing.Point(18, 28);
            this.lbWorkStation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbWorkStation.Name = "lbWorkStation";
            this.lbWorkStation.Size = new System.Drawing.Size(57, 20);
            this.lbWorkStation.TabIndex = 36;
            this.lbWorkStation.Text = "工作站";
            this.lbWorkStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(526, 26);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(140, 28);
            this.cmbLgort.TabIndex = 35;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // cmbWorkStation
            // 
            this.cmbWorkStation.FormattingEnabled = true;
            this.cmbWorkStation.Location = new System.Drawing.Point(81, 26);
            this.cmbWorkStation.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbWorkStation.Name = "cmbWorkStation";
            this.cmbWorkStation.Size = new System.Drawing.Size(143, 28);
            this.cmbWorkStation.TabIndex = 37;
            this.cmbWorkStation.SelectedIndexChanged += new System.EventHandler(this.cmbWorkStation_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabCurrentInfo);
            this.splitContainer2.Panel1.Controls.Add(this.tabTotalInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabAGVInfo);
            this.splitContainer2.Size = new System.Drawing.Size(1333, 647);
            this.splitContainer2.SplitterDistance = 885;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabCurrentInfo
            // 
            this.tabCurrentInfo.Controls.Add(this.tabPage4);
            this.tabCurrentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCurrentInfo.Location = new System.Drawing.Point(0, 0);
            this.tabCurrentInfo.Name = "tabCurrentInfo";
            this.tabCurrentInfo.SelectedIndex = 0;
            this.tabCurrentInfo.Size = new System.Drawing.Size(885, 226);
            this.tabCurrentInfo.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvCurrentShelfInfo);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(877, 193);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Current Shelf Info";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvCurrentShelfInfo
            // 
            this.dgvCurrentShelfInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentShelfInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurrentShelfInfo.Location = new System.Drawing.Point(3, 3);
            this.dgvCurrentShelfInfo.Name = "dgvCurrentShelfInfo";
            this.dgvCurrentShelfInfo.RowHeadersWidth = 62;
            this.dgvCurrentShelfInfo.RowTemplate.Height = 28;
            this.dgvCurrentShelfInfo.Size = new System.Drawing.Size(871, 187);
            this.dgvCurrentShelfInfo.TabIndex = 1;
            // 
            // tabTotalInfo
            // 
            this.tabTotalInfo.Controls.Add(this.tabPage1);
            this.tabTotalInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabTotalInfo.Location = new System.Drawing.Point(0, 226);
            this.tabTotalInfo.Name = "tabTotalInfo";
            this.tabTotalInfo.SelectedIndex = 0;
            this.tabTotalInfo.Size = new System.Drawing.Size(885, 421);
            this.tabTotalInfo.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSendIDInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 388);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "StorageOut Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSendIDInfo
            // 
            this.dgvSendIDInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSendIDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSendIDInfo.Location = new System.Drawing.Point(3, 3);
            this.dgvSendIDInfo.Name = "dgvSendIDInfo";
            this.dgvSendIDInfo.RowHeadersWidth = 62;
            this.dgvSendIDInfo.RowTemplate.Height = 28;
            this.dgvSendIDInfo.Size = new System.Drawing.Size(871, 382);
            this.dgvSendIDInfo.TabIndex = 0;
            // 
            // tabAGVInfo
            // 
            this.tabAGVInfo.Controls.Add(this.tabPage3);
            this.tabAGVInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAGVInfo.Location = new System.Drawing.Point(0, 0);
            this.tabAGVInfo.Name = "tabAGVInfo";
            this.tabAGVInfo.SelectedIndex = 0;
            this.tabAGVInfo.Size = new System.Drawing.Size(444, 647);
            this.tabAGVInfo.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvAGVInfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(436, 614);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "AGV Order";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvAGVInfo
            // 
            this.dgvAGVInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAGVInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAGVInfo.Location = new System.Drawing.Point(3, 3);
            this.dgvAGVInfo.Name = "dgvAGVInfo";
            this.dgvAGVInfo.RowHeadersWidth = 62;
            this.dgvAGVInfo.RowTemplate.Height = 28;
            this.dgvAGVInfo.Size = new System.Drawing.Size(430, 608);
            this.dgvAGVInfo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(987, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "TaskNo";
            // 
            // cmbTaskNo
            // 
            this.cmbTaskNo.FormattingEnabled = true;
            this.cmbTaskNo.Location = new System.Drawing.Point(1056, 26);
            this.cmbTaskNo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.cmbTaskNo.Name = "cmbTaskNo";
            this.cmbTaskNo.Size = new System.Drawing.Size(250, 28);
            this.cmbTaskNo.TabIndex = 42;
            this.cmbTaskNo.SelectedIndexChanged += new System.EventHandler(this.cmbTaskNo_SelectedIndexChanged);
            // 
            // StorageOut_SimulationScan_AGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 833);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "StorageOut_SimulationScan_AGV";
            this.Text = "StorageOut_SimulationScan_AGV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageOut_SimulationScan_AGV_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabCurrentInfo.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentShelfInfo)).EndInit();
            this.tabTotalInfo.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendIDInfo)).EndInit();
            this.tabAGVInfo.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAGVInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtCurrentShelf;
        private System.Windows.Forms.Label lbCurrentShelf;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lbBarCode;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbPlant;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lbStorage;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label lbWorkStation;
        private System.Windows.Forms.ComboBox cmbWorkStation;
        private System.Windows.Forms.Label lbSendID;
        private System.Windows.Forms.ComboBox cmbGrpID;
        private System.Windows.Forms.DataGridView dgvSendIDInfo;
        private System.Windows.Forms.TabControl tabTotalInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabAGVInfo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnFlip;
        private System.Windows.Forms.Button btnCallAGV;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvAGVInfo;
        private System.Windows.Forms.TabControl tabCurrentInfo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvCurrentShelfInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTaskNo;
    }
}