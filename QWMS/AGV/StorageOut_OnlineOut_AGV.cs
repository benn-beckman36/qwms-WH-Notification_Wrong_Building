using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using QCI.QWMS;
using QCI_QWMS_StorageData;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NPOI.HSSF.Util.HSSFColor;
using static System.Windows.Forms.LinkLabel;
using Microsoft.VisualBasic.Devices;
using System.Text.RegularExpressions;

namespace QWMS.AGV {
    public partial class StorageOut_OnlineOut_AGV : Form {
        private string _progid { get; set; }
        private UserInfo _userData { get; set; }
        public string strStation { get => this.cmbWs.Text; }
        public string strWerks { get { return cmbWerks.Text; } }
        public string strLgort { get { return cmbLgort.Text; } }
        public string strType { get => this.cmbOutType.Text.ToUpper(); }
        private string strUrgentLevel { get => this.ckbUrgent.Checked ? "1" : "3"; }
        private string strAgvFunction { get => this.strType == "ONLINE" ? "OnlineOut" : "TransferOut"; }
        private string strShelf { get => this.txtShelfId.Text.Trim(); }
        private string strAgvTaskNo { get; set; }
        private string strScanLocat { get => this.txtScanLocat.Text.Trim(); }
        private int iReqNo { get; set; }

        private string strLOCATscn = "";

        private Authority objAuth { get; set; }
        private SapData objSapData { get; set; }
        private StorageData objStorageData { get; set; }
        private StorageOut objStorageOut { get; set; }
        private StorageOut_AGV objStorageOut_AGV { get; set; }
        private DataTable dtMblnr { get; set; }
        private DataTable dtProcessMblnr { get; set; }
        private DataTable dtMatnr { get; set; }
        private DataTable dtCombineMatnr { get; set; }
        private DataTable dtMatnrAgv { get; set; }
        private DataTable dtScanMatnr { get; set; }
        private List<string> lsMblnrs { get; set; }
        private List<string> lsMatnrs { get; set; }
        private Dictionary<string, string> dicMblnrColsMapping { get; set; }
        private Dictionary<string, string> dicMatnrColsMapping { get; set; }
        private Dictionary<string, string> dicMatnrAgvColsMapping { get; set; }
        private List<string> lsAddDocColsMapping { get; set; }
        private AGVApi objAgv { get; set; }
        private bool AllowToClose { get; set; }
        public StorageOut_OnlineOut_AGV(UserInfo varUserData, string strProgid) {
            _progid = strProgid;
            _userData = varUserData;
            #region 检查权限
            if (false) {
                throw new Exception("You don't have right to use this program!!");
            }
            #endregion
            InitializeComponent();
            InitPageData();
        }

        private void InitPageData() {
            this.VariblesSetter("Init");
            this.ButtonStatus("Init");
            #region 工作站初始化
            List<string> lsWorkStation = objStorageOut_AGV.QueryWorkStation(this._userData.CompanyCode).AsEnumerable().Select(s => s.Field<string>("CTRLNM")).ToList();
            this.cmbWs.DataSource = lsWorkStation;
            #endregion
            // 出库类型
            List<string> lsOutType = new List<string>() { "", "Online", "Transfer" };
            this.cmbOutType.DataSource = lsOutType;
            // 厂区信息
            cmbWerks.Items.Clear();
            objAuth.CheckDCPlantAuthority().AsEnumerable().Select(s => s.Field<string>("F_TEXT")).ToList().ForEach(f => {
                this.cmbWerks.Items.Add(f);
            });
            #region 加扣表格初始化

            lsAddDocColsMapping = new List<string>();
            lsAddDocColsMapping.Add("MANDT");
            lsAddDocColsMapping.Add("ZAPPID");
            lsAddDocColsMapping.Add("ZITEM");
            lsAddDocColsMapping.Add("WERKS");
            lsAddDocColsMapping.Add("WERKS_I");
            lsAddDocColsMapping.Add("LGORT");
            lsAddDocColsMapping.Add("LGORT_I");
            lsAddDocColsMapping.Add("MATNR");
            lsAddDocColsMapping.Add("MENGE");
            lsAddDocColsMapping.Add("MBLNR");
            lsAddDocColsMapping.Add("MJAHR");
            lsAddDocColsMapping.Add("FLAG");
            lsAddDocColsMapping.Add("MESSAGE");
            lsAddDocColsMapping.Add("TXDAT");
            lsAddDocColsMapping.Add("TXTM");
            lsAddDocColsMapping.Add("TXEMP");
            lsAddDocColsMapping.Add("KOSTL");
            lsAddDocColsMapping.Add("CHARG");
            lsAddDocColsMapping.Add("BWART");
            lsAddDocColsMapping.Add("AUFNR");
            #endregion
            #region 初始化表格
            this.dtMblnr = new DataTable();
            this.dtMatnr = new DataTable();
            this.dtCombineMatnr = new DataTable();
            this.dtMatnrAgv = new DataTable();
            this.dtScanMatnr = new DataTable();


            this.dtScanMatnr.Columns.Add("MANDT", typeof(string));
            this.dtScanMatnr.Columns.Add("WERKS", typeof(string));
            this.dtScanMatnr.Columns.Add("LGORT", typeof(string));
            this.dtScanMatnr.Columns.Add("MARNO", typeof(string));
            this.dtScanMatnr.Columns.Add("LOCAT", typeof(string));
            this.dtScanMatnr.Columns.Add("MATNR", typeof(string));
            this.dtScanMatnr.Columns.Add("VEDAT", typeof(string));
            this.dtScanMatnr.Columns.Add("DACOD", typeof(string));
            this.dtScanMatnr.Columns.Add("LOCOD", typeof(string));
            this.dtScanMatnr.Columns.Add("LIFNR", typeof(string));
            this.dtScanMatnr.Columns.Add("MENGE", typeof(string));
            this.dtScanMatnr.Columns.Add("SERNO", typeof(string));
            this.dtScanMatnr.Columns.Add("EXPDAT", typeof(string));
            this.dtScanMatnr.Columns.Add("TASKID", typeof(string));
            this.dtScanMatnr.Columns.Add("SCAN", typeof(string));
            #endregion
        }

        private void ButtonStatus(string strType) {
            switch (strType) {
                case "Init":
                    this.cmbWerks.Enabled = true;
                    this.cmbLgort.Enabled = true;
                    this.btnConfirm.Enabled = true;
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    this.txtMblnr.Enabled = true;
                    this.btnPrint.Enabled = false;
                    this.btnQuery.Enabled = true;
                    //this.txtMatnrBarCode.Enabled = false;
                    //this.txtScanLocat.Enabled = false;
                    this.txtShelfId.Enabled = false;
                    break;
                case "Processing":
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.btnConfirm.Enabled = false;
                    this.btnSave.Enabled = true;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = false;
                    this.txtMblnr.Enabled = false;
                    this.btnPrint.Enabled = false;
                    this.btnQuery.Enabled = true;
                    this.txtMatnrBarCode.Enabled = false;
                    this.txtScanLocat.Enabled = false;
                    this.txtShelfId.Enabled = false;
                    break;
                case "Finish":
                    this.cmbWerks.Enabled = true;
                    this.cmbLgort.Enabled = true;
                    this.btnConfirm.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    this.txtMblnr.Enabled = true;
                    this.btnPrint.Enabled = true;
                    this.btnQuery.Enabled = false;
                    this.txtMatnrBarCode.Enabled = false;
                    this.txtScanLocat.Enabled = false;
                    this.txtShelfId.Enabled = false;
                    break;
                case "AGV":
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.btnConfirm.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = false;
                    this.btnExit.Enabled = false;
                    this.txtMblnr.Enabled = false;
                    this.btnPrint.Enabled = false;
                    this.btnQuery.Enabled = false;
                    this.txtMatnrBarCode.Enabled = false;
                    this.txtScanLocat.Enabled = true;
                    this.btnCall.Enabled = true;
                    //this.btnEndCall.Enabled = false;
                    //this.btnFlip.Enabled = false;
                    this.txtShelfId.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void VariblesSetter(string strStatus) {
            switch (strStatus) {
                case "Init":
                    this.dtMblnr = new DataTable();
                    this.dtMatnr = new DataTable();
                    this.dtCombineMatnr = new DataTable();
                    this.dtProcessMblnr = new DataTable();
                    this.lsMblnrs = new List<string>();
                    this.lsMatnrs = new List<string>();
                    this.objAuth = new Authority(_userData);
                    this.objStorageData = new StorageData(_userData);
                    this.objStorageOut = new StorageOut(this._userData, this._progid);
                    this.objStorageOut_AGV = new StorageOut_AGV(this._userData, this._progid);
                    this.AllowToClose = true;
                    break;
                case "Processing":
                    this.objSapData = new SapData(_userData, this.strWerks, this.strLgort);
                    this.AllowToClose = false;
                    break;
                case "AGV":
                    this.iReqNo = 0;
                    this.AllowToClose = false;
                    this.objAgv = new AGVApi(this._userData);
                    this.objAgv.COMCD = this._userData.CompanyCode;
                    this.objAgv.MANDT = this._userData.Client;
                    this.objAgv.CRNAM = this._userData.UserId;
                    break;
                case "Finish":
                    this.AllowToClose = true;
                    break;
                default:
                    this.AllowToClose = true;
                    break;
            }
        }

        private void GridViewColumnsGenerator() {

            #region 扣账单据的信息栏位
            this.dicMblnrColsMapping = new Dictionary<string, string>();
            this.dicMblnrColsMapping.Add("MBLNR", "Document No");
            this.dicMblnrColsMapping.Add("ZEILE", "Document Item");
            this.dicMblnrColsMapping.Add("MATNR", "Part No");
            this.dicMblnrColsMapping.Add("DECITEM", "DecItem");
            this.dicMblnrColsMapping.Add("KDMAT", "Customer P/N");
            this.dicMblnrColsMapping.Add("INSMK", "Stock");
            this.dicMblnrColsMapping.Add("CHARG", "Version");
            this.dicMblnrColsMapping.Add("LIFNR", "Vendor");
            this.dicMblnrColsMapping.Add("RMANO", "RMA No#");
            this.dicMblnrColsMapping.Add("MENGE", "Un-Store Out Qty");
            this.dicMblnrColsMapping.Add("ALQTY", "Storage Out Qty");
            this.dicMblnrColsMapping.Add("KOSTL", "Dept No.");
            this.dicMblnrColsMapping.Add("UMLGO", "Dest Storage");
            this.dicMblnrColsMapping.Add("ARBPL", "PD Line");
            this.dicMblnrColsMapping.Add("TRNTP", "Trn-Type");
            this.dicMblnrColsMapping.Add("REFID", "Reference id");
            this.dicMblnrColsMapping.Add("SEQNO", "Sequence No.");
            #endregion
            #region 储位料号的信息栏位
            this.dicMatnrColsMapping = new Dictionary<string, string>();
            //this.dicMatnrColsMapping.Add("LOCAT", "Location");
            this.dicMatnrColsMapping.Add("SLOCA", "Location");
            this.dicMatnrColsMapping.Add("MATNR", "Part No");
            this.dicMatnrColsMapping.Add("INSMK", "Stock");
            this.dicMatnrColsMapping.Add("MENGE", "Location Qty");
            this.dicMatnrColsMapping.Add("ALQTY", "Storage Out Qty");
            this.dicMatnrColsMapping.Add("BLACE", "Blance Qty");
            this.dicMatnrColsMapping.Add("MBLNR", "Document No");
            this.dicMatnrColsMapping.Add("ZEILE", "Document Item");
            this.dicMatnrColsMapping.Add("SERNO", "Serial No");
            this.dicMatnrColsMapping.Add("EBELN", "PO No");
            this.dicMatnrColsMapping.Add("LIFNR", "Vendor");
            this.dicMatnrColsMapping.Add("RMANO", "RMA No#");
            this.dicMatnrColsMapping.Add("OMBLNR", "Store In Docu. No");
            this.dicMatnrColsMapping.Add("INDAT", "Store In Date");
            this.dicMatnrColsMapping.Add("RMAK1", "Remark");
            this.dicMatnrColsMapping.Add("VEDAT", "Vendor Manufactured Date");
            this.dicMatnrColsMapping.Add("DACOD", "DateCode");
            this.dicMatnrColsMapping.Add("LOCOD", "LotCode");
            this.dicMatnrColsMapping.Add("REFID", "Reference id");
            this.dicMatnrColsMapping.Add("SEQNO", "Sequence No.");
            this.dicMatnrColsMapping.Add("PKDAT", "Pallet Time");
            this.dicMatnrColsMapping.Add("DECITEM", "DecItem");
            this.dicMatnrColsMapping.Add("KDMAT", "Customer P/N");
            this.dicMatnrColsMapping.Add("CHARG", "Version");
            this.dicMatnrColsMapping.Add("SUBLO", "Sub Location");
            #endregion
            #region AGV小车任务表
            this.dicMatnrAgvColsMapping = new Dictionary<string, string>();
            this.dicMatnrAgvColsMapping.Add("SCAN", "Scaned");
            this.dicMatnrAgvColsMapping.Add("LOCAT", "Location");
            this.dicMatnrAgvColsMapping.Add("SERNO", "Serial No");
            this.dicMatnrAgvColsMapping.Add("MATNR", "Part No");
            this.dicMatnrAgvColsMapping.Add("MENGE", "Location Qty");
            this.dicMatnrAgvColsMapping.Add("DECITEM", "DecItem");
            this.dicMatnrAgvColsMapping.Add("KDMAT", "Customer P/N");
            this.dicMatnrAgvColsMapping.Add("CHARG", "Version");
            this.dicMatnrAgvColsMapping.Add("VEDAT", "Vendor Manufactured Date");
            this.dicMatnrAgvColsMapping.Add("DACOD", "DateCode");
            this.dicMatnrAgvColsMapping.Add("LOCOD", "LotCode");
            #endregion
        }

        private void ShowMblnrDataGridView() {
            this.dgvMblnr.AutoGenerateColumns = false;
            this.dgvMblnr.Columns.Clear();
            this.GridViewColumnsGenerator();
            this.dicMblnrColsMapping.AsEnumerable().ToList().ForEach(f => {
                DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
                dataGridViewTextBoxColumn.DataPropertyName = f.Key;
                dataGridViewTextBoxColumn.HeaderText = f.Value;
                dataGridViewTextBoxColumn.Name = f.Key;
                dataGridViewTextBoxColumn.ReadOnly = true;
                dataGridViewTextBoxColumn.Width = 100;
                this.dgvMblnr.Columns.Add(dataGridViewTextBoxColumn);
            });

            this.dgvMblnr.DataSource = this.dtMblnr;
            this.lbMblnrCount.Text = this.dtMblnr.Rows.Count.ToString() + " Records";
        }

        private void ShowMatnrDataGridView() {
            this.dgvMatnr.AutoGenerateColumns = false;
            this.dgvMatnr.Columns.Clear();
            //this.GridViewColumnsGenerator();
            this.dicMatnrColsMapping.AsEnumerable().ToList().ForEach(f => {
                if (this.strType.Equals("OnlineOut") && f.Key.Equals("UMLGO")) {

                } else {
                    DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
                    dataGridViewTextBoxColumn.DataPropertyName = f.Key;
                    dataGridViewTextBoxColumn.HeaderText = f.Value;
                    dataGridViewTextBoxColumn.Name = f.Key;
                    dataGridViewTextBoxColumn.ReadOnly = true;
                    dataGridViewTextBoxColumn.Width = 100;
                    this.dgvMatnr.Columns.Add(dataGridViewTextBoxColumn);
                }
            });
            // dtMatnr排序
            if (this.dtMatnr.Rows.Count > 0) {
                this.dtMatnr.DefaultView.Sort = "LOCAT ASC, MATNR ASC";
            }

            //this.dgvMatnr.Sort(dgvMatnr.Columns["Location"], ListSortDirection.Ascending);

            this.dgvMatnr.DataSource = this.dtMatnr.DefaultView.ToTable();
            this.lbMatnrCount.Text = this.dtMatnr.Rows.Count.ToString() + " Records";
        }

        private void ShowAgvMatnrDataGridView() {
            this.dgvAgvMatnr.AutoGenerateColumns = false;
            this.dgvAgvMatnr.DataSource = null;
            this.dgvAgvMatnr.Columns.Clear();
            this.dicMatnrAgvColsMapping.AsEnumerable().ToList().ForEach(f => {
                DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
                dataGridViewTextBoxColumn.DataPropertyName = f.Key;
                dataGridViewTextBoxColumn.HeaderText = f.Value;
                dataGridViewTextBoxColumn.Name = f.Key;
                dataGridViewTextBoxColumn.ReadOnly = true;
                dataGridViewTextBoxColumn.Width = 100;
                this.dgvAgvMatnr.Columns.Add(dataGridViewTextBoxColumn);
            });
            this.dgvAgvMatnr.DataSource = this.dtScanMatnr;
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e) {
            // 仓别信息
            cmbLgort.Items.Clear();
            //this.cmbLgort.DataSource = 
            objAuth.CheckLgortWithAuth(this.strWerks, "").AsEnumerable().Select(s => s.Field<string>("F_VALUE")).ToList().ForEach(f => {
                this.cmbLgort.Items.Add(f);
            });
            this.objStorageData.WERKS = this.strWerks;
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e) {
            this.objStorageData.LGORT = this.strLgort;
            this.dtMblnr.Rows.Clear();
            this.txtMblnr.Clear();
        }

        private void txtMblnr_MouseDoubleClick(object sender, MouseEventArgs e) {
            // 检查是否有选择厂区仓别
            if (string.IsNullOrEmpty(this.strWerks) || string.IsNullOrEmpty(this.strLgort)) {
                MessageBox.Show("请先选择厂区和仓别");
                return;
            }
            StorageOut_SapDataSelect storageOut_SapDataSelect = new StorageOut_SapDataSelect(this._userData, this.strWerks, this.strLgort, "C1", null, this.strType);
            storageOut_SapDataSelect.ShowDialog();
            this.lsMblnrs = storageOut_SapDataSelect.Mblnr != null && storageOut_SapDataSelect.Mblnr.Count > 0 ? storageOut_SapDataSelect.Mblnr.Cast<string>().ToList() : new List<string>();
            this.lsMatnrs = storageOut_SapDataSelect.Matnrs != null && storageOut_SapDataSelect.Matnrs.Count > 0 ? storageOut_SapDataSelect.Matnrs.Cast<string>().ToList() : new List<string>();
            this.txtMblnr.Text = lsMblnrs.Count > 0 ? lsMblnrs.Aggregate((current, next) => current + "," + next) : "";
            this.btnConfirm_Click(null, null);
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            QueryMatnrData();
        }
        private void btnConfirm_Click(object sender, EventArgs e) {
            // 检查厂区仓别
            if (string.IsNullOrEmpty(this.strWerks) || string.IsNullOrEmpty(this.strLgort)) {
                MessageBox.Show("请先选择厂区和仓别");
                this.ButtonStatus("Init");
                return;
            }
            // 检查是否填入单据
            if (string.IsNullOrEmpty(this.txtMblnr.Text)) {
                MessageBox.Show("请填入扣账单号");
                this.ButtonStatus("Init");
                return;
            }
            // txtMblnr 是否有值
            if (string.IsNullOrEmpty(this.txtMblnr.Text)) {
                MessageBox.Show("请填入扣账单号");
                this.ButtonStatus("Init");
                return;
            }
            this.VariblesSetter("Processing");
            // 有单据，根据,分割
            this.lsMblnrs = this.txtMblnr.Text.Split(',').ToList();
            QueryMblnrData();
            this.ShowMblnrDataGridView();
        }

        private void QueryMblnrData() {
            // 判断扣账编号列表是否为空
            if (this.lsMblnrs == null || this.lsMblnrs.Count == 0) {
                MessageBox.Show("扣账编号为空");
                this.ButtonStatus("Init");
                return;
            }
            ArrayList alMblnr = new ArrayList();
            this.lsMblnrs.ForEach(f => {
                alMblnr.Add(f);
            });
            ArrayList alMatnr = new ArrayList();
            this.lsMatnrs.ForEach(f => {
                alMatnr.Add(f);
            });

            this.dtMblnr = objSapData.QuerySapLineOutData(alMblnr, this.strType, alMatnr);
            this.dtMblnr.Columns.Add("INSPT", typeof(string));

            #region 透過Send id 取得WHSID.SEQNO  Smose Liao 20100426
            string strSeqno = "";
            string strArbpl = "";
            DataColumn cSequence = new DataColumn("SEQNO", typeof(string));
            this.dtMblnr.Columns.Add(cSequence);
            for (int i = 0; i < this.dtMblnr.Rows.Count; i++) {
                if (this.dtMblnr.Rows[i]["REFID"].ToString() != "") {
                    strArbpl = this.dtMblnr.Rows[i]["ARBPL"].ToString();
                    //若線別為空，則不取Sequence No.
                    if (strArbpl == "") {
                        strSeqno = "";
                    } else {
                        strSeqno = objSapData.QuerySapSeqno(this.dtMblnr.Rows[i]["WERKS"].ToString(), this.dtMblnr.Rows[i]["REFID"].ToString(), this.dtMblnr.Rows[i]["MATNR"].ToString(), strArbpl.Substring(0, 1));
                        this.dtMblnr.Rows[i]["SEQNO"] = strSeqno;
                    }
                }
                //GB 材料防呆 
                if (this.dtMblnr.Rows[i]["WERKS"].ToString() == "CS42" || this.dtMblnr.Rows[i]["WERKS"].ToString() == "CS32" || this.dtMblnr.Rows[i]["WERKS"].ToString() == "CQ5A") {
                    if (this.dtMblnr.Rows[i]["MATNR"].ToString().Substring(0, 2) == "20" || this.dtMblnr.Rows[i]["MATNR"].ToString().Substring(0, 2) == "2L") {
                        DialogResult result = new DialogResult();
                        result = MessageBox.Show("该材料属于GB材料，请确认是否继续使用原材联机出库功能？", "GB材料提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No) {
                            MessageBox.Show("GB材料请使用GB联机出库");
                            return;
                        }
                    }
                }
            }
            #endregion

            if (this.dtMblnr.Rows.Count == 0) {
                MessageBox.Show("No data!!");
                return;
            }
            this.ShowMblnrDataGridView();
            this.ButtonStatus("");
        }

        private void QueryMatnrData() {
            try {
                // ……
                // 根据Mblnr表查询，用作btnQuery_Click
                objStorageOut_AGV = new StorageOut_AGV(this._userData, this.strWerks, this.strLgort, this._progid);
                DataSet dsTmp = objStorageOut_AGV.QueryStorageOutDateCode_AGV(this.dtMblnr, true);
                this.dtProcessMblnr = dsTmp.Tables[0];
                this.dtMatnr = dsTmp.Tables[1];
                if (this.dtMatnr.Rows.Count == 0) {
                    MessageBox.Show("无数据!!");
                    return;
                }
                this.dtMatnr.DefaultView.Sort = "LOCAT ASC";
                if (this.dtMatnr.Rows.Count == 0) {
                    MessageBox.Show("库存不足!!");
                    this.dtMatnr = new DataTable();
                    return;
                }

                // 写入优先级
                this.dtMatnr.AsEnumerable().ToList().ForEach(f => {
                    f.SetField<string>("PROPRI", this.strUrgentLevel);
                });

                this.ShowMblnrDataGridView();
                this.ShowMatnrDataGridView();
                this.ButtonStatus("Processing");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            this.ButtonStatus("Processing");
            DataTable dtAddQty = new DataTable();
            dtAddQty = this.dtMatnr.Clone();
            try {
                #region 生成TaskCode
                objStorageOut_AGV = new StorageOut_AGV(this._userData, this.strWerks, this.strLgort, this._progid);
                List<string> lsLgort = dtMatnr.AsEnumerable().Select(x => x.Field<string>("LGORT")).Distinct().ToList();
                DataTable dtLgort = new DataTable();
                dtLgort.Columns.Add("WERKS");
                dtLgort.Columns.Add("LGORT");
                dtLgort.Columns.Add("TaskNo");
                foreach (string sLgort in lsLgort) {
                    DataRow drResult = dtLgort.NewRow();
                    drResult["WERKS"] = strWerks;
                    drResult["LGORT"] = sLgort;
                    drResult["TaskNo"] = strWerks + strLgort + DateTime.Now.ToString("yyyyMMdd") + objStorageOut_AGV.GetSimulationTaskNo("AGV", "TASKNO");
                    dtLgort.Rows.Add(drResult.ItemArray);
                }
                #endregion

                string strResult = string.Empty;
                strAgvTaskNo = dtLgort.Rows[0]["TaskNo"].ToString();

                #region 计算是否需要加扣
                // 确认库存是否足够
                this.dtMatnr.Select(" BLACE > 0 ").ToList().ForEach(f => {
                    DataRow dr = dtAddQty.NewRow();
                    dr.ItemArray = f.ItemArray;
                    dtAddQty.Rows.Add(dr);
                });




                if (dtAddQty.Rows.Count > 0) {
                    // 加扣只允许到MA*/TW*(TWGS除外)
                    if (dtAddQty.AsEnumerable().Any(a => !string.IsNullOrEmpty(a.Field<string>("UMLGO")) && !(a.Field<string>("UMLGO").StartsWith("TW") || a.Field<string>("UMLGO").StartsWith("MA"))
                    || a.Field<string>("UMLGO").Equals("TWGS"))) {
                        this.ButtonStatus("Processing");
                        MessageBox.Show("只有 TW** 或者 MA** 仓别可加扣!");
                        return;
                    }
                    if (!this.SendToSAP(dtAddQty)) {
                        MessageBox.Show("SAP请求失败，请稍后重试！");
                        this.ButtonStatus("Processing");
                        return;
                    }
                }

                #endregion


                #region 出库
                //string jsSource = JsonConvert.SerializeObject(dtMblnr);
                string jsStorage = JsonConvert.SerializeObject(dtMatnr);
                string jsLgort = JsonConvert.SerializeObject(dtLgort);

                strResult = objStorageOut_AGV.AddOnlineOutSaveData_AGV(jsStorage, jsLgort);

                switch (strResult) {
                    case "Y":
                        MessageBox.Show("保存成功!!!");
                        btnSave.Enabled = false;
                        //btnDacodPrint.Enabled = true;
                        //btnSummary.Enabled = true;
                        btnRefresh.Enabled = true;
                        AllowToClose = true;
                        break;
                    case "N":
                        MessageBox.Show("保存失败!!!");
                        return;
                    case "F":
                        MessageBox.Show("库存不足，请重新查询!!");
                        btnQuery.Enabled = true;
                        dtMatnr.Clear();
                        btnSave.Enabled = false;
                        AllowToClose = true;
                        return;
                    case "D":
                        MessageBox.Show("单据已处理!!!");
                        AllowToClose = true;
                        return;
                    default:
                        MessageBox.Show("问题错误,请联系 MIS!!!");
                        AllowToClose = true;
                        return;
                }

                #endregion

                #region 生成AGV任务
                // 启用扫描
                this.ButtonStatus("AGV");
                this.VariblesSetter("AGV");

                // 生成AGV储位明细
                for (int i = 0; i < dtMatnr.Rows.Count; i++) {
                    if (this.dtScanMatnr.Select(" LOCAT = '" + this.dtMatnr.Rows[i]["LOCAT"].ToString() + "'").Count() > 0) {
                        continue;
                    }
                    DataRow drN = dtScanMatnr.NewRow();
                    drN["MANDT"] = dtMatnr.Rows[i]["MANDT"].ToString();
                    drN["WERKS"] = dtMatnr.Rows[i]["WERKS"].ToString();
                    drN["LGORT"] = dtMatnr.Rows[i]["LGORT"].ToString();
                    drN["MARNO"] = dtMatnr.Rows[i]["MARNO"].ToString();
                    drN["LOCAT"] = dtMatnr.Rows[i]["LOCAT"].ToString();
                    //drN["SLOCA"] = dtMatnr.Rows[i]["SLOCA"].ToString();
                    drN["MATNR"] = dtMatnr.Rows[i]["MATNR"].ToString();
                    drN["VEDAT"] = dtMatnr.Rows[i]["VEDAT"].ToString();
                    drN["DACOD"] = dtMatnr.Rows[i]["DACOD"].ToString();
                    drN["LOCOD"] = dtMatnr.Rows[i]["LOCOD"].ToString();
                    drN["LIFNR"] = dtMatnr.Rows[i]["LIFNR"].ToString();
                    drN["MENGE"] = dtMatnr.Rows[i]["MENGE"].ToString();
                    drN["SERNO"] = dtMatnr.Rows[i]["SERNO"].ToString();
                    drN["EXPDAT"] = dtMatnr.Rows[i]["EXPDAT"].ToString();
                    drN["TASKID"] = dtMatnr.Rows[i]["TASKID"].ToString();
                    drN["SCAN"] = "N";
                    dtScanMatnr.Rows.Add(drN);
                }
                #endregion
                this.ShowAgvMatnrDataGridView();
                this.ShowMatnrDataGridView();
                // 判断储位，如果只包含DY类型，只需要扫描储位、实物
                if (dtScanMatnr.Select(" LOCAT like 'DY%'").Count() == dtScanMatnr.Rows.Count) {
                    this.txtScanLocat.Enabled = true;
                    this.txtMatnrBarCode.Enabled = true;
                    this.txtShelfId.Enabled = false;
                    this.txtScanLocat.Focus();
                }
                //btnCall_Click(null, null);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private bool SendToSAP(DataTable dtAddQty) {


            //sap交互
            #region 将需要加扣的信息填到加扣表
            DataTable dtAddDocToSAP = new DataTable();
            #region 加扣表格初始化
            this.lsAddDocColsMapping.ForEach(f => {
                dtAddDocToSAP.Columns.Add(f, typeof(string));
            });
            #endregion
            for (int i = 0; i < dtAddQty.Rows.Count; i++) {
                string strMblnrNew = dtAddQty.Rows[0]["WERKS"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                string strZeile = (i + 1).ToString().PadLeft(4, '0');

                DataRow drN = dtAddDocToSAP.NewRow();
                drN["MANDT"] = this._userData.Client;
                drN["ZAPPID"] = strMblnrNew;
                drN["ZITEM"] = strZeile; //工单
                drN["WERKS"] = dtAddQty.Rows[i]["WERKS"].ToString();
                drN["WERKS_I"] = dtAddQty.Rows[i]["WERKS"].ToString(); //接收厂区
                drN["LGORT"] = dtAddQty.Rows[i]["LGORT"].ToString();
                drN["LGORT_I"] = dtAddQty.Rows[i]["UMLGO"].ToString(); //接收仓别
                drN["MATNR"] = dtAddQty.Rows[i]["MATNR"].ToString();
                drN["MENGE"] = dtAddQty.Rows[i]["BLACE"].ToString();
                drN["MBLNR"] = "";
                drN["MJAHR"] = "";
                drN["FLAG"] = "";
                drN["MESSAGE"] = "";
                drN["TXDAT"] = "";
                drN["TXTM"] = "";
                drN["TXEMP"] = this._userData.UserId;
                drN["KOSTL"] = dtAddQty.Rows[i]["KOSTL"].ToString(); //部门代码 0A434
                drN["CHARG"] = "";
                drN["BWART"] = "311";
                drN["AUFNR"] = "";
                dtAddDocToSAP.Rows.Add(drN);
            }

            #endregion

            #region SAP扣账

            DataSet dsData = new DataSet();
            dsData.Tables.Add(dtAddDocToSAP);
            MM.MM_Service obj = new QWMS.MM.MM_Service();
            DataSet dsResultFromSAP = obj.Z_MM_RFC_POSTYCN("A", dsData);
            DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];


            if (dtResultFromSAP.Rows[0]["FLAG"].ToString().Trim() == "Y") {
                int sapCount = 1;
                for (int i = 0; i < dtMatnr.Rows.Count; i++) {
                    if (Convert.ToInt32(dtMatnr.Rows[i]["BLACE"].ToString()) > 0) {
                        string itemNo = "0000" + (((sapCount - 1) * 2) + 1);
                        DataRow newrow = dtMatnr.NewRow();
                        //newrow.ItemArray = dtMatnr.Rows[i].ItemArray;
                        foreach (DataColumn dc in this.dtMatnr.Columns) {
                            newrow[dc.ColumnName] = this.dtMatnr.Rows[i][dc.ColumnName];
                        }
                        // 获取Item号的后四位
                        newrow["MBLNR"] = dtResultFromSAP.Rows[0]["MBLNR"].ToString().Trim() + itemNo.Substring(itemNo.Length - 4);
                        newrow["ZEILE"] = "";
                        newrow["MENGE"] = this.dtMatnr.Rows[i]["BLACE"].ToString();
                        newrow["ALQTY"] = this.dtMatnr.Rows[i]["BLACE"].ToString();
                        newrow["BLACE"] = "0";


                        dtMatnr.Rows.Add(newrow);
                        dtMatnr.Rows[i]["BLACE"] = "0";
                        sapCount++;
                    }
                }
                MessageBox.Show("SAP扣账成功!");
                return true;
            } else {
                MessageBox.Show(dtResultFromSAP.Rows[0]["MESSAGE"].ToString().Trim());
                return false;
            }
            #endregion
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            iReqNo = 0;
            this.ButtonStatus("Init");
            this.VariblesSetter("Init");
            this.ShowMatnrDataGridView();
            this.ShowMblnrDataGridView();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void txtMblnr_KeyDown(object sender, KeyEventArgs e) {
            // enter
            if (e.KeyCode == Keys.Enter) {
                this.btnQuery_Click(null, null);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            if (this.dtCombineMatnr == null || this.dtCombineMatnr.Rows.Count == 0) {
                MessageBox.Show("无出库的数据，请确认！");
                return;
            }
            PrintReport("Manual");
        }
        #region PrintReport   (出庫備料單    打印備料單  Ryan 20131023)
        private void PrintReport(string strPrintType) {
            #region 列印資料
            //Order by {
            string strOrderBy = "LOCAT,MATNR,INDAT";


            DataTable dtPrint = CommonInfo.SortDataTable(this.dtMatnr, strOrderBy);
            ReportPrint objReportPrint;
            DataTable dtRecomputeReport = new DataTable();
            //if (chkPrint.Checked == true)                                               //Case 1: 依預設打印方式
            //{

            //objStorageOut.AddChectOutPad(dtPrint, "");
            objReportPrint = new ReportPrint(this._userData, "STOREOUT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
            //} 
            //else if (chkPrintByLine.Checked == true) {                             //Case 2: 依線別打印備料單


            //    dtPrint = GetStorageOutDataWithLineInfo(dtCombineStorage, strWerks);    //增加線別品名Ryan 20131016 
            //    dtRecomputeReport = RecomputeMengeBlace(dtPrint);                       //重新計算原始庫存數量和剩餘數量Ryan 20131107 
            //    DataTable dtRecomputeReportSorting = CommonInfo.SortDataTable(dtRecomputeReport, " PRITY asc , LINE asc , LOCAT asc ");

            //    if (chkThermalpaper.Checked == true) //熱感應紙打印
            //    {
            //        objReportPrint = new ReportPrint(this._userData, "LINEINFOBYLINE_TERPER", dtRecomputeReportSorting);
            //    } else {
            //        objReportPrint = new ReportPrint(this._userData, "LINEINFOBYLINE", dtRecomputeReportSorting);
            //    }
            //    objReportPrint.MdiParent = this.ParentForm;
            //    objReportPrint.Show();

            //    //objStorageOut.AddChectOutPad(dtRecomputeReportSorting, "AGV");


            //} else if (chkPrintByMat.Checked == true)                                     //Case 3: 依品名打印備料單
            //  {
            //    dtPrint = GetStorageOutDataWithLineInfo(dtCombineStorage, strWerks);    //增加線別品名Ryan 20131016 
            //    dtRecomputeReport = RecomputeMengeBlace(dtPrint);                       //重新計算原始庫存數量和剩餘數量Ryan 20131107 
            //    DataTable dtPrintAddInfo = AddReportInfo(dtRecomputeReport);            //增加匯總料號Ryan 20131016 
            //    objReportPrint = new ReportPrint(this._userData, "LINEINFOBYMATNM", dtPrintAddInfo);
            //    objReportPrint.MdiParent = this.ParentForm;
            //    objReportPrint.Show();
            //} else                                                                        //Case 4: 都沒勾選 需要點打印按鈕(此時保存按鈕反灰)
            //  {
            //    if (strPrintType == "Manual") //打印按鈕
            //    {
            //        objReportPrint = new ReportPrint(this._userData, "STOREOUT", dtPrint);
            //        objReportPrint.MdiParent = this.ParentForm;
            //        objReportPrint.Show();
            //    } else {
            //        //strPrintType="PrintAfterSave"
            //        //剛扣完帳 btnSave.Enabled=true 但沒有選擇要預先打印報表
            //    }
            //}
            #endregion
        }
        #endregion

        #region 扫描储位
        private void txtScanLocat_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                strLOCATscn = "";
                strLOCATscn = txtScanLocat.Text.ToString().Trim();

                //扫描到的储位比实际储位位数少
                if (dtMatnrAgv != null && dtMatnrAgv.Columns.Contains("LOCAT")
                    && dtMatnrAgv.AsEnumerable().Where(w => w.Field<string>("LOCAT").Contains(strLOCATscn)).Count() <= 0) {
                    MessageBox.Show("储位不在列表中, 请重新扫描!");
                    SetErrNotice();
                    txtScanLocat.Focus();
                    txtScanLocat.Text = "";
                    return;
                }
                if (txtScanLocat.Text.ToString().Trim().Substring(1, 3) == txtShelfId.Text) {
                } else {
                    DataTable dtAGV = objStorageOut_AGV.QueryAGVWorkStation(strWerks, strLgort, strStation, strAgvTaskNo);
                    if (dtAGV.Rows.Count > 0) {
                        txtShelfId.Text = dtAGV.Rows[0]["MARNO"].ToString().Trim();
                    }
                }
                txtMatnrBarCode.Enabled = true;
                txtMatnrBarCode.Focus();
                SetOKNotice();
            }
        }
        #endregion
        #region 扫描料盘
        private void txtMatnrBarCode_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                // 扫描得来的信息用‘;’分割
                List<string> lsBarCode = this.txtMatnrBarCode.Text.Split(';').ToList();
                this.txtScanLocat.Enabled = true;
                this.txtMatnrBarCode.Enabled = false;
                txtMatnrBarCode.Text = "";
                txtMatnrBarCode.Focus();
                string strTmpMatnr = string.Empty;
                string strTmpDacod = string.Empty;
                string strTmpLifnr = string.Empty;
                string strTmpLocod = string.Empty;
                string strTmpMenge = string.Empty;
                string strTmpSerno = string.Empty;
                string strTmpExpdat = string.Empty;
                string strTmpTaskid = string.Empty;

                if (lsBarCode.Count >= 5) {
                    strTmpMatnr = lsBarCode[0].ToString().Trim();
                    strTmpDacod = lsBarCode[1].ToString().Trim();
                    strTmpLifnr = lsBarCode[2].ToString().Trim();
                    strTmpLocod = lsBarCode[3].ToString().Trim();
                    strTmpMenge = lsBarCode[4].ToString().Trim();
                    if (lsBarCode.Count >= 8 ) {
                        strTmpSerno = lsBarCode[6].Trim();
                    }//大于9最后一项为保存期，则为IQC检验过的材料
                    if (lsBarCode.Count > 9) {
                        string strEXPDAT = lsBarCode[lsBarCode.Count - 1].ToString().Trim();
                        if (!ClaCommon.CheckDateValid(strEXPDAT)) {
                            MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strEXPDAT));
                            txtMatnrBarCode.Text = "";
                            txtMatnrBarCode.Focus();
                            SetErrNotice();
                            return;
                        }
                        strTmpExpdat = strEXPDAT;
                        strTmpTaskid = lsBarCode[6].ToString().Trim().Substring(0, 15);
                    }
                } else {
                    if (lsBarCode.Count == 1) {
                        //1项则为DIDNO，查询WHRID表，抓取数据
                        DataTable dtData = objStorageData.QueryIQC_WHRID(lsBarCode[0]);
                        //MATNR,DACOD,LIFNR,LOCOD,MENGE
                        if (dtData.Rows.Count > 0) {
                            strTmpMatnr = dtData.Rows[0]["MATNR"].ToString();
                            strTmpDacod = dtData.Rows[0]["DACOD"].ToString();
                            strTmpLifnr = dtData.Rows[0]["LIFNR"].ToString();
                            strTmpLocod = dtData.Rows[0]["LOCOD"].ToString();
                            strTmpMenge = dtData.Rows[0]["MENGE"].ToString();
                        } else {
                            MessageBox.Show("无DIDNO数据!");
                            txtMatnrBarCode.Text = "";
                            txtMatnrBarCode.Focus();
                            SetErrNotice();
                            return;
                        }
                    } else {
                        MessageBox.Show("格式错误!");
                        txtMatnrBarCode.Text = "";
                        txtMatnrBarCode.Focus();
                        SetErrNotice();
                        return;
                    }
                }
                // 比对料号是否在AGV料号表

                // 初始设定为不满足
                bool isMatch = false;
                string downWerks = string.Empty;
                string downLgort = string.Empty;
                string downLocat = string.Empty;

                List<string> lsLocat = objStorageOut_AGV.getLocation(strWerks, strLgort, strTmpMatnr, strTmpDacod, strTmpLifnr, strTmpLocod, strTmpMenge, strLOCATscn);

                this.dtScanMatnr.AsEnumerable().ToList().ForEach(f => {
                    // 未找到目标储位才进行循环
                    if (string.IsNullOrEmpty(downLocat)) {
                        if (!isMatch) {
                            string strRowSerno = f.Field<string>("SERNO").Trim();
                            string strSplitTmpSer = strRowSerno.Length < strTmpSerno.Length? strTmpSerno.Substring(0,strRowSerno.Length): strTmpSerno;
                            // 包含LOCAT为开头，设定为true，其他条件满足不会改变为当前判断，其他条件不满足时则会变成false
                            //isMatch = !string.IsNullOrEmpty(strLOCATscn) && f.Field<string>("LOCAT").Contains(strLOCATscn);
                            isMatch = lsLocat != null && lsLocat.Count > 0 && lsLocat.Contains(f.Field<string>("LOCAT"));
                            // 以下条件满足则为不改变，不满足则为false，不排除内容（dacod，locod）为空的情况
                            isMatch = f.Field<string>("MATNR").Equals(strTmpMatnr) ? isMatch : false;
                            isMatch = f.Field<string>("DACOD").Equals(strTmpDacod) ? isMatch : false;
                            isMatch = f.Field<string>("LIFNR").Equals(strTmpLifnr) ? isMatch : false;
                            isMatch = f.Field<string>("LOCOD").Equals(strTmpLocod) ? isMatch : false;
                            isMatch = f.Field<string>("SERNO").Equals(strSplitTmpSer) ? isMatch : false;
                            //isMatch = string.IsNullOrEmpty(strTmpMenge) ? isMatch : f["MENGE"].ToString().Equals(strTmpMenge);
                            isMatch = f.Field<string>("SCAN").Equals("N") ? isMatch : false;
                            // 通过当前Location查找scanMatnr,未扫描的部分
                            //DataRow dr = this.dtScanMatnr.Select("LOCAT LIKE '" + f.Field<string>("LOCAT") + "%' AND SCAN = 'N'").FirstOrDefault();

                            // 是否存在未扫描的部分
                            //isMatch = dr != null ? isMatch : false;
                            if (isMatch) {
                                f.SetField<string>("SCAN", "Complete");
                                downWerks = f.Field<string>("WERKS");
                                downLgort = f.Field<string>("LGORT");
                                downLocat = f.Field<string>("LOCAT");
                            }
                        }
                    }
                });
                if (!isMatch) {
                    MessageBox.Show("料号不在列表中，请重新扫描!");
                    txtMatnrBarCode.Text = "";
                    txtMatnrBarCode.Focus();
                    SetErrNotice();
                    return;
                }
                // 灭灯
                //MessageBox.Show("灭灯——"+downLocat);
                turnOffLight(downLocat);
                // 增加按当前储位更新状态 D -> Y 待下架 -> 已下架
                if (!objStorageOut_AGV.UpdateLocatStockStatus(downWerks, downLgort, downLocat)) {
                    MessageBox.Show("库存状态更新错误! 请联系系统管理员!");
                    txtMatnrBarCode.Text = "";
                    txtMatnrBarCode.Focus();
                    SetErrNotice();
                    return;
                }
                this.ShowAgvMatnrDataGridView();
                // 表格变色
                ShowErrorNumber(strTmpMatnr, strTmpDacod, strTmpLocod, strTmpLifnr, downLocat);
                SetOKNotice();
                txtScanLocat.Focus();
                if (dtScanMatnr.Select(" SCAN = 'N' AND LOCAT LIKE '" + strLOCATscn + "%'").Count() == 0) {
                    txtScanLocat.Text = "";
                }
            }
        }
        #endregion

        public void ShowErrorNumber(string strMatnr, string strDacod, string strLocod, string strVendor, string strLOCAT) {
            string tmpSlocat = string.Empty;
            for (int i = 0; i < dgvMatnr.Rows.Count; i++) {
                if (dgvMatnr.Rows[i].Cells["MATNR"].Value != null) {
                    var matnr = dgvMatnr.Rows[i].Cells["MATNR"].Value.ToString();
                    var lifnr = dgvMatnr.Rows[i].Cells["LIFNR"].Value.ToString();
                    var dacod = dgvMatnr.Rows[i].Cells["DACOD"].Value.ToString();
                    var locod = dgvMatnr.Rows[i].Cells["LOCOD"].Value.ToString();
                    var sloca = dgvMatnr.Rows[i].Cells["SLOCA"].Value.ToString();
                    var sublo = dgvMatnr.Rows[i].Cells["SUBLO"].Value.ToString();
                    if (matnr.Equals(strMatnr) && lifnr.Equals(strVendor) && dacod.Equals(strDacod) && locod.Equals(strLocod) && strLOCAT.Contains(sloca) && ((strLOCAT.Length > sloca.Length && strLOCAT.EndsWith(sublo)) || strLOCAT.Length == sloca.Length)) {
                        if (dgvMatnr.Rows[i].DefaultCellStyle == null) {
                            dgvMatnr.Rows[i].DefaultCellStyle = new DataGridViewCellStyle();
                        }
                        if (dgvMatnr.Rows[i].DefaultCellStyle.BackColor == Color.Empty) {
                            dgvMatnr.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        }
                        //tmpSlocat = this.dtMatnr.Rows[i]["LOCAT"].ToString();
                        //break; // 假设只有一行匹配，找到后即可退出循环
                    }
                }
            }
        }
        private void btnCall_Click(object sender, EventArgs e) {
            try {
                #region 当前是否有货架在作业
                if (iReqNo != 0 && !string.IsNullOrEmpty(this.strShelf) && !this.complateCurrentShelf()) {
                    MessageBox.Show("当前货架正在作业，请稍候!");
                    return;
                }
                #endregion
                #region 第一次请求，将料架信息放在调度中间表
                if (iReqNo == 0) {
                    this.dtMatnrAgv = this.objStorageOut_AGV.QueryStockOutData_AGV(strWerks, strLgort, this.strAgvTaskNo, "N");
                    DataTable dtAGVTemp = dtMatnrAgv.DefaultView.ToTable(true, new string[] { "MANDT", "WERKS", "LGORT", "TASKID", "REQID", "ShelfNo", "COMCD" });
                    if (!objStorageOut_AGV.BulkCopyWHAGV(dtAGVTemp, this.strStation)) {
                        MessageBox.Show("请再次点击呼叫AGV!");
                        return;
                    }
                    InitAGVData();
                }
                #endregion
                #region 判断是否还有未刷入的数据，若有,提示无法call下一辆，若无，则更新备料单数据状态为Y
                // 匹配当前料架刷入的数据
                if (iReqNo != 0 && dtScanMatnr != null && dtScanMatnr.Columns.Contains("MARNO") && this.dtScanMatnr.Select(" MARNO = '" + this.strShelf + "' AND SCAN = 'N'").Count() > 0) {
                    MessageBox.Show("当前料架未完成!");
                    return;
                }
                #endregion

                #region 开始呼叫AGV API，仅第一次呼叫需要单据号
                DataTable dtAgvMblnr = new DataTable();
                dtAgvMblnr.Columns.Add("doc_number");
                if (iReqNo == 0) {
                    foreach (DataRow dr in dtMatnrAgv.Rows) {
                        if (dtAgvMblnr.Select("doc_number='" + dr["MBLNR"] + "'").Length == 0) {
                            DataRow drMblnr = dtAgvMblnr.NewRow();
                            drMblnr["doc_number"] = dr["MBLNR"].ToString();
                            dtAgvMblnr.Rows.Add(drMblnr);
                        }
                    }
                }
                string strRequestType = "outboundTask";
                iReqNo++;
                var outboundTask = new {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = this.strStation,
                    doc_type = strAgvFunction,
                    detail = dtAgvMblnr,
                    task_id = this.strAgvTaskNo,
                    task_sequence = iReqNo,
                    priority = Convert.ToInt32(this.strUrgentLevel)
                };

                string strRequest = this.objAgv.AGVHttpRequest(this.strAgvFunction, strRequestType, JsonConvert.SerializeObject(outboundTask));
                #endregion

                if (string.IsNullOrEmpty(strRequest)) {
                    MessageBox.Show("没有响应，请再次点击");
                    return;
                }

                JObject Request = (JObject)JsonConvert.DeserializeObject(strRequest);

                // 比对料架
                this.txtShelfId.Text = Request["shelf_id"].ToString();
                List<string> locats = Request["location"].ToObject<List<string>>();
                this.dtScanMatnr.AsEnumerable().Where(w => locats.Contains(w.Field<string>("LOCAT"))).ToList()
                    .ForEach(f => {
                        f.SetField<string>("MARNO", Request["shelf_id"].ToString());
                    });
                this.ShowAgvMatnrDataGridView();
                MessageBox.Show("呼叫AGV成功!");
                txtScanLocat.Focus();
                this.ButtonStatus("AGV");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString() + "<-CallAGV()");
            }
        }

        private void btnFlip_Click(object sender, EventArgs e) {
            try {
                #region 翻面API
                txtScanLocat.Focus();
                string strRequestType = "shelfFlip";
                iReqNo++;
                var shelfFlip = new {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = this.strStation,
                    shelf_id = this.strShelf,
                    task_id = this.strAgvTaskNo,
                    task_sequence = iReqNo
                };

                AGVApi objAGVApi = new AGVApi(this._userData);
                string strRequest = objAGVApi.AGVHttpRequest(this.strAgvFunction, strRequestType, JsonConvert.SerializeObject(shelfFlip));
                #endregion

                if (string.IsNullOrEmpty(strRequest)) {
                    MessageBox.Show("翻面失败");
                    return;
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString() + "<-Flip()");
            }
        }

        private void btnEndCall_Click(object sender, EventArgs e) {
            try {
                //没有完成全部作业时，无法结束
                if (!complateAllWork() || !complateCurrentShelf()) {
                    MessageBox.Show("料架未完成作业!");
                    return;
                }

                #region 全部结束作业
                string strRequestType = "endTask";
                iReqNo++;
                var endTask = new {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = this.strStation,
                    task_id = this.strAgvTaskNo,
                    task_sequence = iReqNo
                };
                string strRequest = this.objAgv.AGVHttpRequest(this.strAgvFunction, strRequestType, JsonConvert.SerializeObject(endTask));
                #endregion

                if (string.IsNullOrEmpty(strRequest)) {
                    MessageBox.Show("请重新点击结束任务！");
                    return;
                }

                // 删除已出库数据，并且更新储位表
                objStorageOut_AGV.StrageOut_OutStockSave(strWerks, strLgort, dtScanMatnr);
                MessageBox.Show("结束任务成功!");

                //删除料架调度中间表数据
                if (objStorageOut_AGV.DeleteAGVShelf(strWerks, strLgort, this.strAgvTaskNo, this.strStation)) {
                    dtMatnrAgv.Rows.Clear();
                    dtScanMatnr.Rows.Clear();
                    ShowMatnrDataGridView();
                    ShowMblnrDataGridView();
                }

                btnRefresh_Click(null, null);
                btnRefresh.Enabled = true;

            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString() + "<-End()");
            }
        }

        public void turnOffLight(string strLocation) {
            var data = new {
                plant = strWerks,//厂区
                storage = strLgort,//仓别
                action = "DEL",//操作行为(ADD - 入储；DEL - 出储)
                location = strLocation,//储位
                location_detail = new List<object> { }
            };

            this.dtScanMatnr.AsEnumerable().Where(w => w.Field<string>("LOCAT").Equals(strLocation))
                .Select(s => new {
                    pn = s.Field<string>("MATNR"),
                    vendor_code = s.Field<string>("LIFNR"),
                    date_code = s.Field<string>("DACOD"),
                    quantity = Convert.ToInt32(s.Field<string>("MENGE"))
                }).ToList()
                .ForEach(f => {
                    data.location_detail.Add(f);
                });

            // 将对象转换为 JSON 字符串
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            string respLight = objAgv.AGVHttpRequest(this.strAgvFunction, "shelfMaterialRenewal", json);
            //MessageBox.Show(respLight);
        }

        private void StorageOut_OnlineOut_AGV_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = !AllowToClose;

            if (e.Cancel = !AllowToClose) {
                MessageBox.Show("数据处理中！请不要关闭窗口!");
            }
        }

        #region 判断智能料架中间表中是否有数据，若无则增加表数据
        private void InitAGVData() {
            DataTable dtAGVData = objStorageOut_AGV.GetAGVData(strWerks, strLgort, this.strStation);
            if (dtAGVData.Rows.Count == 0) {
                MessageBox.Show("无AGV数据");
                return;
            }
        }
        #endregion

        #region 判断是否已完成全部作业
        private bool complateAllWork() {
            bool blResult = true;
            if (dtScanMatnr.Select("SCAN = 'N'").Count() > 0) {
                blResult = false;
                MessageBox.Show("料架未完成作业！");
            }
            return blResult;
        }

        private bool complateCurrentShelf() {
            bool blResult = true;
            if (dtScanMatnr.Select(" MARNO = '" + this.strShelf + "' and scan = 'N' ").Count() > 0) {
                blResult = false;
                MessageBox.Show("料架未匹配完成!");
            }
            return blResult;
        }
        #endregion

        public void SetErrNotice() {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
        }

        public void SetOKNotice() {
            SoundPlayer sp = new SoundPlayer(@"Sound\BIU.wav");
            sp.Play();
        }

        private void button1_Click(object sender, EventArgs e) {
            string strMatnr = this.dtMatnr.Rows[0]["MATNR"].ToString();
            string strDacod = this.dtMatnr.Rows[0]["DACOD"].ToString();
            string strLocod = this.dtMatnr.Rows[0]["LOCOD"].ToString();
            string strVendor = this.dtMatnr.Rows[0]["LIFNR"].ToString();
            string strLOCAT = this.dtMatnr.Rows[0]["LOCAT"].ToString();
            ShowErrorNumber(strMatnr, strDacod, strLocod, strVendor, strLOCAT);
        }
    }
}
