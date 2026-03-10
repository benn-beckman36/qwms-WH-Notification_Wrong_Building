namespace QWMS
{
    partial class StorageIn_OnLineIn_SpareParts_Hub
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gbHeaderRight = new System.Windows.Forms.GroupBox();
            this.rdoKdmat = new System.Windows.Forms.RadioButton();
            this.rdoEbeln = new System.Windows.Forms.RadioButton();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxid = new System.Windows.Forms.TextBox();
            this.dtpIndat = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.txtKdmat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEbeln = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCharg = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAddIn = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkClearAll = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.cmbLocat = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.gbHeaderRight.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Controls.Add(this.cmbLgort);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(692, 144);
            this.panel4.TabIndex = 31;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.gbHeaderRight);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(488, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(204, 144);
            this.panel6.TabIndex = 30;
            // 
            // gbHeaderRight
            // 
            this.gbHeaderRight.Controls.Add(this.rdoKdmat);
            this.gbHeaderRight.Controls.Add(this.rdoEbeln);
            this.gbHeaderRight.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeaderRight.Location = new System.Drawing.Point(9, 12);
            this.gbHeaderRight.Name = "gbHeaderRight";
            this.gbHeaderRight.Size = new System.Drawing.Size(192, 56);
            this.gbHeaderRight.TabIndex = 30;
            this.gbHeaderRight.TabStop = false;
            this.gbHeaderRight.Text = "Order by";
            // 
            // rdoKdmat
            // 
            this.rdoKdmat.Location = new System.Drawing.Point(73, 24);
            this.rdoKdmat.Name = "rdoKdmat";
            this.rdoKdmat.Size = new System.Drawing.Size(115, 24);
            this.rdoKdmat.TabIndex = 1;
            this.rdoKdmat.Text = "Customer P/N";
            // 
            // rdoEbeln
            // 
            this.rdoEbeln.Checked = true;
            this.rdoEbeln.Location = new System.Drawing.Point(6, 24);
            this.rdoEbeln.Name = "rdoEbeln";
            this.rdoEbeln.Size = new System.Drawing.Size(82, 24);
            this.rdoEbeln.TabIndex = 0;
            this.rdoEbeln.TabStop = true;
            this.rdoEbeln.Text = "PO No";
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.label4);
            this.gbHeader.Controls.Add(this.txtBoxid);
            this.gbHeader.Controls.Add(this.dtpIndat);
            this.gbHeader.Controls.Add(this.btnQuery);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(20, 12);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(462, 97);
            this.gbHeader.TabIndex = 29;
            this.gbHeader.TabStop = false;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(88, 25);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(118, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.Location = new System.Drawing.Point(273, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "In Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxid
            // 
            this.txtBoxid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxid.Location = new System.Drawing.Point(88, 55);
            this.txtBoxid.MaxLength = 30;
            this.txtBoxid.Name = "txtBoxid";
            this.txtBoxid.Size = new System.Drawing.Size(157, 22);
            this.txtBoxid.TabIndex = 4;
            // 
            // dtpIndat
            // 
            this.dtpIndat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dtpIndat.CalendarFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpIndat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIndat.Location = new System.Drawing.Point(353, 22);
            this.dtpIndat.Name = "dtpIndat";
            this.dtpIndat.Size = new System.Drawing.Size(96, 22);
            this.dtpIndat.TabIndex = 5;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(353, 55);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 32);
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(16, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Box id";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Enabled = false;
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLgort.Location = new System.Drawing.Point(147, 115);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(118, 24);
            this.cmbLgort.TabIndex = 3;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transfer Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 17);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 41;
            this.statusStrip1.Text = "statusStrip1";
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
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(21, 204);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(650, 197);
            this.dgvData.TabIndex = 11;
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(21, 147);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(50, 14);
            this.lblCount.TabIndex = 33;
            this.lblCount.Text = "0 records";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(182, 407);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 144);
            this.panel1.TabIndex = 38;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(101, 407);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 32);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnModify);
            this.panel2.Controls.Add(this.txtKdmat);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtEbeln);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtCharg);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.chkAddIn);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.chkClearAll);
            this.panel2.Controls.Add(this.chkSelectAll);
            this.panel2.Controls.Add(this.cmbLocat);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 473);
            this.panel2.TabIndex = 39;
            // 
            // btnModify
            // 
            this.btnModify.Enabled = false;
            this.btnModify.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.Location = new System.Drawing.Point(596, 170);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 32);
            this.btnModify.TabIndex = 58;
            this.btnModify.Text = "Modify";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // txtKdmat
            // 
            this.txtKdmat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKdmat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKdmat.Location = new System.Drawing.Point(373, 145);
            this.txtKdmat.MaxLength = 30;
            this.txtKdmat.Name = "txtKdmat";
            this.txtKdmat.Size = new System.Drawing.Size(118, 22);
            this.txtKdmat.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(266, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 23);
            this.label8.TabIndex = 56;
            this.label8.Text = "Customer P/N";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEbeln
            // 
            this.txtEbeln.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEbeln.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEbeln.Location = new System.Drawing.Point(147, 145);
            this.txtEbeln.MaxLength = 30;
            this.txtEbeln.Name = "txtEbeln";
            this.txtEbeln.Size = new System.Drawing.Size(118, 22);
            this.txtEbeln.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(77, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 23);
            this.label7.TabIndex = 54;
            this.label7.Text = "PO No.";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCharg
            // 
            this.txtCharg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCharg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCharg.Location = new System.Drawing.Point(567, 146);
            this.txtCharg.MaxLength = 30;
            this.txtCharg.Name = "txtCharg";
            this.txtCharg.Size = new System.Drawing.Size(104, 22);
            this.txtCharg.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(495, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 52;
            this.label6.Text = "Version";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkAddIn
            // 
            this.chkAddIn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddIn.Location = new System.Drawing.Point(455, 173);
            this.chkAddIn.Name = "chkAddIn";
            this.chkAddIn.Size = new System.Drawing.Size(92, 24);
            this.chkAddIn.TabIndex = 51;
            this.chkAddIn.Text = "Add In";
            this.chkAddIn.CheckedChanged += new System.EventHandler(this.chkAddIn_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(213, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 23);
            this.label5.TabIndex = 50;
            this.label5.Text = "Storage Location";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkClearAll
            // 
            this.chkClearAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClearAll.Location = new System.Drawing.Point(115, 173);
            this.chkClearAll.Name = "chkClearAll";
            this.chkClearAll.Size = new System.Drawing.Size(92, 24);
            this.chkClearAll.TabIndex = 49;
            this.chkClearAll.Text = "Clear All";
            this.chkClearAll.CheckedChanged += new System.EventHandler(this.chkClearAll_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.Location = new System.Drawing.Point(21, 174);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(92, 24);
            this.chkSelectAll.TabIndex = 48;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.CheckStateChanged += new System.EventHandler(this.chkSelectAll_CheckStateChanged);
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // cmbLocat
            // 
            this.cmbLocat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocat.ItemHeight = 16;
            this.cmbLocat.Location = new System.Drawing.Point(345, 173);
            this.cmbLocat.Name = "cmbLocat";
            this.cmbLocat.Size = new System.Drawing.Size(104, 24);
            this.cmbLocat.TabIndex = 47;
            this.cmbLocat.SelectedIndexChanged += new System.EventHandler(this.cmbLocat_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(20, 407);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // StorageIn_OnLineIn_SpareParts_Hub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "StorageIn_OnLineIn_SpareParts_Hub";
            this.Text = "StorageIn_OnLineIn_SpareParts_Hub";
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.gbHeaderRight.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxid;
        private System.Windows.Forms.DateTimePicker dtpIndat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkClearAll;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ComboBox cmbLocat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAddIn;
        private System.Windows.Forms.GroupBox gbHeaderRight;
        private System.Windows.Forms.RadioButton rdoKdmat;
        private System.Windows.Forms.RadioButton rdoEbeln;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtKdmat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEbeln;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCharg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnModify;
    }
}