using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using System.IO;

namespace QWMS
{
    // Manage_StorageQueryCSMC 的摘要描述。
    public partial class Manage_StorageQueryCSMC : System.Windows.Forms.Form
    {
        UserInfo UserData = new UserInfo();
        private string strSerno = "";
        private string strBoxid = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strComcd = "";        
        private DataTable dtData = new DataTable();
        private DataTable dtDataDetail = new DataTable();
        //private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private FileInfo fi;
        private StreamWriter sw;

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

        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
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

        public Manage_StorageQueryCSMC(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;
            try
            {
                StorageIn objAdmin = new StorageIn(UserData,Progid);
                //檢查權限
                if (!objAdmin.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
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

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
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
                    strLgort = "";
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
                    cmbLgort.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcSTATU = new DataGridViewTextBoxColumn();
                dgvcSTATU.DataPropertyName = "STATU";
                dgvcSTATU.HeaderText = "Status";
                dgvcSTATU.Width = 50;
                dgvcSTATU.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSTATU);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 60;
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMTYPE = new DataGridViewTextBoxColumn();
                dgvcMTYPE.DataPropertyName = "MTYPE";
                dgvcMTYPE.HeaderText = "Type";
                dgvcMTYPE.Width = 40;
                dgvcMTYPE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMTYPE);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "Document No";
                dgvcMBLNR.Width = 140;
                dgvcMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 40;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Part No";
                dgvcMATNR.Width = 80;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 40;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "Version";
                dgvcCHARG.Width = 60;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 50;
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Quantity";
                dgvcMenge.Width = 60;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "Handled Quantity";
                dgvcOTQTY.ReadOnly = true;
                dgvcOTQTY.Width = 60;
                this.dgvData.Columns.Add(dgvcOTQTY);

                if (checkBox1.Checked == true)
                {
                    DataGridViewTextBoxColumn dgvcCRTDAT = new DataGridViewTextBoxColumn();
                    dgvcCRTDAT.DataPropertyName = "CRTDAT";
                    dgvcCRTDAT.HeaderText = "Store In Date";
                    dgvcCRTDAT.ReadOnly = true;
                    dgvcCRTDAT.Width = 100;
                    this.dgvData.Columns.Add(dgvcCRTDAT);
                }
                else
                {
                    DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                    dgvcSERNO.DataPropertyName = "SERNO";
                    dgvcSERNO.HeaderText = "S/N";
                    dgvcSERNO.ReadOnly = true;
                    dgvcSERNO.Width = 90;
                    this.dgvData.Columns.Add(dgvcSERNO);
                }

                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "Box ID";
                dgvcBOXID.Width = 120;
                dgvcBOXID.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBOXID);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //this.btnQuery.Enabled = false;
            string strWerks = cmbWerks.Text.ToString().Trim();
            string strLgort = cmbLgort.Text.ToString().Trim();
            strSerno = txtSerno.Text.ToString().Trim();
            strBoxid = txtBoxid.Text.ToString().Trim();
            strLocat = txtLocat.Text.ToString().Trim();
            strMatnr = txtMatnr.Text.ToString().Trim();
            string strBoxStatus = "";
            if (checkBox1.Checked == true)
            {
                strBoxStatus = "Y";
            }
            CSMCStorageData objCSMC_Data=new CSMCStorageData(UserData);
            dtData = objCSMC_Data.QueryStorageDataCSMC(strWerks,strLgort,strSerno,strBoxid,strLocat,strMatnr,strBoxStatus,cmbStatus.Text.Trim());
            
            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnQuery.Enabled = true;
            txtSerno.Text = "";
            txtBoxid.Text = "";
            txtLocat.Text = "";
            txtMatnr.Text = "";
            this.dgvData.DataSource = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                saveFileDialog1.FileName = "Inventory.xls";
                //sfdSaveFile.FileName = "Inventory.xls";
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

        #region 导出xls文件
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                dtDataDetail = dtData.Copy();
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                string strBoxStatus = "";
                if (checkBox1.Checked == true)
                {
                    strBoxStatus = "Y";
                }

                if (strBoxStatus == "Y")
                {
                    strLine = "STATU\tLOCAT\tMTYPE\tMBLNR\tWERKS\tLGORT\tMATNR\tINSMK\tCHARG\tLIFNR\tMENGE\tOTQTY\tBOXID";
                    sw.WriteLine(strLine);
                    //Reading data                    

                    for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtDataDetail.Rows[i]["STATU"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LOCAT"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["MANDT"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["MTYPE"].ToString() + "\t";                      
                        ColumnValid(dtDataDetail, i, "MTYPE", ref strLine);

                        strLine += dtDataDetail.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LGORT"].ToString() + "\t";

                        strLine += dtDataDetail.Rows[i]["MATNR"].ToString() + "\t";

                        strLine += dtDataDetail.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CHARG"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["LIFNR"].ToString() + "\t";
                        ColumnValid(dtDataDetail, i, "LIFNR", ref strLine);

                        strLine += dtDataDetail.Rows[i]["MENGE"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["OTQTY"].ToString() + "\t";
                        ColumnValid(dtDataDetail, i, "OTQTY", ref strLine);

                        //strLine += dtDataDetail.Rows[i]["SERNO"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["BOXID"].ToString();

                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    strLine = "STATU\tLOCAT\tMTYPE\tMBLNR\tWERKS\tLGORT\tMATNR\tINSMK\tCHARG\tLIFNR\tMENGE\tOTQTY\tSERNO\tBOXID";
                    sw.WriteLine(strLine);
                    //Reading data                    

                    for (int i = 0; i < dtDataDetail.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtDataDetail.Rows[i]["STATU"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LOCAT"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["MANDT"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["MTYPE"].ToString() + "\t";
                        ColumnValid(dtDataDetail, i, "MTYPE", ref strLine);

                        strLine += dtDataDetail.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["LGORT"].ToString() + "\t";

                        strLine += dtDataDetail.Rows[i]["MATNR"].ToString() + "\t";

                        strLine += dtDataDetail.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["CHARG"].ToString() + "\t";

                        //strLine += dtDataDetail.Rows[i]["LIFNR"].ToString() + "\t";
                        ColumnValid(dtDataDetail, i, "LIFNR", ref strLine);

                        strLine += dtDataDetail.Rows[i]["MENGE"].ToString() + "\t";
                        //strLine += dtDataDetail.Rows[i]["OTQTY"].ToString() + "\t";
                        ColumnValid(dtDataDetail, i, "OTQTY", ref strLine);

                        strLine += dtDataDetail.Rows[i]["SERNO"].ToString() + "\t";
                        strLine += dtDataDetail.Rows[i]["BOXID"].ToString();

                        sw.WriteLine(strLine);
                    }
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

        //Modify by Michael 20150323 for 解决字段不存在的问题
        private void ColumnValid(DataTable dtDataDetail, int i, string Column, ref string strLine)
        {
            if (dtDataDetail.Columns.Contains(Column))
                strLine += dtDataDetail.Rows[i][Column].ToString() + "\t";
            else
                strLine += "\t";
        }

        #endregion

        private void btnSN_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "打开";
            OpenFile.InitialDirectory = @"桌面";
            OpenFile.Filter = "文本文件(*.txt|*.txt;)";
            DialogResult drResult = OpenFile.ShowDialog();
            if (drResult == DialogResult.OK)
                path = OpenFile.FileName;
            else
                return;
            StreamReader myReader;
            myReader = new StreamReader(path);
           string strHeight = myReader.ReadToEnd();
            myReader.Close();
            txtSerno.Text = strHeight.Replace("\r\n","','");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "打开";
            OpenFile.InitialDirectory = @"桌面";
            OpenFile.Filter = "文本文件(*.txt|*.txt;)";
            DialogResult drResult = OpenFile.ShowDialog();
            if (drResult == DialogResult.OK)
                path = OpenFile.FileName;
            else
                return;
            StreamReader myReader;
            myReader = new StreamReader(path);
            string strHeight = myReader.ReadToEnd();
            myReader.Close();
            txtBoxid.Text = strHeight.Replace("\r\n", "','");
        }
       
    }
}
