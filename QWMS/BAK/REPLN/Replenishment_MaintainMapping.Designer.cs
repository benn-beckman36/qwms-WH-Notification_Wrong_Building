namespace QWMS
{
    partial class Replenishment_MaintainMapping
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Stock = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbInsmk = new System.Windows.Forms.ComboBox();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.txtMaxge = new System.Windows.Forms.TextBox();
            this.txtRepot = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(830, 114);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLocat);
            this.panel2.Controls.Add(this.txtMatnr);
            this.panel2.Controls.Add(this.cmbLgort);
            this.panel2.Controls.Add(this.cmbWerks);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 114);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plant";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Storage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Part No";
            // 
            // Stock
            // 
            this.Stock.AutoSize = true;
            this.Stock.Location = new System.Drawing.Point(107, 15);
            this.Stock.Name = "Stock";
            this.Stock.Size = new System.Drawing.Size(40, 15);
            this.Stock.TabIndex = 1;
            this.Stock.Text = "Stock";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Version";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(94, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Max Qty";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 15);
            this.label8.TabIndex = 4;
            this.label8.Text = "Repenishment Point";
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(136, 9);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 23);
            this.cmbWerks.TabIndex = 4;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(136, 35);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(123, 23);
            this.cmbLgort.TabIndex = 5;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk.FormattingEnabled = true;
            this.cmbInsmk.Location = new System.Drawing.Point(165, 11);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(123, 23);
            this.cmbInsmk.TabIndex = 5;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMatnr.Location = new System.Drawing.Point(136, 85);
            this.txtMatnr.MaxLength = 20;
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(123, 21);
            this.txtMatnr.TabIndex = 6;
            // 
            // txtLocat
            // 
            this.txtLocat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocat.Location = new System.Drawing.Point(136, 61);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(123, 21);
            this.txtLocat.TabIndex = 7;
            // 
            // txtCharge
            // 
            this.txtCharge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCharge.Location = new System.Drawing.Point(165, 37);
            this.txtCharge.MaxLength = 10;
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(123, 21);
            this.txtCharge.TabIndex = 8;
            // 
            // txtMaxge
            // 
            this.txtMaxge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxge.Location = new System.Drawing.Point(165, 63);
            this.txtMaxge.MaxLength = 10;
            this.txtMaxge.Name = "txtMaxge";
            this.txtMaxge.Size = new System.Drawing.Size(121, 21);
            this.txtMaxge.TabIndex = 9;
            // 
            // txtRepot
            // 
            this.txtRepot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRepot.Location = new System.Drawing.Point(165, 87);
            this.txtRepot.MaxLength = 10;
            this.txtRepot.Name = "txtRepot";
            this.txtRepot.Size = new System.Drawing.Size(123, 21);
            this.txtRepot.TabIndex = 10;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(324, 63);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 28);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(830, 22);
            this.statusStrip1.TabIndex = 6;
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
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(37, 120);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(760, 244);
            this.dgvData.TabIndex = 7;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(37, 370);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 28);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(163, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.cmbInsmk);
            this.panel3.Controls.Add(this.txtCharge);
            this.panel3.Controls.Add(this.txtRepot);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtMaxge);
            this.panel3.Controls.Add(this.Stock);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(413, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(417, 114);
            this.panel3.TabIndex = 12;
            // 
            // Replenishment_MaintainMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 434);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "Replenishment_MaintainMapping";
            this.Text = "Maintain Mapping of Replenishment";
            this.Resize += new System.EventHandler(this.Replenishment_MaintainMapping_Resize);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Stock;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbInsmk;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.TextBox txtRepot;
        private System.Windows.Forms.TextBox txtMaxge;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel3;
    }
}