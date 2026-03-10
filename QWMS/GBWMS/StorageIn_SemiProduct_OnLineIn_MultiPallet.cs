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
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace QWMS
{
    public partial class StorageIn_SemiProduct_OnLineIn_MultiPallet : Form
    {
        #region Data Member

        #region 申明变量
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
        public string strCharg = "";
        public string strMType = "";
        public string strPlant = "";
        public string strStorage = "";

        UserInfo UserData = new UserInfo();
        //DataGrid DataSource
        public DataTable dtData = new DataTable();
        //Scan DataSource
        public DataTable dtScan = new DataTable();
        //WHDWN可入库 DataSource
        private DataTable dtTmpData = new DataTable();
        //ListBox DataSource
        private DataTable dtListBox = new DataTable();
        //合并资料
        private DataTable dtCombineData = new DataTable();

        public bool bolDuplicate = false;
        private bool AllowToClose = true;//設定能否關閉Form視窗
        //Total Scan Qty
        int intTotalScanQty = 0;
        //Total Pallet Qty
        int intTotalPalletQty = 0;
        //
        //Dictionary<DataRow, DataGridViewRow> dicMapping = new Dictionary<DataRow, DataGridViewRow>();

        private QCI.QWMS.LogData objLogData;

        #endregion

        #region     属性
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

        public DataTable Scan
        {
            get
            {
                return dtScan;
            }
            set
            {
                dtScan = value;
            }
        }

        public string CHARG
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

        public string MATNR
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

        public string MTYPE
        {
            get
            {
                return strMType;
            }
            set
            {
                strMType = value;
            }
        }
        #endregion

        //构造函数
        public StorageIn_SemiProduct_OnLineIn_MultiPallet(UserInfo varUserData, string strProgid)
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
                //检查功能权限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //Status赋值
                    ShowStatusData();
                    //Plant初始化
                    ShowDdlWerks();
                    //Storage初始化
                    ShowDdlLgort();

                    //bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
                //入库方式
                rdoNew.Checked = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region MemberFunction

        #region Condition

        //Status赋值
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }

        //Plant初始化
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

        //Storage初始化
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

        //Storage初始化
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

        //Conditions中控件状态
        private void SetControlState(bool bolBeforeConfirm)
        {
            this.cmbWerks.Enabled = bolBeforeConfirm;
            this.cmbLgort.Enabled = bolBeforeConfirm;
            this.dtpIndat.Enabled = bolBeforeConfirm;
            this.txtBoxID.Enabled = bolBeforeConfirm;
            //Scan
            this.txtScannedQty.Enabled = false;
            this.txtPalletQty.Enabled = false;
            //Total
            this.txtTotalScanQty.Enabled = false;
            this.txtTotalPalletQty.Enabled = false;
        }

        #endregion

        //DataGrid Columns
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
                dgvcMatnr.ReadOnly = false;
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

                dgvData.DataSource = dtScan;
                lblDataCount.Text = dtScan.Rows.Count.ToString() + " records";

                #region Mapping

                //dicMapping.Clear();

                //foreach (DataGridViewRow dgvr in dgvData.Rows)
                //{
                //    DataRow dr = (dgvr.DataBoundItem as DataRowView).Row;
                //    dicMapping.Add(dr, dgvr);
                //}

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        //ListView Columns
        public void ShowListBox(string strFocusPalletID)
        {
            lbxScanInfo.Clear();
            int iFocus = 0;
            int iIndex = 0;
            try
            {
                ColumnHeader chMBLNR = new ColumnHeader();
                chMBLNR.Text = "Document No";
                chMBLNR.Width = 240;
                chMBLNR.TextAlign = HorizontalAlignment.Center;
                lbxScanInfo.Columns.Add(chMBLNR);

                ColumnHeader chBOX = new ColumnHeader();
                chBOX.Text = "Box Num";
                chBOX.Width = 180;
                chBOX.TextAlign = HorizontalAlignment.Center;
                lbxScanInfo.Columns.Add(chBOX);

                ColumnHeader chSCAN = new ColumnHeader();
                chSCAN.Text = "Scanned Qty";
                chSCAN.Width = 180;
                chSCAN.TextAlign = HorizontalAlignment.Center;
                lbxScanInfo.Columns.Add(chSCAN);

                ColumnHeader chPALLET = new ColumnHeader();
                chPALLET.Text = "Pallet Qty";
                chPALLET.Width = 180;
                chPALLET.TextAlign = HorizontalAlignment.Center;
                lbxScanInfo.Columns.Add(chPALLET);

                lbxScanInfo.GridLines = true;
                lbxScanInfo.View = View.Details;
                foreach (DataRow dr in dtListBox.Rows)
                {
                    ListViewItem lvi = new ListViewItem(dr["MBLNR"].ToString());
                    lvi.SubItems.Add(dr["BOXNum"].ToString());
                    lvi.SubItems.Add(dr["ScanQty"].ToString());
                    lvi.SubItems.Add(dr["PalletQty"].ToString());
                    if (dr["MBLNR"].ToString() == strFocusPalletID)
                    {
                        lvi.BackColor = Color.Red;
                        iFocus = iIndex;
                    }
                    lbxScanInfo.Items.Add(lvi);
                    iIndex++;
                }
                lbxScanInfo.Items[iFocus].Selected = true;
                lbxScanInfo.Items[iFocus].EnsureVisible();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowListBox()");
            }
        }

        //dtData 初始化 
        private void GetDataDefaultTable()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetDataDefaultTable()");
            }
        }

        //dtScan 初始化
        private void GetScanDefaultTable()
        {
            try
            {
                if (Scan.Rows.Count == 0)
                {
                    Scan = new DataTable();
                    Scan.Columns.Add("CHKED", typeof(Boolean));
                    Scan.Columns.Add("MANDT", Type.GetType());
                    Scan.Columns.Add("COMCD", Type.GetType());
                    Scan.Columns.Add("WERKS", Type.GetType());
                    Scan.Columns.Add("LGORT", Type.GetType());
                    Scan.Columns.Add("LOCAT", Type.GetType());
                    Scan.Columns.Add("MATNR", Type.GetType());
                    Scan.Columns.Add("INSMK", Type.GetType());
                    Scan.Columns.Add("CHARG", Type.GetType());
                    Scan.Columns.Add("MENGE", Type.GetType());
                    Scan.Columns.Add("ALQTY", Type.GetType());
                    Scan.Columns.Add("MBLNR", Type.GetType());
                    Scan.Columns.Add("BOXID", Type.GetType());
                    Scan.Columns.Add("EBELN", Type.GetType());
                    Scan.Columns.Add("LIFNR", Type.GetType());
                    Scan.Columns.Add("OMBLNR", Type.GetType());
                    Scan.Columns.Add("MRGID", Type.GetType());
                    Scan.Columns.Add("KOSTL", Type.GetType());
                    Scan.Columns.Add("ARBPL", Type.GetType());
                    Scan.Columns.Add("TRNTP", Type.GetType());
                    Scan.Columns.Add("RMANO", Type.GetType());
                    Scan.Columns.Add("RMAK1", Type.GetType());
                    Scan.Columns.Add("INDAT", Type.GetType());
                    Scan.Columns.Add("KDMAT", Type.GetType());
                    Scan.Columns.Add("WO", Type.GetType());
                    Scan.Columns.Add("SERNO", Type.GetType());
                    Scan.Columns.Add("LOADID", Type.GetType());
                    Scan.Columns.Add("MODEL", Type.GetType());
                    Scan.Columns.Add("REGION", Type.GetType());
                    Scan.Columns.Add("PALQTY", Type.GetType());

                    DataColumn[] dcPrimaryKey = new DataColumn[4];
                    dcPrimaryKey[0] = Scan.Columns["MBLNR"];
                    dcPrimaryKey[1] = Scan.Columns["MATNR"];
                    dcPrimaryKey[2] = Scan.Columns["BOXID"];
                    dcPrimaryKey[3] = Scan.Columns["WO"];

                    Scan.PrimaryKey = dcPrimaryKey;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetScanDefaultTable()");
            }
        }

        //dtListBox 初始化
        private void GetListBoxDefaultTable()
        {
            if (dtListBox.Rows.Count == 0)
            {
                dtListBox = new DataTable();
                dtListBox.Columns.Add("MBLNR", Type.GetType());
                dtListBox.Columns.Add("BOXNum", typeof(string));
                dtListBox.Columns.Add("ScanQty", typeof(string));
                dtListBox.Columns.Add("PalletQty", typeof(string));
            }
        }

        //DataTable 赋值
        private void SetDataTableValue(ref DataTable dtTmp, DataTable dtTmpData, bool IsAdd)
        {
            try
            {
                int intPalletQty = 0;
                foreach (DataRow tmpRow in dtTmpData.Rows)
                {
                    DataRow drRow = dtTmp.NewRow();
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
                    if (IsAdd)
                        intPalletQty += Convert.ToInt32(tmpRow["MENGE"].ToString());

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
                    drRow["KDMAT"] = string.Empty;
                    drRow["WO"] = tmpRow["WO"].ToString();
                    drRow["SERNO"] = string.Empty;
                    drRow["LOADID"] = tmpRow["LOADID"].ToString();
                    drRow["MODEL"] = tmpRow["MODEL"].ToString();
                    drRow["REGION"] = tmpRow["REGION"].ToString();
                    drRow["PALQTY"] = tmpRow["PALQTY"].ToString();
                    dtTmp.Rows.Add(drRow);
                }
                //ListBox 
                if (IsAdd)
                {
                    DataRow dr = dtListBox.NewRow();
                    dr["MBLNR"] = dtTmpData.Rows[0]["MBLNR"].ToString();
                    dr["BOXNum"] = "0";
                    dr["ScanQty"] = "0";
                    dr["PalletQty"] = intPalletQty.ToString();
                    dtListBox.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-SetDataTableValue()");
            }
        }

        //Data/Scan CHKED栏位赋值
        private void SetChekdValue(string strPallet, string strBOXID)
        {
            //Data
            foreach (DataRow rsDataRow in this.Data.Rows)
            {
                if (rsDataRow["MBLNR"].ToString() == strPallet && rsDataRow["BOXID"].ToString() == strBOXID)
                {
                    rsDataRow["CHKED"] = "True";
                    break;
                }
            }
            this.Data.AcceptChanges();
            //Scan
            //foreach (DataRow rsScan in Scan.Rows)
            //{
            //    if (rsScan["MBLNR"].ToString() == strPallet && rsScan["BOXID"].ToString() == strBOXID)
            //    {
            //        rsScan["CHKED"] = "True";
            //        break;
            //    }
            //}
            //Scan.AcceptChanges();
        }

        //Condition Qty控件赋值
        private void SetConditionQty(int tempQty, string strPalletID, bool IsFirstScanPallet, bool IsPass)
        {
            try
            {
                foreach (DataRow dr in dtListBox.Rows)
                {
                    if (dr["MBLNR"].ToString() == strPalletID)
                    {
                        int intScanQty = Convert.ToInt32(dr["ScanQty"]) + tempQty;
                        intTotalScanQty += tempQty;
                        if (IsFirstScanPallet)
                            intTotalPalletQty += Convert.ToInt32(dr["PalletQty"]);

                        //ListBox值变化
                        if (IsPass)
                            dr["BOXNum"] = (Convert.ToInt32(dr["BOXNum"]) + 1).ToString();
                        dr["ScanQty"] = intScanQty.ToString();

                        //Conditon Qty控件
                        txtScannedQty.Text = dr["ScanQty"].ToString();
                        txtPalletQty.Text = dr["PalletQty"].ToString();
                        txtTotalScanQty.Text = intTotalScanQty.ToString();
                        txtTotalPalletQty.Text = intTotalPalletQty.ToString();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-SetConditionQty()");
            }
        }

        //Save 不可用
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }

        //Save 可用
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        //取当前Pallet下的所有BOX
        private void GetBOXInfo(string strPalletID, ref ArrayList arrBoxID)
        {
            //string strReturn = string.Empty;
            DataTable dtTmp = Data.Select("MBLNR = '" + strPalletID + "' ").CopyToDataTable<DataRow>();
            foreach (DataRow dr in dtTmp.Rows)
            {
                ////BOXID 字符串
                //if (string.IsNullOrEmpty(strReturn))
                //    strReturn = dr["BOXID"].ToString();
                //else
                //    strReturn += ";" + dr["BOXID"].ToString();
                //BOXID 集合
                if (arrBoxID.IndexOf(dr["BOXID"].ToString()) < 0)
                {
                    arrBoxID.Add(dr["BOXID"].ToString());
                }
            }
            //return strReturn;
        }

        #region LinqtoDatatable

        public static DataTable LinqQueryToDataTable<T>(IEnumerable<T> query)
        {
            DataTable tbl = new DataTable();
            PropertyInfo[] props = null;

            foreach (T item in query)
            {
                if (props == null) //尚未初始化  
                {
                    Type t = item.GetType();
                    props = t.GetProperties();
                    foreach (PropertyInfo pi in props)
                    {
                        Type colType = pi.PropertyType;

                        //針對Nullable<>特別處理  
                        if (colType.IsGenericType && colType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        //建立欄位  
                        tbl.Columns.Add(pi.Name, colType);
                    }
                }

                DataRow row = tbl.NewRow();
                foreach (PropertyInfo pi in props)
                {
                    row[pi.Name] = pi.GetValue(item, null) ?? DBNull.Value;
                }

                tbl.Rows.Add(row);
            }
            return tbl;
        }

        #endregion

        #region     打印

        private void PrintLabel(DataTable dtPrint)
        {
            long intRetval;
            string strOutput = "";
            StreamReader reader = new StreamReader(Application.StartupPath + @"\\label_Product.txt", System.Text.Encoding.UTF8);
            string strLine = reader.ReadLine();

            while ((strLine = reader.ReadLine()) != null)
            {
                if (strLine != "")
                {
                    //Replace The Templte File
                    strLine = strLine.Replace("1DA90003465", dtPrint.Rows[0]["MATNR"].ToString().Trim());
                    strLine = strLine.Replace("MGG52C/A", dtPrint.Rows[0]["KDMAT"].ToString().Trim());
                    strLine = strLine.Replace("174", dtPrint.Rows[0]["MENGE"].ToString().Trim());
                    strLine = strLine.Replace("A1AA06", dtPrint.Rows[0]["LOCAT"].ToString().Trim());
                    strLine = strLine.Replace("20141011103520F", dtPrint.Rows[0]["MBLNR"].ToString().Trim());

                    strOutput = strOutput + strLine;
                }
            }

            if (strOutput != "")
            {
                writeToTempFile(strOutput);
            }
            else
            {
                return;
            }


            #region New Method****

            //OutPut Label
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = @"/c copy " + Application.StartupPath + "\\LabFile.txt com1:";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
            while (true)
            {
                if (cmd.HasExited)
                {
                    break;
                }
            }
            System.Threading.Thread.Sleep(3000);
            #endregion New Method


        }

        public void writeToTempFile(string stroutput)
        {
            try
            {
                if (File.Exists(Application.StartupPath + "\\LabFile.txt"))
                {
                    File.Delete(Application.StartupPath + "\\LabFile.txt");
                }
                FileStream fs = new FileStream(Application.StartupPath + "\\LabFile.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(stroutput);
                sw.Close();
            }
            catch (Exception writeE)
            {
                Console.WriteLine(writeE.Message);
            }
        }

        #endregion

        #region 导出xls文件

        private void CountingResult2File(string strFilePath, string strMsg)
        {
            string strLine = "";
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                sw.WriteLine("ErrorMessage");
                sw.WriteLine(strMsg);
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

        #endregion

        #region Event

        #region Condition

        #region cmbWerks
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region txtLocat
        //DoubleClick
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
                    if (dgvData.Rows.Count > 0)//ShowLocationDataCSMC
                    {
                        StorageIn_LocationSelectCSMC objStorageIn_LocationSelect = new StorageIn_LocationSelectCSMC(UserData, Progid, Werks, Lgort, Type, dgvData.Rows[0].Cells[2].Value.ToString().Substring(0, 2).ToUpper().Trim());
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                    else
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type, this.Ctbto, this.Regon);
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        //KeyPress
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    if (!objPlantData.CheckExistedStorageData(Werks,Lgort, txtLocat.Text.Trim().ToUpper()))
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        stsWarning.Text = "该储位不存在，请输入正确的储位！";
                        this.txtLocat.Text = "";
                        this.txtLocat.Focus();
                        return;
                    }
                    else
                    {
                        #region 將Location 填回Data中
                        foreach (DataRow rsTmpRow in this.Data.Rows)
                        {
                            rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
                        }
                        this.Data.AcceptChanges();
                        #endregion

                        #region 設定按鈕

                        #region Value
                        this.txtBoxID.Text = "";
                        ////Scan
                        //this.txtScannedQty.Text = "";
                        //this.txtPalletQty.Text = "";
                        ////Total
                        //this.txtTotalScanQty.Text = "";
                        //this.txtTotalPalletQty.Text = "";
                        #endregion

                        #region status
                        this.txtBoxID.Enabled = true;
                        //Scan
                        this.txtScannedQty.Enabled = false;
                        this.txtPalletQty.Enabled = false;
                        //Total
                        this.txtTotalScanQty.Enabled = false;
                        this.txtTotalPalletQty.Enabled = false;
                        this.btnSave.Enabled = true;
                        this.txtBoxID.Enabled = true;
                        //this.txtSanLocation.Focus();
                        #endregion

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\Fail.wav");
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        //KeyDown
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            #region 將Location填回CombineData中
            foreach (DataRow rsTmpRow in this.dtCombineData.Rows)
            {
                rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
            }
            this.dtCombineData.AcceptChanges();
            #endregion
        }
        #endregion

        #region rdoNew/rdoAdd

        //rdoNew
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "NEW";
            Type = strType;
        }

        //rdoAdd
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "ADD";
            Type = strType;
            txtLocat.Enabled = true;
        }

        #endregion

        #region txtBoxID

        //KeyDown
        private void txtBoxID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            #region 變數宣告

            stsWarning.Text = string.Empty;
            DataTable dtTemp = new DataTable();
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, cmbWerks.Text.Trim(), Lgort);
            string strPalletID = "";
            bool IsFirstScanPallet = false;
            #endregion

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    #region 校验SF数据存在性

                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

                    DataTable dtbox = new DataTable();
                    //新方法 增加两个Key值查询
                    dtbox = objStorageIn.QueryBoxinfoMultiPallet(Werks, "218", "QMS", txtBoxID.Text.Trim().ToUpper());

                    if (dtbox.Rows.Count > 0)
                    {
                        strPalletID = dtbox.Rows[0]["MBLNR"].ToString().Trim();
                        ShowDdlLgort(dtbox.Rows[0]["LGORT"].ToString().Trim());
                        //记录料号版本
                        if (Data.Rows.Count == 0)
                        {
                            strMatnr = dtbox.Rows[0]["MATNR"].ToString().Trim();
                            strCharg = dtbox.Rows[0]["CHARG"].ToString().Trim();
                            strMType = dtbox.Rows[0]["MTYPE"].ToString().Trim();
                            strPlant = dtbox.Rows[0]["WERKS"].ToString().Trim();
                            strStorage = dtbox.Rows[0]["LGORT"].ToString().Trim();
                        }
                    }
                    else
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        throw new Exception("没有SF数据!!");
                    }

                    #endregion

                    #region 檢查廠區倉別 & 取值

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
                        //strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        //strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {
                        throw new Exception("没有 storage 数据!!");
                    }

                    #endregion

                    #region 取出Box ID

                    string strLocat = txtLocat.Text.Trim();
                    if (string.IsNullOrEmpty(txtBoxID.Text.Trim().ToUpper()))
                    {
                        throw new Exception("Box ID 不能为空!!");
                    }

                    #endregion

                    #endregion

                    #region 检查不同Pallet的信息
                    if (Data.Rows.Count > 0)
                    {
                        if (dtbox.Rows[0]["WERKS"].ToString().Trim() != strPlant)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("厂区不一致!!");
                        }
                        if (dtbox.Rows[0]["LGORT"].ToString().Trim() != strStorage)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("仓别不一致!!");
                        }
                        if (dtbox.Rows[0]["MATNR"].ToString().Trim() != MATNR || dtbox.Rows[0]["CHARG"].ToString().Trim() != CHARG)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("料号版本不一致!!");
                        }
                        if (dtbox.Rows[0]["MTYPE"].ToString().Trim() != MTYPE)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("单据类型不一致!!");
                        }
                    }
                    #endregion

                    #region DataGridView 初始化
                    if (Data != null && Data.Rows.Count > 0)
                    {
                        DataRow[] drScanArr = Data.Select("MBLNR = '" + strPalletID + "' ");
                        //是否第一次刷
                        if (drScanArr.Count() > 0)
                            dtScan = drScanArr.CopyToDataTable<DataRow>();
                        else
                            dtScan = new DataTable();
                    }
                    if (dtScan.Rows.Count == 0)
                    {
                        IsFirstScanPallet = true;
                        #region 確認是否已經SAP扣帳，提示使用僅入QWMS系統
                        if (!CommonInfo.Instance.DBCode.Contains("TEST")) //测试库不需SAP扣帐
                        {
                            //確認是否已經SAP扣帳，提示使用僅入QWMS系統
                            string strResult = "";
                            strResult = objStorageIn.CheckWHTRS(strPalletID);
                            if (strResult != "" && !chkQWMS.Checked)
                            {
                                MessageBox.Show("此Pallet已经扣帐" + strResult + "，请勾选仅入QWMS");
                                throw new Exception("此Pallet已经扣帐" + strResult + "，请勾选仅入QWMS");
                            }
                        }
                        #endregion

                        dtTmpData = new DataTable();
                        //WHDWN可入库 DataSource
                       // dtTmpData = objSapData.QueryQMSLineInDataByBoxID_ALLNEW(strLocat, strPalletID, "", dtpIndat.Value.ToString("yyyyMMdd"), Insmk, strLgort);
                       //合并后用新方法
                        dtTmpData = objSapData.GB_QueryQMSLineInDataByBoxID_ALLNEW(strLocat, strPalletID, "", dtpIndat.Value.ToString("yyyyMMdd"), Insmk, strLgort);

                        if (dtTmpData != null && dtTmpData.Rows.Count > 0)
                        {
                            //dtData 初始化
                            if (Data.Rows.Count == 0)
                                GetDataDefaultTable();
                            //dtScan 初始化
                            GetScanDefaultTable();
                            //dtListBox 初始化
                            GetListBoxDefaultTable();

                            //填值到dtScan
                            SetDataTableValue(ref dtScan, dtTmpData, true);
                            //填值到dtData
                            SetDataTableValue(ref dtData, dtTmpData, false);
                            //ListView Records
                            lblListBox.Text = string.Format("{0} records", dtListBox.Rows.Count);

                            ShowDataGrid();
                        }
                        else
                        {
                            txtBoxID.Text = string.Empty;
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("无数据!!");
                        }
                    }
                    else
                        ShowDataGrid();

                    #endregion

                    #region 刷单笔数据
                    int tempQty = 0;
                    foreach (DataGridViewRow dataGridRow in dgvData.Rows)
                    {
                        //dataGridRow.DefaultCellStyle.BackColor = Color.Silver;
                        if (txtBoxID.Text.Trim().ToUpper() == dataGridRow.Cells[6].Value.ToString().ToUpper())
                        {
                            if (dataGridRow.Cells[0].Value.ToString() == "True")
                            {
                                //绑定ListView
                                ShowListBox(strPalletID);
                                //Condition Qty控件赋值
                                //SetConditionQty(0, strPalletID, IsFirstScanPallet, false);
                                Sound.Play(@"Sound\ERROR.wav");
                                throw new Exception("BOX ID 已經掃過!!");
                            }
                            else
                            {
                                tempQty += Int32.Parse(dataGridRow.Cells[7].Value.ToString());
                                //Data/Scan CHKED栏位赋值
                                SetChekdValue(strPalletID, txtBoxID.Text.Trim());
                                //records显示不正确
                                dataGridRow.Cells[0].Value = true;
                                dataGridRow.DefaultCellStyle.BackColor = Color.Green;
                            }
                        }
                    }

                    if (tempQty == 0)
                    {
                        throw new Exception("Box ID 不存在!!!");
                    }
                    //Condition Qty控件赋值
                    SetConditionQty(tempQty, strPalletID, IsFirstScanPallet, true);

                    if (Int32.Parse(txtScannedQty.Text.ToString()) < Int32.Parse(txtPalletQty.Text.ToString()))
                    {
                        Sound.Play(@"Sound\BIU.wav");
                    }
                    if (Int32.Parse(txtScannedQty.Text.ToString()) == Int32.Parse(txtPalletQty.Text.ToString()))
                    {
                        Sound.Play(@"Sound\OK1.wav");
                    }

                    //绑定ListView
                    ShowListBox(strPalletID);

                    #endregion

                    DataRow[] drScan = Scan.Select("CHKED = True");
                    lblDataCount.Text = string.Format("Scanned {0} of {1} records", drScan.Length, Scan.Rows.Count.ToString());

                    if (txtTotalPalletQty.Text == txtTotalScanQty.Text)
                    {
                        if (strType == "ADD")
                        {
                            //this.txtLocat.Focus();
                            this.txtBoxID.Focus();
                        }
                        this.txtLocat.Focus();
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

        //#region txtSanLocation

        ////KeyDown
        //private void txtSanLocation_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == (char)13)
        //    {
        //        if (txtSanLocation.Text.Trim().ToUpper() != "" && txtSanLocation.Text.Trim().ToUpper() == txtLocat.Text.Trim().ToUpper())
        //        {
        //            txtSanLocation.Text = txtLocat.Text.Trim().ToUpper();
        //            btnSave.Enabled = true;
        //        }
        //        else
        //        {
        //            btnSave.Enabled = false;
        //            Sound.Play(@"Sound\OO.wav");
        //            MessageBox.Show("储位扫描错误！");
        //            return;
        //        }
        //    }
        //}

        //#endregion

        #endregion

        #region Button Event

        //Save Click
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //错误信息
            string strBoxList = string.Empty;
            StringBuilder sbMsg = new StringBuilder();
            try
            {
                if (txtTotalScanQty.Text != txtTotalPalletQty.Text)
                {
                    MessageBox.Show("掃入數量與總數不匹配！");
                    txtBoxID.Focus();
                    return;
                }
                this.btnSave.Enabled = false;

                #region 變數宣告
                SetbtnSaveProcess();
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType,
                    CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
                    CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                #endregion

                #region 防呆機制

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
                                //if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                //    dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                //{
                                //    stsWarning.Text = "该储位中已经存在不同的料号！";
                                //    return;
                                //}
                                if (dtlocation.Rows[i]["MATNR"].ToString().Trim() == dtData.Rows[0]["MATNR"].ToString())
                                {
                                    stsWarning.Text = "该储位中已经存在相同的料号！";
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

                stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";

                #region 正在SAP扣帐

                foreach (DataRow drBOX in dtListBox.Rows)
                {

                    #region 變數宣告

                    string strMtype = string.Empty;
                    stsWarning.Text = "";
                    string[] message = new string[2];
                    ArrayList arrBoxID = new ArrayList();

                    #endregion

                    #region 不讀取SAP回傳的扣帳編號變數

                    bool bolSapStatus = false;
                    AllowToClose = false; //強制User無法關閉視窗
                    string strPalletID = drBOX["MBLNR"].ToString().Trim();
                    string strGrNo = string.Empty;

                    #endregion

                    //取得所有BOX信息
                    GetBOXInfo(strPalletID, ref arrBoxID);
                    strBoxList = string.Join(";", (string[])arrBoxID.ToArray(typeof(string)));

                    #region SAP寫檔

                    #region 合併資料
                    //获取Pallet id 类型是311 QMS_311还是101 QMS 以及归并扣帐数据
                    DataTable dt = objStorageIn.QueryDataForSap(drBOX["MBLNR"].ToString(), out strMtype);

                    if (string.IsNullOrEmpty(strMtype))
                    {
                        sbMsg.Append(strBoxList + ":未获取到单据类型\n");
                        continue;
                    }

                    #endregion

                    //確認是否已經SAP扣帳，提示使用僅入QWMS系統
                    string strResult = "";
                    strResult = objStorageIn.CheckWHTRS(strPalletID);
                    if (strResult != "" && !chkQWMS.Checked)
                    {
                        stsWarning.Text = "此Pallet已经扣帐" + strResult + "，请勾选仅入QWMS";
                        continue;
                    }

                    #region 寫入檔案傳給SAP & 讀取SAP回傳的扣帳編號

                    #region 產生檔案給SAP扣帳但不讀取SAP回傳的扣帳編號

                    #region 回傳扣帳狀態給QMS
                    //測試庫不執行SAP扣帳
                    if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                    {
                        //检查SAP是否已经扣帐
                        if (chkQWMS.Checked)
                        {
                            strGrNo = objStorageIn.CheckWHTRS(strPalletID);
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, null);
                            if (string.IsNullOrEmpty(strGrNo))
                            {
                                objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, "SAP未扣帐不能勾选 重新刷入QWMS");
                                stsWarning.Text = strPalletID + @":在SAP未扣帐，不能勾选 仅入QWMS";
                                sbMsg.Append(strBoxList + ":在SAP未扣帐，不能勾选 仅入QWMS\n");
                                continue;
                            }
                            else
                            {
                                bolSapStatus = true;
                            }
                        }

                        // if (false)  //测试时，不需要与QMS交互
                        //if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                        //{
                        //    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始检查QMS中PalletID是否可用...", null, null);
                        //    message = objStorageIn.TransferPalletIDToQMS(strPalletID, "WMS", "WH Receiving");
                        //    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束检查QMS中PalletID是否可用",
                        //        message[0].ToString().Trim(), message[1].ToString());
                        //    //选择仅入QWMS 略过QMS INFO
                        //    if (!chkQWMS.Checked)
                        //    {
                        //        if (!message[0].ToString().Trim().Equals("Pass", StringComparison.CurrentCultureIgnoreCase))
                        //        {
                        //            stsWarning.Text = message[1].ToString();
                        //            //MessageBox.Show(strPalletID + ":" + message[1].ToString());
                        //            //return;
                        //            sbMsg.Append(strBoxList + ":" + message[1].ToString() + "\n");
                        //            continue;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "选择仅入QWMS，略过检查QMS状态", null, null);
                        //    }
                        //}
                    }
                    #endregion

                    //測試庫不執行SAP扣帳
                    if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                    //测试SAP扣帐
                 //   if (false)//测试不执行产生文档
                    {
                        if (!chkQWMS.Checked)
                        {
                            string strErrorMessage = string.Empty;

                            objLogData.AddQWMSLOG(strPalletID, "SAP", "IN", "开始SAP扣帐", null, null);
                            //返回sap扣帐结果
                            DataTable dtResponse = objStorageIn.WriteSapFile_QSMC_FGNew(dt, strMtype, strWerks);
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
                                else if (dtResponse.Rows[0]["REMARK2"].ToString().Trim().Contains(">"))
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
                                if (!string.IsNullOrEmpty(strErrorMessage))
                                    strErrorMessage = "SAP未扣帳成功，錯誤訊息: " + dtResponse.Rows[0]["REMARK2"].ToString().Trim() + strErrorMessage;
                                //Sound.Play(@"Sound\ERROR.wav");
                            }

                            #region 回传SAP扣帐状态给QMS 解锁
                            //測試庫不執行SAP扣帳
                            if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                            //if (false)
                            {
                                if (!objStorageIn.CheckWHTRS_QMS(strPalletID))
                                {
                                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);
                                    if (!string.IsNullOrEmpty(strGRNoTemp))
                                    {
                                        message = objStorageIn.TransferSAPGRToQMS_SN(strPalletID, strGRNoTemp, "");
                                    }
                                    else
                                    {
                                        message = objStorageIn.TransferSAPGRToQMS_SN(dtResponse.Rows[0]["MBLNR"].ToString(),
                                        dtResponse.Rows[0]["RESULT"].ToString().Trim(),
                                        dtResponse.Rows[0]["REMARK2"].ToString().Trim());
                                    }

                                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态", message[0].ToString().Trim(), message[1].ToString());
                                }
                            }

                            #endregion
                            if (bolSapStatus)
                            {
                                AllowToClose = true;
                                MessageBox.Show("SAP Posting OK!!SAP扣帐编号:" + dtResponse.Rows[0]["RESULT"].ToString().Trim());
                                //stsWarning.Text = "SAP Posting OK!!SAP扣帐编号:" + dtResponse.Rows[0]["RESULT"].ToString().Trim();
                            }
                            else
                            {
                                stsWarning.Text = strErrorMessage;
                                sbMsg.Append(strBoxList + ":" + strErrorMessage + "\n");
                                continue;
                            }

                            #endregion
                        }
                        else
                        {
                            #region 回传SAP扣帐状态给QMS 解锁
                            //测试时，不需要与QMS交互
                            if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                            //if (false)
                            {
                                if (!objStorageIn.CheckWHTRS_QMS(strPalletID))
                                {
                                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);

                                    message = objStorageIn.TransferSAPGRToQMS_SN(strPalletID, strGrNo, "");

                                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态", message[0].ToString().Trim(), message[1].ToString());
                                }
                            }

                            #endregion
                        }

                    }

                    #endregion

                    #endregion

                    #endregion

                    #region 入QWMS的庫存

                    #region 變數宣告

                    DataRow[] combineRow;
                    StringBuilder sbCombineIndex = new StringBuilder();
                    ArrayList alAllCombine = new ArrayList();
                    DataSet dsData = new DataSet();
                    int intCombineQty = 0;

                    #endregion

                    #region 合併資料
                    //Scan
                    dtScan = Data.Select("MBLNR = '" + strPalletID + "' ").CopyToDataTable<DataRow>();
                    dtCombineData.Clear();
                    dtCombineData = dtScan.Clone();
                    for (int i = 0; i < dtScan.Rows.Count; i++)
                    {
                        #region 每次比對的Index (sbCombineIndex)
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtScan.Rows[i]["MANDT"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtScan.Rows[i]["COMCD"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtScan.Rows[i]["WERKS"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtScan.Rows[i]["LGORT"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and MBLNR='" + dtScan.Rows[i]["MBLNR"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtScan.Rows[i]["MATNR"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtScan.Rows[i]["CHARG"].ToString().Trim() + "'");
                        sbCombineIndex.Append(" and INSMK='" + dtScan.Rows[i]["INSMK"].ToString().Trim() + "'");
                        #endregion

                        if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                        {
                            alAllCombine.Add(sbCombineIndex.ToString());
                            combineRow = dtScan.Select(sbCombineIndex.ToString());
                            intCombineQty = 0;
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineQty += Int32.Parse(combineRow[j]["MENGE"].ToString().Trim());
                            }

                            DataRow drRow = dtCombineData.NewRow();
                            drRow["MANDT"] = dtScan.Rows[i]["MANDT"].ToString().Trim();
                            drRow["COMCD"] = dtScan.Rows[i]["COMCD"].ToString().Trim();
                            drRow["WERKS"] = dtScan.Rows[i]["WERKS"].ToString().Trim();
                            drRow["LGORT"] = dtScan.Rows[i]["LGORT"].ToString().Trim();
                            drRow["LOCAT"] = Locat;
                            drRow["MATNR"] = dtScan.Rows[i]["MATNR"].ToString().Trim();
                            drRow["INSMK"] = dtScan.Rows[i]["INSMK"].ToString().Trim();
                            drRow["MBLNR"] = dtScan.Rows[i]["MBLNR"].ToString().Trim();
                            drRow["CHARG"] = dtScan.Rows[i]["CHARG"].ToString().Trim();
                            drRow["LIFNR"] = dtScan.Rows[i]["LIFNR"].ToString().Trim();
                            drRow["EBELN"] = dtScan.Rows[i]["EBELN"].ToString().Trim();
                            drRow["INDAT"] = dtScan.Rows[i]["INDAT"].ToString().Trim();
                            drRow["MENGE"] = intCombineQty.ToString().Trim();
                            drRow["ALQTY"] = intCombineQty.ToString().Trim();
                            drRow["KOSTL"] = dtScan.Rows[i]["KOSTL"].ToString().Trim();
                            drRow["KDMAT"] = dtScan.Rows[i]["KDMAT"].ToString().Trim();
                            drRow["RMANO"] = dtScan.Rows[i]["RMANO"].ToString().Trim();
                            drRow["BOXID"] = dtScan.Rows[i]["BOXID"].ToString().Trim();
                            drRow["RMAK1"] = dtScan.Rows[i]["RMAK1"].ToString().Trim();
                            drRow["WO"] = dtScan.Rows[i]["WO"].ToString().Trim();
                            drRow["SERNO"] = dtScan.Rows[i]["SERNO"].ToString().Trim();
                            drRow["LOADID"] = dtScan.Rows[i]["LOADID"].ToString().Trim();
                            drRow["MODEL"] = dtScan.Rows[i]["MODEL"].ToString().Trim();
                            drRow["REGION"] = dtScan.Rows[i]["REGION"].ToString().Trim();
                            drRow["PALQTY"] = dtScan.Rows[i]["PALQTY"].ToString().Trim();

                            dtCombineData.Rows.Add(drRow);
                        }
                    }

                    #endregion

                    //合併同一個棧板、料號、工號的數量直接入庫
                    objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "开始QWMS入账", null, null);
                    //SAP QM 运行通过后QWMS方可入库
                    //测试 bolSapStatus = true;
                    if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                    {
                        if (bolSapStatus)
                        {
                            //需修改311入库
                            if (objStorageIn.AddSemiProdOnLineInData(Locat, dtCombineData, arrBoxID, dtScan))
                            {
                                objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "OK", null);
                                stsWarning.Text = "SAP and QWMS Posting OK!!";
                            }
                            else
                            {
                                objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + objStorageIn.ERRMSG,
                                    null);
                                stsWarning.Text = "SAP Posting OK but QWMS Add fail!! " + objStorageIn.ERRMSG;
                                sbMsg.Append(strBoxList + ":" + objStorageIn.ERRMSG + "\n");
                            }
                        }
                        else
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail", "SAP未扣账成功");
                        }
                    }

                    #endregion
                }

                #endregion
                //return;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                sbMsg.Append(strBoxList + ":" + ex.Message + "\n");
                //return;
            }
            finally
            {
                if (!string.IsNullOrEmpty(sbMsg.ToString()))
                {
                    //如果存在异常，导出异常信息
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show("Save异常!!");
                    //导出
                    try
                    {
                        saveFileDialog1.FileName = "ErrorMessage" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string strExportName = saveFileDialog1.FileName;
                            CountingResult2File(strExportName, sbMsg.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                //else
                //{
                //    Sound.Play(@"Sound\OO1.wav");
                //    MessageBox.Show("Save OK!!");
                //}
            }
        }

        //Refresh Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            chkQWMS.Checked = false;
            stsWarning.Text = string.Empty;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            this.gbHeader.Enabled = true;
            this.txtLocat.Text = string.Empty;
            //this.txtSanLocation.Text = string.Empty;
            this.txtBoxID.Text = string.Empty;
            this.dgvData.DataSource = null;
            this.lblDataCount.Text = "0 records";
            this.btnSave.Enabled = false;
            this.dgvData.Columns.Clear();

            SetControlState(true);
            this.cmbLgort.Enabled = false;
            this.cmbLgort.Text = "";
            //this.btnConfirm.Enabled = true;

            this.dtData.Rows.Clear();
            this.dtScan.Rows.Clear();
            this.dtListBox.Rows.Clear();
            this.dtTmpData.Clear();
            this.lbxScanInfo.Items.Clear();

            intTotalPalletQty = 0;
            intTotalScanQty = 0;
            this.txtScannedQty.Text = string.Empty;
            this.txtPalletQty.Text = string.Empty;
            this.txtTotalPalletQty.Text = string.Empty;
            this.txtTotalScanQty.Text = string.Empty;
            this.lblListBox.Text = "0 records";
        }

        //Exit Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion
    }
}
