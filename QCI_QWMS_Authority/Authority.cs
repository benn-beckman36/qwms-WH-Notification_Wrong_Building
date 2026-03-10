using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;

namespace QCI
{
    namespace QWMS
    {
        public class Authority : ControlBase
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



            #region Constructor
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
            public Authority(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
            {
                DataTable dtData = new DataTable();
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();
                string strSQL = "Select WKAUT,MAAUT,INAUT,OTAUT,CGAUT,IVAUT,MGAUT,REPLN,ISADM from WHUSR WITH(NOLOCK) where mandt= '" + UserData.Client + "' and comcd='" + UserData.CompanyCode + "' and usrnm = '" + UserData.UserId + "'";

                try
                {
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    //ControlHandleDB();
                    dtData = sqlAccess.GetDataTable(strSQL.ToString());
                    sqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {

                    throw new System.Exception(ex.Message + "<- Authority ");
                }

                if (dtData.Rows.Count > 0)
                {
                    WKAUT = dtData.Rows[0]["WKAUT"].ToString();
                    MAAUT = dtData.Rows[0]["MAAUT"].ToString();
                    INAUT = dtData.Rows[0]["INAUT"].ToString();
                    OTAUT = dtData.Rows[0]["OTAUT"].ToString();
                    CGAUT = dtData.Rows[0]["CGAUT"].ToString();
                    IVAUT = dtData.Rows[0]["IVAUT"].ToString();
                    MGAUT = dtData.Rows[0]["MGAUT"].ToString();
                    REPLN = dtData.Rows[0]["REPLN"].ToString();
                    ISADM = dtData.Rows[0]["ISADM"].ToString();
                }
                else
                {
                    WKAUT = "";
                    MAAUT = "";
                    INAUT = "";
                    OTAUT = "";
                    CGAUT = "";
                    IVAUT = "";
                    MGAUT = "";
                    REPLN = "";
                    ISADM = "N";
                }
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
            public Authority(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
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
            #endregion

            #region DataMember
            UserInfo UserData = new UserInfo();

            #endregion

            #region MemberFunction

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

            #region 查詢使用者資料
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  bool  bolReturn = objAuthority.QueryUserData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////

            public DataTable QueryUserData()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryUserData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    DataWhusr objWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("USRNM,WKAUT,INAUT,OTAUT,MAAUT,MGAUT,IVAUT");
                    alConditions.Add("Mandt='" + UserData.Client + "'");
                    alConditions.Add("Comcd='" + UserData.CompanyCode + "'");
                    //string strSql = objWhusr.EntityGetQuerySql(alColumns, alConditions, true, true);
                    dt = objWhusr.EntityQuery(alColumns, alConditions, true, true);

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
            #endregion
            #region 查詢使用者資料及權限
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者資料及權限
            /// </summary> 
            /// <param name="strUsrnm">帳號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strCrnam);
            ///  bool  bolReturn = objAuthority.QueryUserData(strUsrnm);
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
                    DataTable dt = new DataTable();
                    DataWhusr objWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("USRNM,PASWD");
                    alConditions.Add("Mandt='" + MANDT + "'");
                    alConditions.Add("Comcd='" + COMCD + "'");
                    alConditions.Add("Usrnm='" + UserData.UserId + "'");
                    //string strSql = objWhusr.EntityGetQuerySql(alColumns, alConditions, true, true);
                    dt = objWhusr.EntityQuery(alColumns, alConditions, true, true);
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
            #endregion
            #region 檢查使用者登入帳號密碼是否正確
            #region 檢查使用者登入帳號密碼是否正確  2引數
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者登入帳號密碼是否正確
            /// </summary> 
            /// <param name="strUsrnm">帳號。</param>
            /// <param name="strPaswd">密碼。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(Userdata);
            ///  bool  bolReturn = objAuthority.CheckLoginAuthority(strUsrnm,strPaswd);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool CheckLoginAuthority(string strUsrnm, string strPaswd)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('" + strUsrnm + "','" + strPaswd + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    DataWhusr objWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("*");
                    alConditions.Add("Mandt='" + MANDT + "'");
                    alConditions.Add("Comcd='" + COMCD + "'");
                    alConditions.Add("Usrnm='" + strUsrnm + "'");
                    alConditions.Add("Paswd='" + strPaswd + "'");
                    string strSql = objWhusr.EntityGetQuerySql(alColumns, alConditions, true, true);
                    dt = objWhusr.EntityQuery(alColumns, alConditions, true, true);

                    if (dt.Rows.Count > 0)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhusr.ResetField();
                        objWhusr.Logtm = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        alConditions.Add("Mandt='" + MANDT + "'");
                        alConditions.Add("Comcd='" + COMCD + "'");
                        alConditions.Add("Usrnm='" + strUsrnm + "'");

                        bool bolReturn = false;
                        try
                        {
                            string SstrTmp = objWhusr.EntityGetUpdateSql(alConditions);
                            bolReturn = objWhusr.EntityUpdate(alConditions);
                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- CheckLoginAuthority()";
                        }
                        return bolReturn;
                    }
                    else
                    {
                        return false;
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

            #region 檢查使用者登入帳號密碼是否正確 3引數
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者登入帳號密碼是否正確
            /// </summary> 
            /// <param name="strUsrnm">廠區</param>
            /// <param name="strPaswd">倉別</param>
            /// /// <param name="strUsrnm">儲位</param>            
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(Userdata);
            ///  bool  bolReturn = objAuthority.CheckLoginAuthority(strUsrnm,strPaswd);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>            
            public bool CheckLoginAuthority(string strWerks, string strLgort, string strLocat)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('" + strLgort + "','" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    string strTmp = "";
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add("*");
                    alConditions.Add("Mandt='" + MANDT + "'");

                    string strSQL = "";

                    if (strLgort == "")
                    {
                        //strSQL = "Select * from WHCTRL where MANDT= '" + UserData.Client + "' and SOLDTO='QWMS' and CTRLID='WERKS' and CTRLNM= '" + strWerks + "'";
                        alConditions.Add("SOLDTO='QWMS'");
                        alConditions.Add("CTRLID='WERKS'");
                        alConditions.Add("CTRLNM='" + strWerks + "'");
                        strTmp = objWhctrl.EntityGetQuerySql(alColumns, alConditions, true, true);
                        dt = objWhctrl.EntityQuery(alColumns, alConditions, true, true);

                    }
                    else
                    {
                        if (strLocat == "")
                        {
                            //strSQL = "Select * from WHCTRL where MANDT= '" + UserData.Client + "' and SOLDTO='QWMS' and CTRLID='LGORT' and CTRLNM = '" + strWerks + "' and CTRLC1 = '" + strLgort + "'";
                            alConditions.Add("SOLDTO='QWMS'");
                            alConditions.Add("CTRLID='LGORT'");
                            alConditions.Add("CTRLNM='" + strWerks + "'");
                            alConditions.Add("CTRLC1='" + strLgort + "'");
                            strTmp = objWhctrl.EntityGetQuerySql(alColumns, alConditions, true, true);
                            dt = objWhctrl.EntityQuery(alColumns, alConditions, true, true);
                        }
                        else
                        {
                            //strSQL = "Select * from WHHED where MANDT= '" + UserData.Client + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and LOCAT= '" + strLocat + "'";
                            alConditions.Add("WERKS='" + strWerks + "'");
                            alConditions.Add("LGORT='" + strLgort + "'");
                            alConditions.Add("LOCAT='" + strLocat + "'");
                            strTmp = objWhhed.EntityGetQuerySql(alColumns, alConditions, true, true);
                            dt = objWhhed.EntityQuery(alColumns, alConditions, true, true);

                        }
                    }

                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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
            #endregion
            #region 查詢使用者可以使用的廠區資料
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckPlantAuthority()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckPlantAuthority";
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
                    alColumns.Add(" CTRLC1 as F_VALUE ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='WERKS'");
                    alConditions.Add(" CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);


                    dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();

                    //sbSql.Append("Select CTRLNM as F_TEXT, CTRLC1 as F_VALUE from WHCTRL where  ");                   
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    //sbSql.AppendFormat("and CTRLID='WERKS' ");
                    //sbSql.AppendFormat("and CTRLNM in (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')  ");
                    //sbSql.AppendFormat("Order by CTRLNM");
                    //ControlHandleDB();
                    //dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

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
            public DataTable CheckSLCLgortAuthority()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckSLCLgortAuthority";
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
                    alColumns.Add(" CTRLNM as WERKS");
                    alColumns.Add(" CTRLC1 as LGORT");
                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='LGORT'");
                    alConditions.Add(" CTRLC3='BULK'");  // CTRLC3='BULK' 表示散料仓
                    //  alConditions.Add(" CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, true, true);


                    //dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();

                    //sbSql.Append("Select CTRLNM as F_TEXT, CTRLC1 as F_VALUE from WHCTRL where  ");                   
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    //sbSql.AppendFormat("and CTRLID='WERKS' ");
                    //sbSql.AppendFormat("and CTRLNM in (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')  ");
                    //sbSql.AppendFormat("Order by CTRLNM");
                    //ControlHandleDB();
                    //dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

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




            }  //散料仓 by blank
            public DataTable GetSLCLgortAuthority(string strWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckSLCLgortAuthority";
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
                    alColumns.Add(" CTRLC1 as F_VALUE ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='LGORT'");
                    alConditions.Add(" CTRLC3='BULK'");  // CTRLC3='BULK' 表示散料仓
                    alConditions.Add(" CTRLNM='" + strWerks + "'");
                    //  alConditions.Add(" CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);


                    dtResult = CommonInfo.SortDataTable(dtResult, "F_VALUE");

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();

                    //sbSql.Append("Select CTRLNM as F_TEXT, CTRLC1 as F_VALUE from WHCTRL where  ");                   
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    //sbSql.AppendFormat("and CTRLID='WERKS' ");
                    //sbSql.AppendFormat("and CTRLNM in (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')  ");
                    //sbSql.AppendFormat("Order by CTRLNM");
                    //ControlHandleDB();
                    //dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

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




            }  //散料仓 by blank
            public DataTable CheckGDPlantAuthority()  // BY blank  20150629
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckPlantAuthority";
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
                    alColumns.Add(" CTRLC1 as F_VALUE ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='GDCB'");
                    //  alConditions.Add(" CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
                    string STRTET = objWhctrl.EntityGetQuerySql(alColumns, alConditions, false, true);
                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);


                    dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();

                    //sbSql.Append("Select CTRLNM as F_TEXT, CTRLC1 as F_VALUE from WHCTRL where  ");                   
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    //sbSql.AppendFormat("and CTRLID='WERKS' ");
                    //sbSql.AppendFormat("and CTRLNM in (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')  ");
                    //sbSql.AppendFormat("Order by CTRLNM");
                    //ControlHandleDB();
                    //dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

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
            #region 查詢使用者可以使用的廠區資料
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckPlantWithoutAuthority(string strWerks)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckPlantWithoutAuthority";
                this.ControlMethodParm = "('" + strWerks + "')";
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
                    alColumns.Add(" CTRLC1 as F_VALUE ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='WERKS'");

                    if (!string.IsNullOrEmpty(strWerks))
                    {
                        alConditions.Add(" CTRLNM <> '" + strWerks + "'");
                    }

                    dtResult = objWhctrl.EntityQuery(alColumns, alConditions, false, true);

                    ////brian 20150410 
                    //string strSql = objWhctrl.EntityGetQuerySql(alColumns, alConditions, false, true);

                    dtResult = CommonInfo.SortDataTable(dtResult, "F_TEXT");

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();

                    //sbSql.Append("Select CTRLNM as F_TEXT, CTRLC1 as F_VALUE from WHCTRL where  ");                   
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    //sbSql.AppendFormat("and CTRLID='WERKS' ");
                    //sbSql.AppendFormat("and CTRLNM in (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')  ");
                    //sbSql.AppendFormat("Order by CTRLNM");
                    //ControlHandleDB();
                    //dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

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
            #region 查詢使用者可以使用的洲別資料
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的洲別資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  bool  bolReturn = objAuthority.CheckRegonAuthority();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckRegonAuthority()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select '' as F_TEXT,  '' as F_VALUE  UNION  ");
                    sbSql.AppendFormat("Select CTRLC1 as F_TEXT,  CTRLNM as F_VALUE from WHCTRL where ");
                    sbSql.AppendFormat("MANDT='{0}' ", "QCI");
                    sbSql.AppendFormat("AND SOLDTO='QWMS' ");
                    sbSql.AppendFormat("and CTRLID='REGON' ");

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
            #endregion
            #region 檢查使用者是否有使用某程式功能的權限
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用某程式功能的權限
            /// </summary> 
            /// <param name="strProgramID">程式ID。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  bool  bolReturn = objAuthority.CheckUseAuthority(strProgramID);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool CheckUseAuthority(string strProgramID)
            {
                string strAuthority = MAAUT + ";" + INAUT + ";" + OTAUT + ";" + CGAUT + ";" + IVAUT + ";" + MGAUT + ";" + REPLN;

                if (strAuthority.IndexOf(strProgramID) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            #endregion
            #region 更新使用者密碼(使用者自行修改密碼)
            ////////////Summary by Rock Tzeng ////////////////////////////////////////////
            /// <summary>
            /// 更新使用者密碼(使用者自行修改密碼)
            /// </summary> 
            /// <param name="strUsrnm">帳號。</param>
            /// <param name="strPaswd">密碼。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  bool  bolReturn = objAuthority.UpdateUserPassword(strUsrnm,strPaswd);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>			
            public bool UpdateUserPassword(string strPaswd)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateUserPassword";
                this.ControlMethodParm = "('" + strPaswd + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    DataWhusr objDataWhusr = new DataWhusr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    objDataWhusr.Paswd = strPaswd;
                    objDataWhusr.Monam = UserData.UserId;
                    objDataWhusr.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                    objDataWhusr.Paswddat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                    alCondition.Add("Mandt='" + MANDT + "'");
                    alCondition.Add("Comcd='" + COMCD + "'");
                    alCondition.Add("USRNM='" + UserData.UserId + "'");
                    string strSql = objDataWhusr.EntityGetUpdateSql(alCondition);
                    bool bolReturn = false;

                    try
                    {
                        bolReturn = objDataWhusr.EntityUpdate(alCondition);
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- UpdateUserPassword()";
                    }
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
            #endregion

            #region CheckLgortAuthority() 查詢使用者可以使用的廠區倉別資料

            #region 查詢使用者可以使用的廠區倉別資料_無引數
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區倉別資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  DataTable  dtData  = objAuthority.CheckLgortAuthority();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckLgortAuthority()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    return CheckLgortWithAuth("", "");
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 查詢使用者可以使用的廠區倉別資料,輸入(廠區)
            //=========================================================================
            ////////////Summary by Rock Tzeng ////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區倉別資料,輸入(廠區)
            /// </summary> 
            /// <param name="varWerks">廠區。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  DataTable  dtData = objAuthority.CheckLgortAuthority(strWerks);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckLgortAuthority(string varWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('" + varWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    return CheckLgortWithAuth(varWerks, "");

                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 查詢使用者可以使用的廠區倉別資料,輸入(廠區)及(倉別)
            ////////////Summary by Marc Hong ////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區倉別資料,輸入(廠區)及(倉別)
            /// </summary> 
            /// <param name="varWerks">廠區。</param>
            /// <param name="varLgort">倉別。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  DataTable  dtData = objAuthority.CheckLgortWithAuth(strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckLgortWithAuth(string varWerks, string varLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLgortWithAuth";
                this.ControlMethodParm = "('" + varWerks + "','" + varLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strFrom = "";

                    alColumns.Clear();
                    alColumns.Add(" WHAUT.LGORT as F_TEXT ");
                    alColumns.Add(" WHAUT.LGORT as F_VALUE ");
                    alColumns.Add(" WHCTRL.* ");

                    strFrom = " WHAUT inner join WHCTRL With(Nolock) on WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.SOLDTO='QWMS' and WHCTRL.CTRLID='LGORT' and WHCTRL.CTRLC2=WHAUT.LGORT and WHCTRL.CTRLNM=WHAUT.WERKS ";

                    alConditions.Add(" (WHAUT.MANDT='" + MANDT + "') ");
                    alConditions.Add(" (WHAUT.COMCD='" + COMCD + "') ");
                    alConditions.Add(" (WHAUT.USRNM='" + CRNAM + "') ");
                    if (varWerks != "")
                        alConditions.Add(" (WHAUT.WERKS='" + varWerks + "') ");

                    if (varLgort != "")
                        alConditions.Add(" (WHAUT.LGORT='" + varLgort + "') ");

                    //brian 20150217
                    string strSql = ControlGetQuerySql(strFrom, alColumns, alConditions, true);

                    dtResult = ControlQuery(strFrom, alColumns, alConditions, true);
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

            public DataTable CheckDCPlantAuthority()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckDCPlantAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                  
                   //厂区权利查询
                    sbSql.AppendFormat("SELECT  DISTINCT WHCTRL.CTRLNM as F_TEXT FROM WHAUT inner join WHCTRL With(Nolock) on WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.SOLDTO='QWMS' and WHCTRL.CTRLID='StorageIn_Type' AND WHCTRL.REMAK='Diff DACOD Diff Locat' and WHCTRL.CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
                 
                    
                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            public DataTable CheckDCWithAuth(string varWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckDCWithAuth";
                 this.ControlMethodParm = "('" + varWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    sbSql.AppendFormat("SELECT  DISTINCT WHCTRL.CTRLC1 as F_VALUE  FROM WHAUT inner join WHCTRL With(Nolock) on WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.SOLDTO='QWMS' and WHCTRL.CTRLID='StorageIn_Type' AND WHCTRL.REMAK='Diff DACOD Diff Locat' and WHCTRL.CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "') AND WHCTRL.CTRLNM='" + varWerks + "'");
                    
                    
                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 查询使用者可以使用的仓别
            public DataTable CheckLgortWithAuthSelect(string varWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLgortWithAuthSelect";
                this.ControlMethodParm = "('" + varWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strFrom = "";

                    alColumns.Clear();
                    alColumns.Add(" WHAUT.LGORT as LGORT ");

                    strFrom = " WHAUT inner join WHCTRL With(Nolock) on WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.SOLDTO='QWMS' and WHCTRL.CTRLID='LGORT' and WHCTRL.CTRLC2=WHAUT.LGORT and WHCTRL.CTRLNM=WHAUT.WERKS ";

                    alConditions.Add(" (WHAUT.MANDT='" + MANDT + "') ");
                    alConditions.Add(" (WHAUT.COMCD='" + COMCD + "') ");
                    alConditions.Add(" (WHAUT.USRNM='" + CRNAM + "') ");
                    alConditions.Add(" (WHAUT.WERKS='" + varWerks + "') ");

                    string strSql = ControlGetQuerySql(strFrom, alColumns, alConditions, true);

                    dtResult = ControlQuery(strFrom, alColumns, alConditions, true);
                    dtResult = CommonInfo.SortDataTable(dtResult, "LGORT");
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

            #region 查詢使用者可以使用的廠區倉別資料
            ////////////Summary by Rock Tzeng ////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的廠區倉別資料
            /// </summary> 
            /// <param name="varWerks">廠區。</param>
            /// <param name="varLgort">倉別。</param>
            /// <returns>
            /// boolean。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  bool  bolReturn = objAuthority.CheckLgortAuthority(strWerks, strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            public bool CheckLgortAuthority(string varWerks, string varLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLoginAuthority";
                this.ControlMethodParm = "('" + varWerks + "','" + varLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool bolResult = false;

                    try
                    {

                        DataTable dtAuthLgorts = CheckLgortWithAuth(varWerks, varLgort);
                        if (dtAuthLgorts.Rows.Count > 0)
                        {
                            bolResult = true;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- CheckLgortAuthority()";

                    }

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

             #endregion

            #region 查詢使用者可以使用的报表
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢使用者可以使用的报表
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  DataTable  dtData  = objAuthority.CheckLgortAuthority();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CheckReportAuthority(string varWerks)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckReportAuthority";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strFrom = "";

                    alColumns.Clear();
                    alColumns.Add(" WHCTRL.CTRLC5 as F_TEXT ");
                    alColumns.Add(" WHCTRL.CTRLC5 as F_VALUE ");
                    alColumns.Add(" WHCTRL.* ");

                    strFrom = " WHCTRL INNER JOIN WHAUT With(Nolock) ON WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.CTRLNM=WHAUT.WERKS ";
                    alConditions.Add(" (WHCTRL.SOLDTO='QWMS') ");
                    alConditions.Add(" (WHCTRL.CTRLC1='QWMS_Report') ");
                    alConditions.Add(" (WHAUT.MANDT='" + MANDT + "') ");
                    alConditions.Add(" (WHAUT.COMCD='" + COMCD + "') ");
                    alConditions.Add(" (WHAUT.USRNM='" + CRNAM + "') ");

                    if (varWerks != "")
                        alConditions.Add(" (WHAUT.WERKS='" + varWerks + "') ");

                    //brian 20150217
                    string strSql = ControlGetQuerySql(strFrom, alColumns, alConditions, true);

                    dtResult = ControlQuery(strFrom, alColumns, alConditions, true);
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

            #region 查詢报表收件人
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查詢报表收件人
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Authority objAuthority =new QCI.QWMS.Authority(strConnectionString, strMandt, strCrnam);
            ///  DataTable  dtData  = objAuthority.CheckLgortAuthority();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryAddressee(string varWerks, string varType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryAddressee";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtReturn = new DataTable();

                    string strFrom = "";

                    alColumns.Clear();
                    alColumns.Add(" REMAK2");

                    strFrom = " WHCTRL";
                    alConditions.Add(" (CTRLNM='" + varWerks + "') ");
                    alConditions.Add(" (CTRLC5=N'" + varType + "') ");

                    alConditions.Add(" (WHCTRL.MANDT='" + MANDT + "') ");
                    alConditions.Add(" (WHCTRL.COMCD='" + COMCD + "') ");
                    alConditions.Add(" (CTRLC1='QWMS_Report') ");

                    string strSql = ControlGetQuerySql(strFrom, alColumns, alConditions, true);
                    dtReturn = ControlSqlAccess.GetDataTable(strSql);
                    return dtReturn;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 查詢該倉別是否為散料倉
            //====================================================================================
            ////////////Summary by Smose Liao 20110905////////////////////////////////////////////
            /// <summary>
            /// 查詢該倉別是否為散料倉
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <returns>
            /// string。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
            ///  String strReturn = objSapData.QueryBulkStorage(string strWerks, string strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public string QueryBulkStorage(string strWerks, string strLgort)
            {
                DataWhctrl objCtrl = new DataWhctrl(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                string strReturn = "";

                alColumns.Clear();
                alConditions.Clear();
                alColumns.Add("CTRLC3");//若有放值'BULK'，代表該倉別為散料倉
                alConditions.Add("(SOLDTO= 'QWMS')");
                alConditions.Add("(CTRLID= 'LGORT')");
                alConditions.Add("(CTRLNM= '" + strWerks + "')");
                alConditions.Add("(CTRLC1= '" + strLgort + "')");
                sbSql.Append(objCtrl.EntityGetQuerySql(alColumns, alConditions, false, false));

                try
                {
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    strReturn = sqlAccess.GetFieldValue(sbSql.ToString());
                    sqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryBulkStorage()";
                }

                return strReturn;
            }
            #endregion

            #region Add Pad User
            /// <summary>
            /// Add Pad User
            /// </summary> 

            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>

            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public bool AddPadUser(string strWerks, string strLine, string strAccount)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDataExistsInStorageGR";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flage = false;
                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("INSERT INTO PADUSR(MANDT,WERKS,ARBPL,USRNM,CRENM,COMCD,CRDAT)VALUES('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE())", MANDT, strWerks, strLine, strAccount, UserData.UserId, UserData.CompanyCode);

                try
                {
                    flage = ControlSqlAccess.ExecSql(strsql.ToString().Trim());
                    return flage;
                }

                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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
            #region 删除PadUser
            public bool DeletePadUser(string strWerks, string strAccount, string strLine)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeletePadUser";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flage = false;
                StringBuilder strsql = new StringBuilder();
                strsql.AppendFormat("delete from PADUSR where WERKS='{0}' and  USRNM='{1}' and ARBPL='{2}'", strWerks, strAccount, strLine);

                try
                {
                    flage = ControlSqlAccess.ExecSql(strsql.ToString().Trim());
                    return flage;
                }

                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 查询pad user
            //=========================================================================
            /// <summary>
            /// 查询pad user
            /// </summary> 
            /// <param name="strWerks">儲位。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryPadUser(string Werks, string Account, string Line)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLocatInsmk";
                this.ControlMethodParm = "('" + Werks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat("SELECT * FROM PADUSR WHERE 1=1 ");
                    if (Werks != "")
                    {
                        strsql.AppendFormat(" AND WERKS='{0}'", Werks);
                    }
                    if (Account != "")
                    {
                        strsql.AppendFormat(" AND USRNM='{0}'", Account);
                    }
                    if (Line != "")
                    {
                        strsql.AppendFormat(" AND ARBPL='{0}'", Line);
                    }

                    dt = ControlSqlAccess.GetDataTable(strsql.ToString().Trim());
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
            #endregion

            #region 查询qwms user
            //=========================================================================
            /// <summary>
            /// 查询pad user
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryqwmsUser(string Account)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryqwmsUser";
                //this.ControlMethodParm = "('" + Werks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dt = new DataTable();
                try
                {

                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat("SELECT USRNM,PASWD FROM WHUSR WITH(NOLOCK) WHERE 1=1 ");
                    if (Account != "")
                    {
                        strsql.AppendFormat(" AND USRNM='{0}'", Account);
                    }
                    //ControlHandleDB();
                    SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlAccess.GetDataTable(strsql.ToString().Trim());
                    sqlAccess.CloseConnection();

                }
                catch (CommonObjectsException ex)
                {
                    ////讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    //ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    //ControlExceptionType = ex.SourceExceptionType;
                    //this.ControlPriority = "1";
                    //ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    //throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex)
                {
                    //ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    //ControlExceptionType = ex.GetType().FullName;
                    //this.ControlPriority = "1";
                    //ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    //throw new Exception("999");
                }
                return dt;

            }
            #endregion

            #region 查询已做发料确认的信息
            public DataTable QueryItem_Confirm(string Werks, string Lgort, string DocuNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryItem_Confirm";
                this.ControlMethodParm = "('" + Werks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("SELECT * FROM PADWN WHERE FLAGE='Y' ");
                    if (Werks != "" || Lgort != null)
                    {
                        strSql.AppendFormat(" AND WERKS ='{0}' ", Werks);
                    }
                    if (Lgort != "" || Lgort != null)
                    {
                        strSql.AppendFormat(" AND LGORT='{0}' ", Lgort);
                    }
                    if (DocuNo != "" || Lgort != null)
                    {
                        strSql.AppendFormat(" AND MBLNR ='{0}' ", DocuNo);
                    }
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString().Trim());
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

            #region 解绑已做发料确认的信息
            public bool UnbindItem_Confirm(string Werks, string Lgort, string DocuNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UnbindItem_Confirm";
                this.ControlMethodParm = "('" + Werks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool flag = false;
                    string strSql = " UPDATE PADWN SET FLAGE='N' WHERE WERKS='" + Werks + "' AND LGORT='" + Lgort + "' AND MBLNR='" + DocuNo + "'";
                    flag = ControlSqlAccess.ExecSql(strSql);
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

            #region 查询发料进度
            public DataTable QueryIssueProgress(string userId, string carNo, string docuNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryIssueProgress";
                this.ControlMethodParm = "('" + userId + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat(" SELECT * FROM PADWN  WHERE 1=1 ");
                    if (!string.IsNullOrEmpty(userId))
                    {
                        strSql.AppendFormat(" AND USNAM ='{0}' ", userId);
                    }
                    if (!string.IsNullOrEmpty(carNo))
                    {
                        strSql.AppendFormat(" AND CARNO ='{0}' ", carNo);
                    }
                    if (!string.IsNullOrEmpty(docuNo))
                    {
                        strSql.AppendFormat(" AND MBLNR ='{0}' ", docuNo);
                    }
                    dtResult = ControlSqlAccess.GetDataTable(strSql.ToString().Trim());
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

            #region 查询盘点进度
            public DataTable QueryCountingPrpgress(string strUserID, string strMatnr, string strDateFrom, string strDateTo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCountingPrpgress";
                this.ControlMethodParm = "('" + strUserID + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat(" SELECT * FROM WHCYC WHERE 1=1 ");
                    strSQL.AppendFormat(" AND CKDAT BETWEEN '{0}' AND '{1}'", strDateFrom, strDateTo);
                    if (strUserID != "")
                    {
                        strSQL.AppendFormat(" AND CRNAM='{0}' ", strUserID);
                    }
                    if (strMatnr != "")
                    {
                        strSQL.AppendFormat(" AND MATNR='{0}' ", strMatnr);
                    }
                    dtResult = ControlSqlAccess.GetDataTable(strSQL.ToString().Trim());
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

            #region 车辆信息查询
            public DataTable QueryCarMaintain(string strWerks, string strCarno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCarMaintain";
                this.ControlMethodParm = "('" + strCarno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat(" SELECT * FROM PADCAR WHERE 1=1 ");
                    if (strWerks != "")
                    {
                        strSQL.AppendFormat(" AND WERKS='{0}' ", strWerks);
                    }
                    if (strCarno != "")
                    {
                        strSQL.AppendFormat(" AND CARNO='{0}' ", strCarno);
                    }
                    dtResult = ControlSqlAccess.GetDataTable(strSQL.ToString().Trim());
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

            #region 车辆信息维护
            public bool CarMaintain(string strWerks, string strCarno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CarMaintain";
                this.ControlMethodParm = "('" + strCarno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool flag = false;
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat(" INSERT INTO PADCAR (WERKS,CARNO,FLAGE) VALUES ('{0}','{1}','Y') ", strWerks, strCarno);
                    flag = ControlSqlAccess.ExecSql(strSQL.ToString().Trim());
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

            #region 查询备料信息
            public DataTable QueryMaterialPrepare(string strWerks, string strLgort, string strCarno, string strDocuNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMaterialPrepare";
                this.ControlMethodParm = "('" + strCarno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat(" SELECT * FROM PADWN WHERE 1=1 ");
                    if (strWerks != "")
                    {
                        strSQL.AppendFormat(" AND WERKS='{0}' ", strWerks);
                    }
                    if (strLgort != "")
                    {
                        strSQL.AppendFormat(" AND LGORT='{0}' ", strLgort);
                    }
                    if (strCarno != "")
                    {
                        strSQL.AppendFormat(" AND CARNO='{0}' ", strCarno);
                    }
                    if (strDocuNo != "")
                    {
                        strSQL.AppendFormat(" AND MBLNR='{0}' ", strDocuNo);
                    }
                    dtResult = ControlSqlAccess.GetDataTable(strSQL.ToString().Trim());
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

            #region 获取当前使用和可以使用的厂区、仓别信息
            /// <summary>
            /// 获取当前使用和可以使用的厂区、仓别信息
            /// <remarks>by Chiris 2017年12月28日15:49:51</remarks>
            /// </summary>
            /// <returns></returns>
            public DataTable GetWerksAndLgorts()
            {
                DataTable dtWerksAndLgorts = new DataTable();
                StringBuilder sbSQL = new StringBuilder();

                sbSQL.Append("SELECT VIEWTABLE.WERKS + ',' + STUFF(VIEWTABLE.LGORT, LEN(VIEWTABLE.LGORT), 1, '') AS WERKSandLGORTS FROM")
                    .AppendLine()
                    .Append("(")
                    .AppendLine()
                    .Append("	SELECT BASICTABLE.WERKS,")
                    .AppendLine()
                    .Append("	(")
                    .AppendLine()
                    .Append("	SELECT LGORT+',' FROM")
                    .AppendLine()
                    .Append("		(")
                    .AppendLine()
                    .Append("		SELECT DISTINCT WHCTRL.CTRLNM AS WERKS, WHCTRL.CTRLC1 AS LGORT")
                    .AppendLine()
                    .Append("		FROM WHAUT WITH(NOLOCK) INNER JOIN WHCTRL WITH(NOLOCK)")
                    .AppendLine()
                    .Append("		ON WHAUT.MANDT = WHCTRL.MANDT AND WHAUT.COMCD = WHCTRL.COMCD AND WHCTRL.CTRLC2 = WHAUT.LGORT AND WHCTRL.CTRLNM = WHAUT.WERKS")
                    .AppendLine()
                    .AppendFormat("		WHERE WHAUT.MANDT = '{0}' AND WHAUT.COMCD = '{1}'", MANDT, COMCD)
                    .AppendLine()
                    .AppendFormat("			  AND WHCTRL.SOLDTO = 'QWMS' AND WHCTRL.CTRLID = 'LGORT' AND WHAUT.USRNM = '{0}'", UserData.UserId)
                    .AppendLine()
                    .AppendFormat("			  AND CTRLNM IN (SELECT DISTINCT WERKS FROM WHAUT WHERE MANDT = '{0}' AND COMCD = '{1}' AND USRNM = '{2}')", MANDT, COMCD, UserData.UserId)
                    .AppendLine()
                    .Append("			  AND WHCTRL.CTRLNM = BASICTABLE.WERKS")
                    .AppendLine()
                    .Append("		) AS XMLTABLE FOR XML PATH('')")
                    .AppendLine()
                    .Append("	) AS LGORT")
                    .AppendLine()
                    .Append("	FROM (")
                    .AppendLine()
                    .Append("			SELECT DISTINCT WHCTRL.CTRLNM AS WERKS, WHCTRL.CTRLC1 AS LGORT")
                    .AppendLine()
                    .Append("			FROM WHAUT WITH(NOLOCK) INNER JOIN WHCTRL WITH(NOLOCK)")
                    .AppendLine()
                    .Append("			ON WHAUT.MANDT = WHCTRL.MANDT AND WHAUT.COMCD = WHCTRL.COMCD AND WHCTRL.CTRLC2 = WHAUT.LGORT AND WHCTRL.CTRLNM = WHAUT.WERKS")
                    .AppendLine()
                    .AppendFormat("			WHERE WHAUT.MANDT = '{0}' AND WHAUT.COMCD = '{1}'", MANDT, COMCD)
                    .AppendLine()
                    .AppendFormat("				  AND WHCTRL.SOLDTO = 'QWMS' AND WHCTRL.CTRLID = 'LGORT' AND WHAUT.USRNM = '{0}'", UserData.UserId)
                    .AppendLine()
                    .AppendFormat("				  AND CTRLNM IN (SELECT DISTINCT WERKS FROM WHAUT WHERE MANDT = '{0}' AND COMCD='{1}' AND USRNM = '{2}')", MANDT, COMCD, UserData.UserId)
                    .AppendLine()
                    .Append("		  ) AS BASICTABLE")
                    .AppendLine()
                    .Append("	GROUP BY BASICTABLE.WERKS")
                    .AppendLine()
                    .Append(") AS VIEWTABLE");
                SqlAccess sqlAccess = null;
                try
                {
                    //ControlHandleDB();
                    sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    sqlAccess.OpenConnection();
                    dtWerksAndLgorts = sqlAccess.GetDataTable(sbSQL.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString() + "GetWerksAndLgorts()");
                }
                finally
                {
                    sbSQL.Length = 0;
                    sqlAccess.CloseConnection();
                }

                return dtWerksAndLgorts;

            }
            #endregion

            #region 查詢盤點前置作業的數據 BY廠區&倉別 ADD BY CLAUD.GE

            public DataTable CheckLgortPreProgresses(string varWerks, ArrayList Lgorts, string varGuid)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLgortPreProgresses";
                this.ControlMethodParm = "('" + varWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();


                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql = strSql.AppendFormat("SELECT  * FROM Inventory_Precesses WHERE WERKS = '{0}'", varWerks);
                    strSql = strSql.AppendFormat("and Guid='{0}' ", varGuid); //add by Jason 20200720
                    for (int i = 0; i < Lgorts.Count; i++)
                    {
                        strSql = i == 0 ? strSql.Append(" and LGORT in ('") : strSql.Append("");
                        strSql = strSql.AppendFormat("{0}", Lgorts[i].ToString());
                        strSql = i < Lgorts.Count - 1 ? strSql.Append("','") : strSql.Append("') ");
                    }
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

            #region 页面卡控厂区，不允许调出
            public DataTable CheckWerksSelectable(string strWerks)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckWerksSelectable";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.AppendFormat("SELECT CTRLNM AS WERKS FROM WHCTRL WHERE REMAK='Not Selectable' AND CTRLNM='{0}'", strWerks);


                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 卡控仓别查询
            public string CheckTransLgort(string strLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckWerksSelectable";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    string strUMLGO = "";

                    sbSql.AppendFormat("SELECT CTRLC2 AS UMLGO FROM WHCTRL WITH(NOLOCK) WHERE MANDT='{0}' AND COMCD='{1}' AND SOLDTO='QWMS' AND CTRLID='TRANS' AND CTRLNM='351' AND CTRLC1 LIKE '%{2}%' ", MANDT, COMCD, strLgort);

                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dtData.Rows.Count > 0)
                    {
                        string[] strList = dtData.Rows[0]["UMLGO"].ToString().Split(';');
                        foreach (string str in strList)
                        {
                            strUMLGO = strUMLGO + "'" + str + "',";
                        }
                        strUMLGO = strUMLGO.Substring(0, strUMLGO.Length - 1);
                    }

                    return strUMLGO;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 页面卡控厂区，不允许调出
            public bool CheckLgortExist(string strWerks, string strLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckWerksSelectable";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    bool isExist;

                    sbSql.AppendFormat("SELECT CTRLC1 AS LGORT FROM WHCTRL WITH(NOLOCK) WHERE CTRLID='LGORT' AND CTRLNM='{0}' AND CTRLC1 IN ({1})", strWerks, strLgort);

                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dtData.Rows.Count == 0)
                    {
                        isExist = false;
                    }
                    else
                    {
                        isExist = true;
                    }
                    return isExist;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 页面卡控厂区，不允许调入
            public DataTable CheckWerksTransIn(string Kostl)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckWerksTransIn";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.AppendFormat("SELECT CTRLNM AS WERKS FROM WHCTRL WHERE REMAK='Not TransIn' AND CTRLNM='{0}'", Kostl);


                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            public string GetApiRequestUrl()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAGVRequestUrl";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strRequestUrl = string.Empty;
                try
                {
                    string strSQL = "SELECT CTRLC1 FROM WHCTRL WHERE CTRLID='QWMSApi' ";
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


            #region QPC—IQC判票打印临时使用
            public DataTable QPC_IQC_TempUse(string Id)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QPC_IQC_TempUse";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtResult = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    
                    sbSql.AppendFormat("select ctrlc1 from whctrl with(nolock) where ctrlid='Authority' and ctrlnm='{0}'", Id);


                    ControlHandleDB();
                    DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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
        }
    }
}
