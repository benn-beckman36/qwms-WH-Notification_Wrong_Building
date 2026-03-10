using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.IO;

namespace QWMS
{
    public partial class IQC_QueryOverdueInspect : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        string strStartDate = "";
        string strEndDate = "";
        string strStatus = "";
        string strMtype = "";
        private DataTable dtData = new DataTable();
        private StorageData objStorageData;
        private Authority objAuthority;
        private FileInfo fi;

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
        public string Mtype
        {
            get
            {
                return strMtype;
            }
            set
            {
                strMtype = value;
            }
        }

        #endregion

        #region 构造函数

        private IQC_QueryOverdueInspect()
        {
            InitializeComponent();
        }
        public IQC_QueryOverdueInspect(UserInfo _UserData, string strProgid)
            : this()
        {
            UserData = _UserData;

            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageData = new StorageData(UserData, Werks, Lgort);
                objAuthority = new Authority(UserData);
                QCI.QWMS.Replenishment objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);

                //檢查權限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInspect();
                    ShowDdlStatus();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }

                dtpStartDate.Value = DateTime.Now.AddMonths(-1);
                dtpEndDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort
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
        #endregion

        #region ShowDdlInspect
        private void ShowDdlInspect()
        {
            try
            {
                cmbInspResult.Items.Clear();
                cmbInspResult.Items.Add("");
                cmbInspResult.Items.Add("OK");
                cmbInspResult.Items.Add("NG1");
                cmbInspResult.Items.Add("NG2");
                cmbInspResult.Items.Add("REJECT");
                cmbInspResult.Items.Add("WAIVE");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInspect()");
            }
        }
        #endregion

        #region ShowDdlStatus
        private void ShowDdlStatus()
        {
            try
            {
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("");
                cmbStatus.Items.Add("OPEN");
                cmbStatus.Items.Add("ONGOING");
                cmbStatus.Items.Add("CLOSED");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStatus()");
            }
        }
        #endregion

        //        #region


        //            // 创建一个选项列表
        //            List<string> options = new List<string>
        //            {
        //                "",
        //                "EE",
        //                "ME"
        //            };

        //        // 创建一个BindingSource对象，并将选项列表作为数据源
        //        BindingSource bindingSource = new BindingSource();
        //        bindingSource.DataSource = options;

        //            // 将BindingSource对象赋值给下拉框的数据源
        //cmbMtype.DataSource = bindingSource;

        //// 通过设置SelectedIndex属性来选择默认选项
        //cmbMtype.SelectedIndex = 0;
        //        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                this.dtgData.AutoGenerateColumns = false;
                this.dtgData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "Type";
                dgvcType.HeaderText = "Type";
                dgvcType.Name = "Type";
                dgvcType.Width = 50;
                dgvcType.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "Re-Inspection lot";
                dgvcTASKID.Name = "Taskid";
                dgvcTASKID.Width = 180;
                dgvcTASKID.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Name = "Werks";
                dgvcWerks.Width = 100;
                dgvcWerks.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcInputDate = new DataGridViewTextBoxColumn();
                dgvcInputDate.DataPropertyName = "INPUTDATE";
                dgvcInputDate.HeaderText = "Input Date";
                dgvcInputDate.Name = "Chktim";
                dgvcInputDate.Width = 100;
                dgvcInputDate.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcInputDate);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Quanta P/N";
                dgvcMatnr.Name = "Matnr";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcDescri = new DataGridViewTextBoxColumn();
                dgvcDescri.DataPropertyName = "DESCRI";
                dgvcDescri.HeaderText = "Parts Descripsion";
                dgvcDescri.Name = "Descri";
                dgvcDescri.Width = 150;
                dgvcDescri.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcDescri);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor Code";
                dgvcLifnr.Width = 100;
                dgvcLifnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcPrtid = new DataGridViewTextBoxColumn();
                dgvcPrtid.DataPropertyName = "PRTID";
                dgvcPrtid.HeaderText = "Print QR code label Qty";
                dgvcPrtid.Width = 100;
                dgvcPrtid.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcPrtid);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Quantity";
                dgvcMenge.Width = 100;
                dgvcMenge.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcSampleQty = new DataGridViewTextBoxColumn();
                dgvcSampleQty.DataPropertyName = "SampleQty";
                dgvcSampleQty.HeaderText = "Sampling Qty";
                dgvcSampleQty.Width = 100;
                dgvcSampleQty.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcSampleQty);

                DataGridViewTextBoxColumn dgvcTotalQty = new DataGridViewTextBoxColumn();
                dgvcTotalQty.DataPropertyName = "TotalQty";
                dgvcTotalQty.HeaderText = "Total Sampling Qty";
                dgvcTotalQty.Width = 100;
                dgvcTotalQty.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTotalQty);

                DataGridViewTextBoxColumn dgvcManudate = new DataGridViewTextBoxColumn();
                dgvcManudate.DataPropertyName = "MANUDATE";
                dgvcManudate.HeaderText = "Manufacturing Date";
                dgvcManudate.Width = 150;
                dgvcManudate.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcManudate);

                DataGridViewTextBoxColumn dgvcRESULT = new DataGridViewTextBoxColumn();
                dgvcRESULT.DataPropertyName = "RESULT";
                dgvcRESULT.HeaderText = "Inspection Result";
                dgvcRESULT.Name = "Result";
                dgvcRESULT.Width = 100;
                dgvcRESULT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcRESULT);

                DataGridViewTextBoxColumn dgvcNGSymptom = new DataGridViewTextBoxColumn();
                dgvcNGSymptom.DataPropertyName = "NGDES";
                dgvcNGSymptom.HeaderText = "NG Symptom";
                dgvcNGSymptom.Name = "NGSymptom";
                dgvcNGSymptom.Width = 150;
                dgvcNGSymptom.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcNGSymptom);

                DataGridViewTextBoxColumn dgvcInspectionReason = new DataGridViewTextBoxColumn();
                dgvcInspectionReason.DataPropertyName = "Remark";
                dgvcInspectionReason.HeaderText = "Remark";
                dgvcInspectionReason.Name = "InspectionReason";
                dgvcInspectionReason.Width = 150;
                dgvcInspectionReason.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcInspectionReason);

                DataGridViewTextBoxColumn dgvcChktim = new DataGridViewTextBoxColumn();
                dgvcChktim.DataPropertyName = "CHKTIM";
                dgvcChktim.HeaderText = "Inspection Date";
                dgvcChktim.Name = "Chktim";
                dgvcChktim.Width = 100;
                dgvcChktim.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcChktim);

                DataGridViewTextBoxColumn dgvcChknam = new DataGridViewTextBoxColumn();
                dgvcChknam.DataPropertyName = "CHKNAM";
                dgvcChknam.HeaderText = "Inspector ID";
                dgvcChknam.Name = "Chknam";
                dgvcChknam.Width = 100;
                dgvcChknam.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcChknam);

                DataGridViewTextBoxColumn dgvcEngID = new DataGridViewTextBoxColumn();
                dgvcEngID.DataPropertyName = "ENGID";
                dgvcEngID.HeaderText = "EngID";
                dgvcEngID.Name = "EngID";
                dgvcEngID.Width = 120;
                dgvcEngID.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcEngID);

                DataGridViewTextBoxColumn dgvcEXPDAT = new DataGridViewTextBoxColumn();
                dgvcEXPDAT.DataPropertyName = "EXPDAT";
                dgvcEXPDAT.HeaderText = "Exp. Date Before";
                dgvcEXPDAT.Name = "EXPDAT";
                dgvcEXPDAT.Width = 150;
                dgvcEXPDAT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcEXPDAT);

                DataGridViewTextBoxColumn dgvcEXPDAT_AFTER = new DataGridViewTextBoxColumn();
                dgvcEXPDAT_AFTER.DataPropertyName = "EXPDAT_AFTER";
                dgvcEXPDAT_AFTER.HeaderText = "Exp. Date After";
                dgvcEXPDAT_AFTER.Name = "EXPDAT_AFTER";
                dgvcEXPDAT_AFTER.Width = 150;
                dgvcEXPDAT_AFTER.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcEXPDAT_AFTER);

                DataGridViewTextBoxColumn dgvcMAXEXP = new DataGridViewTextBoxColumn();
                dgvcMAXEXP.DataPropertyName = "MAXEXP";
                dgvcMAXEXP.HeaderText = "Max Exp. Before";
                dgvcMAXEXP.Name = "MAXEXP";
                dgvcMAXEXP.Width = 150;
                dgvcMAXEXP.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMAXEXP);

                DataGridViewTextBoxColumn dgvcMAXEXP_AFTER = new DataGridViewTextBoxColumn();
                dgvcMAXEXP_AFTER.DataPropertyName = "EXPDAT_AFTER";
                dgvcMAXEXP_AFTER.HeaderText = "Max Exp. After";
                dgvcMAXEXP_AFTER.Name = "MAXEXP_AFTER";
                dgvcMAXEXP_AFTER.Width = 150;
                dgvcMAXEXP_AFTER.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMAXEXP_AFTER);

                DataGridViewTextBoxColumn dgvcQMEXP = new DataGridViewTextBoxColumn();
                dgvcQMEXP.DataPropertyName = "QMEXP";
                dgvcQMEXP.HeaderText = "QM Extend Date";
                dgvcQMEXP.Name = "QMEXP";
                dgvcQMEXP.Width = 150;
                dgvcQMEXP.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcQMEXP);

                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "Status";
                dgvcStatus.HeaderText = "Status";
                dgvcStatus.Name = "Status";
                dgvcStatus.Width = 120;
                dgvcStatus.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcStatus);

                //DataGridViewTextBoxColumn dgvcMRBNo = new DataGridViewTextBoxColumn();
                //dgvcMRBNo.DataPropertyName = "MRBNO";
                //dgvcMRBNo.HeaderText = "MRB No";
                //dgvcMRBNo.Name = "MRBNo";
                //dgvcMRBNo.Width = 120;
                //dgvcMRBNo.ReadOnly = true;
                //this.dtgData.Columns.Add(dgvcMRBNo);

                DataGridViewTextBoxColumn dgvcNSMNo = new DataGridViewTextBoxColumn();
                dgvcNSMNo.DataPropertyName = "NSMNO";
                dgvcNSMNo.HeaderText = "NSM No";
                dgvcNSMNo.Name = "NSMNo";
                dgvcNSMNo.Width = 120;
                dgvcNSMNo.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcNSMNo);

                DataGridViewTextBoxColumn dgvcNSMResult = new DataGridViewTextBoxColumn();
                dgvcNSMResult.DataPropertyName = "NSMResult";
                dgvcNSMResult.HeaderText = "NSM Result";
                dgvcNSMResult.Name = "NSMResult";
                dgvcNSMResult.Width = 120;
                dgvcNSMResult.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcNSMResult);

                DataGridViewTextBoxColumn dgvcNSMReason = new DataGridViewTextBoxColumn();
                dgvcNSMReason.DataPropertyName = "NSMReason";
                dgvcNSMReason.HeaderText = "NSM Reason";
                dgvcNSMReason.Name = "NSMReason";
                dgvcNSMReason.Width = 120;
                dgvcNSMReason.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcNSMReason);

                dtgData.DataSource = dtData;
                pagData.Text = string.Format("{0} records", dtData.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dtData.Clear();
                string strInspectResult = "";
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbInspResult.SelectedIndex != -1)
                {
                    strInspectResult = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();
                }
                string strTaskID = txtTaskID.Text.ToString().Trim();
                string strMatnr = txtMatnr.Text.ToString().Trim();
                strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
                if (cmbStatus.SelectedIndex != -1)
                {
                    strStatus = cmbStatus.Items[cmbStatus.SelectedIndex].ToString();
                }

                bool bolNotPrint = chkNotPrint.Checked;
                string strMtype = cmbMtype.Text;
                //DataTable dtLgort = objStorageData.QueryIQCLgort(Werks, Lgort);
                //if (dtLgort.Rows.Count > 0)
                //{
                dtData = objStorageData.QueryIQCTaskID(Werks, Lgort, strInspectResult, strTaskID, strMatnr, strStartDate, strEndDate, strStatus, bolNotPrint, strMtype);
                ShowDataGrid();
                //}
                //else
                //{
                //    stsWarning.Text = "not inspection Storage, please confirm！";
                //    return;
                //}
            }
            catch
            {
                stsWarning.Text = "No Data!";
                return;
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.dtgData.DataSource = null;
            this.dtData.Rows.Clear();
            dtData.Select();
            ShowDdlInspect();
            pagData.Text = string.Format("{0} records", "0");
            txtTaskID.Text = "";
            txtMatnr.Text = "";
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region dtgData relabel
        private void dtgData_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string strTaskid = "";
                string strExpdat = "";
                string strResult = "";
                stsWarning.Text = "";
                int intRowNo;
                DataGridView dgClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    strTaskid = dgClick.Rows[intRowNo].Cells["Taskid"].Value.ToString();
                    strExpdat = dgClick.Rows[intRowNo].Cells["EXPDAT_AFTER"].Value.ToString();
                    strResult = dgClick.Rows[intRowNo].Cells["Result"].Value.ToString();

                    //检验结果为OK，则跳转标签打印页面
                    if (strResult == "OK" || strResult == "WAIVE")
                    {
                        IQC_PrintExpDate obj_IQC_PrintExpDate = new IQC_PrintExpDate(UserData, Progid, strExpdat, strTaskid);
                        obj_IQC_PrintExpDate.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region IQC要求刷入条码截取料号
        private void txtMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string[] strBarCode = txtMatnr.Text.ToString().Trim().ToUpper().Split(';');
                if (strBarCode.Length == 1)
                {
                    string[] strDID = strBarCode[0].Trim().ToUpper().Split('-');
                    if (strDID.Length > 1)
                    {
                        txtMatnr.Text = strDID[0];
                    }
                }
                else
                {
                    txtMatnr.Text = strBarCode[0];
                }
            }
        }
        #endregion

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strInspectResult = cmbInspResult.Text.ToString();
            string strTaskID = txtTaskID.Text.ToString().Trim();
            string strMatnr = txtMatnr.Text.ToString().Trim();
            bool bolNotPrint = chkNotPrint.Checked;
            DataTable dataTable = new DataTable();
            string strMtype = cmbMtype.Text;

            if (strMtype == "")
            {
                MessageBox.Show("Mtype不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                dataTable = objStorageData.IQCDataTable(Werks, Lgort, strInspectResult, strTaskID, strMatnr, strStartDate, strEndDate, strStatus, bolNotPrint, strMtype);

                ExportDataTableToExcel(dataTable);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void ExportDataTableToExcel(DataTable data)
        {
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可供导出！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.DefaultExt = "xls";
                savedialog.Filter = "Microsoft Office Excel files (*.xls)|*.xls";
                savedialog.FilterIndex = 0;
                savedialog.RestoreDirectory = true;
                savedialog.Title = "导出数据到 Excel 表格";
                savedialog.FileName = "IQC再检" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                savedialog.ShowDialog();
                if (savedialog.FileName.IndexOf(":") < 0) return; // 被点了取消

                Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
                if (xlapp == null)
                {
                    MessageBox.Show("可能您的电脑未安装 Excel，无法创建 Excel 对象！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 临时设置环境为英文
                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlapp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1]; // 取得 sheet1

                int rowscount = data.Rows.Count;
                int colscount = data.Columns.Count;

                // 行数不可以大于65536
                if (rowscount > 65536)
                {
                    MessageBox.Show("数据行记录超过65536行，不能保存！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // 列数不可以大于255
                if (colscount > 256)
                {
                    MessageBox.Show("数据列记录超过256列，不能保存！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 写入标题
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = data.Columns[i].ColumnName;
                }

                // 写入数值
                for (int r = 0; r < data.Rows.Count; r++)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (data.Rows[r][i] != DBNull.Value && data.Rows[r][i].GetType() == typeof(string))
                        {
                            worksheet.Cells[r + 2, i + 1] = "" + data.Rows[r][i]; // 将长数值转换成文本
                        }
                        else
                        {
                            worksheet.Cells[r + 2, i + 1] = data.Rows[r][i];
                        }
                    }
                    System.Windows.Forms.Application.DoEvents();
                }

                worksheet.Columns.EntireColumn.AutoFit(); // 列宽自适应
                if (savedialog.FileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(savedialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错，文件可能正被打开！..." + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show("数据导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (xlapp != null)
                {
                    xlapp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
                    xlapp = null;
                }
            }
        }




        //public void ExportDataTableToExcel(DataGridView data)
        //{
        //    if (data.Rows.Count == 0)
        //    {
        //        MessageBox.Show(" 没有数据可供导出！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    else
        //    {
        //        SaveFileDialog savedialog = new SaveFileDialog();
        //        savedialog.DefaultExt = "xls";
        //        savedialog.Filter = "microsoft office execl files (*.xls)|*.xls";
        //        savedialog.FilterIndex = 0;
        //        savedialog.RestoreDirectory = true;
        //        savedialog.Title = "导出数据到excel表格";
        //        savedialog.FileName = "IQC再检" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //        savedialog.ShowDialog();
        //        if (savedialog.FileName.IndexOf(":") < 0) return; //被点了取消  
        //        Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
        //        if (xlapp == null)
        //        {
        //            MessageBox.Show("可能您的电脑未安装excel，无法创建excel对象！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        //临时设置环境为英文
        //        System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");



        //        Microsoft.Office.Interop.Excel.Workbooks workbooks = xlapp.Workbooks;
        //        Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //        Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  
        //        //定义表格内数据的行数和列数   
        //        int rowscount = data.Rows.Count;
        //        int colscount = data.Columns.Count;
        //        //行数不可以大于65536   
        //        if (rowscount > 65536)
        //        {
        //            MessageBox.Show("数据行记录超过65536行，不能保存！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        //列数不可以大于255   
        //        if (colscount > 256)
        //        {
        //            MessageBox.Show("数据列记录超过256列，不能保存！", "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        //写入标题
        //        for (int i = 0; i < data.ColumnCount; i++)
        //        {
        //            worksheet.Cells[1, i + 1] = data.Columns[i].HeaderText;
        //        }
        //        //写入数值
        //        for (int r = 0; r < data.Rows.Count; r++)
        //        {
        //            for (int i = 0; i < data.ColumnCount; i++)
        //            {
        //                if (data[i, r].ValueType == typeof(string))
        //                {
        //                    worksheet.Cells[r + 2, i + 1] = "" + data.Rows[r].Cells[i].Value;//将长数值转换成文本
        //                }
        //                else
        //                {
        //                    worksheet.Cells[r + 2, i + 1] = data.Rows[r].Cells[i].Value;
        //                }
        //            }
        //            System.Windows.Forms.Application.DoEvents();
        //        }
        //        worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
        //        if (savedialog.FileName != "")
        //        {
        //            try
        //            {
        //                workbook.Saved = true;
        //                workbook.SaveCopyAs(savedialog.FileName);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("导出文件时出错,文件可能正被打开！..." + ex.Message, "系统提示 ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        MessageBox.Show("数据导出成功！ ", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //关闭excel进程
        //        if (xlapp != null)
        //        {
        //            xlapp.Quit();
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
        //            xlapp = null;
        //        }
        //    }
        //}
    }
}