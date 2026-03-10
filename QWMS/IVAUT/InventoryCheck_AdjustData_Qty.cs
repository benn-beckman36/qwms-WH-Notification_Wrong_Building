using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class InventoryCheck_AdjustData_Qty : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strCycno = "";
        private string strStats = "";
        private DataRow drRowFound;
        private object[] objFind = new object[4];
        private DataTable dtQtyData = new DataTable();
        private DataTable dtTemp = new DataTable();
        private Counting objCounting;
      //  private AccessConfig objConfig;

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

        public string Cycno
        {
            get
            {
                return strCycno;
            }
            set
            {
                strCycno = value;
            }
        }

        public string Stats
        {
            get
            {
                return strStats;
            }
            set
            {
                strStats = value;
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
        # endregion

        public InventoryCheck_AdjustData_Qty()
        {
            InitializeComponent();
        }

        public InventoryCheck_AdjustData_Qty(UserInfo varUserData, string strProgid, string strMatnr, string strInsmk, string strCharg, string strCycno, string strStats, DataTable dtQtyData)
        {
            InitializeComponent();
            UserData = varUserData;
            DataRow drRow;
            DataRow[] foundRow;
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Matnr = strMatnr;
            Insmk = strInsmk;
            Charg = strCharg;
            Cycno = strCycno;
            Stats = strStats;
            QtyData = dtQtyData;

            try
            {

                objCounting = new Counting(UserData, Progid);

                //检查权限
                if (!objCounting.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {

                    dtTemp.Rows.Clear();
                    DataColumn[] dcPrimaryKey = new DataColumn[4];
                    dcPrimaryKey[0] = QtyData.Columns["MATNR"];
                    dcPrimaryKey[1] = QtyData.Columns["INSMK"];
                    dcPrimaryKey[2] = QtyData.Columns["CHARG"];
                    dcPrimaryKey[3] = QtyData.Columns["CYCNO"];
                    QtyData.PrimaryKey = dcPrimaryKey;

                    if (Matnr != "" && Cycno != "" && QtyData.Rows.Count > 0)
                    {
                        dtTemp = QtyData.Clone();
                        foundRow = QtyData.Select("MATNR='" + Matnr + "' and INSMK='" + Insmk + "' and CHARG='" + Charg + "' and CYCNO='" + Cycno + "'");

                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            drRow = dtTemp.NewRow();
                            drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[0]["COMCD"].ToString();
                            drRow["CYCNO"] = foundRow[0]["CYCNO"].ToString();
                            drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
                            drRow["LGORT"] = foundRow[0]["LGORT"].ToString();
                            drRow["LOCAT"] = foundRow[0]["LOCAT"].ToString();
                            drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
                            drRow["INSMK"] = foundRow[0]["INSMK"].ToString();
                            drRow["CHARG"] = foundRow[0]["CHARG"].ToString();
                            drRow["MENGE"] = foundRow[0]["MENGE"].ToString();
                            drRow["CKQTY"] = foundRow[0]["CKQTY"].ToString();
                            drRow["CKDAT"] = foundRow[0]["CKDAT"].ToString();
                            drRow["STATS"] = foundRow[0]["STATS"].ToString();
                            dtTemp.Rows.Add(drRow);
                        }
                    }

                    this.txtMenge.Enabled = false;
                    this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                    this.txtCkqty.Text = dtTemp.Rows[0]["CKQTY"].ToString();
                    this.txtCkqty.Focus();
                    this.txtCkqty.SelectAll();

                    objFind[0] = Matnr;
                    objFind[1] = Insmk;
                    objFind[2] = Charg;
                    objFind[3] = Cycno;
                    drRowFound = QtyData.Rows.Find(objFind);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //检查是否为数字
                if (!CheckIsNumber(this.txtCkqty.Text.Trim()))
                {
                    MessageBox.Show("Counting Qty should be numeric!!");
                    this.txtCkqty.Focus();
                    this.txtCkqty.SelectAll();
                    return;
                }
                if (Convert.ToInt64(this.txtCkqty.Text.Trim()) < 0)
                {
                    MessageBox.Show("Counting Qty should be greater than 0!!");
                    this.txtCkqty.Focus();
                    this.txtCkqty.SelectAll();
                    return;
                }

                if (txtMenge.Text.Trim() == txtCkqty.Text.Trim())
                {
                    strStats = "2";
                }
                else
                {
                    strStats = "2";
                }

                drRowFound["CKQTY"] = this.txtCkqty.Text.Trim();
                drRowFound["STATS"] = strStats;
                drRowFound.AcceptChanges();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
