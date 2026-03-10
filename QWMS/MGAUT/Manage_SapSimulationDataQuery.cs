using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using System.Collections;

namespace QWMS
{
    public partial class Manage_SapSimulationDataQuery : Form
    {
        public Manage_SapSimulationDataQuery()
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
        private string strGrpid = "";
        private string strProgid = "";
        private string strIdCrdat = "";
        private string strDocCrdat = "";
        private FileInfo fi;
        private StreamWriter sw;
        private StorageIn objStorageIn;
        private DataTable dtIdData = new DataTable();
        private DataTable dtData = new DataTable();
        private DataTable dtSmt = new DataTable();
        private DataTable dtFin = new DataTable();
        private DataTable dtTempStorage = new DataTable();
        private DataTable dtPrint = new DataTable();
        private LogData objLogData;
        private string strOrderBy = "";

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

        public Manage_SapSimulationDataQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            strComcd = varUserData.CompanyCode;

            try
            {
                objStorageIn = new StorageIn(UserData, Progid);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
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
                        Werks = cmbWerks.Text.Trim();
                    }
                    cmbGrpid.SelectedIndex = -1;
                    cmbLgort.SelectedIndex = -1;
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

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Mat Doc.";
                dgvcMblnr.Width = 110;
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
                dgvcMatnr.HeaderText = "Mat No.";
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
                dgvcCharg.HeaderText = "Batch";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "SLoc";
                dgvcUmlgo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcUmlgo);

                DataGridViewTextBoxColumn dgvcIntid = new DataGridViewTextBoxColumn();
                dgvcIntid.DataPropertyName = "INTID";
                dgvcIntid.HeaderText = "RefDoc No.";
                dgvcIntid.ReadOnly = true;
                dgvcIntid.Width = 140;
                this.dgvData.Columns.Add(dgvcIntid);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Doc Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOtqty = new DataGridViewTextBoxColumn();
                dgvcOtqty.DataPropertyName = "OTQTY";
                dgvcOtqty.HeaderText = "Storage Out Qty";
                dgvcOtqty.Width = 90;
                dgvcOtqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOtqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcWkord = new DataGridViewTextBoxColumn();
                dgvcWkord.DataPropertyName = "WKORD";
                dgvcWkord.HeaderText = "Work Order";
                dgvcWkord.Width = 90;
                dgvcWkord.ReadOnly = true;
                dgvData.Columns.Add(dgvcWkord);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Cost Center";
                dgvcKostl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.Width = 50;
                dgvcTrntp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcBwart = new DataGridViewTextBoxColumn();
                dgvcBwart.DataPropertyName = "BWART";
                dgvcBwart.HeaderText = "Mvt.";
                dgvcBwart.Width = 50;
                dgvcBwart.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBwart);

                DataGridViewTextBoxColumn dgvcFlage = new DataGridViewTextBoxColumn();
                dgvcFlage.DataPropertyName = "FLAGE";
                dgvcFlage.HeaderText = "Transmit Files(Y/N)";
                dgvcFlage.Width = 100;
                dgvcFlage.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcFlage);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region ShowPrintDataGrid
        public void ShowPrintDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 60;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 60;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.ReadOnly = true;
                dgvcGrpid.Width = 140;
                this.dgvData.Columns.Add(dgvcGrpid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Doc No.";
                dgvcMblnr.Width = 110;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No.";
                dgvcMatnr.Width = 110;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = chkDateCode.Checked ? "LOCAT" : "OLOCA";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Batch";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = chkDateCode.Checked ? "BKQTY" : "QWQTY";
                dgvcMenge.HeaderText = "Storage Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOtqty = new DataGridViewTextBoxColumn();
                dgvcOtqty.DataPropertyName = chkDateCode.Checked ? "ALQTY" : "MENGE";
                dgvcOtqty.HeaderText = "Storage Out Qty";
                dgvcOtqty.Width = 90;
                dgvcOtqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOtqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Cost Center";
                dgvcKostl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcArbpl);

                dgvData.DataSource = dtPrint;
                lblData.Text = dtPrint.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowPrintDataGrid()");
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strLgort = "";
            this.stsWarning.Text = "";
            this.txtMblnr.Text = "";
            this.txtDoc.Text = "";
            this.cmbGrpid.Items.Clear();
            this.chkShortage.Checked = false;
            this.checkStock.Checked = false;
            this.gbDoc.Enabled = false;
            this.gbId.Enabled = false;
            this.lblData.Text = "0 records";
            this.dgvData.DataSource = null;
            this.btnPrint.Enabled = false;
            this.btnReport.Enabled = false;
            this.btnDownload.Enabled = false;
            this.btnDocDownload.Enabled = false;
            this.btnQuery.Enabled = false;
            this.btnReprint.Enabled = false;
            this.rdoMatnr.Checked = false;
            this.rdoLocat.Checked = false;
            this.cmbLgort.SelectedIndex = -1;
            this.cmbTimeFrom.SelectedIndex = -1;
            this.cmbTimeTo.SelectedIndex = -1;
            this.cmbWerks.Focus();
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnQueryDoc_Click
        private void btnQueryDoc_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            Mblnr = txtMblnr.Text.Trim();
            Grpid = cmbGrpid.Text.Trim();
            string strTimeFrom = "";
            string strTimeTo = "";
            strDocCrdat = dtpDocCrdat.Value.ToString("yyyy-MM-dd");

            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }

            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }

            if (cmbTimeFrom.SelectedIndex != -1)
            {
                if (cmbTimeTo.SelectedIndex == -1)
                {
                    stsWarning.Text = "請選擇結束時間!!";
                    return;
                }
                strTimeFrom = cmbTimeFrom.SelectedItem.ToString();
                strTimeTo = cmbTimeTo.SelectedItem.ToString();
            }

            //單據號碼不能為空
            if (Mblnr == "")
            {
                stsWarning.Text = "請輸入單據號碼或*";
                return;
            }

            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            dtData = objStorageData.QuerySimulationDocList(Mblnr, strDocCrdat, strTimeFrom, strTimeTo);

            if (dtData.Rows.Count > 0)
            {
                DataTable dtTmp = new DataTable();
                dtData.Columns.Add("BLACE");
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["BLACE"] = int.Parse(dtData.Rows[i]["MENGE"].ToString()) - int.Parse(dtData.Rows[i]["OTQTY"].ToString());
                }

                //只顯示發料數量不足的資料列
                if (checkStock.Checked == true)
                {

                    dtTmp = dtData.Clone();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["BLACE"].ToString()) != 0)
                        {
                            dtTmp.ImportRow(dtData.Rows[i]);
                        }
                    }
                    dtData.Clear();
                    dtData = dtTmp.Copy();
                    if (dtData.Rows.Count > 0)
                        btnDocDownload.Enabled = true;
                }

                ShowDataGrid();
                this.btnPrint.Enabled = true;
            }
            else
            {
                stsWarning.Text = "查無單據資料，請確認!!";
                return;
            }
        }
        #endregion

        #region dtpIdCrdat_ValueChanged
        private void dtpIdCrdat_ValueChanged(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            strIdCrdat = dtpIdCrdat.Value.ToString("yyyy-MM-dd");

            ShowGroupId(); //Show Group id
        }
        #endregion

        #region btnPrint_Click
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "SIMUDOCLIST", dtData);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        #region dgvData_RowHeaderMouseClick
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (btnQueryDoc.Enabled = true && dtData.Rows.Count > 0)
            {
                string strMblnr = "";
                string strInsmk = "";
                string strCharg = "";
                string strBwart = "";
                string strMtype = "";
                string strIntid = "";
                DataRow[] findRow;
                DataTable dtSelect = new DataTable();
                DataTable dtPrint = new DataTable();
                ArrayList alAllSearch = new ArrayList();
                StringBuilder sbSearchIndex = new StringBuilder();

                DataRow[] combineRow;
                DataRow drRow;
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataTable dtCombinePrint = new DataTable();
                int intCombineMenge = 0;

                dtSelect = dtData.Clone();

                //記錄所選取的某一列單據編號、庫別、版本等資訊
                if (dtData.Rows.Count > 0)
                {
                    strMblnr = dtData.Rows[dgvData.CurrentRow.Index]["MBLNR"].ToString();
                    strInsmk = dtData.Rows[dgvData.CurrentRow.Index]["INSMK"].ToString();
                    strCharg = dtData.Rows[dgvData.CurrentRow.Index]["CHARG"].ToString();
                    strBwart = dtData.Rows[dgvData.CurrentRow.Index]["BWART"].ToString();
                    strMtype = dtData.Rows[dgvData.CurrentRow.Index]["MTYPE"].ToString();
                    strIntid = dtData.Rows[dgvData.CurrentRow.Index]["INTID"].ToString();
                }


                #region  //每次比對的Index(sbSearchIndex)  Smose Liao 20100308
                sbSearchIndex.Remove(0, sbSearchIndex.Length);

                sbSearchIndex.Append("MBLNR='" + strMblnr + "'");
                sbSearchIndex.Append(" and INSMK='" + strInsmk + "'");
                //combine暫時不考慮版本，半成品會有版本的問題
                //sbSearchIndex.Append(" and CHARG='" + strCharg + "'");
                sbSearchIndex.Append(" and BWART='" + strBwart + "'");
                sbSearchIndex.Append(" and MTYPE='" + strMtype + "'");
                sbSearchIndex.Append(" and INTID='" + strIntid + "'");

                #endregion

                if (alAllSearch.IndexOf(sbSearchIndex.ToString()) < 0)
                {
                    alAllSearch.Add(sbSearchIndex.ToString());
                    findRow = dtData.Select(sbSearchIndex.ToString());

                    //將比對到的combineRow依序存入dtPrint
                    for (int j = 0; j < findRow.Length; j++)
                    {
                        dtSelect.ImportRow(findRow[j]);
                    }

                }

                //透過WHDWN與WHPAT做INNER JOIN取得料號描述(WHPAT.MAKTX)
                QCI.QWMS.StorageData objStorage = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtPrint = objStorage.QuerySimulationDocData(dtSelect);

                #region 報表的資料將FINAL的資料By料號、線別加總  Smose Liao  20100907

                if (strBwart == "261")  //FINAL
                {
                    dtCombinePrint = dtPrint.Clone();
                    for (int i = 0; i < dtPrint.Rows.Count; i++)
                    {
                        #region 每次比對的Index (sbCombineIndex)
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtPrint.Rows[i]["MANDT"].ToString() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtPrint.Rows[i]["COMCD"].ToString() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtPrint.Rows[i]["WERKS"].ToString() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtPrint.Rows[i]["LGORT"].ToString() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtPrint.Rows[i]["MATNR"].ToString() + "'");
                        sbCombineIndex.Append(" and MBLNR='" + dtPrint.Rows[i]["MBLNR"].ToString() + "'");
                        sbCombineIndex.Append(" and INTID='" + dtPrint.Rows[i]["INTID"].ToString() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtPrint.Rows[i]["CHARG"].ToString() + "'");
                        sbCombineIndex.Append(" and ARBPL='" + dtPrint.Rows[i]["ARBPL"].ToString() + "'");
                        #endregion

                        if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                        {
                            intCombineMenge = 0;
                            alAllCombine.Add(sbCombineIndex.ToString());

                            combineRow = dtPrint.Select(sbCombineIndex.ToString());
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineMenge += Int32.Parse(combineRow[j]["MENGE"].ToString());
                            }

                            drRow = dtCombinePrint.NewRow();
                            drRow["MANDT"] = dtPrint.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtPrint.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtPrint.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtPrint.Rows[i]["LGORT"].ToString();
                            drRow["MATNR"] = dtPrint.Rows[i]["MATNR"].ToString();
                            drRow["CHARG"] = dtPrint.Rows[i]["CHARG"].ToString();
                            drRow["INTID"] = dtPrint.Rows[i]["INTID"].ToString();
                            drRow["FMATN"] = dtPrint.Rows[i]["FMATN"].ToString();
                            drRow["MENGE"] = intCombineMenge.ToString();
                            drRow["QWQTY"] = dtPrint.Rows[i]["QWQTY"].ToString();
                            drRow["MBLNR"] = dtPrint.Rows[i]["MBLNR"].ToString();
                            drRow["MAKTX"] = dtPrint.Rows[i]["MAKTX"].ToString();
                            drRow["MTYPE"] = dtPrint.Rows[i]["MTYPE"].ToString();
                            drRow["BWART"] = dtPrint.Rows[i]["BWART"].ToString();
                            drRow["ARBPL"] = dtPrint.Rows[i]["ARBPL"].ToString();
                            drRow["KOSTL"] = dtPrint.Rows[i]["KOSTL"].ToString();
                            drRow["TRNTP"] = dtPrint.Rows[i]["TRNTP"].ToString();

                            dtCombinePrint.Rows.Add(drRow);
                        }
                    }
                }

                #endregion

                if (strBwart == "311")  //SMT
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "SMTData", dtPrint);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                else if (strBwart == "261")  //FINAL
                {
                    //ReportPrint objReportPrint = new ReportPrint(UserData, "FINData", dtPrint);
                    ReportPrint objReportPrint = new ReportPrint(UserData, "FINData", dtCombinePrint);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
            }
        }
        #endregion

        #region btnQueryId_Click
        private void btnQueryId_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = new DataTable();

            stsWarning.Text = "";
            Mblnr = txtMblnr.Text.Trim();
            Grpid = cmbGrpid.Text.Trim();

            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }

            //Group id不能為空
            if (Grpid == "")
            {
                stsWarning.Text = "Send/Group id can't be empty!!";
                return;
            }

            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            if (cmbType.Text == "SMT")
            {
                dtSmt = objStorageData.QuerySendGroupId(Grpid, strIdCrdat, cmbType.Text.ToString());
            }
            else if (cmbType.Text == "FINAL")
            {
                dtFin = objStorageData.QueryGroupIdFinalData(Grpid, cmbType.Text.ToString());
            }

            if (dtSmt.Rows.Count > 0)
            {
                dtFin.Clear();
                dtSmt.Columns.Add("BLACE");
                for (int i = 0; i < dtSmt.Rows.Count; i++)
                {
                    dtSmt.Rows[i]["BLACE"] = int.Parse(dtSmt.Rows[i]["MENGE"].ToString()) - int.Parse(dtSmt.Rows[i]["TLQTY"].ToString());
                }

                //只顯示有缺料的資料列
                if (chkShortage.Checked == true)
                {
                    dtTemp = dtSmt.Clone();
                    for (int i = 0; i < dtSmt.Rows.Count; i++)
                    {
                        if (int.Parse(dtSmt.Rows[i]["BLACE"].ToString()) < 0)
                        {
                            dtTemp.ImportRow(dtSmt.Rows[i]);
                        }
                    }
                    dtSmt.Clear();
                    dtSmt = dtTemp.Copy();
                }

                dtSmt = CommonInfo.SortDataTable(dtSmt, "MATNR");

                ShowSmtDataGrid();
                this.checkStock.Enabled = false;
                this.txtMblnr.Enabled = false;
                this.dtpDocCrdat.Enabled = false;
                this.btnQueryDoc.Enabled = false;
                this.btnReport.Enabled = true;
                this.btnDownload.Enabled = true;
            }
            else if (dtFin.Rows.Count > 0)
            {
                dtSmt.Clear();
                //只顯示有缺料的資料列
                if (chkShortage.Checked == true)
                {
                    dtTemp = dtFin.Clone();
                    for (int i = 0; i < dtFin.Rows.Count; i++)
                    {
                        dtFin.Rows[i]["BLACE"] = int.Parse(dtFin.Rows[i]["ALQTY"].ToString()) - int.Parse(dtFin.Rows[i]["MENGE"].ToString());
                        if (int.Parse(dtFin.Rows[i]["BLACE"].ToString()) < 0)
                        {
                            dtTemp.ImportRow(dtFin.Rows[i]);
                        }
                    }
                    dtFin.Clear();
                    dtFin = dtTemp.Copy();
                }

                dtFin = CommonInfo.SortDataTable(dtFin, "MATNR");

                ShowFinDataGrid();
                this.txtMblnr.Enabled = false;
                this.dtpDocCrdat.Enabled = false;
                this.btnQueryDoc.Enabled = false;
                this.btnReport.Enabled = true;
                this.btnDownload.Enabled = true;
            }
            else
            {
                dtSmt.Clear();
                dtFin.Clear();
                dgvData.DataSource = null;
                stsWarning.Text = "No Data!!";
            }
        }
        #endregion

        #region ShowSmtDataGrid
        public void ShowSmtDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group/Send id";
                dgvcGrpid.ReadOnly = true;
                dgvcGrpid.Width = 140;
                this.dgvData.Columns.Add(dgvcGrpid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Simulation Doc";
                dgvcMblnr.Width = 110;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No.";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcTlqty = new DataGridViewTextBoxColumn();
                dgvcTlqty.DataPropertyName = "TLQTY";
                dgvcTlqty.HeaderText = "Total Request Qty";
                dgvcTlqty.Width = 90;
                dgvcTlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTlqty);

                DataGridViewTextBoxColumn dgvcRlqty = new DataGridViewTextBoxColumn();
                dgvcRlqty.DataPropertyName = "RLQTY";
                dgvcRlqty.HeaderText = "Reel Qty";
                dgvcRlqty.Width = 90;
                dgvcRlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRlqty);

                DataGridViewTextBoxColumn dgvcRoval = new DataGridViewTextBoxColumn();
                dgvcRoval.DataPropertyName = "ROVAL";
                dgvcRoval.HeaderText = "Rounding Value";
                dgvcRoval.ReadOnly = true;
                dgvcRoval.Width = 90;
                this.dgvData.Columns.Add(dgvcRoval);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Issued Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcWodat = new DataGridViewTextBoxColumn();
                dgvcWodat.DataPropertyName = "WODAT";
                dgvcWodat.HeaderText = "Work Date";
                dgvcWodat.ReadOnly = true;
                dgvcWodat.Width = 90;
                this.dgvData.Columns.Add(dgvcWodat);

                DataGridViewTextBoxColumn dgvcTrdat = new DataGridViewTextBoxColumn();
                dgvcTrdat.DataPropertyName = "TRDAT";
                dgvcTrdat.HeaderText = "Trans Date Time";
                dgvcTrdat.ReadOnly = true;
                dgvcTrdat.Width = 90;
                this.dgvData.Columns.Add(dgvcTrdat);

                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "COSCT";
                dgvcCosct.HeaderText = "Cost Center";
                dgvcCosct.ReadOnly = true;
                dgvcCosct.Width = 110;
                this.dgvData.Columns.Add(dgvcCosct);

                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "SLoc Storage";
                dgvcUmlgo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcUmlgo);

                dgvData.DataSource = dtSmt;
                lblData.Text = dtSmt.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        #region ShowFinDataGrid
        public void ShowFinDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group/Send ID";
                dgvcGrpid.Width = 140;
                dgvcGrpid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcGrpid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Simulation Doc";
                dgvcMblnr.Width = 110;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcFmatn = new DataGridViewTextBoxColumn();
                dgvcFmatn.DataPropertyName = "FMATN";
                dgvcFmatn.HeaderText = "Father material";
                dgvcFmatn.Width = 90;
                dgvcFmatn.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcFmatn);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No.";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "QMS Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOutQty = new DataGridViewTextBoxColumn();
                dgvcOutQty.DataPropertyName = "ALQTY";
                dgvcOutQty.HeaderText = "Storage Out Qty";
                dgvcOutQty.Width = 90;
                dgvcOutQty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOutQty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Batch";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcWkord = new DataGridViewTextBoxColumn();
                dgvcWkord.DataPropertyName = "WKORD";
                dgvcWkord.HeaderText = "Work Order";
                dgvcWkord.ReadOnly = true;
                dgvcWkord.Width = 100;
                this.dgvData.Columns.Add(dgvcWkord);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcArbpl);

                dgvData.DataSource = dtFin;
                lblData.Text = dtFin.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowFinDataGrid()");
            }
        }
        #endregion

        #region cmbType_SelectedIndexChanged
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = "";
            }

            if (strLgort != "")
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                gbDoc.Enabled = true;
            }
            else
            {
                gbId.Enabled = true;
                strIdCrdat = dtpIdCrdat.Value.ToString("yyyy-MM-dd");
                ShowGroupId();
            }
        }
        #endregion

        #region ShowGroupId
        private void ShowGroupId()
        {
            try
            {
                this.stsWarning.Text = "";
                this.cmbGrpid.Items.Clear();
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (cmbType.Text == "SMT")
                {
                    dtIdData = objSapData.QueryGroupIdData(strIdCrdat, "SMT", false);
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
                        stsWarning.Text = "查無id資料，請確認廠區與日期是否正確!!";
                        return;
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    dtIdData = objSapData.QueryGroupIdData(strIdCrdat, "FINAL", false);
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
                        stsWarning.Text = "No id data，請確認廠區與日期是否正確!!";
                        return;
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

        #region btnReport
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cmbType.Text == "SMT")
            {
                dtSmt.Columns.Add("LGORT");
                ReportPrint objReportPrint = new ReportPrint(UserData, "SMTGROUPIDREPORT", dtSmt);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            else if (cmbType.Text == "FINAL")
            {
                dtFin.Columns.Add("LGORT");
                ReportPrint objReportPrint = new ReportPrint(UserData, "FINALGROUPIDREPORT", dtFin);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
        }
        #endregion

        #region btnDownload
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
        #endregion

        # region Export
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                if (cmbType.Text == "SMT")
                {
                    fi = new FileInfo(strFilePath);
                    sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                    strLine = "Plant\tGroup id\tDocument No\tPart No\tReel Qty\tRounding Value\tTotal Request Qty\tStorage Out Qty\tBalance Qty\tWork Date\tTrans Date Time\tCost Center\tSLoc Storage\t";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtSmt.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtSmt.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["GRPID"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["RLQTY"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["ROVAL"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["TLQTY"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["BLACE"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["WODAT"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["TRDAT"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["COSCT"].ToString() + "\t";
                        strLine += dtSmt.Rows[i]["UMLGO"].ToString();
                        sw.WriteLine(strLine);
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    fi = new FileInfo(strFilePath);
                    sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                    strLine = "Plant\tGroup id\tDocument No\tFather P/N\tPart No\tQMS Qty\tStore Out Qty\tBalance Qty\tVersion\tWork Order\tPD Line\t";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtFin.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtFin.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["GRPID"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["FMATN"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["ALQTY"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["BLACE"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["WKORD"].ToString() + "\t";
                        strLine += dtFin.Rows[i]["ARBPL"].ToString();
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

        # region Document Export
        private void CountingDocument2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Storage\tGroup id\tDocument No\tDocument Item\tPart No\tDoc Qty\tStore Out Qty\tBalance Qty\tVersion\tWork Order\tCost Center\tPD Line\t";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["INTID"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["OTQTY"].ToString() + "\t";
                    strLine += dtData.Rows[i]["BLACE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtData.Rows[i]["WKORD"].ToString() + "\t";
                    strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ARBPL"].ToString();
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingDocument2File()");
            }
            finally
            {
                sw.Close();
            }
        }
        # endregion

        #region cmbGrpid_SelectedIndexChanged
        private void cmbGrpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGrpid.Text != "")
            {
                btnQueryId.Enabled = true;
                chkShortage.Enabled = true;
                btnQuery.Enabled = true;
                txtDoc.Enabled = false;
            }
        }
        #endregion

        #region dtpDocCrdat_ValueChanged
        private void dtpDocCrdat_ValueChanged(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            strDocCrdat = dtpDocCrdat.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        #region btnDocDownload_Click
        private void btnDocDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile1.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile1.FileName;
                    CountingDocument2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region cmbTimeFrom_SelectedIndexChanged
        private void cmbTimeFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTimeTo.Items.Clear();
            int intSelectIndex;
            intSelectIndex = int.Parse(cmbTimeFrom.SelectedIndex.ToString());
            for (int i = intSelectIndex + 1; i < cmbTimeFrom.Items.Count; i++)
            {
                cmbTimeTo.Items.Add(cmbTimeFrom.Items[i].ToString());
            }
            cmbTimeTo.Items.Add("23:00");
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

        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            dtPrint.Clear();
            stsWarning.Text = "";
            objLogData = new LogData(UserData, strWerks, strLgort, Progid);
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            else
            {
                stsWarning.Text = "請先選擇倉別!!";
                return;
            }

            if (rdoMatnr.Checked)
            {
                //依料號排序
                strOrderBy = chkDateCode.Checked ? "MATNR,LOCAT,MBLNR" : "MATNR,OLOCA,MBLNR";
            }
            else if (rdoLocat.Checked)
            {
                //依儲位排序
                strOrderBy = chkDateCode.Checked ? "LOCAT,MATNR,MBLNR" : "OLOCA,MATNR,MBLNR";
            }
            else
            {
                stsWarning.Text = "請先選擇排序方式!!";
                return;
            }
            #region DateCode 查询
            if (cmbGrpid.SelectedIndex != -1 && chkDateCode.Checked)
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                DataTable dtData = objStorageData.QuerySimulationGrrno_DateCode(cmbGrpid.SelectedItem.ToString(), strWerks, strLgort);

                dtPrint = dtData.Clone();

                DataRow drNew;
                DataRow[] drArray;

                drArray = dtData.Select("", "ITEMNUM");
                foreach (DataRow dr in drArray)
                {
                    drNew = dtPrint.NewRow();

                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        drNew[i] = dr[i].ToString();
                    }
                    dtPrint.Rows.Add(drNew);
                }

                dtPrint = CommonInfo.SortDataTable(dtPrint, strOrderBy);
                ShowPrintDataGrid();
                this.cmbGrpid.Enabled = false;
                this.btnReprint.Enabled = false;
                this.btnDateCode.Enabled = true;
                this.btnSummary.Enabled = true;
                return;
            }
            #endregion

            #region 非DateCode查询
            if (cmbGrpid.SelectedIndex != -1)
            {
                dtTempStorage = objLogData.QueryLogDataForReprint(cmbGrpid.SelectedItem.ToString(), "");
            }
            else
            {
                dtTempStorage = objLogData.QueryLogDataForReprint("", txtDoc.Text.Trim());
            }

            if (dtTempStorage.Rows.Count == 0)
            {
                stsWarning.Text = "查不到可供補印的記錄，請確認Group id和扣帳單號是否正確!!";
                return;
            }
            else
            {
                DataRow drRow;
                DataRow[] combineRow;
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                int intCombineLocalTotal = 0;
                int intCombineLocatOut = 0;

                //將同儲位同料號的資料加總
                dtPrint = dtTempStorage.Clone();
                dtPrint.Columns.Add("BLACE");
                for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                    sbCombineIndex.Append(" and OLOCA='" + dtTempStorage.Rows[i]["OLOCA"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");

                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                    {
                        intCombineLocatOut = 0;
                        intCombineLocalTotal = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtTempStorage.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineLocatOut += Int32.Parse(combineRow[j]["MENGE"].ToString());
                        }

                        //若有兩筆同料號同儲位的資料，只需找到第一筆記錄的庫存數量即可
                        intCombineLocalTotal = Int32.Parse(combineRow[0]["QWQTY"].ToString());

                        drRow = dtPrint.NewRow();
                        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                        drRow["OLOCA"] = dtTempStorage.Rows[i]["OLOCA"].ToString();
                        drRow["GRPID"] = dtTempStorage.Rows[i]["GRPID"].ToString();
                        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        drRow["QWQTY"] = intCombineLocalTotal.ToString();          //總庫存數量
                        drRow["MENGE"] = intCombineLocatOut.ToString();            //總出庫數量
                        drRow["BLACE"] = intCombineLocalTotal + intCombineLocatOut;//差異數量
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["KOSTL"] = dtTempStorage.Rows[i]["KOSTL"].ToString();
                        drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();

                        dtPrint.Rows.Add(drRow);
                    }
                }

                dtPrint = CommonInfo.SortDataTable(dtPrint, strOrderBy);
                ShowPrintDataGrid();
                this.cmbGrpid.Enabled = false;
                this.btnReprint.Enabled = true;
                this.btnDateCode.Enabled = false;
                this.btnSummary.Enabled = false;
            }
            #endregion
        }
        #endregion

        #region btnReprint_Click
        private void btnReprint_Click(object sender, EventArgs e)
        {
            if (rdoMatnr.Checked)
            {
                //依料號排序
                strOrderBy = "MATNR,OLOCA,MBLNR";
            }
            else if (rdoLocat.Checked)
            {
                //依儲位排序
                strOrderBy = "OLOCA,MATNR,MBLNR";
            }
            else
            {
                stsWarning.Text = "請先選擇排序方式!!";
                return;
            }

            dtPrint = CommonInfo.SortDataTable(dtPrint, strOrderBy);

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_REPRINT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        #region txtDoc_KeyDown
        private void txtDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }

                if (Lgort == "")
                {
                    stsWarning.Text = "請先選擇倉別!!";
                    return;
                }

                btnQuery_Click(null, null);
            }
        }
        #endregion

        #region cmbLgort_SelectedIndexChanged
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                PlantData objPlantData = new PlantData(UserData);
                DataTable dtLgort = objPlantData.QueryLgortByProperty(strWerks, "", strLgort);
                chkDateCode.Checked = dtLgort.Rows.Count > 0 ? true : false;
            }
            else
            {
                strLgort = "";
            }
        }
        #endregion

        private void btnDateCode_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DATECODE_NEW", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            DataTable dtData = objStorageData.QuerySimulationGrrno_DateCode(cmbGrpid.SelectedItem.ToString(), strWerks, strLgort);
            dtPrint = dtData.Clone();

            StringBuilder sbCombinePrint = new StringBuilder();
            ArrayList alCombinePrint = new ArrayList();
            DataRow[] combineRow;
            DataRow drRow;

            int intCombineLocalTotal = 0;
            int intCombineLocatOut = 0;
            dtPrint = dtData.Clone();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                #region 每次比對的Index (sbCombinePrint)
                sbCombinePrint.Remove(0, sbCombinePrint.Length);
                sbCombinePrint.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                sbCombinePrint.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                sbCombinePrint.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                sbCombinePrint.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                sbCombinePrint.Append(" and LOCAT='" + dtData.Rows[i]["LOCAT"].ToString() + "'");
                sbCombinePrint.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                sbCombinePrint.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString() + "'");
                sbCombinePrint.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString() + "'");
                sbCombinePrint.Append(" and DACOD='" + dtData.Rows[i]["DACOD"].ToString() + "'");
                sbCombinePrint.Append(" and LOCOD='" + dtData.Rows[i]["LOCOD"].ToString() + "'");
                #endregion

                if (alCombinePrint.IndexOf(sbCombinePrint.ToString()) < 0)
                {
                    intCombineLocatOut = 0;
                    intCombineLocalTotal = 0;
                    alCombinePrint.Add(sbCombinePrint.ToString());

                    combineRow = dtData.Select(sbCombinePrint.ToString());
                    for (int j = 0; j < combineRow.Length; j++)
                    {
                        intCombineLocalTotal = Int32.Parse(combineRow[j]["BKQTY"].ToString());  //庫存總數量
                        intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());  //出庫數量
                    }

                    drRow = dtPrint.NewRow();
                    drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                    drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                    drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                    drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                    drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                    drRow["GRRNO"] = dtData.Rows[i]["GRRNO"].ToString();
                    drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                    drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                    drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                    drRow["BKQTY"] = intCombineLocalTotal.ToString();
                    drRow["ALQTY"] = intCombineLocatOut.ToString();
                    drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                    drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                    drRow["DACOD"] = dtData.Rows[i]["DACOD"].ToString();
                    drRow["LOCOD"] = dtData.Rows[i]["LOCOD"].ToString();
                    dtPrint.Rows.Add(drRow);
                }
            }
            dtPrint = CommonInfo.SortDataTable(dtPrint, strOrderBy);
            //列印匯總的報表
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DateCode_Summary", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
    }
}
