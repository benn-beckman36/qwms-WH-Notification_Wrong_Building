namespace QWMS
{
    partial class TransferOut_Upload_351_60S
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.lbFLgort = new System.Windows.Forms.Label();
            this.lbFWerks = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtmblnr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuary = new System.Windows.Forms.Button();
            this.gvData_sap = new System.Windows.Forms.DataGridView();
            this.gvData_qwms = new System.Windows.Forms.DataGridView();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbType = new System.Windows.Forms.GroupBox();
            this.rb60S = new System.Windows.Forms.RadioButton();
            this.rb351 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtlocat = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LnkSOP = new System.Windows.Forms.LinkLabel();
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.lbFile = new System.Windows.Forms.Label();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.btnPrint = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData_sap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData_qwms)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(994, 22);
            this.statusStrip1.TabIndex = 43;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 17);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 17);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 17);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 17);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 17);
            // 
            // cmbLgort
            // 
            this.cmbLgort.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLgort.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(237, 70);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(115, 20);
            this.cmbLgort.TabIndex = 47;
            // 
            // lbFLgort
            // 
            this.lbFLgort.AutoSize = true;
            this.lbFLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFLgort.Location = new System.Drawing.Point(140, 70);
            this.lbFLgort.Name = "lbFLgort";
            this.lbFLgort.Size = new System.Drawing.Size(76, 16);
            this.lbFLgort.TabIndex = 46;
            this.lbFLgort.Text = "调出仓别";
            // 
            // lbFWerks
            // 
            this.lbFWerks.AutoSize = true;
            this.lbFWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFWerks.Location = new System.Drawing.Point(140, 37);
            this.lbFWerks.Name = "lbFWerks";
            this.lbFWerks.Size = new System.Drawing.Size(76, 16);
            this.lbFWerks.TabIndex = 45;
            this.lbFWerks.Text = "调出厂区";
            // 
            // cmbWerks
            // 
            this.cmbWerks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbWerks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(237, 33);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(115, 20);
            this.cmbWerks.TabIndex = 44;
            this.cmbWerks.SelectedValueChanged += new System.EventHandler(this.cmbWerks_SelectedValueChanged);
            // 
            // txtmblnr
            // 
            this.txtmblnr.Location = new System.Drawing.Point(453, 33);
            this.txtmblnr.Name = "txtmblnr";
            this.txtmblnr.Size = new System.Drawing.Size(143, 21);
            this.txtmblnr.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(415, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "PO:";
            // 
            // btnQuary
            // 
            this.btnQuary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnQuary.Location = new System.Drawing.Point(652, 41);
            this.btnQuary.Name = "btnQuary";
            this.btnQuary.Size = new System.Drawing.Size(75, 32);
            this.btnQuary.TabIndex = 50;
            this.btnQuary.Text = "quary";
            this.btnQuary.UseVisualStyleBackColor = true;
            this.btnQuary.Click += new System.EventHandler(this.btnQuary_Click);
            // 
            // gvData_sap
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData_sap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.gvData_sap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvData_sap.DefaultCellStyle = dataGridViewCellStyle26;
            this.gvData_sap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData_sap.Location = new System.Drawing.Point(0, 0);
            this.gvData_sap.Name = "gvData_sap";
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData_sap.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.gvData_sap.RowTemplate.Height = 23;
            this.gvData_sap.Size = new System.Drawing.Size(994, 185);
            this.gvData_sap.TabIndex = 51;
            // 
            // gvData_qwms
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData_qwms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.gvData_qwms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvData_qwms.DefaultCellStyle = dataGridViewCellStyle29;
            this.gvData_qwms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData_qwms.Location = new System.Drawing.Point(0, 0);
            this.gvData_qwms.Name = "gvData_qwms";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData_qwms.RowHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.gvData_qwms.RowTemplate.Height = 23;
            this.gvData_qwms.Size = new System.Drawing.Size(994, 182);
            this.gvData_qwms.TabIndex = 52;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(237, 494);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(88, 32);
            this.btnConfirm.TabIndex = 53;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(453, 494);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 54;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(881, 494);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 32);
            this.btnRefresh.TabIndex = 55;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 107);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gvData_sap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvData_qwms);
            this.splitContainer1.Size = new System.Drawing.Size(994, 371);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 56;
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.rb60S);
            this.gbType.Controls.Add(this.rb351);
            this.gbType.Location = new System.Drawing.Point(12, 12);
            this.gbType.Name = "gbType";
            this.gbType.Size = new System.Drawing.Size(122, 89);
            this.gbType.TabIndex = 57;
            this.gbType.TabStop = false;
            this.gbType.Text = "Tpye";
            // 
            // rb60S
            // 
            this.rb60S.AutoSize = true;
            this.rb60S.Location = new System.Drawing.Point(21, 58);
            this.rb60S.Name = "rb60S";
            this.rb60S.Size = new System.Drawing.Size(41, 16);
            this.rb60S.TabIndex = 1;
            this.rb60S.TabStop = true;
            this.rb60S.Text = "60S";
            this.rb60S.UseVisualStyleBackColor = true;
            this.rb60S.CheckedChanged += new System.EventHandler(this.rb60S_CheckedChanged);
            // 
            // rb351
            // 
            this.rb351.AutoSize = true;
            this.rb351.Location = new System.Drawing.Point(21, 26);
            this.rb351.Name = "rb351";
            this.rb351.Size = new System.Drawing.Size(41, 16);
            this.rb351.TabIndex = 0;
            this.rb351.TabStop = true;
            this.rb351.Text = "351";
            this.rb351.UseVisualStyleBackColor = true;
            this.rb351.CheckedChanged += new System.EventHandler(this.rb351_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "储位：";
            // 
            // txtlocat
            // 
            this.txtlocat.Enabled = false;
            this.txtlocat.Location = new System.Drawing.Point(453, 69);
            this.txtlocat.Name = "txtlocat";
            this.txtlocat.Size = new System.Drawing.Size(143, 21);
            this.txtlocat.TabIndex = 59;
            this.txtlocat.DoubleClick += new System.EventHandler(this.txtlocat_DoubleClick);
            this.txtlocat.TextChanged += new System.EventHandler(this.txtlocat_TextChanged);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnImport.Enabled = false;
            this.btnImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(46, 494);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 32);
            this.btnImport.TabIndex = 60;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.LnkSOP);
            this.panel1.Controls.Add(this.lnkSample);
            this.panel1.Controls.Add(this.txtFile);
            this.panel1.Controls.Add(this.btnFile);
            this.panel1.Controls.Add(this.lbFile);
            this.panel1.Location = new System.Drawing.Point(794, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 61;
            // 
            // LnkSOP
            // 
            this.LnkSOP.AutoSize = true;
            this.LnkSOP.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LnkSOP.Location = new System.Drawing.Point(110, 8);
            this.LnkSOP.Name = "LnkSOP";
            this.LnkSOP.Size = new System.Drawing.Size(57, 27);
            this.LnkSOP.TabIndex = 62;
            this.LnkSOP.TabStop = true;
            this.LnkSOP.Text = "SOP";
            this.LnkSOP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkSOP_LinkClicked);
            // 
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(111, 69);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(75, 26);
            this.lnkSample.TabIndex = 49;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "Sample";
            this.lnkSample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSample_LinkClicked);
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point(6, 47);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(182, 21);
            this.txtFile.TabIndex = 47;
            this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(6, 72);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(71, 23);
            this.btnFile.TabIndex = 48;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lbFile
            // 
            this.lbFile.AutoSize = true;
            this.lbFile.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFile.Location = new System.Drawing.Point(3, 13);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(65, 16);
            this.lbFile.TabIndex = 46;
            this.lbFile.Text = "File Path";
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(671, 495);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 54;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // TransferOut_Upload_351_60S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 551);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtlocat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbType);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnQuary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtmblnr);
            this.Controls.Add(this.cmbLgort);
            this.Controls.Add(this.lbFLgort);
            this.Controls.Add(this.lbFWerks);
            this.Controls.Add(this.cmbWerks);
            this.Controls.Add(this.statusStrip1);
            this.Name = "TransferOut_Upload_351_60S";
            this.Text = "TransferOut_Upload_351";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData_sap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData_qwms)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label lbFLgort;
        private System.Windows.Forms.Label lbFWerks;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.TextBox txtmblnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuary;
        private System.Windows.Forms.DataGridView gvData_sap;
        private System.Windows.Forms.DataGridView gvData_qwms;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rb60S;
        private System.Windows.Forms.RadioButton rb351;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtlocat;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkSample;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.LinkLabel LnkSOP;
        private System.Windows.Forms.Button btnPrint;
    }
}