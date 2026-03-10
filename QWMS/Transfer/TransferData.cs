using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;

namespace QWMS
{
    public partial class TransferData : Form
    {
        #region 初始设定
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strInsmk = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strCharg = "";
        //private string strDouble = "";
        private ArrayList aryMblnr;
        private ArrayList aryMatnr;
        private DataTable dtData = new DataTable();//库存数据
        private DataTable dtTransData = new DataTable();//一次性调拨数据
        private StorageOut objStorageOut;
        private Authority objAuthority;
        private PlantData objPlantData;
        #endregion

        #region 变量
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
        public ArrayList Matnrs
        {
            get
            {
                return aryMatnr;
            }
            set
            {
                aryMatnr = value;
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
        public DataTable TransData
        {
            get
            {
                return dtTransData;
            }
            set
            {
                dtTransData = value;
            }
        }
        #endregion

        public TransferData(UserInfo varUserData, string varWerks, string varLgort,string varInsmk, string varLocat, string varMatnr, string varCharg, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Werks = varWerks;
            Lgort = varLgort;
            Insmk = varInsmk;
            Locat = varLocat;
            Matnr = varMatnr;
            Charg = varCharg;
            //Double = varDouble;
            Progid = strProgid;

            try
            {
                objStorageOut = new StorageOut(UserData, Progid);
                if (!objStorageOut.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    QueryTransData();
                }
                if (dtData.Rows.Count == 0)
                {
                    dtTransData = new DataTable();
                    dtTransData.Columns.Add("WERKS");
                    dtTransData.Columns.Add("LGORT");
                    dtTransData.Columns.Add("LOCAT");
                    dtTransData.Columns.Add("MATNR");
                    dtTransData.Columns.Add("INSMK");
                    dtTransData.Columns.Add("CHARG");
                    dtTransData.Columns.Add("MENGE");
                    dtTransData.Columns.Add("ALQTY");
                    dtTransData.Columns.Add("LIFNR");

                    TransData = dtTransData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void QueryTransData()
        {
            try
            {
                Transfer objTransfer = new Transfer(UserData, Werks, Lgort);
                dtData = objTransfer.GetStock(Locat, Matnr,Insmk, Charg);
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSapData()");
            }

        }

        private void ShowDataGrid()
        {
            try
            {
                this.dgvTransData.AutoGenerateColumns = false;
                this.dgvTransData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                dgvSelect.DataPropertyName = "Select";
                dgvSelect.HeaderText = "Select";
                dgvSelect.Name = "Select";
                dgvSelect.Width = 50;
                this.dgvTransData.Columns.Add(dgvSelect);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Werks";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                this.dgvTransData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Lgort";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                this.dgvTransData.Columns.Add(dgvcLgort);


                //DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                //dgvcLocat.DataPropertyName = "LOCAT";
                //dgvcLocat.HeaderText = "Locat";
                //dgvcLocat.Name = "LOCAT";
                //dgvcLocat.ReadOnly = true;
                //dgvcLocat.Width = 50;
                //this.dgvTransData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Insmk";
                dgvcInsmk.Name = "INSMK";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                this.dgvTransData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                this.dgvTransData.Columns.Add(dgvcMatnr);


                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 50;
                this.dgvTransData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Menge";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 50;
                this.dgvTransData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Alqty";
                dgvcAlqty.Name = "ALQTY";
                dgvcAlqty.Width = 50;
                this.dgvTransData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Lifnr";
                dgvcLifnr.Name = "LIFNR";
                dgvcLifnr.Width = 80;
                this.dgvTransData.Columns.Add(dgvcLifnr);

                dgvTransData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
                dgvTransData.ClearSelection();
                dgvTransData.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dtTransData.Columns.Clear();
            dtTransData.Columns.Add("WERKS");
            dtTransData.Columns.Add("LGORT"); 
            //dtTransData.Columns.Add("LOCAT");
            dtTransData.Columns.Add("MATNR");
            dtTransData.Columns.Add("INSMK");
            dtTransData.Columns.Add("CHARG");
            dtTransData.Columns.Add("MENGE");
            dtTransData.Columns.Add("ALQTY");
            dtTransData.Columns.Add("LIFNR");

            for (int i = 0; i < dgvTransData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvTransData.Rows[i].Cells["Select"].Value))
                {
                    if (dgvTransData.Rows[i].Cells["ALQTY"].Value.ToString() == "" || dgvTransData.Rows[i].Cells["ALQTY"].Value.ToString() == "0")
                    {
                        stsWarning.Text = "调拨数量不能为空或0";
                        return;
                    }
                    else if (Convert.ToInt32(dgvTransData.Rows[i].Cells["MENGE"].Value.ToString()) < Convert.ToInt32(dgvTransData.Rows[i].Cells["ALQTY"].Value.ToString()))
                    {
                        stsWarning.Text = "调拨数量不能超过库存数量";
                        return;
                    }
                    else
                    {
                        DataRow drTrans = dtTransData.NewRow();
                        drTrans["WERKS"] = dgvTransData.Rows[i].Cells["WERKS"].Value.ToString();
                        drTrans["LGORT"] = dgvTransData.Rows[i].Cells["LGORT"].Value.ToString();
                        //drTrans["LOCAT"] = dgvTransData.Rows[i].Cells["LOCAT"].Value.ToString();
                        drTrans["MATNR"] = dgvTransData.Rows[i].Cells["MATNR"].Value.ToString();
                        drTrans["INSMK"] = dgvTransData.Rows[i].Cells["INSMK"].Value.ToString();
                        drTrans["CHARG"] = dgvTransData.Rows[i].Cells["CHARG"].Value.ToString();
                        drTrans["MENGE"] = dgvTransData.Rows[i].Cells["MENGE"].Value.ToString();
                        drTrans["ALQTY"] = dgvTransData.Rows[i].Cells["ALQTY"].Value.ToString();
                        drTrans["LIFNR"] = dgvTransData.Rows[i].Cells["LIFNR"].Value.ToString();
                        dtTransData.Rows.Add(drTrans);
                    }
                }
            }

            TransData = dtTransData;

            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
