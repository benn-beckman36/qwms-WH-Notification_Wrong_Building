namespace QWMS
{
    partial class TransferIn_101_Split
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLgort = new System.Windows.Forms.Label();
            this.lblPartNO = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.txtSplitTqty = new System.Windows.Forms.TextBox();
            this.lblSplitTqty = new System.Windows.Forms.Label();
            this.lblTQTY = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSplitData = new System.Windows.Forms.DataGridView();
            this.lblMatnr = new System.Windows.Forms.Label();
            this.lblStorage = new System.Windows.Forms.Label();
            this.lblToqty = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStorage);
            this.panel1.Controls.Add(this.lblMatnr);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.txtqty);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblLgort);
            this.panel1.Controls.Add(this.lblPartNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 88);
            this.panel1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(497, 48);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(133, 29);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "拆分";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(311, 51);
            this.txtqty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(132, 25);
            this.txtqty.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "数量：";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(93, 51);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(160, 25);
            this.txtLocation.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "储位：";
            // 
            // lblLgort
            // 
            this.lblLgort.AutoSize = true;
            this.lblLgort.Location = new System.Drawing.Point(263, 16);
            this.lblLgort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(52, 15);
            this.lblLgort.TabIndex = 4;
            this.lblLgort.Text = "仓别：";
            // 
            // lblPartNO
            // 
            this.lblPartNO.AutoSize = true;
            this.lblPartNO.Location = new System.Drawing.Point(16, 16);
            this.lblPartNO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPartNO.Name = "lblPartNO";
            this.lblPartNO.Size = new System.Drawing.Size(52, 15);
            this.lblPartNO.TabIndex = 2;
            this.lblPartNO.Text = "料号：";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lblToqty);
            this.panel2.Controls.Add(this.btnrefresh);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.txtSplitTqty);
            this.panel2.Controls.Add(this.lblSplitTqty);
            this.panel2.Controls.Add(this.lblTQTY);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.dgvSplitData);
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(652, 392);
            this.panel2.TabIndex = 2;
            // 
            // btnrefresh
            // 
            this.btnrefresh.Location = new System.Drawing.Point(256, 340);
            this.btnrefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(133, 38);
            this.btnrefresh.TabIndex = 15;
            this.btnrefresh.Text = "重置";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(436, 340);
            this.btnexit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(133, 38);
            this.btnexit.TabIndex = 14;
            this.btnexit.Text = "退出";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // txtSplitTqty
            // 
            this.txtSplitTqty.Enabled = false;
            this.txtSplitTqty.Location = new System.Drawing.Point(459, 19);
            this.txtSplitTqty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSplitTqty.Name = "txtSplitTqty";
            this.txtSplitTqty.Size = new System.Drawing.Size(112, 25);
            this.txtSplitTqty.TabIndex = 13;
            // 
            // lblSplitTqty
            // 
            this.lblSplitTqty.AutoSize = true;
            this.lblSplitTqty.Location = new System.Drawing.Point(361, 24);
            this.lblSplitTqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSplitTqty.Name = "lblSplitTqty";
            this.lblSplitTqty.Size = new System.Drawing.Size(97, 15);
            this.lblSplitTqty.TabIndex = 12;
            this.lblSplitTqty.Text = "拆分总数量：";
            // 
            // lblTQTY
            // 
            this.lblTQTY.AutoSize = true;
            this.lblTQTY.Location = new System.Drawing.Point(24, 24);
            this.lblTQTY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTQTY.Name = "lblTQTY";
            this.lblTQTY.Size = new System.Drawing.Size(127, 15);
            this.lblTQTY.TabIndex = 11;
            this.lblTQTY.Text = "扣帐单号总数量：";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(76, 340);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 38);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvSplitData
            // 
            this.dgvSplitData.AllowUserToAddRows = false;
            this.dgvSplitData.AllowUserToDeleteRows = false;
            this.dgvSplitData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSplitData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSplitData.Location = new System.Drawing.Point(27, 56);
            this.dgvSplitData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSplitData.Name = "dgvSplitData";
            this.dgvSplitData.RowHeadersWidth = 51;
            this.dgvSplitData.RowTemplate.Height = 23;
            this.dgvSplitData.Size = new System.Drawing.Size(597, 271);
            this.dgvSplitData.TabIndex = 0;
            // 
            // lblMatnr
            // 
            this.lblMatnr.AutoSize = true;
            this.lblMatnr.Location = new System.Drawing.Point(90, 16);
            this.lblMatnr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMatnr.Name = "lblMatnr";
            this.lblMatnr.Size = new System.Drawing.Size(0, 15);
            this.lblMatnr.TabIndex = 11;
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.Location = new System.Drawing.Point(308, 16);
            this.lblStorage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(0, 15);
            this.lblStorage.TabIndex = 12;
            // 
            // lblToqty
            // 
            this.lblToqty.AutoSize = true;
            this.lblToqty.Location = new System.Drawing.Point(157, 24);
            this.lblToqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToqty.Name = "lblToqty";
            this.lblToqty.Size = new System.Drawing.Size(0, 15);
            this.lblToqty.TabIndex = 16;
            // 
            // TransferIn_101_Split
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 480);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TransferIn_101_Split";
            this.Text = "TransferIn_101_Split";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPartNO;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvSplitData;
        private System.Windows.Forms.TextBox txtSplitTqty;
        private System.Windows.Forms.Label lblSplitTqty;
        private System.Windows.Forms.Label lblTQTY;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Label lblMatnr;
        private System.Windows.Forms.Label lblToqty;
    }
}