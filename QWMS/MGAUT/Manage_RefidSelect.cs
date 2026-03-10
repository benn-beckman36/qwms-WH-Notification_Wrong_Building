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
    public partial class Manage_RefidSelect : Form
    {
       
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strRefid = "";
        private DataTable dtData = new DataTable();
        private PlantData objPlantData;
        private StorageIn objStorageIn;
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

        public string Refid
        {
            get
            {
                return strRefid;
            }
            set
            {
                strRefid = value;
            }
        }
        #endregion

        #region 构造函数
        public Manage_RefidSelect()
        {
            InitializeComponent();
        }

        public Manage_RefidSelect(UserInfo varUserData, string strProgid, string strWerks, string strLgort)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Werks = strWerks;
            Lgort = strLgort;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objStorageIn = new StorageIn(UserData, Progid);

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowLocationData();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region ShowLocationData
        private void ShowLocationData()
        {
            try
            {
                dtData = objPlantData.GetAllRefID(Werks, Lgort);
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocationData()");
            }

        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {


                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Refid";
                dgvcRefid.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRefid);

                this.gvData.DataSource = dtData;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Selete
        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            if (Refid == "")
            {
                MessageBox.Show("Please select one Reference ID!!");
                return;
            }
            this.Close();
        }
        #endregion

        #region Return
        private void btnReturn_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  gvData Mouse Down Event
        private void gvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                DataGridView dgClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    this.btnSelect.Enabled = true;
                    intRowNo = hitRow.RowIndex;
                    Refid = this.gvData.Rows[intRowNo].Cells[0].Value.ToString();
                }
                else
                {
                    this.btnSelect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion


    }
}
