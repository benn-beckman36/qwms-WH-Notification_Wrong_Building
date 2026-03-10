using System;
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

namespace QWMS
{
    public partial class Admin_BatchImportPartNo : Form
    {
        public string MANTR { get; set; }
        public string MAKTX { get; set; }
        public string Progid { get; set; }
        public string Type { get; set; }
        private DataTable dtData = new DataTable();
        private DataTable dtImport = new DataTable();
        private Admin objAdmin;
        UserInfo UserData = new UserInfo();

        public Admin_BatchImportPartNo()
        {
            InitializeComponent();
            btnExecute.Enabled = false;
            btnImport.Enabled = false;
        }

        
        # region 构建式
        public Admin_BatchImportPartNo(UserInfo varUserData, string strProgid,string strType)
        {
            InitializeComponent();
            UserData = varUserData;

            Progid = strProgid;
            Type = strType;
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


        private void lnkSample_Click(object sender, EventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "PartNo模板.xlsx";
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();


            try
            {
                DataGridViewTextBoxColumn labMANTR = new DataGridViewTextBoxColumn();
                labMANTR.DataPropertyName = "PartNo";
                labMANTR.HeaderText = "Part.No";
                labMANTR.ReadOnly = true;
                dgvData.Columns.Add(labMANTR);

                DataGridViewTextBoxColumn labDescription = new DataGridViewTextBoxColumn();
                labDescription.DataPropertyName = "Description";
                labDescription.HeaderText = "Description";
                labDescription.ReadOnly = true;
                dgvData.Columns.Add(labDescription);



                dgvData.DataSource = dtImport;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {		
			if(ofdOpenFile.ShowDialog() == DialogResult.OK)
			{
				this.txtFilePath.Text = ofdOpenFile.FileName;
                btnImport.Enabled = true;
                btnExecute.Enabled = false;
			}
		}
        private void btnImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            string strExistedPart = "";
            try
            {
                if (string.IsNullOrEmpty(this.txtFilePath.Text.Trim()))
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                # region 校验格式
                string strFileName = this.txtFilePath.Text;
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                    return;
                }
                # endregion
                stsWarning.Text = "Please don't close the window ,Check the data...";
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper ();
                //string strCmd = "select * from [Sheet1$]";
                //dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtData.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                    return;
                }
                #endregion
                dtImport = dtData.Clone();

                foreach (DataRow dr in dtData.Rows)
                {
                    //檢查是不是空值
                    if (!string.IsNullOrEmpty(dr["PartNo"].ToString().Trim()))
                    {
                        dtImport.ImportRow(dr);
                    }
                }
                if (dtImport.DefaultView.ToTable(true, "PartNo").Rows.Count < dtImport.Rows.Count)
                {
                    stsWarning.Text = "上传模板中有重复料号！";
                    return;
                }

                if (Type == "Add")
                {
                    //檢查料號是否已存在
                    foreach (DataRow dr in dtImport.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["PartNo"].ToString().Trim()) ||
                            string.IsNullOrEmpty(dr["Description"].ToString().Trim()))
                        {
                            stsWarning.Text = "Part No. and Description can't be empty!!";
                            return;
                        }
                        if (objAdmin.CheckExistedPart(dr["PartNo"].ToString().Trim()))
                        {
                            strExistedPart += dr["PartNo"].ToString().Trim() + ";";
                        }
                    }
                    if (strExistedPart.Length > 1)
                    {
                        MessageBox.Show("Part No:" + strExistedPart + " is already existed!!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (Type == "Modify")
                {
                    foreach (DataRow dr in dtImport.Rows)
                    {
                        if (string.IsNullOrEmpty(dr["PartNo"].ToString().Trim()) ||
                            string.IsNullOrEmpty(dr["Description"].ToString().Trim()))
                        {
                            stsWarning.Text = "Part No. and Description can't be empty!!";
                            return;
                        }
                        if (!objAdmin.CheckExistedPart(dr["PartNo"].ToString().Trim()))
                        {
                            strExistedPart += dr["PartNo"].ToString().Trim() + ";";
                        }
                    }
                    if (strExistedPart.Length > 1)
                    {
                        MessageBox.Show("Part No:" + strExistedPart + " is Not existed!!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                ShowDataGrid();
                stsWarning.Text = "Import OK!";
                btnImport.Enabled = false;
                btnExecute.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try {
                foreach (DataRow dr in dtImport.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["PartNo"].ToString().Trim()) || !string.IsNullOrEmpty(dr["Description"].ToString().Trim()))
                    {
                        if (Type == "Add")
                        {
                            if (objAdmin.AddPartNo(dr["PartNo"].ToString().Trim(), dr["Description"].ToString().Trim()))
                            {
                                stsWarning.Text = "Add OK!";
                                this.btnExecute.Enabled = false;
                                this.btnImport.Enabled = false;
                            }
                            else
                            {
                                stsWarning.Text = "Add fail!";
                                this.btnExecute.Enabled = false;
                                this.btnImport.Enabled = false;
                            }
                        }
                        if (Type == "Modify")
                        {
                            if (objAdmin.ModifyPartNo(dr["PartNo"].ToString().Trim(), dr["Description"].ToString().Trim()))
                            {
                                stsWarning.Text = "Modify OK!";
                                this.btnExecute.Enabled = false;
                                this.btnImport.Enabled = false;
                            }
                            else
                            {
                                stsWarning.Text = "Modify fail!";
                                this.btnExecute.Enabled = false;
                                this.btnImport.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
    }
}
