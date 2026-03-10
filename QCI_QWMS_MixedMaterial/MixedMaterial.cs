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
        /// <summary>
        /// MixedMaterial 的摘要描述。
        /// </summary>
        public class MixedMaterial : ControlBase
        {

            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strErrmsg = "";

            #region Constructer

            public MixedMaterial(UserInfo varUserData, string strWerks)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks)
            {

            }

            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Admin物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.MixedMaterial objMixedMaterial =new QCI.QWMS.MixedMaterial(strConnectionString,strMandt,strWerks);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public MixedMaterial(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks)
            {
                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = strWerks;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.MixedMaterial";
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
            ///Cmpany Code
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

            # region 获得当前Plant下所有的连板料号资料
            /// <summary>
            ///  获得当前Plant下所有的连板料号资料
            /// </summary> 
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  MixedMaterial objMixedMaterial =new MixedMaterial(UserInfo,strWerks);
            ///  DataTable  dtData = objMixedMaterial.QueryMixedMaterialTable();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryMixedMaterialTable()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMixedMaterialTable";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhmtr objDataWhmtr = new DataWhmtr(UserData);

                    alColumns.Clear();
                    alColumns.Add(" * ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + WERKS + "'");

                    DataTable dtData = new DataTable();
                    dtData = objDataWhmtr.EntityQuery(alColumns, alConditions, false, true);

                    # region old function
                    //string strSQL = "Select * from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' order by MRGID";
                    //DataTable dtData = new DataTable();
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL);
                    //ControlSqlAccess.CloseConnection();
                    # endregion

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

            # region 传入料号获得连板资料
            /// <summary>
            /// 传入料号获得连板资料
            /// </summary> 
            /// <param name="aryMatnr">料号</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  MixedMaterial objMixedMaterial =new MixedMaterial(UserInfo,strWerks);
            ///  DataTable  dtData = objMixedMaterial.QueryMixedMaterialTable(strLgort,strLocat,strMrgid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryMixedMaterialTable(ArrayList aryMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMixedMaterialTable";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here..

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhmtr objDataWhmtr = new DataWhmtr(UserData);

                    int j = 0;
                    int intTotalRows = 0;
                    string strTempMrgid = "";

                    for (int i = 0; i < aryMatnr.Count; i++)
                    {
                        if (i == 0)
                        {
                            # region old function
                            //string strSQL = "Select distinct MRGID from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MATNR= '" + aryMatnr[i].ToString() + "'";

                            //DataTable dtData = new DataTable();
                            //ControlHandleDB();
                            //dtData = ControlSqlAccess.GetDataTable(strSQL);
                            //ControlSqlAccess.CloseConnection();
                            # endregion

                            alColumns.Clear();
                            alColumns.Add(" MRGID ");

                            alConditions.Clear();
                            alConditions.Add(" MANDT='" + MANDT + "'");
                            alConditions.Add(" COMCD='" + COMCD + "'");
                            alConditions.Add(" WERKS='" + WERKS + "'");
                            alConditions.Add(" MATNR='" + aryMatnr[i].ToString() + "'");
                            DataTable dtData = new DataTable();
                            dtData = objDataWhmtr.EntityQuery(alColumns, alConditions, true, true);

                            intTotalRows = dtData.Rows.Count;

                            for (int k = 0; k < dtData.Rows.Count; k++)
                            {
                                strTempMrgid += ",'" + dtData.Rows[k]["MRGID"].ToString() + "'";
                            }
                            if (intTotalRows > 0)
                                strTempMrgid = strTempMrgid.Substring(1);
                            else
                                strTempMrgid = "";

                            j++;
                        }

                        while (j < aryMatnr.Count)
                        {
                            string strSQL2 = "";
                            DataTable dtData2 = new DataTable();
                            if (strTempMrgid.Trim() != "")
                            {
                                //strSQL2 = "Select distinct MRGID from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MATNR= '" + aryMatnr[j].ToString() + "' and MRGID in (" + strTempMrgid + ")";
                                //ControlHandleDB();

                                alColumns.Clear();
                                alColumns.Add(" MRGID ");

                                alConditions.Clear();
                                alConditions.Add(" MANDT='" + MANDT + "'");
                                alConditions.Add(" COMCD='" + COMCD + "'");
                                alConditions.Add(" WERKS='" + WERKS + "'");
                                alConditions.Add(" MATNR='" + aryMatnr[i].ToString() + "'");
                                alConditions.Add(" MRGID in (" + strTempMrgid + ")");

                                try
                                {
                                    dtData2 = objDataWhmtr.EntityQuery(alColumns, alConditions, true, true);
                                }
                                catch (System.Exception ex)
                                {
                                    ERRMSG = ex.Message + "<- QueryMixedMaterialTable()";
                                }
                                finally
                                {
                                    //ControlSqlAccess.CloseConnection();
                                }
                            }
                            intTotalRows = dtData2.Rows.Count;
                            strTempMrgid = "";

                            for (int k = 0; k < dtData2.Rows.Count; k++)
                            {
                                strTempMrgid += ",'" + dtData2.Rows[k]["MRGID"].ToString() + "'";
                            }

                            if (intTotalRows > 0)
                                strTempMrgid = strTempMrgid.Substring(1);
                            else
                                strTempMrgid = "";

                            j++;
                        }
                    }
                    string strSQL3 = "";
                    DataTable dtData3 = new DataTable();
                    if (strTempMrgid.Trim() != "")
                    {
                        # region old function
                        //strSQL3 = "select * from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MRGID in (" + strTempMrgid + ") order by MRGID";
                        //ControlHandleDB();
                        //dtData3 = ControlSqlAccess.GetDataTable(strSQL3);
                        //ControlSqlAccess.CloseConnection();
                        # endregion

                        alColumns.Clear();
                        alColumns.Add(" * ");

                        alConditions.Clear();
                        alConditions.Add(" MANDT='" + MANDT + "'");
                        alConditions.Add(" COMCD='" + COMCD + "'");
                        alConditions.Add(" WERKS='" + WERKS + "'");
                        alConditions.Add(" MRGID in (" + strTempMrgid + ")");
                        dtData3 = objDataWhmtr.EntityQuery(alColumns, alConditions, false, true);
                        dtData3 = CommonInfo.SortDataTable(dtData3, " MRGID ");
                    }
                    return dtData3;

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

            # region 判断连板料号是否有库存
            /// <summary>
            /// 判断连板料号是否有库存
            /// </summary> 
            /// <param name="strMrgid">连板流水号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  MixedMaterial objMixedMaterial =new MixedMaterial(UserInfo,strWerks);
            ///  bool  bolReturn = objMixedMaterial.QueryMixedMaterialStorage(strMrgid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public bool QueryMixedMaterialStorage(string strMgrid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMixedMaterialStorage";
                this.ControlMethodParm = "('" + strMgrid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhitm objDataWhitm = new DataWhitm(UserData);

                    alColumns.Clear();
                    alColumns.Add(" * ");

                    alConditions.Clear();
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" MRGID='" + strMgrid + "'");

                    DataTable dtData = new DataTable();
                    dtData = objDataWhitm.EntityQuery(alColumns, alConditions, false, true);

                    # region old function
                    //string strSQL = "Select * from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and MRGID= '" + strMgrid + "'";

                    //DataTable dtData = new DataTable();
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL);
                    //ControlSqlAccess.CloseConnection();
                    # endregion


                    if (dtData.Rows.Count == 0)
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

            # region 檢查目前是否已經設定連板料號
            /// <summary>
            /// 檢查目前是否已經設定連板料號
            /// </summary> 
            /// <param name="aryMatnr">料号</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  MixedMaterial objMixedMaterial =new MixedMaterial(UserInfo,strWerks);
            ///  string strReturn = objMixedMaterial.QueryMixedMaterialID(aryMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public string QueryMixedMaterialID(ArrayList aryMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMixedMaterialStorage";
                this.ControlMethodParm = "('" + aryMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    int j = 0;
                    int intTotalRows = 0;
                    string strTempMrgid = "";
                    string strMrgid = "";
                    DataTable dtData2 = new DataTable();
                    DataTable dtData3 = new DataTable();
                    DataWhmtr objWhmtr = new DataWhmtr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    string strFrom = "";

                    for (int i = 0; i < aryMatnr.Count; i++)
                    {
                        if (i == 0)
                        {
                            #region delete
                            //string strSQL = "Select distinct MRGID from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MATNR= '" + aryMatnr[i].ToString() + "'";

                            //DataTable dtData = new DataTable();
                            //ControlHandleDB();
                            //dtData = ControlSqlAccess.GetDataTable(strSQL);
                            //ControlSqlAccess.CloseConnection();
                            #endregion

                            DataTable dtData = new DataTable();
                            alColumns.Clear();
                            alConditions.Clear();

                            alColumns.Add(" MRGID ");

                            alConditions.Add(" (MANDT='" + MANDT + "') ");
                            alConditions.Add(" (COMCD='" + COMCD + "') ");
                            alConditions.Add(" (WERKS='" + WERKS + "') ");
                            alConditions.Add(" (MATNR='" + aryMatnr[i].ToString() + "') ");

                            dtData = objWhmtr.EntityQuery(alColumns, alConditions, true, true);

                            intTotalRows = dtData.Rows.Count;

                            //strTempMrgid	
                            for (int k = 0; k < dtData.Rows.Count; k++)
                            {
                                strTempMrgid += ",'" + dtData.Rows[k]["MRGID"].ToString() + "'";
                            }

                            if (intTotalRows > 0)
                                strTempMrgid = strTempMrgid.Substring(1);
                            else
                                strTempMrgid = "";
                            j++;
                        }

                        while (j < aryMatnr.Count)
                        {
                            string strSQL2 = "";
                            if (strTempMrgid == "")
                            {
                                //strSQL2 = "Select distinct MRGID from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MATNR= '" + aryMatnr[j].ToString() + "' and MRGID = ''";

                                alColumns.Clear();
                                alConditions.Clear();
                                alColumns.Add(" MRGID ");
                                alConditions.Add(" (MANDT='" + MANDT + "') ");
                                alConditions.Add(" (COMCD='" + COMCD + "') ");
                                alConditions.Add(" (WERKS='" + WERKS + "') ");
                                alConditions.Add(" (MATNR='" + aryMatnr[j].ToString() + "') ");
                                alConditions.Add(" (MRGID='') ");

                            }
                            else
                            {
                                //strSQL2 = "Select distinct MRGID from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MATNR= '" + aryMatnr[j].ToString() + "' and MRGID in (" + strTempMrgid + ")";

                                alColumns.Clear();
                                alConditions.Clear();
                                alColumns.Add(" MRGID ");
                                alConditions.Add(" (MANDT='" + MANDT + "') ");
                                alConditions.Add(" (COMCD='" + COMCD + "') ");
                                alConditions.Add(" (WERKS='" + WERKS + "') ");
                                alConditions.Add(" (MATNR='" + aryMatnr[j].ToString() + "') ");
                                alConditions.Add(" (MRGID in (" + strTempMrgid + ")) ");


                            }

                            try
                            {
                                dtData2 = objWhmtr.EntityQuery(alColumns, alConditions, true, true);
                            }
                            catch (System.Exception ex)
                            {
                                ERRMSG = ex.Message + "<- QueryMixedMaterialID()";
                            }


                            intTotalRows = dtData2.Rows.Count;
                            strTempMrgid = "";

                            for (int k = 0; k < dtData2.Rows.Count; k++)
                            {
                                strTempMrgid += ",'" + dtData2.Rows[k]["MRGID"].ToString() + "'";
                            }
                            if (intTotalRows > 0)
                                strTempMrgid = strTempMrgid.Substring(1);
                            else
                                strTempMrgid = "";

                            j++;
                        }
                    }

                    if (intTotalRows >= 1)
                    {
                        for (int i = 0; i < dtData2.Rows.Count; i++)
                        {
                            //string strSQL3 = "select * from WHMTR where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and MRGID= '" + dtData2.Rows[i]["MRGID"].ToString() + "'";

                            alColumns.Clear();
                            alConditions.Clear();
                            alColumns.Add(" * ");
                            alConditions.Add(" (MANDT='" + MANDT + "') ");
                            alConditions.Add(" (COMCD='" + COMCD + "') ");
                            alConditions.Add(" (WERKS='" + WERKS + "') ");
                            alConditions.Add(" (MRGID='" + dtData2.Rows[i]["MRGID"].ToString() + "') ");

                            try
                            {
                                dtData3 = objWhmtr.EntityQuery(alColumns, alConditions, true, true);
                            }
                            catch (System.Exception ex)
                            {
                                ERRMSG = ex.Message + "<- QueryMixedMaterialID()";
                            }

                            if (aryMatnr.Count == dtData3.Rows.Count)
                            {
                                strMrgid = dtData3.Rows[i]["MRGID"].ToString();
                                break;
                            }
                        }
                    }

                    return strMrgid;
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

            #region 檢查某連板編號是否在儲位中是否有另外一個組合在
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查某連板編號是否在儲位中是否有另外一個組合在, 例如原本已經有AB, 現在要儲入的資料是不是AC, 亦即檢查相同料號卻屬於不同連板編號
            /// </summary> 
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMrgid">連板編號。</param>
            /////////////////////////////////////////////////////////////////////////////
            public bool QueryExistDifferentMixedMaterial(string strLgort, string strLocat, string strMrgid)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryExistDifferentMixedMaterial";
                this.ControlMethodParm = "('" + strLgort + "','" + strLocat + "','" + strMrgid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataWhmtr objWhmtr = new DataWhmtr(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataTable dtData = new DataTable();

                    alColumns.Clear();
                    alConditions.Clear();
                    alColumns.Add(" MATNR ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + WERKS + "'");
                    alConditions.Add(" MRGID='" + strMrgid + "'");

                    dtData = objWhmtr.EntityQuery(alColumns, alConditions, true, true);
                    #region delete
                    //StringBuilder sbSql = new StringBuilder();
                    //sbSql.Append("Select DISTINCT MATNR from WHMTR where  ");
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("and WERKS='{0}' ", WERKS);
                    //sbSql.AppendFormat("and MRGID='{0}' ", strMrgid);                   
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    #endregion

                    string strMatnr = "";

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strMatnr += ",'" + dtData.Rows[i]["MATNR"].ToString() + "'";
                    }

                    strMatnr = strMatnr.Substring(1);

                    DataTable dtData2 = new DataTable();
                    DataWhitm objWhitm = new DataWhitm(UserData);
                    alColumns.Clear();
                    alConditions.Clear();
                    alColumns.Add(" * ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + WERKS + "'");
                    alConditions.Add(" LGORT='" + strLgort + "'");
                    alConditions.Add(" LOCAT='" + strLocat + "'");
                    alConditions.Add(" MATNR in (" + strMatnr + ")");
                    alConditions.Add(" MRGID <>'" + strMrgid + "'");
                    alConditions.Add(" MRGID <>'' ");

                    dtData2 = objWhitm.EntityQuery(alColumns, alConditions, false, true);

                    #region delete
                    //StringBuilder sbSql2 = new StringBuilder();

                    //sbSql2.Append("Select * from WHITM where  ");
                    //sbSql2.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql2.AppendFormat("and COMCD='{0}' ", COMCD);
                    //sbSql2.AppendFormat("and WERKS='{0}' ", WERKS);
                    //sbSql2.AppendFormat("and LGORT='{0}' ", strLgort);
                    //sbSql2.AppendFormat("and LOCAT='{0}' ", strLocat);
                    //sbSql2.AppendFormat("and MATNR in (" + strMatnr + ") ");
                    //sbSql2.AppendFormat("and MRGID<>'{0}' ", strMrgid);
                    //sbSql2.AppendFormat("and MRGID<>'' ");

                    //DataTable dtData2 = new DataTable();
                    //dtData2 = ControlSqlAccess.GetDataTable(sbSql2.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion
                    if (dtData2.Rows.Count > 0)
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


        }
    }
}
