using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QWMS.Common;
using QCI.QWMS;
using System.Text;
using QWMS.Entity;
using Microsoft.VisualBasic;

namespace QWMS
{
	/// <summary>
	/// Summary description for StorageIn_SMT_Query_RefID.
	/// </summary>
	public class StorageIn_SMT_Query_RefID : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnReturn;
        private DataGridView dgvData;
        private Button btnSave;
		private System.ComponentModel.Container components = null;
        private DataTable dtRefID = new DataTable();
        private string strMandt = "";
        private string strUsrnm = "";
        private Button btnSelect;
        private string strComcd = "";
        private string strRefID = "";
        private string strType = "";
        public string RefID
        {
            get
            {
                return strRefID;
            }
            set
            {
                strRefID = value;
            }
        }

        public StorageIn_SMT_Query_RefID(DataTable varRefID, DataTable dtScaned, string varType, UserInfo varUserData)
		{
			InitializeComponent();
            dtRefID = varRefID;
            strType = varType;
            if (strType == "SMT")
            {
                //╭SMT Reference ID戈
                ShowDataGrid(dtRefID);
            }
            else if (strType == "Pallet")
            {
                UserData = varUserData;
                strMandt = UserData.Client;//Client
                strUsrnm = UserData.UserId;//ㄏノ祅眀腹
                strComcd = varUserData.CompanyCode;//そ

                //╭Θ珇笆畐Reference ID戈
                ShowRefidDataGrid(dtRefID);
            }
		}

        public void ShowScaned(DataTable dtRefID, DataTable dtScaned)
        {
            string strTmp = "";
            for (int i = 0; i < dtScaned.Rows.Count; i++)
            {
                strTmp += dtScaned.Rows[i]["MBLNR"].ToString() + ";";
            }
            string strDidno2 = "";
            for (int i = 0; i < dgvData.Rows.Count-1; i++)
            {
                try
                {
                    strDidno2 = this.dgvData.Rows[i].Cells[1].Value.ToString();
                    if (strTmp.IndexOf(strDidno2) != -1)
                    {
                        dgvData.Rows[i].Cells[1].OwningRow.DataGridView.RowsDefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region ShowDataGrid
        public void ShowDataGrid(DataTable dtRefID)
		{
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
			try
			{
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Reference ID.";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 150;
                dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "DIDNO";
                dgvcMblnr.HeaderText = "DID No.";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 160;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 50;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 50;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcOtlgt = new DataGridViewTextBoxColumn();
                dgvcOtlgt.DataPropertyName = "OTLGT";
                dgvcOtlgt.HeaderText = "Storage From";
                dgvcOtlgt.ReadOnly = true;
                dgvcOtlgt.Width = 50;
                dgvData.Columns.Add(dgvcOtlgt);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage TO";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcModat = new DataGridViewTextBoxColumn();
                dgvcModat.DataPropertyName = "MODAT";
                dgvcModat.HeaderText = "Modify Date";
                dgvcModat.ReadOnly = true;
                dgvcModat.Width = 50;
                dgvData.Columns.Add(dgvcModat);

                dgvData.DataSource = dtRefID;	
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
        }
        #endregion

        #region ShowRefidDataGrid
        public void ShowRefidDataGrid(DataTable dtRefID)
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Reference ID.";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 150;
                dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Create Date";
                dgvcCrdat.ReadOnly = true;
                dgvcCrdat.Width = 150;
                dgvData.Columns.Add(dgvcCrdat);

                DataGridViewTextBoxColumn dgvcRemak1 = new DataGridViewTextBoxColumn();
                dgvcRemak1.DataPropertyName = "REMAK1";
                dgvcRemak1.HeaderText = "Remark";
                dgvcRemak1.ReadOnly = false;//REMAK1逆砞﹚絪胯篈
                dgvcRemak1.Width = 150;
                dgvData.Columns.Add(dgvcRemak1);

                dgvData.DataSource = dtRefID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowRefidDataGrid()");
            }
        }
        #endregion


        /// <summary>
		/// Clean up any resources being used.
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 374);
            this.panel1.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Location = new System.Drawing.Point(267, 339);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 29);
            this.btnSelect.TabIndex = 20;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(143, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 29);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(23, 41);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(427, 292);
            this.dgvData.TabIndex = 17;
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(23, 340);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(91, 29);
            this.btnReturn.TabIndex = 16;
            this.btnReturn.Text = "Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "RefID Data From QSMS";
            // 
            // StorageIn_SMT_Query_RefID
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(475, 374);
            this.Controls.Add(this.panel1);
            this.Name = "StorageIn_SMT_Query_RefID";
            this.Text = "StorageIn_SMT_Query_RefID";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList alRefid = new ArrayList();
            ArrayList alRemak1 = new ArrayList();

            #region ㄏノInputBox盞絏喷靡

            string Message = "叫块爹ID盞絏: ";//砞﹚矗ボ癟
            string Title = "盞絏喷靡";//砞﹚Title
            string DefaultValue = "";//砞﹚箇砞
            string ReturnPassword = "";//眔ㄏノ块喷靡盞絏
            bool Confirm = false;//琌絋粄秙

            //琩高琌Τ爹ID舦: WHUSR.ISADM = 'Y'
            string strIsAdm = QueryModifyAuthority(strUsrnm);

            if (strIsAdm.ToUpper() == "Y")
            {
                ReturnPassword = Microsoft.VisualBasic.Interaction.InputBox(Message, Title, DefaultValue, 50, 50);
                if (ReturnPassword != "")
                {
                    Confirm = true;
                    switch (strUsrnm)
                    {
                        #region F7紅跋爹ID盞絏
                        case "Justin":
                            if (ReturnPassword == "670342")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        case "F705129085":
                            if (ReturnPassword == "670321")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        case "F704100395":
                            if (ReturnPassword == "670315")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        case "QNX":
                            if (ReturnPassword == "670333")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        #endregion
                        #region F1紅跋爹ID盞絏
                        case "F108101557":
                            if (ReturnPassword == "640406")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        case "QDM":
                            if (ReturnPassword == "612560")
                            {
                                break;
                            }
                            else
                            {
                                PasswordInvalidAlert(Confirm);
                                return;
                            }
                        #endregion
                    }
                }
                else
                {
                    Confirm = false;
                    PasswordInvalidAlert(Confirm);
                    return;
                }
            }
            else
            {
                MessageBox.Show("眤眀腹⊿Τ爹ID舦叫絋粄!!", "Modify Error");
                return;
            }

            #endregion

            for (int i=0; i< dgvData.Rows.Count; i++)
            {
                if (dgvData.Rows[i].Cells[2].FormattedValue != "")
                {
                    alRefid.Add(dgvData.Rows[i].Cells[0].Value.ToString());
                    alRemak1.Add(dgvData.Rows[i].Cells[2].Value.ToString());
                }
            }

            SapData objSapData = new SapData(UserData, dtRefID.Rows[0]["WERKS"].ToString(), dtRefID.Rows[0]["LGORT"].ToString());
            if (objSapData.UpdateRefIDRemak1(alRefid, alRemak1))
            {
                MessageBox.Show("Reference ID爹Θ!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.btnSave.Enabled = false;
            }
        }

        public void PasswordInvalidAlert(bool Confirm)
        {
            if (Confirm)
            {
                MessageBox.Show("块盞絏Τ粇!!Reference ID﹟ゼ爹Θ叫穝絋粄!!", "Password Invalid");
            }
            else
            {
                MessageBox.Show("叫块爹ID盞絏!!", "Password Error");
            }
        }

        public string QueryModifyAuthority(string strUsrnm)
        {
            DataWhusr objDataWhusr = new DataWhusr(UserData);
            ArrayList alColumns = new ArrayList();
            ArrayList alCondition = new ArrayList();
            DataTable dtTemp = new DataTable();
            string strReturn = "";

            alColumns.Add("ISADM");
            alCondition.Add("MANDT='" + strMandt.Trim() + "'");
            alCondition.Add("COMCD='" + strComcd.Trim() + "'");
            alCondition.Add("USRNM='" + strUsrnm + "'");

            dtTemp = objDataWhusr.EntityQuery(alColumns, alCondition, false,true);

            strReturn = dtTemp.Rows[0]["ISADM"].ToString();//是否管理者
            return strReturn;
        }

        private void dgvData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (strType == "Pallet")
                {
                    btnSelect.Enabled = true;
                    int intRowNo;
                    DataGridView dgClick = (DataGridView)sender;
                    DataGridView.HitTestInfo hitRow = dgClick.HitTest(e.X, e.Y);
                    if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                    {
                        this.btnSelect.Enabled = true;
                        intRowNo = hitRow.RowY;
                        dgClick.CurrentCell = dgClick.Rows[hitRow.RowIndex].Cells[hitRow.ColumnIndex + 1];
                        RefID = dgClick.CurrentCell.FormattedValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("请选择一行");
                        this.btnSelect.Enabled = false;
                        return;
                    }
                }
                else
                {
                    btnSelect.Enabled = false;
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
            this.Close();
        }
	}
}
