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

namespace QWMS
{
    public partial class InventoryCheck_StorageChangeQuery : Form
    {
        # region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strRegon = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;

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
        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
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
        # endregion

        public InventoryCheck_StorageChangeQuery(UserInfo varUserData, string strProgid)
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
                    ShowDdlRegon();
                    ShowDdlLgort();
                    ShowDdlInsmk();
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

        # region ShowDdlRegon
        private void ShowDdlRegon()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbRegon.Items.Clear();
                dtTemp = objAuthority.CheckRegonAuthority();


                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbRegon.DataSource = dtTemp;
                    cmbRegon.DisplayMember = "F_TEXT";
                    cmbRegon.ValueMember = "F_VALUE";

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlRegon()");
            }
        }
        # endregion

        # region Plant SelectedChange
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

        # region ShowDdlInsmk
        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
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
                dgvcCycno.DataPropertyName = "WERKS";
                dgvcCycno.HeaderText = "Plant";
                dgvcCycno.ReadOnly = true;
                dgvcCycno.Width = 100;
                dgvData.Columns.Add(dgvcCycno);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

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

                DataGridViewTextBoxColumn dgvcMaktx = new DataGridViewTextBoxColumn();
                dgvcMaktx.DataPropertyName = "MAKTX";
                dgvcMaktx.HeaderText = "Part# Description";
                dgvcMaktx.ReadOnly = true;
                dgvcMaktx.Width = 300;
                dgvData.Columns.Add(dgvcMaktx);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "CUST Mat";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Width = 90;
                dgvData.Columns.Add(dgvcKdmat);

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

                if (!chkIsCombine.Checked)
                {
                    DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                    dgvcSerno.DataPropertyName = "SERNO";
                    dgvcSerno.HeaderText = "Serial No.";
                    dgvcSerno.ReadOnly = true;
                    dgvcSerno.Width = 90;
                    dgvData.Columns.Add(dgvcSerno);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 90;
                dgvData.Columns.Add(dgvcMblnr);

                if (!chkIsCombine.Checked)
                {
                    DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                    dgvcLifnr.DataPropertyName = "LIFNR";
                    dgvcLifnr.HeaderText = "Vendor";
                    dgvcLifnr.ReadOnly = true;
                    dgvData.Columns.Add(dgvcLifnr);

                    DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                    dgvcMrgid.DataPropertyName = "MRGID";
                    dgvcMrgid.HeaderText = "Mixed Material ID";
                    dgvcMrgid.ReadOnly = true;
                    dgvcMrgid.Width = 120;
                    dgvData.Columns.Add(dgvcMrgid);
                }

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvData.Columns.Add(dgvcIndat);

                if (!chkIsCombine.Checked)
                {
                    DataGridViewTextBoxColumn dgvcRmak = new DataGridViewTextBoxColumn();
                    dgvcRmak.DataPropertyName = "RMAK1";
                    dgvcRmak.HeaderText = "Remark";
                    dgvcRmak.ReadOnly = true;
                    dgvcRmak.Width = 110;
                    dgvData.Columns.Add(dgvcRmak);
                }

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
            string strStartDate = "";
            string strEndDate = "";
            string strStartDate_Change = "";
            string strEndDate_Change = "";
            string strInsmk = "";
            string strIsmrg = "N";
            string strIsCombine = "N";
            stsWarning.Text = "";

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
                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                if (cmbRegon.SelectedIndex != -1)
                {
                    strRegon = cmbRegon.SelectedValue.ToString();
                }
                if (chkIsmrg.Checked)
                {
                    strIsmrg = "Y";
                }
                if (chkIsCombine.Checked)
                {
                    strIsCombine = "Y";
                }

                if (chkDate.Checked)
                {
                    strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                    strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
                }
                else
                {
                    strStartDate = "";
                    strEndDate = "";
                }
                #region 異動日期 whlog

                if (chkDate_Change.Checked)
                {
                    strStartDate_Change = dtpStartDate_Change.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    strEndDate_Change = dtpEndDate_Change.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    strStartDate_Change = "";
                    strEndDate_Change = "";
                }
                #endregion
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtData = objCounting.QueryDetailChangedData(strInsmk, txtStartLocat.Text.Trim(), txtEndLocat.Text.Trim(), txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), strStartDate, strEndDate, txtCharg.Text.Trim(), txtMblnr.Text.Trim(), strIsmrg, strIsCombine, strRegon, strStartDate_Change, strEndDate_Change);
                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    btnPrint.Enabled = true;
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.cmbInsmk.SelectedIndex = -1;
                this.txtStartLocat.Text = "";
                this.txtEndLocat.Text = "";
                this.txtStartMatnr.Text = "";
                this.txtEndMatnr.Text = "";
                this.txtCharg.Text = "";
                this.txtMblnr.Text = "";
                this.lblCount.Text = "";
                this.dtpStartDate.Value = DateTime.Now;
                this.dtpEndDate.Value = DateTime.Now;

                this.dtpEndDate_Change.Value = DateTime.Now;
                this.dtpStartDate_Change.Value = DateTime.Now;

                this.btnPrint.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.chkIsCombine.Checked = false;
                this.chkIsmrg.Checked = false;
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
                ReportPrint objReportPrint = new ReportPrint(UserData, "DETAILCOUNT", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }

        # region Resize
        private void InventoryCheck_StorageChangeQuery_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
        }
        # endregion

        # region Download
        private void btnDownload_Click(object sender, EventArgs e)
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
                MessageBox.Show(ex.Message);
                return;
            }
        }
        # endregion

        # region Export File
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (this.chkIsCombine.Checked)
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tPart No Description\tCUST Mat\tStock\tVersion\tQty\tDocument No\tStore In Date";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MAKTX"].ToString() + "\t";
                        strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";

                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tPart No Description\tCUST Mat\tStock\tVersion\tSerial No\tQty\tDocument No\tVendor\tMixed Material ID\tStore In Date\tRemark";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MAKTX"].ToString() + "\t";
                        strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtData.Rows[i]["SERNO"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MRGID"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";

                        strLine += dtData.Rows[i]["RMAK1"].ToString();
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
        # endregion


    }
}
