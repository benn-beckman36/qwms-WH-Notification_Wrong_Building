using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using QCI.QWMS;
using QCI_QWMS_Models; 
using QWMS.Common;
using System.Diagnostics;
using System.Windows.Forms;

namespace QWMS.Models
{
    public partial class Model_CreateTransferOrder : Form
    {
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strtoWerks = "";
        private string strLgort = "";
        private string strtoLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private int intFormIndex = 0;
        private bool bolDuplicate = false;
        private ArrayList aryMatnr = new ArrayList();
        private DataTable dtData = new DataTable();
        private DataTable dtModelOrder = new DataTable();
        UserInfo UserData = new UserInfo();
        private string strOUTMBLNR = "";
        private string strINMBLNR = "";
           
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
        public string ToWerks
        {
            get
            {
                return strtoWerks;
            }
            set
            {
                strtoWerks = value;
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
        public string ToLgort
        {
            get
            {
                return strtoLgort;
            }
            set
            {
                strtoLgort = value;
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
        public ArrayList Matnrs
        {
            get
            {
                return aryMatnr;
            }
            set
            {
                aryMatnr = value;
            }
        }
        #endregion
        #region 构造
        public Model_CreateTransferOrder(ref UserInfo varUserData, string strProgid)
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
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");

                }
                else
                {
                    ShowDdlWerks();
                    ShowDdlLgort();
                    btnSave.Enabled = false;
                    btnPrint.Enabled = false;
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    ShowDdlToWerks();
                    ShowDdlToLgort();
                    if (cmbToWerks.Items.Count > 0)
                    {
                        this.cmbToWerks.SelectedIndex = 0;
                    }
                    if (cmbToLGORT.Items.Count > 0)
                    {
                        this.cmbToLGORT.SelectedIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //				stsWarning.Text = ex.Message;
            }
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
                    cmbToWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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


        #region 廠區下拉選單
        private void ShowDdlToWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                cmbToWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbToWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlToWerks()");
            }
        }
        #endregion
        private void cmbToWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlToLgort();
        }
        #region 倉別下拉選單
        private void ShowDdlToLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbToWerks.SelectedIndex != -1)
                {
                    strtoWerks = cmbToWerks.Items[cmbToWerks.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strtoWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbToLGORT.SelectedIndex != -1)
                {
                    strtoLgort = cmbToLGORT.Items[cmbToLGORT.SelectedIndex].ToString();
                }
                else
                {
                    cmbToLGORT.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbToLGORT.Items.Clear();
                    strtoLgort = "";
                }
                else
                {
                    cmbToLGORT.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbToLGORT.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strtoLgort && strtoLgort != "")
                        {
                            cmbToLGORT.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdltoLgort()");
            }
        }
        #endregion
       

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try{
                 stsWarning.Text = "";   
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }
                if (cmbToWerks.SelectedIndex != -1)
                {
                    ToWerks = cmbToWerks.Items[cmbToWerks.SelectedIndex].ToString();
                }
                else
                {
                    ToWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }

                if (cmbToLGORT.SelectedIndex != -1)
                {
                    ToLgort = cmbToLGORT.Items[cmbToLGORT.SelectedIndex].ToString();
                }
                else
                {
                    ToLgort = "";
                }

                //廠區不為空
                if (Werks == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (ToWerks == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if ((Werks + Lgort).ToString() == (ToWerks + ToLgort).ToString())
                {
                    stsWarning.Text = "调出、调入厂区仓别不能相同！";
                    return;
                }

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort,Progid);
                if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                {
                    //單號不為空
                    if (string.IsNullOrEmpty(txtaryMatnr.Text.Trim()))
                    {
                        stsWarning.Text = "Model No can't be empty!!";
                        return;
                    }
                    string strMatnrs = txtaryMatnr.Text.Trim().Replace(",", "','");
                    Matnrs.Clear();

                    dtModelOrder = objModelsData.GetModelsListInWhitm(strMatnrs);
                }
                if (!string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                {
                    # region 校验格式
                    string strFileName = this.txtFilePath.Text.Trim();
                    string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                    if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                    {
                        stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！"; 
                    }
                    # endregion

                    QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                   // string strCmd = "select * from [Sheet1$]";
                   // dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                    # region 获取EXCEL数据
                    dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                    if (dtData.Rows.Count <= 0)
                    {
                        stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                    }
                    #endregion
                    //DataRow[] temp = dtData.Select(" isnull(ModelNo,'')<>'' ");

                    DataTable dtImport = new DataTable();
                    dtImport = dtData.Clone();


                    //foreach (DataRow dataRow in temp)
                    //{
                    //    dtImport.ImportRow(dataRow);
                    //}


                    foreach (DataRow dr in dtData.Rows)
                    {
                        //檢查是不是空值
                        if (!string.IsNullOrEmpty(dr["ModelNo"].ToString().Trim()))
                        {
                            //stsWarning.Text = "ModelNo. can't be empty!!";
                            //return;
                            dtImport.ImportRow(dr);

                            //檢查模具是否存在
                            if (!objModelsData.CheckExistedAssetsModel(dr["ModelNo"].ToString().Trim(), ""))
                            {
                                MessageBox.Show(
                                    dr["ModelNo"].ToString().Trim() + " doesn't exist!!",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strMatnr += dtData.Rows[i]["ModelNo"].ToString().Trim() + "','";
                    }
                    strMatnr = strMatnr.Substring(0, strMatnr.Length - 3);
                    //获取基础信息
                    dtModelOrder = objModelsData.GetModelsListInWhitm(strMatnr);
                }
                

                if (dtModelOrder.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
                ShowSourceDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtaryMatnr_DoubleClick(object sender, EventArgs e)
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

                //廠區不為空
                if (Werks == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                aryMatnr.Clear();
                if (txtaryMatnr.Text.Trim() != "")
                {
                    aryMatnr.Add(txtaryMatnr.Text.Trim());
                }
                Model_ModelNoSelect objModelModelNoSelect = new Model_ModelNoSelect(UserData, strWerks, strLgort, Progid);
                objModelModelNoSelect.ShowDialog();
                Matnrs = objModelModelNoSelect.Matnrs;
                txtaryMatnr.Text = GetMantrData();
                if (Matnrs.Count > 0)
                {
                    this.txtaryMatnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #region GetMantrData
        private string GetMantrData()
        {
            try
            {
                StringBuilder sbMatnr = new StringBuilder();
                sbMatnr.Remove(0, sbMatnr.Length);
                for (int i = 0; i < Matnrs.Count; i++)
                {
                    if (i != 0)
                    {
                        sbMatnr.Append(",");      
                    }
                    if (i > 15)
                    {
                        MessageBox.Show(@"一个调拨单不能超过15个模具");
                        return "" ;
                    }
                    sbMatnr.Append(Matnrs[i].ToString().Trim());

                }
                return sbMatnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }
        #endregion

        #region ShowSourceDataGrid
        private void ShowSourceDataGrid()
        {

            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MATNR";
                dgvcMblnr.HeaderText = "模号";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "品名";
                dgvcItemName.Width = 90;
                dgvcItemName.ReadOnly = true;
                dgvData.Columns.Add(dgvcItemName);

                //MATNR
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本号";
                dgvcCHARG.Width = 90;
                dgvcCHARG.ReadOnly = true;
                dgvData.Columns.Add(dgvcCHARG);

                //EBELN
                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 90;
                dgvcBU.ReadOnly = true;
                dgvData.Columns.Add(dgvcBU);

                //KDMAT
                DataGridViewTextBoxColumn dgvcMACHINE = new DataGridViewTextBoxColumn();
                dgvcMACHINE.DataPropertyName = "MACHINE";
                dgvcMACHINE.HeaderText = "机种";
                dgvcMACHINE.Width = 90;
                dgvcMACHINE.ReadOnly = true;
                dgvData.Columns.Add(dgvcMACHINE);

                //INSMK
                DataGridViewTextBoxColumn dgvcPoNo = new DataGridViewTextBoxColumn();
                dgvcPoNo.DataPropertyName = "PoNo";
                dgvcPoNo.HeaderText = "PO";
                dgvcPoNo.Width = 60;
                dgvcPoNo.ReadOnly = true;
                dgvData.Columns.Add(dgvcPoNo);

                //CHARG
                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "资产编号";
                dgvcAssetsNo.Width = 60;
                dgvcAssetsNo.ReadOnly = true;
                dgvData.Columns.Add(dgvcAssetsNo);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "数量";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "净重";
                dgvcNweight.Width = 90;
                dgvcNweight.ReadOnly = true;
                dgvData.Columns.Add(dgvcNweight);
                //Remark
                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark ";
                dgvcRemark.HeaderText = "模具备注";
                dgvcRemark.Width = 90;
                dgvcRemark.ReadOnly = true;
                dgvData.Columns.Add(dgvcRemark);

                dgvData.DataSource = dtModelOrder;
                lblCount.Text = dtModelOrder.Rows.Count.ToString() + " records";

                if (dtModelOrder.Rows.Count > 0)
                {
                    cmbLgort.Enabled = false;
                    cmbWerks.Enabled = false;
                    cmbToLGORT.Enabled = false;
                    cmbToWerks.Enabled = false;
                    txtaryMatnr.Enabled = false;
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            this.strWerks = "";
            this.strLgort = "";
            this.strtoLgort = "";
            this.strtoWerks = "";
            this.lblCount.Text = "0 records";
            this.txtFilePath.Text = "";
            this.txtaryMatnr.Text = "";
            this.aryMatnr.Clear();

            this.btnConfirm.Enabled = true;
            this.dtModelOrder.Clear();
            this.dtData.Clear();
          
            this.dgvData.DataSource = null;
            this.stsWarning.Text = "";
            this.btnSave.Enabled = false;
            this.txtaryMatnr.Focus();
            cmbLgort.Enabled = true;
            cmbWerks.Enabled = true;
            cmbToLGORT.Enabled = true;
            cmbToWerks.Enabled = true;
            txtaryMatnr.Enabled = true;
            btnPrint.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                DataTable dtCheck = new DataTable();
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort,Progid);
                dtCheck = objModelsData.CheckModelNo(dtModelOrder);
                string strMark = txtRemark.Text.Trim();
                if (dtCheck.Rows.Count > 0)
                {
                    stsWarning.Text = "存在两个相同的模号：" + dtCheck.Rows[0]["MATNR"].ToString();
                    btnSave.Enabled = true;
                    return;
                }
                else
                {
                    for (int i = 0; i < dtModelOrder.Rows.Count; i++)
                    {
                        if (dtModelOrder.Rows[i]["Nweight"].ToString() == "" ||
                            Convert.ToDecimal(dtModelOrder.Rows[i]["Nweight"].ToString()) == 0)
                        {
                            stsWarning.Text = "净重不能为空或0，请维护主档重量,模号：" + dtModelOrder.Rows[i]["MATNR"].ToString();
                            btnSave.Enabled = false;
                            return;
                        }
                    }
                    DataTable dtResult = new DataTable();
                    dtResult = objModelsData.CreateModelOrder(dtModelOrder, Werks, Lgort, ToWerks, ToLgort, strMark);
                     strOUTMBLNR = dtResult.Rows[0]["OUTMBLNR"].ToString();
                     strINMBLNR = dtResult.Rows[0]["INMBLNR"].ToString();
                    MessageBox.Show("调拨出单号：" + strOUTMBLNR + "\t调拨入单号：" + strINMBLNR);
                    btnSave.Enabled = false;
                    btnPrint.Enabled = true;
                    PrintReport();
                }
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }

        private void PrintReport()
        {
            try
            {

                DataTable dtPrint = new DataTable();
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                dtPrint = objModelsData.GetPrintModelData(strWerks, strLgort, strOUTMBLNR, "", "", "");
                if (dtPrint.Rows.Count > 0)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "MODEL", dtPrint);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                else
                {
                    throw new Exception("打印无数据！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        

        private void txtaryMatnr_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("双击输入框显示模具选择表", txtaryMatnr);
        }

        private void txtaryMatnr_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.Show("双击输入框显示模具选择表", txtaryMatnr);
        }

        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "Model库存批量查询模板.xlsx";
                try
                {
                    Process.Start("Excel", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
            }
        } 

        }

    
}
