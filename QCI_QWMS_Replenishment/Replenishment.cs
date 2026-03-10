using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.Data;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// Replenishment 的摘要描述。
        /// </summary>
        public class Replenishment : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strProgid = "";
            private string strCrnam = "";
            private string strErrmsg = "";

            #region Constructer

            public Replenishment()
            {
            }

            #region 通过UserInfo，Progid初始化QCI.QWMS.Replenishment
            ////////////Summary by Bruce Zhang 20091030////////////////////////////////////////////
            /// <summary>
            /// 通过UserInfo，Progid初始化QCI.QWMS.Replenishment
            /// </summary>
            /// <param name="varUserData"></param>
            /// <param name="strProgid"></param>
            public Replenishment(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
            }
            #endregion

            #region 通过UserInfo，Werks,Lgort,Progid初始化QCI.QWMS.Replenishment
            ////////////Summary by Bruce Zhang 20091030////////////////////////////////////////////
            /// <summary>
            /// 通过UserInfo，Werks,Lgort,Progid初始化QCI.QWMS.Replenishment
            /// </summary>
            /// <param name="varUserData"></param>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strProgid"></param>
            public Replenishment(UserInfo varUserData, string strWerks, string strLgort, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort, strProgid)
            {
            }
            #endregion

            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Replenishment物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishmenta =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Replenishment(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                PROGID = strProgid;
                CRNAM = varUserData.UserId;

                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.Replenishment";
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






            ////////////Summary byRock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Replenishment物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishmenta =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Replenishment(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = strWerks;
                LGORT = strLgort;
                PROGID = strProgid;
                CRNAM = varUserData.UserId;

                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.Replenishment";
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
            /// SAP Client。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
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
            /// 廠區。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string WERKS
            {
                get { return strWerks; }
                set { strWerks = value; }
            }

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

            #endregion

            #region MemberFunction

            #region 检查使用者是否有使用补货作业功能的权限
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091030////////////////////////////////////////////
            /// <summary>
            /// 检查使用者是否有使用补货作业功能的权限
            /// </summary> 
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  bool  bolReturn = objStorageIn.CheckAuthority();
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
            #endregion

            #region 查询需要产生补货单的资料
            ////////////Summary by Bruce Zhang 20091030////////////////////////////////////////////
            /// <summary>
            /// 查询需要产生补货单的资料
            /// </summary>
            /// <returns>DataTable</returns>
            public DataTable QueryNeedReplenishment()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryNeedReplenishment";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //strSQL = "Select T.MANDT, T.WERKS, T.LGORT, T.LOCAT, '' as MBLNR, '' as ZEILE, T.MATNR, T.INSMK, T.CHARG, '' as LIFNR, isnull((T.MAXGE-T.BLACE),0) as MENGE, 0 as ALQTY, '' as KOSTL, '' as ARBPL, '' as TRNTP, T.MAXGE, T.REPOT, T.BLACE, T.TOTAL from (select WHREP.*, isnull((Select sum(WHITM.MENGE-WHITM.REQTY) from WHITM where WHITM.MANDT=WHREP.MANDT and WHITM.WERKS=WHREP.WERKS and WHITM.LGORT=WHREP.LGORT and WHITM.LOCAT=WHREP.LOCAT and WHITM.MATNR=WHREP.MATNR and WHITM.INSMK=WHREP.INSMK and WHITM.CHARG=WHREP.CHARG and WHITM.MRGID='' group by MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG),0) as BLACE, isnull((Select sum(WHITM.MENGE-WHITM.REQTY) from WHITM where WHITM.MANDT=WHREP.MANDT and WHITM.WERKS=WHREP.WERKS and WHITM.LGORT=WHREP.LGORT and WHITM.MATNR=WHREP.MATNR and WHITM.INSMK=WHREP.INSMK and WHITM.CHARG=WHREP.CHARG and WHITM.MRGID='' and WHITM.LOCAT not in (Select LOCAT from WHREP where MANDT=WHREP.MANDT and WERKS=WHREP.WERKS and LGORT=WHREP.LGORT and MATNR=WHREP.MATNR and INSMK=WHREP.INSMK and CHARG=WHREP.CHARG) group by MANDT, WERKS, LGORT,  MATNR, INSMK, CHARG),0) as TOTAL from WHREP where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "') as T where T.REPOT>T.BLACE order by T.LOCAT, T.MATNR";
                    StringBuilder sbSQL = new StringBuilder();
                    DataTable dtData = new DataTable();
                    sbSQL.Append(" Select T.MANDT, T.COMCD, T.WERKS, T.LGORT, T.LOCAT, '' as MBLNR, '' as ZEILE, T.MATNR, T.INSMK, T.CHARG, '' as LIFNR, isnull((T.MAXGE-T.BLACE),0) as MENGE")
                        .Append(", 0 as ALQTY, '' as KOSTL, '' as ARBPL, '' as TRNTP, T.MAXGE, T.REPOT, T.BLACE, T.TOTAL from")
                        .Append("  (select WHREP.*, isnull((Select sum(WHITM.MENGE-WHITM.REQTY) from WHITM WITH (NOLOCK) where WHITM.MANDT=WHREP.MANDT and WHITM.COMCD=WHREP.COMCD and WHITM.WERKS=WHREP.WERKS and WHITM.LGORT=WHREP.LGORT and  WHITM.LOCAT=WHREP.LOCAT and WHITM.MATNR=WHREP.MATNR and WHITM.INSMK=WHREP.INSMK and WHITM.CHARG=WHREP.CHARG and WHITM.MRGID='' group by MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG),0) as BLACE")
                        .Append(", isnull((Select sum(WHITM.MENGE-WHITM.REQTY) from WHITM WITH (NOLOCK) where WHITM.MANDT=WHREP.MANDT and WHITM.COMCD=WHREP.COMCD and WHITM.WERKS=WHREP.WERKS and WHITM.LGORT=WHREP.LGORT and WHITM.MATNR=WHREP.MATNR and WHITM.INSMK=WHREP.INSMK and WHITM.CHARG=WHREP.CHARG and WHITM.MRGID='' and WHITM.LOCAT not in (Select LOCAT from WHREP WITH (NOLOCK) where MANDT=WHREP.MANDT and COMCD=WHREP.COMCD and WERKS=WHREP.WERKS and LGORT=WHREP.LGORT and MATNR=WHREP.MATNR and INSMK=WHREP.INSMK and CHARG=WHREP.CHARG) group by MANDT, WERKS, LGORT,  MATNR, INSMK, CHARG),0) as TOTAL ")
                        .AppendFormat(" from WHREP WITH (NOLOCK) where MANDT='{0}' and COMCD='{1}' and WERKS='{2}' and LGORT='{3}') as T", MANDT, COMCD, WERKS, LGORT)
                        .Append(" where T.REPOT>T.BLACE order by T.LOCAT, T.MATNR");

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

            #region 查询仓库中可出库的资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091030////////////////////////////////////////////
            /// <summary>
            /// 查询仓库中可出库的资料
            /// </summary> 
            /// <param name="strLocat"></param>
            /// <param name="dtOutSource"></param>
            /// <returns>
            /// DataSet
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataSet  dsData = objStorageData.QueryReplenishmentOutData(strLocat, dtOutSource);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataSet QueryReplenishmentOutData(DataTable dtOutSource)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryReplenishmentOutData";
                this.ControlMethodParm = "('" + dtOutSource + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtStorage = new DataTable();
                    DataRow drRow;
                    StringBuilder sbSQL = new StringBuilder();
                    string strSqlWhere = "";

                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        int intSourceMenge = int.Parse(dtOutSource.Rows[i]["MENGE"].ToString());

                        //strSQL = "Select MANDT, WERKS, LGORT, LOCAT, '" + dtOutSource.Rows[i]["LOCAT"].ToString() + "' as TOLOC, MBLNR, '" + dtOutSource.Rows[i]["ZEILE"].ToString() + "' as ZEILE, MBLNR as OMBLNR, MATNR, INSMK, CHARG, (MENGE - REQTY) as MENGE, REQTY, 0 as ALQTY, MRGID,'" +
                        //    dtOutSource.Rows[i]["KOSTL"].ToString() + "' as KOSTL, '" + dtOutSource.Rows[i]["ARBPL"].ToString() + "' as ARBPL, '" + dtOutSource.Rows[i]["TRNTP"].ToString() + "' as TRNTP, EBELN, LIFNR, RMAK1, INDAT from WHITM where MANDT= '" + dtOutSource.Rows[i]["MANDT"].ToString() +
                        //    "' and WERKS = '" + dtOutSource.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtOutSource.Rows[i]["LGORT"].ToString() + "' and not exists (select * from WHREP where WHREP.MANDT=WHITM.MANDT and WHREP.WERKS=WHITM.WERKS and WHREP.LGORT=WHITM.LGORT and WHREP.LOCAT=WHITM.LOCAT and WHREP.MATNR=WHITM.MATNR and WHREP.INSMK=WHITM.INSMK and WHREP.CHARG=WHITM.CHARG) and MATNR= '" + dtOutSource.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtOutSource.Rows[i]["INSMK"].ToString() + "' and CHARG= '" +
                        //    dtOutSource.Rows[i]["CHARG"].ToString() + "'  and MRGID= '' order by INDAT, CRDAT";

                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.AppendFormat("Select MANDT, COMCD, WERKS, LGORT, LOCAT, '{0}' as TOLOC, MBLNR, '{1}' as ZEILE, MBLNR as OMBLNR, MATNR, INSMK, CHARG, (MENGE - REQTY) as MENGE, REQTY, 0 as ALQTY", dtOutSource.Rows[i]["LOCAT"].ToString(), dtOutSource.Rows[i]["ZEILE"].ToString())
                            .AppendFormat(", MRGID,'{0}' as KOSTL, '{1}' as ARBPL, '{2}' as TRNTP, EBELN, LIFNR, RMAK1, INDAT from WHITM WITH (NOLOCK) where MANDT= '{3}'", dtOutSource.Rows[i]["KOSTL"].ToString(), dtOutSource.Rows[i]["ARBPL"].ToString(), dtOutSource.Rows[i]["TRNTP"].ToString(), dtOutSource.Rows[i]["MANDT"].ToString())
                            .AppendFormat(" and COMCD='{0}' and WERKS ='{1}' and LGORT= '{2}' and not exists (select * from WHREP WITH (NOLOCK) where WHREP.MANDT=WHITM.MANDT and COMCD=WHITM.COMCD and WHREP.WERKS=WHITM.WERKS and WHREP.LGORT=WHITM.LGORT and WHREP.LOCAT=WHITM.LOCAT and WHREP.MATNR=WHITM.MATNR and WHREP.INSMK=WHITM.INSMK and WHREP.CHARG=WHITM.CHARG) and MATNR= '{3}'", dtOutSource.Rows[i]["COMCD"].ToString(), dtOutSource.Rows[i]["WERKS"].ToString(), dtOutSource.Rows[i]["LGORT"].ToString(), dtOutSource.Rows[i]["MATNR"].ToString())
                            .AppendFormat(" and INSMK= '{0}' and CHARG= '{1}'  and MRGID= '' order by INDAT, CRDAT", dtOutSource.Rows[i]["INSMK"].ToString(), dtOutSource.Rows[i]["CHARG"].ToString());

                        DataTable dtData = new DataTable();
                        try
                        {
                            //dtData = this.objSQLAccess.GetDataTable(strSQL);
                            ControlHandleDB();
                            dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                            ControlSqlAccess.CloseConnection();
                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- QueryReplenishmentOutData()";
                        }

                        int intLocatMenge = 0;

                        if (i == 0)
                        {
                            dtStorage = dtData.Clone();
                        }

                        for (int j = 0; j < dtData.Rows.Count; j++)
                        {
                            if (intLocatMenge + int.Parse(dtData.Rows[j]["MENGE"].ToString()) <= intSourceMenge)
                            {
                                intLocatMenge += int.Parse(dtData.Rows[j]["MENGE"].ToString());
                                dtData.Rows[j]["ALQTY"] = dtData.Rows[j]["MENGE"].ToString();
                            }
                            else
                            {
                                dtData.Rows[j]["ALQTY"] = intSourceMenge - intLocatMenge;
                                intLocatMenge += intSourceMenge - intLocatMenge;
                            }
                            if (dtData.Rows[j]["ALQTY"].ToString() != "0")
                            {
                                drRow = dtStorage.NewRow();
                                drRow["MANDT"] = dtData.Rows[j]["MANDT"].ToString();
                                drRow["COMCD"] = dtData.Rows[j]["COMCD"].ToString();
                                drRow["WERKS"] = dtData.Rows[j]["WERKS"].ToString();
                                drRow["LGORT"] = dtData.Rows[j]["LGORT"].ToString();
                                drRow["LOCAT"] = dtData.Rows[j]["LOCAT"].ToString();
                                drRow["TOLOC"] = dtData.Rows[j]["TOLOC"].ToString();
                                drRow["MATNR"] = dtData.Rows[j]["MATNR"].ToString();
                                drRow["INSMK"] = dtData.Rows[j]["INSMK"].ToString();
                                drRow["CHARG"] = dtData.Rows[j]["CHARG"].ToString();
                                drRow["MENGE"] = dtData.Rows[j]["MENGE"].ToString();
                                drRow["REQTY"] = dtData.Rows[j]["REQTY"].ToString();
                                drRow["ALQTY"] = dtData.Rows[j]["ALQTY"].ToString();
                                drRow["MBLNR"] = dtData.Rows[j]["MBLNR"].ToString();
                                drRow["ZEILE"] = dtData.Rows[j]["ZEILE"].ToString();
                                drRow["EBELN"] = dtData.Rows[j]["EBELN"].ToString();
                                drRow["LIFNR"] = dtData.Rows[j]["LIFNR"].ToString();
                                drRow["OMBLNR"] = dtData.Rows[j]["OMBLNR"].ToString();
                                drRow["MRGID"] = dtData.Rows[j]["MRGID"].ToString();
                                drRow["KOSTL"] = dtData.Rows[j]["KOSTL"].ToString();
                                drRow["ARBPL"] = dtData.Rows[j]["ARBPL"].ToString();
                                drRow["TRNTP"] = dtData.Rows[j]["TRNTP"].ToString();
                                drRow["RMAK1"] = dtData.Rows[j]["RMAK1"].ToString();
                                drRow["INDAT"] = dtData.Rows[j]["INDAT"].ToString();
                                dtStorage.Rows.Add(drRow);
                            }
                        }
                        dtOutSource.Rows[i]["ALQTY"] = intLocatMenge;
                    }

                    DataSet dsData = new DataSet();
                    DataTable dtData1;
                    DataTable dtData2;
                    dtData1 = dtOutSource.Copy();
                    dtData2 = dtStorage.Copy();
                    dtData1.TableName = "Source";
                    dtData2.TableName = "Destination";
                    dsData.Tables.Add(dtData1);
                    dsData.Tables.Add(dtData2);
                    return dsData;
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

            #region Generate Replenishment Order
            ////////////Summary by Bruce Zhang 20091102////////////////////////////////////////////
            /// <summary>
            ///  生成补货单订单
            /// </summary> 
            /// <param name="dtStorage"></param>
            /// <returns>
            /// bolReturn
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  bool bolReturn = objStorageData.GenerateReplenishmentOrder(dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool GenerateReplenishmentOrder(ref DataTable dtStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GenerateReplenishmentOrder";
                this.ControlMethodParm = "('" + dtStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    DataWhres objWhres = new DataWhres(UserData);
                    ArrayList alCondition = new ArrayList();
                    ArrayList arySQL = new ArrayList();
                    bool bolReturn = false;
                    string strResno = "";
                    strResno = GenerateReplenishmentNo();
                    dtStorage.Columns.Add("RESNO");
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        dtStorage.Rows[i]["RESNO"] = strResno;
                    }

                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);

                    //WHLOG
                    arySQL = objLogData.AddReplenishmentLogData(dtStorage);

                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        //strSQL = "update WHITM set REQTY=REQTY+" + dtStorage.Rows[i]["ALQTY"].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "' and WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "' and LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MBLNR='" + dtStorage.Rows[i]["MBLNR"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "' and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString() + "' ";

                        objWhitm.Reqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhitm.Monam = strCrnam;
                        objWhitm.Modat = "GetDate()";
                        alCondition.Clear();
                        alCondition.Add("MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                        alCondition.Add("COMCD='" + dtStorage.Rows[i]["COMCD"].ToString() + "'");
                        alCondition.Add("WERKS= '" + dtStorage.Rows[i]["WERKS"].ToString() + "'");
                        alCondition.Add("LGORT= '" + dtStorage.Rows[i]["LGORT"].ToString() + "'");
                        alCondition.Add("LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "'");
                        alCondition.Add("MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "'");
                        alCondition.Add("MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'");
                        alCondition.Add("INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "'");
                        alCondition.Add("CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alCondition));

                        //strSQL = "insert into WHRES(RESNO, MANDT, WERKS, LGORT, LOCAT, TOLOC, MBLNR, MATNR, INSMK, CHARG, MENGE, ALQTY, STATUS, CRNAM, CRDAT, MONAM, MODAT) values('" + strResno + "', '" + dtStorage.Rows[i]["MANDT"].ToString() + "', '" + dtStorage.Rows[i]["WERKS"].ToString() + "', '" + dtStorage.Rows[i]["LGORT"].ToString() + "', '" + dtStorage.Rows[i]["LOCAT"].ToString() + "', '" + dtStorage.Rows[i]["TOLOC"].ToString() + "', '" + dtStorage.Rows[i]["MBLNR"].ToString() + "', '" + dtStorage.Rows[i]["MATNR"].ToString() + "', '" + dtStorage.Rows[i]["INSMK"].ToString() + "', '" + dtStorage.Rows[i]["CHARG"].ToString() + "', '" + dtStorage.Rows[i]["ALQTY"].ToString() + "', '" + dtStorage.Rows[i]["ALQTY"].ToString() + "', '0', '" + strCrnam + "', getdate(), '" + strCrnam + "', getdate())";


                        objWhres.ResetField();
                        objWhres.Resno = strResno;
                        objWhres.Mandt = dtStorage.Rows[i]["MANDT"].ToString();
                        objWhres.Comcd = dtStorage.Rows[i]["COMCD"].ToString();
                        objWhres.Werks = dtStorage.Rows[i]["WERKS"].ToString();
                        objWhres.Lgort = dtStorage.Rows[i]["LGORT"].ToString();
                        objWhres.Locat = dtStorage.Rows[i]["LOCAT"].ToString();
                        objWhres.Toloc = dtStorage.Rows[i]["TOLOC"].ToString();
                        objWhres.Mblnr = dtStorage.Rows[i]["MBLNR"].ToString();
                        objWhres.Matnr = dtStorage.Rows[i]["MATNR"].ToString();
                        objWhres.Insmk = dtStorage.Rows[i]["INSMK"].ToString();
                        objWhres.Charg = dtStorage.Rows[i]["CHARG"].ToString();
                        objWhres.Menge = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhres.Alqty = dtStorage.Rows[i]["ALQTY"].ToString();
                        objWhres.Status = "0";
                        objWhres.Crnam = strCrnam;
                        objWhres.Crdat = "getdate()";
                        objWhres.Monam = strCrnam;
                        objWhres.Modat = "getdate()";

                        arySQL.Add(objWhres.EntityGetInsertSql());
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

            #region Generate Replenishment NO
            ////////////Summary by Bruce Zhang 20091102////////////////////////////////////////////
            /// <summary>
            /// 生成补货单单号
            /// </summary> 
            /// <returns>
            /// string
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  string strReturn = objStorageData.GenerateReplenishmentNo();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public string GenerateReplenishmentNo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GenerateReplenishmentOrder";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    string strResno = "";
                    //strSQL = "Select Convert(varchar(8), getdate(), 112) + isnull(right('000'+cast(max(convert(integer,right(RESNO,3)))+1 as varchar),3),'001') as RESNO from WHRES where MANDT='" + MANDT + "' and left(RESNO, 8)=Convert(varchar(8), getdate(), 112)";
                    DataTable dtData = new DataTable();

                    DataWhres objDataWhres = new DataWhres(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    alColumns.Add("Convert(varchar(8), getdate(), 112) + isnull(right('000'+cast(max(convert(integer,right(RESNO,3)))+1 as varchar),3),'001') as RESNO");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("left(RESNO, 8)=Convert(varchar(8), getdate(), 112)");

                    dtData = objDataWhres.EntityQuery(alColumns, alCondition, false);
                    strResno = dtData.Rows[0]["RESNO"].ToString();

                    return strResno;

                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());

                    //ControlSqlAccess.CloseConnection();
                    //return dtData;
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

            #region 查询 ReplenishmentStatus
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091104////////////////////////////////////////////
            /// <summary>
            /// 查询 ReplenishmentStatus
            /// </summary>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable dtReturn = objReplenishment.GetReplenishmentStatus();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable GetReplenishmentStatus()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetReplenishmentStatus";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "select CTRLNM, CTRLC1, CTRLNM+':'+CTRLC1 as F_TEXT from WHCTRL where MANDT='QCI' and SOLDTO='QWMS' and CTRLID='REPLENISH_STATUS'";

                    DataTable dtData = new DataTable();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("CTRLNM");
                    alColumns.Add("CTRLC1");
                    alColumns.Add("CTRLNM+':'+CTRLC1 as F_TEXT");

                    alCondition.Clear();
                    alCondition.Add("MANDT='QCI'");
                    //alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("SOLDTO='QWMS'");
                    alCondition.Add("CTRLID='REPLENISH_STATUS'");
                    dtData = objWhctrl.EntityQuery(alColumns, alCondition, false, true);
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

            #region 查询 ReplenishmentHeader 资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091104////////////////////////////////////////////
            /// <summary>
            /// 查询 ReplenishmentHeader 资料
            /// </summary> 
            /// <param name="strStatus">状态</param>
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="strResno">补货单号</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable dtReturn = objReplenishment.QueryReplenishmentHeader(strStatus, strLocat, strMatnr, strResno);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryReplenishmentHeader(string strStatus, string strLocat, string strMatnr, string strResno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetReplenishmentStatus";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "select RESNO, MANDT, WERKS, LGORT, TOLOC, MATNR, INSMK, STATUS, sum(MENGE) as MENGE from WHRES where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "'";

                    //if (strResno != "")
                    //{
                    //    strSQL += " and RESNO = '" + strResno + "'";
                    //}

                    //if (strStatus != "")
                    //{
                    //    strSQL += " and STATUS = '" + strStatus + "'";
                    //}

                    //if (strLocat != "")
                    //{
                    //    strSQL += " and LOCAT = '" + strLocat + "'";
                    //}

                    //if (strMatnr != "")
                    //{
                    //    strSQL += " and MATNR like '" + strMatnr + "%'";
                    //}

                    //strSQL += " group by RESNO, MANDT, WERKS, LGORT, TOLOC, MATNR, INSMK, STATUS order by RESNO";

                    DataTable dtData = new DataTable();
                    DataWhres objWhres = new DataWhres(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("RESNO");
                    alColumns.Add("MANDT");
                    alColumns.Add("COMCD");
                    alColumns.Add("WERKS");
                    alColumns.Add("LGORT");
                    alColumns.Add("TOLOC");
                    alColumns.Add("MATNR");
                    alColumns.Add("INSMK");
                    alColumns.Add("STATUS");
                    alColumns.Add("sum(MENGE) as MENGE");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS= '" + strWerks + "'");
                    if (strResno != "")
                    {
                        alCondition.Add("RESNO='" + strResno + "'");
                    }

                    if (strStatus != "")
                    {
                        alCondition.Add("STATUS='" + strStatus + "'");
                    }

                    if (strLocat != "")
                    {
                        alCondition.Add("LOCAT='" + strLocat + "'");
                    }

                    if (strMatnr != "")
                    {
                        alCondition.Add("MATNR like '" + strMatnr + "%'");
                    }
                    alCondition.Add("LGORT= '" + strLgort + "' group by RESNO, MANDT,COMCD, WERKS, LGORT, TOLOC, MATNR, INSMK, STATUS");

                    dtData = objWhres.EntityQuery(alColumns, alCondition, false, true);
                    dtData = CommonInfo.SortDataTable(dtData, "RESNO");
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

            #region 查询ReplenishmentItem资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091104////////////////////////////////////////////
            /// <summary>
            /// 查询ReplenishmentItem资料
            /// </summary> 
            /// <param name="strResno">补货单号</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable dtReturn = objReplenishment.QueryReplenishmentItem(strResno);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryReplenishmentItem(string strResno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryReplenishmentItem";
                this.ControlMethodParm = "('" + strResno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "select * from WHRES where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "'";

                    DataTable dtData = new DataTable();
                    DataWhres objWhres = new DataWhres(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("*");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS= '" + WERKS + "'");
                    alCondition.Add("LGORT= '" + LGORT + "'");

                    if (strResno != "")
                    {
                        alCondition.Add("RESNO= '" + strResno + "'");
                    }
                    dtData = objWhres.EntityQuery(alColumns, alCondition, false, true);
                    dtData = CommonInfo.SortDataTable(dtData, "LOCAT, MATNR");
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

            #region 更改补货单状态
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091104////////////////////////////////////////////
            /// <summary>
            /// 更改补货单状态
            /// </summary>
            /// <param name="arrResno">补货单号</param>
            /// <param name="arrMandt">Client</param>
            /// <param name="arrWerks">厂区</param>
            /// <param name="arrLgort">仓别</param>
            /// <param name="arrLocat">储位</param>
            /// <param name="arrToloc">调拨到的储位</param>
            /// <param name="arrMblnr">扣帐编号</param>
            /// <param name="arrMatnr">料号</param>
            /// <param name="arrInsmk">Stock</param>
            /// <param name="arrCharg">版本</param>
            /// <param name="arrMenge">Qty</param>
            /// <param name="arrAlqty">Storage Out Qty</param>
            /// <param name="strStatus">Status</param>
            /// <returns></returns>
            public bool UpdateReplenishmentStatus(ArrayList arrResno, ArrayList arrMandt, ArrayList arrComcd, ArrayList arrWerks, ArrayList arrLgort, ArrayList arrLocat, ArrayList arrToloc, ArrayList arrMblnr, ArrayList arrMatnr, ArrayList arrInsmk, ArrayList arrCharg, ArrayList arrMenge, ArrayList arrAlqty, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateReplenishmentStatus";
                this.ControlMethodParm = "('" + arrResno + "','" + arrMandt + "','" + arrWerks + "','" + arrLgort + "','" + arrLocat + "','" + arrToloc + "','" + arrMblnr + "','" + arrMatnr + "','" + arrInsmk + "','" + arrCharg + "','" + arrMenge + "','" + arrAlqty + "','" + strStatus + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    bool bolReturn = false;
                    ArrayList arySQL = new ArrayList();
                    StringBuilder sbSQL = new StringBuilder();
                    string strTempLocat = "";
                    ArrayList arrTempLocat = new ArrayList();
                    LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                    arySQL = objLogData.AddReplenishmentLogData(arrResno, arrMandt, arrComcd, arrWerks, arrLgort, arrLocat, arrToloc, arrMblnr, arrMatnr, arrInsmk, arrCharg, arrAlqty);
                    try
                    {
                        //Post
                        if (strStatus == "1")
                        {
                            //WHRES
                            for (int i = 0; i < arrResno.Count; i++)
                            {


                                if (strTempLocat.IndexOf(arrLocat[i].ToString()) < 0)
                                {
                                    arrTempLocat.Add(arrLocat[i].ToString());
                                }
                                //arySQL.Add("update WHITM set MENGE=MENGE-" + arrAlqty[i].ToString() + ", REQTY=REQTY-" + arrMenge[i].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrLocat[i].ToString() + "' and MATNR= '" + arrMatnr[i].ToString() + "' and INSMK= '" + arrInsmk[i].ToString() + "' and CHARG= '" + arrCharg[i].ToString() + "' and MBLNR= '" + arrMblnr[i].ToString() + "'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.AppendFormat("update WHITM set MENGE=MENGE-{0},REQTY=REQTY-{1}, MONAM='{2}', MODAT=getdate()", arrAlqty[i].ToString(), arrMenge[i].ToString(), strCrnam)
                                    .AppendFormat(" where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and INSMK= '{6}' and CHARG= '{7}' and MBLNR= '{8}'", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString(), arrMatnr[i].ToString(), arrInsmk[i].ToString(), arrCharg[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                                //strSQL = "Update WHITM set MENGE=MENGE + " + arrAlqty[i].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrToloc[i].ToString() + "' and MATNR= '" + arrMatnr[i].ToString() + "' and INSMK= '" + arrInsmk[i].ToString() + "' and CHARG= '" + arrCharg[i].ToString() + "'  and MBLNR= '" + arrMblnr[i].ToString() + "';" +
                                //    "IF @@ROWCOUNT = 0 " +
                                //    "Insert into WHITM(MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, LIFNR, EBELN, INDAT, MENGE, QCQTY, REFNO, MRGID, ISPTM, RMAK1,CRNAM, CRDAT, MONAM, MODAT) " +
                                //    " Select '" + arrMandt[i].ToString() + "', '" + arrWerks[i].ToString() + "','" + arrLgort[i].ToString() + "','" +
                                //    arrToloc[i].ToString() + "','" + arrMatnr[i].ToString() + "','" + arrInsmk[i].ToString() + "','" + arrMblnr[i].ToString() + "','" +
                                //    arrCharg[i].ToString() + "', LIFNR, EBELN, INDAT," +
                                //    arrAlqty[i].ToString() + ", 0, '', '', '','Replenishment','" + CRNAM + "', getdate() ,'" + CRNAM + "', getdate() from WHITM where MANDT='" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrLocat[i].ToString() + "' and MATNR= '" + arrMatnr[i].ToString() + "' and INSMK= '" + arrInsmk[i].ToString() + "' and CHARG= '" + arrCharg[i].ToString() + "' and MBLNR= '" + arrMblnr[i].ToString() + "';";

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.AppendFormat("update WHITM set MENGE=MENGE+{0}, MONAM='{1}', MODAT=getdate()", arrAlqty[i].ToString(), strCrnam)
                                    .AppendFormat(" where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and INSMK= '{6}' and CHARG= '{7}' and MBLNR= '{8}';", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrToloc[i].ToString(), arrMatnr[i].ToString(), arrInsmk[i].ToString(), arrCharg[i].ToString(), arrMblnr[i].ToString())
                                    .Append("IF @@ROWCOUNT = 0 ")
                                    .Append("Insert into WHITM(MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, MBLNR, CHARG, LIFNR, EBELN, INDAT, MENGE, QCQTY, REFNO, MRGID, ISPTM, RMAK1,CRNAM, CRDAT, MONAM, MODAT) ")
                                    .AppendFormat("Select '{0}','{1}','{2}','{3}','{4}',", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrToloc[i].ToString())
                                    .AppendFormat("'{0}','{1}','{2}','{3}',", arrMatnr[i].ToString(), arrInsmk[i].ToString(), arrMblnr[i].ToString(), arrCharg[i].ToString())
                                    .AppendFormat("LIFNR, EBELN, INDAT,{0}, 0, '', '', '','Replenishment','{1}', getdate() ,'{2}', getdate()", arrAlqty[i].ToString(), CRNAM, CRNAM)
                                    .AppendFormat(" from WHITM where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and INSMK= '{6}' and CHARG= '{7}' and MBLNR= '{8}';", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString(), arrMatnr[i].ToString(), arrInsmk[i].ToString(), arrCharg[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                                //arySQL.Add("Delete from WHITM where MANDT= '" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrLocat[i].ToString() +
                                //    "' and MATNR= '" + arrMatnr[i].ToString() + "'and MBLNR= '" + arrMblnr[i].ToString() + "' and MENGE= '0'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.Append("Delete from WHITM ")
                                    .AppendFormat(" where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and MBLNR= '{6}' and MENGE= '0'", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString(), arrMatnr[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                                //arySQL.Add("update WHRES set ALQTY='" + arrAlqty[i].ToString() + "', STATUS='" + strStatus + "', MONAM='" + CRNAM + "', MODAT=getdate() where RESNO='" + arrResno[i].ToString() + "' and MANDT='" + arrMandt[i].ToString() + "' and WERKS='" + arrWerks[i].ToString() + "' and LGORT='" + arrLgort[i].ToString() + "' and LOCAT='" + arrLocat[i].ToString() + "' and MBLNR='" + arrMblnr[i].ToString() + "' and MATNR='" + arrMatnr[i].ToString() + "' and STATUS='0'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.AppendFormat("update WHRES set ALQTY='{0}', STATUS='{1}', MONAM='{2}', MODAT=getdate() ", arrAlqty[i].ToString(), strStatus, CRNAM)
                                     .AppendFormat(" where RESNO='{0}' and MANDT='{1}' and COMCD='{2}' and WERKS= '{3}' and LGORT= '{4}' and LOCAT= '{5}' and MATNR= '{6}' and MBLNR= '{7}' and STATUS='0'", arrResno[i].ToString(), arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString().ToString(), arrMatnr[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                            }
                        }
                        //Cancel
                        if (strStatus == "2")
                        {
                            //WHRES
                            for (int i = 0; i < arrResno.Count; i++)
                            {

                                //
                                if (strTempLocat.IndexOf(arrLocat[i].ToString()) < 0)
                                {
                                    arrTempLocat.Add(arrLocat[i].ToString());
                                }
                                //arySQL.Add("update WHITM set REQTY=REQTY-" + arrMenge[i].ToString() + ", MONAM='" + strCrnam + "', MODAT=getdate() where MANDT='" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrLocat[i].ToString() + "' and MATNR= '" + arrMatnr[i].ToString() + "' and INSMK= '" + arrInsmk[i].ToString() + "' and CHARG= '" + arrCharg[i].ToString() + "' and MBLNR= '" + arrMblnr[i].ToString() + "'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.AppendFormat("update WHITM set REQTY=REQTY-{0}, MONAM='{1}', MODAT=getdate()", arrMenge[i].ToString(), strCrnam)
                                    .AppendFormat(" where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and INSMK= '{6}' and CHARG= '{7}' and MBLNR= '{8}'", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString(), arrMatnr[i].ToString(), arrInsmk[i].ToString(), arrCharg[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                                //
                                //arySQL.Add("Delete from WHITM where MANDT= '" + arrMandt[i].ToString() + "' and WERKS= '" + arrWerks[i].ToString() + "' and LGORT= '" + arrLgort[i].ToString() + "' and LOCAT= '" + arrLocat[i].ToString() +
                                //    "' and MATNR= '" + arrMatnr[i].ToString() + "'and MBLNR= '" + arrMblnr[i].ToString() + "' and MENGE= '0'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.Append("Delete from WHITM ")
                                    .AppendFormat(" where MANDT='{0}' and COMCD='{1}' and WERKS= '{2}' and LGORT= '{3}' and LOCAT= '{4}' and MATNR= '{5}' and MBLNR= '{6}' and MENGE= '0'", arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString(), arrMatnr[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                                //arySQL.Add("update WHRES set ALQTY='" + arrAlqty[i].ToString() + "', STATUS='" + strStatus + "', MONAM='" + CRNAM + "', MODAT=getdate() where RESNO='" + arrResno[i].ToString() + "' and MANDT='" + arrMandt[i].ToString() + "' and WERKS='" + arrWerks[i].ToString() + "' and LGORT='" + arrLgort[i].ToString() + "' and LOCAT='" + arrLocat[i].ToString() + "' and MBLNR='" + arrMblnr[i].ToString() + "' and MATNR='" + arrMatnr[i].ToString() + "' and STATUS='0'");

                                sbSQL.Remove(0, sbSQL.Length);
                                sbSQL.AppendFormat("update WHRES set ALQTY='{0}', STATUS='{1}', MONAM='{2}', MODAT=getdate() ", arrAlqty[i].ToString(), strStatus, CRNAM)
                                     .AppendFormat(" where RESNO='{0}' and MANDT='{1}' and COMCD='{2}' and WERKS= '{3}' and LGORT= '{4}' and LOCAT= '{5}' and MATNR= '{6}' and MBLNR= '{7}' and STATUS='0'", arrResno[i].ToString(), arrMandt[i].ToString(), arrComcd[i].ToString(), arrWerks[i].ToString(), arrLgort[i].ToString(), arrLocat[i].ToString().ToString(), arrMatnr[i].ToString(), arrMblnr[i].ToString());
                                arySQL.Add(sbSQL.ToString());

                            }
                        }

                        //WHHED
                        for (int i = 0; i < arrTempLocat.Count; i++)
                        {
                            // arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrTempLocat[i] + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrTempLocat[i] + "'");

                            sbSQL.Remove(0, sbSQL.Length);
                            sbSQL.AppendFormat("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}') as T ", strMandt, strComcd, strWerks, strLgort, arrTempLocat[i])
                                .AppendFormat("where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}'", strMandt, strComcd, strWerks, strLgort, arrTempLocat[i]);
                            arySQL.Add(sbSQL.ToString());

                            //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrTempLocat[i] + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrTempLocat[i] + "'");

                            //sbSQL.Remove(0, sbSQL.Length);
                            //sbSQL.AppendFormat("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end    from whitm where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}' and MRGID<>'') as T ", strMandt, strComcd, strWerks, strLgort, arrTempLocat[i])
                            //    .AppendFormat("where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}'", strMandt, strComcd, strWerks, strLgort, arrTempLocat[i]);
                            //arySQL.Add(sbSQL.ToString());

                        }
                        //arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrToloc[0].ToString() + "') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrToloc[0].ToString() + "'");
                        //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrToloc[0].ToString() + "' and MRGID<>'') as T where MANDT= '" + strMandt + "' and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + arrToloc[0].ToString() + "'");

                        sbSQL.Remove(0, sbSQL.Length);
                        sbSQL.AppendFormat("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}') as T ", strMandt, strComcd, strWerks, strLgort, arrToloc[0].ToString())
                            .AppendFormat("where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}'", strMandt, strComcd, strWerks, strLgort, arrToloc[0].ToString());
                        arySQL.Add(sbSQL.ToString());

                        //sbSQL.Remove(0, sbSQL.Length);
                        //sbSQL.AppendFormat("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end    from whitm where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}' and MRGID<>'') as T ", strMandt, strComcd, strWerks, strLgort, arrToloc[0].ToString())
                        //    .AppendFormat("where MANDT= '{0}' and COMCD='{1}'  and WERKS= '{2}'  and LGORT= '{3}' and LOCAT= '{4}'", strMandt, strComcd, strWerks, strLgort, arrToloc[0].ToString());
                        //arySQL.Add(sbSQL.ToString());


                        ControlHandleDB();
                        bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- UpdateReplenishmentStatus()";
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

            #region 查询补货资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091105////////////////////////////////////////////
            /// <summary>
            /// 查询补货资料
            /// </summary>
            /// <param name="strStatus"></param>
            /// <param name="strStartLocat"></param>
            /// <param name="strEndLocat"></param>
            /// <param name="strStartMatnr"></param>
            /// <param name="strEndMatnr"></param>
            /// <param name="strStartDate"></param>
            /// <param name="strEndDate"></param>
            /// <param name="strInsmk"></param>
            /// <param name="strResno"></param>
            /// <returns></returns>
            public DataTable QueryReplenishmentData(string strStatus, string strStartLocat, string strEndLocat, string strStartMatnr, string strEndMatnr, string strStartDate, string strEndDate, string strInsmk, string strResno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryReplenishmentData";
                this.ControlMethodParm = "('" + strStatus + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartDate + "','" + strEndDate + "','" + strInsmk + "','" + strResno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //strSQL = "select * from WHRES where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "'";
                    DataTable dtData = new DataTable();
                    DataWhres objWhres = new DataWhres(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("*");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS= '" + WERKS + "'");
                    alCondition.Add("LGORT= '" + LGORT + "'");

                    if (strResno != "")
                    {
                        alCondition.Add("RESNO= '" + strResno + "'");
                    }

                    if (strStatus != "")
                    {
                        alCondition.Add("STATUS= '" + strStatus + "'");
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        alCondition.Add("(LOCAT between '" + strStartLocat + "' and '" + strEndLocat + "')");
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        alCondition.Add("LOCAT= '" + strEndLocat + "'");
                    }
                    else if (strStartLocat != "" && strEndMatnr == "")
                    {
                        alCondition.Add("LOCAT= '" + strStartLocat + "'");
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        alCondition.Add("(MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')");
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        alCondition.Add("MATNR like '" + strEndMatnr + "%'");
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        alCondition.Add("MATNR like '" + strStartMatnr + "%'");
                    }

                    if (strInsmk != "")
                    {
                        alCondition.Add("INSMK= '" + strInsmk + "'");
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        alCondition.Add("(Convert(varchar(8), CRDAT, 112) between '" + strStartDate + "' and '" + strEndDate + "')");
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        alCondition.Add("Convert(varchar(8), CRDAT, 112) = '" + strEndDate + "'");
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        alCondition.Add("Convert(varchar(8), CRDAT, 112) = '" + strStartDate + "'");
                    }

                    dtData = objWhres.EntityQuery(alColumns, alCondition, false, true);
                    dtData = CommonInfo.SortDataTable(dtData, "LOCAT, MATNR");
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

            #region 查询Replentishment Type
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091105////////////////////////////////////////////
            /// <summary>
            /// 查询Replentishment Type
            /// </summary>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDdlReplentishmentType();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            public DataTable GetDdlReplentishmentType()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlReplentishmentType";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "Select CTRLNM + ':' + CTRLC1 as F_TEXT, CTRLNM as F_VALUE, * from WHCTRL where MANDT= 'QCI' and SOLDTO='QWMS' and CTRLID='REPLENISH_TYPE'";

                    DataTable dtData = new DataTable();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("CTRLNM + ':' + CTRLC1 as F_TEXT");
                    alColumns.Add("CTRLNM as F_VALUE");
                    //alColumns.Add("*");

                    alCondition.Clear();
                    alCondition.Add("MANDT='QCI'");
                    //alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("SOLDTO='QWMS'");
                    alCondition.Add("CTRLID='REPLENISH_TYPE'");
                    dtData = objWhctrl.EntityQuery(alColumns, alCondition, false, true);
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

            #region 查询 Replenishment Email
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091105////////////////////////////////////////////
            /// <summary>
            /// 查询 Replenishment Email
            /// </summary> 
            /// <param name="strUser">Email</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable dtReturn = objReplenishment.QueryMailUser(strUserMail);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryMailUser(string strType, string strUserMail)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMailUser";
                this.ControlMethodParm = "('" + strType + "','" + strUserMail + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "Select * from WHMAL where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "'";
                    //if (strType.Trim() != "")
                    //{
                    //    strSQL += " and MTYPE='" + strType + "'";
                    //}
                    //if (strUserMail.Trim() != "")
                    //{
                    //    strSQL += " and EMAIL=" + strUserMail.Trim() + "'";
                    //}
                    DataTable dtData = new DataTable();
                    DataWhmal objWhmal = new DataWhmal(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();

                    alColumns.Add("*");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS='" + WERKS + "'");
                    alCondition.Add("LGORT='" + LGORT + "'");
                    if (strType.Trim() != "")
                    {
                        alCondition.Add("MTYPE='" + strType + "'");
                    }
                    if (strUserMail.Trim() != "")
                    {
                        alCondition.Add("EMAIL='" + strUserMail.Trim() + "'");
                    }
                    string strSQL = objWhmal.EntityGetQuerySql(alColumns, alCondition, false, true);
                    dtData = objWhmal.EntityQuery(alColumns, alCondition, false, true);
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

            #region 设定补货被通知人 Email
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091106////////////////////////////////////////////
            /// <summary>
            /// 设定补货被通知人 Email
            /// </summary> 
            /// <param name="strUser">Email</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  bool  bolReturn = objReplenishment.AddMailUser(strUserMail);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddMailUser(string strType, string strUserMail)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddMailUser";
                this.ControlMethodParm = "('" + strType + "','" + strUserMail + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "insert into WHMAL(MANDT, WERKS, LGORT, FUNCT, MTYPE, EMAIL, CRNAM, CRDAT, MONAM, MODAT) 
                    //values('" + MANDT + "','" + WERKS + "','" + LGORT + "', 'REPLENISH', '" + strType + "', '" + strUserMail.Trim() + "', '" + CRNAM + "', getdate(), '" + CRNAM + "', getdate())";
                    DataWhmal objWhmal = new DataWhmal(UserData);

                    objWhmal.ResetField();
                    objWhmal.Mandt = MANDT;
                    objWhmal.Comcd = COMCD;
                    objWhmal.Werks = WERKS;
                    objWhmal.Lgort = LGORT;
                    objWhmal.Funct = "REPLENISH";
                    objWhmal.Mtype = strType;
                    objWhmal.Email = strUserMail.Trim();
                    objWhmal.Crnam = CRNAM;
                    objWhmal.Crdat = "getdate()";
                    objWhmal.Monam = CRNAM;
                    objWhmal.Modat = "getdate()";

                    bool blResult = false;
                    blResult = objWhmal.EntityInsert();
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

            #region 删除补货被通知人 Email
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091106////////////////////////////////////////////         
            /// <summary>
            /// 删除补货被通知人 Email
            /// </summary> 
            /// <param name="strUser">Email</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  bool  bolReturn = objReplenishment.DeleteMailUserArray(strUserMail);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool DeleteMailUserArray(ArrayList arrType, ArrayList arrUserMail)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddMailUser";
                this.ControlMethodParm = "('" + arrType + "','" + arrUserMail + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhmal objWhmal = new DataWhmal(UserData);
                    ArrayList alCondition = new ArrayList();
                    ArrayList arrSQL = new ArrayList();
                    bool bolReturn = false;
                    for (int i = 0; i < arrUserMail.Count; i++)
                    {
                        //arrSQL.Add("Delete from WHMAL where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "' and MTYPE='" + arrType[i].ToString().Trim() + "' and EMAIL='" + arrUserMail[i].ToString().Trim() + "'");
                        alCondition.Clear();
                        alCondition.Add("MANDT='" + MANDT + "'");
                        alCondition.Add("COMCD='" + COMCD + "'");
                        alCondition.Add("WERKS='" + WERKS + "'");
                        alCondition.Add("LGORT='" + LGORT + "'");
                        alCondition.Add("MTYPE='" + arrType[i].ToString().Trim() + "'");
                        alCondition.Add("EMAIL='" + arrUserMail[i].ToString().Trim() + "'");
                        arrSQL.Add(objWhmal.EntityGetDeleteSql(alCondition));
                    }


                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arrSQL);
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

            #region 查询储位 Replenishment 资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091106////////////////////////////////////////////  
            /// <summary>
            /// 查询储位 Replenishment 资料
            /// </summary> 
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable dtReturn = objReplenishment.QueryReplenishmentMapping(strLocat, strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryReplenishmentMapping(string strLocat, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryReplenishmentMapping";
                this.ControlMethodParm = "('" + strLocat + "','" + strMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhrep objWhrep = new DataWhrep(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    DataTable dtData = new DataTable();
                    //strSQL = "select * from WHREP where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "'";

                    alColumns.Add("*");
                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS='" + WERKS + "'");
                    alCondition.Add("LGORT='" + LGORT + "'");

                    if (strLocat.Trim() != "")
                    {
                        alCondition.Add("LOCAT='" + strLocat.Trim() + "'");
                    }
                    if (strMatnr.Trim() != "")
                    {
                        alCondition.Add("MATNR='" + strMatnr.Trim() + "'");
                    }

                    dtData = objWhrep.EntityQuery(alColumns, alCondition, false, true);
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

            #region 新增储位的补货的基本资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091106////////////////////////////////////////////  
            /// <summary>
            /// 新增储位的补货的基本资料
            /// </summary> 
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="strInsmk">库位</param>
            /// <param name="strCharg">版本</param>
            /// <param name="intMaxge">最大放量</param>
            /// <param name="intRepot">水位量</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  bool  bolReturn = objReplenishment.AddReplenishmentMapping(strLocat, strMatnr, strInsmk, strCharg, intMaxge,intRepot);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool AddReplenishmentMapping(string strLocat, string strMatnr, string strInsmk, string strCharg, int intMaxge, int intRepot)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddReplenishmentMapping";
                this.ControlMethodParm = "('" + strLocat + "','" + strMatnr + "','" + strInsmk + "','" + strCharg + "','" + intMaxge + "','" + intRepot + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "insert into WHREP(MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MAXGE, REPOT, CRNAM, CRDAT, MONAM, MODAT) 
                    //values('" + MANDT + "','" + WERKS + "', '" + LGORT + "', '" + strLocat.Trim() + "', '" + strMatnr.Trim() + "'
                    //, '" + strInsmk.Trim() + "', '" + strCharg.Trim() + "', '" + intMaxge + "', '" + intRepot + "', '" + CRNAM + "', getdate(), '" + CRNAM + "', getdate())";

                    DataWhrep objWhrep = new DataWhrep(UserData);
                    bool bolResult = false;

                    objWhrep.ResetField();
                    objWhrep.Mandt = MANDT;
                    objWhrep.Comcd = COMCD;
                    objWhrep.Werks = WERKS;
                    objWhrep.Lgort = LGORT;
                    objWhrep.Locat = strLocat.Trim();
                    objWhrep.Matnr = strMatnr.Trim();
                    objWhrep.Insmk = strInsmk.Trim();
                    objWhrep.Charg = strCharg.Trim();
                    objWhrep.Maxge = intMaxge.ToString();
                    objWhrep.Repot = intRepot.ToString();
                    objWhrep.Crnam = CRNAM;
                    objWhrep.Crdat = "getdate()";
                    objWhrep.Monam = CRNAM;
                    objWhrep.Modat = "getdate()";

                    string strsql = objWhrep.EntityGetInsertSql();
                    bolResult = objWhrep.EntityInsert();
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

            #region 删除储位的补货的基本资料
            //=========================================================================
            ////////////Summary by Bruce Zhang 20091106////////////////////////////////////////////  
            /// <summary>
            /// 删除储位的补货的基本资料
            /// </summary> 
            /// <param name="strLocat">储位</param>
            /// <param name="strMatnr">料号</param>
            /// <param name="strInsmk">库位</param>
            /// <param name="strCharg">版本</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Replenishment objReplenishment =new QCI.QWMS.Replenishment(strConnectionString,strMandt,strWerks,strLgort);
            ///  bool  bolReturn = objReplenishment.DeleteReplenishmentMappingArray(arrLocat, arrMatnr, arrInsmk, arrCharg);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool DeleteReplenishmentMappingArray(ArrayList arrLocat, ArrayList arrMatnr, ArrayList arrInsmk, ArrayList arrCharg)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "DeleteReplenishmentMappingArray";
                this.ControlMethodParm = "('" + arrLocat + "','" + arrMatnr + "','" + arrInsmk + "','" + arrCharg + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhrep objWhrep = new DataWhrep(UserData);
                    ArrayList alCondition = new ArrayList();
                    ArrayList arrSQL = new ArrayList();
                    bool bolReturn = false;
                    for (int i = 0; i < arrLocat.Count; i++)
                    {
                        //strSQL = "Delete from WHREP where MANDT='" + MANDT + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "' and  LOCAT='" + arrLocat[i].ToString().Trim() + "' and MATNR='" + arrMatnr[i].ToString().Trim() + "' and INSMK='" + arrInsmk[i].ToString().Trim() + "' and CHARG='" + arrCharg[i].ToString().Trim() + "'";

                        alCondition.Clear();
                        alCondition.Add("MANDT='" + MANDT + "'");
                        alCondition.Add("COMCD='" + COMCD + "'");
                        alCondition.Add("WERKS='" + WERKS + "'");
                        alCondition.Add("LGORT='" + LGORT + "'");
                        alCondition.Add("LOCAT='" + arrLocat[i].ToString().Trim() + "'");
                        alCondition.Add("MATNR='" + arrMatnr[i].ToString().Trim() + "'");
                        alCondition.Add("INSMK='" + arrInsmk[i].ToString().Trim() + "'");
                        alCondition.Add("CHARG='" + arrCharg[i].ToString().Trim() + "'");

                        arrSQL.Add(objWhrep.EntityGetDeleteSql(alCondition));
                    }

                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arrSQL);
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

            #endregion
        }
    }
}
