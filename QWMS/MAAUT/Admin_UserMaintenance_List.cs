using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;


namespace QWMS
{
    public partial class Admin_UserMaintenance_List : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strAccount = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtData1 = new DataTable();
        private Admin objAdmin;
        //private AccessConfig objConfig;
        private Authority objAuthority;

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

        public string Account
        {
            get
            {
                return strAccount;
            }
            set
            {
                strAccount = value;
            }
        }
        # endregion


        public Admin_UserMaintenance_List()
        {
            InitializeComponent();
           
        }

        public Admin_UserMaintenance_List(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objAuthority = new Authority(UserData);

                //判断是否有权限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowUserData();
                    dgvUserList.AutoGenerateColumns = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ShowUserData()
        {
            string strTempWerksLgort = "";
            try
            {
                dtData = objAuthority.QueryUserData();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strTempWerksLgort = "";
                    dtData1 = objAdmin.QueryUserPlantStorageData(dtData.Rows[i]["USRNM"].ToString());
                    for (int j = 0; j < dtData1.Rows.Count; j++)
                    {
                        strTempWerksLgort += dtData1.Rows[j]["WERKS"].ToString() + "-" + dtData1.Rows[j]["LGORT"].ToString() + ";";
                    }
                    dtData.Rows[i]["WKAUT"] = strTempWerksLgort;
                }

                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowUserData()");
            }

        }

        private void ShowDataGrid()
        {
            try
            {
                dgvUserList.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (Account == "")
            {
                MessageBox.Show("Please select one user!!");
                return;
            }
            this.Close();

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Account = "";
            this.Close();
        }



        private void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                this.btnSelect.Enabled = true;
                int i = e.RowIndex;
                Account = dgvUserList.Rows[e.RowIndex].Cells["User"].Value.ToString();
            }
            else
            {
                this.btnSelect.Enabled = false;
                Account = "";
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.GetEncoding(936));
                strLine = "User\tPlant Authority\tG/R Authority\tG/I Authority\tSystem Maintenance\tManagement\tPhysical Counting";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["USRNM"].ToString() + "\t";
                    strLine += dtData.Rows[i]["WKAUT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["INAUT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["OTAUT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MAAUT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MGAUT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["IVAUT"].ToString();
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }

       
    }
}
