using QCI.QWMS;
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
    public partial class Manage_IntelligentReceive : Form
    {
        #region DataMember

        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;
        private string strEC = string.Empty;
        private string strBoxid = string.Empty;
        private string strFromDate = string.Empty;
        private string strToDate = string.Empty;
        private string strStatus = string.Empty;
        private DataTable dtECHeadSource = new DataTable();//EC单表头
        private DataTable dtECItemSource = new DataTable();//EC单表体
        private DataTable dtBoxID = new DataTable();//BOXID数据
        private DataTable dtStorage = new DataTable();
        QCI.QWMS.StorageIn objStorageIn;
        QCI.QWMS.StorageData objStorageData;
        QCI.QWMS.Admin objAdmin;
        QCI.QWMS.PlantData objPlantData;
        QCI.QWMS.LogData objLogData;
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
        public Manage_IntelligentReceive(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            stsUsrnm.Text = Usrnm;

            try
            {
                objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objStorageData = new StorageData(UserData);
                objLogData = new LogData(UserData, string.Empty, string.Empty, Progid);
                ShowStatusData();
                ShowDdlWerks();
                ShowStorageInData(); 
                ShowECHeadDataGrid();
                ShowECItemDataGrid(dtECItemSource);
                rdBtnQuery.Checked = false;
                rdBtnSolve.Checked = false;
                if (cmbWerks.Items.Count > 0)
                {
                    this.cmbWerks.SelectedIndex = 0;
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
            this.stsWarning.Width = 1000;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAdmin.PermissionQuery(Mandt, Comcd, string.Empty, Usrnm);
                if(dtTemp.Rows.Count>0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        if (!cmbWerks.Items.Contains(dtTemp.Rows[i]["WERKS"].ToString()))
                        {
                            cmbWerks.Items.Add(dtTemp.Rows[i]["WERKS"].ToString());
                        }
                    }
                    cmbWerks.cmbRows();
                }
                else
                {
                    MessageBox.Show("请先对该账号维护厂区仓别信息");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }


        #region DataGridView

        #region ShowECHeadDataGrid
        private void ShowECHeadDataGrid()
        {
            dgvECHead.AutoGenerateColumns = false;
            dgvECHead.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.Name = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvECHead.Columns.Add(dgvcSelect);

                //PNUM
                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.HeaderText = "EC单号";
                dgvcPNUM.Name = "PNUM";
                dgvcPNUM.Width = 130;
                dgvcPNUM.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcPNUM);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "VendorCode";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 90;
                dgvcLIFNR.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcBKTXT = new DataGridViewTextBoxColumn();
                dgvcBKTXT.DataPropertyName = "BKTXT";
                dgvcBKTXT.HeaderText = "报关单号";
                dgvcBKTXT.Name = "BKTXT";
                dgvcBKTXT.Width = 130;
                dgvcBKTXT.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcBKTXT);

                DataGridViewTextBoxColumn dgvcCRLNAM = new DataGridViewTextBoxColumn();
                dgvcCRLNAM.DataPropertyName = "CLRNAM";
                dgvcCRLNAM.HeaderText = "类型";
                dgvcCRLNAM.Name = "CLRNAM";
                dgvcCRLNAM.Width = 90;
                dgvcCRLNAM.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcCRLNAM);

                DataGridViewTextBoxColumn dgvcSTATUS = new DataGridViewTextBoxColumn();
                dgvcSTATUS.DataPropertyName = "STATUS";
                dgvcSTATUS.HeaderText = "状态";
                dgvcSTATUS.Name = "STATUS";
                dgvcSTATUS.Visible = false;
                dgvECHead.Columns.Add(dgvcSTATUS);

                dgvECHead.DataSource = dtECHeadSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowECItemDataGrid
        private void ShowECItemDataGrid(DataTable dtData)
        {
            //select  PNUM,PITEM,MATNR,CHARG1,MENGE,LGORT,KOSTL,AEC,CHECKSTATS
            dgvECItem.AutoGenerateColumns = false;
            dgvECItem.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcPITEM = new DataGridViewTextBoxColumn();
                dgvcPITEM.DataPropertyName = "PITEM";
                dgvcPITEM.Name = "PITEM";
                dgvcPITEM.HeaderText = "序号";
                dgvcPITEM.Width = 50;
                dgvcPITEM.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcPITEM);

                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.Name = "PNUM";
                dgvcPNUM.HeaderText = "EC单号";
                dgvcPNUM.Width = 90;
                dgvcPNUM.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcPNUM);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.Name = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcLGORT);

                //BOXID
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG1 = new DataGridViewTextBoxColumn();
                dgvcCHARG1.DataPropertyName = "CHARG1";
                dgvcCHARG1.Name = "CHARG1";
                dgvcCHARG1.HeaderText = "版本";
                dgvcCHARG1.Width = 70;
                dgvcCHARG1.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcCHARG1);

                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 70;
                dgvcMENGE.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcMENGEC = new DataGridViewTextBoxColumn();
                dgvcMENGEC.DataPropertyName = "MENGEC";
                dgvcMENGEC.Name = "ALQTY";
                dgvcMENGEC.HeaderText = "已刷数量";
                dgvcMENGEC.Width = 70;
                dgvcMENGEC.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMENGEC);

                //AEC
                DataGridViewTextBoxColumn dgvcAEC = new DataGridViewTextBoxColumn();
                dgvcAEC.DataPropertyName = "AEC";
                dgvcAEC.Name = "AEC";
                dgvcAEC.HeaderText = "AEC";
                dgvcAEC.Width = 50;
                dgvcAEC.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcAEC);

                //CHECKSTATS
                DataGridViewTextBoxColumn dgvcCHECKSTATS = new DataGridViewTextBoxColumn();
                dgvcCHECKSTATS.DataPropertyName = "CHECKSTATS";
                dgvcCHECKSTATS.Name = "CHECKSTATS";
                dgvcCHECKSTATS.HeaderText = "检验";
                dgvcCHECKSTATS.Width = 50;
                dgvcCHECKSTATS.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcCHECKSTATS);

                dgvECItem.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowBoxDataGrid
        private void ShowBoxDataGrid(DataTable dtdata)
        {
            dgvBox.AutoGenerateColumns = false;
            dgvBox.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvBox.Columns.Add(dgvcMATNR);

                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.Width = 90;
                dgvBox.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvLocat = new DataGridViewTextBoxColumn();
                dgvLocat.DataPropertyName = "LOCAT";
                dgvLocat.Name = "LOCAT";
                dgvLocat.HeaderText = "储位";
                dgvLocat.Width = 130;
                //dgvLocat.ReadOnly = true;
                dgvBox.Columns.Add(dgvLocat);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "VendorCode";
                dgvcLifnr.Name = "LIFNR";
                dgvcLifnr.Width = 100;
                dgvcLifnr.ReadOnly = true;
                dgvBox.Columns.Add(dgvcLifnr);

                if (objPlantData.CheckCHARGLGORT(strWerks))
                {
                    DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                    dgvcCharg.DataPropertyName = "CHARG";
                    dgvcCharg.HeaderText = "Charg";
                    dgvcCharg.Name = "CHARG";
                    dgvcCharg.Width = 100;
                    dgvcCharg.ReadOnly = true;
                    dgvBox.Columns.Add(dgvcCharg);
                }

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DACOD";
                dgvcDACOD.Name = "DACOD";
                dgvcDACOD.Width = 90;
                dgvcDACOD.ReadOnly = true;
                dgvBox.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "VEDAT";
                dgvcVedat.Name = "VEDAT";
                dgvcVedat.Width = 90;
                dgvcVedat.ReadOnly = true;
                dgvBox.Columns.Add(dgvcVedat);

                DataGridViewTextBoxColumn dgvcBoxID = new DataGridViewTextBoxColumn();
                dgvcBoxID.DataPropertyName = "BOXID";
                dgvcBoxID.HeaderText = "BOXID";
                dgvcBoxID.Name = "BOXID";
                dgvcBoxID.Visible = false;
                dgvBox.Columns.Add(dgvcBoxID);

                DataGridViewTextBoxColumn dgvcBoxItem = new DataGridViewTextBoxColumn();
                dgvcBoxItem.DataPropertyName = "BOXITEM";
                dgvcBoxItem.HeaderText = "BOXITEM";
                dgvcBoxItem.Name = "BOXITEM";
                dgvcBoxItem.Visible = false;
                dgvBox.Columns.Add(dgvcBoxItem);
                dgvBox.DataSource = dtdata; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #endregion
        private void getShowEcItem()
        {
            if(dtECItemSource.Rows.Count>0)
            {
                int sumMatnr = 0;
                int sumMengec = 0;
                foreach (DataRow drItem in dtECItemSource.Rows)
                {
                    sumMatnr += int.Parse(drItem["MENGE"].ToString());
                    sumMengec += int.Parse(drItem["MENGEC"].ToString());
                }
                DataTable dtData = dtECItemSource.Copy();
                DataRow drNew = dtData.NewRow();
                drNew["MENGE"] = sumMatnr;
                drNew["MENGEC"] = sumMengec;
                dtData.Rows.Add(drNew);
                ShowECItemDataGrid(dtData);
            }
            else
            {
                ShowECItemDataGrid(dtECItemSource);
            }
        }
        private void getShowBox(DataTable dtData)
        {
            if(dtData.Rows.Count>0)
            {
                //dtBoxID = objStorageIn.GetECTBoxid(strBoxid, strStatus);
                //DataTable dtData = dtBoxID.Copy();
                DataRow drSum = dtData.NewRow();
                drSum["MENGE"] = objStorageIn.GetBoxSumMenge(dtData);
                dtData.Rows.Add(drSum);
            }
            ShowBoxDataGrid(dtData);
        }

        #region button
        private void btnReset_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            txtEC.Enabled = true;
            btnConfirm.Enabled = true;
            btnSave.Enabled = false;
            rdBtnQuery.Checked = false;
            rdBtnSolve.Checked = false;
            strEC = string.Empty;
            strBoxid = string.Empty;
            strStatus = string.Empty;
            dtECHeadSource.Rows.Clear();
            dtECItemSource.Rows.Clear();
            dtStorage.Rows.Clear();
            dtBoxID.Rows.Clear();
            dgvBox.DataSource = null;
            dgvECHead.DataSource = null;
            dgvECItem.DataSource = null;
            btOnlyQwms.Enabled = false;
            if (strComcd == "9900" && strComcd == "9100")
            {
                btnClearrecord.Enabled = true;
            }
            else
            {
                btnClearrecord.Enabled = false;
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            DataSet();
            if(!rdBtnQuery.Checked&&!rdBtnSolve.Checked)
            {
                stsWarning.Text = "请选择查询或者处理问题";
                return;
            }
            strFromDate = dtpFromDate.Value.ToString("yyyyMMdd");
            strToDate = dtpToDate.Value.ToString("yyyyMMdd");

            #region 输入信息权限
            if (!string.IsNullOrEmpty(txtEC.Text.ToString().Trim().ToUpper()))
            {
                string strUser = objStorageIn.getECUser(txtEC.Text.ToString().Trim().ToUpper(), string.Empty);
                if(strUser.Equals(Usrnm))
                {
                    strEC = txtEC.Text.ToString().Trim().ToUpper();
                }
                else
                {
                    MessageBox.Show("当前用户不是该EC单的作业人，该EC单的作业人是：" + strUser);
                    return;
                }
            }
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (!string.IsNullOrEmpty(txtLgort.Text.ToString().Trim().ToUpper()))
            {
                strLgort = txtLgort.Text.ToString().Trim().ToUpper();
                if(!string.IsNullOrEmpty(strWerks))
                {
                    DataTable dtTemp = objAdmin.PermissionQuery(Mandt, Comcd, strWerks, Usrnm); //校验仓别           
                    if (!dtTemp.Rows[0]["LGORT"].ToString().Contains(strLgort))
                    {
                        MessageBox.Show("当前用户无该仓别EC单操作权限！！！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请选择厂区！！！");
                    return;
                }
            }
            #endregion

            if (string.IsNullOrEmpty(txtMatnr.Text.ToString()))
            {
                if (rdBtnQuery.Checked)
                {
                    if (string.IsNullOrEmpty(txtEC.Text.ToString())) //EC单栏位为空，查询该日期下所有的EC单
                    {
                        dtECHeadSource = objStorageIn.GetECHead(strWerks, string.Empty, string.Empty, strFromDate, strToDate, strStatus, string.Empty);
                    }
                    else  //EC 单栏位不为空，查询该EC单的BOXID关联的所有EC单数据
                    {
                        DataTable dtEC = objStorageIn.GetECHead(strWerks, strEC, string.Empty, strFromDate, strToDate, strStatus, string.Empty);
                        if (dtEC.Rows.Count > 0)
                        {
                            dtECHeadSource = objStorageIn.GetECHead(strWerks, string.Empty, dtEC.Rows[0]["BOXID"].ToString(), strFromDate, strToDate, strStatus, string.Empty);
                        }
                    }
                }
                if (rdBtnSolve.Checked) //问题单处理，只查询该用户该EC单数据
                {
                    dtECHeadSource = objStorageIn.GetECHead(strWerks, strEC, string.Empty, strFromDate, strToDate, strStatus, strUsrnm);
                }
            }
            else
            {
                if(string.IsNullOrEmpty(strWerks))
                {
                    MessageBox.Show("请选择厂区");
                    return;
                }
                string strMatnr = txtMatnr.Text.ToString().ToUpper().Trim();
                dtECHeadSource = objStorageIn.getECHeadByPN(strWerks, strMatnr, strStatus, strLgort);
            }
            ShowECHeadDataGrid();
            if (rdBtnQuery.Checked)
                GetEcHeadColor();
        }

        private void GetEcHeadColor()
        {
            //btnSave.Enabled = true;
            for (int i = 0; i < dgvECHead.RowCount - 1; i++)
            {
                if (dgvECHead.Rows[i].Cells["STATUS"].Value.ToString().Equals("T"))
                {
                    dgvECHead.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            string strMessage = string.Empty;

            #region 判断ECItem数量TTL、EC已刷数量TTL和BOXID数量TTL是否相符，相符才可以丢给SAP扣账才可以丢给SAP扣帐
            //int intEcItem = int.Parse(dgvECItem.Rows[dgvECItem.Rows.Count - 2].Cells["MENGE"].Value.ToString());
            //int intEcItemScan = int.Parse(dgvECItem.Rows[dgvECItem.Rows.Count - 2].Cells["ALQTY"].Value.ToString());
            //int intBox = int.Parse(dgvBox.Rows[dgvBox.Rows.Count - 2].Cells["MENGE"].Value.ToString());
            //if (intEcItem != intEcItemScan || intEcItem != intBox)
            //{
            //    MessageBox.Show("ECItem数量TTL、EC已刷数量TTL和BOXID数量TTL不相符!!!");
            //    return;
            //}
            #endregion

            #region 判断是否已扣账
            foreach (DataGridViewRow drEcHead in dgvECHead.Rows)
            {
                if (drEcHead.Cells["Select"].EditedFormattedValue.ToString().ToUpper().Equals("TRUE"))
                {
                    DataTable dtstatus = objStorageIn.GetPacingStatus(drEcHead.Cells["PNUM"].Value.ToString());//查看当前EC单数据
                    if (dtstatus.Rows.Count > 0)
                    {
                        if (dtstatus.Rows[0]["STATUS"].ToString().Trim() == "Y")
                        {
                            MessageBox.Show("已经完成扣账，不能重新作业！");
                            txtEC.Text = string.Empty;
                            return;
                        }
                    }
                }
            }
            #endregion

            #region EC单扣账
            string stsMessage = string.Empty;
            foreach (DataGridViewRow drEcHead in dgvECHead.Rows)
            {
                if (drEcHead.Cells["Select"].EditedFormattedValue.ToString().ToUpper().Equals("TRUE"))
                {
                    objLogData.AddQWMSLOG(drEcHead.Cells["PNUM"].Value.ToString(), "SAP", "Send", "T扣账", "N", Usrnm);
                    DataSet ds = new DataSet();
                    DataTable dthead = objStorageIn.GetTAB_ZM000(drEcHead.Cells["PNUM"].Value.ToString(), strWerks, "", "", "");
                    DataTable dtitem = objStorageIn.GetTAB_ZM001_NEW(drEcHead.Cells["PNUM"].Value.ToString(), strWerks, "", "");
                    dthead.TableName = "TAB_ZM000";
                    dtitem.TableName = "TAB_ZM001";

                    ds.Tables.Add(dthead.Copy());
                    ds.Tables.Add(dtitem.Copy());
                    ArrayList sqlarr = new ArrayList();
                    DataSet dsreturn = new DataSet();

                    MM.MM_Service objMM = new MM.MM_Service();
                    dsreturn = objMM.Z_RFC_PACKING_POST(Usrnm, drEcHead.Cells["PNUM"].Value.ToString(), "QWMS_ZNSL", string.Empty, ds);
                    DataTable dtTAB_ZM025 = dsreturn.Tables["TAB_ZM025"];
                    if (dtTAB_ZM025.Rows.Count > 0)
                    {
                        if (dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() != "")
                        {
                            StringBuilder strsql = new StringBuilder();
                            strsql.AppendFormat("UPDATE EC_HEAD SET STATUS='Y' WHERE PNUM='{0}'", drEcHead.Cells["PNUM"].Value.ToString());
                            strsql.AppendFormat("UPDATE EC_INSTOCK SET MBLNR='{0}' WHERE PNUM='{1}'", dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim(), drEcHead.Cells["PNUM"].Value.ToString());
                            sqlarr.Add(strsql);
                            foreach (DataRow dr in dtStorage.Rows)
                            {
                                if (dr["PNUM"].Equals(drEcHead.Cells["PNUM"].Value.ToString()))
                                {
                                    dr["MBLNR"] = dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim();
                                }
                            }
                        }
                        for (int i = 0; i < dtTAB_ZM025.Rows.Count; i++)
                        {
                            if (dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim() != "")
                            {
                                StringBuilder strsql = new StringBuilder();
                                strsql.AppendFormat("UPDATE EC_ITEM SET SGTXT='{0}',GDREC='{1}',BKTXT='{2}',GJAHR='{3}',BELNR='{4}',BUZEI='{5}',BUDAT='{6}',UNAME='{7}',MESSAGE=N'{8}' WHERE MANDT='{9}' AND PNUM='{10}' AND PITEM='{11}'",
                                    dtTAB_ZM025.Rows[i]["SGTXT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["GDREC"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BKTXT"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["MJAHR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["ZEILE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BUDAT"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["UNAME"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MESSAGE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MANDT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["PNUM"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["PITEM"].ToString().Trim());
                                sqlarr.Add(strsql);
                            }
                        }

                    }
                    if (sqlarr.Count > 0)
                    {
                        if (objStorageIn.updateZM025(sqlarr))
                        {
                            if (objStorageIn.updateECstatus(drEcHead.Cells["PNUM"].Value.ToString(), "Y", string.Empty))
                            {
                                //DataGridDelEC();
                                foreach (DataRow dr in dtECHeadSource.Rows)
                                {
                                    if (dr["PNUM"].Equals(drEcHead.Cells["PNUM"].Value.ToString()))
                                    {
                                        dr["STATUS"] = "Y";
                                    }
                                }
                            }
                            stsMessage = stsMessage + "\r\n" + drEcHead.Cells["PNUM"].Value.ToString() + "扣账成功，扣账编号为：" + dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() + "";
                        }
                    }
                    else
                    {
                        stsMessage = stsMessage + "\r\n" + drEcHead.Cells["PNUM"].Value.ToString() + "扣账失败，失败原因为：" + dtTAB_ZM025.Rows[0]["MESSAGE"].ToString().Trim() + "！";
                    }
                }
            }
            if (string.IsNullOrEmpty(stsMessage))
            {
                MessageBox.Show("无单据过SAP账");
            }
            else
            {
                MessageBox.Show(stsMessage);
            }
            #endregion

            #region QWMS入库 
            strMessage = string.Empty;
            stsWarning.Text = "QWMS正在入库中，请勿关闭窗口！！！";
            foreach (DataGridViewRow drEcHead in dgvECHead.Rows)
            {
                if (drEcHead.Cells["Select"].EditedFormattedValue.ToString().ToUpper().Equals("TRUE"))
                {
                    objLogData.AddQWMSLOG(drEcHead.Cells["PNUM"].Value.ToString(), "ECT", "Save", "QWMS开始入库", "N", Usrnm);
                    DataTable dtstatus = objStorageIn.GetPacingStatus(drEcHead.Cells["PNUM"].Value.ToString());//查看当前EC单数据
                    if (dtstatus.Rows.Count > 0)
                    {
                        if (dtstatus.Rows[0]["STATUS"].ToString().Trim() == "Y")
                        {
                            DataTable dtStoragein = objStorageIn.GetEcInStock(drEcHead.Cells["PNUM"].Value.ToString());
                            if(dtStoragein.Rows.Count>0)
                            {
                                if (objStorageIn.StorageInWHEC(dtStoragein))
                                {
                                    if (objStorageIn.UpdateEcInStock(drEcHead.Cells["PNUM"].Value.ToString()))
                                    {
                                        strMessage += "\r\n" + drEcHead.Cells["PNUM"].Value.ToString() + "：入库QWMS成功！";
                                    }
                                }
                                else
                                {
                                    strMessage += "\r\n" + drEcHead.Cells["PNUM"].Value.ToString() + "：入库QWMS失败！";
                                }
                            }
                            else
                            {
                                strMessage += "\r\n" + drEcHead.Cells["PNUM"].Value.ToString() + "：无入库数据！";
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(strMessage))
            {
                MessageBox.Show("无入库数据！");
            }
            else
            {
                MessageBox.Show(strMessage);
            }

            #endregion
        }


        private DataTable CombineDataTableByMatnr(DataTable dtData)
        {
            DataTable dtResult = new DataTable();
            try
            {
                dtResult.Columns.Add("MATNR");
                dtResult.Columns.Add("LIFNR");
                dtResult.Columns.Add("MENGE");

                var query = from row in dtData.AsEnumerable()
                            group row by
                            new
                            {
                                p1 = row.Field<string>("MATNR"),
                                p2 = row.Field<string>("LIFNR"),
                            } into m

                            select new
                            {
                                MATNR = m.Key.p1,
                                LIFNR = m.Key.p2,
                                MENGE = m.Sum(n => n.Field<int>("MENGE"))
                            };
                foreach (var item in query)
                {
                    DataRow drResult = dtResult.NewRow();
                    drResult["MATNR"] = item.MATNR;
                    drResult["LIFNR"] = item.LIFNR;
                    drResult["MENGE"] = item.MENGE;
                    dtResult.Rows.Add(drResult.ItemArray);
                }
            }
            catch (Exception e)
            {
                stsWarning.Text = dtData.TableName + "Table转化异常";
            }

            return dtResult;
        }

        #region EC单和BOXID匹配
        private bool matchBox() //入库数据以dtstorage为主
        {
            stsWarning.Text = string.Empty;
            dtStorage.Rows.Clear();
            #region dtBoxID和ECitem匹配
            foreach (DataRow drEcItem in dtECItemSource.Rows)
            {
                int sum = int.Parse(drEcItem["MENGE"].ToString()) - int.Parse(drEcItem["MENGEC"].ToString());//需求数量
                DataRow[] drBoxIDS = dtBoxID.Select("MATNR='" + drEcItem["MATNR"].ToString() + "'  AND LIFNR='" + drEcItem["LIFNR"].ToString() + "' AND MENGE>0 ", "MENGE ASC");

                //#region 判断当前储位DaCode规则
                //foreach (DataRow drBoxID in drBoxIDS)
                //{
                //    StorageData objTempStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, drEcItem["LGORT"].ToString());
                //    if (objTempStorageData.CheckExistedSameMaterialDC(drBoxID["LOCAT"].ToString(), drEcItem["MATNR"].ToString(), "G", drBoxID["DACOD"].ToString()))
                //    {
                //        MessageBox.Show(drEcItem["MATNR"].ToString() + " 该料号在该储位已经存在不同DateCode，请确认");
                //        return false;
                //    }
                //}
                //#endregion

                foreach (DataRow drBoxID in drBoxIDS)
                {
                    string strStorageLocat = drBoxID["LOCAT"].ToString();
                    int Alqty = int.Parse(drBoxID["MENGE"].ToString());//待处理数量
                    int Otqty = 0;

                    #region 判断已处理数量
                    if (sum >= Alqty)
                    {
                        drEcItem["MENGEC"] = int.Parse(drEcItem["MENGEC"].ToString()) + Alqty;
                        drBoxID["OTQTY"] = int.Parse(drBoxID["OTQTY"].ToString()) + Alqty;
                        drBoxID["MENGE"] = 0;
                        drBoxID["LGORT"] = drEcItem["LGORT"].ToString();
                        if (!drBoxID["REMAK"].ToString().Contains(drEcItem["PNUM"].ToString()))
                        {
                            drBoxID["REMAK"] = ";" + drEcItem["PNUM"].ToString();
                        }
                        sum = sum - Alqty;
                        Otqty = Alqty;//已处理数量
                    }
                    else
                    {
                        drEcItem["MENGEC"] = int.Parse(drEcItem["MENGEC"].ToString()) + sum;
                        drBoxID["OTQTY"] = int.Parse(drBoxID["OTQTY"].ToString()) + sum;
                        drBoxID["MENGE"] = int.Parse(drBoxID["MENGE"].ToString()) - sum;
                        drBoxID["LGORT"] = drEcItem["LGORT"].ToString();
                        if (!drBoxID["REMAK"].ToString().Contains(drEcItem["PNUM"].ToString()))
                        {
                            drBoxID["REMAK"] = ";" + drEcItem["PNUM"].ToString();
                        }
                        Otqty = sum;//已处理数量
                        sum = 0;
                    }
                    #endregion

                    #region EC单储位修改
                    if (string.IsNullOrEmpty(drEcItem["LOCAT"].ToString()))
                    {
                        drEcItem["LOCAT"] = strStorageLocat;
                    }
                    else if (!drEcItem["LOCAT"].ToString().Contains(strStorageLocat))
                    {
                        drEcItem["LOCAT"] = drEcItem["LOCAT"] + ";" + strStorageLocat;
                    }
                    #endregion

                    #region 若已存在该储位该料号，该EC单，则并入相同入库数据数量,否则重新增加一条入库数据
                    DataRow[] drM = dtStorage.Select("LGORT='" + drEcItem["LGORT"].ToString() + "' AND PNUM='" + drEcItem["PNUM"].ToString() + "' AND MATNR='" + drEcItem["MATNR"].ToString() + "'  AND LOCAT='" + strStorageLocat + "' ");
                    if (drM.Length > 0)
                    {
                        DataRow drExist = drM[0];
                        int CombineMenge = int.Parse(drExist["MENGE"].ToString());
                        drExist["MENGE"] = CombineMenge + Otqty;
                        drExist["ALQTY"] = CombineMenge + Otqty;
                    }
                    else
                    {
                        DataRow drStorage = dtStorage.NewRow();
                        drStorage["MANDT"] = Mandt;
                        drStorage["COMCD"] = Comcd;
                        drStorage["WERKS"] = strWerks;
                        drStorage["LGORT"] = drEcItem["LGORT"].ToString();
                        drStorage["LOCAT"] = strStorageLocat;
                        drStorage["PNUM"] = drEcItem["PNUM"].ToString();
                        drStorage["MBLNR"] = "";
                        drStorage["INSMK"] = "0";
                        drStorage["MATNR"] = drEcItem["MATNR"].ToString();
                        drStorage["DACOD"] = drBoxID["DACOD"].ToString();
                        drStorage["LIFNR"] = drBoxID["LIFNR"].ToString();
                        drStorage["LOCOD"] = drBoxID["LOCOD"].ToString();
                        drStorage["MENGE"] = Otqty;
                        drStorage["ALQTY"] = Otqty;
                        drStorage["VEDAT"] = drBoxID["VEDAT"].ToString();
                        drStorage["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                        drStorage["RMAK1"] = "StorageInEC";
                        dtStorage.Rows.Add(drStorage);
                    }
                    if (sum == 0)
                    {
                        break;
                    }
                    #endregion
                }

            }
            #endregion
            return true;
        }
        #endregion


        #endregion

        #region rediobutton

        private void rdBtnQuery_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (rdBtnQuery.Checked)
            {
                btnConfirm.Enabled = false;
                btnSave.Enabled = false;
                btOnlyQwms.Enabled = true;
               
                //清除记录只开通9100 9900权限
                if (strComcd == "9900" || strComcd == "9100")
                {
                    btnClearrecord.Enabled = true;
                }
                else
                {
                    btnClearrecord.Enabled = false;
                }
                strStatus = "";
                DataSet();
            }
        }

        private void rdBtnSolve_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (rdBtnSolve.Checked)
            {
                btnConfirm.Enabled = true;
                btnSave.Enabled = false;
                btOnlyQwms.Enabled = false;
               
                if (strComcd == "9900" || strComcd == "9100")
                {
                    btnClearrecord.Enabled = true;
                }
                else
                {
                    btnClearrecord.Enabled = false;
                }
                strStatus = "T";
                DataSet();
            }
        }

        private void DataSet()
        {
            strEC = string.Empty;
            strBoxid = string.Empty;
            dtECHeadSource.Rows.Clear();
            dtECItemSource.Rows.Clear();
            dtBoxID.Rows.Clear();
            ShowECHeadDataGrid();
            getShowEcItem();
            getShowBox(dtBoxID);
        }
        #endregion

        #region ShowStorageInData
        public void ShowStorageInData()
        {
            if (dtECHeadSource.Columns.Count == 0)
            {
                //WERKS,PNUM,LIFNR,BKTXT,CASE CLRTYP WHEN 'A' THEN N'逐票报关' ELSE N'汇总报关'END AS CLRNAM,CUSTID
                dtECHeadSource.Columns.Add("WERKS");
                dtECHeadSource.Columns.Add("PNUM");
                dtECHeadSource.Columns.Add("LIFNR");
                dtECHeadSource.Columns.Add("BKTXT");
                dtECHeadSource.Columns.Add("CLRNAM");
                dtECHeadSource.Columns.Add("CUSTID");
                dtECHeadSource.Columns.Add("STATUS");
                dtECHeadSource.Columns.Add("BOXID");
            }
            if (dtECItemSource.Columns.Count == 0)
            {
                // select  PNUM,PITEM,MATNR,CHARG1,MENGE,LGORT,KOSTL,AEC,CHECKSTATS
                dtECItemSource.Columns.Add("PNUM");
                dtECItemSource.Columns.Add("PITEM");
                dtECItemSource.Columns.Add("MATNR");
                dtECItemSource.Columns.Add("LOCAT");
                dtECItemSource.Columns.Add("LIFNR");
                dtECItemSource.Columns.Add("CHARG1");
                dtECItemSource.Columns.Add("MENGE", typeof(int));
                dtECItemSource.Columns.Add("MENGEC", typeof(int));
                dtECItemSource.Columns.Add("LGORT");
                dtECItemSource.Columns.Add("KOSTL");
                dtECItemSource.Columns.Add("AEC");
                dtECItemSource.Columns.Add("CHECKSTATS");
                dtECItemSource.Columns.Add("MATCH");
            }
            if (dtBoxID.Columns.Count == 0)
            {
                dtBoxID.Columns.Add("WERKS");
                dtBoxID.Columns.Add("LGORT");
                dtBoxID.Columns.Add("LOCAT");
                dtBoxID.Columns.Add("MATNR");
                dtBoxID.Columns.Add("DACOD");
                dtBoxID.Columns.Add("LIFNR");
                dtBoxID.Columns.Add("LOCOD");
                dtBoxID.Columns.Add("MENGE", typeof(int));
                dtBoxID.Columns.Add("OTQTY", typeof(int));
                dtBoxID.Columns.Add("VEDAT");
                dtBoxID.Columns.Add("INDAT");
                dtBoxID.Columns.Add("REMAK");
            }
            if (dtStorage.Columns.Count == 0)
            {
                dtStorage.Columns.Add("MANDT");
                dtStorage.Columns.Add("COMCD");
                dtStorage.Columns.Add("WERKS");
                dtStorage.Columns.Add("LGORT");
                dtStorage.Columns.Add("PNUM");
                dtStorage.Columns.Add("MBLNR");
                dtStorage.Columns.Add("MATNR");
                dtStorage.Columns.Add("CHARG");
                dtStorage.Columns.Add("INSMK");
                dtStorage.Columns.Add("LOCAT");
                dtStorage.Columns.Add("MENGE", typeof(int));
                dtStorage.Columns.Add("ALQTY", typeof(int));
                dtStorage.Columns.Add("LIFNR");
                dtStorage.Columns.Add("DACOD");
                dtStorage.Columns.Add("LOCOD");
                dtStorage.Columns.Add("VEDAT");
                dtStorage.Columns.Add("INDAT");
                dtStorage.Columns.Add("RMAK1");
                dtStorage.Columns.Add("MRGID");
                dtStorage.Columns.Add("ARBPL");
                dtStorage.Columns.Add("KOSTL");
            }
        }
        #endregion

        private void dgvECHead_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvECHead.IsCurrentCellDirty)
            {
                dgvECHead.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvECHead_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.RowIndex != -1)
            {
                strEC = dgvECHead.Rows[e.RowIndex].Cells["PNUM"].Value.ToString().ToUpper();
                if (string.IsNullOrEmpty(strEC))
                    return;
                DataTable dtECHead = objStorageIn.GetECHead(string.Empty, strEC, string.Empty, string.Empty, string.Empty, strStatus, string.Empty);
                strBoxid = dtECHead.Rows[0]["BOXID"].ToString();
                if (dgvECHead.Rows[e.RowIndex].Cells["Select"].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    DataTable dtECItem = objStorageIn.GetECItem(strEC);
                    //DataTable dtLotCode = objStorageIn.GetECTBoxid(strBoxid,strStatus);
                    //dtECItemSource.Merge(dtECItem);
                    //dtBoxID.Merge(dtLotCode);
                    dtECItemSource.Merge(dtECItem);
                    DataTable dtLotCode = new DataTable();
                    if (!string.IsNullOrEmpty(dtECItem.Rows[0]["CHARG1"].ToString()) && objPlantData.CheckCHARGLGORT(Werks))
                    {
                        dtLotCode = objStorageIn.GetECTBoxidPCB(strBoxid, strStatus);
                    }
                    else
                    {
                        dtLotCode = objStorageIn.GetECTBoxid(strBoxid, strStatus);
                    }
                    if (dtBoxID.Rows.Count > 0)
                    {
                        List<string> lsBox = (from d in dtBoxID.AsEnumerable() select d.Field<string>("BOXID")).Distinct().ToList();
                        if (!lsBox.Contains(strBoxid))
                        {
                            dtBoxID.Merge(dtLotCode);
                        }
                    }
                    else
                        dtBoxID.Merge(dtLotCode);
                }
                else
                {
                    DataGridDelEC();
                    strBoxid = string.Empty;
                    strEC = string.Empty;
                }
                getShowEcItem();
                getShowBox(dtBoxID);
                ShowConfirmData();
            }
        }

        public void ShowConfirmData()
        {
            stsWarning.Text = string.Empty;
            List<string> lsMatnr = new List<string>();
            #region 比对EC单数据和刷入数据
            dtBoxID = objStorageIn.GetECTBoxid(strBoxid, strStatus);
            if (dtBoxID.Rows.Count > 0 && dtECItemSource.Rows.Count > 0)
            {
                DataTable dtECCombine = CombineDataTableByMatnr(dtECItemSource);
                DataTable dtMatnrCombine = CombineDataTableByMatnr(dtBoxID);
                foreach (DataRow dr in dtECCombine.Rows)
                {
                    DataRow[] drSelect = dtMatnrCombine.Select(" MATNR='" + dr["MATNR"].ToString() + "' AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                    if (drSelect.Length > 0)
                    {
                        if (int.Parse(dr["MENGE"].ToString()) != int.Parse(drSelect[0]["MENGE"].ToString()))
                        {
                            lsMatnr.Add(dr["MATNR"].ToString());
                        }
                    }
                }
            }
            #endregion
            if (lsMatnr.Count == 0)
                return;
            else
                ShowErrorMatnr(lsMatnr);
        }

        public void ShowErrorMatnr(List<string> lsMatnr)
        {
            #region EC单
            foreach (string strMatnr in lsMatnr)
            {
                for (int j = 0; j < dtECItemSource.Rows.Count; j++)
                {
                    if (dgvECItem.Rows[j].Cells["MATNR"].Value.Equals(strMatnr))
                    {
                        dgvECItem.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            #endregion
            #region BOXID信息
            foreach (string strMatnr in lsMatnr)
            {
                for (int i = 0; i < dtBoxID.Rows.Count; i++)
                {
                    if (dgvBox.Rows[i].Cells["MATNR"].Value.Equals(strMatnr))
                    {
                        dgvBox.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            #endregion
        }

        private void DataGridDelEC()
        {
            DataRow[] deleteItem = dtECItemSource.Select("PNUM='" + strEC + "'");
            if (deleteItem.Length > 0)
            {
                foreach (DataRow itemrow in deleteItem)
                {
                    dtECItemSource.Rows.Remove(itemrow);
                }
            }
            DataRow[] deleteBox = dtBoxID.Select("BOXID='" + strBoxid + "'");
            if (deleteBox.Length > 0)
            {
                foreach (DataRow boxrow in deleteBox)
                {
                    dtBoxID.Rows.Remove(boxrow);
                }
            }
        }
        private void dgvBox_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.RowIndex != -1)
            {
                int Alqty = int.Parse(dgvBox.Rows[e.RowIndex].Cells["MENGE"].Value.ToString());
                string strLocat = dgvBox.Rows[e.RowIndex].Cells["LOCAT"].Value.ToString();
                string strMatnr = dgvBox.Rows[e.RowIndex].Cells["MATNR"].Value.ToString();
                string strBoxItem = dgvBox.Rows[e.RowIndex].Cells["BOXITEM"].Value.ToString();
                string strLifnr = dgvBox.Rows[e.RowIndex].Cells["LIFNR"].Value.ToString();
                DataRow[] dr = dtECItemSource.Select("MATNR='" + strMatnr + "'");
                if (dr[0]["CHECKSTATS"].Equals("Y") && (!strLocat.ToUpper().Substring(0, 2).Equals("DY")))
                {
                    MessageBox.Show("该料号为待验材料，请使用待验储位");
                    return;
                }
                else
                {
                    if (objStorageIn.updateTMatbox(strBoxid, strBoxItem, strLocat, strMatnr, Alqty, strLifnr))
                    {
                        stsWarning.Text = "更新成功";
                        if (!string.IsNullOrEmpty(dr[0]["CHARG1"].ToString()) && objPlantData.CheckCHARGLGORT(Werks))
                        {
                            getShowBox(objStorageIn.GetECTBoxidPCB(strBoxid, strStatus));

                        }
                        else
                        {
                            getShowBox(objStorageIn.GetECTBoxid(strBoxid, strStatus));
                        }
                    }
                }
            }
            return;
        }
        private void dgvBox_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.RowIndex != -1)
            {
                string strUser = objStorageIn.getECUser(string.Empty, strBoxid);
                if(rdBtnQuery.Checked)
                {
                    MessageBox.Show("当前为查询，不可以修改");
                    e.Cancel = true;
                    return;
                }
                if (!strUser.Equals(Usrnm))
                {
                    MessageBox.Show("当前用户非该EC单的作业人，该EC单的作业人是" + strUser);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;

            #region 比对EC单数据和刷入数据
            dtBoxID = objStorageIn.GetECTBoxid(strBoxid, strStatus);
            if (dtBoxID.Rows.Count > 0 && dtECItemSource.Rows.Count > 0)
            {
                DataTable dtECCombine = CombineDataTableByMatnr(dtECItemSource);
                DataTable dtMatnrCombine = CombineDataTableByMatnr(dtBoxID);
                List<string> lsECError = new List<string>();
                List<string> lsMatnr = new List<string>();
                foreach (DataRow dr in dtECCombine.Rows)
                {
                    DataRow[] drSelect = dtMatnrCombine.Select(" MATNR='" + dr["MATNR"].ToString() + "' AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                    if (drSelect.Length > 0)
                    {
                        if (int.Parse(dr["MENGE"].ToString()) != int.Parse(drSelect[0]["MENGE"].ToString()))
                        {
                            lsMatnr.Add(dr["MATNR"].ToString());
                            foreach (DataRow drEcItem in dtECItemSource.Rows)
                            {
                                if (drEcItem["MATNR"].Equals(dr["MATNR"].ToString()) && dr["LIFNR"].Equals(drEcItem["LIFNR"].ToString()) && (!lsECError.Contains(drEcItem["PNUM"].ToString())))
                                {
                                    lsECError.Add(drEcItem["PNUM"].ToString());
                                }
                            }
                            MessageBox.Show(dr["MATNR"].ToString() + " " + dr["LIFNR"].ToString() + "EC单料号与BOXID数量不一致，请确认！");
                            ShowErrorMatnr(lsMatnr);
                            btnSave.Enabled = false;
                            return;
                        }
                    }
                }
            }
            #endregion

            //matchBox();
            if (!matchBox())
            {
                objLogData.AddQWMSLOG(strBoxid, "BOXID", "Confirm", "问题单处理Match不成功!", "N", Usrnm);
                return;
            }

            #region dtLotCode数据回退到MATBOX表中
            if (!objStorageIn.backMatbox(dtBoxID))
            {
                if (!objStorageIn.DeleteMatbox(strBoxid))
                {
                    stsWarning.Text = "保存异常，请重新确认！！！";
                }
                return;
            }
            #endregion 


            #region EC单数据存放到数据库
            DataRow[] drECItem = dtECItemSource.Select("PNUM='" + strEC + "'");
            if (objStorageIn.updateECItem(drECItem))
            {
                stsWarning.Text = "EC单信息保存成功";
            }
            else
            {
                stsWarning.Text = "EC单信息未保存成功，请重新确认";
                return;
            }
            #endregion


            #region 将入库数据dtStorage存放在EC_INSTOCK表中
            if (dtStorage.Rows.Count > 0)
            {
                if (!objStorageIn.ECinStock(dtStorage))
                {
                    stsWarning.Text = "入库数据未保存成功，请重新确认";
                    return;
                }
            }
            #endregion

            getShowEcItem();
            btnConfirm.Enabled = false;
            btnSave.Enabled = true;
            txtEC.Enabled = false;
        }

        private void dgvECItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.RowIndex != -1)
            {
                string strCLgort = dgvECItem.Rows[e.RowIndex].Cells["LGORT"].Value.ToString();
                string strPitem = dgvECItem.Rows[e.RowIndex].Cells["PITEM"].Value.ToString();
                DataRow[] dr = dtECItemSource.Select("PITEM='" + strPitem + "'");
                dr[0]["LGORT"] = strCLgort;
                if (objStorageIn.updateTECItem(dtECItemSource))
                {
                    stsWarning.Text = "EC单信息保存成功";
                    getShowEcItem();
                }
            }
        }

        private void btOnlyQwms_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            btnConfirm.Enabled = false;
            btnSave.Enabled = false;
            btnClearrecord.Enabled = false;
            string strMessage = string.Empty;

            #region 仅入QWMS
            foreach (DataGridViewRow drEcHead in dgvECHead.Rows)
            {    
                if (drEcHead.Cells["Select"].EditedFormattedValue.ToString().ToUpper().Equals("TRUE"))
                {
                    string strCurrentEC = drEcHead.Cells["PNUM"].Value.ToString();
                    DataTable dtCheckStorage = objStorageIn.GetEcInStock(strCurrentEC);
                    if (dtCheckStorage.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dtCheckStorage.Rows[0]["MBLNR"].ToString())) //EC_INSTOCK单据为空
                        {
                            #region 单据为空，先更新单据信息，再入库
                            string strMblnr = objStorageIn.GetECDocument(Comcd, strWerks, strCurrentEC); //获取SAP扣账单据号
                            if (!string.IsNullOrEmpty(strMblnr))
                            {
                                #region 更新状态
                                if (objStorageIn.UpdateECDocument(strCurrentEC, strMblnr)) 
                                {
                                    objLogData.AddQWMSLOG(strCurrentEC, "EC", "Save", "仅入QWMS，更新EC单扣账信息", "Y", strMblnr);
                                    strMessage += StockInEC(strCurrentEC);
                                }
                                #endregion
                            }
                            else
                            {
                                strMessage += "\r\n" + strCurrentEC + "：无扣账信息，请确认SAP是否已扣账（SAP同步扣账数据时间间隔为2分钟）！";
                                continue;
                            }
                            #endregion
                        }
                        else //直接入库
                        {
                            objStorageIn.updateECstatus(strCurrentEC, "Y", string.Empty);
                            strMessage += StockInEC(strCurrentEC);
                        }
                    }
                    else
                    {
                        strMessage +=  "\r\n" + strCurrentEC + "：无入库数据！";
                    }
                }
            }
            if (!string.IsNullOrEmpty(strMessage))
                MessageBox.Show(strMessage);
            #endregion

        }

        #region 清除扫描记录

        private void btnClearrecord_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            btnConfirm.Enabled = false;
            btnSave.Enabled = false;
            string strMessage = string.Empty;

            #region 清除扫描记录 9100、9900-未过账的可清空 已过帐的不能清空
            foreach (DataGridViewRow drEcHead in dgvECHead.Rows)
            {
                if (drEcHead.Cells["Select"].EditedFormattedValue.ToString().ToUpper().Equals("TRUE"))
                {
                    string strCurrentEC = drEcHead.Cells["PNUM"].Value.ToString();
                    //string strCurrentBID = drEcHead.Cells["BOXID"].Value.ToString();
                    //查看是否过账 未过账的数据清空时候 已经解绑
                    string strMblnr = objStorageIn.GetECDocument(Comcd, strWerks, strCurrentEC);
                    string strCurrentBID = objStorageIn.GetBOXID(strWerks, strCurrentEC);
                    //判断是否过账
                    if (string.IsNullOrEmpty(strMblnr))
                    {
                        //清空四个表
                        DataTable dtCheckStorage = objStorageIn.GetEcInStock(strCurrentEC);
                        if (!string.IsNullOrEmpty(drEcHead.Cells["PNUM"].Value.ToString()))//EC_INSTOCK单据不为空
                        {
                            //清空数据
                            objStorageIn.DeleteEcInStock(strCurrentEC);
                        }
                        //DataTable dtECItem = objStorageIn.GetEcItem(strCurrentEC);
                        if (!string.IsNullOrEmpty(drEcHead.Cells["PNUM"].Value.ToString()))//EC_ITEM单据不为空
                        {
                            //清空数据
                            objStorageIn.DeleteEcItem(strCurrentEC);
                        }
                        if (!string.IsNullOrEmpty(drEcHead.Cells["PNUM"].Value.ToString()))//MATBOX单据不为空
                        {
                            //清空数据
                            string strRemak = ";" + strCurrentEC;
                            objStorageIn.DeleteMatBox(strRemak, strCurrentBID);
                        }
                        if (!string.IsNullOrEmpty(drEcHead.Cells["PNUM"].Value.ToString()))//EC_HEAD单据不为空
                        {
                            //清空数据
                            objStorageIn.DeleteECHead(strCurrentEC, strCurrentBID);
                        }
                        MessageBox.Show("删除成功，点击Query重新查询");
                    }
                    else
                    {
                        strMessage += "\r\n" + strCurrentEC + "：已经过账，无法清除扫描记录！";
                    }
                }
            }
            if (!string.IsNullOrEmpty(strMessage))
                MessageBox.Show(strMessage);
            #endregion
        }
        #endregion


        private string StockInEC(string strCurrentEC)
        {
            string strMessage = string.Empty;
            DataTable dtCurrentStorage = objStorageIn.GetEcInStock(strCurrentEC);
            var Mblnrs = dtCurrentStorage.AsEnumerable().Select(row => row.Field<string>("MBLNR")).Distinct().ToList();
            if (string.IsNullOrEmpty(Mblnrs[0].ToString()))
            {
                string strCurrentMblnr = objStorageIn.GetEcInStockMblnr(strCurrentEC);
                for (int i = 0; i < dtCurrentStorage.Rows.Count; i++)
                {
                    dtCurrentStorage.Rows[i]["MBLNR"] = strCurrentMblnr;
                }
            }
            if (objStorageIn.StorageInWHEC(dtCurrentStorage))
            {
                if (objStorageIn.UpdateEcInStock(strCurrentEC))
                {
                    objLogData.AddQWMSLOG(strCurrentEC, "EC", "Save", "仅入QWMS，入库成功", "Y", string.Empty);
                    strMessage += "\r\n" + strCurrentEC + "：入库QWMS成功！";
                }
            }
            else
            {
                strMessage += "\r\n" + strCurrentEC + "：入库QWMS失败！";
            }
            return strMessage;
        }
    }
}
