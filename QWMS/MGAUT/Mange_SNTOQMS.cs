using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using System.Runtime.InteropServices;
using System.Threading;


namespace QWMS
{
    public partial class Mange_SNTOQMS : Form
    {
        #region 初始化設定

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strCtbto = "";
        private string strRegon = "";
        private string strMachine = "";
        private string strPalid = "";
        private string strComcd = "";
        private string strSN = "";
        private string strBoxid = "";
        private StorageData objStorageData;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        string strCurrentBoxId = "";

        private int intFormIndex = 0;
        private bool AllowToClose = true;//設定能否關閉Form視窗
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();
        private DataTable dtSelect = new DataTable();
        private DataTable dtCombine = new DataTable();
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        #endregion

        #region 变数


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

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }

        public string SN
        {
            get { return strSN; }
            set { strSN = value; }
        }

        public string Boxid
        {
            get { return strBoxid; }
            set { strBoxid = value; }
        }

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }

        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
            }
        }

        #endregion

        public Mange_SNTOQMS(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {

                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        #region 清除任何使用中的資源。
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.lblCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtSn);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBoxid);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbLgort);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1093, 123);
            this.panel1.TabIndex = 0;
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(815, 84);
            this.txtSn.Name = "txtSn";
            this.txtSn.Size = new System.Drawing.Size(233, 23);
            this.txtSn.TabIndex = 7;
            this.txtSn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSn_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(664, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "SN";
            // 
            // txtBoxid
            // 
            this.txtBoxid.Location = new System.Drawing.Point(184, 78);
            this.txtBoxid.Name = "txtBoxid";
            this.txtBoxid.Size = new System.Drawing.Size(218, 23);
            this.txtBoxid.TabIndex = 5;
            this.txtBoxid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxid_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "BoxID";
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(815, 23);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 22);
            this.cmbLgort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(664, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(184, 18);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 22);
            this.cmbWerks.TabIndex = 1;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(73, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plant";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(3, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 379);
            this.panel2.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.Location = new System.Drawing.Point(9, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(1076, 373);
            this.dgvData.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Location = new System.Drawing.Point(3, 504);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1097, 102);
            this.panel3.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(707, 29);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 37);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(374, 29);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 37);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(66, 29);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 37);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 612);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1100, 21);
            this.stbStatus.TabIndex = 34;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(3, 102);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(70, 14);
            this.lblCount.TabIndex = 8;
            this.lblCount.Text = "0 Records";
            // 
            // Mange_SNTOQMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 633);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Mange_SNTOQMS";
            this.Text = "Mange_SNTOQMS";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region ShowDdlWerks
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
        # endregion

        # region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
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
                    cmbLgort.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            this.dgvData.AllowUserToAddRows = false;
            try
            {
                //DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                //dgvcSelect.DataPropertyName = "Select";
                //dgvcSelect.HeaderText = "Select";
                //dgvcSelect.Name = "Select";
                //dgvcSelect.Width = 40;
                //this.dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "LGORT";
                dgvcStorage.HeaderText = "Storage";
                dgvcStorage.Width = 60;
                dgvcStorage.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcStorage);

                DataGridViewTextBoxColumn dgvcLocation = new DataGridViewTextBoxColumn();
                dgvcLocation.DataPropertyName = "LOCAT";
                dgvcLocation.HeaderText = "Location";
                dgvcLocation.Width = 60;
                dgvcLocation.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocation);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.Width = 130;
                boxidStyle.Name = "BOXID";
                boxidStyle.ReadOnly = true;
                this.dgvData.Columns.Add(boxidStyle);

                DataGridViewTextBoxColumn SernoStyle = new DataGridViewTextBoxColumn();
                SernoStyle.DataPropertyName = "SERNO";
                SernoStyle.Name = "SERNO";
                SernoStyle.HeaderText = "SN";
                SernoStyle.ReadOnly = true;
                SernoStyle.Width = 150;
                this.dgvData.Columns.Add(SernoStyle);

                dgvData.DataSource = dtData;
                lblCount.Text = dgvData.Rows.Count.ToString() + " records";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region txtBoxid_KeyDown

        private void txtBoxid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    #region "檢查廠區倉別資料"

                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();


                    if (cmbWerks.SelectedIndex != -1)
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        strWerks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        strLgort = "";

                    if (Werks == "" || Lgort == "")
                    {

                        throw new Exception("Plant and storage can't be empty!!");
                    }

                    //取得Sttyp及Lotyp
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                    if (dtTemp.Rows.Count >= 1)
                    {
                        strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {

                        throw new Exception("Can't find the storage data!!");
                    }

                    #endregion

                    txtSn.Enabled = false;
                    string strBoxid = txtBoxid.Text.Trim();

                    if (strBoxid == "")
                    {
                        throw new Exception("Box ID. can't be Empty!!");
                    }

                    #region 防呆

                    objStorageData = new StorageData(UserData, Werks, Lgort);
                    DataTable dtBoxidData = objStorageData.GetBoxIDInfo(this.Mandt, this.Werks, this.Lgort, strBoxid);
                    if (dtBoxidData.Rows.Count <= 0)
                    {

                        this.txtBoxid.Focus();
                        this.txtBoxid.SelectAll();
                        throw new Exception(strBoxid.Trim() + " doesn't exist!!");
                    }
                    if (Mandt.ToString() != strMandt)
                    {

                        this.txtBoxid.Focus();
                        this.txtBoxid.SelectAll();
                        throw new Exception("Client " + Mandt.ToString() + " doesn't match!!");

                    }
                    #endregion

                    if (dtBoxidData.Rows.Count > 0)
                    {
                        #region 检查是否为首次刷入BOXID，首次刷对参数进行赋值
                        if (Data.Rows.Count > 0)
                        {
                            #region 检查是否重复刷入BOXID

                            DataRow[] drCheck = Data.Select(" BOXID = '" + strBoxid + "' ");
                            if (drCheck.Length > 0)
                            {
                                stsWarning.Text = "该BOXID已刷入，请确认";
                                return;
                            }

                            #endregion
                        }
                        else
                        {
                            #region 第一次刷入BOXID,展示刷BOXID号码信息
                            GetDefaultTable();

                            //DataColumn cSelect = new DataColumn("Select", typeof(bool));
                            //dtBoxidData.Columns.Add(cSelect);
                            DataRow drRow;
                            foreach (DataRow tmpRow in dtBoxidData.Rows)
                            {
                                drRow = Data.NewRow();
                                //drRow["Select"] = tmpRow["Select"];
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["LOCAT"] = tmpRow["LOCAT"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["SERNO"] = tmpRow["SERNO"].ToString();
                                drRow["DELFLG"] = tmpRow["DELFLG"].ToString();
                                drRow["DELDAT"] = tmpRow["DELDAT"].ToString();
                                drRow["LOADID"] = tmpRow["LOADID"].ToString();
                                drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["DELDOC"] = tmpRow["DELDOC"].ToString();
                                drRow["CRTDAT"] = tmpRow["CRTDAT"].ToString();
                                Data.Rows.Add(drRow);
                            }

                            ShowDataGrid();
                            this.btnSave.Enabled = true;
                            stsWarning.Text = "";
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "BOXID不存在，请确认！";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    this.txtBoxid.Text = "";
                    //SoundPlayer sp = new SoundPlayer(@"Sound\Fail.wav");
                    //sp.Play();
                    return;
                }
            }
        }
        #endregion

        #region txtSn_KeyDown
        private void txtSn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    #region "檢查廠區倉別資料"

                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();


                    if (cmbWerks.SelectedIndex != -1)
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        strWerks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        strLgort = "";

                    if (Werks == "" || Lgort == "")
                    {

                        throw new Exception("Plant and storage can't be empty!!");
                    }

                    //取得Sttyp及Lotyp
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                    if (dtTemp.Rows.Count >= 1)
                    {
                        strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {

                        throw new Exception("Can't find the storage data!!");
                    }

                    #endregion


                    string strSN = txtSn.Text.Trim();
                    txtBoxid.Enabled = false;

                    if (txtSn.Text.Trim() == "")
                    {
                        throw new Exception("SN can't be Empty!!");
                    }

                    #region 防呆
                    objStorageData = new StorageData(UserData, Werks, Lgort);
                    DataTable dtSNData = objStorageData.GetSNInfo(this.Mandt, this.Werks, this.Lgort, strSN);
                    if (dtSNData.Rows.Count <= 0)
                    {

                        this.txtSn.Focus();
                        this.txtSn.SelectAll();
                        throw new Exception(strSN.Trim() + " doesn't exist!!");
                    }
                    if (Mandt.ToString() != strMandt)
                    {

                        this.txtBoxid.Focus();
                        this.txtBoxid.SelectAll();
                        throw new Exception("Client " + Mandt.ToString() + " doesn't match!!");

                    }
                    #endregion

                    if (dtSNData.Rows.Count > 0)
                    {
                        #region 检查是否为首次刷入SN，首次刷对参数进行赋值
                        if (Data.Rows.Count > 0)
                        {
                            #region 检查是否重复刷入SN

                            DataRow[] drCheck = Data.Select(" SERNO = '" + strSN + "' ");
                            if (drCheck.Length > 0)
                            {
                                stsWarning.Text = "该SN已刷入，请确认";
                                return;
                            }

                            #endregion
                        }
                        else
                        {
                            #region 第一次刷入SN,展示刷SN号码信息
                            GetDefaultTable();

                            //DataColumn cSelect = new DataColumn("Select", typeof(bool));
                            //dtSNData.Columns.Add(cSelect);
                            DataRow drRow;
                            foreach (DataRow tmpRow in dtSNData.Rows)
                            {
                                drRow = Data.NewRow();
                                //drRow["Select"] = tmpRow["Select"];
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["LOCAT"] = tmpRow["LOCAT"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["SERNO"] = tmpRow["SERNO"].ToString();
                                drRow["DELFLG"] = tmpRow["DELFLG"].ToString();
                                drRow["DELDAT"] = tmpRow["DELDAT"].ToString();
                                drRow["LOADID"] = tmpRow["LOADID"].ToString();
                                drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["DELDOC"] = tmpRow["DELDOC"].ToString();
                                drRow["CRTDAT"] = tmpRow["CRTDAT"].ToString();
                                Data.Rows.Add(drRow);
                            }

                            ShowDataGrid();
                            this.btnSave.Enabled = true;
                            stsWarning.Text = "";
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "SN不存在，请确认！";
                        return;
                    }

                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    this.txtBoxid.Text = "";
                    //SoundPlayer sp = new SoundPlayer(@"Sound\Fail.wav");
                    //sp.Play();
                    return;
                }
            }
        }
        #endregion

        #region GetDefaultTable
        private void GetDefaultTable()
        {
            if (Data.Rows.Count == 0)
            {
                Data.Clear();
                Data = new DataTable();
                //Data.Columns.Add("Select", Type.GetType());
                Data.Columns.Add("MANDT", Type.GetType());
                Data.Columns.Add("COMCD", Type.GetType());
                Data.Columns.Add("WERKS", Type.GetType());
                Data.Columns.Add("LGORT", Type.GetType());
                Data.Columns.Add("LOCAT", Type.GetType());
                Data.Columns.Add("MBLNR", Type.GetType());
                Data.Columns.Add("BOXID", Type.GetType());
                Data.Columns.Add("INSMK", Type.GetType());
                Data.Columns.Add("CHARG", Type.GetType());
                Data.Columns.Add("MATNR", Type.GetType());
                Data.Columns.Add("SERNO", Type.GetType());
                Data.Columns.Add("DELFLG", Type.GetType());
                Data.Columns.Add("DELDAT", Type.GetType());
                Data.Columns.Add("LOADID", Type.GetType());
                Data.Columns.Add("KDMAT", Type.GetType());
                Data.Columns.Add("MENGE", Type.GetType());
                Data.Columns.Add("DELDOC", Type.GetType());
                Data.Columns.Add("CRTDAT", Type.GetType());
            }
        }
        #endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.strWerks = "";
            this.strSN = "";
            this.strBoxid = "";
            this.lblCount.Text = "0 records";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.strLgort = "";
            this.txtSn.Text = "";
            this.txtBoxid.Text = "";
            this.txtBoxid.Enabled = true;
            this.txtSn.Enabled = true;
            this.dgvData.DataSource = null;
            this.btnSave.Enabled = false;
            this.Data.Clear();
            this.dtData.Clear();
        }
        # endregion

        #region  Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";

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


            if (dgvData.Rows.Count == 0)
            {
                stsWarning.Text = "无数据";
                return;
            }

            //dtCombine = dtData.Select("Select=true ").CopyToDataTable();
            //if (dtCombine.Rows.Count <= 0)
            //{
            //    stsWarning.Text = "请选择维护的SN号";
            //    return;
            //}

            objStorageIn = new StorageIn(UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.AddSNData(dtData))
            {
                stsWarning.Text = "SN维护成功!";
                btnSave.Enabled = false;
                this.cmbWerks.Enabled = true;
                this.cmbLgort.Enabled = true;
                this.txtSn.Text = "";
                this.txtBoxid.Text = "";
                this.txtBoxid.Enabled = true;
                this.txtSn.Enabled = true;
                this.Data.Clear();
                this.dtData.Clear();
                this.dtCombine.Rows.Clear();
                this.dgvData.DataSource = null;

            }
            else
            {
                stsWarning.Text = "SN维护失败! " + objStorageIn.ERRMSG;
                return;
            }
        }

        #endregion















    }
}
