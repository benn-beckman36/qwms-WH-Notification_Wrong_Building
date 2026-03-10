using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{   
    /// <summary>
    /// StorageIn_Product_OffLineIn 的摘要描述。
    /// </summary>
    public class Pad_IssueProgress : System.Windows.Forms.Form
    {
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.GroupBox gbHeader;
        private TextBox txtDocuNo;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;

        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        //private string strLocat = "";
        private string strMatnr = "";
        private string strType = "";
        private string strMrgid = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        //private string strSerno = "";
        private string strComcd = "";
        UserInfo UserData = new UserInfo();
        private bool bolDuplicate = false;
        //private int intFormIndex = 0;
        private DataTable dtData = new DataTable();
        //private MixedMaterial objMixedMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel4;
        private Label lblData;
        private DataGridView dgvData;
        private Label label2;
        private TextBox txtCarNo;
        private TextBox txtUserID;

        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
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
                return this.txtDocuNo.Text.Trim();
            }
            set
            {
                this.txtDocuNo.Text = value;
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

        public Pad_IssueProgress(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                Admin objStorageIn = new Admin(UserData, Progid);
                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
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

        #region Windows Form Designer generated code
        /// <summary>
        /// 此為設計工具支援所必需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocuNo = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.txtCarNo);
            this.gbHeader.Controls.Add(this.txtUserID);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.txtDocuNo);
            this.gbHeader.Controls.Add(this.btnQuery);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(3, 7);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(746, 105);
            this.gbHeader.TabIndex = 32;
            this.gbHeader.TabStop = false;
            // 
            // txtCarNo
            // 
            this.txtCarNo.Location = new System.Drawing.Point(396, 12);
            this.txtCarNo.MaxLength = 20;
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(100, 22);
            this.txtCarNo.TabIndex = 10;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(160, 12);
            this.txtUserID.MaxLength = 10;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 22);
            this.txtUserID.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Car No.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDocuNo
            // 
            this.txtDocuNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocuNo.Location = new System.Drawing.Point(158, 67);
            this.txtDocuNo.MaxLength = 50;
            this.txtDocuNo.Name = "txtDocuNo";
            this.txtDocuNo.Size = new System.Drawing.Size(128, 22);
            this.txtDocuNo.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(521, 62);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(71, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Docu No.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(71, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "User ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.stbStatus.Size = new System.Drawing.Size(761, 20);
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(761, 134);
            this.panel1.TabIndex = 33;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(761, 134);
            this.panel4.TabIndex = 34;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 134);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(761, 319);
            this.panel2.TabIndex = 34;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(29, 3);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 27;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(52, 22);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(655, 246);
            this.dgvData.TabIndex = 26;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(469, 274);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 33);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(199, 274);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 33);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Pad_IssueProgress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(761, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "Pad_IssueProgress";
            this.Text = "Issue Progress";
            this.Load += new System.EventHandler(this.Pad_IssueProgress_Load);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }        

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "ItemNo";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "StorageOut Quantity";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);

                //原库存
                DataGridViewTextBoxColumn dgvcSTQTY = new DataGridViewTextBoxColumn();
                dgvcSTQTY.DataPropertyName = "STQTY";
                dgvcSTQTY.HeaderText = "Pre-Quantity";
                dgvcSTQTY.Width = 90;
                dgvcSTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSTQTY);

                //剩余库存
                DataGridViewTextBoxColumn dgvcBLQTY = new DataGridViewTextBoxColumn();
                dgvcBLQTY.DataPropertyName = "BLQTY";
                dgvcBLQTY.HeaderText = "Balance Quantity";
                dgvcBLQTY.Width = 90;
                dgvcBLQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBLQTY);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcADDQTY = new DataGridViewTextBoxColumn();
                dgvcADDQTY.DataPropertyName = "ADDQTY";
                dgvcADDQTY.HeaderText = "Add Quantity";
                dgvcADDQTY.Width = 90;
                dgvcADDQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcADDQTY);

                DataGridViewTextBoxColumn dgvcFLAGE = new DataGridViewTextBoxColumn();
                dgvcFLAGE.DataPropertyName = "FLAGE";
                dgvcFLAGE.HeaderText = "Flag";
                dgvcFLAGE.Width = 90;
                dgvcFLAGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcFLAGE);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "Create Date";
                dgvcCRDAT.Width = 90;
                dgvcCRDAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRDAT);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strUserID = txtUserID.Text.ToString().Trim();
            string strCarNo = txtCarNo.Text.ToString().Trim();
            string strDocuNo=txtDocuNo.Text.ToString().Trim();
            Authority objAuth = new Authority(UserData);
            dtData = objAuth.QueryIssueProgress(strUserID, strCarNo, strDocuNo);
            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtCarNo.Text = "";
            txtDocuNo.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pad_IssueProgress_Load(object sender, EventArgs e)
        {
            ShowStatusData();
        }
    }
}
