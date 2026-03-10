using QCI.QWMS;
using QWMS.Common;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Manage_Real_time_pending_query : Form
    {
        #region 全局变量
        private UserInfo UserData;
        private string Progid;
        private string Usrnm;
        private string Mandt;
        private string Comcd;
        private string strWerks;
        private string strLgort;
        private StorageIn objStorageIn;
        private DataTable dtData = new DataTable();
        #endregion

        #region 构造函数
        public Manage_Real_time_pending_query(UserInfo _UserData, string strProgid)
        {
            InitializeComponent();

            UserData = _UserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            objStorageIn = new StorageIn(UserData, Progid);
            //檢查權限
            if (!objStorageIn.CheckAuthority("MANAGE"))
            {
                throw new Exception("You don't have right to use this program!!");
            }
            else
            {
                //秀出status的资料
                ShowStatusData();
                ShowDdlWerks();
                if (cmbWerks.Items.Count > 0)
                {
                    this.cmbWerks.SelectedIndex = 0;
                    strWerks = cmbWerks.Text.Trim();
                }
            }
        }
        #endregion

        #region 厂区和仓别

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
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
        #endregion

        #region cmbWerks_SelectedIndexChanged
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
                Authority objAuthority = new Authority(UserData);
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

        #region 退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dtData.Rows.Count > 0)
            {
                string ReportPrintType = "Real_time_peding_query";
                ReportPrint objReportPrint = new ReportPrint(UserData, ReportPrintType, dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            else
            {
                stsWarning.Text = "无数据。";
            }
        }
        #endregion

        #region 刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtData.Rows.Clear();
            cmbLgort.SelectedIndex = -1;
            stsWarning.Text = "";
            lblCount.Text = "0 records";
        }
        #endregion

        #region Download
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtData.Rows.Count > 0 && sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    CountingResult2File(sfdSaveFile.FileName);
                }
                else
                {
                    stsWarning.Text = "无数据。";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        # region Export
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            FileInfo fi = null;
            StreamWriter sw = null;
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "PLANT\tGR_SLOC\tGR_DATE\tGR_MAT_DOC\tITEM\tGR_BIN_LOCATION\tMATERIAL\tGR_MVT\tIQC_QTY\tGR_USER";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["BUDAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["SGTXT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["BWART"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LMENGEZUB"].ToString() + "\t";
                    strLine += dtData.Rows[i]["USNAM"].ToString() ;
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }
        }
        # endregion

        #endregion

        #region 查询
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Action<string, DataSet> a = SendToSAP;
                ////a.BeginInvoke(3, 4, null, null);
                //Console.WriteLine("执行线程");
                //Console.ReadKey();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("请选择厂区");
                    return;
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.SelectedItem.ToString();
                }
                else
                {
                    strLgort = string.Empty;
                }
                DataSet ds = new DataSet();
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("WERKS");
                dtTemp.Columns.Add("LGORT");
                dtTemp.Rows.Add(strWerks, strLgort);

                ds.Tables.Add(dtTemp);
                SendToSAP(ds);
                //去除小数点以及小数点后的0
                foreach(DataRow dr in dtData.Rows)
                {
                    dr["LMENGEZUB"] = dr["LMENGEZUB"].ToString().Substring(0, dr["LMENGEZUB"].ToString().LastIndexOf('.'));
                }
                //排序
                DataView dvData = dtData.DefaultView;
                dvData.Sort = "LGORT,MBLNR,ZEILE";
                dtData = dvData.ToTable();

                ShowDataView(dtData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #region 通过RFC跟SAP交互
        public void SendToSAP(DataSet ds)
        {
            try
            {
                DataSet dsResult = new DataSet();
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                obj.Timeout = 300000;
                dsResult = obj.Z_MM_RFC_COMPARE_QWMSSAP(ds, "2");
                dtData = dsResult.Tables["ITAB"];
                QCI.QWMS.LogData objLogData = new LogData(UserData, strWerks, strLgort, Progid);
                objLogData.CompareLog(strWerks, strLgort, "RealTime");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #endregion

        #region ShowDataView
        private void ShowDataView(DataTable dtData)
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "PLANT";
                dgvcWERKS.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcWERKS.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "GR SLOC";
                dgvcLGORT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcLGORT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcBUDAT = new DataGridViewTextBoxColumn();
                dgvcBUDAT.DataPropertyName = "BUDAT";
                dgvcBUDAT.HeaderText = "GR DATE";
                dgvcBUDAT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcBUDAT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcBUDAT);

                DataGridViewTextBoxColumn dgvcUSNAM = new DataGridViewTextBoxColumn();
                dgvcUSNAM.DataPropertyName = "USNAM";
                dgvcUSNAM.HeaderText = "GR USER";
                dgvcUSNAM.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcUSNAM.ReadOnly = true;
                this.gvData.Columns.Add(dgvcUSNAM);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "GR MAT DOC";
                dgvcMBLNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvcMBLNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "ITEM";
                dgvcZEILE.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcZEILE.ReadOnly = true;
                this.gvData.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcSGTXT = new DataGridViewTextBoxColumn();
                dgvcSGTXT.DataPropertyName = "SGTXT";
                dgvcSGTXT.HeaderText = "GR BIN LOCATION";
                dgvcSGTXT.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcSGTXT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcSGTXT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATERIAL";
                dgvcMATNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcMATNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "GR MVT";
                dgvcBWART.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcBWART.ReadOnly = true;
                this.gvData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcLMENGEZUB = new DataGridViewTextBoxColumn();
                dgvcLMENGEZUB.DataPropertyName = "LMENGEZUB";
                dgvcLMENGEZUB.HeaderText = "IQC QTY";
                dgvcLMENGEZUB.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvcLMENGEZUB.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLMENGEZUB);




                this.gvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView()");
            }
        }
        #endregion


    }
}
