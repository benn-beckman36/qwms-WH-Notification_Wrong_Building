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
using QCI.QWMS;

namespace QWMS
{
    public partial class TransferOut_SemiProduct_Confirm : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private int intTotalDocQty = 0;
        private int intTotalInvQty = 0;
        private ArrayList aryBoxID = new ArrayList();
        private ArrayList arySN = new ArrayList();
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtData = new DataTable();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtTmpStorage = new DataTable();
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

        #endregion
        #region 建構式
        public TransferOut_SemiProduct_Confirm(UserInfo varUserData, string strProgid)
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
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
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

        #region cmbWerks_SelectedIndexChanged
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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

        #region ShowOutSourceDataGrid
        private void ShowOutSourceDataGrid()
        {
            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 130;
                dgvcMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcZeile);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcKdmat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //DCQTY
                DataGridViewTextBoxColumn dgvcDcqty = new DataGridViewTextBoxColumn();
                dgvcDcqty.DataPropertyName = "DCQTY";
                dgvcDcqty.HeaderText = "Document Qty";
                dgvcDcqty.Width = 90;
                dgvcDcqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcDcqty);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALMNG";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = false;
                dgvOutSource.Columns.Add(dgvcAlqty);

                //SCQTY
                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "Scanned Qty";
                dgvcScqty.Width = 90;
                dgvcScqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcScqty);

                dgvOutSource.DataSource = dtOutSource;
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " record(s)";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        #region ShowStorageDataGrid
        private void ShowStorageDataGrid()
        {
            dgvStorage.AutoGenerateColumns = false;
            dgvStorage.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 130;
                dgvcMblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMblnr);

                //BOXID
                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "Box ID";
                dgvcBoxid.Width = 100;
                dgvcBoxid.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBoxid);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "Serial NO";
                dgvcSERNO.Width = 90;
                dgvcSERNO.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcSERNO);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMatnr);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLocat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 60;
                dgvcInsmk.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcInsmk);

                //LOCAT
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "ALQTY";
                dgvcMenge.HeaderText = "BoxID Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMenge);

                dgvStorage.DataSource = dtStorage;
                lblStorage.Text = dtStorage.Rows.Count.ToString() + " record(s)";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ResetPage
        private void ResetPage()
        {
            this.txtBoxid.Text = string.Empty;
            this.txtMblnr.Text = string.Empty;
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.lblOutSource.Text = "0 record(s)";
            this.lblStorage.Text = "0 record(s)";
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
        }
        #endregion


        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 變數宣告
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                #endregion
                //#region 加總單據的出庫總數量
                //intTotalDocQty = 0;
                //for (int i = 0; i < dtOutSource.Rows.Count; i++)
                //{
                //    intTotalDocQty += int.Parse(dtOutSource.Rows[i]["MENGE"].ToString().Trim());
                //}
                //#endregion
                //#region 加總刷入的Box ID的庫存總數量
                //intTotalInvQty = 0;
                //for (int i = 0; i < dtStorage.Rows.Count; i++)
                //{
                //    intTotalInvQty += int.Parse(dtStorage.Rows[i]["ALQTY"].ToString().Trim());
                //}
                //#endregion
                //if (intTotalDocQty != intTotalInvQty)
                //{
                //    stsWarning.Text = "刷入的BOX ID總數量不等於SAP單據的數量，請確認!!";
                //    return;
                //}

                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    int intTotalSapDocQty = int.Parse(dtOutSource.Rows[i]["ALMNG"].ToString().Trim());
                    int intTotalScannedQty = int.Parse(dtOutSource.Rows[i]["SCQTY"].ToString().Trim());

                    if (intTotalSapDocQty != intTotalScannedQty)
                    {
                        string strMblnr = dtOutSource.Rows[i]["MBLNR"].ToString().Trim();
                        MessageBox.Show(string.Format("刷入的BOX ID總數量不等於SAP單據的數量，SAP單據：{0}，請確認!!", strMblnr), "提醒");
                        return;
                    }
                }

                if (objStorageOut.AddSemiProductTransferOut_Confirm(dtOutSource, dtStorage))
                {
                    stsWarning.Text = "出庫扣帳OK!!";
                    this.btnSave.Enabled = false;
                    ResetPage();
                    return;
                }
                else
                {
                    stsWarning.Text = "Save fail!! " + objStorageOut.ERRMSG;
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                this.btnSave.Enabled = false;
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ResetPage();
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }
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
                TransferIn_SapDataSelect objTransferIn_SapDataSelect
                                 = new TransferIn_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "TRANSFEROUT");
                objTransferIn_SapDataSelect.ShowDialog();
                Mblnrs = objTransferIn_SapDataSelect.Mblnr;
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

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] { ',' });
                Mblnrs.Clear();
                if (Mblnrs.Count == 0)
                {
                    for (int i = 0; i < aryTempMblnr.Length; i++)
                    {
                        Mblnrs.Add(aryTempMblnr[i].ToString().Trim());
                    }
                }

                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                //單號不為空
                if (Mblnrs.Count == 0)
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                dtOutSource = objSapData.QuerySapLineOutData(Mblnrs, "ONLINE");

                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    txtBoxid.Enabled = true;
                    txtSn.Enabled = true;
                    txtBoxid.Focus();
                    ShowOutSourceDataGrid();
                   
                    btnConfirm.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region txtBoxid_KeyDown
        private void txtBoxid_KeyDown(object sender, KeyEventArgs e)
        {
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = "";

                #region 防呆限制

                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "SAP单据不能为空，请确认！！";
                    txtBoxid.Focus();
                    return;
                }

                if (txtBoxid.Text.Trim() == "")
                {
                    stsWarning.Text = "刷入的Box ID不能為空，請確認!!";
                    txtBoxid.Focus();
                    return;
                }
                //if (aryBoxID.Contains(txtBoxid.Text.Trim()))
                //{
                //    stsWarning.Text = "刷入的Box ID重複，請確認!!";
                //    txtBoxid.Text = "";
                //    txtBoxid.Focus();
                //    return;
                //}
                //else
                //{
                if (objSapData.GetBoxIDHoldStatus(this.txtBoxid.Text.Trim()))
                {
                    stsWarning.Text = "刷入的Box ID被Hold住，請確認!!";
                    txtBoxid.Text = "";
                    txtBoxid.Focus();
                    return;
                }
                //else
                //{
                //    //if (objSapData.GetCheckOutBySN(this.txtBoxid.Text.Trim()))
                //    //{
                //    //    stsWarning.Text = "刷入的Box ID已经做过SN出库，不能再刷BOX ID,请刷SN入库!!";
                //    //    txtBoxid.Text = "";
                //    //    txtBoxid.Focus();
                //    //    return;
                //    //}
                //    //else
                //    //{
                //    aryBoxID.Add(txtBoxid.Text.Trim());
                //    //}
                //}
                //}

                #endregion

                try
                {
                    dtTmpData = objSapData.GetStorageDataByBoxID(this.txtBoxid.Text.Trim());
                    if (dtTmpData.Rows.Count > 0)
                    {
                        string strMatnr = dtTmpData.Rows[0]["MATNR"].ToString().Trim();
                        string strInsmk = dtTmpData.Rows[0]["INSMK"].ToString().Trim();
                        string strCharg = dtTmpData.Rows[0]["CHARG"].ToString().Trim();
                        string strSelectConditions = string.Format("MATNR = '{0}' AND INSMK='{1}' AND CHARG='{2}'", strMatnr, strInsmk, strCharg);
                        DataRow[] drsOutSource = dtOutSource.Select(strSelectConditions);
                        //brian 20150304 增加比对SAP单据与刷入的料号版本是否一致
                        if (drsOutSource.Length > 0)
                        {
                            if (dtStorage.Rows.Count == 0)
                            {
                                dtStorage = dtTmpData.Clone();
                            }

                            for (int i = 0; i < dtTmpData.Rows.Count; i++)
                            {
                                //dtStorage.ImportRow(dtTmpData.Rows[i]);
                                DataRow[] drsSelected;
                                //防呆：已刷入SN，再刷SN所在的BOX
                                string strBOXID = dtTmpData.Rows[i]["BOXID"].ToString().Trim();

                                drsSelected = dtStorage.Select("BOXID = '" + strBOXID + "' AND SERNO <> ''");

                                if (drsSelected.Length > 0)
                                {
                                    MessageBox.Show(string.Format("此Box ID：{0} 已经有刷过SN，请确认！", strBOXID), "警告");

                                    txtBoxid.Text = "";
                                    txtBoxid.Focus();
                                    return;
                                }

                                drsSelected = dtStorage.Select("BOXID = '" + strBOXID + "'");

                                if (drsSelected.Length > 0)
                                {
                                    stsWarning.Text = "刷入的Box ID重複，請確認!!";
                                    txtBoxid.Text = "";
                                    txtBoxid.Focus();
                                    return;
                                }
                                dtStorage.ImportRow(dtTmpData.Rows[i]);

                                drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTmpData.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                            }

                            ShowStorageDataGrid();
                            txtBoxid.Text = "";
                            txtBoxid.Focus();
                        }
                        else
                        {
                            stsWarning.Text = "刷入的BOXID，料号或版本与SAP单据不符，请确认！！";
                            txtBoxid.Text = "";
                            txtBoxid.Focus();
                        }
                    }
                    else
                    {
                        stsWarning.Text = "查無此票Box ID，請確認!!";
                        txtBoxid.Text = "";
                        txtBoxid.Focus();
                        return;
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

        private void txtSn_KeyPress(object sender, KeyPressEventArgs e)
        {
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

            if (e.KeyChar == (char)13)
            {
                //if (checkSn.Checked == true)
                //{
                //    if (txtfixSn.Text.Trim() != "")
                //    {
                //        if (txtSn.Text.Length > 0)
                //        {
                //            if (txtSn.Text.Trim().Substring(0, 1).ToUpper() == "S")
                //            {
                //                txtSn.Text = txtSn.Text.Trim().Substring(1, txtSn.Text.Trim().Length - 1);
                //            }

                //            if (txtfixSn.Text.Trim().Substring(0, 1).ToUpper() == "S")
                //            {
                //                txtfixSn.Text = txtfixSn.Text.Trim().Substring(1, txtSn.Text.Trim().Length - 1);
                //            }

                //            if (txtSn.Text.Trim() == txtfixSn.Text.Trim())
                //            {
                //                if (!objSapData.GetBoxIDHoldStatusBySn(txtSn.Text.Trim()))
                //                {
                //                    DataTable dtTemp = new DataTable();
                //                    dtTemp = objSapData.GetBoxIDStorageDataBySn(txtSn.Text.Trim());

                //                    if (dtTemp.Rows.Count > 0)
                //                    {

                //                        if (dtStorage.Rows.Count == 0)
                //                        {
                //                            dtStorage = dtTemp.Clone();
                //                        }

                //                        for (int i = 0; i < dtTmpData.Rows.Count; i++)
                //                        {
                //                            dtStorage.ImportRow(dtTemp.Rows[i]);

                //                        }

                //                        ShowStorageDataGrid();
                //                        txtSn.Text = "";
                //                        txtSn.Focus();
                //                    }
                //                    else
                //                    {
                //                        stsWarning.Text = "查無此票Serial NO，請確認!!";
                //                        txtSn.Text = "";
                //                        txtSn.Focus();
                //                        return;
                //                    }


                //                }
                //                else
                //                {
                //                    MessageBox.Show("SN:" + txtSn.Text.Trim() + "被Hold，不允许出库！");
                //                    return;
                //                }
                //            }
                //            else
                //            {
                //                txtSn.Text = "";
                //                txtSn.Focus();
                //                MessageBox.Show("");
                //                return;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("请输入SN!");
                //        return;
                //    }
                //}
                //else
                //{
                stsWarning.Text = string.Empty;

                #region 刷入SN
                if (txtSn.Text.Trim() == "")
                {
                    stsWarning.Text = "刷入的Serial NO不能為空，請確認!!";
                    txtSn.Focus();
                    return;
                }
                //if (arySN.Contains(txtSn.Text.Trim()))
                //{
                //    stsWarning.Text = "刷入的Serial NO重複，請確認!!";
                //    txtSn.Text = string.Empty;
                //    txtSn.Focus();
                //    return;
                //}
                //else
                //{
                if (!objSapData.GetBoxIDHoldStatusBySn(txtSn.Text.Trim()))
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = objSapData.GetStorageDataBySn(txtSn.Text.Trim());

                    if (dtTemp.Rows.Count > 0)
                    {
                        string strMatnr = dtTemp.Rows[0]["MATNR"].ToString().Trim();
                        string strInsmk = dtTemp.Rows[0]["INSMK"].ToString().Trim();
                        string strCharg = dtTemp.Rows[0]["CHARG"].ToString().Trim();
                        string strSelectConditions = string.Format("MATNR = '{0}' AND INSMK='{1}' AND CHARG='{2}'", strMatnr, strInsmk, strCharg);
                        DataRow[] drsOutSource = dtOutSource.Select(strSelectConditions);
                        //brian 20150304 增加比对SAP单据与刷入的料号版本是否一致
                        if (drsOutSource.Length > 0)
                        {
                            if (dtStorage.Rows.Count == 0)
                            {
                                dtStorage = dtTemp.Clone();
                            }

                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                DataRow[] drsSelect;
                                string strBoxID = dtTemp.Rows[i]["BOXID"].ToString().Trim();
                                string strSerNO = dtTemp.Rows[i]["SERNO"].ToString().Trim();

                                drsSelect = dtStorage.Select("BOXID = '" + strBoxID + "' AND SERNO = ''");

                                if (drsSelect.Length > 0)
                                {
                                    MessageBox.Show(string.Format("此Serial NO：{0} 所在Box ID：{1} 已经存在，请确认！", strSerNO, strBoxID), "警告");

                                    txtSn.Text = "";
                                    txtSn.Focus();
                                    return;
                                }

                                drsSelect = dtStorage.Select("SERNO = '" + strSerNO + "'");

                                if (drsSelect.Length > 0)
                                {
                                    stsWarning.Text = "刷入的Serial NO已存在，请确认！！";

                                    txtSn.Text = "";
                                    txtSn.Focus();
                                    return;
                                }

                                dtStorage.ImportRow(dtTemp.Rows[i]);
                                drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;

                            }

                            //arySN.Add(txtSn.Text.Trim());

                            ShowStorageDataGrid();
                            txtSn.Text = "";
                            txtSn.Focus();
                        }
                        else
                        {
                            stsWarning.Text = "刷入的BOXID，料号或版本与SAP单据不符，请确认！！";
                            txtSn.Text = "";
                            txtSn.Focus();
                        }
                    }
                    else
                    {
                        stsWarning.Text = "查無此票Serial NO，請確認!!";
                        txtSn.Text = "";
                        txtSn.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("SN:" + txtSn.Text.Trim() + "被Hold，不允许出库！");
                    return;
                }
                //}
                #endregion
            }
        }
    }
}