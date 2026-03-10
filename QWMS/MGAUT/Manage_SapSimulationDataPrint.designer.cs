namespace QWMS
{
    partial class Manage_SapSimulationDataPrint
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
            this.labelStorage = new System.Windows.Forms.Label();
            this.labelLgort = new System.Windows.Forms.Label();
            this.labelCostCenter = new System.Windows.Forms.Label();
            this.labelOrderNo = new System.Windows.Forms.Label();
            this.labelPPNo = new System.Windows.Forms.Label();
            this.labelPerson = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbWerks = new System.Windows.Forms.ComboBox();
            this.cbLgorts = new System.Windows.Forms.ComboBox();
            this.tbCostCenter = new System.Windows.Forms.TextBox();
            this.tbOrderNo = new System.Windows.Forms.TextBox();
            this.tbPPNo = new System.Windows.Forms.TextBox();
            this.tbPerson = new System.Windows.Forms.TextBox();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.labelRecords = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.btnPrintSetting = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtBounnd = new System.Windows.Forms.TextBox();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ckPrintAgain = new System.Windows.Forms.CheckBox();
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCombine = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPackingno = new System.Windows.Forms.Label();
            this.tbpackingno = new System.Windows.Forms.TextBox();
            this.dgvBottom = new System.Windows.Forms.DataGridView();
            this.lblprint = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.cmsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStorage
            // 
            this.labelStorage.AutoSize = true;
            this.labelStorage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStorage.Location = new System.Drawing.Point(54, 23);
            this.labelStorage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStorage.Name = "labelStorage";
            this.labelStorage.Size = new System.Drawing.Size(35, 12);
            this.labelStorage.TabIndex = 0;
            this.labelStorage.Text = "厂区:";
            // 
            // labelLgort
            // 
            this.labelLgort.AutoSize = true;
            this.labelLgort.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLgort.Location = new System.Drawing.Point(54, 69);
            this.labelLgort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLgort.Name = "labelLgort";
            this.labelLgort.Size = new System.Drawing.Size(35, 12);
            this.labelLgort.TabIndex = 1;
            this.labelLgort.Text = "仓别:";
            // 
            // labelCostCenter
            // 
            this.labelCostCenter.AutoSize = true;
            this.labelCostCenter.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCostCenter.Location = new System.Drawing.Point(12, 112);
            this.labelCostCenter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCostCenter.Name = "labelCostCenter";
            this.labelCostCenter.Size = new System.Drawing.Size(77, 12);
            this.labelCostCenter.TabIndex = 2;
            this.labelCostCenter.Text = "Cost Center:";
            // 
            // labelOrderNo
            // 
            this.labelOrderNo.AutoSize = true;
            this.labelOrderNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOrderNo.Location = new System.Drawing.Point(299, 23);
            this.labelOrderNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOrderNo.Name = "labelOrderNo";
            this.labelOrderNo.Size = new System.Drawing.Size(29, 12);
            this.labelOrderNo.TabIndex = 3;
            this.labelOrderNo.Text = "单号";
            // 
            // labelPPNo
            // 
            this.labelPPNo.AutoSize = true;
            this.labelPPNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPPNo.Location = new System.Drawing.Point(287, 69);
            this.labelPPNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPPNo.Name = "labelPPNo";
            this.labelPPNo.Size = new System.Drawing.Size(41, 12);
            this.labelPPNo.TabIndex = 4;
            this.labelPPNo.Text = "判票号";
            // 
            // labelPerson
            // 
            this.labelPerson.AutoSize = true;
            this.labelPerson.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPerson.Location = new System.Drawing.Point(287, 114);
            this.labelPerson.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPerson.Name = "labelPerson";
            this.labelPerson.Size = new System.Drawing.Size(41, 12);
            this.labelPerson.TabIndex = 5;
            this.labelPerson.Text = "开单人";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDate.Location = new System.Drawing.Point(524, 23);
            this.labelDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(29, 12);
            this.labelDate.TabIndex = 6;
            this.labelDate.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(535, 145);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 28);
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(707, 145);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 28);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(1, 8);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(124, 28);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbWerks
            // 
            this.cbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWerks.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbWerks.FormattingEnabled = true;
            this.cbWerks.Location = new System.Drawing.Point(104, 20);
            this.cbWerks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbWerks.Name = "cbWerks";
            this.cbWerks.Size = new System.Drawing.Size(159, 21);
            this.cbWerks.TabIndex = 11;
            this.cbWerks.SelectedIndexChanged += new System.EventHandler(this.cbWerks_SelectedIndexChanged);
            // 
            // cbLgorts
            // 
            this.cbLgorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLgorts.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLgorts.FormattingEnabled = true;
            this.cbLgorts.Location = new System.Drawing.Point(104, 66);
            this.cbLgorts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbLgorts.Name = "cbLgorts";
            this.cbLgorts.Size = new System.Drawing.Size(159, 21);
            this.cbLgorts.TabIndex = 12;
            // 
            // tbCostCenter
            // 
            this.tbCostCenter.Location = new System.Drawing.Point(104, 113);
            this.tbCostCenter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCostCenter.Name = "tbCostCenter";
            this.tbCostCenter.Size = new System.Drawing.Size(159, 21);
            this.tbCostCenter.TabIndex = 13;
            // 
            // tbOrderNo
            // 
            this.tbOrderNo.Location = new System.Drawing.Point(362, 19);
            this.tbOrderNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbOrderNo.Name = "tbOrderNo";
            this.tbOrderNo.Size = new System.Drawing.Size(142, 21);
            this.tbOrderNo.TabIndex = 14;
            // 
            // tbPPNo
            // 
            this.tbPPNo.Location = new System.Drawing.Point(362, 65);
            this.tbPPNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPPNo.Name = "tbPPNo";
            this.tbPPNo.Size = new System.Drawing.Size(142, 21);
            this.tbPPNo.TabIndex = 15;
            this.tbPPNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPPNo_KeyDown);
            // 
            // tbPerson
            // 
            this.tbPerson.Location = new System.Drawing.Point(360, 113);
            this.tbPerson.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPerson.Name = "tbPerson";
            this.tbPerson.Size = new System.Drawing.Size(144, 21);
            this.tbPerson.TabIndex = 16;
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(568, 19);
            this.dtpBegin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(121, 21);
            this.dtpBegin.TabIndex = 17;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(723, 19);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(121, 21);
            this.dtpEnd.TabIndex = 18;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTo.Location = new System.Drawing.Point(694, 23);
            this.labelTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(17, 12);
            this.labelTo.TabIndex = 19;
            this.labelTo.Text = "TO";
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 654);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1259, 21);
            this.stbStatus.TabIndex = 34;
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
            // labelRecords
            // 
            this.labelRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRecords.AutoSize = true;
            this.labelRecords.Location = new System.Drawing.Point(1032, 16);
            this.labelRecords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRecords.Name = "labelRecords";
            this.labelRecords.Size = new System.Drawing.Size(77, 12);
            this.labelRecords.TabIndex = 35;
            this.labelRecords.Text = "labelRecords";
            this.labelRecords.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.btnPrintSetting);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.txtBounnd);
            this.groupBox1.Controls.Add(this.txtCom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(525, 48);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(316, 82);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机设置";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIP.Location = new System.Drawing.Point(164, 19);
            this.lblIP.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(29, 12);
            this.lblIP.TabIndex = 39;
            this.lblIP.Text = "IP：";
            // 
            // btnPrintSetting
            // 
            this.btnPrintSetting.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintSetting.Location = new System.Drawing.Point(195, 44);
            this.btnPrintSetting.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPrintSetting.Name = "btnPrintSetting";
            this.btnPrintSetting.Size = new System.Drawing.Size(106, 26);
            this.btnPrintSetting.TabIndex = 4;
            this.btnPrintSetting.Text = "设置";
            this.btnPrintSetting.UseVisualStyleBackColor = true;
            this.btnPrintSetting.Click += new System.EventHandler(this.btnPrintSetting_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(198, 17);
            this.txtIP.Margin = new System.Windows.Forms.Padding(2, 15, 2, 15);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(103, 21);
            this.txtIP.TabIndex = 40;
            // 
            // txtBounnd
            // 
            this.txtBounnd.Location = new System.Drawing.Point(60, 44);
            this.txtBounnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBounnd.Name = "txtBounnd";
            this.txtBounnd.Size = new System.Drawing.Size(102, 21);
            this.txtBounnd.TabIndex = 3;
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(59, 17);
            this.txtCom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(103, 21);
            this.txtCom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口：";
            // 
            // ckPrintAgain
            // 
            this.ckPrintAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckPrintAgain.AutoSize = true;
            this.ckPrintAgain.Location = new System.Drawing.Point(104, 153);
            this.ckPrintAgain.Name = "ckPrintAgain";
            this.ckPrintAgain.Size = new System.Drawing.Size(48, 16);
            this.ckPrintAgain.TabIndex = 38;
            this.ckPrintAgain.Text = "补印";
            this.ckPrintAgain.UseVisualStyleBackColor = true;
            // 
            // cmsMenu
            // 
            this.cmsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSplit,
            this.tsmiCombine});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiSplit
            // 
            this.tsmiSplit.Name = "tsmiSplit";
            this.tsmiSplit.Size = new System.Drawing.Size(100, 22);
            this.tsmiSplit.Text = "拆分";
            this.tsmiSplit.Click += new System.EventHandler(this.tsmiSplit_Click);
            // 
            // tsmiCombine
            // 
            this.tsmiCombine.Name = "tsmiCombine";
            this.tsmiCombine.Size = new System.Drawing.Size(100, 22);
            this.tsmiCombine.Text = "合并";
            this.tsmiCombine.Click += new System.EventHandler(this.tsmiCombine_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(338, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 14);
            this.label3.TabIndex = 39;
            this.label3.Text = "BPM签核数据需Key入单据号";
            // 
            // labelPackingno
            // 
            this.labelPackingno.AutoSize = true;
            this.labelPackingno.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPackingno.Location = new System.Drawing.Point(883, 69);
            this.labelPackingno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPackingno.Name = "labelPackingno";
            this.labelPackingno.Size = new System.Drawing.Size(59, 12);
            this.labelPackingno.TabIndex = 40;
            this.labelPackingno.Text = "PackingNo";
            // 
            // tbpackingno
            // 
            this.tbpackingno.Location = new System.Drawing.Point(951, 66);
            this.tbpackingno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbpackingno.Name = "tbpackingno";
            this.tbpackingno.Size = new System.Drawing.Size(142, 21);
            this.tbpackingno.TabIndex = 41;
            // 
            // dgvBottom
            // 
            this.dgvBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBottom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBottom.Location = new System.Drawing.Point(2, 2);
            this.dgvBottom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvBottom.Name = "dgvBottom";
            this.dgvBottom.RowTemplate.Height = 27;
            this.dgvBottom.Size = new System.Drawing.Size(1117, 407);
            this.dgvBottom.TabIndex = 0;
            // 
            // lblprint
            // 
            this.lblprint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblprint.AutoSize = true;
            this.lblprint.Location = new System.Drawing.Point(188, 430);
            this.lblprint.Name = "lblprint";
            this.lblprint.Size = new System.Drawing.Size(0, 12);
            this.lblprint.TabIndex = 37;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(14, 196);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvBottom);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnPrint);
            this.splitContainer1.Panel2.Controls.Add(this.labelRecords);
            this.splitContainer1.Size = new System.Drawing.Size(1121, 452);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 42;
            // 
            // Manage_SapSimulationDataPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 675);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tbpackingno);
            this.Controls.Add(this.labelPackingno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckPrintAgain);
            this.Controls.Add(this.lblprint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpBegin);
            this.Controls.Add(this.tbPerson);
            this.Controls.Add(this.tbPPNo);
            this.Controls.Add(this.tbOrderNo);
            this.Controls.Add(this.tbCostCenter);
            this.Controls.Add(this.cbLgorts);
            this.Controls.Add(this.cbWerks);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelPerson);
            this.Controls.Add(this.labelPPNo);
            this.Controls.Add(this.labelOrderNo);
            this.Controls.Add(this.labelCostCenter);
            this.Controls.Add(this.labelLgort);
            this.Controls.Add(this.labelStorage);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Manage_SapSimulationDataPrint";
            this.Text = "Manage_SapSimulationDataPrint";
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBottom)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStorage;
        private System.Windows.Forms.Label labelLgort;
        private System.Windows.Forms.Label labelCostCenter;
        private System.Windows.Forms.Label labelOrderNo;
        private System.Windows.Forms.Label labelPPNo;
        private System.Windows.Forms.Label labelPerson;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cbWerks;
        private System.Windows.Forms.ComboBox cbLgorts;
        private System.Windows.Forms.TextBox tbCostCenter;
        private System.Windows.Forms.TextBox tbOrderNo;
        private System.Windows.Forms.TextBox tbPPNo;
        private System.Windows.Forms.TextBox tbPerson;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label labelTo;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Label labelRecords;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBounnd;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintSetting;
        private System.Windows.Forms.CheckBox ckPrintAgain;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSplit;
        private System.Windows.Forms.ToolStripMenuItem tsmiCombine;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPackingno;
        private System.Windows.Forms.TextBox tbpackingno;
        private System.Windows.Forms.DataGridView dgvBottom;
        private System.Windows.Forms.Label lblprint;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}