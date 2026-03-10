using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Alim_LocationDefine : Form
    {


        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";//QWMS系统基础资料维护权限
        private string strWerks = "";
        private string strLgort = "";
        private string strType = "";
        private string strContrno = "";//柜号
        private string strMainRun = "";//主流道
        private string strSubRun = "";//支流道

        DataTable dtSize = new DataTable();

        QCI.QWMS.Alim objAlim;
        #endregion


        #region 构造函数
        public Alim_LocationDefine(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;

            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;


            objAlim = new QCI.QWMS.Alim(UserData, strProgid);

            try
            {
                 //检查权限
                if (!objAlim.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();//厂区
                    ShowDdlLgort();//仓别
                    ShowSize();//尺寸
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
            this.stsMandt.Text = strMandt;
            this.stsUsrnm.Text = strUsrnm;
            this.stsComcd.Text = strComcd;

        }
        # endregion


        #region ShowDataView
        private void ShowDataView(DataTable dtData)
        {
            try
            {
                this.GvData.AutoGenerateColumns = false;
                this.GvData.Columns.Clear();

                //if (!dtsap.Columns.Contains("Select"))
                //{
                //    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                //    dtsap.Columns.Add(cSelect);
                //}

                //DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                //dgvcSelect.DataPropertyName = "Select";
                //dgvcSelect.HeaderText = "选择";
                //dgvcSelect.Width = 50;
                //dgvcSelect.Selected = false;
                //this.GvData.Columns.Add(dgvcSelect);

                //WERKS,LGORT,LOCAT,MAINRUN,SUBRUN,CONTRNO,SIZE,COLNUM,ROWNUM,DEPTH,MONAM,MODAT


                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                this.GvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                this.GvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "储位";
                dgvcLOCAT.ReadOnly = true;
                this.GvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMAINRUN = new DataGridViewTextBoxColumn();
                dgvcMAINRUN.DataPropertyName = "MAINRUN";
                dgvcMAINRUN.HeaderText = "主流道";
                dgvcMAINRUN.ReadOnly = true;
                this.GvData.Columns.Add(dgvcMAINRUN);

                DataGridViewTextBoxColumn dgvcSUBRUN = new DataGridViewTextBoxColumn();
                dgvcSUBRUN.DataPropertyName = "SUBRUN";
                dgvcSUBRUN.HeaderText = "支流道";
                dgvcSUBRUN.ReadOnly = true;
                this.GvData.Columns.Add(dgvcSUBRUN);

                DataGridViewTextBoxColumn dgvcCONTRNO = new DataGridViewTextBoxColumn();
                dgvcCONTRNO.DataPropertyName = "CONTRNO";
                dgvcCONTRNO.HeaderText = "柜号";
                dgvcCONTRNO.ReadOnly = true;
                this.GvData.Columns.Add(dgvcCONTRNO);

                DataGridViewTextBoxColumn dgvcSIZE = new DataGridViewTextBoxColumn();
                dgvcSIZE.DataPropertyName = "SIZE";
                dgvcSIZE.HeaderText = "尺寸";
                dgvcSIZE.ReadOnly = true;
                this.GvData.Columns.Add(dgvcSIZE);

                DataGridViewTextBoxColumn dgvcCOLNUM = new DataGridViewTextBoxColumn();
                dgvcCOLNUM.DataPropertyName = "COLNUM";
                dgvcCOLNUM.HeaderText = "列";
                dgvcCOLNUM.ReadOnly = true;
                this.GvData.Columns.Add(dgvcCOLNUM);

                DataGridViewTextBoxColumn dgvcROWNUM = new DataGridViewTextBoxColumn();
                dgvcROWNUM.DataPropertyName = "ROWNUM";
                dgvcROWNUM.HeaderText = "行";
                dgvcROWNUM.ReadOnly = true;
                this.GvData.Columns.Add(dgvcROWNUM);

                DataGridViewTextBoxColumn dgvcDEPTH = new DataGridViewTextBoxColumn();
                dgvcDEPTH.DataPropertyName = "DEPTH";
                dgvcDEPTH.HeaderText = "深";
                dgvcDEPTH.ReadOnly = true;
                this.GvData.Columns.Add(dgvcDEPTH);

                DataGridViewTextBoxColumn dgvcMONAM = new DataGridViewTextBoxColumn();
                dgvcMONAM.DataPropertyName = "MONAM";
                dgvcMONAM.HeaderText = "操作员";
                dgvcMONAM.ReadOnly = true;
                this.GvData.Columns.Add(dgvcMONAM);

                DataGridViewTextBoxColumn dgvcMODAT = new DataGridViewTextBoxColumn();
                dgvcMODAT.DataPropertyName = "MODAT";
                dgvcMODAT.HeaderText = "修改时间";
                dgvcMODAT.ReadOnly = true;
                this.GvData.Columns.Add(dgvcMODAT);

                this.GvData.DataSource = dtData;
                lbrecords.Text = dtData.Rows.Count.ToString() + " records";
                GvData.ClearSelection();
                GvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView()");
            }
        }
        #endregion


        #region Type
        private void RbAdd_CheckedChanged(object sender, EventArgs e)
        {
            GrpType.Enabled = false;
            strType = "Add";
            txtColnum.Enabled = true;
            txtRownum.Enabled = true;
            txtDepth.Enabled = true;
            BtnSave.Enabled = true;
            cmbSize.Enabled = true ;
        }

        private void RbModify_CheckedChanged(object sender, EventArgs e)
        {
            GrpType.Enabled = false;
            strType = "Modify";
            BtnSave.Enabled = true;
            txtMainRun.Text = "";
            txtSubRun.Text = "";
            cmbSize.Enabled = true;
        }

        private void RbDelete_CheckedChanged(object sender, EventArgs e)
        {
            GrpType.Enabled = false;
            strType = "Delete";
            BtnSave.Enabled = true;
            txtMainRun.Text = "";
            txtSubRun.Text = "";
            txtMainRun.Enabled = false;
            txtSubRun.Enabled = false;
        }
        #endregion


        #region Quary
        private void BtnQuary_Click(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }

            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }

            strContrno = txtContrno.Text.ToString().Trim();
            strMainRun = txtMainRun.Text.ToString().Trim();
            strSubRun = txtSubRun.Text.ToString().Trim();

            DataTable dtData = new DataTable();
            dtData = objAlim.QuaryAlimALHED(strWerks,strLgort,strContrno,strMainRun,strSubRun);
            ShowDataView(dtData);
            stsWarning.Text = "";

        }
        #endregion


        #region Save
        private void BtnSave_Click(object sender, EventArgs e)
        {
            #region 必要数据  厂区、仓别、柜号
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }
            strContrno = txtContrno.Text.ToString().Trim();
            if (strContrno.Length > 4)
            {
                stsWarning.Text = "柜号长度过长";
                return;
            }
            if (strWerks==""||strLgort==""||strContrno=="")
            {
                stsWarning.Text = "请检查厂区仓别和柜号";
                return;
            }
            //检查目前该柜号是否已有储位
            DataTable dtTemp = objAlim.QuaryAlimALHED(strWerks, strLgort, strContrno, "", "");
            #endregion

            if (strType == "")
            {
                stsWarning.Text = "请选择类型";
                return;
            }
            #region Add
            else if (strType == "Add")
            {
                #region Add必要数据检查
                strMainRun = txtMainRun.Text.ToString().Trim();
                strSubRun = txtSubRun.Text.ToString().Trim();
                int TempSize = cmbSize.SelectedIndex;
                if (strMainRun == "" || strSubRun == ""||TempSize==-1)
                {
                    stsWarning.Text = "请检查主流道、支流道，并选择尺寸后重试";
                    return;
                }

                if (dtTemp.Rows.Count > 0)
                {
                    stsWarning.Text = "该柜号已有储位,无法新增";
                    return;
                }

                int Colnum, Rownum, Depth;
                try
                {
                    Colnum = Convert.ToInt32(txtColnum.Text.ToString().Trim());
                    Rownum = Convert.ToInt32(txtRownum.Text.ToString().Trim());
                    Depth = Convert.ToInt32(txtDepth.Text.ToString().Trim());


                    if (Colnum < 1 || Rownum < 1 || Depth < 1)
                    {
                        stsWarning.Text = "列行深不能为零或负数。";
                        return;
                    }
                    if (Colnum > Convert.ToInt32(dtSize.Rows[TempSize]["CTRLC4"].ToString().Split(';')[0]) || Rownum > Convert.ToInt32(dtSize.Rows[TempSize]["CTRLC4"].ToString().Split(';')[1]) || Depth > Convert.ToInt32(dtSize.Rows[TempSize]["CTRLC4"].ToString().Split(';')[2]))
                    {
                        stsWarning.Text = "列行深数据过大。";
                        return;
                    }
                }
                catch 
                {
                    stsWarning.Text = "请检查列行深数据，列行深中只能填入数字。";
                    return;
                }
                //增加防呆，一个公司别下，主流道唯一（后续会通过主流道查询厂区仓别）
                if(objAlim.CheckMainRun(strWerks,strLgort,strMainRun))
                {
                    stsWarning.Text = "别的仓别已存在该主流道，无法增加";
                    return;
                }
                #endregion


                if (objAlim.AddAlimALHED(strWerks, strLgort, strContrno, strMainRun, strSubRun, dtSize.Rows[TempSize]["CTRLNM"].ToString(), Colnum, Rownum, Depth, Convert.ToInt32(dtSize.Rows[TempSize]["CTRLC3"].ToString().Split(';')[0]), Convert.ToInt32(dtSize.Rows[TempSize]["CTRLC3"].ToString().Split(';')[1])))
                {
                    stsWarning.Text = "增加储位成功";
                    ShowDataView(objAlim.QuaryAlimALHED(strWerks, strLgort, strContrno, "", ""));
                    return;
                }
                else
                {
                    stsWarning.Text = "增加储位失败，请联系QWMS负责人处理";
                    return;
                }
            }
            #endregion
            #region Modify
            else if (strType == "Modify")//只能按照柜号修改尺寸
            {
                #region Modify数据检查
                if (dtTemp.Rows.Count == 0)
                {
                    stsWarning.Text = "该柜号无数据，无法修改。";
                    return;
                }
                int TempSize = cmbSize.SelectedIndex;
                if (TempSize == -1)
                {
                    stsWarning.Text = "请选择一个尺寸";
                    return;
                }
                if ((dtSize.Rows[TempSize]["CTRLC1"].ToString() + "寸*" + dtSize.Rows[TempSize]["CTRLC2"].ToString() + "mm").Equals(dtTemp.Rows[0]["SIZE"].ToString()))
                {
                    stsWarning.Text = "修改后尺寸不能与之前尺寸相同";
                    return;
                }
                #endregion
                //检查库存
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i]["LOSTS"].ToString() != "0")
                    {
                        stsWarning.Text = "该柜号中存在库存，无法修改";
                        return;
                    }
                }
                strMainRun = txtMainRun.Text.ToString().Trim();
                strSubRun = txtSubRun.Text.ToString().Trim();
                //修改并记LOG
                if (objAlim.ModifyAlimALHED(strWerks, strLgort, strContrno, strMainRun, strSubRun, dtTemp.Rows[0]["SIZE"].ToString(), dtSize.Rows[TempSize]["CTRLC1"].ToString() + "寸*" + dtSize.Rows[TempSize]["CTRLC2"].ToString() + "mm", dtSize.Rows[TempSize]["CTRLNM"].ToString()))
                {
                    stsWarning.Text = "修改储位成功";
                    ShowDataView(objAlim.QuaryAlimALHED(strWerks, strLgort, strContrno, "", ""));
                    return;
                }
                else
                {
                    stsWarning.Text = "修改储位失败，请联系QWMS负责人处理";
                    return;
                }
            }
            #endregion
            #region Delete
            else if (strType == "Delete")
            {
                if(dtTemp.Rows.Count==0)
                {
                    stsWarning.Text = "该柜号无储位，无法删除";
                    return;
                }
                //检查库存
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i]["LOSTS"].ToString() != "0")
                    {
                        stsWarning.Text = "该柜号中存在库存，无法删除";
                        return;
                    }
                }
                //删除并记LOG
                if (objAlim.DeleteAlimALHED(strWerks, strLgort, strContrno))
                {
                    stsWarning.Text = "删除储位成功";
                    this.GvData.Columns.Clear();
                    return;
                }
                else
                {
                    stsWarning.Text = "删除储位失败，请联系QWMS负责人处理";
                    return;
                }
            }
            #endregion

        }
        #endregion


        #region Fresh
        private void BtnFresh_Click(object sender, EventArgs e)
        {
            RbAdd.Checked = false;
            RbModify.Checked = false;
            RbDelete.Checked = false;
            GrpType.Enabled = true;
            strType = "";
            stsWarning.Text = "";
            txtMainRun.Text = "";
            txtSubRun.Text = "";
            txtContrno.Text = "";
            txtColnum.Text = "";
            txtRownum.Text = "";
            txtDepth.Text = "";
            this.GvData.Columns.Clear();
            cmbWerks.SelectedIndex = -1;
            cmbLgort.SelectedIndex = -1;
            cmbSize.SelectedIndex = -1;
            txtColnum.Enabled = false;
            txtRownum.Enabled = false;
            txtDepth.Enabled = false;
            BtnSave.Enabled = false;
            txtMainRun.Enabled = true;
            txtSubRun.Enabled = true;
            cmbSize.Enabled = false;
            lbrecords.Text = "";
        }
        #endregion


        #region Exit
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region 厂区、仓别和尺寸
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAlim.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAlim.CheckLgortAuthority();
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
                    strLgort = string.Empty;
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                        if (dtTemp.Rows[i]["CTRLC1"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void ShowSize()
        {
            try
            {
                cmbSize.Items.Clear();
                dtSize = objAlim.CheckSize();
                for (int i = 0; i < dtSize.Rows.Count; i++)
                {
                    cmbSize.Items.Add(dtSize.Rows[i]["CTRLC1"].ToString() + "寸*" + dtSize.Rows[i]["CTRLC2"].ToString()+"mm");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSize()");
            }
        }

        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }
        #endregion







    }
}
