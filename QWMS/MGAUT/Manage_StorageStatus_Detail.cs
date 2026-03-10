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
    public partial class Manage_StorageStatus_Detail : Form
    {


        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strStset = "";
        private string strSthgh = "";
        private string strStlen = "";
        private string strStwid = "";
        private string strType = "NEW";
        private DataTable dtStorageData = new DataTable();
        private DataTable dtTemp = new DataTable();
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private Admin objAdmin;

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

        public string Locat
        {
            get
            {
                return txtLocat.Text.Trim();
            }
            set
            {
                txtLocat.Text = value;
            }
        }

        public string Menge1
        {
            get
            {
                return txtMenge1.Text.Trim();
            }
            set
            {
                txtMenge1.Text = value;
            }
        }

        public string Menge2
        {
            get
            {
                return txtMenge2.Text.Trim();
            }
            set
            {
                txtMenge2.Text = value;
            }
        }

        public string Menge
        {
            get
            {
                return txtMenge.Text.Trim();
            }
            set
            {
                txtMenge.Text = value;
            }
        }

        public string Stset
        {
            get
            {
                return strStset;
            }
            set
            {
                strStset = value;
            }
        }

        public string Sthgh
        {
            get
            {
                return strSthgh;
            }
            set
            {
                strSthgh = value;
            }
        }

        public string Stlen
        {
            get
            {
                return strStlen;
            }
            set
            {
                strStlen = value;
            }
        }

        public string Stwid
        {
            get
            {
                return strStwid;
            }
            set
            {
                strStwid = value;
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

        #endregion

        #region 构造函数
        public Manage_StorageStatus_Detail()
        {
            InitializeComponent();
        }
        public Manage_StorageStatus_Detail(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strSttyp, string strLotyp, string strStset, string strSthgh, string strStlen, string strStwid, string strSourceType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Sttyp = strSttyp;
            Lotyp = strLotyp;
            Stset = strStset;
            Sthgh = strSthgh;
            Stlen = strStlen;
            Stwid = strStwid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Progid);

                if (strSourceType.ToUpper() == "ADMIN")
                {
                    if (!objAdmin.CheckAuthority())
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }
                else if (strSourceType.ToUpper() == "MANAGE")
                {
                    if (!objStorageIn.CheckAuthority("MANAGE"))
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }

                if (Stset != "" && Sthgh != "" && Stlen != "" && Stwid != "")
                {
                    ShowData();

                }

                if (Locat != "")
                {
                    btnConfirm.Enabled = false;

                    if (Sttyp.ToUpper() == "TRADITIONAL")
                    {
                        btnRelease.Enabled = true;
                    }
                    else
                    {
                        btnRelease.Enabled = false;
                    }
                }
                else
                {
                    txtLocat.Enabled = true;
                    btnConfirm.Enabled = true;
                    txtMenge1.Text = "0";
                    txtMenge2.Text = "0";
                    txtMenge.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

	
        #endregion

        #region ShowData
        private void ShowData()
        {
            try
            {
                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);
                dtTemp = objStorageData.QueryStorageDetailData(Locat, Stset, Sthgh, Stlen, Stwid);
                if (dtTemp.Rows.Count > 0)
                {
                    txtLocat.Text = dtTemp.Rows[0]["LOCAT"].ToString();
                    txtMenge1.Text = dtTemp.Rows[0]["MENGE1"].ToString();
                    txtMenge2.Text = dtTemp.Rows[0]["MENGE2"].ToString();
                    txtMenge.Text = dtTemp.Rows[0]["MENGE"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
			{
				DataTable dtTemp = new DataTable();
				if(Locat == "")
				{
					MessageBox.Show("Location can't be empty!!");
					return;
				}

				if(!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
				{
					DialogResult dr = MessageBox.Show("The location doesn't exist in the system. Do you want to create it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					if(dr == DialogResult.Yes)
					{
		
						if(objAdmin.AddLocation(Werks, Lgort, Locat, Stset, Sthgh, Stlen, Stwid,"",""))
						{
							MessageBox.Show("Add OK!");
							Type = "RELOAD";
							this.Close();
							return;
						}
						else
						{
							MessageBox.Show( "Update Fail!" + objAdmin.ERRMSG);
							return;
						}
					}
					else
					{
						return;
					}
				}

				dtTemp = objPlantData.GetLocatData(Werks, Lgort, Locat);
				if(dtTemp.Rows.Count == 0)
				{
					MessageBox.Show("Position of this location had be assigned!!");
					return;
				}

			
				if(objAdmin.ModifyLocation(Werks, Lgort, Locat, Stset, Sthgh, Stlen, Stwid, "UPDATE"))
				{
					MessageBox.Show("Update OK!");
					Type = "RELOAD";	
					this.Close();
				}
				else
				{
					MessageBox.Show( "Update Fail!" + objAdmin.ERRMSG);
					return;
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region Release
        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                {
                    MessageBox.Show("The location doesn't exist in the system!!");
                    return;
                }
              
                if (objAdmin.ModifyLocation(Werks, Lgort, Locat, "", "", "", "", "RELEASE"))
                {
                    MessageBox.Show("Update OK!");
                    Type = "RELOAD";	 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update Fail!" + objAdmin.ERRMSG);
                    return;
                }
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

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "MAP");
                objManage_LocationSelect.ShowDialog();
                txtLocat.Text = objManage_LocationSelect.Locat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }



    }
}
