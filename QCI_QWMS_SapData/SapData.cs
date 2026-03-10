using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.Collections.Generic;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// SapData 的摘要描述。
        /// </summary>
        public class SapData : ControlBase
        {
            private string strMandt = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strErrmsg = "";
            private string strComcd = "";

            #region Constructer


            public SapData()
            {
            }

            public SapData(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
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
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public SapData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = strWerks;
                LGORT = strLgort;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.SapData";
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
            /// Company Code。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
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
            /// 錯誤訊息。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }

            #endregion

            #region MemberFunction

            #region 列出Spare Parts的單據清單(MTYPE='SAP_SPT' and 'QMS_SPT')
            //=========================================================================
            ////////////Summary by Smose Liao 20100415/////////////////////////////////
            /// <summary>
            ///  列出Spare Parts的單據清單(MTYPE='SAP_SPT' and 'QMS_SPT')
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <param name="Progid">Function id。</param>
            /// <param name="varCrdat">建立日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_SpareParts(strMblnr, strInsmk, Progid, varCrdat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_SpareParts(string strMblnr, string strInsmk, string Progid, string varCrdat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_SpareParts";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + "," + Progid + "," + varCrdat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("* , (Menge - Otqty) as BALANCE");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE> OTQTY ");
                alConditions.Add(" TRNTP in ('G+','R+','M+') ");

                if (strMblnr != "")
                {
                    alConditions.Add("MBLNR= '" + strMblnr + "'");
                }

                if (strInsmk != "")
                {
                    alConditions.Add("INSMK= '" + strInsmk + "'");
                }

                if (Progid != "")
                {
                    if (Progid == "B8")  //自動入庫
                    {
                        alConditions.Add("MTYPE='QMS_SPT'");
                    }
                    else if (Progid == "B9")  //手動入庫
                    {
                        alConditions.Add("MTYPE='SAP_SPT'");
                    }
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, true);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region 列出DOA的單據清單
            //===================================================================================================
            ////////////Summary by Smose Liao 20110208///////////////////////////////////////////////////////////
            /// <summary>
            ///  列出DOA的單據清單
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="Progid">Function id。</param>
            /// <param name="varCrdat">建立日期。</param>
            /// <param name="strRmano">RMA No.。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_DOA(strInsmk, Progid, varCrdat, strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_DOA(string strInsmk, string Progid, string varCrdat, string strRmano)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_DOA";
                this.ControlMethodParm = "(" + strInsmk + "," + Progid + "," + varCrdat + "," + strRmano + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("* , (Menge - Otqty) as BALANCE");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE> OTQTY ");
                alConditions.Add(" TRNTP in ('G+', 'R+', 'M+', 'T+') ");
                alConditions.Add(" MTYPE='SAP_DOA' ");

                if (strInsmk != "")
                {
                    alConditions.Add("INSMK= '" + strInsmk + "'");
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                if (Progid == "BD")//DOA入庫
                {
                    alConditions.Add(" BWART in ('651', '45K', '95C', '455') ");
                }
                else if (Progid == "BE")//銷退入庫
                {
                    alConditions.Add(" BWART in ('913', '920', '315', '305', '311') ");
                }

                if (strRmano != "")
                {
                    alConditions.Add("RMANO= '" + strRmano + "'");
                }

                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, true);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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


            #region 列出SAP 101倉庫收料的資料(BWART='101' and '503')
            //=========================================================================
            ////////////Summary by Smose Liao 20091012/////////////////////////////////
            /// <summary>
            /// 列出SAP 101倉庫收料的資料(BWART='101' and '503')
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strDate">日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_GR(strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_GR(string strMblnr, string strInsmk, string strDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_GR";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + "，" + strDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //string strSQL = "Select * , (Menge - Otqty) as BALANCE from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MENGE> OTQTY and TRNTP in ('G+','R+','M+') and MTYPE='SAP' and BWART='101'";

                alColumns.Add("* , (Menge - Otqty) as BALANCE");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE> OTQTY ");
                alConditions.Add(" TRNTP in ('G+','R+','M+') ");
                alConditions.Add(" MTYPE='SAP'");
                alConditions.Add(" BWART in ('101', '503') ");

                if (strMblnr != "")
                {
                    //strSQL += " and MBLNR= '" + strMblnr + "'";
                    alConditions.Add("MBLNR= '" + strMblnr + "'");
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK= '" + strInsmk + "'";
                    alConditions.Add("INSMK= '" + strInsmk + "'");
                }

                if (strDate != "")
                {
                    //strSQL += " AND CRDAT Between '" + strDate + " 00:00:00.000' and '" + strDate + " 23:59:59.999'";
                    alConditions.Add("CRDAT Between '" + strDate + " 00:00:00.000' and '" + strDate + " 23:59:59.999'");
                }

                try
                {
                    string strSQL = ControlGetQuerySql("whdwn", alColumns, alConditions, true);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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


            #region 列出SAP 連線入庫的單據資料(G+)
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 列出SAP 連線入庫的單據資料(G+)
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData(strMblnr, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData(string strMblnr, string strInsmk, string strDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + "," + strDate + "))";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select * , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+') ", "");

                if (strMblnr != "")
                {
                    sbSql.AppendFormat("AND MBLNR like '{0}%' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                if (strDate != "")
                {
                    sbSql.AppendFormat("AND CRDAT Between '" + strDate + " 00:00:00.000' and '" + strDate + " 23:59:59.999'");
                }

                DataTable dtData = new DataTable();
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


            #region 查詢倉庫101收料資料
            //=======================================================================================================
            ////////////Summary by Smose Liao 20091012///////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢倉庫101收料資料
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData(strLocat,strMblnr,strMatnr,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_GR(string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_GR";
                this.ControlMethodParm = "(" + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP' AND BWART='101'";

                alColumns.Add("MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT, ISNULL(DACOD,'') AS DACOD, INSPT");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE> OTQTY ");
                alConditions.Add(" TRNTP in ('G+','R+','M+') ");
                alConditions.Add(" MTYPE='SAP'");
                alConditions.Add(" BWART in ('101', '503') ");

                if (strMatnr != "")
                {
                    //strSQL += " and MATNR= '" + strMatnr + "'";
                    alConditions.Add("MATNR= '" + strMatnr + "'");
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK= '" + strInsmk + "'";
                    alConditions.Add("INSMK= '" + strInsmk + "'");
                }

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            //=========================================================================
            ////////////Summary by Smose Liao 20091015/////////////////////////////////
            /// <summary>
            /// 列出SAP 321連線入庫的單據資料(G+)(BWART='321')
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_APBU(strMblnr, string strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_APBU(string strMblnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_APBU";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                //string strSQL = "Select * , (Menge - Otqty) as BALANCE from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MENGE> OTQTY and TRNTP in ('G+','R+','M+') and MTYPE='SAP' and BWART='321'";

                sbSql.Append("Select *, (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+') ", "");
                sbSql.AppendFormat("AND BWART='{0}' ", "321");

                if (strMblnr != "")
                {
                    sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                DataTable dtData = new DataTable();
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

            //=========================================================================
            ////////////Summary by Smose Liao 20091015/////////////////////////////////
            /// <summary>
            /// 根據扣帳編號(Document No.)查詢連線入庫資料(G+) by DateCode(New)
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData_DateCode_New(strMblnr, string strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_DateCode_New(string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_DateCode_New";
                this.ControlMethodParm = "(" + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //string strSQL = "";
                string strINSPT = "";
                DataTable dtData = new DataTable();

                StringBuilder sbSql = new StringBuilder();
                //strSQL = "Select distinct INSPT from WHDWN where MANDT='" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR like '" + strMblnr + "%'";
                sbSql.Append("Select distinct INSPT from WHDWN with (nolock) where   ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MBLNR LIKE '{0}%' ", strMblnr);

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtData.Rows.Count > 0)
                    {
                        strINSPT = dtData.Rows[0]["INSPT"].ToString();
                    }
                    else
                    {
                        strINSPT = "";
                    }

                    sbSql.Remove(0, sbSql.Length);

                    //strSQL = "SELECT WHDWN.MBLNR, WHDWN.ZEILE, WHDWN.MATNR, WHDWN.INSMK, WHDWN.CHARG, WHDWN.LIFNR, WHDWN.EBELN, (WHGRD.MENGE-WHGRD.QCQTY) as MENGE, 0 as ALQTY, WHDWN.KOSTL, WHDWN.ARBPL, WHDWN.TRNTP, WHDWN.KDMAT , WHGRD.INSPT, WHGRD.SERNO, WHGRD.LOCOD FROM WHDWN INNER JOIN WHGRD ON WHGRD.INSPT = '" + strINSPT + "' and WHDWN.MBLNR like '" + strMblnr + "%' and WHDWN.MENGE > WHDWN.OTQTY and WHDWN.TRNTP in ('G+','M+','R+') AND WHDWN.MTYPE='SAP'";
                    sbSql.Append("SELECT WHDWN.MBLNR, WHDWN.ZEILE, WHDWN.MATNR, WHDWN.INSMK, WHDWN.CHARG, WHDWN.LIFNR, WHDWN.EBELN, (WHGRD.MENGE-WHGRD.QCQTY) as MENGE, 0 as ALQTY, WHDWN.KOSTL, WHDWN.ARBPL, WHDWN.TRNTP, WHDWN.KDMAT , WHGRD.INDAT, WHGRD.VEDAT, WHGRD.INSPT, WHGRD.DACOD, WHGRD.LOCOD, WHGRD.COMCD FROM WHDWN with (nolock) INNER JOIN WHGRD ON  ");
                    sbSql.AppendFormat("WHGRD.INSPT ='{0}' ", strINSPT);
                    sbSql.AppendFormat("and WHDWN.MBLNR like '{0}%' ", strMblnr);
                    sbSql.AppendFormat("and WHDWN.MENGE > WHDWN.OTQTY ", "");
                    sbSql.AppendFormat("and WHDWN.TRNTP in ('G+','M+','R+') ", "");
                    sbSql.AppendFormat("AND WHDWN.MTYPE='SAP' ", "");

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineInData_DateCode_New()";
                }
                return dtData;

            }


            #region 查詢SpareParts入庫資料(G+)
            //============================================================================================================================
            ////////////Summary by Smose Liao 20100415////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢SpareParts入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySparePartsLineInData(strLocat, strMblnr, strMatnr, strIndat, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySparePartsLineInData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySparePartsLineInData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");


                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','M+','R+') ");
                alConditions.Add(" MTYPE in ('SAP_SPT','QMS_SPT') ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr + "'");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }

                DataTable dtData = new DataTable();
                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            # region 查詢連線入庫資料(G+)
            //=====================================================================================================================
            ////////////Summary by Rock Tzeng//////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData(strLocat, strMblnr, strMatnr, strIndat, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable Query321SapLineInData(string str101Locat, string strLocat, string strmatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "Query321SapLineInData";
                this.ControlMethodParm = "(" + str101Locat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strindat = DateTime.Now.ToString("yyyyMMdd");
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add("'" + strLocat + "'" + " AS Location ");
                alColumns.Add("'" + strindat + "'" + " AS INDAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" (MENGE-OTQTY) as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                //alColumns.Add(" '' as OMBLNR ");
                //alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");


                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" GRLOC LIKE '" + str101Locat + "%'");
                alConditions.Add(" MATNR LIKE '" + strmatnr + "%'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+') ");
                alConditions.Add(" MTYPE='SAP' ");
                alConditions.Add(" BWART='321' ");


                DataTable dtData = new DataTable();
                try
                {
                    string strSQL = ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            # region 查詢連線入庫資料(G+)
            //=====================================================================================================================
            ////////////Summary by Rock Tzeng//////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData(strLocat, strMblnr, strMatnr, strIndat, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable Query321SapLineInData(string str101Locat, string strLocat, string strmatnr, string strMblnr, string strMvt, string strDateFrom, string strDateTo)
            {
                //101Location快捷入库 查询321单据
                this.ControlMethodName = "Query321SapLineInData";
                this.ControlMethodParm = "(" + str101Locat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    #region  2019-08-24  Galen

                    //StringBuilder strSQL = new StringBuilder();
                    //strSQL.AppendFormat("SELECT  D.MANDT, D.COMCD , D.WERKS , D.LGORT ,  D.LIFNR , D.RMANO ,D.Location, ");
                    //strSQL.AppendFormat("CASE WHEN D.INDAT IS NULL THEN  CONVERT(varchar(8),GETDATE(),112) ");
                    //strSQL.AppendFormat("ELSE CONVERT(VARCHAR(8), DATEADD(DAY,-1,D.INDAT),112) END AS INDAT, ");
                    //strSQL.AppendFormat("D.KDMAT ,D.CRDAT ,");
                    //strSQL.AppendFormat("D.MATNR,D.INSMK , D.BWART, D.CHARG ,D.MENGE ,D.ALQTY,D.MBLNR , D.ZEILE,D.OMBLNR,D.MRGID , D.KOSTL,D.ARBPL, D.TRNTP , D.EBELN,D.RMAK1,D.SERNO ,D.DACOD ,D.GRLOC , M.DECITEM  ");
                    //if (type) //MQC PE TWCR 仓比较特别，需要显示料号对应人员
                    //{
                    //    strSQL.AppendFormat(",MQCID,MQCNM , PEID,PENM  ");
                    //}
                    //strSQL.AppendFormat("FROM (  SELECT  MANDT, COMCD , WERKS , LGORT ,  LIFNR , ISNULL(RMANO,'')AS RMANO ,  '" + strLocat + "' AS Location,MATNR, INSMK , BWART, CHARG , (MENGE-OTQTY) as MENGE , (MENGE-OTQTY) as ALQTY, ");
                    //strSQL.AppendFormat("CASE WHEN BWART IN ('262','202','311') ");
                    //strSQL.AppendFormat("THEN (  SELECT MIN(I.INDAT) ");
                    //strSQL.AppendFormat("FROM  WHITM I WITH (NOLOCK) ");
                    //strSQL.AppendFormat("WHERE I.MANDT= A.MANDT AND I.COMCD=A.COMCD AND I.WERKS=A.WERKS AND I.LGORT=A.LGORT AND I.MATNR=A.MATNR AND I.CHARG=A.CHARG ) ");
                    //strSQL.AppendFormat("ELSE  NULL ");
                    //strSQL.AppendFormat("END AS INDAT ,MBLNR , ZEILE,'' as OMBLNR,'' as MRGID , KOSTL,ARBPL, TRNTP , EBELN, '' as RMAK1, ISNULL(SERNO,'') AS SERNO , ISNULL(DACOD,'') AS DACOD , ");
                    //strSQL.AppendFormat("ISNULL(GRLOC,'') AS GRLOC ,ISNULL(KDMAT,'') AS KDMAT ,  CONVERT(varchar(16),CRDAT,120) AS CRDAT ");
                    //strSQL.AppendFormat("FROM WHDWN AS A WITH (NOLOCK) ");
                    //strSQL.AppendFormat("WHERE  MTYPE='SAP' ");
                    //strSQL.AppendFormat("AND MENGE>OTQTY AND BWART IN ('321','343', '311','202','262','350','305','315','309','105','325','4FX','262','4S1','455','45S','55Y','10S') AND FLAGE='N' AND TRNTP LIKE '%+' AND  NOT (BWART='321' AND TRNTP='T+') ");
                    //strSQL.AppendFormat("AND  MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + WERKS + "' AND LGORT='" + LGORT + "' AND CRDAT BETWEEN '" + strDateFrom + "' AND '" + strDateTo + "' ");
                    //strSQL.AppendFormat(") AS D ");
                    //strSQL.AppendFormat("LEFT JOIN MATDIC AS M WITH (NOLOCK)  ON D.COMCD=M.COMCD AND  D.MATNR=M.MATNR ");
                    //if(type) 
                    //{
                    //    strSQL.AppendFormat("LEFT JOIN PNREMAK AS P WITH(NOLOCK) ON D.COMCD=P.COMCD AND D.MATNR=P.MATNR ");
                    //}
                    //strSQL.AppendFormat("WHERE 1=1 ");

                    //if (strMblnr != "")
                    //{
                    //    strSQL.AppendFormat("AND  MBLNR LIKE '" + strMblnr + "%' ");
                    //}
                    //if (strmatnr != "")
                    //{
                    //    strSQL.AppendFormat("AND  D.MATNR LIKE '" + strmatnr + "%' ");
                    //}
                    //if (str101Locat != "")
                    //{
                    //    strSQL.AppendFormat("AND  GRLOC LIKE '" + str101Locat + "%' ");
                    //}
                    //if (strMvt != "")
                    //{
                    //    strSQL.AppendFormat("AND  BWART LIKE '" + strMvt + "%' ");
                    //}
                    //strSQL.AppendFormat("ORDER BY MANDT,COMCD,WERKS,LGORT,Location,GRLOC,MBLNR,MATNR,CRDAT ");

                    #endregion

                    #region  SP_GetDataBy101Location
                    string strSQL = "EXEC SP_GetDataBy101Location '" + MANDT + "','" + COMCD + "','" + WERKS + "','" + LGORT + "','" + strLocat + "','" + str101Locat + "','" + strmatnr + "','" + strMblnr + "','" + strMvt + "','" + strDateFrom + "','" + strDateTo + "'";
                    #endregion
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            # region 查詢連線入庫資料(G+)
            //=====================================================================================================================
            ////////////Summary by Rock Tzeng//////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData(strLocat, strMblnr, strMatnr, strIndat, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" REMAK1 as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                // alColumns.Add("   CASE WHEN BWART='101' THEN  CONVERT(DATETIME , '"+strIndat+"',120)-7 ELSE '"+strIndat+"' END   as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' as VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','M+','R+') ");
                alConditions.Add(" MTYPE='SAP' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr + "'");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }

                DataTable dtData = new DataTable();
                try
                {
                    string strSQL = ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            public DataTable QuerySapLineInData(string strMblnr, string strMatnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData";
                this.ControlMethodParm = "(," + strMblnr + "," + strMatnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" '' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" REMAK1 as RMAK1 ");
                alColumns.Add(" '' as INDAT");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' as VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR in ('" + strMblnr + "')");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','M+','R+') ");
                alConditions.Add(" MTYPE='SAP' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR in ('" + strMatnr + "')");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }

                DataTable dtData = new DataTable();
                try
                {
                    string strSQL = ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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


            # region 查詢連線入庫資料(G+)(DOA/銷退)
            //=====================================================================================================================
            ////////////Summary by Smose Liao//////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)(DOA/銷退)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData_DOA(strLocat, strMblnr, strMatnr, strIndat, strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_DOA(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_DOA";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' AS VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','M+','R+','T+') ");
                alConditions.Add(" MTYPE='SAP_DOA' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr + "'");
                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");
                }

                DataTable dtData = new DataTable();
                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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


            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInData(strLocat,strMblnr,strMatnr,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" '" + LGORT + "' as LGORT ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(RMANO,'') AS RMANO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" '' as VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','M+','R+') ");
                alConditions.Add(" MTYPE='QMS' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr + "'");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }
                #region delete
                //StringBuilder sbSql = new StringBuilder();
                ////string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP' AND BWART='101'";

                //sbSql.Append("Select MANDT, COMCD,WERKS,'" + LGORT + "' as LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT from WHDWN where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'");
                //if (strMatnr != "")
                //{
                //    sbSql.AppendFormat(" AND MATNR='{0}' ", strMatnr);

                //}

                //if (strInsmk != "")
                //{
                //    sbSql.AppendFormat(" AND INSMK='{0}' ", strInsmk);


                //}
                //ControlHandleDB();
                //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                //ControlSqlAccess.CloseConnection();
                #endregion

                DataTable dtData = new DataTable();
                try
                {

                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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





            //=========================================================================
            ////////////Summary by Rock Tzeng ////////////////////////////////////////////
            /// <summary>
            /// 查詢轉倉入庫資料(T+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapTransferInData(strLocat,strMblnr,strMatnr,intMenge,strRmak1,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapTransferInData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" REMAK1 as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(RMANO,'') AS RMANO ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' as VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP ='T+' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr + "'");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }
                #region delete
                //StringBuilder sbSql = new StringBuilder();
                ////string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP' AND BWART='101'";

                //sbSql.Append("Select MANDT, COMCD,WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR, '' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT, ISNULL(SERNO,'') AS SERNO, ISNULL(DACOD,'') AS DACOD from WHDWN where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP='T+'");
                //if (strMatnr != "")
                //{
                //    sbSql.AppendFormat(" AND MATNR='{0}' ", strMatnr);

                //}

                //if (strInsmk != "")
                //{
                //    sbSql.AppendFormat(" AND INSMK='{0}' ", strInsmk);


                //}
                //ControlHandleDB();
                //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                //ControlSqlAccess.CloseConnection();
                #endregion


                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            public DataTable QuerySapCombineInData(string strMblnr, string strMatnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInData";
                this.ControlMethodParm = "(" + strMblnr + "," + strMatnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" '' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" REMAK1 as RMAK1 ");
                alColumns.Add(" '' as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" INSPT ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(RMANO,'') AS RMANO ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' as VEDAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR in ('" + strMblnr + "')");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','T+','R+','M+')  ");
                alConditions.Add("MTYPE = 'SAP' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR in ('" + strMatnr + "')");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" INSMK='" + strInsmk + "'");

                }
                #region delete
                //StringBuilder sbSql = new StringBuilder();
                ////string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP' AND BWART='101'";

                //sbSql.Append("Select MANDT, COMCD,WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR, '' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT, ISNULL(SERNO,'') AS SERNO, ISNULL(DACOD,'') AS DACOD from WHDWN where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP='T+'");
                //if (strMatnr != "")
                //{
                //    sbSql.AppendFormat(" AND MATNR='{0}' ", strMatnr);

                //}

                //if (strInsmk != "")
                //{
                //    sbSql.AppendFormat(" AND INSMK='{0}' ", strInsmk);


                //}
                //ControlHandleDB();
                //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                //ControlSqlAccess.CloseConnection();
                #endregion


                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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



            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢連板入庫資料(G+)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// /// <param name="strIndat">庫別。</param>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapCombineInData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapCombineInData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + ",'" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strFrom = "";
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" D.MANDT ");
                alColumns.Add(" D.COMCD ");
                alColumns.Add(" D.WERKS ");
                alColumns.Add(" D.LGORT ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" D.MATNR ");
                alColumns.Add(" D.INSMK ");
                alColumns.Add(" D.CHARG ");
                alColumns.Add(" (D.MENGE-D.OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" D.MBLNR ");
                alColumns.Add(" D.ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" D.KOSTL ");
                alColumns.Add(" D.ARBPL as ARBPL ");
                alColumns.Add(" D.TRNTP ");
                alColumns.Add(" D.EBELN ");
                alColumns.Add(" D.LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(D.SERNO,'') AS SERNO ");
                alColumns.Add(" D.INSPT ");
                alColumns.Add(" ISNULL(D.DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(D.GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(D.RMANO,'') AS RMANO ");
                alColumns.Add(" ISNULL(D.KDMAT,'') AS KDMAT ");
                alColumns.Add(" '' AS VEDAT ");
                alColumns.Add(" (CASE WHEN ISNULL(I.EXPDAT_AFTER,'') = '' THEN D.ExpiryDate ELSE I.EXPDAT_AFTER END) AS ExpiryDate ");
                alColumns.Add(" I.TASKID ");
                alColumns.Add(" I.RMAK1 AS IQCRMAK1 ");
                alColumns.Add(" (CASE WHEN ISNULL(I.MAXEXP,'')='' THEN '' ELSE (CASE WHEN ISNULL(I.MAXEXP_AFTER,'')='' THEN I.MAXEXP ELSE I.MAXEXP_AFTER END) END) AS MAXEXP ");

                strFrom = " WHDWN D with (nolock) LEFT JOIN WHIQC I WITH(NOLOCK) ON I.TASKID=D.REFID ";


                alConditions.Add(" D.MANDT='" + MANDT + "'");
                alConditions.Add(" D.COMCD='" + COMCD + "'");
                alConditions.Add(" D.WERKS='" + WERKS + "'");
                alConditions.Add(" D.LGORT='" + LGORT + "'");
                alConditions.Add(" D.MBLNR ='" + strMblnr + "'");
                alConditions.Add(" D.MENGE>D.OTQTY");
                alConditions.Add(" D.TRNTP in ('G+','T+','R+','M+')  ");
                alConditions.Add(" D.MTYPE = 'SAP' ");

                if (strMatnr != "")
                {
                    alConditions.Add(" D.MATNR ='" + strMatnr + "'");

                }

                if (strInsmk != "")
                {
                    alConditions.Add(" D.INSMK='" + strInsmk + "'");

                }

                #region delete
                //StringBuilder sbSql = new StringBuilder();
                ////string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP' AND BWART='101'";

                //sbSql.Append("Select MANDT, COMCD,WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT, ISNULL(SERNO,'') AS SERNO,INSPT,RMANO , ISNULL(DACOD,'') AS DACOD from WHDWN where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','T+','R+','M+') AND MTYPE = 'SAP'");
                //if (strMatnr != "")
                //{
                //    sbSql.AppendFormat(" AND MATNR='{0}' ", strMatnr);

                //}

                //if (strInsmk != "")
                //{
                //    sbSql.AppendFormat(" AND INSMK='{0}' ", strInsmk);


                //}
                //ControlHandleDB();
                //   dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                //   ControlSqlAccess.CloseConnection();
                #endregion

                DataTable dtData = new DataTable();
                try
                {
                    dtData = ControlQuery(strFrom, alColumns, alConditions, true);

                    //string strsql = objWhdwn.ControlGetQuerySql(strFrom, alColumns, alConditions, false);
                    //dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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



            #region  查詢該筆EC單是否已比對過
            //===========================================================================
            ////////////Summary by Smose Liao 20090615///////////////////////////////////
            /// <summary>
            /// 查詢該筆EC單是否已比對過
            /// </summary> 
            /// <param name="strVbeln"> EC單號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryEC_Document(string strVbeln);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryEC_DocumentExists(string strVbeln)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryEC_DocumentExists";
                this.ControlMethodParm = "(" + MANDT + "," + WERKS + "," + LGORT + "," + strVbeln + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                //string strSQL = "Select VBELN, PAKID, MATNR, BATCH, MENGE from WHECD where MANDT= '" + MANDT + "' and VBELN= '" + strVbeln + "'";
                sbSql.Append("Select VBELN, PAKID, MATNR, BATCH, MENGE from WHECD WHERE ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND VBELN='{0}' ", strVbeln);

                DataTable dtData = new DataTable();
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
            #region  查詢該筆EC單是否已比對過(無廠區倉別查詢條件)
            //===========================================================================
            ////////////Summary by Ryan Tsai 20110921///////////////////////////////////
            /// <summary>
            /// 查詢該筆EC單是否已比對過
            /// </summary> 
            /// <param name="strVbeln"> EC單號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryEC_Document(string strVbeln);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryEC_DocumentExists_LessCondition(string strVbeln)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryEC_DocumentExists";
                this.ControlMethodParm = "(" + MANDT + "," + WERKS + "," + LGORT + "," + strVbeln + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                //string strSQL = "Select VBELN, PAKID, MATNR, BATCH, MENGE from WHECD where MANDT= '" + MANDT + "' and VBELN= '" + strVbeln + "'";
                sbSql.Append("Select * from WHECD WHERE ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND VBELN='{0}' ", strVbeln);

                DataTable dtData = new DataTable();
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

            #region  查詢EC單資料
            //===========================================================================
            ////////////Summary by Smose Liao 20090603///////////////////////////////////
            /// <summary>
            /// 查詢EC單資料
            /// </summary> 
            /// <param name="strVbeln"> EC單號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryEC_Document(string strVbeln);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryEC_Document(string strVbeln, string strFunc)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryEC_Document";
                this.ControlMethodParm = "(" + strVbeln + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                switch (strFunc)
                {
                    #region Picasso專案使用的查詢EC單條件
                    case "":
                        //string strSQL = "Select VBELN, PAKID, MATNR, BATCH, MENGE, EBELN from PITM where MANDT= '" + MANDT + "' and VBELN= '" + strVbeln + "' and Delmk = '' order by EBELN";
                        sbSql.Append("Select VBELN, PAKID, MATNR, BATCH, MENGE, EBELN from PITM where ");
                        sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("AND VBELN='{0}' ", strVbeln);
                        sbSql.AppendFormat("AND Delmk = '' order by EBELN", "");
                        break;
                    #endregion
                    #region CSMC Barcode專案的查詢EC單條件
                    case "Barcode":
                        if (COMCD == "9200")
                        {
                            //string strSQL = "Select VBELN, PAKID, MATNR, BATCH, MENGE, EBELN from PITM where MANDT= '" + MANDT + "' and VBELN= '" + strVbeln + "' and Delmk = '' order by EBELN";
                            sbSql.Append("Select VBELN, MATNR, SUM(MENGE) AS MENGE  from PITM WITH (NOLOCK) where ");
                            sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                            sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                            sbSql.AppendFormat("AND VBELN='{0}' ", strVbeln);
                            sbSql.AppendFormat("AND Delmk = '' ", "");
                            sbSql.AppendFormat("group by VBELN, MATNR  order by MATNR ", "");
                        }
                        if (COMCD == "9700")
                        {
                            sbSql.Append("Select PAKNUM AS VBELN, PARTNO AS MATNR, SUM(SHPQTY) AS MENGE  from PAKITM WITH (NOLOCK) where ");
                            sbSql.AppendFormat("CLIENT='{0}' ", MANDT);
                            sbSql.AppendFormat("AND COMCOD='{0}' ", COMCD);
                            sbSql.AppendFormat("AND PAKNUM='{0}' ", strVbeln);
                            sbSql.AppendFormat("group by PAKNUM, PARTNO  order by PARTNO ", "");
                        }
                        break;
                    #endregion
                }

                DataTable dtData = new DataTable();
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



            #region 列出SAP連板入庫的單據資料(G+, T+)
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 列出SAP連板入庫的單據資料(G+, T+)
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapMixedInData(string strMblnr, string strInsmk, string strDate,string strIntype)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapMixedInData";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select * , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY ");
                if (strIntype == "TRANSFER")
                {
                    sbSql.AppendFormat("and TRNTP='T+'  ");
                }
                if (strIntype == "COMBINE")
                {
                    sbSql.AppendFormat("and TRNTP in ('G+','T+','R+','M+') ");
                }


                if (strMblnr != "")
                {

                    sbSql.AppendFormat("AND MBLNR like '{0}%' ", strMblnr);

                }


                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);

                }

                if (strDate != "")
                {
                    sbSql.AppendFormat("AND CRDAT Between '" + strDate + " 00:00:00.000' and '" + strDate + " 23:59:59.999'");
                }

                DataTable dtData = new DataTable();
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




            #region 查詢連線入庫資料(G+) By Date Code
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+) By Date Code
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_DateCode(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_DateCode";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                //string strSQL = "Select VBELN, PAKID, MATNR, BATCH, MENGE, EBELN from PITM where MANDT= '" + MANDT + "' and VBELN= '" + strVbeln + "' and Delmk = '' order by EBELN";
                sbSql.Append("Select MANDT, COMCD,WERKS, LGORT,LIFNR, '" + strLocat + "' as LOCAT, ");
                sbSql.AppendFormat("MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, ");
                sbSql.AppendFormat("MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ");
                sbSql.AppendFormat("ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, ");
                sbSql.AppendFormat("'" + strIndat + "' as INDAT , ISNULL(SERNO,'') AS SERNO,INSPT  from WHDWN with (nolock) ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                sbSql.AppendFormat("AND MENGE>OTQTY ");
                sbSql.AppendFormat("AND TRNTP in ('G+','M+','R+') ");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");

                if (strMatnr != "")
                {
                    sbSql.AppendFormat("AND MATNR='{0}' ", strMatnr);

                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }
                DataTable dtData = new DataTable();
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



            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// SAP單據查詢 strInsmk:庫別, strStartDate:下載開始時間, strEndDate:下載結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartLocat:單據開始, strEndLocat:單據結束
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objSapData.QuerySapData(strInsmk, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartMblnr, strEndMblnr, intType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QuerySapData(string strInsmk, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, int intType, int intGRGI, string strBwart)
            {
                string strSQL = "Select * from WHDWN with (nolock) where MANDT='" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' and LGORT = '" + LGORT + "' ";

                if (strInsmk != "")
                {
                    strSQL += " and INSMK = '" + strInsmk + "'";
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    strSQL += " and CRDAT = '" + strEndDate + "'";
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    strSQL += " and CRDAT = '" + strStartDate + "'";
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    strSQL += " and MATNR = '" + strEndMatnr + "'";
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    strSQL += " and MATNR = '" + strStartMatnr + "'";
                }

                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    strSQL += " and (MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    strSQL += " and MBLNR like '" + strEndMblnr + "%'";
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    strSQL += " and MBLNR like '" + strStartMblnr + "%'";
                }

                if (intType == 0)
                {
                    strSQL += " and MENGE=OTQTY";
                }
                else if (intType == 1)
                {
                    strSQL += " and MENGE>OTQTY";
                }

                if (intGRGI == 0)
                {
                    strSQL += " and TRNTP in ('T+', 'G+' ,'M+', 'R+')";
                }
                else if (intGRGI == 1)
                {
                    strSQL += " and TRNTP in ('T-', 'G-' ,'M-', 'R-')";
                }

                if (strBwart.Trim() != "")
                {
                    strSQL += " and BWART='" + strBwart.Trim() + "'";
                }
                strSQL += " order by MANDT, WERKS, LGORT, MBLNR";
                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapData()";
                }
                return dtData;

            }

            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// SAP單據查詢 strInsmk:庫別, strStartDate:下載開始時間, strEndDate:下載結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartLocat:單據開始, strEndLocat:單據結束, strMType:單據類型(SAP、QMS、SDS)
            /// 20051012為了修改SAP單據查詢而Overload該方法(多了一個strMType參數，判斷SAP、QMS、SDS)
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objSapData.QuerySapData(strInsmk, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartMblnr, strEndMblnr, intType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QuerySapData(string strInsmk, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, int intType, int intGRGI, string strBwart, string strMType, string strQueryTable)
            {
                ArrayList alColumns = new ArrayList();
                ArrayList alQueryCondition = new ArrayList();

                alColumns.Add(" TOP 10000 MANDT,COMCD,MBLNR,ZEILE,WERKS,LGORT,TRNTP,MATNR,INSMK,CHARG,MENGE,OTQTY,BWART,UMLGO,USNAM,LIFNR,IIF(BWART='311' and TRNTP='T-',UMLGO,KOSTL)KOSTL,KDMAT,CRDAT,GRLOC");

                alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
                alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                //alQueryCondition.Add("WERKS = '" + WERKS + "' And UMLGO<>'IQC2'");
                //REFID NOT LIKE 'B%AUTO'
                // alQueryCondition.Add("REFID NOT LIKE 'B%AUTO'");
                //    alQueryCondition.Add(" LEFT(USNAM,4)<>'7604'  ");//卡掉SAP自动扣账的单子
                alQueryCondition.Add("WERKS = '" + WERKS + "'");

                if (strInsmk != "")
                {
                    alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                }

                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                }

                if (intType == 0)
                {
                    alQueryCondition.Add("MENGE=OTQTY");
                }
                else if (intType == 1)
                {
                    alQueryCondition.Add("MENGE>OTQTY");
                }

                if (intGRGI == 0)
                {
                    alQueryCondition.Add("TRNTP in ('T+', 'G+' ,'M+', 'R+') AND BWART NOT IN ('313')");
                }
                else if (intGRGI == 1)
                {
                    alQueryCondition.Add("TRNTP in ('T-', 'G-' ,'M-', 'R-')");
                }

                if (strBwart.Trim() != "")
                {
                    alQueryCondition.Add("BWART='" + strBwart.Trim() + "'");
                }

                if (strMType.Trim() != "")
                {
                    if (strMType.Trim() == "SAP")
                    {
                        #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                        if (COMCD == "9110" && LGORT == "" || COMCD == "9200" && LGORT == "" || COMCD == "2281" && LGORT == "")
                        {
                            alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                        }
                        else if (COMCD == "9110" && LGORT != "" || COMCD == "9200" && LGORT != "" || COMCD == "2281" && LGORT != "")
                        {
                            alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                        }
                        else
                        {
                            alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                        }
                        #endregion
                    }
                    else
                    {
                        if (strMType.Trim() == "S/F(PCBA)")
                            strMType = "QMS_M";

                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                    }

                }
                alQueryCondition.Add("NOT ( BWART='321' AND TRNTP='T+' ) ");
                DataTable dtData = new DataTable();
                try
                {
                    string strSQL = ControlGetQuerySql(strQueryTable, alColumns, alQueryCondition, false);
                    dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapData()";
                }
                return dtData;

            }

            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// SAP單據查詢 strInsmk:庫別, strStartDate:下載開始時間, strEndDate:下載結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartLocat:單據開始, strEndLocat:單據結束, strMType:單據類型(SAP、QMS、SDS)
            /// 20051012為了修改SAP單據查詢而Overload該方法(多了一個strMType參數，判斷SAP、QMS、SDS)
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objSapData.QueryQsmsData(strInsmk, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartMblnr, strEndMblnr, intType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryQsmsData(string strInsmk, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, int intType, int intGRGI, string strBwart, string strMType)
            {
                DataWhrid objWhrid = new DataWhrid(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alQueryCondition = new ArrayList();


                alColumns.Add("TOP 500 *");
                alColumns.Add("REFID as MBLNR");
                alColumns.Add("DIDNO as ZEILE");
                alColumns.Add("'G+' as TRNTP");
                alColumns.Add("'' as BWART");
                alColumns.Add("'' as UMLGO");
                alColumns.Add("'' as USNAM");
                alColumns.Add("'' as KDMAT");

                alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
                alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                alQueryCondition.Add("WERKS = '" + WERKS + "'");

                //string strSQL = "Select *, REFID as MBLNR, DIDNO as ZEILE, 'G+' as TRNTP, '' as BWART, '' as UMLGO, '' as USNAM, '' as KDMAT from WHRID where MANDT='" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' ";

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK = '" + strInsmk + "'";
                    alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                    alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    //strSQL += " and CRDAT = '" + strEndDate + "'";
                    alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    //strSQL += " and CRDAT = '" + strStartDate + "'";
                    alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    //strSQL += " and MATNR = '" + strEndMatnr + "'";
                    alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    //strSQL += " and MATNR = '" + strStartMatnr + "'";
                    alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                }

                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    //strSQL += " and (REFID between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                    alQueryCondition.Add("(REFID between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    //strSQL += " and REFID like '" + strEndMblnr + "%'";
                    alQueryCondition.Add("REFID like '" + strEndMblnr + "%'");
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    //strSQL += " and REFID like '" + strStartMblnr + "%'";
                    alQueryCondition.Add("REFID like '" + strStartMblnr + "%'");
                }

                if (intType == 0)
                {
                    //strSQL += " and MENGE=OTQTY";
                    alQueryCondition.Add("MENGE = OTQTY");
                }
                else if (intType == 1)
                {
                    //strSQL += " and MENGE>OTQTY";
                    alQueryCondition.Add("MENGE > OTQTY");
                }

                if (strBwart.Trim() != "")
                {
                    //strSQL += " and BWART='" + strBwart.Trim() + "'";
                    alQueryCondition.Add("BWART='" + strBwart.Trim() + "'");
                }
                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhrid.EntityQuery(alColumns, alQueryCondition, false, true);
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL);
                    //ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryQsmsData()";
                }
                return dtData;

            }

            #region
            //======================================================================================
            ////////////Summary by Smose Liao 20100421//////////////////////////////////////////////
            /// <summary>
            /// Spare Parts單據查詢 strStartDate:下載開始時間, strEndDate:下載結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strStartMblnr:單據開始, strEndMblnr:單據結束, strMType:單據類型(SAP_SI、QMS_SPT、SAP_SPT)
            /// 20051012為了修改SAP單據查詢而Overload該方法(多了一個strMType參數，判斷SAP、QMS、SDS)
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objSapData.QueryQsmsData(strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartMblnr, strEndMblnr, strStartRmano, strEndRmano, intType, strBwart, strMType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable QuerySparePartsData(string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, string strStartRmano, string strEndRmano, int intType, string strBwart, string strMType, string strQueryTable)
            {
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alQueryCondition = new ArrayList();

                alColumns.Add("*");

                alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
                alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                alQueryCondition.Add("WERKS = '" + WERKS + "'");
                alQueryCondition.Add("LGORT = '" + LGORT + "'");

                if (strStartDate != "" && strEndDate != "")
                {
                    alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                }

                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                }

                if (strStartRmano != "" && strEndRmano != "")
                {
                    alQueryCondition.Add("(RMANO between '" + strStartRmano + "' and '" + strEndRmano + "')");
                }
                else if (strStartRmano == "" && strEndRmano != "")
                {
                    alQueryCondition.Add("RMANO = '" + strEndRmano + "'");
                }
                else if (strStartRmano != "" && strEndRmano == "")
                {
                    alQueryCondition.Add("RMANO = '" + strStartRmano + "'");
                }

                if (intType == 0)
                {
                    alQueryCondition.Add("MENGE = OTQTY");
                }
                else if (intType == 1)
                {
                    alQueryCondition.Add("MENGE > OTQTY");
                }

                if (strBwart.Trim() != "")
                {
                    alQueryCondition.Add("BWART='" + strBwart.Trim() + "'");
                }

                if (strMType.Trim() != "")
                {
                    alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                }

                DataTable dtData = new DataTable();
                try
                {
                    dtData = this.ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySparePartsData()";
                }
                return dtData;

            }

            #endregion


            #region 列出SAP Picasso連線入庫的單據資料(G+)(BWART='321')且有BOX ID
            //====================================================================================
            ////////////Summary by Smose Liao 20091022////////////////////////////////////////////
            /// <summary>
            /// 列出SAP Picasso連線入庫的單據資料(G+)(BWART='321')且有BOX ID
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_Picasso(strMblnr, string strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_Picasso(string strMblnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_Picasso";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //string strSQL = "Select distinct MBLNR,MATNR , (Menge - Otqty) as BALANCE from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MENGE> OTQTY and TRNTP in ('G+','R+','M+') and MTYPE='SAP' and BWART='321' and isnull(SERNO,'') <> '' ";
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("Select distinct MBLNR,MATNR , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY ", "");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+') ", "");
                sbSql.AppendFormat("AND BWART='321' ", "");
                sbSql.AppendFormat("AND isnull(BOXID,'') <> '' ", "");

                if (strMblnr != "")
                {
                    //strSQL += " and MBLNR= '" + strMblnr + "'";
                    sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK= '" + strInsmk + "'";
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                DataTable dtData = new DataTable();
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


            #region 查詢連線入庫資料(G+)(Picasso專案)
            //=====================================================================================
            ////////////Summary by Smose Liao 20090611/////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)(Picasso專案)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData_Picasso(strLocat,strMblnr,strMatnr,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_Picasso(string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_Picasso";
                this.ControlMethodParm = "(" + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //string strSQL = "Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, KDMAT, ISNULL(SERNO,'') AS SERNO,BXQTY,INSPT from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') AND MTYPE='SAP'";
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("Select MANDT, WERKS, LGORT,LIFNR, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, KDMAT, ISNULL(SERNO,'') AS SERNO,BXQTY,INSPT from WHDWN with (nolock) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                sbSql.AppendFormat("AND MENGE>OTQTY ", "");
                sbSql.AppendFormat("AND TRNTP in ('G+','M+','R+') ", "");
                sbSql.AppendFormat("AND MTYPE='SAP' ", "");

                DataTable dtData = new DataTable();
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


            #region 查詢連線入庫資料(G+)(Picasso專案)(New)
            //=====================================================================================
            ////////////Summary by Smose Liao 20090709/////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)(Picasso專案)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineInData_Picasso(strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineInData_Picasso_New(string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineInData_Picasso_New";
                this.ControlMethodParm = "(" + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "select *, 0 as alqty, '' as OMBLNR,'' as MRGID, '' as RMAK1 from whdwn D with (nolock) where mandt = '" + MANDT + "' and werks='" + WERKS + "' and lgort='" + LGORT + "' and trntp='G+' and mblnr like '49%' and inspt in ( ";
                strSQL += "select inspt from whdwn C with (nolock) where mandt = '" + MANDT + "' and comcd='" + COMCD + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and trntp='G+' and left(mblnr,10) in ";
                strSQL += "(select left(mblnr,10) from whdwn A with (nolock) where mandt = '" + MANDT + "' and comcd='" + COMCD + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and trntp='G+' and mblnr like '50%' ";
                strSQL += "and inspt in (select inspt from whdwn B with (nolock) where mandt = '" + MANDT + "' and comcd='" + COMCD + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and mblnr = '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+') AND MTYPE='SAP' AND BWART='321')))";

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 查詢321單據的總數量(Picasso專案)
            //=====================================================================================
            ////////////Summary by Smose Liao 20090710/////////////////////////////////////////////
            /// <summary>
            /// 查詢321單據的總數量(Picasso專案)
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapData_Picasso(strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////
            public int QuerySapData_Picasso(string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapData_Picasso";
                this.ControlMethodParm = "(" + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "select left(mblnr,10) from whdwn A with (nolock) ";
                strSQL += "where mandt = '" + MANDT + "' and comcd='" + COMCD + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and trntp='G+' and mblnr like '50%' ";
                strSQL += "and inspt in (select distinct inspt from whdwn B with (nolock) where mandt ='" + MANDT + "' and comcd='" + COMCD + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and mblnr = '" + strMblnr + "')";

                string strTempMblnr = "";
                int intMenge = 0;
                DataTable dtData = new DataTable();

                try
                {
                    //先取得101單據的前10碼
                    ControlHandleDB();
                    strTempMblnr = ControlSqlAccess.GetFieldValue(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    //取得101單據的總數量
                    //strSQL = "select * from whdwn where mandt = '" + MANDT + "'  and werks='" + WERKS + "' and lgort='" + LGORT + "' and mblnr like + '" + strTempMblnr + '%' + "'";
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select * from WHDWN with (nolock) where  ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                    sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                    sbSql.AppendFormat("AND mblnr like '{0}%' ", strTempMblnr);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        intMenge += int.Parse(dtData.Rows[i]["MENGE"].ToString());
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
                return intMenge;
            }
            #endregion


            #region  查詢連線入庫資料(G+)by Pallet ID
            //==========================================================================================
            ////////////Summary by Smose Liao 20091023//////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by Pallet ID
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strPalid">PalletID。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataByPalid(strLocat, strPalid, strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataByPalid(string strLocat, string strPalid, string strIndat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByPalid";
                this.ControlMethodParm = "(" + strLocat + "," + strPalid + "," + strIndat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("PROCESSING, MANDT, COMCD ,WERKS, LGORT,'" + strLocat + "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL,  ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, PKDAT ");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE>OTQTY  ");
                alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS')");
                alConditions.Add(" MBLNR='" + strPalid + "'");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region  查詢連線入庫資料(G+)by 多笔Pallet ID  add by Refun 170622
            //==========================================================================================
            ////////////Summary by Smose Liao 20091023//////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by Pallet ID
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strPalid">PalletID。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataByPalid(strLocat, strPalid, strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataByPalids(string strLocat, string strPalids, string strIndat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByPalids";
                this.ControlMethodParm = "(" + strLocat + "," + strPalids + "," + strIndat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("MANDT,COMCD, WERKS, LGORT,'" + strLocat + "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID,KOSTL,  ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, PKDAT ");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" MENGE>OTQTY  ");
                alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS')");
                alConditions.Add(" MBLNR IN (" + strPalids + " )");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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


            #region  查詢連線入庫資料(G+) by Reference ID
            //==========================================================================================
            ////////////Summary by Smose Liao 20101228//////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+) by Reference ID
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strRefid">Reference ID。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataByPalid(strLocat, strRefid, strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataByRefid(string strLocat, string strRefid, string strIndat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByRefid";
                this.ControlMethodParm = "(" + strLocat + "," + strRefid + "," + strIndat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("PROCESSING,MANDT,COMCD, WERKS, LGORT,'" + strLocat + "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE,'' as OMBLNR,'' as MRGID, KOSTL,  ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, REFID, MBLNR AS NMBLN, OTLGT, '' as DIDNO, CONVERT(VARCHAR, PKDAT, 120) as PKDAT");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE>OTQTY  ");
                alConditions.Add(" MTYPE='QMS_M'");
                alConditions.Add(" REFID='" + strRefid + "'");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region  查詢連線入庫資料(G+) by Reference ID && PALID add by Refun 170704
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strMethod">按照RID 或者是PID方式</param>
            /// <param name="id">RID&PID</param>
            /// <param name="strIndat">入库时间</param>
            /// <returns></returns>
            public DataTable QueryQMSLineInData(string strLocat, string strMethod, string id, string strIndat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInData";
                this.ControlMethodParm = "(" + strMethod + "," + id + "," + strIndat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strSQL = "";
                if (strMethod == "RID")
                {
                    strSQL = "SELECT MANDT, COMCD,WERKS, LGORT,'" + strLocat + "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL,  "
               + "ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, REFID,MBLNR AS NMBLN, OTLGT, '' as DIDNO, "
               + "CONVERT(VARCHAR, PKDAT, 120) as PKDAT  FROM WHDWN WHERE MENGE>OTQTY AND MTYPE='QMS_M'  AND  "
               + " MANDT='" + MANDT + "' AND  COMCD='" + COMCD + "' AND WERKS='" + WERKS + "' AND  REFID in ('" + id + "') ";

                }
                if (strMethod == "PID")
                {
                    strSQL = " SELECT  MANDT, COMCD,WERKS, LGORT,'" + strLocat + "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL,  "
               + "ARBPL,TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, PKDAT,OTLGT,'' AS NMBLN "
               + " FROM WHDWN WHERE MENGE>OTQTY AND MTYPE IN ('QMS_M' ,'QMS')  AND "
               + " MANDT='" + MANDT + "' AND  COMCD='" + COMCD + "' AND WERKS='" + WERKS + "' AND  REFID in ('" + id + "') ";
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 列出QMS連線入庫的PalletID資料(G+)
            //====================================================================================================
            ////////////Summary by Smose Liao 20091026////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出QMS連線入庫的PalletID資料(G+)
            /// </summary> 
            /// <param name="strMblnr">PalletID。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListQMSInPALData(strMblnr, string strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListQMSInPALData(string strMblnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListQMSInPALData";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //string strSQL = "Select distinct MBLNR from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "'  and LGORT= '" + LGORT + "' and MENGE> OTQTY and TRNTP in ('G+','R+','M+') and MTYPE='QMS' ";
                alColumns.Add("MBLNR");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MENGE>OTQTY  ");
                alConditions.Add(" TRNTP in ('G+','R+','M+')");
                alConditions.Add(" MTYPE='QMS' ");

                if (strMblnr != "")
                {
                    //strSQL += " and MBLNR= '" + strMblnr + "'";
                    alConditions.Add(" MBLNR='" + strMblnr + "'");
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK= '" + strInsmk + "'";
                    alConditions.Add(" INSMK='" + strInsmk + "'");
                }

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, true, true);
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


            #region  查詢連線入庫資料(G+)by PalletID
            //========================================================================================================================
            ////////////Summary by Smose Liao 20091026////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by PalletID
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">PalletID。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            ///  <param name="strInsmk">庫別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInPALData(strLocat,strMblnr,strMatnr,strIndat, string strInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInPALData(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInPALData";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //string strSQL = "Select MANDT, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO  from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and MBLNR= '" + strMblnr + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'";
                alColumns.Add("MANDT, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" MBLNR='" + strMblnr + "'");
                alConditions.Add(" MENGE>OTQTY  ");
                alConditions.Add(" TRNTP in ('G+','M+','R+')");
                alConditions.Add(" MTYPE='QMS' ");

                if (strMatnr != "")
                {
                    //strSQL += " and MATNR= '" + strMatnr + "'";
                    alConditions.Add(" MATNR='" + strMatnr + "'");
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK= '" + strInsmk + "'";
                    alConditions.Add(" INSMK='" + strInsmk + "'");
                }

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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


            #region 查詢連線入庫資料(G+)by Sernal No
            //===============================================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by Sernal No
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">PalletID。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataBySerno(strLocat,strSerno,strMatnr,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataBySerno(string strLocat, string strSerno, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataBySerno";
                this.ControlMethodParm = "(" + strLocat + "," + strSerno + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = " Select MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, RMANO, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, WO, LOADID, MODEL, REGION,BOXID, PALQTY " +//ADD BOXID BY KAREN
                                " From WHDWN with (nolock) " +
                                " Where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'" +
                                " and MBLNR= (Select top 1 MBLNR From WHDWN with (nolock) Where SERNO = '" + strSerno + "' and MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and MTYPE='QMS' order by CRDAT desc) ";

                if (strMatnr != "")
                {
                    strSQL += " and MATNR= '" + strMatnr + "'";
                }

                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            public DataTable QueryQMSLineInDataBySerno_SN(string strLocat, string strSerno, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataBySerno";
                this.ControlMethodParm = "(" + strLocat + "," + strSerno + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = " Select top 1 MANDT, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO,BOXID,WO,LOADID " +//ADD BOXID BY KAREN
                                " From WHDWN with (nolock) " +
                                " Where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'" +
                                "       AND SERNO = '" + strSerno + "'";

                if (strMatnr != "")
                {
                    strSQL += " and MATNR= '" + strMatnr + "'";
                }

                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";
                }

                strSQL += " order by CRDAT desc";

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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

            public DataTable QueryQMSLineInDataByPID(string MBLNR, string strMatnr, string strInsmk, ArrayList SN)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByPID";
                this.ControlMethodParm = "(" + MBLNR + "," + strMatnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = " Select SERNO" +
                                " From WHDWN with (nolock) " +
                                " Where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'" +
                                "       and MBLNR='" + MBLNR + "'";

                string strSQL2 = " and SERNO NOT IN (";
                for (int i = 0; i < SN.Count; i++)
                {
                    if (i != SN.Count - 1)
                    {
                        strSQL2 = strSQL2 + "'" + SN[i].ToString() + "',";
                    }
                    else
                    {
                        strSQL2 = strSQL2 + "'" + SN[i].ToString() + "'";
                    }
                }

                strSQL2 = strSQL2 + ")";

                if (strSQL2 != "")
                    strSQL = strSQL + strSQL2;


                if (strMatnr != "")
                {
                    strSQL += " and MATNR= '" + strMatnr + "'";
                }

                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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

            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// SAP單據查詢 strInsmk:庫別, strStartDate:下載開始時間, strEndDate:下載結束時間, strStartMatnr:料號開始, strEndMatnr:料號結束, strMType:單據類型(SAP、QMS、SDS)
            /// 20051012為了修改庫存比對而Overload該方法(多了一個strMType參數，判斷SAP、QMS、SDS)
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  DataTable dtData = objSapData.QuerySapData(strInsmk, strStartDate, strEndDate, strStartMatnr, strEndMatnr, strStartMblnr, strEndMblnr, intType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QuerySapData(string strStartDate, string strEndDate, int intType, string strStartMatnr, string strEndMatnr, string strMType)
            {
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alQueryCondition = new ArrayList();
                ArrayList alGroupby = new ArrayList();

                alColumns.Add("MANDT");
                alColumns.Add("COMCD");
                alColumns.Add("WERKS");
                alColumns.Add("LGORT");
                alColumns.Add("MATNR");
                alColumns.Add("INSMK");
                alColumns.Add("CHARG");
                alColumns.Add("right(TRNTP,1) as TRNTP");
                alColumns.Add("sum(MENGE) as MENGE");
                alColumns.Add("sum(OTQTY) as OTQTY");
                alColumns.Add("sum(MENGE)-sum(OTQTY) as BALANCE");

                alQueryCondition.Add("MANDT='" + UserData.Client + "'");
                alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                alQueryCondition.Add("WERKS = '" + WERKS + "'");

                //string strSQL = "Select MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, right(TRNTP,1) as TRNTP, sum(MENGE) as MENGE, sum(OTQTY) as OTQTY, sum(MENGE)-sum(OTQTY) as BALANCE from WHDWN where MANDT='" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' ";

                if (strStartDate != "" && strEndDate != "")
                {
                    //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                    alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    //strSQL += " and CRDAT = '" + strEndDate + "'";
                    alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    //strSQL += " and CRDAT = '" + strStartDate + "'";
                    alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    //strSQL += " and MATNR = '" + strEndMatnr + "'";
                    alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    //strSQL += " and MATNR = '" + strStartMatnr + "'";
                    alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                }

                if (intType == 0)
                {
                    //strSQL += " and MENGE=OTQTY";
                    alQueryCondition.Add("MENGE=OTQTY");
                }
                else if (intType == 1)
                {
                    //strSQL += " and MENGE>OTQTY";
                    alQueryCondition.Add("MENGE>OTQTY");
                }

                if (strMType.Trim() != "")
                {
                    if (strMType.Trim() == "SAP")
                    {
                        //strSQL += " and MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'";
                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                    }
                    else
                    {
                        //strSQL += " and MTYPE='" + strMType.Trim() + "' ";
                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                    }
                }

                alGroupby.Add("MANDT");
                alGroupby.Add("COMCD");
                alGroupby.Add("WERKS");
                alGroupby.Add("LGORT");
                alGroupby.Add("MATNR");
                alGroupby.Add("INSMK");
                alGroupby.Add("CHARG");
                alGroupby.Add("right(TRNTP,1)");

                string strFromTable = "WHDWN";

                //strSQL += " group by MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, right(TRNTP,1)";
                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhdwn.ControlQuery(strFromTable, alColumns, alQueryCondition, false, alGroupby, "");
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL);
                    //ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapData()";
                }
                return dtData;

            }

            #region ListSDSOutData()
            //=======================================================================================================
            ////////////Summary by Smose Liao////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出SDS連線出庫的單據資料(G-)
            /// </summary> 
            /// <param name="strQuery">查詢條件。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSDSOutData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSDSOutData(string strDocNoQuery, string strModel, String strRegion, string strCrdat)
            {
                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("Distinct MBLNR");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                alConditions.Add(" MENGE>OTQTY ");
                alConditions.Add(" TRNTP in ('G-','R-','M-') ");
                alConditions.Add(" MTYPE='SDS' ");
                if (strDocNoQuery.Trim() != "")
                {
                    alConditions.Add(" MBLNR LIKE '" + strDocNoQuery.Trim() + "'");
                }
                if (strModel.Trim() != "")
                {
                    alConditions.Add(" MATNR LIKE '%" + strModel + "%'");
                }
                if (strRegion.Trim() != "")
                {
                    alConditions.Add(" LIFNR LIKE '%" + strRegion.Trim() + "%'");
                }
                if (strCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + strCrdat + " 00:00:00.000' and '" + strCrdat + " 23:59:59.999')");
                }

                try
                {
                    ControlHandleDB();
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListSDSOutData()";
                }
                return dtData;
            }
            #endregion


            #region 查詢聯機入庫(成品)的資料(By Serial No.)  Smose Liao 20141121
            //=============================================================================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by Sernal No
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMblnr">PalletID。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataBySerno(strLocat,strSerno,strMatnr,strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataBySerno(string strLocat, string strSerno, string strMatnr, string strIndat, string strInsmk, string strLogrt, bool bolRetry)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataBySerno";
                this.ControlMethodParm = "(" + strLocat + "," + strSerno + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strSQL = "";
                if (bolRetry)
                {
                    //代表要重刷S/N，所以不卡Menge > Otqty的where條件
                    strSQL = " Select MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, RMANO, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, WO, LOADID, MODEL, REGION,BOXID, PALQTY " +//ADD BOXID BY KAREN
                    " From WHDWN with (nolock) " +
                    " Where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and TRNTP in ('G+','M+','R+') and MTYPE='QMS'" +
                    " and MBLNR= (Select top 1 MBLNR From WHDWN with (nolock) Where SERNO = '" + strSerno + "' and MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and MTYPE='QMS' order by CRDAT desc) ";
                }
                else
                {
                    strSQL = " Select MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' as LOCAT, MATNR, INSMK, CHARG, (MENGE-OTQTY) as MENGE, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL, ARBPL as ARBPL, TRNTP, EBELN, LIFNR, RMANO, '' as RMAK1, '" + strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, WO, LOADID, MODEL, REGION,BOXID, PALQTY " +//ADD BOXID BY KAREN
                    " From WHDWN with (nolock) " +
                    " Where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and MENGE>OTQTY and TRNTP in ('G+','M+','R+') and MTYPE='QMS'" +
                    " and MBLNR= (Select top 1 MBLNR From WHDWN with (nolock) Where SERNO = '" + strSerno + "' and MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + strLogrt + "' and MTYPE='QMS' order by CRDAT desc) ";
                }

                if (strMatnr != "")
                {
                    strSQL += " and MATNR= '" + strMatnr + "'";
                }

                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 列出SAP連線出庫的單據資料(G-,T-) by Marc Hong
            //==================================================================================
            ////////////Summary by Marc Hong////////////////////////////////////////////////////
            /// <summary>
            /// 列出SAP連線出庫的單據資料(G-,T-)
            /// </summary> 
            /// <param name="varMblnr">MBLNR的前幾碼。</param>
            /// <param name="varCrdat">查詢特定一天的資料。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapOutData(varMblnr, varCrdat, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapOutData(string varMblnr, string varCrdat, string varOutType)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                #region 增列祥天計畫單據  Smose Liao 20100305
                //alConditions.Add("(MTYPE = 'SAP')");
                alConditions.Add("(MTYPE in ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                #endregion

                if (varMblnr.Trim() != "")
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");

                if (varCrdat.Trim() != "")
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                    case "TRANSFER":
                        alConditions.Add("(TRNTP='T-')");
                        alConditions.Add(" (BWART<>'321') ");
                        break;
                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','T-','R-','M-'))");
                        break;
                    case "ONLINEHUB":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                }


                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- ListSapOutData ";
                    //throw new System.Exception(ex.Message +"<- ListSapOutData " );

                    ERRMSG = ex.Message + "<- ListSapOutData()";
                }
                return dtResult;
            }
            //增加根据部门代号筛选
            public DataTable ListSapOutData(string varMblnr, string varCrdat, string varOutType, string varDept)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" MANDT,COMCD,MBLNR,ZEILE,WERKS,LGORT,TRNTP,MATNR,INSMK,CHARG,MENGE,OTQTY,BWART,UMLGO,USNAM,LIFNR,IIF(BWART='311' and TRNTP='T-',UMLGO,KOSTL)KOSTL,KDMAT,CRDAT,GRLOC,REFID,ARBPL ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                #region 增列祥天計畫單據  Smose Liao 20100305
                //alConditions.Add("(MTYPE = 'SAP')");
                alConditions.Add("(MTYPE in ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                #endregion

                if (varMblnr.Trim() != "")
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");

                if (varCrdat.Trim() != "")
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");

                if (varDept.Trim() != "")
                    alConditions.Add("(KOSTL in  (" + varDept + "))");//根据部门筛选

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                    case "TRANSFER":
                        alConditions.Add("(TRNTP='T-')");
                        alConditions.Add(" (BWART<>'321') ");
                        break;
                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','T-','R-','M-'))");
                        break;
                    case "ONLINEHUB":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                }


                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- ListSapOutData ";
                    //throw new System.Exception(ex.Message +"<- ListSapOutData " );

                    ERRMSG = ex.Message + "<- ListSapOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 列出SAP連線出庫的單據資料(G-,T-) by Marc Hong
            //==================================================================================
            ////////////Summary by Marc Hong////////////////////////////////////////////////////
            /// <summary>
            /// 列出SAP連線出庫的單據資料(G-,T-)
            /// </summary> 
            /// <param name="varMblnr">MBLNR的前幾碼。</param>
            /// <param name="varCrdat">查詢特定一天的資料。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapOutData(varMblnr, varCrdat, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapOutDataTransferCSMC(string varMblnr, string varCrdat, string varOutType)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                #region 增列祥天計畫單據  Smose Liao 20100305
                alConditions.Add("(MTYPE = 'SAP')");
                //alConditions.Add("(MTYPE in ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                #endregion

                if (varMblnr.Trim() != "")
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");

                if (varCrdat.Trim() != "")
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                    case "TRANSFERIN":
                        //alConditions.Add(" (MATNR='20%' OR MATNR LIKE '2L%') AND ( BWART='101')");
                        alConditions.Add(" (MATNR LIKE '20%' OR MATNR LIKE '2L%') AND ( BWART='101')");
                        break;
                    case "TRANSFEROUT":
                        //alConditions.Add(" (MATNR='20%' OR MATNR LIKE '2L%') AND ( BWART='351')");
                        alConditions.Add(" (MATNR LIKE '20%' OR MATNR LIKE '2L%') AND ( BWART='351')");
                        break;
                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','T-','R-','M-'))");
                        break;
                    case "ONLINEHUB":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                }

                //brian 20150217
                //string strSql = objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, false);

                try
                {
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- ListSapOutData ";
                    //throw new System.Exception(ex.Message +"<- ListSapOutData " );

                    ERRMSG = ex.Message + "<- ListSapOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 列出SAP連線出庫的單據資料(G-,T-)(考慮線別) by Smose Liao 20100503
            //=================================================================================================
            ////////////Summary by Marc Hong///////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出SAP連線出庫的單據資料(G-,T-)
            /// </summary> 
            /// <param name="varMblnr">MBLNR的前幾碼。</param>
            /// <param name="varCrdat">查詢特定一天的資料。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapOutData(varMblnr, varCrdat, varOutType, varArbpl);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapOutData(string varMblnr, string varCrdat, string varOutType, string varArbpl, string varSendID)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                #region 增列祥天計畫單據  Smose Liao 20100305
                //alConditions.Add("(MTYPE = 'SAP')");
                alConditions.Add("(MTYPE in ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                #endregion
                #region 增添AGV Kitting Ryan Tsai 20131212
                if (varSendID != "")
                {
                    alConditions.Add("(REFID='" + varSendID + "')");
                }
                #endregion
                if (varMblnr.Trim() != "")
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");

                if (varCrdat.Trim() != "")
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");

                if (varArbpl.Trim() != "")
                    alConditions.Add("(ARBPL= '" + varArbpl + "')");



                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                    case "TRANSFER":
                        alConditions.Add("(TRNTP='T-')");
                        break;
                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','T-','R-','M-'))");
                        break;
                    case "ONLINEHUB":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                }

                //brian 20150309
                string strSql = objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);

                try
                {
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListSapOutData()";
                }
                return dtResult;
            }
            //增加部门代号筛选
            public DataTable ListSapOutData(string varMblnr, string varCrdat, string varOutType, string varArbpl, string varDept, string varSendID)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                //alColumns.Add(" * ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" REFID ");
                alColumns.Add(" ARBPL ");
                alColumns.Add(" CRDAT ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" MENGE - OTQTY as BALANCE");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                #region 增列祥天計畫單據  Smose Liao 20100305
                //alConditions.Add("(MTYPE = 'SAP')");
                alConditions.Add("(MTYPE in ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                #endregion
                #region 增添AGV Kitting Ryan Tsai 20131212
                if (varSendID != "")
                {
                    alConditions.Add("(REFID='" + varSendID + "')");
                }
                #endregion
                if (varMblnr.Trim() != "")
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");

                if (varCrdat.Trim() != "")
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");

                if (varArbpl.Trim() != "")
                    alConditions.Add("(ARBPL= '" + varArbpl + "')");

                if (varDept.Trim() != "")
                    alConditions.Add("(KOSTL in  (" + varDept + "))");//根据部门筛选

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                    case "TRANSFER":
                        alConditions.Add("(TRNTP='T-')");
                        break;
                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','T-','R-','M-'))");
                        break;
                    case "ONLINEHUB":
                        alConditions.Add("(TRNTP in ('G-','R-','M-'))");
                        break;
                }

                //brian 20150309
                string strSql = objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);

                try
                {
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListSapOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 列出SI出庫的單據資料(G-)
            //==================================================================================
            ////////////Summary by Smose Liao 20100420//////////////////////////////////////////
            /// <summary>
            /// 列出SI出庫的單據資料(G-)
            /// </summary> 
            /// <param name="varMblnr">SI No.的前幾碼。</param>
            /// <param name="varCrdat">查詢特定一天的資料。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSIOutData(varMblnr, varCrdat, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSIOutData(string varMblnr, string varCrdat, string varOutType)
            {

                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                //alConditions.Add("(LGORT= '" + LGORT + "')");
                //by blank 20151222
                if (LGORT.Trim() != "")
                { alConditions.Add("(LGORT= '" + LGORT + "')"); }

                alConditions.Add("(MTYPE ='" + varOutType + "')");
                alConditions.Add("(MENGE>OTQTY)");
                alConditions.Add("(TRNTP in ('G-'))");

                if (varMblnr.Trim() != "")
                {
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                try
                {
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListSIOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 列出SI出庫的單據資料(G-)(DOA資料)
            //==================================================================================
            ////////////Summary by Smose Liao 20110121//////////////////////////////////////////
            /// <summary>
            /// 列出SI出庫的單據資料(G-)(DOA資料)
            /// </summary> 
            /// <param name="varMblnr">SI No.的前幾碼。</param>
            /// <param name="varCrdat">查詢特定一天的資料。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSIOut(varMblnr, varCrdat, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListDOASIOut(string varMblnr, string varCrdat, string varOutType)
            {

                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Clear();
                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(MTYPE= '" + varOutType + "')");
                alConditions.Add("(MENGE>OTQTY)");
                alConditions.Add("(TRNTP in ('G-'))");
                alConditions.Add("(ODTYP <> '')");

                if (varMblnr.Trim() != "")
                {
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                try
                {
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListDOASIOut()";
                }
                return dtResult;
            }
            #endregion


            #region 列出連線出庫(產生單據出庫)(PowerⅡ-QWMS)的單據資料
            //==================================================================================
            ////////////Summary by Smose Liao 20101110//////////////////////////////////////////
            /// <summary>
            /// 列出連線出庫(產生單據出庫)(PowerⅡ-QWMS)的單據資料
            /// </summary> 
            /// <param name="varMblnr">SI No.的前幾碼。</param>
            /// <param name="varCrdat">建立日期。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSimulationOutData(varMblnr, varCrdat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSimulationOutData(string varMblnr, string varCrdat)
            {

                DataTable dtResult = new DataTable();
                DataTable dtTmp = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strMblnr = "";
                string strFrom = "";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" W.* ");
                alColumns.Add(" W.MENGE - W.OTQTY as BALANCE ");

                alConditions.Add("(W.MANDT= '" + MANDT + "')");
                alConditions.Add("(W.COMCD= '" + COMCD + "')");
                alConditions.Add("(W.WERKS= '" + WERKS + "')");
                alConditions.Add("(W.LGORT= '" + LGORT + "')");
                alConditions.Add("(W.TRNTP in ('T-'))");
                alConditions.Add(" W.MTYPE in ('QMS_QS', 'QMS_AS') ");//單據的型態只有SMT

                if (checkSMTaddWerksData().Rows.Count == 1)
                {
                    alConditions.Add(" isnull(M.MENGE,0) > 0 ");//库存数量大于0
                    alColumns.Add("M.LOCAT ");
                    strFrom = " WHDWN AS W left join WHITM AS M With(Nolock) on W.MANDT=M.MANDT AND W.COMCD=M.COMCD AND W.WERKS=M.WERKS AND W.LGORT=M.LGORT AND W.MATNR=M.MATNR  AND LOCAT IN (SELECT OLOCA FROM WHLOG WITH(NOLOCK) WHERE MBLNR LIKE '66%' AND (CRDAT Between  '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')) ";
                }
                else 
                {
                    strFrom = " WHDWN AS W ";
                }
                if (varMblnr.Trim() != "")
                {
                    alConditions.Add("(W.MBLNR like '" + varMblnr.Trim() + "%')");
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(W.CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                try
                {
                    string A = ControlGetQuerySql(strFrom, alColumns, alConditions, true);    

                   dtResult = ControlQuery(strFrom, alColumns, alConditions, true);

                   

                    if (dtResult.Rows.Count > 0)
                    {                  
                        StringBuilder sbSql = new StringBuilder();
                        DataTable dtsmtData = new DataTable();
                        string strSmt = "";
                        string strLocat = "";
                        if (checkSMTaddWerksData().Rows.Count == 1)
                        {
                            for (int i = dtResult.Rows.Count-1; i >= 0; i--)
                            {
                                strMblnr = dtResult.Rows[i]["MBLNR"].ToString();
                                sbSql.AppendFormat(@"SELECT DISTINCT OLOCA FROM WHLOG WITH(NOLOCK) WHERE WERKS= '" + WERKS + "'AND LGORT= '" + LGORT + "'AND MBLNR ='{0}'", strMblnr);
                                dtsmtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                                sbSql.Clear();
                                if (dtsmtData.Rows.Count > 1)
                                {                              
                                    for (int j =  dtsmtData.Rows.Count-1; j >=0; j--)
                                    {
                                        strSmt = dtsmtData.Rows[j]["OLOCA"].ToString();
                                        strLocat = dtResult.Rows[i]["LOCAT"].ToString();
                                        if (strSmt != strLocat)
                                        {
                                            dtsmtData.Rows.RemoveAt(j);
                                        }
                                    }
                                     if (dtsmtData.Rows.Count !=1)
                                     {
                                         dtResult.Rows.RemoveAt(i);
                                     }
                                }
                                else
                                {
                                    strSmt = dtsmtData.Rows[0]["OLOCA"].ToString();
                                    strLocat = dtResult.Rows[i]["LOCAT"].ToString();
                                    if (strSmt != strLocat)
                                    {
                                        dtResult.Rows.RemoveAt(i);
                                    }
                                }
                                              
                            }
                        }
                        dtTmp = dtResult.Clone();
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            //捨棄原祥天計畫-模擬SAP產生虛擬單據功能的資料(長度10碼)
                            strMblnr = dtResult.Rows[i]["MBLNR"].ToString();
                            if (strMblnr.Length == 14)
                            {
                                dtTmp.ImportRow(dtResult.Rows[i]);
                            }
                        }    
                     
                        dtResult.Clear();
                        dtResult = dtTmp.Copy();
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- ListSimulationOutData()";
                }
                return dtResult;
            }
            #endregion

            #region  获取散料加扣获取库存的厂区仓别
            public DataTable checkSMTaddWerksData()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "checkSMTaddWerksData";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendFormat(@"SELECT CTRLNM FROM WHCTRL WITH(NOLOCK) WHERE SOLDTO='QWMS' AND CTRLID='SMT_ADD' AND REMAK ='ListSMTOutData' AND CTRLNM ='{0}'AND CTRLC1='{1}'", WERKS, LGORT);


                DataTable dtData = new DataTable();
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


            #region 查詢SAP連線出庫單據的卷數
            //============================================================================================
            ////////////Summary by Smose Liao 20100920////////////////////////////////////////////////////
            /// <summary>
            /// 查詢SAP連線出庫單據的卷數
            /// </summary> 
            /// <param name="varWerks">廠區。</param>
            /// <param name="varGrpid">Group id。</param>
            /// <param name="varMatnr">料號。</param>
            /// <returns>
            /// String。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapDocRlqty(varWerks, varGrpid, varMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////
            public String QuerySapDocRlqty(string varWerks, string varGrpid, string varMatnr)
            {
                DataTable dtResult = new DataTable();
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strRlqty = "";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" * ");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(MATNR= '" + varMatnr + "')");

                if (varGrpid.Trim() != "")
                {
                    alConditions.Add("(GRPID = '" + varGrpid + "')");
                }

                try
                {
                    dtResult = objWhsmt.EntityQuery(alColumns, alConditions, false, false);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapDocRlqty()";
                }

                if (dtResult.Rows.Count > 0)
                {
                    strRlqty = dtResult.Rows[0]["RLQTY"].ToString();
                }

                return strRlqty;

            }
            #endregion


            #region 查詢SAP連線出庫單據的Sequence No.
            //============================================================================================
            ////////////Summary by Smose Liao 20100426////////////////////////////////////////////////////
            /// <summary>
            /// 查詢SAP連線出庫單據的Sequence No.
            /// </summary> 
            /// <param name="varWerks">廠區。</param>
            /// <param name="varSenid">Send id。</param>
            /// <param name="varMatnr">料號。</param>
            /// <param name="varArbpl">線別第一碼。</param>
            /// <returns>
            /// String。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapSeqno(varWerks, varSenid, varMatnr, varArbpl);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////
            public string QuerySapSeqno(string varWerks, string varSenid, string varMatnr, string varArbpl)
            {

                DataTable dtResult = new DataTable();
                DataWhsid objWhsid = new DataWhsid(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strSeqno = "";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" * ");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(MATNR= '" + varMatnr + "')");

                if (varSenid.Trim() != "")
                {
                    alConditions.Add("(SENID = '" + varSenid + "')");
                }

                if (varArbpl.Trim() != "")
                {
                    alConditions.Add("(ARBPL LIKE '" + varArbpl + "%')");
                }

                try
                {
                    dtResult = objWhsid.EntityQuery(alColumns, alConditions, false, false);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapSeqno()";
                }

                if (dtResult.Rows.Count > 0)
                {
                    strSeqno = dtResult.Rows[0]["SEQNO"].ToString();
                }

                return strSeqno;
            }
            #endregion


            #region 傳入PowerⅡ單據號碼，查詢連線出庫資料(T-)
            //=========================================================================================
            ////////////Summary by Smose Liao 20101028/////////////////////////////////////////////////
            /// <summary>
            /// 傳入PowerⅡ單據號碼，查詢連線出庫資料(T-)
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryPower2LineOutData(aryMblnr, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryPower2LineOutData(string strGrpid, string strLogrts)
            {
                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                if (!strLogrts.StartsWith("'"))
                    strLogrts = "'" + strLogrts + "'";
                sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,MTYPE,MBLNR,ZEILE,MATNR,INSMK,CHARG,LIFNR,RMANO,(MENGE-OTQTY) as MENGE,0 as ALQTY,EBELN,PRCDE,PUTYP,KOSTL,RESLT,BUDAT,PRITY,ARBPL,TRNTP,KDMAT,SERNO,REFID,INTID FROM WHDWN WITH(NOLOCK) WHERE MANDT='{0}' AND  COMCD='{1}' AND  WERKS='{2}' AND LGORT IN({3}) AND MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS') AND MBLNR IN(SELECT MBLNR FROM WHQDP WITH(NOLOCK) WHERE MANDT='{0}' AND  COMCD='{1}' AND  WERKS='{2}' AND LGORT IN({3}) AND GRPID='{4}') AND TRNTP in ('G-','T-')", strMandt, strComcd, strWerks, strLogrts, strGrpid);

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryPower2LineOutData()";
                }
                return dtResult;
            }
            #endregion

            public DataTable QueryPower2LineOutDataAdd(ArrayList varMblnr, string varOutType)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbMblnr = new StringBuilder();

                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(MBLNR Like '" + varMblnr[i].ToString() + "%')");
                }

                alColumns.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" MTYPE ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                //alColumns.Add(" MENGE ");//WHDWN在產生單據的同時，其實已經扣帳
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" PRCDE ");
                alColumns.Add(" PUTYP ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" RESLT ");
                alColumns.Add(" BUDAT ");
                alColumns.Add(" PRITY ");
                alColumns.Add(" ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" KDMAT ");
                alColumns.Add(" SERNO ");
                alColumns.Add(" REFID ");
                alColumns.Add(" INTID ");

                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                //祥天計畫單據 
                alConditions.Add("(MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
                alConditions.Add("(" + sbMblnr.ToString() + ")");

                if (varOutType == "ONLINE")
                {
                    alConditions.Add("(TRNTP in ('G-','T-'))");
                }

                sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryPower2LineOutData()";
                }
                return dtResult;
            }

            #region 傳入單據號碼查詢RMA入庫資料
            //====================================================================================================
            ////////////Summary by Smose Liao 20110223////////////////////////////////////////////////////////////
            /// <summary>
            /// 傳入單據號碼查詢RMA入庫資料
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapRmaData(varMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapRmaData(ArrayList varMblnr)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbMblnr = new StringBuilder();

                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(MBLNR Like '" + varMblnr[i].ToString() + "%')");
                }

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" * ");

                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                alConditions.Add("(" + sbMblnr.ToString() + ")");
                alConditions.Add("(MTYPE= 'SAP_DOA')");

                sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapRmaData()";
                }
                return dtResult;
            }
            #endregion


            #region 傳入單據號碼查詢連線出庫資料(G-)
            //=========================================================================================
            ////////////Summary by Marc Hong //////////////////////////////////////////////////////////
            /// <summary>
            /// 傳入單據號碼查詢連線出庫資料(G-) 
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineOutData(aryMblnr, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineOutData(ArrayList varMblnr, string varOutType)
            {
                DataTable dtResult = new DataTable();
                //string strMblnr = Array2String(varMblnr);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                StringBuilder sbMblnr = new StringBuilder();

                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(MBLNR Like '" + varMblnr[i].ToString() + "%')");
                }

                alColumns.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" MTYPE ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" PRCDE ");
                alColumns.Add(" PUTYP ");
                alColumns.Add(" IIF(BWART='311' and TRNTP='T-',UMLGO,KOSTL) KOSTL ");
                alColumns.Add(" RESLT ");
                alColumns.Add(" BUDAT ");
                alColumns.Add(" PRITY ");
                alColumns.Add(" ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" KDMAT ");
                alColumns.Add(" SERNO ");
                alColumns.Add(" REFID ");
                alColumns.Add(" INTID ");
                alColumns.Add(" UMLGO ");
                //20150305 brian 增加
                alColumns.Add(" MENGE as DCQTY ");//SAP单据原始数量
                alColumns.Add(" 0 as SCQTY ");//Scanned数量
                alColumns.Add(" (MENGE-OTQTY) as ALMNG ");//Partial数量，默认等于未出库数量，用户可以修改


                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");

                if (varOutType == "ONLINEPROD")
                {
                    alConditions.Add("(MTYPE='SDS')");
                }
                else if (varOutType == "SPARE_PARTS")
                {
                    alConditions.Add("(MTYPE = 'SAP_SI')");
                }
                else
                {
                    //祥天計畫單據 
                    alConditions.Add("(MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS','SAP_DOA'))");
                }

                alConditions.Add("(" + sbMblnr.ToString() + ")");

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','M-','R-','T-'))");
                        break;

                    case "TRANSFER":
                        alConditions.Add("(TRNTP = 'T-')");
                        break;

                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','M-','R-', 'T-'))");
                        break;

                    case "ONLINEPROD":
                        alConditions.Add("(TRNTP in ('G-','M-','R-','T-'))");
                        break;

                    case "SPARE_PARTS":
                        alConditions.Add("(TRNTP in ('G-'))");
                        break;

                    case "TWOPHASEOUT":
                        alConditions.Add("(TRNTP in ('G-','T-'))");
                        break;
                    //Add By Michael 20150928 for Film出库
                    case "Film_OutMaterial":
                        alConditions.Add("(BWART in ('261','311'))");
                        alConditions.Add("(TRNTP in ('G-','M-','R-', 'T-'))");
                        break;
                }

                sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;//連線SQL Server的時間3分鐘
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return dtResult;
            }
            #endregion

            #region 查询转仓出库WHLOG，提供二次补印报表数据。
            public DataTable QueryWhlog(string strWERKS, string strLGORT, ArrayList strMBLNRs)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryWhlog";
                this.ControlMethodParm = "(" + strWERKS + "," + strLGORT + "," + strMBLNRs + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbMblnr = new StringBuilder();
                for (int i = 0; i < strMBLNRs.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(D.MBLNR Like '" + strMBLNRs[i].ToString() + "%')");
                }
                StringBuilder sbSql = new StringBuilder();
                //SELECT 用只读实例查询SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                sbSql.Append("select  G.MANDT,G.COMCD,G.WERKS,G.LGORT,G.OLOCA AS LOCAT,G.MATNR,G.INSMK,G.CHARG,''AS MENGE,ABS(G.MENGE)AS ALQTY,''AS BLACE,D.MBLNR,D.ZEILE,G.EBELN,G.LIFNR,G.RMANO,'' AS OMBLNR,G.MRGID,G.KOSTL,G.ARBPL,G.TRNTP,'' AS RMAK1,G.INDAT,G.KDMAT,G.SERNO,G.DACOD,G.LOCOD,G.INSPT,D.PKDAT,G.CONFIG FROM WHLOG AS G  INNER JOIN WHDWN AS D ON G.WERKS=D.WERKS AND G.LGORT=D.LGORT AND G.MBLNR=D.MBLNR WHERE ");
                sbSql.AppendFormat("D.MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND D.WERKS='{0}' ", strWERKS);
                sbSql.AppendFormat("AND D.LGORT='{0}' ", strLGORT);
                sbSql.AppendFormat("AND (" + sbMblnr.ToString() + ")");
                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = sqlAccess.GetDataTable(sbSql.ToString());
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

            #region 傳入單據號碼查詢連線出庫資料(G-)
            //=========================================================================================
            ////////////Summary by Marc Hong //////////////////////////////////////////////////////////
            /// <summary>
            /// 傳入單據號碼查詢連線出庫資料(G-) 
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineOutData(aryMblnr, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineOutDataCSMCTransfer(ArrayList varMblnr, string varOutType)
            {
                DataTable dtResult = new DataTable();
                //string strMblnr = Array2String(varMblnr);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                StringBuilder sbMblnr = new StringBuilder();

                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(MBLNR LIKE '" + varMblnr[i].ToString() + "%')");
                }

                alColumns.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" MTYPE ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" PRCDE ");
                alColumns.Add(" PUTYP ");
                alColumns.Add(" IIF(BWART='311' and TRNTP='T-',UMLGO,KOSTL) KOSTL ");
                alColumns.Add(" RESLT ");
                alColumns.Add(" BUDAT ");
                alColumns.Add(" PRITY ");
                alColumns.Add(" ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" KDMAT ");
                alColumns.Add(" SERNO ");
                alColumns.Add(" REFID ");
                alColumns.Add(" INTID ");
                alColumns.Add(" '' AS LOCAT ");

                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                // alConditions.Add("(MENGE>OTQTY)");

                if (varOutType == "ONLINEPROD")
                {
                    alConditions.Add("(MTYPE='SDS')");
                }
                else if (varOutType == "SPARE_PARTS")
                {
                    alConditions.Add("(MTYPE = 'SAP_SI')");
                }
                else if (varOutType == "TRANSFERIN")
                {
                    alConditions.Add("(MTYPE = 'SAP')");
                }
                else
                {
                    //祥天計畫單據 
                    alConditions.Add("(MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS','SAP_DOA'))");
                }

                alConditions.Add("(" + sbMblnr.ToString() + ")");

                switch (varOutType)
                {
                    case "ONLINE":
                        alConditions.Add("(TRNTP in ('G-','M-','R-','T-'))");
                        break;

                    case "TRANSFER":
                        alConditions.Add("(TRNTP = 'T-')");
                        break;

                    case "COMBINE":
                        alConditions.Add("(TRNTP in ('G-','M-','R-', 'T-'))");
                        break;

                    case "ONLINEPROD":
                        alConditions.Add("(TRNTP in ('G-','M-','R-','T-'))");
                        break;

                    case "SPARE_PARTS":
                        alConditions.Add("(TRNTP in ('G-'))");
                        break;

                    case "TWOPHASEOUT":
                        alConditions.Add("(TRNTP in ('G-','T-'))");
                        break;
                    case "TRANSFERIN"://Add BCM Material No.
                        alConditions.Add(
                            " (SUBSTRING(MATNR,1,4) IN('20JH','2LJH') OR MATNR IN (SELECT DISTINCT CTRLNM FROM WHCTRL WHERE MANDT=218 AND SOLDTO='QWMS' AND CTRLID='BCMPN')) AND ( BWART='101')  AND EBELN LIKE '46%'  AND (MENGE>OTQTY)");
                        break;

                }

                sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;//連線SQL Server的時間3分鐘
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return dtResult;
            }
            #endregion
            #region 傳入單據號碼查詢連線出庫資料(G-)-GB QueryWHTRATransfer_GB
            //=========================================================================================
            ////////////Summary by Marc Hong //////////////////////////////////////////////////////////
            /// <summary>
            /// 傳入單據號碼查詢連線出庫資料(G-) 
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineOutData(aryMblnr, varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryWHTRATransfer_GB(ArrayList varMblnr, string strMatnr, string strCharg)
            {
                DataTable dtResult = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string sbSql = null;
                string sbMblnr = null;
                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                    {
                        sbMblnr = (" or ");
                    }
                    sbMblnr += ("( B.MBLNR = '" + varMblnr[i].ToString() + "')");
                }
                if (string.IsNullOrEmpty(sbMblnr))
                {
                    throw new Exception("未获取到Pallet ID<- QuerySapLineOutDataCSMCTransfer_GB()");
                }
                else
                {
                    sbSql = string.Format(
                        @"SELECT  (CASE WHEN  ISNULL(A.BOXID +B.SERNO,'')='' THEN 'TRUE' ELSE 'FALSE' END )  CHKED  , 
A.MANDT ,A.COMCD ,A.WERKS,A.LGORT ,A.MBLNR,A.PLTID,A.BOXID,A.INSMK,A.CHARG,A.MATNR,A.SERNO  ,A.MENGE
FROM  dbo.WHTRA  A WITH(NOLOCK) INNER JOIN WHDWN B WITH(NOLOCK) ON B.MANDT = A.MANDT AND B.COMCD = A.COMCD AND B.WERKS = A.DWERK AND B.LGORT = A.DLGOR
					  AND B.MATNR = A.MATNR AND B.INSMK = A.INSMK AND B.CHARG = A.CHARG
                      AND (B.WERKS= '{1}') AND (B.LGORT= '{0}') AND (B.MATNR = '{3}') AND (B.CHARG = '{4}')
                      AND B.EBELN LIKE '46%'  AND (B.MENGE>B.OTQTY)  AND (B.MTYPE = 'SAP')  AND {2}                      
WHERE A.DWERK = '{1}' AND A.DLGOR = '{0}' AND (A.MATNR = '{3}') AND A.CHARG = '{4}' AND a.MBLNR LIKE '%'+SUBSTRING( B.REFID,CHARINDEX( '/',B.REFID)+1,LEN(B.REFID)-CHARINDEX( '/',B.REFID))+'%'  AND ISNULL( DMBLN ,'')='' AND  ISNULL( A.FLAGE,'')<>'Y' ", LGORT, WERKS, sbMblnr, strMatnr, strCharg);
                    sbSql += string.Format(@" UNION ALL  ");
                    sbSql += string.Format(
                        @"SELECT CHKED,MANDT ,COMCD ,WERKS,LGORT ,MBLNR,PLTID,BOXID,INSMK,CHARG,MATNR,''  AS SERNO ,SUM(MENGE) AS   MENGE  FROM (
SELECT  (CASE WHEN  ISNULL(A.BOXID +B.SERNO,'')='' THEN 'TRUE' ELSE 'FALSE' END )  CHKED  , 
A.MANDT ,A.COMCD ,A.WERKS,A.LGORT ,A.MBLNR,A.PLTID,A.BOXID,A.INSMK,A.CHARG,A.MATNR  ,A.MENGE  FROM  dbo.WHTRA  A  WITH(NOLOCK)
   INNER JOIN WHDWN B WITH(NOLOCK) ON B.MANDT = A.MANDT AND B.COMCD = A.COMCD AND B.WERKS = A.DWERK AND B.LGORT = A.DLGOR
					  AND B.MATNR = A.MATNR AND B.INSMK = A.INSMK AND B.CHARG = A.CHARG
                      AND (B.WERKS= '{1}') AND (B.LGORT= '{0}') AND (B.MATNR = '{3}') AND (B.CHARG = '{4}')
                      AND B.EBELN LIKE '46%'  AND (B.MENGE>B.OTQTY)  AND (B.MTYPE = 'SAP')  AND {2}   
WHERE A.DWERK = '{1}' AND A.DLGOR = '{0}' AND (A.MATNR = '{3}') AND A.CHARG = '{4}' AND a.MBLNR LIKE '%'+SUBSTRING( B.REFID,CHARINDEX( '/',B.REFID)+1,LEN(B.REFID)-CHARINDEX( '/',B.REFID))+'%'  AND ISNULL( DMBLN ,'')='' AND  ISNULL( A.FLAGE,'')='Y'
  ) AS T  GROUP  BY CHKED,MANDT ,COMCD ,WERKS,LGORT ,MBLNR,PLTID,BOXID,INSMK,CHARG,MATNR ", LGORT, WERKS, sbMblnr, strMatnr, strCharg);

                }

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;//連線SQL Server的時間3分鐘
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutDataCSMCTransfer_GB()";
                }
                return dtResult;
            }
            #endregion

            //#region 傳入單據號碼查詢連線出庫資料(G-)(考慮料號的前2碼)
            ////============================================================================================
            //////////////Summary by Smose Liao 20101020 ///////////////////////////////////////////////////
            ///// <summary>
            ///// 傳入單據號碼查詢連線出庫資料(G-) 
            ///// </summary> 
            ///// <param name="varMblnr">單據號碼。</param>
            ///// <param name="varOutType">出庫類型。</param>
            ///// <param name="varMatnr">料號的前兩碼。</param>
            ///// <returns>
            ///// DataTable。
            ///// </returns>
            ///// <example>
            ///// <code>
            ///// <remarks>
            /////  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            /////  DataTable  dtData = objSapData.QuerySapLineOutData(aryMblnr, varOutType, varMatnr);
            /////  Your Code Here......
            ///// </remarks>
            ///// </code>
            ///// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////
            //public DataTable QuerySapLineOutData(ArrayList varMblnr, string varOutType, ArrayList varMatnr)
            //{
            //    DataTable dtResult = new DataTable();
            //    DataWhdwn objWhdwn = new DataWhdwn(UserData);
            //    ArrayList alColumns = new ArrayList();
            //    ArrayList alConditions = new ArrayList();
            //    StringBuilder sbSql = new StringBuilder();

            //    StringBuilder sbMblnr = new StringBuilder();
            //    StringBuilder sbMatnr = new StringBuilder();

            //    for (int i = 0; i < varMblnr.Count; i++)
            //    {
            //        if (i != 0)
            //            sbMblnr.Append(" or ");

            //        sbMblnr.Append("(MBLNR Like '" + varMblnr[i].ToString() + "%')");
            //    }

            //    for (int i = 0; i < varMatnr.Count; i++)
            //    {
            //        if (i != 0)
            //            sbMatnr.Append(" or ");

            //        sbMatnr.Append("(MATNR Like '" + varMatnr[i].ToString() + "%')");
            //    }

            //    alColumns.Clear();
            //    alColumns.Add(" MANDT ");
            //    alColumns.Add(" COMCD ");
            //    alColumns.Add(" WERKS ");
            //    alColumns.Add(" LGORT ");
            //    alColumns.Add(" MTYPE ");
            //    alColumns.Add(" MBLNR ");
            //    alColumns.Add(" ZEILE ");
            //    alColumns.Add(" MATNR ");
            //    alColumns.Add(" INSMK ");
            //    alColumns.Add(" CHARG ");
            //    alColumns.Add(" LIFNR ");
            //    alColumns.Add(" RMANO ");
            //    alColumns.Add(" (MENGE-OTQTY) as MENGE ");
            //    alColumns.Add(" 0 as ALQTY ");
            //    alColumns.Add(" EBELN ");
            //    alColumns.Add(" PRCDE ");
            //    alColumns.Add(" PUTYP ");
            //    alColumns.Add(" KOSTL ");
            //    alColumns.Add(" RESLT ");
            //    alColumns.Add(" BUDAT ");
            //    alColumns.Add(" PRITY ");
            //    alColumns.Add(" ARBPL ");
            //    alColumns.Add(" TRNTP ");
            //    alColumns.Add(" KDMAT ");
            //    alColumns.Add(" SERNO ");
            //    alColumns.Add(" REFID ");
            //    alColumns.Add(" INTID ");

            //    alConditions.Clear();
            //    alConditions.Add("(MANDT='" + MANDT + "')");
            //    alConditions.Add("(COMCD='" + COMCD + "')");
            //    alConditions.Add("(WERKS= '" + WERKS + "')");
            //    alConditions.Add("(LGORT= '" + LGORT + "')");
            //    alConditions.Add("(MENGE>OTQTY)");

            //    if (varOutType == "ONLINEPROD")
            //    {
            //        alConditions.Add("(MTYPE='SDS')");
            //    }
            //    else if (varOutType == "SPARE_PARTS")
            //    {
            //        alConditions.Add("(MTYPE = 'SAP_SI')");
            //    }
            //    else if (varOutType == "TWOPHASEOUT")
            //    {
            //        //兩階段出庫單據
            //        alConditions.Add("(MTYPE IN ('SAP','QMS_SQ','QMS_SA','QMS_FQ','QMS_FA'))");
            //    }
            //    else
            //    {
            //        //祥天計畫單據 
            //        alConditions.Add("(MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS'))");
            //    }

            //    alConditions.Add("(" + sbMblnr.ToString() + ")");
            //    alConditions.Add("(" + sbMatnr.ToString() + ")");

            //    switch (varOutType)
            //    {
            //        case "ONLINE":
            //            alConditions.Add("(TRNTP in ('G-','M-','R-'))");
            //            break;

            //        case "TRANSFER":
            //            alConditions.Add("(TRNTP = 'T-')");
            //            break;

            //        case "COMBINE":
            //            alConditions.Add("(TRNTP in ('G-','M-','R-', 'T-'))");
            //            break;

            //        case "ONLINEPROD":
            //            alConditions.Add("(TRNTP in ('G-','M-','R-'))");
            //            break;

            //        case "SPARE_PARTS":
            //            alConditions.Add("(TRNTP in ('G-'))");
            //            break;

            //        case "TWOPHASEOUT":
            //            alConditions.Add("(TRNTP in ('G-','T-'))");
            //            break;
            //        //Add By Michael 20150928 for Film出库
            //        case "Film_OutMaterial":
            //            alConditions.Add("(BWART in ('261','311'))");
            //            alConditions.Add("(TRNTP in ('G-','M-','R-', 'T-'))");
            //            break;

            //    }

            //    sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

            //    try
            //    {
            //        ControlHandleDB();
            //        ControlSqlAccess.TimeOut = 300;//連線SQL Server的時間3分鐘
            //        dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
            //        ControlSqlAccess.CloseConnection();
            //    }
            //    catch (System.Exception ex)
            //    {
            //        ERRMSG = ex.Message + "<- QuerySapLineOutData()";
            //    }
            //    return dtResult;
            //}
            //#endregion


            #region 傳入SI單據號碼查詢連線出庫資料(G-)
            //====================================================================================================
            ////////////Summary by Smose Liao 20100507 ///////////////////////////////////////////////////////////
            /// <summary>
            /// 傳入SI單據號碼查詢連線出庫資料(G-) 
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySinoOutData(aryMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySinoOutData(ArrayList varMblnr)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                StringBuilder sbMblnr = new StringBuilder();

                for (int i = 0; i < varMblnr.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(" or ");

                    sbMblnr.Append("(MBLNR Like '" + varMblnr[i].ToString() + "%')");
                }

                alColumns.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" MTYPE ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" LIFNR ");
                alColumns.Add(" RMANO ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" 0 as ALQTY ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" PRCDE ");
                alColumns.Add(" PUTYP ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" RESLT ");
                alColumns.Add(" BUDAT ");
                alColumns.Add(" PRITY ");
                alColumns.Add(" ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" KDMAT ");
                alColumns.Add(" SERNO ");
                alColumns.Add(" REFID ");

                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                //Where條件先不考慮倉別，待user選定倉別並保存之後再更新回原SI單據
                //alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add("(MENGE>OTQTY)");
                alConditions.Add("(MTYPE = 'SAP_SI')");
                alConditions.Add("(TRNTP in ('G-'))");
                alConditions.Add("(" + sbMblnr.ToString() + ")");

                sbSql.Append(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySinoOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 傳入Send/Group ID查詢SMT/FINAL的資料
            //====================================================================================
            ////////////Summary by Smose Liao 20100202////////////////////////////////////////////
            /// <summary>
            /// 傳入Send / Group ID查詢SMT/FINAL的資料
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySmtData(string varMblnr, string varDate, string varOutType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySmtFinData(string varMblnr, string varOutType)
            {
                DataTable dtResult = new DataTable();
                DataWhfin objWhfin = new DataWhfin(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("*");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(GRPID LIKE '" + varMblnr + "%')");

                switch (varOutType)
                {
                    case "SMT":
                        alConditions.Add("(TLQTY > MENGE)");  //發不足需求量的料號，下一個倉繼續再發
                        sbSql.Append(objWhsmt.EntityGetQuerySql(alColumns, alConditions, false, false));
                        break;

                    case "FINAL":
                        alConditions.Add("MBLNR IS NULL");
                        sbSql.Append(objWhfin.EntityGetQuerySql(alColumns, alConditions, false, false));
                        break;
                }
                alConditions.Add("(AND ROVAL!=0 AND RLQTY!=0)");

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySmtFinData()";
                }

                return dtResult;
            }
            #endregion


            #region 查詢SMT/FINAL的Group ID
            //====================================================================================
            ////////////Summary by Smose Liao 20100202////////////////////////////////////////////
            /// <summary>
            /// 查詢SMT/FINAL的Group ID
            /// </summary> 
            /// <param name="varDate"> ID日期。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <param name="bolAllid">Show all id(True/False)。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryGroupIdData(varDate, varOutType, bolAllid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryGroupIdData(string varDate, string varOutType, bool bolAllid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdData";
                this.ControlMethodParm = "(" + varDate + "," + varOutType + "," + bolAllid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                DataWhfin objWhfin = new DataWhfin(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("GRPID");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(CRDAT BETWEEN '" + varDate + " 00:00:00.000' and '" + varDate + " 23:59:59.999')");

                if (bolAllid == true)
                {
                    alConditions.Add("(MBLNR IS NULL)");
                }

                switch (varOutType)
                {
                    case "SMT":
                        sbSql.Append(objWhsmt.EntityGetQuerySql(alColumns, alConditions, true, false));
                        break;

                    case "FINAL":
                        sbSql.Append(objWhfin.EntityGetQuerySql(alColumns, alConditions, true, false));
                        break;
                }

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdData()";
                }

                return dtResult;
            }
            #endregion


            #region 查詢SMT/FINAL的Group ID(只帶出兩小時內的id)
            //======================================================================================================
            ////////////Summary by Smose Liao 20100202//////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢SMT/FINAL的Group ID
            /// </summary> 
            /// <param name="varDate"> ID日期。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <param name="bolAllid">Show all id(True/False)。</param>
            ///  <param name="dtDateTime">dtDataTable。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryGroupIdData(varDate, varOutType, bolAllid, dtDateTime);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryGroupIdData(string varDate, string varOutType, bool bolAllid, DataTable dtDateTime)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdData";
                this.ControlMethodParm = "(" + varDate + "," + varOutType + "," + bolAllid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                DataWhfin objWhfin = new DataWhfin(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                string strHour = (int.Parse(dtDateTime.Rows[0]["NowHour"].ToString()) - 2 < 0 ? 0 : int.Parse(dtDateTime.Rows[0]["NowHour"].ToString()) - 2).ToString();
                string NowMinute = dtDateTime.Rows[0]["NowMinute"].ToString();
                string strTimeFrom = strHour + ":" + NowMinute + ":00.000";
                string strTimeTo = dtDateTime.Rows[0]["NowHour"].ToString() + ":" + NowMinute + ":50.999";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("GRPID");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(CRDAT BETWEEN '" + varDate + ' ' + strTimeFrom + "' and '" + varDate + ' ' + strTimeTo + "')");

                if (bolAllid == true)
                {
                    alConditions.Add("(MBLNR IS NULL)");
                }

                switch (varOutType)
                {
                    case "SMT":
                        sbSql.Append(objWhsmt.EntityGetQuerySql(alColumns, alConditions, true, false));
                        break;

                    case "FINAL":
                        sbSql.Append(objWhfin.EntityGetQuerySql(alColumns, alConditions, true, false));
                        break;
                }

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdData()";
                }

                return dtResult;
            }
            #endregion


            #region 查詢SMT的Group ID(未扣帳的ID)
            //====================================================================================
            ////////////Summary by Smose Liao 2010110////////////////////////////////////////////
            /// <summary>
            /// 查詢SMT的Group ID(未扣帳的ID)
            /// </summary> 
            /// <param name="varDate"> ID日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryGroupId(varDate);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryGroupId(string varDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupId";
                this.ControlMethodParm = "(" + varDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                DataWhfin objWhfin = new DataWhfin(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("GRPID");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(CRDAT BETWEEN '" + varDate + " 00:00:00.000' and '" + varDate + " 23:59:59.999')");
                alConditions.Add("(MBLNR IS NOT NULL)");
                sbSql.Append(objWhsmt.EntityGetQuerySql(alColumns, alConditions, true, false));

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupId()";
                }

                return dtResult;
            }
            #endregion


            #region 查詢SMT Group ID的倉別是否已扣過帳
            //====================================================================================
            ////////////Summary by Smose Liao 20101110////////////////////////////////////////////
            /// <summary>
            /// 查詢SMT Group ID的倉別是否已扣過帳
            /// </summary> 
            /// <param name="varGrpid"> Group ID。</param>
            /// <param name="varLgort"> 倉別。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryGroupIdStorage(varGrpid, varLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public bool QueryGroupIdStorage(string varGrpid, string varLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdStorage";
                this.ControlMethodParm = "(" + varGrpid + "," + varLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataWhqdp objWhqdp = new DataWhqdp(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                DataTable dtLgort = new DataTable();
                bool bolShowId = true;

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("LGORT");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(GRPID= '" + varGrpid + "')");

                sbSql.Append(objWhqdp.EntityGetQuerySql(alColumns, alConditions, true, false));

                try
                {
                    ControlHandleDB();
                    dtLgort = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtLgort.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLgort.Rows.Count; i++)
                        {
                            if (varLgort == dtLgort.Rows[i]["LGORT"].ToString())
                            {
                                bolShowId = false;
                                break;
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdStorage()";
                }

                return bolShowId;
            }
            #endregion


            #region 傳入ArrayList轉成字串，併用","隔開 by Marc Hong
            //=========================================================================
            ////////////Summary by Marc Hong ////////////////////////////////////////////
            /// <summary>
            ///  傳入ArrayList轉成字串，併用","隔開 by Marc Hong
            /// </summary> 
            /// <param name="varData">傳入 ArrayList。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  string strMblnrs = Array2String(aryMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            private string Array2String(ArrayList varData)
            {
                string strTemp = "";
                for (int i = 0; i < varData.Count; i++)
                {
                    strTemp += ",'" + varData[i] + "'";
                }
                if (strTemp.Length > 0)
                {
                    strTemp = strTemp.Substring(1);
                }
                return strTemp;
            }
            #endregion


            #region  列出SAP出庫的單據資料(G-, M-, T-, R-)
            //=======================================================================================
            ////////////Summary by Smose Liao 20091105///////////////////////////////////////////////
            /// <summary>
            /// 列出SAP出庫的單據資料(G-, M-, T-, R-)
            /// </summary> 
            /// <param name="strMatnr">料號。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strCharg">版本。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapLocationOutData(string strMatnr, string strInsmk, string strCharg);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapLocationOutData(string strMatnr, string strInsmk, string strCharg)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapLocationOutData";
                this.ControlMethodParm = "(" + strMatnr + "," + strInsmk + "," + strCharg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //string strSQL = "Select *, MENGE - OTQTY as BALANCE from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MATNR='" + strMatnr.Trim() + "' and INSMK='" + strInsmk.Trim() + "' and CHARG='" + strCharg.Trim() + "' and MENGE>OTQTY and TRNTP in ('G-','R-','M-','T-')";
                alColumns.Add("*, MENGE - OTQTY as BALANCE");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MATNR='" + strMatnr.Trim() + "'");
                alConditions.Add(" INSMK='" + strInsmk.Trim() + "'");
                alConditions.Add(" CHARG='" + strCharg.Trim() + "'");
                alConditions.Add(" MENGE>OTQTY ");
                alConditions.Add(" TRNTP in ('G-','R-','M-','T-') ");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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


            #region 列出SAP連板入庫的單據資料(G+, T+)
            //============================================================================================================================
            ////////////Summary by Smose Liao 20091105////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出SAP連板入庫的單據資料(G+, T+)
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strCharg">版本(Batch)。</param>
            /// <param name="intQty">數量。</param>
            /// <param name="strZeile">單據Item。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.CheckSapDocumentQty(string strMblnr, string strMatnr, string strInsmk, string strCharg, int intQty, string strZeile);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool CheckSapDocumentQty(string strMblnr, string strMatnr, string strInsmk, string strCharg, int intQty, string strZeile)
            {
                int intTempQty = 0;
                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR='" + strMblnr.Trim() + "'");
                alConditions.Add(" MATNR='" + strMatnr.Trim() + "'");
                alConditions.Add(" INSMK='" + strInsmk.Trim() + "'");
                alConditions.Add(" CHARG='" + strCharg.Trim() + "'");
                alConditions.Add(" TRNTP in ('G-','T-','R-','M-') ");

                if (strZeile.Trim() != "")
                {
                    alConditions.Add(" ZEILE='" + strZeile.Trim() + "'");
                }

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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
                if (dtData.Rows.Count == 0)
                {
                    this.ERRMSG = "The material document of goods issue you input doesn't exist!<- CheckSapDocumentQty()";
                    return false;
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    intTempQty += Int32.Parse(dtData.Rows[i]["MENGE"].ToString()) - Int32.Parse(dtData.Rows[i]["OTQTY"].ToString());
                }
                if (intTempQty == 0)
                {
                    this.ERRMSG = "The balance Qty in this material document is 0!<- CheckSapDocumentQty()";
                    return false;
                }
                if (intTempQty < intQty)
                {
                    this.ERRMSG = "The balance Qty in this material document is less than the Qty you input!<- CheckSapDocumentQty()";
                    return false;
                }
                return true;
            }

            #endregion


            #region  查詢連線出庫資料(G-) by Picasso
            //====================================================================================================
            ////////////Summary by Smose Liao 20091106////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線出庫資料(G-) by Picasso
            /// </summary> 
            /// <param name="aryMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineOutData_Picasso(aryMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineOutData_Picasso(ArrayList aryMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineOutData_Picasso";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strMblnr = Array2String(aryMblnr);
                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                string strSQL = "";
                for (int i = 0; i < aryMblnr.Count; i++)
                {
                    if (i != aryMblnr.Count - 1)
                    {
                        //strSQL += "Select MANDT, WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT,SERNO from WHDWN where MANDT='" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR like '" + aryMblnr[i].ToString() + "%' and MENGE>OTQTY and TRNTP in ('G-','M-','R-') and MTYPE='SAP' union ";
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT, INSPT, BOXID");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR LIKE '" + aryMblnr[i].ToString() + "%" + "'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','M-','R-')");
                        alConditions.Add(" MTYPE='SAP' union  ");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                    else
                    {
                        //strSQL += "Select MANDT, WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT ,INSPT,SERNO from WHDWN where MANDT='" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR like '" + aryMblnr[i].ToString() + "%' and MENGE>OTQTY and TRNTP in ('G-','M-','R-') and MTYPE='SAP' ";
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT, INSPT, BOXID");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR LIKE '" + aryMblnr[i].ToString() + "%" + "'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','M-','R-')");
                        alConditions.Add(" MTYPE='SAP'");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                }

                try
                {
                    // dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 查詢連線出庫資料(G-) by DateCode
            //====================================================================================================
            ////////////Summary by Smose Liao 20091109////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線出庫資料(G-) by DateCode
            /// </summary> 
            /// <param name="aryMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QuerySapLineOutData(aryMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QuerySapLineOutData_DateCode(ArrayList aryMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapLineOutData_DateCode";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                string strMblnr = Array2String(aryMblnr);
                string strSQL = "";

                for (int i = 0; i < aryMblnr.Count; i++)
                {
                    if (i != aryMblnr.Count - 1)
                    {
                        //strSQL += "Select MANDT, WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT from WHDWN where MANDT='" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR like '" + aryMblnr[i].ToString() + "%' and MENGE>OTQTY and TRNTP in ('G-','M-','R-') and MTYPE='SAP' union ";
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR like '" + aryMblnr[i].ToString() + "%'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','M-','R-') ");
                        alConditions.Add(" MTYPE='SAP' union  ");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                    else
                    {
                        //strSQL += "Select MANDT, WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT ,INSPT  from WHDWN where MANDT='" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MBLNR like '" + aryMblnr[i].ToString() + "%' and MENGE>OTQTY and TRNTP in ('G-','M-','R-') and MTYPE='SAP' ";
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR like '" + aryMblnr[i].ToString() + "%'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','M-','R-') ");
                        alConditions.Add(" MTYPE='SAP'  ");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                }

                try
                {
                    //dtData = objWhdwn.EntityQuery(alColumns, alConditions, false,true);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 查詢連線出庫資料(G-) by DateCode(PowerⅡ-QWMS)
            //====================================================================================================
            ////////////Summary by Smose Liao 20101202////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線出庫資料(G-) by DateCode(PowerⅡ-QWMS)
            /// </summary> 
            /// <param name="aryMblnr">單據號碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.Power2LineOutData_DateCode(aryMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable Power2LineOutData_DateCode(ArrayList aryMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "Power2LineOutData_DateCode";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                string strMblnr = Array2String(aryMblnr);
                string strSQL = "";

                for (int i = 0; i < aryMblnr.Count; i++)
                {
                    if (i != aryMblnr.Count - 1)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR like '" + aryMblnr[i].ToString() + "%'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','T-') ");
                        alConditions.Add(" MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS') union  ");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                    else
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        alColumns.Add("MANDT, COMCD,WERKS, LGORT, MBLNR, ZEILE, MATNR, INSMK, CHARG, LIFNR, (MENGE-OTQTY) as MENGE, 0 as ALQTY, EBELN, PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, KDMAT,INSPT");
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" LGORT='" + LGORT + "'");
                        alConditions.Add(" MBLNR like '" + aryMblnr[i].ToString() + "%'");
                        alConditions.Add(" MENGE>OTQTY");
                        alConditions.Add(" TRNTP in ('G-','T-') ");
                        alConditions.Add(" MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS') ");
                        strSQL = strSQL + objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true);
                    }
                }

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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


            #region 查詢加扣的虛擬單據資料
            //=====================================================================================================
            ////////////Summary by Smose Liao 20100223/////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢加扣的虛擬單據資料
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strCrdat">建立日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryAddSimulationDoc(strMblnr, strCrdat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryAddSimulationDoc(string strMblnr, string strCrdat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryAddSimulationDoc";
                this.ControlMethodParm = "(" + strMblnr + "," + strCrdat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("*, 0 as MDQTY");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR like '" + strMblnr + "%' ");
                alConditions.Add(" MTYPE in ('QMS_QS', 'QMS_AS') ");//加扣的單據型態只有SMT
                alConditions.Add("(CRDAT BETWEEN '" + strCrdat + " 00:00:00.000' and '" + strCrdat + " 23:59:59.999')");

                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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


            #region 查詢加扣的虛擬單據資料(PowerⅡ-QWMS)(可傳入多筆單據資料)
            //=====================================================================================================
            ////////////Summary by Smose Liao 20101111/////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢加扣的虛擬單據資料(PowerⅡ-QWMS)(可傳入多筆單據資料)
            /// </summary> 
            /// <param name="varMblnr">單據號碼。</param>
            /// <param name="strCrdat">建立日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryPower2AddDoc(varMblnr, strCrdat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryPower2AddDoc(ArrayList varMblnr, string strCrdat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryPower2AddDoc";
                this.ControlMethodParm = "(" + strCrdat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbMblnr = new StringBuilder();
                string strFrom = "";
                DataTable dtData = new DataTable();
                DataTable dtData1 = new DataTable();


                for (int i = 0; i < varMblnr.Count; i++)
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    if (checkSMTaddWerksData().Rows.Count == 1)
                    {
                        alColumns.Add(" top 1 W.*");
                        alColumns.Add("M.MENGE as MDQTY"); //WHITM 数量重命名MDQTY
                        alColumns.Add("M.INDAT");
                        alColumns.Add("M.DACOD");
                        strFrom = " WHDWN AS W left join WHITM AS M With(Nolock) on W.MANDT=M.MANDT AND W.COMCD=M.COMCD AND W.WERKS=M.WERKS AND W.LGORT=M.LGORT AND W.MATNR=M.MATNR  ";
                    }
                    else
                    {
                        alColumns.Add("W.*");
                        alColumns.Add("0 as MDQTY"); //WHITM 数量重命名MDQTY
                        strFrom = " WHDWN AS W ";
                    }


                    alConditions.Add(" W.MANDT='" + MANDT + "'");
                    alConditions.Add(" W.COMCD='" + COMCD + "'");
                    alConditions.Add(" W.WERKS='" + WERKS + "'");
                    alConditions.Add(" W.LGORT='" + LGORT + "'");
                    alConditions.Add("(W.MBLNR Like '" + varMblnr[i].ToString() + "%')");

                    //alConditions.Add("(" + sbMblnr.ToString() + ")");
                    alConditions.Add(" W.MTYPE in ('QMS_QS', 'QMS_AS') ");//加扣的單據型態只有SMT
                    alConditions.Add("(W.CRDAT BETWEEN '" + strCrdat + " 00:00:00.000' and '" + strCrdat + " 23:59:59.999')");

                    try
                    {                      
                        if (checkSMTaddWerksData().Rows.Count == 1)
                        {
                            dtData1 = ControlQuery(strFrom, alColumns, alConditions, true, "M.INDAT,M.DACOD,W.LGORT");
                        }
                        else
                        {
                           
                            dtData1 = ControlQuery(strFrom, alColumns, alConditions, true, "W.LGORT");
                        }
                       
                        if (dtData.Rows.Count==0)
                        {
                           dtData = dtData1.Clone();
                        }
                        dtData.Merge(dtData1);
                        //string A = ControlGetQuerySql(strFrom, alColumns, alConditions, true);
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

                return dtData;
            }
            #endregion


            #region 寫入註消Reference ID的Remak1(理由)的資料
            //=========================================================================
            ////////////Summary by Smose Liao//////////////////////////////////////////
            /// <summary>
            /// 寫入註消Reference ID的Remak1(理由)的資料
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString, strMandt, strWerks, strLgort);
            /// bool bolReturn = obj.UpdateRefIDRemak1(alRefid, alRemak1);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool UpdateRefIDRemak1(ArrayList alRefid, ArrayList alRemak1)
            {
                bool bolReturn = false;
                try
                {
                    ArrayList arySQL = new ArrayList();
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    ArrayList alConditions = new ArrayList();

                    //WHDWN
                    for (int i = 0; i < alRefid.Count; i++)
                    {
                        objWhdwn.ResetField();
                        objWhdwn.Remak1 = alRemak1[i].ToString();
                        alConditions.Clear();
                        alConditions.Add("(REFID= '" + alRefid[i].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                    }

                    bolReturn = ControlExeSqlStringArr(arySQL);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- UpdateRefIDRemak1()";
                }

                return bolReturn;
            }


            #endregion


            #region 查詢未扣帳SendID
            ////////////Summary by Ryan Tsai (20131212)//////////////////////////////////
            /// <summary>
            /// 查詢SenID資料
            /// </summary> 
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QuerySendID(string strWERKS, string strLGORT, string strBWART, string strBUDAT)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySendID";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append(" select distinct REFID from WHDWN with(nolock) ");
                    sbSql.Append(" where OTQTY=0 and MTYPE='SAP' ");

                    if (strBWART != "")
                    {
                        sbSql.AppendFormat(" and BWART='{0}'", strBWART);
                    }
                    if (strWERKS != "")
                    {
                        sbSql.AppendFormat(" and WERKS='{0}'", strWERKS);
                    }
                    if (strLGORT != "")
                    {
                        sbSql.AppendFormat(" and LGORT='{0}'", strLGORT);
                    }
                    if (strBUDAT != "")
                    {
                        sbSql.AppendFormat(" and BUDAT='{0}' ", strBUDAT);
                    }
                    else
                    {
                        sbSql.AppendFormat(" and BUDAT=CONVERT(varchar(8),GETDATE(),112) ", strBUDAT);
                    }
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


            #region 查詢是否需走調撥流程
            //==========================================================================================================
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢是否需走調撥流程
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryIsAllocateProcess();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryIsAllocateProcess(string strWerks, string strModel, string strCharg, string strRegion)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryIsAllocateProcess";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append(" select * from WHCTRL with(nolock) ");
                    sbSql.Append(" where CTRLID='ALLOCATE' ");

                    if (strWerks != "")
                    {
                        sbSql.AppendFormat(" and CTRLNM='{0}'", strWerks);
                    }
                    if (strModel != "")
                    {
                        sbSql.AppendFormat(" and CTRLC1='{0}'", strModel);
                    }
                    if (strCharg != "")
                    {
                        sbSql.AppendFormat(" and CTRLC2='{0}'", strCharg);
                    }
                    if (strRegion != "")
                    {
                        sbSql.AppendFormat(" and CTRLC3='{0}'", strRegion);
                    }
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


            #region 半成品入库需求 by brian

            #region 查詢連線入庫資料(G+)by Box ID

            public DataTable QueryQMSLineInDataByBoxID(string strLocat, string strBoxID, string strMatnr, string strIndat, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataBySerno";
                this.ControlMethodParm = "(" + strLocat + "," + strBoxID + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                //sbSql.AppendLine("SELECT TOP (1) MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                //    .AppendLine("AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR,")
                //    .AppendLine("'' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT, WO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY")
                //    .AppendLine("FROM WHDWN WITH (nolock)")
                //    .AppendLine("WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+')) AND (MTYPE = 'QMS')")
                //    .AppendLine("AND (BOXID = '" + strBoxID + "')");

                sbSql.AppendLine("SELECT TOP (1) MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                    .AppendLine("AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR,")
                    .AppendLine("'' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT,'' AS WO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY")
                    .AppendLine("FROM WHDWN WITH (nolock)")
                    .AppendLine("WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+')) AND (MTYPE = 'QMS')")
                    .AppendLine("AND (BOXID = '" + strBoxID + "')");

                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }
                //sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT,  LOADID, MODEL, REGION,WO");
                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT,  LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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


            public DataTable QueryQMSLineInDataByBoxID_ALL(string strLocat, string strBoxID, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByBoxID";
                this.ControlMethodParm = "(" + strLocat + "," + strBoxID + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();


                //sbSql.AppendLine("SELECT DISTINCT")
                //    .AppendLine("MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                //    .AppendLine("AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                //    .AppendLine("'' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT,  WO, '' AS SERNO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY")
                //    .AppendLine("FROM WHDWN WITH (nolock)")
                //    .AppendLine("WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+'))")
                //    .AppendLine("AND (MTYPE = 'QMS') AND (MBLNR =")
                //    .AppendLine("(SELECT TOP (1) MBLNR")
                //    .AppendLine("FROM WHDWN WITH (nolock)")
                //    .AppendLine("WHERE (BOXID = '" + strBoxID + "') AND (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MTYPE = 'QMS')")
                //    .AppendLine("ORDER BY CRDAT DESC))");


                sbSql.AppendLine("SELECT DISTINCT")
                    .AppendLine("MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                    .AppendLine("AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                    .AppendLine("'' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT,'' AS  WO, '' AS SERNO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY")
                    .AppendLine("FROM WHDWN WITH (nolock)")
                    .AppendLine("WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+'))")
                    .AppendLine("AND (MTYPE = 'QMS') AND (MBLNR =")
                    .AppendLine("(SELECT TOP (1) MBLNR")
                    .AppendLine("FROM WHDWN WITH (nolock)")
                    .AppendLine("WHERE (BOXID = '" + strBoxID + "') AND (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MTYPE = 'QMS')")
                    .AppendLine("ORDER BY CRDAT DESC))");


                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }
                //sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT, LOADID, MODEL, REGION,WO");
                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT, LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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

            //public DataTable QueryQMSLineInDataByBoxID_ALLNEW(string strLocat, string strBoxID, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            public DataTable QueryQMSLineInDataByBoxID_ALLNEW(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByBoxID";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine(" SELECT ")
                    .AppendLine(" MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                    .AppendLine(" AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                    .AppendLine(" '' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT, '' AS WO, '' AS SERNO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY, MBLNR AS ORGMBLNR")
                    .AppendLine(" FROM WHDWN WITH (nolock)")
                    .AppendLine(" WHERE (MANDT = '" + MANDT + "') AND (COMCD='" + COMCD + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+'))")
                    .AppendLine(" AND (MTYPE in( 'QMS','QMS_311')) AND (MBLNR ='" + strMblnr + "') ");


                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }

                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT, LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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

            #endregion

            #region 列出半成品出庫確認的Box ID與庫存的資料
            //=====================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出半成品出庫確認的Box ID與庫存的資料
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDStorageData(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetBoxIDStorageData(string varBoxid)
            {

                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                //sbSql.Append("Select B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.MENGE AS ALQTY, I.MBLNR, I.LOCAT, I.CHARG, I.INSMK, I.MENGE ");
                //sbSql.Append("from WHBOX B inner join WHITM I ");
                //sbSql.Append("ON B.MANDT = I.MANDT AND B.WERKS = I.WERKS AND B.LGORT = I.LGORT ");
                //sbSql.Append("AND B.MATNR = I.MATNR AND B.LOCAT = I.LOCAT AND B.MATNR = I.MATNR AND B.MBLNR = I.MBLNR ");
                //sbSql.Append("where 1=1 ");

                sbSql.Append(" SELECT DISTINCT B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.MENGE AS ALQTY, I.MBLNR, I.LOCAT, I.CHARG, I.INSMK, I.MENGE ,B.SERNO ");
                sbSql.Append(" FROM WHBOX B WITH (NOLOCK) INNER JOIN WHITM I WITH (NOLOCK) ");
                sbSql.Append(" ON B.MANDT = I.MANDT AND B.COMCD = I.COMCD  AND B.WERKS = I.WERKS AND B.LGORT = I.LGORT AND B.LOCAT = I.LOCAT ");
                sbSql.Append(" AND B.MATNR = I.MATNR AND B.MBLNR = I.MBLNR AND B.CHARG=I.CHARG AND B.INSMK=I.INSMK ");
                sbSql.Append(" WHERE 1=1 ");

                if (!string.IsNullOrEmpty(varBoxid))
                {
                    sbSql.Append("AND B.BOXID ='" + varBoxid.Trim() + "' ");
                }
                sbSql.Append("AND (B.DELFLG <> 'Y' or B.DELFLG IS NULL) ");

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                return dtData;
            }
            #endregion

            #region 列出半成品出庫確認的Box ID與庫存的資料
            //=====================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出半成品出庫確認的Box ID與庫存的資料
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDStorageData(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetStorageDataByBoxID(string varBoxid)
            {

                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine("SELECT DISTINCT")
                    .AppendLine("B.MANDT, B.COMCD, B.WERKS, B.LGORT, '' AS MBLNR, B.BOXID, '' AS SERNO, B.MATNR, B.LOCAT, B.CHARG,")
                    .AppendLine("B.INSMK, SUM(B.MENGE) AS ALQTY, '' AS INDAT, B.MBLNR AS OMBLNR, 'Y' AS FLAGE")
                    .AppendLine("FROM WHBOX AS B WITH (NOLOCK) INNER JOIN")
                    .AppendLine("(SELECT DISTINCT MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,CHARG,INSMK FROM WHITM WITH(NOLOCK)) AS I ON B.MANDT = I.MANDT AND B.COMCD = I.COMCD AND B.WERKS = I.WERKS AND")
                    .AppendLine("B.LGORT = I.LGORT AND B.LOCAT = I.LOCAT AND B.MATNR = I.MATNR AND")
                    .AppendLine("B.CHARG = I.CHARG AND B.INSMK = I.INSMK")
                    .AppendLine(string.Format("WHERE (B.MANDT = '{0}') AND (B.COMCD = '{1}') AND (B.WERKS = '{2}') AND (B.LGORT = '{3}')", MANDT, COMCD, WERKS, LGORT));

                if (!string.IsNullOrEmpty(varBoxid))
                {
                    sbSql.Append(string.Format("AND (B.BOXID ='{0}')", varBoxid));
                }

                sbSql.AppendLine("AND (B.DELFLG <> 'Y' or B.DELFLG IS NULL)");

                sbSql.AppendLine("GROUP BY B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.LOCAT, B.CHARG, B.INSMK");

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                return dtData;
            }
            #endregion


            #region 列出半成品出庫確認的Box ID與庫存的資料
            //=====================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出半成品出庫確認的Box ID與庫存的資料
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDStorageData(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetBoxIDStorageDataBySn(string varSN)
            {

                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("  Select DISTINCT  B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, H.BOXID, B.MATNR, H.MENGE AS ALQTY, I.MBLNR, I.LOCAT, I.CHARG, I.INSMK, I.MENGE  ,B.SERNO");
                sbSql.Append("  from WHDWN as H WITH (NOLOCK) inner JOIN  WHBOX B WITH (NOLOCK) ON H.MBLNR=B.MBLNR AND H.BOXID=B.BOXID inner join WHITM I ");
                sbSql.Append(" ON B.MANDT = I.MANDT AND B.COMCD = I.COMCD AND B.WERKS = I.WERKS AND B.LGORT = I.LGORT AND B.LOCAT = I.LOCAT AND B.CHARG=I.CHARG AND B.INSMK=I.INSMK AND H.SERNO=B.SERNO ");
                sbSql.Append(" AND B.MATNR = I.MATNR AND B.MBLNR = I.MBLNR ");
                sbSql.Append(" WHERE isnull(H.IOFLAGE,'')='Y' AND H.MTYPE='QMS' ");
                if (!string.IsNullOrEmpty(varSN))
                {
                    sbSql.Append("and H.SERNO ='" + varSN.Trim() + "'");
                }
                sbSql.Append("and (B.DELFLG <> 'Y' or B.DELFLG IS NULL) ");
                //B.LOCAT = I.LOCAT AND
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                return dtData;
            }
            #endregion


            #region 列出半成品出庫確認的Box ID與庫存的資料
            //=====================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 列出半成品出庫確認的Box ID與庫存的資料
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDStorageData(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetStorageDataBySn(string varSN)
            {

                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine("SELECT DISTINCT")
                    .AppendLine("B.MANDT, B.COMCD, B.WERKS, B.LGORT,  '' AS MBLNR, B.BOXID, B.SERNO, B.MATNR, B.LOCAT, B.CHARG,")
                    .AppendLine("B.INSMK, B.MENGE AS ALQTY, '' AS INDAT, B.MBLNR AS OMBLNR, 'N' AS FLAGE")
                    .AppendLine("FROM WHBOX AS B WITH (NOLOCK) INNER JOIN")
                    .AppendLine("WHITM AS I WITH (NOLOCK) ON B.MANDT = I.MANDT AND B.COMCD = I.COMCD AND B.WERKS = I.WERKS AND")
                    .AppendLine("B.LGORT = I.LGORT AND B.LOCAT = I.LOCAT AND B.MATNR = I.MATNR AND")
                    .AppendLine("B.CHARG = I.CHARG AND B.INSMK = I.INSMK")
                    .AppendLine(string.Format("WHERE (B.MANDT = '{0}') AND (B.COMCD = '{1}') AND (B.WERKS = '{2}') AND (B.LGORT = '{3}')", MANDT, COMCD, WERKS, LGORT));

                if (!string.IsNullOrEmpty(varSN))
                {
                    sbSql.Append(string.Format("AND (B.SERNO ='{0}')", varSN));
                }

                sbSql.AppendLine("AND (B.DELFLG <> 'Y' or B.DELFLG IS NULL)");

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                return dtData;
            }
            #endregion


            #region 检查库存是否有超出5天
            //=====================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 检查库存是否有超出5天
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDStorageData(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetBoxIDStorageDataAlter(string Locat, string Matnr)
            {


                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("SELECT * FROM WHITM WITH(NOLOCK) WHERE WERKS+LOCAT NOT IN(SELECT WERKS+LOCAT FROM WHHED WITH(NOLOCK) where LGORT='TW61' ) AND DATEDIFF(DAY,CRDAT,GETDATE())>5 AND MATNR IN(SELECT CTRLNM FROM WHCTRL WITH(NOLOCK) WHERE MANDT='218' AND SOLDTO='QWMS'  AND CTRLID='MAT' AND COMCD='" + COMCD + "' ) ");

                sbSql.AppendFormat("  and locat='{0}' and matnr='{1}'", Locat, Matnr);


                //ControlHandleDB();
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                return dtData;
            }
            #endregion

            #region 查詢該票BOX ID是否含有HOLD狀態的SN
            //===================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢該票BOX ID是否含有HOLD狀態的SN
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// Bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDHoldStatus(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool GetBoxIDHoldStatus(string varBoxid)
            {


                bool bolReturn = false;
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" Select  B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.MENGE AS ALQTY, B.SERNO, H.SERNO  from WHBOX B with(nolock) inner join WHHOLD H with(nolock) ON B.SERNO = H.SERNO where 1=1  ");
                // sbSql.Append("Select B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.MENGE AS ALQTY, B.SERNO, H.SERNO  from WHBOX B  INNER JOIN WHDWN AS K ON B.BOXID=K.BOXID   inner join WHHOLD H ON K.SERNO = H.SERNO where 1=1  ");
                //sbSql.Append("from WHBOX B inner join WHHOLD H ");
                //sbSql.Append("ON B.SERNO = H.SERNO ");
                //sbSql.Append("where 1=1 ");
                if (!string.IsNullOrEmpty(varBoxid))
                {
                    sbSql.Append("and B.BOXID ='" + varBoxid.Trim() + "'");
                }

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                if (dtData.Rows.Count > 0)
                {
                    bolReturn = true;
                }

                return bolReturn;
            }
            #endregion


            #region 查詢該票BOX ID是否含有HOLD狀態的SN
            //===================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢該票BOX ID是否含有HOLD狀態的SN
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// Bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDHoldStatus(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool GetBoxIDHoldStatusBySn(string varSN)
            {


                bool bolReturn = false;
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select  B.MANDT, B.COMCD, B.WERKS, B.LGORT, B.MBLNR, B.BOXID, B.MATNR, B.MENGE AS ALQTY, B.SERNO, H.SERNO  from WHBOX B with(nolock)  inner join WHHOLD H with(nolock) ON B.SERNO = H.SERNO where 1=1  ");

                if (!string.IsNullOrEmpty(varSN))
                {
                    sbSql.Append("and B.SERNO ='" + varSN.Trim() + "'");
                }

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                if (dtData.Rows.Count > 0)
                {
                    bolReturn = true;
                }

                return bolReturn;
            }
            #endregion

            #region 查詢該票BOX ID是否含有SN出库
            //===================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢該票BOX ID是否含有SN出库
            /// </summary> 
            /// <param name="varMblnr">BOX ID。</param>
            /// <returns>
            /// Bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.GetBoxIDHoldStatus(varBoxid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool GetCheckOutBySN(string varBoxid)
            {


                bool bolReturn = false;
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendFormat("SELECT * FROM WHDWN  with(nolock) WHERE IOFLAGE='T' AND BOXID='{0}' ", varBoxid.Trim());

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                if (dtData.Rows.Count > 0)
                {
                    bolReturn = true;
                }

                return bolReturn;
            }
            #endregion

            public DataTable QueryOfflineData(string strWERKS, string strLGORT)
            {
                DataTable dt = new DataTable();
                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendLine("Select TOP 1  MANDT  ,  COMCD  ,  WERKS  ,  LGORT  ,  MTYPE  ,  MBLNR  ,  ZEILE  ,  MATNR  ,  INSMK  ,  CHARG  ,  LIFNR  ,  RMANO  ,  '' MENGE  , '' as ALQTY  ,  EBELN  ,  PRCDE  ,  PUTYP  ,  KOSTL  ,  RESLT  ,  BUDAT  ,  PRITY  ,  ARBPL  ,  TRNTP  ,  KDMAT  ,  SERNO  ,  REFID  ,  INTID  ");
                    sbSql.AppendLine(" from WHDWN With (NoLock) Where (MANDT='218') And (COMCD='" + COMCD + "') And (WERKS= '" + strWERKS + "') And (LGORT= '" + strLGORT + "') AND 1=0");
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return dt;

            }

            #region Add By Michael 20150602 for 321，350入库
            public DataTable ListSapInData_NEW(string strMblnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_NEW";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select * , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+') ", "");
                sbSql.Append(" AND BWART IN ('321','350') ");

                if (strMblnr != "")
                {
                    sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                DataTable dtData = new DataTable();
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

            #region Add By Michael 20150915 for 261,311退库
            public DataTable ListSapReturnFilm(string strMblnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapReturnFilm";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select * , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+','T+') ", "");
                sbSql.Append(" AND BWART IN ('261','311') ");

                if (strMblnr != "")
                {
                    sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                DataTable dtData = new DataTable();
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

            #region Add By Michael 20150928 for 261,311出库

            public DataTable ListSapOutFilm(string varMblnr, string varCrdat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapOutFilm";
                this.ControlMethodParm = "(" + varMblnr + "," + varCrdat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                DataTable dtTmp = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strMblnr = "";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" * ");
                alColumns.Add(" MENGE - OTQTY as BALANCE ");

                alConditions.Add("(MANDT= '" + MANDT + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + WERKS + "')");
                alConditions.Add("(LGORT= '" + LGORT + "')");
                alConditions.Add(" MENGE> OTQTY  ");
                alConditions.Add(" MTYPE='SAP' ");
                alConditions.Add(" TRNTP IN ('G-','R-','M-','T-') ");
                alConditions.Add(" BWART IN ('261','311') ");

                if (varMblnr.Trim() != "")
                {
                    alConditions.Add("(MBLNR like '" + varMblnr.Trim() + "%')");
                }

                if (varCrdat.Trim() != "")
                {
                    alConditions.Add("(CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtResult = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #endregion

            #region 获取DocNo的数据
            /// <summary>
            /// 获取DocNo的数据
            /// </summary>
            /// <param name="strDate">日期(格式：20160517)</param>
            /// <remarks>add By Mike Deng 20160513</remarks>
            /// <returns></returns>
            public DataTable GetDocNoData(string strDate)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDocNoData";
                this.ControlMethodParm = "(" + strDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select distinct substring(MBLNR,1,10) AS DocNo,MBLNR,MATNR, (Menge - Otqty) as BALANCE, CRDAT from WHDWN with (nolock) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY ");
                sbSql.AppendFormat("and TRNTP in ('G+','T+','R+','M+') ");
                if (!string.IsNullOrEmpty(strDate))
                {
                    sbSql.AppendFormat(" AND CONVERT(varchar(8),CRDAT,112)='{0}'", strDate);
                }
                else
                {
                    sbSql.Append(" AND CONVERT(varchar(8),CRDAT,112)>CONVERT(varchar(8),dateadd(day,-2,getdate()),112) ");
                }
                sbSql.Append("order by CRDAT desc ");

                DataTable dtData = new DataTable();
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

            #region 转仓入库(批量)查询入库资料
            /// <summary>
            /// 转仓入库(批量)查询入库资料
            /// </summary>
            /// <param name="strLocat">储位</param>
            /// <param name="strMblnr">扣账编号</param>
            /// <param name="strIndat">入库日期</param>
            /// <remarks>Add by Mike Deng 20160516</remarks>
            /// <returns></returns>
            public DataTable QuerySapCombineInDataBatch(string strLocat, string strMblnr, string strIndat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySapCombineInDataBatch";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strIndat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add(" MANDT ");
                alColumns.Add(" COMCD ");
                alColumns.Add(" WERKS ");
                alColumns.Add(" LGORT ");
                alColumns.Add(" '" + strLocat + "' as LOCAT ");
                alColumns.Add(" MATNR ");
                alColumns.Add(" INSMK ");
                alColumns.Add(" CHARG ");
                alColumns.Add(" (MENGE-OTQTY) as MENGE ");
                alColumns.Add(" (MENGE-OTQTY) as ALQTY ");
                alColumns.Add(" MBLNR ");
                alColumns.Add(" ZEILE ");
                alColumns.Add(" '' as OMBLNR ");
                alColumns.Add(" '' as MRGID ");
                alColumns.Add(" KOSTL ");
                alColumns.Add(" ARBPL as ARBPL ");
                alColumns.Add(" TRNTP ");
                alColumns.Add(" EBELN ");
                alColumns.Add(" LIFNR ");
                alColumns.Add("  '' as RMAK1 ");
                alColumns.Add(" '" + strIndat + "' as INDAT ");
                alColumns.Add(" ISNULL(SERNO,'') AS SERNO ");
                alColumns.Add(" INSPT ");
                alColumns.Add(" ISNULL(DACOD,'') AS DACOD ");
                alColumns.Add(" ISNULL(GRLOC,'') AS GRLOC ");
                alColumns.Add(" ISNULL(RMANO,'') AS RMANO ");
                alColumns.Add(" ISNULL(KDMAT,'') AS KDMAT ");

                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + WERKS + "'");
                alConditions.Add(" LGORT='" + LGORT + "'");
                alConditions.Add(" MBLNR in ('" + strMblnr + "')");
                alConditions.Add(" MENGE>OTQTY");
                alConditions.Add(" TRNTP in ('G+','T+','R+','M+')  ");
                alConditions.Add("MTYPE = 'SAP' ");

                DataTable dtData = new DataTable();
                try
                {
                    string strsql = objWhdwn.ControlGetQuerySql("whdwn", alColumns, alConditions, false);
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region 查询栈板号下的所有待入库的BOXID信息
            /// <summary>
            /// 查询栈板号下的所有待入库的BOXID信息
            /// </summary>
            /// <param name="strLocat"></param>
            /// <param name="strMblnr"></param>
            /// <param name="strMatnr"></param>
            /// <param name="strIndat"></param>
            /// <param name="strInsmk"></param>
            /// <param name="strLogrt"></param>
            /// <returns></returns>
            public DataTable QueryBoxIDInfoByPalletID(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryBoxIDInfoByPalletID";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ")
                         .AppendLine(" MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                         .AppendLine(" AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                         .AppendLine(" '' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT, '' AS WO, '' AS SERNO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY, MBLNR AS ORGMBLNR")
                         .AppendLine(" FROM WHDWN WITH (nolock)")
                         .AppendLine(" WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+'))")
                         .AppendLine(" AND (MTYPE in( 'QMS','QMS_311')) AND (MBLNR ='" + strMblnr + "') ");

                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }

                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT, LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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

            #region  查詢連線入庫資料(G+)by BOX ID
            //==========================================================================================
            ////////////Summary by Smose Liao 20091023//////////////////////////////////////////////////
            /// <summary>
            /// 查詢連線入庫資料(G+)by BOX ID
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strPalid">PalletID。</param>
            /// <param name="strIndat">入庫時間。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.QueryQMSLineInDataByPalid(strLocat, strPalid, strIndat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQMSLineInDataByBoxid(string strLocat, string strBoxid, string strIndat, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByBoxid";
                this.ControlMethodParm = "(" + strLocat + "," + strBoxid + "," + strIndat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                if (strType == "Integer")//整板
                {
                    alColumns.Add("MANDT,COMCD, WERKS, LGORT,'" + strLocat +
                                  "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL,  ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" +
                                  strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, PKDAT, BOXID ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + WERKS + "'");
                    alConditions.Add(" MENGE>OTQTY  ");
                    alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS')");
                    alConditions.Add(" MBLNR=(SELECT MBLNR FROM WHDWN WHERE BOXID='" + strBoxid + "' AND WERKS='" + WERKS + "' AND MENGE>OTQTY AND MTYPE='QMS_M' or MTYPE='QMS')");
                }
                if (strType == "Scattered")//零板
                {
                    alColumns.Add("MANDT,COMCD, WERKS, LGORT,'" + strLocat +
                                  "' as LOCAT,  MATNR, INSMK, CHARG, MENGE, OTQTY, 0 as ALQTY, MBLNR, ZEILE, '' as OMBLNR,'' as MRGID, KOSTL,  ARBPL, TRNTP, EBELN, LIFNR, '' as RMAK1, '" +
                                  strIndat + "' as INDAT ,KDMAT ,ISNULL(SERNO,'') AS SERNO, PKDAT, BOXID  ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + WERKS + "'");
                    alConditions.Add(" MENGE>OTQTY  ");
                    alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS')");
                    alConditions.Add(" BOXID='" + strBoxid + "'");
                }

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region 查询IQC借料资料
            //=========================================================================
            ////////////Summary by Freeman 20170522/////////////////////////////////
            /// <summary>
            /// 查询IQC借料资料(BWART='101' and '503')
            /// </summary> 
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strLocat">储位。</param>
            /// <param name="strInsmk">厂商代码。</param>
            /// <param name="strDate">日期。</param>
            /// <param name="strDate">状态。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objSapData.ListSapInData_IQCBorrow(string strMblnr,string strLocat,string strLifnr,string strDate, string strStatus);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable ListSapInData_IQCBorrow(string strMblnr, string strLocat, string strLifnr, string strInDate, string strToDate, string strStatus, string strMatnr, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_IQCBorrow";
                this.ControlMethodParm = "(" + strMblnr + "," + strLocat + "," + strLifnr + "," + strInDate + "," + strStatus + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();

                //string strSQL = "SSELECT A.MBLNR,A.BWART,A.LIFNR,A.WERKS,A.LGORT,A.GRLOC,A.MATNR,A.MENGE,isnull(B.BRQTY,'0')as BRQTY,isnull(B.RTQTY,'0') as RTQTY,isnull(B.IQCID,'') as IQCID,isnull(B.WHID,'') as WHID,A.BUDAT,A.CRDAT,B.Remark as REMAK
                // FROM WHDWN A LEFT JOIN WHBRM B ON A.MBLNR=B.MBLNR where A.MANDT= '" + MANDT + "' and A.WERKS= '" + WERKS + "' and A.LGORT= '" + LGORT + "' and A.MENGE> A.OTQTY  and MTYPE='SAP' and A.BWART in ('101', '503')";

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ")
                         .AppendLine(" A.MBLNR,A.BWART,A.LIFNR,A.WERKS,A.LGORT,A.GRLOC,A.MATNR,A.MENGE,isnull(B.BRQTY,'0')as BRQTY,isnull(B.RTQTY,'0') as RTQTY,isnull(B.IQCID,'') as IQCID,isnull(B.WHID,'') as WHID,isnull(B.RTIQCID,'') as RTIQCID,isnull(B.RTWHID,'') as RTWHID,A.BUDAT,A.CRDAT,B.CRDAT AS BRDAT,B.RTDAT,B.Remark as REMAK")
                         .AppendLine(" FROM WHDWN A LEFT JOIN WHBRM B ON A.MBLNR=B.MBLNR")
                         .AppendLine(" WHERE (A.MANDT = '" + MANDT + "') AND (A.WERKS = '" + WERKS + "')  AND (A.MENGE > A.OTQTY) ")
                         .AppendLine(" AND (A.MTYPE='SAP') AND (A.BWART in ('101', '503', '511')) ");

                if (!string.IsNullOrEmpty(LGORT))
                {
                    sbSql.AppendLine("AND A.LGORT='" + LGORT + "'");
                }
                if (!string.IsNullOrEmpty(strMblnr))
                {
                    sbSql.AppendLine("AND A.MBLNR= '" + strMblnr + "'");
                }
                if (!string.IsNullOrEmpty(strLocat))
                {
                    sbSql.AppendLine("AND A.GRLOC= '" + strLocat + "'");
                }
                if (!string.IsNullOrEmpty(strLifnr))
                {
                    sbSql.AppendLine("AND A.LIFNR= '" + strLifnr + "'");
                }
                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND A.MATNR='" + strMatnr + "'");
                }
                if (!string.IsNullOrEmpty(strInDate))
                {
                    if (strType=="Query")
                    {
                        sbSql.AppendLine("AND B.CRDAT BETWEEN '" + strInDate + " 00:00:00.000' AND '" + strToDate + " 23:59:59.999'");
                }
                    else
                    {
                        sbSql.AppendLine("AND A.BUDAT BETWEEN '" + strInDate + "' AND '" + strToDate + "'");
                    }
                }
                if (strType == "Query" &&  strStatus == "All")
                {
                    sbSql.AppendLine("AND B.BRQTY IS NOT NULL AND B.RTQTY IS NOT NULL");
                }
                if (strStatus == "Y")
                {
                    sbSql.AppendLine("AND B.BRQTY=0");
                }
                if (strStatus == "N")
                {
                    sbSql.AppendLine("AND B.BRQTY>0");
                }
                sbSql.AppendLine("AND (B.Remark IS NULL OR B.Remark='')");

                sbSql.AppendLine("ORDER BY A.BUDAT,A.CRDAT DESC");

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

            #region 查询IQC借还料记录
            public DataTable Query_IQCBorrowData(string strWerks, string strLgort, string strMblnr, string strLocat, string strMatnr, string strLifnr, string strRemark)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInData_IQCBorrow";
                this.ControlMethodParm = "(" + strMblnr + "," + strLocat + "," + strLifnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();

                StringBuilder sbSql = new StringBuilder();
                if (strRemark == "borrow")
                {
                    sbSql.AppendLine("SELECT MBLNR,WERKS,LGORT,LOCAT,MATNR,MENGE,BRQTY,CRDAT,IQCID,WHID,LIFNR,Remark FROM WHBRM WITH(NOLOCK) ");
                }
                if (strRemark == "return")
                {
                    sbSql.AppendLine("SELECT MBLNR,WERKS,LGORT,LOCAT,MATNR,MENGE,RTQTY,RTDAT,RTIQCID,RTWHID,LIFNR,Remark FROM WHBRM WITH(NOLOCK) ");
                }
                sbSql.AppendLine("WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND MATNR='" + strMatnr + "' AND LIFNR='" + strLifnr + "'");
                sbSql.AppendLine("AND MBLNR='"+strMblnr+"' AND Remark='" + strRemark + "'");
                if (strRemark == "borrow")
                {
                    sbSql.AppendLine(" ORDER BY CRDAT DESC ");
                }
                if (strRemark == "return")
                {
                    sbSql.AppendLine(" ORDER BY RTDAT DESC ");
                }
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

            #region Add By Michael 20160229 for BP
            public DataTable QueryQMSLineInDataByBoxID_BP(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByBoxID_BP";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ")
                     .AppendLine(" MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                     .AppendLine(" AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                     .AppendLine(" '' AS RMAK1, '" + strIndat + "' AS INDAT, KDMAT, '' AS WO, '' AS SERNO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY, MBLNR AS ORGMBLNR")
                     .AppendLine(" FROM WHDWN WITH (nolock)")
                     .AppendLine(" WHERE (MANDT = '" + MANDT + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+', 'T+'))")
                     .AppendLine(" AND (MTYPE in( 'QMS_262','QMS_311')) AND (MBLNR ='" + strMblnr + "') ");

                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }

                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, KDMAT, LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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

            #region  GB
            public DataTable GB_QueryQMSLineInDataByBoxID_ALLNEW(string strLocat, string strMblnr, string strMatnr, string strIndat, string strInsmk, string strLogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryQMSLineInDataByBoxID";
                //this.ControlMethodParm = "(" + strLocat + "," + strBoxID + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                this.ControlMethodParm = "(" + strLocat + "," + strMblnr + "," + strMatnr + "," + strIndat + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ")
                         .AppendLine(" MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, SUM(MENGE - OTQTY)")
                         .AppendLine(" AS MENGE, 0 AS ALQTY, MBLNR, BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO,")
                         .AppendLine(" '' AS RMAK1, '" + strIndat + "' AS INDAT, DACOD, KDMAT, '' AS WO, LOADID, MODEL, REGION, SUM(PALQTY) AS PALQTY, MBLNR AS ORGMBLNR")
                         .AppendLine(" FROM WHDWN WITH (nolock)")
                         .AppendLine(" WHERE (MANDT = '" + MANDT + "') AND (COMCD='" + COMCD + "') AND (WERKS = '" + WERKS + "') AND (LGORT = '" + strLogrt + "') AND (MENGE > OTQTY) AND (TRNTP IN ('G+', 'M+', 'R+'))")
                         .AppendLine(" AND (MTYPE in( 'QMS','QMS_311')) AND (MBLNR ='" + strMblnr + "') ");

                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbSql.AppendLine("AND MATNR= '" + strMatnr + "'");
                }

                if (!string.IsNullOrEmpty(strInsmk))
                {
                    sbSql.AppendLine("AND INSMK= '" + strInsmk + "'");
                }

                sbSql.AppendLine("GROUP BY MANDT, COMCD, WERKS, LGORT, MATNR, INSMK, CHARG, MBLNR, BOXID, KOSTL, ARBPL, TRNTP, EBELN, LIFNR, RMANO, DACOD, KDMAT, LOADID, MODEL, REGION");

                DataTable dtData = new DataTable();
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

            #region 联机出库带出单据号串底账序号

            public DataTable QuerySapLineOutData(ArrayList varMblnr, string varOutType, ArrayList varMatnr)
            {
                DataTable dtResult = new DataTable();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();

                StringBuilder sbMblnr = new StringBuilder();
                StringBuilder sbMatnr = new StringBuilder();
                string strMatnr = "";
                string strMblnr = "";

                sbSql.AppendLine(" Select  MANDT,D.COMCD,WERKS,LGORT,MTYPE,MBLNR,ZEILE,D.MATNR, M.DECITEM , INSMK,CHARG,LIFNR,RMANO, (MENGE-OTQTY) as MENGE,0 as ALQTY,"
                    + " EBELN,PRCDE,PUTYP,IIF(BWART='311' and TRNTP='T-',KOSTL,UMLGO) KOSTL,UMLGO,RESLT,BUDAT,PRITY,ARBPL,TRNTP,KDMAT,SERNO,REFID,INTID  from WHDWN D With (NoLock)     ");
                sbSql.AppendLine("  LEFT JOIN MATDIC AS M ON D.COMCD=M.COMCD AND  D.MATNR=M.MATNR   ");
                sbSql.AppendLine("  WHERE MANDT='" + MANDT + "' And D.COMCD='" + COMCD + "' And WERKS= '" + WERKS + "'  And LGORT= '" + LGORT + "' And MENGE>OTQTY  ");

                if (varOutType == "ONLINEPROD")
                {
                    sbSql.AppendFormat("  AND  MTYPE='SDS' ");
                }
                else if (varOutType == "SPARE_PARTS")
                {
                    sbSql.AppendFormat("  AND  MTYPE='SAP_SI' ");
                }
                else if (varOutType == "TWOPHASEOUT")
                {
                    //两阶段出库单据
                    sbSql.AppendFormat(" AND MTYPE IN ('SAP','QMS_SQ','QMS_SA','QMS_FQ','QMS_FA') ");
                }
                else
                {
                    //祥天计划单据
                    sbSql.AppendFormat("  AND  MTYPE IN ('SAP','QMS_QF','QMS_QS','QMS_AF','QMS_AS')");
                }

                switch (varOutType)
                {
                    case "ONLINE":
                        sbSql.AppendFormat(" AND  TRNTP in ('G-','M-','R-')");
                        break;

                    case "TRANSFER":
                        sbSql.AppendFormat(" AND   TRNTP = 'T-'");
                        break;

                    case "COMBINE":
                        sbSql.AppendFormat(" AND  TRNTP in ('G-','M-','R-', 'T-')");
                        break;

                    case "ONLINEPROD":
                        sbSql.AppendFormat("  AND  TRNTP in ('G-','M-','R-')");
                        break;

                    case "SPARE_PARTS":
                        alConditions.Add(" AND   TRNTP in ('G-')");
                        break;

                    case "TWOPHASEOUT":
                        sbSql.AppendFormat("  AND  TRNTP in ('G-','T-')");
                        break;
                    //Add By Michael 20150928 for Film出库
                    case "Film_OutMaterial":
                        alConditions.Add(" AND   BWART in ('261','311')");
                        alConditions.Add(" AND   TRNTP in ('G-','M-','R-', 'T-')");
                        break;

                }
                if (varMatnr.Count > 0)
                {
                    foreach (string str in varMatnr)
                    {
                        strMatnr += "OR  D.MATNR Like '" + str.ToString() + "%' ";
                    }
                    strMatnr = strMatnr.Substring(2);
                    sbSql.AppendLine(" AND ( " + strMatnr + " )");
                }
                if (varMblnr.Count > 0)
                {
                    foreach (string str in varMblnr)
                    {
                        strMblnr += "OR  MBLNR Like '" + str.ToString() + "%' ";
                    }
                    strMblnr = strMblnr.Substring(2);
                    sbSql.AppendLine(" AND (  " + strMblnr + " )");
                }
                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;//連線SQL Server的時間3分鐘
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return dtResult;
            }
            #endregion


            #region 根据SN获取boxid 和palletid

            public DataTable GetSNInfo(string strSN, string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSNInfo";
                this.ControlMethodParm = "(" + strSN + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("  SELECT ")
                      .AppendLine(" MTYPE, D.MANDT, D.COMCD, WERKS, LGORT,  MATNR, INSMK, CHARG, MENGE, MBLNR, ZEILE , BOXID,REFID,  KOSTL, SERNO, MODEL,ISNULL(C.CTRLC1,'') AS PicturePath,ISNULL(C.CTRLC2,'') AS USRNAM,ISNULL(C.CTRLC3,'') AS PSWD ")
                      .AppendLine(" FROM WHDWN D WITH (nolock)")
                      .AppendLine(" LEFT JOIN  WHCTRL  C WITH (nolock)   ON C.MANDT=D.MANDT AND C.SOLDTO='QMS' AND C.CTRLID='PicturePath' AND C.CTRLNM=D.MODEL  ")
                      .AppendLine(" WHERE (D.MANDT = '" + MANDT + "')  AND (TRNTP IN ('G+', 'M+', 'R+'))  AND MENGE>OTQTY  ")
                      .AppendLine(" AND (MTYPE in( 'QMS_BGM','QMS_FAT'))   AND (SERNO ='" + strSN + "') ");
                if (strWerks != "")
                {
                    sbSql.AppendLine(" AND (WERKS='" + strWerks + "')");
                }
                if (strLgort != "")
                {
                    sbSql.AppendLine(" AND (LGORT='" + strLgort + "')");
                }

                DataTable dtData = new DataTable();
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

            #region 根据SN获取boxid 和palletid

            public DataTable GetAllSNByPalId(string strWerks, string strLogrt, string strLocat, string strPalId)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllSNByPalId";
                this.ControlMethodParm = "(" + strPalId + "," + strWerks + "," + strLgort + "," + strLocat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine(" SELECT ")
                         .AppendLine(" 'N' AS CHKED, MTYPE , MANDT, COMCD, WERKS, LGORT, '" + strLocat + "' AS LOCAT, MATNR, INSMK, CHARG, MENGE, 0 AS ALQTY, MBLNR, ZEILE ,  BOXID, '' AS OMBLNR, '' AS MRGID, KOSTL,  "
                                         + "ARBPL, TRNTP, EBELN, UMLGO,LIFNR, RMANO, '' AS RMAK1, '" + DateTime.Now.ToString("yyyyMMdd") + "' AS INDAT, KDMAT,   WO,  SERNO, LOADID, MODEL, REGION,PALQTY ,LOADID,REFID ")
                         .AppendLine(" FROM WHDWN WITH (nolock)")
                         .AppendLine(" WHERE  (TRNTP IN ('G+', 'M+', 'R+'))  AND MTYPE IN ( 'QMS_BGM','QMS_FAT')   AND MENGE>OTQTY  ")
                         .AppendLine(" AND  MANDT = '" + MANDT + "'  AND COMCD='" + COMCD + "'  AND WERKS='" + strWerks + "'  AND  MBLNR ='" + strPalId + "' ");
                sbSql.AppendLine("GROUP BY MTYPE, MANDT, COMCD, WERKS, LGORT ,MBLNR ,ZEILE,BOXID,  SERNO, MATNR, INSMK, CHARG,REFID,UMLGO,WO,MODEL,MENGE,KOSTL,ARBPL, TRNTP, EBELN, LIFNR, RMANO,KDMAT ,LOADID, MODEL, REGION,PALQTY,REFID  ");
                sbSql.AppendLine(" ORDER BY BOXID,SERNO ");
                DataTable dtData = new DataTable();
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

            #region Add By Annie 20180101for 退料判料入库
            public DataTable ListSapInReturn(string strMblnr, string strInsmk, string strQueryDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapInReturn";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + "," + strQueryDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select * , (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE in ('{0}') ", "SAP_TIC','OA_TIC','BPM_TIC");
                sbSql.AppendFormat(" AND ISNULL(REMAK1,'')='' ");


                if (strMblnr != "")
                {
                    sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                }

                if (strInsmk != "")
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }
                if (strQueryDate != "")
                {
                    sbSql.AppendFormat("AND CRDAT Between '" + strQueryDate + " 00:00:00.000' and '" + strQueryDate + " 23:59:59.999'");
                }

                DataTable dtData = new DataTable();
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

            #region  判票SA 退料判料入库
            public DataTable SapInSAReturn()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "SapInSAReturn";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select LOADID AS MBLNR, MATNR,CHARG,MENGE, (Menge - Otqty) as BALANCE from WHDWN with (nolock) where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE in ('{0}') ", "SAP_TIC','OA_TIC','BPM_TIC");
                sbSql.AppendFormat(" AND ISNULL(LOADID,'') !=''");
                sbSql.AppendFormat(" AND ISNULL(REMAK1,'')='' ");

                DataTable dtData = new DataTable();
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

            #region 列出异动为309的转换料号单据信息
            public DataTable ListSapData_MaterialConvert(string strMblnr, string strInsmk, string strDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ListSapData_MaterialConvert";
                this.ControlMethodParm = "(" + strMblnr + "," + strInsmk + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //string strSQL = "Select distinct MBLNR,MATNR , (Menge - Otqty) as BALANCE from WHDWN where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and MENGE> OTQTY and TRNTP in ('G+','R+','M+') and MTYPE='SAP' and BWART='321' and isnull(SERNO,'') <> '' ";
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("Select distinct MBLNR,MATNR ,INSMK, MENGE from WHDWN with (nolock) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                sbSql.AppendFormat("AND BWART='309' ", "");
                sbSql.AppendFormat("AND TRNTP='M-' ", "");//原料号
                sbSql.AppendFormat("AND CONVERT(varchar(8),CRDAT,112)='{0}' ", strDate);
                if (strInsmk == "")
                {
                }
                else
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }

                DataTable dtData = new DataTable();
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

            #region 转料号打印获取转料号或者原料号//转料号Trntp=M+，原料号Trntp=M-
            public DataTable GetMatnrConvert(string strMblnr, string strInsmk, string strDate, string strTrntp)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMatnrConvert";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT MBLNR,WERKS,LGORT,MATNR,LIFNR,MENGE,(MENGE-OTQTY) as RESIDUE, 0 as SCQTY ,DACOD,REFID FROM WHDWN with (nolock) WHERE BWART='309' ");
                sbSql.AppendFormat("AND MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", WERKS);
                sbSql.AppendFormat("AND LGORT='{0}' ", LGORT);
                if (strInsmk == "")
                {
                }
                else
                {
                    sbSql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                }
                sbSql.AppendFormat("AND TRNTP='{0}' ", strTrntp);
                sbSql.AppendFormat("AND MBLNR LIKE'{0}%' ", strMblnr);
                sbSql.AppendFormat("AND CONVERT(varchar(8),CRDAT,112)='{0}' ", strDate);

                DataTable dtData = new DataTable();
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

            #region 转料号打印获取原料号库存的位置
            public DataTable GetLocatCount(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetLocatCount";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT 1 AS ITEM,MBLNR,'' AS CMBLNR,'' AS OMBLNR,MATNR,WERKS,LGORT,LOCAT,0 as MENGE,0 AS RESIDUE,MENGE as ALMNG,0 as SCQTY FROM WHITM with (nolock) WHERE  ");
                sbSql.AppendFormat("WERKS='{0}' ", dtData.Rows[0]["WERKS"]);
                sbSql.AppendFormat("AND LGORT='{0}' ", dtData.Rows[0]["LGORT"]);
                sbSql.AppendFormat("AND MATNR='{0}' order by INDAT ASC,LOCAT ASC ", dtData.Rows[0]["MATNR"]);

                DataTable dtLocat = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtLocat = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return dtLocat;
            }
            #endregion

            #region 转料号打印更新数据库状态
            public bool UpdateStatus(DataTable dtLocat, DataTable ScanData, int SumScan, string Progid)
            {
                //設定要記錄Error Message的相關訊息
                //this.ControlMethodName = "UpdateStatus";
                //this.ControlMethodParm = "( )";
                //if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                //{
                //    ControlHandleError("000", "", "");
                //}

                //DataTable dtLocat = dtLocat;
                //DataTable ScanData = ScanData;
                //int SumScan = SumScan;

                bool flg = false;
                ArrayList arySQL = new ArrayList();
                ArrayList Locats = new ArrayList();
                StringBuilder strSqlUpdate = new StringBuilder();
                StringBuilder strSqlUpdate2 = new StringBuilder();
                DataTable dtCgcls = new DataTable();
                string strProgid = Progid;
                dtCgcls = GetCgcls(strProgid);

                #region WHDWN
                strSqlUpdate.Append("UPDATE WHDWN SET ");
                strSqlUpdate.AppendFormat("OTQTY=OTQTY+{0} WHERE ", SumScan);
                strSqlUpdate.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[0]["CMBLNR"]);
                strSqlUpdate.AppendFormat("AND MATNR='{0}' ", dtLocat.Rows[0]["MATNR"]);
                strSqlUpdate.AppendFormat("AND BWART='309' ");
                arySQL.Add(strSqlUpdate.ToString());

                strSqlUpdate2.Append("UPDATE WHDWN SET ");
                strSqlUpdate2.AppendFormat("OTQTY=OTQTY+{0} WHERE ", SumScan);
                strSqlUpdate2.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[0]["OMBLNR"]);
                strSqlUpdate2.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                strSqlUpdate2.AppendFormat("AND BWART='309' ");
                arySQL.Add(strSqlUpdate2.ToString());

                #endregion

                #region WHITM与WHLOG
                for (int i = 0; i < dtLocat.Rows.Count; i++)
                {
                    StringBuilder WhitmSqlUpdate = new StringBuilder();
                    StringBuilder strSqlInsert = new StringBuilder();

                    if (Convert.ToInt32(dtLocat.Rows[i]["SCQTY"]) > 0)
                    {

                        #region WHLOG

                        string strCgcls = dtCgcls.Rows[0]["CTRLC3"].ToString();
                        //转出异动
                        StringBuilder OWhlogSqlInsert = new StringBuilder();
                        OWhlogSqlInsert.AppendFormat("INSERT INTO WHLOG (MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,LIFNR,TRNTP,MBLNR,OMBLN,MENGE,INSMK,MRGID,INDAT,CRNAM,CRDAT,COMCD) (SELECT  MANDT,WERKS,LGORT,'{0}' AS CGCLS,LOCAT, ", strCgcls);
                        OWhlogSqlInsert.AppendFormat("'{0}' AS MATNR,", ScanData.Rows[0]["MATNR"]);
                        OWhlogSqlInsert.AppendFormat("LIFNR,'M-' AS TRNTP,'{0}' AS OMBLNR,MBLNR, ", dtLocat.Rows[0]["OMBLNR"]);
                        OWhlogSqlInsert.AppendFormat("-{0} AS MENGE,INSMK,MRGID,CONVERT(varchar(12) , getdate(), 112 ) AS INDAT, CRNAM,GETDATE() AS CRDAT,COMCD FROM WHITM WHERE ", dtLocat.Rows[i]["SCQTY"]);
                        OWhlogSqlInsert.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[i]["MBLNR"]);
                        OWhlogSqlInsert.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                        OWhlogSqlInsert.AppendFormat("AND LOCAT='{0}')", dtLocat.Rows[i]["LOCAT"]);
                        arySQL.Add(OWhlogSqlInsert.ToString());

                        //转入异动
                        StringBuilder CWhlogSqlInsert = new StringBuilder();
                        CWhlogSqlInsert.AppendFormat("INSERT INTO WHLOG (MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,LIFNR,TRNTP,MBLNR,OMBLN,MENGE,INSMK,MRGID,INDAT,CRNAM,CRDAT,COMCD) (SELECT  MANDT,WERKS,LGORT,'{0}' AS CGCLS,LOCAT,", strCgcls);
                        CWhlogSqlInsert.AppendFormat("'{0}' AS MATNR,", dtLocat.Rows[0]["MATNR"]);
                        CWhlogSqlInsert.AppendFormat("LIFNR,'M+' AS TRNTP,'{0}' AS CMBLNR,MBLNR, ", dtLocat.Rows[0]["CMBLNR"]);
                        CWhlogSqlInsert.AppendFormat("{0} AS MENGE,INSMK,MRGID,CONVERT(varchar(12) , getdate(), 112 ) AS INDAT, CRNAM,GETDATE() AS CRDAT,COMCD FROM WHITM WHERE ", dtLocat.Rows[i]["SCQTY"]);
                        CWhlogSqlInsert.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[i]["MBLNR"]);
                        CWhlogSqlInsert.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                        CWhlogSqlInsert.AppendFormat("AND LOCAT='{0}')", dtLocat.Rows[i]["LOCAT"]);
                        arySQL.Add(CWhlogSqlInsert.ToString());

                        #endregion

                        //foreach (string Locat in Locats)
                        //{
                        //    if (Locat == dtLocat.Rows[i]["LOCAT"])
                        //    {


                        //        //储位相同，删除原库存重复的储位
                        //        StringBuilder WhitmSqlDelete = new StringBuilder();
                        //        WhitmSqlDelete.Append("DELETE FROM WHITM WHERE ");
                        //        WhitmSqlDelete.AppendFormat("MBLNR ='{0}',", ScanData.Rows[i]["MBLNR"]);
                        //        WhitmSqlDelete.AppendFormat("AND MATNR ='{0}',", ScanData.Rows[i]["MATNR"]);
                        //        WhitmSqlDelete.AppendFormat("AND Locat ='{0}'", dtLocat.Rows[i]["LOCAT"]);
                        //        arySQL.Add(WhitmSqlDelete.ToString());

                        //        //储位相同，所以插入的数据主要一行
                        //        StringBuilder RWhitmSqlUpdate = new StringBuilder();
                        //        RWhitmSqlUpdate.Append("UPDATE WHITM SET ");
                        //        RWhitmSqlUpdate.AppendFormat("MENGE={0}+MENGE WHERE ", dtLocat.Rows[i]["SCQTY"]);
                        //        RWhitmSqlUpdate.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[i]["MBLNR"]);
                        //        RWhitmSqlUpdate.AppendFormat("AND MATNR='{0}' ", dtLocat.Rows[0]["MATNR"]);
                        //        RWhitmSqlUpdate.AppendFormat("AND LOCAT='{0}'", dtLocat.Rows[i]["LOCAT"]);
                        //        arySQL.Add(RWhitmSqlUpdate.ToString());

                        //    }
                        //}

                        //strSqlInsert.Append("INSERT INTO WHITM (MANDT,WERKS,LGORT,LOCAT,MATNR,INSMK,MBLNR,CHARG,EBELN,INDAT,MENGE,QCQTY,REQTY,CRNAM,CRDAT,MONAM,MODAT,COMCD,BKQTY,DACOD) (SELECT MANDT,WERKS,LGORT,LOCAT,"; 
                        //strSqlInsert.AppendFormat("'{0}' AS MATNR,INSMK,",dtLocat.Rows[0]["MATNR"]);
                        //strSqlInsert.AppendFormat("'{0}' AS MBLNR,CHARG,EBELN,CONVERT(varchar(12) , getdate(), 112 ) as INDAT,",dtLocat.Rows[0]["MBLNR"]);
                        //strSqlInsert.AppendFormat("'{0}' AS MENGE,0 AS QCQTY,0 AS REQTY,CRNAM,,GETDATE() AS CRDAT,MONAM,GETDATE() AS MODAT,COMCD,BKQTY,CONVERT(varchar(12) , getdate(), 112 ) as DACOD",dtLocat.Rows[i]["SCQTY"]);
                        #region WHITM
                        if (Convert.ToInt32(dtLocat.Rows[i]["SCQTY"]) == Convert.ToInt32(dtLocat.Rows[i]["ALMNG"]))
                        {

                            if (Locats.Contains(dtLocat.Rows[i]["LOCAT"]))
                            {


                                //储位相同，删除原库存重复的储位
                                StringBuilder WhitmSqlDelete = new StringBuilder();
                                WhitmSqlDelete.Append("DELETE FROM WHITM WHERE ");
                                WhitmSqlDelete.AppendFormat("MBLNR ='{0}' ", ScanData.Rows[i]["MBLNR"]);
                                WhitmSqlDelete.AppendFormat("AND MATNR ='{0}' ", ScanData.Rows[i]["MATNR"]);
                                WhitmSqlDelete.AppendFormat("AND Locat ='{0}'", dtLocat.Rows[i]["LOCAT"]);
                                arySQL.Add(WhitmSqlDelete.ToString());

                                //储位相同，所以插入的数据主要一行
                                StringBuilder RWhitmSqlUpdate = new StringBuilder();
                                RWhitmSqlUpdate.Append("UPDATE WHITM SET ");
                                RWhitmSqlUpdate.AppendFormat("MENGE={0}+MENGE WHERE ", dtLocat.Rows[i]["SCQTY"]);
                                RWhitmSqlUpdate.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[i]["CMBLNR"]);
                                RWhitmSqlUpdate.AppendFormat("AND MATNR='{0}' ", dtLocat.Rows[0]["MATNR"]);
                                RWhitmSqlUpdate.AppendFormat("AND LOCAT='{0}'", dtLocat.Rows[i]["LOCAT"]);
                                arySQL.Add(RWhitmSqlUpdate.ToString());

                            }
                            else
                            {
                                //旧就料号更新为新料号
                                WhitmSqlUpdate.Append("UPDATE WHITM SET ");
                                WhitmSqlUpdate.AppendFormat("MBLNR='{0}',", dtLocat.Rows[0]["CMBLNR"]);
                                WhitmSqlUpdate.AppendFormat("MATNR='{0}',", dtLocat.Rows[0]["MATNR"]);
                                WhitmSqlUpdate.AppendFormat("MENGE={0},QCQTY=0 WHERE ", dtLocat.Rows[i]["SCQTY"]);
                                WhitmSqlUpdate.AppendFormat("MBLNR='{0}' ", ScanData.Rows[i]["MBLNR"]);
                                WhitmSqlUpdate.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                                WhitmSqlUpdate.AppendFormat("AND LOCAT='{0}'", dtLocat.Rows[i]["LOCAT"]);
                                arySQL.Add(WhitmSqlUpdate.ToString());
                                Locats.Add(dtLocat.Rows[i]["LOCAT"]);
                            }


                        }
                        else if (Convert.ToInt32(dtLocat.Rows[i]["SCQTY"]) < Convert.ToInt32(dtLocat.Rows[i]["ALMNG"]))
                        {
                            //将旧料号库存更新
                            WhitmSqlUpdate.Append("UPDATE WHITM SET ");
                            //WhitmSqlUpdate.AppendFormat("QCQTY=QCQTY+{0} WHERE ", dtLocat.Rows[i]["SCQTY"]);
                            WhitmSqlUpdate.AppendFormat("MENGE=MENGE-{0} WHERE ", dtLocat.Rows[i]["SCQTY"]);
                            WhitmSqlUpdate.AppendFormat("MBLNR='{0}' ", ScanData.Rows[i]["MBLNR"]);
                            WhitmSqlUpdate.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                            WhitmSqlUpdate.AppendFormat("AND LOCAT='{0}'", dtLocat.Rows[i]["LOCAT"]);
                            arySQL.Add(WhitmSqlUpdate.ToString());


                            if (Locats.Contains(dtLocat.Rows[i]["LOCAT"]))
                            {




                                //储位相同，所以插入的数据主要一行
                                StringBuilder RWhitmSqlUpdate = new StringBuilder();
                                RWhitmSqlUpdate.Append("UPDATE WHITM SET ");
                                RWhitmSqlUpdate.AppendFormat("MENGE={0}+MENGE WHERE ", dtLocat.Rows[i]["SCQTY"]);
                                RWhitmSqlUpdate.AppendFormat("MBLNR='{0}' ", dtLocat.Rows[i]["CMBLNR"]);
                                RWhitmSqlUpdate.AppendFormat("AND MATNR='{0}' ", dtLocat.Rows[0]["MATNR"]);
                                RWhitmSqlUpdate.AppendFormat("AND LOCAT='{0}'", dtLocat.Rows[i]["LOCAT"]);
                                arySQL.Add(RWhitmSqlUpdate.ToString());

                            }
                            else
                            {


                                //将旧料号转出的库存插入到WHITM中
                                strSqlInsert.Append("INSERT INTO WHITM (MANDT,WERKS,LGORT,LOCAT,MATNR,INSMK,MBLNR,CHARG,EBELN,INDAT,MENGE,QCQTY,REQTY,CRNAM,CRDAT,MONAM,MODAT,COMCD,BKQTY,DACOD) (SELECT MANDT,WERKS,LGORT,LOCAT,");
                                strSqlInsert.AppendFormat("'{0}' AS MATNR,INSMK,", dtLocat.Rows[0]["MATNR"]);
                                strSqlInsert.AppendFormat("'{0}' AS MBLNR,CHARG,EBELN,INDAT,", dtLocat.Rows[0]["CMBLNR"]);
                                strSqlInsert.AppendFormat("{0} AS MENGE,0 AS QCQTY,0 AS REQTY,CRNAM,CRDAT,MONAM,MODAT,COMCD,BKQTY,DACOD FROM WHITM WHERE ", dtLocat.Rows[i]["SCQTY"]);
                                strSqlInsert.AppendFormat("MBLNR='{0}' ", ScanData.Rows[i]["MBLNR"]);
                                strSqlInsert.AppendFormat("AND MATNR='{0}' ", ScanData.Rows[0]["MATNR"]);
                                strSqlInsert.AppendFormat("AND LOCAT='{0}')", dtLocat.Rows[i]["LOCAT"]);
                                arySQL.Add(strSqlInsert.ToString());
                                Locats.Add(dtLocat.Rows[i]["LOCAT"]);
                            }



                        }
                        #endregion


                    }

                }
                #endregion


                try
                {

                    ControlHandleDB();
                    ControlSqlAccess.ExecSqlArray(arySQL);
                    flg = true;
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySapLineOutData()";
                }
                return flg;
            }

            #endregion

            #region  获取配置表的Cgcls
            public DataTable GetCgcls(string strProgid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strCgcls";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("Select CTRLC3 FROM WHCTRL where  ");
                sbSql.AppendFormat("CTRLC2='{0}' ", strProgid);


                DataTable dtData = new DataTable();
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

            #region  获取WHSMT的实时料号信息
            public DataTable getWhsmtCheck(string strWerks,string strGrpid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getWhsmtCheck";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("SELECT MATNR FROM WHSMT WHERE MANDT='218' AND COMCD='" + COMCD + "' AND WERKS='" + strWerks + "' AND (GRPID LIKE '" + strGrpid + "%') and TLQTY>MENGE GROUP BY MATNR");


                DataTable dtData = new DataTable();
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

            #region
            /// <summary>
            /// DateCode祥龙获取需求
            /// </summary>
            /// <param name="strWerk">厂区</param>
            /// <param name="strGrpid">GroupID</param>
            /// <returns></returns>
            public DataTable QuerySmtDateCodeData(string strWerk, string strGrpid)
            {
                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendFormat(" SELECT MANDT,COMCD,WERKS,COSCT,GRPID,MATNR,RLQTY,ROVAL,TLQTY-MENGE AS TOTAL,MENGE ISSUED_MENGE,(RLQTY-MENGE/ROVAL)*ROVAL AS DemandQty,WODAT,SHIFT,UMLGO FROM WHSMT WHERE WERKS='{0}' AND GRPID = '{1}' AND TLQTY>MENGE ", strWerk, strGrpid);

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QuerySmtDateCodeData()";
                }

                return dtResult;
            }
            #endregion

            #region 转仓311查询
            public DataTable QueryWhitmOverdueInspection(string strFlag, string strLgortTo)
            {
                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                if (strFlag == "Y")
                {
                    if (strLgortTo == "RWDP")
                    {
                        sbSql.AppendFormat(" SELECT * FROM View_DCQuery_Overdue_311 WHERE INSMK = 'G' AND DACOD <> '' AND VEDAT <> '' AND LOCKED = 'Y' and CONVERT(VARCHAR(8), getdate(), 112) >= QMMAX_New AND WERKS = '{0}' AND LGORT = '{1}' AND BKQTY >= 0 ", WERKS, LGORT);
                        sbSql.AppendFormat(" UNION ALL SELECT V.* FROM View_DCQuery_Overdue_311 V INNER JOIN WHIQC I with(nolock) ON V.TASKID= I.TASKID WHERE V.INSMK = 'G' AND V.DACOD <> '' AND V.VEDAT <> '' AND I.RESULT IN  ('REJECT','NG1','NG2') AND V.WERKS = '{0}' AND V.LGORT = '{1}' AND V.BKQTY >= 0 ", WERKS, LGORT);
                    }
                    else 
                    {
                        sbSql.AppendFormat(" SELECT * FROM View_DCQuery_Overdue_311 WHERE INSMK = 'G' AND DACOD <> '' AND VEDAT <> '' AND ISNULL(LOCKED,'') = '' AND ISNULL(TASKID,'') <> ''AND WERKS = '{0}' AND LGORT = '{1}' AND BKQTY >= 0 ", WERKS, LGORT);
                    }
                }
                else
                {
                    sbSql.AppendFormat(" SELECT * FROM View_DCQuery_Overdue_311 WHERE INSMK = 'G' AND DACOD <> '' AND VEDAT <> '' AND LOCKED = 'Y' AND WERKS = '{0}' AND LGORT = '{1}'AND BKQTY >= 0  ", WERKS, LGORT);
                }

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryWhitmOverdueInspection()";
                }

                return dtResult;
            }
            #endregion

            #region 转仓311查询
            public DataTable QueryWhlogUMLGO(string strMBLNR, string strCHARG, string strLIFNR, string strMATNR, string strDACOD)
            {
                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendFormat(" SELECT LGORT FROM Whlog with(nolock) WHERE WERKS='{0}' AND CHARG='{1}' AND LIFNR='{2}' AND MATNR='{3}' AND DACOD='{4}' and TRNTP = 'T-'AND SUBSTRING(MBLNR,1,10)= SUBSTRING('{5}',1,10) ", WERKS, strCHARG, strLIFNR,strMATNR,strDACOD, strMBLNR);

                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryWhlogUMLGO()";
                }

                return dtResult;
            }
            #endregion

            #endregion
        }
    }
}
