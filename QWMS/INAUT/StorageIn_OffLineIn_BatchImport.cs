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
using System.Text.RegularExpressions;
using System.Configuration;

namespace QWMS
{
    public partial class StorageIn_OffLineIn_BatchImport : Form
    {
        public string MANTR { get; set; }
        public string MAKTX { get; set; }
        public string Progid { get; set; }
        private Admin objAdmin;
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private string strType = "";
        bool H95_flag = false;

        private Counting objCounting;

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
        private DataTable dtImport = new DataTable();

        private DataTable dtData = new DataTable();

        UserInfo UserData = new UserInfo();

        public StorageIn_OffLineIn_BatchImport()
        {
            InitializeComponent();
            btnExecute.Enabled = false;
            btnImport.Enabled = false;
        }

        # region 构建式
        public StorageIn_OffLineIn_BatchImport(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat)
        {
            InitializeComponent();
            UserData = varUserData;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
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
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "离线Sample.xlsx";
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
                //if (Werks == "CQ3B" && Lgort == "RJ11" || Lgort == "RJ21" || Lgort == "TW70" || Lgort == "TW80" || Lgort == "TWCP" || Lgort == "TWEP")//華為料號
                //{
                //    H95_flag = true;
                //}
                # region 校验格式
                string strFileName = this.txtFilePath.Text;
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                    return ;
                }
                # endregion

                stsWarning.Text = "Please don't close the window ,Check the data...";
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                //string strCmd = "select * from [Sheet1$]";
                //dtData = objExcel.ExcelQuery(this.txtFilePath.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtData.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                    return ;
                }
                #endregion
                dtData.Columns.Add("MANDT", Type.GetType());
                dtData.Columns.Add("COMCD", Type.GetType());
                dtData.Columns.Add("WERKS", Type.GetType());
                dtData.Columns.Add("LGORT", Type.GetType());
                dtData.Columns.Add("LOCAT", Type.GetType());
                dtData.Columns["Part No"].ColumnName = "MATNR";
                dtData.Columns["Stock"].ColumnName = "INSMK";
                dtData.Columns["Version"].ColumnName = "CHARG";
                //dtData.Columns.Add("CHARG", Type.GetType());
                dtData.Columns.Add("MENGE", Type.GetType());
                dtData.Columns["Qty"].ColumnName = "ALQTY";
                dtData.Columns.Add("MBLNR", Type.GetType());
                dtData.Columns.Add("ZEILE", Type.GetType());
                dtData.Columns.Add("EBELN", Type.GetType());
                dtData.Columns["Vendor"].ColumnName = "LIFNR";
                dtData.Columns.Add("OMBLNR", Type.GetType());
                dtData.Columns.Add("MRGID", Type.GetType());
                dtData.Columns.Add("KOSTL", Type.GetType());
                dtData.Columns.Add("ARBPL", Type.GetType());
                dtData.Columns.Add("TRNTP", Type.GetType());
                dtData.Columns.Add("RMANO", Type.GetType());
                dtData.Columns.Add("RMAK1", Type.GetType());
                //dtData.Columns.Add("DACOD", Type.GetType());
                dtData.Columns["DateCode"].ColumnName = "DACOD";
                dtData.Columns["Store In Date(For FIFO)"].ColumnName = "INDAT";
                dtData.Columns.Add("VEDAT", Type.GetType());           
                dtImport = dtData.Clone();


                //foreach (DataRow dataRow in temp)
                //{
                //    dtImport.ImportRow(dataRow);
                //}

                //判断是否是管控仓别+是管控仓添加转换
                #region 检查是否为 DateCode 仓别 ，若是需要维护DateCode
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                if (objStorageData.CheckStorageInType(strWerks, strLgort, "Diff DACOD Diff Locat"))
                {
                    string strDacod = "";
                    string strVedat = "";
                    string strLifnr = "";
                    DataRow[] dtdccode = dtData.Select(" isnull(MATNR,'')<>'' ");
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strDacod = dtData.Rows[i]["DACOD"].ToString().Trim();
                        strVedat = dtData.Rows[i]["VEDAT"].ToString().Trim();
                        strLifnr = dtData.Rows[i]["LIFNR"].ToString().Trim();
                        if (strDacod == "" || strLifnr == "")
                        {
                            MessageBox.Show("DateCode can't be empty");
                            return;
                        }
                        else
                        {
                            #region Datecode匹配
                            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                  CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                            strVedat = objCounting.WHDCR_Query(strDacod, strLifnr);
                            if (strVedat == "")
                            {
                                string strTemp = objCounting.getDCTrans(strLifnr, strDacod).ToString();
                                if (!string.IsNullOrEmpty(strTemp))
                                {                                
                                    DataTable dtNewDateCode = new DataTable();
                                    dtNewDateCode.Columns.Add("LIFNR");
                                    dtNewDateCode.Columns.Add("DC_Before");
                                    dtNewDateCode.Columns.Add("DC_After");

                                    DataRow drnr = dtNewDateCode.NewRow();
                                    drnr["LIFNR"] = strLifnr;
                                    drnr["DC_Before"] = strDacod;
                                    drnr["DC_After"] = strTemp;
                                    dtNewDateCode.Rows.Add(drnr.ItemArray);
                                    string strTransType = "NEW";
                                    objCounting.WHDCR_DML(dtNewDateCode, strTransType);
                                    strVedat = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                    dtData.Rows[i]["VEDAT"] = strVedat;
                                    //把数据交给数据表
                                }
                                else
                                {
                                    MessageBox.Show("请先维护"+ strDacod + "的D/C转换规则");
                                    return;
                                }
                            }
                            else
                            {
                                //把数据交给数据表
                                DateTime dtVedat = Convert.ToDateTime(strVedat);
                                strVedat = dtVedat.ToString("yyyyMMdd");
                                dtData.Rows[i]["VEDAT"] = strVedat;
                            }
                            #endregion
                        }
                    }              
                }
                #endregion



                DataRow[] temp = dtData.Select(" isnull(MATNR,'')<>'' ");
                foreach (DataRow dr in temp)
                {
                    //檢查是不是空值
                    if (!string.IsNullOrEmpty(dr["MATNR"].ToString().Trim()))
                    {
                        //stsWarning.Text = "ModelNo. can't be empty!!";
                        //return;
                        dr["MANDT"] = UserData.Client;
                        dr["WERKS"] = Werks;
                        dr["LGORT"] = Lgort;
                        dr["LOCAT"] = Locat;
                        //dr["CHARG"] = "";
                        dr["MENGE"] = "0";
                        dr["MBLNR"] = "";
                        dr["ZEILE"] = "";
                        dr["EBELN"] = "";
                        dr["OMBLNR"] = "";
                        dr["MRGID"] = "";
                        dr["KOSTL"] = "";
                        dr["ARBPL"] = "";
                        dr["TRNTP"] = "";
                        dr["RMAK1"] = "";
                        dr["COMCD"] = UserData.CompanyCode;
                        dr["RMANO"] = "";
                        //dr["DateCode"] = "";
                        dtImport.ImportRow(dr);

                    }
                    else
                    {
                        MessageBox.Show("Part No can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {
                    //檢查料號是否存在
                    if (!objPlantData.CheckExistedMatnr(dtImport.Rows[i]["MATNR"].ToString().Trim()))
                    {
                        MessageBox.Show(dtImport.Rows[i]["MATNR"].ToString().Trim() + " doesn't exist!!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (i == dtImport.Rows.Count - 1)
                        {
                            return;
                        }
                    }
                    //庫別不可空白
                    if (string.IsNullOrEmpty(dtImport.Rows[i]["INSMK"].ToString().Trim()))
                    {
                        MessageBox.Show("Stock can't be empty!!");
                        return;
                    }
                    //數量不可空白或0
                    if (!CheckIsNumber(dtImport.Rows[i]["ALQTY"].ToString().Trim()) || dtImport.Rows[i]["ALQTY"].ToString().Trim() == "0")
                    {
                        MessageBox.Show("Store in Qty should be numeric and greater than 0!!");
                        return;
                    }
                    if (dtImport.DefaultView.ToTable(true, "MATNR").Rows.Count < dtImport.Rows.Count)
                    {
                        MessageBox.Show("Duplicate data!!");
                        return;
                    }
                    //if (H95_flag) //華為料號管控DateCode
                    //{
                    //    if (dtImport.Rows[i]["DateCode"].ToString().Trim().Equals(DateTime.Now.ToString("yyyyMMdd")))
                    //    {
                    //        MessageBox.Show("请注意选择DateCode!!");
                    //        return;
                    //    }
                    //}

                    #region PCB料号 版本不能为空
                    if (objPlantData.CheckCHARGLGORT(Werks))
                    {
                        if (dtImport.Rows[i]["MATNR"].ToString().Substring(0, 2) == "SA" || dtImport.Rows[i]["MATNR"].ToString().Substring(0, 2) == "DA")
                        {
                            if (string.IsNullOrEmpty(dtImport.Rows[i]["CHARG"].ToString().Trim()))
                            {
                                MessageBox.Show("Part No(PCB) can't be empty!!");
                                return;
                            }
                        }
                    }
                    #endregion

                    #region PCB不同版本不允许入库 

                    DataTable dtLocMatCar = new DataTable();
                    if (objPlantData.CheckCHARGLGORT(Werks))
                    {
                        if (objStorageData.CheckExistedSameLocation(strLocat))
                        {
                            //判断是否为PCB材料
                            if (dtImport.Rows[i]["MATNR"].ToString().Substring(0, 2) == "SA" || dtImport.Rows[i]["MATNR"].ToString().Substring(0, 2) == "DA")
                            {
                                string strMatnr = dtImport.Rows[i]["MATNR"].ToString();
                                //string strVendor = dtImport.Rows[i]["LIFNR"].ToString();
                                string strCharg = dtImport.Rows[i]["CHARG"].ToString();
                                if (objStorageData.CheckExistedDifferentCHARG(strLocat, strMatnr, strCharg))
                                {
                                    stsWarning.Text = strMatnr + "  with different version can't be stored in the same location!!";
                                    return;
                                }
                            }

                        }
                    }
                    #endregion
                }

                if (dtImport.Rows.Count > 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "Import OK!";
                    btnImport.Enabled = false;
                    btnExecute.Enabled = true;
                }

                if (dtImport.Rows.Count == 0)
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

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 90;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcPartNo = new DataGridViewTextBoxColumn();
                dgvcPartNo.DataPropertyName = "MATNR";
                dgvcPartNo.HeaderText = "Part No";
                dgvcPartNo.Width = 120;
                dgvcPartNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPartNo);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "Version";
                dgvcVersion.Width = 120;
                dgvcVersion.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcStock = new DataGridViewTextBoxColumn();
                dgvcStock.DataPropertyName = "INSMK";
                dgvcStock.HeaderText = "Stock";
                dgvcStock.Width = 90;
                dgvcStock.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcStock);

                DataGridViewTextBoxColumn dgvcStoreInDate = new DataGridViewTextBoxColumn();
                dgvcStoreInDate.DataPropertyName = "INDAT";
                dgvcStoreInDate.HeaderText = "Store In Date(For FIFO)";
                dgvcStoreInDate.Width = 90;
                dgvcStoreInDate.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcStoreInDate);

                DataGridViewTextBoxColumn dgvcQty = new DataGridViewTextBoxColumn();
                dgvcQty.DataPropertyName = "ALQTY";
                dgvcQty.HeaderText = "Qty";
                dgvcQty.Width = 90;
                dgvcQty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQty);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "Date Code";
                dgvcDacod.Width = 90;
                dgvcDacod.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "Vendor Date Code";
                dgvcVedat.Width = 90;
                dgvcVedat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcVedat);

                dgvData.DataSource = dtImport;


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
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                if (dtImport.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    return;
                }

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);


                for (int i = 0; i < dtImport.Rows.Count; i++)
                {
                    if (strTempMblnrMatnr.IndexOf(dtImport.Rows[i]["LOCAT"].ToString() + dtImport.Rows[i]["MATNR"].ToString() + dtImport.Rows[i]["INSMK"].ToString() + ";") == -1)
                    {
                        strTempMblnrMatnr += dtImport.Rows[i]["LOCAT"].ToString() + dtImport.Rows[i]["MATNR"].ToString() + dtImport.Rows[i]["INSMK"].ToString() + ";";
                    }
                    else
                    {
                        MessageBox.Show("The data you input is duplicate in the location!!");
                        return;
                    }
                }

              

                if (objStorageIn.AddOffLineInData(Locat, "", dtImport))
                {
                    stsWarning.Text = "Add OK!!";
                    this.btnExecute.Enabled = false;
                    return;
                }
                else
                {
                    stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                    this.btnExecute.Enabled = true;
                    return;
                }




            }
            catch (Exception ex)
            {

                stsWarning.Text = ex.Message;
                return;
            }
        }

        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
