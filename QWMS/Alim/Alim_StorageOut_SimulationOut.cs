using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Alim_StorageOut_SimulationOut : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strGrpid = "";
        private string strCrdat = "";
        private DataTable dtDateTime = new DataTable();
        private DataTable dtIdData = new DataTable();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtOutSave = new DataTable();

        QCI.QWMS.Alim_Storage objAlimStorage;
        QCI.QWMS.Alim objAlim;
        #endregion

        #region DataMember

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

        public string Grpid
        {
            get
            {
                return strGrpid;
            }
            set
            {
                strGrpid = value;
            }
        }

        #endregion

        public Alim_StorageOut_SimulationOut(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;
            objAlim = new QCI.QWMS.Alim(UserData, Progid);
            objAlimStorage = new QCI.QWMS.Alim_Storage(UserData, Progid);

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

                    dtDateTime.Columns.Add("NowDate");
                    dtDateTime.Columns.Add("NowHour");
                    dtDateTime.Columns.Add("NowMinute");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Show 厂区、仓别、Status、DateTime、GroupID
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsUsrnm.Text = strUsrnm;
            this.stsComcd.Text = strComcd;

        }
        private void ShowDdlWerks()
        {
            stsWarning.Text = string.Empty;
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
        public DataTable GetDateTime()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;

            DataRow drRow = dtDateTime.NewRow();
            dtDateTime.Rows.Add(drRow);
            dtDateTime.Rows[0]["NowDate"] = currentTime.Date.ToString("yyyy-MM-dd");
            dtDateTime.Rows[0]["NowHour"] = currentTime.Hour;
            dtDateTime.Rows[0]["NowMinute"] = currentTime.Minute;

            return dtDateTime;
        }
        private void ShowGroupId()
        {
            stsWarning.Text = "";
            try
            {
                //已扣帳的id不show出來
                if (chkAllId.Checked)
                {
                    dtIdData = objAlimStorage.QueryGroupIdData(strWerks, strLgort, strCrdat);
                }
                else
                {
                    GetDateTime();
                    dtIdData = objAlimStorage.QueryGroupIdData(strWerks, strLgort, strCrdat, dtDateTime);
                }

                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        cmbSendID.Items.Add(dtIdData.Rows[i]["GRPID"]);

                    }
                    this.cmbSendID.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "No id data!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion

        #region 厂区仓别选择
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            else
            {
                stsWarning.Text = "厂区/仓别不能为空!!";
                return;
            }

            cmbSendID.Items.Clear();
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

            //秀Send/Group id
            ShowGroupId();
        }
        #endregion

        #region Function
        private void btnProduce_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 产生虚拟单据
                DataSet dsData = objAlimStorage.ProduceSapSimulationData(dtOutSource);
                DataTable dtMblnr = dsData.Tables[0];
                dtOutSave = dsData.Tables[1];
                if (dtMblnr.Rows.Count > 0)
                {
                    ShowDataGrid(dtOutSave);
                    stsWarning.Text = "已產生扣帳單據...";
                    btnProduce.Enabled = false;
                    btnSave.Enabled = true;
                    btnRefresh.Enabled = false;
                    btnExit.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "產生扣帳單據失敗，請確認!! (Error: " + objAlimStorage.ERRMSG + " )";
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 出库数据插入出库中间表、库存状态更新待下架、WHDWN更新、加扣记录
                if (objAlimStorage.StorageOut_SimulationOut(dtOutSave))
                {
                    stsWarning.Text = "已增加出库需求";
                    btnSave.Enabled = false;
                    btnRefresh.Enabled = true;
                    btnExit.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "添加出库需求失败，请重新点击Save";
                    return;
                }
                #endregion

                //同步出库信息到QMS，调用QMS OutStore API接口
                ///////待写///////
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            this.cmbWerks.SelectedIndex = -1;
            this.cmbLgort.SelectedIndex = -1;
            this.cmbSendID.Items.Clear();
            this.cmbSendID.Text = string.Empty;
            this.lblOutSource.Text = "0 records";
            this.strWerks = string.Empty;
            this.strLgort = string.Empty;
            this.strGrpid = string.Empty;
            this.btnProduce.Enabled = false;
            this.btnSave.Enabled = false;
            this.dgvData.DataSource = null;
            dtDateTime.Rows.Clear();
            dtIdData.Rows.Clear();
            dtOutSource = new DataTable();
            dtOutSave = new DataTable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid(DataTable dtData)
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 50;
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 50;
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

                //COSCT
                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "KOSTL";
                dgvcCosct.HeaderText = "Material_Cost_Center";
                dgvcCosct.Width = 90;
                dgvcCosct.ReadOnly = true;
                dgvData.Columns.Add(dgvcCosct);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "LOCAT";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvData.Columns.Add(dgvcLocat);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMatnr);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Charg";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                dgvData.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage out Qty";
                dgvcMenge.Width = 60;
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                //LOCOD
                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "Lot Code";
                dgvcLocod.Width = 90;
                dgvcLocod.ReadOnly = true;
                dgvData.Columns.Add(dgvcLocod);

                //DACOD
                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "Date Code";
                dgvcDacod.Width = 90;
                dgvcDacod.ReadOnly = true;
                dgvData.Columns.Add(dgvcDacod);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor Code";
                dgvcLifnr.Width = 50;
                dgvcLifnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcLifnr);

                //Receipt Storage
                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "Receipt Storage";
                dgvcUmlgo.Width = 50;
                dgvcUmlgo.ReadOnly = true;
                dgvData.Columns.Add(dgvcUmlgo);

                //GRPID
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.Width = 140;
                dgvcGrpid.ReadOnly = true;
                dgvData.Columns.Add(dgvcGrpid);

                //DIDNO
                DataGridViewTextBoxColumn dgvcDidno = new DataGridViewTextBoxColumn();
                dgvcDidno.DataPropertyName = "DIDNO";
                dgvcDidno.HeaderText = "DIDNO";
                dgvcDidno.Width = 140;
                dgvcDidno.ReadOnly = true;
                dgvData.Columns.Add(dgvcDidno);

                //SERNO
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "虚拟单据";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMblnr);

                //SERNO
                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "SERNO";
                dgvcSerno.Width = 90;
                dgvcSerno.ReadOnly = true;
                dgvData.Columns.Add(dgvcSerno);

                //TRDAT
                DataGridViewTextBoxColumn dgvcTrdat = new DataGridViewTextBoxColumn();
                dgvcTrdat.DataPropertyName = "CRDAT";
                dgvcTrdat.HeaderText = "TransDate Time";
                dgvcTrdat.Width = 90;
                dgvcTrdat.ReadOnly = true;
                dgvData.Columns.Add(dgvcTrdat);

                dgvData.DataSource = dtData;
                lblOutSource.Text = dtData.Rows.Count.ToString() + " records";

                if (dtData.Rows.Count > 0)
                {
                    this.btnProduce.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSmtDataGrid()");
            }
        }
        #endregion

        private void cmbSendID_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if(cmbSendID.SelectedItem.ToString()!="")
            {
                Grpid = cmbSendID.Items[cmbSendID.SelectedIndex].ToString();
            }
            else
            {
                stsWarning.Text = "Send id can't be empty!!";
                return;
            }

            dtIdData = objAlimStorage.QuerySmtIDData(Werks, Lgort, Grpid);
            if (dtIdData.Rows.Count == 0)
            {
                stsWarning.Text = "No SMT data!!";
                return;
            }
            confirm();
        }
        private void confirm()
        {
            try
            {
                DataTable dtTemp = dtIdData.Clone();
                foreach (DataRow dr in dtIdData.Rows)
                {
                    #region 校验每笔数据当前的库存情况
                    if (objAlimStorage.IsOutStore(dr["WERKS"].ToString(), dr["LGORT"].ToString(), dr["MATNR"].ToString(), dr["LOCAT"].ToString(), dr["MENGE"].ToString()))
                    {
                        dtTemp.ImportRow(dr);
                    }
                    #endregion
                }
                dtIdData.Clear();
                dtIdData = dtTemp.Copy();

                #region 待出库数据
                if (dtIdData.Rows.Count > 0)
                {
                    dtOutSource = dtIdData.Clone();
                    dtOutSource.Columns.Add("PRI");//单据优先级
                    dtOutSource.Columns.Add("SUBPRI");//储位优先级
                    dtOutSource.Columns.Add("INSMK");//G                    
                    dtOutSource.Columns.Add("DACOD_before");
                    foreach (DataRow drID in dtIdData.Rows)
                    {
                        #region 储位优先级和柜号
                        string strLocat = drID["LOCAT"].ToString();
                        DataTable dtLocat = objAlimStorage.getLocatPRI(strWerks, strLgort, strLocat);
                        #endregion
                        DataRow drRow = dtOutSource.NewRow();
                        drRow["MANDT"] = UserData.Client;
                        drRow["COMCD"] = UserData.CompanyCode;
                        drRow["WERKS"] = drID["WERKS"].ToString();
                        drRow["LGORT"] = drID["LGORT"].ToString();
                        drRow["PRI"] = 5;//祥龙出库单据优先级为5
                        drRow["SUBPRI"] = dtLocat.Rows[0]["PRI"].ToString();
                        drRow["LOCAT"] = strLocat;
                        drRow["DACOD"] = drID["DACOD"].ToString();
                        drRow["LOCOD"] = drID["LOCOD"].ToString();
                        drRow["LIFNR"] = drID["LIFNR"].ToString();
                        drRow["KOSTL"] = drID["KOSTL"].ToString();  //Cost Center
                        drRow["MATNR"] = drID["MATNR"].ToString();
                        drRow["INSMK"] = 'G';
                        drRow["CHARG"] = drID["CHARG"].ToString();
                        drRow["MENGE"] = drID["MENGE"].ToString();  //要出的數量
                        drRow["GRPID"] = drID["GRPID"].ToString();
                        drRow["CRDAT"] = drID["CRDAT"].ToString();
                        drRow["UMLGO"] = drID["UMLGO"].ToString();  //收料倉
                        drRow["DIDNO"] = drID["DIDNO"].ToString();
                        drRow["MBLNR"] = drID["MBLNR"].ToString();
                        drRow["Line"] = drID["Line"].ToString();
                        drRow["Side"]=drID["Side"].ToString();
                        drRow["Machine"]=drID["Machine"].ToString();
                        drRow["SDTSlot"]=drID["SDTSlot"].ToString();
                        drRow["SDTLr"] = drID["SDTLr"].ToString();
                        dtOutSource.Rows.Add(drRow);
                    }
                    string strOrderBy = "MATNR";
                    dtOutSource = CommonInfo.SortDataTable(dtOutSource, strOrderBy);
                    ShowDataGrid(dtOutSource);

                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }
    }
}
