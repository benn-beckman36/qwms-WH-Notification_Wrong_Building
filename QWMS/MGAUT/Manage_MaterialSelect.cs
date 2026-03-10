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
	/// Manage_MaterialSelect 的摘要描述。
	/// </summary>
	public class Manage_MaterialSelect : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();

		private System.Windows.Forms.Button btnReturn;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.DataGrid dtgData;

		private string strMandt = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strProgid = "";
		private string strType = "";
		private string strMatnr = "";
		private string strTempMatnr = "";
		private DataTable dtData = new DataTable();
		//private SQLAccess objDB;
		private PlantData objPlantData;

		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;	

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

		public string TempMatnr
		{
			get
			{
				return strTempMatnr;
			}
			set
			{
				strTempMatnr = value;
			}
		}

		public Manage_MaterialSelect(UserInfo _UserData, string strProgid, string strWerks,string strLgort, string strType, string strTempMatnr)
		{
            UserData = _UserData;
            
			InitializeComponent();
			Mandt = UserData.Client;
			Usrnm = UserData.UserId;
			Progid = strProgid;
			Werks = strWerks;
			Lgort = strLgort;
			Type = strType;
			TempMatnr = strTempMatnr;

			try
			{
				//AccessConfig objConfig = new AccessConfig(Mandt, "QWMS.xml");
				//objDB = new SQLAccess(objConfig.DBServer, objConfig.UserID, objConfig.Password, objConfig.InitialCatalog);
				objPlantData = new PlantData(UserData);
				ShowMatnrData();
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
			this.btnReturn.Location = new System.Drawing.Point(328, 294);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(64, 32);
			this.btnReturn.TabIndex = 5;
			this.btnReturn.Text = "Return";
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Enabled = false;
			this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSelect.Location = new System.Drawing.Point(7, 294);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(64, 32);
			this.btnSelect.TabIndex = 4;
			this.btnSelect.Text = "Select";
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// dtgData
			// 
			this.dtgData.DataMember = "";
			this.dtgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtgData.Location = new System.Drawing.Point(7, 14);
			this.dtgData.Name = "dtgData";
			this.dtgData.Size = new System.Drawing.Size(385, 272);
			this.dtgData.TabIndex = 3;
			this.dtgData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgData_MouseDown);
			// 
			// Manage_MaterialSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(400, 341);
			this.Controls.Add(this.btnReturn);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.dtgData);
			this.Name = "Manage_MaterialSelect";
			this.Text = "Material Select";
			((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ShowMatnrData()
		{
			try
			{
				dtData = objPlantData.QueryPartData(Werks, Lgort, strTempMatnr, Type);
				ShowDataGrid();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowMatnrData()");
			}

		}

		private void ShowDataGrid()
		{			
			try
			{
				dtgData.TableStyles.Clear();
				DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
				mydtgTableStyle.MappingName = dtData.TableName;

				DataGridColumnStyle matnrStyle = new DataGridTextBoxColumn();
				matnrStyle.MappingName = "MATNR";
				matnrStyle.HeaderText = "Part No";
				matnrStyle.Width = 90;
				matnrStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(matnrStyle);

				DataGridColumnStyle maktxStyle = new DataGridTextBoxColumn();
				maktxStyle.MappingName = "MAKTX";
				maktxStyle.HeaderText = "Description";
				maktxStyle.Width = 250;
				maktxStyle.ReadOnly = true;
				mydtgTableStyle.GridColumnStyles.Add(maktxStyle);
						
				dtgData.DataSource = dtData;	
				dtgData.TableStyles.Add(mydtgTableStyle);

				dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
		}

		private void btnSelect_Click(object sender, System.EventArgs e)
		{
			if(Matnr == "")
			{
				MessageBox.Show("Please select one material!!");
				return;
			}
			this.Close();
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void dtgData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				int intRowNo;
				DataGrid dgClick = (DataGrid)sender;
				DataGrid.HitTestInfo hitRow;
				hitRow = dgClick.HitTest(e.X, e.Y);
				if(hitRow.Type == DataGrid.HitTestType.RowHeader)
				{
					this.btnSelect.Enabled = true;
					intRowNo = hitRow.Row;
					dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
					Matnr = dgClick[dgClick.CurrentCell].ToString(); 
				}
				else
				{
					Matnr = "";
				}
				if(Matnr != "")
				{
					this.btnSelect.Enabled = true;
				}
				else
				{
					this.btnSelect.Enabled = false;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

	}
}
