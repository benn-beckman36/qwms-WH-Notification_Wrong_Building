namespace QWMS
{
    partial class Admin_PartNoDefine
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
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.txtMaktx = new System.Windows.Forms.TextBox();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCount = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnBathImport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.gbHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.txtFilePath);
            this.gbHeader.Controls.Add(this.btnFile);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.lnkSample);
            this.gbHeader.Controls.Add(this.txtMaktx);
            this.gbHeader.Controls.Add(this.txtMatnr);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbHeader.Location = new System.Drawing.Point(1, 12);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(722, 110);
            this.gbHeader.TabIndex = 31;
            this.gbHeader.TabStop = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(117, 73);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(372, 22);
            this.txtFilePath.TabIndex = 42;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(495, 73);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(71, 26);
            this.btnFile.TabIndex = 43;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(26, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 21);
            this.label3.TabIndex = 41;
            this.label3.Text = "File Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(587, 80);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(96, 15);
            this.lnkSample.TabIndex = 40;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "Sample";
            this.lnkSample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSample_LinkClicked);
            // 
            // txtMaktx
            // 
            this.txtMaktx.Location = new System.Drawing.Point(117, 44);
            this.txtMaktx.Name = "txtMaktx";
            this.txtMaktx.Size = new System.Drawing.Size(372, 22);
            this.txtMaktx.TabIndex = 4;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(117, 17);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(372, 22);
            this.txtMatnr.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Part No.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(511, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(93, 30);
            this.btnRefresh.TabIndex = 40;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(11, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(621, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 30);
            this.btnExit.TabIndex = 41;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 40);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(727, 22);
            this.statusStrip1.TabIndex = 43;
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
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.Location = new System.Drawing.Point(82, 6);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 15);
            this.lblCount.TabIndex = 44;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Controls.Add(this.btnBathImport);
            this.panel2.Controls.Add(this.lblCount);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Location = new System.Drawing.Point(1, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(727, 62);
            this.panel2.TabIndex = 39;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(379, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 29);
            this.btnDelete.TabIndex = 47;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Enabled = false;
            this.btnExecute.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExecute.Location = new System.Drawing.Point(239, 8);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(109, 29);
            this.btnExecute.TabIndex = 46;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnBathImport
            // 
            this.btnBathImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBathImport.Enabled = false;
            this.btnBathImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBathImport.Location = new System.Drawing.Point(106, 8);
            this.btnBathImport.Name = "btnBathImport";
            this.btnBathImport.Size = new System.Drawing.Size(109, 30);
            this.btnBathImport.TabIndex = 45;
            this.btnBathImport.Text = "Import";
            this.btnBathImport.UseVisualStyleBackColor = true;
            this.btnBathImport.Click += new System.EventHandler(this.btnBathImport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Location = new System.Drawing.Point(1, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 278);
            this.panel1.TabIndex = 40;
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(11, 13);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(692, 254);
            this.dgvData.TabIndex = 49;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            // 
            // Admin_PartNoDefine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbHeader);
            this.Controls.Add(this.panel2);
            this.Name = "Admin_PartNoDefine";
            this.Text = "Location Definition Maintenance";
            this.Resize += new System.EventHandler(this.Admin_PartNoDefine_Resize);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.TextBox txtMaktx;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBathImport;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.LinkLabel lnkSample;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.Button btnDelete;
    }
}