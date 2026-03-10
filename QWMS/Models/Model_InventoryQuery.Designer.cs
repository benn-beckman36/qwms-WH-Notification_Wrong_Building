namespace QWMS.Models
{
    partial class Model_InventoryQuery
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
            this.tsslMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRecords = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMandt,
            this.tsslComcd,
            this.tsslUsrnm,
            this.tsslWarning,
            this.tsslDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 417);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(818, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslMandt
            // 
            this.tsslMandt.AutoSize = false;
            this.tsslMandt.Name = "tsslMandt";
            this.tsslMandt.Size = new System.Drawing.Size(60, 17);
            this.tsslMandt.Text = "tsslMandt";
            // 
            // tsslComcd
            // 
            this.tsslComcd.AutoSize = false;
            this.tsslComcd.Name = "tsslComcd";
            this.tsslComcd.Size = new System.Drawing.Size(70, 17);
            this.tsslComcd.Text = "tsslComcd";
            // 
            // tsslUsrnm
            // 
            this.tsslUsrnm.Name = "tsslUsrnm";
            this.tsslUsrnm.Size = new System.Drawing.Size(65, 17);
            this.tsslUsrnm.Text = "tsslUsrnm";
            // 
            // tsslWarning
            // 
            this.tsslWarning.AutoSize = false;
            this.tsslWarning.Name = "tsslWarning";
            this.tsslWarning.Size = new System.Drawing.Size(470, 17);
            this.tsslWarning.Text = "tsslWarning";
            // 
            // tsslDate
            // 
            this.tsslDate.AutoSize = false;
            this.tsslDate.Name = "tsslDate";
            this.tsslDate.Size = new System.Drawing.Size(131, 17);
            this.tsslDate.Text = "tsslDate";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.btnFile);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lnkSample);
            this.panel1.Controls.Add(this.txtLocat);
            this.panel1.Controls.Add(this.txtMblnr);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbLgort);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.txtMatnr);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 118);
            this.panel1.TabIndex = 1;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(538, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(191, 21);
            this.txtFilePath.TabIndex = 59;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFile.Location = new System.Drawing.Point(735, 19);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(71, 21);
            this.btnFile.TabIndex = 60;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(458, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 21);
            this.label6.TabIndex = 58;
            this.label6.Text = "File Path";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(731, 0);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(96, 15);
            this.lnkSample.TabIndex = 57;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "Sample";
            this.lnkSample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSample_LinkClicked);
            // 
            // txtLocat
            // 
            this.txtLocat.Location = new System.Drawing.Point(323, 14);
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(100, 21);
            this.txtLocat.TabIndex = 10;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            // 
            // txtMblnr
            // 
            this.txtMblnr.Location = new System.Drawing.Point(532, 66);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(100, 21);
            this.txtMblnr.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(457, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Docu No.:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(240, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "ModelNo.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(245, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(25, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Storage:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(42, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Plant:";
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(93, 66);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 20);
            this.cmbLgort.TabIndex = 2;
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(93, 15);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 20);
            this.cmbWerks.TabIndex = 1;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(323, 66);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(100, 21);
            this.txtMatnr.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lblRecords);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(0, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 267);
            this.panel2.TabIndex = 2;
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(25, 3);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(59, 12);
            this.lblRecords.TabIndex = 1;
            this.lblRecords.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(37, 19);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(743, 232);
            this.dgvData.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnQuery.Location = new System.Drawing.Point(73, 384);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(80, 30);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(232, 384);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(403, 384);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 30);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // Model_InventoryQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 439);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Model_InventoryQuery";
            this.Text = "Model_InventoryQuery(模具库存查询)";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMandt;
        private System.Windows.Forms.ToolStripStatusLabel tsslComcd;
        private System.Windows.Forms.ToolStripStatusLabel tsslWarning;
        private System.Windows.Forms.ToolStripStatusLabel tsslUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel tsslDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkSample;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
    }
}