using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Qci.Base.Common;
using QCI.QWMS;
using QWMS.Common;
using QWMS.Entity;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace QCI
{
    namespace QWMS
    {
        public class Admin : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strCrnam = "";
            private string strProgid = "";
            private string strErrmsg = "";

            #region Constructer


            public Admin(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
                //PROGID = strProgid;

            }

            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Admin物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString,strMandt,strCrnam,strProgid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Admin(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
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
                ControlErrorInfo.ObjectName = "QWMS.Admin";
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

            #region MemberFunction

            #region GetProgramVersion(取得程式版本資訊)
            //=========================================================================
            ////////////Summary by Ryan Tsai//////////////////////////////////////////
            /// <summary>
            /// 取得程式版本
            /// </summary> 
            /// <param name="strPUVI">使用者程式版本</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(UserData, Progid);
            ///  DataTable dtData = objPlantData.GetProgramVersion();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable GetProgramVersion(string strType, string strPUVI)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetProgramVersion";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    switch (strType)
                    {
                        case "New":
                            sbSql.Append("Select CTRLC5,CTRLN1,CTRLN2,CTRLN3,CTRLNM,CTRLC3  from WHCTRL WITH(NOLOCK) WHERE   ");
                            sbSql.AppendFormat("MANDT='QCI' ");
                            sbSql.AppendFormat("and SOLDTO='QWMS' ");
                            sbSql.AppendFormat("and CTRLID='VERSION' ");
                            sbSql.AppendFormat("and CTRLC1='Y' ");
                            break;
                        //测试使用-20231009
                        case "TEST":
                            sbSql.Append("Select CTRLC5,CTRLN1,CTRLN2,CTRLN3,CTRLNM,CTRLC3  from WHCTRL WITH(NOLOCK) WHERE   ");
                            sbSql.AppendFormat("MANDT='QCI' ");
                            sbSql.AppendFormat("and SOLDTO='QWMS' ");
                            sbSql.AppendFormat("and CTRLID='VERSION' ");
                            sbSql.AppendFormat("and CTRLC1='TEST' ");
                            break;
                        case "Update":
                            sbSql.Append("Select CTRLNM, CTRLC3 , REMAK  from WHCTRL WITH(NOLOCK) WHERE   ");
                            sbSql.AppendFormat("MANDT='QCI' ");
                            sbSql.AppendFormat("and SOLDTO='QWMS' ");
                            sbSql.AppendFormat("and CTRLID='VERSION' ");
                            sbSql.AppendFormat("and convert(int ,CTRLC2) > convert(int, (select CTRLC2 from WHCTRL WITH(NOLOCK) WHERE CTRLID='VERSION' and  CTRLNM='" + strPUVI + "')) ");
                            break;
                        case "Current":
                            sbSql.Append("Select CTRLC5,CTRLN1,CTRLN2,CTRLN3,CTRLNM,CTRLC3,REMAK  from WHCTRL WITH(NOLOCK) WHERE   ");
                            sbSql.AppendFormat("MANDT='QCI' ");
                            sbSql.AppendFormat("and SOLDTO='QWMS' ");
                            sbSql.AppendFormat("and CTRLID='VERSION' ");
                            sbSql.AppendFormat("and CTRLNM='" + strPUVI + "' ");
                            break;
                    }

                    DataTable dtData = new DataTable();
                    
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(sbSql.ToString());
                    sqlAccess.CloseConnection();

                    return dtData;
                }
                catch (CommonObjectsException ex)
                {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg; //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
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

            # region 檢查使用者是否有使用系統維護相關功能的權限
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用系統維護相關功能的權限
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.CheckAuthority();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
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
                    QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                    if (objAuthority.MAAUT.IndexOf(PROGID) < 0)
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
            # endregion

            #region 获得口罩仓别的权限人员
            public DataTable QueryMaskUser()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMaskUser";
                //this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhusr objWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strSQL ="SELECT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT='218' AND  SOLDTO='QWMS' AND CTRLID='MASK'" ;
                    DataTable dtData = new DataTable();
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSQL);
                    sqlAccess.CloseConnection();

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

            # region 获得CTO/BTO资料
            /// <summary>
            /// 获得CTO/BTO资料
            /// </summary>
            /// <example>
            /// <code>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  Your Code Here......
            /// </code>
            /// </example>
            /// <returns></returns>
            public DataTable GetCtoBto()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetCtoBto";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dt = new DataTable();

                    sbSql.Append("SELECT '' AS 'F_TEXT','' AS 'F_VALUE' UNION ");
                    sbSql.Append("SELECT CTRLC1 AS 'F_TEXT', CTRLNM AS 'F_VALUE' FROM WHCTRL WHERE CTRLID = 'CTBTO' ");
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSql.ToString());
                    sqlAccess.CloseConnection();
                    return dt;

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
            # endregion

            # region 获得洲别资料
            /// <summary>
            /// 获得洲别资料
            /// </summary>
            /// <example>
            /// <code>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  Your Code Here......
            /// </code>
            /// </example>
            /// <returns></returns>
            public DataTable GetRegion()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetRegion";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dt = new DataTable();

                    sbSql.Append("SELECT '' AS 'F_TEXT','' AS 'F_VALUE' UNION ");
                    sbSql.Append("SELECT CTRLC1 AS 'F_TEXT', CTRLNM AS 'F_VALUE' FROM WHCTRL WHERE CTRLID = 'REGON' ");
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSql.ToString());
                    sqlAccess.CloseConnection();
                    return dt;

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
            # endregion

            # region 获得机种资料
            /// <summary>
            /// 获得机种资料
            /// </summary>
            /// <example>
            /// <code>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  Your Code Here......
            /// </code>
            /// </example>
            /// <returns></returns>
            public DataTable GetMachine()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMachine";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dt = new DataTable();

                    sbSql.Append("SELECT '' AS 'F_TEXT','' AS 'F_VALUE' UNION ");
                    sbSql.Append("SELECT CTRLC1 AS 'F_TEXT', CTRLNM AS 'F_VALUE' FROM WHCTRL WHERE CTRLID = 'MAPID' ");
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dt;

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
            # endregion

            # region 删除储位资料
            /// <summary>
            /// 删除储位资料
            /// </summary> 
            /// <param name="strWerks">Plant</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="arlLocat">储位List</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteLocation(strWerks, strLgort, arlLocat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool DeleteLocation(string strWerks, string strLgort, ArrayList arlLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + arlLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arlSql = new ArrayList();
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhmap objWhmap = new DataWhmap(UserData);

                    ArrayList alConditions = new ArrayList();
                    LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, PROGID);
                    string strRmak = "Delete Location";
                    arlSql = objLogData.LocationLog(strWerks, strLgort, arlLocat, strRmak);
                    string strWhhed = "";
                    string strWhmap = "";
                    for (int i = 0; i < arlLocat.Count; i++)
                    {
                        alConditions.Clear();
                        alConditions.Add("MANDT='" + MANDT + "'");
                        alConditions.Add("COMCD='" + COMCD + "'");
                        alConditions.Add("WERKS='" + strWerks + "'");
                        alConditions.Add("LGORT='" + strLgort + "'");
                        alConditions.Add("LOCAT='" + arlLocat[i].ToString() + "'");
                        strWhhed = objWhhed.EntityGetDeleteSql(alConditions);
                        strWhmap = objWhmap.EntityGetDeleteSql(alConditions);
                        arlSql.Add(strWhhed);
                        arlSql.Add(strWhmap);

                    }

                    bool bolReturn = false;
                    try
                    {
                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                        ControlSqlAccess.CloseConnection();

                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- DeleteLocation()";
                    }
                    return bolReturn;
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
            #region 新增储位
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 新增储位
            /// </summary> 
            /// <param name="strWerks">Plant</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="arlLocat">储位(ArrayList)</param>
            /// <param name="strStset">座</param>
            /// <param name="strSthgh">高</param>
            /// <param name="strStlen">长</param>
            /// <param name="strStwid">宽</param>
            /// <param name="strCtbto">CTO/BTO</param>
            /// <param name="strRegon">Region</param>
            /// <param name="strMapid">Machine</param>
            /// <param name="bolPNLoc">料架儲位 </param>
            /// <param name="strPalQty">滿板數量 </param>
            /// <param name="strModel">機種</param>
            /// <param name="strVersion">版本</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.AddLocation(strWerks, strLgort, arrLocat, strStset, strSthgh, strStlen, strStwid,strCtbto,strRegon);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddLocation(string strWerks, string strLgort, ArrayList arlLocat, string strStset, string strSthgh, string strStlen, string strStwid, string strCtbto, string strRegon, string strMapid, string Floor, string Area, string Type, bool bolPNLoc, int intPalQty, string strModel, string strVersion, bool blIsAuto, string strIDNum, string strPdnam)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + arlLocat + "','" + strStset + "','" + strSthgh + "','" + strStlen + "','" + strStwid + "','" + strCtbto + "','" + strRegon + "','" + strMapid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region 變數宣告
                    ArrayList arlSql = new ArrayList();
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, PROGID);
                    string strRmak = "Insert Location";
                    arlSql = objLogData.LocationLog(strWerks, strLgort, arlLocat, strRmak);
                    #endregion
                    for (int i = 0; i < arlLocat.Count; i++)
                    {
                        string strSQL = "";
                        if (strStset != "" && strSthgh != "" && strStlen != "" && strStwid != "")
                        {
                            objWhhed.ResetField();

                            objWhhed.Mandt = MANDT;
                            objWhhed.Comcd = COMCD;
                            objWhhed.Werks = strWerks;
                            objWhhed.Lgort = strLgort;
                            objWhhed.Locat = arlLocat[i].ToString().Trim().ToUpper();
                            objWhhed.Losts = "0";
                            objWhhed.Ismrg = "N";
                            objWhhed.Stset = strStset;
                            objWhhed.Sthgh = strSthgh;
                            objWhhed.Stlen = strStlen;
                            objWhhed.Stwid = strStwid;
                            objWhhed.Crnam = CRNAM;
                            objWhhed.Crdat = "getdate()";
                            objWhhed.Monam = CRNAM;
                            objWhhed.Modat = "getdate()";
                            objWhhed.Ctbto = strCtbto;
                            objWhhed.Regon = strRegon;
                            objWhhed.Mapid = strMapid;
                            objWhhed.FLOOR = Floor;
                            objWhhed.AREA = Area;
                            objWhhed.TYPE = Type;

                            strSQL = objWhhed.EntityGetInsertSql();
                        }
                        else
                        {
                            objWhhed.ResetField();

                            objWhhed.Mandt = MANDT;
                            objWhhed.Comcd = COMCD;
                            objWhhed.Werks = strWerks;
                            objWhhed.Lgort = strLgort;
                            objWhhed.Locat = arlLocat[i].ToString().Trim().ToUpper();
                            objWhhed.Losts = "0";
                            objWhhed.Ismrg = "N";
                            objWhhed.Crnam = CRNAM;
                            objWhhed.Crdat = "getdate()";
                            objWhhed.Monam = CRNAM;
                            objWhhed.Modat = "getdate()";
                            objWhhed.Ctbto = strCtbto;
                            objWhhed.Regon = strRegon;
                            objWhhed.Mapid = strMapid;
                            objWhhed.FLOOR = Floor;
                            objWhhed.AREA = Area;
                            objWhhed.TYPE = Type;
                            //For QCMC AutoRunLocat
                            if (COMCD == "9110")
                            {
                                if (blIsAuto == true)
                                {
                                    objWhhed.ISATL = "Y";
                                    objWhhed.RIDNO = strIDNum;
                                    objWhhed.PDNAM = strPdnam;
                                }
                                else
                                {
                                    objWhhed.ISATL = "N";
                                }
                            }

                            //For CSMC JHX Model  Smose Liao 20141027
                            if (COMCD != "9110" && COMCD != "9501")
                            {
                                if (!bolPNLoc)
                                {
                                    objWhhed.PNLOC = "0";
                                }
                                else
                                {
                                    objWhhed.PNLOC = "1";
                                }
                                objWhhed.PALQTY = intPalQty.ToString();
                                objWhhed.MODEL = strModel;
                                objWhhed.VERSION = strVersion;
                            }

                            strSQL = objWhhed.EntityGetInsertSql();
                        }

                        arlSql.Add(strSQL);
                    }

                    bool bolReturn = false;
                    try
                    {
                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- AddLocation()";
                    }

                    return bolReturn;
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

            #region 更新储位
            public bool UpdateLocation(string strWerks, string strLgort, ArrayList arlLocat, string strCtbto, string strRegon, string strMapid, string Floor, string Area, string Type, bool bolPNLoc, int intPalQty, string strModel, string strVersion, bool blIsAuto, string strIDNum, string strPdnam)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + arlLocat + "','" + strCtbto + "','" + strRegon + "','" + strMapid + "','" + Floor + "','" + Area + "','" + Type + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region 變數宣告
                    ArrayList arlSql = new ArrayList();
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, PROGID);
                    string strRmak = "Update Location";
                    arlSql = objLogData.LocationLog(strWerks, strLgort, arlLocat, strRmak);
                    #endregion
                    for (int i = 0; i < arlLocat.Count; i++)
                    {
                        string strSQL = "";

                        objWhhed.ResetField();
                        objWhhed.Monam = CRNAM;
                        objWhhed.Modat = "getdate()";
                        objWhhed.Ctbto = strCtbto;
                        objWhhed.Regon = strRegon;
                        objWhhed.Mapid = strMapid;
                        objWhhed.FLOOR = Floor;
                        objWhhed.AREA = Area;
                        objWhhed.TYPE = Type;
                        //For QCMC AutoRunLocat
                        if (COMCD == "9110")
                        {
                            if (blIsAuto == true)
                            {
                                objWhhed.ISATL = "Y";
                                objWhhed.RIDNO = strIDNum;
                                objWhhed.PDNAM = strPdnam;
                            }
                            else
                            {
                                objWhhed.ISATL = "N";
                                objWhhed.RIDNO = "";
                                objWhhed.PDNAM = "";
                            }
                        }

                        //For CSMC JHX Model  Smose Liao 20141027
                        if (COMCD != "9110" && COMCD != "9501")
                        {
                            if (!bolPNLoc)
                            {
                                objWhhed.PNLOC = "0";
                            }
                            else
                            {
                                objWhhed.PNLOC = "1";
                            }
                            objWhhed.PALQTY = intPalQty.ToString();
                            objWhhed.MODEL = strModel;
                            objWhhed.VERSION = strVersion;
                        }

                        alConditions.Clear();
                        alConditions.Add("MANDT='" + MANDT + "'");
                        alConditions.Add("COMCD='" + COMCD + "'");
                        alConditions.Add("WERKS='" + strWerks + "'");
                        alConditions.Add("LGORT='" + strLgort + "'");
                        alConditions.Add("LOCAT='" + arlLocat[i].ToString() + "'");

                        strSQL = objWhhed.EntityGetUpdateSql(alConditions);
                        arlSql.Add(strSQL);
                    }

                    bool bolReturn = false;
                    try
                    {
                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- ModifyLocation()";
                    }

                    return bolReturn;
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
            # endregion

            # region 查询是否是自动跑储
            /// <summary>
            /// 查询是否是自动跑储
            /// </summary>
            /// <example>
            /// <code>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  Your Code Here......
            /// </code>
            /// </example>
            /// <returns></returns>
            public DataTable IsAutoLocation(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "IsAutoLocation";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dt = new DataTable();

                    sbSql.Append("SELECT * FROM WHHED WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "'");

                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(sbSql.ToString());
                    sqlAccess.CloseConnection();

                    return dt;

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
            # endregion

            # region 更新储位資訊by倉別

            public bool UpdateLocationByStorage(string strWerks, string strLgort, string Floor, string Area, string Type)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyLocationByStorage";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + Floor + "','" + Area + "','" + Type + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    ArrayList arlSql = new ArrayList();

                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    string strSQL = "";

                    objWhhed.ResetField();
                    objWhhed.Monam = CRNAM;
                    objWhhed.Modat = "getdate()";
                    objWhhed.FLOOR = Floor;
                    objWhhed.AREA = Area;
                    objWhhed.TYPE = Type;

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + strWerks + "'");
                    alConditions.Add("LGORT='" + strLgort + "'");

                    strSQL = objWhhed.EntityGetUpdateSql(alConditions);
                    arlSql.Add(strSQL);


                    bool bolReturn = false;
                    try
                    {
                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- ModifyLocationByStorage()";
                    }

                    return bolReturn;

                    # endregion
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
            # endregion

            # region 获得用户名对应的权限
            /// <summary>
            /// 获得用户名对应的权限
            /// </summary> 
            /// <param name="strUsrnm">用户名</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  DataTable dtData = objAdmin.QueryUserData(strUsrnm);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            public DataTable QueryUserData(string strUsrnm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryUserData";
                this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhusr objWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("USRNM,PASWD,WKAUT,MAAUT,INAUT,OTAUT,CGAUT,IVAUT,MGAUT,REPLN");
                    alConditions.Add("MANDT= '" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("USRNM='" + strUsrnm + "'");
                    string strSQL = objWhusr.EntityGetQuerySql(alColumns, alConditions, true, true);
                    //string strSQL = "Select * from WHUSR where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and USRNM='" + strUsrnm + "'";

                    DataTable dtData = new DataTable();
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSQL);
                    sqlAccess.CloseConnection();

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
            # endregion

            # region 获得用户名对应的厂区仓别权限
            /// <summary>
            /// 获得用户名对应的厂区仓别权限
            /// </summary> 
            /// <param name="strUsrnm">用户名</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  DataTable dtData = objAdmin.QueryUserPlantStorageData(strUsrnm);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryUserPlantStorageData(string strUsrnm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryUserPlantStorageData";
                this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataWhaut objWhaut = new DataWhaut(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("WERKS,LGORT");
                    alConditions.Add("MANDT= '" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("USRNM='" + strUsrnm + "'");
                    string strSQL = objWhaut.EntityGetQuerySql(alColumns, alConditions, true, true);

                    //string strSQL = "Select * from WHAUT where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and USRNM='" + strUsrnm + "'";

                    DataTable dtData = new DataTable();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    //ControlHandleDB();
                    dtData = sqlAccess.GetDataTable(strSQL);
                    sqlAccess.CloseConnection();

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
            # endregion

            # region 组合MAAUT/INAUT/OTAUT/MGAUT/IVAUT/REPLN/TRANS权限
            /// <summary>
            /// 组合MAAUT/INAUT/OTAUT/MGAUT/IVAUT/REPLN权限
            /// </summary>
            /// <param name="arr"></param>
            /// <returns></returns>
            private string CombineAuthorityArray(ArrayList arr)
            {
                string strReturn = "";
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i == arr.Count - 1)
                    {
                        strReturn += arr[i].ToString();
                    }
                    else
                    {
                        strReturn += arr[i].ToString() + ";";
                    }
                }

                return strReturn;
            }
            # endregion

            # region 组合Plant权限
            /// <summary>
            /// 组合Plant权限
            /// </summary>
            /// <param name="arr"></param>
            /// <returns></returns>
            private string CombinePlantAuthorityArray(ArrayList arr)
            {
                string strReturn = "";
                for (int i = 0; i < arr.Count; i++)
                {
                    if (strReturn.IndexOf(arr[i].ToString().Substring(0, 4)) < 0)
                    {
                        if (i == arr.Count - 1)
                        {
                            strReturn += arr[i].ToString().Substring(0, 4);
                        }
                        else
                        {
                            strReturn += arr[i].ToString().Substring(0, 4) + ";";
                        }
                    }
                }

                return strReturn;
            }
            # endregion

            # region 修改用户权限
            /// <summary>
            /// 修改用户权限
            /// </summary> 
            /// <param name="strUsrnm">用户名</param>
            /// <param name="strPaswd">密码</param>
            /// <param name="aryWkaut">Plant权限</param>
            /// <param name="aryMaaut">系统资料维护权限</param>
            /// <param name="aryInaut">入库作业权限</param>
            /// <param name="aryOtaut">出库作业权限</param>
            /// <param name="aryCgaut">库存管理作业权限</param>
            /// <param name="aryIvaut">盘点作业权限</param>
            /// <param name="aryMgaut">补货作业权限</param>
            /// <param name="strIsadm">是否管理者</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.ModifyUserData(string strUsrnm, string strPaswd, ArrayList aryWkaut, ArrayList aryMaaut, ArrayList aryInaut, ArrayList aryOtaut, ArrayList aryCgaut, ArrayList aryIvaut,ArrayList aryMgaut, string strIsadm);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool ModifyUserData(string strUsrnm, string strPaswd, ArrayList aryWkaut, ArrayList aryMaaut, ArrayList aryInaut, ArrayList aryOtaut, ArrayList aryCgaut, ArrayList aryIvaut, ArrayList aryMgaut, ArrayList aryRepln, ArrayList aryTrans, ArrayList aryAgaut, string strIsadm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyUserData";
                this.ControlMethodParm = "('" + strUsrnm + "','" + strPaswd + "','" + aryWkaut + "','" + aryMaaut + "','" + aryInaut + "','" + aryOtaut + "','" + aryCgaut + "','" + aryIvaut + "','" + aryMgaut + "','" + aryRepln + "','" + aryTrans + "','" + strIsadm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strWkaut = CombinePlantAuthorityArray(aryWkaut);
                    string strMaaut = CombineAuthorityArray(aryMaaut);
                    string strInaut = CombineAuthorityArray(aryInaut);
                    string strOtaut = CombineAuthorityArray(aryOtaut);
                    string strCgaut = CombineAuthorityArray(aryCgaut);
                    string strIvaut = CombineAuthorityArray(aryIvaut);
                    string strMgaut = CombineAuthorityArray(aryMgaut);
                    string strRepln = CombineAuthorityArray(aryRepln);
                    string strTrans = CombineAuthorityArray(aryTrans);
                    string strAgaut = CombineAuthorityArray(aryAgaut);
                    string[] aryWerksLgort;
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhusr objDataWhusr = new DataWhusr(UserData);
                    DataWhaut objDataWhaut = new DataWhaut(UserData);
                    string strSQL = "";

                    # region old function
                    //if (strPaswd == "")
                    //{                       
                    //    strSQL = "Update WHUSR set WKAUT = '" + strWkaut + "', MAAUT= '" + strMaaut + "', INAUT= '" + strInaut + "', OTAUT= '" + strOtaut
                    //        + "', CGAUT= '" + strCgaut + "', IVAUT= '" + strIvaut + "', MGAUT='" + strMgaut + "', REPLN='" + strRepln
                    //        + "', ISADM= '" + strIsadm + "', MONAM= '" + CRNAM + "', MODAT=getdate() where MANDT= '"
                    //        + MANDT + "' AND COMCD='" + COMCD + "' and USRNM= '" + strUsrnm + "'";
                    //    arySQL.Add(strSQL);

                    //}
                    //else
                    //{         
                    //    strSQL = "Update WHUSR set PASWD='" + strPaswd + "', WKAUT = '" + strWkaut + "', MAAUT= '" + strMaaut + "', INAUT= '" + strInaut
                    //        + "', OTAUT= '" + strOtaut + "', CGAUT= '" + strCgaut + "', IVAUT= '" + strIvaut + "', MGAUT='" + strMgaut + "', REPLN='" + strRepln
                    //        + "', ISADM= '" + strIsadm + "', MONAM= '" + CRNAM + "', MODAT=getdate() where MANDT= '"
                    //        + MANDT + "' AND COMCD='" + COMCD + "' and USRNM= '" + strUsrnm + "'";
                    //    arySQL.Add(strSQL);

                    //} 
                    # endregion

                    objDataWhusr.ResetField();
                    if (strPaswd != "")
                    {
                        objDataWhusr.Paswd = strPaswd;
                    }
                    objDataWhusr.Wkaut = strWkaut;
                    objDataWhusr.Maaut = strMaaut;
                    objDataWhusr.Inaut = strInaut;
                    objDataWhusr.Otaut = strOtaut;
                    objDataWhusr.Cgaut = strCgaut;
                    objDataWhusr.Ivaut = strIvaut;
                    objDataWhusr.Mgaut = strMgaut;
                    objDataWhusr.Repln = strRepln + ";" + strTrans + ";" + strAgaut;
                    //objDataWhusr.Isadm = strIsadm;
                    objDataWhusr.Monam = CRNAM;
                    objDataWhusr.Modat = "getdate()";

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("USRNM='" + strUsrnm + "'");
                    arySQL.Add(objDataWhusr.EntityGetUpdateSql(alConditions));


                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("USRNM='" + strUsrnm + "'");
                    arySQL.Add(objDataWhaut.EntityGetDeleteSql(alConditions));

                    //strSQL = "Delete from WHAUT where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and USRNM= '" + strUsrnm + "'";
                    //arySQL.Add(strSQL);

                    for (int i = 0; i < aryWkaut.Count; i++)
                    {
                        aryWerksLgort = aryWkaut[i].ToString().Split(new char[] { '-' });
                        objDataWhaut.ResetField();
                        objDataWhaut.Mandt = MANDT;
                        objDataWhaut.Comcd = COMCD;
                        objDataWhaut.Usrnm = strUsrnm.ToUpper();
                        objDataWhaut.Werks = aryWerksLgort[0].ToString();
                        objDataWhaut.Lgort = aryWerksLgort[1].ToString();
                        objDataWhaut.Crnam = CRNAM;
                        objDataWhaut.Crdat = "getdate()";
                        arySQL.Add(objDataWhaut.EntityGetInsertSql());

                        # region old function
                        //strSQL = "Insert into WHAUT(MANDT, USRNM, WERKS, LGORT, CRNAM, CRDAT,COMCD) values('" 
                        //    + MANDT + "', '" + strUsrnm.ToUpper() + "', '" + aryWerksLgort[0].ToString() + "', '" + aryWerksLgort[1].ToString() + "', '" 
                        //    + CRNAM + "', getdate(),'" + COMCD + "')";
                        //arySQL.Add(strSQL);
                        # endregion
                    }

                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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
            # endregion

            # region 新增用户资料及权限
            /// <summary>
            /// 新增用户资料及权限
            /// </summary> 
            /// <param name="strArrayUsrnm">用户名(传入多个用户名时以逗号分隔)</param>
            /// <param name="strPaswd">密码(传入多个用户名时，密码默认一致)</param>
            /// <param name="aryWkaut">Plant权限</param>
            /// <param name="aryMaaut">系统资料维护权限</param>
            /// <param name="aryInaut">入库作业权限</param>
            /// <param name="aryOtaut">出库作业权限</param>
            /// <param name="aryCgaut">库存管理作业权限</param>
            /// <param name="aryIvaut">盘点作业权限</param>
            /// <param name="aryMgaut">补货作业权限</param>
            /// <param name="strIsadm">是否管理者</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.AddUserData(string[] strArrayUsrnm, string strPaswd, ArrayList aryWkaut, ArrayList aryMaaut, ArrayList aryInaut, ArrayList aryOtaut, ArrayList aryCgaut, ArrayList aryIvaut,ArrayList aryMgaut, string strIsadm);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool AddUserData(string[] strArrayUsrnm, string strPaswd, ArrayList aryWkaut, ArrayList aryMaaut, ArrayList aryInaut, ArrayList aryOtaut, ArrayList aryCgaut, ArrayList aryIvaut, ArrayList aryMgaut, ArrayList aryRepln, string strIsadm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddUserData";
                this.ControlMethodParm = "('" + strArrayUsrnm + "','" + strPaswd + "','" + aryWkaut + "','" + aryMaaut + "','" + aryInaut + "','" + aryOtaut + "','" + aryCgaut + "','" + aryIvaut + "','" + aryMgaut + "','" + aryRepln + "','" + strIsadm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strWkaut = CombinePlantAuthorityArray(aryWkaut);
                    string strMaaut = CombineAuthorityArray(aryMaaut);
                    string strInaut = CombineAuthorityArray(aryInaut);
                    string strOtaut = CombineAuthorityArray(aryOtaut);
                    string strCgaut = CombineAuthorityArray(aryCgaut);
                    string strIvaut = CombineAuthorityArray(aryIvaut);
                    string strMgaut = CombineAuthorityArray(aryMgaut);
                    string strRepln = CombineAuthorityArray(aryRepln);
                    string[] aryWerksLgort;
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhusr objDataWhusr = new DataWhusr(UserData);
                    DataWhaut objDataWhaut = new DataWhaut(UserData);
                    string strSQL = "";

                    for (int j = 0; j < strArrayUsrnm.Length; j++)
                    {
                        if (strArrayUsrnm[j].ToString().Trim() == "")
                        {
                            continue;
                        }

                        objDataWhusr.ResetField();
                        objDataWhusr.Mandt = MANDT;
                        objDataWhusr.Usrnm = strArrayUsrnm[j].ToString().ToUpper();
                        objDataWhusr.Paswd = strPaswd;
                        objDataWhusr.Wkaut = strWkaut;
                        objDataWhusr.Maaut = strMaaut;
                        objDataWhusr.Inaut = strInaut;
                        objDataWhusr.Otaut = strOtaut;
                        objDataWhusr.Cgaut = strCgaut;
                        objDataWhusr.Ivaut = strIvaut;
                        objDataWhusr.Mgaut = strMgaut;
                        objDataWhusr.Repln = strRepln;
                        objDataWhusr.Isadm = strIsadm;
                        objDataWhusr.Crnam = CRNAM;
                        objDataWhusr.Crdat = "getdate()";
                        objDataWhusr.Monam = CRNAM;
                        objDataWhusr.Modat = "getdate()";
                        objDataWhusr.Comcd = COMCD;
                        arySQL.Add(objDataWhusr.EntityGetInsertSql());

                        # region old function
                        //strSQL = "Insert into WHUSR(MANDT, USRNM, PASWD, WKAUT, MAAUT, INAUT, OTAUT, CGAUT, IVAUT, MGAUT, REPLN, ISADM, CRNAM, CRDAT, MONAM, MODAT,COMCD) values" +
                        //                "('" + MANDT + "','" + strArrayUsrnm[j].ToString().ToUpper() + "','" + strPaswd + "','" + strWkaut + "','" + strMaaut + "','"
                        //                + strInaut + "','" + strOtaut + "','" + strCgaut + "','" + strIvaut + "','" + strMgaut + "','" + strRepln + "','" + strIsadm 
                        //                + "','" + CRNAM + "', getdate(),'" + CRNAM + "', getdate(),'" + COMCD + "')";
                        //arySQL.Add(strSQL);
                        # endregion

                        for (int i = 0; i < aryWkaut.Count; i++)
                        {
                            aryWerksLgort = aryWkaut[i].ToString().Split(new char[] { '-' });

                            objDataWhaut.ResetField();
                            objDataWhaut.Mandt = MANDT;
                            objDataWhaut.Usrnm = strArrayUsrnm[j].ToString().ToUpper();
                            objDataWhaut.Werks = aryWerksLgort[0].ToString();
                            objDataWhaut.Lgort = aryWerksLgort[1].ToString();
                            objDataWhaut.Crnam = CRNAM;
                            objDataWhaut.Crdat = "getdate()";
                            objDataWhaut.Comcd = COMCD;
                            arySQL.Add(objDataWhaut.EntityGetInsertSql());

                            # region old function
                            //strSQL = "Insert into WHAUT(MANDT, USRNM, WERKS, LGORT, CRNAM, CRDAT,COMCD) values('" 
                            //+ MANDT + "', '" + strArrayUsrnm[j].ToString().ToUpper() + "', '" + aryWerksLgort[0].ToString() + "', '" + aryWerksLgort[1].ToString() 
                            //+ "', '" + CRNAM + "', getdate(),'" + COMCD + "')";
                            //arySQL.Add(strSQL);
                            # endregion
                        }
                    }

                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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
            # endregion

            # region 删除用户
            /// <summary>
            /// 删除用户资料及权限
            /// </summary> 
            /// <param name="strUsrnm">用户名(传入多个时以逗号分隔)</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteUserData(strUsrnm);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool DeleteUserData(string[] strUsrnm, UserInfo UserData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteUserData";
                this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhusr objDataWhusr = new DataWhusr(UserData);
                    DataWhaut objDataWhaut = new DataWhaut(UserData);
                    string strSQL;


                    for (int i = 0; i < strUsrnm.Length; i++)
                    {
                        if (strUsrnm[i].ToString().Trim() == "")
                        {
                            continue;
                        }
                        strSQL = "";                        //select * from WHLOG where RMAK1='Delete Usrnm'    被删除账号在MATNR栏位
                        strSQL = "  insert into WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,TRNTP,MENGE,CRNAM,CRDAT,RMAK1,RMAK2,COMCD)  VALUES('" + UserData.Client + "','','','A0','','" + strUsrnm[i].ToString() + "','',0,'" + UserData.UserId + "',GETDATE(),'Delete Usrnm','" + UserData.ClientIP + "','" + UserData.CompanyCode + "')   ";
                        arySQL.Add(strSQL);


                        alConditions.Clear();
                        alConditions.Add("MANDT='" + MANDT + "'");
                        alConditions.Add("COMCD='" + COMCD + "'");
                        alConditions.Add("USRNM='" + strUsrnm[i].ToString().ToUpper() + "'");
                        arySQL.Add(objDataWhusr.EntityGetDeleteSql(alConditions));

                        alConditions.Clear();
                        alConditions.Add("MANDT='" + MANDT + "'");
                        alConditions.Add("COMCD='" + COMCD + "'");
                        alConditions.Add("USRNM='" + strUsrnm[i].ToString().ToUpper() + "'");
                        arySQL.Add(objDataWhaut.EntityGetDeleteSql(alConditions));

                        # region old function
                        //strSQL = "Delete from WHUSR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and USRNM = '" + strUsrnm[i].ToString().ToUpper() + "'";
                        //arySQL.Add(strSQL);

                        //strSQL = "Delete from WHAUT where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and USRNM = '" + strUsrnm[i].ToString().ToUpper() + "'";
                        //arySQL.Add(strSQL);
                        # endregion
                    }

                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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
            # endregion

            # region 删除连板料号资料
            /// <summary>
            /// 删除连板料号资料
            /// </summary> 
            /// <param name="strMrgid">连板流水号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteMixedMaterial(strMrgid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool DeleteMixedMaterial(string strMrgid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteMixedMaterial";
                this.ControlMethodParm = "('" + strMrgid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList alConditions = new ArrayList();
                    DataWhmtr objDataWhmtr = new DataWhmtr(UserData);

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" MRGID='" + strMrgid + "'");
                    bool bolReturn = false;
                    bolReturn = objDataWhmtr.EntityDelete(alConditions);

                    # region old function
                    //string strSQL = "Delete from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and MRGID= '" + strMrgid + "'";

                    //bool bolReturn = false;
                    //ControlHandleDB();
                    //bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    //ControlSqlAccess.CloseConnection();
                    # endregion
                    return bolReturn;
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
            # endregion

            # region 获得最大连板流水号
            /// <summary>
            /// 获得最大连板流水号
            /// </summary> 
            /// <returns>
            /// int
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Select max(MRGID)+1 from WHMTR
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public string GetMaxMrgid()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMaxMrgid";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "select isnull(right('00000' + Convert(varchar, (Convert(int, Max(MRGID)) + 1)), 5), '00001') as MRGID from WHMTR where MANDT = '" + MANDT + "' AND COMCD='" + COMCD + "'";
                    DataTable dtData = new DataTable();

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                    if (dtData.Rows[0]["MRGID"].ToString() == "")
                    {
                        return "1";
                    }
                    else
                    {
                        return dtData.Rows[0]["MRGID"].ToString();
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
            # endregion

            # region 新增新增连板料号资料
            /// <summary>
            /// 新增连板料号资料
            /// </summary> 
            /// <param name="strWerks">Plant</param>
            /// <param name="aryMatnr">料号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.AddMixedMaterial(strWerks, aryMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool AddMixedMaterial(string strWerks, ArrayList aryMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddMixedMaterial";
                this.ControlMethodParm = "('" + strWerks + "','" + aryMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhmtr objWhmtr = new DataWhmtr(UserData);

                    string strMrgid = GetMaxMrgid();
                    ArrayList arlSql = new ArrayList();

                    for (int i = 0; i < aryMatnr.Count; i++)
                    {
                        //string strSQL = "Insert into WHMTR(MANDT, WERKS, MRGID, MATNR, CRNAM, CRDAT, MONAM, MODAT,COMCD) values('" + MANDT + "','" + strWerks + "','" + strMrgid + "','" + aryMatnr[i].ToString().ToUpper() + "','" + CRNAM + "', getdate() ,'" + CRNAM + "', getdate(),'" + COMCD + "' )";

                        objWhmtr.ResetField();
                        objWhmtr.Mandt = MANDT;
                        objWhmtr.Werks = strWerks;
                        objWhmtr.Mrgid = strMrgid;
                        objWhmtr.Matnr = aryMatnr[i].ToString().ToUpper();
                        objWhmtr.Crnam = CRNAM;
                        objWhmtr.Crdat = "getdate()";
                        objWhmtr.Monam = CRNAM;
                        objWhmtr.Modat = "getdate()";
                        objWhmtr.Comcd = COMCD;

                        string strSQL = objWhmtr.EntityGetInsertSql();
                        arlSql.Add(strSQL);
                    }

                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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
            # endregion

            #region 删除仓别
            /// <summary>
            /// 删除仓别
            /// </summary>
            /// <param name="strWerks">厂别</param>
            /// <param name="strLgort">仓别</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteStorage(strWerks, strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool DeleteStorage(string strWerks, string strLgort, string Progid, UserInfo UserData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteStorage";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region old funcion
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHCTRL where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='LGORT' and CTRLNM= '" + strWerks + "' and CTRLC1= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHHED  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHITM  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHMAP  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHSTG  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHCST  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHCYC  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHAUT  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");
                    #endregion

                    ArrayList arySQL = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhmap objWhmap = new DataWhmap(UserData);
                    DataWhstg objWhstg = new DataWhstg(UserData);
                    DataWhcst objWhcst = new DataWhcst(UserData);
                    DataWhcyc objWhcyc = new DataWhcyc(UserData);
                    DataWhaut objWhaut = new DataWhaut(UserData);
                    bool bolReturn = false;
                    //增加LOG
                    string strSQL;
                    strSQL = "";                        //select * from WHLOG where RMAK1='Delete Storage'    
                    strSQL = "  insert into WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,TRNTP,MENGE,CRNAM,CRDAT,RMAK1,RMAK2,COMCD)  VALUES('" + UserData.Client + "','" + strWerks + "','" + strLgort + "','" + Progid + "','','','',0,'" + UserData.UserId + "',GETDATE(),'Delete Storage','" + UserData.ClientIP + "','" + UserData.CompanyCode + "')   ";
                    arySQL.Add(strSQL);



                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='LGORT' ");
                    alConditions.Add("CTRLNM= '" + strWerks + "'");
                    alConditions.Add("CTRLC1= '" + strLgort + "'");
                    arySQL.Add(objWhctrl.EntityGetDeleteSql(alConditions));

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS= '" + strWerks + "'");
                    alConditions.Add("LGORT= '" + strLgort + "'");
                    arySQL.Add(objWhhed.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhmap.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhstg.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhcst.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhcyc.EntityGetDeleteSql(alConditions));
                    arySQL.Add(objWhaut.EntityGetDeleteSql(alConditions));
                    //删除已维护的盘点周期
                    arySQL.Add(" DELETE FROM WHCTRL 	WHERE CTRLID='INVTYP'  AND MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND CTRLNM='" + strWerks + "' AND CTRLC1 IN ('" + strLgort + "')  ");
                    //删除datecode管控仓别
                    arySQL.Add(" DELETE FROM WHCTRL 	WHERE CTRLID='StorageIn_Type' AND  REMAK='Diff DACOD Diff Locat'  AND MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND CTRLNM='" + strWerks + "' AND CTRLC1 IN ('" + strLgort + "')  ");

                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 新增仓别
            /// <summary>
            ///  新增仓别
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strSttyp"></param>
            /// <param name="strLotyp"></param>
            /// <param name="dtData"></param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.AddStorage(strWerks, strLgort, aryInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            public bool AddStorage(string strWerks, string strLgort, string strSttyp, string strLotyp, DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddStorage";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strSttyp + "','" + strLotyp + "','" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    bool bolReturn = false;
                    string strTempLocat = "";
                    DataWhaut objWhaut = new DataWhaut(UserData);
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhstg objWhstg = new DataWhstg(UserData);

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS= '" + strWerks + "'");
                    alConditions.Add("LGORT= '" + strLgort + "'");
                    arySQL.Add(objWhaut.EntityGetDeleteSql(alConditions));

                    //ControlSqlAccess.SqlArray.Clear();
                    //this.ControlSqlAccess.SqlArray.Add("Delete from WHAUT  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT= '" + strLgort + "'");


                    objWhctrl.ResetField();
                    objWhctrl.Mandt = MANDT;
                    objWhctrl.Comcd = COMCD;
                    objWhctrl.Soldto = "QWMS";
                    objWhctrl.Ctrlid = "LGORT";
                    objWhctrl.Ctrlnm = strWerks;
                    objWhctrl.Ctrlc1 = strLgort.ToUpper();
                    objWhctrl.Ctrlc2 = strLgort.ToUpper();
                    objWhctrl.Ctrlc4 = strSttyp;
                    objWhctrl.Ctrlc5 = strLotyp;
                    arySQL.Add(objWhctrl.EntityGetInsertSql());

                    //this.ControlSqlAccess.SqlArray.Add("Insert into WHCTRL(MANDT,COMCD, SOLDTO, CTRLID, CTRLNM, CTRLC1, CTRLC2, CTRLC4, CTRLC5) values('" + MANDT + "','" + COMCD + "', 'QWMS', 'LGORT','" + strWerks + "','" + strLgort.ToUpper() + "','" + strLgort.ToUpper() + "','" + strSttyp + "','" + strLotyp + "')");
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        objWhstg.ResetField();
                        objWhstg.Mandt = MANDT;
                        objWhstg.Comcd = COMCD;
                        objWhstg.Werks = strWerks;
                        objWhstg.Lgort = strLgort.ToUpper();
                        objWhstg.Stset = dtData.Rows[i]["STSET"].ToString();
                        objWhstg.Sthgh = dtData.Rows[i]["STHGH"].ToString();
                        objWhstg.Stlen = dtData.Rows[i]["STLEN"].ToString();
                        objWhstg.Stwid = dtData.Rows[i]["STWID"].ToString();
                        objWhstg.Crnam = CRNAM;
                        objWhstg.Crdat = "getdate()";
                        arySQL.Add(objWhstg.EntityGetInsertSql());

                        // this.ControlSqlAccess.SqlArray.Add("Insert into WHSTG(MANDT,COMCD, WERKS, LGORT, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT) values('" + MANDT + "','" + COMCD + "', '" + strWerks + "', '" + strLgort.ToUpper() + "','" + dtData.Rows[i]["STSET"].ToString() + "','" + dtData.Rows[i]["STHGH"].ToString() + "','" + dtData.Rows[i]["STLEN"].ToString() + "','" + dtData.Rows[i]["STWID"].ToString() + "', '" + CRNAM + "', getdate())");
                        if (strSttyp.ToUpper() == "CARROUSEL")
                        {
                            for (int j = 1; j <= Convert.ToInt32(dtData.Rows[i]["STHGH"].ToString()); j++)
                            {
                                for (int k = 1; k <= Convert.ToInt32(dtData.Rows[i]["STLEN"].ToString()); k++)
                                {
                                    for (int l = 1; l <= Convert.ToInt32(dtData.Rows[i]["STWID"].ToString()); l++)
                                    {
                                        strTempLocat = Convert.ToInt32(dtData.Rows[i]["STSET"].ToString()).ToString("00") + j.ToString("00") + k.ToString("00") + l.ToString();

                                        objWhhed.ResetField();
                                        objWhhed.Mandt = MANDT;
                                        objWhhed.Comcd = COMCD;
                                        objWhhed.Werks = strWerks;
                                        objWhhed.Lgort = strLgort;
                                        objWhhed.Locat = strTempLocat.ToUpper();
                                        objWhhed.Losts = "0";
                                        objWhhed.Ismrg = "N";
                                        objWhhed.Stset = dtData.Rows[i]["STSET"].ToString();
                                        objWhhed.Sthgh = j.ToString();
                                        objWhhed.Stlen = k.ToString();
                                        objWhhed.Stwid = l.ToString();
                                        objWhhed.Crnam = CRNAM;
                                        objWhhed.Crdat = "getdate()";
                                        objWhhed.Monam = CRNAM;
                                        objWhhed.Modat = "getdate()";
                                        arySQL.Add(objWhhed.EntityGetInsertSql());
                                        //this.ControlSqlAccess.SqlArray.Add("Insert into WHHED(MANDT,COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT, MONAM, MODAT) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strTempLocat.ToUpper() + "', '0','N','" + dtData.Rows[i]["STSET"].ToString() + "', '" + j.ToString() + "', '" + k.ToString() + "', '" + l.ToString() + "', '" + CRNAM + "', getdate(),'" + CRNAM + "', getdate() )");
                                    }
                                }
                            }
                        }
                    }
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            public bool AddStorage(string flag, string strMark, string strWerks, string strLgort, string strSttyp, string strLotyp, DataTable dtData, bool isSap)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddStorage";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strSttyp + "','" + strLotyp + "','" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region 变量
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    bool bolReturn = false;
                    string strTempLocat = "";
                    DataWhaut objWhaut = new DataWhaut(UserData);
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhstg objWhstg = new DataWhstg(UserData);
                    #endregion

                    #region 删除WHAUT
                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS= '" + strWerks + "'");
                    alConditions.Add("LGORT= '" + strLgort + "'");
                    arySQL.Add(objWhaut.EntityGetDeleteSql(alConditions));

                    #endregion

                    #region 新增WHCTRL

                    objWhctrl.ResetField();
                    objWhctrl.Mandt = MANDT;
                    objWhctrl.Comcd = COMCD;
                    objWhctrl.Soldto = "QWMS";
                    objWhctrl.Ctrlid = "LGORT";
                    objWhctrl.Ctrlnm = strWerks;
                    objWhctrl.Ctrlc1 = strLgort.ToUpper();
                    objWhctrl.Ctrlc2 = strLgort.ToUpper();
                    objWhctrl.Ctrlc3 = strMark;
                    objWhctrl.Ctrlc4 = strSttyp;
                    objWhctrl.Ctrlc5 = strLotyp;

                    if (flag == "XL")
                    {
                        objWhctrl.Ctrln3 = "1";
                    }

                    if (isSap)
                    {
                        objWhctrl.Ctrln2 = "1";
                    }
                    else
                    {
                        objWhctrl.Ctrln2 = "0";
                    }
                    arySQL.Add(objWhctrl.EntityGetInsertSql());

                    #endregion

                    if (flag == "XL" || flag == "DC")
                    {
                        objWhctrl.ResetField();
                        objWhctrl.Mandt = MANDT;
                        objWhctrl.Comcd = COMCD;
                        objWhctrl.Soldto = "QWMS";
                        objWhctrl.Ctrlid = "StorageIn_Type";
                        objWhctrl.Ctrlnm = strWerks;
                        objWhctrl.Ctrlc1 = strLgort.ToUpper();
                        objWhctrl.Remak = "Diff DACOD Diff Locat";
                    
                        arySQL.Add(objWhctrl.EntityGetInsertSql());

                    }
                    #region 新增WHSTG

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        objWhstg.ResetField();
                        objWhstg.Mandt = MANDT;
                        objWhstg.Comcd = COMCD;
                        objWhstg.Werks = strWerks;
                        objWhstg.Lgort = strLgort.ToUpper();
                        objWhstg.Stset = dtData.Rows[i]["STSET"].ToString();
                        objWhstg.Sthgh = dtData.Rows[i]["STHGH"].ToString();
                        objWhstg.Stlen = dtData.Rows[i]["STLEN"].ToString();
                        objWhstg.Stwid = dtData.Rows[i]["STWID"].ToString();
                        objWhstg.Crnam = CRNAM;
                        objWhstg.Crdat = "getdate()";
                        arySQL.Add(objWhstg.EntityGetInsertSql());

                        #region CARROUSEL仓别
                        if (strSttyp.ToUpper() == "CARROUSEL")
                        {
                            for (int j = 1; j <= Convert.ToInt32(dtData.Rows[i]["STHGH"].ToString()); j++)
                            {
                                for (int k = 1; k <= Convert.ToInt32(dtData.Rows[i]["STLEN"].ToString()); k++)
                                {
                                    for (int l = 1; l <= Convert.ToInt32(dtData.Rows[i]["STWID"].ToString()); l++)
                                    {
                                        strTempLocat = Convert.ToInt32(dtData.Rows[i]["STSET"].ToString()).ToString("00") + j.ToString("00") + k.ToString("00") + l.ToString();

                                        objWhhed.ResetField();
                                        objWhhed.Mandt = MANDT;
                                        objWhhed.Comcd = COMCD;
                                        objWhhed.Werks = strWerks;
                                        objWhhed.Lgort = strLgort;
                                        objWhhed.Locat = strTempLocat.ToUpper();
                                        objWhhed.Losts = "0";
                                        objWhhed.Ismrg = "N";
                                        objWhhed.Stset = dtData.Rows[i]["STSET"].ToString();
                                        objWhhed.Sthgh = j.ToString();
                                        objWhhed.Stlen = k.ToString();
                                        objWhhed.Stwid = l.ToString();
                                        objWhhed.Crnam = CRNAM;
                                        objWhhed.Crdat = "getdate()";
                                        objWhhed.Monam = CRNAM;
                                        objWhhed.Modat = "getdate()";
                                        arySQL.Add(objWhhed.EntityGetInsertSql());
                                        //this.ControlSqlAccess.SqlArray.Add("Insert into WHHED(MANDT,COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT, MONAM, MODAT) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strTempLocat.ToUpper() + "', '0','N','" + dtData.Rows[i]["STSET"].ToString() + "', '" + j.ToString() + "', '" + k.ToString() + "', '" + l.ToString() + "', '" + CRNAM + "', getdate(),'" + CRNAM + "', getdate() )");
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 新增仓别(NB5 JIT)
            /// <summary>
            ///  新增仓别(NB5 JIT)
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strSttyp"></param>
            /// <param name="strLotyp"></param>
            /// <param name="dtData"></param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.AddStorage(strWerks, strLgort, aryInsmk);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            public bool AddStorage_JIT(string strWerks, string strLgort, string strSttyp, string strLotyp, DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddStorage_JIT";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strSttyp + "','" + strLotyp + "','" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    bool bolReturn = false;
                    string strTempLocat = "";
                    DataWhaut objWhaut = new DataWhaut(UserData);
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhstg objWhstg = new DataWhstg(UserData);

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS= '" + strWerks + "'");
                    alConditions.Add("LGORT= '" + strLgort + "'");
                    arySQL.Add(objWhaut.EntityGetDeleteSql(alConditions));


                    objWhctrl.ResetField();
                    objWhctrl.Mandt = MANDT;
                    objWhctrl.Comcd = COMCD;
                    objWhctrl.Soldto = "QWMS";
                    objWhctrl.Ctrlid = "LGORT";
                    objWhctrl.Ctrlnm = strWerks;
                    objWhctrl.Ctrlc1 = strLgort.ToUpper();
                    objWhctrl.Ctrlc2 = strLgort.ToUpper();
                    objWhctrl.Ctrlc4 = strSttyp;
                    objWhctrl.Ctrlc5 = strLotyp;
                    objWhctrl.Remak = "JIT";
                    arySQL.Add(objWhctrl.EntityGetInsertSql());

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        objWhstg.ResetField();
                        objWhstg.Mandt = MANDT;
                        objWhstg.Comcd = COMCD;
                        objWhstg.Werks = strWerks;
                        objWhstg.Lgort = strLgort.ToUpper();
                        objWhstg.Stset = dtData.Rows[i]["STSET"].ToString();
                        objWhstg.Sthgh = dtData.Rows[i]["STHGH"].ToString();
                        objWhstg.Stlen = dtData.Rows[i]["STLEN"].ToString();
                        objWhstg.Stwid = dtData.Rows[i]["STWID"].ToString();
                        objWhstg.Crnam = CRNAM;
                        objWhstg.Crdat = "getdate()";
                        arySQL.Add(objWhstg.EntityGetInsertSql());

                        if (strSttyp.ToUpper() == "CARROUSEL")
                        {
                            for (int j = 1; j <= Convert.ToInt32(dtData.Rows[i]["STHGH"].ToString()); j++)
                            {
                                for (int k = 1; k <= Convert.ToInt32(dtData.Rows[i]["STLEN"].ToString()); k++)
                                {
                                    for (int l = 1; l <= Convert.ToInt32(dtData.Rows[i]["STWID"].ToString()); l++)
                                    {
                                        strTempLocat = Convert.ToInt32(dtData.Rows[i]["STSET"].ToString()).ToString("00") + j.ToString("00") + k.ToString("00") + l.ToString();

                                        objWhhed.ResetField();
                                        objWhhed.Mandt = MANDT;
                                        objWhhed.Comcd = COMCD;
                                        objWhhed.Werks = strWerks;
                                        objWhhed.Lgort = strLgort;
                                        objWhhed.Locat = strTempLocat.ToUpper();
                                        objWhhed.Losts = "0";
                                        objWhhed.Ismrg = "N";
                                        objWhhed.Stset = dtData.Rows[i]["STSET"].ToString();
                                        objWhhed.Sthgh = j.ToString();
                                        objWhhed.Stlen = k.ToString();
                                        objWhhed.Stwid = l.ToString();
                                        objWhhed.Crnam = CRNAM;
                                        objWhhed.Crdat = "getdate()";
                                        objWhhed.Monam = CRNAM;
                                        objWhhed.Modat = "getdate()";
                                        arySQL.Add(objWhhed.EntityGetInsertSql());
                                    }
                                }
                            }
                        }
                    }
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 修改仓别
            /// <summary>
            /// 修改仓别
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strStset"></param>
            /// <param name="dtData"></param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteStorage(strWerks, strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool ModifyStorage(string strWerks, string strLgort, string strStset, DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyStorage";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strStset + "','" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.SqlArray.Clear();
                    string strTempLocat = "";
                    this.ControlSqlAccess.SqlArray.Add("Update WHSTG set STHGH='" + dtData.Rows[0]["STHGH"].ToString() + "', STLEN='" + dtData.Rows[0]["STLEN"].ToString() + "', STWID='" + dtData.Rows[0]["STWID"].ToString() + "' where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");
                    // Carrousel
                    if (dtData.Rows[0]["CTRLC4"].ToString().ToUpper() == "CARROUSEL")
                    {
                        //Fixed Material
                        if (dtData.Rows[0]["CTRLC5"].ToString().ToUpper() == "FIXED LOCATION")
                        {

                            this.ControlSqlAccess.SqlArray.Add("Delete from WHMAP where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT like '" + Convert.ToInt32(strStset).ToString("00") + "'");
                        }

                        this.ControlSqlAccess.SqlArray.Add("Delete from WHHED where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");

                        for (int j = 1; j <= Convert.ToInt32(dtData.Rows[0]["STHGH"].ToString()); j++)
                        {
                            for (int k = 1; k <= Convert.ToInt32(dtData.Rows[0]["STLEN"].ToString()); k++)
                            {
                                for (int l = 1; l <= Convert.ToInt32(dtData.Rows[0]["STWID"].ToString()); l++)
                                {
                                    strTempLocat = Convert.ToInt32(dtData.Rows[0]["STSET"].ToString()).ToString("00") + j.ToString("00") + k.ToString("00") + l.ToString();
                                    this.ControlSqlAccess.SqlArray.Add("Insert into WHHED(MANDT,COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT, MONAM, MODAT) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strTempLocat.ToUpper() + "', '0','N','" + strStset + "', '" + j.ToString() + "', '" + k.ToString() + "', '" + l.ToString() + "', '" + CRNAM + "', getdate(),'" + CRNAM + "', getdate() )");
                                }
                            }
                        }

                    }
                    else
                    {
                        this.ControlSqlAccess.SqlArray.Add("Update WHHED set STHGH=null, STLEN=null, STWID=null where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");
                    }


                    bool bolReturn = false;

                    bolReturn = ControlSqlAccess.ExecSqlArray();
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 修改仓别(NB5 JIT)
            /// <summary>
            /// 修改仓别(NB5 JIT)
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strStset"></param>
            /// <param name="dtData"></param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.ModifyStorage_JIT(strWerks, strLgort, strStset, dtData);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool ModifyStorage_JIT(string strWerks, string strLgort, string strStset, DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyStorage_JIT";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strStset + "','" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ControlHandleDB();
                    ControlSqlAccess.SqlArray.Clear();
                    string strTempLocat = "";
                    this.ControlSqlAccess.SqlArray.Add("Update WHSTG set STHGH='" + dtData.Rows[0]["STHGH"].ToString() + "', STLEN='" + dtData.Rows[0]["STLEN"].ToString() + "', STWID='" + dtData.Rows[0]["STWID"].ToString() + "' where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");
                    // Carrousel
                    if (dtData.Rows[0]["CTRLC4"].ToString().ToUpper() == "CARROUSEL")
                    {
                        //Fixed Material
                        if (dtData.Rows[0]["CTRLC5"].ToString().ToUpper() == "FIXED LOCATION")
                        {

                            this.ControlSqlAccess.SqlArray.Add("Delete from WHMAP where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT like '" + Convert.ToInt32(strStset).ToString("00") + "'");
                        }

                        this.ControlSqlAccess.SqlArray.Add("Delete from WHHED where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");

                        for (int j = 1; j <= Convert.ToInt32(dtData.Rows[0]["STHGH"].ToString()); j++)
                        {
                            for (int k = 1; k <= Convert.ToInt32(dtData.Rows[0]["STLEN"].ToString()); k++)
                            {
                                for (int l = 1; l <= Convert.ToInt32(dtData.Rows[0]["STWID"].ToString()); l++)
                                {
                                    strTempLocat = Convert.ToInt32(dtData.Rows[0]["STSET"].ToString()).ToString("00") + j.ToString("00") + k.ToString("00") + l.ToString();
                                    this.ControlSqlAccess.SqlArray.Add("Insert into WHHED(MANDT,COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT, MONAM, MODAT) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strTempLocat.ToUpper() + "', '0','N','" + strStset + "', '" + j.ToString() + "', '" + k.ToString() + "', '" + l.ToString() + "', '" + CRNAM + "', getdate(),'" + CRNAM + "', getdate() )");
                                }
                            }
                        }

                    }
                    else
                    {
                        this.ControlSqlAccess.SqlArray.Add("Update WHHED set STHGH=null, STLEN=null, STWID=null where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and STSET='" + strStset + "'");
                    }

                    //更新WHCTRL.REMAK的內容
                    this.ControlSqlAccess.SqlArray.Add("Update WHCTRL set REMAK = '" + dtData.Rows[0]["REMAK"].ToString() + "' where SOLDTO='QWMS' and CTRLID='LGORT' and MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and CTRLNM='" + strWerks + "' and CTRLC1='" + strLgort + "'");

                    bool bolReturn = false;

                    bolReturn = ControlSqlAccess.ExecSqlArray();
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 新增储位
            /// <summary>
            /// 新增储位
            /// </summary> 
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strLocat"></param>
            /// <param name="strStset"></param>
            /// <param name="strSthgh"></param>
            /// <param name="strStlen"></param>
            /// <param name="strStwid"></param>
            /// <param name="strCtbto">CTO/BTO</param>
            /// <param name="strRegon">Region</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.AddLocation(strWerks, strLgort, strLocat, strStset, strSthgh, strStlen, strStwid,strCtbto,strRegon);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddLocation(string strWerks, string strLgort, string strLocat, string strStset, string strSthgh, string strStlen, string strStwid, string strCtbto, string strRegon)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strStset + "','" + strSthgh + "','" + strStlen + "','" + strStwid + "','" + strCtbto + "','" + strRegon + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhhed objWhhed = new DataWhhed(UserData);

                    //ControlHandleDB();
                    string strSQL = "";
                    if (strStset != "" && strSthgh != "" && strStlen != "" && strStwid != "")
                    {
                        //strSQL = "Insert into WHHED(MANDT, COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, STSET, STHGH, STLEN, STWID, CRNAM, CRDAT, MONAM, MODAT, CTBTO, REGON) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strLocat.ToUpper() + "', '0','N','" + strStset + "', '" + strSthgh + "', '" + strStlen + "', '" + strStwid + "', '" + CRNAM + "', getdate(),'" + CRNAM + "', getdate(),'" + strCtbto + "','" + strRegon + "' )";

                        objWhhed.Mandt = UserData.Client;
                        objWhhed.Comcd = UserData.CompanyCode;
                        objWhhed.Werks = strWerks;
                        objWhhed.Lgort = strLgort;
                        objWhhed.Locat = strLocat.ToUpper();
                        objWhhed.Losts = "0";
                        objWhhed.Ismrg = "N";
                        objWhhed.Stset = strStset;
                        objWhhed.Sthgh = strSthgh;
                        objWhhed.Stlen = strSthgh;
                        objWhhed.Stwid = strStwid;
                        objWhhed.Crnam = UserData.UserId;
                        objWhhed.Crdat = "getdate()";
                        objWhhed.Monam = UserData.UserId;
                        objWhhed.Modat = "getdate()";
                        objWhhed.Ctbto = strCtbto;
                        objWhhed.Regon = strRegon;
                    }
                    else
                    {
                        objWhhed.Mandt = UserData.Client;
                        objWhhed.Comcd = UserData.CompanyCode;
                        objWhhed.Werks = strWerks;
                        objWhhed.Lgort = strLgort;
                        objWhhed.Locat = strLocat.ToUpper();
                        objWhhed.Losts = "0";
                        objWhhed.Ismrg = "N";
                        objWhhed.Crnam = UserData.UserId;
                        objWhhed.Crdat = "getdate()";
                        objWhhed.Monam = UserData.UserId;
                        objWhhed.Modat = "getdate()";
                        objWhhed.Ctbto = strCtbto;
                        objWhhed.Regon = strRegon;

                        //strSQL = "Insert into WHHED(MANDT, COMCD, WERKS, LGORT, LOCAT, LOSTS, ISMRG, CRNAM, CRDAT, MONAM, MODAT, CTBTO, REGON) values('" + MANDT + "','" + COMCD + "','" + strWerks + "','" + strLgort + "','" + strLocat.ToUpper() + "', '0','N','" + CRNAM + "', getdate(),'" + CRNAM + "', getdate(),'" + strCtbto + "','" + strRegon + "' )";
                    }

                    //objWhhed.EntityInsert();

                    bool bolReturn = false;
                    //bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    //ControlSqlAccess.CloseConnection();
                    bolReturn = objWhhed.EntityInsert();
                    return bolReturn;
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

            #region 修改储位
            /// <summary>
            /// 修改储位
            /// </summary> 
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strLocat"></param>
            /// <param name="strStset"></param>
            /// <param name="strSthgh"></param>
            /// <param name="strStlen"></param>
            /// <param name="strStwid"></param>
            /// <param name="strType"></param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.ModifyLocation(strWerks, strLgort, strLocat, strStset, strSthgh, strStlen, strStwid, strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool ModifyLocation(string strWerks, string strLgort, string strLocat, string strStset, string strSthgh, string strStlen, string strStwid, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strStset + "','" + strSthgh + "','" + strStlen + "','" + strStwid + "','" + strType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhhed objDataWhhed = new DataWhhed(UserData);

                    ArrayList alCondition = new ArrayList();

                    ControlHandleDB();
                    string strSQL = "";
                    if (strType.ToUpper() == "UPDATE")
                    {
                        objDataWhhed.ResetField();
                        objDataWhhed.Stset = strStset;
                        objDataWhhed.Sthgh = strSthgh;
                        objDataWhhed.Stlen = strStlen;
                        objDataWhhed.Stwid = strStwid;
                        alCondition.Clear();
                        alCondition.Add("MANDT='" + MANDT + "'");
                        alCondition.Add("COMCD='" + COMCD + "'");
                        alCondition.Add("WERKS= '" + strWerks + "'");
                        alCondition.Add("LGORT= '" + strLgort + "'");
                        alCondition.Add("LOCAT= '" + strLocat + "'");

                        strSQL = objDataWhhed.EntityGetUpdateSql(alCondition);
                        // strSQL = "Update WHHED set STSET='" + strStset + "', STHGH='" + strSthgh + "', STLEN='" + strStlen + "', STWID='" + strStwid + "' where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and LGORT= '" + strLgort + "' and LOCAT= '" + strLocat + "'";
                    }
                    else
                    {
                        objDataWhhed.ResetField();
                        objDataWhhed.Stset = "null";
                        objDataWhhed.Sthgh = "null";
                        objDataWhhed.Stlen = "null";
                        objDataWhhed.Stwid = "null";
                        alCondition.Clear();
                        alCondition.Add("MANDT='" + MANDT + "'");
                        alCondition.Add("COMCD='" + COMCD + "'");
                        alCondition.Add("WERKS= '" + strWerks + "'");
                        alCondition.Add("LGORT= '" + strLgort + "'");
                        alCondition.Add("LOCAT= '" + strLocat + "'");
                        strSQL = objDataWhhed.EntityGetUpdateSql(alCondition);

                        //strSQL = "Update WHHED set STSET=null, STHGH=null, STLEN=null, STWID=null where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and LGORT= '" + strLgort + "' and LOCAT= '" + strLocat + "'";

                    }
                    bool bolReturn = false;
                    bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            #region 删除固定储位料号
            /// <summary>
            /// 删除固定储位料号
            /// </summary> 
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.DeleteFixedMatnr(strWerks, strLgort, strLocat, strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool DeleteFixedMatnr(string strWerks, string strLgort, string strLocat, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteFixedMatnr";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region old function
                    //string strSQL = "";
                    //strSQL = "Delete from WHMAP where MANDT='" + MANDT + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT='" + strLocat + "' and MATNR='" + strMatnr + "'";
                    //StringBuilder sbSQL = new StringBuilder();
                    //sbSQL.AppendFormat(" Delete from WHMAP where MANDT='{0}' and COMCD='{1}' and WERKS='{2}' and LGORT='{3}' and LOCAT='{4}' and MATNR='{5}'", MANDT, COMCD, strWerks, strLgort, strLocat, strMatnr);
                    //ControlHandleDB();
                    //bool bolReturn = false;
                    //bolReturn = ControlSqlAccess.ExecSql(sbSQL.ToString());
                    //ControlSqlAccess.CloseConnection();
                    //return bolReturn;
                    #endregion

                    ArrayList alConditions = new ArrayList();
                    DataWhmap objWhmap = new DataWhmap(UserData);
                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS= '" + strWerks + "'");
                    alConditions.Add("LGORT= '" + strLgort + "'");
                    alConditions.Add("LOCAT= '" + strLocat + "'");
                    alConditions.Add("MATNR= '" + strMatnr + "'");
                    bool bolReturn = false;
                    bolReturn = objWhmap.EntityDelete(alConditions);
                    return bolReturn;

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

            #region 新增固定储位料号
            /// <summary>
            /// 新增固定储位料号
            /// </summary> 
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.AddFixedMatnr(strWerks, strLgort, strLocat, strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddFixedMatnr(string strWerks, string strLgort, string strLocat, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddFixedMatnr";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region old function
                    //string strSQL = "";
                    //strSQL = "Insert into WHMAP(MANDT, WERKS, LGORT, LOCAT, MATNR, CRNAM, CRDAT) values('" + MANDT + "','" + strWerks + "','" + strLgort + "','" + strLocat + "', '" + strMatnr + "', '" + CRNAM + "', getdate() )";                
                    //StringBuilder sbSQL = new StringBuilder();
                    //sbSQL.Append(" Insert into WHMAP(MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, CRNAM, CRDAT) values(")
                    //     .AppendFormat(" '{0}',", MANDT)
                    //     .AppendFormat(" '{0}',", COMCD)
                    //     .AppendFormat(" '{0}',", strWerks)
                    //     .AppendFormat(" '{0}',", strLgort)
                    //     .AppendFormat(" '{0}',", strLocat)
                    //     .AppendFormat(" '{0}',", strMatnr)
                    //     .AppendFormat(" '{0}',", CRNAM)
                    //     .Append("  getdate())");
                    //ControlHandleDB();
                    //bool bolReturn = false;
                    //bolReturn = ControlSqlAccess.ExecSql(sbSQL.ToString());
                    //ControlSqlAccess.CloseConnection();
                    //return bolReturn;
                    #endregion

                    DataWhmap objWhmap = new DataWhmap(UserData);
                    objWhmap.Mandt = MANDT;
                    objWhmap.Comcd = COMCD;
                    objWhmap.Werks = strWerks;
                    objWhmap.Lgort = strLgort;
                    objWhmap.Locat = strLocat;
                    objWhmap.Matnr = strMatnr;
                    objWhmap.Crnam = CRNAM;
                    objWhmap.Crdat = "getdate()";
                    bool bolReturn = false;
                    bolReturn = objWhmap.EntityInsert();
                    return bolReturn;
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

            #region 更改储位Block状态
            ////////////Summary by Bruce Zhang 20091029 //////////////////////////////////////////////////////////
            /// <summary>
            /// 更改储位Block状态
            /// </summary> 
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="dtData">DataTable</param>
            /// <param name="strStatus">状态</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Admin objAdmin =new QCI.QWMS.Admin(strConnectionString, strMandt, strCrnam, strProgid);
            ///  bool  bolReturn = objAdmin.UpdateLocationBlock(string strWerks, string strLgort, DataTable dtData, string strStatus);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool UpdateLocationBlock(string strWerks, string strLgort, DataTable dtData, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateLocationBlock";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + dtData + "','" + strStatus + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataWhhed objDataWhhed = new DataWhhed(UserData);
                    ArrayList alSQL = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    foreach (DataRow dr in dtData.Rows)
                    {
                        if (int.Parse(strStatus) == 1)
                        {
                            objDataWhhed.Block = "0";
                        }
                        else
                        {
                            objDataWhhed.Block = "1";
                        }
                        alCondition.Clear();
                        alCondition.Add("MANDT='" + MANDT + "'");
                        alCondition.Add("COMCD='" + COMCD + "'");
                        alCondition.Add("WERKS= '" + strWerks + "'");
                        alCondition.Add("LGORT= '" + strLgort + "'");
                        alCondition.Add("LOCAT= '" + dr["LOCAT"].ToString() + "'");
                        alSQL.Add(objDataWhhed.EntityGetUpdateSql(alCondition));
                    }

                    //if (int.Parse(strStatus) == 1)
                    //{
                    //    for (int i = 0; i < dtData.Rows.Count; i++)
                    //    {
                    //        strLocat = dtData.Rows[i]["LOCAT"].ToString();
                    //        strSQL = "Update WHHED set BLOCK = 0 where MANDT='" + MANDT + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT='" + strLocat + "'";
                    //        arySQL.Add(strSQL);
                    //    }
                    //}
                    //else if (int.Parse(strStatus) == 0)
                    //{
                    //    for (int i = 0; i < dtData.Rows.Count; i++)
                    //    {
                    //        strLocat = dtData.Rows[i]["LOCAT"].ToString();
                    //        strSQL = "Update WHHED set BLOCK = 1 where MANDT='" + MANDT + "' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and LOCAT='" + strLocat + "'";
                    //        arySQL.Add(strSQL);
                    //    }
                    //}

                    ControlHandleDB();
                    bool bolReturn = false;
                    bolReturn = ControlSqlAccess.ExecSqlArray(alSQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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

            # region 删除过期资料
            /// <summary>
            /// 删除过期资料
            /// </summary> 
            /// <param name="aryLogType">删除的资料类型 0:LOG 1:盘点票 2:SAP下载资料</param>
            /// <param name="strWerks">Plant</param>  
            /// <param name="strLgort">Storage</param>  
            /// <param name="strStartDate">开始日期</param>
            /// <param name="strEndDate">结束日期</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeleteOldLog(aryLogType, strWerks, strLgort, strStartDate, strEndDate);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////           
            public bool DeleteOldLog(ArrayList aryLogType, string strWerks, string strLgort, string strStartDate, string strEndDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteOldLog";
                this.ControlMethodParm = "('" + aryLogType + "','" + strWerks + "','" + strLgort + "','" + strStartDate + "','" + strEndDate + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strLogSql = "";
                    ArrayList arlSql = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhlog objDataWhlog = new DataWhlog(UserData);
                    DataWhdwn objDataWhdwn = new DataWhdwn(UserData);
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);

                    for (int i = 0; i < aryLogType.Count; i++)
                    {
                        if (aryLogType[i].ToString() == "0")
                        {
                            //LOG
                            //arlSql.Add("delete from WHLOG where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and CRDAT between '" + strStartDate + "' and '" + strEndDate + "'");

                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + MANDT + "'");
                            alConditions.Add(" COMCD='" + COMCD + "'");
                            alConditions.Add(" WERKS='" + strWerks + "'");
                            alConditions.Add(" LGORT='" + strLgort + "'");
                            alConditions.Add(" CRDAT between '" + strStartDate + "' and '" + strEndDate + "' ");
                            arlSql.Add(objDataWhlog.EntityGetDeleteSql(alConditions));
                            strLogSql = objDataWhlog.EntityGetDeleteSql(alConditions);
                        }
                        else if (aryLogType[i].ToString() == "2")
                        {
                            //SAP下载资料
                            //arlSql.Add("delete from WHDWN where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and CRDAT between '" + strStartDate + "' and '" + strEndDate + "'");

                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + MANDT + "'");
                            alConditions.Add(" COMCD='" + COMCD + "'");
                            alConditions.Add(" WERKS='" + strWerks + "'");
                            alConditions.Add(" LGORT='" + strLgort + "'");
                            alConditions.Add(" CRDAT between '" + strStartDate + "' and '" + strEndDate + "' ");
                            arlSql.Add(objDataWhdwn.EntityGetDeleteSql(alConditions));
                            strLogSql = objDataWhdwn.EntityGetDeleteSql(alConditions);
                        }
                        else if (aryLogType[i].ToString() == "1")
                        {
                            //盘点票
                            //arlSql.Add("delete from WHCYC where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and CRDAT between '" + strStartDate + "' and '" + strEndDate + "'");

                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + MANDT + "'");
                            alConditions.Add(" COMCD='" + COMCD + "'");
                            alConditions.Add(" WERKS='" + strWerks + "'");
                            alConditions.Add(" LGORT='" + strLgort + "'");
                            alConditions.Add(" CRDAT between '" + strStartDate + "' and '" + strEndDate + "' ");
                            arlSql.Add(objDataWhcyc.EntityGetDeleteSql(alConditions));
                            strLogSql = objDataWhcyc.EntityGetDeleteSql(alConditions);
                        }

                        //記錄刪掉資料的User ID  Smose Liao 20100629
                        DataErrlog objErrlog = new DataErrlog(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);
                        alColumns.Clear();
                        alConditions.Clear();
                        objErrlog.ResetField();

                        objErrlog.Mandt = MANDT;
                        objErrlog.Comcd = COMCD;
                        objErrlog.Logtim = "GetDate()";
                        objErrlog.Maltim = "GetDate()";
                        objErrlog.Logsql = strLogSql;
                        objErrlog.Usrnm = CRNAM;
                        objErrlog.Malflg = aryLogType[i].ToString();

                        arlSql.Add(objErrlog.EntityGetInsertSql());
                    }

                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
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
            # endregion

            # region 删除删除Plant资料
            /// <summary>
            /// 删除Plant资料
            /// </summary> 
            /// <param name="strWerks">Plant</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.DeletePlant(strWerks);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////

            public bool DeletePlant(string strWerks, UserInfo UserData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeletePlant";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arlSql = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhctrl objDataWhctrl = new DataWhctrl(UserData);
                    DataWhhed objDataWhhed = new DataWhhed(UserData);
                    DataWhitm objDataWhitm = new DataWhitm(UserData);
                    DataWhmtr objDataWhmtr = new DataWhmtr(UserData);
                    DataWhmap objDataWhmap = new DataWhmap(UserData);
                    DataWhstg objDataWhstg = new DataWhstg(UserData);
                    DataWhcst objDataWhcst = new DataWhcst(UserData);
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);
                    DataWhaut objDataWhaut = new DataWhaut(UserData);

                    # region old function
                    //arlSql.Add("Delete from WHCTRL where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='WERKS' and CTRLNM='" + strWerks + "'");
                    //arlSql.Add("Delete from WHCTRL where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='LGORT' and CTRLNM='" + strWerks + "'");
                    //arlSql.Add("Delete from WHHED  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHITM  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHMTR  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHMAP  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHSTG  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHCST  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHCYC  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    //arlSql.Add("Delete from WHAUT  where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + strWerks + "'");
                    # endregion

                    string strSQL;
                    strSQL = "";                        //select * from WHLOG where RMAK1='Delete Plant'    
                    strSQL = "  insert into WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,TRNTP,MENGE,CRNAM,CRDAT,RMAK1,RMAK2,COMCD)  VALUES('" + UserData.Client + "','" + strWerks + "','','A1','','','',0,'" + UserData.UserId + "',GETDATE(),'Delete Plant','" + UserData.ClientIP + "','" + UserData.CompanyCode + "')   ";
                    arlSql.Add(strSQL);


                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='WERKS' ");
                    alConditions.Add(" CTRLNM='" + strWerks + "'");
                    arlSql.Add(objDataWhctrl.EntityGetDeleteSql(alConditions));

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='LGORT' ");
                    alConditions.Add(" CTRLNM='" + strWerks + "'");
                    arlSql.Add(objDataWhctrl.EntityGetDeleteSql(alConditions));

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + strWerks + "'");
                    arlSql.Add(objDataWhhed.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhitm.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhmtr.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhmap.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhstg.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhcst.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhcyc.EntityGetDeleteSql(alConditions));
                    arlSql.Add(objDataWhaut.EntityGetDeleteSql(alConditions));



                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arlSql);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;

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
            # endregion

            # region 新增Plant
            /// <summary>
            /// 新增Plant
            /// </summary> 
            /// <param name="strWerks">紅跋</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.AddPlant(strWerks);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddPlant(string strWerks)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddPlant";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "Insert WHCTRL(MANDT, SOLDTO, CTRLID, CTRLNM, CTRLC1,COMCD) values('" + MANDT + "', 'QWMS', 'WERKS','" + strWerks.ToUpper() + "','" + strWerks.ToUpper() + "','" + COMCD + "')";

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhctrl objDataWhctrl = new DataWhctrl(UserData);

                    objDataWhctrl.Mandt = MANDT;
                    objDataWhctrl.Soldto = "QWMS";
                    objDataWhctrl.Ctrlid = "WERKS";
                    objDataWhctrl.Ctrlnm = strWerks.ToUpper();
                    objDataWhctrl.Ctrlc1 = strWerks.ToUpper();
                    objDataWhctrl.Comcd = COMCD;

                    bool bolReturn = false;
                    bolReturn = objDataWhctrl.EntityInsert();
                    //ControlHandleDB();
                    //bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    //ControlSqlAccess.CloseConnection();

                    return bolReturn;

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
            # endregion

            # region 查看料號
            /// <summary>
            /// 查看料號 by Rock Tzeng
            /// </summary> 
            /// <param name="strMatnr">料號</param>          
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.CheckExistedPart(varMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool CheckExistedPart(string varMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddPlant";
                this.ControlMethodParm = "('" + varMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhpat objDataWhpat = new DataWhpat(UserData);
                    ArrayList alSQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtMatnr = new DataTable();
                    bool bolReturn = false;

                    #region 判斷此料號是否已存在
                    alColumns.Add("'Y'");
                    if (varMatnr.Trim() != "")
                    {
                        alConditions.Add(" Matnr='" + varMatnr.Trim() + "'");
                    }

                    objDataWhpat.EntityGetQuerySql(alColumns, alConditions, false, true);
                    dtMatnr = objDataWhpat.EntityQuery(alColumns, alConditions, false, true);
                    if (dtMatnr.Rows.Count > 0)
                    {
                        bolReturn = true;
                    }
                    else
                    {
                        bolReturn = false;
                    }
                    #endregion

                    return bolReturn;

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
            # endregion

            # region 新增料號
            /// <summary>
            /// 新增料號 by Rock Tzeng
            /// </summary> 
            /// <param name="strMatnr">料號</param>
            /// <param name="strMaktx">料號描述</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"A3");
            ///  bool  bolReturn = objAdmin.AddPartNo(varMatnr,varMaktx);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddPartNo(string varMatnr, string varMaktx)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddPlant";
                this.ControlMethodParm = "('" + varMatnr + "','" + varMaktx + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhpat objDataWhpat = new DataWhpat(UserData);
                    ArrayList alSQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtMatnr = new DataTable();
                    bool bolReturn = false;

                    objDataWhpat.Matnr = varMatnr.Trim();
                    objDataWhpat.Maktx = varMaktx.Trim();
                    objDataWhpat.Crdat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".000";
                    string strSql = objDataWhpat.EntityGetInsertSql();
                    bolReturn = objDataWhpat.EntityInsert();

                    return bolReturn;

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
            # endregion

            # region 批量插入料号信息

            public bool InsertMatnrMore(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InsertMatnrMore";
                this.ControlMethodParm = "('" + dtData + "')";
                bool bolReturn = true;
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    ControlHandleDB();//"WHPAT", dtData,
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(ControlSqlAccess.ConnectionString);
                    bulkCopy.DestinationTableName = "WHPAT";
                    bulkCopy.WriteToServer(dtData);
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    bolReturn = false;
                    throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex)
                {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    ControlExceptionType = ex.GetType().FullName;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    bolReturn = false;
                    throw new Exception("999");
                }
                return bolReturn;
            }
            # endregion

            # region 删除料号
            public bool DeletePartNo(string varMatnr, UserInfo UserData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeletePartNo";
                this.ControlMethodParm = "('" + varMatnr + "')";
                bool bolReturn = false;
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ControlHandleDB();
                    string strSql = string.Empty;    //select * from WHLOG where RMAK1='Delete Part.NO'
                    strSql = "  insert into WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,TRNTP,MENGE,CRNAM,CRDAT,RMAK1,RMAK2,COMCD)  VALUES('" + UserData.Client + "','','','A10','','" + varMatnr + "','',0,'" + UserData.UserId + "',GETDATE(),'Delete Part.NO','" + UserData.ClientIP + "','" + UserData.CompanyCode + "')   ";
                    strSql += " DELETE FROM WHPAT WHERE MATNR='" + varMatnr + "' ";

                    bolReturn = ControlSqlAccess.ExecSql(strSql);
                    return bolReturn;

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
            # endregion

            #region
            public bool ModifyPartNo(string varMatnr, string varMaktx)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ModifyPartNo";
                this.ControlMethodParm = "('" + varMatnr + "','" + varMaktx + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhpat objDataWhpat = new DataWhpat(UserData);
                    ArrayList alSQL = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtMatnr = new DataTable();
                    bool bolReturn = false;

                    objDataWhpat.Maktx = varMaktx.Trim();
                    objDataWhpat.Modat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".000";

                    alConditions.Clear();
                    alConditions.Add("MATNR='" + varMatnr + "'");

                    string strSql = objDataWhpat.EntityGetUpdateSql(alConditions);
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSql(strSql);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;

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

            # region 檢查是否啟動祥天FINAL的機制
            /// <summary>
            /// 檢查是否啟動祥天FINAL的機制
            /// </summary> 
            /// <param name="varType">SMT/FINAL</param>
            /// <returns>
            /// True/False
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Admin objAdmin = new Admin(UserData,"CC");
            ///  DataTable dtData = objWhctrl.CheckPower2FinalSwith(varType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckPower2FinalSwith(string varType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckPower2FinalSwith";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    bool bolActive = false;

                    alColumns.Add("CTRLNM");
                    alConditions.Add("SOLDTO= 'QWMS'");
                    alConditions.Add("CTRLID='STOTYP'");
                    alConditions.Add("CTRLC1='" + varType + "'");

                    string strSQL = objWhctrl.EntityGetQuerySql(alColumns, alConditions, true, true);

                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                    if (int.Parse(dtData.Rows[0]["CTRLNM"].ToString()) == 1)
                        bolActive = true;

                    return bolActive;
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
            # endregion

            #region 新增或更改H95料号DateCode期限
            public bool AddPartNoDateCodeStockTime(string Matnr, string WarningTime, string DacodStockTime, string Insmk, DataTable dtData)
            {
                bool bolReturn = false;
                string strSQL = "";

                //未查到数据做新增
                if (dtData.Rows.Count == 0)
                {
                    strSQL = "Insert into WHHDC (MANDT,MATNR,INSMK,StockTime,AdvanceWarningTime,CRDAT,MODAT,COMCD) values ('" + MANDT + "','" + Matnr +
                             "','" + Insmk + "','" + DacodStockTime + "','" + WarningTime + "',getdate(),getdate(),'" + COMCD + "')";
                }
                //查到数据做修改
                if (dtData.Rows.Count != 0)
                {
                    strSQL = "update WHHDC set StockTime='" + DacodStockTime + "',AdvanceWarningTime='" + WarningTime +
                        "',MODAT=getdate() where MATNR = '" + Matnr + "' and INSMK='" + Insmk + "'";
                }

                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryStorageData_DateCode ";
                    //throw new System.Exception(ex.Message +"<- QueryStorageData " );

                    ERRMSG = ex.Message + "<- QueryStorageData_DateCode()";
                }
                return bolReturn;
            }
            #endregion

            #region 延长H95料号DateCode期限
            public bool ExtendDateCodeStockTime(string Werks, string Lgort, string Matnr, string Insmk, string Dacod, string Extend, string Aretdat, DataTable dtData)
            {
                bool bolReturn = false;
                string strSQL = "";
                //未查到数据做新增
                if (dtData.Rows[0]["ETDAT"].ToString() == "")
                {
                    strSQL = "Insert into WHETDC (MANDT,WERKS,LGORT,MATNR,INSMK,DACOD,ETDAT,ARETDAT,CRDAT,MODAT,COMCD) values ('" + MANDT + "','" + Werks +
                             "','" + Lgort + "','" + Matnr + "','" + Insmk + "','" + Dacod + "','" + Extend + "','0',getdate(),getdate(),'" + COMCD + "')";
                }
                //查到数据做修改
                if (dtData.Rows[0]["ETDAT"].ToString() != "")
                {

                    strSQL = "update WHETDC set ETDAT='" + Extend + "',ARETDAT='" + Aretdat.ToString() +
                        "',MODAT=getdate() where WERKS='" + Werks + "' and LGORT='" + Lgort + "' and MATNR = '" + Matnr + "' and INSMK='" + Insmk + "' and DACOD='" + Dacod + "'";
                }

                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryStorageData_DateCode ";
                    //throw new System.Exception(ex.Message +"<- QueryStorageData " );

                    ERRMSG = ex.Message + "<- QueryStorageData_DateCode()";
                }
                return bolReturn;
            }
            #endregion

            #region 新增或删除报表收件人
            public bool AddOrDeleteAddressee(string Werks, string Report, string strAdres, string strAdres2, string Type)
            {
                bool bolReturn = false;
                string strSQL = "";
                string strAddAdres = strAdres + strAdres2 + ";";
                string strDelAdres = strAdres.Replace(strAdres2 + ";", "");
                //未查到数据做新增
                if (Type == "ADD")
                {
                    strSQL = "UPDATE WHCTRL SET REMAK2='" + strAddAdres.Trim() + "' WHERE CTRLNM='" + Werks + "'  AND CTRLC5=N'" + Report + "' AND CTRLC1='QWMS_Report'";
                }
                //查到数据做修改
                if (Type == "DELETE")
                {
                    strSQL = "UPDATE WHCTRL SET REMAK2='" + strDelAdres.Trim() + "' WHERE CTRLNM='" + Werks + "'  AND CTRLC5=N'" + Report + "' AND CTRLC1='QWMS_Report'";
                }

                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryStorageData_DateCode ";
                    //throw new System.Exception(ex.Message +"<- QueryStorageData " );

                    ERRMSG = ex.Message + "<- QueryStorageData_DateCode()";
                }
                return bolReturn;
            }
            #endregion

            #region 确认是否为SAP仓别

            public bool CheckSAPLgort(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckSAPLgort";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool bolReturn = false;
                try
                {
                    // CTRLN2=1  表示 需要接收SAP单据仓别,0表示不需要接收单据
                    string strSQL = " SELECT 1 FROM WHCTRL WHERE CTRLID='LGORT' AND CTRLNM='" + strWerks + "' AND CTRLN2=1   AND CTRLC1='" + strLgort + "' ";
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    DataTable dtData = sqlAccess.GetDataTable(strSQL);
                    sqlAccess.CloseConnection();

                    if (dtData.Rows.Count > 0)
                    {
                        bolReturn = true;
                    }

                    return bolReturn;
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

            # region 获取系统负责人

            /// <summary>
            /// 获取每个公司别对应的系统负责人
            /// </summary>
            /// <returns></returns>
            public DataTable GetSystemManager()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSystemManager";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    string strSQL = (" SELECT CTRLC1+CTRLNM AS MANAGER FROM  WHCTRL   WHERE CTRLID='SystemManager' ORDER BY CTRLN1  ");
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSQL);
                    sqlAccess.CloseConnection();

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
            # endregion

            #region 设置二维码转料打印密码
            /// <summary>
            /// 转料二维码打印密码固定
            /// </summary>
            /// <returns></returns>
            public DataTable GetPassword()
            {
                this.ControlMethodName = "GetPassword";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append("SELECT PASWD FROM [dbo].[WHUSR]  WITH(NOLOCK) WHERE USRNM IN ('02090444')");

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtResult = sqlAccess.GetDataTable(sbSQL.ToString());
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
                return dtResult;
            }
            #endregion

            #region 设置调拨删除账号 Add By Lora
            /// <summary>
            /// 调拨删除账号密码设置
            /// </summary>
            /// <param name="varUsernm"></param>
            /// <returns></returns>
            public DataTable GetPassword(string varUsernm)
            {
                this.ControlMethodName = "GetPassword";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                if (varUsernm == "TRANSFER")
                {
                    sbSQL.AppendFormat("declare @tem varchar(max); select @tem=CHSNM from WHUSR WITH(NOLOCK) where USRNM='{0}'; select a PASWD from dbo.FN_Split(@tem,';')", varUsernm);
                }
                else
                {
                    sbSQL.AppendFormat("SELECT PASWD FROM [dbo].[WHUSR]  WITH (NOLOCK) WHERE USRNM='{0}'", varUsernm);
                }
                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtResult = sqlAccess.GetDataTable(sbSQL.ToString());
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
                return dtResult;
            }
            #endregion

            #region 料号人员维护   增/删/改   by Galen
            /// <summary>
            /// 料号人员维护 增/删/改
            /// </summary>
            /// <param name="strType">操作类型</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="strMqcID">MQC工号</param>
            /// <param name="strMqcnm">MQC姓名</param>
            /// <param name="strPeID">PE工号</param>
            /// <param name="strPenm">PE姓名</param>
            /// <returns>是否成功</returns>
            public bool Material_personnel_modify(string strWerks, string strLgort, string strType, string strMatnr, string strMqcID, string strMqcnm, string strPeID, string strPenm)
            {
                this.ControlMethodName = "Material_personnel_modify";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSQL = new StringBuilder();
                if (strType == "add")
                {
                    #region 增加
                    //matnr mqcid mqcnm 
                    sbSQL.AppendFormat("INSERT INTO PNREMAK VALUES('218','" + strComcd + "','" + strWerks + "','" + strLgort + "','" + strMatnr + "','" + strMqcID + "',N'" + strMqcnm + "','" + strPeID + "',N'" + strPenm + "',GETDATE(),GETDATE(),NULL,NULL) ");
                    #endregion
                }
                else if (strType == "del")
                {
                    #region 删除
                    //matnr
                    sbSQL.AppendFormat("DELETE PNREMAK WHERE MATNR='" + strMatnr + "' AND WERKS ='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    #endregion
                }
                else if (strType == "modify")
                {
                    #region 修改
                    //matnr mqcid mqcnm
                    sbSQL.AppendFormat("UPDATE PNREMAK SET MODAT=GETDATE(),MQCID='" + strMqcID + "',MQCNM=N'" + strMqcnm + "' ");
                    if (strPeID != "")
                    {
                        sbSQL.AppendFormat(",PEID='" + strPeID + "'");
                    }
                    if (strPenm != "")
                    {
                        sbSQL.AppendFormat(",PENM=N'" + strPenm + "'");
                    }
                    sbSQL.AppendFormat(" WHERE MATNR='" + strMatnr + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    #endregion
                }
                else
                {
                    return false;
                }


                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSQL.ToString());
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

            #region 料号人员维护    查   by Galen
            public DataTable Material_personnel_query(string strWerks, string strLgort, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "Material_personnel_query";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,MATNR,MQCID,MQCNM,PEID,PENM,CRDAT,MODAT,REMAK1,ULFLG FROM PNREMAK WITH(NOLOCK) ");
                strSQL.AppendFormat("WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                if (strMatnr != "")
                {
                    strSQL.AppendFormat("AND MATNR LIKE '" + strMatnr + "%' ");
                }
                strSQL.AppendFormat("ORDER BY MODAT");
                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSQL.ToString());
                    sqlAccess.CloseConnection();
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

            #region     查询公告
            public DataTable QuaryNotice()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryNotice";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat(" SELECT REMAK,COLOR,SIZE,FORMAT,LOCX,LOCY FROM NOTICE WITH(NOLOCK) WHERE FLAGE='Y' ORDER BY SORT ");

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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

            #region 查询文档路径
            public DataTable QuaryDocumentPath()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryDocumentPath";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat(" SELECT CTRLC1 FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='Document' AND CTRLNM='Path' ");

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(strSQL.ToString());
                    sqlAccess.CloseConnection();
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

            #region 智能收料人员维护   增/删/改   by Zachary
            /// <summary>
            /// 智能收料人员维护 增/删/改
            /// </summary>
            /// <param name="strType">操作类型</param>
            /// <param name="strUserID">工号</param>
            /// <returns>是否成功</returns>
            public bool Intelligent_personnel_modify(string strMandt, string strComcd, string strWerks, string strLgort, string strType, string strUserID, string strUsrnm, string strCrnam)
            {
                this.ControlMethodName = "Intelligent_personnel_modify";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSQL = new StringBuilder();
                if (strType == "add")
                {
                    #region 增加

                    //sbSQL.AppendFormat("INSERT INTO PNREMAK VALUES('218','" + strComcd + "','" + strWerks + "','" + strLgort + "','" + strMatnr + "','" + strMqcID + "',N'" + strMqcnm + "','" + strPeID + "',N'" + strPenm + "',GETDATE(),GETDATE(),NULL,NULL) ");
                    sbSQL.AppendFormat("INSERT INTO WHAIP VALUES('" + strMandt + "','" + strComcd + "','" + strWerks + "','" + strLgort + "','" + strUserID + "',N'" + strUsrnm + "',N'" + strCrnam + "',GETDATE(),NULL,NULL,NULL) ");

                    #endregion
                }
                else if (strType == "del")
                {
                    #region 删除
                    //matnr
                    //sbSQL.AppendFormat("DELETE PNREMAK WHERE MATNR='"' AND WERKS ='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    DataTable dt = PermissionQuery(strMandt, strComcd, strWerks, strUserID);
                    string Lgorts=dt.Rows[0]["LGORT"].ToString();
                    string[] sArray = Lgorts.Split(';');
                    if (strLgort == "" || Lgorts.Length == 4)
                    {
                        sbSQL.AppendFormat("DELETE WHAIP WHERE COMCD='" + strComcd + "' AND WERKS ='" + strWerks + "' AND USRID='" + strUserID +"'");
                    }
                    else if (strLgort == sArray[0])
                    {
                        sbSQL.AppendFormat("UPDATE WHAIP SET LGORT=replace(LGORT,'" + strLgort + ";',''),MONAM='" + strCrnam + "',MODAT=GETDATE() WHERE MANDT='" + strMandt + "' AND COMCD='" + strComcd + "' AND WERKS='" + strWerks + "' AND USRID='" + strUserID + "'");
                    }
                    else
                    {
                        sbSQL.AppendFormat("UPDATE WHAIP SET LGORT=replace(LGORT,';" + strLgort + "',''),MONAM='" + strCrnam + "',MODAT=GETDATE() WHERE MANDT='" + strMandt + "' AND COMCD='" + strComcd + "' AND WERKS='" + strWerks + "' AND USRID='" + strUserID + "'");
                    }
                    
                    #endregion
                }
                else if (strType == "modify")
                {
                    #region 修改
                    //matnr mqcid mqcnm
                    sbSQL.AppendFormat("UPDATE WHAIP SET LGORT=LGORT+';'+'" + strLgort + "',MONAM='" + strCrnam + "',MODAT=GETDATE() WHERE MANDT='" + strMandt + "' AND COMCD='" + strComcd + "' AND WERKS='" + strWerks + "' AND USRID='" + strUserID+"'");
                    //if (strPeID != "")
                    //{
                    //    sbSQL.AppendFormat(",PEID='" + strPeID + "'");
                    //}
                    //if (strPenm != "")
                    //{
                    //    sbSQL.AppendFormat(",PENM=N'" + strPenm + "'");
                    //}
                    //sbSQL.AppendFormat(" WHERE MATNR='" + strMatnr + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                    #endregion
                }
                else
                {
                    return false;
                }


                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSQL.ToString());
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

            #region 智能收料人员维护   查   by Zachary
            /// <summary>
            /// 智能收料人员维护 查
            /// </summary>
            /// <param name="strUserID">工号</param>
            /// <returns>table内容</returns>
            public DataTable PermissionQuery(string strMandt, string strComcd,string strWerks, string strUserID)
            {
                this.ControlMethodName = "PermissionQuery";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append("SELECT COMCD AS SITE,USRID,USRNM,WERKS,LGORT,REMAK1 FROM WHAIP WITH (Nolock) WHERE ");
                sbSQL.AppendFormat("MANDT='{0}' ", strMandt);
                sbSQL.AppendFormat("AND COMCD='{0}' ", strComcd);
                if(strWerks!="")
                {
                    sbSQL.AppendFormat("AND WERKS='{0}' ", strWerks);
                }
                if (strUserID!="")
                {
                    sbSQL.AppendFormat("AND USRID='{0}' ", strUserID);
                }
                
                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtData = sqlAccess.GetDataTable(sbSQL.ToString());
                    sqlAccess.CloseConnection();
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

            # region 从AlimAPI获取人员信息
            /// <summary>
            /// 从AlimAPI获取人员信息
            /// </summary> 
            /// <param name="strUsrnm">工号</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// </remarks>
            /// </code>
            /// </example>
            public DataTable CheckHR(string strUsrnm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckHR";
                this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ClaHttpHelper claheepHelper = new ClaHttpHelper();
                    var Data = new
                    {
                        UserName = strUsrnm,
                        Neweid = string.Empty,
                    };
                    string strData = JsonConvert.SerializeObject(Data);

                    QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                    //域名配置在数据库中
                    string strUrl = objAuthority.GetApiRequestUrl();
                    strUrl = strUrl + "/QWMSAPI/api/QsmcQwms/GetHRDataFromQSBN";

                    //旧：固定写死
                    //string strUrl = "http://172.19.81.219/AlimAPI/api/QsmcQwms/GetHRDataFromQSBN";


                    string strResult = claheepHelper.HttpPostByHttpWebRequest(strUrl, strData);
                    DataTable dtHR = JsonConvert.DeserializeObject<DataTable>(strResult);
                    return dtHR;
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

            #region 访问OA API获取员工信息
            public DataTable GetEmployeeData(string strUsrnm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetEmployeeData";
                this.ControlMethodParm = "('" + strUsrnm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ClaHttpHelper claheepHelper = new ClaHttpHelper();
                    object Data;

                    if (strUsrnm.Trim().Length == 8) // 工号
                    {
                        Data = new
                        {
                            CompanyCode = "7300",
                            Type = "GetEmployeeData_New",
                            site = "QMH",
                            EmployeeID = strUsrnm.Trim()
                        };
                    }
                    else // 卡号
                    {
                        Data = new
                        {
                            CompanyCode = "7300",
                            Type = "GetEmployeeData_New",
                            site = "QMH",
                            CardNo = strUsrnm.Trim()
                        };
                    }

                    string strData = JsonConvert.SerializeObject(Data);


                    DataTable dt = new DataTable();

                    QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                    //域名配置在数据库中
                    string strUrl = objAuthority.GetApiRequestUrl();
                    strUrl = strUrl + "/QWMSAPI/api/QsmcQwms/GetEmployeeData";
                    string strResult = claheepHelper.HttpPostByHttpWebRequest(strUrl, strData);

                    JObject jsonObj = (JObject)JsonConvert.DeserializeObject(strResult);
                    if ((bool)jsonObj["result"] == true)
                    {
                        JArray dataArray = (JArray)jsonObj["data"];
                        dt = JsonConvert.DeserializeObject<DataTable>(dataArray.ToString());
                    }
                    if (dt.Rows.Count == 0)//若输入的是上海工号或卡号
                    {
                        if (strUsrnm.Trim().Length == 8) // 工号
                        {
                            Data = new
                            {
                                CompanyCode = "9200",
                                Type = "GetEmployeeData_New",
                                site = "QSMC",
                                EmployeeID = strUsrnm.Trim()
                            };
                        }
                        else // 卡号
                        {
                            Data = new
                            {
                                CompanyCode = "9200",
                                Type = "GetEmployeeData_New",
                                site = "QSMC",
                                CardNo = strUsrnm.Trim()
                            };
                        }
                        strData = JsonConvert.SerializeObject(Data);
                        strResult = claheepHelper.HttpPostByHttpWebRequest(strUrl, strData);
                        jsonObj = (JObject)JsonConvert.DeserializeObject(strResult);
                        JArray dataArray = (JArray)jsonObj["data"];
                        dt = JsonConvert.DeserializeObject<DataTable>(dataArray.ToString());
                    }
                    return dt;

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
            # endregion

            # region 查询DateCodeRule
            /// <summary>
            /// 查询DateCodeRule
            /// </summary>
            /// <param name="strVendor">厂商</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public DataTable QueryDCRule(string strVendor, string strDescription)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDCRule";
                this.ControlMethodParm = "('" + strVendor + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    string strSql = string.Empty;
                    if(string.IsNullOrEmpty(strVendor) && string.IsNullOrEmpty(strDescription))
                    {
                        strSql = "SELECT CTRLC1 RuleItem,CTRLC5 Describe,REMAK VEDOR FROM WHCTRL WITH(NOLOCK) WHERE CTRLID='D/C_ADMIN' AND CTRLNM='TransferRule' ORDER BY CTRLC1";
                    }
                    else
                    {
                        strSql = "WITH T AS(SELECT CTRLC1, CTRLC5, value VENDOR  FROM WHCTRL  CROSS APPLY STRING_SPLIT(REMAK, ';') WHERE CTRLID='D/C_ADMIN' AND CTRLNM='TransferRule') SELECT CTRLC1 RuleItem,CTRLC5 Describe,VENDOR VEDOR FROM T ";
                        if (!string.IsNullOrEmpty(strVendor) && string.IsNullOrEmpty(strDescription))
                            strSql += "WHERE VENDOR IN ('" + strVendor + "')";
                        if (!string.IsNullOrEmpty(strDescription) && string.IsNullOrEmpty(strVendor))
                            strSql += "WHERE CTRLC1 ='" + strDescription + "'";
                        if (!string.IsNullOrEmpty(strVendor) && !string.IsNullOrEmpty(strDescription))
                            strSql += "WHERE VENDOR IN ('" + strVendor + "') AND CTRLC1 ='" + strDescription + "'";
                    }
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dtResult = sqlAccess.GetDataTable(strSql);
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
            # endregion

            # region 新增或者删除DateCodeRule
            public bool DeleteOrAddDCRule(string strType, string strVendor, string strDescriptionItem)
            {
                this.ControlMethodName = "DeleteOrAddDCRule";
                this.ControlMethodParm = "('" + strVendor + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool blResult = false;
                    StringBuilder sbSql = new StringBuilder();
                    if (strType.Equals("Delete"))
                    {
                        sbSql.AppendFormat("WITH T AS(SELECT CTRLC1, value  Vedor FROM WHCTRL CROSS APPLY STRING_SPLIT(REMAK, ';')   WHERE CTRLID='D/C_ADMIN' AND CTRLNM='TransferRule' AND CTRLC1='{1}' ) UPDATE WHCTRL SET REMAK= (SELECT STRING_AGG(VEDOR,';') FROM T WHERE VEDOR <>'{0}') WHERE  CTRLID='D/C_ADMIN' AND CTRLNM='TransferRule' AND CTRLC1='{1}' ", strVendor, strDescriptionItem);
                    }
                    if(strType.Equals("Add"))
                    {
                        sbSql.AppendFormat("UPDATE WHCTRL SET REMAK= REMAK+';{0}' WHERE  CTRLID='D/C_ADMIN' AND CTRLNM='TransferRule' AND CTRLC1='{1}' ", strVendor, strDescriptionItem);
                    }
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
            # endregion

            # endregion
        }
    }
}
