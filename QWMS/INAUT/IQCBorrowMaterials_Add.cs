using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;
using NPOI.SS.Formula.Functions;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class IQCBorrowMaterials_Add : Form
    {
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

        public IQCBorrowMaterials_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strSttyp, string strLotyp,  string strIndat, string strMblnr, string strMatnr, DataTable dtSapData)
        {
            InitializeComponent();
            UserData = varUserData;
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
            Type = "NEW";
            Sttyp = strSttyp;
            Lotyp = strLotyp;

            ShowDdlWerks();
            ShowDdlLgort();
            ShowLocat();
            dtTemp.Rows.Clear();

            DataColumn[] dcPrimaryKey = new DataColumn[3];
            dcPrimaryKey[0] = SapData.Columns["MBLNR"];
            dcPrimaryKey[1] = SapData.Columns["MATNR"];
            SapData.PrimaryKey = dcPrimaryKey;

            if (Mblnr != "" && Matnr != "" && SapData.Rows.Count > 0)
            {
                Type = "MODIFY";
                dtTemp.Columns.Add("MANDT", Type.GetType());
                dtTemp.Columns.Add("WERKS", Type.GetType());
                dtTemp.Columns.Add("LGORT", Type.GetType());
                dtTemp.Columns.Add("LOCAT", Type.GetType());
                dtTemp.Columns.Add("MATNR", Type.GetType());
                dtTemp.Columns.Add("MENGE", Type.GetType());//数量
                dtTemp.Columns.Add("BRQTY", Type.GetType());//借料数量
                dtTemp.Columns.Add("RTQTY", Type.GetType());//还料数量
                dtTemp.Columns.Add("MBLNR", Type.GetType());
                dtTemp.Columns.Add("LIFNR", Type.GetType());
                dtTemp.Columns.Add("BWART", Type.GetType());
                dtTemp.Columns.Add("IQCID", Type.GetType());//借料IQC工号
                dtTemp.Columns.Add("WHID", Type.GetType());//借料仓管工号
                dtTemp.Columns.Add("RTIQCID", Type.GetType());//还料IQC工号
                dtTemp.Columns.Add("RTWHID", Type.GetType());//还料仓管工号
                dtTemp.Columns.Add("BUDAT", Type.GetType());//入账时间
                dtTemp.Columns.Add("CRDAT", Type.GetType());//SAP单据建档时间
                dtTemp.Columns.Add("BRDAT", Type.GetType());//借出时间
                dtTemp.Columns.Add("RTDAT", Type.GetType());//还料时间
                dtTemp.Columns.Add("REMAK", Type.GetType());

                foundRow = SapData.Select("MANDT='" + Mandt + "' and  WERKS='" + Werks + "' and LGORT='" + Lgort + "' and MBLNR='" + Mblnr + "' and MATNR='" + Matnr + "'");

                for (int i = 0; i < foundRow.Length; i++)
                {
                    drRow = dtTemp.NewRow();
                    drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                    drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
                    drRow["LGORT"] = foundRow[0]["LGORT"].ToString();
                    drRow["LOCAT"] = foundRow[0]["LOCAT"].ToString();
                    drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
                    drRow["MENGE"] = foundRow[0]["MENGE"].ToString();
                    drRow["BRQTY"] = foundRow[0]["BRQTY"].ToString();
                    drRow["RTQTY"] = foundRow[0]["RTQTY"].ToString();
                    drRow["MBLNR"] = foundRow[0]["MBLNR"].ToString();
                    drRow["LIFNR"] = foundRow[0]["LIFNR"].ToString();
                    drRow["BWART"] = foundRow[0]["BWART"].ToString();
                    drRow["IQCID"] = foundRow[0]["IQCID"].ToString();
                    drRow["WHID"] = foundRow[0]["WHID"].ToString();
                    drRow["RTIQCID"] = foundRow[0]["RTIQCID"].ToString();
                    drRow["RTWHID"] = foundRow[0]["RTWHID"].ToString();
                    drRow["BUDAT"] = foundRow[0]["BUDAT"].ToString(); 
                    drRow["CRDAT"] = foundRow[0]["CRDAT"].ToString();
                    drRow["BRDAT"] = foundRow[0]["BRDAT"].ToString();
                    drRow["RTDAT"] = foundRow[0]["RTDAT"].ToString();
                    drRow["REMAK"] = foundRow[0]["REMAK"].ToString();
                    dtTemp.Rows.Add(drRow);
                }
            }
            if (Type == "MODIFY")
            {
                this.btnConfirm.Enabled = true;
                this.txtMblnr.Enabled = false;
                this.txtMatnr.Enabled = false;
                this.txtMblnr.Text = dtTemp.Rows[0]["MBLNR"].ToString();
                this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
                this.txtLifnr.Text = dtTemp.Rows[0]["LIFNR"].ToString();
                this.txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                if (!string.IsNullOrEmpty(dtTemp.Rows[0]["BRQTY"].ToString()))//借料数量栏位有值，即已借过料
                {
                    if (Convert.ToInt32(dtTemp.Rows[0]["BRQTY"].ToString()) != 0)
                    {
                        this.txtBrqty.Text = dtTemp.Rows[0]["BRQTY"].ToString();
                    }
                    else
                    {
                        this.txtBrqty.Text = "0";
                    }
                }
                else
                {
                    this.txtBrqty.Text = "0";
                }
                if (!string.IsNullOrEmpty(dtTemp.Rows[0]["RTQTY"].ToString()))
                {
                    if (Convert.ToInt32(dtTemp.Rows[0]["RTQTY"].ToString()) != 0)
                    {
                        this.txtRtqty.Text = dtTemp.Rows[0]["RTQTY"].ToString();
                    }
                    else
                    {
                        this.txtRtqty.Text = "0";
                    }
                }
                else
                {
                    this.txtRtqty.Text = "0";
                }
               
                this.txtRmak1.Text = dtTemp.Rows[0]["REMAK"].ToString();
            }
        }

        private void ShowDdlWerks()
		{
			try
			{
                QCI.QWMS.Authority objAuthority=new QCI.QWMS.Authority(UserData);
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

        #region 倉別
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
        #endregion

        #region 顯示儲位
        private void ShowLocat()
		{
			try
			{
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
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
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataRow drRow;

				object[] objFind = new object[2];

				if(cmbLocat.SelectedIndex == -1)
				{
					strLocat = "";
				}
				else
				{
					strLocat = cmbLocat.Items[cmbLocat.SelectedIndex].ToString();
				}
				if(Locat == "")
				{
					MessageBox.Show("Location can't be empty!!");
					this.cmbLocat.Focus();
					return;
				}
			    
			    if (!CheckIsNumber(this.txtBrqty.Text.Trim()) || Convert.ToInt32(this.txtBrqty.Text.Trim()) <= 0)
		        {
		            MessageBox.Show("借料数量必须为数字且大于0!!");
                    this.txtBrqty.Focus();
                    this.txtBrqty.SelectAll();
		            return;
		        }
                if (Convert.ToInt32(this.txtBrqty.Text.Trim()) > Convert.ToInt32(this.txtMenge.Text.Trim()))
		        {
		            MessageBox.Show("借料数量不能大于单据数量!!");
                    this.txtBrqty.Focus();
                    this.txtBrqty.SelectAll();
		            return;
		        }
			    if (Convert.ToInt32(this.txtRtqty.Text.Trim()) != 0)//选择还料
			    {
			        if (string.IsNullOrEmpty(dtTemp.Rows[0]["IQCID"].ToString()) || dtTemp.Rows[0]["IQCID"].ToString() == "")//借料IQC与仓管栏位为空即还未借过料
			        {
			            if (string.IsNullOrEmpty(dtTemp.Rows[0]["WHID"].ToString()) || dtTemp.Rows[0]["WHID"].ToString() == "")
			            {
			                MessageBox.Show("未借料不可还料!!");
			                this.txtRtqty.Focus();
			                this.txtRtqty.SelectAll();
			                return;
			            }
			        }
			        if (!CheckIsNumber(this.txtRtqty.Text.Trim()) || Convert.ToInt32(this.txtRtqty.Text.Trim()) <= 0)
                    {
                        MessageBox.Show("还料数量必须为数字且大于0!!");
                        this.txtRtqty.Focus();
                        this.txtRtqty.SelectAll();
                        return;
                    }
			        if (Convert.ToInt32(this.txtRtqty.Text.Trim()) > Convert.ToInt32(this.txtBrqty.Text.Trim()))
			        {
			            MessageBox.Show("还料数量必须小于或等于借料数量!!");
			            this.txtRtqty.Focus();
			            this.txtRtqty.SelectAll();
			            return;
			        }
			    }
			    if (txtIQCID.Text.Trim()== "" || txtWHID.Text.Trim()== "")
                {
                    MessageBox.Show("工号不可为空!!");
                    this.txtMatnr.Focus();
                    return;
                }
                //工号為8碼
                if (txtIQCID.Text.Trim().Length != 8||txtWHID.Text.Trim().Length!=8)
                {
                    MessageBox.Show("工号长度必须为8码!!");
                    this.txtMatnr.Focus();
                    return;
                }
                

	            objFind[0] = dtTemp.Rows[0]["MBLNR"].ToString();
	            objFind[1] = dtTemp.Rows[0]["MATNR"].ToString();
	            drRow = SapData.Rows.Find(objFind);//数据回传
	            drRow["LOCAT"] = this.Locat;
    		    
			    if (Convert.ToInt32(this.txtRtqty.Text.Trim()) != 0)//选择还料
			    {
			        drRow["RTQTY"] = this.txtRtqty.Text.Trim();
                    drRow["RTIQCID"] = this.txtIQCID.Text.Trim();
                    drRow["RTWHID"] = this.txtWHID.Text.Trim();
			    }
			    else//第一次借料
			    {
                    drRow["BRQTY"] = this.txtBrqty.Text.Trim();
			        drRow["RTQTY"] = "0";
                    drRow["IQCID"] = this.txtIQCID.Text.Trim();
                    drRow["WHID"] = this.txtWHID.Text.Trim();
			    }
                drRow["REMAK"] =this.txtRmak1.Text.Trim();
	            SapData.AcceptChanges();
	            this.Close();

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
        }
        #endregion
        #region 是否為數字
        private bool CheckIsNumber(string strValue)
		{
			Regex rgxNumber = new Regex("[0-9]");
			return rgxNumber.IsMatch(strValue);
        }
        #endregion
       
        #region Return
        private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
        }
        #endregion
    }
}
