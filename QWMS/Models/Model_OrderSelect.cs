using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QCI_QWMS_Models;
using QWMS.Common;

namespace QWMS.Models
{
    public partial class Model_OrderSelect : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strDate = "";
        private string strChkArbpl = "";
        private string strSendID = "";
        private string  strMblnr="";
        private ArrayList aryMatnr = new ArrayList();
        private string strMatnr = "";
        private string strOuttype = "";
        private DataTable dtData = new DataTable();
        private DataTable dtSendID = new DataTable();
       private StorageOut objStorageOut;
		private ModelsData objModelsData;
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

        public ArrayList Matnrs
        {
            get
            {
                return aryMatnr;
            }
            set
            {
                aryMatnr = value;
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

        public string Outtype
        {
            get
            {
                return strOuttype;
            }
            set
            {
                strOuttype = value;
            }
        }
        public Model_OrderSelect( UserInfo varUserData, string varWerks, string varLgort, string varProgid , string varOuttype)
		{
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = varProgid;
            Werks = varWerks;
            Lgort = varLgort;
           // Mblnr = varMblnr;
            Outtype = varOuttype;
			strDate =  DateTime.Now.ToString("yyyy-MM-dd");

			try
			{        
                objModelsData = new ModelsData(UserData, Werks, Lgort,Progid);
                    ShowOrderData();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        	private void ShowOrderData()
		{
            string strQueryDate = "";
            if (chkDate.Checked)
                strQueryDate = strDate;
			try
			{
			    dtData = objModelsData.ListOrderData(this.txtQuery.Text.Trim(), strQueryDate, Outtype);

                DataColumn cSelect = new DataColumn("SELECTED", typeof(bool));
                dtData.Columns.Add(cSelect);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["SELECTED"] = false;
                }
				ShowDataGrid();
			}
			catch(Exception ex)
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

				DataGridBoolColumn selectStyle = new DataGridBoolColumn();
                selectStyle.MappingName = "SELECTED";
                selectStyle.HeaderText = "SELECT";
				selectStyle.Width = 40;
				((DataGridBoolColumn)selectStyle).AllowNull = false;
				mydtgTableStyle.GridColumnStyles.Add(selectStyle);

				DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
				mblnrStyle.MappingName = "MBLNR";
				mblnrStyle.HeaderText = "Document No";
				mblnrStyle.Width = 100;
				mblnrStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

				DataGridColumnStyle matnrStyle = new DataGridTextBoxColumn();
				matnrStyle.MappingName = "MATNR";
				matnrStyle.HeaderText = "模号";
				matnrStyle.Width = 90;
				matnrStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(matnrStyle);

                DataGridColumnStyle ItemNameStyle = new DataGridTextBoxColumn();
                ItemNameStyle.MappingName = "ItemName";
                ItemNameStyle.HeaderText = "ItemName";
                ItemNameStyle.Width = 200;
                ItemNameStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(ItemNameStyle);

                DataGridColumnStyle BUStyle = new DataGridTextBoxColumn();
                BUStyle.MappingName = "BU";
                BUStyle.HeaderText = "PU";
                BUStyle.Width = 50;
                BUStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(BUStyle);

                DataGridColumnStyle MachineStyle = new DataGridTextBoxColumn();
                MachineStyle.MappingName = "Machine";
                MachineStyle.HeaderText = "Machine";
                MachineStyle.Width = 50;
                MachineStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(MachineStyle);

                DataGridColumnStyle QuantityStyle = new DataGridTextBoxColumn();
                QuantityStyle.MappingName = "Quantity";
                QuantityStyle.HeaderText = "Quantity";
                QuantityStyle.Width = 50;
                QuantityStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(QuantityStyle);

                DataGridColumnStyle NweightStyle = new DataGridTextBoxColumn();
                NweightStyle.MappingName = "Nweight";
                NweightStyle.HeaderText = "Nweight";
                NweightStyle.Width = 50;
                NweightStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(NweightStyle);

                DataGridColumnStyle PoNoStyle = new DataGridTextBoxColumn();
                PoNoStyle.MappingName = "PoNo";
                PoNoStyle.HeaderText = "PoNo";
                PoNoStyle.Width = 100;
                PoNoStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(PoNoStyle);

                DataGridColumnStyle crdatStyle = new DataGridTextBoxColumn();
                crdatStyle.MappingName = "CRDAT";
                crdatStyle.HeaderText = "Create Date";
                crdatStyle.Width = 120;
                crdatStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(crdatStyle);

				dtgData.DataSource = dtData;	
				dtgData.TableStyles.Add(mydtgTableStyle);
				dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";

			    
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");  
			}
		}

            private void btnSelect_Click(object sender, EventArgs e)
            {
                Mblnr = "";
                int selectcount = 0;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtData.Rows[i]["SELECTED"]))
                    {
                        Mblnr +=objModelsData.GetModerOrderBYItems(dtData.Rows[i]["MBLNR"].ToString())+";";
                        selectcount++;
                        for (int j = 0; j < dtData.Rows.Count; j++)
                        {
                            if (Convert.ToBoolean(dtData.Rows[j]["SELECTED"]))
                            {
                                if (
                                    dtData.Rows[i]["MBLNR"].ToString()
                                        .Substring(0, dtData.Rows[i]["MBLNR"].ToString().Length - 3) !=
                                    dtData.Rows[j]["MBLNR"].ToString()
                                        .Substring(0, dtData.Rows[j]["MBLNR"].ToString().Length - 3))
                                {
                                    MessageBox.Show("只能选择同单号下的模具！");
                                    return;
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(Mblnr))
                {
                    MessageBox.Show("Please select one Document No!!");
                    return;
                }
                this.Close();
            }

            private void btnReturn_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void btnQuery_Click(object sender, EventArgs e)
            {
                string inDate = dtpOudat.Value.ToString("yyyyMMdd");
                dtData = objModelsData.ListOrderData(txtQuery.Text.ToString().Trim(), inDate, Outtype);
                DataColumn cSelect = new DataColumn("SELECTED", typeof(bool));
                dtData.Columns.Add(cSelect);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["SELECTED"] = false;
                }
                ShowDataGrid();
            }

            private void chkDate_CheckedChanged(object sender, EventArgs e)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (chkDate.Checked)
                    {
                        dtData.Rows[i]["SELECTED"] = true;
                    }
                    else
                    {
                        dtData.Rows[i]["SELECTED"] = false;
                    }
                    
                }
                ShowDataGrid();
            }
    }
}
