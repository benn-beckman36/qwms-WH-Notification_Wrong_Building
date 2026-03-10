using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using QCI.QWMS;
using QWMS.Common;
using QWMS_CommonInfo_Biz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageIn_AGV : Form
    {
        #region 定义变量

        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";

        private string strWorkstation = ""; //工作站
        private string strStoragetype = ""; //入库类型
        private string strShelfsize = ""; //料架尺寸
        private string strShelftype = ""; //料架类型

        private string strTaskNo = ""; //任务单号
        private int strreqCode = 0; //请求单号
        private string strMARNO = ""; //料架编码

        private string strAGVSTATE = ""; //当前货架使用状态
        private string strBarCodeInfo_CombinStock = ""; //并储入库实物标签

        private DataTable dtBarCodeInfo;
        private DataRow drBarCodeInfo;
        private DataTable dtDocumentInfo;
        private DataTable dtTempNew;
        private DataTable dtAGVOrder;

        //private SQLAccess objDB;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private PlantData objPlantData;
        private Authority objAuthority;
        private AGVStorageIn objAGVStorageIn;
        private AGVApi objAGVApi;
        private Replenishment objReplenishment;

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

        public string Workstation
        {
            get
            {
                return strWorkstation;
            }
            set
            {
                strWorkstation = value;
            }
        }

        public string Storagetype
        {
            get
            {
                return strStoragetype;
            }
            set
            {
                strStoragetype = value;
            }
        }

        public string Shelfsize
        {
            get
            {
                return strShelfsize;
            }
            set
            {
                strShelfsize = value;
            }
        }

        public string Shelftype
        {
            get
            {
                return strShelftype;
            }
            set
            {
                strShelftype = value;
            }
        }
        #endregion

        #region 构造函数
        private StorageIn_AGV()
        {
            InitializeComponent();
        }
        public StorageIn_AGV(UserInfo _UserData, string strProgid)
            : this()
        {
            UserData = _UserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageIn = new StorageIn(UserData, strProgid);
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objAGVStorageIn = new AGVStorageIn(UserData, strProgid);
                objAGVApi = new AGVApi(UserData);
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strProgid);

                //检查权限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlWorkstation();
                    ShowDdlStoragetype();
                    ShowDdlShelfsize();
                    ShowDdlShelftype();
                    GetDocumentInfo();
                    GetBarCodeInfo();
                    GetdtTempNew();

                    if (cmbWorkstation.Items.Count > 0)
                    {
                        this.cmbWorkstation.SelectedIndex = 0;
                    }
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }

                    this.gbAGVOption.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGridDocumentInfo()
        {
            this.dgvDocumentInfo.AutoGenerateColumns = false;
            this.dgvDocumentInfo.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvDocumentInfo.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvDocumentInfo.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dgvDocumentInfo.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Material";
                dgvcMATNR.Width = 120;
                dgvcMATNR.ReadOnly = true;
                this.dgvDocumentInfo.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Qty";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.Width = 90;
                this.dgvDocumentInfo.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcALQTY = new DataGridViewTextBoxColumn();
                dgvcALQTY.DataPropertyName = "ALQTY";
                dgvcALQTY.HeaderText = "Scan Qty";
                dgvcALQTY.ReadOnly = true;
                dgvcALQTY.Width = 90;
                this.dgvDocumentInfo.Columns.Add(dgvcALQTY);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "Status";
                dgvcINSMK.ReadOnly = true;
                dgvcINSMK.Width = 50;
                dgvcINSMK.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvDocumentInfo.Columns.Add(dgvcINSMK);

                dgvDocumentInfo.DataSource = dtDocumentInfo;

                if (dtDocumentInfo.Rows.Count > 0)
                {
                    this.dgvDocumentInfo.Columns[4].DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    this.dgvDocumentInfo.Columns[5].DefaultCellStyle.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridDocumentInfo()");
            }
        }

        public void ShowDataGridAGVOrder()
        {
            this.dgvAGVOrder.AutoGenerateColumns = false;
            this.dgvAGVOrder.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcPLACE = new DataGridViewTextBoxColumn();
                dgvcPLACE.DataPropertyName = "PLACE";
                dgvcPLACE.HeaderText = "Workstation";
                dgvcPLACE.Width = 60;
                dgvcPLACE.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcPLACE);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKNO";
                dgvcTASKID.Width = 180;
                dgvcTASKID.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcREQNO = new DataGridViewTextBoxColumn();
                dgvcREQNO.DataPropertyName = "REQNO";
                dgvcREQNO.HeaderText = "REQNO";
                dgvcREQNO.ReadOnly = true;
                dgvcREQNO.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcREQNO);

                DataGridViewTextBoxColumn dgvcMARNO = new DataGridViewTextBoxColumn();
                dgvcMARNO.DataPropertyName = "MARNO";
                dgvcMARNO.HeaderText = "MARNO";
                dgvcMARNO.ReadOnly = true;
                dgvcMARNO.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcMARNO);

                DataGridViewTextBoxColumn dgvcSTATE = new DataGridViewTextBoxColumn();
                dgvcSTATE.DataPropertyName = "Shelf_state";
                dgvcSTATE.HeaderText = "Status";
                dgvcSTATE.ReadOnly = true;
                dgvcSTATE.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcSTATE);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "COMCD";
                dgvcCOMCD.ReadOnly = true;
                dgvcCOMCD.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcCOMCD);

                dgvAGVOrder.DataSource = dtAGVOrder;

                //if (dtAGVOrder.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtAGVOrder.Rows.Count; i++)
                //    {
                //        if (dtAGVOrder.Rows[i]["Shelf_state"].ToString() == "1")
                //        {
                //            this.dgvAGVOrder.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridAGVOrder()");
            }
        }

        public void ShowDataGridBarCodeInfo()
        {
            this.dgvBarCodeInfo.AutoGenerateColumns = false;
            this.dgvBarCodeInfo.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMARNO = new DataGridViewTextBoxColumn();
                dgvcMARNO.DataPropertyName = "MARNO";
                dgvcMARNO.HeaderText = "MARNO";
                dgvcMARNO.Width = 60;
                dgvcMARNO.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcMARNO);

                DataGridViewTextBoxColumn dgvcFTYPE = new DataGridViewTextBoxColumn();
                dgvcFTYPE.DataPropertyName = "FTYPE";
                dgvcFTYPE.HeaderText = "FTYPE";
                dgvcFTYPE.Width = 60;
                dgvcFTYPE.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcFTYPE);

                DataGridViewTextBoxColumn dgvcLOCTYPE = new DataGridViewTextBoxColumn();
                dgvcLOCTYPE.DataPropertyName = "LOCTYPE";
                dgvcLOCTYPE.HeaderText = "LOCTYPE";
                dgvcLOCTYPE.ReadOnly = true;
                dgvcLOCTYPE.Width = 80;
                this.dgvBarCodeInfo.Columns.Add(dgvcLOCTYPE);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "LOCAT";
                dgvcLOCAT.ReadOnly = true;
                dgvcLOCAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.ReadOnly = true;
                dgvcMATNR.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "INSMK";
                dgvcINSMK.ReadOnly = true;
                dgvcINSMK.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcINSMK);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.ReadOnly = true;
                dgvcCHARG.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcINDAT = new DataGridViewTextBoxColumn();
                dgvcINDAT.DataPropertyName = "INDAT";
                dgvcINDAT.HeaderText = "INDAT";
                dgvcINDAT.ReadOnly = true;
                dgvcINDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcINDAT);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DACOD";
                dgvcDACOD.ReadOnly = true;
                dgvcDACOD.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcVEDAT = new DataGridViewTextBoxColumn();
                dgvcVEDAT.DataPropertyName = "VEDAT";
                dgvcVEDAT.HeaderText = "VEDAT";
                dgvcVEDAT.ReadOnly = true;
                dgvcVEDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcVEDAT);

                DataGridViewTextBoxColumn dgvcLOCOD = new DataGridViewTextBoxColumn();
                dgvcLOCOD.DataPropertyName = "LOCOD";
                dgvcLOCOD.HeaderText = "LOCOD";
                dgvcLOCOD.ReadOnly = true;
                dgvcLOCOD.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcLOCOD);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "SERNO";
                dgvcSERNO.ReadOnly = true;
                dgvcSERNO.Width = 180;
                this.dgvBarCodeInfo.Columns.Add(dgvcSERNO);

                DataGridViewTextBoxColumn dgvcALQTY = new DataGridViewTextBoxColumn();
                dgvcALQTY.DataPropertyName = "ALQTY";
                dgvcALQTY.HeaderText = "ALQTY";
                dgvcALQTY.ReadOnly = true;
                dgvcALQTY.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcALQTY);

                DataGridViewTextBoxColumn dgvcITEMSTATES = new DataGridViewTextBoxColumn();
                dgvcITEMSTATES.DataPropertyName = "ITEMSTATES";
                dgvcITEMSTATES.HeaderText = "ITEMSTATES";
                dgvcITEMSTATES.ReadOnly = true;
                dgvcITEMSTATES.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcITEMSTATES);

                DataGridViewTextBoxColumn dgvcSTOCSTATES = new DataGridViewTextBoxColumn();
                dgvcSTOCSTATES.DataPropertyName = "STOCSTATES";
                dgvcSTOCSTATES.HeaderText = "STOCSTATES";
                dgvcSTOCSTATES.ReadOnly = true;
                dgvcSTOCSTATES.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcSTOCSTATES);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "ZEILE";
                dgvcZEILE.ReadOnly = true;
                dgvcZEILE.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcEBELN = new DataGridViewTextBoxColumn();
                dgvcEBELN.DataPropertyName = "EBELN";
                dgvcEBELN.HeaderText = "EBELN";
                dgvcEBELN.ReadOnly = true;
                dgvcEBELN.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcEBELN);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.ReadOnly = true;
                dgvcLIFNR.Width = 80;
                this.dgvBarCodeInfo.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcRMANO = new DataGridViewTextBoxColumn();
                dgvcRMANO.DataPropertyName = "RMANO";
                dgvcRMANO.HeaderText = "RMANO";
                dgvcRMANO.ReadOnly = true;
                dgvcRMANO.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcRMANO);

                DataGridViewTextBoxColumn dgvcOMBLNR = new DataGridViewTextBoxColumn();
                dgvcOMBLNR.DataPropertyName = "OMBLNR";
                dgvcOMBLNR.HeaderText = "OMBLNR";
                dgvcOMBLNR.ReadOnly = true;
                dgvcOMBLNR.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcOMBLNR);

                DataGridViewTextBoxColumn dgvcMRGID = new DataGridViewTextBoxColumn();
                dgvcMRGID.DataPropertyName = "MRGID";
                dgvcMRGID.HeaderText = "MRGID";
                dgvcMRGID.ReadOnly = true;
                dgvcMRGID.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcMRGID);

                DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                dgvcKOSTL.DataPropertyName = "KOSTL";
                dgvcKOSTL.HeaderText = "KOSTL";
                dgvcKOSTL.ReadOnly = true;
                dgvcKOSTL.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcKOSTL);

                DataGridViewTextBoxColumn dgvcARBPL = new DataGridViewTextBoxColumn();
                dgvcARBPL.DataPropertyName = "ARBPL";
                dgvcARBPL.HeaderText = "ARBPL";
                dgvcARBPL.ReadOnly = true;
                dgvcARBPL.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcARBPL);

                DataGridViewTextBoxColumn dgvcTRNTP = new DataGridViewTextBoxColumn();
                dgvcTRNTP.DataPropertyName = "TRNTP";
                dgvcTRNTP.HeaderText = "TRNTP";
                dgvcTRNTP.ReadOnly = true;
                dgvcTRNTP.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcTRNTP);

                DataGridViewTextBoxColumn dgvcRMAK1 = new DataGridViewTextBoxColumn();
                dgvcRMAK1.DataPropertyName = "RMAK1";
                dgvcRMAK1.HeaderText = "RMAK1";
                dgvcRMAK1.ReadOnly = true;
                dgvcRMAK1.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcRMAK1);

                DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                dgvcKDMAT.DataPropertyName = "KDMAT";
                dgvcKDMAT.HeaderText = "KDMAT";
                dgvcKDMAT.ReadOnly = true;
                dgvcKDMAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcKDMAT);

                DataGridViewTextBoxColumn dgvcGRLOC = new DataGridViewTextBoxColumn();
                dgvcGRLOC.DataPropertyName = "GRLOC";
                dgvcGRLOC.HeaderText = "GRLOC";
                dgvcGRLOC.ReadOnly = true;
                dgvcGRLOC.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcGRLOC);

                DataGridViewTextBoxColumn dgvcEXPDAT = new DataGridViewTextBoxColumn();
                dgvcEXPDAT.DataPropertyName = "EXPDAT";
                dgvcEXPDAT.HeaderText = "EXPDAT";
                dgvcEXPDAT.ReadOnly = true;
                dgvcEXPDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcEXPDAT);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKID";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcMAXEXP = new DataGridViewTextBoxColumn();
                dgvcMAXEXP.DataPropertyName = "MAXEXP";
                dgvcMAXEXP.HeaderText = "MAXEXP";
                dgvcMAXEXP.ReadOnly = true;
                dgvcMAXEXP.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcMAXEXP);

                DataTable dtDataNew = new DataTable();
                dtDataNew = dtBarCodeInfo.Copy();
                foreach (DataRow row in dtDataNew.Rows)
                {
                    string cellValue = row["LOCAT"].ToString();
                    if (cellValue.Length > 8)
                    {
                        row["LOCAT"] = cellValue.Substring(0, 8);
                    }
                }
                //dgvBarCodeInfo.DataSource = dtBarCodeInfo;
                dgvBarCodeInfo.DataSource = dtDataNew;

                
                dgvBarCodeInfo.Sort(dgvcMBLNR, ListSortDirection.Ascending); //按照单号升序排序

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridBarCodeInfo()");
            }
        }
        #endregion

        #region Basic Option
        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }
        #endregion

        #region 绑定厂区
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();          
        }
        #endregion

        #region 绑定仓别
        private void ShowDdlLgort()
        {
            try
            {
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
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
                ShowDdlShelftype();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region 绑定工作站
        private void ShowDdlWorkstation()
        {
            DataTable dtWorkstation = new DataTable();
            try
            {
                cmbWorkstation.Items.Clear();
                dtWorkstation = objAGVStorageIn.GetAGVWorkStation();
                for (int i = 0; i < dtWorkstation.Rows.Count; i++)
                {
                    cmbWorkstation.Items.Add(dtWorkstation.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWorkstation()");
            }
        }
        #endregion

        #region 绑定入库类型

        private void ShowDdlStoragetype()
        {
            try
            {
                DataTable dtStoragetype = new DataTable();

                dtStoragetype.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtStoragetype.Rows.Add(new string[] { "Online" });
                dtStoragetype.Rows.Add(new string[] { "Transfer" });
                dtStoragetype.Rows.Add(new string[] { "CombineStock" });

                cmbStoragetype.DisplayMember = "CNTEXT";
                cmbStoragetype.ValueMember = "CNTEXT";
                cmbStoragetype.DataSource = dtStoragetype;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStoragetype()");
            }
        }
        #endregion

        #region 绑定料架尺寸

        private void ShowDdlShelfsize()
        {
            try
            {
                DataTable dtShelfsize = new DataTable();

                dtShelfsize.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtShelfsize.Rows.Add(new string[] { "7" });
                dtShelfsize.Rows.Add(new string[] { "13" });

                cmbShelfsize.DisplayMember = "CNTEXT";
                cmbShelfsize.ValueMember = "CNTEXT";
                cmbShelfsize.DataSource = dtShelfsize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlShelfsize()");
            }
        }
        #endregion

        #region 绑定料架类型

        private void ShowDdlShelftype()
        {
            try
            {
                string strShelftype = objAGVStorageIn.getShelftype(Werks, Lgort);
                if (strShelftype == "BULK")
                {
                    txtShelftype.Text = "BULK";
                }
                else
                {
                    txtShelftype.Text = "ALL";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlShelftype()");
            }
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string strINSMK = "G";
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!";
                    return;
                }

                string strStoragetype = cmbStoragetype.SelectedValue.ToString().Trim(); //入库类型
                if (strStoragetype != "CombineStock")
                {
                    StorageIn_SapDataSelect objStorageIn_SapDataSelect = new StorageIn_SapDataSelect(UserData, Werks, Lgort, Progid, txtMblnr.Text.Trim(), strStoragetype.ToUpper(), strINSMK);
                    objStorageIn_SapDataSelect.ShowDialog();
                    dtDocumentInfo = objStorageIn_SapDataSelect.SapData;
                    //将Scan Qty清零
                    foreach (DataRow row in dtDocumentInfo.Rows)
                    {
                        row["ALQTY"] = 0;
                    }
                    ShowDataGridDocumentInfo();
                }
                else
                {
                    stsWarning.Text = "并储入库单据，自动带出";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetErrNotice();
                return;
            }
        }
        #endregion

        #region btnStartJobs_Click
        private void btnStartJobs_Click(object sender, EventArgs e)
        {
            ShowDdlShelftype();
            List<string> docNumbers = new List<string>();
            docNumbers.Add(txtShelf.Text.ToString());
            strWorkstation = cmbWorkstation.Text.ToString().Trim();
            strStoragetype = cmbStoragetype.SelectedValue.ToString().Trim();
            strShelfsize = cmbShelfsize.SelectedValue.ToString().Trim();
            strShelftype = txtShelftype.Text.ToString().Trim();

            this.stsWarning.Text = string.Empty;
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!";
                    return;
                }
                if (strWorkstation == "" || strStoragetype == "" || strShelfsize == "" || strShelftype == "")
                {
                    stsWarning.Text = "Workstation/Shelf attribute can't be empty!";
                    return;
                }

                // 如果入库类型为并储，则自动带出入库信息
                if (strStoragetype == "CombineStock")
                {
                    DataTable dtWHITM_AGV = objAGVStorageIn.QueryStockWHITM_AGV(Werks, Lgort, Comcd);
                    if (dtWHITM_AGV.Rows.Count > 0)
                    {
                        dtBarCodeInfo = dtWHITM_AGV;
                        ShowDataGridBarCodeInfo();
                    }
                    else
                    {
                        stsWarning.Text = "没有并储入库数据，请确认";
                        return;
                    }
                }

                //1、查询工作站，判断是否分配
                dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation("", "", Workstation, Comcd, "", 0);
                if (dtAGVOrder.Rows.Count <= 0)
                {
                    //生成任务编号 厂区+仓别+年月日+item(6码)
                    //生成任务序号 厂区+仓别+年月日+item(6码) + item(6码)
                    string strHeader = Werks + Lgort + DateTime.Now.ToString("yyyyMMdd");
                    int strSerno = objAGVStorageIn.GetAGVTaskNo();
                    strTaskNo = strHeader + (strSerno).ToString("000000"); //任务编号
                    strreqCode = 1; //任务序号
                    string strResult = string.Empty;

                    if (strStoragetype == "CombineStock")
                    {
                        var head = new
                        {
                            plant = Werks, //所属厂区 32 (必填)
                            storage = Lgort, //仓库别 32 (必填)
                            work_station = Workstation, //工作站别 32 (必填)
                            doc_type = "CombineStock", //单据类型 32 (必填)
                            detail = docNumbers.Select(docNumber => new { shelf_number = docNumber }).ToList(),//料架编号,
                            task_id = strTaskNo, //任务编号 32 (必填)
                            task_sequence = strreqCode, //任务序号 (必填)
                            priority = "4", //优先级 (非必填) 优先级：紧急-1；祥龙出-2；普通出-3；其他-4
                        };
                        string strParam = JsonConvert.SerializeObject(head, Formatting.Indented);

                        strResult = objAGVApi.AGVHttpRequest(strStoragetype, "inboundTask", strParam);
                    }
                    else
                    {
                        Agv_inboundTask head = new Agv_inboundTask
                        {
                            plant = Werks, //所属厂区 32 (必填)
                            storage = Lgort, //仓库别 32 (必填)
                            work_station = Workstation, //工作站别 32 (必填)
                            doc_type = strStoragetype + "In", //单据类型 32 (必填)
                            shelf_size = strShelfsize, //货架尺寸 32 (必填)
                            shelf_type = strShelftype, //货架类型 32 (必填)
                            task_id = strTaskNo, //任务编号 32 (必填)
                            task_type = "", //任务类型 32 (非必填)
                            task_sequence = strreqCode, //任务序号 (必填)
                            priority = "4", //优先级 (非必填) 优先级：紧急-1；祥龙出-2；普通出-3；其他-4
                        };
                        string strParam = JsonConvert.SerializeObject(head);
                        strResult = objAGVApi.AGVHttpRequest(strStoragetype + "In", "inboundTask", strParam);
                    }
                        

                    if (!string.IsNullOrEmpty(strResult))
                    {
                        inboundTaskTaskData response = JsonConvert.DeserializeObject<inboundTaskTaskData>(strResult);
                        string strtask_id = response.task_id;
                        int ittask_sequence = response.task_status;
                        int strtask_sequence = response.task_sequence;
                        if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                        {
                            //回执成功，创建AGV指令到WHAGV，锁定扣账单据WHDWN
                            if (objAGVStorageIn.InsertWHAGV(Werks, Lgort, Workstation, strTaskNo, strreqCode, "", "0", dtDocumentInfo))
                            {
                                dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                                ShowDataGridAGVOrder();

                                this.gbBasicOption.Enabled = false;
                                this.gbAGVOption.Enabled = true;
                                SetOKNotice();
                                ChangeLocatToBarcode();
                                stsWarning.Text = "创建AGV任务成功";
                            }
                            else
                            {
                                stsWarning.Text = "创建AGV任务失败";
                                SetErrNotice();
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "响应错误，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                            SetErrNotice();
                            return;
                        }
                    }
                }
                else
                {
                    stsWarning.Text = "工作站: " + Workstation + " 被占用";
                    SetErrNotice();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetErrNotice();
                return;
            }
        }
        #endregion

        #endregion

        #region AGV Option
        #region btnFlip_Click
        private void btnFlip_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            try
            {
                #region 判断当前工作站货架状态是否到位
                //小车到位(QWMS_api，AGV指令表状态变更1)，判断该状态成功为1后，货架状使用状态(“”-出库默认；0-在途；1-到达；2-回途)
                if (!string.IsNullOrEmpty(strAGVSTATE) && strAGVSTATE == "1")
                {
                    Lightdeng();
                }
                else
                {
                    dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                    DataRow[] drM = dtAGVOrder.Select("TASKID='" + strTaskNo + "' AND REQNO='" + strreqCode + "' ");
                    if (drM.Length > 0)
                    {
                        DataRow drN = drM[0];
                        strAGVSTATE = drN["Shelf_state"].ToString();
                        if (strAGVSTATE == "1")
                        {
                            strMARNO = drN["MARNO"].ToString();
                            ShowDataGridAGVOrder();
                            Lightdeng();
                        }
                        else
                        {
                            stsWarning.Text = "小车未到达工作站，请稍候";
                            SetErrNotice();
                            ChangeLocatToBarcode();
                            return;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "无AGV任务，请确认！";
                        SetErrNotice();
                        ChangeLocatToBarcode();
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetErrNotice();
                return;
            }
        }

        private void Lightdeng()
        {
            //更新WHAGV任务序号
            objAGVStorageIn.UpdateAGVShelf(strWerks, strLgort, strTaskNo, strreqCode);
            strreqCode = strreqCode + 1; //任务序号累加

            //1、调用AGV货架反面接口
            Agv_shelfFlip head = new Agv_shelfFlip
            {
                plant = Werks, //所属厂区 32 (必填)
                storage = Lgort, //仓库别 32 (必填)
                work_station = Workstation, //工作站别 32 (必填)
                shelf_id = strStoragetype + "In", //货架编号 32 (非必填)
                task_id = strTaskNo, //任务编号 32 (必填)
                task_type = "", //任务类型 32 (非必填)
                task_sequence = strreqCode, //任务序号 (必填)
            };
            string strParam = JsonConvert.SerializeObject(head);
            string strResult = string.Empty;
            if (strStoragetype == "CombineStock")
            {
                strResult = objAGVApi.AGVHttpRequest(strStoragetype, "shelfFlip", strParam);
            }
            else
            {
                strResult = objAGVApi.AGVHttpRequest(strStoragetype + "In", "shelfFlip", strParam);
            }

            if (!string.IsNullOrEmpty(strResult))
            {
                inboundTaskTaskData response = JsonConvert.DeserializeObject<inboundTaskTaskData>(strResult);
                string strtask_id = response.task_id;
                int ittask_sequence = response.task_status;
                int strtask_sequence = response.task_sequence;
                if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                {
                    stsWarning.Text = "翻面成功";
                }
                else
                {
                    stsWarning.Text = "响应失败，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                    SetErrNotice();
                    return;
                }
                ChangeLocatToBarcode();
            }
        }
        #endregion

        #region btnCallOff_Click
        private void btnCallOff_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            try
            {
                //查询是否是在该工作站、任务编号下
                dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                if (dtAGVOrder.Rows.Count > 0)
                {
                    strreqCode = strreqCode + 1; //任务序号累加

                    Agv_inboundTask head = new Agv_inboundTask
                    {
                        plant = Werks, //所属厂区 32 (必填)
                        storage = Lgort, //仓库别 32 (必填)
                        work_station = Workstation, //工作站别 32 (必填)
                        doc_type = strStoragetype + "In", //单据类型 32 (必填)
                        shelf_size = strShelfsize, //货架尺寸 32 (必填)
                        shelf_type = strShelftype, //货架类型 32 (必填)
                        task_id = strTaskNo, //任务编号 32 (必填)
                        task_type = "", //任务类型 32 (非必填)
                        task_sequence = strreqCode, //任务序号 (必填)
                        priority = "4", //优先级 (非必填) 优先级：紧急-1；祥龙出-2；普通出-3；其他-4
                    };
                    string strParam = JsonConvert.SerializeObject(head);
                    string strResult = string.Empty;
                    if (strStoragetype == "CombineStock")
                    {
                        strResult = objAGVApi.AGVHttpRequest(strStoragetype, "inboundTask", strParam);
                    }
                    else
                    {
                        strResult = objAGVApi.AGVHttpRequest(strStoragetype + "In", "inboundTask", strParam);
                    }

                    if (!string.IsNullOrEmpty(strResult))
                    {
                        inboundTaskTaskData response = JsonConvert.DeserializeObject<inboundTaskTaskData>(strResult);
                        string strtask_id = response.task_id;
                        int ittask_sequence = response.task_status;
                        int strtask_sequence = response.task_sequence;
                        if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                        {
                            //回执成功，创建AGV指令到WHAGV，锁定扣账单据WHDWN
                            if (objAGVStorageIn.InsertWHAGV(Werks, Lgort, Workstation, strTaskNo, strreqCode, "", "0", dtDocumentInfo))
                            {
                                dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                                //库存正式入储WHITM表，更新WHDWN表单据数量，清除dtDocumentInfo、dtBarCodeInfo
                                if (objAGVStorageIn.Agv_AddStorageInData(dtBarCodeInfo, dtDocumentInfo, strShelfsize))
                                {
                                    dtDocumentInfo.Clear();
                                    dtBarCodeInfo.Clear();
                                    strAGVSTATE = ""; //清空小车状态
                                    strMARNO = ""; //清空小车编号
                                    ShowDataGridAGVOrder();
                                    ShowDataGridDocumentInfo();
                                    ShowDataGridBarCodeInfo();
                                    ChangeLocatToBarcode();
                                    stsWarning.Text = "创建下一个AGV任务成功";
                                    SetOKNotice();
                                }
                                else
                                {
                                    stsWarning.Text = "入库失败";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            else
                            {
                                stsWarning.Text = "创建下一个AGV任务失败";
                                SetErrNotice();
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "响应错误，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                            SetErrNotice();
                            return;
                        }
                    }
                }
                else
                {
                    stsWarning.Text = "工作站: " + Workstation + " 无任务";
                    SetErrNotice();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetErrNotice();
                return;
            }
        }
        #endregion

        #region txtBarCode_KeyDown
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                #region 判断当前工作站货架状态是否到位
                //小车到位(QWMS_api，AGV指令表状态变更1)，判断该状态成功为1后，货架状使用状态(“”-出库默认；0-在途；1-到达；2-回途)
                //if (!string.IsNullOrEmpty(strAGVSTATE) && strAGVSTATE == "1")
                //{
                //    ScanBarcode();
                //}
                //else
                //{
                //    dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                //    DataRow[] drM = dtAGVOrder.Select("TASKID='" + strTaskNo + "' AND REQNO='" + strreqCode + "' ");
                //    if (drM.Length > 0)
                //    {
                //        DataRow drN = drM[0];
                //        strAGVSTATE = drN["Shelf_state"].ToString();
                //        if (strAGVSTATE == "1")
                //        {
                //            strMARNO = drN["MARNO"].ToString();
                //            ShowDataGridAGVOrder();
                //            ScanBarcode();
                //        }
                //        else
                //        {
                //            stsWarning.Text = "AGV don't arrive at the Workstation, please wait";
                //            SetErrNotice();
                //            ChangeLocatToBarcode();
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        stsWarning.Text = "No AGV task, please confirm";
                //        SetErrNotice();
                //        ChangeLocatToBarcode();
                //        return;
                //    }
                //}
                #endregion

                if (strStoragetype != "CombineStock")
                {
                    ShowDataGridAGVOrder();
                    ScanBarcode();
                }
                else
                {
                    strBarCodeInfo_CombinStock = "";
                    strBarCodeInfo_CombinStock = txtBarCode.Text.Trim().ToUpper();
                    ChangeBarcodeToLocat();
                    SetOKNotice();
                }
            }
        }
        #endregion

        #region 处理刷入的实物数据
        private void ScanBarcode()
        {
            #region 处理刷入的数据
            string strBarCode = txtBarCode.Text.Trim().ToUpper();
            string[] str = strBarCode.Split(';');
            drBarCodeInfo = dtBarCodeInfo.NewRow();

            //正常交料 9项：AL040170001; 202250; TIR-TIC; 5052548MY2; 3000; TPS40170QRGYRQ1; BTIR-TIC230223A0254MY; Made in Malaysia; TPS40170QRGYRQ1
            //IQC ReLabel 10项：DAV31UECCC0; 0723; AKV-AMV; 0723; 20; PCB V31U ECU/B (12L,200*184,REVC); R7202308280000711004; MADE IN TAIWAN; THIW12C986B; 20240213
            //物料打印 5项：DA0V3HVB4A0; 2423-0D5Q; GRU-ZDT; SP1230608055; 45
            if (str.Length >= 5)
            {
                drBarCodeInfo["MATNR"] = str[0].ToString().Trim();
                drBarCodeInfo["DACOD"] = str[1].ToString().Trim();
                drBarCodeInfo["LIFNR"] = str[2].ToString().Trim();
                drBarCodeInfo["LOCOD"] = str[3].ToString().Trim();
                drBarCodeInfo["MENGE"] = Convert.ToInt32(str[4].ToString().Trim());
                drBarCodeInfo["SERNO"] = str.Length > 6 ? str[6].ToString().Trim() : "";            
                if (str.Length > 9)
                {
                    string strEXPDAT = str[9].ToString().Trim();
                    if (!ClaCommon.CheckDateValid(strEXPDAT))
                    {
                        MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strEXPDAT));
                        SetErrNotice();
                        txtBarCode.Text = string.Empty;
                        return;
                    }
                    drBarCodeInfo["EXPDAT"] = str.Length > 9 ? strEXPDAT : "";

                    // 根据检验批号查询最大保存期
                    DataTable dtMAXEXP = objAGVStorageIn.QueryMAXEXP(str[6].ToString().Trim().Substring(0, 15));
                    if (dtMAXEXP.Rows.Count > 0)
                    {
                        drBarCodeInfo["MAXEXP"] = dtMAXEXP.Rows[0]["MAXEXP_AFTER"].ToString();
                    }
                }                
                drBarCodeInfo["TASKID"] = str.Length > 9 ? str[6].ToString().Trim().Substring(0, 15) : "";
                if (str.Length > 6)
                {
                    if (dtBarCodeInfo.Rows.Count > 0)
                    {
                        DataRow[] drTemp = dtBarCodeInfo.Select(" SERNO='" + str[6].ToString().Trim() + "' ");
                        if (drTemp.Length > 0)
                        {
                            MessageBox.Show("Serno: " + str[6].ToString().Trim() + " 已经存在!");
                            SetErrNotice();
                            txtBarCode.Text = string.Empty;
                            return;
                        }
                    }
                    if (objStorageData.QueryLgortSameSerno(Werks, Lgort, str[6].ToString().Trim()))
                    {
                        MessageBox.Show("Serno: " + str[6].ToString().Trim() + " 已经存在 "+ Lgort +"!");
                        SetErrNotice();
                        txtBarCode.Text = string.Empty;
                        return;
                    }
                }
            }
            //DID退料 1项：DFHD28MR005-CC37MRR0001
            else
            {
                if (str.Length == 1)
                {
                    //1项则为DIDNO，查询WHRID表，抓取数据
                    DataTable dtData = objStorageData.QueryIQC_WHRID(strBarCode);
                    //MATNR,DACOD,LIFNR,LOCOD,MENGE
                    if (dtData.Rows.Count > 0)
                    {
                        drBarCodeInfo["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                        drBarCodeInfo["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                        drBarCodeInfo["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                        drBarCodeInfo["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                        drBarCodeInfo["MENGE"] = Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                        drBarCodeInfo["SERNO"] = dtData.Rows[0]["SERNO"].ToString();

                        string strEXPDATScan = dtData.Rows[0]["EXPDAT"].ToString();
                        if (strEXPDATScan != "" && !ClaCommon.CheckDateValid(strEXPDATScan))
                        {
                            MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strEXPDATScan));
                            SetErrNotice();
                            txtBarCode.Text = string.Empty;
                            return;
                        }
                        drBarCodeInfo["EXPDAT"] = strEXPDATScan;

                        string strTaskidScan = dtData.Rows[0]["TASKID"].ToString();
                        if (strTaskidScan.Length > 0 && strTaskidScan[0] == 'R')
                        {
                            drBarCodeInfo["TASKID"] = strTaskidScan;
                        }
                        if (!string.IsNullOrEmpty(dtData.Rows[0]["SERNO"].ToString()))
                        {
                            if (dtBarCodeInfo.Rows.Count > 0)
                            {
                                DataRow[] drTemp = dtBarCodeInfo.Select(" SERNO='" + dtData.Rows[0]["SERNO"].ToString() + "' ");
                                if (drTemp.Length > 0)
                                {
                                    MessageBox.Show("Serno: " + dtData.Rows[0]["SERNO"].ToString() + " 已经存在!");
                                    SetErrNotice();
                                    txtBarCode.Text = string.Empty;
                                    return;
                                }
                            }
                            if (objStorageData.QueryLgortSameSerno(Werks, Lgort, dtData.Rows[0]["SERNO"].ToString()))
                            {
                                MessageBox.Show("Serno: " + dtData.Rows[0]["SERNO"].ToString() + " 已经存在 " + Lgort + "!");
                                SetErrNotice();
                                txtBarCode.Text = string.Empty;
                                return;
                            }
                        }
                    }
                    else
                    {
                        stsWarning.Text = "无DIDNO数据!";
                        SetErrNotice();
                        txtBarCode.Text = string.Empty;
                        return;
                    }
                }
                else
                {
                    stsWarning.Text = "格式错误!";
                    SetErrNotice();
                    txtBarCode.Text = string.Empty;
                    return;
                }
            }
            #endregion

            #region 处理DateCode转换问题
            try
            {
                string DC_After = objStorageData.WHDCR_Query(drBarCodeInfo["LIFNR"].ToString(), drBarCodeInfo["DACOD"].ToString());
                if (DC_After != "")
                {
                    DateTime dtVedat = Convert.ToDateTime(DC_After);
                    DC_After = dtVedat.ToString("yyyyMMdd");
                    drBarCodeInfo["VEDAT"] = DC_After;
                }
                else
                {
                    #region 确认是否要转化DateCode Rule
                    string strTemp = objStorageData.getDCTrans(drBarCodeInfo["LIFNR"].ToString(), drBarCodeInfo["DACOD"].ToString()).ToString();
                    if (!string.IsNullOrEmpty(strTemp))
                    {
                        DataTable dtNewDateCode = new DataTable();
                        dtNewDateCode.Columns.Add("LIFNR");
                        dtNewDateCode.Columns.Add("DC_Before");
                        dtNewDateCode.Columns.Add("DC_After");

                        DataRow dr = dtNewDateCode.NewRow();
                        dr["LIFNR"] = drBarCodeInfo["LIFNR"].ToString();
                        dr["DC_Before"] = drBarCodeInfo["DACOD"].ToString();
                        dr["DC_After"] = strTemp;
                        dtNewDateCode.Rows.Add(dr.ItemArray);
                        objStorageData.WHDCR_DML(dtNewDateCode, "NEW", "System");
                        drBarCodeInfo["VEDAT"] = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                    }
                    else
                    {
                        MessageBox.Show("无D/C转换信息找D/C管理人员处理");
                        SetErrNotice();
                        txtBarCode.Text = string.Empty;
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DateCode Transfer error：" + ex.ToString());
                SetErrNotice();
                txtBarCode.Text = string.Empty;
                return;
            }
            #endregion

            ChangeBarcodeToLocat();
            SetOKNotice();
        }
        #endregion

        #region txtLocat_KeyDown
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {                
                try
                {
                    //根据储位前3码，抓取当前货架编码
                    strMARNO = txtLocat.Text.Trim().ToLower().Substring(0, 3);
                    string strLocat = txtLocat.Text.Trim().ToUpper();
                    DataTable dtLocat = objAGVStorageIn.QueryAGVWHHED(Werks, Lgort, strLocat, Comcd, strMARNO, "AGV", strShelfsize);

                    string strMATNR = "";
                    string strDACOD = "";
                    string strLIFNR = "";
                    string strLOCOD = "";
                    int strMENGE = 0;
                    string strSERNO = "";
                    string strEXPDAT = "";
                    string strTASKID = "";

                    if (strStoragetype != "CombineStock")
                    {
                        #region 处理刷入的数据
                        //1、先扫描实物，再扫描储位(调用亮灯接口，熄灯)
                        //校验储位是否存在，以及空闲                        
                        int a = Convert.ToInt32(drBarCodeInfo["MENGE"].ToString().Trim()); //获取此次扫描实物数量                        

                        //判断货架尺寸，7寸则调用亮灯接口(空储位需亮灯)，13寸不用亮灯
                        //校验储位是否存在该料架中
                        if (strShelfsize == "7")
                        {
                            if (dtLocat.Rows.Count > 0)
                            {
                                drBarCodeInfo["MANDT"] = "218";
                                drBarCodeInfo["COMCD"] = Comcd;
                                drBarCodeInfo["LOCAT"] = strLocat;
                                drBarCodeInfo["MARNO"] = strMARNO; //料架编号
                                drBarCodeInfo["FTYPE"] = strLocat.Substring(3, 1); //料架面别(储位第4码)
                                drBarCodeInfo["LOCTYPE"] = "AGV"; //储位类型
                                drBarCodeInfo["ITEMSTATES"] = "U"; //库位状态 (待上架U，待下架D，正常Y)
                                drBarCodeInfo["STOCSTATES"] = "Y"; //库存状态 (未扣帐：N，已扣帐/正常：Y)
                            }
                            else
                            {
                                stsWarning.Text = "Locat: " + strLocat + " 不存在或为空!";
                                SetErrNotice();
                                txtLocat.Text = string.Empty;
                                return;
                            }

                            //储位不可重复
                            if (dtBarCodeInfo.Rows.Count > 0)
                            {
                                DataRow[] drTemp = dtBarCodeInfo.Select(" LOCAT='" + drBarCodeInfo["LOCAT"].ToString().Trim() + "' ");
                                if (drTemp.Length > 0)
                                {
                                    stsWarning.Text = "Locat: " + drBarCodeInfo["LOCAT"].ToString().Trim() + " already exists!";
                                    SetErrNotice();
                                    ChangeLocatToBarcode();
                                    txtLocat.Text = string.Empty;
                                    return;
                                }
                            }

                            #region 验证是否第一次刷入
                            //DataRow[] drExist = dtDocumentInfo.Select(" MATNR='" + drBarCodeInfo["MATNR"].ToString().Trim() + "' AND MENGE > ALQTY ");

                            string matnr = drBarCodeInfo["MATNR"].ToString().Trim(); //扫描实物料号
                            List<DataRow> selectedRowsList = new List<DataRow>();

                            foreach (DataRow row in dtDocumentInfo.Rows)
                            {
                                if (row["MATNR"].ToString().Trim() == matnr)
                                {
                                    decimal menge, alqty;
                                    if (decimal.TryParse(row["MENGE"].ToString(), out menge) && decimal.TryParse(row["ALQTY"].ToString(), out alqty))
                                    {
                                        if (menge > alqty)
                                        {
                                            selectedRowsList.Add(row);
                                        }
                                    }
                                }
                            }
                            DataRow[] drExist = selectedRowsList.ToArray();

                            if (drExist.Length > 0)
                            {
                                int b = Convert.ToInt32(drExist[0]["MENGE"]) - Convert.ToInt32(drExist[0]["ALQTY"]); //dtDocumentInfo中未扫描满的剩余数量
                                int c = a - b; //此次实物多余数量
                                if (c > 0)
                                {
                                    if (ShowStorageInData(strLocat, drBarCodeInfo["MATNR"].ToString(), drBarCodeInfo["LIFNR"].ToString(), c, drBarCodeInfo["VEDAT"].ToString(), drBarCodeInfo["DACOD"].ToString(), drBarCodeInfo["LOCOD"].ToString(), drBarCodeInfo["EXPDAT"].ToString(), drBarCodeInfo["TASKID"].ToString()))
                                    {
                                        drExist[0]["ALQTY"] = Convert.ToInt32(drExist[0]["ALQTY"].ToString().Trim()) + b;

                                        drBarCodeInfo["WERKS"] = drExist[0]["WERKS"].ToString().Trim();
                                        drBarCodeInfo["LGORT"] = drExist[0]["LGORT"].ToString().Trim();
                                        drBarCodeInfo["INSMK"] = drExist[0]["INSMK"].ToString().Trim();
                                        drBarCodeInfo["CHARG"] = drExist[0]["CHARG"].ToString().Trim();
                                        drBarCodeInfo["MBLNR"] = drExist[0]["MBLNR"].ToString().Trim();
                                        drBarCodeInfo["ZEILE"] = drExist[0]["ZEILE"].ToString().Trim();
                                        drBarCodeInfo["EBELN"] = drExist[0]["EBELN"].ToString().Trim();
                                        drBarCodeInfo["INDAT"] = drExist[0]["INDAT"].ToString().Trim();
                                        drBarCodeInfo["MENGE"] = b.ToString();
                                        dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                                    }
                                    else
                                    {
                                        ChangeLocatToBarcode();
                                        return;
                                    }
                                }
                                else
                                {
                                    drExist[0]["ALQTY"] = Convert.ToInt32(drExist[0]["ALQTY"].ToString().Trim()) + a;

                                    drBarCodeInfo["WERKS"] = drExist[0]["WERKS"].ToString().Trim();
                                    drBarCodeInfo["LGORT"] = drExist[0]["LGORT"].ToString().Trim();
                                    drBarCodeInfo["INSMK"] = drExist[0]["INSMK"].ToString().Trim();
                                    drBarCodeInfo["CHARG"] = drExist[0]["CHARG"].ToString().Trim();
                                    drBarCodeInfo["MBLNR"] = drExist[0]["MBLNR"].ToString().Trim();
                                    drBarCodeInfo["ZEILE"] = drExist[0]["ZEILE"].ToString().Trim();
                                    drBarCodeInfo["EBELN"] = drExist[0]["EBELN"].ToString().Trim();
                                    drBarCodeInfo["INDAT"] = drExist[0]["INDAT"].ToString().Trim();
                                    dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                                }
                            }
                            else
                            {
                                //料号第一次刷入
                                if (ShowStorageInData(strLocat, drBarCodeInfo["MATNR"].ToString(), drBarCodeInfo["LIFNR"].ToString(), a, drBarCodeInfo["VEDAT"].ToString(), drBarCodeInfo["DACOD"].ToString(), drBarCodeInfo["LOCOD"].ToString(), drBarCodeInfo["EXPDAT"].ToString(), drBarCodeInfo["TASKID"].ToString()))
                                {
                                    SetOKNotice();
                                }
                                else
                                {
                                    ChangeLocatToBarcode();
                                    return;
                                }
                            }
                            #endregion
                        }
                        if (strShelfsize == "13")
                        {
                            #region 根据实际储位模糊查询，匹配一个为占用的储位
                            if (dtLocat.Rows.Count > 0)
                            {
                                DataRow[] rows = dtLocat.Select("LOSTS = 0");
                                if (rows.Length == 0)
                                {
                                    stsWarning.Text = "Locat: " + strLocat + " not exist or not empty!";
                                    SetErrNotice();
                                    txtLocat.Text = string.Empty;
                                    return;
                                }
                                else
                                {
                                    //13寸料架的储位，获取排序第一个的空闲储位
                                    DataRow firstRow = rows.OrderBy(row => row["LOCAT"]).First();
                                    strLocat = firstRow["LOCAT"].ToString();
                                }
                                drBarCodeInfo["MANDT"] = "218";
                                drBarCodeInfo["COMCD"] = Comcd;
                                drBarCodeInfo["LOCAT"] = strLocat;
                                drBarCodeInfo["MARNO"] = strMARNO; //料架编号
                                drBarCodeInfo["FTYPE"] = strLocat.Substring(3, 1); //料架面别(储位第4码)
                                drBarCodeInfo["LOCTYPE"] = "AGV"; //储位类型
                                drBarCodeInfo["ITEMSTATES"] = "U"; //库位状态
                                drBarCodeInfo["STOCSTATES"] = "Y"; //库存状态
                            }
                            else
                            {
                                stsWarning.Text = "Locat: " + strLocat + " 不存在或为空!";
                                txtLocat.Text = string.Empty;
                                SetErrNotice();
                                return;
                            }
                            #endregion

                            #region 验证是否第一次刷入
                            string matnr = drBarCodeInfo["MATNR"].ToString().Trim(); //扫描实物料号
                            List<DataRow> selectedRowsList = new List<DataRow>();
                            foreach (DataRow row in dtDocumentInfo.Rows)
                            {
                                if (row["MATNR"].ToString().Trim() == matnr)
                                {
                                    decimal menge, alqty;
                                    if (decimal.TryParse(row["MENGE"].ToString(), out menge) && decimal.TryParse(row["ALQTY"].ToString(), out alqty))
                                    {
                                        if (menge > alqty)
                                        {
                                            selectedRowsList.Add(row);
                                        }
                                    }
                                }
                            }
                            DataRow[] drExist = selectedRowsList.ToArray();

                            if (drExist.Length > 0)
                            {
                                //刷入多笔-刷入多笔的时候同一料号不同厂商不能放在同一储位，相同的料号，但是DateCode不一致，不能放在同一储位

                                #region 刷入比对
                                List<DataRow> selectedRowsList1 = new List<DataRow>();
                                if (dtBarCodeInfo.Rows.Count > 1)
                                {
                                    foreach (DataRow row in dtBarCodeInfo.Rows)
                                    {
                                        if (row["MATNR"].ToString().Trim() == strMATNR)
                                        {
                                            string strLOCAT1 = row["LOCAT"].ToString().Substring(0, 8);
                                            string strLOCAT2 = strLocat.Substring(0, 8);
                                            if (strLOCAT1 == strLOCAT2)
                                            {
                                                selectedRowsList1.Add(row);
                                            }
                                        }
                                    }
                                }
                                DataRow[] drEntry = selectedRowsList1.ToArray();

                                if (drEntry.Length > 0)
                                {
                                    for (int i = 0; i < drEntry.Length; i++)
                                    {
                                        DataRow drN = drEntry[i];
                                        if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["DACOD"].ToString().Trim() != drN["DACOD"].ToString())
                                        {
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Date Code";
                                            return;
                                        }
                                        #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                        {
                                            if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["LOCOD"].ToString().Trim() != drN["LOCOD"].ToString())
                                            {
                                                SetErrNotice();
                                                ChangeLocatToBarcode();
                                                txtLocat.Text = string.Empty;
                                                stsWarning.Text = "同储位存在不同Lot Code";
                                                return;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region 库存比对
                                string strLocat_Eight = strLocat.Substring(0, Math.Min(8, strLocat.Length)); //截取储位前8码
                                DataTable dtWhitm = objAGVStorageIn.QueryAGVWHITM(Werks, Lgort, strLocat_Eight, Comcd, strMARNO); //根据储位前8码查询库存
                                DataRow[] drM = dtWhitm.Select();
                                if (drM.Length > 0)
                                {
                                    for (int i = 0; i < drM.Length; i++)
                                    {
                                        DataRow drN = drM[i];
                                        if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["DACOD"].ToString().Trim() != drN["DACOD"].ToString())
                                        {
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Date Code";
                                            return;
                                        }
                                        #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                        {
                                            if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["LOCOD"].ToString().Trim() != drN["LOCOD"].ToString())
                                            {
                                                SetErrNotice();
                                                ChangeLocatToBarcode();
                                                txtLocat.Text = string.Empty;
                                                stsWarning.Text = "同储位存在不同Lot Code";
                                                return;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                int b = Convert.ToInt32(drExist[0]["MENGE"]) - Convert.ToInt32(drExist[0]["ALQTY"]); //dtDocumentInfo中未扫描满的剩余数量
                                int c = a - b; //此次实物多余数量
                                if (c > 0)
                                {
                                    if (ShowStorageInData(strLocat, drBarCodeInfo["MATNR"].ToString(), drBarCodeInfo["LIFNR"].ToString(), c, drBarCodeInfo["VEDAT"].ToString(), drBarCodeInfo["DACOD"].ToString(), drBarCodeInfo["LOCOD"].ToString(), drBarCodeInfo["EXPDAT"].ToString(), drBarCodeInfo["TASKID"].ToString()))
                                    {
                                        drExist[0]["ALQTY"] = Convert.ToInt32(drExist[0]["ALQTY"].ToString().Trim()) + b;

                                        drBarCodeInfo["WERKS"] = drExist[0]["WERKS"].ToString().Trim();
                                        drBarCodeInfo["LGORT"] = drExist[0]["LGORT"].ToString().Trim();
                                        drBarCodeInfo["INSMK"] = drExist[0]["INSMK"].ToString().Trim();
                                        drBarCodeInfo["CHARG"] = drExist[0]["CHARG"].ToString().Trim();
                                        drBarCodeInfo["MBLNR"] = drExist[0]["MBLNR"].ToString().Trim();
                                        drBarCodeInfo["ZEILE"] = drExist[0]["ZEILE"].ToString().Trim();
                                        drBarCodeInfo["EBELN"] = drExist[0]["EBELN"].ToString().Trim();
                                        drBarCodeInfo["INDAT"] = drExist[0]["INDAT"].ToString().Trim();
                                        drBarCodeInfo["MENGE"] = b.ToString();
                                        dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                                    }
                                    else
                                    {
                                        ChangeLocatToBarcode();
                                        return;
                                    }
                                }
                                else
                                {
                                    drExist[0]["ALQTY"] = Convert.ToInt32(drExist[0]["ALQTY"].ToString().Trim()) + a;

                                    drBarCodeInfo["WERKS"] = drExist[0]["WERKS"].ToString().Trim();
                                    drBarCodeInfo["LGORT"] = drExist[0]["LGORT"].ToString().Trim();
                                    drBarCodeInfo["INSMK"] = drExist[0]["INSMK"].ToString().Trim();
                                    drBarCodeInfo["CHARG"] = drExist[0]["CHARG"].ToString().Trim();
                                    drBarCodeInfo["MBLNR"] = drExist[0]["MBLNR"].ToString().Trim();
                                    drBarCodeInfo["ZEILE"] = drExist[0]["ZEILE"].ToString().Trim();
                                    drBarCodeInfo["EBELN"] = drExist[0]["EBELN"].ToString().Trim();
                                    drBarCodeInfo["INDAT"] = drExist[0]["INDAT"].ToString().Trim();
                                    dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                                }
                            }
                            else
                            {
                                //料号第一次刷入-刷入多笔的时候同一料号不同厂商不能放在同一储位，相同的料号，但是DateCode不一致，不能放在同一储位

                                #region 库存比对
                                string strLocat_Eight = strLocat.Substring(0, Math.Min(8, strLocat.Length)); //截取储位前8码
                                DataTable dtWhitm = objAGVStorageIn.QueryAGVWHITM(Werks, Lgort, strLocat_Eight, Comcd, strMARNO); //根据储位前8码查询库存
                                DataRow[] drM = dtWhitm.Select();
                                if (drM.Length > 0)
                                {
                                    for (int i = 0; i < drM.Length; i++)
                                    {
                                        DataRow drN = drM[i];
                                        if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["DACOD"].ToString().Trim() != drN["DACOD"].ToString())
                                        {                                          
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Date Code";
                                            return;
                                        }
                                        #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                        {
                                            if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["LOCOD"].ToString().Trim() != drN["LOCOD"].ToString())
                                            {                                             
                                                SetErrNotice();
                                                ChangeLocatToBarcode();
                                                txtLocat.Text = string.Empty;
                                                stsWarning.Text = "同储位存在不同Lot Code";
                                                return;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region 刷入数据比对
                                List<DataRow> selectedRowsList1 = new List<DataRow>();
                                foreach (DataRow row in dtBarCodeInfo.Rows)
                                {
                                    if (row["MATNR"].ToString().Trim() == matnr)
                                    {
                                        string strLOCAT1 = row["LOCAT"].ToString().Substring(0, 8);
                                        string strLOCAT2 = strLocat.Substring(0, 8);
                                        if (strLOCAT1 == strLOCAT2)
                                        {
                                            selectedRowsList1.Add(row);
                                        }
                                    }
                                }
                                DataRow[] drEntry = selectedRowsList1.ToArray();

                                if (drM.Length > 0)
                                {
                                    for (int i = 0; i < drEntry.Length; i++)
                                    {
                                        DataRow drN = drEntry[i];
                                        if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["DACOD"].ToString().Trim() != drN["DACOD"].ToString())
                                        {
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Date Code";
                                            return;
                                        }
                                        #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                        {
                                            if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["LOCOD"].ToString().Trim() != drN["LOCOD"].ToString())
                                            {
                                                SetErrNotice();
                                                ChangeLocatToBarcode();
                                                txtLocat.Text = string.Empty;
                                                stsWarning.Text = "同储位存在不同Lot Code";
                                                return;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                //料号第一次刷入
                                if (ShowStorageInData(strLocat, drBarCodeInfo["MATNR"].ToString(), drBarCodeInfo["LIFNR"].ToString(), a, drBarCodeInfo["VEDAT"].ToString(), drBarCodeInfo["DACOD"].ToString(), drBarCodeInfo["LOCOD"].ToString(), drBarCodeInfo["EXPDAT"].ToString(), drBarCodeInfo["TASKID"].ToString()))
                                {
                                    SetOKNotice();
                                }
                                else
                                {
                                    ChangeLocatToBarcode();
                                    return;
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region 锁定绑定的WHDWN表单据号 OTQTY -> MENGE
                        if (objAGVStorageIn.AddStorageIn_AGV(Werks, Lgort, strLocat, strShelfsize, dtDocumentInfo, dtBarCodeInfo))
                        {
                            stsWarning.Text = "Add OK";
                            SetOKNotice();
                            ShowDataGridDocumentInfo();
                            ShowDataGridBarCodeInfo();
                            ChangeLocatToBarcode();
                        }
                        else
                        {
                            stsWarning.Text = "Add fail";
                            SetErrNotice();
                            ChangeLocatToBarcode();
                            txtLocat.Text = string.Empty;
                            return;
                        }
                        #endregion

                        #region 调用货架物料更新接口
                        //1、调用AGV货架反面接口
                        Agv_shelfMaterialRenewal head = new Agv_shelfMaterialRenewal
                        {
                            plant = Werks, //所属厂区 32 (必填)
                            storage = Lgort, //仓库别 32 (必填)
                            shelf_id = strMARNO, //货架编号 32 (非必填)
                            action = "ADD", //操作行为 32 (必填)(ADD-入储；DEL- 出储)
                            location = strLocat, //储位 32 (非必填)
                            location_detail = new List<LocationDetailItem>
                            {
                                new LocationDetailItem
                                {
                                    pn = drBarCodeInfo["MATNR"].ToString().Trim(),
                                    version = drBarCodeInfo["CHARG"].ToString().Trim(),
                                    vendor_code = drBarCodeInfo["LIFNR"].ToString().Trim(),
                                    date_code = drBarCodeInfo["DACOD"].ToString().Trim(),
                                    quantity = a
                                }
                            }
                        };
                        string strParam = JsonConvert.SerializeObject(head);
                        string strResult = objAGVApi.AGVHttpRequest(strStoragetype + "In", "shelfMaterialRenewal", strParam);

                        if (!string.IsNullOrEmpty(strResult))
                        {
                            //待确认
                        }
                        #endregion
                    }
                    else
                    {
                        #region 处理刷入的实物标签
                        string[] str = strBarCodeInfo_CombinStock.Split(';');

                        if (str.Length >= 5)
                        {
                            strMATNR = str[0].ToString().Trim();
                            strDACOD = str[1].ToString().Trim();
                            strLIFNR = str[2].ToString().Trim();
                            strLOCOD = str[3].ToString().Trim();
                            strMENGE = Convert.ToInt32(str[4].ToString().Trim());
                            strSERNO = str.Length > 6 ? str[6].ToString().Trim() : "";
                            strEXPDAT = str.Length > 9 ? str[9].ToString().Trim() : "";
                            strTASKID = str.Length > 9 ? str[6].ToString().Trim().Substring(0, 15) : "";

                            //扫描信息需与库存进行匹配，同一料号不同DateCode，不能放在同一储位

                            #region 库存比对
                            string strLocat_Eight = strLocat.Substring(0, Math.Min(8, strLocat.Length)); //截取储位前8码
                            DataTable dtWhitm = objAGVStorageIn.QueryAGVWHITM(Werks, Lgort, strLocat_Eight, Comcd, strMARNO); //根据储位前8码查询库存
                            DataRow[] drM = dtWhitm.Select();
                            if (drM.Length > 0)
                            {
                                for (int i = 0; i < drM.Length; i++)
                                {
                                    DataRow drN = drM[i];
                                    if (strMATNR == drN["MATNR"].ToString() && strDACOD != drN["DACOD"].ToString())
                                    {                                      
                                        SetErrNotice();
                                        ChangeLocatToBarcode();
                                        txtLocat.Text = string.Empty;
                                        stsWarning.Text = "同储位存在不同Date Code";
                                        return;
                                    }
                                    #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                    if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                    {
                                        if (strMATNR == drN["MATNR"].ToString() && strLOCOD != drN["LOCOD"].ToString())
                                        {                                           
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Lot Code";
                                            return;
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region 刷入比对

                            List<DataRow> selectedRowsList1 = new List<DataRow>();
                            if(dtBarCodeInfo.Rows.Count>1)
                            {
                                foreach (DataRow row in dtBarCodeInfo.Rows)
                                {
                                    if (row["MATNR"].ToString().Trim() == strMATNR)
                                    {
                                        string strOutLocat = "";
                                        if (row["LOCAT"].ToString().Substring(0,2)=="DY")
                                        {
                                            strOutLocat = row["LOCAT"].ToString();
                                        }
                                        else
                                        {
                                            strOutLocat = row["LOCAT"].ToString().Substring(0, 8);
                                        }                                    
                                        string strInLocat = strLocat.Substring(0, 8);
                                        if (strOutLocat == strInLocat)
                                        {
                                            selectedRowsList1.Add(row);
                                        }
                                    }
                                }
                            }
                            
                            DataRow[] drEntry = selectedRowsList1.ToArray();

                            if (drEntry.Length > 0)
                            {
                                for (int i = 0; i < drEntry.Length; i++)
                                {
                                    DataRow drN = drEntry[i];
                                    if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["DACOD"].ToString().Trim() != drN["DACOD"].ToString())
                                    {
                                        SetErrNotice();
                                        ChangeLocatToBarcode();
                                        txtLocat.Text = string.Empty;
                                        stsWarning.Text = "同储位存在不同Date Code";
                                        return;
                                    }
                                    #region Lot Code不同Lot Code不允许入库 - Lot code仓别                                                                   
                                    if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                    {
                                        if (drBarCodeInfo["MATNR"].ToString().Trim() == drN["MATNR"].ToString() && drBarCodeInfo["LOCOD"].ToString().Trim() != drN["LOCOD"].ToString())
                                        {
                                            SetErrNotice();
                                            ChangeLocatToBarcode();
                                            txtLocat.Text = string.Empty;
                                            stsWarning.Text = "同储位存在不同Lot Code";
                                            return;
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                        }
                        else
                        {
                            if (str.Length == 1)
                            {
                                //1项则为DIDNO，查询WHRID表，抓取数据
                                DataTable dtData = objStorageData.QueryIQC_WHRID(strBarCodeInfo_CombinStock);
                                //MATNR,DACOD,LIFNR,LOCOD,MENGE
                                if (dtData.Rows.Count > 0)
                                {
                                    strMATNR = dtData.Rows[0]["MATNR"].ToString();
                                    strDACOD = dtData.Rows[0]["DACOD"].ToString();
                                    strLIFNR = dtData.Rows[0]["LIFNR"].ToString();
                                    strLOCOD = dtData.Rows[0]["LOCOD"].ToString();
                                    strMENGE = Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                                    strSERNO = dtData.Rows[0]["SERNO"].ToString();
                                    strEXPDAT = dtData.Rows[0]["EXPDAT"].ToString();
                                    if (strEXPDAT != "" && !ClaCommon.CheckDateValid(strEXPDAT))
                                    {
                                        MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strEXPDAT));
                                        SetErrNotice();
                                        return;
                                    }
                                    strTASKID = dtData.Rows[0]["TASKID"].ToString().Trim();
                                }
                                else
                                {
                                    stsWarning.Text = "无DIDNO数据!";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            else
                            {
                                stsWarning.Text = "格式错误!";
                                txtLocat.Text = string.Empty;
                                SetErrNotice();
                                return;
                            }
                        }
                        #endregion

                        #region 根据实物数据和扫描储位，进行界面库存匹配，更新数量
                        
                        DataTable result = dtBarCodeInfo.Clone();
                       
                        #region 根据实际储位模糊查询，匹配一个为占用的储位
                        if (dtLocat.Rows.Count > 0)
                        {
                            DataRow[] rows = dtLocat.Select("LOSTS = 0");
                            if (rows.Length == 0)
                            {
                                stsWarning.Text = "Locat: " + strLocat + " 不存在或者不为空!";
                                SetErrNotice();
                                txtLocat.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                //13寸料架的储位，获取排序第一个的空闲储位
                                DataRow firstRow = rows.OrderBy(row => row["LOCAT"]).First();
                                strLocat = firstRow["LOCAT"].ToString();
                            }
                        }
                        else
                        {
                            stsWarning.Text = "Locat: " + strLocat + " 不存在或者不为空!";
                            SetErrNotice();
                            txtLocat.Text = string.Empty;
                            return;
                        }
                        #endregion
                       
                        //刷入数据与库存数据作比对，避免多单号对应同一个数量-同一个储位
                        DataTable dtInpWHITM_AGV = objAGVStorageIn.CheckStockWHITM_AGV(Werks, Lgort, Comcd,strMATNR, strDACOD, strLIFNR,strLOCOD);
                        foreach (DataRow Inpdr in dtInpWHITM_AGV.Rows)
                        {
                            if (Inpdr["MATNR"].ToString().Trim() == strMATNR &&
                                Inpdr["DACOD"].ToString().Trim() == strDACOD &&
                                Inpdr["LIFNR"].ToString().Trim() == strLIFNR &&
                                Inpdr["LOCOD"].ToString().Trim() == strLOCOD &&
                                Inpdr["MENGE"].ToString().Trim() == strMENGE.ToString())
                            {



                                foreach (DataRow dr in dtBarCodeInfo.Rows)
                                {
                                    if (dr["MATNR"].ToString().Trim() == strMATNR &&
                                        dr["DACOD"].ToString().Trim() == strDACOD &&
                                        dr["LIFNR"].ToString().Trim() == strLIFNR &&
                                        dr["LOCOD"].ToString().Trim() == strLOCOD &&
                                        dr["SERNO"].ToString().Trim() == strSERNO &&
                                        (dr["TASKID"].ToString().Trim() == strTASKID || strTASKID=="") &&
                                        //dr["MENGE"].ToString().Trim() == strMENGE.ToString() &&
                                        Convert.ToInt32(dr["ALQTY"].ToString().Trim()) == 0)
                                    {
                                        dr["LOCAT"] = strLocat;
                                        //dr["ALQTY"] = dr["MENGE"];
                                        //数据更新刷入数据
                                        dr["ALQTY"] = strMENGE.ToString();

                                        //1、调用AGV货架反面接口
                                        Agv_shelfMaterialRenewal head1 = new Agv_shelfMaterialRenewal
                                        {
                                            plant = Werks, //所属厂区 32 (必填)
                                            storage = Lgort, //仓库别 32 (必填)
                                            shelf_id = strMARNO, //货架编号 32 (非必填)
                                            action = "ADD", //操作行为 32 (必填)(ADD-入储；DEL- 出储)
                                            location = strLocat, //储位 32 (非必填)
                                            location_detail = new List<LocationDetailItem>
                                    {
                                        new LocationDetailItem
                                        {
                                            pn = dr["MATNR"].ToString().Trim(),
                                            version = dr["CHARG"].ToString().Trim(),
                                            vendor_code = dr["LIFNR"].ToString().Trim(),
                                            date_code = dr["DACOD"].ToString().Trim(),
                                            quantity = strMENGE
                                        }
                                    }
                                        };
                                        string strParam1 = JsonConvert.SerializeObject(head1);
                                        string strResult1 = objAGVApi.AGVHttpRequest(strStoragetype + "In", "shelfMaterialRenewal", strParam1);

                                    }
                                }
                            }
                            else
                            {
                                stsWarning.Text = "数量不匹配！";
                                SetErrNotice();
                                txtLocat.Text = string.Empty;
                                return;
                            }
                        }
                        objAGVStorageIn.UpdateLocation(strWerks, strLgort, strLocat);

                        stsWarning.Text = "Add OK";
                        SetOKNotice();
                        ShowDataGridBarCodeInfo();
                        ChangeLocatToBarcode();
                        

                        #endregion


                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    ChangeLocatToBarcode();
                    return;
                }
            }
        }
        #endregion

        #region btnEndJobs_Click
        private void btnEndJobs_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            try
            {
                DialogResult result = MessageBox.Show("确认结束任务？", "确认", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    strreqCode = strreqCode + 1; //任务序号累加

                    Agv_endTask head = new Agv_endTask
                    {
                        plant = Werks, //所属厂区 32 (必填)
                        storage = Lgort, //仓库别 32 (必填)
                        work_station = Workstation, //工作站别 32 (必填)
                        task_id = strTaskNo, //任务编号 32 (必填)
                        task_type = "", //任务类型 32 (非必填)
                        task_sequence = strreqCode, //任务序号 (必填)
                    };
                    string strParam = JsonConvert.SerializeObject(head);
                    string strResult = string.Empty;
                    if (strStoragetype == "CombineStock")
                    {
                        strResult = objAGVApi.AGVHttpRequest(strStoragetype, "endTask", strParam);
                    }
                    else
                    {
                        strResult = objAGVApi.AGVHttpRequest(strStoragetype + "In", "endTask", strParam);
                    }

                    if (!string.IsNullOrEmpty(strResult))
                    {
                        endTaskTaskData response = JsonConvert.DeserializeObject<endTaskTaskData>(strResult);
                        string strtask_id = response.task_id;
                        int ittask_sequence = response.task_status;
                        int strtask_sequence = response.task_sequence;
                        if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                        {
                            //库存正式入储WHITM表，更新WHDWN表单据数量，清除dtDocumentInfo、dtBarCodeInfo
                            if (objAGVStorageIn.Agv_AddStorageInData(dtBarCodeInfo, dtDocumentInfo, strShelfsize))
                            {
                                //删除料架调度中间表数据
                                if (objAGVStorageIn.DeleteAGVShelf(strWerks, strLgort, strTaskNo, strWorkstation))
                                {
                                    if (strStoragetype == "CombineStock")
                                    {
                                        objAGVStorageIn.DeleteCombineStockIn(strWerks, strLgort);
                                    }

                                    dtAGVOrder.Clear();
                                    dtDocumentInfo.Clear();
                                    dtBarCodeInfo.Clear();
                                    ShowDataGridAGVOrder();
                                    ShowDataGridDocumentInfo();
                                    ShowDataGridBarCodeInfo();

                                    strWorkstation = ""; //清空工作站
                                    strStoragetype = ""; //清空入库类型
                                    strShelfsize = ""; //清空料架尺寸
                                    strShelftype = ""; //清空料架类型
                                    strAGVSTATE = ""; //清空货架使用状态
                                    strTaskNo = ""; //清空任务单号
                                    strreqCode = 0; //清空任务序号
                                    strMARNO = ""; //清空料架编号

                                    ChangeLocatToBarcode();
                                    this.gbBasicOption.Enabled = true;
                                    this.gbAGVOption.Enabled = false;
                                    stsWarning.Text = "结束任务成功";
                                    SetOKNotice();
                                }
                            }
                            else
                            {
                                stsWarning.Text = "入库失败";
                                SetErrNotice();
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "响应错误，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                            SetErrNotice();
                            return;
                        }
                    }
                    else 
                    {
                        stsWarning.Text = "调用接口失败, 请稍后重试！";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #endregion

        #region Sound Remind
        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
        }

        public void SetOKNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\BIU.wav");
            sp.Play();
        }

        public void ChangeBarcodeToLocat()
        {
            this.txtBarCode.Text = "";
            this.txtBarCode.Enabled = false;
            this.txtLocat.Text = "";
            this.txtLocat.Enabled = true;
            this.txtLocat.Focus();
        }

        public void ChangeLocatToBarcode()
        {
            this.txtBarCode.Text = "";
            this.txtBarCode.Enabled = true;
            this.txtLocat.Text = "";
            this.txtLocat.Enabled = false;
            this.txtBarCode.Focus();
        }
        #endregion

        #region ShowStorageInData 累加实物数量，到SAP单据中
        private bool ShowStorageInData(string strLocat, string strMatnr, string strLifnr, int intMenge, string strVedat, string strDacod, string strLocod, string strExpdate, string strTaskid)
        {
            bool bol = true;
            DataTable dtTemp = new DataTable();
            dtTempNew.Clear();
            string strIndat = DateTime.Now.ToString("yyyyMMdd");

            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
            dtTemp = objStorageIn.QuerySapLineInData_AGV(strMatnr, strLifnr, strLocat, strIndat, strVedat, strDacod, strLocod, false, strExpdate, strTaskid);

            if (dtTemp == null || dtTemp.Rows.Count <= 0)
            {
                stsWarning.Text = "无SAP单据";
                SetErrNotice();
                bol = false;
                return bol;
            }

            int intTotal = 0;
            for (int h = 0; h < dtTemp.Rows.Count; h++)
            { 
                intTotal = intTotal + Convert.ToInt32(dtTemp.Rows[h]["MENGE"].ToString()); //单据总数量
            }
            if (intMenge > intTotal) //实物数量大于单据数量
            {
                stsWarning.Text = "请确认单据数量";
                SetErrNotice();
                bol = false;
                return bol;
            }

            foreach (DataRow row in dtTemp.Rows)
            {
                DataRow newRow = dtTempNew.NewRow();
                newRow["WERKS"] = row["WERKS"];
                newRow["LGORT"] = row["LGORT"];
                newRow["INSMK"] = row["INSMK"];
                newRow["CHARG"] = row["CHARG"];
                newRow["MBLNR"] = row["MBLNR"];
                newRow["ZEILE"] = row["ZEILE"];
                newRow["EBELN"] = row["EBELN"];
                newRow["INDAT"] = row["INDAT"];
                newRow["MATNR"] = row["MATNR"];
                newRow["MENGE"] = row["MENGE"];
                newRow["ALQTY"] = row["ALQTY"];
                newRow["INSMK"] = row["INSMK"];
                dtTempNew.Rows.Add(newRow);
            }

            foreach (DataRow dr in dtTempNew.Rows)
            {
                if (intMenge != 0) //实物数量不为0
                {
                    int a = Convert.ToInt32(dr["MENGE"].ToString()); //单据数量
                    if (a <= intMenge) //单据数量小于等于扫描实物数量
                    {
                        intMenge = intMenge - a; //匹配完第一笔单据后，实物剩余数量
                        dr["ALQTY"] = (Convert.ToInt32(dr["ALQTY"].ToString()) + a).ToString();
                        dtDocumentInfo.Rows.Add(dr.ItemArray);

                        //更新dtBarCodeInfo
                        drBarCodeInfo["WERKS"] = dr["WERKS"].ToString().Trim();
                        drBarCodeInfo["LGORT"] = dr["LGORT"].ToString().Trim();
                        drBarCodeInfo["INSMK"] = dr["INSMK"].ToString().Trim();
                        drBarCodeInfo["CHARG"] = dr["CHARG"].ToString().Trim();
                        drBarCodeInfo["MBLNR"] = dr["MBLNR"].ToString().Trim();
                        drBarCodeInfo["ZEILE"] = dr["ZEILE"].ToString().Trim();
                        drBarCodeInfo["EBELN"] = dr["EBELN"].ToString().Trim();
                        drBarCodeInfo["INDAT"] = dr["INDAT"].ToString().Trim();
                        drBarCodeInfo["MENGE"] = a.ToString();
                        dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                    }
                    else
                    {
                        dr["ALQTY"] = intMenge.ToString();
                        intMenge = 0;
                        //dtTemp.AcceptChanges();
                        dtDocumentInfo.Rows.Add(dr.ItemArray);

                        //更新dtBarCodeInfo
                        drBarCodeInfo["WERKS"] = dr["WERKS"].ToString().Trim();
                        drBarCodeInfo["LGORT"] = dr["LGORT"].ToString().Trim();
                        drBarCodeInfo["INSMK"] = dr["INSMK"].ToString().Trim();
                        drBarCodeInfo["CHARG"] = dr["CHARG"].ToString().Trim();
                        drBarCodeInfo["MBLNR"] = dr["MBLNR"].ToString().Trim();
                        drBarCodeInfo["ZEILE"] = dr["ZEILE"].ToString().Trim();
                        drBarCodeInfo["EBELN"] = dr["EBELN"].ToString().Trim();
                        drBarCodeInfo["INDAT"] = dr["INDAT"].ToString().Trim();
                        drBarCodeInfo["MENGE"] = dr["ALQTY"].ToString().Trim();
                        dtBarCodeInfo.Rows.Add(drBarCodeInfo.ItemArray);
                    }
                }
                else
                {
                    break;
                }
            }
            return bol;
        }
        #endregion

        #region 初始化dtTempNew Table
        public void GetdtTempNew()
        {
            dtTempNew = new DataTable();
            dtTempNew.Columns.Add("WERKS");
            dtTempNew.Columns.Add("LGORT");
            dtTempNew.Columns.Add("CHARG");
            dtTempNew.Columns.Add("MBLNR");
            dtTempNew.Columns.Add("ZEILE");
            dtTempNew.Columns.Add("EBELN");
            dtTempNew.Columns.Add("INDAT");
            dtTempNew.Columns.Add("MATNR");
            dtTempNew.Columns.Add("MENGE");
            dtTempNew.Columns.Add("ALQTY");
            dtTempNew.Columns.Add("INSMK");
        }
        #endregion

        #region 初始化DocumentInfo Table
        public void GetDocumentInfo()
        {
            dtDocumentInfo = new DataTable();
            dtDocumentInfo.Columns.Add("WERKS");
            dtDocumentInfo.Columns.Add("LGORT");
            dtDocumentInfo.Columns.Add("CHARG");
            dtDocumentInfo.Columns.Add("MBLNR");
            dtDocumentInfo.Columns.Add("ZEILE");
            dtDocumentInfo.Columns.Add("EBELN");
            dtDocumentInfo.Columns.Add("INDAT");
            dtDocumentInfo.Columns.Add("MATNR");
            dtDocumentInfo.Columns.Add("MENGE");
            dtDocumentInfo.Columns.Add("ALQTY");
            dtDocumentInfo.Columns.Add("INSMK");

            //dtDocumentInfo = new DataTable();
            //dtDocumentInfo.Columns.Add("ExpiryDate");
            //dtDocumentInfo.Columns.Add("MAXEXP");
            //dtDocumentInfo.Columns.Add("TASKID");
            //dtDocumentInfo.Columns.Add("IQCRMAK1");
            //dtDocumentInfo.Columns.Add("MANDT");
            //dtDocumentInfo.Columns.Add("COMCD");
            //dtDocumentInfo.Columns.Add("WERKS");
            //dtDocumentInfo.Columns.Add("LGORT");
            //dtDocumentInfo.Columns.Add("LOCAT");
            //dtDocumentInfo.Columns.Add("MATNR");
            //dtDocumentInfo.Columns.Add("INSMK");
            //dtDocumentInfo.Columns.Add("CHARG");
            //dtDocumentInfo.Columns.Add("MENGE");
            //dtDocumentInfo.Columns.Add("ALQTY");
            //dtDocumentInfo.Columns.Add("MBLNR");
            //dtDocumentInfo.Columns.Add("ZEILE");
            //dtDocumentInfo.Columns.Add("EBELN");
            //dtDocumentInfo.Columns.Add("LIFNR");
            //dtDocumentInfo.Columns.Add("RMANO");
            //dtDocumentInfo.Columns.Add("OMBLNR");
            //dtDocumentInfo.Columns.Add("MRGID");
            //dtDocumentInfo.Columns.Add("KOSTL");
            //dtDocumentInfo.Columns.Add("ARBPL");
            //dtDocumentInfo.Columns.Add("TRNTP");
            //dtDocumentInfo.Columns.Add("RMAK1");
            //dtDocumentInfo.Columns.Add("INDAT");
            //dtDocumentInfo.Columns.Add("KDMAT");
            //dtDocumentInfo.Columns.Add("SERNO");
            //dtDocumentInfo.Columns.Add("GRLOC");
            //dtDocumentInfo.Columns.Add("DACOD");
            //dtDocumentInfo.Columns.Add("VEDAT");
            //dtDocumentInfo.Columns.Add("INSPT");
        }
        #endregion

        #region 初始化BarCodeInfo Table
        public void GetBarCodeInfo()
        {
            dtBarCodeInfo = new DataTable();
            dtBarCodeInfo.Columns.Add("MANDT");
            dtBarCodeInfo.Columns.Add("COMCD");
            dtBarCodeInfo.Columns.Add("WERKS");
            dtBarCodeInfo.Columns.Add("LGORT");
            dtBarCodeInfo.Columns.Add("MARNO");
            dtBarCodeInfo.Columns.Add("FTYPE");
            dtBarCodeInfo.Columns.Add("LOCTYPE");
            dtBarCodeInfo.Columns.Add("LOCAT");
            dtBarCodeInfo.Columns.Add("MATNR");
            dtBarCodeInfo.Columns.Add("INSMK");
            dtBarCodeInfo.Columns.Add("CHARG");
            dtBarCodeInfo.Columns.Add("MENGE");
            dtBarCodeInfo.Columns.Add("ALQTY");
            dtBarCodeInfo.Columns.Add("ITEMSTATES");
            dtBarCodeInfo.Columns.Add("STOCSTATES");
            dtBarCodeInfo.Columns.Add("MBLNR");
            dtBarCodeInfo.Columns.Add("ZEILE");
            dtBarCodeInfo.Columns.Add("EBELN");
            dtBarCodeInfo.Columns.Add("LIFNR");
            dtBarCodeInfo.Columns.Add("RMANO");
            dtBarCodeInfo.Columns.Add("OMBLNR");
            dtBarCodeInfo.Columns.Add("MRGID");
            dtBarCodeInfo.Columns.Add("KOSTL");
            dtBarCodeInfo.Columns.Add("ARBPL");
            dtBarCodeInfo.Columns.Add("TRNTP");
            dtBarCodeInfo.Columns.Add("RMAK1");
            dtBarCodeInfo.Columns.Add("INDAT");
            dtBarCodeInfo.Columns.Add("KDMAT");
            dtBarCodeInfo.Columns.Add("SERNO");
            dtBarCodeInfo.Columns.Add("GRLOC");
            dtBarCodeInfo.Columns.Add("DACOD");
            dtBarCodeInfo.Columns.Add("VEDAT");
            dtBarCodeInfo.Columns.Add("LOCOD");
            dtBarCodeInfo.Columns.Add("EXPDAT");
            dtBarCodeInfo.Columns.Add("TASKID");
            dtBarCodeInfo.Columns.Add("MAXEXP");
        }
        #endregion

        #region 窗口关闭确认
        private void StorageIn_AGV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(strTaskNo))
            {
                MessageBox.Show("At work,Taskid:" + strTaskNo);
                SetErrNotice();
                e.Cancel = true;
            }
        }
        #endregion
    }
}
