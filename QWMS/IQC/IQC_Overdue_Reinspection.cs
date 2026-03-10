using Newtonsoft.Json;
using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class IQC_Overdue_Reinspection : Form
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

        //private SQLAccess objDB;
        private StorageData objStorageData;
        private PlantData objPlantData;
        private Authority objAuthority;

        private PageState pageState = PageState.ReadOnly;
        private enum PageState { ReadOnly, Create, Modify, Close }

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
        private IQC_Overdue_Reinspection()
        {
            InitializeComponent();
        }
        public IQC_Overdue_Reinspection(UserInfo _UserData, string strProgid)
            : this()
        {
            UserData = _UserData;

            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                objStorageData = new StorageData(UserData, strWerks, strLgort);

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
                    //ShowDdlInspResult();
                    //ShowDdlMaterialType();
                    //ShowDdlExptp();

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
                    cmbResult.Enabled = true;
                    txtMatnr.Enabled = true;
                    btnQuery.Enabled = true;

                    //InitReInspectionLotInfo();

                    BindReInspectionLotInfo(string.Empty, string.Empty);
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
                DataTable dtResult = new DataTable();

                dtResult.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtResult.Rows.Add(new string[] { "" });
                dtResult.Rows.Add(new string[] { "OK" });
                dtResult.Rows.Add(new string[] { "NC REVIEW" });
                dtResult.Rows.Add(new string[] { "NG1" });
                dtResult.Rows.Add(new string[] { "NG2" });
                dtResult.Rows.Add(new string[] { "REJECT" });
                dtResult.Rows.Add(new string[] { "WAIVE" });
                //dtResult.Rows.Add(new string[] { "NSM APPLYING" });

                cmbResult.DisplayMember = "CNTEXT";
                cmbResult.ValueMember = "CNTEXT";
                cmbResult.DataSource = dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInspect()");
            }
        }

        private void ShowDdlInspResult()
        {
            try
            {
                DataTable dtResult = new DataTable();

                dtResult.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtResult.Rows.Add(new string[] { "" });
                dtResult.Rows.Add(new string[] { "OK" });
                dtResult.Rows.Add(new string[] { "NC REVIEW" });
                dtResult.Rows.Add(new string[] { "NG1" });
                dtResult.Rows.Add(new string[] { "NG2" });
                dtResult.Rows.Add(new string[] { "REJECT" });
                //dtResult.Rows.Add(new string[] { "WAIVE" });
                //dtResult.Rows.Add(new string[] { "NSM APPLYING" });

                cmbInspResult.DisplayMember = "CNTEXT";
                cmbInspResult.ValueMember = "CNTEXT";
                cmbInspResult.DataSource = dtResult;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInspResult()");
            }
        }
        #endregion

        #region 绑定检验类型
        private void ShowDdlExptp()
        {
            try
            {
                DataTable dtExptp = new DataTable();

                dtExptp.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT"), new DataColumn("CNVALUE") });

                //dtExptp.Rows.Add(new string[] { "", "" });
                dtExptp.Rows.Add(new string[] { "0", "绿区" });
                dtExptp.Rows.Add(new string[] { "1", "黄区" });
                dtExptp.Rows.Add(new string[] { "2", "红区" });

                cmbInspExptp.DisplayMember = "CNVALUE";
                cmbInspExptp.ValueMember = "CNTEXT";
                cmbInspExptp.DataSource = dtExptp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlExptp()");
            }
        }
        #endregion

        #region 绑定料号类型

        private void ShowDdlMaterialType()
        {
            try
            {
                DataTable dtMaterialType = new DataTable();

                dtMaterialType.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtMaterialType.Rows.Add(new string[] { "EE" });
                dtMaterialType.Rows.Add(new string[] { "ME" });

                cmbInspMaterialType.DisplayMember = "CNTEXT";
                cmbInspMaterialType.ValueMember = "CNTEXT";
                cmbInspMaterialType.DataSource = dtMaterialType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMaterialType()");
            }
        }
        #endregion

        #region 绑定不良现象
        private void BindNGSymptom(string strNGSymptoms)
        {
            try
            {
                string strMaterialType = "EE";

                if (cmbInspMaterialType.SelectedValue != null)
                    strMaterialType = cmbInspMaterialType.SelectedValue.ToString().Trim();
                else
                    cmbInspMaterialType.SelectedValue = strMaterialType;

                string strResult = cmbInspResult.Text;

                string[] arrNGSymptoms = strNGSymptoms.Split(';');

                DataTable dtNGSymptom = objStorageData.GetNGSymptom(strMaterialType, strResult);

                clbInspNGSymptom.Items.Clear();
                foreach (DataRow dr in dtNGSymptom.Rows)
                {
                    string strNGSymptom = dr["CTRLC1"].ToString().Trim();
                    bool bolCheck = arrNGSymptoms.Contains<string>(strNGSymptom);
                    clbInspNGSymptom.Items.Add(strNGSymptom, bolCheck);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-BindNGSymptom()");
            }

        }

        #endregion

        private void InitReInspectionLotInfo()
        {
            try
            {
                lblInspTaskid.Text = string.Empty;
                txtInspWerks.Text = string.Empty;
                txtInspLgort.Text = string.Empty;
                txtInspInsmk.Text = string.Empty;
                txtInspMatnr.Text = string.Empty;
                txtInspLifnr.Text = string.Empty;
                txtInspDacod.Text = string.Empty;
                txtInspVedat.Text = string.Empty;
                txtInspMenge.Text = string.Empty;
                txtInspChkQty.Text = string.Empty;
                ShowDdlInspResult();
                cmbResult.SelectedValue = string.Empty;
                txtInspExpdat.Text = string.Empty;
                txtInspMaxexp.Text = string.Empty;
                txtInspExpdatAfter.Text = string.Empty;
                txtInspMaxexpAfter.Text = string.Empty;
                txtInspNsmno.Text = string.Empty;
                lblInspTempid.Text = string.Empty;
                ShowDdlExptp();
                cmbInspExptp.SelectedValue = string.Empty;
                txtInspChknam.Text = string.Empty;
                txtInspReason.Text = string.Empty;
                ShowDdlMaterialType();
                //cmbInspMaterialType.SelectedValue = string.Empty;
                BindNGSymptom(string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-InitReInspectionLotInfo()");
            }
        }
        private void BindReInspectionLotInfo(string strTaskid, string strMenge)
        {
            try
            {
                DataTable dtTaskid = objStorageData.QueryIQCTaskID(strTaskid);

                if (dtTaskid != null && dtTaskid.Rows.Count > 0)
                {
                    lblInspTaskid.Text = dtTaskid.Rows[0]["TASKID"].ToString().Trim();
                    txtInspWerks.Text = dtTaskid.Rows[0]["WERKS"].ToString().Trim();
                    txtInspLgort.Text = dtTaskid.Rows[0]["LGORT"].ToString().Trim();
                    txtInspInsmk.Text = dtTaskid.Rows[0]["INSMK"].ToString().Trim();
                    txtInspMatnr.Text = dtTaskid.Rows[0]["MATNR"].ToString().Trim();
                    txtInspLifnr.Text = dtTaskid.Rows[0]["LIFNR"].ToString().Trim();
                    txtInspDacod.Text = dtTaskid.Rows[0]["DACOD"].ToString().Trim();
                    txtInspVedat.Text = dtTaskid.Rows[0]["VEDAT"].ToString().Trim();
                    if (!string.IsNullOrEmpty(strMenge) && Convert.ToInt32(strMenge) != Convert.ToInt32(dtTaskid.Rows[0]["MENGE"].ToString().Trim()))
                    {
                        txtInspMenge.Text = strMenge;
                    }
                    else
                    {
                        txtInspMenge.Text = dtTaskid.Rows[0]["MENGE"].ToString().Trim();
                    }
                    txtInspChkQty.Text = dtTaskid.Rows[0]["CHKQTY"].ToString().Trim();
                    string strResult = dtTaskid.Rows[0]["RESULT"].ToString().Trim();
                    cmbInspResult.SelectedValue = strResult;
                    txtInspExpdat.Text = dtTaskid.Rows[0]["EXPDAT"].ToString().Trim();
                    txtInspMaxexp.Text = dtTaskid.Rows[0]["MAXEXP"].ToString().Trim();
                    txtInspExpdatAfter.Text = dtTaskid.Rows[0]["EXPDAT_AFTER"].ToString().Trim();
                    txtInspMaxexpAfter.Text = dtTaskid.Rows[0]["MAXEXP_AFTER"].ToString().Trim();
                    txtInspQmexp.Text = dtTaskid.Rows[0]["QMEXP"].ToString().Trim();
                    txtInspNsmno.Text = dtTaskid.Rows[0]["APPNO"].ToString().Trim();
                    lblInspTempid.Text = dtTaskid.Rows[0]["TEMPID"].ToString().Trim();
                    string strExptp = dtTaskid.Rows[0]["EXPTP"].ToString().Trim();
                    cmbInspExptp.SelectedValue = strExptp;
                    txtInspChknam.Text = dtTaskid.Rows[0]["CHKNAM"].ToString().Trim();
                    txtInspReason.Text = dtTaskid.Rows[0]["RMAK1"].ToString().Trim();
                    cmbInspMaterialType.SelectedValue = dtTaskid.Rows[0]["MTYPE"].ToString().Trim();
                    BindNGSymptom(dtTaskid.Rows[0]["NGDES"].ToString().Trim());

                    if (strExptp == "0")
                    {
                        SetReInspectionLotStatus(PageState.ReadOnly);
                    }
                    else
                    {
                        if (strResult == "OK" || strResult == "WAIVE" || strResult == "REJECT")
                        {
                            SetReInspectionLotStatus(PageState.Close);
                        }
                        else if (strResult == "NG1" || strResult == "NG2")
                        {
                            SetReInspectionLotStatus(PageState.ReadOnly);
                        }
                        else
                        {
                            SetReInspectionLotStatus(PageState.Modify);
                        }
                    }
                }
                else
                {
                    InitReInspectionLotInfo();

                    //txtInspWerks.Text = string.Empty;
                    //txtInspLgort.Text = string.Empty;
                    //txtInspInsmk.Text = string.Empty;
                    //txtInspMatnr.Text = string.Empty;
                    //txtInspLifnr.Text = string.Empty;
                    //txtInspDacod.Text = string.Empty;
                    //txtInspVedat.Text = string.Empty;
                    //txtInspMenge.Text = string.Empty;
                    //txtInspChkQty.Text = string.Empty;

                    //cmbInspResult.SelectedValue = string.Empty;
                    //txtInspExpdat.Text = string.Empty;
                    //txtInspMaxexpAfter.Text = string.Empty;
                    //txtInspExpdat.Text = string.Empty;
                    //txtInspMaxexpAfter.Text = string.Empty;
                    //txtInspQmmax.Text = string.Empty;
                    //txtInspNsmno.Text = string.Empty;
                    //lblInspTempid.Text = string.Empty;
                    //cmbInspExptp.SelectedIndex = -1;
                    //txtInspChknam.Text = string.Empty;
                    //txtInspReason.Text = string.Empty;

                    SetReInspectionLotStatus(PageState.ReadOnly);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-BindReInspectionLotInfo()");
            }
        }
        private void SetReInspectionLotStatus(PageState state)
        {
            try
            {
                pageState = state;

                switch (pageState)
                {
                    case PageState.ReadOnly:
                        txtInspChkQty.ReadOnly = true;
                        cmbInspResult.Enabled = false;
                        txtInspNsmno.ReadOnly = true;
                        //lblInspTempid.ReadOnly = true;
                        cmbInspMaterialType.Enabled = false;
                        clbInspNGSymptom.Enabled = false;
                        txtInspReason.ReadOnly = true;

                        btnCreate.Enabled = false;
                        btnSave.Enabled = false;
                        btnPrint.Enabled = false;

                        break;
                    case PageState.Create:
                        txtInspChkQty.ReadOnly = true;
                        cmbInspResult.Enabled = false;
                        txtInspNsmno.ReadOnly = true;
                        //lblInspTempid.ReadOnly = true;
                        cmbInspMaterialType.Enabled = false;
                        clbInspNGSymptom.Enabled = false;
                        txtInspReason.ReadOnly = true;

                        btnCreate.Enabled = true;
                        btnSave.Enabled = false;
                        btnPrint.Enabled = false;

                        break;
                    case PageState.Modify:
                        txtInspChkQty.ReadOnly = false;
                        cmbInspResult.Enabled = true;
                        txtInspNsmno.ReadOnly = false;
                        //lblInspTempid.ReadOnly = true;
                        cmbInspMaterialType.Enabled = true;
                        clbInspNGSymptom.Enabled = true;
                        txtInspReason.ReadOnly = false;

                        btnCreate.Enabled = false;
                        btnSave.Enabled = true;
                        btnPrint.Enabled = false;

                        break;
                    case PageState.Close:
                        txtInspChkQty.ReadOnly = true;
                        cmbInspResult.Enabled = false;
                        txtInspNsmno.ReadOnly = true;
                        //lblInspTempid.ReadOnly = true;
                        cmbInspMaterialType.Enabled = false;
                        clbInspNGSymptom.Enabled = false;
                        txtInspReason.ReadOnly = true;

                        btnCreate.Enabled = true;
                        btnSave.Enabled = false;
                        btnPrint.Enabled = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-SetReInspectionLotStatus()");
            }
        }

        private void BindData()
        {
            string strWerks = string.Empty;
            string strLgort = string.Empty;
            string strMatnr = txtMatnr.Text.ToString().Trim();
            string strTaskid = txtTaskid.Text.ToString().Trim();
            string strInspectResult = cmbResult.SelectedValue.ToString().Trim();
            string strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
            string strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");

            this.stsWarning.Text = string.Empty;

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
                //if (cmbResult.SelectedIndex != -1)
                //{
                //    strInspectResult = cmbResult.Items[cmbResult.SelectedIndex].ToString();
                //}

                objStorageData = new StorageData(UserData, strWerks, strLgort);
                DataTable dtLgort = objStorageData.QueryIQCLgort(strWerks, strLgort);

                if (dtLgort.Rows.Count > 0)
                {
                    dtData = objStorageData.QueryIQCOverdueStorageData(strWerks, strLgort, strInspectResult, strTaskid, strMatnr);
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

                        cmbWerks.Enabled = true;
                        cmbLgort.Enabled = true;
                        cmbResult.Enabled = true;
                        txtMatnr.Enabled = true;
                        txtTaskid.Enabled = true;
                        cmbDateType.Enabled = false;
                        dtpStartDate.Enabled = false;
                        dtpEndDate.Enabled = false;

                        btnQuery.Enabled = true;
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
                dgvcSelect.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "Type";
                dgvcType.HeaderText = "Sampling Level";
                dgvcType.Name = "Type";
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
                dgvcRESULT.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcRESULT);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Name = "Werks";
                dgvcWerks.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Name = "Lgort";
                dgvcLgort.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Name = "Insmk";
                dgvcInsmk.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Quanta P/N";
                dgvcMatnr.Name = "Matnr";
                dgvcMatnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "VENDOR";
                dgvcLifnr.Name = "Lifnr";
                dgvcLifnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Input Date";
                dgvcIndat.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Quanlity";
                dgvcMenge.Name = "Menge";
                dgvcMenge.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "Vendor DateCode";
                dgvcDACOD.Name = "Dacod";
                dgvcDACOD.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcVEDAT = new DataGridViewTextBoxColumn();
                dgvcVEDAT.DataPropertyName = "VEDAT";
                dgvcVEDAT.HeaderText = "DateCode";
                dgvcVEDAT.Name = "Vedat";
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

                DataGridViewTextBoxColumn dgvcQmexp = new DataGridViewTextBoxColumn();
                dgvcQmexp.DataPropertyName = "QMMAX";
                dgvcQmexp.HeaderText = "QM Max Exp. Date";
                dgvcQmexp.Name = "Qmexp";
                dgvcQmexp.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcQmexp);

                DataGridViewTextBoxColumn dgvcTOTAL = new DataGridViewTextBoxColumn();
                dgvcTOTAL.DataPropertyName = "TOTAL";
                dgvcTOTAL.HeaderText = "Location Detail";
                dgvcTOTAL.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTOTAL);

                DataGridViewTextBoxColumn dgvcReqdat = new DataGridViewTextBoxColumn();
                dgvcReqdat.DataPropertyName = "Reqdat";
                dgvcReqdat.HeaderText = "Request Date";
                dgvcReqdat.Name = "Reqdat";
                dgvcReqdat.ReadOnly = true;
                dgvcReqdat.Visible = true;
                this.dtgData.Columns.Add(dgvcReqdat);

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

                DataGridViewTextBoxColumn dgvcExptp = new DataGridViewTextBoxColumn();
                dgvcExptp.DataPropertyName = "Exptp";
                dgvcExptp.HeaderText = "Type";
                dgvcExptp.Name = "Exptp";
                dgvcExptp.ReadOnly = true;
                dgvcExptp.Visible = false;
                this.dtgData.Columns.Add(dgvcExptp);

                dtgData.DataSource = dtData;

                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
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
                                dtData.Rows[i]["Type"] = "R（ Tighten）";

                                string strDateFormat = "yyyyMMdd";
                                DateTime timQmexp = DateTime.ParseExact(dtData.Rows[i]["QMMAX"].ToString(), strDateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                                if (timQmexp.Date < DateTime.Now.Date)
                                    this.dtgData.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Yellow;

                                break;
                        }

                        //if (dtData.Rows[i]["EXPTP"].ToString())
                        //{
                        //    this.dtgData.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;

                        //}
                    }
                }
                tabData.Text = string.Format("Inventory {0} records", dtData.Rows.Count.ToString());
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
                //lblDataDetail.Text = dtDataDetail.Rows.Count.ToString() + " records";
                tabDataDetail.Text = string.Format("Location {0} records", dtDataDetail.Rows.Count.ToString());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDetailDataGrid()");
            }
        }
        #endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
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

            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            cmbResult.Enabled = true;
            txtMatnr.Enabled = true;
            txtTaskid.Enabled = true;

            btnQuery.Enabled = true;
            ShowDdlInspect();
            ShowDdlInspResult();
            cmbInspResult.Enabled = false;
            txtInspReason.Text = "";
            //txtInspReason.Enabled = false;
            strWerks = "";
            strLgort = "";
            txtMatnr.Text = "";
            txtTaskid.Text = "";
            //lblData.Text = 0 + " records";
            tabData.Text = string.Format("Inventory {0} records", "0");
            //lblDataDetail.Text = 0 + " records";
            tabDataDetail.Text = string.Format("Location {0} records", "0");

            lblInspTaskid.Text = "";
            btnCreate.Enabled = false;
            btnSave.Enabled = false;
            txtInspWerks.Text = "";
            txtInspLgort.Text = "";
            txtInspInsmk.Text = "";
            txtInspMatnr.Text = "";
            txtInspLifnr.Text = "";
            txtInspDacod.Text = "";
            txtInspVedat.Text = "";
            txtInspMenge.Text = "";
            txtInspChkQty.Text = "";
            cmbInspResult.SelectedIndex = -1;
            txtInspExpdat.Text = "";
            txtInspMaxexp.Text = "";
            txtInspExpdatAfter.Text = string.Empty;
            txtInspMaxexpAfter.Text = string.Empty;
            txtInspQmexp.Text = "";
            txtInspNsmno.Text = string.Empty;
            lblInspTempid.Text = string.Empty;
            string strExptp = "";
            cmbInspExptp.SelectedValue = "";
            txtInspChknam.Text = "";
            txtInspReason.Text = string.Empty;
            cmbInspMaterialType.SelectedIndex = -1;
            txtEngID.Text = string.Empty;
            //BindNGSymptom(string.Empty);
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //#region Produce TaskID
        //private void btnTaskID_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objStorageData = new StorageData(UserData, strWerks, strLgort);
        //        dtDataInspect.Clear();
        //        string strInspectResult1 = "";
        //        if (cmbInspResult.SelectedIndex != -1)
        //        {
        //            strInspectResult1 = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();
        //        }
        //        if (string.IsNullOrEmpty(strInspectResult1))
        //        {
        //            MessageBox.Show("The inspection results have not been maintained, please confirm！");
        //            return;
        //        }

        //        //选择勾选的检验单据，判断条数是否大于0
        //        DataRow[] drTo = dtData.Select("Select=true");
        //        if (drTo.Count() > 0)
        //        {
        //            DataTable dtTo = drTo.CopyToDataTable();
        //            //针对选择行单据生成检验单号
        //            //产生检验单号By厂区作区分
        //            string strHeader = "R7" + DateTime.Now.ToString("yyyyMMdd");
        //            string strSerno = objStorageData.GetInspectNo_IQC();
        //            string strInspectNo = strHeader + (int.Parse(strSerno)).ToString("00000"); //检验单号

        //            DataRow row = dtDataInspect.NewRow();
        //            row["TASKID"] = strInspectNo;
        //            row["RESULT"] = strInspectResult1;
        //            row["REASON"] = txtInspReason.Text.Trim();
        //            row["CHKQTY"] = dtTo.Rows[0]["MENGE"].ToString();
        //            row["CHKNAM"] = UserData.UserId;
        //            dtDataInspect.Rows.Add(row);
        //            txtInspExpdat.Text = dtTo.Rows[0]["EXPDAT"].ToString();
        //            txtInspNsmno.Text = dtTo.Rows[0]["APPNO"].ToString();
        //            txtInspMrbno.Text = dtTo.Rows[0]["MRBNO"].ToString();
        //            btnTaskID.Enabled = false;
        //            btnSave.Enabled = true;
        //        }
        //        else
        //        {
        //            stsWarning.Text = "Unchecked inspection document！";
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //#endregion

        #region dtgData Detail
        private void dtgData_MouseClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region IQC要求刷入条码截取料号
        private void txtMatnr_KeyPress(object sender, KeyPressEventArgs e)
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
        #endregion

        private void dtgData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string strMatnr = "";
                string strLifnr = "";
                string strDacod = "";
                string strTaskid = "";
                string strMenge = "";
                //string strExpdat = "";
                //string strNsmno = "";
                //string strMrbno = "";
                stsWarning.Text = "";
                int intRowNo;
                DataGridView dtTaskid = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dtTaskid.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    try
                    {
                        strTaskid = lblInspTaskid.Text = dtTaskid.Rows[intRowNo].Cells["Taskid"].Value.ToString();

                        txtInspWerks.Text = strWerks = dtTaskid.Rows[intRowNo].Cells["Werks"].Value.ToString();
                        txtInspLgort.Text = strLgort = dtTaskid.Rows[intRowNo].Cells["Lgort"].Value.ToString();

                        strMatnr = dtTaskid.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                        strLifnr = dtTaskid.Rows[intRowNo].Cells["Lifnr"].Value.ToString();
                        strDacod = dtTaskid.Rows[intRowNo].Cells["Dacod"].Value.ToString();
                        strMenge = dtTaskid.Rows[intRowNo].Cells["Menge"].Value.ToString();

                        foreach (DataRow dtCheck in dtData.Rows)
                        {
                            dtCheck["Select"] = false;
                        }
                        dtData.Rows[intRowNo]["Select"] = true;
                        //ShowDataGrid();

                        //库存明细
                        dtDataDetail = objStorageData.QueryIQCOverdueStorageData_Detail(strWerks, strLgort, strMatnr, strLifnr, strDacod);
                        this.ShowDetailDataGrid();

                        //检验信息
                        if (!string.IsNullOrEmpty(strTaskid))
                        {
                            BindReInspectionLotInfo(strTaskid, strMenge);

                            //string strResult = cmbInspResult.SelectedValue.ToString().Trim();
                        }
                        else
                        {
                            txtInspWerks.Text = dtTaskid.Rows[intRowNo].Cells["Werks"].Value.ToString();
                            txtInspLgort.Text = dtTaskid.Rows[intRowNo].Cells["Lgort"].Value.ToString();
                            txtInspInsmk.Text = dtTaskid.Rows[intRowNo].Cells["Insmk"].Value.ToString();
                            txtInspMatnr.Text = dtTaskid.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                            txtInspLifnr.Text = dtTaskid.Rows[intRowNo].Cells["Lifnr"].Value.ToString();
                            txtInspDacod.Text = dtTaskid.Rows[intRowNo].Cells["Dacod"].Value.ToString();
                            txtInspVedat.Text = dtTaskid.Rows[intRowNo].Cells["Vedat"].Value.ToString();
                            txtInspMenge.Text = dtTaskid.Rows[intRowNo].Cells["Menge"].Value.ToString();
                            txtInspChkQty.Text = dtTaskid.Rows[intRowNo].Cells["Menge"].Value.ToString();
                            cmbInspResult.SelectedIndex = 0;
                            txtInspExpdat.Text = dtTaskid.Rows[intRowNo].Cells["Expdat"].Value.ToString();
                            txtInspMaxexp.Text = dtTaskid.Rows[intRowNo].Cells["Maxexp"].Value.ToString();
                            txtInspExpdatAfter.Text = string.Empty;
                            txtInspMaxexpAfter.Text = string.Empty;
                            txtInspQmexp.Text = dtTaskid.Rows[intRowNo].Cells["Qmexp"].Value.ToString();
                            txtInspNsmno.Text = string.Empty;
                            lblInspTempid.Text = string.Empty;
                            string strExptp = dtTaskid.Rows[intRowNo].Cells["Exptp"].Value.ToString();
                            cmbInspExptp.SelectedValue = strExptp;
                            txtInspChknam.Text = UserData.UserId;
                            txtInspReason.Text = string.Empty;
                            cmbInspMaterialType.SelectedIndex = 0;
                            BindNGSymptom(string.Empty);

                            SetReInspectionLotStatus(strExptp != "0" ? PageState.Create : PageState.ReadOnly);


                        }
                    }
                    catch (Exception ex)
                    {
                        txtInspWerks.Text = string.Empty;
                        txtInspLgort.Text = string.Empty;
                        txtInspMatnr.Text = string.Empty;
                        txtInspLifnr.Text = string.Empty;
                        txtInspDacod.Text = string.Empty;
                        txtInspExpdat.Text = string.Empty;
                        txtInspNsmno.Text = string.Empty;
                        lblInspTempid.Text = string.Empty;

                        dtData.Rows[intRowNo]["Select"] = false;
                        ShowDataGrid();
                        dtDataDetail.Clear();
                        this.ShowDetailDataGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string strWerks = txtInspWerks.Text.Trim();
                string strLgort = txtInspLgort.Text.Trim();
                string strInsmk = txtInspInsmk.Text.Trim();
                string strMatnr = txtInspMatnr.Text.Trim();
                string strLifnr = txtInspLifnr.Text.Trim();
                string strDacod = txtInspDacod.Text.Trim();
                string strVedat = txtInspVedat.Text.Trim();
                string strMenge = txtInspMenge.Text.Trim();
                string strChkQty = txtInspChkQty.Text.Trim();
                string strExpdat = txtInspExpdat.Text.Trim();
                string strMaxexp = txtInspMaxexp.Text.Trim();
                string strQmexp = txtInspQmexp.Text.Trim();
                string strExptp = cmbInspExptp.SelectedValue.ToString().Trim();
                string strChknam = txtInspChknam.Text.Trim();
                string strEngID = txtEngID.Text.Trim();
                string strTaskid = lblInspTaskid.Text.Trim();
                string strQCFlag = string.Empty;

                if (!ClaCommon.CheckDateValid(strExpdat) || !ClaCommon.CheckDateValid(strMaxexp))
                {
                    stsWarning.Text = "Wrong Date Format.";
                    return;
                }

                //#region 判断WHITM表是否有Taskid检验单号

                ////Save 第一次检验结果信息
                //if (string.IsNullOrEmpty(strTaskid))
                //{ }

                //#endregion



                #region Save
                //检验信息插入到WHIQC表
                //更新TASKID—>WHITM表
                if (objStorageData.CreateReInspectInfo(strWerks, strLgort, strInsmk, strMatnr, strLifnr, strDacod, strVedat, strMenge, strChkQty, strExpdat, strMaxexp, strQmexp, strExptp, strChknam, strEngID, dtDataDetail, out strTaskid))
                {
                    stsWarning.Text = "Save success";

                    DataRow[] drs = dtData.Select(string.Format("WERKS='{0}' AND LGORT='{1}' AND INSMK='{2}' AND MATNR='{3}' AND LIFNR='{4}' AND DACOD='{5}' AND VEDAT='{6}'", strWerks, strLgort, strInsmk, strMatnr, strLifnr, strDacod, strVedat));

                    //drs[0]["TASKID"] = strTaskid;
                    //ShowDataGrid();

                    BindReInspectionLotInfo(strTaskid, string.Empty);

                    BindData();
                }
                else
                {
                    stsWarning.Text = "Save failed";
                }
                #endregion


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strTaskid = lblInspTaskid.Text.Trim();
                string strWerks = txtInspWerks.Text.Trim();
                string strLgort = txtInspLgort.Text.Trim();
                //string strInsmk = txtInspInsmk.Text.Trim();
                string strMatnr = txtInspMatnr.Text.Trim();
                string strLifnr = txtInspLifnr.Text.Trim();
                //string strDacod = txtInspDacod.Text.Trim();
                string strVedat = txtInspVedat.Text.Trim();
                string strMenge = txtInspMenge.Text.Trim();
                string strChkQty = txtInspChkQty.Text.Trim();
                string strExpdat = txtInspExpdat.Text.Trim();
                string strQmexp = txtInspQmexp.Text.Trim();
                string strMaxexp = txtInspMaxexp.Text.Trim();
                string strNsmno = txtInspNsmno.Text.Trim();
                string strTempid = lblInspTempid.Text.Trim();
                string strExptp = cmbInspExptp.SelectedValue.ToString().Trim();
                string strChknam = txtInspChknam.Text.Trim();
                string strReason = txtInspReason.Text.Trim();
                string strEngID = txtEngID.Text.Trim();
                string strMaterialType = cmbInspMaterialType.SelectedValue.ToString().Trim();

                string strNGSymptom = string.Empty;
                foreach (object item in clbInspNGSymptom.CheckedItems)
                {
                    if (!string.IsNullOrEmpty(strNGSymptom))
                        strNGSymptom += ";";
                    strNGSymptom += item.ToString();
                }

                btnSave.Enabled = false;
                if (strNGSymptom.Contains("真空包装漏气") && strNGSymptom.Contains("真空包装未漏气"))
                {
                    MessageBox.Show("不能同时选择真空包装漏气和未漏气");
                    return;
                }
                if (strNGSymptom.Contains("湿度卡变色") && strNGSymptom.Contains("湿度卡未变色"))
                {
                    MessageBox.Show("不能同时选择湿度卡变色和未变色");
                    return;
                }
                if (strNGSymptom.Contains("湿度卡变色") && strNGSymptom.Contains("无湿度卡"))
                {
                    MessageBox.Show("不能同时选择湿度卡变色和无湿度卡");
                    return;
                }

                if (strNGSymptom.Contains("其他") && string.IsNullOrEmpty(strReason))
                {
                    MessageBox.Show("不良原因选择其他，请备注原因");
                    return;
                }

                if (!ClaCommon.CheckDateValid(strExpdat) || !ClaCommon.CheckDateValid(strMaxexp)) //|| !ClaCommon.CheckDateValid(strQmmax)
                {
                    //stsWarning.Text = "Wrong Date Format.";
                    //return;

                    MessageBox.Show("Wrong Date Format.");
                    return;
                }

                string strQCFlag = string.Empty;
                string strResult = cmbInspResult.SelectedValue.ToString().Trim();

                if (strResult == "WAIVE")
                {
                    if (string.IsNullOrEmpty(strNsmno))
                    {
                        MessageBox.Show("Please input NSM NO.");
                        return;
                    }
                }

                if (strResult == "OK" || strResult == "NG1" || strResult == "NG2")
                {
                    if (strExptp == "2")
                    {
                        string strDateFormat = "yyyyMMdd";
                        DateTime timQmexp = DateTime.ParseExact(strQmexp, strDateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                        if (timQmexp.Date < DateTime.Now.Date)
                        {
                            MessageBox.Show("QM展延后仍然过期，可以直接REJECT");
                            return;
                        }
                    }
                }

                if (strResult == "OK")
                {
                    if (!string.IsNullOrEmpty(strNGSymptom))
                    {
                        MessageBox.Show("检验结果为OK，不需要勾选不良现象");
                        return;
                    }

                    if (MessageBox.Show("检验结果为OK，有效期将会被展延，请确认", "Caution", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if (strResult == "NG1" || strResult == "NG2")
                {
                    if (string.IsNullOrEmpty(strNGSymptom))
                    {
                        MessageBox.Show("Please select NG Symptom.");
                        return;
                    }

                    if (MessageBox.Show("检验结果是NG，将会发送到超期再检系统，请确认\r\n The inspect result is NG, will create a non-standard process, are you sure?", "Caution", MessageBoxButtons.OKCancel)
                         == System.Windows.Forms.DialogResult.OK)
                    {
                        var inspData = new[]{
                            new {
                                TASKID = strTaskid,
                                COMCD = UserData.CompanyCode,
                                WERKS = strWerks,
                                QCFLG = strExptp,
                                RESULT = strResult,
                                LIFNR = strLifnr,
                                MATNR = strMatnr,
                                MENGE = strMenge,
                                DACOD = strVedat,
                                EXPDAT = strExpdat,
                                MAXEXP = strMaxexp,
                                REASON = string.IsNullOrEmpty(strReason) ? strNGSymptom : strNGSymptom + ":" + strReason
                            }
                        };

                        var json = JsonConvert.SerializeObject(inspData);

                        //var str = await doPostAsync(json);

                        ////测试
                        //string strUrl = @"https://mzl-sj1r8kxp.quanta-camp.com/ScmDefectApi/Api/ScmAutoNSMApi/ApiInsertAutoNSMMainData";
                        //正式
                        string strUrl = @"https://mps-scmdefect.quanta-camp.com/ScmDefectApi/Api/ScmAutoNSMApi/ApiInsertAutoNSMMainData";

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        ClaHttpHelper httpHelper = new ClaHttpHelper();
                        string strMessage = httpHelper.HttpPostByHttpWebRequest(strUrl, json);

                        var returnMsg = JsonConvert.DeserializeObject<dynamic>(strMessage);

                        if (returnMsg.code == "200")
                        {
                            strTempid = returnMsg.data.TEMPID;
                        }
                        else
                        {
                            MessageBox.Show(returnMsg.message);
                            return;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "Please select the correct inspect result";
                        return;
                    }

                }


                #region 判断WHITM表是否有Taskid检验单号

                //获取入库时间
                string strGetCrdat;
                strGetCrdat = objStorageData.QueryCrdat(strMatnr,strWerks,strLgort);

                #region Update //更新RESULT—>WHIQC表
                strReason = txtInspReason.Text.Trim();
                if (cmbResult.Text == "NC REVIEW" && string.IsNullOrEmpty(txtEngID.Text))
                {
                    MessageBox.Show("请填写工程师工号:EngID!!");
                    return;
                }
                if (objStorageData.UpdateReInspectInfo(strTaskid, strWerks, strLgort, strChkQty, strResult, strExpdat, strMaxexp, strQmexp, strNsmno, string.Empty, strExptp, strChknam, strReason, strMaterialType, strNGSymptom, strTempid, strEngID, strGetCrdat))
                {
                    stsWarning.Text = "Update success";
                    BindReInspectionLotInfo(strTaskid, string.Empty);
                    BindData();
                }
                else
                {
                    stsWarning.Text = "Update failed";
                    return;
                }
                #endregion

                //检验结果为OK，则跳转标签打印页面
                if (strResult == "OK" || strResult == "WAIVE")
                {

                    IQC_PrintExpDate obj_IQC_PrintExpDate = new IQC_PrintExpDate(UserData, Progid, strExpdat, strTaskid);
                    obj_IQC_PrintExpDate.ShowDialog();
                }


                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //private async Task<string> doPostAsync(string json)
        //{
        //    string result = string.Empty;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        string strUrl = @"https://mzl-sj1r8kxp.quanta-camp.com/ScmDefectApi/Api/ScmAutoNSMApi/ApiInsertAutoNSMMainData";

        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await client.PostAsync(strUrl, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            result = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    return result;
        //}
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strTaskid = lblInspTaskid.Text.Trim();
            string strExpdat = txtInspExpdat.Text.Trim();

            string strResult = cmbInspResult.SelectedValue.ToString().Trim();
            //if (cmbInspResult.SelectedIndex != -1)
            //{
            //    strResult = cmbInspResult.Items[cmbInspResult.SelectedIndex].ToString();
            //}

            //检验结果为OK，则跳转标签打印页面
            if (strResult == "OK" || strResult == "WAIVE")
            {
                IQC_PrintExpDate obj_IQC_PrintExpDate = new IQC_PrintExpDate(UserData, Progid, strExpdat, strTaskid);
                obj_IQC_PrintExpDate.ShowDialog();
            }
        }

        private void cmbInspMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strNGSymptom = string.Empty;
            foreach (object item in clbInspNGSymptom.CheckedItems)
            {
                if (!string.IsNullOrEmpty(strNGSymptom))
                    strNGSymptom += ";";
                strNGSymptom += item.ToString();
            }
            BindNGSymptom(strNGSymptom);
        }

        private void cmbInspResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strNGSymptom = string.Empty;
            foreach (object item in clbInspNGSymptom.CheckedItems)
            {
                if (!string.IsNullOrEmpty(strNGSymptom))
                    strNGSymptom += ";";
                strNGSymptom += item.ToString();
            }
            BindNGSymptom(strNGSymptom);
        }

        private void txtQuantaPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindData();
            }
        }
    }
}