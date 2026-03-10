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
    public partial class StorageOut_SpareParts_Add : Form
    {
        #region 變數宣告
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strType = "";
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
        #endregion

        public StorageOut_SpareParts_Add()
        {
            InitializeComponent();
        }

        public StorageOut_SpareParts_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strMatnr, string strInsmk, string strCharg, string strMblnr, DataTable dtSapData)
		{
			InitializeComponent();
			DataRow drRow;
			DataRow[] foundRow;
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
			Progid = strProgid;
			Werks = strWerks;
			Lgort = strLgort;
			Matnr = strMatnr;
			Insmk = strInsmk;
			Charg = strCharg;
			Mblnr = strMblnr;
			SapData = dtSapData;
			strType = "NEW";

			try
			{
				//檢查權限
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
				if(!objStorageOut.CheckAuthority())
				{
					MessageBox.Show("You don't have right to use this program!!");
					this.Close();
				}
				else
				{
					ShowDdlWerks();
					ShowDdlLgort();
                    //ShowDdlInsmk();
                    cmbInsmk.SelectedIndex = 0;  //庫別預設都帶G庫
					dtTemp.Rows.Clear();
					DataColumn[] dcPrimaryKey = new DataColumn[4];
					dcPrimaryKey[0] = SapData.Columns["MATNR"];
					dcPrimaryKey[1] = SapData.Columns["INSMK"];
					dcPrimaryKey[2] = SapData.Columns["CHARG"];
					dcPrimaryKey[3] = SapData.Columns["MBLNR"];
					SapData.PrimaryKey = dcPrimaryKey;

					if(Matnr != "" && Insmk != "" && SapData.Rows.Count > 0)
					{
						strType = "MODIFY";
						dtTemp = SapData.Clone();
						foundRow = SapData.Select("MANDT='"+ Mandt +"' and COMCD='"+Comcd+"'  and WERKS='"+ Werks +"' and LGORT='"+ Lgort +"' and MATNR='"+ Matnr +"' and INSMK='"+ Insmk +"' and CHARG='"+ Charg +"' and MBLNR='"+ Mblnr +"'");
						
						for(int i=0;i<foundRow.Length;i++)
						{
							drRow = dtTemp.NewRow();
							drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[0]["COMCD"].ToString();
							drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
							drRow["LGORT"] = foundRow[0]["LGORT"].ToString();
                            //drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
                            //drRow["MBLNR"] = foundRow[0]["ZEILE"].ToString();
							drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
							drRow["INSMK"] = foundRow[0]["INSMK"].ToString();
							drRow["CHARG"] = foundRow[0]["CHARG"].ToString();
							drRow["MENGE"] = foundRow[0]["MENGE"].ToString();
							drRow["ALQTY"] = foundRow[0]["ALQTY"].ToString();
							drRow["EBELN"] = foundRow[0]["EBELN"].ToString();
                            drRow["KDMAT"] = foundRow[0]["KDMAT"].ToString();
							drRow["ARBPL"] = foundRow[0]["ARBPL"].ToString();
							drRow["TRNTP"] = foundRow[0]["TRNTP"].ToString();
							dtTemp.Rows.Add(drRow);
						}
					}
					else
					{
						if(dtSapData.Rows.Count == 0)
						{
							dtSapData = new DataTable();
							dtSapData.Columns.Add("MANDT", Type.GetType("System.String"));
                            dtSapData.Columns.Add("COMCD", Type.GetType("System.String"));
							dtSapData.Columns.Add("WERKS", Type.GetType("System.String"));
							dtSapData.Columns.Add("LGORT", Type.GetType("System.String"));
                            //dtSapData.Columns.Add("MBLNR", Type.GetType("System.String"));
                            //dtSapData.Columns.Add("ZEILE", Type.GetType("System.String"));
							dtSapData.Columns.Add("MATNR", Type.GetType("System.String"));
							dtSapData.Columns.Add("INSMK", Type.GetType("System.String"));
							dtSapData.Columns.Add("CHARG", Type.GetType("System.String"));
							dtSapData.Columns.Add("MENGE", Type.GetType("System.String"));
							dtSapData.Columns.Add("ALQTY", Type.GetType("System.String"));
							dtSapData.Columns.Add("EBELN", Type.GetType("System.String"));
                            dtSapData.Columns.Add("KDMAT", Type.GetType("System.String"));
                            dtSapData.Columns.Add("ARBPL", Type.GetType("System.String"));
                            dtSapData.Columns.Add("TRNTP", Type.GetType("System.String"));
                            dtSapData.Columns.Add("KOSTL", Type.GetType("System.String"));
							SapData = dtSapData;
						}
					}
					if(strType == "MODIFY")
					{
						this.txtMatnr.Enabled = false;
						this.txtCharg.Enabled = false;
						this.txtEbeln.Enabled = false;
						this.btnDelete.Enabled = true;
						this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
						this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();
						this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
						this.txtEbeln.Text = dtTemp.Rows[0]["EBELN"].ToString();
						this.txtKdmat.Text = dtTemp.Rows[0]["KDMAT"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp.Rows[i]["F_TEXT"].ToString() == Werks)
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

        private void ShowDdlLgort()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbLgort.Items.Clear();
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                dtTemp = objPlantData.GetDdlLgortData();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp.Rows[i]["F_TEXT"].ToString() == Lgort)
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


        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShowDdlLgort();
        }

        //private void ShowDdlInsmk()
        //{
        //    try
        //    {
        //        cmbInsmk.Items.Clear();
        //        QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
        //        dtTemp = objPlantData.GetDdlInsmk();
        //        for (int i = 0; i < dtTemp.Rows.Count; i++)
        //        {
        //            cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //            if (Insmk != "" && cmbInsmk.Items[i].ToString() == Insmk)
        //            {
        //                cmbInsmk.SelectedIndex = i;
        //                cmbInsmk.Enabled = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlInsmk()");
        //    }
        //}

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                string strTempMblnrMatnr = "";
                DataRow drRow;
                object[] objFind = new object[4];
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
                ////庫別不可空白
                //if (this.cmbInsmk.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Stock can't be empty!!");
                //    this.txtMatnr.Focus();
                //    return;
                //}

                //數量不可空白或0
                if (!CheckIsNumber(this.txtMenge.Text.Trim()) || this.txtMenge.Text.Trim() == "0")
                {
                    MessageBox.Show("Store out Qty should be numeric and greater than 0!!");
                    this.txtMenge.Focus();
                    this.txtMenge.SelectAll();
                    return;
                }

                if (strType != "MODIFY")
                {
                    //檢查料號資料有沒有重覆
                    for (int i = 0; i < SapData.Rows.Count; i++)
                    {
                        if (strTempMblnrMatnr.IndexOf(SapData.Rows[i]["MATNR"].ToString() + SapData.Rows[i]["INSMK"].ToString() + SapData.Rows[i]["CHARG"].ToString() + SapData.Rows[i]["MBLNR"].ToString() + ";") == -1)
                        {
                            strTempMblnrMatnr += SapData.Rows[i]["MATNR"].ToString() + SapData.Rows[i]["INSMK"].ToString() + SapData.Rows[i]["CHARG"].ToString() + SapData.Rows[i]["MBLNR"].ToString() + ";";
                        }
                    }
                }
                if (strTempMblnrMatnr.IndexOf(txtMatnr.Text.Trim() + this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString() + this.txtCharg.Text.Trim() + this.txtEbeln.Text.Trim() + ";") != -1)
                {
                    MessageBox.Show("The data you input is duplicate in the location!!");
                    this.txtMatnr.Focus();
                    this.txtMatnr.SelectAll();
                    return;
                }

                if (strType == "NEW")
                {
                    drRow = SapData.NewRow();
                    drRow["MANDT"] = Mandt;
                    drRow["COMCD"] = Comcd;
                    drRow["WERKS"] = Werks;
                    drRow["LGORT"] = Lgort;
                    drRow["EBELN"] = this.txtEbeln.Text.Trim();
                    drRow["MATNR"] = this.txtMatnr.Text.Trim();
                    drRow["INSMK"] = this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                    drRow["CHARG"] = this.txtCharg.Text.Trim();
                    drRow["MENGE"] = this.txtMenge.Text.Trim();
                    drRow["ALQTY"] = "0";
                    drRow["KDMAT"] = this.txtKdmat.Text.Trim();
                    drRow["ARBPL"] = "";
                    drRow["TRNTP"] = "";
                    drRow["KOSTL"] = "";
                    SapData.Rows.Add(drRow);
                }
                else
                {

                    objFind[0] = this.txtMatnr.Text.Trim();
                    objFind[1] = this.cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                    objFind[2] = this.txtCharg.Text.Trim();
                    objFind[3] = this.txtEbeln.Text.Trim();
                    drRow = SapData.Rows.Find(objFind);
                    drRow["MENGE"] = this.txtMenge.Text.Trim();
                    drRow["KDMAT"] = this.txtKdmat.Text.Trim();
                    SapData.AcceptChanges();

                }
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

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                DataRow drRow;
                object[] objFind = new object[4];
                objFind[0] = Matnr;
                objFind[1] = Insmk;
                objFind[2] = Charg;
                objFind[3] = Mblnr;
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

        private void btnReturn_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
