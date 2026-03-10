namespace QWMS
{
    partial class StorageIn_OnLineIn_101Location_Split
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
            this.txtLgort = new System.Windows.Forms.TextBox();
            this.lblLgort = new System.Windows.Forms.Label();
            this.txtpart = new System.Windows.Forms.TextBox();
            this.lblPartNO = new System.Windows.Forms.Label();
            this.txtDocumentNO = new System.Windows.Forms.TextBox();
            this.lblDocumentNO = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.txtSplitTqty = new System.Windows.Forms.TextBox();
            this.lblSplitTqty = new System.Windows.Forms.Label();
            this.txtTqty = new System.Windows.Forms.TextBox();
            this.lblTQTY = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvSplitData = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.txtqty);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtLgort);
            this.panel1.Controls.Add(this.lblLgort);
            this.panel1.Controls.Add(this.txtpart);
            this.panel1.Controls.Add(this.lblPartNO);
            this.panel1.Controls.Add(this.txtDocumentNO);
            this.panel1.Controls.Add(this.lblDocumentNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 70);
            this.panel1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(373, 38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "拆分";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(233, 41);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(100, 21);
            this.txtqty.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "数量：";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(70, 41);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(121, 21);
            this.txtLocation.TabIndex = 7;
            this.txtLocation.DoubleClick += new System.EventHandler(this.txtLocation_DoubleClick);
            this.txtLocation.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtLocation_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "储位：";
            // 
            // txtLgort
            // 
            this.txtLgort.Location = new System.Drawing.Point(373, 10);
            this.txtLgort.Name = "txtLgort";
            this.txtLgort.Size = new System.Drawing.Size(100, 21);
            this.txtLgort.TabIndex = 5;
            // 
            // lblLgort
            // 
            this.lblLgort.AutoSize = true;
            this.lblLgort.Location = new System.Drawing.Point(338, 13);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(41, 12);
            this.lblLgort.TabIndex = 4;
            this.lblLgort.Text = "仓别：";
            // 
            // txtpart
            // 
            this.txtpart.Location = new System.Drawing.Point(233, 9);
            this.txtpart.Name = "txtpart";
            this.txtpart.Size = new System.Drawing.Size(100, 21);
            this.txtpart.TabIndex = 3;
            // 
            // lblPartNO
            // 
            this.lblPartNO.AutoSize = true;
            this.lblPartNO.Location = new System.Drawing.Point(197, 13);
            this.lblPartNO.Name = "lblPartNO";
            this.lblPartNO.Size = new System.Drawing.Size(41, 12);
            this.lblPartNO.TabIndex = 2;
            this.lblPartNO.Text = "料号：";
            // 
            // txtDocumentNO
            // 
            this.txtDocumentNO.Location = new System.Drawing.Point(70, 8);
            this.txtDocumentNO.Name = "txtDocumentNO";
            this.txtDocumentNO.Size = new System.Drawing.Size(121, 21);
            this.txtDocumentNO.TabIndex = 1;
            // 
            // lblDocumentNO
            // 
            this.lblDocumentNO.AutoSize = true;
            this.lblDocumentNO.Location = new System.Drawing.Point(13, 13);
            this.lblDocumentNO.Name = "lblDocumentNO";
            this.lblDocumentNO.Size = new System.Drawing.Size(65, 12);
            this.lblDocumentNO.TabIndex = 0;
            this.lblDocumentNO.Text = "扣帐编号：";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnrefresh);
            this.panel2.Controls.Add(this.btnexit);
            this.panel2.Controls.Add(this.txtSplitTqty);
            this.panel2.Controls.Add(this.lblSplitTqty);
            this.panel2.Controls.Add(this.txtTqty);
            this.panel2.Controls.Add(this.lblTQTY);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.dgvSplitData);
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(489, 314);
            this.panel2.TabIndex = 2;
            // 
            // btnrefresh
            // 
            this.btnrefresh.Location = new System.Drawing.Point(192, 272);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(100, 30);
            this.btnrefresh.TabIndex = 15;
            this.btnrefresh.Text = "重置";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(327, 272);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(100, 30);
            this.btnexit.TabIndex = 14;
            this.btnexit.Text = "退出";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // txtSplitTqty
            // 
            this.txtSplitTqty.Enabled = false;
            this.txtSplitTqty.Location = new System.Drawing.Point(344, 15);
            this.txtSplitTqty.Name = "txtSplitTqty";
            this.txtSplitTqty.Size = new System.Drawing.Size(85, 21);
            this.txtSplitTqty.TabIndex = 13;
            // 
            // lblSplitTqty
            // 
            this.lblSplitTqty.AutoSize = true;
            this.lblSplitTqty.Location = new System.Drawing.Point(271, 19);
            this.lblSplitTqty.Name = "lblSplitTqty";
            this.lblSplitTqty.Size = new System.Drawing.Size(77, 12);
            this.lblSplitTqty.TabIndex = 12;
            this.lblSplitTqty.Text = "拆分总数量：";
            // 
            // txtTqty
            // 
            this.txtTqty.Enabled = false;
            this.txtTqty.Location = new System.Drawing.Point(147, 14);
            this.txtTqty.Name = "txtTqty";
            this.txtTqty.Size = new System.Drawing.Size(89, 21);
            this.txtTqty.TabIndex = 11;
            // 
            // lblTQTY
            // 
            this.lblTQTY.AutoSize = true;
            this.lblTQTY.Location = new System.Drawing.Point(54, 19);
            this.lblTQTY.Name = "lblTQTY";
            this.lblTQTY.Size = new System.Drawing.Size(101, 12);
            this.lblTQTY.TabIndex = 11;
            this.lblTQTY.Text = "扣帐单号总数量：";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(57, 272);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
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
            this.dgvSplitData.Location = new System.Drawing.Point(20, 45);
            this.dgvSplitData.Name = "dgvSplitData";
            this.dgvSplitData.RowTemplate.Height = 23;
            this.dgvSplitData.Size = new System.Drawing.Size(448, 217);
            this.dgvSplitData.TabIndex = 0;
            // 
            // StorageIn_OnLineIn_101Location_Split
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 384);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "StorageIn_OnLineIn_101Location_Split";
            this.Text = "StorageIn_OnLineIn_101Location_Split";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplitData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtpart;
        private System.Windows.Forms.Label lblPartNO;
        private System.Windows.Forms.TextBox txtDocumentNO;
        private System.Windows.Forms.Label lblDocumentNO;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLgort;
        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvSplitData;
        private System.Windows.Forms.TextBox txtSplitTqty;
        private System.Windows.Forms.Label lblSplitTqty;
        private System.Windows.Forms.TextBox txtTqty;
        private System.Windows.Forms.Label lblTQTY;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnrefresh;
    }
}