using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //引用数字公式类
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using QCI.QWMS;
using QWMS.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QWMS
{
    public partial class IQCBorrowMaterials_Detail : Form
    {
        public IQCBorrowMaterials_Detail()
        {
            InitializeComponent();
        }
        public IQCBorrowMaterials_Detail(UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strLocat, string strMatnr, string strLifnr, string strRemark)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            Werks = strWerks;
            Remark = strRemark;

            try
            {
                showIQCBorrowDetail(strWerks, strLgort, strMblnr, strLocat, strMatnr, strLifnr, strRemark);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }
        UserInfo UserData = new UserInfo();
        DataTable dt = new DataTable();
        private string Werks = "";
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string Remark = "";

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
        public string Werkss
        {
            get
            {
                return Werks;
            }
            set
            {
                Werks = value;
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
        #endregion

        # region 显示借料资料
        public void ShowBorrowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "单据号";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.Width = 100;
                dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                dgvcWERKS.Width = 60;
                dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                dgvcLGORT.Width = 60;
                dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 60;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "异动代码";
                dgvcBWART.ReadOnly = true;
                dgvcBWART.Width = 60;
                dgvData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "料号数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBRQTY = new DataGridViewTextBoxColumn();
                dgvcBRQTY.DataPropertyName = "BRQTY";
                dgvcBRQTY.Name = "BRQTY";
                dgvcBRQTY.HeaderText = "借出数量";
                dgvcBRQTY.Width = 60;
                dgvData.Columns.Add(dgvcBRQTY);
                dgvcBRQTY.ReadOnly = true;

                DataGridViewTextBoxColumn dgvcBRDAT = new DataGridViewTextBoxColumn();
                dgvcBRDAT.DataPropertyName = "CRDAT";
                dgvcBRDAT.HeaderText = "借出时间";
                dgvcBRDAT.ReadOnly = true;
                dgvcBRDAT.Width = 160;
                dgvData.Columns.Add(dgvcBRDAT);

                DataGridViewTextBoxColumn dgvcIQCID = new DataGridViewTextBoxColumn();
                dgvcIQCID.DataPropertyName = "IQCID";
                dgvcIQCID.HeaderText = "借料IQC工号";
                dgvcIQCID.ReadOnly = true;
                dgvcIQCID.Width = 100;
                dgvData.Columns.Add(dgvcIQCID);

                DataGridViewTextBoxColumn dgvcWHID = new DataGridViewTextBoxColumn();
                dgvcWHID.DataPropertyName = "WHID";
                dgvcWHID.HeaderText = "借料仓管工号";
                dgvcWHID.ReadOnly = true;
                dgvcWHID.Width = 100;
                dgvData.Columns.Add(dgvcWHID);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.ReadOnly = true;
                dgvcLIFNR.Width = 80;
                dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "Remark";
                dgvcREMAK.HeaderText = "备注";
                dgvcREMAK.ReadOnly = false;
                dgvcREMAK.Width = 100;
                dgvData.Columns.Add(dgvcREMAK);

                dgvData.DataSource = dt;
                lblCount.Text = dt.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowBorrowDataGrid()");
            }
        }
        #endregion

        # region 显示还料资料
        public void ShowReturnDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "单据号";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.Width = 100;
                dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                dgvcWERKS.Width = 60;
                dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                dgvcLGORT.Width = 60;
                dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 60;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "异动代码";
                dgvcBWART.ReadOnly = true;
                dgvcBWART.Width = 60;
                dgvData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "料号数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcRTQTY = new DataGridViewTextBoxColumn();
                dgvcRTQTY.DataPropertyName = "RTQTY";
                dgvcRTQTY.Name = "RTQTY";
                dgvcRTQTY.HeaderText = "还料数量";
                dgvcRTQTY.Width = 60;
                dgvData.Columns.Add(dgvcRTQTY);
                dgvcRTQTY.ReadOnly = true;

                DataGridViewTextBoxColumn dgvcRTDAT = new DataGridViewTextBoxColumn();
                dgvcRTDAT.DataPropertyName = "RTDAT";
                dgvcRTDAT.HeaderText = "还料时间";
                dgvcRTDAT.ReadOnly = true;
                dgvcRTDAT.Width = 160;
                dgvData.Columns.Add(dgvcRTDAT);

                DataGridViewTextBoxColumn dgvcrRTIQCID = new DataGridViewTextBoxColumn();
                dgvcrRTIQCID.DataPropertyName = "RTIQCID";
                dgvcrRTIQCID.HeaderText = "还料IQC工号";
                dgvcrRTIQCID.ReadOnly = true;
                dgvcrRTIQCID.Width = 100;
                dgvData.Columns.Add(dgvcrRTIQCID);

                DataGridViewTextBoxColumn dgvcRTWHID = new DataGridViewTextBoxColumn();
                dgvcRTWHID.DataPropertyName = "RTWHID";
                dgvcRTWHID.HeaderText = "还料仓管工号";
                dgvcRTWHID.ReadOnly = true;
                dgvcRTWHID.Width = 100;
                dgvData.Columns.Add(dgvcRTWHID);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.ReadOnly = true;
                dgvcLIFNR.Width = 80;
                dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "Remark";
                dgvcREMAK.HeaderText = "备注";
                dgvcREMAK.ReadOnly = false;
                dgvcREMAK.Width = 100;
                dgvData.Columns.Add(dgvcREMAK);

                dgvData.DataSource = dt;
                lblCount.Text = dt.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowReturnDataGrid()");
            }
        }
        #endregion

        private void btnexit_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.Close();
            this.Dispose();
        }

        private void showIQCBorrowDetail(string strWerks, string strLgort, string strMblnr, string strLocat, string strMatnr, string strLifnr, string strRemark)
        {
            try
            {
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dt = objSapData.Query_IQCBorrowData(strWerks, strLgort, strMblnr, strLocat, strMatnr, strLifnr, strRemark);
                if (dt.Rows.Count == 0)
                {
                    this.btnDownload.Enabled = false;
                }
                if (strRemark == "borrow")
                {
                    ShowBorrowDataGrid();
                }
                if (strRemark == "return")
                {
                    ShowReturnDataGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-showIQCBorrowDetail()");
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";

            try
            {

                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    DownExcel(strExportName, dt, Remark);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Duplicate file name");
                return;
            }
        }

        #region 下载excel表格
        public void DownExcel(string path, System.Data.DataTable dt, string strRemark)
        {
            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = book.CreateSheet("worksheet") as HSSFSheet;
            HSSFCellStyle style = book.CreateCellStyle() as HSSFCellStyle;
            ICellStyle headStyle = book.CreateCellStyle();
            HSSFFont font = (HSSFFont)book.CreateFont();

            #region 表头显示内容
            try
            {
                if (sheet != null)
                {
                    HSSFRow cellHead = (HSSFRow)sheet.CreateRow(0);
                    ICell cell0 = cellHead.CreateCell(0);
                    cell0.SetCellValue("单据号");
                    cell0.CellStyle = headStyle;

                    ICell cell1 = cellHead.CreateCell(1);
                    cell1.SetCellValue("厂区");
                    cell1.CellStyle = headStyle;

                    ICell cell2 = cellHead.CreateCell(2);
                    cell2.SetCellValue("仓别");
                    cell2.CellStyle = headStyle;

                    ICell cell3 = cellHead.CreateCell(3);
                    cell3.SetCellValue("储位");
                    cell3.CellStyle = headStyle;

                    ICell cell4 = cellHead.CreateCell(4);
                    cell4.SetCellValue("料号");
                    cell4.CellStyle = headStyle;

                    ICell cell5 = cellHead.CreateCell(5);
                    cell5.SetCellValue("单据数量");
                    cell5.CellStyle = headStyle;

                    if (strRemark=="borrow")
                    {
                        ICell cell6 = cellHead.CreateCell(6);
                        cell6.SetCellValue("借料数量");
                        cell6.CellStyle = headStyle;

                        ICell cell7 = cellHead.CreateCell(7);
                        cell7.SetCellValue("借料时间");
                        cell7.CellStyle = headStyle;

                        ICell cell8 = cellHead.CreateCell(8);
                        cell8.SetCellValue("借料IQC工号");
                        cell8.CellStyle = headStyle;

                        ICell cell9 = cellHead.CreateCell(9);
                        cell9.SetCellValue("借料仓管工号");
                        cell9.CellStyle = headStyle;
                    }
                    else if (strRemark == "return")
                    {
                        ICell cell6 = cellHead.CreateCell(6);
                        cell6.SetCellValue("还料数量");
                        cell6.CellStyle = headStyle;

                        ICell cell7 = cellHead.CreateCell(7);
                        cell7.SetCellValue("还料时间");
                        cell7.CellStyle = headStyle;

                        ICell cell8 = cellHead.CreateCell(8);
                        cell8.SetCellValue("还料IQC工号");
                        cell8.CellStyle = headStyle;

                        ICell cell9 = cellHead.CreateCell(9);
                        cell9.SetCellValue("还料仓管工号");
                        cell9.CellStyle = headStyle;
                    }

                    ICell cell10 = cellHead.CreateCell(10);
                    cell10.SetCellValue("厂商代码");
                    cell10.CellStyle = headStyle;

                    ICell cell11 = cellHead.CreateCell(11);
                    cell11.SetCellValue("备注");
                    cell11.CellStyle = headStyle;
                }
                #endregion

                #region 写入单元格内容

                for (int i = 0; i < dt.Rows.Count; i++)//表体            
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string d = dt.Rows[i][j].ToString();
                        dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }
                #endregion

                #region 调整列宽
                CellRangeAddress range = new CellRangeAddress(1, dt.Rows.Count + 1, 0, dt.Columns.Count + 1);
                for (int columnNum = 0; columnNum <= 10; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)//在这一列上循环行  
                    {
                        IRow currentRow = sheet.GetRow(rowNum);
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = System.Text.Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                        if (columnWidth < length + 1)
                        {
                            columnWidth = length + 2;
                        }
                    }
                    sheet.SetColumnWidth(columnNum, columnWidth * 256);
                }
                FileStream file = File.OpenWrite(path);
                book.Write(file);
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }
        #endregion
    }
}
