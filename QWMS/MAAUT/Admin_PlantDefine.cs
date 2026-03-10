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
    public partial class Admin_PlantDefine : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private DataTable dtData;
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


        public Admin_PlantDefine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                dgvData.AutoGenerateColumns = false;

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    //列出廠區資料
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDataGrid()
        {
            try
            {

                dtData = objPlantData.GetDdlWerksData();
                dgvData.AutoGenerateColumns = false;
                dgvData.Columns.Clear();
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.DataPropertyName = "F_TEXT";
                dgvc.HeaderText = "Plant";
                dgvc.ReadOnly = true;
                dgvData.Columns.Add(dgvc);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.txtWerks.Enabled = true;
            this.gbFunction.Enabled = false;
            this.btnSave.Enabled = true;
        }



        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.txtWerks.Enabled = true;
            this.gbFunction.Enabled = false;
            this.btnSave.Enabled = true;

        }

        private void txtWerks_Leave(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";

                # region 删除
                //刪除
                if (rdoDelete.Checked == true)
                {
                    //檢查是不是空值及長度
                    if (this.txtWerks.Text.Trim() == "")
                    {
                        stsWarning.Text = "The plant can't be empty!!";
                        return;
                    }
                    //檢查廠區是否存在
                    if (!objPlantData.CheckExistedStorageData(txtWerks.Text.Trim(), "", ""))
                    {
                        stsWarning.Text = "The plant doesn't exist in the system!!";
                        return;
                    }
                    //檢查廠區是否有庫存
                    if (objPlantData.CheckStorageData(txtWerks.Text.Trim(), "", ""))
                    {
                        stsWarning.Text = "The plant has storage now and can't be deleted!!";
                        return;
                    }
                    //刪除資料
                    if (objAdmin.DeletePlant(txtWerks.Text.Trim(), UserData))
                    {
                        stsWarning.Text = "Delete OK!";
                        this.txtWerks.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail!" + objAdmin.ERRMSG;
                    }
                }
                # endregion

                # region 新增
                //新增
                if (rdoAdd.Checked == true)
                {
                    //檢查是不是空值及長度
                    if (this.txtWerks.Text.Trim() == "" || this.txtWerks.Text.Trim().Length != 4)
                    {
                        stsWarning.Text = "The plant can't be empty and the length should be 4 chars";
                        return;
                    }
                    //檢查廠區是否存在
                    if (objPlantData.CheckExistedStorageData(txtWerks.Text.Trim(), "", ""))
                    {
                        stsWarning.Text = "The plant has existed in the system!!";
                        return;
                    }
                    //新增資料
                    if (objAdmin.AddPlant(txtWerks.Text.Trim()))
                    {
                        stsWarning.Text = "Add OK!";
                        this.txtWerks.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                    }
                }
                # endregion

                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.rdoDelete.Checked = false;
                this.rdoAdd.Checked = false;
                this.rdoDelete.Enabled = true;
                this.rdoAdd.Enabled = true;
                this.gbFunction.Enabled = true;
                this.btnSave.Enabled = false;
                this.txtWerks.Text = "";
                this.dgvData.DataSource = null;
                this.stsWarning.Text = "";
                this.txtWerks.Enabled = false;
                this.lblCount.Text = "";
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Admin_PlantDefine_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.4), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
        }
    }
}
