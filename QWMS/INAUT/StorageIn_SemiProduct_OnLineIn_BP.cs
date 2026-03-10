using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;

namespace QWMS
{
    public partial class StorageIn_SemiProduct_OnLineIn_BP : Form
    {
        #region Constructor

        public StorageIn_SemiProduct_OnLineIn_BP(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
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
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }

                rdoNew.Checked = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region DataMember

        #region 變數宣告
        public string strMandt = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strProgid = "";
        public string strMatnr = "";
        public string strMblnr = "";
        public string strType = "";
        public string strInsmk = "";
        public string strSttyp = "";
        public string strLotyp = "";
        public string strCtbto = "";
        public string strRegon = "";
        public string strMachine = "";
        public string strComcd = "";
        public int intFormIndex = 0;
        public bool bolDuplicate = false;
        public DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtCombineData = new DataTable();
        private DataTable dtCombineInventory = new DataTable();
        private bool AllowToClose = true;//設定能否關閉Form視窗
        private string strVersion = "";
        private int intInvqty = 0;
        private int intAlqty = 0;
        private int intPalqty = 0;
        private string strFindLocat = "";
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
        //private TableLayoutPanel tableLayoutPanel1;
        //private TableLayoutPanel tableLayoutPanel2;
        //private TableLayoutPanel tableLayoutPanel3;
        //private TableLayoutPanel tableLayoutPanel4;
        private ArrayList arrBoxID = new ArrayList();
        private QCI.QWMS.LogData objLogData;

        int intScanQty = 0;
        int intTotalQty = 0;

        Dictionary<DataRow, DataGridViewRow> dicMapping = new Dictionary<DataRow, DataGridViewRow>();

        #endregion

        #region 變數
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

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }

        public string Machine
        {
            get
            {
                return strMachine;
            }
            set
            {
                strMachine = value;
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

        #endregion

        #region MemberFunction

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
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

        private void ShowDdlLgort(string varLogort)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortWithAuth(strWerks, varLogort);
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("无该仓别入库权限！");
                        return;
                    }
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortWithAuth(strWerks, varLogort);
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("无该仓别入库权限！");
                        return;
                    }
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                    }
                    cmbLgort.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcChecked);

                DataGridViewTextBoxColumn dgvcLoadid = new DataGridViewTextBoxColumn();
                dgvcLoadid.DataPropertyName = "LOADID";
                dgvcLoadid.HeaderText = "LOADID";
                dgvcLoadid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLoadid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvckdmat = new DataGridViewTextBoxColumn();
                dgvckdmat.DataPropertyName = "KDMAT";
                dgvckdmat.HeaderText = "CUST Mat";
                dgvckdmat.ReadOnly = true;
                dgvckdmat.Width = 90;
                this.dgvData.Columns.Add(dgvckdmat);

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

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.ReadOnly = true;
                this.dgvData.Columns.Add(boxidStyle);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.Width = 100;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcrmano = new DataGridViewTextBoxColumn();
                dgvcrmano.DataPropertyName = "RMANO";
                dgvcrmano.HeaderText = "RMANO";
                dgvcrmano.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcrmano);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
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
                dgvcTrntp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcrmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;

                lblDataCount.Text = dtData.Rows.Count.ToString() + " records";

                #region Mapping

                dicMapping.Clear();

                foreach (DataGridViewRow dgvr in dgvData.Rows)
                {
                    DataRow dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dicMapping.Add(dr, dgvr);
                }

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }

        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        #region GetDefaultTable
        private void GetDefaultTable()
        {
            if (Data.Rows.Count == 0)
            {
                Data = new DataTable();
                Data.Columns.Add("CHKED", typeof(Boolean));
                Data.Columns.Add("MANDT", Type.GetType());
                Data.Columns.Add("COMCD", Type.GetType());
                Data.Columns.Add("WERKS", Type.GetType());
                Data.Columns.Add("LGORT", Type.GetType());
                Data.Columns.Add("LOCAT", Type.GetType());
                Data.Columns.Add("MATNR", Type.GetType());
                Data.Columns.Add("INSMK", Type.GetType());
                Data.Columns.Add("CHARG", Type.GetType());
                Data.Columns.Add("MENGE", Type.GetType());
                Data.Columns.Add("ALQTY", Type.GetType());
                Data.Columns.Add("MBLNR", Type.GetType());
                Data.Columns.Add("BOXID", Type.GetType());
                Data.Columns.Add("EBELN", Type.GetType());
                Data.Columns.Add("LIFNR", Type.GetType());
                Data.Columns.Add("OMBLNR", Type.GetType());
                Data.Columns.Add("MRGID", Type.GetType());
                Data.Columns.Add("KOSTL", Type.GetType());
                Data.Columns.Add("ARBPL", Type.GetType());
                Data.Columns.Add("TRNTP", Type.GetType());
                Data.Columns.Add("RMANO", Type.GetType());
                Data.Columns.Add("RMAK1", Type.GetType());
                Data.Columns.Add("INDAT", Type.GetType());
                Data.Columns.Add("KDMAT", Type.GetType());
                Data.Columns.Add("WO", Type.GetType());
                Data.Columns.Add("SERNO", Type.GetType());
                Data.Columns.Add("LOADID", Type.GetType());
                Data.Columns.Add("MODEL", Type.GetType());
                Data.Columns.Add("REGION", Type.GetType());
                Data.Columns.Add("PALQTY", Type.GetType());

                DataColumn[] dcPrimaryKey = new DataColumn[4];
                dcPrimaryKey[0] = Data.Columns["MBLNR"];
                dcPrimaryKey[1] = Data.Columns["MATNR"];
                dcPrimaryKey[2] = Data.Columns["BOXID"];
                dcPrimaryKey[3] = Data.Columns["WO"];

                Data.PrimaryKey = dcPrimaryKey;
            }
        }
        #endregion

        private void SetControlState(bool bolBeforeConfirm)
        {
            this.cmbWerks.Enabled = bolBeforeConfirm;
            this.cmbLgort.Enabled = bolBeforeConfirm;
            this.dtpIndat.Enabled = bolBeforeConfirm;

            this.txtBoxID.Enabled = bolBeforeConfirm;
            this.txtTotalQty.Enabled = false;
            this.txtScannedQty.Enabled = false;
        }

        #endregion

        #region Event

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    Lgort = "";
                }

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type, this.Ctbto, this.Regon);
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

        #region txtLocat_KeyPress
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                    DataTable dtbox = new DataTable();
                    dtbox = objStorageIn.QueryInStorageBoxQty(txtLocat.Text.Trim());
                    if (dtbox.Rows.Count > 0)
                    {
                        txtBoxQty.Text = dtbox.Rows.Count.ToString();
                    }
                    else
                    {
                        txtBoxQty.Text = "0";
                    }

                    #region 將Location 填回Data中
                    foreach (DataRow rsTmpRow in this.Data.Rows)
                    {
                        rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
                    }
                    this.Data.AcceptChanges();
                    #endregion

                    #region 設定按鈕
                    this.txtBoxID.Text = "";
                    this.txtTotalQty.Text = "";
                    this.txtScannedQty.Text = "";
                    this.txtBoxID.Enabled = true;
                    this.txtTotalQty.Enabled = false;
                    this.txtScannedQty.Enabled = false;
                    this.txtBoxID.Enabled = true;
                    this.txtBoxID.Focus();
                    #endregion
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\Fail.wav");
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion

        #region txtLocat_KeyDown
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (DataRow rsTmpRow in this.dtCombineData.Rows)
            {
                rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
            }
            this.dtCombineData.AcceptChanges();
        }
        #endregion

        #region rdoNew_CheckedChanged

        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "NEW";
            Type = strType;
        }

        #endregion

        #region rdoAdd_CheckedChanged

        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "ADD";
            Type = strType;
            txtLocat.Enabled = true;
        }

        #endregion

        #region txtBoxID_KeyDown
        private void txtBoxID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            #region 變數宣告

            DataRow drRow;
            stsWarning.Text = string.Empty;
            string strTempMblnrMatnr = string.Empty;
            DataTable dtTemp = new DataTable();
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, cmbWerks.Text.Trim(), Lgort);
            DataTable dtPalData = new DataTable();
            DataTable dtPalData_chk = new DataTable();
            string strPalletID = "";
            #endregion

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

                    DataTable dtbox = new DataTable();
                    //查询Box信息
                    dtbox = objStorageIn.QueryBoxinfo_BP(Werks, "218", "QMS", txtBoxID.Text.Trim().ToUpper());

                    if (dtbox.Rows.Count > 0)
                    {
                        strPalletID = dtbox.Rows[0]["MBLNR"].ToString().Trim();
                        ShowDdlLgort(dtbox.Rows[0]["LGORT"].ToString().Trim());
                    }
                    else
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        throw new Exception("没有SF数据!!");
                    }

                    #region 检查厂区和仓别

                    strWerks = Convert.ToString(cmbWerks.Items[cmbWerks.SelectedIndex]);
                    strLgort = Convert.ToString(cmbLgort.Items[cmbLgort.SelectedIndex]);

                    if (string.IsNullOrEmpty(Werks) || string.IsNullOrEmpty(Lgort))
                    {
                        throw new Exception("Plant 和 storage 不能为空!!");
                    }

                    #region 取得Sttyp及Lotyp

                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                    if (dtTemp.Rows.Count >= 1)
                    {
                        strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {
                        throw new Exception("没有 storage 数据!!");
                    }

                    #endregion

                    #region 取出Box ID

                    string strBoxID = txtBoxID.Text.Trim().ToUpper();
                    string strLocat = txtLocat.Text.Trim();

                    if (string.IsNullOrEmpty(strBoxID))
                    {
                        throw new Exception("Box ID 不能为空!!");
                    }

                    #endregion

                    #endregion

                    #region DataGridView 初始化

                    if (Data.Rows.Count == 0)
                    {
                        //HAC
                        #region 確認是否已經SAP扣帳，提示使用僅入QWMS系統
                        if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                        {
                            //確認是否已經SAP扣帳，提示使用僅入QWMS系統
                            string strResult = "";
                            strResult = objStorageIn.CheckWHTRS(strPalletID);
                            if (strResult != "" && !chkQWMS.Checked)
                            {
                                MessageBox.Show("此Pallet已經扣帳" + strResult + "，請勾選重新刷入QWMS");
                                throw new Exception("此Pallet已經扣帳" + strResult + "，請勾選重新刷入QWMS");
                            }
                        }
                        #endregion

                        dtTmpData = new DataTable();
                        //BOX信息
                        dtTmpData = objSapData.QueryQMSLineInDataByBoxID_BP(strLocat, strPalletID, "", dtpIndat.Value.ToString("yyyyMMdd"), Insmk, strLgort);

                        if (dtTmpData != null && dtTmpData.Rows.Count > 0)
                        {
                            GetDefaultTable();

                            #region 填值到datatable中

                            foreach (DataRow tmpRow in dtTmpData.Rows)
                            {
                                drRow = Data.NewRow();
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["LOCAT"] = Locat;
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["ALQTY"] = tmpRow["MENGE"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();
                                drRow["EBELN"] = tmpRow["EBELN"].ToString();
                                drRow["LIFNR"] = tmpRow["LIFNR"].ToString();
                                drRow["OMBLNR"] = tmpRow["OMBLNR"].ToString();
                                drRow["MRGID"] = tmpRow["MRGID"].ToString();
                                drRow["KOSTL"] = tmpRow["KOSTL"].ToString();
                                drRow["ARBPL"] = tmpRow["ARBPL"].ToString();
                                drRow["TRNTP"] = tmpRow["TRNTP"].ToString();
                                drRow["RMAK1"] = string.Empty;
                                drRow["INDAT"] = tmpRow["INDAT"].ToString();
                                drRow["KDMAT"] = (dtPalData.Columns.IndexOf("KDMAT") > -1) ? tmpRow["KDMAT"].ToString() : string.Empty;
                                drRow["WO"] = tmpRow["WO"].ToString();
                                drRow["SERNO"] = string.Empty;
                                drRow["LOADID"] = tmpRow["LOADID"].ToString();
                                drRow["MODEL"] = tmpRow["MODEL"].ToString();
                                drRow["REGION"] = tmpRow["REGION"].ToString();
                                drRow["PALQTY"] = tmpRow["PALQTY"].ToString();

                                Data.Rows.Add(drRow);
                            }

                            #endregion

                            ShowDataGrid();

                            foreach (DataRow drData in Data.Rows)
                            {
                                intTotalQty += Convert.ToUInt16(drData["MENGE"].ToString());
                            }
                        }
                        else
                        {
                            txtBoxID.Text = string.Empty;
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("无数据!!");
                        }
                    }

                    #endregion

                    #region 刷单笔数据
                    int tempQty = 0;
                    foreach (DataGridViewRow dataGridRow in dgvData.Rows)
                    {
                        dataGridRow.DefaultCellStyle.BackColor = Color.Silver;
                        //string strg1 = txtBoxID.Text.Trim().ToUpper();
                        //string strt = dataGridRow.Cells[6].Value.ToString().ToUpper();
                        if (txtBoxID.Text.Trim().ToUpper() == dataGridRow.Cells[6].Value.ToString().ToUpper())
                        {

                            if (dataGridRow.Cells[0].Value.ToString() == "True")
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                throw new Exception("BOX ID 已經掃過!!");
                            }
                            else
                            {
                                tempQty += Int32.Parse(dataGridRow.Cells[7].Value.ToString());
                                dataGridRow.Cells[0].Value = true;
                                dataGridRow.DefaultCellStyle.BackColor = Color.Blue;
                            }
                        }
                    }

                    if (tempQty == 0)
                    {
                        throw new Exception("Box ID 不存在!!!");
                    }
                    else
                    {
                        intScanQty += tempQty;
                    }

                    txtScannedQty.Text = intScanQty.ToString();
                    txtTotalQty.Text = intTotalQty.ToString();

                    #endregion

                    DataRow[] drScan = Data.Select("CHKED = true");
                    lblDataCount.Text = string.Format("Scanned {0} of {1} records", drScan.Length, Data.Rows.Count.ToString());
                    //HAC
                    if (txtTotalQty.Text == txtScannedQty.Text)
                    {
                        #region 合併同一個Pallet ID、料號、版本、庫別的資料

                        #region 變數宣告

                        DataRow[] combineRow;
                        StringBuilder sbCombineIndex = new StringBuilder();
                        ArrayList alAllCombine = new ArrayList();
                        DataSet dsData = new DataSet();
                        int intCombineQty = 0;

                        #endregion

                        #region 合併資料

                        dtCombineData.Clear();
                        dtCombineData = dtTmpData.Clone();
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            #region 每次比對的Index (sbCombineIndex)
                            sbCombineIndex.Remove(0, sbCombineIndex.Length);
                            sbCombineIndex.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and MBLNR='" + dtData.Rows[i]["MBLNR"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString().Trim() + "'");
                            #endregion

                            if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                            {
                                alAllCombine.Add(sbCombineIndex.ToString());
                                combineRow = dtData.Select(sbCombineIndex.ToString());
                                intCombineQty = 0;
                                for (int j = 0; j < combineRow.Length; j++)
                                {
                                    intCombineQty += Int32.Parse(combineRow[j]["MENGE"].ToString().Trim());
                                }

                                drRow = dtCombineData.NewRow();
                                drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString().Trim();
                                drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString().Trim();
                                drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString().Trim();
                                drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString().Trim();
                                drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString().Trim();
                                drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString().Trim();
                                drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString().Trim();
                                drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString().Trim();
                                drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString().Trim();
                                drRow["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString().Trim();
                                drRow["EBELN"] = dtData.Rows[i]["EBELN"].ToString().Trim();
                                drRow["INDAT"] = dtData.Rows[i]["INDAT"].ToString().Trim();
                                drRow["MENGE"] = intCombineQty.ToString().Trim();
                                drRow["ALQTY"] = intCombineQty.ToString().Trim();
                                drRow["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString().Trim();
                                drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString().Trim();
                                drRow["RMANO"] = dtData.Rows[i]["RMANO"].ToString().Trim();
                                drRow["BOXID"] = dtData.Rows[i]["BOXID"].ToString().Trim();
                                drRow["RMAK1"] = dtData.Rows[i]["RMAK1"].ToString().Trim();
                                drRow["WO"] = dtData.Rows[i]["WO"].ToString().Trim();
                                drRow["SERNO"] = dtData.Rows[i]["SERNO"].ToString().Trim();
                                drRow["LOADID"] = dtData.Rows[i]["LOADID"].ToString().Trim();
                                drRow["MODEL"] = dtData.Rows[i]["MODEL"].ToString().Trim();
                                drRow["REGION"] = dtData.Rows[i]["REGION"].ToString().Trim();
                                drRow["PALQTY"] = dtData.Rows[i]["PALQTY"].ToString().Trim();

                                dtCombineData.Rows.Add(drRow);
                            }

                            if (arrBoxID.IndexOf(dtData.Rows[i]["BOXID"].ToString().Trim()) < 0)
                            {
                                arrBoxID.Add(dtData.Rows[i]["BOXID"].ToString().Trim());
                            }
                        }

                        #endregion

                        #endregion

                        if (strType == "ADD")
                        {
                            this.txtLocat.Focus();
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    this.txtBoxID.Text = "";
                    Sound.Play(@"Sound\ERROR.wav");
                    return;
                }
                finally
                {
                    this.txtBoxID.Text = string.Empty;
                }
            }
        }
        #endregion

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (txtTotalQty.Text != txtScannedQty.Text)
                {
                    MessageBox.Show("扫入数量与总数量不匹配！");
                    txtBoxID.Focus();
                    return;
                }
                this.btnSave.Enabled = false;

                #region 参数初始化

                SetbtnSaveProcess();
                string strMtype = string.Empty;
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                DataTable dtPalData = new DataTable();
                DataTable dtTemp = new DataTable();
                ArrayList SN = new ArrayList();
                string[] message = new string[2];
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType,
                    CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
                    CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType,
                    CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
                    CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

                #endregion

                #region 校验

                if (txtLocat.Text.Trim() == "")
                {
                    MessageBox.Show("Location不能为空！");
                    return;
                }
                else
                {
                    DataTable dtlocation = new DataTable();
                    dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                    if (dtlocation.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtlocation.Rows.Count; i++)
                        {
                            if (dtData.Rows.Count > 0)
                            {
                                if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                    dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                {
                                    stsWarning.Text = "该储位中已经存在不同的料号！";
                                    return;
                                }
                            }
                        }
                    }
                }
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "数据不能为空!!";
                    SetbtnSaveException();
                    return;
                }

                #endregion

                #region SAP寫檔

                #region 去除不必要的欄位

                if (dtTmpData.Columns.Count > 0)
                {
                    dtTmpData.Columns.Remove("MANDT");
                    dtTmpData.Columns.Remove("LGORT");
                    dtTmpData.Columns.Remove("LOCAT");
                    dtTmpData.Columns.Remove("INSMK");
                    dtTmpData.Columns.Remove("CHARG");
                    dtTmpData.Columns.Remove("ALQTY");
                    dtTmpData.Columns.Remove("BOXID");
                    dtTmpData.Columns.Remove("EBELN");
                    dtTmpData.Columns.Remove("LIFNR");
                    dtTmpData.Columns.Remove("OMBLNR");
                    dtTmpData.Columns.Remove("MRGID");
                    dtTmpData.Columns.Remove("KOSTL");
                    dtTmpData.Columns.Remove("ARBPL");
                    dtTmpData.Columns.Remove("TRNTP");
                    dtTmpData.Columns.Remove("RMAK1");
                    dtTmpData.Columns.Remove("INDAT");
                    dtTmpData.Columns.Remove("KDMAT");
                    dtTmpData.Columns.Remove("LOADID");
                }
                else
                {
                    stsWarning.Text = "请Refresh后重刷";
                    SetbtnSaveException();
                    return;
                }

                #endregion

                #region 合併資料
                //获取Pallet id 类型是262 QMS_311还是311 QMS 以及归并扣帐数据
                DataTable dt = objStorageIn.QueryDataForSap_BP(dtTmpData.Rows[0]["MBLNR"].ToString(), out strMtype);
                if (string.IsNullOrEmpty(strMtype))
                {
                    MessageBox.Show("未获取到单据类型");
                    return;
                }

                #endregion

                #region 寫入檔案傳給SAP & 讀取SAP回傳的扣帳編號

                #region 不讀取SAP回傳的扣帳編號

                bool bolSapStatus = false;
                bool blQMSStatus = false;
                stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                AllowToClose = false; //強制User無法關閉視窗
                string strPalletID = dt.Rows[0]["MBLNR"].ToString().Trim();
                string strGrNo = string.Empty;

                #endregion

                #region 產生檔案給SAP扣帳但不讀取SAP回傳的扣帳編號

                #region 回傳扣帳狀態給QMS

                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                {
                    if (chkQWMS.Checked)
                    {
                        strGrNo = objStorageIn.CheckWHTRS(strPalletID);
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, null);
                        if (string.IsNullOrEmpty(strGrNo))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, "SAP未扣帐不能勾选 重新刷入QWMS");
                            stsWarning.Text = strPalletID + @":在SAP未扣帐，不能勾选 仅入QWMS";
                            MessageBox.Show(strPalletID + @":在SAP未扣帐，不能勾选 仅入QWMS");
                            return;
                        }
                        else
                        {
                            bolSapStatus = true;
                        }
                    }

                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始检查QMS中PalletID是否可用...", null, null);
                    message = objStorageIn.TransferStatusToQMS_BP(strPalletID, "WH Receiving", "", "", "WH IN");
                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束检查QMS中PalletID是否可用", message[0].ToString().Trim(), message[1].ToString());

                    //选择仅入QWMS 略过QMS INFO
                    if (!chkQWMS.Checked)
                    {
                        if (!message[0].ToString().Trim().Equals("Pass", StringComparison.CurrentCultureIgnoreCase))
                        {
                            stsWarning.Text = message[1].ToString();
                            MessageBox.Show(strPalletID + ":" + message[1].ToString());
                            return;
                        }
                    }
                    else
                    {
                        objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "选择仅入QWMS，略过检查QMS状态", null, null);
                    }
                }
                #endregion

                //測試庫不執行SAP扣帳
                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                //测试SAP扣帐
                //  if(true)
                {
                    if (!chkQWMS.Checked)
                    {
                        string strErrorMessage = string.Empty;

                        objLogData.AddQWMSLOG(strPalletID, "SAP", "IN", "开始SAP扣帐", null, null);
                        DataTable dtResponse = objStorageIn.WriteSapFile_CSMC_BP(dt, strMtype);
                        objLogData.AddQWMSLOG(strPalletID, "SAP", "IN", "结束SAP扣帐", dtResponse.Rows[0]["RESULT"].ToString().Trim() + dtResponse.Rows[0]["REMARK2"].ToString().Trim(), null);

                        #region 讀取SAP回傳的扣帳編號

                        string strGRNoTemp = string.Empty;
                        if (dtResponse.Rows[0]["MBLNR"].ToString() == strPalletID &&
                            dtResponse.Rows[0]["REMARK2"].ToString().Trim() == "")
                        {
                            bolSapStatus = true;
                        }
                        if (dtResponse.Rows[0]["MBLNR"].ToString() == strPalletID &&
                            !string.IsNullOrEmpty(dtResponse.Rows[0]["REMARK2"].ToString().Trim()))
                        {
                            string[] GRtemp = dtResponse.Rows[0]["REMARK2"].ToString().Trim().Split(':');
                            if (GRtemp.Length >= 2 && "49;50".Contains(GRtemp[1].Trim().Substring(0, 2)) &&
                                objStorageIn.CheckWHBOXInfo(strPalletID, arrBoxID) &&
                                GRtemp[1].Trim().Length == 10 && !GRtemp[1].Trim().Contains("."))
                            {
                                strGRNoTemp = GRtemp[1].Trim();
                                bolSapStatus = true;
                            }
                            if (dtResponse.Rows[0]["REMARK2"].ToString().Trim().Contains(">"))
                            {
                                strErrorMessage = @"請找PMC或产线成管人員協助處理！";
                            }
                            else if (dtResponse.Rows[0]["REMARK2"].ToString().Trim().Contains(":"))
                            {
                                strErrorMessage = @"请过3分钟后重试！";
                            }
                            else
                            {
                                strErrorMessage = "请找SAP人員協助處理";
                            }
                            strErrorMessage = "SAP未扣帳成功，錯誤訊息: " + dtResponse.Rows[0]["REMARK2"].ToString().Trim() + strErrorMessage;
                            Sound.Play(@"Sound\ERROR.wav");
                        }

                        #region Add By Michael 20150603 for 储位库存防呆
                        DataTable dtlocation = new DataTable();
                        dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                        if (dtlocation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtlocation.Rows.Count; i++)
                            {
                                if (dtData.Rows.Count > 0)
                                {
                                    if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                        dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                    {
                                        stsWarning.Text = "储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！";
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！");
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 回传SAP扣帐状态给QMS 解锁
                        //測試庫不執行SAP扣帳
                        if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                        // if (false)
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);
                            if (!string.IsNullOrEmpty(strGRNoTemp))
                            {
                                message = objStorageIn.TransferStatusToQMS_BP(strPalletID, "", strGRNoTemp, "", "SAP IN");
                            }
                            else
                            {
                                message = objStorageIn.TransferStatusToQMS_BP(dtResponse.Rows[0]["MBLNR"].ToString(), "",
                                                                              dtResponse.Rows[0]["RESULT"].ToString().Trim(),
                                                                              dtResponse.Rows[0]["REMARK2"].ToString().Trim(), "SAP IN");
                            }

                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态", message[0].ToString().Trim(), message[1].ToString());
                        }

                        #endregion
                        if (bolSapStatus)
                        {
                            AllowToClose = true;
                            stsWarning.Text = "SAP Posting OK!!SAP扣帐编号:" + dtResponse.Rows[0]["RESULT"].ToString().Trim();
                        }
                        else
                        {
                            stsWarning.Text = strErrorMessage;
                            MessageBox.Show(strErrorMessage);
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Add By Michael 20150603 for 储位库存防呆
                        DataTable dtlocation = new DataTable();
                        dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                        if (dtlocation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtlocation.Rows.Count; i++)
                            {
                                if (dtData.Rows.Count > 0)
                                {
                                    if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                        dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                    {
                                        stsWarning.Text = "储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！";
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！");
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 回传SAP扣帐状态给QMS 解锁
                        //測試庫不執行SAP扣帳
                        if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                        // if (false)
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);
                            message = objStorageIn.TransferStatusToQMS_BP(strPalletID, "", strGrNo, "", "SAP IN");
                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态", message[0].ToString().Trim(), message[1].ToString());
                        }

                        #endregion
                    }
                }

                #endregion

                #endregion

                #endregion

                #region 入QWMS的庫存

                foreach (DataRow row in dtCombineData.Rows)
                {
                    row["LOCAT"] = Locat;
                }

                objLogData.AddQWMSLOG(dtData.Rows[0]["MBLNR"].ToString(), "QWMS", "IN", "开始QWMS入账", null, null);
                //SAP QMS 运行通过后QWMS方可入库
                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                //  if (true)
                {
                    if (bolSapStatus)
                    //  if (true)
                    {
                        //需修改311入库
                        if (objStorageIn.AddSemiProdOnLineInData_BP(Locat, dtCombineData, arrBoxID, dtData))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "OK", null);
                            stsWarning.Text = "QWMS Add OK!!";
                            Sound.Play(@"Sound\OO1.wav");
                        }
                        else
                        {
                            stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + objStorageIn.ERRMSG,
                                null);

                            Sound.Play(@"Sound\ERROR.wav");
                        }
                    }
                }

                #endregion
                dtTmpData.Clear();
                return;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            chkQWMS.Checked = false;
            stsWarning.Text = string.Empty;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            this.gbHeader.Enabled = true;
            this.txtLocat.Text = string.Empty;
            this.txtBoxID.Text = string.Empty;
            this.txtTotalQty.Text = string.Empty;
            this.txtScannedQty.Text = string.Empty;
            this.txtSanLocation.Text = string.Empty;
            intScanQty = 0;
            intTotalQty = 0;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.lblDataCount.Text = "0 records";
            this.btnSave.Enabled = false;
            this.dgvData.Columns.Clear();
            dtTmpData.Clear();
            SetControlState(true);
            this.cmbLgort.Enabled = false;
            this.cmbLgort.Text = "";
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void txtSanLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                if (txtSanLocation.Text.Trim().ToUpper() != "" && txtSanLocation.Text.Trim().ToUpper() == txtLocat.Text.Trim().ToUpper())
                {
                    txtSanLocation.Text = txtLocat.Text.Trim().ToUpper();
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    Sound.Play(@"Sound\OO.wav");
                    MessageBox.Show("储位扫描错误！");
                    return;
                }
            }
        }
        #endregion
    }
}
