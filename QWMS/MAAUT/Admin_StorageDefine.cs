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
using QWMS.PP;


namespace QWMS
{
    public partial class Admin_StorageDefine : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strStset = "";
        private string strType = "";
        private string strMark = "";
        private bool IsSAP = true;
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


        #region 构造函数
        public Admin_StorageDefine(UserInfo varUserData, string varProgid)
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
                    // 绑定下拉菜单
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

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
            chkDC.Enabled = false;
            chkXL.Enabled = false;
            cmbMark.Enabled = false;
        }
        # endregion

        #region 绑定厂区
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

        #region 绑定仓别类型
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

        #region 绑定储位类型
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

                DataGridViewTextBoxColumn dgvcCTRLC3 = new DataGridViewTextBoxColumn();
                dgvcCTRLC3.DataPropertyName = "CTRLC3";
                dgvcCTRLC3.HeaderText = "Mark";
                dgvcCTRLC3.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCTRLC3);

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

                DataGridViewTextBoxColumn dgvcCTRLN3 = new DataGridViewTextBoxColumn();
                dgvcCTRLN3.DataPropertyName = "DC";
                dgvcCTRLN3.HeaderText = "D/C管控";
                dgvcCTRLN3.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCTRLN3);

                DataGridViewTextBoxColumn dgvcXL = new DataGridViewTextBoxColumn();
                dgvcXL.DataPropertyName = "XL";
                dgvcXL.HeaderText = "祥龙解欠";
                dgvcXL.ReadOnly = true;
                this.gvData.Columns.Add(dgvcXL);

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
              
                this.gvData.DataSource = dtData;

                //DataGridColumnStyle werksStyle = new DataGridTextBoxColumn();
                //werksStyle.MappingName = "CTRLNM";
                //werksStyle.HeaderText = "Plant";
                //werksStyle.ReadOnly = true;
                //mydtgTableStyle.GridColumnStyles.Add(werksStyle);
                //dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }

        }
        #endregion

        #region Radio
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
            this.chkSAP.Enabled = false;
            this.strType = "DELETE";
            this.btnConfirm.Enabled = false;
            chkDC.Enabled = false;
            chkXL.Enabled = false;
            cmbMark.Enabled = false;
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
            this.chkSAP.Enabled = true;
            this.gvData.Enabled = true;
            chkDC.Enabled = true;
            chkXL.Enabled = true;
            cmbMark.Enabled = true;
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
            this.chkSAP.Enabled = true; ;
            chkDC.Enabled = false;
            chkXL.Enabled = false;
            cmbMark.Enabled = false;
        }
        private void chkDC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDC.Checked)
            {
                chkXL.Checked = false;
            }

           
        }
        private void chkXL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkXL.Checked)
            {
                chkDC.Checked = false;
            }

        }

        #endregion

        #region cmbWerksChanged
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

        #endregion

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
                this.chkSAP.Enabled = false;
                cmbWerks.SelectedIndex = -1;
                cmbSttyp.SelectedIndex = -1;
                cmbLotyp.SelectedIndex = -1;
                cmbMark.SelectedIndex = -1;
                cmbMark.Enabled = false;
                chkDC.Checked = false;
                chkDC.Enabled = true;
                chkXL.Checked = false;
                chkXL.Enabled = true;

                cmbSttyp.Enabled = true;
                cmbLotyp.Enabled = true;
                this.gvData.Enabled = true;
                strType = "";
                IsSAP = true;
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

                #region 删除
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
                    if (objAdmin.CheckSAPLgort(strWerks, txtLgort.Text.Trim()))
                    {
                        #region 同步删除仓别数据到SAP
                        DataTable dtLgortSAP = GetDataTableToSAP(strWerks, txtLgort.Text.Trim(), "X");

                        if (dtLgortSAP.Rows.Count > 0)
                        {
                            DataSet dsData = new DataSet();
                            dsData.Tables.Add(dtLgortSAP.Copy());

                            PP_Service ppObj = new PP_Service();
                            DataSet dsResultFromSAP = ppObj.ZRFC_MM_QWMS_LGORT(dsData);
                            DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];
                            if (dtResultFromSAP.Rows.Count > 0)
                            {
                                if (dtResultFromSAP.Rows[0]["P_FLAG"].ToString() == "Y")
                                {
                                    stsWarning.Text = "仓别信息已同步至SAP";
                                }
                                else
                                {
                                    //Add By Lora 2019/06/11
                                    stsWarning.Text = "仓别信息同步至SAP出现错误";
                                    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                                    DialogResult dr = MessageBox.Show("仓别信息同步至SAP出现错误，请确认SAP数据是否已删除?", "删除仓别", messButton);
                                    if (dr == DialogResult.OK)//如果点击“确定”按钮
                                    {
                                    }
                                    else//如果点击“取消”按钮
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion
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
                #endregion

                #region 新增
                if (rdoAdd.Checked == true)
                {

                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "Please at least add one data in the table!!";
                        return;
                    }
                    if (IsSAP)
                    {
                        #region 同步DataTable数据到SAP
                        DataTable dtLgortSAP = GetDataTableToSAP(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), "");

                        if (dtLgortSAP.Rows.Count > 0)
                        {
                            DataSet dsData = new DataSet();
                            dsData.Tables.Add(dtLgortSAP.Copy());

                            PP_Service ppObj = new PP_Service();
                            DataSet dsResultFromSAP = ppObj.ZRFC_MM_QWMS_LGORT(dsData);
                            DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];

                            if (dtResultFromSAP.Rows.Count > 0)
                            {
                                if (dtResultFromSAP.Rows[0]["P_FLAG"].ToString() == "Y")
                                {
                                    stsWarning.Text = "仓别信息已同步至SAP";
                                }
                                else
                                {
                                    MessageBox.Show("仓别信息同步至SAP出现错误");
                                    return;
                                }
                            }
                        }
                        #endregion

                    }

                    if (cmbMark.Text.ToString() == "散料")
                    {
                        strMark = "BULK";
                    }
                    else if (cmbMark.Text.ToString() == "整料")
                    {
                        strMark = "FULL";
                    }

                    string flag = "";
                    if (chkDC.Checked)
                    {
                        flag = "DC";
                    }
                    else if (chkXL.Checked)
                    {
                        flag = "XL";
                    }
                    else
                    {
                        flag = "LGORT";
                    }
                    

                    if (objAdmin.AddStorage(flag, strMark, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), txtLgort.Text.Trim(), cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString(), cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString(), dtData, IsSAP))
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
                #endregion

                #region 查询数据

                dtData = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), "");

                ShowDataGridView();
                #endregion
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
                #region ADD
                if (rdoAdd.Checked == true)
                {
                    if (cmbWerks.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant can't be empty!!";
                        return;
                    }
                    if (cmbSttyp.SelectedIndex == -1 || cmbLotyp.SelectedIndex == -1)
                    {
                        if (Comcd == "9700")
                        {
                            if (Lgort == "" || Lgort.Length != 6)
                            {
                                stsWarning.Text = "Storage can't be empty and the length should be 6 chars!!";
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "Storage type and location type can't be empty!!";
                            return;
                        }

                    }
                    if (this.txtLgort.Text.Trim() == "" || this.txtLgort.Text.Trim().Length != 4)
                    {
                        if (Comcd == "9700")
                        {
                            if (Lgort == "" || Lgort.Length != 6)
                            {
                                stsWarning.Text = "Storage can't be empty and the length should be 6 chars!!";
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "Storage can't be empty and the length should be 4 chars!!";
                            return;
                        }

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
                    chkDC.Enabled = false;
                    chkXL.Enabled = false;
                    cmbMark.Enabled = false;
                }
                #endregion

                #region Modify
                if (rdoModify.Checked == true)
                {

                    if (cmbWerks.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant can't be empty!!";
                        return;
                    }

                    if (Lgort == "" || Lgort.Length != 4)
                    {

                        //CSMC
                        if (Comcd == "9700")
                        {
                            if (Lgort == "" || Lgort.Length != 6)
                            {
                                stsWarning.Text = "Storage can't be empty and the length should be 6 chars!!";
                                return;
                            }
                        }

                        else
                        {
                            stsWarning.Text = "Storage can't be empty and the length should be 4 chars!!";
                            return;
                        }



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
                    //if (cmbMark.Text.ToString() == "")
                    //{
                    //    stsWarning.Text = "Mark can't be empty !!";
                    //    return;
                    //}

                    this.panel4.Enabled = false;
                    this.panel5.Enabled = false;
                    btnAdd.Enabled = false;
                    btnSave.Enabled = false;
                    cmbMark.Enabled = false;
                    chkDC.Enabled = false;
                    chkXL.Enabled = false;
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
                #endregion

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
                Admin_StorageDefine_Add objAdmin_StorageDefine_Add = new Admin_StorageDefine_Add(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, cmbSttyp.Items[cmbSttyp.SelectedIndex].ToString(), cmbLotyp.Items[cmbLotyp.SelectedIndex].ToString(), "", dtData, Type);
                objAdmin_StorageDefine_Add.ShowDialog();
                dtData = objAdmin_StorageDefine_Add.StorageData;
                dtData.Columns.Add("CTRLC3");


                dtData.Rows[0]["CTRLC3"] = cmbMark.Text.ToString();
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

        #region 导出txt
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tType\tLocation Type\tCrane\tHeight(Level)\tLength\tWidth(Depth)";
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
        #endregion

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
                    return;
                }
                if (Lgort == "" || Lgort.Length != 4)
                {
                    return;
                }
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;

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
                        Admin_StorageDefine_Add objAdmin_StorageDefine_Add = new Admin_StorageDefine_Add(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort, strSttyp, strLotyp, strStset, dtData, Type);
                        objAdmin_StorageDefine_Add.ShowDialog();
                        if (rdoModify.Checked)
                        {
                            dtData = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), Lgort);
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

        #region 调整布局大小
        private void Admin_StorageDefine_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        #region 是否需要SAP单据
        private void chkSAP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSAP.Checked)
            {
                IsSAP = true;
            }
            else
            {
                IsSAP = false;
            }
        }
        #endregion

        #region 仓别信息同步至SAP的表格格式 --by Chiris 2017-11-1
        public DataTable GetDataTableToSAP(string strWerks, string strLgort, string remak)
        {
            DataTable dtLgortSAP = new DataTable();
            dtLgortSAP.Columns.Add("MANDT", typeof(string));
            dtLgortSAP.Columns.Add("WERKS", typeof(string));
            dtLgortSAP.Columns.Add("LGORT", typeof(string));
            dtLgortSAP.Columns.Add("REMARK", typeof(string));

            DataRow drN = dtLgortSAP.NewRow();
            drN["MANDT"] = Mandt;
            drN["WERKS"] = strWerks;
            drN["LGORT"] = strLgort;
            drN["REMARK"] = remak;
            dtLgortSAP.Rows.Add(drN);
            return dtLgortSAP;
        }

        #endregion
    }
}
