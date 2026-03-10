using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_StorageData;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class Transfer_BindPortInfo : Form
    {
        public Transfer_BindPortInfo()
        {
            InitializeComponent();
        }
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        DataTable dtData = new DataTable();
        DataTable dtTemp = new DataTable();
        ArrayList failTID = new ArrayList();
        private Admin objAdmin;
        private CarData objCarData;
        private Authority objAuthority;
        private PlantData objPlantData;
        string strFWerks = "";
        string strDWerks = "";
        string  strFHouse="";
        string  strDHouse="";
        string strTransferID = "";
        string strReqTime = "";
        string strFPort="";
        string strDPort="";
        string strCartons="";
        string strPallets="";
        

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

        public string StrReqTime
            {
                get {
                    return strReqTime;
                }
                set { strReqTime = value; }
            }

        #endregion

        #region 构造函数
        public Transfer_BindPortInfo(UserInfo varUserData, string varProgid)
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
                objCarData = new CarData (UserData);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else 
                {
                    ShowStatusData(); //秀出Status的資料
                    // 绑定下拉菜单
                    ShowDdlWerks();
                    ShowDdlHouse();
                    ShowDdlPort();
                    Query();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 绑定厂区
        private void ShowDdlWerks()
        {
            try
            {
                cmbFWerks.Items.Clear();
                cmbDWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                DataTable dtDwerks = objPlantData.GetDdlWerksData();//调拨目的厂区不卡账号权限
                for (int i = 0; i < dtDwerks.Rows.Count; i++)
                {
                    cmbDWerks.Items.Add(dtDwerks.Rows[i]["F_TEXT"].ToString());
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
                cmbFHouse.Items.Clear();
                cmbDHouse.Items.Clear();
                dtTemp = objCarData.QueryBuilding("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    cmbDHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 绑定码头

        private void ShowDdlPort()
        {
            cmbFPort.Items.Clear();
            cmbDPort.Items.Clear();
            dtTemp = objCarData.QueryPort("", "");
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbFPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                cmbDPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
            }

        }

        #endregion


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

        #region Query

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        public void Query()
        {
            strTransferID = txtTransferID.Text;
            strCartons = txtCartons.Text;
            strFHouse = cmbFHouse.Text;
            strDHouse = cmbDHouse.Text;
            strPallets = txtPallets.Text;
            strDWerks = cmbDWerks.Text;
            strFWerks = cmbFWerks.Text;
            string hour;
            string minute;
            if (cmbDPort.Text != "" & strFHouse != "")
            {
                strFPort = strFHouse + "_" + cmbFPort.Text;
            }
            if (cmbFPort.Text != "" & strDHouse != "")
            {
                strDPort = strDHouse + "_" + cmbDPort.Text;
            }
            if (cmbHFrom.SelectedIndex == -1 || cmbMFrom.SelectedIndex == -1)
            {
                strReqTime = "";
            }
            else
            {
                hour = cmbHFrom.SelectedItem.ToString();
                minute = cmbMFrom.SelectedItem.ToString();
                strReqTime = dateFrom.Value.ToString("yyyy-MM-dd") + " " + hour + ":" + minute + ":" + "00.000";
            }
          
            dtData = objCarData.QueryBindPortInfo(strTransferID,strFWerks,strDWerks, strFPort, strDPort, strReqTime, strCartons, strPallets);
            lblCount.Text = "共有" + dtData.Rows.Count + "条记录";
            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            if (dtData.Rows.Count <= 0)
            {
                stsWarning.Text = "没有可绑定码头信息的调拨单号，请确认！";
                
            }
            ShowDataGridView();

        }
        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            strCartons = txtCartons.Text;
            strFHouse = cmbFHouse.Text;
            strDHouse = cmbDHouse.Text;
            strDPort = strDHouse + "_" + cmbDPort.Text;
            strFPort = strFHouse + "_" + cmbFPort.Text;
            strPallets = txtPallets.Text;
            strReqTime = "";
            if (cmbHFrom.SelectedIndex != -1 && cmbMFrom.SelectedIndex != -1)
            {
                strReqTime = dateFrom.Value.ToString("yyyy-MM-dd") + " " + cmbHFrom.Text.ToString() + ":" + cmbMFrom.Text.ToString() + ":" + "00.000";
                if (Convert.ToDateTime(strReqTime) <= DateTime.Now)
                {
                    stsWarning.Text = "需求时间应超过当前时间！";
                    return;
                }
            }
           
            if (strCartons == "" || strPallets == "" || strDPort == "" || strFPort == "" || dateFrom.Text == "" || strReqTime== ""||strDHouse==""||strFHouse=="")
            {
                stsWarning.Text = "厂房、起始码头、目的码头、需求时间、箱数、板数不能为空！";
                return;
            }
            
            DataRow[] drSelect = dtData.Select("Select=True AND (STATUS='N' OR STATUS ='WP' )");
            if(drSelect.Length>0)
            {
                string order=objCarData.CreateBindNo();
                DataTable dtResult = new DataTable();
                foreach (DataRow dr in drSelect)
                {
                    strTransferID=dr["MBLNR"].ToString();
                    dtResult = objCarData.UpdateTransferDataForPortInfo(strTransferID, strFPort, strDPort, strCartons, strReqTime, strPallets, order);
                    if(dtResult.Rows[0]["Result"].ToString()=="Success") 
                    {
                        stsWarning.Text="Save OK";
                    }
                    else
                    {
                        failTID.Add(dr["MBLNR"].ToString());
                    }
                    if (failTID.Count != 0)
                    {
                        string sbid = "";
                        foreach (string id in failTID)
                        {
                            sbid += id + ",";
                        }
                        stsWarning.Text = "Save Fail ,调拨单号:" + sbid + "不在该维护状态下";
                    }

                }
            }
            else
            {
                stsWarning.Text = "没有数据需要绑定派车单!";
            }
      
            #region 不用
            //for (int i = 0; i < dtData.Rows.Count; i++)
            //{
            //    try
            //    {
            //         strTransferID = dtData.Rows[i]["MBLNR"].ToString().Trim();
            //        if ((bool)dtData.Rows[i]["Select"] == true)
            //        {
            //            DataTable dtResult = new DataTable();
            //            dtResult = objCarData.UpdateTransferDataForPortInfo(strTransferID, strFPort, strDPort, strCartons, strReqTime, strPallets,order);
            //            if (dtResult.Rows[0]["Result"].ToString() == "Success")
            //            {
            //                stsWarning.Text = "Save OK";
            //            }
            //            else
            //            {
            //                failTID.Add(dtData.Rows[i]["MBLNR"].ToString().Trim());
            //            }
            //        }
            //    }
            //    catch 
            //    {
            //        MessageBox.Show(e+"btnSave");
            //    }
               
            //}
         
            //if (failTID.Count != 0)
            //{
            //    string sbid = "";
            //    foreach (string id in failTID)
            //    {
            //        sbid += id + ",";
            //    }
            //    stsWarning.Text = "Save Fail ,调拨单号:" + sbid + "不在该维护状态下";
            //}

            #endregion
            InitialControl();
            Query();

        }

        #endregion

        #region Refresh

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitialControl();
            ShowDdlPort();
            ShowDdlHouse();
            ShowDdlWerks();
            Query();
        }

        public void InitialControl()
        {
            this.stsWarning.Text = "";
            this.txtCartons.Text = "";
            this.txtPallets.Text = "";
            this.txtTransferID.Text = "";
            this.lblCount.Text = "";
            strFPort = "";
            strDPort = "";
            this.cmbFWerks.SelectedIndex = -1;
            this.cmbDWerks.SelectedIndex = -1;
            this.cmbDPort.SelectedIndex = -1;
            this.cmbFPort.SelectedIndex = -1;
            this.cmbFHouse.SelectedIndex = -1;
            this.cmbDHouse.SelectedIndex = -1;
            this.cmbHFrom.SelectedIndex = -1;
            this.cmbMFrom.SelectedIndex = -1;
            this.dateFrom.Value = DateTime.Now;
            this.cmbDWerks.Enabled = true;
            this.cmbFWerks.Enabled = true;
            

        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                dgvSelect.DataPropertyName = "Select";
                dgvSelect.HeaderText = "选择";
                dgvSelect.ReadOnly = false;
                dgvSelect.Selected = false;
                dgvSelect.Width = 50;
                this.gvData.Columns.Add(dgvSelect);

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "Item";
                dgvcItem.HeaderText = "Item";
                dgvcItem.ReadOnly = true;
                this.gvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcTransferID = new DataGridViewTextBoxColumn();
                dgvcTransferID.DataPropertyName = "MBLNR";
                dgvcTransferID.HeaderText = "调拨单号";
                dgvcTransferID.ReadOnly = true;
                this.gvData.Columns.Add(dgvcTransferID);

                DataGridViewTextBoxColumn dgvcBindNo = new DataGridViewTextBoxColumn();
                dgvcBindNo.DataPropertyName = "BINDNO";
                dgvcBindNo.HeaderText = "绑定单号";
                dgvcBindNo.ReadOnly = true;
                dgvcBindNo.Width = 80;
                this.gvData.Columns.Add(dgvcBindNo);

                
                DataGridViewTextBoxColumn dgvcFwerks = new DataGridViewTextBoxColumn();
                dgvcFwerks.DataPropertyName = "FWERKS";
                dgvcFwerks.HeaderText = "调拨出厂区";
                dgvcFwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFwerks);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "目的厂区";
                dgvcDwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDwerks);

                DataGridViewTextBoxColumn dgvcFPort = new DataGridViewTextBoxColumn();
                dgvcFPort.DataPropertyName = "FPORT";
                dgvcFPort.HeaderText = "起始码头";
                dgvcFPort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFPort);

                DataGridViewTextBoxColumn dgvcDPort = new DataGridViewTextBoxColumn();
                dgvcDPort.DataPropertyName = "DPORT";
                dgvcDPort.HeaderText = "目的码头";
                dgvcDPort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDPort);

                DataGridViewTextBoxColumn dgvcReqTime = new DataGridViewTextBoxColumn();
                dgvcReqTime.DataPropertyName = "REQTIM";
                dgvcReqTime.HeaderText = "需求时间";
                dgvcReqTime.ReadOnly = true;
                this.gvData.Columns.Add(dgvcReqTime);

                DataGridViewTextBoxColumn dgvcCartons= new DataGridViewTextBoxColumn();
                dgvcCartons.DataPropertyName = "CARTONS";
                dgvcCartons.HeaderText = "箱数";
                dgvcCartons.ReadOnly = true;
                dgvcCartons.Width = 60;
                this.gvData.Columns.Add(dgvcCartons);

                DataGridViewTextBoxColumn dgvcPallets= new DataGridViewTextBoxColumn();
                dgvcPallets.DataPropertyName = "PALLETS";
                dgvcPallets.HeaderText = "栈板数";
                dgvcPallets.ReadOnly = true;
                dgvcPallets.Width = 80;
                this.gvData.Columns.Add(dgvcPallets);

                this.gvData.DataSource = dtData;
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
            
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 调整布局大小
        private void Transfer_BindPortInfo_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }

        #endregion


        #region select changed
        private void cmbFWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbFWerks.SelectedIndex != -1)
            {
                strFWerks = cmbFWerks.Items[cmbFWerks.SelectedIndex].ToString();
                dtTemp = objCarData.QueryBuilding(strFWerks);
                cmbFHouse.Items.Clear();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            
           
        }

        private void cmbDWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDWerks.SelectedIndex != -1)
            {
                strDWerks = cmbDWerks.Items[cmbDWerks.SelectedIndex].ToString();
                dtTemp = objCarData.QueryBuilding(strDWerks);
                cmbDHouse.Items.Clear();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbDHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            
        }

        private void cmbFHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFWerks.SelectedIndex != -1)
            {
                strFWerks = cmbFWerks.SelectedItem.ToString();
                if (cmbFHouse.SelectedIndex != -1)
                {
                    strFHouse = cmbFHouse.Text.ToString();
                    dtTemp = objCarData.QueryPort(strFWerks, strFHouse);
                    cmbFPort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbFPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    }
                }
            }
            else
            {
                strFHouse = cmbFHouse.Text.ToString();
                dtTemp = objCarData.QueryPort("", strFHouse);
                cmbFPort.Items.Clear();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
        }

        private void cmbDHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDWerks.SelectedIndex != -1)
            {
                strDWerks = cmbDWerks.SelectedItem.ToString();
                if (cmbDHouse.SelectedIndex != -1)
                {
                    strDHouse = cmbDHouse.SelectedItem.ToString();
                    dtTemp = objCarData.QueryPort(strDWerks, strDHouse);
                    cmbDPort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbDPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    }
                }
            }
            else
            {
                strDHouse = cmbDHouse.Text.ToString();
                dtTemp = objCarData.QueryPort("", strDHouse);
                cmbDPort.Items.Clear();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbDPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
        }

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow[] drDelete  = dtData.Select("Select =True");
            ArrayList alDelMblnr = new ArrayList();
            foreach(DataRow dr in drDelete )
            {
                if (dr["STATUS"].ToString() == "WP")
                {
                    alDelMblnr.Add(dr["MBLNR"].ToString().Trim());
                }
                else
                {
                    failTID.Add(dr["MBLNR"].ToString().Trim());
                }
            }
            if (alDelMblnr.Count > 0)
            {
                string sbid = "''";
                foreach (string id in alDelMblnr)
                {
                    sbid += ",'"+id+"'" ;
                }
                bool flg = objCarData.DeleteTransferDataForPortInfo(sbid);
                if (flg)
                {
                    stsWarning.Text = "Delete OK";
                }
            }
            if (failTID.Count > 0)
            {
                string sbid = "";
                foreach (string id in failTID)
                {
                    sbid += id + ",";
                }
                stsWarning.Text = "Save Fail ,调拨单号:" + sbid + "不在该维护状态下";
            }
            InitialControl();
            Query();
            //dtData = objCarData.QueryBindPortInfo("", "","","", "", "", "", "");
            //ShowDataGridView();
         //  InitialControl();
        
        }

    }
}
