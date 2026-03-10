using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using QCI.QWMS;
using QWMS.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

namespace QWMS
{
    public  partial class Manage_DateCodeTimeQuery : Form
    {

        #region new-
        UserInfo UserData = new UserInfo();
        private System.Windows.Forms.StatusBarPanel stsWarning;


        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strType = "";
        private System.Data.DataTable dtData = new System.Data.DataTable();
        private System.Data.DataTable dtUpdata = new System.Data.DataTable();
        private PlantData objPlantData;
        private StorageIn objStorageIn;
        private Authority objAuthority;
        private StorageData objStorageData;
        

        private FileInfo fi;
        private StreamWriter sw;


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
        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region CheckAuthority
        public Manage_DateCodeTimeQuery(UserInfo _UserData, string strProgid)
		{
            InitializeComponent();
            UserData = _UserData;	
			Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
			Usrnm = UserData.UserId;
			Progid = strProgid;
			try
			{
				objStorageIn = new StorageIn(UserData, Progid);
				objPlantData = new PlantData(UserData);
				objAuthority = new Authority(UserData);

				if(!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInsmk();
                    this.cmbType1.SelectedIndex = -1;
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = -1;
                    }
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
        #endregion

        private void ShowDdlInsmk()
        {
            System.Data.DataTable dtTemp = new System.Data.DataTable();
            //DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }
        private void ShowDdlWerks()
        {
            System.Data.DataTable dtTemp = new System.Data.DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckDCPlantAuthority();
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

        #region cmbwerks
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
            this.cmbLgort.SelectedIndex = -1;
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                System.Data.DataTable dtTemp = new System.Data.DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckDCWithAuth(strWerks);
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_VALUE"].ToString());
                        if (dtTemp.Rows[i]["F_VALUE"].ToString() == strLgort && strLgort != "")
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
        #endregion 

        #region confirm
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            string strType1 = "";
            string strType2 = "";
            string strStartDate = "";
            string strEndDate = "";
            string strComparsion = "";
            string strWerks = "";
            string strLgort = "";
            string strInsmk = "";
            string strVendor = "";
            string EndMatnr = "";
            string StartMatnr = "";
            string StartLocat = "";
            string EndLocat = "";
            string strLocked = "";
        
            stsWarning.Text = "";

            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                //厂区不能为空
                if (strWerks == "")
                {
                    stsWarning.Text = "Plant  can't be empty!!";
                    return;
                }
                if (strInsmk == "")
                {
                    stsWarning.Text = "Stock  can't be empty!!";
                    return;
                }
                //DateCode时间
                if (this.txtStartDate.Text.Trim() == "")
                {
                    stsWarning.Text = "Time period can not't be empty!!";
                    return;
                }             
                try
                {
                    int intStartDate = Int32.Parse(this.txtStartDate.Text.Trim());
                    if (this.txtEndDate.Text.Trim() != "")
                    {
                        int intEndDate = Int32.Parse(this.txtEndDate.Text.Trim());
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = "Time period shoud be number!!";
                    return;
                }
                // 第一个符号的选择 
                strType1 = cmbType1.Items[cmbType1.SelectedIndex].ToString();
                //开始日期
                strStartDate = this.txtStartDate.Text.Trim();
                strVendor = this.txtVendor.Text.Trim();
                if (this.cmbComparsion.SelectedIndex == -1 || this.cmbType2.SelectedIndex == -1 || this.txtEndDate.Text.Trim() == "")
                {
                    strType2 = "";
                    strEndDate = "";
                    strComparsion = "";
                }
                else
                {
                    strType2 = cmbType2.Items[cmbType2.SelectedIndex].ToString();
                    strEndDate = this.txtEndDate.Text.Trim();
                    strComparsion = cmbComparsion.Items[cmbComparsion.SelectedIndex].ToString();
                }
                StartMatnr = txtStartMatnr.Text.Trim();
                EndMatnr = txtEndMatnr.Text.Trim();
                if (StartMatnr != "" && EndMatnr != "")
                {
                    if (StartMatnr.CompareTo(EndMatnr) > 0)
                    {
                        stsWarning.Text = "Part No unqualified!!";
                        return;
                    }
      
                }
                //添加储位区间
                StartLocat = txtStartLocat.Text.Trim();
                EndLocat = txtEndLocat.Text.Trim();
                if (StartLocat != "" && EndLocat != "")
                {
                    if (StartLocat.CompareTo(EndLocat) > 0)
                    {
                        stsWarning.Text = "Location unqualified!!";
                        return;
                    }

                }

                strLocked = txtLocked.Text.Trim();
              
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                //数据查询
                dtData = objStorageData.QueryDateCodeTimeData(strInsmk, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtVendor.Text.Trim(), strType1, strStartDate, strComparsion, strType2, strEndDate, txtStartLocat.Text.Trim(), txtEndLocat.Text.Trim(), txtLocked.Text.Trim());
                if (dtData.Rows.Count != 0)
                {
                    dtUpdata.Clear();
                    dtUpdata.Columns.Clear();
                    dtUpdata.Columns.Add(new DataColumn("Plant", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Storage", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Location", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Stock", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Part No", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Vendor", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Qty", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Store In Date", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Date Code", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Exp. Date", typeof(string))); 
                    dtUpdata.Columns.Add(new DataColumn("Remark(Vendor Date Code)", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Charg", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Lot Code", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Lock", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Re-Inspection lot", typeof(string)));
                    dtUpdata.Columns.Add(new DataColumn("Remark", typeof(string)));
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        DataRow dr = dtUpdata.NewRow();
                        dr["Plant"] = dtData.Rows[i]["WERKS"];
                        dr["Storage"] = dtData.Rows[i]["LGORT"];
                        dr["Location"] = dtData.Rows[i]["LOCAT"];
                        dr["Stock"] = dtData.Rows[i]["INSMK"];
                        dr["Part No"] = dtData.Rows[i]["MATNR"];
                        dr["Vendor"] = dtData.Rows[i]["LIFNR"];
                        dr["Qty"] = dtData.Rows[i]["MENGE"];
                        dr["Store In Date"] = dtData.Rows[i]["INDAT"];
                        dr["Date Code"] = dtData.Rows[i]["VEDAT"];
                        dr["Exp. Date"] = dtData.Rows[i]["ExpiryDate"];
                        dr["Remark(Vendor Date Code)"] = dtData.Rows[i]["DACOD"];
                        dr["Charg"] = dtData.Rows[i]["CHARG"];
                        dr["Lot Code"] = dtData.Rows[i]["LOCOD"];
                        dr["Lock"] = dtData.Rows[i]["LOCKED"];
                        dr["Re-Inspection lot"] = dtData.Rows[i]["TASKID"];
                        dr["Remark"] = dtData.Rows[i]["RMAK1"];
                        dtUpdata.Rows.Add(dr);                  
                    }
                }
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid();
                    return;
                }
                else
                {    
                    ShowDataGrid();
                    //ColorStats();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            dgvData.RowTemplate.Height = 20;
            try
            {
                //厂区
                DataGridViewTextBoxColumn werksStyle = new DataGridViewTextBoxColumn();
                werksStyle.DataPropertyName = "WERKS";
                werksStyle.HeaderText = "Plant";
                werksStyle.Width = 50;
                werksStyle.ReadOnly = true;
                dgvData.Columns.Add(werksStyle);
                //仓别
                DataGridViewTextBoxColumn lgortStyle = new DataGridViewTextBoxColumn();
                lgortStyle.DataPropertyName = "LGORT";
                lgortStyle.HeaderText = "Storage";
                lgortStyle.Width = 50;
                lgortStyle.ReadOnly = true;
                dgvData.Columns.Add(lgortStyle);
                //储位
                DataGridViewTextBoxColumn locatStyle = new DataGridViewTextBoxColumn();
                locatStyle.DataPropertyName = "LOCAT";
                locatStyle.HeaderText = "Location";
                locatStyle.Width = 60;
                locatStyle.ReadOnly = true;
                dgvData.Columns.Add(locatStyle);
                //库别
                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.Width = 60;
                insmkStyle.ReadOnly = true;
                dgvData.Columns.Add(insmkStyle);
                //料号
                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 120;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);
                //厂商
                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
                lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
                lifnrStyle.ReadOnly = true;
                dgvData.Columns.Add(lifnrStyle);
                //数量
                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Qty";
                mengeStyle.Width = 70;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);
                //入库日期
                DataGridViewTextBoxColumn indatStyle = new DataGridViewTextBoxColumn();
                indatStyle.DataPropertyName = "INDAT";
                indatStyle.HeaderText = "Store In Date";
                indatStyle.Width = 120;
                indatStyle.ReadOnly = true;
                dgvData.Columns.Add(indatStyle);                
                //转换后日期
                DataGridViewTextBoxColumn vedatStyle = new DataGridViewTextBoxColumn();
                vedatStyle.DataPropertyName = "VEDAT";
                //vedatStyle.HeaderText = "TR Date Code";
                vedatStyle.HeaderText = "Date Code";
                vedatStyle.Width = 120;
                vedatStyle.ReadOnly = true;
                dgvData.Columns.Add(vedatStyle);
                //有效期
                DataGridViewTextBoxColumn ExpiryDateStyle = new DataGridViewTextBoxColumn();
                ExpiryDateStyle.DataPropertyName = "ExpiryDate";
                //ExpiryDateStyle.HeaderText = "ExpiryDate";
                ExpiryDateStyle.HeaderText = "Exp. Date";
                ExpiryDateStyle.Width = 110;
                ExpiryDateStyle.ReadOnly = true;
                dgvData.Columns.Add(ExpiryDateStyle);
       
                DataGridViewTextBoxColumn dacodStyle = new DataGridViewTextBoxColumn();
                dacodStyle.DataPropertyName = "DACOD";
                //dacodStyle.HeaderText = "Date Code";
                dacodStyle.HeaderText = "Remark(Vendor Date Code)";
                dacodStyle.Width = 90;
                dacodStyle.ReadOnly = true;
                dgvData.Columns.Add(dacodStyle);
                //CHARG
                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Charg";
                chargStyle.Width = 70;
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);
                //LOCOD
                DataGridViewTextBoxColumn locodStyle = new DataGridViewTextBoxColumn();
                locodStyle.DataPropertyName = "LOCOD";
                locodStyle.HeaderText = "Lot Code";
                locodStyle.Width = 70;
                locodStyle.ReadOnly = true;
                dgvData.Columns.Add(locodStyle);

                DataGridViewTextBoxColumn dcdayLOCKED = new DataGridViewTextBoxColumn();
                dcdayLOCKED.DataPropertyName = "LOCKED";
                dcdayLOCKED.HeaderText = "Lock";
                dcdayLOCKED.Width = 80;
                dcdayLOCKED.ReadOnly = true;
                dgvData.Columns.Add(dcdayLOCKED);

                DataGridViewTextBoxColumn dcdayTASKID = new DataGridViewTextBoxColumn();
                dcdayTASKID.DataPropertyName = "TASKID";
                dcdayTASKID.HeaderText = "Re-Inspection lot";
                dcdayTASKID.Width = 180;
                dcdayTASKID.ReadOnly = true;
                dgvData.Columns.Add(dcdayTASKID);

                DataGridViewTextBoxColumn rmak1Style = new DataGridViewTextBoxColumn();
                rmak1Style.DataPropertyName = "RMAK1";
                rmak1Style.HeaderText = "Remark";
                rmak1Style.Width = 110;
                rmak1Style.ReadOnly = true;
                dgvData.Columns.Add(rmak1Style);

                dgvData.DataSource = dtData;
                this.lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }

        #endregion 

        #region Refresh
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.cmbWerks.SelectedIndex = -1;
                this.cmbLgort.SelectedIndex = -1;
                this.cmbInsmk.SelectedIndex = -1;
                this.cmbType1.SelectedIndex = -1;
                this.cmbType2.SelectedIndex = -1;
                this.cmbComparsion.SelectedIndex = -1;
                this.txtStartMatnr.Text = "";
                this.txtEndMatnr.Text = "";
                this.txtStartDate.Text = "";
                this.lblData.Text = "0 records";
                this.txtEndDate.Text = "";
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.dtUpdata.Clear();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region DownLoad
        private void btnDownload_Click(object sender, System.EventArgs e)
        {
            string strExportName = "";
          
            try
            {

                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    DownExcel(strExportName, dtUpdata);  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Duplicate file name");
                return;
            }
        }
        #endregion

        #region 下载excel表格

        public void DownExcel(string path, System.Data.DataTable dt)
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
                    cell0.SetCellValue("Plant");
                    cell0.CellStyle = headStyle;

                    ICell cell1 = cellHead.CreateCell(1);
                    cell1.SetCellValue("Storage");
                    cell1.CellStyle = headStyle;

                    ICell cell2 = cellHead.CreateCell(2);
                    cell2.SetCellValue("Location");
                    cell2.CellStyle = headStyle;

                    ICell cell3 = cellHead.CreateCell(3);
                    cell3.SetCellValue("Stock");
                    cell3.CellStyle = headStyle;

                    ICell cell4 = cellHead.CreateCell(4);
                    cell4.SetCellValue("Part NO");
                    cell4.CellStyle = headStyle;

                    ICell cell5 = cellHead.CreateCell(5);
                    cell5.SetCellValue("Vendor");
                    cell5.CellStyle = headStyle;

                    ICell cell6 = cellHead.CreateCell(6);
                    cell6.SetCellValue("Qty");
                    cell6.CellStyle = headStyle;

                    ICell cell7 = cellHead.CreateCell(7);
                    cell7.SetCellValue("Store In Date");
                    cell7.CellStyle = headStyle;

                    ICell cell8 = cellHead.CreateCell(8);
                    cell8.SetCellValue("Date Code");
                    cell8.CellStyle = headStyle;

                    ICell cell9 = cellHead.CreateCell(9);
                    cell9.SetCellValue("Exp. Date");
                    cell9.CellStyle = headStyle;

                    ICell cell10 = cellHead.CreateCell(10);
                    cell10.SetCellValue("Remark(Vendor Date Code)");
                    cell10.CellStyle = headStyle;

                    ICell cell15 = cellHead.CreateCell(15);
                    cell15.SetCellValue("Charg");
                    cell15.CellStyle = headStyle;

                    ICell cell11 = cellHead.CreateCell(11);
                    cell11.SetCellValue("Lot Code");
                    cell11.CellStyle = headStyle;

                    ICell cell12 = cellHead.CreateCell(12);
                    cell12.SetCellValue("Lock");
                    cell12.CellStyle = headStyle;


                    ICell cell13 = cellHead.CreateCell(13);
                    cell13.SetCellValue("Re-Inspection lot");
                    cell13.CellStyle = headStyle;

                    ICell cell14 = cellHead.CreateCell(14);
                    cell14.SetCellValue("Remark");
                    cell14.CellStyle = headStyle;
                }
            #endregion
            
            #region 写入单元格内容
        
                for (int i = 0; i < dt.Rows.Count; i++)//表体            
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(i+1);
                    for (int j = 0; j <dt.Columns.Count; j++)
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

        #region Size
        private void Manage_DateCodeTimeQuery_Resize(object sender, System.EventArgs e)
        {
            panel5.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel5.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - this.stsComcd.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }
        #endregion

        #region ColorStats：显示行颜色
        public void ColorStats()
        {
            try
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //if (Convert.ToInt32(dtData.Rows[i]["DCDAY"]) >= 730)
                    //{
                    //    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                    //}
                    //else if (Convert.ToInt32(dtData.Rows[i]["DCDAY"]) < 730 && Convert.ToInt32(dtData.Rows[i]["DCDAY"]) >= 365)
                    //{
                    //    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    //}
                    //else if (Convert.ToInt32(dtData.Rows[i]["DCDAY"]) < 365 && Convert.ToInt32(dtData.Rows[i]["DCDAY"]) >= 180)
                    //{
                    //    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    //}
                    if (Convert.ToString(dtData.Rows[i]["LOCKED"]) == "Y")
                    {
                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
    }
}
