namespace QWMS
{
    partial class InventoryCheck_Barcode
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
            this.lblLgort = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtLocatEnd = new System.Windows.Forms.TextBox();
            this.lblLocatStart = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lblWerks = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtLocatStart = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoChkLocat = new System.Windows.Forms.RadioButton();
            this.rdoChkLgort = new System.Windows.Forms.RadioButton();
            this.chkDiff = new System.Windows.Forms.CheckBox();
            this.rdoLocat = new System.Windows.Forms.RadioButton();
            this.rdoMatnr = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblDiffNum = new System.Windows.Forms.Label();
            this.lblDiffCount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblOrder = new System.Windows.Forms.Label();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.lblLocat = new System.Windows.Forms.Label();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.gbHeader.SuspendLayout();
            this.gbFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLgort
            // 
            this.lblLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLgort.Location = new System.Drawing.Point(7, 48);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(62, 23);
            this.lblLgort.TabIndex = 23;
            this.lblLgort.Text = "Storage";
            this.lblLgort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLgort.ItemHeight = 16;
            this.cmbLgort.Location = new System.Drawing.Point(73, 49);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(97, 24);
            this.cmbLgort.TabIndex = 22;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Controls.Add(this.txtLocatEnd);
            this.gbHeader.Controls.Add(this.lblLgort);
            this.gbHeader.Controls.Add(this.lblLocatStart);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.lblWerks);
            this.gbHeader.Controls.Add(this.lblTo);
            this.gbHeader.Controls.Add(this.txtLocatStart);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbHeader.Location = new System.Drawing.Point(138, 2);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(341, 113);
            this.gbHeader.TabIndex = 23;
            this.gbHeader.TabStop = false;
            this.gbHeader.Text = "Query Inventory";
            // 
            // txtLocatEnd
            // 
            this.txtLocatEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocatEnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocatEnd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocatEnd.Location = new System.Drawing.Point(223, 79);
            this.txtLocatEnd.MaxLength = 100;
            this.txtLocatEnd.Name = "txtLocatEnd";
            this.txtLocatEnd.Size = new System.Drawing.Size(87, 22);
            this.txtLocatEnd.TabIndex = 15;
            // 
            // lblLocatStart
            // 
            this.lblLocatStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocatStart.Location = new System.Drawing.Point(6, 78);
            this.lblLocatStart.Name = "lblLocatStart";
            this.lblLocatStart.Size = new System.Drawing.Size(105, 23);
            this.lblLocatStart.TabIndex = 47;
            this.lblLocatStart.Text = "Location From";
            this.lblLocatStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWerks.ItemHeight = 16;
            this.cmbWerks.Location = new System.Drawing.Point(73, 19);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(97, 24);
            this.cmbWerks.TabIndex = 24;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lblWerks
            // 
            this.lblWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWerks.Location = new System.Drawing.Point(13, 20);
            this.lblWerks.Name = "lblWerks";
            this.lblWerks.Size = new System.Drawing.Size(41, 23);
            this.lblWerks.TabIndex = 25;
            this.lblWerks.Text = "Plant";
            this.lblWerks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTo
            // 
            this.lblTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(192, 78);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(31, 23);
            this.lblTo.TabIndex = 49;
            this.lblTo.Text = "To ";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocatStart
            // 
            this.txtLocatStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocatStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocatStart.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocatStart.Location = new System.Drawing.Point(112, 79);
            this.txtLocatStart.MaxLength = 100;
            this.txtLocatStart.Name = "txtLocatStart";
            this.txtLocatStart.Size = new System.Drawing.Size(79, 22);
            this.txtLocatStart.TabIndex = 50;
            // 
            // txtPartNo
            // 
            this.txtPartNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartNo.Location = new System.Drawing.Point(559, 53);
            this.txtPartNo.MaxLength = 100;
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(96, 22);
            this.txtPartNo.TabIndex = 20;
            this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
            // 
            // txtQty
            // 
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(558, 84);
            this.txtQty.MaxLength = 100;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(96, 22);
            this.txtQty.TabIndex = 27;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // lblPartNo
            // 
            this.lblPartNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNo.Location = new System.Drawing.Point(493, 53);
            this.lblPartNo.Name = "lblPartNo";
            this.lblPartNo.Size = new System.Drawing.Size(67, 23);
            this.lblPartNo.TabIndex = 29;
            this.lblPartNo.Text = "Part No.";
            this.lblPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblQty
            // 
            this.lblQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(524, 83);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(37, 23);
            this.lblQty.TabIndex = 21;
            this.lblQty.Text = "Qty";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.rdoChkLocat);
            this.gbFunction.Controls.Add(this.rdoChkLgort);
            this.gbFunction.Enabled = false;
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(12, 2);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(117, 113);
            this.gbFunction.TabIndex = 48;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Check By";
            // 
            // rdoChkLocat
            // 
            this.rdoChkLocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoChkLocat.Location = new System.Drawing.Point(12, 68);
            this.rdoChkLocat.Name = "rdoChkLocat";
            this.rdoChkLocat.Size = new System.Drawing.Size(95, 24);
            this.rdoChkLocat.TabIndex = 1;
            this.rdoChkLocat.Text = "Location";
            this.rdoChkLocat.CheckedChanged += new System.EventHandler(this.rdoChkLocat_CheckedChanged);
            // 
            // rdoChkLgort
            // 
            this.rdoChkLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoChkLgort.Location = new System.Drawing.Point(12, 29);
            this.rdoChkLgort.Name = "rdoChkLgort";
            this.rdoChkLgort.Size = new System.Drawing.Size(89, 24);
            this.rdoChkLgort.TabIndex = 0;
            this.rdoChkLgort.Text = "Storage";
            this.rdoChkLgort.CheckedChanged += new System.EventHandler(this.rdoChkLgort_CheckedChanged);
            // 
            // chkDiff
            // 
            this.chkDiff.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkDiff.Location = new System.Drawing.Point(323, 114);
            this.chkDiff.Name = "chkDiff";
            this.chkDiff.Size = new System.Drawing.Size(99, 26);
            this.chkDiff.TabIndex = 39;
            this.chkDiff.Text = "差異報表";
            this.chkDiff.CheckedChanged += new System.EventHandler(this.chkDiff_CheckedChanged);
            // 
            // rdoLocat
            // 
            this.rdoLocat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoLocat.Location = new System.Drawing.Point(725, 115);
            this.rdoLocat.Name = "rdoLocat";
            this.rdoLocat.Size = new System.Drawing.Size(81, 24);
            this.rdoLocat.TabIndex = 1;
            this.rdoLocat.Text = "Location";
            this.rdoLocat.Click += new System.EventHandler(this.rdoLocat_Click);
            // 
            // rdoMatnr
            // 
            this.rdoMatnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdoMatnr.Location = new System.Drawing.Point(656, 115);
            this.rdoMatnr.Name = "rdoMatnr";
            this.rdoMatnr.Size = new System.Drawing.Size(79, 24);
            this.rdoMatnr.TabIndex = 0;
            this.rdoMatnr.Text = "Part No";
            this.rdoMatnr.Click += new System.EventHandler(this.rdoMatnr_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(665, 72);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(96, 38);
            this.btnConfirm.TabIndex = 35;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(16, 144);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(793, 201);
            this.dgvData.TabIndex = 34;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(321, 351);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 39);
            this.btnExit.TabIndex = 36;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotalCount.Location = new System.Drawing.Point(86, 118);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(68, 18);
            this.lblTotalCount.TabIndex = 37;
            this.lblTotalCount.Text = "0 records";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(219, 351);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 39);
            this.btnRefresh.TabIndex = 17;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(16, 351);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 39);
            this.btnPrint.TabIndex = 36;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(117, 351);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(96, 39);
            this.btnDownload.TabIndex = 42;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblDiffNum
            // 
            this.lblDiffNum.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiffNum.Location = new System.Drawing.Point(156, 117);
            this.lblDiffNum.Name = "lblDiffNum";
            this.lblDiffNum.Size = new System.Drawing.Size(90, 23);
            this.lblDiffNum.TabIndex = 51;
            this.lblDiffNum.Text = "差異筆數:";
            this.lblDiffNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDiffCount
            // 
            this.lblDiffCount.AutoSize = true;
            this.lblDiffCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDiffCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDiffCount.Location = new System.Drawing.Point(240, 118);
            this.lblDiffCount.Name = "lblDiffCount";
            this.lblDiffCount.Size = new System.Drawing.Size(68, 18);
            this.lblDiffCount.TabIndex = 52;
            this.lblDiffCount.Text = "0 records";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(13, 116);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(72, 23);
            this.lblTotal.TabIndex = 53;
            this.lblTotal.Text = "總筆數:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrder
            // 
            this.lblOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrder.Location = new System.Drawing.Point(587, 116);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(67, 23);
            this.lblOrder.TabIndex = 54;
            this.lblOrder.Text = "Order By";
            this.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 392);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(821, 26);
            this.stbStatus.TabIndex = 55;
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
            // lblLocat
            // 
            this.lblLocat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocat.Location = new System.Drawing.Point(488, 22);
            this.lblLocat.Name = "lblLocat";
            this.lblLocat.Size = new System.Drawing.Size(67, 23);
            this.lblLocat.TabIndex = 56;
            this.lblLocat.Text = "Location";
            this.lblLocat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocat
            // 
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocat.Location = new System.Drawing.Point(558, 22);
            this.txtLocat.MaxLength = 100;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(97, 22);
            this.txtLocat.TabIndex = 51;
            this.txtLocat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "InventoryCheck_Barcode.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // InventoryCheck_Barcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 418);
            this.Controls.Add(this.txtLocat);
            this.Controls.Add(this.lblLocat);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.lblOrder);
            this.Controls.Add(this.rdoLocat);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.rdoMatnr);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPartNo);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDiffCount);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.lblDiffNum);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblPartNo);
            this.Controls.Add(this.chkDiff);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.gbHeader);
            this.Name = "InventoryCheck_Barcode";
            this.Text = " InventoryCheck_Barcode";
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.gbFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.Label lblPartNo;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblWerks;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtLocatEnd;
        private System.Windows.Forms.Label lblLocatStart;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoChkLocat;
        private System.Windows.Forms.RadioButton rdoChkLgort;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rdoLocat;
        private System.Windows.Forms.RadioButton rdoMatnr;
        private System.Windows.Forms.CheckBox chkDiff;
        private System.Windows.Forms.TextBox txtLocatStart;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblDiffNum;
        private System.Windows.Forms.Label lblDiffCount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblOrder;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Label lblLocat;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
    }
}