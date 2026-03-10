using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_SapDocumentNo : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strRefid = "";
        private string strMblnr = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();
        private DataTable dtQty = new DataTable();
        private DataTable dtMblnrData = new DataTable();
        private DataTable dtMblnrQty = new DataTable();

        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private Admin objAdmin;

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

        public string Refid
        {
            get
            {
                return this.txtRefid.Text.Trim();
            }
            set
            {
                this.txtRefid.Text = value;
            }
        }
        #endregion

        #region 构造函数
        public Manage_SapDocumentNo()
        {
            InitializeComponent();
        }

        public Manage_SapDocumentNo(UserInfo varUserData, string strProgid, string strType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);

                //檢查權限
                if (strType.ToUpper() == "ADMIN")
                {
                    if (!objAdmin.CheckAuthority())
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }
                else if (strType.ToUpper() == "MANAGE")
                {
                    if (!objStorageIn.CheckAuthority("MANAGE"))
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }

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
        # endregion

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

        #region txtRefid Double Click Event
        private void txtRefid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_RefidSelect objManage_RefidSelect = new Manage_RefidSelect(UserData, Progid, Werks, Lgort);
                    objManage_RefidSelect.ShowDialog();
                    txtRefid.Text = objManage_RefidSelect.Refid;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                strRefid = txtRefid.Text.Trim();

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                if (strRefid == "")
                {
                    stsWarning.Text = "Reference id can't be empty!!";
                    return;
                }

                //取得Refid资料
                dtData = objPlantData.GetRefidData(strWerks, strLgort, strRefid);
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Reference id data!!";
                    return;
                }
                ShowDataGrid();
                gbFunction.Enabled = true;
                txtMblnr.Enabled = true;
                btnQuery.Enabled = true;
                txtMblnr.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "REFID";
                dgvcRefid.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcDIDNO = new DataGridViewTextBoxColumn();
                dgvcDIDNO.DataPropertyName = "DIDNO";
                dgvcDIDNO.HeaderText = "DID No";
                dgvcDIDNO.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDIDNO);

                DataGridViewTextBoxColumn dgvcPart = new DataGridViewTextBoxColumn();
                dgvcPart.DataPropertyName = "MATNR";
                dgvcPart.HeaderText = "Part No";
                dgvcPart.ReadOnly = true;
                this.gvData.Columns.Add(dgvcPart);

                DataGridViewTextBoxColumn dgvcStock = new DataGridViewTextBoxColumn();
                dgvcStock.DataPropertyName = "INSMK";
                dgvcStock.HeaderText = "Stock";
                dgvcStock.ReadOnly = true;
                this.gvData.Columns.Add(dgvcStock);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "Version";
                dgvcVersion.ReadOnly = true;
                this.gvData.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcQty = new DataGridViewTextBoxColumn();
                dgvcQty.DataPropertyName = "MENGE";
                dgvcQty.HeaderText = "Qty";
                dgvcQty.ReadOnly = true;
                this.gvData.Columns.Add(dgvcQty);

                DataGridViewTextBoxColumn dgvcVendor = new DataGridViewTextBoxColumn();
                dgvcVendor.DataPropertyName = "LIFNR";
                dgvcVendor.HeaderText = "Vendor";
                dgvcVendor.ReadOnly = true;
                this.gvData.Columns.Add(dgvcVendor);

                DataGridViewTextBoxColumn dgvcDept = new DataGridViewTextBoxColumn();
                dgvcDept.DataPropertyName = "KOSTL";
                dgvcDept.HeaderText = "Dept No";
                dgvcDept.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDept);

                this.gvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #endregion

        #region ShowDocNoDataGrid
        private void ShowDocNoDataGrid()
        {
            try
            {
                this.gvDocNoData.AutoGenerateColumns = false;
                this.gvDocNoData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "Document No";
                dgvcMBLNR.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "Document Item";
                dgvcZEILE.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Part No";
                dgvcMATNR.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                dgvcKDMAT.DataPropertyName = "KDMAT";
                dgvcKDMAT.HeaderText = "Customer P/N";
                dgvcKDMAT.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcKDMAT);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "Stock";
                dgvcINSMK.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcINSMK);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "Version";
                dgvcCHARG.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "Vendor";
                dgvcLIFNR.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "Qty";
                dgvcOTQTY.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcOTQTY);

                DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                dgvcKOSTL.DataPropertyName = "KOSTL";
                dgvcKOSTL.HeaderText = "Dept No.";
                dgvcKOSTL.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcKOSTL);

                DataGridViewTextBoxColumn dgvcARBPL = new DataGridViewTextBoxColumn();
                dgvcARBPL.DataPropertyName = "ARBPL";
                dgvcARBPL.HeaderText = "PD Line.";
                dgvcARBPL.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcARBPL);

                DataGridViewTextBoxColumn dgvcTRNTP = new DataGridViewTextBoxColumn();
                dgvcTRNTP.DataPropertyName = "TRNTP";
                dgvcTRNTP.HeaderText = "Trn-Type";
                dgvcTRNTP.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcTRNTP);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "Movement";
                dgvcBWART.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcINSPT = new DataGridViewTextBoxColumn();
                dgvcINSPT.DataPropertyName = "INSPT";
                dgvcINSPT.HeaderText = "Insp.Lot No.";
                dgvcINSPT.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcINSPT);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "Date Code";
                dgvcSERNO.ReadOnly = true;
                this.gvDocNoData.Columns.Add(dgvcSERNO);

                this.gvDocNoData.DataSource = dtMblnrData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDocNoDataGrid()");
            }
        }
        #endregion

        #region cmbWerks Selected Index Changed
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region Query     
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                strMblnr = txtMblnr.Text.Trim();
                stsWarning.Text = "";
                if (strMblnr == "")
                {
                    stsWarning.Text = "Document No. can't be empty!!";
                    return;
                }

                //取得扣帐编号(Document No)资料
                dtMblnrData = objPlantData.GetDocNoData(strWerks, strLgort, strMblnr);

                if (dtMblnrData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Document No. data!!";
                    return;
                }
                ShowDocNoDataGrid();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSave.Enabled = false;


                //取得Refid 依據料號(MATNR)作加總後的數量
                dtQty = objPlantData.GetRefidQty(strWerks, strLgort, strRefid);
  

                //取得SAP扣帳編號(Document No.)依據料號(MATNR)作加總後的數量
                dtMblnrQty = objPlantData.GetDocNoQty(strWerks, strLgort, strMblnr);

                if (dtQty.Rows.Count == dtMblnrQty.Rows.Count)
                {
 
                    objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Progid, Lgort, "", Progid);

                    if (int.Parse(dtData.Rows[0]["OTQTY"].ToString().Trim()) > 0)
                    {
                        //若倉庫已處理過，則不需要處理
                        stsWarning.Text = "倉庫已處理過，不需要處理!! " + objStorageIn.ERRMSG;
                        this.btnSave.Enabled = false;
                    }
                    else
                    {
                        if (objStorageIn.UpdateRefidQty(strMblnr, strRefid))
                        {
                            stsWarning.Text = "QWMS扣帳已OK!!";
                            this.btnSave.Enabled = false;

                            return;
                        }
                        else
                        {
                            stsWarning.Text = "Update fail!! " + objStorageIn.ERRMSG;
                            this.btnSave.Enabled = true;
                            return;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                this.btnSave.Enabled = true;
                return;
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.txtRefid.Text = "";
            this.txtMblnr.Text = "";
            if (cmbWerks.Items.Count > 0)
            {
                this.cmbWerks.SelectedIndex = 0;
            }
            if (cmbLgort.Items.Count > 0)
            {
                this.cmbLgort.SelectedIndex = 0;
            }
            this.gbHeader.Enabled = true;
            this.gbFunction.Enabled = false;
            this.dtData.Rows.Clear();
            this.gvData.DataSource = null;
            this.dtMblnrData.Rows.Clear();
            this.gvDocNoData.DataSource = null;
            strWerks = "";
            strMblnr = "";
            strLgort = "";
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 调整布局大小
        private void Manage_SapDocumentNo_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion
        
    }
}
