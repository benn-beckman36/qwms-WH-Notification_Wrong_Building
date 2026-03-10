using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI_QWMS_StorageData;
using System.Collections;
using QCI.QWMS;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace QWMS
{
    public partial class StorageOut_MergingBox_Print : Form
    {

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strType = "";
        private string strProgid = "";
        
        public string strIP = "";
        public int num;//打印数量
        private string[] str = new string[7];
        private string strWerks_PL = "";
        private string strLgort_PL = "";
            //
        private string strMblnr = "";
        private string Type = "";
        private string BoxId = "";
        private string UntieType = "";
        //打印数据集合
        List<string> listPrint = new List<string>();
        //private CarData objCarData;
        private DataTable dWData = new DataTable();//PrintLabel专用
        private DataTable allData = new DataTable();//存放WHDWN数据
        private DataTable meData = new DataTable();//存放归并后的数据
        //private DataTable dgvData = new DataTable();//GridView显示的数据，点击的按钮不同dgvData就会Copy allData或者meData的数据
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
        

        //public ArrayList Mblnrs
        //{
        //    get
        //    {
        //        return alMblnrs;
        //    }
        //    set
        //    {
        //        alMblnrs = value;
        //    }
        //}

        #endregion

        #region Constructor
        public StorageOut_MergingBox_Print(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            txtNum.Text = "1";
            PrintDetail.Enabled = false;
            //lblCompany.Text = Comcd;
            //lblUserid.Text = Usrnm;

            try
            {
                //QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                QCI.QWMS.Replenishment objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);


                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                //objCarData = new CarData(UserData);

                //檢查權限
                if (!objReplenishment.CheckAuthority())
                //if (false)
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlWerks_PL();
                    ShowDdlLgort_PL();//显示PL仓别
                    ShowDdlPLHour();//显示PL时间
                    ShowDdlPLEndHour();//显示截止时间

                    
                    
                    //ShowPrintCheckBox();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    if (cmbWerks_PL.Items.Count > 0)
                    {
                        this.cmbWerks_PL.SelectedIndex = 0;
                    }
                    if (cmbLgort_PL.Items.Count > 0)
                    {
                        this.cmbLgort_PL.SelectedIndex = 0;
                    }
                    string time = DateTime.Now.ToString("yyyyMMdd");
                    BindPrintSetting();
                    // ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                cmbWerks.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                // stsWarning.Text = string.Empty;
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
                    strLgort = string.Empty;
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            strWerks = cmbWerks.Text.ToString();
            strLgort = cmbLgort.Text.ToString();
        }

        #endregion

        #region 选择判票/SN、解绑
        private void rdoSN_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            Type = "SN";
            gbFunction.Enabled = false;
            txtBoxId.Enabled = false;
        }

        private void rdoMC_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            Type = "TIC";
            gbFunction.Enabled = false;
            txtBoxId.Enabled = false;
        }
        private void checkUntie_CheckedChanged(object sender, EventArgs e)
        {
            UntieType = "Untie";
        }

        #endregion  

        #region ShowWasteDataGrid
        private void ShowWasteDataGrid()
        {
            dgvWaste.AutoGenerateColumns = false;
            dgvWaste.Columns.Clear();

            try
            {
                //MANDT   
                //DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                //dgvcMANDT.DataPropertyName = "MANDT";
                //dgvcMANDT.HeaderText = "MANDT";
                //dgvcMANDT.Width = 50;
                //dgvcMANDT.ReadOnly = true;
                //enddgvEC.Columns.Add(dgvcMANDT);

                //COMCD
                //DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                //dgvcCOMCD.DataPropertyName = "COMCD";
                //dgvcCOMCD.HeaderText = "COMCD";
                //dgvcCOMCD.Width = 50;
                //dgvcCOMCD.ReadOnly = true;
                //enddgvEC.Columns.Add(dgvcCOMCD);

                //WERKS
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "WERKS";
                dgvcWERKS.Width = 80;
                dgvcWERKS.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcWERKS);

                //ORDERNO 车辆信息
                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "LGORT";
                dgvcLGORT.Width = 120;
                dgvcLGORT.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcLGORT);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 120;
                dgvcMBLNR.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMBLNR);

                //MATNR
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Width = 120;
                dgvcMATNR.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMATNR);

                //CHARG  
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Width = 120;
                dgvcCHARG.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcCHARG);

                //MENGE  
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Width = 120;
                dgvcMENGE.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMENGE);

                //USRNM   创建人
                DataGridViewTextBoxColumn dgvcUSRNM = new DataGridViewTextBoxColumn();
                dgvcUSRNM.DataPropertyName = "CRNAM";
                dgvcUSRNM.HeaderText = "USRNM";
                dgvcUSRNM.Width = 70;
                dgvcUSRNM.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcUSRNM);

                //CRDAT   创建时间
                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDATE";
                dgvcCRDAT.Width = 120;
                dgvcCRDAT.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcCRDAT);

                //MONAM  
                DataGridViewTextBoxColumn dgvcMONAM = new DataGridViewTextBoxColumn();
                dgvcMONAM.DataPropertyName = "MONAM";
                dgvcMONAM.HeaderText = "MONAM";
                dgvcMONAM.Width = 120;
                dgvcMONAM.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMONAM);

                //MODAT   修改时间
                DataGridViewTextBoxColumn dgvcMODAT = new DataGridViewTextBoxColumn();
                dgvcMODAT.DataPropertyName = "MODAT";
                dgvcMODAT.HeaderText = "MODAT";
                dgvcMODAT.Width = 120;
                dgvcMODAT.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMODAT);

                //BOXID  
                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "BOXID";
                dgvcBOXID.Width = 120;
                dgvcBOXID.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcBOXID);

                //FLAGE   
                DataGridViewTextBoxColumn dgvcFLAGE = new DataGridViewTextBoxColumn();
                dgvcFLAGE.DataPropertyName = "FLAGE";
                dgvcFLAGE.HeaderText = "FLAGE";
                dgvcFLAGE.Width = 120;
                dgvcFLAGE.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcFLAGE);

                // //添加原产地栏位
                DataGridViewTextBoxColumn dgvcREGION = new DataGridViewTextBoxColumn();
                dgvcREGION.DataPropertyName = "REGION";
                dgvcREGION.HeaderText = "REGION";
                dgvcREGION.Width = 120;
                dgvcREGION.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcREGION);

                dgvWaste.DataSource = allData;
                lblData.Text = allData.Rows.Count.ToString() + " records";
                dgvWaste.ClearSelection();
                dgvWaste.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOrderNoDataGrid()");
            }

        }
        #endregion

        #region txtMblnr_KeyDown
        private void txtMblnr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (Type == "")
                {
                    Sound.Play(@"Sound\ERROR.wav"); 
                    stsWarning.Text = "请选择合箱类型,谢谢!!";
                    return;
                }
                if (txtMblnr.Text.ToString() == "" && txtBoxId.Text.ToString() == "")
                {
                    Sound.Play(@"Sound\ERROR.wav"); 
                    stsWarning.Text = "单号和BoxId不能同时为空，请确认!!";
                    return;
                }
                strMblnr = txtMblnr.Text.ToString().Trim();

                if (Type == "SN" || Type == "BOXID")
                {
                    if (!strMblnr.Contains(":"))
                    {
                        Type = "BOXID";
                    }
                    else
                    {
                        Type = "SN";
                    }
                }
                else if (Type == "TIC")
                {
                    string[] sArray = txtMblnr.Text.ToString().ToUpper().Trim().Split(';');
                    //strMblnr = txtMblnr.Text.ToString().ToUpper().Trim();
                    strMblnr = sArray[0];
                }


                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                DataTable drData = new DataTable();
                //drData = objStorageOut.JudgmentWhdwn(Mandt, Comcd, Werks, Lgort, strMblnr);
                //if (drData.Rows.Count > 0)
                //{
                //    MessageBox.Show("此废品" + strMblnr + "已绑定BOXID:" + drData.Rows[0]["BOXID"].ToString() + ",请确认!!");
                //    txtMblnr.Text = "";
                //    return;
                //}
                DataTable dtData = new DataTable();
                dtData = objStorageOut.GetWhdwnData(Mandt, Comcd, Werks, Lgort, strMblnr, Type);
                
                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        foreach (DataRow item in allData.Rows)
                        {
                            if (item["MBLNR"].ToString().Trim() == dtData.Rows[i]["MBLNR"].ToString().Trim())
                            {
                                Sound.Play(@"Sound\ERROR.wav"); 
                                MessageBox.Show("该单已被刷入，请确认！");
                                strMblnr = "";
                                txtMblnr.Text = "";
                                txtBoxId.Text = "";
                                return ;
                            }
                            //DataTable drData = new DataTable(); 
                        }
                        drData = objStorageOut.JudgmentWhdwn(Mandt, Comcd, Werks, Lgort, dtData.Rows[i]["MBLNR"].ToString().Trim(), Type);
                        if (drData.Rows.Count > 0 && UntieType!="Untie")
                        {
                            Sound.Play(@"Sound\ERROR.wav"); 
                            MessageBox.Show("此废品" + dtData.Rows[i]["MBLNR"].ToString().Trim() + "已绑定BOXID:" + drData.Rows[0]["BOXID"].ToString() + ",请确认!!");
                            
                            txtMblnr.Text = "";
                            return;
                        }

                    }
                    if (allData.Columns.Count == 0)
                    {
                        allData = dtData.Clone();
                    }
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        DataRow dr = allData.NewRow();
                        dr["MANDT"] = dtData.Rows[i]["MANDT"];
                        dr["COMCD"] = dtData.Rows[i]["COMCD"];
                        dr["WERKS"] = dtData.Rows[i]["WERKS"];
                        dr["LGORT"] = dtData.Rows[i]["LGORT"];
                        dr["MBLNR"] = dtData.Rows[i]["MBLNR"];
                        dr["MATNR"] = dtData.Rows[i]["MATNR"];
                        dr["CHARG"] = dtData.Rows[i]["CHARG"];
                        dr["MENGE"] = dtData.Rows[i]["MENGE"];
                        dr["FLAGE"] = dtData.Rows[i]["FLAGE"];
                        dr["BOXID"] = dtData.Rows[i]["BOXID"];
                        dr["REGION"] = dtData.Rows[i]["REGION"];
                        allData.Rows.Add(dr);
                    }
                    var groups = allData.AsEnumerable()
                  .GroupBy(row => row.Field<string>("MATNR")) // 根据料号进行分组
                  .Where(group => group.Select(row => row.Field<string>("REGION")).Distinct().Count() > 1); // 判断分组中的原产地的唯一值数量是否大于 1
                    if (groups.Any())
                    {
                        DataRow RowToRemove = allData.Rows[allData.Rows.Count-1];
                        allData.Rows.Remove(RowToRemove);
                        Sound.Play(@"Sound\ERROR.wav");
                        MessageBox.Show("同一料号存在不同原产国，不允许合箱！");
                        return;
                    }
                    //dgvData = allData.Copy();
                    Sound.Play(@"Sound\BIU.wav");  //刷入的boxid與EC單的boxid連結成功
                    ShowWasteDataGrid();
                    txtMblnr.Text = "";
                    txtBoxId.Text = "";
                }
                else
                {
                    Sound.Play(@"Sound\ERROR.wav"); 
                    stsWarning.Text = "No Data";
                    return;
                }

            }
        }
        #endregion

        #region txtBoxId_KeyDown
        private void txtBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (txtBoxId.Text.ToString() == "")
                {
                    stsWarning.Text = "BOXID不能为空，请确认!!";
                    return;
                }
                string[] sArray = txtBoxId.Text.ToString().ToUpper().Trim().Split(';');
                BoxId = sArray[0];
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                //BoxId = txtBoxId.Text.ToString();
                allData = objStorageOut.GetWasteData(Mandt, Comcd, Werks, Lgort, BoxId);
                //dgvData = allData.Copy();
                ShowWasteDataGrid();
                PrintDetail.Enabled = true;
            }
        }
        #endregion

        #region Confrim
        private void btnConfrim_Click(object sender, EventArgs e)
        {
            if (allData.Rows.Count > 0)
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                if (UntieType == "Untie")
                {                   
                    string Flage = allData.Rows[0]["FLAGE"].ToString();

                    bool result = objStorageOut.DeleteWaste(allData, Type, Flage);
                    MessageBox.Show("解绑成功!!");
                    return;
                }
                else
                {
                    //判断数据里面是否有不同原产国的料号，不同原产国料号不允许合箱
                    var groups = allData.AsEnumerable()
                    .GroupBy(row => row.Field<string>("MATNR")) // 根据料号进行分组
                    .Where(group => group.Select(row => row.Field<string>("REGION")).Distinct().Count() > 1); // 判断分组中的原产地的唯一值数量是否大于 1
                    if (groups.Any())
                    {
                        DataRow RowToRemove = allData.Rows[allData.Rows.Count - 1];
                        allData.Rows.Remove(RowToRemove);
                        Sound.Play(@"Sound\ERROR.wav");
                        MessageBox.Show("同一料号存在不同原产国，不允许合箱！");
                        return;
                    }                 
                    //生成废品boxid
                    BoxId = objStorageOut.CreateBoxId(Werks);
                    bool flg = objStorageOut.InsertWaste(allData, BoxId, Type);
                    if (flg)
                    {
                        MessageBox.Show("保存成功");
                    }
                    else
                    {
                        MessageBox.Show("保存失败");
                        return;
                    }
                    allData = objStorageOut.GetWasteData(Mandt, Comcd, Werks, Lgort, BoxId);
                    //dgvData = allData.Copy();
                    ShowWasteDataGrid();
                    PrintDetail.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("没有数据，请确认!!");
                return;
            }
        }
        #endregion

        #region 打印
        private void btnPrintCode_Click(object sender, EventArgs e)
        {
            if (allData.Rows.Count > 0)
            {
                meData = allData.Clone();
                foreach (DataRow item in allData.Rows)
                {
                    if (meData.Rows.Count > 0)
                    {
                        for (int i = 0; i < meData.Rows.Count; i++)
                        {
                            if (item["MATNR"].ToString() == meData.Rows[i]["MATNR"].ToString() && item["CHARG"].ToString() == meData.Rows[i]["CHARG"].ToString())
                            {
                                meData.Rows[i]["MENGE"] = Convert.ToInt32(meData.Rows[i]["MENGE"].ToString()) + Convert.ToInt32(item["MENGE"].ToString());
                                break;
                            }
                            else if (i == meData.Rows.Count - 1)
                            {
                                DataRow dr = meData.NewRow();
                                dr["MANDT"] = item["MANDT"];
                                dr["COMCD"] = item["COMCD"];
                                dr["WERKS"] = item["WERKS"];
                                dr["LGORT"] = item["LGORT"];
                                dr["MATNR"] = item["MATNR"];
                                dr["CHARG"] = item["CHARG"];
                                dr["MENGE"] = item["MENGE"];
                                dr["CRNAM"] = item["CRNAM"];
                                dr["CRDAT"] = item["CRDAT"];
                                dr["MONAM"] = item["MONAM"];
                                dr["MODAT"] = item["MODAT"];
                                dr["BOXID"] = item["BOXID"];
                                dr["FLAGE"] = item["FLAGE"];
                                dr["REGION"] = item["REGION"];
                                meData.Rows.Add(dr);
                                break;
                            }

                        }
                    }
                    else
                    {
                        DataRow dr = meData.NewRow();
                        dr["MANDT"] = item["MANDT"];
                        dr["COMCD"] = item["COMCD"];
                        dr["WERKS"] = item["WERKS"];
                        dr["LGORT"] = item["LGORT"];
                        dr["MATNR"] = item["MATNR"];
                        dr["CHARG"] = item["CHARG"];
                        dr["MENGE"] = item["MENGE"];
                        dr["CRNAM"] = item["CRNAM"];
                        dr["CRDAT"] = item["CRDAT"];
                        dr["MONAM"] = item["MONAM"];
                        dr["MODAT"] = item["MODAT"];
                        dr["BOXID"] = item["BOXID"];
                        dr["FLAGE"] = item["FLAGE"];
                        dr["REGION"] = item["REGION"];
                        meData.Rows.Add(dr);
                    }

                }

                bool Print = btPrint_Click();
                if (Print)
                {
                    //stsWarning.Text = "二维码打印成功";
                    QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                    bool flg = objStorageOut.UpdateWasteStatus(Mandt, Comcd, Werks, Lgort, BoxId);
                    if (flg)
                    {
                        stsWarning.Text = "二维码打印成功";
                        Refresh();
                    }
                    else
                    {
                        stsWarning.Text = "FLAGE更新失败！！";
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("二维码打印失败，请确认！！");
                    return;
                }

            }
            else
            {
                MessageBox.Show("没有数据，请确认!!");
                return;
            }
        }

        private bool btPrint_Click()//object sender, EventArgs e
        {
            #region 打印机参数限制
            if (txtIP.Text.Trim() == "")
            {
                stsWarning.Text = "请输入打印IP！";
                return false;
            }
            else
            {
                strIP = txtIP.Text.Trim();
            }
            if (txtNum.Text.Trim() == "")
            {
                stsWarning.Text = "请输入打印数量！";
                return false;
            }
            else
            {
                num = Convert.ToInt32(txtNum.Text.ToString().Trim());
            }
            #endregion 打印机参数限制[END]

            #region 设置打印字段内容
            DataTable dt = new DataTable();
            dt.Columns.Add("NewBOXID");
            dt.Columns.Add("No");
            dt.Columns.Add("MATNR");
            dt.Columns.Add("REGION");
            dt.Columns.Add("MENGE");
            dt.Columns.Add("NewTOTAL");
            int NO = 1;
            int Total = 0;

            for (int i = 0; i < meData.Rows.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["NewBOXID"] = BoxId;
                newRow["No"] = NO;
                newRow["MATNR"] = meData.Rows[i]["MATNR"].ToString().Trim().ToUpper();
                newRow["REGION"] = meData.Rows[i]["REGION"].ToString().Trim().ToUpper();
                newRow["MENGE"] = meData.Rows[i]["MENGE"].ToString().Trim().ToUpper();
                newRow["NewTOTAL"] = Total+ Convert.ToInt32(meData.Rows[i]["MENGE"].ToString());
                dt.Rows.Add(newRow);
                NO++;
                Total = Convert.ToInt32(dt.Rows[i]["NewTOTAL"].ToString());
            }
            #endregion


            //StreamReader sr;
            //string AllContexttmp = "";

            ////打印机模板文件路径
            //string strFilePath = Application.StartupPath + "\\report\\Waste_Print_Model.txt";

            ////未找到打印机模板文件
            //if (!File.Exists(strFilePath))
            //{
            //    stsWarning.Text = "";
            //    stsWarning.Text = "未找到打印机参数文件!";
            //    return false;
            //}
            //else
            //{
            //    sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
            //    AllContexttmp = sr.ReadToEnd();
            //    sr.Close();
            //}
            //string[] line;

            //stsWarning.Text = "开始打印:";
            //string AllContext = AllContexttmp;
            string AllContext = "";
            string[] line;


            #region 打印机打印内容

            try
            {
                //// 构造ZPL字符串
                ////StringBuilder zplBuilder_all = new StringBuilder();
                ////StringBuilder zplBuilder = new StringBuilder();
                ////int yPos = 525; // 初始y坐标位置
                ////int count = Math.Min(dt.Rows.Count, 13); // 限制处理的最大行数为13
               //// string boxid = null; int totalqty = 0; //定义变量值
                // 构造ZPL字符串
                int rowLimit = 13; // 每个ZPL包含的最大行数限制
                int totalRows = dt.Rows.Count; // 数据总行数
                int zplCount = (totalRows + rowLimit - 1) / rowLimit; // 计算需要生成的ZPL数量
                StringBuilder zplBuilder_all = new StringBuilder();
                StringBuilder zplBuilder = new StringBuilder();
                string boxid = null;

                ////for (int i = 0; i < count; i++) //数据 行循环
                for (int zplIndex = 0; zplIndex < zplCount; zplIndex++)
                {
                    zplBuilder.Clear(); 
                    zplBuilder_all.Clear();
                    int totalqty = 0; //定义total总数为0
                    int yPos = 520; // 初始y坐标位置
                                    // 计算当前ZPL中要处理的行数
                    int startRow = zplIndex * rowLimit;
                    int endRow = Math.Min(startRow + rowLimit, totalRows);
                    for (int i = startRow; i < endRow; i++) //数据 行循环
                    {
                        boxid = BoxId;
                        //dt.Rows[0]["NewBOXID"].ToString().Trim().ToUpper() ?? ""; // 获取boxid字段的值 只取首行字段的值
                        string noValue = dt.Rows[i]["No"].ToString().Trim().ToUpper() ?? ""; // 获取No字段的值
                        string qciValue = dt.Rows[i]["MATNR"].ToString().Trim().ToUpper() ?? ""; // 获取qcipn字段的值
                        string qtyValue = dt.Rows[i]["MENGE"].ToString().Trim().ToUpper() ?? ""; // 获取qty字段的值
                        string originValue = dt.Rows[i]["REGION"].ToString().Trim().ToUpper() ?? ""; // 获取region字段的值

                        // 添加对应变量值的ZPL指令，并更新y坐标位置
                        zplBuilder.AppendLine($"^FT120,{yPos}^A0N,50,45^FD{noValue}^FS");
                        zplBuilder.AppendLine($"^FT250,{yPos}^A0N,45,45^FD{qciValue}^FS"); //满足17码广达料号
                        zplBuilder.AppendLine($"^FT760,{yPos}^A0N,45,40^FD{qtyValue}^FS");
                        zplBuilder.AppendLine($"^FT965,{yPos}^A0N,45,45^FD{originValue}^FS");

                        yPos += 90; // 更新y坐标位置 每行间隔100

                        //汇总qty的字段 取值总数
                        int noValueInt;
                        if (int.TryParse(qtyValue, out noValueInt))
                        {
                            totalqty += noValueInt;
                        }
                    }
                    int page = zplIndex + 1;
                    zplBuilder_all.AppendLine($"^XA  ^MCY  ^XZ  ^XA  ^FWN^CFD,24^PW1196^LH0,0^XZ^XA^CI0,100,100^XZ^XA^PR3^FS^MNY^FS^FO35,50^GB1125,300,6,B^FS	^FO280,50^GB600,300,6,B^FS	^FT55,220^A0N,65,60^FDBOX ID^FS");
                    zplBuilder_all.AppendLine($"^FT120,425^A0N,65,60^FDNo.^FS^FT420,425^A0N,65,60^FDQCIPN^FS^FT810,420^A0N,65,60^FDQty^FS^FT980,425^A0N,65,60^FDOrigin^FS^FT90,1720^A0N,65,60^FDTotal:^FS");
                    zplBuilder_all.AppendLine($"^FT280,220^A0N,65,60^FD{boxid}^FS ^FO887,75BXN,13,200,20,20,,_^FD{boxid}^FS");
                    zplBuilder_all.AppendLine($"^FT755,1720^A0N,50,45^FD{totalqty}^FS ");
                    zplBuilder_all.AppendLine($"^FT300,1720^A0N,50,45^FDPage:{page}/{zplCount}^FS");
                    zplBuilder_all.AppendLine(zplBuilder.ToString());
                    zplBuilder_all.AppendLine($"^FO35,346^GB1125,1400,6,B^FS^FO749,346^GB210,1400,6,B^FS	^FO230,346^GB520,1400,6,B^FS ^FO35,450^GB1125,0,6,B^FS^FO35,540^GB1125,0,6,B^FS^FO35,630^GB1125,0,6,B^FS ^FO35,720^GB1125,0,6,B^FS	^FO35,810^GB1125,0,6,B^FS	^FO35,900^GB1125,0,6,B^FS ^FO35,990^GB1125,0,6,B^FS	^FO35,1080^GB1125,0,6,B^FS	");
                    zplBuilder_all.AppendLine($"^ISSTRNWARE,N^FS^XZ^XA^PR3^FS^ILSTRNWARE^FS^PF0^FS^PQQTY,0,1,Y^XZ^XA^IDSTRNWARE^XZ");
             
                    AllContext = zplBuilder_all.ToString();


                    //利用正则表达式来分解
                    line = System.Text.RegularExpressions.Regex.Split(AllContext, "\r\n");

                    int j = 1;
                    foreach (string ss in line)
                    {
                        if (ss.IndexOf("<NA>") > -1)
                        {
                            continue;
                        }
                        if (j == 1)
                        {
                            AllContext = ss;
                            j = j + 1;
                        }
                        else
                        {
                            AllContext = AllContext + "\r\n" + ss;
                            j = j + 1;
                        }
                    }
                    //SP.WriteLine(AllContext);
                    for (int i = 1; i <= num; i++)
                    {
                        PrintLabelIP(strIP, AllContext);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "-<PrintData()>");
                return false;
            }
            //SP.DiscardOutBuffer();
            //SP.Close();

            stsWarning.Text = "已发送打印机";
            return true;
            #endregion
        }
        #endregion

        #region 打印主要方法
        private bool PrintCode(DataTable dt,string AllContexttmp) 
        {
            string[] line;

            stsWarning.Text = "开始打印:";
            string AllContext = AllContexttmp;

            #region 打印机打印内容

            try
            {
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    //Zebra 打印机特殊字符^  ，字符和二维码要转化为'_5E' ,条形码要转化为><
                    if (dt.Columns[x].ToString() == "BoxId" || dt.Columns[x].ToString() == "TTL")
                    {
                        //AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                        AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[0][x].ToString().Replace("^", "_5E"));
                    }
                    else
                    {
                        AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[0][x].ToString());
                    }
                }
                //利用正则表达式来分解
                line = System.Text.RegularExpressions.Regex.Split(AllContext, "\r\n");

                int j = 1;
                foreach (string ss in line)
                {
                    if (ss.IndexOf("<NA>") > -1)
                    {
                        continue;
                    }
                    if (j == 1)
                    {
                        AllContext = ss;
                        j = j + 1;
                    }
                    else
                    {
                        AllContext = AllContext + "\r\n" + ss;
                        j = j + 1;
                    }
                }
                //SP.WriteLine(AllContext);
                PrintLabelIP(strIP, AllContext);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "-<PrintData()>");
                return false;
            }
            stsWarning.Text = "已发送打印机";
            return true;
            #endregion
        }
        #endregion

        #region IP打印
        private void PrintLabelIP(string strIP, string strLabel)
        {
            string strPort = "9100";
            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(strIP), Convert.ToInt32(strPort));
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(hostEndPoint);
            if (!s.Connected)
            {
                MessageBox.Show("Not Connected!");
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(strLabel);
                s.Send(data, data.Length, 0);
                if (s.Connected)
                    s.Close();
            }
        }
        #endregion

        #region 窗体载入时读取打印机设置文件
        private void BindPrintSetting()
        {
            //打印机设置文件默认路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            if (File.Exists(strFileSettingPath))
            {
                //读取文件
                using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                {
                    string[] arrayPrintSetting = sr.ReadToEnd().Split(',');

                    //设置默认打印设置参数
                    txtIP.Text = arrayPrintSetting[2].ToString();
                    txtIP_PL.Text = arrayPrintSetting[2].ToString();
                }
            }
        }
        #endregion

        #region 打印机设置
        private void btSetting_Click(object sender, EventArgs e)
        {
            //打印机设置文件路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            //打印机设置文件不存在
            if (!File.Exists(strFileSettingPath))
            {
                //保存当前打印设置信息到E盘根目录下
                if (MessageBox.Show("是否保存当前打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(strFileSettingPath))
                        {
                            sw.WriteLine(",,"+txtIP.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "File.CreateText(strFileSettingPath)");
                    }
                }
            }
            //打印机设置文件存在
            else
            {
                if (MessageBox.Show("是否覆盖现有打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string[] arrayPrintSetting;
                        using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                        {
                            arrayPrintSetting = sr.ReadToEnd().Split(',');
                        }
                        //覆盖现有的打印机设置文件
                        using (StreamWriter sw = new StreamWriter(strFileSettingPath, false))
                        {
                            sw.WriteLine(arrayPrintSetting[0] + "," + arrayPrintSetting[1] + "," + txtIP.Text.Trim());
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "-<StreamWriter(strFileSettingPath, false)>");
                    }
                }
            }
        }
        #endregion

        #region 打印数量文本框
        private void txtNum_TextChanged(object sender, EventArgs e)
        {

            if (txtNum.Text.ToString().Trim() != "")
            {
                num = Convert.ToInt32(txtNum.Text.ToString().Trim());
            }
        }
        #endregion

        #region refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            //txtBoxId.Enabled = true;
            //txtSCode.Enabled = true;
            //txtECode.Enabled = true;
            Refresh();
            txtNum.Text = "1";
            txtBoxId.Enabled = true;
            gbFunction.Enabled = true;
            
        }
        private void Refresh()
        {
            allData.Clear();
            meData.Clear();
            txtMblnr.Text = "";
            txtBoxId.Text = "";
            stsWarning.Text = "";
            txtNum.Text = "";
            txtSCode.Text = "";
            txtECode.Text = "";
            rdoSN.Checked = false;
            rdoMC.Checked = false;
            checkUntie.Checked = false;
            UntieType = "";
            PrintDetail.Enabled = false;
            
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region print
        private void btnPrint_Click(object sender, EventArgs e)
        {
                        //if (txtfalg.Text.ToString().Trim() == "")
            //    PrintCode();
            #region 打印机参数限制
            if (txtIP.Text.Trim() == "")
            {
                stsWarning.Text = "请输入打印IP！";
                return;
            }
            else
            {
                strIP = txtIP.Text.Trim();
            }
            #endregion 打印机参数限制[END]

            #region 设置箱号标识
            string boxfalg = "";
            try
            {
                boxfalg = txtfalg.Text.ToString().Trim().ToUpper().Substring(0, 1);
                string pattern = @"^[A-Z]$";


                foreach (Match match in Regex.Matches(boxfalg, pattern))
                    boxfalg = match.Value.ToString();
                if (boxfalg == "")
                {
                    stsWarning.Text = "请输入正确的箱号标识！";
                    return;
                }
            }
            catch
            {
                stsWarning.Text = "请输入正确的箱号标识！";
                return;
            }
            #endregion

            #region 设置打印字段内容
            DataTable dt = new DataTable();
            dt.Columns.Add("Number");
            dt.Columns.Add("BarCode");
            if (num > 0 && txtSCode.Text.ToString().Trim() == "" && txtECode.Text.ToString().Trim() == "")
            {
                for (int i = 1; i < num + 1; i++)
                {
                    string number = i.ToString("000");

                    DataRow dr = dt.NewRow();
                    dr["Number"] = boxfalg + number;
                    dr["BarCode"] = boxfalg + number;
                    dt.Rows.Add(dr);
                }

            }
            else if (txtSCode.Text.ToString().Trim() != "" && txtECode.Text.ToString().Trim() != "")
            {
                string SCode = txtSCode.Text.ToString();
                string ECode = txtECode.Text.ToString();
                string Scon = "";
                string Econ = "";

                for (int i = 0; i < SCode.Length; i++)
                {
                    if (Char.IsNumber(SCode, i))
                    {
                        Scon += SCode[i]; //输入的是数字   
                    }
                }
                for (int i = 0; i < ECode.Length; i++)
                {
                    if (Char.IsNumber(ECode, i))
                    {
                        Econ += ECode[i]; //输入的是数字   
                    }
                }
                if (SCode == Scon && ECode == Econ)
                {
                    if (Convert.ToInt32(txtSCode.Text.ToString()) > Convert.ToInt32(txtECode.Text.ToString()))
                    {
                        stsWarning.Text = "起码不能大于止码";
                        return;
                    }
                    else
                    {
                        for (int i = Convert.ToInt32(txtSCode.Text.ToString()); i < Convert.ToInt32(txtECode.Text.ToString()) + 1; i++)
                        {
                            string number = i.ToString("000");

                            DataRow dr = dt.NewRow();
                            dr["Number"] = boxfalg + number;
                            dr["BarCode"] = boxfalg + number;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("起止码为数字，请确认!!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("起止码需要同时都有，请确认!!");
                return;
            }
            #endregion

            StreamReader sr;
            string AllContexttmp = "";

            //打印机模板文件路径
            string strFilePath = Application.StartupPath + "\\report\\BarCode.txt";

            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                stsWarning.Text = "";
                stsWarning.Text = "未找到打印机参数文件!";
                return;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }

            string[] line;

            stsWarning.Text = "开始打印:";

            string strPort = "9100";
            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(strIP), Convert.ToInt32(strPort));
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(hostEndPoint);
            
            #region 打印机打印内容

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string AllContext = AllContexttmp;
                try
                {
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        //Zebra 打印机特殊字符^  ，字符和二维码要转化为'_5E' ,条形码要转化为><
                        if (dt.Columns[x].ToString() == "Number" || dt.Columns[x].ToString() == "BarCode")
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                            //AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[0][x].ToString().Replace("^", "_5E"));
                        }
                        else
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString());
                        }
                    }

                    //利用正则表达式来分解
                    line = System.Text.RegularExpressions.Regex.Split(AllContext, "\r\n");

                    int j = 1;
                    foreach (string ss in line)
                    {
                        if (ss.IndexOf("<NA>") > -1)
                        {
                            continue;
                        }
                        if (j == 1)
                        {
                            AllContext = ss;
                            j = j + 1;
                        }
                        else
                        {
                            AllContext = AllContext + "\r\n" + ss;
                            j = j + 1;
                        }
                    }


                    if (!s.Connected)
                    {
                        MessageBox.Show("Not Connected!");
                    }
                    else
                    {
                        byte[] data = Encoding.UTF8.GetBytes(AllContext);
                        s.Send(data, data.Length, 0);              
                    }
                    //PrintLabelIP(strIP, AllContext);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "-<PrintData()>");
                    return;
                }
            }
            //SP.DiscardOutBuffer();
            //SP.Close();
            s.Close();
            stsWarning.Text = "条形码打印成功";

            #endregion 打印[END]
        }
        #endregion

        #region SOP
        private void LnkSOP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + @"SOP\QWMS_废品合箱打印SOP.docx";
                try
                {
                    Process.Start("winword.exe", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }
        #endregion

        #region 打印明细
        private void PrintDetail_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < allData.Rows.Count; i++)
            {
                allData.Rows[i]["ITEM"] = Convert.ToString(i + 1);
            }
            string ReportPrintType = "WASTEDETAIL";
            //ReportPrintType = ReportPrintType + strChecked;
            ReportPrint objReportPrint = new ReportPrint(UserData, ReportPrintType, allData);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        //
        //
        //
        //
        //AGV Print Label
        //AGV Print Label-----顯示方法

        #region ShowDbLgort_PL (查询Print Label仓别)
        private void ShowDdlLgort_PL()
        {
            Authority objAuthority = new Authority(UserData);
            try
            {
                stsWarning.Text = "";
                
                DataTable dtTemp = new DataTable();
                if (cmbWerks_PL.SelectedIndex != -1)
                {
                    strWerks_PL = cmbWerks_PL.Items[cmbWerks_PL.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks_PL);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort_PL.SelectedIndex != -1)
                {
                    strLgort_PL = cmbLgort_PL.Items[cmbLgort_PL.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_PL.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_PL.Items.Clear();
                    strLgort_PL = "";
                }
                else
                {
                    cmbLgort_PL.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_PL.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort_PL && strLgort_PL != "")
                        {
                            cmbLgort_PL.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort_PL()");
            }
        }
        private void cmbLgort_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            strWerks_PL = cmbWerks_PL.Text.ToString();
            strLgort_PL = cmbLgort_PL.Text.ToString();
        }
        #endregion

        #region cmbWerks_PL_SelectedIndexChanged   (AGV Print Label查询分页-厂区)
        private void ShowDdlWerks_PL()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks_PL.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_PL.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                cmbWerks_PL.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void cmbWerks_PL_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort_PL();
        }
        #endregion

        #region ShowDdlPLHour   (AGV Kitting查询分页-显示时间下拉选项)
        private void ShowDdlPLHour()
        {
            
            for (int i = 0; i <= 23; i++)
            {
                cmbHour_PL.Items.Add(i.ToString());
            }
        }
        #endregion

        #region ShowDdlPLEndHour   (AGV Kitting查询分页-显示截止时间下拉选项)
        private void ShowDdlPLEndHour()
        {
            
            for (int i = 0; i <= 23; i++)
            {
                cmbendHour_PL.Items.Add(i.ToString());
            }
        }
        #endregion

        #region dtpDate_PL_ValueChanged   (AGV Print查询分页-日期)
        private void dtpDate_PL_ValueChanged(object sender, EventArgs e)
        {
            dtpDate_PL.CustomFormat = "yyyy/MM/dd";
        }
        #endregion
        
        #region dgv显示数据信息
        private void ShowWasteDataGrid_AGV() 
        {
            dgvWaste.AutoGenerateColumns = false;
            dgvWaste.Columns.Clear();

            try
            {
                //WERKS
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "WERKS";
                dgvcWERKS.Width = 150;
                dgvcWERKS.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcWERKS);

                //ORDERNO 车辆信息
                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "LGORT";
                dgvcLGORT.Width = 150;
                dgvcLGORT.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcLGORT);

                //MATNR
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Width = 150;
                dgvcMATNR.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMATNR);

                //MENGE  
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Width = 150;
                dgvcMENGE.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcMENGE);

                //USRNM   创建人
                DataGridViewTextBoxColumn dgvcUSRNM = new DataGridViewTextBoxColumn();
                dgvcUSRNM.DataPropertyName = "CRNAM";
                dgvcUSRNM.HeaderText = "USRNM";
                dgvcUSRNM.Width = 150;
                dgvcUSRNM.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcUSRNM);

                //CRDAT   创建时间
                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDATE";
                dgvcCRDAT.Width = 150;
                dgvcCRDAT.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcCRDAT);

                //BOXID  
                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "BOXID";
                dgvcBOXID.Width = 150;
                dgvcBOXID.ReadOnly = true;
                dgvWaste.Columns.Add(dgvcBOXID);

                dgvWaste.DataSource = allData;
                lblData.Text = allData.Rows.Count.ToString() + " records";
                dgvWaste.ClearSelection();
                dgvWaste.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOrderNoDataGrid_PL()");
            }
        }
        #endregion

        #region btnSelect_PL (PL btnSelect_PL查询功能)
        private void btnSelect_PL_Click(object sender, EventArgs e)
        {
            string strDate_PL = "";
            string strendDate_PL = "";
            string strHour_PL = "";
            string strendHour_PL = "";
            string strMatnr_PL = "";
            string strMenge_PL = "";
            strMenge_PL = txtCn_PL.Text.ToString().Trim();
            strMatnr_PL = txtQpn_PL.Text.ToString().Trim().ToUpper();
            stsWarning.Text = "";

            try
            {
                #region 判断查询条件 
                if (cmbWerks_PL.SelectedIndex != -1) 
                {
                    strWerks_PL = cmbWerks_PL.Items[cmbWerks_PL.SelectedIndex].ToString();
                }
                if (cmbLgort_PL.SelectedIndex != -1) 
                {
                    strLgort_PL = cmbLgort_PL.Items[cmbLgort_PL.SelectedIndex].ToString();
                }
                if (strWerks_PL == "" || strLgort_PL == "")
                {
                    stsWarning.Text="Plant and storage can't be empty!!";
                    return;
                }
                if (strMatnr_PL == "")
                {
                    stsWarning.Text = "料号不可为空！！";
                    return;
                }
                if (cmbHour_PL.SelectedIndex != -1)
                {
                    strHour_PL = cmbHour_PL.Items[cmbHour_PL.SelectedIndex].ToString().PadLeft(2, '0');
                }
                else
                {
                    stsWarning.Text = "Please select hour!! 时间";
                    return;
                }
                if (cmbendHour_PL.SelectedIndex != -1)
                {
                    strendHour_PL = cmbendHour_PL.Items[cmbendHour_PL.SelectedIndex].ToString().PadLeft(2, '0');
                }
                else
                {
                    stsWarning.Text = "Please select hour!! 截止时间";
                    return;
                }
                #endregion

                strDate_PL = dtpDate_PL.Value.ToString("yyyy-MM-dd") + " " + strHour_PL + ":00:00.000";
                strendDate_PL = dtpendDate_PL.Value.ToString("yyyy-MM-dd") + " " + strendHour_PL + ":00:00.000";
                
                DateTime dt1 = Convert.ToDateTime(strDate_PL);
                DateTime dt2 = Convert.ToDateTime(strendDate_PL);
                if (strDate_PL == strendDate_PL)
                {
                    stsWarning.Text="起止时间不可相同！！！！";
                    return;
                }
                else if (dt1>dt2)
                {
                    stsWarning.Text = "开始时间小于结束时间！！！！";
                    return;
                }
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                allData = objStorageOut.GetWasteDataSelect_PL(Mandt, Comcd, strWerks_PL, strLgort_PL, strMatnr_PL, strDate_PL, strendDate_PL);
                if (allData.Rows.Count > 0)
                {
                    ShowWasteDataGrid_AGV();
                }
                else 
                {
                    stsWarning.Text = "NO DATA!!!";
                    dgvWaste.Columns.Clear();
                    allData.Clear();
                    ShowWasteDataGrid_AGV();
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

        #region btnSave_PL （btnSave_PL保存功能）
        private void btnSave_PL_Click(object sender, EventArgs e)
        {
            string strMatnr_PL = "";
            string strMenge_PL = "";
            string strBoxQty_PL = "";
            strBoxQty_PL = txtBn_PL.Text.ToString().Trim();
            strMenge_PL = txtCn_PL.Text.ToString().Trim();
            strMatnr_PL = txtQpn_PL.Text.ToString().Trim().ToUpper();
            stsWarning.Text = "";
            try
            {
                #region 判断保存条件
                if (cmbWerks_PL.SelectedIndex != -1)
                {
                    strWerks_PL = cmbWerks_PL.Items[cmbWerks_PL.SelectedIndex].ToString();
                }
                if (cmbLgort_PL.SelectedIndex != -1)
                {
                    strLgort_PL = cmbLgort_PL.Items[cmbLgort_PL.SelectedIndex].ToString();
                }
                if (strWerks_PL == "" || strLgort_PL == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (strMatnr_PL == "")
                {
                    stsWarning.Text = "料号不可为空！！";
                    return;
                }
                if (strBoxQty_PL == "" || strMenge_PL == "") 
                {
                    stsWarning.Text = "基数或箱数不可以为空!!";
                    return;
                }
                #endregion
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                //生成boxid流水号 可能一次生成多个流水号
                //CreatWasteBoxid_AGV()
                //生成好流水号之后 将数据插入到 WASTE表
                //InsertWasteData_AGV()
                //返回数据 进行if判断 保存成功 or 保存失败
                //allData = objStorageOut.GetWasteDataSelect(Mandt, Comcd, strWerks_AGV, strLgort_AGV, strMatnr_AGV, strDate_AGV, strMenge_AGV);
                //if (allData.Rows.Count > 0) 
                //{
                //    stsWarning.Text = "已存在该数据，不可重复插入";
                //    return;
                //}
                string BoxId_PL = objStorageOut.CreateWasteBoxid_PL(strWerks_PL, strBoxQty_PL);
                bool flg = objStorageOut.InsertWasteData_PL(Mandt, Comcd, strWerks_PL, strLgort_PL, strMatnr_PL, strMenge_PL, BoxId_PL, strBoxQty_PL);
                if (flg)
                {
                    MessageBox.Show("保存成功！！！！");
                }
                else 
                {
                    MessageBox.Show("保存失败！！！！");
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

        #region btnDelete_PL (btnDelete_PL删除功能)
        private void btnDelete_PL_Click(object sender, EventArgs e)
        {
            string strDate_PL = "";
            string strHour_PL = "";
            string strendHour_PL = "";
            string strendDate_PL = "";
            string strMatnr_PL = "";
            string strMenge_PL = "";
            string strBoxQty_PL = "";
            strMenge_PL = txtCn_PL.Text.ToString().Trim();
            strBoxQty_PL = txtBn_PL.Text.ToString().Trim();
            strMatnr_PL = txtQpn_PL.Text.ToString().Trim().ToUpper();
            stsWarning.Text = "";
            try
            {
                if (cmbWerks_PL.SelectedIndex != -1)
                {
                    strWerks_PL = cmbWerks_PL.Items[cmbWerks_PL.SelectedIndex].ToString();
                }
                if (cmbLgort_PL.SelectedIndex != -1)
                {
                    strLgort_PL = cmbLgort_PL.Items[cmbLgort_PL.SelectedIndex].ToString();

                }
                if (strWerks_PL == "" || strLgort_PL == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (strMatnr_PL == "")
                {
                    stsWarning.Text = "料号不可为空！！";
                    return;
                }
                if (strMenge_PL == "")
                {
                    stsWarning.Text = "基数不可以为空!!";
                    return;
                }
                if (cmbHour_PL.SelectedIndex != -1)
                {
                    strHour_PL = cmbHour_PL.Items[cmbHour_PL.SelectedIndex].ToString().PadLeft(2, '0');
                }
                else
                {
                    stsWarning.Text = "Please select hour!! 时间";
                    return;
                }
                if (cmbendHour_PL.SelectedIndex != -1)
                {
                    strendHour_PL = cmbendHour_PL.Items[cmbendHour_PL.SelectedIndex].ToString().PadLeft(2, '0');
                }
                else
                {
                    stsWarning.Text = "Please select hour!! 截止时间";
                    return;
                }
                strDate_PL = dtpDate_PL.Value.ToString("yyyy-MM-dd") + " " + strHour_PL + ":00:00.000";
                strendDate_PL = dtpendDate_PL.Value.ToString("yyyy-MM-dd") + " " + strendHour_PL + ":00:00.000";
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                if (dgvWaste.RowCount > 0)
                {
                    allData = objStorageOut.GetWasteDataSelect_PL(Mandt, Comcd, strWerks_PL, strLgort_PL, strMatnr_PL, strDate_PL, strendDate_PL);
                    if (allData.Rows.Count > 0)
                    {
                        bool flg = objStorageOut.DeleteWasteData_PL(Mandt, Comcd, strWerks_PL, strLgort_PL, strMatnr_PL, strDate_PL, strendDate_PL, strMenge_PL);
                        if (flg)
                        {
                            MessageBox.Show("删除成功");
                        }
                        else
                        {
                            MessageBox.Show("删除失败！！！");
                            return;
                        }
                        allData = objStorageOut.GetWasteDataSelect_PL(Mandt, Comcd, strWerks_PL, strLgort_PL, strMatnr_PL, strDate_PL, strendDate_PL);
                        ShowWasteDataGrid_AGV();
                    }
                    else
                    {
                        stsWarning.Text = "不存在该数据！！！";
                        return;
                    }
                }
                else 
                {
                    stsWarning.Text = "请先点击查询按钮，显示数据";
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

        #region btnPrint_PL (btnPrint_PL打印功能)
        private void btnPrint_PL_Click(object sender, EventArgs e)
        {
            if (allData.Rows.Count > 0)
            {
                if (txtIP_PL.Text.Trim() == "")
                {
                    stsWarning.Text = "请输入打印IP！";
                    return;
                }
                else
                {
                    strIP = txtIP_PL.Text.Trim();
                }
                for (int i = 0; i < allData.Rows.Count; i++)
                {
                    str[0] = allData.Rows[i]["BOXID"].ToString();
                    str[1] = allData.Rows[i]["MATNR"].ToString();
                    str[2] = allData.Rows[i]["MENGE"].ToString();
                    str[3] = allData.Rows[i]["LGORT"].ToString();
                    str[4] = allData.Rows[i]["CRNAM"].ToString();
                    str[5] = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                    bool print = btPrint_PL_Click(strIP);
                    if (print)
                    {
                        stsWarning.Text = "标签打印成功";
                            
                    }
                    else
                    {
                        MessageBox.Show("标签打印失败，请确认！！");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("没有显示打印数据，请先点击查询!!");
                return;
            }
        }
        #endregion

        #region Print_PL_Click
        private bool btPrint_PL_Click(string strIP)//object sender, EventArgs e
        {
            #region 打印机参数限制
            stsWarning.Text = "";
            
            
            #endregion 打印机参数限制[END]

            #region 设置打印字段内容
            DataTable dt = new DataTable();
            dt.Columns.Add("BOXID");
            dt.Columns.Add("MATNR");
            dt.Columns.Add("LGORT");
            dt.Columns.Add("MENGE");
            dt.Columns.Add("CRDAT");
            dt.Columns.Add("CRNAM");
            dt.Columns.Add("TTL");
            StringBuilder print = new StringBuilder("");
            DataRow dr = dt.NewRow();
            dr["BOXID"] = str[0];
            dr["MATNR"] = str[1];
            dr["MENGE"] = str[2];
            dr["LGORT"] = str[3];
            dr["CRNAM"] = str[4];
            dr["CRDAT"] = str[5];
            print.Append(str[0] + ";" + str[1] + ";" + str[2] + ";" + str[3] + ";" + str[4]);
            dr["TTL"] = print;//二维码内容


            dt.Rows.Add(dr);
            #endregion

            StreamReader sr;
            string AllContexttmp = "";

            //打印机模板文件路径
            string strFilePath = Application.StartupPath + "\\report\\Print_Label.txt";

            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                stsWarning.Text = "";
                stsWarning.Text = "未找到打印机参数文件!";
                return false;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }
            bool P_Print = PrintCode(dt, AllContexttmp);

            if (P_Print)
            {
                return true;
            }
            else 
            {
                return false;
            }
            
        }
        #endregion

        #region refresh_PL
        private void btnRefresh_PL_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            //清空dgv
            dgvWaste.Columns.Clear();
            allData.Clear();
            this.lblData.Text = "0 records";
            gbFunction_PL.Enabled = true;
            //清空文本框
            txtBn_PL.Text = "";
            txtQpn_PL.Text = "";
            txtCn_PL.Text = "";
            txtIP_PL.Text = "";
            btnSave_PL.Enabled = true;
            btnSelect_PL.Enabled = true;
            btnPrint_PL.Enabled = true;
            btnDelete_PL.Enabled = true;
            txtIP_PL.Enabled = true;
            txtCn_PL.Enabled = true;
            txtBn_PL.Enabled = true;
            radQuery.Checked = false;
            radDelete.Checked = false;
            radAdd.Checked = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
        }
        #endregion

        #region PAL报废 Add Ash Chen
        private void radDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (radDelete.Checked == true) 
            {
                stsWarning.Text = "";
                gbFunction_PL.Enabled = false;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                btnSave_PL.Enabled = false;
                
                txtBn_PL.Enabled = false;
                txtIP_PL.Enabled = false;
                btnPrint_PL.Enabled = false;
            }
            
        }

        private void radQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (radQuery.Checked == true) 
            {
                stsWarning.Text = "";
                gbFunction_PL.Enabled = false;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                btnSave_PL.Enabled = false;
                btnDelete_PL.Enabled = false;
                txtBn_PL.Enabled = false;
                txtCn_PL.Enabled = false;
            }
            
        }

        private void radAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (radAdd.Checked == true) 
            {
                stsWarning.Text = "";
                gbFunction_PL.Enabled = false;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                btnDelete_PL.Enabled = false;
            }
        }
        #endregion
    }

}





    



