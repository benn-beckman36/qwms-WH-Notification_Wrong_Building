using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageIn_OnLineIn_Return : Form
    {
        #region 初始化
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();

        #endregion

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
        #endregion

        #region 主程式
        public StorageIn_OnLineIn_Return(ref UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

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

        # region 顯示入庫資料
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                //筆數
                DataGridViewTextBoxColumn dgvcCount = new DataGridViewTextBoxColumn();
                dgvcCount.DataPropertyName = "Item";
                dgvcCount.HeaderText = "Item";
                dgvcCount.ReadOnly = true;
                dgvcCount.Width = 40;
                dgvData.Columns.Add(dgvcCount);



                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 80;
                dgvData.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 110;
                dgvData.Columns.Add(dgvcMenge);


                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                dgvcZeile.Width = 100;
                dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 100;
                dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvData.Columns.Add(dgvcLifnr);


                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMANO";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 100;
                dgvData.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 100;
                dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                dgvcArbpl.Width = 100;
                dgvData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                dgvcTrntp.Width = 80;
                dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Width = 100;
                dgvData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 80;
                dgvData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "GRLOC";
                dgvcGrloc.HeaderText = "101Location";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 80;
                dgvData.Columns.Add(dgvcGrloc);

                if (Data.Columns.IndexOf("Item") == -1)
                {
                    Data.Columns.Add("Item");
                }

                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    Data.Rows[i]["Item"] = i + 1;
                }


                dgvData.DataSource = Data;
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

        #region 選擇入庫方式-新板入庫(New Pallet)
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region 選擇入庫方式-加料入庫(Add In)
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
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

                //變動儲位才需要檢查儲位有沒有輸入
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    if (strType == "ADD")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            stsWarning.Text = "Please input a location first!!";
                            this.txtLocat.Focus();
                            return;
                        }
                    }
                    if (strType == "NEW")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
                        }
                        else
                        {
                            if (objPlantData.CheckStorageData(Werks, Lgort, Locat))
                            {
                                stsWarning.Text = "The location you input is not a empty location!!";
                                this.txtLocat.Focus();
                                return;
                            }
                        }
                    }

                    if (txtLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Please input a location first!!";
                        this.txtLocat.Focus();
                        return;
                    }


                    if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                    {
                        stsWarning.Text = "The location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    //找出庫別

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);


                    if (this.rdoAdd.Checked)
                    {

                        dtTemp = objStorageData.QueryLocatInsmk(strLocat);
                        if (dtTemp.Rows.Count > 1)
                        {
                            stsWarning.Text = "The location has different stock and you can't add any new Part No!!";
                            return;
                        }
                        strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                    }
                }
                else
                {
                    //固定儲位時Location不能輸入
                    if (this.txtLocat.Text.Trim() != "")
                    {
                        stsWarning.Text = "You can't input location because " + strLgort + " is a fixed-Location storage!!";
                        this.txtLocat.Focus();
                        return;
                    }
                }
                gbHeader.Enabled = false;
                btnAdd.Enabled = true;
                btnAdd_Click(null, null);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    //如果是選擇New, 則第一次按下Add時不限制庫別, 但只要選擇了一個物料, 之後按下Add就依照第一個物料的庫別為主
                    if (rdoNew.Checked)
                    {
                        if (dtData.Rows.Count == 0 || dtData == null)
                        {
                            strInsmk = "";
                        }
                        else
                        {
                            strInsmk = dtData.Rows[0]["INSMK"].ToString();
                        }
                    }
                }
                else
                {
                    strInsmk = "";
                }
                if (CheckIsOpen("StorageIn_OnLineIn_Add"))
                {
                    this.MdiParent.MdiChildren[intFormIndex].Close();
                }
                StorageIn_OnLineIn_Add objStorageIn_OnLineIn_Add = new StorageIn_OnLineIn_Add(UserData, Progid, Werks, Lgort, Locat, Sttyp, Lotyp, dtpIndat.Value.ToString("yyyyMMdd"), "", "", dtData, "DOA", Insmk, "", Duplicate);
                objStorageIn_OnLineIn_Add.MdiParent = this.ParentForm;
                objStorageIn_OnLineIn_Add.Show();
                dtData = objStorageIn_OnLineIn_Add.SapData;
                ShowDataGrid();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
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
                SetbtnSaveProcess();
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString()) == -1)
                    {
                        strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + ";";
                    }
                    else
                    {
                        stsWarning.Text = "The Document No and Part No you input is duplicate!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的單號之前是否有使用連板入庫的方式入庫

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    dtTemp = objStorageData.QueryNotCombineInData(Locat, dtData.Rows[i]["MBLNR"].ToString(), "0");
                    if (dtTemp.Rows.Count > 0)
                    {
                        stsWarning.Text = "The Document No was stored in with mixed material last time!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        SetbtnSaveException();
                        return;
                    }

                    //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                    if (this.Duplicate == false)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                }

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.AddOnLineInData(Locat, dtData))
                {
                    stsWarning.Text = "Add OK!!";

                    this.btnAdd.Enabled = false;
                    this.btnSave.Enabled = false;

                    #region 增加和ASRS接口

                    QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);

                    if (objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
                    {

                        DataTable dtASRS = new DataTable();
                        dtASRS.TableName = "QWMS";

                        dtASRS.Columns.Add("TRN_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("SEQ_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("TRN_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("LOC", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("ITEM_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STK", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VER", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VENDOR", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("QTY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PUR_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PO_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PLANT", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PRIORITY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STORAGE_TYPE", typeof(string)).DefaultValue = string.Empty;

                        int j = 1;
                        string strTRN_NO = objInterface.CreateAsrsNo();

                        foreach (DataRow dr in dtData.Rows)
                        {
                            DataRow drASRS = dtASRS.NewRow();

                            drASRS["TRN_NO"] = strTRN_NO;
                            drASRS["SEQ_NO"] = objInterface.Createseq_no(j);
                            drASRS["TRN_TYPE"] = "G+";
                            drASRS["LOC"] = dr["LOCAT"];
                            drASRS["ITEM_NO"] = dr["MATNR"];
                            drASRS["STK"] = dr["INSMK"];
                            drASRS["VER"] = dr["CHARG"];
                            drASRS["VENDOR"] = dr["LIFNR"];
                            drASRS["QTY"] = dr["ALQTY"];
                            drASRS["PUR_TYPE"] = string.Empty;
                            drASRS["PO_NO"] = dr["EBELN"];
                            drASRS["PLANT"] = dr["WERKS"];
                            drASRS["PRIORITY"] = string.Empty;
                            drASRS["STORAGE_TYPE"] = dr["LGORT"];

                            dtASRS.Rows.Add(drASRS);
                            j++;
                        }
                        //bool bolresult = objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false;

                        if (objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false)
                        {
                            stsWarning.Text = "Add OK!!,数据已同步到ASRS";
                        }
                    }

                    #endregion
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

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;
            this.lblCount.Text = "0 records";
            strWerks = "";
            strLgort = "";
            strInsmk = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region CheckIsOpen
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

        #region SetbtnSaveProcess
        private void SetbtnSaveProcess()
        {
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;

        }
        #endregion

        #region SetbtnSaveException
        private void SetbtnSaveException()
        {
            this.btnAdd.Enabled = true;
            this.btnSave.Enabled = true;
        }
        #endregion
    }
}
