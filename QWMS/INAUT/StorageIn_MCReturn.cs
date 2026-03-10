using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.IO;
using System.Collections;
using QWMS.PP;
using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
//using QWMS.WF;

namespace QWMS
{
    public partial class StorageIn_MCReturn : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private DataTable dtData = new DataTable();
        private StreamWriter sw = null;
        private string strType = "";
        private DataTable dtGetSAPInfo = new DataTable();

        private DataGridViewComboBoxColumn dgvcLocat = new DataGridViewComboBoxColumn();

        private string strReferenceID = "";
        string strFirstMblnr = "";

        private string strInsmk = "";
        private string strMblnr = "";
        private string strLifnr = "";

        QCI.QWMS.PlantData objPlantData;
        QCI.QWMS.LogData objLogData;
        QCI.QWMS.StorageData objStorageData;


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

        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
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

        QCI.QWMS.Authority objAuthority = null;
        QCI.QWMS.StorageIn StorageIn = null;

        #endregion

        #region 构造函数
        public StorageIn_MCReturn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                StorageIn = new StorageIn(UserData, strProgid);
                objAuthority = new QCI.QWMS.Authority(UserData);
                objPlantData = new QCI.QWMS.PlantData(UserData);
                objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                //檢查權限
                if (!StorageIn.CheckAuthority(""))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化

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

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.txtBoxid.Text = "";
            this.txtLocat.Text = "";
            this.dgvData.DataSource = null;
            this.checkBox1.Checked = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Enabled = true;
            this.txtLocat.Text = "";
            this.btnConfirm.Enabled = false;
            strReferenceID = string.Empty;
            strLocat = "";
            txtReferanceID.Text = "";
            strFirstMblnr = "";
            arrbox = new ArrayList();
            //20190706   Galen Chen
            this.dgvData.Columns.Clear();
            stsWarning.Text = "";
            txtSANo.Text = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 厂区仓别储位赋值
        private void ShowDdlWerks()
        {
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

        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                //当前Plant
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                    dtTemp = objAuthority.CheckLgortAuthority();
                //现有值
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    cmbLgort.Items.Clear();

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
                        //去重复值
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                            if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                            {
                                cmbLgort.SelectedIndex = i;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void ShowDdlLocat(string strEvent)
        {
            stsWarning.Text = "";
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
                    if (strEvent == "DoubleClick")
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                        #region 增加判断是不是ASRS空储位
                        QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);
                        if (Werks == "CS41" && objInterface.CheckLGORT(Werks, Lgort))
                        {
                            if (objInterface.CheckLOCAT(txtLocat.Text))//判断是否为ASRS空储位
                            {
                                stsWarning.Text = "此储位为ASRS的非空储位，请重新选择!";
                                txtLocat.Text = string.Empty;
                                return;
                            }
                        }
                        #endregion
                    }
                    else if (strEvent == "CmbLoactDataSource")
                    {
                        string strLocStatus = strType == "NEW" ? "0" : "1";
                        DataTable dtTemp = objPlantData.GetAllLocatData(Werks, Lgort, "", strLocStatus);
                        if (dtTemp.Rows.Count == 0)
                        {
                            dgvcLocat.Items.Clear();
                            strLocat = "";
                        }
                        else
                        {
                            dgvcLocat.Items.Clear();

                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                //去重复值
                                if (!dgvcLocat.Items.Contains(dtTemp.Rows[i]["LOCAT"].ToString()))
                                {
                                    dgvcLocat.Items.Add(dtTemp.Rows[i]["LOCAT"].ToString());
                                }
                            }
                        }
                        //if (txtLocat.Text.Trim() != "")
                        //{
                        //    dgvcLocat.DisplayMember= txtLocat.Text.Trim();
                        //    dgvcLocat.ValueMember = txtLocat.Text.Trim();
                        //}
                    }
                    else
                    {
                        strLocat = "";
                        dgvcLocat.Items.Clear();
                    }





                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            ShowDdlLocat("DoubleClick");
        }

        #endregion

        #region 選擇入庫方式-新板入庫(New Pallet)
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;

        }
        #endregion

        #region 選擇入庫方式-加料入庫(Add In)
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;

        }
        #endregion

        #region 公共方法

        //画表格
        public void ShowDataGrid()
        {

            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            this.dgvData.AllowUserToAddRows = false;
            try
            {
                if (checkBox1.Checked == false)
                {
                    DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                    dgvcSelect.DataPropertyName = "Cselect";
                    dgvcSelect.HeaderText = "选择";
                    dgvcSelect.Width = 50;
                    this.dgvData.Columns.Add(dgvcSelect);
                }

                DataGridViewTextBoxColumn dgvcRefDoc = new DataGridViewTextBoxColumn();
                dgvcRefDoc.DataPropertyName = "MBLNR";
                dgvcRefDoc.HeaderText = "ReferenceID";
                dgvcRefDoc.Width = 150;
                dgvcRefDoc.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRefDoc);

                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "BOXID";
                dgvcBoxid.Width = 150;
                dgvcBoxid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBoxid);

                //DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                //dgvcWERKS.DataPropertyName = "LOCAT";
                //dgvcWERKS.HeaderText = "Location";
                //dgvcWERKS.Width = 60;
                //dgvcWERKS.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcWERKS);

                ShowDdlLocat("CmbLoactDataSource");
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Name = "LOCAT";
                dgvcLocat.Width = 100;
                dgvcLocat.ReadOnly = false;
                dgvcLocat.DisplayStyleForCurrentCellOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcCostCenter = new DataGridViewTextBoxColumn();
                dgvcCostCenter.DataPropertyName = "MATNR";
                dgvcCostCenter.HeaderText = "MaterialNo";
                dgvcCostCenter.Width = 100;
                dgvcCostCenter.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCostCenter);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "CHARG";
                dgvcMATNR.HeaderText = "Version";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "MENGE";
                dgvcCHARG.HeaderText = "Storage In Qty";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcQTY = new DataGridViewTextBoxColumn();
                dgvcQTY.DataPropertyName = "SCANQTY";
                dgvcQTY.HeaderText = "Scan Qty";
                dgvcQTY.Width = 50;
                dgvcQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQTY);

                if (Comcd== "9200" && Werks == "CS31" && (Lgort.Substring(0, 2) == "TW" || Lgort.Substring(0, 2) == "TC"))
                {
                    DataGridViewTextBoxColumn dgvcMCDAT = new DataGridViewTextBoxColumn();
                    dgvcMCDAT.DataPropertyName = "MCDAT";
                    dgvcMCDAT.HeaderText = "MC Date";
                    dgvcMCDAT.Width = 90;
                    dgvcMCDAT.ReadOnly = true;
                    this.dgvData.Columns.Add(dgvcMCDAT);
                }


                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "Date Code";
                dgvcDACOD.Width = 90;
                dgvcDACOD.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDACOD);
                //数据源
                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
                this.dgvData_CurrentCellChanged(null, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        public DataSet SendToSAP(DataTable vardtSend)
        {
            try
            {
                DataSet dsResult = new DataSet();

                PP_Service obj = new PP_Service();
                DataSet ds = new DataSet();
                ds.Tables.Add(vardtSend);
                dsResult = obj.ZRFC_PP_QWMS_DEBIT(ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet SendToSAPSC(string varMANDT, string varMJAHR, string varWERKS, string varSCPNO)
        {
            try
            {
                DataSet dsResult = new DataSet();

                MM.MM_Service obj = new QWMS.MM.MM_Service();

                dsResult = obj.Z_RFC_SCRAP_ZSC1_POST(varMANDT, varMJAHR, varWERKS, varSCPNO);


                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet SendToSAPSC_F(string varMANDT, string varMJAHR, string varWERKS, string varZBLBNO)
        {
            try
            {
                DataSet dsResult = new DataSet();

                MM.MM_Service obj = new QWMS.MM.MM_Service();

                dsResult = obj.Z_RFC_PCBA_LIKE_ZCL2(varMANDT, varMJAHR, varWERKS, varZBLBNO);


                return dsResult;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region btnConfirm_Click

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //效验厂区、厂别、储位是否存在
            #region
            if (string.IsNullOrEmpty(txtLocat.Text.ToString()))
            {
                stsWarning.Text = "Location can't be empty!!";
                return;
            }
            else
            {
                string strLocStatus = strType == "NEW" ? "0" : "1";
                DataTable dtCheckLocat = objPlantData.GetAllLocatData(cmbWerks.Text, cmbLgort.Text, txtLocat.Text.ToString(), strLocStatus);
                if (dtCheckLocat.Rows.Count == 0)
                {
                    stsWarning.Text = "该储位不存在或者储位状态不对，请确认并重新输入";
                    return;
                }
            }
            #endregion


            arrbox = new ArrayList();
            if (dgvData.Rows.Count > 0)
            {
                if (checkBox1.Checked == false)
                {
                    int qty = 0;
                    int sqty = 0;
                    for (int i = 0; i < dgvData.Rows.Count; i++)
                    {
                        // this.dgvData.Rows[j].Cells[6].Value.ToString() == this.dgvData.Rows[j].Cells[7].Value.ToString()
                        qty = qty + Convert.ToInt32(this.dgvData.Rows[i].Cells[6].Value);
                        sqty = sqty + Convert.ToInt32(this.dgvData.Rows[i].Cells[7].Value);
                    }

                    if (qty != sqty)
                    {
                        MessageBox.Show("判票数量与扫描数量不一致，请确认！");
                        return;
                    }
                }
            }

            #region
            btnConfirm.Enabled = false;
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            stsWarning.Text = "";
            DataTable dtSend = new DataTable();
            bool bolSAP = false;
            string strMessageFromSAP = string.Empty;
            string strTwo = Lgort.Substring(0, 2);
            string strCheckLocat = "";
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dgvData.Rows[i].Cells["LOCAT"].Value == null)
                {
                    strCheckLocat = txtLocat.Text.Trim();
                }
                else
                {
                    strCheckLocat = dgvData.Rows[i].Cells["LOCAT"].Value.ToString();
                }
                if (CheckLocat(strCheckLocat))
                {
                    dtData.Rows[i]["LOCAT"] = strCheckLocat;
                }
                else
                {
                    return;
                }
            }

            DataView dvMblnr = new DataView(dtData);
            DataTable dtMBLNR = dvMblnr.ToTable(true, new string[] { "MBLNR" });

            if (dtMBLNR.Rows.Count > 0)
            {
                dtData.AcceptChanges();
                DataTable dtDataAll = dtData.Copy();
                foreach (DataRow drMblnr in dtMBLNR.Rows)
                {
                    dtData = dtDataAll.Select(" MBLNR='" + drMblnr["MBLNR"].ToString() + "' ").CopyToDataTable();
                    if (dtData.Rows.Count > 0)
                    {
                        if (this.dgvData.Columns.Contains("Cselect"))
                        {
                            DataRow[] drSelect = dtData.Select(" Cselect=true ");
                            if (drSelect.Length != dtData.Rows.Count)
                            {
                                stsWarning.Text = "还有未刷入到Boxid,请先刷入";
                                return;
                            }
                        }

                        #region PCB材料不同版本不允许入库

                        string strCharg = "";
                        string strMatnr = "";
                        //string strVendor = drMblnr["LIFNR"].ToString();                     
                        if (objPlantData.CheckCHARGLGORT(Werks))
                        {
                            DataTable dtMBLNRByPCB = dvMblnr.ToTable(true, new string[] { "MBLNR", "MATNR", "CHARG", "LIFNR" });
                            foreach (DataRow drMblnrByPCB in dtMBLNRByPCB.Rows)
                            {
                                strCharg = drMblnrByPCB["CHARG"].ToString();
                                strMatnr = drMblnrByPCB["MATNR"].ToString();
                                //PCB料号
                                if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                                {
                                    if (objStorageData.CheckExistedDifferentCHARG(strLocat, strMatnr, strCharg))
                                    {
                                        MessageBox.Show("该储位:" + txtLocat.Text + "有不同版本的库存,【不允许入库到该储位】,请更换储位！");
                                        return;
                                    }
                                }
                            }
                                ////PCB料号
                                //if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                                //    {
                                //if (objStorageData.CheckExistedDifferentCHARG(strLocat, strMatnr, strCharg))
                                //{
                                //    MessageBox.Show("该储位:" + txtLocat.Text + "有不同版本的库存,【不允许入库到该储位】,请更换储位！");
                                //    return;
                                //}
                                //    }
                        }
                        #endregion



                        #region 入库SAP
                        try
                        {
                            if (strTwo == "SC")
                            {
                                string strMblnr = drMblnr["MBLNR"].ToString();
                                string strOne = strMblnr.Substring(0, 1);
                                if (strOne == "1")
                                {

                                    #region 报废到SAP
                                    DataSet dsDataFromSAPSC = SendToSAPSC("218", objStorageIn.QuaryMCYear(drMblnr["MBLNR"].ToString()), Werks, drMblnr["MBLNR"].ToString());
                                    DataTable dtDataFromSAPSC = dsDataFromSAPSC.Tables[0];
                                    string strFlag = dtDataFromSAPSC.Rows[0]["FLAG"].ToString().Trim();
                                    string strRemark = dtDataFromSAPSC.Rows[0]["REMARK"].ToString().Trim();
                                    strMessageFromSAP = strRemark;
                                    if (strFlag == "Y")
                                    {
                                        if (objStorageIn.GetUpdateWhdwnRemak(strWerks, strReferenceID, strRemark))
                                        {
                                            bolSAP = true;
                                        }
                                    }
                                    else if (strFlag == "N")
                                    {
                                        MessageBox.Show("扣账失败，失败原因为" + strRemark + ",请查找SAP发出扣账失败邮件查明原因");
                                        return;
                                    }

                                    #endregion

                                }
                                else

                                #region F开头报废单号
                                    if (strOne == "F")
                                {
                                    #region 报废到SAP
                                    DataSet dsDataFromSAPSC = SendToSAPSC_F("218", objStorageIn.QuaryMCYear(drMblnr["MBLNR"].ToString()), Werks, drMblnr["MBLNR"].ToString());
                                    DataTable dtDataFromSAPSC = dsDataFromSAPSC.Tables[0];
                                    string strFlag = dtDataFromSAPSC.Rows[0]["FLAG"].ToString().Trim();
                                    string strRemark = dtDataFromSAPSC.Rows[0]["REMARK"].ToString().Trim();
                                    strMessageFromSAP = strRemark;
                                    if (strFlag == "Y")
                                    {
                                        if (objStorageIn.GetUpdateWhdwnRemak(strWerks, strReferenceID, strRemark))
                                        {
                                            bolSAP = true;
                                        }
                                    }
                                    else if (strFlag == "N")
                                    {
                                        MessageBox.Show("扣账失败，失败原因为" + strRemark + ",请查找SAP发出扣账失败邮件查明原因");
                                        return;
                                    }
                                    #endregion
                                }
                                #endregion

                            }
                            else
                            {
                                #region 其他仓入库到SAP
                                dtSend.Columns.Add("DYEAR");
                                dtSend.Columns.Add("MBLNR");
                                DataRow dr = dtSend.NewRow();
                                dr["DYEAR"] = objStorageIn.QuaryMCYear(drMblnr["MBLNR"].ToString());
                                dr["MBLNR"] = drMblnr["MBLNR"].ToString();
                                dtSend.Rows.Add(dr);

                                DataSet dsDataFromSAP = SendToSAP(dtSend);
                                DataTable dtDataFromSAP = dsDataFromSAP.Tables[0];
                                string strZEMSG = string.Empty;
                                string strZSUCC = string.Empty;
                                string strFailMatnr = string.Empty;
                                ArrayList aryList = new ArrayList();
                                if (dtDataFromSAP.Rows.Count > 0)
                                {
                                    //if (dtDataFromSAP.Rows.Count > 1)
                                    //{
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
                                    //  strMessageFromSAP = strZEMSG;
                                    if (strZSUCC == "S")
                                    {
                                        for (int i = 0; i < dtDataFromSAP.Rows.Count; i++)
                                        {
                                            string strQty = dtDataFromSAP.Rows[i]["MENGE"].ToString();
                                            string strMBLNR = dtDataFromSAP.Rows[i]["MBLNR"].ToString();
                                            string strZEILE = dtDataFromSAP.Rows[i]["ZEILE"].ToString();
                                            strMessageFromSAP = strMBLNR;
                                            if (objStorageIn.GetUpdateWhdwnRemak(strWerks, strMBLNR, strZEMSG))
                                            {
                                                bolSAP = true;
                                            }
                                        }
                                    }
                                    else if (strZSUCC == "E")
                                    {
                                        if (strZEMSG.Contains(":has been posted!"))
                                        {
                                            #region   SAP已扣账选择是否仅入
                                            MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                                            DialogResult dr1 = MessageBox.Show("此单SAP已扣账，如QWMS未入库请点“确认”；已入库点“取消”", "提示!", mess);
                                            if (dr1.ToString() == "Cancel")
                                            {
                                                return;
                                            }
                                            #endregion
                                            for (int i = 0; i < dtDataFromSAP.Rows.Count; i++)
                                            {
                                                string strQty = dtDataFromSAP.Rows[i]["MENGE"].ToString();
                                                string strMBLNR = dtDataFromSAP.Rows[i]["MBLNR"].ToString();
                                                string strZEILE = dtDataFromSAP.Rows[i]["ZEILE"].ToString();
                                                strMessageFromSAP = strMBLNR;
                                                if (objStorageIn.GetUpdateWhdwnRemak(strWerks, strMBLNR, strZEMSG))
                                                {
                                                    bolSAP = true;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            MessageBox.Show("扣账失败！失败原因料号和SAP数据对不上，请找物料和SAP核对：" + strFailMatnr + " ， " + strZEMSG + "");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("读取SAP扣账文档失败，请重新操作退库读取SAP扣账文档");
                                    return;
                                }
                                #endregion
                            }
                        }
                        catch (Exception eSAP)
                        {
                            MessageBox.Show("btnConfirm_Click==> 入库到SAP异常，请确认" + eSAP.ToString());
                            return;
                        }

                        #endregion

                        #region 入库到QWMS
                        if (bolSAP)
                        // if (true)
                        {
                            try
                            {
                                #region 汇总QWMS入库数据
                                DataView dv = new DataView(dtData);
                                DataTable dtMatnr = dv.ToTable(true, new string[] { "MANDT", "COMCD", "WERKS", "LGORT", "MBLNR", "LOCAT", "MATNR", "CHARG", "INSMK" });
                                DataTable dtCombine = dtData.Clone();
                                foreach (DataRow dr in dtMatnr.Rows)
                                {
                                    DataRow[] drSelect = dtData.Select(" MANDT='" + dr["MANDT"].ToString() + "'       AND "
                                                                                               + " COMCD='" + dr["COMCD"].ToString() + "' AND "
                                                                                               + " WERKS='" + dr["WERKS"].ToString() + "' AND "
                                                                                               + " LGORT='" + dr["LGORT"].ToString() + "'   AND "
                                                                                               + " MBLNR='" + dr["MBLNR"].ToString() + "'   AND "
                                                                                               + " LOCAT='" + dr["LOCAT"].ToString() + "'    AND "
                                                                                               + "MATNR='" + dr["MATNR"].ToString() + "'    AND "
                                                                                               + " CHARG='" + dr["CHARG"].ToString() + "'  AND "
                                                                                               + " INSMK='" + dr["INSMK"].ToString() + "'  ");
                                    DataRow drQWMS = dtCombine.NewRow();
                                    int MengQty = 0;
                                    int Alqty = 0;
                                    if (drSelect.Length > 0)
                                    {
                                        foreach (DataRow dr1 in drSelect)
                                        {
                                            MengQty += Convert.ToInt32(dr1["MENGE"].ToString());
                                            Alqty += Convert.ToInt32(dr1["ALQTY"].ToString());
                                        }
                                        drQWMS["MANDT"] = drSelect[0]["MANDT"].ToString();
                                        drQWMS["COMCD"] = drSelect[0]["COMCD"].ToString();
                                        drQWMS["MTYPE"] = drSelect[0]["MTYPE"].ToString();
                                        drQWMS["WERKS"] = drSelect[0]["WERKS"].ToString();
                                        drQWMS["LGORT"] = drSelect[0]["LGORT"].ToString();
                                        drQWMS["MBLNR"] = drSelect[0]["MBLNR"].ToString();
                                        drQWMS["BOXID"] = drSelect[0]["BOXID"].ToString();
                                        drQWMS["LOCAT"] = drSelect[0]["LOCAT"].ToString();
                                        drQWMS["MATNR"] = drSelect[0]["MATNR"].ToString();
                                        drQWMS["CHARG"] = drSelect[0]["CHARG"].ToString();
                                        drQWMS["MENGE"] = MengQty;
                                        drQWMS["INSMK"] = drSelect[0]["INSMK"].ToString();
                                        drQWMS["SERNO"] = drSelect[0]["SERNO"].ToString();
                                        drQWMS["LIFNR"] = drSelect[0]["LIFNR"].ToString();
                                        drQWMS["EBELN"] = drSelect[0]["EBELN"].ToString();
                                        drQWMS["INDAT"] = drSelect[0]["INDAT"].ToString();
                                        drQWMS["REMAK1"] = drSelect[0]["REMAK1"].ToString();
                                        drQWMS["ALQTY"] = Alqty;
                                        if (Comcd == "9200" && Werks == "CS31" && (Lgort.Substring(0, 2) == "TW" || Lgort.Substring(0, 2) == "TC"))//添加退料日期
                                        {
                                            drQWMS["MCDAT"] = drSelect[0]["MCDAT"].ToString();
                                        }
                                        if (dtData.Columns.Contains("DACOD") && dtData.Columns.Contains("VEDAT"))
                                        {
                                            if (string.IsNullOrEmpty(drSelect[0]["LIFNR"].ToString()))
                                            {
                                                drQWMS["LIFNR"] = strLifnr;
                                            }
                                            drQWMS["DACOD"] = drSelect[0]["DACOD"].ToString();
                                            drQWMS["VEDAT"] = drSelect[0]["VEDAT"].ToString();
                                        }
                                        dtCombine.Rows.Add(drQWMS);
                                    }
                                }

                                #endregion

                                #region 入库QWMS并返回到OA
                              
                                if (objStorageIn.AddMCReturnInData(dtCombine, dtDataAll))  //入库QWMS
                                {
                                    string strMc = dtData.Rows[0]["MBLNR"].ToString();
                                    MessageBox.Show("QWMS与SAP均入库成功！SAP扣账编号为" + strMessageFromSAP + "");


                                    #region 增加和ASRS接口，储位问题一笔一个储位
                                    QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);
                                    if (Werks == "CS41" && objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
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

                                        foreach (DataRow dr in dtCombine.Rows)
                                        {
                                            DataRow drASRS = dtASRS.NewRow();

                                            drASRS["TRN_NO"] = dr["MBLNR"].ToString();
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
                                        LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                                        string strXML = objInterface.ConvertDataTableToXML(dtASRS);
                                        objLogData.XMLLog(strXML);
                                        if (objInterface.PostStorageInData(strXML) == "SUCCESS" ? true : false)
                                        {
                                            MessageBox.Show("Add OK!!,数据已同步到ASRS");
                                        }
                                        else
                                            MessageBox.Show("同步失败");
                                    }
                                    #endregion

                                    #region 同步OA + 记录日志
                                    if (dtData.Rows[0]["MTYPE"].ToString() == "OA_TIC") //OA_TIC
                                    {
                                        stsWarning.Text = "正在同步IworkFlow";
                                        objLogData.AddQWMSLOG(strMc, "OA", "Approve", "开始去OA同步签核", null, null);
                                        QWMS.WF.WF0152ForQWMS objWF = new QWMS.WF.WF0152ForQWMS();
                                        bool blResult = objWF.Approve(dtData.Rows[0]["MBLNR"].ToString());
                                        if (blResult)
                                        {
                                            objLogData.AddQWMSLOG(strMc, "OA", "Approve", "回传OA签核成功", "OK", null);
                                            stsWarning.Text = "已将数据同步IworkFolw";
                                            return;
                                        }
                                        else
                                        {
                                            objLogData.AddQWMSLOG(strMc, "OA", "Approve", "回传OA签核失败", "Fail", null);
                                            stsWarning.Text = "数据同步IworkFolw失败，请确认！";
                                        }
                                    }
                                    #endregion
                                }

                                #endregion
                            }
                            catch (Exception eQWMS)
                            {
                                MessageBox.Show("btnConfirm_Click==> 入库到QWMS异常，请确认" + eQWMS.ToString());
                            }
                        }

                        #endregion
                    }
                }
            }
            dtData.Rows.Clear();
            strFirstMblnr = "";
            dgvData.DataSource = null;
            this.checkBox1.Checked = false;
            #endregion
        }

        #endregion

        #region txtBoxid_KeyPress
        ArrayList arrbox = new ArrayList();
        int flagescan = 0;
        private void txtBoxid_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.checkBox1.Checked = false;
            stsWarning.Text = "";
            // strReferenceID = "";
            txtReferanceID.Text = "";
            if (e.KeyChar == (char)13)
            {
                #region 校验
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
                    Sound.Play(@"Sound\ERROR.wav");
                    return;
                }
                CheckLocat("");
                string strLocat = txtLocat.Text.Trim();
                if (string.IsNullOrEmpty(strLocat))
                {
                    stsWarning.Text = "Location can't be empty!!";
                    Sound.Play(@"Sound\ERROR.wav");
                    return;
                }
                else
                {
                    string strLocStatus = strType == "NEW" ? "0" : "1";
                    DataTable dtCheckLocat = objPlantData.GetAllLocatData(strWerks, strLgort, strLocat, strLocStatus);
                    if (dtCheckLocat.Rows.Count == 0)
                    {
                        stsWarning.Text = "该储位不存在或者储位状态不对，请确认并重新输入";
                        Sound.Play(@"Sound\ERROR.wav");
                        return;
                    }
                }

                string strBoxid = "";// txtBoxid.Text.Trim();
                string strQty = "0";
                if (txtBoxid.Text.ToString().Trim() != "")
                {
                    string[] arr = txtBoxid.Text.ToString().Replace('；',';').Split(';');
                    if (arr.Length > 0)
                    {
                        strBoxid = arr[0].ToString().ToUpper();
                    }
                    if (arr.Length > 4)
                    {
                        strQty = arr[4].ToString().ToUpper();
                        strLifnr = arr[3].ToString().ToUpper();
                    }
                    else
                    {
                        strQty = arr[3].ToString().ToUpper();
                    }
                    strBoxid = System.Text.RegularExpressions.Regex.Replace(strBoxid, "([ ]+)", "");
                }

                if (string.IsNullOrEmpty(strBoxid))
                {
                    stsWarning.Text = "Boxid can't be empty!!";
                    txtBoxid.Text = "";
                    Sound.Play(@"Sound\ERROR.wav");
                    return;
                }
                #endregion

                #region 获取BOXID信息
                try
                {
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);

                    DataTable dtTemp = new DataTable();

                    #region 通过调用Web引用获取BPM数据,未处理时可重复获取并覆盖原数据,BPM单据以11,F,S（9码）开头,不卡控年份
                    if (strBoxid.Substring(0, 1).Equals("1") || strBoxid.Substring(0, 1).Equals("F"))
                    {
                        if (GetDataFromBPM(Werks, Lgort, strBoxid.Substring(0, strBoxid.Length - 3), objStorageIn))
                        {
                            dtTemp = objStorageIn.GetReferenceIDbyBoxID(Werks, Lgort, strBoxid);
                        }
                    }
                    #endregion

                    #region   add  2019/12/02 Galen
                    else
                    {
                        if (string.IsNullOrEmpty(strFirstMblnr))
                        {
                            dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strBoxid.Substring(0, strBoxid.Length - 3).ToString(), "", "", "");
                            if (dtData.Rows.Count == 0)
                            {
                                GetDataFromOA(Werks, Lgort, strBoxid.Substring(0, strBoxid.Length - 3), objStorageIn);
                            }
                        }
                        dtTemp = objStorageIn.GetReferenceIDbyBoxID(Werks, Lgort, strBoxid);
                    }
                    #endregion
                    string strReferenceIDTemp = string.Empty;
                    string strInsmk = string.Empty;
                    string strDacod = string.Empty;
                    string strVedat = string.Empty;
                    if (dtTemp.Rows.Count > 0)
                    {
                        strReferenceIDTemp = dtTemp.Rows[0]["MBLNR"].ToString().Trim();
                        strInsmk = dtTemp.Rows[0]["INSMK"].ToString().Trim();
                        strDacod = dtTemp.Rows[0]["DACOD"].ToString().Trim();
                        #region Date Code规则校验
                        if (!string.IsNullOrEmpty(strDacod) && !string.IsNullOrEmpty(strLifnr))
                        {
                            string DC_After = objStorageData.WHDCR_Query(strLifnr,strDacod);
                            if (string.IsNullOrEmpty(DC_After))
                            {
                                stsWarning.Text = "该Date Code无对应转换日期，请确认！";
                                return;
                            }
                            else
                            {
                                DateTime dtTime = new DateTime();

                                if (DateTime.TryParse(DC_After, out dtTime))
                                {
                                    strVedat = dtTime.ToString("yyyyMMdd");
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "未找到该BOXID信息，请确认！";
                        Sound.Play(@"Sound\ERROR.wav");
                        txtBoxid.Text = "";
                        return;
                    }
                    if (!string.IsNullOrEmpty(strReferenceIDTemp))
                    {
                        if (string.IsNullOrEmpty(strFirstMblnr))
                        {
                            strFirstMblnr = strReferenceIDTemp;
                            if (!string.IsNullOrEmpty(strDacod) && !string.IsNullOrEmpty(strVedat))
                            {
                                dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strFirstMblnr, "", strDacod, strVedat);
                            }
                            else
                            {
                                dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strFirstMblnr, "", "", "");
                            }
                            if (dtData.Rows.Count > 0)
                            {
                                ShowDataGrid();
                            }
                            else
                            {
                                stsWarning.Text = "该BOXID已入库";
                                Sound.Play(@"Sound\ERROR.wav");
                                txtBoxid.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            if (strFirstMblnr != strReferenceIDTemp)
                            {
                                stsWarning.Text = "不同ReferenceID的BoxID不可以一起入库！";
                                Sound.Play(@"Sound\ERROR.wav");
                                txtBoxid.Text = "";
                                return;
                            }
                        }
                    }
                    //#region 检查是否重复刷入BOXID
                    //DataRow[] drCheck = dtData.Select("Cselect='true' AND BOXID='" + strBoxid + "'");
                    //if (drCheck.Length > 0)
                    //{
                    //    stsWarning.Text = "该BOXID已刷入，请确认";
                    //    Sound.Play(@"Sound\ERROR.wav");
                    //    return;
                    //}
                    //#endregion
                    flagescan = flagescan + 1;
                    for (int j = 0; j < dtData.Rows.Count; j++)
                    {
                        string strBoxidTemp = this.dgvData.Rows[j].Cells[2].Value.ToString(); ;
                        string stritem = "";
                        if (!string.IsNullOrEmpty(strBoxidTemp) && CheckLocat(""))
                        {

                            for (int m = 0; m < dtTemp.Rows.Count; m++)
                            {
                                if (!string.IsNullOrEmpty(strDacod) && !string.IsNullOrEmpty(strVedat))
                                {
                                    stritem = dtTemp.Rows[m]["BOXIDW"].ToString().Trim();
                                }
                                else
                                {
                                    stritem = dtTemp.Rows[m]["BOXID"].ToString().Trim();
                                }
                                if (strBoxidTemp == stritem)
                                {
                                    if (!arrbox.Contains(strBoxid))
                                    {
                                        this.dgvData.Rows[j].Cells[7].Value = Convert.ToInt32(this.dgvData.Rows[j].Cells[7].Value) + Convert.ToInt32(strQty);
                                    }
                                    else
                                    {
                                        if (flagescan > 1)
                                        {

                                            stsWarning.Text = "该BOXID已刷入，请确认";
                                            Sound.Play(@"Sound\ERROR.wav");
                                            return;
                                        }
                                    }

                                    this.dgvData.Rows[j].Cells[3].Value = strLocat;
                                    this.dgvData.Rows[j].Cells[0].Value = true;
                                    dtData.Rows[j]["Cselect"] = "true";
                                    string num1 = this.dgvData.Rows[j].Cells[6].Value.ToString();
                                    string num2 = this.dgvData.Rows[j].Cells[7].Value.ToString();
                                    //if (this.dgvData.Rows[j].Cells[6].Value.ToString() == this.dgvData.Rows[j].Cells[7].Value.ToString())
                                    if (num1 == num2)
                                    {

                                        dgvData.Rows[j].DefaultCellStyle.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        dgvData.Rows[j].DefaultCellStyle.BackColor = Color.Orange;
                                    }
                                    arrbox.Add(strBoxid);
                                    Sound.Play(@"Sound\ready.wav");


                                }
                            }
                        }
                    }
                    btnConfirm.Enabled = true;
                    this.txtBoxid.Text = "";
                    flagescan = 0;
                }
                catch (Exception eBoxid)
                {
                    MessageBox.Show("btnConfirm_Click ==> " + eBoxid.ToString());
                    Sound.Play(@"Sound\ERROR.wav");
                    txtBoxid.Text = "";
                    return;
                }
                #endregion
            }
        }
        #endregion

        #region txtReferanceID_DoubleClick
        private void txtReferanceID_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strFirstMblnr = "";
            this.checkBox1.Checked = true;
            if (checkBox1.Checked == true)
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
                    if (CheckLocat(""))
                    {
                        StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, "", "SapInReturn", Insmk);
                        objStorageIn_SapDataSelect.ShowDialog();
                        Mblnr = objStorageIn_SapDataSelect.Mblnr;
                        strReferenceID = Mblnr;
                        txtReferanceID.Text = strReferenceID;
                        QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                        dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strReferenceID, "", "", "");
                        ShowDataGrid();
                        btnConfirm.Enabled = true;
                        //this.checkBox1.Checked = false;
                    }
                }

                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
            else
            {
                stsWarning.Text = "请勾选无BOXID";
                return;
            }

        }

        #endregion

        #region txtSANo_KeyPress

        private void txtSANo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                stsWarning.Text = "";
                strFirstMblnr = "";
                this.checkBox1.Checked = true;
                string strSANo = txtSANo.Text.Trim().ToString();
                if ((txtBoxid.Text != "" || txtReferanceID.Text != "") && txtSANo.Text.Trim() != "")
                {
                    stsWarning.Text = "选择SA单号入库，请不要输入boxid和ReferanceID ";
                    return;
                }
                if (CheckLocat(""))
                {
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                    dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, "", strSANo, "", "");
                    ShowDataGrid();
                    btnConfirm.Enabled = true;
                    txtSANo.Text = "";
                    //this.checkBox1.Checked = false;
                }

            }
        }

        #endregion

        #region txtReferanceID_KeyPress

        private void txtReferanceID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                stsWarning.Text = "";
                strFirstMblnr = "";
                this.checkBox1.Checked = true;
                if (checkBox1.Checked == true)
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
                    strReferenceID = txtReferanceID.Text.Trim().ToString();
                    if (strReferenceID != "" && CheckLocat(""))
                    {
                        QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);

                        #region 通过调用Web引用获取BPM数据,未处理时可重复获取并覆盖原数据,BPM单据以11,F,S（9码）开头,不卡控年份
                        if (strReferenceID.Substring(0, 1).Equals("1") || strReferenceID.Substring(0, 1).Equals("F"))
                        {
                            if(GetDataFromBPM(Werks, Lgort, strReferenceID, objStorageIn))
                            {
                                dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strReferenceID, "", "", "");
                            }
                        }
                        #endregion

                        #region   add  2019/12/02 Galen 获取OA的数据,需卡控年份
                        else
                        {
                            dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, strReferenceID, "", "", "");
                            if (dtData.Rows.Count == 0)
                                dtData = GetDataFromOA(Werks, Lgort, strReferenceID, objStorageIn);
                        }
                        #endregion

                        ShowDataGrid();
                        btnConfirm.Enabled = true;
                        txtReferanceID.Text = "";
                        //this.checkBox1.Checked = false;
                    }
                }
                else
                {
                    stsWarning.Text = "请勾选无BOXID";
                    return;
                }
            }
        }

        #endregion


        //判断Loaction是否为空，若不是空，判断该Loaction是否存在或状态是否正确

        public bool CheckLocat(string strCheckLocat)
        {
            if (strCheckLocat == "")
            {
                strLocat = txtLocat.Text.Trim();//默认是已经选中的Loaction
            }
            else
            {
                strLocat = strCheckLocat;
            }
            if (string.IsNullOrEmpty(strLocat))
            {
                stsWarning.Text = "Location can't be empty!!";
                return false;
            }
            else
            {
                string strLocStatus = strType == "NEW" ? "0" : "1";
                DataTable dtCheckLocat = objPlantData.GetAllLocatData(strWerks, strLgort, strLocat, strLocStatus);
                if (dtCheckLocat.Rows.Count == 0)
                {
                    stsWarning.Text = "该储位不存在或者储位状态不对，请确认并重新输入";
                    return false;
                }
                #region 增加判断是不是ASRS空储位
                QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);

                if (Werks == "CS41" && objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
                {
                    if (objInterface.CheckLOCAT(txtLocat.Text.ToString()))//判断是否为ASRS空储位
                    {
                        stsWarning.Text = strCheckLocat + "该储位为ASRS的非空储位，请重新选择!";
                        return false; ;
                    }

                }
                #endregion
            }
            return true;
        }

        private void dgvData_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    row.Cells["LOCAT"].Style.NullValue = txtLocat.Text.Trim();
                }

            }
            catch
            {
            }
        }

        private void txtSANo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                checkBox1.Checked = true;
                txtReferanceID.Text = "";
                strReferenceID = "";
                strFirstMblnr = "";
                txtBoxid.Text = "";
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
                if (CheckLocat(""))
                {
                    StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, "", "SapInSAReturn", Insmk);
                    objStorageIn_SapDataSelect.ShowDialog();
                    Mblnr = objStorageIn_SapDataSelect.Mblnr;
                    txtSANo.Text = Mblnr;
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                    dtData = objStorageIn.GetAllDatabyReferenceID(Werks, Lgort, strLocat, "", txtSANo.Text.Trim(), "", "");
                    ShowDataGrid();
                    btnConfirm.Enabled = true;
                    //this.checkBox1.Checked = false;
                }
            }

            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }


        #region 调用OA的API获取判票信息    add by Galen  2019/12/4

        private DataTable GetDataFromOA(string strwerks, string strlgort, string RefID, QCI.QWMS.StorageIn objStorageIn)
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
                objLogData.AddQWMSLOG(RefID, "OA_TIC", "Approve", "开始调用OA的API获取数据", "N", string.Empty);
                //获取Token信息
                string ResultGetToken = HttpPostByHttpWebRequest("https://wf.quantacn.com/WFAPI/API/Login/Login", varUserInfo, "");

                //解析返回Json
                JObject jo = (JObject)JsonConvert.DeserializeObject(ResultGetToken);
                string strToken = "";
                if (jo["result"].ToString().ToUpper() == "TRUE")
                {
                    strToken = jo["token"].ToString();

                }
                //访问iworkflow接口
                //正式地址
                string Result = HttpPostByHttpWebRequest("https://wf.quantacn.com/WFAPI/API/WF0152ToQWMS/SetDataToQWMS", varPostData, strToken);
                //测试地址
                //string Result = HttpPostByHttpWebRequest("http://wftest.quantacn.com/WFAPI/API/WF0152ToQWMS/SetDataToQWMS", varPostData, strToken);

                #region 判断返回结果
                JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
                objJavaScriptSerializer.MaxJsonLength = Int32.MaxValue;
                Dictionary<string, object> DicText = objJavaScriptSerializer.Deserialize<Dictionary<string, object>>(Result);
                if (DicText.ContainsKey("Result"))
                {
                    MessageBox.Show(DicText["Message"].ToString());
                    return dtNULL;
                }
                dt = JsonToDataTable(Result);//Json转换成DataTable

                if (dt == null || dt.Rows.Count == 0)
                {
                    objLogData.AddQWMSLOG(RefID, "OA_TIC", "Approve", "获取不到OA的数据", "N", string.Empty);
                    MessageBox.Show("iWorkflow无数据，无法同步");
                    return dtNULL;
                }
                else
                    objLogData.AddQWMSLOG(RefID, "OA_TIC", "Approve", "获取到OA的数据", "Y", string.Empty);

                string strYear = dtpYear.Value.ToString("yyyy");
                DataTable dtYear = dt.Select("BUDAT LIKE '" + strYear + "%'").CopyToDataTable();

                if (dtYear.Rows[0]["Status"].ToString() != "A")
                {
                    MessageBox.Show("单据类型错误，无法同步");
                    return dtNULL;
                }
                if (dtYear.Rows[0]["ApplyStatus"].ToString() != "0" && dtYear.Rows[0]["ApplyStatus"].ToString() != "4")
                {
                    MessageBox.Show("此单据已签核完成，无法同步");
                    return dtNULL;
                }
                if (!dtYear.Rows[0]["FlowDescription"].ToString().Contains("仓管员"))
                {
                    MessageBox.Show("当前签核关卡为：" + dtYear.Rows[0]["FlowDescription"].ToString() + "，无法同步");
                    return dtNULL;
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

                if (objStorageIn.addOA_TICtoWHDWN(dtYear))//插入WHDWN表
                {
                    objLogData.AddQWMSLOG(RefID, "OA_TIC", "Approve", "插入到WHDWN", "Y", string.Empty);
                    dt = objStorageIn.GetAllDatabyReferenceID(strwerks, strlgort, strLocat, RefID, "", "", "");
                    return dt;
                }
                return dtNULL;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return dtNULL;
            }

        }

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
        #endregion

        #region 调用BPM的WebService获取报废信息
        private bool GetDataFromBPM(string strwerks, string strlgort, string RefID, QCI.QWMS.StorageIn objStorageIn)
        {
            DataTable dt = new DataTable();
            DataTable dtNULL = new DataTable();
            bool blResult = false;

            try
            {
                DataTable dtTemp = objStorageIn.GetWHdwnBPM_TIC(strWerks, strlgort, RefID);
                if (int.Parse(dtTemp.Rows[0]["OTQTY"].ToString()) > 0)
                {
                    MessageBox.Show("该单据已被处理");
                    return blResult;
                }
                objLogData.AddQWMSLOG(RefID, "BPM_TIC", "Approve", "开始调用BPM的Web引用获取数据", "N", string.Empty);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };//web service认证
                BPMTIC.GWF074_wsUploadApvsta objBPMTIC = new BPMTIC.GWF074_wsUploadApvsta();
                DataSet dsSet = objBPMTIC.GWF074_ProvideQWMSApproveScrapItem(string.Empty, RefID);
                DataTable dtMessage = dsSet.Tables[0];
                DataTable dtResult = dsSet.Tables[1];
                if (dtMessage.Rows[0]["Result"].ToString().Equals("Y"))
                {
                    if(dtResult.Rows[0]["WERKS"].ToString().Equals(strWerks)&&dtResult.Rows[0]["LGORT"].ToString().Equals(strlgort))
                    {
                        DataTable dtWhdwn = dtTemp.DefaultView.ToTable(false, "MBLNR", "ZEILE", "WERKS", "LGORT", "MATNR", "MENGE", "KOSTL", "BWART");
                        DataTable dtCompare = dtResult.DefaultView.ToTable(false, "MBLNR", "ZEILE", "WERKS", "LGORT", "MATNR", "MENGE", "KOSTL", "BWART");
                        #region 比对dtWhdwn和dtResult
                        var varCompare = dtCompare.AsEnumerable().Except(dtWhdwn.AsEnumerable(), DataRowComparer.Default);
                        if (varCompare.Count() > 0)
                        {
                            MessageBox.Show("当前打印数据与BPM数据不符，确认是否有修改数据，请重新打印");
                        }
                        else
                            blResult = true;
                    }
                    else
                    {
                        MessageBox.Show("输入的厂区仓别有误");
                    }
                        #endregion
                }
                else
                {
                    MessageBox.Show(dtMessage.Rows[0]["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
            return blResult;
        }
        #endregion

    }
}