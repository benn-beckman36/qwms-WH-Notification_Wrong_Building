using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageIn_OffLineIn_SpareParts_Add : Form
    {
        #region 宣告變數
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strIndat = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strType = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strComcd = "";
        private bool bolDuplicate = false;
        UserInfo UserData = new UserInfo();
        private DataTable dtSapData = new DataTable();
        private DataTable dtTemp = new DataTable();
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

        #endregion

        public StorageIn_OffLineIn_SpareParts_Add()
        {
            InitializeComponent();
        }

        public StorageIn_OffLineIn_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp, string strMatnr, string strInsmk, string strCharg, DataTable dtSapData, bool bolDuplicate)
		{
			InitializeComponent();
            UserData = varUserData;
			DataRow drRow;
			DataRow[] foundRow;
            Mandt = UserData.Client;
			Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
			Progid = strProgid;
			Werks = strWerks;
			Lgort = strLgort;
			Locat = strLocat;
			txtMatnr.Text = strMatnr;
			Insmk = strInsmk;
			Charg = strCharg;
			SapData = dtSapData;
			Type = "NEW";
			Sttyp = strSttyp;
			Lotyp = strLotyp;
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
                    cmbInsmk.SelectedIndex = 0;  //庫別預設都帶G庫
					dtTemp.Rows.Clear();
					DataColumn[] dcPrimaryKey = new DataColumn[4];
					dcPrimaryKey[0] = SapData.Columns["LOCAT"];
					dcPrimaryKey[1] = SapData.Columns["MATNR"];
					dcPrimaryKey[2] = SapData.Columns["INSMK"];
					dcPrimaryKey[3] = SapData.Columns["CHARG"];
					SapData.PrimaryKey = dcPrimaryKey;

					if(Matnr != "" && Insmk != "" && SapData.Rows.Count > 0)
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
                        dtTemp.Columns.Add("KDMAT", Type.GetType());
						dtTemp.Columns.Add("LIFNR", Type.GetType());
						dtTemp.Columns.Add("OMBLNR", Type.GetType());
						dtTemp.Columns.Add("MRGID", Type.GetType());
						dtTemp.Columns.Add("KOSTL", Type.GetType());
						dtTemp.Columns.Add("ARBPL", Type.GetType());
						dtTemp.Columns.Add("TRNTP", Type.GetType());
						dtTemp.Columns.Add("RMAK1", Type.GetType());
						dtTemp.Columns.Add("INDAT", Type.GetType());
						foundRow = SapData.Select("MANDT='"+ Mandt +"' and COMCD='"+Comcd+"' and WERKS='"+ Werks +"' and LGORT='"+ Lgort +"' and MATNR='"+ Matnr +"' and INSMK='"+ Insmk +"' and CHARG='"+ Charg +"'");
						
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
                            drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
                            drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
							drRow["OMBLNR"] = foundRow[0]["OMBLNR"].ToString();
							drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
							drRow["KOSTL"] = foundRow[0]["KOSTL"].ToString();
							drRow["ARBPL"] = foundRow[0]["ARBPL"].ToString();
							drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
							drRow["RMAK1"] = foundRow[0]["RMAK1"].ToString();
							drRow["INDAT"] = foundRow[0]["INDAT"].ToString();
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
                            dtSapData.Columns.Add("KDMAT", Type.GetType());
                            dtSapData.Columns.Add("LIFNR", Type.GetType());
							dtSapData.Columns.Add("OMBLNR", Type.GetType());
							dtSapData.Columns.Add("MRGID", Type.GetType());
							dtSapData.Columns.Add("KOSTL", Type.GetType());
							dtSapData.Columns.Add("ARBPL", Type.GetType());
							dtSapData.Columns.Add("TRNTP", Type.GetType());
							dtSapData.Columns.Add("RMAK1", Type.GetType());
							dtSapData.Columns.Add("INDAT", Type.GetType());
							SapData = dtSapData;
						}
					}
					if(Type == "MODIFY")
					{
						this.cmbLocat.Enabled = false;
						this.txtMatnr.Enabled = false;
						this.txtCharg.Enabled = false;
						this.btnDelete.Enabled = true;
						this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
						this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
                        this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
                        this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
                        this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();
						this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
						this.txtRmak1.Text = dtTemp.Rows[0]["RMAK1"].ToString();
						this.dtpIndat.Value = new DateTime(Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(0,4)), Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(4,2)), Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(6,2)));
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}


        //for 離線入庫修改PO No./Customer P/N/數量使用  Smose Liao 20100726
        public StorageIn_OffLineIn_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp, string strMblnr, string strMatnr, string strCharg, string strEbeln, string strKdmat, DataTable dtSapData, bool bolDuplicate)
        {
            InitializeComponent();
            UserData = varUserData;
            DataRow drRow;
            DataRow[] foundRow;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
            Charg = strCharg;
            Insmk = "G";  //庫別預設都帶G庫
            txtMblnr.Text = strMblnr;
            txtMatnr.Text = strMatnr;
            txtEbeln.Text = strEbeln;
            txtKdmat.Text = strKdmat;
            txtCharg.Text = strCharg;
            SapData = dtSapData;
            Type = "NEW";
            Sttyp = strSttyp;
            Lotyp = strLotyp;
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
                    cmbInsmk.SelectedIndex = 0;  //庫別預設都帶G庫
                    dtTemp.Rows.Clear();
                    DataColumn[] dcPrimaryKey = new DataColumn[3];
                    dcPrimaryKey[0] = SapData.Columns["LOCAT"];
                    dcPrimaryKey[1] = SapData.Columns["MATNR"];
                    dcPrimaryKey[2] = SapData.Columns["INSMK"];
                    //dcPrimaryKey[3] = SapData.Columns["CHARG"];
                    SapData.PrimaryKey = dcPrimaryKey;

                    if (strMblnr != "" && strMatnr != "" && SapData.Rows.Count > 0)
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
                        dtTemp.Columns.Add("KDMAT", Type.GetType());
                        dtTemp.Columns.Add("LIFNR", Type.GetType());
                        dtTemp.Columns.Add("OMBLNR", Type.GetType());
                        dtTemp.Columns.Add("MRGID", Type.GetType());
                        dtTemp.Columns.Add("KOSTL", Type.GetType());
                        dtTemp.Columns.Add("ARBPL", Type.GetType());
                        dtTemp.Columns.Add("TRNTP", Type.GetType());
                        dtTemp.Columns.Add("RMAK1", Type.GetType());
                        dtTemp.Columns.Add("INDAT", Type.GetType());
                        foundRow = SapData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and MATNR='" + Matnr + "' and CHARG='" + Charg + "' and EBELN='" + Ebeln + "' and KDMAT='" + Kdmat + "'");

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
                            drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
                            drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
                            drRow["OMBLNR"] = foundRow[0]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
                            drRow["KOSTL"] = foundRow[0]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[0]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[0]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[0]["INDAT"].ToString();
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
                            dtSapData.Columns.Add("KDMAT", Type.GetType());
                            dtSapData.Columns.Add("LIFNR", Type.GetType());
                            dtSapData.Columns.Add("OMBLNR", Type.GetType());
                            dtSapData.Columns.Add("MRGID", Type.GetType());
                            dtSapData.Columns.Add("KOSTL", Type.GetType());
                            dtSapData.Columns.Add("ARBPL", Type.GetType());
                            dtSapData.Columns.Add("TRNTP", Type.GetType());
                            dtSapData.Columns.Add("RMAK1", Type.GetType());
                            dtSapData.Columns.Add("INDAT", Type.GetType());
                            SapData = dtSapData;
                        }
                    }
                    if (Type == "MODIFY")
                    {
                        this.cmbLocat.Enabled = false;
                        this.txtMatnr.Enabled = false;
                        this.btnDelete.Enabled = true;
                        this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                        this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
                        this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
                        this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
                        this.txtAlqty.Text = dtTemp.Rows[0]["ALQTY"].ToString();
                        this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
                        this.txtRmak1.Text = dtTemp.Rows[0]["RMAK1"].ToString();
                        this.dtpIndat.Value = new DateTime(Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(0, 4)), Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(4, 2)), Convert.ToInt16(dtTemp.Rows[0]["INDAT"].ToString().Substring(6, 2)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        private void ShowDdlWerks()
		{
			try
			{
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				DataTable dtTemp1 = new DataTable();
				cmbWerks.Items.Clear();
				dtTemp1 = objAuthority.CheckPlantAuthority();
				for(int i=0;i<dtTemp1.Rows.Count;i++)
				{
					cmbWerks.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
					if(dtTemp1.Rows[i]["F_TEXT"].ToString() == Werks)
					{
						cmbWerks.SelectedIndex = i;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlWerks()");
			}
		}

		private void ShowDdlLgort()
		{
			try
			{
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
				DataTable dtTemp1 = new DataTable();
				cmbLgort.Items.Clear();
				dtTemp1 = objPlantData.GetDdlLgortData();
				for(int i=0;i<dtTemp1.Rows.Count;i++)
				{
					cmbLgort.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
					if(dtTemp1.Rows[i]["F_TEXT"].ToString() == Lgort)
					{
						cmbLgort.SelectedIndex = i;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
		}

		private void ShowLocat()
		{
			try
			{
				if(Lotyp.ToUpper() == "DYNAMIC LOCATION")
				{
					cmbLocat.Items.Add(Locat);
					this.cmbLocat.SelectedIndex = 0;
				}
				else
				{
					DataTable dtTemp1 = new DataTable();
					cmbLocat.Items.Clear();
					if(Matnr != "")
					{
                        QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
						dtTemp1 = objPlantData.QueryMappingData(Werks, Lgort, "", Matnr);
						if(dtTemp1.Rows.Count > 1)
						{
							this.cmbLocat.Enabled = true;
						}
						else
						{
							this.cmbLocat.Enabled = false;
						}
						for(int i=0;i<dtTemp1.Rows.Count;i++)
						{
							cmbLocat.Items.Add(dtTemp1.Rows[i]["LOCAT"].ToString());
							if(Locat != "" && Locat == dtTemp1.Rows[i]["LOCAT"].ToString())
							{
								this.cmbLocat.SelectedIndex = i;
							}
						}
						if(this.cmbLocat.SelectedIndex == -1)
						{
							if(this.cmbLocat.Items.Count >0)
							{
								this.cmbLocat.SelectedIndex = 0;
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowLocat()");
			}
		}

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempMblnrMatnr = "";
                DataRow drRow;
                DataRow[] foundRow;
                DataRow[] foundRow1;
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
                //料號不可空白
                if (this.txtMatnr.Text.Trim() == "")
                {
                    MessageBox.Show("Part No can't be empty!!");
                    this.txtMatnr.Focus();
                    return;
                }
                //檢查料號是否存在
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                if (!objPlantData.CheckExistedMatnr(this.txtMatnr.Text.Trim()))
                {
                    MessageBox.Show(this.txtMatnr.Text.Trim() + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //數量不可空白或0
                if (!CheckIsNumber(this.txtAlqty.Text.Trim()) || this.txtAlqty.Text.Trim() == "0")
                {
                    MessageBox.Show("Store in Qty should be numeric and greater than 0!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
                //庫別不可空白
                if (this.cmbInsmk.SelectedIndex == -1)
                {
                    MessageBox.Show("Stock can't be empty!!");
                    this.txtMatnr.Focus();
                    return;
                }
                //檢查離線入庫的PO No.是否為空
                if (this.txtEbeln.Text == "" && Progid == "BA")
                {
                    if (MessageBox.Show("The PO No. is empty, Really confirm?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.txtEbeln.Focus();
                        return;
                    }
                }
                //檢查離線入庫的Customer P/N是否為空
                if (this.txtKdmat.Text == "" && Progid == "BA")
                {
                    if (MessageBox.Show("The Customer P/N is empty, Really confirm?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.txtKdmat.Focus();
                        return;
                    }
                }

                if (this.Type != "MODIFY")
                {
                    //檢查料號資料有沒有重覆
                    for (int i = 0; i < SapData.Rows.Count; i++)
                    {
                        if (strTempMblnrMatnr.IndexOf(SapData.Rows[i]["LOCAT"].ToString() + SapData.Rows[i]["MATNR"].ToString() + SapData.Rows[i]["INSMK"].ToString() + SapData.Rows[i]["CHARG"].ToString() + ";") == -1)
                        {
                            strTempMblnrMatnr += SapData.Rows[i]["LOCAT"].ToString() + SapData.Rows[i]["MATNR"].ToString() + SapData.Rows[i]["INSMK"].ToString() + SapData.Rows[i]["CHARG"].ToString() + ";";
                        }
                    }
                }
                if (strTempMblnrMatnr.IndexOf(strLocat + txtMatnr.Text.Trim() + this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString() + this.txtCharg.Text.Trim() + ";") != -1)
                {
                    MessageBox.Show("The data you input is duplicate in the location!!");
                    this.txtMatnr.Focus();
                    this.txtMatnr.SelectAll();
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
                    if (strInsmk != "" && strInsmk != cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString())
                    {
                        MessageBox.Show("The stock of this location is " + strInsmk + " and you can't store in " + cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString() + " now!!");
                        return;
                    }
                    foundRow = SapData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and LOCAT='" + Locat + "'");
                    if (foundRow.Length > 0)
                    {
                        if (foundRow[0]["INSMK"].ToString() != cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString())
                        {
                            MessageBox.Show("You can't store different Part No with different stock in the location!!");
                            return;
                        }
                    }
                }

                if (Type == "NEW")
                {
                    drRow = SapData.NewRow();
                    drRow["MANDT"] = Mandt;
                    drRow["WERKS"] = Werks;
                    drRow["LGORT"] = Lgort;
                    drRow["LOCAT"] = Locat;
                    drRow["MATNR"] = this.txtMatnr.Text.Trim();
                    drRow["INSMK"] = this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                    drRow["CHARG"] = this.txtCharg.Text.Trim();
                    drRow["MENGE"] = "0";
                    drRow["ALQTY"] = this.txtAlqty.Text.Trim();
                    drRow["MBLNR"] = this.txtMblnr.Text.Trim();
                    drRow["ZEILE"] = "";
                    drRow["EBELN"] = this.txtEbeln.Text.Trim();
                    drRow["KDMAT"] = this.txtKdmat.Text.Trim();
                    drRow["OMBLNR"] = "";
                    drRow["MRGID"] = "";
                    drRow["KOSTL"] = "";
                    drRow["ARBPL"] = "";
                    drRow["TRNTP"] = "";
                    drRow["RMAK1"] = this.txtRmak1.Text.Trim();
                    drRow["INDAT"] = this.dtpIndat.Value.ToString("yyyyMMdd");
                    drRow["Comcd"] = Comcd;
                    SapData.Rows.Add(drRow);

                    StorageIn_OffLineIn_SpareParts objStorageIn_OffLineIn_SpareParts = new StorageIn_OffLineIn_SpareParts(ref UserData, Progid);
                    objStorageIn_OffLineIn_SpareParts.Data = SapData;
                    objStorageIn_OffLineIn_SpareParts.ShowDataGrid();
                    this.txtMblnr.Text = "";
                    this.txtMatnr.Text = "";
                    this.txtCharg.Text = "";
                    this.txtEbeln.Text = "";
                    this.txtKdmat.Text = "";
                    this.txtRmak1.Text = "";
                    this.txtAlqty.Text = "0";
                    this.txtMatnr.Focus();
                    if (SapData.Rows.Count > 0)
                    {
                        Insmk = SapData.Rows[0]["INSMK"].ToString();
                        this.cmbInsmk.Enabled = false;
                    }
                    ShowLocat();

                }
                else if (Type == "MODIFY")
                {
                    StorageIn_OffLineIn_SpareParts objStorageIn_OffLineIn_SpareParts = new StorageIn_OffLineIn_SpareParts(ref UserData, Progid);
                    objStorageIn_OffLineIn_SpareParts.Data = SapData;
                    objStorageIn_OffLineIn_SpareParts.ShowDataGrid();

                    objFind[0] = Locat;
                    objFind[1] = this.txtMatnr.Text.Trim();
                    objFind[2] = this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                    drRow = SapData.Rows.Find(objFind);
                    drRow["LOCAT"] = this.Locat;
                    drRow["CHARG"] = this.txtCharg.Text.Trim();
                    drRow["ALQTY"] = this.txtAlqty.Text.Trim();
                    drRow["MBLNR"] = this.txtMblnr.Text.Trim();
                    drRow["EBELN"] = this.txtEbeln.Text.Trim();
                    drRow["KDMAT"] = this.txtKdmat.Text.Trim();
                    drRow["RMAK1"] = this.txtRmak1.Text.Trim();
                    drRow["INDAT"] = this.dtpIndat.Value.ToString("yyyyMMdd");
                    drRow["COMCD"] = Comcd;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drRow;
                object[] objFind = new object[3];
                objFind[0] = Locat;
                objFind[1] = Matnr;
                objFind[2] = Insmk;
                //objFind[3] = Charg;
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    if (Lotyp.ToUpper() == "FIXED LOCATION")
                    {
                        ShowLocat();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void txtMatnr_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Lotyp.ToUpper() == "FIXED LOCATION" && Matnr != "")
                {
                    ShowLocat();
                }
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
