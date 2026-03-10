using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class StorageIn_OnLineIn_DOA : Form
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
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strFunction = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private ArrayList aryMblnr = new ArrayList();
        private ArrayList arySerno = new ArrayList();
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtData = new DataTable();
        private DataTable dtQuery = new DataTable();
        private DataTable dtTemp = new DataTable();
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
        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
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
        public StorageIn_OnLineIn_DOA(ref UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
    
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
			try
			{
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(),  UserData, Progid);

				//檢查權限
				if(!objStorageIn.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					bolDuplicate = objStorageIn.CheckDuplicatLocat();
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
					{
						this.cmbLgort.SelectedIndex = 0;
					}
				}
			}
			catch(Exception ex)
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

        #region 顯示入庫資料(RMA No.)
        public void ShowRmaDataGrid()
        {
            int TotalInQty = 0;

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

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 80;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 80;
                dgvData.Columns.Add(dgvcLgort);

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

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 140;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMANO";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 110;
                dgvData.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 110;
                dgvData.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Width = 90;
                dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 60;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 80;
                dgvData.Columns.Add(dgvcIndat);

                if (Data.Columns.IndexOf("Item") == -1)
                {
                    Data.Columns.Add("Item");
                }

                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    Data.Rows[i]["Item"] = i + 1;
                    TotalInQty += int.Parse(Data.Rows[i]["MENGE"].ToString());
                }

                dgvData.DataSource = Data;
                lblCount.Text = Data.Rows.Count.ToString() + " records";
                lblMenge.Text = "Total: " + TotalInQty + " pcs";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowRmaDataGrid()");
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
            //Normal入庫不需輸入扣帳編號與刷入Serial No.
            if (rdoNormal.Checked == true)
            {
                txtMblnr.Enabled = false;
                txtSerno.Enabled = false;
            }
            if (rdoRmano.Checked == true || rdoSerno.Checked)
            {
                txtMblnr.Enabled = true;
            }
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
            //Normal入庫不需輸入扣帳編號與刷入Serial No.
            if (rdoNormal.Checked == true)
            {
                txtMblnr.Enabled = false;
                txtSerno.Enabled = false;
            }
            if (rdoRmano.Checked == true || rdoSerno.Checked)
            {
                txtMblnr.Enabled = true;
            }
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

                if (rdoNormal.Checked == true)
                {
                    gbHeader.Enabled = false;
                }
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
                
                if (strFunction == "NORMAL")
                {
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
                else if (strFunction == "RMANO")
                {
                    SapData objSapData = new SapData(UserData, Werks, Lgort);
                    if (Mblnrs.Count > 0)
                    {
                        dtData = objSapData.QuerySapRmaData(Mblnrs);
                    }
                    else
                    {
                        stsWarning.Text = "請先選擇扣帳編號!!";
                        txtMblnr.Focus();
                        return;
                    }

                    if (dtData.Rows.Count > 0)
                    {
                        stsWarning.Text = "";
                        gbHeader.Enabled = false;
                        btnSave.Enabled = true;
                        dtData.Columns.Add("LOCAT");//儲位
                        dtData.Columns.Add("MRGID");//連板料號設定
                        dtData.Columns.Add("OMBLNR");//原扣帳編號
                        dtData.Columns.Add("RMAK1");//Description
                        dtData.Columns.Add("ALQTY");//入庫數量
                        dtData.Columns.Add("INDAT");//入庫日期
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["LOCAT"] = txtLocat.Text.Trim();
                            dtData.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                            dtData.Rows[i]["ALQTY"] = int.Parse(dtData.Rows[i]["MENGE"].ToString());
                        }
                    }

                    ShowRmaDataGrid();
                }
                else if (strFunction == "SERNO")
                {
                    stsWarning.Text = "請連按兩下選擇扣帳編號或是刷入棧板號後按Enter鍵!!";
                    txtMblnr.Focus();
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
                    if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["SERNO"].ToString()) == -1)
                    {
                        strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["SERNO"].ToString() + ";";
                    }
                    else
                    {
                        stsWarning.Text = "The Document No, Serial No, and Part No you input is duplicate!!";
                        SetbtnSaveException();
                        return;
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
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.txtLocat.Enabled = true;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.rdoNormal.Checked = false;
            this.rdoRmano.Checked = false;
            this.rdoSerno.Checked = false;
            this.gbType.Enabled = true;
            this.gbFunction.Enabled = false;
            this.txtLocat.Text = "";
            this.txtMblnr.Text = "";
            this.txtSerno.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;
            this.lblCount.Text = "0 records";
            strWerks = "";
            strLgort = "";
            strInsmk = "";
            arySerno.Clear();
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

        #region rdoNormal_CheckedChanged
        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strFunction = "NORMAL";
            gbType.Enabled = false;
            gbFunction.Enabled = true;
        }
        #endregion

        #region rdoRmano_CheckedChanged
        private void rdoRmano_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strFunction = "RMANO";
            gbType.Enabled = false;
            gbFunction.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region rdoSerno_CheckedChanged
        private void rdoSerno_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strFunction = "SERNO";
            gbType.Enabled = false;
            gbFunction.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region GetMblnrData
        private string GetMblnrData()
        {
            try
            {
                StringBuilder sbMblnr = new StringBuilder();
                sbMblnr.Remove(0, sbMblnr.Length);
                for (int i = 0; i < Mblnrs.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(",");
                    sbMblnr.Append(Mblnrs[i].ToString().Trim());
                }
                return sbMblnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                alMblnrs.Clear();
                if (txtMblnr.Text.Trim() != "")
                {
                    alMblnrs.Add(txtMblnr.Text.Trim());
                }
                StorageIn_SapDataSelect_Rmano objStorageIn_SapDataSelect_Rmano = new StorageIn_SapDataSelect_Rmano(UserData, Werks, Lgort, Progid, aryMblnr, "DOA", Insmk, dtpIndat.Value.ToString("yyyyMMdd"));
                objStorageIn_SapDataSelect_Rmano.ShowDialog();
                Mblnrs = objStorageIn_SapDataSelect_Rmano.Mblnr;
                txtMblnr.Text = GetMblnrData();
                if (Mblnrs.Count > 0)
                {
                    this.txtMblnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region txtMblnr_KeyDown
        private void txtMblnr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = "";
                btnConfirm.Enabled = false;

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

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtData = objStorageData.QueryDOAAllBoxid(txtMblnr.Text.Trim());
                if (dtData.Rows.Count > 0)
                {
                    dtData.Columns.Add("LOCAT");//儲位
                    dtData.Columns.Add("MRGID");//連板料號設定
                    dtData.Columns.Add("OMBLNR");//原扣帳編號
                    dtData.Columns.Add("RMAK1");//Description
                    dtData.Columns.Add("ALQTY");//入庫數量
                    dtData.Columns.Add("INDAT");//入庫日期
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        dtData.Rows[i]["LOCAT"] = txtLocat.Text.Trim();
                        dtData.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                        dtData.Rows[i]["ALQTY"] = int.Parse(dtData.Rows[i]["MENGE"].ToString());
                    }

                    ShowRmaDataGrid();
                    gbHeader.Enabled = false;
                    btnSave.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "No Data!!";
                }
            }
        }
        #endregion

        #region txtSerno_KeyDown
        private void txtSerno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = "";
                btnConfirm.Enabled = false;

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

                if (arySerno.IndexOf(txtSerno.Text.Trim()) != -1)
                {
                    stsWarning.Text = "刷入的Box ID已經重複，請確認!!";
                    txtSerno.Text = "";
                    txtSerno.Focus();
                    return;
                }

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                if (dtData.Rows.Count == 0)
                {
                    dtData = objStorageData.QueryDOABoxid(txtSerno.Text.Trim());

                    //刷入的Box ID有資料
                    if (dtData.Rows.Count > 0)
                    {
                        #region 查詢user所選取的倉別是否有預設的庫別
                        PlantData objPlantData = new PlantData(UserData);
                        strInsmk = objPlantData.GetDefaultInsmk(Lgort);
                        #endregion

                        //查到資料才新增至arySerno中
                        arySerno.Add(txtSerno.Text.Trim());

                        dtData.Columns.Add("LOCAT");//儲位
                        dtData.Columns.Add("MRGID");//連板料號設定
                        dtData.Columns.Add("OMBLNR");//原扣帳編號
                        dtData.Columns.Add("RMAK1");//Description
                        dtData.Columns.Add("ALQTY");//入庫數量
                        dtData.Columns.Add("INDAT");//入庫日期
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            //帶入該倉別所預設的庫別
                            if (strInsmk != "")
                                dtData.Rows[i]["INSMK"] = strInsmk;

                            dtData.Rows[i]["LGORT"] = strLgort;
                            dtData.Rows[i]["LOCAT"] = txtLocat.Text.Trim();
                            dtData.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                            dtData.Rows[i]["ALQTY"] = int.Parse(dtData.Rows[i]["MENGE"].ToString());
                        }

                        ShowRmaDataGrid();
                        cmbWerks.Enabled = false;
                        cmbLgort.Enabled = false;
                        txtLocat.Enabled = false;
                        txtMblnr.Enabled = false;
                        btnSave.Enabled = true;
                        txtSerno.Text = "";
                        txtSerno.Focus();
                    }
                    else
                    {
                        stsWarning.Text = "刷入的Box ID無資料，請確認!!";
                        txtSerno.Text = "";
                        txtSerno.Focus();
                        return;
                    }
                }
                //刷新的Box ID
                else if (dtData.Rows.Count > 0)
                {                    
                    dtQuery = objStorageData.QueryDOABoxid(txtSerno.Text.Trim());
                    if (dtQuery.Rows.Count > 0)
                    {
                        //查到資料才新增至arySerno中
                        arySerno.Add(txtSerno.Text.Trim());

                        //暫存資料
                        dtTemp = dtData.Clone();
                        dtTemp = dtData.Copy();
                        dtData.Clear();

                        //原資料後面附加一筆資料
                        dtTemp.ImportRow(dtQuery.Rows[0]);
                        dtData = dtTemp.Copy();

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            //帶入該倉別所預設的庫別
                            if (strInsmk != "")
                                dtData.Rows[i]["INSMK"] = strInsmk;

                            dtData.Rows[i]["LGORT"] = strLgort;
                            dtData.Rows[i]["LOCAT"] = txtLocat.Text.Trim();
                            dtData.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                            dtData.Rows[i]["ALQTY"] = int.Parse(dtData.Rows[i]["MENGE"].ToString());
                        }
                        dtData.AcceptChanges();
                        ShowRmaDataGrid();
                        txtSerno.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "刷入的Box ID無資料，請確認!!";
                        txtSerno.Text = "";
                        txtSerno.Focus();
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
