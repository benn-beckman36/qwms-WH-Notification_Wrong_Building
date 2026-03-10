using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Qci.Base.Common;
using QWMS.Common;



namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// AGVApi 的摘要描述。
        /// </summary>
        public class AGVApi : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strCrnam = "";
            private string strWkaut = "";
            private string strMaaut = "";
            private string strInaut = "";
            private string strOtaut = "";
            private string strCgaut = "";
            private string strIvaut = "";
            private string strRepln = "";
            private string strMgaut = "";
            private string strIsadm = "";
            private string strErrmsg = "";

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
            /// 廠區權限。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string WKAUT
            {
                get { return strWkaut; }
                set { strWkaut = value; }
            }


            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 基本資料維護權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string MAAUT
            {
                get { return strMaaut; }
                set { strMaaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 入庫作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string INAUT
            {
                get { return strInaut; }
                set { strInaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 出庫作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string OTAUT
            {
                get { return strOtaut; }
                set { strOtaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 檢驗作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string CGAUT
            {
                get { return strCgaut; }
                set { strCgaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 盤點作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string IVAUT
            {
                get { return strIvaut; }
                set { strIvaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 庫存管理作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string MGAUT
            {
                get { return strMgaut; }
                set { strMgaut = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 庫存管理作業權限
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string REPLN
            {
                get { return strRepln; }
                set { strRepln = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 是否管理者
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ISADM
            {
                get { return strIsadm; }
                set { strIsadm = value; }
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


            #region 不傳入任何參數產生Authority物件
            /// <summary>
            /// 不傳入任何參數產生Authority物件。
            /// </summary>
            /// <example>
            /// <code>
            ///  QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority();
            ///  Your Code Here......
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public AGVApi(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
            {
            }
            #endregion

            #region 利用傳入參數產生Authority物件
            /// <summary>
            /// 利用傳入參數產生Authority物件。
            /// </summary>
            /// <param name="varDBType">DB Type。</param>
            /// <param name="varDBCode">DB Code。</param>
            /// <param name="varErrorType">Error Type。</param>
            /// <param name="varErrorCode">Error Code。</param>
            /// <example>
            /// <code>
            ///  QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(1, "TEST", 2, "ERR",UserData);
            ///  Your Code Here......
            /// </code>
            /// </example>
            public AGVApi(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
            {
                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                CRNAM = varUserData.UserId;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.PlantData";
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

            #region AGV调度API接口
            /// <summary>
            /// AGV调度API接口
            /// </summary>
            /// <param name="strFunction">功能名字</param>
            /// <param name="strRequestType">请求类型</param>
            /// <param name="strRequestData">请求数据</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public string AGVHttpRequest(string strFunction, string strRequestType, string strRequestData)
            {
                string strResult = string.Empty;
                string strFlage = "N";
                ERRMSG = string.Empty;
                try
                {
                    string strRequestUrl = GetAGVRequestUrl(strRequestType);
                    ClaHttpHelper httpHelper = new ClaHttpHelper();
                    string strResponse = httpHelper.HttpPostByHttpWebRequest(strRequestUrl, strRequestData);
                    JObject obj = (JObject)JsonConvert.DeserializeObject(strResponse);
                    if (obj["message"].ToString().Equals("OK"))
                    {
                        strResult = obj["data"].ToString();
                        strFlage = "Y";
                    }
                    else
                    {
                        ERRMSG = obj["message"].ToString();
                    }
                    //catch 不影响正常流程
                    WriteAPILog(strFunction, strRequestData, strRequestType, strFlage, strResponse);
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


            #region API日志记录
            public bool WriteAPILog(string strFunction, string strRequestData, string strRequestType,string strFlage,string strResponse)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAGVRequestUrl";
                this.ControlMethodParm = "(" + strRequestType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                StringBuilder sbSql = new StringBuilder();
                try
                {
                    sbSql.AppendFormat("INSERT INTO AGV_APILOG(STEP,JSONINFO,FLAGE,Msg,CREDATE,STYPE) VALUES('{0}','{1}','{2}','{3}',GETDATE(),'{4}')", strFunction, strRequestData, strFlage, strResponse, strRequestType);
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (Exception ex)
                {
                }

                return blResult;
            }
            #endregion

            public string AGVHttpGetRequest(string strFunction, string strRequestType, string strRequestData)
            {
                string strResult = string.Empty;
                string strFlage = "N";
                try
                {
                    string strRequestUrl = GetAGVRequestUrl(strRequestType);
                    ClaHttpHelper httpHelper = new ClaHttpHelper();
                    string strResponse = httpHelper.HttpGetByHttpWebRequest(strRequestUrl + strRequestData);
                    JObject obj = (JObject)JsonConvert.DeserializeObject(strResponse);
                    if (obj["message"].ToString().Equals("OK"))
                    {
                        strResult = obj["data"].ToString();
                        strFlage = "Y";
                    }
                    
                    //catch 不影响正常流程
                    WriteAPILog(strFunction, strRequestData, strRequestType, strFlage, strResponse);
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
        }
    }
}
