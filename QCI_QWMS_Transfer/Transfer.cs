using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;
using System.Data;
using System.Collections;
using QWMS.Entity;
using System.Net;
using System.IO;

namespace QCI
{
    namespace QWMS
    {
        public class Transfer : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strLgortNew = "";
            private string strErrmsg = "";

            #region Constructer

            public Transfer()
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
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Transfer(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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
                ControlErrorInfo.ObjectName = "QWMS.StorageData";
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

            public Transfer(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
            {
            }

            public Transfer(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "")
            {
            }

            public Transfer(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
                : this(varDBType, varDBCode, varErrorType, varErrorCode, varUserData, "", "")
            {
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
            /// Company code。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 倉別。--Old
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

            #region 根据条形码或输入的CPNO(车牌号)查询司机信息
            public DataTable GetTransferDriverInfo(string varCPNO, string varDriver)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetTransferDriverInfo";
                this.ControlMethodParm = "(" + varCPNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" Select CTRLC1 AS DriverName ,CTRLC2 AS TEL,CTRLC4 AS 身份证号,CTRLC5 AS CARID,REMAK AS IMGURL from WHCTRL WHERE MANDT='218'AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver' AND CTRLC5=N'" + varCPNO + "'AND CTRLC3=N'" + varDriver + "'");



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

            #region 根据条形码或输入的PCID(派车单号)查询派车单和车牌号的关联关系
            public DataTable GetTransferRelation(string varCarNo, string varPCID, string varstatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetTransferRelation";
                this.ControlMethodParm = "(" + varPCID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                //sbSql.AppendLine(" select * from trdwn where carno=N'"+varCarNo+"'and orderno='"+varPCID+"'");
                sbSql.AppendLine(" select DISTINCT MBLNR,CARNO,ORDERNO from trdwn where carno=N'" + varCarNo + "'and orderno='" + varPCID + "'and status='" + varstatus + "'");
                // DISTINCT MBLNR,CARNO,ORDERNO


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

            #region 根据条形码或输入的OutGRNO查询调拨信息
            public DataTable GetTransferData(string varCarNo, string varPCID, string varGRNO, string varFlag)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetTransferData";
                this.ControlMethodParm = "(" + varGRNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ROW_NUMBER()OVER(ORDER BY MBLNR,ZEILE)AS Item, MANDT, COMCD, FWERKS, FLGORT, DWERKS, DLGORT, MBLNR, STATUS,PO,BWART,ZEILE, MATNR, CHARG, MENGE, CARTONS,"
                + " PALLETS,  FPORT+'-'+ DPORT AS Transferroute, datepart(YEAR,CRETIME)AS Year,convert(varchar(100),CRETIME,111)AS PostingDate,CREWHO AS PostingUser,CONVERT(VARCHAR(100),GETDATE(),111) AS PrintDate "
                + " from [dbo].[TRDWN] WHERE   MBLNR='" + varGRNO + "'");

                if (varFlag == "E")
                {
                    sbSql.AppendLine(" AND FWERKS='" + WERKS + "' ");
                }
                else if (varFlag == "I")
                {
                    sbSql.AppendLine(" AND DWERKS='" + WERKS + "' ");
                }

                if (!string.IsNullOrEmpty(varCarNo))
                {
                    sbSql.AppendLine(" AND CarNo=N'" + varCarNo + "' ");
                }
                if (!string.IsNullOrEmpty(varPCID))
                {
                    sbSql.AppendLine(" AND orderno='" + varPCID + "' ");
                }

                //if (!string.IsNullOrEmpty(LGORT))
                //{
                //    sbSql.AppendLine(" AND DLGORT='" + LGORT + "' ");
                //}


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

            #region 查询状态等信息根据OutGrNo
            public DataTable GetTransferStatus(string varGRNO)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetTransferStatus";
                this.ControlMethodParm = "(" + varGRNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("  select STATUS,(case STATUS when 'N'then N'初始调拨单'when 'WP' then N'维护完码头'when'AT'then N'维护完派车单'when'WW'then N'打印完调拨单'when'WL'then N'仓库离厂'when'JL'then N'警卫离厂'when'JR'then N'警卫进厂'when'WR'then N'进入码头'when'WC'then N'仓库确认收货'end) AS ST from [dbo].[TRDWN]  WHERE MBLNR='" + varGRNO + "'");


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

            #region 警卫离厂更新状态和时间
            public bool UpdateStatus(string varGRNO, string varUsername, string varStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateStatus";
                this.ControlMethodParm = "(" + varGRNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                if (varStatus == "WW")
                {
                    sbSql.AppendLine(" update  [dbo].[TRDWN] set STATUS='WL',SLEAVE=GETDATE(),SLACOUNT='" + varUsername + "' WHERE MBLNR='" + varGRNO + "'");
                }

                else if (varStatus == "WL")
                {
                    sbSql.AppendLine(" update  [dbo].[TRDWN] set STATUS='JL',JLEAVE=GETDATE(),JLACOUNT='" + varUsername + "' WHERE MBLNR='" + varGRNO + "'");
                }
                else if (varStatus == "JL")
                {
                    sbSql.AppendLine(" update  [dbo].[TRDWN] set STATUS='JR',JRECEIVE=GETDATE(),JRACOUNT='" + varUsername + "' WHERE MBLNR='" + varGRNO + "'");
                }
                else if (varStatus == "JR")
                {
                    sbSql.AppendLine(" update  [dbo].[TRDWN] set STATUS='WR',SRECEIVE=GETDATE(),SRACOUNT='" + varUsername + "' WHERE MBLNR='" + varGRNO + "'");
                }
                else if (varStatus == "WR")
                {
                    sbSql.AppendLine(" update  [dbo].[TRDWN] set STATUS='WC',SCONFIRM=GETDATE(),SCACOUNT='" + varUsername + "' WHERE MBLNR='" + varGRNO + "'");
                }



                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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
                return bolResult;
            }
            #endregion

            #region 抓取351数据同步到sap获取101扣账信息
            public DataTable GetTransferDataby351(string varGRNO)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetTransferDataby351";
                this.ControlMethodParm = "(" + varGRNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" select distinct  MANDT,substring( MBLNR351,1,10) AS MBLNR351,YEAR351,DLGORT from TRDWN where MBLNR='" + varGRNO + "'");


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

            #region 更新101扣账编号到数据库
            public bool UpdateTransfer101MBLNR(string varGRNO, string varMBLNR101, string varYEAR101, string varMESSAGE101, string varLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateTransfer101MBLNR";
                this.ControlMethodParm = "(" + varGRNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine(" update  [dbo].[TRDWN] set DLGORT='" + varLgort + "',MBLNR101='" + varMBLNR101 + "',YEAR101='" + varYEAR101 + "',MESSAGE101='" + varMESSAGE101 + "' WHERE MBLNR='" + varGRNO + "'");





                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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
                return bolResult;
            }

            #endregion

            #region 获取收件人类别
            public DataTable getMailToType()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getMailToType";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT ''AS MailToType union select distinct [MailToType] FROM [dbo].[MailLoop] ");



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

            #region 获取收件人或抄送人信息
            public DataTable getMailInfo(string varMailToType, string varMailType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getMailInfo";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine(" SELECT distinct * FROM [dbo].[MailLoop] where  SOLDTO='QWMS'  ");

                if (!string.IsNullOrEmpty(varMailToType))
                {
                    sbSql.AppendLine(" and MailToType=N'" + varMailToType + "'");
                }
                if (!string.IsNullOrEmpty(varMailType))
                {
                    sbSql.AppendLine(" and MailType=N'" + varMailType + "'");
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

            #region 更新收件人信息
            public bool updateMailInfo(string varMailToType, string varMailType, string varMail)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateMailInfo";
                this.ControlMethodParm = "(" + varMailToType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();

                sbSql.AppendLine(" update MailLoop set MailTo='" + varMail + "' where SOLDTO='QWMS' and MailToType=N'" + varMailToType + "' and MailType=N'" + varMailType + "'");





                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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
                return bolResult;
            }

            #endregion

            #region 调拨 Add By Lora 2019/04/18

            #region 调拨制单获取库存信息
            /// <summary>
            /// 调拨制单获取库存信息
            /// </summary>
            /// <param name="varLocat"></param>
            /// <param name="varMatnr"></param>
            /// <param name="varInsmk"></param>
            /// <param name="varCharg"></param>
            /// <returns></returns>
            public DataTable GetStock(string varLocat, string varMatnr, string varInsmk, string varCharg)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetStock";
                this.ControlMethodParm = "(" + varLocat + "," + varMatnr + "," + varInsmk + "," + varCharg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                //if (varDouble == "L")
                //{
                sbSql.AppendFormat("SELECT WERKS,LGORT,MATNR,INSMK,CHARG,SUM(MENGE) AS MENGE,SUM(MENGE) AS ALQTY, LIFNR FROM WHITM WITH(NOLOCK) WHERE WERKS='" + WERKS + "' AND LGORT='" + LGORT + "' AND INSMK='" + varInsmk + "' ");
                //}

                if (varLocat != "")
                {
                    sbSql.AppendFormat("AND LOCAT='{0}' ", varLocat);
                }
                if (varMatnr != "")
                {
                    sbSql.AppendFormat("AND MATNR LIKE '{0}%' ", varMatnr);
                }
                if (varCharg != "")
                {
                    sbSql.AppendFormat("AND CHARG LIKE '{0}%' ", varCharg);
                }
                sbSql.AppendFormat("GROUP BY WERKS,LGORT,MATNR,CHARG,INSMK,LIFNR");

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

            #region 扣账校验调拨库存信息
            /// <summary>
            /// 校验WHDWN和WHITM数据信息
            /// </summary>
            /// <param name="varLocat"></param>
            /// <param name="varMatnr"></param>
            /// <param name="varCharg"></param>
            /// <returns></returns>
            public DataTable GetQwmsStorage(string strMblnr, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetQwmsStorage";
                this.ControlMethodParm = "(" + strMblnr + "," + strType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strSql;
                if (strType == "SAP_351")
                {
                    strSql = "SELECT MBLNR FROM ( SELECT WERKS,LGORT,MBLNR,MATNR,CHARG,SUM(MENGE) AS MENGE FROM PODWN WITH(NOLOCK) WHERE BWART='" + strType.Substring(strType.Length - 3, 3) + "' AND OTQTY=0 AND MBLNR ='" + strMblnr + "' GROUP BY WERKS,LGORT,MATNR,CHARG,MBLNR ) AS W INNER JOIN ( SELECT WERKS,LGORT,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) GROUP BY WERKS,LGORT,MATNR,CHARG ) AS I ON  W.MATNR=I.MATNR AND W.CHARG=I.CHARG AND W.LGORT=I.LGORT AND W.WERKS=I.WERKS AND W.MENGE-ISNULL(I.MENGE,0)>0 ";
                }
                else
                {
                    strSql = "SELECT MBLNR FROM ( SELECT WERKS,LGORT,MBLNR,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHDWN WITH(NOLOCK) WHERE MTYPE='" + strType + "' AND OTQTY=0 AND SUBSTRING(MBLNR,1,10)='" + strMblnr + "'   GROUP BY WERKS,LGORT,MATNR,CHARG,MBLNR) AS  W INNER JOIN ( SELECT WERKS,LGORT,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) GROUP BY WERKS,LGORT,MATNR,CHARG) AS I ON  W.MATNR=I.MATNR AND W.CHARG=I.CHARG AND W.LGORT=I.LGORT AND W.WERKS=I.WERKS AND W.MENGE-ISNULL(I.MENGE,0)>0 ";
                }
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql);
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

            #region 存储313/303调拨产生模拟单据的资料
            /// <summary>
            /// 313/303调拨产生模拟单据的信息
            /// </summary>
            /// <param name="dtSource">需要存储的数据信息</param>
            /// <param name="Type">313/303</param>
            /// <returns></returns>
            public DataTable ProduceTransSimulationData(DataTable dtOutSource, string Type)
            {
                this.ControlMethodName = "ProduceTransSimulationData";
                this.ControlMethodParm = "(" + Type + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                DataTable dtReturn = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                string strType = "";
                switch (Type)
                {
                    case "SAP_313": strType = "B";
                        break;
                    case "SAP_303": strType = "C";
                        break;
                    case "SAP_60S": strType = "D";
                        break;
                }

                string strMblnr = "";
                dtReturn.Columns.Add("MBLNR");
                int j = 0;


                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    #region Insert WHDWN

                    //将单据编号存入DataTable内并回传扣账
                    if (dtOutSource.Rows[i]["ZEILE"].ToString() == "0001")
                    {
                        strMblnr = GetTransSerno(strType);  //取得虚拟单据新的流水号
                        dtReturn.Rows.Add();   //產生新的一行
                        dtReturn.Rows[j++]["MBLNR"] = strMblnr;
                    }
                    alColumns.Clear();
                    alConditions.Clear();
                    objWhdwn.ResetField();

                    objWhdwn.Mandt = MANDT;
                    objWhdwn.Mtype = Type;//303/313调拨
                    objWhdwn.Mblnr = strMblnr;
                    objWhdwn.Zeile = dtOutSource.Rows[i]["ZEILE"].ToString();//Item
                    objWhdwn.Werks = dtOutSource.Rows[i]["FWERKS"].ToString();//调出厂区
                    objWhdwn.Lgort = dtOutSource.Rows[i]["FLGORT"].ToString();//调出仓别
                    objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString().ToUpper();
                    objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();//库别
                    objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString().ToUpper(); //版本
                    objWhdwn.Lifnr = dtOutSource.Rows[i]["LIFNR"].ToString().ToUpper();//厂商代码
                    objWhdwn.Ebeln = "";
                    objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString(); //调拨数量
                    objWhdwn.Prcde = "";
                    objWhdwn.Putyp = "";
                    objWhdwn.Kostl = dtOutSource.Rows[i]["DWERKS"].ToString().ToUpper();  //调入厂区
                    objWhdwn.Reslt = "";
                    objWhdwn.Budat = DateTime.Now.ToString("yyyyMMdd");
                    objWhdwn.Prity = "000000";
                    objWhdwn.Arbpl = "";
                    objWhdwn.Trntp = "";
                    objWhdwn.Bwart = "";
                    objWhdwn.Umlgo = dtOutSource.Rows[i]["DLGORT"].ToString().ToUpper();  //调入仓别
                    objWhdwn.Usnam = UserData.UserId.ToString().Trim();
                    objWhdwn.Kdmat = "";
                    objWhdwn.Crdat = "GETDATE()";
                    objWhdwn.Modat = "GETDATE()";
                    objWhdwn.Comcd = COMCD;
                    objWhdwn.Flage = "N";//N-生成虚拟单据，Y-SAP扣账
                    //objWhdwn.Remak1 = "";//扣账编号
                    //objWhdwn.Wkord = "";
                    //objWhdwn.Fmatn = "";
                    //objWhdwn.Ebelp = "";
                    //objWhdwn.Intid = "";

                    arySQL.Add(objWhdwn.EntityGetInsertSql());
                    #endregion
                }
                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
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

                return dtReturn;
            }
            #endregion

            #region 产生313/303/351调拨虚拟单号
            /// <summary>
            /// 生成313/303虚拟调拨单号
            /// </summary>
            /// <param name="strType">调拨类型：303/313</param>
            /// <returns></returns>
            public string GetTransSerno(string strType)
            {
               // sbSQL.Append("EXEC [dbo].[SP_CreateTransSerno] '" + strType + "' ");
                try
                {
                    QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                    string strDate = "{\"Type\":\"" + strType + "\"}";

                    //域名配置在数据库中
                    string strRequestUrl = objAuthority.GetApiRequestUrl();
                    strRequestUrl = strRequestUrl + "/QWMSAPI/api/QsmcQwms/GetTransSerno";
                    string strResult = HttpPostByHttpWebRequest(strRequestUrl, strDate);

                    //旧：固定写死
                    //string strResult = HttpPostByHttpWebRequest("http://172.19.81.219/AlimAPI/api/QsmcQwms/GetTransSerno", strDate);


                    if (strResult.Contains("ReturnMsg"))
                    {
                        throw new System.Exception("获取单号错误，请重试!!!!");
                    }
                    return strResult.Substring(1, 10);
                }
                catch
                {
                    throw;
                }
            }
            #endregion

            #region 根据单据号查询虚拟单据信息
            /// <summary>
            /// 根据单据号查询虚拟单据信息，用于生成之后的查询
            /// </summary>
            /// <param name="dtMblnr">单据号</param>
            /// <param name="strType">单据类型</param>
            /// <returns></returns>
            public DataTable QuerySimulationData(DataTable dtMblnr, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuerySimulationData";
                this.ControlMethodParm = "(" + dtMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                ArrayList arySQL = new ArrayList();
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT * FROM WHDWN WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND MTYPE='" + strType + "' AND ");
                for (int i = 0; i < dtMblnr.Rows.Count; i++)
                {
                    sbSql.AppendFormat("MBLNR='{0}' ", dtMblnr.Rows[i]["MBLNR"]);
                    if (i < dtMblnr.Rows.Count - 1)
                    {
                        sbSql.AppendFormat(" OR ");
                    }
                }

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.OpenConnection();
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

            #region 生成SAP扣账文档
            /// <summary>
            /// 生成调拨SAP扣帐文档
            /// </summary>
            /// <param name="varMblnr"></param>
            /// <param name="varType"></param>
            /// <returns></returns>
            public DataTable GetSapData(string varMblnr, string varType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSapData";
                this.ControlMethodParm = "(" + varMblnr + "," + varType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strSql = "";
                if (varType == "SAP_60S")
                {
                    strSql = " SELECT MANDT,CONVERT(VARCHAR(4),CRDAT,112) AS MYEAR,MBLNR AS IP_MBLNR,RIGHT(ZEILE,3) AS ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,LIFNR,USNAM,DWERKS,DLGORT,EBELN,EBELP FROM PODWN  WITH(NOLOCK) WHERE BWART='60S' AND MBLNR='" + varMblnr + "' order by ZEILE ";
                }
                else if (varType == "SAP_351" )
                {
                    strSql = " SELECT MANDT,CONVERT(VARCHAR(4),CRDAT,112) AS MYEAR,MBLNR AS IP_MBLNR,RIGHT(ZEILE,3) AS ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,LIFNR,USNAM,DWERKS,DLGORT,EBELN,EBELP FROM PODWN  WITH(NOLOCK) WHERE BWART='351' AND MBLNR='" + varMblnr + "' order by ZEILE ";
                }
                else if (varType == "101")
                {
                    strSql = " SELECT MANDT,CONVERT(VARCHAR(4),CRDAT,112) AS MYEAR,MBLNR AS IP_MBLNR,CONVERT(varchar(4),CRDAT,112) AS MJAHR,MBLNR351 AS MBLNR, RIGHT(ZEILE,3) AS ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,LIFNR,USNAM,DWERKS,DLGORT,EBELN,EBELP FROM PODWN  WITH(NOLOCK) WHERE BWART='351' AND MBLNR='" + varMblnr + "' order by ZEILE ";
                }
                else
                {
                    strSql = " SELECT MANDT,CONVERT(VARCHAR(4),CRDAT,112) AS MYEAR,MBLNR AS IP_MBLNR,RIGHT(ZEILE,3) AS ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,LIFNR,USNAM,KOSTL,UMLGO FROM WHDWN  WITH(NOLOCK) WHERE MTYPE='" + varType + "' and MBLNR='" + varMblnr + "' order by ZEILE ";
                }

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSql);
                    sqlAccess.CloseConnection();
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

            #region 记录调拨扣账dtSAP的条数日志

            public void insertQWMS_LOG(string str46Mblnr,string strSAPQty,string strQWMSQTY,string strFlag)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateTransRemark";
                this.ControlMethodParm = "(" + str46Mblnr + strSAPQty + strQWMSQTY + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                bool result = false;
                strsql.AppendFormat(" INSERT QWMS_LOG (MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME)VALUES('{0}','351','Save','{1}','{2}','{3}',GETDATE()) ", str46Mblnr, strSAPQty, strFlag, strQWMSQTY);
                try
                {
                    ControlHandleDB();
                    result = ControlSqlAccess.ExecSql(strsql.ToString());
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

            #region 更新调拨扣账信息
            /// <summary>
            /// 根据SAP回执更新WHDWN调拨信息
            /// </summary>
            /// <param name="dsrec"></param>
            public void UpdateTransRemark(DataSet dsrec, string varMBLNR)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateTransRemark";
                this.ControlMethodParm = "(" + varMBLNR + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                bool result = false;



                if (dsrec.Tables.Count > 0)//成功获取SAP文档并将数据放入dsrec
                {
                    if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")//扣账成功
                    {
                        if (varMBLNR.Substring(0, 1) == "A")//351
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET OTQTY=MENGE,FLAGE='Y',REMAK1=N'{0}',MBLNR351=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                        else if (varMBLNR.Substring(0, 1) == "B" || varMBLNR.Substring(0, 1) == "C")//303和313
                        {
                            strsql.AppendFormat(" UPDATE WHDWN SET OTQTY=MENGE,FLAGE='Y',REMAK1=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                        else//60S   一般是59的单子，但是问了SAP之后，他们说是5开头的单子，但是有时候会加零，变成05开头
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET OTQTY=MENGE,FLAGE='Y',REMAK1=N'{0}',MBLNR351=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                    }
                    else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")//扣账失败
                    {
                        if (varMBLNR.Substring(0, 1) == "A")//351
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET OTQTY=0,FLAGE='N',REMAK1=N'{0}',MBLNR351='' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                        else if (varMBLNR.Substring(0, 1) == "B" || varMBLNR.Substring(0, 1) == "C")//303和313
                        {
                            strsql.AppendFormat(" UPDATE WHDWN SET OTQTY=0,FLAGE='N',REMAK1=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                        else//60S   一般是59的单子，但是问了SAP之后，他们说是5开头的单子，但是有时候会加零，变成05开头
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET REMAK1=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                    }
                }




                try
                {
                    ControlHandleDB();
                    result = ControlSqlAccess.ExecSql(strsql.ToString());
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

            #region 调拨数量不足更新WHDWN信息
            /// <summary>
            /// 调拨数量不足更新WHDWN信息
            /// </summary>
            /// <param name="varMblnr"></param>
            public void UpdateMengeRemark(string varMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateMengeRemark";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsqlrec = new StringBuilder();
                bool result = false;
                string remak1 = "Less Menge";
                strsqlrec.AppendFormat(" UPDATE WHDWN SET REMAK1='{0}' WHERE MBLNR='{1}' ", remak1, varMblnr);

                try
                {
                    ControlHandleDB();
                    result = ControlSqlAccess.ExecSql(strsqlrec.ToString());
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

            #region 删除调拨数据
            /// <summary>
            /// 删除调拨数据
            /// </summary>
            /// <param name="varMblnr">调拨单号</param>
            /// <param name="strType">异动</param>
            /// <returns></returns>
            public bool delTransData(string varMblnr, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "delTransData";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    if (strType == "SAP_351")
                    {
                        //先退大PO数据
                        DataTable dt = new DataTable();
                        string selectSQL = "SELECT EBELN ,EBELP,MATNR,CHARG,WERKS,LGORT,MENGE FROM PODWN WITH(NOLOCK) WHERE MBLNR ='" + varMblnr + "'";
                        ControlHandleDB();
                        dt = ControlSqlAccess.GetDataTable(selectSQL);
                        ControlSqlAccess.CloseConnection();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sbSql.AppendFormat("UPDATE WHDWN SET OTQTY=OTQTY-{0} WHERE MBLNR='{1}' AND ZEILE='{2}' AND MATNR='{3}'  AND WERKS='{4}' ", dt.Rows[i]["MENGE"].ToString(), dt.Rows[i]["EBELN"].ToString(), dt.Rows[i]["EBELP"].ToString(), dt.Rows[i]["MATNR"].ToString(), dt.Rows[i]["WERKS"].ToString());
                        }
                        //删除数据
                        sbSql.AppendFormat(" UPDATE PODWN SET OTQTY=MENGE WHERE MBLNR='{0}'", varMblnr);
                    }
                    else
                    {
                        sbSql.AppendFormat(" UPDATE WHDWN SET OTQTY=MENGE WHERE MBLNR='{0}'", varMblnr);
                    }
                    bool bolResult = false;

                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return bolResult;
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

            #region 校验调出仓和接收仓是否对应
            /// <summary>
            /// 校验调出仓和接收仓是否对应
            /// </summary>
            /// <param name="varType">异动</param>
            /// <param name="varFlogrt">调出仓别</param>
            /// <param name="varDlogrt">调入仓别</param>
            /// <returns></returns>
            public DataTable CheckLgort(string varType, string varFlogrt, string varDlogrt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLgort";
                this.ControlMethodParm = "(" + varFlogrt + "," + varDlogrt + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                string strType = varType.Substring(4, 3);
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append("  DECLARE @return  varchar(1) ");
                sbSQL.Append("EXEC [SP_CheckTransLgort] '" + strType + "','" + varFlogrt + "','" + varDlogrt + "',@return out ");
                sbSQL.Append("SELECT @return");
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 调拨单打印(单号，异动代码) Add By Galen
            /// <summary>
            /// 调拨单打印(单号，异动代码)
            /// </summary>
            /// <param name="varMblnr">单号</param>
            /// <param name="varBwart">异动代码</param>
            /// <returns>DataTable</returns>
            public DataTable TransferOutPrint(string varMblnr, string varBwart)
            {
                this.ControlMethodName = "TransferOutPrint";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                if (varBwart.Trim() == "SAP_303" || varBwart.Trim() == "SAP_313")
                {
                    sbSQL.AppendFormat("SELECT ");
                    sbSQL.AppendFormat("MBLNR AS PO,COMCD,'*'+REMAK1+'*' AS MBLNR,WERKS,LGORT,KOSTL AS DWERKS,UMLGO AS DLGORT,RIGHT(MTYPE,3) AS MVT,RIGHT(ZEILE,3) AS Itm,WHDWN.MATNR AS Material,CONNECTIONSER='',CHARG AS Batch,MENGE AS RequireQTY,YEAR(GETDATE()) as YEAR,MAKTX AS Description ,CONVERT(varchar(100), WHDWN.CRDAT, 111) AS CRETIME,USNAM AS CREWHO,LIFNR AS Vendor  ");
                    sbSQL.AppendFormat("FROM WHDWN  WITH(NOLOCK) LEFT JOIN WHPAT WITH(NOLOCK) ON WHDWN.MATNR=WHPAT.MATNR ");
                    sbSQL.AppendFormat("WHERE MBLNR='" + varMblnr + "' AND MTYPE='" + varBwart + "' ORDER BY ZEILE ");
                }
                else if (varBwart.Trim() == "SAP_351")
                {
                    sbSQL.AppendFormat("SELECT ");
                    sbSQL.AppendFormat(" MBLNR, YEAR(GETDATE()) as YEAR,WERKS,LGORT,DWERKS,BWART,EBELN,CONVERT(varchar(100), PODWN.CRDAT, 111) AS CRDAT,'*'+MBLNR351+'*' AS MBLNR351,USNAM,ZEILE,PODWN.MATNR,CHARG,MAKTX,MENGE  ");
                    sbSQL.AppendFormat("FROM PODWN  WITH(NOLOCK) LEFT JOIN WHPAT WITH(NOLOCK) ON PODWN.MATNR=WHPAT.MATNR ");
                    sbSQL.AppendFormat("WHERE MBLNR='" + varMblnr + "' AND BWART='351' ORDER BY ZEILE ");
                }
                else if (varBwart.Trim() == "SAP_60S")
                {
                    sbSQL.AppendFormat("SELECT WERKS,LGORT,DWERKS,T1.CTRLC1 AS addrWerks,UPPER(T1.CTRLC1) AS addrWerk2,T1.CTRLC2 AS sAddr,T1.CTRLC5 AS tel,T1.REMAK AS fax,T2.CTRLC1 AS addrDwerks,T2.CTRLC3 AS addr1,T2.CTRLC4 AS addr2,'*'+MBLNR+'*' AS Delivery,'*'+MBLNR351+'*' AS Document,EBELN AS PO,EBELP AS POITEM ");
                    sbSQL.AppendFormat("FROM PODWN WITH(NOLOCK) ");
                    sbSQL.AppendFormat("INNER JOIN WHCTRL T1 ON T1.MANDT='218' AND T1.SOLDTO='QWMS' AND T1.CTRLID='WERKSINFO' AND WERKS=T1.CTRLNM ");
                    sbSQL.AppendFormat("INNER JOIN WHCTRL T2 ON T2.MANDT='218' AND T2.SOLDTO='QWMS' AND T2.CTRLID='WERKSINFO' AND DWERKS=T2.CTRLNM ");
                    sbSQL.AppendFormat("WHERE MBLNR='" + varMblnr + "' AND BWART='60S' ORDER BY ZEILE ");
                }
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region  获取是否打印过的状态
            /// <summary>
            /// 获取是否打印过的状态
            /// </summary>
            /// <returns></returns>
            public DataTable returnstatus(string varMblnr, string strType)
            {
                this.ControlMethodName = "returnstatus";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                sbSQL.AppendFormat("SELECT ");
                sbSQL.AppendFormat("ULFLG ");
                if (strType == "SAP_351" || strType == "SAP_60S")
                {
                    sbSQL.AppendFormat("FROM PODWN WITH(NOLOCK) ");
                    sbSQL.AppendFormat("WHERE MBLNR='" + varMblnr + "' AND BWART='" + strType.Substring(4, 3) + "'  ORDER BY ZEILE");
                }
                else
                {
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) ");
                    sbSQL.AppendFormat("WHERE MBLNR='" + varMblnr + "' AND MTYPE='" + strType + "' ORDER BY ZEILE");
                }
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region  更新是否打印过的状态
            /// <summary>
            /// 更新是否打印过的状态，再次打印增加管控
            /// </summary>
            /// <returns></returns>
            public void updatestatus(string varMblnr, string strType)
            {
                this.ControlMethodName = "updatestatus";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();

                if (strType == "SAP_351" || strType == "SAP_60S")
                {
                    sbSQL.AppendFormat("UPDATE PODWN SET ULFLG='Y' WHERE MBLNR='" + varMblnr + "'");
                }
                else
                {
                    sbSQL.AppendFormat("UPDATE WHDWN SET ULFLG='Y' WHERE MBLNR='" + varMblnr + "'");
                }
                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(sbSQL.ToString());
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

            #region 获取可以打印的单号
            /// <summary>
            /// 获取可以打印的单号
            /// </summary>
            /// <param name="usrnm">登陆账号名</param>
            /// <param name="again">重复打印</param>
            /// <param name="varBwart">异动代码</param>
            /// <returns></returns>
            public DataTable returnPrintPO(string usrnm, bool again, string varBwart)
            {
                this.ControlMethodName = "returnPrintPO";
                this.ControlMethodParm = "(" + usrnm + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                sbSQL.AppendFormat("SELECT ");
                sbSQL.AppendFormat("MBLNR ");
                if (varBwart == "SAP_351")
                {
                    sbSQL.AppendFormat("FROM PODWN WITH(NOLOCK)  ");
                    sbSQL.AppendFormat("WHERE OTQTY>0 AND FLAGE='Y' AND LEN(MBLNR351)=10 AND USNAM='" + usrnm + "' AND BWART='351' ");
                }
                else
                {
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK)  ");
                    sbSQL.AppendFormat("WHERE OTQTY>0 AND FLAGE='Y' AND LEN(REMAK1)=10 AND USNAM='" + usrnm + "' AND MTYPE='" + varBwart + "' ");
                }
                if (again)
                {
                    sbSQL.AppendFormat("AND ULFLG='Y' ");
                }
                else
                {
                    sbSQL.AppendFormat("AND ULFLG='N' ");
                }
                sbSQL.AppendFormat("GROUP BY MBLNR ORDER BY MAX(CRDAT) DESC ");

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 判断料号是否存在
            /// <summary>
            /// 判断WHITM表中是否有该料号
            /// </summary>
            /// <param name="varmatnr">料号</param>
            /// <param name="varcharg">版本</param>
            /// <param name="varwerks">厂区</param>
            /// <param name="varlgort">仓别</param>
            /// <returns></returns>
            public DataTable returnmatnrexists(string varmatnr, string varcharg, string varwerks, string varlgort)
            {
                this.ControlMethodName = "returnmatnrexists";
                this.ControlMethodParm = "(" + varmatnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();


                sbSQL.AppendFormat("SELECT MATNR FROM WHITM WITH(NOLOCK) WHERE  WERKS='" + varwerks + "' AND LGORT='" + varlgort + "' AND MATNR='" + varmatnr + "' AND CHARG='" + varcharg + "' ");
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 46P调拨(异动351)   BY Galen  2019/08/29

            #region 获取46P数据
            /// <summary>
            /// 获取SAP传过来的46P单据信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strMblnr">单号</param>
            /// <returns></returns>
            public DataTable Query46Pdata(string strWerks, string strLgort, string strLocat, string strMblnr, string strType)
            {
                this.ControlMethodName = "query46Pdata";
                this.ControlMethodParm = "(" + strMblnr + "PODWN表)";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();
                if (strType == "SAP_60S2")
                {
                    sbSQL.AppendFormat("UPDATE PODWN SET MODAT=GETDATE(),USNAM='{0}' WHERE MBLNR ='{1}' AND WERKS='{2}'   ", strLgort, strMblnr, strWerks);


                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,OTQTY,DWERKS,DLGORT,REMAK1   ");
                    sbSQL.AppendFormat("FROM PODWN   ");
                    sbSQL.AppendFormat("WHERE BWART='60S' AND MENGE-OTQTY>0 AND MBLNR='" + strMblnr + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    sbSQL.AppendFormat("ORDER BY ZEILE   ");
                }
                else if (strType == "SAP_60S")
                {
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,MENGE AS OTQTY,DWERKS AS KOSTL,DLGORT AS UMLGO,WSTATE ");
                    sbSQL.AppendFormat("FROM PODWN  WITH(NOLOCK)");
                    sbSQL.AppendFormat("WHERE BWART='60S' AND MENGE-OTQTY>0 AND MBLNR='" + strMblnr + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    sbSQL.AppendFormat("ORDER BY ZEILE  ");
                }
                else if (strLocat == "" && strType == "SAP_351")
                {
                    sbSQL.AppendFormat("SELECT ");
                    sbSQL.AppendFormat("MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,'0' AS OTQTY,KOSTL,UMLGO ");
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) ");
                    sbSQL.AppendFormat("WHERE MTYPE='SAP_46P' AND MENGE-OTQTY>0 AND MBLNR='" + strMblnr + "' AND WERKS='" + strWerks + "'  ");
                    sbSQL.AppendFormat("ORDER BY ZEILE ");
                }
                else if (strType == "SAP_351")//自动匹配该储位数据
                {
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,ISNULL(OTQTY,0) AS OTQTY,KOSTL,UMLGO FROM ( ");
                    sbSQL.AppendFormat("SELECT D.MBLNR,D.ZEILE,D.WERKS,D.LGORT,D.MATNR,D.CHARG,D.MENGE,I.MENGE AS OTQTY,D.KOSTL,D.UMLGO FROM ( ");
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,0 AS OTQTY,KOSTL,UMLGO  ");
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) WHERE MBLNR ='" + strMblnr + "' AND MENGE-OTQTY>0 AND CHARG<>'' ");
                    sbSQL.AppendFormat(") D ");
                    sbSQL.AppendFormat("LEFT JOIN  ");
                    sbSQL.AppendFormat("( ");
                    sbSQL.AppendFormat("SELECT WERKS,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND CHARG<>'' GROUP BY WERKS,MATNR,CHARG ");
                    sbSQL.AppendFormat(") I ON D.MATNR=I.MATNR AND D.WERKS=I.WERKS AND D.CHARG=I.CHARG ");

                    sbSQL.AppendFormat("UNION ALL ");

                    sbSQL.AppendFormat("SELECT D.MBLNR,D.ZEILE,D.WERKS,D.LGORT,D.MATNR,I.CHARG,D.MENGE,I.MENGE AS OTQTY,D.KOSTL,D.UMLGO FROM ( ");
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,0 AS OTQTY,KOSTL,UMLGO  ");
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) WHERE MBLNR ='" + strMblnr + "' AND MENGE-OTQTY>0 AND CHARG='' ");
                    sbSQL.AppendFormat(") D ");
                    sbSQL.AppendFormat("INNER JOIN  ");
                    sbSQL.AppendFormat("( ");
                    sbSQL.AppendFormat("SELECT WERKS,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND CHARG<>'' GROUP BY WERKS,MATNR,CHARG ");
                    sbSQL.AppendFormat(") I ON D.MATNR=I.MATNR AND D.WERKS=I.WERKS  ");

                    sbSQL.AppendFormat("UNION ALL ");

                    sbSQL.AppendFormat("SELECT D.MBLNR,D.ZEILE,D.WERKS,D.LGORT,D.MATNR,D.CHARG,D.MENGE,I.MENGE AS OTQTY,D.KOSTL,D.UMLGO FROM ( ");
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,0 AS OTQTY,KOSTL,UMLGO  ");
                    sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) WHERE MBLNR ='" + strMblnr + "' AND MENGE-OTQTY>0 AND CHARG='' ");
                    sbSQL.AppendFormat(") D ");
                    sbSQL.AppendFormat("INNER JOIN  ");
                    sbSQL.AppendFormat("( ");
                    sbSQL.AppendFormat("SELECT WERKS,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHITM WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND CHARG='' GROUP BY WERKS,MATNR,CHARG ");
                    sbSQL.AppendFormat(") I ON D.MATNR=I.MATNR AND D.WERKS=I.WERKS AND D.CHARG=I.CHARG ");
                    sbSQL.AppendFormat("UNION ALL ");
                    sbSQL.AppendFormat("SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,0 AS OTQTY,KOSTL,UMLGO  FROM WHDWN  WITH(NOLOCK) WHERE MBLNR ='" + strMblnr + "' AND MENGE-OTQTY>0 AND CHARG='' AND MATNR NOT IN (SELECT DISTINCT MATNR FROM WHITM WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' ) ");
                    sbSQL.AppendFormat(") G ORDER BY ZEILE,MATNR,OTQTY DESC ");
                }

                #region old code
                //if (strlocat=="")
                //{
                //    strlocat = "Galen";
                //}
                //sbSQL.AppendFormat("SELECT D.MBLNR,D.ZEILE,D.WERKS,D.LGORT,D.MATNR,D.CHARG,D.MENGE,CASE WHEN ISNULL(I.MENGE,0)=0 THEN 0 WHEN ISNULL(I.MENGE,0)>=D.MENGE THEN D.MENGE ELSE I.MENGE END OTQTY,D.KOSTL,D.UMLGO ");
                //sbSQL.AppendFormat("FROM (SELECT MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE-OTQTY AS MENGE,KOSTL,'' AS UMLGO FROM WHDWN WITH(NOLOCK) ");
                //sbSQL.AppendFormat("WHERE MTYPE='SAP_46P' AND OTQTY<>MENGE AND MBLNR='" + strMblnr + "' AND WERKS='" + strWerks + "' ) D ");
                //sbSQL.AppendFormat("LEFT JOIN ");
                //sbSQL.AppendFormat("(SELECT WERKS,LGORT,LOCAT,MATNR,CHARG,SUM(MENGE) AS MENGE,SUM(QCQTY) AS QCQTY ");
                //sbSQL.AppendFormat("FROM WHITM WHERE  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strlocat + "' GROUP BY WERKS,LGORT,LOCAT,MATNR,CHARG) I ");
                //sbSQL.AppendFormat("ON D.WERKS=I.WERKS AND D.MATNR=I.MATNR AND D.CHARG=I.CHARG ");
                //sbSQL.AppendFormat("ORDER BY D.ZEILE ");
                #endregion

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            public bool CheckChargQty(string strWweks,string strMblnr, string strZeile,string strMatnr,string strCharg,int Qty)
            {
                this.ControlMethodName = "query46Pdata";
                this.ControlMethodParm = "(" + strMblnr + "PODWN表)";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();
                bool flg=true;
                sbSQL.AppendFormat("SELECT ");
                sbSQL.AppendFormat("MBLNR ,MENGE-OTQTY AS MENGE ");
                sbSQL.AppendFormat("FROM WHDWN WITH(NOLOCK) ");
                sbSQL.AppendFormat("WHERE MTYPE='SAP_46P' AND WERKS='{0}' AND MBLNR='{1}' AND ZEILE='{2}' AND MATNR='{3}' AND MENGE-OTQTY>0 AND CHARG='{4}' ", strWweks, strMblnr, strZeile, strMatnr, strCharg);
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                    if (dtData.Rows.Count > 0)
                    {
                        int Menge=Convert.ToInt32(dtData.Rows[0]["MENGE"]);
                        if (Menge < Qty)
                            flg = false;
                    }
                    else
                        flg = false;
                    ControlSqlAccess.CloseConnection();
                    return flg;
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

            #region  生成46P单号，并获取去扣账的信息
            /// <summary>
            /// 生成46P单号，并获取去扣账的信息
            /// </summary>
            /// <param name="dt">选中信息的dt</param>
            /// <param name="USRNM">开单人</param>
            /// <returns></returns>
            public DataTable insert46Pdata(DataTable dt, string USRNM)
            {
                this.ControlMethodName = "insert46Pdata";
                this.ControlMethodParm = "(" + USRNM + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    string order = GetTransSerno("A");//获取QWMS生成的单号
                    StringBuilder sbSQL = new StringBuilder();
                    DataTable dtData = new DataTable();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StringBuilder insertSQL = new StringBuilder();
                        insertSQL.AppendFormat("INSERT INTO PODWN (MANDT,COMCD,MBLNR,ZEILE,EBELN,EBELP,WERKS,LGORT,MATNR,CHARG,INSMK,LIFNR,MENGE,OTQTY,DWERKS,DLGORT,BWART,CRDAT,MODAT,FLAGE,USNAM,ULFLG) ");
                        insertSQL.AppendFormat("VALUES('" + strMandt + "','" + strComcd + "','" + order + "','" + (i + 1).ToString("000") + "','" + dt.Rows[i]["MBLNR"].ToString() + "','" + dt.Rows[i]["ZEILE"].ToString() + "','" + dt.Rows[i]["WERKS"].ToString() + "','" + dt.Rows[i]["LGORT"].ToString() + "','" + dt.Rows[i]["MATNR"].ToString() + "','" + dt.Rows[i]["CHARG"].ToString() + "','','','" + dt.Rows[i]["OTQTY"].ToString() + "','0','" + dt.Rows[i]["KOSTL"].ToString() + "','" + dt.Rows[i]["UMLGO"].ToString() + "','351',GETDATE(),GETDATE(),'N','" + USRNM + "','N')  ");
                        ControlHandleDB();
                        ControlSqlAccess.ExecSql(insertSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                        StringBuilder updateSQL = new StringBuilder();
                        updateSQL.AppendFormat("UPDATE WHDWN SET OTQTY=OTQTY+" + dt.Rows[i]["OTQTY"].ToString() + " WHERE MTYPE='SAP_46P' AND MBLNR ='" + dt.Rows[i]["MBLNR"].ToString() + "' AND ZEILE='" + dt.Rows[i]["ZEILE"].ToString() + "' ");
                        ControlHandleDB();
                        ControlSqlAccess.ExecSql(updateSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }

                    sbSQL.AppendFormat("SELECT ");
                    sbSQL.AppendFormat("MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,MENGE,OTQTY,DWERKS,DLGORT,REMAK1 ");
                    sbSQL.AppendFormat("FROM PODWN ");
                    sbSQL.AppendFormat("WHERE MBLNR ='" + order + "' ORDER BY ZEILE");

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 获取调拨单号数据以及调拨入的厂区仓别信息
            /// <summary>
            /// 获取调拨单号数据以及调拨入的厂区仓别信息
            /// </summary>
            /// <param name="varMblnr">调拨单号</param>
            /// <param name="varMblnr351">351扣账单号</param>
            /// <returns></returns>
            public DataTable GetTransferNo(string varMblnr,string varMblnr351)
            {
                this.ControlMethodName = "GetTransferNo";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dt = new DataTable();
                sbSQL.AppendFormat("SELECT 'false' AS cSelect,COMCD,WERKS,LGORT,MBLNR,ZEILE,EBELN,EBELP,MATNR,CHARG,LIFNR,MENGE,DWERKS,DLGORT FROM PODWN WITH(NOLOCK) WHERE MANDT='218' AND COMCD='{0}' ", COMCD);
                if (!string.IsNullOrEmpty(varMblnr))
                    sbSQL.AppendFormat("AND MBLNR='{0}' ", varMblnr);
                else
                    sbSQL.AppendFormat("AND MBLNR351='{0}' ", varMblnr351);
                sbSQL.Append(" AND (MBLNR101 IS NULL OR MBLNR101='')");

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSQL.ToString());
                    sqlAccess.CloseConnection();
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
                return dt;
            }
            #endregion

            #region 获取351调拨单特殊仓别调入仓别须和调出一致

            public bool QuryLgortInfo(string strLgort)
            {
                this.ControlMethodName = "QuryLgortInfo";
                this.ControlMethodParm = "(" + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dt = new DataTable();
                bool bol = false;
                sbSQL.AppendFormat("SELECT   CTRLC1 FROM WHCTRL WITH (NOLOCK) WHERE  COMCD='{0}'AND  CTRLNM='Transfer101' AND CTRLC1='{1}'  ", COMCD, strLgort);
                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSQL.ToString());
                    sqlAccess.CloseConnection();
                    if (dt.Rows.Count > 0)
                    {
                        bol = true;
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
                return bol;
            }



            #endregion

            #region Tranfer101获取调拨单号数据以及调拨入的厂区仓别信息以及扣账信息
            /// <summary>
            /// 获取调拨单号数据以及调拨入的厂区仓别信息
            /// </summary>
            /// <param name="varMblnr">调拨单号</param>
            /// <param name="varMblnr351">351扣账单号</param>
            /// <returns></returns>
            public DataTable GetTransfer101SAP(string varMblnr,string strwerks,string type)
            {
                this.ControlMethodName = "GetTransfer101SAP";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dt = new DataTable();
                if (type == "351DWN")
                {
                    sbSQL.AppendFormat("SELECT WERKS,LGORT,MBLNR,ZEILE,EBELN,EBELP,MATNR,CHARG,LIFNR,MENGE,DWERKS,DLGORT,MBLNR351,MBLNR101,REMAK2 FROM PODWN WITH(NOLOCK) WHERE MANDT='218' AND COMCD='{0}' ", COMCD);
                    sbSQL.AppendFormat("AND MBLNR351='{0}' ", varMblnr);
                }
                else if (type == "101DWN")
                {
                    sbSQL.AppendFormat("SELECT DISTINCT SUBSTRING(MBLNR,1,10) AS MBLNR101 FROM WHDWN WITH(NOLOCK) WHERE MANDT='218' AND COMCD='{0}' AND WERKS='{1}' AND MTYPE='SAP' ",COMCD,strwerks);
                    sbSQL.AppendFormat("AND GRLOC LIKE '%{0}' ", varMblnr);
                }
                try
                {
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSQL.ToString());
                    sqlAccess.CloseConnection();
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
                return dt;
            }
            #endregion

            #region 更新Tranfer101扣账信息
            /// <summary>
            /// 更新Tranfer101扣账信息
            /// </summary>
            /// <param name="varMblnr">调拨单号</param>
            /// <param name="varMblnr351">351扣账单号</param>
            /// <returns></returns>
            public void updateTransfer101SAP(string varMblnr351,string varMblnr101)
            {
                this.ControlMethodName = "updateTransfer101SAP";
                this.ControlMethodParm = "(" + varMblnr351 + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                bool blRestult = false;
                sbSQL.AppendFormat("UPDATE PODWN SET MBLNR101='{0}' WHERE MANDT='218' AND COMCD='{1}' AND MBLNR351='{2}' ",varMblnr101, COMCD, varMblnr351);
                try
                {
                    ControlHandleDB();
                    blRestult = ControlSqlAccess.ExecSql(sbSQL.ToString());
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

            #region 根据Pallet或者BOXID获取料号数量
            /// <summary>
            /// 根据Pallet或者BOXID获取料号数量
            /// </summary>
            /// <param name="varMblnr">Pallet或者BOXID</param>
            /// <returns></returns>
            public DataTable GetPalletPNNum(string varMblnr)
            {
                this.ControlMethodName = "GetPalletPartNoNum";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dt = new DataTable();
                sbSQL.AppendFormat("SELECT PALLENT AS MBLNR,BOXID, MATNR,CHARG,SUM(MENGE) AS MENGE,INSMK  FROM PAL_DETAIL WITH(NOLOCK) WHERE MANDT='218' AND COMCD='{0}' AND (PALLENT='{1}' OR BOXID='{1}') ", COMCD, varMblnr);
                sbSQL.Append(" GROUP BY PALLENT,BOXID,MATNR,INSMK,CHARG");

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSQL.ToString());
                    sqlAccess.CloseConnection();
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
                return dt;
            }
            #endregion

            #region 更新351调拨单的调入仓别
            public bool UpdateTransfer101Lgort(string strWerks, string strLgort, string strMblnr, string strDLgort)
            {
                this.ControlMethodName = "UpdateTransfer101Lgort";
                this.ControlMethodParm = "(" + strWerks + "" + strLgort + "" + strMblnr + "" + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                bool blRestult = false;
                sbSQL.AppendFormat("UPDATE PODWN SET DLGORT='{0}' WHERE MANDT='{1}' AND COMCD='{2}' AND WERKS='{3}' AND LGORT='{4}' AND  MBLNR='{5}' ", strDLgort, MANDT, COMCD, strWerks, strLgort, strMblnr);

                try
                {
                    ControlHandleDB();
                    blRestult = ControlSqlAccess.ExecSql(sbSQL.ToString());
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
                return blRestult;
            }
            #endregion

            public void UpdateTransInRemark(DataSet dsrec, string varMBLNR)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateTransRemark";
                this.ControlMethodParm = "(" + varMBLNR + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                bool result = false;
                if (dsrec.Tables.Count > 0)//成功获取SAP文档并将数据放入dsrec
                {
                    if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")//扣账成功
                    {
                        if (varMBLNR.Substring(0, 1) == "A")//351
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET REMAK2=N'{0}',MBLNR101=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                        else if (varMBLNR.Substring(0, 1) == "B" || varMBLNR.Substring(0, 1) == "C")//303和313
                        {
                            strsql.AppendFormat(" UPDATE WHDWN SET GRLNR=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                        else//60S   一般是59的单子，但是问了SAP之后，他们说是5开头的单子，但是有时候会加零，变成05开头
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET REMAK2=N'{0}',MBLNR101=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim(), varMBLNR);
                        }
                    }
                    else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")//扣账失败
                    {
                        if (varMBLNR.Substring(0, 1) == "A")//351
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET REMAK2=N'{0}',MBLNR101='' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                        else if (varMBLNR.Substring(0, 1) == "B" || varMBLNR.Substring(0, 1) == "C")//303和313
                        {
                            strsql.AppendFormat(" UPDATE WHDWN SET REMAK1=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                        else//60S   一般是59的单子，但是问了SAP之后，他们说是5开头的单子，但是有时候会加零，变成05开头
                        {
                            strsql.AppendFormat(" UPDATE PODWN SET REMAK2=N'{0}' WHERE MBLNR='{1}' ", dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString(), varMBLNR);
                        }
                    }
                }

                try
                {
                    ControlHandleDB();
                    result = ControlSqlAccess.ExecSql(strsql.ToString());
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


            #region 比对调拨实收数量
            /// <summary>
            /// 比对调拨实收数量
            /// </summary>
            /// <param name="strMblnr">调拨单号</param>
            /// <returns></returns>
            public DataTable CheckTransfer101ScanQty(string strMblnr)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckTransfer101ScanQty";
                this.ControlMethodParm = "(" + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("SELECT COUNT(1) FROM (   SELECT MBLNR,MATNR,CHARG,SUM(MENGE) AS POQTY FROM PODWN WITH(NOLOCK) GROUP BY MBLNR,MATNR,CHARG) AS P LEFT JOIN (   SELECT PNUM,MATNR,CHARG,SUM(MENGE) AS SCANQTY FROM EC_INSTOCK WITH(NOLOCK) where PNUM='{0}'  GROUP BY PNUM,MATNR,CHARG) AS B ON P.MBLNR=B.PNUM AND P.MATNR=B.MATNR  AND P.CHARG=B.CHARG WHERE P.MBLNR='{0}' AND (P.POQTY!=B.SCANQTY OR B.SCANQTY IS NULL)", strMblnr);



                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();

                    dtData = ControlSqlAccess.GetDataTable(strsql.ToString());
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

            #region 46PO最初时间
            /// <summary>
            /// 查询46PO最初时间
            /// </summary>
            /// <param name="type"></param>
            /// <param name="varmblnr"></param>
            /// <returns></returns>
            public DataTable returnInitialTime(string type, string varmblnr)
            {
                this.ControlMethodName = "returnInitialTime";
                this.ControlMethodParm = "(" + varmblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                #region  最初sql
                //sbSQL.AppendFormat("select DISTINCT A.WERKS,A.LGORT,A.MBLNR,A.MATNR,A.MENGE,A.REFID,A.CRDAT,LEFT(B.MBLNR,10) AS TrOut,LEFT(D.MBLNR,10) AS TrIn,D.WERKS AS DWERKS,D.LGORT AS DLGORT,E.OLOCA ,E.MENGE AS EMENGE ");
                //sbSQL.AppendFormat("FROM WHDWN  AS A with(nolock) ");
                //sbSQL.AppendFormat("right join WHLOG AS B with(nolock) ON B.OMBLN=A.MBLNR AND A.MATNR=B.MATNR AND A.CHARG=B.CHARG ");
                //sbSQL.AppendFormat("right join WHDWN AS C with(nolock) ON B.MBLNR=C.MBLNR  AND B.MATNR=C.MATNR AND B.CHARG=C.CHARG ");
                //sbSQL.AppendFormat("right join WHDWN AS D with(nolock) ON RIGHT(D.REFID,10)=LEFT(C.MBLNR,10) AND C.MATNR=D.MATNR AND C.CHARG=D.CHARG ");
                //sbSQL.AppendFormat("right join WHLOG AS E WITH(NOLOCK) ON D.MBLNR=E.MBLNR AND D.MATNR=E.MATNR AND D.CHARG=E.CHARG ");
                //sbSQL.AppendFormat("where A.MTYPE='QMS_M' ");

                //if (type == "PalletID")
                //{
                //    sbSQL.AppendFormat("AND A.MBLNR='" + varmblnr + "'");
                //}
                //else if (type == "TrOut")
                //{
                //    sbSQL.AppendFormat("AND LEFT(B.MBLNR,10)='" + varmblnr + "'");
                //}
                //else if (type == "TrIn")
                //{
                //    sbSQL.AppendFormat("AND LEFT(D.MBLNR,10)='" + varmblnr + "'");
                //}
                //sbSQL.AppendFormat(" ORDER BY EMENGE,A.CRDAT,MENGE");
                #endregion

                #region  2019/07/23
                //sbSQL.AppendFormat("SELECT  T1.WERKS,T1.LGORT,T1.OMBLN AS PalletID,T1.MATNR,T1.CHARG,CRDAT,T1.MENGE, ");
                //if (type == "PalletID")
                //{
                //    sbSQL.AppendFormat("(SELECT DISTINCT SUBSTRING(MBLNR,1,10) FROM WHLOG WITH(NOLOCK) WHERE OMBLN='" + varmblnr + "') AS TrOut, ");
                //    sbSQL.AppendFormat("(select DISTINCT substring(mblnr,1,10) from WHDWN  WITH(NOLOCK) where right(REFID,10) in (SELECT DISTINCT SUBSTRING(MBLNR,1,10) FROM WHLOG WITH(NOLOCK) WHERE OMBLN='" + varmblnr + "')) AS TrIn, ");
                //    sbSQL.AppendFormat("T2.WERKS AS DWERKS,T2.LGORT AS DLGORT,T2.OLOCA ,T2.MENGE AS EMENGE FROM ");
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,MATNR,OMBLN,CHARG,CONVERT(VARCHAR(16),CRDAT,20) AS CRDAT,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE OMBLN ='" + varmblnr + "' ");
                //    sbSQL.AppendFormat("GROUP BY  WERKS,LGORT,MATNR,OMBLN,CHARG,CRDAT ");
                //    sbSQL.AppendFormat(") AS T1 INNER JOIN "); 
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,OLOCA,MATNR,OMBLN,CHARG,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE SUBSTRING(MBLNR,1,10) in (select TOP 1 substring(mblnr,1,10) from WHDWN WITH(NOLOCK) where right(REFID,10) in (SELECT TOP 1 SUBSTRING(MBLNR,1,10) FROM WHLOG WITH(NOLOCK) WHERE OMBLN='" + varmblnr + "')) ");
                //}
                //else if (type == "TrOut")
                //{
                //    sbSQL.AppendFormat("'"+varmblnr+"' AS TrOut, ");
                //    sbSQL.AppendFormat("(SELECT DISTINCT SUBSTRING(MBLNR,1,10) FROM WHDWN WITH(NOLOCK) WHERE RIGHT(REFID,10)='" + varmblnr + "' ) AS TrIn, ");
                //    sbSQL.AppendFormat("T2.WERKS AS DWERKS,T2.LGORT AS DLGORT,T2.OLOCA ,T2.MENGE  AS EMENGE FROM ");
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,MATNR,OMBLN,CHARG,CONVERT(VARCHAR(16),CRDAT,20) AS CRDAT,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE SUBSTRING(MBLNR,1,10) ='" + varmblnr + "' ");
                //    sbSQL.AppendFormat("GROUP BY  WERKS,LGORT,MATNR,OMBLN,CHARG,CRDAT ");
                //    sbSQL.AppendFormat(") AS T1 INNER JOIN ");
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,OLOCA,MATNR,OMBLN,CHARG,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE SUBSTRING(MBLNR,1,10) in (select distinct substring(mblnr,1,10) as TrIn from WHDWN WITH(NOLOCK) where right(REFID,10)='" + varmblnr + "') ");
                //}
                //else if (type == "TrIn")
                //{
                //    sbSQL.AppendFormat("(SELECT DISTINCT SUBSTRING(REFID,6,10) FROM WHDWN WITH(NOLOCK) WHERE LEFT(MBLNR,10) = '" + varmblnr + "' ) AS TrOut, ");
                //    sbSQL.AppendFormat("'"+varmblnr+"' AS TrIn, ");
                //    sbSQL.AppendFormat("T2.WERKS AS DWERKS,T2.LGORT AS DLGORT,T2.OLOCA ,T2.MENGE  AS EMENGE FROM ");
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,MATNR,OMBLN,CHARG,CONVERT(VARCHAR(16),CRDAT,20) AS CRDAT,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE SUBSTRING(MBLNR,1,10) IN (SELECT DISTINCT SUBSTRING(REFID,6,10) AS MBLNR  FROM WHDWN WITH(NOLOCK) WHERE LEFT(MBLNR,10) = '" + varmblnr + "') ");
                //    sbSQL.AppendFormat("GROUP BY  WERKS,LGORT,MATNR,OMBLN,CHARG,CRDAT ");
                //    sbSQL.AppendFormat(") AS T1 ");
                //    sbSQL.AppendFormat("INNER JOIN ");
                //    sbSQL.AppendFormat("( ");
                //    sbSQL.AppendFormat("SELECT WERKS,LGORT,OLOCA,MATNR,OMBLN,CHARG,SUM(MENGE) AS MENGE FROM WHLOG WITH(NOLOCK) WHERE SUBSTRING(MBLNR,1,10)='" + varmblnr + "' ");
                //}

                //sbSQL.AppendFormat("GROUP BY  WERKS,LGORT,OLOCA,MATNR,OMBLN,CHARG ");
                //sbSQL.AppendFormat(") AS T2 ON  T1.MATNR=T2.MATNR AND T1.CHARG=T2.CHARG ");
                //sbSQL.AppendFormat("ORDER BY T1.MATNR,T1.CHARG,T2.OLOCA,T2.MENGE,CRDAT,T1.MENGE ");
                #endregion

                #region 2019/08/05
                sbSQL.AppendFormat("EXEC [sp_InitialTimeOf46PO] '" + type + "','" + varmblnr + "'");
                #endregion
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 通过API获取数据
            /// <summary>
            /// 通过API获取数据
            /// </summary>
            /// <param name="Url">网址</param>
            /// <param name="PostData">消息体</param>
            /// <returns></returns>
            public static string HttpPostByHttpWebRequest(string Url, object PostData)
            {
                try
                {
                    //POST参数
                    string strPostData = PostData.ToString().Replace("\r\n", "");
                    byte[] bytPostData = Encoding.UTF8.GetBytes(strPostData);
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                    request.Method = "POST";
                    request.Timeout = 1000000;
                    request.ContentType = "application/json";   //"application/x-www-form-urlencoded";
                    request.ContentLength = bytPostData.Length;

                    //解决IIS配置
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                    System.IO.Stream objPostStream = request.GetRequestStream();
                    objPostStream.Write(bytPostData, 0, bytPostData.Length);
                    objPostStream.Close();
                    //获取响应
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader objResponseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string strResult = objResponseStreamReader.ReadToEnd();
                    objResponseStreamReader.Close();

                    return strResult;
                }
                catch
                {
                    throw;
                }
            }
            #endregion

            #region 调拨车申请获取单据类型
            public DataTable CheckBwartAuthority()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckBwartAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    alColumns.Clear();
                    alColumns.Add(" CTRLC1 as F_TEXT ");
                    //alColumns.Add(" CTRLC1 as F_VALUE ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='Transfer_Type'");
                    alConditions.Add(" CTRLNM ='MVT'");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);


                    dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    return dtResult;

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

            #region 获取调拨车邮件收件及抄送人
            public DataTable CheckSendMailAuthority(string strRoute1, string strType)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckSendMailAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    if (strRoute1.Length>4)
                    {
                        if (strRoute1.Substring(0, 4).ToString().Trim() == "QBUS")
                        {
                            strRoute1 = "QBS" + strRoute1.Substring(strRoute1.Length - 1, 1).ToString().Trim();
                        }
                    }
                    DataTable dtResult = new DataTable();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("SELECT EMAIL FROM WHMAL WITH(NOLOCK) WHERE ");
                    strSql.AppendFormat("MANDT='{0}' ", MANDT);
                    strSql.AppendFormat(" AND WERKS='{0}' ", strRoute1);
                    strSql.AppendFormat(" AND FUNCT='TransferCar_Email' ");
                    strSql.AppendFormat(" AND MTYPE='{0}'",strType);
                    strSql.AppendFormat(" AND COMCD='{0}' ", COMCD);
                    //SELECT EMAIL FROM WHMAL WITH(NOLOCK) WHERE MANDT='218' AND COMCD='9200' AND FUNCT='TransferCar_Email' AND MTYPE='0'
                    //DataTable dtResult = new DataTable();
                    //DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    //ArrayList alColumns = new ArrayList();
                    //ArrayList alConditions = new ArrayList();

                    //alColumns.Clear();
                    //alColumns.Add(" CTRLC2 as CS ");
                    //alColumns.Add(" CTRLC1 as CC ");

                    //alConditions.Clear();
                    //alConditions.Add(" MANDT='" + MANDT + "'");
                    //alConditions.Add(" COMCD='" + COMCD + "'");
                    //alConditions.Add(" SOLDTO='QWMS'");
                    //alConditions.Add(" CTRLID='Transfer_Type'");
                    //alConditions.Add(" CTRLNM ='" + strRoute1 + "'"); 
                    //alConditions.Add(" REMAK ='Transfer_ApplyForCar_Email'");
                    //dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtResult;
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

            #region 调拨车申请获取单据
            public DataTable GetMblnrData(string strWerks,string strLgort,string strType,string dtTime)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMblnrData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT DISTINCT SUBSTRING(MBLNR,1,10) AS MBLNR  FROM WHDWN WITH(NOLOCK) WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", MANDT);
                strSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                strSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                if (strLgort.Contains(","))
                {
                    string[] aryLgort = strLgort.Split(',');
                    string strLgortList = "(";
                    for (int i = 0; i < aryLgort.Length; i++)
                    {
                        strLgortList = strLgortList + "'" + aryLgort[i].ToString() + "',";
                    }
                    strLgortList = strLgortList.Substring(0, strLgortList.Length - 1);
                    strLgortList = strLgortList + ")";
                    strSql.AppendFormat("AND (LGORT IN {0}) ", strLgortList);
                }
                else
                {
                    strSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                }
                //strSql.AppendFormat(" AND MBLNR NOT LIKE '66%' ");
                if (strType.Equals(""))
                {
                    strSql.Append("AND (BWART IN ('261','303','311','313','321','351','45L','60S','90C','911')) ");
                }
                else if (strType.Contains(","))
                {
                    string[] aryType = strType.Split(',');
                    string strTypeList = "(";
                    for (int i = 0; i < aryType.Length; i++)
                    {
                        strTypeList = strTypeList + "'" + aryType[i].ToString() + "',";
                    }
                    strTypeList = strTypeList.Substring(0, strTypeList.Length - 1);
                    strTypeList = strTypeList + ")";
                    strSql.AppendFormat("AND (BWART IN {0}) ", strTypeList);
                }
                else
                {
                    strSql.AppendFormat("AND BWART='{0}' ", strType);
                }
                if (dtTime.Equals(""))
                {
                    strSql.Append("AND (CRDAT>convert(varchar(10),dateadd(day,-5,GETDATE()),120)) ");
                }
                else
                {
                    strSql.AppendFormat("AND (CRDAT BETWEEN '{0}" + " 00:00:00.000' " + "AND '{0}" + " 23:59:59.999'" + ") ", dtTime);
                }
                if (strLgort != "AS10")
                {
                    strSql.Append("AND OTQTY=MENGE");
                }
                strSql.Append(" AND TRNTP IN ('T-','G-')  ");
                strSql.Append("AND NOT EXISTS (SELECT TRITEM.MBLNR FROM TRITEM WHERE TRITEM.MBLNR=WHDWN.MBLNR AND  TRITEM.ZEILE=WHDWN.ZEILE AND TRITEM.FLAGE <> 'X')  ");
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    dtResult = CommonInfo.SortDataTable(dtResult, "MBLNR");
                    return dtResult;

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

            #region 调拨车申请获取调拨路线
            public DataTable CheckRouteAuthority(string strWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckRouteAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    alColumns.Clear();
                    alColumns.Add(" CTRLC2 AS F_TEXT ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='Transfer_Type'");
                    alConditions.Add(" REMAK ='TransferCar_Route'");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                    dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    return dtResult;

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

            #region 调拨车申请获取单据明细
            public DataTable QueryMblnrData(string strWerks, string strLgort, string strMblnr, string strType)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMblnrData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT  '' AS ITEM,WERKS,LGORT,MBLNR,ZEILE,WHDWN.MATNR,CHARG,MENGE,KOSTL,BWART,ISNULL(WHPAT.MAKTX,'') AS KDMAT,'' AS Type  FROM WHDWN WITH(NOLOCK) LEFT JOIN WHPAT WITH(NOLOCK) ON WHDWN.MATNR=WHPAT.MATNR WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", MANDT);
                strSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                if (strLgort.Contains(","))
                {
                    string[] aryLgort = strLgort.Split(',');
                    string strLgortList = "(";
                    for (int i = 0; i < aryLgort.Length; i++)
                    {
                        strLgortList = strLgortList + "'" + aryLgort[i].ToString() + "',";
                    }
                    strLgortList = strLgortList.Substring(0, strLgortList.Length - 1);
                    strLgortList = strLgortList + ")";
                    strSql.AppendFormat("AND (LGORT IN {0}) ", strLgortList);
                }
                else
                {
                    strSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                }              
                strSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                if (strMblnr.Contains(","))
                {
                    string[] aryMblnr = strMblnr.Split(',');
                    string strMblnrList = "(";
                    for (int i = 0; i < aryMblnr.Length; i++)
                    {
                        strMblnrList = strMblnrList + "'" + aryMblnr[i].ToString() +"',";
                    }
                    strMblnrList = strMblnrList.Substring(0, strMblnrList.Length - 1);
                    strMblnrList = strMblnrList + ")";
                    strSql.AppendFormat("AND SUBSTRING(MBLNR,1,10) IN {0} ", strMblnrList);
                }
                else
                    strSql.AppendFormat("AND MBLNR LIKE '{0}' ", strMblnr+"%");
                if (strLgort != "AS10")
                {
                    strSql.Append("AND OTQTY=MENGE");
                }
                strSql.Append(" AND TRNTP IN ('T-','G-')  ");

                if (strType.Equals(""))
                {
                    strSql.Append("AND (BWART IN ('261','303','311','313','321','351','45L','60S','90C','911')) ");
                }
                else if (strType.Contains(","))
                {
                    string[] aryType = strType.Split(',');
                    string strTypeList = "(";
                    for (int i = 0; i < aryType.Length; i++)
                    {
                        strTypeList = strTypeList + "'" + aryType[i].ToString() + "',";
                    }
                    strTypeList = strTypeList.Substring(0, strTypeList.Length - 1);
                    strTypeList = strTypeList + ")";
                    strSql.AppendFormat("AND (BWART IN {0}) ", strTypeList);
                }
                else
                {
                    strSql.AppendFormat("AND BWART='{0}' ", strType);
                }
                strSql.AppendFormat("AND NOT EXISTS (SELECT TRITEM.MBLNR FROM TRITEM WHERE TRITEM.MBLNR=WHDWN.MBLNR AND  TRITEM.ZEILE=WHDWN.ZEILE AND TRITEM.FLAGE <> 'X') ");
                strSql.AppendFormat(" ORDER BY MBLNR");
                

                try
                {

                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection(); 
                    return dtResult;

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

            #region 自动仓申请调拨车单据卡控
            public bool CheckIsAsrs(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckIsASRS";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    bool flag = true;
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    alColumns.Clear();
                    alColumns.Add(" CTRLNM");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='StorageIn_Type'");
                    alConditions.Add(" CTRLNM='"+ strWerks +"'");
                    alConditions.Add(" CTRLC1='" + strLgort + "'");
                    alConditions.Add(" REMAK ='Transfer_ASRS'");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);

                    if (dtResult.Rows.Count > 0)
                    {
                        flag = false;
                    }
                    return flag;


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

            #region check是否有未全部作业的Mblnr
            /// <summary>
            /// check是否有未全部作业的Mblnr
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            public DataTable QueryMblnrStatus(string strWerks, string strLgort, string strMblnr)
            {
                //設定要記錄Error Message的相關訊息dtpTime
                this.ControlMethodName = "QueryMblnrStatus";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort +  "," + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT  MBLNR FROM WHDWN WITH (NOLOCK)  ");
                strSql.AppendFormat("WHERE WERKS='{0}' ", strWerks);
                strSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                strSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                strSql.AppendFormat("AND OTQTY<MENGE AND (BWART IN ('261','303','311','313','321','351','45L','60S','90C','911'))AND TRNTP IN ('T-','G-') AND");
                if (strMblnr.Contains(","))
                {
                    string[] aryMblnr = strMblnr.Split(',');
                    strSql.AppendFormat(" ( ");
                    for (int i = 0; i < aryMblnr.Length; i++)
                    {                       
                        if (i != 0)
                            strSql.AppendFormat(" OR ");
                        strSql.AppendFormat(" MBLNR LIKE '" + aryMblnr[i].ToString() + "%'");                                       
                    }
                    strSql.AppendFormat(" ) ");
                }
                else
                {
                    strSql.AppendFormat(" MBLNR LIKE '{0}' ", strMblnr + "%");
                }
                try
                {
                    //只读实例
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtResult = sqlAccess.GetDataTable(strSql.ToString());
                    sqlAccess.CloseConnection();
                    return dtResult;
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




            #region 调拨车49单据明细
            public DataTable QueryMblnrItem(string strWerks, string dtpTime)
            {

                //設定要記錄Error Message的相關訊息dtpTime
                this.ControlMethodName = "QueryMblnrItem";
                this.ControlMethodParm = "(" + strWerks + "," + dtpTime + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT H.CRDAT,H.APPLYNO,TRTYPE,APPLYNM,ACCOUNT,KOSTL,SROUTE,EROUTE,TRDAT,PLATE,BOX,PCS,MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,KDMAT,MENGE,DWERKS,DLGORT,BWART,TYPE,I.CRNAM,I.FLAGE FROM TRHEAD AS H WITH(NOLOCK) LEFT JOIN TRITEM AS I WITH(NOLOCK) ON H.COMCD=I.COMCD AND H.APPLYNO=I.APPLYNO ");
                strSql.AppendFormat("WHERE H.MANDT='{0}' ", MANDT);
                strSql.AppendFormat("AND H.COMCD='{0}' ", COMCD);
                strSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                //strSql.AppendFormat("AND H.FLAGE<>'X' ");
                strSql.AppendFormat("AND H.CRDAT BETWEEN '{0} 00:00:00.000' AND GETDATE() ORDER BY H.CRDAT", dtpTime);           
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtResult;
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

            #region 生成调拨车申请单单号 Add By Zachary 20210507
            public string CreateApplyNo(string strWerks)
            {
                this.ControlMethodName = "CreateApplyNo";
                this.ControlMethodParm = "("+strWerks+")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSQL = new StringBuilder();
                string ApplyNo;
                strSQL.AppendFormat("EXEC SP_CreateApplyNO '{0}'", strWerks);
                try
                {
                    ControlHandleDB();
                    ApplyNo = ControlSqlAccess.GetFieldValue(strSQL.ToString());
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
                return ApplyNo;
            }


            #endregion

            #region 生成调拨车申请单 Add By Zachary 20210507
            public bool InsertTrHeader(string strUserID, string strUserName, string strFiCode, string strRoute1, string strRoute2, string ApplyTime, string strPlate, string strBox, string strPCS, string strApplyNo, string strApplyType, DataTable dtData)
            {
                this.ControlMethodName = "InsertTrHeader";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                ArrayList arrSql = new ArrayList();
                StringBuilder strSQL = new StringBuilder();
                bool flg = false;
                //string ApplyNo;
                strSQL.Append("INSERT INTO TRHEAD (APPLYNO,TRTYPE,APPLYNM,ACCOUNT,KOSTL,ULFLG,SROUTE,EROUTE,TRDAT,PLATE,BOX,PCS,CRNAM,CRDAT,COMCD) VALUES(");
                strSQL.AppendFormat("'{0}',N'{1}',N'{2}','{3}','{4}','0',", strApplyNo, strApplyType, strUserName, strUserID, strFiCode);
                strSQL.AppendFormat("'{0}','{1}','{2}',{3},{4},{5},", strRoute1, strRoute2, ApplyTime, strPlate, strBox, strPCS);
                strSQL.AppendFormat("'{0}',GETDATE(),'{1}' )",UserData.UserId , COMCD);
                arrSql.Add(strSQL.ToString());

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    StringBuilder strSQL2 = new StringBuilder();
                    strSQL2.Append("INSERT INTO TRITEM (MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,KDMAT,MENGE,DWERKS,ULFLG,BWART,CRNAM,CRDAT,APPLYNO,TYPE,COMCD) VALUES(");
                    strSQL2.AppendFormat("'{0}','{1}','{2}','{3}','{4}',", dtData.Rows[i]["MBLNR"].ToString(), dtData.Rows[i]["ZEILE"].ToString(), dtData.Rows[i]["WERKS"].ToString(), dtData.Rows[i]["LGORT"].ToString(), dtData.Rows[i]["MATNR"].ToString());
                    strSQL2.AppendFormat("'{0}','{1}',{2},'{3}','0',", dtData.Rows[i]["CHARG"].ToString(), dtData.Rows[i]["KDMAT"].ToString(), dtData.Rows[i]["MENGE"], dtData.Rows[i]["InPlant"].ToString());
                    strSQL2.AppendFormat("'{0}','{1}',GETDATE(),'{2}','{3}','{4}')", dtData.Rows[i]["BWART"].ToString(), UserData.UserId, strApplyNo, dtData.Rows[i]["TYPE"], COMCD);
                    arrSql.Add(strSQL2.ToString());
                }
                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlArray(arrSql);
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
                return flg;
            }


            #endregion
            #region 邮件发送失败，需手动再发送一次调用方法，传入调拨单号及部门代码
            public DataTable QuryTrITME(string applyno, string kostl)
            {
                this.ControlMethodName = "QuryTrHeader";
                this.ControlMethodParm = "(" + applyno + "," + kostl + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,KDMAT,MENGE,DWERKS,ULFLG,BWART,CRNAM,CRDAT,APPLYNO,TYPE,COMCD,'" + kostl + "'AS KOSTL from TRITEM WITH (NOLOCK) WHERE APPLYNO='" + applyno + "' ");
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtResult;
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

            #region 更新中间表PalletID/BOXID的状态
            public bool UpdatePalDetailData(string strWerks, string strLgort,string strLocat, string strMblnr,string strMessage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdatePalDetailData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool blresult = false;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE PAL_DETAIL SET  " );
                if(!string.IsNullOrEmpty(strWerks))
                    strSql.AppendFormat("WERKS='{0}', ",strWerks);
                if(!string.IsNullOrEmpty(strLgort))
                    strSql.AppendFormat("LGORT='{0}', ",strLgort);
                if (!string.IsNullOrEmpty(strLocat))
                    strSql.AppendFormat("LOCAT='{0}', ", strLocat);
                strSql.AppendFormat("Message='{0}',MODATE=GETDATE() ", strMessage);
                if (!string.IsNullOrEmpty(strMblnr))
                    strSql.AppendFormat("WHERE (PALLENT='{0}' OR  BOXID='{0}')", strMblnr);
                else
                    return false;
               
                try
                {

                    ControlHandleDB();
                    blresult = ControlSqlAccess.ExecSql(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return blresult;

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

            #region 根据BOXID更新料号数量
            /// <summary>
            /// 根据BOXID更新料号数量
            /// </summary>
            /// <param name="strPallet">PalletID</param>
            /// <param name="strBOXID">PalletID对应的BOXID</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="strCharg">版本</param>
            /// <param name="intMenge">修改后的数量</param>
            /// <param name="strMessage">备注</param>
            /// <returns></returns>
            public bool UpdatePalletBOXIDNum(string strPallet, string strBOXID,string strMatnr,string strCharg, int intMenge, string strMessage)
            {
                this.ControlMethodName = "UpdatePalletBOXIDNum";
                this.ControlMethodParm = "(" + strBOXID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                bool blResult = false;
                sbSQL.AppendFormat("UPDATE PAL_DETAIL SET MENGE='{0}',Remark=N'{1}'   WHERE MANDT='218' AND COMCD='{2}' AND PALLENT='{3}' and BOXID='{4}'  AND MATNR='{5}' AND CHARG='{6}' ", intMenge, strMessage, COMCD, strPallet, strBOXID, strMatnr, strCharg);

                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(sbSQL.ToString());
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

            #region 获取Pallet详细资料
            /// <summary>
            /// 获取Pallet详细资料
            /// </summary>
            /// <param name="varMblnr">Pallet或者BOXID</param>
            /// <returns></returns>
            public DataTable GetPalletDetail(string strWerks, string strLgort, string varMblnr, string strMatnr, string strLocat)
            {
                this.ControlMethodName = "GetPalletDetail";
                this.ControlMethodParm = "(" + varMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dt = new DataTable();
                sbSQL.AppendFormat("SELECT WERKS,LGORT,LOCAT,PALLENT,BOXID, MATNR,CHARG, MENGE,INSMK  FROM PAL_DETAIL WITH(NOLOCK) WHERE MANDT='218' AND COMCD='{0}' AND PALLENT='{1}' AND WERKS='{2}' AND LGORT='{3}' AND (LOCAT='{4}' OR LOCAT='')  AND MATNR='{5}' AND Message<>'Transfer'  ", COMCD, varMblnr, strWerks, strLgort, strLocat, strMatnr);

                try
                {
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
                return dt;
            }
            #endregion

            #region 检查podwn是否存在不同厂区
            public DataTable CheckDIffWerks(string strMblnr)
            {
                this.ControlMethodName = "query46Pdata";
                this.ControlMethodParm = "(" + strMblnr + "PODWN表)";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                sbSQL.AppendFormat("SELECT * FROM PODWN  WITH(NOLOCK)");
                sbSQL.AppendFormat("WHERE MBLNR='" + strMblnr + "' ");


                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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


        }
    }
}
