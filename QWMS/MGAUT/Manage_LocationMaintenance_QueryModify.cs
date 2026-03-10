using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using QCI.QWMS;
using QWMS.Common;


namespace QWMS
{
	/// <summary>
	/// Manage_LocationMaintenance_QueryModify 的摘要描述。
	/// </summary>
	public class Manage_LocationMaintenance_QueryModify : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtMatnr;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnReturn;

		private string strMandt = "";
		private string strUsrnm = "";
		private string strProgid = "";
		private string strMrgid = "";
		private string strMatnr = "";
		private string strCharg = "";
		private DataRow drRowFound;
		private object[] objFind = new object[2];
		private DataTable dtMixedData = new DataTable();
		private DataTable dtTemp = new DataTable();
		private StorageIn objStorageIn;
		private System.Windows.Forms.TextBox txtCharg;


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

		public string Mrgid
		{
			get
			{
				return strMrgid;
			}
			set
			{
				strMrgid = value;
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

		public DataTable MixedData
		{
			get
			{
				return dtMixedData;
			}
			set
			{
				dtMixedData = value;
			}
		}

		public Manage_LocationMaintenance_QueryModify(string strMandt, string strUsrnm, string strProgid, string strMrgid, string strMatnr, DataTable dtMixedData)
		{
			InitializeComponent();

			DataRow drRow;
			DataRow[] foundRow;
			Mandt = strMandt;
			Usrnm = strUsrnm;
			Progid = strProgid;
			Matnr = strMatnr;
			Mrgid = strMrgid;
			MixedData = dtMixedData;

			try
			{
				//objConfig = new AccessConfig(Mandt, "QWMS.xml");
				//objDB = new SQLAccess(objConfig.DBServer, objConfig.UserID, objConfig.Password, objConfig.InitialCatalog);
				objStorageIn = new StorageIn(UserData, Progid);

				//檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					dtTemp.Rows.Clear();
					DataColumn[] dcPrimaryKey = new DataColumn[2];
					dcPrimaryKey[0] = MixedData.Columns["MRGID"];
					dcPrimaryKey[1] = MixedData.Columns["MATNR"];
					MixedData.PrimaryKey = dcPrimaryKey;

					if(Matnr != "" && MixedData.Rows.Count > 0)
					{
						dtTemp = MixedData.Clone();
						foundRow = MixedData.Select("MRGID='"+ Mrgid +"' and MATNR='"+ Matnr +"'");
						
						for(int i=0;i<foundRow.Length;i++)
						{
							drRow = dtTemp.NewRow();
							drRow["WERKS"] = foundRow[0]["WERKS"].ToString();
							drRow["MRGID"] = foundRow[0]["MRGID"].ToString();
							drRow["MATNR"] = foundRow[0]["MATNR"].ToString();
							drRow["CHARG"] = foundRow[0]["CHARG"].ToString();
							dtTemp.Rows.Add(drRow);
						}
					}

					this.txtMatnr.Enabled = false;
					this.txtMatnr.Text = dtTemp.Rows[0]["MATNR"].ToString();
					this.txtCharg.Text = dtTemp.Rows[0]["CHARG"].ToString();

					objFind[0] = Mrgid;
					objFind[1] = Matnr;
					drRowFound = MixedData.Rows.Find(objFind);
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
			this.btnConfirm = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtCharg = new System.Windows.Forms.TextBox();
			this.txtMatnr = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnReturn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnConfirm
			// 
			this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnConfirm.Location = new System.Drawing.Point(26, 104);
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.Size = new System.Drawing.Size(75, 32);
			this.btnConfirm.TabIndex = 22;
			this.btnConfirm.Text = "Confirm";
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtCharg);
			this.groupBox1.Controls.Add(this.txtMatnr);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(2, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 96);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			// 
			// txtCharg
			// 
			this.txtCharg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtCharg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCharg.Location = new System.Drawing.Point(128, 56);
			this.txtCharg.Name = "txtCharg";
			this.txtCharg.Size = new System.Drawing.Size(128, 22);
			this.txtCharg.TabIndex = 0;
			this.txtCharg.Text = "";
			// 
			// txtMatnr
			// 
			this.txtMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtMatnr.Enabled = false;
			this.txtMatnr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtMatnr.Location = new System.Drawing.Point(128, 24);
			this.txtMatnr.Name = "txtMatnr";
			this.txtMatnr.Size = new System.Drawing.Size(128, 22);
			this.txtMatnr.TabIndex = 22;
			this.txtMatnr.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 23);
			this.label7.TabIndex = 17;
			this.label7.Text = "Version";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 23);
			this.label4.TabIndex = 14;
			this.label4.Text = "Part No";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnReturn
			// 
			this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnReturn.Location = new System.Drawing.Point(186, 104);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(75, 32);
			this.btnReturn.TabIndex = 23;
			this.btnReturn.Text = "Return";
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			// 
			// Manage_LocationMaintenance_QueryModify
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(292, 141);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnReturn);
			this.Name = "Manage_LocationMaintenance_QueryModify";
			this.Text = "Part No Edit";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			try
			{					
				drRowFound["CHARG"] = this.txtCharg.Text.Trim();
				drRowFound.AcceptChanges();

				this.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
