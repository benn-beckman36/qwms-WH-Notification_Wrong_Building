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

namespace QWMS
{
    public partial class IQCBorrowMaterial_HistoricalQuery : Form
    {
        #region 成员
        UserInfo UserData = new UserInfo();
        private DataTable dtBorrowInfo = new DataTable();
        private ReturnMaterial objRM;

        #region Werks(厂区)
        private string strWerks = "";
        /// <summary>
        /// Werks(厂区)
        /// </summary>
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
        #endregion

        #region Lgort(仓别)
        private string strLgort = "";
        /// <summary>
        /// Lgort(仓别)
        /// </summary>
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
        #endregion

        #region Mblnr(扣账编号)
        private string strMblnr = "";
        /// <summary>
        /// Mblnr(查询条件页面的扣账编号)
        /// </summary>
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
        #endregion

        #region Usernm(登录账号)
        private string strUsernm = "";
        /// <summary>
        /// Usernm(登录账号)
        /// </summary>
        public string Usernm
        {
            get { return strUsernm; }
            set { strUsernm = value; }
        }
        #endregion

        #region Comcd(公司代码)
        private string strComcd = "";
        /// <summary>
        /// Comcd(公司代码)
        /// </summary>
        public string Comcd
        {
            get { return strComcd; }
            set { strComcd = value; }
        }
        #endregion

        #region Progid(页面调用配置代码)
        private string strProgid = "";
        /// <summary>
        /// Progid(页面调用配置代码)
        /// </summary>
        public string Progid
        {
            get { return strProgid; }
            set { strProgid = value; }
        }
        #endregion

        #region Mandt(Client)
        private string strMandt = "";
        /// <summary>
        /// Mandt(Client)
        /// </summary>
        public string Mandt
        {
            get { return strMandt; }
            set { strMandt = value; }
        }
        #endregion
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="varUserData"></param>
        /// <param name="strProgid"></param>
        public IQCBorrowMaterial_HistoricalQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usernm = UserData.UserId;
            Progid = strProgid;
            try
            {
                StorageIn objAdmin = new StorageIn(UserData, Progid);
                objRM = new ReturnMaterial(UserData, Progid);
                //檢查權限
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

        # region 窗口底部显示基本信息
        /// <summary>
        /// 窗口底部显示基本信息
        /// </summary>
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsernm.Text = Usernm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region 绑定厂区(Plant)
        /// <summary>
        /// 绑定厂区的下拉列表值
        /// </summary>
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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
        #endregion

        #region 厂区触发事件
        /// <summary>
        /// 选择厂区自动带出对应的仓别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region 仓别触发事件
        /// <summary>
        /// 选择仓别自动带出有借料记录的扣账编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbMblnr.Text = "";
            ShowDdlMblnr();
        }
        #endregion

        #region 绑定仓别(Storage)
        /// <summary>
        /// 绑定仓别的下拉列表值
        /// </summary>
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

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
                    cmbLgort.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region 绑定扣账编号
        /// <summary>
        /// 绑定扣账编号的下拉列表值
        /// </summary>
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
                    dtTemp = objRM.QueryMblnrInfo(strWerks, strLgort);//抓取对应的有借料记录的扣账编号
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

        #region 显示查询结果的栏位信息
        /// <summary>
        /// 显示查询结果的栏位信息
        /// </summary>
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.AllowUserToAddRows = false;
            dgvData.Columns.Clear();
            try
            {
                //DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                //dgvcItem.DataPropertyName = "Item";
                //dgvcItem.HeaderText = "Item";
                //dgvcItem.ReadOnly = true;
                //dgvcItem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dgvcItem.DisplayIndex = 0;                
                //dgvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "Type";
                dgvcType.HeaderText = "Type";
                dgvcType.ReadOnly = true;
                dgvcType.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcType.DisplayIndex = 1;
                dgvData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcAccountDAT = new DataGridViewTextBoxColumn();
                dgvcAccountDAT.DataPropertyName = "AccountDAT";
                dgvcAccountDAT.HeaderText = "入账日期";
                dgvcAccountDAT.ReadOnly = true;
                dgvcAccountDAT.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcAccountDAT.DisplayIndex = 2;
                dgvData.Columns.Add(dgvcAccountDAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                dgvcMATNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMATNR.DisplayIndex = 3;
                dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "扣账编号";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMBLNR.DisplayIndex = 4;
                dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcBorrowNo = new DataGridViewTextBoxColumn();
                dgvcBorrowNo.DataPropertyName = "BorrowNo";
                dgvcBorrowNo.HeaderText = "借料单号";
                dgvcBorrowNo.ReadOnly = true;
                dgvcBorrowNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowNo.DisplayIndex = 5;
                dgvData.Columns.Add(dgvcBorrowNo);

                DataGridViewTextBoxColumn dgvcReturnNo = new DataGridViewTextBoxColumn();
                dgvcReturnNo.DataPropertyName = "ReturnNo";
                dgvcReturnNo.HeaderText = "还料单号";
                dgvcReturnNo.ReadOnly = true;
                dgvcReturnNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnNo.DisplayIndex = 6;
                dgvData.Columns.Add(dgvcReturnNo);

                DataGridViewTextBoxColumn dgvcPlant = new DataGridViewTextBoxColumn();
                dgvcPlant.DataPropertyName = "Plant";
                dgvcPlant.HeaderText = "厂区";
                dgvcPlant.ReadOnly = true;
                dgvcPlant.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcPlant.DisplayIndex = 7;
                dgvData.Columns.Add(dgvcPlant);

                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "Storage";
                dgvcStorage.HeaderText = "仓别";
                dgvcStorage.ReadOnly = true;
                dgvcStorage.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcStorage.DisplayIndex = 8;
                dgvData.Columns.Add(dgvcStorage);

                DataGridViewTextBoxColumn dgvcLocationID = new DataGridViewTextBoxColumn();
                dgvcLocationID.DataPropertyName = "LocationID";
                dgvcLocationID.HeaderText = "储位/人";
                dgvcLocationID.ReadOnly = true;
                dgvcLocationID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcLocationID.DisplayIndex = 9;
                dgvData.Columns.Add(dgvcLocationID);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "总数量";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMENGE.DisplayIndex = 10;
                dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcBorrowedQTY = new DataGridViewTextBoxColumn();
                dgvcBorrowedQTY.DataPropertyName = "BorrowedQTY";
                dgvcBorrowedQTY.HeaderText = "已借数量";
                dgvcBorrowedQTY.ReadOnly = true;
                dgvcBorrowedQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowedQTY.DisplayIndex = 11;
                dgvData.Columns.Add(dgvcBorrowedQTY);

                DataGridViewTextBoxColumn dgvcBorrowQTY = new DataGridViewTextBoxColumn();
                dgvcBorrowQTY.DataPropertyName = "BorrowQTY";
                dgvcBorrowQTY.HeaderText = "本次借料数量";
                dgvcBorrowQTY.ReadOnly = true;
                dgvcBorrowQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowQTY.DisplayIndex = 12;
                dgvData.Columns.Add(dgvcBorrowQTY);

                DataGridViewTextBoxColumn dgvcReturnedQTY = new DataGridViewTextBoxColumn();
                dgvcReturnedQTY.DataPropertyName = "ReturnedQTY";
                dgvcReturnedQTY.HeaderText = "已还数量";
                dgvcReturnedQTY.ReadOnly = true;
                dgvcReturnedQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnedQTY.DisplayIndex = 13;
                dgvData.Columns.Add(dgvcReturnedQTY);

                DataGridViewTextBoxColumn dgvcReturnQTY = new DataGridViewTextBoxColumn();
                dgvcReturnQTY.DataPropertyName = "ReturnQTY";
                dgvcReturnQTY.HeaderText = "还料数量";
                dgvcReturnQTY.ReadOnly = true;
                dgvcReturnQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnQTY.DisplayIndex = 14;
                dgvData.Columns.Add(dgvcReturnQTY);

                DataGridViewTextBoxColumn dgvcBorrowTime = new DataGridViewTextBoxColumn();
                dgvcBorrowTime.DataPropertyName = "BorrowTime";
                dgvcBorrowTime.HeaderText = "借料时间";
                dgvcBorrowTime.ReadOnly = true;
                dgvcBorrowTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvcBorrowTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowTime.DisplayIndex = 15;                
                dgvData.Columns.Add(dgvcBorrowTime);

                DataGridViewTextBoxColumn dgvcReturnTime = new DataGridViewTextBoxColumn();
                dgvcReturnTime.DataPropertyName = "ReturnTime";
                dgvcReturnTime.HeaderText = "还料时间";
                dgvcReturnTime.ReadOnly = true;
                dgvcReturnTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvcReturnTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnTime.DisplayIndex = 16;
                dgvData.Columns.Add(dgvcReturnTime);

                DataGridViewTextBoxColumn dgvcBorrowIQCID = new DataGridViewTextBoxColumn();
                dgvcBorrowIQCID.DataPropertyName = "BorrowIQCID";
                dgvcBorrowIQCID.HeaderText = "IQC借料人";
                dgvcBorrowIQCID.ReadOnly = true;
                dgvcBorrowIQCID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowIQCID.DisplayIndex = 17;
                dgvData.Columns.Add(dgvcBorrowIQCID);

                DataGridViewTextBoxColumn dgvcIQCBorrowID = new DataGridViewTextBoxColumn();
                dgvcIQCBorrowID.DataPropertyName = "BorrowWHID";
                dgvcIQCBorrowID.HeaderText = "WH借出人";
                dgvcIQCBorrowID.ReadOnly = true;
                dgvcIQCBorrowID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcIQCBorrowID.DisplayIndex = 18;
                dgvData.Columns.Add(dgvcIQCBorrowID);

                DataGridViewTextBoxColumn dgvcReturnIQCID = new DataGridViewTextBoxColumn();
                dgvcReturnIQCID.DataPropertyName = "ReturnIQCID";
                dgvcReturnIQCID.HeaderText = "IQC还料人";
                dgvcReturnIQCID.ReadOnly = true;
                dgvcReturnIQCID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnIQCID.DisplayIndex = 19;
                dgvData.Columns.Add(dgvcReturnIQCID);

                DataGridViewTextBoxColumn dgvcReturnWHID = new DataGridViewTextBoxColumn();
                dgvcReturnWHID.DataPropertyName = "ReturnWHID";
                dgvcReturnWHID.HeaderText = "WH接收人";
                dgvcReturnWHID.ReadOnly = true;
                dgvcReturnWHID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnWHID.DisplayIndex = 20;
                dgvData.Columns.Add(dgvcReturnWHID);

                dgvData.DataSource = dtBorrowInfo;
                lblRecords.Text = dtBorrowInfo.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }        
        #endregion        

        #region Query按钮
        /// <summary>
        /// 查询按钮触发时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            //cbUnReturn.AutoCheck,cbReturning.AutoCheck,cbReturned.AutoCheck
            string strStatus = "";
            if (cbUnReturn.Checked == true)
            {
                strStatus = strStatus + "',N'" + "已借未还";
            }
            if (cbReturning.Checked == true)
            {
                strStatus = strStatus + "',N'" + "未还完";
            }
            if (cbReturned.Checked == true)
            {
                strStatus = strStatus + "',N'" + "已借已还";
            }
            if (string.IsNullOrEmpty(strStatus))
            {
                MessageBox.Show("请至少勾选一种状态进行查询！", "提示信息", MessageBoxButtons.OK);
                stsWarning.Text = "请至少勾选一种状态进行查询！";
                return;
            }
            dtBorrowInfo = objRM.QueryBMHistoryInfo(cmbWerks.Text, cmbLgort.Text, cmbMblnr.Text, txtMatnr.Text, txtBorrowNo.Text, txtID.Text, strStatus);

            if (dtBorrowInfo.Rows.Count == 0)
            {
                stsWarning.Text = "No Data!";
            }
            else
            {
                ShowDataGrid();
            }

        }
        #endregion
        
    }
}
