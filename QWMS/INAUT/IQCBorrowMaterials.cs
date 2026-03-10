using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Text.RegularExpressions;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Timers;

namespace QWMS
{
    public partial class IQCBorrowMaterials : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();
        private string strBorrowIQCID = "";
        private string strBorrowWHID = "";
        string strFlag = "";

        #region 設定變數
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
        public string Locat
        {
            get
            {
                return this.txtLocat.Text.Trim();
            }
            set
            {
                this.txtLocat.Text = value;
            }
        }
        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
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
        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }
        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
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
        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
            }
        }
        public string BorrowIQCID
        {
            get
            {
                return strBorrowIQCID;
            }
            set
            {
                strBorrowIQCID = value;
            }
        }

        public string BorrowWHID
        {
            get
            {
                return strBorrowWHID;
            }
            set
            {
                strBorrowWHID = value;
            }
        }
        #endregion

        public IQCBorrowMaterials(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            Data = dtData;
            try
            {


                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
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
                    this.cmbStatus.Enabled = false;
                }

                dtData.Columns.Add("Select", typeof(bool));
                dtData.Columns.Add("MANDT", Type.GetType());
                dtData.Columns.Add("WERKS", Type.GetType());
                dtData.Columns.Add("LGORT", Type.GetType());
                dtData.Columns.Add("LOCAT", Type.GetType());
                dtData.Columns.Add("MATNR", Type.GetType());
                dtData.Columns.Add("MENGE", Type.GetType());//数量
                dtData.Columns.Add("BRQTY", Type.GetType());//借料数量
                dtData.Columns.Add("OldBRQTY", Type.GetType());//借料前数量
                dtData.Columns.Add("RTQTY", Type.GetType());//还料数量
                dtData.Columns.Add("OldRTQTY", Type.GetType());//还料前数量
                dtData.Columns.Add("MBLNR", Type.GetType());
                dtData.Columns.Add("LIFNR", Type.GetType());
                dtData.Columns.Add("BWART", Type.GetType());
                dtData.Columns.Add("IQCID", Type.GetType());//借料IQC工号
                dtData.Columns.Add("WHID", Type.GetType());//借料仓管工号
                dtData.Columns.Add("RTIQCID", Type.GetType());//还料IQC工号
                dtData.Columns.Add("RTWHID", Type.GetType());//还料仓管工号
                dtData.Columns.Add("BUDAT", Type.GetType());//入账时间
                dtData.Columns.Add("CRDAT", Type.GetType());//SAP单据建档时间
                dtData.Columns.Add("BRDAT", Type.GetType());//借出时间
                dtData.Columns.Add("RTDAT", Type.GetType());//还料时间
                dtData.Columns.Add("REMAK", Type.GetType());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //				stsWarning.Text = ex.Message;
            }
        }

        #region 顯示狀態列
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region 廠區下拉選單
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
        #endregion

        #region 選取廠區
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
        }

        #region 倉別下拉選單
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
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
                    cmbLgort.Items.Add("");
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

        # region 顯示入庫資料
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "Select";
                chk.HeaderText = "";//Multilanguage.Instance.ResourceManager.GetString("TransactionID");
                chk.Name = "Select";
                chk.Frozen = true;
                chk.Width = 30;
                this.dgvData.Columns.Add(chk);
                //this.dgvData.MultiSelect = false;
                this.dgvData.SelectionMode = DataGridViewSelectionMode.CellSelect;

               // DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
               // dgvcSelect.DataPropertyName = "Select";
               // dgvcSelect.HeaderText = "Select";
               //// dgvcSelect.ReadOnly = true;
               // dgvcSelect.Width = 50;
               // dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.Name = "MBLNR";
                dgvcMBLNR.HeaderText = "单据号";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.Width = 100;
                dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.Name = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                dgvcWERKS.Width = 60;
                dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.Name = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                dgvcLGORT.Width = 60;
                dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.Name = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 60;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.Name = "BWART";
                dgvcBWART.HeaderText = "异动代码";
                dgvcBWART.ReadOnly = true;
                dgvcBWART.Width = 60;
                dgvData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.HeaderText = "料号数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBRQTY = new DataGridViewTextBoxColumn();
                dgvcBRQTY.DataPropertyName = "BRQTY";
                dgvcBRQTY.Name = "BRQTY";
                dgvcBRQTY.HeaderText = "借出数量";
                dgvcBRQTY.Width = 60;
                dgvData.Columns.Add(dgvcBRQTY);
                dgvcBRQTY.ReadOnly = true;

                DataGridViewTextBoxColumn dgvcBRDAT = new DataGridViewTextBoxColumn();
                dgvcBRDAT.DataPropertyName = "BRDAT";
                dgvcBRDAT.Name = "BRDAT";
                dgvcBRDAT.HeaderText = "借出时间";
                dgvcBRDAT.ReadOnly = true;
                dgvcBRDAT.Width = 160;
                dgvData.Columns.Add(dgvcBRDAT);

                DataGridViewTextBoxColumn dgvcRTQTY = new DataGridViewTextBoxColumn();
                dgvcRTQTY.DataPropertyName = "RTQTY";
                dgvcRTQTY.Name = "RTQTY";
                dgvcRTQTY.HeaderText = "还料数量";
                dgvcRTQTY.Width = 60;
                dgvData.Columns.Add(dgvcRTQTY);
                dgvcRTQTY.ReadOnly = true;

                DataGridViewTextBoxColumn dgvcRTDAT = new DataGridViewTextBoxColumn();
                dgvcRTDAT.DataPropertyName = "RTDAT";
                dgvcRTDAT.Name = "RTDAT";
                dgvcRTDAT.HeaderText = "还料时间";
                dgvcRTDAT.ReadOnly = true;
                dgvcRTDAT.Width = 160;
                dgvData.Columns.Add(dgvcRTDAT);

                DataGridViewTextBoxColumn dgvcIQCID = new DataGridViewTextBoxColumn();
                dgvcIQCID.DataPropertyName = "IQCID";
                dgvcIQCID.Name = "IQCID";
                dgvcIQCID.HeaderText = "借料IQC工号";
                dgvcIQCID.ReadOnly = true;
                dgvcIQCID.Width = 100;
                dgvData.Columns.Add(dgvcIQCID);

                DataGridViewTextBoxColumn dgvcWHID = new DataGridViewTextBoxColumn();
                dgvcWHID.DataPropertyName = "WHID";
                dgvcWHID.Name = "WHID";
                dgvcWHID.HeaderText = "借料仓管工号";
                dgvcWHID.ReadOnly = true;
                dgvcWHID.Width = 100;
                dgvData.Columns.Add(dgvcWHID);

                DataGridViewTextBoxColumn dgvcrRTIQCID = new DataGridViewTextBoxColumn();
                dgvcrRTIQCID.DataPropertyName = "RTIQCID";
                dgvcrRTIQCID.Name = "RTIQCID";
                dgvcrRTIQCID.HeaderText = "还料IQC工号";
                dgvcrRTIQCID.ReadOnly = true;
                dgvcrRTIQCID.Width = 100;
                dgvData.Columns.Add(dgvcrRTIQCID);

                DataGridViewTextBoxColumn dgvcRTWHID = new DataGridViewTextBoxColumn();
                dgvcRTWHID.DataPropertyName = "RTWHID";
                dgvcRTWHID.Name = "RTWHID";
                dgvcRTWHID.HeaderText = "还料仓管工号";
                dgvcRTWHID.ReadOnly = true;
                dgvcRTWHID.Width = 100;
                dgvData.Columns.Add(dgvcRTWHID);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.ReadOnly = true;
                dgvcLIFNR.Width = 80;
                dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcINDAT = new DataGridViewTextBoxColumn();
                dgvcINDAT.DataPropertyName = "BUDAT";
                dgvcINDAT.Name = "BUDAT";
                dgvcINDAT.HeaderText = "入账时间";
                dgvcINDAT.ReadOnly = true;
                dgvcINDAT.Width = 100;
                dgvData.Columns.Add(dgvcINDAT);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "REMAK";
                dgvcREMAK.Name = "REMAK";
                dgvcREMAK.HeaderText = "备注";
                dgvcREMAK.ReadOnly = false;
                dgvcREMAK.Width = 100;
                dgvData.Columns.Add(dgvcREMAK);


                //dtData = CommonInfo.SortDataTable(dtData, "MATNR,MENGE");
                //dtSource = CommonInfo.SortDataTable(dtSource, "MATNR,MENGE");
                dgvData.DataSource = dtData;
                lblCount.Text = Data.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Double click 儲位欄位
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                Type = "All";
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
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
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
                StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, txtMblnr.Text.Trim(), "ONLINE_GR", Insmk);
                objStorageIn_SapDataSelect.ShowDialog();

                txtMblnr.Text = objStorageIn_SapDataSelect.Mblnr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #region 確認廠區、倉別、儲位、SAP单据
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.dtData.Rows.Clear();
                this.dgvData.DataSource = null;
                string strBarcode = txtMatnr.Text.ToString().Trim().ToUpper();
                string[] str = strBarcode.Split(';');
                string strInDate = dtpIndat.Value.ToString("yyyyMMdd");
                string strToDate = dtpToDate.Value.ToString("yyyyMMdd");
                string strLifnr = "";
                string strMatnr = "";
                string strStatus = "";
                string strType = "";
                int menge = 0;
                DataRow[] drAllSource;
                DataRow drRow;
                DataRow drOldRow;
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                strLocat = txtLocat.Text.Trim();

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (Werks == "" )
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (strInDate == "")
                {
                    stsWarning.Text = "Date can't be empty!!";
                    return;
                }

                //取得Sttyp及Lotyp

                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                else
                {
                    stsWarning.Text = "Can't find the storage data!!";
                    return;
                }

                if (cmbStatus.Items[cmbStatus.SelectedIndex].ToString() == "已还")
                {
                    strStatus = "Y";
                }
                else if (cmbStatus.Items[cmbStatus.SelectedIndex].ToString() == "未还")
                {
                    strStatus = "N";
                }
                else
                {
                    strStatus = "All";
                }

                if (rdbBR.Checked)
                {
                    strType = "BR";
                }
                else if (rdbRT.Checked)
                {
                    strType = "RT";
                }
                else
                {
                    strType = "Query";
                }

                if (rdbBR.Checked || rdbRT.Checked)
                {
                    if (string.IsNullOrEmpty(txtBorrowIQCID.Text.ToString()) || string.IsNullOrEmpty(txtBorrowWHID.Text.ToString()))
                    {
                        MessageBox.Show("请先刷卡！");
                        txtMatnr.Text = string.Empty;
                        txtBorrowIQCID.Focus();
                        return;
                    }
                }

                if (str.Length >= 5)
                {
                    strLifnr = str[2].ToString().Trim();
                    strMatnr = str[0].ToString().Trim();
                    menge = Convert.ToInt32(str[4].ToString().Trim());
                }
                else if(str.Length == 1 && rdQuery.Checked)
                {
                    strMatnr = txtMatnr.Text.ToString().Trim().ToUpper();
                }
                else
                {
                    MessageBox.Show("请刷入正确的数据！");
                    txtMatnr.Text = string.Empty;
                    txtMatnr.Focus();
                    return;
                }

                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                 DataTable dtSource = objSapData.ListSapInData_IQCBorrow(txtMblnr.Text.ToString(), Locat, strLifnr, strInDate,strToDate, strStatus,strMatnr, strType);

                if (dtSource.Rows.Count>0)
                {
                    if (strStatus == "All")
                    {
                        if (strType == "RT")
                        {
                            drAllSource = dtSource.Select("BRQTY>0");
                            if (drAllSource.Length==0)
                            {
                                MessageBox.Show("无相关数据!");
                                txtMatnr.Text = string.Empty;
                                txtMatnr.Focus();
                                return;
                            }
                        }
                        else
                        {
                            drAllSource = dtSource.Select(" BRQTY<>RTQTY OR BRQTY=RTQTY OR BRQTY=0 OR RTQTY=0");
                        }
                        for (int i = 0; i < drAllSource.Length; i++)
                        {
                            if (drAllSource.Length == 1 && ((rdbBR.Checked && (Convert.ToInt32(drAllSource[i]["BRQTY"].ToString()) + menge) <= Convert.ToInt32(drAllSource[i]["MENGE"].ToString())) || (rdbRT.Checked && menge <= Convert.ToInt32(drAllSource[i]["BRQTY"].ToString()))))
                            {
                                drRow = dtData.NewRow();
                                drRow["Select"] = true;
                                drRow["MANDT"] = UserData.Client;
                                drRow["WERKS"] = Werks;
                                drRow["LGORT"] = drAllSource[i]["LGORT"].ToString();
                                drRow["MBLNR"] = drAllSource[i]["MBLNR"].ToString();
                                drRow["BWART"] = drAllSource[i]["BWART"].ToString();
                                drRow["LIFNR"] = drAllSource[i]["LIFNR"].ToString();
                                drRow["LOCAT"] = drAllSource[i]["GRLOC"].ToString();
                                drRow["MATNR"] = drAllSource[i]["MATNR"].ToString();
                                drRow["MENGE"] = drAllSource[i]["MENGE"].ToString();
                                if (rdbBR.Checked)
                                {
                                    drRow["BRQTY"] = (Convert.ToInt32(drAllSource[i]["BRQTY"].ToString()) + menge).ToString();
                                    drRow["RTQTY"] = drAllSource[i]["RTQTY"].ToString();
                                }
                                else if (rdbRT.Checked && (drAllSource[i]["IQCID"].ToString()!="" || drAllSource[i]["WHID"].ToString()!=""))
                                {
                                    //SumRTQTY += menge;
                                    drRow["BRQTY"] = (Convert.ToInt32(drAllSource[i]["BRQTY"].ToString()) - menge).ToString();
                                    drRow["RTQTY"] = (Convert.ToInt32(drAllSource[i]["RTQTY"].ToString()) + menge).ToString();
                                }
                                else
                                {
                                    drRow["BRQTY"] = drAllSource[i]["BRQTY"].ToString();
                                    drRow["RTQTY"] = drAllSource[i]["RTQTY"].ToString();
                                }
                                drRow["OldBRQTY"] = drAllSource[i]["BRQTY"].ToString();
                                drRow["OldRTQTY"] = drAllSource[i]["RTQTY"].ToString();
                                drRow["IQCID"] = drAllSource[i]["IQCID"].ToString();
                                drRow["WHID"] = drAllSource[i]["WHID"].ToString();
                                drRow["RTIQCID"] = drAllSource[i]["RTIQCID"].ToString();
                                drRow["RTWHID"] = drAllSource[i]["RTWHID"].ToString();
                                drRow["BUDAT"] = drAllSource[i]["BUDAT"].ToString();
                                drRow["CRDAT"] = drAllSource[i]["CRDAT"].ToString();
                                drRow["BRDAT"] = drAllSource[i]["BRDAT"].ToString();
                                drRow["RTDAT"] = drAllSource[i]["RTDAT"].ToString();
                                drRow["REMAK"] = drAllSource[i]["REMAK"].ToString();
                                dtData.Rows.Add(drRow);
                            }
                            else
                            {
                                drRow = dtData.NewRow();
                                drRow["Select"] = false;
                                drRow["MANDT"] = UserData.Client;
                                drRow["WERKS"] = Werks;
                                drRow["LGORT"] = drAllSource[i]["LGORT"].ToString();
                                drRow["MBLNR"] = drAllSource[i]["MBLNR"].ToString();
                                drRow["BWART"] = drAllSource[i]["BWART"].ToString();
                                drRow["LIFNR"] = drAllSource[i]["LIFNR"].ToString();
                                drRow["LOCAT"] = drAllSource[i]["GRLOC"].ToString();
                                drRow["MATNR"] = drAllSource[i]["MATNR"].ToString();
                                drRow["MENGE"] = drAllSource[i]["MENGE"].ToString();
                                drRow["BRQTY"] = drAllSource[i]["BRQTY"].ToString();
                                drRow["RTQTY"] = drAllSource[i]["RTQTY"].ToString();
                                drRow["OldBRQTY"] = drAllSource[i]["BRQTY"].ToString();
                                drRow["OldRTQTY"] = drAllSource[i]["RTQTY"].ToString();
                                drRow["IQCID"] = drAllSource[i]["IQCID"].ToString();
                                drRow["WHID"] = drAllSource[i]["WHID"].ToString();
                                drRow["RTIQCID"] = drAllSource[i]["RTIQCID"].ToString();
                                drRow["RTWHID"] = drAllSource[i]["RTWHID"].ToString();
                                drRow["BUDAT"] = drAllSource[i]["BUDAT"].ToString();
                                drRow["CRDAT"] = drAllSource[i]["CRDAT"].ToString();
                                drRow["BRDAT"] = drAllSource[i]["BRDAT"].ToString();
                                drRow["RTDAT"] = drAllSource[i]["RTDAT"].ToString();
                                drRow["REMAK"] = drAllSource[i]["REMAK"].ToString();
                                dtData.Rows.Add(drRow);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            drRow = dtData.NewRow();
                            drRow["Select"] = false;
                            drRow["MANDT"] = UserData.Client;
                            drRow["WERKS"] = Werks;
                            drRow["LGORT"] = dtSource.Rows[i]["LGORT"].ToString();
                            drRow["MBLNR"] = dtSource.Rows[i]["MBLNR"].ToString();
                            drRow["BWART"] = dtSource.Rows[i]["BWART"].ToString();
                            drRow["LIFNR"] = dtSource.Rows[i]["LIFNR"].ToString();
                            drRow["LOCAT"] = dtSource.Rows[i]["GRLOC"].ToString();
                            drRow["MATNR"] = dtSource.Rows[i]["MATNR"].ToString();
                            drRow["MENGE"] = dtSource.Rows[i]["MENGE"].ToString();
                            drRow["BRQTY"] = dtSource.Rows[i]["BRQTY"].ToString();
                            drRow["RTQTY"] = dtSource.Rows[i]["RTQTY"].ToString();
                            drRow["OldBRQTY"] = dtSource.Rows[i]["BRQTY"].ToString();
                            drRow["OldRTQTY"] = dtSource.Rows[i]["RTQTY"].ToString();
                            drRow["IQCID"] = dtSource.Rows[i]["IQCID"].ToString();
                            drRow["WHID"] = dtSource.Rows[i]["WHID"].ToString();
                            drRow["RTIQCID"] = dtSource.Rows[i]["RTIQCID"].ToString();
                            drRow["RTWHID"] = dtSource.Rows[i]["RTWHID"].ToString();
                            drRow["BUDAT"] = dtSource.Rows[i]["BUDAT"].ToString();
                            drRow["CRDAT"] = dtSource.Rows[i]["CRDAT"].ToString();
                            drRow["BRDAT"] = dtSource.Rows[i]["BRDAT"].ToString();
                            drRow["RTDAT"] = dtSource.Rows[i]["RTDAT"].ToString();
                            drRow["REMAK"] = dtSource.Rows[i]["REMAK"].ToString();
                            dtData.Rows.Add(drRow);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("无相关数据!");
                    return;
                }
                ShowDataGrid();
                //ColorStats();
                if (rdbBR.Checked || rdbRT.Checked)
                {
                    this.btnSave.Enabled = true;
                }
                this.chkSelect.Enabled = true;
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
        }
        #endregion

        #region 儲存入庫資料
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                SetbtnSaveProcess();
                stsWarning.Text = ""; 

                dtData = (DataTable)dgvData.DataSource;

                DataTable dtSelectData = new DataTable();
                dtSelectData = dtData.Clone();

                dtSelectData = dtData.Select(" Select=true ").CopyToDataTable();
                var strMatnrs = dtSelectData.AsEnumerable().Select(row => row.Field<string>("MATNR")).Distinct().ToList();
                if (strMatnrs.Count > 1)
                {
                    stsWarning.Text = "一次只可借/还一个料号!";
                    return;
                }
                for (int i = 0; i < dtSelectData.Rows.Count; i++)
                {
                    if (rdbBR.Checked)
                    {
                        if (!CheckIsNumber(dtSelectData.Rows[i]["BRQTY"].ToString()) ||
                            Convert.ToInt32(dtSelectData.Rows[i]["BRQTY"].ToString()) <= 0)
                        {
                            MessageBox.Show("借料数量必须为数字且大于0!!");
                            return;
                        }
                        if (Convert.ToInt32(dtSelectData.Rows[i]["BRQTY"].ToString()) > Convert.ToInt32(dtSelectData.Rows[i]["MENGE"].ToString()))
                        {
                            MessageBox.Show("借料数量不能大于单据数量!!");
                            return;
                        }
                        //if (dtSelectData.Rows[i]["IQCID"].ToString() == "" || dtSelectData.Rows[i]["WHID"].ToString() == "")
                        //{
                        //    MessageBox.Show("工号不可为空!!");
                        //    return;
                        //}
                        if (Convert.ToInt32(dtSelectData.Rows[i]["BRQTY"].ToString()) == Convert.ToInt32(dtSelectData.Rows[i]["OldBRQTY"].ToString()))
                        {
                            MessageBox.Show("未刷入不可借料!!");
                            return;
                        }
                        if (txtBorrowWHID.Text.Length< 8 || txtBorrowIQCID.Text.Length < 8)
                        {
                            MessageBox.Show("请刷卡！！，且要有8位长度的工号！！");
                            return;
                        }
                        else
                        {
                            dtSelectData.Rows[i]["IQCID"] = txtBorrowIQCID.Text.Substring(0, 8).ToString();
                            dtSelectData.Rows[i]["WHID"] = txtBorrowWHID.Text.Substring(0, 8).ToString();
                        }
                    }
                    if (rdbRT.Checked)
                    {
                        if (string.IsNullOrEmpty(dtSelectData.Rows[i]["IQCID"].ToString()) || dtSelectData.Rows[i]["IQCID"].ToString() == "")//借料IQC与仓管栏位为空即还未借过料
                        {
                            if (string.IsNullOrEmpty(dtSelectData.Rows[i]["WHID"].ToString()) || dtSelectData.Rows[i]["WHID"].ToString() == "")
                            {
                                MessageBox.Show("未借料不可还料!!");
                                return;
                            }
                        }
                        if (!CheckIsNumber(dtSelectData.Rows[i]["RTQTY"].ToString()) || Convert.ToInt32(dtSelectData.Rows[i]["RTQTY"].ToString()) <= 0)
                        {
                            MessageBox.Show("还料数量必须为数字且大于0!!");
                            return;
                        }
                        //if (Convert.ToInt32(dtSelectData.Rows[i]["RTQTY"].ToString()) > Convert.ToInt32(dtSelectData.Rows[i]["BRQTY"].ToString()))
                        //{
                        //    MessageBox.Show("还料数量必须小于或等于借料数量!!");
                        //    return;
                        //}
                        if (Convert.ToInt32(dtSelectData.Rows[i]["OldBRQTY"].ToString()) == 0)
                        {
                            MessageBox.Show("借料数量为0不可还料!!");
                            return;
                        }
                        if (Convert.ToInt32(dtSelectData.Rows[i]["RTQTY"].ToString()) == Convert.ToInt32(dtSelectData.Rows[i]["OldRTQTY"].ToString()))
                        {
                            MessageBox.Show("未刷入不可还料!!");
                            return;
                        }

                        if (txtBorrowWHID.Text.Length < 8 || txtBorrowIQCID.Text.Length < 8)
                        {
                            MessageBox.Show("请刷卡！！，且要有8位长度的工号！！");
                            return;
                        }
                        else
                        {
                            dtSelectData.Rows[i]["RTIQCID"] = txtBorrowIQCID.Text.Substring(0, 8).ToString();
                            dtSelectData.Rows[i]["RTWHID"] = txtBorrowWHID.Text.Substring(0, 8).ToString();
                        }
                    }
                }

                if (dtSelectData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

                if (objStorageIn.IQCBorrowMaterialsData(dtSelectData,strFlag))
                {
                    stsWarning.Text = "Add OK!!";
                    //this.btnConfirm_Click(null, null);
                    this.QueryData(dtSelectData);
                    this.btnSave.Enabled = false;
                    this.btnConfirm.Enabled = false;
                    this.txtMatnr.Focus();
                    return;
                }
                else
                {
                    stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
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
        #endregion

        #region 重設查詢條件
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.txtLocat.Text = "";
            this.dtData.Rows.Clear();
            //this.dtSource.Rows.Clear();
            this.dgvData.DataSource = null;
            this.txtLifnr.Text = "";
            this.txtMblnr.Text = "";
            this.btnSave.Enabled = false;
            this.lblCount.Text = "0 records";
            this.txtBorrowIQCID.Text = "";
            this.txtBorrowWHID.Text = "";
            this.lblWHCheck.Text = "验证WH身份";
            this.lblIQCCheck.Text = "验证IQC身份";
            this.chkSelect.Enabled = false;
            this.chkSelect.Checked = false;
            if (rdQuery.Checked)
            {
                this.btnConfirm.Enabled = true;
            }
            else
            {
                this.btnConfirm.Enabled = false;
            }
            this.rdbBR.Checked = false;
            this.rdbRT.Checked = false;
            //this.SumRTQTY = 0;
            strWerks = "";
            strLgort = "";
            strInsmk = "";
            strFlag = "";
        }
        #endregion

        #region 離開此功能
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 設定panel的大小與位置
        private void IQCBorrowMaterials_Resize(object sender, System.EventArgs e)
        {
            panel4.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel4.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;

        }
        #endregion

        # region 確認視窗是否已經打開
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
        #endregion

        #region  获取HR信息，身份验证
        private void txtBorrowIQCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.CheckID("IQC", txtBorrowIQCID.Text.Trim()))
                {
                    lblIQCCheck.Text = "IQC身份验证OK";
                    txtBorrowWHID.Focus();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if ((bool)dtData.Rows[i]["Select"] == true)
                        {
                            if (rdbBR.Checked)
                            {
                                dtData.Rows[i]["IQCID"] = BorrowIQCID;
                            }
                            if(rdbRT.Checked)
                            {
                                dtData.Rows[i]["RTIQCID"] = BorrowIQCID;
                            }
                        }
                    }
                }
                else
                {
                    lblIQCCheck.Text = "请按Enter键验证IQC身份";
                    return;
                }
            }
        }

        private void txtBorrowWHID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.CheckID("WH", txtBorrowWHID.Text.Trim()))
                {
                    lblWHCheck.Text = "WH身份验证OK";
                    txtMatnr.Focus();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if ((bool)dtData.Rows[i]["Select"]==true)
                        {
                            if (rdbBR.Checked)
                            {
                                dtData.Rows[i]["WHID"] = BorrowWHID;
                            }
                            if (rdbRT.Checked)
                            {
                                dtData.Rows[i]["RTWHID"] = BorrowWHID;
                            }
                        }
                    }
                }
                else
                {
                    lblWHCheck.Text = "请按Enter键验证WH身份";
                    return;
                }
            }
        }

        private bool CheckID(string strFlag, string strCardNo)
        {
            bool bl = false;
            string strNam = "";
            string strTextFail = strFlag + "刷卡验证失败";
            DataTable dtHR = GetHRData(strCardNo.Trim());
            if (dtHR.Rows.Count > 0)
            {
                strNam = dtHR.Rows[0]["depnam"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("HR无" + strNam + "资料", strFlag + "刷卡验证失败", MessageBoxButtons.OK);
                return bl;
            }
            MessageBox.Show("该员工的部门是" + strNam );
           // string strOwnerFail = "该员工的部门是" + strNam + "，非" + strFlag + "人员，不能进行还料作业！";
            if (strFlag == "IQC")
            {
                //校验IQC的人
                BorrowIQCID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的IQC还料人工号
                txtBorrowIQCID.Text = BorrowIQCID + "：" + strNam;//IQC刷卡显示工号和部门信息
                // Regex reg = new Regex(@"品保中心进料品保处进料检验部进料检验");
                //Regex reg = new Regex(@"进料");
                //if (!reg.IsMatch(strNam))
                //{
                //    //MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                //    //txtBorrowIQCID.Text = "";
                //    //return bl;
                //}
            }
            else if (strFlag == "WH")
            {
                //校验WH的人
                BorrowWHID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的WH接收人工号
                txtBorrowWHID.Text = BorrowWHID + "：" + strNam;//WH刷卡显示工号和部门信息
                //Regex reg = new Regex(@"物流服务部物流服务");
                Regex reg = new Regex(@"物流服务");
                if (!reg.IsMatch(strNam))
                {
                    //MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                    //txtBorrowWHID.Text = "";
                    //return bl;
                }
            }
            bl = true;//部门名称存在变化，故不作验证，只记录就好
            return bl;
        }
      
        public DataTable GetHRData(string varUserID)
        {
            try
            {
                string varSite = "";
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                DataTable dtEmployeeData = new DataTable();
                Admin objAdmin = new Admin(UserData, Progid);
                switch (UserData.CompanyCode)
                {
                    case "9110":
                        varSite = "QCMC";
                        break;
                    case "9210":
                        varSite = "QCMC";
                        break;
                    case "9700":
                        varSite = "CSMC";
                        break;
                    default:
                        varSite = "QSMC";
                        break;
                }
                
                //带出员工信息
                dtEmployeeData = objAdmin.GetEmployeeData(varUserID);
                if (dtEmployeeData.Rows.Count > 0)
                {
                    dt = dtEmployeeData;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 是否為數字
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }
        #endregion

        #region RadioButton
        private void rdbBR_CheckedChanged(object sender, EventArgs e)
        {
            this.btnConfirm.Enabled = true;
            strFlag = "Borrow";
            txtMatnr.Text = string.Empty;
            stsWarning.Text = string.Empty;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.SelectedIndex = 0;
            this.btnDownload.Enabled = false;
            this.dtpIndat.Value = DateTime.Now.AddDays(-15); 
            this.lblCount.Text = "0 records";
            this.txtBorrowIQCID.Text = "";
            this.txtBorrowWHID.Text = "";
            this.lblWHCheck.Text = "验证WH身份";
            this.lblIQCCheck.Text = "验证IQC身份";
        }

        private void rdbRT_CheckedChanged(object sender, EventArgs e)
        {
            this.btnConfirm.Enabled = true;
            strFlag = "Return";
            txtMatnr.Text = string.Empty;
            stsWarning.Text = string.Empty;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.SelectedIndex = 0;
            this.btnDownload.Enabled = false;
            this.dtpIndat.Value = DateTime.Now.AddDays(-15);
            this.lblCount.Text = "0 records";
            this.txtBorrowIQCID.Text = "";
            this.txtBorrowWHID.Text = "";
            this.lblWHCheck.Text = "验证WH身份";
            this.lblIQCCheck.Text = "验证IQC身份";
        }

        private void rdQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.btnConfirm.Enabled = true;
            txtMatnr.Text = string.Empty;
            stsWarning.Text = string.Empty;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.cmbStatus.Enabled = true;
            this.btnDownload.Enabled = true;
            this.dtpIndat.Value = DateTime.Now.AddDays(-30);
            this.lblCount.Text = "0 records";
            this.txtBorrowIQCID.Text = "";
            this.txtBorrowWHID.Text = "";
            this.lblWHCheck.Text = "验证WH身份";
            this.lblIQCCheck.Text = "验证IQC身份";
            this.btnSave.Enabled = false;
        }
        #endregion 

        #region 加一个checkbox控件跟datagridview组合来实现全选反选功能
        #region
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                    //dgvRequests.Rows[i].Cells[0].Value = 1;
                }
            }
            else
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                    //dgvRequests.Rows[i].Cells[0].Value = 0;
                }
            }
        }
        #endregion

        #region 重绘全选表头
        //重绘表头
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();

            System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {
            }

            protected override void Paint(System.Drawing.Graphics graphics,
                System.Drawing.Rectangle clipBounds,
                System.Drawing.Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates dataGridViewElementState,
                object value,
                object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle,
                    advancedBorderStyle, paintParts);
                Point p = new Point();
                Size s = CheckBoxRenderer.GetGlyphSize(graphics,
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);
                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;
                if (_checked)
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.CheckedNormal;
                else
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.UncheckedNormal;
                CheckBoxRenderer.DrawCheckBox
                (graphics, checkBoxLocation, _cbState);
            }


            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
                if (p.X >= checkBoxLocation.X && p.X <=
                    checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <=
                    checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;
                    if (OnCheckBoxClicked != null)
                    {
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }
                base.OnMouseClick(e);
            }

        }

        public delegate void CheckBoxClickedHandler(bool state);

        public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
        {
            bool isChecked;

            public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
            {
                isChecked = bChecked;
            }
            public bool Checked
            {
                get { return isChecked; }
                set { isChecked = value; }
            }
        }

        #endregion

        #endregion

        private void txtMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                string strBarcode = txtMatnr.Text.ToString().Trim().ToUpper();
                string[] str = strBarcode.Split(';');
                string strMatnr = "";
                int menge = 0;
                int sumMenge = 0;
                if (str.Length>=5)
                {
                    strMatnr = str[0].ToString().Trim();
                    menge = Convert.ToInt32(str[4].ToString().Trim());
                }
                else if(str.Length == 1 && rdQuery.Checked)
                {
                    strMatnr = txtMatnr.Text.ToString().Trim().ToUpper();
                }
                else
                {
                    MessageBox.Show("请刷入正确的数据！");
                    txtMatnr.Text = string.Empty;
                    txtMatnr.Focus();
                    return;
                }
                if (this.btnSave.Enabled == false && dtData.Rows.Count != 0 && dgvData.Rows.Count != 0)
                {
                    btnConfirm_Click(null, null);
                    txtMatnr.Text = string.Empty;
                    return;
                }
                if (rdbBR.Checked || rdbRT.Checked)
                {
                    if (string.IsNullOrEmpty(txtBorrowIQCID.Text.ToString()) || string.IsNullOrEmpty(txtBorrowWHID.Text.ToString()))
                    {
                        MessageBox.Show("请先刷卡！");
                        txtMatnr.Text = string.Empty;
                        txtBorrowIQCID.Focus();
                        return;
                    }
                }
                if (rdbBR.Checked)
                {
                    if (dtData.Rows.Count ==0)
                    {
                        btnConfirm_Click(null, null);
                        txtMatnr.Text = string.Empty;
                    }
                    else
                    {
                        #region 判断刷入数据
                        if (dtData.Rows.Count == 1)
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dtData.Rows[i]["MENGE"]) > Convert.ToInt32(dtData.Rows[i]["BRQTY"]))
                                {
                                    if (strMatnr != dtData.Rows[i]["MATNR"].ToString())
                                    {
                                        stsWarning.Text = "刷入数据料号与当前数据不符，请确认！";
                                        txtMatnr.Text = string.Empty;
                                        return;
                                    }
                                    if ((Convert.ToInt32(dtData.Rows[i]["BRQTY"]) + menge) > Convert.ToInt32(dtData.Rows[i]["MENGE"]))
                                    {
                                        stsWarning.Text = "借料数量累加不能大于单据数量，请确认！";
                                        txtMatnr.Text = string.Empty;
                                        return;
                                    }
                                    else
                                    {
                                        dgvData.Rows[i].Cells["BRQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) + menge).ToString();
                                    }
                                }
                                //if(Convert.ToInt32(dtData.Rows[i]["MENGE"]) == Convert.ToInt32(dtData.Rows[i]["BRQTY"]))
                                //{
                                //    stsWarning.Text = "借料数量等于单据数量，无法再次刷入！";
                                //    return;
                                //}
                            }
                        }
                        else
                        {
                            DataRow[] drSelect = dtData.Select("Select=true");
                            if (drSelect.Length == 0)
                            {
                                stsWarning.Text = "请勾选需要刷入的数据！";
                                txtMatnr.Text = string.Empty;
                                return;
                            }
                            bool bolMore = true;
                            foreach (DataRow dr in drSelect)
                            {
                                if (menge <= (Convert.ToInt32(dr["MENGE"].ToString()) - Convert.ToInt32(dr["BRQTY"].ToString())))
                                {
                                    bolMore = false;
                                }
                            }
                            if (drSelect.Length > 1 && bolMore)
                            {
                                foreach (DataRow dr in drSelect)
                                {
                                    sumMenge += (Convert.ToInt32(dr["MENGE"].ToString()) - Convert.ToInt32(dr["BRQTY"].ToString()));
                                }
                                if (sumMenge>=menge)
                                {
                                    for (int i = 0; i < dtData.Rows.Count; i++)
                                    {
                                        if (dtData.Rows[i]["Select"].ToString() == "True")
                                        {
                                            if (menge > Convert.ToInt32(dtData.Rows[i]["MENGE"]))
                                            {
                                                dgvData.Rows[i].Cells["BRQTY"].Value = Convert.ToInt32(dtData.Rows[i]["MENGE"]).ToString();
                                                menge -= Convert.ToInt32(dtData.Rows[i]["MENGE"]);
                                            }
                                            else
                                            {
                                                dgvData.Rows[i].Cells["BRQTY"].Value = (menge+Convert.ToInt32(dgvData.Rows[i].Cells["BRQTY"].Value)).ToString();
                                                menge = 0;
                                            }
                                        }
                                    }
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                                else
                                {
                                    stsWarning.Text = "刷入数量不等于单据数量，请确认！";
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtData.Rows.Count; i++)
                                {
                                    if (dtData.Rows[i]["Select"].ToString() == "True" && (Convert.ToInt32(dtData.Rows[i]["MENGE"].ToString()) > Convert.ToInt32(dtData.Rows[i]["BRQTY"].ToString())))
                                    {
                                        if (strMatnr != dtData.Rows[i]["MATNR"].ToString())
                                        {
                                            stsWarning.Text = "刷入数据料号与当前数据不符，请确认！";
                                            txtMatnr.Text = string.Empty;
                                            return;
                                        }
                                        if ((Convert.ToInt32(dtData.Rows[i]["BRQTY"].ToString()) + menge) > Convert.ToInt32(dtData.Rows[i]["MENGE"].ToString()))
                                        {
                                            stsWarning.Text = "刷入数量大于单据数量，请确认！";
                                            txtMatnr.Text = string.Empty;
                                            return;
                                        }
                                        else
                                        {
                                            dgvData.Rows[i].Cells["BRQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) + menge).ToString();
                                            txtMatnr.Text = string.Empty;
                                            return;
                                            //if (dtData.Rows[i]["BRQTY"].ToString() == dtData.Rows[i]["MENGE"].ToString())
                                            //{
                                            //    return;
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        //ColorStats();
                    }
                }
                if (rdbRT.Checked)
                {
                    if (dtData.Rows.Count == 0)
                    {
                        btnConfirm_Click(null, null);
                        txtMatnr.Text = string.Empty;
                    }
                    else
                    {
                        if (dtData.Rows.Count == 1)
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (string.IsNullOrEmpty(dtData.Rows[i]["IQCID"].ToString()) || dtData.Rows[i]["IQCID"].ToString() == "")
                                {
                                    stsWarning.Text = "未借料不可还料!!";
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                                if (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) > 0)
                                {
                                    if (strMatnr != dtData.Rows[i]["MATNR"].ToString())
                                    {
                                        stsWarning.Text = "刷入数据料号与当前数据不符，请确认！";
                                        txtMatnr.Text = string.Empty;
                                        return;
                                    }
                                    if (menge > Convert.ToInt32(dtData.Rows[i]["BRQTY"]))
                                    {
                                        stsWarning.Text = "还料数量累加不能大于借料数量，请确认！";
                                        txtMatnr.Text = string.Empty;
                                        return;
                                    }
                                    else
                                    {
                                        dgvData.Rows[i].Cells["BRQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) - menge).ToString();
                                        dgvData.Rows[i].Cells["RTQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["RTQTY"]) + menge).ToString();
                                    }
                                }
                                else
                                {
                                    stsWarning.Text = "借料数量为0，无需再刷还料！";
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            DataRow[] drSelect = dtData.Select("Select=true");
                            if (drSelect.Length == 0)
                            {
                                stsWarning.Text = "请勾选需要刷入的数据！";
                                txtMatnr.Text = string.Empty;
                                return;
                            }
                            bool bolMore = true;
                            foreach (DataRow dr in drSelect)
                            {
                                if (menge <= Convert.ToInt32(dr["BRQTY"].ToString()))
                                {
                                    bolMore = false;
                                }
                            }
                            if (drSelect.Length>1 && bolMore)
                            {
                                foreach (DataRow dr in drSelect)
                                {
                                    sumMenge += Convert.ToInt32(dr["BRQTY"].ToString());
                                }
                                if (sumMenge >= menge)
                                {
                                    for (int i = 0; i < dtData.Rows.Count; i++)
                                    {
                                        if (dtData.Rows[i]["Select"].ToString() == "True")
                                        {
                                            if (menge> Convert.ToInt32(dtData.Rows[i]["BRQTY"]))
                                            {
                                                dgvData.Rows[i].Cells["BRQTY"].Value = "0";
                                                dgvData.Rows[i].Cells["RTQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["RTQTY"]) + Convert.ToInt32(dtData.Rows[i]["OldBRQTY"])).ToString();
                                                menge -= Convert.ToInt32(dtData.Rows[i]["OldBRQTY"]);
                                            }
                                            else
                                            {
                                                dgvData.Rows[i].Cells["BRQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) - menge).ToString();
                                                dgvData.Rows[i].Cells["RTQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["RTQTY"]) + menge).ToString();
                                                menge = 0;
                                            }
                                        }
                                    }
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                                else
                                {
                                    stsWarning.Text = "刷入数量不等于借料数量，请确认！";
                                    txtMatnr.Text = string.Empty;
                                    return;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtData.Rows.Count; i++)
                                {
                                    if (dtData.Rows[i]["Select"].ToString() == "True")
                                    {
                                        if (Convert.ToInt32(dtData.Rows[i]["BRQTY"].ToString()) > 0)
                                        {
                                            if (strMatnr != dtData.Rows[i]["MATNR"].ToString())
                                            {
                                                stsWarning.Text = "刷入数据料号与当前数据不符，请确认！";
                                                txtMatnr.Text = string.Empty;
                                                return;
                                            }
                                            if (string.IsNullOrEmpty(dtData.Rows[i]["IQCID"].ToString()) || dtData.Rows[i]["IQCID"].ToString() == "")
                                            {
                                                stsWarning.Text = "未借料不可还料!!";
                                                txtMatnr.Text = string.Empty;
                                                return;
                                            }
                                            if (menge > Convert.ToInt32(dtData.Rows[i]["BRQTY"]))
                                            {
                                                stsWarning.Text = "刷入数量大于借出数量，请确认！";
                                                txtMatnr.Text = string.Empty;
                                                return;
                                            }
                                            else
                                            {
                                                dgvData.Rows[i].Cells["BRQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["BRQTY"]) - menge).ToString();
                                                dgvData.Rows[i].Cells["RTQTY"].Value = (Convert.ToInt32(dtData.Rows[i]["RTQTY"]) + menge).ToString();
                                                txtMatnr.Text = string.Empty;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            stsWarning.Text = "借料数量为0，无需再刷还料！";
                                            txtMatnr.Text = string.Empty;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        //ColorStats();
                    }
                }
                if (rdQuery.Checked)
                {
                    btnConfirm_Click(null, null);
                    txtMatnr.Text = string.Empty;
                    return;
                }
                txtMatnr.Text = string.Empty;
            }
        }

        #region 查询数据
        public void QueryData(DataTable dt)
        {
            try
            {
                this.dtData.Rows.Clear();
                this.dgvData.DataSource = null;
                string strInDate = dtpIndat.Value.ToString("yyyyMMdd");
                string strToDate = dtpToDate.Value.ToString("yyyyMMdd");
                string strLifnr = dt.Rows[0]["LIFNR"].ToString();
                string strMatnr = dt.Rows[0]["MATNR"].ToString();
                string strStatus = "";
                DataRow[] drAllSource;
                DataRow drRow;
                strLocat = txtLocat.Text.Trim();

                if (cmbStatus.Items[cmbStatus.SelectedIndex].ToString() == "已还")
                {
                    strStatus = "Y";
                }
                else if (cmbStatus.Items[cmbStatus.SelectedIndex].ToString() == "未还")
                {
                    strStatus = "N";
                }
                else
                {
                    strStatus = "All";
                }
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                DataTable dtSource = objSapData.ListSapInData_IQCBorrow(txtMblnr.Text.ToString(), Locat, strLifnr, strInDate, strToDate, strStatus, strMatnr, "Query");

                if (dtSource.Rows.Count > 0)
                {
                    if (strStatus == "All")
                    {
                        drAllSource = dtSource.Select(" BRQTY<>RTQTY OR BRQTY=RTQTY OR BRQTY=0 OR RTQTY=0 ");//已借已还的不允许再借
                        for (int i = 0; i < drAllSource.Length; i++)
                        {
                            drRow = dtData.NewRow();
                            drRow["Select"] = false;
                            drRow["MANDT"] = UserData.Client;
                            drRow["WERKS"] = Werks;
                            drRow["LGORT"] = drAllSource[i]["LGORT"].ToString();
                            drRow["MBLNR"] = drAllSource[i]["MBLNR"].ToString();
                            drRow["BWART"] = drAllSource[i]["BWART"].ToString();
                            drRow["LIFNR"] = drAllSource[i]["LIFNR"].ToString();
                            drRow["LOCAT"] = drAllSource[i]["GRLOC"].ToString();
                            drRow["MATNR"] = drAllSource[i]["MATNR"].ToString();
                            drRow["MENGE"] = drAllSource[i]["MENGE"].ToString();
                            drRow["BRQTY"] = drAllSource[i]["BRQTY"].ToString();
                            drRow["RTQTY"] = drAllSource[i]["RTQTY"].ToString();
                            drRow["OldBRQTY"] = drAllSource[i]["BRQTY"].ToString();
                            drRow["OldRTQTY"] = drAllSource[i]["RTQTY"].ToString();
                            drRow["IQCID"] = drAllSource[i]["IQCID"].ToString();
                            drRow["WHID"] = drAllSource[i]["WHID"].ToString();
                            drRow["RTIQCID"] = drAllSource[i]["RTIQCID"].ToString();
                            drRow["RTWHID"] = drAllSource[i]["RTWHID"].ToString();
                            drRow["BUDAT"] = drAllSource[i]["BUDAT"].ToString();
                            drRow["CRDAT"] = drAllSource[i]["CRDAT"].ToString();
                            drRow["BRDAT"] = drAllSource[i]["BRDAT"].ToString();
                            drRow["RTDAT"] = drAllSource[i]["RTDAT"].ToString();
                            drRow["REMAK"] = drAllSource[i]["REMAK"].ToString();
                            dtData.Rows.Add(drRow);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtSource.Rows.Count; i++)
                        {
                            drRow = dtData.NewRow();
                            drRow["Select"] = false;
                            drRow["MANDT"] = UserData.Client;
                            drRow["WERKS"] = Werks;
                            drRow["LGORT"] = dtSource.Rows[i]["LGORT"].ToString();
                            drRow["MBLNR"] = dtSource.Rows[i]["MBLNR"].ToString();
                            drRow["BWART"] = dtSource.Rows[i]["BWART"].ToString();
                            drRow["LIFNR"] = dtSource.Rows[i]["LIFNR"].ToString();
                            drRow["LOCAT"] = dtSource.Rows[i]["GRLOC"].ToString();
                            drRow["MATNR"] = dtSource.Rows[i]["MATNR"].ToString();
                            drRow["MENGE"] = dtSource.Rows[i]["MENGE"].ToString();
                            drRow["BRQTY"] = dtSource.Rows[i]["BRQTY"].ToString();
                            drRow["RTQTY"] = dtSource.Rows[i]["RTQTY"].ToString();
                            drRow["OldBRQTY"] = dtSource.Rows[i]["BRQTY"].ToString();
                            drRow["OldRTQTY"] = dtSource.Rows[i]["RTQTY"].ToString();
                            drRow["IQCID"] = dtSource.Rows[i]["IQCID"].ToString();
                            drRow["WHID"] = dtSource.Rows[i]["WHID"].ToString();
                            drRow["RTIQCID"] = dtSource.Rows[i]["RTIQCID"].ToString();
                            drRow["RTWHID"] = dtSource.Rows[i]["RTWHID"].ToString();
                            drRow["BUDAT"] = dtSource.Rows[i]["BUDAT"].ToString();
                            drRow["CRDAT"] = dtSource.Rows[i]["CRDAT"].ToString();
                            drRow["BRDAT"] = dtSource.Rows[i]["BRDAT"].ToString();
                            drRow["RTDAT"] = dtSource.Rows[i]["RTDAT"].ToString();
                            drRow["REMAK"] = dtSource.Rows[i]["REMAK"].ToString();
                            dtData.Rows.Add(drRow);
                        }
                    }
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
            
        }
        #endregion

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rdQuery.Checked)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                string strMblnr = dgvData.Rows[e.RowIndex].Cells["MBLNR"].Value.ToString();
                string strLgort = dgvData.Rows[e.RowIndex].Cells["LGORT"].Value.ToString();
                string strLocat = dgvData.Rows[e.RowIndex].Cells["LOCAT"].Value.ToString();
                string strLifnr = dgvData.Rows[e.RowIndex].Cells["LIFNR"].Value.ToString(); 
                //string strInDate = dtpIndat.Value.ToString("yyyyMMdd");
                //string strToDate = dtpToDate.Value.ToString("yyyyMMdd");
                //string strStatus = string.Empty;
                string strMatnr = dgvData.Rows[e.RowIndex].Cells["MATNR"].Value.ToString();
                string strRemark = string.Empty;
                if (e.ColumnIndex == 8)
                {
                    strRemark = "borrow";
                    IQCBorrowMaterials_Detail objIQCBorrowMaterials_Detail = new IQCBorrowMaterials_Detail(UserData,Werks,strLgort,strMblnr,strLocat,strMatnr,strLifnr,strRemark);
                    objIQCBorrowMaterials_Detail.ShowDialog();
                }
                else if (e.ColumnIndex == 10)
                {
                    strRemark = "return";
                    IQCBorrowMaterials_Detail objIQCBorrowMaterials_Detail = new IQCBorrowMaterials_Detail(UserData, Werks, strLgort, strMblnr, strLocat, strMatnr, strLifnr, strRemark);
                    objIQCBorrowMaterials_Detail.ShowDialog();
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";

            try
            {

                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    DownExcel(strExportName, dtData);
                    stsWarning.Text = "下载成功";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Duplicate file name");
                return;
            }
        }

        #region 下载excel表格
        public void DownExcel(string path, System.Data.DataTable dt)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("worksheet") as HSSFSheet;
            HSSFCellStyle style = book.CreateCellStyle() as HSSFCellStyle;
            ICellStyle headStyle = book.CreateCellStyle();
            HSSFFont font = (HSSFFont)book.CreateFont();

            #region 表头显示内容
            try
            {
                if (sheet != null)
                {
                    HSSFRow cellHead = (HSSFRow)sheet.CreateRow(0);
                    
                    ICell cell0 = cellHead.CreateCell(0);
                    cell0.SetCellValue("Client");
                    cell0.CellStyle = headStyle;

                    ICell cell1 = cellHead.CreateCell(1);
                    cell1.SetCellValue("厂区");
                    cell1.CellStyle = headStyle;

                    ICell cell2 = cellHead.CreateCell(2);
                    cell2.SetCellValue("仓别");
                    cell2.CellStyle = headStyle;

                    ICell cell3 = cellHead.CreateCell(3);
                    cell3.SetCellValue("储位");
                    cell3.CellStyle = headStyle;

                    ICell cell4 = cellHead.CreateCell(4);
                    cell4.SetCellValue("料号");
                    cell4.CellStyle = headStyle;

                    ICell cell5 = cellHead.CreateCell(5);
                    cell5.SetCellValue("单据数量");
                    cell5.CellStyle = headStyle;

                    ICell cell6 = cellHead.CreateCell(6);
                    cell6.SetCellValue("借料数量");
                    cell6.CellStyle = headStyle;

                    ICell cell7 = cellHead.CreateCell(7);
                    cell7.SetCellValue("还料数量");
                    cell7.CellStyle = headStyle;

                    ICell cell8 = cellHead.CreateCell(8);
                    cell8.SetCellValue("单据号");
                    cell8.CellStyle = headStyle;

                    ICell cell9 = cellHead.CreateCell(9);
                    cell9.SetCellValue("厂商代码");
                    cell9.CellStyle = headStyle;

                    ICell cell10 = cellHead.CreateCell(10);
                    cell10.SetCellValue("异动代码");
                    cell10.CellStyle = headStyle;

                    ICell cell11 = cellHead.CreateCell(11);
                    cell11.SetCellValue("借料IQC工号");
                    cell11.CellStyle = headStyle;

                    ICell cell12 = cellHead.CreateCell(12);
                    cell12.SetCellValue("借料仓管工号");
                    cell12.CellStyle = headStyle;

                    ICell cell13 = cellHead.CreateCell(13);
                    cell13.SetCellValue("还料IQC工号");
                    cell13.CellStyle = headStyle;

                    ICell cell14 = cellHead.CreateCell(14);
                    cell14.SetCellValue("还料仓管工号");
                    cell14.CellStyle = headStyle;

                    ICell cell15 = cellHead.CreateCell(15);
                    cell15.SetCellValue("入账时间");
                    cell15.CellStyle = headStyle;

                    ICell cell16 = cellHead.CreateCell(16);
                    cell16.SetCellValue("创建时间");
                    cell16.CellStyle = headStyle;

                    ICell cell17 = cellHead.CreateCell(17);
                    cell17.SetCellValue("借料时间");
                    cell17.CellStyle = headStyle;

                    ICell cell18 = cellHead.CreateCell(18);
                    cell18.SetCellValue("还料时间");
                    cell18.CellStyle = headStyle;

                    ICell cell19 = cellHead.CreateCell(19);
                    cell19.SetCellValue("备注");
                    cell19.CellStyle = headStyle;

                    //ICell cell19 = cellHead.CreateCell(19);
                    //cell19.SetCellValue("借料IQC工号");
                    //cell19.CellStyle = headStyle;
                }
                #endregion

                #region 写入单元格内容

                for (int i = 0; i < dt.Rows.Count; i++)//表体            
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count-1; j++)
                    {
                        //判断dt列名是否为CRDAT，如果是则不写入
                        //if (dt.Columns[j].ColumnName.ToString() != "Select")
                        //{
                            string d = dt.Rows[i][j+1].ToString();
                            dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j+1].ToString());
                        //}
                    }
                }
                #endregion

                #region 调整列宽
                CellRangeAddress range = new CellRangeAddress(1, dt.Rows.Count + 1, 0, dt.Columns.Count + 1);
                for (int columnNum = 1; columnNum <= 10; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)//在这一列上循环行  
                    {
                        IRow currentRow = sheet.GetRow(rowNum);
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = System.Text.Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                        if (columnWidth < length + 1)
                        {
                            columnWidth = length + 2;
                        }
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth * 256);
                }
                FileStream file = File.OpenWrite(path);
                book.Write(file);
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }
        #endregion

    }
}
