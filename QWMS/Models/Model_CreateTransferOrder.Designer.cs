namespace QWMS.Models
{
    partial class Model_CreateTransferOrder
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtaryMatnr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbToLGORT = new System.Windows.Forms.ComboBox();
            this.cmbToWerks = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // txtaryMatnr
            // 
            this.txtaryMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtaryMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtaryMatnr.Location = new System.Drawing.Point(92, 83);
            this.txtaryMatnr.Margin = new System.Windows.Forms.Padding(2);
            this.txtaryMatnr.MaxLength = 10;
            this.txtaryMatnr.Name = "txtaryMatnr";
            this.txtaryMatnr.Size = new System.Drawing.Size(224, 26);
            this.txtaryMatnr.TabIndex = 8;
            this.txtaryMatnr.DoubleClick += new System.EventHandler(this.txtaryMatnr_DoubleClick);
            this.txtaryMatnr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtaryMatnr_MouseMove);
            this.txtaryMatnr.MouseEnter += new System.EventHandler(this.txtaryMatnr_MouseEnter);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(19, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "模号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(92, 19);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(2);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(224, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(4, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(250, 79);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(92, 52);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(224, 24);
            this.cmbLgort.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 161);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(686, 375);
            this.panel2.TabIndex = 34;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(122, 339);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 31);
            this.btnPrint.TabIndex = 33;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(25, 3);
            this.lblCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(61, 14);
            this.lblCount.TabIndex = 32;
            this.lblCount.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvData.Location = new System.Drawing.Point(28, 19);
            this.dgvData.Margin = new System.Windows.Forms.Padding(2);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(652, 315);
            this.dgvData.TabIndex = 11;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(311, 339);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 31);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(217, 339);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 31);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(28, 339);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 31);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(686, 161);
            this.panel4.TabIndex = 31;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbToLGORT);
            this.groupBox2.Controls.Add(this.cmbToWerks);
            this.groupBox2.Controls.Add(this.btnConfirm);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(333, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(344, 147);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TO";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(87, 83);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(2);
            this.txtRemark.MaxLength = 50;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(146, 26);
            this.txtRemark.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Location = new System.Drawing.Point(16, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 22);
            this.label6.TabIndex = 11;
            this.label6.Text = "备注";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(4, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Plant";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(8, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 22);
            this.label5.TabIndex = 9;
            this.label5.Text = "Storage";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbToLGORT
            // 
            this.cmbToLGORT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbToLGORT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToLGORT.Location = new System.Drawing.Point(87, 50);
            this.cmbToLGORT.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToLGORT.Name = "cmbToLGORT";
            this.cmbToLGORT.Size = new System.Drawing.Size(253, 24);
            this.cmbToLGORT.TabIndex = 10;
            // 
            // cmbToWerks
            // 
            this.cmbToWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbToWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToWerks.Location = new System.Drawing.Point(87, 17);
            this.cmbToWerks.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToWerks.Name = "cmbToWerks";
            this.cmbToWerks.Size = new System.Drawing.Size(253, 24);
            this.cmbToWerks.TabIndex = 8;
            this.cmbToWerks.SelectedIndexChanged += new System.EventHandler(this.cmbToWerks_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFile);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.lnkSample);
            this.groupBox1.Controls.Add(this.txtaryMatnr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbLgort);
            this.groupBox1.Controls.Add(this.cmbWerks);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(320, 147);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 161);
            this.panel1.TabIndex = 33;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 536);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(686, 21);
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
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(5, 120);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(96, 25);
            this.lnkSample.TabIndex = 58;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "File Path:";
            this.lnkSample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSample_LinkClicked);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(92, 114);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(166, 26);
            this.txtFilePath.TabIndex = 60;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFile.Location = new System.Drawing.Point(264, 114);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(51, 26);
            this.btnFile.TabIndex = 61;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "InventoryComparsion.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*";
            // 
            // Model_CreateTransferOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 557);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Model_CreateTransferOrder";
            this.Text = "开立调拨单";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.TextBox txtaryMatnr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbToLGORT;
        private System.Windows.Forms.ComboBox cmbToWerks;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel lnkSample;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;

    }
}