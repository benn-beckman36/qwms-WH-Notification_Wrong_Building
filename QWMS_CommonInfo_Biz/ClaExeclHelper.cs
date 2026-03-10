using System;
using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Threading;
using System.Globalization;
using OfficeOpenXml;
using System.IO;
using System.Linq;


namespace QWMS.Common 
{
    public class ClaExeclHelper
    {
        #region Excel查询方法 OLEDB形式
        public System.Data.DataTable ExcelQuery(string strFilePath, string strcmd)
        {
            string varExt = strFilePath.Substring(strFilePath.LastIndexOf(".") + 1).ToLower();
            OleDbConnection oleConn = null;
            OleDbCommand cmd = null;
            DataSet dsExcel = new DataSet();
            try
            {
                string strConn = "";
                if (varExt.ToUpper() == "XLS")
                {
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended Properties='Excel 8.0;HDR=yes;IMEX=1'";
                }
                else
                    if (varExt.ToUpper() == "XLSX")
                    {
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties='Excel 12.0;HDR=yes;IMEX=1'";
                    }

                oleConn = new OleDbConnection(strConn);

                oleConn.Open();
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter(strcmd, oleConn);

                oleAdapter.Fill(dsExcel, "dtExcel");
                return dsExcel.Tables["dtExcel"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oleConn.Close();
            }

        }
        #endregion

        #region Datatable To Excel 
        public void DatatableToExcel(System.Data.DataTable dtData, string strFilePath,string strSheetName)
        {
            Application excel = new Application();
            Workbook book;
            Worksheet sheet;
            Missing miss = Missing.Value;
            System.Globalization.CultureInfo currentci = System.Threading.Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
           try
           {
                book = excel.Workbooks.Add(miss);
                sheet = (Worksheet)book.Worksheets.Add(miss, miss, miss, miss);
                sheet.Name =strSheetName;
                string str = book.Name;
               
                
               excel.Visible = false;
                // excel.Application.Workbooks.Add(true);
               for (int i = 0; i < dtData.Columns.Count; i++)
               {
                   sheet.Cells[1, i + 1] = dtData.Columns[i].ColumnName.ToString();
               }
               for (int i = 0; i < dtData.Rows.Count; i++)
               {
                   for (int j = 0; j < dtData.Columns.Count; j++)
                   {
                       sheet.Cells[i + 2, j + 1] = "'" + dtData.Rows[i].ItemArray[j];
                   }
               }
              book.SaveAs(strFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                excel.Quit();
                excel = null;
            }
        }
        #endregion 

        #region Excel读取，EPPlus方法
        /// <summary>
        /// EPPlus读取EXCEL文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="hasHeader">是否有表头</param>
        /// <returns></returns>
        public System.Data.DataTable GetDataTableFromExcel(string path, bool hasHeader)
        {
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    package.Load(stream);
                }
                ExcelWorksheet ws = package.Workbook.Worksheets.First();
                System.Data.DataTable dtExcel = new System.Data.DataTable();
                #region  获取表格数据列数dscolum
                int colum = ws.Dimension.End.Column;
                int dscolum = 0;
                for (int i =1 ; i<= colum;i++)
                {
                    string w = Convert.ToString(ws.Cells[1, i, 1, i].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(ws.Cells[1, i, 1, i].Value)))
                {
                    dscolum = dscolum + 1;
                }
                }
                #endregion
                foreach (var firstRowCell in ws.Cells[1, 1, 1, dscolum])
                {
                    dtExcel.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column { 0}", firstRowCell.Start.Column));
                }

                int startRow = hasHeader ? 2 : 1;
                #region  获取表格数据行数dsrows
                int rows = ws.Dimension.End.Row;
                int dsrows = 0;
                for (int i = 1; i <= rows; i++)
                {
                    string w = Convert.ToString(ws.Cells[i, 1, i, 1].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(ws.Cells[i, 1, i, 1].Value)))
                    {
                        dsrows = dsrows + 1;
                    }
                }
                #endregion
                for (int rowNum = startRow; rowNum <= dsrows; rowNum++)
                {
                    DataRow row = dtExcel.NewRow();
                    for (int colNum = 1; colNum <= dscolum; colNum++)
                    {
                        var value = ws.GetValue<string>(rowNum, colNum);
                        row[colNum - 1] = value;
                    }
                    dtExcel.Rows.Add(row);
                }
                return dtExcel;
            }
        }
        #endregion

    }
}
