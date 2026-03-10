using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;
using System.Text.RegularExpressions;
using System.Media;
namespace QWMS
{
    public partial class StorageIn_EC_DateCode : Form
    {
        #region DataMember

        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;
        private string strLocat = string.Empty;
        private string strType = string.Empty;
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtECSource = new DataTable();
        private DataTable dtStorageLocation = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtMatnr = new DataTable();
        QCI.QWMS.PlantData objPlantData = null;
        QCI.QWMS.StorageData objStorageData = null;

        
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
            get { return strLocat; }
            set { strLocat = value; }
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


        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
            }
        }

        #endregion

        #region Constructor

        public StorageIn_EC_DateCode(UserInfo varUserData, string strProgid)
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
                objStorageData = new StorageData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowStatusData();
                    ShowStorageInData();
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
        #endregion

        #region ShowStatusData()
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
            this.stsWarning.Width = 1000;
        }
        #endregion

        #region txtEC_KeyDown
        private void txtEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                DataTable dtstatus = new DataTable();
                dtstatus = StorageIn.GetPacingStatus(txtEC.Text.Trim());
                //if (dtstatus.Rows.Count > 0)
                //{
                //    if (dtstatus.Rows[0]["status"].ToString().Trim() == "Y")
                //    {
                //        MessageBox.Show("已经完成入库，不能重新作业！");
                //        txtEC.Text = "";
                //        return;
                //    }
                //}
                if (dtECSource.Rows.Count > 0)
                {
                    dtECSource.Merge(StorageIn.GetECInfo(cmbWerks.Text.ToString(), txtEC.Text.Trim()));
                }
                else
                {
                    dtECSource = StorageIn.GetECInfo(cmbWerks.Text.ToString(), txtEC.Text.Trim());
                }

                ShowECDataGrid();
                if (dtECSource.Rows.Count > 0)
                {
                    //if (dtECSource.Rows[0]["lgort"].ToString() != "")
                    //{
                    //    cmbLgort.Text = dtECSource.Rows[0]["lgort"].ToString();
                    //}

                    if (cmbLgort.Text != "")
                    {
                        foreach (DataRow dr in dtECSource.Rows)
                        {
                            dr["lgort"] = cmbLgort.Text;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "请输入仓别，谢谢！！";
                        return;
                    }

                    lblCustomno.Text = dtECSource.Rows[0]["BKTXT1"].ToString();
                    lblType.Text = dtECSource.Rows[0]["CLRNAM"].ToString();
                }
                else
                {
                    MessageBox.Show("EC单不存在，请确认！");
                    return;
                }
            }
          //  txtEC.Text = "";
        }

        #endregion

        #region ShowECDataGrid
        private void ShowECDataGrid()
        {
            dgvEC.AutoGenerateColumns = false;
            dgvEC.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.HeaderText = "EC单号";
                dgvcPNUM.Width = 130;
                dgvcPNUM.ReadOnly = true;
                dgvEC.Columns.Add(dgvcPNUM);

                DataGridViewTextBoxColumn dgvcPITEM = new DataGridViewTextBoxColumn();
                dgvcPITEM.DataPropertyName = "PITEM";
                dgvcPITEM.HeaderText = "序号";
                dgvcPITEM.Width = 130;
                dgvcPITEM.ReadOnly = true;
                dgvEC.Columns.Add(dgvcPITEM);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvEC.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG1 = new DataGridViewTextBoxColumn();
                dgvcCHARG1.DataPropertyName = "CHARG1";
                dgvcCHARG1.HeaderText = "版本";
                dgvcCHARG1.Width = 90;
                dgvcCHARG1.ReadOnly = true;
                dgvEC.Columns.Add(dgvcCHARG1);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "VendorCode";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                dgvEC.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                dgvEC.Columns.Add(dgvcMENGE);

                dgvEC.DataSource = dtECSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowECDataGrid()");
            }
        }
        #endregion

        #region ShowMatnrDataGrid

        private void ShowMatnrDataGrid()
        {
            dgvLotCode.AutoGenerateColumns = false;
            dgvLotCode.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvLocat = new DataGridViewTextBoxColumn();
                dgvLocat.DataPropertyName = "LOCAT";
                dgvLocat.HeaderText = "储位";
                dgvLocat.Width = 130;
                dgvLocat.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvLocat);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "VendorCode";
                dgvcLifnr.Width = 100;
                dgvcLifnr.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "MENGE";
                dgvcDacod.HeaderText = "数量";
                dgvcDacod.Width = 90;
                dgvcDacod.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcLOCOD = new DataGridViewTextBoxColumn();
                dgvcLOCOD.DataPropertyName = "DACOD";
                dgvcLOCOD.HeaderText = "DateCode1";
                dgvcLOCOD.Width = 90;
                dgvcLOCOD.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvcLOCOD);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "DateCode2";
                dgvcVedat.Width = 90;
                dgvcVedat.ReadOnly = true;
                dgvLotCode.Columns.Add(dgvcVedat);
                dgvLotCode.DataSource = dtMatnr;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMatnrDataGrid()");
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLocat.Text.Trim() == "")
            {
                MessageBox.Show("储位不能为空！");
                return;
            }
            QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

            #region 比对EC单和lotcode料号数量

            if (dtMatnr.Rows.Count > 0 && dtECSource.Rows.Count > 0)
            {
                DataTable dtECCombine = CombineDataTableByMatnr(dtECSource);
                DataTable dtMatnrCombine = CombineDataTableByMatnr(dtMatnr);
                foreach (DataRow dr in dtECCombine.Rows)
                {
                    DataRow[] drSelect = dtMatnrCombine.Select(" MATNR='" + dr["MATNR"].ToString() + "' AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                    if (Convert.ToInt32(dr["MENGE"].ToString()) != Convert.ToInt32(drSelect[0]["MENGE"].ToString()))
                    {
                        DataRow[] drECInfo = dtECSource.Select(" MATNR='" + dr["MATNR"].ToString() + "' AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                        MessageBox.Show(dr["MATNR"].ToString() + " " + dr["LIFNR"].ToString() + "EC单料号与刷入的LotCode信息不一致，请确认！");
                        SetErrNotice();
                        return;
                    }
                }
                stsWarning.Text = "刷入的EC单信息与LotCode信息一致";
            }
            else
            {
                MessageBox.Show(" 请刷入EC单信息和LotCode信息 ");
                return;
            }

            #endregion

            #region SAP扣账

            DataSet ds = new DataSet();
            DataTable dthead = StorageIn.GetTAB_ZM000(txtEC.Text.Trim(), cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), "", "");
            DataTable dtitem = StorageIn.GetTAB_ZM001(txtEC.Text.Trim(), cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), "","");
            dthead.TableName = "TAB_ZM000";
            dtitem.TableName = "TAB_ZM001";

            if (cmbLgort.Text != "")
            {
                foreach (DataRow dr in dtitem.Rows)
                {
                    dr["lgort"] = cmbLgort.Text;
                }
                dtitem.AcceptChanges();
                ds.Tables.Add(dthead.Copy());
                ds.Tables.Add(dtitem.Copy());
            }
            else
            {
                stsWarning.Text = "请输入仓别，谢谢！！";
                return;
            }

            ArrayList sqlarr=new ArrayList();
            DataSet dsreturn = new DataSet();

            MM.MM_Service objMM = new QWMS.MM.MM_Service();
            dsreturn = objMM.Z_RFC_PACKING_POST(UserData.UserId, txtEC.Text.Trim(),"SDS_PDA","", ds);
         //   dsreturn = ds;//测试

            DataTable dtTAB_ZM025 = dsreturn.Tables["TAB_ZM025"];
            if (dtTAB_ZM025.Rows.Count > 0)
            {
                 if (dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() != "")
                    {
                        StringBuilder strsql=new StringBuilder();
                        strsql.AppendFormat("UPDATE EC_HEAD SET STATUS='Y' WHERE PNUM='{0}'",txtEC.Text.Trim());
                        sqlarr.Add(strsql);
                    }
                for (int i = 0; i < dtTAB_ZM025.Rows.Count; i++)
                {
                    if (dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim() != "")
                    {
                        StringBuilder strsql = new StringBuilder();
                        strsql.AppendFormat("UPDATE EC_ITEM SET WERKS='{0}',LGORT='{1}',SGTXT='{2}',GDREC='{3}',BKTXT='{4}',GJAHR='{5}',BELNR='{6}',BUZEI='{7}',BUDAT='{8}',UNAME='{9}',MESSAGE=N'{10}' WHERE PNUM='{11}' AND PITEM='{12}'",
                            dtTAB_ZM025.Rows[i]["WERKS"].ToString().Trim(),dtTAB_ZM025.Rows[i]["LGORT"].ToString().Trim(),dtTAB_ZM025.Rows[i]["SGTXT"].ToString().Trim(),dtTAB_ZM025.Rows[i]["GDREC"].ToString().Trim(),dtTAB_ZM025.Rows[i]["BKTXT"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["MJAHR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["ZEILE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BUDAT"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["UNAME"].ToString().Trim(),dtTAB_ZM025.Rows[i]["MESSAGE"].ToString().Trim(),dtTAB_ZM025.Rows[i]["PNUM"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["PITEM"].ToString().Trim());
                        sqlarr.Add(strsql);
                    }
                }
            }

            if (sqlarr.Count>0)
            {
                if (StorageIn.updateZM025(sqlarr))
                {
                    MessageBox.Show("扣账成功，扣账编号为：" + dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() + "！");
                    #region  入QWMS
                    if (StorageIn.StorageInWH(dtMatnr))
                    {
                        stsWarning.Text = "入库QWMS成功";
                    }
                    else
                    {
                        stsWarning.Text = "入库QWMS失败";
                    }

                    #endregion
                }
            }
            else
            {
                MessageBox.Show("扣账失败，失败原因为：" + dtTAB_ZM025.Rows[0]["MESSAGE"].ToString().Trim() + "！");
            }
            #endregion
        }
        #endregion

        #region btnDelete_Click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            dtMatnr.Rows.Clear();
            ShowMatnrDataGrid();
        }

        #endregion

        #region btnReset_Click
        private void btnReset_Click(object sender, EventArgs e)
        {
            lblCustomno.Text = string.Empty;
            lblType.Text = string.Empty;
            txtEC.Text = "";
            txtLocat.Text = "";
            dtECSource.Rows.Clear();
            dtMatnr.Rows.Clear();
            dtECSource.Rows.Clear();
            ShowMatnrDataGrid();
            ShowECDataGrid();
            btnSave.Enabled = false;


        }

        #endregion

        #region txtLotCode_KeyDown
        private void txtLotCode_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = "";
            if (e.KeyCode == Keys.Enter)
            {
                strLocat = txtLocat.Text.Trim();
                strWerks = cmbWerks.Text.Trim();
                strLgort = cmbLgort.Text.Trim();
                if (strLocat == "")
                {
                    stsWarning.Text = "请输入储位信息";
                    return;
                }
                else
                {
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    DataTable dtCheckLocat = objPlantData.GetAllLocatData(strWerks, strLgort, strLocat, "");
                    if (dtCheckLocat.Rows.Count == 0)
                    {
                        stsWarning.Text = "该储位不存在，请确认并重新输入";
                        return;
                    }
                }

                if (dtECSource.Rows.Count>0)
                {
                    #region 处理刷入的数据 
                    string  strLotCode = txtLotCode.Text.Trim();
                    string[] str = strLotCode.Split(';');
                   

                    if (str.Length >= 5)
                    {
                        if (CheckECInfo(str[0].ToString().Trim(), str[2].ToString().Trim()))
                        {
                            #region 确认刷入的LotCode信息与EC单信息一致
                            DataRow[] drM = dtMatnr.Select("MATNR='" + str[0].ToString().Trim() + "'  AND LOCAT='"+strLocat+"' ");
                            if (drM.Length > 0)
                            {
                                #region 刷入多笔的时候同一料号不同厂商不能放在同一储位，相同的料号，但是DateCode不一致，不能入库
                                DataRow drExist = drM[0];
                                if (str[2].ToString().Trim() != drExist["LIFNR"].ToString() && strLocat==drExist["LOCAT"].ToString())
                                {
                                    stsWarning.Text = "相同料号，不同VendorCode ,不能放在同一储位，请确认！";
                                    SetErrNotice();
                                    return;
                                }
                                if (str[1].ToString().Trim() != drExist["DACOD"].ToString() && strLocat == drExist["LOCAT"].ToString())
                                {
                                    stsWarning.Text = "相同的料号，但是DateCode不一致，请确认！";
                                    SetErrNotice();
                                    return;
                                }
                                objStorageData = new StorageData(UserData, strWerks, strLgort);
                                //确认储位相同料号是否有相同DateCode
                                if (objStorageData.CheckExistedSameMaterialDC(strLocat, str[0].ToString().Trim(), "G", str[1].ToString().Trim()))
                                {
                                    stsWarning.Text = str[0].ToString().Trim() + " 该料号在该储位已经存在不同DateCode，请确认";
                                    return;
                                }
                                if (objStorageData.CheckStorageInType(strWerks, strLgort, "Diff Vendor Diff Locat"))
                                {
                                    //确认储位相同料号是否有相同DateCode
                                    if (objStorageData.CheckExistedSameMaterialDC(Locat, str[0].ToString().Trim(), "G", str[2].ToString().Trim()))
                                    {
                                        stsWarning.Text = str[0].ToString().Trim() + "  料号在该储位已存在不同的厂商，请确认！！";
                                        return;
                                    }
                                }

                                #endregion
                                int CombineMenge = Convert.ToInt32(drExist["MENGE"]);
                                drExist["MENGE"] = CombineMenge + Convert.ToInt32(str[4].ToString().Trim());
                                drExist["ALQTY"] = CombineMenge + Convert.ToInt32(str[4].ToString().Trim());

                             
                            }

                            else
                            {
                                DataRow drMatnr = dtMatnr.NewRow();
                                drMatnr["MANDT"] = Mandt;
                                drMatnr["COMCD"] = Comcd;
                                drMatnr["WERKS"] = strWerks;
                                drMatnr["LGORT"] = strLgort;
                                drMatnr["LOCAT"] = strLocat;
                                drMatnr["MBLNR"] = txtEC.Text.Trim();
                                drMatnr["INSMK"] = "G";
                                drMatnr["MATNR"] = str[0].ToString().Trim();
                                drMatnr["DACOD"] = str[1].ToString().Trim();
                                drMatnr["LIFNR"] = str[2].ToString().Trim();
                                drMatnr["LOCOD"] = str[3].ToString().Trim();
                                drMatnr["MENGE"] = Convert.ToInt32(str[4].ToString().Trim());
                                drMatnr["ALQTY"] = Convert.ToInt32(str[4].ToString().Trim());
                                drMatnr["VEDAT"] = "";
                                drMatnr["INDAT"] = "";
                                dtMatnr.Rows.Add(drMatnr.ItemArray);

                                #region 处理DateCode转换问题
                                try
                                {
                                    StorageData objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
                                    string DC_After = objStorageData.WHDCR_Query(drMatnr["LIFNR"].ToString(), drMatnr["DACOD"].ToString());
                                    if (DC_After != "")
                                    {
                                        DateTime dtVedat = Convert.ToDateTime(DC_After);
                                        DC_After = dtVedat.ToString("yyyyMMdd");
                                        drMatnr["VEDAT"] = DC_After;
                                    }
                                    else
                                    {
                                        #region 确认是否要转化DateCode Rule
                                        string strTemp = objStorageData.getDCTrans(drMatnr["LIFNR"].ToString(), drMatnr["DACOD"].ToString()).ToString();
                                        if (!string.IsNullOrEmpty(strTemp))
                                        {
                                            //strDC = DateTime.Now.ToString("yyyyMMdd");
                                            DataTable dtNewDateCode = new DataTable();
                                            dtNewDateCode.Columns.Add("LIFNR");
                                            dtNewDateCode.Columns.Add("DC_Before");
                                            dtNewDateCode.Columns.Add("DC_After");

                                            DataRow dr = dtNewDateCode.NewRow();
                                            dr["LIFNR"] = drMatnr["LIFNR"].ToString();
                                            dr["DC_Before"] = drMatnr["DACOD"].ToString();
                                            dr["DC_After"] = strTemp;
                                            dtNewDateCode.Rows.Add(dr.ItemArray);
                                            objStorageData.WHDCR_DML(dtNewDateCode, "NEW", "System");
                                            drMatnr["VEDAT"] = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                        }
                                        else
                                        {
                                            MessageBox.Show("无D/C转换信息找D/C管理人员处理!");
                                            return;
                                        }
                                        #endregion


                                        #region old 确认是否要转化DateCode Rule
                                        //DialogResult result = new DialogResult();
                                        //result = MessageBox.Show("该DateCode未维护" + "\n" + "是否要新增DateCode转换", "QWMS-DateCode转换", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                        //if (result == DialogResult.Yes)
                                        //{
                                        //    Mange_DateCodeRule_Pop objMange_DateCodeRule_Pop = new Mange_DateCodeRule_Pop(UserData, Progid, "NEW", drMatnr["LIFNR"].ToString(), drMatnr["DACOD"].ToString());
                                        //    objMange_DateCodeRule_Pop.MdiParent = this.ParentForm;
                                        //    objMange_DateCodeRule_Pop.Show();
                                        //}
                                        //stsWarning.Text = "(Datecode rule not found!!)";
                                        //SetErrNotice();
                                        //return;
                                        #endregion
                                    }
                                    ShowMatnrDataGrid();
                                }
                                catch (Exception e1)
                                {
                                    MessageBox.Show("DateCode转化错误！" + e1.ToString());
                                    SetErrNotice();
                                    return;
                                }
                                #endregion


                            }
                            
                            #endregion
                        }
                        else
                        {
                            SetErrNotice();
                        }
                    }
                    else
                    {
                        if (str.Length == 1)
                        {
                            if (str[0].Length != 11)
                            {
                                stsWarning.Text = "请先扫描料号信息！";
                                return;
                            }
                        }
                        txtLotCode.Text = txtLotCode.Text.Trim().ToString() + ";";
                        txtLotCode.Focus();
                        txtLotCode.Select(txtLotCode.Text.Length, 1);//将光标停在文本最后
                    }
                    #endregion
                }
                else
                {
                    stsWarning.Text = "请刷入EC单号";
                    SetErrNotice();
                }
                txtLotCode.Text = "";
                txtLotCode.Focus();
            }
        }

        #endregion

        #region SetErrNotice
        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
            txtLotCode.Focus();
            txtLotCode.Text = "";
        }

        #endregion        

        #region ShowStorageInData
        public void ShowStorageInData()
        {
            dtMatnr = new DataTable();
            if (dtMatnr.Columns.Count== 0)
            {
                dtMatnr.Columns.Add("MANDT");
                dtMatnr.Columns.Add("COMCD");
                dtMatnr.Columns.Add("WERKS");
                dtMatnr.Columns.Add("LGORT");
                dtMatnr.Columns.Add("LOCAT");
                dtMatnr.Columns.Add("MBLNR");
                dtMatnr.Columns.Add("MATNR");
                dtMatnr.Columns.Add("CHARG");
                dtMatnr.Columns.Add("INSMK");
                dtMatnr.Columns.Add("DACOD");
                dtMatnr.Columns.Add("LIFNR");
                dtMatnr.Columns.Add("LOCOD");
                dtMatnr.Columns.Add("MENGE",typeof(int));
                dtMatnr.Columns.Add("VEDAT");
                dtMatnr.Columns.Add("INDAT");
                dtMatnr.Columns.Add("RMAK1");
                dtMatnr.Columns.Add("MRGID");
                dtMatnr.Columns.Add("ARBPL");
                dtMatnr.Columns.Add("KOSTL");
                dtMatnr.Columns.Add("ALQTY", typeof(int));
                dtMatnr.Columns.Add("EBELN");
                dtMatnr.Columns.Add("OMBLNR");
                dtMatnr.Columns.Add("TRNTP");
            }
       
        }

        #endregion 

        #region CheckECInfo
        /// <summary>
        /// 确认刷入的LotCode信息与EC信息是否一致
        /// </summary>
        /// <param name="strMatnr"></param>
        /// <param name="strLifnr"></param>
        /// <returns></returns>
        private bool CheckECInfo(string strMatnr, string strLifnr)
        {
            bool bol = false;
            DataRow[] drSelect = dtECSource.Select(" MATNR='" + strMatnr + "' AND LIFNR='" + strLifnr + "' ");
            if (drSelect.Length <= 0)
            {
                stsWarning.Text = "刷入的LotCode与刷入的EC单信息不一致";
                SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                sp.Play();
            }
            else
            {
                bol = true;
            }
            txtLotCode.Text = string.Empty;
            txtLotCode.Focus();
            return bol;
        }

        #endregion
 
        private DataTable CombineDataTableByMatnr(DataTable dtData)
        {
            DataTable dtResult = new DataTable();
            try
            {
                dtResult.Columns.Add("MATNR");
                dtResult.Columns.Add("LIFNR");
                dtResult.Columns.Add("MENGE");

                var query = from row in dtData.AsEnumerable()
                            group row by row.Field<string>("MATNR") into m
                            select new
                            {
                                MATNR = m.Key,
                                LIFNR = m.FirstOrDefault().Field<string>("LIFNR"),
                                MENGE = m.Sum(n => n.Field<int>("MENGE"))
                            };
                foreach (var item in query)
                {
                    DataRow drResult = dtResult.NewRow();
                    drResult["MATNR"] = item.MATNR;
                    drResult["LIFNR"] = item.LIFNR;
                    drResult["MENGE"] = item.MENGE;
                    dtResult.Rows.Add(drResult.ItemArray);
                    // Console.WriteLine($"{item.name},{item.sex},{item.score}");
                }
            }
            catch (Exception e)
            {
                stsWarning.Text =dtData.TableName+ "Table转化异常";
            }
          
            return dtResult;
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDdlLgort();
        }

        #region txtLocat
        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Werks = cmbWerks.Text.Trim();
                Lgort = cmbLgort.Text.Trim();

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "请输入厂区仓别";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, strType);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    //this.btnConfirm.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        
        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    Locat = txtLocat.Text.Trim();
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    DataTable dtLocat = new DataTable();

                    #region 检查输入的储位是否允许入库

                    dtLocat = objPlantData.GetAllLocatData(strWerks, strLgort, Locat,strType, "", "");
              
                    if (dtLocat.Rows.Count < 0)
                    {
                        stsWarning.Text = "该储位不存在或者状态不对，请确认！";
                        return;
                    }
                    #endregion

                   // this.btnConfirm.Enabled = true;
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\Fail.wav");
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion 

        #region Intype

        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "NEW";
            Type = strType;
            this.txtEC.Focus();
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "ADD";
            Type = strType;
            this.txtEC.Focus();
        }

        #endregion 

  

  




    }
}
