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

namespace QWMS
{
    public partial class Manage_SparePartsDocmentQuery : Form
    {
        #region 變數宣告

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string StrComcd = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private StorageIn objStorageIn;
		private PlantData objPlantData;
		private Authority objAuthority;
		private SapData objSapData;

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
                return StrComcd;
            }
            set
            {
                StrComcd = value;
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

        public Manage_SparePartsDocmentQuery()
        {
            InitializeComponent();
        }

        public Manage_SparePartsDocmentQuery(UserInfo _UserData, string strProgid)
		{
            UserData = _UserData;
			InitializeComponent();
			Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
			Usrnm = UserData.UserId;
			Progid = strProgid;
			
			try
			{
                objStorageIn = new StorageIn(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

				//檢查權限
				if(!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					//秀出Status的資料
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					ShowDdlStartEndHour();
					this.cmbType.SelectedIndex = 2;
					this.cmbMType.SelectedIndex = 0;
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

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }

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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

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

        private void ShowDdlStartEndHour()
        {
            for (int i = 1; i <= 24; i++)
            {
                cmbStartHour.Items.Add(i.ToString());
                cmbEndHour.Items.Add(i.ToString());
            }
        }

        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 130;
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn zeileStyle = new DataGridViewTextBoxColumn();
                zeileStyle.DataPropertyName = "ZEILE";
                zeileStyle.HeaderText = "Document Item";
                zeileStyle.Width = 70;
                zeileStyle.ReadOnly = true;
                dgvData.Columns.Add(zeileStyle);

                DataGridViewTextBoxColumn trntpStyle = new DataGridViewTextBoxColumn();
                trntpStyle.DataPropertyName = "TRNTP";
                trntpStyle.HeaderText = "Type";
                trntpStyle.Width = 50;
                trntpStyle.ReadOnly = true;
                dgvData.Columns.Add(trntpStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn kdmatStyle = new DataGridViewTextBoxColumn();
                kdmatStyle.DataPropertyName = "KDMAT";
                kdmatStyle.HeaderText = "Customer P/N";
                kdmatStyle.Width = 90;
                kdmatStyle.ReadOnly = true;
                dgvData.Columns.Add(kdmatStyle);

                DataGridViewTextBoxColumn ebelnStyle = new DataGridViewTextBoxColumn();
                ebelnStyle.DataPropertyName = "EBELN";
                ebelnStyle.HeaderText = "PO No.";
                ebelnStyle.Width = 90;
                ebelnStyle.ReadOnly = true;
                dgvData.Columns.Add(ebelnStyle);

                DataGridViewTextBoxColumn rmanoStyle = new DataGridViewTextBoxColumn();
                rmanoStyle.DataPropertyName = "RMANO";
                rmanoStyle.HeaderText = "RMA No.";
                rmanoStyle.Width = 110;
                rmanoStyle.ReadOnly = true;
                dgvData.Columns.Add(rmanoStyle);

                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.Width = 50;
                insmkStyle.ReadOnly = true;
                dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Version";
                chargStyle.Width = 50;
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn otqtyStyle = new DataGridViewTextBoxColumn();
                otqtyStyle.DataPropertyName = "OTQTY";
                otqtyStyle.HeaderText = "In/Out Qty";
                otqtyStyle.Width = 80;
                otqtyStyle.ReadOnly = true;
                dgvData.Columns.Add(otqtyStyle);

                DataGridViewTextBoxColumn bwartStyle = new DataGridViewTextBoxColumn();
                bwartStyle.DataPropertyName = "BWART";
                bwartStyle.HeaderText = "Mvt";
                bwartStyle.Width = 50;
                bwartStyle.ReadOnly = true;
                dgvData.Columns.Add(bwartStyle);

                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
                lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
                lgort1Style.ReadOnly = true;
                dgvData.Columns.Add(lgort1Style);

                DataGridViewTextBoxColumn umlgoStyle = new DataGridViewTextBoxColumn();
                umlgoStyle.DataPropertyName = "UMLGO";
                umlgoStyle.HeaderText = "To S.L.";
                umlgoStyle.Width = 50;
                umlgoStyle.ReadOnly = true;
                dgvData.Columns.Add(umlgoStyle);

                DataGridViewTextBoxColumn usnamStyle = new DataGridViewTextBoxColumn();
                usnamStyle.DataPropertyName = "USNAM";
                usnamStyle.HeaderText = "SAP User";
                usnamStyle.ReadOnly = true;
                dgvData.Columns.Add(usnamStyle);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
                lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
                lifnrStyle.ReadOnly = true;
                dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn kostlStyle = new DataGridViewTextBoxColumn();
                kostlStyle.DataPropertyName = "KOSTL";
                kostlStyle.HeaderText = "Dept No.";
                kostlStyle.Width = 90;
                kostlStyle.ReadOnly = true;
                dgvData.Columns.Add(kostlStyle);

                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Created Date";
                crdatStyle.Width = 110;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            string strStartDate = "";
            string strEndDate = "";
            string strStartHour = "";
            string strEndHour = "";
            string strWerks = "";
            string strLgort = "";
            string strMType = "";
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

                if (chkDate.Checked)
                {
                    strStartDate = dtpStartDate.Value.ToString("yyyyMMdd") + " " + strStartHour;
                    strEndDate = dtpEndDate.Value.ToString("yyyyMMdd") + " " + strEndHour;
                }
                else
                {
                    strStartDate = "";
                    strEndDate = "";
                }

                if (cmbMType.SelectedIndex != -1)
                {
                    strMType = cmbMType.Items[cmbMType.SelectedIndex].ToString();
                }
                else
                {
                    stsWarning.Text = "MType can't be empty!!";
                    return;
                }


                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                string strQueryTable = "WHDWN";
                if (chkOldData.Checked)
                {
                    strQueryTable = "WHDWN_BAK";
                }

                objSapData = new SapData(UserData, strWerks, strLgort);

                dtData = objSapData.QuerySparePartsData(strStartDate, strEndDate, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), txtStartRmano.Text.Trim(), txtEndRmano.Text.Trim(), this.cmbType.SelectedIndex, this.txtBwart.Text.Trim(), strMType.Trim(), strQueryTable);

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    return;
                }

                dtData.DefaultView.Sort = "MANDT, COMCD, WERKS, LGORT, MBLNR";
                ShowDataGrid();
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
                this.cmbStartHour.SelectedIndex = -1;
                this.cmbEndHour.SelectedIndex = -1;
                this.cmbMType.SelectedIndex = -1;
                this.cmbType.SelectedIndex = 2;
                this.txtStartMblnr.Text = "";
                this.txtEndMblnr.Text = "";
                this.txtStartMatnr.Text = "";
                this.txtEndMatnr.Text = "";
                this.lblData.Text = "0 records";
                this.dtpStartDate.Value = DateTime.Now;
                this.dtpEndDate.Value = DateTime.Now;
                this.panel1.Enabled = true;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
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

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Document No\tDocument Item\tType\tPart No\tStock\tVersion\tDoc. Qty\tIn/Out Qty\tMovement Type\tFrom S.L.\tTo S.L.\tSAP User\tDept No.\tCustomer P/N\tPO No.\tCreated Date";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {

                    strLine = "";
                    strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["TRNTP"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["OTQTY"].ToString() + "\t";
                    strLine += dtData.Rows[i]["BWART"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["UMLGO"].ToString() + "\t";
                    strLine += dtData.Rows[i]["USNAM"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                    strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["EBELN"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRDAT"].ToString();
                    sw.WriteLine(strLine);
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
    }
}
