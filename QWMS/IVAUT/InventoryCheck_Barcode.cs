using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Media;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace QWMS
{
    public partial class InventoryCheck_Barcode : Form
    {
        #region 參數定義
        UserInfo UserData = new UserInfo();

        public DataTable dtShwData, dtData;
        public DataTable dtLocat; //盤點儲位
        private Counting objCounting;
        private FileInfo fi;
        private StreamWriter sw;


        private int intIndex;    //紀錄搜尋料號索引值
        private Int32 intQty;    //刷barcode數量


        private bool bolIndex;   //紀錄搜尋狀態
        private bool bolQty;     //輸入數量狀態
        bool bolExistLocat;

        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private string strProgid = "";
        private string strComcd = "";


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
        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
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
        #endregion

        public InventoryCheck_Barcode(UserInfo varUserData, string strProgid)
        {
            //載入登入者資訊
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            //載入視窗
            InitializeComponent();
            try
            {
                QCI.QWMS.Counting objCounting = new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

                //檢查權限
                if (!objCounting.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    //初始設定元件
                    SetInitial();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Button 事件
        #region btnConfirm_Click(點擊Confirm按鈕)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //參數定義
            bool bolQuery = true;
            //檢查是否選擇廠區
            if (bolQuery == true && cmbWerks.SelectedIndex == -1)
            {
                stsWarning.Text = "提示訊息：請選擇廠區 !!";
                Sound.Play(@"Sound\OO.wav");
                bolQuery = false;
            }
            //檢查是否選擇倉別
            if (bolQuery == true && cmbLgort.SelectedIndex == -1)
            {
                stsWarning.Text = "提示訊息：請選擇倉別 !!";
                Sound.Play(@"Sound\OO.wav");
                bolQuery = false;
            }
            //依儲位盤點
            if (bolQuery == true && rdoChkLocat.Checked == true)
            {
                //檢查是否輸入儲位
                if (txtLocatStart.Text.ToString().Trim() == "" && txtLocatEnd.Text.ToString().Trim() == "")
                {
                    stsWarning.Text = "提示訊息：請輸入Location !!";
                    Sound.Play(@"Sound\OO.wav");
                    bolQuery = false;
                }
            }
            if (bolQuery == true)
            {
                try
                {
                    Counting objCounting = new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                    //DataTable  dtData = objCounting QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr, strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strIsCombine,strRegon,strVendorCode,strCombineParts,strRmano)
                    dtData = objCounting.QueryDetailCountingData("", txtLocatStart.Text.ToString().Trim(), txtLocatEnd.Text.ToString().Trim(), "", "", "", "", "", "", "", "Y", "", "", "", "", "", false, "", false,"");
                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "提示訊息：查無庫存 !!";
                        Sound.Play(@"Sound\OO.wav");
                    }
                    else
                    {
                        DataColumn cSelect = new DataColumn("Select", typeof(bool));
                        DataColumn cBalance = new DataColumn("Blance", typeof(int));
                        DataColumn cAlqty = new DataColumn("Alqty", typeof(int));
                        dtData.Columns.Add(cSelect);
                        dtData.Columns.Add(cBalance); //紀錄目前剩餘盤點數量
                        dtData.Columns.Add(cAlqty);   //紀錄目前盤點數量

                        //初始值
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["Blance"] = Int32.Parse(dtData.Rows[i]["MENGE"].ToString());
                            dtData.Rows[i]["Alqty"] = 0;
                            dtData.Rows[i]["Select"] = false;
                        }


                        dtShwData = dtData.Copy();
                        //設定gv顯示欄位
                        ShowDataGrid("all");
                        //如果盤點儲位儲位大於一個儲位
                        if ((dtData.DefaultView.ToTable(true, "LOCAT")).Rows.Count > 1)
                        {
                            //必須要先刷儲位
                            stsWarning.Text = "多個盤點儲位，請先刷盤點儲位!!";
                            SetKeyLocation();
                        }
                        else
                        {
                            txtLocat.Enabled = false;
                            chkDiff.Enabled = true;
                            SetKeyPartno();
                        }
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }

            }
        }
        #endregion

        #region btnExit_Click(點擊Exit按鈕)
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region btnRefresh_Click(點擊Refresh按鈕)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInitial();
        }
        #endregion
        #region btnPrint_Click(點擊Print按鈕)
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
                //更新目前盤點狀況 reqty
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                bool bolresult = objCounting.UpdateCountingData(dtData);
                //宣告Report類別
                ReportPrint objReportPrint = new ReportPrint(UserData, "CONTRASTBARCODE", dtShwData);
                //使用此張　ContrastReportBarcode.rpt
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
        #region btnDownload_Click(下載按鈕觸發事件)
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
        #endregion

        #region Radio Button 事件
        #region rdoChkLocat_CheckedChanged(選取依Location盤點選紐)
        private void rdoChkLocat_CheckedChanged(object sender, EventArgs e)
        {
            SetLocat();
        }
        #endregion
        #region rdoChkLgort_CheckedChanged(選取依Storage盤點選紐)
        private void rdoChkLgort_CheckedChanged(object sender, EventArgs e)
        {
            SetLocat();
        }
        #endregion
        #region rdoMatnr_Click(選取依料號排序盤點庫存)
        private void rdoMatnr_Click(object sender, EventArgs e)
        {
            dtShwData.DefaultView.Sort = "MATNR ASC, LOCAT ASC";
            //this.dgvData.Columns["MATNR"].SortMode = DataGridViewColumnSortMode.Automatic;
            txtPartNo.Text = "";
            txtQty.Text = "";
        }
        #endregion
        #region rdoLocat_Click(選取依儲位排序盤點庫存)
        private void rdoLocat_Click(object sender, EventArgs e)
        {
            dtShwData.DefaultView.Sort = "LOCAT ASC , MATNR ASC";
            //this.dgvData.Columns["LOCAT"].SortMode = DataGridViewColumnSortMode.Automatic;
            txtPartNo.Text = "";
            txtQty.Text = "";
        }
        #endregion
        #endregion

        #region 元件Enable 狀態
        #region SetInitial(設定元件初始狀態)
        private void SetInitial()
        {
            this.gbFunction.Enabled = true;
            //單選紐
            this.rdoChkLgort.Enabled = true;
            this.rdoChkLocat.Enabled = true;

            this.rdoChkLgort.Checked = false;
            this.rdoChkLocat.Checked = false;

            rdoMatnr.Enabled = false;
            rdoLocat.Enabled = false;
            //下拉式選單
            cmbLgort.Enabled = false;
            cmbWerks.Enabled = false;
            txtLocatStart.Enabled = false;
            txtLocatEnd.Enabled = false;
            //文字區塊
            txtPartNo.Enabled = false;
            txtQty.Enabled = false;
            txtLocat.Enabled = false;
            //核取方塊
            chkDiff.Enabled = false;
            //按鈕
            btnPrint.Enabled = false;
            btnDownload.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnConfirm.Enabled = false;

            cmbWerks.Items.Clear();
            cmbLgort.Items.Clear();
            if (dtData != null)
            {
                dtData.Clear();
            }
            if (dtShwData != null)
            {
                dtShwData.Clear();
            }
            this.gbFunction.Enabled = true;
            this.rdoChkLgort.Enabled = true;
            this.rdoChkLocat.Enabled = true;
            txtLocat.Text = "";
            txtPartNo.Text = "";
            txtQty.Text = "";
            txtLocatStart.Text = "";
            txtLocatEnd.Text = "";
        }
        #endregion
        #region SetLocat(設定依儲位盤點狀態)
        private void SetLocat()
        {
            gbHeader.Enabled = true;
            //單選紐
            rdoChkLgort.Enabled = false;
            rdoChkLocat.Enabled = false;
            rdoMatnr.Enabled = false;
            rdoLocat.Enabled = false;
            //下拉式選單
            cmbLgort.Enabled = true;
            cmbWerks.Enabled = true;
            if (rdoChkLocat.Checked == true)
            {
                txtLocatStart.Enabled = true;
                txtLocatEnd.Enabled = true;
            }
            if (rdoChkLgort.Checked == true)
            {
                txtLocatStart.Enabled = false;
                txtLocatEnd.Enabled = false;
            }
            //文字區塊
            txtPartNo.Enabled = false;
            txtQty.Enabled = false;
            //核取方塊
            chkDiff.Enabled = false;
            //按鈕
            btnConfirm.Enabled = true;
            btnPrint.Enabled = false;
            btnDownload.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;

            ShowDdlWerks();
            ShowDdlLgort();
        }
        #endregion
        #region SetQuery(設定查詢庫存狀態)
        private void SetQuery()
        {
            gbHeader.Enabled = true;
            //單選紐
            rdoChkLgort.Enabled = false;
            rdoChkLocat.Enabled = false;
            rdoMatnr.Enabled = false;
            rdoLocat.Enabled = false;
            //下拉式選單
            cmbLgort.Enabled = false;
            cmbWerks.Enabled = false;
            txtLocatStart.Enabled = false;
            txtLocatEnd.Enabled = false;
            //文字區塊
            txtPartNo.Enabled = false;
            txtQty.Enabled = false;
            //核取方塊
            chkDiff.Enabled = false;
            //按鈕
            btnPrint.Enabled = false;
            btnDownload.Enabled = true;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
        }
        #endregion
        #region SetKeyPartno(設定刷料號狀態)
        private void SetKeyPartno()
        {
            gbHeader.Enabled = true;
            //單選紐
            rdoChkLgort.Enabled = false;
            rdoChkLocat.Enabled = false;
            rdoMatnr.Enabled = true;
            rdoLocat.Enabled = true;
            //下拉式選單
            cmbLgort.Enabled = false;
            cmbWerks.Enabled = false;
            txtLocatStart.Enabled = false;
            txtLocatEnd.Enabled = false;
            //文字區塊
            txtPartNo.Enabled = true;
            this.txtPartNo.Focus();
            txtQty.Enabled = false;
            //核取方塊
            chkDiff.Enabled = true;
            //按鈕e;
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;

            txtPartNo.Text = "";
            txtQty.Text = "";
        }
        #endregion
        #region SetKeyQty(設定刷數量狀態)
        private void SetKeyQty()
        {
            gbHeader.Enabled = true;
            //單選紐
            rdoChkLgort.Enabled = false;
            rdoChkLocat.Enabled = false;
            rdoMatnr.Enabled = false;
            rdoLocat.Enabled = false;
            //下拉式選單
            cmbLgort.Enabled = false;
            cmbWerks.Enabled = false;
            txtLocatStart.Enabled = false;
            txtLocatEnd.Enabled = false;
            //文字區塊
            txtPartNo.Enabled = false;
            txtQty.Enabled = true;
            this.txtQty.Focus();
            txtLocat.Enabled = true;
            //核取方塊
            chkDiff.Enabled = true;
            //按鈕
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
        }
        #endregion
        #region SetKeyLocation(設定刷儲位狀態)
        private void SetKeyLocation()
        {
            gbHeader.Enabled = true;
            //單選紐
            rdoChkLgort.Enabled = false;
            rdoChkLocat.Enabled = false;
            rdoMatnr.Enabled = true;
            rdoLocat.Enabled = true;
            //下拉式選單
            cmbLgort.Enabled = false;
            cmbWerks.Enabled = false;
            txtLocatStart.Enabled = false;
            txtLocatEnd.Enabled = false;
            //文字區塊
            txtPartNo.Enabled = false;
            txtQty.Enabled = false;
            txtLocat.Enabled = true;
            this.txtLocat.Focus();
            //核取方塊
            chkDiff.Enabled = true;
            //按鈕
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
        }
        #endregion
        #endregion

        #region 顯示處理
        #region ShowStatusData(顯示登入訊息)
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion
        #region ShowDdlWerks(設定廠區選項)
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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
        #region ShowDdlLgort(設定倉別選項)
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
        #endregion
        #region ShowDataGrid(顯示gv內容)
        private void ShowDataGrid(string strTyp)
        {


            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();

            //複製DataTable數值
            dtShwData = dtData.Copy();
            //dtShwData.Columns.Remove("MANDT");
            //dtShwData.Columns.Remove("COMCD");
            //dtShwData.Columns.Remove("WERKS");
            //dtShwData.Columns.Remove("LGORT");
            dtShwData.Columns.Remove("RMANO");
            dtShwData.Columns.Remove("BKQTY");
            dtShwData.Columns.Remove("MBLNR");

            if (strTyp == "all")
            {

            }
            if (strTyp == "different")
            {
                for (int i = 0; i < dtShwData.Rows.Count; i++)
                {
                    if (dtShwData.Rows[i]["Blance"].ToString().Trim() == "0")
                    {
                        //移除盤點無誤的部分
                        dtShwData.Rows.RemoveAt(i);
                    }
                }
            }


            this.dgvData.DataSource = dtShwData;
            lblTotalCount.Text = dtShwData.Rows.Count.ToString() + " records";

            DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
            dgvcSelect.DataPropertyName = "Select";
            dgvcSelect.HeaderText = "Select";
            dgvcSelect.Width = 40;
            dgvcSelect.ReadOnly = true;
            this.dgvData.Columns.Add(dgvcSelect);

            //儲位
            DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
            dgvcLocat.DataPropertyName = "LOCAT";
            dgvcLocat.HeaderText = "Location";
            dgvcLocat.ReadOnly = true;
            dgvcLocat.Width = 80;
            dgvData.Columns.Add(dgvcLocat);
            //料號
            DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
            dgvcMatnr.DataPropertyName = "MATNR";
            dgvcMatnr.HeaderText = "Part No";
            dgvcMatnr.ReadOnly = true;
            dgvcMatnr.Width = 90;
            dgvData.Columns.Add(dgvcMatnr);
            //料號說明
            //料號說明欄位
            DataGridViewTextBoxColumn dgvcMaktx = new DataGridViewTextBoxColumn();
            dgvcMaktx.DataPropertyName = "MAKTX";
            dgvcMaktx.HeaderText = "Part# Description";
            dgvcMaktx.Width = 200;
            dgvcMaktx.ReadOnly = true;
            dgvData.Columns.Add(dgvcMaktx);
            //PO Number
            DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
            dgvcEbeln.DataPropertyName = "EBELN";
            dgvcEbeln.HeaderText = "PO No.";
            dgvcEbeln.ReadOnly = true;
            dgvcEbeln.Width = 100;
            dgvData.Columns.Add(dgvcEbeln);
            //Customer P/N
            //Stock
            DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
            dgvcInsmk.DataPropertyName = "INSMK";
            dgvcInsmk.HeaderText = "Stock";
            dgvcInsmk.ReadOnly = true;
            dgvcInsmk.Width = 30;
            dgvData.Columns.Add(dgvcInsmk);
            //Version
            DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
            dgvcCharg.DataPropertyName = "CHARG";
            dgvcCharg.HeaderText = "Version";
            dgvcCharg.ReadOnly = true;
            dgvcCharg.Width = 50;
            dgvData.Columns.Add(dgvcCharg);
            //RMA No
            //Qty
            DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
            dgvcMenge.DataPropertyName = "MENGE";
            dgvcMenge.HeaderText = "Qty";
            dgvcMenge.ReadOnly = true;
            dgvcMenge.Width = 200;
            dgvData.Columns.Add(dgvcMenge);

            //盤點數量
            DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
            dgvcAlqty.DataPropertyName = "Alqty";
            dgvcAlqty.HeaderText = "盤點數";
            dgvcAlqty.Width = 200;
            this.dgvData.Columns.Add(dgvcAlqty);

            //剩餘數
            DataGridViewTextBoxColumn dgvcBlance = new DataGridViewTextBoxColumn();
            dgvcBlance.DataPropertyName = "Blance";
            dgvcBlance.HeaderText = "剩餘數";
            dgvcBlance.Width = 200;
            this.dgvData.Columns.Add(dgvcBlance);
            //Vendor
            //Store In Date
        }
        #endregion
        #region ShowCurrentDataGrid(顯示目前gridview內容)
        private void ShowCurrentDataGrid(int varQty)
        {
            //增加盤點數
            dtData.Rows[intIndex]["Alqty"] = (Convert.ToInt32(dtData.Rows[intIndex]["Alqty"].ToString()) + varQty);
            //減少剩餘數
            dtData.Rows[intIndex]["Blance"] = (Convert.ToInt32(dtData.Rows[intIndex]["Blance"].ToString()) - varQty);
        }
        #endregion
        #endregion

        #region TextBox 事件
        #region txtPartNo_KeyDown(刷料號觸發事件)
        private void txtPartNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            intIndex = 0;
            bolIndex = false;
            bolExistLocat = false;
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (txtPartNo.Text.ToString().Trim() == "")
                {
                    stsWarning.Text = "提示訊息：請刷料號 !!";
                    Sound.Play(@"Sound\OO.wav");
                }
                else
                {
                    #region 刷儲位
                    dtLocat = dtData.DefaultView.ToTable(true, "LOCAT");
                    for (int i = 0; i < dtLocat.Rows.Count; i++)
                    {
                        if (txtPartNo.Text.ToString().Trim() == dtLocat.Rows[i]["LOCAT"].ToString().Trim())
                        {
                            bolExistLocat = true;
                            txtLocat.Text = txtPartNo.Text.ToString().Trim();
                            Sound.Play(@"Sound\ready.wav");
                            txtPartNo.Text = "";
                            txtQty.Text = "";
                            SetKeyPartno();
                        }
                    }
                    #endregion
                    #region 刷料號
                    if (bolExistLocat == false)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i]["MATNR"].ToString().Trim() == txtPartNo.Text.ToString().Trim() && txtLocat.Text.ToString().Trim() == dtData.Rows[i]["LOCAT"].ToString().Trim())
                            {
                                //紀錄此料索引值
                                intIndex = i;
                                bolIndex = true;
                                break;
                            }
                        }
                        if (bolIndex == false)
                        {
                            Sound.Play(@"Sound\OO.wav");
                            stsWarning.Text = "提示訊息：無此料號 !!";
                            txtPartNo.Text = "";
                        }
                        else
                        {
                            SetKeyQty();
                            this.dgvData.Rows[intIndex].Selected = true;
                            stsWarning.Text = "提示訊息：料號 " + txtPartNo.Text.ToString().Trim() + "  !!";
                            Sound.Play(@"Windows Ding.wav");
                        }
                    }

                    #endregion
                }

            }
        }
        #endregion
        #region txtQty_KeyDown(刷數量觸發事件)
        private void txtQty_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bolQty = false;
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {

                    intQty = Convert.ToInt32(Convert.ToDouble(txtQty.Text.Trim()));
                    if (intQty <= 0)
                    {
                        Sound.Play(@"Sound\OO.wav");
                        stsWarning.Text = "錯誤訊息：刷入值為" + txtQty.Text + " ，請重刷數量";
                        txtQty.Text = "";
                    }
                    else //確定數量值正常
                    {
                        Sound.Play(@"Windows Ding.wav");
                        SetKeyPartno();
                        ShowCurrentDataGrid(intQty); //計算目前刷料數量並更新gv
                        ShowDataGrid("all");
                    }
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\OO.wav");
                    stsWarning.Text = "錯誤訊息：" + txtQty.Text.ToString().Trim() + " ，請重刷數量";
                    txtQty.Text = "";
                }

            }
        }
        #endregion
        #region txtLocat_KeyDown(刷儲位觸發事件)
        private void txtLocat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            bolExistLocat = false;
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (txtLocat.Text.ToString().Trim() != "")
                {
                    //檢查是否有此儲位
                    DataTable dtLocat = dtData.DefaultView.ToTable(true, "LOCAT");
                    for (int i = 0; i < dtLocat.Rows.Count; i++)
                    {
                        if (txtLocat.Text.ToString().Trim() == dtLocat.Rows[i]["LOCAT"].ToString().Trim())
                        {
                            bolExistLocat = true;
                            break;
                        }
                    }
                    if (bolExistLocat == true)
                    {
                        //刷料號
                        Sound.Play(@"Sound\ready.wav");
                        stsWarning.Text = "提示訊息：請刷料號";
                        //指標跑到刷料號
                        txtPartNo.Focus();
                        //設定刷料號狀態
                        SetKeyPartno();
                    }
                    //此儲位不存在
                    else
                    {
                        Sound.Play(@"Sound\OO.wav");
                        stsWarning.Text = "錯誤訊息：儲位 '" + txtLocat.Text.ToString().Trim() + "' 不存在";
                        txtLocat.Text = "";
                    }
                }
                else
                {
                    //若沒有輸入儲位
                    Sound.Play(@"Sound\OO.wav");
                    stsWarning.Text = "錯誤訊息：請刷儲位";
                    txtLocat.Text = "";
                }
            }

        }
        #endregion
        #endregion

        #region ComboBox 事件
        #region cmbWerks_SelectedIndexChanged(選取廠區觸發事件)
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion
        #region cmbLgort_SelectedIndexChanged(選取倉別觸發事件)
        private void cmbLgort_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
        }
        #endregion
        #endregion

        #region CheckBox 事件
        #region chkDiff_CheckedChanged(點選差異顯示觸發事件)
        private void chkDiff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiff.Checked == true)
            {
                ShowDataGrid("different");
            }
            else
            {
                ShowDataGrid("all");
            }
        }
        #endregion

        #endregion

        #region CountingResult2File(寫入Excel資料表)

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tLocation\tPart No.\tDescription\tStock\tVersion\tQty\tPhysical Qty\tDifference";
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
                    strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["Alqty"].ToString() + "\t";
                    strLine += dtData.Rows[i]["Blance"].ToString();

                    sw.WriteLine(strLine);
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
    }
}
