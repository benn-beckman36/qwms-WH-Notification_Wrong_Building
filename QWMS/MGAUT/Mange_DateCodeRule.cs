using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;
using System.Diagnostics;
using NPOI;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;
using System.Text.RegularExpressions;


namespace QWMS
{
    public partial class Mange_DateCodeRule : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strStset = "";
        private string strType = "";
        private Admin objAdmin;
        private StorageIn objStorageIn;        
        private PlantData objPlantData;
        private StorageData objStorageData;
        string[] name = { "Vendor", "DateCode", "DateCode(After Transfer)", "USNAM", "CRDAT", "MODAT", "COMCD" };
        //string[] name_temp = { "ID","Vendor", "DateCode", "DateCode(After Transfer)" };

        private DataTable dtData = new DataTable();
        private DataTable dtData_temp = new DataTable();
                    
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
        public string Stset
        {
            get
            {
                return strStset;
            }
            set
            {
                strStset = value;
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

        #region 构造函数
        public Mange_DateCodeRule(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
            
            try
            {
                //dtdata schema
                for (int i = 0; i < name.Length; i++)
                    dtData.Columns.Add(new DataColumn(i.ToString(), typeof(string)));

                //dtdata_temp schema
                //for (int i = 0; i < name_temp.Length; i++)
                    //dtData_temp.Columns.Add(new DataColumn(i.ToString(), typeof(string)));

                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objStorageIn = new StorageIn(UserData, Progid);
                if (!objStorageIn.CheckDatecode(Usrnm))
                {
                    throw new Exception("You don't have right to use this program!!");
                }


                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else 
                {  
                    //初始化控制項
                    this.btnRefresh_Click(null, null);

                    //秀出Status的資料
                    ShowStatusData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示状态栏

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        #region RadioButton

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.txtBatch.Enabled = false;
            this.btnUpload.Enabled = false;
            this.link1.Enabled = false;
            this.label4.Enabled = false;
            this.strType = "ADD";

            ShowDataGridView();
            AutoComplete_Content();           
        }

        private void rdoModify_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.txtBatch.Enabled = false;
            this.btnUpload.Enabled = false;
            this.link1.Enabled = false;
            this.label4.Enabled = false;
            this.strType = "MODIFY";

            //秀出datagridview
            dtData_temp = objStorageData.QueryDateCode();

            //copy dtData
            for (int i = 0; i < dtData_temp.Rows.Count; i++)
            {
                object[] rowArray = new object[7];
                DataRow Temp = dtData.NewRow();
                rowArray[0] = dtData_temp.Rows[i][3].ToString();
                rowArray[1] = dtData_temp.Rows[i][1].ToString();
                rowArray[2] = dtData_temp.Rows[i][2].ToString();
                rowArray[3] = dtData_temp.Rows[i][4].ToString();
                rowArray[4] = dtData_temp.Rows[i][5].ToString();
                rowArray[5] = dtData_temp.Rows[i][6].ToString();
                rowArray[6] = dtData_temp.Rows[i][7].ToString();
                Temp.ItemArray = rowArray;
                dtData.Rows.Add(Temp);
            }

            ShowDataGridView();
            gvData.DataSource = dtData;
            AutoComplete_Content();
        }

        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.txtBatch.Enabled = false;
            this.btnUpload.Enabled = false;
            this.link1.Enabled = false;
            this.label4.Enabled = false;
            this.strType = "DELETE";

            //秀出datagridview
            dtData_temp = objStorageData.QueryDateCode();

            //copy dtData
            for (int i = 0; i < dtData_temp.Rows.Count; i++)
            {
                object[] rowArray = new object[7];
                DataRow Temp = dtData.NewRow();
                rowArray[0] = dtData_temp.Rows[i][3].ToString();
                rowArray[1] = dtData_temp.Rows[i][1].ToString();
                rowArray[2] = dtData_temp.Rows[i][2].ToString();
                rowArray[3] = dtData_temp.Rows[i][4].ToString();
                rowArray[4] = dtData_temp.Rows[i][5].ToString();
                rowArray[5] = dtData_temp.Rows[i][6].ToString();
                rowArray[6] = dtData_temp.Rows[i][7].ToString();

                Temp.ItemArray = rowArray;
                dtData.Rows.Add(Temp);
            }

            ShowDataGridView();
            gvData.DataSource = dtData;
            AutoComplete_Content();
        }

        private void rdoBatch_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.txtVendor.Enabled = false;
            this.txtDatecode.Enabled = false;
            this.txtDatecodeAfter.Enabled = false;
            this.label1.Enabled = false;
            this.label2.Enabled = false;
            this.label3.Enabled = false;
            this.strType = "BATCH";

            ShowDataGridView();
        }

        #endregion

        #region Button

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";

                #region ADD

                if (rdoAdd.Checked == true)
                {
                    objStorageData.WHDCR_DML(dtData, Type);
                    stsWarning.Text = "ADD OK!";
                }

                #endregion

                #region UPDATE

                if (rdoModify.Checked == true)
                {
                    DataTable dtData_upd = new DataTable();

                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        dtData_upd.Columns.Add();
                        dtData_upd.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
                    }
                        object[] rowArray = new object[3];
                        DataRow Temp = dtData_upd.NewRow();
                        rowArray[0] = txtVendor.Text.Trim();
                        rowArray[1] = txtDatecode.Text.Trim();
                        rowArray[2] = txtDatecodeAfter.Text.Trim();
                        Temp.ItemArray = rowArray;
                        dtData_upd.Rows.Add(Temp);
                    
                    for (int i = 0; i < dtData_upd.Rows.Count; i++)
                    {
                        string strDC_After = dtData_upd.Rows[i][2].ToString();

                        if (!ClaCommon.CheckDateValid(strDC_After, "yyyy/MM/dd"))
                        {
                            MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strDC_After));
                            return;
                        }
                    }

                    objStorageData.WHDCR_DML(dtData_upd, Type);
                    stsWarning.Text = "UPDATE OK!";
                }  
               

                #endregion

                #region DELETE

                if (rdoDelete.Checked == true)
                {
                    //gvdata to dtData_del
                    DataTable dtData_del = new DataTable();

                    for (int i = 0; i < dtData.Columns.Count; i++)
                    {
                        dtData_del.Columns.Add();
                        dtData_del.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
                    }

                    for (int i = 0; i < gvData.Rows.Count; i++)
                    {
                        object[] rowArray = new object[7];
                        DataRow Temp = dtData_del.NewRow();
                        rowArray[0] = dtData_temp.Rows[i][3].ToString();
                        rowArray[1] = dtData_temp.Rows[i][1].ToString();
                        rowArray[2] = dtData_temp.Rows[i][2].ToString();
                        rowArray[3] = dtData_temp.Rows[i][4].ToString();
                        rowArray[4] = dtData_temp.Rows[i][5].ToString();
                        rowArray[5] = dtData_temp.Rows[i][6].ToString();
                        rowArray[6] = dtData_temp.Rows[i][7].ToString();
                        Temp.ItemArray = rowArray;
                        dtData_del.Rows.Add(Temp);
                    }

                    objStorageData.WHDCR_DML(dtData_del, Type);
                    stsWarning.Text = "DELETE OK!";
                }

                #endregion

                #region BATCH

                if (rdoBatch.Checked == true)
                {
                    #region 批量复制数据到数据库
                    ArrayList arrColumns = new ArrayList();//要匹配的列
                    arrColumns.Add("LIFNR");
                    arrColumns.Add("DC_Before");
                    arrColumns.Add("DC_After");
                    arrColumns.Add("USNAM");
                    arrColumns.Add("CRDAT");
                    arrColumns.Add("MODAT");
                    arrColumns.Add("COMCD");

                    bool flg = objStorageData.BulkInsertWHDCR("WHDCR", arrColumns, dtData);//复制数据到数据库

                    #endregion
                    if (flg)
                    {
                        stsWarning.Text = "BATCH UPLOAD OK!";
                    }
                    else
                    {
                        stsWarning.Text = "BATCH UPLOAD Fail!";
                    }
                    //objStorageData.BulkInsertWHDCR(dtData, Type);

                }

                #endregion

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";

                #region ADD

                if (rdoAdd.Checked == true)
                {
                    //check
                    if (!string.IsNullOrEmpty(txtDatecode.Text) && !string.IsNullOrEmpty(txtDatecodeAfter.Text) && !string.IsNullOrEmpty(txtVendor.Text))
                    {
                        if (objStorageData.QueryDateCode2(txtDatecode.Text.Trim(), txtVendor.Text.Trim()) == 0)
                        {
                            if (txtDatecodeAfter_check(txtDatecodeAfter.Text.Trim()))
                            {
                                object[] rowArray = new object[7];
                                DataRow Temp = dtData.NewRow();
                                rowArray[0] = txtVendor.Text.ToUpper().Trim();
                                rowArray[1] = txtDatecode.Text.Trim();
                                rowArray[2] = txtDatecodeAfter.Text.Trim();
                                rowArray[3] = UserData.UserId;
                                rowArray[4] = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                                rowArray[5] = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                                rowArray[6] = Comcd;
                                Temp.ItemArray = rowArray;
                                dtData.Rows.Add(Temp);
                                btnSave.Enabled = true;
                                gvData.DataSource = dtData;
                            }
                            else
                                MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", txtDatecodeAfter.Text.Trim()));
                        }
                        else
                            MessageBox.Show("DateCode rule already exists!!");
                    }                        
                    else
                        MessageBox.Show("DateCode or DateCode(After Transfer) or Vedor can't be empty!!");
                }

                #endregion

                #region UPDATE

                if (rdoModify.Checked == true)
                {
                    //check
                    if (gvData.Rows.Count != 0)
                    {
                        label1.Enabled = false;
                        label2.Enabled = false;
                        label3.Enabled = false;
                        txtVendor.Enabled = false;
                        txtDatecode.Enabled = false;
                        txtDatecodeAfter.Enabled = false;
                        btnConfirm.Enabled = false;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No data!!");
                    }       
                }

                #endregion

                #region DELETE

                if (rdoDelete.Checked == true)
                {
                    string DateCode = txtDatecode.Text.Trim();
                    string Vendor = txtVendor.Text.Trim();
                    string DateAfter = txtDatecodeAfter.Text.Trim();
                    dtData = objStorageData.QueryDateRule(Vendor, DateCode, DateAfter);
                    
                    //check
                    if (dtData.Rows.Count != 0)
                    {
                        label1.Enabled = false;
                        label2.Enabled = false;
                        label3.Enabled = false;
                        txtVendor.Enabled = false;
                        txtDatecode.Enabled = false;
                        txtDatecodeAfter.Enabled = false;
                        btnConfirm.Enabled = false;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No data!!");
                    }
                    gvData.DataSource = dtData;
                    AutoComplete_Content();
                }

                #endregion

                #region BATCH

                if (rdoBatch.Checked == true)
                {
                    //抓出檔案路徑及file extension
                    string path = txtBatch.Text.Trim();
                    string file_extension = SplitExtension(path);

                    //檢查上傳檔案是否為excel檔
                    if (file_extension.Equals(".xls", StringComparison.CurrentCultureIgnoreCase) || file_extension.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.Cursor = Cursors.WaitCursor;
                        System.Data.DataTable Excel_Data = new DataTable();

                        //將excel轉換成datatable
                        Excel_Data = RenderDataTableFromExcel(path, 0, 0, file_extension);

                        //excel檔案檢查 and showgridview
                        CheckExcel(Excel_Data);

                        this.Cursor = Cursors.Default;
                    }
                    else
                        MessageBox.Show("Please Import Excel Format File!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (gvData.SelectedRows.Count == 1)
            {
                Mange_DateCodeRule_Pop objMange_DateCodeRule_Pop = new Mange_DateCodeRule_Pop(UserData, Progid, dtData, Type);
                objMange_DateCodeRule_Pop.ShowDialog();
                dtData = objMange_DateCodeRule_Pop.dtData_Pop;
                ShowDataGridView();
            }
            else
            {
                if (gvData.SelectedRows.Count > 1)
                    MessageBox.Show("Only one row can be selected!!");
                else if (gvData.SelectedRows.Count == 0)
                    MessageBox.Show("Please select the row you want to update first!!");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.rdoDelete.Checked = false;
                this.rdoAdd.Checked = false;
                this.rdoModify.Checked = false;
                this.rdoBatch.Checked = false;
                this.gbFunction.Enabled = true;
                this.gbMiddle.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnConfirm.Enabled = true;
                this.txtVendor.Text = "";
                this.txtDatecode.Text = "";
                this.txtDatecodeAfter.Text = "";
                this.txtBatch.Text = "";
                this.stsWarning.Text = "";
                this.txtVendor.Enabled = true;
                this.txtDatecode.Enabled = true;
                this.txtDatecodeAfter.Enabled = true;
                this.txtBatch.Enabled = true;
                this.btnUpload.Enabled = true;
                this.link1.Enabled = true;
                this.label1.Enabled = true;
                this.label2.Enabled = true;
                this.label3.Enabled = true;
                this.label4.Enabled = true;

                dtData.Clear();
                dtData_temp.Clear();
                gvData.DataSource = null;

                strType = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBatch.Text = openFileDialog1.FileName.ToString();
            }
        }

        #endregion

        #region textbox

        private void txtVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type == "MODIFY" || Type == "DELETE")
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (txtVendor.Text.Trim().ToString().Equals(""))//輸入為空值
                        gvData.DataSource = dtData;
                    else
                    {
                        Vendor_Filter();
                        AutoComplete_Content();
                    }

                }
            }
        }

        private void txtDatecode_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type == "MODIFY" || Type == "DELETE")
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (txtDatecode.Text.Trim().ToString().Equals(""))//輸入為空值
                        gvData.DataSource = dtData;
                    else
                    {
                        DateCode_Filter();
                        AutoComplete_Content();
                    }

                }
            }
        }

        private void txtDatecodeAfter_KeyDown(object sender, KeyEventArgs e)
        {
            if (Type == "MODIFY" || Type == "DELETE")
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (txtDatecodeAfter.Text.Trim().ToString().Equals(""))//輸入為空值
                        gvData.DataSource = dtData;
                    else
                    {
                        DateCodeAfter_Filter();
                        AutoComplete_Content();
                    }

                }
            }
        }

        private bool txtDatecodeAfter_check(string DatecodeAfter)
        {
            bool flag = false;

            if (ClaCommon.CheckDateValid(DatecodeAfter, "yyyy/MM/dd"))
            {
                DateTime dVedat;
                if (DateTime.TryParse(DatecodeAfter, out dVedat))
                {
                    TimeSpan diff = DateTime.Now - dVedat;
                    if (diff.TotalDays >= 0 && diff.TotalDays <= 6 * 365)
            {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        #endregion

        #region LinkButton

        private void link1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "DateCode.xls";
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

        #region ShowDataGridView

        private void ShowDataGridView()
        {
            try
            {
                if (gvData.Columns.Count != 0)
                    gvData.Columns.Clear();


                #region 设置DataGridView的欄名

                DataGridViewTextBoxColumn aColumnTextColumn;

                for (int i = 1; i <= name.Length; i++)
                {
                    aColumnTextColumn = new DataGridViewTextBoxColumn();
                    aColumnTextColumn.HeaderText = name[i - 1];
                    aColumnTextColumn.DataPropertyName = (i - 1).ToString();
                    aColumnTextColumn.Width = 150;
                    aColumnTextColumn.ReadOnly = false;
                    aColumnTextColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.gvData.Columns.Add(aColumnTextColumn);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
            
        }

        #endregion

        #region 调整布局大小

        private void Admin_StorageDefine_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }

        #endregion        

        #region excel檔案處理

        //Data<-->Excel(NPOI)
        private DataTable RenderDataTableFromExcel(string fileName, int SheetIndex, int HeaderRowIndex, string file_extension)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            if (file_extension.Equals(".xls"))
            {
                #region xls excel版本

                using (FileStream ExcelFileStream = new FileStream(fileName, FileMode.Open))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
                    HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);
                    HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                    //int cellCount = headerRow.LastCellNum;

                    int cellCount = 0;
                    //找出報表中最長的列長
                    for (int i = 0; i < sheet.LastRowNum; i++)
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);

                        if (row != null)
                        {
                            if (row.LastCellNum > cellCount)
                                cellCount = row.LastCellNum;
                            else
                                continue;
                        }
                        else
                            continue;
                    }

                    for (
                        int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        //DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        DataColumn column = new DataColumn();
                        table.Columns.Add(column);
                        table.Columns[i].ColumnName = headerRow.Cells[i].ToString();
                    }

                    int rowCount = sheet.LastRowNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    dataRow[j] = row.GetCell(j).ToString();

                                    if (row.GetCell(j).CellType == NPOI.SS.UserModel.CellType.FORMULA)//為公式值
                                    {
                                        HSSFCell cell = (HSSFCell)e.EvaluateInCell(row.GetCell(j));
                                        dataRow[j] = cell.ToString();
                                    }
                                    else//不為公式值
                                    {
                                        //if (j == 2)
                                        //{
                                            //dataRow[j] = row.GetCell(j).DateCellValue.ToString("yyyy/MM/dd");
                                        //}
                                        //else
                                            dataRow[j] = row.GetCell(j).ToString();                                        
                                    }
                                }
                            }
                        }
                        else
                            dataRow[0] = "";

                        table.Rows.Add(dataRow);
                    }

                    ExcelFileStream.Close();
                    workbook = null;
                    sheet = null;
                }

                #endregion
            }
            else if (file_extension.Equals(".xlsx"))
            {
                #region xlsx excel版本

                DataTable dtTData = new DataTable();

                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                //string strCmd = "select * from [Sheet1$]";
               // dtTData = objExcel.ExcelQuery(fileName, strCmd);
                # region 获取EXCEL数据
                dtTData = objExcel.GetDataTableFromExcel(fileName, true);
                if (dtTData.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                }
                #endregion
                DataColumn dcVendor = new DataColumn();
                dcVendor.ColumnName = "Vendor";
                DataColumn dcDateCode = new DataColumn();
                dcDateCode.ColumnName = "DateCode";
                DataColumn dcDateAfter = new DataColumn();
                dcDateAfter.ColumnName = "DataCode(After Transfer)";
                if (!dtTData.Columns.Contains("Vendor"))
                {
                    dtTData.Columns.Add(dcVendor);
                }
                if (!dtTData.Columns.Contains("DateCode"))
                {
                    dtTData.Columns.Add(dcDateCode);
                }
                if (!dtTData.Columns.Contains("DateCode(After Transfer)"))
                {
                    dtTData.Columns.Add(dcDateAfter);
                }
                
                table = dtTData;
                #endregion
            }

            return table;
        }

        //找出檔案路徑的附檔名
        private string SplitExtension(string path)
        {
            string file_extension = "";

            for (int i = path.Length - 1; i > 0; i--)
            {
                if (path.Substring(i, 1).Equals("."))
                {
                    file_extension = path.Substring(i, path.Length - i);
                    break;
                }
            }

            return file_extension;
        }

        //Excel檔案檢查
        private void CheckExcel(DataTable Excel_Data)
        {
            //(檢查順序分3大區塊 : Excel版本-->Excel資料正確性-->Excel資料重複)
            string message = "";
            bool flag_version, flag_null_D, flag_null_DA;
            flag_version  = flag_null_D = flag_null_DA = true;
            string Vendor, DateCode, DateCode_A;
            Vendor = DateCode = DateCode_A = "";

            #region 檢查Excel版本(By 欄位數,及欄位名稱)

            flag_version = true;
            string[] a1 = { "Vendor", "DateCode", "DateCode(After Transfer)" };//正確欄位名稱

            if (Excel_Data.Columns.Count == a1.Length)//檢查欄位數量
            {
                //檢查欄位名稱
                for (int i = 0; i < a1.Length; i++)
                {
                    //檢查到不符合的欄位名稱
                    if (!Excel_Data.Columns[i].ColumnName.ToString().Equals(a1[i].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag_version = false;
                        message = "Excel Version Incorrect!(column name)";
                        break;
                    }
                }
            }
            else
            {
                flag_version = false;
                message = "Excel Version Incorrect!(column number)";
            }

            #endregion

            #region 檢查Excel資料正確性

            if (flag_version)//Excel版本正確
            {
                ArrayList dataFormat = new ArrayList();//紀錄資料錯誤的種類({RowIndex,是否空值})

                for (int i = 0; i < Excel_Data.Rows.Count; i++)
                {
                    #region 檢查欄位是否都有值(DateCode,DateCode(After Transfer))

                    Vendor = DateCode = DateCode_A = "";
                    flag_null_D = flag_null_DA = true;
                    if (!string.IsNullOrEmpty(Excel_Data.Rows[i]["Vendor"].ToString()))
                    {
                            Vendor = Excel_Data.Rows[i]["Vendor"].ToString();
                    }
                    else
                        flag_null_D = false;

                    if (Excel_Data.Rows[i]["DateCode"] != null)
                    {
                        if (!Excel_Data.Rows[i]["DateCode"].ToString().Trim().Equals(""))
                        {
                            if (objStorageData.QueryDateCode2(Excel_Data.Rows[i]["DateCode"].ToString(),Vendor) == 0)
                            {
                                DateCode = Excel_Data.Rows[i]["DateCode"].ToString();
                            }
                            else
                                flag_null_D = false;
                        }                            
                        else
                            flag_null_D = false;
                    }
                    else
                        flag_null_D = false;

                    if (Excel_Data.Rows[i]["DateCode(After Transfer)"] != null)
                    {
                        if (!Excel_Data.Rows[i]["DateCode(After Transfer)"].ToString().Trim().Equals(""))
                        {
                            if (txtDatecodeAfter_check(Excel_Data.Rows[i]["DateCode(After Transfer)"].ToString().Trim()))
                            {
                                DateCode_A = Excel_Data.Rows[i]["DateCode(After Transfer)"].ToString();
                            }
                            else
                                flag_null_DA = false;
                        }
                            
                        else
                            flag_null_DA = false;
                    }
                    else
                        flag_null_DA = false;

                    #endregion

                    if (!flag_null_D || !flag_null_DA)
                    {
                        ArrayList temp = new ArrayList { (i + 2).ToString(), "N" };
                        dataFormat.Add(temp);
                    }
                }

                if (dataFormat.Count != 0)//Excel資料正確性檢查未通過
                {
                    message = "";

                    for (int i = 0; i < dataFormat.Count; i++)//將資料錯誤列arraylist轉換成文字
                    {
                        ArrayList temp = (ArrayList)dataFormat[i];

                        if (temp[1].Equals("N"))//空值 
                        {

                            message = message + "Excel Rows " + temp[0].ToString() + " : " + "Value Empty,format error or datecode already exists" + "!!" + "\n";
                        }

                    }
                }
            }

            #endregion

            if (!message.Trim().Equals(""))//有錯誤
            {
                MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else//全部檢查通過
            {
                //Excel_Data --> dtData
                for (int i = 0; i < Excel_Data.Rows.Count; i++)
                {
                    object[] rowArray = new object[7];
                    DataRow Temp = dtData.NewRow();
                    rowArray[0] = Excel_Data.Rows[i][0].ToString();
                    rowArray[1] = Excel_Data.Rows[i][1].ToString();
                    rowArray[2] = Excel_Data.Rows[i][2].ToString();
                    rowArray[3] = UserData.UserId;
                    rowArray[4] = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                    rowArray[5] = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
                    rowArray[6] = Comcd;
                    Temp.ItemArray = rowArray;
                    dtData.Rows.Add(Temp);
                }

                btnSave.Enabled = true;
                gvData.DataSource = dtData;
            }
        }

        #endregion

        #region 搜尋

        //Vendor名稱過濾條件
        private void Vendor_Filter()
        {
            string Vendor = "";

            Vendor = txtVendor.Text.Trim().ToString();

            //根據所選擇條件,帶出datagridview
            DataTable dtVendor = new DataTable();//暫存table

            //dtVendor schema build
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                dtVendor.Columns.Add();
                dtVendor.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
            }

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                if (gvData.Rows[i].Cells[0].Value.ToString().Equals(Vendor, StringComparison.CurrentCultureIgnoreCase))
                {
                    dtVendor.Rows.Add();
                    for (int j = 0; j < gvData.Columns.Count; j++)
                        dtVendor.Rows[dtVendor.Rows.Count - 1][j] = gvData.Rows[i].Cells[j].Value;
                }
            }

            gvData.DataSource = dtVendor;
        }

        //DateCode過濾條件
        private void DateCode_Filter()
        {
            string DateCode = "";

            DateCode = txtDatecode.Text.Trim().ToString();

            //根據所選擇條件,帶出datagridview
            DataTable dtDateCode = new DataTable();//暫存table

            //dtVendor schema build
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                dtDateCode.Columns.Add();
                dtDateCode.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
            }

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                if (gvData.Rows[i].Cells[1].Value.ToString().Equals(DateCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    dtDateCode.Rows.Add();
                    for (int j = 0; j < gvData.Columns.Count; j++)
                        dtDateCode.Rows[dtDateCode.Rows.Count - 1][j] = gvData.Rows[i].Cells[j].Value;
                }
            }

            gvData.DataSource = dtDateCode;
        }

        //DateCodeAfter名稱過濾條件
        private void DateCodeAfter_Filter()
        {
            string DateCodeAfter = "";

            DateCodeAfter = txtDatecodeAfter.Text.Trim().ToString();           

            //根據所選擇條件,帶出datagridview
            DataTable dtDateCodeAfter = new DataTable();//暫存table

            //dtVendor schema build
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                dtDateCodeAfter.Columns.Add();
                dtDateCodeAfter.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
            }

            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                if (gvData.Rows[i].Cells[2].Value.ToString().Equals(DateCodeAfter, StringComparison.CurrentCultureIgnoreCase))
                {
                    dtDateCodeAfter.Rows.Add();
                    for (int j = 0; j < gvData.Columns.Count; j++)
                        dtDateCodeAfter.Rows[dtDateCodeAfter.Rows.Count - 1][j] = gvData.Rows[i].Cells[j].Value;
                }
            }

            gvData.DataSource = dtDateCodeAfter;
        }

        private void AutoComplete_Content()
        {
            AutoCompleteStringCollection Vendor_ac = new AutoCompleteStringCollection();
            AutoCompleteStringCollection DateCode_ac = new AutoCompleteStringCollection();
            AutoCompleteStringCollection DateCodeAfter_ac = new AutoCompleteStringCollection();
            //加入Location自動完成選單
            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                Vendor_ac.Add(gvData.Rows[i].Cells[0].Value.ToString());
                DateCode_ac.Add(gvData.Rows[i].Cells[1].Value.ToString());
                DateCodeAfter_ac.Add(gvData.Rows[i].Cells[2].Value.ToString());
            }

            txtVendor.AutoCompleteCustomSource = Vendor_ac;
            txtDatecode.AutoCompleteCustomSource = DateCode_ac;
            txtDatecodeAfter.AutoCompleteCustomSource = DateCodeAfter_ac;
        }

        
        #endregion

    }
}