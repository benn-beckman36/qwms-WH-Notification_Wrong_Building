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
using System.Diagnostics;
using System.Collections;
using QCI_QWMS_Models;

namespace QWMS.Models
{
    public partial class Model_Manage_LogQuery : Form
    {
        #region     设置参数
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private StorageIn objStorageIn;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageOut objStorageOut;

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
        #endregion

        public Model_Manage_LogQuery(UserInfo _UserData, string strProgid)
        {
            InitializeComponent();
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                UserData = _UserData;
                objStorageIn = new StorageIn(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageOut = new StorageOut(UserData, Progid);

                //检查权限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //Status
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlCgcls();
                    ShowDdlStartEndHour();

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

        #region     Function
        
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }

        //厂区
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

        //仓别
        private void ShowDdlLgort()
        {
            try
            {
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

        //时间(H)
        private void ShowDdlStartEndHour()
        {
            try
            {
                for (int i = 1; i <= 24; i++)
                {
                    cmbStartHour.Items.Add(i.ToString());
                    cmbEndHour.Items.Add(i.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStartEndHour()");
            }
        }

        //功能
        private void ShowDdlCgcls()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbCgcls.Items.Clear();
                dtTemp = objPlantData.GetDdlCgcls();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbCgcls.Items.Add(dtTemp.Rows[i]["F_VALUE"].ToString() + "-" + dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlCgcls()");
            }
        }

        //GridView
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Trn-Date";
                crdatStyle.Width = 130;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);

                DataGridViewTextBoxColumn cgclsStyle = new DataGridViewTextBoxColumn();
                cgclsStyle.DataPropertyName = "CGCLS";
                cgclsStyle.HeaderText = "Log Type";
                cgclsStyle.ReadOnly = true;
                dgvData.Columns.Add(cgclsStyle);

                DataGridViewTextBoxColumn werksStyle = new DataGridViewTextBoxColumn();
                werksStyle.DataPropertyName = "WERKS";
                werksStyle.HeaderText = "Plant";
                werksStyle.ReadOnly = true;
                dgvData.Columns.Add(werksStyle);

                DataGridViewTextBoxColumn lgortStyle = new DataGridViewTextBoxColumn();
                lgortStyle.DataPropertyName = "LGORT";
                lgortStyle.HeaderText = "Storage";
                lgortStyle.ReadOnly = true;
                dgvData.Columns.Add(lgortStyle);

                DataGridViewTextBoxColumn locatStyle = new DataGridViewTextBoxColumn();
                locatStyle.DataPropertyName = "OLOCA";
                locatStyle.HeaderText = "Location";
                locatStyle.ReadOnly = true;
                dgvData.Columns.Add(locatStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Model No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Trn-Qty";
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                //DataGridViewTextBoxColumn indatStyle = new DataGridViewTextBoxColumn();
                //indatStyle.DataPropertyName = "INDAT";
                //indatStyle.HeaderText = "Store In Date";
                //indatStyle.ReadOnly = true;
                //dgvData.Columns.Add(indatStyle);
                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "TransferOrder";
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn crnamStyle = new DataGridViewTextBoxColumn();
                crnamStyle.DataPropertyName = "CRNAM";
                crnamStyle.HeaderText = "User ID";
                crnamStyle.ReadOnly = true;
                dgvData.Columns.Add(crnamStyle);

                dgvData.DataSource = dtData;
               
                lblCount.Text = dtData.Rows.Count.ToString() + " records";  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }

        //Download文档
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);

                #region     Reading data
                strLine = "Trn-Date\tLog Type\tPlant\tStorage\tLocation\tModel No\tModel Name\tTrn-Qty\tStore In Date\tUser ID";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CGCLS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["OLOCA"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ITEMNAME"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRNAM"].ToString();

                    sw.WriteLine(strLine);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }

        #endregion

        #region     Event

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            string strStartDate = "";
            string strEndDate = "";
            string strStartHour = "";
            string strEndHour = "";
            string strWerks = "";
            string strLgort = "";
            string strCgcls = "";
            stsWarning.Text = "";
            DataTable dtExcel = new DataTable();
            
            try
            {
                #region 查詢條件
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbCgcls.SelectedIndex != -1)
                {
                    string strCgclsTemp = cmbCgcls.Items[cmbCgcls.SelectedIndex].ToString();
                    strCgcls = strCgclsTemp.Substring(0, strCgclsTemp.IndexOf('-'));
                }
                if (cmbStartHour.SelectedIndex != -1)
                {
                    strStartHour = Convert.ToDecimal(cmbStartHour.Items[cmbStartHour.SelectedIndex]).ToString("00") + ":00:00";
                }
                else
                {
                    strStartHour = "00:00:00";
                }
                if (cmbEndHour.SelectedIndex != -1)
                {
                    strEndHour = Convert.ToDecimal(cmbEndHour.Items[cmbEndHour.SelectedIndex].ToString()).ToString("00") + ":00:00";
                }
                else
                {
                    strEndHour = "23:59:59";
                }

                strStartDate = dtpStartDate.Value.ToString("yyyyMMdd") + " " + strStartHour;
                strEndDate = dtpEndDate.Value.ToString("yyyyMMdd") + " " + strEndHour;

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                string strQueryTable = "WHLOG";
                if (chkOldData.Checked)
                {
                    strQueryTable = "WHLOG_BAK";
                }
                #endregion

                # region 校验格式
                string strFileName = this.txtFilePath.Text.Trim();
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                }
                # endregion

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
               // string strCmd = "select * from [Sheet1$]";
               // dtExcel = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtExcel = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtExcel.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                }
                #endregion

                //DataRow[] temp = dtExcel.Select(" isnull(ModelNo,'')<>'' ");

                DataTable dtImport = new DataTable();
                dtImport = dtExcel.Clone();


                //foreach (DataRow dataRow in temp)
                //{
                //    dtImport.ImportRow(dataRow);
                //}


                foreach (DataRow dr in dtExcel.Rows)
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
                dtData = objModelsData.QueryLogData(strCgcls, txtCrnam.Text.Trim(), strStartDate, strEndDate, dtImport, txtStartLocat.Text.Trim(), txtEndLocat.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), strQueryTable);

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid();
                    return;
                }
                else
                {
                    //將datatable依 时间,异动Type,储位做排序
                    IEnumerable<DataRow> results = (from row in dtData.AsEnumerable()
                                                    orderby row["CRDAT"], row["CGCLS"], row["OLOCA"]
                                                    select row);

                    dtData = results.CopyToDataTable<DataRow>();
                    btnPrint.Enabled = true;
                    ShowDataGrid();
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.cmbCgcls.SelectedIndex = -1;
                this.cmbStartHour.SelectedIndex = -1;
                this.cmbEndHour.SelectedIndex = -1;
                this.txtStartLocat.Text = "";
                this.txtEndLocat.Text = "";
                this.txtStartMblnr.Text = "";
                this.txtEndMblnr.Text = "";
                this.txtCrnam.Text = "";
                this.dtpStartDate.Value = DateTime.Now;
                this.dtpEndDate.Value = DateTime.Now;
                this.dgvData.DataSource = null;
                this.btnPrint.Enabled = false;
                this.dtData.Rows.Clear();
                this.txtFilePath.Text = "";
                stsWarning.Text = "";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                ReportPrint objReportPrint;
                objReportPrint = new ReportPrint(UserData, "LOG", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnPrint_Click()");
            }
        }

        private void btnDownload_Click(object sender, System.EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
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

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
            }
        }

        #endregion
   
    }
}
