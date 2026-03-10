using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
	/// <summary>
	/// StorageOut_SapDataSelectLocation 的摘要描述。
	/// </summary>
	public class StorageOut_SapDataSelectLocation : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnReturn;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.DataGrid dtgData;
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strProgid = "";
		private string strMblnr = "";
		private string strZeile = "";
		private string strMatnr = "";
		private string strInsmk = "";
		private string strCharg = "";
		private string strAlqty = "";
		private ArrayList aryMblnr = new ArrayList();
		private ArrayList aryZeile = new ArrayList();
		private ArrayList aryAlqty = new ArrayList();
		private DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();

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

		public string Zeile
		{
			get
			{
				return strZeile;
			}
			set
			{
				strZeile = value;
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

		public string Charg
		{
			get
			{
				return strCharg;
			}
			set
			{
				strCharg = value;
			}
		}

		public string Alqty
		{
			get
			{
				return strAlqty;
			}
			set
			{
				strAlqty = value;
			}
		}

		public StorageOut_SapDataSelectLocation(UserInfo varUserData, string strWerks, string strLgort, string strProgid, string strMatnr, string strInsmk, string strCharg)
		{
			InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
			Werks = strWerks;
			Lgort = strLgort;
			Progid = strProgid;
			Matnr = strMatnr;
			Insmk = strInsmk;
			Charg = strCharg;

			try
			{
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
				//檢查權限
				if(!objStorageOut.CheckAuthority())
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

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form 設計工具產生的程式碼
		/// <summary>
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnReturn = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.dtgData = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
			this.SuspendLayout();
			// 
			// btnReturn
			// 
			this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnReturn.Location = new System.Drawing.Point(224, 360);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(75, 32);
			this.btnReturn.TabIndex = 5;
			this.btnReturn.Text = "Return";
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSelect.Location = new System.Drawing.Point(22, 358);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 32);
			this.btnSelect.TabIndex = 4;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// dtgData
			// 
			this.dtgData.AllowSorting = false;
			this.dtgData.DataMember = "";
			this.dtgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtgData.Location = new System.Drawing.Point(6, 14);
			this.dtgData.Name = "dtgData";
			this.dtgData.Size = new System.Drawing.Size(330, 336);
			this.dtgData.TabIndex = 3;
			// 
			// StorageOut_SapDataSelectLocation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(344, 405);
			this.Controls.Add(this.btnReturn);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.dtgData);
			this.Name = "StorageOut_SapDataSelectLocation";
			this.Text = "SAP Data";
			((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ShowSapData()
		{
			try
			{
                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
				dtData = objSapData.ListSapLocationOutData(strMatnr, strInsmk, strCharg);
				DataColumn cSelect = new DataColumn("Select", typeof(bool));
				dtData.Columns.Add(cSelect);
				for(int i=0;i<dtData.Rows.Count;i++)
				{
					dtData.Rows[i]["Select"] = false;
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
				selectStyle.MappingName = "Select";
				selectStyle.HeaderText = "Select";
				selectStyle.Width = 40;
				((DataGridBoolColumn)selectStyle).AllowNull = false;
				mydtgTableStyle.GridColumnStyles.Add(selectStyle);

				DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
				mblnrStyle.MappingName = "MBLNR";
				mblnrStyle.HeaderText = "Document No";
				mblnrStyle.Width = 100;
				mblnrStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

				DataGridColumnStyle zeileStyle = new DataGridTextBoxColumn();
				zeileStyle.MappingName = "ZEILE";
				zeileStyle.HeaderText = "Item No.";
				zeileStyle.Width = 100;
				zeileStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(zeileStyle);

				DataGridColumnStyle balanceStyle = new DataGridTextBoxColumn();
				balanceStyle.MappingName = "BALANCE";
				balanceStyle.HeaderText = "Balance Qty";
				balanceStyle.Width = 80;
				balanceStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(balanceStyle);
						
				dtgData.DataSource = dtData;	
				dtgData.TableStyles.Add(mydtgTableStyle);

				dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
		}

		public ArrayList GetSelectedRows(DataGrid dg)  
		{  
			try
			{
				ArrayList aryTemp = new ArrayList();
				CurrencyManager cm = (CurrencyManager)this.BindingContext[dg.DataSource, dg.DataMember]; 
				DataView dv = (DataView)cm.List; 

				for(int i = 0; i < dv.Count; ++i) 
				{ 
					if(dg.IsSelected(i)) 
					{
						aryTemp.Add(dv.Table.Rows[i]["MBLNR"].ToString());
					}
				} 
				return aryTemp;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-GetSelectRows()");
			}
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			aryMblnr.Clear();
			aryZeile.Clear();
			aryAlqty.Clear();
			for(int i=0;i<dtData.Rows.Count;i++)
			{
				if(Convert.ToBoolean(dtData.Rows[i]["Select"]))
				{
					aryMblnr.Add(dtData.Rows[i]["MBLNR"]);
					aryZeile.Add(dtData.Rows[i]["ZEILE"]);
					aryAlqty.Add(dtData.Rows[i]["BALANCE"].ToString().Trim());
				}
			}
			if(aryMblnr.Count > 1)
			{
				MessageBox.Show("You can only select one Document No!!");
				return;
			}
			if(aryMblnr.Count == 0)
			{
				MessageBox.Show("Please at least select one Document No!!");
				return;
			}
			if(aryMblnr.Count == 1)
			{
				strMblnr = aryMblnr[0].ToString();
				strZeile = aryZeile[0].ToString();
				strAlqty = aryAlqty[0].ToString();

			}
			this.Close();
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
