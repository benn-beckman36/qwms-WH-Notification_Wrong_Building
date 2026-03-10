namespace QWMS
{
    partial class Alim_LocationDefine
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GrpType = new System.Windows.Forms.GroupBox();
            this.RbDelete = new System.Windows.Forms.RadioButton();
            this.RbModify = new System.Windows.Forms.RadioButton();
            this.RbAdd = new System.Windows.Forms.RadioButton();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.LbPlant = new System.Windows.Forms.Label();
            this.LbStorage = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMainRun = new System.Windows.Forms.TextBox();
            this.txtSubRun = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtColnum = new System.Windows.Forms.TextBox();
            this.txtRownum = new System.Windows.Forms.TextBox();
            this.txtDepth = new System.Windows.Forms.TextBox();
            this.txtContrno = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnFresh = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnQuary = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.GvData = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbrecords = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.GrpType.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpType
            // 
            this.GrpType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.GrpType.Controls.Add(this.RbDelete);
            this.GrpType.Controls.Add(this.RbModify);
            this.GrpType.Controls.Add(this.RbAdd);
            this.GrpType.Location = new System.Drawing.Point(33, 27);
            this.GrpType.Name = "GrpType";
            this.GrpType.Size = new System.Drawing.Size(126, 130);
            this.GrpType.TabIndex = 0;
            this.GrpType.TabStop = false;
            this.GrpType.Text = "Type";
            // 
            // RbDelete
            // 
            this.RbDelete.AutoSize = true;
            this.RbDelete.Location = new System.Drawing.Point(25, 94);
            this.RbDelete.Name = "RbDelete";
            this.RbDelete.Size = new System.Drawing.Size(59, 16);
            this.RbDelete.TabIndex = 40;
            this.RbDelete.Text = "Delete";
            this.RbDelete.UseVisualStyleBackColor = true;
            this.RbDelete.CheckedChanged += new System.EventHandler(this.RbDelete_CheckedChanged);
            // 
            // RbModify
            // 
            this.RbModify.AutoSize = true;
            this.RbModify.Location = new System.Drawing.Point(25, 63);
            this.RbModify.Name = "RbModify";
            this.RbModify.Size = new System.Drawing.Size(59, 16);
            this.RbModify.TabIndex = 30;
            this.RbModify.Text = "Modify";
            this.RbModify.UseVisualStyleBackColor = true;
            this.RbModify.CheckedChanged += new System.EventHandler(this.RbModify_CheckedChanged);
            // 
            // RbAdd
            // 
            this.RbAdd.AutoSize = true;
            this.RbAdd.Location = new System.Drawing.Point(25, 30);
            this.RbAdd.Name = "RbAdd";
            this.RbAdd.Size = new System.Drawing.Size(41, 16);
            this.RbAdd.TabIndex = 20;
            this.RbAdd.Text = "Add";
            this.RbAdd.UseVisualStyleBackColor = true;
            this.RbAdd.CheckedChanged += new System.EventHandler(this.RbAdd_CheckedChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(72, 27);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 20);
            this.cmbWerks.TabIndex = 1;
            this.cmbWerks.SelectedValueChanged += new System.EventHandler(this.cmbWerks_SelectedValueChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(72, 62);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 20);
            this.cmbLgort.TabIndex = 2;
            // 
            // LbPlant
            // 
            this.LbPlant.AutoSize = true;
            this.LbPlant.Location = new System.Drawing.Point(23, 34);
            this.LbPlant.Name = "LbPlant";
            this.LbPlant.Size = new System.Drawing.Size(35, 12);
            this.LbPlant.TabIndex = 3;
            this.LbPlant.Text = "Plant";
            // 
            // LbStorage
            // 
            this.LbStorage.AutoSize = true;
            this.LbStorage.Location = new System.Drawing.Point(25, 69);
            this.LbStorage.Name = "LbStorage";
            this.LbStorage.Size = new System.Drawing.Size(47, 12);
            this.LbStorage.TabIndex = 4;
            this.LbStorage.Text = "Storage";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(260, 11);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "主流道：";
            // 
            // txtMainRun
            // 
            this.txtMainRun.Location = new System.Drawing.Point(287, 27);
            this.txtMainRun.Name = "txtMainRun";
            this.txtMainRun.Size = new System.Drawing.Size(121, 21);
            this.txtMainRun.TabIndex = 7;
            // 
            // txtSubRun
            // 
            this.txtSubRun.Location = new System.Drawing.Point(287, 70);
            this.txtSubRun.Name = "txtSubRun";
            this.txtSubRun.Size = new System.Drawing.Size(121, 21);
            this.txtSubRun.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "支流道:";
            // 
            // txtColnum
            // 
            this.txtColnum.Enabled = false;
            this.txtColnum.Location = new System.Drawing.Point(76, 152);
            this.txtColnum.Name = "txtColnum";
            this.txtColnum.Size = new System.Drawing.Size(63, 21);
            this.txtColnum.TabIndex = 10;
            // 
            // txtRownum
            // 
            this.txtRownum.Enabled = false;
            this.txtRownum.Location = new System.Drawing.Point(157, 152);
            this.txtRownum.Name = "txtRownum";
            this.txtRownum.Size = new System.Drawing.Size(63, 21);
            this.txtRownum.TabIndex = 11;
            // 
            // txtDepth
            // 
            this.txtDepth.Enabled = false;
            this.txtDepth.Location = new System.Drawing.Point(242, 152);
            this.txtDepth.Name = "txtDepth";
            this.txtDepth.Size = new System.Drawing.Size(63, 21);
            this.txtDepth.TabIndex = 12;
            // 
            // txtContrno
            // 
            this.txtContrno.Location = new System.Drawing.Point(287, 106);
            this.txtContrno.Name = "txtContrno";
            this.txtContrno.Size = new System.Drawing.Size(121, 21);
            this.txtContrno.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "柜号：";
            // 
            // BtnFresh
            // 
            this.BtnFresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnFresh.Location = new System.Drawing.Point(432, 11);
            this.BtnFresh.Name = "BtnFresh";
            this.BtnFresh.Size = new System.Drawing.Size(75, 23);
            this.BtnFresh.TabIndex = 15;
            this.BtnFresh.Text = "Refresh";
            this.BtnFresh.UseVisualStyleBackColor = true;
            this.BtnFresh.Click += new System.EventHandler(this.BtnFresh_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnExit.Location = new System.Drawing.Point(629, 11);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 16;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // cmbSize
            // 
            this.cmbSize.Enabled = false;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(72, 100);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(121, 20);
            this.cmbSize.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "尺寸：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "列";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(178, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "行";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(265, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "深";
            // 
            // BtnQuary
            // 
            this.BtnQuary.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnQuary.Location = new System.Drawing.Point(92, 11);
            this.BtnQuary.Name = "BtnQuary";
            this.BtnQuary.Size = new System.Drawing.Size(75, 23);
            this.BtnQuary.TabIndex = 22;
            this.BtnQuary.Text = "Quary";
            this.BtnQuary.UseVisualStyleBackColor = true;
            this.BtnQuary.Click += new System.EventHandler(this.BtnQuary_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 55);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 44;
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
            // GvData
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvData.Location = new System.Drawing.Point(0, 0);
            this.GvData.Name = "GvData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvData.RowTemplate.Height = 23;
            this.GvData.Size = new System.Drawing.Size(834, 189);
            this.GvData.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 46;
            this.label8.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "-";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(834, 456);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 48;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbrecords);
            this.splitContainer3.Panel1.Controls.Add(this.GrpType);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.cmbWerks);
            this.splitContainer3.Panel2.Controls.Add(this.label9);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.txtContrno);
            this.splitContainer3.Panel2.Controls.Add(this.label8);
            this.splitContainer3.Panel2.Controls.Add(this.txtDepth);
            this.splitContainer3.Panel2.Controls.Add(this.cmbLgort);
            this.splitContainer3.Panel2.Controls.Add(this.txtRownum);
            this.splitContainer3.Panel2.Controls.Add(this.LbPlant);
            this.splitContainer3.Panel2.Controls.Add(this.cmbSize);
            this.splitContainer3.Panel2.Controls.Add(this.LbStorage);
            this.splitContainer3.Panel2.Controls.Add(this.txtColnum);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Panel2.Controls.Add(this.label4);
            this.splitContainer3.Panel2.Controls.Add(this.label7);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this.txtMainRun);
            this.splitContainer3.Panel2.Controls.Add(this.label5);
            this.splitContainer3.Panel2.Controls.Add(this.label6);
            this.splitContainer3.Panel2.Controls.Add(this.txtSubRun);
            this.splitContainer3.Size = new System.Drawing.Size(834, 182);
            this.splitContainer3.SplitterDistance = 210;
            this.splitContainer3.TabIndex = 48;
            // 
            // lbrecords
            // 
            this.lbrecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbrecords.AutoSize = true;
            this.lbrecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbrecords.Location = new System.Drawing.Point(10, 168);
            this.lbrecords.Name = "lbrecords";
            this.lbrecords.Size = new System.Drawing.Size(61, 14);
            this.lbrecords.TabIndex = 1;
            this.lbrecords.Text = "0 records";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.GvData);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.BtnQuary);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer2.Panel2.Controls.Add(this.BtnSave);
            this.splitContainer2.Panel2.Controls.Add(this.BtnExit);
            this.splitContainer2.Panel2.Controls.Add(this.BtnFresh);
            this.splitContainer2.Size = new System.Drawing.Size(834, 270);
            this.splitContainer2.SplitterDistance = 189;
            this.splitContainer2.TabIndex = 49;
            // 
            // Alim_LocationDefine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 456);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Alim_LocationDefine";
            this.Text = "Admin_LocationDefine_AL";
            this.GrpType.ResumeLayout(false);
            this.GrpType.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpType;
        private System.Windows.Forms.RadioButton RbDelete;
        private System.Windows.Forms.RadioButton RbModify;
        private System.Windows.Forms.RadioButton RbAdd;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label LbPlant;
        private System.Windows.Forms.Label LbStorage;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMainRun;
        private System.Windows.Forms.TextBox txtSubRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColnum;
        private System.Windows.Forms.TextBox txtRownum;
        private System.Windows.Forms.TextBox txtDepth;
        private System.Windows.Forms.TextBox txtContrno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnFresh;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnQuary;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.DataGridView GvData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbrecords;
    }
}