using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.Linq;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// LogData 的摘要描述。
        /// </summary>
        public class LogData : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strProgid = "";
            private string strCrnam = "";
            private string strErrmsg = "";


            #region Constructer

            public LogData()
            {

            }

            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Admin物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strProgid">程式代碼。</param>			
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public LogData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = strWerks;
                LGORT = strLgort;
                PROGID = strProgid;
                CRNAM = varUserData.UserId;

                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.LogData";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Rock Tzeng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            public LogData(UserInfo varUserData, string strWerks, string strLgort, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort, strProgid)
            {

            }

            #endregion

            #region MemberFunction


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
            /// 程式代碼。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string PROGID
            {
                get { return strProgid; }
                set { strProgid = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 建立者。
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


            #region 查詢異動記錄，補印發料單(QueryLogDataForReprint)
            //===========================================================================
            ////////////Summary by Smose Liao////////////////////////////////////////////
            /// <summary>
            /// 查詢異動記錄，補印發料單
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objLogData.QueryLogDataForReprint(strGrpid, strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryLogDataForReprint(string strGrpid, string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogDataForReprint";
                this.ControlMethodParm = "('" + strGrpid + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhlog objDataWhlog = new DataWhlog(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    alColumns.Add("*");
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS='" + WERKS + "'");
                    alCondition.Add("LGORT='" + LGORT + "'");

                    if (strGrpid != "")
                    {
                        alCondition.Add("GRPID='" + strGrpid + "'");
                    }

                    if (strMblnr != "")
                    {
                        alCondition.Add("MBLNR like '" + strMblnr + "%'");
                    }

                    DataTable dtData = new DataTable();
                    try
                    {
                        dtData = objDataWhlog.EntityQuery(alColumns, alCondition, false, true);
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- QueryLogDataForReprint()";
                    }
                    return dtData;
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
            #endregion


            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 異動紀錄查詢 strWerks:廠區, strLgort:倉別, strCgcls:異動類型, strInsmk:庫別, strCharg:料號版本, strUsrnm:異動者, strStartDate:異動開始時間, strEndDate:異動結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartLocat:儲位開始, strEndLocat:儲位結束
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objLogData.QueryLogData(strCgcls, strInsmk, strCharg, strUsrnm, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartLocat, strEndLocat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryLogData(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogData";
                this.ControlMethodParm = "('" + strCgcls + "','" + strInsmk + "','" + strCharg + "','" + strUsrnm + "','" + strStartDate + "','" + strEndDate + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartLocat + "','" + strEndLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhlog objDataWhlog = new DataWhlog(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    alColumns.Add("*");
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS='" + WERKS + "'");
                    alCondition.Add("LGORT='" + LGORT + "'");
                    // string strSQL = "Select * from WHLOG where MANDT='" + MANDT + "' and COMCD='"+COMCD+"'  and WERKS = '" + WERKS + "' and LGORT = '" + LGORT + "'";

                    if (strCgcls != "")
                    {
                        alCondition.Add("CGCLS='" + strCgcls + "'");
                        // strSQL += " and CGCLS = '" + strCgcls + "'";
                    }

                    if (strInsmk != "")
                    {
                        alCondition.Add("INSMK='" + strInsmk + "'");
                        //  strSQL += " and INSMK = '" + strInsmk + "'";
                    }

                    if (strCharg != "")
                    {
                        alCondition.Add("CHARG='" + strCharg + "'");
                        //  strSQL += " and CHARG = '" + strCharg + "'";
                    }

                    if (strUsrnm != "")
                    {
                        alCondition.Add("CRNAM='" + strUsrnm + "'");
                        // strSQL += " and CRNAM = '" + strUsrnm + "'";
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        alCondition.Add("CRDAT between'" + strStartDate + "' AND '" + strEndDate + "'");
                        //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        alCondition.Add("CRDAT='" + strEndDate + "'");
                        // strSQL += " and CRDAT = '" + strEndDate + "'";
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        alCondition.Add("CRDAT='" + strStartDate + "'");
                        //strSQL += " and CRDAT = '" + strStartDate + "'";
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        alCondition.Add("MATNR between'" + strStartMatnr + "' AND '" + strEndMatnr + "'");
                        // strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        alCondition.Add("MATNR='" + strEndMatnr + "'");
                        // strSQL += " and MATNR = '" + strEndMatnr + "'";
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        alCondition.Add("MATNR='" + strStartMatnr + "'");
                        // strSQL += " and MATNR = '" + strStartMatnr + "'";
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        alCondition.Add("((OLOCA between'" + strStartLocat + "' AND '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                        //strSQL += " and ((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))";
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        alCondition.Add("(OLOCA='" + strEndLocat + "' or NLOCA='" + strEndLocat + "')");
                        //   strSQL += " and (OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')";
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        alCondition.Add("(OLOCA='" + strStartLocat + "' or NLOCA='" + strStartLocat + "')");
                        //strSQL += " and (OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')";
                    }

                    DataTable dtData = new DataTable();
                    try
                    {
                        dtData = objDataWhlog.EntityQuery(alColumns, alCondition, false, true);
                        //ControlHandleDB();
                        //dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        //ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                    return dtData;
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

            #region QueryLogDataWithLine   (查詢異動紀錄增添品名線別信息) 20131101 Ryan Tsai
            public DataTable QueryLogDataWithLine(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat, string strStartMblnr, string strEndMblnr, string strQueryTable)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogDataWithLine";
                this.ControlMethodParm = "('" + strCgcls + "','" + strInsmk + "','" + strCharg + "','" + strUsrnm + "','" + strStartDate + "','" + strEndDate + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMblnr + "','" + strEndMblnr + "','" + strQueryTable + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtData = new DataTable();
                    sbSql.Append(" select pri.PRITY as Priority,pri.ARBPL as Line ,man.MATNM as PartName,log.* from WHLOG  as log ");
                    sbSql.Append(" left join WHPRI as pri on CharIndex(pri.ARBPL,log.ARBPL)>0 and pri.WERKS=log.WERKS  ");
                    sbSql.Append(" left join WHMAN as man on log.MATNR=man.MATNR ");
                    sbSql.Append(" where 1=1 ");

                    sbSql.Append(" and  log.MANDT = '" + UserData.Client + "'");
                    sbSql.Append(" and  log.COMCD = '" + UserData.CompanyCode + "'");
                    sbSql.Append(" and  log.WERKS = '" + WERKS + "'");
                    sbSql.Append(" and  log.LGORT = '" + LGORT + "'");
                    #region 查詢條件-非必填
                    if (strCgcls != "")
                    {
                        //strSQL += " and CGCLS = '" + strCgcls + "'";
                        sbSql.Append(" and log.CGCLS = '" + strCgcls + "'");
                    }

                    if (strInsmk != "")
                    {
                        //strSQL += " and INSMK = '" + strInsmk + "'";
                        sbSql.Append(" and log.INSMK = '" + strInsmk + "'");
                    }

                    if (strCharg != "")
                    {
                        //strSQL += " and CHARG = '" + strCharg + "'";
                        sbSql.Append(" and log.CHARG = '" + strCharg + "'");
                    }

                    if (strUsrnm != "")
                    {
                        //strSQL += " and CRNAM = '" + strUsrnm + "'";
                        sbSql.Append(" and log.CRNAM = '" + strUsrnm.ToString().Trim() + "'");
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        sbSql.Append(" and (log.CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                        //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        sbSql.Append(" and log.CRDAT = '" + strEndDate + "'");
                        //strSQL += " and CRDAT = '" + strEndDate + "'";
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        //strSQL += " and CRDAT = '" + strStartDate + "'";
                        sbSql.Append(" and log.CRDAT = '" + strStartDate + "'");
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                        sbSql.Append(" and (log.MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        //strSQL += " and MATNR = '" + strEndMatnr + "'";
                        sbSql.Append(" and log.MATNR = '" + strEndMatnr + "'");
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        //strSQL += " and MATNR = '" + strStartMatnr + "'";
                        sbSql.Append(" and log.MATNR = '" + strStartMatnr + "'");
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        //strSQL += " and ((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))";
                        sbSql.Append(" and ((log.OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (log.NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        //strSQL += " and (OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')";
                        sbSql.Append(" and (log.OLOCA = '" + strEndLocat + "' or log.NLOCA = '" + strEndLocat + "')");
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        sbSql.Append(" and (log.OLOCA = '" + strStartLocat + "' or log.NLOCA = '" + strStartLocat + "')");
                        //strSQL += " and (OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')";
                    }

                    //20070419 for document No. marc add
                    if (strStartMblnr != "" && strEndMblnr != "")
                    {
                        sbSql.Append(" and (log.MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                        //strSQL += " and (MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                    }
                    else if (strStartMblnr == "" && strEndMblnr != "")
                    {
                        sbSql.Append(" and log.MBLNR like '" + strEndMblnr + "%'");
                        //strSQL += " and MBLNR = '" + strEndMblnr + "'";
                    }
                    else if (strStartMblnr != "" && strEndMblnr == "")
                    {
                        sbSql.Append(" and log.MBLNR like '" + strStartMblnr + "%'");
                        //strSQL += " and MBLNR = '" + strStartMblnr + "'";
                    }

                    #endregion

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 異動紀錄查詢 strWerks:廠區, strLgort:倉別, strCgcls:異動類型, strInsmk:庫別, strCharg:料號版本, strUsrnm:異動者, strStartDate:異動開始時間, strEndDate:異動結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartLocat:儲位開始, strEndLocat:儲位結束,strStartMblnr:單號開始,strEndMblnr:單號結束
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objLogData.QueryLogData(strCgcls, strInsmk, strCharg, strUsrnm, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartLocat, strEndLocat,strStartMblnr,strEndMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryLogData(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat, string strStartMblnr, string strEndMblnr, string strQueryTable, string DC)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogData";
                this.ControlMethodParm = "('" + strCgcls + "','" + strInsmk + "','" + strCharg + "','" + strUsrnm + "','" + strStartDate + "','" + strEndDate + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMblnr + "','" + strEndMblnr + "','" + strQueryTable + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    //ArrayList alColumns = new ArrayList();
                    //ArrayList alQueryCondition = new ArrayList();

                    //alColumns.Add("CRDAT");
                    //alColumns.Add("CGCLS");
                    //alColumns.Add("TRNTP");
                    //alColumns.Add("WERKS");
                    //alColumns.Add("LGORT");
                    //alColumns.Add("CRDAT");
                    //alColumns.Add("OLOCA");
                    //alColumns.Add("MATNR");
                    //alColumns.Add("INSMK");
                    //alColumns.Add("CHARG");
                    //alColumns.Add("EBELN");
                    //alColumns.Add("KDMAT");
                    //alColumns.Add("SERNO");
                    //alColumns.Add("BOXID");
                    //alColumns.Add("DACOD");
                    //alColumns.Add("MENGE");
                    //alColumns.Add("MBLNR");
                    //alColumns.Add("RMANO");
                    //alColumns.Add("OMBLN");
                    //alColumns.Add("LIFNR");
                    //alColumns.Add("INDAT");
                    //alColumns.Add("CRNAM");
                    //alColumns.Add("KOSTL");
                    //alColumns.Add("ARBPL");
                    //alColumns.Add("LOCOD");
                    //alColumns.Add("GRPID");

                    //alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
                    //alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                    //alQueryCondition.Add("WERKS = '" + WERKS + "'");
                    //alQueryCondition.Add("LGORT = '" + LGORT + "'");

                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat(" Select CRDAT , CGCLS , TRNTP , WERKS , LGORT , CRDAT , OLOCA , MATNR , INSMK , CHARG , D.DC_After, EBELN , KDMAT , SERNO , BOXID , DACOD , MENGE , MBLNR , RMANO , OMBLN , L.LIFNR , INDAT , CRNAM , KOSTL , ARBPL , LOCOD , GRPID,QWQTY,RMAK1,VEDAT,TASKID,EXPDAT,MCDAT ");
                    //strSql.AppendFormat(" FROM " + strQueryTable + " WITH(NOLOCK) Where MANDT = '" + UserData.Client + "' And COMCD = '" + UserData.CompanyCode + "' And WERKS = '" + WERKS + "' ");//"' And LGORT = '" + LGORT +
                    strSql.AppendFormat(" FROM " + strQueryTable + " AS L WITH(NOLOCK) LEFT JOIN (SELECT DISTINCT DC_Before,DC_After,LIFNR,COMCD FROM WHDCR WITH(NOLOCK)) AS D ON L.LIFNR=D.LIFNR AND L.DACOD=D.DC_Before AND L.COMCD=D.COMCD ");
                    strSql.AppendFormat(" Where L.MANDT = '" + UserData.Client + "' And L.COMCD = '" + UserData.CompanyCode + "' And L.WERKS = '" + WERKS + "' ");


                    if (LGORT != "")
                    {
                        strSql.AppendFormat(" And L.LGORT = '" + LGORT + "' ");
                        //alQueryCondition.Add("CGCLS = '" + strCgcls + "'");
                    }
                    if (strCgcls != "")
                    {
                        strSql.AppendFormat(" And L.CGCLS = '" + strCgcls + "' ");
                        //alQueryCondition.Add("CGCLS = '" + strCgcls + "'");
                    }
                    else
                    {
                        strSql.AppendFormat(" And L.CGCLS <> '99' "); //Type没有选择情况下，排除99异动显示 Add By Jason 20230719
                    }

                    if (strInsmk != "")
                    {
                        strSql.AppendFormat(" And L.INSMK = '" + strInsmk + "' ");
                        //alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                    }

                    if (strCharg != "")
                    {
                        strSql.AppendFormat(" And L.CHARG = '" + strCharg + "' ");
                        //alQueryCondition.Add("CHARG = '" + strCharg + "'");
                    }

                    if (strUsrnm != "")
                    {
                        strSql.AppendFormat(" And L.CRNAM = '" + strUsrnm.ToString().Trim() + "' ");
                        //alQueryCondition.Add("CRNAM = '" + strUsrnm.ToString().Trim() + "'");
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        strSql.AppendFormat(" And (L.CRDAT between '" + strStartDate + "' and '" + strEndDate + "') ");
                        //alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        //alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                        strSql.AppendFormat(" And L.CRDAT = '" + strEndDate + "' ");
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        strSql.AppendFormat(" And L.CRDAT = '" + strStartDate + "' ");
                        //alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        strSql.AppendFormat(" And (L.MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "') ");
                        //alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        strSql.AppendFormat("  And L.MATNR = '" + strEndMatnr + "' ");
                        //alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        strSql.AppendFormat(" And L.MATNR = '" + strStartMatnr + "' ");
                        //alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        strSql.AppendFormat(" And ((L.OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (L.NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "')) ");
                        //alQueryCondition.Add("((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        strSql.AppendFormat(" And (L.OLOCA = '" + strEndLocat + "' or L.NLOCA = '" + strEndLocat + "') ");
                        //alQueryCondition.Add("(OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')");
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        strSql.AppendFormat(" And (L.OLOCA = '" + strStartLocat + "' or L.NLOCA = '" + strStartLocat + "') ");
                        //alQueryCondition.Add("(OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')");
                    }

                    //20070419 for document No. marc add
                    if (strStartMblnr != "" && strEndMblnr != "")
                    {
                        strSql.AppendFormat(" And (L.MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "') ");
                        //alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                    }
                    else if (strStartMblnr == "" && strEndMblnr != "")
                    {
                        strSql.AppendFormat(" And L.MBLNR like '" + strEndMblnr + "%' ");
                        //alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                    }
                    else if (strStartMblnr != "" && strEndMblnr == "")
                    {
                        strSql.AppendFormat(" And L.MBLNR like '" + strStartMblnr + "%' ");
                        //alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                    }

                    if (DC != "")
                        strSql.AppendFormat(" And L.DACOD ='" + DC + "' ");
                    //alQueryCondition.Add("DACOD ='" + DC + "'");

                    DataTable dtData = new DataTable();
                    try
                    {
                        //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                        //ControlHandleDB();
                        SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                        dtData = sqlAccess.GetDataTable(strSql.ToString());
                        sqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                    return dtData;
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


            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳, 入庫時NewLocat為空值dtStorage包括1.MANDT: Client2.WERKS: 廠區3.LGORT: 倉別4.LOCAT: 儲位5.MATNR: 料號6.INSMK: 庫別7.CHARG: 版本8.MENGE: 原庫存數量(如果新增庫存則此欄位為0)9.NMENG: 調整後數量12.OMBLNR: 之前入庫的單據號碼(如果新增庫存則此欄位為空值)13.MRGID: 連板編號17.EBELN: PO號碼18.LIFNR: 廠商碼19.RMAK1: 備註20.INDAT: 入庫時間
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddLogData(strLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddLogData(string strLocat, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogData";
                this.ControlMethodParm = "('" + strLocat + "','" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";

                    DataWhctrl objWhctrl = new DataWhctrl(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alQueryConditions = new ArrayList();

                    DataTable dtData = new DataTable();

                    alColumns.Add("CTRLC3");
                    alQueryConditions.Add(" SOLDTO='QWMS'");
                    alQueryConditions.Add(" CTRLID='PROGM'");
                    alQueryConditions.Add(" CTRLC2='" + PROGID + "'");
                    //string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    try
                    {
                        //ControlHandleDB();
                        //dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        //ControlSqlAccess.CloseConnection();
                        dtData = objWhctrl.EntityQuery(alColumns, alQueryConditions, false, true);
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddLogData()";
                    }

                    string strCgcls = dtData.Rows[0]["CTRLC3"].ToString();

                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) < int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()))
                        {
                            strMenge = "+" + Convert.ToString(int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) - int.Parse(dtStorage.Rows[i]["MENGE"].ToString()));
                        }
                        else if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) > int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()))
                        {
                            strMenge = "-" + Convert.ToString(int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()));
                        }
                        else
                        {
                            strMenge = "0";
                        }

                        // Quanta, Smose.Liao, 20090323：Add the Insp.Lot No,DateCode and LockCode fields into Manage_Location_Combine(併儲作業新增Insp.Lot No,DateCode與LockCode欄位)
                        //arySQL.Add("Insert into WHLOG(MANDT, WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT, SERNO, LOCOD, INSPT) values('" + 
                        //	dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" +  strCgcls + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "','','" +
                        //	dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','','','" + dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + 
                        //	dtStorage.Rows[i]["EBELN"].ToString() + "'," + strMenge + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '' , '' ,'" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" +  dtStorage.Rows[i]["INDAT"].ToString() + "','" +  dtStorage.Rows[i]["SERNO"].ToString() + "','" +  dtStorage.Rows[i]["LOCOD"].ToString() + "','" +  dtStorage.Rows[i]["INSPT"].ToString() + "')");

                        DataWhlog objWhlog = new DataWhlog(UserData);

                        objWhlog.Mandt = UserData.Client;
                        objWhlog.Comcd = UserData.CompanyCode;
                        objWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhlog.Cgcls = strCgcls;
                        objWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhlog.Nloca = "";
                        objWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhlog.Trntp = "";
                        objWhlog.Mblnr = "";
                        objWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                        objWhlog.Menge = strMenge;
                        objWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhlog.Kostl = "";
                        objWhlog.Arbpl = "";
                        objWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                        objWhlog.Crnam = UserData.UserId;
                        objWhlog.Crdat = "getdate()";
                        objWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                        objWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                        objWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();


                        //strSQL = "Insert into WHLOG(MANDT, WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT, SERNO, LOCOD, INSPT) values('" +
                        //    dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + strCgcls + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "','','" +
                        //    dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','','','" + dtStorage.Rows[i]["OMBLNR"].ToString() + "','" +
                        //    dtStorage.Rows[i]["EBELN"].ToString() + "'," + strMenge + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '' , '' ,'" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "','" + dtStorage.Rows[i]["SERNO"].ToString() + "','" + dtStorage.Rows[i]["LOCOD"].ToString() + "','" + dtStorage.Rows[i]["INSPT"].ToString() + "')";
                        //arySQL.Add(strSQL);

                        arySQL.Add(objWhlog.EntityGetInsertSql());

                    }

                    return arySQL;
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

            #region 將異動資料產生SQL指令回傳
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳, 入庫時NewLocat為空值dtStorage包括1.MANDT: Client2.WERKS: 廠區3.LGORT: 倉別4.LOCAT: 儲位5.MATNR: 料號6.INSMK: 庫別7.CHARG: 版本8.MENGE: 庫存數量(入庫時為入庫數量)9.ALQTY: 庫存可出數10.MBLNR: 單據號碼11.ZEILE: 單據item12.OMBLNR: 之前入庫的單據號碼(入庫時為空值)13.MRGID: 連板編號14.KOSTL: 部門15.ARBPL: 生產線別16TRNTP: 異動類型(G+, G-….)17.EBELN: PO號碼18.LIFNR: 廠商碼19.RMAK1: 備註20.INDAT: 入庫時間
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddLogData(strLocat,strNewLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddLogData(string strLocat, string strNewLocat, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogData";
                this.ControlMethodParm = "('" + strLocat + "','" + strNewLocat + "','" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region 變數宣告
                    DataWhctrl objDataWhctrl = new DataWhctrl(UserData);
                    DataWhlog objDataWhlog = new DataWhlog(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    ArrayList arySQL = new ArrayList();
                    bool flag_L = false;

                    string[] aryCgcls = { "" };
                    #endregion
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alCondition.Clear();
                        alColumns.Add("*");
                        alCondition.Add("SOLDTO='QWMS'");
                        alCondition.Add("CTRLID='PROGM'");
                        alCondition.Add("CTRLC2='" + PROGID + "'");

                        DataTable dtData = new DataTable();
                        try
                        {
                            dtData = objDataWhctrl.EntityQuery(alColumns, alCondition, false, true);
                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- AddLogData()";
                        }

                        if (dtData.Rows[0]["CTRLC3"].ToString().IndexOf(";") < 0)
                        {
                            aryCgcls[0] = dtData.Rows[0]["CTRLC3"].ToString();
                        }
                        else
                        {
                            aryCgcls = dtData.Rows[0]["CTRLC3"].ToString().Split(new char[] { ';' });
                        }

                        for (int j = 0; j < aryCgcls.Length; j++)
                        {
                            #region 联机入库 01, 离线入库 02, 连版入库 05,转仓入库 06 , 联机入库（成品） 07, 离线入库（成品） 08, 联机入库（SMT）09 , 联机入库（飞天计划）35 ,联机入库（SMT）批量入库 B28,自動入庫(Spare Parts), 手動入庫(Spare Parts), 離線入庫(Spare Parts) 38  39  42,棧板入庫 10   63
                            if (aryCgcls[j].ToString() == "01" || aryCgcls[j].ToString() == "02" || aryCgcls[j].ToString() == "05" || aryCgcls[j].ToString() == "06" || aryCgcls[j].ToString() == "07" ||
                                aryCgcls[j].ToString() == "08" || aryCgcls[j].ToString() == "09" || aryCgcls[j].ToString() == "35" || aryCgcls[j].ToString() == "70" ||
                                aryCgcls[j].ToString() == "60" || aryCgcls[j].ToString() == "51" || aryCgcls[j].ToString() == "B28"|| aryCgcls[j].ToString() == "62" || aryCgcls[j].ToString() == "38" || aryCgcls[j].ToString() == "39" || aryCgcls[j].ToString() == "42" || aryCgcls[j].ToString() == "10" || aryCgcls[j].ToString() == "63" || aryCgcls[j].ToString() == "V7")
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = aryCgcls[j].ToString() == "62" ? dtStorage.Rows[i]["Location"].ToString() : dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString() + aryCgcls[j].ToString() == "10" || aryCgcls[j].ToString() == "63" ? ";GL3" : "";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("LOCOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["LOCOD"].ToString()))
                                    {
                                        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("SERNO"))
                                    {
                                        objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("RMANO"))
                                    {
                                        objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("EXPDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["EXPDAT"].ToString()))
                                    {
                                        if (ClaCommon.CheckDateValid(dtStorage.Rows[i]["EXPDAT"].ToString().Trim()))
                                        {
                                            objDataWhlog.Expdat = dtStorage.Rows[i]["EXPDAT"].ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                        {
                                            objDataWhlog.Expdat = dtStorage.Rows[i]["VEDAT"].ToString();
                                        }
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Trim().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString().Trim() : "";
                                    }
                                    if (dtStorage.Columns.Contains("CONFIG"))
                                    {
                                        objDataWhlog.Config = dtStorage.Rows[i]["CONFIG"].ToString();
                                    }
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SMT退庫(PowerⅡ-QWMS)  46
                            if (aryCgcls[j].ToString() == "46") //SMT退庫(PowerⅡ-QWMS)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Trntp = "";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = "";

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 調撥入庫(Spare Parts)  44
                            if (aryCgcls[j].ToString() == "44") //調撥入庫(Spare Parts)
                            {
                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["NEWLGT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫by DateCode,轉倉入庫by DateCode  27  28   52
                            if (aryCgcls[j].ToString() == "27" || aryCgcls[j].ToString() == "28" || aryCgcls[j].ToString() == "52")  //連線入庫by DateCode,轉倉入庫by DateCode
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("LOCOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["LOCOD"].ToString()))
                                    {
                                        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("ExpiryDate") && !string.IsNullOrEmpty(dtStorage.Rows[i]["ExpiryDate"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["ExpiryDate"].ToString().Trim()) ? dtStorage.Rows[i]["ExpiryDate"].ToString().Trim(): "";
                                    }
                                    else
                                    {
                                        if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                        {
                                            objDataWhlog.Expdat = dtStorage.Rows[i]["VEDAT"].ToString();
                                        }
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("SERNO") && !string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                                    {
                                        objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString().ToUpper();
                                    }
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region DOA入庫, 銷退入庫  49  50
                            if (aryCgcls[j].ToString() == "49" || aryCgcls[j].ToString() == "50")
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫確認  30
                            if (aryCgcls[j].ToString() == "30") //連線入庫確認
                            {
                                if (int.Parse(dtStorage.Rows[i]["BKQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["BKQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫確認(Vendor Manufactured Date)  34  sam 更改，出库确认异动改成一次
                            if (aryCgcls[j].ToString() == "34")
                            {
                                if (int.Parse(dtStorage.Rows[i]["BKQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    string strRmaNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();

                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("LOCOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["LOCOD"].ToString()))
                                    {
                                        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    }
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();                                                                   
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                           
                            #endregion

                            #region 連線出庫確認(Vendor Manufactured Date)
                            //if (aryCgcls[j].ToString() == "34") //連線出庫確認(Vendor Manufactured Date)
                            //{
                            //    if (int.Parse(dtStorage.Rows[i]["BKQTY"].ToString()) != 0)
                            //    {
                            //        string strDacod = "";
                            //        if (dtStorage.Columns.IndexOf("DACOD") > -1)
                            //        {
                            //            strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                            //        }
                            //        alColumns.Clear();
                            //        alCondition.Clear();
                            //        objDataWhlog.ResetField();

                            //        objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                            //        objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                            //        objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                            //        objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                            //        objDataWhlog.Cgcls = aryCgcls[j].ToString();
                            //        objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                            //        objDataWhlog.Nloca = "";
                            //        objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                            //        objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                            //        objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                            //        objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                            //        objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                            //        objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                            //        objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                            //        objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                            //        objDataWhlog.Crnam = CRNAM;
                            //        objDataWhlog.Crdat = "getdate()";
                            //        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                            //        objDataWhlog.Dacod = strDacod;

                            //        arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            //    }
                            //}
                            #endregion

                            #region [GB]联机入库(CSMC半成品 67,判票退库 78

                            if (aryCgcls[j].ToString() == "67" || aryCgcls[j].ToString() == "78") //連線入庫確認(CSMC半成品)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    if (dtStorage.Columns.Contains("MCDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["MCDAT"].ToString()))
                                    {
                                        objDataWhlog.Mcdat = dtStorage.Rows[i]["MCDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region [GB]联机入库(Multi半成品) 72
                            if (aryCgcls[j].ToString() == "72")
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    //objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫確認(CSMC半成品)  68
                            if (aryCgcls[j].ToString() == "68") //連線出庫確認(CSMC半成品)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";

                                    //brian 201504014增加INDAT、SAP扣帐单号
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 調撥出庫(CSMC半成品)  69 
                            if (aryCgcls[j].ToString() == "69") //調撥出庫(CSMC半成品)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    //brian 201504014增加INDAT、SAP扣帐单号
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SI出庫確認  41 
                            if (aryCgcls[j].ToString() == "41") //SI出庫確認
                            {
                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["NLOCA"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["SIDNO"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫(By Picasso)  33
                            if (aryCgcls[j].ToString() == "33") //連線入庫by Picasso
                            {
                                if (int.Parse(dtStorage.Rows[i]["BXQTY"].ToString()) != 0)
                                {
                                    string strBoxid = "";
                                    if (dtStorage.Columns.IndexOf("BOXID") > -1)
                                    {
                                        strBoxid = dtStorage.Rows[i]["BOXID"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["BXQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Boxid = strBoxid;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(By Picasso)  31
                            if (aryCgcls[j].ToString() == "31") //連線出庫(by Picasso)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strBoxid = "";
                                    if (dtStorage.Columns.IndexOf("BOXID") > -1)
                                    {
                                        strBoxid = dtStorage.Rows[i]["BOXID"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Boxid = strBoxid;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 連線出庫, 離線出庫, 連板出庫, 轉倉出庫, 儲位出庫, 成品連線出庫, 成品離線出庫  11 12 15 16 17 18 20 64 40
                            if (aryCgcls[j].ToString() == "11" || aryCgcls[j].ToString() == "12" || aryCgcls[j].ToString() == "15" || aryCgcls[j].ToString() == "16" || aryCgcls[j].ToString() == "17" || aryCgcls[j].ToString() == "18" || aryCgcls[j].ToString() == "20" || aryCgcls[j].ToString() == "64" || aryCgcls[j].ToString() == "40") //連線出庫, 離線出庫, 連板出庫, 轉倉出庫, 儲位出庫, 成品連線出庫, 成品離線出庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    string strRmaNo = "";
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = strRmaNo;

                                    if (aryCgcls[j].ToString() == "12")
                                    {
                                        if (dtStorage.Rows[i]["DACOD"] != null)
                                        {
                                            if (!dtStorage.Rows[i]["DACOD"].ToString().Trim().Equals(""))
                                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                        }
                                    }

                                    if (dtStorage.Columns.Contains("EXPDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["EXPDAT"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["EXPDAT"].ToString()) ? dtStorage.Rows[i]["EXPDAT"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("CONFIG"))
                                    {
                                        objDataWhlog.Config = dtStorage.Rows[i]["CONFIG"].ToString();
                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(產生單據出庫)(PowerⅡ-QWMS)  25 45
                            if (aryCgcls[j].ToString() == "25" || aryCgcls[j].ToString() == "45") //連線出庫(產生單據出庫)(PowerⅡ-QWMS)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Grpid = dtStorage.Rows[i]["GRPID"].ToString();
                                    objDataWhlog.Qwqty = dtStorage.Rows[i]["QWQTY"].ToString();

                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("LOCOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["LOCOD"].ToString()))
                                    {
                                        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("EXPDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["EXPDAT"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["EXPDAT"].ToString()) ? dtStorage.Rows[i]["EXPDAT"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SI出庫(SpareParts), 離線出庫(SpareParts)  40 43
                            if (aryCgcls[j].ToString() == "40" || aryCgcls[j].ToString() == "43") //SI出庫(SpareParts), 離線出庫(SpareParts)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region IQC退料文档到SAP 71
                            if (aryCgcls[j].ToString() == "71")
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    //IQC退料增加退料日期记录
                                    objDataWhlog.Indat = dtStorage.Rows[i]["MCDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SI出庫(DOA)  48
                            if (aryCgcls[j].ToString() == "48") //SI出庫(DOA)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(By DateCode), 轉倉出庫(by DateCode) 26 29
                            if (aryCgcls[j].ToString() == "26" || aryCgcls[j].ToString() == "29")  //連線出庫by DateCode, 轉倉出庫by DateCode
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                    {
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    if (dtStorage.Columns.Contains("EXPDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["EXPDAT"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["EXPDAT"].ToString()) ? dtStorage.Rows[i]["EXPDAT"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 併板入庫 04
                            if (aryCgcls[j].ToString() == "04")//併板入庫
                            {
                                string strRmaNo = "";
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strNewLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    //objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;
                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("ExpiryDate") && !string.IsNullOrEmpty(dtStorage.Rows[i]["ExpiryDate"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["ExpiryDate"].ToString()) ? dtStorage.Rows[i]["ExpiryDate"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("CONFIG") && !string.IsNullOrEmpty(dtStorage.Rows[i]["CONFIG"].ToString()))
                                    {
                                        objDataWhlog.Config = dtStorage.Rows[i]["CONFIG"].ToString();
                                    }
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 併板出庫 14
                            if (aryCgcls[j].ToString() == "14")//併板出庫
                            {
                                string strRmaNo = "";
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    //objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;
                                    if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                    {
                                        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                    {
                                        objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    }
                                    if(dtStorage.Columns.Contains("ExpiryDate") && !string.IsNullOrEmpty(dtStorage.Rows[i]["ExpiryDate"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["ExpiryDate"].ToString()) ? dtStorage.Rows[i]["ExpiryDate"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }
                                    if (dtStorage.Columns.Contains("CONFIG") && !string.IsNullOrEmpty(dtStorage.Rows[i]["CONFIG"].ToString()))
                                    {
                                        objDataWhlog.Config = dtStorage.Rows[i]["CONFIG"].ToString();
                                    }
                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 儲位調整 22
                            if (aryCgcls[j].ToString() == "22")//儲位調整
                            {
                                string strRmaNo = "";
                                if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                    strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strNewLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 批次轉倉 25
                            if (aryCgcls[j].ToString() == "25")//批次轉倉
                            {
                                string strAlqty = "";
                                if (dtStorage.Rows[i]["TRNTP"].ToString() == "T-")
                                    strAlqty = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                else
                                    strAlqty = dtStorage.Rows[i]["ALQTY"].ToString();

                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region IQC入库  61
                            if (aryCgcls[j].ToString() == "61")//IQC入库
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                objDataWhlog.Menge = dtStorage.Rows[i]["ALGQTY"].ToString();
                                objDataWhlog.Rmak1 = "";
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region Add By Michael 20150603 for 调拨入库Log 73
                            if (aryCgcls[j].ToString() == "73")
                            {
                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 模具离线入库  83
                            if (aryCgcls[j].ToString() == "83")
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();
                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["ModelNO"].ToString();
                                objDataWhlog.Mblnr = "";
                                objDataWhlog.Menge = dtStorage.Rows[i]["Quantity"].ToString();
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["Remark"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Insmk = "";
                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 模具离线出库   84
                            if (aryCgcls[j].ToString() == "84")
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();
                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Insmk = "";
                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 模具调拨出入库 --johnny 20150720  81  82
                            if (aryCgcls[j].ToString() == "81" || aryCgcls[j].ToString() == "82")
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString(); ;
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                if (aryCgcls[j].ToString() == "81")
                                {
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                }
                                if (aryCgcls[j].ToString() == "82")
                                {
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                }
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["REMAK1"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 模具不用 62   
                            //if (aryCgcls[j].ToString() == "62")
                            //{
                            //    string strRmaNo = "";
                            //    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                            //        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                            //    if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                            //    {

                            //        alColumns.Clear();
                            //        alCondition.Clear();
                            //        objDataWhlog.ResetField();

                            //        objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                            //        objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                            //        objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                            //        objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                            //        objDataWhlog.Cgcls = aryCgcls[j].ToString();
                            //        objDataWhlog.Oloca = strLocat;
                            //        objDataWhlog.Nloca = "";
                            //        objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                            //        objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                            //        objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                            //        objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                            //        objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                            //        objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                            //        objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                            //        objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                            //        objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                            //        objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                            //        objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                            //        objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                            //        objDataWhlog.Crnam = CRNAM;
                            //        objDataWhlog.Crdat = "getdate()";
                            //        objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                            //        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                            //        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                            //        objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                            //        objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                            //        objDataWhlog.Rmano = strRmaNo;

                            //        arySQL.Add(objDataWhlog.EntityGetInsertSql());

                            //        alColumns.Clear();
                            //        alCondition.Clear();
                            //        objDataWhlog.ResetField();

                            //        objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                            //        objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                            //        objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                            //        objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                            //        objDataWhlog.Cgcls = aryCgcls[j].ToString();
                            //        objDataWhlog.Oloca = strNewLocat;
                            //        objDataWhlog.Nloca = "";
                            //        objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                            //        objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                            //        objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                            //        objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                            //        objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                            //        objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                            //        objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                            //        objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                            //        objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                            //        objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                            //        objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                            //        objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                            //        objDataWhlog.Crnam = CRNAM;
                            //        objDataWhlog.Crdat = "getdate()";
                            //        objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                            //        objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                            //        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                            //        objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                            //        objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                            //        objDataWhlog.Rmano = strRmaNo;

                            //        arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            //    }
                            //}
                            #endregion

                            #region 拼板操作/拆板操作 58 59
                            if (aryCgcls[j].ToString() == "58" || aryCgcls[j].ToString() == "59")//拼板操作
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = "";
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                if (aryCgcls[j].ToString() == "59")
                                {
                                    if (!string.IsNullOrEmpty(dtStorage.Rows[i]["OMBLNR"].ToString()))
                                    {
                                        objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    }
                                    else
                                    {
                                        objDataWhlog.Menge = dtStorage.Rows[i]["AddQty"].ToString();
                                    }
                                }
                                if (aryCgcls[j].ToString() == "58")
                                {
                                    if (string.IsNullOrEmpty(dtStorage.Rows[i]["OMBLNR"].ToString()))
                                    {
                                        objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    }
                                    else
                                    {
                                        objDataWhlog.Menge = dtStorage.Rows[i]["AddQty"].ToString();
                                    }
                                }
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 智能收料   B27
                            if (aryCgcls[j].ToString() == "B27" || aryCgcls[j].ToString() == "F34" || aryCgcls[j].ToString() == "T5" || aryCgcls[j].ToString() == "B30" || aryCgcls[j].ToString() == "V2" || aryCgcls[j].ToString() == "V8")  //智能收料,101入库
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = "G+";
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    if (dtStorage.Columns.Contains("SERNO"))
                                    {
                                        objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("LOCOD"))
                                    {
                                        objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    }
                                    if (dtStorage.Columns.Contains("ExpiryDate") && !string.IsNullOrEmpty(dtStorage.Rows[i]["ExpiryDate"].ToString()))
                                    {
                                        objDataWhlog.Expdat = ClaCommon.CheckDateValid(dtStorage.Rows[i]["ExpiryDate"].ToString().Trim()) ? dtStorage.Rows[i]["ExpiryDate"].ToString().Trim() : "";
                                    }
                                    if (dtStorage.Columns.Contains("TASKID") && !string.IsNullOrEmpty(dtStorage.Rows[i]["TASKID"].ToString()))
                                    {
                                        objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7" ? dtStorage.Rows[i]["TASKID"].ToString() : "";
                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 转仓311
                            if (aryCgcls[j].ToString() == "65") 
                            {
                                string strDacod = "";
                                if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                {
                                    strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                                }

                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                objDataWhlog.Trntp = "T-";
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["OMBLN"].ToString();
                                objDataWhlog.Ombln = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                objDataWhlog.Arbpl = "";
                                objDataWhlog.Mrgid = "";
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                objDataWhlog.Dacod = strDacod;
                                objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 加锁异动
                            if (aryCgcls[j].ToString() == "01" || aryCgcls[j].ToString() == "02" || aryCgcls[j].ToString() == "06" || aryCgcls[j].ToString() == "28" || aryCgcls[j].ToString() == "B28" || aryCgcls[j].ToString() == "B27" || aryCgcls[j].ToString() == "V7" || aryCgcls[j].ToString() == "T5")
                            {
                                flag_L = false;
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    if (CheckStorageInType(strWerks, strLgort, "StorageInLock"))
                                    {
                                        if (dtStorage.Columns.Contains("ExpiryDate"))
                                        {
                                            if (dtStorage.Rows[i]["ExpiryDate"].ToString() == "")
                                            {
                                                if (GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows.Count > 0)
                                                {
                                                    if (Convert.ToInt32(GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                                    {
                                                        flag_L = true;
                                                    }
                                                }
                                                else
                                                {
                                                    flag_L = true;

                                                }

                                            }
                                            else
                                            {
                                                if (Convert.ToInt32(dtStorage.Rows[i]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                                {
                                                    flag_L = true;
                                                }
                                            }
                                        }
                                        else if (dtStorage.Columns.Contains("EXPDAT"))
                                        {
                                            if (dtStorage.Rows[i]["EXPDAT"].ToString() == "")
                                            {
                                                if (GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows.Count > 0)
                                                {
                                                    if (Convert.ToInt32(GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                                    {
                                                        flag_L = true;
                                                    }
                                                }
                                                else
                                                {
                                                    flag_L = true;

                                                }

                                            }
                                            else
                                            {
                                                if (Convert.ToInt32(dtStorage.Rows[i]["EXPDAT"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                                {
                                                    flag_L = true;
                                                }
                                            }
                                        }                                                                              
                                        if (flag_L)
                                        {
                                            alColumns.Clear();
                                            alCondition.Clear();
                                            objDataWhlog.ResetField();
                                            objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                            objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                            objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                            objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                            objDataWhlog.Cgcls = "L+";
                                            objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                            objDataWhlog.Nloca = "";
                                            objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                            objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                            objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                            objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                            objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                            objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                            objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                            objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                            objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                            objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                            objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                            objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                            if (aryCgcls[j].ToString() == "28")
                                            {
                                                objDataWhlog.Expdat = dtStorage.Rows[i]["ExpiryDate"].ToString();
                                                objDataWhlog.Taskid = dtStorage.Rows[i]["TASKID"].ToString();
                                                objDataWhlog.Maxexp = dtStorage.Rows[i]["MAXEXP"].ToString();
                                            }
                                            objDataWhlog.Crnam = CRNAM;
                                            objDataWhlog.Crdat = "getdate()";
                                            objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                            objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                            if (dtStorage.Columns.Contains("DACOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["DACOD"].ToString()))
                                            {
                                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                            }
                                            if (dtStorage.Columns.Contains("LOCOD") && !string.IsNullOrEmpty(dtStorage.Rows[i]["LOCOD"].ToString()))
                                            {
                                                objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                            }
                                            if (dtStorage.Columns.Contains("VEDAT") && !string.IsNullOrEmpty(dtStorage.Rows[i]["VEDAT"].ToString()))
                                            {
                                                objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                            }
                                            arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                        }
                                    }                                   
                                }
                            }
                            #endregion

                            #region 联机(转仓)入库_AGV B31
                            if (aryCgcls[j].ToString() == "V1")
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();
                                objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region 并储出库 V9
                            if (aryCgcls[j].ToString() == "V9")
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();
                                objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = dtStorage.Rows[i]["NLOCA"].ToString();
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                objDataWhlog.Ombln = "";
                                objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                objDataWhlog.Kostl = "";
                                objDataWhlog.Arbpl = "";
                                objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                        }
                    }

                    return arySQL;
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

            #endregion

            #region 將異動資料產生SQL指令回傳SPlist Location by blank
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳, 入庫時NewLocat為空值dtStorage包括1.MANDT: Client2.WERKS: 廠區3.LGORT: 倉別4.LOCAT: 儲位5.MATNR: 料號6.INSMK: 庫別7.CHARG: 版本8.MENGE: 庫存數量(入庫時為入庫數量)9.ALQTY: 庫存可出數10.MBLNR: 單據號碼11.ZEILE: 單據item12.OMBLNR: 之前入庫的單據號碼(入庫時為空值)13.MRGID: 連板編號14.KOSTL: 部門15.ARBPL: 生產線別16TRNTP: 異動類型(G+, G-….)17.EBELN: PO號碼18.LIFNR: 廠商碼19.RMAK1: 備註20.INDAT: 入庫時間
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddLogData(strLocat,strNewLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddLogData(string strLocat, string strNewLocat, DataTable dtStorage, DataTable dtdata)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogData";
                this.ControlMethodParm = "('" + strLocat + "','" + strNewLocat + "','" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region 變數宣告
                    DataWhctrl objDataWhctrl = new DataWhctrl(UserData);
                    DataWhlog objDataWhlog = new DataWhlog(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    ArrayList arySQL = new ArrayList();
                    string[] aryCgcls = { "" };
                    #endregion
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alCondition.Clear();
                        alColumns.Add("*");
                        alCondition.Add("SOLDTO='QWMS'");
                        alCondition.Add("CTRLID='PROGM'");
                        alCondition.Add("CTRLC2='" + PROGID + "'");

                        DataTable dtData = new DataTable();
                        try
                        {
                            dtData = objDataWhctrl.EntityQuery(alColumns, alCondition, false, true);
                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- AddLogData()";
                        }

                        if (dtData.Rows[0]["CTRLC3"].ToString().IndexOf(";") < 0)
                        {
                            aryCgcls[0] = dtData.Rows[0]["CTRLC3"].ToString();
                        }
                        else
                        {
                            aryCgcls = dtData.Rows[0]["CTRLC3"].ToString().Split(new char[] { ';' });
                        }

                        for (int j = 0; j < aryCgcls.Length; j++)
                        {
                            #region 連線入庫, 離線入庫, 連板入庫, 轉倉入庫, 成品連線入庫, 成品離線入庫, SMT連線入庫
                            if (aryCgcls[j].ToString() == "01" || aryCgcls[j].ToString() == "02" || aryCgcls[j].ToString() == "05" || aryCgcls[j].ToString() == "06" || aryCgcls[j].ToString() == "07" ||
                                aryCgcls[j].ToString() == "08" || aryCgcls[j].ToString() == "09" || aryCgcls[j].ToString() == "35" || aryCgcls[j].ToString() == "62" || aryCgcls[j].ToString() == "51") //連線入庫, 離線入庫, 連板入庫, 轉倉入庫, 成品連線入庫, 成品離線入庫, SMT連線入庫,连线入库(Split)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    string strRmaNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtdata.Rows[0]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtdata.Rows[0]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtdata.Rows[0]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtdata.Rows[0]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["Location"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    // objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    // objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = DateTime.Now.ToString("yyyyMMdd");
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = strRmaNo;


                                    if (aryCgcls[j].ToString() == "02" || aryCgcls[j].ToString() == "09" || aryCgcls[j].ToString() == "B28")
                                    {

                                        if (dtStorage.Columns.Contains("DACOD"))
                                        {
                                            if (dtStorage.Rows[i]["DACOD"] != null)
                                            {
                                                if (!dtStorage.Rows[i]["DACOD"].ToString().Trim().Equals(""))
                                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                            }
                                        }

                                        if (dtStorage.Columns.Contains("LOCOD"))
                                        {
                                            //GARYWU
                                            if (dtStorage.Rows[i]["LOCOD"] != null)
                                            {
                                                if (!dtStorage.Rows[i]["LOCOD"].ToString().Trim().Equals(""))
                                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                            }
                                        }


                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 棧板入庫
                            if (aryCgcls[j].ToString() == "10") //棧板入庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString() + ";GL3";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SMT退庫(PowerⅡ-QWMS)
                            if (aryCgcls[j].ToString() == "46") //SMT退庫(PowerⅡ-QWMS)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Trntp = "";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = "";

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 自動入庫(Spare Parts), 手動入庫(Spare Parts), 離線入庫(Spare Parts)
                            if (aryCgcls[j].ToString() == "38" || aryCgcls[j].ToString() == "39" || aryCgcls[j].ToString() == "42") //自動入庫(Spare Parts), 手動入庫(Spare Parts), 離線入庫(Spare Parts)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 調撥入庫(Spare Parts)
                            if (aryCgcls[j].ToString() == "44") //調撥入庫(Spare Parts)
                            {
                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["NEWLGT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫by DateCode,轉倉入庫by DateCode
                            if (aryCgcls[j].ToString() == "27" || aryCgcls[j].ToString() == "28")  //連線入庫by DateCode,轉倉入庫by DateCode
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region DOA入庫, 銷退入庫
                            if (aryCgcls[j].ToString() == "49" || aryCgcls[j].ToString() == "50")
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();
                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫確認
                            if (aryCgcls[j].ToString() == "30") //連線入庫確認
                            {
                                if (int.Parse(dtStorage.Rows[i]["BKQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["BKQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                           

                            #region 連線出庫確認(CSMC半成品) 68
                            if (aryCgcls[j].ToString() == "68") //連線出庫確認(CSMC半成品)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion


                            #region 調撥出庫(CSMC半成品) 69
                            if (aryCgcls[j].ToString() == "69") //調撥出庫(CSMC半成品)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion


                            #region SI出庫確認
                            if (aryCgcls[j].ToString() == "41") //SI出庫確認
                            {
                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["NLOCA"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Trntp = "G-";
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["SIDNO"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線入庫(By Picasso)
                            if (aryCgcls[j].ToString() == "33") //連線入庫by Picasso
                            {
                                if (int.Parse(dtStorage.Rows[i]["BXQTY"].ToString()) != 0)
                                {
                                    string strBoxid = "";
                                    if (dtStorage.Columns.IndexOf("BOXID") > -1)
                                    {
                                        strBoxid = dtStorage.Rows[i]["BOXID"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["BXQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Boxid = strBoxid;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(By Picasso)
                            if (aryCgcls[j].ToString() == "31") //連線出庫(by Picasso)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strBoxid = "";
                                    if (dtStorage.Columns.IndexOf("BOXID") > -1)
                                    {
                                        strBoxid = dtStorage.Rows[i]["BOXID"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Boxid = strBoxid;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 連線出庫, 離線出庫, 連板出庫, 轉倉出庫, 儲位出庫, 成品連線出庫, 成品離線出庫
                            if (aryCgcls[j].ToString() == "11" || aryCgcls[j].ToString() == "12" || aryCgcls[j].ToString() == "15" || aryCgcls[j].ToString() == "16" || aryCgcls[j].ToString() == "17" || aryCgcls[j].ToString() == "18" || aryCgcls[j].ToString() == "20" || aryCgcls[j].ToString() == "40") //連線出庫, 離線出庫, 連板出庫, 轉倉出庫, 儲位出庫, 成品連線出庫, 成品離線出庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    string strRmaNo = "";
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = strRmaNo;

                                    if (aryCgcls[j].ToString() == "12")
                                    {
                                        if (dtStorage.Rows[i]["DACOD"] != null)
                                        {
                                            if (!dtStorage.Rows[i]["DACOD"].ToString().Trim().Equals(""))
                                                objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                        }
                                    }

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(產生單據出庫)(PowerⅡ-QWMS)
                            if (aryCgcls[j].ToString() == "45") //連線出庫(產生單據出庫)(PowerⅡ-QWMS)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Grpid = dtStorage.Rows[i]["GRPID"].ToString();
                                    objDataWhlog.Qwqty = dtStorage.Rows[i]["QWQTY"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SI出庫(SpareParts), 離線出庫(SpareParts)
                            if (aryCgcls[j].ToString() == "40" || aryCgcls[j].ToString() == "43") //SI出庫(SpareParts), 離線出庫(SpareParts)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region SI出庫(DOA)
                            if (aryCgcls[j].ToString() == "48") //SI出庫(DOA)
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Rmano = dtStorage.Rows[i]["RMANO"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 連線出庫(By DateCode), 轉倉出庫(by DateCode)
                            if (aryCgcls[j].ToString() == "26" || aryCgcls[j].ToString() == "29")  //連線出庫by DateCode, 轉倉出庫by DateCode
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strDacod = "";
                                    if (dtStorage.Columns.IndexOf("DACOD") > -1)
                                    {
                                        strDacod = dtStorage.Rows[i]["DACOD"].ToString().Trim();
                                    }

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                                    objDataWhlog.Dacod = strDacod;
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 併板入庫
                            if (aryCgcls[j].ToString() == "04")//併板入庫
                            {
                                string strRmaNo = "";
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strNewLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 併板出庫
                            if (aryCgcls[j].ToString() == "14")//併板出庫
                            {
                                string strRmaNo = "";
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    string strSerNo = "";
                                    if (dtStorage.Columns.IndexOf("SERNO") > -1)
                                        strSerNo = dtStorage.Rows[i]["SERNO"].ToString().Trim();
                                    if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                        strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Serno = strSerNo;
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                }
                            }
                            #endregion

                            #region 儲位調整
                            if (aryCgcls[j].ToString() == "22")//儲位調整
                            {
                                string strRmaNo = "";
                                if (dtStorage.Columns.IndexOf("RMANO") > -1)
                                    strRmaNo = dtStorage.Rows[i]["RMANO"].ToString().Trim();

                                if (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) != 0)
                                {

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strNewLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                                    objDataWhlog.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                                    objDataWhlog.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                                    objDataWhlog.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                                    objDataWhlog.Boxid = dtStorage.Rows[i]["BOXID"].ToString();
                                    objDataWhlog.Rmano = strRmaNo;

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region 批次轉倉
                            if (aryCgcls[j].ToString() == "25")//批次轉倉
                            {
                                string strAlqty = "";
                                if (dtStorage.Rows[i]["TRNTP"].ToString() == "T-")
                                    strAlqty = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                else
                                    strAlqty = dtStorage.Rows[i]["ALQTY"].ToString();

                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {

                                    alColumns.Clear();
                                    alCondition.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());
                                }
                            }
                            #endregion

                            #region IQC 检验出入库 54
                            if (aryCgcls[j].ToString() == "54")//檢驗入出庫
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                if (strNewLocat == "StorageOut")
                                {
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["ALQTY"].ToString();
                                }
                                objDataWhlog.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion

                            #region IQC入库 61
                            if (aryCgcls[j].ToString() == "61")//IQC入库
                            {
                                alColumns.Clear();
                                alCondition.Clear();
                                objDataWhlog.ResetField();

                                objDataWhlog.Mandt = MANDT;
                                objDataWhlog.Comcd = COMCD;
                                objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                objDataWhlog.Oloca = dtStorage.Rows[i]["LOCAT"].ToString();
                                objDataWhlog.Nloca = "";
                                objDataWhlog.Trntp = "";
                                objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                objDataWhlog.Mblnr = "EC";
                                objDataWhlog.Menge = dtStorage.Rows[i]["ALGQTY"].ToString();
                                objDataWhlog.Rmak1 = dtStorage.Rows[i]["ALSQTY"].ToString();
                                objDataWhlog.Crnam = CRNAM;
                                objDataWhlog.Crdat = "getdate()";
                                objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();

                                arySQL.Add(objDataWhlog.EntityGetInsertSql());
                            }
                            #endregion
                        }
                    }

                    return arySQL;
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

            #endregion


            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳, 入庫時NewLocat為空值dtStorage包括1.MANDT: Client2.WERKS: 廠區3.LGORT: 倉別4.LOCAT: 儲位5.MATNR: 料號6.INSMK: 庫別7.CHARG: 版本8.MENGE: 庫存數量(入庫時為入庫數量)9.ALQTY: 庫存可出數10.MBLNR: 單據號碼11.ZEILE: 單據item12.OMBLNR: 之前入庫的單據號碼(入庫時為空值)13.MRGID: 連板編號14.KOSTL: 部門15.ARBPL: 生產線別16TRNTP: 異動類型(G+, G-….)17.EBELN: PO號碼18.LIFNR: 廠商碼19.RMAK1: 備註20.INDAT: 入庫時間
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddCombineLogData(strLocat,strNewLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddCombineLogData(string strLocat, string strNewLocat, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddCombineLogData";
                this.ControlMethodParm = "('" + strLocat + "','" + strNewLocat + "','" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string[] aryCgcls = { "" };

                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhlog objDataWhlog = new DataWhlog(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtData = new DataTable();
                    alColumns.Clear();
                    alConditions.Clear();
                    alColumns.Add("CTRLC3");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='PROGM'");
                    alConditions.Add(" CTRLC2='" + PROGID + "'");
                    // string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    try
                    {
                        //ControlHandleDB();
                        //dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        //ControlSqlAccess.CloseConnection();
                        dtData = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddLogData()";
                    }

                    if (dtData.Rows[0]["CTRLC3"].ToString().IndexOf(";") < 0)
                    {
                        aryCgcls[0] = dtData.Rows[0]["CTRLC3"].ToString();
                    }
                    else
                    {
                        aryCgcls = dtData.Rows[0]["CTRLC3"].ToString().Split(new char[] { ';' });
                    }

                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        for (int j = 0; j < aryCgcls.Length; j++)
                        {

                            if (aryCgcls[j].ToString() == "04")//併板入庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["OUTGE"].ToString()) != 0)
                                {

                                    alColumns.Clear();
                                    alConditions.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strNewLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = dtStorage.Rows[i]["OUTGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                    //arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '" +
                                    //    dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + aryCgcls[j].ToString() + "','" + strNewLocat + "','','" +
                                    //    dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','" + dtStorage.Rows[i]["TRNTP"].ToString() + "','" + dtStorage.Rows[i]["MBLNR"].ToString() + "','" +
                                    //    dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + dtStorage.Rows[i]["EBELN"].ToString() + "'," + dtStorage.Rows[i]["OUTGE"].ToString() + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '" + dtStorage.Rows[i]["KOSTL"].ToString() + "','" +
                                    //    dtStorage.Rows[i]["ARBPL"].ToString() + "','" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "')");
                                }
                            }

                            if (aryCgcls[j].ToString() == "14")//併板出庫
                            {

                                if (int.Parse(dtStorage.Rows[i]["OUTGE"].ToString()) != 0)
                                {
                                    alColumns.Clear();
                                    alConditions.Clear();
                                    objDataWhlog.ResetField();

                                    objDataWhlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                                    objDataWhlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                                    objDataWhlog.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                                    objDataWhlog.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                                    objDataWhlog.Cgcls = aryCgcls[j].ToString();
                                    objDataWhlog.Oloca = strLocat;
                                    objDataWhlog.Nloca = "";
                                    objDataWhlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                                    objDataWhlog.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                                    objDataWhlog.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                                    objDataWhlog.Trntp = dtStorage.Rows[i]["TRNTP"].ToString();
                                    objDataWhlog.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                                    objDataWhlog.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                                    objDataWhlog.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                                    objDataWhlog.Menge = "-" + dtStorage.Rows[i]["OUTGE"].ToString();
                                    objDataWhlog.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                                    objDataWhlog.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                                    objDataWhlog.Arbpl = dtStorage.Rows[i]["ARBPL"].ToString();
                                    objDataWhlog.Mrgid = dtStorage.Rows[i]["MRGID"].ToString();
                                    objDataWhlog.Crnam = CRNAM;
                                    objDataWhlog.Crdat = "getdate()";
                                    objDataWhlog.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                                    objDataWhlog.Indat = dtStorage.Rows[i]["INDAT"].ToString();

                                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                                    //arySQL.Add("Insert into WHLOG(MANDT,COMCD, WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '" +
                                    //    dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + aryCgcls[j].ToString() + "','" + strLocat + "','','" +
                                    //    dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','" + dtStorage.Rows[i]["TRNTP"].ToString() + "','" + dtStorage.Rows[i]["MBLNR"].ToString() + "','" +
                                    //    dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + dtStorage.Rows[i]["EBELN"].ToString() + "'," + "-" + dtStorage.Rows[i]["OUTGE"].ToString() + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '" + dtStorage.Rows[i]["KOSTL"].ToString() + "','" +
                                    //    dtStorage.Rows[i]["ARBPL"].ToString() + "','" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "')");
                                }
                            }

                            if (aryCgcls[j].ToString() == "22")//儲位調整
                            {

                                if (int.Parse(dtStorage.Rows[i]["OUTGE"].ToString()) != 0)
                                {
                                    arySQL.Add("Insert into WHLOG(MANDT,COMCD, WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '" +
                                        dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + aryCgcls[j].ToString() + "','" + strLocat + "','','" +
                                        dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','" + dtStorage.Rows[i]["TRNTP"].ToString() + "','" + dtStorage.Rows[i]["MBLNR"].ToString() + "','" +
                                        dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + dtStorage.Rows[i]["EBELN"].ToString() + "'," + "-" + dtStorage.Rows[i]["OUTGE"].ToString() + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '" + dtStorage.Rows[i]["KOSTL"].ToString() + "','" +
                                        dtStorage.Rows[i]["ARBPL"].ToString() + "','" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "')");

                                    arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '" +
                                        dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + aryCgcls[j].ToString() + "','" + strNewLocat + "','','" +
                                        dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','" + dtStorage.Rows[i]["TRNTP"].ToString() + "','" + dtStorage.Rows[i]["MBLNR"].ToString() + "','" +
                                        dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + dtStorage.Rows[i]["EBELN"].ToString() + "'," + dtStorage.Rows[i]["OUTGE"].ToString() + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '" + dtStorage.Rows[i]["KOSTL"].ToString() + "','" +
                                        dtStorage.Rows[i]["ARBPL"].ToString() + "','" + dtStorage.Rows[i]["MRGID"].ToString() + "','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RMAK1"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "')");
                                }
                            }
                        }
                    }

                    return arySQL;
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

            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 紀錄盤點調帳資料
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddLogData(dtCoungint);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddLogData(DataTable dtCounting)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogData";
                this.ControlMethodParm = "('" + dtCounting + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    ArrayList arySQL = new ArrayList();
                    string[] aryCgcls = { "" };
                    string strAdjust = "0";

                    for (int i = 0; i < dtCounting.Rows.Count; i++)
                    {
                        string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                        DataTable dtData = new DataTable();
                        try
                        {
                            ControlHandleDB();
                            dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                            ControlSqlAccess.CloseConnection();
                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- AddLogData()";
                        }

                        if (dtData.Rows[0]["CTRLC3"].ToString().IndexOf(";") < 0)
                        {
                            aryCgcls[0] = dtData.Rows[0]["CTRLC3"].ToString();
                        }
                        else
                        {
                            aryCgcls = dtData.Rows[0]["CTRLC3"].ToString().Split(new char[] { ';' });
                        }

                        for (int j = 0; j < aryCgcls.Length; j++)
                        {
                            if (aryCgcls[j].ToString() == "23")//盤點調帳
                            {
                                if (int.Parse(dtCounting.Rows[i]["MENGE"].ToString()) >= int.Parse(dtCounting.Rows[i]["ALQTY"].ToString()))
                                {
                                    strAdjust = "+" + Convert.ToString(int.Parse(dtCounting.Rows[i]["MENGE"].ToString()) - int.Parse(dtCounting.Rows[i]["ALQTY"].ToString()));
                                }
                                else
                                {
                                    strAdjust = "-" + Convert.ToString(int.Parse(dtCounting.Rows[i]["ALQTY"].ToString()) - int.Parse(dtCounting.Rows[i]["MENGE"].ToString()));
                                }
                                arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '" +
                                    dtCounting.Rows[i]["MANDT"].ToString() + "','" + dtCounting.Rows[i]["COMCD"].ToString() + "','" + dtCounting.Rows[i]["WERKS"].ToString() + "','" + dtCounting.Rows[i]["LGORT"].ToString() + "','" + aryCgcls[j].ToString() + "','" + dtCounting.Rows[i]["LOCAT"].ToString() + "','','" +
                                    dtCounting.Rows[i]["MATNR"].ToString() + "','" + dtCounting.Rows[i]["CHARG"].ToString() + "','','','','','','" + strAdjust + "','" + dtCounting.Rows[i]["INSMK"].ToString() + "', '" + dtCounting.Rows[i]["KOSTL"].ToString() + "','','','" + CRNAM + "', getdate() ,'','')");
                            }
                        }
                    }

                    return arySQL;
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

            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddReplenishmentLogData(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddReplenishmentLogData(DataTable dtStorage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddReplenishmentLogData";
                this.ControlMethodParm = "('" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";

                    string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    DataTable dtData = new DataTable();
                    try
                    {

                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddReplenishmentLogData()";
                    }

                    string strCgcls = dtData.Rows[0]["CTRLC3"].ToString();

                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values('" +
                            dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + strCgcls + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "','" + dtStorage.Rows[i]["TOLOC"].ToString() + "','" +
                            dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','','G-','','',''," + dtStorage.Rows[i]["ALQTY"].ToString() + ",'" + dtStorage.Rows[i]["INSMK"].ToString() + "', '' , '' ,'','" + CRNAM + "', getdate() ,'" + dtStorage.Rows[i]["RESNO"].ToString() + "','')");
                    }

                    return arySQL;
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

            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生SQL指令回傳
            /// </summary> 
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objLogData.AddReplenishmentLogData(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddReplenishmentLogData(ArrayList arrResno, ArrayList arrMandt, ArrayList arrComcd, ArrayList arrWerks, ArrayList arrLgort, ArrayList arrLocat, ArrayList arrToloc, ArrayList arrMblnr, ArrayList arrMatnr, ArrayList arrInsmk, ArrayList arrCharg, ArrayList arrMenge)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddReplenishmentLogData";
                this.ControlMethodParm = "('" + arrResno + "','" + arrMandt + "','" + arrWerks + "','" + arrLgort + "','" + arrToloc + "','" + arrMblnr + "','" + arrMatnr + "','" + arrInsmk + "','" + arrCharg + "','" + arrMenge + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";

                    string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    DataTable dtData = new DataTable();
                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddReplenishmentLogData()";
                    }

                    string strCgcls = dtData.Rows[0]["CTRLC3"].ToString();

                    for (int i = 0; i < arrResno.Count; i++)
                    {
                        arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values('" +
                            arrMandt[i].ToString() + "','" + arrComcd[i].ToString() + "','" + arrWerks[i].ToString() + "','" + arrLgort[i].ToString() + "','" + strCgcls + "','','" + arrLocat[i].ToString() + "','" +
                            arrMatnr[i].ToString() + "','" + arrCharg[i].ToString() + "','','G-','','',''," + arrMenge[i].ToString() + ",'" + arrInsmk[i].ToString() + "', '' , '' ,'','" + CRNAM + "', getdate() ,'" + arrResno[i].ToString() + "','')");

                        arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values('" +
                            arrMandt[i].ToString() + "','" + arrComcd[i].ToString() + "','" + arrWerks[i].ToString() + "','" + arrLgort[i].ToString() + "','" + strCgcls + "','','" + arrToloc[i].ToString() + "','" +
                            arrMatnr[i].ToString() + "','" + arrCharg[i].ToString() + "','','G+','','',''," + arrMenge[i].ToString() + ",'" + arrInsmk[i].ToString() + "', '' , '' ,'','" + CRNAM + "', getdate() ,'" + arrResno[i].ToString() + "','')");
                    }

                    return arySQL;
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

            public string AddLogData(UserInfo UserData, string varWerks, string varLgort, string varCgcls)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogData";
                this.ControlMethodParm = "('" + UserData.Client + "','" + varWerks + "','" + varLgort + "','" + varCgcls + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataWhlog objWhlog = new DataWhlog(UserData);
                    objWhlog.Mandt = UserData.Client;
                    objWhlog.Comcd = UserData.CompanyCode;
                    objWhlog.Werks = varWerks;
                    objWhlog.Lgort = varLgort;
                    objWhlog.Cgcls = varCgcls;
                    objWhlog.Oloca = "ALL";
                    objWhlog.Nloca = "";
                    objWhlog.Matnr = "All";
                    objWhlog.Charg = "";
                    objWhlog.Lifnr = "";
                    objWhlog.Trntp = "";
                    objWhlog.Mblnr = "";
                    objWhlog.Ombln = "";
                    objWhlog.Ebeln = "";
                    objWhlog.Menge = "0";
                    objWhlog.Insmk = "";
                    objWhlog.Kostl = "";
                    objWhlog.Arbpl = "";
                    objWhlog.Mrgid = "";
                    objWhlog.Crnam = UserData.UserId;
                    objWhlog.Crdat = "getdate()";
                    objWhlog.Rmak1 = "";
                    objWhlog.Indat = "";

                    //string strSQL = "Insert into WHLOG(MANDT, WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR, TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT) values( '','" + varWerks + "','" + varLgort + "','" + varCgcls + "','ALL','','ALL','','','','','','','0','', '','','','" + CRNAM + "', getdate() ,'','')";

                    return objWhlog.EntityGetInsertSql();
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


            #region QueryLogData_AGV   (查詢異動紀錄 for AGV Kitting flow)
            ////////////Summary by Ryan Tsai (20131127)//////////////////////////////////
            /// <summary>
            /// 若於異動紀錄功能,輸入AGV Kitting相關查詢條件,查詢相關異動資料
            /// </summary> 
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryLogData_AGV(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate,
                                            string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat,
                                            string strStartMblnr, string strEndMblnr, string strQueryTable,
                                            string strAGVLine, string strAGVSendID, string strAGVMATNM, string strAGVStartDate, string strAGVEndDate,
                                            string strAGVStartTime, string strAGVEndTime, string strBWART)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogData_AGV";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append(" select PRI.ARBPL AS LINE, MAN.MATNM, DWN.REFID AS SENDID, DWN.PRITY, DWN.BUDAT, ");
                    sbSql.Append(" substring(DWN.PRITY,1, 2)+':'+substring(DWN.PRITY,3, 2) AS SendIDTime, ");
                    sbSql.Append(" LOG.CRDAT, LOG.CGCLS, LOG.TRNTP, LOG.WERKS, ");
                    sbSql.Append(" LOG.LGORT, LOG.OLOCA, LOG.MATNR, LOG.INSMK, LOG.CHARG, ");
                    sbSql.Append(" LOG.EBELN, LOG.KDMAT, LOG.SERNO, LOG.BOXID, LOG.DACOD, ");
                    sbSql.Append(" LOG.MENGE, LOG.MBLNR, LOG.OMBLN, LOG.LIFNR, LOG.INDAT, ");
                    sbSql.Append(" LOG.CRNAM, LOG.KOSTL, LOG.ARBPL, LOG.LOCOD, LOG.INSPT, LOG.GRPID ");

                    sbSql.Append(" from WHLOG AS LOG with(nolock) ");
                    sbSql.Append(" left join WHDWN AS DWN with(nolock) ");      //left join WHDWN
                    sbSql.Append(" on LOG.MBLNR=DWN.MBLNR ");

                    sbSql.Append(" left join WHMAN AS MAN with(nolock) ");      //left join WHMAN (料號品名)
                    sbSql.Append(" on LOG.MATNR=MAN.MATNR ");

                    sbSql.Append(" left join WHPRI AS PRI with(nolock) ");     //left join WHPRI (線別順序)
                    sbSql.Append(" on CharIndex(PRI.ARBPL,substring(DWN.ARBPL,1,5))>0 and PRI.WERKS=DWN.WERKS AND PRI.ARBPL=DWN.ARBPL  ");

                    sbSql.Append(" where 1=1 ");


                    #region where condition- General
                    sbSql.AppendFormat(" and LOG.MANDT='{0}' ", UserData.Client);
                    sbSql.AppendFormat(" and LOG.COMCD='{0}' ", UserData.CompanyCode);
                    sbSql.AppendFormat(" and LOG.WERKS='{0}' ", WERKS);
                    sbSql.AppendFormat(" and LOG.LGORT='{0}' ", LGORT);
                    #endregion
                    #region where condition- Normal Query
                    if (strCgcls != "")
                    {
                        //strSQL += " and CGCLS = '" + strCgcls + "'";
                        //alQueryCondition.Add("CGCLS = '" + strCgcls + "'");
                        sbSql.AppendFormat(" and LOG.CGCLS='{0}' ", strCgcls);
                    }

                    if (strInsmk != "")
                    {
                        //strSQL += " and INSMK = '" + strInsmk + "'";
                        //alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                        sbSql.AppendFormat(" and LOG.INSMK='{0}' ", strInsmk);
                    }

                    if (strCharg != "")
                    {
                        //strSQL += " and CHARG = '" + strCharg + "'";
                        //alQueryCondition.Add("CHARG = '" + strCharg + "'");
                        sbSql.AppendFormat(" and LOG.CHARG='{0}' ", strCharg);
                    }

                    if (strUsrnm != "")
                    {
                        //strSQL += " and CRNAM = '" + strUsrnm + "'";
                        //alQueryCondition.Add("CRNAM = '" + strUsrnm.ToString().Trim() + "'");
                        sbSql.AppendFormat(" and LOG.CRNAM='{0}' ", strUsrnm.ToString().Trim());
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        //alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                        //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                        sbSql.AppendFormat(" and (LOG.CRDAT between '{0}' and '{1}' ) ", strStartDate, strEndDate);
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        //alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                        //strSQL += " and CRDAT = '" + strEndDate + "'";
                        sbSql.AppendFormat(" and LOG.CRDAT='{0}' ", strEndDate);
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        //strSQL += " and CRDAT = '" + strStartDate + "'";
                        //alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                        sbSql.AppendFormat(" and LOG.CRDAT='{0}' ", strStartDate);
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                        // alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                        sbSql.AppendFormat(" and (LOG.MATNR between '{0}' and '{1}' ) ", strStartMatnr, strEndMatnr);
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        //strSQL += " and MATNR = '" + strEndMatnr + "'";
                        //alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                        sbSql.AppendFormat(" and LOG.MATNR='{0}' ", strEndMatnr);
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        //strSQL += " and MATNR = '" + strStartMatnr + "'";
                        //alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                        sbSql.AppendFormat(" and LOG.MATNR='{0}' ", strStartMatnr);
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        //strSQL += " and ((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))";
                        //alQueryCondition.Add("((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                        sbSql.AppendFormat(" and (LOG.OLOCA between '{0}' and '{1}' ) ", strStartLocat, strEndLocat);
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        //strSQL += " and (OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')";
                        //alQueryCondition.Add("(OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')");
                        sbSql.AppendFormat(" and ( LOG.OLOCA='{0}' or LOG.NLOCA='{1}' ) ", strEndLocat, strEndLocat);
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        //alQueryCondition.Add("(OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')");
                        //strSQL += " and (OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')";
                        sbSql.AppendFormat(" and ( LOG.OLOCA='{0}' or LOG.NLOCA='{1}' ) ", strStartLocat, strStartLocat);
                    }

                    //20070419 for document No. marc add
                    if (strStartMblnr != "" && strEndMblnr != "")
                    {
                        //alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                        //strSQL += " and (MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                        sbSql.AppendFormat(" and (LOG.MBLNR between '{0}' and '{1}' ) ", strStartMblnr, strEndMblnr);
                    }
                    else if (strStartMblnr == "" && strEndMblnr != "")
                    {
                        //alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                        //strSQL += " and MBLNR = '" + strEndMblnr + "'";
                        sbSql.AppendFormat(" and LOG.MBLNR like '{0}%'", strEndMblnr);
                    }
                    else if (strStartMblnr != "" && strEndMblnr == "")
                    {
                        //alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                        //strSQL += " and MBLNR = '" + strStartMblnr + "'";
                        sbSql.AppendFormat(" and LOG.MBLNR like '{0}%'", strStartMblnr);
                    }
                    #endregion
                    #region where condition- AGV Kitting Query
                    if (strBWART != "")
                    {
                        sbSql.AppendFormat(" and DWN.BWART='{0}'", strBWART);
                    }


                    if (strAGVLine != "")   //線別
                    {
                        sbSql.AppendFormat(" and PRI.ARBPL='{0}' ", strAGVLine);
                    }
                    if (strAGVSendID != "") //SendID
                    {
                        sbSql.AppendFormat(" and DWN.REFID='{0}' ", strAGVSendID);
                    }
                    if (strAGVMATNM != "") //品名
                    {
                        sbSql.AppendFormat(" and MAN.MATNM like '{0}%' ", strAGVMATNM);
                    }

                    //發料日期
                    if (strAGVStartDate != "" && strAGVEndDate != "")
                    {
                        sbSql.AppendFormat(" and (DWN.BUDAT between '{0}' and '{1}' ) ", strAGVStartDate, strAGVEndDate);
                    }
                    else if (strAGVStartDate == "" && strAGVEndDate != "")
                    {
                        sbSql.AppendFormat(" and DWN.BUDAT < '{0}' ", strAGVEndDate);
                    }
                    else if (strAGVStartDate != "" && strAGVEndDate == "")
                    {
                        sbSql.AppendFormat(" and DWN.BUDAT > '{0}' ", strAGVStartDate);
                    }
                    else
                    {
                    }

                    //發料時間
                    if (strAGVStartTime != "" && strAGVEndTime != "")
                    {
                        sbSql.AppendFormat(" and (DWN.PRITY between '{0}' and '{1}' ) ", strAGVStartTime, strAGVEndTime);
                    }
                    else if (strAGVStartTime == "" && strAGVEndTime != "")
                    {
                        sbSql.AppendFormat(" and DWN.PRITY <= '{0}' ", strAGVEndTime);
                    }
                    else if (strAGVStartTime != "" && strAGVEndTime == "")
                    {
                        sbSql.AppendFormat(" and DWN.PRITY >= '{0}' ", strAGVStartTime);
                    }
                    else
                    {
                    }
                    #endregion

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    return dtData;
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
            #endregion

            #endregion

            #region GenerID_101
            public int GenerID_101()
            {
                string strSQL = "";
                int id = 0;
                DataTable dt_temp = new DataTable();

                try
                {
                    ControlHandleDB();

                    strSQL = "select Top 1 ADD_ID from WHLOG_PAL101 order by ADD_ID DESC";
                    dt_temp = ControlSqlAccess.GetDataTable(strSQL);

                    if (dt_temp.Rows.Count == 0)
                        id = 1;
                    else
                        id = Convert.ToInt32(dt_temp.Rows[0][0].ToString()) + 1;

                    ControlSqlAccess.CloseConnection();
                    return id;

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

            #endregion

            #region OlinePALLogtxt101
            public void OlinePALLogtxt101(DataTable dt, DataTable dtResponse, int id, string type, string type2)
            {
                string strSQL = "";
                string OMBLN = "";
                string ERRMSG = "";
                string RID = "";
                string PID = "";
                DataTable dt_temp = new DataTable();

                try
                {
                    ControlHandleDB();

                    #region 帶出此票單據的PID及RID


                    if (type2.Equals("RID"))//刷ReferenceID入庫
                    {

                        RID = dt.Rows[0]["ZPALLETID"].ToString();
                        strSQL = "select TOP 1 MBLNR from whdwn where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + WERKS + "'";
                        strSQL = strSQL + " and LGORT='" + LGORT + "' and  MENGE>OTQTY and MTYPE='QMS_M'and REFID='" + RID + "'";
                        PID = ControlSqlAccess.GetFieldValue(strSQL);
                    }
                    else if (type2.Equals("PID"))//刷PalletID入庫
                    {
                        PID = dt.Rows[0]["ZPALLETID"].ToString();
                        strSQL = "select TOP 1 REFID from whdwn where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + WERKS + "'";
                        strSQL = strSQL + " and LGORT='" + LGORT + "' and  MENGE>OTQTY and MTYPE='QMS_M'and MBLNR='" + PID + "'";
                        RID = ControlSqlAccess.GetFieldValue(strSQL);
                    }

                    #endregion


                    strSQL = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OMBLN = ERRMSG = "";


                        if (type.Equals("return"))//為回傳文檔
                        {
                            #region 抓出dtResponse內的扣帳標號及錯誤訊息資訊

                            if (dtResponse.Rows[i]["OMBLN1"] != null)
                            {
                                if (dtResponse.Rows[i]["OMBLN1"].ToString().Trim() != "")
                                    OMBLN = dtResponse.Rows[i]["OMBLN1"].ToString().Trim();
                            }

                            if (dtResponse.Rows[i]["OMBLN2"] != null)
                            {
                                if (dtResponse.Rows[i]["OMBLN2"].ToString().Trim() != "")
                                    OMBLN = dtResponse.Rows[i]["OMBLN2"].ToString().Trim();
                            }

                            if (dtResponse.Rows[i]["ERRMSG"] != null)
                            {
                                if (dtResponse.Rows[i]["ERRMSG"].ToString().Trim() != "")
                                    ERRMSG = dtResponse.Rows[i]["ERRMSG"].ToString().Trim();
                            }

                            #endregion

                            strSQL = strSQL + " insert into WHLOG_PAL101 (ADD_ID, ZPALLETID, PLANT, COSTCENTER, WO, QTY, MATERIAL, REGION, LOCA, REF1, REF2, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_ERROR,OMBLN,ADD_TYPE2,ADD_RID,ADD_PID)";
                            strSQL = strSQL + " values('" + id.ToString() + "','" + dt.Rows[i]["ZPALLETID"] + "','" + dt.Rows[i]["PLANT"] + "','" + dt.Rows[i]["COSTCENTER"] + "','" + dt.Rows[i]["WO"] + "','" + dt.Rows[i]["QTY"] + "','" + dt.Rows[i]["MATERIAL"] + "','" + dt.Rows[i]["REGION"] + "','" + dt.Rows[i]["LOCA"] + "','" + dt.Rows[i]["REF1"] + "','" + dt.Rows[i]["REF2"] + "','" + type + "',getdate(),'" + CRNAM + "','" + ERRMSG + "','" + OMBLN + "','" + type2 + "','" + RID + "','" + PID + "')";
                        }
                        else if (type.Equals("send"))//為送出文檔
                        {
                            strSQL = strSQL + " insert into WHLOG_PAL101 (ADD_ID, ZPALLETID, PLANT, COSTCENTER, WO, QTY, MATERIAL, REGION, LOCA, REF1, REF2, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_TYPE2,ADD_RID,ADD_PID)";
                            strSQL = strSQL + " values('" + id.ToString() + "','" + dt.Rows[i]["ZPALLETID"] + "','" + dt.Rows[i]["PLANT"] + "','" + dt.Rows[i]["COSTCENTER"] + "','" + dt.Rows[i]["WO"] + "','" + dt.Rows[i]["QTY"] + "','" + dt.Rows[i]["MATERIAL"] + "','" + dt.Rows[i]["REGION"] + "','" + dt.Rows[i]["LOCA"] + "','" + dt.Rows[i]["REF1"] + "','" + dt.Rows[i]["REF2"] + "','" + type + "',getdate(),'" + CRNAM + "','" + type2 + "','" + RID + "','" + PID + "')";
                        }
                    }

                    ControlSqlAccess.ExecSql(strSQL);
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
            }
            #endregion

            #region multiPALLog101
            /// <summary>
            /// 批量把扣账编号加入该表WHLOG_PAL101
            /// </summary>
            /// <param name="dt"></param>
            /// <param name="dtResponse">SAP返回扣账</param>
            /// <param name="id">ADD_ID：WHLOG_PAL101的id</param>
            /// <param name="type">return（sap返回）还是send（发送给sap）</param>
            /// <param name="type2"></param>
            public void multiPALLog101(DataTable dt, DataTable dtResponse, int id, string type, string type2)
            {
                StringBuilder strsql = new StringBuilder();
                string RID = "";
                string PID = "";
                try
                {
                    ControlHandleDB();
                    var l1 = (from d in dt.AsEnumerable() select d.Field<string>("ZPALLETID")).Distinct().ToList();//L1为传给SAP的单据放在List
                    for (int j = 0; j < l1.Count; j++)
                    {
                        #region 查询PID/RID
                        strsql.Clear();
                        if (type2.Equals("RID"))//刷ReferenceID入庫
                        {
                            RID = l1[j].ToString();
                            strsql.AppendFormat("select TOP 1 MBLNR from whdwn WITH(NOLOCK) where ");
                            strsql.AppendFormat("MANDT='{0}' ", MANDT);
                            strsql.AppendFormat("and COMCD='{0}' ", COMCD);
                            strsql.AppendFormat("and WERKS='{0}' ", WERKS);
                            strsql.AppendFormat("and LGORT='{0}' ", LGORT);
                            strsql.AppendFormat("and  MENGE>OTQTY and MTYPE='QMS_M'and REFID='{0}' ", RID);
                            PID = ControlSqlAccess.GetFieldValue(strsql.ToString());
                        }
                        else if (type2.Equals("PID"))//刷PalletID入庫
                        {
                            PID = l1[j].ToString();
                            strsql.AppendFormat("select TOP 1 REFID from whdwn WITH(NOLOCK) where ");
                            strsql.AppendFormat("MANDT='{0}' ", MANDT);
                            strsql.AppendFormat("and COMCD='{0}' ", COMCD);
                            strsql.AppendFormat("and WERKS='{0}' ", WERKS);
                            strsql.AppendFormat("and LGORT='{0}' ", LGORT);
                            strsql.AppendFormat("and  MENGE>OTQTY and MTYPE='QMS_M'and MBLNR='{0}' ", PID);
                            RID = ControlSqlAccess.GetFieldValue(strsql.ToString());
                        }
                        #endregion
                        strsql.Clear();
                        DataTable dtZPALLETID = dt.Select("ZPALLETID='" + l1[j].ToString() + "'").CopyToDataTable();

                        if (type.Equals("return"))//回传文档
                        {
                            DataTable dtReturn = dtResponse.Select("ZPALLETID='" + l1[j].ToString() + "'").CopyToDataTable();
                            for (int i = 0; i < dtZPALLETID.Rows.Count; i++)
                            {
                                strsql.AppendFormat(" insert into WHLOG_PAL101 (ADD_ID, ZPALLETID, PLANT, COSTCENTER, WO, QTY, MATERIAL, REGION, LOCA, REF1, REF2, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_ERROR,OMBLN,ADD_TYPE2,ADD_RID,ADD_PID) ");
                                strsql.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate(),'{12}','{13}','{14}','{15}','{16}','{17}')",
                                    id.ToString(), dtZPALLETID.Rows[i]["ZPALLETID"], dtZPALLETID.Rows[i]["PLANT"], dtZPALLETID.Rows[i]["COSTCENTER"], dtZPALLETID.Rows[i]["WO"], dtZPALLETID.Rows[i]["QTY"], dtZPALLETID.Rows[i]["MATERIAL"], dtZPALLETID.Rows[i]["REGION"], dtZPALLETID.Rows[i]["LOCA"], dtZPALLETID.Rows[i]["REF1"], dtZPALLETID.Rows[i]["REF2"], type, CRNAM, dtReturn.Rows[i]["ERRMSG"].ToString().Trim(), dtReturn.Rows[i]["OMBLN1"].ToString().Trim() != "" ? dtReturn.Rows[i]["OMBLN1"].ToString().Trim() : dtReturn.Rows[i]["OMBLN2"].ToString().Trim(), type2, RID, PID);

                            }
                        }
                        else if (type.Equals("send"))//送出文档
                        {
                            for (int i = 0; i < dtZPALLETID.Rows.Count; i++)
                            {
                                strsql.AppendFormat(" insert into WHLOG_PAL101 (ADD_ID, ZPALLETID, PLANT, COSTCENTER, WO, QTY, MATERIAL, REGION, LOCA, REF1, REF2, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_TYPE2,ADD_RID,ADD_PID) ");
                                strsql.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate(),'{12}','{13}','{14}','{15}')",
                                    id.ToString(), dtZPALLETID.Rows[i]["ZPALLETID"], dtZPALLETID.Rows[i]["PLANT"], dtZPALLETID.Rows[i]["COSTCENTER"], dt.Rows[i]["WO"], dtZPALLETID.Rows[i]["QTY"], dt.Rows[i]["MATERIAL"], dtZPALLETID.Rows[i]["REGION"], dtZPALLETID.Rows[i]["LOCA"], dtZPALLETID.Rows[i]["REF1"], dtZPALLETID.Rows[i]["REF2"], type, CRNAM, type2, RID, PID);
                            }
                        }
                        ControlSqlAccess.ExecSql(strsql.ToString());
                    }
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
            }
            #endregion

            #region GenerID_311
            public int GenerID_311()
            {
                string strSQL = "";
                int id = 0;
                DataTable dt_temp = new DataTable();

                try
                {
                    ControlHandleDB();

                    strSQL = "select Top 1 ADD_ID from WHLOG_PAL311 order by ADD_ID DESC";
                    dt_temp = ControlSqlAccess.GetDataTable(strSQL);

                    if (dt_temp.Rows.Count == 0)
                        id = 1;
                    else
                        id = Convert.ToInt32(dt_temp.Rows[0][0].ToString()) + 1;

                    ControlSqlAccess.CloseConnection();
                    return id;

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
            #endregion

            #region OlinePALLogtxt311
            public void OlinePALLogtxt311(DataTable dt, DataTable dtResponse, int id, string type, string type2)
            {
                string strSQL = "";
                string OMBLN = "";
                string ERRMSG = "";
                string RID = "";
                string PID = "";
                DataTable dt_temp = new DataTable();

                try
                {
                    ControlHandleDB();

                    #region 帶出此票單據的PID及RID


                    if (type2.Equals("RID"))//刷ReferenceID入庫
                    {

                        RID = dt.Rows[0]["REFNO"].ToString();
                        strSQL = "select TOP 1 MBLNR from whdwn where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + WERKS + "'";
                        strSQL = strSQL + " and LGORT='" + LGORT + "' and  MENGE>OTQTY and MTYPE='QMS_M'and REFID='" + RID + "'";
                        PID = ControlSqlAccess.GetFieldValue(strSQL);
                    }
                    else if (type2.Equals("PID"))//刷PalletID入庫
                    {
                        PID = dt.Rows[0]["REFNO"].ToString();
                        strSQL = "select TOP 1 REFID from whdwn where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + WERKS + "'";
                        strSQL = strSQL + " and LGORT='" + LGORT + "' and  MENGE>OTQTY and MTYPE='QMS_M'and MBLNR='" + PID + "'";
                        RID = ControlSqlAccess.GetFieldValue(strSQL);
                    }

                    #endregion

                    strSQL = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        OMBLN = ERRMSG = "";


                        if (type.Equals("return"))//為回傳文檔
                        {
                            #region 抓出dtResponse內的扣帳標號及錯誤訊息資訊

                            if (dtResponse.Rows[i]["OMBLN"] != null)
                            {
                                if (dtResponse.Rows[i]["OMBLN"].ToString().Trim() != "")
                                    OMBLN = dtResponse.Rows[i]["OMBLN"].ToString().Trim();
                            }

                            if (dtResponse.Rows[i]["ERRMSG"] != null)
                            {
                                if (dtResponse.Rows[i]["ERRMSG"].ToString().Trim() != "")
                                    ERRMSG = dtResponse.Rows[i]["ERRMSG"].ToString().Trim();
                            }

                            #endregion


                            strSQL = strSQL + " insert into WHLOG_PAL311 (ADD_ID, REFNO, PLANT, MATNR, KOSTL, LGORTF, LGORTT, RETQTY, CHARG, STATUS, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_ERROR,OMBLN,ADD_TYPE2,ADD_RID,ADD_PID)";
                            strSQL = strSQL + " values('" + id.ToString() + "','" + dt.Rows[i]["REFNO"] + "','" + dt.Rows[i]["PLANT"] + "','" + dt.Rows[i]["MATNR"] + "','" + dt.Rows[i]["KOSTL"] + "','" + dt.Rows[i]["LGORTF"] + "','" + dt.Rows[i]["LGORTT"] + "','" + dt.Rows[i]["RETQTY"] + "','" + dt.Rows[i]["CHARG"] + "','" + dt.Rows[i]["STATUS"] + "','" + type + "',getdate(),'" + CRNAM + "','" + ERRMSG + "','" + OMBLN + "','" + type2 + "','" + RID + "','" + PID + "')";
                        }
                        else if (type.Equals("send"))//為送出文檔
                        {
                            strSQL = strSQL + " insert into WHLOG_PAL311 (ADD_ID, REFNO, PLANT, MATNR, KOSTL, LGORTF, LGORTT, RETQTY, CHARG, STATUS, OMBLN, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_TYPE2,ADD_RID,ADD_PID)";
                            strSQL = strSQL + " values('" + id.ToString() + "','" + dt.Rows[i]["REFNO"] + "','" + dt.Rows[i]["PLANT"] + "','" + dt.Rows[i]["MATNR"] + "','" + dt.Rows[i]["KOSTL"] + "','" + dt.Rows[i]["LGORTF"] + "','" + dt.Rows[i]["LGORTT"] + "','" + dt.Rows[i]["RETQTY"] + "','" + dt.Rows[i]["CHARG"] + "','" + dt.Rows[i]["STATUS"] + "','" + OMBLN + "','" + type + "',getdate(),'" + CRNAM + "','" + type2 + "','" + RID + "','" + PID + "')";
                        }
                    }

                    ControlSqlAccess.ExecSql(strSQL);
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
            }
            #endregion

            #region multiPALLog311
            public void multiPALLog311(DataTable dt, DataTable dtResponse, int id, string type, string type2)
            {
                StringBuilder strsql = new StringBuilder();
                string RID = "";
                string PID = "";
                DataTable dt_temp = new DataTable();

                try
                {
                    ControlHandleDB();
                    var l1 = (from d in dt.AsEnumerable() select d.Field<string>("REFNO")).Distinct().ToList();
                    for (int j = 0; j < l1.Count; j++)
                    {
                        #region 查询PID/RID
                        strsql.Clear();
                        if (type2.Equals("RID"))//刷ReferenceID入庫
                        {
                            RID = dt.Rows[j]["REFNO"].ToString();
                            strsql.AppendFormat("select TOP 1 MBLNR from whdwn WITH(NOLOCK) where ");
                            strsql.AppendFormat("MANDT='{0}' ", MANDT);
                            strsql.AppendFormat("and COMCD='{0}' ", COMCD);
                            strsql.AppendFormat("and WERKS='{0}' ", WERKS);
                            strsql.AppendFormat("and LGORT='{0}' ", LGORT);
                            strsql.AppendFormat("and  MENGE>OTQTY and MTYPE='QMS_M'and REFID='{0}' ", RID);
                            PID = ControlSqlAccess.GetFieldValue(strsql.ToString());
                        }
                        else if (type2.Equals("PID"))//刷PalletID入庫
                        {
                            PID = dt.Rows[j]["REFNO"].ToString();
                            strsql.AppendFormat("select TOP 1 REFID from whdwn WITH(NOLOCK) where ");
                            strsql.AppendFormat("MANDT='{0}' ", MANDT);
                            strsql.AppendFormat("and COMCD='{0}' ", COMCD);
                            strsql.AppendFormat("and WERKS='{0}' ", WERKS);
                            strsql.AppendFormat("and LGORT='{0}' ", LGORT);
                            strsql.AppendFormat("and  MENGE>OTQTY and MTYPE='QMS_M'and MBLNR='{0}' ", PID);
                            RID = ControlSqlAccess.GetFieldValue(strsql.ToString());
                        }
                        #endregion
                        strsql.Clear();
                        DataTable dtREFNO = dt.Select("REFNO='" + l1[j].ToString() + "'").CopyToDataTable();
                        if (type.Equals("return"))//回传文档
                        {
                            DataTable dtReturn = dtResponse.Select("REFNO='" + l1[j].ToString() + "'").CopyToDataTable();
                            for (int i = 0; i < dtREFNO.Rows.Count; i++)
                            {
                                strsql.AppendFormat(" insert into WHLOG_PAL311 (ADD_ID, REFNO, PLANT, MATNR, KOSTL, LGORTF, LGORTT, RETQTY, CHARG, STATUS, ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_ERROR,OMBLN,ADD_TYPE2,ADD_RID,ADD_PID) ");
                                strsql.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',getdate(),'{11}','{12}','{13}','{14}','{15}','{16}')",
                                    id.ToString(),dtREFNO.Rows[i]["REFNO"],dtREFNO.Rows[i]["PLANT"],dtREFNO.Rows[i]["MATNR"],dtREFNO.Rows[i]["KOSTL"],dtREFNO.Rows[i]["LGORTF"],dtREFNO.Rows[i]["LGORTT"],dtREFNO.Rows[i]["RETQTY"],dtREFNO.Rows[i]["CHARG"],dtREFNO.Rows[i]["STATUS"],type,CRNAM,dtReturn.Rows[i]["ERRMSG"].ToString().Trim(),dtReturn.Rows[i]["OMBLN"].ToString().Trim(),type2,RID,PID);
                            }
                        }
                        else if (type.Equals("send"))//送出文档
                        {
                            for (int i = 0; i < dtREFNO.Rows.Count; i++)
                            {
                                strsql.AppendFormat(" insert into WHLOG_PAL311 (ADD_ID, REFNO, PLANT, MATNR, KOSTL, LGORTF, LGORTT, RETQTY, CHARG, STATUS,ADD_TYPE,ADD_CREDAT,ADD_CRENAME,ADD_TYPE2,ADD_RID,ADD_PID) ");
                                strsql.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',getdate(),'{11}','{12}','{13}','{14}')",
                                    id.ToString(), dtREFNO.Rows[i]["REFNO"], dtREFNO.Rows[i]["PLANT"], dtREFNO.Rows[i]["MATNR"], dtREFNO.Rows[i]["KOSTL"], dtREFNO.Rows[i]["LGORTF"], dtREFNO.Rows[i]["LGORTT"], dtREFNO.Rows[i]["RETQTY"], dtREFNO.Rows[i]["CHARG"], dtREFNO.Rows[i]["STATUS"], type, CRNAM, type2, RID, PID);
                            }
                        }
                        ControlSqlAccess.ExecSql(strsql.ToString());
                    }
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
            }
            #endregion

            #region 查詢log裡有無此RID or PID的紀錄(For SAP ONLY)--101
            public string QuerySAPInDataByPalid101(string REFID, string type)
            {
                string strSQL = "";
                bool flag = false;
                string OMBLN = "";

                try
                {
                    ControlHandleDB();
                    strSQL = "";

                    if (type.Equals("RID"))//刷RID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL101 WITH(NOLOCK) WHERE ADD_RID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";
                    else if (type.Equals("PID"))//刷PID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL101 WITH(NOLOCK) WHERE ADD_PID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";

                    OMBLN = ControlSqlAccess.GetFieldValue(strSQL);

                    ControlSqlAccess.CloseConnection();

                    return OMBLN;
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

            #endregion

            #region 查詢log裡有無此RID or PID的紀錄(For SAP ONLY)--311
            public string QuerySAPInDataByPalid311(string REFID, string type)
            {
                string strSQL = "";
                bool flag = false;
                string OMBLN = "";

                try
                {
                    ControlHandleDB();
                    strSQL = "";

                    if (type.Equals("RID"))//刷RID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL311 WHERE ADD_RID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";
                    else if (type.Equals("PID"))//刷PID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL311 WHERE ADD_PID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";

                    OMBLN = ControlSqlAccess.GetFieldValue(strSQL);

                    ControlSqlAccess.CloseConnection();

                    return OMBLN;
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

            #endregion

            #region 查詢log裡有無此RID or PID的紀錄(For SAP ONLY)--101
            public string QueryMultiSAPInDataByPalid101(string REFID, string type)
            {
                string strSQL = "";
                bool flag = false;
                string OMBLN = "";

                try
                {
                    ControlHandleDB();
                    strSQL = "";

                    if (type.Equals("RID"))//刷RID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL101 WHERE ADD_RID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";
                    else if (type.Equals("PID"))//刷PID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL101 WHERE ADD_PID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";

                    OMBLN = ControlSqlAccess.GetFieldValue(strSQL);

                    ControlSqlAccess.CloseConnection();

                    return OMBLN;
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

            #endregion

            #region 查詢log裡有無此RID or PID的紀錄(For SAP ONLY)--311
            public string QueryMultiSAPInDataByPalid311(string REFID, string type)
            {
                string strSQL = "";
                bool flag = false;
                string OMBLN = "";

                try
                {
                    ControlHandleDB();
                    strSQL = "";

                    if (type.Equals("RID"))//刷RID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL311 WHERE ADD_RID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";
                    else if (type.Equals("PID"))//刷PID
                        strSQL = "select TOP 1 OMBLN from WHLOG_PAL311 WHERE ADD_PID='" + REFID + "' and ADD_TYPE='return' and (ADD_ERROR IS NULL OR ADD_ERROR='') and OMBLN IS NOT NULL and OMBLN !=''";

                    OMBLN = ControlSqlAccess.GetFieldValue(strSQL);

                    ControlSqlAccess.CloseConnection();

                    return OMBLN;
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

            #endregion

            #region 记录QWMS操作log
            /// <summary>
            /// 记录QWMS操作log
            /// </summary>
            /// <param name="dtStorage"></param>
            /// <returns></returns>
            public bool AddQWMSLOG(string MBLNR, string MTYPE, string FLAGE, string OPERATION, string RESULT, string REMARK)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddQWMSLOG";
                this.ControlMethodParm = "('" + MBLNR + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool blResult = false;
                    try
                    {
                        string strSQL = string.Format(@"INSERT INTO QWMS_LOG(MBLNR,MTYPE,OPERATION,RESULT,REMARK,FLAGE,CREATETIME) VALUES ('{0}','{1}',N'{2}',N'{3}',N'{4}','{5}',GETDATE())", MBLNR, MTYPE, OPERATION, string.IsNullOrEmpty(RESULT) == true ? RESULT : RESULT.Replace("'", "''"), string.IsNullOrEmpty(REMARK) == true ? REMARK : REMARK.Replace("'", "''"), FLAGE);
                        ControlHandleDB();
                        blResult = ControlSqlAccess.ExecSql(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddQWMSLOG()";
                    }

                    return blResult;
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
            #endregion

            #region PCBA退库，记录SAP回执信息
            /// <summary> 
            /// 记录QWMS操作log 
            /// </summary> 
            /// <param name="dtStorage"></param> 
            /// <returns></returns> 
            public bool AddSAPData(string SYSTYP, string TRASID, string REFNUM, string STATUS, string RESULT, string UPDNAM)
            {

                //設定要記錄Error Message的相關訊息 
                this.ControlMethodName = "AddQWMSLOG";
                this.ControlMethodParm = "('" + SYSTYP + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時... 
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool blResult = false;
                    try
                    {
                        string strSQL = string.Format(@"INSERT INTO WHTRS (SYSTYP,TRASID,REFNUM,STATUS,RESULT,UPDDAT,UPDNAM) VALUES ('{0}','{1}',N'{2}',N'{3}',N'{4}',GETDATE(),'{5}')", SYSTYP, TRASID, REFNUM, STATUS, RESULT, UPDNAM);

                        ControlHandleDB();
                        blResult = ControlSqlAccess.ExecSql(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddSAPData()";
                    }

                    return blResult;
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
            #endregion

            #region 回传维护SN和boxid的异动记录下来(Mange_SNTOQMS)  add by Ellis 20180913
            public ArrayList AddLogSNTOQMSData(DataTable dtCombine)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLogSNTOQMSData";
                this.ControlMethodParm = "('" + dtCombine + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhlog objDataWhlog = new DataWhlog(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    DataTable dtData = new DataTable();
                    try
                    {

                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddLogSNTOQMSData()";
                    }

                    string strCgcls = dtData.Rows[0]["CTRLC3"].ToString();

                    for (int i = 0; i < dtCombine.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objDataWhlog.ResetField();

                        objDataWhlog.Mandt = dtCombine.Rows[i]["MANDT"].ToString();
                        objDataWhlog.Comcd = dtCombine.Rows[i]["COMCD"].ToString();
                        objDataWhlog.Werks = dtCombine.Rows[i]["WERKS"].ToString();
                        objDataWhlog.Lgort = dtCombine.Rows[i]["LGORT"].ToString();
                        objDataWhlog.Cgcls = strCgcls;
                        objDataWhlog.Oloca = dtCombine.Rows[i]["LOCAT"].ToString();
                        objDataWhlog.Nloca = "";
                        objDataWhlog.Matnr = dtCombine.Rows[i]["MATNR"].ToString();
                        objDataWhlog.Charg = dtCombine.Rows[i]["CHARG"].ToString();
                        objDataWhlog.Lifnr = "";
                        objDataWhlog.Trntp = "";
                        objDataWhlog.Mblnr = dtCombine.Rows[i]["MBLNR"].ToString();
                        objDataWhlog.Ombln = "";
                        objDataWhlog.Ebeln = "";
                        objDataWhlog.Menge = "0";
                        objDataWhlog.Insmk = "";
                        objDataWhlog.Kostl = "";
                        objDataWhlog.Arbpl = "";
                        objDataWhlog.Mrgid = "";
                        objDataWhlog.Crnam = CRNAM;
                        objDataWhlog.Crdat = "getdate()";
                        objDataWhlog.Boxid = dtCombine.Rows[i]["BOXID"].ToString();
                        objDataWhlog.Serno = dtCombine.Rows[i]["SERNO"].ToString();

                        arySQL.Add(objDataWhlog.EntityGetInsertSql());


                        //arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR,TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT,BOXID,SERNO) values( '" +
                        //    dtCombine.Rows[i]["MANDT"].ToString() + "','" + dtCombine.Rows[i]["COMCD"].ToString() + "','" + dtCombine.Rows[i]["WERKS"].ToString() + "','" + dtCombine.Rows[i]["LGORT"].ToString() + "','" + strCgcls + "','" + dtCombine.Rows[i]["LOCAT"].ToString() + "','','" +
                        //    dtCombine.Rows[i]["MATNR"].ToString() + "','" + dtCombine.Rows[i]["CHARG"].ToString() + "','','','" + dtCombine.Rows[i]["MBLNR"].ToString() + "','','','','', '' , '' ,'','" + CRNAM + "', getdate() ,'','','" + dtCombine.Rows[i]["BOXID"].ToString() + "','" + dtCombine.Rows[i]["SERNO"].ToString() + "')");
                    }

                    return arySQL;
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
            #endregion

            #region 储位异动记录  add by Ellis 20181015
            public ArrayList LocationLog(string strWerks, string strLgort, ArrayList arlLocat, string strRmak)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "LocationLog";
                this.ControlMethodParm = "('" + arlLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhlog objDataWhlog = new DataWhlog(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                    DataTable dtData = new DataTable();
                    try
                    {

                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddLogSNTOQMSData()";
                    }

                    string strCgcls = dtData.Rows[0]["CTRLC3"].ToString();

                    for (int i = 0; i < arlLocat.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objDataWhlog.ResetField();

                        objDataWhlog.Mandt = strMandt;
                        objDataWhlog.Comcd = strComcd;
                        objDataWhlog.Werks = strWerks;
                        objDataWhlog.Lgort = strLgort;
                        objDataWhlog.Cgcls = strCgcls;
                        objDataWhlog.Oloca = arlLocat[i].ToString().Trim().ToUpper();
                        objDataWhlog.Trntp = "";
                        objDataWhlog.Menge = "0";
                        objDataWhlog.Insmk = "";
                        objDataWhlog.Rmak1 = strRmak;
                        objDataWhlog.Crnam = CRNAM;
                        objDataWhlog.Crdat = "getdate()";
                        objDataWhlog.Matnr = "";
                        arySQL.Add(objDataWhlog.EntityGetInsertSql());


                        //arySQL.Add("Insert into WHLOG(MANDT, COMCD,WERKS, LGORT, CGCLS, OLOCA, NLOCA, MATNR, CHARG, LIFNR,TRNTP, MBLNR, OMBLN, EBELN, MENGE, INSMK, KOSTL, ARBPL, MRGID, CRNAM, CRDAT, RMAK1, INDAT,BOXID,SERNO) values( '" +
                        //    dtCombine.Rows[i]["MANDT"].ToString() + "','" + dtCombine.Rows[i]["COMCD"].ToString() + "','" + dtCombine.Rows[i]["WERKS"].ToString() + "','" + dtCombine.Rows[i]["LGORT"].ToString() + "','" + strCgcls + "','" + dtCombine.Rows[i]["LOCAT"].ToString() + "','','" +
                        //    dtCombine.Rows[i]["MATNR"].ToString() + "','" + dtCombine.Rows[i]["CHARG"].ToString() + "','','','" + dtCombine.Rows[i]["MBLNR"].ToString() + "','','','','', '' , '' ,'','" + CRNAM + "', getdate() ,'','','" + dtCombine.Rows[i]["BOXID"].ToString() + "','" + dtCombine.Rows[i]["SERNO"].ToString() + "')");
                    }

                    return arySQL;
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
            #endregion

            #region 实时库存比对日志记录
            /// <summary>
            /// 实时库存比对日志记录
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            public void CompareLog(string strWerks, string strLgort, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CompareLog";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("INSERT INTO [dbo].[log_Compara] ([Compara] ,[WERKS] ,[LGORTS] ,[spid] ,[login_name] ,[hostname] ,[ipaddress] ,[UDate]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}',GETDATE())", strType, strWerks, strLgort, "", "qwms", UserData.UserId, UserData.ClientIP, DateTime.Now);
                    try
                    {

                        ControlHandleDB();
                        ControlSqlAccess.ExecSql(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- CompareLog()";
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
            #endregion

            #region XML日志记录
            public void XMLLog(string strXML)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CompareLog";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("INSERT INTO [dbo].[WHXMLLOG] ([XMLTXT] ,[CRDATE] ,[BEGTIME]) VALUES ('{0}',GETDATE(),GETDATE())", strXML);
                    try
                    {

                        ControlHandleDB();
                        ControlSqlAccess.ExecSql(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- CompareLog()";
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
            #endregion

            #region 加锁仓别配置
            public bool CheckStorageInType(string strWerk, string strLgort, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckStorageInType";
                this.ControlMethodParm = "(" + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flage = false;
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("SELECT 1  FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='StorageIn_Type' AND  REMAK='{0}' AND CTRLNM='{1}'", strType, strWerk);
                    if (!strType.Equals("PalletInStorage"))
                    {
                        sbSql.AppendFormat(" AND CTRLC1='{0}'", strLgort);
                    }
                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dtData.Rows.Count > 0)
                    {
                        flage = true;
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
                return flage;
            }
            #endregion
            #region 获取库存有效期
            public DataTable GetExpiryDate(string strVEDAT, string strMATNR)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetExpiryDate";
                this.ControlMethodParm = "(" + strMATNR + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtExpiryDate = new DataTable();
                StringBuilder strsql = new StringBuilder();

                strsql.AppendFormat(@"SELECT CONVERT(VARCHAR(8), DATEADD(MONTH, CONVERT(INT, CTRLC2),'{0}'), 112) AS ExpiryDate FROM WHCTRL WITH (NOLOCK)
	                                    WHERE CTRLC1 = [dbo].[fn_ExpDateRule]('{1}') AND ctrlid = 'D/C_ADMIN' AND ctrlnm = 'ExpdatRule'", strVEDAT, strMATNR);

                try
                {

                    ControlHandleDB();
                    dtExpiryDate = ControlSqlAccess.GetDataTable(strsql.ToString());
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

                return dtExpiryDate;
            }
            #endregion


            public bool AddChangeDCLog(string mblnr,string matnr,string strDacod_before, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddChangeDCLog";
                this.ControlMethodParm = "('" + mblnr + "','" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    string strMenge = "";

                    DataWhctrl objWhctrl = new DataWhctrl(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alQueryConditions = new ArrayList();

                    //DataTable dtData = new DataTable();

                    DataTable dtData = dtStorage.Clone(); // 创建一个与原始表结构相同的空表

                    foreach (DataRow row in dtStorage.Rows)
                    {
                        if (row["omblnr"].ToString() == mblnr && row["matnr"].ToString() == matnr)
                        {
                            dtData.ImportRow(row); // 将符合条件的行逐行导入到新表中
                        }
                    }


                    string strCgcls = "99";

                    
                    DataWhlog objWhlog = new DataWhlog(UserData);

                    objWhlog.Mandt = UserData.Client;
                    objWhlog.Comcd = UserData.CompanyCode;
                    objWhlog.Werks = dtData.Rows[0]["WERKS"].ToString();
                    objWhlog.Lgort = dtData.Rows[0]["LGORT"].ToString();
                    objWhlog.Cgcls = strCgcls;
                    objWhlog.Oloca = dtData.Rows[0]["LOCAT"].ToString();
                    objWhlog.Nloca = "";
                    objWhlog.Matnr = dtData.Rows[0]["MATNR"].ToString();
                    objWhlog.Charg = dtData.Rows[0]["CHARG"].ToString();
                    objWhlog.Lifnr = dtData.Rows[0]["LIFNR"].ToString();
                    objWhlog.Trntp = "";
                    objWhlog.Mblnr = "";
                    objWhlog.Ombln = dtData.Rows[0]["OMBLNR"].ToString();
                    objWhlog.Ebeln = dtData.Rows[0]["EBELN"].ToString();
                    objWhlog.Menge = dtData.Rows[0]["MENGE"].ToString();
                    objWhlog.Insmk = dtData.Rows[0]["INSMK"].ToString();
                    objWhlog.Kostl = "";
                    objWhlog.Arbpl = "";
                    objWhlog.Mrgid = dtData.Rows[0]["MRGID"].ToString();
                    objWhlog.Crnam = UserData.UserId;
                    objWhlog.Crdat = "getdate()";
                    objWhlog.Rmak1 = "DCchanged";
                    objWhlog.Indat = dtData.Rows[0]["INDAT"].ToString();
                    objWhlog.Serno = dtData.Rows[0]["SERNO"].ToString();
                    objWhlog.Locod = dtData.Rows[0]["LOCOD"].ToString();
                    objWhlog.Inspt = dtData.Rows[0]["INSPT"].ToString();
                    objWhlog.Dacod = strDacod_before;

                    arySQL.Clear();
                    arySQL.Add(objWhlog.EntityGetInsertSql());
                    ControlHandleDB();
                    bool success = ControlSqlAccess.ExecSqlArray(arySQL); // 执行 SQL 语句
                    ControlSqlAccess.CloseConnection();
                    return success;


                    return true;
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
        }
    }
}
