using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Replenishment_ChangeReplenishStatus_Edit : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private string strMenge = "";
        private DataTable dtQtyData = new DataTable();
        private DataRow drRowFound;
        private object[] objFind = new object[5];
        private DataTable dtTemp = new DataTable();
 
        private Replenishment objReplenishment;
 

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

        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
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

        public string Menge
        {
            get
            {
                return strMenge;
            }
            set
            {
                strMenge = value;
            }
        }

        public DataTable QtyData
        {
            get
            {
                return dtQtyData;
            }
            set
            {
                dtQtyData = value;
            }
        }
        #endregion

        #region 构造函数
        public Replenishment_ChangeReplenishStatus_Edit()
        {
            InitializeComponent();
        }
        public Replenishment_ChangeReplenishStatus_Edit(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strMblnr, string strMatnr, string strInsmk, string strCharg, string strMenge, DataTable dtDetailData)
        {
            InitializeComponent();
            DataRow drRow;
            DataRow[] foundRow;
            UserData = varUserData;
            Mandt = UserData.Client;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Mblnr = strMblnr;
            Matnr = strMatnr;
            Insmk = strInsmk;
            Charg = strCharg;
            Menge = strMenge;
            QtyData = dtDetailData;

            try
            {
                objReplenishment = new Replenishment(UserData, Progid);

                // 检查权限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {

                    dtTemp.Rows.Clear();
                    DataColumn[] dcPrimaryKey = new DataColumn[5];
                    dcPrimaryKey[0] = QtyData.Columns["LOCAT"];
                    dcPrimaryKey[1] = QtyData.Columns["MATNR"];
                    dcPrimaryKey[2] = QtyData.Columns["INSMK"];
                    dcPrimaryKey[3] = QtyData.Columns["CHARG"];
                    dcPrimaryKey[4] = QtyData.Columns["MBLNR"];
                    QtyData.PrimaryKey = dcPrimaryKey;

                    if (Matnr != "" && Insmk != "" && QtyData.Rows.Count > 0)
                    {
                        dtTemp = QtyData.Clone();
                        foundRow = QtyData.Select("LOCAT='" + Locat + "' and MATNR='" + Matnr + "' and INSMK='" + Insmk + "' and CHARG='" + Charg + "' and MBLNR='" + Mblnr + "'");

                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            dtTemp.Rows.Add(foundRow[0].ItemArray);
                        }
                    }
                    this.txtWerks.Text = dtTemp.Rows[0]["WERKS"].ToString();
                    this.txtLgort.Text = dtTemp.Rows[0]["LGORT"].ToString();
                    this.txtLocat.Text = dtTemp.Rows[0]["LOCAT"].ToString();
                    this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                    this.txtInsmk.Text = dtTemp.Rows[0]["INSMK"].ToString();
                    this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
                    this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                    this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();

                    objFind[0] = Locat;
                    objFind[1] = Matnr;
                    objFind[2] = Insmk;
                    objFind[3] = Charg;
                    objFind[4] = Mblnr;
                    drRowFound = QtyData.Rows.Find(objFind);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // Alqty 必需为数值型
                try
                {
                    Int64 intA = Int64.Parse(this.txtAlqty.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Store out Qty should be numeric!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                // 比较 Alqty与 Qty
                if (Convert.ToInt64(this.txtAlqty.Text.Trim()) > Convert.ToInt64(this.txtMenge.Text.Trim()))
                {
                    MessageBox.Show("Store out Qty can't be greater than storage Qty!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                drRowFound["ALQTY"] = this.txtAlqty.Text.Trim();
                drRowFound.AcceptChanges();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region Return
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
