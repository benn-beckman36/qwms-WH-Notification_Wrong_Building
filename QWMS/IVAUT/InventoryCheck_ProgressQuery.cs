using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class InventoryCheck_ProgressQuery : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMatnr = "";
        private string strLocat = "";
        private string strType = "";
        private string strInvNo = "";
        private string strFromLocat = "";
        private string strToLocat = "";
        string strStartDate = "";
        string strEndDate = "";
        int flag = 0;
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private DataTable dtProgress=new DataTable();
        private DataTable dtInvNO=new DataTable();
        private DataTable dtResultCopy = new DataTable();
        private DataTable dtInvContent = new DataTable();
        DataTable dtResult = new DataTable();
        private FileInfo fi;
        private StreamWriter sw;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private StorageIn objStorageIn;        


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

        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }

        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
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

        #region 构造函数
        public InventoryCheck_ProgressQuery(UserInfo varUserData, string strProgid)
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
                objStorageIn = new StorageIn(UserData, Progid);

                this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
                this.sfdSaveFile.FileName = "InvFile.xls";
                this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";

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
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            dgvData.RowTemplate.Height = 20;
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcCrtim = new DataGridViewTextBoxColumn();
                dgvcCrtim.DataPropertyName = "STATM";
                dgvcCrtim.HeaderText = "开始时间";
                dgvcCrtim.ReadOnly = true;
                dgvcCrtim.Width = 100;
                dgvData.Columns.Add(dgvcCrtim);

                DataGridViewTextBoxColumn dgvcCftim = new DataGridViewTextBoxColumn();
                dgvcCftim.DataPropertyName = "ENDTM";
                dgvcCftim.HeaderText = "结束时间";
                dgvcCftim.ReadOnly = true;
                dgvcCftim.Width = 100;
                dgvData.Columns.Add(dgvcCftim);

                DataGridViewLinkColumn dgvcInvNo = new DataGridViewLinkColumn();
                dgvcInvNo.HeaderText = "盘点票号";
                dgvcInvNo.DataPropertyName = "INVNO";
                dgvcInvNo.ActiveLinkColor = Color.White;
                dgvcInvNo.LinkBehavior = LinkBehavior.SystemDefault;
                dgvcInvNo.LinkColor = Color.Blue;
                dgvcInvNo.TrackVisitedState = true;
                //dgvcInvNo.VisitedLinkColor = Color.Black;
                dgvcInvNo.Width = 150;
                dgvData.Columns.Add(dgvcInvNo);


                DataGridViewTextBoxColumn dgvcCountInvLocat = new DataGridViewTextBoxColumn();
                dgvcCountInvLocat.DataPropertyName = "CountInvLocat";
                dgvcCountInvLocat.HeaderText = "总储位";
                dgvcCountInvLocat.ReadOnly = true;
                dgvcCountInvLocat.Width = 50;
                dgvData.Columns.Add(dgvcCountInvLocat);

                DataGridViewTextBoxColumn dgvcInvLocat = new DataGridViewTextBoxColumn();
                dgvcInvLocat.DataPropertyName = "InvLocat";
                dgvcInvLocat.HeaderText = "已盘";
                dgvcInvLocat.ReadOnly = true;
                dgvcInvLocat.Width = 50;
                dgvData.Columns.Add(dgvcInvLocat);

                DataGridViewLinkColumn dgvcNoInvLocat = new DataGridViewLinkColumn();
                dgvcNoInvLocat.DataPropertyName = "NoInvLocat";
                dgvcNoInvLocat.HeaderText = "未盘";
                dgvcNoInvLocat.ActiveLinkColor = Color.White;
                dgvcNoInvLocat.LinkBehavior = LinkBehavior.SystemDefault;
                dgvcNoInvLocat.LinkColor = Color.Blue;
                dgvcNoInvLocat.TrackVisitedState = true;
                dgvcNoInvLocat.Width = 50;
                dgvData.Columns.Add(dgvcNoInvLocat);

                DataGridViewTextBoxColumn dgvcProgress = new DataGridViewTextBoxColumn();
                dgvcProgress.DataPropertyName = "Progress";
                dgvcProgress.HeaderText = "进度";
                dgvcProgress.ReadOnly = true;
                dgvcProgress.Width = 50;
                dgvData.Columns.Add(dgvcProgress);

                DataGridViewTextBoxColumn dgvcCrwho = new DataGridViewTextBoxColumn();
                dgvcCrwho.DataPropertyName = "CRWHO";
                dgvcCrwho.HeaderText = "盘点人";
                dgvcCrwho.ReadOnly = true;
                dgvcCrwho.Width = 100;
                dgvData.Columns.Add(dgvcCrwho);

                dgvData.DataSource = dtProgress;
                lblCount3.Text = dtProgress.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string strINV = "";
                DataTable dtNInvLocat = new DataTable();
                strINV = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                           CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                if (dgvData.Columns[e.ColumnIndex].HeaderText.ToString().Trim()=="盘点票号"  && e.RowIndex>=0 && e.ColumnIndex==4)
                { 
                    dtInvContent = objCounting.InvContent(strINV);
                    InventoryCheck_InvContent objInventoryCheck_InvContent = new InventoryCheck_InvContent(dtInvContent,0);
                    objInventoryCheck_InvContent.ShowDialog();
                }
                if (dgvData.Columns[e.ColumnIndex].HeaderText.ToString().Trim() == "未盘" && e.RowIndex >= 0 && e.ColumnIndex == 7)
                {
                    dtNInvLocat = objCounting.NInvLocat(strINV);
                    InventoryCheck_InvContent objInventoryCheck_InvContent = new InventoryCheck_InvContent(dtNInvLocat,1);
                    objInventoryCheck_InvContent.ShowDialog();
                }
                if (dgvData.Columns[e.ColumnIndex].HeaderText.ToString().Trim() == "勾选" && e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    if ((bool)dgvData.Rows[e.RowIndex].Cells[0].EditedFormattedValue == true)
                    {
                        dgvData.Rows[e.RowIndex].Cells[0].Value = false;
                        dgvData.Rows[e.RowIndex].Cells[5].Value = dtResultCopy.Rows[e.RowIndex]["SCMENGE"].ToString().Trim();
                        dgvData.Rows[e.RowIndex].Cells[6].Value = dtResultCopy.Rows[e.RowIndex]["MGDIF"].ToString().Trim();
                        dgvData.Rows[e.RowIndex].Cells[8].Value = dtResultCopy.Rows[e.RowIndex]["RMARK"].ToString().Trim();
                    }
                    else
                    {
                        dgvData.Rows[e.RowIndex].Cells[0].Value = true;
                        dgvData.Rows[e.RowIndex].Cells[5].Value = dgvData.Rows[e.RowIndex].Cells[4].Value;
                        dgvData.Rows[e.RowIndex].Cells[6].Value = 0;
                        dgvData.Rows[e.RowIndex].Cells[8].Value = "";
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-dgvData_CellContentClick()");

            }
        }
        #endregion

        #region ShowResult
        private void ShowResult()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            dgvData.RowTemplate.Height = 20;
            try
            {
                DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
                ck.HeaderText = "勾选";
                ck.ReadOnly = true;
                ck.Width = 30;
                dgvData.Columns.Add(ck);

                DataGridViewTextBoxColumn dgvcInvNo = new DataGridViewTextBoxColumn();
                dgvcInvNo.DataPropertyName = "INVNO";
                dgvcInvNo.HeaderText = "盘点票号";
                dgvcInvNo.ReadOnly = true;
                dgvcInvNo.Width = 140;
                dgvData.Columns.Add(dgvcInvNo);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 60;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 120;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "QWMS数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcSCMenge = new DataGridViewTextBoxColumn();
                dgvcSCMenge.DataPropertyName = "SCMENGE";
                dgvcSCMenge.HeaderText = "刷入数量";
                dgvcSCMenge.ReadOnly = false;
                dgvcSCMenge.DefaultCellStyle.BackColor = Color.MistyRose;
                dgvcSCMenge.Width = 60;
                dgvData.Columns.Add(dgvcSCMenge);

                DataGridViewTextBoxColumn dgvcMgdif = new DataGridViewTextBoxColumn();
                dgvcMgdif.DataPropertyName = "MGDIF";
                dgvcMgdif.HeaderText = "差异";
                dgvcMgdif.ReadOnly = false;
                dgvcMgdif.DefaultCellStyle.BackColor = Color.MistyRose;
                dgvData.Columns.Add(dgvcMgdif);

                DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                dgvcStats.DataPropertyName = "STATS";
                dgvcStats.HeaderText = "状态";
                dgvcStats.ReadOnly = true;
                dgvcStats.Width = 50;
                dgvData.Columns.Add(dgvcStats);

                DataGridViewTextBoxColumn dgvcRmark = new DataGridViewTextBoxColumn();
                dgvcRmark.DataPropertyName = "RMARK";
                dgvcRmark.HeaderText = "异常";
                dgvcRmark.ReadOnly = false;
                dgvcRmark.DefaultCellStyle.BackColor = Color.MistyRose;
                dgvcRmark.Width = 100;
                dgvData.Columns.Add(dgvcRmark);

                DataGridViewTextBoxColumn dgvcCftim = new DataGridViewTextBoxColumn();
                dgvcCftim.DataPropertyName = "CFTIM";
                dgvcCftim.HeaderText = "盘点时间";
                dgvcCftim.ReadOnly = true;
                dgvcCftim.Width = 100;
                dgvData.Columns.Add(dgvcCftim);

                DataGridViewTextBoxColumn dgvcCrwho = new DataGridViewTextBoxColumn();
                dgvcCrwho.DataPropertyName = "CRWHO";
                dgvcCrwho.HeaderText = "盘点人";
                dgvcCrwho.ReadOnly = true;
                dgvcCrwho.Width = 80;
                dgvData.Columns.Add(dgvcCrwho);

                dgvData.DataSource = dtResult;
                dtResultCopy = dtResult.Copy();
                lblCount3.Text = dtResult.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        private void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelect.Checked)
            {
                for (int i = 0; i < dgvData.Rows.Count-1; i++)
                {
                    dgvData.Rows[i].Cells[0].Value = true;
                    dgvData.Rows[i].Cells[5].Value = dgvData.Rows[i].Cells[4].Value;
                    dgvData.Rows[i].Cells[6].Value = 0;
                    dgvData.Rows[i].Cells[8].Value = "";
                }
            }
            else
            {
                for (int i = 0; i < dgvData.Rows.Count-1; i++)
                {
                    dgvData.Rows[i].Cells[0].Value = false;
                    dgvData.Rows[i].Cells[5].Value = dtResultCopy.Rows[i]["SCMENGE"].ToString().Trim();
                    dgvData.Rows[i].Cells[6].Value = dtResultCopy.Rows[i]["MGDIF"].ToString().Trim();
                    dgvData.Rows[i].Cells[8].Value = dtResultCopy.Rows[i]["RMARK"].ToString().Trim();
                }
            }
        }
        #endregion

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.btnDownload.Enabled = false;
            this.cbSelect.Visible = false;
        }
        # endregion

        # region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objCounting.InvWerks("RInv");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["CTRLNM"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.dtInvNO.Clear();
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            ShowDdlLgort();
            //RTShowInv();
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
                    dtTemp = objCounting.InvLgort(strWerks);
                }
                else
                {
                    strWerks = "";
                    dtTemp = objCounting.InvLgort(strWerks);
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                        //if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        //{
                        //    cmbLgort.SelectedIndex = i;
                        //    //return;
                        //}
                        //else
                        //{
                        //    cmbLgort.SelectedIndex = 0;
                        //}
                        //cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    }
                    cmbLgort.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            //RTShowInv();
        }
        # endregion

        #region ShowInv
        private void RTShowInv()
        {
            try
            {
                stsWarning.Text = "";
                cmbInvNo.Items.Clear();
                strStartDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
                strEndDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
                dtInvNO = new DataTable();
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                        CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtInvNO = objCounting.ShowInv(strStartDate, strEndDate);
                for (int i = 0; i < dtInvNO.Rows.Count; i++)
                {
                    cmbInvNo.Items.Add(dtInvNO.Rows[i]["INVNO"].ToString());
                }
                if (dtInvNO.Rows.Count == 0)
                {
                    stsWarning.Text = "无盘点票!";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-RTShowInv()");
            }
        }
        private void cmbInvNo_Click(object sender, EventArgs e)
        {
            RTShowInv();
        }
        private void cmbInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strInvNo = cmbInvNo.Items[cmbInvNo.SelectedIndex].ToString();
        }

        #endregion

        #region INVProgress
        private void  INVProgress()
        {
            stsWarning.Text = "";
            strStartDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
            strEndDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
            strInvNo = cmbInvNo.Text.ToString().Trim();
            try 
            {
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                     CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtProgress = objCounting.InvProgressDT(strInvNo,strStartDate, strEndDate);              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-INVProgress()");
            }
        }

        #endregion

        #region btnQuery
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.btnDownload.Enabled = true;
            lblCount3.Text = "0 records";
            stsWarning.Text = "";
            try
            {
                INVProgress();
                ShowDataGrid();
                flag = 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnQuery_Click()");
            }
        }
        #endregion

        #region 结果按钮（只显示盘点异常）
        private void btnException_Click(object sender, EventArgs e)
        {
            this.cbSelect.Visible = true;
            this.cbSelect.Checked = false;
            this.btnDownload.Enabled = true;
            this.dgvData.DataSource = null;
            lblCount3.Text = "0 records";
            stsWarning.Text = "";
            strFromLocat = txtFromLocat.Text.ToString().Trim();
            strToLocat = txtToLocat.Text.ToString().Trim();
            strMatnr = txtMatnr.Text.ToString().Trim();
            try
            {
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                   CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtResult = objCounting.InvNOResult(strInvNo, strStartDate, strEndDate, strFromLocat, strToLocat, strMatnr);
                if (dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("无数据！请重新设置查询条件！");
                }
                else
                {
                    ShowResult();
                }
                flag = 2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnException_Click()");
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            flag = 0;
            stsWarning.Text = "";
            this.dgvData.DataSource = null;
            this.dtProgress.Clear();
            this.dtInvNO.Clear();
            this.dtResult.Clear();
            this.dtResultCopy.Clear();
            this.txtFromLocat.Text = "";
            this.lblCount3.Text = "0 records";
            this.btnDownload.Enabled = false;
            this.cmbInvNo.Text = "";
            this.cbSelect.Visible = false;
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 日期改变
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            //RTShowInv();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            //RTShowInv();
        }
        #endregion

        #region DownLoad：导出

        private void btnDownload_Click(object sender, System.EventArgs e)
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
                throw new Exception(ex.Message + "<-btnDownload_Click()");
            }
        }
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (flag==2)
                {                   
                    strLine = "厂区\t仓别\t盘点票号\t储位\t料号\tQWMS数量\t刷入数量\t差异\t状态\t异常\t盘点时间\t盘点人";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += strWerks + "\t";
                        strLine += strLgort + "\t";
                        strLine += dtResult.Rows[i]["INVNO"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["SCMENGE"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["MGDIF"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["STATS"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["RMARK"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["CFTIM"].ToString() + "\t";
                        strLine += dtResult.Rows[i]["CRWHO"].ToString();
                        sw.WriteLine(strLine);
                    }
                }
                if(flag==1)
                {
                    strLine = "厂区\t仓别\t创建时间\t结束时间\t盘点票号\t总储位\t已盘\t未盘\t进度\t盘点人";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtProgress.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtProgress.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["LGORT"].ToString() + "\t";
                         strLine += dtProgress.Rows[i]["CRTIM"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["CFTIM"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["INVNO"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["CountInvLocat"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["InvLocat"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["NoInvLocat"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["Progress"].ToString() + "\t";
                        strLine += dtProgress.Rows[i]["CRWHO"].ToString() + "\t";
                        sw.WriteLine(strLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
                flag = 0;
            }

        }
        #endregion

        //#region Resize
        //private void InventoryCheck_ProgressQuery_Resize(object sender, EventArgs e)
        //{
        //    panel1.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel1.Size.Height);
        //    if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
        //        stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        //}
        //#endregion

        #region 处理异常
        private void btnEX_Click(object sender, EventArgs e)
        {
            DataTable dtResultUpdate = new DataTable();
            DataTable dtResultDC = new DataTable();
            dtResultUpdate = dtResult.Clone();
            dtResultDC = dtResult.Clone();
            bool boolReturn;
            string[] strSplit = new string[5];
            string strDacod = "";
            string strScdac = "";
            string strdc_after = "";
            string strVendorcode = "";
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                DataRow dr = dtResultDC.NewRow();
                if ((bool)dgvData.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    dr["INVNO"] = dtResult.Rows[i]["INVNO"].ToString().Trim(); 
                    dr["LOCAT"] = dtResult.Rows[i]["LOCAT"].ToString().Trim();
                    dr["MATNR"] = dtResult.Rows[i]["MATNR"].ToString().Trim();

                    dtResultDC.Rows.Add(dr);
                }
                //NVNO,LOCAT,MATNR,MENGE,MENGE-MGDIF AS SCMENGE,MGDIF,STATS,RMARK,CFTIM,CRWHO
            }


            try
            {
                DataTable dtremak = new DataTable();
                for (int i = 0; i < dtResultDC.Rows.Count; i++)
                {
                    string strInvNo = dtResultDC.Rows[i]["INVNO"].ToString().Trim();
                    string strLOCAT = dtResultDC.Rows[i]["LOCAT"].ToString().Trim();
                    string strMATNR = dtResultDC.Rows[i]["MATNR"].ToString().Trim();
                    dtremak = objCounting.InvDateCodeDT(strInvNo, strLOCAT, strMATNR);
                    for (int k = 0; k < dtremak.Rows.Count; k++)
                    {
                        strSplit = dtremak.Rows[k]["RMARK"].ToString().Trim().Replace('；', ';').Split(';');
                        if (strSplit.Length > 1)
                        {
                            strVendorcode = strSplit[1];
                            strScdac = strSplit[2];

                            DialogResult result;
                            result = MessageBox.Show("盘点Scan实物与系统信息不符，是否同步实物D/C信息?", "提示信息", MessageBoxButtons.OKCancel);
                            if (result == DialogResult.OK)
                            {
                                if (objStorageIn.CheckDatecode(Usrnm))
                                {
                                    bool Vencode = false;
                                    strDacod = objCounting.SelectDetail(strMATNR, strLOCAT);
                                    string DC_After = objCounting.WHDCR_Query(strScdac, strVendorcode);
                                    if (DC_After != "")
                                    {
                                        DateTime dtVedat = new DateTime();
                                        dtVedat = Convert.ToDateTime(DC_After);
                                        DC_After = dtVedat.ToString("yyyyMMdd");
                                        Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMATNR, strLOCAT);
                                        strdc_after = DC_After;
                                    }
                                    else
                                    {
                                        string strTemp = objCounting.getDCTrans(strVendorcode, strScdac).ToString();
                                        if (!string.IsNullOrEmpty(strTemp))
                                        {
                                            DataTable dtNewDateCode = new DataTable();
                                            dtNewDateCode.Columns.Add("LIFNR");
                                            dtNewDateCode.Columns.Add("DC_Before");
                                            dtNewDateCode.Columns.Add("DC_After");

                                            DataRow drnr = dtNewDateCode.NewRow();
                                            drnr["LIFNR"] = strVendorcode;
                                            drnr["DC_Before"] = strScdac;
                                            drnr["DC_After"] = strTemp;
                                            dtNewDateCode.Rows.Add(drnr.ItemArray);
                                            string strTransType = "NEW";
                                            objCounting.WHDCR_DML(dtNewDateCode, strTransType);

                                            DC_After = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                            strdc_after = DC_After;
                                            Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMATNR, strLOCAT);
                                        }
                                        else
                                        {
                                            MessageBox.Show("请先维护D/C转换规则");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("盘点Scan实物与系统信息不符,D/C转换请管理人员进行异常处理");
                                    return;
                                }

                            }
                            else
                            {
                                if (objStorageIn.CheckDatecode(Usrnm))
                                {
                                    MessageBox.Show("异常已处理！");
                                }
                                else
                                {
                                    MessageBox.Show("盘点Scan实物与系统信息不符,无异常处理权限");
                                    return;
                                }

                            }

                        }
                    }

                }
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    DataRow dr = dtResultUpdate.NewRow();
                    if ((bool)dgvData.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        dr["INVNO"] = strInvNo;
                        dr["LOCAT"] = dtResult.Rows[i]["LOCAT"].ToString();
                        dr["MATNR"] = dtResult.Rows[i]["MATNR"].ToString();
                        dr["MENGE"] = dtResult.Rows[i]["MENGE"].ToString();
                        dr["SCMENGE"] = dtResult.Rows[i]["SCMENGE"].ToString();
                        dr["MGDIF"] = dtResult.Rows[i]["MGDIF"].ToString();
                        dr["STATS"] = dtResult.Rows[i]["STATS"].ToString();
                        dr["RMARK"] = dtResult.Rows[i]["RMARK"].ToString();
                        dr["CFTIM"] = dtResult.Rows[i]["CFTIM"].ToString();
                        dr["CRWHO"] = dtResult.Rows[i]["CRWHO"].ToString();
                        dtResultUpdate.Rows.Add(dr);
                    }
                }

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (dtResult.Rows[i]["RMARK"].ToString().Trim() == "")
                    {
                        MessageBox.Show("异常原因不能为空！");
                        return;
                    }
                }

                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                        CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                boolReturn = objCounting.InvLogEx(dtResultUpdate);
                if (boolReturn)
                {
                    stsWarning.Text = "Insert OK！";
                    Sound.Play(@"Sound\Success.wav");
                }
                else
                {
                    stsWarning.Text = "Insert Fail！";
                    Sound.Play(@"Sound\ERROR.wav");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnEX_Click()");
            }
        }
        #endregion

    }
}
