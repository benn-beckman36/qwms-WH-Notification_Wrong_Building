using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using QCI.QWMS;
using QWMS.Common;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageOut_QMS_Dispatch : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strGrpid = "";
        private string strCrdat = "";
        private string strProgid = "";
        private DataTable dtIdData = new DataTable();
        private DataTable dtDetail = new DataTable();
        private DataTable dtSmtData = new DataTable();
        private DataTable dtSmtReturn = new DataTable();
        private DataTable dtCombineData = new DataTable();

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

        public string Grpid
        {
            get
            {
                return strGrpid;
            }
            set
            {
                strGrpid = value;
            }
        }

        #endregion

        public StorageOut_QMS_Dispatch(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);

                //檢查權限
                if (!StorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }

                    Werks = cmbWerks.SelectedItem.ToString();
                    ShowGroupId();
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

        #region ShowGroupId
        private void ShowGroupId()
        {
            try
            {
                stsWarning.Text = "";
                cmbGrpid.Items.Clear();
                strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                //查詢已扣過帳的id
                dtIdData = objSapData.QueryGroupId(strCrdat);
                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                    }

                    this.cmbGrpid.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "No id data!!";
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

        #region ShowDetailDataGrid
        public void ShowDetailDataGrid()
        {
            this.dgvDetail.AutoGenerateColumns = false;
            this.dgvDetail.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 60;
                dgvcLgort.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.Width = 140;
                dgvcGrpid.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcGrpid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 120;
                dgvcMblnr.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 60;
                dgvcZeile.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage Out Qty";
                dgvcMenge.Width = 60;
                dgvcMenge.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDidno = new DataGridViewTextBoxColumn();
                dgvcDidno.DataPropertyName = "DIDNO";
                dgvcDidno.HeaderText = "DID No.";
                dgvcDidno.Width = 170;
                dgvcDidno.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcDidno);

                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "COSCT";
                dgvcCosct.HeaderText = "Cost Center";
                dgvcCosct.Width = 60;
                dgvcCosct.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcCosct);

                DataGridViewTextBoxColumn dgvcStype = new DataGridViewTextBoxColumn();
                dgvcStype.DataPropertyName = "STYPE";
                dgvcStype.HeaderText = "TYPE";
                dgvcStype.Width = 90;
                dgvcStype.ReadOnly = true;
                this.dgvDetail.Columns.Add(dgvcStype);

                dgvDetail.DataSource = dtDetail;
                lblDetail.Text = dtDetail.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDetailDataGrid()");
            }
        }
        #endregion

        #region ShowSummaryDataGrid
        public void ShowSummaryDataGrid()
        {
            this.dgvSummary.AutoGenerateColumns = false;
            this.dgvSummary.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.Width = 140;
                dgvcGrpid.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcGrpid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "QWMS Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcQmsqty = new DataGridViewTextBoxColumn();
                dgvcQmsqty.DataPropertyName = "QMSQTY";
                dgvcQmsqty.HeaderText = "QMS Dispatch Qty";
                dgvcQmsqty.Width = 90;
                dgvcQmsqty.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcQmsqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "COSCT";
                dgvcCosct.HeaderText = "Cost Center";
                dgvcCosct.Width = 60;
                dgvcCosct.ReadOnly = true;
                this.dgvSummary.Columns.Add(dgvcCosct);

                dgvSummary.DataSource = dtCombineData;
                lblSummary.Text = dtCombineData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSummaryDataGrid()");
            }
        }
        #endregion

        #region dtpCrdat_ValueChanged
        private void dtpCrdat_ValueChanged(object sender, EventArgs e)
        {
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期
            ShowGroupId();
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            else
            {
                strWerks = "";
            }

            if (cmbGrpid.Items.Count > 0)
            {
                strGrpid = cmbGrpid.SelectedItem.ToString();
            }

            //廠區不能為空
            if (Werks == "")
            {
                stsWarning.Text = "廠區不能為空!!";
                return;
            }
            //單號不為空
            if (strGrpid == "")
            {
                stsWarning.Text = "請先選擇Send id!!";
                return;
            }

            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            dtDetail = objStorageData.QueryQdpAllData(strGrpid);
            if (dtDetail.Rows.Count > 0)
            {
                dtDetail = CommonInfo.SortDataTable(dtDetail, "GRPID, MATNR, MBLNR, STYPE");
                ShowDetailDataGrid();
                this.btnQuery.Enabled = true;
                this.btnSave.Enabled = true;
            }
            else
            {
                stsWarning.Text = "查無ID出庫、加扣和SMT退庫的資料，請確認!!";
                return;
            }
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
				DataRow[] combineRow;
                DataRow[] findReturnRow;
                DataRow[] findQmsRow;
				DataRow drRow;
                string strOrderBy = "Grpid, Cosct, Matnr";
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataTable dtQms = new DataTable();

                dtSmtReturn = dtDetail.Clone();
                dtSmtData = dtDetail.Clone();

                #region 先將SMT退庫的資料存到另一個DataTable(dtSmtReturn)
                for (int i = 0; i < dtDetail.Rows.Count; i++)
                {
                    if (dtDetail.Rows[i]["STYPE"].ToString() == "RETURN")
                    {
                        dtSmtReturn.ImportRow(dtDetail.Rows[i]);
                    }
                    else
                    {
                        dtSmtData.ImportRow(dtDetail.Rows[i]);
                    }
                }
                #endregion

                #region 將同Group id同料號的資料加總(dtSmtData)
                int intCombineQwmsQty = 0;
                int intCombineQmsQty = 0;
                dtCombineData = dtSmtData.Clone();
                dtCombineData.Columns.Add("QMSQTY");
                dtCombineData.Columns.Add("BLACE");
                for (int i = 0; i < dtSmtData.Rows.Count; i++)
				{   
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtSmtData.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtSmtData.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtSmtData.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtSmtData.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and GRPID='" + dtSmtData.Rows[i]["GRPID"].ToString() + "'");
                    sbCombineIndex.Append(" and COSCT='" + dtSmtData.Rows[i]["COSCT"].ToString() + "'");

                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
					{
                        intCombineQwmsQty = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtSmtData.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineQwmsQty += Int32.Parse(combineRow[j]["MENGE"].ToString());
                        }

                        drRow = dtCombineData.NewRow();
                        drRow["MANDT"] = dtSmtData.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtSmtData.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtSmtData.Rows[i]["WERKS"].ToString();
                        drRow["GRPID"] = dtSmtData.Rows[i]["GRPID"].ToString();
                        drRow["MATNR"] = dtSmtData.Rows[i]["MATNR"].ToString();
                        drRow["MENGE"] = intCombineQwmsQty.ToString();
                        drRow["QMSQTY"] = intCombineQmsQty.ToString();
                        drRow["BLACE"] = Convert.ToString(intCombineQwmsQty - intCombineQmsQty);
                        drRow["COSCT"] = dtSmtData.Rows[i]["COSCT"].ToString();
                        dtCombineData.Rows.Add(drRow);
                    }
                }
                #endregion

                #region 查詢QMS Dispatch的資料(WHCPA)
                StorageData objStorageData = new StorageData(UserData,Werks,Lgort);
                dtQms = objStorageData.QueryCpaData(strGrpid);
                #endregion

                for (int i = 0; i < dtCombineData.Rows.Count; i++)
                {
                    findQmsRow = dtQms.Select("MATNR='" + dtCombineData.Rows[i]["MATNR"].ToString() + "'");
                    if (findQmsRow.Length > 0)
                    {
                        //取得QMS DisPatch的數量
                        dtCombineData.Rows[i]["QMSQTY"] = findQmsRow[0]["MENGE"].ToString();
                    }

                    findReturnRow = dtSmtReturn.Select("MATNR='" + dtCombineData.Rows[i]["MATNR"].ToString() + "'");
                    if (findReturnRow.Length > 0)
                    {
                        //扣掉相同料號的SMT退庫數量
                        dtCombineData.Rows[i]["MENGE"] = int.Parse(dtCombineData.Rows[i]["MENGE"].ToString()) - int.Parse(findReturnRow[0]["MENGE"].ToString());
                        dtCombineData.Rows[i]["BLACE"] = int.Parse(dtCombineData.Rows[i]["BLACE"].ToString()) - int.Parse(findReturnRow[0]["MENGE"].ToString());
                    }
                }

                dtCombineData = CommonInfo.SortDataTable(dtCombineData, strOrderBy);
            	ShowSummaryDataGrid();
            }
            catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                //先檢查Balance的數量是否為0
                for (int i = 0; i < dtCombineData.Rows.Count; i++)
                {
                    if (int.Parse(dtCombineData.Rows[i]["BLACE"].ToString()) != 0)
                    {
                        stsWarning.Text = "QWMS彙總之數量與QMS Dispatch之數量不相符，請確認!!";
                        return;
                    }
                }

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                if (objStorageOut.UpdateDocStatus(dtSmtData))
                {
                    stsWarning.Text = "更改單據的狀態成功，已產生檔案傳至SAP扣帳!!";
                    btnQuery.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "更改單據的狀態失敗，請確認!!";
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.dtIdData.Clear();
            this.cmbGrpid.Items.Clear();
            this.lblDetail.Text = "0 records";
            this.lblSummary.Text = "0 records";
            this.dgvDetail.DataSource = null;
            this.dgvSummary.DataSource = null;
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
