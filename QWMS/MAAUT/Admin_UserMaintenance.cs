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
    public partial class Admin_UserMaintenance : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private ArrayList aryProgm = new ArrayList();
        private DataTable dtAuthority;
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
        private DataTable dtPlantStorageAuthority;

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

        public Admin_UserMaintenance()
        {
            InitializeComponent();

        }

        public Admin_UserMaintenance(UserInfo varUserData, string strProgid)
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

                //取得程式類別
                GetProgType();
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    //列出權限的選項資料
                    GetTabPageData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        # region 显示状态栏资料
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        # endregion

        # region 取得程式类别
        private void GetProgType()
        {
            try
            {
                //取得程式類別
                dtAuthority = objPlantData.GetProgramClass();
                for (int i = 0; i < dtAuthority.Rows.Count; i++)
                {
                    aryProgm.Add(dtAuthority.Rows[i]["CTRLNM"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetProgType()");
            }
        }
        # endregion

        # region 列出权限选项资料
        private void GetTabPageData()
        {
            string strProtp = "";
            DataTable dtTempAuthority = new DataTable();
            try
            {
                //取得廠區資料
                dtTempAuthority = objPlantData.GetDdlWerksLgortData();
                for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                {
                    chkWkaut.Items.Add(dtTempAuthority.Rows[j]["CTRLNM"].ToString() + "-" + dtTempAuthority.Rows[j]["CTRLC1"].ToString());
                }
                for (int i = 0; i < aryProgm.Count; i++)
                {
                    strProtp = aryProgm[i].ToString();
                    dtTempAuthority = objPlantData.GetProgramData(strProtp);
                    switch (strProtp)
                    {
                        case "MAAUT": //系統資料維護(MAAUT)                            
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkMaaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "INAUT": //入庫作業(INAUT)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkInaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "OTAUT": //出庫作業(OTAUT)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkOtaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "MGAUT": //庫存管理作業(MGAUT)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkMgaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "IVAUT": //盤點作業(IVAUT)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkIvaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "REPLN": //補貨作業(REPLN)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkRepln.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        case "TRANS": //上海调拨作业(TRANS)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkTrans.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;
                        //case "ALAUT": //电子仓作业(ALAUT)
                        //    for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                        //    {
                        //        chkAlim.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                        //    }
                        //    break;
                        case "AGAUT": //AGV智能作业(AGAUT)
                            for (int j = 0; j < dtTempAuthority.Rows.Count; j++)
                            {
                                chkAgaut.Items.Add(dtTempAuthority.Rows[j]["CTRLC4"].ToString());
                            }
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetTabPageData()");
            }
        }
        # endregion

        # region RadioButton CheckedChange
        private void rdoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            txtUsrnm.Enabled = true;
            txtPasswd.Enabled = true;
            btnQuery.Enabled = true;
            gbFunction.Enabled = false;

        }

        //private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtUsrnm.Enabled = true;
        //    txtPasswd.Enabled = true;
        //    //btnQuery.Enabled = true;
        //    gbFunction.Enabled = false;
        //    btnSave.Enabled = true;

        //}

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            txtUsrnm.Enabled = true;
            txtPasswd.Enabled = true;
            gbFunction.Enabled = false;
            btnSave.Enabled = true;

        }
        # endregion

        # region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            gbFunction.Enabled = false;
            try
            {
                dtAuthority = objAdmin.QueryUserData(txtUsrnm.Text.Trim());
                dtPlantStorageAuthority = objAdmin.QueryUserPlantStorageData(txtUsrnm.Text.Trim());
                if (dtAuthority.Rows.Count > 0)
                {
                    txtUsrnm.Enabled = false;
                    btnQuery.Enabled = false;
                    //if (rdoUpdate.Checked || rdoDelete.Checked)
                    if (rdoUpdate.Checked)
                    {
                        btnSave.Enabled = true;
                    }
                    //將資料呈現出來
                    this.txtPasswd.Text = dtAuthority.Rows[0]["PASWD"].ToString();
                    GetAuthorityData();
                    GetWerksAuthorityData();
                }
                else
                {
                    this.stsWarning.Text = "The user doesn't exist!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region 绑定plant-storage权限
        private void GetWerksAuthorityData()
        {
            string strTempWerksLgort = "";
            for (int i = 0; i < dtPlantStorageAuthority.Rows.Count; i++)
            {
                strTempWerksLgort += dtPlantStorageAuthority.Rows[i]["WERKS"].ToString() + "-" + dtPlantStorageAuthority.Rows[i]["LGORT"].ToString() + ";";
            }

            for (int j = 0; j < chkWkaut.Items.Count; j++)
            {
                if (strTempWerksLgort.IndexOf(chkWkaut.Items[j].ToString()) != -1)
                {
                    chkWkaut.SetItemChecked(j, true);
                }
            }

        }
        # endregion

        # region 绑定MAAUT/INAUT/OTAUT/MGAUT/IVAUT/REPLN权限
        private void GetAuthorityData()
        {
            DataTable dtTemp = new DataTable();
            DataRow[] foundRow;
            try
            {
                dtTemp = objPlantData.GetProgramData();
                for (int i = 0; i < aryProgm.Count; i++)
                {
                    foundRow = dtTemp.Select("CTRLNM='" + aryProgm[i].ToString() + "'");
                    switch (aryProgm[i].ToString())
                    {
                        case "MAAUT": //系統資料維護(MAAUT)	
                            for (int j = 0; j < chkMaaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkMaaut.Items[j].ToString() && dtAuthority.Rows[0]["MAAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkMaaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "INAUT": //入庫作業(INAUT)
                            for (int j = 0; j < chkInaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkInaut.Items[j].ToString() && dtAuthority.Rows[0]["INAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkInaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "OTAUT": //出庫作業(OTAUT)
                            for (int j = 0; j < chkOtaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkOtaut.Items[j].ToString() && dtAuthority.Rows[0]["OTAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkOtaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "MGAUT": //庫存管理作業(MGAUT)
                            for (int j = 0; j < chkMgaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkMgaut.Items[j].ToString() && dtAuthority.Rows[0]["MGAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkMgaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "IVAUT": //盤點作業(IVAUT)
                            for (int j = 0; j < chkIvaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkIvaut.Items[j].ToString() && dtAuthority.Rows[0]["IVAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkIvaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "REPLN": //補貨作業(REPLN)
                            for (int j = 0; j < chkRepln.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkRepln.Items[j].ToString() && dtAuthority.Rows[0]["REPLN"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkRepln.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        case "TRANS": //上海调拨作业(TRANS)
                            for (int j = 0; j < chkTrans.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkTrans.Items[j].ToString() && dtAuthority.Rows[0]["REPLN"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkTrans.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                        //case "ALAUT": //电子仓作业(ALAUT)
                        //    for (int j = 0; j < chkAlim.Items.Count; j++)
                        //    {
                        //        for (int k = 0; k < foundRow.Length; k++)
                        //        {
                        //            if (foundRow[k]["CTRLC4"].ToString() == chkAlim.Items[j].ToString() && dtAuthority.Rows[0]["CGAUT"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                        //            {
                        //                chkAlim.SetItemChecked(j, true);
                        //            }
                        //        }
                        //    }
                        //    break;
                        case "AGAUT": //AGV智能作业(AGAUT)
                            for (int j = 0; j < chkAgaut.Items.Count; j++)
                            {
                                for (int k = 0; k < foundRow.Length; k++)
                                {
                                    if (foundRow[k]["CTRLC4"].ToString() == chkAgaut.Items[j].ToString() && dtAuthority.Rows[0]["REPLN"].ToString().IndexOf(foundRow[k]["CTRLC2"].ToString()) != -1)
                                    {
                                        chkAgaut.SetItemChecked(j, true);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetAuthorityData()");
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.rdoAdd.Checked = false;
                //this.rdoDelete.Checked = false;
                this.rdoUpdate.Checked = false;
                this.rdoAdd.Enabled = true;
                //this.rdoDelete.Enabled = true;
                this.rdoUpdate.Enabled = true;
                this.gbFunction.Enabled = true;
                this.btnQuery.Enabled = false;
                this.btnSave.Enabled = false;

                this.stsWarning.Text = "";
                this.txtUsrnm.Text = "";
                this.txtPasswd.Text = "";
                this.txtUsrnm.Enabled = false;
                this.txtPasswd.Enabled = false;


                for (int i = 0; i < chkMaaut.Items.Count; i++)
                {
                    chkMaaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkInaut.Items.Count; i++)
                {
                    chkInaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkOtaut.Items.Count; i++)
                {
                    chkOtaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkMgaut.Items.Count; i++)
                {
                    chkMgaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkIvaut.Items.Count; i++)
                {
                    chkIvaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkWkaut.Items.Count; i++)
                {
                    chkWkaut.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkRepln.Items.Count; i++)
                {
                    chkRepln.SetItemChecked(i, false);
                }
                for (int i = 0; i < chkAgaut.Items.Count; i++)
                {
                    chkAgaut.SetItemChecked(i, false);
                }
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

        # region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            bool bolTransaction = false;
            DataTable dtTemp = new DataTable();
            ArrayList aryMaaut = new ArrayList();
            ArrayList aryInaut = new ArrayList();
            ArrayList aryOtaut = new ArrayList();
            ArrayList aryCgaut = new ArrayList();
            ArrayList aryMgaut = new ArrayList();
            ArrayList aryIvaut = new ArrayList();
            ArrayList aryWkaut = new ArrayList();
            ArrayList aryRepln = new ArrayList();
            ArrayList aryTrans = new ArrayList();
            ArrayList aryAgaut = new ArrayList();

            try
            {
                aryMaaut = CombineQueryInString(chkMaaut);
                aryInaut = CombineQueryInString(chkInaut);
                aryOtaut = CombineQueryInString(chkOtaut);
                aryMgaut = CombineQueryInString(chkMgaut);
                aryIvaut = CombineQueryInString(chkIvaut);
                aryWkaut = CombineQueryInString(chkWkaut);
                aryRepln = CombineQueryInString(chkRepln);
                aryTrans = CombineQueryInString(chkTrans);
                //aryCgaut = CombineQueryInString(chkAlim);
                aryAgaut = CombineQueryInString(chkAgaut);

                # region 修改
                //修改
                if (rdoUpdate.Checked)
                {
                    bolTransaction = objAdmin.ModifyUserData(txtUsrnm.Text, txtPasswd.Text, aryWkaut, aryMaaut, aryInaut, aryOtaut, aryCgaut, aryIvaut, aryMgaut, aryRepln, aryTrans, aryAgaut, "N");
                    if (bolTransaction == true)
                        stsWarning.Text = "Update OK!!";
                    else
                        stsWarning.Text = "Update fail!" + objAdmin.ERRMSG;
                }
                # endregion

                # region 新增
                //新增
                if (rdoAdd.Checked)
                {
                    //User ID & Password can't be empty
                    if (txtUsrnm.Text.Trim() == "" || txtPasswd.Text.Trim() == "")
                    {
                        stsWarning.Text = "User ID and password can't be empty!!";
                        return;
                    }

                    //可以一次存入多个用户资料(以逗号分隔)
                    string[] strArrayUserName = txtUsrnm.Text.ToString().Trim().Split(new char[] { ',' });

                    for (int i = 0; i < strArrayUserName.Length; i++)
                    {
                        if (strArrayUserName[i].ToString().Trim() == "")
                        {
                            continue;
                        }
                        if (strArrayUserName[i].ToString().Trim().Length > 10)
                        {
                            stsWarning.Text = strArrayUserName[i].ToString().Trim() + " :The user's length is too long!!";
                            return;

                        }
                        //Check user is duplication or not
                        dtTemp = objAdmin.QueryUserData(strArrayUserName[i].ToString().Trim());
                        if (dtTemp.Rows.Count > 0)
                        {
                            stsWarning.Text = strArrayUserName[i].ToString().Trim() + " :The user has existed in the system!!";
                            return;
                        }
                    }
                    //Add user data
                    if (objAdmin.AddUserData(strArrayUserName, txtPasswd.Text, aryWkaut, aryMaaut, aryInaut, aryOtaut, aryCgaut, aryIvaut, aryMgaut, aryRepln, "N"))
                    {
                        stsWarning.Text = "Add OK!!";
                    }
                    else
                    {
                        stsWarning.Text = "Add fail!" + objAdmin.ERRMSG;
                    }
                }
                # endregion

                # region 删除（不用）
                ////删除
                //if (rdoDelete.Checked)
                //{

                //    MessageBoxButtons mesButton = MessageBoxButtons.OKCancel;
                //    DialogResult dlog = MessageBox.Show("确认要删除这些账号吗？", "删除账号", mesButton);

                //    if (dlog == DialogResult.OK)//如果点击确认
                //    {
                //        //可以一次存入多个用户资料(以逗号分隔)
                //        string[] strArrayUserName = txtUsrnm.Text.ToString().Trim().Split(new char[] { ',' });

                //        for (int i = 0; i < strArrayUserName.Length; i++)
                //        {
                //            if (strArrayUserName[i].ToString().Trim() == "")
                //            {
                //                continue;
                //            }
                //            //Check user exists or not
                //            dtTemp = objAdmin.QueryUserData(strArrayUserName[i].ToString().Trim());
                //            if (dtTemp.Rows.Count == 0)
                //            {
                //                stsWarning.Text = strArrayUserName[i].ToString().Trim() + ": The user doesn't exist in the system!!";
                //                return;
                //            }
                //        }
                //        //Delete user data
                //       if (objAdmin.DeleteUserData(strArrayUserName, UserData))
                //            stsWarning.Text = "Delete OK!!";
                //        else
                //            stsWarning.Text = "Delete fail!" + objAdmin.ERRMSG;
                //    }
                //}
                # endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region 组合权限List
        private ArrayList CombineQueryInString(CheckedListBox chkListBox)
        {
            string strTemp = "";
            ArrayList aryProgID = new ArrayList();
            DataTable dtTemp = new DataTable();

            try
            {
                if (chkListBox.Name == "chkWkaut")
                {
                    for (int i = 0; i < chkListBox.Items.Count; i++)
                    {
                        if (chkListBox.GetItemChecked(i))
                        {
                            aryProgID.Add(chkListBox.Items[i].ToString());
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chkListBox.Items.Count; i++)
                    {
                        //实时库存比对页面权限由MIS管控
                        if (chkListBox.GetItemChecked(i))
                        {
                            strTemp += "," + "'" + chkListBox.Items[i].ToString() + "'";
                        }
                    }
                    if (strTemp.Length > 0)
                        strTemp = strTemp.Substring(1);
                    else
                        strTemp = "''";
                    //获得对应的权限代码
                    dtTemp = objPlantData.GetProgramCode(strTemp);
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        aryProgID.Add(dtTemp.Rows[i]["CTRLC2"].ToString());
                    }
                }

                return aryProgID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CombineQueryInString()");
            }
        }
        # endregion

        # region 查询User List
        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                Admin_UserMaintenance_List objAdmin_UserMaintenance_List = new Admin_UserMaintenance_List(UserData, Progid);
                objAdmin_UserMaintenance_List.MdiParent = this.ParentForm.ParentForm;
                objAdmin_UserMaintenance_List.ShowDialog();
                if (objAdmin_UserMaintenance_List.Account != "")
                {
                    for (int i = 0; i < chkMaaut.Items.Count; i++)
                    {
                        chkMaaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkInaut.Items.Count; i++)
                    {
                        chkInaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkOtaut.Items.Count; i++)
                    {
                        chkOtaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkMgaut.Items.Count; i++)
                    {
                        chkMgaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkIvaut.Items.Count; i++)
                    {
                        chkIvaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkWkaut.Items.Count; i++)
                    {
                        chkWkaut.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkRepln.Items.Count; i++)
                    {
                        chkRepln.SetItemChecked(i, false);
                    }
                    for (int i = 0; i < chkAgaut.Items.Count; i++)
                    {
                        chkAgaut.SetItemChecked(i, false);
                    }
                    this.txtUsrnm.Text = objAdmin_UserMaintenance_List.Account;
                    btnQuery_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        private void Admin_UserMaintenance_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - this.panel1.Width - 200 > 0)
            {
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - this.panel1.Width - 200;
            }
        }

        private void chkWkaut_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkWkaut.Items.Count; i++)
            {
                if (chkWkaut.Items[i].ToString() == "CS12-MK10" || chkWkaut.Items[i].ToString() == "CS12-MKFG")
                {
                    DataTable dtmask = new DataTable();
                    dtmask = objAdmin.QueryMaskUser();
                    chkWkaut.SetItemChecked(i, false);
                    //仓别权限
                    if (dtmask.Rows.Count > 0)
                    {
                        if (dtmask.Rows[0][0].ToString().Trim().Contains(txtUsrnm.Text.ToString().Trim()))
                        {
                            chkWkaut.SetItemChecked(i, true);
                        }
                        //if (txtUsrnm.Text == "ADMIN" || txtUsrnm.Text == "CHE" || txtUsrnm.Text == "BOB" || txtUsrnm.Text == "WINTER" || txtUsrnm.Text == "JEFF" || txtUsrnm.Text == "06090228")
                        //{
                            //chkWkaut.SetItemChecked(i, true);
                        //}
                    }
                }
            }
        }


        private void chkWkaut_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < chkWkaut.Items.Count; i++)
            {
                if (chkWkaut.Items[i].ToString() == "CS12-MK10" || chkWkaut.Items[i].ToString() == "CS12-MKFG")
                {
                    DataTable dtmask = new DataTable();
                    dtmask = objAdmin.QueryMaskUser();
                    chkWkaut.SetItemChecked(i, false);
                    //仓别权限
                    if (dtmask.Rows.Count > 0)
                    {
                        if (dtmask.Rows[0][0].ToString().Trim().Contains(txtUsrnm.Text.ToString().Trim()))
                        {
                            chkWkaut.SetItemChecked(i, true);
                        }
                        //if (txtUsrnm.Text == "ADMIN" || txtUsrnm.Text == "CHE" || txtUsrnm.Text == "BOB" || txtUsrnm.Text == "WINTER" || txtUsrnm.Text == "JEFF" || txtUsrnm.Text == "06090228")
                        //{
                        //chkWkaut.SetItemChecked(i, true);
                        //}
                    }
                }
            }
        }







    }
}
