using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Media;

namespace QWMS
{
    /// <summary>
    /// StorageIn_TransferIn_DateCode 篕璶磞瓃
    /// </summary>
    public class StorageIn_TransferIn_DateCode : System.Windows.Forms.Form
    {
        #region ﹍て
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.DateTimePicker dtpIndat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoAdd;
        private System.Windows.Forms.RadioButton rdoNew;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        #endregion
        private DataGridView dgvData;
        private Label lblData;
        private Label label5;
        private Panel panel5;
        private DataGridView dgvMatnrData;
        private GroupBox gbEdit;
        DataTable dtMatnr;
        private TextBox txtLockCode;
        private Label label6;
        private StorageData objStorageData;
        private CheckBox chkType101;
        private PlantData objPlantData;

        #region 砞璸ㄣ┮惠跑计

        private System.ComponentModel.Container components = null;

        public string Mandt
        {
            get
            {
                return strMandt;
            }
            set
            {
                strMandt = value;
            }
        }
        public string Comcd
        {
            get
            {
                return strComcd;
            }
            set
            {
                strComcd = value;
            }
        }
        public string Usrnm
        {
            get
            {
                return strUsrnm;
            }
            set
            {
                strUsrnm = value;
            }
        }
        public string Progid
        {
            get
            {
                return strProgid;
            }
            set
            {
                strProgid = value;
            }
        }
        public string Werks
        {
            get
            {
                return strWerks;
            }
            set
            {
                strWerks = value;
            }
        }
        public string Lgort
        {
            get
            {
                return strLgort;
            }
            set
            {
                strLgort = value;
            }
        }
        public string Locat
        {
            get
            {
                return this.txtLocat.Text.Trim();
            }
            set
            {
                this.txtLocat.Text = value;
            }
        }
        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }
        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }
        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }
        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }
        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }
        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
            }
        }
        #endregion

        #region 初始化页面
        public StorageIn_TransferIn_DateCode(ref UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                objPlantData = new QCI.QWMS.PlantData(UserData);
                objStorageData = new StorageData(UserData, Werks, Lgort);



                //浪琩舦
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    dtMatnr = new DataTable();
                    dtMatnr.Columns.Add("MATNR");
                    dtMatnr.Columns.Add("DACOD");
                    dtMatnr.Columns.Add("LIFNR");
                    dtMatnr.Columns.Add("CHARG");
                    dtMatnr.Columns.Add("LOCOD");
                    dtMatnr.Columns.Add("MENGE");
                    dtMatnr.Columns.Add("VEDAT");
                    dtMatnr.Columns.Add("ExpiryDate");
                    dtMatnr.Columns.Add("TASKID");
                    dtMatnr.Columns.Add("SERNO");

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// 砞璸ㄣや穿┮ゲ惠よ猭 - 叫づㄏノ祘Α絏絪胯竟э
        /// 硂よ猭ず甧
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkType101 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLockCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.dtpIndat = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.rdoNew = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.gbEdit = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvMatnrData = new System.Windows.Forms.DataGridView();
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.gbEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatnrData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 134);
            this.panel1.TabIndex = 33;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.chkType101);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.txtLockCode);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Controls.Add(this.btnConfirm);
            this.panel4.Location = new System.Drawing.Point(190, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1178, 129);
            this.panel4.TabIndex = 31;
            // 
            // chkType101
            // 
            this.chkType101.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkType101.Enabled = false;
            this.chkType101.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkType101.Location = new System.Drawing.Point(662, 87);
            this.chkType101.Name = "chkType101";
            this.chkType101.Size = new System.Drawing.Size(89, 24);
            this.chkType101.TabIndex = 57;
            this.chkType101.Text = "Type101";
            this.chkType101.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.OrangeRed;
            this.label6.Location = new System.Drawing.Point(467, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(342, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "ex:PartNo;DateCode;VendorCode;LotCode;Menge";
            // 
            // txtLockCode
            // 
            this.txtLockCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLockCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtLockCode.Location = new System.Drawing.Point(500, 27);
            this.txtLockCode.Name = "txtLockCode";
            this.txtLockCode.Size = new System.Drawing.Size(666, 21);
            this.txtLockCode.TabIndex = 30;
            this.txtLockCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLockCode_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(440, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "LotCode";
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.dtpIndat);
            this.gbHeader.Controls.Add(this.label4);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.txtLocat);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbHeader.Enabled = false;
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(0, 0);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(436, 129);
            this.gbHeader.TabIndex = 29;
            this.gbHeader.TabStop = false;
            // 
            // dtpIndat
            // 
            this.dtpIndat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpIndat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIndat.Location = new System.Drawing.Point(295, 81);
            this.dtpIndat.Name = "dtpIndat";
            this.dtpIndat.Size = new System.Drawing.Size(126, 22);
            this.dtpIndat.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(216, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 22);
            this.label4.TabIndex = 11;
            this.label4.Text = "In Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(80, 27);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(117, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // txtLocat
            // 
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Location = new System.Drawing.Point(80, 84);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(117, 22);
            this.txtLocat.TabIndex = 4;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            this.txtLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocat_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(224, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(10, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(295, 28);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(126, 24);
            this.cmbLgort.TabIndex = 3;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(482, 86);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(134, 30);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "ConfirmLocation";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbFunction);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 134);
            this.panel3.TabIndex = 30;
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.rdoAdd);
            this.gbFunction.Controls.Add(this.rdoNew);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(8, 11);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(164, 105);
            this.gbFunction.TabIndex = 0;
            this.gbFunction.TabStop = false;
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoAdd.Location = new System.Drawing.Point(19, 67);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(87, 23);
            this.rdoAdd.TabIndex = 1;
            this.rdoAdd.Text = "Add In";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoNew
            // 
            this.rdoNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoNew.Location = new System.Drawing.Point(19, 22);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(87, 23);
            this.rdoNew.TabIndex = 0;
            this.rdoNew.Text = "New Pallet";
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 468);
            this.panel2.TabIndex = 34;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.gbEdit);
            this.panel5.Controls.Add(this.dgvMatnrData);
            this.panel5.Location = new System.Drawing.Point(2, 140);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1018, 123);
            this.panel5.TabIndex = 25;
            // 
            // gbEdit
            // 
            this.gbEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEdit.Controls.Add(this.btnRefresh);
            this.gbEdit.Controls.Add(this.btnExit);
            this.gbEdit.Controls.Add(this.btnSave);
            this.gbEdit.Controls.Add(this.btnAdd);
            this.gbEdit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbEdit.Location = new System.Drawing.Point(710, 6);
            this.gbEdit.Name = "gbEdit";
            this.gbEdit.Size = new System.Drawing.Size(294, 112);
            this.gbEdit.TabIndex = 13;
            this.gbEdit.TabStop = false;
            this.gbEdit.Text = "Edit";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(158, 75);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 29);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(158, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(26, 75);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 29);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(26, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 30);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvMatnrData
            // 
            this.dgvMatnrData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatnrData.Location = new System.Drawing.Point(44, 6);
            this.dgvMatnrData.Name = "dgvMatnrData";
            this.dgvMatnrData.RowHeadersWidth = 51;
            this.dgvMatnrData.RowTemplate.Height = 23;
            this.dgvMatnrData.Size = new System.Drawing.Size(600, 112);
            this.dgvMatnrData.TabIndex = 11;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(12, 267);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 24;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(48, 284);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 18;
            this.dgvData.Size = new System.Drawing.Size(1286, 166);
            this.dgvData.TabIndex = 12;
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
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
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 468);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1370, 21);
            this.stbStatus.TabIndex = 32;
            this.stbStatus.Text = "Status";
            // 
            // StorageIn_TransferIn_DateCode
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1370, 489);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stbStatus);
            this.Name = "StorageIn_TransferIn_DateCode";
            this.Text = "Goods Receipt thru Transfer Posting By Vendor Manufactured Date";
            this.Resize += new System.EventHandler(this.StorageIn_TransferIn_DateCode_Resize);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.gbFunction.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.gbEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatnrData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region ShowStatusData()
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks()
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region cmbWerks_SelectedChanged
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort()
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        # region ShowDataGrid()
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 100;
                dgvData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 100;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 100;
                dgvData.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 100;
                dgvData.Columns.Add(dgvcMenge);


                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                dgvcZeile.Width = 100;
                dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 100;
                dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 100;
                dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                dgvcArbpl.Width = 100;
                dgvData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                dgvcTrntp.Width = 100;
                dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                dgvcInspt.DataPropertyName = "INSPT";
                dgvcInspt.HeaderText = "Insp.Lot No.";
                dgvcInspt.ReadOnly = true;
                dgvcInspt.Width = 100;
                dgvData.Columns.Add(dgvcInspt);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 100;
                dgvData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LockCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 100;
                dgvData.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 100;
                dgvData.Columns.Add(dgvcDacod);


                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "Vendor Manufactured Date";
                dgvcVedat.ReadOnly = true;
                dgvcVedat.Width = 100;
                dgvData.Columns.Add(dgvcVedat);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Width = 100;
                dgvData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "GRLOC";
                dgvcGrloc.HeaderText = "101Location";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 100;
                dgvData.Columns.Add(dgvcGrloc);

                DataGridViewTextBoxColumn dgvcExpiryDate = new DataGridViewTextBoxColumn();
                dgvcExpiryDate.DataPropertyName = "ExpiryDate";
                dgvcExpiryDate.HeaderText = "Exp Date";
                dgvcExpiryDate.ReadOnly = true;
                dgvcExpiryDate.Width = 100;
                dgvData.Columns.Add(dgvcExpiryDate);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKID";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 100;
                dgvData.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcIQCRMAK1 = new DataGridViewTextBoxColumn();
                dgvcIQCRMAK1.DataPropertyName = "IQCRMAK1";
                dgvcIQCRMAK1.HeaderText = "IQC Remark";
                dgvcIQCRMAK1.ReadOnly = true;
                dgvcIQCRMAK1.Width = 100;
                dgvData.Columns.Add(dgvcIQCRMAK1);

                dgvData.DataSource = Data;
                lblData.Text = dtData.Rows.Count.ToString() + " records";

                if (Data.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        # region ShowMatnrDataGrid()
        public void ShowMatnrDataGrid()
        {
            dgvMatnrData.Columns.Clear();
            dgvMatnrData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvMatnrData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvMatnrData.Columns.Add(dgvcLifnr);

                if (objPlantData.CheckCHARGLGORT(strWerks))
                {
                    DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                    dgvcCharg.DataPropertyName = "CHARG";
                    dgvcCharg.HeaderText = "Charg";
                    dgvcCharg.ReadOnly = true;
                    dgvcCharg.Width = 100;
                    dgvMatnrData.Columns.Add(dgvcCharg);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 100;
                dgvMatnrData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 100;
                dgvMatnrData.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "Vendor Manufactured Date";
                dgvcVedat.ReadOnly = true;
                dgvcVedat.Width = 100;
                dgvMatnrData.Columns.Add(dgvcVedat);

                DataGridViewTextBoxColumn dgvcExpiryDate = new DataGridViewTextBoxColumn();
                dgvcExpiryDate.DataPropertyName = "ExpiryDate";
                dgvcExpiryDate.HeaderText = "EXP. Date";
                dgvcExpiryDate.ReadOnly = true;
                dgvcExpiryDate.Width = 100;
                dgvMatnrData.Columns.Add(dgvcExpiryDate);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "InspectionNo";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 100;
                dgvMatnrData.Columns.Add(dgvcTASKID);

                dgvMatnrData.DataSource = dtMatnr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMatnrDataGrid()");
            }
        }
        #endregion


        #region txtLocat_DoubleClick
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region rdoNew_CheckedChanged(New Pallet)
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region rdoAdd_CheckedChanged(Add In)
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                strLocat = txtLocat.Text.Trim();

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                //获取仓别类型和储位类型SttypのLotyp
                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                else
                {
                    stsWarning.Text = "Can't find the storage data!!";
                    return;
                }
                //动态储位要验证储位是否输入
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    #region 动态储位要输入储位
                    if (strType == "ADD")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            stsWarning.Text = "Please input a location first!!";
                            this.txtLocat.Focus();
                            return;
                        }
                    }
                    if (strType == "NEW")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
                        }
                        else
                        {
                            if (objPlantData.CheckStorageData(Werks, Lgort, Locat))
                            {
                                stsWarning.Text = "The location you input is not a empty location!!";
                                this.txtLocat.Focus();
                                return;
                            }
                        }
                    }

                    if (txtLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Please input a location first!!";
                        this.txtLocat.Focus();
                        return;
                    }

                    if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                    {
                        stsWarning.Text = "The location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    //т畐
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    //加料入库判断库存表中stock是否一致
                    if (this.rdoAdd.Checked)
                    {
                        dtTemp = objStorageData.QueryLocatInsmk(strLocat);
                        if (dtTemp.Rows.Count > 1)
                        {
                            stsWarning.Text = "The location has different stock and you can't add any new Part No!!";
                            return;
                        }
                        strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                    }
                }
                    #endregion
                else
                {
                    #region 固定储位不能输入储位
                    if (this.txtLocat.Text.Trim() != "")
                    {
                        stsWarning.Text = "You can't input location because " + strLgort + " is a fixed-Location storage!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    #endregion
                }
                //判断是否管控仓
                if(objPlantData.CheckDACODLGORT(strWerks, strLgort))
                {
                    chkType101.Enabled = true;
                    chkType101.Visible = true;
                }
                gbHeader.Enabled = false;
                btnAdd.Enabled = true;
                txtLockCode.Enabled = true;
                txtLockCode.Focus();
                dtMatnr.Rows.Clear();
                //btnAdd_Click(null, null);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnAdd_Click
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    //新增入库，只能入库同一中库别，按照第一个数据为准
                    if (rdoNew.Checked)
                    {
                        if (dtData.Rows.Count == 0 || dtData == null)
                        {
                            strInsmk = "";
                        }
                        else
                        {
                            strInsmk = dtData.Rows[0]["INSMK"].ToString();
                        }
                    }
                }
                else
                {
                    strInsmk = "";
                }
                if (CheckIsOpen("StorageIn_OnLineIn_DateCode_Add"))
                {
                    this.MdiParent.MdiChildren[intFormIndex].Close();
                }

                StorageIn_OnLineIn_DateCode_Add objStorageIn_OnLineIn_DateCode_Add = new StorageIn_OnLineIn_DateCode_Add(ref UserData, Progid, Werks, Lgort, Locat, Sttyp, Lotyp, dtpIndat.Value.ToString("yyyyMMdd"), "", "", dtData, "COMBINE", Insmk, "", Duplicate);
                objStorageIn_OnLineIn_DateCode_Add.MdiParent = this.ParentForm;
                objStorageIn_OnLineIn_DateCode_Add.Show();
                dtData = objStorageIn_OnLineIn_DateCode_Add.SapData;
                ShowDataGrid();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 没用
        //private void dtgData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    try
        //    {
        //        int intRowNo;
        //        string strTempLocat = "";
        //        string strTempCharg = "";
        //        string strDACOD = "";
        //        string strINSPT = "";
        //        string strINDAT = "";

        //        stsWarning.Text = "";
        //        DataGrid dgClick = (DataGrid)sender;
        //        DataGrid.HitTestInfo hitRow;
        //        hitRow = dgClick.HitTest(e.X, e.Y);
        //        if(hitRow.Type == DataGrid.HitTestType.RowHeader)
        //        {
        //            //狦琌匡拒New, 玥材ΩAddぃ畐, 璶匡拒, ぇAdd碞ㄌ酚材畐
        //            if(rdoNew.Checked)
        //            {
        //                if(dtData.Rows.Count == 0 || dtData == null)
        //                {
        //                    strInsmk = "";
        //                }
        //                else
        //                {
        //                    strInsmk = dtData.Rows[0]["INSMK"].ToString();
        //                }
        //            }

        //            //20090406 Marc Add Inspection Lot to Key
        //            #region 20090406 Marc Add Inspection Lot to Key
        //            intRowNo = hitRow.Row;
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 6);
        //            strMblnr = dgClick[dgClick.CurrentCell].ToString(); 
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 1);
        //            strMatnr = dgClick[dgClick.CurrentCell].ToString();
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
        //            strTempLocat = dgClick[dgClick.CurrentCell].ToString(); 
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 3);
        //            strTempCharg = dgClick[dgClick.CurrentCell].ToString(); 
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 15);
        //            strDACOD = dgClick[dgClick.CurrentCell].ToString(); 
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 13);
        //            strINSPT = dgClick[dgClick.CurrentCell].ToString(); 
        //            dgClick.CurrentCell = new DataGridCell(intRowNo, 16);
        //            strINDAT = dgClick[dgClick.CurrentCell].ToString(); 

        //            StorageIn_OnLineIn_DateCode_Add objStorageIn_OnLineIn_DateCode_Add = new StorageIn_OnLineIn_DateCode_Add(ref UserData, Progid, Werks, Lgort, strTempLocat, Sttyp, Lotyp, strINDAT, strMblnr, strMatnr, dtData, "ONLINE", Insmk, strTempCharg, Duplicate,strDACOD,strINSPT);
        //            objStorageIn_OnLineIn_DateCode_Add.ShowDialog();
        //            dtData = objStorageIn_OnLineIn_DateCode_Add.SapData;
        //            ShowDataGrid();
        //            #endregion



        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        stsWarning.Text = ex.Message;
        //        return;
        //    }
        //}
        #endregion

        # region Click RowHeader
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string strTempLocat = "";
            string strTempCharg = "";
            string strDACOD = "";
            string strINSPT = "";
            string strINDAT = "";

            try
            {
                if (rdoNew.Checked)
                {
                    if (dtData.Rows.Count == 0 || dtData == null)
                    {
                        strInsmk = "";
                    }
                    else
                    {
                        strInsmk = dtData.Rows[0]["INSMK"].ToString();
                    }
                }
                strMblnr = dgvData.CurrentRow.Cells[6].Value.ToString();//Mblnr
                strMatnr = dgvData.CurrentRow.Cells[1].Value.ToString();//Part NO.
                strTempLocat = dgvData.CurrentRow.Cells[0].Value.ToString();
                strTempCharg = dgvData.CurrentRow.Cells[3].Value.ToString();
                strDACOD = dgvData.CurrentRow.Cells[15].Value.ToString();//Part NO.
                strINSPT = dgvData.CurrentRow.Cells[13].Value.ToString();
                strINDAT = dgvData.CurrentRow.Cells[16].Value.ToString();


                StorageIn_OnLineIn_DateCode_Add objStorageIn_OnLineIn_DateCode_Add = new StorageIn_OnLineIn_DateCode_Add(ref UserData, Progid, Werks, Lgort, strTempLocat, Sttyp, Lotyp, strINDAT, strMblnr, strMatnr, dtData, "ONLINE", Insmk, strTempCharg, Duplicate, strDACOD, strINSPT);
                objStorageIn_OnLineIn_DateCode_Add.ShowDialog();
                dtData = objStorageIn_OnLineIn_DateCode_Add.SapData;
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                SetbtnSaveProcess();
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //20090406 Marc Add Inspection Lot to key
                    #region 20090406 Marc Add Inspection Lot to key
                    if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["DACOD"].ToString() + dtData.Rows[i]["INSPT"].ToString()) == -1)
                    {
                        strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["DACOD"].ToString() + dtData.Rows[i]["INSPT"].ToString() + ";";
                    }
                    else
                    {
                        stsWarning.Text = "The Document No , Part No , DateCode and Inspection Lot you input is duplicate!!";
                        SetbtnSaveException();
                        return;
                    }
                    #endregion

                    //浪琩璶畐虫腹ぇ玡琌Τㄏノ硈狾畐よΑ畐
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                    dtTemp = objStorageData.QueryNotCombineInData(Locat, dtData.Rows[i]["MBLNR"].ToString(), "0");
                    if (dtTemp.Rows.Count > 0)
                    {
                        stsWarning.Text = "The Document No was stored in with mixed material last time!!";
                        SetbtnSaveException();
                        return;
                    }
                    //相同料号是否有混料
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        SetbtnSaveException();
                        return;
                    }
                    if (this.Duplicate == false)
                    {
                        //相同料号是否有相同版本
                        if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
                            SetbtnSaveException();
                            return;
                        }
                    }

                    //确认储位相同料号是否有相同DateCode
                    if (objStorageData.CheckExistedSameMaterialDC(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["DACOD"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different datecode!";
                        SetbtnSaveException();
                        return;
                    }
                    if (objStorageData.CheckStorageInType(strWerks, strLgort, "Diff Vendor Diff Locat"))
                    {
                        //确认储位相同料号是否有相同DateCode
                        if (objStorageData.CheckExistedSameMaterialDC(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["LIFNR"].ToString()))
                        {
                            stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + "The item number already exists with different manufacturers in the storage location. Please confirm!";
                            SetbtnSaveException();
                            return;
                        }
                    }

                }



                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.AddTransferInData_DateCode(Locat, dtData))
                {
                    SetCompleteNotice();
                    stsWarning.Text = "Add OK!!";
                    SetbtnSaveProcess();
                    this.btnAdd.Enabled = false;
                    this.btnSave.Enabled = false;
                    return;
                }
                else
                {
                    stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                    SetbtnSaveException();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetbtnSaveException();
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            //			if(cmbWerks.Items.Count > 0)
            //			{
            //				this.cmbWerks.SelectedIndex = 0;
            //			}
            //			if(cmbLgort.Items.Count > 0)
            //			{
            //				this.cmbLgort.SelectedIndex = 0;
            //			}
            this.txtLocat.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Rows.Clear();
            this.dtMatnr.Rows.Clear();
            this.dgvMatnrData.DataSource = null;
            this.txtLockCode.Text = "";
            this.dgvData.DataSource = null;
            this.btnAdd.Enabled = false;
            this.txtLockCode.Enabled = false;
            this.btnSave.Enabled = false;
            chkType101.Visible = false;
            strWerks = "";
            strLgort = "";
            strInsmk = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Resize
        private void StorageIn_TransferIn_DateCode_Resize(object sender, System.EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }
        #endregion

        #region txtLocat_KeyPress
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion

        # region CheckIsOpen
        private bool CheckIsOpen(string strForm)
        {
            bool bolOpened = false;
            string strTest = "";
            try
            {
                for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
                {
                    strTest = MdiParent.MdiChildren[i].ToString();
                    if (MdiParent.MdiChildren[i].ToString().IndexOf(strForm) != -1)
                    {
                        bolOpened = true;
                        intFormIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckIsOpen()");
            }
            return bolOpened;
        }
        #endregion

        #region SetbtnSaveProcess
        private void SetbtnSaveProcess()
        {
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;
            this.txtLockCode.Enabled = false;

        }
        #endregion

        #region SetbtnSaveException
        private void SetbtnSaveException()
        {
            this.btnAdd.Enabled = true;
            this.btnSave.Enabled = true;
            this.txtLockCode.Enabled = true;

        }
        #endregion


        private bool CheckStorage(string strMatnr, string strLifnr, int intMenge)
        {
            bool bol = true;
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
             DataTable dtTemp=new DataTable();
            bool bolNotVenCode = true;
            StorageData objStorageData = new StorageData(UserData);
            if (objStorageData.QueryNotVendateLGORT(strWerks, strLgort))//判断是不是特殊单据没有VenCode的仓别
            {
                bolNotVenCode = false;
            }
            if (chkType101.Checked)
            {
                dtTemp = objStorageIn.QuerySapInDataDateCode(strWerks, strLgort,strMatnr,strLifnr,bolNotVenCode);
            }
            else
            {
                dtTemp = objStorageIn.GetStorageByMatnrLifnr(strWerks, strLgort, strMatnr, strLifnr, bolNotVenCode);

            }
            if (dtTemp.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtTemp.Rows[0]["MENGE"].ToString()) < intMenge)
                {
                    bol = false;
                    stsWarning.Text = "Insufficient quantity, please confirm!";
                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                    sp.Play();
                    txtLockCode.Focus();
                }
                else
                {
                    SetOKNotice();
                }

            }
            else
            {
                bol = false;
                stsWarning.Text = "The item number information was not found in the inventory. Please confirm!";
                SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                sp.Play();
                txtLockCode.Focus();
            }
            return bol;

        }

        private void ShowStorageInData(string strMatnr, string strLifnr, int intMenge, string strVedat, string strDaCod, string strLocod, string strExpdate, string strTASKID, string strSerno)
        {
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
            string Indat = dtpIndat.Value.ToString("yyyyMMdd");
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(UserData, Werks, Lgort);
            DataTable dtTemp = new DataTable();

            bool bolNotVenCode = true;
            StorageData objStorageData = new StorageData(UserData);
            if (objStorageData.QueryNotVendateLGORT(strWerks, strLgort))//判断是不是特殊单据没有VenCode的仓别
            {
                bolNotVenCode = false;
            }
            if (chkType101.Checked)
            {

                dtTemp = objStorageIn.QuerySapLineInDataDateCode(strMatnr, strLifnr, Locat, Indat, strVedat, strDaCod, strLocod, bolNotVenCode);
            }
            else
            {
                dtTemp = objStorageIn.QuerySapLineInData_DateCode(strMatnr, strLifnr, Locat, Indat, strVedat, strDaCod, strLocod, bolNotVenCode, strExpdate, strTASKID, strSerno);
            }
            if (dtData == null || dtData.Rows.Count <= 0)
            {
                dtData = dtTemp.Clone();
            }
            int restQty = intMenge;

            foreach (DataRow dr in dtTemp.Rows)
            {
                if (restQty != 0)
                {
                    int a = Convert.ToInt32(dr["MENGE"].ToString());
                    if (a <= restQty)
                    {
                        restQty = restQty - a;
                        dr["ALQTY"] = (Convert.ToInt32(dr["ALQTY"].ToString()) + a).ToString();
                        dtData.Rows.Add(dr.ItemArray);
                    }
                    else
                    {
                        dr["ALQTY"] = restQty.ToString();
                        restQty = 0;
                        dtTemp.AcceptChanges();
                        dtData.Rows.Add(dr.ItemArray);
                    }
                }
                else
                {
                    break;
                }
            }
            ShowDataGrid();
            SoundPlayer sp = new SoundPlayer(@"Sound\Success.wav");
            sp.Play();
        }

        private void txtLockCode_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = "";
            if (e.KeyCode == Keys.Enter)
            {

                #region 处理刷入的数据
                string strLotCode = "";
                strLotCode = txtLockCode.Text.Trim().ToUpper();
                string[] str = strLotCode.Split(';');
                DataRow drMatnr = dtMatnr.NewRow();

                //正常交料 9项：AL040170001; 202250; TIR-TIC; 5052548MY2; 3000; TPS40170QRGYRQ1; BTIR-TIC230223A0254MY; Made in Malaysia; TPS40170QRGYRQ1
                //IQC ReLabel 10项：DAV31UECCC0; 0723; AKV-AMV; 0723; 20; PCB V31U ECU/B (12L,200*184,REVC); R7202308280000711004; MADE IN TAIWAN; THIW12C986B; 20240213
                //IQC ReLabel 10项：CS16982FB07; 20201008; QCI-KOA; 20201008; 9564; ; R7202308240014311001; ; ; 20251008
                //物料打印 5项：DA0V3HVB4A0; 2423-0D5Q; GRU-ZDT; SP1230608055; 45
                //IQC检验过渡阶段产生 7项：cs41472fb05;201712;wpk-dal;0007031156;3593;20240320;r72023081700017
                if (str.Length >= 5)
                {
                    DataRow[] drM = dtMatnr.Select("MATNR='" + str[0].ToString().Trim() + "' ");
                    if (drM.Length > 0)
                    {
                        #region 刷入多笔的时候同一料号不同厂商不能放在同一储位，相同的料号，但是DateCode不一致，不能入库
                        DataRow drExist = drM[0];
                        if (str[2].ToString().Trim() != drExist["LIFNR"].ToString())
                        {
                            stsWarning.Text = "The same item number with different Vendor Codes cannot be placed in the same storage location. Please confirm!";
                            SetErrNotice();
                            return;
                        }
                        if (str[1].ToString().Trim() != drExist["DACOD"].ToString())
                        {
                            stsWarning.Text = "The same item number with different DateCode Codes cannot be placed in the same storage location. Please confirm!";
                            SetErrNotice();
                            return;
                        }
                        #endregion

                        #region Lot Code不同Lot Code不允许入库 - Lot code仓别
                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                        {
                            if (str[3].ToString().Trim() != drExist["LOCOD"].ToString())
                            {
                                stsWarning.Text = "The same item number with different Lot Code Codes cannot be placed in the same storage location. Please confirm!";
                                SetErrNotice();
                                return;
                            }
                        }
                        #endregion
                        drMatnr["MATNR"] = str[0].ToString().Trim();
                        drMatnr["DACOD"] = str[1].ToString().Trim();
                        drMatnr["LIFNR"] = str[2].ToString().Trim();
                        if (str[0].ToString().Trim().Substring(0,2) == "SA" || str[0].ToString().Trim().Substring(0, 2) == "DA")
                        {
                            drMatnr["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? str[2].ToString().Trim().Substring((str[2].ToString().Trim().Length) - 3) : "";
                        }
                        else
                        {
                            drMatnr["CHARG"] = "";
                        }
                        drMatnr["LOCOD"] = str[3].ToString().Trim();
                        drMatnr["MENGE"] = Convert.ToInt32(drExist["MENGE"]) + Convert.ToInt32(str[4].ToString().Trim());
                        //大于9最后一项为保存期，则为IQC检验过的材料
                        if (str.Length > 9)
                        {
                            #region Brian Add 20230905
                            string strExpiryDate = str[str.Length - 1].ToString().Trim();
                            if (!ClaCommon.CheckDateValid(strExpiryDate))
                            {
                                MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                return;
                            } 
                            #endregion
                            drMatnr["ExpiryDate"] = strExpiryDate;
                            drMatnr["TASKID"] = str[6].ToString().Trim().Substring(0, 15);
                            drMatnr["SERNO"] = str[6].ToString().Trim();
                        }
                        //等于7最后一项为检验批号，倒数第二为保存期
                        if (str.Length == 7)
                        {
                            #region Brian Add 20230905
                            string strExpiryDate = str[5].ToString().Trim();
                            if (!ClaCommon.CheckDateValid(strExpiryDate))
                            {
                                MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                return;
                            } 
                            #endregion
                            drMatnr["ExpiryDate"] = strExpiryDate;
                            drMatnr["TASKID"] = str[6].ToString().Trim();
                        }
                    }
                    else
                    {
                        drMatnr["MATNR"] = str[0].ToString().Trim();
                        drMatnr["DACOD"] = str[1].ToString().Trim();
                        drMatnr["LIFNR"] = str[2].ToString().Trim();
                        if (str[0].ToString().Trim().Substring(0,2) == "SA" || str[0].ToString().Trim().Substring(0,2) == "DA")
                        {
                            drMatnr["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? str[2].ToString().Trim().Substring((str[2].ToString().Trim().Length) - 3) : "";
                        }
                        else
                        {
                            drMatnr["CHARG"] = "";
                        }
                        drMatnr["LOCOD"] = str[3].ToString().Trim();
                        drMatnr["MENGE"] = Convert.ToInt32(str[4].ToString().Trim());
                        //大于9最后一项为保存期，则为IQC检验过的材料
                        if (str.Length > 9)
                        {
                            #region Brian Add 20230905
                            string strExpiryDate = str[str.Length - 1].ToString().Trim();
                            if (!ClaCommon.CheckDateValid(strExpiryDate))
                            {
                                MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                return;
                            }                            
                            #endregion                   
                            drMatnr["ExpiryDate"] = strExpiryDate;
                            drMatnr["TASKID"] = str[6].ToString().Trim().Substring(0, 15);
                            drMatnr["SERNO"] = str[6].ToString().Trim();
                        }
                        //等于7最后一项为检验批号，倒数第二为保存期
                        if (str.Length == 7)
                        {
                            #region Brian Add 20230905
                            string strExpiryDate = str[5].ToString().Trim();
                            if (!ClaCommon.CheckDateValid(strExpiryDate))
                            {
                                MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                return;
                            } 
                            #endregion
                            drMatnr["ExpiryDate"] = strExpiryDate;
                            drMatnr["TASKID"] = str[6].ToString().Trim();
                        }
                    }
                }
                //DID退料 1项：DFHD28MR005-CC37MRR0001
                else
                {
                    if (str.Length == 1)
                    {
                        //1项则为DIDNO，查询WHRID表，抓取数据
                        DataTable dtData = objStorageData.QueryIQC_WHRID(strLotCode);
                        //MATNR,DACOD,LIFNR,LOCOD,MENGE
                        if (dtData.Rows.Count > 0)
                        {
                            DataRow[] drM = dtMatnr.Select("MATNR='" + dtData.Rows[0]["MATNR"].ToString() + "' ");
                            if (drM.Length > 0)
                            {
                                #region 刷入多笔的时候同一料号不同厂商不能放在同一储位，相同的料号，但是DateCode不一致，不能入库
                                DataRow drExist = drM[0];
                                if (dtData.Rows[0]["LIFNR"].ToString() != drExist["LIFNR"].ToString())
                                {
                                    stsWarning.Text = "The same item number with different Vendor Codes cannot be placed in the same storage location. Please confirm!";
                                    SetErrNotice();
                                    return;
                                }
                                if (dtData.Rows[0]["DACOD"].ToString() != drExist["DACOD"].ToString())
                                {
                                    stsWarning.Text = "The same item number with different DateCode Codes cannot be placed in the same storage location. Please confirm!";
                                    SetErrNotice();
                                    return;
                                }
                                #endregion
                                #region Lot Code不同Lot Code不允许入库
                                if (objPlantData.CheckLOCODLGORT(Werks, Lgort))
                                {
                                    if (dtData.Rows[0]["LOCOD"].ToString() != drExist["LOCOD"].ToString())
                                    {
                                        stsWarning.Text = "The same item number with different Lot Code Codes cannot be placed in the same storage location. Please confirm!";
                                        SetErrNotice();
                                        return;
                                    }
                                }
                                #endregion
                                drMatnr["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                                drMatnr["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                                drMatnr["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                                if (dtData.Rows[0]["MATNR"].ToString().Substring(0,2) == "SA" || dtData.Rows[0]["MATNR"].ToString().Substring(0,2) == "DA")
                                {
                                    drMatnr["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtData.Rows[0]["LIFNR"].ToString().Substring((dtData.Rows[0]["LIFNR"].ToString().Length) - 3) : "";
                                }
                                else
                                {
                                    drMatnr["CHARG"] = "";
                                }
                                drMatnr["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                                drMatnr["MENGE"] = Convert.ToInt32(drExist["MENGE"]) + Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                                drMatnr["ExpiryDate"] = dtData.Rows[0]["EXPDAT"].ToString();
                                drMatnr["TASKID"] = dtData.Rows[0]["TASKID"].ToString();
                                drMatnr["SERNO"] = dtData.Rows[0]["SERNO"].ToString();
                            }
                            else
                            {
                                drMatnr["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                                drMatnr["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                                drMatnr["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                                if (dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "SA" || dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "DA")
                                {
                                    drMatnr["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtData.Rows[0]["LIFNR"].ToString().Substring((dtData.Rows[0]["LIFNR"].ToString().Length) - 3) : "";
                                }
                                else
                                {
                                    drMatnr["CHARG"] = "";
                                }
                                drMatnr["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                                drMatnr["MENGE"] = Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                                drMatnr["ExpiryDate"] = dtData.Rows[0]["EXPDAT"].ToString();
                                drMatnr["TASKID"] = dtData.Rows[0]["TASKID"].ToString();
                                drMatnr["SERNO"] = dtData.Rows[0]["SERNO"].ToString();
                            }
                        }
                        else
                        {
                            stsWarning.Text = "No information was found for the DIDNO document. Please be informed!";
                            return;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "Incorrect number of items for the physical QR code. Please confirm!";
                        return;
                    }
                }
                #endregion

                #region 处理DateCode转换问题
                try
                {
                    StorageData objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
                    string DC_After = objStorageData.WHDCR_Query(drMatnr["LIFNR"].ToString(), drMatnr["DACOD"].ToString());
                    if (DC_After != "")
                    {
                        DateTime dtVedat = Convert.ToDateTime(DC_After);
                        DC_After = dtVedat.ToString("yyyyMMdd");
                        drMatnr["VEDAT"] = DC_After;
                    }
                    else
                    {
                        #region 确认是否要转化DateCode Rule
                        string strTemp = objStorageData.getDCTrans(drMatnr["LIFNR"].ToString(), drMatnr["DACOD"].ToString()).ToString();
                        if (!string.IsNullOrEmpty(strTemp))
                        {
                            //strDC = DateTime.Now.ToString("yyyyMMdd");
                            DataTable dtNewDateCode = new DataTable();
                            dtNewDateCode.Columns.Add("LIFNR");
                            dtNewDateCode.Columns.Add("DC_Before");
                            dtNewDateCode.Columns.Add("DC_After");

                            DataRow dr = dtNewDateCode.NewRow();
                            dr["LIFNR"] = drMatnr["LIFNR"].ToString();
                            dr["DC_Before"] = drMatnr["DACOD"].ToString();
                            dr["DC_After"] = strTemp;
                            dtNewDateCode.Rows.Add(dr.ItemArray);
                            objStorageData.WHDCR_DML(dtNewDateCode, "NEW", "System");
                            drMatnr["VEDAT"] = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                        }
                        else
                        {
                            MessageBox.Show("无D/C转换信息找D/C管理人员处理");
                            return;
                        }
                        #endregion
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show("DateCode转化错误！"+e1.ToString());
                    SetErrNotice();
                    return;
                }
                #endregion

                #region 防呆
                try
                {
                    if (dtData.Rows.Count > 0)
                    {
                        # region 验证是否第一次刷入
                        DataRow[] drExist = dtData.Select(" MATNR='" + drMatnr["MATNR"].ToString().Trim() + "'");
                        if (drExist.Length > 0)
                        {
                            # region 相同料号，相同DateCode,相同厂商，数量相加
                            int  a =Convert.ToInt32( drMatnr["MENGE"].ToString().Trim());
                            if (CheckStorage(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), a))
                            {
                                DataRow[] drTemp= dtMatnr.Select(" MATNR='" + drMatnr["MATNR"].ToString().Trim() + "' ");
                                dtMatnr.Rows.Remove(drTemp[0]);
                                drMatnr["MENGE"] = a;
                                dtMatnr.Rows.Add(drMatnr);
                                dtMatnr.AcceptChanges();
                                ShowMatnrDataGrid();
                                DataRow[] drExists = dtData.Select(" MATNR='" + drMatnr["MATNR"].ToString().Trim() + "' AND LIFNR='" + drMatnr["LIFNR"].ToString().Trim() + "'");

                                //刷入同一板数，把dtData清空，重新计算
                                if (drExists.Length > 0)
                                {
                                    foreach (DataRow dr1 in drExists)
                                    {
                                        dtData.Rows.Remove(dr1);
                                    }
                                }
                                ShowStorageInData(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), Convert.ToInt32(a), drMatnr["VEDAT"].ToString(), drMatnr["DACOD"].ToString(), drMatnr["LOCOD"].ToString(), drMatnr["ExpiryDate"].ToString(), drMatnr["TASKID"].ToString(), drMatnr["SERNO"].ToString());
                            }
                            else
                            {
                                return;
                            }
                            #endregion
                        }
                        else
                        {
                            #region  料号第一次刷入
                            if (CheckStorage(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), Convert.ToInt32(drMatnr["MENGE"].ToString())))
                            {
                                dtMatnr.Rows.Add(drMatnr.ItemArray);
                                ShowMatnrDataGrid();
                                ShowStorageInData(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), Convert.ToInt32(drMatnr["MENGE"].ToString()), drMatnr["VEDAT"].ToString(), drMatnr["DACOD"].ToString(), drMatnr["LOCOD"].ToString(), drMatnr["ExpiryDate"].ToString(), drMatnr["TASKID"].ToString(), drMatnr["SERNO"].ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        # region 第一次刷入
                        if (CheckStorage(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), Convert.ToInt32(drMatnr["MENGE"].ToString())))
                        {
                            dtMatnr.Rows.Add(drMatnr.ItemArray);
                            ShowMatnrDataGrid();
                            ShowStorageInData(drMatnr["MATNR"].ToString(), drMatnr["LIFNR"].ToString(), Convert.ToInt32(drMatnr["MENGE"].ToString()), drMatnr["VEDAT"].ToString(), drMatnr["DACOD"].ToString(), drMatnr["LOCOD"].ToString(), drMatnr["ExpiryDate"].ToString(), drMatnr["TASKID"].ToString(), drMatnr["SERNO"].ToString());
                        }
                        #endregion
                    }
                    txtLockCode.Text = "";
                }
                catch (Exception e2)
                {
                    MessageBox.Show("库存校验错误"+e2.ToString());
                    SetErrNotice();
                    return;
                }
                #endregion
                txtLockCode.Focus();
            }
        }


        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
            txtLockCode.Focus();
            txtLockCode.Text = "";
        }

        public void SetOKNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\BIU.wav");
            sp.Play();
            txtLockCode.Focus();
        }

        public void SetCompleteNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\Complete.wav");
            sp.Play();
        }
    }
}
