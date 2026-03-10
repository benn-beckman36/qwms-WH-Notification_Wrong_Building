using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QCI.QWMS;
using QWMS.Common;
using System.Diagnostics;

namespace QWMS
{
    public partial class Manage_SparePartsStorageQuery : Form
    {
        #region 變數宣告

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strKdmat = "";
        private string strEbeln = "";
        private string strNloca = "";
        private string strRmano = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtDataDetail = new DataTable();
        private StorageData objStorageData;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;

        #endregion

        #region DataMember

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

        #endregion

        public Manage_SparePartsStorageQuery()
        {
            InitializeComponent();
        }

        #region 建構式
        public Manage_SparePartsStorageQuery(UserInfo varUserData, string strProgid)
		{
            UserData = varUserData;

			InitializeComponent();
			Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
			Usrnm = UserData.UserId;
			Progid = strProgid;
			
			try
			{
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);
				
				//檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					//秀出Status的資料
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
					{
						this.cmbLgort.SelectedIndex = 0;
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }
        #endregion

        #region 绑定廠區
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
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }


        #region 绑定倉别
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
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
        #endregion


        #region ShowDataGrid
        private void ShowDataGrid()
        {
            try
            {
                this.dtgData.AutoGenerateColumns = false;
                this.dtgData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 50;
                dgvcWerks.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 55;
                dgvcLgort.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Name = "Matnr";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No.";
                dgvcEbeln.Width = 80;
                dgvcEbeln.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 100;
                dgvcKdmat.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Name = "Insmk";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcReqty = new DataGridViewTextBoxColumn();
                dgvcReqty.DataPropertyName = "REQTY";
                dgvcReqty.HeaderText = "Reservation Qty";
                dgvcReqty.Width = 110;
                dgvcReqty.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcReqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Old Date";
                dgvcIndat.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcTotal = new DataGridViewTextBoxColumn();
                dgvcTotal.DataPropertyName = "TOTAL";
                dgvcTotal.HeaderText = "Rows Count";
                dgvcTotal.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcTotal);

                DataGridViewTextBoxColumn dgvcToloc = new DataGridViewTextBoxColumn();
                dgvcToloc.DataPropertyName = "TOLOC";
                dgvcToloc.HeaderText = "Total Location";
                dgvcToloc.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcToloc);

                DataGridViewTextBoxColumn dgvcMiloc = new DataGridViewTextBoxColumn();
                dgvcMiloc.DataPropertyName = "MILOC";
                dgvcMiloc.HeaderText = "Mixed Location";
                dgvcMiloc.ReadOnly = true;
                this.dtgData.Columns.Add(dgvcMiloc);

                dtgData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region ShowDetailDataGrid
        private void ShowDetailDataGrid()
        {
            try
            {
                this.dtgDataDetail.AutoGenerateColumns = false;
                this.dtgDataDetail.Columns.Clear();

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcNloca = new DataGridViewTextBoxColumn();
                dgvcNloca.DataPropertyName = "NLOCA";
                dgvcNloca.HeaderText = "Shipment Location";
                dgvcNloca.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcNloca);

                DataGridViewTextBoxColumn dgvcSino = new DataGridViewTextBoxColumn();
                dgvcSino.DataPropertyName = "SIDNO";
                dgvcSino.HeaderText = "SI No.";
                dgvcSino.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcSino);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No.";
                dgvcEbeln.Width = 80;
                dgvcEbeln.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn rmanoStyle = new DataGridViewTextBoxColumn();
                rmanoStyle.DataPropertyName = "RMANO";
                rmanoStyle.HeaderText = "RMA No.";
                rmanoStyle.Width = 110;
                rmanoStyle.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(rmanoStyle);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 100;
                dgvcKdmat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 80;
                dgvcCharg.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No.";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcReqty = new DataGridViewTextBoxColumn();
                dgvcReqty.DataPropertyName = "REQTY";
                dgvcReqty.HeaderText = "Reservation Qty";
                dgvcReqty.Width = 110;
                dgvcReqty.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcReqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Create Date";
                dgvcCrdat.ReadOnly = true;
                this.dtgDataDetail.Columns.Add(dgvcCrdat);

                dtgDataDetail.DataSource = dtDataDetail;
                lblDataDetail.Text = dtDataDetail.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDetailDataGrid()");
            }
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            this.stsWarning.Text = "";
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

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                strKdmat = this.txtKdmat.Text.ToString().Trim();
                strEbeln = this.txtEbeln.Text.ToString().Trim();
                strNloca = this.txtNloca.Text.ToString().Trim();
                strRmano = this.txtRmano.Text.ToString().Trim();
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                dtData = objStorageData.QueryStorageData_SpareParts(txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtEbeln.Text.Trim(), txtSidno.Text.Trim(), txtKdmat.Text.Trim(), txtNloca.Text.Trim(), txtRmano.Text.Trim());
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid();
                    return;
                }
                else
                {
                    gbPrint.Enabled = true;
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region rdoSummery_CheckedChanged
        private void rdoSummery_CheckedChanged(object sender, System.EventArgs e)
        {
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
        }
        #endregion

        #region rdoDetail_CheckedChanged
        private void rdoDetail_CheckedChanged(object sender, System.EventArgs e)
        {
            btnPrint.Enabled = true;
            btnDownload.Enabled = true;
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            this.txtEndMatnr.Text = "";
            this.txtStartMatnr.Text = "";
            this.txtEbeln.Text = "";
            this.txtSidno.Text = "";
            this.txtKdmat.Text = "";
            this.txtNloca.Text = "";
            this.lblData.Text = "0 records";
            this.lblDataDetail.Text = "0 records";
            this.gbStock.Enabled = true;
            this.rdoDetail.Checked = false;
            this.rdoSummary.Checked = false;
            this.btnPrint.Enabled = false;
            this.btnDownload.Enabled = false;
            this.gbPrint.Enabled = false;
            this.dtgDataDetail.DataSource = null;
            this.dtDataDetail.Rows.Clear();
            this.dtgDataDetail.Select();
            this.dtgData.DataSource = null;
            this.dtData.Rows.Clear();
            dtData.Select();
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void dtgData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                string strMatnr = "";
                int intRowNo;
                DataGridView dgClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    try
                    {
                        strMatnr = dgClick.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                    }
                    catch
                    {
                        strMatnr = "";
                    }
                    dtDataDetail = objStorageData.QueryStorageData_SpareParts(strMatnr, strKdmat, strNloca, strEbeln, strRmano);
                    this.ShowDetailDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region Print
        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.rdoSummary.Checked)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "SUMMARY", dtData);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                if (this.rdoDetail.Checked)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "DETAILSPAREPARTS", dtDataDetail);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Download
        private void btnDownload_Click(object sender, System.EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (this.rdoSummary.Checked)
                {
                    sfdSaveFile.FileName = "InventorySummary.xls";
                    if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        strExportName = sfdSaveFile.FileName;
                        CountingResult2File(strExportName);
                    }
                }
                if (this.rdoDetail.Checked)
                {
                    sfdSaveFile.FileName = "InventoryDetail.xls";
                    if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        strExportName = sfdSaveFile.FileName;
                        CountingResult2File(strExportName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region 導出文件
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (this.rdoSummary.Checked)
                {
                    strLine = "Plant\tStorage\tPart No\tStock\tQty\tOld Date\tRows Count\tTotal Location\tMixed Location";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["TOTAL"].ToString() + "\t";
                        strLine += dtData.Rows[i]["TOLOC"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MILOC"].ToString();
                        sw.WriteLine(strLine);
                    }
                }

                if (this.rdoDetail.Checked)
                {
                    strLine = "Plant\tStorage\tLocation\tShipment Location\tPart No\tCust Mat\tPO No\tStock\tVersion\tSI No.\tSerial No\tVendor\tQty\tMixed Material No\tStore In Date";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtDataDetail.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["NLOCA"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["EBELN"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["SIDNO"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["SERNO"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LIFNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["MRGID"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["INDAT"].ToString();
                        sw.WriteLine(strLine);
                    }
                }
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

        private void txtStartMatnr_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                Manage_MaterialSelect objManage_MaterialSelect = new Manage_MaterialSelect(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "INVENTORY", txtStartMatnr.Text.Trim());
                objManage_MaterialSelect.ShowDialog();
                txtStartMatnr.Text = objManage_MaterialSelect.Matnr;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtEndMatnr_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                Manage_MaterialSelect objManage_MaterialSelect = new Manage_MaterialSelect(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "INVENTORY", txtEndMatnr.Text.Trim());
                objManage_MaterialSelect.ShowDialog();
                txtEndMatnr.Text = objManage_MaterialSelect.Matnr;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private string GetMblnrData(ArrayList aryLoadID)
        {
            try
            {
                string strTempMblnr = "";
                for (int i = 0; i < aryLoadID.Count; i++)
                {
                    strTempMblnr += "," + aryLoadID[i].ToString();
                }
                if (strTempMblnr.Length > 0)
                {
                    strTempMblnr = strTempMblnr.Substring(1);
                }
                return strTempMblnr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }

        private void txtSino_DoubleClick(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            ArrayList arySidno = new ArrayList();
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }

                //廠區倉別不為空
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                arySidno.Clear();

                StorageOut_SapDataSelect objStorageOut_SapDataSelect = new StorageOut_SapDataSelect(UserData, strWerks, strLgort, Progid, arySidno, "SPARE_PARTS");
                objStorageOut_SapDataSelect.ShowDialog();
                arySidno = objStorageOut_SapDataSelect.Mblnr;
                txtSidno.Text = GetMblnrData(arySidno);
                if (arySidno.Count > 0)
                {
                    this.txtSidno.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void dtgDataDetail_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.btnSave.Enabled = true;
            stsWarning.Text = "";

            try
            {
                string strMatnr = "";
                string strInsmk = "";
                string strCharg = "";
                string strEbeln = "";
                string strKdmat = "";
                string strLocat = "";
                string strMblnr = "";

                strLocat = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[0].Value);
                strMatnr = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[3].Value);
                strEbeln = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[4].Value);
                strKdmat = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[5].Value);
                strInsmk = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[6].Value);
                strCharg = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[7].Value);
                strMblnr = Convert.ToString(dtgDataDetail.Rows[e.RowIndex].Cells[8].Value);

                if (strLocat.Substring(0, 1) == "N")
                {
                    stsWarning.Text = "待出貨儲位不能修改PO NO.與客人料號，請確認!!";
                    return;
                }
                else
                {
                    //C10: StorageOut_OnLineOut_SpareParts的progid
                    StorageOut_OnLineOut_SpareParts_Modify objStorageOut_OnLineOut_SpareParts_Modify = new StorageOut_OnLineOut_SpareParts_Modify(UserData, strWerks, strLgort, "C10", strMatnr, strInsmk, strCharg, strEbeln, strKdmat, strMblnr, dtDataDetail);
                    objStorageOut_OnLineOut_SpareParts_Modify.Matnr = strMatnr;
                    objStorageOut_OnLineOut_SpareParts_Modify.Charg = strCharg;
                    objStorageOut_OnLineOut_SpareParts_Modify.Ebeln = strEbeln;
                    objStorageOut_OnLineOut_SpareParts_Modify.Kdmat = strKdmat;
                    objStorageOut_OnLineOut_SpareParts_Modify.ShowDialog();
                    dtData = objStorageOut_OnLineOut_SpareParts_Modify.QtyData;
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dtData.AcceptChanges();

            try
            {
                //C10: StorageOut_OnLineOut_SpareParts的progid
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "C10");
                //更新庫存資料(Spare Parts)
                if (objStorageOut.UpdateStorageData(dtData))
                {
                    stsWarning.Text = "Update Data Ok!!";
                    this.btnSave.Enabled = false;
                    return;
                }
                else
                {
                    stsWarning.Text = "Save fail!! " + objStorageOut.ERRMSG;
                    this.btnSave.Enabled = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                this.btnSave.Enabled = false;
                return;
            }
        }
    }
}
