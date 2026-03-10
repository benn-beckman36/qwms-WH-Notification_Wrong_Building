using Qci.Base.Common;
using QWMS.Common;
using QWMS.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCI.QWMS
{
    public class Inventory_AGV:ControlBase
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
        public Inventory_AGV()
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
        public Inventory_AGV(UserInfo varUserData, string varProgid)
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
        public Inventory_AGV(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
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
        public Inventory_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string strProgid)
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
        public Inventory_AGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
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
                if (objAuthority.REPLN.IndexOf(PROGID) < 0)
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


        #region 创建AGV指令到WHAGV
        /// <summary>
        /// 创建AGV指令到WHAGV表
        /// </summary>
        /// <param name="strWerks">厂区</param>
        /// <param name="strLgort">仓别</param>
        /// <param name="strWorkStation">工作站</param>
        /// <param name="strTaskID">任务编号</param>
        /// <param name="strReqNo">任务序号</param>
        /// <param name="strShelfNo">货架编号</param>
        /// <param name="strState">货架状态0,1</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool InsertWHAGV(string strWerks, string strLgort, string strWorkStation, string strTaskID, int strReqNo, string strShelfNo, string strState)
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
                sbSql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','','{8}',GETDATE(),'{9}') ", MANDT, strWerks, strLgort, strWorkStation, strTaskID, strReqNo, strShelfNo, strState, UserData.UserId, strComcd);
                arySQL.Add(sbSql.ToString());
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

        #region 查询该储位以料号版本分组的sum(menge)
        /// <summary>
        /// 查询该储位以料号版本分组的sum(menge)
        /// </summary>
        /// <param name="strLocat">储位</param>
        /// <returns></returns>
        public DataTable QueryLocatITM(string strLocat)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLocatITM";
            this.ControlMethodParm = "('" + strLocat + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                #region Code here
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT LOCAT,MATNR,CHARG,DACOD,SUM(MENGE) AS MENGE,'' AS SCQTY, '' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                strSQL.AppendFormat(" AND SUBSTRING(LOCAT,1,8)='" + strLocat + "'");
                strSQL.AppendFormat("GROUP BY LOCAT,MATNR,CHARG,DACOD");
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

        public DataTable getLocat(string strWerks, string strLgort, string strInvNo)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "getLocal";
            this.ControlMethodParm = "('" + strInvNo + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                #region Code here
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT DISTINCT SUBSTRING(LOCAT,1,8) LOCAT FROM IVITM WITH(NOLOCK) WHERE");
                strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                strSQL.AppendFormat(" AND WERKS='" + strWerks + "'");
                strSQL.AppendFormat(" AND LGORT='" + strLgort + "'");
                strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
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

        #region 储位盘点明细查询
        public DataTable getInventoryLocatDetail(string strLocat, bool blCharg, bool blDacod)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "getInventoryLocatDetail";
            this.ControlMethodParm = "('" + strLocat + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                #region Code here
                StringBuilder strSQL = new StringBuilder();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objDataWhitm = new DataWhitm(UserData);

                alColumns.Clear();
                //alColumns.Add("LOCAT");
                alColumns.Add("SUBSTRING(LOCAT,1,8) AS LOCAT");
                alColumns.Add("MATNR");
                alColumns.Add("SUM(MENGE) AS MENGE");
                alColumns.Add("'' AS SCDAC");
                alColumns.Add("'' AS SCQTY");
                alColumns.Add("'' AS MGDIF");
                alColumns.Add("'' AS STATS");
                alColumns.Add("'' AS RMARK");

                alConditions.Clear();
                alConditions.Add("MANDT='" + MANDT + "'");
                alConditions.Add("COMCD='" + COMCD + "'");
                alConditions.Add("WERKS='" + WERKS + "'");
                alConditions.Add("LGORT='" + LGORT + "'");
                //alConditions.Add("SUBSTRING(LOCAT,1,8)='" + strLocat + "'");
                if (blCharg && blDacod)
                {
                    alColumns.Add("CHARG");
                    alColumns.Add("DACOD");
                    alConditions.Add("SUBSTRING(LOCAT,1,8)='" + strLocat + "' GROUP BY SUBSTRING(LOCAT,1,8),MATNR,CHARG,DACOD");
                }
                if (blCharg && !blDacod)
                {
                    alColumns.Add("CHARG");
                    alColumns.Add("'' AS DACOD");
                    alConditions.Add("SUBSTRING(LOCAT,1,8)='" + strLocat + "' GROUP BY MATNR,SUBSTRING(LOCAT,1,8),CHARG");
                }
                if (!blCharg && blDacod)
                {
                    alColumns.Add("DACOD");
                    alConditions.Add("SUBSTRING(LOCAT,1,8)='" + strLocat + "' GROUP BY SUBSTRING(LOCAT,1,8),MATNR,DACOD");
                }
                if (!blCharg && !blDacod)
                {
                    alConditions.Add("SUBSTRING(LOCAT,1,8)='" + strLocat + "' GROUP BY MATNR,SUBSTRING(LOCAT,1,8)");
                }
                DataTable dtData = new DataTable();
                ControlHandleDB();
                string str = objDataWhitm.EntityGetQuerySql(alColumns, alConditions, true);
                dtData = objDataWhitm.EntityQuery(alColumns, alConditions, true);
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

        public DataTable DIDMenge(string strLocat)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "DIDMenge";
            this.ControlMethodParm = "('" + strLocat + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                #region Code here
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT LOCAT,MATNR,MBLNR,SUM(MENGE) AS didMENGE FROM WHITM WITH(NOLOCK) WHERE");
                strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                strSQL.AppendFormat(" AND SUBSTRING(LOCAT,1,8)='" + strLocat + "'");
                strSQL.AppendFormat("GROUP BY LOCAT,MATNR,MBLNR");
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

        #region 二次扫描 查询更改后的DACOD
        public string SelectDetail(string strMatnr, string strLocat)
        {
            string strSQL = "";
            string strDacod = "";
            try
            {
                strSQL = "SELECT DACOD FROM WHITM WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "'AND WERKS='" + WERKS + "'AND LGORT='" + LGORT + "'AND SUBSTRING(LOCAT,1,8)='" + strLocat + "'  AND MATNR='" + strMatnr + "'";
                ControlHandleDB();
                strDacod = ControlSqlAccess.GetFieldValue(strSQL);
                ControlSqlAccess.CloseConnection();
                return strDacod;
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

        public DataTable getLocatByMatnr(string strMatnr)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "getLocatByMatnr";
            this.ControlMethodParm = "('" + strMatnr + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                #region Code here
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("SELECT LOCAT,SUBSTRING(LOCAT,1,8) ShowLocat, MENGE FROM WHITM WITH(NOLOCK) WHERE");
                strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                strSQL.AppendFormat(" AND MATNR='" + strMatnr + "'");
                //strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
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

        public bool InventoryLoad(int type, string strInvNo, string setsLocat, DataTable dtData, string strType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "InventoryLoad";
            this.ControlMethodParm = "('" + type + "','" + strInvNo + "','" + setsLocat + "','" + dtData + "','" + strType + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataWhcyc objDataWhcyc = new DataWhcyc(UserData);
                DataTable dtCloneIVITM = new DataTable();
                DataTable dtCopyIVITM = new DataTable();
                DataTable dtCRNT = new DataTable();
                DataTable dtEndtmUpdate = new DataTable();
                DataTable dtStatmUpdate = new DataTable();
                bool bolReturn = false;//标记SQL是否执行成功，返回值
                bool flag = false;//标记SQL是否执行成功
                string CopyIVITMSQL;
                string deleteSQL;
                string crSQL;
                string endtmSQL;
                string updateIvitmSQL;
                string updateEndtm;
                ControlHandleDB();
                if (setsLocat != "")
                {
                    crSQL =
                       string.Format(@"SELECT CRTIM,CRWHO FROM IVHED WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND WERKS='{2}' AND LGORT='{3}'AND INVNO='{4}'", MANDT, COMCD, WERKS, LGORT, strInvNo);
                    dtCRNT = ControlSqlAccess.GetDataTable(crSQL.ToString());
                    if (type == 0)
                    {
                        deleteSQL =
                            string.Format(@"DELETE FROM IVITM WHERE MANDT='{0}'AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{5}' AND SUBSTRING(LOCAT,1,8) = '{4}'", MANDT, COMCD, WERKS, LGORT, setsLocat, strInvNo);
                        flag = ControlSqlAccess.ExecSql(deleteSQL);
                        if (flag)
                        {
                            CopyIVITMSQL =
                                string.Format(@"SELECT TOP 1 MANDT,COMCD,WERKS,LGORT,LOCAT,INVNO,MATNR,CHARG,MENGE,MGDIF,RSDIF,STATS,LEADR,CFTIM,RMARK,CRTIM,CRWHO FROM IVITM WITH (NOLOCK)");
                            dtCopyIVITM = ControlSqlAccess.GetDataTable(CopyIVITMSQL);
                            dtCloneIVITM = dtCopyIVITM.Clone();
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                DataRow dr = dtCloneIVITM.NewRow();
                                dr["MANDT"] = MANDT;
                                dr["COMCD"] = COMCD;
                                dr["WERKS"] = WERKS;
                                dr["LGORT"] = LGORT;
                                dr["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                                dr["INVNO"] = strInvNo;
                                dr["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                                if (strType == "BoxID" || strType == "Sn")
                                {
                                    dr["CHARG"] = dtData.Rows[i]["CHARG"].ToString(); ;
                                }
                                else
                                {
                                    dr["CHARG"] = "";
                                }
                                dr["MENGE"] = Convert.ToInt32(dtData.Rows[i]["MENGE"]);
                                dr["MGDIF"] = Convert.ToInt32(dtData.Rows[i]["MGDIF"]);
                                dr["STATS"] = dtData.Rows[i]["STATS"].ToString();
                                dr["RMARK"] = dtData.Rows[i]["RMARK"].ToString();
                                dr["CFTIM"] = DateTime.Now.ToLocalTime().ToString();
                                dr["CRTIM"] = dtCRNT.Rows[0]["CRTIM"].ToString();
                                dr["CRWHO"] = UserData.UserId;
                                dtCloneIVITM.Rows.Add(dr);
                            }
                            bolReturn = ControlSqlAccess.ExecSqlBulkCopy("IVITM", dtCloneIVITM);
                        }
                    }
                    else
                    {
                        updateIvitmSQL =
                      string.Format(@"UPDATE IVITM SET STATS='Y',CFTIM='{6}',CRWHO='{7}',CRTIM='{8}',MENGE=0,MGDIF=0 WHERE MANDT='{0}' AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{4}' AND LOCAT='{5}'", MANDT, COMCD, WERKS, LGORT, strInvNo, setsLocat, DateTime.Now.ToLocalTime().ToString(), UserData.UserId, dtCRNT.Rows[0]["CRTIM"].ToString());
                        bolReturn = ControlSqlAccess.ExecSql(updateIvitmSQL);
                    }
                    endtmSQL =
                        string.Format(@"SELECT DISTINCT(LOCAT) FROM IVITM WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{4}' AND STATS='N'", MANDT, COMCD, WERKS, LGORT, strInvNo);
                    dtEndtmUpdate = ControlSqlAccess.GetDataTable(endtmSQL.ToString());
                    if (dtEndtmUpdate.Rows.Count == 0)
                    {
                        updateEndtm =
                       string.Format(@"UPDATE IVHED SET ENDTM=(SELECT MAX(CFTIM) FROM IVITM WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{4}'),STATS='Y' WHERE MANDT='{0}'AND COMCD='{1}'AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{4}'", MANDT, COMCD, WERKS, LGORT, strInvNo);
                        ControlSqlAccess.ExecSql(updateEndtm);
                    }

                    ControlSqlAccess.CloseConnection();
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
    }
}
