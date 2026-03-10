using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageOut_OnLineOut_TwoPhaseConfirm_Modify : Form
    {
        #region 變數宣告
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strZeile = "";
        private string strBarcode = "";
        private int intMenge = 0;
        private DataRow drRowFound;
        private object[] objFind = new object[7];
        private DataTable dtQtyData = new DataTable();
        private DataTable dtTemp = new DataTable();
        //private Admin objAdmin;
        UserInfo UserData = new UserInfo();
        #endregion

        #region DataMember
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

        public string Zeile
        {
            get
            {
                return strZeile;
            }
            set
            {
                strZeile = value;
            }
        }

        public string Barcode
        {
            get
            {
                return strBarcode;
            }
            set
            {
                strBarcode = value;
            }
        }

        public int Menge
        {
            get
            {
                return intMenge;
            }
            set
            {
                intMenge = value;
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

        public StorageOut_OnLineOut_TwoPhaseConfirm_Modify()
        {
            InitializeComponent();
        }

        public StorageOut_OnLineOut_TwoPhaseConfirm_Modify(UserInfo varUserData, string strWerks, string strLgort, string strProgid, string strLocat, string strMatnr, string strInsmk, string strCharg, string strMblnr, string strZeile, string strBarcode, DataTable dtQtyData)
        {
            InitializeComponent();
            DataRow drRow;
            DataRow[] foundRow;
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Werks = strWerks;
            Lgort = strLgort;
            Progid = strProgid;
            Locat = strLocat;
            Matnr = strMatnr;
            Insmk = strInsmk;
            Charg = strCharg;
            Mblnr = strMblnr;
            Zeile = strZeile;
            Barcode = strBarcode;
            QtyData = dtQtyData;

            try
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);

                //檢查權限
                if (!objStorageOut.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {

                    dtTemp.Rows.Clear();
                    DataColumn[] dcPrimaryKey = new DataColumn[7];
                    dcPrimaryKey[0] = QtyData.Columns["MATNR"];
                    dcPrimaryKey[1] = QtyData.Columns["INSMK"];
                    dcPrimaryKey[2] = QtyData.Columns["CHARG"];
                    dcPrimaryKey[3] = QtyData.Columns["MBLNR"];
                    dcPrimaryKey[4] = QtyData.Columns["ZEILE"];
                    dcPrimaryKey[5] = QtyData.Columns["LOCAT"];
                    dcPrimaryKey[6] = QtyData.Columns["BARCODE"];
                    QtyData.PrimaryKey = dcPrimaryKey;

                    if (Matnr != "" && Insmk != "" && Mblnr != "" && Zeile != "" && Locat != "" && Barcode != "" && QtyData.Rows.Count > 0)
                    {
                        dtTemp = QtyData.Clone();
                        foundRow = QtyData.Select("MATNR='" + Matnr + "' and INSMK='" + Insmk + "' and CHARG='" + Charg + "' and MBLNR='" + Mblnr + "' and ZEILE='" + Zeile + "' and LOCAT='" + Locat + "' and BARCODE='" + Barcode + "'");

                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            drRow = dtTemp.NewRow();
                            drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[0]["COMCD"].ToString();
                            drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
                            drRow["LGORT"] = foundRow[0]["LGORT"].ToString();
                            drRow["LOCAT"] = foundRow[0]["LOCAT"].ToString();
                            drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
                            drRow["ZEILE"] = foundRow[0]["ZEILE"].ToString();
                            drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
                            drRow["INSMK"] = foundRow[0]["INSMK"].ToString();
                            drRow["CHARG"] = foundRow[0]["CHARG"].ToString();
                            drRow["BKQTY"] = foundRow[0]["BKQTY"].ToString();
                            drRow["ALQTY"] = foundRow[0]["ALQTY"].ToString();
                            drRow["BLACE"] = foundRow[0]["BLACE"].ToString();
                            drRow["EBELN"] = foundRow[0]["EBELN"].ToString();
                            drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
                            drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
                            drRow["BARCODE"] = foundRow[0]["BARCODE"].ToString();
                            dtTemp.Rows.Add(drRow);
                        }
                    }

                    this.txtLocat.Enabled = false;
                    this.txtMblnr.Enabled = false;
                    this.txtMatnr.Enabled = false;
                    this.txtZeile.Enabled = false;
                    this.txtMenge.Enabled = false;
                    this.txtBarcode.Enabled = false;
                    this.txtLocat.Text = dtTemp.Rows[0]["LOCAT"].ToString();
                    this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
                    this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                    this.txtZeile.Text = dtTemp.Rows[0]["ZEILE"].ToString();
                    this.txtBarcode.Text = dtTemp.Rows[0]["BARCODE"].ToString();
                    this.txtMenge.Text = dtTemp.Rows[0]["BKQTY"].ToString();
                    this.txtMdQty.Text = dtTemp.Rows[0]["ALQTY"].ToString();

                    if (txtLocat.Text.Trim() != "")
                    {
                        this.txtLocat.ReadOnly = true;
                    }
                    if (txtMblnr.Text.Trim() != "")
                    {
                        this.txtMblnr.ReadOnly = true;
                    }
                    if (txtZeile.Text.Trim() != "")
                    {
                        this.txtZeile.ReadOnly = true;
                    }
                    if (txtMenge.Text.Trim() != "")
                    {
                        this.txtMenge.ReadOnly = true;
                    }
                    if (txtMatnr.Text.Trim() != "")
                    {
                        this.txtMatnr.ReadOnly = true;
                    }

                    this.txtMdQty.Focus();
                    this.txtMdQty.SelectAll();

                    objFind[0] = Matnr;
                    objFind[1] = Insmk;
                    objFind[2] = Charg;
                    objFind[3] = Mblnr;
                    objFind[4] = Zeile;
                    objFind[5] = Locat;
                    objFind[6] = Barcode;
                    drRowFound = QtyData.Rows.Find(objFind);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //數量不可空白或0
                if (!CheckIsNumber(this.txtMdQty.Text.Trim()))
                {
                    MessageBox.Show("Modify Qty should be numeric!!");
                    this.txtMdQty.Focus();
                    this.txtMdQty.SelectAll();
                    return;
                }
                if (Convert.ToInt64(this.txtMdQty.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Modify Qty should be greater than 0!!");
                    this.txtMdQty.Focus();
                    this.txtMdQty.SelectAll();
                    return;
                }
                //加扣數量不可大於單據數量
                if (Convert.ToInt64(this.txtMdQty.Text.Trim()) > Convert.ToInt64(this.txtMenge.Text.Trim()))
                {
                    MessageBox.Show("Modify Qty can't be greater than Document Qty!!");
                    this.txtMdQty.Focus();
                    this.txtMdQty.SelectAll();
                    return;
                }

                drRowFound["LOCAT"] = this.txtLocat.Text.Trim();
                drRowFound["MBLNR"] = this.txtMblnr.Text.Trim();
                drRowFound["ZEILE"] = this.txtZeile.Text.Trim();
                drRowFound["MATNR"] = this.txtMatnr.Text.Trim();
                drRowFound["BARCODE"] = this.txtBarcode.Text.Trim();
                drRowFound["BKQTY"] = this.txtMenge.Text.Trim();
                drRowFound["ALQTY"] = this.txtMdQty.Text.Trim();
                drRowFound["BLACE"] = int.Parse(this.txtMenge.Text.Trim()) - int.Parse(this.txtMdQty.Text.Trim());
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
    }
}


