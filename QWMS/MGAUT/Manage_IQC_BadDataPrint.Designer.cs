namespace QWMS
{
    partial class Manage_IQC_BadDataPrint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbLgort = new System.Windows.Forms.ComboBox();
            this.cbWerks = new System.Windows.Forms.ComboBox();
            this.labelLgort = new System.Windows.Forms.Label();
            this.labelStorage = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.txtGRno = new System.Windows.Forms.TextBox();
            this.labelOrderNo = new System.Windows.Forms.Label();
            this.ckPrintAgain = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dgvGridData = new System.Windows.Forms.DataGridView();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.gbPrintMachine = new System.Windows.Forms.GroupBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblCom = new System.Windows.Forms.Label();
            this.txtBounnd = new System.Windows.Forms.TextBox();
            this.lblBounnd = new System.Windows.Forms.Label();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.btSetting = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkCombine = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.lbrecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridData)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.gbPrintMachine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLgort
            // 
            this.cbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLgort.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLgort.FormattingEnabled = true;
            this.cbLgort.Location = new System.Drawing.Point(95, 81);
            this.cbLgort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbLgort.Name = "cbLgort";
            this.cbLgort.Size = new System.Drawing.Size(211, 28);
            this.cbLgort.TabIndex = 16;
            // 
            // cbWerks
            // 
            this.cbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWerks.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbWerks.FormattingEnabled = true;
            this.cbWerks.Location = new System.Drawing.Point(95, 24);
            this.cbWerks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbWerks.Name = "cbWerks";
            this.cbWerks.Size = new System.Drawing.Size(211, 28);
            this.cbWerks.TabIndex = 15;
            this.cbWerks.SelectedIndexChanged += new System.EventHandler(this.cbWerks_SelectedIndexChanged);
            // 
            // labelLgort
            // 
            this.labelLgort.AutoSize = true;
            this.labelLgort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLgort.Location = new System.Drawing.Point(28, 85);
            this.labelLgort.Name = "labelLgort";
            this.labelLgort.Size = new System.Drawing.Size(53, 18);
            this.labelLgort.TabIndex = 14;
            this.labelLgort.Text = "仓别:";
            // 
            // labelStorage
            // 
            this.labelStorage.AutoSize = true;
            this.labelStorage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStorage.Location = new System.Drawing.Point(28, 28);
            this.labelStorage.Name = "labelStorage";
            this.labelStorage.Size = new System.Drawing.Size(53, 18);
            this.labelStorage.TabIndex = 13;
            this.labelStorage.Text = "厂区:";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTo.Location = new System.Drawing.Point(957, 25);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(26, 18);
            this.labelTo.TabIndex = 23;
            this.labelTo.Text = "TO";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(985, 20);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(160, 28);
            this.dtpEnd.TabIndex = 22;
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(789, 20);
            this.dtpBegin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(160, 28);
            this.dtpBegin.TabIndex = 21;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDate.Location = new System.Drawing.Point(731, 25);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(44, 18);
            this.labelDate.TabIndex = 20;
            this.labelDate.Text = "日期";
            // 
            // txtGRno
            // 
            this.txtGRno.Location = new System.Drawing.Point(443, 80);
            this.txtGRno.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGRno.Name = "txtGRno";
            this.txtGRno.Size = new System.Drawing.Size(188, 28);
            this.txtGRno.TabIndex = 25;
            // 
            // labelOrderNo
            // 
            this.labelOrderNo.AutoSize = true;
            this.labelOrderNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderNo.Location = new System.Drawing.Point(359, 85);
            this.labelOrderNo.Name = "labelOrderNo";
            this.labelOrderNo.Size = new System.Drawing.Size(62, 18);
            this.labelOrderNo.TabIndex = 24;
            this.labelOrderNo.Text = "GR单号";
            // 
            // ckPrintAgain
            // 
            this.ckPrintAgain.AutoSize = true;
            this.ckPrintAgain.Location = new System.Drawing.Point(95, 201);
            this.ckPrintAgain.Margin = new System.Windows.Forms.Padding(4);
            this.ckPrintAgain.Name = "ckPrintAgain";
            this.ckPrintAgain.Size = new System.Drawing.Size(70, 22);
            this.ckPrintAgain.TabIndex = 41;
            this.ckPrintAgain.Text = "补印";
            this.ckPrintAgain.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1114, 201);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 35);
            this.btnRefresh.TabIndex = 40;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(734, 201);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(160, 35);
            this.btnQuery.TabIndex = 39;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(12, 33);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(165, 35);
            this.btnPrint.TabIndex = 42;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dgvGridData
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGridData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGridData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGridData.Location = new System.Drawing.Point(0, 0);
            this.dgvGridData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvGridData.Name = "dgvGridData";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGridData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGridData.RowTemplate.Height = 27;
            this.dgvGridData.Size = new System.Drawing.Size(1304, 332);
            this.dgvGridData.TabIndex = 46;
            // 
            // cmsMenu
            // 
            this.cmsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSplit});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(117, 32);
            // 
            // tsmiSplit
            // 
            this.tsmiSplit.Name = "tsmiSplit";
            this.tsmiSplit.Size = new System.Drawing.Size(116, 28);
            this.tsmiSplit.Text = "拆分";
            this.tsmiSplit.Click += new System.EventHandler(this.tsmiSplit_Click);
            // 
            // gbPrintMachine
            // 
            this.gbPrintMachine.Controls.Add(this.lblIP);
            this.gbPrintMachine.Controls.Add(this.txtIP);
            this.gbPrintMachine.Controls.Add(this.lblCom);
            this.gbPrintMachine.Controls.Add(this.txtBounnd);
            this.gbPrintMachine.Controls.Add(this.lblBounnd);
            this.gbPrintMachine.Controls.Add(this.txtCom);
            this.gbPrintMachine.Controls.Add(this.btSetting);
            this.gbPrintMachine.Font = new System.Drawing.Font("宋体", 9F);
            this.gbPrintMachine.Location = new System.Drawing.Point(734, 55);
            this.gbPrintMachine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbPrintMachine.Name = "gbPrintMachine";
            this.gbPrintMachine.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbPrintMachine.Size = new System.Drawing.Size(540, 138);
            this.gbPrintMachine.TabIndex = 46;
            this.gbPrintMachine.TabStop = false;
            this.gbPrintMachine.Text = "打印机设置";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("宋体", 10F);
            this.lblIP.Location = new System.Drawing.Point(331, 46);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(29, 20);
            this.lblIP.TabIndex = 44;
            this.lblIP.Text = "IP";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(372, 42);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 23, 3, 23);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(152, 28);
            this.txtIP.TabIndex = 45;
            // 
            // lblCom
            // 
            this.lblCom.AutoSize = true;
            this.lblCom.Font = new System.Drawing.Font("宋体", 10F);
            this.lblCom.Location = new System.Drawing.Point(70, 50);
            this.lblCom.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.lblCom.Name = "lblCom";
            this.lblCom.Size = new System.Drawing.Size(49, 20);
            this.lblCom.TabIndex = 9;
            this.lblCom.Text = "串口";
            // 
            // txtBounnd
            // 
            this.txtBounnd.Location = new System.Drawing.Point(140, 87);
            this.txtBounnd.Margin = new System.Windows.Forms.Padding(3, 23, 3, 23);
            this.txtBounnd.Name = "txtBounnd";
            this.txtBounnd.Size = new System.Drawing.Size(152, 28);
            this.txtBounnd.TabIndex = 7;
            // 
            // lblBounnd
            // 
            this.lblBounnd.AutoSize = true;
            this.lblBounnd.Font = new System.Drawing.Font("宋体", 10F);
            this.lblBounnd.Location = new System.Drawing.Point(50, 87);
            this.lblBounnd.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.lblBounnd.Name = "lblBounnd";
            this.lblBounnd.Size = new System.Drawing.Size(69, 20);
            this.lblBounnd.TabIndex = 5;
            this.lblBounnd.Text = "波特率";
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(140, 43);
            this.txtCom.Margin = new System.Windows.Forms.Padding(3, 23, 3, 23);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(152, 28);
            this.txtCom.TabIndex = 6;
            // 
            // btSetting
            // 
            this.btSetting.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSetting.Location = new System.Drawing.Point(372, 87);
            this.btSetting.Margin = new System.Windows.Forms.Padding(4, 12, 4, 12);
            this.btSetting.Name = "btSetting";
            this.btSetting.Size = new System.Drawing.Size(152, 36);
            this.btSetting.TabIndex = 1;
            this.btSetting.Text = "设置";
            this.btSetting.UseVisualStyleBackColor = true;
            this.btSetting.Click += new System.EventHandler(this.btSetting_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbrecords);
            this.splitContainer1.Panel1.Controls.Add(this.chkCombine);
            this.splitContainer1.Panel1.Controls.Add(this.gbPrintMachine);
            this.splitContainer1.Panel1.Controls.Add(this.cbWerks);
            this.splitContainer1.Panel1.Controls.Add(this.labelStorage);
            this.splitContainer1.Panel1.Controls.Add(this.labelLgort);
            this.splitContainer1.Panel1.Controls.Add(this.ckPrintAgain);
            this.splitContainer1.Panel1.Controls.Add(this.cbLgort);
            this.splitContainer1.Panel1.Controls.Add(this.labelDate);
            this.splitContainer1.Panel1.Controls.Add(this.btnQuery);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.dtpBegin);
            this.splitContainer1.Panel1.Controls.Add(this.txtGRno);
            this.splitContainer1.Panel1.Controls.Add(this.labelTo);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEnd);
            this.splitContainer1.Panel1.Controls.Add(this.labelOrderNo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1304, 662);
            this.splitContainer1.SplitterDistance = 197;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 49;
            // 
            // chkCombine
            // 
            this.chkCombine.AutoSize = true;
            this.chkCombine.Location = new System.Drawing.Point(705, 260);
            this.chkCombine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkCombine.Name = "chkCombine";
            this.chkCombine.Size = new System.Drawing.Size(70, 22);
            this.chkCombine.TabIndex = 47;
            this.chkCombine.Text = "合箱";
            this.chkCombine.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvGridData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnPrint);
            this.splitContainer2.Panel2.Controls.Add(this.stbStatus);
            this.splitContainer2.Size = new System.Drawing.Size(1304, 459);
            this.splitContainer2.SplitterDistance = 332;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 45;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 85);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1304, 36);
            this.stbStatus.TabIndex = 45;
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
            this.stsComcd.Width = 60;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Width = 120;
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 450;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            this.stsDate.Width = 120;
            // 
            // lbrecords
            // 
            this.lbrecords.AutoSize = true;
            this.lbrecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbrecords.Location = new System.Drawing.Point(4, 262);
            this.lbrecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbrecords.Name = "lbrecords";
            this.lbrecords.Size = new System.Drawing.Size(91, 20);
            this.lbrecords.TabIndex = 49;
            this.lbrecords.Text = "0 records";
            // 
            // Manage_IQC_BadDataPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 662);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Manage_IQC_BadDataPrint";
            this.Text = "Manage_IQC_BadDataPrint";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridData)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.gbPrintMachine.ResumeLayout(false);
            this.gbPrintMachine.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLgort;
        private System.Windows.Forms.ComboBox cbWerks;
        private System.Windows.Forms.Label labelLgort;
        private System.Windows.Forms.Label labelStorage;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.TextBox txtGRno;
        private System.Windows.Forms.Label labelOrderNo;
        private System.Windows.Forms.CheckBox ckPrintAgain;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvGridData;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSplit;
        private System.Windows.Forms.GroupBox gbPrintMachine;
        private System.Windows.Forms.Label lblBounnd;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.TextBox txtBounnd;
        private System.Windows.Forms.Label lblCom;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btSetting;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.CheckBox chkCombine;
        private System.Windows.Forms.Label lbrecords;  
    }
}