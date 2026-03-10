using Qci.Base.Common;
using QCI.QWMS;
using QWMS.Common;
using QWMS.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QCI_QWMS_Alim
{
    public class AlimStorageQuery : ControlBase
    {
        #region 变量
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

        #region 构造函数
        #region 不傳參數的建構式(不用)
            public AlimStorageQuery()
            {
            }
            #endregion

        #region 傳入UserData當參數的建構式
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
           public AlimStorageQuery(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
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
           public AlimStorageQuery(UserInfo varUserData, string strProgid)
               : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
            }

            #endregion

        #region 傳入varUserData，strWerks及strLgort當參數的建構式
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
           public AlimStorageQuery(UserInfo varUserData, string strWerks, string strLgort)
               : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
           {
           }

           #endregion
       
        #region 利用傳入參數產生DBType,DBCode,ErrType,ErrCode及varUserData當參數的建構式
        /// <summary>
            /// 利用傳入參數產生PlantData物件。
            /// </summary>
            /// <param name="varDBType">DB Type。</param>
            /// <param name="varDBCode">DB Code。</param>
            /// <param name="varErrorType">Error Type。</param>
            /// <param name="varErrorCode">Error Code。</param>
            /// <example>
            /// <code>
            ///  PlantData objPlantData = new PlantData(1, "TEST", 2, "ERR");
            ///  Your Code Here......
            /// </code>
            /// </example>
        public AlimStorageQuery(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
            {
                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                CRNAM = varUserData.UserId;
                Authority objAuthority = new Authority(UserData);
                objAuthority = new Authority(UserData);
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

        public AlimStorageQuery(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
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

        public AlimStorageQuery(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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

        #endregion

        #region MemberFunction


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

        #endregion

        #region 查询使用哪个电子仓权限(varWerks)
        /// <summary>
        /// 查询使用哪个电子仓权限
        /// </summary>
        /// <param name="varWerks">厂区</param>
        /// <returns></returns>
        public DataTable CheckLgortAuthority(string varWerks)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckLoginAuthority";
            this.ControlMethodParm = "('" + varWerks + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLNM='{0}' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE WERKS='{0}' AND USRNM='{1}') ORDER BY CTRLC1 ", varWerks, UserData.UserId);


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

        #region 查询使用哪个电子仓权限
        /// <summary>
        /// 查询使用哪个电子仓权限
        /// </summary>
        /// <returns></returns>
        public DataTable CheckLgortAuthority()
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckLoginAuthority";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE  USRNM='{0}') ORDER BY CTRLC1 ", UserData.UserId);


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

        #region 庫位下拉選單資料 無引數
        //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 庫位下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlInsmk()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlInsmk";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alQueryCondition = new ArrayList();

                    DataTable dtData = new DataTable();

                    alColumns.Add("CTRLC1 as F_TEXT");
                    alColumns.Add("CTRLNM as F_VALUE");
                    alColumns.Add("CTRLN1");

                    alQueryCondition.Add("MANDT = 'QCI'");
                    alQueryCondition.Add("SOLDTO = 'QWMS'");
                    alQueryCondition.Add("CTRLID = 'INSMK'");
                    alQueryCondition.Add("CTRLNM IN ('','0','G')");

                    dtData = objWhctrl.EntityQuery(alColumns, alQueryCondition, false, true);

                    dtData = CommonInfo.SortDataTable(dtData, "CTRLN1");

                    //StringBuilder sbSql = new StringBuilder();
                    //sbSql.Append("Select CTRLC1 as F_TEXT, CTRLNM as F_VALUE from WHCTRL where   ");
                    //sbSql.AppendFormat("MANDT='{0}' ", "QCI");
                    //sbSql.AppendFormat("and SOLDTO='{0}' ", "QWMS");
                    //sbSql.AppendFormat("and CTRLID='{0}' ", "INSMK");
                    //sbSql.AppendFormat("order by CTRLN1 ");


                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
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

        # region 检查使用者是否有使用电子仓作业相关的权限
            //=========================================================================
            ////////////Summary by Galen Chen////////////////////////////////////////////
            /// <summary>
            /// 检查使用者是否有使用电子仓作业相关的权限
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
            public bool CheckAuthority(string strType)
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
                    if (objAuthority.CGAUT.IndexOf(PROGID) < 0)
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

        #region 查詢倉別庫存資料
            //=========================================================================
            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 查詢倉別庫存資料, 將相同料號的資料Group by在一起strInsmk: 庫別strStartMatnr: 開始料號strEndMatnr: 結束料號
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strStartMatnr">開始料號。</param>
            /// <param name="strEndMatnr">結束料號。</param>
            /// <param name="strMaktx">料號說明。</param>
            /// <param name="strLoadID">LoadID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objStorageData.QueryAlitmStorageData(strInsmk,strStartMatnr,strEndMatnr, strMaktx, strLoadID);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryAlitmStorageData(string strInsmk, string strStartMatnr, string strEndMatnr, string strMaktx, string strSerno, string strItem, string strStoc)
            {
                //偞隅猁??Error Message腔眈燊?洘息
                this.ControlMethodName = "QueryAlitmStorageData";
                this.ControlMethodParm = "('" + strInsmk + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strMaktx + "','" + strSerno  + "')";
                if (ControlTraceCode == "T") //猁??網請森WebMethod腔log...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtData = new DataTable();

                    if (strMaktx == "")
                    {
                        //strSQL = "Select MANDT, WERKS, LGORT, MATNR, KDMAT, INSMK, sum(MENGE) as MENGE, sum(REQTY) as REQTY, count(*) as TOTAL, MIN(INDAT) as INDAT, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK) as TOLOC, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK and MRGID<>'') as MILOC from WHITM where MANDT= '" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' and LGORT= '" + LGORT + "'  ";
                        sbSql.Append("Select MANDT, WERKS, LGORT, ALITM.MATNR, KDMAT, WHPAT.MAKTX AS MAKTX, INSMK, sum(MENGE) as MENGE, sum(REQTY) as REQTY, count(*) as TOTAL, MIN(INDAT) as INDAT, (select count(distinct LOCAT) from ALITM as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.MATNR=ALITM.MATNR and A.INSMK=ALITM.INSMK) as TOLOC, (select count(distinct LOCAT) from ALITM  as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.MATNR=ALITM.MATNR and A.INSMK=ALITM.INSMK and MRGID<>'') as MILOC")
                            .AppendFormat(" from ALITM With(Nolock) left join WHPAT on ALITM.MATNR=WHPAT.MATNR where MANDT= '{0}' and COMCD = '{1}' and WERKS = '{2}' and LGORT= '{3}'", MANDT, COMCD, WERKS, LGORT);
                    }
                    
                    else
                    {
                        //strSQL = "Select WHITM.MANDT, WHITM.WERKS, WHITM.LGORT, WHITM.MATNR, WHITM.KDMAT, WHITM.INSMK, sum(WHITM.MENGE) as MENGE, sum(WHITM.REQTY) as REQTY, count(*) as TOTAL, MIN(WHITM.INDAT) as INDAT, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK) as TOLOC, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK and MRGID<>'') as MILOC from WHITM inner join WHPAT on WHITM.MATNR=WHPAT.MATNR where WHITM.MANDT= '" + MANDT + "' and WHITM.WERKS = '" + WERKS + "' and WHITM.LGORT= '" + LGORT + "'  ";
                        sbSql.Append("Select ALITM.MANDT, ALITM.WERKS, ALITM.LGORT, ALITM.MATNR, ALITM.KDMAT, WHPAT.MAKTX AS MAKTX,ALITM.INSMK, sum(ALITM.MENGE) as MENGE, sum(ALITM.REQTY) as REQTY, count(*) as TOTAL, MIN(ALITM.INDAT) as INDAT, (select count(distinct LOCAT) from ALITM as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.MATNR=ALITM.MATNR and A.INSMK=ALITM.INSMK) as TOLOC, (select count(distinct LOCAT) from ALITM as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.MATNR=ALITM.MATNR and A.INSMK=ALITM.INSMK and MRGID<>'') as MILOC ")
                            .AppendFormat(" from ALITM With(Nolock) left join WHPAT on ALITM.MATNR=WHPAT.MATNR where ALITM.MANDT= '{0}' and ALITM.COMCD='{1}' and ALITM.WERKS = '{2}' and ALITM.LGORT= '{3}'", MANDT, COMCD, WERKS, LGORT);
                    }

                    if (strInsmk != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.INSMK = '{0}'", strInsmk);
                    }

                    if (strItem != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.ITEMSTATES = '{0}'", strItem);
                    }

                    if (strStoc != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.STOCSTATES = '{0}'", strStoc);
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        //strSQL += " and ((WHITM.MATNR like '" + strStartMatnr + "%') or (WHITM.MATNR between '" + strStartMatnr + "%' and '" + strEndMatnr + "%'))";

                        sbSql.AppendFormat(" and ((ALITM.MATNR like '{0}%') or (ALITM.MATNR between '{1}%' and '{2}%'))", strStartMatnr, strStartMatnr, strEndMatnr);
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        // strSQL += " and WHITM.MATNR like '" + strEndMatnr + "%'";
                        sbSql.AppendFormat(" and ALITM.MATNR like '{0}%'", strEndMatnr);
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        //strSQL += " and WHITM.MATNR like '" + strStartMatnr + "%'";
                        sbSql.AppendFormat(" and ALITM.MATNR like '{0}%'", strStartMatnr);
                    }

                    if (strMaktx != "")
                    {
                        // strSQL += " and WHPAT.MAKTX like '%" + strMaktx + "%'";
                        sbSql.AppendFormat(" and WHPAT.MAKTX like '%{0}%'", strMaktx);
                    }

                    if (strSerno != "")
                    {
                        sbSql.AppendFormat(" and ALITM.SERNO = '{0}'", strSerno);
                    }



                    //strSQL += " group by WHITM.MANDT, WHITM.WERKS, WHITM.LGORT, WHITM.MATNR, WHITM.KDMAT, WHITM.INSMK ";
                    sbSql.Append(" group by ALITM.MANDT, ALITM.COMCD, ALITM.WERKS, ALITM.LGORT, ALITM.MATNR, ALITM.KDMAT, ALITM.INSMK,MAKTX ");
                    
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
                }
                catch (CommonObjectsException ex)
                {
                    //莮Common Object腔淩淏嶒悷?洘ㄛ瞰ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//褫赻俴崝樓猁handle腔Exception  
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

        #region 查詢倉別庫存資料 以储位
            //=========================================================================
            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 查詢倉別庫存資料, 將相同料號的資料Group by在一起strInsmk: 庫別strStartMatnr: 開始料號strEndMatnr: 結束料號
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strStartMatnr">開始料號。</param>
            /// <param name="strEndMatnr">結束料號。</param>
            /// <param name="strMaktx">料號說明。</param>
            /// <param name="strLoadID">LoadID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objStorageData.QueryAlitmStorageData(strInsmk,strStartMatnr,strEndMatnr, strMaktx, strLoadID);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryAlitmStorageData_CW(string strWerks,string strLgort,string strInsmk, string strItem, string strStoc,string strLocat)
            {
                //偞隅猁??Error Message腔眈燊?洘息
                this.ControlMethodName = "QueryAlitmStorageData_CW";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strInsmk + "','" + strItem + "','" + strStoc + "')";
                if (ControlTraceCode == "T") //猁??網請森WebMethod腔log...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtData = new DataTable();

                   
                   
                    //strSQL = "Select WHITM.MANDT, WHITM.WERKS, WHITM.LGORT, WHITM.MATNR, WHITM.KDMAT, WHITM.INSMK, sum(WHITM.MENGE) as MENGE, sum(WHITM.REQTY) as REQTY, count(*) as TOTAL, MIN(WHITM.INDAT) as INDAT, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK) as TOLOC, (select count(distinct LOCAT) from WHITM as A where A.MANDT=WHITM.MANDT and A.WERKS=WHITM.WERKS and A.LGORT=WHITM.LGORT and A.MATNR=WHITM.MATNR and A.INSMK=WHITM.INSMK and MRGID<>'') as MILOC from WHITM inner join WHPAT on WHITM.MATNR=WHPAT.MATNR where WHITM.MANDT= '" + MANDT + "' and WHITM.WERKS = '" + WERKS + "' and WHITM.LGORT= '" + LGORT + "'  ";
                    sbSql.Append("Select ALITM.MANDT, ALITM.WERKS, ALITM.LGORT, ALITM.LOCAT, ALITM.KDMAT,ALITM.INSMK, sum(ALITM.MENGE) as MENGE, sum(ALITM.REQTY) as REQTY, count(*) as TOTAL, MIN(ALITM.INDAT) as INDAT, (select count(distinct MATNR) from ALITM as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.LOCAT=ALITM.LOCAT and A.INSMK=ALITM.INSMK) as TOLOC, (select count(distinct MATNR) from ALITM as A With(Nolock) where A.MANDT=ALITM.MANDT and A.COMCD=ALITM.COMCD and A.WERKS=ALITM.WERKS and A.LGORT=ALITM.LGORT and A.LOCAT=ALITM.LOCAT and A.INSMK=ALITM.INSMK and MRGID<>'') as MILOC ")
                          .AppendFormat(" from ALITM With(Nolock)  where ALITM.MANDT= '{0}' and ALITM.COMCD='{1}' and ALITM.WERKS = '{2}' and ALITM.LGORT= '{3}'", MANDT, COMCD, strWerks, strLgort);
                  

                    if (strInsmk != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.INSMK = '{0}'", strInsmk);
                    }

                    if (strItem != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.ITEMSTATES = '{0}'", strItem);
                    }

                    if (strStoc != "")
                    {
                        //strSQL += " and WHITM.INSMK = '" + strInsmk + "'";
                        sbSql.AppendFormat(" and ALITM.STOCSTATES = '{0}'", strStoc);
                    }

                    if (strLocat != "")
                    {
                        sbSql.AppendFormat(" and ALITM.LOCAT = '{0}'", strLocat);
                    }



                    //strSQL += " group by WHITM.MANDT, WHITM.WERKS, WHITM.LGORT, WHITM.MATNR, WHITM.KDMAT, WHITM.INSMK ";
                    sbSql.Append(" group by ALITM.MANDT, ALITM.COMCD, ALITM.WERKS, ALITM.LGORT, ALITM.KDMAT, ALITM.INSMK,ALITM.LOCAT ");

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
                }
                catch (CommonObjectsException ex)
                {
                    //莮Common Object腔淩淏嶒悷?洘ㄛ瞰ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//褫赻俴崝樓猁handle腔Exception  
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

        #region 查詢儲位的庫存明細資料 by Insmk,Matnr
            //=========================================================================
            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 查詢儲位的庫存明細資料strInsmk: 庫別strMatnr: 料號
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strMatnr">料號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objStorageData.QueryStorageData(strInsmk,strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryStorageData(string strInsmk, string strMatnr,string strLocat, string strSerno, string strMblnr)
            {

                this.ControlMethodName = "QueryStorageData";
                this.ControlMethodParm = "('" + strInsmk + "','" + strMatnr + "','" + strSerno + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //猁??網請森WebMethod腔log...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();

                    //string strSQL = "Select * from WHITM where MANDT= '" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' and LGORT = '" + LGORT + "' and INSMK= '" + strInsmk + "' and MATNR = '" + strMatnr + "'";
                    //DataWhitm objWhitm = new DataWhitm(UserData);
                    //ArrayList alColumns = new ArrayList();
                    //ArrayList alCondition = new ArrayList();
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("SELECT * FROM ALITM WHERE ");
                    strsql.AppendFormat("MANDT='{0}' ",MANDT);
                    strsql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    strsql.AppendFormat("AND WERKS='{0}' ", WERKS);
                    strsql.AppendFormat("AND LGORT='{0}' ", LGORT);
                    strsql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                    if (strMatnr != "")
                    {
                        strsql.AppendFormat("AND MATNR='{0}' ", strMatnr);
                    }

                    if (strLocat != "")
                    {
                        strsql.AppendFormat("AND LOCAT='{0}' ", strLocat);
                    }
                   

                    //alColumns.Add("*");
                    //alCondition.Clear();
                    //alCondition.Add("MANDT='" + MANDT + "'");
                    //alCondition.Add("COMCD='" + COMCD + "'");
                    //alCondition.Add("WERKS = '" + WERKS + "'");
                    //alCondition.Add("LGORT = '" + LGORT + "'");
                    //alCondition.Add("INSMK= '" + strInsmk + "'");
                    //alCondition.Add("MATNR = '" + strMatnr + "'");
                    if (strSerno != "")
                    {
                        //alCondition.Add("SERNO = '" + strSerno + "'");
                        strsql.AppendFormat("AND SERNO='{0}' ", strSerno);
                    }
                    if (strMblnr != "")
                    {
                        //alCondition.Add("MBLNR = '" + strMblnr + "'");
                        strsql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                    }
                    //dtData = objWhitm.EntityQuery(alColumns, alCondition, false, true);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strsql.ToString());
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

        #region 查詢儲位的庫存明細資料 by Insmk,Matnr
            //=========================================================================
            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 查詢儲位的庫存明細資料strInsmk: 庫別strMatnr: 料號
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strMatnr">料號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objStorageData.QueryStorageData(strInsmk,strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryStorageDatawhbox(string strInsmk, string strMatnr, string strSerno, string strMblnr)
            {

                this.ControlMethodName = "QueryStorageData";
                this.ControlMethodParm = "('" + strInsmk + "','" + strMatnr + "','" + strSerno + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //猁??網請森WebMethod腔log...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();

                    //string strSQL = "Select * from WHITM where MANDT= '" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' and LGORT = '" + LGORT + "' and INSMK= '" + strInsmk + "' and MATNR = '" + strMatnr + "'";
                    //DataWhbox objWhitm = new DataWhbox(UserData);
                    //ArrayList alColumns = new ArrayList();
                    //ArrayList alCondition = new ArrayList();
                    StringBuilder strsql = new StringBuilder();
                    strsql.Append("SELECT * FROM ALITM WHERE ");
                    strsql.AppendFormat("MANDT='{0}' ", MANDT);
                    strsql.AppendFormat("AND COMCD='{0}' ", COMCD);
                    strsql.AppendFormat("AND WERKS='{0}' ", WERKS);
                    strsql.AppendFormat("AND LGORT='{0}' ", LGORT);
                    strsql.AppendFormat("AND INSMK='{0}' ", strInsmk);
                    strsql.AppendFormat("AND MATNR='{0}' ", strMatnr);

                    //alColumns.Add("*");

                    //alCondition.Clear();
                    //alCondition.Add("MANDT='" + MANDT + "'");
                    //alCondition.Add("COMCD='" + COMCD + "'");
                    //alCondition.Add("WERKS = '" + WERKS + "'");
                    //alCondition.Add("LGORT = '" + LGORT + "'");
                    //alCondition.Add("INSMK= '" + strInsmk + "'");
                    //alCondition.Add("MATNR = '" + strMatnr + "'");
                    if (strSerno != "")
                    {
                        //alCondition.Add("SERNO = '" + strSerno + "'");
                        strsql.AppendFormat("AND SERNO='{0}' ", strSerno);
                    }
                    if (strMblnr != "")
                    {
                        //alCondition.Add("MBLNR = '" + strMblnr + "'");
                        strsql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                    }
                    //dtData = objWhitm.EntityQuery(alColumns, alCondition, false, true);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strsql.ToString());
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

        #region 查詢料號
            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 查詢料號
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strType">類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.QueryPartData(string strWerks, string strLgort, strMatnr, strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryPartData(string strWerks, string strLgort, string strMatnr, string strType)
            {
                DataWhpat objWhpat = new DataWhpat(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alQueryCondition = new ArrayList();

                alColumns.Add("*");

                if (strMatnr.Trim() != "")
                {
                    alQueryCondition.Add("MATNR Like '" + strMatnr + "%'");
                }
                if (strType == "INVENTORY")
                {
                    alQueryCondition.Add("MATNR in (Select MATNR from ALITM where MANDT='" + this.MANDT + "' and WERKS='" + strWerks.Trim() + "' and LGORT='" + strLgort.Trim() + "')");
                }
                if (strType == "LOG")
                {
                    alQueryCondition.Add("MATNR in (Select MATNR from WHLOG where MANDT='" + this.MANDT + "' and WERKS='" + strWerks.Trim() + "' and LGORT='" + strLgort.Trim() + "' and Convert(varchar(8), CRDAT, 112) = '" + System.DateTime.Now.ToString("yyyyMMdd") + "')");
                }

                DataTable dtData = new DataTable();
                try
                {
                    dtData = objWhpat.EntityQuery(alColumns, alQueryCondition, false, true);
                    dtData = CommonInfo.SortDataTable(dtData, "MATNR");
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryPartData()";
                }

                return dtData;
            }
            #endregion

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

        #region 执行SAP&QWMS库存比对
            //========================================================================================
            /// <summary>
            /// 执行SAP&QWMS库存比对   
            /// </summary> 
            /// 
            public DataTable ExcuteComparation(string strXML, string strWerk, string strLgorts, out string strErr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ExcuteComparation";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();
                try
                {

                    strErr = string.Empty;
                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 0;
                    sbSQL.Append("  DECLARE @return  varchar(100) ");
                    sbSQL.AppendLine(" EXEC  [dbo].[SP_ExecuteComparation_SAP_QWMS] '" + strXML + "','" + MANDT + "','" + COMCD + "','" + strWerk + "','" + strLgorts.Replace("'", "''") + "',@return out  ");
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

        #region DataTable转化成xml
            /// <summary>
            /// DataTable转化成xml
            /// </summary>
            /// <param name="xmlDS"></param>
            /// <returns></returns>
            public string ConvertDataTableToXML(DataTable dtData)
            {
                MemoryStream stream = null;
                XmlTextWriter writer = null;
                try
                {
                    stream = new MemoryStream();
                    writer = new XmlTextWriter(stream, Encoding.Default);
                    dtData.WriteXml(writer);
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
         #endregion

        
    }
}

        
