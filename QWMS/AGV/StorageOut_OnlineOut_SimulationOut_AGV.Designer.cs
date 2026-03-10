namespace QWMS.OTAUT
{
    partial class StorageOut_OnlineOut_SimulationOut_AGV
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnProduce = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpCrdat = new System.Windows.Forms.DateTimePicker();
            this.lbPlant = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lbStorage = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.lbGrpID = new System.Windows.Forms.Label();
            this.cmbGrpID = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStorage = new System.Windows.Forms.Label();
            this.dgvStorage = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvOutSource = new System.Windows.Forms.DataGridView();
            this.lblOutSource = new System.Windows.Forms.Label();
            this.chkAllId = new System.Windows.Forms.CheckBox();
            this.chkDateCode = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(1206, 680);
            this.splitContainer2.SplitterDistance = 115;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.btnConfirm);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnQuery);
            this.panel3.Controls.Add(this.btnProduce);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1206, 51);
            this.panel3.TabIndex = 44;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(768, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(97, 37);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(215, 9);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(97, 37);
            this.btnConfirm.TabIndex = 44;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(623, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 37);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuery.Location = new System.Drawing.Point(487, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(97, 37);
            this.btnQuery.TabIndex = 46;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnProduce
            // 
            this.btnProduce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProduce.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnProduce.Location = new System.Drawing.Point(351, 9);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(97, 37);
            this.btnProduce.TabIndex = 45;
            this.btnProduce.Text = "Produce";
            this.btnProduce.UseVisualStyleBackColor = true;
            this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.40341F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.981872F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.12998F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.477987F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.62869F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.39746F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.09372F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.886884F));
            this.tableLayoutPanel1.Controls.Add(this.dtpCrdat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbPlant, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbWerks, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbStorage, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbLgort, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbGrpID, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbGrpID, 7, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1206, 64);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dtpCrdat
            // 
            this.dtpCrdat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpCrdat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCrdat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCrdat.Location = new System.Drawing.Point(24, 18);
            this.dtpCrdat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpCrdat.Name = "dtpCrdat";
            this.dtpCrdat.Size = new System.Drawing.Size(139, 30);
            this.dtpCrdat.TabIndex = 27;
            // 
            // lbPlant
            // 
            this.lbPlant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPlant.AutoSize = true;
            this.lbPlant.Location = new System.Drawing.Point(192, 21);
            this.lbPlant.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbPlant.Name = "lbPlant";
            this.lbPlant.Size = new System.Drawing.Size(54, 21);
            this.lbPlant.TabIndex = 0;
            this.lbPlant.Text = "Plant";
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(252, 16);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(161, 29);
            this.cmbWerks.TabIndex = 3;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lbStorage
            // 
            this.lbStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbStorage.AutoSize = true;
            this.lbStorage.Location = new System.Drawing.Point(435, 21);
            this.lbStorage.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbStorage.Name = "lbStorage";
            this.lbStorage.Size = new System.Drawing.Size(78, 21);
            this.lbStorage.TabIndex = 1;
            this.lbStorage.Text = "Storage";
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(519, 16);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(167, 29);
            this.cmbLgort.TabIndex = 4;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // lbGrpID
            // 
            this.lbGrpID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGrpID.AutoSize = true;
            this.lbGrpID.Location = new System.Drawing.Point(695, 21);
            this.lbGrpID.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lbGrpID.Name = "lbGrpID";
            this.lbGrpID.Size = new System.Drawing.Size(78, 21);
            this.lbGrpID.TabIndex = 6;
            this.lbGrpID.Text = "Send ID";
            // 
            // cmbGrpID
            // 
            this.cmbGrpID.FormattingEnabled = true;
            this.cmbGrpID.Location = new System.Drawing.Point(779, 16);
            this.cmbGrpID.Name = "cmbGrpID";
            this.cmbGrpID.Size = new System.Drawing.Size(327, 29);
            this.cmbGrpID.TabIndex = 7;
            this.cmbGrpID.SelectedIndexChanged += new System.EventHandler(this.cmbGrpID_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblStorage);
            this.panel2.Controls.Add(this.dgvStorage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 253);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1206, 308);
            this.panel2.TabIndex = 1;
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStorage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblStorage.Location = new System.Drawing.Point(21, 3);
            this.lblStorage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(77, 22);
            this.lblStorage.TabIndex = 43;
            this.lblStorage.Text = "0 records";
            // 
            // dgvStorage
            // 
            this.dgvStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStorage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStorage.Location = new System.Drawing.Point(21, 28);
            this.dgvStorage.Name = "dgvStorage";
            this.dgvStorage.RowHeadersWidth = 62;
            this.dgvStorage.RowTemplate.Height = 28;
            this.dgvStorage.Size = new System.Drawing.Size(1162, 263);
            this.dgvStorage.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvOutSource);
            this.panel1.Controls.Add(this.lblOutSource);
            this.panel1.Controls.Add(this.chkAllId);
            this.panel1.Controls.Add(this.chkDateCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1206, 253);
            this.panel1.TabIndex = 0;
            // 
            // dgvOutSource
            // 
            this.dgvOutSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutSource.Location = new System.Drawing.Point(21, 24);
            this.dgvOutSource.Name = "dgvOutSource";
            this.dgvOutSource.RowHeadersWidth = 62;
            this.dgvOutSource.RowTemplate.Height = 28;
            this.dgvOutSource.Size = new System.Drawing.Size(1162, 214);
            this.dgvOutSource.TabIndex = 1;
            // 
            // lblOutSource
            // 
            this.lblOutSource.AutoSize = true;
            this.lblOutSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblOutSource.Location = new System.Drawing.Point(21, 0);
            this.lblOutSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutSource.Name = "lblOutSource";
            this.lblOutSource.Size = new System.Drawing.Size(77, 22);
            this.lblOutSource.TabIndex = 11;
            this.lblOutSource.Text = "0 records";
            // 
            // chkAllId
            // 
            this.chkAllId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllId.AutoSize = true;
            this.chkAllId.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.chkAllId.Location = new System.Drawing.Point(935, 0);
            this.chkAllId.Name = "chkAllId";
            this.chkAllId.Size = new System.Drawing.Size(82, 25);
            this.chkAllId.TabIndex = 38;
            this.chkAllId.Text = "All ID";
            this.chkAllId.UseVisualStyleBackColor = true;
            this.chkAllId.CheckedChanged += new System.EventHandler(this.chkAllId_CheckedChanged);
            // 
            // chkDateCode
            // 
            this.chkDateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDateCode.AutoSize = true;
            this.chkDateCode.Enabled = false;
            this.chkDateCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.chkDateCode.Location = new System.Drawing.Point(1032, 0);
            this.chkDateCode.Name = "chkDateCode";
            this.chkDateCode.Size = new System.Drawing.Size(122, 25);
            this.chkDateCode.TabIndex = 40;
            this.chkDateCode.Text = "DateCode";
            this.chkDateCode.UseVisualStyleBackColor = true;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 680);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1206, 28);
            this.statusStrip1.TabIndex = 44;
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
            // StorageOut_OnlineOut_SimulationOut_AGV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 708);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Name = "StorageOut_OnlineOut_SimulationOut_AGV";
            this.Text = "StorageOut_OnlineOut_SimulationOut_AGV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageOut_OnlineOut_SimulationOut_AGV_FormClosing);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lbPlant;
        private System.Windows.Forms.DateTimePicker dtpCrdat;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lbStorage;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbGrpID;
        private System.Windows.Forms.Label lbGrpID;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.CheckBox chkAllId;
        private System.Windows.Forms.CheckBox chkDateCode;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Label lblOutSource;
        private System.Windows.Forms.DataGridView dgvOutSource;
        private System.Windows.Forms.DataGridView dgvStorage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnProduce;
    }
}