using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;
using System.Text.RegularExpressions;
using System.Collections;
namespace QWMS
{
    public partial class InventoryCheck_CYC : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strInsmk = "";
        private DataTable dtData = new DataTable();
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private string strUID = string.Empty;
        string pasteText = string.Empty;
        ArrayList arrLgort = new ArrayList();

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
                return strLgort;
            }
            set
            {
                strLgort = value;
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

        public InventoryCheck_CYC()
        {
            InitializeComponent();
        }

        public InventoryCheck_CYC(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objCounting = new Counting(UserData, Progid);
                objStorageIn = new StorageIn(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
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
        # endregion

        #region Comfirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            strLgort = txtLgort.Text.ToUpper();
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (strWerks == "")
            {
                stsWarning.Text = "Plant no can't be empty!!";
                return;
            }

            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            //dtData = objCounting.QueryCycFromOA(strWerks, strLgort, "");
            OAMM.ICMSServiceClient objOAMM = new OAMM.ICMSServiceClient();
            DataTable dtResult = objOAMM.ICInventoryDataForEC(strWerks, strLgort, "2");
            #region 将OA的数据存入到WHCYC_OA表中
            if (!objCounting.insertWhcycOA(dtResult, strWerks, strLgort))
            {
                MessageBox.Show("请重新点击Confirm");
                return;
            }
            #endregion

            dtData = objCounting.GetWhcycOA(strWerks, strLgort);
            ShowDataGrid();
            this.btnSave.Enabled = true;
            
        }
        #endregion

        # region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {

                DataGridViewTextBoxColumn dgvcCycTICKETFROM = new DataGridViewTextBoxColumn();
                dgvcCycTICKETFROM.DataPropertyName = "TICKETFROM";
                dgvcCycTICKETFROM.HeaderText = "TICKETFROM";
                dgvcCycTICKETFROM.ReadOnly = true;
                dgvcCycTICKETFROM.Width = 100;
                dgvData.Columns.Add(dgvcCycTICKETFROM);

                DataGridViewTextBoxColumn dgvcCycTICKETTO = new DataGridViewTextBoxColumn();
                dgvcCycTICKETTO.DataPropertyName = "TICKETTO";
                dgvcCycTICKETTO.HeaderText = "TICKETTO";
                dgvcCycTICKETTO.ReadOnly = true;
                dgvcCycTICKETTO.Width = 100;
                dgvData.Columns.Add(dgvcCycTICKETTO);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcCheckStatus = new DataGridViewTextBoxColumn();
                dgvcCheckStatus.DataPropertyName = "CheckStatus";
                dgvcCheckStatus.HeaderText = "CheckStatus";
                dgvcCheckStatus.ReadOnly = true;
                dgvData.Columns.Add(dgvcCheckStatus);

                DataGridViewTextBoxColumn dgvcsubStorage = new DataGridViewTextBoxColumn();
                dgvcsubStorage.DataPropertyName = "subStorage";
                dgvcsubStorage.HeaderText = "subStorage";
                dgvcsubStorage.ReadOnly = true;
                dgvData.Columns.Add(dgvcsubStorage);

                DataGridViewTextBoxColumn dgvcUID = new DataGridViewTextBoxColumn();
                dgvcUID.DataPropertyName = "UID";
                dgvcUID.HeaderText = "UID";
                dgvcUID.ReadOnly = true;
                dgvData.Columns.Add(dgvcUID);

                dgvData.DataSource = dtData;

                lblCount.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.btnSave.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.txtLgort.Text = "";
                stsWarning.Text = "";
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

        #region  Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            QCI.QWMS.StorageData objStorageData = new StorageData(UserData);
            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            string strNow = DateTime.Now.ToString();
            stsWarning.Text = "上传盘点资料当中，请勿关闭窗体....";

            int i = 0;
            DataTable dtZBA19 = new DataTable();
            DataTable dtZBA21 = new DataTable();
            dtZBA19.TableName = "ZBA19";
            dtZBA21.TableName = "ZBA21";
            if (dtZBA21.Columns.Count == 0)
            {
                dtZBA21.Columns.Add("WERKS");
                dtZBA21.Columns.Add("LGORT");
                dtZBA21.Columns.Add("FLAG");
            }
            objCounting.ExecLog_CYC(strWerks, "", "", 0, "Begin", "", "JOB", strNow);//记录log
            DataRow[] drLgorts = dtData.Select("WERKS='" + strWerks + "'");
            string[] strUIDList = new string[drLgorts.Length];
            foreach (DataRow dr in drLgorts)
            {
                strLgort = dr["LGORT"].ToString();
                strUIDList[i] = dr["UID"].ToString();
                DataRow drZBA21 = dtZBA21.NewRow();
                drZBA21["WERKS"] = strWerks;
                drZBA21["LGORT"] = strLgort;
                drZBA21["FLAG"] = "1";
                dtZBA21.Rows.Add(drZBA21.ItemArray);
                i++;
            }
            try
            {
                if(txtLgort.Text.ToString() == "")
                {
                   dtZBA19 = objCounting.QueryWHCYC(strWerks);
                   dtZBA19.TableName = "ZBA19";
                }
                else
                {
                    dtZBA19 = objCounting.QueryWHCYC_Lgort(strWerks,strLgort);
                    dtZBA19.TableName = "ZBA19";
                }
                List<DataRow> ls = dtZBA19.AsEnumerable().Where(x => x.Field<decimal>("BUCHM") < 0).ToList();
                int inCheck = ls.Count();
                if (inCheck > 0)
                {
                    StringBuilder sbCheck = new StringBuilder();
                    foreach(DataRow dr in ls)
                    {
                        sbCheck.Append(dr["WERKS"].ToString() + " " + dr["LGORT"].ToString() + " " + dr["LOCAT"].ToString() + " " + dr["MATNR"].ToString());
                        sbCheck.Append("\t\n");
                    }
                    objCounting.ExecLog(strWerks, "", "", dtZBA19.Rows.Count, "ERR", "库存带有负数", "JOB", strNow);//记录log
                    MessageBox.Show("当前库存存在负数，请核对库存信息，信息如下：\r\n" + sbCheck.ToString());
                    stsWarning.Text = string.Empty;
                    return;
                }
                string strMessage = string.Empty;
                objCounting.ExecLog(strWerks, "", "", dtZBA19.Rows.Count, "OA", "", "JOB", strNow);//记录log
                OAMM.ICMSServiceClient objOAMM = new OAMM.ICMSServiceClient();
                bool blResult = objOAMM.LogisticsKeyinDataInterfaceForQWMS(strMandt, dtZBA19, dtZBA21, strUIDList, "System", out strMessage);
                //bool blResult = true;
                if (blResult)
                {
                    objCounting.ExecLog(strWerks, "", "", dtZBA19.Rows.Count, "End", strMessage, "JOB", strNow);//记录log
                    stsWarning.Text = "同步OA盘点管理系统成功";
                }
                else
                {
                    objCounting.ExecLog(strWerks, "", "", dtZBA19.Rows.Count, "End", strMessage, "JOB", strNow);
                    stsWarning.Text = "同步OA盘点管理系统失败" + strMessage;

                }
                this.btnSave.Enabled = false;
                dgvData.DataSource = null;
                dtData.Clear();
                dtZBA19.Clear();
                dtZBA21.Clear();
            }
            catch (Exception ex)
            {
                objCounting.ExecLog_CYC(strWerks, "", "", dtZBA19.Rows.Count, "End", ex.ToString(), "JOB", strNow);//记录log
            }

 
        }
        #endregion

    }
}
