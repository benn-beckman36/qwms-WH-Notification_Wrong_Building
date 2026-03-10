using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class Admin_DocumentAddQty : Form
    {
        public Admin_DocumentAddQty()
        {
            InitializeComponent();
        }

        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMblnr = "";
        private string strProgid = "";
        private string strCrdat = "";
        private Admin objAdmin;
        private DataTable dtData = new DataTable();
        private DataTable dtReturn = new DataTable();

        #endregion


        #region DataMember

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

        #endregion

        public Admin_DocumentAddQty(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            strComcd = varUserData.CompanyCode;

            try
            {
                objAdmin = new Admin(UserData, Progid);

                //檢查權限
                if (!objAdmin.CheckAuthority())
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 設定State Bar中的日期
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
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Doc Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcMdqty = new DataGridViewTextBoxColumn();
                dgvcMdqty.DataPropertyName = "MDQTY";
                dgvcMdqty.HeaderText = "Add Qty";
                dgvcMdqty.Width = 90;
                dgvcMdqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMdqty);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Cost Center";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 110;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcBwart = new DataGridViewTextBoxColumn();
                dgvcBwart.DataPropertyName = "BWART";
                dgvcBwart.HeaderText = "Mvt.";
                dgvcBwart.Width = 50;
                dgvcBwart.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBwart);

                DataGridViewTextBoxColumn kdmatStyle = new DataGridViewTextBoxColumn();
                kdmatStyle.DataPropertyName = "KDMAT";
                kdmatStyle.HeaderText = "Customer Part No.";
                kdmatStyle.Width = 110;
                kdmatStyle.ReadOnly = true;
                dgvData.Columns.Add(kdmatStyle);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                strMblnr = txtMblnr.Text.Trim();
                strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                //單號不為空
                if (strMblnr == "")
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtData = objSapData.QueryAddSimulationDoc(strMblnr, strCrdat);
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
                ShowDataGrid();

                gbHeader.Enabled = false;
                btnSave.Enabled = true;
                btnPrint.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            this.stsWarning.Text = "";
            this.txtMblnr.Text = "";
            this.cmbWerks.SelectedIndex = -1;
            this.cmbLgort.SelectedIndex = -1;
            this.lblData.Text = "0 records";
            this.dgvData.DataSource = null;
            this.gbHeader.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.cmbWerks.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string strMblnr = "";
                string strMatnr = "";
                string strZeile = "";
                string strInsmk = "";
                string strCharg = "";
                int intMenge = 0;

                strMblnr = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[0].Value);
                strZeile = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[1].Value);
                strMatnr = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[2].Value);
                strInsmk = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[3].Value);
                strCharg = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[4].Value);
                intMenge = int.Parse(Convert.ToString(dgvData.Rows[e.RowIndex].Cells[7].Value));

                Admin_DocumentAddQty_Modify objAdmin_DocumentAddDecrease_ModifyQty = new Admin_DocumentAddQty_Modify(UserData, Werks, Lgort, Progid, strMatnr, strInsmk, strCharg, strMblnr, strZeile, dtData);
                objAdmin_DocumentAddDecrease_ModifyQty.Mblnr = strMblnr;
                objAdmin_DocumentAddDecrease_ModifyQty.Matnr = strMatnr;
                objAdmin_DocumentAddDecrease_ModifyQty.Zeile = strZeile;
                objAdmin_DocumentAddDecrease_ModifyQty.Menge = intMenge;
                objAdmin_DocumentAddDecrease_ModifyQty.ShowDialog();
                dtData = objAdmin_DocumentAddDecrease_ModifyQty.QtyData;
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                int intQwmsQty = 0;
                DataTable dtSave = new DataTable();
                dtSave = dtData.Clone();

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //先掃瞄DataTable(dtData)中的資料，若MdQty(加扣數量)有變更才儲存
                    if (int.Parse(dtData.Rows[i]["MdQty"].ToString()) > 0)
                    {
                        dtSave.ImportRow(dtData.Rows[i]);
                    }
                }

                //需加扣的料號，先查詢該料號在QWMS中的庫存數量
                for (int i = 0; i < dtSave.Rows.Count; i++)
                {
                    intQwmsQty = objStorageData.QueryMatnrQty(dtSave.Rows[i]["MATNR"].ToString(), dtSave.Rows[i]["INSMK"].ToString(), dtSave.Rows[i]["CHARG"].ToString());
                    if (intQwmsQty != 0)
                    {
                        dtSave.Rows[i]["QWQTY"] = intQwmsQty;
                    }
                    else
                    {
                        stsWarning.Text = "QWMS庫存數量為0，請確認!!";
                        return;
                    }

                    if (int.Parse(dtSave.Rows[i]["MdQty"].ToString()) > int.Parse(dtSave.Rows[i]["QWQTY"].ToString()))
                    {
                        stsWarning.Text = "加扣的數量大於QWMS目前的庫存數量，請確認!!";
                        return;
                    }
                }

                dtReturn = objStorageOut.AddAdmin_DocumentAddQty(dtSave);
                if (dtReturn.Rows.Count > 0)
                {
                    this.btnSave.Enabled = false;

                    //秀出新產生的單據號碼
                    try
                    {
                        stsWarning.Text = "";

                        Admin_DocumentAddQty_Show objAdmin_DocumentAddQty_Show = new Admin_DocumentAddQty_Show(UserData, Progid, Werks, Lgort, dtReturn);
                        objAdmin_DocumentAddQty_Show.MdiParent = this.ParentForm;
                        objAdmin_DocumentAddQty_Show.Show();

                        stsWarning.Text = "Update OK!!";
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
                else
                {
                    stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
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

        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        private void dtpCrdat_ValueChanged(object sender, EventArgs e)
        {
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();

            QCI.QWMS.StorageData objStorage = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            dtPrint = objStorage.QuerySimulationDocData(dtReturn);

            ReportPrint objReportPrint = new ReportPrint(UserData, "SMTData", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
    }

}
