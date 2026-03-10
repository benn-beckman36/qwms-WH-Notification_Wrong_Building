using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.IO;
using System.Xml;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;




namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// StorageData 的摘要描述。
        /// </summary>
        public class AsrsInterface : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";

            private string strErrmsg = "";

            #region Constructer

            public AsrsInterface()
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
            public AsrsInterface(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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

            public AsrsInterface(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
            {
            }

            public AsrsInterface(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "")
            {
            }

            public AsrsInterface(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
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

            #region 同步入库数据到ASRS中间表
            /// <summary>
            /// 同步入库数据到ASRS中间表
            /// </summary>
            /// <param name="dt">格式固定的数据结构</param>
            /// <returns></returns>
            public string PostStorageInData(string XMLData)
            {
                //dt.Rows[0]["TRN_NO"] = "G2018020100016";
                //string XMLData = ConvertDataTableToXML(dt);

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //域名配置在数据库中
                string strRequestUrl = objAuthority.GetApiRequestUrl();
                strRequestUrl = strRequestUrl + "/QWMSAPI/api/ASRS/PostStorageInData";
                string strReturn = RequestPostData(XMLData, strRequestUrl);

                //旧：固定写死
                //string strReturn = RequestPostData(XMLData, "https://scm.quantacn.com/ASRSAPI/api/ASRS/PostStorageInData");

                //   string strReturn = RequestPostData(XMLData, "http://localhost:10985/api/asrs/poststorageindata");

                return strReturn;
            }
            public string PostStorageInData(DataTable dt)
            {
                //dt.Rows[0]["TRN_NO"] = "G2018020100016";
                string XMLData = ConvertDataTableToXML(dt);

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //域名配置在数据库中
                string strRequestUrl = objAuthority.GetApiRequestUrl();
                strRequestUrl = strRequestUrl + "/QWMSAPI/api/ASRS/PostStorageInData";
                string strReturn = RequestPostData(XMLData, strRequestUrl);

                //旧：固定写死
                //string strReturn = RequestPostData(XMLData, "https://scm.quantacn.com/ASRSAPI/api/ASRS/PostStorageInData");
                //   string strReturn = RequestPostData(XMLData, "http://localhost:10985/api/asrs/poststorageindata");

                return strReturn;
            }
            #endregion

            #region 同步出库数据到ASRS中间表
            /// <summary>
            /// 同步出库数据到ASRS中间表
            /// </summary>
            /// <param name="dt">格式固定的数据结构</param>
            /// <returns></returns>
            //public string PostStorageOutData(DataTable dt)
            public string PostStorageOutData(string XMLData)
            {
                //string XMLData = ConvertDataTableToXML(dt);

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //域名配置在数据库中
                string strRequestUrl = objAuthority.GetApiRequestUrl();
                strRequestUrl = strRequestUrl + "/QWMSAPI/api/ASRS/PostStorageOutData";
                string strReturn = RequestPostData(XMLData, strRequestUrl);

                //旧：固定写死
                //string strReturn = RequestPostData(XMLData, "https://scm.quantacn.com/ASRSAPI/api/ASRS/PostStorageOutData");

                return strReturn;
            }

            #endregion

            #region 同步并储数据到ASRS中间表
            /// <summary>
            /// 同步并储数据到ASRS中间表
            /// </summary>
            /// <param name="dt">格式固定的数据结构</param>
            /// <returns></returns>
            public string PostStorageMergeData(DataTable dt)
            {
                string XMLData = ConvertDataTableToXML(dt);
                string strReturn = RequestPostData(XMLData, "");

                return strReturn;
            }

            #endregion

            #region DataTable转化成xml
            /// <summary>
            /// DataTable转化成xml
            /// </summary>
            /// <param name="xmlDS"></param>
            /// <returns></returns>
            public string ConvertDataTableToXML(DataTable xmlDS)
            {
                MemoryStream stream = null;
                XmlTextWriter writer = null;
                try
                {
                    stream = new MemoryStream();
                    writer = new XmlTextWriter(stream, Encoding.Default);
                    xmlDS.WriteXml(writer);
                    int count = (int)stream.Length;
                    byte[] arr = new byte[count];
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.Read(arr, 0, count);
                    UTF8Encoding utf = new UTF8Encoding();
                    return utf.GetString(arr).Trim().Replace("DocumentElement", "XML");
                }
                catch
                {
                    return String.Empty;
                }
                finally
                {
                    if (writer != null) writer.Close();
                }
            }

            #endregion



            #region  向Url发送post请求,返回网站响应内容
            /// <summary>
            /// 向Url发送post请求,返回网站响应内容
            /// </summary>
            /// <param name="postData">发送数据</param>
            /// <param name="uriStr">接受数据的Url</param>
            /// <param name="action">更新操作</param>
            /// <returns>返回网站响应内容</returns>
            public static string RequestPostData(string postData, string uriStr)
            {
                HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uriStr);
                StringBuilder postContent = new StringBuilder();
                Encoding myEncoding = Encoding.UTF8; //Encoding.GetEncoding("UTF-8");
                postContent.Append(HttpUtility.UrlEncode("StorageData", myEncoding));
                postContent.Append("=");

                postContent.Append(HttpUtility.UrlEncode(postData,myEncoding));
                // postContent.Append("asadsadsada");

                byte[] data = Encoding.ASCII.GetBytes(postContent.ToString());
                requestScore.Method = "Post";
                requestScore.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                //requestScore.ContentType = "text/xml";
                requestScore.ContentLength = data.Length;
                requestScore.KeepAlive = true;

                //解决IIS配置
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                Stream stream = requestScore.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse responseSorce;
                try
                {
                    responseSorce = (HttpWebResponse)requestScore.GetResponse();
                }
                catch (WebException ex)
                {
                    responseSorce = (HttpWebResponse)ex.Response;//得到请求网站的详细错误提示
                }
                StreamReader reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();
                requestScore.Abort();
                responseSorce.Close();
                responseSorce.Close();
                reader.Dispose();
                stream.Dispose();
                string strReturn = HttpUtility.UrlDecode(content);
                return HttpUtility.UrlDecode(content);
            }
            #endregion

            #region 生成上传ASRS单号

            public string CreateAsrsNo()
            {
                this.ControlMethodName = "CreateAsrsNo";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strAsrsNo = "";
                DataTable dt = new DataTable();
                string sql = "EXEC [dbo].[GetAsrsID]  ";

                try
                {
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sql);// ControlSqlAccess.ExecSql(sql.ToString());

                    ControlSqlAccess.CloseConnection();
                    if (dt.Rows.Count > 0)
                    {
                        strAsrsNo = dt.Rows[0][0].ToString().Trim();
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
                return strAsrsNo;
            }

            #endregion

            #region 生成上传seq_no单号

            public string Createseq_no(int j)
            {

                string strAsrsNo = "";
                DataTable dt = new DataTable();
                string sql = "select Replicate('0',4-Len(Convert(varchar," + j + "))) + Convert(varchar," + j + ") ";


                ControlHandleDB();
                dt = ControlSqlAccess.GetDataTable(sql);// ControlSqlAccess.ExecSql(sql.ToString());

                ControlSqlAccess.CloseConnection();
                if (dt.Rows.Count > 0)
                {
                    strAsrsNo = dt.Rows[0][0].ToString().Trim();
                }


                return strAsrsNo;
            }

            #endregion

            #region 判断是否为ASRS仓别
            public bool CheckLGORT(string varPlant, string varLgort)
            { 
                bool bolResult = false;
                string sbSql = " SELECT CTRLC4 FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS'AND CTRLID='LGORT'AND CTRLNM='" + varPlant + "'AND CTRLC1='" + varLgort + "'";

                ControlHandleDB();
                string strType = ControlSqlAccess.GetFieldValue(sbSql);
                if (strType == "ASRS")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion

            #region 判断ASRS仓别验证储位的唯一性
            //=========================================================================
            ////////////Summary by Refun Zhan////////////////////////////////////////////
            /// <summary>
            ///仓别类型为ASRS,检验储位是否重复，不同仓别，储位也不允许重复，保证ASRS储位的唯一性
            /// </summary> 
            /// <param name="strWerks">廠區?/param>
            /// <param name="strLgort">�倉別?/param>
            /// <param name="strLocat">�儲?/param>          
            /// <returns>
            /// DataTable?
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckASRSLocExisted(string strWerk, string strLocat)
            {
                //���設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckASRSLocExisted";
                this.ControlMethodParm = "('" + strWerk + "','" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log?..
                {
                    ControlHandleError("000", "", "");
                }
                bool bolResult = false;
                string sbSql = "SELECT WERKS,LGORT,LOCAT FROM WHHED WHERE WERKS='" + strWerk + "' AND LOCAT='" + strLocat + "' ";

                ControlHandleDB();
                DataTable dtData = ControlSqlAccess.GetDataTable(sbSql);
                if (dtData.Rows.Count > 0)
                {
                    bolResult = true;
                }

                return bolResult;
            }
            #endregion

            #region 判断是否ASRS空储位strLocat
            public bool CheckLOCAT(string varLocat)
            {
                bool bolResult = false;
                var json = new
                {
                    LOCAT = varLocat,
                };

                //旧：固定写死
                //string strUrl = "https://172.19.81.219/AlimAPI/api/QsmcQwms/GetStockStatus";

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //域名配置在数据库中
                string strUrl = objAuthority.GetApiRequestUrl();
                strUrl = strUrl + "/QWMSAPI/api/QsmcQwms/GetStockStatus";

                string strData = JsonConvert.SerializeObject(json);
                string strResult = HttpPostByHttpWebRequest(strUrl, strData);
                string strType = JsonConvert.DeserializeObject<string>(strResult);
                if (strType != "0")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion

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

                    ////解决IIS配置
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
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
