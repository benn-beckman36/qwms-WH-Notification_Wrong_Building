using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_InitialTimeOf46PO : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strType = "";
        private DataTable dtData = new DataTable();
        private PlantData objPlantData;
        private Transfer objTransfer;

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
                return strLocat;
            }
            set
            {
                strLocat = value;
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
        #endregion

        #region 构造函数
        public Manage_InitialTimeOf46PO(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                 objTransfer = new Transfer(UserData);
                StorageIn objStorageIn = new StorageIn(UserData, Progid);
                objPlantData = new PlantData(UserData);
                Authority objAuthority = new Authority(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出status的资料
                    ShowStatusData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            txtmblnr.Text="";
            this.dgvData.Columns.Clear();
        }

        private void rbPalletID_CheckedChanged(object sender, EventArgs e)
        {
            strType = "PalletID";
            stsWarning.Text = "";
            txtmblnr.Text = "";
        }

        private void rbTrOut_CheckedChanged(object sender, EventArgs e)
        {
            strType = "TrOut";
            stsWarning.Text = "";
            txtmblnr.Text = "";
        }

        private void rbTrIn_CheckedChanged(object sender, EventArgs e)
        {
            strType = "TrIn";
            stsWarning.Text = "";
            txtmblnr.Text = "";
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.dgvData.Columns.Clear();
            string mblnr = "";
            mblnr = txtmblnr.Text.Trim();
            if (mblnr == "")
            {
                stsWarning.Text = "请输入单据号或者PalletID！";
                return;
            }
            if(strType=="")
            {
                stsWarning.Text = "请选择输入的类型";
                return;
            }

            dtData = objTransfer.returnInitialTime(strType, mblnr);
            if (dtData.Rows.Count > 0)
            {
                ShowDataGrid();
            }
            else
            {
                MessageBox.Show("NO DATA!!");
            }
        }

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                //      A.WERKS,A.LGORT,A.MBLNR,A.ZEILE,A.MATNR,A.MENGE,A.REFID,A.CRDAT
                //   WERKS,LGORT,MBLNR,ZEILE,MATNR,MENGE,REFID,CRDAT,调出扣账单号，调入扣账单号
                //  A.WERKS,A.LGORT,A.MBLNR,A.ZEILE,A.MATNR,A.MENGE,A.REFID,A.CRDAT, TrIn, TrOut
                this.dgvData.AutoGenerateColumns = false;
                this.dgvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "原始厂区";
                dgvcWerks.Width = 100;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "原始仓别";
                dgvcLgort.Width = 100;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcPalletID = new DataGridViewTextBoxColumn();
                dgvcPalletID.DataPropertyName = "PalletID";
                dgvcPalletID.HeaderText = "PalletID";
                dgvcPalletID.Name = "Mblnr";
                dgvcPalletID.Width = 200;
                dgvcPalletID.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPalletID);

                //DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                //dgvcZeile.DataPropertyName = "ZEILE";
                //dgvcZeile.HeaderText = "ZEILE";
                //dgvcZeile.Width = 90;
                //dgvcZeile.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "MATNR";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "CHARG";
                dgvcCharg.Width = 100;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "MENGE";
                dgvcMenge.Width = 50;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);


                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "CRDAT";
                dgvcCrdat.Width = 110;
                dgvcCrdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdat);


                DataGridViewTextBoxColumn dgvcTrOut = new DataGridViewTextBoxColumn();
                dgvcTrOut.DataPropertyName = "TrOut";
                dgvcTrOut.HeaderText = "调出扣账单号";
                dgvcTrOut.Width = 110;
                dgvcTrOut.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrOut);


                DataGridViewTextBoxColumn dgvcTrIn = new DataGridViewTextBoxColumn();
                dgvcTrIn.DataPropertyName = "TrIn";
                dgvcTrIn.HeaderText = "调入扣账单号";
                dgvcTrIn.Width = 110;
                dgvcTrIn.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrIn);


                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "调入厂区";
                dgvcDwerks.Width = 100;
                dgvcDwerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDwerks);


                DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                dgvcDlgort.DataPropertyName = "DLGORT";
                dgvcDlgort.HeaderText = "调入仓别";
                dgvcDlgort.Width = 100;
                dgvcDlgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDlgort);


                DataGridViewTextBoxColumn dgvcOloca = new DataGridViewTextBoxColumn();
                dgvcOloca.DataPropertyName = "OLOCA";
                dgvcOloca.HeaderText = "调入储位";
                dgvcOloca.Width = 100;
                dgvcOloca.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOloca);


                DataGridViewTextBoxColumn dgvcEmenge = new DataGridViewTextBoxColumn();
                dgvcEmenge.DataPropertyName = "EMENGE";
                dgvcEmenge.HeaderText = "调入数量";
                dgvcEmenge.Width = 100;
                dgvcEmenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEmenge);



                dgvData.DataSource = dtData;
              //  lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion





    }
}
