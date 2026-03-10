using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using QCI.QWMS;
using QWMS.Common;
using QCI_QWMS_Models;

namespace QWMS.Models
{
    public partial class Model_StrorageIn_OffLineIn_BatchImport : Form
    {
        public string MANTR { get; set; }
        public string MAKTX { get; set; }
        public string Progid { get; set; }
        private Admin objAdmin;
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";

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
        private DataTable dtData = new DataTable();
        private DataTable dtData2 = new DataTable();
        
        UserInfo UserData = new UserInfo();

        public Model_StrorageIn_OffLineIn_BatchImport()
        {
            InitializeComponent();
            btnExecute.Enabled = false;
            btnImport.Enabled = false;
        }

        # region 构建式
        public Model_StrorageIn_OffLineIn_BatchImport(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;

            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        # endregion

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
                btnImport.Enabled = true;
                btnExecute.Enabled = false;
            }
        }

        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "ModelNo模板.xlsx";
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                # region 校验格式
                string strFileName = this.txtFilePath.Text.Trim();
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                }
                # endregion

                stsWarning.Text = "Please don't close the window ,Check the data...";
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                //string strCmd = "select * from [Sheet1$]";
                //dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
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
                    if (dr["AssetsNo"].ToString().Trim() != "")
                    {
                        if (!objModelsData.CheckExistedAssetsModel(dr["ModelNo"].ToString().Trim(), dr["AssetsNo"].ToString().Trim()))
                        {
                            MessageBox.Show(
                                dr["ModelNo"].ToString().Trim() + "and" + dr["AssetsNo"].ToString().Trim() + " doesn't exist!!",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (!objModelsData.CheckExistedAssetsModel(dr["ModelNo"].ToString().Trim(), ""))
                    {
                        MessageBox.Show(
                            dr["ModelNo"].ToString().Trim() + " doesn't exist!!",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    }

                    //else
                    //{
                    //    if (!objModelsData.CheckExistedAssetsModel(dr["ModelNo"].ToString().Trim(), ""))
                    //    {
                    //        MessageBox.Show(
                    //            dr["ModelNo"].ToString().Trim() + " doesn't exist!!",
                    //            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}
                }


              //获取基础信息
                dtData2 = objModelsData.QueryModelData(Locat,dtImport);
                      


                if (dtData2.Rows.Count > 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "Import OK!";
                    btnImport.Enabled = false;
                    btnExecute.Enabled = true;
                }

                if (dtData2.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcModelNO = new DataGridViewTextBoxColumn();
                dgvcModelNO.DataPropertyName = "ModelNO";
                dgvcModelNO.HeaderText = "模号";
                dgvcModelNO.Width = 90;
                dgvcModelNO.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcModelNO);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "资产编号";
                dgvcAssetsNo.Width = 90;
                dgvcAssetsNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "品名";
                dgvcItemName.Width = 50;
                dgvcItemName.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcItemName);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 60;
                dgvcBU.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 90;
                dgvcMachine.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMachine);

                DataGridViewTextBoxColumn dgvcQuantity = new DataGridViewTextBoxColumn();
                dgvcQuantity.DataPropertyName = "Quantity";
                dgvcQuantity.HeaderText = "数量";
                dgvcQuantity.Width = 90;
                dgvcQuantity.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQuantity);

                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "净重";
                dgvcNweight.Width = 90;
                dgvcNweight.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcNweight);

                DataGridViewTextBoxColumn dgvcPoNo = new DataGridViewTextBoxColumn();
                dgvcPoNo.DataPropertyName = "PoNo";
                dgvcPoNo.HeaderText = "PO.";
                dgvcPoNo.Width = 90;
                dgvcPoNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPoNo);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "模具备注";
                dgvcRemark.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemark);

                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "建立者";
                dgvcCRNAM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRNAM);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRDAT";
                dgvcCRNAM.HeaderText = "建立时间";
                dgvcCRNAM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRDAT);

                dgvData.DataSource = dtData2;
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData2.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    return;
                }

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                
                
                if (objModelsData.CheckRepeatAssetsModel(Locat, dtData2))
                {
                    for (int i = 0; i < dtData2.Rows.Count; i++)
                    {
                        MessageBox.Show(dtData2.Rows[i]["ModelNo"].ToString() + "has storage in before!!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
                else
                {
                    if (objModelsData.ModelOffLineInData(Werks, Lgort, Locat, dtData2))
                    {
                        stsWarning.Text = "Add OK!!";
                        this.btnExecute.Enabled = false;
                        return;
                    }
                    else
                    {
                        stsWarning.Text = "Add fail!! " + objModelsData.ERRMSG;
                        this.btnExecute.Enabled = true;
                        return;
                    }
                }
                

                
                
            }
            catch (Exception ex)
            {

                stsWarning.Text = ex.Message;
                return;
            }
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}
