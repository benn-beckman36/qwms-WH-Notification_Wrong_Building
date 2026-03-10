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
    public partial class StorageIn_IQC_Query : Form
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
        string strStartDate = "";
        string strEndDate = "";

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
        public StorageIn_IQC_Query(UserInfo varUserData, string strProgid)
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
            string strRefDoc = txtRefDoc.Text.ToString().Trim();
            string strMatnr = txtMatnr.Text.ToString().Trim();
            strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
            strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
            //查询Data
            StorageData objIQCData = new StorageData(UserData, strWerks, strLgort);
            dtData = objIQCData.QueryDataForIQCAdd(strRefDoc, strMatnr, strStartDate, strEndDate);

            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.txtRefDoc.Text = "";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                saveFileDialog1.FileName = "IQC.xls";
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
                strLine = "Plant\tRecv Sloc\tVendor\tPart No\tRef.Doc\tBatch\tTo Unrestricted\tCost Center\tIssue Slo\tMovement Type\tINSMK";
                sw.WriteLine(strLine);
                //Reading data                    

                for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtDataDetail.Rows[i]["Plant"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["RecvSloc"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["Vendor"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["Material"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["RefDoc"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["Batch"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["ToUnrestricted"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["CostCenter"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["IssueSlo"].ToString() + "\t";
                    strLine += dtDataDetail.Rows[i]["MovementType"].ToString()+"\t";
                    strLine += dtDataDetail.Rows[i]["INSMK"].ToString();

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
                dgvcMBLNR.DataPropertyName = "RefDoc";
                dgvcMBLNR.HeaderText = "结单单号";
                dgvcMBLNR.Width = 130;
                dgvcMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMBLNR);
               
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "Plant";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.Width = 50;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                

                

                DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                dgvcKOSTL.DataPropertyName = "CostCenter";
                dgvcKOSTL.HeaderText = "部门代码";
                dgvcKOSTL.ReadOnly = true;
                dgvcKOSTL.Width = 60;
                this.dgvData.Columns.Add(dgvcKOSTL);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "Material";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "Batch";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = true;
                dgvcCHARG.Width =30;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "Vendor";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.Width = 70;
                dgvcLIFNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "MovementType";
                dgvcBWART.HeaderText = "异动代码";
                dgvcBWART.ReadOnly = true;
                dgvcBWART.Width = 50;
                this.dgvData.Columns.Add(dgvcBWART);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "ToUnrestricted";
                dgvcMENGE.HeaderText = "结单数量";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.Width = 80;
                dgvcMENGE.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "状态";
                dgvcINSMK.ReadOnly = true;
                dgvcINSMK.Width = 20;
                dgvcINSMK.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcINSMK);

                

                DataGridViewTextBoxColumn dgvcUMLGO = new DataGridViewTextBoxColumn();
                dgvcUMLGO.DataPropertyName = "IssueSlo";
                dgvcUMLGO.HeaderText = "起始仓别";
                dgvcUMLGO.ReadOnly = true;
                dgvcUMLGO.Width = 60;
                this.dgvData.Columns.Add(dgvcUMLGO);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "RecvSloc";
                dgvcLGORT.HeaderText = "目的仓别";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "创建时间";
                dgvcCRDAT.ReadOnly = true;
                dgvcCRDAT.Width = 120;
                dgvcCRDAT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcCRDAT);

                DataGridViewTextBoxColumn dgvcREMARK1 = new DataGridViewTextBoxColumn();
                dgvcREMARK1.DataPropertyName = "REMAK1";
                dgvcREMARK1.HeaderText = "SAP回执";
                dgvcREMARK1.ReadOnly = true;
                dgvcREMARK1.Width = 100;
                dgvcREMARK1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcREMARK1);

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

     
    }
}
