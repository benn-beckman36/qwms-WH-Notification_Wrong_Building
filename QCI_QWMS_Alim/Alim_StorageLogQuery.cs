using Qci.Base.Common;
using QCI.QWMS;
using QWMS.Common;
using QWMS.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCI_QWMS_Alim
{
    public class Alim_StorageLogQuery : ControlBase
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

        #endregion
        #region 构造函数
            #region 不傳參數的建構式(不用)
            public Alim_StorageLogQuery()
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
            public Alim_StorageLogQuery(UserInfo varUserData, string varProgid)
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
            public Alim_StorageLogQuery(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
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
        public Alim_StorageLogQuery(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.StorageIn";
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
                //QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(varUserData);

                dtTemp = GetPlantStorageData("LGORT", WERKS, LGORT);//objPlantData.
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }


            }
            #endregion
            #region sapdata构造函数
        public Alim_StorageLogQuery(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
            {
            }
        public Alim_StorageLogQuery(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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
                ControlErrorInfo.ObjectName = "QWMS.SapData";
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
        #region Method
        #region 取得廠區或倉別資料
        //=========================================================================
        ////////////Summary by Rock Tzeng////////////////////////////////////////////
        /// <summary>
        /// 取得廠區或倉別資料
        /// </summary> 
        /// <param name="strType">類型。</param>
        /// <param name="strWerks">廠區。</param>
        /// <param name="strLgort">倉別。</param>
        /// <returns>
        /////////////////////////////////////////////////////////////////////////////	
        public DataTable GetPlantStorageData(string strType, string strWerks, string strLgort)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetPlantStorageData";
            this.ControlMethodParm = "('" + strType + "','" + strWerks + "','" + strLgort + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                string strFrom = "";

                if (strType.ToUpper() == "WERKS")
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    alColumns.Add(" * ");
                    strFrom = " WHCTRL With(Nolock) ";
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" SOLDTO='QWMS'");
                    alConditions.Add(" CTRLID='WERKS'");

                    dtData = ControlQuery(strFrom, alColumns, alConditions, true);
                    dtData = CommonInfo.SortDataTable(dtData, "CTRLNM");
                }
                else if (strType.ToUpper() == "LGORT")
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    alColumns.Add(" WHCTRL.MANDT ");
                    alColumns.Add(" WHCTRL.COMCD ");
                    alColumns.Add(" WHCTRL.CTRLNM ");
                    alColumns.Add(" WHCTRL.CTRLC2 ");
                    alColumns.Add(" WHCTRL.REMAK ");
                    alColumns.Add(" isnull(WHCTRL.CTRLC4,'') as CTRLC4 ");
                    alColumns.Add(" isnull(WHCTRL.CTRLC5,'') as CTRLC5 ");
                    alColumns.Add(" WHSTG.STSET ");
                    alColumns.Add(" WHSTG.STHGH ");
                    alColumns.Add(" WHSTG.STLEN ");
                    alColumns.Add(" WHSTG.STWID ");
                    alColumns.Add(" WHSTG.CRNAM ");
                    alColumns.Add(" WHSTG.CRDAT ");

                    strFrom = " WHCTRL left outer join WHSTG With(Nolock) on WHCTRL.MANDT=WHSTG.MANDT AND WHCTRL.COMCD=WHSTG.COMCD  and WHCTRL.CTRLNM=WHSTG.WERKS and WHCTRL.CTRLC1=WHSTG.LGORT ";

                    alConditions.Add(" (WHCTRL.MANDT='" + MANDT + "') ");
                    alConditions.Add(" (WHCTRL.COMCD='" + COMCD + "') ");
                    alConditions.Add(" (WHCTRL.SOLDTO='QWMS') ");
                    alConditions.Add(" (WHCTRL.CTRLID='LGORT') ");

                    if (strWerks == "")
                    {

                    }
                    else
                    {
                        if (strLgort != "")
                        {
                            alConditions.Add(" (WHCTRL.CTRLNM='" + strWerks + "') ");
                            alConditions.Add(" (CTRLC2='" + strLgort + "') ");
                        }
                        else
                        {
                            alConditions.Add(" (WHCTRL.CTRLNM='" + strWerks + "') ");
                        }
                    }

                    dtData = ControlQuery(strFrom, alColumns, alConditions, true);
                    dtData = CommonInfo.SortDataTable(dtData, "CTRLNM, CTRLC2");
                }


                try
                {

                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- GetPlantStorageData()";
                }
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
        #region 檢查使用者是否有使用管理作業功能的權限
        ////=========================================================================
        //////////////Summary by Donald Chen////////////////////////////////////////////
        ///// <summary>
        ///// 檢查使用者是否有使用管理作業功能的權限
        ///// </summary> 
        ///// <param name="strType">參數。</param>
        ///// <returns>
        ///// bool。
        ///// </returns>
        ///// <example>
        ///// <code>
        ///// <remarks>
        /////  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
        /////  bool  bolReturn = objStorageIn.CheckAuthority(strType);
        /////  Your Code Here......
        ///// </remarks>
        ///// </code>
        ///// </example>
        ///////////////////////////////////////////////////////////////////////////////	
        //public bool CheckAuthority(string strType)
        //{
        //    Authority objAuthority = new Authority(UserData);
        //    if (strType.ToUpper() == "ALIM")
        //    {
        //        if (objAuthority.CGAUT.IndexOf(PROGID) < 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    else
        //    {
        //        if (objAuthority.INAUT.IndexOf(PROGID) < 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //}
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
                alColumns.Add("distinct CTRLNM as F_TEXT ");

                alConditions.Clear();
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" SOLDTO='QWMS'");
                alConditions.Add(" CTRLID='LGORT'");
                alConditions.Add(" CTRLC3='BULK'");  // CTRLC3='BULK' 表示散料仓
                //  alConditions.Add(" CTRLNM IN (Select WERKS from WHAUT where MANDT='" + MANDT + "' and COMCD='" + COMCD + "' and USRNM='" + CRNAM + "')");
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
            sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 AS F_TEXT FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLNM='{0}' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE WERKS='{0}' AND USRNM='{1}') ORDER BY CTRLC1 ", varWerks, UserData.UserId);


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
            sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 AS F_TEXT FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE  USRNM='{0}') ORDER BY CTRLC1 ", UserData.UserId);


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
        #region GetDdlCgcls()
        //=========================================================================
        ////////////Summary by Donald Chen////////////////////////////////////////////
        /// <summary>
        /// 異動類別下拉選單資料
        /// </summary> 
        /// <returns>
        /// DataTable。
        /// </returns>
        /// <example>
        /// <code>
        /// <remarks>
        ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
        ///  DataTable dtData = objPlantData.GetDdlCgcls();
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////	
        public DataTable GetDdlCgcls()
        {
            DataWhctrl objWhctrl = new DataWhctrl(UserData);

            ArrayList alColumns = new ArrayList();
            ArrayList alQueryCondition = new ArrayList();

            alColumns.Add("CTRLC1 as F_TEXT");
            alColumns.Add("CTRLNM as F_VALUE");
            alColumns.Add("CTRLN1");

            alQueryCondition.Add("MANDT = 'QCI'");
            alQueryCondition.Add("SOLDTO = 'QWMS'");
            alQueryCondition.Add("CTRLID = 'CGCLS'");
            alQueryCondition.Add("");
            alQueryCondition.Add("");
            alQueryCondition.Add("");

            DataTable dtData = new DataTable();

            try
            {
                dtData = objWhctrl.EntityQuery(alColumns, alQueryCondition, false, true);
                dtData = CommonInfo.SortDataTable(dtData, "CTRLN1");
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- GetDdlCgcls()";
            }
            return dtData;

        }
        #endregion
        #region 查询log
        public DataTable QueryLogData(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat, string strStartMblnr, string strEndMblnr, string strQueryTable, string DC)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData";
            this.ControlMethodParm = "('" + strCgcls + "','" + strInsmk + "','" + strCharg + "','" + strUsrnm + "','" + strStartDate + "','" + strEndDate + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMblnr + "','" + strEndMblnr + "','" + strQueryTable + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" Select G.CRDAT , G.CGCLS , G.TRNTP , G.WERKS , G.LGORT , G.CRDAT , G.OLOCA , G.MATNR , G.INSMK , G.CHARG , G.EBELN , G.KDMAT , G.SERNO , G.BOXID , G.DACOD , G.MENGE , G.MBLNR , G.RMANO , G.OMBLN , G.LIFNR , G.INDAT , G.CRNAM , G.KOSTL , G.ARBPL , G.LOCOD , G.GRPID,P.MAKTX  ");
                strSql.AppendFormat(" FROM " + strQueryTable + " AS G WITH(NOLOCK)  left join WHPAT AS P WITH(NOLOCK) ON G.MATNR=P.MATNR Where G.MANDT = '" + UserData.Client + "' And G.COMCD = '" + UserData.CompanyCode + "' And G.WERKS = '" + WERKS + "' And G.LGORT = '" + LGORT + "' ");

                if (strCgcls != "")
                {
                    strSql.AppendFormat(" And G.CGCLS = '" + strCgcls + "' ");
                    //alQueryCondition.Add("CGCLS = '" + strCgcls + "'");
                }

                if (strInsmk != "")
                {
                    strSql.AppendFormat(" And G.INSMK = '" + strInsmk + "' ");
                    //alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                }

                if (strCharg != "")
                {
                    strSql.AppendFormat(" And G.CHARG = '" + strCharg + "' ");
                    //alQueryCondition.Add("CHARG = '" + strCharg + "'");
                }

                if (strUsrnm != "")
                {
                    strSql.AppendFormat(" And G.CRNAM = '" + strUsrnm.ToString().Trim() + "' ");
                    //alQueryCondition.Add("CRNAM = '" + strUsrnm.ToString().Trim() + "'");
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    strSql.AppendFormat(" And (G.CRDAT between '" + strStartDate + "' and '" + strEndDate + "') ");
                    //alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    //alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                    strSql.AppendFormat(" And G.CRDAT = '" + strEndDate + "' ");
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    strSql.AppendFormat(" And G.CRDAT = '" + strStartDate + "' ");
                    //alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    strSql.AppendFormat(" And (G.MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "') ");
                    //alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    strSql.AppendFormat("  And G.MATNR = '" + strEndMatnr + "' ");
                    //alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    strSql.AppendFormat(" And G.MATNR = '" + strStartMatnr + "' ");
                    //alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                }

                if (strStartLocat != "" && strEndLocat != "")
                {
                    strSql.AppendFormat(" And ((G.OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (G.NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "')) ");
                    //alQueryCondition.Add("((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                }
                else if (strStartLocat == "" && strEndLocat != "")
                {
                    strSql.AppendFormat(" And (G.OLOCA = '" + strEndLocat + "' or G.NLOCA = '" + strEndLocat + "') ");
                    //alQueryCondition.Add("(OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')");
                }
                else if (strStartLocat != "" && strEndLocat == "")
                {
                    strSql.AppendFormat(" And (G.OLOCA = '" + strStartLocat + "' or G.NLOCA = '" + strStartLocat + "') ");
                    //alQueryCondition.Add("(OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')");
                }

                //20070419 for document No. marc add
                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    strSql.AppendFormat(" And (G.MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "') ");
                    //alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    strSql.AppendFormat(" And G.MBLNR like '" + strEndMblnr + "%' ");
                    //alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    strSql.AppendFormat(" And G.MBLNR like '" + strStartMblnr + "%' ");
                    //alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                }

                if (DC != "")
                    strSql.AppendFormat(" And G.DACOD ='" + DC + "' ");
                //alQueryCondition.Add("DACOD ='" + DC + "'");

                DataTable dtData = new DataTable();
                try
                {
                    //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
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
        #region 查詢線別選項清單 by Ryan Tsai 20131126
        public DataTable QueryLineList(string strWerks)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLineList";
            this.ControlMethodParm = "(" + strWerks + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                StringBuilder sbSql = new StringBuilder();
                DataTable dtData = new DataTable();

                //sbSql.AppendFormat("select * from WHCTRL with(nolock) where CTRLID='OUTRPT' and CTRLNM='{0}'", strWerksLgort);
                sbSql.Append(" select ARBPL as F_TEXT, ARBPL as F_VALUE from WHPRI with(nolock) ");

                if (!string.IsNullOrEmpty(strWerks))
                {
                    sbSql.AppendFormat(" where WERKS='{0}' ", strWerks);
                }

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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
        #region 查詢SendID選項清單 by Ryan Tsai 20131126
        public DataTable QuerySendIDList(string strStartDate, string strEndDate, string strStartTime, string strEndTime, string strWerks)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QuerySendIDList";
            this.ControlMethodParm = "(" + strStartDate + "," + strEndDate + "," + strStartTime + "," + strEndTime + "," + strWerks + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                StringBuilder sbSql = new StringBuilder();
                DataTable dtData = new DataTable();
                string strNowDate = DateTime.Now.ToString("yyyyMMdd");
                string strNowTime = DateTime.Now.ToString("dd");

                sbSql.Append("select distinct REFID AS F_TEXT from WHDWN with(nolock) where BWART='261' and REFID!='' and MTYPE='SAP' and OTQTY !=0 ");

                #region where condition-SendID日期
                if (!string.IsNullOrEmpty(strStartDate) && string.IsNullOrEmpty(strEndDate))
                {
                    //若僅輸入起始日期,無輸入結束日期
                    sbSql.AppendFormat(" and BUDAT between '{0}' and '{1}' ", strStartDate, strNowDate);
                }
                else if (!string.IsNullOrEmpty(strEndDate) && string.IsNullOrEmpty(strStartDate))
                {
                    //若僅輸入結束日期,無輸入起始日期
                    sbSql.AppendFormat(" and BUDAT <= '{0}' ", strEndDate);

                }
                else if (!string.IsNullOrEmpty(strStartDate) && !string.IsNullOrEmpty(strEndDate))
                {
                    //若皆有輸入起始日期,有輸入結束日期
                    sbSql.AppendFormat(" and BUDAT >= '{0}' ", strStartDate);
                }
                else
                {
                }
                #endregion
                #region where condition-SendID時間
                if (!string.IsNullOrEmpty(strStartTime) && string.IsNullOrEmpty(strEndTime))
                {
                    //有輸入起始時間,無輸入結束時間
                    sbSql.AppendFormat("and PRITY >= '{0}'  ", strStartTime);
                }
                else if (!string.IsNullOrEmpty(strEndTime) && string.IsNullOrEmpty(strStartTime))
                {
                    //有輸入結束時間,無輸入起始時間
                    sbSql.AppendFormat("and PRITY <= '{0}'  ", strEndTime);
                }
                else if (!string.IsNullOrEmpty(strEndTime) && !string.IsNullOrEmpty(strStartTime))
                {
                    //有輸入起始時間,有輸入結束時間
                    sbSql.AppendFormat("and PRITY between '{0}' and '{1}' ", strStartTime, strEndTime);
                }
                else
                {
                }
                #endregion
                #region where condition-廠區
                if (!string.IsNullOrEmpty(strWerks))
                {
                    sbSql.AppendFormat("and WERKS='{0}'", strWerks);
                }
                #endregion

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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
        #region QueryLogData_AGV   (查詢異動紀錄 for AGV Kitting flow)
        ////////////Summary by Ryan Tsai (20131127)//////////////////////////////////
        /// <summary>
        /// 若於異動紀錄功能,輸入AGV Kitting相關查詢條件,查詢相關異動資料
        /// </summary> 
        /////////////////////////////////////////////////////////////////////////////	
        public DataTable QueryLogData_AGV(string strCgcls, string strInsmk, string strCharg, string strUsrnm, string strStartDate,
                                        string strEndDate, string strStartMatnr, string strEndMatnr, string strStartLocat, string strEndLocat,
                                        string strStartMblnr, string strEndMblnr, string strQueryTable,
                                        string strAGVLine, string strAGVSendID, string strAGVMATNM, string strAGVStartDate, string strAGVEndDate,
                                        string strAGVStartTime, string strAGVEndTime, string strBWART)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData_AGV";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append(" select PRI.ARBPL AS LINE, MAN.MATNM, DWN.REFID AS SENDID, DWN.PRITY, DWN.BUDAT, ");
                sbSql.Append(" substring(DWN.PRITY,1, 2)+':'+substring(DWN.PRITY,3, 2) AS SendIDTime, ");
                sbSql.Append(" LOG.CRDAT, LOG.CGCLS, LOG.TRNTP, LOG.WERKS, ");
                sbSql.Append(" LOG.LGORT, LOG.OLOCA, LOG.MATNR, LOG.INSMK, LOG.CHARG, ");
                sbSql.Append(" LOG.EBELN, LOG.KDMAT, LOG.SERNO, LOG.BOXID, LOG.DACOD, ");
                sbSql.Append(" LOG.MENGE, LOG.MBLNR, LOG.OMBLN, LOG.LIFNR, LOG.INDAT, ");
                sbSql.Append(" LOG.CRNAM, LOG.KOSTL, LOG.ARBPL, LOG.LOCOD, LOG.INSPT, LOG.GRPID ");

                sbSql.Append(" from WHLOG AS LOG with(nolock) ");
                sbSql.Append(" left join WHDWN AS DWN with(nolock) ");      //left join WHDWN
                sbSql.Append(" on LOG.MBLNR=DWN.MBLNR ");

                sbSql.Append(" left join WHMAN AS MAN with(nolock) ");      //left join WHMAN (料號品名)
                sbSql.Append(" on LOG.MATNR=MAN.MATNR ");

                sbSql.Append(" left join WHPRI AS PRI with(nolock) ");     //left join WHPRI (線別順序)
                sbSql.Append(" on CharIndex(PRI.ARBPL,substring(DWN.ARBPL,1,5))>0 and PRI.WERKS=DWN.WERKS AND PRI.ARBPL=DWN.ARBPL  ");

                sbSql.Append(" where 1=1 ");


                #region where condition- General
                sbSql.AppendFormat(" and LOG.MANDT='{0}' ", UserData.Client);
                sbSql.AppendFormat(" and LOG.COMCD='{0}' ", UserData.CompanyCode);
                sbSql.AppendFormat(" and LOG.WERKS='{0}' ", WERKS);
                sbSql.AppendFormat(" and LOG.LGORT='{0}' ", LGORT);
                #endregion
                #region where condition- Normal Query
                if (strCgcls != "")
                {
                    //strSQL += " and CGCLS = '" + strCgcls + "'";
                    //alQueryCondition.Add("CGCLS = '" + strCgcls + "'");
                    sbSql.AppendFormat(" and LOG.CGCLS='{0}' ", strCgcls);
                }

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK = '" + strInsmk + "'";
                    //alQueryCondition.Add("INSMK = '" + strInsmk + "'");
                    sbSql.AppendFormat(" and LOG.INSMK='{0}' ", strInsmk);
                }

                if (strCharg != "")
                {
                    //strSQL += " and CHARG = '" + strCharg + "'";
                    //alQueryCondition.Add("CHARG = '" + strCharg + "'");
                    sbSql.AppendFormat(" and LOG.CHARG='{0}' ", strCharg);
                }

                if (strUsrnm != "")
                {
                    //strSQL += " and CRNAM = '" + strUsrnm + "'";
                    //alQueryCondition.Add("CRNAM = '" + strUsrnm.ToString().Trim() + "'");
                    sbSql.AppendFormat(" and LOG.CRNAM='{0}' ", strUsrnm.ToString().Trim());
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    //alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
                    //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                    sbSql.AppendFormat(" and (LOG.CRDAT between '{0}' and '{1}' ) ", strStartDate, strEndDate);
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    //alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
                    //strSQL += " and CRDAT = '" + strEndDate + "'";
                    sbSql.AppendFormat(" and LOG.CRDAT='{0}' ", strEndDate);
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    //strSQL += " and CRDAT = '" + strStartDate + "'";
                    //alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
                    sbSql.AppendFormat(" and LOG.CRDAT='{0}' ", strStartDate);
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    // alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                    sbSql.AppendFormat(" and (LOG.MATNR between '{0}' and '{1}' ) ", strStartMatnr, strEndMatnr);
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    //strSQL += " and MATNR = '" + strEndMatnr + "'";
                    //alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
                    sbSql.AppendFormat(" and LOG.MATNR='{0}' ", strEndMatnr);
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    //strSQL += " and MATNR = '" + strStartMatnr + "'";
                    //alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
                    sbSql.AppendFormat(" and LOG.MATNR='{0}' ", strStartMatnr);
                }

                if (strStartLocat != "" && strEndLocat != "")
                {
                    //strSQL += " and ((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))";
                    //alQueryCondition.Add("((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "') or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))");
                    sbSql.AppendFormat(" and (LOG.OLOCA between '{0}' and '{1}' ) ", strStartLocat, strEndLocat);
                }
                else if (strStartLocat == "" && strEndLocat != "")
                {
                    //strSQL += " and (OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')";
                    //alQueryCondition.Add("(OLOCA = '" + strEndLocat + "' or NLOCA = '" + strEndLocat + "')");
                    sbSql.AppendFormat(" and ( LOG.OLOCA='{0}' or LOG.NLOCA='{1}' ) ", strEndLocat, strEndLocat);
                }
                else if (strStartLocat != "" && strEndLocat == "")
                {
                    //alQueryCondition.Add("(OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')");
                    //strSQL += " and (OLOCA = '" + strStartLocat + "' or NLOCA = '" + strStartLocat + "')";
                    sbSql.AppendFormat(" and ( LOG.OLOCA='{0}' or LOG.NLOCA='{1}' ) ", strStartLocat, strStartLocat);
                }

                //20070419 for document No. marc add
                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    //alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
                    //strSQL += " and (MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                    sbSql.AppendFormat(" and (LOG.MBLNR between '{0}' and '{1}' ) ", strStartMblnr, strEndMblnr);
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    //alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
                    //strSQL += " and MBLNR = '" + strEndMblnr + "'";
                    sbSql.AppendFormat(" and LOG.MBLNR like '{0}%'", strEndMblnr);
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    //alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
                    //strSQL += " and MBLNR = '" + strStartMblnr + "'";
                    sbSql.AppendFormat(" and LOG.MBLNR like '{0}%'", strStartMblnr);
                }
                #endregion
                #region where condition- AGV Kitting Query
                if (strBWART != "")
                {
                    sbSql.AppendFormat(" and DWN.BWART='{0}'", strBWART);
                }


                if (strAGVLine != "")   //線別
                {
                    sbSql.AppendFormat(" and PRI.ARBPL='{0}' ", strAGVLine);
                }
                if (strAGVSendID != "") //SendID
                {
                    sbSql.AppendFormat(" and DWN.REFID='{0}' ", strAGVSendID);
                }
                if (strAGVMATNM != "") //品名
                {
                    sbSql.AppendFormat(" and MAN.MATNM like '{0}%' ", strAGVMATNM);
                }

                //發料日期
                if (strAGVStartDate != "" && strAGVEndDate != "")
                {
                    sbSql.AppendFormat(" and (DWN.BUDAT between '{0}' and '{1}' ) ", strAGVStartDate, strAGVEndDate);
                }
                else if (strAGVStartDate == "" && strAGVEndDate != "")
                {
                    sbSql.AppendFormat(" and DWN.BUDAT < '{0}' ", strAGVEndDate);
                }
                else if (strAGVStartDate != "" && strAGVEndDate == "")
                {
                    sbSql.AppendFormat(" and DWN.BUDAT > '{0}' ", strAGVStartDate);
                }
                else
                {
                }

                //發料時間
                if (strAGVStartTime != "" && strAGVEndTime != "")
                {
                    sbSql.AppendFormat(" and (DWN.PRITY between '{0}' and '{1}' ) ", strAGVStartTime, strAGVEndTime);
                }
                else if (strAGVStartTime == "" && strAGVEndTime != "")
                {
                    sbSql.AppendFormat(" and DWN.PRITY <= '{0}' ", strAGVEndTime);
                }
                else if (strAGVStartTime != "" && strAGVEndTime == "")
                {
                    sbSql.AppendFormat(" and DWN.PRITY >= '{0}' ", strAGVStartTime);
                }
                else
                {
                }
                #endregion

                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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
        public DataTable QueryQsmsData(string strInsmk, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, int intType, int intGRGI, string strBwart, string strMType)
        {
            DataWhrid objWhrid = new DataWhrid(UserData);
            ArrayList alColumns = new ArrayList();
            ArrayList alQueryCondition = new ArrayList();


            alColumns.Add("TOP 500 *");
            alColumns.Add("REFID as MBLNR");
            alColumns.Add("DIDNO as ZEILE");
            alColumns.Add("'G+' as TRNTP");
            alColumns.Add("'' as BWART");
            alColumns.Add("'' as UMLGO");
            alColumns.Add("'' as USNAM");
            alColumns.Add("'' as KDMAT");

            alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
            alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
            alQueryCondition.Add("WERKS = '" + WERKS + "'");

            //string strSQL = "Select *, REFID as MBLNR, DIDNO as ZEILE, 'G+' as TRNTP, '' as BWART, '' as UMLGO, '' as USNAM, '' as KDMAT from WHRID where MANDT='" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' ";

            if (strInsmk != "")
            {
                //strSQL += " and INSMK = '" + strInsmk + "'";
                alQueryCondition.Add("INSMK = '" + strInsmk + "'");
            }

            if (strStartDate != "" && strEndDate != "")
            {
                //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
            }
            else if (strStartDate == "" && strEndDate != "")
            {
                //strSQL += " and CRDAT = '" + strEndDate + "'";
                alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
            }
            else if (strStartDate != "" && strEndDate == "")
            {
                //strSQL += " and CRDAT = '" + strStartDate + "'";
                alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
            }

            if (strStartMatnr != "" && strEndMatnr != "")
            {
                //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
            }
            else if (strStartMatnr == "" && strEndMatnr != "")
            {
                //strSQL += " and MATNR = '" + strEndMatnr + "'";
                alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
            }
            else if (strStartMatnr != "" && strEndMatnr == "")
            {
                //strSQL += " and MATNR = '" + strStartMatnr + "'";
                alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
            }

            if (strStartMblnr != "" && strEndMblnr != "")
            {
                //strSQL += " and (REFID between '" + strStartMblnr + "' and '" + strEndMblnr + "')";
                alQueryCondition.Add("(REFID between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
            }
            else if (strStartMblnr == "" && strEndMblnr != "")
            {
                //strSQL += " and REFID like '" + strEndMblnr + "%'";
                alQueryCondition.Add("REFID like '" + strEndMblnr + "%'");
            }
            else if (strStartMblnr != "" && strEndMblnr == "")
            {
                //strSQL += " and REFID like '" + strStartMblnr + "%'";
                alQueryCondition.Add("REFID like '" + strStartMblnr + "%'");
            }

            if (intType == 0)
            {
                //strSQL += " and MENGE=OTQTY";
                alQueryCondition.Add("MENGE = OTQTY");
            }
            else if (intType == 1)
            {
                //strSQL += " and MENGE>OTQTY";
                alQueryCondition.Add("MENGE > OTQTY");
            }

            if (strBwart.Trim() != "")
            {
                //strSQL += " and BWART='" + strBwart.Trim() + "'";
                alQueryCondition.Add("BWART='" + strBwart.Trim() + "'");
            }
            DataTable dtData = new DataTable();
            try
            {
                dtData = objWhrid.EntityQuery(alColumns, alQueryCondition, false, true);
                //ControlHandleDB();
                //dtData = ControlSqlAccess.GetDataTable(strSQL);
                //ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- QueryQsmsData()";
            }
            return dtData;

        }
        public DataTable QueryXLData(string strWerks,string strLgort,string strGrno)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData";
            this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strGrno + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" select MANDT,COMCD,WERKS,LGORT,KOSTL,GRPID,LOCAT,MATNR,charg,menge,serno,lifnr,dacod,LOCOD,DIDNO,MBLNR,ZEILE,UMLGO,CRDAT,FLAGE,REMARK1,REMARK2,REMARK3,REMARK4,REMARK5,UID  ");
                strSql.AppendFormat(" FROM alsmt WHERE 1=1  ");

                if (strWerks != "")
                    strSql.AppendFormat(" And WERKS ='" + strWerks + "' ");
                if (strLgort != "")
                    strSql.AppendFormat(" And LGORT ='" + strLgort + "' ");
                //if (strInsmk != "")
                //    strSql.AppendFormat(" And WERKS ='" + strInsmk + "' ");
                if (strGrno != "")
                    strSql.AppendFormat(" And GRPID ='" + strGrno + "' ");
                //alQueryCondition.Add("DACOD ='" + DC + "'");

                DataTable dtData = new DataTable();
                try
                {
                    //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
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
        public DataTable QueryDIDData(string strWerks,string strLgort,string strInsmk,string tbDIDNo,string tbXNDJ)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData";
            this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strInsmk + "','" + tbDIDNo + "','" + tbXNDJ + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("  SELECT MANDT,COMCD,REFID,DIDNO,WERKS,LGORT,RUNNER,MATNR,INSMK,CHARG,MENGE,OTQTY,LOCAT,OTWRK,OTLGT,LIFNR,CRDAT,MANDT,OMBLN,KOSTL,UPDAT,MBLNR,DACOD,LOCOD,SERNO,ULFLG,Msg,REMARK1,REMARK2,REMARK3,UID ");
                strSql.AppendFormat(" FROM ALRID  WHERE 1=1  ");

                if (strWerks != "")
                    strSql.AppendFormat(" And WERKS ='" + strWerks + "' ");
                if (strLgort != "")
                    strSql.AppendFormat(" And LGORT ='" + strLgort + "' ");
                //if (strInsmk != "")
                //    strSql.AppendFormat(" And WERKS ='" + strInsmk + "' ");
                if (strInsmk != "")
                    strSql.AppendFormat(" And INSMK ='" + strInsmk + "' ");
                if (tbDIDNo != "")
                    strSql.AppendFormat(" And DIDNO ='" + tbDIDNo + "' ");
                if (tbXNDJ != "")
                    strSql.AppendFormat(" And MBLNR ='" + tbXNDJ + "' ");
                //alQueryCondition.Add("DACOD ='" + DC + "'");

                DataTable dtData = new DataTable();
                try
                {
                    //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
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
        public DataTable QueryXYData(string strWerks, string strLgort, string strInsmk, string tbXYNo, string tbXYGRNo)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData";
            this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strInsmk + "','" + tbXYNo + "','" + tbXYGRNo + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" SELECT MTYPE,MBLNR,ZEILE,WERKS,LGORT,MATNR,INSMK,CHARG,LIFNR,MENGE,OTQTY,KOSTL,BWART,KDMAT,REFID,COMCD,BOXID  ");
                strSql.AppendFormat(" FROM  WHDWN WHERE OTQTY<MENGE   ");

                if (strWerks != "")
                    strSql.AppendFormat(" And WERKS ='" + strWerks + "' ");
                if (strLgort != "")
                    strSql.AppendFormat(" And LGORT ='" + strLgort + "' ");
                //if (strInsmk != "")
                //    strSql.AppendFormat(" And WERKS ='" + strInsmk + "' ");
                if (strInsmk != "")
                    strSql.AppendFormat(" And INSMK ='" + strInsmk + "' ");
                if (tbXYNo != "")
                    strSql.AppendFormat(" And DIDNO ='" + tbXYNo + "' ");
                if (tbXYGRNo != "")
                    strSql.AppendFormat(" And MBLNR ='" + tbXYGRNo + "' ");
                //alQueryCondition.Add("DACOD ='" + DC + "'");

                DataTable dtData = new DataTable();
                try
                {
                    //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
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

        public DataTable QuerySapData(string strStartDate, string strEndDate, int intType, string strStartMatnr, string strEndMatnr, string strMType)
        {
            DataWhdwn objWhdwn = new DataWhdwn(UserData);
            ArrayList alColumns = new ArrayList();
            ArrayList alQueryCondition = new ArrayList();
            ArrayList alGroupby = new ArrayList();

            alColumns.Add("MANDT");
            alColumns.Add("COMCD");
            alColumns.Add("WERKS");
            alColumns.Add("LGORT");
            alColumns.Add("MATNR");
            alColumns.Add("INSMK");
            alColumns.Add("CHARG");
            alColumns.Add("right(TRNTP,1) as TRNTP");
            alColumns.Add("sum(MENGE) as MENGE");
            alColumns.Add("sum(OTQTY) as OTQTY");
            alColumns.Add("sum(MENGE)-sum(OTQTY) as BALANCE");

            alQueryCondition.Add("MANDT='" + UserData.Client + "'");
            alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
            alQueryCondition.Add("WERKS = '" + WERKS + "'");

            //string strSQL = "Select MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, right(TRNTP,1) as TRNTP, sum(MENGE) as MENGE, sum(OTQTY) as OTQTY, sum(MENGE)-sum(OTQTY) as BALANCE from WHDWN where MANDT='" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS = '" + WERKS + "' ";

            if (strStartDate != "" && strEndDate != "")
            {
                //strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "')";
                alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
            }
            else if (strStartDate == "" && strEndDate != "")
            {
                //strSQL += " and CRDAT = '" + strEndDate + "'";
                alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
            }
            else if (strStartDate != "" && strEndDate == "")
            {
                //strSQL += " and CRDAT = '" + strStartDate + "'";
                alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
            }

            if (strStartMatnr != "" && strEndMatnr != "")
            {
                //strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
            }
            else if (strStartMatnr == "" && strEndMatnr != "")
            {
                //strSQL += " and MATNR = '" + strEndMatnr + "'";
                alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
            }
            else if (strStartMatnr != "" && strEndMatnr == "")
            {
                //strSQL += " and MATNR = '" + strStartMatnr + "'";
                alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
            }

            if (intType == 0)
            {
                //strSQL += " and MENGE=OTQTY";
                alQueryCondition.Add("MENGE=OTQTY");
            }
            else if (intType == 1)
            {
                //strSQL += " and MENGE>OTQTY";
                alQueryCondition.Add("MENGE>OTQTY");
            }

            if (strMType.Trim() != "")
            {
                if (strMType.Trim() == "SAP")
                {
                    //strSQL += " and MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'";
                    alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                }
                else
                {
                    //strSQL += " and MTYPE='" + strMType.Trim() + "' ";
                    alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                }
            }

            alGroupby.Add("MANDT");
            alGroupby.Add("COMCD");
            alGroupby.Add("WERKS");
            alGroupby.Add("LGORT");
            alGroupby.Add("MATNR");
            alGroupby.Add("INSMK");
            alGroupby.Add("CHARG");
            alGroupby.Add("right(TRNTP,1)");

            string strFromTable = "WHDWN";

            //strSQL += " group by MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, right(TRNTP,1)";
            DataTable dtData = new DataTable();
            try
            {
                dtData = objWhdwn.ControlQuery(strFromTable, alColumns, alQueryCondition, false, alGroupby, "");
                //ControlHandleDB();
                //dtData = ControlSqlAccess.GetDataTable(strSQL);
                //ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- QuerySapData()";
            }
            return dtData;

        }
        public DataTable QuerySapData(string strInsmk, string strStartDate, string strEndDate, string strStartMatnr, string strEndMatnr, string strStartMblnr, string strEndMblnr, int intType, int intGRGI, string strBwart, string strMType, string strQueryTable)
        {
            ArrayList alColumns = new ArrayList();
            ArrayList alQueryCondition = new ArrayList();

            alColumns.Add(" TOP 10000 *");

            alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
            alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
            //alQueryCondition.Add("WERKS = '" + WERKS + "' And UMLGO<>'IQC2'");
            //REFID NOT LIKE 'B%AUTO'
            // alQueryCondition.Add("REFID NOT LIKE 'B%AUTO'");
            //    alQueryCondition.Add(" LEFT(USNAM,4)<>'7604'  ");//卡掉SAP自动扣账的单子
            alQueryCondition.Add("WERKS = '" + WERKS + "'");

            if (strInsmk != "")
            {
                alQueryCondition.Add("INSMK = '" + strInsmk + "'");
            }

            if (strStartDate != "" && strEndDate != "")
            {
                alQueryCondition.Add("(CRDAT between '" + strStartDate + "' and '" + strEndDate + "')");
            }
            else if (strStartDate == "" && strEndDate != "")
            {
                alQueryCondition.Add("CRDAT = '" + strEndDate + "'");
            }
            else if (strStartDate != "" && strEndDate == "")
            {
                alQueryCondition.Add("CRDAT = '" + strStartDate + "'");
            }

            if (strStartMatnr != "" && strEndMatnr != "")
            {
                alQueryCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
            }
            else if (strStartMatnr == "" && strEndMatnr != "")
            {
                alQueryCondition.Add("MATNR = '" + strEndMatnr + "'");
            }
            else if (strStartMatnr != "" && strEndMatnr == "")
            {
                alQueryCondition.Add("MATNR = '" + strStartMatnr + "'");
            }

            if (strStartMblnr != "" && strEndMblnr != "")
            {
                alQueryCondition.Add("(MBLNR between '" + strStartMblnr + "' and '" + strEndMblnr + "')");
            }
            else if (strStartMblnr == "" && strEndMblnr != "")
            {
                alQueryCondition.Add("MBLNR like '" + strEndMblnr + "%'");
            }
            else if (strStartMblnr != "" && strEndMblnr == "")
            {
                alQueryCondition.Add("MBLNR like '" + strStartMblnr + "%'");
            }

            if (intType == 0)
            {
                alQueryCondition.Add("MENGE=OTQTY");
            }
            else if (intType == 1)
            {
                alQueryCondition.Add("MENGE>OTQTY");
            }

            if (intGRGI == 0)
            {
                alQueryCondition.Add("TRNTP in ('T+', 'G+' ,'M+', 'R+')");
            }
            else if (intGRGI == 1)
            {
                alQueryCondition.Add("TRNTP in ('T-', 'G-' ,'M-', 'R-')");
            }

            if (strBwart.Trim() != "")
            {
                alQueryCondition.Add("BWART='" + strBwart.Trim() + "'");
            }

            if (strMType.Trim() != "")
            {
                if (strMType.Trim() == "SAP")
                {
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (COMCD == "9110" && LGORT == "" || COMCD == "9200" && LGORT == "" || COMCD == "2281" && LGORT == "")
                    {
                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                    }
                    else if (COMCD == "9110" && LGORT != "" || COMCD == "9200" && LGORT != "" || COMCD == "2281" && LGORT != "")
                    {
                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                    }
                    else
                    {
                        alQueryCondition.Add("MTYPE='" + strMType.Trim() + "' and LGORT = '" + LGORT + "'");
                    }
                    #endregion
                }
                else
                {
                    if (strMType.Trim() == "S/F(PCBA)")
                        strMType = "QMS_M";

                    alQueryCondition.Add("MTYPE='" + strMType.Trim() + "'");
                }

            }
            alQueryCondition.Add("NOT ( BWART='321' AND TRNTP='T+' ) ");
            DataTable dtData = new DataTable();
            try
            {
                string strSQL = ControlGetQuerySql(strQueryTable, alColumns, alQueryCondition, false);
                dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- QuerySapData()";
            }
            return dtData;

        }
        public DataTable QueryGRData(string strWerks, string strLgort, string strGrno)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryLogData";
            this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strGrno + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("  SELECT WERKS,LGORT,MBLNR,MATNR,MENGE,ALQTY,CRNAM,CRDAT,Remark1,UID    ");
                strSql.AppendFormat(" FROM ALGIN WITH(NOLOCK) WHERE 1=1  ");

                if (strWerks != "")
                    strSql.AppendFormat(" And WERKS ='" + strWerks + "' ");
                if (strLgort != "")
                    strSql.AppendFormat(" And LGORT ='" + strLgort + "' ");
                //if (strInsmk != "")
                //    strSql.AppendFormat(" And WERKS ='" + strInsmk + "' ");
                if (strGrno != "")
                    strSql.AppendFormat(" And MBLNR ='" + strGrno + "' ");
                //alQueryCondition.Add("DACOD ='" + DC + "'");

                DataTable dtData = new DataTable();
                try
                {
                    //dtData = ControlQuery(strQueryTable, alColumns, alQueryCondition, false);
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
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
