namespace QWMS.Models
{
    partial class Model_InventoryCheck_StorageQuery
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
            System.Windows.Forms.Panel panel1;
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkIsCombine = new System.Windows.Forms.CheckBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.txtVendorCode = new System.Windows.Forms.TextBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.txtStartMatnr = new System.Windows.Forms.TextBox();
            this.txtStartLocat = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.txtEndMatnr = new System.Windows.Forms.TextBox();
            this.txtEndLocat = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbInsmk = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnLabelPrint = new System.Windows.Forms.Button();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCount = new System.Windows.Forms.Label();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExit = new System.Windows.Forms.Button();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(this.panel5);
            panel1.Controls.Add(this.panel3);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(824, 119);
            panel1.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkIsCombine);
            this.panel5.Controls.Add(this.chkDate);
            this.panel5.Controls.Add(this.txtVendorCode);
            this.panel5.Controls.Add(this.dtpStartDate);
            this.panel5.Controls.Add(this.txtMblnr);
            this.panel5.Controls.Add(this.txtStartMatnr);
            this.panel5.Controls.Add(this.txtStartLocat);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.btnConfirm);
            this.panel5.Controls.Add(this.dtpEndDate);
            this.panel5.Controls.Add(this.txtEndMatnr);
            this.panel5.Controls.Add(this.txtEndLocat);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(200, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(624, 119);
            this.panel5.TabIndex = 2;
            // 
            // chkIsCombine
            // 
            this.chkIsCombine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkIsCombine.AutoSize = true;
            this.chkIsCombine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsCombine.Location = new System.Drawing.Point(428, 14);
            this.chkIsCombine.Name = "chkIsCombine";
            this.chkIsCombine.Size = new System.Drawing.Size(113, 19);
            this.chkIsCombine.TabIndex = 29;
            this.chkIsCombine.Text = "Combine Model";
            this.chkIsCombine.UseVisualStyleBackColor = true;
            this.chkIsCombine.Visible = false;
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Location = new System.Drawing.Point(19, 59);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(15, 14);
            this.chkDate.TabIndex = 28;
            this.chkDate.UseVisualStyleBackColor = true;
            // 
            // txtVendorCode
            // 
            this.txtVendorCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVendorCode.Location = new System.Drawing.Point(323, 80);
            this.txtVendorCode.Name = "txtVendorCode";
            this.txtVendorCode.Size = new System.Drawing.Size(118, 21);
            this.txtVendorCode.TabIndex = 26;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(117, 56);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(97, 21);
            this.dtpStartDate.TabIndex = 27;
            // 
            // txtMblnr
            // 
            this.txtMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMblnr.Location = new System.Drawing.Point(116, 80);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(98, 21);
            this.txtMblnr.TabIndex = 25;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMatnr.Location = new System.Drawing.Point(117, 33);
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(97, 21);
            this.txtStartMatnr.TabIndex = 24;
            // 
            // txtStartLocat
            // 
            this.txtStartLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartLocat.Location = new System.Drawing.Point(117, 8);
            this.txtStartLocat.Name = "txtStartLocat";
            this.txtStartLocat.Size = new System.Drawing.Size(97, 21);
            this.txtStartLocat.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(242, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 15);
            this.label13.TabIndex = 20;
            this.label13.Text = "Vendor Code";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Doucment No";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "InDate From";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Model No. From";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "Location From";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnConfirm.Location = new System.Drawing.Point(478, 65);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 34);
            this.btnConfirm.TabIndex = 16;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(284, 56);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(112, 21);
            this.dtpEndDate.TabIndex = 13;
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEndMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMatnr.Location = new System.Drawing.Point(284, 33);
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(112, 21);
            this.txtEndMatnr.TabIndex = 10;
            // 
            // txtEndLocat
            // 
            this.txtEndLocat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEndLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndLocat.Location = new System.Drawing.Point(284, 8);
            this.txtEndLocat.Name = "txtEndLocat";
            this.txtEndLocat.Size = new System.Drawing.Size(112, 21);
            this.txtEndLocat.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(253, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "To";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(253, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 15);
            this.label10.TabIndex = 2;
            this.label10.Text = "To";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(253, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "To";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbInsmk);
            this.panel3.Controls.Add(this.cmbLgort);
            this.panel3.Controls.Add(this.cmbWerks);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 119);
            this.panel3.TabIndex = 0;
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk.FormattingEnabled = true;
            this.cmbInsmk.Location = new System.Drawing.Point(70, 57);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(120, 23);
            this.cmbInsmk.TabIndex = 6;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(70, 33);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(120, 23);
            this.cmbLgort.TabIndex = 5;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(70, 9);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(120, 23);
            this.cmbWerks.TabIndex = 4;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Stock";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 17);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(796, 178);
            this.dgvData.TabIndex = 0;
            // 
            // btnLabelPrint
            // 
            this.btnLabelPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLabelPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLabelPrint.Location = new System.Drawing.Point(536, 200);
            this.btnLabelPrint.Name = "btnLabelPrint";
            this.btnLabelPrint.Size = new System.Drawing.Size(89, 33);
            this.btnLabelPrint.TabIndex = 46;
            this.btnLabelPrint.Text = "Print Label";
            this.btnLabelPrint.UseVisualStyleBackColor = true;
            this.btnLabelPrint.Click += new System.EventHandler(this.btnLabelPrint_Click);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 17);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.Location = new System.Drawing.Point(16, -1);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 15);
            this.lblCount.TabIndex = 45;
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 17);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(430, 200);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 34);
            this.btnExit.TabIndex = 44;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 17);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(176, 200);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 34);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 17);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDownload.Location = new System.Drawing.Point(298, 200);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(89, 34);
            this.btnDownload.TabIndex = 43;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLabelPrint);
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.statusStrip2);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 259);
            this.panel2.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(59, 200);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 34);
            this.btnPrint.TabIndex = 41;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip2.Location = new System.Drawing.Point(0, 237);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(824, 22);
            this.statusStrip2.TabIndex = 40;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 17);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "LocationStorageDetail.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // Model_InventoryCheck_StorageQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 378);
            this.Controls.Add(this.panel2);
            this.Controls.Add(panel1);
            this.Name = "Model_InventoryCheck_StorageQuery";
            this.ShowIcon = false;
            this.Text = "Model_InventoryCheck_StorageQuery";
            panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbInsmk;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLabelPrint;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtVendorCode;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.TextBox txtStartMatnr;
        private System.Windows.Forms.TextBox txtStartLocat;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.TextBox txtEndMatnr;
        private System.Windows.Forms.TextBox txtEndLocat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.CheckBox chkIsCombine;
    }
}