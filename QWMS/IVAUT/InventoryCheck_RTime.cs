using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class InventoryCheck_RTime : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private string strInvNo = "";
        private string strQty = "";
        string strCode = "";
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private DataTable dtData;
        private DataTable dtStorage = new DataTable();
        private DataTable dtError = new DataTable();
        private DataTable dtInvNO = new DataTable();
        int a = 0;//该储位已盘条数（最少盘一条才能设为已盘，全盘必须设成已盘）
        int type = 0;//type=1自动设为已盘
        string setsLocat = "";
        List<string> strList = new List<string>();//防止DID重复刷入
        private DataTable dtBoxPlant = new DataTable();//ScanBoxid厂区仓别
        private DataTable dtBoxSNPlant = new DataTable();//ScanSNBoxid厂区仓别
        private StorageIn objStorageIn;
        private StorageData objStorageData;



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
        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
            }
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
        public DataTable Storage
        {
            get
            {
                return dtStorage;
            }
            set
            {
                dtStorage = value;
            }
        }
        public string InvNo
        {
            get
            {
                return strInvNo;
            }
            set
            {
                strInvNo = value;
            }
        }
        #endregion

        #region 构造函数
        public InventoryCheck_RTime(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Data = dtData;
            try
            {
                objCounting = new Counting(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);
                objStorageData = new StorageData(UserData);
                //檢查權限
                if (!objCounting.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //show出Status的資料
                    ShowStatusData();
                    dtBoxPlant = objCounting.InvWerks("BoxID");
                    dtBoxSNPlant = objCounting.InvWerks("SN");
                    ShowDdlWerks();
                    ShowDdlLgort();

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
        #endregion
    
        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objCounting.InvWerks("RInv");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["CTRLNM"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.lblitm.Text = "总笔数:";
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            ShowDdlLgort();
        }
        #endregion
        
        # region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objCounting.InvLgort(strWerks);
                }
                else
                {
                    strWerks = "";
                    dtTemp = objCounting.InvLgort(strWerks);
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                    }
                    cmbLgort.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            this.lblitm.Text = "总笔数:";
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            if (dtBoxPlant.Select("WERKS='" + strWerks + "' AND LGORT='" + strLgort + "'").Length > 0)
            {
                rdoBoxid.Visible = true;
                rdoBoxid.Checked = true;
                txtQty.Enabled = false;
            }
            else if (dtBoxSNPlant.Select("WERKS='" + strWerks + "' AND LGORT='" + strLgort + "'").Length > 0)
            {
                rdoSN.Visible = true;
                rdoSN.Checked = true;
                txtQty.Enabled = false;
            }
            else
            {
                rdoBoxid.Visible = false;
                rdoBoxid.Checked = false;
                rdoSN.Visible = false;
                rdoSN.Checked = false;
                txtQty.Enabled = true;
            }
        }
        # endregion

        #region ShowDataGrid
        
        #region ShowErrorData
        private void ShowErrorData()
        {
            dgvError.AutoGenerateColumns = false;
            dgvError.Columns.Clear();
            dgvError.RowTemplate.Height = 20;
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 65;
                dgvError.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvError.Columns.Add(dgvcMatnr);

                if (rdoBoxid.Checked || rdoSN.Checked || (objPlantData.CheckCHARGLGORT(strWerks)) )
                {
                    DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                    dgvcCharg.DataPropertyName = "CHARG";
                    dgvcCharg.HeaderText = "版本";
                    dgvcCharg.ReadOnly = true;
                    dgvcCharg.Width = 50;
                    dgvError.Columns.Add(dgvcCharg);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "QWMS数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 70;
                dgvError.Columns.Add(dgvcMenge);

                if (!rdoSN.Checked)
                {
                    //QWMSDateCode
                    DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                    dgvcDacod.DataPropertyName = "DACOD";
                    dgvcDacod.HeaderText = "QWMSDateCode";
                    dgvcDacod.ReadOnly = true;
                    dgvcDacod.Width = 80;
                    dgvError.Columns.Add(dgvcDacod);
                }

                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "刷入数量";
                dgvcScqty.ReadOnly = true;
                dgvcScqty.Width = 70;
                dgvError.Columns.Add(dgvcScqty);

                if (!rdoSN.Checked)
                {
                    //刷入DateCode
                    DataGridViewTextBoxColumn dgvcScdac = new DataGridViewTextBoxColumn();
                    dgvcScdac.DataPropertyName = "SCDAC";
                    dgvcScdac.HeaderText = "刷入DateCode";
                    dgvcScdac.ReadOnly = true;
                    dgvcScdac.Width = 110;
                    dgvError.Columns.Add(dgvcScdac);
                }
                
                DataGridViewTextBoxColumn dgvcDifqty = new DataGridViewTextBoxColumn();
                dgvcDifqty.DataPropertyName = "MGDIF";
                dgvcDifqty.HeaderText = "差异";
                dgvcDifqty.ReadOnly = true;
                dgvcDifqty.Width = 70;
                dgvError.Columns.Add(dgvcDifqty);

                DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                dgvcStats.DataPropertyName = "STATS";
                dgvcStats.HeaderText = "状态";
                dgvcStats.ReadOnly = true;
                dgvcStats.Width = 60;
                dgvError.Columns.Add(dgvcStats);

                DataGridViewTextBoxColumn dgvcRmark = new DataGridViewTextBoxColumn();
                dgvcRmark.DataPropertyName = "RMARK";
                dgvcRmark.HeaderText = "异常";
                dgvcRmark.ReadOnly = true;
                dgvcRmark.Width = 90;
                dgvError.Columns.Add(dgvcRmark);

                dgvError.DataSource = dtError;
                lblCount1.Text = dtError.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowErrorData()");
            }
        }
        # endregion
   
        #region  ShowData
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            dgvData.RowTemplate.Height = 20;
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 70;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                if (rdoBoxid.Checked || rdoSN.Checked || (objPlantData.CheckCHARGLGORT(strWerks)))
                {
                    DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                    dgvcCharg.DataPropertyName = "CHARG";
                    dgvcCharg.HeaderText = "版本";
                    dgvcCharg.ReadOnly = true;
                    dgvcCharg.Width = 50;
                    dgvData.Columns.Add(dgvcCharg);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "QWMS数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 80;
                dgvData.Columns.Add(dgvcMenge);
                //DC
                if (!rdoSN.Checked)
                {
                    DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                    dgvcDacod.DataPropertyName = "DACOD";
                    dgvcDacod.HeaderText = "QWMSDateCode";
                    dgvcDacod.ReadOnly = true;
                    dgvcDacod.Width = 80;
                    dgvData.Columns.Add(dgvcDacod);
                }

                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "刷入数量";
                dgvcScqty.ReadOnly = true;
                dgvcScqty.Width = 80;
                dgvData.Columns.Add(dgvcScqty);

                //刷入DateCoe
                if (!rdoSN.Checked)
                {
                    DataGridViewTextBoxColumn dgvcScdac = new DataGridViewTextBoxColumn();
                    dgvcScdac.DataPropertyName = "SCDAC";
                    dgvcScdac.HeaderText = "刷入DateCode";
                    dgvcScdac.ReadOnly = true;
                    dgvcScdac.Width = 110;
                    dgvData.Columns.Add(dgvcScdac);
                }


                DataGridViewTextBoxColumn dgvcDifqty = new DataGridViewTextBoxColumn();
                dgvcDifqty.DataPropertyName = "MGDIF";
                dgvcDifqty.HeaderText = "差异";
                dgvcDifqty.ReadOnly = true;
                dgvcDifqty.Width = 70;
                dgvData.Columns.Add(dgvcDifqty);

                DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                dgvcStats.DataPropertyName = "STATS";
                dgvcStats.HeaderText = "状态";
                dgvcStats.ReadOnly = true;
                dgvcStats.Width = 60;
                dgvData.Columns.Add(dgvcStats);

                DataGridViewTextBoxColumn dgvcRmark = new DataGridViewTextBoxColumn();
                dgvcRmark.DataPropertyName = "RMARK";
                dgvcRmark.HeaderText = "异常";
                dgvcRmark.ReadOnly = true;
                dgvcRmark.Width = 90;
                dgvData.Columns.Add(dgvcRmark);

                dgvData.DataSource = dtStorage;
                lblCount2.Text = dtStorage.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        # endregion
        
        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion
        
        #region 日期改变
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            this.lblitm.Text = "总笔数:";
        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbInvNo.Text = "";
            this.lblitm.Text = "总笔数:";
        }
        #endregion
       
        #region ShowInv：展现盘点票号
        private void RTShowInv()
        {
            try
            {
                stsWarning.Text = "";
                cmbInvNo.Items.Clear();
                dtInvNO = new DataTable();
                string strStartDate = "";
                string strEndDate = "";
                strStartDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
                strEndDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                    CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtInvNO = objCounting.ShowInv(strStartDate, strEndDate);
                for (int i = 0; i < dtInvNO.Rows.Count; i++)
                {
                    cmbInvNo.Items.Add(dtInvNO.Rows[i]["INVNO"].ToString());
                }
                if (dtInvNO.Rows.Count == 0)
                {
                    stsWarning.Text = "无盘点票!";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-RTShowInv()");
            }
        }
        private void cmbInvNo_Click(object sender, EventArgs e)
        {
            RTShowInv();
        }
        private void cmbInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblitm.Text = "总笔数:";
            stsWarning.Text = "";
            strInvNo = cmbInvNo.Items[cmbInvNo.SelectedIndex].ToString();
            showlblInvitm();
        }
        #endregion
       
        #region Locat-Enter：储位回车之后的操作
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = "";
            this.cmbWerks.Enabled = false;
            this.cmbLgort.Enabled = false;
            this.dtpStartDate.Enabled = false;
            this.dtpEndDate.Enabled = false;
            this.cmbInvNo.Enabled = false;
            //bool flag = false;//储位是否存在于盘点票中
            DataTable dtLocat = new DataTable();
            try
            {            
                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                   CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                    stsWarning.Text = "";
                    Clear();//清空数量和识别码文本框
                    strInvNo = cmbInvNo.Items[cmbInvNo.SelectedIndex].ToString();
                    strLocat = txtLocat.Text.ToString().Trim().ToUpper();
                    if (strLocat == "")
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        stsWarning.Text = "储位不能为空!";
                        return;
                    }
                    else
                    {
                        if (rdoBoxid.Checked)
                        {
                            if (dtError.Rows.Count > 0 ||  (dtStorage.Columns.Count == 0?false:dtStorage.Select("MGDIF<>0").Length > 0))
                            {
                                if (MessageBox.Show("未盘点完毕或有异常，不能设为已盘，是否继续更换储位?", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    Sound.Play(@"Sound\ERROR.wav");
                                    return;
                                }
                                else
                                {
                                    dtStorage.Clear();
                                }
                            }
                        }
                        else
                        {
                            if (dtError.Rows.Count > 0 && dtStorage.Rows.Count > 0)//储位至少盘一条才能设成已盘，全盘自动设成已盘。
                            {
                                ifINV();
                            }
                        }
                        strList.Clear();//换储位时清空上一储位刷入的DID
                        dtLocat = objCounting.getLocat(strInvNo);//获得该盘点票号的储位
                        if (dtLocat.Select("LOCAT='" + strLocat + "'").Length > 0)
                        {
                            if (rdoBoxid.Checked || rdoSN.Checked)
                            {
                                //--带出 表格的所有数据。
                                dtError = objCounting.QueryLocatITM(Locat);//获得该储位的库存数据
                            }
                            else
                            {
                                if (objPlantData.CheckCHARGLGORT(strWerks))
                                {
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                    {
                                        dtError = objCounting.LocatInfoPCB(Locat);//获得该储位的库存数据
                                    }
                                    else
                                    {
                                        dtError = objCounting.UnDCLocatDataPCB(Locat);//获得该储位的库存数据
                                    }
                                }
                                else
                                {
                                    //--带出表格和其他数据
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                    {
                                        dtError = objCounting.LocatInfo(Locat);//获得该储位的库存数据
                                    }
                                    else
                                    {
                                        dtError = objCounting.UnDCLocatData(Locat);//获得该储位的库存数据
                                    }
                                }                    
                            }
                            
                            if (dtError.Rows.Count == 0)
                            {
                                type = 1;
                                setsLocat = strLocat;
                                ifInvDT(1);//空储位自动设成已盘
                                this.txtLocat.Focus();
                                this.txtLocat.SelectAll();
                                return;
                            }
                            if (dtStorage.Columns.Count == 0)
                            {
                                dtStorage = dtError.Clone();
                            }
                            ShowErrorData();
                            ColorStats();
                            Sound.Play(@"Sound\BIU.wav");
                            this.txtCode.Focus();
                        }
                        else
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            MessageBox.Show("该储位不存在于该盘点票中！");
                            this.txtLocat.Focus();
                            this.txtLocat.SelectAll();
                        }
                        //for (int i = 0; i < dtLocat.Rows.Count; i++)
                        //{
                        //    if (dtLocat.Rows[i]["LOCAT"].ToString() == strLocat)
                        //    {
                        //        flag = true;
                        //        dtError = objCounting.LocatInfo(Locat);//获得该储位的库存数据
                        //        if (dtError.Rows.Count == 0)
                        //        {
                        //            type = 1;//自动设为已盘
                        //            setsLocat = strLocat;
                        //            ifInvDT(1);
                        //            this.txtLocat.Focus();
                        //            this.txtLocat.SelectAll();
                        //            return;
                        //        }
                        //        if (dtStorage.Columns.Count == 0)
                        //        {
                        //            dtStorage = dtError.Clone();
                        //        }
                        //        ShowErrorData();
                        //        ColorStats();
                        //        Sound.Play(@"Sound\BIU.wav");
                        //        this.txtCode.Focus();
                        //        break;
                        //    }
                        //}
                    }
                    //if (!flag)
                    //{
                    //    Sound.Play(@"Sound\ERROR.wav");
                    //    MessageBox.Show("该储位不存在于该盘点票中！");
                    //    //stsWarning.Text = "该储位不存在于该盘点票中！";
                    //    this.txtLocat.Focus();
                    //    this.txtLocat.SelectAll();
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-txtLocat_KeyDown()");
                //stsWarning.Text = ex.Message;
                //return;
            }
        }
        #endregion

        #region ifINV是否设为已盘
        /// <summary>
        /// ifINV：是否设为已盘
        /// 在更换储位，储位回车和save处判断是否设为已盘
        /// </summary>
        private void ifINV()
        {
            try
            {
                setsLocat = "";
                type = 0;
                if (a > 0)//最少盘点一条数据
                {
                    if (MessageBox.Show("是否将该储位设为已盘?", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        if (dtError.Rows.Count == 0)//如果已Scan完必须设成已盘
                        {
                            MessageBox.Show("不能设为未盘！");
                        }
                        else
                        {
                            dtStorage.Clear();
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dtError.Rows.Count; j++)
                        {
                            DataRow dr = dtStorage.NewRow();
                            dr["LOCAT"] = dtError.Rows[j]["LOCAT"].ToString();
                            dr["MATNR"] = dtError.Rows[j]["MATNR"].ToString();
                            dr["MENGE"] = dtError.Rows[j]["MENGE"].ToString();
                            if (dtError.Rows[j]["SCQTY"].ToString().Trim() == "")
                            {
                                dr["SCQTY"] = "0";
                                dr["MGDIF"] = dtError.Rows[j]["MENGE"].ToString();
                                dr["RMARK"] = "未刷入数量";
                            }
                            else
                            {
                                dr["SCQTY"] = dtError.Rows[j]["SCQTY"].ToString();
                                dr["MGDIF"] = dtError.Rows[j]["MGDIF"].ToString();
                                dr["RMARK"] = dtError.Rows[j]["RMARK"].ToString();
                            }
                            dr["STATS"] = "Y";
                            dtStorage.Rows.Add(dr);
                        }
                        //更新数据库
                        objCounting = new Counting(UserData, Werks, Lgort, Progid);
                        setsLocat = dtStorage.Rows[0]["LOCAT"].ToString().Trim();
                        if (objCounting.InventoryLoad(type, strInvNo, setsLocat, dtStorage, "RInv") && objCounting.InvLogLoad(setsLocat, strInvNo, dtStorage, "RInv"))
                        {
                            stsWarning.Text = "Insert OK!";
                            Sound.Play(@"Sound\NEWOK.wav");
                        }
                        else
                        {
                            stsWarning.Text = "Insert fail!! " + objCounting.ERRMSG;
                            Sound.Play(@"Sound\ERROR.wav");
                            return;
                        }
                    }
                    dtError.Clear();
                    dtStorage.Clear();
                    ShowDataGrid();
                    ShowErrorData();
                    a = 0;
                    this.txtLocat.Focus();
                    this.txtLocat.SelectAll();
                }
                else
                {
                    stsWarning.Text = "储位最少盘点一条数据！";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ifINV()");
                //stsWarning.Text = ex.Message;
                //return;
            }
        }
        #endregion

        #region ifInvDT：由dtError判断是否自动设为已盘
        private void ifInvDT(int type)
        {
            try
            {
                //更新数据库
                objCounting = new Counting(UserData, Werks, Lgort, Progid);
                if (type == 0)
                {
                    setsLocat = dtStorage.Rows[0]["LOCAT"].ToString().Trim();
                }
                if (rdoSN.Checked)
                {
                    if (objCounting.InventoryLoad(type, strInvNo, setsLocat, dtStorage, "Sn") && objCounting.InvLogLoad(setsLocat, strInvNo, dtStorage, "RInv"))
                    {
                        stsWarning.Text = "Insert OK!";
                        Sound.Play(@"Sound\NEWOK.wav");
                    }
                    else
                    {
                        stsWarning.Text = "Insert fail!! " + objCounting.ERRMSG;
                        Sound.Play(@"Sound\ERROR.wav");
                        return;
                    }
                }
                else
                {
                    if (objCounting.InventoryLoad(type, strInvNo, setsLocat, dtStorage, "RInv") && objCounting.InvLogLoad(setsLocat, strInvNo, dtStorage, "RInv"))
                    {
                        stsWarning.Text = "Insert OK!";
                        Sound.Play(@"Sound\NEWOK.wav");
                    }
                    else
                    {
                        stsWarning.Text = "Insert fail!! " + objCounting.ERRMSG;
                        Sound.Play(@"Sound\ERROR.wav");
                        return;
                    }
                }
                this.txtLocat.Focus();
                this.txtLocat.SelectAll();
                this.txtLocat.Text = "";
                dtError.Clear();
                dtStorage.Clear();
                ShowDataGrid();
                ShowErrorData();
                a = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ifInvDT()");
            }
        }
        #endregion

        #region txtCode：识别码回车之后的操作
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = "";
            strCode = txtCode.Text.ToString().Trim().ToUpper();
            int indexOfFirstSemicolon = strCode.IndexOf(';');
            if (indexOfFirstSemicolon != -1)
            {
                // 获取双引号到第一个分号之间的子字符串，并去除空格
                string substringToReplace = strCode.Substring(0, indexOfFirstSemicolon);
                substringToReplace = substringToReplace.Replace(" ", "");

                // 用处理后的子字符串替换原始字符串中的部分
                strCode = substringToReplace + strCode.Substring(indexOfFirstSemicolon);
            }

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (strCode == "")
                {
                    stsWarning.Text = "请扫描识别码！";
                    return;
                }
                else
                {
                    if (rdoBoxid.Checked)
                    {
                        #region ScanBoxID
                        if (strCode.Length != 16)
                        {
                            stsWarning.Text = "识别码输入有误！";
                            Sound.Play(@"Sound\ERROR.wav");
                            this.txtCode.Clear();
                            this.txtCode.Focus();
                            return;
                        }
                        else
                        {
                            DataTable dtDetail = new DataTable();
                            dtDetail = objCounting.QueryDetail(strCode);
                            if (dtDetail.Rows.Count == 1)
                            {
                                if (strList.Contains(strCode))
                                {
                                    Sound.Play(@"Sound\ERROR.wav");
                                    stsWarning.Text = "该BoxID已刷入！！";
                                    this.txtCode.Focus();
                                    this.txtCode.SelectAll();
                                    return;
                                }
                                else
                                {
                                    strList.Add(strCode);
                                }
                                string strMatnr = dtDetail.Rows[0]["MATNR"].ToString();
                                string strCharg = dtDetail.Rows[0]["CHARG"].ToString();
                                string strQty = dtDetail.Rows[0]["MENGE"].ToString();
                                if (dtError.Select("MATNR='" + strMatnr + "' AND CHARG='" + strCharg + "'").Length > 0)
                                {
                                    #region 该料号在dgvError
                                    for (int i = 0; i < dtError.Rows.Count; i++)
                                    {
                                        DataRow dr = dtStorage.NewRow();
                                        if (dtError.Rows[i]["MATNR"].ToString().Trim() == strMatnr && dtError.Rows[i]["CHARG"].ToString().Trim() == strCharg)
                                        {
                                            if (dtError.Rows[i]["STATS"].ToString().Trim() == "Y")
                                            {
                                                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(dtError.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                            }
                                            else
                                            {
                                                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(strQty);
                                            }
                                            dtError.Rows[i]["MGDIF"] = Convert.ToInt32(dtError.Rows[i]["MENGE"]) - Convert.ToInt32(dtError.Rows[i]["SCQTY"]);
                                            if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) == 0)
                                            {
                                                dtError.Rows[i]["RMARK"] = "";
                                                dr["LOCAT"] = dtError.Rows[i]["LOCAT"].ToString();
                                                dr["MATNR"] = dtError.Rows[i]["MATNR"].ToString();
                                                if (dtError.Columns.Contains("CHARG"))
                                                {
                                                    dr["CHARG"] = dtError.Rows[i]["CHARG"].ToString();
                                                }
                                                dr["MENGE"] = dtError.Rows[i]["MENGE"].ToString();
                                                dr["SCQTY"] = dtError.Rows[i]["SCQTY"].ToString();
                                                dr["MGDIF"] = dtError.Rows[i]["MGDIF"].ToString();
                                                dr["STATS"] = "Y";
                                                dr["RMARK"] = dtError.Rows[i]["RMARK"].ToString();
                                                dtStorage.Rows.Add(dr);
                                                dtError.Rows.RemoveAt(i);
                                                ShowErrorData();
                                                ShowDataGrid();
                                            }
                                            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) > 0)
                                            {
                                                dtError.Rows[i]["STATS"] = "Y";
                                                dtError.Rows[i]["RMARK"] = "数据异常 More";
                                            }
                                            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) < 0)
                                            {
                                                dtError.Rows[i]["STATS"] = "Y";
                                                dtError.Rows[i]["RMARK"] = "数据异常 Less";
                                            }
                                            Sound.Play(@"Sound\BIU.wav");
                                        }
                                    }
                                    ColorStats();
                                    #endregion
                                }
                                else if (dtStorage.Select("MATNR='" + strMatnr + "' AND CHARG='" + strCharg + "'").Length > 0)
                                {
                                    #region 该料号在dgvData
                                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                                    {
                                        if (dtStorage.Rows[i]["MATNR"].ToString().Trim() == strMatnr && dtStorage.Rows[i]["CHARG"].ToString().Trim() == strCharg)
                                        {
                                            dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                            dtStorage.Rows[i]["MGDIF"] = Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                            if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) == 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "";
                                            }
                                            else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) > 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 More";
                                            }
                                            else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) < 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 Less";
                                            }
                                            ShowDataGrid();
                                            Sound.Play(@"Sound\BIU.wav");
                                        }
                                    }
                                    ColorStats();
                                    #endregion
                                }
                                else
                                {
                                    Sound.Play(@"Sound\ERROR.wav");
                                    stsWarning.Text = "该BoxID的料号版本为" + strMatnr + "-" + strCharg + "，在该储位未找到匹配数据！！";
                                    return;
                                }
                                txtCode.Clear();
                                if (dtError.Rows.Count == 0 && dtStorage.Select("MGDIF<>0").Length == 0)
                                {
                                    ifInvDT(0);
                                }
                            }
                            else
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                stsWarning.Text = "该BoxID不存在！！";
                                this.txtCode.Focus();
                                this.txtCode.SelectAll();
                                return;
                            }
                        }
                        #endregion
                    }
                    else if (rdoSN.Checked)
                    {
                        #region ScanBoxID_SN
                        if (string.IsNullOrEmpty(strLocat))
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            stsWarning.Text = "请先刷入储位！！";
                            this.txtLocat.Focus();
                            return;
                        }
                        if (strCode.Length < 22)
                        {
                            stsWarning.Text = "识别码输入有误！";
                            Sound.Play(@"Sound\ERROR.wav");
                            this.txtCode.Clear();
                            this.txtCode.Focus();
                            return;
                        }
                        else
                        {
                            DataTable dtDetail = new DataTable();
                            dtDetail = objCounting.QueryDetail_SN(strCode, strLocat);
                            if (dtDetail.Rows.Count == 1)
                            {
                                if (strList.Contains(strCode))
                                {
                                    Sound.Play(@"Sound\ERROR.wav");
                                    stsWarning.Text = "该BoxID已刷入！！";
                                    this.txtCode.Focus();
                                    this.txtCode.SelectAll();
                                    return;
                                }
                                else
                                {
                                    strList.Add(strCode);
                                }
                                string strMatnr = dtDetail.Rows[0]["MATNR"].ToString();
                                string strCharg = dtDetail.Rows[0]["CHARG"].ToString();
                                string strQty = dtDetail.Rows[0]["MENGE"].ToString();
                                if (dtError.Select("MATNR='" + strMatnr + "' AND CHARG='" + strCharg + "'").Length > 0)
                                {
                                    #region 该料号在dgvError
                                    for (int i = 0; i < dtError.Rows.Count; i++)
                                    {
                                        DataRow dr = dtStorage.NewRow();
                                        if (dtError.Rows[i]["MATNR"].ToString().Trim() == strMatnr && dtError.Rows[i]["CHARG"].ToString().Trim() == strCharg)
                                        {
                                            if (dtError.Rows[i]["STATS"].ToString().Trim() == "Y")
                                            {
                                                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(dtError.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                            }
                                            else
                                            {
                                                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(strQty);
                                            }
                                            dtError.Rows[i]["MGDIF"] = Convert.ToInt32(dtError.Rows[i]["MENGE"]) - Convert.ToInt32(dtError.Rows[i]["SCQTY"]);
                                            if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) == 0)
                                            {
                                                dtError.Rows[i]["RMARK"] = "";
                                                dr["LOCAT"] = dtError.Rows[i]["LOCAT"].ToString();
                                                dr["MATNR"] = dtError.Rows[i]["MATNR"].ToString();
                                                if (dtError.Columns.Contains("CHARG"))
                                                {
                                                    dr["CHARG"] = dtError.Rows[i]["CHARG"].ToString();
                                                }
                                                dr["MENGE"] = dtError.Rows[i]["MENGE"].ToString();
                                                dr["SCQTY"] = dtError.Rows[i]["SCQTY"].ToString();
                                                dr["MGDIF"] = dtError.Rows[i]["MGDIF"].ToString();
                                                dr["STATS"] = "Y";
                                                dr["RMARK"] = dtError.Rows[i]["RMARK"].ToString();
                                                dtStorage.Rows.Add(dr);
                                                dtError.Rows.RemoveAt(i);
                                                ShowErrorData();
                                                ShowDataGrid();
                                            }
                                            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) > 0)
                                            {
                                                dtError.Rows[i]["STATS"] = "Y";
                                                dtError.Rows[i]["RMARK"] = "数据异常 More";
                                            }
                                            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) < 0)
                                            {
                                                dtError.Rows[i]["STATS"] = "Y";
                                                dtError.Rows[i]["RMARK"] = "数据异常 Less";
                                            }
                                            Sound.Play(@"Sound\BIU.wav");
                                        }
                                    }
                                    ColorStats();
                                    #endregion
                                }
                                else if (dtStorage.Select("MATNR='" + strMatnr + "' AND CHARG='" + strCharg + "'").Length > 0)
                                {
                                    #region 该料号在dgvData
                                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                                    {
                                        if (dtStorage.Rows[i]["MATNR"].ToString().Trim() == strMatnr && dtStorage.Rows[i]["CHARG"].ToString().Trim() == strCharg)
                                        {
                                            dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                            dtStorage.Rows[i]["MGDIF"] = Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                            if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) == 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "";
                                            }
                                            else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) > 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 More";
                                            }
                                            else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) < 0)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 Less";
                                            }
                                            ShowDataGrid();
                                            Sound.Play(@"Sound\BIU.wav");
                                        }
                                    }
                                    ColorStats();
                                    #endregion
                                }
                                else
                                {
                                    Sound.Play(@"Sound\ERROR.wav");
                                    stsWarning.Text = "该BoxID的料号版本为" + strMatnr + "-" + strCharg + "，在该储位未找到匹配数据！！";
                                    return;
                                }
                                txtCode.Clear();
                                if (dtError.Rows.Count == 0 && dtStorage.Select("MGDIF<>0").Length == 0)
                                {
                                    ifInvDT(0);
                                }
                                txtQty.Text = strQty;
                            }
                            else
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                stsWarning.Text = "该BoxID不存在！！";
                                this.txtCode.Focus();
                                this.txtCode.SelectAll();
                                return;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (strCode.Length < 11)
                        {
                            stsWarning.Text = "识别码输入有误！";
                            Sound.Play(@"Sound\ERROR.wav");
                            this.txtCode.Clear();
                            this.txtCode.Focus();
                            return;
                        }
                        dtRowsChange();
                        ShowErrorData();
                        ColorStats();
                        //DID只刷入DID即可，不用刷数量，在识别码回车以及数量回车处都判断一下是否自动设成已盘
                        if (dtError.Rows.Count == 0 && dtStorage.Rows.Count > 0)
                        {
                            type = 0;
                            ifInvDT(0);
                        }
                    }
                }
            }
        }
        #endregion

        #region txtQty：数量
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = "";
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                strQty = txtQty.Text.ToString().Trim();
                if (txtQty.Text.ToString().Trim() == "")
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    stsWarning.Text = "数量不能为空！";
                }
                else
                {
                    int num = 0;
                    if (!int.TryParse(strQty, out num))
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        MessageBox.Show("数量必须为数字！");
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                        return;
                    }
                    dtRowsChange();
                    ShowErrorData();
                    ShowDataGrid();
                    ColorStats();
                    Sound.Play(@"Sound\BIU.wav");
                    this.txtCode.Focus();
                    if (dtError.Rows.Count == 0 && dtStorage.Rows.Count > 0)
                    {
                        type = 0;
                        ifInvDT(0);
                    }
                }
            }
        }
        #endregion
   
        #region dtRowsChange 两表之间的变化
        public void dtRowsChange()
        {
            stsWarning.Text = "";
            string strMatnr = "";
            //定义Datecode 刷入的DateCode 
            string strDacod = "";
            string strScdac = "";
            string strVendorcode = "";
            string strCharg = "";
            string strdc_after = "";
            string strDID = "";
            bool flag = false;
            string strDIDFLAG = "";
            DataTable dtMatnrLocat = new DataTable();
            DataTable dtDIDMenge = new DataTable();
            strCode = txtCode.Text.ToString().Trim().ToUpper();
            strLocat = txtLocat.Text.ToString().Trim().ToUpper();
            string[] strSplit = strCode.Replace('；',';').Split(new char[] { ';' });

            try
            {
                #region code here

                #region 判断识别码
                #region 判断二维码
                if (strSplit.Length >= 5)
                {
                    string strFristMatnr = "";
                    string strSecondMatnr = "";
                    int f = 0;//标记二维码的类型
                    strFristMatnr = strSplit[0].ToUpper();
                    strSecondMatnr = strSplit[1].ToUpper();
                    strMatnr = strSplit[0].ToUpper();
                    for (int i = 0; i < dtError.Rows.Count; i++)
                    {
                        if (dtError.Rows[i]["MATNR"].ToString().Trim() == strFristMatnr)
                        {
                            f = 1;
                        }
                        if (dtError.Rows[i]["MATNR"].ToString().Trim() == strSecondMatnr)
                        {
                            f = 2;
                        }
                    }
                    if (f == 0)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {
                            if (dtStorage.Rows[i]["MATNR"].ToString().Trim() == strFristMatnr)
                            {
                                f = 1;
                            }
                            if (dtStorage.Rows[i]["MATNR"].ToString().Trim() == strSecondMatnr)
                            {
                                f = 2;
                            }
                        }
                    }
                    if (f == 1)
                    {
                        //CS11622FB15;20190523;20180412;WDFZWDF;100; （有datecode信息）；
                        strMatnr = strSplit[0].ToUpper();
                        //分割识别码 分割顺序按照
                        strScdac = strSplit[1].Trim().ToUpper();
                        strVendorcode = strSplit[2].Trim().ToUpper();
                        if (strMatnr.Substring(0,2)=="SA" || strMatnr.Substring(0, 2) == "DA")
                        {
                            strCharg = strVendorcode.Substring((strVendorcode.Length) - 3);
                        }
                    }
                    if (f == 2)
                    {
                        strMatnr = strSplit[1].ToUpper();
                    }
                    strQty = strSplit[4];
                }
                #endregion
                #region 判断DID
                else if (strCode.Length > 11 && strCode.Contains("-"))
                {
                    int f = 0;
                    int c = 0;
                    strDID = strCode;

                    DataTable dt = new DataTable();
                    dt = objCounting.Query_WHRID(strDID);//查询判退
                    if (dt.Rows.Count == 1)
                    {
                        for (int j = 0; j < strList.Count; j++)
                        {
                            if (strList[j].ToString().Trim() == strDID)
                            {
                                c = 1;//DID已盘
                                Sound.Play(@"Sound\ERROR.wav");
                                stsWarning.Text = "该DID已盘！";
                                this.txtCode.Focus();
                                this.txtCode.SelectAll();
                                return;
                            }
                        }
                        if (c == 0)
                        {
                            strList.Add(strDID);
                            strMatnr = strCode.Substring(0, 11).ToUpper();
                            }
                        f = 1;
                        strDIDFLAG = "Y";
                        strQty = Convert.ToInt32(dt.Rows[0]["MENGE"]).ToString().Trim();
                        strScdac = dt.Rows[0]["DACOD"].ToString().Trim();
                        strVendorcode = dt.Rows[0]["LIFNR"].ToString().Trim();
                        if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                        {
                            strCharg = strVendorcode.Substring((strVendorcode.Length) - 3);
                        }
                    }
                    else
                    {
                        dtDIDMenge = objCounting.DIDMenge(Locat);
                        for (int i = 0; i < dtDIDMenge.Rows.Count; i++)
                        {
                            if (dtDIDMenge.Rows[i]["MBLNR"].ToString().Trim() == strDID)
                            {
                                for (int j = 0; j < strList.Count; j++)
                                {
                                    if (strList[j].ToString().Trim() == strDID)
                                    {
                                        c = 1;//DID已盘
                                        Sound.Play(@"Sound\ERROR.wav");
                                        stsWarning.Text = "该DID已盘！";
                                        this.txtCode.Focus();
                                        this.txtCode.SelectAll();
                                        return;
                                    }
                                }
                                if (c == 0)
                                {
                                    strList.Add(strDID);
                                    strMatnr = strCode.Substring(0, 11).ToUpper();
                                }
                                f = 1;//该DID存在该储位
                                //strList.Add(dtDIDMenge.Rows[i]["MBLNR"].ToString().Trim());
                                strQty = Convert.ToInt32(dtDIDMenge.Rows[i]["didMENGE"]).ToString().Trim();
                            }
                        }
                    }
                    if (f == 0)
                    {
                        DataTable dtMatnr = new DataTable();
                        string strLocatByMatnr = "";
                        dtMatnr = objCounting.getLocatByDID(strDID);
                        if (dtMatnr.Rows.Count != 0)
                        {
                            for (int j = 0; j < dtMatnr.Rows.Count; j++)
                            {
                                strLocatByMatnr = strLocatByMatnr + "\n" + "储位:" + dtMatnr.Rows[j]["LOCAT"] + "数量:" + dtMatnr.Rows[j]["MENGE"] + "\n";
                            }
                            Sound.Play(@"Sound\ERROR.wav");
                            MessageBox.Show("DID不存在该储位！\n料号" + strDID + "存在于该仓的以下储位:" + strLocatByMatnr);
                        }
                        else
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            stsWarning.Text = "该DID不存在于该仓！";
                        }
                        this.txtCode.Focus();
                        this.txtCode.SelectAll();
                        return;
                    }
                }
                #endregion
                #region 判断一维码
                else if (strCode.Length >= 11)
                {
                    strMatnr = strCode.Substring(0, 11).ToUpper();
                    strQty = txtQty.Text.ToString().Trim();
                    this.txtQty.Focus();
                }
                #endregion
                else
                {
                    stsWarning.Text = "识别码格式有误，请重新扫描！";
                    Sound.Play(@"Sound\ERROR.wav");
                    this.txtCode.Focus();
                    this.txtCode.SelectAll();
                    return;
                }
                #endregion
                #region 仓别对应数据
                if (objPlantData.CheckCHARGLGORT(strWerks))
                {
                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                    {
                        dtMatnrLocat = objCounting.LocatInfoPCB(strLocat);
                    }
                    else
                    {
                        dtMatnrLocat = objCounting.UnDCLocatDataPCB(strLocat);
                    }
                }
                else
                {
                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                    {
                        dtMatnrLocat = objCounting.LocatInfo(strLocat);
                    }
                    else
                    {
                        dtMatnrLocat = objCounting.UnDCLocatData(strLocat);
                    }
                }
                #endregion
                if (dtMatnrLocat.Select("MATNR='" + strMatnr + "'").Length > 0)
                {
                    #region 料号存在该储位
                    Sound.Play(@"Sound\BIU.wav");
                    if (strQty.Trim().Length > 0)
                    {
                        #region 该料号在dgvError
                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            DataRow dr = dtStorage.NewRow();            
                            if (dtError.Rows[i]["MATNR"].ToString().Trim() == strMatnr)
                            {
                                if (objPlantData.CheckCHARGLGORT(strWerks))
                                {
                                    if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                                    {
                                        if (!string.IsNullOrEmpty(dtError.Rows[i]["CHARG"].ToString().Trim()))
                                        {
                                            if (dtError.Rows[i]["CHARG"].ToString().Trim() != strCharg)
                                            {
                                                dtError.Rows[i]["RMARK"] = "刷入版本与当前库存版本不符" + ";" + dtError.Rows[i]["CHARG"].ToString().Trim() + ";" + strCharg;
                                                this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                            }
                                        }
                                    }
                                }
                                flag = true;
                                strdc_after = objCounting.SelectDetail(strMatnr, strLocat);
                                #region DateCode判断 转换问题
                                if (objPlantData.CheckDACODLGORT(strWerks, strLgort) && strSplit.Length < 9)
                                {
                                    if (((strSplit.Length >= 5 && strMatnr == strSplit[0].ToUpper()) || (strDIDFLAG == "Y")) && strdc_after != strScdac)
                                    {
                                        DialogResult result;
                                        //WHDCR表的修改权限
                                        if (objStorageIn.CheckDatecode(Usrnm))
                                        {
                                            #region 有修改权限
                                            result = MessageBox.Show("盘点Scan实物与系统信息不符，是否同步实物D/C信息?", "提示信息", MessageBoxButtons.OKCancel);
                                            if (result == DialogResult.OK)
                                            {
                                                bool Vencode = false;
                                                //二次刷入查询库存中的DACOD
                                                strDacod = objCounting.SelectDetail(strMatnr, strLocat);
                                                //VEDAT格式转换
                                                string DC_After = objCounting.WHDCR_Query(strScdac, strVendorcode);


                                                if (DC_After != "")
                                                {
                                                    DateTime dtVedat = new DateTime();
                                                    dtVedat = Convert.ToDateTime(DC_After);
                                                    DC_After = dtVedat.ToString("yyyyMMdd");
                                                    //更新 update DateCode VEDAT
                                                    Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMatnr, strLocat);
                                                    strdc_after = DC_After;
                                                }
                                                else
                                                {
                                                    string strTemp = objCounting.getDCTrans(strVendorcode, strScdac).ToString();
                                                    if (!string.IsNullOrEmpty(strTemp))
                                                    {
                                                        //新增规则 记录人员！
                                                        //插入！！！人工维护 作业人工号 更新时间
                                                        DataTable dtNewDateCode = new DataTable();
                                                        dtNewDateCode.Columns.Add("LIFNR");
                                                        dtNewDateCode.Columns.Add("DC_Before");
                                                        dtNewDateCode.Columns.Add("DC_After");

                                                        DataRow drnr = dtNewDateCode.NewRow();
                                                        drnr["LIFNR"] = strVendorcode;
                                                        drnr["DC_Before"] = strScdac;
                                                        drnr["DC_After"] = strTemp;
                                                        dtNewDateCode.Rows.Add(drnr.ItemArray);
                                                        string strTransType = "NEW";
                                                        objCounting.WHDCR_DML(dtNewDateCode, strTransType);

                                                        DC_After = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                                        strdc_after = DC_After;
                                                        //更新 update DateCode VEDAT
                                                        Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMatnr, strLocat);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("请先维护D/C转换规则");
                                                    }
                                                }
                                            }

                                        }
                                            #endregion
                                        else
                                        {
                                            MessageBox.Show("盘点Scan实物与系统信息不符,D/C转换请管理人员进行异常处理");
                                        }
                                    }
                                }
                                #endregion



                                if (dtError.Rows[i]["STATS"].ToString().Trim() == "Y")
                                {
                                    dtError.Rows[i]["SCQTY"] = Convert.ToInt32(dtError.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                }
                                else
                                {
                                    dtError.Rows[i]["SCQTY"] = Convert.ToInt32(strQty);
                                }
                                dtError.Rows[i]["MGDIF"] = Convert.ToInt32(dtError.Rows[i]["MENGE"]) - Convert.ToInt32(dtError.Rows[i]["SCQTY"]);
                                dtError.Rows[i]["DACOD"] = strdc_after;
                                dtError.Rows[i]["SCDAC"] = strScdac;
                                if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) == 0 )
                                {
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                    {
                                        if (strdc_after == strScdac)
                                        {
                                            dtError.Rows[i]["RMARK"] = "";
                                        }
                                        else
                                        {
                                            dtError.Rows[i]["RMARK"] = "DateCode不一致" + ";" + strVendorcode + ";" + strScdac;
                                        }
                                    }
                                    else
                                    {
                                        dtError.Rows[i]["RMARK"] = "";
                                    }                                 
                                    dr["LOCAT"] = dtError.Rows[i]["LOCAT"].ToString();
                                    dr["MATNR"] = dtError.Rows[i]["MATNR"].ToString();
                                    if (dtError.Columns.Contains("CHARG"))
                                    {
                                        dr["CHARG"] = dtError.Rows[i]["CHARG"].ToString();
                                    }
                                    dr["DACOD"] = strdc_after;
                                    dr["MENGE"] = dtError.Rows[i]["MENGE"].ToString();
                                    dr["SCDAC"] = strScdac;
                                    dr["SCQTY"] = dtError.Rows[i]["SCQTY"].ToString();
                                    dr["MGDIF"] = dtError.Rows[i]["MGDIF"].ToString();
                                    dr["STATS"] = "Y";
                                    dr["RMARK"] = dtError.Rows[i]["RMARK"].ToString();
                                    dtStorage.Rows.Add(dr);
                                    dtError.Rows.RemoveAt(i);
                                    ShowErrorData();
                                    ShowDataGrid();
                                    a = a + 1;
                                    Clear();
                                    return;
                                }
                                else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) > 0)
                                {
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                    {
                                        if (strdc_after == strScdac)
                                        {
                                            dtError.Rows[i]["RMARK"] = "数据异常 More";
                                        }
                                        else
                                        {
                                            dtError.Rows[i]["RMARK"] = "数据异常 More" + ";" + strVendorcode + ";" + strScdac;
                                        }
                                    }
                                   else
                                    {
                                        dtError.Rows[i]["RMARK"] = "数据异常 More";
                                    }
                                    dtError.Rows[i]["STATS"] = "Y";
                                    this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    a = a + 1;
                                }
                                else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) < 0)
                                {
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                    {
                                        if (strdc_after == strScdac)
                                        {
                                            dtError.Rows[i]["RMARK"] = "数据异常 Less";
                                        }
                                        else
                                        {
                                            dtError.Rows[i]["RMARK"] = "数据异常 Less" + ";" + strVendorcode + ";" + strScdac;
                                        }
                                    }
                                    else
                                    {
                                        dtError.Rows[i]["RMARK"] = "数据异常 Less";
                                    }
                                   
                                    dtError.Rows[i]["STATS"] = "Y";
                                    this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    a = a + 1;
                                }
                                Sound.Play(@"Sound\BIU.wav");
                                Clear();
                            }
                        }
                        #endregion

                        #region 该料号在dgvData
                        if (!flag)
                        {
                            for (int i = 0; i < dtStorage.Rows.Count; i++)
                            {

                                if (dtStorage.Rows[i]["LOCAT"].ToString().Trim() == strLocat && dtStorage.Rows[i]["MATNR"].ToString().Trim() == strMatnr)
                                {
                                    if (objPlantData.CheckCHARGLGORT(strWerks))
                                    {
                                        if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                                        {
                                            if (!string.IsNullOrEmpty(dtError.Rows[i]["CHARG"].ToString().Trim()))
                                            {
                                                if (dtStorage.Rows[i]["CHARG"].ToString().Trim() != strCharg)
                                                {
                                                    dtStorage.Rows[i]["RMARK"] = "刷入版本与当前库存版本不符" + ";" + dtStorage.Rows[i]["CHARG"].ToString().Trim() + ";" + strCharg;
                                                    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                                }
                                            }
                                        }
                                    }
                                    strdc_after = objCounting.SelectDetail(strMatnr, strLocat);
                                    #region DateCode判断 转换问题
                                    if (objPlantData.CheckDACODLGORT(strWerks, strLgort) && strSplit.Length < 9)
                                    {
                                        if (((strSplit.Length >= 5 && strMatnr == strSplit[0].ToUpper()) || (strDIDFLAG == "Y")) && strdc_after != strScdac)// 添加不为空 
                                        {
                                            if (objStorageIn.CheckDatecode(Usrnm))
                                            {
                                                DialogResult result;
                                                result = MessageBox.Show("盘点Scan实物与系统信息不符，是否同步实物D/C信息?", "提示信息", MessageBoxButtons.OKCancel);
                                                if (result == DialogResult.OK)
                                                {
                                                    bool Vencode = false;
                                                    strDacod = objCounting.SelectDetail(strMatnr, strLocat);
                                                    string DC_After = objCounting.WHDCR_Query(strScdac, strVendorcode);
                                                    if (DC_After != "")
                                                    {
                                                        DateTime dtVedat = new DateTime();
                                                        dtVedat = Convert.ToDateTime(DC_After);
                                                        DC_After = dtVedat.ToString("yyyyMMdd");
                                                        Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMatnr, strLocat);
                                                        strdc_after = DC_After;
                                                    }
                                                    else
                                                    {
                                                        string strTemp = objCounting.getDCTrans(strVendorcode, strScdac).ToString();
                                                        if (!string.IsNullOrEmpty(strTemp))
                                                        {
                                                            //新增规则 记录人员！
                                                            //插入！！！人工维护 作业人工号 更新时间
                                                            DataTable dtNewDateCode = new DataTable();
                                                            dtNewDateCode.Columns.Add("LIFNR");
                                                            dtNewDateCode.Columns.Add("DC_Before");
                                                            dtNewDateCode.Columns.Add("DC_After");

                                                            DataRow drnr = dtNewDateCode.NewRow();
                                                            drnr["LIFNR"] = strVendorcode;
                                                            drnr["DC_Before"] = strScdac;
                                                            drnr["DC_After"] = strTemp;
                                                            dtNewDateCode.Rows.Add(drnr.ItemArray);
                                                            string strTransType = "NEW";
                                                            objCounting.WHDCR_DML(dtNewDateCode, strTransType);

                                                            DC_After = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                                            strdc_after = DC_After;
                                                            Vencode = objCounting.UpdateDetail(strScdac, DC_After, strDacod, strMatnr, strLocat);
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("请先维护D/C转换规则");
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("盘点Scan实物与系统信息不符,请管理人员进行异常处理");
                                            }

                                        }
                                    }
                                    #endregion
                                    dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                                    dtStorage.Rows[i]["MGDIF"] = Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                    dtStorage.Rows[i]["DACOD"] = strdc_after;
                                    dtStorage.Rows[i]["SCDAC"] = strScdac;
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) == 0)
                                    {
                                         if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                         {
                                             if (strdc_after == strScdac)//QWMSDateCode 刷入DateCode是否一致
                                             {
                                                 dtStorage.Rows[i]["RMARK"] = "";
                                                 this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                                             }
                                             else
                                             {
                                                 dtStorage.Rows[i]["RMARK"] = "DateCode不一致" + ";" + strVendorcode + ";" + strScdac;
                                                 this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                             }
                                         }                                      
                                        else
                                        {
                                            dtStorage.Rows[i]["RMARK"] = "";
                                            this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                                        }
                                    }
                                    else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) > 0)
                                    {
                                        if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                        {
                                            if (strdc_after == strScdac)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 More";
                                            }
                                            else
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 More" + ";" + strVendorcode + ";" + strScdac;
                                            }
                                        }
                                        else
                                        {
                                            dtStorage.Rows[i]["RMARK"] = "数据异常 More";
                                        }                                     
                                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    }
                                    else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) < 0)
                                    {
                                        if (objPlantData.CheckDACODLGORT(strWerks, strLgort))
                                        {
                                            if (strdc_after == strScdac)
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 Less";
                                            }
                                            else
                                            {
                                                dtStorage.Rows[i]["RMARK"] = "数据异常 Less" + ";" + strVendorcode + ";" + strScdac;
                                            }
                                        }
                                        else
                                        {
                                            dtStorage.Rows[i]["RMARK"] = "数据异常 Less";
                                        }                                     
                                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    }
                                    ShowDataGrid();
                                    Sound.Play(@"Sound\BIU.wav");
                                    Clear();
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 料号不存在该储位
                    Sound.Play(@"Sound\ERROR.wav");
                    string strLocatByMatnr = "";
                    DataTable dtMatnr = new DataTable();
                    dtMatnr = objCounting.getLocatByMatnr(strMatnr);
                    if (dtMatnr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtMatnr.Rows.Count; i++)
                        {
                            strLocatByMatnr = strLocatByMatnr + "\n" + "储位:" + dtMatnr.Rows[i]["LOCAT"] + "数量:" + dtMatnr.Rows[i]["MENGE"];
                        }
                        MessageBox.Show("料号不存在该储位！\n料号" + strMatnr + "存在于该仓的以下储位:" + strLocatByMatnr);
                    }
                    else
                    {
                        stsWarning.Text = "料号不存在于该仓！";
                    }
                    this.txtQty.Clear();
                    this.txtCode.Focus();
                    this.txtCode.SelectAll();
                    #endregion
                }

                #region 不用
                //#region 料号是否存在
                ////int b = 0;
                //dtMatnrLocat = objCounting.LocatInfo(strLocat);
                //if (strMatnr.Trim().Length > 0)
                //{
                //    for (int i = 0; i < dtMatnrLocat.Rows.Count; i++)
                //    {
                //        if (dtMatnrLocat.Rows[i]["MATNR"].ToString().Trim() == strMatnr)
                //        {
                //            b = 1;
                //            Sound.Play(@"Sound\BIU.wav");
                //        }
                //    }
                //    if (b == 0)
                //    {                  
                //        Sound.Play(@"Sound\ERROR.wav");
                //        string strLocatByMatnr = "";
                //        DataTable dtMatnr = new DataTable();
                //        dtMatnr = objCounting.getLocatByMatnr(strMatnr);
                //        if (dtMatnr.Rows.Count > 0)
                //        {
                //            for (int i = 0; i < dtMatnr.Rows.Count; i++)
                //            {
                //                strLocatByMatnr = strLocatByMatnr + "\n" + "储位:" + dtMatnr.Rows[i]["LOCAT"] + "数量:" + dtMatnr.Rows[i]["MENGE"];
                //            }
                //            MessageBox.Show("料号不存在该储位！\n料号" + strMatnr + "存在于该仓的以下储位:" + strLocatByMatnr);
                //        }
                //        else
                //        {
                //            stsWarning.Text = "料号不存在于该仓！";
                //        }                      
                //        this.txtQty.Clear();
                //        this.txtCode.Focus();
                //        this.txtCode.SelectAll();
                //    }
                //}
                //#endregion

                //#region 料号存在该储位的操作
                //if (b == 1 && strQty.Trim().Length > 0)
                //{
                //    for (int i = 0; i < dtError.Rows.Count; i++)
                //    {
                //        DataRow dr = dtStorage.NewRow();
                //        if (dtError.Rows[i]["MATNR"].ToString().Trim() == strMatnr)
                //        {
                //            flag = true;
                //            if (dtError.Rows[i]["STATS"].ToString().Trim() == "Y")
                //            {
                //                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(dtError.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                //            }
                //            else
                //            {
                //                dtError.Rows[i]["SCQTY"] = Convert.ToInt32(strQty);
                //            }
                //            dtError.Rows[i]["MGDIF"] = Convert.ToInt32(dtError.Rows[i]["MENGE"]) - Convert.ToInt32(dtError.Rows[i]["SCQTY"]);
                //            if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) == 0)
                //            {
                //                dtError.Rows[i]["RMARK"] = "";
                //                dr["LOCAT"] = dtError.Rows[i]["LOCAT"].ToString();
                //                dr["MATNR"] = dtError.Rows[i]["MATNR"].ToString();
                //                dr["MENGE"] = dtError.Rows[i]["MENGE"].ToString();
                //                dr["SCQTY"] = dtError.Rows[i]["SCQTY"].ToString();
                //                dr["MGDIF"] = dtError.Rows[i]["MGDIF"].ToString();
                //                dr["STATS"] = "Y";
                //                dr["RMARK"] = dtError.Rows[i]["RMARK"].ToString();
                //                dtStorage.Rows.Add(dr);
                //                dtError.Rows.RemoveAt(i);
                //                ShowErrorData();
                //                ShowDataGrid();
                //                a = a + 1;
                //            }
                //            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) > 0)
                //            {
                //                dtError.Rows[i]["STATS"] = "Y";
                //                dtError.Rows[i]["RMARK"] = "数据异常 More";
                //                this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //                a = a + 1;
                //            }
                //            else if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) < 0)
                //            {
                //                dtError.Rows[i]["STATS"] = "Y";
                //                dtError.Rows[i]["RMARK"] = "数据异常 Less";
                //                this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //                a = a + 1;
                //            }
                //            Sound.Play(@"Sound\BIU.wav");
                //            Clear();
                //        }
                //    }
                //    if (!flag)
                //    {
                //        for (int i = 0; i < dtStorage.Rows.Count; i++)
                //        {
                //            if (dtStorage.Rows[i]["LOCAT"].ToString().Trim() == strLocat && dtStorage.Rows[i]["MATNR"].ToString().Trim() == strMatnr)
                //            {
                //                dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty);
                //                dtStorage.Rows[i]["MGDIF"] = Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                //                if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) == 0)
                //                {
                //                    dtStorage.Rows[i]["RMARK"] = "";
                //                    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                //                }
                //                else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) > 0)
                //                {
                //                    dtStorage.Rows[i]["RMARK"] = "数据异常 More";
                //                    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //                }
                //                else if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) < 0)
                //                {
                //                    dtStorage.Rows[i]["RMARK"] = "数据异常 Less";
                //                    this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //                }
                //                ShowDataGrid();
                //                Sound.Play(@"Sound\BIU.wav");
                //                Clear();
                //            }
                //        }
                //    }
                //}
                //#endregion
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion
      
        #region ColorStats：显示行颜色
        public void ColorStats()
        {
            try
            {
                for (int i = 0; i < dtError.Rows.Count; i++)
                {
                    if (Convert.ToString(dtError.Rows[i]["MGDIF"]).Trim() != "")
                    {
                        if (Convert.ToInt32(dtError.Rows[i]["MGDIF"]) > 0 || Convert.ToInt32(dtError.Rows[i]["MGDIF"]) < 0 || Convert.ToString(dtError.Rows[i]["RMARK"]) == "DateCode不一致")
                        {
                            this.dgvError.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtStorage.Rows[i]["MGDIF"]) == 0 && Convert.ToString(dtStorage.Rows[i]["RMARK"]) == "") 
                    {
                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) == 0)
                    {
                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        this.dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
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


        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
       
        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            a = 0;
            this.dgvData.DataSource = null;
            this.dgvError.DataSource = null;
            this.dtStorage.Clear();
            this.dtError.Clear();
            this.cmbInvNo.Text = "";
            this.cmbWerks.Text = "";
            this.cmbLgort.Text = "";
            this.dtInvNO.Clear();
            this.txtCode.Text = "";
            this.txtLocat.Text = "";
            this.txtQty.Text = "";
            this.lblCount1.Text = "0 records";
            this.lblCount2.Text = "0 records";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.cmbInvNo.Enabled = true;
            this.dtpStartDate.Enabled = true;
            this.dtpEndDate.Enabled = true;
            this.lblitm.Text = "总笔数:";
            setsLocat = "";
            type = 0;
            rdoBoxid.Visible = false;
            rdoBoxid.Checked = false;
            rdoSN.Visible = false;
            rdoSN.Checked = false;
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (rdoBoxid.Checked || rdoSN.Checked)
                {
                    if (dtError.Rows.Count > 0 || dtStorage.Select("MGDIF<>0").Length > 0)
                    {
                        MessageBox.Show("盘点数据存在异常，不能设为已盘，请处理后再盘点！！");
                        return;
                    }
                    else
                    {
                        ifInvDT(0);
                    }
                }
                else
                {
                    ifINV();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
       
        #region Clear
        private void Clear()
        {
            txtQty.Clear();
            txtCode.Clear();
        }
        #endregion
  
        #region 总笔数
        private void showlblInvitm()
        {
            stsWarning.Text = "";
            int invItm = 0;
            try
            {
                strInvNo = cmbInvNo.Text.ToString().Trim().ToUpper();
                objCounting = new Counting(UserData, Werks, Lgort, Progid);
                invItm = objCounting.InvITMCount(strInvNo);
                this.lblitm.Text = "总笔数:" + invItm;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

    }
}
