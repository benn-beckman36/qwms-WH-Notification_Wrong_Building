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
    public partial class Manage_StorageStatus_SetupRowCloumn : Form
    {

        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strStset = "";
        private string strSthgh = "";
        private string strRctyp = "";
        private int intRcnum = 0;
        private string strRcnam = "";
        private DataTable dtStorageData = new DataTable();
        private DataTable dtTemp = new DataTable();
        private PlantData objPlantData;

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

        public string Rctyp
        {
            get
            {
                return strRctyp;
            }
            set
            {
                strRctyp = value;
            }
        }

        public int Rcnum
        {
            get
            {
                return intRcnum;
            }
            set
            {
                intRcnum = value;
            }
        }

        public string Rcnam
        {
            get
            {
                return this.txtRcnam.Text.Trim();
            }
            set
            {
                this.txtRcnam.Text = value;
            }
        }
        #endregion

        #region 构造函数
        public Manage_StorageStatus_SetupRowCloumn()
        {
            InitializeComponent();
        }

        public Manage_StorageStatus_SetupRowCloumn(UserInfo varUserData, string strWerks, string strLgort, string strStset, string strSthgh, string strRctyp, int intRcnum)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Werks = strWerks;
            Lgort = strLgort;
            Stset = strStset;
            Sthgh = strSthgh;
            Rctyp = strRctyp;
            Rcnum = intRcnum;

            try
            {

                objPlantData = new PlantData(UserData);

                ShowData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (objPlantData.UpdateRowColName(Werks, Lgort, Stset, Sthgh, Rctyp, Rcnum, this.txtRcnam.Text))
                {
                    MessageBox.Show("Update OK!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update Fail!" + objPlantData.ERRMSG);
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

        #region ShowData
        private void ShowData()
        {
            try
            {
                this.txtRcnam.Text = objPlantData.QueryRowColName(Werks, Lgort, Stset, Sthgh, Rctyp, Rcnum);
                if (this.txtRcnam.Text == "")
                {
                    this.txtRcnam.Text = Rcnum.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
