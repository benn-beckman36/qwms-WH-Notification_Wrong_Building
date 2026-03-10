using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using QCI.QWMS;
using QWMS.Common;
using QCI_QWMS_Models;

namespace QWMS.Models
{
    public partial class Model_StorageOut_OffLineOut : Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strMrgid = "";
        private string strIsmrg = "N";
        private string strComcd = "";
        private DataTable dtOutSource = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        UserInfo UserData = new UserInfo();
        private int intFormIndex = 0;

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

        public string Mrgid
        {
            get
            {
                return strMrgid;
            }
            set
            {
                strMrgid = value;
            }
        }

        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
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

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }
        public string Charg
        {
            get
            {
                return strCharg;
            }
            set
            {
                strCharg = value;
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

        public Model_StorageOut_OffLineOut(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                //檢查權限
                if (!objStorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowPrintCheckBox();
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

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowPrintCheckBox()
        {
            bool bolPrint = false;
            try
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                bolPrint = objStorageOut.CheckPrintCheckBox();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowPrintCheckBox()");
            }
            if (bolPrint == true)
            {
                this.chkPrint.Checked = true;
            }
            else
            {
                this.chkPrint.Checked = false;
            }
        }


        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                
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
                    //模号不可为空
                    if (txtModelNo.Text.Trim() == "")
                    {
                        stsWarning.Text = "ModelNo can't be empty";
                        return;
                    }

                    ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                    //资产编号、模号是否存在
                    if (this.txtAssetNo.Text.Trim() != "")
                    {
                        if (!objModelsData.CheckExistedAssetsModel(this.txtModelNo.Text.Trim(), this.txtAssetNo.Text.Trim()))
                        {
                            MessageBox.Show(
                                this.txtModelNo.Text.Trim() + "and" + this.txtAssetNo.Text.Trim() + " doesn't exist!!",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    else
                    {
                        if (!objModelsData.CheckExistedAssetsModel(this.txtModelNo.Text.Trim(), ""))
                        {
                            MessageBox.Show(
                                this.txtModelNo.Text.Trim() + " doesn't exist!!",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string strModels = txtModelNo.Text.Trim();
                    string strAssets = txtAssetNo.Text.Trim();

                    dtOutSource = objModelsData.QueryModelData("",strModels, strAssets);
                    if (dtOutSource.Rows.Count > 0)
                    {
                        ShowOutSourceDataGridView();

                    }
                    if (dtOutSource.Rows.Count == 0)
                    {
                        stsWarning.Text = "No data!!";
                        return;
                    }
                
                
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        public void ShowOutSourceDataGridView()
        {
            this.dgvOutSource.AutoGenerateColumns = false;
            this.dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcModelNO = new DataGridViewTextBoxColumn();
                dgvcModelNO.DataPropertyName = "ModelNO";
                dgvcModelNO.HeaderText = "模号";
                dgvcModelNO.Width = 90;
                dgvcModelNO.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcModelNO);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "资产编号";
                dgvcAssetsNo.Width = 90;
                dgvcAssetsNo.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "品名";
                dgvcItemName.Width = 50;
                dgvcItemName.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcItemName);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 60;
                dgvcBU.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 90;
                dgvcMachine.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMachine);

                DataGridViewTextBoxColumn dgvcQuantity = new DataGridViewTextBoxColumn();
                dgvcQuantity.DataPropertyName = "Quantity";
                dgvcQuantity.HeaderText = "数量";
                dgvcQuantity.Width = 90;
                dgvcQuantity.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcQuantity);

                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "净重";
                dgvcNweight.Width = 90;
                dgvcNweight.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcNweight);

                DataGridViewTextBoxColumn dgvcPoNo = new DataGridViewTextBoxColumn();
                dgvcPoNo.DataPropertyName = "PoNo";
                dgvcPoNo.HeaderText = "PO.";
                dgvcPoNo.Width = 90;
                dgvcPoNo.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcPoNo);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "模具备注";
                dgvcRemark.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcRemark);

                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "建立者";
                dgvcCRNAM.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcCRNAM);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRDAT";
                dgvcCRNAM.HeaderText = "建立时间";
                dgvcCRNAM.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcCRDAT);

                dgvOutSource.DataSource = dtOutSource;
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " records";

                if (dtOutSource.Rows.Count > 0) 
                {
                    this.panel1.Enabled = false;
                    this.btnQuery.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void ShowStorageDataGridView()
        {
            this.dgvStorage.AutoGenerateColumns = false;
            this.dgvStorage.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Model No";
                dgvcMatnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "Assets No";
                dgvcAssetsNo.Width = 90;
                dgvcAssetsNo.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcdatecode = new DataGridViewTextBoxColumn();
                dgvcdatecode.DataPropertyName = "DACOD";
                dgvcdatecode.HeaderText = "DateCode";
                dgvcdatecode.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcdatecode);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcrmak1);

                dgvStorage.DataSource = dtCombineStorage;
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                if (dtCombineStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow drRow;
                DataSet dsData = new DataSet();
                strIsmrg = "N";
                string strOrderBy = "";
                DataTable dtTempStorage = new DataTable();
                strWerks = cmbWerks.SelectedItem.ToString();
                strLgort = cmbLgort.SelectedItem.ToString();

                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "Please input data first!!";
                    return;
                }

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                dtCombineStorage = objModelsData.QueryStorageModelData(strWerks, strLgort, dtOutSource);
                //dtTempStorage = dsData.Tables[0].Copy();
                dtCombineStorage.Columns.Add("ALQTY");


                if (dtCombineStorage.Rows.Count > 0)
                {
                    drRow = dtCombineStorage.NewRow();
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        dtCombineStorage.Rows[i]["ALQTY"] = dtCombineStorage.Rows[i]["MENGE"].ToString();
                        //drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        //drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        //drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        //drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                        //drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                        //drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        //drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        //drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        //drRow["MENGE"] = dtTempStorage.Rows[i]["MENGE"].ToString();
                        //drRow["ALQTY"] = dtTempStorage.Rows[i]["MENGE"].ToString();
                        //drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        //drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        //drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                        //drRow["RMAK1"] = dtTempStorage.Rows[i]["RMAK1"].ToString(); ;
                        //drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString(); ;
                        //drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        //drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                        
                    }
                    
                    ShowStorageDataGridView();
                }
                if (dtCombineStorage.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();
            try
            {
                stsWarning.Text = "";

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                if (objModelsData.ModelOffLineOutData(dtOutSource, dtCombineStorage))
                    {
                        stsWarning.Text = "Update OK!!";
                        this.btnSave.Enabled = false;
                        this.btnPrint.Enabled = true;
                        if (chkPrintHeader.Checked)
                        {
                            //列印Header資料
                            ReportPrint objReportPrint1 = new ReportPrint(UserData, "MODELSTOREOUTHEADER", dtOutSource);
                            objReportPrint1.Report.PrintToPrinter(1, true, 0, 0);
                        }
                        if (chkPrint.Checked)
                        {
                            //列印實際出庫資料

                            ReportPrint objReportPrint = new ReportPrint(UserData, "MODELSTOREOUT", dtCombineStorage);
                            objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
                        }
                    }
                    else
                    {
                        stsWarning.Text = "Update fail!! " + objModelsData.ERRMSG;
                        SetbtnSaveException();
                        return;
                    }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetbtnSaveException();
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (chkPrintHeader.Checked)
            {
                //列印Header資料
                ReportPrint objReportPrint1 = new ReportPrint(UserData, "MODELSTOREOUTHEADER", dtOutSource);
                objReportPrint1.MdiParent = this.ParentForm;
                objReportPrint1.Show();
            }
            
            //列印實際出庫資料
            ReportPrint objReportPrint = new ReportPrint(UserData, "MODELSTOREOUT", dtCombineStorage);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            ShowPrintCheckBox();

            this.lblOutSource.Text = "0 records";
            this.lblStorage.Text = "0 records";
            this.dtOutSource.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.stsWarning.Text = "";
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.panel1.Enabled = true;
            this.txtModelNo.Text = "";
            this.txtAssetNo.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Model_StorageIn_OffLineIn_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        private bool CheckIsOpen(string strForm)
        {
            bool bolOpened = false;
            string strTest = "";
            try
            {
                for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
                {
                    strTest = MdiParent.MdiChildren[i].ToString();
                    if (MdiParent.MdiChildren[i].ToString().IndexOf(strForm) != -1)
                    {
                        bolOpened = true;
                        intFormIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckIsOpen()");
            }
            return bolOpened;
        }

        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

    }
}
