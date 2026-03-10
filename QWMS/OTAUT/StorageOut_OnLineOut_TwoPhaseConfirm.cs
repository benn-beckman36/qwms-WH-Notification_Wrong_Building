using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Text;

namespace QWMS
{
    public partial class StorageOut_OnLineOut_TwoPhaseConfirm : Form
    {
        #region 變數宣告

        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strComcd = "";
        private DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();

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
        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }
        public string Grrno
        {
            get
            {
                return this.txtGrrno.Text.Trim();
            }
            set
            {
                this.txtGrrno.Text = value;
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

        public StorageOut_OnLineOut_TwoPhaseConfirm()
        {
            InitializeComponent();
        }

        public StorageOut_OnLineOut_TwoPhaseConfirm(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;

            try
            {
                //檢查權限
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                if (!objStorageOut.CheckAuthority())
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

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region cmbWerks_SelectedIndexChanged
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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
                DataGridViewTextBoxColumn dgvcItemnum = new DataGridViewTextBoxColumn();
                dgvcItemnum.DataPropertyName = "ITEMNUM";
                dgvcItemnum.HeaderText = "ITEMNUM";
                dgvcItemnum.ReadOnly = true;
                dgvcItemnum.Width = 60;
                this.dgvData.Columns.Add(dgvcItemnum);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 60;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "LGORT";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 60;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 60;
                this.dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No.";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 60;
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

                DataGridViewTextBoxColumn dgvcbkqty = new DataGridViewTextBoxColumn();
                dgvcbkqty.DataPropertyName = "BKQTY";
                dgvcbkqty.HeaderText = "QWMS Stock Qty";
                dgvcbkqty.Width = 90;
                dgvcbkqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcbkqty);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 90;
                dgvcBlace.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcBarcode = new DataGridViewTextBoxColumn();
                dgvcBarcode.DataPropertyName = "BARCODE";
                dgvcBarcode.HeaderText = "BarCode";
                dgvcBarcode.Width = 140;
                dgvcBarcode.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBarcode);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.stsWarning.Text = "";
                Grrno = txtGrrno.Text.ToString();
                DataTable dtProduct = new DataTable();
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtData = objStorageData.QueryOnLineInData_Grrno(Grrno, "Simulation");

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    lblWerks.Enabled = false;
                    lblLgort.Enabled = false;
                    lblGrrno.Enabled = false;
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.btnQuery.Enabled = false;
                    this.btnPrint.Enabled = true;
                }

                this.txtGrrno.Enabled = false;
                ShowDataGrid();

                //判斷此筆出庫單是否已經處理過
                if (int.Parse(dtData.Rows[0]["PRTYP"].ToString()) == 1)
                {
                    txtGrrno.Enabled = false;
                    btnSave.Enabled = false;
                    stsWarning.Text = "這筆出庫單已經扣過帳，請勿重複扣帳!!";
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        #endregion

        #region btnSave_Click

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

            DataTable dtStorage = new DataTable();
            DataTable dtQmsData = new DataTable();
            DataTable dtSapInventory = new DataTable();
            string strStatus = "";
            string strSort = "";

            DataRow drRow;
            DataRow[] combineRow;
            DataTable dtCombineStorage = new DataTable();
            ArrayList alAllCombine = new ArrayList();
            StringBuilder sbCombineIndex = new StringBuilder();

            try
            {
                #region 將同料號、Cost Center的數量作加總
                int intCombineRndQty = 0;
                int intCombineTotalQty = 0;
                dtCombineStorage = dtData.Clone();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    #region 每次Combine的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and GRPID='" + dtData.Rows[i]["GRPID"].ToString() + "'");

                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                    {
                        intCombineRndQty = 0;
                        intCombineTotalQty = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtData.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineRndQty += Int32.Parse(combineRow[j]["RNDQTY"].ToString());
                            intCombineTotalQty += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }

                        drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                        drRow["GRPID"] = dtData.Rows[i]["GRPID"].ToString();
                        drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                        drRow["RNDQTY"] = intCombineRndQty.ToString();
                        drRow["ALQTY"] = intCombineTotalQty.ToString();
                        drRow["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString();

                        dtCombineStorage.Rows.Add(drRow);
                    }
                }
                #endregion

                #region 1.查詢和比對QMS傳過來的數量

                //查詢QMS傳過來之dispatch的料號和數量
                dtQmsData = objStorageData.QueryQMSDispatchData(dtCombineStorage.Rows[0]["GRPID"].ToString());
                if (dtQmsData.Rows.Count > 0)
                {
                    if (dtQmsData.Rows.Count != dtCombineStorage.Rows.Count)
                    {
                        MessageBox.Show("QMS給的dispatch資料與QWMS的發料資料筆數不ㄧ致，請確認!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SetbtnSaveException();
                        return;
                    }

                    //Sort dtData(QWMS的發料資料)
                    strSort = "GRPID,KOSTL,MATNR";
                    dtCombineStorage = CommonInfo.SortDataTable(dtCombineStorage, strSort);

                    //Sort dtQmsData(QMS給的dispatch資料)
                    strSort = "GRPID,COSCT,MATNR";
                    dtQmsData = CommonInfo.SortDataTable(dtQmsData, strSort);

                    //比對QMS傳過來的料號及數量和QWMS要發料的數量是否相符合
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        if (dtCombineStorage.Rows[i]["ALQTY"].ToString().Trim().ToUpper() != dtQmsData.Rows[i]["MENGE"].ToString().Trim().ToUpper())
                        {
                            MessageBox.Show("料號: '" + dtCombineStorage.Rows[i]["MATNR"].ToString() + "' 之QMS dispatch的數量與QWMS的實際發料數量有差異，請確認!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SetbtnSaveException();
                            return;
                        }
                    }
                }
                else
                {
                    stsWarning.Text = "QMS的需求數量尚未Dispatch與傳送給QWMS系統，請確認!!";
                    SetbtnSaveException();
                    return;
                }

                #endregion

                #region  2.查詢SAP庫存數量

                //if (dtData.Rows.Count > 0)
                //{
                //    dtSapInventory = objStorageIn.QuerySMTSapInventory(dtData);  //查詢SAP庫存數量
                //    if (dtSapInventory.Rows.Count <= 0)
                //    {
                //        MessageBox.Show("查不到SAP的庫存數量資料，請確認!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        SetbtnSaveException();
                //        return;
                //    }

                //    for (int i = 0; i < dtSapInventory.Rows.Count; i++)
                //    {
                //        if (double.Parse(dtSapInventory.Rows[i]["ALQTY"].ToString().Trim()) > double.Parse(dtSapInventory.Rows[i]["SAPQY"].ToString().Trim()))
                //        {
                //            MessageBox.Show("料號: " + dtSapInventory.Rows[i]["MATNR"].ToString().Trim() + " 之SAP庫存數量小於QWMS出庫數量，請確認!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //            SetbtnSaveException();
                //            return;
                //        }
                //    }
                //}

                #endregion

                this.btnRefresh.Enabled = false;
                this.btnExit.Enabled = false;
                Application.DoEvents();

                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                //if (objStorageOut.AddOnLineOutConfirmData(dtData))
                {
                    #region 3.SAP扣帳

                    try
                    {
                        //呼叫SAP RFC扣帳
                        dtStorage = objStorageIn.InsertSMTSapInventory(dtCombineStorage);  
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SetbtnSaveException();
                        return;
                    }

                    strStatus = dtStorage.Rows[0]["SAP_RETURN"].ToString().Trim();
                    switch (strStatus)
                    {
                        case "S":
                            stsWarning.Text = "QWMS扣帳成功，SAP Return OK!!";
                            this.btnSave.Enabled = false;
                            this.btnRefresh.Enabled = true;
                            this.btnExit.Enabled = true;
                            return;
                        case "D":
                            stsWarning.Text = "SAP重複扣帳(SAP Return: " + strStatus + ")，請確認!!";
                            goto err;
                        case "F":
                            stsWarning.Text = "SAP扣帳失敗(SAP Return: " + strStatus + ")，請確認!!";
                            goto err;
                        case "G":
                            stsWarning.Text = "SAP扣帳錯誤(SAP Return: " + strStatus + "_One Group ID can only contain one plant)，請確認!!";
                            goto err;
                    }

                err:
                    SetbtnSaveException();
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    return;

                    #endregion
                //}
                //else
                //{
                    stsWarning.Text = "QWMS庫存除帳失敗(Save Fail)，請確認!! " + objStorageIn.ERRMSG;
                    SetbtnSaveException();
                    MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.btnSave.Enabled = false;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetbtnSaveException();
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.gbHeader.Enabled = true;
            this.lblGrrno.Enabled = true;
            this.lblWerks.Enabled = true;
            this.lblLgort.Enabled = true;
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.txtGrrno.Text = "";
            this.txtGrrno.Enabled = true;
            this.txtGrrno.Text = "";
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.lblData.Text = "0 records";
            this.btnSave.Enabled = false;
            this.btnQuery.Enabled = true;
            this.btnPrint.Enabled = false;
            this.txtGrrno.Focus();
            strWerks = "";
            strLgort = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region txtGrrno_KeyDown
        private void txtGrrno_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
        #endregion

        #region btnPrint_Click
        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            DataSet dsData = new DataSet();
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

            dsData = objStorageData.QueryOnLineOutGrrno_DateCode(dtData, txtGrrno.Text.Trim());

            DataTable dtPrint = new DataTable();
            dtData = dsData.Tables[0].Copy();
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

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DateCode_New", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        #region txtGrrno_DoubleClick
        private void txtGrrno_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                StorageOut_GoodIssueReport_Select objStorageOut_GoodIssueReport_Select = new StorageOut_GoodIssueReport_Select(UserData, Progid, Werks, Lgort);
                objStorageOut_GoodIssueReport_Select.ShowDialog();
                txtGrrno.Text = objStorageOut_GoodIssueReport_Select.Grrno;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region dgvData_RowHeaderMouseClick
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string strLocat = "";
                string strMblnr = "";
                string strMatnr = "";
                string strZeile = "";
                string strInsmk = "";
                string strCharg = "";
                string strBarcode = "";
                int intMenge = 0;

                strLocat = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[3].Value);
                strMblnr = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[4].Value);
                strZeile = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[5].Value);
                strMatnr = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[6].Value);
                strInsmk = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[7].Value);
                strCharg = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[8].Value);
                intMenge = int.Parse(Convert.ToString(dgvData.Rows[e.RowIndex].Cells[10].Value));
                strBarcode = Convert.ToString(dgvData.Rows[e.RowIndex].Cells[12].Value);

                StorageOut_OnLineOut_TwoPhaseConfirm_Modify objStorageOut_OnLineOut_TwoPhaseConfirm_Modify = new StorageOut_OnLineOut_TwoPhaseConfirm_Modify(UserData, Werks, Lgort, Progid, strLocat, strMatnr, strInsmk, strCharg, strMblnr, strZeile, strBarcode, dtData);
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Locat = strLocat;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Mblnr = strMblnr;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Matnr = strMatnr;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Zeile = strZeile;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Barcode = strBarcode;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.Menge = intMenge;
                objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.ShowDialog();
                dtData = objStorageOut_OnLineOut_TwoPhaseConfirm_Modify.QtyData;
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 按鈕設定-儲存中
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        #endregion

        #region 按鈕設定-儲存失敗
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
            this.btnRefresh.Enabled = true;
            this.btnExit.Enabled = true;
        }
        #endregion
    }
}
