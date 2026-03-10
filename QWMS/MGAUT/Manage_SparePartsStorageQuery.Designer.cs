namespace QWMS
{
    partial class Manage_SparePartsStorageQuery
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
            this.dtgData = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbPrint = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rdoDetail = new System.Windows.Forms.RadioButton();
            this.rdoSummary = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNloca = new System.Windows.Forms.TextBox();
            this.txtKdmat = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEbeln = new System.Windows.Forms.TextBox();
            this.txtStartMatnr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndMatnr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSidno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbStock = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDataDetail = new System.Windows.Forms.Label();
            this.dtgDataDetail = new System.Windows.Forms.DataGridView();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.txtRmano = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.panel4.SuspendLayout();
            this.gbPrint.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbStock.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            this.SuspendLayout();
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(8, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(64, 18);
            this.lblData.TabIndex = 4;
            this.lblData.Text = "0 records";
            // 
            // dtgData
            // 
            this.dtgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgData.Location = new System.Drawing.Point(8, 21);
            this.dtgData.Name = "dtgData";
            this.dtgData.RowTemplate.Height = 24;
            this.dtgData.Size = new System.Drawing.Size(322, 105);
            this.dtgData.TabIndex = 0;
            this.dtgData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgData_MouseDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.btnRefresh);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.gbPrint);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 349);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(336, 102);
            this.panel4.TabIndex = 27;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(8, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(8, 35);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 32);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(8, 67);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 32);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gbPrint
            // 
            this.gbPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPrint.Controls.Add(this.btnDownload);
            this.gbPrint.Controls.Add(this.btnPrint);
            this.gbPrint.Controls.Add(this.rdoDetail);
            this.gbPrint.Controls.Add(this.rdoSummary);
            this.gbPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPrint.Location = new System.Drawing.Point(88, 3);
            this.gbPrint.Name = "gbPrint";
            this.gbPrint.Size = new System.Drawing.Size(240, 88);
            this.gbPrint.TabIndex = 24;
            this.gbPrint.TabStop = false;
            this.gbPrint.Text = "Print";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(152, 56);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(80, 25);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(152, 24);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 26);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rdoDetail
            // 
            this.rdoDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoDetail.Location = new System.Drawing.Point(8, 44);
            this.rdoDetail.Name = "rdoDetail";
            this.rdoDetail.Size = new System.Drawing.Size(112, 23);
            this.rdoDetail.TabIndex = 1;
            this.rdoDetail.Text = "Detail Report";
            this.rdoDetail.CheckedChanged += new System.EventHandler(this.rdoDetail_CheckedChanged);
            // 
            // rdoSummary
            // 
            this.rdoSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoSummary.Location = new System.Drawing.Point(8, 20);
            this.rdoSummary.Name = "rdoSummary";
            this.rdoSummary.Size = new System.Drawing.Size(136, 24);
            this.rdoSummary.TabIndex = 0;
            this.rdoSummary.Text = "Summary Report";
            this.rdoSummary.CheckedChanged += new System.EventHandler(this.rdoSummery_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtRmano);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.txtNloca);
            this.panel6.Controls.Add(this.txtKdmat);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.txtEbeln);
            this.panel6.Controls.Add(this.txtStartMatnr);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.txtEndMatnr);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.txtSidno);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(136, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 217);
            this.panel6.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(-5, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 34);
            this.label9.TabIndex = 24;
            this.label9.Text = "Shipment Location";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNloca
            // 
            this.txtNloca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNloca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNloca.Location = new System.Drawing.Point(64, 161);
            this.txtNloca.Name = "txtNloca";
            this.txtNloca.Size = new System.Drawing.Size(128, 21);
            this.txtNloca.TabIndex = 26;
            // 
            // txtKdmat
            // 
            this.txtKdmat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKdmat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKdmat.Location = new System.Drawing.Point(64, 130);
            this.txtKdmat.Name = "txtKdmat";
            this.txtKdmat.Size = new System.Drawing.Size(128, 21);
            this.txtKdmat.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "SI No.";
            // 
            // txtEbeln
            // 
            this.txtEbeln.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEbeln.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEbeln.Location = new System.Drawing.Point(64, 101);
            this.txtEbeln.Name = "txtEbeln";
            this.txtEbeln.Size = new System.Drawing.Size(128, 21);
            this.txtEbeln.TabIndex = 5;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMatnr.Location = new System.Drawing.Point(64, 12);
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(128, 21);
            this.txtStartMatnr.TabIndex = 3;
            this.txtStartMatnr.DoubleClick += new System.EventHandler(this.txtStartMatnr_DoubleClick);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "P/N From";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMatnr.Location = new System.Drawing.Point(64, 44);
            this.txtEndMatnr.MaxLength = 20;
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(128, 21);
            this.txtEndMatnr.TabIndex = 4;
            this.txtEndMatnr.DoubleClick += new System.EventHandler(this.txtEndMatnr_DoubleClick);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 21;
            this.label6.Text = "PO No.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSidno
            // 
            this.txtSidno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSidno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSidno.Location = new System.Drawing.Point(64, 71);
            this.txtSidno.Name = "txtSidno";
            this.txtSidno.Size = new System.Drawing.Size(128, 21);
            this.txtSidno.TabIndex = 22;
            this.txtSidno.DoubleClick += new System.EventHandler(this.txtSino_DoubleClick);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(-5, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 34);
            this.label7.TabIndex = 23;
            this.label7.Text = "Customer P/N";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblData);
            this.panel3.Controls.Add(this.dtgData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 217);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(336, 234);
            this.panel3.TabIndex = 26;
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.gbStock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 451);
            this.panel1.TabIndex = 32;
            // 
            // gbStock
            // 
            this.gbStock.Controls.Add(this.panel6);
            this.gbStock.Controls.Add(this.panel5);
            this.gbStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbStock.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStock.Location = new System.Drawing.Point(0, 0);
            this.gbStock.Name = "gbStock";
            this.gbStock.Size = new System.Drawing.Size(336, 217);
            this.gbStock.TabIndex = 25;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnConfirm);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.cmbWerks);
            this.panel5.Controls.Add(this.cmbLgort);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(136, 217);
            this.panel5.TabIndex = 21;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(53, 92);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 32);
            this.btnConfirm.TabIndex = 15;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(64, 20);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(64, 23);
            this.cmbWerks.TabIndex = 0;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(64, 52);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(64, 23);
            this.cmbLgort.TabIndex = 0;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(761, 451);
            this.panel2.TabIndex = 33;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblDataDetail);
            this.groupBox2.Controls.Add(this.dtgDataDetail);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(352, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 427);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location Detail";
            // 
            // lblDataDetail
            // 
            this.lblDataDetail.AutoSize = true;
            this.lblDataDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataDetail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataDetail.Location = new System.Drawing.Point(7, 18);
            this.lblDataDetail.Name = "lblDataDetail";
            this.lblDataDetail.Size = new System.Drawing.Size(64, 18);
            this.lblDataDetail.TabIndex = 5;
            this.lblDataDetail.Text = "0 records";
            // 
            // dtgDataDetail
            // 
            this.dtgDataDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDataDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDataDetail.Location = new System.Drawing.Point(7, 40);
            this.dtgDataDetail.Name = "dtgDataDetail";
            this.dtgDataDetail.RowTemplate.Height = 24;
            this.dtgDataDetail.Size = new System.Drawing.Size(384, 380);
            this.dtgDataDetail.TabIndex = 0;
            this.dtgDataDetail.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgDataDetail_RowHeaderMouseClick);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 451);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(761, 22);
            this.stbStatus.TabIndex = 31;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // txtRmano
            // 
            this.txtRmano.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRmano.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRmano.Location = new System.Drawing.Point(64, 188);
            this.txtRmano.Name = "txtRmano";
            this.txtRmano.Size = new System.Drawing.Size(128, 21);
            this.txtRmano.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 23);
            this.label5.TabIndex = 28;
            this.label5.Text = "RMA No.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Manage_SparePartsStorageQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stbStatus);
            this.Name = "Manage_SparePartsStorageQuery";
            this.Text = "Manage_SparePartsStorageQuery";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.panel4.ResumeLayout(false);
            this.gbPrint.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbStock.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DataGridView dtgData;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox gbPrint;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdoDetail;
        private System.Windows.Forms.RadioButton rdoSummary;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtNloca;
        private System.Windows.Forms.TextBox txtKdmat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEbeln;
        private System.Windows.Forms.TextBox txtStartMatnr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndMatnr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSidno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel gbStock;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDataDetail;
        private System.Windows.Forms.DataGridView dtgDataDetail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRmano;
        private System.Windows.Forms.Label label5;
    }
}