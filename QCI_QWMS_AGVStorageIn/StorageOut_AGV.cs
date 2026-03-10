using Qci.Base.Common;
using QWMS.Common;
using QWMS.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QCI.QWMS
{
    public class StorageOut_AGV : ControlBase
    {
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMblnr = "";
        private string strProgid = "";
        private string strCrnam = "";
        private string strErrmsg = "";
        private string strSttyp = "";
        private string strLotyp = "";

        #region Constructer

        #region 不傳參數的建構式(不用)
        public StorageOut_AGV()
        {
        }
        #endregion

        #region 傳入UserData及ProgramID當參數的建構式
        ////////////Summary by Marc Hong ////////////////////////////////////////////
        /// <summary>
        /// 產生QCI.QWMS.StorageOut物件, 並將所需的UserData及ProgramID傳入。
        /// </summary> 
        /// <param name="varUserData">UserData。</param>
        /// <param name="varProgid">Program ID。</param>
        /// <example>
        /// <code>
        /// <remarks>
        ///  QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut (UserData,  "001")
        ///  
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public StorageOut_AGV(UserInfo varUserData, string varProgid)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "", varProgid)
        {
        }

        #endregion

        #region 傳入UserData及ProgramID當參數的建構式
        ////////////Summary by Marc Hong ////////////////////////////////////////////
        /// <summary>
        /// 產生QCI.QWMS.StorageOut物件, 並將所需的UserData及ProgramID傳入。
        /// </summary> 
        /// <param name="varUserData">UserData。</param>
        /// <param name="varProgid">Program ID。</param>
        /// <example>
        /// <code>
        /// <remarks>
        ///  QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut (UserData,  "001")
        ///  
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public StorageOut_AGV(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, varWerks, varLgort, varProgid)
        {
        }

        #endregion


        #region 傳入DBType, DBCode,ErrorType,ErrorCode,UserData及ProgramID當參數的建構式
        ////////////Summary by Marc Hong ////////////////////////////////////////////
        /// <summary>
        /// 產生QCI.QWMS.StorageOut物件, 並將所需的DBType, DBCode,ErrorType,ErrorCode,UserData及ProgramID傳入。
        /// </summary> 
        /// <param name="varDBType">DBTyp。</param>
        /// <param name="varDBCode">DBCode。</param>
        /// <param name="varErrorType">ErrorType。</param>
        /// <param name="varErrorCode">ErrorCode。</param>
        /// <param name="varUserData">UserData。</param>
        /// <param name="varProgid">Program ID。</param>
        /// <example>
        /// <code>
        /// <remarks>
        ///  QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut (UserData,  "001")
        ///  
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public StorageOut_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string strProgid)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.StorageOut_AGV";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;


            MANDT = varUserData.Client;
            COMCD = varUserData.CompanyCode;
            PROGID = strProgid;
            CRNAM = varUserData.UserId;
            WERKS = varWerks;
            LGORT = varLgort;

            DataTable dtTemp = new DataTable();
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(varUserData);

            dtTemp = objPlantData.GetPlantStorageData("LGORT", WERKS, LGORT);
            if (dtTemp.Rows.Count >= 1)
            {
                strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
            }


        }
        #endregion

        #region 建構式
        ////////////Summary by Rock Tzeng ////////////////////////////////////////////
        /// <summary>
        /// 產生QCI.QWMS.Admin物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
        /// </summary> 
        /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
        /// <param name="strMandt">SAP CLIENT。</param>
        /// <param name="strWerks">廠區。</param>
        /// <param name="strLgort">倉別。</param>
        /// <param name="strMblnr">單據號碼。</param>
        /// <param name="strProgid">目前使用的程式代號。</param>
        /// <param name="strCrnam">使用者帳號。</param>
        /// <example>
        /// <code>
        /// <remarks>
        ///  QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
        ///  
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public StorageOut_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();

            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.StorageOut_AGV";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            DataTable dtTemp = new DataTable();
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(varUserData);

            MANDT = varUserData.Client;
            WERKS = strWerks;
            LGORT = strLgort;
            MBLNR = strMblnr;
            PROGID = strProgid;
            CRNAM = varUserData.UserId;
            dtTemp = objPlantData.GetPlantStorageData("LGORT", WERKS, LGORT);
            if (dtTemp.Rows.Count >= 1)
            {
                strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
            }

        }
        #endregion


        #endregion

        #region DataMember


        UserInfo UserData = new UserInfo();


        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// SAP Client。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string MANDT
        {
            get { return strMandt; }
            set { strMandt = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Company Code
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string COMCD
        {
            get { return strComcd; }
            set { strComcd = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 廠區。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string WERKS
        {
            get { return strWerks; }
            set { strWerks = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 倉別。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string LGORT
        {
            get { return strLgort; }
            set { strLgort = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 單據號碼。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string MBLNR
        {
            get { return strMblnr; }
            set { strMblnr = value; }
        }
        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 目前使用的程式代號。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string PROGID
        {
            get { return strProgid; }
            set { strProgid = value; }
        }/////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 使用者帳號。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string CRNAM
        {
            get { return strCrnam; }
            set { strCrnam = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 錯誤訊息。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string ERRMSG
        {
            get { return strErrmsg; }
            set { strErrmsg = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 倉別類型。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string STTYP
        {
            get { return strSttyp; }
            set { strSttyp = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 儲位類型。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string LOTYP
        {
            get { return strLotyp; }
            set { strLotyp = value; }
        }

        #endregion

        public bool CheckAuthority()
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckAuthority";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                Authority objAuthority = new Authority(UserData);
                if (objAuthority.REPLN.IndexOf(PROGID) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
        }


        #region AGV 祥龙DateCode出库获取库存数据，按照 VEDAT,CRDAT,LOCAT,MBLNR排序
        /// <summary>
        /// 祥龙DateCode出库获取库存数据，按照 VEDAT,CRDAT,LOCAT,MBLNR排序
        /// </summary>
        /// <param name="strWerks">厂区</param>
        /// <param name="strMatnr">料号</param>
        /// <param name="strInsmk">库别</param>
        /// <param name="strCharg">版本</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable QueryQwmsDCData_AGV(string strWerks, string strLgorts, string strMatnr, string strInsmk, string strCharg, string strShelfList, string strShelfType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryQwmsDCData_AGV";
            this.ControlMethodParm = "(" + strWerks + "," + strLgorts + "," + strMatnr + "," + strInsmk + "," + strCharg + "," + strShelfList + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtData = new DataTable();
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT MANDT,COMCD,WERKS,LGORT,MARNO,LOCAT,MATNR,CHARG,INSMK,MENGE StockQty,LIFNR,DACOD,LOCOD,VEDAT,INDAT,SERNO,ROW_NUMBER() OVER(PARTITION BY MATNR ORDER BY VEDAT,LOCOD,INDAT,LOCAT) ID,SUM(MENGE) OVER(PARTITION BY MATNR) LocatSumStock,0 AS StockOutQty FROM [View_QueryWhitmByLocat] ");

            sbSql.AppendFormat(" WHERE MANDT='{0}' and COMCD='{1}' and WERKS='{2}' and LGORT ='{3}' AND MATNR='{4}' AND INSMK='{5}' AND CHARG='{6}' AND ITEMSTATES='Y' AND STOCSTATES='Y' ", MANDT, COMCD, strWerks, strLgorts, strMatnr, strInsmk, strCharg);


            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return dtData;
        }
        #endregion

        #region 祥龙出库查询库存信息
        /// <summary>
        /// 祥龙出库查询库存信息
        /// </summary>
        /// <param name="dtDwnSource"></param>
        /// <param name="varByDateCode"></param>
        /// <returns></returns>
        public DataSet QuerySimulationDateCodeData_AGV(DataTable dtDwnSource, string strGrpid, bool varByDateCode)
        {
            DataSet dsResult = new DataSet();
            try
            {
                DataTable dtOutWhitm = new DataTable();//要出庫的庫存數據

                DataRow drRow;
                DataRow[] drFound;

                DataWhitm objWhitm = new DataWhitm(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataTable dtWhitmALL = new DataTable(); //一次性查询全部库存
                DataTable dtWhitm = new DataTable(); //根据条件获取当前库存
                StringBuilder sbSql = new StringBuilder();
                for (int i = 0; i < dtDwnSource.Rows.Count; i++)
                {
                    #region 組單據的Key
                    StringBuilder sbWhdwnKey = new StringBuilder();
                    sbWhdwnKey.Remove(0, sbWhdwnKey.Length);
                    sbWhdwnKey.Append(" MBLNR='" + dtDwnSource.Rows[i]["MBLNR"].ToString() + "'");
                    sbWhdwnKey.Append(" and ZEILE='" + dtDwnSource.Rows[i]["ZEILE"].ToString() + "'");
                    sbWhdwnKey.Append(" and MATNR='" + dtDwnSource.Rows[i]["MATNR"].ToString() + "'");
                    sbWhdwnKey.Append(" and INSMK='" + dtDwnSource.Rows[i]["INSMK"].ToString() + "'");
                    sbWhdwnKey.Append(" and CHARG='" + dtDwnSource.Rows[i]["CHARG"].ToString() + "'");
                    #endregion

                    //出庫單據的數量
                    int intSourceMenge = int.Parse(dtDwnSource.Rows[i]["MENGE"].ToString());

                    //出库排序规则:INDAT->先進先出, LOCAT->儲位由小到大出, CRDAT->先建檔的先出)
                    string strOrderBy = "";
                    if (varByDateCode == true)
                    {
                        strOrderBy = "VEDAT,CRDAT,LOCAT,MBLNR";//DateCode: 廠商生產日期, 建立日期, 儲位
                    }
                    else
                    {
                        strOrderBy = "INDAT, CRDAT, LOCAT,MBLNR";//正常出庫：入庫日期, 建立日期, 儲位
                    }
                    #region 查询库存SQL
                    sbSql.Length = 0;
                    sbSql.AppendLine(" SELECT MANDT,W.COMCD,WERKS,LGORT,LOCAT,MBLNR as OMBLNR,"
                    + "  '" + dtDwnSource.Rows[i]["MTYPE"].ToString() + "'  as MTYPE, "
                    + "  '" + dtDwnSource.Rows[i]["MBLNR"].ToString() + "'  as MBLNR,"
                    + "  '" + dtDwnSource.Rows[i]["ZEILE"].ToString() + "' as ZEILE,"
                    + "  '" + dtDwnSource.Rows[i]["KOSTL"].ToString() + "' as KOSTL, "
                    + "  '" + dtDwnSource.Rows[i]["ARBPL"].ToString() + "' as ARBPL ,"
                    + "  '" + dtDwnSource.Rows[i]["TRNTP"].ToString() + "' as TRNTP ,"
                    + " W.MATNR, M.DECITEM ,INSMK, CHARG,(MENGE-REQTY+BKQTY) as MENGE,0 as ALQTY,MRGID,"
                    + "EBELN,LIFNR,RMANO,RMAK1,INDAT,  KDMAT,CRDAT,SERNO,BOXID,VEDAT,DACOD,LOCOD,INSPT,PKDAT,"
                    + "SUBSTRING(PKDAT,0,5) + RIGHT('0'+CONVERT(VARCHAR(2), DATEPART(month,PKDAT)), 2) + RIGHT('0'+CONVERT(VARCHAR(2), DATEPART(day,PKDAT)), 2) AS NEW_PKDAT,'' REFID, SEQNO,MARNO "
                    + " FROM DBO.WHITM W   LEFT JOIN MATDIC AS M ON W.COMCD=M.COMCD AND  W.MATNR=M.MATNR   ");

                    sbSql.AppendLine(" WHERE  (MANDT= '" + MANDT + "') And (W.COMCD= '" + COMCD + "') And (WERKS= '" + WERKS + "') And (LGORT= '" + dtDwnSource.Rows[i]["LGORT"].ToString() + "') "
                      + " And (W.MATNR= '" + dtDwnSource.Rows[i]["MATNR"].ToString() + "') And (INSMK= '" + dtDwnSource.Rows[i]["INSMK"].ToString() + "') "
                       + " And (CHARG= '" + dtDwnSource.Rows[i]["CHARG"].ToString() + "')    "
                      + " AND ITEMSTATES='N' AND STOCSTATES='N' AND LOCKED<>'Y' ");
                    sbSql.AppendFormat("ORDER BY {0}", strOrderBy);
                    try
                    {
                        ControlHandleDB();
                        dtWhitm = ControlSqlAccess.GetDataTable(sbSql.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (Exception ex)
                    {
                        ERRMSG = ex.Message + "<- QuerySimulationDateCodeData_AGV()查询库存异常";
                    }
                    #endregion

                    int intSourceOutMenge = 0;
                    //DataRow[] dataRows = dtWhitmALL.Select("MATNR='" + dtDwnSource.Rows[i]["MATNR"].ToString() + "'", strOrderBy);
                    //dtWhitm = dataRows.CopyToDataTable();
                    if (i == 0)
                    {
                        dtOutWhitm = dtWhitm.Clone();  //即将要出库的数据,初始化无内容，后续单条新增
                    }
                    for (int j = 0; j < dtWhitm.Rows.Count; j++)
                    {
                        intSourceOutMenge = 0;

                        //intSourceOutMenge：已出庫單據的出库总量
                        intSourceOutMenge = dtOutWhitm.Select(sbWhdwnKey.ToString()).Sum(s => s.Field<int>("ALQTY"));

                        //intSourceMenge:whdwn上要出的數量, intSourceMenge:已經出的數量
                        if (intSourceMenge > intSourceOutMenge)
                        {
                            #region 单据数量大于库存数量的情况   //如果還沒出完
                            int intLocatMblnrTempAlqty = 0;

                            //從目前已出庫的數量中找出該儲位該料號已經出庫的數量
                            #region 組已出單據中儲位庫存的Key(比對WHITM的Key)
                            StringBuilder sbWhitmKey = new StringBuilder();
                            sbWhitmKey.Remove(0, sbWhitmKey.Length);
                            sbWhitmKey.Append(" LOCAT='" + dtWhitm.Rows[j]["LOCAT"].ToString() + "'");
                            sbWhitmKey.Append(" and OMBLNR = '" + dtWhitm.Rows[j]["OMBLNR"].ToString() + "'");
                            sbWhitmKey.Append(" and MATNR='" + dtWhitm.Rows[j]["MATNR"].ToString() + "'");
                            sbWhitmKey.Append(" and INSMK='" + dtWhitm.Rows[j]["INSMK"].ToString() + "'");
                            sbWhitmKey.Append(" and CHARG='" + dtWhitm.Rows[j]["CHARG"].ToString() + "'");
                            sbWhitmKey.Append(" and MRGID='" + dtWhitm.Rows[j]["MRGID"].ToString() + "'");
                            //成品
                            sbWhitmKey.Append(" and SERNO='" + dtWhitm.Rows[j]["SERNO"].ToString() + "'");
                            //DataCode
                            sbWhitmKey.Append(" and DACOD='" + dtWhitm.Rows[j]["DACOD"].ToString() + "'");
                            sbWhitmKey.Append(" and LOCOD='" + dtWhitm.Rows[j]["LOCOD"].ToString() + "'");
                            sbWhitmKey.Append(" and INSPT='" + dtWhitm.Rows[j]["INSPT"].ToString() + "'");

                            #endregion

                            //查询出前面单据已出的库存
                            intLocatMblnrTempAlqty = dtOutWhitm.Select(sbWhitmKey.ToString()).Sum(s => s.Field<int>("ALQTY"));
                            //这个储位可以出的数量 MENGE:Locat數量, ALQTY:已出數量
                            int intBalanceQty = Int32.Parse(dtWhitm.Rows[j]["MENGE"].ToString()) - intLocatMblnrTempAlqty;

                            #region 如果儲位還有剩餘數量 计算出库逻辑
                            if (intBalanceQty > 0)
                            {
                                int intLocatAlqty = 0;
                                //如果儲位的剩餘數量小於單據的剩餘數量則全出, 否則出差值
                                if (intSourceMenge - intSourceOutMenge >= intBalanceQty)
                                {
                                    intLocatAlqty = intBalanceQty;
                                }
                                else
                                {
                                    intLocatAlqty = intSourceMenge - intSourceOutMenge;
                                }
                                #region  如果儲位有出庫則紀錄
                                if (intLocatAlqty > 0)
                                {
                                    drRow = dtOutWhitm.NewRow();
                                    drRow["MANDT"] = dtWhitm.Rows[j]["MANDT"].ToString();
                                    drRow["COMCD"] = dtWhitm.Rows[j]["COMCD"].ToString();
                                    drRow["WERKS"] = dtWhitm.Rows[j]["WERKS"].ToString();
                                    drRow["LGORT"] = dtWhitm.Rows[j]["LGORT"].ToString();
                                    drRow["LOCAT"] = dtWhitm.Rows[j]["LOCAT"].ToString();
                                    drRow["MATNR"] = dtWhitm.Rows[j]["MATNR"].ToString();
                                    drRow["DECITEM"] = dtWhitm.Rows[j]["DECITEM"];
                                    drRow["INSMK"] = dtWhitm.Rows[j]["INSMK"].ToString();
                                    drRow["CHARG"] = dtWhitm.Rows[j]["CHARG"].ToString();
                                    drRow["MENGE"] = dtWhitm.Rows[j]["MENGE"].ToString();
                                    drRow["ALQTY"] = intLocatAlqty.ToString();
                                    drRow["MTYPE"] = dtWhitm.Rows[j]["MTYPE"].ToString();
                                    drRow["MBLNR"] = dtWhitm.Rows[j]["MBLNR"].ToString();
                                    drRow["ZEILE"] = dtWhitm.Rows[j]["ZEILE"].ToString();
                                    drRow["EBELN"] = dtWhitm.Rows[j]["EBELN"].ToString();
                                    drRow["LIFNR"] = dtWhitm.Rows[j]["LIFNR"].ToString();
                                    drRow["RMANO"] = dtWhitm.Rows[j]["RMANO"].ToString();  // Quanta, Smose.Liao, 20090225：Add the function of Online Goods Issue by RMA No.(連線出庫依據RMA NO.進行出庫)
                                    drRow["OMBLNR"] = dtWhitm.Rows[j]["OMBLNR"].ToString();
                                    drRow["MRGID"] = dtWhitm.Rows[j]["MRGID"].ToString();
                                    drRow["KOSTL"] = dtWhitm.Rows[j]["KOSTL"].ToString();
                                    drRow["ARBPL"] = dtWhitm.Rows[j]["ARBPL"].ToString();
                                    drRow["TRNTP"] = dtWhitm.Rows[j]["TRNTP"].ToString();
                                    drRow["RMAK1"] = dtWhitm.Rows[j]["RMAK1"].ToString();
                                    drRow["INDAT"] = dtWhitm.Rows[j]["INDAT"].ToString();
                                    //20051017 MARC 新增KDMAT欄位
                                    drRow["KDMAT"] = dtWhitm.Rows[j]["KDMAT"].ToString();
                                    //成品
                                    drRow["SERNO"] = dtWhitm.Rows[j]["SERNO"].ToString();
                                    drRow["BOXID"] = dtWhitm.Rows[j]["BOXID"].ToString();
                                    //DateCode
                                    drRow["VEDAT"] = dtWhitm.Rows[j]["VEDAT"].ToString();
                                    drRow["DACOD"] = dtWhitm.Rows[j]["DACOD"].ToString();
                                    drRow["LOCOD"] = dtWhitm.Rows[j]["LOCOD"].ToString();
                                    drRow["INSPT"] = dtWhitm.Rows[j]["INSPT"].ToString();
                                    drRow["PKDAT"] = dtWhitm.Rows[j]["PKDAT"].ToString();
                                    //新增料架号
                                    drRow["MARNO"] = dtWhitm.Rows[j]["MARNO"].ToString();

                                    dtOutWhitm.Rows.Add(drRow);

                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                        else
                        {
                            break;
                        }
                    }
                    //更新單據的總出庫數量
                    intSourceOutMenge = 0;

                    drFound = dtOutWhitm.Select(sbWhdwnKey.ToString());

                    for (int k = 0; k < drFound.Length; k++)
                    {
                        intSourceOutMenge += Int32.Parse(drFound[k]["ALQTY"].ToString());
                    }
                    dtDwnSource.Rows[i]["ALQTY"] = intSourceOutMenge;

                }

                if (dtDwnSource.DataSet != null)
                    dtDwnSource.DataSet.Tables.Remove(dtDwnSource);

                dtDwnSource.TableName = "Source";
                dtOutWhitm.TableName = "Destination";
                dsResult.Tables.Add(dtDwnSource);
                dsResult.Tables.Add(dtOutWhitm);

            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- QuerySimulationDateCodeData()";
            }

            return dsResult;
        }
        #endregion

        public DataTable CombineSimulationComfirmData(string strSmtStock)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CombineSimulationComfirmData";
            this.ControlMethodParm = "(" + strSmtStock + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
            DataTable dtResult = new DataTable();

            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("EXEC SP_SimulationOut_CombineConfirmData_AGV '{0}'", strSmtStock);
            try
            {
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return dtResult;
        }

        #region 获取工作站信息
        public DataTable QueryWorkStation(string strComcd)
        {
            this.ControlMethodName = "QueryWorkStation";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtWorkStation = new DataTable();
            try
            {
                StringBuilder sbSql=new StringBuilder();
                sbSql.AppendFormat("SELECT CTRLNM FROM WHCTRL WHERE CTRLID='WorkStation' AND COMCD='{0}'", strComcd);
                ControlHandleDB();
                dtWorkStation = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return dtWorkStation;
        }
        #endregion

        #region 祥龙出库查询出库信息SP
        /// <summary>
        /// 祥龙出库查询出库信息
        /// </summary>
        /// <param name="strQueryData"></param>
        /// <param name="strQueryType"></param>
        /// <param name="strGrpID"></param>
        /// <returns></returns>
        public DataTable QuerySimulationQueryData_AGV(string strQueryData, string strGrpID, string strWerks, string strLgort, string strShelfType)
        {
            DataTable dtResult = new DataTable();
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" DECLARE @Restult VARCHAR(1) ");
                sbSql.AppendFormat(" EXEC SP_SimulationOut_Query_AGV '{0}','{1}','{2}','{3}',@Restult OUT ", strQueryData, strGrpID, strWerks, strLgort);
                sbSql.Append(" SELECT @Restult ");
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- QuerySimulationQueryData_AGV()";
            }

            return dtResult;
        }
        #endregion

        #region 祥龙AGV出库保存数据
        /// <summary>
        /// 祥龙AGV出库
        /// </summary>
        /// <param name="strOutSource"></param>
        /// <param name="strStorage"></param>
        /// <returns></returns>
        public string AddSinmulationOutSaveData_AGV(string strStorage, string strLgortJson)
        {
            string strResult = string.Empty;
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" DECLARE @Result VARCHAR(1) ");
                sbSql.AppendFormat(" EXEC [SP_SimulationOut_SaveMultiTask_AGV] '{0}','{1}','{2}','{3}',@Result OUT ", strStorage, strLgortJson, strProgid, UserData.UserId);
                sbSql.Append(" SELECT @Result ");
                ControlHandleDB();
                strResult = ControlSqlAccess.GetFieldValue(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- AddSinmulationOutSaveData_AGV()";
            }

            return strResult;
        }

        #endregion

        #region 生成TaskCode

        public string GetSimulationTaskNo(string strFunction, string strType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetSimulationTaskNo";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string strSQL = string.Empty;
            string strGrrno = string.Empty;

            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                strSQL = "EXEC SP_GetSerialNum_NEW '" + strFunction + "', '" + strType + "'";
                ControlHandleDB();
                string SERNO = ControlSqlAccess.GetFieldValue(strSQL);
                ControlSqlAccess.CloseConnection();
                strGrrno = SERNO.PadLeft(6, '0');
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return strGrrno;
        }
        #endregion

        #region 获取AGV出库数据
        /// <summary>
        /// 获取AGV出库数据
        /// </summary>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
        /// <param name="strGrpID"></param>
        /// <param name="strFunc"></param>
        /// <param name="strWorkStation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable QueryStockOutData_AGV(string strWerks, string strLgort, string strGrpID, string strFunc, string strFlage, string strTaskNo = "")
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryOnLineInData_AGV";
            this.ControlMethodParm = "(" + strGrpID + "," + strFunc + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtData = new DataTable();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,FUNC,WorkStation,ShelfNo,TASKID,REQID,SUBSTRING(LOCAT,1,8) LOCAT,LOCAT AS RealLocat,MATNR,CHARG,SendID,MBLNR,OMBLN,INSMK,LIFNR,EBELN,TRNTP,INDAT,KDMAT,MENGE,ALQTY,BLACE,SERNO,DACOD,VEDAT,LOCOD,INSPT,KOSTL,GroupID,LINE,SIDE,Proprity,CRNAM,REMARK,FLAGE  FROM AGV_MaterialForm WHERE COMCD='{0}' AND WERKS='{1}' AND LGORT='{2}'  AND FUNC='{3}' AND SendID='{4}' ", COMCD, strWerks, strLgort, strFunc, strGrpID);

            if (!string.IsNullOrEmpty(strFlage))
                sbSql.AppendFormat(" AND FLAGE='{0}'", strFlage);
            if (!string.IsNullOrEmpty(strTaskNo))
                sbSql.AppendFormat(" AND TASKID='{0}'", strTaskNo);

            sbSql.Append("ORDER BY ShelfNo,LOCAT,MATNR");
            try
            {
                ControlHandleDB(); 
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return dtData;
        }

        public DataTable QueryStockOutData_AGV(string strWerks,string strLgort, string strTaskID,string strFlage) {

            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryOnLineInData_AGV";
            this.ControlMethodParm = "(" + strTaskID + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtData = new DataTable();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,FUNC,WorkStation,ShelfNo,TASKID,REQID,LOCAT,MATNR,CHARG,SendID,MBLNR,OMBLN,INSMK,LIFNR,EBELN,TRNTP,INDAT,KDMAT,MENGE,ALQTY,BLACE,SERNO,DACOD,VEDAT,LOCOD,INSPT,KOSTL,GroupID,LINE,SIDE,Proprity,CRNAM,REMARK,FLAGE  FROM AGV_MaterialForm WHERE COMCD='{0}' AND WERKS='{1}' AND LGORT='{2}'  AND TASKID = '{3}' ", COMCD, strWerks, strLgort, strTaskID);

            if (!string.IsNullOrEmpty(strFlage))
                sbSql.AppendFormat(" AND FLAGE='{0}'", strFlage);

            sbSql.Append("ORDER BY ShelfNo,LOCAT,MATNR");
            try {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            } catch (CommonObjectsException ex) {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex) {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return dtData;
        }
        #endregion

        #region 获取祥龙AGV待扫描Send ID
        /// <summary>
        /// 获取祥龙AGV待扫描Send ID
        /// </summary>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
        /// <param name="strWorkStation"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable GetSimulationSendID(string strWerks, string strLgort, string strGrpID = "")
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetSimulationSendID";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtSendID = new DataTable();
            try
            {
                StringBuilder sbSql=new StringBuilder();
                if (string.IsNullOrEmpty(strGrpID))
                    sbSql.AppendFormat("SELECT DISTINCT SendID FROM AGV_MaterialForm WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND FUNC='Simulation' AND FLAGE='N'", strWerks, strLgort);
                else
                    sbSql.AppendFormat("SELECT DISTINCT TASKID FROM AGV_MaterialForm WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND FUNC='Simulation' AND SendID='{2}' AND FLAGE='N' AND TASKID NOT IN(SELECT DISTINCT TASKID FROM WHAGV WITH(NOLOCK)) ", strWerks, strLgort, strGrpID);
                ControlHandleDB();
                dtSendID = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return dtSendID;
        }
        #endregion

        #region 获取DID数据
        /// <summary>
        /// 获取DID数据
        /// </summary>
        /// <param name="strDidNo">DIDNO号码</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable GetStorageOutDidData(string strDidNo)
        {
            this.ControlMethodName = "GetStorageOutDidData";
            this.ControlMethodParm = "(" + strDidNo + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtRestult = new DataTable();
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT  WERKS,LGORT,MATNR,CHARG,DIDNO, MENGE,LIFNR,LOCOD,DACOD FROM WHRID WITH (NOLOCK)  WHERE MANDT = '218' AND COMCD = '{0}' AND DIDNO='{1}' AND OTQTY>0 " +
                    "union SELECT WERKS,LGORT,MATNR,CHARG,DIDNO, MENGE,LIFNR,LOCOD,DACOD FROM [qwmsbak].[dbo].[WHRID_2020] WITH (NOLOCK) WHERE MANDT = '218' AND COMCD = '{0}' AND DIDNO = '{1}' AND OTQTY>0 ", strComcd, strDidNo);
                ControlHandleDB();
                dtRestult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return dtRestult;
        }
        #endregion

        #region 批量导入数据到WHAGV表中
        /// <summary>
        /// 批量导入数据到WHAGV表中
        /// </summary>
        /// <param name="dtTempAGV"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool BulkCopyWHAGV(DataTable dtTempAGV, string strWorkStation)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "BulkCopyWHAGV";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool flg = false;
            try
            {
                if (dtTempAGV.Rows.Count > 0)
                {
                    dtTempAGV.Columns.Add("WorkStation");
                    dtTempAGV.Columns.Add("CRNAM");
                    dtTempAGV.Columns.Add("CRDAT");
                    foreach(DataRow dr in dtTempAGV.Rows)
                    {
                        dr["WorkStation"] = strWorkStation;
                        dr["CRNAM"] = UserData.UserId.ToString();
                        dr["CRDAT"] = DateTime.Now;
                    }
                    ArrayList SourceColumnsName = new ArrayList();
                    SourceColumnsName.Add("MANDT");
                    SourceColumnsName.Add("WERKS");
                    SourceColumnsName.Add("LGORT");
                    SourceColumnsName.Add("WorkStation");
                    SourceColumnsName.Add("TASKID");
                    SourceColumnsName.Add("REQID");
                    SourceColumnsName.Add("ShelfNo");
                    SourceColumnsName.Add("CRNAM");
                    SourceColumnsName.Add("CRDAT");
                    SourceColumnsName.Add("COMCD");

                    ArrayList DistinctColumnsName = new ArrayList();
                    DistinctColumnsName.Add("MANDT");
                    DistinctColumnsName.Add("WERKS");
                    DistinctColumnsName.Add("LGORT");
                    DistinctColumnsName.Add("PLACE");
                    DistinctColumnsName.Add("TASKID");
                    DistinctColumnsName.Add("REQNO");
                    DistinctColumnsName.Add("MARNO");
                    DistinctColumnsName.Add("CRNAM");
                    DistinctColumnsName.Add("CRDAT");
                    DistinctColumnsName.Add("COMCD");

                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlBulkCopyWithColumnsMapping("WHAGV", dtTempAGV, SourceColumnsName, DistinctColumnsName);
                    ControlSqlAccess.CloseConnection();
                }
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return flg;
        }
        #endregion

        #region 获取中间表数据
        public DataTable GetAGVData(string strWerks, string strLgort, string strWorkStation, string strTaskNo = "", string strShelfNo = "")
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetAGVStateData";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtSendID = new DataTable();
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT MANDT,COMCD, WERKS,LGORT,PLACE,TASKID,REQNO,MARNO,Shelf_state FROM WHAGV WHERE PLACE='{0}'",strWorkStation);
                if(!string.IsNullOrEmpty(strWerks))
                    sbSql.AppendFormat(" AND WERKS='{0}'", strWerks);
                if (!string.IsNullOrEmpty(strLgort))
                    sbSql.AppendFormat(" AND LGORT='{0}'", strLgort);
                if (!string.IsNullOrEmpty(strTaskNo))
                    sbSql.AppendFormat(" AND TASKID='{0}' ", strTaskNo); 
                if (!string.IsNullOrEmpty(strShelfNo))
                    sbSql.AppendFormat(" AND MARNO='{0}' ", strShelfNo);

                sbSql.Append(" ORDER BY TASKID,REQNO DESC");
                ControlHandleDB();
                dtSendID = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return dtSendID;
        }
        #endregion

        #region 保存加扣数据并自动出库
        public string SaveAGVAddData(string strWerks, string strLgort, string strWorkStation, string strSendID, string strAddByShelf, string strTaskID, string strReqNO, string strMoveType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "SaveAGVAddData";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string strResult = string.Empty;
            try
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("DECLARE @Result VARCHAR(1)");
                sbSql.AppendFormat("EXEC SP_SimulationOut_SaveAddData_AGV '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',@Result OUT", strAddByShelf, strWerks, strLgort,strSendID, strWorkStation,strTaskID,strReqNO,strProgid,UserData.UserId);
                sbSql.Append(" SELECT @Result ");
                ControlHandleDB();
                strResult = ControlSqlAccess.GetFieldValue(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return strResult;
        }
        #endregion

        #region 一次获取多个虚拟单号
        /// <summary>
        /// 一次获取多个虚拟单号
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable GetSimulationNumBeginEnd(int step)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetSimulationNumBeginEnd";
            this.ControlMethodParm = "(" + step + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string strSQL = "";
            DataTable dtSerno = new DataTable();

            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                strSQL = "EXEC SP_SimulationOut_GetSerialNum 'SimulationDoc', 'FLYPLAN','" + step + "'";
                ControlHandleDB();
                dtSerno = ControlSqlAccess.GetDataTable(strSQL);
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return dtSerno;
        }
        #endregion

        #region 获取AGV调度API地址
        /// <summary>
        /// 获取AGV调度API地址
        /// </summary>
        /// <param name="strRequestType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetAGVRequestUrl(string strRequestType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetAGVRequestUrl";
            this.ControlMethodParm = "(" + strRequestType + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string strRequestUrl = string.Empty;
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                string strSQL = "SELECT CTRLC1 FROM WHCTRL WHERE CTRLID='AgvApi' AND CTRLNM='" + strRequestType + "' ";
                ControlHandleDB();
                strRequestUrl = ControlSqlAccess.GetFieldValue(strSQL);
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }

            return strRequestUrl;
        }
        #endregion

        #region 更新料架调度中间表reqNo和料架的关系
        /// <summary>
        /// 更新料架调度中间表reqNo和料架的关系
        /// </summary>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
        /// <param name="strTaskNo"></param>
        /// <param name="strReqNo"></param>
        /// <param name="strShelfNo"></param>
        /// <exception cref="Exception"></exception>
        public bool UpdateOutAGVShelf(string strWerks, string strLgort,string strTaskNo, string strReqNo, string strShelfNo)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "UpdateOutAGVShelf";
            this.ControlMethodParm = "(" + strShelfNo + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool blResult = false;
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("UPDATE WHAGV SET REQNO='{0}' WHERE WERKS='{1}' AND LGORT='{2}' AND TASKID='{3}' AND MARNO='{4}'", strReqNo, strWerks, strLgort, strTaskNo, strShelfNo);
                ControlHandleDB();
                blResult = ControlSqlAccess.ExecSql(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return blResult;
        }
        #endregion

        #region 删除料架调度中间表数据
        /// <summary>
        /// 删除料架调度中间表数据
        /// </summary>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
        /// <param name="strTaskNo"></param>
        /// <param name="strWorkStation"></param>
        /// <exception cref="Exception"></exception>
        public bool DeleteAGVShelf(string strWerks, string strLgort, string strTaskNo, string strWorkStation)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "DeleteAGVShelf";
            this.ControlMethodParm = "(" + strWorkStation + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool blResult = false;
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("DELETE WHAGV WHERE WERKS='{0}' AND LGORT='{1}' AND TASKID='{2}' AND PLACE='{3}'", strWerks, strLgort, strTaskNo, strWorkStation);

                ControlHandleDB();
                blResult = ControlSqlAccess.ExecSql(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return blResult;
        }
        #endregion

        #region 更新出库备料单工作站以及处理状态,更新库存信息,更新储位信息
        /// <summary>
        /// 更新出库备料单工作站以及处理状态,更新库存信息,更新储位信息
        /// </summary>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
        /// <param name="strTaskNo"></param>
        /// <param name="strWorkStation"></param>
        /// <param name="strGrpID"></param>
        /// <param name="strShelfNo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UpdateScanOutSource(string strWerks, string strLgort, string strTaskNo, string strWorkStation, string strGrpID, string strShelfNo, string strLocat)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "UpdateMaterialForm";
            this.ControlMethodParm = "(" + strWorkStation + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool blResult = false;
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" DECLARE @Result VARCHAR(1) ");
                sbSql.AppendFormat(" EXEC [SP_UpdateScanOutSource_AGV] '{0}','{1}','{2}','{3}','{4}','{5}','{6}',@Result OUT ", strWerks, strLgort, strTaskNo, strWorkStation, strGrpID, strShelfNo, strLocat);
                sbSql.Append(" SELECT @Result ");
                ControlHandleDB();
                blResult = ControlSqlAccess.GetFieldValue(sbSql.ToString()).Equals("Y");
                ControlSqlAccess.CloseConnection();
            }
            catch (CommonObjectsException ex)
            {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return blResult;
        }
        #endregion

        #region 查询工作站状态
        public DataTable QueryAGVWorkStation(string strWerks, string strLgort, string strPlace, string strTASKID)
        {
            this.ControlMethodName = "QueryAGVWorkStation";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T")
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataTable dtData = new DataTable();
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT * FROM WHAGV WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND PLACE='{2}' AND COMCD='{3}' ", strWerks, strLgort, strPlace, strComcd);
                if (!string.IsNullOrEmpty(strTASKID))
                {
                    strSQL.AppendFormat(" AND TASKID='{0}' AND Shelf_state='1' ORDER BY CRDAT DESC ", strTASKID);
                }

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                ControlSqlAccess.CloseConnection();
                return dtData;
            }
            catch (CommonObjectsException ex)
            {

                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }
            catch (Exception ex)
            {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
        }
        #endregion


        #region 联机（转仓）出口保存出库数据
        public string AddOnlineOutSaveData_AGV(string dtSource, string dtStorage) {
            string strResult = string.Empty;
            try {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(" DECLARE @Result VARCHAR(1) ");
                strBuilder.AppendFormat(" EXEC SP_StorageOut_OnlineOutSave_AGV '{0}','{1}','{2}','{3}',@Result OUT  ", dtSource, dtStorage, strProgid, UserData.UserId);
                strBuilder.Append(" SELECT @Result ");
                ControlHandleDB();
                strResult = ControlSqlAccess.GetFieldValue(strBuilder.ToString());
                ControlSqlAccess.CloseConnection();
            } catch (System.Exception ex) {
                ERRMSG = ex.Message + "<- AddSinmulationOutSaveData_AGV()";
            }

            return strResult;
        }
        #endregion

        #region 联机（转仓）出口查询库存
        public DataSet QueryStorageOutDateCode_AGV(DataTable dtMblnr, bool varByDateCode) {
            DataSet dsResult = new DataSet();
            try {
                DataTable dtOutStorage = new DataTable();// 保存出库数据处理结果
                DataWhitm objWhitm = new DataWhitm(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataTable dtStorage = new DataTable();// 查询出的库存数据


                // 按照INSMK MATNR CHARG 分组查询库存
                dtMblnr.AsEnumerable().ToList().ForEach(f => {
                    StringBuilder strSql = new StringBuilder();

                    string strTranLgort = "";
                    // 判断是否包含转入仓
                    if (f.Table.Columns.Contains("UMLGO")) {
                        strTranLgort = f.Field<string>("UMLGO");//转入仓
                    }

                    //出库排序规则:INDAT->先進先出, LOCAT->儲位由小到大出, CRDAT->先建檔的先出)
                    // 添加LOCOD 20240612
                    string strOrderBy = "";
                    if (varByDateCode) {
                        strOrderBy = " VEDAT, LOCOD, INDAT, LOCAT";//DateCode: 廠商生產日期, 建立日期, 儲位
                    } else {
                        strOrderBy = " INDAT, LOCAT, MBLNR";//正常出庫：入庫日期, 建立日期, 儲位
                    }
                    #region 查询库存SQL
                    strSql.Clear();
                    strSql.Length = 0;
                    strSql.AppendLine(" SELECT MANDT,W.COMCD,WERKS,LGORT,LOCAT,LEFT(LOCAT,8) SLOCA,CASE WHEN LEN(LOCAT) = 11 THEN RIGHT(LOCAT,2) ELSE '' END SUBLO,"
                    + "  '" + f.Field<string>("MTYPE") + "' as MTYPE, "
                    + "  '" + f.Field<string>("MBLNR") + "' as MBLNR, "
                    + "  '" + f.Field<string>("ZEILE") + "' as ZEILE, "
                    + "  '" + f.Field<string>("KOSTL") + "' as KOSTL, "
                    + "  '" + f.Field<string>("ARBPL") + "' as ARBPL ,"
                    + "  '" + f.Field<string>("TRNTP") + "' as TRNTP ,"
                    + "  '" + strTranLgort + "' as UMLGO ,"
                    + " W.MATNR, M.DECITEM ,INSMK, CHARG,SUM(MENGE-REQTY+BKQTY) as MENGE,0 as ALQTY, 0 as BLACE,MRGID,"
                    + "EBELN,LIFNR,RMANO,RMAK1,INDAT,  KDMAT,SERNO,BOXID,VEDAT,DACOD,LOCOD,'' INSPT,PKDAT,"
                    + "SUBSTRING(PKDAT,0,5) + RIGHT('0'+CONVERT(VARCHAR(2), DATEPART(month,PKDAT)), 2) + RIGHT('0'+CONVERT(VARCHAR(2), DATEPART(day,PKDAT)), 2) AS NEW_PKDAT,'' REFID, SEQNO,MARNO,'' PROPRI,ExpiryDate AS EXPDAT ,TASKID "
                    + " FROM DBO.WHITM W   LEFT JOIN MATDIC AS M ON W.COMCD=M.COMCD AND  W.MATNR=M.MATNR   ");

                    strSql.AppendLine(" WHERE  (MANDT= '" + MANDT + "') And (W.COMCD= '" + COMCD + "') And (WERKS= '" + f.Field<string>("WERKS") + "') And (LGORT= '" + f.Field<string>("LGORT") + "') " + " And (W.MATNR= '" + f.Field<string>("MATNR") + "') And (INSMK= '" + f.Field<string>("INSMK") + "') "
                       + " And (CHARG= '" + f.Field<string>("CHARG") + "')    "
                      + " AND ITEMSTATES='Y' AND STOCSTATES='Y' "
                      );

                    //strIQCLgort 转出仓 
                    //转入待验仓、不良品仓都卡控加锁库存
                    if (CheckStorageInType(f.Field<string>("WERKS"), strTranLgort, "IQC LGORT") || CheckStorageInType(f.Field<string>("WERKS"), strTranLgort, "DEF LGORT")) {
                        strSql.AppendFormat("  AND LOCKED ='Y' ");
                    } else {
                        strSql.AppendFormat("  AND LOCKED<>'Y' ");
                    }
                    strSql.AppendFormat(" GROUP BY MANDT,W.COMCD,WERKS,LGORT,LOCAT,W.MATNR, M.DECITEM ,INSMK, CHARG,MRGID,EBELN,LIFNR,RMANO,RMAK1,INDAT,  KDMAT,SERNO,BOXID,VEDAT,DACOD,LOCOD,PKDAT,ExpiryDate,TASKID,SERNO,SEQNO,MARNO");
                    strSql.AppendFormat(" ORDER BY {0}", strOrderBy);

                    try {
                        ControlHandleDB();
                        dtStorage = ControlSqlAccess.GetDataTable(strSql.ToString());
                        ControlSqlAccess.CloseConnection();
                    } catch (Exception ex) {
                        ERRMSG = ex.Message + "<- QueryStorageOutDateCode_AGV()查询库存异常";
                    }
                    #endregion

                    #region 库存数据匹配出库单据
                    int mblnrMenge = 0, mblnrAlqty = 0, storageMenge = 0, storageAlqty = 0, storageOut = 0, balanceQty = 0, locatAlqty = 0;
                    // 出库数据初始化
                    if (dtOutStorage.Rows.Count <= 0) {
                        dtOutStorage = dtStorage.Clone();
                    }
                    // 循环储位（因为是虚拟储位，所以一个储位只有一条数据——一个料盘一个储位，一个料盘只有一条数据）
                    foreach (DataRow drStorage in dtStorage.Rows) {

                        #region 組單據的Key
                        StringBuilder sbWhdwnKey = new StringBuilder();
                        sbWhdwnKey.Remove(0, sbWhdwnKey.Length);
                        sbWhdwnKey.Append("WERKS='" + f.Field<string>("WERKS") + "'");
                        sbWhdwnKey.Append(" and LGORT='" + f.Field<string>("LGORT") + "'");
                        sbWhdwnKey.Append(" and LOCAT='" + drStorage["LOCAT"] + "'");
                        sbWhdwnKey.Append(" and MATNR='" + f.Field<string>("MATNR") + "'");
                        sbWhdwnKey.Append(" and INSMK='" + f.Field<string>("INSMK") + "'");
                        sbWhdwnKey.Append(" and CHARG='" + f.Field<string>("CHARG") + "'");
                        #endregion

                        // 历史储位已出数量(前面的循环是否已出这个储位)
                        locatAlqty = dtOutStorage.Select(sbWhdwnKey.ToString()).Sum(s => s.Field<int>("ALQTY"));
                        // 储位数量
                        storageMenge = Convert.ToInt32(drStorage["MENGE"]);
                        // 储位已出数量
                        storageAlqty = Convert.ToInt32(drStorage["ALQTY"]);
                        // 储位实际库存
                        int storageRealMenge = storageMenge - storageAlqty - locatAlqty;
                        //单据数量
                        mblnrMenge = Convert.ToInt32(f["MENGE"]);
                        //单据已出数量
                        mblnrAlqty = Convert.ToInt32(f["ALQTY"]);
                        // 单据实际待出数量
                        int mblnrRealMenge = mblnrMenge - mblnrAlqty;
                        // 检查当前储位是否还有库存
                        if (storageRealMenge <= 0) {
                            continue;
                        }
                        // 如果单据出完了，就不用再循环储位
                        if (mblnrAlqty >= mblnrMenge) {
                            break;
                        }
                        // 单据数量大于等于储位数量
                        if (mblnrRealMenge >= storageRealMenge) {
                            // 需要出的数量是储位剩余数量
                            storageOut = storageRealMenge;
                            balanceQty = 0;
                        } else {
                            // 需要出的数量是单据剩余数量
                            storageOut = mblnrRealMenge;
                            balanceQty = storageRealMenge - mblnrRealMenge;
                        }
                        // 更新当前储位已存到dtOutStorage的部分，取消BLACE数量
                        dtOutStorage.Select(sbWhdwnKey.ToString()).AsEnumerable().ToList().ForEach(s => {
                            s["BLACE"] = 0;
                        });

                        // 更新储位已出数量
                        drStorage["ALQTY"] = storageOut + storageAlqty;
                        drStorage["BLACE"] = balanceQty;
                        #region 出库数据
                        dtOutStorage.ImportRow(drStorage);
                        #endregion
                        // 更新单据已出数量
                        f.SetField("ALQTY", storageOut + mblnrAlqty);
                    }
                    #endregion
                });

                dtMblnr.DataSet.Tables.Remove(dtMblnr);

                dtMblnr.TableName = "Source";
                dtOutStorage.TableName = "Destination";
                dsResult.Tables.Add(dtMblnr);
                dsResult.Tables.Add(dtOutStorage);
            } catch (System.Exception ex) {
                ERRMSG = ex.Message + "<- QueryStorageOutDateCode_AGV()";
            }

            return dsResult;
        }
        #endregion

        #region 联机（转仓）扫描实物后更新库存状态
        public bool UpdateLocatStockStatus(string strWerks, string strLgort, string strLocat) {
            bool dsResult = false;
            try {
                DataWhitm objWhitm = new DataWhitm(this.UserData);
                ArrayList alConditions = new ArrayList();
                alConditions.Add(" WERKS = '" + strWerks + "'");
                alConditions.Add(" LGORT = '" + strLgort + "'");
                alConditions.Add(" LOCAT = '" + strLocat + "'");
                objWhitm.ITEMSTATES = "Y";
                objWhitm.STOCSTATES = "Y";
                dsResult = objWhitm.EntityUpdate(alConditions);
            } catch (System.Exception ex) {
                ERRMSG = ex.Message + "<- QueryStorageOutDateCode_AGV()";
            }
            return dsResult;
        }
        #endregion

        #region 根据TASKID删除库存数据
        public string StorageOutWithTaskID(string strTaskID) {
            string strResult = string.Empty;
            try {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(" EXEC SP_StorageOutFinish_AGV '{0}' ", strTaskID);
                ControlHandleDB();
                strResult = ControlSqlAccess.GetFieldValue(strBuilder.ToString());
                ControlSqlAccess.CloseConnection();
            } catch (System.Exception ex) {
                ERRMSG = ex.Message + "<- StorageOutWithTaskID()";
            }
            return strResult;
        }
        #endregion

        #region 联机（转仓）出库删除库存数据，更新储位表
        public bool StrageOut_OutStockSave(string strWerks, string strLgort, DataTable dtMatnrAgv)
        {
            StringBuilder sbSql = new StringBuilder();
            DataWhhed objWhhed = new DataWhhed(UserData);
            DataWhitm objWhitm = new DataWhitm(UserData);
            DataWhdwn objWhdwn = new DataWhdwn(UserData);
            ArrayList alColumns = new ArrayList();
            ArrayList alConditions = new ArrayList();
            ArrayList arySQL = new ArrayList();
            ArrayList aryCheckSQL = new ArrayList();
            ArrayList aryCheckList = new ArrayList();
            StringBuilder sbCheckList = new StringBuilder();
            ArrayList arySQL1 = new ArrayList();
            ArrayList aryLocat = new ArrayList();
            LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, PROGID);
            StorageData objStorage = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);

            #region WHITM

            string strSQL = "";
            string strTempLocat = "";
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);

            for (int i = 0; i < dtMatnrAgv.Rows.Count; i++)
            {
                #region 已有库存
                alColumns.Clear();
                alConditions.Clear();
                objWhitm.ResetField();

                objWhitm.Menge = "MENGE -" + dtMatnrAgv.Rows[i]["MENGE"].ToString();
                objWhitm.Monam = CRNAM;
                objWhitm.Modat = "GetDate()";
                alConditions.Add("Mandt= '" + MANDT + "'");
                alConditions.Add("Comcd= '" + COMCD + "'");
                alConditions.Add("Werks= '" + dtMatnrAgv.Rows[i]["WERKS"].ToString() + "'");
                alConditions.Add("Lgort= '" + dtMatnrAgv.Rows[i]["LGORT"].ToString() + "'");
                alConditions.Add("Locat= '" + dtMatnrAgv.Rows[i]["LOCAT"].ToString() + "'");
                alConditions.Add("Matnr= '" + dtMatnrAgv.Rows[i]["MATNR"].ToString() + "'");
                alConditions.Add("Dacod= '" + dtMatnrAgv.Rows[i]["DACOD"].ToString() + "'");

                strSQL = objWhitm.EntityGetUpdateSql(alConditions);
                arySQL.Add(strSQL);

                #endregion

                #region delete庫存為零的
                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + dtMatnrAgv.Rows[i]["WERKS"].ToString() + "')");
                alConditions.Add("(LGORT= '" + dtMatnrAgv.Rows[i]["LGORT"].ToString() + "')");
                alConditions.Add("(LOCAT= '" + dtMatnrAgv.Rows[i]["LOCAT"].ToString() + "')");
                alConditions.Add("(MATNR= '" + dtMatnrAgv.Rows[i]["MATNR"].ToString() + "')");
                alConditions.Add("(ISNULL(DACOD,'')= '" + dtMatnrAgv.Rows[i]["DACOD"].ToString() + "')");
                alConditions.Add("(MENGE<=0)");

                arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                #endregion
            }
            #endregion

            #region  WHHED
            for (int i = 0; i < dtMatnrAgv.Rows.Count; i++)
            {
                if (strTempLocat.IndexOf(dtMatnrAgv.Rows[i]["WERKS"].ToString() + dtMatnrAgv.Rows[i]["LGORT"].ToString() + dtMatnrAgv.Rows[i]["LOCAT"].ToString()) < 0)
                {
                    sbSql.Remove(0, sbSql.Length);
                    sbSql.Append("Update WHHED set  ");
                    sbSql.AppendFormat("  LOSTS=T.TOTAL from ");
                    sbSql.AppendFormat(" (Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH (NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + dtMatnrAgv.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtMatnrAgv.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtMatnrAgv.Rows[i]["LOCAT"].ToString() + "') as T  ");
                    sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat(" and WERKS='{0}' ", dtMatnrAgv.Rows[i]["WERKS"].ToString());
                    sbSql.AppendFormat(" and LGORT='{0}' ", dtMatnrAgv.Rows[i]["LGORT"].ToString());
                    sbSql.AppendFormat(" and LOCAT='{0}' ", dtMatnrAgv.Rows[i]["LOCAT"].ToString());
                    arySQL.Add(sbSql.ToString());

                    strTempLocat += "++" + dtMatnrAgv.Rows[i]["WERKS"].ToString() + dtMatnrAgv.Rows[i]["LGORT"].ToString() + dtMatnrAgv.Rows[i]["LOCAT"].ToString();
                }
            }
            #endregion

            bool bolReturn = false;
            try
            {
                ControlHandleDB();
                ControlSqlAccess.TimeOut = 300;
                //連線SQL Server的時間拉長到5分鐘
                bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- StorageInWHEC()";
            }
            return bolReturn;
        }
        #endregion

        #region 确认入库防呆类型

        /// <summary>
        /// 确认入库防呆类型
        /// </summary>
        /// <param name="strWerk"></param>
        /// <param name="strLgort"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public bool CheckStorageInType(string strWerk, string strLgort, string strType) {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckStorageInType";
            this.ControlMethodParm = "(" + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool flage = false;
            try {
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT 1  FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='StorageIn_Type' AND  REMAK='{0}' AND CTRLNM='{1}'", strType, strWerk);
                if (!strType.Equals("PalletInStorage")) {
                    sbSql.AppendFormat(" AND CTRLC1='{0}'", strLgort);
                }
                //string strSql = "SELECT 1  FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='StorageIn_Type' "
                //                + "   AND  REMAK='" + strType + "' AND CTRLNM='" + strWerk + "' AND CTRLC1='" + strLgort + "' ";
                ControlHandleDB();
                DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                if (dtData.Rows.Count > 0) {
                    flage = true;
                }
            } catch (CommonObjectsException ex) {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex) {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            }
            return flage;
        }
        #endregion

        #region 获取虚拟储位
        public List<string> getLocation(string strWERKS, string strLGORT, string strTmpMatnr, string strTmpDacod, string strTmpLifnr, string strTmpLocod, string strTmpMenge, string strLOCATscn) {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "getLocation";
            this.ControlMethodParm = "";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string strSql = string.Empty;

            //strSql = " SELECT TOP 1 LOCAT FROM WHITM WITH(NOLOCK) WHERE WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "'" +
            //    "and MATNR = '" + strTmpMatnr + "' and DACOD = '" + strTmpDacod + "'and LIFNR = '" + strTmpLifnr + "' " +
            //    "and LOCOD = '" + strTmpLocod + "'" + 
            //    // 因为有同一个料盘分多个扣账编号入库的情况，所以不能加上数量
            //    //"and MENGE = '" + strTmpMenge + "'" +
            //    "and LOCAT like '" + strLOCATscn + "%' ";
            strSql =
                "SELECT I.WERKS, LGORT, I.LOCAT, SUM(MENGE) FROM WHITM I INNER JOIN(" +
                   " SELECT DISTINCT WERKS, LOCAT, MATNR FROM WHITM WITH(NOLOCK) WHERE WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "'" +
                "and MATNR = '" + strTmpMatnr + "' and DACOD = '" + strTmpDacod + "'and LIFNR = '" + strTmpLifnr + "' " +
                "and LOCOD = '" + strTmpLocod + "'" +
                "and LOCAT like '" + strLOCATscn + "%' " +
            ") T ON I.WERKS = T.WERKS AND I.LOCAT = T.LOCAT AND I.MATNR = T.MATNR GROUP BY I.WERKS,LGORT,I.LOCAT HAVING SUM(MENGE) = " + strTmpMenge;



            try {
                ControlHandleDB();
                return ControlSqlAccess.GetDataTable(strSql).AsEnumerable().Select(s => s.Field<string>("LOCAT")).ToList();
            } catch (CommonObjectsException ex) {
                //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                ControlExceptionType = ex.SourceExceptionType;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw ex;
            }//可自行增加要handle的Exception  
            catch (Exception ex) {
                ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                ControlExceptionType = ex.GetType().FullName;
                this.ControlPriority = "1";
                ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                throw new Exception("999");
            } finally {
                ControlSqlAccess.CloseConnection();
            }
        }
        #endregion

    }
}
