namespace QWMS
{
    partial class StorageOut_OnLineOut_SimulationOut
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
            this.btnSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblOutSource = new System.Windows.Forms.Label();
            this.dgvOutSource = new System.Windows.Forms.DataGridView();
            this.chkDateCode = new System.Windows.Forms.CheckBox();
            this.chkAllId = new System.Windows.Forms.CheckBox();
            this.lblWerks = new System.Windows.Forms.Label();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDacodPrint = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.lblStorage = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvStorage = new System.Windows.Forms.DataGridView();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnProduce = new System.Windows.Forms.Button();
            this.lblMatnr = new System.Windows.Forms.Label();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.lblArbpl = new System.Windows.Forms.Label();
            this.cmbArbpl = new System.Windows.Forms.ComboBox();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.lblMblnr = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblQwms = new System.Windows.Forms.Label();
            this.cmbQwms = new System.Windows.Forms.ComboBox();
            this.lblGrpid = new System.Windows.Forms.Label();
            this.cmbGrpid = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblLgort = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtpCrdat = new System.Windows.Forms.DateTimePicker();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(104, 315);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblOutSource);
            this.panel2.Controls.Add(this.dgvOutSource);
            this.panel2.Controls.Add(this.chkDateCode);
            this.panel2.Controls.Add(this.chkAllId);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 142);
            this.panel2.TabIndex = 35;
            // 
            // lblOutSource
            // 
            this.lblOutSource.AutoSize = true;
            this.lblOutSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutSource.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutSource.Location = new System.Drawing.Point(23, 0);
            this.lblOutSource.Name = "lblOutSource";
            this.lblOutSource.Size = new System.Drawing.Size(64, 18);
            this.lblOutSource.TabIndex = 10;
            this.lblOutSource.Text = "0 records";
            // 
            // dgvOutSource
            // 
            this.dgvOutSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutSource.Location = new System.Drawing.Point(22, 21);
            this.dgvOutSource.Name = "dgvOutSource";
            this.dgvOutSource.RowTemplate.Height = 18;
            this.dgvOutSource.Size = new System.Drawing.Size(640, 113);
            this.dgvOutSource.TabIndex = 9;
            this.dgvOutSource.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOutSource_RowHeaderMouseClick);
            // 
            // chkDateCode
            // 
            this.chkDateCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDateCode.Enabled = false;
            this.chkDateCode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateCode.Location = new System.Drawing.Point(613, 0);
            this.chkDateCode.Name = "chkDateCode";
            this.chkDateCode.Size = new System.Drawing.Size(89, 24);
            this.chkDateCode.TabIndex = 11;
            this.chkDateCode.Text = "DateCode";
            // 
            // chkAllId
            // 
            this.chkAllId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAllId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllId.Location = new System.Drawing.Point(548, 0);
            this.chkAllId.Name = "chkAllId";
            this.chkAllId.Size = new System.Drawing.Size(65, 24);
            this.chkAllId.TabIndex = 12;
            this.chkAllId.Text = "All id";
            this.chkAllId.CheckedChanged += new System.EventHandler(this.chkAllId_CheckedChanged);
            // 
            // lblWerks
            // 
            this.lblWerks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWerks.Location = new System.Drawing.Point(4, 4);
            this.lblWerks.Name = "lblWerks";
            this.lblWerks.Size = new System.Drawing.Size(48, 23);
            this.lblWerks.TabIndex = 9;
            this.lblWerks.Text = "Plant";
            this.lblWerks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 17);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 17);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDacodPrint);
            this.panel3.Controls.Add(this.btnSummary);
            this.panel3.Controls.Add(this.lblStorage);
            this.panel3.Controls.Add(this.statusStrip1);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.btnQuery);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.dgvStorage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(692, 377);
            this.panel3.TabIndex = 36;
            // 
            // btnDacodPrint
            // 
            this.btnDacodPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDacodPrint.Enabled = false;
            this.btnDacodPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDacodPrint.Location = new System.Drawing.Point(265, 310);
            this.btnDacodPrint.Name = "btnDacodPrint";
            this.btnDacodPrint.Size = new System.Drawing.Size(92, 44);
            this.btnDacodPrint.TabIndex = 49;
            this.btnDacodPrint.Text = "Print DateCode";
            this.btnDacodPrint.Click += new System.EventHandler(this.btnDacodPrint_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSummary.Enabled = false;
            this.btnSummary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummary.Location = new System.Drawing.Point(363, 315);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(77, 32);
            this.btnSummary.TabIndex = 48;
            this.btnSummary.Text = "Summary";
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStorage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorage.Location = new System.Drawing.Point(24, 145);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(64, 18);
            this.lblStorage.TabIndex = 42;
            this.lblStorage.Text = "0 records";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 355);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 41;
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
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(446, 315);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 32);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Enabled = false;
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(24, 315);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 32);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(527, 315);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(184, 315);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvStorage
            // 
            this.dgvStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStorage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStorage.Location = new System.Drawing.Point(24, 166);
            this.dgvStorage.Name = "dgvStorage";
            this.dgvStorage.RowTemplate.Height = 18;
            this.dgvStorage.Size = new System.Drawing.Size(640, 143);
            this.dgvStorage.TabIndex = 16;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.ItemHeight = 16;
            this.cmbWerks.Location = new System.Drawing.Point(86, 3);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(94, 24);
            this.cmbWerks.TabIndex = 8;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 96);
            this.panel1.TabIndex = 34;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.btnProduce);
            this.panel6.Controls.Add(this.lblMatnr);
            this.panel6.Controls.Add(this.txtMatnr);
            this.panel6.Controls.Add(this.lblArbpl);
            this.panel6.Controls.Add(this.cmbArbpl);
            this.panel6.Controls.Add(this.txtMblnr);
            this.panel6.Controls.Add(this.lblMblnr);
            this.panel6.Controls.Add(this.btnConfirm);
            this.panel6.Controls.Add(this.lblQwms);
            this.panel6.Controls.Add(this.cmbQwms);
            this.panel6.Controls.Add(this.lblGrpid);
            this.panel6.Controls.Add(this.cmbGrpid);
            this.panel6.Enabled = false;
            this.panel6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(295, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(397, 96);
            this.panel6.TabIndex = 13;
            // 
            // btnProduce
            // 
            this.btnProduce.Enabled = false;
            this.btnProduce.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProduce.Location = new System.Drawing.Point(318, 63);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(74, 32);
            this.btnProduce.TabIndex = 39;
            this.btnProduce.Text = "產生單據";
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // lblMatnr
            // 
            this.lblMatnr.Location = new System.Drawing.Point(253, 35);
            this.lblMatnr.Name = "lblMatnr";
            this.lblMatnr.Size = new System.Drawing.Size(64, 23);
            this.lblMatnr.TabIndex = 38;
            this.lblMatnr.Text = "Material";
            this.lblMatnr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMatnr
            // 
            this.txtMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMatnr.Enabled = false;
            this.txtMatnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatnr.Location = new System.Drawing.Point(318, 35);
            this.txtMatnr.MaxLength = 100;
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(74, 22);
            this.txtMatnr.TabIndex = 37;
            this.txtMatnr.TextChanged += new System.EventHandler(this.txtMatnr_TextChanged);
            // 
            // lblArbpl
            // 
            this.lblArbpl.Location = new System.Drawing.Point(191, 3);
            this.lblArbpl.Name = "lblArbpl";
            this.lblArbpl.Size = new System.Drawing.Size(59, 23);
            this.lblArbpl.TabIndex = 36;
            this.lblArbpl.Text = "PD Line";
            this.lblArbpl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbArbpl
            // 
            this.cmbArbpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArbpl.Enabled = false;
            this.cmbArbpl.ItemHeight = 16;
            this.cmbArbpl.Location = new System.Drawing.Point(256, 4);
            this.cmbArbpl.Name = "cmbArbpl";
            this.cmbArbpl.Size = new System.Drawing.Size(136, 24);
            this.cmbArbpl.TabIndex = 35;
            this.cmbArbpl.SelectedIndexChanged += new System.EventHandler(this.cmbArbpl_SelectedIndexChanged);
            // 
            // txtMblnr
            // 
            this.txtMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMblnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMblnr.Location = new System.Drawing.Point(78, 68);
            this.txtMblnr.MaxLength = 100;
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(172, 22);
            this.txtMblnr.TabIndex = 33;
            this.txtMblnr.DoubleClick += new System.EventHandler(this.txtMblnr_DoubleClick);
            // 
            // lblMblnr
            // 
            this.lblMblnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMblnr.Location = new System.Drawing.Point(3, 66);
            this.lblMblnr.Name = "lblMblnr";
            this.lblMblnr.Size = new System.Drawing.Size(72, 23);
            this.lblMblnr.TabIndex = 32;
            this.lblMblnr.Text = "Document";
            this.lblMblnr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(253, 63);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(65, 32);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblQwms
            // 
            this.lblQwms.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQwms.Location = new System.Drawing.Point(2, 3);
            this.lblQwms.Name = "lblQwms";
            this.lblQwms.Size = new System.Drawing.Size(91, 23);
            this.lblQwms.TabIndex = 31;
            this.lblQwms.Text = "QWMS/ASRS";
            this.lblQwms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbQwms
            // 
            this.cmbQwms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQwms.Enabled = false;
            this.cmbQwms.ItemHeight = 16;
            this.cmbQwms.Items.AddRange(new object[] {
            "QWMS",
            "ASRS"});
            this.cmbQwms.Location = new System.Drawing.Point(93, 3);
            this.cmbQwms.Name = "cmbQwms";
            this.cmbQwms.Size = new System.Drawing.Size(92, 24);
            this.cmbQwms.TabIndex = 30;
            this.cmbQwms.SelectedIndexChanged += new System.EventHandler(this.cmbQwms_SelectedIndexChanged);
            // 
            // lblGrpid
            // 
            this.lblGrpid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGrpid.Location = new System.Drawing.Point(3, 33);
            this.lblGrpid.Name = "lblGrpid";
            this.lblGrpid.Size = new System.Drawing.Size(59, 23);
            this.lblGrpid.TabIndex = 28;
            this.lblGrpid.Text = "Send ID";
            this.lblGrpid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbGrpid
            // 
            this.cmbGrpid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrpid.Enabled = false;
            this.cmbGrpid.ItemHeight = 16;
            this.cmbGrpid.Location = new System.Drawing.Point(78, 34);
            this.cmbGrpid.Name = "cmbGrpid";
            this.cmbGrpid.Size = new System.Drawing.Size(172, 24);
            this.cmbGrpid.TabIndex = 29;
            this.cmbGrpid.SelectedIndexChanged += new System.EventHandler(this.cmbGrpid_SelectedIndexChanged);
            this.cmbGrpid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbGrpid_MouseMove);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblType);
            this.panel5.Controls.Add(this.cmbLgort);
            this.panel5.Controls.Add(this.cmbType);
            this.panel5.Controls.Add(this.lblWerks);
            this.panel5.Controls.Add(this.cmbWerks);
            this.panel5.Controls.Add(this.lblLgort);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Enabled = false;
            this.panel5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(109, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(583, 96);
            this.panel5.TabIndex = 12;
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblType.Location = new System.Drawing.Point(4, 65);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(81, 23);
            this.lblType.TabIndex = 15;
            this.lblType.Text = "SMT/FINAL";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLgort.ItemHeight = 16;
            this.cmbLgort.Location = new System.Drawing.Point(86, 32);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(94, 24);
            this.cmbLgort.TabIndex = 12;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            this.cmbLgort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmbLgort_MouseMove);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.ItemHeight = 16;
            this.cmbType.Items.AddRange(new object[] {
            "SMT",
            "FINAL"});
            this.cmbType.Location = new System.Drawing.Point(86, 65);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(94, 24);
            this.cmbType.TabIndex = 14;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblLgort
            // 
            this.lblLgort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLgort.Location = new System.Drawing.Point(4, 34);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(62, 23);
            this.lblLgort.TabIndex = 13;
            this.lblLgort.Text = "Storage";
            this.lblLgort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dtpCrdat);
            this.panel4.Controls.Add(this.gbFunction);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(109, 96);
            this.panel4.TabIndex = 11;
            // 
            // dtpCrdat
            // 
            this.dtpCrdat.CalendarFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpCrdat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCrdat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCrdat.Location = new System.Drawing.Point(6, 6);
            this.dtpCrdat.Name = "dtpCrdat";
            this.dtpCrdat.Size = new System.Drawing.Size(96, 22);
            this.dtpCrdat.TabIndex = 26;
            this.dtpCrdat.ValueChanged += new System.EventHandler(this.dtpCrdat_ValueChanged);
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.rdoAdd);
            this.gbFunction.Controls.Add(this.rdoNormal);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(6, 26);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(97, 64);
            this.gbFunction.TabIndex = 1;
            this.gbFunction.TabStop = false;
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoAdd.Location = new System.Drawing.Point(6, 38);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(69, 24);
            this.rdoAdd.TabIndex = 1;
            this.rdoAdd.Text = "加扣";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoNormal
            // 
            this.rdoNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoNormal.Location = new System.Drawing.Point(6, 12);
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size(80, 24);
            this.rdoNormal.TabIndex = 0;
            this.rdoNormal.Text = "ID 出庫";
            this.rdoNormal.CheckedChanged += new System.EventHandler(this.rdoNormal_CheckedChanged);
            // 
            // StorageOut_OnLineOut_SimulationOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "StorageOut_OnLineOut_SimulationOut";
            this.Text = "StorageOut_OnLineOut_SimulationOut (PowerⅡ-QWMS)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageOut_OnLineOut_SimulationOut_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.gbFunction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblOutSource;
        private System.Windows.Forms.DataGridView dgvOutSource;
        private System.Windows.Forms.Label lblWerks;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvStorage;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.ComboBox cmbGrpid;
        private System.Windows.Forms.Label lblGrpid;
        private System.Windows.Forms.Label lblQwms;
        private System.Windows.Forms.ComboBox cmbQwms;
        private System.Windows.Forms.Label lblMblnr;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoAdd;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Label lblArbpl;
        private System.Windows.Forms.ComboBox cmbArbpl;
        private System.Windows.Forms.Label lblMatnr;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.DateTimePicker dtpCrdat;
        private System.Windows.Forms.Button btnProduce;
        private System.Windows.Forms.CheckBox chkDateCode;
        private System.Windows.Forms.CheckBox chkAllId;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnDacodPrint;
    }
}