using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageIn_OnLineIn_SpareParts_Add : Form
    {
        #region 變數宣告

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strIndat = "";
        private string strType = "";
        private string strMblnr = "";
        private string strEbeln = "";
        private string strKdmat = "";
        private string strIntype = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strSerNo = "";
        private bool bolDuplicate = false;
        private DataTable dtSapData = new DataTable();
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
        public string Indat
        {
            get
            {
                return strIndat;
            }
            set
            {
                strIndat = value;
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
                return txtMatnr.Text.Trim();
            }
            set
            {
                txtMatnr.Text = value;
            }
        }
        public string Ebeln
        {
            get
            {
                return txtEbeln.Text.Trim();
            }
            set
            {
                txtEbeln.Text = value;
            }
        }
        public string Kdmat
        {
            get
            {
                return txtKdmat.Text.Trim();
            }
            set
            {
                txtKdmat.Text = value;
            }
        }
        public DataTable SapData
        {
            get
            {
                return dtSapData;
            }
            set
            {
                dtSapData = value;
            }
        }
        public string Intype
        {
            get
            {
                return strIntype;
            }
            set
            {
                strIntype = value;
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
        public string SerNo
        {
            get
            {
                return strSerNo;
            }
            set
            {
                strSerNo = value;
            }
        }

        #endregion

        public StorageIn_OnLineIn_SpareParts_Add()
        {
            InitializeComponent();
        }

        #region 建構式
        public StorageIn_OnLineIn_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp, string strIndat, string strMblnr, string strMatnr, DataTable dtSapData, string strIntype, string strInsmk, string strCharg, bool bolDuplicate) :
            this(varUserData, strProgid, strWerks, strLgort, strLocat, strSttyp, strLotyp, strIndat, strMblnr, strMatnr, dtSapData, strIntype, strInsmk, strCharg, bolDuplicate, "") { }

        public StorageIn_OnLineIn_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp, string strIndat, string strMblnr, string strMatnr, DataTable dtSapData, string strIntype, string strInsmk, string strCharg, bool bolDuplicate, string strSerNo)
		{
            UserData = varUserData;
            
            InitializeComponent();
			DataRow drRow;
			DataRow[] foundRow;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
			Progid = strProgid;
			Werks = strWerks;
			Lgort = strLgort;
			Locat = strLocat;
			Indat = strIndat;
			Mblnr = strMblnr;
			txtMatnr.Text = strMatnr;
			SapData = dtSapData;
			Intype = strIntype;
            Insmk = "G";  //庫別預設都帶G庫
			Charg = strCharg;
			Type = "NEW";
			Sttyp = strSttyp;
			Lotyp = strLotyp;
			SerNo = strSerNo; 
			Duplicate = bolDuplicate;
			try
			{
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
 
				//檢查權限
				if(!objStorageIn.CheckAuthority())
				{
					MessageBox.Show("You don't have right to use this program!!");
					this.Close();
				}
				else
				{
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowLocat();
					dtTemp.Rows.Clear();
					this.txtInsmk.Text = Insmk;

					DataColumn[] dcPrimaryKey = new DataColumn[3];
					dcPrimaryKey[0] = SapData.Columns["MBLNR"];
					dcPrimaryKey[1] = SapData.Columns["MATNR"];
					dcPrimaryKey[2] = SapData.Columns["SERNO"];
					SapData.PrimaryKey = dcPrimaryKey;

					if(Mblnr != "" && Matnr != "" && SapData.Rows.Count > 0)
					{
						Type = "MODIFY";
						dtTemp.Columns.Add("MANDT", Type.GetType());
                        dtTemp.Columns.Add("COMCD", Type.GetType());
						dtTemp.Columns.Add("WERKS", Type.GetType());
						dtTemp.Columns.Add("LGORT", Type.GetType());
						dtTemp.Columns.Add("LOCAT", Type.GetType());
						dtTemp.Columns.Add("MATNR", Type.GetType());
						dtTemp.Columns.Add("INSMK", Type.GetType());
						dtTemp.Columns.Add("CHARG", Type.GetType());
						dtTemp.Columns.Add("MENGE", Type.GetType());
						dtTemp.Columns.Add("ALQTY", Type.GetType());
						dtTemp.Columns.Add("MBLNR", Type.GetType());
						dtTemp.Columns.Add("ZEILE", Type.GetType());
						dtTemp.Columns.Add("EBELN", Type.GetType());
						dtTemp.Columns.Add("LIFNR", Type.GetType());
						dtTemp.Columns.Add("RMANO", Type.GetType());  
						dtTemp.Columns.Add("OMBLNR", Type.GetType());
						dtTemp.Columns.Add("MRGID", Type.GetType());
						dtTemp.Columns.Add("KOSTL", Type.GetType());
						dtTemp.Columns.Add("ARBPL", Type.GetType());
						dtTemp.Columns.Add("TRNTP", Type.GetType());
						dtTemp.Columns.Add("RMAK1", Type.GetType());
						dtTemp.Columns.Add("INDAT", Type.GetType());
						dtTemp.Columns.Add("KDMAT", Type.GetType());
						dtTemp.Columns.Add("SERNO", Type.GetType());
                        dtTemp.Columns.Add("GRLOC", Type.GetType());
						foundRow = SapData.Select("MANDT='"+ Mandt +"' and COMCD='"+Comcd+"' and WERKS='"+ Werks +"' and LGORT='"+ Lgort +"' and MBLNR='"+ Mblnr +"' and MATNR='"+ Matnr +"' and SERNO ='" + SerNo  + "'");
						
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
							drRow["ALQTY"] = foundRow[0]["ALQTY"].ToString();
							drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
							drRow["ZEILE"] = foundRow[0]["ZEILE"].ToString();
							drRow["EBELN"] = foundRow[0]["EBELN"].ToString();
							drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
							drRow["RMANO"] = foundRow[0]["RMANO"].ToString();  
							drRow["OMBLNR"] = foundRow[0]["OMBLNR"].ToString();
							drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
							drRow["KOSTL"] = foundRow[0]["KOSTL"].ToString();
							drRow["ARBPL"] = foundRow[0]["ARBPL"].ToString();
							drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
							drRow["RMAK1"] = foundRow[0]["RMAK1"].ToString();
							drRow["INDAT"] = foundRow[0]["INDAT"].ToString();
							drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
							drRow["SERNO"] = foundRow[0]["SERNO"].ToString();
                            drRow["GRLOC"] = foundRow[0]["GRLOC"].ToString();
							dtTemp.Rows.Add(drRow);
						}
					}
					else
					{
						if(dtSapData.Rows.Count == 0)
						{
							dtSapData = new DataTable();
							dtSapData.Columns.Add("MANDT", Type.GetType());
                            dtSapData.Columns.Add("COMCD", Type.GetType());
							dtSapData.Columns.Add("WERKS", Type.GetType());
							dtSapData.Columns.Add("LGORT", Type.GetType());
							dtSapData.Columns.Add("LOCAT", Type.GetType());
							dtSapData.Columns.Add("MATNR", Type.GetType());
							dtSapData.Columns.Add("INSMK", Type.GetType());
							dtSapData.Columns.Add("CHARG", Type.GetType());
							dtSapData.Columns.Add("MENGE", Type.GetType());
							dtSapData.Columns.Add("ALQTY", Type.GetType());
							dtSapData.Columns.Add("MBLNR", Type.GetType());
							dtSapData.Columns.Add("ZEILE", Type.GetType());
							dtSapData.Columns.Add("EBELN", Type.GetType());
							dtSapData.Columns.Add("LIFNR", Type.GetType());
							dtSapData.Columns.Add("RMANO", Type.GetType());  
							dtSapData.Columns.Add("OMBLNR", Type.GetType());
							dtSapData.Columns.Add("MRGID", Type.GetType());
							dtSapData.Columns.Add("KOSTL", Type.GetType());
							dtSapData.Columns.Add("ARBPL", Type.GetType());
							dtSapData.Columns.Add("TRNTP", Type.GetType());
							dtSapData.Columns.Add("RMAK1", Type.GetType());
							dtSapData.Columns.Add("INDAT", Type.GetType());
							dtSapData.Columns.Add("KDMAT", Type.GetType());
							dtSapData.Columns.Add("SERNO", Type.GetType());
                            dtSapData.Columns.Add("GRLOC", Type.GetType());

							SapData = dtSapData;
						}
					}
					if(Type == "MODIFY")
					{
						this.btnQuery.Enabled = false;
						this.btnConfirm.Enabled = true;
						this.btnDelete.Enabled = true;
						this.txtMblnr.Enabled = false;
						this.txtMatnr.Enabled = false;
						this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
						this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
						this.txtInsmk.Text = dtTemp.Rows[0]["INSMK"].ToString();
						this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
						this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
						this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();
						this.txtRmak1.Text = dtTemp.Rows[0]["RMAK1"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
        }

        //for 手動入庫/自動入庫修改PO No./Customer P/N/數量使用  Smose Liao 20100726
        public StorageIn_OnLineIn_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp, string strIndat, string strMblnr, string strMatnr, string strEbeln, string strKdmat, DataTable dtSapData, string strIntype, string strInsmk, string strCharg, bool bolDuplicate)
        {
            UserData = varUserData;

            InitializeComponent();
            DataRow drRow;
            DataRow[] foundRow;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
            Indat = strIndat;
            Mblnr = strMblnr;
            txtMatnr.Text = strMatnr;
            txtEbeln.Text = strEbeln;
            txtKdmat.Text = strKdmat;
            SapData = dtSapData;
            Intype = strIntype;
            Insmk = "G";  //庫別預設都帶G庫
            Charg = strCharg;
            Type = "NEW";
            Sttyp = strSttyp;
            Lotyp = strLotyp;
            //SerNo = strSerNo;
            Duplicate = bolDuplicate;
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowLocat();
                    dtTemp.Rows.Clear();
                    this.txtInsmk.Text = Insmk;

                    DataColumn[] dcPrimaryKey = new DataColumn[3];
                    dcPrimaryKey[0] = SapData.Columns["MBLNR"];
                    dcPrimaryKey[1] = SapData.Columns["MATNR"];
                    dcPrimaryKey[2] = SapData.Columns["SERNO"];
                    SapData.PrimaryKey = dcPrimaryKey;

                    if (Mblnr != "" && Matnr != "" && SapData.Rows.Count > 0)
                    {
                        Type = "MODIFY";
                        dtTemp.Columns.Add("MANDT", Type.GetType());
                        dtTemp.Columns.Add("COMCD", Type.GetType());
                        dtTemp.Columns.Add("WERKS", Type.GetType());
                        dtTemp.Columns.Add("LGORT", Type.GetType());
                        dtTemp.Columns.Add("LOCAT", Type.GetType());
                        dtTemp.Columns.Add("MATNR", Type.GetType());
                        dtTemp.Columns.Add("INSMK", Type.GetType());
                        dtTemp.Columns.Add("CHARG", Type.GetType());
                        dtTemp.Columns.Add("MENGE", Type.GetType());
                        dtTemp.Columns.Add("ALQTY", Type.GetType());
                        dtTemp.Columns.Add("MBLNR", Type.GetType());
                        dtTemp.Columns.Add("ZEILE", Type.GetType());
                        dtTemp.Columns.Add("EBELN", Type.GetType());
                        dtTemp.Columns.Add("LIFNR", Type.GetType());
                        dtTemp.Columns.Add("RMANO", Type.GetType());
                        dtTemp.Columns.Add("OMBLNR", Type.GetType());
                        dtTemp.Columns.Add("MRGID", Type.GetType());
                        dtTemp.Columns.Add("KOSTL", Type.GetType());
                        dtTemp.Columns.Add("ARBPL", Type.GetType());
                        dtTemp.Columns.Add("TRNTP", Type.GetType());
                        dtTemp.Columns.Add("RMAK1", Type.GetType());
                        dtTemp.Columns.Add("INDAT", Type.GetType());
                        dtTemp.Columns.Add("KDMAT", Type.GetType());
                        dtTemp.Columns.Add("SERNO", Type.GetType());
                        dtTemp.Columns.Add("GRLOC", Type.GetType());
                        foundRow = SapData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and MBLNR='" + Mblnr + "' and MATNR='" + Matnr + "' and EBELN='" + Ebeln + "' and KDMAT='" + Kdmat + "'");

                        for (int i = 0; i < foundRow.Length; i++)
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
                            drRow["ALQTY"] = foundRow[0]["ALQTY"].ToString();
                            drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
                            drRow["ZEILE"] = foundRow[0]["ZEILE"].ToString();
                            drRow["EBELN"] = foundRow[0]["EBELN"].ToString();
                            drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
                            drRow["RMANO"] = foundRow[0]["RMANO"].ToString();
                            drRow["OMBLNR"] = foundRow[0]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
                            drRow["KOSTL"] = foundRow[0]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[0]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[0]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[0]["INDAT"].ToString();
                            drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
                            drRow["SERNO"] = foundRow[0]["SERNO"].ToString();
                            drRow["GRLOC"] = foundRow[0]["GRLOC"].ToString();
                            dtTemp.Rows.Add(drRow);
                        }
                    }
                    else
                    {
                        if (dtSapData.Rows.Count == 0)
                        {
                            dtSapData = new DataTable();
                            dtSapData.Columns.Add("MANDT", Type.GetType());
                            dtSapData.Columns.Add("COMCD", Type.GetType());
                            dtSapData.Columns.Add("WERKS", Type.GetType());
                            dtSapData.Columns.Add("LGORT", Type.GetType());
                            dtSapData.Columns.Add("LOCAT", Type.GetType());
                            dtSapData.Columns.Add("MATNR", Type.GetType());
                            dtSapData.Columns.Add("INSMK", Type.GetType());
                            dtSapData.Columns.Add("CHARG", Type.GetType());
                            dtSapData.Columns.Add("MENGE", Type.GetType());
                            dtSapData.Columns.Add("ALQTY", Type.GetType());
                            dtSapData.Columns.Add("MBLNR", Type.GetType());
                            dtSapData.Columns.Add("ZEILE", Type.GetType());
                            dtSapData.Columns.Add("EBELN", Type.GetType());
                            dtSapData.Columns.Add("LIFNR", Type.GetType());
                            dtSapData.Columns.Add("RMANO", Type.GetType());
                            dtSapData.Columns.Add("OMBLNR", Type.GetType());
                            dtSapData.Columns.Add("MRGID", Type.GetType());
                            dtSapData.Columns.Add("KOSTL", Type.GetType());
                            dtSapData.Columns.Add("ARBPL", Type.GetType());
                            dtSapData.Columns.Add("TRNTP", Type.GetType());
                            dtSapData.Columns.Add("RMAK1", Type.GetType());
                            dtSapData.Columns.Add("INDAT", Type.GetType());
                            dtSapData.Columns.Add("KDMAT", Type.GetType());
                            dtSapData.Columns.Add("SERNO", Type.GetType());
                            dtSapData.Columns.Add("GRLOC", Type.GetType());

                            SapData = dtSapData;
                        }
                    }
                    if (Type == "MODIFY")
                    {
                        this.btnQuery.Enabled = false;
                        this.btnConfirm.Enabled = true;
                        this.btnDelete.Enabled = true;
                        this.txtMblnr.Enabled = false;
                        this.txtMatnr.Enabled = false;
                        this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
                        this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                        this.txtInsmk.Text = dtTemp.Rows[0]["INSMK"].ToString();
                        this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
                        this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
                        this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
                        this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                        this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();
                        this.txtRmak1.Text = dtTemp.Rows[0]["RMAK1"].ToString();
                        this.txtAlqty.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region ShowDdlWerks(廠區下拉選單)
        private void ShowDdlWerks()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                DataTable dtTemp1 = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp1 = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp1.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp1.Rows[i]["F_TEXT"].ToString() == Werks)
                    {
                        cmbWerks.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region ShowDdlLgort(倉別下拉選單)
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                DataTable dtTemp1 = new DataTable();
                cmbLgort.Items.Clear();
                dtTemp1 = objPlantData.GetDdlLgortData();
                for (int i = 0; i < dtTemp1.Rows.Count; i++)
                {
                    cmbLgort.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp1.Rows[i]["F_TEXT"].ToString() == Lgort)
                    {
                        cmbLgort.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region ShowLocat(顯示儲位)
        private void ShowLocat()
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                if (Lotyp.ToUpper() == "DYNAMIC LOCATION")
                {
                    cmbLocat.Items.Add(Locat);
                    this.cmbLocat.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtTemp1 = new DataTable();
                    cmbLocat.Items.Clear();
                    if (Matnr != "")
                    {
                        dtTemp1 = objPlantData.QueryMappingData(Werks, Lgort, "", Matnr);
                        if (dtTemp1.Rows.Count > 1)
                        {
                            this.cmbLocat.Enabled = true;
                        }
                        else
                        {
                            this.cmbLocat.Enabled = false;
                        }
                        for (int i = 0; i < dtTemp1.Rows.Count; i++)
                        {
                            cmbLocat.Items.Add(dtTemp1.Rows[i]["LOCAT"].ToString());
                            if (Locat != "" && Locat == dtTemp1.Rows[i]["LOCAT"].ToString())
                            {
                                this.cmbLocat.SelectedIndex = i;
                            }
                        }
                        if (this.cmbLocat.SelectedIndex == -1)
                        {
                            if (this.cmbLocat.Items.Count > 0)
                            {
                                this.cmbLocat.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocat()");
            }
        }
        #endregion

        #region CheckIsNumber
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, txtMblnr.Text.Trim(), Intype, Insmk);
                objStorageIn_SapDataSelect.ShowDialog();
                txtMblnr.Text = objStorageIn_SapDataSelect.Mblnr;
                txtMatnr.Text = objStorageIn_SapDataSelect.Matnr;

                QuerySapDocumentData();

                if (Progid == "B8")  //自動入庫沒有版本，開放給user填
                {
                    this.txtCharg.Focus();
                }
                else if (Progid == "B9")  //手動入庫沒有客人料號(WHDWN.KDMAT)，開放給user填
                {
                    this.txtKdmat.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region txtMatnr_DoubleClick
        private void txtMatnr_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, txtMblnr.Text.Trim(), Intype, Insmk);
                objStorageIn_SapDataSelect.ShowDialog();
                txtMatnr.Text = objStorageIn_SapDataSelect.Matnr;
                QuerySapDocumentData();
                if (Progid == "B8")  //自動入庫沒有版本，開放給user填
                {
                    this.txtCharg.Enabled = true;
                    this.txtCharg.Focus();
                    this.txtKdmat.Enabled = false;
                    this.txtEbeln.Enabled = false;
                }
                else if (Progid == "B9")  //手動入庫沒有客人料號(WHDWN.KDMAT)，開放給user填
                {
                    this.txtKdmat.Focus();
                }
                //this.txtAlqty.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region QuerySapDocumentData(查詢SAP單據)
        private void QuerySapDocumentData()
        {
            try
            {
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                if (txtMblnr.Text.Trim() != "")
                {
                    //依不同類型查詢不同資料
                    switch (Intype)
                    {
                        case "SPARE_PARTS":
                            dtTemp = objSapData.QuerySparePartsLineInData(Locat, txtMblnr.Text.Trim(), txtMatnr.Text.Trim(), Indat, Insmk);
                            break;
                    }
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("No Data!!");
                        return;
                    }
                    if (dtTemp.Rows.Count > 1)
                    {
                        MessageBox.Show("There is more than one Sap Data, please check the data!!");
                        return;
                    }

                    this.txtMblnr.Enabled = false;
                    this.txtMatnr.Enabled = false;
                    this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                    this.txtInsmk.Text = dtTemp.Rows[0]["INSMK"].ToString();
                    this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
                    this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
                    this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
                    this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                    this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();

                    this.btnQuery.Enabled = false;
                    this.btnConfirm.Enabled = true;
                    //this.txtAlqty.SelectAll();
                    if (Lotyp.ToUpper() == "FIXED LOCATION")
                    {
                        ShowLocat();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-QuerySapDocumentData()");
            }
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, System.EventArgs e)
        {

            try
            {
                if (txtMblnr.Text.Trim() == "" || txtMatnr.Text.Trim() == "")
                {
                    MessageBox.Show("Document No and Part No can't be empty!!");
                    this.txtMblnr.Focus();
                    return;
                }

                QuerySapDocumentData();

                if (Progid == "B8")  //自動入庫沒有版本，開放給user填
                {
                    this.txtCharg.Enabled = true;
                    this.txtCharg.Focus();
                    this.txtKdmat.Enabled = false;
                    this.txtEbeln.Enabled = false;
                }
                else if (Progid == "B9")  //手動入庫沒有客人料號(WHDWN.KDMAT)，開放給user填
                {
                    this.txtKdmat.Focus();
                }
                
                //this.txtAlqty.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                DataRow drRow;
                DataRow[] foundRow;
                DataRow[] foundRow1;
                string strTempMblnrMatnr = "";
                object[] objFind = new object[3];
                if (cmbLocat.SelectedIndex == -1)
                {
                    strLocat = "";
                }
                else
                {
                    strLocat = cmbLocat.Items[cmbLocat.SelectedIndex].ToString();
                }
                if (Locat == "")
                {
                    MessageBox.Show("Location can't be empty!!");
                    this.cmbLocat.Focus();
                    return;
                }

                if (!CheckIsNumber(this.txtAlqty.Text.Trim()) || this.txtAlqty.Text.Trim() == "0")
                {
                    MessageBox.Show("Store in Qty should be numeric and greater than 0!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                if (Convert.ToInt32(this.txtAlqty.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Store in Qty should be greater than 0!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                if (Convert.ToInt64(this.txtAlqty.Text.Trim()) > Convert.ToInt64(this.txtMenge.Text.Trim()))
                {
                    MessageBox.Show("Store in Qty should less than Un-Store Qty!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                //檢查手動入庫的PO No.是否為空
                if (this.txtEbeln.Text == "" && Progid =="B9")
                {
                    if (MessageBox.Show("The PO No. is empty, Really confirm?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.txtEbeln.Focus();
                        return;
                    }
                }
                //檢查手動入庫的Customer P/N是否為空
                if (this.txtKdmat.Text == "" && Progid == "B9")
                {
                    if (MessageBox.Show("The Customer P/N is empty, Really confirm?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.txtKdmat.Focus();
                        return;
                    }
                }
                if (this.Type != "MODIFY")
                {
                    //檢查單據號碼有沒有重覆
                    for (int i = 0; i < SapData.Rows.Count; i++)
                    {
                        if (strTempMblnrMatnr.IndexOf(SapData.Rows[i]["MBLNR"].ToString() + SapData.Rows[i]["MATNR"].ToString()) == -1)
                        {
                            strTempMblnrMatnr += SapData.Rows[i]["MBLNR"].ToString() + SapData.Rows[i]["MATNR"].ToString() + ";";
                        }
                    }
                }
                if (strTempMblnrMatnr.IndexOf(dtTemp.Rows[0]["MBLNR"].ToString() + dtTemp.Rows[0]["MATNR"].ToString()) != -1)
                {
                    MessageBox.Show("The Document No and Part No you input is duplicate!!");
                    this.txtMblnr.Enabled = true;
                    this.txtMatnr.Enabled = true;
                    this.txtMblnr.Text = "";
                    this.txtMatnr.Text = "";
                    this.txtInsmk.Text = "";
                    this.txtCharg.Text = "";
                    this.txtKdmat.Text = "";
                    this.txtEbeln.Text = "";
                    this.txtMenge.Text = "";
                    this.txtAlqty.Text = "0";
                    this.txtMblnr.Focus();
                    return;
                }
                //檢查要入庫的儲位是否已經有同料號但不版本的情況存在
                if (this.Type != "MODIFY")
                {
                    //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                    if (this.Duplicate == false)
                    {
                        foundRow1 = SapData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and LOCAT='" + Locat + "' and MATNR='" + this.txtMatnr.Text.Trim() + "' and CHARG<>'" + this.txtCharg.Text.Trim() + "'");
                        if (foundRow1.Length > 0)
                        {
                            MessageBox.Show("The material with different version can't be stored in the same location!!");
                            return;
                        }
                    }
                }
                //固定儲位如果已經有庫存資料要檢查要入庫的儲位是否有一種以上的庫別
                if (Lotyp.ToUpper() == "FIXED LOCATION")
                {
                    DataTable dtTemp1 = new DataTable();
                    //找出庫別
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                    dtTemp1 = objStorageData.QueryLocatInsmk(Locat);
                    if (dtTemp1.Rows.Count > 1)
                    {
                        MessageBox.Show("The location has different stock and you can't add any new Part No!!");
                        return;
                    }
                    if (dtTemp1.Rows.Count > 0)
                    {
                        strInsmk = dtTemp1.Rows[0]["INSMK"].ToString();
                    }
                    if (strInsmk != "" && strInsmk != txtInsmk.Text)
                    {
                        MessageBox.Show("The stock of this location is " + strInsmk + " and you can't store in " + txtInsmk.Text + " now!!");
                        return;
                    }
                    foundRow = SapData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "'  and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and LOCAT='" + Locat + "'");
                    if (foundRow.Length > 0)
                    {
                        if (foundRow[0]["INSMK"].ToString() != txtInsmk.Text)
                        {
                            MessageBox.Show("You can't store different Part No with different stock in the location!!");
                            return;
                        }
                    }
                }

                if (Type == "NEW")
                {
                    drRow = SapData.NewRow();
                    drRow["MANDT"] = dtTemp.Rows[0]["MANDT"].ToString();
                    drRow["COMCD"] = dtTemp.Rows[0]["COMCD"].ToString();
                    drRow["WERKS"] = dtTemp.Rows[0]["WERKS"].ToString();
                    drRow["LGORT"] = dtTemp.Rows[0]["LGORT"].ToString();
                    drRow["LOCAT"] = Locat;
                    drRow["MATNR"] = dtTemp.Rows[0]["MATNR"].ToString();
                    drRow["INSMK"] = dtTemp.Rows[0]["INSMK"].ToString();
                    drRow["CHARG"] = this.txtCharg.Text.Trim();
                    drRow["MENGE"] = dtTemp.Rows[0]["MENGE"].ToString();
                    drRow["ALQTY"] = this.txtAlqty.Text.Trim();
                    drRow["MBLNR"] = dtTemp.Rows[0]["MBLNR"].ToString();
                    drRow["ZEILE"] = dtTemp.Rows[0]["ZEILE"].ToString();
                    drRow["EBELN"] = this.txtEbeln.Text.Trim(); 
                    drRow["OMBLNR"] = dtTemp.Rows[0]["OMBLNR"].ToString();
                    drRow["MRGID"] = dtTemp.Rows[0]["MRGID"].ToString();
                    drRow["KOSTL"] = dtTemp.Rows[0]["KOSTL"].ToString();
                    drRow["ARBPL"] = dtTemp.Rows[0]["ARBPL"].ToString();
                    drRow["TRNTP"] = dtTemp.Rows[0]["TRNTP"].ToString();
                    drRow["RMAK1"] = this.txtRmak1.Text.Trim();
                    drRow["INDAT"] = dtTemp.Rows[0]["INDAT"].ToString();
                    if (dtTemp.Columns.IndexOf("KDMAT") > -1)
                    {
                        drRow["KDMAT"] = this.txtKdmat.Text.Trim(); 
                    }
                    else
                    {
                        drRow["KDMAT"] = "";
                    }
                    drRow["SERNO"] = dtTemp.Rows[0]["SERNO"].ToString();
                    drRow["GRLOC"] = dtTemp.Rows[0]["GRLOC"].ToString();
                    SapData.Rows.Add(drRow);

                    switch (Intype)
                    {
                        case "ONLINE":
                            StorageIn_OnLineIn objStorageIn_OnLineIn = new StorageIn_OnLineIn(ref UserData, Progid);
                            objStorageIn_OnLineIn.Data = SapData;
                            objStorageIn_OnLineIn.ShowDataGrid();
                            break;
                        case "ONLINEPROD":
                            StorageIn_Product_OnLineIn objStorageInProduct_OnLineIn = new StorageIn_Product_OnLineIn(UserData, Progid);
                            objStorageInProduct_OnLineIn.Data = SapData;
                            objStorageInProduct_OnLineIn.ShowDataGrid();
                            break;
                        case "TRANSFER":
                            StorageIn_TransferIn objStorageIn_TransferIn = new StorageIn_TransferIn(ref UserData, Progid);
                            objStorageIn_TransferIn.Data = SapData;
                            objStorageIn_TransferIn.ShowDataGrid();
                            break;
                        case "COMBINE":
                            StorageIn_CombineIn objStorageIn_CombineIn = new StorageIn_CombineIn(ref UserData, Progid);
                            objStorageIn_CombineIn.Data = SapData;
                            objStorageIn_CombineIn.ShowDataGrid();
                            break;
                    }

                    if (Intype == "SPARE_PARTS")
                    {
                        if (Progid == "B8")  //自動入庫
                        {
                            StorageIn_OnLineIn_SpareParts_Automatic objStorageIn_OnLineIn_SpareParts_Automatic = new StorageIn_OnLineIn_SpareParts_Automatic(ref UserData, Progid);
                            objStorageIn_OnLineIn_SpareParts_Automatic.Data = SapData;
                            objStorageIn_OnLineIn_SpareParts_Automatic.ShowDataGrid();
                        }
                        else if (Progid == "B9")  //手動入庫
                        {
                            StorageIn_OnLineIn_SpareParts_Manual objStorageIn_OnLineIn_SpareParts_Manual = new StorageIn_OnLineIn_SpareParts_Manual(ref UserData, Progid);
                            objStorageIn_OnLineIn_SpareParts_Manual.Data = SapData;
                            objStorageIn_OnLineIn_SpareParts_Manual.ShowDataGrid();
                        }
                    }

                    this.txtMblnr.Text = "";
                    this.txtMatnr.Text = "";
                    this.txtCharg.Text = "";
                    this.txtKdmat.Text = "";
                    this.txtEbeln.Text = "";
                    this.txtMenge.Text = "";
                    this.txtRmak1.Text = "";
                    this.txtAlqty.Text = "0";
                    this.txtMblnr.Enabled = true;
                    this.txtMatnr.Enabled = true;
                    this.txtMblnr.Focus();
                    ShowLocat();
                }
                else
                {
                    objFind[0] = dtTemp.Rows[0]["MBLNR"].ToString();
                    objFind[1] = dtTemp.Rows[0]["MATNR"].ToString();
                    objFind[2] = dtTemp.Rows[0]["SERNO"].ToString();
                    drRow = SapData.Rows.Find(objFind);
                    drRow["LOCAT"] = this.Locat;
                    drRow["CHARG"] = this.txtCharg.Text.Trim();
                    drRow["EBELN"] = this.txtEbeln.Text.Trim();
                    drRow["KDMAT"] = this.txtKdmat.Text.Trim(); 
                    drRow["ALQTY"] = this.txtAlqty.Text.Trim();
                    drRow["RMAK1"] = this.txtRmak1.Text.Trim();
                    SapData.AcceptChanges();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region btnDelete_Click
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                DataRow drRow;
                object[] objFind = new object[3];
                objFind[0] = Mblnr;
                objFind[1] = Matnr;
                objFind[2] = SerNo;

                drRow = SapData.Rows.Find(objFind);
                SapData.Rows.Remove(drRow);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region btnReturn_Click
        private void btnReturn_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region txtMblnr_KeyPress
        private void txtMblnr_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    QuerySapDocumentData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

        }
        #endregion

        #region txtAlqty_KeyPress
        private void txtAlqty_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

        }
        #endregion

        #region txtCharg_KeyDown
        private void txtCharg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.txtKdmat.Focus();
            }
        }
        #endregion

        #region txtKdmat_KeyDown
        private void txtKdmat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.txtEbeln.Focus();
            }
        }
        #endregion

        #region txtEbeln_KeyDown
        private void txtEbeln_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.txtAlqty.Focus();
                this.txtAlqty.SelectAll();
            }
        }
        #endregion

    }
}
