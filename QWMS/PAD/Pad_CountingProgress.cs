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
    /// Pad_CountingProgress 的摘要描述。
    /// </summary>
    public partial class Pad_CountingProgress : System.Windows.Forms.Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        //private string strLocat = "";
        //private string strMatnr = "";
        private string strType = "";
        private string strMrgid = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        //private string strSerno = "";
        private string strComcd = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtCounting = new DataTable();

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

        public  Pad_CountingProgress()
        {
            InitializeComponent();
        }
        public Pad_CountingProgress(UserInfo varUserData, string strProgid)
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
                else
                {
                    //ShowDdlWerks();
                    //ShowDdlLgort();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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

                DataGridViewTextBoxColumn dgvcSTQTY = new DataGridViewTextBoxColumn();
                dgvcSTQTY.DataPropertyName = "STQTY";
                dgvcSTQTY.HeaderText = "Pre-Quantity";
                dgvcSTQTY.Width = 90;
                dgvcSTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSTQTY);

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

                dgvData.DataSource = dtCounting;
                lblData.Text = dtCounting.Rows.Count.ToString()+" records";
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strUserID = txtUserID.Text.ToString().Trim();
            string strMatnr = txtMatnr.Text.ToString().Trim();
            string strDateFrom = DateFrom.Value.ToString("yyyyMMdd");
            string strDateTo = DateTo.Value.ToString("yyyyMMdd");
            int intDateFrom = Convert.ToInt32(strDateFrom);
            int intDateTo = Convert.ToInt32(strDateTo);
            if (intDateFrom > intDateTo)
            {
                MessageBox.Show("Time From To区间日期顺序输入错误!!");
            }
            Authority objAuth = new Authority(UserData);
            dtCounting = objAuth.QueryCountingPrpgress(strUserID,strMatnr,strDateFrom,strDateTo);
            ShowDataGrid();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUserID.Text = "";
            txtMatnr.Text = "";
            //DateFrom.Value = DateTime.Now();
            //DateTo.Value = DateTime.Now();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pad_CountingProgress_Load(object sender, EventArgs e)
        {
            ShowStatusData();
        }
    }
}
