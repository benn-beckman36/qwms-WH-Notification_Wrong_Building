using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using System.IO;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Text;
using QWMS.Common;

namespace QWMS
{
	/// <summary>
	/// Summary description for StorageOut_StorageOut_Auto.
	/// </summary>
	public class StorageOut_StorageOut_Auto : System.Windows.Forms.Form
	{
		private ArrayList aryMblnr = new ArrayList();
		private DataTable dtOutSource = new DataTable();
		private DataTable dtStorage = new DataTable();
		private bool bolTransferIn = false;
		private string strProgid = "";
		private string strMandt = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort_From = "";
		private string strLgort_To = "";
		private string strInsmk = "";
		private decimal dcmMenge = 0;
		private string strLocat_From = "";
		private string strLocat_To = "";
		private string strMatnr = "";
		private string strType = "";
		private string strMrgid = "";
		private string strCharg = "";
		private string strSttyp = "";
		private string strLotyp = "";
		private string strRefid = "";
        private string strComcd = "";
		private bool bolDuplicate = false;
		private int intFormIndex = 0;
		private DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
		private MixedMaterial objMixedMaterial;
		private System.Windows.Forms.Button btnSave;
		protected internal System.Windows.Forms.StatusBar stbStatus;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
		private System.Windows.Forms.StatusBarPanel stsUsrnm;
		private System.Windows.Forms.StatusBarPanel stsWarning;
		private System.Windows.Forms.StatusBarPanel stsDate;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox gbHeader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbLgort_To;
		private System.Windows.Forms.ComboBox cmbWerks;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbLgort_From;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtLocatTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtLocatFrom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtCharg;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtQty;
		private System.Windows.Forms.TextBox txtPartNo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox cmbInsmk;
		private System.Windows.Forms.Panel panel7;
        private Label lblData;
        private DataGridView dgvData;		
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

		public string Lgort_From
		{
			get
			{
				return strLgort_From;
			}
			set
			{
				strLgort_From = value;
			}
		}

		public string Lgort_To
		{
			get
			{
				return strLgort_To;
			}
			set
			{
				strLgort_To = value;
			}
		}

		public string Locat_From
		{
			get
			{
				return strLocat_From;
			}
			set
			{
				strLocat_From = value;
			}
		}

		public string Locat_To
		{
			get
			{
				return strLocat_To;
			}
			set
			{
				strLocat_To = value;
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

		public decimal Menge
		{
			get
			{
				return dcmMenge;
			}
			set
			{
				dcmMenge = value;
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
		public ArrayList Mblnr
		{
			get
			{
				return aryMblnr;
			}
			set
			{
				aryMblnr = value;
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


		public StorageOut_StorageOut_Auto(UserInfo varUserData, string strProgid)
		{
			
			InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            strComcd = varUserData.CompanyCode;
			Progid = strProgid;

			try
			{
				//檢查權限
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
				if(!objStorageOut.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort_From();
					ShowDdlLgort_TO();
					ShowDdlInsmk();
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort_From.Items.Count > 0)
					{
						this.cmbLgort_From.SelectedIndex = 0;
					}
					if(cmbLgort_To.Items.Count > 0)
					{
						this.cmbLgort_To.SelectedIndex = 0;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		/// <summary>
		///清除任何使用中的資源。
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnSave = new System.Windows.Forms.Button();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLgort_To = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort_From = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbInsmk = new System.Windows.Forms.ComboBox();
            this.txtLocatTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocatFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCharg = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(38, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 34);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 585);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(832, 21);
            this.stbStatus.TabIndex = 39;
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
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(144, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 34);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(832, 90);
            this.panel1.TabIndex = 41;
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.btnConfirm);
            this.gbHeader.Controls.Add(this.label4);
            this.gbHeader.Controls.Add(this.cmbLgort_To);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.cmbLgort_From);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(29, 7);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(775, 83);
            this.gbHeader.TabIndex = 34;
            this.gbHeader.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(669, 37);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 34);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(509, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Storage To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbLgort_To
            // 
            this.cmbLgort_To.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort_To.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_To.Location = new System.Drawing.Point(624, 43);
            this.cmbLgort_To.Name = "cmbLgort_To";
            this.cmbLgort_To.Size = new System.Drawing.Size(36, 24);
            this.cmbLgort_To.TabIndex = 2;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(163, 15);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(300, 24);
            this.cmbWerks.TabIndex = 0;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage From";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbLgort_From
            // 
            this.cmbLgort_From.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_From.Location = new System.Drawing.Point(163, 45);
            this.cmbLgort_From.Name = "cmbLgort_From";
            this.cmbLgort_From.Size = new System.Drawing.Size(300, 24);
            this.cmbLgort_From.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbInsmk);
            this.groupBox1.Controls.Add(this.txtLocatTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLocatFrom);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtCharg);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtPartNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 82);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk.Enabled = false;
            this.cmbInsmk.Location = new System.Drawing.Point(355, 45);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(67, 24);
            this.cmbInsmk.TabIndex = 5;
            // 
            // txtLocatTo
            // 
            this.txtLocatTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocatTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocatTo.Enabled = false;
            this.txtLocatTo.Location = new System.Drawing.Point(643, 45);
            this.txtLocatTo.MaxLength = 10;
            this.txtLocatTo.Name = "txtLocatTo";
            this.txtLocatTo.Size = new System.Drawing.Size(17, 22);
            this.txtLocatTo.TabIndex = 8;
            this.txtLocatTo.DoubleClick += new System.EventHandler(this.txtLocatTo_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(653, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 161;
            this.label3.Text = "Location TO";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLocatFrom
            // 
            this.txtLocatFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocatFrom.Enabled = false;
            this.txtLocatFrom.Location = new System.Drawing.Point(19, 45);
            this.txtLocatFrom.MaxLength = 10;
            this.txtLocatFrom.Name = "txtLocatFrom";
            this.txtLocatFrom.Size = new System.Drawing.Size(125, 22);
            this.txtLocatFrom.TabIndex = 3;
            this.txtLocatFrom.DoubleClick += new System.EventHandler(this.txtLocatFrom_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 16);
            this.label7.TabIndex = 159;
            this.label7.Text = "Location From";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCharg
            // 
            this.txtCharg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCharg.Enabled = false;
            this.txtCharg.Location = new System.Drawing.Point(432, 45);
            this.txtCharg.MaxLength = 10;
            this.txtCharg.Name = "txtCharg";
            this.txtCharg.Size = new System.Drawing.Size(106, 22);
            this.txtCharg.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(355, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 16);
            this.label14.TabIndex = 155;
            this.label14.Text = "Stock";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(432, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 16);
            this.label13.TabIndex = 154;
            this.label13.Text = "Batch";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQty
            // 
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Enabled = false;
            this.txtQty.Location = new System.Drawing.Point(547, 45);
            this.txtQty.MaxLength = 10;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(87, 22);
            this.txtQty.TabIndex = 7;
            // 
            // txtPartNo
            // 
            this.txtPartNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartNo.Enabled = false;
            this.txtPartNo.Location = new System.Drawing.Point(163, 45);
            this.txtPartNo.MaxLength = 20;
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.Size = new System.Drawing.Size(173, 22);
            this.txtPartNo.TabIndex = 4;
            this.txtPartNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartNo_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(547, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 148;
            this.label6.Text = "Quantity";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 147;
            this.label8.Text = "Part Number";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(669, 37);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 34);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(832, 90);
            this.panel2.TabIndex = 43;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 540);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(832, 45);
            this.panel3.TabIndex = 44;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 90);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(832, 450);
            this.panel4.TabIndex = 45;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lblData);
            this.panel7.Controls.Add(this.dgvData);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(29, 90);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(775, 360);
            this.panel7.TabIndex = 44;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(0, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 3;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(0, 19);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(941, 302);
            this.dgvData.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(804, 90);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(28, 360);
            this.panel6.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 90);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(29, 360);
            this.panel5.TabIndex = 0;
            // 
            // StorageOut_StorageOut_Auto
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(832, 606);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "StorageOut_StorageOut_Auto";
            this.Text = "StorageOut_StorageOut_Auto";
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbHeader.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void ShowStatusData()
		{
			this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
			this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
			this.stsUsrnm.Text = Usrnm;
		}
		private void ShowDdlWerks()
		{
			DataTable dtTemp = new DataTable();
			try
			{
				cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

		private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			stsWarning.Text = "";
			ShowDdlLgort_From();
			ShowDdlLgort_TO();
		}

		private void ShowDdlLgort_From()
		{
			try
			{
				stsWarning.Text = "";
				DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				if(cmbWerks.SelectedIndex != -1)
				{
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
					//dtTemp = objPlantData.GetDdlLgortData(strWerks);
					dtTemp = objAuthority.CheckLgortAuthority(strWerks);
				}
				else
				{
					//dtTemp = objPlantData.GetDdlLgortData();
					dtTemp = objAuthority.CheckLgortAuthority();
				}
				if(cmbLgort_From.SelectedIndex != -1)
				{
					strLgort_From = cmbLgort_From.Items[cmbLgort_From.SelectedIndex].ToString();
				}
				else
				{
					cmbLgort_From.Items.Clear();
				}
				
				if(dtTemp.Rows.Count == 0)
				{
					cmbLgort_From.Items.Clear();
					strLgort_From = "";
				}
				else
				{
					cmbLgort_From.Items.Clear();
					for(int i=0;i<dtTemp.Rows.Count;i++)
					{
						cmbLgort_From.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
						if(dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort_From && strLgort_From != "")
						{
							cmbLgort_From.SelectedIndex = i;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
		}


		private void ShowDdlLgort_TO()
		{
			try
			{
				stsWarning.Text = "";
				DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				if(cmbWerks.SelectedIndex != -1)
				{
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
					//dtTemp = objPlantData.GetDdlLgortData(strWerks);
					dtTemp = objAuthority.CheckLgortAuthority(strWerks);
				}
				else
				{
					//dtTemp = objPlantData.GetDdlLgortData();
					dtTemp = objAuthority.CheckLgortAuthority();
				}
				if(cmbLgort_To.SelectedIndex != -1)
				{
					strLgort_To = cmbLgort_To.Items[cmbLgort_To.SelectedIndex].ToString();
				}
				else
				{
					cmbLgort_To.Items.Clear();
				}
				
				if(dtTemp.Rows.Count == 0)
				{
					cmbLgort_To.Items.Clear();
					strLgort_To = "";
				}
				else
				{
					cmbLgort_To.Items.Clear();
					for(int i=0;i<dtTemp.Rows.Count;i++)
					{
						cmbLgort_To.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
						if(dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort_To && strLgort_To != "")
						{
							cmbLgort_To.SelectedIndex = i;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
		}



		private void txtLocatFrom_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort_From.SelectedIndex != -1)
                {
                    Lgort_From = cmbLgort_From.Items[cmbLgort_From.SelectedIndex].ToString();
                }
                else
                {
                    Lgort_From = "";
                }

				if(Werks == "" || Lgort_From == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				else
				{
					strType = "ADD";
					Type = strType;
					StorageOut_LocationSelect objStorageOut_LocationSelect = new StorageOut_LocationSelect(UserData, Progid, Werks, Lgort_From, Type);
					objStorageOut_LocationSelect.ShowDialog();
					txtLocatFrom.Text = objStorageOut_LocationSelect.Locat;					
					Locat_From = txtLocatFrom.Text.Trim().ToString();
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}

		private void txtLocatTo_DoubleClick(object sender, System.EventArgs e)
		{
			try
			{
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort_To.SelectedIndex != -1)
                {
                    Lgort_To = cmbLgort_From.Items[cmbLgort_To.SelectedIndex].ToString();
                }
                else
                {
                    Lgort_To = "";
                }

				if(Werks == "" || Lgort_To == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				else
				{
					strType = "ALL";
					Type = strType;
					StorageOut_LocationSelect objStorageOut_LocationSelect = new StorageOut_LocationSelect(UserData, Progid, Werks, Lgort_To, Type);
					objStorageOut_LocationSelect.ShowDialog();
					txtLocatTo.Text = objStorageOut_LocationSelect.Locat;
					Locat_To = txtLocatTo.Text.Trim().ToString();
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}


		public void ShowDataGrid()
		{
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvclocat_From = new DataGridViewTextBoxColumn();
                dgvclocat_From.DataPropertyName = "LOCAT_FROM";
                dgvclocat_From.HeaderText = "Location From";
                dgvclocat_From.Width = 90;
                dgvclocat_From.ReadOnly = true;
                this.dgvData.Columns.Add(dgvclocat_From);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvclocat_To = new DataGridViewTextBoxColumn();
                dgvclocat_To.DataPropertyName = "LOCAT_TO";
                dgvclocat_To.HeaderText = "Location To";
                dgvclocat_To.Width = 90;
                dgvclocat_To.ReadOnly = true;
                this.dgvData.Columns.Add(dgvclocat_To);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			ArrayList alSQL = new ArrayList();

			this.EnableAddItems(false);
			this.btnSave.Enabled = false;
			stsWarning.Text = "";
//			string strMblnr = "MarcTest111111";

            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort_From, "", Progid);
            QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort_From, Progid);

			#region 防呆
			dtData.AcceptChanges();
			if(dtData.Rows.Count == 0)
			{
				stsWarning.Text = "The data can't be empty!!";
				SetbtnSaveException();
				return;
			}

			#region 入庫防呆

			
			DataTable dtTemp = new DataTable();

			for(int i=0;i<dtData.Rows.Count;i++)
			{

				//檢查要入庫的單號之前是否有使用連板入庫的方式入庫
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort_From);
				dtTemp = objStorageData.QueryNotCombineInData(Locat_To, dtData.Rows[i]["MBLNR"].ToString(), "0");
				if(dtTemp.Rows.Count > 0)
				{
					stsWarning.Text = "The Document No was stored in with mixed material last time!!";
					SetbtnSaveException();
					return;
				}

				//如果可以允許同一儲位置放不同版本的料號, Kent 20050130
				if(this.Duplicate == false)
				{
					//檢查要入庫的儲位是否已經有相同料號但不同版本)
					if(objStorageData.CheckExistedSameMaterial(Locat_To, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
					{
						stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
						SetbtnSaveException();
						return;
					}
				}
			}
			#endregion

			#endregion

			#region 比對SAP庫存
			
			DataTable dtSapInventory=objStorageIn.QuerySap318Inventory(dtData);
			if(dtSapInventory.Rows.Count <= 0)
			{
				MessageBox.Show( " SAP QTY ERROR!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				SetbtnSaveException();
				return;
			}
			for(int i=0;i<dtSapInventory.Rows.Count;i++)
			{
				if(double.Parse(dtSapInventory.Rows[i]["MENGE"].ToString().Trim()) > double.Parse(dtSapInventory.Rows[i]["SAPQY"].ToString().Trim()))
				{
					//SAP庫存及真正庫存
					MessageBox.Show("Part NO: "+dtSapInventory.Rows[i]["MATNR"].ToString().Trim() + " Qty("+ dtSapInventory.Rows[i]["MENGE"].ToString() +") doesn't match with SAP("+ dtSapInventory.Rows[i]["SAPQY"].ToString() +")!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					SetbtnSaveException();
					return;							
				}
			}

			#endregion

			#region//SAP扣帳

			try
			{
				dtStorage=objStorageIn.UpdateSapTransfer(dtData);//扣帳
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				SetbtnSaveException();
				return;
			}
	
			if(dtStorage.Rows[0]["MBLNR"].ToString().Trim() == "")
			{
				stsWarning.Text = objStorageIn.ERRMSG;
				SetbtnSaveException();
				return;
			}

			#endregion

			#region QWMS出入庫處理
			try
			{
				#region 填入SAP扣帳編號(MBLNR)
//				for(int i=0 ; i< dtData.Rows.Count;i++)
//				{
//					dtData.Rows[i]["MBLNR"] = strMblnr.Trim();
//				}
//				dtData.AcceptChanges();

				#endregion

				#region//出庫

				    //抓dtStorage
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort_From);
					DataSet dsData = objStorageData.QueryTransferOutDataByLocat(dtData);

					dtData = dsData.Tables["Source"].Copy();
					DataTable dtTransferOutStorage = dsData.Tables["Destination"].Copy();
					
					try
					{
						#region 匯整出庫資料轉入庫
						#region "Combine DataTable"
						ArrayList alKeys = new ArrayList();
						alKeys.Add("MANDT");
						alKeys.Add("WERKS");
						alKeys.Add("LGORT");
						alKeys.Add("LOCAT");
						alKeys.Add("MATNR");
						alKeys.Add("INSMK");
						alKeys.Add("CHARG");
						alKeys.Add("OMBLNR");

						Hashtable htCmpFields = new Hashtable();
						htCmpFields.Add("ALQTY", CmpAction.Sum);
						DataTable dtTmpOutStorage = CombineTable(dtTransferOutStorage, alKeys, htCmpFields);
						#endregion

						#endregion

						stsWarning.Text = "";
						alSQL = objStorageOut.AddTransferOutData_Auto(dtData, dtTmpOutStorage);

					}
					catch(Exception ex)
					{
						stsWarning.Text = ex.Message;
						SetbtnSaveException();
						return;
					}

				#endregion
				#region//入庫
				if(bolTransferIn==true)
				{
					try
					{
						#region 匯整出庫資料轉入庫
						#region "Combine DataTable"
						ArrayList alKeys = new ArrayList();
						alKeys.Add("MANDT");
						alKeys.Add("WERKS");
						alKeys.Add("LGORT_TO");
						alKeys.Add("LOCAT_TO");
						alKeys.Add("MATNR");
						alKeys.Add("INSMK");
						alKeys.Add("CHARG");
						alKeys.Add("MBLNR");

						

						Hashtable htCmpFields = new Hashtable();
						htCmpFields.Add("MENGE", CmpAction.Sum);
						htCmpFields.Add("ALQTY", CmpAction.Sum);
						DataTable dtCombinStorage = CombineTable(dtTransferOutStorage, alKeys, htCmpFields);
						#endregion

						#endregion


						#region 產生要入庫的DataTable(必須要符合Log需要的欄位)
						DataTable dtInStorage = new DataTable();
						

						#region 定義欄位
						dtInStorage.Columns.Add("MANDT");
						dtInStorage.Columns.Add("WERKS");
						dtInStorage.Columns.Add("LGORT");
						dtInStorage.Columns.Add("LOCAT");
						dtInStorage.Columns.Add("MATNR");
						dtInStorage.Columns.Add("CHARG");
						dtInStorage.Columns.Add("LIFNR");
						dtInStorage.Columns.Add("MBLNR");
						dtInStorage.Columns.Add("OMBLNR");
						dtInStorage.Columns.Add("EBELN");
						dtInStorage.Columns.Add("INSMK");
						dtInStorage.Columns.Add("MENGE");
						dtInStorage.Columns.Add("ALQTY");
						dtInStorage.Columns.Add("MRGID");
						dtInStorage.Columns.Add("TRNTP");
						dtInStorage.Columns.Add("KOSTL");
						dtInStorage.Columns.Add("ARBPL");
						dtInStorage.Columns.Add("RMAK1");
						dtInStorage.Columns.Add("INDAT");
						#endregion

						#region 設定資料
						for (int i = 0 ; i < dtCombinStorage.Rows.Count ; i++)
						{
							DataRow drTmp = dtInStorage.NewRow();
							drTmp["MANDT"] = dtCombinStorage.Rows[i]["MANDT"].ToString();
							drTmp["WERKS"] = dtCombinStorage.Rows[i]["WERKS"].ToString();
							drTmp["LGORT"] = dtCombinStorage.Rows[i]["LGORT_TO"].ToString();
							drTmp["LOCAT"] = dtCombinStorage.Rows[i]["LOCAT_TO"].ToString();
							drTmp["MATNR"] = dtCombinStorage.Rows[i]["MATNR"].ToString();
							drTmp["CHARG"] = dtCombinStorage.Rows[i]["CHARG"].ToString();
							drTmp["LIFNR"] = dtCombinStorage.Rows[i]["LIFNR"].ToString();
							drTmp["MBLNR"] = dtCombinStorage.Rows[i]["MBLNR"].ToString();
							drTmp["OMBLNR"] = dtCombinStorage.Rows[i]["OMBLNR"].ToString();
							drTmp["EBELN"] = "";
							drTmp["INSMK"] = dtCombinStorage.Rows[i]["INSMK"].ToString();
							drTmp["MENGE"] = "0";
							drTmp["ALQTY"] = dtCombinStorage.Rows[i]["ALQTY"].ToString();
							drTmp["MRGID"] = dtCombinStorage.Rows[i]["MRGID"].ToString();
							drTmp["TRNTP"] = "T+";
							drTmp["KOSTL"] = dtCombinStorage.Rows[i]["KOSTL"].ToString();
							drTmp["ARBPL"] = dtCombinStorage.Rows[i]["ARBPL"].ToString();
							drTmp["RMAK1"] = dtCombinStorage.Rows[i]["RMAK1"].ToString();
							drTmp["INDAT"] = DateTime.Now.ToString("yyyyMMdd");

							dtInStorage.Rows.Add(drTmp);
						}
						#endregion

						#endregion
						ArrayList alTmpSql = objStorageIn.AddTransferInData_Auto(dtInStorage);
						for (int i = 0 ; i< alTmpSql.Count ; i++ )
						{
							alSQL.Add(alTmpSql[i].ToString());
						}

					}
					catch(Exception ex)
					{
						stsWarning.Text = ex.Message;
						SetbtnSaveException();
						return;
					}
				}
				#endregion

				if(objStorageOut.ExecuteSQL(alSQL))
				{
					stsWarning.Text = "Update OK!!";
					this.btnSave.Enabled = false;

				}
				else
				{
					stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
					SetbtnSaveException();
					return;
				}

			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				SetbtnSaveException();
				return;
			}
			#endregion
	
		}
		private void SetbtnSaveException()
		{
			this.btnSave.Enabled = true;
		
		}
	



	
		private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				try
				{
					DataTable dtTmpCheckMatnr = new DataTable();	
					string strMatnr = txtPartNo.Text.Trim();			
					
					//檢查Part NO.是否存在
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
					dtTmpCheckMatnr=objPlantData.QueryPartData(this.Werks,this.Lgort_From,strMatnr,"LOG");
					if(dtTmpCheckMatnr.Rows.Count <= 0)
					{
						MessageBox.Show(strMatnr.Trim() + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.txtPartNo.Focus();
						this.txtPartNo.Text ="";
						
						this.txtPartNo.SelectAll();
						return;
					}
					else
					{
					}
					
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;

				}
			}
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			this.strWerks = "";
			this.strLgort_From = "";	
			this.strLgort_To = "";
			this.strLocat_From = "";
			this.strLocat_To = "";
            this.lblData.Text = "0 records";
			this.aryMblnr.Clear();
            this.dgvData.DataSource = null;
			this.dtData.Clear();
			this.dtStorage.Clear();		
			this.stsWarning.Text = "";
			this.cmbWerks.Text = "";
			this.cmbLgort_From.Text = "";
			this.cmbLgort_To.Text = "";
			txtLocatFrom.Text="";
			txtPartNo.Text="";
			this.cmbInsmk.Text = "G";
			txtCharg.Text="";
			txtQty.Text="";
			txtLocatTo.Text="";
			this.btnAdd.Enabled = true;
			this.btnSave.Enabled = false;
			this.EnableAddItems(false);
			this.EnableHeader(true);
		    ShowDataGrid();
		}

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			if (this.cmbWerks.SelectedIndex == -1)
			{
				MessageBox.Show("Must select plant!!");
				return;
			}
			if (this.cmbLgort_From.SelectedIndex == -1)
			{
				MessageBox.Show("Must select stroage from!!");
				return;
			}
			if (this.cmbLgort_To.SelectedIndex == -1)
			{
				MessageBox.Show("Must select stroage to!!");
				return;
			}
			if (this.cmbLgort_From.Text.Trim() == this.cmbLgort_To.Text.Trim() )
			{
				MessageBox.Show("Lgort_From & Lgort_To must be difference!!");
				return;
			}

			EnableHeader(false);
			EnableAddItems(true);

			#region 若Storage to沒有儲位
			string strTmpWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
			string strTmpLgort_to = this.cmbLgort_To.Items[this.cmbLgort_To.SelectedIndex].ToString();

            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
			DataTable dtTmpLocat = objPlantData.GetAllLocatData(strTmpWerks, strTmpLgort_to, "", "2");
			if (dtTmpLocat.Rows.Count == 0 )
			{
				this.txtLocatTo.Enabled = false;
			}
			else
			{
				this.txtLocatTo.Enabled = true;
			}
			#endregion

		}

		private void EnableAddItems(bool bolEnable)
		{
			this.txtLocatFrom.Enabled = bolEnable;
			this.txtPartNo.Enabled = bolEnable;
			this.cmbInsmk.Enabled = bolEnable;
			this.txtCharg.Enabled = bolEnable;
			this.txtQty.Enabled = bolEnable;
			this.txtLocatTo.Enabled = bolEnable;
			this.btnAdd.Enabled = bolEnable;
		}

		private void EnableHeader(bool bolEnable)
		{
			this.cmbWerks.Enabled = bolEnable;
			this.cmbLgort_From.Enabled = bolEnable;
			this.cmbLgort_To.Enabled = bolEnable;
			this.btnConfirm.Enabled = bolEnable;

		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			try 
			{
				#region 防呆

				#region WERKS
				if(cmbWerks.SelectedIndex == -1)
				{
					strWerks = "";
				}
				else
				{
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString().Trim();
				}
				if(strWerks == "")
				{
					MessageBox.Show("Plant can't be empty!!");
					this.cmbWerks.Focus();
					return;
				}
				#endregion
				
				#region Lgort_From
				if(cmbLgort_From.SelectedIndex == -1)
				{
					strLgort_From = "";
				}
				else
				{
					strLgort_From = cmbLgort_From.Items[cmbLgort_From.SelectedIndex].ToString().Trim();
				}
				if(strLgort_From == "")
				{
					MessageBox.Show("Storage_From can't be empty!!");
					this.cmbLgort_From.Focus();
					return;
				}
				#endregion

				#region Lgort_To
				if(cmbLgort_To.SelectedIndex == -1)
				{
					strLgort_To = "";
				}
				else
				{
					strLgort_To = cmbLgort_To.Items[cmbLgort_To.SelectedIndex].ToString().Trim();
				}
				if(strLgort_To == "")
				{
					MessageBox.Show("Storage_To can't be empty!!");
					this.cmbLgort_To.Focus();
					return;
				}
				#endregion

				#region 料號
				//料號不可空白
				if(this.txtPartNo.Text.Trim() == "")
				{
					MessageBox.Show("Part No can't be empty!!");
					this.txtPartNo.Focus();
					this.txtPartNo.SelectAll();
					return;
				}

				//檢查料號是否存在
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
				if(!objPlantData.CheckExistedMatnr(this.txtPartNo.Text.Trim()))
				{
					MessageBox.Show(this.txtPartNo.Text.Trim() + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				strMatnr = this.txtPartNo.Text.Trim();
				#endregion

				#region 庫別
				//庫別不可空白
				if(this.cmbInsmk.SelectedIndex == -1)
				{
					MessageBox.Show("Stock can't be empty!!");
					this.cmbInsmk.Focus();
					return;
				}
				strInsmk=cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString().Trim();
				#endregion

				#region 版本
				strCharg=txtCharg.Text.Trim();
				#endregion

				#region 數量
				//數量不可空白或0
				if(!CheckIsNumber(this.txtQty.Text.Trim()) || this.txtQty.Text.Trim() == "0")
				{
					MessageBox.Show("Store in Qty should be numeric and greater than 0!!");
					this.txtQty.Focus();
					this.txtQty.SelectAll();
					return;
				}
				dcmMenge=decimal.Parse(txtQty.Text.Trim());
				#endregion

				#region Locate_From
				strLocat_From=txtLocatFrom.Text.Trim();
				if(strLocat_From == "")
				{
					MessageBox.Show("Location_From can't be empty!!");
					this.txtLocatFrom.Focus();
					return;
				}
				else
				{
					if(!objPlantData.CheckExistedStorageData(Werks, strLgort_From, strLocat_From))
					{
						MessageBox.Show("The Location_From doesn't exist!!");
						this.txtLocatFrom.Focus();
						return;
					}
				
				}

				#endregion

				#region Locate_To
				strLocat_To=txtLocatTo.Text.Trim();
				if (txtLocatTo.Enabled == true)
					if (strLocat_To == "")
					{
						MessageBox.Show("Location_From can't be empty!!");
						this.txtLocatTo.Focus();
						return;
					}
				#endregion


				#endregion

				#region 檢查資料是否已增加到Grid中
				if (dtData.Rows.Count > 0)
				{
					StringBuilder sbSearch = new StringBuilder();
					sbSearch.Append(" MANDT='" + this.Mandt +  "' and WERKS='" + strWerks+ "' ");
					sbSearch.Append(" and LGORT_FROM='" + strLgort_From + "' and LOCAT_FROM ='" +strLocat_From + "' ");
//					sbSearch.Append(" and LGORT_TO='" + strLgort_To + "' and LOCAT_TO='" + strLocat_To + "' ");
					sbSearch.Append(" and MATNR='" +strMatnr+ "' and INSMK ='" +strInsmk+ "' and CHARG='" +strCharg+ "' ");

					DataRow [] arrSearch = dtData.Select(sbSearch.ToString());
					if (arrSearch.Length > 0)
					{
						MessageBox.Show("There are duplication data(the same Location_From & Part#) !! Please Check it!!");
						this.txtLocatTo.Focus();
						return;
					}

				}
				#endregion


				#region 入庫檢查
				//入庫檢查
				if(cmbLgort_To.Text=="")
				{
					MessageBox.Show("Please select the Storage_To field!!");
					this.cmbLgort_From.Focus();
					return;			
			
				}
				else
				{
					if(!objPlantData.CheckExistedStorage(Werks, strLgort_To))
					{
						bolTransferIn=false;  //無bin location不做入庫動作					
					}
					else  //有bin location
					{
					
						if(strLocat_To=="")
						{
							MessageBox.Show("Please insert the Locate_To field!!");
							this.txtLocatTo.Focus();
							return;
						}
						else
						{
							if(!objPlantData.CheckExistedStorageData(Werks, strLgort_To, strLocat_To))
							{
								MessageBox.Show("The Location_To doesn't exist!!");
								this.txtLocatTo.Focus();
								return;
							}
							else
							{
								bolTransferIn=true;//資料正確，可以進行入庫動作
							}
						}
					}
				}
				#endregion

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort_From);
				DataTable dtTmpTransfer = objStorageData.QueryTransferData(strWerks,strLgort_From,strLgort_To,strLocat_From,strLocat_To,strMatnr,dcmMenge, strInsmk,strCharg);
				
				if(dtTmpTransfer.Rows.Count <=0)
				{
					MessageBox.Show("Please make sure the data is correct!!");
					return;
				}
				else
				{
					if(dtData.Columns.Count == 0)
					{
						#region 設定dtData欄位
						dtData.Columns.Add("MANDT");
						dtData.Columns.Add("WERKS");
						dtData.Columns.Add("LGORT_FROM");
						dtData.Columns.Add("LGORT_TO");
						dtData.Columns.Add("LOCAT_FROM");
						dtData.Columns.Add("LOCAT_TO");
						dtData.Columns.Add("MATNR");
						dtData.Columns.Add("INSMK");
						dtData.Columns.Add("CHARG");
						dtData.Columns.Add("MENGE");
						dtData.Columns.Add("MBLNR");
						dtData.Columns.Add("ALQTY");
						#endregion
					}
					DataRow drNewRow = dtData.NewRow();
					drNewRow["MANDT"] = this.Mandt;
					drNewRow["WERKS"] = strWerks;
					drNewRow["LGORT_FROM"] = strLgort_From;
					drNewRow["LGORT_TO"] = strLgort_To;
					drNewRow["LOCAT_FROM"] = strLocat_From;
					drNewRow["LOCAT_TO"] = strLocat_To;
					drNewRow["MATNR"] = strMatnr;
					drNewRow["INSMK"] = strInsmk;
					drNewRow["CHARG"] = strCharg;
					drNewRow["MENGE"] = dcmMenge.ToString();
					drNewRow["MBLNR"] = "";
					drNewRow["ALQTY"] = "0";
					dtData.Rows.Add(drNewRow);
					dtData.AcceptChanges();
					ClearAdditem();
				}
				ShowDataGrid();
				btnSave.Enabled = true;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		
		}

		private void ClearAdditem()
		{
			this.txtLocatFrom.Text = "";
			this.txtPartNo.Text = "";
			this.cmbInsmk.Text ="G";
			this.txtCharg.Text = "";
			this.txtQty.Text ="";
			this.txtLocatTo.Text ="";
		
		}

		private void ShowDdlInsmk()
		{
			try
			{
				DataTable dtTemp = new DataTable();
				cmbInsmk.Items.Clear();
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
				dtTemp = objPlantData.GetDdlInsmk();
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlInsmk()");
			}
		}


		private bool CheckIsNumber(string strValue)
		{
			Regex rgxNumber = new Regex("[0-9]");
			return rgxNumber.IsMatch(strValue);
		}

		#region "Common Function"
		public enum CmpAction { Sum, StringCombine, EmptyString, ZeroInt, OneInt, Count}
		////////////將DataTable作GroupBy的動作 by Marc Hong/////////////////////////////////////////
		/// <summary>
		/// 將DataTable作GroupBy的動作
		/// </summary>
		/// <param name="varTargetTable">要Combine的raw data</param>
		/// <param name="varKeys">Combine時的Key</param>
		/// <param name="varCmpFields">Combine後要進行運算的欄位及做的運算</param>
		/// <returns>回傳值型態為DataTable，回傳Combine後的DataTable。</returns>
		/// <example>
		/// <code>
		///  Inventory objInventory = new Inventory();
		///  DataTable dtReturn = objInventory.CombineTable(dtPickListView,alKeys,htCmpFields);
		///  Your Code Here......
		/// </code>
		/// </example>
		/////////////////////////////////////////////////////////////////////////////
		public DataTable CombineTable(DataTable varTargetTable, ArrayList varKeys, Hashtable varCmpFields)
		{

			DataTable dtReturn = varTargetTable.Clone();
			try
			{
				
				if (varTargetTable == null || varTargetTable.Rows.Count < 1)
				{
					dtReturn = varTargetTable;
					return dtReturn;
				}
				#region "Combine Pick List rawdata "


				StringBuilder sbOrderBy = new StringBuilder();
				for (int k = 0; k < varKeys.Count; k++)
				{
					if (sbOrderBy.Length != 0)
						sbOrderBy.Append(",");
					sbOrderBy.Append(varKeys[k].ToString().Trim());
				}

				DataRow[] arrTargetData = varTargetTable.Select("", sbOrderBy.ToString());

				string strStdKey = "";
				StringBuilder sbIndexKey = new StringBuilder();
				StringBuilder sbCmbineKey = new StringBuilder();

				int intTagDataCount = arrTargetData.Length;
				for (int i = 0; i < intTagDataCount; i++)
				{
					sbIndexKey.Remove(0, sbIndexKey.Length);
					for (int k = 0; k < varKeys.Count; k++)
					{
						if (sbIndexKey.Length != 0)
							sbIndexKey.Append("+");
						sbIndexKey.Append(arrTargetData[i][varKeys[k].ToString().Trim()].ToString().Trim());
					}
					if (strStdKey != sbIndexKey.ToString().Trim())
					{
						strStdKey = sbIndexKey.ToString().Trim();

						sbCmbineKey.Remove(0, sbCmbineKey.Length);
						for (int k = 0; k < varKeys.Count; k++)
						{
							if (sbCmbineKey.Length != 0)
								sbCmbineKey.Append(" and ");
							sbCmbineKey.Append(" (" + varKeys[k].ToString().Trim() + " = '" + arrTargetData[i][varKeys[k].ToString().Trim()].ToString().Trim() + "') ");
						}

						DataRow[] arrCombine = varTargetTable.Select(sbCmbineKey.ToString(), sbOrderBy.ToString());
						for (int k = 0; k < arrCombine.Length; k++)
						{
							if (k == 0)
							{
								dtReturn.Rows.Add(arrCombine[k].ItemArray);
							}
							#region "聚合函式"
							IEnumerator ieCmpField = varCmpFields.Keys.GetEnumerator();

							while (ieCmpField.MoveNext())
							{
								switch ((CmpAction)varCmpFields[ieCmpField.Current.ToString()])
								{
									case CmpAction.Sum:
										if (k != 0)
											dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = Convert.ToString(Convert.ToInt64(dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim()) + Convert.ToInt64(arrCombine[k][ieCmpField.Current.ToString().Trim()].ToString().Trim()));
										break;
									case CmpAction.StringCombine:
										if (k != 0)
											dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim() + arrCombine[k][ieCmpField.Current.ToString().Trim()].ToString().Trim();
										break;
									case CmpAction.EmptyString:
										dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "";
										break;
									case CmpAction.ZeroInt:
										dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "0";
										break;
									case CmpAction.OneInt:
										dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "1";
										break;
									case CmpAction.Count:
										if (k != 0)
											dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = Convert.ToString(Convert.ToInt64(dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim()) + 1);
										else
											dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "1";
										break;
								}
							}
							#endregion
						}
						dtReturn.AcceptChanges();
					}
				}

				#endregion


			}
			catch(System.Exception ex)
			{
				//this.objSQLAccess.ErrorMessage = ex.Message + "<- UpdateUserPassword "  ;
				//throw new System.Exception(ex.Message +"<- UpdateUserPassword ");

				throw new Exception(ex.Message + "<-CombineTable()");
			}
			return dtReturn;
		}



		#endregion


	}
}
