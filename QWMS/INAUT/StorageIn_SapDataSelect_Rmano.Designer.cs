namespace QWMS
{
    partial class StorageIn_SapDataSelect_Rmano
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.dtpOudat = new System.Windows.Forms.DateTimePicker();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dtgData = new System.Windows.Forms.DataGrid();
            this.txtRmano = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(214, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(72, 24);
            this.btnQuery.TabIndex = 71;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(96, 5);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(16, 24);
            this.chkDate.TabIndex = 70;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // dtpOudat
            // 
            this.dtpOudat.CalendarFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpOudat.Enabled = false;
            this.dtpOudat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpOudat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOudat.Location = new System.Drawing.Point(112, 5);
            this.dtpOudat.Name = "dtpOudat";
            this.dtpOudat.Size = new System.Drawing.Size(96, 22);
            this.dtpOudat.TabIndex = 69;
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(174, 362);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 32);
            this.btnReturn.TabIndex = 68;
            this.btnReturn.Text = "Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(22, 362);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 32);
            this.btnSelect.TabIndex = 67;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dtgData
            // 
            this.dtgData.DataMember = "";
            this.dtgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgData.Location = new System.Drawing.Point(6, 55);
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(280, 303);
            this.dtgData.TabIndex = 66;
            // 
            // txtRmano
            // 
            this.txtRmano.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRmano.Location = new System.Drawing.Point(96, 30);
            this.txtRmano.MaxLength = 20;
            this.txtRmano.Name = "txtRmano";
            this.txtRmano.Size = new System.Drawing.Size(112, 22);
            this.txtRmano.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 23);
            this.label3.TabIndex = 73;
            this.label3.Text = "RMA No.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StorageIn_SapDataSelect_Rmano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 398);
            this.Controls.Add(this.txtRmano);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.chkDate);
            this.Controls.Add(this.dtpOudat);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dtgData);
            this.Name = "StorageIn_SapDataSelect_Rmano";
            this.Text = "StorageIn_SapDataSelect_Rmano";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.DateTimePicker dtpOudat;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGrid dtgData;
        private System.Windows.Forms.TextBox txtRmano;
        private System.Windows.Forms.Label label3;
    }
}