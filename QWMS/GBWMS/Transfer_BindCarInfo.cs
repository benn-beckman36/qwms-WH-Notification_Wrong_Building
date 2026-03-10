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
using System.IO;
using QCI_QWMS_StorageData;
using System.Collections;
using Microsoft.VisualBasic;


namespace QWMS
{
    public partial class Transfer_BindCarInfo : Form
    {
        public Transfer_BindCarInfo()
        {
            InitializeComponent();
        }

         #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strDriver = ""; 
        private string strUsrnm = "";
        private string strProgid = "";

        private string strTransferID = "";
        public string StrTransferID
        {
            get { return strTransferID; }
            set { strTransferID = value; }
        }

        private string strSealNo = "";
        public string StrSealNo
        {
            get { return strSealNo; }
            set { strSealNo = value; }
        }

        private string strSendTime = "";
        public string StrSendTime
        {
            get { return strSendTime; }
            set { strSendTime = value; }
        }

        private string strCarNo = "";
        public string StrCarNo
        {
            get { return strCarNo; }
            set { strCarNo = value; }
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

        private string strDport = "";
        public string StrDport
        {
            get { return strDport; }
            set { strDport = value; }
        }

        private string strFport = "";
        public string StrFport
        {
            get { return strFport; }
            set { strFport = value; }
        }

        
        

        private Admin objAdmin;
        private CarData objCarData;
        private Authority objAuthority;
        DataTable dtData = new DataTable();
        DataTable dtResult = new DataTable();
        

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
        public string StrDriver
        {
            get { return strDriver; }
            set { strDriver = value; }
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

    
        #endregion
       
        #region 构造函数
        public Transfer_BindCarInfo(UserInfo varUserData, string varProgid)
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
                QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else 
                {
                    ShowStatusData();//秀出Status的資料
                    ShowDdlCarNo();  // 绑定下拉菜单
                    Query();
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

        #region 绑定车牌号
        private void ShowDdlCarNo()
		{
			try
			{
				DataTable dtTemp = new DataTable();
                dtTemp = objCarData.GetCarData();
                cmbCarNo.Items.Clear();
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
                    cmbCarNo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLotyp()");
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

                DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                dgvSelect.DataPropertyName = "Select";
                dgvSelect.HeaderText = "选择";
                dgvSelect.ReadOnly = false;
                dgvSelect.Selected = false;
                dgvSelect.Width = 50;
                this.gvData.Columns.Add(dgvSelect);

                DataGridViewTextBoxColumn dgvcTruckOrder = new DataGridViewTextBoxColumn();
                dgvcTruckOrder.DataPropertyName = "ORDERNO";
                dgvcTruckOrder.HeaderText = "派车单号";
                dgvcTruckOrder.ReadOnly = true;
                dgvcTruckOrder.Width = 120;
                this.gvData.Columns.Add(dgvcTruckOrder);

                DataGridViewTextBoxColumn dgvcTransferID = new DataGridViewTextBoxColumn();
                dgvcTransferID.DataPropertyName = "MBLNR";
                dgvcTransferID.HeaderText = "调拨单号";
                dgvcTransferID.ReadOnly = true;
                dgvcTransferID.Width = 120;
                this.gvData.Columns.Add(dgvcTransferID);

                DataGridViewTextBoxColumn dgvcBindNo= new DataGridViewTextBoxColumn();
                dgvcBindNo.DataPropertyName = "BINDNO";
                dgvcBindNo.HeaderText = "绑定单号";
                dgvcBindNo.ReadOnly = true;
                dgvcBindNo.Width = 120;
                this.gvData.Columns.Add(dgvcBindNo);

                DataGridViewTextBoxColumn dgvcSealNo= new DataGridViewTextBoxColumn();
                dgvcSealNo.DataPropertyName = "SEALNO";
                dgvcSealNo.HeaderText = "封条号";
                dgvcSealNo.ReadOnly = true;
                dgvcSealNo.Width = 100;
                this.gvData.Columns.Add(dgvcSealNo);

                DataGridViewTextBoxColumn dgvcFlgort = new DataGridViewTextBoxColumn();
                dgvcFlgort.DataPropertyName = "FLGORT";
                dgvcFlgort.HeaderText = "起始仓别";
                dgvcFlgort.ReadOnly = true;
                dgvcFlgort.Width = 100;
                this.gvData.Columns.Add(dgvcFlgort);

                DataGridViewTextBoxColumn dgvcFport = new DataGridViewTextBoxColumn();
                dgvcFport.DataPropertyName = "FPORT";
                dgvcFport.HeaderText = "起始码头";
                dgvcFport.ReadOnly = true;
                dgvcFport.Width = 100;
                this.gvData.Columns.Add(dgvcFport);

                DataGridViewTextBoxColumn dgvcDport = new DataGridViewTextBoxColumn();
                dgvcDport.DataPropertyName = "DPORT";
                dgvcDport.HeaderText = "目的码头";
                dgvcDport.ReadOnly = true;
                dgvcDport.Width = 100;
                this.gvData.Columns.Add(dgvcDport);
              

                DataGridViewTextBoxColumn dgvcCarNo = new DataGridViewTextBoxColumn();
                dgvcCarNo.DataPropertyName = "CARNO";
                dgvcCarNo.HeaderText = "车牌号";
                dgvcCarNo.ReadOnly = true;
                dgvcCarNo.Width = 100;
                this.gvData.Columns.Add(dgvcCarNo);

                DataGridViewTextBoxColumn dgvcDriver= new DataGridViewTextBoxColumn();
                dgvcDriver.DataPropertyName = "DIRVER";
                dgvcDriver.HeaderText = "司机名称";
                dgvcDriver.ReadOnly = true;
                dgvcDriver.Width = 100;
                this.gvData.Columns.Add(dgvcDriver);

                DataGridViewTextBoxColumn dgvcReqTime = new DataGridViewTextBoxColumn();
                dgvcReqTime.DataPropertyName = "REQTIM";
                dgvcReqTime.HeaderText = "需求发车时间";
                dgvcReqTime.ReadOnly = true;
                dgvcReqTime.Width = 150;
                this.gvData.Columns.Add(dgvcReqTime);


                DataGridViewTextBoxColumn dgvcSendTime = new DataGridViewTextBoxColumn();
                dgvcSendTime.DataPropertyName = "SEDTIM";
                dgvcSendTime.HeaderText = "预计发车时间";
                dgvcSendTime.ReadOnly = true;
                dgvcSendTime.Width = 150;
                this.gvData.Columns.Add(dgvcSendTime);

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

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = true;
            panel2.Enabled = true;
            InitialControl();
            Query();
        }

        public void InitialControl()
        {
           this.stsWarning.Text = "";
            this.cmbCarNo.Text = "";
            this.txtSealNo.Text = string.Empty;
            this.txtSealNo.Enabled = true;
            this.txtTruckOrder.Text = "";
            this.txtTransferID.Text = string.Empty;
            this.lblCount.Text = "";
            this.cmbCarNo.SelectedIndex = -1;
            this.cmbCarNo.Enabled = true;
            this.cmbDriver.Enabled = true;
            this.cmbDriver.SelectedIndex = -1;
            this.cmbHFrom.SelectedIndex = -1;
            this.cmbMFrom.SelectedIndex = -1;
            this.dateFrom.Value = DateTime.Now;
            this.txtTruckOrder.Enabled = true;
            this.txtTransferID.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.rdoAdd.Checked = false;
            this.rdoAddTo.Checked = false;
            this.rdoDelete.Checked = false;
            this.rdoModify.Checked = false;
            strTruckOrder = "";
            strTransferID = "";
            strSendTime = "";
            strSealNo = "";
            strFport = "";
            strDriver = "";
            strDport = "";
            strCarNo = "";
            strType = "";
            gbFunction.Enabled = true;
            panel2.Enabled = true;
        }
        #endregion


        #region Query
     
        public void Query()
        {
            stsWarning.Text = "";
            strTransferID = txtTransferID.Text;
            strTruckOrder = txtTruckOrder.Text;
            strSealNo = txtSealNo.Text.Trim();
            strCarNo = cmbCarNo.Text;
            strSendTime = "";
            if (cmbHFrom.SelectedIndex != -1 && cmbMFrom.SelectedIndex != -1)
            {
                strSendTime = dateFrom.Value.ToString("yyyy-MM-dd") + " " + cmbHFrom.SelectedItem.ToString() + ":" + cmbMFrom.SelectedItem.ToString() + ":00.000";
            }

            dtData = objCarData.QueryBindCarInfo(strTruckOrder,strTransferID, strSealNo, strSendTime, strCarNo, strDriver);
            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            if (dtData.Rows.Count <= 0)
            {
                stsWarning.Text = "没有需要绑定派车的调拨单，请确认！";
            }
            ShowDataGridView();
            lblCount.Text = "共" + dtData.Rows.Count + "条记录";
        }
        #endregion

        #region 调整布局大小
        private void Transfer_BindCarInfo_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ArrayList failTID = new ArrayList();
            DataTable dtPrint = new DataTable();
            
            foreach( DataRow dr in dtData.Rows )
            {
                if ((bool)dr["Select"]==true)
                {

                    if (dr["STATUS"].ToString() == "AT")
                    {
                        dtPrint.Rows.Clear();
                        strTransferID = dr["MBLNR"].ToString();
                        dtPrint = objCarData.TransferAdditionalPrint(strTransferID);
                        ReportPrint objReportPrint = new ReportPrint(UserData, "TRUCKINGORDER", dtPrint);
                        objReportPrint.MdiParent = this.ParentForm;
                        objReportPrint.Show();
                        bool flg = objCarData.UpdateTransferForPrint(strTransferID, strUsrnm);
                        Query();
                    }
                    else
                    {
                        failTID.Add(dr["MBLNR"].ToString());
                    }
                                       
                }
            }


            if (failTID.Count != 0)
            {
                string sbid = "";
                foreach (string id in failTID)
                {
                    sbid += id + ",";
                }
                stsWarning.Text = "Print Fail ,调拨单号:" + sbid + "打印失败，请确认！";
            }
         
        }
        #endregion

        #region radio changed

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.strType = "Add";
            this.gvData.Enabled = true;
            this.txtTruckOrder.Enabled = false;
        }

        private void rdoModify_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.strType = "Modify";
            this.gvData.Enabled = true;
            this.txtTruckOrder.Enabled = true;
            this.txtTransferID.Enabled = false;
        }

        private void rdoAddTo_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.strType = "AddTo";
            this.gvData.Enabled = true;
            this.txtTruckOrder.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.txtSealNo.Enabled = false;
            this.txtTransferID.Enabled = false;
            this.cmbCarNo.Enabled = false;
            this.cmbDriver.Enabled = false;

        }

        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.strType = "Delete";
            this.gvData.Enabled = true;
            this.txtTruckOrder.Enabled = true;
            
        }

        #endregion 

        #region select changed
        private void cmbCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCarNo.SelectedIndex != -1)
                {
                    strCarNo = cmbCarNo.SelectedItem.ToString();
                    DataTable dtTemp = new DataTable();
                    dtTemp = objCarData.QueryDriverByCarNo(strCarNo);
                    cmbDriver.Items.Clear();
                    if (dtTemp.Rows.Count > 0)
                    {
                        for ( int i=0; i < dtTemp.Rows.Count; i++)
                        {
                            cmbDriver.Items.Add(dtTemp.Rows[i]["DriverName"].ToString());
                        }
                    }
                  
                 }
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-QueryCarNoByDriver()");
            }

        }

        #endregion

        #region Confirm

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strTransferID = txtTransferID.Text;
            strTruckOrder = txtTruckOrder.Text;
            strSealNo = txtSealNo.Text.Trim();
            strCarNo = cmbCarNo.Text.ToString();
            StrDriver = cmbDriver.Text;
            strSendTime = "";
            if (cmbHFrom.SelectedIndex != -1 && cmbMFrom.SelectedIndex != -1)
            {
                strSendTime = dateFrom.Value.ToString("yyyy-MM-dd") + " " + cmbHFrom.SelectedItem.ToString() + ":" + cmbMFrom.SelectedItem.ToString() + ":00.000";
                if (Convert.ToDateTime(strSendTime) <= DateTime.Now)
                {
                    stsWarning.Text = "预计发车时间应超过当前时间！";
                    return;
                }
            }

            #region 查询
            if (strType == "")
            {
                Query();
            }
            #endregion

            #region 新增
            if (strType == "Add")
            {
                ArrayList alMblnrs=new ArrayList();
                if (strSealNo == "" || strDriver == "" || strCarNo == "" || dateFrom.Text == "" || cmbHFrom.Text == "" || cmbMFrom.Text == "")
                {
                    stsWarning.Text = "封条号、司机、车牌号、预计发车时间都不能为空！";
                    return;
                }
                else
                {
                    if (strSealNo.Length != 7)
                    {
                        MessageBox.Show("封条号必须7码！");
                        return;
                    }
                    DataRow[] drSelect = dtData.Select("Select=True AND (STATUS='AT' OR STATUS ='WP' )");
                    foreach (DataRow dr in drSelect)
                    {
                        alMblnrs.Add(dr["MBLNR"].ToString());
                    }
                    if (alMblnrs.Count > 0)
                    {
                        string orderNo = objCarData.CreateCarOrder();

                        //string Mblnrs = "";
                        foreach (string id in alMblnrs)
                        {
                            dtResult = objCarData.BindTransferDataForCarInfo(id, strSealNo, strSendTime, strCarNo, strDriver,orderNo);
                            //Mblnrs += "''" + id + "''"+ ",";
                        }
                        //Mblnrs = Mblnrs.Remove(Mblnrs.Length - 1, 1);
                        //dtResult = objCarData.BindTransferDataForCarInfo(Mblnrs, strSealNo, strSendTime, strCarNo, strDriver);
                    }
                    else
                    {
                        stsWarning.Text = "没有数据需要绑定派车单!";
                    }
                 }
             }
    
 
            #endregion

            #region 修改
            if (strType == "Modify")
            {
                if (strTruckOrder == "")
                {
                    stsWarning.Text = "请输入要修改的派车单号！";
                }
                else
                {
                    if (strSealNo == "" || strDriver == "" || strSendTime == "" || strCarNo == "")
                    {
                        stsWarning.Text = "封条号、司机、车牌号、预计发车时间都不能为空！";
                    }
                    else
                    {
                       bool flg= objCarData.UpdateTransferCarInfo(strTruckOrder, strSealNo, strSendTime, strCarNo, strDriver);
                       if (flg)
                       {
                           stsWarning.Text = "Update Success!";
                       }
                       else
                       {
                           stsWarning.Text = "Update Fail";
                       }
                    }
                }

            }

            #endregion

            #region 追加
            if (strType == "AddTo")
            {
                if (strTruckOrder == "")
                {
                    stsWarning.Text = "请输入需要追加的派车单号！";
                    return;
                }
                else
                {
                    dtData = objCarData.AddToCarOrder(strTruckOrder);
                    if (dtData.Rows.Count > 0)
                    {
                        strDriver = dtData.Rows[0]["DIRVER"].ToString();
                        strCarNo = dtData.Rows[0]["CARNO"].ToString();
                        strSendTime = dtData.Rows[0]["SEDTIM"].ToString();
                        strDport = dtData.Rows[0]["DPORT"].ToString();
                        strFport = dtData.Rows[0]["FPORT"].ToString();

                        AddTruckOrder objAddTruckOrder = new AddTruckOrder(UserData,strTruckOrder, StrCarNo, StrDriver, strSendTime, StrDport, StrFport);
                        objAddTruckOrder.Owner = this;
                        objAddTruckOrder.Show();
                    }
                    else
                    {
                        stsWarning.Text = "该派车单号不存在！";
                    }
                }
            }
            #endregion

            #region 删除

            if (strType == "Delete")
            {
                if (strTruckOrder == "")
                {
                    stsWarning.Text = "请选择要删除的派车单号！";
                    return;
                }
                else
                {
                    bool flg = objCarData.DeleteTransferDataForCarInfo(strTruckOrder);//将填好的信息与调拨单绑定
                    if (flg)
                    {
                        stsWarning.Text = "Delete OK";
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail ";
                    }
                }
            }
                        
       
            #endregion
            InitialControl();
            Query();
        }
       
        
        #endregion

        private void rdoDelete_Click(object sender, EventArgs e)
        {
            String PM = Interaction.InputBox("请输入密码", "输入密码", "", 100, 100);
            DataTable dtPWD = new DataTable();
            dtPWD = objCarData.GetPassword();
            DataRow[] dr = dtPWD.Select("PASWD='" + PM + "'");
            if (dr.Count() == 0)
            {
                MessageBox.Show("请输入正确的密码谢谢！！！！！");
                btnConfirm.Enabled = false;
                return;
            }
            else
            {
                btnConfirm.Enabled = true;
            }
        }
    }
}
