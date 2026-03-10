
namespace QWMS
{
    partial class Admin_LocationPrint
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btFile = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbSFour = new System.Windows.Forms.RadioButton();
            this.rdbLFour = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(222, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(466, 182);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printer Setting";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(117, 73);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(191, 31);
            this.txtIP.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(262, 285);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(251, 31);
            this.txtFile.TabIndex = 1;
            this.txtFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFile_DragDrop);
            this.txtFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFile_DragEnter);
            // 
            // btFile
            // 
            this.btFile.Location = new System.Drawing.Point(58, 280);
            this.btFile.Margin = new System.Windows.Forms.Padding(4);
            this.btFile.Name = "btFile";
            this.btFile.Size = new System.Drawing.Size(136, 34);
            this.btFile.TabIndex = 2;
            this.btFile.Text = "Choose File";
            this.btFile.UseVisualStyleBackColor = true;
            this.btFile.Click += new System.EventHandler(this.btFile_Click);
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(58, 343);
            this.btPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(85, 34);
            this.btPrint.TabIndex = 3;
            this.btPrint.Text = "Print";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(204, 442);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 24);
            this.lblStatus.TabIndex = 4;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
            // 
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(571, 285);
            this.lnkSample.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(106, 32);
            this.lnkSample.TabIndex = 41;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "Sample";
            this.lnkSample.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSample_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbSFour);
            this.groupBox2.Controls.Add(this.rdbLFour);
            this.groupBox2.Location = new System.Drawing.Point(58, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 182);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location Setting";
            // 
            // rdbSFour
            // 
            this.rdbSFour.AutoSize = true;
            this.rdbSFour.Location = new System.Drawing.Point(17, 115);
            this.rdbSFour.Name = "rdbSFour";
            this.rdbSFour.Size = new System.Drawing.Size(113, 28);
            this.rdbSFour.TabIndex = 1;
            this.rdbSFour.TabStop = true;
            this.rdbSFour.Text = "S -Label";
            this.rdbSFour.UseVisualStyleBackColor = true;
            // 
            // rdbLFour
            // 
            this.rdbLFour.AutoSize = true;
            this.rdbLFour.Location = new System.Drawing.Point(17, 55);
            this.rdbLFour.Name = "rdbLFour";
            this.rdbLFour.Size = new System.Drawing.Size(110, 28);
            this.rdbLFour.TabIndex = 0;
            this.rdbLFour.TabStop = true;
            this.rdbLFour.Text = "L -Label";
            this.rdbLFour.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(410, 387);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(332, 113);
            this.label2.TabIndex = 43;
            this.label2.Text = "说明：\r\n1.大-储位Label规格：90*40MM\r\n2.小-储位Label规格：70*30MM\r\n3. 储位编码长度：4-6位";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10.2F);
            this.btnRefresh.Location = new System.Drawing.Point(208, 344);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 33);
            this.btnRefresh.TabIndex = 44;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Admin_LocationPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 601);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lnkSample);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin_LocationPrint";
            this.Text = "Admin_LocationPrint";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btFile;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.LinkLabel lnkSample;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbSFour;
        private System.Windows.Forms.RadioButton rdbLFour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefresh;
    }
}