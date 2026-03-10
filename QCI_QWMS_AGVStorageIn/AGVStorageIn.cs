using System;
using System.Data;
using System.Collections;
using System.Text;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using QWMS.Entity;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// AGVStorageIn 的摘要描述。
        /// </summary>
        public class AGVStorageIn : ControlBase
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
            private string FileInPath = "";//產生文檔的路徑
            private string FileOutPath = "";//讀取文檔的路徑
            private string FileInPathQM = "";
            private string FileOutPathQM = "";
            private string strSAPUser = "";
            private string strPwd = "";

            #region Constructer

            #region 不傳入參數產生StorageIn物件
            public AGVStorageIn()
            {

            }

            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 不傳入任何參數產生StorageIn物件。
            /// </summary>
            /// <param name="varUserData"></param>
            public AGVStorageIn(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
            }
            #endregion

            #endregion

            #region 利用傳入參數產生StorageIn物件

            ////////////Summary by Rock Teng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.StorageIn物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public AGVStorageIn(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();


                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                PROGID = strProgid;
                CRNAM = varUserData.UserId;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.AGVStorageIn";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Rock Tzeng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;

                string FileInSql =
                    string.Format(
                        @"SELECT CTRLC1, ctrlc3,ctrlc4 FROM dbo.WHCTRL  WITH(NOLOCK) WHERE SOLDTO ='sap' AND CTRLID ='path' AND CTRLNM ='IN'");

                string FileOutSql =
                    string.Format(
                        @"SELECT CTRLC1 FROM dbo.WHCTRL  WITH(NOLOCK) WHERE SOLDTO ='sap' AND CTRLID ='path' AND CTRLNM ='OUT'");

                ControlHandleDB();
                DataTable dtPath = new DataTable();
                dtPath = ControlSqlAccess.GetDataTable(FileInSql);
                if (dtPath.Rows.Count > 0)
                {
                    strFileInPath = dtPath.Rows[0]["CTRLC1"].ToString();
                    strSAPUser = dtPath.Rows[0]["ctrlc3"].ToString();
                    strPwd = dtPath.Rows[0]["ctrlc4"].ToString();
                    strFileOutPath = ControlSqlAccess.GetFieldValue(FileOutSql);
                    strFileInPathQM = dtPath.Rows[0]["CTRLC1"].ToString();
                }

                strFileOutPathQM = ControlSqlAccess.GetFieldValue(FileOutSql);
                ControlSqlAccess.CloseConnection();
            }

            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.StorageIn物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public AGVStorageIn(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                DataTable dtTemp = new DataTable();
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(varUserData);
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
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

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.AGVStorageIn";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Rock Tzeng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;

                string FileInSql =
                    string.Format(
                        @"SELECT CTRLC1, ctrlc3,ctrlc4 FROM dbo.WHCTRL  WITH(NOLOCK) WHERE SOLDTO ='sap' AND CTRLID ='path' AND CTRLNM ='IN'");

                string FileOutSql =
                    string.Format(
                        @"SELECT CTRLC1 FROM dbo.WHCTRL  WITH(NOLOCK) WHERE SOLDTO ='sap' AND CTRLID ='path' AND CTRLNM ='OUT'");

                ControlHandleDB();
                DataTable dtPath = new DataTable();
                dtPath = ControlSqlAccess.GetDataTable(FileInSql);
                if (dtPath.Rows.Count > 0)
                {
                    strFileInPath = dtPath.Rows[0]["CTRLC1"].ToString();
                    strSAPUser = dtPath.Rows[0]["ctrlc3"].ToString();
                    strPwd = dtPath.Rows[0]["ctrlc4"].ToString();
                    strFileInPathQM = dtPath.Rows[0]["CTRLC1"].ToString();
                }

                strFileOutPath = ControlSqlAccess.GetFieldValue(FileOutSql);
                strFileOutPathQM = ControlSqlAccess.GetFieldValue(FileOutSql);

                ControlSqlAccess.CloseConnection();

            }

            public AGVStorageIn(UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort, strMblnr, strProgid)
            {
            }

            #endregion

            #region DataMember

            UserInfo UserData = new UserInfo();

            #region 變數
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

            public string strFileInPath
            {
                get { return FileInPath; }
                set { FileInPath = value; }
            }

            public string strFileOutPath
            {
                get { return FileOutPath; }
                set { FileOutPath = value; }
            }

            public string strFileInPathQM
            {
                get { return FileInPathQM; }
                set { FileInPathQM = value; }
            }

            public string strFileOutPathQM
            {
                get { return FileOutPathQM; }
                set { FileOutPathQM = value; }
            }
            #endregion

            #endregion

            #region MemberFunction

            #region 檢查使用者是否有使用入庫作業功能的權限
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用入庫作業功能的權限
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
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
                    if (objAuthority.INAUT.IndexOf(PROGID) < 0)
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
            #endregion

            #region 檢查使用者是否有使用管理作業功能的權限
            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用管理作業功能的權限
            /// </summary> 
            /// <param name="strType">參數。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageIn.CheckAuthority(strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckAuthority(string strType)
            {
                Authority objAuthority = new Authority(UserData);
                if (strType.ToUpper() == "MANAGE")
                {
                    if (objAuthority.MGAUT.IndexOf(PROGID) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if (objAuthority.INAUT.IndexOf(PROGID) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            #endregion

            #region  检查使用者是否有D/C维护权限
            /// <summary>
            /// 检查使用者是否有D/C维护权限
            /// </summary>
            /// <param name="strUSERNM"></param>
            /// <returns></returns>
            public bool CheckDatecode(string strUSERNM)
            {
                string strSQL = "";
                bool bolChecked = false;
                DataTable dtData = new DataTable();

                strSQL = "SELECT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE CTRLID='D/C_ADMIN'AND CTRLNM='D/C'  AND CTRLC1='" + strUSERNM + "'";
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtData.Rows.Count == 0)
                    {
                        bolChecked = false;
                    }
                    else
                    {
                        bolChecked = true;
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
                return bolChecked;


            }

            #endregion

            #region 仓别类型
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

            #region AGV 智能收料

            #region 获取EC单状态
            public DataTable GetPacingStatus(string ECNo)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPacingStatus";
                this.ControlMethodParm = "(" + ECNo + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();

                strsql.AppendFormat("select STATUS from EC_HEAD where PNUM='{0}'", ECNo);

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

            #region 扣帐资料
            public DataTable GetTAB_ZM000(string ECNo, string werks, string lgort, string locat, string box)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDataExistsInStorage";
                this.ControlMethodParm = "(" + ECNo + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable TAB_ZM000 = new DataTable();
                TAB_ZM000.TableName = "TAB_ZM000";
                //DataTable TAB_ZM001 = new DataTable();
                //TAB_ZM001.TableName = "TAB_ZM001";
                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("SELECT DISTINCT H.MANDT,H.PNUM,H.LIFNR,H.WERKS,I.CURREN AS WAERS,H.ZCARNO,H.ZCARNO1,H.SHIPR,H.INCO1,H.ZPORT,H.FORDE,H.BUDAT,H.LOSTAX,"
                + "  H.PLSTAT,H.BKTXT,H.PLTYPE,H.HBTYPE,H.COUNTRY,H.UDCODE,H.SYSDAT,H.SYSTIM,H.INDIC1,H.INDIC2,H.INDIC3,H.STEXT1,H.STEXT2,H.LTEXT1,H.LTEXT2 "
                + "  FROM EC_HEAD AS H WITH(NOLOCK) INNER JOIN EC_ITEM AS I WITH(NOLOCK) ON H.PNUM=I.PNUM WHERE H.PNUM='{0}'", ECNo);


                //StringBuilder strsql01 = new StringBuilder();
                //  strsql.AppendFormat(" SELECT I.MANDT,I.PNUM,I.PITEM,I.EBELN,I.EBELP,H.PLSTAT as PLSTAT,I.MATNR,I.CHARG1,H.LIFNR,I.MENGE,I.MENGEC,I.MEINS,'{0}' AS WERKS,'{1}' AS LGORT,H.BUDAT,'{2}' AS SGTXT,ECODE,GDREC,I.BKTXT,BELNR,BUZEI,GJAHR,ERDAT,DCLNO,DFLAG,TFLAG,DWERKS FROM EC_HEAD AS H INNER JOIN EC_ITEM AS I ON H.PNUM=I.PNUM WHERE H.PNUM='{3}'", werks, lgort, locat, ECNo);



                DataSet dtData = new DataSet();
                try
                {

                    ControlHandleDB();
                    TAB_ZM000 = ControlSqlAccess.GetDataTable(strsql.ToString());
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

                return TAB_ZM000;
            }

            #endregion

            public bool updateZM025(ArrayList varsqlarr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "createDtResponse";
                this.ControlMethodParm = "(" + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flage = false;
                try
                {

                    ControlHandleDB();
                    flage = ControlSqlAccess.ExecSqlArray(varsqlarr);
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
                return flage;
            }

            #region 获取EC单表头

            /// <summary>
            /// 获取EC单表头
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="ECNO">EC单号</param>
            /// <param name="strBOXID">BOXID</param>
            /// <param name="strFromData">查询开始时间</param>
            /// <param name="strToData">查询结束时间</param>
            /// <returns></returns>
            public DataTable GetECHead(string strWerks, string ECNO, string strBOXID, string strFromDate, string strToDate, string strStatus, string strUser)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetECHead";
                this.ControlMethodParm = "(" + ECNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("select WERKS,PNUM,LIFNR,BKTXT,CASE CLRTYP WHEN 'A' THEN N'逐票报关' ELSE N'汇总报关'END AS CLRNAM,CUSTID,STATUS,BOXID from EC_HEAD WITH(NOLOCK) WHERE MANDT='{0}' ", MANDT);
                if (!string.IsNullOrEmpty(strWerks))
                {
                    strsql.AppendFormat("AND WERKS='{0}'", strWerks);
                }
                if (!string.IsNullOrEmpty(ECNO))
                {
                    strsql.AppendFormat(" AND PNUM='{0}' ", ECNO);
                }
                if (!string.IsNullOrEmpty(strStatus))
                {
                    strsql.AppendFormat(" AND STATUS='{0}' ", strStatus);
                }
                if (!string.IsNullOrEmpty(strBOXID)) //取出正在处理中的EC单数据
                {
                    strsql.AppendFormat(" AND BOXID='{0}'  ", strBOXID);
                }
                if (!string.IsNullOrEmpty(strUser))
                {
                    strsql.AppendFormat(" AND USERNM='{0}'", strUser);
                }
                if (!string.IsNullOrEmpty(strFromDate) && !string.IsNullOrEmpty(strToDate))
                {
                    strsql.AppendFormat("AND UPTIME BETWEEN DATEADD(DAY,0,'{0}') AND DATEADD(DAY,+1,'{1}')", strFromDate, strToDate);
                }
                strsql.Append("ORDER BY UPTIME DESC");
                DataTable dtResult = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strsql.ToString());
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

            #region 获取EC单表头 QCMC

            /// <summary>
            /// 获取EC单表头
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="ECNO">EC单号</param>
            /// <param name="strBOXID">BOXID</param>
            /// <param name="strFromData">查询开始时间</param>
            /// <param name="strToData">查询结束时间</param>
            /// <returns></returns>
            public DataTable GetECHead(string strWerks, string ECNO)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetECHead";
                this.ControlMethodParm = "(" + ECNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("SELECT I.PITEM,I.MATNR,I.CHARG1,I.MENGE,H.lgort,H.BKTXT AS  BKTXT1,H.CLRTYP,CASE H.CLRTYP WHEN 'A' THEN N'逐票报关' ELSE N'汇总报关' END AS CLRNAM ,H.CUSTID  FROM  EC_ITEM AS I WITH(NOLOCK) INNER JOIN EC_HEAD AS H WITH(NOLOCK) ON I.PNUM=H.PNUM WHERE H.PNUM='{0}'", ECNO);
                if (!string.IsNullOrEmpty(strWerks))
                {
                    strsql.AppendFormat("AND H.WERKS='{0}'", strWerks);
                }
                strsql.AppendFormat("UNION ALL SELECT '' AS PITEM,'' AS MATNR,'' AS CHARG1,SUM(MENGE) AS MENGE,'' AS lgort,'' AS  BKTXT1,'' AS CLRTYP,'' AS CLRNAM,'' AS CUSTID FROM  EC_ITEM AS I WITH(NOLOCK) INNER JOIN EC_HEAD AS H WITH(NOLOCK) ON I.PNUM=H.PNUM WHERE H.PNUM='{0}' ", ECNO);
                if (!string.IsNullOrEmpty(strWerks))
                {
                    strsql.AppendFormat("AND H.WERKS='{0}'", strWerks);
                }
                DataTable dtResult = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strsql.ToString());
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

            #region 获取EC单表体信息
            /// <summary>
            /// 获取EC单表体信息
            /// </summary>
            /// <param name="ECNO"></param>
            /// <returns></returns>
            public DataTable GetECItem(string ECNO)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetECItem";
                this.ControlMethodParm = "(" + ECNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("select  PNUM,PITEM,MATNR,LIFNR,CHARG1,MENGE,MENGEC, LGORT,KOSTL,AEC,CHECKSTATS FROM EC_ITEM WITH(NOLOCK)  WHERE MANDT='{0}' AND PNUM='{1}'",MANDT, ECNO);
                DataTable dtResult = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strsql.ToString());
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

            #region 从QEC端获取EC单表头和表体信息
            /// <summary>
            /// 从QEC端获取EC单表头和表体信息
            /// </summary>
            /// <param name="ECNO"></param>
            /// <returns></returns>
            public bool GetECFromQEC(string ECNO)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetECFromQEC";
                this.ControlMethodParm = "(" + ECNO + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                ClaHttpHelper claheepHelper = new ClaHttpHelper();
                StringBuilder strsql = new StringBuilder();
                bool blResult = false;
                var Data = new
                {
                    PNUM = ECNO,
                    USERNM = UserData.UserId.ToString().Trim(),
                };
                string strData = JsonConvert.SerializeObject(Data);

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //域名配置在数据库中
                string strUrl = objAuthority.GetApiRequestUrl();
                strUrl = strUrl + "/QWMSAPI/api/QsmcQwms/GetECDataFromQEC";

                //旧：固定写死
                //string strUrl = "http://172.19.81.219/AlimAPI/api/QsmcQwms/GetECDataFromQEC";
                string strResult = claheepHelper.HttpPostByHttpWebRequest(strUrl, strData);
                string str = strResult.Replace("null", "\"\"");
                strsql.AppendFormat("EXEC sp_GetECDataFromQSMCQEC '{0}'", str);

                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql.ToString());
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

            #region 更新EC单状态
            /// <summary>
            /// 更新EC单状态
            /// </summary>
            /// <param name="strPnum"></param>
            /// <param name="strStatus"></param>
            /// <returns></returns>
            public bool updateECstatus(string strPnum, string strStatus, string strBoxID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateECstatus";
                this.ControlMethodParm = "(" + strPnum + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = string.Empty;
                if (string.IsNullOrEmpty(strBoxID))
                {
                    strsql = "UPDATE EC_HEAD SET STATUS='" + strStatus + "' WHERE PNUM='" + strPnum + "'";
                }
                else if (string.IsNullOrEmpty(strStatus))
                {
                    strsql = "UPDATE EC_HEAD SET BOXID='" + strBoxID + "' WHERE PNUM='" + strPnum + "'";
                }
                else
                {
                    strsql = "UPDATE EC_HEAD SET STATUS='" + strStatus + "',BOXID='" + strBoxID + "' WHERE PNUM='" + strPnum + "'";
                }
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 获取刷入的BOXID信息
            public DataTable getECBoxid(string strBoxid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getECBoxid";
                this.ControlMethodParm = "(" + strBoxid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                string strsql = "SELECT WERKS,LGORT,BOXID,BOXITEM,MATNR,LOCAT,MENGE,OTQTY,LIFNR,DACOD,LOCOD,VEDAT,REMAK,SERNO FROM MATBOX WITH(NOLOCK) WHERE BOXID='" + strBoxid + "'";
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strsql);
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

            #region 更新ECItem的信息
            public bool updateECItem(DataRow[] drECitem)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateECItem";
                this.ControlMethodParm = "(" + drECitem + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                ArrayList arrySql = new ArrayList();
                foreach (DataRow dr in drECitem)
                {
                    arrySql.Add("UPDATE EC_ITEM SET LGORT='" + dr["LGORT"].ToString() + "',LOCAT='" + dr["LOCAT"].ToString() + "',MENGEC='" + dr["MENGEC"].ToString() + "' WHERE MANDT='" + MANDT + "' AND PNUM='" + dr["PNUM"].ToString() + "' AND PITEM='" + dr["PITEM"].ToString() + "'");
                }
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSqlArray(arrySql);
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

            #region 问题单据更新ECItem的信息
            public bool updateTECItem(DataTable dtECitem)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateECItem";
                this.ControlMethodParm = "(" + dtECitem + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                ArrayList arrySql = new ArrayList();
                foreach (DataRow dr in dtECitem.Rows)
                {
                    arrySql.Add("UPDATE EC_ITEM SET LGORT='" + dr["LGORT"].ToString() + "' WHERE MANDT='" + MANDT + "' AND PNUM='" + dr["PNUM"].ToString() + "' AND PITEM='" + dr["PITEM"].ToString() + "'");
                }
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSqlArray(arrySql);
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

            #region 刷Boxid
            /// <summary>
            /// 刷Boxid
            /// </summary>
            /// <param name="ECNo"></param>
            /// <param name="ECItem"></param>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strBOXID"></param>
            /// <param name="strMatnr"></param>
            /// <param name="intMenge"></param>
            /// <param name="strLifnr"></param>
            /// <param name="strDacod"></param>
            /// <param name="strLotCode"></param>
            /// <param name="strLocat"></param>
            /// <param name="strVedat"></param>
            /// <param name="strBoxItem"></param>
            /// <returns></returns>
            public bool ScanBoxid_NEW(string strWerks, string strLgort, string strBOXID, string strMatnr, int intMenge, string strLifnr, string strDacod, string strLotCode, string strLocat, string strVedat, string strBoxItem, string strUniqueId)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ScanBoxid";
                this.ControlMethodParm = "(" + strBOXID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat(" INSERT INTO MATBOX(WERKS,LGORT,BOXID, MATNR,LOCAT,MENGE,LIFNR,DACOD,LOCOD,VEDAT,CRETIME,CREWHO,BOXITEM,SERNO) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',GETDATE(),'{10}','{11}','{12}')",
                    strWerks, strLgort, strBOXID, strMatnr, strLocat, intMenge, strLifnr, strDacod, strLotCode, strVedat,
                    UserData.UserId.ToString(), strBoxItem, strUniqueId);

                //strsql.AppendFormat("SELECT * FROM MATBOX where PNUM='{0}'", ECNo);

                try
                {
                    ControlHandleDB();

                    blResult = ControlSqlAccess.ExecSql(strsql.ToString());
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

            public DataTable GetTAB_ZM001_NEW(string ECNo, string werks, string lgort, string box)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDataExistsInStorage";
                this.ControlMethodParm = "(" + ECNo + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //DataTable TAB_ZM000 = new DataTable();
                //TAB_ZM000.TableName = "TAB_ZM000";
                DataTable TAB_ZM001 = new DataTable();
                TAB_ZM001.TableName = "TAB_ZM001";
                StringBuilder strsql = new StringBuilder();

                strsql.AppendFormat("SELECT I.MANDT,I.PNUM,I.PITEM,I.EBELN,I.EBELP,H.PLSTAT as PLSTAT,I.MATNR,I.CHARG1,H.LIFNR,I.MENGE,0 AS MENGEC,I.MEINS,H.WERKS AS WERKS,I.LGORT AS LGORT,H.BUDAT,I.LOCAT AS SGTXT,ECODE,GDREC,H.BKTXT,BELNR,BUZEI,GJAHR,ERDAT,DCLNO,DFLAG,TFLAG,DWERKS FROM EC_HEAD AS H WITH(NOLOCK) INNER JOIN EC_ITEM AS I WITH(NOLOCK) ON H.PNUM=I.PNUM WHERE H.PNUM='{2}'", werks, lgort, ECNo);

                DataSet dtData = new DataSet();
                try
                {

                    ControlHandleDB();
                    TAB_ZM001 = ControlSqlAccess.GetDataTable(strsql.ToString());
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

                return TAB_ZM001;
            }

            #region 回退dtLotCode到MATBOX表中
            /// <summary>
            /// 回退dtLotCode到MATBOX表中
            /// </summary>
            /// <param name="dtLotCode"></param>
            /// <returns></returns>
            public bool backMatbox(DataTable dtLotCode)
            {
                this.ControlMethodName = "backMatbox";
                this.ControlMethodParm = "('" + dtLotCode + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                DataMatbox objMatbox = new DataMatbox(UserData);
                DataTable dtResult = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList alConditions = new ArrayList();
                bool blResult = false;
                foreach (DataRow drBoxID in dtLotCode.Rows)
                {
                    alConditions.Clear();
                    objMatbox.ResetField();
                    objMatbox.Otqty = ((objMatbox.Otqty == null ? 0 : int.Parse(objMatbox.Otqty)) + int.Parse(drBoxID["OTQTY"].ToString())).ToString();
                    objMatbox.Lgort = drBoxID["LGORT"].ToString();
                    objMatbox.Remak = drBoxID["REMAK"].ToString();
                    alConditions.Add(" WERKS='" + drBoxID["WERKS"].ToString() + "'");
                    alConditions.Add(" BOXID='" + drBoxID["BOXID"].ToString() + "'");
                    alConditions.Add(" BOXITEM='" + drBoxID["BOXITEM"].ToString() + "'");
                    alConditions.Add(" MATNR='" + drBoxID["MATNR"].ToString() + "'");
                    alConditions.Add(" LIFNR='" + drBoxID["LIFNR"].ToString() + "'");
                    alConditions.Add(" LOCAT='" + drBoxID["LOCAT"].ToString() + "'");
                    arySQL.Add(objMatbox.EntityGetUpdateSql(alConditions));
                }
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
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

            #region 将EC入库数据存放到表EC_INSTOCK_AGV
            /// <summary>
            /// 将EC入库数据存放到表EC_INSTOCK_AGV
            /// </summary>
            /// <param name="dtStorage"></param>
            /// <returns></returns>
            public bool ECinStock(DataTable dtStorage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ECinStock";
                this.ControlMethodParm = "(" + dtStorage + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool blResult;

                try
                {
                    ControlHandleDB();

                    blResult = ControlSqlAccess.ExecSqlBulkCopy("EC_INSTOCK_AGV", dtStorage);
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

            #region 删除EC单的入库数据
            /// <summary>
            /// 删除EC单的入库数据
            /// </summary>
            /// <param name="strPnum"></param>
            /// <returns></returns>
            public bool DeleteECInStore(string strPnum)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteECInStore";
                this.ControlMethodParm = "(" + strPnum + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = "DELETE EC_INSTOCK_AGV WHERE PNUM='" + strPnum + "'";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 更新EC单的明细匹配数据
            /// <summary>
            /// 更新EC单的明细匹配数据
            /// </summary>
            /// <param name="strPnum"></param>
            /// <returns></returns>
            public bool UpdateECItemMengec(string strPnum)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateECItemMengec";
                this.ControlMethodParm = "(" + strPnum + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = "UPDATE EC_ITEM SET MENGEC=0 WHERE MANDT='" + MANDT + "' AND PNUM='" + strPnum + "'";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 更新EC单MATBOX的刷入数据
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strBoxID"></param>
            /// <returns></returns>
            public bool UpdateMatbox(string strBoxID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateMatbox";
                this.ControlMethodParm = "(" + strBoxID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = "UPDATE MATBOX SET OTQTY=0,REMAK='' WHERE BOXID='" + strBoxID + "'";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 判断MATBOX中是否已存在同一料号，同一储位
            /// <summary>
            /// 判断MATBOX中是否已存在同一料号，同一储位
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strBoxID"></param>
            /// <param name="strLocat"></param>
            /// <param name="strMatnr"></param>
            /// <param name="strVendorcode"></param>
            /// <param name="strDCbefore"></param>
            /// <returns></returns>
            public bool checkMatbox(string strWerks, string strBoxID, string strLocat, string strMatnr, string strVendorcode, string strDCbefore, string strUniqueId)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "checkMatbox";
                this.ControlMethodParm = "(" + strBoxID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                string strsql = "SELECT COUNT(BOXID) FROM MATBOX WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND BOXID='" + strBoxID + "'  AND MATNR='" + strMatnr + "' AND LOCAT='" + strLocat + "'  AND LIFNR='" + strVendorcode + "' AND DACOD='" + strDCbefore + "'AND SERNO='" + strUniqueId + "'";
                try
                {
                    ControlHandleDB();
                    string strResult = ControlSqlAccess.GetFieldValue(strsql);
                    if (int.Parse(strResult) > 0)
                    {
                        blResult = true;
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
                return blResult;
            }
            #endregion

            #region 更新MATBOX表中的料号数量
            /// <summary>
            /// 更新MATBOX表中的料号数量
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strBoxID"></param>
            /// <param name="strLocat"></param>
            /// <param name="strMatnr"></param>
            /// <param name="intMenge"></param>
            /// <param name="strVendorcode"></param>
            /// <param name="strDCbefore"></param>
            /// <returns></returns>
            public bool updateMatbox(string strWerks, string strBoxID, string strLocat, string strMatnr, int intMenge, string strVendorcode, string strDCbefore, string strUniqueId)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "checkMatbox";
                this.ControlMethodParm = "(" + strBoxID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = "UPDATE MATBOX SET MENGE=MENGE+'" + intMenge + "' WHERE WERKS='" + strWerks + "' AND BOXID='" + strBoxID + "' AND  MATNR='" + strMatnr + "' AND LOCAT='" + strLocat + "'   AND LIFNR='" + strVendorcode + "' AND DACOD='" + strDCbefore + "' AND DACOD='" + strUniqueId + "'";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 获取EC单的操作人
            /// <summary>
            /// 获取EC单的操作人
            /// </summary>
            /// <param name="strEC">EC单号</param>
            /// <param name="strBoxID">BOXID</param>
            /// <returns></returns>
            public string getECUser(string strEC, string strBoxID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getECUser";
                this.ControlMethodParm = "(" + strEC + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strUser = string.Empty;
                string strSql = string.Empty;
                if (!string.IsNullOrEmpty(strEC))
                {
                    strSql = "SELECT USERNM FROM EC_HEAD WITH(NOLOCK)  WHERE PNUM='" + strEC + "'";
                }
                if (!string.IsNullOrEmpty(strBoxID))
                {
                    strSql = "SELECT USERNM FROM EC_HEAD WITH(NOLOCK)  WHERE BOXID='" + strBoxID + "'";
                }
                try
                {
                    ControlHandleDB();
                    strUser = ControlSqlAccess.GetFieldValue(strSql);
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
                return strUser;
            }
            #endregion

            #region 更新问题单MATBOX数量
            /// <summary>
            /// 更新问题单MATBOX数量
            /// </summary>
            /// <param name="strBoxID">BOXID信息</param>
            /// <param name="strBoxItem">BOXITEM信息</param>
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="intMenge">待处理数量</param>
            /// <param name="strVendorcode">厂商代码</param>
            /// <returns></returns>
            public bool updateTMatbox(string strBoxID, string strBoxItem, string strLocat, string strMatnr, int intMenge, string strVendorcode)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateTMatbox";
                this.ControlMethodParm = "(" + strBoxID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = string.Empty;
                strsql = "UPDATE MATBOX SET LOCAT='" + strLocat + "',MENGE=OTQTY+'" + intMenge + "' WHERE BOXID='" + strBoxID + "' AND BOXITEM='" + strBoxItem + "'  AND MATNR='" + strMatnr + "' AND LIFNR='" + strVendorcode + "'";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 获取BOXID数据最大的item
            /// <summary>
            /// 获取BOXID数据最大的item
            /// </summary>
            /// <param name="strBoxid">BOXID信息</param>
            /// <returns></returns>
            public string getBoxMaxItem(string strBoxid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getBoxMaxItem";
                this.ControlMethodParm = "(" + strBoxid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strResult = string.Empty;
                string strsql = "SELECT MAX(BOXITEM) FROM MATBOX WITH(NOLOCK) WHERE BOXID='" + strBoxid + "'";
                try
                {
                    ControlHandleDB();
                    strResult = ControlSqlAccess.GetFieldValue(strsql);
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

            #region 获取BOXID的数据总量
            /// <summary>
            /// 获取BOXID的数据总量
            /// </summary>
            /// <param name="dtBoxID">BOXID信息</param>
            /// <returns></returns>
            public string GetBoxSumMenge(DataTable dtBoxID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getBoxSumMenge";
                this.ControlMethodParm = "(" + dtBoxID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                int sum = 0;
                foreach (DataRow dr in dtBoxID.Rows)
                {
                    sum += int.Parse(dr["MENGE"].ToString());
                }
                //string strsql = "SELECT SUM(MENGE) AS MENGE FROM MATBOX WITH(NOLOCK) WHERE BOXID='" + strBoxid + "'";

                return sum.ToString();
            }
            #endregion

            #region 获取EC单的待入库数据
            /// <summary>
            /// 获取EC单的待入库数据
            /// </summary>
            /// <param name="strPnum">EC单号</param>
            /// <returns></returns>
            public DataTable GetEcInStock(string strPnum)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strPnum";
                this.ControlMethodParm = "(" + strPnum + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strsql = "SELECT MANDT,COMCD,WERKS,LGORT,PNUM,MBLNR,(select top 1 PONUM from WHTWW where  MBLNR = '" + strPnum + "'and IEFLG = 'E') as Ombln,MATNR,CHARG,INSMK,LOCAT,MENGE,ALQTY,LIFNR,DACOD,LOCOD,VEDAT,INDAT,RMAK1,MRGID,ARBPL,KOSTL,KDMAT,SERNO FROM EC_INSTOCK_AGV WITH(NOLOCK) WHERE PNUM = '" + strPnum + "' AND ALQTY> 0";

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strsql);
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

            #region 更新EC单的待入库数据
            /// <summary>
            /// 更新EC单的待入库数据
            /// </summary>
            /// <param name="strPnum">EC单号</param>
            /// <returns></returns>
            public bool UpdateEcInStock(string strPnum)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strPnum";
                this.ControlMethodParm = "(" + strPnum + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                string strsql = "UPDATE EC_INSTOCK_AGV SET ALQTY=0  WHERE PNUM='" + strPnum + "' ";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 入库QWMS WHITM WHLOG  WHHED dtStorage的厂区仓别可能不同
            public bool StorageInWHEC(DataTable dtStorage)
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

                #region WHLOG
                arySQL = objLogData.AddLogData("", "", dtStorage);

                #endregion

                #region WHITM

                string strSQL = "";
                string strTempLocat = "";
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);

                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    //if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    //{
                    //    aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                    //    strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                    //}
                    if (!objStorageData.QueryDataExistsInStorage(dtStorage.Rows[i]["WERKS"].ToString(), dtStorage.Rows[i]["LGORT"].ToString(), dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["MBLNR"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), "", "", ""))
                    {
                        #region 不存在库存
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();

                        objWhitm.Mandt = MANDT;
                        objWhitm.Comcd = COMCD;
                        objWhitm.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhitm.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhitm.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhitm.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhitm.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhitm.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhitm.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhitm.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhitm.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhitm.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhitm.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhitm.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhitm.Qcqty = "0";
                        objWhitm.Refno = "";
                        objWhitm.Mrgid = "";
                        objWhitm.Isptm = "";
                        objWhitm.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Crnam = CRNAM;
                        objWhitm.Crdat = "GetDate()";
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = "GetDate()";
                        objWhitm.Boxid = "";
                        objWhitm.STOCSTATES = "Y";
                        objWhitm.ITEMSTATES = "Y";

                        if (CheckStorageInType(strWerks, strLgort, "StorageInLock"))
                        {
                            if (GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows.Count > 0)
                            {
                                objWhitm.ExpiryDate = GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString();
                                if (Convert.ToInt32(GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                {
                                    objWhitm.LOCKED = "Y";
                                    objWhitm.LKDAT = "GetDate()";
                                }
                            }
                            else
                            {
                                objWhitm.LOCKED = "Y";
                                objWhitm.LKDAT = "GetDate()";

                            }
                        }

                        if (dtStorage.Columns.IndexOf("KDMAT") != -1)
                        {
                            objWhitm.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                            objWhitm.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                        }

                        strSQL = objWhitm.EntityGetInsertSql();
                        arySQL.Add(strSQL);
                        #endregion
                    }
                    else
                    {
                        #region 已有库存
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();

                        objWhitm.Menge = "MENGE +" + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = "GetDate()";
                        alConditions.Add("Mandt= '" + MANDT + "'");
                        alConditions.Add("Comcd= '" + COMCD + "'");
                        alConditions.Add("Werks= '" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add("Lgort= '" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add("Locat= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add("Matnr= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add("Insmk= '" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add("Mblnr= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add("Charg= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        if (dtStorage.Columns.IndexOf("KDMAT") != -1)
                        {
                            alConditions.Add("Serno= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                        }
                        strSQL = objWhitm.EntityGetUpdateSql(alConditions);
                        arySQL.Add(strSQL);

                        #endregion
                    }
                }
                #endregion

                #region  WHHED
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["WERKS"].ToString() + dtStorage.Rows[i]["LGORT"].ToString() + dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        sbSql.Remove(0, sbSql.Length);
                        sbSql.Append("Update WHHED set  ");
                        sbSql.AppendFormat("  LOSTS=T.TOTAL from ");
                        sbSql.AppendFormat(" (Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH (NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "') as T  ");
                        sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat(" and WERKS='{0}' ", dtStorage.Rows[i]["WERKS"].ToString());
                        sbSql.AppendFormat(" and LGORT='{0}' ", dtStorage.Rows[i]["LGORT"].ToString());
                        sbSql.AppendFormat(" and LOCAT='{0}' ", dtStorage.Rows[i]["LOCAT"].ToString());
                        arySQL.Add(sbSql.ToString());

                        //sbSql.Remove(0, sbSql.Length);
                        //sbSql.Append("Update WHHED set  ");
                        //sbSql.AppendFormat("  ISMRG=T.ISMRG from ");
                        //sbSql.AppendFormat(" (Select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH (NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MRGID<>'') as T  ");
                        //sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                        //sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                        //sbSql.AppendFormat(" and WERKS='{0}' ", dtStorage.Rows[i]["WERKS"].ToString());
                        //sbSql.AppendFormat(" and LGORT='{0}' ", dtStorage.Rows[i]["LGORT"].ToString());
                        //sbSql.AppendFormat(" and LOCAT='{0}' ", dtStorage.Rows[i]["LOCAT"].ToString());
                        //arySQL.Add(sbSql.ToString());

                        strTempLocat += "++" + dtStorage.Rows[i]["WERKS"].ToString() + dtStorage.Rows[i]["LGORT"].ToString() + dtStorage.Rows[i]["LOCAT"].ToString();

                    }
                }
                #endregion

                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300;  //連線SQL Server的時間拉長到5分鐘
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


            #region 获取工作站

            public DataTable CheckWorkStation()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strPnum";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strsql = "SELECT '' AS F_TEXT UNION ALL SELECT CTRLNM AS F_TEXT FROM WHCTRL WHERE CTRLID='WORKSTATION' and COMCD = '9200'";

                try
                {
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strsql);
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

            #region 更新储位
            public bool UpdateLocation(string strWERKS, string strLGORT, string strLocation)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateLocation";
                this.ControlMethodParm = "(" + strLocation + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                string strsql = "Update WHHED set LOSTS = '1' where WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "' and LOCAT = '" + strLocation + "' ";
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 获取储位数据
            public DataTable GetWhHedLOCAT(string strWERKS, string strLGORT, string strLocation)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strLocation";
                this.ControlMethodParm = "(" + strLocation + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strsql = "SELECT LOCAT FROM WHHED WITH(NOLOCK) WHERE WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "' and LOCAT like '" + strLocation + "%' ";

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strsql);
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

            #region 校验更新储位
            public bool CheckLocation(string strWERKS, string strLGORT, string strLocation)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateLocation";
                this.ControlMethodParm = "(" + strLocation + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;

                string strsql = "";

                if (GetWhItmStock(strWERKS, strLGORT, strLocation).Rows.Count == 0)
                {
                    strsql = "Update WHHED set LOSTS = '0' where WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "' and LOCAT = '" + strLocation + "' ";
                }
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strsql);
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

            #region 获取库存储位数据
            public DataTable GetWhItmStock(string strWERKS, string strLGORT, string strLocation)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "strLocation";
                this.ControlMethodParm = "(" + strLocation + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                string strsql = "Select LOCAT from whitm WITH(NOLOCK) where WERKS = '"+ strWERKS +"' and LGORT = '" + strLGORT + "'and LOCAT = '" + strLocation +"'";

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strsql);
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

            #region 获取虚拟储位
            public string getLocation(string strWERKS, string strLGORT, string strLocation)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getLocation";
                this.ControlMethodParm = "(" + strLocation + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strSql = string.Empty;

                strSql = "SELECT TOP 1 LOCAT FROM WHHED WITH(NOLOCK) WHERE LOSTS = '0' and  WERKS = '" + strWERKS + "' and LGORT = '" + strLGORT + "' and LOCAT like '" + strLocation + "%' ";

                try
                {
                    ControlHandleDB();
                    strLocation = ControlSqlAccess.GetFieldValue(strSql);
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
                return strLocation;
            }
            #endregion

            #endregion

            #region 联机(转仓)入库_AGV Add by Jason 20240123

            #region 获取工作站信息
            public DataTable GetAGVWorkStation()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAGVWorkStation";
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
                    alColumns.Add(" CTRLNM as F_TEXT ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='WorkStation'");
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

            #region 查询工作站状态
            public DataTable QueryAGVWorkStation(string strWerks, string strLgort, string strPlace, string strComcd, string strTASKID, int strreqCode)
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
                    strSQL.AppendFormat("SELECT * FROM WHAGV WITH(NOLOCK) WHERE PLACE='{0}' ", strPlace);
                    if (!string.IsNullOrEmpty(strWerks))
                    {
                        strSQL.AppendFormat(" AND WERKS='{0}' ", strWerks);
                    }
                    if (!string.IsNullOrEmpty(strLgort))
                    {
                        strSQL.AppendFormat(" AND LGORT='{0}' ", strLgort);
                    }
                    if (!string.IsNullOrEmpty(strComcd))
                    {
                        strSQL.AppendFormat(" AND COMCD='{0}' ", strComcd);
                    }
                    if (!string.IsNullOrEmpty(strTASKID))
                    {
                        strSQL.AppendFormat(" AND TASKID='{0}' ", strTASKID);
                    }
                    if (strreqCode != 0)
                    {
                        strSQL.AppendFormat(" AND REQNO='{0}' ", strreqCode);
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

            #region 查询货架储位状态
            public DataTable QueryAGVWHHED(string strWerks, string strLGORT, string strLOCAT, string strComcd, string strMARNO, string strLOCTYPE, string strShelfsize)
            {
                this.ControlMethodName = "QueryAGVWHHED";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT * FROM WHHED WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND LOCAT like '{2}%' AND COMCD='{3}' AND MARNO='{4}' AND LOCTYPE='{5}' ", strWerks, strLGORT, strLOCAT, strComcd, strMARNO, strLOCTYPE);
                    if (strShelfsize == "7")
                    {
                        strSQL.AppendFormat(" AND LOSTS='0' ");
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

            #region 查询货架储位状态
            public DataTable QueryWHHED(string strWerks, string strLGORT, string strLOCAT, string strComcd)
            {
                this.ControlMethodName = "QueryAGVWHHED";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT * FROM WHHED WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND LOCAT like '{2}%' AND COMCD='{3}' ", strWerks, strLGORT, strLOCAT, strComcd);

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


            #region 产生AGV任务单号
            public int GetAGVTaskNo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAGVTaskNo";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                int strSerno = 0;
                string strSQL = "";
                try
                {
                    strSQL = "EXEC SP_GetSerialNum_NEW 'AGV', 'TASKNO'";
                    ControlHandleDB();
                    strSerno = Convert.ToInt32(ControlSqlAccess.GetFieldValue(strSQL));
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
                return strSerno;
            }
            #endregion

            #region 创建AGV指令到WHAGV，锁定扣账单据WHDWN
            public bool InsertWHAGV(string strWERKS, string strLGORT, string strPLACE, string strTASKID, int strREQNO, string strMARNO, string strSTATE, DataTable dtDocumentInfo)
            {
                this.ControlMethodName = "InsertWHAGV";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    bool bolreslt = false;

                    #region Insert WHAGV Table
                    sbSql.Append("INSERT INTO WHAGV(MANDT,WERKS,LGORT,PLACE,TASKID,REQNO,MARNO,Shelf_state,Light_status,CRNAM,CRDAT,COMCD) ");
                    sbSql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','','{8}',GETDATE(),'{9}') ", MANDT, strWERKS, strLGORT, strPLACE, strTASKID, strREQNO, strMARNO, strSTATE, UserData.UserId, strComcd);
                    arySQL.Add(sbSql.ToString());
                    #endregion

                    #region Update WHDWN Table
                    //如果dtDocumentInfo大于0，则选择的是先单据，后扫描实物
                    if (dtDocumentInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDocumentInfo.Rows.Count; i++)
                        {
                            string sql = "Update WHDWN set OTQTY=MENGE " +
                        " where WERKS='" + dtDocumentInfo.Rows[i]["WERKS"].ToString() + "' " +
                        " And LGORT='" + dtDocumentInfo.Rows[i]["LGORT"].ToString() + "' " +
                        " And MBLNR='" + dtDocumentInfo.Rows[i]["MBLNR"].ToString() + "' " +
                        " And MATNR='" + dtDocumentInfo.Rows[i]["MATNR"].ToString() + "' " +
                        " And MENGE='" + dtDocumentInfo.Rows[i]["MENGE"].ToString() + "' ";
                            arySQL.Add(sql.ToString());
                        }
                    }
                    #endregion

                    ControlHandleDB();
                    bolreslt = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolreslt;
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

            #region 锁定绑定的WHDWN表单据号 OTQTY -> MENGE
            public bool AddStorageIn_AGV(string strWerks, string strLgort, string strLocat, string strShelfsize, DataTable dtDocumentInfo, DataTable dtBarCodeInfo)
            {
                StringBuilder sbSql = new StringBuilder();
                ArrayList arySQL = new ArrayList();

                //锁定绑定的WHDWN表单据号 OTQTY -> MENGE
                //dtBarCodeInfo 插入到AGV入库中间表 AGV_WHITMTEMP

                //WHDWN
                for (int i = 0; i < dtDocumentInfo.Rows.Count; i++)
                {
                    sbSql.Remove(0, sbSql.Length);
                    sbSql.Append("Update WHDWN set ");
                    sbSql.AppendFormat("  OTQTY=MENGE Where ");
                    sbSql.AppendFormat(" WERKS='{0}' ", dtDocumentInfo.Rows[i]["WERKS"].ToString());
                    sbSql.AppendFormat(" AND LGORT='{0}' ", dtDocumentInfo.Rows[i]["LGORT"].ToString());
                    sbSql.AppendFormat(" AND MBLNR='{0}' ", dtDocumentInfo.Rows[i]["MBLNR"].ToString());
                    sbSql.AppendFormat(" AND MATNR='{0}' ", dtDocumentInfo.Rows[i]["MATNR"].ToString());
                    //sbSql.AppendFormat(" AND MENGE='{0}' ", dtDocumentInfo.Rows[i]["MENGE"].ToString());
                    arySQL.Add(sbSql.ToString());
                }

                //WHHED 13寸料架则锁定已分配储位
                if (strShelfsize == "13")
                {
                    sbSql.Remove(0, sbSql.Length);
                    sbSql.Append("Update WHHED set ");
                    sbSql.AppendFormat("  LOSTS=1 Where ");
                    sbSql.AppendFormat(" WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat(" AND LOCAT='{0}' ", strLocat);
                    sbSql.AppendFormat(" AND COMCD='{0}' ", COMCD);
                    arySQL.Add(sbSql.ToString());
                }

                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddStorageIn_AGV()";
                }
                return bolReturn;
            }
            #endregion

            #region 查询料架库存
            public DataTable QueryAGVWHITM(string strWerks, string strLGORT, string strLOCAT, string strComcd, string strMARNO)
            {
                this.ControlMethodName = "QueryAGVWHITM";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT * FROM WHITM WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND SUBSTRING(LOCAT,1,8)='{2}' AND COMCD='{3}' AND MARNO='{4}'", strWerks, strLGORT, strLOCAT, strComcd, strMARNO);

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

            #region AGV联机/转仓入库
            public bool Agv_AddStorageInData(DataTable dtBarCodeInfo, DataTable dtDocumentInfo, string strShelfsize)
            {
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strTemp = "";
                StringBuilder sbSql = new StringBuilder();

                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, WERKS, LGORT, PROGID);

                #region Insert WHLOG
                arySQL = objLogData.AddLogData("", "", dtBarCodeInfo);
                #endregion

                #region Insert WHITM
                string strSQL = "";
                string strTempLocat = "";
                for (int i = 0; i < dtBarCodeInfo.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtBarCodeInfo.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtBarCodeInfo.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtBarCodeInfo.Rows[i]["LOCAT"].ToString();
                    }

                    alColumns.Clear();
                    alConditions.Clear();
                    objWhitm.ResetField();
                    string strWerks = dtBarCodeInfo.Rows[i]["WERKS"].ToString();
                    string strLgort = dtBarCodeInfo.Rows[i]["LGORT"].ToString();
                    objWhitm.Mandt = dtBarCodeInfo.Rows[i]["MANDT"].ToString();
                    objWhitm.Comcd = dtBarCodeInfo.Rows[i]["COMCD"].ToString();
                    objWhitm.Werks = dtBarCodeInfo.Rows[i]["WERKS"].ToString();
                    objWhitm.Lgort = dtBarCodeInfo.Rows[i]["LGORT"].ToString();
                    objWhitm.MARNO = dtBarCodeInfo.Rows[i]["LOCAT"].ToString().Substring(0,3);
                    objWhitm.Locat = dtBarCodeInfo.Rows[i]["LOCAT"].ToString();
                    objWhitm.Matnr = dtBarCodeInfo.Rows[i]["MATNR"].ToString();
                    objWhitm.Insmk = dtBarCodeInfo.Rows[i]["INSMK"].ToString();
                    objWhitm.Mblnr = dtBarCodeInfo.Rows[i]["MBLNR"].ToString();
                    objWhitm.Charg = dtBarCodeInfo.Rows[i]["CHARG"].ToString();
                    objWhitm.Lifnr = dtBarCodeInfo.Rows[i]["LIFNR"].ToString();
                    objWhitm.Rmano = dtBarCodeInfo.Rows[i]["RMANO"].ToString();
                    objWhitm.Ebeln = dtBarCodeInfo.Rows[i]["EBELN"].ToString();
                    objWhitm.Vedat = dtBarCodeInfo.Rows[i]["VEDAT"].ToString();
                    objWhitm.Indat = dtBarCodeInfo.Rows[i]["INDAT"].ToString();
                    objWhitm.Menge = dtBarCodeInfo.Rows[i]["MENGE"].ToString();
                    objWhitm.Qcqty = "0";
                    objWhitm.Refno = "";
                    objWhitm.Mrgid = dtBarCodeInfo.Rows[i]["MRGID"].ToString();
                    objWhitm.Isptm = "";
                    objWhitm.Kdmat = dtBarCodeInfo.Rows[i]["KDMAT"].ToString();
                    objWhitm.Rmak1 = dtBarCodeInfo.Rows[i]["RMAK1"].ToString();
                    objWhitm.Crnam = CRNAM;
                    objWhitm.Crdat = "GetDate()";
                    objWhitm.Monam = CRNAM;
                    objWhitm.Modat = "GetDate()";
                    objWhitm.Serno = dtBarCodeInfo.Rows[i]["SERNO"].ToString();
                    objWhitm.Dacod = dtBarCodeInfo.Rows[i]["DACOD"].ToString();
                    objWhitm.Locod = dtBarCodeInfo.Rows[i]["LOCOD"].ToString();
                    objWhitm.TASKID = dtBarCodeInfo.Rows[i]["TASKID"].ToString();
                    objWhitm.ITEMSTATES = "Y";
                    objWhitm.STOCSTATES = "Y";
                    objWhitm.MAXEXP = dtBarCodeInfo.Rows[i]["MAXEXP"].ToString();

                    if (CheckStorageInType(strWerks, strLgort, "StorageInLock"))
                    {
                        if (dtBarCodeInfo.Rows[i]["EXPDAT"].ToString() == "")
                        {
                            if (GetExpiryDate(dtBarCodeInfo.Rows[i]["VEDAT"].ToString(), dtBarCodeInfo.Rows[i]["MATNR"].ToString()).Rows.Count > 0)
                            {
                                objWhitm.ExpiryDate = GetExpiryDate(dtBarCodeInfo.Rows[i]["VEDAT"].ToString(), dtBarCodeInfo.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString();
                                if (Convert.ToInt32(GetExpiryDate(dtBarCodeInfo.Rows[i]["VEDAT"].ToString(), dtBarCodeInfo.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                {
                                    objWhitm.LOCKED = "Y";
                                    objWhitm.LKDAT = "GetDate()";
                                }
                            }
                            else
                            {
                                objWhitm.ExpiryDate = dtBarCodeInfo.Rows[i]["EXPDAT"].ToString();
                                objWhitm.LOCKED = "Y";
                                objWhitm.LKDAT = "GetDate()";

                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(dtBarCodeInfo.Rows[i]["EXPDAT"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                            {
                                objWhitm.LOCKED = "Y";
                                objWhitm.LKDAT = "GetDate()";
                            }
                            objWhitm.ExpiryDate = dtBarCodeInfo.Rows[i]["EXPDAT"].ToString();
                        }
                    }
                    else
                    {
                        objWhitm.ExpiryDate = dtBarCodeInfo.Rows[i]["EXPDAT"].ToString();

                    }
                    strSQL = objWhitm.EntityGetInsertSql();
                    arySQL.Add(strSQL);
                }
                #endregion

                if (dtDocumentInfo.Rows.Count > 0)
                {
                    #region Update WHDWN
                    for (int j = 0; j < dtDocumentInfo.Rows.Count; j++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "MENGE - " + dtDocumentInfo.Rows[j]["MENGE"].ToString() + "+" + dtDocumentInfo.Rows[j]["ALQTY"].ToString();
                        alConditions.Add("Mandt='" + MANDT + "'");
                        alConditions.Add("Comcd='" + COMCD + "'");
                        alConditions.Add("Mblnr='" + dtDocumentInfo.Rows[j]["MBLNR"].ToString() + "'");
                        alConditions.Add("Zeile='" + dtDocumentInfo.Rows[j]["ZEILE"].ToString() + "'");
                        strTemp = "";
                        strTemp = objWhdwn.EntityGetUpdateSql(alConditions);
                        arySQL.Add(strTemp);
                    }
                    #endregion
                }

                #region Update WHHED
                if (strShelfsize == "7")
                {
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        string sql = "Update WHHED set LOSTS=(Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + dtBarCodeInfo.Rows[0]["WERKS"].ToString() + "' and LGORT= '" + dtBarCodeInfo.Rows[0]["LGORT"].ToString() + "' and LOCAT= '" + aryLocat[i].ToString() + "') " +
                    " Where MANDT='" + MANDT + "' " +
                    " And COMCD='" + COMCD + "' " +
                    " And WERKS='" + dtBarCodeInfo.Rows[0]["WERKS"].ToString() + "' " +
                    " And LGORT='" + dtBarCodeInfo.Rows[0]["LGORT"].ToString() + "' " +
                    " And LOCAT='" + aryLocat[i].ToString() + "' ";
                        arySQL.Add(sql.ToString());
                    }
                }
                #endregion

                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- Agv_AddStorageInData()";
                }
                return bolReturn;
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

            #region 更新货架序号
            public bool UpdateAGVShelf(string strWerks, string strLgort, string strTASKID, int strREQNO)
            {
                StringBuilder sbSql = new StringBuilder();
                ArrayList arySQL = new ArrayList();

                string strREQNOAdd = (strREQNO + 1).ToString();

                sbSql.Remove(0, sbSql.Length);
                sbSql.Append("Update WHAGV set ");
                sbSql.AppendFormat("  REQNO='{0}' Where ", strREQNOAdd);
                sbSql.AppendFormat(" WERKS='{0}' ", strWerks);
                sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat(" AND TASKID='{0}' ", strTASKID);
                sbSql.AppendFormat(" AND REQNO='{0}' ", strREQNO);
                arySQL.Add(sbSql.ToString());

                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddStorageIn_AGV()";
                }
                return bolReturn;
            }
            #endregion

            #region 查询IQC检验信息
            public DataTable QueryMAXEXP(string strTASKID)
            {
                this.ControlMethodName = "QueryMAXEXP";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT * FROM WHIQC WITH(NOLOCK) WHERE TASKID='{0}' ", strTASKID);

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

            #endregion

            public bool AddOnLineSMTInData(string strMrgid, DataTable dtStorage)
            {
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhrid objWhrid = new DataWhrid(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strTemp = "";
                StringBuilder sbSql = new StringBuilder();

                ArrayList arySQL = new ArrayList();
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();

                LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, PROGID);
                StorageData objStorage = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                //WHLOG
                arySQL = objLogData.AddLogData("", "", dtStorage);
                string strSQL = "";

                string strTempLocat = "";


                //WHITM
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                    }

                    if (!objStorage.QueryDataExistsInStorage(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["MBLNR"].ToString(), dtStorage.Rows[i]["CHARG"].ToString()))
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();

                        objWhitm.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                        objWhitm.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhitm.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhitm.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhitm.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhitm.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhitm.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhitm.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhitm.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhitm.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhitm.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                        objWhitm.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhitm.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Qcqty = "0";
                        objWhitm.Refno = dtStorage.Rows[i]["REFID"].ToString();
                        objWhitm.Mrgid = strMrgid;
                        objWhitm.Isptm = "";
                        objWhitm.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Crnam = CRNAM;
                        objWhitm.Crdat = "GetDate()";
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = "GetDate()";
                        objWhitm.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                        objWhitm.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhitm.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhitm.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhitm.ITEMSTATES = "Y";
                        objWhitm.STOCSTATES = "Y";
                        objWhitm.MARNO = dtStorage.Rows[i]["LOCAT"].ToString().Substring(0,3);


                        //入库日期-管控仓 2023917 Nesta            
                        if (CheckStorageInType(dtStorage.Rows[i]["WERKS"].ToString(), dtStorage.Rows[i]["LGORT"].ToString(), "Diff DACOD Diff Locat"))
                        {
                            objWhitm.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        }
                        if (CheckStorageInType(dtStorage.Rows[i]["WERKS"].ToString(), dtStorage.Rows[i]["LGORT"].ToString(), "StorageInLock"))
                        {
                            if (dtStorage.Rows[i]["EXPDAT"].ToString() == "")
                            {
                                if (GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows.Count > 0)
                                {
                                    objWhitm.ExpiryDate = GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString();
                                    if (Convert.ToInt32(GetExpiryDate(dtStorage.Rows[i]["VEDAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()).Rows[0]["ExpiryDate"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                    {
                                        objWhitm.LOCKED = "Y";
                                        objWhitm.LKDAT = "GetDate()";
                                    }
                                }
                                else
                                {
                                    objWhitm.ExpiryDate = dtStorage.Rows[i]["EXPDAT"].ToString();
                                    objWhitm.LOCKED = "Y";
                                    objWhitm.LKDAT = "GetDate()";

                                }

                            }
                            else
                            {
                                if (Convert.ToInt32(dtStorage.Rows[i]["EXPDAT"].ToString()) < Convert.ToInt32(DateTime.Today.ToString("yyyyMMdd")))
                                {
                                    objWhitm.LOCKED = "Y";
                                    objWhitm.LKDAT = "GetDate()";
                                }
                                objWhitm.ExpiryDate = dtStorage.Rows[i]["EXPDAT"].ToString();

                            }

                        }
                        else
                        {
                            objWhitm.ExpiryDate = dtStorage.Rows[i]["EXPDAT"].ToString();

                        }
                        if (dtStorage.Rows[i]["TASKID"].ToString() != "")
                        {
                            if (dtStorage.Rows[i]["TASKID"].ToString().ToUpper().Substring(0, 2) == "R7")
                            {
                                objWhitm.TASKID = dtStorage.Rows[i]["TASKID"].ToString().ToUpper();
                            }
                            else
                            {
                                objWhitm.TASKID = "";
                            }
                        }
                        else
                        {
                            objWhitm.TASKID = "";
                        }
                        objWhitm.MAXEXP = dtStorage.Rows[i]["MAXEXP"].ToString();


                        strSQL = objWhitm.EntityGetInsertSql();
                        arySQL.Add(strSQL);
                    }
                    else
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();

                        objWhitm.Menge = "MENGE +" + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Rmak1 = dtStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = "GetDate()";
                        objWhitm.Refno = dtStorage.Rows[i]["REFID"].ToString();
                        objWhitm.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhitm.ExpiryDate = dtStorage.Rows[i]["EXPDAT"].ToString();
                        objWhitm.TASKID = dtStorage.Rows[i]["TASKID"].ToString();
                        objWhitm.MAXEXP = dtStorage.Rows[i]["MAXEXP"].ToString();

                        alConditions.Add("Mandt= '" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add("Comcd= '" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add("Werks= '" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add("Lgort= '" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add("Locat= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add("Matnr= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add("Insmk= '" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add("Mblnr= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add("Charg= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                        strSQL = objWhitm.EntityGetUpdateSql(alConditions);
                        arySQL.Add(strSQL);
                    }

                    #region WHRID
                    alColumns.Clear();
                    alConditions.Clear();
                    objWhrid.ResetField();
                    objWhrid.Otqty = "OTQTY +" + dtStorage.Rows[i]["ALQTY"].ToString();
                    objWhrid.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                    objWhrid.Modat = "GetDate()";

                    alConditions.Add("Mandt='" + MANDT + "'");
                    alConditions.Add("Comcd='" + COMCD + "'");
                    alConditions.Add("Didno='" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                    alConditions.Add("Refid='" + dtStorage.Rows[i]["REFID"].ToString() + "'");
                    strTemp = "";
                    strTemp = objWhrid.EntityGetUpdateSql(alConditions);
                    arySQL.Add(strTemp);

                    #endregion
                }

                //WHHED
                #region "Update WHHED Table"
                for (int i = 0; i < aryLocat.Count; i++)
                {
                    sbSql.Remove(0, sbSql.Length);
                    sbSql.Append("Update WHHED set  ");
                    sbSql.AppendFormat("  LOSTS=T.TOTAL from ");
                    sbSql.AppendFormat(" (Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "') as T  ");
                    sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat(" and WERKS='{0}' ", WERKS);
                    sbSql.AppendFormat(" and LGORT='{0}' ", LGORT);
                    sbSql.AppendFormat(" and LOCAT='{0}' ", aryLocat[i].ToString());
                    arySQL.Add(sbSql.ToString());
                }
                #endregion


                bool bolReturn = false;
                try
                {
                    ControlHandleDB();

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlSqlAccess.GetDataTable(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion

                    if (aryCheckList.Count > 0)
                    {
                        #region 防呆，避免重複處理同一張SAP Document

                        sbCheckList.Remove(0, sbCheckList.Length);
                        for (int j = 0; j < aryCheckList.Count; j++)
                        {
                            if (j != 0)
                                sbCheckList.Append(" , ");
                            sbCheckList.Append(aryCheckList[j]);
                        }
                        ERRMSG = "Doc. No. " + sbCheckList.ToString() + " are processed. Please Check it.";
                        #endregion
                    }
                    else
                    {
                        bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                    }
                }
                catch (System.Exception ex)
                {

                    ERRMSG = ex.Message + "<- AddOnLineSMTInData()";

                    #region 將錯誤訊息寫回DB
                    StringBuilder sbErrSql = new StringBuilder();

                    for (int i = 0; i < arySQL.Count; i++)
                    {
                        if (i != 0)
                        {
                            sbErrSql.Append("\n\r");
                        }
                        sbErrSql.Append(arySQL[i].ToString().Replace("'", "''"));

                    }
                    string strErrSQL = "Insert into ERRLOG (MANDT,COMCD,REFID,MBLNR,LOGSQL,LOGTIM,MALFLG) " +
                                       "Values('" + dtStorage.Rows[0]["MANDT"].ToString() + "','" + dtStorage.Rows[0]["COMCD"].ToString() + "','" + dtStorage.Rows[0]["REFID"].ToString() + "'," +
                                              "'" + dtStorage.Rows[0]["OMBLNR"].ToString() + "',N'" + sbErrSql.ToString() + "',getdate(),'N')";
                    this.ControlSqlAccess.ExecSql(strErrSQL);

                    #endregion


                }
                return bolReturn;
            }

            #region 并储查询料架库存
            public DataTable QueryAGVCombineWHITM(string strWerks, string strLGORT, string strLOCAT, string strComcd, string strMATNR, string strLIFNR, string strDACOD, string strLOCOD, string strSERNO)
            {
                this.ControlMethodName = "QueryAGVCombineWHITM";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat(@"  SELECT [MANDT]
                                                  ,[WERKS]
                                                  ,[LGORT]
                                                  ,[LOCAT]
                                                  ,[MATNR]
                                                  ,[INSMK]
                                                  ,[MBLNR]
                                                  ,[CHARG]
                                                  ,[LIFNR]
                                                  ,[RMANO]
                                                  ,[EBELN]
                                                  ,[INDAT]
                                                  ,[MENGE]
                                                  ,[QCQTY]
                                                  ,[REFNO]
                                                  ,[MRGID]
                                                  ,[ISPTM]
                                                  ,[REQTY]
                                                  ,[KDMAT]
                                                  ,[RMAK1]
                                                  ,[CRNAM]
                                                  ,[CRDAT]
                                                  ,[MONAM]
                                                  ,[MODAT]
                                                  ,[SERNO]
                                                  ,[LOCOD]
                                                  ,[INSPT]
                                                  ,[COMCD]
                                                  ,[BKQTY]
                                                  ,[DACOD]
                                                  ,[VEDAT]
                                                  ,[BOXID]
                                                  ,[NLOCA]
                                                  ,[SIDNO]
                                                  ,[REFID]
                                                  ,[SEQNO]
                                                  ,[PKDAT]
                                                  ,[ExpiryDate]
                                                  ,[TASKID]
                                                  ,[MAXEXP]
                                                  ,[MRBNO]
                                                  ,[LOCKED]
                                                  ,[LKDAT]
                                                  ,[MARNO]
                                                  ,[ITEMSTATES]
                                                  ,[STOCSTATES]
                                                  ,[CONFIG]
                                                  ,[MCDAT] FROM WHITM WITH(NOLOCK) 
                        WHERE WERKS='{0}' AND LGORT='{1}' 
                        AND LOCAT like '{2}%' 
                        AND COMCD='{3}' AND MATNR='{4}' AND LIFNR='{5}' AND SERNO='{6}' AND DACOD='{7}'AND  LOCOD='{8}' ", strWerks, strLGORT, strLOCAT, strComcd, strMATNR, strLIFNR, strSERNO, strDACOD, strLOCOD);
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

            #region AGV并储出库
            public bool Agv_CombineStorageOutData(DataTable dtBarCodeInfo)
            {
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitmAGV objWhitmAGV = new DataWhitmAGV(UserData);

                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strTemp = "";
                StringBuilder sbSql = new StringBuilder();

                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, WERKS, LGORT, PROGID);

                #region Insert WHLOG
                arySQL = objLogData.AddLogData("", "", dtBarCodeInfo);
                #endregion

                #region Insert 出储到临时表
                string strSQL = "";
                string strTempLocat = "";
                for (int i = 0; i < dtBarCodeInfo.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtBarCodeInfo.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtBarCodeInfo.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtBarCodeInfo.Rows[i]["LOCAT"].ToString();
                    }
                    
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitmAGV.ResetField();
                        objWhitmAGV.UniqueID = dtBarCodeInfo.Rows[i]["SERNO"].ToString();
                        objWhitmAGV.Mandt = dtBarCodeInfo.Rows[i]["MANDT"].ToString();
                        objWhitmAGV.Comcd = dtBarCodeInfo.Rows[i]["COMCD"].ToString();
                        objWhitmAGV.Werks = dtBarCodeInfo.Rows[i]["WERKS"].ToString();
                        objWhitmAGV.Lgort = dtBarCodeInfo.Rows[i]["LGORT"].ToString();
                        objWhitmAGV.Locat = dtBarCodeInfo.Rows[i]["LOCAT"].ToString();
                        objWhitmAGV.Matnr = dtBarCodeInfo.Rows[i]["MATNR"].ToString();
                        objWhitmAGV.Insmk = dtBarCodeInfo.Rows[i]["INSMK"].ToString();
                        objWhitmAGV.Mblnr = dtBarCodeInfo.Rows[i]["MBLNR"].ToString();
                        objWhitmAGV.Charg = dtBarCodeInfo.Rows[i]["CHARG"].ToString();
                        objWhitmAGV.Lifnr = dtBarCodeInfo.Rows[i]["LIFNR"].ToString();
                        objWhitmAGV.Rmano = dtBarCodeInfo.Rows[i]["RMANO"].ToString();
                        objWhitmAGV.Ebeln = dtBarCodeInfo.Rows[i]["EBELN"].ToString();
                        objWhitmAGV.Indat = dtBarCodeInfo.Rows[i]["INDAT"].ToString();
                        objWhitmAGV.Menge = dtBarCodeInfo.Rows[i]["MENGE"].ToString();
                        objWhitmAGV.Qcqty = dtBarCodeInfo.Rows[i]["QCQTY"].ToString();
                        objWhitmAGV.Refno = dtBarCodeInfo.Rows[i]["REFNO"].ToString();
                        objWhitmAGV.Mrgid = dtBarCodeInfo.Rows[i]["MRGID"].ToString();
                        objWhitmAGV.Isptm = dtBarCodeInfo.Rows[i]["ISPTM"].ToString();
                        objWhitmAGV.Kdmat = dtBarCodeInfo.Rows[i]["KDMAT"].ToString();
                        objWhitmAGV.Rmak1 = dtBarCodeInfo.Rows[i]["RMAK1"].ToString();
                        objWhitmAGV.Crnam = CRNAM;
                        objWhitmAGV.Crdat = "GetDate()";
                        objWhitmAGV.Monam = CRNAM;
                        objWhitmAGV.Modat = "GetDate()";
                        objWhitmAGV.Serno = dtBarCodeInfo.Rows[i]["SERNO"].ToString();
                        objWhitmAGV.Locod = dtBarCodeInfo.Rows[i]["LOCOD"].ToString();
                        objWhitmAGV.Inspt = dtBarCodeInfo.Rows[i]["INSPT"].ToString();
                        objWhitmAGV.Dacod = dtBarCodeInfo.Rows[i]["DACOD"].ToString();
                        objWhitmAGV.Vedat = dtBarCodeInfo.Rows[i]["VEDAT"].ToString();
                        objWhitmAGV.Boxid = dtBarCodeInfo.Rows[i]["BOXID"].ToString();
                        objWhitmAGV.Refid = dtBarCodeInfo.Rows[i]["REFID"].ToString();
                        objWhitmAGV.ExpiryDate = dtBarCodeInfo.Rows[i]["ExpiryDate"].ToString();
                        objWhitmAGV.TASKID = dtBarCodeInfo.Rows[i]["TASKID"].ToString();
                        objWhitmAGV.MAXEXP = dtBarCodeInfo.Rows[i]["MAXEXP"].ToString();
                        objWhitmAGV.MRBNO = dtBarCodeInfo.Rows[i]["MRBNO"].ToString();
                        objWhitmAGV.LOCKED = dtBarCodeInfo.Rows[i]["LOCKED"].ToString();
                        objWhitmAGV.LKDAT = dtBarCodeInfo.Rows[i]["LKDAT"].ToString();
                        objWhitmAGV.MARNO = dtBarCodeInfo.Rows[i]["MARNO"].ToString();
                        objWhitmAGV.ITEMSTATES = dtBarCodeInfo.Rows[i]["ITEMSTATES"].ToString();
                        objWhitmAGV.STOCSTATES = dtBarCodeInfo.Rows[i]["STOCSTATES"].ToString();
                        objWhitmAGV.CONFIG = dtBarCodeInfo.Rows[i]["CONFIG"].ToString();
                        objWhitmAGV.MCDAT = dtBarCodeInfo.Rows[i]["MCDAT"].ToString();
        
                    strSQL = objWhitmAGV.EntityGetInsertSql();
                    arySQL.Add(strSQL);

                    #region 删除并储出原库存
                    string sqlWHITMDelete = "DELETE FROM WHITM " +
                        " WHERE MANDT='" + dtBarCodeInfo.Rows[i]["MANDT"].ToString() + "'" +
                        " AND COMCD='" + dtBarCodeInfo.Rows[i]["COMCD"].ToString() + "'" +
                        " AND WERKS='" + dtBarCodeInfo.Rows[i]["WERKS"].ToString() + "'" +
                        " AND LGORT='" + dtBarCodeInfo.Rows[i]["LGORT"].ToString() + "'" +
                        " AND LOCAT='" + dtBarCodeInfo.Rows[i]["LOCAT"].ToString() + "'" +
                        " AND MATNR='" + dtBarCodeInfo.Rows[i]["MATNR"].ToString() + "'" +
                        " AND INSMK='" + dtBarCodeInfo.Rows[i]["INSMK"].ToString() + "'" +
                        " AND CHARG='" + dtBarCodeInfo.Rows[i]["CHARG"].ToString() + "'" +
                        " AND MBLNR='" + dtBarCodeInfo.Rows[i]["MBLNR"].ToString() + "'" +
                        " AND MENGE='" + dtBarCodeInfo.Rows[i]["MENGE"].ToString() + "'" +
                        " AND SERNO='" + dtBarCodeInfo.Rows[i]["SERNO"].ToString() + "'" +
                        " AND DACOD='" + dtBarCodeInfo.Rows[i]["DACOD"].ToString() + "'";
                    arySQL.Add(sqlWHITMDelete.ToString());
                    #endregion
                }
                #endregion

                #region Update WHHED
                for (int i = 0; i < aryLocat.Count; i++)
                {
                    string sql = "Update WHHED set LOSTS=(Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + dtBarCodeInfo.Rows[0]["WERKS"].ToString() + "' and LGORT= '" + dtBarCodeInfo.Rows[0]["LGORT"].ToString() + "' and LOCAT= '" + aryLocat[i].ToString() + "') " +
                        " Where MANDT='" + MANDT + "' " +
                        " And COMCD='" + COMCD + "' " +
                        " And WERKS='" + dtBarCodeInfo.Rows[0]["WERKS"].ToString() + "' " +
                        " And LGORT='" + dtBarCodeInfo.Rows[0]["LGORT"].ToString() + "' " +
                        " And LOCAT='" + aryLocat[i].ToString() + "' ";
                    arySQL.Add(sql.ToString());
                }
                #endregion

                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- Agv_AddStorageInData()";
                }
                return bolReturn;
            }
            #endregion

            #region 查询并储库存_库存临时表 WHITM_AGV
            public DataTable QueryStockWHITM_AGV(string strWerks, string strLGORT, string strComcd)
            {
                this.ControlMethodName = "QueryStockWHITM_AGV";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT UniqueID,MANDT,COMCD,WERKS,LGORT,MARNO,SUBSTRING(LOCAT, 4, 1) AS FTYPE,'AGV' AS LOCTYPE," +
                        "LOCAT,MATNR,INSMK,CHARG,MENGE,0 AS ALQTY,ITEMSTATES,STOCSTATES,MBLNR,'' AS ZEILE,EBELN,LIFNR,RMANO,'' AS OMBLNR,MRGID,'' AS KOSTL," +
                        "'' AS ARBPL,'' AS TRNTP,RMAK1,INDAT,KDMAT,SERNO,'' AS GRLOC,DACOD,VEDAT,LOCOD,ExpiryDate AS EXPDAT,TASKID,MAXEXP " +
                        "FROM WHITM_AGV WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND COMCD='{2}' ", strWerks, strLGORT, strComcd);
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

            #region 检验表格WHITM_AGV
            public DataTable CheckStockWHITM_AGV(string strWerks, string strLGORT, string strComcd,string strMatnr,string strDacod,string Lifnr, string strLocod)
            {
                this.ControlMethodName = "CheckStockWHITM_AGV";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT MATNR,DACOD,LIFNR,LOCOD,LOCAT ,SUM(MENGE)AS MENGE FROM WHITM_AGV WITH(NOLOCK) WHERE WERKS='{0}' AND LGORT='{1}' AND COMCD='{2}' AND MATNR='{3}'AND DACOD='{4}'AND LIFNR='{5}'AND LOCOD='{6}'GROUP BY MATNR, DACOD, LIFNR, LOCOD, LOCAT ", strWerks, strLGORT, strComcd, strMatnr, strDacod, Lifnr, strLocod);
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

            #region 删除并储作业，已入库数据
            /// <summary>
            /// 删除并储作业，已入库数据
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strTaskNo"></param>
            /// <param name="strWorkStation"></param>
            /// <exception cref="Exception"></exception>
            public bool DeleteCombineStockIn(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteCombineStockIn";
                this.ControlMethodParm = "(" + strWerks + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                try
                {
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("DELETE WHITM_AGV WHERE WERKS='{0}' AND LGORT='{1}'", strWerks, strLgort);
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

            public void AddErrorLog(DataTable dtStorage, DataTable dtLocat)
            {
                this.ControlMethodName = "InsertErrorLog";
                this.ControlMethodParm = "(" + dtStorage + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataErrlog objErrlog = new DataErrlog(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                ArrayList arySQL = new ArrayList();

                string Locat = "";

                arySQL.Clear();
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    Locat = dtLocat.Select("MBLNR = '" + dtStorage.Rows[i]["DIDNO"].ToString() + "'").CopyToDataTable().Rows[0]["LOCAT"].ToString();

                    alColumns.Clear();
                    alConditions.Clear();
                    objErrlog.ResetField();

                    objErrlog.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                    objErrlog.Mblnr = dtStorage.Rows[i]["DIDNO"].ToString();
                    objErrlog.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                    objErrlog.Refid = dtStorage.Rows[i]["REFID"].ToString();
                    objErrlog.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                    objErrlog.Menge = dtStorage.Rows[i]["MENGE"].ToString();
                    objErrlog.Logtim = "GetDate()";
                    objErrlog.Maltim = "GetDate()";
                    objErrlog.Logsql = "UpdateSapInventory()_only" + "/" + Locat;
                    objErrlog.Usrnm = UserData.UserId.ToString();
                    arySQL.Add(objErrlog.EntityGetInsertSql());
                }

                ControlHandleDB();
                ControlSqlAccess.ExecSqlArray(arySQL);
                ControlSqlAccess.CloseConnection();
            }

            public string getShelftype(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getECUser";
                this.ControlMethodParm = "(" + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strUser = string.Empty;
                string strSql = string.Empty;

                strSql = "SELECT CTRLC3 FROM WHCTRL WHERE CTRLID='LGORT' AND CTRLNM='" + strWerks + "' and CTRLC1 = '" + strLgort + "' ";

                try
                {
                    ControlHandleDB();
                    strUser = ControlSqlAccess.GetFieldValue(strSql);
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
                return strUser;
            }


            #endregion

        }
    }
}
