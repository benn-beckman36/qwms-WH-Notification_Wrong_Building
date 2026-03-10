namespace QWMS
{
    partial class Admin_Material_personnel
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbox3 = new System.Windows.Forms.GroupBox();
            this.RBadd = new System.Windows.Forms.RadioButton();
            this.RBdel = new System.Windows.Forms.RadioButton();
            this.RBmdf = new System.Windows.Forms.RadioButton();
            this.gbPE = new System.Windows.Forms.GroupBox();
            this.txtPenm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPeID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbMQE = new System.Windows.Forms.GroupBox();
            this.txtMqcnm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMqcID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSAVE = new System.Windows.Forms.Button();
            this.btnREFRESH = new System.Windows.Forms.Button();
            this.btnQUERY = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbox3.SuspendLayout();
            this.gbPE.SuspendLayout();
            this.gbMQE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbLgort);
            this.splitContainer1.Panel1.Controls.Add(this.cmbWerks);
            this.splitContainer1.Panel1.Controls.Add(this.txtMatnr);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.gbox3);
            this.splitContainer1.Panel1.Controls.Add(this.gbPE);
            this.splitContainer1.Panel1.Controls.Add(this.gbMQE);
            this.splitContainer1.Panel1.Controls.Add(this.btnSAVE);
            this.splitContainer1.Panel1.Controls.Add(this.btnREFRESH);
            this.splitContainer1.Panel1.Controls.Add(this.btnQUERY);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvData);
            this.splitContainer1.Size = new System.Drawing.Size(779, 447);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(510, 23);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(100, 21);
            this.txtMatnr.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(460, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "料号：";
            // 
            // gbox3
            // 
            this.gbox3.Controls.Add(this.RBadd);
            this.gbox3.Controls.Add(this.RBdel);
            this.gbox3.Controls.Add(this.RBmdf);
            this.gbox3.Location = new System.Drawing.Point(12, 20);
            this.gbox3.Name = "gbox3";
            this.gbox3.Size = new System.Drawing.Size(119, 137);
            this.gbox3.TabIndex = 10;
            this.gbox3.TabStop = false;
            this.gbox3.Text = "Function";
            // 
            // RBadd
            // 
            this.RBadd.AutoSize = true;
            this.RBadd.Checked = true;
            this.RBadd.Location = new System.Drawing.Point(6, 34);
            this.RBadd.Name = "RBadd";
            this.RBadd.Size = new System.Drawing.Size(47, 16);
            this.RBadd.TabIndex = 0;
            this.RBadd.TabStop = true;
            this.RBadd.Text = "新增";
            this.RBadd.UseVisualStyleBackColor = true;
            this.RBadd.CheckedChanged += new System.EventHandler(this.RBadd_CheckedChanged);
            // 
            // RBdel
            // 
            this.RBdel.AutoSize = true;
            this.RBdel.Location = new System.Drawing.Point(6, 68);
            this.RBdel.Name = "RBdel";
            this.RBdel.Size = new System.Drawing.Size(47, 16);
            this.RBdel.TabIndex = 1;
            this.RBdel.Text = "删除";
            this.RBdel.UseVisualStyleBackColor = true;
            this.RBdel.CheckedChanged += new System.EventHandler(this.RBdel_CheckedChanged);
            // 
            // RBmdf
            // 
            this.RBmdf.AutoSize = true;
            this.RBmdf.Location = new System.Drawing.Point(6, 103);
            this.RBmdf.Name = "RBmdf";
            this.RBmdf.Size = new System.Drawing.Size(47, 16);
            this.RBmdf.TabIndex = 2;
            this.RBmdf.Text = "修改";
            this.RBmdf.UseVisualStyleBackColor = true;
            this.RBmdf.CheckedChanged += new System.EventHandler(this.RBmdf_CheckedChanged);
            // 
            // gbPE
            // 
            this.gbPE.Controls.Add(this.txtPenm);
            this.gbPE.Controls.Add(this.label5);
            this.gbPE.Controls.Add(this.txtPeID);
            this.gbPE.Controls.Add(this.label6);
            this.gbPE.Location = new System.Drawing.Point(398, 72);
            this.gbPE.Name = "gbPE";
            this.gbPE.Size = new System.Drawing.Size(171, 85);
            this.gbPE.TabIndex = 9;
            this.gbPE.TabStop = false;
            this.gbPE.Text = "PE :";
            // 
            // txtPenm
            // 
            this.txtPenm.Location = new System.Drawing.Point(53, 52);
            this.txtPenm.Name = "txtPenm";
            this.txtPenm.Size = new System.Drawing.Size(100, 21);
            this.txtPenm.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "姓名：";
            // 
            // txtPeID
            // 
            this.txtPeID.Location = new System.Drawing.Point(53, 20);
            this.txtPeID.Name = "txtPeID";
            this.txtPeID.Size = new System.Drawing.Size(100, 21);
            this.txtPeID.TabIndex = 1;
            this.txtPeID.TextChanged += new System.EventHandler(this.txtPeID_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "工号：";
            // 
            // gbMQE
            // 
            this.gbMQE.Controls.Add(this.txtMqcnm);
            this.gbMQE.Controls.Add(this.label4);
            this.gbMQE.Controls.Add(this.txtMqcID);
            this.gbMQE.Controls.Add(this.label3);
            this.gbMQE.Location = new System.Drawing.Point(157, 72);
            this.gbMQE.Name = "gbMQE";
            this.gbMQE.Size = new System.Drawing.Size(171, 85);
            this.gbMQE.TabIndex = 8;
            this.gbMQE.TabStop = false;
            this.gbMQE.Text = "MQC :";
            // 
            // txtMqcnm
            // 
            this.txtMqcnm.Location = new System.Drawing.Point(53, 51);
            this.txtMqcnm.Name = "txtMqcnm";
            this.txtMqcnm.Size = new System.Drawing.Size(100, 21);
            this.txtMqcnm.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "姓名：";
            // 
            // txtMqcID
            // 
            this.txtMqcID.Location = new System.Drawing.Point(53, 20);
            this.txtMqcID.Name = "txtMqcID";
            this.txtMqcID.Size = new System.Drawing.Size(100, 21);
            this.txtMqcID.TabIndex = 1;
            this.txtMqcID.TextChanged += new System.EventHandler(this.txtMqcID_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "工号：";
            // 
            // btnSAVE
            // 
            this.btnSAVE.Location = new System.Drawing.Point(646, 134);
            this.btnSAVE.Name = "btnSAVE";
            this.btnSAVE.Size = new System.Drawing.Size(75, 23);
            this.btnSAVE.TabIndex = 7;
            this.btnSAVE.Text = "save";
            this.btnSAVE.UseVisualStyleBackColor = true;
            this.btnSAVE.Click += new System.EventHandler(this.btnSAVE_Click);
            // 
            // btnREFRESH
            // 
            this.btnREFRESH.Location = new System.Drawing.Point(646, 80);
            this.btnREFRESH.Name = "btnREFRESH";
            this.btnREFRESH.Size = new System.Drawing.Size(75, 23);
            this.btnREFRESH.TabIndex = 6;
            this.btnREFRESH.Text = "refresh";
            this.btnREFRESH.UseVisualStyleBackColor = true;
            this.btnREFRESH.Click += new System.EventHandler(this.btnREFRESH_Click);
            // 
            // btnQUERY
            // 
            this.btnQUERY.Location = new System.Drawing.Point(646, 27);
            this.btnQUERY.Name = "btnQUERY";
            this.btnQUERY.Size = new System.Drawing.Size(75, 23);
            this.btnQUERY.TabIndex = 5;
            this.btnQUERY.Text = "query";
            this.btnQUERY.UseVisualStyleBackColor = true;
            this.btnQUERY.Click += new System.EventHandler(this.btnQUERY_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(313, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "仓别:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(145, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "厂区:";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(779, 280);
            this.dgvData.TabIndex = 0;
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(199, 23);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(90, 20);
            this.cmbWerks.TabIndex = 13;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(367, 24);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(78, 20);
            this.cmbLgort.TabIndex = 14;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(779, 22);
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
            // Admin_Material_personnel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 447);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Admin_Material_personnel";
            this.Text = "Admin_Material_personnel";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gbox3.ResumeLayout(false);
            this.gbox3.PerformLayout();
            this.gbPE.ResumeLayout(false);
            this.gbPE.PerformLayout();
            this.gbMQE.ResumeLayout(false);
            this.gbMQE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton RBmdf;
        private System.Windows.Forms.RadioButton RBdel;
        private System.Windows.Forms.RadioButton RBadd;
        private System.Windows.Forms.GroupBox gbMQE;
        private System.Windows.Forms.Button btnSAVE;
        private System.Windows.Forms.Button btnREFRESH;
        private System.Windows.Forms.Button btnQUERY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPE;
        private System.Windows.Forms.TextBox txtPenm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPeID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMqcnm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMqcID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox gbox3;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
    }
}