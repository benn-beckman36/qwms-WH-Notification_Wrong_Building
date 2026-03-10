using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class IQC_OverDue_Inspect : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private DataTable dtData = new DataTable();
        private DataTable dtDataDetail = new DataTable();
        private DataTable dtDataInspect = new DataTable();
        //private SQLAccess objDB;
        private StorageData objStorageData;
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
        public IQC_OverDue_Inspect(UserInfo _UserData, string strProgid)
        {
            UserData = _UserData;

            InitializeComponent();
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objPlantData = new PlantData(UserData);
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
                    ShowDdlInspect1();
                    dtDataInspect.Columns.Add("TASKID");
                    dtDataInspect.Columns.Add("RESULT");
                    dtDataInspect.Columns.Add("REASON");
                    dtDataInspect.Columns.Add("CHKQTY");
                    dtDataInspect.Columns.Add("CHKNAM");
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    cmbWerks.Enabled = true;
                    cmbLgort.Enabled = true;
                    cmbInspResult.Enabled = true;
                    txtMatnr.Enabled = true;
                    btnQuery.Enabled = true;
                    txtNsmNo.Enabled = true;
                    txtMrbNo.Enabled = true;
                    cmbInspResult1.Enabled = false;
                    txtInspReason.Enabled = false;
                    btnTaskID.Enabled = false;
                    btnSave.Enabled = false;
                }
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
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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

        #region 绑定检验结果
        private void ShowDdlInspect()
        {
            try
            {
                cmbInspResult.Items.Clear();
                cmbInspResult.Items.Add("");
                cmbInspResult.Items.Add("NC REVIEW");//工程师确认
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInspect()");
            }
        }

        private void ShowDdlInspect1()
        {
            try
            {
                cmbInspResult1.Items.Clear();
                cmbInspResult1.Items.Add("");
                cmbInspResult1.Items.Add("OK");
                cmbInspResult1.Items.Add("NC REVIEW");//工程师确认
                cmbInspResult1.Items.Add("NG");
                cmbInspResult1.Items.Add("REJECT");
                cmbInspResult1.Items.Add("WAIVE");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInspect1()");
            }
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                this.dtgData.AutoGenerateColumns = false;
                this.dtgData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "Choose";
                dgvcSelect.Name = "Select";
                dgvcSelect.Width = 50;
                dgvcSelect.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "Type";
                dgvcType.HeaderText = "Sampling Level";
                dgvcType.Name = "Type";
                dgvcType.Width = 180;
                dgvcType.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "Re-Inspection lot";
                dgvcTASKID.Name = "Taskid";
                dgvcTASKID.Width = 180;
                dgvcTASKID.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcRESULT = new DataGridViewTextBoxColumn();
                dgvcRESULT.DataPropertyName = "RESULT";
                dgvcRESULT.HeaderText = "Inspection Result";
                dgvcRESULT.Name = "Result";
                dgvcRESULT.Width = 150;
                dgvcRESULT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcRESULT);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Name = "Werks";
                dgvcWerks.Width = 80;
                dgvcWerks.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Name = "Lgort";
                dgvcLgort.Width = 80;
                dgvcLgort.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 80;
                dgvcInsmk.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Name = "Matnr";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "VENDOR";
                dgvcLifnr.Name = "Lifnr";
                dgvcLifnr.Width = 100;
                dgvcLifnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Input Date";
                dgvcIndat.Width = 100;
                dgvcIndat.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Quanlity";
                dgvcMenge.Name = "Menge";
                dgvcMenge.Width = 100;
                dgvcMenge.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "Vendor DateCode";
                dgvcDACOD.Name = "Dacod";
                dgvcDACOD.Width = 150;
                dgvcDACOD.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcVEDAT = new DataGridViewTextBoxColumn();
                dgvcVEDAT.DataPropertyName = "VEDAT";
                dgvcVEDAT.HeaderText = "DateCode";
                dgvcVEDAT.Width = 150;
                dgvcVEDAT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcVEDAT);

                DataGridViewTextBoxColumn dgvcEXPDAT = new DataGridViewTextBoxColumn();
                dgvcEXPDAT.DataPropertyName = "EXPDAT";
                dgvcEXPDAT.HeaderText = "Exp. Date";
                dgvcEXPDAT.Name = "Expdat";
                dgvcEXPDAT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcEXPDAT);

                DataGridViewTextBoxColumn dgvcMAXEXP = new DataGridViewTextBoxColumn();
                dgvcMAXEXP.DataPropertyName = "MAXEXP";
                dgvcMAXEXP.HeaderText = "Max Exp. Date";
                dgvcMAXEXP.Name = "Maxexp";
                dgvcMAXEXP.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMAXEXP);


                DataGridViewTextBoxColumn dgvcNsmno = new DataGridViewTextBoxColumn();
                dgvcNsmno.DataPropertyName = "APPNO";
                dgvcNsmno.HeaderText = "NSM NO.";
                dgvcNsmno.Name = "Appno";
                dgvcNsmno.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcNsmno);

                DataGridViewTextBoxColumn dgvcMrbno = new DataGridViewTextBoxColumn();
                dgvcMrbno.DataPropertyName = "MRBNO";
                dgvcMrbno.HeaderText = "MRB NO.";
                dgvcMrbno.Name = "Mrbno";
                dgvcMrbno.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMrbno);

                DataGridViewTextBoxColumn dgvcTOTAL = new DataGridViewTextBoxColumn();
                dgvcTOTAL.DataPropertyName = "TOTAL";
                dgvcTOTAL.HeaderText = "Location Detail";
                dgvcTOTAL.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTOTAL);

                dtgData.DataSource = dtData;
                //string strTimeExpdat = "";
                //string strTimeMaxexp = "";
                //DateTime dtTimeNow = DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        //strTimeExpdat = dtData.Rows[i]["EXPDAT"].ToString();
                        //strTimeMaxexp = dtData.Rows[i]["MAXEXP"].ToString();
                        //DateTime dtTimeExpdat = DateTime.ParseExact(strTimeExpdat, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        //DateTime dtTimeMaxexp = DateTime.ParseExact(strTimeMaxexp, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        ////黄区材料，大于保存期，小于最大保存期
                        //if (DateTime.Compare(dtTimeNow, dtTimeExpdat) >= 0 && DateTime.Compare(dtTimeNow, dtTimeMaxexp) < 0)
                        //{
                        //    this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                        //    dtData.Rows[i]["Type"] = "Y（Normal）";
                        //}
                        ////红区，大于最大保存期
                        //if (DateTime.Compare(dtTimeNow, dtTimeMaxexp) >= 0)
                        //{
                        //    this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Salmon;
                        //    dtData.Rows[i]["Type"] = "R（Strict）";
                        //}

                        switch (dtData.Rows[i]["EXPTP"].ToString())
                        {
                            case "0":
                                this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LawnGreen;
                                dtData.Rows[i]["Type"] = "G（None）";
                                break;
                            case "1":
                                this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                                dtData.Rows[i]["Type"] = "Y（Normal）";
                                break;
                            case "2":
                                this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Salmon;
                                dtData.Rows[i]["Type"] = "R（Strict）";
                                break;
                        }
                    }
                }
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region ShowDetailDataGrid
        private void ShowDetailDataGrid()
        {
            try
            {
                this.dtgDataDetail.AutoGenerateColumns = false;
                this.dtgDataDetail.Columns.Clear();

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 100;
                dgvcLocat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "TASKID";
                dgvcMblnr.HeaderText = "Re-Inspection lot";
                dgvcMblnr.Width = 180;
                dgvcMblnr.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Quanlity";
                dgvcMenge.Width = 150;
                dgvcMenge.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "Vendor DateCode";
                dgvcDacod.Width = 150;
                dgvcDacod.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "DateCode";
                dgvcVedat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcVedat);

                dtgDataDetail.DataSource = dtDataDetail;
                lblDataDetail.Text = dtDataDetail.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDetailDataGrid()");
            }
        }
        #endregion

        #region ShowInspectDataGrid
        private void ShowInspectDataGrid()
        {
            try
            {
                this.dtgDataInspect.AutoGenerateColumns = false;
                this.dtgDataInspect.Columns.Clear();

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "Re-Inspection lot";
                dgvcTASKID.Name = "TASKID";
                dgvcTASKID.Width = 180;
                dgvcTASKID.ReadOnly = true;
                this.dtgDataInspect.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcRESULT = new DataGridViewTextBoxColumn();
                dgvcRESULT.DataPropertyName = "RESULT";
                dgvcRESULT.HeaderText = "Inspection Result";
                dgvcRESULT.Name = "RESULT";
                dgvcRESULT.Width = 150;
                dgvcRESULT.ReadOnly = true;
                this.dtgDataInspect.Columns.Add(dgvcRESULT);

                DataGridViewTextBoxColumn dgvcREASON = new DataGridViewTextBoxColumn();
                dgvcREASON.DataPropertyName = "REASON";
                dgvcREASON.HeaderText = "NC Symptom";
                dgvcREASON.Name = "REASON";
                dgvcREASON.Width = 150;
                this.dtgDataInspect.Columns.Add(dgvcREASON);

                DataGridViewTextBoxColumn dgvcCHKQTY = new DataGridViewTextBoxColumn();
                dgvcCHKQTY.DataPropertyName = "CHKQTY";
                dgvcCHKQTY.HeaderText = "Quanlity";
                dgvcCHKQTY.Name = "CHKQTY";
                dgvcCHKQTY.Width = 150;
                this.dtgDataInspect.Columns.Add(dgvcCHKQTY);

                DataGridViewTextBoxColumn dgvcCHKNAM = new DataGridViewTextBoxColumn();
                dgvcCHKNAM.DataPropertyName = "CHKNAM";
                dgvcCHKNAM.HeaderText = "Inspector ID";
                dgvcCHKNAM.Name = "CHKNAM";
                dgvcCHKNAM.Width = 150;
                this.dtgDataInspect.Columns.Add(dgvcCHKNAM);

                dtgDataInspect.DataSource = dtDataInspect;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowInspectDataGrid()");
            }
        }
        #endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            string strInspectResult = "";
            string strMatnr = "";
            this.stsWarning.Text = "";
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
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (cmbInspResult.SelectedIndex != -1)
                {
                    strInspectResult = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();
                }
                strMatnr = txtMatnr.Text.ToString().Trim();
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                DataTable dtLgort = objStorageData.QueryIQCLgort(strWerks, strLgort);
                if (dtLgort.Rows.Count > 0)
                {
                    dtData = objStorageData.QueryIQCOverdueStorageData(strWerks, strLgort, strInspectResult, "", strMatnr);
                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "No data!";
                        ShowDataGrid();
                        return;
                    }
                    else
                    {
                        DataColumn cSelect = new DataColumn("Select", typeof(bool));
                        DataColumn cType = new DataColumn("Type", typeof(string));
                        dtData.Columns.Add(cSelect);
                        dtData.Columns.Add(cType);
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["Select"] = false;
                        }
                        ShowDataGrid();
                        cmbWerks.Enabled = false;
                        cmbLgort.Enabled = false;
                        cmbInspResult.Enabled = false;
                        txtMatnr.Enabled = false;
                        btnQuery.Enabled = false;
                    }
                }
                else
                {
                    stsWarning.Text = "Not inspection Storage, please confirm！";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
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
            this.dtgDataDetail.DataSource = null;
            this.dtDataDetail.Rows.Clear();
            dtgDataDetail.Select();
            this.dtgDataInspect.DataSource = null;
            this.dtgDataInspect.Rows.Clear();
            dtgDataInspect.Select();

            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            cmbInspResult.Enabled = true;
            txtMatnr.Enabled = true;
            btnQuery.Enabled = true;
            ShowDdlInspect();
            ShowDdlInspect1();
            cmbInspResult1.Enabled = false;
            txtInspReason.Text = "";
            txtInspReason.Enabled = false;
            strWerks = "";
            strLgort = "";
            txtMatnr.Text = "";
            lblData.Text = 0 + " records";
            lblDataDetail.Text = 0 + " records";

            btnTaskID.Enabled = false;
            btnSave.Enabled = false;
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Produce TaskID
        private void btnTaskID_Click(object sender, EventArgs e)
        {
            try
            {
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                dtDataInspect.Clear();
                string strInspectResult1 = "";
                if (cmbInspResult1.SelectedIndex != -1)
                {
                    strInspectResult1 = cmbInspResult1.Items[cmbInspResult1.SelectedIndex].ToString();
                }
                if (string.IsNullOrEmpty(strInspectResult1))
                {
                    MessageBox.Show("The inspection results have not been maintained, please confirm！");
                    return;
                }

                //选择勾选的检验单据，判断条数是否大于0
                DataRow[] drTo = dtData.Select("Select=true");
                if (drTo.Count() > 0)
                {
                    DataTable dtTo = drTo.CopyToDataTable();
                    //针对选择行单据生成检验单号
                    //产生检验单号By厂区作区分
                    string strHeader = "R7" + DateTime.Now.ToString("yyyyMMdd");
                    string strSerno = objStorageData.GetInspectNo_IQC();
                    string strInspectNo = strHeader + (int.Parse(strSerno)).ToString("00000"); //检验单号

                    DataRow row = dtDataInspect.NewRow();
                    row["TASKID"] = strInspectNo;
                    row["RESULT"] = strInspectResult1;
                    row["REASON"] = txtInspReason.Text.Trim();
                    row["CHKQTY"] = dtTo.Rows[0]["MENGE"].ToString();
                    row["CHKNAM"] = UserData.UserId;
                    dtDataInspect.Rows.Add(row);
                    txtExpdat.Text = dtTo.Rows[0]["EXPDAT"].ToString();
                    txtNsmNo.Text = dtTo.Rows[0]["APPNO"].ToString();
                    txtMrbNo.Text = dtTo.Rows[0]["MRBNO"].ToString();
                    btnTaskID.Enabled = false;
                    btnSave.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "Unchecked inspection document！";
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strInspectResult = "";
                string strInspectResult1 = "";
                string strInspectReason = "";
                string strTaskid = "";
                string strExpdat = txtExpdat.Text.ToString().Trim();
                string strNsmNo = txtNsmNo.Text.ToString().Trim();
                string strMrbNo = txtMrbNo.Text.ToString().Trim();
                string strQCFlag = string.Empty;

                if (cmbInspResult.SelectedIndex != -1)
                {
                    strInspectResult = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();
                }
                if (cmbInspResult1.SelectedIndex != -1)
                {
                    strInspectResult1 = cmbInspResult1.Items[cmbInspResult1.SelectedIndex].ToString();
                }

                if (strInspectResult1 == "WAIVE" || strInspectResult1 == "REJECT")
                {
                    if (string.IsNullOrEmpty(strNsmNo) && string.IsNullOrEmpty(strMrbNo))
                    {
                        stsWarning.Text = "Please input NSM NO. or MRB NO.";
                        return;
                    }
                    else if (!string.IsNullOrEmpty(strNsmNo) && !string.IsNullOrEmpty(strMrbNo))
                    {
                        stsWarning.Text = "You may only select one NO. to input";
                        return;
                    }
                }

                if (!ClaCommon.CheckDateValid(strExpdat)) 
                {
                    stsWarning.Text = "Wrong Date Format.";
                    return;
                }
                
                DataRow[] drTo = dtData.Select("Select=true");
                if (drTo.Count() > 0)
                {
                    DataTable dtTo = drTo.CopyToDataTable();
                    //判断是否有检验信息
                    if (dtDataInspect.Rows.Count == 1)
                    {
                        #region 判断WHITM表是否有Taskid检验单号
                        strTaskid = dtTo.Rows[0]["TASKID"].ToString();

                        //Save 第一次检验结果信息
                        if (string.IsNullOrEmpty(strTaskid))
                        {
                            #region Save
                            //检验信息插入到WHIQC表
                            //更新TASKID—>WHITM表
                            if (objStorageData.SaveInspectInfo(dtTo, dtDataDetail, dtDataInspect, strExpdat, strNsmNo, strMrbNo, strInspectResult1))
                            {
                                stsWarning.Text = "Save success";

                                if (cmbInspResult.SelectedIndex != -1)
                                {
                                    strInspectResult = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();

                                    //string strEXPTP

                                    //switch (strInspectResult) 
                                    //{
                                    //    case "OK":
                                    //        strQCFlag = "";
                                    //}
                                }
                                //重新查询检验信息
                                dtData = objStorageData.QueryIQCOverdueStorageData(strWerks, strLgort, strInspectResult, "", "");
                                if (dtData.Rows.Count == 0)
                                {
                                    ShowDataGrid();
                                    this.dtgDataDetail.DataSource = null;
                                    this.dtDataDetail.Rows.Clear();
                                    dtgDataDetail.Select();
                                    this.dtgDataInspect.DataSource = null;
                                    this.dtgDataInspect.Rows.Clear();
                                    dtgDataInspect.Select();
                                    btnQuery.Enabled = true;

                                    ShowDdlInspect1();
                                    txtInspReason.Text = "";
                                    txtExpdat.Text = "";
                                    txtNsmNo.Text = "";
                                    txtMrbNo.Text = "";
                                    btnTaskID.Enabled = false;
                                    btnSave.Enabled = false;
                                }
                                else
                                {
                                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                                    DataColumn cType = new DataColumn("Type", typeof(string));
                                    dtData.Columns.Add(cSelect);
                                    dtData.Columns.Add(cType);
                                    for (int i = 0; i < dtData.Rows.Count; i++)
                                    {
                                        dtData.Rows[i]["Select"] = false;
                                    }
                                    ShowDataGrid();
                                    this.dtgDataDetail.DataSource = null;
                                    this.dtDataDetail.Rows.Clear();
                                    dtgDataDetail.Select();
                                    this.dtgDataInspect.DataSource = null;
                                    this.dtgDataInspect.Rows.Clear();
                                    dtgDataInspect.Select();

                                    ShowDdlInspect1();
                                    txtInspReason.Text = "";
                                    txtExpdat.Text = "";
                                    txtNsmNo.Text = "";
                                    txtMrbNo.Text = "";
                                }
                                //检验结果为OK，则跳转标签打印页面
                                if (strInspectResult1 == "OK" || strInspectResult1 == "WAIVE")
                                {
                                    IQC_PrintExpDate obj_IQC_PrintExpDate = new IQC_PrintExpDate(UserData, Progid, strExpdat, dtDataInspect.Rows[0]["TASKID"].ToString());
                                    obj_IQC_PrintExpDate.ShowDialog();
                                }
                            }
                            else
                            {
                                stsWarning.Text = "Save failed";
                                btnQuery.Enabled = false;
                                cmbInspResult1.Enabled = true;
                                txtInspReason.Enabled = true;
                                btnTaskID.Enabled = true;
                                btnSave.Enabled = true;
                                return;
                            }
                            #endregion
                        }
                        //Save 更新检验结果状态
                        else
                        {
                            #region Update //更新RESULT—>WHIQC表
                            strInspectReason = txtInspReason.Text.Trim();

                            if (objStorageData.UpdateInspectInfo(dtTo, dtDataDetail, dtDataInspect, strExpdat, strNsmNo, strMrbNo, strInspectResult1, strInspectReason))
                            {
                                stsWarning.Text = "Update success";

                                //重新查询检验信息
                                dtData = objStorageData.QueryIQCOverdueStorageData(strWerks, strLgort, strInspectResult, "", "");
                                if (dtData.Rows.Count == 0)
                                {
                                    ShowDataGrid();
                                    this.dtgDataDetail.DataSource = null;
                                    this.dtDataDetail.Rows.Clear();
                                    dtgDataDetail.Select();
                                    this.dtgDataInspect.DataSource = null;
                                    this.dtgDataInspect.Rows.Clear();
                                    dtgDataInspect.Select();
                                    btnQuery.Enabled = true;

                                    ShowDdlInspect1();
                                    txtInspReason.Text = "";
                                    txtExpdat.Text = "";
                                    txtNsmNo.Text = "";
                                    txtMrbNo.Text = "";
                                    btnTaskID.Enabled = false;
                                    btnSave.Enabled = false;
                                }
                                else
                                {
                                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                                    DataColumn cType = new DataColumn("Type", typeof(string));
                                    dtData.Columns.Add(cSelect);
                                    dtData.Columns.Add(cType);
                                    for (int i = 0; i < dtData.Rows.Count; i++)
                                    {
                                        dtData.Rows[i]["Select"] = false;
                                    }
                                    ShowDataGrid();
                                    this.dtgDataDetail.DataSource = null;
                                    this.dtDataDetail.Rows.Clear();
                                    dtgDataDetail.Select();
                                    this.dtgDataInspect.DataSource = null;
                                    this.dtgDataInspect.Rows.Clear();
                                    dtgDataInspect.Select();

                                    ShowDdlInspect1();
                                    txtInspReason.Text = "";
                                    txtExpdat.Text = "";
                                    txtNsmNo.Text = "";
                                    txtMrbNo.Text = "";
                                }
                                //检验结果为OK，则跳转标签打印页面
                                if (strInspectResult1 == "OK" || strInspectResult1 == "WAIVE")
                                {
                                    IQC_PrintExpDate obj_IQC_PrintExpDate = new IQC_PrintExpDate(UserData, Progid, strExpdat, dtDataInspect.Rows[0]["TASKID"].ToString());
                                    obj_IQC_PrintExpDate.ShowDialog();
                                }
                            }
                            else
                            {
                                stsWarning.Text = "Update failed";
                                return;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "No inspection information available, unable to update. Please confirm！";
                        return;
                    }
                }
                else
                {
                    stsWarning.Text = "The inspection document is not checked and cannot be saved. Please confirm！";
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region dtgData Detail
        private void dtgData_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string strMatnr = "";
                string strLifnr = "";
                string strDacod = "";
                string strTaskid = "";
                string strExpdat = "";
                string strNsmno = "";
                string strMrbno = "";
                stsWarning.Text = "";
                int intRowNo;
                DataGridView dgClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    try
                    {
                        strWerks = dgClick.Rows[intRowNo].Cells["Werks"].Value.ToString();
                        strLgort = dgClick.Rows[intRowNo].Cells["Lgort"].Value.ToString();
                        strMatnr = dgClick.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                        strLifnr = dgClick.Rows[intRowNo].Cells["Lifnr"].Value.ToString();
                        strDacod = dgClick.Rows[intRowNo].Cells["Dacod"].Value.ToString();
                        strTaskid = dgClick.Rows[intRowNo].Cells["Taskid"].Value.ToString();
                        strExpdat = dgClick.Rows[intRowNo].Cells["Expdat"].Value.ToString();
                        strNsmno = dgClick.Rows[intRowNo].Cells["Appno"].Value.ToString();
                        strMrbno = dgClick.Rows[intRowNo].Cells["Mrbno"].Value.ToString();
                        foreach (DataRow dtCheck in dtData.Rows)
                        {
                            dtCheck["Select"] = false;
                        }
                        dtData.Rows[intRowNo]["Select"] = true;
                        ShowDataGrid();

                        //库存明细
                        dtDataDetail = objStorageData.QueryIQCOverdueStorageData_Detail(strWerks, strLgort, strMatnr, strLifnr, strDacod);
                        this.ShowDetailDataGrid();

                        //检验信息
                        if (!string.IsNullOrEmpty(strTaskid))
                        {
                            dtDataInspect = objStorageData.QueryIQCOverdueStorageData(strWerks, strLgort, "", strTaskid, strMatnr);
                            this.ShowInspectDataGrid();
                            cmbInspResult1.Enabled = true;
                            txtInspReason.Enabled = true;
                            btnTaskID.Enabled = false;
                            txtExpdat.Text = strExpdat;
                            txtNsmNo.Text = strMrbno;
                            txtMrbNo.Text = strMrbno;
                            btnSave.Enabled = true;
                        }
                        else
                        {
                            dtDataInspect.Clear();
                            this.ShowInspectDataGrid();
                            cmbInspResult1.Enabled = true;
                            txtInspReason.Enabled = true;
                            btnTaskID.Enabled = true;
                            btnSave.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        strWerks = "";
                        strLgort = "";
                        strMatnr = "";
                        strDacod = "";
                        dtData.Rows[intRowNo]["Select"] = false;
                        ShowDataGrid();
                        dtDataDetail.Clear();
                        this.ShowDetailDataGrid();
                        dtDataInspect.Clear();
                        this.ShowInspectDataGrid();
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
    }
}