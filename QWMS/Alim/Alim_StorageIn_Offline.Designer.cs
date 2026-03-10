namespace QWMS
{
    partial class Alim_StorageIn_Offline
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.LbPlant = new System.Windows.Forms.Label();
            this.LbStorage = new System.Windows.Forms.Label();
            this.btnFresh = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTempMblnr = new System.Windows.Forms.Label();
            this.cmbRunner = new System.Windows.Forms.ComboBox();
            this.lbRunner = new System.Windows.Forms.Label();
            this.gvData = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(718, 22);
            this.statusStrip1.TabIndex = 45;
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
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(70, 36);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 20);
            this.cmbWerks.TabIndex = 46;
            this.cmbWerks.SelectedValueChanged += new System.EventHandler(this.cmbWerks_SelectedValueChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(70, 78);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 20);
            this.cmbLgort.TabIndex = 47;
            this.cmbLgort.SelectedValueChanged += new System.EventHandler(this.cmbLgort_SelectedValueChanged);
            // 
            // LbPlant
            // 
            this.LbPlant.AutoSize = true;
            this.LbPlant.Location = new System.Drawing.Point(29, 42);
            this.LbPlant.Name = "LbPlant";
            this.LbPlant.Size = new System.Drawing.Size(35, 12);
            this.LbPlant.TabIndex = 48;
            this.LbPlant.Text = "Plant";
            // 
            // LbStorage
            // 
            this.LbStorage.AutoSize = true;
            this.LbStorage.Location = new System.Drawing.Point(17, 84);
            this.LbStorage.Name = "LbStorage";
            this.LbStorage.Size = new System.Drawing.Size(47, 12);
            this.LbStorage.TabIndex = 49;
            this.LbStorage.Text = "Storage";
            // 
            // btnFresh
            // 
            this.btnFresh.Location = new System.Drawing.Point(326, 105);
            this.btnFresh.Name = "btnFresh";
            this.btnFresh.Size = new System.Drawing.Size(75, 23);
            this.btnFresh.TabIndex = 50;
            this.btnFresh.Text = "Refresh";
            this.btnFresh.UseVisualStyleBackColor = true;
            this.btnFresh.Click += new System.EventHandler(this.BtnFresh_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(40, 105);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 51;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(174, 105);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 52;
            this.btnEnd.Text = "End";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "虚拟单号：";
            // 
            // lbTempMblnr
            // 
            this.lbTempMblnr.AutoSize = true;
            this.lbTempMblnr.Location = new System.Drawing.Point(104, 58);
            this.lbTempMblnr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTempMblnr.Name = "lbTempMblnr";
            this.lbTempMblnr.Size = new System.Drawing.Size(0, 12);
            this.lbTempMblnr.TabIndex = 55;
            // 
            // cmbRunner
            // 
            this.cmbRunner.FormattingEnabled = true;
            this.cmbRunner.Location = new System.Drawing.Point(70, 122);
            this.cmbRunner.Name = "cmbRunner";
            this.cmbRunner.Size = new System.Drawing.Size(121, 20);
            this.cmbRunner.TabIndex = 54;
            // 
            // lbRunner
            // 
            this.lbRunner.AutoSize = true;
            this.lbRunner.Location = new System.Drawing.Point(23, 124);
            this.lbRunner.Name = "lbRunner";
            this.lbRunner.Size = new System.Drawing.Size(41, 12);
            this.lbRunner.TabIndex = 53;
            this.lbRunner.Text = "Runner";
            // 
            // gvData
            // 
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvData.Location = new System.Drawing.Point(0, 0);
            this.gvData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gvData.Name = "gvData";
            this.gvData.RowTemplate.Height = 27;
            this.gvData.Size = new System.Drawing.Size(718, 203);
            this.gvData.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LbPlant);
            this.splitContainer1.Panel1.Controls.Add(this.cmbWerks);
            this.splitContainer1.Panel1.Controls.Add(this.cmbLgort);
            this.splitContainer1.Panel1.Controls.Add(this.LbStorage);
            this.splitContainer1.Panel1.Controls.Add(this.cmbRunner);
            this.splitContainer1.Panel1.Controls.Add(this.lbRunner);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnStart);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnEnd);
            this.splitContainer1.Panel2.Controls.Add(this.lbTempMblnr);
            this.splitContainer1.Panel2.Controls.Add(this.btnFresh);
            this.splitContainer1.Size = new System.Drawing.Size(718, 192);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 57;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(718, 426);
            this.splitContainer2.SplitterDistance = 192;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 57;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.gvData);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer3.Size = new System.Drawing.Size(718, 231);
            this.splitContainer3.SplitterDistance = 203;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 46;
            // 
            // Alim_StorageIn_Offline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 426);
            this.Controls.Add(this.splitContainer2);
            this.Name = "Alim_StorageIn_Offline";
            this.Text = "Alim_OffLineIn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Alim_StorageIn_Offline_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label LbPlant;
        private System.Windows.Forms.Label LbStorage;
        private System.Windows.Forms.Button btnFresh;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.ComboBox cmbRunner;
        private System.Windows.Forms.Label lbRunner;
        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.Label lbTempMblnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}