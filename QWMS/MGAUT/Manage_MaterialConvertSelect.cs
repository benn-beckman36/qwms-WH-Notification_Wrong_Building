using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;

namespace QWMS
{
    public partial class Manage_MaterialConvertSelect : Form
    {
        #region 变量
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strIntype = "";
        private string strInsmk = "";
        private string strTrntp = "";
        private string strComcd = "";
        private string strDate = "";
        private string strType = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();

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
        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }
        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }
        public string Intype
        {
            get
            {
                return strIntype;
            }
            set
            {
                strIntype = value;
            }
        }
        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }
        public string Trntp
        {
            get
            {
                return strTrntp;
            }
            set
            {
                strTrntp = value;
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
        public string Date
        {
            get
            {
                return strDate;
            }
            set
            {
                strDate = value;
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

        public Manage_MaterialConvertSelect()
        {
            InitializeComponent();
        }

        public Manage_MaterialConvertSelect(UserInfo varUserData, string strWerks, string strLgort, string strProgid, string strMblnr, string strIntype, string strInsmk, string strDate)
		{
			InitializeComponent();
            UserData = varUserData;
			Mandt = varUserData.Client;
			Werks = strWerks;
			Lgort = strLgort;
			Usrnm = varUserData.UserId;
			Progid = strProgid;
			Mblnr = strMblnr;
			Intype = strIntype;
			Insmk = strInsmk;
            Date = strDate;
            strComcd = varUserData.CompanyCode;

			try
			{
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), varUserData, Progid);

				//檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
				{
					MessageBox.Show("You don't have right to use this program!!");
					this.Close();
				}
				else
				{
					ShowSapData();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void ShowSapData()
        {
            try
            {
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtData = objSapData.ListSapData_MaterialConvert(Mblnr, Insmk, strDate);
                Mblnr = "";
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSapData()");
            }

        }

        private void ShowDataGrid()
        {
            try
            {
                dtgData.TableStyles.Clear();
                DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                mydtgTableStyle.MappingName = dtData.TableName;

                DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
                mblnrStyle.MappingName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 120;
                mblnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

                DataGridColumnStyle matnrStyle = new DataGridTextBoxColumn();
                matnrStyle.MappingName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(matnrStyle);

                DataGridColumnStyle insmkStyle = new DataGridTextBoxColumn();
                insmkStyle.MappingName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.Width = 90;
                insmkStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(insmkStyle);

                DataGridColumnStyle mengeStyle = new DataGridTextBoxColumn();
                mengeStyle.MappingName = "MENGE";
                mengeStyle.HeaderText = "Qty";
                mengeStyle.Width = 90;
                mengeStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(mengeStyle);

                dtgData.DataSource = dtData;
                dtgData.TableStyles.Add(mydtgTableStyle);

                dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void dtgData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                DataGrid dgClick = (DataGrid)sender;
                DataGrid.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGrid.HitTestType.RowHeader)
                {
                    this.btnSelect.Enabled = true;
                    intRowNo = hitRow.Row;
                    dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
                    Mblnr = dgClick[dgClick.CurrentCell].ToString();
                }
                else
                {
                    this.btnSelect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (Mblnr == "")
            {
                MessageBox.Show("Please select one document No!!");
                return;
            }
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
