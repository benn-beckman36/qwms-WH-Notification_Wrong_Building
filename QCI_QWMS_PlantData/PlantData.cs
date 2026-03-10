using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.Data.SqlClient;
using QCI_QWMS_Entity;
using System.Linq;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// PlantData 的摘要描述。
        /// </summary>
        public class PlantData : ControlBase
        {

            private string strMandt = "";
            private string strComcd = "";
            private string strCrnam = "";
            private string strErrmsg = "";
            private Authority objAuthority = null;
            #region Constructor
            #region 不傳入任何參數產生PlantData物件
            /// <summary>
            /// 不傳入任何參數產生PlantData物件。
            /// </summary>
            /// <example>
            /// <code>
            ///  PlantData objPlantData = new PlantData();
            ///  Your Code Here......
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public PlantData(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
            {
            }
            #endregion

            #region 利用傳入參數產生PlantData物件
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
            public PlantData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
            {
                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                CRNAM = varUserData.UserId;
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

            # region 获得Plant
            /// <summary>
            /// 获得Plant
            /// </summary>          
            /// <param name="varClient">varMandt</param>           
            /// <returns>String</returns>
            /// <example>
            /// <code>
            ///  PlantData objPlantData = new PlantData(1, "TEST", 2, "ERR");
            ///  objPlantData.GetPlant("218","9200");
            ///  Your Code Here......
            /// </code>
            /// </example> 
            public string GetMenuType(string varMandt)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMenuType";
                this.ControlMethodParm = "('" + varMandt + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append("Select CTRLC3  from WHCTRL WITH (Nolock) where  ");
                    sbSql.AppendFormat("MANDT='{0}' ", "QCI");
                    sbSql.AppendFormat("AND SOLDTO='{0}' ", "CLIENT");
                    sbSql.AppendFormat("AND CTRLID='{0}' ", varMandt);

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0]["CTRLC3"].ToString();
                    }
                    else
                    {
                        return "";
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

            #region


            public DataTable GetProgramClass()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetProgramClass";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("select * from WHCTRL WITH (Nolock) where MANDT='QCI' and SOLDTO='QWMS' and CTRLID='MENU' and CTRLN3 = '1' order by CTRLN1 ");
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

            public DataTable GetProgramData()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetProgramData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append("select * from WHCTRL WITH (Nolock) where MANDT='QCI' and SOLDTO='QWMS' and CTRLID='PROGM' order by CTRLNM ");

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


            public DataTable GetProgramData(string strProtp, bool blStockComparePermission = false)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetProgramData";
                this.ControlMethodParm = "('" + strProtp + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append("Select  CTRLC1, CTRLC2,CTRLC3,CTRLC4,CTRLC5  from WHCTRL WITH (Nolock) where MANDT= 'QCI' and SOLDTO='QWMS' and CTRLID='PROGM' and CTRLNM= '" + strProtp + "' and CTRLN3='1'  ");
                    if (blStockComparePermission)
                        sbSql.Append("OR (CTRLC2='FK')");
                    sbSql.Append("order by CTRLN1");
                    SqlAccess sqlSccess = new SqlAccess(CommonInfo.Instance.DBCode);
                    dt = sqlSccess.GetDataTable(sbSql.ToString());
                    sqlSccess.CloseConnection();

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

            #region 查看已存在的倉別,倉別,儲位
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 查看已存在的倉別,倉別,儲位
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>          
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckExistedStorageData(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckExistedStorageData";
                this.ControlMethodParm = "('" + strLgort + "','" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    DataWhctrl objWhctrl = new DataWhctrl(UserData);
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add(" 1 ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");

                    if (strLgort == "")
                    {
                        alConditions.Add(" SOLDTO='QWMS'");
                        alConditions.Add(" CTRLID='WERKS'");
                        alConditions.Add(" CTRLNM='" + strWerks + "'");
                        dt = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                    }
                    else
                    {
                        if (strLocat == "")
                        {
                            alConditions.Add(" SOLDTO='QWMS'");
                            alConditions.Add(" CTRLID='LGORT'");
                            alConditions.Add(" CTRLNM='" + strWerks + "'");
                            alConditions.Add(" CTRLC1='" + strLgort + "'");
                            dt = objWhctrl.EntityQuery(alColumns, alConditions, false, true);
                        }
                        else
                        {
                            alConditions.Add(" WERKS='" + strWerks + "'");
                            alConditions.Add(" LGORT='" + strLgort + "'");
                            alConditions.Add(" LOCAT='" + strLocat + "'");

                            dt = objWhhed.EntityQuery(alColumns, alConditions, false, true);
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

            #region 取得倉別下的儲位資訊
            //=================================================================================================
            ////////////Summary by Eric Chou///////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks, strLgort, strLocat, strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////		
            public DataTable GetAllLocatData(string strWerks, string strLgort, string strLocat, string strType)
            {
                DataWhhed objWhhed = new DataWhhed(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                alColumns.Add("LOCAT");
                alConditions.Add("MANDT='" + MANDT + "'");
                alConditions.Add("COMCD='" + COMCD + "'");
                alConditions.Add("WERKS='" + strWerks + "'");
                alConditions.Add("LGORT='" + strLgort + "'");
                alConditions.Add("Block='0'");  //儲位維護的條件(Block = 0) 

                if (strLocat != "")
                {
                    alConditions.Add("LOCAT='" + strLocat + "'");
                }

                if (strType == "0") //空儲位
                {
                    alConditions.Add("LOSTS='0'");
                }
                else if (strType == "1") //有庫存儲位
                {
                    alConditions.Add("LOSTS='1'");
                }
                //else if (strType == "2") //全部儲位
                //{
                //   // strSQL += "";
                //}
                else if (strType == "3") //尚未設定位置儲位
                {
                    alConditions.Add("(STLEN is null and STWID is null) ");
                }

                DataTable dtData = new DataTable();
                try
                {
                    string strsql = objWhhed.ControlGetQuerySql("whhed", alColumns, alConditions, true);
                    dtData = objWhhed.EntityQuery(alColumns, alConditions, false, true);
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- GetAllLocatData()";
                }
                return dtData;
            }
            #endregion

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
                        alColumns.Add(" W.MANDT ");
                        alColumns.Add(" W.COMCD ");
                        alColumns.Add(" W.CTRLNM ");
                        alColumns.Add(" W.CTRLC2 ");
                        alColumns.Add(" W.REMAK ");
                        alColumns.Add(" isnull(W.CTRLC4,'') as CTRLC4 ");
                        alColumns.Add(" isnull(W.CTRLC5,'') as CTRLC5 ");
                        alColumns.Add(" ( CASE W.CTRLC3 WHEN 'FULL' THEN N'整料仓' WHEN 'BULK' THEN N'散料仓' ELSE '' END ) AS CTRLC3");
                        alColumns.Add(" ( CASE W.CTRLN3 WHEN '1' THEN N'D/C祥龙解欠' ELSE '' END ) AS XL ");
                        alColumns.Add(" (CASE WHEN (SELECT COUNT(*)  FROM WHCTRL  WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='StorageIn_Type' AND  REMAK='Diff DACOD Diff Locat' AND CTRLC1 = W.CTRLC2) = 1 THEN N'D/C管控' ELSE '' END ) AS DC ");
                        alColumns.Add(" WHSTG.STSET ");
                        alColumns.Add(" WHSTG.STHGH ");
                        alColumns.Add(" WHSTG.STLEN ");
                        alColumns.Add(" WHSTG.STWID ");
                        alColumns.Add(" WHSTG.CRNAM ");
                        alColumns.Add(" WHSTG.CRDAT ");
                        strFrom = " WHCTRL AS W left outer join WHSTG With(Nolock) on W.MANDT=WHSTG.MANDT AND W.COMCD=WHSTG.COMCD  and W.CTRLNM=WHSTG.WERKS and W.CTRLC1=WHSTG.LGORT ";

                        alConditions.Add(" (W.MANDT='" + MANDT + "') ");
                        alConditions.Add(" (W.COMCD='" + COMCD + "') ");
                        alConditions.Add(" (W.SOLDTO='QWMS') ");
                        alConditions.Add(" (W.CTRLID='LGORT') ");

                        if (strWerks == "")
                        {

                        }
                        else
                        {
                            if (strLgort != "")
                            {
                                alConditions.Add(" (W.CTRLNM='" + strWerks + "') ");
                                alConditions.Add(" (CTRLC2='" + strLgort + "') ");
                            }
                            else
                            {
                                alConditions.Add(" (W.CTRLNM='" + strWerks + "') ");
                            }
                        }

                        dtData = ControlQuery(strFrom, alColumns, alConditions, true);
                        dtData = CommonInfo.SortDataTable(dtData, "CTRLNM, CTRLC2");
                    }



                    //try
                    //{

                    //}
                    //catch (System.Exception ex)
                    //{
                    //    ERRMSG = ex.Message + "<- GetPlantStorageData()";
                    //}
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
            # region 获得储位资料
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <param name="strMapid">Machine。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  PlantData objPlantData =new PlantData(UserData);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks, strLgort, "", "2", strCtbto, strRegon, strMapid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetSPAllLocatData(string Mandt, string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSql = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    //    " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    //    "  from WHHED where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'and Block = '0'";

                    //Modify by Jack 20150610
                    string strSql = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                        " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                        "  from WHHED WITH(NOLOCK) where MANDT= '" + Mandt + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSql);
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

            # region 获得储位资料
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <param name="strMapid">Machine。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  PlantData objPlantData =new PlantData(UserData);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks, strLgort, "", "2", strCtbto, strRegon, strMapid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatData(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon, string strMapid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strType + "','" + strCtbto + "','" + strRegon + "','" + strMapid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSql = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    //    " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    //    "  from WHHED where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'and Block = '0'";

                    //Modify by Jack 20150610
                    string strSql = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                        " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                        "  from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";
                    if (strLocat != "")
                    {
                        strSql += " and LOCAT ='" + strLocat + "'";
                    }

                    if (strType == "0") //空儲位
                    {
                        strSql += " and LOSTS='0' ";
                    }
                    else if (strType == "1") //有庫存儲位
                    {
                        strSql += " and LOSTS='1' ";
                    }
                    else if (strType == "2") //全部儲位
                    {
                        strSql += "";
                    }
                    else if (strType == "3") //尚未設定位置儲位
                    {
                        strSql += " and STLEN is null and STWID is null ";
                    }

                    if (strCtbto != "")
                    {
                        strSql += " and CTBTO ='" + strCtbto + "'";
                    }

                    if (strRegon != "")
                    {
                        strSql += " and REGON ='" + strRegon + "'";
                    }

                    if (strMapid != "")
                    {
                        strSql += " and MAPID ='" + strMapid + "'";
                    }
                    strSql += " AND COMCD='" + COMCD + "' and Block = '0'";

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSql);
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

            # region 獲得儲位資料
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatData(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strType + "','" + strCtbto + "','" + strRegon + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    // string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    //" REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    //"  from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and Block = '0'";

                    //Modify by Jack 20150610
                    string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                        " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                        "  from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";
                    if (strLocat != "")
                    {
                        strSQL += " and LOCAT ='" + strLocat + "'";
                    }

                    if (strType == "0") //空儲位
                    {
                        strSQL += " and LOSTS='0' ";
                    }
                    else if (strType == "1") //有庫存儲位
                    {
                        strSQL += " and LOSTS='1' ";
                    }
                    else if (strType == "2") //全部儲位
                    {
                        strSQL += "";
                    }
                    else if (strType == "3") //尚未設定位置儲位
                    {
                        strSQL += " and STLEN is null and STWID is null ";
                    }

                    if (strCtbto != "")
                    {
                        strSQL += " and CTBTO ='" + strCtbto + "'";
                    }

                    if (strRegon != "")
                    {
                        strSQL += " and REGON ='" + strRegon + "'";
                    }
                    strSQL += " AND COMCD='" + COMCD + "' and Block = '0'";

                    //ASRS仓别储位格式为 列层格，需要按照格排序
                    if (CheckLGORT(strWerks, strLgort))
                    {
                        strSQL += " ORDER BY  SUBSTRING(locat, 1, 2) DESC, SUBSTRING(locat, 3, 2) DESC,   SUBSTRING(locat, 5, 2) DESC ";
                    }

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSQL);
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
            #endregion

            # region 獲得儲位資料 by blank
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatData(string strWerks, string strLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    // string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    //" REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    //"  from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and Block = '0'";

                    //Modify by Jack 20150610
                    string strSQL = "Select LOCAT from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";

                    strSQL += " AND COMCD='" + COMCD + "' and Block = '0'";

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSQL);
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
            #endregion

            #region 获取储位资料   by mblnr

            public DataTable GetAllLocatDataBymblnr(string strWerks, string strLgort, string strMblnr)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    #region old
                    //太慢，结果没问题
                    //  string strSQL = "select DISTINCT I.LOCAT from whitm  I WITH(NOLOCK) INNER JOIN WHDWN D WITH(NOLOCK) ON D.MATNR=I.MATNR AND D.WERKS=I.WERKS where I.werks='" + strWerks + "' and I.LGORT='" + strLgort + "' AND D.MBLNR='" + strMblnr + "' AND D.MENGE-D.OTQTY>0  "; 
                    #endregion

                    string strSQL = "SELECT DISTINCT I.LOCAT FROM ( SELECT MATNR,WERKS,MBLNR FROM WHDWN WITH(NOLOCK) WHERE MBLNR ='" + strMblnr + "' AND MENGE-OTQTY>0 ) D INNER JOIN  ( SELECT DISTINCT MATNR,WERKS,LOCAT FROM WHITM WITH(NOLOCK) WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ) I ON D.MATNR=I.MATNR AND D.WERKS=I.WERKS ";

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSQL);
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
            #endregion

            # region 獲得儲位資料
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatDataCSMC(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon, string strMat)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strType + "','" + strCtbto + "','" + strRegon + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    if (strMat != "")
                    {
                        StringBuilder sbSql = new StringBuilder();
                        sbSql.Append("Select H.* from WHHED AS H WITH (NOLOCK) INNER JOIN WHLOC AS L ON H.MANDT=L.MANDT  COLLATE Chinese_Taiwan_Stroke_CI_AS AND H.COMCD=L.COMCD  COLLATE Chinese_Taiwan_Stroke_CI_AS AND H.LGORT=L.LGORT  COLLATE Chinese_Taiwan_Stroke_CI_AS   AND  H.LOCAT=L.LOCAT  COLLATE Chinese_Taiwan_Stroke_CI_AS where ");
                        sbSql.AppendFormat("H.MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("and H.COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("and H.WERKS='{0}' ", strWerks);
                        sbSql.AppendFormat("and H.LGORT='{0}' ", strLgort);
                        sbSql.AppendFormat("and H.LOSTS='{0}' ", "0");
                        sbSql.AppendFormat(" and L.MATNR='{0}'", strMat);
                        sbSql.AppendFormat(" order by H.LGORT, H.LOCAT ASC", "");


                        ControlHandleDB();
                        dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    else
                    {
                        // string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                        //" REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                        //"  from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and Block = '0'";

                        //Modify by Jack 20150610
                        string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                            " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                            "  from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";
                        if (strLocat != "")
                        {
                            strSQL += " and LOCAT ='" + strLocat + "'";
                        }

                        if (strType == "0") //空儲位
                        {
                            strSQL += " and LOSTS='0' ";
                        }
                        else if (strType == "1") //有庫存儲位
                        {
                            strSQL += " and LOSTS='1' ";
                        }
                        else if (strType == "2") //全部儲位
                        {
                            strSQL += "";
                        }
                        else if (strType == "3") //尚未設定位置儲位
                        {
                            strSQL += " and STLEN is null and STWID is null ";
                        }

                        if (strCtbto != "")
                        {
                            strSQL += " and CTBTO ='" + strCtbto + "'";
                        }

                        if (strRegon != "")
                        {
                            strSQL += " and REGON ='" + strRegon + "'";
                        }
                        strSQL += " AND COMCD='" + COMCD + "' and Block = '0'";

                        ControlHandleDB();
                        dt = ControlSqlAccess.GetDataTable(strSQL);
                        ControlSqlAccess.CloseConnection();
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
            #endregion


            # region 獲得儲位資料(不包含待出貨儲位)(SpareParts)
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatNoneShip(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatNoneShip";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strType + "','" + strCtbto + "','" + strRegon + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    // string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    //" REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    //"  from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and Block = '0' and LEFT(LOCAT,1) <> 'N'";

                    //Modify by Jack 20150610
                    string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                        " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                        "  from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";
                    if (strLocat != "")
                    {
                        strSQL += " and LOCAT ='" + strLocat + "'";
                    }

                    if (strType == "0") //空儲位
                    {
                        strSQL += " and LOSTS='0' ";
                    }
                    else if (strType == "1") //有庫存儲位
                    {
                        strSQL += " and LOSTS='1' ";
                    }
                    else if (strType == "2") //全部儲位
                    {
                        strSQL += "";
                    }
                    else if (strType == "3") //尚未設定位置儲位
                    {
                        strSQL += " and STLEN is null and STWID is null ";
                    }

                    if (strCtbto != "")
                    {
                        strSQL += " and CTBTO ='" + strCtbto + "'";
                    }

                    if (strRegon != "")
                    {
                        strSQL += " and REGON ='" + strRegon + "'";
                    }
                    strSQL += " AND COMCD='" + COMCD + "' and Block = '0'";

                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSQL);
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
            #endregion


            # region 獲得儲位資料--大储位用于半成品调拨
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 獲得儲位資料--大储位用于半成品调拨
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllBigLocatCSMC(string strWerks, string strLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatNoneShip";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat(" SELECT CTRLC2 AS LOCAT  FROM WHCTRL WHERE MANDT='QCI' AND SOLDTO='QWMS' AND CTRLID='BGLOC' AND CTRLNM='{0}' AND CTRLC1='{1}' ", strWerks, strLgort);
                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strsql.ToString());
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
            #endregion

            # region 获得CS41asrs仓别
            public DataTable GetAsrsLgort()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAsrsLgort";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "SELECT CTRLNM AS WERKS,CTRLC1 AS LGORT FROM WHCTRL WITH(NOLOCK) WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='LGORT'  AND CTRLNM='CS41' AND CTRLC4='ASRS'";
                    DataTable dt = new DataTable();
                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(strSQL);
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
            #endregion

            # region 获得CS41asrs一个储位
            public DataTable GetAsrsLocat(string strWerks, string strLgort,string strasrs)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAsrsLocat";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strasrs + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtLocat = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("SELECT TOP 1 LOCAT FROM WHHED WITH(NOLOCK)");
                    sbSql.AppendFormat(" WHERE MANDT= '{0}'", MANDT);
                    sbSql.AppendFormat(" and COMCD='{0}'", COMCD);
                    sbSql.AppendFormat(" and WERKS = '{0}'", strWerks);
                    sbSql.AppendFormat(" and LGORT= '{0}'", strLgort);
                    sbSql.AppendFormat(" and LEFT(LOCAT,2) = '{0}'", strasrs);
                    sbSql.AppendFormat(" and LOSTS='0'");
                    sbSql.AppendFormat(" and Block = '0'");
                    sbSql.AppendFormat(" ORDER BY SUBSTRING(locat, 5, 2),SUBSTRING(locat, 3, 2)");


                    ControlHandleDB();
                    dtLocat = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtLocat;
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

            # region 获得CS41asrs仓别前两位(座别01-02一座)
            public DataTable GetAsrsLeft(string strWerks,string strLgort)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAsrsLeft";
                this.ControlMethodParm = "('" + strWerks + "'," + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtleft = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("SELECT DISTINCT LEFT(LOCAT,2) AS F_TEXT FROM WHHED WITH(NOLOCK) ");
                    sbSql.AppendFormat(" WHERE MANDT= '{0}'", MANDT);
                    sbSql.AppendFormat(" and COMCD='{0}'", COMCD);
                    sbSql.AppendFormat(" and WERKS = '{0}'", strWerks);
                    sbSql.AppendFormat(" and LGORT= '{0}'", strLgort);
                    sbSql.AppendFormat(" and LOSTS='0'");
                    sbSql.AppendFormat(" and Block = '0'");
                    sbSql.AppendFormat(" ORDER BY F_TEXT");

                    ControlHandleDB();
                    dtleft = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtleft;
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

            #region 查詢目前廠區, 倉別及儲位的使用狀態(刪除前使用)
            //////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢目前廠區, 倉別及儲位的使用狀態(刪除前使用)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            //////////////////////////////////////////////////////////////////////////////////////////	
            public bool CheckStorageData(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckStorageData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhhed objWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    alColumns.Add(" * ");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + strWerks + "'");
                    alConditions.Add(" LOSTS='1'");

                    if (strLgort != "")
                    {
                        alConditions.Add(" LGORT='" + strLgort + "'");

                    }

                    if (strLocat != "")
                    {
                        alConditions.Add(" LOCAT='" + strLocat + "'");
                    }

                    DataTable dtData = new DataTable();
                    try
                    {
                        dtData = objWhhed.EntityQuery(alColumns, alConditions, false, true);

                    }
                    catch (System.Exception ex)
                    {

                        ERRMSG = ex.Message + "<- CheckStorageData()";
                    }

                    if (dtData.Rows.Count > 0)
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
            # endregion

            #region 查詢目前廠區, 倉別及儲位的使用狀態(刪除前使用)
            /// <summary>
            /// 查詢目前廠區, 倉別及儲位的使用狀態(刪除前使用)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  PlantData objPlantData =new PlantData(UserData);
            ///  DataTable dtData = objPlantData.CheckStorageData(strWerks,strLgort,strLocat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckStorageData(string strWerks, string strLgort, string strLocat, string strStset)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckStorageData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "','" + strStset + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select * from WHHED where MANDT= '" + strMandt + "'  AND COMCD='" + COMCD + "' and WERKS= '" + strWerks + "' and LOSTS='1'";

                    if (strLgort != "")
                    {
                        strSQL += " and LGORT ='" + strLgort + "'";
                    }

                    if (strLocat != "")
                    {
                        strSQL += " and LOCAT ='" + strLocat + "'";
                    }

                    if (strStset != "")
                    {
                        strSQL += " and STSET ='" + strStset + "'";
                    }

                    DataTable dtData = new DataTable();
                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {

                        ERRMSG = ex.Message + "<- CheckStorageData()";
                    }

                    if (dtData.Rows.Count > 0)
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

            # region 厂区下拉选单资料
            /// <summary>
            /// 廠區下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  PlantData objPlantData =new PlantData(UserData);
            ///  DataTable dtData = objPlantData.GetDdlWerksLgortData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlWerksLgortData()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlWerksLgortData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select distinct CTRLNM, CTRLC1, MANDT, SOLDTO, CTRLID, CTRLC2, CTRLC4, CTRLC5, COMCD from WHCTRL where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and SOLDTO='QWMS' and CTRLID='LGORT' order by CTRLNM, CTRLC1";

                    DataTable dtData = new DataTable();
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
            # endregion

            # region 传入权限名称货代对应的权限代码
            public DataTable GetProgramCode(string strProgramName)
            {


                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetProgramCode";
                this.ControlMethodParm = "('" + strProgramName + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append("select CTRLC2 from WHCTRL where MANDT='QCI' and SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC4 in (" + strProgramName + ") ");


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

            # region 檢查料號是否存在
            /// <summary>
            /// 檢查料號是否存在
            /// </summary> 
            /// <param name="strMatnr">料號。</param>
            /// <returns>
            /// bool 。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  PlantData objPlantData =new PlantData(UserData);
            ///  bool strReturn = objPlantData.CheckExistedMatnr(strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool CheckExistedMatnr(string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlWerksLgortData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataWhpat objWhpat = new DataWhpat(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alQueryConditions = new ArrayList();

                    DataTable dtData = new DataTable();

                    alColumns.Add("1");
                    alQueryConditions.Add("MATNR = '" + strMatnr + "'");

                    dtData = objWhpat.EntityQuery(alColumns, alQueryConditions, false, true);

                    //string strSQL = "Select * from WHPAT where MATNR= '" + strMatnr + "'";
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    //ControlSqlAccess.CloseConnection();

                    if (dtData.Rows.Count > 0)
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
            # endregion

            #region 廠區下拉選單資料
            //=========================================================================
            /// <summary>
            /// 廠區下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDdlWerksData();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlWerksData()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlWerksData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select distinct CTRLC1 as F_TEXT, CTRLNM as F_VALUE from WHCTRL where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and SOLDTO='QWMS' and CTRLID='WERKS' order by CTRLNM";
                    DataTable dtData = new DataTable();
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

            #region 倉別類型下拉選單資料
            /// <summary>
            /// 倉別類型下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDdlSttyp();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlSttyp()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlSttyp";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select * from WHCTRL where MANDT= 'QCI' and SOLDTO='QWMS' and CTRLID='STTYP'";
                    DataTable dtData = new DataTable();
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

            #region 儲位類型下拉選單資料
            /// <summary>
            /// 儲位類型下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDdlLotyp();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlLotyp()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlLotyp";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select * from WHCTRL where MANDT= 'QCI' and SOLDTO='QWMS' and CTRLID='LOTYP'";
                    DataTable dtData = new DataTable();
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

            #region 儲位狀態查詢
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 儲位狀態查詢
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetLocatPosition(strWerks,strLgort,strLocat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetLocatPosition(string strWerks, string strLgort, string strLocat)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetLocatPosition";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();


                    sbSql.Append("Select * from WHHED where  ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("LGORT='{0}' ", strLgort);

                    if (strLocat != "")
                    {
                        sbSql.AppendFormat("LOCAT='{0}' ", strLocat);

                    }

                    DataTable dtData = new DataTable();
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

            #region 查詢Row/Column設定的名稱
            /// <summary>
            /// 查詢Row/Column設定的名稱
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strStset">座別。</param>
            /// <param name="strSthgh">高度。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  bool bolResult = objPlantData.QueryRowColName(strWerks,strLgort,strStset,strSthgh);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryRowColName(string strWerks, string strLgort, string strStset, string strSthgh)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryRowColName";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strStset + "','" + strSthgh + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "select * from WHLAY where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and STSET='" + strStset + "' and STHGH='" + strSthgh + "'";
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
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

            #region 查詢Row/Column設定的名稱
            /// <summary>
            /// 查詢Row/Column設定的名稱
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strStset">座別。</param>
            /// <param name="strSthgh">高度。</param>
            /// param name="strRctyp">Row/Column類型(R or C)。</param>
            /// <param name="strRcnum">Row/Column代碼。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  string strResult = objPlantData.QueryRowColName(strWerks,strLgort,strRctyp,intRcnum);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public string QueryRowColName(string strWerks, string strLgort, string strStset, string strSthgh, string strRctyp, int intRcnum)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryRowColName";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strStset + "','" + strSthgh + "','" + strRctyp + "','" + intRcnum + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "select * from WHLAY where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'  and STSET='" + strStset + "' and STHGH='" + strSthgh + "' and RCTYP='" + strRctyp + "' and RCNUM='" + intRcnum.ToString() + "'";
                    string strResult = "";
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    if (dtData.Rows.Count > 0)
                    {
                        strResult = dtData.Rows[0]["RCNAM"].ToString();
                    }
                    ControlSqlAccess.CloseConnection();

                    return strResult;
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

            #region 設定Row/Column設定的名稱
            /// <summary>
            /// 設定Row/Column設定的名稱
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strStset">座別。</param>
            /// <param name="strSthgh">高度。</param>
            /// <param name="strRctyp">Row/Column類型(R or C)。</param>
            /// <param name="strRcnum">Row/Column代碼。</param>
            /// <param name="strRcnam">Row/Column名稱。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  bool bolResult = objPlantData.UpdateRowColName(strWerks,strLgort,strStset,strSthgh,strRctyp,intRcnum,strRcnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool UpdateRowColName(string strWerks, string strLgort, string strStset, string strSthgh, string strRctyp, int intRcnum, string strRcnam)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateRowColName";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strStset + "','" + strSthgh + "','" + strRctyp + "','" + intRcnum + "','" + strRcnam + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "";
                    bool bolResult = false;
                    DataTable dtData = new DataTable();
                    strSQL = "select * from WHLAY where MANDT= '" + MANDT + "'  and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and STSET='" + strStset + "' and STHGH='" + strSthgh + "' and RCTYP='" + strRctyp + "' and RCNUM='" + intRcnum.ToString() + "'";

                    ControlHandleDB();
                    if (this.ControlSqlAccess.GetDataTable(strSQL).Rows.Count > 0)
                    {
                        strSQL = "update WHLAY set RCNAM=N'" + strRcnam + "',MONAM='" + CRNAM + "', MODAT=getdate() where MANDT= '" + MANDT + "'  and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and STSET='" + strStset + "' and STHGH='" + strSthgh + "' and RCTYP='" + strRctyp + "' and RCNUM='" + intRcnum.ToString() + "'";
                    }
                    else
                    {
                        strSQL = "insert into WHLAY(MANDT,COMCD, WERKS, LGORT, STSET, STHGH, RCTYP, RCNUM, RCNAM, CRNAM, CRDAT, MONAM, MODAT) values('" + MANDT + "','" + COMCD + "', '" + strWerks + "', '" + strLgort + "', '" + strStset + "', '" + strSthgh + "', '" + strRctyp + "', '" + intRcnum.ToString() + "', N'" + strRcnam + "', '" + CRNAM + "', getdate(), '" + CRNAM + "', getdate())";
                    }
                    bolResult = this.ControlSqlAccess.ExecSql(strSQL);
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

            #region 儲位狀態查詢
            /// <summary>
            /// 儲位狀態查詢
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetLocatData(strWerks,strLgort,strLocat);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetLocatData(string strWerks, string strLgort, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetLocatData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "Select * from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and STLEN is null and STWID is null";

                    if (strLocat != "")
                    {
                        strSQL += " and LOCAT ='" + strLocat + "'";
                    }

                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
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

            #region 倉別下拉選單資料
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 倉別下拉選單資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDdlLgortData()
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlLgortData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select distinct CTRLC1 as F_TEXT, CTRLC2 as F_VALUE,  ");
                    sbSql.Append("MANDT, COMCD,SOLDTO, CTRLID, CTRLNM, CTRLC1, CTRLC2, CTRLC4, CTRLC5 from WHCTRL where ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("and SOLDTO='{0}' ", "QWMS");
                    sbSql.AppendFormat("and CTRLID='{0}' ", "LGORT");
                    sbSql.Append(" order by CTRLC2");



                    DataTable dtData = new DataTable();
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

            #region 查詢目前設定料號和儲位對應資料(固定儲位)
            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 查詢目前設定料號和儲位對應資料(固定儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strMatnr">料號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.QueryMappingData(strWerks,strLgort,strLocat,strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryMappingData(string strWerks, string strLgort, string strLocat, string strMatnr)
            {


                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDdlLgortData";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataWhmap objWhmap = new DataWhmap(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alQueryCondition = new ArrayList();
                    DataTable dtData = new DataTable();

                    alColumns.Add("*");
                    alQueryCondition.Add("MANDT = '" + UserData.Client + "'");
                    alQueryCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                    alQueryCondition.Add("WERKS = '" + strWerks + "'");
                    alQueryCondition.Add("LGORT = '" + strLgort + "'");

                    //StringBuilder sbSql = new StringBuilder();
                    //sbSql.Append("Select * from WHMAP where  ");

                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    //sbSql.AppendFormat("and LGORT='{0}' ", strLgort);

                    if (strLocat != "")
                    {
                        alQueryCondition.Add("LOCAT = '" + strLocat + "'");
                        //sbSql.AppendFormat("and LOCAT='{0}' ", strLocat);

                    }
                    if (strMatnr != "")
                    {
                        alQueryCondition.Add("MATNR = '" + strMatnr + "'");
                        //sbSql.AppendFormat("and MATNR='{0}' ", strMatnr);

                    }

                    dtData = objWhmap.EntityQuery(alColumns, alQueryCondition, false, true);
                    dtData = CommonInfo.SortDataTable(dtData, "MANDT, WERKS, LGORT, MATNR, LOCAT");

                    //sbSql.AppendFormat("order by MANDT, WERKS, LGORT, MATNR, LOCAT");
                    //DataTable dtData = new DataTable();
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

            #region 取得空白儲位(考慮CTO/BTO & Region & Machine)
            //===========================================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得空白儲位(考慮CTO/BTO & Region & Machine)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">Region。</param>
            /// <param name="strMachine">Machine。</param>
            /// <returns>
            /// string 。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  string strReturn = objPlantData.GetEmptyLocation(string strWerks, string strLgort, string strCtbto, string strRegon, string strMachine);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public string GetEmptyLocation(string strWerks, string strLgort, string strCtbto, string strRegon, string strMachine)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetEmptyLocation";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strCtbto + "," + strRegon + "," + strMachine + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select * from WHHED where ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and LOSTS='{0}' ", "0");

                    //if (strCtbto.Trim() != "")
                    //{
                    //    sbSql.AppendFormat("and CTBTO='{0}' ", strCtbto);
                    //}

                    if (strRegon.Trim() != "")
                    {
                        sbSql.AppendFormat("and REGON='{0}' ", strRegon);
                    }

                    sbSql.AppendFormat(" order by LGORT, LOCAT ASC", "");

                    DataTable dtDataCTBTO = new DataTable();
                    ControlHandleDB();
                    dtDataCTBTO = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtDataCTBTO.Rows.Count > 0)
                    {

                        DataRow[] drLocation = dtDataCTBTO.Select(" MAPID='" + strMachine + "' ", "LOCAT");
                        if (drLocation.Length > 0)
                        {
                            return drLocation[0]["LOCAT"].ToString();

                        }
                        else
                        {
                            return dtDataCTBTO.Rows[0]["LOCAT"].ToString();
                        }
                    }
                    else
                    {
                        #region 如依CTBTO規則抓不到儲位，則隨便抓一筆沒庫存的空儲位
                        sbSql.Remove(0, sbSql.Length);
                        sbSql.Append("Select top 1 LOCAT from WHHED where ");
                        sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                        sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                        sbSql.AppendFormat("and LOSTS='{0}' ", "0");
                        sbSql.AppendFormat(" order by LGORT", "");

                        DataTable dtDataNomal = new DataTable();
                        try
                        {
                            dtDataNomal = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

                        if (dtDataNomal.Rows.Count > 0)
                        {
                            return dtDataNomal.Rows[0]["LOCAT"].ToString();
                        }
                        else
                        {
                            return "";
                        }
                        #endregion
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

            #region 取得自动跑储空白儲位
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            ///  取得自动跑储空白儲位
            /// </summary> 
            /// <returns>
            /// string
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public string GetAutoRunEmptyLocation(string strWerks, string strLgort, string strType, string strPdnam)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAutoRunEmptyLocation";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strType + "," + strPdnam + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select * from WHHED where ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and LOSTS='{0}' ", "0");
                    sbSql.AppendFormat("and ISATL = 'Y'");
                    if (strType == "P")
                    {
                        sbSql.AppendFormat("and TYPE = 'P'");
                    }
                    if (strType == "NP")
                    {
                        sbSql.AppendFormat("and TYPE = 'NP'");
                    }
                    if (strPdnam != "")
                    {
                        sbSql.AppendFormat("and PDNAM = '" + strPdnam + "'");
                    }

                    //if (strCtbto.Trim() != "")
                    //{
                    //    sbSql.AppendFormat("and CTBTO='{0}' ", strCtbto);
                    //}

                    sbSql.AppendFormat(" order by LGORT, LOCAT ASC", "");

                    DataTable dtDataCTBTO = new DataTable();
                    ControlHandleDB();
                    dtDataCTBTO = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dtDataCTBTO.Rows.Count > 0)
                    {
                        return dtDataCTBTO.Rows[0]["LOCAT"].ToString();
                    }
                    else
                    {
                        return "";
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

            #region 取得自动跑储已有料号儲位
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            ///  取得自动跑储已有料号儲位
            /// </summary> 
            /// <returns>
            /// string
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public string GetAutoRunAddLocation(string strWerks, string strLgort, string strType, string strPdnam)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAutoRunAddLocation";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strType + "," + strPdnam + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    List<List<string>> ListMin = new List<List<string>>();
                    StringBuilder sbSql = new StringBuilder();

                    //获得自动跑储已有料号儲位
                    sbSql.Append("Select * from WHHED where ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and LOSTS='{0}' ", "1");
                    sbSql.AppendFormat("and ISATL = 'Y'");
                    if (strType == "P")
                    {
                        sbSql.AppendFormat("and TYPE = 'P'");
                    }
                    if (strType == "NP")
                    {
                        sbSql.AppendFormat("and TYPE = 'NP'");
                    }
                    if (strPdnam != "")
                    {
                        sbSql.AppendFormat("and PDNAM = '" + strPdnam + "'");
                    }

                    sbSql.AppendFormat(" order by LGORT, LOCAT ASC", "");

                    DataTable dtLocat = new DataTable();

                    ControlHandleDB();
                    dtLocat = ControlSqlAccess.GetDataTable(sbSql.ToString());

                    //获得此储位PID/RID
                    for (int i = 0; i < dtLocat.Rows.Count; i++)
                    {
                        DataTable dtData = new DataTable();
                        StringBuilder sbSql2 = new StringBuilder();
                        sbSql2.AppendFormat(
                            "SELECT MBLNR FROM WHITM WHERE");

                        sbSql2.AppendFormat(" MANDT='" + MANDT + "'");
                        sbSql2.AppendFormat(" AND COMCD='" + COMCD + "'");
                        sbSql2.AppendFormat(" AND WERKS='" + strWerks + "'");
                        sbSql2.AppendFormat(" AND LGORT='" + strLgort + "'");
                        sbSql2.AppendFormat(" AND LOCAT='" + dtLocat.Rows[i]["LOCAT"].ToString() + "'");
                        sbSql2.AppendFormat(" GROUP BY MBLNR");

                        dtData = ControlSqlAccess.GetDataTable(sbSql2.ToString());
                        if (dtData.Rows.Count < Convert.ToInt32(dtLocat.Rows[i]["RIDNO"].ToString()))
                        {
                            int num = Convert.ToInt32(dtLocat.Rows[i]["RIDNO"].ToString()) - dtData.Rows.Count;
                            List<string> ListLocat = new List<string>();
                            ListLocat.Add(num.ToString());
                            ListLocat.Add(dtLocat.Rows[i]["LOCAT"].ToString());
                            ListMin.Add(ListLocat);
                        }
                    }
                    ControlSqlAccess.CloseConnection();

                    if (ListMin.Count > 0)
                    {
                        int a = Convert.ToInt32(ListMin[0][0]);
                        string strLocat = ListMin[0][1].ToString();
                        for (int i = 0; i < ListMin.Count; i++)
                        {
                            if (Convert.ToInt32(ListMin[i][0]) < a)
                            {
                                a = Convert.ToInt32(ListMin[i][0]);
                                strLocat = ListMin[i][1];
                            }
                        }
                        return strLocat;
                    }
                    else
                    {
                        return "";
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


            #region 取得空白儲位(GB根据仓别和料头抓取储位)
            //===========================================================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得空白儲位(GB根据仓别和料头抓取储位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">Region。</param>
            /// <param name="strMachine">Machine。</param>
            /// <returns>
            /// string 。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  string strReturn = objPlantData.GetEmptyLocation(string strWerks, string strLgort, string strCtbto, string strRegon, string strMachine);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public string GetEmptyLocationByMatnr(string strWerks, string strLgort, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetEmptyLocation";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    if (strMatnr != "")
                    {
                        sbSql.Append("Select * from WHHED AS H WITH (NOLOCK) INNER JOIN WHLOC AS L ON H.MANDT=L.MANDT  COLLATE Chinese_Taiwan_Stroke_CI_AS AND H.COMCD=L.COMCD  COLLATE Chinese_Taiwan_Stroke_CI_AS AND H.LGORT=L.LGORT  COLLATE Chinese_Taiwan_Stroke_CI_AS   AND  H.LOCAT=L.LOCAT  COLLATE Chinese_Taiwan_Stroke_CI_AS where ");
                        sbSql.AppendFormat("H.MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("and H.COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("and H.WERKS='{0}' ", strWerks);
                        sbSql.AppendFormat("and H.LGORT='{0}' ", strLgort);
                        sbSql.AppendFormat("and H.LOSTS='{0}' ", "0");
                        sbSql.AppendFormat(" and L.MATNR='{0}'", strMatnr);
                        sbSql.AppendFormat(" order by H.LGORT, H.LOCAT ASC", "");
                    }
                    else
                    {
                        sbSql.Append("Select * from WHHED where ");
                        sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                        sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                        sbSql.AppendFormat("and LOSTS='{0}' ", "0");
                        sbSql.AppendFormat("and LOCAT NOT IN(SELECT DISTINCT LOCAT FROM WHLOC WITH(NOLOCK))");
                        sbSql.AppendFormat(" order by LGORT, LOCAT ASC", "");
                    }





                    DataTable dtDataCTBTO = new DataTable();
                    ControlHandleDB();
                    dtDataCTBTO = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtDataCTBTO.Rows[0]["LOCAT"].ToString();

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



            #region 取得料架儲位
            //============================================================================================================================================================
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得料架儲位
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLosts">0: 無庫存  1:有庫存。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">Region。</param>
            /// <param name="strMachine">Machine。</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  string strReturn = objPlantData.GetEmptyLocation(string strWerks, string strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetPartNoLocation(string strWerks, string strLgort, string strLosts, string strCtbto, string strRegon, string strMachine, string strVersion)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPartNoLocation";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtDataCTBTO = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select * from WHHED where ");
                    sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                    sbSql.AppendFormat(" and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat(" and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat(" and PNLOC='{0}' ", "1");
                    sbSql.AppendFormat(" and LOSTS='{0}' ", strLosts);
                    if (strCtbto.Trim() != "")
                    {
                        sbSql.AppendFormat(" and CTBTO='{0}' ", strCtbto);
                    }
                    if (strRegon.Trim() != "")
                    {
                        sbSql.AppendFormat(" and REGON='{0}' ", strRegon);
                    }
                    if (strVersion.Trim() != "")
                    {
                        sbSql.AppendFormat(" and Version='{0}' ", strVersion);
                    }
                    sbSql.AppendFormat(" order by LGORT, LOCAT ASC");

                    ControlHandleDB();
                    dtDataCTBTO = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtDataCTBTO.Rows.Count > 0)
                    {
                        //DataRow[] drLocation = dtDataCTBTO.Select(" MAPID='" + strMachine + "' ", "LOCAT");
                        //if (drLocation.Length > 0)
                        //{
                        //    return drLocation[0]["LOCAT"].ToString();
                        //}
                        //else
                        //{
                        //    return dtDataCTBTO.Rows[0]["LOCAT"].ToString();
                        //}
                        return dtDataCTBTO;
                    }
                    else
                    {
                        #region 如依CTBTO規則抓不到儲位
                        sbSql.Remove(0, sbSql.Length);
                        sbSql.Append("Select top 1 * from WHHED where ");
                        sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                        sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                        sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                        sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                        sbSql.AppendFormat("and LOSTS='{0}' ", strLosts);
                        sbSql.AppendFormat(" order by LGORT", "");

                        DataTable dtDataNomal = new DataTable();
                        try
                        {
                            dtDataNomal = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

                        //if (dtDataNomal.Rows.Count > 0)
                        //{
                        //    return dtDataNomal.Rows[0]["LOCAT"].ToString();
                        //}
                        //else
                        //{
                        //    return "";
                        //}
                        return dtDataNomal;
                        #endregion
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

            #region 取得儲位及設定滿板數量的資料
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得儲位及設定滿板數量的資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// TWQ.QWMS.Setting objData =new TWQ.QWMS.Setting(UserData, Progid);
            /// DataTable dtData = objOutbound.GetLocationData("ZYEZZYE");
            /// Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetLocationData(string strWerks, string strLgort, string strLocat, string strMatnr, string strKdmat, string strCharg, string strPNLOC)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetLocationData";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    #region 變數宣告
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    #endregion
                    #region 查詢欄位
                    sbSql.Append("SELECT I.MENGE,H.PALQTY,* ");
                    sbSql.Append("from WHHED H inner join WHITM I ");
                    sbSql.Append("on H.WERKS = I.WERKS AND H.LGORT = I.LGORT AND H.LOCAT = I.LOCAT ");
                    #endregion
                    #region 查詢條件
                    sbSql.Append("where 1=1 ");
                    sbSql.Append("and I.Werks ='" + strWerks.Trim() + "'");
                    sbSql.Append("and I.Lgort ='" + strLgort.Trim() + "'");
                    if (!string.IsNullOrEmpty(strLocat))
                    {
                        sbSql.Append(" and I.Locat ='" + strLocat.Trim() + "'");
                    }
                    if (!string.IsNullOrEmpty(strMatnr))
                    {
                        sbSql.Append(" and I.Matnr ='" + strMatnr.Trim() + "'");
                    }
                    if (!string.IsNullOrEmpty(strKdmat))//客人料號
                    {
                        sbSql.Append(" and I.Kdmat ='" + strKdmat.Trim() + "'");
                    }
                    if (!string.IsNullOrEmpty(strCharg))
                    {
                        sbSql.Append(" and I.Charg ='" + strCharg.Trim() + "'");
                    }
                    if (!string.IsNullOrEmpty(strPNLOC))
                    {
                        //0:平面儲位 1:料架儲位
                        sbSql.Append(" and H.PNLOC ='" + strPNLOC.Trim() + "'");
                    }
                    #endregion
                    #region 執行SQL
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    #endregion

                    //dtData = CommonInfo.SortDataTable(dtData, "LGORT,LOCAT,MENGE ASC");
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

            #region 抓取PALID的資料
            //============================================================================================
            ////////////Summary by Smose Liao 20091023////////////////////////////////////////////////////
            /// <summary>
            /// 抓取PALID的資料
            /// </summary> 
            /// <param name="strMandt">Client</param>
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strPalid">PALID</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.GetPALInfo(strMandt,strWerks, strLgort,strPalid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetPALInfo(string strMandt, string strWerks, string strLgort, string strPalid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPALInfo";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strPalid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                alConditions.Add(" MBLNR='" + strPalid.Trim() + "'");
                alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS') ");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            #region 抓取Reference id的資料
            //============================================================================================
            ////////////Summary by Smose Liao 20101228////////////////////////////////////////////////////
            /// <summary>
            /// 抓取Reference id的資料
            /// </summary> 
            /// <param name="strMandt">Client</param>
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strRefid">Reference ID</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.GetPALInfo(strMandt, strWerks, strLgort, strRefid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetRefIdInfo(string strMandt, string strWerks, string strLgort, string strRefid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetRefIdInfo";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strRefid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                alConditions.Add(" REFID='" + strRefid.Trim() + "'");
                alConditions.Add(" MTYPE='QMS_M' ");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            #region 取得空白儲位
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 取得空白儲位
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /////////////////////////////////////////////////////////////////////////////	
            public string GetEmptyLocation(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetEmptyLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    DataTable dtData = new DataTable();
                    DataWhhed objWhhed = new DataWhhed(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    alColumns.Add(" Top 1 LOCAT ");

                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + strWerks + "'");
                    alConditions.Add(" LGORT='" + strLgort + "'");
                    alConditions.Add(" LOSTS='0' Order by LGORT ");

                    dtData = objWhhed.EntityQuery(alColumns, alConditions, false);

                    #region delete
                    //StringBuilder sbSql = new StringBuilder();
                    //sbSql.Append("Select top 1 LOCAT from WHHED where  ");
                    //sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                    //sbSql.AppendFormat("and COMCD='{0}' ", COMCD);
                    //sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    //sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    //sbSql.AppendFormat("and LOSTS='{0}' ", "0");
                    //sbSql.AppendFormat("order by LGORT");
                    // ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    //ControlSqlAccess.CloseConnection();
                    #endregion

                    if (dtData.Rows.Count > 0)
                    {
                        return dtData.Rows[0]["LOCAT"].ToString();
                    }
                    else
                    {
                        return "";
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

            #region 取得未使用的待出貨儲位(SpareParts)
            //=========================================================================
            ////////////Summary by Smose Liao 20100604/////////////////////////////////
            /// <summary>
            /// 取得未使用的待出貨儲位(SpareParts)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="bolAddIn">是否選擇有庫存的儲位。</param>
            ///////////////////////////////////////////////////////////////////////////
            public DataTable GetShipmentLocation(string strWerks, string strLgort, bool bolAddIn)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetShipmentLocation";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + bolAddIn + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    DataWhhed objWhhed = new DataWhhed(UserData);

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();

                    alColumns.Add("LOCAT");

                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + strWerks + "'");
                    alConditions.Add(" LGORT='" + strLgort + "'");
                    alConditions.Add(" left(LOCAT,1) = 'N' ");
                    if (bolAddIn == true)
                    {
                        alConditions.Add(" LOSTS='1' Order by LGORT ");
                    }
                    else
                    {
                        alConditions.Add(" LOSTS='0' Order by LGORT ");
                    }

                    dtData = objWhhed.EntityQuery(alColumns, alConditions, false);

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

            #region 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            //====================================================================================
            ////////////Summary by Smose Liao 20090422////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatData_DateCode(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData_DateCode";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strLocat + "," + strType + "," + strCtbto + "," + strRegon + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //新增儲位維護的條件(Block = 0)   Smose Liao 20090720
                //string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                //    " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                //    "  from WHHED where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and Block = '0'";

                //Modify by Jack 20150610
                string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''), " +
                    " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'')" +
                    "  from WHHED WITH(NOLOCK) where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";
                if (strLocat != "")
                {
                    strSQL += " and LOCAT ='" + strLocat + "'";
                }

                if (strType == "0") //空儲位
                {
                    strSQL += " and LOSTS='0' ";
                }
                else if (strType == "1") //有庫存儲位
                {
                    strSQL += " and LOSTS='1' ";
                }
                else if (strType == "2") //全部儲位
                {
                    strSQL += "";
                }
                else if (strType == "3") //尚未設定位置儲位
                {
                    strSQL += " and STLEN is null and STWID is null ";
                }

                if (strCtbto != "")
                {
                    strSQL += " and CTBTO ='" + strCtbto + "'";
                }

                if (strRegon != "")
                {
                    strSQL += " and REGON ='" + strRegon + "'";
                }
                strSQL += " AND COMCD='" + COMCD + "' and Block = '0'";

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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

            #region 不屬於某筆料號(MATNR)的儲位，以及與某筆料號相同，且DateCode與LockCode也相同的所有儲位
            //====================================================================================
            ////////////Summary by Smose Liao 20090427////////////////////////////////////////////
            /// <summary>
            /// 不屬於某筆料號(MATNR)的儲位，以及與某筆料號相同，且DateCode與LockCode也相同的所有儲位
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strMatnr">料號。</param>
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetStorageLocatData(strWerks,strLgort,strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public ArrayList GetStorageLocatData(string strWerks, string strLgort, string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetStorageLocatData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //取得該筆扣帳編號的料號(Matnr)  Smose Liao 20090427
                string strMatnr = "";
                string strDacod = "";
                string strLocod = "";
                StringBuilder sbSql = new StringBuilder();
                //string strSQL = "Select MATNR,SERNO,LOCOD from WHITM where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and MBLNR = '" + strMblnr + "'";
                sbSql.Append("Select MATNR, DACOD, LOCOD from WHITM where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat("AND MBLNR='{0}' ", strMblnr);
                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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
                if (dtData.Rows.Count > 0)
                {
                    strMatnr = dtData.Rows[0]["MATNR"].ToString();
                    strDacod = dtData.Rows[0]["DACOD"].ToString();
                    strLocod = dtData.Rows[0]["LOCOD"].ToString();
                }

                //取得所有不屬於某筆料號的儲位 Smose Liao 20090427
                //strSQL = "Select distinct LOCAT from WHITM where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and MATNR <> '" + strMatnr + "'";
                sbSql.Append("Select distinct LOCAT from WHITM where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat("AND MATNR<>'{0}' ", strMatnr);
                DataTable dt1Data = new DataTable();
                ArrayList alData = new ArrayList();
                try
                {
                    ControlHandleDB();
                    dt1Data = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    for (int i = 0; i < dt1Data.Rows.Count; i++)
                    {
                        alData.Add(dt1Data.Rows[i]["LOCAT"].ToString());
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

                //取得與某筆料號相同，且DateCode與LockCode也相同的儲位  Smose Liao 20090427
                //strSQL = "Select distinct LOCAT from WHITM where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and MATNR = '" + strMatnr + "' and SERNO = '" + strSerno + "' and LOCOD = '" + strLocod + "'";
                sbSql.Append("Select distinct LOCAT from WHITM where  ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat("AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat("AND MATNR='{0}' ", strMatnr);
                sbSql.AppendFormat("AND DACOD='{0}' ", strDacod);
                sbSql.AppendFormat("AND LOCOD='{0}' ", strLocod);
                DataTable dt2Data = new DataTable();
                ArrayList alDateCode = new ArrayList();
                try
                {
                    ControlHandleDB();
                    dt2Data = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    for (int i = 0; i < dt2Data.Rows.Count; i++)
                    {
                        alDateCode.Add(dt2Data.Rows[i]["LOCAT"].ToString());
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

                //合併所有儲位  Smose Liao 20090427
                alData.AddRange(alDateCode);
                return alData;
            }

            #endregion

            #region  取得不屬於與DataGrid上面相同料號(MATNR)的所有儲位
            //==================================================================================================================================
            ////////////Summary by Smose Liao 2009118///////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得不屬於與DataGrid上面相同料號(MATNR)的所有儲位
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="aryMatnr">料號。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetStorageLocatData(strWerks,strLgort,strMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public ArrayList GetStorageLocatData_DateCode(string strWerks, string strLgort, ArrayList aryMatnr, string strCtbto, string strRegon)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetStorageLocatData_DateCode";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + aryMatnr + "," + strCtbto + "," + strRegon + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                DataTable dt1Data = new DataTable();
                ArrayList aryAllStorageLocat = new ArrayList();
                ArrayList aryStorageLocat = new ArrayList();

                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);

                //取得所有有庫存的儲位 Smose Liao 20091118
                dtData = GetAllLocatData_DateCode(strWerks, strLgort, "", "1", strCtbto, strRegon);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    aryAllStorageLocat.Add(dtData.Rows[i]["LOCAT"].ToString());
                }

                for (int i = 0; i < aryMatnr.Count; i++)
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    objWhitm.ResetField();

                    //找出所有具有相同料號的儲位
                    //strSQL = "Select distinct LOCAT from WHITM where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and MATNR = '" + aryMatnr[i] + "'";
                    alColumns.Add("LOCAT");
                    alConditions.Add(" MANDT='" + MANDT + "'");
                    alConditions.Add(" COMCD='" + COMCD + "'");
                    alConditions.Add(" WERKS='" + strWerks + "'");
                    alConditions.Add(" LGORT='" + strLgort + "'");
                    alConditions.Add(" MATNR='" + aryMatnr[i] + "'");

                    dt1Data = objWhitm.EntityQuery(alColumns, alConditions, true);

                    if (dt1Data.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1Data.Rows.Count; j++)
                        {
                            aryStorageLocat.Add(dt1Data.Rows[j]["LOCAT"]);
                        }
                    }
                }

                try
                {
                    //移除掉具有相同料號的儲位
                    for (int i = 0; i < aryStorageLocat.Count; i++)
                    {
                        aryAllStorageLocat.Remove(aryStorageLocat[i]);
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

                return aryAllStorageLocat;
            }
            #endregion

            #region (Picasso專案)倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            //====================================================================================
            ////////////Summary by Smose Liao 20090615////////////////////////////////////////////
            /// <summary>
            /// 倉別下的儲位資訊strType: 0 (空儲位), 1(有庫存), 2(所有儲位),3(未維護位置的儲位)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strType">查詢類型。</param>
            /// <param name="strCtbto">CTO/BTO。</param>
            /// <param name="strRegon">REGION。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData_Picasso(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllLocatData_Picasso(string strWerks, string strLgort, string strLocat, string strType, string strCtbto, string strRegon)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData_Picasso";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strLocat + "," + strType + "," + strCtbto + "," + strRegon + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                //string strSQL = "Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO),''), " +
                //    " REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON),'')" +
                //    "  from WHHED where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("Select *,CTBTONM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'CTBTO' AND CTRLNM = WHHED.CTBTO COLLATE database_default),''),  REGONNM = ISNULL((SELECT CTRLC1 FROM WHCTRL WHERE CTRLID = 'REGON' AND CTRLNM = WHHED.REGON COLLATE database_default),'') from WHHED WITH(NOLOCK) where ");
                sbSql.AppendFormat("MANDT='{0}' ", MANDT);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat("AND LGORT='{0}' ", strLgort);

                if (strLocat != "")
                {
                    //strSQL += " and LOCAT ='" + strLocat + "'";
                    sbSql.AppendFormat("AND LOCAT='{0}' ", strLocat);
                }

                if (strType == "0") //空儲位
                {
                    //strSQL += " and LOSTS='0' ";
                    sbSql.AppendFormat("AND LOSTS='0' ", "");
                }
                else if (strType == "1") //有庫存儲位
                {
                    //strSQL += " and LOSTS='1' ";
                    sbSql.AppendFormat("AND LOSTS='1' ", "");
                }
                else if (strType == "2") //全部儲位
                {
                    //strSQL += "";
                    sbSql.AppendFormat("", "");
                }
                else if (strType == "3") //尚未設定位置儲位
                {
                    //strSQL += " and STLEN is null and STWID is null ";
                    sbSql.AppendFormat(" and STLEN is null and STWID is null ", "");
                }

                if (strCtbto != "")
                {
                    //strSQL += " and CTBTO ='" + strCtbto + "'";
                    sbSql.AppendFormat("AND CTBTO='{0}' ", strCtbto);
                }

                if (strRegon != "")
                {
                    //strSQL += " and REGON ='" + strRegon + "'";
                    sbSql.AppendFormat("AND REGON='{0}' ", strRegon);
                }


                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
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

            #region 抓取RefID的所有資料(GetRefIDData)
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 抓取RefID的所有資料
            /// </summary> 
            /// <param name="strMandt">Client。</param>
            /// <param name="strComcd">Company Code。</param>
            /// <param name="strWerks">厰區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strRefid">Reference ID。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetRefIDData(string strMandt, string strComcd, string strWerks, string strLgort, string strRefid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetRefIDData";
                this.ControlMethodParm = "('" + strMandt + "','" + strWerks + "','" + strLgort + "','" + strRefid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select * from WHRID where  ");
                    sbSql.AppendFormat("MANDT='{0}' ", strMandt);
                    sbSql.AppendFormat("and COMCD='{0}' ", strComcd);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and REFID='{0}' ", strRefid);

                    DataTable dtData = new DataTable();
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

            #region 抓取所有未扣帳的RefID的資料(GetAllRefIDData)
            //=========================================================================
            ////////////Summary by Smose Liao////////////////////////////////////////////
            /// <summary>
            /// 抓取所有未扣帳的RefID的資料
            /// </summary> 
            /// <param name="strMandt">Client。</param>
            /// <param name="strComcd">Company Code。</param>
            /// <param name="strWerks">厰區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strCrdat">建立日期。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllRefIDData(string strMandt, string strComcd, string strWerks, string strLgort, string strCrdat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllRefIDData";
                this.ControlMethodParm = "('" + strMandt + "','" + strWerks + "','" + strLgort + "','" + strCrdat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select DISTINCT WERKS, LGORT, REFID, CRDAT, REMAK1 from WHDWN with (nolock) where  ");
                    sbSql.AppendFormat("MANDT='{0}' ", strMandt);
                    sbSql.AppendFormat("and COMCD='{0}' ", strComcd);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and MTYPE='QMS_M' ");
                    sbSql.AppendFormat("and OTQTY= 0 ");
                    sbSql.AppendFormat("and CRDAT between '" + strCrdat + " 00:00:00.000' and '" + strCrdat + " 23:59:59.999' ");

                    DataTable dtData = new DataTable();
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

            #region GetStoragebyPatnum() 抓storage data  group by patnum
            ////////////Summary by Rock tzeng////////////////////////////////////////////
            /// <summary>
            /// 抓storage data  group by patnum
            /// </summary> 
            /// <param name="strMandt">Client。</param>
            /// <param name="strWerks">Company Code。</param>
            /// <param name="strWerks">厰區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strRefid">Reference ID。</param>
            /// <returns>
            /// bool 。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetStoragebyPatnum(string strMandt, string strComcd, string strWerks, string strLgort, string strRefid)
            {

                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetStoragebyPatnum";
                this.ControlMethodParm = "('" + strMandt + "','" + strWerks + "','" + strLgort + "','" + strRefid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("SELECT UPPER(OTWRK) OTWRK, UPPER(OTLGT) OTLGT,   ");
                    sbSql.AppendFormat(" UPPER(MATNR) MATNR,SUM(MENGE)AS MENGE FROM WHRID WHERE ");
                    sbSql.AppendFormat("MANDT='{0}' ", strMandt);
                    sbSql.AppendFormat("and COMCD='{0}' ", strComcd);
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks);
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort);
                    sbSql.AppendFormat("and REFID='{0}' ", strRefid);
                    sbSql.AppendFormat("GROUP BY MATNR,OTWRK,OTLGT ");

                    DataTable dtData = new DataTable();
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

            #region 查詢該倉別是否有預設的庫別
            //===========================================================================
            ////////////Summary by Smose Liao 20110316///////////////////////////////////
            /// <summary>
            /// 查詢該倉別是否有預設的庫別
            /// </summary> 
            /// <param name="strLgort">倉別。</param> 
            /// <returns>
            /// String。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public String GetDefaultInsmk(string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDefaultInsmk";
                this.ControlMethodParm = "('" + strLgort + "')";
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
                    string strInsmk;

                    alColumns.Add("CTRLC1");
                    alQueryCondition.Add("MANDT = 'QCI'");
                    alQueryCondition.Add("SOLDTO = 'QWMS'");
                    alQueryCondition.Add("CTRLID = 'DEFAULT_INSMK'");
                    alQueryCondition.Add("CTRLNM = '" + strLgort + "'");

                    dtData = objWhctrl.EntityQuery(alColumns, alQueryCondition, false, true);

                    if (dtData.Rows.Count > 0)
                    {
                        strInsmk = dtData.Rows[0]["CTRLC1"].ToString();
                    }
                    else
                    {
                        strInsmk = "";
                    }

                    return strInsmk;
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

            #region GetDIDInfo() 抓取DIDNO的資料
            ////////////Summary by Rock Tzeng ////////////////////////////////////////////
            /// <summary>
            /// 抓取DIDNO的資料
            /// </summary> 
            /// <param name="strMandt">Client</param>
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strRefid">REFID</param>
            /// <param name="strDidno">DIDNO</param>
            /// <returns>
            /// bool 。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDIDInfo(string strMandt, string strComcd, string strWerks, string strLgort, string strRefid, string strDidno)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDIDInfo";
                this.ControlMethodParm = "('" + strMandt + "','" + strComcd + "','" + strWerks + "','" + strLgort + "','" + strRefid + "','" + strDidno + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("Select R.MANDT,R.COMCD,R.MATNR,R.LIFNR,R.CHARG,R.INSMK,R.WERKS,R.OTLGT,R.SERNO,R.DACOD,R.LOCOD,R.MENGE,R.OTQTY,R.REFID,(CASE WHEN ISNULL(Q.EXPDAT_AFTER,'')='' THEN '' ELSE Q.EXPDAT_AFTER END) AS EXPDAT, R.TASKID ,(CASE WHEN ISNULL(Q.MAXEXP,'')='' THEN '' ELSE (CASE WHEN ISNULL(Q.MAXEXP_AFTER,'')='' THEN Q.MAXEXP ELSE Q.MAXEXP_AFTER END) END) AS MAXEXP from WHRID AS R WITH(NOLOCK) LEFT JOIN WHIQC AS Q WITH(NOLOCK) ON R.TASKID=Q.TASKID AND R.EXPDAT=Q.EXPDAT_AFTER where    ");
                    // MANDT,COMCD,MATNR,LIFNR,CHARG,INSMK,WERKS,OTLGT,SERNO,DACOD,LOCOD,MENGE,REFID
                    sbSql.AppendFormat("R.MANDT='{0}' ", strMandt.Trim());
                    sbSql.AppendFormat("and R.COMCD='{0}' ", strComcd.Trim());
                    sbSql.AppendFormat("and R.WERKS='{0}' ", strWerks.Trim());
                    sbSql.AppendFormat("and R.LGORT='{0}' ", strLgort.Trim());
                    sbSql.AppendFormat("and R.DIDNO='{0}' ", strDidno.Trim());

                    DataTable dtData = new DataTable();
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

            #region GetSAPDataByRefID() By Referance抓取欲扣SAP帳的資訊
            ////////////Summary by Rock Tzeng///////////////////////////////////////////
            /// <summary>
            /// By Referance抓取欲扣SAP帳的資訊 
            /// </summary> 
            /// <param name="strMandt">Client。</param>
            /// <param name="strComcd">Company Code。</param>
            /// <param name="strWerks">厰區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strRefID">Reference ID。</param>
            /// <returns>
            /// DataTable 。
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetSAPDataByRefID(string strMandt, string strComcd, string strWerks, string strLgort, string strRefID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetSAPDataByRefID";
                this.ControlMethodParm = "('" + strMandt + "','" + strComcd + "','" + strWerks + "','" + strLgort + "','" + strRefID + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = " SELECT REFID, MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, SUM(MENGE) AS MENGE, SUM(OTQTY) AS OTQTY, OTWRK, OTLGT, CRDAT, MODAT, OMBLN, KOSTL,COMCD " +
                    //          " FROM WHRID " +
                    //          " Where MANDT='" + strMandt + "' and COMCD='"+strComcd+"' and WERKS='" + strWerks + "' and LGORT='" + strLgort + "' and REFID='" + strRefID + "' " +
                    //          " GROUP BY REFID, MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, OTWRK, OTLGT, CRDAT, MODAT, OMBLN, KOSTL,COMCD "

                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("SELECT REFID, DIDNO, MANDT,WERKS, LGORT, MATNR, INSMK, CHARG, SUM(MENGE) AS MENGE, SUM(OTQTY) AS OTQTY, OTWRK, OTLGT, CRDAT, MODAT, OMBLN, KOSTL,COMCD  ");
                    sbSql.AppendFormat("FROM WHRID Where ");
                    sbSql.AppendFormat("MANDT='{0}' ", strMandt.Trim());
                    sbSql.AppendFormat("and COMCD='{0}' ", strComcd.Trim());
                    sbSql.AppendFormat("and WERKS='{0}' ", strWerks.Trim());
                    sbSql.AppendFormat("and LGORT='{0}' ", strLgort.Trim());
                    sbSql.AppendFormat("and REFID IN ('{0}') ", strRefID.Trim());
                    sbSql.AppendFormat("GROUP BY REFID, DIDNO, MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, OTWRK, OTLGT, CRDAT, MODAT, OMBLN, KOSTL,COMCD ");

                    DataTable dtData = new DataTable();
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

                alColumns.Add("CTRLNM AS F_VALUE");
                alColumns.Add("CTRLC5 AS F_TEXT");
                alColumns.Add("CTRLN1");

                alQueryCondition.Add("MANDT = 'QCI'");
                alQueryCondition.Add("SOLDTO = 'QWMS'");
                alQueryCondition.Add("CTRLID = 'CGCLS'");

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
                    alQueryCondition.Add("MATNR in (Select MATNR from WHITM where MANDT='" + this.MANDT + "' and WERKS='" + strWerks.Trim() + "' and LGORT='" + strLgort.Trim() + "')");
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

            #region 根据仓别得到Refid,Add by Bruce Zhang
            /// <summary>
            /// 根据仓别得到Refid
            /// </summary> 
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetAllLocatData(strWerks,strLgort,strLocat,strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public DataTable GetAllRefID(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllRefID";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "Select distinct refid from WHRID where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "'";

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendFormat(" select distinct refid from WHRID where MANDT= '{0}' AND COMCD='{1}'", MANDT, COMCD)
                         .AppendFormat("  and WERKS= '{0}' and LGORT = '{1}'", strWerks, strLgort);

                    DataTable dtData = new DataTable();

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

            #region 取得Refid资料, Add by Bruce Zhang
            /// <summary>
            /// 取得Refid资料
            /// </summary> 
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strRefid">Reference id。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetRefidData(strWerks, strLgort, string strRefid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetRefidData(string strWerks, string strLgort, string strRefid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetRefidData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strRefid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "select * from WHRID where MANDT= '" + MANDT + "' and WERKS= '" + strWerks + "' and LGORT = '" + strLgort + "' and REFID = '" + strRefid + "' ";

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendFormat(" select * from WHRID where MANDT= '{0}' AND COMCD='{1}'", MANDT, COMCD)
                         .AppendFormat("  and WERKS= '{0}' and LGORT = '{1}' and REFID = '{2}'", strWerks, strLgort, strRefid);

                    DataTable dtData = new DataTable();
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

            #region 取得扣帐编号(Document No)资料, Add by Bruce Zhang
            /// <summary>
            /// 取得扣帐编号(Document No)资料
            /// </summary> 
            /// <param name="strWerks">厂区。</param>
            /// <param name="strLgort">仓别。</param>
            /// <param name="strMblnr">扣帐编号。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDocNoData(strWerks, strLgort, string strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDocNoData(string strWerks, string strLgort, string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDocNoData";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "select * from WHDWN where MANDT = '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT = '" + strLgort + "' and MBLNR like '" + strMblnr + "' ";

                    strMblnr = strMblnr + '%';

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendFormat(" select * from WHDWN with (nolock) where MANDT= '{0}' AND COMCD='{1}'", MANDT, COMCD)
                         .AppendFormat("  and WERKS= '{0}' and LGORT = '{1}' and MBLNR like'{2}' ", strWerks, strLgort, strMblnr);

                    DataTable dtData = new DataTable();

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

            #region 取得Refid 依据(MATNR)作加总后的数量, Add by Bruce Zhang
            /// <summary>
            /// 取得Refid 依据(MATNR)作加总后的数量
            /// </summary> 
            /// <param name="strWerks">厂区。</param>
            /// <param name="strLgort">仓别。</param>
            /// <param name="strRefid">Reference id。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetRefidQty(strWerks, strLgort, string strRefid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetRefidQty(string strWerks, string strLgort, string strRefid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetRefidQty";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strRefid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "SELECT MATNR from WHRID where MANDT = '" + MANDT + "' and WERKS ='" + strWerks + "' AND LGORT =  '" + strLgort + "' and REFID= '" + strRefid + "' group by MATNR";

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendFormat(" SELECT MATNR from WHRID where MANDT= '{0}' AND COMCD='{1}'", MANDT, COMCD)
                         .AppendFormat("  and WERKS= '{0}' and LGORT = '{1}' and REFID = '{2}'", strWerks, strLgort, strRefid)
                         .AppendFormat("  group by MATNR");
                    DataTable dtData = new DataTable();
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

            #region 取得SAP扣帐编号(Document No.)依据料号(MATNR)作加总后的数量, Add by Bruce Zhang
            /// <summary>
            /// 取得SAP扣帐编号(Document No.)依据料号(MATNR)作加总后的数量
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strMblnr">扣帳編號。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.GetDocNoQty(strWerks, strLgort, string strMblnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetDocNoQty(string strWerks, string strLgort, string strMblnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDocNoQty";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strMblnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    //string strSQL = "";
                    //strSQL = "SELECT MATNR from WHDWN where MANDT = '" + MANDT + "' and WERKS ='" + strWerks + "' AND LGORT =  '" + strLgort + "' and MBLNR like '" + strMblnr + "' group by MATNR";

                    strMblnr = strMblnr + '%';

                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendFormat(" SELECT MATNR from WHDWN with (nolock) where MANDT= '{0}' AND COMCD='{1}'", MANDT, COMCD)
                         .AppendFormat("  and WERKS= '{0}' and LGORT = '{1}' and MBLNR like'{2}' ", strWerks, strLgort, strMblnr)
                         .AppendFormat("  group by MATNR");

                    DataTable dtData = new DataTable();

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

            #region  設定CTO/BTO
            //====================================================================================
            ////////////Summary by Smose Liao 20091026////////////////////////////////////////////
            /// <summary>
            /// 設定CTO/BTO
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.SetCTBTO();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////	
            public DataTable SetCTBTO()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "SetCTBTO";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT * FROM WHCTRL WHERE CTRLID='CTBTO'");

                DataTable dtData = new DataTable();
                try
                {
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

            #region  設定Region
            //====================================================================================
            ////////////Summary by Smose Liao 20091026////////////////////////////////////////////
            /// <summary>
            /// 設定CTO/BTO
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.SetRegion();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////	
            public DataTable SetRegion()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "SetRegion";
                this.ControlMethodParm = "(' ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT * FROM WHCTRL WHERE CTRLID='REGON'");

                DataTable dtData = new DataTable();
                try
                {
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

            #region  設定機種
            //====================================================================================
            ////////////Summary by Smose Liao 20091026////////////////////////////////////////////
            /// <summary>
            /// 設定機種
            /// </summary> 
            /// <returns>
            /// String。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.SetMapid(string strMandt, string strStdMatnr);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////	
            public string GetMapid(string strMandt, string strStdMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "SetMapid";
                this.ControlMethodParm = "(" + strMandt + "," + strStdMatnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("SELECT MAPID FROM WHMAT WHERE ");
                sbSql.AppendFormat("MANDT='{0}' ", strMandt.Trim());
                sbSql.AppendFormat("AND MATNR='{0}' ", strStdMatnr);
                sbSql.AppendFormat("AND COMCD='{0}' ", COMCD);
                sbSql.AppendFormat("AND SOLDTO ='QWMS' ", "");

                String strMapid = "";
                try
                {
                    ControlHandleDB();
                    strMapid = ControlSqlAccess.GetFieldValue(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return strMapid;
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

            #region 查询储位的Block状态
            ////////////Summary by Bruce Zhang 20091028////////////////////////////////////////////
            /// <summary>
            ///  查询储位的Block状态
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strStatus">狀態。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  bool  bolReturn = objPlantData.QueryLocationStatus ();
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryLocationStatus(string strWerks, string strLgort, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLocationStatus";
                this.ControlMethodParm = "(" + strMandt + "," + strLgort + "," + strStatus + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }


                try
                {
                    //string strSQL = "Select * from WHHED where MANDT='" + MANDT + "' and WERKS= '" + strWerks + "' and LGORT= '" + strLgort + "' and BLOCK= '" + strStatus + "'";
                    DataTable dtData = new DataTable();

                    DataWhhed objDataWhhed = new DataWhhed(UserData);
                    ArrayList alColumns = new ArrayList();
                    ArrayList alCondition = new ArrayList();
                    alColumns.Add("*");

                    alCondition.Clear();
                    alCondition.Add("MANDT='" + MANDT + "'");
                    alCondition.Add("COMCD='" + COMCD + "'");
                    alCondition.Add("WERKS= '" + strWerks + "'");
                    alCondition.Add("LGORT= '" + strLgort + "'");
                    alCondition.Add("BLOCK= '" + strStatus + "'");
                    dtData = objDataWhhed.EntityQuery(alColumns, alCondition, false, true);
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

            #region 查詢儲位存放的料號
            //=================================================================================================================
            ////////////Summary by Smose Liao 20091105/////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢儲位存放的料號
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strLocat">儲位。</param>
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strIsmrg">是否連板。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.QueryPartData(strWerks,strLgort,strLocat,strInsmk,strIsmrg);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable QueryPartData(string strWerks, string strLgort, string strLocat, string strInsmk, string strIsmrg)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryPartData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strLocat + "," + strInsmk + "," + strIsmrg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);

                //string strSQL = "Select distinct MATNR from WHITM where MANDT= '" + MANDT + "' and WERKS = '" + strWerks + "' and LGORT= '" + strLgort + "' and LOCAT='" + strLocat + "'";
                alColumns.Add("MATNR");
                alConditions.Add(" MANDT='" + MANDT + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks + "'");
                alConditions.Add(" LGORT='" + strLgort + "'");

                if (strInsmk != "")
                {
                    //strSQL += " and INSMK='" + strInsmk + "'";
                    alConditions.Add(" INSMK='" + strInsmk + "'");
                }

                if (strIsmrg != "")
                {
                    if (strIsmrg == "Y")
                    {
                        //strSQL += " and MRGID <> ''";
                        alConditions.Add(" MRGID<>''");
                    }
                    else
                    {
                        //strSQL += " and MRGID = ''";
                        alConditions.Add(" MRGID = ''");
                    }
                }

                try
                {
                    dtData = objWhitm.EntityQuery(alColumns, alConditions, true);
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

            #region  查詢廠區, 倉別及儲位是否存在(新增前使用)
            //==================================================================================================
            ////////////Summary by Smose Liao 20091116//////////////////////////////////////////////////////////
            /// <summary>
            /// 查詢廠區, 倉別及儲位是否存在(新增前使用)
            /// </summary> 
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <returns>
            /// bool。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtData = objPlantData.CheckExistedStorage(strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////////	
            public bool CheckExistedStorage(string strWerks, string strLgort)
            {
                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhhed objWhhed = new DataWhhed(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks + "'");
                alConditions.Add(" LGORT='" + strLgort + "'");

                try
                {
                    dtData = objWhhed.EntityQuery(alColumns, alConditions, false);
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

                if (dtData.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            #endregion

            #region 抓取相同料號、相同版本的庫存資料
            //===========================================================================================================
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 抓取相同料號、相同版本的庫存資料
            /// </summary> 
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strMatnr">料號</param>
            /// <param name="strCharg">版本</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.GetPALInfo(strMandt, strWerks, strLgort, strRefid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetPalletIDInventoryData(string strWerks, string strLgort, string strMatnr, string strCharg)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPalletIDInventoryData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strMatnr + "," + strCharg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr.Trim() + "'");
                }
                if (strCharg != "")
                {
                    alConditions.Add(" CHARG='" + strCharg.Trim() + "'");
                }

                try
                {
                    dtData = objWhitm.EntityQuery(alColumns, alConditions, false);
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

            #region 抓取相同料號、相同版本的庫存資料
            //============================================================================================================================
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 抓取相同料號、相同客人料號、相同版本的庫存資料
            /// </summary> 
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strMatnr">料號</param>
            /// <param name="strCharg">版本</param>
            /// <param name="strKdmat">客人料號</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.GetPALInfo(strMandt, strWerks, strLgort, strRefid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetPalletIDInventoryData(string strWerks, string strLgort, string strMatnr, string strCharg, string strKdmat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPalletIDInventoryData";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strMatnr + "," + strCharg + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhitm objWhitm = new DataWhitm(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                if (strMatnr != "")
                {
                    alConditions.Add(" MATNR='" + strMatnr.Trim() + "'");
                }
                if (strCharg != "")
                {
                    alConditions.Add(" CHARG='" + strCharg.Trim() + "'");
                }
                if (strKdmat != "")
                {
                    alConditions.Add(" KDMAT='" + strKdmat.Trim() + "'");
                }

                try
                {
                    dtData = objWhitm.EntityQuery(alColumns, alConditions, false);
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

            #region 取得料號儲位的滿板數量與庫存數量資料
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 取得料號儲位的滿板數量與庫存數量資料
            /// </summary> 
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            /// TWQ.QWMS.Setting objData =new TWQ.QWMS.Setting(UserData, Progid);
            /// DataTable dtData = objOutbound.GetPartNoLocationInventoryData("ZYEZZYE");
            /// Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable GetPartNoLocationInventoryData(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPartNoLocationInventoryData";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    #region 變數宣告
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    #endregion
                    #region 查詢欄位
                    sbSql.Append("SELECT I.MENGE,H.PALQTY, (H.PALQTY - I.MENGE) AS BALANCE,* ");
                    sbSql.Append("from WHHED H WITH (NOLOCK) inner join WHITM I ");
                    sbSql.Append("on H.WERKS = I.WERKS AND H.LGORT = I.LGORT AND H.LOCAT = I.LOCAT ");
                    #endregion
                    #region 查詢條件
                    sbSql.Append("where 1=1 ");
                    sbSql.Append("and I.Werks ='" + strWerks.Trim() + "'");
                    sbSql.Append("and I.Lgort ='" + strLgort.Trim() + "'");
                    //if (!string.IsNullOrEmpty(strLocat))
                    //{
                    //    sbSql.Append(" and I.Locat ='" + strLocat.Trim() + "'");
                    //}
                    //if (!string.IsNullOrEmpty(strMatnr))
                    //{
                    //    sbSql.Append(" and I.Matnr ='" + strMatnr.Trim() + "'");
                    //}
                    //if (!string.IsNullOrEmpty(strCharg))
                    //{
                    //    sbSql.Append(" and I.Charg ='" + strCharg.Trim() + "'");
                    //}

                    //0:平面儲位 1:料架儲位
                    sbSql.Append(" and H.PNLOC ='1' ");
                    //良品(G)
                    sbSql.Append(" and I.INSMK ='G' ");
                    sbSql.Append(" and I.MENGE > 1 ");

                    #endregion
                    #region 執行SQL
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    #endregion
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

            #region Add By Michael 20150615 for 新增版本检测
            # region 获取最新版
            public string GetVersion()
            {
                try
                {
                    ControlHandleDB();
                    string NewVersion = ControlSqlAccess.GetFieldValue("SELECT CTRLNM FROM WHCTRL WHERE MANDT='QCI' AND SOLDTO='QWMS' AND CTRLID = 'VERSION' AND CTRLC1 = 'Y' ");
                    ControlSqlAccess.CloseConnection();
                    return NewVersion;
                }
                catch (CommonObjectsException ex)
                {
                    //�讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
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

            #region 记录未更新版本的信息
            public void AddPCInformation(string IP, string UserNM, string OldVersion, int Num, string strConn)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UserVersionControl";
                    cmd.CommandType = CommandType.StoredProcedure;
                    #region 参数
                    cmd.Parameters.Add("@MANDT", SqlDbType.VarChar);
                    cmd.Parameters["@MANDT"].Value = MANDT;
                    cmd.Parameters.Add("@COMCD", SqlDbType.VarChar);
                    cmd.Parameters["@COMCD"].Value = COMCD;
                    cmd.Parameters.Add("@IP", SqlDbType.VarChar);
                    cmd.Parameters["@IP"].Value = IP;
                    cmd.Parameters.Add("@UserNM", SqlDbType.VarChar);
                    cmd.Parameters["@UserNM"].Value = UserNM;
                    cmd.Parameters.Add("@OldVersion", SqlDbType.VarChar);
                    cmd.Parameters["@OldVersion"].Value = OldVersion;
                    cmd.Parameters.Add("@Num", SqlDbType.Int);
                    cmd.Parameters["@Num"].Value = Num;
                    #endregion

                    //UserVersionControl '218','9200','1.1.1.1','ADMIN','1234','111'


                    cmd.ExecuteNonQuery();
                    conn.Close();

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

            #region 更新UserVersion
            public bool UpdateUserVersion(string MANDT, string COMCD, string IP, string NewVersion)
            {
                try
                {
                    ControlHandleDB();
                    string strSQL = string.Format(@"IF (SELECT COUNT(1) FROM UserVersion WITH(NOLOCK) WHERE MANDT ='{0}' AND COMCD = '{1}' AND IP = '{2}' AND NEWVERSION = '{3}' AND FLAG = 'N') > 0
                                                    BEGIN
	                                                    UPDATE UserVersion SET FLAG = 'Y', OLDVERSION = '{3}'
	                                                    WHERE MANDT ='{0}' AND COMCD = '{1}' AND IP = '{2}' AND NEWVERSION = '{3}'
                                                    END", MANDT, COMCD, IP, NewVersion);
                    bool IsSucess = ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return IsSucess;
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

            #region Add By Michael 20151105 for SMT祥龙退料优化
            //SMT祥龙退料优化
            public DataTable GetAllLocatData_New(string strWerks, string strLgort, string strLocat, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetAllLocatData_New";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                try
                {
                    StringBuilder strWhereSQL = new StringBuilder();
                    if (strLocat != "")
                    {
                        strWhereSQL.Append(" AND LOCAT='" + strLocat + "'");
                    }

                    if (strType == "0") //空儲位
                    {
                        strWhereSQL.Append(" AND LOSTS='0'");
                    }
                    else if (strType == "1") //有庫存儲位
                    {
                        strWhereSQL.Append(" AND LOSTS='1'");
                    }
                    else if (strType == "2") //全部儲位
                    {
                        //
                    }
                    else if (strType == "3") //尚未設定位置儲位
                    {
                        strWhereSQL.Append(" AND (STLEN is null and STWID is null) ");
                    }


                    string strSQL = string.Format(@"SELECT SUM(ISNULL(I.TOTAL,0)) AS TOTAL, H.LOCAT FROM WHHED H WITH(NOLOCK) 
                                                      LEFT JOIN (
		                                                         SELECT COUNT(1) AS TOTAL, MANDT, COMCD, WERKS, LGORT, LOCAT, ISNULL(MBLNR,'') AS MBLNR 
			                                                     FROM WHITM WITH(NOLOCK)
			                                                     GROUP BY MANDT, COMCD, WERKS, LGORT, LOCAT, ISNULL(MBLNR,'')
			                                                     ) I ON I.MANDT = H.MANDT AND I.COMCD = H.COMCD
				                                                    AND I.WERKS = H.WERKS AND I.LGORT = H.LGORT AND I.LOCAT = H.LOCAT
                                                    WHERE H.MANDT = '{0}' AND H.COMCD = '{1}' AND H.WERKS = '{2}' AND H.LGORT = '{3}'
                                                      AND BLOCK = '0' {4}
                                                    GROUP BY H.LOCAT", MANDT, COMCD, strWerks, strLgort, strWhereSQL.ToString());

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
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

            #region Add By Michael 20151105 for 获取Film材料Mail人员
            //获取Film材料Mail人员
            public string GetMailConfig_Film()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMailConfig_Film";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strMailTo = new StringBuilder();
                DataTable dtData = new DataTable();
                try
                {
                    StringBuilder strWhereSQL = new StringBuilder();
                    string strSQL = string.Format(@"SELECT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT = '{0}' AND COMCD = '{1}' AND SOLDTO = 'QWMS' AND CTRLID = 'Film_Mail' ", MANDT, COMCD);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtData.Rows)
                        {
                            if (string.IsNullOrEmpty(strMailTo.ToString()))
                                strMailTo.Append(dr["CTRLC1"].ToString());
                            else
                                strMailTo.Append(";" + dr["CTRLC1"].ToString());
                        }
                    }
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

                return strMailTo.ToString();
            }
            #endregion

            #region 检查仓别是否存在
            /// <summary>
            /// 检查仓别是否存在
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="usrnm">用户名</param>
            /// <returns></returns>
            public DataTable CheckLgort(string strWerks, string strLgort, string usrnm)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckLgort";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + usrnm + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();

                string strSql = "";
                strSql = "SELECT * FROM WHCTRL with(nolock) WHERE CTRLNM='" + strWerks + "' AND CTRLC1='" + strLgort + "' AND MANDT=( SELECT TOP 1 MANDT FROM WHAUT  WHERE USRNM='" + usrnm + "' )";

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSql);
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

            #region 抓取BOXID的資料
            //============================================================================================
            /// <summary>
            /// 抓取BOXID的資料
            /// </summary> 
            /// <param name="strMandt">Client</param>
            /// <param name="strWerks">厰區</param>
            /// <param name="strLgort">倉別</param>
            /// <param name="strPalid">BOXID</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.PlantData objPlantData =new QCI.QWMS.PlantData(strConnectionString,strMandt,strCrnam);
            ///  DataTable dtReturn = objPlantData.GetBOXIDInfo(strMandt,strWerks, strLgort,strPalid);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ///////////////////////////////////////////////////////////////////////////////////////////////	
            public DataTable GetBOXIDInfo(string strMandt, string strWerks, string strLgort, string strBoxid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPALInfo";
                this.ControlMethodParm = "(" + strWerks + "," + strLgort + "," + strBoxid + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                DataWhdwn objWhdwn = new DataWhdwn(UserData);

                alColumns.Add("*");
                alConditions.Add(" MANDT='" + strMandt.Trim() + "'");
                alConditions.Add(" COMCD='" + COMCD + "'");
                alConditions.Add(" WERKS='" + strWerks.Trim() + "'");
                alConditions.Add(" LGORT='" + strLgort.Trim() + "'");
                alConditions.Add(" BOXID='" + strBoxid.Trim() + "'");
                alConditions.Add(" (MTYPE='QMS_M' or MTYPE='QMS') ");

                try
                {
                    dtData = objWhdwn.EntityQuery(alColumns, alConditions, false);
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

            public bool CheckAuthority(string PROGID)
            {
                string strauth = objAuthority.INAUT + ";" + objAuthority.CGAUT + ";" +
                                 objAuthority.IVAUT + ";" + objAuthority.MAAUT + ";" + objAuthority.MGAUT + ";"
                                 + objAuthority.OTAUT + ";" + objAuthority.REPLN + ";";
                string[] strMenu_ = new string[1000];
                strMenu_ = strauth.Split(';');
                
                if (strMenu_.Contains(PROGID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //if (strauth.IndexOf(PROGID + ";") < 0)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
            }


            #region 判断是否为ASRS仓别
            public bool CheckLGORT(string varPlant, string varLgort)
            {
                bool bolResult = false;
                string sbSql = " SELECT CTRLC4 FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS'AND CTRLID='LGORT'AND CTRLNM='" + varPlant + "'AND CTRLC1='" + varLgort + "'";

                ControlHandleDB();
                string strType = ControlSqlAccess.GetFieldValue(sbSql);
                if (strType == "ASRS")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion


            #region 判断是否为DateCode管控仓别
            public bool CheckDACODLGORT(string varPlant, string varLgort)
            {
                bool bolResult = false;
                string sbSql = " SELECT REMAK FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS'AND CTRLID='StorageIn_Type' AND REMAK='Diff DACOD Diff Locat' AND CTRLNM='" + varPlant + "'AND CTRLC1='" + varLgort + "'";

                ControlHandleDB();
                string strType = ControlSqlAccess.GetFieldValue(sbSql);
                if (strType == "Diff DACOD Diff Locat")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion

            #region 判断是否为Lot Code管控仓别
            public bool CheckLOCODLGORT(string varPlant, string varLgort)
            {
                bool bolResult = false;
                string sbSql = " SELECT REMAK FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS'AND CTRLID='StorageIn_Type' AND REMAK='Diff LOCOD Diff Locat' AND CTRLNM='" + varPlant + "'AND CTRLC1='" + varLgort + "'";

                ControlHandleDB();
                string strType = ControlSqlAccess.GetFieldValue(sbSql);
                if (strType == "Diff LOCOD Diff Locat")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion

            #region SMT退料发送邮件
            public bool CheckSMTSendMail(string varPlant)
            {
                DataTable dtData = new DataTable();
                string sbSql = " SELECT  * FROM WHMAL WITH(NOLOCK)  WHERE MANDT='218' AND FUNCT='SMTtoSAP' AND WERKS='" + varPlant + "'";
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql);
                if (dtData.Rows.Count > 0)
                {
                    return  true;
                }
                else
                {
                    return false;
                }
            }
            #endregion

            #region 判断是否为Batch管控仓别
            public bool CheckCHARGLGORT(string varPlant)
            {
                bool bolResult = false;
                string sbSql = " SELECT REMAK FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS'AND CTRLID='StorageIn_Type' AND REMAK='Diff CHARG Diff Locat' AND CTRLNM='" + varPlant + "'";

                ControlHandleDB();
                string strType = ControlSqlAccess.GetFieldValue(sbSql);
                if (strType == "Diff CHARG Diff Locat")
                {
                    bolResult = true;
                }

                return bolResult;


            }
            #endregion


            #region 查询带权限仓别
            public DataTable CheckStorageWithAuthority()
            {

                DataTable dtResult = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.Append(" SELECT USRNM,WERKS,LGORT FROM WHCTRL C ");
                sbSQL.AppendLine(" INNER JOIN WHAUT A ON  C.CTRLID='LGORT' AND C.COMCD=A.COMCD AND  C.CTRLNM=A.WERKS AND C.CTRLC1=A.LGORT  ");
                sbSQL.AppendLine(" WHERE USRNM='" + UserData.UserId + "' ");
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());

                return dtResult;


            }
            #endregion


            #region 判断IQC扫描单号是否与所选仓一致
            /// <summary>
            /// 判断IQC扫描单号是否与所选仓一致
            /// </summary>
            /// <param name="strMblnr">扫描单号</param>
            /// <param name="strWerks">所选厂区</param>
            /// <param name="strLgort">所选仓别</param>
            /// <returns></returns>
            public bool checkWhticLgort(string strMblnr, string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "checkWhticLgort";
                this.ControlMethodParm = "('" + strMblnr + strWerks + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append(" SELECT MBLNR FROM  WHTIC WITH(NOLOCK) WHERE MTYPE IN('SAP_IQC','OA_IQC') AND OTQTY=0 AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND BOXID='" + strMblnr + "' ");

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                        return false;
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

            #region 抓取达伟外包仓部门-厂区对应关系
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <returns></returns>
            public string GetPlantCostcenter(string strWerks) {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPalletIDInventoryData";
                this.ControlMethodParm = "(" + strWerks + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataWhctrl objWhctrl = new DataWhctrl(this.UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alCondition = new ArrayList();
                alColumns.Add("CTRLC2");
                alCondition.Add(" soldto = 'QWMS'");
                alCondition.Add(" ctrlid = 'KOSTL'");
                alCondition.Add(" CTRLC1 = '" + strWerks + "'");
                try {
                    // 执行查询
                    DataTable data = objWhctrl.EntityQuery(alColumns, alCondition, true);
                    return data.Rows[0][0].ToString();
                } catch (CommonObjectsException ex) {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex) {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    ControlExceptionType = ex.GetType().FullName;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw new Exception("999");
                }
            }
            #endregion 
            #region 抓取达伟外包仓出库数据
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public DataTable GetOutDataFromTWW(string strWerks, string strMblnr) {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPalletIDInventoryData";
                this.ControlMethodParm = "(" + strWerks + "," + strMblnr + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                // 创建datatable 用于返回数据
                DataTable dtData = new DataTable();
                // 创建sql语句
                DataWhtww objWhtww = new DataWhtww(this.UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                // 填入数据
                alColumns.Add("*");
                // 填入条件
                alConditions.Add("WERKS='" + strWerks + "'");
                alConditions.Add("MBLNR='" + strMblnr + "'");
                alConditions.Add("IEFLG = 'E'");
                try {
                    // 执行查询
                    dtData = objWhtww.EntityQuery(alColumns, alConditions, false);
                } catch (CommonObjectsException ex) {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex) {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    ControlExceptionType = ex.GetType().FullName;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw new Exception("999");
                }
                return dtData;
            }
            #endregion

            #endregion

            #region 登录QWMS日志记录
            /// <summary>
            /// 登录QWMS日志记录
            /// </summary>
            /// <param name="strLogType"></param>
            /// <param name="strDate"></param>
            public void LogOnQWMSLog(string strLogType,string strDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "LogOnQWMSLog";
                this.ControlMethodParm = "('" + strLogType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("INSERT INTO [dbo].[log_logOn] ([login_type],[login_name] ,[ipaddress] ,[CDate],[Remark]) VALUES ('{0}','{1}','{2}',GETDATE(),'{3}')", strLogType, UserData.UserId, UserData.ClientIP, strDate);
                    try
                    {

                        ControlHandleDB();
                        ControlSqlAccess.ExecSql(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- LogOnQWMSLog()";
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

            #region 判断用户是否可以使用(实时库存比对页面,DC查询页面)
            public bool GetPermission(string strUsernm,string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetPermission";
                this.ControlMethodParm = "('" + strUsernm + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blResult = false;
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("SELECT COUNT(1) FROM WHUSR WHERE MANDT='{0}' AND COMCD='{1}' AND USRNM='{2}' ", MANDT, COMCD, strUsernm);
                    if (strType.Equals("StockCompare"))  //实时库存比对权限
                        sb.Append("AND ISADM='Y'");
                    if (strType.Equals("DC"))   //DC查询页面，提供给非仓库人员使用，仅有该页面
                        sb.Append("AND ROLCD='DC'");
                    try
                    {
                        SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                         DataTable dt= sqlAccess.GetDataTable(sb.ToString());
                        sqlAccess.CloseConnection();
                        blResult = dt.Rows[0][0].ToString().Equals("0") ? false : true;
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- GetECPermission()";
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
                return blResult;
            }
            #endregion

            #region  判断用户是否3个月没有修改密码
            public bool QueryPasswdDat()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryPasswdDat";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool result = false;
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("SELECT 1 FROM WHUSR WITH (NOLOCK) WHERE USRNM ='{0}' AND PASWDDAT<DATEADD(MONTH,-3,GETDATE())",UserData.UserId);
                    try
                    {

                        ControlHandleDB();
                        DataTable dtresult = ControlSqlAccess.GetDataTable(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                        if (dtresult.Rows.Count > 0)
                        {
                            result = true;
                        }
                        return result;
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- QueryPasswdDat()";
                    }
                    return result;

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

            #region  根据属性获取祥龙DateCode的仓别（整料仓以及散料仓）
            /// <summary>
            /// 根据属性获取祥龙DateCode的仓别（整料仓以及散料仓）
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strProperty">整料/散料</param>
            /// <returns></returns>
            public DataTable QueryLgortByProperty(string strWerks, string strProperty, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLgortByProperty";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtResult = new DataTable();
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("SELECT CTRLC1 FROM WHCTRL WHERE SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLNM='{0}'  ", strWerks);
                    if (!string.IsNullOrEmpty(strProperty))
                        sb.AppendFormat(" AND CTRLC3 ='{0}' ", strProperty);
                    if (!string.IsNullOrEmpty(strLgort))
                        sb.AppendFormat(" AND CTRLC1 ='{0}' ", strLgort);
                    sb.AppendFormat(" AND COMCD='{0}' AND CTRLN3=1", COMCD);
                    try
                    {
                        ControlHandleDB();
                        dtResult = ControlSqlAccess.GetDataTable(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- QueryPasswdDat()";
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
                return dtResult;
            }
            #endregion

            #region 获取登录QWMS祥龙DataCode日志记录
            public DataTable QueryLogOnXLLog(string strLogType, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryLogOnXLLog";
                this.ControlMethodParm = "('" + strLogType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtLog = new DataTable();
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("SELECT login_name,ipaddress,CDate FROM log_logOn WHERE [login_type]='{0}' AND Remark='{1}'", strLogType, strStatus);
                    try
                    {
                        ControlHandleDB();
                        dtLog = ControlSqlAccess.GetDataTable(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- LogOnQWMSLog()";
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
                return dtLog;
            }
            #endregion


            #region 更新登录QWMS祥龙DataCode日志记录
            /// <summary>
            /// 更新登录QWMS祥龙DataCode日志记录
            /// </summary>
            /// <param name="strLogType">登录类型：XL</param>
            /// <param name="strStatus">是否有在使用：0-无，1-有</param>
            /// <returns></returns>
            public bool UpdateLogOnXLLog(string strLogType, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateLogOnXLLog";
                this.ControlMethodParm = "('" + strLogType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool blReult = false;
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("UPDATE log_logOn SET Remark='{0}' WHERE login_name='{1}' AND Remark='1'", strStatus, UserData.UserId);
                    try
                    {
                        ControlHandleDB();
                        blReult = ControlSqlAccess.ExecSql(sb.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        ERRMSG = ex.Message + "<- LogOnQWMSLog()";
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
                return blReult;
            }
            #endregion
        }
    }
}
