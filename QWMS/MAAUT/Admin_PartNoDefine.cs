using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;
using System.Diagnostics;
using Microsoft.SqlServer.Server;
using System.Web;

namespace QWMS
{
    public partial class Admin_PartNoDefine : Form
    {

        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";//QWMS系统基础资料维护权限
        private string strWerks = "";
        private string strLgort = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData;
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
        DataTable dtImport = new DataTable();


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
        # endregion

        # region 构建式
        public Admin_PartNoDefine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;

            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //检查权限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        # endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        # region 布局尺寸调整
        private void Admin_PartNoDefine_Resize(object sender, EventArgs e)
        {
            //panel6.Height = 120;
            //panel5.Height = 120;
            ////panel6.Size = new System.Drawing.Size((int)(this.Size.Width * 0.4), panel5.Size.Height);
            ////if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
            //    stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;

        }
        # endregion

        # region 保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            DataTable dtTemp = new DataTable();
            try
            {

                //檢查是不是空值
                if (this.txtMatnr.Text.Trim() == "" || this.txtMaktx.Text.Trim() == "")
                {
                    stsWarning.Text = "Part No. and Description can't be empty!!";
                    return;
                }
                //檢查料號是否已存在
                if (objAdmin.CheckExistedPart(txtMatnr.Text.Trim()))
                {
                    stsWarning.Text = "Part No. is already existed!!";
                    return;
                }
                //新增
                if (objAdmin.AddPartNo(txtMatnr.Text.Trim(), this.txtMaktx.Text.Trim()))
                {
                    stsWarning.Text = "Add OK!";
                    this.txtMatnr.Enabled = false;
                    this.txtMaktx.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtMaktx.Text = "";
                this.txtMatnr.Text = "";
                this.txtMaktx.Enabled = true;
                this.txtMatnr.Enabled = true;
                txtFilePath.Text = "";
                btnBathImport.Enabled = false;
                btnExecute.Enabled = false;
                dgvData.DataSource = null;
                dtImport.Rows.Clear();
                btnSave.Enabled = true;


            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        #region Import
        private void btnBathImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            dtImport.Rows.Clear();
            string strExistedPart = "";
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
                    return;
                }
                # endregion
                stsWarning.Text = "Please don't close the window ,Check the data...";
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                //string strCmd = "select * from [Sheet1$]";
               // dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtData.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                    return;
                }
                #endregion
                DataColumn dcCrdat = new DataColumn();
                dcCrdat.ColumnName = "CRDAT";
                DataColumn dcModat = new DataColumn();
                dcModat.ColumnName = "MODAT";
                if (!dtData.Columns.Contains("CRDAT"))
                {
                    dtData.Columns.Add(dcCrdat);
                }
                if (!dtData.Columns.Contains("MODAT"))
                {
                    dtData.Columns.Add(dcModat);
                }
                dtImport = dtData.Clone();
                foreach (DataRow dr in dtData.Rows)
                {
                    //檢查是不是空值
                    if (string.IsNullOrEmpty(dr["PartNo"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["Description"].ToString().Trim()))
                    {
                        stsWarning.Text = dr["PartNo"].ToString() + "料号或料号描述有存在空值，请确认！！";
                        return;
                    }
                    else
                    {
                        if (objAdmin.CheckExistedPart(dr["PartNo"].ToString().Trim()))
                        {
                            objAdmin.DeletePartNo(dr["PartNo"].ToString().Trim(), UserData);//已存在先删除吧！！
                        }
                        dr["CRDAT"] = DateTime.Now.ToString();
                        dr["MODAT"] = DateTime.Now.ToString();
                        dtImport.ImportRow(dr);
                    }
                }
                if (dtImport.DefaultView.ToTable(true, "PartNo").Rows.Count < dtImport.Rows.Count)
                {
                    stsWarning.Text = "上传模板中有重复料号！";
                    return;
                }
                ShowDataGrid();
                stsWarning.Text = "Import OK!";
                btnBathImport.Enabled = false;
                btnExecute.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }


        }
        #endregion

        #region SampleLink
        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        #endregion

        #region ShowDataGird
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
                labDescription.Width = 300;
                dgvData.Columns.Add(labDescription);



                dgvData.DataSource = dtImport;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #endregion

        #region btnExecute

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (dtImport.Rows.Count > 0)
            {
                try
                {
                    if (objAdmin.InsertMatnrMore(dtImport))
                    {
                        stsWarning.Text = "Execute OK!";
                        return;
                    }
                }
                catch (Exception e1)
                {
                    stsWarning.Text = "btnBathImport_Click ==> Execute Fail" + e1.ToString();
                    return;
                }
            }
            else
            {
                stsWarning.Text = "No Data To Import!Please Check";
                return;
            }

        }

        #endregion

        #region btnFile
        private void btnFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
                btnBathImport.Enabled = true;
                btnExecute.Enabled = false;
            }
        }
        #endregion

        #region btnDelete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatnr.Text.Trim().ToString()))
            {
                stsWarning.Text = "请输入要删除的料号！！";
                return;
            }
            else
            {
                if (objAdmin.DeletePartNo(txtMatnr.Text.Trim().ToString(), UserData))
                {
                    stsWarning.Text = "Delete OK!";
                    return;
                }
                else
                {
                    stsWarning.Text = "Delete Fail!";
                    return;
                }
            }
        }

        #endregion


    }
}
