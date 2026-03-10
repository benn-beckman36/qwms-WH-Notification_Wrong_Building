using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCI
{
    namespace QWMS
    {
        public class StorageLocation_AGV : ControlBase
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
            public StorageLocation_AGV()
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
            public StorageLocation_AGV(UserInfo varUserData, string varProgid)
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
            public StorageLocation_AGV(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
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
            public StorageLocation_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string strProgid)
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
            public StorageLocation_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.Inventory_AGV";
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

            #region Method
            public DataTable queryLocat(string strWerks, string strLgort, string strSize, string strMarNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getLocal";
                this.ControlMethodParm = "('" + strMarNo + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region Code here
                    StringBuilder strSQL = new StringBuilder();

                    if (strSize == "7")
                    {
                        strSQL.Append(@" SELECT I.WERKS AS '厂区',I.LGORT AS '仓别',I.MARNO AS '料架',SUBSTRING(I.LOCAT,1,3) AS '储位前3码',cast(count(I.MARNO) as varchar(5))+'/'+'720' AS '容积率' FROM WHITM I WITH(NOLOCK)
                                            INNER JOIN WHHED H WITH (NOLOCK) ON I.LOCAT = H.LOCAT AND I.WERKS=H.WERKS AND I.LGORT = H.LGORT AND I.LOCAT = H.LOCAT
                                            WHERE H.LOCTYPE = 'AGV'
                                            and len(I.LOCAT) = '8' ");
                        if (!string.IsNullOrEmpty(strWerks))
                        {
                            strSQL.AppendFormat(" AND I.WERKS = '{0}' ", strWerks);
                        }
                        if (!string.IsNullOrEmpty(strLgort))
                        {
                            strSQL.AppendFormat(" AND I.LGORT = '{0}' ", strLgort);
                        }
                        if (!string.IsNullOrEmpty(strMarNo))
                        {
                            strSQL.AppendFormat(" AND I.MARNO ='{0}' ", strMarNo);
                        }
                        strSQL.Append(@" group by I.WERKS,I.LGORT,I.MARNO,SUBSTRING(I.LOCAT,1,3)
                                     order by I.WERKS,I.LGORT,I.MARNO,SUBSTRING(I.LOCAT,1,3) ");
                    }
                    else if (strSize == "13")
                    {
                        strSQL.AppendFormat(@" SELECT I.WERKS AS '厂区',I.LGORT AS '仓别',I.MARNO AS '料架',SUBSTRING(I.LOCAT,1,8) AS '储位前8码',cast(count(I.MARNO) as varchar(5))+'/'+'8' AS '容积率' FROM WHITM I WITH(NOLOCK)
                                             INNER JOIN WHHED H WITH (NOLOCK) ON I.LOCAT = H.LOCAT AND I.WERKS=H.WERKS AND I.LGORT = H.LGORT AND I.LOCAT = H.LOCAT
                                             WHERE H.LOCTYPE = 'AGV'
                                             and len(I.LOCAT) = '11' ");
                        if (!string.IsNullOrEmpty(strWerks))
                        {
                            strSQL.AppendFormat(" AND I.WERKS = '{0}' ", strWerks);
                        }
                        if (!string.IsNullOrEmpty(strLgort))
                        {
                            strSQL.AppendFormat(" AND I.LGORT = '{0}' ", strLgort);
                        }
                        if (!string.IsNullOrEmpty(strMarNo))
                        {
                            strSQL.AppendFormat(" AND I.MARNO ='{0}' ", strMarNo);
                        }
                        strSQL.Append(@" group by I.WERKS,I.LGORT,I.MARNO,SUBSTRING(I.LOCAT,1,8)
                                     order by I.WERKS,I.LGORT,I.MARNO,SUBSTRING(I.LOCAT,1,8) ");

                    }
                    else
                    {
                        strSQL.AppendFormat(@" SELECT I.WERKS AS '厂区',I.LGORT AS '仓别',I.MARNO AS '料架',I.LOCAT AS '储位' FROM WHITM I WITH(NOLOCK)
                                             INNER JOIN WHHED H WITH (NOLOCK) ON I.LOCAT = H.LOCAT AND I.WERKS=H.WERKS AND I.LGORT = H.LGORT AND I.LOCAT = H.LOCAT
                                             WHERE H.LOCTYPE = 'AGV' ");
                        if (!string.IsNullOrEmpty(strWerks))
                        {
                            strSQL.AppendFormat(" AND I.WERKS = '{0}' ", strWerks);
                        }
                        if (!string.IsNullOrEmpty(strLgort))
                        {
                            strSQL.AppendFormat(" AND I.LGORT = '{0}' ", strLgort);
                        }
                        strSQL.AppendFormat(" AND SUBSTRING(I.LOCAT,1,3) ='{0}' ", strMarNo);

                        strSQL.Append(@" order by I.WERKS,I.LGORT,I.MARNO ");

                    }


                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
                    #endregion
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

            #region 更新出货库存状态
            /// <summary>
            /// 更新EC单状态
            /// </summary>
            /// <param name="strPnum"></param>
            /// <param name="strStatus"></param>
            /// <returns></returns>
            public bool updateWhitm(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "updateWhitm";
                this.ControlMethodParm = "(" + strLocat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult;
                string strsql = string.Empty;

                strsql = "Update WHITM set ITEMSTATES='Y',STOCSTATES = 'Y',MARNO = SUBSTRING(LOCAT,1,3) where WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT = '" + strLocat + "'";

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


            public DataTable queryPLACE(string strWerks, string strLgort, string strPLACE)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getLocal";
                this.ControlMethodParm = "('" + strPLACE + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region Code here
                    StringBuilder strSQL = new StringBuilder();

                    strSQL.AppendFormat(" select top 1 TASKID AS 'F_TEXT',TASKID AS 'F_VALUE' from  WHAGV WHERE WERKS = '{0}'and LGORT = '{1}' and PLACE = '{2}' ", strWerks, strLgort, strPLACE);
                 
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
                    #endregion
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

        }
    }
}
