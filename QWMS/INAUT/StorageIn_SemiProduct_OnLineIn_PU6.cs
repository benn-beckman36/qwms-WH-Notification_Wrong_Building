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
using QWMS.PP;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace QWMS
{
    public partial class StorageIn_SemiProduct_OnLineIn_PU6 : Form
    {
        #region Constructor

        #region 变量
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

        public string SN
        {
            get { return strSN; }
            set { strSN = value; }
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

        #region  变量
        public string strMandt = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strProgid = "";
        public string strMatnr = "";
        public string strMblnr = "";
        public string strType = "";
        public string strMethod = "";
        public string strInsmk = "";
        public string strSttyp = "";
        public string strLotyp = "";
        public string strComcd = "";
        private string strSN = "";

        public DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtCombineData = new DataTable();
        private DataTable dtCombineInventory = new DataTable();
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
        private ArrayList arrBoxID = new ArrayList();
        DataTable dtAllSNByPalletID = new DataTable();
        DataTable dtBoxIds = new DataTable();
        DataTable dtSNInfo = new DataTable();
        DataTable dtSAP;
        DataRow drRow;
        string strLastBXID = "";
        string strCurrentBoxId = "";
        string strPalletId = "";
        QCI.QWMS.StorageIn objStorageIn;
        QCI.QWMS.PlantData objPlantData;
        QCI.QWMS.LogData objLogData;

        int intScanBoxQty = 0;
        int intScanPalQty = 0;
        int intBoxTotalQty = 0;
        int intPalTotalQty = 0;
        Dictionary<DataRow, DataGridViewRow> dicMapping = new Dictionary<DataRow, DataGridViewRow>();

        #endregion

        public StorageIn_SemiProduct_OnLineIn_PU6(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;
            try
            {
                objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                objPlantData = new QCI.QWMS.PlantData(UserData);
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        # region DataMember

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

        private bool CheckLgortWithAuth()
        {
            bool bolResult = false;
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (!string.IsNullOrEmpty(strWerks) && !string.IsNullOrEmpty(strLgort))
                {
                    DataTable dtTemp = objAuthority.CheckLgortWithAuth(strWerks, strLgort);

                    if (dtTemp.Rows.Count <= 0)
                    {
                        stsWarning.Text = "没有该仓别的操作权限，请确认！";
                    }
                    else
                    {
                        ShowDdlLgort();
                        cmbWerks.SelectedItem = strWerks;
                        cmbLgort.SelectedItem = strLgort;
                        cmbLgort.Enabled = false;
                        cmbWerks.Enabled = false;
                        bolResult = true;
                    }
                }
                else
                {
                    stsWarning.Text = "请先刷入SN";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckLgortWithAuth()");
            }
            return bolResult;
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
                    }
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
            this.dgvData.AllowUserToAddRows = false;
            try
            {
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                // dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.ReadOnly = true;
                dgvcChecked.Width = 50;
                this.dgvData.Columns.Add(dgvcChecked);

                DataGridViewTextBoxColumn SernoStyle = new DataGridViewTextBoxColumn();
                SernoStyle.DataPropertyName = "SERNO";
                SernoStyle.Name = "SERNO";
                SernoStyle.HeaderText = "SN";
                SernoStyle.ReadOnly = true;
                SernoStyle.Width = 150;
                this.dgvData.Columns.Add(SernoStyle);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
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
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn LoadID = new DataGridViewTextBoxColumn();
                LoadID.DataPropertyName = "LOADID";
                LoadID.HeaderText = "CPU规格";
                LoadID.ReadOnly = true;
                LoadID.Width = 80;
                this.dgvData.Columns.Add(LoadID);

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.Width = 130;
                boxidStyle.Name = "BOXID";
                boxidStyle.ReadOnly = true;
                this.dgvData.Columns.Add(boxidStyle);


                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "LGORT";
                dgvcStorage.HeaderText = "Storage";
                dgvcStorage.Width = 60;
                dgvcStorage.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcStorage);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Items";
                dgvcZeile.Width = 50;
                dgvcZeile.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcZeile);


                dgvData.DataSource = dtData;

                lblCount.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #region ShowDataGrid
        public void ShowMatnrDataGrid()
        {
            this.dgvCombineMatnr.AutoGenerateColumns = false;
            this.dgvCombineMatnr.Columns.Clear();
            this.dgvCombineMatnr.AllowUserToAddRows = false;
            try
            {
                DataTable dtTemp = dtSAP.Copy();
                dtTemp.Columns.Remove("PLANT");
                dtTemp.Columns.Remove("POST_OUT");
                dtTemp.Columns.Remove("ERROR_STRING");
                dtTemp.Columns.Remove("STATUS");
                DataColumn dcPallid = new DataColumn();

                dcPallid.ColumnName = "PallID";
                dcPallid.DefaultValue = strPalletId;
                dtTemp.Columns.Add(dcPallid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "PallID";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORTF";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.Width = 70;
                dgvcLgort.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "LGORTT";
                dgvcUmlgo.HeaderText = "线上仓";
                dgvcUmlgo.Width = 70;
                dgvcUmlgo.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcUmlgo);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "RETQTY";
                dgvcMenge.HeaderText = " Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvCombineMatnr.Columns.Add(dgvcMenge);

                dgvCombineMatnr.DataSource = dtTemp;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #region GetDefaultTable
        private void GetDefaultTable()
        {
            if (Data.Rows.Count == 0)
            {
                Data = new DataTable();
                //Data.Columns.Add("CHKED", typeof(Boolean));
                Data.Columns.Add("CHKED", Type.GetType());
                Data.Columns.Add("MANDT", Type.GetType());
                Data.Columns.Add("COMCD", Type.GetType());
                Data.Columns.Add("WERKS", Type.GetType());
                Data.Columns.Add("LGORT", Type.GetType());
                Data.Columns.Add("UMLGO", Type.GetType());
                Data.Columns.Add("LOCAT", Type.GetType());
                Data.Columns.Add("MATNR", Type.GetType());
                Data.Columns.Add("INSMK", Type.GetType());
                Data.Columns.Add("CHARG", Type.GetType());
                Data.Columns.Add("MENGE", Type.GetType());
                Data.Columns.Add("ALQTY", Type.GetType());
                Data.Columns.Add("MBLNR", Type.GetType());
                Data.Columns.Add("ZEILE", Type.GetType());
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
                Data.Columns.Add("REFID", Type.GetType());

                //DataColumn[] dcPrimaryKey = new DataColumn[3];
                //dcPrimaryKey[0] = Data.Columns["MBLNR"]; 
                //dcPrimaryKey[1] = Data.Columns["BOXID"];
                //dcPrimaryKey[2] = Data.Columns["WO"];

                // DataColumn[] dcPrimaryKey = new DataColumn[1];
                // dcPrimaryKey[0] = Data.Columns["MBLNR"];
                //dcPrimaryKey[1] = Data.Columns["ZEILE"]; 
                //Data.PrimaryKey = dcPrimaryKey;
            }
        }
        #endregion
        #endregion

        # endregion

        #region Function

        #region Intype

        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "NEW";
            Type = strType;
            this.txtSn.Focus();
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "ADD";
            Type = strType;
            this.txtSn.Focus();
        }

        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            strLgort = cmbLgort.Text.ToString();

            #region 储位防呆
            if (Locat == "")
            {
                stsWarning.Text = "请先选择储位";
                SetErrException();
                this.txtLocat.Focus();
                return;
            }
            #endregion

            if (strMethod == "FATP")
            {
                #region 判断若是FATP入库，若是整理数据，并同步给SAP
                try
                {
                    #region 同步数据到SAP
                    stsWarning.Text = "SAP开始入账，请勿关闭视窗";
                    DataSet ds = SendToSAP(dtSAP, "Z_RFC_SCRAP_ZSC1_POST");
                    DataTable dt = ds.Tables[0];  //正式
                    //DataTable dt = dtSAP;  //测试               
                    if (dt.Rows.Count > 0)
                    {
                        string strGrno = dt.Rows[0]["POST_OUT"].ToString();
                        string strMessage = dt.Rows[0]["ERROR_STRING"].ToString();
                        string strRefnum = dt.Rows[0]["FileName"].ToString();
                        //string strRefnum = "TEST";//测试
                        string strStatus = "Y";
                        if (string.IsNullOrEmpty(strGrno))
                        {
                            strStatus = "N";
                            strGrno = strMessage;
                            stsWarning.Text = strGrno;
                            objLogData.AddSAPData("SAP", strPalletId, strRefnum, strStatus, strGrno, strUsrnm);
                            MessageBox.Show("同步SAP异常，" + strGrno + "");
                            return;
                        }
                        else
                        {
                            objLogData.AddSAPData("SAP", strPalletId, strSN, strStatus, strGrno, strUsrnm);
                            SaveInQWMStock();
                        }

                    }
                    #endregion
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString() + "==>同步SAP数据出错");
                    SetInit();
                    return;
                }
                #endregion
            }
            else
            {
                #region 同步BGM数据到SAP ,并入库QWMS
                if (strLgort.Substring(0, 2) == "SC")
                {
                    try
                    {
                        GetOtherDataToSAP();
                        DataRow dr = dtSAP.NewRow();
                        dr["P_MANDT"] = Mandt;
                        dr["P_MJAHR"] = objStorageIn.QuaryMCYear(strPalletId);
                        dr["P_WERKS"] = strWerks;
                        dr["P_SCPNO"] = strPalletId;
                        dtSAP.Rows.Add(dr.ItemArray);
                        DataSet ds = SendToSAP(dtSAP, "Z_RFC_SCRAP_ZSC1_POST");
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            string strMessage = dt.Rows[0]["REMARK"].ToString();
                            string strFlage = dt.Rows[0]["FLAG"].ToString();
                            if (strFlage == "Y")
                            {
                                objLogData.AddSAPData("SAP", strPalletId, "Z_RFC_SCRAP_ZSC1_POST", "Y", strMessage, strUsrnm);
                                SaveInQWMStock();
                            }
                            else
                            {
                                objLogData.AddSAPData("SAP", strPalletId, "Z_RFC_SCRAP_ZSC1_POST", "N", strMessage, strUsrnm);
                                stsWarning.Text = "SAP 扣帐失败 " + strMessage;
                                return;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString() + "==>同步SAP，RFC:Z_RFC_SCRAP_ZSC1_POST 出错");
                        objLogData.AddSAPData("SAP", strPalletId, "Z_RFC_SCRAP_ZSC1_POST", "N", "同步异常", strUsrnm);
                        SetInit();
                        return;
                    }
                }
                else
                {
                    #region 非SC仓入库
                    try
                    {
                        GetSCDataToSAP();
                        DataRow dr = dtSAP.NewRow();
                        dr["DYEAR"] = objStorageIn.QuaryMCYear(strPalletId);
                        dr["MBLNR"] = dtData.Rows[0]["INSMK"].ToString() + strPalletId;
                        dtSAP.Rows.Add(dr.ItemArray);
                        DataSet ds = SendToSAP(dtSAP, "ZRFC_PP_QWMS_DEBIT");
                        DataTable dtDataFromSAP = ds.Tables[0];
                        // DataTable dt = dtSAP;//测试

                        string strZEMSG = string.Empty;
                        string strZSUCC = string.Empty;
                        string strFailMatnr = string.Empty;

                        if (dtDataFromSAP.Rows.Count > 0)
                        {
                            //if (dtDataFromSAP.Rows.Count > 1)
                            //{
                            //DataRow[] drFail = dtDataFromSAP.Select(" WERKS <>'' AND ZSUCC='E' ");
                            DataRow[] drFail = dtDataFromSAP.Select(" ZSUCC='E' ");
                            if (drFail.Length > 0)
                            {
                                strZEMSG = drFail[0]["ZEMSG"].ToString();
                                strZSUCC = drFail[0]["ZSUCC"].ToString();
                                strFailMatnr = drFail[0]["MATNR"].ToString();
                            }
                            else
                            {
                                strZEMSG = dtDataFromSAP.Rows[0]["ZEMSG"].ToString();
                                strZSUCC = dtDataFromSAP.Rows[0]["ZSUCC"].ToString();
                            }
                            //}
                            // strMessageFromSAP = strZEMSG;
                            if (strZSUCC == "S")
                            {
                                objLogData.AddQWMSLOG(strPalletId, "SAP", "ZRFC_PP_QWMS_DEBIT", "Y", strZSUCC, strZEMSG);
                            }
                            else if (strZSUCC == "E")
                            {
                                if (strZEMSG.Contains(":has been posted!"))
                                {
                                    objLogData.AddQWMSLOG(strPalletId, "SAP", "ZRFC_PP_QWMS_DEBIT", "Y", strZSUCC, strZEMSG);
                                }
                                else
                                {
                                    objLogData.AddQWMSLOG(strPalletId, "SAP", "ZRFC_PP_QWMS_DEBIT", "Y", strZSUCC, strFailMatnr + " ," + strZEMSG);
                                    MessageBox.Show("扣账失败！失败原因料号：" + strFailMatnr + " ， " + strZEMSG + "");
                                    return;
                                }
                            }

                            SaveInQWMStock();
                        }
                        else
                        {
                            MessageBox.Show("读取SAP扣账文档失败，请联系系统负责人维护SN状态，重新入库");
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString() + "==>同步SAP，RFC:ZRFC_PP_QWMS_DEBIT 出错");
                        objLogData.AddSAPData("SAP", strPalletId, "ZRFC_PP_QWMS_DEBIT", "N", "同步出错", strUsrnm);
                        SetInit();
                        return;
                    }

                    #endregion
                }
                #endregion
            }

        }

        #endregion

        #region txtLocat
        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    Locat = txtLocat.Text.Trim();
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    DataTable dtLocat = new DataTable();

                    #region 检查输入的储位是否允许入库

                    if (Type.ToUpper() == "NEW")
                    {
                        dtLocat = objPlantData.GetAllLocatData(strWerks, strLgort, Locat, "0", "", "");
                    }
                    if (Type.ToUpper() == "ADD")
                    {
                        dtLocat = objPlantData.GetAllLocatData(strWerks, strLgort, Locat, "1", "", "");
                    }
                    if (dtLocat.Rows.Count < 0)
                    {
                        stsWarning.Text = "该储位不存在，请确认！";
                        return;
                    }
                    #endregion

                    this.btnConfirm.Enabled = true;
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\Fail.wav");
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }


        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Werks = strWerks;
                Lgort = strLgort;

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "请先刷入SN，自动带出厂区仓别信息--->txtLocat_DoubleClick";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    this.btnConfirm.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        #endregion

        #region txtSn_KeyDown
        private void txtSn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow drRow;
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, "", "");
                strSN = txtSn.Text.ToString().ToUpper().Trim();
                stsWarning.Text = "";

                if (string.IsNullOrEmpty(strType))
                {
                    stsWarning.Text = "请先选择入库方式";
                    SetErrException();
                    return;
                }
                gbInType.Enabled = false;
                try
                {
                    if (!string.IsNullOrEmpty(strSN))
                    {
                        #region 获取SN信息
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
                        dtSNInfo = objSapData.GetSNInfo(strSN, strWerks, strLgort);
                        #region 主动获取OA信息
                        if (dtSNInfo.Rows.Count == 0)
                        {
                            stsWarning.Text = "该SN在QWMS无数据，请联系QMS人员同步数据！";
                            return;
                        }
                        if (dtSNInfo.Rows[0]["REFID"].ToString() != "IWorkFlow")
                        {
                            DataTable dtTemp = objStorageIn.QuaryOAMblnrfromWhdwn(strSN);
                            if (dtTemp.Rows.Count > 0)
                            {
                                //更新数据后再次查询
                                bool flg = GetDataFromOA(dtTemp.Rows[0]["WERKS"].ToString(), dtTemp.Rows[0]["LGORT"].ToString(), dtTemp.Rows[0]["MBLNR"].ToString(), objStorageIn);
                                if (flg)
                                    dtSNInfo = objSapData.GetSNInfo(strSN, strWerks, strLgort);
                                else
                                {
                                    dtSNInfo.Clear();
                                    return;
                                }
                            }
                        }
                        #endregion
                        if (dtSNInfo.Rows.Count > 1)
                        {
                            stsWarning.Text = "该SN有多个厂区/仓别信息，请选择厂区/仓别";
                            return;
                        }  
                                                             
                        if (dtSNInfo.Rows.Count > 0)
                        {
                            #region 检查是否为首次刷入SN，首次刷对参数进行赋值
                            if (Data.Rows.Count > 0)
                            {
                                #region 已经有刷入的SN

                                #region 检查是否重复刷入SN

                                DataRow[] drCheck = Data.Select(" CHKED='Y'  AND SERNO='" + strSN + "' ");
                                if (drCheck.Length > 0)
                                {
                                    stsWarning.Text = "该SN已刷入，请确认";
                                    SetErrException();
                                    return;
                                }

                                #endregion

                                #region 检查是否有未完全刷入的BOXID

                                DataRow[] drRest = Data.Select(" CHKED='N'  AND BOXID='" + strLastBXID + "' ");
                                if (drRest.Length > 0)
                                {
                                    #region 从未刷入的数据中去找，获取未刷完的boxid

                                    //判断刷入的SN是否跟上一次刷入的是否属于同一个BOXID
                                    if (dtSNInfo.Rows[0]["BOXID"].ToString() == strLastBXID)
                                    {
                                        SetChecked(strSN, strLastBXID);
                                        intScanBoxQty += Convert.ToInt32(dtSNInfo.Rows[0]["MENGE"].ToString());
                                        intScanPalQty += Convert.ToInt32(dtSNInfo.Rows[0]["MENGE"].ToString());
                                        //同一个BOXID刷完的情况
                                        if (intScanBoxQty == intBoxTotalQty)
                                        {
                                            SetSuccessByBxID();
                                        }
                                        else
                                        {
                                            Sound.Play(@"Sound\ready.wav");
                                        }
                                    }
                                    else
                                    {
                                        SetErrException();
                                        MessageBox.Show(" BOXID:" + strLastBXID + "还有未刷入的SN，请先刷入该BOXID对应的所有SN");
                                        //stsWarning.Text = " BOXID:" + strLastBXID + "还有未刷入的SN，请先刷入该BOXID对应的所有SN";
                                        return;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 上个BOXID完全刷入，重新开始一个BOXID
                                    strCurrentBoxId = strLastBXID = dtSNInfo.Rows[0]["BOXID"].ToString();
                                    intScanBoxQty = 0;
                                    DataRow[] drSelect = Data.Select("SERNO='" + strSN + "' AND BOXID='" + strLastBXID + "' ");
                                    DataRow[] drBxids = Data.Select("BOXID='" + strLastBXID + "' ");
                                    intBoxTotalQty = drBxids.Length;
                                    txtCurBXToQty.Text = intBoxTotalQty.ToString();
                                    if (drSelect.Length > 0)
                                    {
                                        //先刷入的SN，存在data 设置为已勾选的状态
                                        intScanBoxQty += 1;
                                        intScanPalQty += 1;
                                        SetChecked(strSN, strCurrentBoxId);
                                        getQMSPicture();
                                        if (intScanBoxQty == intBoxTotalQty)
                                        {
                                            SetSuccessByBxID();                                          
                                        }
                                        else
                                        {
                                            Sound.Play(@"Sound\ready.wav");
                                        }
                                    }
                                    else
                                    {
                                        stsWarning.Text = "该SN不存在该PalltID里面，请确认！";
                                        SetErrException();
                                        return;
                                    }

                                    #endregion
                                }
                                #endregion
                                #endregion
                            }
                            else
                            {
                                #region 第一次刷入SN, 通过sn获取厂区，仓别，对应的boxid 和palletid
                                //修复不同厂区出现同一个SN的情况
                                if (cmbWerks.SelectedIndex != -1)
                                {
                                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                                }
                                else
                                {
                                    strWerks = dtSNInfo.Rows[0]["WERKS"].ToString();
                                }
                                if (cmbLgort.SelectedIndex != -1)
                                {
                                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                                }
                                else
                                {
                                    strLgort = dtSNInfo.Rows[0]["LGORT"].ToString();
                                }
                                //验证是否有仓别使用的权限
                                if (!CheckLgortWithAuth())
                                {
                                    return;
                                }
                                strCurrentBoxId = dtSNInfo.Rows[0]["BOXID"].ToString();
                                strPalletId = dtSNInfo.Rows[0]["MBLNR"].ToString();
                                dtAllSNByPalletID = objSapData.GetAllSNByPalId(strWerks, strLgort, Locat, strPalletId);
                                //检查该palletId下面有多少Boxid
                                DataView dvTemp = new DataView(dtAllSNByPalletID);
                                dtBoxIds = dvTemp.ToTable(true, "BOXID");

                                intPalTotalQty = dtAllSNByPalletID.Rows.Count; //pallid下面所有的SN
                                DataRow[] drCurBxIdSN = dtAllSNByPalletID.Select(" BOXID='" + strCurrentBoxId + "' ");
                                intBoxTotalQty = drCurBxIdSN.Length;
                                intScanBoxQty += 1;
                                intScanPalQty += 1;
                                strLastBXID = strCurrentBoxId;
                                txtCurBXToQty.Text = intBoxTotalQty.ToString();
                                txtPalToQty.Text = dtAllSNByPalletID.Rows.Count.ToString();
                                txtBXQty.Text = dtBoxIds.Rows.Count.ToString();
                                //lblMessage.Text = "Message: 该SN对应的PalletID 下面共有  " + dtBoxIds.Rows.Count + " 条BOXID，共有  " + dtAllSNByPalletID.Rows.Count + "  条SN数据 ";
                                GetDefaultTable();

                                #region 填值到datatable中 ,并显示数据

                                foreach (DataRow tmpRow in dtAllSNByPalletID.Rows)
                                {
                                    drRow = Data.NewRow();
                                    //  drRow["CHKED"] = tmpRow["CHKED"].ToString();
                                    drRow["CHKED"] = tmpRow["CHKED"];
                                    drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                    drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                    drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                    drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                    drRow["UMLGO"] = tmpRow["UMLGO"].ToString();
                                    drRow["LOCAT"] = tmpRow["LOCAT"].ToString();
                                    drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                    drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                    drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                    drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                    drRow["ALQTY"] = tmpRow["MENGE"].ToString();
                                    drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                    drRow["ZEILE"] = tmpRow["ZEILE"].ToString();
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
                                    //drRow["KDMAT"] = (dtAllSNByPalletID.Columns.IndexOf("KDMAT") > -1) ? tmpRow["KDMAT"].ToString() : string.Empty;
                                    drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                                    drRow["WO"] = tmpRow["WO"].ToString();
                                    //  drRow["SERNO"] = string.Empty;
                                    drRow["SERNO"] = tmpRow["SERNO"].ToString();
                                    drRow["LOADID"] = tmpRow["LOADID"].ToString();
                                    drRow["MODEL"] = tmpRow["MODEL"].ToString();
                                    drRow["REGION"] = tmpRow["REGION"].ToString();
                                    drRow["PALQTY"] = tmpRow["PALQTY"].ToString();
                                    drRow["REFID"] = tmpRow["REFID"].ToString();
                                    Data.Rows.Add(drRow);
                                }
                                ShowDataGrid();
                                #endregion

                                #region 对dataGridView填充颜色
                                SetChecked(strSN, strCurrentBoxId);
                                #endregion

                                #region 检查是否为FATP入库形式
                                if (dtSNInfo.Rows[0]["MTYPE"].ToString() == "QMS_FAT")
                                {
                                    strMethod = "FATP";
                                }
                                else
                                {
                                    strMethod = "UNFATP";
                                    //txtLocat.Enabled = false;
                                }

                                #endregion

                                if (intScanBoxQty == intBoxTotalQty)
                                {
                                    SetSuccessByBxID();
                                }
                                else
                                {
                                    Sound.Play(@"Sound\ready.wav");
                                }
                                #endregion

                                getQMSPicture();
                            }
                            #endregion
                        }
                        else
                        {
                            stsWarning.Text = "SN不存在，请确认！";
                            SetErrException();
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "SN不能为空，请确认！";
                        SetErrException();
                        return;
                    }
                    if (intPalTotalQty == intScanPalQty)
                    {
                        btnConfirm.Enabled = true;
                        Sound.Play(@"Sound\End.wav");
                    }
                    if (intPalTotalQty < intScanPalQty)
                    {
                        SetErrException();
                        stsWarning.Text = "刷入异常，请确认！";//防呆，再次防止出现重复刷入，刷出高于总数的可能
                        return;
                    }
                    SetControlInit();
                }
                catch (Exception err)
                {
                    SetErrException();
                    MessageBox.Show(err.ToString() + "==>txtSn_KeyDown");
                    return;
                }
            }
        }

        #endregion

        #region 读取QMS服务器图片
        private void getQMSPicture()
        {
            string picflage = "";
            string[] files = Directory.GetFiles(@"images\");
            #region
            //for (int i = 0; i < files.Length; i++)
            //{
            //    string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
            //    if (fileTEMP.Length > 10)
            //    {
            //        if (fileTEMP.Substring(0, 11) == dtSNInfo.Rows[0]["MATNR"].ToString())
            //        {
            //            picbox.ImageLocation = @"images\" + fileTEMP;
            //            picflage = files[i].ToString();
            //        }
            //    }
            //}
            //if (picflage == "")
            //{
            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
            //        if (fileTEMP.Length > 6)
            //        {
            //            string A = fileTEMP.Substring(0, 7);
            //            if (fileTEMP.Substring(0, 7) == dtSNInfo.Rows[0]["MATNR"].ToString().Substring(0, 7))
            //            {
            //                picbox.ImageLocation = @"images\" + fileTEMP;
            //                picflage = files[i].ToString();
            //            }
            //        }
            //    }
            //}
            //if (picflage == "")
            //{
            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
            //        if (fileTEMP.Length > 2)
            //        {
            //            if (fileTEMP.Substring(0, 3) == dtSNInfo.Rows[0]["MATNR"].ToString().Substring(2, 3))
            //            {
            //                picbox.ImageLocation = @"images\" + fileTEMP;
            //                picflage = files[i].ToString();
            //            }
            //        }
            //    }             
            //}
            #endregion
            if (picflage == "")
            {
                bool flage = false;
                string strPicturePath = dtSNInfo.Rows[0]["PicturePath"].ToString();
                string strUsrnm = dtSNInfo.Rows[0]["USRNAM"].ToString();
                string strPswd = dtSNInfo.Rows[0]["PSWD"].ToString();
                if (!string.IsNullOrEmpty(strPicturePath))
                {
                    flage = connectState(strPicturePath, strUsrnm, strPswd);
                    if (flage)
                    {
                        TransportRemoteToLocal(strPicturePath, AppDomain.CurrentDomain.BaseDirectory + @"images", dtSNInfo.Rows[0]["MATNR"].ToString());
                        files = Directory.GetFiles(@"images\");                      
                        for (int i = 0; i < files.Length; i++)
                        {
                            string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                            if (fileTEMP.Length >10)
                            {
                                if (fileTEMP.Substring(0, 11) == dtSNInfo.Rows[0]["MATNR"].ToString())
                                {
                                    picbox.ImageLocation = @"images\" + fileTEMP;
                                    picflage = files[i].ToString();
                                }
                            }
                        }
                        if (picflage == "")
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                                if (fileTEMP.Length > 6 && fileTEMP.Length < 12)
                                {
                                    if (fileTEMP.Substring(0, 7) == dtSNInfo.Rows[0]["MATNR"].ToString().Substring(0, 7))
                                    {
                                        picbox.ImageLocation = @"images\" + fileTEMP;
                                        picflage = files[i].ToString();
                                    }
                                }
                            }
                        }
                        if (picflage == "")
                        {
                            for (int i = 0; i < files.Length; i++)
                            {
                                string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                                if (fileTEMP.Length > 2)
                                {
                                    if (fileTEMP.Substring(0, 3) == dtSNInfo.Rows[0]["MATNR"].ToString().Substring(2, 3))
                                    {
                                        picbox.ImageLocation = @"images\" + fileTEMP;
                                        picflage = files[i].ToString();
                                    }
                                }
                            }
                        }                  
                    }
                }
                else
                {
                    MessageBox.Show("未找到该BU的图片路径，请先联系系统管理员！！");
                    return;
                }
            }
        }
        #endregion


        #region 获取QMS图片路径

        /// <summary>  
        /// 连接远程共享文件夹  
        /// </summary>  
        /// <param name="path">远程共享文件夹的路径</param>  
        /// <param name="userName">用户名</param>  
        /// <param name="passWord">密码</param>  
        /// <returns></returns>  
        public bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(10000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        /// <summary>  
        /// 从远程服务器下载文件到本地  
        /// </summary>  
        /// <param name="src">远程服务器路径</param>  
        /// <param name="dst">下载到本地后的文件路径</param>  
        /// <param name="fileName">远程服务器（共享文件夹）中的文件名称，包含扩展名</param>  
        public void TransportRemoteToLocal(string src, string dst, string fileName)  //src：远程服务器路径     dst：下载到本地后的文件路径    fileName:远程服务器src路径下的文件名  
        {
            try
            {
                if (!Directory.Exists(dst))
                {
                    Directory.CreateDirectory(dst);
                }
                string strflage = "";
                string[] files = Directory.GetFiles(src);
                for (int i = 0; i < files.Length; i++)
                {
                    string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                    if (fileTEMP.Length > 10)
                    {
                        if (fileTEMP.Substring(0, 11) == fileName)
                        {
                            // string strPath = dst + @"\" + fileTEMP;
                            string strPath = dst + @"\" + fileTEMP.Substring(0, fileTEMP.IndexOf('.')) + fileTEMP.Substring(fileTEMP.IndexOf('.'), fileTEMP.Length - fileTEMP.IndexOf('.')).ToLower();
                            strflage = files[i].ToString();
                            File.Copy(strflage, strPath, true);
                        }
                    }
                }
                if (strflage == "")
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                        if (fileTEMP.Length > 6 && fileTEMP.Length < 12)
                        {
                            if (fileTEMP.Substring(0, 7) == fileName.Substring(0, 7))
                            {
                                // string strPath = dst + @"\" + fileTEMP;
                                string strPath = dst + @"\" + fileTEMP.Substring(0, fileTEMP.IndexOf('.')) + fileTEMP.Substring(fileTEMP.IndexOf('.'), fileTEMP.Length - fileTEMP.IndexOf('.')).ToLower();
                                strflage = files[i].ToString();
                                File.Copy(strflage, strPath, true);
                            }
                        }
                    }
                }
                if (strflage == "")
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string fileTEMP = files[i].ToString().Substring(files[i].ToString().LastIndexOf('\\') + 1);
                        if (fileTEMP.Length > 2 )
                        {
                            if (fileTEMP.Substring(0, 3) == fileName.Substring(2, 3))
                            {
                                // string strPath = dst + @"\" + fileTEMP;
                                string strPath = dst + @"\" + fileTEMP.Substring(0, fileTEMP.IndexOf('.')) + fileTEMP.Substring(fileTEMP.IndexOf('.'), fileTEMP.Length - fileTEMP.IndexOf('.')).ToLower();
                                strflage = files[i].ToString();
                                File.Copy(strflage, strPath, true);
                        }
                    }
                }

            }
                if (strflage == "")
                {
                    MessageBox.Show("找不到对应的图片！");
                    return;
                }
                //  File.Copy(strflage, dst);
            }
            catch (Exception e)
            {
                MessageBox.Show("获取图片异常！" + e.ToString());
                return;
            }



        }

        #endregion

        #region  Setting

        public void SetChecked(string strSN, string strBoxid)
        {

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dgvData.Rows[i].Cells["SERNO"].Value.ToString().ToUpper() == strSN && dgvData.Rows[i].Cells["BOXID"].Value.ToString().ToUpper() == strLastBXID)
                {
                    dtData.Rows[i]["CHKED"] = "Y";
                    dgvData.Rows[i].Cells[0].Value = true;
                    dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        public void SetControlInit()
        {
            this.txtSn.Text = string.Empty;
            txtscbxQty.Text = intScanBoxQty.ToString();
            txtscplQty.Text = intScanPalQty.ToString();
        }

        private void SetSuccessByBxID()
        {
            //intScanBoxQty = 0;
            intBoxTotalQty = 0;
            strLastBXID = "";
            Sound.Play(@"Sound\Success.wav");
        }

        private void SetErrException()
        {
            this.btnSave.Enabled = false;
            this.txtSn.Focus();
            this.txtSn.Text = string.Empty;
            Sound.Play(@"Sound\ERROR.wav");
        }

        public void SetInit()
        {
            stsWarning.Text = string.Empty;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbInType.Enabled = true;
            this.txtLocat.Text = string.Empty;
            this.txtSn.Text = string.Empty;
            this.txtscbxQty.Text = string.Empty;
            this.txtscplQty.Text = string.Empty;
            this.txtCurBXToQty.Text = string.Empty;
            this.txtPalToQty.Text = string.Empty;
            this.txtBXQty.Text = string.Empty;

            Werks = "";
            Lgort = "";
            Locat = "";
            strLastBXID = "";
            strCurrentBoxId = "";
            strPalletId = "";
            strMethod = "";
            strType = "";
            Type = "";
            intScanBoxQty = 0;
            intScanPalQty = 0;
            intBoxTotalQty = 0;
            intPalTotalQty = 0;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.dgvCombineMatnr.DataSource = null;
            this.lblCount.Text = "0 records";
            this.btnSave.Enabled = false;
            this.btnConfirm.Enabled = false;
            this.dgvData.Columns.Clear();
            dtTmpData.Clear();
            this.cmbWerks.SelectedIndex = -1;
            this.cmbWerks.Text = "";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.cmbLgort.Text = "";
            picbox.ImageLocation = "";

            dtAllSNByPalletID.Rows.Clear();
            dtBoxIds.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInit();
        }

        #endregion

        #region Confirm

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (Locat == "")
            {
                stsWarning.Text = "请选择储位";
                SetErrException();
                return;
            }
            DataRow[] drCheck = dtData.Select(" CHKED='N' ");
            if (drCheck.Length > 0)
            {
                MessageBox.Show("还有未刷入的SN，请确认！");
                SetErrException();
                return;
            }
            else
            {
                CombineDataToSap();//合并要同步给SAP的数据，并show 出来
                ShowMatnrDataGrid();
                this.btnSave.Enabled = true;
            }
        }

        #endregion

        #region 回传QMS S/F扣账
        private void btnPassBack_Click(object sender, EventArgs e)
        {
            string strMblnr = txtSn.Text.ToString().Trim();
            if (objStorageIn.TransferPalletIDToQMS_PCBA(strMblnr, "PASS", "WH IN"))
            {
                objLogData.AddQWMSLOG(strMblnr, "QWMS", "IN", "重新回传QMS成功", "OK", null);
                stsWarning.Text = "回传QMS成功！";
            }
            else
            {
                stsWarning.Text = "回传QMS失败！";
                objLogData.AddQWMSLOG(strMblnr, "QWMS", "IN", "重新回传QMS失败", "Fail:" + objStorageIn.ERRMSG, null);
                SetErrException();
            }
        }
        #endregion

        //public DataSet SendToSAP(DataTable vardtSend)
        //{
        //    try
        //    {
        //        DataSet dsResult = new DataSet();
        //        PP_Service obj = new PP_Service();
        //        DataSet ds = new DataSet();
        //        ds.Tables.Add(vardtSend);
        //        dsResult = obj.ZRFC_PP_311_AUTO_RETURN_N(ds);
        //        return dsResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //} 

        #region CombineData
        private void CombineDataToSap()
        {
            #region 合并SAP数据
            GetSAPData();
            int j = 0;
            DataView dv = new DataView(dtData);
            DataTable dtMatnr = dv.ToTable(true, new string[] { "MATNR", "CHARG" });
            dtCombineData = dtData.Clone();
            foreach (DataRow dr in dtMatnr.Rows)
            {
                string matnr = dr["MATNR"].ToString();
                string charg = dr["CHARG"].ToString();
                DataRow[] drSelect = dtData.Select("MATNR='" + matnr + "' AND CHARG='" + charg + "' ");
                DataRow drSAP = dtSAP.NewRow();
                DataRow drCombine = dtCombineData.NewRow();
                drSAP["REFNO"] = "QWMS" + dtData.Rows[0]["MBLNR"].ToString();
                drSAP["PLANT"] = drSelect[0]["WERKS"].ToString();
                drSAP["MATNR"] = drSelect[0]["MATNR"].ToString();
                drSAP["KOSTL"] = drSelect[0]["KOSTL"].ToString();
                drSAP["LGORTF"] = drSelect[0]["LGORT"].ToString();
                drSAP["LGORTT"] = drSelect[0]["UMLGO"].ToString();
                drSAP["RETQTY"] = drSelect.Length;
                drSAP["CHARG"] = drSelect[0]["CHARG"].ToString();
                drSAP["STATUS"] = "Q";
                drSAP["POST_OUT"] = string.Empty;
                drSAP["ERROR_STRING"] = string.Empty;
                dtSAP.Rows.Add(drSAP);


                drCombine["MANDT"] = drSelect[0]["MANDT"].ToString();
                drCombine["COMCD"] = drSelect[0]["COMCD"].ToString();
                drCombine["WERKS"] = drSelect[0]["WERKS"].ToString();
                drCombine["LGORT"] = drSelect[0]["LGORT"].ToString();
                drCombine["LOCAT"] = Locat;
                drCombine["MBLNR"] = drSelect[0]["MBLNR"].ToString();
                drCombine["MATNR"] = drSelect[0]["MATNR"].ToString();
                drCombine["CHARG"] = drSelect[0]["CHARG"].ToString();
                drCombine["INSMK"] = drSelect[0]["INSMK"].ToString();
                drCombine["MENGE"] = drSelect.Length;
                drCombine["ALQTY"] = drSelect.Length;
                drCombine["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                dtCombineData.Rows.Add(drCombine.ItemArray);


            }
            //  dtCombineData= CombineDataTableByMatnr();


            #endregion
        }

        public void GetOtherDataToSAP()
        {
            dtSAP = new DataTable();
            dtSAP.Columns.Add("P_MANDT", Type.GetType());
            dtSAP.Columns.Add("P_MJAHR", Type.GetType());
            dtSAP.Columns.Add("P_WERKS", Type.GetType());
            dtSAP.Columns.Add("P_SCPNO", Type.GetType());
            dtSAP.Columns.Add("FLAG", Type.GetType());
        }

        public void GetSCDataToSAP()
        {
            dtSAP = new DataTable();
            dtSAP.Columns.Add("DYEAR", Type.GetType());
            dtSAP.Columns.Add("MBLNR", Type.GetType());
            dtSAP.Columns.Add("WERKS", Type.GetType());
            dtSAP.Columns.Add("ZEILE", Type.GetType());
            dtSAP.Columns.Add("TCODE", Type.GetType());
            dtSAP.Columns.Add("MVT", Type.GetType());
            dtSAP.Columns.Add("MATNR", Type.GetType());
            dtSAP.Columns.Add("AUFNR", Type.GetType());
            dtSAP.Columns.Add("CHARG", Type.GetType());
            dtSAP.Columns.Add("RCHARG", Type.GetType());
            dtSAP.Columns.Add("MENGE", Type.GetType());
            dtSAP.Columns.Add("UMLGO", Type.GetType());
            dtSAP.Columns.Add("LGORT", Type.GetType());
            dtSAP.Columns.Add("UNAME", Type.GetType());
            dtSAP.Columns.Add("PRQID", Type.GetType());
            dtSAP.Columns.Add("ZSUCC", Type.GetType());
            dtSAP.Columns.Add("ZEMSG", Type.GetType());
        }

        public void GetSAPData()
        {
            dtSAP = new DataTable();
            dtSAP.Columns.Add("REFNO", Type.GetType());
            dtSAP.Columns.Add("PLANT", Type.GetType());
            dtSAP.Columns.Add("MATNR", Type.GetType());
            dtSAP.Columns.Add("KOSTL", Type.GetType());
            dtSAP.Columns.Add("LGORTF", Type.GetType());
            dtSAP.Columns.Add("LGORTT", Type.GetType());
            dtSAP.Columns.Add("RETQTY", Type.GetType());
            dtSAP.Columns.Add("CHARG", Type.GetType());
            dtSAP.Columns.Add("STATUS", Type.GetType());
            dtSAP.Columns.Add("POST_OUT", Type.GetType());
            dtSAP.Columns.Add("ERROR_STRING", Type.GetType());
        }

        #endregion

        #region 同步SAP
        public DataSet SendToSAP(DataTable dtSend, string strRFCName)
        {
            try
            {
                DataSet dsResult = new DataSet();
                PP_Service obj = new PP_Service();
                MM.MM_Service objMM = new MM.MM_Service();
                DataSet ds = new DataSet();
                ds.Tables.Add(dtSend);
                if (strRFCName == "ZRFC_PP_311_AUTO_RETURN_N")
                {
                    dsResult = obj.ZRFC_PP_311_AUTO_RETURN_N(ds);
                }
                if (strRFCName == "Z_RFC_SCRAP_ZSC1_POST")
                {
                    //  varMANDT,varMJAHR,varWERKS,varSCPNO
                    // dsResult = objMM.Z_RFC_SCRAP_ZSC1_POST(dtSend.Rows[0]["P_MANDT"].ToString(), dtSend.Rows[0]["P_MJAHR"].ToString(), dtSend.Rows[0]["P_WERKS"].ToString(), dtSend.Rows[0]["P_SCPNO"].ToString());
                    dsResult = objMM.Z_RFC_SCRAP_ZSC1_POST(dtSend.Rows[0]["P_MANDT"].ToString(), objStorageIn.QuaryMCYear(dtSend.Rows[0]["P_SCPNO"].ToString()), dtSend.Rows[0]["P_WERKS"].ToString(), dtSend.Rows[0]["P_SCPNO"].ToString());

                }
                if (strRFCName == "ZRFC_PP_QWMS_DEBIT")
                {
                    dsResult = obj.ZRFC_PP_QWMS_DEBIT(ds);
                }

                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 开始入QWMS库存
        private void SaveInQWMStock()
        {
            try
            {
                if (dtData.Rows.Count > 0)
                {
                    string strPalletID = dtData.Rows[0]["MBLNR"].ToString();
                    string strOAPalletID = dtData.Rows[0]["INSMK"].ToString() + dtData.Rows[0]["MBLNR"].ToString();

                    stsWarning.Text = "开始QWMS入库,请勿关闭视窗";
                    //记录LOG
                    objLogData.AddQWMSLOG(dtData.Rows[0]["MBLNR"].ToString(), "QWMS", "IN", "开始QWMS入账", null, null);
                    //入QWMS库存
                    if (objStorageIn.AddGBOnLineInData_PCBA(strWerks, strLgort, Locat, dtCombineData, arrBoxID, dtData, strMethod))
                    {
                        //记录LOG
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入库", "OK", null);
                        //回传QMS
                        if (objStorageIn.TransferPalletIDToQMS_PCBA(strPalletID, "PASS", "WH IN"))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "回传QMS成功", "OK", null);
                            stsWarning.Text = "QWMS Add OK!! 回传QMS成功";
                            #region 同步OA签核
                            if (dtData.Rows[0]["REFID"].ToString() == "IWorkFlow" || strPalletID.Substring(0, 1) == "P")
                            {
                                objLogData.AddQWMSLOG(strPalletID, "OA", "Approve", "开始去OA同步签核", null, null);
                                QWMS.WF.WF0152ForQWMS objWF = new QWMS.WF.WF0152ForQWMS();
                                if (objWF.Approve(strOAPalletID))
                                {
                                    objLogData.AddQWMSLOG(strPalletID, "OA", "Approve", "回传OA签核成功", "OK", null);
                                    stsWarning.Text = "同步IWorkFlow签核成功";
                                    Sound.Play(@"Sound\OK.wav");
                                }
                                else
                                {
                                    objLogData.AddQWMSLOG(strPalletID, "OA", "Approve", "回传OA签核失败", "Fail", null);
                                    Sound.Play(@"Sound\ERROR.wav");
                                    stsWarning.Text = "同步IWorkFlow签核失败";
                                    return;
                                }
                            }
                            //else 
                            //{
                            //    objLogData.AddQWMSLOG(strPalletID, "OA", "Approve", "去OA签核失败", dtData.Rows[0]["REFID"].ToString(), null);
                            //    Sound.Play(@"Sound\ERROR.wav");
                            //    stsWarning.Text = "同步IWorkFlow签核失败";
                            //    return;
                            //}

                            #endregion
                        }
                        else
                        {
                            stsWarning.Text = "QWMS Add OK!! 回传QMS失败";
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "回传QMS失败", "Fail:" + objStorageIn.ERRMSG, null);
                            SetErrException();
                        }
                    }
                    else
                    {
                        stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + objStorageIn.ERRMSG, null);
                        Sound.Play(@"Sound\ERROR.wav");
                    }
                    dtTmpData.Clear();
                    SetInit();
                }

            }
            catch (Exception ex)
            {
                string strPalletID = dtData.Rows[0]["MBLNR"].ToString();
                MessageBox.Show(ex.Message + "入库QWMS出错");
                objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + ex.Message, null);
                Sound.Play(@"Sound\ERROR.wav");
                SetInit();
                return;
            }

        }
        #endregion

        private DataTable CombineDataTableByMatnr()
        {
            dtCombineData = dtData.Clone();
            dtCombineData.Rows.Clear();
            try
            {
                var query = from row in dtData.AsEnumerable()
                            group row by row.Field<string>("MATNR") into m
                            select new
                            {
                                MATNR = m.Key,
                                CHARG = m.FirstOrDefault().Field<string>("CHARG"),
                                INSMK = m.FirstOrDefault().Field<string>("INSMK"),
                                LIFNR = m.FirstOrDefault().Field<string>("LIFNR"),
                                MENGE = m.Sum(n => n.Field<int>("MENGE"))
                            };
                foreach (var item in query)
                {
                    DataRow drResult = dtCombineData.NewRow();

                    drResult["MATNR"] = item.MATNR;
                    drResult["LIFNR"] = item.LIFNR;
                    drResult["MENGE"] = item.MENGE;
                    drResult["INSMK"] = item.INSMK;
                    drResult["LIFNR"] = item.LIFNR;
                    drResult["INDAT"] = DateTime.Now.Date.ToString();
                    dtCombineData.Rows.Add(drResult.ItemArray);
                }
            }
            catch (Exception e)
            {
                stsWarning.Text = dtData.TableName + "Table转化异常";
            }

            return dtCombineData;
        }

        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        #region 调用OA的API获取判票信息    add by Galen  2019/12/4

        private bool GetDataFromOA(string strwerks, string strlgort, string RefID, QCI.QWMS.StorageIn objStorageIn)
        {

            DataTable dt = new DataTable();
            DataTable dtNULL = new DataTable();

            var varPostData = new
            {
                WERKS = strwerks,
                LGORT = strlgort,
                MBLNR = RefID
            };
            var varUserInfo = new
            {
                UserId = "EC",
                PassWord = MD5EncryptToHexadecimal("9JvTt28y7CJF" + DateTime.Now.ToString("yyyyMMdd")) //规则：（9JvTt28y7CJF+当前日期 yyyyMMdd）的十六进制MD5值

            };
            try
            {

                // 获取Token信息
                string ResultGetToken = HttpPostByHttpWebRequest("https://wf.quantacn.com/WFAPI/API/Login/Login", varUserInfo, "");

                //解析返回Json
                JObject jo = (JObject)JsonConvert.DeserializeObject(ResultGetToken);
                string strToken = "";
                if (jo["result"].ToString().ToUpper() == "TRUE")
                {
                    strToken = jo["token"].ToString();

                }
                //访问iworkflow接口
                //正式库
                string Result = HttpPostByHttpWebRequest("https://wf.quantacn.com/WFAPI/API/WF0152ToQWMS/SetDataToQWMS", varPostData, strToken);
                //测试地址
                //string Result = HttpPostByHttpWebRequest("http://wftest.quantacn.com/WFAPI/API/WF0152ToQWMS/SetDataToQWMS", varPostData, strToken);

                #region 判断返回结果
                JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
                objJavaScriptSerializer.MaxJsonLength = Int32.MaxValue;
                Dictionary<string, object> DicText = objJavaScriptSerializer.Deserialize<Dictionary<string, object>>(Result);
                if (DicText.ContainsKey("Result"))
                {
                    //MessageBox.Show(DicText["Message"].ToString());
                    return true;
                }
                dt = JsonToDataTable(Result);//Json转换成DataTable
                if (dt == null || dt.Rows.Count == 0)
                {
                    //MessageBox.Show("iWorkflow无数据，无法同步");
                    return true;
                }
                if (dt.Rows[0]["Status"].ToString() != "A")
                {
                    //MessageBox.Show("单据类型错误，无法同步");
                    return false;
                }
                if (dt.Rows[0]["ApplyStatus"].ToString() != "0" && dt.Rows[0]["ApplyStatus"].ToString() != "4")
                {
                    MessageBox.Show("此单据已签核完成，回传IworkFlow可能会失败，请知悉！");
                    return false;
                }
                if (!dt.Rows[0]["FlowDescription"].ToString().Contains("仓管员"))
                {
                    MessageBox.Show("当前签核关卡为：" + dt.Rows[0]["FlowDescription"].ToString() + "，回传IworkFlow可能会失败，请知悉！");
                    return false;
                }

                //DateTime DtNow = DateTime.Now;
                //DateTime DtOA = DateTime.ParseExact(dt.Rows[0]["Applydate"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                //TimeSpan TS = DtNow.Subtract(DtOA);
                //if (TS.TotalDays > 31)
                //{
                //    MessageBox.Show("此单据距开立时间已超一个月，无法同步");
                //    return dtNULL;
                //}

                #endregion
                objStorageIn.addOA_TICtoWHDWN(dt);
                return true;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return false;
            }

        }

        #region 生成获取token的密码
        public static string MD5EncryptToHexadecimal(string inputString)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                    return ConvertBytesToHexadecimalString(md5Bytes);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string ConvertBytesToHexadecimalString(byte[] inputBytes)
        {
            if (inputBytes == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputBytes.Length; i++)
            {
                sb.Append(inputBytes[i].ToString("X2")); // "X2" 表示大写十六进制
            }
            return sb.ToString();
        }

        #endregion

        #region 构建POST请求并返回结果主体
        /// <summary>
        /// 构建POST请求并返回结果主体
        /// </summary>
        /// <param name="Url">网址</param>
        /// <param name="PostData">object参数</param>
        /// <returns></returns>
        public static string HttpPostByHttpWebRequest(string Url, object PostData, string strToken)
        {
            try
            {
                //POST参数
                JavaScriptSerializer json = new JavaScriptSerializer();
                string strPostData = json.Serialize(PostData);
                byte[] bytPostData = Encoding.UTF8.GetBytes(strPostData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 1000000;
                request.ContentType = "application/json";   //"application/x-www-form-urlencoded";
                request.ContentLength = bytPostData.Length;
                if (!string.IsNullOrEmpty(strToken))
                {
                    string token = "Bearer " + strToken;//获取完Token使用Bearer+空格+Token
                    request.Headers.Add("Authorization", token); //如果接口需要鉴权, 把"token"换成自己需要的
                }
                System.IO.Stream objPostStream = request.GetRequestStream();
                objPostStream.Write(bytPostData, 0, bytPostData.Length);
                objPostStream.Close();
                //获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader objResponseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string strResult = objResponseStreamReader.ReadToEnd();
                objResponseStreamReader.Close();

                return strResult;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region  将Json格式的字符串转换成DataTable
        /// <summary>
        /// 将JSON格式数据转换成table
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName.Replace('"', ' ').Trim();
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Replace('"', ' ').Trim().Split(':');
                        dc.ColumnName = strCell[0].Trim();
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        #endregion


        #endregion




    }
}
