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
using QCI_QWMS_Alim;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QWMS
{
    public partial class Alim_StorageOut_OnLineOut_Add : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strMatnrSum = "";
        public DataTable dtData = new DataTable();
        public DataTable dtDataWhdwn = new DataTable();
        public DataTable dtDateAlitm = new DataTable();
        public DataTable dtDataAdd = new DataTable();
        public DataTable dtAddDocToSAP;
        public QCI.QWMS.Alim objAlim;

        #region
        public string Mandt
        {
            get { return strMandt; }
            set { strMandt = value; }
        }
        public string Usrnm
        {
            get { return strUsrnm; }
            set { strUsrnm = value; }
        }
        public string Comcd
        {
            get { return strComcd; }
            set { strComcd = value; }
        }
        public string Progid
        {
            get { return strProgid; }
            set { strProgid = value; }
        }
        public string Werks
        {
            get { return strWerks; }
            set { strWerks = value; }
        }
        public string Lgort
        {
            get { return strLgort; }
            set { strLgort = value; }
        }
        public string Mblnr
        {
            get { return strMblnr; }
            set { strMblnr = value; }
        }
        #endregion

        public Alim_StorageOut_OnLineOut_Add(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            try
            {
                objAlim = new QCI.QWMS.Alim(UserData, Progid);
                //检查权限
                if (!objAlim.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDataGridWhdwn();
                    ShowDataGridAlitm();
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
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            try
            {
                dtTemp = objAlimStorageInSMT.GetPlant();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["WERKS"].ToString());
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
            DataTable dtTemp = new DataTable();
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAlimStorageInSMT.GetLgort(strWerks);
                }
                else
                {
                    dtTemp = objAlimStorageInSMT.GetLgort("");
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["LGORT"].ToString());
                        if (dtTemp.Rows[i]["LGORT"].ToString() == strLgort && strLgort != "")
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

        #region ShowDataGridWhdwn
        public void ShowDataGridWhdwn()
        {
            dgvDataSum.AutoGenerateColumns = false;            
            dgvDataSum.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvMANDT = new DataGridViewTextBoxColumn();
                dgvMANDT.DataPropertyName = "MANDT";
                dgvMANDT.HeaderText = "MANDT";
                dgvMANDT.ReadOnly = true;
                dgvMANDT.Width = 50;
                dgvDataSum.Columns.Add(dgvMANDT);

                DataGridViewTextBoxColumn dgvCOMCD = new DataGridViewTextBoxColumn();
                dgvCOMCD.DataPropertyName = "COMCD";
                dgvCOMCD.HeaderText = "公司别";
                dgvCOMCD.Width = 80;
                dgvCOMCD.ReadOnly = true;
                dgvDataSum.Columns.Add(dgvCOMCD);

                DataGridViewTextBoxColumn dgvWERKS = new DataGridViewTextBoxColumn();
                dgvWERKS.DataPropertyName = "WERKS";
                dgvWERKS.HeaderText = "厂区";
                dgvWERKS.Width = 80;
                dgvWERKS.ReadOnly = true;
                dgvDataSum.Columns.Add(dgvWERKS);

                DataGridViewTextBoxColumn dgvLGORT = new DataGridViewTextBoxColumn();
                dgvLGORT.DataPropertyName = "LGORT";
                dgvLGORT.HeaderText = "仓别";
                dgvLGORT.ReadOnly = true;
                dgvLGORT.Width = 80;
                dgvDataSum.Columns.Add(dgvLGORT);

                DataGridViewTextBoxColumn dgvMATNR = new DataGridViewTextBoxColumn();
                dgvMATNR.DataPropertyName = "MATNR";
                dgvMATNR.HeaderText = "料号";
                dgvMATNR.ReadOnly = true;
                dgvMATNR.Width = 100;
                dgvDataSum.Columns.Add(dgvMATNR);

                DataGridViewTextBoxColumn dgvCHARG = new DataGridViewTextBoxColumn();
                dgvCHARG.DataPropertyName = "CHARG";
                dgvCHARG.HeaderText = "CHARG";
                dgvCHARG.ReadOnly = true;
                dgvCHARG.Width = 50;
                dgvDataSum.Columns.Add(dgvCHARG);

                DataGridViewTextBoxColumn dgvMENGE = new DataGridViewTextBoxColumn();
                dgvMENGE.DataPropertyName = "MENGE";
                dgvMENGE.HeaderText = "预出库的数量";
                dgvMENGE.ReadOnly = true;
                dgvMENGE.Width = 80;
                dgvDataSum.Columns.Add(dgvMENGE);

                DataGridViewTextBoxColumn dgvRELMENGE = new DataGridViewTextBoxColumn();
                dgvRELMENGE.DataPropertyName = "RELMENGE";
                dgvRELMENGE.HeaderText = "实际出库数量";
                dgvRELMENGE.ReadOnly = true;
                dgvRELMENGE.Width = 80;
                dgvDataSum.Columns.Add(dgvRELMENGE);

                DataGridViewTextBoxColumn dgvINSMK = new DataGridViewTextBoxColumn();
                dgvINSMK.DataPropertyName = "INSMK";
                dgvINSMK.HeaderText = "INSMK";
                dgvINSMK.ReadOnly = true;
                dgvINSMK.Width = 50;
                dgvDataSum.Columns.Add(dgvINSMK);

                DataGridViewTextBoxColumn dgvBWART = new DataGridViewTextBoxColumn();
                dgvBWART.DataPropertyName = "BWART";
                dgvBWART.HeaderText = "异动代码";
                dgvBWART.ReadOnly = true;
                dgvBWART.Width = 80;
                dgvDataSum.Columns.Add(dgvBWART);

                DataGridViewTextBoxColumn dgvLIFNR = new DataGridViewTextBoxColumn();
                dgvLIFNR.DataPropertyName = "LIFNR";
                dgvLIFNR.HeaderText = "厂商代码";
                dgvLIFNR.ReadOnly = true;
                dgvLIFNR.Width = 100;
                dgvDataSum.Columns.Add(dgvLIFNR);

                DataGridViewTextBoxColumn dgvKOSTL = new DataGridViewTextBoxColumn();
                dgvKOSTL.DataPropertyName = "KOSTL";
                dgvKOSTL.HeaderText = "部门代码";
                dgvKOSTL.ReadOnly = true;
                dgvKOSTL.Width = 100;
                dgvDataSum.Columns.Add(dgvKOSTL);

                DataGridViewTextBoxColumn dgvDACOD = new DataGridViewTextBoxColumn();
                dgvDACOD.DataPropertyName = "DACOD";
                dgvDACOD.HeaderText = "DACOD";
                dgvDACOD.ReadOnly = true;
                dgvDACOD.Width = 100;
                dgvDataSum.Columns.Add(dgvDACOD);

                DataGridViewTextBoxColumn dgvLOADID = new DataGridViewTextBoxColumn();
                dgvLOADID.DataPropertyName = "LOADID";
                dgvLOADID.HeaderText = "LOADID";
                dgvLOADID.ReadOnly = true;
                dgvLOADID.Width = 100;
                dgvDataSum.Columns.Add(dgvLOADID);

                DataGridViewTextBoxColumn dgvSERNO = new DataGridViewTextBoxColumn();
                dgvSERNO.DataPropertyName = "SERNO";
                dgvSERNO.HeaderText = "SERNO";
                dgvSERNO.ReadOnly = true;
                dgvSERNO.Width = 100;
                dgvDataSum.Columns.Add(dgvSERNO);

                dgvDataSum.DataSource = dtDataWhdwn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridWhdwn()");
            }
        }
        #endregion

        #region ShowDataGridAlitm
        public void ShowDataGridAlitm()
        {
            dgvDataDetail.AutoGenerateColumns = false;
            dgvDataDetail.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvMANDT = new DataGridViewTextBoxColumn();
                dgvMANDT.DataPropertyName = "MANDT";
                dgvMANDT.HeaderText = "MANDT";
                dgvMANDT.ReadOnly = true;
                dgvMANDT.Width = 50;
                dgvDataDetail.Columns.Add(dgvMANDT);

                DataGridViewTextBoxColumn dgvCOMCD = new DataGridViewTextBoxColumn();
                dgvCOMCD.DataPropertyName = "COMCD";
                dgvCOMCD.HeaderText = "公司别";
                dgvCOMCD.Width = 80;
                dgvCOMCD.ReadOnly = true;
                dgvDataDetail.Columns.Add(dgvCOMCD);

                DataGridViewTextBoxColumn dgvWERKS = new DataGridViewTextBoxColumn();
                dgvWERKS.DataPropertyName = "WERKS";
                dgvWERKS.HeaderText = "厂区";
                dgvWERKS.Width = 80;
                dgvWERKS.ReadOnly = true;
                dgvDataDetail.Columns.Add(dgvWERKS);

                DataGridViewTextBoxColumn dgvLGORT = new DataGridViewTextBoxColumn();
                dgvLGORT.DataPropertyName = "LGORT";
                dgvLGORT.HeaderText = "仓别";
                dgvLGORT.ReadOnly = true;
                dgvLGORT.Width = 80;
                dgvDataDetail.Columns.Add(dgvLGORT);

                DataGridViewTextBoxColumn dgvMATNR = new DataGridViewTextBoxColumn();
                dgvMATNR.DataPropertyName = "MATNR";
                dgvMATNR.HeaderText = "料号";
                dgvMATNR.ReadOnly = true;
                dgvMATNR.Width = 100;
                dgvDataDetail.Columns.Add(dgvMATNR);

                DataGridViewTextBoxColumn dgvMBLNR = new DataGridViewTextBoxColumn();
                dgvMBLNR.DataPropertyName = "MBLNR";
                dgvMBLNR.HeaderText = "编号";
                dgvMBLNR.ReadOnly = true;
                dgvMBLNR.Width = 150;
                dgvDataDetail.Columns.Add(dgvMBLNR);

                DataGridViewTextBoxColumn dgvLOCAT = new DataGridViewTextBoxColumn();
                dgvLOCAT.DataPropertyName = "LOCAT";
                dgvLOCAT.HeaderText = "储位";
                dgvLOCAT.ReadOnly = true;
                dgvLOCAT.Width = 100;
                dgvDataDetail.Columns.Add(dgvLOCAT);

                //DataGridViewTextBoxColumn dgvCONTRNO = new DataGridViewTextBoxColumn();
                //dgvCONTRNO.DataPropertyName = "CONTRNO";
                //dgvCONTRNO.HeaderText = "柜号";
                //dgvCONTRNO.ReadOnly = true;
                //dgvCONTRNO.Width = 80;
                //dgvDataDetail.Columns.Add(dgvCONTRNO);

                DataGridViewTextBoxColumn dgvMENGE = new DataGridViewTextBoxColumn();
                dgvMENGE.DataPropertyName = "MENGE";
                dgvMENGE.HeaderText = "库存储位数量";
                dgvMENGE.ReadOnly = true;
                dgvMENGE.Width = 80;
                dgvDataDetail.Columns.Add(dgvMENGE);

                DataGridViewTextBoxColumn dgvADDQTY = new DataGridViewTextBoxColumn();
                dgvADDQTY.DataPropertyName = "ADDQTY";
                dgvADDQTY.HeaderText = "加扣数量";
                dgvADDQTY.ReadOnly = true;
                dgvADDQTY.Width = 80;
                dgvDataDetail.Columns.Add(dgvADDQTY);

                DataGridViewTextBoxColumn dgvCRNAM = new DataGridViewTextBoxColumn();
                dgvCRNAM.DataPropertyName = "CRNAM";
                dgvCRNAM.HeaderText = "CRNAM";
                dgvCRNAM.ReadOnly = true;
                dgvCRNAM.Width = 100;
                dgvDataDetail.Columns.Add(dgvCRNAM);

                DataGridViewTextBoxColumn dgvCRDAT = new DataGridViewTextBoxColumn();
                dgvCRDAT.DataPropertyName = "CRDAT";
                dgvCRDAT.HeaderText = "CRDAT";
                dgvCRDAT.ReadOnly = true;
                dgvCRDAT.Width = 150;
                dgvDataDetail.Columns.Add(dgvCRDAT);

                dgvDataDetail.DataSource = dtDataAdd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridWhdwn()");
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cmbWerks.Items[cmbWerks.SelectedIndex].ToString() == "")
            {
                stsWarning.Text = "请选择厂区！";
                return;
            }
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            strMblnr = txtMblnr.Text.Trim().ToString();  //维护的十码扣账编号
            if (strMblnr == "")
            {
                stsWarning.Text = "请维护扣账编号！";
                return;
            }
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            //dtData = objAlimStorageInSMT.GetStorageOutData(strMandt, strComcd, strWerks, strLgort, strMblnr);            
            dtDataWhdwn = objAlimStorageInSMT.GetStorageOutDataWhdwn(strMandt, strComcd, strWerks, strLgort, strMblnr);
            if (dtDataWhdwn.Rows.Count <= 0)
            {
                stsWarning.Text = "未获取到出库数据，请确认！";
                return;
            }
            //获取WHDWN中料号
            for (int i = 0; i < dtDataWhdwn.Rows.Count; i++)
            {
                strMatnr = ("'") + dtDataWhdwn.Rows[i]["MATNR"].ToString() + ("'") + (",") + strMatnr;
            }
            strMatnrSum = strMatnr.Substring(0, strMatnr.Length - 1);
            
            //Alitm库存，根据BULK(散料)、DACOD材料、普通料号优先级进行排序
            DataTable dtDataBulk = objAlimStorageInSMT.GetStorageOutDataAlitmBulk(strMandt, strComcd, strWerks, strLgort, strMatnrSum);
            DataTable dtDataDacod = objAlimStorageInSMT.GetStorageOutDataAlitmDacod(strMandt, strComcd, strWerks, strLgort, strMatnrSum);
            DataTable dtDataNull = objAlimStorageInSMT.GetStorageOutDataAlitm(strMandt, strComcd, strWerks, strLgort, strMatnrSum);
            dtDateAlitm = GetNewAlitmDataTable(dtDataBulk, dtDataDacod, dtDataNull);
            if (dtDateAlitm.Rows.Count <= 0)
            {
                stsWarning.Text = "没有库存，请仓库确认！";
                return;
            }
            ShowDataGridWhdwn();
            ShowDataGridAlitm();            

            //创建加扣DataTable dtDataAdd
            GetDataTableAddQty();
            for (int i = 0; i < dtDataWhdwn.Rows.Count; i++)
            {
                string srComcd = dtDataWhdwn.Rows[i]["COMCD"].ToString();
                string srWerks = dtDataWhdwn.Rows[i]["WERKS"].ToString();
                string srLgort = dtDataWhdwn.Rows[i]["LGORT"].ToString();
                string srMatnr = dtDataWhdwn.Rows[i]["MATNR"].ToString();
                string strKostl = dtDataWhdwn.Rows[i]["KOSTL"].ToString();
                //WHDWN表出库数量
                int srMenge = Convert.ToInt32(dtDataWhdwn.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(dtDataWhdwn.Rows[i]["OTQTY"].ToString());
                //待处理数量
                int intOtqty = srMenge;
                //初始化已处理数量
                int intAlqty = 0;
                //根据WHDWN数据查找Alitm库存信息
                DataRow[] drDataAlitm = dtDateAlitm.Select("COMCD='" + srComcd + "' AND WERKS='" + srWerks + "' AND LGORT='" + srLgort + "' AND MATNR='" + srMatnr + "' ");
                if (drDataAlitm.Length > 0)
                {
                    //判断现有库存是否足够
                    //计算库存总和
                    //var AlitmMengeSum = drDataAlitm.Sum(x => x.Field<int>("MENGE"));
                    foreach (DataRow dr in drDataAlitm)
                    {
                        //单个储位对应库存数量
                        int AlitmQty = Convert.ToInt32(dr["MENGE"].ToString());
                        //待处理数量(WHDWN数量)-单储位库存量
                        intOtqty = intOtqty - AlitmQty;
                        DataRow drAddQty = dtDataAdd.NewRow();
                        if (intOtqty > 0)
                        {
                            intAlqty = intAlqty + AlitmQty;
                            dtDataWhdwn.Rows[i]["RELMENGE"] = intAlqty;
                            drAddQty["MANDT"] = dr["MANDT"].ToString();
                            drAddQty["COMCD"] = srComcd;
                            drAddQty["WERKS"] = srWerks;
                            drAddQty["LGORT"] = srLgort;
                            drAddQty["PRI"] = dr["PRI"].ToString();
                            drAddQty["SUBPRI"] = dr["SUBPRI"].ToString();
                            drAddQty["DIDNO"] = "";
                            drAddQty["MBLNR"] = dtDataWhdwn.Rows[i]["MBLNR"].ToString();
                            drAddQty["ZEILE"] = "";
                            drAddQty["LOCAT"] = dr["LOCAT"].ToString();                           
                            drAddQty["MATNR"] = dr["MATNR"].ToString();
                            drAddQty["CHARG"] = dr["CHARG"].ToString();
                            drAddQty["MENGE"] = dr["MENGE"].ToString();
                            drAddQty["ADDQTY"] = 0;
                            drAddQty["INSMK"] = dtDataWhdwn.Rows[i]["INSMK"].ToString();
                            drAddQty["BWART"] = dtDataWhdwn.Rows[i]["BWART"].ToString();
                            drAddQty["LIFNR"] = dr["LIFNR"].ToString();
                            drAddQty["KOSTL"] = dtDataWhdwn.Rows[i]["KOSTL"].ToString();
                            drAddQty["DACOD"] = dr["DACOD"].ToString();
                            drAddQty["LOCOD"] = dr["LOCOD"].ToString();
                            drAddQty["SERNO"] = dtDataWhdwn.Rows[i]["SERNO"].ToString();
                            drAddQty["CRNAM"] = dr["CRNAM"].ToString();
                            drAddQty["CRDAT"] = dr["CRDAT"].ToString();
                            drAddQty["FLAGE"] = "N";
                            dtDataAdd.Rows.Add(drAddQty);
                        }
                        else if (intOtqty < 0) //加扣
                        {
                            intAlqty = intAlqty + AlitmQty;
                            dtDataWhdwn.Rows[i]["RELMENGE"] = intAlqty;
                            drAddQty["MANDT"] = dr["MANDT"].ToString();
                            drAddQty["COMCD"] = srComcd;
                            drAddQty["WERKS"] = srWerks;
                            drAddQty["LGORT"] = srLgort;
                            drAddQty["PRI"] = dr["PRI"].ToString();
                            drAddQty["SUBPRI"] = dr["SUBPRI"].ToString();
                            drAddQty["DIDNO"] = "";
                            drAddQty["MBLNR"] = dtDataWhdwn.Rows[i]["MBLNR"].ToString();
                            drAddQty["ZEILE"] = "";
                            drAddQty["LOCAT"] = dr["LOCAT"].ToString();                            
                            drAddQty["MATNR"] = dr["MATNR"].ToString();
                            drAddQty["CHARG"] = dr["CHARG"].ToString();
                            drAddQty["MENGE"] = dr["MENGE"].ToString();
                            drAddQty["ADDQTY"] = System.Math.Abs(intOtqty);
                            drAddQty["INSMK"] = dtDataWhdwn.Rows[i]["INSMK"].ToString();
                            drAddQty["BWART"] = dtDataWhdwn.Rows[i]["BWART"].ToString();
                            drAddQty["LIFNR"] = dr["LIFNR"].ToString();
                            drAddQty["KOSTL"] = dtDataWhdwn.Rows[i]["KOSTL"].ToString();
                            drAddQty["DACOD"] = dr["DACOD"].ToString();
                            drAddQty["LOCOD"] = dr["LOCOD"].ToString();
                            drAddQty["SERNO"] = dtDataWhdwn.Rows[i]["SERNO"].ToString();
                            drAddQty["CRNAM"] = dr["CRNAM"].ToString();
                            drAddQty["CRDAT"] = dr["CRDAT"].ToString();
                            drAddQty["FLAGE"] = "N";
                            dtDataAdd.Rows.Add(drAddQty);
                            break;
                        }
                        else
                        {
                            intAlqty = intAlqty + AlitmQty;
                            dtDataWhdwn.Rows[i]["RELMENGE"] = intAlqty;
                            drAddQty["MANDT"] = dr["MANDT"].ToString();
                            drAddQty["COMCD"] = srComcd;
                            drAddQty["WERKS"] = srWerks;
                            drAddQty["LGORT"] = srLgort;
                            drAddQty["PRI"] = dr["PRI"].ToString();
                            drAddQty["SUBPRI"] = dr["SUBPRI"].ToString();
                            drAddQty["DIDNO"] = "";
                            drAddQty["MBLNR"] = dtDataWhdwn.Rows[i]["MBLNR"].ToString();
                            drAddQty["ZEILE"] = "";
                            drAddQty["LOCAT"] = dr["LOCAT"].ToString();                           
                            drAddQty["MATNR"] = dr["MATNR"].ToString();
                            drAddQty["CHARG"] = dr["CHARG"].ToString();
                            drAddQty["MENGE"] = dr["MENGE"].ToString();
                            drAddQty["ADDQTY"] = 0;
                            drAddQty["INSMK"] = dtDataWhdwn.Rows[i]["INSMK"].ToString();
                            drAddQty["BWART"] = dtDataWhdwn.Rows[i]["BWART"].ToString();
                            drAddQty["LIFNR"] = dr["LIFNR"].ToString();
                            drAddQty["KOSTL"] = dtDataWhdwn.Rows[i]["KOSTL"].ToString();
                            drAddQty["DACOD"] = dr["DACOD"].ToString();
                            drAddQty["LOCOD"] = dr["LOCOD"].ToString();
                            drAddQty["SERNO"] = dtDataWhdwn.Rows[i]["SERNO"].ToString();
                            drAddQty["CRNAM"] = dr["CRNAM"].ToString();
                            drAddQty["CRDAT"] = dr["CRDAT"].ToString();
                            drAddQty["FLAGE"] = "N";
                            dtDataAdd.Rows.Add(drAddQty);
                            break;
                        }
                    }
                    ShowDataGridAlitm();
                    //颜色标识出库存数量以及加扣扣账数量
                    dgvDataDetail.Columns[8].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvDataDetail.Columns[6].DefaultCellStyle.BackColor = Color.LightBlue;

                    ShowDataGridWhdwn();
                    //颜色标识出库存数量以及加扣扣账数量
                    //dgvDataSum.Columns[7].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    //stsWarning.Text = "没有可出库库存，请确认！";
                    //return;
                    dtDataWhdwn.Rows[i]["RELMENGE"] = 0;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            AlimStorageOutInfo objAlimStorageOutInfo = new AlimStorageOutInfo();
            try
            {                
                //初始化传给SAP的DataTable
                GetDataTableToSAP();
                btnSave.Enabled = false;
                chkPRI.Enabled = false;
                //储位信息存放出库中间表、库存状态更新待下架、WHDWN更新、加扣记录
                bool strresult = objAlimStorageInSMT.StorageOut_OnLineOut_Add(dtDataAdd, chkPRI.Checked);
                if (strresult == false)
                {
                    stsWarning.Text = "存放出库中间表,更新库存为待下架状态失败！";
                    return;
                }
                //同步出库信息到QMS，调用QMS OutStore API接口
                #region 出库数据获取节点转换为JSON
                AlimStorageOutInfo list = new AlimStorageOutInfo
                {
                    Step = "GetDemandData",
                };

                List<AlimList> listItem = (from t in dtDataAdd.AsEnumerable() select new AlimList
                {
                    WERKS = t.Field<string>("WERKS"),
                    LGORT = t.Field<string>("LGORT"),
                    GRPID = "",
                    DIDNO = "",
                    LOCAT = t.Field<string>("LOCAT"),
                    MBLNR = t.Field<string>("MBLNR"),
                    COSTCENTER = "",  //KOSTL
                    MATNR = t.Field<string>("MATNR"),
                    LIFNR = t.Field<string>("LIFNR"),
                    DACOD = t.Field<string>("DACOD"),
                    LOCOD = t.Field<string>("LOCOD"),
                    Line = "",
                    Side = "",
                    Machine = "",
                    SDTSlot = "",
                    SDTLr = ""
                }).ToList();                                                                  
                #endregion
                list.Alim = listItem;

                string strParam = JsonConvert.SerializeObject(list);
                string strUrl = "http://10.18.11.95/AlimApi/api/Alim/GetQWMSData";
                string Result = objAlimStorageInSMT.HttpPostByHttpWebRequest(strUrl, strParam);

                //解析回执{"ReturnMsg":"Y"}，{"ReturnMsg":"N"}
                JObject jo = (JObject)JsonConvert.DeserializeObject(Result);
                string ReturnMsg = jo["ReturnMsg"].ToString();
                if (ReturnMsg == "N")
                {
                    //更新FLAGE=N
                    stsWarning.Text = "出库数据同步QMS失败！";
                    return;
                }
                
                //获取加扣信息中，需要加扣(去除值等于0)的数据到dtAddDocToSAP
                int j = 0;
                foreach (DataRow dr in dtDataAdd.Rows)
                {
                    string srAddQty = dr["ADDQTY"].ToString();
                    string srMblnr_ZAPPID = dr["MBLNR"].ToString() + dr["MATNR"].ToString();
                    j = j + 1;
                    if (srAddQty != "0")
                    {
                        #region 将需要加扣的信息填到加扣表
                        string srZeile = (j).ToString().PadLeft(4, '0');
                        DataRow drN = dtAddDocToSAP.NewRow();
                        drN["MANDT"] = dr["MANDT"].ToString();   //集团代码
                        drN["ZAPPID"] = srMblnr_ZAPPID;   //扣账编号前十码+时间(秒)
                        drN["ZITEM"] = srZeile;   //工单
                        drN["WERKS"] = dr["WERKS"].ToString();   //厂区
                        drN["WERKS_I"] = "";   //接收厂区
                        drN["LGORT"] = dr["LGORT"].ToString();   //仓别
                        drN["LGORT_I"] = "";   //接收仓别
                        drN["MATNR"] = dr["MATNR"].ToString();   //料号
                        drN["MENGE"] = srAddQty;   //加扣数量
                        drN["MBLNR"] = "";   //扣账编号
                        drN["MJAHR"] = "";   //扣账年份
                        drN["FLAG"] = "";   //是否扣账成功标志
                        drN["MESSAGE"] = "";   //扣账返回信息（报错等或者扣账成功提醒）
                        drN["TXDAT"] = "";   //扣账当天日期
                        drN["TXTM"] = "";   //时间
                        drN["TXEMP"] = UserData.UserId;   //工号
                        if (dr["KOSTL"].ToString().Length == 4) //部门代码
                        {
                            drN["KOSTL"] = "";
                        }
                        else
                        {
                            drN["KOSTL"] = dr["KOSTL"].ToString();
                        }
                        drN["CHARG"] = dr["CHARG"].ToString();   //料号版本
                        drN["BWART"] = dr["BWART"].ToString();  //异动代码
                        drN["AUFNR"] = "";   //工单
                        dtAddDocToSAP.Rows.Add(drN);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

            //if (dtAddDocToSAP.Rows.Count > 0 && dtAddDocToSAP != null)
            //{
            //    #region 加扣扣账
            //    DataSet dsData = new DataSet();
            //    dsData.Tables.Add(dtAddDocToSAP);
            //    MM_Test.MM_Service obj = new QWMS.MM_Test.MM_Service(); //测试
            //    //MM.MM_Service obj = new QWMS.MM.MM_Service(); //正式
            //    //DataSet dsResultFromSAP = obj.Z_MM_RFC_POSTYCN("A", dsData);
            //    //DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];
            //    DataTable dtResultFromSAP = null;
            //    stsWarning.Text = "";
            //    if (dtResultFromSAP.Rows.Count > 0)
            //    {
            //        DataTable dtSuccess = dtResultFromSAP.Clone();
            //        DataRow[] drSuccess = dtResultFromSAP.Select("FLAG='Y'");
            //        DataRow[] drFail = dtResultFromSAP.Select("FLAG='N' ");

            //        #region  加扣成功
            //        if (drSuccess.Length > 0)
            //        {
            //            foreach (DataRow dr in drSuccess)
            //            {
            //                string strZappid = dr["ZAPPID"].ToString();
            //                string strItem = dr["ZITEM"].ToString();
            //                string strMblnr = dr["MBLNR"].ToString(); //加扣扣账编号
            //                string strMESSAGE = dr["MESSAGE"].ToString().Trim();
            //                string strFlag = dr["FLAG"].ToString().Trim();
            //                objAlimStorageInSMT.UpdateAgoutMblnr(strMblnr, strZappid); //更新加扣扣帐单号到RWMAK1栏位
            //                dtSuccess.Rows.Add(dr.ItemArray);
            //            }
            //            stsWarning.Text = "Sap扣账成功！";
            //        }
            //        #endregion

            //        #region 加扣失败
            //        if (drFail.Length > 0)
            //        {
            //            string matnrs = "";
            //            foreach (DataRow dr in drFail)
            //            {
            //                matnrs += dr["MATNR"].ToString() + ",";
            //                string strZappid = dr["ZAPPID"].ToString();
            //                string strMESSAGE = dr["MESSAGE"].ToString().Trim();
            //                objAlimStorageInSMT.UpdateAgoutMblnrError(strMESSAGE, strZappid); //更新错误信息到REMAK2栏位
            //            }
            //            stsWarning.Text = "Sap扣账失败！";
            //            MessageBox.Show(matnrs + "加扣失败");
            //        }
            //        #endregion
            //    }
            //    #endregion
            //}
        }

        #region 初始化传Sap加扣扣账DataTable
        public void GetDataTableToSAP()
        {
            dtAddDocToSAP = new DataTable();
            dtAddDocToSAP.Columns.Add("MANDT"); //集团代码
            dtAddDocToSAP.Columns.Add("ZAPPID"); //扣账编号
            dtAddDocToSAP.Columns.Add("ZITEM"); //工单
            dtAddDocToSAP.Columns.Add("WERKS"); //厂区
            dtAddDocToSAP.Columns.Add("WERKS_I"); //接收厂区
            dtAddDocToSAP.Columns.Add("LGORT"); //仓别
            dtAddDocToSAP.Columns.Add("LGORT_I"); //接收仓别
            dtAddDocToSAP.Columns.Add("MATNR"); //料号
            dtAddDocToSAP.Columns.Add("MENGE"); //加扣数量
            dtAddDocToSAP.Columns.Add("MBLNR"); //扣账编号
            dtAddDocToSAP.Columns.Add("MJAHR"); //扣账年份
            dtAddDocToSAP.Columns.Add("FLAG"); //是否扣账成功标志
            dtAddDocToSAP.Columns.Add("MESSAGE");  //扣账返回信息（报错等或者扣账成功提醒）
            dtAddDocToSAP.Columns.Add("TXDAT"); //扣账当天日期
            dtAddDocToSAP.Columns.Add("TXTM"); //时间
            dtAddDocToSAP.Columns.Add("TXEMP"); //工号
            dtAddDocToSAP.Columns.Add("KOSTL"); //部门代码
            dtAddDocToSAP.Columns.Add("CHARG"); //料号版本
            dtAddDocToSAP.Columns.Add("BWART"); //异动代码
            dtAddDocToSAP.Columns.Add("AUFNR"); //工单
        }
        #endregion
        
        #region 初始化AddQtyDataTable
        public void GetDataTableAddQty()
        {
            dtDataAdd = new DataTable();
            dtDataAdd.Columns.Add("MANDT");
            dtDataAdd.Columns.Add("COMCD");
            dtDataAdd.Columns.Add("WERKS");
            dtDataAdd.Columns.Add("LGORT");
            dtDataAdd.Columns.Add("PRI");
            dtDataAdd.Columns.Add("SUBPRI");
            dtDataAdd.Columns.Add("DIDNO");
            dtDataAdd.Columns.Add("MBLNR");
            dtDataAdd.Columns.Add("ZEILE");
            dtDataAdd.Columns.Add("LOCAT");            
            dtDataAdd.Columns.Add("MATNR");
            dtDataAdd.Columns.Add("CHARG");
            dtDataAdd.Columns.Add("MENGE");
            dtDataAdd.Columns.Add("ADDQTY");
            dtDataAdd.Columns.Add("INSMK");
            dtDataAdd.Columns.Add("BWART");
            dtDataAdd.Columns.Add("LIFNR");
            dtDataAdd.Columns.Add("KOSTL");
            dtDataAdd.Columns.Add("DACOD");
            dtDataAdd.Columns.Add("LOCOD");
            dtDataAdd.Columns.Add("SERNO");
            dtDataAdd.Columns.Add("CRNAM");
            dtDataAdd.Columns.Add("CRDAT");
            dtDataAdd.Columns.Add("FLAGE");
        }
        #endregion

        #region 构建Alitm新的结果集
        public DataTable GetNewAlitmDataTable(DataTable dtDataBulk, DataTable dtDataDacod, DataTable dtDataNull)
        {
            DataTable newdt = new DataTable();
            try
            {
                newdt.Columns.Add("MANDT");
                newdt.Columns.Add("COMCD");
                newdt.Columns.Add("WERKS");
                newdt.Columns.Add("LGORT");
                newdt.Columns.Add("PRI");
                newdt.Columns.Add("SUBPRI");
                newdt.Columns.Add("MATNR");
                newdt.Columns.Add("MBLNR");
                newdt.Columns.Add("LOCAT");                
                newdt.Columns.Add("CHARG");
                newdt.Columns.Add("LIFNR");
                newdt.Columns.Add("DACOD");
                newdt.Columns.Add("LOCOD");
                newdt.Columns.Add("MENGE",System.Type.GetType("System.Int32"));
                newdt.Columns.Add("ADDQTY");
                newdt.Columns.Add("CRNAM");
                newdt.Columns.Add("CRDAT");

                if (dtDataBulk.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDataBulk.Rows.Count; i++)
                    {
                        DataRow dr = newdt.NewRow();
                        dr["MANDT"] = dtDataBulk.Rows[i]["MANDT"].ToString();
                        dr["COMCD"] = dtDataBulk.Rows[i]["COMCD"].ToString();
                        dr["WERKS"] = dtDataBulk.Rows[i]["WERKS"].ToString();
                        dr["LGORT"] = dtDataBulk.Rows[i]["LGORT"].ToString();
                        dr["PRI"] = dtDataBulk.Rows[i]["PRI"].ToString();
                        dr["SUBPRI"] = dtDataBulk.Rows[i]["SUBPRI"].ToString();
                        dr["MATNR"] = dtDataBulk.Rows[i]["MATNR"].ToString();
                        dr["MBLNR"] = dtDataBulk.Rows[i]["MBLNR"].ToString();
                        dr["LOCAT"] = dtDataBulk.Rows[i]["LOCAT"].ToString();                        
                        dr["CHARG"] = dtDataBulk.Rows[i]["CHARG"].ToString();
                        dr["LIFNR"] = dtDataBulk.Rows[i]["LIFNR"].ToString();
                        dr["DACOD"] = dtDataBulk.Rows[i]["DACOD"].ToString();
                        dr["LOCOD"] = dtDataBulk.Rows[i]["LOCOD"].ToString();
                        dr["MENGE"] = dtDataBulk.Rows[i]["MENGE"].ToString();
                        dr["ADDQTY"] = dtDataBulk.Rows[i]["ADDQTY"].ToString();
                        dr["CRNAM"] = dtDataBulk.Rows[i]["CRNAM"].ToString();
                        dr["CRDAT"] = dtDataBulk.Rows[i]["CRDAT"].ToString();
                        newdt.Rows.Add(dr);
                    }
                }
                if (dtDataDacod.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDataDacod.Rows.Count; i++)
                    {
                        DataRow dr = newdt.NewRow();
                        dr["MANDT"] = dtDataDacod.Rows[i]["MANDT"].ToString();
                        dr["COMCD"] = dtDataDacod.Rows[i]["COMCD"].ToString();
                        dr["WERKS"] = dtDataDacod.Rows[i]["WERKS"].ToString();
                        dr["LGORT"] = dtDataDacod.Rows[i]["LGORT"].ToString();
                        dr["PRI"] = dtDataDacod.Rows[i]["PRI"].ToString();
                        dr["SUBPRI"] = dtDataDacod.Rows[i]["SUBPRI"].ToString();
                        dr["MATNR"] = dtDataDacod.Rows[i]["MATNR"].ToString();
                        dr["MBLNR"] = dtDataDacod.Rows[i]["MBLNR"].ToString();
                        dr["LOCAT"] = dtDataDacod.Rows[i]["LOCAT"].ToString();                        
                        dr["CHARG"] = dtDataDacod.Rows[i]["CHARG"].ToString();
                        dr["LIFNR"] = dtDataDacod.Rows[i]["LIFNR"].ToString();
                        dr["DACOD"] = dtDataDacod.Rows[i]["DACOD"].ToString();
                        dr["LOCOD"] = dtDataDacod.Rows[i]["LOCOD"].ToString();
                        dr["MENGE"] = dtDataDacod.Rows[i]["MENGE"].ToString();
                        dr["ADDQTY"] = dtDataDacod.Rows[i]["ADDQTY"].ToString();
                        dr["CRNAM"] = dtDataDacod.Rows[i]["CRNAM"].ToString();
                        dr["CRDAT"] = dtDataDacod.Rows[i]["CRDAT"].ToString();
                        newdt.Rows.Add(dr);
                    }
                }
                if (dtDataNull.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDataNull.Rows.Count; i++)
                    {
                        DataRow dr = newdt.NewRow();
                        dr["MANDT"] = dtDataNull.Rows[i]["MANDT"].ToString();
                        dr["COMCD"] = dtDataNull.Rows[i]["COMCD"].ToString();
                        dr["WERKS"] = dtDataNull.Rows[i]["WERKS"].ToString();
                        dr["LGORT"] = dtDataNull.Rows[i]["LGORT"].ToString();
                        dr["PRI"] = dtDataNull.Rows[i]["PRI"].ToString();
                        dr["SUBPRI"] = dtDataNull.Rows[i]["SUBPRI"].ToString();
                        dr["MATNR"] = dtDataNull.Rows[i]["MATNR"].ToString();
                        dr["MBLNR"] = dtDataNull.Rows[i]["MBLNR"].ToString();
                        dr["LOCAT"] = dtDataNull.Rows[i]["LOCAT"].ToString();                        
                        dr["CHARG"] = dtDataNull.Rows[i]["CHARG"].ToString();
                        dr["LIFNR"] = dtDataNull.Rows[i]["LIFNR"].ToString();
                        dr["DACOD"] = dtDataNull.Rows[i]["DACOD"].ToString();
                        dr["LOCOD"] = dtDataNull.Rows[i]["LOCOD"].ToString();
                        dr["MENGE"] = dtDataNull.Rows[i]["MENGE"].ToString();
                        dr["ADDQTY"] = dtDataNull.Rows[i]["ADDQTY"].ToString();
                        dr["CRNAM"] = dtDataNull.Rows[i]["CRNAM"].ToString();
                        dr["CRDAT"] = dtDataNull.Rows[i]["CRDAT"].ToString();
                        newdt.Rows.Add(dr);
                    }
                }
                return newdt;
            }
            catch (Exception)
            {
                throw;
            }           
        }
        #endregion
    }
}
