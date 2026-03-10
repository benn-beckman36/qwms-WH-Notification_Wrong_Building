using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_StorageDefine_JIT : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strStset = "";
        private string strType = "";
        private string strRemark = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;

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
                return txtLgort.Text.Trim();
            }
            set
            {
                txtLgort.Text = value;
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

        public string Stset
        {
            get
            {
                return strStset;
            }
            set
            {
                strStset = value;
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

        #region 建構函數
        public Admin_StorageDefine_JIT(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    // 绑定下拉選單
                    ShowDdlWerks();
                    ShowDdlSttyp();
                    ShowDdlLotyp();

                    dtData = objPlantData.GetPlantStorageData("LGORT", strWerks, Lgort);
                    ShowDataGridView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public Admin_StorageDefine_JIT()
        {
            InitializeComponent();
        }

        # region 顯示狀態列
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        #region 绑定廠區
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp = objPlantData.GetDdlWerksData();
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
        #endregion

        #region 绑定倉別類型
        private void ShowDdlSttyp()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbSttyp.Items.Clear();
                dtTemp = objPlantData.GetDdlSttyp();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbSttyp.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlSttyp()");
            }
        }
        #endregion

        #region 绑定儲位類型
        private void ShowDdlLotyp()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbLotyp.Items.Clear();
                dtTemp = objPlantData.GetDdlLotyp();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbLotyp.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLotyp()");
            }
        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcPlant = new DataGridViewTextBoxColumn();
                dgvcPlant.DataPropertyName = "CTRLNM";
                dgvcPlant.HeaderText = "Plant";
                dgvcPlant.ReadOnly = true;
                this.gvData.Columns.Add(dgvcPlant);

                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "CTRLC2";
                dgvcStorage.HeaderText = "Storage";
                dgvcStorage.ReadOnly = true;
                this.gvData.Columns.Add(dgvcStorage);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "CTRLC4";
                dgvcType.HeaderText = "Type";
                dgvcType.ReadOnly = true;
                this.gvData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcLocation = new DataGridViewTextBoxColumn();
                dgvcLocation.DataPropertyName = "CTRLC5";
                dgvcLocation.HeaderText = "Location Type";
                dgvcLocation.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLocation);

                DataGridViewTextBoxColumn dgvcCrane = new DataGridViewTextBoxColumn();
                dgvcCrane.DataPropertyName = "STSET";
                dgvcCrane.HeaderText = "Crane";
                dgvcCrane.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCrane);

                DataGridViewTextBoxColumn dgvcHeight = new DataGridViewTextBoxColumn();
                dgvcHeight.DataPropertyName = "STHGH";
                dgvcHeight.HeaderText = "Height(Level)";
                dgvcHeight.ReadOnly = true;
                this.gvData.Columns.Add(dgvcHeight);

                DataGridViewTextBoxColumn dgvcLength = new DataGridViewTextBoxColumn();
                dgvcLength.DataPropertyName = "STLEN";
                dgvcLength.HeaderText = "Length";
                dgvcLength.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLength);

                DataGridViewTextBoxColumn dgvcWidth = new DataGridViewTextBoxColumn();
                dgvcWidth.DataPropertyName = "STWID";
                dgvcWidth.HeaderText = "Width(Depth)";
                dgvcWidth.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWidth);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "REMAK";
                dgvcRemark.HeaderText = "JIT Procedure";
                dgvcRemark.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRemark);

                this.gvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }

        }
        #endregion

        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.panel5.Enabled = true;
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.gbTail.Enabled = true;
            this.cmbSttyp.Enabled = false;
            this.cmbLotyp.Enabled = false;
            this.cmbWerks.Enabled = true;
            this.txtLgort.Enabled = true;
            this.btnSave.Enabled = true;
            this.strType = "DELETE";
            this.btnConfirm.Enabled = false;
        }
        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.panel5.Enabled = true;
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.gbTail.Enabled = true;
            this.cmbSttyp.Enabled = true;
            this.cmbLotyp.Enabled = true;
            this.cmbWerks.Enabled = true;
            this.txtLgort.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.strType = "ADD";
            this.gvData.Enabled = true;
        }
        private void rdoModify_CheckedChanged(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.panel5.Enabled = true;
            this.gbFunction.Enabled = false;
            this.gbMiddle.Enabled = true;
            this.gbTail.Enabled = true;
            this.cmbSttyp.Enabled = false;
            this.cmbLotyp.Enabled = false;
            this.cmbWerks.Enabled = true;
            this.txtLgort.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.strType = "MODIFY";
            this.gvData.Enabled = true;
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                dtData = objPlantData.GetPlantStorageData("LGORT", strWerks, Lgort);
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.rdoDelete.Checked = false;
                this.rdoAdd.Checked = false;
                this.rdoModify.Checked = false;
                this.gbFunction.Enabled = true;
                this.panel4.Enabled = false;
                this.panel5.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnAdd.Enabled = false;
                this.txtLgort.Text = "";
                this.stsWarning.Text = "";
                cmbWerks.SelectedIndex = -1;
                cmbSttyp.SelectedIndex = -1;
                cmbLotyp.SelectedIndex = -1;
                cmbSttyp.Enabled = true;
                cmbLotyp.Enabled = true;
                this.gvData.Enabled = true;
                strType = "";
                dtData = objPlantData.GetPlantStorageData("LGORT", strWerks, Lgort);
                ShowDataGridView();

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (rdoDelete.Checked == true)
                {
                    if (this.txtLgort.Text.Trim() == "" || cmbWerks.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    if (!objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), ""))
                    {
                        stsWarning.Text = "The storage doesn't exist in the system!!";
                        return;
                    }
                    if (!objAuthority.CheckLgortAuthority(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim()))
                    {
                        stsWarning.Text = "You don't have the right to modify the storage!!";
                        return;
                    }
                    if (objPlantData.CheckStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), "", ""))
                    {
                        stsWarning.Text = "The storage has storage now and can't be deleted!!";
                        return;
                    }
                    if (objAdmin.DeleteStorage(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), Progid, UserData))
                    {
                        stsWarning.Text = "Delete OK!";
                        this.txtLgort.Text = "";
                        this.strType = "";
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail!" + objAdmin.ERRMSG;
                    }
                }
                if (rdoAdd.Checked == true)
                {

                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "Please at least add one data in the table!!";
                        return;
                    }
                    if (objAdmin.AddStorage_JIT(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString(), cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString(), dtData))
                    {
                        stsWarning.Text = "Add OK!";
                        this.btnAdd.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.strType = "";
                    }
                    else
                    {
                        stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                        return;
                    }
                }
                dtData = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), "");
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                // ADD
                if (rdoAdd.Checked == true)
                {
                    if (cmbWerks.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant can't be empty!!";
                        return;
                    }
                    if (cmbSttyp.SelectedIndex == -1 || cmbLotyp.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Storage type and location type can't be empty!!";
                        return;
                    }
                    if (this.txtLgort.Text.Trim() == "" || this.txtLgort.Text.Trim().Length != 4)
                    {
                        stsWarning.Text = "Storage can't be empty and the length should be 4 chars!!";
                        return;
                    }
                    if (objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), ""))
                    {
                        stsWarning.Text = "The storage has existed in the system!!";
                        return;
                    }

                    dtData.Rows.Clear();
                    ShowDataGridView();
                    this.panel4.Enabled = false;
                    this.panel5.Enabled = false;
                    btnAdd.Enabled = true;
                    btnSave.Enabled = true;
                }
                // Modify
                if (rdoModify.Checked == true)
                {

                    if (cmbWerks.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant can't be empty!!";
                        return;
                    }

                    if (Lgort == "" || Lgort.Length != 4)
                    {
                        stsWarning.Text = "Storage can't be empty and the length should be 4 chars!!";
                        return;
                    }

                    if (!objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, ""))
                    {
                        stsWarning.Text = "The storage doesn't exist in the system!!";
                        return;
                    }

                    if (!objAuthority.CheckLgortAuthority(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim()))
                    {
                        stsWarning.Text = "You don't have the right to modify the storage!!";
                        return;
                    }
                    this.panel4.Enabled = false;
                    this.panel5.Enabled = false;
                    btnAdd.Enabled = false;
                    btnSave.Enabled = false;
                    if (cmbWerks.SelectedIndex != -1)
                    {
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    }
                    dtData = objPlantData.GetPlantStorageData("LGORT", strWerks, Lgort);
                    ShowDataGridView();
                    if (objPlantData.CheckStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, "", "") && dtData.Rows[0]["CTRLC4"].ToString().ToUpper() == "TRADITIONAL")
                    {
                        if (DialogResult.No == MessageBox.Show("If you modify the storage, you have to re-maintain the mapping data in the storage layout!!", "Warning", MessageBoxButtons.YesNo))
                        {
                            btnAdd.Enabled = false;
                            btnSave.Enabled = false;
                            this.gvData.Enabled = false;
                            return;
                        }
                        else
                        {
                            btnAdd.Enabled = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region ADD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString().ToUpper() == "TRADITIONAL" && dtData.Rows.Count == 1)
                {
                    stsWarning.Text = "You can only setup one data if you select traditional storage!!";
                    return;
                }
                if (cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString().ToUpper() == "CARROUSEL" && dtData.Rows.Count == 99)
                {
                    stsWarning.Text = "You can only setup 99 cranes if you select carrousel storage!!";
                    return;
                }
                Admin_StorageDefine_AddJIT objAdmin_StorageDefine_AddJIT = new Admin_StorageDefine_AddJIT(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString(), cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString(), "", dtData, Type);
                objAdmin_StorageDefine_AddJIT.ShowDialog();
                dtData = objAdmin_StorageDefine_AddJIT.StorageData;
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region DownLoad
        private void btnDown_Click(object sender, EventArgs e)
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
                MessageBox.Show(ex.Message + "<-btnDown_Click()");
                return;
            }
        }
        #endregion

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tType\tLocation Type\tCrane\tHeight(Level)\tLength\tWidth(Depth)\tJIT Procedure";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["CTRLNM"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CTRLC2"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CTRLC4"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CTRLC5"].ToString() + "\t";
                    strLine += dtData.Rows[i]["STSET"].ToString() + "\t";
                    strLine += dtData.Rows[i]["STHGH"].ToString() + "\t";
                    strLine += dtData.Rows[i]["STLEN"].ToString() + "\t";
                    strLine += dtData.Rows[i]["STWID"].ToString() + "\t";
                    strLine += dtData.Rows[i]["REMAK"].ToString() + "\t";
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

        #region gvData Mouse Down Event
        private void gvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                stsWarning.Text = "";
                string strSttyp = "";
                string strLotyp = "";
                DataRow[] foundRow;


                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);


                if (strType == "" || cmbWerks.SelectedIndex == -1)
                {
                    stsWarning.Text = "Please select the plant!!";
                    return;
                }
                //if (Lgort == "" || Lgort.Length != 4)
                //{
                //    stsWarning.Text = "Please input the storage!!";
                //    return;
                //}
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;

                    strStset = dgvClick.Rows[intRowNo].Cells[4].Value.ToString();
                    Lgort = dgvClick.Rows[intRowNo].Cells[1].Value.ToString();
                    if (Stset != "")
                    {
                        if (rdoModify.Checked)
                        {
                            DataColumn[] dcPrimaryKey = new DataColumn[2];
                            dcPrimaryKey[0] = dtData.Columns["CTRLC2"];
                            dcPrimaryKey[1] = dtData.Columns["STSET"];
                            dtData.PrimaryKey = dcPrimaryKey;
                            foundRow = dtData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + Stset + "'");
                            for (int i = 0; i < foundRow.Length; i++)
                            {
                                strSttyp = foundRow[0]["CTRLC4"].ToString();
                                strLotyp = foundRow[0]["CTRLC5"].ToString();
                            }
                            if (objPlantData.CheckStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, "", Stset) && strSttyp.ToUpper() == "CARROUSEL")
                            {
                                stsWarning.Text = "The crane of this storage has storage now and can't be modified!!";
                                return;
                            }

                        }
                        else
                        {
                            //strSttyp = cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString();
                            //strLotyp = cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString();
                        }
                        Admin_StorageDefine_AddJIT objAdmin_StorageDefine_AddJIT = new Admin_StorageDefine_AddJIT(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, strSttyp, strLotyp, strStset, dtData, Type);
                        objAdmin_StorageDefine_AddJIT.ShowDialog();
                        strRemark = objAdmin_StorageDefine_AddJIT.Remak;
                        if (rdoModify.Checked)
                        {
                            dtData = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort);

                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (dtData.Rows[i]["CTRLC2"].ToString() == Lgort)
                                {
                                    dtData.Rows[i]["REMAK"] = strRemark;
                                }
                                dtData.AcceptChanges();
                            }
                            ShowDataGridView();
                        }
                        else
                        {
                            //dtData = objAdmin_StorageDefine_Add.StorageData;
                            //ShowDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 調整佈局大小
        private void Admin_StorageDefine_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        private void gvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int intRowNo;
                stsWarning.Text = "";
                string strSttyp = "";
                string strLotyp = "";
                DataRow[] foundRow;


                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);


                if (strType == "" || cmbWerks.SelectedIndex == -1)
                {
                    stsWarning.Text = "Please select the plant!!";
                    return;
                }
                //if (Lgort == "" || Lgort.Length != 4)
                //{
                //    stsWarning.Text = "Please input the storage!!";
                //    return;
                //}
                //if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    //intRowNo = hitRow.RowIndex;
                    intRowNo = dgvClick.CurrentRow.Index;

                    strStset = dgvClick.Rows[intRowNo].Cells[4].Value.ToString();
                    if (Stset != "")
                    {
                        if (rdoModify.Checked)
                        {
                            DataColumn[] dcPrimaryKey = new DataColumn[2];
                            dcPrimaryKey[0] = dtData.Columns["CTRLC2"];
                            dcPrimaryKey[1] = dtData.Columns["STSET"];
                            dtData.PrimaryKey = dcPrimaryKey;
                            foundRow = dtData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + Stset + "'");
                            for (int i = 0; i < foundRow.Length; i++)
                            {
                                strSttyp = foundRow[0]["CTRLC4"].ToString();
                                strLotyp = foundRow[0]["CTRLC5"].ToString();
                            }
                            if (objPlantData.CheckStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, "", Stset) && strSttyp.ToUpper() == "CARROUSEL")
                            {
                                stsWarning.Text = "The crane of this storage has storage now and can't be modified!!";
                                return;
                            }

                        }
                        else
                        {
                            //strSttyp = cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString();
                            //strLotyp = cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString();
                        }
                        Admin_StorageDefine_AddJIT objAdmin_StorageDefine_AddJIT = new Admin_StorageDefine_AddJIT(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, strSttyp, strLotyp, strStset, dtData, Type);
                        objAdmin_StorageDefine_AddJIT.ShowDialog();
                        strRemark = objAdmin_StorageDefine_AddJIT.Remak;
                        if (rdoModify.Checked)
                        {
                            dtData = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort);

                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (dtData.Rows[i]["CTRLC2"].ToString() == Lgort)
                                {
                                    dtData.Rows[i]["REMAK"] = strRemark;
                                }
                                dtData.AcceptChanges();
                            }
                            ShowDataGridView();
                        }
                        else
                        {
                            //dtData = objAdmin_StorageDefine_Add.StorageData;
                            //ShowDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
    }
}
