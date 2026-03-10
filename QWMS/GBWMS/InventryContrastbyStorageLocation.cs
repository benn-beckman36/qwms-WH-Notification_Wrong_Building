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
using Qci.Base.Common;
using QWMS.Entity;
using System.Collections;
using System.IO;

namespace QWMS
{
    public partial class InventryContrastbyStorageLocation : Form
    {

        #region Datamember
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private CSMCStorageData objStorageData;
        //private ArrayList arySQL;


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

        #endregion Datamember
          
        #region function
        public InventryContrastbyStorageLocation(UserInfo _UserData, string strProgid)
		{
			InitializeComponent();
            UserData = _UserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
			Progid = strProgid;
			
			try
			{
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);
                objStorageData = new CSMCStorageData(UserData);


				//檢查權限
				if(!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					//秀出Status的資料
					ShowDdlWerks();
                    ShowStatusData();
					//ShowDdlLgort();
                    //if(cmbWerks.Items.Count > 0)
                    //{
                    //    this.cmbWerks.SelectedIndex = 0;
                    //}
                    //if(cmbLgort.Items.Count > 0)
                    //{
                    //    this.cmbLgort.SelectedIndex = 0;
                    //}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


        public InventryContrastbyStorageLocation()
        {
            InitializeComponent();
        }

        //查询事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex == -1)
            {
                MessageBox.Show("请选择厂区！"); return;
            }
            if (cmbLgort.SelectedIndex == -1)
            {
                MessageBox.Show("请选择仓别！"); return;
            }  
            lblRowCount.Text = "0 records";
            DataTable dtSource = objStorageData.QueryInventoryContrastbyStorageLocation(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "");
            if (dtSource.Rows.Count == 0)
            {
                stsWarning.Text = "No Different!!";
                return;
            }
            dgvData.DataSource  = dtSource;
            dgvData.Show();
            lblRowCount.Text = dtSource.Rows.Count + " records";
        }

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                cmbWerks.SelectedIndex = -1;
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                cmbWerks.Items.Remove("");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                cmbLgort.Items.Clear();
                cmbLgort.Text = "";
                cmbLocat.Items.Clear();
                cmbLocat.Text = "";
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    dtTemp = objAuthority.CheckLgortAuthority(cmbWerks.Items[cmbWerks.SelectedIndex].ToString());
                    if (dtTemp.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("无此厂区仓别权限!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择厂区!");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
       
        private void ShowDdlLocat()
        {
            DataTable dtTemp = new DataTable();
            try
            {

                cmbLocat.Items.Clear();
                cmbLocat.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                {
                    if (cmbLgort.SelectedIndex != -1)
                    {
                        dtTemp = objPlantData.GetAllLocatData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "", "", "", "");
                        if (dtTemp.Rows.Count != 0)
                        {
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                cmbLocat.Items.Add(dtTemp.Rows[i]["LOCAT"].ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("此仓别无储位信息！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择仓别！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请先选择厂区！");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLocat()");
            }
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            stsWarning.Text = "";
            ShowDdlLocat();
        }

        #endregion function
    }
}
