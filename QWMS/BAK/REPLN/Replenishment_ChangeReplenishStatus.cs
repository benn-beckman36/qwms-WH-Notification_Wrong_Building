using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.IO;
using System.Collections;

namespace QWMS
{
    public partial class Replenishment_ChangeReplenishStatus : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtDataDetail = new DataTable();
        private PlantData objPlantData;
        private Authority objAuthority;
        private Replenishment objReplenishment;

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
        #endregion

        #region 构造函数
        public Replenishment_ChangeReplenishStatus()
        {
            InitializeComponent();
        }
        public Replenishment_ChangeReplenishStatus(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objReplenishment = new Replenishment(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                // 检查权限
                if (!objReplenishment.CheckAuthority())
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
                    if (cmbWerks.Items.Count > 0 && cmbLgort.Items.Count > 0)
                    {
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    }
                    ShowDdlStatus();
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
        #endregion

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
        #endregion

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

        #endregion

        #region 绑定 Status
        private void ShowDdlStatus()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbStatus.Items.Clear();
                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                dtTemp = objReplenishment.GetReplenishmentStatus();
                cmbStatus.Items.Add("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbStatus.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStatus()");
            }
        }
        #endregion

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }       
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strStatus = "";
            this.stsWarning.Text = "";
            string[] arrStatus;
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbStatus.SelectedIndex != -1)
                {
                    strStatus = cmbStatus.Items[cmbStatus.SelectedIndex].ToString();
                    arrStatus = strStatus.Split(new char[] { ':' });
                    strStatus = arrStatus[0].ToString();
                }

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                dtData = objReplenishment.QueryReplenishmentHeader(strStatus, "", this.txtMatnr.Text.Trim(), this.txtResno.Text.Trim());

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    gbPrint.Enabled = true;
                    panel4.Enabled = false;
                    ShowDataGridView();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.dgvData.AutoGenerateColumns = false;
                this.dgvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcResno = new DataGridViewTextBoxColumn();
                dgvcResno.DataPropertyName = "RESNO";
                dgvcResno.Name = "Resno";  //设置列明
                dgvcResno.HeaderText = "Replenish No";
                dgvcResno.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcResno);

                DataGridViewTextBoxColumn dgvcToloc = new DataGridViewTextBoxColumn();
                dgvcToloc.DataPropertyName = "TOLOC";
                dgvcToloc.HeaderText = "Location";
                dgvcToloc.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcToloc);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "STATUS";
                dgvcStatus.HeaderText = "Status";
                dgvcStatus.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcStatus);

                this.dgvData.DataSource = dtData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region ShowDetailDataGridView
        private void ShowDetailDataGridView()
        {
            this.dgvDataDetail.AutoGenerateColumns = false;
            this.dgvDataDetail.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgcbSelect = new DataGridViewCheckBoxColumn();
                dgcbSelect.DataPropertyName = "Select";
                dgcbSelect.HeaderText = "Select";
                this.dgvDataDetail.Columns.Add(dgcbSelect);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "From Location";
                dgvcLocat.Name = "Locat";
                dgvcLocat.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcToloc = new DataGridViewTextBoxColumn();
                dgvcToloc.DataPropertyName = "TOLOC";
                dgvcToloc.HeaderText = "Location";
                dgvcToloc.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcToloc);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Name = "Matnr";
                dgvcMatnr.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Name = "Insmk";
                dgvcInsmk.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Name = "Charg";
                dgvcCharg.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Name = "Mblnr";
                dgvcMblnr.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "STATUS";
                dgvcStatus.HeaderText = "Status";
                dgvcStatus.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcStatus);

                DataGridViewTextBoxColumn dgvcCrnam = new DataGridViewTextBoxColumn();
                dgvcCrnam.DataPropertyName = "CRNAM";
                dgvcCrnam.HeaderText = "Create User";
                dgvcCrnam.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcCrnam);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Create Date";
                dgvcCrdat.ReadOnly = true;
                this.dgvDataDetail.Columns.Add(dgvcCrdat);

                this.dgvDataDetail.DataSource = dtDataDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDetailDataGridView()");
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbStatus.SelectedIndex = -1;
            this.txtMatnr.Text = "";
            this.txtResno.Text = "";
            this.panel4.Enabled = true;
            this.rdoDetail.Checked = false;
            this.rdoSummary.Checked = false;
            this.btnPrint.Enabled = false;
            this.btnDownload.Enabled = false;
            this.gbPrint.Enabled = false;
            this.dgvDataDetail.DataSource = null;
            this.dtDataDetail.Rows.Clear();
            dgvDataDetail.Select();
            this.dgvData.DataSource = null;
            this.dtData.Rows.Clear();
            dtData.Select();
            this.btnPost.Enabled = false;
            this.btnCancel.Enabled = false;
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region txtMatnr Double Click Event
        private void txtMatnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Manage_MaterialSelect objManage_MaterialSelect = new Manage_MaterialSelect(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "INVENTORY", txtMatnr.Text.Trim());
                objManage_MaterialSelect.ShowDialog();
                txtMatnr.Text = objManage_MaterialSelect.Matnr;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region rdoSummary Checked Changed Event
        private void rdoSummary_CheckedChanged(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
        }
        #endregion

        #region rdoDetail Checked Changed Event
        private void rdoDetail_CheckedChanged(object sender, EventArgs e)
        {
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
        }

        #endregion

        #region  dgvData Mouse Down Event
        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string strResno = "";
                int intRowNo;
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 2);
                    //strResno = dgClick[dgClick.CurrentCell].ToString();
                    strResno = dgvClick.Rows[intRowNo].Cells["Resno"].Value.ToString();
                    objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                    dtDataDetail = objReplenishment.QueryReplenishmentItem(strResno);

                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                    dtDataDetail.Columns.Add(cSelect);
                    for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                    {
                        dtDataDetail.Rows[i]["Select"] = false;
                        //						if(dtDataDetail.Rows[i]["STATUS"].ToString() != "0")
                        //						{
                        //							((CheckBox)dtDataDetail.Rows[i]["Select"]).Enabled = false;
                        //						}
                    }

                    this.ShowDetailDataGridView();
                    this.btnPost.Enabled = true;
                    this.btnCancel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.rdoSummary.Checked)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "REPLENISH_SUMMARY", dtData);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                if (this.rdoDetail.Checked)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "REPLENISH_DETAIL", dtDataDetail);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Download
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (this.rdoSummary.Checked)
                {
                    sfdSaveFile.FileName = "ReplenishmentSummary.xls";
                    if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        strExportName = sfdSaveFile.FileName;
                        CountingResult2File(strExportName);
                    }
                }
                if (this.rdoDetail.Checked)
                {
                    sfdSaveFile.FileName = "ReplenishmentDetail.xls";
                    if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        strExportName = sfdSaveFile.FileName;
                        CountingResult2File(strExportName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region 导出文件
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (this.rdoSummary.Checked)
                {
                    strLine = "Plant\tStorage\tReplenish No\tLocation\tPart No\tStock\tQty\tStatus";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["RESNO"].ToString() + "\t";
                        strLine += dtData.Rows[i]["TOLOC"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["STATUS"].ToString();
                        sw.WriteLine(strLine);
                    }
                }

                if (this.rdoDetail.Checked)
                {
                    strLine = "Plant\tStorage\tFrom Location\tTo Location\tPart No\tStock\tVersion\tQty\tOut Qty\tDocument No\tStatus\tCreate User\tCreate Date";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtDataDetail.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["TOLOC"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["ALQTY"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["STATUS"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CRNAM"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CRDAT"].ToString();
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
            }

        }

        #endregion

        #region Post
        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList arrResno = new ArrayList();
                ArrayList arrMandt = new ArrayList();
                ArrayList arrComcd = new ArrayList();
                ArrayList arrWerks = new ArrayList();
                ArrayList arrLgort = new ArrayList();
                ArrayList arrLocat = new ArrayList();
                ArrayList arrToloc = new ArrayList();
                ArrayList arrMblnr = new ArrayList();
                ArrayList arrMatnr = new ArrayList();
                ArrayList arrInsmk = new ArrayList();
                ArrayList arrCharg = new ArrayList();
                ArrayList arrMenge = new ArrayList();
                ArrayList arrAlqty = new ArrayList();
                for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtDataDetail.Rows[i]["Select"]) && dtDataDetail.Rows[i]["STATUS"].ToString() != "0")
                    {
                        MessageBox.Show("Some item had been cancelled or posted!");
                        return;
                    }
                    if (Convert.ToBoolean(dtDataDetail.Rows[i]["Select"]) && dtDataDetail.Rows[i]["STATUS"].ToString() == "0")
                    {
                        arrResno.Add(dtDataDetail.Rows[i]["RESNO"]);
                        arrMandt.Add(dtDataDetail.Rows[i]["MANDT"]);
                        arrComcd.Add(dtDataDetail.Rows[i]["Comcd"]);
                        arrWerks.Add(dtDataDetail.Rows[i]["WERKS"]);
                        arrLgort.Add(dtDataDetail.Rows[i]["LGORT"]);
                        arrLocat.Add(dtDataDetail.Rows[i]["LOCAT"]);
                        arrToloc.Add(dtDataDetail.Rows[i]["TOLOC"]);
                        arrMblnr.Add(dtDataDetail.Rows[i]["MBLNR"]);
                        arrMatnr.Add(dtDataDetail.Rows[i]["MATNR"]);
                        arrInsmk.Add(dtDataDetail.Rows[i]["INSMK"]);
                        arrCharg.Add(dtDataDetail.Rows[i]["CHARG"]);
                        arrMenge.Add(dtDataDetail.Rows[i]["MENGE"]);
                        arrAlqty.Add(dtDataDetail.Rows[i]["ALQTY"]);
                    }
                }
                if (arrResno.Count == 0)
                {
                    MessageBox.Show("Please select one mail!!");
                    return;
                }

                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                if (objReplenishment.UpdateReplenishmentStatus(arrResno, arrMandt,arrComcd, arrWerks, arrLgort, arrLocat, arrToloc, arrMblnr, arrMatnr, arrInsmk, arrCharg, arrMenge, arrAlqty, "1"))
                {
                    stsWarning.Text = "Post OK!";
                    this.btnPost.Enabled = false;
                    this.btnCancel.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Post Fail!" + objReplenishment.ERRMSG;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList arrResno = new ArrayList();
                ArrayList arrMandt = new ArrayList();
                ArrayList arrComcd = new ArrayList();
                ArrayList arrWerks = new ArrayList();
                ArrayList arrLgort = new ArrayList();
                ArrayList arrLocat = new ArrayList();
                ArrayList arrToloc = new ArrayList();
                ArrayList arrMblnr = new ArrayList();
                ArrayList arrMatnr = new ArrayList();
                ArrayList arrInsmk = new ArrayList();
                ArrayList arrCharg = new ArrayList();
                ArrayList arrMenge = new ArrayList();
                ArrayList arrAlqty = new ArrayList();
                for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtDataDetail.Rows[i]["Select"]))
                    {
                        arrResno.Add(dtDataDetail.Rows[i]["RESNO"]);
                        arrMandt.Add(dtDataDetail.Rows[i]["MANDT"]);
                        arrComcd.Add(dtDataDetail.Rows[i]["Comcd"]);
                        arrWerks.Add(dtDataDetail.Rows[i]["WERKS"]);
                        arrLgort.Add(dtDataDetail.Rows[i]["LGORT"]);
                        arrLocat.Add(dtDataDetail.Rows[i]["LOCAT"]);
                        arrToloc.Add(dtDataDetail.Rows[i]["TOLOC"]);
                        arrMblnr.Add(dtDataDetail.Rows[i]["MBLNR"]);
                        arrMatnr.Add(dtDataDetail.Rows[i]["MATNR"]);
                        arrInsmk.Add(dtDataDetail.Rows[i]["INSMK"]);
                        arrCharg.Add(dtDataDetail.Rows[i]["CHARG"]);
                        arrMenge.Add(dtDataDetail.Rows[i]["MENGE"]);
                        arrAlqty.Add(dtDataDetail.Rows[i]["MENGE"]);
                    }
                }
                if (arrResno.Count == 0)
                {
                    MessageBox.Show("Please select one mail!!");
                    return;
                }

                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                if (objReplenishment.UpdateReplenishmentStatus(arrResno, arrMandt,arrComcd, arrWerks, arrLgort, arrLocat, arrToloc, arrMblnr, arrMatnr, arrInsmk, arrCharg, arrMenge, arrAlqty, "2"))
                {
                    stsWarning.Text = "Cancel OK!";
                    this.btnPost.Enabled = false;
                    this.btnCancel.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Cancel Fail!" + objReplenishment.ERRMSG;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region dgvDataDetail Mouse Down Event
        private void dgvDataDetail_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                string strTempLocat = "";
                string strTempMatnr = "";
                string strTempInsmk = "";
                string strTempCharg = "";
                string strTempMenge = "";
                string strTempMblnr = "";
                stsWarning.Text = "";
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 1);
                    //strTempLocat = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 3);
                    //strTempMatnr = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 4);
                    //strTempInsmk = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 5);
                    //strTempCharg = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 8);
                    //strTempMblnr = dgClick[dgClick.CurrentCell].ToString();
                    strTempLocat = dgvDataDetail.Rows[intRowNo].Cells["Locat"].Value.ToString();
                    strTempMatnr = dgvDataDetail.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                    strTempInsmk = dgvDataDetail.Rows[intRowNo].Cells["Insmk"].Value.ToString();
                    strTempCharg = dgvDataDetail.Rows[intRowNo].Cells["Charg"].Value.ToString();
                    strTempMblnr = dgvDataDetail.Rows[intRowNo].Cells["Mblnr"].Value.ToString();


                    Replenishment_ChangeReplenishStatus_Edit objReplenishment_ChangeReplenishStatus_Edit = new Replenishment_ChangeReplenishStatus_Edit(UserData, Progid, Werks, Lgort, strTempLocat, strTempMblnr, strTempMatnr, strTempInsmk, strTempCharg, strTempMenge, dtDataDetail);
                    objReplenishment_ChangeReplenishStatus_Edit.ShowDialog();
                    dtDataDetail = objReplenishment_ChangeReplenishStatus_Edit.QtyData;
                    ShowDetailDataGridView();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 调整布局大小
        private void Replenishment_ChangeReplenishStatus_Resize(object sender, EventArgs e)
        {
            panel2.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel1.Size.Height);
            panel5.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5 * 0.4), panel5.Size.Height);
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        # endregion
    }
}
