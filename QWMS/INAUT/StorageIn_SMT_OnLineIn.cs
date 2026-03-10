using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using System.IO;
using System.Web.Services;
using QWMS.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace QWMS
{
	/// <summary>
	/// StorageIn_SMT_OnLineIn 的摘要描述。
	/// </summary>
	public class StorageIn_SMT_OnLineIn : System.Windows.Forms.Form
    {
        #region 初始化
        UserInfo UserData = new UserInfo();
		protected internal System.Windows.Forms.StatusBar stbStatus;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
		private System.Windows.Forms.StatusBarPanel stsUsrnm;
		private System.Windows.Forms.StatusBarPanel stsWarning;
		private System.Windows.Forms.StatusBarPanel stsDate;
		private System.Windows.Forms.GroupBox gbHeader;
		private System.Windows.Forms.ComboBox cmbWerks;
		private System.Windows.Forms.TextBox txtLocat;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbLgort;
		
		private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strProgid = "";
		private string strLocat = "";
		private string strMatnr = "";
		private string strType = "";
		private string strMrgid = "";
		private string strInsmk = "";
		private string strCharg = "";
		private string strSttyp = "";
		private string strLotyp = "";
		//20080109 add REFID by marc
		private string strRefid = "";
        private bool AllowToClose = true;//設定能否關閉Form視窗
		private bool bolDuplicate = false;
		private int intFormIndex = 0;
		private DataTable dtData = new DataTable();

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.GroupBox gbFunction;
		private System.Windows.Forms.RadioButton rdoAdd;
		private System.Windows.Forms.RadioButton rdoNew;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.CheckBox chkMixed;
		private System.Windows.Forms.Timer tmProgressTimer;
		private System.Windows.Forms.ProgressBar pbrProgressBar;
		public System.Windows.Forms.Button btnQueryRefid;
        private DataGridView dgvData;
        private Label lblData;
		private System.ComponentModel.IContainer components;

        #endregion 
        #region 變數
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

		public string Matnr
		{
			get
			{
				return strMatnr;
			}
			set
			{
				strMatnr = value;
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

		public string Charg
		{
			get
			{
				return strCharg;
			}
			set
			{
				strCharg = value;
			}
		}

		public string Mrgid
		{
			get
			{
				return strMrgid;
			}
			set
			{
				strMrgid = value;
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
		
		//20080109 add REFID By marc
		public string Refid
		{
			get
			{
				return strRefid;
			}
			set
			{
				strRefid = value;
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
        #region 建構式
        public StorageIn_SMT_OnLineIn(ref UserInfo varUserData, string strProgid)
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
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

				//檢查權限
				if(!objStorageIn.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					bolDuplicate = objStorageIn.CheckDuplicatLocat();
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
					{
						this.cmbLgort.SelectedIndex = 0;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
        #endregion
        #region 清除任何使用中的資源        
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }
        #endregion
        #region Windows Form Designer generated code
        /// <summary>
		/// 此為設計工具支援所必需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.chkMixed = new System.Windows.Forms.CheckBox();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.rdoNew = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnQueryRefid = new System.Windows.Forms.Button();
            this.pbrProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tmProgressTimer = new System.Windows.Forms.Timer(this.components);
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.txtLocat);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.btnConfirm);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Controls.Add(this.chkMixed);
            this.gbHeader.Enabled = false;
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(10, 7);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(818, 105);
            this.gbHeader.TabIndex = 32;
            this.gbHeader.TabStop = false;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(106, 15);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(494, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // txtLocat
            // 
            this.txtLocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Location = new System.Drawing.Point(106, 75);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(494, 22);
            this.txtLocat.TabIndex = 4;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            this.txtLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocat_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(654, 60);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(19, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(106, 45);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(494, 24);
            this.cmbLgort.TabIndex = 3;
            // 
            // chkMixed
            // 
            this.chkMixed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMixed.Location = new System.Drawing.Point(628, 22);
            this.chkMixed.Name = "chkMixed";
            this.chkMixed.Size = new System.Drawing.Size(144, 23);
            this.chkMixed.TabIndex = 33;
            this.chkMixed.Text = "Mixed Material";
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 453);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1110, 20);
            this.stbStatus.TabIndex = 28;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1110, 134);
            this.panel1.TabIndex = 33;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(250, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(860, 134);
            this.panel4.TabIndex = 34;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbFunction);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 134);
            this.panel3.TabIndex = 33;
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.rdoAdd);
            this.gbFunction.Controls.Add(this.rdoNew);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(29, 7);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(201, 105);
            this.gbFunction.TabIndex = 31;
            this.gbFunction.TabStop = false;
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoAdd.Location = new System.Drawing.Point(19, 67);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(125, 23);
            this.rdoAdd.TabIndex = 1;
            this.rdoAdd.Text = "Add In";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoNew
            // 
            this.rdoNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoNew.Location = new System.Drawing.Point(19, 30);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(125, 22);
            this.rdoNew.TabIndex = 0;
            this.rdoNew.Text = "New Pallet";
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnQueryRefid);
            this.panel2.Controls.Add(this.pbrProgressBar);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 134);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1110, 319);
            this.panel2.TabIndex = 34;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(29, 3);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 25;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(29, 23);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(1037, 245);
            this.dgvData.TabIndex = 18;
            this.dgvData.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_RowHeaderMouseClick);
            // 
            // btnQueryRefid
            // 
            this.btnQueryRefid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQueryRefid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQueryRefid.Location = new System.Drawing.Point(422, 275);
            this.btnQueryRefid.Name = "btnQueryRefid";
            this.btnQueryRefid.Size = new System.Drawing.Size(111, 38);
            this.btnQueryRefid.TabIndex = 17;
            this.btnQueryRefid.Text = "Query RefID Data";
            this.btnQueryRefid.Click += new System.EventHandler(this.btnQueryRefid_Click);
            // 
            // pbrProgressBar
            // 
            this.pbrProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbrProgressBar.Location = new System.Drawing.Point(578, 286);
            this.pbrProgressBar.Name = "pbrProgressBar";
            this.pbrProgressBar.Size = new System.Drawing.Size(231, 21);
            this.pbrProgressBar.TabIndex = 16;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(317, 275);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 32);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(29, 275);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 32);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(221, 275);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(125, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 32);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tmProgressTimer
            // 
            this.tmProgressTimer.Tick += new System.EventHandler(this.tmProgressTimer_Tick);
            // 
            // StorageIn_SMT_OnLineIn
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1110, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "StorageIn_SMT_OnLineIn";
            this.Text = "Online Goods Receipt (SMT)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StorageIn_SMT_OnLineIn_FormClosing);
            this.Resize += new System.EventHandler(this.StorageIn_SMT_OnLineIn_Resize);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbFunction.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
        #region 狀態列
        private void ShowStatusData()
		{
			this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
			this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
			this.stsUsrnm.Text = Usrnm;
        }
        #endregion
        #region 廠區下拉選單
        private void ShowDdlWerks()
		{
			DataTable dtTemp = new DataTable();
			try
			{
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				cmbWerks.Items.Clear();
				dtTemp = objAuthority.CheckPlantAuthority();
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlWerks()");
			}
        }
        #endregion
        #region 選取廠區
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			stsWarning.Text = "";
			ShowDdlLgort();
        }
        #endregion
        #region 倉別下拉選單
        private void ShowDdlLgort()
		{
			try
			{
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				stsWarning.Text = "";
				DataTable dtTemp = new DataTable();
				if(cmbWerks.SelectedIndex != -1)
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
				if(cmbLgort.SelectedIndex != -1)
				{
					strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
				}
				else
				{
					cmbLgort.Items.Clear();
				}
				
				if(dtTemp.Rows.Count == 0)
				{
					cmbLgort.Items.Clear();
					strLgort = "";
				}
				else
				{
					cmbLgort.Items.Clear();
					for(int i=0;i<dtTemp.Rows.Count;i++)
					{
						cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
						if(dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
						{
							cmbLgort.SelectedIndex = i;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
        }
        #endregion
        #region DataGrid 入庫資料
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


                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Referance ID.";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 100;
                dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "DID No.";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcOtlgt = new DataGridViewTextBoxColumn();
                dgvcOtlgt.DataPropertyName = "OTLGT";
                dgvcOtlgt.HeaderText = "Storage From.";
                dgvcOtlgt.ReadOnly = true;
                dgvcOtlgt.Width = 100;
                dgvData.Columns.Add(dgvcOtlgt);

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


                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Width = 100;
                dgvData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 100;
                dgvData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcInpst = new DataGridViewTextBoxColumn();
                dgvcInpst.DataPropertyName = "INSPT";
                dgvcInpst.HeaderText = "Inspection Lot No.";
                dgvcInpst.ReadOnly = true;
                dgvcInpst.Width = 100;
                dgvData.Columns.Add(dgvcInpst);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "Locod";
                dgvcLocod.HeaderText = "Lock Code";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 100;
                dgvData.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcdDacod = new DataGridViewTextBoxColumn();
                dgvcdDacod.DataPropertyName = "Dacod";
                dgvcdDacod.HeaderText = "Date Code";
                dgvcdDacod.ReadOnly = true;
                dgvcdDacod.Width = 100;
                dgvData.Columns.Add(dgvcdDacod);

                dgvData.DataSource = Data;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
        }
        #endregion
        #region DoubleClick儲位
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbWerks.SelectedIndex != -1)
					Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
				else
					Werks = "";

				if(cmbLgort.SelectedIndex != -1)
					Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
				else
					Lgort = "";

				if(Werks == "" || Lgort == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				else
				{
                    //StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    //objStorageIn_LocationSelect.ShowDialog();
                    //txtLocat.Text = objStorageIn_LocationSelect.Locat;

                    //Add By Michael 20151105 for SMT祥龙退料优化
                    Manage_LocationSelect_New objManage_LocationSelect_New = new Manage_LocationSelect_New(UserData, Progid, Werks, Lgort, Type);
                    objManage_LocationSelect_New.ShowDialog();
                    txtLocat.Text = objManage_LocationSelect_New.Locat;
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
        }
        #endregion
        #region 選擇入庫方式-新板入庫(New Pallet)
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
        #region 選擇入庫方式-加料入庫(Add In)
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
        #region Confirm
        private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			try
			{

				stsWarning.Text = "";
				DataTable dtTemp = new DataTable();
				strLocat = txtLocat.Text.Trim();
				
				if(cmbWerks.SelectedIndex != -1)
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
				else
					strWerks = "";

				if(cmbLgort.SelectedIndex != -1)
					strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
				else
					strLgort = "";

				if(Werks == "" || Lgort == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				
				//取得Sttyp及Lotyp

                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
				dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
				if(dtTemp.Rows.Count >= 1)
				{
					strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
					strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
				}
				else
				{
					stsWarning.Text = "Can't find the storage data!!";
					return;
				}
				
				//變動儲位才需要檢查儲位有沒有輸入
				if(Lotyp == "DYNAMIC LOCATION")
				{
					if(strType == "ADD")
					{
						if(txtLocat.Text.Trim() == "")
						{
							stsWarning.Text = "Please input a location first!!";
							this.txtLocat.Focus();
							return;
						}
					}
					if(strType == "NEW")
					{
						if(txtLocat.Text.Trim() == "")
						{
							txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
						}
						else
						{
							if(objPlantData.CheckStorageData(Werks, Lgort, Locat))
							{
								stsWarning.Text = "The location you input is not a empty location!!";
								this.txtLocat.Focus();
								return;
							}
						}
					}

					if(txtLocat.Text.Trim() == "")
					{
						stsWarning.Text = "Please input a location first!!";
						this.txtLocat.Focus();
						return;
					}
				
					if(!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
					{
						stsWarning.Text = "The location doesn't exist!!";
						this.txtLocat.Focus();
						return;
					}
					//找出庫別
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
					
					if(this.rdoAdd.Checked)
					{
					
						dtTemp = objStorageData.QueryLocatInsmk(strLocat);
						if(dtTemp.Rows.Count > 1)
						{
							stsWarning.Text = "The location has different stock and you can't add any new Part No!!";
							return;
						}
						strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
					}
				}
				else
				{
					//固定儲位時Location不能輸入
					if(this.txtLocat.Text.Trim() != "")
					{
						stsWarning.Text = "You can't input location because "+ strLgort +" is a fixed-Location storage!!";
						this.txtLocat.Focus();
						return;
					}
					//固定儲位時不能選擇Mixed Material
					if(this.chkMixed.Checked)
					{
						stsWarning.Text = "You can't choose mixed material function because "+ strLgort +" is a fixed-Location storage!!";
						this.chkMixed.Focus();
						return;
					}
				}
				
				gbHeader.Enabled = false;
				btnAdd.Enabled = true;
				btnAdd_Click(null, null);
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
        }
        #endregion
        #region Add
        private void btnAdd_Click(object sender, System.EventArgs e)
		{			
			try
			{
				stsWarning.Text = "";
				if(Lotyp == "DYNAMIC LOCATION")
				{
					//如果是選擇New, 則第一次按下Add時不限制庫別, 但只要選擇了一個物料, 之後按下Add就依照第一個物料的庫別為主
					if(rdoNew.Checked)
					{
						if(dtData.Rows.Count == 0 || dtData == null)
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
				if(CheckIsOpen("StorageIn_SMT_OnLineIn_Add"))
				{
					this.MdiParent.MdiChildren[intFormIndex].Close();
				}

				if(dtData.Rows.Count > 0)
				{
					this.Refid = dtData.Rows[0]["REFID"].ToString().Trim();
				}

				StorageIn_SMT_OnLineIn_Add objStorageIn_SMT_OnLineIn_Add = new StorageIn_SMT_OnLineIn_Add(ref UserData, Progid, Werks, Lgort, Locat, Sttyp, Lotyp, "", Insmk, "", dtData, Duplicate,this.Refid, "");
				objStorageIn_SMT_OnLineIn_Add.MdiParent = this.ParentForm;
				objStorageIn_SMT_OnLineIn_Add.Refid = this.Refid;
				objStorageIn_SMT_OnLineIn_Add.objSMT_OnLineIn = this;
				objStorageIn_SMT_OnLineIn_Add.Show();
				dtData = objStorageIn_SMT_OnLineIn_Add.SapData;
				this.Refid = objStorageIn_SMT_OnLineIn_Add.Refid;
				ShowDataGrid();
				btnSave.Enabled = true;
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}
        #endregion
        #region 點選入庫資料
        private void dtgData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            //try
            //{
            //    int intRowNo;
            //    string strTempLocat = "";
            //    string strMblnr = "";
            //    stsWarning.Text = "";
            //    DataGrid dgClick = (DataGrid)sender;
            //    DataGrid.HitTestInfo hitRow;
            //    hitRow = dgClick.HitTest(e.X, e.Y);
            //    if(hitRow.Type == DataGrid.HitTestType.RowHeader)
            //    {
            //        //如果是選擇New, 則第一次按下Add時不限制庫別, 但只要選擇了一個物料, 之後按下Add就依照第一個物料的庫別為主
            //        if(rdoNew.Checked)
            //        {
            //            if(dtData.Rows.Count == 0 || dtData == null)
            //            {
            //                strInsmk = "";
            //            }
            //            else
            //            {
            //                strInsmk = dtData.Rows[0]["INSMK"].ToString();
            //            }
            //        }

            //        intRowNo = hitRow.Row;
            //        dgClick.CurrentCell = new DataGridCell(intRowNo,4);
            //        strMatnr = dgClick[dgClick.CurrentCell].ToString(); 
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 5);
            //        strInsmk = dgClick[dgClick.CurrentCell].ToString();
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 6);
            //        strCharg = dgClick[dgClick.CurrentCell].ToString();
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
            //        strTempLocat = dgClick[dgClick.CurrentCell].ToString(); 
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 2);
            //        strMblnr = dgClick[dgClick.CurrentCell].ToString(); 
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 1);
            //        strRefid = dgClick[dgClick.CurrentCell].ToString(); 
					
            //        StorageIn_SMT_OnLineIn_Add objStorageIn_SMT_OnLineIn_Add = new StorageIn_SMT_OnLineIn_Add(ref UserData, Progid, Werks, Lgort, strTempLocat, Sttyp, Lotyp, Matnr, Insmk, Charg, dtData, Duplicate,this.Refid, strMblnr);
            //        objStorageIn_SMT_OnLineIn_Add.Refid = this.Refid;
            //        objStorageIn_SMT_OnLineIn_Add.ShowDialog();
            //        dtData = objStorageIn_SMT_OnLineIn_Add.SapData;
            //        this.Refid = objStorageIn_SMT_OnLineIn_Add.Refid;
            //        ShowDataGrid();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    stsWarning.Text = ex.Message;
            //    return;
            //}
        }
        #endregion
        # region Click RowHeader
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string strTempLocat = "";
            string strMblnr = "";
            stsWarning.Text = "";

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
                strMatnr = dgvData.CurrentRow.Cells[4].Value.ToString();
                strInsmk = dgvData.CurrentRow.Cells[5].Value.ToString();
                strCharg = dgvData.CurrentRow.Cells[6].Value.ToString();
                strTempLocat = dgvData.CurrentRow.Cells[0].Value.ToString();
                strMblnr = dgvData.CurrentRow.Cells[2].Value.ToString();
                strRefid = dgvData.CurrentRow.Cells[1].Value.ToString();


                StorageIn_SMT_OnLineIn_Add objStorageIn_SMT_OnLineIn_Add = new StorageIn_SMT_OnLineIn_Add(ref UserData, Progid, Werks, Lgort, strTempLocat, Sttyp, Lotyp, Matnr, Insmk, Charg, dtData, Duplicate, this.Refid, strMblnr);
                objStorageIn_SMT_OnLineIn_Add.Refid = this.Refid;
                objStorageIn_SMT_OnLineIn_Add.ShowDialog();
                dtData = objStorageIn_SMT_OnLineIn_Add.SapData;
                this.Refid = objStorageIn_SMT_OnLineIn_Add.Refid;
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion
        #region Save
        private void btnSave_Click(object sender, System.EventArgs e)
		{

            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
            QCI.QWMS.MixedMaterial objMixedMaterial = new QCI.QWMS.MixedMaterial(UserData, Werks);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

            #region QWMS原本的判斷式
			SetbtnSaveProcess();
			string strTempMblnrMatnr = "";
			ArrayList aryMixedMaterial = new ArrayList();
			DataTable dtTemp = new DataTable();
			stsWarning.Text = "";
			if(dtData.Rows.Count == 0)
			{
				stsWarning.Text = "The data can't be empty!!";
				MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				SetbtnSaveException();
				return;
			}
			else
			{
				this.Refid = dtData.Rows[0]["REFID"].ToString().Trim();
            }

            #region Add check logic:dtData[0]["LOCAT"],["MATNR"],["CHARG"] compare with WHITM.LOCAT,MATNR,CHARG by Jack 20151123
            if (rdoAdd.Checked && Comcd == "9900" && ((Werks == "CS90" && (Lgort == "TW51" || Lgort == "TW10" || Lgort == "TW50")) || (Werks == "CS91" && (Lgort == "TW51")) || (Werks == "CS92" && (Lgort == "TW60" || Lgort == "TW70" || Lgort == "TW51"))))
            {
                DataTable dtLocMatVer = new DataTable();//reason Plant,Storage,Locat,Matnr,SELECT * FROM WHITM
                StringBuilder sbMatnrs = new StringBuilder();
                sbMatnrs.Append("'"+dtData.Rows[0]["MATNR"].ToString());
                for (int i = 1; i < dtData.Rows.Count; i++)
                {
                    sbMatnrs.Append("','" + dtData.Rows[i]["MATNR"]);
                }
                sbMatnrs.Append("'");
                //检查这几颗料在该仓别、储位是否有库存.
                dtLocMatVer = objStorageData.CheckLocatMatnrCharg(dtData.Rows[0]["LOCAT"].ToString(), sbMatnrs.ToString().Trim());
                //若有,则提示不允许入库.
                if (int.Parse(dtLocMatVer.Rows[0][0].ToString()) > 0)//have same P/N or Version stock,not allow in.
                {
                    DialogResult drResult = MessageBox.Show("该储位:" + txtLocat.Text + "有与此DID:" + Refid + "相同料号版本的库存,【不允许入库到该储位】,请确认是否继续入库?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (drResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            #endregion

            for (int i=0;i<dtData.Rows.Count;i++)
			{
				if(strTempMblnrMatnr.IndexOf(dtData.Rows[i]["LOCAT"].ToString()+dtData.Rows[i]["MBLNR"].ToString()+dtData.Rows[i]["MATNR"].ToString()) == -1)
				{
					strTempMblnrMatnr += dtData.Rows[i]["LOCAT"].ToString()+dtData.Rows[i]["MBLNR"].ToString()+dtData.Rows[i]["MATNR"].ToString() + ";";
				}
				else
				{
					stsWarning.Text = "The Document No and Part No you input is duplicate!!";
					MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;
				}

				//如果勾選連板
				if(chkMixed.Checked)
				{
					aryMixedMaterial.Add(dtData.Rows[i]["MATNR"].ToString());
					//檢查連板料號數量有無相同
					if(dtData.Rows[i]["ALQTY"].ToString() != dtData.Rows[0]["ALQTY"].ToString())
					{
						stsWarning.Text = "The mixed materials don't have the same Qty!!";
						MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
				}
			}
				
			//如果勾選連板
			if(chkMixed.Checked)
			{
				//檢查料號組合有無設定連板
				strMrgid = objMixedMaterial.QueryMixedMaterialID(aryMixedMaterial);
				if(Mrgid == "")
				{
					stsWarning.Text = "The materials you select is not mixed material!!";
					MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;
				}
				//檢查儲位是否已經有同料號但不同連板編號資料存在
				if(objMixedMaterial.QueryExistDifferentMixedMaterial(Lgort, Locat, Mrgid))
				{
					stsWarning.Text = "The location has existed another mixed material!!";
					MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;
				}
				for(int i=0;i<dtData.Rows.Count;i++)
				{
					dtData.Rows[i]["MRGID"] = Mrgid;
					//檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
					if(objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
					{
						stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location!";
						MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
					//如果可以允許同一儲位置放不同版本的料號, Kent 20050130
					if(this.Duplicate == false)
					{
						//檢查要入庫的儲位是否已經有相同料號但不同版本)
						if(objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
						{
							stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
							MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
							SetbtnSaveException();
							return;
						}
					}
				}
			}
			else
			{
				for(int i=0;i<dtData.Rows.Count;i++)
				{
					//檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
					if(objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
					{
						stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
						MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
					//如果可以允許同一儲位置放不同版本的料號, Kent 20050130
					if(this.Duplicate == false)
					{
						//檢查要入庫的儲位是否已經有相同料號但不同版本)
						if(objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
						{
							stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
							MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
							SetbtnSaveException();
							return;
						}
					}
				}
			}

            #region 个别仓别有特殊要求
            if ((strWerks == "CS32" && strLgort == "TW20") || (strWerks == "CS90" && (strLgort == "TW20" || strLgort == "TW70" || strLgort == "TW50")) || (strWerks == "CS91" && (strLgort == "TW20" || strLgort == "TW50" || strLgort == "TW51")) || (strWerks == "CS92" && (strLgort == "TW20" || strLgort == "TW51" || strLgort == "TW50")))
            {
                #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank
                if (dtData.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        string strloc = dtData.Rows[i]["LOCAT"].ToString();
                        string strinsmk = dtData.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                        string strvander = dtData.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtData.Rows[i]["CHARG"].ToString();
                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                            return;
                        }

                    }
                }
                #endregion
            }
            else if ((strWerks == "CS12" && (strLgort == "TW60" || strLgort == "TW20" || strLgort == "TW11" || strLgort == "TW52" || strLgort == "TW53" || strLgort == "TW12")) || (strWerks == "CS20" && (strLgort == "TW22" || strLgort == "TW25" || strLgort == "TWEJ" || strLgort == "TW91" || strLgort == "TW31" || strLgort == "TW51" || strLgort == "TW15")) || (Comcd == "9200" && strWerks == "CS42" && (strLgort == "TW60" || strLgort == "TW61")))
            {
                #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank
                if (dtData.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        string strloc = dtData.Rows[i]["LOCAT"].ToString();
                        string strinsmk = dtData.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                        string strvander = dtData.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtData.Rows[i]["CHARG"].ToString();

                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                            return;
                        }
                    }
                }
                #endregion
            }
            #endregion

			#endregion

			DataTable dtStorageByPatnum = new DataTable();
			DataTable dtStorage = new DataTable();
			DataTable dtSapInventory = new DataTable();
			string strOMBLNR="";

			try
			{
				#region 只能有一個Reference ID
				string strAllReferenceID = "";
				ArrayList arrAllReferenceID = new ArrayList();
				for(int i=0;i<dtData.Rows.Count;i++)
				{
					if(strAllReferenceID.IndexOf(dtData.Rows[i]["REFID"].ToString().Trim()) < 0)
					{
						strAllReferenceID += dtData.Rows[i]["REFID"].ToString().Trim() + ";";
						arrAllReferenceID.Add(dtData.Rows[i]["REFID"].ToString());
					}
					
				}
				if(arrAllReferenceID.Count > 1)
				{
					MessageBox.Show("You can not process more than one Reference ID!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;
				}
				#endregion
				
				#region 判斷1.刷條碼的數據與QSMS給的一致,2.DID Item數量、項目一致 by Rock Tzeng

				#region 1.比對刷條碼的數據與QSMS給的是否一致
               
				DataTable dtQsmsData = objPlantData.GetRefIDData(this.Mandt,this.Comcd, this.Werks,this.Lgort,this.Refid);
				if(dtQsmsData.Rows.Count != dtData.Rows.Count)
				{
					MessageBox.Show("處理筆數與QSMS給的不ㄧ致!!請確認是否有遺漏或多刷資料!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;
				}
				DataTable dtTmpSort;
				string strSort="";
				DataRow [] arrdrTmpSort;

				#region Sort dtData(處理中的資料)
				dtTmpSort=dtData.Clone();
				strSort = "REFID,MBLNR";
				arrdrTmpSort = dtData.Select("",strSort);
				for (int i = 0 ; i < arrdrTmpSort.Length ; i++ )
				{
					dtTmpSort.Rows.Add(arrdrTmpSort[i].ItemArray);
				}
				dtData = dtTmpSort;
				#endregion

				#region Sort dtQsmsData(QSMS給的資料)
				dtTmpSort=dtQsmsData.Clone();
				strSort = "REFID,DIDNO";
				arrdrTmpSort = dtQsmsData.Select("",strSort);
				for (int i = 0 ; i < arrdrTmpSort.Length ; i++ )
				{
					dtTmpSort.Rows.Add(arrdrTmpSort[i].ItemArray);
				}
				dtQsmsData = dtTmpSort;
				#endregion

				for (int i = 0 ; i < dtData.Rows.Count ; i++ )
				{
					if (dtData.Rows[i]["REFID"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["REFID"].ToString().Trim().ToUpper())
					{
						MessageBox.Show("REFID '" + dtData.Rows[i]["REFID"].ToString()+"'  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
					if (dtData.Rows[i]["MBLNR"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["DIDNO"].ToString().Trim().ToUpper())
					{
						MessageBox.Show("DID No. '" + dtData.Rows[i]["MBLNR"].ToString()+"'  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
					if (dtData.Rows[i]["ALQTY"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["MENGE"].ToString().Trim().ToUpper())
					{
						MessageBox.Show("DID No. '" + dtData.Rows[i]["MBLNR"].ToString()+"' Q'ty  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						SetbtnSaveException();
						return;
					}
			
                    //檢查目前單據數量是否足夠
                    int QTY_remain = 0;//剩餘可扣數量

                    if (Convert.ToInt32(dtQsmsData.Rows[i]["MENGE"].ToString()) > Convert.ToInt32(dtQsmsData.Rows[i]["OTQTY"].ToString()))
                        QTY_remain = Convert.ToInt32(dtQsmsData.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(dtQsmsData.Rows[i]["OTQTY"].ToString());
 
                    if(QTY_remain < Convert.ToInt32(dtData.Rows[i]["ALQTY"].ToString()))
                    {
                        MessageBox.Show("DID No. '" + dtData.Rows[i]["MBLNR"].ToString() + "' Q'ty  doesn't enough!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SetbtnSaveException();
                        return;
                    }
				}

				#endregion

                #region 比對SAP庫存(Deleted)
                /*
                dtStorageByPatnum = objPlantData.GetStoragebyPatnum(this.Mandt, this.Comcd, this.Werks, this.Lgort, this.Refid);// 重撈WHRID資訊 group by MATNR,OTWRK,OTLGT
                if (dtStorageByPatnum.Rows.Count > 0)
                {
                    //記錄QuerySapInventory開始與結束的時間  Smose Liao 20100412
                    //dtSapInventory = objStorageIn.QuerySapInventory(dtStorageByPatnum, dtData);

                    //新版RFC寫法，透過TXT文檔存取SAP庫存數量  Smose Liao 20120719
                    stsWarning.Text = "系統正在讀取SAP的庫存數量中，請稍候!!";
                    dtSapInventory = objStorageIn.QuerySapInventory_ByTxt(dtStorageByPatnum, dtData);

                    if (dtSapInventory.Rows.Count <= 0)
                    {
                        MessageBox.Show("REFID: " + this.Refid.ToString().Trim() + " SAP QTY ERROR!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SetbtnSaveException();
                        return;
                    }
                    for (int i = 0; i < dtSapInventory.Rows.Count; i++)
                    {
                        if (double.Parse(dtSapInventory.Rows[i]["MENGE"].ToString().Trim()) > double.Parse(dtSapInventory.Rows[i]["SAPQY"].ToString().Trim()))
                        {
                            #region 當QWMS庫存與SAP庫存不一致時，不提示錯誤訊息且還能繼續扣SAP帳，因為SAP有防呆限制，數量不足還是無法扣帳  Smose Liao 20120731
                            //SAP庫存及真正庫存 
                            //MessageBox.Show("Part NO: " + dtSapInventory.Rows[i]["MATNR"].ToString().Trim() + " Qty(" + dtSapInventory.Rows[i]["MENGE"].ToString() + ") doesn't match with SAP(" + dtSapInventory.Rows[i]["SAPQY"].ToString() + ")!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //SetbtnSaveException();
                            //return;
                            #endregion
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Referencr ID: " + this.Refid.Trim() + " isn't Correct !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return;
                }
                */
                #endregion

				#endregion
				
				this.pbrProgressBar.Maximum = 10; 
				this.pbrProgressBar.Value =0; 
				this.pbrProgressBar.Visible = true;
				this.tmProgressTimer.Interval = 1; 
				this.tmProgressTimer.Enabled = true; 
				this.btnRefresh.Enabled = false;
				this.btnExit.Enabled = false;
				Application.DoEvents();

                #region SAP扣帳

                try
                {
                    dtStorage = objPlantData.GetSAPDataByRefID(this.Mandt, this.Comcd, this.Werks, this.Lgort, this.Refid.Trim());// 查詢WHRID資訊
                    //記錄UpdateSapInventory開始與結束的時間  Smose Liao 20100412
                    //dtStorage = objStorageIn.wsUpdateSapInventory(dtStorage);//扣帳

                    //新版RFC寫法，透過TXT文檔的存取扣SAP帳，並取得SAP回傳的扣帳編號  Smose Liao 20120724
                    stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                    AllowToClose = false;//強制User無法關閉視窗
                    dtStorage = StorageToSAP(dtStorage, Locat);
                    //dtStorage = objStorageIn.wsUpdateSapInventory_SMT_ByTxt(dtStorage,Locat);//扣帳
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return;
                }

                if (dtStorage.Rows[0]["OMBLN"].ToString().Trim() == "")
                {
                    //stsWarning.Text = objStorageIn.ERRMSG;
                    MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return;
                }
                else
                {
                    //更新扣帳編號
                    strOMBLNR = dtStorage.Rows[0]["OMBLN"].ToString().Trim();
                }

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["OMBLNR"] = strOMBLNR;
                    dtData.Rows[i]["RMAK1"] = this.Refid.Trim();
                }

                #endregion

                QCI.QWMS.StorageIn objStorageIn2 = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                if (objStorageIn2.AddOnLineSMTInData(Mrgid, dtData))
				{
					
					stsWarning.Text = "SAP Posting OK("+ strOMBLNR +")!!";
                    AllowToClose = true;//扣帳成功，恢復可以關閉Form視窗
					this.btnAdd.Enabled = false;
					this.btnSave.Enabled = false;
					this.tmProgressTimer.Enabled = false; 
					this.pbrProgressBar.Visible = false;
					this.btnRefresh.Enabled = true;
					this.btnExit.Enabled = true;
                    #region 增加和ASRS接口

                    QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);

                    if (objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
                    {

                        DataTable dtASRS = new DataTable();
                        dtASRS.TableName = "QWMS";

                        dtASRS.Columns.Add("TRN_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("SEQ_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("TRN_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("LOC", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("ITEM_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STK", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VER", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VENDOR", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("QTY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PUR_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PO_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PLANT", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PRIORITY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STORAGE_TYPE", typeof(string)).DefaultValue = string.Empty;

                        int j = 1;

                        string strTRN_NO = objInterface.CreateAsrsNo();

                        foreach (DataRow dr in dtData.Rows)
                        {
                            DataRow drASRS = dtASRS.NewRow();

                            drASRS["TRN_NO"] = strTRN_NO;
                            drASRS["SEQ_NO"] = objInterface.Createseq_no(j);
                            drASRS["TRN_TYPE"] = "G+";
                            drASRS["LOC"] = dr["LOCAT"];
                            drASRS["ITEM_NO"] = dr["MATNR"];
                            drASRS["STK"] = dr["INSMK"];
                            drASRS["VER"] = dr["CHARG"];
                            drASRS["VENDOR"] = dr["LIFNR"];
                            drASRS["QTY"] = dr["ALQTY"];
                            drASRS["PUR_TYPE"] = string.Empty;
                            drASRS["PO_NO"] = dr["EBELN"];
                            drASRS["PLANT"] = dr["WERKS"];
                            drASRS["PRIORITY"] = string.Empty;
                            drASRS["STORAGE_TYPE"] = dr["LGORT"];

                            dtASRS.Rows.Add(drASRS);
                            j++;
                        }
                        //bool bolresult = objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false;

                        if (objInterface.PostStorageInData(dtASRS) == "SUCCESS" )
                        {
                            stsWarning.Text = "Add OK!!,数据已同步到ASRS";
                        }
                    }

                    #endregion
					return;
				}
				else
				{
					//stsWarning.Text = "SAP Posting OK("+ strOMBLNR +") but QWMS Save Fail!!Reference id連線入庫作業失敗，SAP已扣帳但QWMS未扣帳，請使用SAP單據連線入庫!! " + objStorageIn.ERRMSG;
                    stsWarning.Text = "SAP Posting OK(" + strOMBLNR + ") but QWMS Save Fail!!Reference id連線入庫作業失敗，SAP已扣帳但QWMS未扣帳，請使用SAP單據連線入庫!! ";
					SetbtnSaveException();
					MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Error );
					this.btnAdd.Enabled = false;
					this.btnSave.Enabled = false;
					this.tmProgressTimer.Enabled = false; 
					this.pbrProgressBar.Visible = false;
					this.btnRefresh.Enabled = true;
					this.btnExit.Enabled = true;
					return;
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				SetbtnSaveException();
				return;
			}
		}
        #endregion
        #region Refresh
        private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			stsWarning.Text = "";
			this.rdoAdd.Checked = false;
			this.rdoNew.Checked = false;
			this.chkMixed.Checked = false;
			this.gbFunction.Enabled = true;
			this.txtLocat.Text = "";
			this.gbHeader.Enabled = false;
			this.dtData.Rows.Clear();
			this.dgvData.DataSource = null;
            this.dgvData.Columns.Clear();
			this.btnAdd.Enabled = false;
			this.btnSave.Enabled = false;
			strWerks = "";
			strLgort = "";
			strInsmk = "";
			strRefid = "";
        }
        #endregion
        #region Exit
        private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
        }
        #endregion
        #region 重設Panel大小位置
        private void StorageIn_SMT_OnLineIn_Resize(object sender, System.EventArgs e)
		{
			panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width  - this.stsUsrnm.Width - this.stsDate.Width;
        }
        #endregion
        #region 輸入儲位
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
				try
				{
					btnConfirm_Click(null, null);
				}
				catch(Exception ex)
				{
					stsWarning.Text = ex.Message;
					return;
				}
			}
        }
        #endregion
        #region 確認視窗是否已經打開
        private bool CheckIsOpen(string strForm)
		{
			bool bolOpened = false;
			string strTest = "";
			try
			{
				for(int i=0;i<this.MdiParent.MdiChildren.Length;i++)
				{
					strTest = MdiParent.MdiChildren[i].ToString();
					if(MdiParent.MdiChildren[i].ToString().IndexOf(strForm) != -1)
					{
						bolOpened = true;
						intFormIndex = i;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-CheckIsOpen()");
			}
			return bolOpened;
        }
        #endregion
        #region 按鈕設定-儲存中
        private void SetbtnSaveProcess ()
		{
			this.btnAdd.Enabled = false;
			this.btnSave.Enabled = false;

        }
        #endregion
        #region 按鈕設定-儲存失敗
        private void SetbtnSaveException ()
		{
			this.btnAdd.Enabled = true;
			this.btnSave.Enabled = true;	
			this.tmProgressTimer.Enabled = false; 
			this.pbrProgressBar.Visible = false;
			this.btnRefresh.Enabled = true;
			this.btnExit.Enabled = true;
		}
        #endregion
        #region tmProgressTimer_Tick
        private void tmProgressTimer_Tick(object sender, System.EventArgs e)
		{
			if(this.pbrProgressBar.Value == this.pbrProgressBar.Maximum)
			{
				this.pbrProgressBar.Value = 0;
			}
			this.pbrProgressBar.Value += 2;
			Application.DoEvents();
        }
        #endregion
        #region 查詢Reference ID
        private void btnQueryRefid_Click(object sender, System.EventArgs e)
		{
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                if (dtData.Rows.Count == 0)
                {
                    MessageBox.Show("Please insert data first!");
                }
                else
                {
                    string strRefID = dtData.Rows[0]["REFID"].ToString();
                    DataTable dtRefID = new DataTable();

                    dtRefID = objPlantData.GetRefIDData(this.strMandt, this.Comcd, this.strWerks, this.strLgort, strRefID);
                    StorageIn_SMT_Query_RefID objStorageIn_SMT_Query_RefID = new StorageIn_SMT_Query_RefID(dtRefID, dtData, "SMT", UserData);
                    objStorageIn_SMT_Query_RefID.Show();
                }
            }
            catch(Exception ex)
            {
                
            
            }
        }
        #endregion
        #region 判斷是否可關閉Form視窗
        private void StorageIn_SMT_OnLineIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowToClose;

            if (e.Cancel = !AllowToClose)
            {
                stsWarning.Text = "扣SAP帳正在處理中，無法關閉視窗!!";
            }
        }
        #endregion

        private DataTable StorageToSAP(DataTable dtStorage, string strLocat)
        {
            string strError = "";
            string strPost = "";
            string strStatus = "";
            string strException = "";

            //if (dtStorage.Rows[0]["REFID"].ToString().Substring(0, 3) == "SR-")
            //{
            //    strStatus = "T";  //來源倉別若不為空，則設定傳送的參數為"T"
            //}
            strStatus = "T";//所有的311轉倉資料，Status狀態都需傳'T'  Smose Liao 20130523

            DataTable dtOutTable = new DataTable();
            dtOutTable.TableName = "SAP";
            DataColumnCollection columns = dtOutTable.Columns;
            columns.Add("REFNO", typeof(System.String));         //Reference ID
            columns.Add("PLANT", typeof(System.String));         //廠區
            columns.Add("MATNR", typeof(System.String));         //料號
            columns.Add("KOSTL", typeof(System.String));　　　　 //Cost Center
            columns.Add("LGORTF", typeof(System.String));        //發料倉別
            columns.Add("LGORTT", typeof(System.String));        //入庫倉別
            columns.Add("RETQTY", typeof(System.Decimal));       //數量
            columns.Add("CHARG", typeof(System.String));         //版本
            columns.Add("STATUS", typeof(System.String));        //狀態

            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {
                DataRow dr = dtOutTable.NewRow();
                dr[0] = dtStorage.Rows[i]["REFID"].ToString().Trim();
                dr[1] = dtStorage.Rows[i]["WERKS"].ToString().Trim();
                dr[2] = dtStorage.Rows[i]["MATNR"].ToString().Trim();
                dr[3] = dtStorage.Rows[i]["KOSTL"].ToString().Trim();
                dr[4] = dtStorage.Rows[i]["OTLGT"].ToString().Trim();
                dr[5] = dtStorage.Rows[i]["LGORT"].ToString().Trim();
                dr[6] = dtStorage.Rows[i]["MENGE"].ToString().Trim();
                if (dtStorage.Rows[i]["CHARG"].ToString() != "")//版本
                {
                    dr[7] = dtStorage.Rows[i]["CHARG"].ToString().Trim();
                }
                dr[8] = strStatus;//狀態

                dtOutTable.Rows.Add(dr);
            }

            DataTable dtResponse = new DataTable();
            QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, strProgid);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

            try
            {
                #region 記錄UpdateSapInventory開始的時間  Lora
                objStorageIn.AddErrorLog(dtStorage, strLocat);
                #endregion


                dtResponse = SendToSAP(dtOutTable).Tables[0];

                for (int i = 0; i < dtResponse.Rows.Count; i++)
                {
                    if (dtResponse.Rows[i]["OMBLN"].ToString() != "")
                    {
                        strPost = dtResponse.Rows[i]["OMBLN"].ToString();
                    }

                    if (dtResponse.Rows[i]["ERRMSG"].ToString() != "")
                    {
                        //Error Message有回傳值，跳出迴圈
                        strError = dtResponse.Rows[i]["ERRMSG"].ToString();
                        break;
                    }
                }

                #region 記錄UpdateSapInventory結束的時間  Smose Liao 20100412
                objStorageIn.AddErrorLog(dtStorage, "Update");
                #endregion
            }
            catch (Exception ex)
            {
                strException = ex.Message;
                throw new Exception(ex.Message);
            }
            #region 比對SAP回傳的錯誤訊息是否含有扣帳編號，有的話取出扣帳編號
            int MathIndex = 0;
            string strDuplicatDoc = "";
            bool bolTryParse = false;
            long outNumber = 0;
            if (strError != "")
            {
                Regex pattern = new Regex(@"49\d{8}");//比對的pattern，SAP回傳的扣帳編號為49開頭
                Match m = pattern.Match(strError);

                #region 檢查SAP回傳的扣帳編號是否全為數值型態，避免誤判抓到料號的情況下，誤入了QWMS庫存
                //【例】SAP回傳的錯誤訊息：3VPJ7AB0000F3AFD Material Batch is not mapping with WO
                while (m.Success)
                {
                    MathIndex = m.Index;//取得SAP回傳的扣帳編號之所在的位置
                    bolTryParse = long.TryParse(strError.Substring(MathIndex, 10), out outNumber);

                    if (bolTryParse == false)
                    {
                        strDuplicatDoc = "";
                        m = m.NextMatch();//往下一筆繼續搜尋
                    }
                    else
                    {
                        strDuplicatDoc = outNumber.ToString();//搜尋成功，取出SAP回傳的扣帳編號
                    }

                    if (strDuplicatDoc != "")
                    {
                        break;//搜尋成功，跳出while迴圈不再搜尋
                    }
                }
                #endregion
            }
            #endregion

            #region 把扣帳編號加入原始的Table
            if (strPost.Trim() != "")
            {
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    dtStorage.Rows[i]["OMBLN"] = strPost;
                }
            }
            else
            {
                //SAP已扣帳成功並回傳扣帳編號，但QWMS未扣帳成功
                if (int.Parse(dtStorage.Rows[0]["OTQTY"].ToString()) == 0 && strDuplicatDoc != "")
                {
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        dtStorage.Rows[i]["OMBLN"] = strDuplicatDoc;
                    }

                    strError = "";//清空strError
                }

                if (strError.Trim() == "")
                {
                    if (strException != "")
                    {
                        stsWarning.Text = " SAP Error : Can not post(" + Mandt + ")-" + "Please contact MIS staff." + "<- wsUpdateSapInventory_SMT_ByTxt()";
                    }
                }
                else
                {
                    stsWarning.Text = " SAP Error : " + Mandt + " " + strError + "<- wsUpdateSapInventory_SMT_ByTxt()";
                }
            }

            #endregion

            return dtResponse;
        }

        public DataSet SendToSAP(DataTable dtSap)
        {
            try
            {
                DataSet dsSap = new DataSet();
                dsSap.Tables.Add(dtSap);
                DataSet dsResult = new DataSet();
                PP.PP_Service objPP = new PP.PP_Service();
                dsResult = objPP.ZRFC_PP_311_AUTO_RETURN_M_WithPlant(strWerks, dsSap);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
