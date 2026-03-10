using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using QWMS.Common;
using System.Collections;
using System.Diagnostics;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class Model_InventoryQuery : Form
    {
        #region Parameters
        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strType = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strMblnr = "";
        private DataTable dtData = new DataTable();
        private DataTable dtDataGrid = new DataTable();
        private ModelsData objModelsData;
        #endregion

        #region Function
        public Model_InventoryQuery()
        {
            InitializeComponent();
        }

        public Model_InventoryQuery(UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            objModelsData = new ModelsData(varUserData, Werks, Lgort, Progid);
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
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

        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
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
        #endregion

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.tsslDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.tsslMandt.Text = Mandt;
            this.tsslComcd.Text = Comcd;
            this.tsslUsrnm.Text = Usrnm;
        }
        #endregion

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
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                tsslWarning.Text = "";
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
        //显示DataGridView
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "MANDT";
                dgvcMANDT.HeaderText = "Client";
                dgvcMANDT.Width = 50;
                dgvcMANDT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMANDT);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "Company Code";
                dgvcCOMCD.Width = 60;
                dgvcCOMCD.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCOMCD);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 50;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 50;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 60;
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Model No.";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "Asset No";
                dgvcAssetsNo.Width = 100;
                dgvcAssetsNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "Model Name";
                dgvcItemName.Width = 240;
                dgvcItemName.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcItemName);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 50;
                dgvcBU.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "Machine";
                dgvcMachine.Width = 50;
                dgvcMachine.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMachine);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Quantity";
                dgvcMENGE.Width = 60;
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "Weight";
                dgvcNweight.Width = 70;
                dgvcNweight.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcNweight);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "MODAT";
                dgvcCRDAT.HeaderText = "TRANSDAT";
                dgvcCRDAT.Width = 70;
                dgvcCRDAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRDAT);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "Remark";
                dgvcRemark.Width = 70;
                dgvcRemark.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemark);


                dgvData.DataSource = dtDataGrid;
                lblRecords.Text = dtDataGrid.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #region Event
        //Query按钮
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.btnQuery.Enabled = false;
            strWerks = cmbWerks.Text.ToString().Trim();
            strLgort = cmbLgort.Text.ToString().Trim();
            strLocat = txtLocat.Text.ToString().Trim();
            strMatnr = txtMatnr.Text.ToString().Trim();
            strMblnr = txtMblnr.Text.ToString().Trim();

            if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
            {                
                if (string.IsNullOrEmpty(strWerks) || string.IsNullOrEmpty(strLgort))
                {
                    tsslWarning.Text = "Plant and Storage can't be empty!!";
                    MessageBox.Show("Plant and Storage can't be empty!!");
                    return;
                }
                else
                {
                    ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
                    dtDataGrid = objModelsData.ModelInventoryQuery(strWerks, strLgort, strLocat, strMatnr, strMblnr);

                }
            }
            if(!string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
            {
                # region 校验格式
                string strFileName = txtFilePath.Text.Trim();
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    tsslWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                }
                # endregion

                tsslWarning.Text = "Please don't close the window ,Check the data...";
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
               // string strCmd = "select * from [Sheet1$]";
                //dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtData.Rows.Count <= 0)
                {
                    tsslWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
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
                //获取基础信息
                dtDataGrid = objModelsData.ModelInventoryQuery(strWerks, strLgort, strLocat, dtImport);
            }
            ShowDataGrid();
        }
        //Refresh 按钮
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbWerks.SelectedIndex = -1;
            this.cmbLgort.SelectedIndex = -1;
            this.txtLocat.Text = "";
            this.txtMatnr.Text = "";
            this.txtMblnr.Text = "";
            this.tsslWarning.Text = "";
            this.btnQuery.Enabled = true;
            this.dgvData.DataSource = null;
            this.txtFilePath.Text = "";
        }
        //Exit 按钮
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //储位DoubleClick按钮
        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                if (string.IsNullOrEmpty(strWerks) || string.IsNullOrEmpty(strLgort))
                {
                    tsslWarning.Text = "Plant or Storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ALL");
                    objManage_LocationSelect.ShowDialog();
                    txtLocat.Text = objManage_LocationSelect.Locat;                    
                }
            }   
            catch (Exception ex)
            {
                tsslWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

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
