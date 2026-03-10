using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using QCI_QWMS_StorageData;

namespace QWMS
{
    public partial class Admin_PortAdd : Form
    {

        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strType = "";
        private string strPort = "";
        private string strHouse = "";

     
        private DataTable dtData = new DataTable();
        private Admin objAdmin;
        private CarData objCarData;
     //   CarData objCarData = new CarData();
        private PlantData objPlantData;
        private Authority objAuthority;
        public int intRowNo;

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

        public string StrHouse
        {
            get { return strHouse; }
            set { strHouse = value; }
        }

        public string StrPort
        {
            get { return strPort; }
            set { strPort = value; }
        }

        #endregion

        #region 构造函数
        public Admin_PortAdd(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objCarData = new CarData(UserData);
                objAuthority = new Authority(UserData);
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    // 绑定下拉菜单
                    ShowDdlWerks();
                    ShowDdlHouse();
                    dtData = objCarData.QueryPort("","");
                    ShowDataGridView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        # endregion

        #region 绑定厂区
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 绑定厂房
        private void ShowDdlHouse()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbHouse.Items.Clear();
                dtTemp = objCarData.QueryBuilding("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region ShowDataGridView
        public void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "CTRLNM";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcHouse = new DataGridViewTextBoxColumn();
                dgvcHouse.DataPropertyName = "CTRLC1";
                dgvcHouse.HeaderText = "厂房";
                dgvcHouse.ReadOnly = true;
                this.gvData.Columns.Add(dgvcHouse);

                DataGridViewTextBoxColumn dgvcPort = new DataGridViewTextBoxColumn();
                dgvcPort.DataPropertyName = "F_TEXT";
                dgvcPort.HeaderText = "码头";
                dgvcPort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcPort);

                this.gvData.DataSource = dtData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }

        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            strType = "";
            this.txtPort.Text = "";
            this.cmbWerks.SelectedIndex = -1;
            this.cmbHouse.SelectedIndex = -1;
            this.cmbHouse.Text = "";
            this.btnConfirm.Enabled = true;
            this.gvData.Enabled = true;
            this.rdoAdd.Checked = false;
            this.rdoDelete.Checked = false;
            this.gbFunction.Enabled = true;
            dtData = objCarData.QueryPort("", "");
            ShowDataGridView();
            }

        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strWerks = cmbWerks.Text.ToString();
            strHouse = cmbHouse.Text.ToString();
            strPort=txtPort.Text;
            bool flg;
            try
            {
                if (strType == "")
                {
                    dtData = objCarData.QueryPort("","");
                    ShowDataGridView();
                    return;
                }
               if (strType == "DELETE")
                {
                    if (strPort == "")
                    {
                        stsWarning.Text = "Please select one data !";
                        return;
                    }
                    else
                    {
                        flg = objCarData.DeletePort(strWerks, strHouse, strPort);
                        if (flg)
                        {
                            stsWarning.Text = " Delete Success !";
                        }
                        else
                        {
                            stsWarning.Text = " Delete Fail !";
                        }
                    }
                }
                if (strType == "ADD")
                {
                    if (objCarData.CheckPort(strWerks, strHouse, strPort))
                    {
                        if (strWerks == "" || strHouse == "" || strPort == "")
                        {
                            stsWarning.Text = "厂区、厂房、码头不能为空！";
                            return;
                        }
                        if (strPort.Length > 2)
                        {
                            stsWarning.Text = "码头编号请输入0-99有效数字！";
                            return;
                        }
                        else
                        {
                            flg = objCarData.AddPort(strWerks, strHouse, strPort);
                        }
                        if (flg)
                        {
                            stsWarning.Text = " Add Success !";
                        }
                        else
                        {
                            stsWarning.Text = " Add  Fail !";
                        }
                    }
                    else
                    {
                        stsWarning.Text = "该码头信息已存在！";
                    }
                }
                
                dtData = objCarData.QueryPort("","");
                ShowDataGridView();
                this.txtPort.Text = "";
                this.cmbWerks.SelectedIndex = -1;
                this.cmbHouse.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion

        #region Radio Changed
        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.txtPort.Enabled  = true;
            this.btnConfirm.Enabled = true;
            this.gvData.Enabled = true;
            this.strType = "DELETE";
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.txtPort.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.gvData.Enabled = true;
            this.strType = "ADD";

        }
        #endregion

        #region  gvData_MouseDown

        private void gvData_MouseDown(object sender, MouseEventArgs e)
        {
            stsWarning.Text = "";
            DataGridView dgvClick = (DataGridView)sender;
            DataGridView.HitTestInfo hitRow;
            hitRow = dgvClick.HitTest(e.X, e.Y);
            intRowNo = hitRow.RowIndex;
            if (strType == "DELETE")
            {
                txtPort.Text = dtData.Rows[intRowNo]["F_TEXT"].ToString();
                cmbWerks.Text = dtData.Rows[intRowNo]["CTRLNM"].ToString();
                cmbHouse.Text = dtData.Rows[intRowNo]["CTRLC1"].ToString();
            }
        }
        #endregion 

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString(); 
            }
            DataTable dtTemp = new DataTable();
            cmbHouse.Items.Clear();
            cmbHouse.Text = "";
            txtPort.Text = "";
            dtTemp = objCarData.QueryBuilding(strWerks);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
            }

            dtData = objCarData.QueryPort(strWerks, "");
            ShowDataGridView();
        }

        private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPort.Text = "";
            if (cmbHouse.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strHouse = cmbHouse.Items[cmbHouse.SelectedIndex].ToString();
            }
            dtData = objCarData.QueryPort(strWerks,strHouse);
            ShowDataGridView();
        }


      
       
    }
}
