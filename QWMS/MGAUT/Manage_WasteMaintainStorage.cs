using QCI.QWMS;
using QWMS.Common;
using System;
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
    public partial class Manage_WasteMaintainStorage : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strToLgort = "";
        private string strBoxID = "";
        private string strType = "";

        private DataTable allData = new DataTable();//存放WHTIC数据

        #region get/set
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
        public string ToLgort
        {
            get
            {
                return strToLgort;
            }
            set
            {
                strToLgort = value;
            }
        }
        public string BoxID
        {
            get
            {
                return strBoxID;
            }
            set
            {
                strBoxID = value;
            }
        }
        #endregion

        public Manage_WasteMaintainStorage(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            chkMblnr.Visible = false;

            try
            {
                QCI.QWMS.Replenishment objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                //檢查權限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化
                    ShowStatusData();
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
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

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
                        //因仓库需求（user：王正），CS50 RWPA\RWPB\RWPC\RWPD 为报废仓,F7增加CS31  RW仓为报废仓
                        if (dtTemp.Rows[i]["F_TEXT"].ToString().Substring(0, 2) == "SC" || (dtTemp.Rows[i]["CTRLNM"].ToString() == "CS50" && (dtTemp.Rows[i]["F_TEXT"].ToString() == "RWPA" || dtTemp.Rows[i]["F_TEXT"].ToString() == "RWPB" || dtTemp.Rows[i]["F_TEXT"].ToString() == "RWPC" || dtTemp.Rows[i]["F_TEXT"].ToString() == "RWPD"))||(dtTemp.Rows[i]["CTRLNM"].ToString() == "CS31"&& dtTemp.Rows[i]["F_TEXT"].ToString().Substring(0, 2) == "RW"))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        }
                        //if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        //{
                        //    cmbLgort.SelectedIndex = i;
                        //}
                    }
                }
                if (Werks == "CS31")
                {
                    chkMblnr.Visible = true;
                }
                else
                {
                    chkMblnr.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            strWerks = cmbWerks.Text.ToString();
            strLgort = cmbLgort.Text.ToString();
        }

        #region ShowWhticDataGrid
        private void ShowWhticDataGrid()
        {
            dgvSource.AutoGenerateColumns = false;
            dgvSource.Columns.Clear();

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
                dgvSource.Columns.Add(dgvcWERKS);

                //仓别
                DataGridViewTextBoxColumn dgvcNowLGORT = new DataGridViewTextBoxColumn();
                dgvcNowLGORT.DataPropertyName = "NowLGORT";
                dgvcNowLGORT.HeaderText = "NowLGORT";
                dgvcNowLGORT.Width = 120;
                dgvcNowLGORT.ReadOnly = true;
                dgvSource.Columns.Add(dgvcNowLGORT);

                //目的仓别
                DataGridViewTextBoxColumn dgvcToLGORT = new DataGridViewTextBoxColumn();
                dgvcToLGORT.DataPropertyName = "TOLGORT";
                dgvcToLGORT.HeaderText = "TOLGORT";
                dgvcToLGORT.Width = 120;
                dgvcToLGORT.ReadOnly = true;
                dgvSource.Columns.Add(dgvcToLGORT);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 120;
                dgvcMBLNR.ReadOnly = true;
                dgvSource.Columns.Add(dgvcMBLNR);

                //MATNR
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Width = 120;
                dgvcMATNR.ReadOnly = true;
                dgvSource.Columns.Add(dgvcMATNR);

                //BOXID  
                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "BOXID";
                dgvcBOXID.Width = 120;
                dgvcBOXID.ReadOnly = true;
                dgvSource.Columns.Add(dgvcBOXID);

                //BOXID  
                DataGridViewTextBoxColumn dgvcOldLGORT = new DataGridViewTextBoxColumn();
                dgvcOldLGORT.DataPropertyName = "OldLGORT";
                dgvcOldLGORT.HeaderText = "OldLGORT";
                dgvcOldLGORT.Width = 120;
                dgvcOldLGORT.ReadOnly = true;
                dgvSource.Columns.Add(dgvcOldLGORT);



                dgvSource.DataSource = allData;
                lblSource.Text = allData.Rows.Count.ToString() + " records";
                dgvSource.ClearSelection();
                dgvSource.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOrderNoDataGrid()");
            }

        }
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
               
        }

        private void txtBoxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Confirm();
            }
        }

        private void Confirm()
        {
            if (txtBoxID.Text.ToString() == "")
            {
                stsWarning.Text = "BOXID不能为空，请确认!!";
                return;
            }
            if (cmbLgort.Text.ToString() == "")
            {
                stsWarning.Text = "仓别不能为空，请确认!!";
                return;
            }
            if (txtToLgort.Text.ToString() == "")
            {
                stsWarning.Text = "目的仓别不能为空，请确认!!";
                return;
            }
            if (cmbLgort.Text.ToString().Substring(0, 2) == "SC" && txtToLgort.Text.ToString().Substring(0, 2)!="SC")
            {
                stsWarning.Text = "调出仓别为SC仓，目的仓也必须为SC仓";
                return;
            }
            string[] sArray = txtBoxID.Text.ToString().ToUpper().Trim().Split(';');
            strBoxID = sArray[0];
            strToLgort = txtToLgort.Text.ToString().Trim();
            QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
            DataTable dtData = new DataTable();
            if (chkMblnr.Checked == true)
            {
                dtData = objStorageOut.GetWhticData(Mandt, Comcd, Werks, Lgort, strBoxID, strToLgort, "MBLNR");
            }
            else
            {
                dtData = objStorageOut.GetWhticData(Mandt, Comcd, Werks, Lgort, strBoxID, strToLgort, "BOXID");
            }
            if (dtData.Rows.Count > 0)
            {
                foreach (DataRow item in allData.Rows)
                {
                    if (item["BOXID"].ToString().Trim() == dtData.Rows[0]["BOXID"].ToString().Trim())
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        MessageBox.Show("该单已被刷入，请确认！");
                        txtBoxID.Text = "";
                        return;
                    }
                    //DataTable drData = new DataTable(); 
                }
                if (allData.Columns.Count == 0)
                {
                    allData = dtData.Clone();
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //INSMK,CHARG,LIFNR,TRNTP,KOSTL,MENGE
                    DataRow dr = allData.NewRow();
                    dr["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                    dr["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                    dr["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                    dr["OldLGORT"] = dtData.Rows[i]["OldLGORT"].ToString();
                    dr["NowLGORT"] = dtData.Rows[i]["NowLGORT"].ToString();
                    dr["TOLGORT"] = dtData.Rows[i]["ToLgort"].ToString();
                    dr["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                    dr["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                    dr["MENGE"] = dtData.Rows[i]["MENGE"];
                    dr["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                    dr["BOXID"] = dtData.Rows[i]["BOXID"].ToString();
                    dr["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                    dr["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString();
                    dr["TRNTP"] = dtData.Rows[i]["TRNTP"].ToString();
                    dr["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString();
                    allData.Rows.Add(dr);
                }
                Sound.Play(@"Sound\BIU.wav");  //刷入的boxid與EC單的boxid連結成功
                ShowWhticDataGrid();
                cmbWerks.Enabled = false;
                cmbLgort.Enabled = false;
                txtToLgort.Enabled = false;
                btnSave.Enabled = true;
                this.txtBoxID.Focus();
                this.txtBoxID.SelectAll();
            }
            else
            {
                Sound.Play(@"Sound\ERROR.wav");
                stsWarning.Text = "No Data";
                return;
            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (allData.Rows.Count > 0)
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                bool result = objStorageOut.UpdateToLgort(allData);
                if (result)
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                    return;
                }
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            txtToLgort.Enabled = true;
            txtToLgort.Text = "";
            txtBoxID.Text = "";
            allData.Clear();
            btnSave.Enabled = false;
            chkMblnr.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        


    }
}
