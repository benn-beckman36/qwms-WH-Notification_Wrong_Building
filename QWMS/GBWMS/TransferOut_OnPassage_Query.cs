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
using System.IO;

namespace QWMS
{
    public partial class TransferOut_OnPassage_Query : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private DataTable dtData = new DataTable();
        private DataTable dtDataDetail = new DataTable();
        private StreamWriter sw = null;

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

        #endregion

        //构造函数
        public TransferOut_OnPassage_Query(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new StorageIn(UserData, strProgid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority("MANAGE"))
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = cmbWerks.Text.ToString().Trim();
            string strLgort = cmbLgort.Text.ToString().Trim();
            string strPN = txtMatnr.Text.ToString().Trim();
            string strDateFrom = dtFrom.Value.ToString("yyyy-MM-dd");
            //查询Data
            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
            dtData = objStorageData.QueryOnPassageQty(strPN, strDateFrom);

            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.txtMatnr.Text = "";
            this.dtFrom.Value = DateTime.Now;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                saveFileDialog1.FileName = "Inventory.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strExportName = saveFileDialog1.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 初始化
        //設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        //厂别       
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

        //仓别
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                //当前Plant
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                    dtTemp = objAuthority.CheckLgortAuthority();
                //现有值
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    cmbLgort.Items.Clear();

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
                        //去重复值
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                            if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                            {
                                cmbLgort.SelectedIndex = i;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region 公共方法

        #region 导出xls文件
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                dtDataDetail = dtData.Copy();
                FileInfo fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "DocumentNo\tPlant\tStorage\tVersion\tPartNo\tCreateDate\tQTY";
                sw.WriteLine(strLine);
                //Reading data                    

                for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtDataDetail.Rows[i]["DocumentNo"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["Plant"].ToString() + "\t";                
                    strLine += dtDataDetail.Rows[i]["Storage"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["Version"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["PartNo"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["CreateDate"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["QTY"].ToString();

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

        //DataGrid Columns
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "DocumentNo";
                dgvcMBLNR.HeaderText = "Document No";
                dgvcMBLNR.Width = 160;
                dgvcMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "Plant";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "Storage";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "Version";
                dgvcCHARG.HeaderText = "Version";
                dgvcCHARG.Width = 60;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "PartNo";
                dgvcMATNR.HeaderText = "Part No";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcFRMDAT = new DataGridViewTextBoxColumn();
                dgvcFRMDAT.DataPropertyName = "CreateDate";
                dgvcFRMDAT.HeaderText = "Create Date";
                dgvcFRMDAT.ReadOnly = true;
                dgvcFRMDAT.Width = 120;
                this.dgvData.Columns.Add(dgvcFRMDAT);

                DataGridViewTextBoxColumn dgvcQTY = new DataGridViewTextBoxColumn();
                dgvcQTY.DataPropertyName = "QTY";
                dgvcQTY.HeaderText = "On Passage Quantity";
                dgvcQTY.ReadOnly = true;
                dgvcQTY.Width = 160;
                dgvcQTY.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcQTY);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
