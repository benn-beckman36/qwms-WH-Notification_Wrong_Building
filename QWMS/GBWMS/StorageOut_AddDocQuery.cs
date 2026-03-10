using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QCI_QWMS_StorageOut;
using QWMS.Common;
using System.Data;
using System.IO;

namespace QWMS
{
    public partial class StorageOut_AddDocQuery : Form
    {
        #region 变数宣告

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMblnrNew = "";
        public string strDateFrom = "";
        public string strDateTo = "";
        UserInfo UserData = new UserInfo();
        AddDoc objAddDoc;
        DataTable dtData = new DataTable();
        private FileInfo fi;
        private StreamWriter sw;





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

        public string Mblnrs
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }


        #endregion

        #endregion

        public StorageOut_AddDocQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objAddDoc=new AddDoc(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDataGrid();
                    Query();

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

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = "";
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
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    cmbLgort.Items.Add("");
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
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

        private void ShowDataGrid()
        {

            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "扣帐单号";
                dgvcMblnr.Width = 120;
                dgvcMblnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMblnr);

                //MBLNR261
                DataGridViewTextBoxColumn dgvcMBLNR311 = new DataGridViewTextBoxColumn();
                dgvcMBLNR311.DataPropertyName = "MBLNR311";
                dgvcMBLNR311.HeaderText = "加扣单号";
                dgvcMBLNR311.Width =120;
                dgvcMBLNR311.ReadOnly = true;
                dgvData.Columns.Add(dgvcMBLNR311);

                //MATNR
                DataGridViewTextBoxColumn dgvcAddNo = new DataGridViewTextBoxColumn();
                dgvcAddNo.DataPropertyName = "PARTNO";
                dgvcAddNo.HeaderText = "料号";
                dgvcAddNo.Width = 90;
                dgvcAddNo.ReadOnly = true;
                dgvData.Columns.Add(dgvcAddNo);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvData.Columns.Add(dgvcCharg);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "出库仓别";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

                //DLGORT
                DataGridViewTextBoxColumn dgvcDLgort = new DataGridViewTextBoxColumn();
                dgvcDLgort.DataPropertyName = "UMLGO";
                dgvcDLgort.HeaderText = "目的仓别";
                dgvcDLgort.Width = 90;
                dgvcDLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcDLgort);
   
                //AddQTY.
                DataGridViewTextBoxColumn dgvcAddQTY = new DataGridViewTextBoxColumn();
                dgvcAddQTY.DataPropertyName = "MENGE";
                dgvcAddQTY.HeaderText = "加扣数量";
                dgvcAddQTY.Width = 100;
                dgvcAddQTY.ReadOnly = false;
                //dgvData.DefaultCellStyle = NumericUpDown;
                dgvData.Columns.Add(dgvcAddQTY);

                //CREDATE
                DataGridViewTextBoxColumn dgvcDateTime = new DataGridViewTextBoxColumn();
                dgvcDateTime.DataPropertyName = "CRDAT";
                dgvcDateTime.HeaderText = "加扣时间";
                dgvcDateTime.Width = 120;
                dgvcDateTime.ReadOnly = true;
                dgvData.Columns.Add(dgvcDateTime);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
    
        public void Query()
        {
            strWerks = cmbWerks.Text.ToString();
            strLgort = cmbLgort.Text.ToString();
            strMblnr = txtMblnr.Text.Trim().ToString();
            strMblnrNew = txtAddMblnr.Text.Trim().ToString();
            strDateFrom = dateFrom.Value.ToString("yyyy-MM-dd") + " 00:00:000";
            strDateTo = dateTo.Value.ToString("yyyy-MM-dd") + " 23:59:000";
            stsWarning.Text = "";

            dtData = objAddDoc.QueryAddDocInfo(strWerks, strLgort, strMblnr,strMblnrNew, strDateFrom, strDateTo);
            if (dtData.Rows.Count <= 0)
            {
                stsWarning.Text = "当前没有加扣数据";
            }
            ShowDataGrid();

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btnDwn_Click(object sender, EventArgs e)
        {
            if (dtData.Rows.Count > 0)
            {
                //string strExportName = "加扣信息_"+DateTime.Now.ToShortDateString();
                string strExportName = "";
                try
                {
                    if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        
                         strExportName = sfdSaveFile.FileName;
                        //sfdSaveFile.FileName=strExportName;
                        CountingResult2File(strExportName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "扣账单号\t加扣单号\t料号\t版本\t出库仓别\t目的仓别\t加扣数量\t时间";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MBLNR261"].ToString() + "\t";
                    strLine += dtData.Rows[i]["PARTNO"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRDAT"].ToString();
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

        private void StorageOut_AddDocQuery_Resize(object sender, EventArgs e)
        {
            //panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }


 
    }

}
