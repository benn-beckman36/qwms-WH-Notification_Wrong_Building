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
//using QCI_QWMS_Transfer;
using System.Timers;

namespace QWMS
{
    public partial class Transfer_Query : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
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

        private string strTransferID = "";
        public string StrTransferID
        {
            get { return strTransferID; }
            set { strTransferID = value; }
        }

        private string strSendTimeF = "";
        public string StrSendTimeF
        {
            get { return strSendTimeF; }
            set { strSendTimeF = value; }
        }

        private string strSendTimeT = "";
        public string StrSendTimeT
        {
            get { return strSendTimeT; }
            set { strSendTimeT = value; }
        }

        private string strTruckOrder = "";
        public string StrTruckOrder
        {
            get { return strTruckOrder; }
            set { strTruckOrder = value; }
        }

        private string strType = "";
        public string StrType
        {
            get { return strType; }
            set { strType = value; }
        }

        private string strFWerks = "";
        public string StrFWerks
        {
            get { return strFWerks; }
            set { strFWerks = value; }
        }
        string strFHouse="";
        private string strFport = "";
        public string StrFport
        {
            get { return strFport; }
            set { strFport = value; }
        }

        private Admin objAdmin;
        private CarData objCarData;
        private PlantData objPlantData;
        Authority objAuthority;
        DataTable dtData = new DataTable();
        DataTable dtTemp = new DataTable();
        string strCarNo = "";

        #endregion

        #region 构造函数
        public Transfer_Query(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            #region 初始化
            try
            {
                 objAdmin = new Admin(UserData, Progid);
                objCarData = new CarData (UserData);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);
                //檢查權限
                QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowDdlPort();
                    ShowDdlHouse();
                    ShowDdlWerks();
                    ShowDdlCarNo();
                    ShowStatusData();
                    Query();
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
#endregion
        }
#endregion

        #region 绑定厂区
        private void ShowDdlWerks()
        {
            try
            {
                cmbFWerks.Items.Clear();

                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
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
                dtTemp = objCarData.QueryBuilding("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbFHouse.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 绑定码头
        public void ShowDdlPort()
        {
            try
            {
                dtTemp = objCarData.QueryPort("", "");
                cmbFPort.Items.Clear();
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbFPort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlPort()");
            }
        }

#endregion

        #region 绑定车牌号
        private void ShowDdlCarNo()
        {
            try
            {
                cmbCarNo.Items.Clear();
                dtTemp = objCarData.GetCarData();
            
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbCarNo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message );
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

        #region 查询

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        public void Query()
        {
            stsWarning.Text = "";
            strTransferID = txtTransferID.Text.Trim();
            strTruckOrder = txtTruckOrder.Text.Trim();
            strSendTimeF = "";
            strSendTimeT = "";
            strFWerks = cmbFWerks.Text;
            strFport = "";
            strCarNo = cmbCarNo.Text;
            
            if (cmbHFrom.SelectedIndex != -1 && cmbMFrom.SelectedIndex != -1)
            {
                strSendTimeF = dateFrom.Value.ToString("yyyy-MM-dd") + " " + cmbHFrom.SelectedItem.ToString() + ":" + cmbMFrom.SelectedItem.ToString() + ":00.000";
            }
            else
            {
                strSendTimeF = dateFrom.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            }
            if (cmbHTo.SelectedIndex != -1 && cmbMTo.SelectedIndex != -1)
            {
                strSendTimeT = dateTo.Value.ToString("yyyy-MM-dd") + " " + cmbHTo.SelectedItem.ToString() + ":" + cmbMTo.SelectedItem.ToString() + ":00.000";
            }
            else
            {
                strSendTimeT = dateTo.Value.ToString("yyyy-MM-dd") + " 23:59:59.999";
            }
            if (cmbFHouse.SelectedIndex != -1 && cmbFPort.SelectedIndex != -1)
            {
                strFport = cmbFHouse.Text + "_" + cmbFPort.Text;
            }
          

            dtData = objCarData.QueryTransfer(strTruckOrder, strTransferID, strFWerks, strFport, strSendTimeF, strSendTimeT,strCarNo,CHKDATE.Checked);
            ShowDataGridView();
        }

        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
           
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcPO00 = new DataGridViewTextBoxColumn();
                dgvcPO00.DataPropertyName = "PO";
                dgvcPO00.HeaderText = "PO单号";
                dgvcPO00.ReadOnly = true;
                dgvcPO00.Width = 80;
                this.gvData.Columns.Add(dgvcPO00);

                DataGridViewTextBoxColumn dgvcTransferID01 = new DataGridViewTextBoxColumn();
                dgvcTransferID01.DataPropertyName = "MBLNR";
                dgvcTransferID01.HeaderText = "调拨单号";
                dgvcTransferID01.ReadOnly = true;
                dgvcTransferID01.Width = 85;
                this.gvData.Columns.Add(dgvcTransferID01);

                DataGridViewTextBoxColumn dgvcOrder02 = new DataGridViewTextBoxColumn();
                dgvcOrder02.DataPropertyName = "ORDERNO";
                dgvcOrder02.HeaderText = "派车单号";
                dgvcOrder02.ReadOnly = true;
                dgvcOrder02.Width = 85;
                this.gvData.Columns.Add(dgvcOrder02);

                DataGridViewTextBoxColumn dgvcCarNo03 = new DataGridViewTextBoxColumn();
                dgvcCarNo03.DataPropertyName = "CARNO";
                dgvcCarNo03.HeaderText = "车牌号";
                dgvcCarNo03.ReadOnly = true;
                dgvcCarNo03.Width = 78;
                this.gvData.Columns.Add(dgvcCarNo03);

                DataGridViewTextBoxColumn dgvcFlogrt04 = new DataGridViewTextBoxColumn();
                dgvcFlogrt04.DataPropertyName = "FLGORT";
                dgvcFlogrt04.HeaderText = "调出仓别";
                dgvcFlogrt04.ReadOnly = true;
                dgvcFlogrt04.Width = 82;
                this.gvData.Columns.Add(dgvcFlogrt04);

                DataGridViewTextBoxColumn dgvcFPort05 = new DataGridViewTextBoxColumn();
                dgvcFPort05.DataPropertyName = "FPORT";
                dgvcFPort05.HeaderText = "调出码头";
                dgvcFPort05.ReadOnly = true;
                dgvcFPort05.Width = 82;
                this.gvData.Columns.Add(dgvcFPort05);

                DataGridViewTextBoxColumn dgvcDPort09 = new DataGridViewTextBoxColumn();
                dgvcDPort09.DataPropertyName = "DPORT";
                dgvcDPort09.HeaderText = "调入码头";
                dgvcDPort09.ReadOnly = true;
                dgvcDPort09.Width = 82;
                this.gvData.Columns.Add(dgvcDPort09);

                DataGridViewTextBoxColumn dgvcPrint06 = new DataGridViewTextBoxColumn();
                dgvcPrint06.DataPropertyName = "CSPRINT";
                dgvcPrint06.HeaderText = "是否打印";
                dgvcPrint06.ReadOnly = true;
                dgvcPrint06.Width = 82;
                this.gvData.Columns.Add(dgvcPrint06);

                DataGridViewTextBoxColumn dgvcWHLeft07= new DataGridViewTextBoxColumn();
                dgvcWHLeft07.DataPropertyName = "CSLEAVE";
                dgvcWHLeft07.HeaderText = "离厂仓库确认";
                dgvcWHLeft07.ReadOnly = true;
                dgvcWHLeft07.Width = 108;
                this.gvData.Columns.Add(dgvcWHLeft07);

                DataGridViewTextBoxColumn dgvcJWLeft08 = new DataGridViewTextBoxColumn();
                dgvcJWLeft08.DataPropertyName = "CJLEAVE";
                dgvcJWLeft08.HeaderText = "离厂警卫确认";
                dgvcJWLeft08.ReadOnly = true;
                dgvcJWLeft08.Width = 110;
                this.gvData.Columns.Add(dgvcJWLeft08);


                DataGridViewTextBoxColumn dgvcJWIn10 = new DataGridViewTextBoxColumn();
                dgvcJWIn10.DataPropertyName = "CJRECEIVE";
                dgvcJWIn10.HeaderText = "入厂警卫确认";
                dgvcJWIn10.ReadOnly = true;
                dgvcJWIn10.Width = 109;
                this.gvData.Columns.Add(dgvcJWIn10);

                DataGridViewTextBoxColumn dgvDPort11 = new DataGridViewTextBoxColumn();
                dgvDPort11.DataPropertyName = "CSRECEIVE";
                dgvDPort11.HeaderText = "入厂仓库确认";
                dgvDPort11.ReadOnly = true;
                dgvDPort11.Width = 110;
                this.gvData.Columns.Add(dgvDPort11);

                DataGridViewTextBoxColumn dgvWHConfirm12 = new DataGridViewTextBoxColumn();
                dgvWHConfirm12.DataPropertyName = "CSCONFIRM";
                dgvWHConfirm12.HeaderText = "调拨入库";
                dgvWHConfirm12.ReadOnly = true;
                dgvWHConfirm12.Width = 82;
                this.gvData.Columns.Add(dgvWHConfirm12);


                DataGridViewTextBoxColumn dgvDLgort13= new DataGridViewTextBoxColumn();
                dgvDLgort13.DataPropertyName = "DLGORT";
                dgvDLgort13.HeaderText = "入账仓别";
                dgvDLgort13.ReadOnly = true;
                dgvDLgort13.Width = 82;
                this.gvData.Columns.Add(dgvDLgort13);


                DataGridViewTextBoxColumn dgvJWdate14 = new DataGridViewTextBoxColumn();
                dgvJWdate14.DataPropertyName = "CJWdate";
                dgvJWdate14.HeaderText = "警卫是否超时";
                dgvJWdate14.ReadOnly = true;
                dgvJWdate14.Visible = false;
                this.gvData.Columns.Add(dgvJWdate14);

                DataGridViewTextBoxColumn dgvWHdate15 = new DataGridViewTextBoxColumn();
                dgvWHdate15.DataPropertyName = "CWHdate";
                dgvWHdate15.HeaderText = "仓库是否超时";
                dgvWHdate15.ReadOnly = true;
                dgvWHdate15.Visible = false;
                this.gvData.Columns.Add(dgvWHdate15);

                
            try
            {
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

        #region 刷新

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            txtTransferID.Text="";
            txtTruckOrder.Text="";
            dateFrom.Value = DateTime.Now;
            cmbHFrom.SelectedIndex = -1;
            cmbHTo.SelectedIndex = -1;
            cmbMFrom.SelectedIndex = -1;
            cmbMTo.SelectedIndex = -1;
            cmbFWerks.SelectedIndex = -1;
            cmbFHouse.SelectedIndex = -1;
            cmbFPort.Text = "";
            cmbFPort.SelectedIndex = -1;
            cmbCarNo.Text = "";
            cmbCarNo.SelectedIndex = -1;
            ShowDdlPort();
            ShowDdlHouse();
            ShowDdlCarNo();
            Query();
        }


        #endregion

        #region Select Changed

        private void cmbFWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-QueryBuilding()");
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

        #endregion

        #region 调整窗口大小
        private void Transfer_Query_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }

        #endregion

        #region DataGridView样式

        private void gvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    gvData.ClearSelection();
                    gvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    gvData.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (gvData.Rows[i].Cells[14].Value.ToString() == "N" || gvData.Rows[i].Cells[15].Value.ToString() == "N")
                    {
                        gvData.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    else
                    {
                        if (gvData.Rows[i].Cells[9].Value.ToString() != "N")
                        {
                            gvData[9, i].Style.BackColor = Color.LightGreen;
                        }
                        if (gvData.Rows[i].Cells[7].Value.ToString() != "N")
                        {
                            gvData[7, i].Style.BackColor = Color.LightGreen;
                        }
                        if (gvData.Rows[i].Cells[8].Value.ToString() != "N")
                        {
                            gvData[8, i].Style.BackColor = Color.LightGreen;
                        }
                        if (gvData.Rows[i].Cells[10].Value.ToString() != "N")
                        {
                            gvData[10, i].Style.BackColor = Color.LightGreen;
                        }
                        if (gvData.Rows[i].Cells[11].Value.ToString() != "N")
                        {
                            gvData[11, i].Style.BackColor = Color.LightGreen;
                        }
                        if (gvData.Rows[i].Cells[12].Value.ToString() != "N")
                        {
                            gvData[12, i].Style.BackColor = Color.LightGreen;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
