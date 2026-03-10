namespace QWMS
{
    partial class Online_EC_Copare
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvECData = new System.Windows.Forms.DataGridView();
            this.dgvBOX = new System.Windows.Forms.DataGridView();
            this.lblwarning = new System.Windows.Forms.Label();
            this.btnCopare = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.lblQTY = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.txtBoxID = new System.Windows.Forms.TextBox();
            this.lblBoxID = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtSTNum = new System.Windows.Forms.TextBox();
            this.lblStorage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblECNum = new System.Windows.Forms.Label();
            this.txtGTNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPlant = new System.Windows.Forms.Label();
            this.lblGTNum = new System.Windows.Forms.Label();
            this.txtVbeln = new System.Windows.Forms.TextBox();
            this.txtBoxTNum = new System.Windows.Forms.TextBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.lblBoxTNum = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtECTNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblECTNum = new System.Windows.Forms.Label();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvECData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOX)).BeginInit();
            this.gbHeader.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.stbStatus);
            this.panel3.Controls.Add(this.dgvECData);
            this.panel3.Controls.Add(this.dgvBOX);
            this.panel3.Controls.Add(this.lblwarning);
            this.panel3.Controls.Add(this.btnCopare);
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.gbHeader);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblECTNum);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1033, 631);
            this.panel3.TabIndex = 34;
            // 
            // dgvECData
            // 
            this.dgvECData.AllowUserToAddRows = false;
            this.dgvECData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvECData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvECData.Location = new System.Drawing.Point(31, 168);
            this.dgvECData.Name = "dgvECData";
            this.dgvECData.ReadOnly = true;
            this.dgvECData.RowTemplate.Height = 24;
            this.dgvECData.Size = new System.Drawing.Size(973, 162);
            this.dgvECData.TabIndex = 32;
            // 
            // dgvBOX
            // 
            this.dgvBOX.AllowUserToAddRows = false;
            this.dgvBOX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBOX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBOX.Location = new System.Drawing.Point(29, 365);
            this.dgvBOX.Name = "dgvBOX";
            this.dgvBOX.ReadOnly = true;
            this.dgvBOX.RowTemplate.Height = 24;
            this.dgvBOX.Size = new System.Drawing.Size(975, 176);
            this.dgvBOX.TabIndex = 59;
            this.dgvBOX.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvBOX_RowsRemoved);
            // 
            // lblwarning
            // 
            this.lblwarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblwarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblwarning.ForeColor = System.Drawing.Color.Red;
            this.lblwarning.Location = new System.Drawing.Point(395, 560);
            this.lblwarning.Name = "lblwarning";
            this.lblwarning.Size = new System.Drawing.Size(346, 21);
            this.lblwarning.TabIndex = 59;
            this.lblwarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCopare
            // 
            this.btnCopare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopare.Enabled = false;
            this.btnCopare.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopare.Location = new System.Drawing.Point(35, 555);
            this.btnCopare.Name = "btnCopare";
            this.btnCopare.Size = new System.Drawing.Size(90, 30);
            this.btnCopare.TabIndex = 58;
            this.btnCopare.Text = "比对";
            this.btnCopare.Click += new System.EventHandler(this.btnCopare_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(270, 555);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(154, 555);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "重置";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.panel5);
            this.gbHeader.Controls.Add(this.btnConfirm);
            this.gbHeader.Controls.Add(this.txtSTNum);
            this.gbHeader.Controls.Add(this.lblStorage);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.lblECNum);
            this.gbHeader.Controls.Add(this.txtGTNum);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.lblPlant);
            this.gbHeader.Controls.Add(this.lblGTNum);
            this.gbHeader.Controls.Add(this.txtVbeln);
            this.gbHeader.Controls.Add(this.txtBoxTNum);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Controls.Add(this.lblBoxTNum);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.txtECTNum);
            this.gbHeader.Location = new System.Drawing.Point(3, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1033, 126);
            this.gbHeader.TabIndex = 21;
            this.gbHeader.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.txtLot);
            this.panel5.Controls.Add(this.txtQTY);
            this.panel5.Controls.Add(this.lblQTY);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.txtPartNo);
            this.panel5.Controls.Add(this.lblPartNo);
            this.panel5.Controls.Add(this.txtBoxID);
            this.panel5.Controls.Add(this.lblBoxID);
            this.panel5.Enabled = false;
            this.panel5.Location = new System.Drawing.Point(390, 9);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(631, 73);
            this.panel5.TabIndex = 38;
            // 
            // txtLot
            // 
            this.txtLot.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLot.Enabled = false;
            this.txtLot.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLot.Location = new System.Drawing.Point(59, 11);
            this.txtLot.MaxLength = 100;
            this.txtLot.Name = "txtLot";
            this.txtLot.Size = new System.Drawing.Size(181, 22);
            this.txtLot.TabIndex = 45;
            this.txtLot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLot_KeyPress);
            // 
            // txtQTY
            // 
            this.txtQTY.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQTY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQTY.Location = new System.Drawing.Point(295, 43);
            this.txtQTY.MaxLength = 100;
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(243, 22);
            this.txtQTY.TabIndex = 43;
            this.txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            // 
            // lblQTY
            // 
            this.lblQTY.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQTY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQTY.Location = new System.Drawing.Point(235, 43);
            this.lblQTY.Name = "lblQTY";
            this.lblQTY.Size = new System.Drawing.Size(54, 21);
            this.lblQTY.TabIndex = 42;
            this.lblQTY.Text = "QTY";
            this.lblQTY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 21);
            this.label2.TabIndex = 44;
            this.label2.Text = "Lotcode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPartNo
            // 
            this.txtPartNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartNo.Location = new System.Drawing.Point(60, 41);
            this.txtPartNo.MaxLength = 100;
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(180, 22);
            this.txtPartNo.TabIndex = 41;
            this.txtPartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPartNo_KeyPress);
            // 
            // lblPartNo
            // 
            this.lblPartNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNo.Location = new System.Drawing.Point(7, 44);
            this.lblPartNo.Name = "lblPartNo";
            this.lblPartNo.Size = new System.Drawing.Size(54, 21);
            this.lblPartNo.TabIndex = 40;
            this.lblPartNo.Text = "PartNo";
            this.lblPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxID
            // 
            this.txtBoxID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxID.Enabled = false;
            this.txtBoxID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxID.Location = new System.Drawing.Point(295, 12);
            this.txtBoxID.MaxLength = 100;
            this.txtBoxID.Name = "txtBoxID";
            this.txtBoxID.Size = new System.Drawing.Size(243, 22);
            this.txtBoxID.TabIndex = 39;
            this.txtBoxID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxID_KeyPress);
            // 
            // lblBoxID
            // 
            this.lblBoxID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBoxID.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxID.Location = new System.Drawing.Point(235, 12);
            this.lblBoxID.Name = "lblBoxID";
            this.lblBoxID.Size = new System.Drawing.Size(54, 21);
            this.lblBoxID.TabIndex = 39;
            this.lblBoxID.Text = "BoxID";
            this.lblBoxID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(284, 51);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 30);
            this.btnConfirm.TabIndex = 17;
            this.btnConfirm.Text = "Query";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtSTNum
            // 
            this.txtSTNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSTNum.Enabled = false;
            this.txtSTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSTNum.Location = new System.Drawing.Point(721, 88);
            this.txtSTNum.MaxLength = 100;
            this.txtSTNum.Name = "txtSTNum";
            this.txtSTNum.Size = new System.Drawing.Size(94, 22);
            this.txtSTNum.TabIndex = 65;
            // 
            // lblStorage
            // 
            this.lblStorage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStorage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorage.Location = new System.Drawing.Point(215, -157);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(65, 21);
            this.lblStorage.TabIndex = 23;
            this.lblStorage.Text = "Storage";
            this.lblStorage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(645, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 21);
            this.label1.TabIndex = 66;
            this.label1.Text = "STNum";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblECNum
            // 
            this.lblECNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECNum.Location = new System.Drawing.Point(27, 55);
            this.lblECNum.Name = "lblECNum";
            this.lblECNum.Size = new System.Drawing.Size(54, 21);
            this.lblECNum.TabIndex = 19;
            this.lblECNum.Text = "ECNum";
            this.lblECNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGTNum
            // 
            this.txtGTNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGTNum.Enabled = false;
            this.txtGTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGTNum.Location = new System.Drawing.Point(514, 87);
            this.txtGTNum.MaxLength = 100;
            this.txtGTNum.Name = "txtGTNum";
            this.txtGTNum.Size = new System.Drawing.Size(94, 22);
            this.txtGTNum.TabIndex = 63;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 21);
            this.label3.TabIndex = 60;
            this.label3.Text = "ECTNum";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPlant
            // 
            this.lblPlant.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlant.Location = new System.Drawing.Point(23, 20);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(58, 21);
            this.lblPlant.TabIndex = 25;
            this.lblPlant.Text = "Plant";
            this.lblPlant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGTNum
            // 
            this.lblGTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGTNum.Location = new System.Drawing.Point(438, 89);
            this.lblGTNum.Name = "lblGTNum";
            this.lblGTNum.Size = new System.Drawing.Size(70, 21);
            this.lblGTNum.TabIndex = 64;
            this.lblGTNum.Text = "GTNum";
            this.lblGTNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVbeln
            // 
            this.txtVbeln.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVbeln.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVbeln.Location = new System.Drawing.Point(87, 54);
            this.txtVbeln.MaxLength = 100;
            this.txtVbeln.Name = "txtVbeln";
            this.txtVbeln.Size = new System.Drawing.Size(174, 22);
            this.txtVbeln.TabIndex = 15;
            this.txtVbeln.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVbeln_KeyPress);
            // 
            // txtBoxTNum
            // 
            this.txtBoxTNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxTNum.Enabled = false;
            this.txtBoxTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxTNum.Location = new System.Drawing.Point(290, 87);
            this.txtBoxTNum.MaxLength = 100;
            this.txtBoxTNum.Name = "txtBoxTNum";
            this.txtBoxTNum.Size = new System.Drawing.Size(94, 22);
            this.txtBoxTNum.TabIndex = 61;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLgort.ItemHeight = 16;
            this.cmbLgort.Location = new System.Drawing.Point(284, 17);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(100, 24);
            this.cmbLgort.TabIndex = 22;
            // 
            // lblBoxTNum
            // 
            this.lblBoxTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoxTNum.Location = new System.Drawing.Point(213, 90);
            this.lblBoxTNum.Name = "lblBoxTNum";
            this.lblBoxTNum.Size = new System.Drawing.Size(71, 21);
            this.lblBoxTNum.TabIndex = 62;
            this.lblBoxTNum.Text = "BoxTNum";
            this.lblBoxTNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWerks.ItemHeight = 16;
            this.cmbWerks.Location = new System.Drawing.Point(87, 17);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(120, 24);
            this.cmbWerks.TabIndex = 24;
            // 
            // txtECTNum
            // 
            this.txtECTNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtECTNum.Enabled = false;
            this.txtECTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECTNum.Location = new System.Drawing.Point(101, 87);
            this.txtECTNum.MaxLength = 100;
            this.txtECTNum.Name = "txtECTNum";
            this.txtECTNum.Size = new System.Drawing.Size(94, 22);
            this.txtECTNum.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.TabIndex = 60;
            this.label4.Text = "EC Info";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblECTNum
            // 
            this.lblECTNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECTNum.Location = new System.Drawing.Point(32, 339);
            this.lblECTNum.Name = "lblECTNum";
            this.lblECTNum.Size = new System.Drawing.Size(70, 21);
            this.lblECTNum.TabIndex = 60;
            this.lblECTNum.Text = "BOXID";
            this.lblECTNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 605);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1033, 26);
            this.stbStatus.TabIndex = 61;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // Online_EC_Copare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 631);
            this.Controls.Add(this.panel3);
            this.Name = "Online_EC_Copare";
            this.Text = "Online_EC_Copare";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvECData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBOX)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Label lblECNum;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.TextBox txtVbeln;
        private System.Windows.Forms.Button btnCopare;
        private System.Windows.Forms.DataGridView dgvBOX;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.Label lblPartNo;
        private System.Windows.Forms.TextBox txtBoxID;
        private System.Windows.Forms.Label lblBoxID;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.Label lblQTY;
        private System.Windows.Forms.Label lblwarning;
        private System.Windows.Forms.TextBox txtBoxTNum;
        private System.Windows.Forms.Label lblBoxTNum;
        private System.Windows.Forms.TextBox txtECTNum;
        private System.Windows.Forms.Label lblECTNum;
        private System.Windows.Forms.TextBox txtSTNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGTNum;
        private System.Windows.Forms.Label lblGTNum;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvECData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
    }
}