namespace QWMS.Models
{
    partial class Model_LocationCombine
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
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dgvSource = new System.Windows.Forms.DataGridView();
            this.txtOldLocat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbDestination = new System.Windows.Forms.GroupBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNewLocat = new System.Windows.Forms.TextBox();
            this.dgvDestination = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbCondition.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.gbDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDestination)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(941, 22);
            this.statusStrip1.TabIndex = 1;
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
            this.tsslComcd.Size = new System.Drawing.Size(65, 17);
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
            this.tsslWarning.Size = new System.Drawing.Size(570, 17);
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
            this.panel1.Controls.Add(this.gbCondition);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 99);
            this.panel1.TabIndex = 2;
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.label1);
            this.gbCondition.Controls.Add(this.label2);
            this.gbCondition.Controls.Add(this.cmbWerks);
            this.gbCondition.Controls.Add(this.cmbLgort);
            this.gbCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCondition.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCondition.Location = new System.Drawing.Point(0, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(941, 99);
            this.gbCondition.TabIndex = 4;
            this.gbCondition.TabStop = false;
            this.gbCondition.Text = "Condition";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(70, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plant";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(293, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Storage";
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(128, 35);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(101, 22);
            this.cmbWerks.TabIndex = 0;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(361, 35);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(101, 22);
            this.cmbLgort.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gbSource);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(467, 284);
            this.panel2.TabIndex = 3;
            // 
            // gbSource
            // 
            this.gbSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSource.Controls.Add(this.lblFrom);
            this.gbSource.Controls.Add(this.dgvSource);
            this.gbSource.Controls.Add(this.txtOldLocat);
            this.gbSource.Controls.Add(this.label3);
            this.gbSource.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbSource.Location = new System.Drawing.Point(9, 3);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(449, 261);
            this.gbSource.TabIndex = 0;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(4, 17);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(54, 14);
            this.lblFrom.TabIndex = 3;
            this.lblFrom.Text = "0 records";
            // 
            // dgvSource
            // 
            this.dgvSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSource.Location = new System.Drawing.Point(7, 50);
            this.dgvSource.Name = "dgvSource";
            this.dgvSource.RowTemplate.Height = 23;
            this.dgvSource.Size = new System.Drawing.Size(436, 192);
            this.dgvSource.TabIndex = 2;
            this.dgvSource.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSource_CellValueChanged);
            this.dgvSource.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSource_RowHeaderMouseClick);
            this.dgvSource.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSource_CellContentClick_1);
            // 
            // txtOldLocat
            // 
            this.txtOldLocat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOldLocat.Location = new System.Drawing.Point(208, 10);
            this.txtOldLocat.Name = "txtOldLocat";
            this.txtOldLocat.Size = new System.Drawing.Size(100, 20);
            this.txtOldLocat.TabIndex = 0;
            this.txtOldLocat.DoubleClick += new System.EventHandler(this.txtOldLocat_DoubleClick);
            this.txtOldLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOldLocat_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(97, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "From Location";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbDestination);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(467, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(474, 284);
            this.panel3.TabIndex = 4;
            // 
            // gbDestination
            // 
            this.gbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDestination.Controls.Add(this.lblTo);
            this.gbDestination.Controls.Add(this.label4);
            this.gbDestination.Controls.Add(this.txtNewLocat);
            this.gbDestination.Controls.Add(this.dgvDestination);
            this.gbDestination.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbDestination.Location = new System.Drawing.Point(8, 3);
            this.gbDestination.Name = "gbDestination";
            this.gbDestination.Size = new System.Drawing.Size(463, 261);
            this.gbDestination.TabIndex = 0;
            this.gbDestination.TabStop = false;
            this.gbDestination.Text = "Destination";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lblTo.Location = new System.Drawing.Point(6, 18);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(54, 14);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "0 records";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(116, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "To Location";
            // 
            // txtNewLocat
            // 
            this.txtNewLocat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNewLocat.Enabled = false;
            this.txtNewLocat.Location = new System.Drawing.Point(208, 12);
            this.txtNewLocat.Name = "txtNewLocat";
            this.txtNewLocat.Size = new System.Drawing.Size(100, 20);
            this.txtNewLocat.TabIndex = 0;
            this.txtNewLocat.DoubleClick += new System.EventHandler(this.txtNewLocat_DoubleClick);
            this.txtNewLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewLocat_KeyPress);
            // 
            // dgvDestination
            // 
            this.dgvDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDestination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDestination.Location = new System.Drawing.Point(13, 50);
            this.dgvDestination.Name = "dgvDestination";
            this.dgvDestination.RowTemplate.Height = 23;
            this.dgvDestination.Size = new System.Drawing.Size(438, 192);
            this.dgvDestination.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(79, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(232, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(386, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.btnRefresh);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 383);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(941, 49);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 99);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(941, 284);
            this.panel5.TabIndex = 4;
            // 
            // Model_LocationCombine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 454);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Model_LocationCombine";
            this.Text = "Model_LocationCombine(储位调整)";
            this.Resize += new System.EventHandler(this.Model_LocationCombine_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.gbDestination.ResumeLayout(false);
            this.gbDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDestination)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMandt;
        private System.Windows.Forms.ToolStripStatusLabel tsslComcd;
        private System.Windows.Forms.ToolStripStatusLabel tsslUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel tsslWarning;
        private System.Windows.Forms.ToolStripStatusLabel tsslDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DataGridView dgvSource;
        private System.Windows.Forms.TextBox txtOldLocat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gbDestination;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNewLocat;
        private System.Windows.Forms.DataGridView dgvDestination;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}