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
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Collections;
using System.Data.SqlClient;

namespace QWMS
{

    public partial class InventoryCheck_PreProgresses : Form
    {
        #region Variable Define
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strProgid = string.Empty;
        //上传状态
        private bool msgFlag = false;
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private DataTable dtData = new DataTable();
        private DataTable dtOA = new DataTable();
        private DataTable dtOACyc = new DataTable();
        private DataTable dtTemp = new DataTable();
        //UploadExcelFile
        string[] fileNames = null;
        //GridViewHeaderCheckBox
        private QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell headCheck = new QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell();
        QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell OAHeader = new QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell();

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
        #endregion

        #region PageLoad

        public InventoryCheck_PreProgresses()
        {
            InitializeComponent();

        }

        public InventoryCheck_PreProgresses(UserInfo varUserData, string strProgid)
        {
            dtData.ColumnChanged += new DataColumnChangeEventHandler(dtData_ColumnChanged);
            InitializeComponent();
            //btn false
            btnSave.Enabled = false;
            btnRefresh.Enabled = false;
            btnThread.Enabled = false;
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            this.headCheck.OnCheckBoxClicked += new QWMS.Manage_SapSimulationDataPrint.CheckBoxClickedHandler(headCheck_OnCheckBoxClicked);
            this.OAHeader.OnCheckBoxClicked += new QWMS.Manage_SapSimulationDataPrint.CheckBoxClickedHandler(OAHeader_OnCheckBoxClicked);
            progBar.Visible = false;

            try
            {
                objCounting = new Counting(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objCounting.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    btnThread.Visible = false;
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowLgortOAGrid();
                    progBar.Visible = false;
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void dtData_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 全选功能
        void headCheck_OnCheckBoxClicked(bool state)
        {
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataRow drData in dtData.Rows)
                {
                    drData["isSelect"] = state ? 1 : 0;
                }
            }
        }
        void OAHeader_OnCheckBoxClicked(bool state)
        {
            foreach (DataRow drData in dtOA.Rows)
            {
                drData["isChecked"] = state ? 1 : 0;
            }
        }
        #endregion


        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion
        # region ShowDdlWerks
        private void ShowDdlWerks()
        {
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
        # region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;

            try
            {
                #region 盘掉票号查询成功数据
                //0
                DataGridViewCheckBoxColumn dgvcbc = new DataGridViewCheckBoxColumn();
                dgvcbc.HeaderCell = headCheck;
                dgvcbc.DataPropertyName = "isSelect";
                dgvData.Columns.Add(dgvcbc);
                //1
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);
                //2
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);
                //3
                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "SUBSTORAGE";
                dgvcKostl.HeaderText = "Dept No";
                dgvcKostl.ReadOnly = true;
                dgvData.Columns.Add(dgvcKostl);
                //4
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                #region 复选是否连板和混合料号
                //6
                DataGridViewCheckBoxColumn isMixed = new DataGridViewCheckBoxColumn();
                isMixed.DataPropertyName = "ISMIX";
                isMixed.HeaderText = "isMixedMaterial";
                isMixed.Selected = false;
                isMixed.Width = 120;
                dgvData.Columns.Add(isMixed);
                #endregion
                //7 盘点票号区间起
                DataGridViewTextBoxColumn dgvcCycno = new DataGridViewTextBoxColumn();
                dgvcCycno.DataPropertyName = "TicketFrom";
                dgvcCycno.HeaderText = "TicketFrom";
                dgvcCycno.ReadOnly = true;
                dgvcCycno.Width = 100;
                dgvData.Columns.Add(dgvcCycno);
                //8盘点笔数

                DataGridViewTextBoxColumn dgvcInvNo = new DataGridViewTextBoxColumn();
                dgvcInvNo.DataPropertyName = "INVCOUNT";
                dgvcInvNo.HeaderText = "InvCount";
                dgvcInvNo.ReadOnly = true;
                dgvcInvNo.Width = 100;
                dgvData.Columns.Add(dgvcInvNo);
                //9盤點區間止
                DataGridViewTextBoxColumn dgvcCycnoTo = new DataGridViewTextBoxColumn();
                dgvcCycnoTo.DataPropertyName = "TicketTo";
                dgvcCycnoTo.HeaderText = "TicketTo";
                dgvcCycnoTo.ReadOnly = true;
                dgvcCycnoTo.Width = 100;
                dgvData.Columns.Add(dgvcCycnoTo);
                //10是否成功
                DataGridViewTextBoxColumn dgvcConfirm = new DataGridViewTextBoxColumn();
                dgvcConfirm.DataPropertyName = "REMSG";
                dgvcConfirm.HeaderText = "是否成功";
                dgvcConfirm.Width = 200;
                dgvcConfirm.ReadOnly = true;
                dgvData.Columns.Add(dgvcConfirm);

                dgvData.DataSource = dtData;
                #endregion


                //lblCount.Text = dtData.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        #region ShowOALgort
        private void ShowLgortOAGrid()
        {
            OALgortView.Columns.Clear();
            OALgortView.AutoGenerateColumns = false;
            try
            {
                #region OALgortPaint
                DataGridViewCheckBoxColumn dgvCheckAll = new DataGridViewCheckBoxColumn();
                dgvCheckAll.HeaderCell = OAHeader;
                dgvCheckAll.DataPropertyName = "isChecked";
                OALgortView.Columns.Add(dgvCheckAll);
                DataGridViewTextBoxColumn dgvPlant = new DataGridViewTextBoxColumn();
                dgvPlant.HeaderText = "Plant";
                dgvPlant.ReadOnly = true;
                dgvPlant.DataPropertyName = "Plant";
                OALgortView.Columns.Add(dgvPlant);
                DataGridViewTextBoxColumn dgvLgort = new DataGridViewTextBoxColumn();
                dgvLgort.HeaderText = "Storage";
                dgvLgort.DataPropertyName = "Storage";
                dgvLgort.ReadOnly = true;
                OALgortView.Columns.Add(dgvLgort);

                #endregion
                OALgortView.DataSource = dtOA;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLgortOAGrid()");
            }
        }
        #endregion

        # region 验证字符串
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }

        private bool CheckIsCharacter(string strValue)
        {
            Regex rgxNumber = new Regex("[A-Z]");
            return rgxNumber.IsMatch(strValue.ToUpper());
        }
        # endregion

        #region 选择框

        private void chkIsCombine_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        # region Confirm

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //btn true|false
            btnSave.Enabled = true;
            btnRefresh.Enabled = true;
            btnConfirm.Enabled = false;
            btnThread.Enabled = true;
            confirm_1.Enabled = false;
            ArrayList alLgortSelected = new ArrayList();
            try
            {
                foreach (DataRow drData in dtOA.Rows)
                {
                    if (!string.IsNullOrEmpty(drData["isChecked"].ToString()) && !drData["isChecked"].ToString().Equals("0"))
                    {
                        alLgortSelected.Add(drData["Storage"].ToString());
                        drData["isChecked"] = 1;
                    }
                }

                //Set Status
                stsWarning.Text = "";
                //根據廠區信息進行查找數據
                //查詢出廠區、倉別、子倉、狀態、盤點票號區間、盤點筆數
                string strWERKS = cmbWerks.Text.ToString();
                //dtData = objAuthority.CheckLgortPreProgresses(strWERKS, alLgortSelected);
                //ShowDataGrid();

                //add by Jason 20200720
                OAMM.ICMSServiceClient objOAMM = new QWMS.OAMM.ICMSServiceClient();
                dtOACyc = objOAMM.ICInventoryDataForEC(cmbWerks.Text.Trim(), "", "2");
                dtOACyc.Columns.Add("Guid", typeof(string));
                string guid = Guid.NewGuid().ToString();
                foreach (DataRow dr in dtOACyc.Rows)
                {
                    dr["Guid"] = guid;
                }
                try
                {
                    CommonInfo.Instance.DBCode = ConnectionStringProvider.Instance.Connections["TNC_9210"].ToString().Trim();
                    //using (SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBCodeQWMS_9210"].ToString().Trim()))
                    using (SqlConnection sqlConn = new SqlConnection(CommonInfo.Instance.DBCode))
                    {
                        sqlConn.Open();
                        using (SqlTransaction sqlTran = sqlConn.BeginTransaction())
                        {
                            string deleteQuery = "delete from Inventory_Interface ";
                            SqlCommand sqlComm = new SqlCommand(deleteQuery, sqlConn, sqlTran);
                            sqlComm.ExecuteNonQuery();
                            using (SqlBulkCopy sqlcopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.Default, sqlTran))
                            {
                                sqlcopy.BatchSize = 10;
                                sqlcopy.DestinationTableName = "Inventory_Interface";
                                try
                                {
                                    sqlcopy.WriteToServer(dtOACyc);
                                    sqlTran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    sqlTran.Rollback();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                dtData = objAuthority.CheckLgortPreProgresses(strWERKS, alLgortSelected, guid);
                ShowDataGrid();

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnConfirm.Enabled = true;
                this.btnSave.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.confirm_1.Enabled = true;
                stsWarning.Text = "";
                //this.headCheck.Selected = false;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.progBar.Visible = true;
            DataRow[] drDatas = dtData.Select("isSelect = 1");
            this.progBar.Maximum = drDatas.Count<DataRow>();
            syncRun(drDatas);
            //ThreadSaves();
            this.progBar.Visible = false;
            cmbWerks_SelectedIndexChanged(null, null);
        }
        private void btnThread_Click(object sender, EventArgs e)
        {
            progBar.Visible = true;
            ThreadSaves();
            btnSave.Enabled = true;
            btnThread.Enabled = true;
        }

        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dtOA = objCounting.QueryCountingLgortFromOA(cmbWerks.Text.Trim());
            //ShowLgortOAGrid();

            OAMM.ICMSServiceClient objOAMM = new QWMS.OAMM.ICMSServiceClient();
            dtOA = objOAMM.ICInventoryDataForEC(cmbWerks.Text.Trim(), "", "2");
            dtOA.Columns.Add("isChecked", typeof(int));
            foreach (DataRow dr in dtOA.Rows)
            {
                dr["isChecked"] = "0";
            }
            ShowLgortOAGrid();
        }

        #region Another Confirm
        private void confirm_1_Click(object sender, EventArgs e)
        {
            btnConfirm_Click(null, null);
        }
        #endregion

        #region ThreadSave
        private void ThreadSaves()
        {
            btnSave.Enabled = false;
            //用list装LGORT
            ArrayList alLgort = new ArrayList();
            //设置线程池
            ThreadPool.SetMaxThreads(5, 5);
            foreach (DataRow drData in dtData.Rows)
            {
                if (!drData["isSelect"].ToString().Equals("0") && !string.IsNullOrEmpty(drData["isSelect"].ToString()))
                {
                    alLgort.Add(drData["LGORT"].ToString());
                }
            }
            progBar.Maximum = alLgort.Count;
            method_threadStartEvent(alLgort.Count, null);
            foreach (string strLgort in alLgort)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateMethod), strLgort);
            }
        }

        private void UpdateMethod(object Lgort)
        {
            DataRow[] drDatas = dtData.Select("LGORT = '" + Lgort.ToString() + "'");
            //子仓是否是最后一个的标志
            string strFlag = string.Empty;
            string strCycNoFrom = string.Empty;
            string strCycNoTo = string.Empty;
            string isCombine = string.Empty;
            string isMixMatnr = string.Empty;
            string Kostl = string.Empty;
            long intCycNo;
            string strInsmk = string.Empty;
            foreach (DataRow drData in drDatas)
            {
                #region 循环体
                #region 状态确认
                //判断是否被选中
                if (string.IsNullOrEmpty(drData["isSelect"].ToString()) || drData["isSelect"].ToString().Equals("0"))
                {
                    continue;
                }
                drData["isSelect"] = 1;
                strInsmk = drData["INSMK"].ToString().Trim();

                //如果该行执行成功过，不再执行该行
                if ("OK".Equals(drData["REMSG"].ToString()))
                {
                    continue;
                }
                //检查该子仓是否存在盘点票号
                if (drData["TicketFrom"].ToString().Trim() == ""
                    || drData["TicketTo"].ToString().Trim() == "")
                {
                    drData["REMSG"] = "CYCNO ERROR!!!";
                    continue;
                }
                //连板  5
                if (string.IsNullOrEmpty(drData["ISCOMBINE"].ToString()) || drData["ISCOMBINE"].Equals("0"))
                {
                    isCombine = "Y";
                    drData["ISCOMBINE"] = 1;
                }
                else
                {
                    drData["ISCOMBINE"] = 0;
                }
                //混合料号 6
                if (string.IsNullOrEmpty(drData["ISMIX"].ToString()) || drData["ISMIX"].Equals("0"))
                {
                    drData["ISMIX"] = 1;
                    isMixMatnr = "Y";
                }
                else
                {
                    drData["ISMIX"] = 0;
                }
                strCycNoFrom = drData["TicketFrom"].ToString().Trim();
                strCycNoTo = drData["TicketTo"].ToString().Trim();
                Kostl = drData["SUBSTORAGE"].ToString().Trim();
                #endregion
                #region 子仓确认 盘点票号规则
                //判断是否是最后一个子仓
                //DataTable dtCyc = objCounting.QueryCycNoFromOA(drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), "");
                OAMM.ICMSServiceClient objOA = new QWMS.OAMM.ICMSServiceClient();
                DataTable dtCyc = objOA.ICInventoryDataForEC(drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), "2");
                
                if (dtCyc.Rows.Count > 1)
                {
                    strFlag = "0";
                }
                else
                {
                    strFlag = "1";
                }
                //检查盘点票号是否是规定形式
                if (!CheckIsCharacter(strCycNoFrom.Substring(0, 1))
                    && !CheckIsCharacter(strCycNoTo.Substring(0, 1)))
                {
                    drData["REMSG"] = "CYCNO is wrong!!!";
                    continue;
                }
                if (!CheckIsNumber(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1))
                    && !CheckIsNumber(strCycNoTo.Substring(1, strCycNoTo.Length - 1)))
                {
                    drData["REMSG"] = "CYCNO is wrong!!!";
                    continue;
                }
                intCycNo = Convert.ToInt64(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1));
                #endregion
                #region 获得该子仓的全部数据
                objCounting = new Counting(UserData, drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), Progid);
                dtTemp = objCounting.QueryCountingPrepareDataMore(Kostl, strInsmk, isMixMatnr, "Y");
                if (dtTemp.Rows.Count == Convert.ToInt32(drData["INVCOUNT"].ToString()))
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        strCycNoFrom = strCycNoFrom.Substring(0, 1) + (intCycNo++);
                        dtTemp.Rows[i]["CYCNO"] = strCycNoFrom;
                    }
                }
                else
                {
                    drData["REMSG"] = "INVCOUNT ERROR";
                    continue;
                }

                if (Convert.ToInt32(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1)) > Convert.ToInt32(strCycNoTo.Substring(1, strCycNoTo.Length - 1)))
                {
                    drData["REMSG"] = "CYCNO OUT OF RANGE";
                }
                #endregion
                #region 上传至OA
                if (objCounting.AddCountingDataMore(Kostl, dtTemp))
                {
                    drData["REMSG"] = "QWMS OK";
                    #region 转换表名，列名
                    DataTable dtZBA19 = dtTemp.Copy();
                    DataTable dtZBA21 = new DataTable();
                    dtZBA19.TableName = "ZBA19";
                    dtZBA21.TableName = "ZBA21";
                    dtZBA21.Columns.Add("WERKS");
                    dtZBA21.Columns.Add("LGORT");
                    dtZBA21.Columns.Add("FLAG");

                    DataRow dr = dtZBA21.NewRow();
                    dr["WERKS"] = drData["WERKS"].ToString().Trim();
                    dr["LGORT"] = drData["LGORT"].ToString().Trim();
                    dr["FLAG"] = strFlag;
                    dtZBA21.Rows.Add(dr.ItemArray);

                    if (!dtZBA19.Columns.Contains("AREA_NO"))
                    {
                        dtZBA19.Columns.Add("AREA_NO");
                    }
                    if (!dtZBA19.Columns.Contains("DCODE"))
                    {
                        dtZBA19.Columns.Add("DCODE");
                    }
                    //删除不需要的栏位
                    dtZBA19.Columns.Remove("MANDT");
                    dtZBA19.Columns.Remove("COMCD");
                    dtZBA19.Columns.Remove("CRDAT");
                    dtZBA19.Columns.Remove("MODAT");
                    dtZBA19.Columns.Remove("MONAM");

                    //按照SAP文档栏位重新排序
                    dtZBA19.Columns["WERKS"].SetOrdinal(0);
                    dtZBA19.Columns["STATS"].SetOrdinal(1);
                    dtZBA19.Columns["CYCNO"].SetOrdinal(2);
                    dtZBA19.Columns["INSMK"].SetOrdinal(3);
                    dtZBA19.Columns["LGORT"].SetOrdinal(4);
                    dtZBA19.Columns["LOCAT"].SetOrdinal(5);
                    dtZBA19.Columns["MATNR"].SetOrdinal(6);
                    dtZBA19.Columns["MENGE"].SetOrdinal(7);
                    dtZBA19.Columns["CKQTY"].SetOrdinal(8);
                    dtZBA19.Columns["CKDAT"].SetOrdinal(9);
                    dtZBA19.Columns["CHARG"].SetOrdinal(10);
                    dtZBA19.Columns["KOSTL"].SetOrdinal(11);
                    dtZBA19.Columns["AREA_NO"].SetOrdinal(12);
                    dtZBA19.Columns["DCODE"].SetOrdinal(13);
                    dtZBA19.Columns["CRNAM"].SetOrdinal(14);

                    //按照SAP文档列名重命名
                    dtZBA19.Columns["STATS"].ColumnName = "STATUS";
                    dtZBA19.Columns["CYCNO"].ColumnName = "TICKET";
                    dtZBA19.Columns["INSMK"].ColumnName = "BSTAR";
                    dtZBA19.Columns["MENGE"].ColumnName = "BUCHM";
                    dtZBA19.Columns["CKQTY"].ColumnName = "RIMENGE";
                    dtZBA19.Columns["CKDAT"].ColumnName = "CUNT_DTE";
                    dtZBA19.Columns["KOSTL"].ColumnName = "DEPT_NO";
                    dtZBA19.Columns["CRNAM"].ColumnName = "SNAME";

                    #endregion
                    bool blResult = false;

                    string strMessage = string.Empty;
                    string[] strList = { drData["UID"].ToString().Trim() };


                    OAMM.ICMSServiceClient objOAMM = new QWMS.OAMM.ICMSServiceClient();
                    //blResult= objOAMM.LogisticsKeyinDataInterfaceForQWMS(out strMessage, "QA2", dtZBA19, dtZBA21, strList, UserData.UserId);//SAP测试环境

                    blResult = objOAMM.LogisticsKeyinDataInterfaceForQWMS(Mandt, dtZBA19, dtZBA21, strList, UserData.UserId, out strMessage);
                    if (blResult)
                    {

                        //所有前置作业完成
                        drData["REMSG"] = "OK";
                    }
                    else
                    {
                        drData["REMSG"] = "OA Failed!!!" + strMessage;
                        msgFlag = true;
                    }
                }
                else
                {
                    drData["REMSG"] = "QWMS Failed!!!";
                }
                #endregion
                #endregion
            }
            method_threadEvent(null, null);
            if (msgFlag)
            {
                stsWarning.Text = "已完成保存步骤，但部分数据保存错误。请排查！！！";
            }
            else
            {
                stsWarning.Text = "已全部保存成功！！";
            }
        }
        #endregion
        #region SyncRun
        private void syncRun(DataRow[] drDatas)
        {
            msgFlag = false;
            this.btnSave.Enabled = false;
            string strFlag = string.Empty;
            string strCycNoFrom = string.Empty;
            string strCycNoTo = string.Empty;
            string isCombine = string.Empty;
            string isMixMatnr = string.Empty;
            string Kostl = string.Empty;
            long intCycNo;
            string strInsmk = string.Empty;
            //循环遍历每一行（每一个子仓）
            try
            {
                foreach (DataRow drData in drDatas)
                {
                    #region 循环体
                    #region 状态确认
                    strInsmk = drData["INSMK"].ToString().Trim();

                    //如果该行执行成功过，不再执行该行
                    if ("OK".Equals(drData["REMSG"].ToString()))
                    {
                        continue;
                    }
                    //检查该子仓是否存在盘点票号
                    if (drData["TicketFrom"].ToString().Trim() == ""
                        || drData["TicketTo"].ToString().Trim() == "")
                    {
                        drData["REMSG"] = "CYCNO ERROR!!!";
                        msgFlag = true;
                        continue;
                    }
                    //连板  5
                    if (string.IsNullOrEmpty(drData["ISCOMBINE"].ToString()) || drData["ISCOMBINE"].Equals("0"))
                    {
                        isCombine = "Y";
                        drData["ISCOMBINE"] = 1;
                    }
                    else
                    {
                        drData["ISCOMBINE"] = 0;
                    }
                    //混合料号 6
                    if (string.IsNullOrEmpty(drData["ISMIX"].ToString()) || drData["ISMIX"].Equals("0"))
                    {
                        drData["ISMIX"] = 1;
                        isMixMatnr = "Y";
                    }
                    else
                    {
                        drData["ISMIX"] = 0;
                    }
                    #endregion

                    #region 对该主仓进行逻辑处理
                    strCycNoFrom = drData["TicketFrom"].ToString().Trim();
                    strCycNoTo = drData["TicketTo"].ToString().Trim();
                    Kostl = drData["SUBSTORAGE"].ToString().Trim();
                    //判断是否是最后一个子仓
                    //DataTable dtCyc = objCounting.QueryCycNoFromOA(drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), "");
                    OAMM.ICMSServiceClient objOA = new QWMS.OAMM.ICMSServiceClient();
                    DataTable dtCyc = objOA.ICInventoryDataForEC(drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), "2");
                    
                    if (dtCyc.Rows.Count > 1)
                    {
                        strFlag = "0";
                    }
                    else
                    {
                        strFlag = "Y";
                    }
                    //检查盘点票号是否是规定形式
                    if (!CheckIsCharacter(strCycNoFrom.Substring(0, 1))
                        && !CheckIsCharacter(strCycNoTo.Substring(0, 1)))
                    {
                        drData["REMSG"] = "CYCNO is wrong!!!";
                        msgFlag = true;
                        continue;
                    }
                    if (!CheckIsNumber(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1))
                        && !CheckIsNumber(strCycNoTo.Substring(1, strCycNoTo.Length - 1)))
                    {
                        drData["REMSG"] = "CYCNO is wrong!!!";
                        msgFlag = true;
                        continue;
                    }
                    intCycNo = Convert.ToInt64(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1));
                    objCounting = new Counting(UserData, drData["WERKS"].ToString().Trim(), drData["LGORT"].ToString().Trim(), Progid);
                    dtTemp = objCounting.QueryCountingPrepareDataMore(Kostl, strInsmk, isMixMatnr, "Y");
                    if (dtTemp.Rows.Count != 0)
                    {
                        if (dtTemp.Rows.Count == Convert.ToInt32(drData["INVCOUNT"].ToString()))
                        {
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                strCycNoFrom = strCycNoFrom.Substring(0, 1) + (intCycNo++);
                                dtTemp.Rows[i]["CYCNO"] = strCycNoFrom;
                            }
                        }
                        else
                        {
                            drData["REMSG"] = "INVCOUNT ERROR";
                            msgFlag = true;
                            continue;
                        }
                    }
                    else
                    {
                        //对空表填灌数据
                        DataRow drTemp = dtTemp.NewRow();
                        drTemp["MANDT"] = Mandt;
                        drTemp["COMCD"] = Comcd;
                        drTemp["CYCNO"] = strCycNoFrom;
                        drTemp["KOSTL"] = Kostl;
                        drTemp["WERKS"] = drData["WERKS"].ToString().Trim();
                        drTemp["LGORT"] = drData["LGORT"].ToString().Trim();
                        drTemp["STATS"] = "3";
                        drTemp["CRNAM"] = "";
                        drTemp["CRDAT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        drTemp["MONAM"] = "";
                        drTemp["MODAT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    if (Convert.ToInt32(strCycNoFrom.Substring(1, strCycNoFrom.Length - 1)) > Convert.ToInt32(strCycNoTo.Substring(1, strCycNoTo.Length - 1)))
                    {
                        drData["REMSG"] = "CYCNO OUT OF RANGE";
                        msgFlag = true;
                    }
#endregion
                    #region 上传至OA
                    if (objCounting.AddCountingDataMore(Kostl, dtTemp))
                    {
                        drData["REMSG"] = "QWMS OK";

                        DataTable dtZBA19 = null;
                        if (dtTemp.Rows.Count == 1 && string.IsNullOrEmpty(dtTemp.Rows[0]["MATNR"].ToString()))
                        {
                            dtZBA19 = dtTemp.Clone();
                        }
                        else
                        {
                            dtZBA19 = dtTemp.Copy();
                        }
                        DataTable dtZBA21 = new DataTable();
                        dtZBA19.TableName = "ZBA19";
                        dtZBA21.TableName = "ZBA21";
                        dtZBA21.Columns.Add("WERKS");
                        dtZBA21.Columns.Add("LGORT");
                        dtZBA21.Columns.Add("FLAG");

                        DataRow dr = dtZBA21.NewRow();
                        dr["WERKS"] = drData["WERKS"].ToString().Trim();
                        dr["LGORT"] = drData["LGORT"].ToString().Trim();
                        dr["FLAG"] = strFlag;
                        dtZBA21.Rows.Add(dr.ItemArray);

                        if (!dtZBA19.Columns.Contains("AREA_NO"))
                        {
                            dtZBA19.Columns.Add("AREA_NO");
                        }
                        if (!dtZBA19.Columns.Contains("DCODE"))
                        {
                            dtZBA19.Columns.Add("DCODE");
                        }
                        //删除不需要的栏位
                        dtZBA19.Columns.Remove("MANDT");
                        dtZBA19.Columns.Remove("COMCD");
                        dtZBA19.Columns.Remove("CRDAT");
                        dtZBA19.Columns.Remove("MODAT");
                        dtZBA19.Columns.Remove("MONAM");

                        //按照SAP文档栏位重新排序
                        dtZBA19.Columns["WERKS"].SetOrdinal(0);
                        dtZBA19.Columns["STATS"].SetOrdinal(1);
                        dtZBA19.Columns["CYCNO"].SetOrdinal(2);
                        dtZBA19.Columns["INSMK"].SetOrdinal(3);
                        dtZBA19.Columns["LGORT"].SetOrdinal(4);
                        dtZBA19.Columns["LOCAT"].SetOrdinal(5);
                        dtZBA19.Columns["MATNR"].SetOrdinal(6);
                        dtZBA19.Columns["MENGE"].SetOrdinal(7);
                        dtZBA19.Columns["CKQTY"].SetOrdinal(8);
                        dtZBA19.Columns["CKDAT"].SetOrdinal(9);
                        dtZBA19.Columns["CHARG"].SetOrdinal(10);
                        dtZBA19.Columns["KOSTL"].SetOrdinal(11);
                        dtZBA19.Columns["AREA_NO"].SetOrdinal(12);
                        dtZBA19.Columns["DCODE"].SetOrdinal(13);
                        dtZBA19.Columns["CRNAM"].SetOrdinal(14);

                        //按照SAP文档列名重命名
                        dtZBA19.Columns["STATS"].ColumnName = "STATUS";
                        dtZBA19.Columns["CYCNO"].ColumnName = "TICKET";
                        dtZBA19.Columns["INSMK"].ColumnName = "BSTAR";
                        dtZBA19.Columns["MENGE"].ColumnName = "BUCHM";
                        dtZBA19.Columns["CKQTY"].ColumnName = "RIMENGE";
                        dtZBA19.Columns["CKDAT"].ColumnName = "CUNT_DTE";
                        dtZBA19.Columns["KOSTL"].ColumnName = "DEPT_NO";
                        dtZBA19.Columns["CRNAM"].ColumnName = "SNAME";

                        bool blResult = false;

                        string strMessage = string.Empty;
                        string[] strList = { drData["UID"].ToString().Trim() };


                        OAMM.ICMSServiceClient objOAMM = new QWMS.OAMM.ICMSServiceClient();
                        //blResult= objOAMM.LogisticsKeyinDataInterfaceForQWMS(out strMessage, "QA2", dtZBA19, dtZBA21, strList, UserData.UserId);//SAP测试环境

                        blResult = objOAMM.LogisticsKeyinDataInterfaceForQWMS(Mandt, dtZBA19, dtZBA21, strList, UserData.UserId, out strMessage);
                        if (blResult)
                        {

                            //所有前置作业完成
                            drData["REMSG"] = "OK";
                        }
                        else
                        {
                            drData["REMSG"] = "OA Failed!!!" + strMessage;
                            msgFlag = true;
                        }
                    }
                    #endregion
                    #endregion
                    Thread.Sleep(500);
                }
                if (msgFlag)
                {
                    stsWarning.Text = "已完成保存步骤，但部分数据保存错误。请排查！！！";
                }
                else
                {
                    stsWarning.Text = "已全部保存成功！！";
                }
                this.progBar.Value++;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }
        #endregion

        #region 委托调用 progress delegate
        /// <summary>  
        /// 我被委托调用,专门设置进度条最大值的  
        /// </summary>  
        /// <param name="maxValue"></param>  
        private void setMax(int maxValue)
        {
            this.progBar.Maximum = maxValue;
        }
        /// <summary>  
        /// 我被委托调用,专门设置进度条当前值的  
        /// </summary>  
        /// <param name="nowValue"></param>  
        private void setNow(int nowValue)
        {
            this.progBar.Value++;
            if (this.progBar.Value == this.progBar.Maximum)
            {
                this.progBar.Visible = false;
            }
        }
        /// <summary>  
        /// 线程开始事件,设置进度条最大值  
        /// 但是我不能直接操作进度条,需要一个委托来替我完成  
        /// </summary>  
        /// <param name="sender">ThreadMethod函数中传过来的最大值</param>  
        /// <param name="e"></param>  
        private void method_threadStartEvent(object sender, EventArgs e)
        {
            int maxValue = Convert.ToInt32(sender);
            maxValueDelegate max = new maxValueDelegate(setMax);
            this.Invoke(max, maxValue);
        }
        /// <summary>  
        /// 线程执行中的事件,设置进度条当前进度  
        /// 但是我不能直接操作进度条,需要一个委托来替我完成  
        /// </summary>  
        /// <param name="sender">ThreadMethod函数中传过来的当前值</param>  
        /// <param name="e"></param>  
        void method_threadEvent(object sender, EventArgs e)
        {
            int nowValue = Convert.ToInt32(sender);
            nowValueDelegate now = new nowValueDelegate(setNow);
            this.Invoke(now, nowValue);
        }


        //线程开始的时候调用的委托  
        private delegate void maxValueDelegate(int maxValue);
        //线程执行中调用的委托  
        private delegate void nowValueDelegate(int nowValue);
        #endregion

    }
}
