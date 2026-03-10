
namespace QWMS
{
    partial class Manage_PrintReLabel
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.btnPrintSetting = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtBounnd = new System.Windows.Forms.TextBox();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtBoxID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPartNumberRev = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(54, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "BarCode";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(154, 155);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(786, 28);
            this.txtBarCode.TabIndex = 11;
            this.txtBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarCode_KeyPress);
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
            this.groupBox1.Location = new System.Drawing.Point(58, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(474, 122);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set Up";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIP.Location = new System.Drawing.Point(246, 28);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 30, 4, 30);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(44, 18);
            this.lblIP.TabIndex = 39;
            this.lblIP.Text = "IP：";
            // 
            // btnPrintSetting
            // 
            this.btnPrintSetting.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintSetting.Location = new System.Drawing.Point(292, 66);
            this.btnPrintSetting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintSetting.Name = "btnPrintSetting";
            this.btnPrintSetting.Size = new System.Drawing.Size(159, 38);
            this.btnPrintSetting.TabIndex = 4;
            this.btnPrintSetting.Text = "SetUp";
            this.btnPrintSetting.UseVisualStyleBackColor = true;
            this.btnPrintSetting.Click += new System.EventHandler(this.btnPrintSetting_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(297, 26);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 22, 3, 22);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(152, 28);
            this.txtIP.TabIndex = 40;
            // 
            // txtBounnd
            // 
            this.txtBounnd.Location = new System.Drawing.Point(90, 66);
            this.txtBounnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBounnd.Name = "txtBounnd";
            this.txtBounnd.Size = new System.Drawing.Size(151, 28);
            this.txtBounnd.TabIndex = 3;
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(88, 26);
            this.txtCom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(152, 28);
            this.txtCom.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "BAUD：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port：";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Location = new System.Drawing.Point(781, 407);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(159, 38);
            this.btnPrint.TabIndex = 38;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtBoxID
            // 
            this.txtBoxID.Location = new System.Drawing.Point(196, 205);
            this.txtBoxID.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxID.Name = "txtBoxID";
            this.txtBoxID.Size = new System.Drawing.Size(184, 28);
            this.txtBoxID.TabIndex = 44;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(106, 211);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 24);
            this.label7.TabIndex = 43;
            this.label7.Text = "Box ID";
            // 
            // txtPartNumberRev
            // 
            this.txtPartNumberRev.Location = new System.Drawing.Point(196, 251);
            this.txtPartNumberRev.Margin = new System.Windows.Forms.Padding(4);
            this.txtPartNumberRev.Name = "txtPartNumberRev";
            this.txtPartNumberRev.Size = new System.Drawing.Size(184, 28);
            this.txtPartNumberRev.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(17, 255);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(171, 24);
            this.label8.TabIndex = 45;
            this.label8.Text = "Part Number Rev";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.Location = new System.Drawing.Point(624, 205);
            this.txtPartNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.Size = new System.Drawing.Size(242, 28);
            this.txtPartNumber.TabIndex = 52;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(487, 211);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 24);
            this.label11.TabIndex = 51;
            this.label11.Text = "Part Number";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(624, 251);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(242, 28);
            this.txtQty.TabIndex = 54;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(549, 255);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 24);
            this.label12.TabIndex = 53;
            this.label12.Text = "Total";
            // 
            // Manage_PrintReLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 544);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPartNumber);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPartNumberRev);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBoxID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBarCode);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Manage_PrintReLabel";
            this.Text = "Manage_PrintReLabel";
            this.Activated += new System.EventHandler(this.IQC_PrintExpDate_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Button btnPrintSetting;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtBounnd;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtBoxID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPartNumberRev;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPartNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label12;
    }
}