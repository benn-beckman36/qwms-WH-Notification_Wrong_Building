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
using System.Text.RegularExpressions;


namespace QWMS
{
    public partial class InventoryCheck_PrepareProcess : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strInsmk = "";
        private DataTable dtData = new DataTable();
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private string strUID = string.Empty;
    //    private string strNow = string.Empty;
        OAMM.ICMSServiceClient objOAMM = new OAMM.ICMSServiceClient();

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

        public string Kostl
        {
            get
            {
                return txtKostl.Text.Trim();
            }
            set
            {
                txtKostl.Text = value;
            }
        }
        # endregion


        public InventoryCheck_PrepareProcess()
        {
            InitializeComponent();
        }

        public InventoryCheck_PrepareProcess(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

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
                    //秀出Status的資料
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

        # region Plant Selected
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        # region ShowDdlLgort
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

        # region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {    

                DataGridViewTextBoxColumn dgvcCycno = new DataGridViewTextBoxColumn();
                dgvcCycno.DataPropertyName = "CYCNO";
                dgvcCycno.HeaderText = "Counting No";
                dgvcCycno.ReadOnly = true;
                dgvcCycno.Width = 100;
                dgvData.Columns.Add(dgvcCycno);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No";
                dgvcKostl.ReadOnly = true;
                dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                dgvcStats.DataPropertyName = "STATS";
                dgvcStats.HeaderText = "Status";
                dgvcStats.ReadOnly = true;
                dgvData.Columns.Add(dgvcStats);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 60;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcCkqty = new DataGridViewTextBoxColumn();
                dgvcCkqty.DataPropertyName = "CKQTY";
                dgvcCkqty.HeaderText = "CheckQty";
                dgvcCkqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcCkqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "CKDAT";
                dgvcIndat.HeaderText = "Counging Date";
                dgvcIndat.ReadOnly = true;
                dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        # region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strIsmrg = "N";
            string strIsCombine = "N";
            float intCount = 0;
            long intCycno;
            string strInitial = "";
            string strCycno = "";
            string strNoLen = "";
            stsWarning.Text = "";

            try
            {
                if (chkIsCombine.Checked)
                {
                    strIsCombine = "Y";
                }
                if (chkIsmrg.Checked)
                {
                    strIsmrg = "Y";
                }
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }

                if (strWerks == "" || strLgort == "" || Kostl == "")
                {
                    stsWarning.Text = "Plant,storage and dept no can't be empty!!";
                    return;
                }

                if (txtInsmk0.Text.Trim() != "")
                {
                    intCount++;
                    strInitial = txtInsmk0.Text.Trim();
                    strInsmk = "0";
                }

                if (txtInsmkG.Text.Trim() != "")
                {
                    intCount++;
                    strInitial = txtInsmkG.Text.Trim();
                    strInsmk = "G";
                }

                if (txtInsmkJ.Text.Trim() != "")
                {
                    intCount++;
                    strInitial = txtInsmkJ.Text.Trim();
                    strInsmk = "J";
                }

                if (txtInsmkS.Text.Trim() != "")
                {
                    intCount++;
                    strInitial = txtInsmkS.Text.Trim();
                    strInsmk = "S";
                }

                if (intCount == 0)
                {
                    stsWarning.Text = "Please input one initial number!!";
                    return;
                }

                if (intCount > 1)
                {
                    stsWarning.Text = "You can only input one initial number!!";
                    return;
                }

                if (!CheckIsCharacter(strInitial.Substring(0, 1)))
                {
                    stsWarning.Text = "The first character you input should be A~Z!!";
                    return;
                }

                for (int i = 1; i <= strInitial.Length - 1; i++)
                {
                    if (!CheckIsNumber(strInitial.Substring(i, 1)))
                    {
                        stsWarning.Text = "The characters you input should be numeric(0~9) except for first char!!";
                        return;
                    }
                }

                intCycno = Convert.ToInt64(strInitial.Substring(1, strInitial.Length - 1));
               // strNow = DateTime.Now.ToString();
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                //objCounting.ExecLog(strWerks, strLgort, Kostl, 0, "Begin", "", "UPLOAD",strNow);
                dtData = objCounting.QueryCountingPrepareDataMore(Kostl, strInsmk, strIsmrg, strIsCombine);
                //if (dtData.Rows.Count == 0)
                //{
                //    ShowDataGrid();
                //    stsWarning.Text = "No Data!!";
                //    return;
                //}
                //else
                //{
                    btnSave.Enabled = true;
                    btnDownload.Enabled = true;
                    strNoLen = "";
                    for (int i = 0; i < strInitial.Length - 1; i++)
                    {
                        strNoLen += "0";
                    }
                    //將盤點票號碼加入DataTable
                    if (dtData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            strCycno = strInitial.Substring(0, 1) + Convert.ToInt64(intCycno + i).ToString(strNoLen);
                            dtData.Rows[i]["CYCNO"] = strCycno;
                        }
                        //获取子仓截止盘点票号,较验是否超过盘点截止票号
                        DataTable dtCycNo = new DataTable();
                        dtCycNo = objOAMM.ICInventoryDataForEC(strWerks, strLgort, "2");
                        if (!string.IsNullOrEmpty(txtKostl.Text.Trim().ToUpper()))
                        {
                            dtCycNo = dtCycNo.Select("SUBSTORAGE='" + txtKostl.Text.Trim().ToUpper() + "'").CopyToDataTable();
                        }
                        dtCycNo.Columns["PLANT"].ColumnName = "WERKS";
                        dtCycNo.Columns["Storage"].ColumnName = "LGORT";
                        //DataTable dtCycNo = objCounting.QueryCycNoFromOA(strWerks, strLgort, txtKostl.Text.Trim().ToUpper());
                        string strCycNoTo = dtCycNo.Rows[0]["TICKETTO"].ToString();
                        int a = Convert.ToInt32(strCycno.Substring(1));
                        int b = Convert.ToInt32(strCycNoTo.Substring(1));
                        if (a > b)
                        {
                            stsWarning.Text = "该子仓的盘点票号超过盘点结束票号，请重新调整储位，再生成盘点票号";
                            return;
                        }
                    }
                    ShowDataGrid();
                //}
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

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

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
               
                this.txtInsmk0.Text = "";
                this.txtInsmkG.Text = "";
                this.txtInsmkJ.Text = "";
                this.txtInsmkS.Text = "";
                this.txtKostl.Text = "";
                this.btnPrint.Enabled = false;
                this.btnPrintLabel.Enabled = false;
                this.btnSave.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.chkIsCombine.Checked = false;
                this.chkIsmrg.Checked = false;
                this.lblCount.Text = "";
                this.btnDownload.Enabled = false;
                this.strUID = "";
                stsWarning.Text = "";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }
                ReportPrint objReportPrint = new ReportPrint(UserData, "COUNTPREPARE", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }

        # region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strFlage = "";
            this.btnSave.Enabled = false;
            stsWarning.Text = "正在保存数据，请勿关闭窗体....";
            //DataTable dtCycNo = objCounting.QueryCycNoFromOA(strWerks, strLgort,"");
            DataTable dtCycNo = objOAMM.ICInventoryDataForEC(strWerks, strLgort, "2"); 
            dtCycNo.Columns["PLANT"].ColumnName = "WERKS";
            dtCycNo.Columns["Storage"].ColumnName = "LGORT";
            if (dtCycNo.Rows.Count == 0)
            {
                stsWarning.Text = "该仓别未有待上传的子仓，请确认";
                return;
            }
            else
            {
                //判断是否为最后一个子仓
                if (dtCycNo.Rows.Count > 1)
                {
                    strFlage = "0";
                }
                else
                {
                    strFlage = "1";
                }
            }

            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            if (objCounting.AddCountingDataMore(Kostl, dtData))
            {
                stsWarning.Text = "Save QWMS OK!";

                DataTable dtZBA19 = dtData.Copy();
                DataTable dtZBA21 = new DataTable();
                dtZBA19.TableName = "ZBA19";
                dtZBA21.TableName = "ZBA21";
                dtZBA21.Columns.Add("WERKS");
                dtZBA21.Columns.Add("LGORT");
                dtZBA21.Columns.Add("FLAG");

                DataRow dr =dtZBA21.NewRow();
                dr["WERKS"] = strWerks;
                dr["LGORT"] = strLgort;
                dr["FLAG"] = strFlage;
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

                bool blResult=false;
                string strMessage=string.Empty;
                string[] strList = { strUID };
                //blResult= objOAMM.LogisticsKeyinDataInterfaceForQWMS(out strMessage, "QA2", dtZBA19, dtZBA21, strList, UserData.UserId);//SAP测试环境
                //blResult = objOAMM.LogisticsKeyinDataInterfaceForQWMS(out strMessage, Mandt, dtZBA19, dtZBA21, strList, UserData.UserId);
                blResult = objOAMM.LogisticsKeyinDataInterfaceForQWMS(strMandt, dtZBA19, dtZBA21, strList, UserData.UserId, out strMessage);
               // objCounting.ExecLog(strWerks, strLgort, Kostl, dtZBA19.Rows.Count, "End", strMessage, "UPLOAD",strNow);
                if (blResult)
                {
                    stsWarning.Text = "同步OA盘点管理系统成功";
                }
                else
                {
                    stsWarning.Text = "同步OA盘点管理系统失败" + strMessage;
                }

                this.btnSave.Enabled = false;
                this.btnPrint.Enabled = true;
                this.btnPrintLabel.Enabled = true;
            }
            else
            {
                stsWarning.Text = "Update fail!" + objCounting.ERRMSG;
                return;
            }
        }
        # endregion

        # region Resize
        private void InventoryCheck_PrepareProcess_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.4), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;

        }
        # endregion

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }
                ReportPrint objReportPrint = new ReportPrint(UserData, "COUNTINGLABEL", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }

        private void txtKostl_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtInsmk0.Text = "";
            txtInsmkG.Text = "";
            txtInsmkJ.Text = "";
            txtInsmkS.Text = "";
            stsWarning.Text = "";
            string strSubStorage = txtKostl.Text.ToUpper().Trim();
            strWerks = cmbWerks.Text;
            strLgort = cmbLgort.Text;
            string strInsmk = "";
            dtData.Rows.Clear();
            dgvData.DataSource = dtData;
            if (e.KeyChar == (char)13 || e.KeyChar.ToString()=="1")
            {
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "请选择厂区仓别";
                    return;
                }
                if (strSubStorage.Length >= 4)
                {
                    try
                    {
                        string strSubEndWith=strSubStorage.Substring(4); //判断子仓结尾，再分辨库别
                        if (strLgort == strSubStorage.Substring(0, 4))
                        {
                            //子仓为主仓，仓别是AS10的情况,库别抓库存的第一笔资料
                            if (strSubStorage.Length == 4 || strSubStorage.StartsWith("AS10") || strSubEndWith=="A" || strSubEndWith=="B" || strSubEndWith=="C" || strSubEndWith=="D" )
                            {
                                strInsmk = objCounting.QueryStorageInsmk(strWerks, strLgort);
                            }
                            //子仓超过4码，且结尾为  BL,U,I,R
                            else
                            {
                                if (strSubEndWith == "BL")
                                {
                                    strInsmk = "J";
                                }
                                if (strSubEndWith == "U")
                                {
                                    strInsmk = "G";
                                }
                                if (strSubEndWith == "I")
                                {
                                    strInsmk = "0";
                                }
                                if (strSubEndWith == "R")
                                {
                                    strInsmk = "S";
                                }
                            }
                            ReturnCycNoByInsmk(strWerks, strLgort, strSubStorage, strInsmk);
                        }
                        else
                        {
                            stsWarning.Text = "输入的子仓与选择的仓别不符合，请确认";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("数据转化异常");
                        return;
                    }
                    }
                    else
                    {
                        stsWarning.Text = "请输入正确的子仓";
                        return;
                    }
                     
            }
        }

        public void ReturnCycNoByInsmk(string strWerks,string strLgort,string strSubStorage,string strInsmk)
        {
            //DataTable dtCycNo=objCounting.QueryCycNoFromOA(strWerks, strLgort,"");
            DataTable dtCycNo = objOAMM.ICInventoryDataForEC(strWerks, strLgort, "2");
            dtCycNo.Columns["PLANT"].ColumnName = "WERKS";
            dtCycNo.Columns["Storage"].ColumnName = "LGORT";
            if (dtCycNo.Rows.Count > 0)
            {
                DataRow[] drSubStorage = dtCycNo.Select(" subStorage='" + strSubStorage + "' ");

                if (drSubStorage.Length > 0)
                {
                    string strCycNo = drSubStorage[0]["TICKETFROM"].ToString();
                    strUID = drSubStorage[0]["UID"].ToString();
                    if (strCycNo == "")
                    {
                        stsWarning.Text = "该子仓在盘点管理系统未有盘点票号，请先生成盘点票号";
                        return;
                    }
                    if (strInsmk == "G" || strInsmk=="")
                    {
                        txtInsmk0.Text = "";
                        txtInsmkG.Text = strCycNo;
                        txtInsmkJ.Text = "";
                        txtInsmkS.Text = "";
                    }
                    if (strInsmk == "0")
                    {
                        txtInsmk0.Text = strCycNo;
                        txtInsmkG.Text = "";
                        txtInsmkJ.Text = "";
                        txtInsmkS.Text = "";
                    }
                    if (strInsmk == "J")
                    {
                        txtInsmk0.Text = "";
                        txtInsmkG.Text = "";
                        txtInsmkJ.Text = strCycNo;
                        txtInsmkS.Text = "";
                    }
                    if (strInsmk == "S")
                    {
                        txtInsmk0.Text = "";
                        txtInsmkG.Text = "";
                        txtInsmkJ.Text = "";
                        txtInsmkS.Text = strCycNo;
                    }
                }
                else
                {
                    stsWarning.Text = "未查到该子仓的盘点票信息或该子仓已上传盘点管理系统，请确认！";
                    return;
                }
            }
            else
            {
                stsWarning.Text = "该仓别未有待上传的子仓，请确认";
                return;
            }

          
           
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (dtData.Rows.Count > 0)
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    string strFileName = saveFile.FileName;
                    ClaExeclHelper cls = new ClaExeclHelper();
                    cls.DatatableToExcel(dtData, strFileName, txtKostl.Text.ToString().Trim());
                }
            }
            else
            {
                stsWarning.Text = "请先产生盘点明细";
                return;
            }

          
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            if (!string.IsNullOrEmpty(strWerks) && !string.IsNullOrEmpty(strLgort))
            {
                DataTable dtCycNo = objOAMM.ICInventoryDataForEC(strWerks, strLgort, "2");
                dtCycNo.Columns["PLANT"].ColumnName = "WERKS";
                dtCycNo.Columns["Storage"].ColumnName = "LGORT";
                if (dtCycNo.Rows.Count > 0)
                {
                    txtKostl.Text = dtCycNo.Rows[0]["subStorage"].ToString();
                     KeyPressEventArgs charEnter=new KeyPressEventArgs('1');
                     txtKostl_KeyPress(null, charEnter);
                }
                else
                {
                    stsWarning.Text = "该仓别未有待上传的盘点信息，请确认！！";
                    return;
                }
            }

        }

    








    }
}
