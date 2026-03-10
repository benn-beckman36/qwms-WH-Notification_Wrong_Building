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
using System.Data.SqlClient;
using QWMS.Properties;
using System.Configuration;

namespace QWMS
{
    public partial class Mange_Reflesh_Stock : Form
    {
        
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;

        DataTable dtctrl = new DataTable();
        DataTable dtloct = new DataTable();
        DataTable dtMatnr = new DataTable();
        DataTable dtPN = new DataTable();
        //DataTable dttmpWHBOX_IN;
        private DataTable dttmpWHBOX_IN = new DataTable();
        private DataTable dtData = new DataTable();
        DataTable dtWHITM = new DataTable();
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorageLocation = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtOutLocation = new DataTable();
        private PlantData objPlantData;
        private StorageIn objStorageIn;
        private int ScanAlqty = 0;

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
        #endregion

        public Mange_Reflesh_Stock(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                objPlantData = new PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    //ShowDdlLocat();
                    createData();
                    createWhbox_IN();

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
                //stsWarning.Text = string.Empty;
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

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "TYPE";
                dgvcType.HeaderText = "Type";
                dgvcType.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcboxid = new DataGridViewTextBoxColumn();
                dgvcboxid.DataPropertyName = "BOXID";
                dgvcboxid.HeaderText = "BoxID";
                dgvcboxid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcboxid);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "SerNo";
                dgvcSerno.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Menge";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                dgvData.DataSource = dtData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void createData()
        {
            dtData.Columns.Add("TYPE");
            dtData.Columns.Add("BOXID");
            dtData.Columns.Add("SERNO");
            dtData.Columns.Add("MENGE");
        }

        private void createWhbox_IN()
        {
            dttmpWHBOX_IN.Columns.Add("MANDT");
            dttmpWHBOX_IN.Columns.Add("COMCD");
            dttmpWHBOX_IN.Columns.Add("WERKS");
            dttmpWHBOX_IN.Columns.Add("LGORT");
            dttmpWHBOX_IN.Columns.Add("LOCAT");
            dttmpWHBOX_IN.Columns.Add("MBLNR");
            dttmpWHBOX_IN.Columns.Add("BOXID");
            dttmpWHBOX_IN.Columns.Add("INSMK");
            dttmpWHBOX_IN.Columns.Add("CHARG");
            dttmpWHBOX_IN.Columns.Add("MATNR");
            dttmpWHBOX_IN.Columns.Add("SERNO");
            dttmpWHBOX_IN.Columns.Add("LOADID");
            dttmpWHBOX_IN.Columns.Add("KDMAT");
            dttmpWHBOX_IN.Columns.Add("CRDAT");
            dttmpWHBOX_IN.Columns.Add("MENGE");
        }


        private void Mange_Reflesh_Stock_Load(object sender, EventArgs e)
        {
            init();
        }

        //private void GetLgortData()
        //{
        //    clsGlobalVar.SetDBConn();
        //    //string strSQL = "SELECT CTRLNM,CTRLC1 FROM WHCTRL WHERE  SOLDTO='QWMS' AND CTRLID ='LGORT' AND CTRLC1 LIKE'TW%' ORDER BY CTRLNM,CTRLC1";
        //    string strSQL = " SELECT DISTINCT  WERKS CTRLNM,LGORT CTRLC1 FROM WHITM WHERE  SUBSTRING(MATNR,1,2) IN ('20','2L') ORDER BY WERKS,LGORT";
        //    dtctrl = clsGlobalVar.GetDataTable(strSQL);
        //}
        //private void GetLocatData()
        //{
        //    clsGlobalVar.SetDBConn();

        //    string strSQL = "SELECT WERKS,LGORT,LOCAT FROM WHHED ";
        //    dtloct = clsGlobalVar.GetDataTable(strSQL);
        //}

        private void GetMatnrData()
        {
            clsGlobalVar.SetDBConn();
            //string strSQL = "SELECT DISTINCT WERKS, LGORT, LOCAT, MATNR, CHARG FROM WHITM WHERE MANDT ='"+ UserData.Client +"' AND SUBSTRING(MATNR,1,4) IN ('20JH','2LJH')";
            string strSQL = "SELECT DISTINCT WERKS, LGORT, LOCAT, MATNR, CHARG FROM WHITM WITH(NOLOCK) WHERE MANDT='" + Mandt + "' AND COMCD='" + Comcd + "' AND WERKS='"+Werks+"' AND LGORT='"+Lgort+"' AND LOCAT='"+txtLocat.Text.ToString().Trim() + "' AND MATNR='"+txtMatnr.Text.ToString().Trim()+"'";
            dtMatnr = clsGlobalVar.GetDataTable(strSQL);
        }
        private void AddQWMSLOG(string MBLNR, string MTYPE, string FLAGE, string OPERATION, string RESULT, string REMARK)
        {
            clsGlobalVar.SetDBConn();
            //string strSQL = "SELECT DISTINCT WERKS, LGORT, LOCAT, MATNR, CHARG FROM WHITM WHERE MANDT ='"+ UserData.Client +"' AND SUBSTRING(MATNR,1,4) IN ('20JH','2LJH')";
            string strSQL = "INSERT INTO QWMS_LOG(MBLNR, MTYPE, OPERATION, RESULT, REMARK, FLAGE, CREATETIME) VALUES('" + MBLNR + "', '" + MTYPE + "', N'" + OPERATION + "', N'" + RESULT + "', N'" + REMARK + "', '" + FLAGE + "', GETDATE())";
            dtMatnr = clsGlobalVar.GetDataTable(strSQL);
        }
        private void GetQWMSQty()
        {
            DataTable dtQty = new DataTable();
            clsGlobalVar.SetDBConn();
            //string strSQL = "SELECT DISTINCT WERKS, LGORT, LOCAT, MATNR, CHARG FROM WHITM WHERE MANDT ='"+ UserData.Client +"' AND SUBSTRING(MATNR,1,4) IN ('20JH','2LJH')";
            string strSQL = "SELECT SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) WHERE MANDT='" + Mandt + "' AND COMCD='" + Comcd + "' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT='" + txtLocat.Text.ToString().Trim() + "' AND MATNR='" + txtMatnr.Text.ToString().Trim() + "' AND CHARG='"+ txtCharg.Text.ToString().Trim() + "'";
            dtQty = clsGlobalVar.GetDataTable(strSQL);
            lblQWNSQty.Text = dtQty.Rows[0]["MENGE"].ToString();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblMsg.Text = "";
            string strQTY = "0";
            DataRow drRow;
            if (e.KeyChar != 13)
            {
                return;
            }
            if (Werks == "" || Lgort == "")
            {
                lblMsg.Text = "请选择厂区以及仓别！";
                txtInput.Text = "";
                return;
            }
            if (txtLocat.Text == "")
            {
                lblMsg.Text = "请选择储位";
                txtInput.Text = "";
                return;
            }
            if (cbxTYPE.Text != "BOXID" && cbxTYPE.Text != "SN")
            {
                lblMsg.Text = "请选择扫描类型";
                txtInput.Text = "";
                cbxTYPE.Focus();
                return;
            }
            if (txtMatnr.Text == "")
            {
                lblMsg.Text = "请刷入料号";
                txtInput.Text = "";
                txtMatnr.Focus();
                return;
            }

            if (dtWHITM.Rows.Count == 0)
            {
                dtWHITM = GetStock(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtMatnr.Text, txtCharg.Text);
                if (dtWHITM.Rows.Count == 0)
                {
                    lblMsg.Text = "该储位无库存!!";
                }
                //else
                //{
                //    lblQWNSQty.Text = dtWHITM.Rows[0]["MENGE"].ToString();
                //}
            }
            bool isExist = checkBox(cbxTYPE.Text, txtInput.Text);
            if (isExist)
            {
                lblMsg.Text = "此数据已存在，请刷入其他数据！";
                txtInput.Text = "";
                txtInput.Focus();
                return;
            }

            DataTable dtTMPData = new DataTable();
            dtTMPData = getTMPData(Werks, Lgort, txtLocat.Text, txtMatnr.Text, txtCharg.Text);
            if (dtTMPData.Rows.Count > 0)
            {

                for (int i = 0; i < dtTMPData.Rows.Count; i++)
                {
                    drRow = dtData.NewRow();
                    drRow["TYPE"] = cbxTYPE.SelectedItem.ToString();
                    drRow["BOXID"] = dtTMPData.Rows[i]["BOXID"].ToString().Trim();
                    drRow["SERNO"] = dtTMPData.Rows[i]["SERNO"].ToString().Trim();
                    drRow["MENGE"] = dtTMPData.Rows[i]["MENGE"].ToString().Trim();
                    dtData.Rows.Add(drRow);
                }
                
                lblSNQty.Text = (Int32.Parse(lblSNQty.Text) + Int32.Parse(dtTMPData.Rows.Count.ToString())).ToString(); 
                lblWHTotalQty.Text = (Int32.Parse(lblWHTotalQty.Text) + Int32.Parse(dtTMPData.Rows.Count.ToString())).ToString();
                //dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 2;
                //dgvData.Rows[dgvData.Rows.Count - 1].Cells[0].Selected = true;
                //dttmpWHBOX_IN.Merge(dtTMPData);

            }
            

            DataTable dt = checkBOXORSN(cbxTYPE.Text, txtInput.Text,dtWHITM,ref strQTY);
            
            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = "No Data";
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drRow = dtData.NewRow();
                    drRow["TYPE"] = cbxTYPE.SelectedItem.ToString();
                    drRow["BOXID"] = dt.Rows[i]["BOXID"].ToString().Trim();
                    drRow["SERNO"] = dt.Rows[i]["SERNO"].ToString().Trim();
                    drRow["MENGE"] = dt.Rows[i]["MENGE"].ToString().Trim();
                    dtData.Rows.Add(drRow);
                }
                ShowDataGrid();
            }
            txtInput.Text = "";
        }

        private bool checkBox(string strType,string strSN)
        {
            DataTable dtCheck = new DataTable();
            bool check = false;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM WHBOX WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + txtLocat.Text + "' AND MATNR='" + txtMatnr.Text + "' AND CHARG='" + txtCharg.Text + "'");
            if (strType == "BOXID")
            {
                sb.AppendLine("AND BOXID = '"+ strSN + "'");
            }
            else if (strType == "SN")
            {
                sb.AppendLine("AND SERNO = '" + strSN + "'");
            }
            clsGlobalVar.SetDBConn();
            dtCheck = clsGlobalVar.GetDataTable(sb.ToString());
            if (dtCheck.Rows.Count > 0)
            {
                check = true;
            }
            return check;
        }

        private DataTable getTMPData(string strWerks,string strLgort, string strLocat,string strMatnr, string strCharg)
        {
            string strSQL = "SELECT * FROM tmpWHBOX_IN WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND MATNR='" + strMatnr + "' AND CHARG='" + strCharg + "'";
            DataTable dt = new DataTable();
            clsGlobalVar.SetDBConn();
            dt = clsGlobalVar.GetDataTable(strSQL);
            return dt;
        }

        private DataTable GetStock(string strWerks,string strLgort, string strLocat, string strMatnr, string strCharg)
        {
            string strSQL = "SELECT * FROM WHITM WHERE WERKS='"+ strWerks +"' AND LGORT = '"+ strLgort +"' AND LOCAT ='"+ strLocat +"' AND MATNR ='"+ strMatnr +"' AND CHARG ='"+ strCharg +"'";
            DataTable dt = new DataTable();
            clsGlobalVar.SetDBConn();
            dt = clsGlobalVar.GetDataTable(strSQL);
            return dt;
        }

        private DataTable checkBOXORSN(string strType, string strSN, DataTable dtWHITM ,ref string strQTY)
        {
            //string strMsg = "";
            DataRow[] drs;
            DataRow drRow;
            DataTable dt = new DataTable();
           
            if (strType == "BOXID")
            {
                drs = dttmpWHBOX_IN.Select("BOXID ='" + strSN + "'");
                if (drs.Length > 0)
                {
                    lblMsg.Text = "BOXID资料已存在";
                    //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "BOXID 資料已存在");
                    AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "BOXID 資料已存在", "N", Usrnm);
                    //return strMsg;
                }
                else
                {
                    DataTable dtCheck = new DataTable();
                    //dtCheck = clsGlobalVar.GetDataTable("SELECT * FROM tmpWHBOX_IN  WITH (NOLOCK) WHERE MANDT = '"+ UserData.Client +"' AND COMCD='"+ UserData.CompanyCode +"' AND BOXID ='" + strSN + "' WERKS='" + cbxPlant.Text + "' AND LGORT = '" + cbxLoc.Text + "' AND LOCAT ='" + cbxlogc.Text + "' AND MATNR ='" + txtMatnr.Text + "' AND CHARG ='" + txtCharg.Text + "'");
                    //by Jack 20150410(提示'WERKS'附近有语法错误，在WERKS前面加上AND)
                    dtCheck = clsGlobalVar.GetDataTable("SELECT * FROM tmpWHBOX_IN  WITH (NOLOCK) WHERE MANDT = '"+ Mandt +"' AND COMCD='" + Comcd + "' AND BOXID ='" + strSN + "' AND WERKS='" + Werks + "' AND LGORT = '" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "' AND MATNR ='" + txtMatnr.Text + "' AND CHARG ='" + txtCharg.Text + "'");
                    if (dtCheck.Rows.Count > 0)
                    {
                        lblMsg.Text = "BOXID 資料已存在";
                        //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "BOXID 資料已存在");
                        AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "BOXID 資料已存在", "N", Usrnm);
                        //return strMsg;
                    }
                    else
                    {
                        //string strSQL = "SELECT MANDT, COMCD, '" + cbxPlant.Text + "' WERKS,'" + cbxLoc.Text + "' LGORT, '" + cbxlogc.Text + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "' MATNR, SERNO, LIFNR, KDMAT,LOADID,CONVERT(VARCHAR(8),CRDAT,112) ISPTM, 'N' STATUS FROM DBO.WHBOX WITH (NOLOCK) WHERE MANDT = '"+ UserData.Client +"' AND COMCD='"+ UserData.CompanyCode +"' AND BOXID ='" + strSN + "' AND MATNR ='" + txtMatnr.Text + "' AND CHARG ='" + cbxlogc.Text + "'";
                        //by Jack 20150410(列'LIFNR'不存在，列'CRDAT'不存在。已经把LIFNR字段拿掉，把CRDAT改为CRTDAT)
                        //string strSQL = "SELECT MANDT, COMCD, '" + Werks + "' WERKS,'" + Lgort + "' LGORT, '" + txtLocat.Text.ToString().Trim() + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "' MATNR, SERNO, KDMAT,LOADID,CONVERT(VARCHAR(8),CRTDAT,112) ISPTM, 'N' STATUS FROM DBO.WHBOX WITH (NOLOCK) WHERE MANDT = '"+ Mandt +"' AND COMCD='"+ Comcd +"' AND BOXID ='" + strSN + "' AND MATNR ='" + txtMatnr.Text + "' AND CHARG ='" + txtCharg.Text + "'";
                        //dt = clsGlobalVar.GetDataTable(strSQL);
                        //if (dt.Rows.Count == 0)
                        //{
                        dt.Rows.Clear();
                        string strSQL = "SELECT MANDT, COMCD, WERKS, LGORT, '" + txtLocat.Text.ToString().Trim() + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "' MATNR,  MENGE, SERNO,LOADID,KDMAT,GETDATE() CRDAT FROM DBO.WHDWN WITH (NOLOCK) WHERE MANDT = '" + Mandt +"' AND MTYPE in ('QMS','QMS_311') AND COMCD='"+ Comcd + "' AND BOXID ='" + strSN + "'";
                        //strSQL = "SELECT MANDT, COMCD, '" + cbxPlant.Text + "' WERKS,'"+ cbxLoc.Text +"' LGORT, '" + cbxlogc.Text + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "' MATNR, SERNO, LIFNR, KDMAT,LOADID,CONVERT(VARCHAR(8),CRDAT,112) ISPTM, 'N' STATUS FROM DBO.WHDWN_QMS_BAK WITH (NOLOCK) WHERE MANDT = '"+ UserData.Client +"' AND MTYPE= 'QMS' AND COMCD='"+ UserData.CompanyCode +"' AND BOXID ='" + strSN + "'";
                        dt = clsGlobalVar.GetDataTable(strSQL);
                            //Modify by Jack 20150626
                            //if (dt.Rows.Count == 0)
                            //{
                            //    dt.Rows.Clear();
                            //    strSQL = "SELECT MANDT, COMCD, '" + cmbWerks.Text + "' WERKS,'" + cmbLgort.Text + "' LGORT, '" + txtLocat.Text + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "' MATNR, SERNO, LIFNR, KDMAT,LOADID,CONVERT(VARCHAR(8),CRDAT,112) ISPTM, 'N' STATUS FROM DBO.WHDWN_QMS_BAK WITH (NOLOCK) WHERE MANDT = '"+ UserData.Client +"' AND MTYPE in ('QMS','QMS_311') AND COMCD='"+ UserData.CompanyCode +"' AND BOXID ='" + strSN + "'";
                            //    dt = clsGlobalVar.GetDataTable(strSQL);
                            //}

                        if (dt.Rows.Count > 0)
                        {
                            strQTY = dt.Rows.Count.ToString();
                            lblWHTotalQty.Text = (Int32.Parse(lblWHTotalQty.Text) + Int32.Parse(strQTY)).ToString();
                            lblBOXQty.Text = (Int32.Parse(lblBOXQty.Text) + Int32.Parse(strQTY)).ToString();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                drRow = dttmpWHBOX_IN.NewRow();
                                drRow["MANDT"] = dt.Rows[i]["MANDT"];
                                drRow["COMCD"] = dt.Rows[i]["COMCD"];
                                drRow["WERKS"] = dt.Rows[i]["WERKS"];
                                drRow["LGORT"] = dt.Rows[i]["LGORT"];
                                drRow["LOCAT"] = dt.Rows[i]["LOCAT"];
                                drRow["MBLNR"] = dt.Rows[i]["MBLNR"];
                                drRow["BOXID"] = dt.Rows[i]["BOXID"];
                                drRow["INSMK"] = dt.Rows[i]["INSMK"];
                                drRow["CHARG"] = dt.Rows[i]["CHARG"];
                                drRow["MATNR"] = dt.Rows[i]["MATNR"];
                                drRow["SERNO"] = dt.Rows[i]["SERNO"];
                                drRow["LOADID"] = dt.Rows[i]["LOADID"];
                                drRow["KDMAT"] = dt.Rows[i]["KDMAT"];
                                drRow["CRDAT"] = dt.Rows[i]["CRDAT"];
                                drRow["MENGE"] = dt.Rows[i]["MENGE"];
                                dttmpWHBOX_IN.Rows.Add(drRow);
                            }
                            //dttmpWHBOX_IN.Merge(dt);
                            return dt;
                        }
                        else
                        {
                            lblMsg.Text = txtInput.Text.ToUpper()+ "無SF資料";
                            //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "無SF資料");
                            AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "無SF資料", "N", Usrnm);
                            //return strMsg;
                        }
                    }
                }
            }
            else if (strType == "SN")
            {
                //if (strSN.Length != 12)
                //{
                //    lblMsg.Text = strSN +" 此序號不是12碼";
                //    //return strMsg;
                //}
                drs = dttmpWHBOX_IN.Select("SERNO ='" + strSN + "'");
                if (drs.Length > 0)
                {
                    lblMsg.Text = "SN 資料已存在";
                    //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "SN 資料已存在");
                    AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "SN 資料已存在", "N", Usrnm);
                    //return strMsg;
                }
                else
                {
                    DataTable dtCheck = new DataTable();
                    dtCheck = clsGlobalVar.GetDataTable("SELECT * FROM tmpWHBOX_IN WITH (NOLOCK) WHERE MANDT = '"+ UserData.Client +"' AND COMCD='" + UserData.CompanyCode + "' AND SERNO ='" + strSN + "'");
                    if (dtCheck.Rows.Count > 0)
                    {
                        lblMsg.Text = "SN 資料已存在";
                        //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "SN 資料已存在");
                        AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "SN 資料已存在", "N", Usrnm);
                    }
                    else
                    {
                        string strSQL = "SELECT MANDT, COMCD,  WERKS, LGORT, '" + txtLocat.Text + "' LOCAT, MBLNR, BOXID, INSMK,'" + txtCharg.Text + "' CHARG,'" + txtMatnr.Text + "'  MATNR,  MENGE,  SERNO,LOADID,KDMAT,GETDATE() CRDAT, MENGE FROM DBO.WHDWN WITH (NOLOCK) WHERE MANDT = '" + Mandt +"' AND MTYPE IN ('QMS','QMS_311') AND COMCD='"+ Comcd +"' AND SERNO ='" + strSN + "' ORDER BY CRDAT DESC";
                        dt = clsGlobalVar.GetDataTable(strSQL);

                        if (dt.Rows.Count > 0)
                        {
                            strQTY = dt.Rows.Count.ToString();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                drRow = dttmpWHBOX_IN.NewRow();
                                drRow["MANDT"] = dt.Rows[i]["MANDT"];
                                drRow["COMCD"] = dt.Rows[i]["COMCD"];
                                drRow["WERKS"] = dt.Rows[i]["WERKS"];
                                drRow["LGORT"] = dt.Rows[i]["LGORT"];
                                drRow["LOCAT"] = dt.Rows[i]["LOCAT"];
                                drRow["MBLNR"] = dt.Rows[i]["MBLNR"];
                                drRow["BOXID"] = dt.Rows[i]["BOXID"];
                                drRow["INSMK"] = dt.Rows[i]["INSMK"];
                                drRow["CHARG"] = dt.Rows[i]["CHARG"];
                                drRow["MATNR"] = dt.Rows[i]["MATNR"];
                                drRow["SERNO"] = dt.Rows[i]["SERNO"];
                                drRow["LOADID"] = dt.Rows[i]["LOADID"];
                                drRow["KDMAT"] = dt.Rows[i]["KDMAT"];
                                drRow["CRDAT"] = dt.Rows[i]["CRDAT"];
                                drRow["MENGE"] = dt.Rows[i]["MENGE"];
                                dttmpWHBOX_IN.Rows.Add(drRow);
                            }
                            //dttmpWHBOX_IN.Merge(dt);
                        }
                        else
                        {
                            lblMsg.Text = txtInput.Text.ToUpper() + "無SF資料";
                            //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "無SF資料");
                            AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "無SF資料", "N", Usrnm);
                            #region 不用
                            //strQTY = "1";
                            //DataRow dr = dttmpWHBOX_IN.NewRow();
                            //dr["MANDT"] = Mandt;
                            //dr["COMCD"] = Comcd;
                            //dr["WERKS"] = Werks;
                            //dr["LGORT"] = Lgort;
                            //dr["LOCAT"] = txtLocat.Text.ToString().ToUpper().Trim();
                            //dr["MBLNR"] = txtMatnr.Text.ToUpper().Trim();
                            //dr["BOXID"] = "";
                            //dr["INSMK"] = "G";
                            //dr["CHARG"] = txtCharg.Text.ToUpper().Trim();
                            //dr["MATNR"] = txtMatnr.Text.ToUpper().Trim();
                            //dr["SERNO"] = txtInput.Text.ToUpper().Trim();
                            //dr["LIFNR"] = "PAL";
                            //dr["KDMAT"] = "";
                            //dr["LOADID"] = "";
                            //dr["ISPTM"] = System.DateTime.Now.ToString("yyyyMMdd");
                            //dr["STATUS"] = "N";

                            //dttmpWHBOX_IN.Rows.Add(dr);
                            ////return strMsg;
                            //return dt;
                            #endregion
                        }
                        lblWHTotalQty.Text = (Int32.Parse(lblWHTotalQty.Text) + Int32.Parse(strQTY)).ToString();
                        lblSNQty.Text = (Int32.Parse(lblSNQty.Text) + Int32.Parse(strQTY)).ToString();
                        return dt;
                    }
                }
            }
            else
            {
                lblMsg.Text = "查無資料!!";
                //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "查無資料");
                AddQWMSLOG(strSN, cbxTYPE.SelectedItem.ToString(), "INPUT", "查無資料", "N", Usrnm);
                //return strMsg;
            }

            return dt;
        }

        private void init()
        {
            dtData.Clear();
            dttmpWHBOX_IN.Clear();
            cbxTYPE.Items.Clear();
            cbxTYPE.Items.Add("BOXID");
            cbxTYPE.Items.Add("SN");
            cbxTYPE.Text = "BOXID";
            txtInput.Enabled = false;
            txtInput.Text = "";
            txtMatnr.Enabled = false;
            txtCharg.Enabled = false;
            txtCharg.Text = "";
            txtMatnr.Text = "";
            lblWHTotalQty.Text = "0";
            lblQWNSQty.Text = "0";
            lblBOXQty.Text = "0";
            lblSNQty.Text = "0";
            button1.Enabled = false;
            dgvData.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)//比对并更新
        {
            button1.Enabled = false;

            //塞入WHBOX及WHITM
            try
            {
                DataTable dtResult = new DataTable();
                if (Int64.Parse(lblWHTotalQty.Text) == 0)
                {
                    lblMsg.Text = "無數量";
                    return;
                }
                if (Int64.Parse(lblWHTotalQty.Text) == Int64.Parse(lblQWNSQty.Text))
                {
                    dtResult = clsGlobalVar.GetDataTable("EXEC usp_Insert_Tempdata_To_PRD_NEW '" + Werks + "','" + Lgort + "','" + txtLocat.Text.ToString().Trim() + "','" + txtMatnr.Text.ToString().Trim().ToUpper() + "','"+ txtCharg.Text.ToString().Trim().ToUpper() +"'");
                    if (dtResult.Rows[0]["REASON"].ToString() == "")
                    {
                        lblMsg.Text = "總數量與QWMS一致";
                        MessageBox.Show("比對結果正確!");
                    }
                    else
                    {
                        lblMsg.Text = dtResult.Rows[0]["REASON"].ToString();
                        MessageBox.Show("比對結果失敗!");
                    }
                }
                else
                {
                    lblMsg.Text = "總數量與QWMS不一致";
                    MessageBox.Show("比對結果失敗!");
                }
            }
            catch
            {
                //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "塞入失敗sp_insert");
                AddQWMSLOG("SN", cbxTYPE.SelectedItem.ToString(), "IN", "塞入失敗sp_insert", "N", Usrnm);
                lblMsg.Text = "塞入失敗sp_insert";
            }
            init();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            init();
            txtInput.Text = "";
            txtLocat.Text = "";
            txtCharg.Enabled = false;
            txtCharg.Text = "";
            txtMatnr.Enabled = false;
            txtMatnr.Text = "";
            lblWHTotalQty.Text = "0";
            lblQWNSQty.Text = "0";
            lblBOXQty.Text = "0";
            lblSNQty.Text = "0";
            button1.Enabled = false;
            lblMsg.Text = "";
            this.dgvData.DataSource = null;
        }

        public bool clearBOXIN_Data(string strPlant,string strLgort, string strLocat)
        {
            bool blFlag = false;
            clsGlobalVar.SetDBConn();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT MBLNR FROM tmpWHBOX_IN WHERE MANDT ='"+ Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "' ");
            //sb.AppendLine("union all SELECT TOP 1 MBLNR FROM TMPWHITM WHERE MANDT ='"+ Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "' ");
            //sb.AppendLine("union all SELECT TOP 1 MBLNR FROM TMPWHBOX WHERE MANDT ='"+ Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "' ");

            dt = clsGlobalVar.GetDataTable(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                sb = new StringBuilder();
                sb.AppendLine(" DELETE FROM tmpWHBOX_IN WHERE MANDT ='" + Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "'  ");
                //sb.AppendLine(" DELETE FROM TMPWHITM WHERE MANDT ='"+ Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "' ");
                //sb.AppendLine(" DELETE FROM TMPWHBOX WHERE MANDT ='"+ Mandt +"' AND COMCD ='"+ Comcd +"' AND WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND LOCAT ='" + txtLocat.Text.ToString().Trim() + "'  ");
                //sb.AppendLine(" UPDATE  WHCTRL SET CTRLC2 ='' WHERE CTRLID ='LOCAT' AND CTRLC1 ='" + cbxlogc.Text + "' ");
                clsGlobalVar.ExecuteNonQuery(sb.ToString());
                MessageBox.Show("刪除成功");
            }
            else
            {
                MessageBox.Show("無資料可刪除");
            }
            return blFlag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (cmbWerks.Text == "請選擇廠區")
            {
                lblMsg.Text = "請選擇廠區";
                txtInput.Text = "";
                cmbWerks.Focus();
                return;
            }
            if (cmbLgort.Text == "")
            {
                lblMsg.Text = "請選擇倉別";
                txtInput.Text = "";
                cmbLgort.Focus();
                return;
            }
            if (txtLocat.Text == "")
            {
                lblMsg.Text = "請選擇儲位";
                txtInput.Text = "";
                txtLocat.Focus();
                return;
            }

            clearBOXIN_Data(cmbWerks.Text,cmbLgort.Text , txtLocat.Text);
        }

        //private void cbxlogc_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    txtMatnr.Enabled = true;
        //    txtMatnr.Items.Clear();
        //    txtCharg.Items.Clear();
        //    dtWHITM = new DataTable();
        //    DataView dvMatnr = new DataView(dtMatnr);
        //    dvMatnr.RowFilter = "WERKS='" + cmbWerks.SelectedItem.ToString() + "' AND LGORT ='" + cmbLgort.SelectedItem.ToString() + "' AND LOCAT ='"+ txtLocat.SelectedItem.ToString() +"'";
        //    dtPN = new DataTable();
        //    dtPN = dvMatnr.ToTable(true, new string[] { "MATNR", "CHARG" });
        //    if (dtPN.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtPN.Rows)
        //        {
        //            txtMatnr.Items.Add(dr["MATNR"]);
        //        }
        //    }
        //}

        //private void txtMatnr_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (dtPN.Rows.Count > 0)
        //    {
        //        txtCharg.Enabled = true;
        //        txtCharg.Items.Clear();
        //        DataView dvGharg = new DataView(dtPN);
        //        dvGharg.RowFilter = "MATNR ='"+ txtMatnr.SelectedItem.ToString() + "'";
        //        DataTable dt = new DataTable();
        //        dt = dvGharg.ToTable(true, new string[] { "CHARG" });
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                txtCharg.Items.Add(dr["CHARG"]);
        //            }
        //        }
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dttmpWHBOX_IN.Rows.Count == 0)
                {
                    lblMsg.Text = "無刷入資料";
                }
                if (Int64.Parse(lblWHTotalQty.Text) != Int64.Parse(lblQWNSQty.Text))
                {
                    lblMsg.Text = "数量不同无法保存";
                }
                else
                {
                    lblMsg.Text = "";
                    clsGlobalVar.ExecuteNonQuery("DELETE FROM tmpWHBOX_IN WHERE WERKS='"+ cmbWerks.Text.Trim() +"' AND LGORT='"+ cmbLgort.Text.Trim() +"' AND LOCAT='"+ txtLocat.Text.Trim() +"' AND MATNR='"+ txtMatnr.Text.Trim() +"' AND CHARG='"+ txtCharg.Text.Trim() +"'");
                    clsGlobalVar.SqlBulkCopy("tmpWHBOX_IN", dttmpWHBOX_IN);
                    button1.Enabled = true;
                }
            }
            catch
            {
                lblMsg.Text = "保存失敗，請聯繫QWMS人員";
                //clsGlobalVar.WriteDBLogs(cmbWerks.Text, cmbLgort.Text, txtLocat.Text, txtInput.Text, "保存失敗，請聯繫IT人員");
                AddQWMSLOG("SN", cbxTYPE.SelectedItem.ToString(), "IN", "保存失敗，請聯繫QWMS人員", "N", Usrnm);

                return;
            }
            MessageBox.Show("保存成功!!");
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDdlLgort();
            //ShowDdlLocat();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowDdlLocat();
        }

        private void txtMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Werks == "" || Lgort =="" || txtLocat.Text == "")
                {
                    lblMsg.Text = "厂区仓别储位不得为空！";
                    return;
                }
                else
                {
                    GetMatnrData();
                    if (dtMatnr.Rows.Count == 0)
                    {
                        lblMsg.Text = "该料号不存在";
                        return;
                    }
                    else
                    {
                        this.txtCharg.Enabled = true;
                        this.txtCharg.Focus();
                    }
                }
                //txtMatnr.Enabled = false;
            }
        }

        private void txtCharg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //txtCharg.Enabled = false;
                for (int i = 0; i < dtMatnr.Rows.Count; i++)
                {
                    if (txtCharg.Text != dtMatnr.Rows[i]["CHARG"].ToString())
                    {
                        lblMsg.Text = "请输入正确的版本信息";
                        return;
                    }
                }
                GetQWMSQty();
                this.txtInput.Enabled = true;
                this.txtInput.Focus();
            }
        }

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    lblMsg.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ADD");
                    objManage_LocationSelect.ShowDialog();
                    txtLocat.Text = objManage_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                return;
            }
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    lblMsg.Text = "";
                    if (cmbWerks.SelectedIndex != -1)
                        Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        Werks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        Lgort = "";

                    if (Werks == "" || Lgort == "")
                    {
                        lblMsg.Text = "Plant and storage can't be empty!!";
                        return;
                    }

                    this.txtMatnr.Enabled = true;
                    this.txtMatnr.Focus();
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    return;
                }
            }
        }

        //private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvData.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
        //    {
        //        dgvData.Rows.RemoveAt(e.RowIndex);

        //    }
        //}

    }

    class clsGlobalVar
    {
        public static string _strDBLinkSoldTo { get; set; }
        protected static string _strRunMode;
        protected static string _strDBConnectionString;
        protected static SqlConnection conn;

        #region SetDBConn

        public static void SetDBConn()
        {
            _strDBConnectionString = CommonInfo.Instance.DBCode;//ConfigurationManager.AppSettings["DBCodeTCCTEST_9200"];
        }

        //public static void SetDBConn(string varCompanyCode)
        //{
        //    Dictionary<string, string> _dicConns = new Dictionary<string, string>();
        //    _dicConns.Add("9200", ConfigurationManager.AppSettings["DBCodeTCCTEST_9200"]);

        //    _strDBConnectionString = _dicConns[varCompanyCode];//ConfigurationManager.AppSettings["DBCodeTCCTEST_9200"];
        //}
        #endregion

        //public static void WriteDBLogs(string strPLANTS, string strLGORT, string strLOCAT, string strSERNUM, string strREASON)
        //{
        //    string strDate = DateTime.Now.ToString("yyyyMMdd");
        //    string strTime = DateTime.Now.ToString("HHmmss");
        //    clsGlobalVar.SetDBConn();
        //    //EMP_ID, SERNUM, SOBJID, MACADD, ERRCOD, REASON, UPDDAT, UPDTIM
        //    try
        //    {
        //        //clsGlobalVar.ExecuteNonQuery("INSERT INTO tmpWHLOGS VALUES(N'" + strPLANTS.ToString() + "','" + strLGORT.ToString() + "','" + strLOCAT.ToString() + "','" + strSERNUM.ToString() + "','1',N'" + strREASON.ToString().Replace("'", "").Replace(";", "").Replace(",", "").Replace("*", "%").Replace("--", "") + "','" + strDate + "','" + strTime + "')");
        //    }
        //    catch
        //    { }
        //}


        public static void CloseConnection()
        {
            conn.Close();
        }

        #region GetDataTable
        public static DataTable GetDataTable(string strSQL)
        {
            conn = new SqlConnection(_strDBConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            da.Dispose();
            conn.Close();
            conn.Dispose();
            return ds.Tables["dt"];
        }
        #endregion GetDataTable

        public static void SqlBulkCopy(string strTableName, DataTable dt)
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_strDBConnectionString);
            sqlBulkCopy.DestinationTableName = strTableName;

            foreach (var column in dt.Columns)
                sqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());

            sqlBulkCopy.WriteToServer(dt);

        }

        #region GetDataView
        public static DataView GetDataView(string strSQL)
        {
            conn = new SqlConnection(_strDBConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            da.Dispose();
            conn.Close();
            conn.Dispose();
            return ds.Tables["dt"].DefaultView;
        }
        #endregion GetDataView

        #region ExecuteNonQuery
        public static void ExecuteNonQuery(string strSQL)
        {
            try
            {
                conn = new SqlConnection(_strDBConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion ExecuteNonQuery

        #region GetDataReader
        public static SqlDataReader GetDataReader(string strSQL)
        {
            conn = new SqlConnection(_strDBConnectionString);
            SqlCommand cmd = new SqlCommand(strSQL, conn);
            cmd.CommandTimeout = 0;
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//¤£»Ý­n³s½u®É«á¦Û°ÊÃö³¬dr,Á×§K³s½u¹L¦h¿ù»~
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Cancel();
                cmd.Dispose();
                conn.Dispose();
                throw ex;
            }
            return dr;

            //©I¥s¦¹¤èªk½d¨Ò
            #region example
            //SqlDataReader ds;
            //string msg = "";
            //ds = clsCommon.GetDataReader(sbSQL.ToString());
            //while (ds.Read())
            //{
            //    msg += ds["Äæ¦ì¦WºÙ"].ToString() + "\r\n";
            //}
            #endregion example
        }
        #endregion GetDataReader

        #region GetValue
        public static string GetValue(string strSQL)
        {
            try
            {
                conn = new SqlConnection(_strDBConnectionString);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
                da.SelectCommand.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds, "dt");
                DataTable dt = ds.Tables["dt"];
                conn.Close();
                conn.Dispose();
                da.Dispose();
                return dt.Rows[0][0].ToString();
            }
            catch
            {
                return "Fail";
            }
        }
        #endregion GetValue

        #region GetDataListString
        public static List<string> GetDataListString(string strSQL)
        {
            conn = new SqlConnection(_strDBConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, conn);
            da.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds, "dt");
            da.Dispose();
            conn.Close();
            conn.Dispose();

            List<string> List = new List<string>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    List.Add(ds.Tables[0].Rows[i][0].ToString());
                }
                return List;
            }
            else
            {
                return null;
            }

        }
        #endregion GetDataListString
    }

}
