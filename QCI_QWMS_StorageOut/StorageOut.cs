using System;
using System.Data;
using System.Collections;
using System.Text;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using QWMS.Entity;
using QCI_QWMS_Entity;
using System.Linq;
using System.Collections.Generic;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// StorageData 的摘要描述。
        /// </summary>
        public class StorageOut : ControlBase
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
            public StorageOut()
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
            public StorageOut(UserInfo varUserData, string varProgid)
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
            public StorageOut(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
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
            public StorageOut(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string strProgid)
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
            public StorageOut(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strProgid)
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

            #region MemberFunction

            #region 檢查使用者是否有使用系統維護相關功能的權限
            //=========================================================================
            ////////////Summary by Robert Chen////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用系統維護相關功能的權限。
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.CheckAuthority();
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
                    QCI.QWMS.Authority objAuthority = new Authority(UserData);
                    if (objAuthority.OTAUT.IndexOf(strProgid) < 0)
                        return false;
                    else
                        return true;
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


            #region  儲存離線出庫資料
            //========================================================================================
            ////////////Summary by Smose Liao 20091030////////////////////////////////////////////////
            /// <summary>
            /// 儲存離線出庫資料。
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutData(strIsmrg, dtOutSource, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutData(string strIsmrg, DataTable dtOutSource, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutData";
                this.ControlMethodParm = "(" + strIsmrg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strTempLocat = "";

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=" + dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }

                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

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

            #endregion


            #region 儲存離線出庫資料(加入限定vendor code)
            //========================================================================================================
            ////////////Summary by Smose Liao 20091030////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存離線出庫資料(加入限定vendor code)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutData(strIsmrg, dtOutSource, dtStorage, string strLifnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutData(string strIsmrg, DataTable dtOutSource, DataTable dtStorage, string strLifnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutData";
                this.ControlMethodParm = "(" + strIsmrg + "," + strLifnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strTempLocat = "";

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=" + dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and LIFNR='" + strLifnr.Trim() + "' ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'' and LIFNR='" + strLifnr.Trim() + "'  ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

                    //bolReturn = this.objSQLAccess.ExecSQLArray(arySQL);
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

            #endregion

            #region 儲存離線出庫資料(加入限定vendor code)
            //========================================================================================================
            ////////////Summary by Smose Liao 20091030////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存離線出庫資料(加入限定vendor code)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutData(strIsmrg, dtOutSource, dtStorage, string strLifnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutData_DateCode(string strIsmrg, DataTable dtOutSource, DataTable dtStorage, string strLifnr, string strDacod)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutData_DateCode";
                this.ControlMethodParm = "(" + strIsmrg + "," + strLifnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strTempLocat = "";

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            if (!strDacod.Trim().Equals(""))
                                alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=" + dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            if (!strDacod.Trim().Equals(""))
                                alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and LIFNR='" + strLifnr.Trim() + "' ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'' and LIFNR='" + strLifnr.Trim() + "'  ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

                    //bolReturn = this.objSQLAccess.ExecSQLArray(arySQL);
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

            #endregion

            #region 儲存離線出庫資料(加入限定vendor code BY DateCode)
            //========================================================================================================
            ////////////Summary by Smose Liao 20091030////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存離線出庫資料(加入限定vendor code)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutData(strIsmrg, dtOutSource, dtStorage, string strLifnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutData_DateCode(string strIsmrg, DataTable dtOutSource, DataTable dtStorage, string strLifnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutData_DateCode";
                this.ControlMethodParm = "(" + strIsmrg + "," + strLifnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strTempLocat = "";

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            if (UserData.CompanyCode == "9110")
                            {
                                alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            }
                            if (!strLifnr.Trim().Equals(""))
                                alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            if (dtStorage.Rows[i]["DACOD"] != null)
                            {
                                if (!dtStorage.Rows[i]["DACOD"].ToString().Trim().Equals(""))
                                    alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            }


                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=" + dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and LIFNR='" + strLifnr.Trim() + "'  ");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            if (UserData.CompanyCode == "9110")
                            {
                                alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            }
                            if (!strLifnr.Trim().Equals(""))
                                alConditions.Add(" LIFNR='" + strLifnr.Trim() + "'");

                            if (dtStorage.Rows[i]["DACOD"] != null)
                            {
                                if (!dtStorage.Rows[i]["DACOD"].ToString().Trim().Equals(""))
                                    alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            }

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and LIFNR='" + strLifnr.Trim() + "' ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'' and LIFNR='" + strLifnr.Trim() + "'  ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

                    //bolReturn = this.objSQLAccess.ExecSQLArray(arySQL);
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

            #endregion


            #region 儲存成品離線出庫資料
            //====================================================================================================================================================
            ////////////Summary by Smose Liao 20091103////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存成品離線出庫資料。
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutDataProd(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutDataProd(DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutDataProd";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    string strTempLocat = "";

                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }

                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            alColumns.Clear();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "' and MENGE='0'");
                            objWhitm.ResetField();
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                            alColumns.Clear();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            alConditions.Add(" MENGE= '0' ");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

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

            #endregion


            #region 儲存SpareParts離線出庫資料
            //========================================================================================================
            ////////////Summary by Smose Liao 20100531////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存SpareParts離線出庫資料
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOffLineOutData(dtOutSource, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOffLineOutData_SpareParts(DataTable dtOutSource, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOffLineOutData_SpareParts";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strTempLocat = "";

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'' ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

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

            #endregion


            #region  儲存依儲位出庫的資料
            //======================================================================================================================================================
            ////////////Summary by Smose Liao 20091105//////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲存依儲位出庫的資料
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddLocatOutData(strIsmrg, strLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddLocatOutData(string strIsmrg, string strLocat, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddLocatOutData";
                this.ControlMethodParm = "(" + strIsmrg + "," + strLocat + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    string strTempLocat = "";

                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                        }
                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            alColumns.Clear();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            objWhitm.ResetField();
                            objWhitm.Menge = dtStorage.Rows[i]["MENGE"].ToString() + " - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alColumns.Clear();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }
                    //WHDWN
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        if (dtStorage.Rows[i]["MBLNR"].ToString().Trim() != "")
                        {
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();
                            objWhdwn.Otqty = "OTQTY + " + dtStorage.Rows[i]["ALQTY"].ToString();
                            alConditions.Add(" MANDT='" + strMandt + "'");
                            alConditions.Add(" COMCD='" + strComcd + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                            if (dtStorage.Rows[i]["ZEILE"].ToString().Trim() != "")
                            {
                                alConditions.Add(" ZEILE='" + dtStorage.Rows[i]["ZEILE"].ToString() + "'");
                            }

                            arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        }
                    }

                    ControlHandleDB();
                    ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
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

            #endregion


            #region  儲存連線出庫資料(By Picasso)
            //====================================================================================
            ////////////Summary by Smose Liao 20090606////////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫資料(By Picasso)。
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOnLineOutData_Picasso(dtOutSource, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public bool AddOnLineOutData_Picasso(DataTable dtOutSource, DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOnLineOutData_Picasso";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    string strTempLocat = "";
                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);
                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, SUM(MENGE) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        sbSQL.Append(" GROUP BY MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG ");

                        //dtTmpStock = this.objSQLAccess.GetDataTable(sbSQL.ToString());
                        ControlHandleDB();
                        dtTmpStock = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion


                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" BOXID='" + dtStorage.Rows[i]["BOXID"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "' and MENGE='0'");
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" BOXID='" + dtStorage.Rows[i]["BOXID"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhitm.ResetField();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" BOXID='" + dtStorage.Rows[i]["BOXID"].ToString() + "'");
                            alConditions.Add(" MENGE='0'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }
                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        //arySQL.Add("Update WHDWN set OTQTY=OTQTY +'" + dtOutSource.Rows[i]["ALQTY"].ToString() + "' where MANDT= '" + strMandt + "' and MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "' and ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY + " + dtOutSource.Rows[i]["ALQTY"].ToString();
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                        aryCheckSQL.Add("select MBLNR + '--' + ZEILE  FROM WHDWN with (nolock) WHERE (MANDT= '" + strMandt + "') and (MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "') and (ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "') and (MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        //dtChkTmp = objSQLAccess.GetDataTable(aryCheckSQL[j].ToString());
                        ControlHandleDB();
                        dtChkTmp = ControlSqlAccess.GetDataTable(aryCheckSQL[j].ToString());
                        ControlSqlAccess.CloseConnection();

                        if (dtChkTmp.Rows.Count > 0)
                        {
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                        }
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
                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
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
                return bolReturn;

            }

            #endregion


            #region  儲存SI出庫確認的資料(Spare Parts)
            //===================================================================================
            ////////////Summary by Smose Liao 20100430///////////////////////////////////////////
            /// <summary>
            /// 儲存SI出庫確認的資料(Spare Parts)
            /// </summary> 
            /// <param name="dtStorage">庫存資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddSinoOutData_Confirm(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddSinoOutData_Confirm(DataTable dtStorage)
            {
                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                string strTempLocat = "";
                bool bolReturn = false;

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);

                //WHLOG
                QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                arySQL = objLogData.AddLogData("", "", dtStorage);

                //WHITM
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    //記錄所有的異動待出貨儲位(NLOCA)
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["NLOCA"].ToString()) < 0)
                    {
                        aryLocat.Add(dtStorage.Rows[i]["NLOCA"].ToString());
                        strTempLocat += "++" + dtStorage.Rows[i]["NLOCA"].ToString();
                    }

                    //WHITM
                    //刪掉庫存數量  Smose Liao 20100430
                    objWhitm.ResetField();
                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                    alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                    alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                    alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                    alConditions.Add(" NLOCA='" + dtStorage.Rows[i]["NLOCA"].ToString() + "'");
                    alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                    alConditions.Add(" SIDNO='" + dtStorage.Rows[i]["SIDNO"].ToString() + "'");
                    alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                    alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                    arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                }

                //WHHED
                #region 處理WHHED Table Update 是否有庫存(LOSTS)

                #region 整理出會異動的Location
                StringBuilder sbLocats = new StringBuilder();
                sbLocats.Remove(0, sbLocats.Length);
                for (int i = 0; i < aryLocat.Count; i++)
                {
                    if (i != 0)
                        sbLocats.Append(" , ");
                    sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                }
                #endregion

                #region 先Update 待出貨儲位(WHITM.NLOCA)有庫存的部分(Losts = 1)
                objWhhed.ResetField();
                objWhhed.Losts = "1";

                alConditions.Clear();
                alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and NLOCA= WHHED.LOCAT))");
                alConditions.Add("(MANDT= '" + strMandt + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + strWerks + "')");
                alConditions.Add("(LGORT= '" + strLgort + "')");
                alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                #endregion

                #region 再Update 待出貨儲位(WHITM.NLOCA)無庫存的部分(Losts = 0)
                objWhhed.ResetField();
                objWhhed.Losts = "0";

                alConditions.Clear();
                alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and NLOCA= WHHED.LOCAT))");
                alConditions.Add("(MANDT= '" + strMandt + "')");
                alConditions.Add("(COMCD= '" + COMCD + "')");
                alConditions.Add("(WERKS= '" + strWerks + "')");
                alConditions.Add("(LGORT= '" + strLgort + "')");
                alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                #endregion

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
                return bolReturn;
            }

            #endregion


            #region 儲存連線出庫確認資料(兩階段出庫QWMS庫存除帳)
            //===================================================================================
            ////////////Summary by Smose Liao 2010917////////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫確認資料(兩階段出庫QWMS庫存除帳)
            /// </summary> 
            /// <param name="dtStorage">庫存資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddOnLineOutConfirmData(DataTable dtStorage)
            {
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhgrr objWhgrr = new DataWhgrr(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                bool bolUpdate = false;

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
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Bkqty = "BKQTY + " + "(" + dtStorage.Rows[i]["ALQTY"].ToString() + ")";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        bolUpdate = true;
                    }
                }

                bool bolReturn = false;
                try
                {
                    //如果出庫單扣帳成功，將WHGRR的PRTYP狀態變更為已處理('1')，同時刪掉庫存數量(Menge)為0的庫存
                    if (bolUpdate == true)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {
                            #region  變更WHGRR的狀態

                            objWhgrr.ResetField();
                            alConditions.Clear();
                            objWhgrr.Prtyp = "1";
                            alConditions.Add(" GRRNO='" + dtStorage.Rows[i]["GRRNO"].ToString() + "'");
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhgrr.EntityGetUpdateSql(alConditions));

                            #endregion

                            #region  刪掉庫存數量(Menge)為0的庫存
                            objWhitm.ResetField();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" MENGE= 0 ");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                            #endregion
                        }

                        //WHHED
                        #region "Update WHHED Table"
                        for (int i = 0; i < aryLocat.Count; i++)
                        {
                            arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                            //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and MRGID<>'') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                        }
                        #endregion

                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                    else
                    {
                        ERRMSG = "扣帳未成功，請確認!!";
                    }

                }
                catch (System.Exception ex)
                {

                    ERRMSG = ex.Message + "<- AddOnLineOutConfirmData()";

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
                    string strErrSQL = "Insert into ERRLOG (MANDT,COMCD,GRRNO,MBLNR,LOGSQL,LOGTIM,MALFLG) " +
                                       "Values('" + dtStorage.Rows[0]["MANDT"].ToString() + "','" + dtStorage.Rows[0]["COMCD"].ToString() + "','" + dtStorage.Rows[0]["GRRNO"].ToString() + "'," +
                                              "'" + dtStorage.Rows[0]["OMBLNR"].ToString() + "',N'" + sbErrSql.ToString() + "',getdate(),'N')";
                    this.ControlSqlAccess.ExecSql(strErrSQL);

                    #endregion

                }
                return bolReturn;
            }
            #endregion


            #region  儲存連線出庫確認的資料(連線出庫 by DateCode)
            //===================================================================================
            ////////////Summary by Smose Liao 20091106///////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫確認的資料(連線出庫 by DateCode)
            /// </summary> 
            /// <param name="dtStorage">庫存資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddOnLineOutData_DateCode_Confirm(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddOnLineOutData_DateCode_Confirm(DataTable dtStorage)
            {
                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                string strTempLocat = "";
                bool bolUpdate = false;

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhgrr objWhgrr = new DataWhgrr(UserData);

                //WHLOG
                QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                arySQL = objLogData.AddLogData("", "", dtStorage);

                //WHITM
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                    }

                    //更新WHITM之MENGE欄位的數量(實際扣帳的動作)  Smose Liao 20090507
                    //if(objStorage.QueryDataExistsInStorage(dtStorage.Rows[i]["LOCAT"].ToString(),dtStorage.Rows[i]["MATNR"].ToString(),dtStorage.Rows[i]["INSMK"].ToString(),dtStorage.Rows[i]["MBLNR"].ToString(),dtStorage.Rows[i]["CHARG"].ToString(),dtStorage.Rows[i]["SERNO"].ToString() + "__" + dtStorage.Rows[i]["INSPT"].ToString()))
                    if (objStorageData.QueryDataExistsInStorage_DateCode(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["OMBLN"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), dtStorage.Rows[i]["DACOD"].ToString()))
                    {

                        #region 新出庫確認程式  Smose Liao 20091008
                        //修改為WHITM.MENGE - WHGRR.ALQY and WHITM.BKQTY + WHGRR.ALQTY 
                        //WHGRR.ALQTY 為實際出庫數量
                        //strSQL = "Update WHITM set MENGE= MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", BKQTY = BKQTY + (" + dtStorage.Rows[i]["ALQTY"].ToString() + ")  where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() +
                        //    "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() +
                        //    "' and MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'";
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Bkqty = "BKQTY + " + "(" + dtStorage.Rows[i]["ALQTY"].ToString() + ")";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        //Add: 增加Lock Code與Inspection Lot當主鍵 by Smose Liao 20100127
                        alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        #endregion

                        bolUpdate = true;
                    }
                    else
                    {
                        ERRMSG = "這筆Barcode不存在，請確認!!";
                        bolUpdate = false;
                        break;
                    }
                }

                bool bolReturn = false;
                try
                {
                    //如果出庫單扣帳成功，將WHGRR的PRTYP狀態變更為已處理('1')  Smose Liao 20090508
                    if (bolUpdate == true)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {
                            //strSQL = "Update WHGRR set PRTYP = '1' where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() +
                            //    "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() +
                            //    "' and MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'";
                            objWhgrr.ResetField();
                            alConditions.Clear();
                            objWhgrr.Prtyp = "1";
                            alConditions.Add(" GRRNO='" + dtStorage.Rows[i]["GRRNO"].ToString() + "'");
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            //Add: 增加Lock Code與Inspection Lot當主鍵 by Smose Liao 20100127
                            alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                            alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                            arySQL.Add(objWhgrr.EntityGetUpdateSql(alConditions));

                            //刪掉庫存數量(Menge)為0的料號  Smose Liao 20091009
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and MENGE='0'  and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                            objWhitm.ResetField();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            //Add: 增加Lock Code與Inspection Lot當主鍵 by Smose Liao 20100127
                            alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                            alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");
                            alConditions.Add(" MENGE= 0 ");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }

                        //WHHED
                        #region "Update WHHED Table"
                        for (int i = 0; i < aryLocat.Count; i++)
                        {
                            arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                            //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and MRGID<>'') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                        }
                        #endregion

                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                    else
                    {
                        ERRMSG = "扣帳未成功，請確認!!";
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
                return bolReturn;
            }

            #endregion


            #region  儲存連線出庫資料(By DateCode)
            //=====================================================================================
            ////////////Summary by Smose Liao 20090629/////////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫資料(By DateCode)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString, strMandt, strWerks, strLgort, strMblnr, strProgid, strCrnam)
            /// bool boolCheck =obj.AddOnLineOutData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////
            public string AddOnLineOutData_DateCode_New(DataTable dtOutSource, DataTable dtStorage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOnLineOutData_DateCode_New";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strTempLocat = "";
                string strSERNO = "";
                string strGRRNO = "";
                string CYear = "";
                string CMonth = "";
                string CDay = "";
                string Itemnum = "";
                bool bolReturn = false;

                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);
                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, SUM(MENGE + BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        sbSQL.Append(" GROUP BY MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG ");

                        ControlHandleDB();
                        dtTmpStock = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return ERRMSG;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return ERRMSG;
                        }
                    }

                    #endregion

                    #endregion

                    string SERNO = "";
                    // 200090403 Smose Liao 執行預儲程序(SP_GetSerialNum)取得序號
                    strSQL = "EXEC SP_GetSerialNum_NEW 'GoodIssue', 'OTAUT'";
                    ControlHandleDB();
                    SERNO = ControlSqlAccess.GetFieldValue(strSQL);
                    ControlSqlAccess.CloseConnection();

                    // 200090724 Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                    //strSERNO = objStorageData.GetGrrno("GoodIssue", "OTAUT");

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
                    if (strSERNO.Length < 3)
                    {
                        strSERNO = Convert.ToString("00") + SERNO;
                    }
                    else
                    {
                        strSERNO = SERNO;
                    }
                    CYear = Convert.ToString(dtnow.Year);
                    strGRRNO = CYear.Substring(2, 2) + CMonth + CDay + strSERNO;

                    int intCombineLocalTotal;

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        // 200090629 Smose Liao 每一筆資料均有一個項目編號，從1開始編號，如果小於10，前面加0作編號
                        if (i < 9)
                        {
                            Itemnum = Convert.ToString("0") + Convert.ToString(i + 1);
                        }
                        else
                        {
                            Itemnum = Convert.ToString(i + 1);
                        }

                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }

                        //arySQL.Add("update WHITM set BKQTY=BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'  and SERNO='" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                        //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                        //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and MENGE='0'  and SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "'");
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = strCrnam;
                        objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        //Add: 增加Lock Code與Inspection Lot當主鍵 by Smose Liao 20100127
                        alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        //Add: 增加Lock Code與Inspection Lot當主鍵 by Smose Liao 20100127
                        alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");
                        alConditions.Add(" MENGE='0'");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #region 新增撿料單(WHGRR)

                        //WHGRR 儲存出庫單資料 Smose Liao 20090701
                        //arySQL.Add("Insert into WHGRR(GRRNO, ITEMNUM, MANDT, WERKS, LGORT, LOCAT, MATNR, MBLNR, INSMK, CHARG, LIFNR, INDAT, KDMAT, BKQTY, ALQTY, BLACE, SERNO, LOCOD, INSPT, BARCODE, PRTYP, CTRLNM) " +
                        //    "values('" + strGRRNO + "','" + (i + 1) + "','" + dtStorage.Rows[i]["MANDT"].ToString() + "', '" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "','" +
                        //    dtStorage.Rows[i]["MATNR"].ToString() + "','" + dtStorage.Rows[i]["OMBLNR"].ToString() + "','" + dtStorage.Rows[i]["INSMK"].ToString() + "','" + dtStorage.Rows[i]["CHARG"].ToString() + "','" + dtStorage.Rows[i]["LIFNR"].ToString() + "','" + dtStorage.Rows[i]["INDAT"].ToString() + "','" +
                        //    dtStorage.Rows[i]["KDMAT"].ToString() + "'," + dtStorage.Rows[i]["MENGE"].ToString() + ", " + dtStorage.Rows[i]["ALQTY"].ToString() + ", " + (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString() + ",'" + dtStorage.Rows[i]["SERNO"].ToString() + "','" + dtStorage.Rows[i]["LOCOD"].ToString() + "','" +
                        //    dtStorage.Rows[i]["INSPT"].ToString() + "','" + (strGRRNO + (Itemnum)) + "', 0, 'OTAUT')");

                        //計算目前該料號的總庫存數量  Smose Liao 20100330
                        intCombineLocalTotal = 0;
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhgrr.ResetField();
                        objWhgrr.Grrno = strGRRNO;
                        objWhgrr.Itemnum = (i + 1).ToString();
                        objWhgrr.Mandt = UserData.Client;
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhgrr.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        //objWhgrr.Bkqty = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhgrr.Bkqty = intCombineLocalTotal.ToString();  //該料號的總庫存數量  Smose Liao 20100330
                        objWhgrr.Alqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhgrr.Blace = (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString();
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Barcode = strGRRNO + (Itemnum);
                        objWhgrr.Prtyp = "0";
                        objWhgrr.Ctrlnm = "OTAUT";

                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }
                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        DataRow[] combineRow;
                        int intCombine = 0;
                        combineRow = dtOutSource.Select("MANDT='" + dtOutSource.Rows[i]["MANDT"].ToString() + "'  and MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "' and ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombine += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }
                        //arySQL.Add("Update WHDWN set OTQTY=OTQTY +'" + dtOutSource.Rows[i]["ALQTY"].ToString() + "' where MANDT= '" + strMandt + "' and MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "' and ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY + " + dtOutSource.Rows[i]["ALQTY"].ToString();
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                        //aryCheckSQL.Add("select MBLNR + '--' + ZEILE  FROM WHDWN WHERE (MANDT= '" + strMandt + "') and (MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "') and (ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "') and (MENGE < OTQTY+ " + intCombine + ")");
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        alColumns.Add("MBLNR + '--' + ZEILE");
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        alConditions.Add(" MENGE < OTQTY+ " + intCombine + "");
                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        ControlHandleDB();
                        dtChkTmp = ControlSqlAccess.GetDataTable(aryCheckSQL[j].ToString());
                        ControlSqlAccess.CloseConnection();

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
                        ControlHandleDB();
                        ControlSqlAccess.TimeOut = 300;    //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
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
                return strGRRNO;

            }

            #endregion


            #region  儲存連線出庫資料(By DateCode)
            //=====================================================================================
            ////////////Summary by Smose Liao 20101206/////////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫資料(By DateCode)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString, strMandt, strWerks, strLgort, strMblnr, strProgid, strCrnam)
            /// bool boolCheck =obj.wsAddOnLineOutData_DateCode_New();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////
            public string wsAddOnLineOutData_DateCode_New(DataTable dtOutSource, DataTable dtStorage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "wsAddOnLineOutData_DateCode_New";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strTempLocat = "";
                string strSERNO = "";
                string strGRRNO = "";
                string CYear = "";
                string CMonth = "";
                string CDay = "";
                string Itemnum = "";
                bool bolReturn = false;

                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);
                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, SUM(MENGE + BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        sbSQL.Append(" GROUP BY MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG ");

                        ControlHandleDB();
                        dtTmpStock = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return ERRMSG;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return ERRMSG;
                        }
                    }

                    #endregion

                    #endregion


                    // 200090403 Smose Liao 執行預儲程序(SP_GetSerialNum)取得序號
                    strSQL = "EXEC SP_GetSerialNum 'GoodIssue', 'OTAUT'";
                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();

                    // 200090724 Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                    strSERNO = objStorageData.GetGrrno("GoodIssue", "OTAUT");
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
                    strGRRNO = CYear.Substring(2, 2) + CMonth + CDay + strSERNO;

                    int intCombineLocalTotal;

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        // 200090629 Smose Liao 每一筆資料均有一個項目編號，從1開始編號，如果小於10，前面加0作編號
                        if (i < 9)
                        {
                            Itemnum = Convert.ToString("0") + Convert.ToString(i + 1);
                        }
                        else
                        {
                            Itemnum = Convert.ToString(i + 1);
                        }

                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = strCrnam;
                        objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");
                        alConditions.Add(" MENGE='0'");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #region 新增撿料單(WHGRR)

                        //WHGRR 儲存出庫單資料 Smose Liao 20090701
                        //計算目前該料號的總庫存數量  Smose Liao 20100330
                        intCombineLocalTotal = 0;
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhgrr.ResetField();
                        objWhgrr.Grrno = strGRRNO;
                        objWhgrr.Itemnum = (i + 1).ToString();
                        objWhgrr.Mandt = UserData.Client;
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhgrr.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        objWhgrr.Bkqty = intCombineLocalTotal.ToString();  //該料號的總庫存數量  Smose Liao 20100330
                        objWhgrr.Alqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhgrr.Blace = (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString();
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Kostl = dtStorage.Rows[i]["KOSTL"].ToString();
                        objWhgrr.Grpid = dtStorage.Rows[i]["GRPID"].ToString();
                        objWhgrr.Barcode = strGRRNO + (Itemnum);
                        objWhgrr.Prtyp = "0";
                        objWhgrr.Ctrlnm = "OTAUT";

                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }
                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        DataRow[] combineRow;
                        int intCombine = 0;
                        combineRow = dtOutSource.Select("MANDT='" + dtOutSource.Rows[i]["MANDT"].ToString() + "'  and MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "' and ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombine += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY + " + dtOutSource.Rows[i]["ALQTY"].ToString();
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        alColumns.Add("MBLNR + '--' + ZEILE");
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        alConditions.Add(" MENGE < OTQTY+ " + intCombine + "");
                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        ControlHandleDB();
                        dtChkTmp = ControlSqlAccess.GetDataTable(aryCheckSQL[j].ToString());
                        ControlSqlAccess.CloseConnection();

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
                        ControlHandleDB();
                        ControlSqlAccess.TimeOut = 300;    //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
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
                return strGRRNO;

            }

            #endregion


            #region  儲存連線出庫確認的資料(兩階段出庫)
            //===================================================================================
            ////////////Summary by Smose Liao 20100915///////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫確認的資料(兩階段出庫)
            /// </summary> 
            /// <param name="dtStorage">庫存資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddOnLineOutData_TwoPhaseConfirm(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddOnLineOutData_TwoPhaseConfirm(DataTable dtStorage)
            {
                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                string strTempLocat = "";
                bool bolUpdate = false;

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhgrr objWhgrr = new DataWhgrr(UserData);

                //WHLOG
                QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                arySQL = objLogData.AddLogData("", "", dtStorage);

                //WHITM
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                    }

                    //更新WHITM之MENGE欄位的數量(實際扣帳的動作)
                    if (objStorageData.QueryDataExistsInStorage_DateCode(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["OMBLN"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), dtStorage.Rows[i]["DACOD"].ToString()))
                    {

                        #region 新出庫確認程式  Smose Liao 20091008
                        //修改為WHITM.MENGE - WHGRR.ALQY and WHITM.BKQTY + WHGRR.ALQTY 
                        //WHGRR.ALQTY 為實際出庫數量
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Bkqty = "BKQTY + " + "(" + dtStorage.Rows[i]["ALQTY"].ToString() + ")";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        //alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                        //alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                        //alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        #endregion

                        bolUpdate = true;
                    }
                    else
                    {
                        ERRMSG = "這筆Barcode不存在，請確認!!";
                        bolUpdate = false;
                        break;
                    }
                }

                bool bolReturn = false;
                try
                {
                    //如果出庫單扣帳成功，將WHGRR的PRTYP狀態變更為已處理('1') 
                    if (bolUpdate == true)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {
                            objWhgrr.ResetField();
                            alConditions.Clear();
                            objWhgrr.Prtyp = "1";
                            alConditions.Add(" GRRNO='" + dtStorage.Rows[i]["GRRNO"].ToString() + "'");
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            //alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            //alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                            //alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");

                            arySQL.Add(objWhgrr.EntityGetUpdateSql(alConditions));

                            //刪掉庫存數量(Menge)為0的料號
                            objWhitm.ResetField();
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLN"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            //alConditions.Add(" DACOD='" + dtStorage.Rows[i]["DACOD"].ToString() + "'");
                            //alConditions.Add(" LOCOD='" + dtStorage.Rows[i]["LOCOD"].ToString() + "'");
                            //alConditions.Add(" INSPT='" + dtStorage.Rows[i]["INSPT"].ToString() + "'");
                            alConditions.Add(" MENGE= 0 ");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }

                        //WHHED
                        #region "Update WHHED Table"
                        for (int i = 0; i < aryLocat.Count; i++)
                        {
                            arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                            //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and MRGID<>'') as T where MANDT= '" + MANDT + "' and comcd='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                        }
                        #endregion

                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                    else
                    {
                        ERRMSG = "扣帳未成功，請確認!!";
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
                return bolReturn;
            }

            #endregion


            #region 儲存連線出庫(兩階段出庫)的資料 by Smose Liao 20100913
            //====================================================================================
            ////////////Summary by Smose Liao 20100913////////////////////////////////////////////
            /// <summary>
            /// 儲存連線出庫(兩階段出庫)的資料
            /// </summary> 
            /// <returns>
            /// string。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddTwoPhaseOnLineOutData(dtOutSource, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public string AddTwoPhaseOnLineOutData(DataTable dtOutSource, DataTable dtStorage)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddTwoPhaseOnLineOutData";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strTempLocat = "";
                string strSERNO = "";
                string strGRRNO = "";
                string CYear = "";
                string CMonth = "";
                string CDay = "";
                string Itemnum = "";
                bool bolReturn = false;

                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);
                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, SUM(MENGE + BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        sbSQL.Append(" GROUP BY MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG ");

                        ControlHandleDB();
                        dtTmpStock = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddTwoPhaseOnLineOutData()";
                                return ERRMSG;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddTwoPhaseOnLineOutData()";
                            return ERRMSG;
                        }
                    }

                    #endregion

                    #endregion


                    // 200090403 Smose Liao 執行預儲程序(SP_GetSerialNum)取得序號
                    strSQL = "EXEC SP_GetSerialNum 'QCI_TH20', 'Simulation'";
                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();

                    // 200090724 Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                    strSERNO = objStorageData.GetGrrno("QCI_TH20", "Simulation");
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
                    strGRRNO = CYear.Substring(2, 2) + CMonth + CDay + strSERNO;

                    int intCombineLocalTotal;

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        // 200090629 Smose Liao 每一筆資料均有一個項目編號，從1開始編號，如果小於10，前面加0作編號
                        if (i < 9)
                        {
                            Itemnum = Convert.ToString("0") + Convert.ToString(i + 1);
                        }
                        else
                        {
                            Itemnum = Convert.ToString(i + 1);
                        }

                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = strCrnam;
                        objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" MENGE='0'");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #region 新增撿料單(WHGRR)

                        //計算目前該料號的總庫存數量  Smose Liao 20100330
                        intCombineLocalTotal = 0;
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhgrr.ResetField();
                        objWhgrr.Grrno = strGRRNO;
                        objWhgrr.Itemnum = (i + 1).ToString();
                        objWhgrr.Mandt = UserData.Client;
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Zeile = dtStorage.Rows[i]["ZEILE"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        objWhgrr.Bkqty = intCombineLocalTotal.ToString();      //該料號的總庫存數量
                        objWhgrr.Alqty = dtStorage.Rows[i]["ALQTY"].ToString(); //總需求量
                        objWhgrr.Rndqty = dtStorage.Rows[i]["RLQTY"].ToString(); //卷數
                        objWhgrr.Blace = (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString();
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Barcode = strGRRNO + (Itemnum);
                        objWhgrr.Prtyp = "0";
                        objWhgrr.Ctrlnm = "Simulation";
                        objWhgrr.Grpid = dtStorage.Rows[i]["GRPID"].ToString(); //Group ID
                        objWhgrr.Kostl = dtStorage.Rows[i]["KOSTL"].ToString(); //Cost Center

                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }
                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        DataRow[] combineRow;
                        int intCombine = 0;
                        combineRow = dtOutSource.Select("MANDT='" + dtOutSource.Rows[i]["MANDT"].ToString() + "'  and MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "' and ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombine += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY + " + dtOutSource.Rows[i]["ALQTY"].ToString();
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                        alColumns.Clear();
                        alConditions.Clear();
                        objWhdwn.ResetField();
                        alColumns.Add("MBLNR + '--' + ZEILE");
                        alConditions.Add(" MANDT='" + strMandt + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" MBLNR='" + dtOutSource.Rows[i]["MBLNR"].ToString() + "'");
                        alConditions.Add(" ZEILE='" + dtOutSource.Rows[i]["ZEILE"].ToString() + "'");
                        alConditions.Add(" MENGE < OTQTY+ " + intCombine + "");
                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        ControlHandleDB();
                        dtChkTmp = ControlSqlAccess.GetDataTable(aryCheckSQL[j].ToString());
                        ControlSqlAccess.CloseConnection();

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
                        ControlHandleDB();
                        ControlSqlAccess.TimeOut = 300;    //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
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
                return strGRRNO;

            }

            #endregion


            #region 連線出庫(成品)
            //==========================================================================================================
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 連線出庫(成品)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOnLineOutData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddOnLineOutData(DataTable dtOutSource, DataTable dtStorage, string txtLifnr, string txtRmano)
            {
                #region 變數宣告
                bool bolReturn = false;
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhbox objWhbox = new DataWhbox(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                #endregion
                try
                {
                    #region WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);
                    #endregion
                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    //成品
                    alKeys.Add("SERNO");
                    //DateCode
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        if (txtLifnr.Trim() != "")
                            sbSQL.Append("   AND (LIFNR = '" + txtLifnr.Trim() + "')");

                        if (txtRmano.Trim() != "")
                            sbSQL.Append("   AND (RMANO = '" + txtRmano.Trim() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion

                    #region WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }
                        #region 先Update數量
                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Refid = dtStorage.Rows[i]["REFID"].ToString();
                        objWhitm.Seqno = dtStorage.Rows[i]["SEQNO"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //成品
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion
                        #region delete庫存為零的
                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //成品
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                        alConditions.Add("(MENGE=0)");
                        alConditions.Add("(BKQTY=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion
                    }
                    #endregion
                    #region WHBOX
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        alConditions.Clear();

                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

                        arySQL.Add(objWhbox.EntityGetDeleteSql(alConditions));
                    }
                    #endregion
                    #region WHHED
                    #region 處理WHHED Table Update 是否有庫存(LOSTS)及是否有連版(ISMRG)
                    #region 整理出會異動的Location
                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                            sbLocats.Append(" , ");
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }
                    #endregion
                    #region 先Update 有庫存的部分(Losts = 1)
                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #region 再Update 無庫存的部分(Losts = 0)
                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #region 先Update 有連版資料(ISMRG = Y)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "Y";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #region 再Update 無連版資料(ISMRG = N)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "N";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #endregion
                    #endregion
                    #region WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));

                        //and by Lora（同步调拨）
                        //arySQL.Add("EXEC sp_CreateTranserData  '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "','" + dtOutSource.Rows[i]["ZEILE"].ToString() + "' ");

                        #endregion
                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion
                    }
                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlExeSqlStringArr(arySQL);
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddOnLineOutData()";
                }

                return bolReturn;
            }
            #endregion

            #region 半成品出庫
            //=========================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 半成品出庫
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddSemiProductOutData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddSemiProductOutData(DataTable dtOutSource, DataTable dtStorage, string txtLifnr, string txtRmano)
            {
                bool bolReturn = false;
                try
                {
                    #region 變數宣告
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    #endregion

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    //成品
                    alKeys.Add("SERNO");
                    //DateCode
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        if (txtLifnr.Trim() != "")
                            sbSQL.Append("   AND (LIFNR = '" + txtLifnr.Trim() + "')");

                        if (txtRmano.Trim() != "")
                            sbSQL.Append("   AND (RMANO = '" + txtRmano.Trim() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion


                    //WHITM
                    //for (int i = 0; i < dtStorage.Rows.Count; i++)
                    //{
                    ////記錄所有的異動儲位
                    //if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                    //{
                    //    aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                    //}

                    //#region 先Update
                    //objWhitm.ResetField();
                    //objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                    //objWhitm.Refid = dtStorage.Rows[i]["REFID"].ToString();
                    //objWhitm.Seqno = dtStorage.Rows[i]["SEQNO"].ToString();
                    //objWhitm.Monam = UserData.UserId;
                    //objWhitm.Modat = "GetDate()";

                    //alConditions.Clear();
                    //alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                    //alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                    //alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                    //alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                    //alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                    //alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                    //alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                    //alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                    //alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                    ////成品
                    //alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                    ////DateCode
                    //alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                    //alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                    //alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                    //arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                    //#endregion
                    //#region delete庫存為零的
                    //alConditions.Clear();
                    //alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                    //alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                    //alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                    //alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                    //alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                    //alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                    //alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                    //alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                    //alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                    ////成品
                    //alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                    ////DateCode
                    //alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                    //alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                    //alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                    //alConditions.Add("(MENGE=0)");
                    //alConditions.Add("(BKQTY=0)");

                    //arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                    //#endregion
                    //}

                    //WHHED
                    #region 處理WHHED Table Update 是否有庫存(LOSTS)及是否有連版(ISMRG)

                    //#region 整理出會異動的Location
                    //StringBuilder sbLocats = new StringBuilder();
                    //sbLocats.Remove(0, sbLocats.Length);
                    //for (int i = 0; i < aryLocat.Count; i++)
                    //{
                    //    if (i != 0)
                    //        sbLocats.Append(" , ");
                    //    sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    //}
                    //#endregion
                    //#region 先Update 有庫存的部分(Losts = 1)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "1";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion
                    //#region 再Update 無庫存的部分(Losts = 0)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "0";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion
                    //#region 先Update 有連版資料(ISMRG = Y)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "Y";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion
                    //#region 再Update 無連版資料(ISMRG = N)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "N";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    #endregion

                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        //objWhdwn.ResetField();
                        //objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();

                        //alConditions.Clear();
                        //alConditions.Add("(MANDT= '" + MANDT + "')");
                        //alConditions.Add("(COMCD= '" + COMCD + "')");
                        //alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        //alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        //arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlExeSqlStringArr(arySQL);
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSemiProductOutData()";
                }

                return bolReturn;
            }
            #endregion

            #region 半成品出庫確認/无序号出库   未使用
            ////=================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///// <summary>
            ///// 半成品出庫確認/无序号出库
            ///// </summary> 
            ///// <returns>
            ///// bool。
            ///// </returns>
            ///// <example>
            ///// <code>
            ///// <remarks>
            ///// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            ///// bool boolCheck =obj.AddSemiProductOut_Confirm();
            /////  Your Code Here......
            ///// </remarks>
            ///// </code>
            ///// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //public bool AddSemiProductOut_Confirm(DataTable dtOutSource, DataTable dtStorage, bool bolNoScan)
            //{
            //    #region 變數宣告
            //    bool bolReturn = false;
            //    DataRow[] drFound;
            //    int intTotalOutQty = 0;
            //    ArrayList aryCheckSQL = new ArrayList();
            //    ArrayList aryCheckList = new ArrayList();
            //    StringBuilder sbCheckList = new StringBuilder();
            //    ArrayList arySQL = new ArrayList();
            //    ArrayList arySQL1 = new ArrayList();
            //    ArrayList aryLocat = new ArrayList();
            //    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
            //    DataWhhed objWhhed = new DataWhhed(UserData);
            //    DataWhitm objWhitm = new DataWhitm(UserData);
            //    DataWhdwn objWhdwn = new DataWhdwn(UserData);
            //    DataWhbox objWhbox = new DataWhbox(UserData);
            //    ArrayList alColumns = new ArrayList();
            //    ArrayList alConditions = new ArrayList();
            //    #endregion

            //    try
            //    {
            //        #region WHLOG
            //        arySQL = objLogData.AddLogData("", "", dtStorage);
            //        #endregion

            //        #region 檢查庫存是否足夠
            //        #region 合併DataTable
            //        //Whitm的Key
            //        ArrayList alKeys = new ArrayList();
            //        alKeys.Add("MANDT");
            //        alKeys.Add("COMCD");
            //        alKeys.Add("WERKS");
            //        alKeys.Add("LGORT");
            //        alKeys.Add("LOCAT");
            //        alKeys.Add("MATNR");
            //        alKeys.Add("INSMK");
            //        alKeys.Add("CHARG");
            //        alKeys.Add("MBLNR");

            //        Hashtable htCmpFields = new Hashtable();
            //        htCmpFields.Add("ALQTY", CmpAction.Sum);

            //        #region //brian add 20150321
            //        DataTable dtStorageCopy = dtStorage.Copy();
            //        foreach (DataRow drTemp in dtStorageCopy.Rows)
            //        {
            //            drTemp["MBLNR"] = "GB";
            //        }
            //        #endregion

            //        DataTable dtCombinStorage = CombineTable(dtStorageCopy, alKeys, htCmpFields);
            //        dtCombinStorage = CommonInfo.SortDataTable(dtCombinStorage, "LOCAT, MATNR");
            //        #endregion

            //        #region 檢查DB的庫存是否足夠
            //        StringBuilder sbSQL = new StringBuilder("");
            //        DataTable dtTmpStock;

            //        for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
            //        {
            //            sbSQL.Remove(0, sbSQL.Length);
            //            sbSQL.AppendLine("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SUM(MENGE) AS MENGE")
            //                .AppendLine("FROM WHITM with (nolock)")
            //                .AppendLine("WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')")
            //                .AppendLine("AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')")
            //                .AppendLine("AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')")
            //                .AppendLine("AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')")
            //                .AppendLine("AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')")
            //                .AppendLine("AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')")
            //                .AppendLine("AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')")
            //                .AppendLine("AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')")
            //                .AppendLine("AND (MBLNR = '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");

            //            sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR ");

            //            dtTmpStock = ControlQueryData(sbSQL.ToString());

            //            if (dtTmpStock.Rows.Count > 0)
            //            {
            //                if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
            //                {
            //                    ERRMSG = "庫存不足，請確認!! --> AddSemiProductOut_Confirm()";//+ dtCombinStorage.Rows[i]["MBLNR"].ToString();
            //                    return false;
            //                }
            //            }
            //            else
            //            {
            //                ERRMSG = "庫存不足，請確認!! --> AddSemiProductOut_Confirm()";//+ dtCombinStorage.Rows[i]["MBLNR"].ToString();
            //                return false;
            //            }
            //        }
            //        #endregion
            //        #endregion

            //        #region WHBOX
            //        if (!bolNoScan)
            //        {
            //            for (int i = 0; i < dtStorage.Rows.Count; i++)
            //            {
            //                #region //brian 20150224

            //                sbSQL.Remove(0, sbSQL.Length);
            //                sbSQL.AppendLine("INSERT INTO WHBOX_BAK")
            //                    .AppendLine(string.Format("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MBLNR, BOXID, INSMK, CHARG, MATNR, SERNO, 'Y' AS DELFLG, GETDATE() AS DELDAT, LOADID, KDMAT, MENGE, '{0}' AS DELDOC, CRTDAT FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)", dtStorage.Rows[i]["MBLNR"].ToString()))
            //                    .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
            //                    .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
            //                    .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
            //                    .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
            //                    .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
            //                    .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
            //                    .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["OMBLNR"].ToString()))
            //                    .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()))
            //                    .AppendLine(string.Format("AND (BOXID= '{0}')", dtStorage.Rows[i]["BOXID"].ToString()));

            //                if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
            //                {
            //                    sbSQL.AppendLine(string.Format("AND (SERNO= '{0}')", dtStorage.Rows[i]["SERNO"].ToString()));
            //                }

            //                arySQL.Add(sbSQL.ToString());

            //                //Add By Michael 20150527 for 记录出库时间
            //                sbSQL.Remove(0, sbSQL.Length);
            //                sbSQL.AppendLine("INSERT INTO WHOUT ")
            //                     .AppendLine(string.Format("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, '{1}' AS UMLGO, MBLNR, BOXID, CHARG, MATNR, SERNO, 'N', '{0}', GETDATE() FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)", UserData.UserId, dtOutSource.Rows[0]["UMLGO"].ToString().Trim()))
            //                     .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
            //                     .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
            //                     .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
            //                     .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
            //                     .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
            //                     .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
            //                     .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["OMBLNR"].ToString()))
            //                     .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()))
            //                     .AppendLine(string.Format("AND (BOXID= '{0}')", dtStorage.Rows[i]["BOXID"].ToString()));
            //                if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
            //                {
            //                    sbSQL.AppendLine(string.Format("AND (SERNO= '{0}')", dtStorage.Rows[i]["SERNO"].ToString()));
            //                }
            //                arySQL.Add(sbSQL.ToString());

            //                //brian change 20150301
            //                alConditions.Clear();
            //                alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
            //                alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
            //                alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
            //                alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
            //                alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
            //                alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
            //                alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
            //                alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
            //                alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

            //                if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
            //                {
            //                    alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
            //                }

            //                arySQL.Add(objWhbox.EntityGetDeleteSql(alConditions));

            //                #endregion
            //            }
            //        }
            //        #endregion

            //        #region WHITM
            //        for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
            //        {
            //            //記錄所有的異動儲位
            //            if (aryLocat.IndexOf(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
            //            {
            //                aryLocat.Add(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper());
            //            }
            //            ////找尋該票單據要出庫的數量
            //            //drFound = dtOutSource.Select("MATNR='" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "'");
            //            //for (int k = 0; k < drFound.Length; k++)
            //            //{
            //            //    intTotalOutQty += Int32.Parse(drFound[k]["MENGE"].ToString());
            //            //}

            //            ////brian 20150312
            //            //sbSQL.Remove(0, sbSQL.Length);
            //            //sbSQL.AppendLine("SELECT ISNULL(SUM(MENGE), 0) AS MENGE FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)")
            //            //    .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
            //            //    .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
            //            //    .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
            //            //    .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
            //            //    .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
            //            //    .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
            //            //    .AppendLine(string.Format("AND (INSMK= '{0}')", dtStorage.Rows[i]["INSMK"].ToString()))
            //            //    .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["MBLNR"].ToString()))
            //            //    .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()));

            //            #region 先Update
            //            objWhitm.ResetField();
            //            //objWhitm.Menge = "MENGE - " + intTotalOutQty;
            //            objWhitm.Menge = "MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
            //            //objWhitm.Menge = string.Format("({0})", sbSQL.ToString());//"MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
            //            objWhitm.Monam = UserData.UserId;
            //            objWhitm.Modat = "GetDate()";

            //            alConditions.Clear();
            //            alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
            //            alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
            //            alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
            //            alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
            //            alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
            //            alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
            //            alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
            //            alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
            //            alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");

            //            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

            //            #endregion

            //            #region delete庫存為零的
            //            alConditions.Clear();
            //            alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
            //            alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
            //            alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
            //            alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
            //            alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
            //            alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
            //            alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
            //            alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
            //            alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
            //            alConditions.Add("(MENGE=0)");

            //            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

            //            #endregion
            //        }
            //        #endregion

            //        #region WHHED
            //        #region 整理出會異動的Location
            //        StringBuilder sbLocats = new StringBuilder();
            //        sbLocats.Remove(0, sbLocats.Length);
            //        for (int i = 0; i < aryLocat.Count; i++)
            //        {
            //            if (i != 0)
            //            {
            //                sbLocats.Append(" , ");
            //            }
            //            sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
            //        }
            //        #endregion
            //        #region 先Update 有庫存的部分(Losts = 1)
            //        objWhhed.ResetField();
            //        objWhhed.Losts = "1";

            //        alConditions.Clear();
            //        alConditions.Add("( Exists (Select 'Y' From WHITM  with (nolock)  Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
            //        alConditions.Add("(MANDT= '" + strMandt + "')");
            //        alConditions.Add("(COMCD= '" + COMCD + "')");
            //        alConditions.Add("(WERKS= '" + strWerks + "')");
            //        alConditions.Add("(LGORT= '" + strLgort + "')");
            //        alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

            //        arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
            //        #endregion
            //        #region 再Update 無庫存的部分(Losts = 0)
            //        objWhhed.ResetField();
            //        objWhhed.Losts = "0";

            //        alConditions.Clear();
            //        alConditions.Add("( Not Exists (Select 'Y' From WHITM with (nolock) Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
            //        alConditions.Add("(MANDT= '" + strMandt + "')");
            //        alConditions.Add("(COMCD= '" + COMCD + "')");
            //        alConditions.Add("(WERKS= '" + strWerks + "')");
            //        alConditions.Add("(LGORT= '" + strLgort + "')");
            //        alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

            //        arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
            //        #endregion
            //        #endregion

            //        #region WHDWN
            //        for (int i = 0; i < dtOutSource.Rows.Count; i++)
            //        {
            //            #region 更新WHDWN
            //            objWhdwn.ResetField();
            //            objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALMNG"].ToString();//dtOutSource.Rows[i]["MENGE"].ToString();
            //            objWhdwn.Modat = "MODAT = GETDATE() ";

            //            alConditions.Clear();
            //            alConditions.Add("(MANDT= '" + MANDT + "')");
            //            alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
            //            alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
            //            alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
            //            alConditions.Add("(COMCD= '" + COMCD + "')");

            //            arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
            //            #endregion
            //            #region Double Check WHDWN的單據是不是有被處理過了
            //            alColumns.Clear();
            //            alColumns.Add(" MBLNR + '--' + ZEILE ");

            //            alConditions.Clear();
            //            alConditions.Add("(MANDT= '" + MANDT + "')");
            //            alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
            //            alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
            //            alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
            //            alConditions.Add("(COMCD= '" + COMCD + "')");
            //            alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALMNG"].ToString() + ")");//dtOutSource.Rows[i]["ALQTY"].ToString()

            //            aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
            //            #endregion

            //        }
            //        #region 防呆，避免重複處理同一張SAP Document
            //        DataTable dtChkTmp;

            //        for (int j = 0; j < aryCheckSQL.Count; j++)
            //        {
            //            dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
            //            if (dtChkTmp.Rows.Count > 0)
            //                aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
            //        }

            //        #endregion
            //        #endregion

            //        if (aryCheckList.Count > 0)
            //        {
            //            #region 防呆，避免重複處理同一張SAP Document

            //            sbCheckList.Remove(0, sbCheckList.Length);
            //            for (int j = 0; j < aryCheckList.Count; j++)
            //            {
            //                if (j != 0)
            //                    sbCheckList.Append(" , ");
            //                sbCheckList.Append(aryCheckList[j]);
            //            }
            //            ERRMSG = "Doc. No. " + sbCheckList.ToString() + " are processed. Please Check it.";
            //            #endregion
            //        }
            //        else
            //        {
            //            ControlHandleDB();
            //            bolReturn = ControlExeSqlStringArr(arySQL);
            //            ControlSqlAccess.CloseConnection();
            //        }
            //    }
            //    catch (System.Exception ex)
            //    {
            //        ERRMSG = ex.Message + "<- AddSemiProductOut_Confirm()";
            //    }

            //    return bolReturn;
            //}
            #endregion

            #region 半成品出庫確認/无序号出库
            //=================================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 半成品出庫確認/无序号出库
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddSemiProductOut_Confirm();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddSemiProductOut_Confirm(DataTable dtOutSource, DataTable dtStorage, bool bolNoScan)
            {
                #region 變數宣告
                bool bolReturn = false;
                DataRow[] drFound;
                int intTotalOutQty = 0;
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhbox objWhbox = new DataWhbox(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                #endregion

                try
                {
                    #region WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);
                    #endregion

                    #region 檢查庫存是否足夠
                    #region 合併DataTable
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("MBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);

                    #region //brian add 20150321
                    DataTable dtStorageCopy = dtStorage.Copy();
                    foreach (DataRow drTemp in dtStorageCopy.Rows)
                    {
                        drTemp["MBLNR"] = "GB";
                    }
                    #endregion

                    DataTable dtCombinStorage = CombineTable(dtStorageCopy, alKeys, htCmpFields);
                    dtCombinStorage = CommonInfo.SortDataTable(dtCombinStorage, "LOCAT, MATNR");
                    #endregion

                    #region 檢查DB的庫存是否足夠
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.AppendLine("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SUM(MENGE) AS MENGE")
                            .AppendLine("FROM WHITM with (nolock)")
                            .AppendLine("WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')")
                            .AppendLine("AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')")
                            .AppendLine("AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')")
                            .AppendLine("AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')")
                            .AppendLine("AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')")
                            .AppendLine("AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')")
                            .AppendLine("AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')")
                            .AppendLine("AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')")
                            .AppendLine("AND (MBLNR = '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "庫存不足，請確認!! --> AddSemiProductOut_Confirm()";//+ dtCombinStorage.Rows[i]["MBLNR"].ToString();
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "庫存不足，請確認!! --> AddSemiProductOut_Confirm()";//+ dtCombinStorage.Rows[i]["MBLNR"].ToString();
                            return false;
                        }
                    }
                    #endregion
                    #endregion

                    #region WHBOX
                    if (!bolNoScan)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {
                            #region //brian 20150224

                            sbSQL.Remove(0, sbSQL.Length);
                            sbSQL.AppendLine("INSERT INTO WHBOX_BAK")
                                .AppendLine(string.Format("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MBLNR, BOXID, INSMK, CHARG, MATNR, SERNO, 'Y' AS DELFLG, GETDATE() AS DELDAT, LOADID, KDMAT, MENGE, '{0}' AS DELDOC, CRTDAT FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)", dtStorage.Rows[i]["MBLNR"].ToString()))
                                .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
                                .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
                                .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
                                .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
                                .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
                                .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
                                .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["OMBLNR"].ToString()))
                                .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()))
                                .AppendLine(string.Format("AND (BOXID= '{0}')", dtStorage.Rows[i]["BOXID"].ToString()));

                            if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                            {
                                sbSQL.AppendLine(string.Format("AND (SERNO= '{0}')", dtStorage.Rows[i]["SERNO"].ToString()));
                            }

                            arySQL.Add(sbSQL.ToString());

                            //Add By Michael 20150527 for 记录出库时间
                            sbSQL.Remove(0, sbSQL.Length);
                            sbSQL.AppendLine("INSERT INTO WHOUT ")
                                 .AppendLine(string.Format("SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, '{1}' AS UMLGO, MBLNR, BOXID, CHARG, MATNR, SERNO, 'N', '{0}', GETDATE() FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)", UserData.UserId, dtOutSource.Rows[0]["UMLGO"].ToString().Trim()))
                                 .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
                                 .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
                                 .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
                                 .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
                                 .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
                                 .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
                                 .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["OMBLNR"].ToString()))
                                 .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()))
                                 .AppendLine(string.Format("AND (BOXID= '{0}')", dtStorage.Rows[i]["BOXID"].ToString()));
                            if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                            {
                                sbSQL.AppendLine(string.Format("AND (SERNO= '{0}')", dtStorage.Rows[i]["SERNO"].ToString()));
                            }
                            arySQL.Add(sbSQL.ToString());

                            //brian change 20150301
                            alConditions.Clear();
                            alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                            alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                            alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                            alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                            alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                            alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                            alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                            alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                            alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

                            if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                            {
                                alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                            }

                            arySQL.Add(objWhbox.EntityGetDeleteSql(alConditions));

                            #endregion
                        }
                    }
                    #endregion

                    #region WHITM
                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }
                        ////找尋該票單據要出庫的數量
                        //drFound = dtOutSource.Select("MATNR='" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "'");
                        //for (int k = 0; k < drFound.Length; k++)
                        //{
                        //    intTotalOutQty += Int32.Parse(drFound[k]["MENGE"].ToString());
                        //}

                        ////brian 20150312
                        //sbSQL.Remove(0, sbSQL.Length);
                        //sbSQL.AppendLine("SELECT ISNULL(SUM(MENGE), 0) AS MENGE FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)")
                        //    .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
                        //    .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
                        //    .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
                        //    .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
                        //    .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
                        //    .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
                        //    .AppendLine(string.Format("AND (INSMK= '{0}')", dtStorage.Rows[i]["INSMK"].ToString()))
                        //    .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["MBLNR"].ToString()))
                        //    .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()));

                        #region 先Update
                        objWhitm.ResetField();
                        //objWhitm.Menge = "MENGE - " + intTotalOutQty;
                        objWhitm.Menge = "MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
                        //objWhitm.Menge = string.Format("({0})", sbSQL.ToString());//"MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion

                        #region delete庫存為零的
                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(MENGE=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion
                    }
                    #endregion

                    #region WHHED
                    #region 整理出會異動的Location
                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                        {
                            sbLocats.Append(" , ");
                        }
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }
                    #endregion
                    #region 先Update 有庫存的部分(Losts = 1)
                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM  with (nolock)  Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #region 再Update 無庫存的部分(Losts = 0)
                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM with (nolock) Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #endregion

                    #region WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALMNG"].ToString();//dtOutSource.Rows[i]["MENGE"].ToString();
                        objWhdwn.Modat = "MODAT = GETDATE() ";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion
                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALMNG"].ToString() + ")");//dtOutSource.Rows[i]["ALQTY"].ToString()

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }
                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion
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
                        ControlHandleDB();
                        bolReturn = ControlExeSqlStringArr(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSemiProductOut_Confirm()";
                }

                return bolReturn;
            }
            #endregion

            #region 半成品调拨出庫確認
            //=================================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 半成品调拨出庫確認
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddSemiProductTransferOut_Confirm();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddSemiProductTransferOut_Confirm(DataTable dtOutSource, DataTable dtStorage)
            {
                #region 變數宣告
                bool bolReturn = false;
                DataRow[] drFound;
                int intTotalOutQty = 0;
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhbox objWhbox = new DataWhbox(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                #endregion

                try
                {
                    #region WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);
                    #endregion

                    #region 檢查庫存是否足夠
                    #region 合併DataTable
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("MBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);

                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    dtCombinStorage = CommonInfo.SortDataTable(dtCombinStorage, "LOCAT, MATNR");
                    #endregion

                    #region 檢查DB的庫存是否足夠
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SUM(MENGE) AS MENGE ");
                        sbSQL.Append(" FROM WHITM with (nolock) ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "庫存不足，請確認!! --> AddSemiProductTransferOut_Confirm()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "庫存不足，請確認!! --> AddSemiProductTransferOut_Confirm()";
                            return false;
                        }
                    }
                    #endregion
                    #endregion

                    #region WHBOX
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        #region //brian 20150224

                        //brian change 20150301
                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

                        if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                        {
                            alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        }

                        arySQL.Add(objWhbox.EntityGetDeleteSql(alConditions));

                        #endregion

                        objWhdwn.ResetField();
                        //objWhdwn.Ioflage = "T";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        //alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

                        if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                        {
                            alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        }

                        arySQL.Add(objWhdwn.EntityGetDeleteSql(alConditions));
                    }
                    #endregion

                    #region WHITM
                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        //brian 20150312
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.AppendLine("SELECT ISNULL(SUM(MENGE), 0) AS MENGE FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)")
                            .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
                            .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
                            .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
                            .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
                            .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
                            .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
                            .AppendLine(string.Format("AND (INSMK= '{0}')", dtStorage.Rows[i]["INSMK"].ToString()))
                            .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["MBLNR"].ToString()))
                            .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()));

                        #region 先Update
                        objWhitm.ResetField();
                        objWhitm.Menge = string.Format("({0})", sbSQL.ToString());//"MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion

                        #region delete庫存為零的
                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(MENGE=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion
                    }
                    #endregion

                    #region WHHED
                    #region 整理出會異動的Location
                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                        {
                            sbLocats.Append(" , ");
                        }
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }
                    #endregion

                    #region 先Update 有庫存的部分(Losts = 1)
                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM  with (nolock)  Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));

                    #endregion

                    #region 再Update 無庫存的部分(Losts = 0)
                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM with (nolock) Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion
                    #endregion

                    #region WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALMNG"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALMNG"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion

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
                        ControlHandleDB();
                        bolReturn = ControlExeSqlStringArr(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSemiProductTransferOut_Confirm()";
                }

                return bolReturn;
            }
            #endregion

            #region 半成品调拨出庫確認/无序号出库
            //=================================================================================================================
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 半成品调拨出庫確認/无序号出库
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddSemiProductTransferOut_Confirm();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddSemiProductTransferOut_Confirm(string strWerksFr, string strLgortFr, string strWerksTo, string strLgortTo, DataTable dtOutSource, DataTable dtStorage, bool bolNoScan)
            {
                #region 變數宣告
                bool bolReturn = false;
                DataRow[] drFound;
                int intTotalOutQty = 0;
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhbox objWhbox = new DataWhbox(UserData);
                DataWhtra objWhtra = new DataWhtra(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                #endregion

                try
                {
                    #region WHLOG

                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #endregion

                    #region 檢查庫存是否足夠

                    #region 合併DataTable

                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("MBLNR");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);

                    #region //brian add 20150321

                    DataTable dtStorageCopy = dtStorage.Copy();
                    foreach (DataRow drTemp in dtStorageCopy.Rows)
                    {
                        drTemp["MBLNR"] = "GB";
                    }

                    #endregion

                    DataTable dtCombinStorage = CombineTable(dtStorageCopy, alKeys, htCmpFields);
                    dtCombinStorage = CommonInfo.SortDataTable(dtCombinStorage, "LOCAT, MATNR");

                    #endregion

                    #region 檢查DB的庫存是否足夠

                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SUM(MENGE) AS MENGE ");
                        sbSQL.Append(" FROM WHITM with (nolock) ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "庫存不足，請確認!! --> AddSemiProductTransferOut_Confirm()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "庫存不足，請確認!! --> AddSemiProductTransferOut_Confirm()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion

                    #region WHBOX

                    if (!bolNoScan)
                    {
                        for (int i = 0; i < dtStorage.Rows.Count; i++)
                        {

                            sbSQL.Remove(0, sbSQL.Length);
                            sbSQL.AppendLine("INSERT INTO WHTRA")
                                .AppendLine(string.Format("SELECT MANDT, COMCD, WERKS, LGORT, '{0}' AS MBLNR, MBLNR AS PLTID, BOXID, SERNO, INSMK, CHARG, MATNR, MENGE, '{1}' AS DWERK, '{2}' AS DLGOR, '' AS DMBLN, '{3}' AS FLAGE, GETDATE() AS CRTDAT FROM WHBOX WITH (NOLOCK) WHERE (1 = 1)"
                                , dtStorage.Rows[i]["MBLNR"].ToString(), strWerksTo, strLgortTo, dtStorage.Rows[i]["FLAGE"].ToString()))
                                .AppendLine(string.Format("AND (MANDT= '{0}')", dtStorage.Rows[i]["MANDT"].ToString()))
                                .AppendLine(string.Format("AND (COMCD= '{0}')", dtStorage.Rows[i]["COMCD"].ToString()))
                                .AppendLine(string.Format("AND (WERKS= '{0}')", dtStorage.Rows[i]["WERKS"].ToString()))
                                .AppendLine(string.Format("AND (LGORT= '{0}')", dtStorage.Rows[i]["LGORT"].ToString()))
                                .AppendLine(string.Format("AND (LOCAT= '{0}')", dtStorage.Rows[i]["LOCAT"].ToString()))
                                .AppendLine(string.Format("AND (MATNR= '{0}')", dtStorage.Rows[i]["MATNR"].ToString()))
                                .AppendLine(string.Format("AND (MBLNR= '{0}')", dtStorage.Rows[i]["OMBLNR"].ToString()))
                                .AppendLine(string.Format("AND (CHARG= '{0}')", dtStorage.Rows[i]["CHARG"].ToString()))
                                .AppendLine(string.Format("AND (BOXID= '{0}')", dtStorage.Rows[i]["BOXID"].ToString()));

                            if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                            {
                                sbSQL.AppendLine(string.Format("AND (SERNO= '{0}')", dtStorage.Rows[i]["SERNO"].ToString()));
                            }

                            arySQL.Add(sbSQL.ToString());

                            #region //brian 20150224

                            //brian change 20150301
                            alConditions.Clear();
                            alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                            alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                            alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                            alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                            alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                            alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                            alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                            alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                            alConditions.Add("(BOXID= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");

                            if (!string.IsNullOrEmpty(dtStorage.Rows[i]["SERNO"].ToString()))
                            {
                                alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                            }

                            arySQL.Add(objWhbox.EntityGetDeleteSql(alConditions));

                            #endregion
                        }
                    }

                    #endregion

                    #region WHITM

                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtCombinStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        #region 先Update

                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + int.Parse(dtCombinStorage.Rows[i]["ALQTY"].ToString().Trim());
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion

                        #region delete庫存為零的

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtCombinStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(MENGE=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion
                    }

                    #endregion

                    #region WHHED

                    #region 整理出會異動的Location

                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                        {
                            sbLocats.Append(" , ");
                        }
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }

                    #endregion

                    #region 先Update 有庫存的部分(Losts = 1)

                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM  with (nolock)  Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));

                    #endregion

                    #region 再Update 無庫存的部分(Losts = 0)

                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM with (nolock) Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));

                    #endregion

                    #endregion

                    #region WHDWN

                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        if (bolNoScan)
                        {
                            objWhtra.ResetField();
                            objWhtra.MANDT = dtOutSource.Rows[i]["MANDT"].ToString().Trim();
                            objWhtra.COMCD = dtOutSource.Rows[i]["COMCD"].ToString().Trim();
                            objWhtra.WERKS = dtOutSource.Rows[i]["WERKS"].ToString().Trim();
                            objWhtra.LGORT = dtOutSource.Rows[i]["LGORT"].ToString().Trim();
                            objWhtra.MBLNR = dtOutSource.Rows[i]["MBLNR"].ToString().Trim();
                            objWhtra.INSMK = dtOutSource.Rows[i]["INSMK"].ToString().Trim();
                            objWhtra.CHARG = dtOutSource.Rows[i]["CHARG"].ToString().Trim();
                            objWhtra.MATNR = dtOutSource.Rows[i]["MATNR"].ToString().Trim();
                            objWhtra.MENGE = dtOutSource.Rows[i]["ALMNG"].ToString().Trim();
                            objWhtra.DWERK = strWerksTo;
                            objWhtra.DLGOR = strLgortTo;
                            objWhtra.CRDAT = "GETDATE()";

                            arySQL.Add(objWhtra.EntityGetInsertSql());
                        }

                        #region 更新WHDWN

                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALMNG"].ToString();
                        objWhdwn.Modat = "MODAT = GETDATE() ";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));


                        //add 调拨流程
                        //20170103 add by karen 新增调拨单据
                        //arySQL.Add(" EXEC sp_CreateTranserData '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "','" + dtOutSource.Rows[i]["ZEILE"].ToString() + "' ");


                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了

                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(MTYPE= '" + dtOutSource.Rows[i]["MTYPE"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALMNG"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));

                        #endregion
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion

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
                        ControlHandleDB();
                        bolReturn = ControlExeSqlStringArr(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSemiProductTransferOut_Confirm()";
                }

                return bolReturn;
            }
            #endregion

            #region 儲存SI連線出庫資料 by Smose Liao 20100420
            //========================================================================
            ////////////Summary by Marc Hong//////////////////////////////////////////
            /// <summary>
            /// 儲存SI連線出庫資料
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString, strMandt, strWerks, strLgort, strMblnr, strProgid, strCrnam)
            /// bool boolCheck =obj.AddSIOnLineOutData(dtOutSource, dtStorage, dtCombineStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////
            public bool AddSinoOnLineOutData(DataTable dtOutSource, DataTable dtStorage, DataTable dtCombineStorage)
            {
                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);


                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    alKeys.Add("SERNO");
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        //if (txtLifnr.Trim() != "")
                        //    sbSQL.Append("   AND (LIFNR = '" + txtLifnr.Trim() + "')");

                        //if (txtRmano.Trim() != "")
                        //    sbSQL.Append("   AND (RMANO = '" + txtRmano.Trim() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion


                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        //記錄所有的異動待出貨儲位(NLOCA)
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["NLOCA"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["NLOCA"].ToString().ToUpper());
                        }

                        #region Update原先的庫存數量
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion

                        #region delete庫存為零的庫存
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        alConditions.Add("(MENGE=0)");
                        alConditions.Add("(BKQTY=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion

                    }

                    #region 將庫存移到待出貨儲位
                    //QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        //    if (objStorageData.QueryDataExistsInStorage_SpareParts(dtCombineStorage.Rows[i]["LOCAT"].ToString(), dtCombineStorage.Rows[i]["MATNR"].ToString(), dtCombineStorage.Rows[i]["INSMK"].ToString(), dtCombineStorage.Rows[i]["CHARG"].ToString()))
                        //    {
                        //        //已經有庫存，Update
                        //        alColumns.Clear();
                        //        alConditions.Clear();
                        //        objWhitm.ResetField();
                        //        objWhitm.Menge = "MENGE + " + dtStorage.Rows[i]["ALQTY"].ToString();
                        //        objWhitm.Monam = UserData.UserId;
                        //        objWhitm.Modat = "GetDate()";

                        //        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        //        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        //        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        //        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        //        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        //        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        //        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        //        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");

                        //        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                        //    }
                        //    else
                        //    {
                        //已經有庫存，Update
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Mandt = dtCombineStorage.Rows[i]["MANDT"].ToString();
                        objWhitm.Comcd = dtCombineStorage.Rows[i]["COMCD"].ToString();
                        objWhitm.Werks = dtCombineStorage.Rows[i]["WERKS"].ToString();
                        objWhitm.Lgort = dtCombineStorage.Rows[i]["LGORT"].ToString();
                        //objWhitm.Locat = "";
                        objWhitm.Locat = dtCombineStorage.Rows[i]["NLOCA"].ToString();  //待出貨儲位
                        objWhitm.Nloca = dtCombineStorage.Rows[i]["NLOCA"].ToString();  //待出貨儲位
                        objWhitm.Matnr = dtCombineStorage.Rows[i]["MATNR"].ToString();
                        objWhitm.Insmk = dtCombineStorage.Rows[i]["INSMK"].ToString();
                        objWhitm.Sidno = dtCombineStorage.Rows[i]["MBLNR"].ToString();  //SI單據
                        objWhitm.Mblnr = dtCombineStorage.Rows[i]["OMBLNR"].ToString();
                        objWhitm.Charg = dtCombineStorage.Rows[i]["CHARG"].ToString();
                        objWhitm.Lifnr = dtCombineStorage.Rows[i]["LIFNR"].ToString();
                        objWhitm.Indat = dtCombineStorage.Rows[i]["INDAT"].ToString();
                        objWhitm.Ebeln = dtCombineStorage.Rows[i]["EBELN"].ToString();
                        objWhitm.Kdmat = dtCombineStorage.Rows[i]["KDMAT"].ToString();
                        objWhitm.Menge = dtCombineStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Serno = dtCombineStorage.Rows[i]["SERNO"].ToString();
                        objWhitm.Qcqty = "0";
                        objWhitm.Refno = "";
                        objWhitm.Mrgid = "";
                        objWhitm.Isptm = "";
                        objWhitm.Rmak1 = dtCombineStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Crnam = CRNAM;
                        objWhitm.Crdat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";

                        arySQL.Add(objWhitm.EntityGetInsertSql());
                        //    }
                    }
                    #endregion

                    //WHHED
                    #region 處理WHHED Table Update 是否有庫存(LOSTS)

                    #region 整理出會異動的Location
                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                            sbLocats.Append(" , ");
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }
                    #endregion

                    #region 先Update 有庫存的部分(Losts = 1)
                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion

                    #region 再Update 無庫存的部分(Losts = 0)
                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion

                    #endregion

                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();
                        objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlExeSqlStringArr(arySQL);
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSinoOnLineOutData()";
                }
                return bolReturn;

            }


            #endregion


            #region 儲存SI連線出庫資料(DOA) by Smose Liao 20110125
            //========================================================================
            ////////////Summary by Smose Liao 20110125///////////////////////////////////////////////////////
            /// <summary>
            /// 儲存SI連線出庫資料(DOA)
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString, strMandt, strWerks, strLgort, strMblnr, strProgid, strCrnam)
            /// bool boolCheck =obj.AddDOASILineOutData(dtOutSource, dtStorage, dtCombineStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            public bool AddDOASILineOutData(DataTable dtOutSource, DataTable dtStorage, DataTable dtCombineStorage)
            {
                bool bolReturn = false;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);


                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    alKeys.Add("SERNO");
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion


                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        //記錄所有的異動待出貨儲位(NLOCA)
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["NLOCA"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["NLOCA"].ToString().ToUpper());
                        }

                        #region Update原先的庫存數量
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(SERNO= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                        #endregion

                        #region delete庫存為零的庫存
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        alConditions.Add("(MENGE=0)");
                        alConditions.Add("(BKQTY=0)");

                        arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        #endregion

                    }

                    #region 將Combine料號後的庫存資料移到待出貨儲位
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhitm.ResetField();
                        objWhitm.Mandt = dtCombineStorage.Rows[i]["MANDT"].ToString();
                        objWhitm.Comcd = dtCombineStorage.Rows[i]["COMCD"].ToString();
                        objWhitm.Werks = dtCombineStorage.Rows[i]["WERKS"].ToString();
                        objWhitm.Lgort = dtCombineStorage.Rows[i]["LGORT"].ToString();
                        objWhitm.Locat = dtCombineStorage.Rows[i]["NLOCA"].ToString();  //待出貨儲位
                        objWhitm.Nloca = dtCombineStorage.Rows[i]["NLOCA"].ToString();  //待出貨儲位
                        objWhitm.Matnr = dtCombineStorage.Rows[i]["MATNR"].ToString();
                        objWhitm.Insmk = dtCombineStorage.Rows[i]["INSMK"].ToString();
                        objWhitm.Sidno = dtCombineStorage.Rows[i]["MBLNR"].ToString();  //SI單據
                        objWhitm.Mblnr = dtCombineStorage.Rows[i]["OMBLNR"].ToString();
                        objWhitm.Charg = dtCombineStorage.Rows[i]["CHARG"].ToString();
                        objWhitm.Lifnr = dtCombineStorage.Rows[i]["LIFNR"].ToString();
                        objWhitm.Indat = dtCombineStorage.Rows[i]["INDAT"].ToString();
                        objWhitm.Ebeln = dtCombineStorage.Rows[i]["EBELN"].ToString();
                        objWhitm.Kdmat = dtCombineStorage.Rows[i]["KDMAT"].ToString();
                        objWhitm.Menge = dtCombineStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Serno = dtCombineStorage.Rows[i]["SERNO"].ToString();
                        objWhitm.Rmano = dtCombineStorage.Rows[i]["RMANO"].ToString();
                        objWhitm.Qcqty = "0";
                        objWhitm.Refno = "";
                        objWhitm.Mrgid = "";
                        objWhitm.Isptm = "";
                        objWhitm.Rmak1 = dtCombineStorage.Rows[i]["RMAK1"].ToString();
                        objWhitm.Crnam = CRNAM;
                        objWhitm.Crdat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                        objWhitm.Monam = CRNAM;
                        objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";

                        arySQL.Add(objWhitm.EntityGetInsertSql());
                    }
                    #endregion

                    //WHHED
                    #region 處理WHHED Table Update 是否有庫存(LOSTS)

                    #region 整理出會異動的Location
                    StringBuilder sbLocats = new StringBuilder();
                    sbLocats.Remove(0, sbLocats.Length);
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        if (i != 0)
                            sbLocats.Append(" , ");
                        sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    }
                    #endregion

                    #region 先Update 有庫存的部分(Losts = 1)
                    objWhhed.ResetField();
                    objWhhed.Losts = "1";

                    alConditions.Clear();
                    alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion

                    #region 再Update 無庫存的部分(Losts = 0)
                    objWhhed.ResetField();
                    objWhhed.Losts = "0";

                    alConditions.Clear();
                    alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    alConditions.Add("(MANDT= '" + strMandt + "')");
                    alConditions.Add("(COMCD= '" + COMCD + "')");
                    alConditions.Add("(WERKS= '" + strWerks + "')");
                    alConditions.Add("(LGORT= '" + strLgort + "')");
                    alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion

                    #endregion

                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();
                        objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlExeSqlStringArr(arySQL);
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddDOASILineOutData()";
                }
                return bolReturn;

            }


            #endregion


            #region 連線出庫資料兩段式(先產生撿料單) by Marc Hong
            //=========================================================================
            ////////////Summary by Marc Hong////////////////////////////////////////////
            /// <summary>
            /// 連線出庫資料兩段式(先產生撿料單) by Marc Hong
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddOnLineOutData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public string AddOutData_PickUp(DataTable dtOutSource, DataTable dtStorage, DataTable dtCombineStorage, string txtLifnr, string txtRmano)  // Quanta, Smose.Liao, 20090225：Add the function of Online Goods Issue by RMA No.(連線出庫依據RMA NO.進行出庫)
            {
                string strResult = "";
                try
                {

                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();

                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();

                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);

                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string CYear = "";
                    string CMonth = "";
                    string CDay = "";

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region 取得撿料單號

                    //判斷是否為CSMC的成品出庫  Smose Liao 20100322
                    string strSql = "";
                    if (dtOutSource.Rows[0]["MTYPE"].ToString() == "SDS" && dtOutSource.Rows[0]["COMCD"].ToString() == "9700")
                    {
                        strSql = "EXEC SP_GetSerialNum 'GoodIssue', 'FGAUT'";
                    }
                    else
                    {
                        strSql = "EXEC SP_GetSerialNum 'GoodIssue', 'OTAUT'";
                    }

                    //string strSql = "EXEC SP_GetSerialNum 'GoodIssue', 'OTAUT'";
                    ControlHandleDB();
                    string strPickNo = ControlSqlAccess.GetFieldValue(strSql);
                    ControlSqlAccess.CloseConnection();

                    //20100115  Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號 
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    //判斷是否為CSMC的成品出庫  Smose Liao 20100322
                    //strPickNo = objStorageData.GetGrrno("GoodIssue", "OTAUT");
                    if (dtOutSource.Rows[0]["MTYPE"].ToString() == "SDS" && dtOutSource.Rows[0]["COMCD"].ToString() == "9700")
                    {
                        strPickNo = objStorageData.GetGrrno("GoodIssue", "FGAUT");
                    }
                    else
                    {
                        strPickNo = objStorageData.GetGrrno("GoodIssue", "OTAUT");
                    }

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
                    //strGRRNO = dtnow.Year + CMonth + CDay + strSERNO; 
                    CYear = Convert.ToString(dtnow.Year);
                    strPickNo = CYear.Substring(2, 2) + CMonth + CDay + strPickNo;

                    #endregion

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    //成品
                    alKeys.Add("SERNO");
                    //DateCode
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);
                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        //sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, SERNO, DACOD, LOCOD, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        //sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        //sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        if (txtLifnr.Trim() != "")
                            sbSQL.Append("   AND (LIFNR = '" + txtLifnr.Trim() + "')");

                        if (txtRmano.Trim() != "")
                            sbSQL.Append("   AND (RMANO = '" + txtRmano.Trim() + "')");

                        //sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");
                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, SERNO, DACOD, LOCOD ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return "";
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return "";
                        }
                    }

                    #endregion

                    #endregion


                    int intCombineLocalTotal;

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        #region 老方法   未使用
                        //if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        //{//如果庫存剛好全出光，則砍掉庫存紀錄

                        //    alConditions.Clear();
                        //    alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        //    alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        //    alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        //    alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        //    alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        //    alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        //    alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        //    alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //    alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //    //alConditions.Add("(LIFNR= '" + txtLifnr.Trim() + "')");
                        //    //alConditions.Add("(RMANO= '" + txtRmano.Trim() + "')");

                        //    arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));

                        //}
                        //else
                        //{//如果庫存還有剩，則更新庫存紀錄
                        #endregion

                        #region //先Update
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //成品
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));


                        #region 新增撿料單(WHGRR)

                        //計算目前該料號的總庫存數量  Smose Liao 20100330
                        intCombineLocalTotal = 0;
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtStorage.Rows[i]["LOCAT"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["INSMK"].ToString(), dtStorage.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                        objWhgrr.ResetField();
                        objWhgrr.Grrno = strPickNo;
                        objWhgrr.Itemnum = ((int)(i + 1)).ToString("000");
                        objWhgrr.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhgrr.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        //objWhgrr.Bkqty = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhgrr.Bkqty = intCombineLocalTotal.ToString();  //該料號的總庫存數量  Smose Liao 20100330
                        objWhgrr.Alqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhgrr.Blace = (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString();
                        objWhgrr.Barcode = strPickNo + ((int)(i + 1)).ToString("000");
                        objWhgrr.Prtyp = "0";

                        //判斷是否為CSMC的成品出庫  Smose Liao 20100322
                        //objWhgrr.Ctrlnm = "OTAUT";
                        if (dtStorage.Rows[i]["MTYPE"].ToString() == "SDS" && dtStorage.Rows[i]["COMCD"].ToString() == "9700")
                        {
                            objWhgrr.Ctrlnm = "FGAUT";
                        }
                        else
                        {
                            objWhgrr.Ctrlnm = "OTAUT";
                        }

                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion

                        #endregion
                    }

                    //WHHED

                    #region 處理WHHED Table Update 是否有庫存(LOSTS)及是否有連版(ISMRG)

                    #region 整理出會異動的Location   未使用
                    //StringBuilder sbLocats = new StringBuilder();
                    //sbLocats.Remove(0, sbLocats.Length);
                    //for (int i = 0; i < aryLocat.Count; i++)
                    //{
                    //    if (i != 0)
                    //        sbLocats.Append(" , ");
                    //    sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    //}
                    //#endregion


                    //#region 先Update 有庫存的部分(Losts = 1)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "1";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 再Update 無庫存的部分(Losts = 0)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "0";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 先Update 有連版資料(ISMRG = Y)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "Y";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 再Update 無連版資料(ISMRG = N)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "N";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    #endregion

                    #endregion

                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bool bolReturn = ControlExeSqlStringArr(arySQL);
                        if (bolReturn)
                            strResult = strPickNo;
                    }
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- UpdateUserPassword "  ;
                    //throw new System.Exception(ex.Message +"<- UpdateUserPassword ");

                    ERRMSG = ex.Message + "<- AddOnLineOutData()";
                }
                return strResult;

            }


            #endregion


            #region 成品連線出庫資料兩段式(先產生撿料單) by Smose Liao 20100401
            //====================================================================================
            ////////////Summary by Smose Liao 20100401////////////////////////////////////////////
            /// <summary>
            /// 成品連線出庫資料兩段式(先產生撿料單) by Smose Liao 20100401
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddProductOutData_PickUp();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////
            public string AddProductOutData_PickUp(DataTable dtOutSource, DataTable dtStorage, DataTable dtCombineStorage, string txtLifnr, string txtRmano)
            {
                string strResult = "";
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string CYear = "";
                    string CMonth = "";
                    string CDay = "";

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region 取得撿料單號

                    string strSql = "EXEC SP_GetSerialNum 'GoodIssue', 'FGAUT'";
                    ControlHandleDB();
                    string strPickNo = ControlSqlAccess.GetFieldValue(strSql);
                    ControlSqlAccess.CloseConnection();

                    //20100115  Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號 
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    strPickNo = objStorageData.GetGrrno("GoodIssue", "FGAUT");

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
                    //strGRRNO = dtnow.Year + CMonth + CDay + strSERNO; 
                    CYear = Convert.ToString(dtnow.Year);
                    strPickNo = CYear.Substring(2, 2) + CMonth + CDay + strPickNo;

                    #endregion

                    #region "檢查庫存是否足夠"

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    //成品
                    alKeys.Add("SERNO");
                    //DateCode
                    alKeys.Add("DACOD");
                    alKeys.Add("LOCOD");
                    alKeys.Add("INSPT");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region "查db庫存是否足夠"
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        //成品
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        sbSQL.Append("   AND (ISNULL(DACOD,'') = '" + dtCombinStorage.Rows[i]["DACOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(LOCOD,'') = '" + dtCombinStorage.Rows[i]["LOCOD"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(INSPT,'') = '" + dtCombinStorage.Rows[i]["INSPT"].ToString() + "')");

                        if (txtLifnr.Trim() != "")
                            sbSQL.Append("   AND (LIFNR = '" + txtLifnr.Trim() + "')");

                        if (txtRmano.Trim() != "")
                            sbSQL.Append("   AND (RMANO = '" + txtRmano.Trim() + "')");

                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, DACOD, LOCOD, INSPT ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                                return "";
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重複出庫，請重新查詢!!(Please Query Again!!) <- AddOnLineOutData()";
                            return "";
                        }
                    }

                    #endregion

                    #endregion

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        #region //先Update
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //成品
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));


                        #region 新增撿料單(WHGRR)

                        objWhgrr.ResetField();
                        //objWhgrr.Grrno = strPickNo;
                        objWhgrr.Grrno = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Itemnum = ((int)(i + 1)).ToString("000");
                        objWhgrr.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["OMBLNR"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Serno = dtStorage.Rows[i]["SERNO"].ToString();
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        objWhgrr.Bkqty = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhgrr.Alqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhgrr.Blace = (int.Parse(dtStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtStorage.Rows[i]["ALQTY"].ToString())).ToString();
                        objWhgrr.Barcode = strPickNo + ((int)(i + 1)).ToString("000");
                        objWhgrr.Prtyp = "0";
                        objWhgrr.Ctrlnm = "FGAUT";

                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion

                        #endregion
                    }

                    #region 新增撿料單(WHGRR)
                    //for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    //{
                    //    objWhgrr.ResetField();
                    //    objWhgrr.Grrno = strPickNo;
                    //    objWhgrr.Itemnum = ((int)(i + 1)).ToString("000");
                    //    objWhgrr.Mandt = dtCombineStorage.Rows[i]["MANDT"].ToString();
                    //    objWhgrr.Comcd = dtCombineStorage.Rows[i]["COMCD"].ToString();
                    //    objWhgrr.Werks = dtCombineStorage.Rows[i]["WERKS"].ToString();
                    //    objWhgrr.Lgort = dtCombineStorage.Rows[i]["LGORT"].ToString();
                    //    objWhgrr.Locat = dtCombineStorage.Rows[i]["LOCAT"].ToString();
                    //    objWhgrr.Mblnr = dtCombineStorage.Rows[i]["MBLNR"].ToString();
                    //    objWhgrr.Ombln = dtCombineStorage.Rows[i]["OMBLNR"].ToString();
                    //    objWhgrr.Matnr = dtCombineStorage.Rows[i]["MATNR"].ToString();
                    //    objWhgrr.Insmk = dtCombineStorage.Rows[i]["INSMK"].ToString();
                    //    objWhgrr.Charg = dtCombineStorage.Rows[i]["CHARG"].ToString();
                    //    objWhgrr.Serno = dtCombineStorage.Rows[i]["SERNO"].ToString();
                    //    objWhgrr.Dacod = dtCombineStorage.Rows[i]["DACOD"].ToString();
                    //    objWhgrr.Locod = dtCombineStorage.Rows[i]["LOCOD"].ToString();
                    //    objWhgrr.Inspt = dtCombineStorage.Rows[i]["INSPT"].ToString();
                    //    objWhgrr.Lifnr = dtCombineStorage.Rows[i]["LIFNR"].ToString();
                    //    objWhgrr.Indat = dtCombineStorage.Rows[i]["INDAT"].ToString();
                    //    objWhgrr.Kdmat = dtCombineStorage.Rows[i]["KDMAT"].ToString();
                    //    objWhgrr.Bkqty = dtCombineStorage.Rows[i]["MENGE"].ToString();
                    //    objWhgrr.Alqty = dtCombineStorage.Rows[i]["ALQTY"].ToString();
                    //    objWhgrr.Blace = (int.Parse(dtCombineStorage.Rows[i]["MENGE"].ToString()) - int.Parse(dtCombineStorage.Rows[i]["ALQTY"].ToString())).ToString();
                    //    objWhgrr.Barcode = strPickNo + ((int)(i + 1)).ToString("000");
                    //    objWhgrr.Prtyp = "0";
                    //    objWhgrr.Ctrlnm = "FGAUT";

                    //    arySQL.Add(objWhgrr.EntityGetInsertSql());
                    //}
                    #endregion


                    //WHHED

                    #region 處理WHHED Table Update 是否有庫存(LOSTS)及是否有連版(ISMRG)

                    //#region 整理出會異動的Location
                    //StringBuilder sbLocats = new StringBuilder();
                    //sbLocats.Remove(0, sbLocats.Length);
                    //for (int i = 0; i < aryLocat.Count; i++)
                    //{
                    //    if (i != 0)
                    //        sbLocats.Append(" , ");
                    //    sbLocats.Append("'" + aryLocat[i].ToString().Trim() + "'");
                    //}
                    //#endregion


                    //#region 先Update 有庫存的部分(Losts = 1)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "1";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 再Update 無庫存的部分(Losts = 0)
                    //objWhhed.ResetField();
                    //objWhhed.Losts = "0";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 先Update 有連版資料(ISMRG = Y)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "Y";

                    //alConditions.Clear();
                    //alConditions.Add("( Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    //#region 再Update 無連版資料(ISMRG = N)
                    //objWhhed.ResetField();
                    //objWhhed.Ismrg = "N";

                    //alConditions.Clear();
                    //alConditions.Add("( Not Exists (Select 'Y' From WHITM Where MANDT= WHHED.MANDT and COMCD= WHHED.COMCD and WERKS= WHHED.WERKS  and LGORT= WHHED.LGORT and LOCAT= WHHED.LOCAT AND MRGID<>''))");
                    //alConditions.Add("(MANDT= '" + strMandt + "')");
                    //alConditions.Add("(COMCD= '" + COMCD + "')");
                    //alConditions.Add("(WERKS= '" + strWerks + "')");
                    //alConditions.Add("(LGORT= '" + strLgort + "')");
                    //alConditions.Add("(LOCAT in (" + sbLocats.ToString() + "))");

                    //arySQL.Add(objWhhed.EntityGetUpdateSql(alConditions));
                    //#endregion

                    #endregion

                    //WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion

                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bool bolReturn = ControlExeSqlStringArr(arySQL);
                        if (bolReturn)
                        {
                            //strResult = strPickNo;
                            strResult = dtStorage.Rows[0]["MBLNR"].ToString();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddProductOutData_PickUp()";
                }
                return strResult;

            }


            #endregion


            #region  儲存批次轉倉出庫資料
            //============================================================================================
            ////////////Summary by Smose Liao 20091106////////////////////////////////////////////////////
            /// <summary>
            /// 儲存批次轉倉出庫資料
            /// </summary> 
            /// <param name="dtOutSource">出庫資料(使用者Key的出庫明細)</param>
            /// <param name="dtStorage">庫存明細資料</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool boolCheck =obj.AddTransferOutData_Auto( dtOutSource,  dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////
            public ArrayList AddTransferOutData_Auto(DataTable dtOutSource, DataTable dtStorage)
            {
                ArrayList alReturn = new ArrayList();
                DataRow[] drFound;
                try
                {
                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();
                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();
                    string strTempLocat = "";

                    DataTable dtData = new DataTable();
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objWhitm = new DataWhitm(UserData);

                    //WHLOG
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        int intTotal = 0;
                        //記錄所有的異動儲位
                        if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                            strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();
                        }
                        //計算應該要出庫的總數量
                        drFound = dtStorage.Select("LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' and MRGID='" + dtStorage.Rows[i]["MRGID"].ToString() + "'");
                        for (int k = 0; k < drFound.Length; k++)
                        {
                            intTotal += Int32.Parse(drFound[k]["ALQTY"].ToString());
                        }

                        //全部出庫完畢
                        if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                        {
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                        else
                        {
                            //arySQL.Add("update WHITM set MENGE=MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            //arySQL.Add("Delete from WHITM where MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() +
                            //    "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "' and MENGE='0'");
                            objWhitm.Menge = "MENGE - " + dtStorage.Rows[i]["ALQTY"].ToString();
                            objWhitm.Monam = strCrnam;
                            objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                            arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));

                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                            alConditions.Add(" MBLNR='" + dtStorage.Rows[i]["OMBLNR"].ToString() + "'");
                            alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");
                            alConditions.Add(" MENGE='0'");

                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        }
                    }
                    //WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    }

                    alReturn = arySQL;

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
                return alReturn;
            }

            #endregion


            #region  儲存產生扣帳單據的資料
            //====================================================================================
            ////////////Summary by Smose Liao 20100831////////////////////////////////////////////
            /// <summary>
            /// 儲存產生扣帳單據的資料
            /// </summary> 
            /// <param name="dtOutSource">DataTable</param>
            /// <param name="Type">SMT/FINAL</param>
            /// <param name="Category">QWMS/ASRS</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageOut =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddSimulationDocData(dtOutSource, Type, Category);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddSimulationDocData(DataTable dtOutSource, string Type, string Category)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddSimulationDocData";
                this.ControlMethodParm = "(" + Type + "," + Category + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                DataTable dtData = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList aryInsertSQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                ArrayList alMblnr = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                DataWhfin objWhfin = new DataWhfin(UserData);
                //string CYear = "";
                string CMonth = "";
                string CDay = "";
                string strSerno = "";
                string strMblnr = "";
                string strZeile = "";
                string strBudat = "";
                string strHeader = "";
                int intZeile = 0;

                #region //取得當天日期：WHDWN.BUDAT
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
                //CYear = Convert.ToString(dtnow.Year);
                //strBudat = CYear + CMonth + CDay;
                strBudat = CMonth + CDay;
                #endregion

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                #region  儲存SMT Data
                if (Type == "SMT")
                {
                    if (Category == "QWMS")
                    {
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);//給SAP的單號資料：66+廠區

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            #region Insert WHDWN
                            //判斷SMT的Item數如果大於12，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 12 == 0)
                            {
                                strSerno = GetSimulationNum("AddSimulationDoc", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + strBudat + (int.Parse(strSerno)).ToString("00000");//Header+XX月XX日+5碼流水號
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = MANDT;
                            objWhdwn.Comcd = COMCD;
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString(); ;  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "311";  //SMT:311
                            objWhdwn.Mtype = "QMS_SQ";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString(); //(Final才有)
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #endregion

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr, strZeile, Type, Category, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);//給ASRS的單號資料：60+廠區

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷SMT的Item數如果大於12，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 12 == 0)
                            {
                                strSerno = GetSimulationNum("AddSimulationDoc", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + strBudat + (int.Parse(strSerno)).ToString("00000");//Header+XX月XX日+5碼流水號
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = MANDT;
                            objWhdwn.Comcd = COMCD;
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "311";  //SMT:311
                            objWhdwn.Mtype = "QMS_SA";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();  //(Final才有)
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr, strZeile, Type, Category, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                }
                #endregion

                #region 儲存FINAL Data
                if (Type == "FINAL")
                {
                    if (Category == "QWMS")
                    {
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);//給SAP的單號資料：66+廠區

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("AddSimulationDoc", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + strBudat + (int.Parse(strSerno)).ToString("00000");//Header+XX月XX日+5碼流水號
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_FQ";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)    
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);//給ASRS的單號資料：60+廠區

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("AddSimulationDoc", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + strBudat + (int.Parse(strSerno)).ToString("00000");//Header+XX月XX日+5碼流水號
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉(固定為AS10)
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_FA";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)  
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }

                    #region  將虛擬單據號碼存回Whfin
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhfin.ResetField();

                        objWhfin.Mblnr = strMblnr;
                        alConditions.Add(" WERKS='" + dtOutSource.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" GRPID='" + dtOutSource.Rows[i]["GRPID"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtOutSource.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtOutSource.Rows[i]["CHARG"].ToString() + "'");

                        arySQL.Add(objWhfin.EntityGetUpdateSql(alConditions));
                    }
                    #endregion
                }

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
                return bolReturn;
            }

            #endregion


            #region  儲存祥天計畫模擬SAP產生扣帳單據的資料
            //====================================================================================
            ////////////Summary by Smose Liao 20100209////////////////////////////////////////////
            /// <summary>
            /// 儲存祥天計畫模擬SAP產生扣帳單據的資料
            /// </summary> 
            /// <param name="dtOutSource">DataTable</param>
            /// <param name="Type">SMT/FINAL</param>
            /// <param name="Category">QWMS/ASRS</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageOut =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddSapSimulationData(dtOutSource, Type, Category);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddSapSimulationData(DataTable dtOutSource, string Type, string Category)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddSapSimulationData";
                this.ControlMethodParm = "(" + Type + "," + Category + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                DataTable dtData = new DataTable();
                ArrayList arySQL = new ArrayList();
                ArrayList aryInsertSQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                //ArrayList alMblnr = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                DataWhfin objWhfin = new DataWhfin(UserData);
                string CYear = "";
                string CMonth = "";
                string CDay = "";
                string strSerno = "";
                string strMblnr = "";
                string strZeile = "";
                string strBudat = "";
                string strHeader = "";
                //string strUmlgo = "";
                int intZeile = 0;

                #region //取得當天日期：WHDWN.BUDAT
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

                //#region //取得廠區對應的收料倉：WHDWN.UMLGO
                //QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                //strUmlgo = objStorageData.QueryPlantLgort();
                //#endregion

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                #region  儲存SMT Data
                if (Type == "SMT")
                {
                    if (Category == "QWMS")
                    {
                        //#region //判斷要產生的虛擬單據之廠區是否為CS31(CS31廠區的單據編碼格式特別不同，多加兩碼31)
                        //if (dtOutSource.Rows[0]["WERKS"].ToString() != "CS31")
                        //{
                        //    strHeader = "66";  //給SAP的資料：66開頭
                        //}
                        //else if (dtOutSource.Rows[0]["WERKS"].ToString() == "CS31")
                        //{
                        //    strHeader = "6631";  //給SAP的資料：6631開頭
                        //}
                        //#endregion

                        //產生的虛擬單據By廠區作區分
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            #region Insert WHDWN
                            //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 20 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = MANDT;
                            objWhdwn.Comcd = COMCD;
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString(); ;  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "311";  //SMT:311
                            objWhdwn.Mtype = "QMS_QS";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString(); //(Final才有)
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #endregion

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr, strZeile, Type, Category, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        //#region //判斷要產生的虛擬單據之廠區是否為CS31(CS31廠區的單據編碼格式特別不同，多加兩碼31)
                        //if (dtOutSource.Rows[0]["WERKS"].ToString() != "CS31")
                        //{
                        //    strHeader = "60";  //給ASRS的資料：60開頭
                        //}
                        //else if (dtOutSource.Rows[0]["WERKS"].ToString() == "CS31")
                        //{
                        //    strHeader = "6031";  //給ASRS的資料：6031開頭
                        //}
                        //#endregion

                        //產生的虛擬單據By廠區作區分
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 20 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = MANDT;
                            objWhdwn.Comcd = COMCD;
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "311";  //SMT:311
                            objWhdwn.Mtype = "QMS_AS";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();  //(Final才有)
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr, strZeile, Type, Category, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                }
                #endregion

                #region 儲存FINAL Data
                if (Type == "FINAL")
                {
                    if (Category == "QWMS")
                    {
                        //#region //判斷要產生的虛擬單據之廠區是否為CS31(CS31廠區的單據編碼格式特別不同，多加兩碼31)
                        //if (dtOutSource.Rows[0]["WERKS"].ToString() != "CS31")
                        //{
                        //    strHeader = "66";  //給SAP的資料：66開頭
                        //}
                        //else if (dtOutSource.Rows[0]["WERKS"].ToString() == "CS31")
                        //{
                        //    strHeader = "6631";  //給SAP的資料：6631開頭
                        //}
                        //#endregion

                        //產生的虛擬單據By廠區作區分
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_QF";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)    
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        //#region //判斷要產生的虛擬單據之廠區是否為CS31(CS31廠區的單據編碼格式特別不同，多加兩碼31)
                        //if (dtOutSource.Rows[0]["WERKS"].ToString() != "CS31")
                        //{
                        //    strHeader = "60";  //給ASRS的資料：60開頭
                        //}
                        //else if (dtOutSource.Rows[0]["WERKS"].ToString() == "CS31")
                        //{
                        //    strHeader = "6031";  //給ASRS的資料：6031開頭
                        //}
                        //#endregion

                        //產生的虛擬單據By廠區作區分
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉(固定為AS10)
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_AF";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)  
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }

                    #region  將虛擬單據號碼存回Whfin
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhfin.ResetField();

                        objWhfin.Mblnr = strMblnr + strZeile;
                        objWhfin.Modat = "GetDate()";
                        alConditions.Add(" WERKS='" + dtOutSource.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" GRPID='" + dtOutSource.Rows[i]["GRPID"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtOutSource.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtOutSource.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" FMATN='" + dtOutSource.Rows[i]["FMATN"].ToString() + "'");
                        alConditions.Add(" WKORD='" + dtOutSource.Rows[i]["WKORD"].ToString() + "'");
                        alConditions.Add(" ARBPL='" + dtOutSource.Rows[i]["ARBPL"].ToString() + "'");
                        alConditions.Add(" COSCT='" + dtOutSource.Rows[i]["COSCT"].ToString() + "'");

                        arySQL.Add(objWhfin.EntityGetUpdateSql(alConditions));
                    }
                    #endregion
                }

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
                return bolReturn;
            }

            #endregion


            #region  儲存PowerⅡ-QWMS計畫模擬SAP產生扣帳單據的資料
            //===========================================================================================
            ////////////Summary by Smose Liao 20101026///////////////////////////////////////////////////
            /// <summary>
            /// 儲存PowerⅡ-QWMS計畫模擬SAP產生扣帳單據的資料
            /// </summary> 
            /// <param name="dtOutSource">DataTable</param>
            /// <param name="Type">SMT/FINAL</param>
            /// <param name="Category">QWMS/ASRS</param>
            /// <param name="Function">NORMAL/ADD</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageOut =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.ProduceSapSimulationData(dtOutSource, Type, Category, strFunction);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable ProduceSapSimulationData(DataTable dtOutSource, string Type, string Category, string strFunction)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ProduceSapSimulationData";
                this.ControlMethodParm = "(" + Type + "," + Category + "," + strFunction + ")";
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
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                DataWhfin objWhfin = new DataWhfin(UserData);
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

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                dtReturn.Columns.Add("MBLNR");

                #region  儲存SMT Data
                if (Type == "SMT")
                {
                    if (Category == "QWMS")
                    {
                        //產生的虛擬單據By廠區作區分
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            #region Insert WHDWN
                            //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 20 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            //將單據編號存入DataTable內並回傳扣帳
                            dtReturn.Rows.Add();   //產生新的一列
                            dtReturn.Rows[i]["MBLNR"] = strMblnr + strZeile;

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
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;
                            //objWhdwn.Flage = "W";

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #endregion

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr + strZeile, strZeile, Type, strFunction, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        //產生的虛擬單據By廠區作區分
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 20 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            //將單據編號存入DataTable內並回傳扣帳
                            dtReturn.Rows.Add();   //產生新的一列
                            dtReturn.Rows[i]["MBLNR"] = strMblnr + strZeile;

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = MANDT;
                            objWhdwn.Comcd = COMCD;
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr + strZeile;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "311";  //SMT:311
                            objWhdwn.Mtype = "QMS_AS";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();  //(Final才有)
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "T-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();  //收料倉
                            objWhdwn.Wkord = "";  //work order(工單)  (Final才有)
                            objWhdwn.Fmatn = "";  //father material(上一階材料)  (Final才有)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳時間)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = "";  //Work Center(線別)  (Final才有)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;
                            //objWhdwn.Flage = "W";

                            arySQL.Add(objWhdwn.EntityGetInsertSql());

                            #region Insert WHQDP(QMS Dispatch) & Update WHSMT
                            aryInsertSQL.Clear();
                            aryInsertSQL = InsertSmtQdpData(dtOutSource, strMblnr + strZeile, strZeile, Type, strFunction, i);
                            for (int j = 0; j < aryInsertSQL.Count; j++)
                                arySQL.Add(aryInsertSQL[j].ToString());
                            #endregion
                        }
                    }
                }
                #endregion

                #region 儲存FINAL Data
                if (Type == "FINAL")
                {
                    if (Category == "QWMS")
                    {
                        //產生的虛擬單據By廠區作區分
                        strHeader = "66" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            //將單據編號存入DataTable內並回傳扣帳
                            dtReturn.Rows.Add();   //產生新的一列
                            dtReturn.Rows[i]["MBLNR"] = strMblnr + strZeile;

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();  //發料倉
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr + strZeile;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_QF";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)    
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = "";  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }
                    else if (Category == "ASRS")
                    {
                        //產生的虛擬單據By廠區作區分
                        strHeader = "60" + dtOutSource.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            //判斷FINAL的Item數如果大於330，流水號再加1往下重新編號，Item編號也重新編號
                            if (i % 330 == 0)
                            {
                                strSerno = GetSimulationNum("FlyPlan", Type);  //取得虛擬單據新的流水號
                                intZeile = 0;  //Item編號重新編號
                            }

                            strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                            strZeile = (++intZeile).ToString("0000"); //虛擬單據的Item編號(4碼)

                            //將單據編號存入DataTable內並回傳扣帳
                            dtReturn.Rows.Add();   //產生新的一列
                            dtReturn.Rows[i]["MBLNR"] = strMblnr + strZeile;

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhdwn.ResetField();

                            objWhdwn.Mandt = dtOutSource.Rows[i]["MANDT"].ToString();
                            objWhdwn.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();
                            objWhdwn.Werks = dtOutSource.Rows[i]["WERKS"].ToString();
                            objWhdwn.Lgort = "AS10";  //發料倉(固定為AS10)
                            objWhdwn.Prcde = "Y";
                            objWhdwn.Mblnr = strMblnr + strZeile;
                            objWhdwn.Zeile = strZeile;
                            objWhdwn.Bwart = "261";  //FINAL:261
                            objWhdwn.Mtype = "QMS_AF";
                            objWhdwn.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();
                            objWhdwn.Charg = dtOutSource.Rows[i]["CHARG"].ToString();
                            objWhdwn.Menge = dtOutSource.Rows[i]["MENGE"].ToString();
                            objWhdwn.Qwqty = dtOutSource.Rows[i]["QWMS_MENGE"].ToString();  //QWMS的庫存數量
                            objWhdwn.Insmk = dtOutSource.Rows[i]["INSMK"].ToString();
                            objWhdwn.Trntp = "G-";
                            objWhdwn.Putyp = "";
                            objWhdwn.Lifnr = "";
                            objWhdwn.Umlgo = "";  //收料倉(SMT才有)  
                            objWhdwn.Wkord = dtOutSource.Rows[i]["WKORD"].ToString();  //work order(工單)
                            objWhdwn.Fmatn = dtOutSource.Rows[i]["FMATN"].ToString();  //father material(上一階材料)
                            objWhdwn.Ebeln = "";  //purchase order
                            objWhdwn.Ebelp = "";  //purchase order item
                            objWhdwn.Intid = dtOutSource.Rows[i]["GRPID"].ToString();  //group id
                            objWhdwn.Kostl = dtOutSource.Rows[i]["COSCT"].ToString();  //Cost Center
                            objWhdwn.Reslt = "";
                            objWhdwn.Budat = strBudat;  //Posting Date in Doc(扣帳日期)
                            objWhdwn.Prity = "";  //Priority
                            objWhdwn.Arbpl = dtOutSource.Rows[i]["ARBPL"].ToString();  //Work Center(線別)
                            objWhdwn.Crdat = "GetDate()";
                            objWhdwn.Modat = "GetDate()";
                            objWhdwn.Usnam = CRNAM;

                            arySQL.Add(objWhdwn.EntityGetInsertSql());
                        }
                    }

                    #region  將虛擬單據號碼存回Whfin
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        alColumns.Clear();
                        alConditions.Clear();
                        objWhfin.ResetField();

                        objWhfin.Mblnr = strMblnr + strZeile;
                        objWhfin.Modat = "GetDate()";
                        alConditions.Add(" WERKS='" + dtOutSource.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add(" GRPID='" + dtOutSource.Rows[i]["GRPID"].ToString() + "'");
                        alConditions.Add(" MATNR='" + dtOutSource.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add(" CHARG='" + dtOutSource.Rows[i]["CHARG"].ToString() + "'");
                        alConditions.Add(" FMATN='" + dtOutSource.Rows[i]["FMATN"].ToString() + "'");
                        alConditions.Add(" WKORD='" + dtOutSource.Rows[i]["WKORD"].ToString() + "'");
                        alConditions.Add(" ARBPL='" + dtOutSource.Rows[i]["ARBPL"].ToString() + "'");
                        alConditions.Add(" COSCT='" + dtOutSource.Rows[i]["COSCT"].ToString() + "'");

                        arySQL.Add(objWhfin.EntityGetUpdateSql(alConditions));
                    }
                    #endregion
                }

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

                return dtReturn;
            }

            #endregion


            #region InsertSmtFinData
            public ArrayList InsertSmtQdpData(DataTable dtOutSource, string strMblnr, string strZeile, string Bwart, string Stype, int i)
            {
                int intStoreOutQty;
                string strSmtMblnr = "";
                DataTable dtWhSmt = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList arySQL = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhsmt objWhsmt = new DataWhsmt(UserData);
                DataWhqdp objWhqdp = new DataWhqdp(UserData);
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                #region 查詢該筆Send id所對應的料號是否已經扣過
                intStoreOutQty = 0;
                dtWhSmt = objStorageData.QuerySendIdMblnr(dtOutSource.Rows[i]["GRPID"].ToString(), dtOutSource.Rows[i]["MATNR"].ToString(), dtOutSource.Rows[i]["COSCT"].ToString());
                if (dtWhSmt.Rows.Count > 0)
                {
                    intStoreOutQty = int.Parse(dtWhSmt.Rows[0]["MENGE"].ToString());
                    strSmtMblnr = dtWhSmt.Rows[0]["MBLNR"].ToString();
                }

                #endregion

                #region  將虛擬單據號碼與數量存回WHSMT
                alColumns.Clear();
                alConditions.Clear();
                objWhsmt.ResetField();

                objWhsmt.Mblnr = strSmtMblnr + strMblnr + ";";//原本的單據號碼 + 這次新產生的單據號碼
                objWhsmt.Menge = (intStoreOutQty + int.Parse(dtOutSource.Rows[i]["MENGE"].ToString())).ToString();//原本發出的數量 + 這次新發出的數量 
                objWhsmt.Modat = "GetDate()";//修改的日期與時間
                alConditions.Add(" WERKS='" + dtOutSource.Rows[i]["WERKS"].ToString() + "'");
                alConditions.Add(" GRPID='" + dtOutSource.Rows[i]["GRPID"].ToString() + "'");
                alConditions.Add(" MATNR='" + dtOutSource.Rows[i]["MATNR"].ToString() + "'");
                alConditions.Add(" COSCT='" + dtOutSource.Rows[i]["COSCT"].ToString() + "'");

                arySQL.Add(objWhsmt.EntityGetUpdateSql(alConditions));
                #endregion

                #region 將虛擬單據資料與數量新增至WHQDP(QMS Dispatch)
                alColumns.Clear();
                alConditions.Clear();
                objWhqdp.ResetField();

                objWhqdp.Mandt = UserData.Client;
                objWhqdp.Comcd = dtOutSource.Rows[i]["COMCD"].ToString();//Company Code
                objWhqdp.Werks = dtOutSource.Rows[i]["WERKS"].ToString();//Plant
                objWhqdp.Lgort = dtOutSource.Rows[i]["LGORT"].ToString();//Storage
                objWhqdp.Grpid = dtOutSource.Rows[i]["GRPID"].ToString();//Send/Group id
                objWhqdp.Matnr = dtOutSource.Rows[i]["MATNR"].ToString();//料號
                objWhqdp.Mblnr = strMblnr;                               //扣帳單據編號
                objWhqdp.Zeile = strZeile;                               //Document No. Items
                objWhqdp.Cosct = dtOutSource.Rows[i]["COSCT"].ToString();//Cost Center
                objWhqdp.Menge = dtOutSource.Rows[i]["MENGE"].ToString();//QWMS實際的發料數量
                objWhqdp.Umlgo = dtOutSource.Rows[i]["UMLGO"].ToString();//來料倉
                objWhqdp.Crdat = "GetDate()";//Create Date
                objWhqdp.Stype = Stype;//NORMAL(id出庫)/ADD(加扣)/RETURN(SMT退庫)
                objWhqdp.Crnam = CRNAM;//Create Name

                arySQL.Add(objWhqdp.EntityGetInsertSql());
                #endregion

                return arySQL;
            }
            #endregion


            #region  儲存祥天計畫/PowerⅡ-QWMS之加扣功能所產生的扣帳單據資料
            //====================================================================================
            ////////////Summary by Smose Liao 20100225////////////////////////////////////////////
            /// <summary>
            /// 儲存祥天計畫/PowerⅡ-QWMS之加扣功能所產生的扣帳單據資料
            /// </summary> 
            /// <param name="dtData">DataTable</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageOut objStorageOut =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.AddAdmin_DocumentAddQty(dtData);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable AddAdmin_DocumentAddQty(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddAdmin_DocumentAddQty";
                this.ControlMethodParm = "(" + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                ArrayList arySQL = new ArrayList();
                ArrayList aryInsertSQL = new ArrayList();
                bool bolReturn = false;
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhqdp objWhqdp = new DataWhqdp(UserData);
                DataTable dtStorageData = new DataTable();
                DataRow drRow;
                string strSerno = "";
                string strMblnr = "";
                string strZeile = "";
                string strHeader;
                int intZeile = 0;

                StorageData objStorageData = new StorageData(UserData, WERKS, LGORT);
                dtStorageData = dtData.Clone();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    drRow = dtStorageData.NewRow();
                    //先掃瞄DataGridView中的資料，若MdQty(加扣數量)有變更才儲存
                    if (int.Parse(dtData.Rows[i]["MdQty"].ToString()) > 0)
                    {
                        //先取得單據號碼的前兩碼
                        strHeader = dtData.Rows[i]["MBLNR"].ToString().Substring(0, 2);  //60:ASRS  66:SAP

                        //產生的虛擬單據By廠區作區分
                        strHeader = strHeader + dtData.Rows[0]["WERKS"].ToString().Substring(2, 2);

                        //判斷SMT的Item數如果大於20，流水號再加1往下重新編號，Item編號也重新編號
                        if (i % 20 == 0)
                        {
                            strSerno = GetSimulationNum("FlyPlan", "SMT");  //取得虛擬單據新的流水號
                            intZeile = 0;  //Item編號重新編號
                        }

                        strMblnr = strHeader + (int.Parse(strSerno)).ToString("000000");
                        strZeile = ((int)(++intZeile)).ToString("0000"); //虛擬單據的Item編號(4碼)

                        drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                        drRow["MTYPE"] = dtData.Rows[i]["MTYPE"].ToString();
                        drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                        drRow["MBLNR"] = strMblnr + strZeile;                 //新的單據號碼
                        drRow["ZEILE"] = strZeile;                            //新的單據Item
                        drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                        drRow["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString();
                        drRow["EBELN"] = dtData.Rows[i]["EBELN"].ToString();
                        drRow["MENGE"] = dtData.Rows[i]["MDQTY"].ToString();  //加扣數量
                        drRow["OTQTY"] = dtData.Rows[i]["OTQTY"].ToString();
                        drRow["PRCDE"] = dtData.Rows[i]["PRCDE"].ToString();
                        drRow["PUTYP"] = dtData.Rows[i]["PUTYP"].ToString();
                        drRow["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString();
                        drRow["RESLT"] = dtData.Rows[i]["RESLT"].ToString();
                        drRow["BUDAT"] = dtData.Rows[i]["BUDAT"].ToString();
                        drRow["PRITY"] = dtData.Rows[i]["PRITY"].ToString();
                        drRow["ARBPL"] = dtData.Rows[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = dtData.Rows[i]["TRNTP"].ToString();
                        drRow["BWART"] = dtData.Rows[i]["BWART"].ToString();
                        drRow["UMLGO"] = dtData.Rows[i]["UMLGO"].ToString();
                        drRow["USNAM"] = dtData.Rows[i]["USNAM"].ToString();
                        drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString();
                        drRow["SERNO"] = dtData.Rows[i]["SERNO"].ToString();
                        drRow["WKORD"] = dtData.Rows[i]["WKORD"].ToString();
                        drRow["FMATN"] = dtData.Rows[i]["FMATN"].ToString();
                        drRow["EBELP"] = dtData.Rows[i]["EBELP"].ToString();
                        drRow["INTID"] = dtData.Rows[i]["INTID"].ToString();
                        drRow["FLAGE"] = dtData.Rows[i]["FLAGE"].ToString();
                        drRow["QWQTY"] = dtData.Rows[i]["QWQTY"].ToString();  //QWMS目前的庫存數量

                        dtStorageData.Rows.Add(drRow);

                        #region 查詢WHQDP中是否已經存在加扣的資料
                        if (!objStorageData.QueryExistQdpData(dtData.Rows[i]["INTID"].ToString(), dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["KOSTL"].ToString()))
                        {
                            #region Insert WHQDP(QMS Dispatch)
                            alColumns.Clear();
                            alConditions.Clear();
                            objWhqdp.ResetField();

                            objWhqdp.Mandt = UserData.Client;
                            objWhqdp.Comcd = dtData.Rows[i]["COMCD"].ToString();
                            objWhqdp.Werks = dtData.Rows[i]["WERKS"].ToString();
                            objWhqdp.Lgort = dtData.Rows[i]["LGORT"].ToString();
                            objWhqdp.Grpid = dtData.Rows[i]["INTID"].ToString();//Send/Group id
                            objWhqdp.Matnr = dtData.Rows[i]["MATNR"].ToString();
                            objWhqdp.Mblnr = strMblnr + strZeile;               //扣帳單據編號
                            objWhqdp.Zeile = strZeile;                          //Items
                            objWhqdp.Cosct = dtData.Rows[i]["KOSTL"].ToString();//Cost Center
                            objWhqdp.Menge = dtData.Rows[i]["MDQTY"].ToString();//加扣數量
                            objWhqdp.Crdat = "GetDate()";
                            objWhqdp.Stype = "ADD";//NORMAL(ID出庫)/ADD(加扣功能)/RETURN(SMT退庫)
                            objWhqdp.Crnam = CRNAM;

                            arySQL.Add(objWhqdp.EntityGetInsertSql());
                            #endregion
                            #region 记录祥龙加扣单据的MBLNR,STYPE,WERKS,GRPID,MATNR,MENGE,GETDATE
                            string mtype = "XLADD";
                            arySQL.Add("INSERT INTO QWMS_LOG (MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME)VALUES('" + strMblnr + strZeile + "','" + mtype + "','" + dtData.Rows[i]["WERKS"].ToString() + "','" + dtData.Rows[i]["INTID"].ToString() + "','" + dtData.Rows[i]["MATNR"].ToString() + "','" + dtData.Rows[i]["MDQTY"].ToString() + "',GETDATE())");
                            #endregion
                        }
                        else
                        {
                            #region Update WHQDP(QMS Dispatch)

                            alColumns.Clear();
                            alConditions.Clear();
                            objWhqdp.ResetField();
                            objWhqdp.Menge = "MENGE + " + dtData.Rows[i]["MDQTY"].ToString();//原本加扣的數量 + 這次加扣的數量 

                            objWhqdp.Mandt = UserData.Client;
                            alConditions.Add(" MANDT='" + UserData.Client + "'");
                            alConditions.Add(" COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                            alConditions.Add(" WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                            alConditions.Add(" LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                            alConditions.Add(" GRPID='" + dtData.Rows[i]["INTID"].ToString() + "'");
                            alConditions.Add(" MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                            alConditions.Add(" COSCT='" + dtData.Rows[i]["KOSTL"].ToString() + "'");
                            alConditions.Add(" STYPE='ADD' ");

                            arySQL.Add(objWhqdp.EntityGetUpdateSql(alConditions));
                            #endregion
                            #region 记录祥龙加扣单据的MBLNR,STYPE,WERKS,GRPID,MATNR,MENGE,GETDATE
                            string mtype = "XLADD";
                            arySQL.Add("INSERT INTO QWMS_LOG (MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME)VALUES('" + strMblnr + strZeile + "','" + mtype + "','" + dtData.Rows[i]["WERKS"].ToString() + "','" + dtData.Rows[i]["INTID"].ToString() + "','" + dtData.Rows[i]["MATNR"].ToString() + "','" + dtData.Rows[i]["MDQTY"].ToString() + "',GETDATE())");
                            #endregion

                        }
                        #endregion
                    }
                }

                #region 新增單據存入WHDWN
                for (int i = 0; i < dtStorageData.Rows.Count; i++)
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    objWhdwn.ResetField();

                    objWhdwn.Mandt = dtStorageData.Rows[i]["MANDT"].ToString();
                    objWhdwn.Comcd = dtStorageData.Rows[i]["COMCD"].ToString();
                    objWhdwn.Werks = dtStorageData.Rows[i]["WERKS"].ToString();
                    objWhdwn.Lgort = dtStorageData.Rows[i]["LGORT"].ToString();
                    objWhdwn.Prcde = dtStorageData.Rows[i]["PRCDE"].ToString();
                    objWhdwn.Mblnr = dtStorageData.Rows[i]["MBLNR"].ToString();
                    objWhdwn.Zeile = dtStorageData.Rows[i]["ZEILE"].ToString();
                    objWhdwn.Bwart = dtStorageData.Rows[i]["BWART"].ToString();
                    objWhdwn.Mtype = dtStorageData.Rows[i]["MTYPE"].ToString();
                    objWhdwn.Matnr = dtStorageData.Rows[i]["MATNR"].ToString();
                    objWhdwn.Charg = dtStorageData.Rows[i]["CHARG"].ToString();
                    objWhdwn.Menge = dtStorageData.Rows[i]["MENGE"].ToString();
                    objWhdwn.Insmk = dtStorageData.Rows[i]["INSMK"].ToString();
                    objWhdwn.Trntp = dtStorageData.Rows[i]["TRNTP"].ToString();
                    objWhdwn.Putyp = dtStorageData.Rows[i]["PUTYP"].ToString();
                    objWhdwn.Lifnr = dtStorageData.Rows[i]["LIFNR"].ToString();
                    objWhdwn.Umlgo = dtStorageData.Rows[i]["UMLGO"].ToString();
                    objWhdwn.Wkord = dtStorageData.Rows[i]["WKORD"].ToString();
                    objWhdwn.Fmatn = dtStorageData.Rows[i]["FMATN"].ToString();
                    objWhdwn.Ebeln = dtStorageData.Rows[i]["EBELN"].ToString();
                    objWhdwn.Ebelp = dtStorageData.Rows[i]["EBELP"].ToString();
                    objWhdwn.Intid = dtStorageData.Rows[i]["INTID"].ToString();
                    objWhdwn.Kostl = dtStorageData.Rows[i]["KOSTL"].ToString();
                    objWhdwn.Reslt = dtStorageData.Rows[i]["RESLT"].ToString();
                    objWhdwn.Budat = dtStorageData.Rows[i]["BUDAT"].ToString();
                    objWhdwn.Prity = dtStorageData.Rows[i]["PRITY"].ToString();
                    objWhdwn.Arbpl = dtStorageData.Rows[i]["ARBPL"].ToString();
                    objWhdwn.Qwqty = dtStorageData.Rows[i]["QWQTY"].ToString();
                    objWhdwn.Crdat = "GetDate()";
                    objWhdwn.Modat = "GetDate()";
                    objWhdwn.Usnam = CRNAM;
                    objWhdwn.Adflg = "A";

                    arySQL.Add(objWhdwn.EntityGetInsertSql());
                }
                #endregion

                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();

                    return dtStorageData;
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


            #region 產生虛擬單據的流水號
            //====================================================================================
            ////////////Summary by Smose Liao 20100308////////////////////////////////////////////
            /// <summary>
            /// 產生虛擬單據的流水號
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
            public string GetSimulationNum(string strCtrlnm, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSimulationNum";
                this.ControlMethodParm = "(" + strCtrlnm + "," + strType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strSerno = "";

                try
                {
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    if (strCtrlnm == "FlyPlan")//祥天計畫
                    {
                        //By廠區取得虛擬單據的流水號(累加SMT & FINAL的流水號直到隔年)  by Smose Liao 20100907
                        //strSQL = "EXEC SP_GetSerialNum_FlyPlan 'SimulationDoc', 'FLYPLAN'";

                        //取得虛擬單據的流水號 by Smose Liao 20100209
                        //strSerno = objStorageData.GetSerno("SimulationDoc", "FLYPLAN");
                        strSQL = "EXEC SP_GetSerialNum_Fly 'SimulationDoc', 'FLYPLAN'";
                        ControlHandleDB();
                        strSerno = ControlSqlAccess.GetFieldValue(strSQL);
                        ControlSqlAccess.CloseConnection();
                    }
                    else if (strCtrlnm == "AddSimulationDoc")//QCI_TH20
                    {
                        //By廠區取得虛擬單據的流水號(累加SMT & FINAL的流水號直到隔年)  by Smose Liao 20100907

                        //取得虛擬單據的流水號 by Smose Liao 20100209
                        strSQL = "EXEC SP_GetSerialNum_Fly 'AddSimulationDoc', 'QCI_TH20'";
                        ControlHandleDB();
                        strSerno = ControlSqlAccess.GetFieldValue(strSQL);
                        ControlSqlAccess.CloseConnection();
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

                return strSerno;
            }
            #endregion


            #region  更新PowerⅡ-QWMS的扣帳單據狀態(WHDWN.FLAGE從'W'改成'Y')
            //===================================================================================
            ////////////Summary by Smose Liao 20101115///////////////////////////////////////////
            /// <summary>
            /// 更新PowerⅡ-QWMS的扣帳單據狀態(WHDWN.FLAGE從'W'改成'Y')
            /// </summary> 
            /// <param name="dtData">資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.UpdateDocStatus(dtData);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool UpdateDocStatus(DataTable dtData)
            {
                bool bolReturn = false;
                ArrayList arySQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                //WHDWN
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //更新狀態
                    objWhdwn.ResetField();
                    alConditions.Clear();

                    objWhdwn.Flage = "Y";
                    objWhdwn.Updat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";

                    alConditions.Add(" MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                    alConditions.Add(" COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                    alConditions.Add(" WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                    alConditions.Add(" LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                    alConditions.Add(" INTID='" + dtData.Rows[i]["GRPID"].ToString() + "'");
                    alConditions.Add(" MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                    alConditions.Add(" KOSTL='" + dtData.Rows[i]["COSCT"].ToString() + "'");

                    arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                }

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

            #endregion

            #region  PowerⅡ-QWMS的仓别管控DateCode

            public DataTable CheckDateCode()
            {
                DataTable dt = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT CTRLNM AS WERKS,CTRLC1 AS LGORT FROM WHCTRL WITH(NOLOCK) WHERE ");
                strSql.AppendFormat("MANDT='{0}' AND CTRLID='StorageIn_Type'   ", MANDT);
                strSql.AppendFormat("AND CTRLNM='{0}' AND CTRLC1='{1}'", strWerks, strLgort);
                strSql.AppendFormat("AND COMCD='{0}' AND REMAK=N'Diff DACOD Diff Locat'", COMCD);


                try
                {
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSql.ToString());
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
                return dt;
            }

            #endregion

            #region  更新庫存資料(Spare Parts)
            //===================================================================================
            ////////////Summary by Smose Liao 20100504///////////////////////////////////////////
            /// <summary>
            /// 更新庫存資料(Spare Parts)
            /// </summary> 
            /// <param name="dtStorage">庫存資料。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageOut(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageOut.UpdateStorageData(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public bool UpdateStorageData(DataTable dtStorage)
            {
                bool bolReturn = false;
                ArrayList arySQL = new ArrayList();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);

                //WHITM
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    //WHITM
                    //更新庫存資料
                    objWhitm.ResetField();
                    alConditions.Clear();
                    objWhitm.Ebeln = dtStorage.Rows[i]["EBELN"].ToString();
                    objWhitm.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                    objWhitm.Monam = strCrnam;
                    objWhitm.Modat = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ".000";
                    alConditions.Add(" MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                    alConditions.Add(" COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                    alConditions.Add(" WERKS='" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                    alConditions.Add(" LGORT='" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                    alConditions.Add(" LOCAT='" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                    alConditions.Add(" NLOCA='" + dtStorage.Rows[i]["NLOCA"].ToString() + "'");
                    alConditions.Add(" MATNR='" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                    alConditions.Add(" INSMK='" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                    alConditions.Add(" CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                    arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                }

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

            #endregion
            #region 查詢發料線別信息 by Ryan Tsai 20131016
            //===============================================================================================================
            ////////////Summary by Ryan Tsai 20131016///////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢備料線別資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryLineInfo(string strWERKS, string strLgort, string strMBLNR)
            {
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    //sbSql.AppendFormat(" select * from WHPRI where WERKS='{0}' ", strWERKS);
                    sbSql.Append(" select d.ARBPL,p.ARBPL as Line, Replicate('0',3-LEN(cast(p.PRITY as varchar)))+p.PRITY AS PRITY,  ");
                    sbSql.Append(" substring(d.PRITY,1, 2)+':'+substring(d.PRITY,3, 2) AS DWNTY, ");
                    //W/H(瞿東明)要求需在報表上顯示完整的Send ID資訊  Smose Liao 20141023
                    //sbSql.Append(" (Case when Len(d.REFID)> 20 then '*'+SUBSTRING(d.REFID,0,20)+'*' else '*'+d.REFID+'*' End) AS REFID_Barcode, ");
                    //sbSql.Append(" (Case when Len(d.REFID)> 24 then '*'+SUBSTRING(d.REFID,0,24)+'*' else '*'+d.REFID+'*' End) AS REFID_Barcode, ");
                    sbSql.Append(" (Case when Len(d.REFID)> 24 then SUBSTRING(d.REFID,0,24) else d.REFID End) AS REFID_Barcode, ");
                    sbSql.Append(" d.BUDAT from WHDWN  as d ");
                    //sbSql.Append(" left join WHPRI as p on CharIndex(p.ARBPL,substring(d.ARBPL,1,5))>0 and d.WERKS=p.WERKS ");
                    sbSql.Append(" left join WHPRI as p on p.ARBPL=d.ARBPL and d.WERKS=p.WERKS ");
                    sbSql.AppendFormat(" where p.WERKS= '{0}' and d.LGORT='{1}' and d.MBLNR in ({2})", strWERKS, strLgort, strMBLNR);

                    DataTable dtData = new DataTable();
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

            #region  查詢出庫料號品名信息 by Ryan Tsai 20131017
            //===============================================================================================================
            ////////////Summary by Ryan Tsai 20131017///////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢出庫料號品名資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryMaterialInfo(DataTable dtStorageOut)
            {
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtData = new DataTable();

                    sbSql.Append(" select * from WHMAN where 1=1 and MATNR in ( ");

                    sbSql.AppendFormat(" '{0}' ", dtStorageOut.Rows[0]["MATNR"].ToString().Trim());
                    for (int i = 1; i < dtStorageOut.Rows.Count; i++) //查詢出庫料號品名,避免全部查詢影響join時間
                    {
                        sbSql.AppendFormat(",'{0}' ", dtStorageOut.Rows[i]["MATNR"].ToString().Trim());
                    }
                    sbSql.Append(" )");

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

            #region 查詢出庫備料單預設單據類型 by Ryan Tsai 20131023
            public DataTable QueryStorageOutDefaultReport(string strWerksLgort)
            {
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtData = new DataTable();

                    sbSql.AppendFormat("select * from WHCTRL with(nolock) where CTRLID='OUTRPT' and CTRLNM='{0}'", strWerksLgort);

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


            #region  存储出货信息
            //====================================================================================
            ////////////Summary by Karen yang////////////////////////////////////////////
            /// <summary>
            /// 存储出货信息
            /// </summary> 

            /////////////////////////////////////////////////////////////////////////////////////	
            public bool AddChectOutPad(DataTable dtOutSource, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddSimulationDocData";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool bolReturn = false;
                ArrayList arySQL = new ArrayList(); ;

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);


                if (dtOutSource.Rows.Count > 0)
                {
                    if (strType == "AGV")
                    {
                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {


                            StringBuilder strsql = new StringBuilder();
                            strsql.AppendFormat("INSERT INTO PADWN(MANDT,COMCD,WERKS,LGORT,LOCAT,MTYPE,MBLNR,ZEILE,OMBLNR,MATNR,INSMK,CHARG,ALQTY,MRGID,KOSTL,ARBPL,TRNTP,EBELN,LIFNR,RMANO,RMAK1,INDAT,KDMAT,CRDAT,SERNO,BOXID,DACOD,LOCOD,INSPT,");
                            strsql.AppendFormat(" PKDAT,REFID,SEQNO,BLACE,LINE,PRITY,MATNM,DWNTY,BUDAT,REFID_BARCODE,SUMRY)  VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')",
                                dtOutSource.Rows[i]["MANDT"].ToString(), dtOutSource.Rows[i]["COMCD"].ToString(), dtOutSource.Rows[i]["WERKS"].ToString(), dtOutSource.Rows[i]["LGORT"].ToString(), dtOutSource.Rows[i]["LOCAT"].ToString(), dtOutSource.Rows[i]["MTYPE"].ToString(), dtOutSource.Rows[i]["MBLNR"].ToString(), dtOutSource.Rows[i]["ZEILE"].ToString(), dtOutSource.Rows[i]["OMBLNR"].ToString(), dtOutSource.Rows[i]["MATNR"].ToString(), dtOutSource.Rows[i]["INSMK"].ToString(), dtOutSource.Rows[i]["CHARG"].ToString(), dtOutSource.Rows[i]["ALQTY"].ToString(), dtOutSource.Rows[i]["MRGID"].ToString(), dtOutSource.Rows[i]["KOSTL"].ToString(),
                            dtOutSource.Rows[i]["ARBPL"].ToString(), dtOutSource.Rows[i]["TRNTP"].ToString(), dtOutSource.Rows[i]["EBELN"].ToString(), dtOutSource.Rows[i]["LIFNR"].ToString(), dtOutSource.Rows[i]["RMANO"].ToString(), dtOutSource.Rows[i]["RMAK1"].ToString(), dtOutSource.Rows[i]["INDAT"].ToString(), dtOutSource.Rows[i]["KDMAT"].ToString(),
                            dtOutSource.Rows[i]["CRDAT"].ToString(), dtOutSource.Rows[i]["SERNO"].ToString(), dtOutSource.Rows[i]["BOXID"].ToString(), dtOutSource.Rows[i]["DACOD"].ToString(), dtOutSource.Rows[i]["LOCOD"].ToString(), dtOutSource.Rows[i]["INSPT"].ToString(),
                            dtOutSource.Rows[i]["PKDAT"].ToString(), dtOutSource.Rows[i]["REFID"].ToString(), dtOutSource.Rows[i]["SEQNO"].ToString(), dtOutSource.Rows[i]["BLACE"].ToString(), dtOutSource.Rows[i]["LINE"].ToString(), dtOutSource.Rows[i]["PRITY"].ToString(), dtOutSource.Rows[i]["MATNM"].ToString(), dtOutSource.Rows[i]["DWNTY"].ToString(), dtOutSource.Rows[i]["BUDAT"].ToString(), dtOutSource.Rows[i]["REFID_BARCODE"].ToString(), dtOutSource.Rows[i]["SUMRY"].ToString());
                            arySQL.Add(strsql.ToString());

                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtOutSource.Rows.Count; i++)
                        {
                            StringBuilder strsql = new StringBuilder();
                            strsql.AppendFormat("INSERT INTO PADWN(MANDT,COMCD,WERKS,LGORT,LOCAT,MTYPE,MBLNR,ZEILE,OMBLNR,MATNR,INSMK,CHARG,ALQTY,MRGID,KOSTL,ARBPL,TRNTP,EBELN,LIFNR,RMANO,RMAK1,INDAT,KDMAT,CRDAT,SERNO,BOXID,DACOD,LOCOD,INSPT,");
                            strsql.AppendFormat(" PKDAT,REFID,SEQNO,BLACE,LINE,PRITY,MATNM,DWNTY,BUDAT,REFID_BARCODE,SUMRY)  VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')",
                                dtOutSource.Rows[i]["MANDT"].ToString(), dtOutSource.Rows[i]["COMCD"].ToString(), dtOutSource.Rows[i]["WERKS"].ToString(), dtOutSource.Rows[i]["LGORT"].ToString(),
                                dtOutSource.Rows[i]["LOCAT"].ToString(),
                                dtOutSource.Rows[i]["MTYPE"].ToString(), dtOutSource.Rows[i]["MBLNR"].ToString(), dtOutSource.Rows[i]["ZEILE"].ToString(), dtOutSource.Rows[i]["OMBLNR"].ToString(),
                                dtOutSource.Rows[i]["MATNR"].ToString(), dtOutSource.Rows[i]["INSMK"].ToString(), dtOutSource.Rows[i]["CHARG"].ToString(), dtOutSource.Rows[i]["ALQTY"].ToString(),
                                dtOutSource.Rows[i]["MRGID"].ToString(), dtOutSource.Rows[i]["KOSTL"].ToString(),
                            dtOutSource.Rows[i]["ARBPL"].ToString(), dtOutSource.Rows[i]["TRNTP"].ToString(), dtOutSource.Rows[i]["EBELN"].ToString(), dtOutSource.Rows[i]["LIFNR"].ToString(),
                            dtOutSource.Rows[i]["RMANO"].ToString(), dtOutSource.Rows[i]["RMAK1"].ToString(), dtOutSource.Rows[i]["INDAT"].ToString(), dtOutSource.Rows[i]["KDMAT"].ToString(),
                            dtOutSource.Rows[i]["CRDAT"].ToString(), dtOutSource.Rows[i]["SERNO"].ToString(), dtOutSource.Rows[i]["BOXID"].ToString(), dtOutSource.Rows[i]["DACOD"].ToString(),
                            dtOutSource.Rows[i]["LOCOD"].ToString(), dtOutSource.Rows[i]["INSPT"].ToString(),
                            dtOutSource.Rows[i]["PKDAT"].ToString(), dtOutSource.Rows[i]["REFID"].ToString(), dtOutSource.Rows[i]["SEQNO"].ToString(), dtOutSource.Rows[i]["BLACE"].ToString(),
                           ""// dtOutSource.Rows[i]["LINE"].ToString()
                            , ""//dtOutSource.Rows[i]["PRITY"].ToString()
                            , ""// dtOutSource.Rows[i]["MATNM"].ToString()
                            , ""//dtOutSource.Rows[i]["DWNTY"].ToString()
                            ,
                           ""// dtOutSource.Rows[i]["BUDAT"].ToString()
                           , ""// dtOutSource.Rows[i]["REFID_BARCODE"].ToString()
                           , ""// dtOutSource.Rows[i]["SUMRY"].ToString()
                           );
                            arySQL.Add(strsql.ToString());
                        }
                    }
                }



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

            #endregion

            #region Add by Michael 20150518 for 查询目的厂区
            /// <summary>
            /// Add by Michael 20150518 for 查询目的厂区
            /// </summary>
            /// <returns>回傳值型態為DataTable。</returns>
            /// <example>
            /// <code>
            /// </code>
            /// </example>
            public DataTable GetMBLNRWerks(string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMBLNRWerks";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();

                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");

                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(MTYPE='SAP')");
                alConditions.Add("(MBLNR='" + strMblnr + "')");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false, true);
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

            #region Film出库 Add By Michael 20150930

            public bool AddOnLineOutData_Film(DataTable dtOutSource, DataTable dtStorage)
            {
                #region 變數宣告
                bool bolReturn = false;
                ArrayList aryCheckSQL = new ArrayList();
                ArrayList aryCheckList = new ArrayList();
                StringBuilder sbCheckList = new StringBuilder();
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL1 = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                DataWhhed objWhhed = new DataWhhed(UserData);
                DataWhitm objWhitm = new DataWhitm(UserData);
                DataWhdwn objWhdwn = new DataWhdwn(UserData);
                DataWhbox objWhbox = new DataWhbox(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                #endregion
                try
                {
                    #region WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);
                    #endregion

                    #region 检查库存是否足够

                    #region "Combine DataTable"
                    //Whitm的Key
                    ArrayList alKeys = new ArrayList();
                    alKeys.Add("MANDT");
                    alKeys.Add("COMCD");
                    alKeys.Add("WERKS");
                    alKeys.Add("LGORT");
                    alKeys.Add("LOCAT");
                    alKeys.Add("MATNR");
                    alKeys.Add("INSMK");
                    alKeys.Add("CHARG");
                    alKeys.Add("OMBLNR");
                    alKeys.Add("SERNO");
                    alKeys.Add("BOXID");

                    Hashtable htCmpFields = new Hashtable();
                    htCmpFields.Add("ALQTY", CmpAction.Sum);


                    DataTable dtCombinStorage = CombineTable(dtStorage, alKeys, htCmpFields);
                    #endregion

                    #region 检查DB库存是否足够
                    StringBuilder sbSQL = new StringBuilder("");
                    DataTable dtTmpStock;


                    for (int i = 0; i < dtCombinStorage.Rows.Count; i++)
                    {
                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.Append(" SELECT MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, BOXID, SUM(MENGE-REQTY+BKQTY) AS MENGE ");
                        sbSQL.Append(" FROM WHITM ");
                        sbSQL.Append(" WHERE (MANDT = '" + dtCombinStorage.Rows[i]["MANDT"].ToString() + "')");
                        sbSQL.Append("   AND (COMCD = '" + dtCombinStorage.Rows[i]["COMCD"].ToString() + "')");
                        sbSQL.Append("   AND (WERKS = '" + dtCombinStorage.Rows[i]["WERKS"].ToString() + "')");
                        sbSQL.Append("   AND (LGORT = '" + dtCombinStorage.Rows[i]["LGORT"].ToString() + "')");
                        sbSQL.Append("   AND (LOCAT = '" + dtCombinStorage.Rows[i]["LOCAT"].ToString() + "')");
                        sbSQL.Append("   AND (MATNR = '" + dtCombinStorage.Rows[i]["MATNR"].ToString() + "')");
                        sbSQL.Append("   AND (INSMK = '" + dtCombinStorage.Rows[i]["INSMK"].ToString() + "')");
                        sbSQL.Append("   AND (CHARG = '" + dtCombinStorage.Rows[i]["CHARG"].ToString() + "')");
                        sbSQL.Append("   AND (MBLNR = '" + dtCombinStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(SERNO,'') = '" + dtCombinStorage.Rows[i]["SERNO"].ToString() + "')");
                        sbSQL.Append("   AND (ISNULL(BOXID,'') = '" + dtCombinStorage.Rows[i]["BOXID"].ToString() + "')");
                        sbSQL.Append(" GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR, SERNO, BOXID ");

                        dtTmpStock = ControlQueryData(sbSQL.ToString());

                        if (dtTmpStock.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtTmpStock.Rows[0]["MENGE"].ToString()) < Convert.ToInt32(dtCombinStorage.Rows[i]["ALQTY"].ToString()))
                            {
                                ERRMSG = "物料重复出库，请确认!!<- AddOnLineOutData_Film()";
                                return false;
                            }
                        }
                        else
                        {
                            ERRMSG = "物料重复出库，请确认!!<- AddOnLineOutData_Film()";
                            return false;
                        }
                    }

                    #endregion

                    #endregion

                    #region WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //记录所有异动储位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        #region delete whitm(包括return)
                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        //alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        //alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["OMBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        alConditions.Add("(ISNULL(BOXID,'')= '" + dtStorage.Rows[i]["BOXID"].ToString() + "')");
                        if (arySQL.IndexOf(objWhitm.EntityGetDeleteSql(alConditions)) < 0)
                            arySQL.Add(objWhitm.EntityGetDeleteSql(alConditions));
                        #endregion
                    }
                    #endregion

                    #region WHHED
                    for (int i = 0; i < aryLocat.Count; i++)
                    {
                        sbSql.Remove(0, sbSql.Length);
                        sbSql.Append("Update WHHED set  ");
                        sbSql.AppendFormat("  LOSTS=T.TOTAL from ");
                        sbSql.AppendFormat(" (Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and COMCD='" + COMCD + "') as T  ");
                        sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat(" and WERKS='{0}' ", WERKS);
                        sbSql.AppendFormat(" and LGORT='{0}' ", LGORT);
                        sbSql.AppendFormat(" and LOCAT='{0}' ", aryLocat[i].ToString());
                        sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                        arySQL.Add(sbSql.ToString());

                        //sbSql.Remove(0, sbSql.Length);
                        //sbSql.Append("Update WHHED set  ");
                        //sbSql.AppendFormat("  ISMRG=T.ISMRG from ");
                        //sbSql.AppendFormat(" (Select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and COMCD='" + COMCD + "' and MRGID<>'') as T  ");
                        //sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                        //sbSql.AppendFormat(" and WERKS='{0}' ", WERKS);
                        //sbSql.AppendFormat(" and LGORT='{0}' ", LGORT);
                        //sbSql.AppendFormat(" and LOCAT='{0}' ", aryLocat[i].ToString());
                        //sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                        //arySQL.Add(sbSql.ToString());
                    }
                    #endregion

                    #region WHDWN
                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        #region 更新WHDWN
                        objWhdwn.ResetField();
                        objWhdwn.Otqty = "OTQTY +" + dtOutSource.Rows[i]["ALQTY"].ToString();

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");

                        arySQL.Add(objWhdwn.EntityGetUpdateSql(alConditions));
                        #endregion

                        #region Double Check WHDWN的單據是不是有被處理過了
                        alColumns.Clear();
                        alColumns.Add(" MBLNR + '--' + ZEILE ");

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + MANDT + "')");
                        alConditions.Add("(COMCD= '" + COMCD + "')");
                        alConditions.Add("(MBLNR= '" + dtOutSource.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(ZEILE= '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "')");
                        alConditions.Add("(MENGE < OTQTY+ " + dtOutSource.Rows[i]["ALQTY"].ToString() + ")");

                        aryCheckSQL.Add(objWhdwn.EntityGetQuerySql(alColumns, alConditions, false, true));
                        #endregion
                    }

                    #region 防呆，避免重複處理同一張SAP Document
                    DataTable dtChkTmp;

                    for (int j = 0; j < aryCheckSQL.Count; j++)
                    {
                        dtChkTmp = ControlQueryData(aryCheckSQL[j].ToString());
                        if (dtChkTmp.Rows.Count > 0)
                            aryCheckList.Add(dtChkTmp.Rows[0][0].ToString());
                    }

                    #endregion
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
                        ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                        bolReturn = ControlExeSqlStringArr(arySQL);
                    }
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddOnLineOutData_Film()";
                }

                return bolReturn;
            }


            #endregion

            #region 同步出库数据给ASRS

            public bool PostStorageOutDataToASRS(string Werks, string Lgort, DataTable dtStorage, string type)
            {

                QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                ArrayList arySQL = new ArrayList();
                bool bolResult = false;

                if (objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
                {
                    DataTable dtASRS = new DataTable();
                    dtASRS.TableName = "QWMS";

                    dtASRS.Columns.Add("TRN_NO", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("SEQ_NO", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("TRN_TYPE", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("LOC", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("ITEM_NO", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("STK", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("VER", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("VENDOR", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("QTY", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("PD_LINE", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("DEPT_NO", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("PLANT", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("PRIORITY", typeof(string)).DefaultValue = string.Empty;
                    dtASRS.Columns.Add("STORAGE_TYPE", typeof(string)).DefaultValue = string.Empty;

                    int j = 1;
                    string str_TrnNo = objInterface.CreateAsrsNo();

                    foreach (DataRow dr in dtStorage.Rows)
                    {
                        DataRow drASRS = dtASRS.NewRow();

                        drASRS["TRN_NO"] = str_TrnNo;
                        drASRS["SEQ_NO"] = objInterface.Createseq_no(j);
                        drASRS["TRN_TYPE"] = type;
                        drASRS["LOC"] = dr["LOCAT"];
                        drASRS["ITEM_NO"] = dr["MATNR"];
                        drASRS["STK"] = dr["INSMK"];
                        drASRS["VER"] = dr["CHARG"];
                        drASRS["VENDOR"] = dr["LIFNR"];
                        drASRS["QTY"] = dr["ALQTY"];
                        drASRS["PD_LINE"] = dr["ARBPL"];
                        drASRS["DEPT_NO"] = dr["EBELN"];
                        drASRS["PLANT"] = dr["WERKS"];
                        drASRS["PRIORITY"] = string.Empty;
                        drASRS["STORAGE_TYPE"] = dr["LGORT"];

                        dtASRS.Rows.Add(drASRS);
                        j++;
                    }
                    #region WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);
                    #endregion
                    string strXML = objInterface.ConvertDataTableToXML(dtASRS);
                    objLogData.XMLLog(strXML);
                    if (objInterface.PostStorageOutData(strXML) == "SUCCESS" ? true : false)
                    {
                        bolResult = true;
                    }
                }
                return bolResult;

            }

            #endregion

            #endregion

            #region "Common Function"
            public enum CmpAction { Sum, StringCombine, EmptyString, ZeroInt, OneInt, Count }
            ////////////將DataTable作GroupBy的動作 by Marc Hong/////////////////////////////////////////
            /// <summary>
            /// 將DataTable作GroupBy的動作
            /// </summary>
            /// <param name="varTargetTable">要Combine的raw data</param>
            /// <param name="varKeys">Combine時的Key</param>
            /// <param name="varCmpFields">Combine後要進行運算的欄位及做的運算</param>
            /// <returns>回傳值型態為DataTable，回傳Combine後的DataTable。</returns>
            /// <example>
            /// <code>
            ///  Inventory objInventory = new Inventory();
            ///  DataTable dtReturn = objInventory.CombineTable(dtPickListView,alKeys,htCmpFields);
            ///  Your Code Here......
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable CombineTable(DataTable varTargetTable, ArrayList varKeys, Hashtable varCmpFields)
            {

                DataTable dtReturn = varTargetTable.Clone();
                try
                {

                    if (varTargetTable == null || varTargetTable.Rows.Count < 1)
                    {
                        dtReturn = varTargetTable;
                        return dtReturn;
                    }
                    #region "Combine Pick List rawdata "


                    StringBuilder sbOrderBy = new StringBuilder();
                    for (int k = 0; k < varKeys.Count; k++)
                    {
                        if (sbOrderBy.Length != 0)
                            sbOrderBy.Append(",");
                        sbOrderBy.Append(varKeys[k].ToString().Trim());
                    }

                    DataRow[] arrTargetData = varTargetTable.Select("", sbOrderBy.ToString());

                    string strStdKey = "";
                    StringBuilder sbIndexKey = new StringBuilder();
                    StringBuilder sbCmbineKey = new StringBuilder();

                    int intTagDataCount = arrTargetData.Length;
                    for (int i = 0; i < intTagDataCount; i++)
                    {
                        sbIndexKey.Remove(0, sbIndexKey.Length);
                        for (int k = 0; k < varKeys.Count; k++)
                        {
                            if (sbIndexKey.Length != 0)
                                sbIndexKey.Append("+");
                            sbIndexKey.Append(arrTargetData[i][varKeys[k].ToString().Trim()].ToString().Trim());
                        }
                        if (strStdKey != sbIndexKey.ToString().Trim())
                        {
                            strStdKey = sbIndexKey.ToString().Trim();

                            sbCmbineKey.Remove(0, sbCmbineKey.Length);
                            for (int k = 0; k < varKeys.Count; k++)
                            {
                                if (sbCmbineKey.Length != 0)
                                    sbCmbineKey.Append(" and ");
                                sbCmbineKey.Append(" (" + varKeys[k].ToString().Trim() + " = '" + arrTargetData[i][varKeys[k].ToString().Trim()].ToString().Trim() + "') ");
                            }

                            DataRow[] arrCombine = varTargetTable.Select(sbCmbineKey.ToString(), sbOrderBy.ToString());
                            for (int k = 0; k < arrCombine.Length; k++)
                            {
                                if (k == 0)
                                {
                                    dtReturn.Rows.Add(arrCombine[k].ItemArray);
                                }
                                #region "聚合函式"
                                IEnumerator ieCmpField = varCmpFields.Keys.GetEnumerator();

                                while (ieCmpField.MoveNext())
                                {
                                    switch ((CmpAction)varCmpFields[ieCmpField.Current.ToString()])
                                    {
                                        case CmpAction.Sum:
                                            if (k != 0)
                                                dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = Convert.ToString(Convert.ToInt64(dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim()) + Convert.ToInt64(arrCombine[k][ieCmpField.Current.ToString().Trim()].ToString().Trim()));
                                            break;
                                        case CmpAction.StringCombine:
                                            if (k != 0)
                                                dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim() + arrCombine[k][ieCmpField.Current.ToString().Trim()].ToString().Trim();
                                            break;
                                        case CmpAction.EmptyString:
                                            dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "";
                                            break;
                                        case CmpAction.ZeroInt:
                                            dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "0";
                                            break;
                                        case CmpAction.OneInt:
                                            dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "1";
                                            break;
                                        case CmpAction.Count:
                                            if (k != 0)
                                                dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = Convert.ToString(Convert.ToInt64(dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()].ToString().Trim()) + 1);
                                            else
                                                dtReturn.Rows[dtReturn.Rows.Count - 1][ieCmpField.Current.ToString().Trim()] = "1";
                                            break;
                                    }
                                }
                                #endregion
                            }
                            dtReturn.AcceptChanges();
                        }
                    }

                    #endregion


                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- UpdateUserPassword "  ;
                    //throw new System.Exception(ex.Message +"<- UpdateUserPassword ");

                    ERRMSG = ex.Message + "<- CombineTable()";
                }
                return dtReturn;
            }


            #region 確認WHCTRL中的PRINT設定是否預設要列印 by Marc Hong
            ////////////確認WHCTRL中的PRINT設定是否預設要列印 by Marc Hong/////////////////////////////////////////
            /// <summary>
            /// 確認WHCTRL中的PRINT設定是否預設要列印 by Marc Hong
            /// </summary>
            /// <returns>回傳值型態為bool。</returns>
            /// <example>
            /// <code>
            ///  Inventory objInventory = new Inventory();
            ///  DataTable dtReturn = objInventory.CheckPrintCheckBox(dtPickListView,alKeys,htCmpFields);
            ///  Your Code Here......
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool CheckPrintCheckBox()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckPrintCheckBox";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                bool bolChecked = false;
                DataTable dtData = new DataTable();

                DataWhctrl objWhctrl = new DataWhctrl(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();

                alColumns.Clear();
                alColumns.Add(" * ");

                alConditions.Clear();
                alConditions.Add("(MANDT='" + MANDT + "')");
                alConditions.Add("(COMCD='" + COMCD + "')");
                alConditions.Add("(SOLDTO='QWMS')");
                alConditions.Add("(CTRLID='PRINT')");

                try
                {
                    dtData = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                    if (dtData.Rows.Count == 0)
                    {
                        bolChecked = false;
                    }
                    else
                    {
                        if (dtData.Rows[0]["CTRLNM"].ToString().ToUpper() == "Y")
                        {
                            bolChecked = true;
                        }
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


            #region  批次執行SQL 語法
            //====================================================================================
            ////////////Summary by Smose Liao 20091116////////////////////////////////////////////
            /// <summary>
            /// 批次執行SQL 語法。
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut ( strConnectionString,  strMandt,  strWerks,  strLgort,  strMblnr,  strProgid,  strCrnam)
            /// bool bolReturn =obj.ExecuteSQL();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public bool ExecuteSQL(ArrayList alSql)
            {
                bool bolReturn = false;
                try
                {
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(alSql);
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

            #endregion

            #region 废品出库合箱打印 Add By Zachary 20200107
            public DataTable GetWhdwnData(string Mandt, string Comcd, string Werks, string Lgort, string Mblnr, string Type)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWhdwnData";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + Mblnr + "," + Type + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();


                StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 ");

                if (Type == "SN")//SN
                {
                    string[] sArray = Mblnr.Split(':');
                    string sMblnr = sArray[sArray.Length - 1];
                    strSql.Append("SELECT MANDT,COMCD,WERKS,LGORT,SERNO AS MBLNR,MATNR,CHARG,MENGE,'N' AS FLAGE,BOXID,REGION FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 AND (MTYPE in( 'QMS_BGM','QMS_FAT')) ");
                    strSql.AppendFormat("AND (SERNO='{0}' OR SERNO='{1}') ", Mblnr, sMblnr);
                }
                else if (Type == "BOXID")//BOXID
                {
                    strSql.Append("SELECT MANDT,COMCD,WERKS,LGORT,SERNO AS MBLNR,MATNR,CHARG,MENGE,'N' AS FLAGE,BOXID,REGION FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 AND (MTYPE in( 'QMS_BGM','QMS_FAT')) ");
                    strSql.AppendFormat("AND BOXID='{0}' ", Mblnr);
                }
                else if (Type == "TIC")//判票
                {
                    strSql.Append("SELECT MANDT,COMCD,WERKS,(CASE WHEN ISNULL(OTLGT,'')<>'' THEN OTLGT ELSE LGORT END) AS LGORT,BOXID AS MBLNR,MATNR,CHARG,MENGE,'N' AS FLAGE,BOXID,REGION FROM WHTIC WITH(NOLOCK) WHERE  (MTYPE IN ('BPM_TIC','OA_TIC','SAP_TIC','SAP_PACK')) ");
                    strSql.AppendFormat("AND BOXID='{0}'", Mblnr);
                }
                strSql.AppendFormat("AND MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                //strSql.AppendFormat("AND LGORT='{0}' ", Lgort);

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
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

            #region 判断WASTE是否已有数据Mblnr Add By Zachary 20200108
            public DataTable JudgmentWhdwn(string Mandt, string Comcd, string Werks, string Lgort, string Mblnr, string Type)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "JudgmentWhdwn";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + Mblnr + "," + Type + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();


                StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 ");



                strSql.Append("SELECT MBLNR,BOXID FROM WASTE WITH(NOLOCK) WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                //strSql.AppendFormat("AND LGORT='{0}' ", Lgort);
                strSql.AppendFormat("AND MTYPE='{0}' ", Type);
                strSql.AppendFormat("AND MBLNR='{0}' AND FLAGE<>'X'", Mblnr);

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
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

            #region 废品数据插入到WASTE出库合箱打印 Add By Zachary 20200107
            public bool InsertWaste(DataTable daData, string BoxId, string Type)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InsertWaste";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool flg;
                ArrayList arySQL = new ArrayList();
                for (int i = 0; i < daData.Rows.Count; i++)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("INSERT INTO WASTE(MANDT,COMCD,WERKS,LGORT,MBLNR,MATNR,CHARG,MENGE,CRNAM,CRDAT,BOXID,FLAGE,MTYPE,REGION) ");
                    strSql.AppendFormat("VALUES('{0}', ", daData.Rows[i]["MANDT"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["COMCD"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["WERKS"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["LGORT"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["MBLNR"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["MATNR"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["CHARG"]);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["MENGE"]);
                    strSql.AppendFormat("'{0}', GETDATE(),", CRNAM);
                    strSql.AppendFormat("'{0}', ", BoxId);
                    strSql.AppendFormat("'{0}', ", daData.Rows[i]["FLAGE"]);               
                    strSql.AppendFormat("'{0}', ", Type);
                    //添加原产地信息
                    strSql.AppendFormat("'{0}' )", daData.Rows[i]["REGION"]);
                    arySQL.Add(strSql.ToString());

                }

                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlArray(arySQL);
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

                return flg;
            }

            #endregion

            #region 生成BoxId Add By Zachary 20200108

            public string CreateBoxId(string strWerks)
            {
                this.ControlMethodName = "CreateBoxId";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSQL = new StringBuilder();
                
                string BoxId;
                string Type = "CC" + strWerks;
                strSQL.AppendFormat("EXEC SP_CreateBoxId '{0}'", Type);
                try
                {
                    ControlHandleDB();
                    BoxId = ControlSqlAccess.GetFieldValue(strSQL.ToString());
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
                return BoxId;
            }


            #endregion

            #region 根据BoxId查询WASTE数据 Add By Zachary 20200108
            public DataTable GetWasteData(string Mandt, string Comcd, string Werks, string Lgort, string BoxId)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWasteData";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + BoxId + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();


                StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 ");



                strSql.Append("SELECT '' AS ITEM,MANDT,COMCD,WERKS,LGORT,MBLNR,MATNR,CHARG,MENGE,CRNAM,CRDAT,MONAM,MODAT,BOXID,FLAGE,REMARK,REGION FROM WASTE WITH(NOLOCK) WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                //strSql.AppendFormat("AND LGORT='{0}' ", Lgort);
                strSql.AppendFormat("AND BOXID='{0}' AND FLAGE<>'X' ", BoxId);
                strSql.AppendFormat("ORDER BY MATNR ");


                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
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

            #region 根据BoxId修改WASTE数据状态 Add By Zachary 20200108
            public bool UpdateWasteStatus(string Mandt, string Comcd, string Werks, string Lgort, string BoxId)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateWasteStatus";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + BoxId + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                bool flg;


                StringBuilder strSql = new StringBuilder();
                //strSql.Append("SELECT * FROM WHDWN WITH(NOLOCK) WHERE OTQTY>0 ");



                strSql.Append("UPDATE WASTE SET FLAGE='Y', ");
                strSql.AppendFormat("MONAM='{0}',MODAT= GETDATE() WHERE ", CRNAM);
                strSql.AppendFormat("MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                strSql.AppendFormat("AND LGORT='{0}' ", Lgort);
                strSql.AppendFormat("AND BOXID='{0}' ", BoxId);

                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSql(strSql.ToString());
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

                return flg;
            }

            #endregion

            #region BOXID解绑(删除Waste表数据) Add By Zachary 20201019
            public bool DeleteWaste(DataTable dtData, string Type,string Flage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteWaste";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flg;
                ArrayList arySQL = new ArrayList();
                ArrayList arySQL2 = new ArrayList();
                try
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.AppendFormat("DELETE FROM WASTE WHERE ");
                        strSql.AppendFormat("MANDT='{0}' ", dtData.Rows[i]["MANDT"]);
                        strSql.AppendFormat("AND COMCD='{0}' ", dtData.Rows[i]["COMCD"]);
                        strSql.AppendFormat("AND WERKS='{0}' ", dtData.Rows[i]["WERKS"]);
                        strSql.AppendFormat("AND MBLNR='{0}' ", dtData.Rows[i]["MBLNR"]);
                        strSql.AppendFormat("AND FLAGE='{0}'",Flage );
                        arySQL2.Add(strSql.ToString());
                    }
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlArray(arySQL2);
                    if (!flg)
                        return flg;
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.AppendFormat("UPDATE WASTE SET FLAGE='X',MONAM='{0}',MODAT=GETDATE()  WHERE ", CRNAM);
                        strSql.AppendFormat("MANDT='{0}' ", dtData.Rows[i]["MANDT"]);
                        strSql.AppendFormat("AND COMCD='{0}' ", dtData.Rows[i]["COMCD"]);
                        strSql.AppendFormat("AND WERKS='{0}' ", dtData.Rows[i]["WERKS"]);
                        if (dtData.Rows[i]["MBLNR"].ToString() != "")
                            strSql.AppendFormat("AND MBLNR='{0}' ", dtData.Rows[i]["MBLNR"]);
                        else
                            strSql.AppendFormat("AND MATNR='{0}' AND BOXID='{1}' ", dtData.Rows[i]["MATNR"], dtData.Rows[i]["BOXID"]);
                        strSql.AppendFormat("AND MTYPE='{0}' ", Type);
                        arySQL.Add(strSql.ToString());
                    }

                    flg = ControlSqlAccess.ExecSqlArray(arySQL);
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

                return flg;
            }

            #endregion

            #region 获取废品调拨数据 Add By Zachary 20201228/   增加厂区: 可刷入单号带出WHTIC下全部数据
            public DataTable GetWhticData(string Mandt, string Comcd, string Werks, string Lgort, string BoxIdMblnr, string ToLgort,string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWhticData";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + BoxIdMblnr + "," + ToLgort + "," + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                StringBuilder strSql = new StringBuilder();
                if (strType == "BOXID")
                {
                    strSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT AS OldLGORT,'{0}' AS NowLGORT,'{1}' AS ToLgort,MBLNR,MATNR,BOXID,INSMK,CHARG,LIFNR,TRNTP,KOSTL,MENGE FROM WHTIC WITH(NOLOCK) WHERE ", Lgort, ToLgort);
                    strSql.AppendFormat("MANDT='{0}' ", Mandt);
                    strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                    strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                    strSql.AppendFormat("AND (CASE WHEN ISNULL(OTLGT,'')<>'' THEN OTLGT ELSE LGORT END )='{0}' ", Lgort);
                    strSql.AppendFormat("AND BOXID='{0}' ", BoxIdMblnr);
                }
                else if (strType == "MBLNR")
                {
                    strSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT AS OldLGORT,'{0}' AS NowLGORT,'{1}' AS ToLgort,MBLNR,MATNR,BOXID,INSMK,CHARG,LIFNR,TRNTP,KOSTL,MENGE FROM WHTIC WITH(NOLOCK) WHERE ", Lgort, ToLgort);
                    strSql.AppendFormat("MANDT='{0}' ", Mandt);
                    strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                    strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                    strSql.AppendFormat("AND (CASE WHEN ISNULL(OTLGT,'')<>'' THEN OTLGT ELSE LGORT END )='{0}' ", Lgort);
                    strSql.AppendFormat("AND MBLNR='{0}' ", BoxIdMblnr);
                }

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
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

            #region 维护废品调拨仓别 Add By Zachary 20201228
            public bool UpdateToLgort(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateToLgort";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flg;
                ArrayList arySQL = new ArrayList();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    StringBuilder strSql = new StringBuilder();
                    StringBuilder sbSql = new StringBuilder();
                    strSql.AppendFormat("UPDATE WHTIC SET OTLGT='{0}',MODAT=GETDATE()  WHERE ", dtData.Rows[i]["TOLGORT"]);
                    strSql.AppendFormat("MANDT='{0}' ", dtData.Rows[i]["MANDT"]);
                    strSql.AppendFormat("AND COMCD='{0}' ", dtData.Rows[i]["COMCD"]);
                    strSql.AppendFormat("AND WERKS='{0}' ", dtData.Rows[i]["WERKS"]);
                    strSql.AppendFormat("AND (CASE WHEN ISNULL(OTLGT,'')<>'' THEN OTLGT ELSE LGORT END )='{0}' ", dtData.Rows[i]["NowLGORT"]);
                    strSql.AppendFormat("AND BOXID='{0}' ", dtData.Rows[i]["BOXID"]);
                    arySQL.Add(strSql.ToString());

                    sbSql.AppendFormat("INSERT INTO WHLOG(MANDT,WERKS,LGORT,CGCLS,MATNR,BOXID,CHARG,LIFNR,TRNTP,MENGE,INSMK,KOSTL,MBLNR,CRNAM,CRDAT,COMCD,RMAK1) ");
                    sbSql.AppendFormat("VALUES('{0}', ", dtData.Rows[i]["MANDT"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["WERKS"]);
                    sbSql.AppendFormat("'{0}', 'R3',", dtData.Rows[i]["NowLGORT"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["MATNR"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["BOXID"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["CHARG"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["LIFNR"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["TRNTP"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["MENGE"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["INSMK"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["KOSTL"]);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["MBLNR"]);
                    sbSql.AppendFormat("'{0}', GETDATE(),", CRNAM);
                    sbSql.AppendFormat("'{0}', ", dtData.Rows[i]["COMCD"]);
                    sbSql.AppendFormat("'{0}') ", dtData.Rows[i]["TOLGORT"]);
                    arySQL.Add(sbSql.ToString());



                }
                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlArray(arySQL);
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

                return flg;
            }

            #endregion

            #region 获取客责报废录入信息 Add By Ash 20210825
            public DataTable GetWasteDataSelect_PL(string Mandt, string Comcd, string Werks, string Lgort, string Matnr, string strDate,string strEndDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWasteDataSelect_PL";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + Matnr + "," + strDate + "," + strEndDate + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                //int Menge = Convert.ToInt32(strMenge);
                DataTable dtData = new DataTable();
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,MBLNR,MATNR,CHARG,MENGE,CRNAM,CRDAT,MONAM,MODAT,BOXID,FLAGE,REMARK FROM WASTE WITH(NOLOCK) WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                strSql.AppendFormat("AND LGORT='{0}' ", Lgort);
                strSql.AppendFormat("AND MATNR='{0}' ", Matnr);
                //strSql.AppendFormat("AND MENGE={0} ", Menge);
                strSql.AppendFormat("AND CRDAT BETWEEN '{0}' ", strDate);
                strSql.AppendFormat("AND '{0}' ", strEndDate);
                //strSql.AppendFormat("AND CRDAT BETWEEN CONVERT(NVARCHAR(23),'{0}',121) AND CONVERT(NVARCHAR(23),DATEADD(HOUR,+1,'{0}'),121) ", strDate, strDate);
                strSql.AppendFormat("AND FLAGE='N' ");
                strSql.AppendFormat("AND MTYPE='PAL' ");
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
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

            #region 根据料号、时间和数量删除信息 Add By Ash 20210825
            public bool DeleteWasteData_PL(string Mandt, string Comcd, string Werks, string Lgort, string Matnr, string strDate,string strEndDate,string Menge)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteWasteData_PL";
                this.ControlMethodParm = "(" + Mandt + "," + Comcd + "," + Werks + "," + Lgort + "," + Matnr + "," + strDate +","+ Menge+ ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flg;
                //int Menge = Convert.ToInt32(strMenge);
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("Delete FROM WASTE WHERE ");
                strSql.AppendFormat("MANDT='{0}' ", Mandt);
                strSql.AppendFormat("AND COMCD='{0}' ", Comcd);
                strSql.AppendFormat("AND WERKS='{0}' ", Werks);
                strSql.AppendFormat("AND LGORT='{0}' ", Lgort);
                strSql.AppendFormat("AND MATNR='{0}' ", Matnr);
                strSql.AppendFormat("AND MENGE={0} ", Menge);
                strSql.AppendFormat("AND CRDAT BETWEEN '{0}' ", strDate);
                strSql.AppendFormat("AND '{0}' ", strEndDate);
                //strSql.AppendFormat("AND CRDAT BETWEEN CONVERT(NVARCHAR(23),'{0}',121) AND CONVERT(NVARCHAR(23),DATEADD(HOUR,+1,'{0}'),121) ", strDate, strDate);
                strSql.AppendFormat("AND FLAGE='N' ");
                strSql.AppendFormat("AND MTYPE='PAL' ");

                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return flg;
            }
            #endregion

            #region 生成boxidWASTE_AGV 流水号 Add By Ash 20210825
            public string CreateWasteBoxid_PL(string strWerks, string strBty)
            {
                this.ControlMethodName = "CreateWasteBoxid_PL";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSQL = new StringBuilder();
                string BoxId_AGV;
                int strBoxty = Convert.ToInt32(strBty);
                string Type = "CC" + strWerks;
                strSQL.AppendFormat("EXEC [SP_CreateWasteBoxId] '" + Type + "','" + strBoxty + "'");
                try
                {
                    ControlHandleDB();
                    BoxId_AGV = ControlSqlAccess.GetFieldValue(strSQL.ToString());
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
                return BoxId_AGV;
            }
            #endregion

            #region 保存客责报废信息 Add By Ash 20210825
            public bool InsertWasteData_PL(string Mandt, string Comcd, string Werks, string Lgort, string Matnr, string strMenge, string BoxId, string Bty)
            {
                this.ControlMethodName = "InsertWasteData_PL";
                this.ControlMethodParm = "( )";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool flg;
                int Menge = Convert.ToInt32(strMenge);

                int bty = Convert.ToInt32(Bty);
                ArrayList arySQL = new ArrayList();

                for (int i = 0; i < bty; i++)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("INSERT INTO WASTE(MANDT,COMCD,WERKS,LGORT,MBLNR,MATNR,CHARG,MENGE,CRNAM,CRDAT,BOXID,FLAGE,MTYPE) ");
                    strSql.AppendFormat("VALUES('{0}', ", Mandt);
                    strSql.AppendFormat("'{0}', ", Comcd);
                    strSql.AppendFormat("'{0}', ", Werks);
                    strSql.AppendFormat("'{0}', ", Lgort);
                    strSql.AppendFormat("'{0}', ", BoxId);
                    strSql.AppendFormat("'{0}', ", Matnr);
                    strSql.AppendFormat("'', ");
                    strSql.AppendFormat("'{0}', ", Menge);
                    strSql.AppendFormat("'{0}', ", CRNAM);
                    strSql.AppendFormat("GETDATE(), ");
                    strSql.AppendFormat("'{0}', ", BoxId);
                    strSql.AppendFormat("'N', ");
                    strSql.AppendFormat("'PAL')");
                    BoxId = BoxId.Substring(0, 12) + (Convert.ToInt32(BoxId.Substring(12, 7)) + 1).ToString("0000000");
                    arySQL.Add(strSql.ToString());
                }
                try
                {
                    ControlHandleDB();
                    flg = ControlSqlAccess.ExecSqlArray(arySQL);
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

                return flg;
            }
            #endregion

            #region 
            public DataTable GetSimulationNumBeginEnd(string strCtrlnm, string strType,int step)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSimulationNum";
                this.ControlMethodParm = "(" + strCtrlnm + "," + strType + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                DataTable dtSerno = new DataTable();

                try
                {
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    if (strCtrlnm == "FlyPlan")//祥天計畫
                    {
                        //strSQL = "EXEC SP_GetSerialNum_Fly 'SimulationDoc', 'FLYPLAN'"; 20230425 Lora
                        strSQL = "EXEC SP_SimulationOut_GetSerialNum 'SimulationDoc', 'FLYPLAN','" + step + "'";
                    }

                    if (strCtrlnm == "AGV") //智能料架出库
                    {
                        strSQL = "EXEC SP_SimulationOut_GetAGVTaskNo 'AGV', 'TASKNO','" + step + "'";
                    }

                    ControlHandleDB();
                    dtSerno = ControlSqlAccess.GetDataTable(strSQL);
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

                return dtSerno;
            }
            #endregion

            #region 祥龙DateCode保留出库的Grrno
            public string GetSimulationGrrno()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSimulationGrrno";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSQL = "";
                string strGrrno = "";

                try
                {
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    strSQL = "EXEC SP_GetSerialNum_NEW 'GoodIssue', 'OTAUT'";
                    ControlHandleDB();
                    string SERNO = ControlSqlAccess.GetFieldValue(strSQL);
                    ControlSqlAccess.CloseConnection();

                    DateTime dtnow = DateTime.Now;
                    string CMonth = dtnow.Month.ToString();
                    string CDay = dtnow.Day.ToString();
                    string strSERNO = "";
                    if (CMonth.Length < 2)
                    {
                        CMonth = Convert.ToString("0") + dtnow.Month;
                    }
                    if (CDay.Length < 2)
                    {
                        CDay = Convert.ToString("0") + dtnow.Day;
                    }
                    if (strGrrno.Length < 3)
                    {
                        strSERNO = Convert.ToString("00") + SERNO;
                    }
                    else
                    {
                        strSERNO = SERNO;
                    }
                    string CYear = Convert.ToString(dtnow.Year);
                    strGrrno = CYear.Substring(2, 2) + CMonth + CDay + strSERNO;
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

                return strGrrno;
            }
            #endregion

            #region 改写ProduceSimulationData，虚拟单号获取起始单号
            /// <summary>
            /// 改写ProduceSimulationData，虚拟单号获取起始单号
            /// </summary>
            /// <param name="strOutSource"></param>
            /// <param name="Type">SMT</param>
            /// <param name="Category">QWMS/ASRS</param>
            /// <param name="strFunction">NORMAL/ADD</param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public string ProduceSimulationData(string strOutSource, string Type, string Category, string strFunction, string strSernoByLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ProduceSimulationData";
                this.ControlMethodParm = "(" + Type + "," + Category + "," + strFunction + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                string strResult = string.Empty;

                StringBuilder sbSql = new StringBuilder();
                if (Category == "QWMS")
                {
                    sbSql.Append("DECLARE @Restult varchar(1) ");
                    sbSql.AppendFormat("EXEC SP_SimulationOut_ProduceByLgort '{0}','{1}','{2}','{3}','{4}','{5}',@Restult OUT", strOutSource, Type, Category, strFunction, strSernoByLgort, UserData.UserId);
                    sbSql.Append(" SELECT @Restult ");
                }
                try
                {
                    ControlHandleDB();
                    strResult = ControlSqlAccess.GetFieldValue(sbSql.ToString());
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

            #region DateCode整料：整合发料数据
            /// <summary>
            /// DateCode整料：整合发料数据 
            /// </summary>
            /// <param name="strSmtStock">Confirm库存数据</param>
            /// <returns></returns>
            public DataTable CombineSimulationComfirmData(string strSmtStock)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CombineSimulationComfirmData";
                this.ControlMethodParm = "(" + strSmtStock + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);
                DataTable dtResult = new DataTable();

                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("EXEC SP_SimulationOut_CombineConfirmData '{0}'", strSmtStock);
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            /// <summary>
            /// 祥龙DateCode出库
            /// </summary>
            /// <param name="strOutSource"></param>
            /// <param name="strStorage"></param>
            /// <returns></returns>
            public string AddSinmulationOutSaveData(string strOutSource, string strStorage, string strLgortJson)
            {
                string strResult = string.Empty;
                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append(" DECLARE @Result VARCHAR(1) ");
                    sbSql.AppendFormat(" EXEC SP_SimulationOut_Save_DateCode '{0}','{1}','{2}','{3}','{4}',@Result OUT ", strOutSource, strStorage, strLgortJson, strProgid, UserData.UserId);
                    sbSql.Append(" SELECT @Result ");
                    ControlHandleDB();
                    strResult = ControlSqlAccess.GetFieldValue(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddSinmulationOutSaveData()";
                }

                return strResult;
            }

            #region TWW 外包仓出库
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dtData"></param>
            /// <returns></returns>
            public bool AddTwwStorageOutSaveData(string strWerks, string strLgort, string strMblnr, DataTable dtData) {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddOnLineOutData_Picasso";
                this.ControlMethodParm = "(' " + strWerks + "','" + strLgort + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                // 查询数据库对应数据是否已经处理
                DataWhtww whtww = new DataWhtww(this.UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                // 添加查询列
                alColumns.Add("*");
                // 添加查询条件
                alConditions.Add(" WERKS = '" + strWerks + "'");
                alConditions.Add(" MBLNR = '" + strMblnr + "'");
                alConditions.Add(" OTQTY >= MENGE");
                // 查询数据
                DataTable dtProcessed = whtww.EntityQuery(alColumns, alConditions, false);
                // 检查是否存在已经处理的数据
                if (dtProcessed.AsEnumerable().Any(a => a.Field<string>("OMFLG").Equals("Y"))) {
                    CommonObjectsException ex = new CommonObjectsException("303", "当前单据存在已扣账部分，请确认单据状态是否正常！");
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }
                // 开始链接
                ControlHandleDB();
                // 保存数据
                // 记录log
                #region 未操作数据写入异动，删除库存
                if (!dtProcessed.AsEnumerable().Any(a => !string.IsNullOrEmpty(a.Field<string>("BWART")))) {
                    #region 写入WHLOG表
                    foreach (DataRow dr in dtData.Rows) {
                        DataWhlog objWhlog = new DataWhlog(this.UserData);
                        objWhlog.Mandt = dr["MANDT"].ToString().Trim();
                        objWhlog.Werks = dr["WERKS"].ToString().Trim();
                        objWhlog.Lgort = dr["LGORT"].ToString().Trim();
                        objWhlog.Cgcls = "98";
                        objWhlog.Oloca = dr["LOCAT"].ToString().Trim();
                        objWhlog.Nloca = dr["UMLOC"].ToString().Trim();
                        objWhlog.Matnr = dr["MATNR"].ToString().Trim();
                        objWhlog.Charg = dr["CHARG"].ToString().Trim();
                        objWhlog.Lifnr = dr["LIFNR"].ToString().Trim();
                        objWhlog.Trntp = dr["INSMK"].ToString().Trim() + "-";
                        objWhlog.Mblnr = dr["MBLNR"].ToString().Trim();
                        objWhlog.Ombln = dr["PONUM"].ToString().Trim() + dr["GRNUM"].ToString().Trim();
                        objWhlog.Ebeln = "";
                        objWhlog.Menge = "-" + dr["MENGE"].ToString().Trim();
                        objWhlog.Insmk = dr["INSMK"].ToString().Trim();
                        objWhlog.Kostl = dr["KOSTL"].ToString().Trim();
                        objWhlog.Arbpl = "";
                        objWhlog.Mrgid = "";
                        objWhlog.Crnam = UserData.UserId;
                        objWhlog.Crdat = DateTime.Now.ToString("yyyyMMddHHmmss");
                        objWhlog.Rmak2 = "";
                        objWhlog.Rmak3 = "";
                        objWhlog.Serno = dr["SERNO"].ToString().Trim();
                        objWhlog.Comcd = dr["COMCD"].ToString().Trim();
                        objWhlog.Locod = dr["LOCOD"].ToString().Trim();
                        objWhlog.Dacod = dr["VEDAT"].ToString().Trim();
                        objWhlog.Vedat = dr["DACOD"].ToString().Trim();
                        objWhlog.Boxid = dr["BOXID"].ToString().Trim();
                        objWhlog.Indat = dr["INDAT"].ToString().Trim();
                        // 检查是否存在已记录的数据
                        ArrayList alColWhlog = new ArrayList();
                        alColWhlog.Add("*");
                        ArrayList alCondWhlog = new ArrayList();
                        alCondWhlog.Add(" WERKS = '" + objWhlog.Werks + "'");
                        alCondWhlog.Add(" LGORT = '" + objWhlog.Lgort + "'");
                        alCondWhlog.Add(" MBLNR = '" + objWhlog.Mblnr + "'");
                        alCondWhlog.Add(" TRNTP = '" + objWhlog.Trntp + "'");
                        alCondWhlog.Add(" OLOCA = '" + objWhlog.Oloca + "'");
                        // 删除已存在的记录
                        if (objWhlog.EntityQuery(alColWhlog, alCondWhlog, false).Rows.Count > 0) {
                            objWhlog.EntityDelete(alCondWhlog);
                        }
                        string sql = objWhlog.EntityGetInsertSql();
                        ControlSqlAccess.ExecSql(sql);
                    }
                    #endregion
                    #region 更新WHITM库存数据
                    List<string> lsSqls = new List<string>();
                    dtData.AsEnumerable().ToList().ForEach(f => {
                        DataWhitm objWhitm = new DataWhitm(this.UserData);
                        // 创建查询条件
                        ArrayList alColWhitm = new ArrayList();
                        alColWhitm.Add("*");
                        ArrayList alCondWhitm = new ArrayList();
                        alCondWhitm.Add(" WERKS = '" + strWerks + "'");
                        alCondWhitm.Add(" LGORT = '" + strLgort + "'");
                        //alCondWhitm.Add(" MBLNR = '" + f["MBLNR"].ToString().Trim() + "'");
                        alCondWhitm.Add(" LOCAT = '" + f["LOCAT"].ToString().Trim() + "'");
                        alCondWhitm.Add(" INSMK = '" + f["INSMK"].ToString().Trim() + "'");
                        alCondWhitm.Add(" MATNR = '" + f["MATNR"].ToString().Trim() + "'");
                        alCondWhitm.Add(" CHARG = '" + f["CHARG"].ToString().Trim() + "'");
                        // whhed 储位状态表
                        DataWhhed objWhhed = new DataWhhed(this.UserData);
                        objWhhed.Losts = "0";
                        ArrayList alHedCond = new ArrayList();
                        alHedCond.Add(" WERKS = '" + strWerks + "'");
                        alHedCond.Add(" LGORT = '" + strLgort + "'");
                        alHedCond.Add(" LOCAT = '" + f["LOCAT"].ToString().Trim() + "'");
                        bool isEmptyLocat = true;
                        // 查询数量
                        // 查看是否可出完
                        DataTable dtWhitm = objWhitm.EntityQuery(alColWhitm, alCondWhitm, false);
                        // 获取当前出库需求数量
                        int locatMenge = Convert.ToInt32(f.Field<int>("MENGE").ToString());
                        // 获取到的数据可能为多行
                        dtWhitm.AsEnumerable().ToList().ForEach(e => {
                            if (locatMenge > 0) {
                                decimal stockMenge =  Convert.ToInt32(e.Field<decimal>("MENGE"));
                                // 更改比较方式
                                if (locatMenge >= stockMenge) {
                                    locatMenge = locatMenge - (int)stockMenge;
                                    stockMenge = 0;
                                } else {
                                    stockMenge -= locatMenge;
                                    locatMenge = 0;
                                }
                                ArrayList itemCondition = new ArrayList();
                                itemCondition.AddRange(alCondWhitm);
                                itemCondition.Add(" MBLNR = '" + e["MBLNR"].ToString().Trim() + "'");
                                // 判断是否已扣减完成
                                if (stockMenge <= 0) {
                                    lsSqls.Add(objWhitm.EntityGetDeleteSql(itemCondition));
                                } else if(stockMenge > 0) {
                                    objWhitm.Menge = stockMenge.ToString();
                                    lsSqls.Add(objWhitm.EntityGetUpdateSql(itemCondition));
                                    // 储位未出完，储位不为空
                                    isEmptyLocat = false;
                                }
                            }
                        });
                        if(locatMenge > 0) {
                            throw new Exception("储位：" + f.Field<string>("LOCAT").Trim() + ",料号：" + f.Field<string>("MATNR").Trim() + "库存不足！");
                        }
                        // 添加储位更新状态
                        if (isEmptyLocat) {
                            lsSqls.Add(objWhhed.EntityGetUpdateSql(alHedCond));
                        }
                    });
                    lsSqls.ForEach(f => {
                        ControlSqlAccess.ExecSql(f);
                    });
                    #endregion
                }
                #endregion
                #region 更新WHTWW表
                StringBuilder sbTww = new StringBuilder();
                foreach (DataRow dr in dtData.Rows) {
                    DataWhtww objWhtww = new DataWhtww(this.UserData);
                    // 更新BWART栏位，表示已填写完成
                    objWhtww.COMCD = dr["COMCD"].ToString().Trim();
                    //objWhtww.BWART = dr["BWART"].ToString().Trim();
                    objWhtww.MONAM = this.UserData.UserId;
                    objWhtww.MODAT = DateTime.Now.ToString("yyyyMMddHHmmss");
                    objWhtww.LGORT = dr["LGORT"].ToString().Trim();
                    objWhtww.OTQTY = dr["MENGE"].ToString().Trim();
                    objWhtww.UMLGO = dr["UMLGO"].ToString().Trim();
                    objWhtww.UMLOC = dr["UMLOC"].ToString().Trim();
                    objWhtww.GRNUM = dr["GRNUM"].ToString().Trim();
                    objWhtww.PONUM = dr["PONUM"].ToString().Trim();
                    objWhtww.KOSTL = dr["KOSTL"].ToString().Trim();
                    objWhtww.OMFLG = "";
                    // 创建查询条件
                    ArrayList alCondTww = new ArrayList();
                    alCondTww.Add(" WERKS = '" + dr["WERKS"].ToString() + "'");
                    alCondTww.Add(" MBLNR = '" + dr["MBLNR"].ToString() + "'");
                    alCondTww.Add(" MATNR = '" + dr["MATNR"].ToString() + "'");
                    alCondTww.Add(" ZEILE = '" + dr["ZEILE"].ToString() + "'");
                    alCondTww.Add(" LOCAT = '" + dr["LOCAT"].ToString() + "'");
                    objWhtww.EntityUpdate(alCondTww);
                }
                #endregion

                // 执行SQL更新异动代码
                DataWhtww dataWhtww = new DataWhtww(this.UserData);
                dataWhtww.BWART = dtData.AsEnumerable().Select(s => s.Field<string>("BWART")).FirstOrDefault();
                // 按照MBLNR更新异动代码
                ArrayList alMblnrCond = new ArrayList();
                alMblnrCond.Add(" WERKS = '" + strWerks + "'");
                alMblnrCond.Add(" MBLNR = '" + strMblnr + "'");
                alMblnrCond.Add(" IEFLG = 'E'");
                dataWhtww.EntityUpdate(alMblnrCond);

                ControlSqlAccess.CloseConnection();
                return true;
            }
            #endregion

            public string wsAddOnLineOutData_311_New(DataTable dtStorage) 
            {
                string strResult = "";
                try
                {

                    ArrayList aryCheckSQL = new ArrayList();
                    ArrayList aryCheckList = new ArrayList();
                    StringBuilder sbCheckList = new StringBuilder();

                    ArrayList arySQL = new ArrayList();
                    ArrayList arySQL1 = new ArrayList();
                    ArrayList aryLocat = new ArrayList();

                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);

                    DataWhhed objWhhed = new DataWhhed(UserData);
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhdwn objWhdwn = new DataWhdwn(UserData);
                    DataWhgrr objWhgrr = new DataWhgrr(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string CYear = "";
                    string CMonth = "";
                    string CDay = "";

                    //WHLOG
                    arySQL = objLogData.AddLogData("", "", dtStorage);

                    #region 取得撿料單號

                    string strSql = "EXEC SP_GetSerialNum 'GoodIssue', 'OTAUT'";
                    ControlHandleDB();
                    string strPickNo = ControlSqlAccess.GetFieldValue(strSql);
                    ControlSqlAccess.CloseConnection();

                    //20100115  Smose Liao 取得出庫單號(GRRNO)
                    //出庫單格式：年月日 + 2碼流水號 
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT);

                    strPickNo = objStorageData.GetGrrno("GoodIssue", "OTAUT");

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
                    //strGRRNO = dtnow.Year + CMonth + CDay + strSERNO; 
                    CYear = Convert.ToString(dtnow.Year);
                    strPickNo = CYear.Substring(2, 2) + CMonth + CDay + strPickNo;

                    #endregion

                    //WHITM
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //記錄所有的異動儲位
                        if (aryLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper()) < 0)
                        {
                            aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString().ToUpper());
                        }

                        #region //先Update
                        objWhitm.ResetField();
                        objWhitm.Bkqty = "BKQTY - " + dtStorage.Rows[i]["MENGE"].ToString();
                        objWhitm.Monam = UserData.UserId;
                        objWhitm.Modat = "GetDate()";

                        alConditions.Clear();
                        alConditions.Add("(MANDT= '" + dtStorage.Rows[i]["MANDT"].ToString() + "')");
                        alConditions.Add("(COMCD= '" + dtStorage.Rows[i]["COMCD"].ToString() + "')");
                        alConditions.Add("(WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "')");
                        alConditions.Add("(LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "')");
                        alConditions.Add("(LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "')");
                        alConditions.Add("(MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "')");
                        alConditions.Add("(INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "')");
                        alConditions.Add("(MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "')");
                        alConditions.Add("(CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "')");
                        //成品
                        alConditions.Add("(ISNULL(SERNO,'')= '" + dtStorage.Rows[i]["SERNO"].ToString() + "')");
                        //DateCode
                        alConditions.Add("(ISNULL(DACOD,'')= '" + dtStorage.Rows[i]["DACOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(LOCOD,'')= '" + dtStorage.Rows[i]["LOCOD"].ToString() + "')");
                        alConditions.Add("(ISNULL(INSPT,'')= '" + dtStorage.Rows[i]["INSPT"].ToString() + "')");
                        alConditions.Add(" MENGE='" + dtStorage.Rows[i]["MENGE"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));


                        #region 新增撿料單(WHGRR)

                        objWhgrr.ResetField();
                        objWhgrr.Grrno = strPickNo;
                        objWhgrr.Itemnum = ((int)(i + 1)).ToString("000");
                        objWhgrr.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                        objWhgrr.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhgrr.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhgrr.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhgrr.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhgrr.Mblnr = dtStorage.Rows[i]["OMBLN"].ToString();
                        //objWhgrr.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhgrr.Ombln = dtStorage.Rows[i]["MBLNR"].ToString();
                        //objWhgrr.Ombln = dtStorage.Rows[i]["OMBLN"].ToString();
                        objWhgrr.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhgrr.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhgrr.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhgrr.Serno = "";
                        objWhgrr.Dacod = dtStorage.Rows[i]["DACOD"].ToString();
                        objWhgrr.Locod = dtStorage.Rows[i]["LOCOD"].ToString();
                        objWhgrr.Inspt = dtStorage.Rows[i]["INSPT"].ToString();
                        objWhgrr.Lifnr = dtStorage.Rows[i]["LIFNR"].ToString();
                        objWhgrr.Indat = dtStorage.Rows[i]["INDAT"].ToString();
                        objWhgrr.Vedat = dtStorage.Rows[i]["VEDAT"].ToString();
                        objWhgrr.Kdmat = dtStorage.Rows[i]["KDMAT"].ToString();
                        objWhgrr.Bkqty = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhgrr.Alqty = dtStorage.Rows[i]["MENGE"].ToString();
                        objWhgrr.Blace = "0";
                        objWhgrr.Barcode = strPickNo + ((int)(i + 1)).ToString("000");
                        objWhgrr.Prtyp = "0";
                        objWhgrr.Ctrlnm = "OTAUT";


                        arySQL.Add(objWhgrr.EntityGetInsertSql());

                        #endregion

                        #endregion
                    }

                    ControlSqlAccess.TimeOut = 300; //連線SQL Server的時間拉長到5分鐘
                    bool bolReturn = ControlExeSqlStringArr(arySQL);
                    if (bolReturn)
                        strResult = strPickNo;
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- UpdateUserPassword "  ;
                    //throw new System.Exception(ex.Message +"<- UpdateUserPassword ");

                    ERRMSG = ex.Message + "<- AddOnLineOutData()";
                }
                return strResult;

            }

        }
    }
}
