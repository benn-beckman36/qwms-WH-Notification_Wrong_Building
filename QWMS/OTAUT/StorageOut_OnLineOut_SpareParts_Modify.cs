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
    public partial class StorageOut_OnLineOut_SpareParts_Modify : Form
    {
        #region 宣告變數

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strEbeln= "";
        private string strKdmat = "";
        private DataRow drRowFound;
        private object[] objFind = new object[5];
        private DataTable dtQtyData = new DataTable();
        private DataTable dtTemp = new DataTable();
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

        public string Ebeln
        {
            get
            {
                return strEbeln;
            }
            set
            {
                strEbeln = value;
            }
        }

        public string Kdmat
        {
            get
            {
                return strKdmat;
            }
            set
            {
                strKdmat = value;
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

        public StorageOut_OnLineOut_SpareParts_Modify()
        {
            InitializeComponent();
        }

        public StorageOut_OnLineOut_SpareParts_Modify(UserInfo varUserData, string strWerks, string strLgort, string strProgid, string strMatnr, string strInsmk, string strCharg, string strEbeln, string strKdmat, string strMblnr, DataTable dtQtyData)
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
			Matnr = strMatnr;
			Insmk = strInsmk;
			Charg = strCharg;
            Ebeln = strEbeln;
            Kdmat = strKdmat;
            Mblnr = strMblnr;
			QtyData = dtQtyData;

			try
			{
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                //檢查權限
				if(!objStorageOut.CheckAuthority())
				{
					MessageBox.Show("You don't have right to use this program!!");
					this.Close();
				}
				else
				{
					
					dtTemp.Rows.Clear();
					DataColumn[] dcPrimaryKey = new DataColumn[5];
					dcPrimaryKey[0] = QtyData.Columns["MATNR"];
                    dcPrimaryKey[1] = QtyData.Columns["CHARG"];
                    dcPrimaryKey[2] = QtyData.Columns["EBELN"];
					dcPrimaryKey[3] = QtyData.Columns["KDMAT"];
                    dcPrimaryKey[4] = QtyData.Columns["MBLNR"];

					QtyData.PrimaryKey = dcPrimaryKey;

                    if (Matnr != "" && QtyData.Rows.Count > 0)
					{
						dtTemp = QtyData.Clone();
                        foundRow = QtyData.Select("MATNR='" + Matnr + "' and CHARG='" + Charg + "' and EBELN='" + Ebeln + "' and KDMAT='" + Kdmat + "' and MBLNR='" + Mblnr + "'");
						
						for(int i=0;i<foundRow.Length;i++)
						{
							drRow = dtTemp.NewRow();
							drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[0]["COMCD"].ToString();
							drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
							drRow["LGORT"] = foundRow[0]["LGORT"].ToString();
							drRow["LOCAT"] = foundRow[0]["LOCAT"].ToString();
							drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
							drRow["INSMK"] = foundRow[0]["INSMK"].ToString();
							drRow["CHARG"] = foundRow[0]["CHARG"].ToString();
							drRow["MENGE"] = foundRow[0]["MENGE"].ToString();
							drRow["EBELN"] = foundRow[0]["EBELN"].ToString();
                            drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
                            drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
                            drRow["RMAK1"] = foundRow[0]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[0]["INDAT"].ToString();
                            drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
                            drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
							dtTemp.Rows.Add(drRow);
						}
					}

					this.txtMenge.Enabled = false;
                    this.txtMatnr.Enabled = false;
					this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
					this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
					this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
                    this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
					this.txtEbeln.Focus();

                    objFind[0] = Matnr;
					objFind[1] = Charg;
					objFind[2] = Ebeln;
					objFind[3] = Kdmat;
					objFind[4] = Mblnr;
					drRowFound = QtyData.Rows.Find(objFind);	
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                drRowFound["EBELN"] = this.txtEbeln.Text.Trim();
                drRowFound["KDMAT"] = this.txtKdmat.Text.Trim();
                drRowFound.AcceptChanges();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
