using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_MixedMaterial : Form
    {

        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
        private MixedMaterial objMixedMaterial;
       // private AccessConfig objConfig;
        private ArrayList aryMrgid = new ArrayList();

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
        # endregion

        public Admin_MixedMaterial()
        {
            InitializeComponent();
           
        }

        public Admin_MixedMaterial(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;

            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                lblAddType.Text = "NEW";

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    //廠區資料
                    ShowDdlWerks();
                    dgvData.AutoGenerateColumns = false;//设置DataGridView不能自动生成列
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        # endregion

        # region 绑定Plant
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        # endregion

        # region Show DataGridView
        private void ShowDataGrid(string strMrgid)
        {
            DataTable dtTemp = new DataTable();
            DataRow objRow;
            DataRow[] foundRow;

            try
            {
                dtTemp.Columns.Add("WERKS", Type.GetType("System.String"));
                dtTemp.Columns.Add("MRGID", Type.GetType("System.String"));
                dtTemp.Columns.Add("MATNR", Type.GetType("System.String"));

                dgvData.Columns.Clear();
                foundRow = dtData.Select("MRGID='" + strMrgid + "'", "MATNR");
                for (int i = 0; i < foundRow.Length; i++)
                {
                    objRow = dtTemp.NewRow();
                    objRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                    objRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                    objRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                    dtTemp.Rows.Add(objRow);
                }

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed ID";
                dgvcMrgid.ReadOnly = true;
                dgvData.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMatnr);

                dgvData.DataSource = dtTemp;
                lblCount.Text = dtTemp.Rows.Count-1 + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void ShowAddDataGrid()
        {
            try
            {
                dgvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMatnr);                

                dgvData.DataSource = dtData;
                lblCount.Text = dgvData.Rows.Count + " records";
                btnDownload.Enabled = false;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowAddDataGrid()");
            }
        }
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                this.gbFunction.Enabled = true;
                this.gbPlant.Enabled = false;
                try
                {
                    objMixedMaterial = new MixedMaterial(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString());
                    ShowAllData();
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        # endregion

        # region ShowAllData
        private void ShowAllData()
        {
            stsWarning.Text = "";
            try
            {
                dtData = objMixedMaterial.QueryMixedMaterialTable();
                dgvData.AutoGenerateColumns = false;
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed ID";
                dgvcMrgid.ReadOnly = true;
                dgvData.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCrnam = new DataGridViewTextBoxColumn();
                dgvcCrnam.DataPropertyName = "CRNAM";
                dgvcCrnam.HeaderText = "Created By";
                dgvcCrnam.ReadOnly = true;
                dgvData.Columns.Add(dgvcCrnam);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Created Date";
                dgvcCrdat.ReadOnly = true;
                dgvData.Columns.Add(dgvcCrdat);

                dgvData.DataSource = dtData;
                lblCount.Text = dgvData.Rows.Count + " records";
                btnDownload.Enabled = true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowAllData()");
            }
        }
        # endregion

        # region RadioButton CheckedChange
        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.gbFunction.Enabled = false;
            this.gbHeader.Enabled = true;

        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.gbFunction.Enabled = false;
            this.btnAdd.Enabled = true;
        }
        # endregion

        # region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ArrayList aryMixedMaterial = new ArrayList();
            aryMrgid.Clear();
            string strTempMrgid = "";
            if (txtMatnr1.Text.Trim() != "")
                aryMixedMaterial.Add(txtMatnr1.Text.Trim());
            if (txtMatnr2.Text.Trim() != "")
                aryMixedMaterial.Add(txtMatnr2.Text.Trim());

            if (aryMixedMaterial.Count == 0)
            {
                stsWarning.Text = "Pleaset at least input one Part No!!";
                return;
            }
            else
            {
                try
                {
                    dtData = objMixedMaterial.QueryMixedMaterialTable(aryMixedMaterial);
                    //決定要不要將>>及<< Enable
                    if (dtData.Rows.Count > 0)
                    {
                        this.btnSave.Enabled = true;
                        this.gbHeader.Enabled = false;

                        //將Mrgid存到aryMrgid中
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (strTempMrgid.IndexOf(dtData.Rows[i]["MRGID"].ToString()) == -1)
                            {
                                strTempMrgid += dtData.Rows[i]["MRGID"].ToString();
                                aryMrgid.Add(dtData.Rows[i]["MRGID"].ToString());
                            }
                        }
                        if (aryMrgid.Count > 1)
                        {
                            this.btnNext.Enabled = true;
                        }
                        //DataGrid秀出第一個連板編號的資料
                        this.lblRowNo.Text = "0";
                        this.lblMrgid.Text = aryMrgid[Convert.ToInt32(lblRowNo.Text)].ToString();
                        ShowDataGrid(lblMrgid.Text);
                    }
                    else
                    {
                        stsWarning.Text = "No Data!!";
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        # endregion

        # region Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                Admin_MixedMaterial_Add objMixedAdd = new Admin_MixedMaterial_Add(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), lblAddType.Text);
                if (lblAddType.Text == "NEW")
                {
                    objMixedAdd.ShowDialog();
                    if (objMixedAdd.NewMixedMaterial != null)
                    {
                        dtData = objMixedAdd.NewMixedMaterial;
                        lblAddType.Text = objMixedAdd.AddType;
                        ShowAddDataGrid();
                        gbFunction.Enabled = false;
                        btnSave.Enabled = true;
                        return;
                    }
                }
                if (lblAddType.Text == "MODIFY")
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        switch (i.ToString())
                        {
                            case "0":
                                objMixedAdd.Matnr1 = dtData.Rows[0]["MATNR"].ToString();
                                break;
                            case "1":
                                objMixedAdd.Matnr2 = dtData.Rows[1]["MATNR"].ToString();
                                break;
                            case "2":
                                objMixedAdd.Matnr3 = dtData.Rows[2]["MATNR"].ToString();
                                break;
                            case "3":
                                objMixedAdd.Matnr4 = dtData.Rows[3]["MATNR"].ToString();
                                break;
                        }
                    }
                    objMixedAdd.SetMaterialData();
                    objMixedAdd.ShowDialog();
                    if (objMixedAdd.NewMixedMaterial != null)
                    {
                        dtData = objMixedAdd.NewMixedMaterial;
                        lblAddType.Text = objMixedAdd.AddType;
                        ShowAddDataGrid();
                        gbFunction.Enabled = false;
                        btnSave.Enabled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ArrayList aryMatnr = new ArrayList();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                aryMatnr.Add(dtData.Rows[i]["MATNR"].ToString());
            }
            try
            {

                //刪除
                if (rdoDelete.Checked == true)
                {

                    //檢查是否有庫存資料
                    if (objMixedMaterial.QueryMixedMaterialStorage(lblMrgid.Text))
                    {
                        stsWarning.Text = "The mixed material is used in the storage now and can't be deleted!!";
                        return;
                    }
                    //刪除資料
                    if (objAdmin.DeleteMixedMaterial(lblMrgid.Text))
                    {
                        stsWarning.Text = "Delete OK!";
                        dgvData.DataSource = null;
                        btnQuery_Click(sender, e);
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail!" + objAdmin.ERRMSG;
                    }
                }
                //新增
                if (rdoAdd.Checked == true)
                {
                    //檢查是不是有資料
                    if (dtData.Rows.Count < 2)
                    {
                        stsWarning.Text = "Please input more than two Part No!!";
                        return;
                    }
                    //檢查目前是否已經設定連板料號
                    if (objMixedMaterial.QueryMixedMaterialID(aryMatnr) != "")
                    {
                        stsWarning.Text = "The mixed materials had existed in the system!!";
                        return;
                    }
                    //新增資料
                    if (objAdmin.AddMixedMaterial(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), aryMatnr))
                    {
                        stsWarning.Text = "Add OK!";
                        this.txtMatnr1.Text = "";
                        this.txtMatnr2.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                this.cmbWerks.SelectedIndex = -1;
                this.gbPlant.Enabled = true;
                this.rdoAdd.Checked = false;
                this.rdoDelete.Checked = false;
                this.gbFunction.Enabled = false;
                this.txtMatnr1.Text = "";
                this.txtMatnr2.Text = "";
                this.gbHeader.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnSave.Enabled = false;
                this.stsWarning.Text = "";
                this.dgvData.DataSource = null;
                lblAddType.Text = "NEW";
                lblCount.Text = "";
                dtData.Clear();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        # endregion

        # region Previous<<
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                int intNowRow = Convert.ToInt32(lblRowNo.Text) - 1;
                if (intNowRow < 0)
                    intNowRow = 0;
                CheckNextPreviousStatus(intNowRow);
                //DataGrid秀出目前連板編號的資料
                this.lblRowNo.Text = Convert.ToString(intNowRow);
                this.lblMrgid.Text = aryMrgid[intNowRow].ToString();
                ShowDataGrid(lblMrgid.Text);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Next >>
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int intNowRow = Convert.ToInt32(lblRowNo.Text) + 1;
                if (intNowRow >= aryMrgid.Count)
                    intNowRow = aryMrgid.Count - 1;
                CheckNextPreviousStatus(intNowRow);
                //DataGrid秀出目前連板編號的資料
                this.lblRowNo.Text = Convert.ToString(intNowRow);
                this.lblMrgid.Text = aryMrgid[intNowRow].ToString();
                ShowDataGrid(lblMrgid.Text);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region CheckNextPreviousStatus
        private void CheckNextPreviousStatus(int intRow)
        {
            try
            {
                //檢查Next需不需要Enable
                if (intRow < aryMrgid.Count)
                    this.btnNext.Enabled = true;
                else
                    this.btnNext.Enabled = false;
                //檢查Previous需不需要Enable
                if (intRow >= 0)
                    this.btnPrevious.Enabled = true;
                else
                    this.btnNext.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckNextPreviousStatus()");
            }
        }
        # endregion

        # region 调整布局
        private void Admin_MixedMaterial_Resize(object sender, EventArgs e)
        {
            panel4.Size = new System.Drawing.Size((int)(this.Size.Width * 0.4), panel4.Size.Height);
            if (this.Size.Width - stsMandt.Width - this.stsUsrnm.Width - this.stsDate.Width - this.stsComcd.Width - 40 > 0)
            {
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsUsrnm.Width - this.stsDate.Width - this.stsComcd.Width - 40;
            }
        }
        # endregion

        # region Download
        private void btnDownload_Click(object sender, EventArgs e)
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
        # endregion

        # region Export File
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tMixed ID\tPart No\tCreate By\tCreated Date";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MRGID"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRNAM"].ToString() + "\t";
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
        # endregion


    }
}
