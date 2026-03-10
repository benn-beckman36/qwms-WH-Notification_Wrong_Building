using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class IQCBorrowMaterial_BorrowMaterial : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMblnr = "";
        private string strBorrowIQCID = "";
        private string strBorrowWHID = "";

        private DataTable dtPNData = new DataTable();

        private PlantData objPlantData;
        private Authority objAuthority;
        private BorrowMateria objBorrowMateria;

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
       
        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }

        public string BorrowIQCID
        {
            get
            {
                return strBorrowIQCID;
            }
            set
            {
                strBorrowIQCID = value;
            }
        }

        public string BorrowWHID
        {
            get
            {
                return strBorrowWHID;
            }
            set
            {
                strBorrowWHID = value;
            }
        }
        #endregion

        #region 构造函数
        public IQCBorrowMaterial_BorrowMaterial()
        {
            InitializeComponent();
        }

        public IQCBorrowMaterial_BorrowMaterial(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {   
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objBorrowMateria = new BorrowMateria(UserData, Progid);

                StorageIn objAdmin = new StorageIn(UserData, Progid);

                // 检查权限
                if (!objAdmin.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
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
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        # endregion

        #region 绑定仓别
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                     
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        # endregion

        #region 绑定扣账编号
        private void ShowDdlMblnr()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString().Trim();//获取厂区
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString().Trim();//获取仓别
                    dtTemp = objBorrowMateria.GetMblnrInfo(strWerks, strLgort);//抓取对应的有借料记录的扣账编号
                }

                if (cmbMblnr.SelectedIndex != -1)
                {
                    strMblnr = cmbMblnr.Items[cmbMblnr.SelectedIndex].ToString().Trim();//获取当前显示的扣账编号
                }
                else
                {
                    cmbMblnr.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbMblnr.Items.Clear();
                    strMblnr = "";
                }
                else
                {
                    cmbMblnr.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbMblnr.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString().Trim());
                        if (dtTemp.Rows[i]["MBLNR"].ToString().Trim() == strMblnr && strMblnr != "")
                        {
                            cmbMblnr.SelectedIndex = i;
                        }
                    }
                    cmbMblnr.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMblnr()");
            }
        }
        #endregion

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        #region cmbLgort Selected Index Changed Event
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbMblnr.Text = "";
            ShowDdlMblnr();
        }
        #endregion
    
        #region ShowDataGridView
        private void ShowDataGridView()
        {          
            try
            {
                DataColumn cSelect = new DataColumn("Select", typeof(bool));
                dtPNData.Columns.Add(cSelect);
                for (int i = 0; i < dtPNData.Rows.Count; i++)
                {
                    dtPNData.Rows[i]["Select"] = false;
                }

                dgvPNData.AutoGenerateColumns = false;
                dgvPNData.Columns.Clear();
          
                DataGridViewTextBoxColumn Item = new DataGridViewTextBoxColumn();
                Item.DataPropertyName = "Item";
                Item.HeaderText = "Item";
                Item.ReadOnly = true;
                dgvPNData.Columns.Add(Item);

                DataGridViewTextBoxColumn BUDAT = new DataGridViewTextBoxColumn();
                BUDAT.DataPropertyName = "BUDAT";
                BUDAT.HeaderText = "入账日期";
                BUDAT.ReadOnly = true;
                dgvPNData.Columns.Add(BUDAT);

                DataGridViewTextBoxColumn MATNR = new DataGridViewTextBoxColumn();
                MATNR.DataPropertyName = "MATNR";
                MATNR.HeaderText = "料号";
                MATNR.ReadOnly = true;
                dgvPNData.Columns.Add(MATNR);

                DataGridViewTextBoxColumn MBLNR = new DataGridViewTextBoxColumn();
                MBLNR.DataPropertyName = "MBLNR";
                MBLNR.HeaderText = "扣账编号";
                MBLNR.ReadOnly = true;
                dgvPNData.Columns.Add(MBLNR);

                DataGridViewTextBoxColumn WERKS = new DataGridViewTextBoxColumn();
                WERKS.DataPropertyName = "WERKS";
                WERKS.HeaderText = "厂区";
                WERKS.ReadOnly = true;
                dgvPNData.Columns.Add(WERKS);

                DataGridViewTextBoxColumn LGORT = new DataGridViewTextBoxColumn();
                LGORT.DataPropertyName = "LGORT";
                LGORT.HeaderText = "仓别";
                LGORT.ReadOnly = true;
                dgvPNData.Columns.Add(LGORT);

                DataGridViewTextBoxColumn GRLOC = new DataGridViewTextBoxColumn();
                GRLOC.DataPropertyName = "GRLOC";
                GRLOC.HeaderText = "储位/人";
                GRLOC.ReadOnly = true;
                dgvPNData.Columns.Add(GRLOC);

                DataGridViewTextBoxColumn MENGE = new DataGridViewTextBoxColumn();
                MENGE.DataPropertyName = "MENGE";
                MENGE.HeaderText = "数量";
                MENGE.ReadOnly = true;
                dgvPNData.Columns.Add(MENGE);

                DataGridViewTextBoxColumn OTQTY = new DataGridViewTextBoxColumn();
                OTQTY.DataPropertyName = "OTQTY";
                OTQTY.HeaderText = "已借数量";
                OTQTY.ReadOnly = true;
                dgvPNData.Columns.Add(OTQTY);

                DataGridViewTextBoxColumn BorrowQTY = new DataGridViewTextBoxColumn();
                BorrowQTY.DataPropertyName = "BorrowQTY";
                BorrowQTY.HeaderText = "借料数量";
                dgvPNData.Columns.Add(BorrowQTY);

                DataGridViewCheckBoxColumn Select = new DataGridViewCheckBoxColumn();
                Select.DataPropertyName = "Select";
                Select.HeaderText = "Select";
                dgvPNData.Columns.Add(Select);

                dgvPNData.DataSource = dtPNData;

                if (dtPNData.Rows.Count > 0)
                {
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        # endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            txtBorrowIQCID.Text = "";
            txtBorrowWHID.Text = "";
            lblIQCCheck.Text = "请按Enter键验证IQC身份";
            lblWHCheck.Text = "请按Enter键验证WH身份";

            objBorrowMateria = new BorrowMateria(UserData, Progid);
            dtPNData = objBorrowMateria.QueryMaterialInfo(cmbWerks.Text.ToString().Trim(), cmbLgort.Text.ToString().Trim(), txtPN.Text.ToString().Trim(), cmbMblnr.Text.ToString().Trim());
            if (dtPNData.Rows.Count >0)
            {
                ShowDataGridView();     
            }
            else
            {
                MessageBox.Show("No Data!");
                ShowDataGridView();
            }                      
        }
        # endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            # region 构造DataTable架构
            DataTable dtTemp = new DataTable();
            DataColumn dc = new DataColumn("BUDAT", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("MATNR", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("MBLNR", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("WERKS", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("LGORT", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("GRLOC", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("MENGE", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("OTQTY", typeof(string));
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("BorrowQTY", typeof(string));
            dtTemp.Columns.Add(dc);
            # endregion

            List<DataRow> lstRow = new List<DataRow>();

            try
            {
                for (int i = 0; i < dtPNData.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtPNData.Rows[i]["Select"]))
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr["BUDAT"] = dtPNData.Rows[i]["BUDAT"];
                        dr["MATNR"] = dtPNData.Rows[i]["MATNR"];
                        dr["MBLNR"] = dtPNData.Rows[i]["MBLNR"];
                        dr["WERKS"] = dtPNData.Rows[i]["WERKS"];
                        dr["LGORT"] = dtPNData.Rows[i]["LGORT"];
                        dr["GRLOC"] = dtPNData.Rows[i]["GRLOC"];
                        dr["MENGE"] = dtPNData.Rows[i]["MENGE"];
                        dr["OTQTY"] = dtPNData.Rows[i]["OTQTY"];
                        dr["BorrowQTY"] = dtPNData.Rows[i]["BorrowQTY"];
                        lstRow.Add(dr);
                    }
                }

                #region 验证

                #region 校验QIC刷卡和WH刷卡
                //身份验证，按Enter键会进行身份验证
                if (lblIQCCheck.Text != "IQC身份验证OK")
                {
                    MessageBox.Show("请进行IQC身份验证！", "身份验证", MessageBoxButtons.OK);
                    return;
                }
                if (lblWHCheck.Text != "WH身份验证OK")
                {
                    MessageBox.Show("请进行WH身份验证！", "身份验证", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                if (lstRow.Count == 0)
                {
                    MessageBox.Show("没有勾选资料，请确认!", "Error", MessageBoxButtons.OK);
                    return;
                }

                #region 校验已借数量与借料数量
                for (int i = 0; i < lstRow.Count; i++)
                {
                    string strQTY = lstRow[i]["MENGE"].ToString().Trim();
                    string strBorrowedQTY = lstRow[i]["OTQTY"].ToString().Trim();
                    string strBorrowQTY = lstRow[i]["BorrowQTY"].ToString().Trim();
                 
                    Regex reg = new Regex(@"^\+?[1-9][0-9]*$");
                    if (!reg.IsMatch(strBorrowQTY))
                    {
                        MessageBox.Show("借料数量必须是非零的正整数！");
                        return;
                    }
                    string strNewBorrowedQTY=objBorrowMateria.GetNewBorrowedQTY(lstRow[i]["WERKS"].ToString().Trim(), lstRow[i]["LGORT"].ToString().Trim(), lstRow[i]["MATNR"].ToString().Trim(), lstRow[i]["MBLNR"].ToString().Trim());

                    if (Convert.ToInt32(strNewBorrowedQTY)!=Convert.ToInt32(strBorrowedQTY))//判断已借数量
                    {
                        MessageBox.Show("扣账编号：" + lstRow[i]["MBLNR"].ToString().Trim() + " 已借数量后台数据有更新，请确认!");
                        btnQuery_Click(null, null);
                        return;
                    }
                    if (!this.CheckBorrowQTY(strQTY, strBorrowedQTY, strBorrowQTY))//判断数量
                    {
                        MessageBox.Show("扣账编号：" + lstRow[i]["MBLNR"].ToString().Trim() + "借料数量大于可借数量，请确认!");  
                        return;
                    }                  
                }
                #endregion
              
                #endregion

                string strBorrowNo = "";
                strBorrowNo = objBorrowMateria.BorrowMaterial(lstRow, BorrowIQCID, BorrowWHID);
                strBorrowNo = strBorrowNo.Replace(",", @"','");
                if (strBorrowNo!="")
                {
                    objBorrowMateria.SendMail(strBorrowNo);
                    string strNO = strBorrowNo.Replace("','", @",");
                    MessageBox.Show("借料成功！借料单号：" + strNO);
                    btnQuery_Click(null, null);                 
                }
                else
                {
                    MessageBox.Show(objBorrowMateria.ERRMSG);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnSave()");
            }
        }
        # endregion

        # region 校验借料数量
        protected bool CheckBorrowQTY(string strQTY, string strBorrowedQTY, string strBorrowQTY)
        {
            int Qty = 0;
            int BorrowedQTY = 0;
            int BorrowQTY = 0;
            if (!string.IsNullOrEmpty(strQTY))
            {
                Qty = Convert.ToInt32(strQTY);
            }
            if (!string.IsNullOrEmpty(strBorrowedQTY))
            {
                BorrowedQTY = Convert.ToInt32(strBorrowedQTY);
            }
            if (!string.IsNullOrEmpty(strBorrowQTY))
            {
                BorrowQTY = Convert.ToInt32(strBorrowQTY);
            }
            if (Qty < BorrowedQTY + BorrowQTY)
            {                       
                return false;
            }
            else
            {
                return true;
            }
        }
        # endregion

        #region IQC刷卡Enter键触发事件
        private void txtBorrowIQCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //测试不需要验证
                if (this.CheckID("IQC", txtBorrowIQCID.Text.Trim()))
                {
                    lblIQCCheck.Text = "IQC身份验证OK";
                }
                else
                {
                    lblIQCCheck.Text = "请按Enter键验证IQC身份";
                    return;
                }
            }
        }
        #endregion

        #region WH刷卡Enter键触发事件
        private void txtBorrowWHID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.CheckID("WH", txtBorrowWHID.Text.Trim()))
                {
                    lblWHCheck.Text = "WH身份验证OK";
                }
                else
                {
                    lblWHCheck.Text = "请按Enter键验证WH身份";
                    return;
                }
            }
        }
        #endregion

        #region 身份验证
        private bool CheckID(string strFlag, string strCardNo)
        {
            bool bl = false;
            string strNam = "";
            string strTextFail = strFlag + "刷卡验证失败";


            DataTable dtEmployeeData = new DataTable();
            DataTable dtHR = new DataTable();
            Admin objAdmin = new Admin(UserData, Progid);

            dtEmployeeData = objAdmin.GetEmployeeData(strCardNo.Trim());
            if (dtEmployeeData.Rows.Count > 0)
            {
                dtHR = dtEmployeeData;
            }
           // DataTable dtHR = objBorrowMateria.GetHRData(strCardNo.Trim());
            //测试 直接通过验证
            if (strCardNo == "A1101442")
            {
                return true;
            }
            if (dtHR.Rows.Count > 0)
            {
                strNam = dtHR.Rows[0]["depnam"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("HR无" + strNam + "资料", strFlag + "刷卡验证失败", MessageBoxButtons.OK);
                return bl;
            }
            string strOwnerFail = "该员工的部门是" + strNam + "，非" + strFlag + "人员，不能进行还料作业！";
            if (strFlag == "IQC")
            {//校验IQC的人
                BorrowIQCID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的IQC还料人工号
                txtBorrowIQCID.Text = BorrowIQCID + "：" + strNam;//IQC刷卡显示工号和部门信息

                Regex reg = new Regex(@"进料检验部");

                if (!reg.IsMatch(strNam))
                {
                    MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                    return bl;
                }
                else
                {
                    bl = true;
                    return bl;
                }
            }
            else if (strFlag == "WH")
            {//校验WH的人
                BorrowWHID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的WH接收人工号
                txtBorrowWHID.Text = BorrowWHID + "：" + strNam;//WH刷卡显示工号和部门信息

                Regex reg = new Regex(@"物流服务部");
                if (!reg.IsMatch(strNam))
                {
                    MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                    return bl;
                }
                else
                {
                    bl = true;
                    return bl;
                }
            }
            return bl;
        }
        #endregion

        #region 调整布局大小
        private void IQCBorrowMaterial_BorrowMaterial_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        # endregion
    }
}
