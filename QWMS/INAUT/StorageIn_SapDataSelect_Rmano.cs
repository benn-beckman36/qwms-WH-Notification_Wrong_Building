using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class StorageIn_SapDataSelect_Rmano : Form
    {
        #region 宣告變數
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strIntype = "";
        private string strInsmk = "";
        private string strComcd = "";
        private string strCrdat = "";
        private string strRmano = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();
        private ArrayList aryMblnr;
        private ArrayList aryMatnr;

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

        public string Crdat
        {
            get
            {
                return strCrdat;
            }
            set
            {
                strCrdat = value;
            }
        }
        #endregion

        #region 主程式 
        public StorageIn_SapDataSelect_Rmano(UserInfo varUserData, string strWerks, string strLgort, string strProgid, ArrayList varMblnr, string strIntype, string strInsmk, string strCrdat)
		{
			InitializeComponent();
            UserData = varUserData;
			Mandt = varUserData.Client;
			Werks = strWerks;
			Lgort = strLgort;
			Usrnm = varUserData.UserId;
			Progid = strProgid;
            Mblnr = varMblnr;
			Intype = strIntype;
			Insmk = strInsmk;
            Crdat = strCrdat;
            strComcd = varUserData.CompanyCode;

			try
			{
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), varUserData, Progid);

				//檢查權限
				if(!objStorageIn.CheckAuthority())
				{
					MessageBox.Show("You don't have right to use this program!!");
					this.Close();
				}
				else
				{
					ShowSapData();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
        }
        #endregion

        #region ShowSapData
        private void ShowSapData()
        {
            string strQueryDate = "";
            if (chkDate.Checked)
                strQueryDate = Crdat;

            try
            {
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                switch (Intype)
                {
                    case "DOA":
                        dtData = objSapData.ListSapInData_DOA(Insmk, Progid, strQueryDate, strRmano);
                        break;
                }

                DataColumn cSelect = new DataColumn("Select", typeof(bool));
                dtData.Columns.Add(cSelect);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["Select"] = false;
                }

                if (dtData.Rows.Count > 0)
                {
                    btnSelect.Enabled = true;
                }

                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSapData()");
            }

        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                dtgData.TableStyles.Clear();
                DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                mydtgTableStyle.MappingName = dtData.TableName;

                DataGridBoolColumn selectStyle = new DataGridBoolColumn();
                selectStyle.MappingName = "Select";
                selectStyle.HeaderText = "Select";
                selectStyle.Width = 40;
                ((DataGridBoolColumn)selectStyle).AllowNull = false;
                mydtgTableStyle.GridColumnStyles.Add(selectStyle);

                DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
                mblnrStyle.MappingName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 120;
                mblnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

                DataGridColumnStyle matnrStyle = new DataGridTextBoxColumn();
                matnrStyle.MappingName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(matnrStyle);

                DataGridColumnStyle balanceStyle = new DataGridTextBoxColumn();
                balanceStyle.MappingName = "BALANCE";
                balanceStyle.HeaderText = "Balance Qty";
                balanceStyle.Width = 90;
                balanceStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(balanceStyle);

                dtgData.DataSource = dtData;
                dtgData.TableStyles.Add(mydtgTableStyle);

                dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region btnSelect_Click
        private void btnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["Select"]))
                {
                    if (aryMblnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString()) < 0)
                    {
                        aryMblnr.Add(dtData.Rows[i]["MBLNR"]);
                    }
                }
            }

            if (Mblnr.Count == 0)
            {
                MessageBox.Show("Please select one Document No!!");
                return;
            }
            this.Close();
        }
        #endregion

        #region btnReturn_Click
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            strCrdat = dtpOudat.Text.Replace("/", "-");
            strRmano = txtRmano.Text.Trim();
            ShowSapData();
        }
        #endregion

        #region chkDate_CheckedChanged
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked == true)
            {
                dtpOudat.Enabled = true;
            }
            else
            {
                dtpOudat.Enabled = false;
            }
        }
        #endregion
    }
}
