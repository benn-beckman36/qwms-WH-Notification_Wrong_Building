using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

namespace QWMS
{
    public partial class ReportPrint : Form
    {
        #region 初始化参数
        private string strMandt = "";
        private string strUsrnm = "";
        private string strType = "";
        private string strCompany = "";
        private string strReportFile = "";
        private string strComcd = "";
        private ReportDocument rdReport;
        private DataTable dtReportData;
        UserInfo UserData = new UserInfo();
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

        public string Company
        {
            get
            {
                return strCompany;
            }
            set
            {
                strCompany = value;
            }
        }

        public string ReportFile
        {
            get
            {
                return strReportFile;
            }
            set
            {
                strReportFile = value;
            }
        }

        public ReportDocument Report
        {
            get
            {
                return rdReport;
            }
            set
            {
                rdReport = value;
            }
        }

        public DataTable ReportData
        {
            get
            {
                return dtReportData;
            }
            set
            {
                dtReportData = value;
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
        #endregion
        public ReportPrint(UserInfo varUserData,  string strType, DataTable dtData)
        {

            try
            {
                InitializeComponent();
                UserData = varUserData;
                Mandt = varUserData.Client;
                Usrnm = varUserData.UserId;
                Type = strType;
                dtReportData = dtData;
                Comcd = UserData.CompanyCode;

                switch (strType.ToUpper())
                {
                    case "LOG":			//異動記錄查詢
                        strReportFile = "LogReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREIN_PRODUCT":	//成品入庫
                        strReportFile = "StoreOutReport_PalletIDLabel.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT":	//出庫作業-Detail
                        strReportFile = "StoreOutReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT_CFG":	//CONFIG FIFO出庫作業-Detail
                        strReportFile = "StoreOutReportCFG.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT_GRPID":	//出庫作業(含Group id)
                        strReportFile = "StoreOutReport_Groupid.rpt";
                        PrintOutIdReport(dtData.Rows[0]["GRPID"].ToString());
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT_REPRINT":	//出庫作業(補印報表)
                        strReportFile = "StoreOutReport_Reprint.rpt";
                        PrintOutIdReport(dtData.Rows[0]["GRPID"].ToString());
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT_SPAREPARTS":	//出庫作業-Spare Parts
                        strReportFile = "StoreOutReport_SPAREPARTS.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUT_DATECODE":	//出庫作業-Detail_DateCode
                        strReportFile = "StoreOutReport_DateCode.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //Add by Smose Liao 20090714
                    case "STOREOUT_DATECODE_NEW":	//出庫作業-Detail_DateCode(New)
                        strReportFile = "StoreOutReport_DateCode_New.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //Add by Smose Liao 20100326
                    case "STOREOUT_DATECODE_SUMMARY":	//出庫作業-Detail_DateCode(SUMMARY)
                        strReportFile = "StoreOutReport_DateCode_Summary.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "STOREOUTHEADER":	//出庫作業-Header
                        strReportFile = "StoreOutHeaderReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印出庫單  Smose Liao 20090603
                    case "STOREOUT_PICASSO":	//出庫作業-Picasso
                        strReportFile = "StoreOutReport_Picasso.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //Add by Blank Zhu 20150812
                    case "PRINTBYSPLITLOCATION":	//出庫作業-Split Location
                        strReportFile = "StoreIn_SplitLocation.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印出庫單  Smose Liao 20091007
                    case "STOREOUT_PRODUCT":	//出庫作業-Product
                        strReportFile = "StoreOutReport_Product.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印入庫單  Smose Liao 20090330
                    case "STOREIN_DATECODE":  //入庫作業-Detail_DateCode  
                        strReportFile = "StoreInReport_DateCode.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "SUMMARY":		//庫存資料Summary
                        strReportFile = "SummaryReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "DETAIL":		//庫存資料明細
                        strReportFile = "DetailReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "DETAILSPAREPARTS":		//庫存資料明細 BY SPARE PARTS
                        strReportFile = "DetailReport_SpareParts.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "DETAILDATECODE":		//庫存資料明細 BY Datecode
                        strReportFile = "DetailReport_DateCode.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "CONTRAST":	//庫存比對
                        strReportFile = "ContrastReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "WASTE"://实时库存——报废
                        strReportFile = "WasteReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "CONTRAST_DOCUMENT":	//庫存比對
                        strReportFile = "ContrastReportWithDocument.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "FIXEDLOCATION":	//固定儲位料號
                        strReportFile = "FixedLocationReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "DETAILCOUNT":		//盤點明細查詢
                        strReportFile = "CountingLocationReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;

                    case "DETAILCOUNTALL":		//盤點明細查詢
                        strReportFile = "CountingLocationAllReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;

                    case "COUNTPREPARE":		//盤點前置作業
                        strReportFile = "CountingPrepareReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "COUNTADJUST":		//盤點調帳
                        strReportFile = "CountingAdjustReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "COUNTINGLABEL":	//盤點票Label
                        strReportFile = "CountingLabelReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "COUNTINGLABELLOCAT":	//儲位明細盤點票Label
                        strReportFile = "CountingLabelLocatReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "EMPTYLOCATION":		//空儲位列表
                        strReportFile = "LocationReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "REPLENISH_SUMMARY":		//補貨作業Summary
                        strReportFile = "ReplenishSummaryReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "REPLENISH_DETAIL":		//補貨作業明細
                        strReportFile = "ReplenishDetailReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "REPLENISHOUT":	//補貨出庫
                        strReportFile = "ReplenishOutReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "REPLENISH":	//補貨單查詢
                        strReportFile = "ReplenishReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印311(SMT)報表  Smose Liao 20100303
                    case "SMTDATA":	 //模擬產生SAP單據
                        strReportFile = "SMTDataReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印261(FINAL)報表  Smose Liao 20100303
                    case "FINDATA": //模擬產生SAP單據
                        strReportFile = "FINALDataReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印所有261與311單據資訊的報表  Smose Liao 20100303
                    case "SIMUDOCLIST": //模擬產生SAP單據
                        strReportFile = "SimulationDocListReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印Groupd id資料(SMT)的報表  Smose Liao 20100803
                    case "SMTGROUPIDREPORT": //模擬產生SAP單據
                        strReportFile = "SMTGroupIdReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印Groupd id資料(FINAL)的報表  Smose Liao 20100803
                    case "FINALGROUPIDREPORT": //模擬產生SAP單據
                        strReportFile = "FinalGroupIdReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印出庫單包含線別線別順序(依線別顯示) Ryan Tsai 20131016
                    case "LINEINFOBYLINE":
                        strReportFile = "StoreOutReport_LineInfoByLine.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "LINEINFOBYLINE_TERPER":
                        strReportFile = "StoreOutReport_LineInfoByLine_Thermalpaper.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印出庫單包含線別線別順序(依品名顯示) Ryan Tsai 20131016
                    case "LINEINFOBYMATNM":
                        strReportFile = "StoreOutReport_LineInfoByMaterial.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //列印異動紀錄(添加線別信息) Ryan Tsai 20131128
                    case "LOG_AGV":
                        strReportFile = "LogReport_LineInfo.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "MODELSTOREOUT":	//模具出庫作業-Detail
                        strReportFile = "ModelStorageOutOffLine.rpt";
                        ModelPrintOutReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "MODELSTOREOUTHEADER":	//模具出庫作業-Header
                        strReportFile = "ModelStorageOutHeaderReport.rpt";
                        ModelPrintOutReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //打印模具部分
                    case "MODEL":
                        strReportFile = "ModelOrderReport.rpt";
                        PrintModelReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //打印模具部分
                    case "MODELLOCAT":
                        strReportFile = "ModelLocationReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    //打印Film出库
                    case "FILMOUT":
                        strReportFile = "FilmOutReport.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "LOCATIONSCAN":
                        strReportFile = "LocationScan.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "PLASTICMATERIALLOCATLABEL_SCATTERED":	//塑壳入库零板Label
                        strReportFile = "PlasticMaterialLabelLocatReport_Scattered.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "PLASTICMATERIALLOCATLABEL_SCATTERED2":	//塑壳入库零板Label
                        strReportFile = "PlasticMaterialLabelLocatReport_Scattered2.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "PLASTICMATERIALLOCATLABEL_INTEGER":	//塑壳入库整板Label
                        strReportFile = "PlasticMaterialLabelLocatReport_Integer.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "SCRAPPACKING":
                        strReportFile = "ScrapPacking.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "TRANSFEROUTSAP_313"://313调拨单
                        strReportFile = "TransferOut313.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "TRANSFEROUTSAP_303"://303调拨单
                        strReportFile = "TransferOut303.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "TRANSFEROUTSAP_351"://351调拨单
                        strReportFile = "TransferOut351.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "TRANSFEROUTSAP_60S"://60S调拨单
                        strReportFile = "TransferOut60S.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "REAL_TIME_PEDING_QUERY"://实时待验查询
                        strReportFile = "Real_time_peding_query.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;
                    case "WASTEDETAIL"://废品合箱打印
                        strReportFile = "Waste_Print_Detail.rpt";
                        PrintGeneralReport();
                        crystalReportViewer1.ReportSource = rdReport;
                        crystalReportViewer1.Zoom(2);
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #region PrintGeneralReport
        private void PrintGeneralReport()
        {
            try
            {
                LoadReport();
                GetCompanyName();
                //Get Parameter
                ParameterValues MyParameterValues = new ParameterValues();
                ParameterDiscreteValue DiscreteValue = new ParameterDiscreteValue();

                DiscreteValue.Value = Usrnm;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Usrnm"].ApplyCurrentValues(MyParameterValues);
                DiscreteValue.Value = Company;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Company"].ApplyCurrentValues(MyParameterValues);
                //rdReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"D:\gr\IN\2.pdf");//保存PDF文件
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "<-PrintGeneralReport()");
            }
        }
        #endregion

        #region PrintOutIdReport
        private void PrintOutIdReport(string strGrpid)
        {
            try
            {
                LoadReport();
                GetCompanyName();
                //Get Parameter
                ParameterValues MyParameterValues = new ParameterValues();
                ParameterDiscreteValue DiscreteValue = new ParameterDiscreteValue();

                DiscreteValue.Value = Usrnm;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Usrnm"].ApplyCurrentValues(MyParameterValues);
                DiscreteValue.Value = Company;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Company"].ApplyCurrentValues(MyParameterValues);
                DiscreteValue.Value = strGrpid;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Grpid"].ApplyCurrentValues(MyParameterValues);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "<-PrintOutIdReport()");
            }
        }
        #endregion

        #region PrintModelReport
        private void PrintModelReport()
        {
            try
            {
                LoadReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "<-PrintModelReport()");
            }
        }
        #endregion

        #region ModelPrintOutReport
        private void ModelPrintOutReport()
        {
            try
            {
                LoadReport();
                GetCompanyName();
                //Get Parameter
                ParameterValues MyParameterValues = new ParameterValues();
                ParameterDiscreteValue DiscreteValue = new ParameterDiscreteValue();

                DiscreteValue.Value = Usrnm;
                MyParameterValues.Add(DiscreteValue);
                rdReport.DataDefinition.ParameterFields["Usrnm"].ApplyCurrentValues(MyParameterValues);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "<-ModelPrintOutReport()");
            }
        }
        #endregion

        #region GetCompanyName
        private void GetCompanyName()
        {
            try
            {
                QCI.QWMS.StorageData objStorage = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, dtReportData.Rows[0]["WERKS"].ToString(), dtReportData.Rows[0]["LGORT"].ToString());
                strCompany = objStorage.GetCompanyName();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetCompanyName()");
            }
        }
        #endregion

        #region LoadReport
        private void LoadReport()
        {
            string strTmp = Application.StartupPath + "\\report\\" + strReportFile;
            try
            {
                rdReport = new ReportDocument();
                rdReport.Load(Application.StartupPath + "\\report\\" + strReportFile);
                rdReport.SetDataSource(dtReportData);
                this.crystalReportViewer1.ReportSource = rdReport;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + strTmp + "<-LoadReport()");
            }
        }
        #endregion
    }
}
