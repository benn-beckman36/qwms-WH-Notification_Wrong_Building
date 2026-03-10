using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWMS.Common;
using QWMS.Entity;
using Qci.Base.Common;
using System.Data;
using System.Collections;

namespace QCI
{
    namespace QWMS
    {
        public class Alim_Storage: ControlBase
        {
            #region 变量
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strCrnam = "";
            private string strProgid = "";
            private string strErrmsg = "";


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
            /// CompanyCode
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
            /// 錯誤訊息。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }
            #endregion

            #region 构造函数
            public Alim_Storage(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
                //PROGID = strProgid;

            }


            public Alim_Storage(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
            {
                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                CRNAM = varUserData.UserId;
                PROGID = strProgid;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.Alim_Storage";
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

            #region 获取可使用的流道信息
            /// <summary>
            /// 获取可使用的主流道信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <returns></returns>
            public DataTable getMainRun(string strWerks, string strLgort)
            {
                this.ControlMethodName = "getMainRun";
                this.ControlMethodParm = "('" + strWerks + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                DataTable dtResult = new DataTable();
                sbSql.Append("SELECT WERKS,LGORT,MAINRUN FROM View_WERKS_LGORT_RUNNER ORDER BY MAINRUN");
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 获取单据号绑定的流道信息
            /// <summary>
            /// 获取单据号绑定的流道信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            public DataTable getMblnrRun(string strWerks, string strLgort, string strMblnr, string strMainrun)
            {
                this.ControlMethodName = "getMblnrRun";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                DataTable dtResult = new DataTable();
                sbSql.Append("SELECT DISTINCT WERKS,LGORT,MAINRUN,MBLNR FROM ALRIN WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LEFT(MBLNR,10)='" + strMblnr + "' ");
                if (strMainrun != "")
                {
                    sbSql.Append("  AND MAINRUN='" + strMainrun + "'");
                }
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 绑定流道关系
            /// <summary>
            /// 绑定流道关系
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <param name="strMainrun"></param>
            /// <returns></returns>
            public bool lockRunner(string strWerks, string strLgort, string strMblnr, string strMainrun)
            {
                this.ControlMethodName = "lockRunner";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + strMainrun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strSql = "INSERT INTO ALRIN(MANDT,WERKS,LGORT,MAINRUN,CGCLS,MBLNR,CRNAM,CRDAT,COMCD) VALUES('" + MANDT + "','" + strWerks + "','" + strLgort + "','" + strMainrun + "','"+PROGID+"','" + strMblnr + "','" + UserData.UserId + "',GETDATE(),'" + COMCD + "')";
                bool blResult;
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strSql);
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

            #region 解除流道绑定关系
            /// <summary>
            /// 解除流道绑定关系
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <param name="strMainrun"></param>
            /// <returns></returns>
            public bool openRunner(string strWerks, string strLgort, string strMblnr, string strMainrun)
            {
                this.ControlMethodName = "openRunner";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + strMainrun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                bool blResult;
                sbSql.Append("DELETE ALRIN WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND MBLNR='" + strMblnr + "'AND MAINRUN='" + strMainrun + "' ");
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

            #region 获取入库中间表单据数量信息，若无，则插入
            /// <summary>
            /// 获取入库中间表单据数量信息，若无，则插入
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            public DataTable getMblnrIn(string strWerks, string strLgort, string strMblnr)
            {
                this.ControlMethodName = "getMblnrIn";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                string strSql = "EXEC SP_CreateMblnr_AlimIn '" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strMblnr + "','" + UserData.UserId + "'";
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql);
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

            #region 获取ALGIN入库中间表单据信息,with(nolock)
            /// <summary>
            /// 获取ALGIN入库中间表单据信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            public DataTable getAlGinMblnr(string strWerks, string strLgort, string strMblnr)
            {
                this.ControlMethodName = "getAlGinMblnr";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                string strSql = "SELECT MANDT,COMCD,WERKS,LGORT,MBLNR,MATNR,MENGE,ALQTY FROM ALGIN WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND MBLNR='" + strMblnr + "'";
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSql);
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

            #region 回退中间表数据ALGIN到WHDWN
            public bool backWhdwn(DataTable dtReturn, DataTable dtOrigin)
            {
                this.ControlMethodName = "backWhdwn";
                this.ControlMethodParm = "('" + dtReturn + dtOrigin + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataAlgin objAlgin = new DataAlgin(UserData);
                DataTable dtResult = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList alConditions = new ArrayList();
                bool blResult = false;
                foreach (DataRow dr in dtReturn.Rows)
                {
                    //回退数据
                    DataRow[] drWhdwn = dtOrigin.Select("MATNR='" + dr["MATNR"] + "'");
                    foreach (DataRow dw in drWhdwn)
                    {
                        int intAlqty = Convert.ToInt32(dw["MENGE"].ToString()) - Convert.ToInt32(dw["OTQTY"].ToString());
                        if (intAlqty <= Convert.ToInt32(dr["ALQTY"].ToString()))
                        {
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Otqty = dw["MENGE"].ToString();
                            objWhdwn.Modat = "GetDate()";
                            alConditions.Add(" WERKS='" + dw["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dw["LGORT"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dw["MBLNR"].ToString() + "'");
                            alConditions.Add(" ZEILE='" + dw["ZEILE"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dw["COMCD"].ToString() + "'");
                            alConditions.Add(" MANDT='" + dw["MANDT"].ToString() + "'");
                            arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                            dr["ALQTY"] = (Convert.ToInt32(dr["ALQTY"].ToString()) - intAlqty).ToString();

                        }
                        else
                        {
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Otqty = (Convert.ToInt32(dw["OTQTY"].ToString()) + Convert.ToInt32(dr["ALQTY"].ToString())).ToString();
                            objWhdwn.Modat = "GetDate()";
                            alConditions.Add(" WERKS='" + dw["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dw["LGORT"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dw["MBLNR"].ToString() + "'");
                            alConditions.Add(" ZEILE='" + dw["ZEILE"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dw["COMCD"].ToString() + "'");
                            alConditions.Add(" MANDT='" + dw["MANDT"].ToString() + "'");
                            arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                            dr["ALQTY"] = "0";

                        }
                        if (dr["ALQTY"].Equals("0"))
                        {
                            break;
                        }
                            
                    }

                    //删除中间表数据
                    alConditions.Clear();
                    objAlgin.ResetField();

                    alConditions.Add(" MANDT='" + dr["MANDT"].ToString() + "'");
                    alConditions.Add(" COMCD='" + dr["COMCD"].ToString() + "'");
                    alConditions.Add(" WERKS='" + dr["WERKS"].ToString() + "'");
                    alConditions.Add(" LGORT='" + dr["LGORT"].ToString() + "'");
                    alConditions.Add(" MBLNR='" + dr["MBLNR"].ToString() + "'");
                    alConditions.Add(" MATNR='" + dr["MATNR"].ToString() + "'");
                    arySQL.Add(objAlgin.EntityGetDeleteSql(alConditions));
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

            #region 获取原始单据信息
            /// <summary>
            /// 获取原始单据信息WHDWN
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            public DataTable getWhdwnMblnr(string strWerks, string strLgort, string strMblnr, string strBwart, string strType, string strFdate, string strTdate)
            {
                this.ControlMethodName = "getWhdwnMblnr";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                if(strType=="listSapInData")//查询未处理的单据
                {
                    sbSql.AppendFormat("SELECT DISTINCT SUBSTRING(MBLNR,1,10) AS MBLNR FROM WHDWN WITH(NOLOCK) WHERE MANDT='" + MANDT + "' ");
                    if (strFdate == strTdate)
                    {
                        sbSql.AppendFormat(" AND CONVERT(VARCHAR(8),CRDAT,112)='{0}' ", strTdate);
                    }
                    else
                    {
                        sbSql.AppendFormat(" AND CONVERT(VARCHAR(8),CRDAT,112)  BETWEEN '{0}' AND '{1}'", strFdate, strTdate);
                    }
                }
                else if (strType == "getOnlineInMblnr")//查询单据信息
                {
                    sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,MBLNR,ZEILE,MATNR,MENGE,OTQTY,INSMK,CHARG,EBELN FROM WHDWN WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND LEFT(MBLNR,10)='" + strMblnr + "' ");
                }
                if(strBwart!="")
                {
                    sbSql.AppendFormat(" AND BWART='" + strBwart + "'");
                }
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat("AND MENGE> OTQTY  ", "");
                sbSql.AppendFormat("AND MTYPE='{0}' ", "SAP");
                sbSql.AppendFormat("AND TRNTP in ('G+','R+','M+') ","");
                
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 查詢Group ID
            /// <summary>
            /// 查詢Group ID
            /// </summary> 
            /// <param name="varDate"> ID日期。</param>
            /// <param name="varOutType">出庫類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            public DataTable QueryGroupIdData(string strWerks,string strLgort, string varDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdData";
                this.ControlMethodParm = "(" + varDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT GRPID FROM ALSMT WHERE MANDT='218' AND COMCD='{0}' AND WERKS='{1}' AND LGORT='{2}' AND (CRDAT BETWEEN '" + varDate + " 00:00:00.000' and '" + varDate + " 23:59:59.999') GROUP BY GRPID HAVING COUNT(MBLNR)=0 ", COMCD, strWerks, strLgort);

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

            #region 查詢Group ID(只帶出兩小時內未扣账的id)
            /// <summary>
            /// 查詢Group ID
            /// </summary> 
            /// <param name="varDate"> ID日期。</param>
            /// <param name="varOutType">出庫類型。</param>
            ///  <param name="dtDateTime">dtDataTable。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            public DataTable QueryGroupIdData(string strWerks, string strLgort, string varDate, DataTable dtDateTime)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + varDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                string strHour = (int.Parse(dtDateTime.Rows[0]["NowHour"].ToString()) - 2).ToString();
                string NowMinute = dtDateTime.Rows[0]["NowMinute"].ToString();
                string strTimeFrom = varDate + " " + strHour + ":" + NowMinute + ":00.000";
                string strTimeTo = varDate + " " + dtDateTime.Rows[0]["NowHour"].ToString() + ":" + NowMinute + ":50.999";

                sbSql.AppendFormat("SELECT GRPID FROM ALSMT WHERE MANDT='218' AND COMCD='{0}' AND WERKS='{1}' AND LGORT='{2}' AND (CRDAT BETWEEN '{3}' and '{4}')  GROUP BY GRPID,MBLNR HAVING MBLNR='' ", COMCD, strWerks, strLgort, strTimeFrom, strTimeTo);

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

            #region 获取GroupID的详细信息
            /// <summary>
            /// 获取GroupID的详细信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="varDate"></param>
            /// <param name="strGrpID"></param>
            /// <returns></returns>
            public DataTable QuerySmtIDData(string strWerks, string strLgort, string strGrpID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryGroupIdData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strGrpID + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtResult = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                //sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,KOSTL,GRPID,MATNR,CHARG,MENGE, LOCAT,DACOD,LOCOD,LIFNR,UMLGO,DIDNO,MBLNR,ZEILE, SERNO,Line,Side,Machine,SDTSlot,SDTLr,CRDAT FROM ALSMT WHERE MANDT='218' AND COMCD='{0}' AND WERKS='{1}' AND LGORT='{2}' AND GRPID='{3}' ", COMCD, strWerks, strLgort, strGrpID);
                sbSql.AppendFormat("SELECT S.MANDT,S.COMCD,S.WERKS,S.LGORT,S.KOSTL,S.GRPID,S.MATNR,S.CHARG,S.MENGE, S.LOCAT,I.DACOD,I.LOCOD,I.LIFNR,S.UMLGO,S.DIDNO,S.MBLNR,S.ZEILE, S.SERNO,S.Line,S.Side,S.Machine,S.SDTSlot,S.SDTLr,S.CRDAT,I.Diameter,I.Thickness  FROM ALSMT S WITH(NOLOCK)INNER JOIN ALITM I ON S.MANDT=I.MANDT AND S.COMCD=I.COMCD AND S.WERKS=I.WERKS AND S.LGORT=I.LGORT AND S.LOCAT=I.LOCAT WHERE S.MANDT='218' AND S.COMCD='{0}' AND S.WERKS='{1}' AND S.LGORT='{2}' AND GRPID='{3}' AND I.ITEMSTATES='N' AND I.STOCSTATES='D'", COMCD, strWerks, strLgort, strGrpID);

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

            #region 校验祥龙待出库数据当前状态
            /// <summary>
            /// 校验祥龙待出库数据当前状态
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMatnr"></param>
            /// <returns></returns>
            public bool IsOutStore(string strWerks, string strLgort, string strMatnr,string strLocat,string strMenge)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "IsOutStore";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strMatnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool blRestlt = false;
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT COUNT(1) FROM ALITM WHERE MANDT='{0}' AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND MATNR='{4}' AND LOCAT='{5}' AND MENGE='{6}' AND  ITEMSTATES='N' AND STOCSTATES='D'", MANDT, COMCD, strWerks, strLgort, strMatnr, strLocat, strMenge);

                try
                {
                    ControlHandleDB();
                    DataTable dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    if ( int.Parse(dtResult.Rows[0][0].ToString()) > 0)
                    {
                        blRestlt = true;
                    }
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdData()";
                }

                return blRestlt;
            }
            #endregion

            #region 获取储位的优先级和柜号
            /// <summary>
            /// 获取储位的优先级和柜号
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strLocat"></param>
            /// <returns></returns>
            public DataTable getLocatPRI(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getLocatPRI";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strLocat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtRestlt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT PRI,CONTRNO FROM ALHED WHERE MANDT='{0}' AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND LOCAT='{4}'", MANDT, COMCD, strWerks, strLgort, strLocat);

                try
                {
                    ControlHandleDB();
                    dtRestlt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdData()";
                }

                return dtRestlt;
            }
            #endregion

            #region 产生虚拟单据
            /// <summary>
            /// 产生虚拟单据信息
            /// </summary>
            /// <param name="dtOutSource"></param>
            /// <returns></returns>
            public DataSet ProduceSapSimulationData(DataTable dtOutSource)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ProduceSapSimulationData";
                this.ControlMethodParm = "(" + dtOutSource + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                DataTable dtReturn = new DataTable();
                DataTable dtData = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList aryInsertSQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataAlsmt objAlsmt = new DataAlsmt(UserData);
                DataSet dsData = new DataSet();
                string CYear = "";
                string CMonth = "";
                string CDay = "";
                string strSerno = "";
                string strMblnr = "";
                string strZeile = "";
                string strBudat = "";
                string strHeader = "";
                int intZeile = 0;

                #region 取得當天日期：WHDWN.BUDAT
                DateTime dtnow = DateTime.Now;
                CMonth = dtnow.Month.ToString();
                CDay = dtnow.Day.ToString();
                if (CMonth.Length < 2)
                {
                    CMonth = Convert.ToString("0") + dtnow.Month;
                }
                if (CDay.Length < 2)
                {
                    CDay = Convert.ToString("0") + dtnow.Day;
                }
                CYear = Convert.ToString(dtnow.Year);
                strBudat = CYear + CMonth + CDay;
                #endregion


                #region  儲存SMT Data

                dtReturn.Columns.Add("MBLNR");
                //產生的虛擬單據By廠區作區分
                strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    #region Insert WHDWN
                    //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                    if (i % 20 == 0)
                    {
                        strSerno = GetSimulationNum();  //取得虛擬單據新的流水號
                        intZeile = 0;  //Item編號重新編號
                    }

                    strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                    strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                    //將單據編號存入DataTable內並回傳扣帳
                    dtReturn.Rows.Add();  
                    dtReturn.Rows[i]["MBLNR"] = strMblnr + strZeile;
                    dtOutSource.Rows[i]["MBLNR"] = strMblnr + strZeile;
                    dtOutSource.Rows[i]["ZEILE"] = strZeile;

                    alColumns.Clear();
                    alConditions.Clear();
                    objWhdwn.ResetField();

                    objWhdwn.Mandt = MANDT;
                    objWhdwn.Comcd = COMCD;
                    objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                    objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString(); ;  //發料倉
                    objWhdwn.Prcde = "Y";
                    objWhdwn.Mblnr = strMblnr + strZeile;
                    objWhdwn.Zeile = strZeile;
                    objWhdwn.Bwart = "311";  //SMT:311
                    objWhdwn.Mtype = "QMS_QS";
                    objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                    objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString(); //(Final才有)
                    objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                    objWhdwn.Qwqty = dtOutSource.Rows[i]["MENGE"].ToString();  //QWMS的庫存數量
                    objWhdwn.Insmk = "G";
                    objWhdwn.Trntp = "T-";
                    objWhdwn.Putyp = "";
                    objWhdwn.Lifnr = "";
                    objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                    objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                    objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                    objWhdwn.Ebeln = "";  //purchase order
                    objWhdwn.Ebelp = "";  //purchase order item
                    objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                    objWhdwn.Kostl = dtOutSource.Rows[i]["KOSTL"].ToString();  //Cost Center
                    objWhdwn.Reslt = "";
                    objWhdwn.Budat = "";  //Posting Date in Doc(扣帳時間)
                    objWhdwn.Prity = "";  //Priority
                    objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                    objWhdwn.Crdat = "GetDate()";
                    objWhdwn.Modat = "GetDate()";
                    objWhdwn.Usnam = CRNAM;

                    arySQL.Add(objWhdwn.EntityGetInsertSql());

                    #endregion

                    #region  Update ALSMT
                    aryInsertSQL.Clear();
                    aryInsertSQL = InsertSmtData(dtOutSource, strMblnr + strZeile, strZeile, i);
                    for (int j = 0; j < aryInsertSQL.Count; j++)
                        arySQL.Add(aryInsertSQL[j].ToString());
                    #endregion
                }
                dsData.Tables.Add(dtReturn);
                dsData.Tables.Add(dtOutSource);
                #endregion

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

                return dsData;
            }
            #endregion

            #region 产生虚拟单据的流水号
            //====================================================================================
            ////////////Summary by Smose Liao 20100308////////////////////////////////////////////
            /// <summary>
            /// 产生虚拟单据的流水号
            /// </summary> 
            /// <param name="strCtrlnm">祥天計畫/QCI TH20</param>
            /// <param name="strType">SMT/FINAL</param>
            /// <returns>
            /// String。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageOut objStorageOut =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.GetSimulationNum(strCtrlnm, strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public string GetSimulationNum()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSimulationNum";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strSerno = "";

                try
                {
                    strSQL = "EXEC SP_GetSerialNum_Fly 'SimulationDoc', 'FLYPLAN'";
                    ControlHandleDB();
                    strSerno = ControlSqlAccess.GetFieldValue(strSQL);
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

            #region 将虚拟单据信息存回到ALSMT
            /// <summary>
            /// 将虚拟单据信息存回到ALSMT
            /// </summary>
            /// <param name="dtOutSource"></param>
            /// <param name="strMblnr"></param>
            /// <param name="strZeile"></param>
            /// <param name="i"></param>
            /// <returns></returns>
            public ArrayList InsertSmtData(DataTable dtOutSource, string strMblnr, string strZeile,int i)
            {
                DataTable dtWhSmt = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList arySQL = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataAlsmt objAlsmt = new DataAlsmt(UserData);

                #region  将虚拟单据信息存回到ALSMT
                alColumns.Clear();
                alConditions.Clear();
                objAlsmt.ResetField();

                objAlsmt.Mblnr = strMblnr;//這次新產生的單據號碼
                objAlsmt.Zeile = strZeile;//這次新產生的單據item
                //objAlsmt.Menge = dtOutSource.Rows[i]["MENGE"].ToString();//這次新發出的數量 
                objAlsmt.Modat = "GetDate()";//修改的日期與時間
                objAlsmt.Flage = "Y";
                alConditions.Add(" WERKS='" + dtOutSource.Rows[i]["WERKS"].ToString() + "'");
                alConditions.Add(" GRPID='" + dtOutSource.Rows[i]["GRPID"].ToString() + "'");
                alConditions.Add(" MATNR='" + dtOutSource.Rows[i]["MATNR"].ToString() + "'");
                alConditions.Add(" KOSTL='" + dtOutSource.Rows[i]["KOSTL"].ToString() + "'");
                alConditions.Add(" LOCAT='" + dtOutSource.Rows[i]["LOCAT"].ToString() + "'");

                arySQL.Add(objAlsmt.EntityGetUpdateSql(alConditions));
                #endregion

                return arySQL;
            }
            #endregion

            #region 出库数据插入到中间表AGOUT
            /// <summary>
            /// 出库数据插入到中间表AGOUT
            /// </summary>
            /// <param name="dtOutSource"></param>
            /// <returns></returns>
            public bool InsertSMTAgoutData(DataTable dtOutSource)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InsertSMTAgoutData";
                this.ControlMethodParm = "(" + dtOutSource + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                ArrayList arySQL = new ArrayList();
                DataAgout objAgout = new DataAgout(UserData);
                DataWhlog objDataWhlog = new DataWhlog(UserData);

                foreach(DataRow dr in dtOutSource.Rows)
                {
                    //出库中间表
                    objAgout.ResetField();

                    objAgout.Mandt = MANDT;
                    objAgout.Comcd = COMCD;
                    objAgout.Werks = dr["WERKS"].ToString();
                    objAgout.Lgort = dr["LGORT"].ToString();
                    objAgout.Pri = dr["PRI"].ToString();
                    objAgout.Subpri = dr["SUBPRI"].ToString();
                    objAgout.Didno = dr["DIDNO"].ToString();
                    objAgout.Mblnr = dr["MBLNR"].ToString();
                    objAgout.Zeile = dr["ZEILE"].ToString();
                    objAgout.Locat = dr["LOCAT"].ToString();
                    objAgout.Contrno = dr["CONTRNO"].ToString();
                    objAgout.X = dr["X"].ToString();
                    objAgout.Y = dr["Y"].ToString();
                    objAgout.Z = dr["Z"].ToString();
                    objAgout.R = dr["R"].ToString();
                    objAgout.Diameter = dr["Diameter"].ToString();
                    objAgout.Thickness = dr["Thickness"].ToString();
                    objAgout.Matnr = dr["MATNR"].ToString();
                    objAgout.Charg = dr["CHARG"].ToString();
                    objAgout.Menge = dr["MENGE"].ToString();
                    objAgout.Insmk = "G";
                    objAgout.Lifnr = dr["LIFNR"].ToString();
                    objAgout.Dacod = dr["DACOD"].ToString();
                    objAgout.Locod = dr["LOCOD"].ToString();
                    objAgout.Serno = dr["SERNO"].ToString();
                    objAgout.Line = dr["Line"].ToString();
                    objAgout.Side = dr["Side"].ToString();
                    objAgout.Machine = dr["Machine"].ToString();
                    objAgout.SDTSlot = dr["SDTSlot"].ToString();
                    objAgout.SDTLr = dr["SDTLr"].ToString();
                    objAgout.Crnam = UserData.UserId;
                    objAgout.Crdat = DateTime.Now.ToString();
                    objAgout.Flage = "N";

                    arySQL.Add(objAgout.EntityGetInsertSql());

                    //日志表WHLOG
                    objDataWhlog.ResetField();

                    objDataWhlog.Mandt = MANDT;
                    objDataWhlog.Comcd = COMCD;
                    objDataWhlog.Werks = dr["WERKS"].ToString();
                    objDataWhlog.Lgort = dr["LGORT"].ToString();
                    objDataWhlog.Cgcls = "Z15";
                    objDataWhlog.Oloca = dr["LOCAT"].ToString();
                    objDataWhlog.Nloca = "";
                    objDataWhlog.Trntp = "T-";
                    objDataWhlog.Matnr = dr["MATNR"].ToString();
                    objDataWhlog.Mblnr = dr["MBLNR"].ToString();
                    objDataWhlog.Menge = (0 - Convert.ToInt32(dr["MENGE"].ToString())).ToString();
                    objDataWhlog.Rmak1 = "";
                    objDataWhlog.Crnam = UserData.UserId;
                    objDataWhlog.Crdat = "getdate()";
                    objDataWhlog.Insmk = "G";
                    objDataWhlog.Locod = dr["LOCOD"].ToString();
                    objDataWhlog.Grpid = dr["GRPID"].ToString();
                    objDataWhlog.Dacod = dr["DACOD"].ToString();
                    objDataWhlog.Lifnr = dr["LIFNR"].ToString();

                    arySQL.Add(objDataWhlog.EntityGetInsertSql());

                }


                //修改库存数据
                //for (int j = 0; j < dtOutSource.Rows.Count; j++)
                //{
                //    string sql = "UPDATE ALITM SET STOCSTATES='D' WHERE WERKS=" +
                //    "'" + dtOutSource.Rows[j]["WERKS"].ToString() + "'" + "AND LGORT=" +
                //    "'" + dtOutSource.Rows[j]["LGORT"].ToString() + "'" + "AND LOCAT=" +
                //    "'" + dtOutSource.Rows[j]["LOCAT"].ToString() + "'" + "AND MATNR=" +
                //    "'" + dtOutSource.Rows[j]["MATNR"].ToString() + "'" + "AND ITEMSTATES='N' AND STOCSTATES='N'";
                //    arySQL.Add(sql.ToString());
                //}
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

                return bolReturn;
            }

            public bool StorageOut_SimulationOut(DataTable dtOutSource)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "StorageOut_SimulationOut";
                this.ControlMethodParm = "(" + dtOutSource + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                #region 變數宣告
                StringBuilder sbSql = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                #endregion

                #region Insert AGOUT 出库中间表
                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    string sql = "INSERT INTO AGOUT(MANDT,COMCD,WERKS,LGORT,PRI,SUBPRI,DIDNO,MBLNR,ZEILE,LOCAT,MATNR,CHARG,MENGE,INSMK,LIFNR,DACOD,LOCOD,SERNO,CRNAM,CRDAT,FLAGE)" +
                            "VALUES('" + dtOutSource.Rows[i]["MANDT"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["COMCD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["WERKS"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["LGORT"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["PRI"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["SUBPRI"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["DIDNO"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["LOCAT"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["MATNR"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["CHARG"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["MENGE"].ToString() + "'" +
                            ",'G'" +
                            ",'" + dtOutSource.Rows[i]["LIFNR"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["DACOD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["LOCOD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[i]["SERNO"].ToString() + "'" +
                            ",'" + UserData.UserId + "'" +
                            ",GETDATE()" +
                            ",'N')";
                    arySQL.Add(sql.ToString());
                }
                #endregion

                #region Insert WHLOG 日志记录表
                for (int k = 0; k < dtOutSource.Rows.Count; k++)
                {
                    string sql = "INSERT INTO WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,CHARG,LIFNR,TRNTP,MENGE,INSMK,CRNAM,CRDAT,SERNO,COMCD,LOCOD,DACOD,MBLNR,GRPID,OMBLN,RMAK1)" +
                            "VALUES('" + dtOutSource.Rows[k]["MANDT"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["WERKS"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["LGORT"].ToString() + "'" +
                            ",'Z15'" +
                            ",'" + dtOutSource.Rows[k]["LOCAT"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["MATNR"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["CHARG"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["LIFNR"].ToString() + "'" +
                            ",'T-'" +
                            ",'" + (0 - Convert.ToInt32(dtOutSource.Rows[k]["MENGE"].ToString())).ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["INSMK"].ToString() + "'" +
                            ",'" + UserData.UserId + "'" +
                            ",GETDATE()" +
                            ",'" + dtOutSource.Rows[k]["SERNO"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["COMCD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["LOCOD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["DACOD"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["MBLNR"].ToString() + "'" +
                            ",'" + dtOutSource.Rows[k]["GRPID"].ToString() + "'" +
                            ",''" +
                            ",N'祥龙出库')";
                    arySQL.Add(sql.ToString());
                }
                #endregion

                bool bolReturn = false;
                try
                {
                    StringBuilder alSQL = new StringBuilder();
                    for (int i = 0; i < arySQL.Count; i++)
                    {
                        alSQL.AppendLine(" " + arySQL[i].ToString() + "");
                    }
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSql(alSQL.ToString());
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
                return bolReturn;
            }
            #endregion

            #region 获取料号的直径和高度
            /// <summary>
            /// 获取料号的直径和高度
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strLocat"></param>
            /// <returns></returns>
            public DataTable getMatnrAttr(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getMatnrAttr";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strLocat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtRestlt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT Diameter,Thickness,DACOD,LOCOD,LIFNR FROM ALITM WITH(NOLOCK) WHERE MANDT='{0}' AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND LOCAT='{4}'", MANDT, COMCD, strWerks, strLgort, strLocat);

                try
                {
                    ControlHandleDB();
                    dtRestlt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryGroupIdData()";
                }

                return dtRestlt;
            }
            #endregion

        }
    }
}
