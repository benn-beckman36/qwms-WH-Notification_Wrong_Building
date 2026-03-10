using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using System.Collections;
using QWMS.Common;
using System.Diagnostics;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class ModelBaseInfo : Form
    {
        ModelInfo objData;
        DataTable dtModelInfo = new DataTable();
        UserInfo UserData = new UserInfo();
        private Admin objAdmin;
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strUsrnm = "";
        DataTable dtData= new DataTable();
        private System.Windows.Forms.StatusBarPanel stsWarning;
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

        public ModelBaseInfo(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            objData = new ModelInfo(UserData);
            Progid = strProgid;
            Usrnm = varUserData.UserId;
            try
            {
                objAdmin = new Admin(UserData, Progid);

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Button

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MaintainModelBaseInfo fm = new MaintainModelBaseInfo(UserData, this, "");
            fm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ArrayList arr = GetSingleModelNo();
            if (arr.Count < 1)
            {
                MessageBox.Show("请勾选需要修改的数据！");
                return;
            }
            MaintainModelBaseInfo fm = new MaintainModelBaseInfo(UserData, this, arr[0].ToString());
            fm.ShowDialog();
            Query();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strModelNOs = GetSelectedModelNOs();
            if (!string.IsNullOrEmpty(strModelNOs))
            {
                if (MessageBox.Show("确定要删除吗？") == DialogResult.OK)
                {
                    //检查模具是否可修改或删除
                  
                    if (objData.CheckModel(strModelNOs) == true)
                    {
                        MessageBox.Show("库中模具不允许删除！");
                        return;
                    }
                   
                    if (objData.DeleteModelInfo(strModelNOs))
                    {
                        MessageBox.Show("删除成功！");
                        this.Query();
                    }
                }
            }
        }
        #endregion

        #region Function

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Selected";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 30;
                this.gvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcModelNo = new DataGridViewTextBoxColumn();
                dgvcModelNo.DataPropertyName = "ModelNo";
                dgvcModelNo.HeaderText = "模具号";
                dgvcModelNo.Width = 70;
                dgvcModelNo.ReadOnly = true;
                this.gvData.Columns.Add(dgvcModelNo);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "资产编号";
                dgvcAssetsNo.Width = 70;
                dgvcAssetsNo.ReadOnly = true;
                this.gvData.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "品名";
                dgvcItemName.Width = 100;
                dgvcItemName.ReadOnly = true;
                this.gvData.Columns.Add(dgvcItemName);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 50;
                dgvcBU.ReadOnly = true;
                this.gvData.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 80;
                dgvcMachine.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMachine);

                DataGridViewTextBoxColumn dgvcQuantity = new DataGridViewTextBoxColumn();
                dgvcQuantity.DataPropertyName = "Quantity";
                dgvcQuantity.HeaderText = "数量";
                dgvcQuantity.Width = 20;
                dgvcQuantity.ReadOnly = true;
                this.gvData.Columns.Add(dgvcQuantity);

                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "净重";
                dgvcNweight.Width = 30;
                dgvcNweight.ReadOnly = true;
                this.gvData.Columns.Add(dgvcNweight);

                DataGridViewTextBoxColumn dgvcPoNo = new DataGridViewTextBoxColumn();
                dgvcPoNo.DataPropertyName = "PoNo";
                dgvcPoNo.HeaderText = "Po No";
                dgvcPoNo.Width = 50;
                dgvcPoNo.ReadOnly = true;
                this.gvData.Columns.Add(dgvcPoNo);

                DataGridViewTextBoxColumn dgvcRemark1 = new DataGridViewTextBoxColumn();
                dgvcRemark1.DataPropertyName = "Remark";
                dgvcRemark1.HeaderText = "备注";
                dgvcRemark1.Width = 100;
                dgvcRemark1.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRemark1);

                DataGridViewTextBoxColumn dgvcCreater = new DataGridViewTextBoxColumn();
                dgvcCreater.DataPropertyName = "CRNAM";
                dgvcCreater.HeaderText = "创建人";
                dgvcCreater.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCreater);

                DataGridViewTextBoxColumn dgvcCreatTime = new DataGridViewTextBoxColumn();
                dgvcCreatTime.DataPropertyName = "CRDAT";
                dgvcCreatTime.HeaderText = "创建时间";
                dgvcCreatTime.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCreatTime);

                DataGridViewTextBoxColumn dgvcMODNM = new DataGridViewTextBoxColumn();
                dgvcMODNM.DataPropertyName = "MODNM";
                dgvcMODNM.HeaderText = "修改人";
                dgvcMODNM.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMODNM);

                DataGridViewTextBoxColumn dgvcMODTM = new DataGridViewTextBoxColumn();
                dgvcMODTM.DataPropertyName = "MODTM";
                dgvcMODTM.HeaderText = "修改时间";
                dgvcMODTM.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMODTM);

                gvData.DataSource = dtModelInfo;
                
                lblData.Text = dtModelInfo.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        public void Query()
        {
            if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
            {
                string strModelNo = txtModelNo.Text.ToString().Trim();
                string strAssetsNo = txtAssetsNo.Text.ToString().Trim();
                dtModelInfo = objData.GetModelInfo(strModelNo, strAssetsNo);
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
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
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
                dtModelInfo = objData.GetModelInfo(dtImport);
            }
            ShowDataGrid();
        }

        private string GetSelectedModelNOs()//获取选中的ModelNO
        {
            string strModelNOs = string.Empty;
            DataTable dgv = (DataTable)gvData.DataSource;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i]["Selected"].ToString() == "True")
                {
                    strModelNOs += dgv.Rows[i]["ModelNO"].ToString()+"','";
                }
            }
            if (strModelNOs.Length > 0)
            {
                strModelNOs = strModelNOs.Substring(0,strModelNOs.Length-3);
            }
            return strModelNOs;
        }

        private ArrayList GetSingleModelNo()//获取单个模具号
        {
            ArrayList arry = new ArrayList();
            DataTable dgv = (DataTable)gvData.DataSource;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i]["Selected"].ToString()=="True")
                {
                    arry.Add(dgv.Rows[i]["ModelNO"].ToString());
                }
            }
            return arry;
        }

        #endregion

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
            }
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

    }
}
