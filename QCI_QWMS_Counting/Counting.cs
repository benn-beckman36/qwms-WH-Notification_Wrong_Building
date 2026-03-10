using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using QWMS.Entity;
using System.Text;

namespace QCI
{
    namespace QWMS
    {
        public class Counting : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strCrnam = "";
            private string strProgid = "";
            private string strErrmsg = "";

            #region Constructer

            public Counting()
            {

            }

            public Counting(UserInfo varUserData, string strWerks, string strLgort, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort, strProgid)
            {
            }

            public Counting(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
            }


            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Counting物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objCounting =new QCI.QWMS.Counting(strConnectionString,strMandt,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Counting(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
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
                ControlErrorInfo.ObjectName = "QWMS.Counting";
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


            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Counting物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageData objStorageData =new QCI.QWMS.StorageData(strConnectionString,strMandt,strWerks,strLgort);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public Counting(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strProgid)
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
                ControlErrorInfo.ObjectName = "QWMS.Counting";
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
            /// Company code
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

            # region CheckAuthority
            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 檢查使用者是否有使用盤點功能的權限
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
                this.ControlMethodName = "GetMenuType";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                    if (objAuthority.IVAUT.IndexOf(PROGID) < 0)
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

            # region 庫存明細查詢(盤點使用) by Donald Chen
            //=========================================================================
            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 庫存明細查詢(盤點使用), 如IsMixed為Y, 則只列出連板入庫的資料strInsmk: 庫別strStartLocat, strEndLocat:儲位strStartMatnr, strEndMatnr:料號strStartDate, strEndDate:入庫日期strCharg: 料號版本strMblnr: 單據號碼strIsmrg: 是否連板入庫(Y or N)
            /// </summary> 
            /// <param name="strInsmk">庫別。</param>
            /// <param name="strStartLocat">開始儲位。</param>
            /// <param name="strEndLocat">結束儲位。</param>
            /// <param name="strStartMatnr">開始料號。</param>
            /// <param name="strEndMatnr">結束料號。</param>
            /// <param name="strStartDate">開始入庫日期。</param>
            /// <param name="strEndDate">結束入庫日期。</param>
            /// <param name="strCharg">料號版本。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strIsmrg">是否連板入庫。</param>
            /// <returns>
            /// DataTable。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Counting objCounting =new QCI.QWMS.Counting(strConnectionString,strMandt,strWerks,strLgort);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryDetailCountingData_Resell(string strInsmk, string strStartLocat, string strEndLocat, string strStartMatnr, string strEndMatnr, string strStartDate, string strEndDate, string strCharg, string strMblnr, string strIsmrg, string strIsCombine, string strRegon, string strLifnr)
            {
                string strSQL = "";
                if (strIsCombine == "Y")
                {
                    //20041018 marc 增加客人料號kdmat的欄位(目前設定當Combine時抓的kdmat設定為空值)
                    //20041018 原始程式碼
                    //strSQL = "Select MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE) as MENGE, '' as MBLNR, '' as INDAT from WHITM where MANDT= '" + MANDT + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                    //20041018 修改後的程式
                    strSQL = "Select MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, LIFNR, INSMK, CHARG, sum(MENGE) as MENGE, '' as MBLNR, INDAT, KDMAT , ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX  from WHITM where MANDT= '" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                }
                else
                {
                    strSQL = "Select *, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX from WHITM where MANDT= '" + UserData.Client + "' and COMCD = '" + UserData.CompanyCode + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";

                }
                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";

                }
                if (strCharg != "")
                {
                    strSQL += " and CHARG = '" + strCharg + "'";
                }

                if (strMblnr != "")
                {
                    strSQL += " and MBLNR= '" + strMblnr + "'";

                }
                if (strLifnr != "")
                {
                    strSQL += " and LIFNR= '" + strLifnr + "'";
                }

                if (strStartLocat != "" && strEndLocat != "")
                {
                    strSQL += " and (LOCAT between '" + strStartLocat + "' and '" + strEndLocat + "')";

                }
                if (strRegon != "")
                {
                    strSQL += " and exists (select 'Y' from whhed where regon = '" + strRegon + "' and whhed.mandt = whitm.mandt and whhed.comcd = whitm.comcd and whhed.werks = whitm.werks and whhed.lgort = whitm.lgort and whhed.locat = whitm.locat )";

                }
                else if (strStartLocat == "" && strEndLocat != "")
                {
                    strSQL += " and LOCAT = '" + strEndLocat + "'";
                }
                else if (strStartLocat != "" && strEndLocat == "")
                {
                    strSQL += " and LOCAT = '" + strStartLocat + "'";
                }

                if (strStartMatnr != "" && strEndMatnr != "")
                {
                    strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                }
                else if (strStartMatnr == "" && strEndMatnr != "")
                {
                    strSQL += " and MATNR = '" + strEndMatnr + "'";
                }
                else if (strStartMatnr != "" && strEndMatnr == "")
                {
                    strSQL += " and MATNR = '" + strStartMatnr + "'";
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "  23:59:59')";
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    strSQL += " and CRDAT = '" + strEndDate + "'";
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    strSQL += " and CRDAT = '" + strEndDate + "'";
                }

                if (strIsmrg != "")
                {
                    if (strIsmrg == "Y")
                    {
                        strSQL += " and MRGID <> ''";

                    }
                    else
                    {
                        //列出全部資料
                        strSQL += "";

                    }
                }


                if (strIsCombine == "Y")
                {
                    //20041018 marc 增加客人料號kdmat的欄位(目前設定當Combine時，對應SELECT將kdmat設定為GROUP BY)
                    //20041018 原始程式碼
                    //strSQL += " group by MANDT, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG order by LOCAT, MATNR";
                    //20041018 修改後的程式
                    strSQL += " group by MANDT, COMCD,WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, KDMAT,LIFNR,INDAT order by LOCAT, MATNR";
                }
                else
                {
                    strSQL += " order by LOCAT, MATNR";
                }


                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryDetailCountingData()";

                }
                return dtData;
            }
            # endregion


            # region 获得储位明细盘点资料 Add by Mandy Ma
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 获得储位明细盘点资料
            /// </summary> 
            /// <param name="strInsmk">库别</param>
            /// <param name="strStartLocat">储位起</param>
            /// <param name="strEndLocat">储位止</param>
            /// <param name="strStartMatnr">料号起</param>
            /// <param name="strEndMatnr">料号止</param>
            /// <param name="strStartDate">创建时间起</param>
            /// <param name="strEndDate">创建时间止</param>
            /// <param name="strCharg">料号版本</param>
            /// <param name="strMblnr">扣帐单号</param>
            /// <param name="strIsmrg">是否连板料号</param>
            /// <param name="strIsCombine">是否合并料号</param>
            /// <param name="strRegon">洲别</param>
            /// <param name="strVendorCode">厂商编码</param>
            /// <param name="strRmano">RMA No.</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strVendorCode,strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryDetailCountingData(string strInsmk, string strStartLocat, string strEndLocat, string strStartMatnr, string strEndMatnr, string strStartDate, string strEndDate, string strCharg, string strMblnr, string strIsmrg, string strIsCombine, string strRegon, string strVendorCode, string strCombineParts, string strRmano, string str24H, bool blDacod, string strDecitem, bool blMqcPe,string strConfig)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetailCountingData";
                this.ControlMethodParm = "('" + strInsmk + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartDate + "','" + strEndDate + "','" + strCharg + "','" + strMblnr + "','" + strIsmrg + "','" + strIsCombine + "','" + strRegon + "','" + strVendorCode + "','" + strRmano + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = "";

                    if (blDacod == true)//查询DateCode
                    {
                        if (strIsCombine == "Y")//按料号合并
                        {
                            //  strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT,DACOD  from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                            //Modify by karen add decitem
                            strSQL = "Select I.MANDT, I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, sum(I.MENGE+I.BKQTY) as MENGE, sum(I.BKQTY) as BKQTY, '' as MBLNR,"
                            + " '' as INDAT, I.KDMAT,  ISNULL( I.RMANO,'')  AS RMANO,  ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, I.PKDAT,I.DACOD,M.DECITEM  "
                            + "  from WHITM AS I WITH(NOLOCK) LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                            + "  where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                        }
                        else if (strCombineParts == "Y")//將相同的儲位、料号、PO No.客人料號的資料做合併(Spare Parts)  Smose Liao 20100803
                        {
                            //strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, EBELN, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT,DACOD  from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                            strSQL = "Select I.MANDT, I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, sum(I.MENGE+I.BKQTY) as MENGE, sum(I.BKQTY) as BKQTY, "
                            + "  '' as MBLNR, '' as INDAT, I.EBELN, I.KDMAT, I.RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, I.PKDAT,I.DACOD ,M.DECITEM "
                            + "  from WHITM  AS I WITH(NOLOCK) LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                            + "  where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                        }
                        else
                        {
                            //if (WERKS == "CS20" && LGORT=="TWCR")
                            if (blMqcPe)
                            {
                                strSQL = "Select I.[MANDT],I.[WERKS],I.[LGORT],I.[LOCAT],I.[MATNR],I.[INSMK],I.[MBLNR],I.[CHARG],I.[LIFNR],I.[RMANO],I.[EBELN],I.[INDAT],I.[MENGE],I.[QCQTY],I.[REFNO],I.[MRGID],I.[ISPTM],I.[REQTY],I.[KDMAT],P.MQCID,P.MQCNM,P.PEID,P.PENM,I.[CRNAM],I.[CRDAT],I.[MONAM],I.[MODAT],I.[SERNO],I.[LOCOD],I.[INSPT],I.[COMCD],I.[BKQTY],I.[DACOD],I.[VEDAT],I.[BOXID],I.[NLOCA],I.[SIDNO],I.[REFID],I.[SEQNO],I.[PKDAT], ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX,I.PKDAT,I.DACOD,M.DECITEM "
                                        + "from WHITM  AS I WITH(NOLOCK)   LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR  "
                                        + "LEFT JOIN PNREMAK AS P WITH(NOLOCK) ON I.MATNR=P.MATNR AND I.WERKS=P.WERKS AND I.LGORT=P.LGORT "
                                        + "  where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                            }
                            else
                            {
                                //strSQL = "Select *, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT,DACOD from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                                strSQL = "Select I.*, ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, I.PKDAT,I.DACOD,M.DECITEM "
                                    + " from WHITM  AS I WITH(NOLOCK) LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                                    + " where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                            }
                        }
                    }
                    else
                    {
                        if (strIsCombine == "Y")//按料号合并
                        {
                            // strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                            strSQL = "Select I.MANDT, I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, sum(I.MENGE+I.BKQTY) as MENGE, sum(I.BKQTY) as BKQTY, '' as MBLNR,"
                            + " '' as INDAT, I.KDMAT,  ISNULL( I.RMANO,'')  AS RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, I.PKDAT ,M.DECITEM ,ISNULL(I.CONFIG,'') AS CONFIG"
                            + " from WHITM AS I WITH(NOLOCK) LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                            + " where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                        }
                        else if (strCombineParts == "Y")//將相同的儲位、料号、PO No.客人料號的資料做合併(Spare Parts)  Smose Liao 20100803
                        {
                            //strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, EBELN, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                            strSQL = "Select I.MANDT, I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, sum(I.MENGE+I.BKQTY) as MENGE, sum(I.BKQTY) as BKQTY, "
                            + " '' as MBLNR, '' as INDAT, I.EBELN, I.KDMAT, ISNULL( I.RMANO,'')  AS RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, I.PKDAT  ,M.DECITEM ,ISNULL(I.CONFIG,'') AS CONFIG"
                            + " from WHITM  AS I WITH(NOLOCK) LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                            + " where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                        }
                        else
                        {
                            //if (WERKS == "CS20" && LGORT == "TWCR")
                            if (blMqcPe)
                            {
                                strSQL = "Select I.[MANDT],I.[WERKS],I.[LGORT],I.[LOCAT],I.[MATNR],I.[INSMK],I.[MBLNR],I.[CHARG],I.[LIFNR],I.[RMANO],I.[EBELN],I.[INDAT],I.[MENGE],I.[QCQTY],I.[REFNO],I.[MRGID],I.[ISPTM],I.[REQTY],I.[KDMAT],P.MQCID,P.MQCNM ,P.PEID,P.PENM,I.[CRNAM],I.[CRDAT],I.[MONAM],I.[MODAT],I.[SERNO],I.[LOCOD],I.[INSPT],I.[COMCD],I.[BKQTY],I.[DACOD],I.[VEDAT],I.[BOXID],I.[NLOCA],I.[SIDNO],I.[REFID],I.[SEQNO],I.[PKDAT], I.[CONFIG],ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, PKDAT  ,M.DECITEM "
                                       + "from WHITM  AS I WITH(NOLOCK)   LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR  "
                                       + "LEFT JOIN PNREMAK AS P WITH(NOLOCK) ON I.MATNR=P.MATNR AND I.WERKS=P.WERKS AND I.LGORT=P.LGORT "
                                       + "  where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "' and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                            }
                            else
                            {
                                //strSQL = "Select *, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                                strSQL = "Select I.*, ISNULL((select MAKTX from whpat where whpat.matnr = I.matnr),'') as MAKTX, PKDAT  ,M.DECITEM from WHITM  AS I WITH(NOLOCK) "
                                + "  LEFT JOIN MATDIC AS M WITH(NOLOCK) ON I.COMCD=M.COMCD AND I.MATNR=M.MATNR "
                                + " where I.MANDT= '" + MANDT + "' AND I.COMCD='" + COMCD + "'  and I.WERKS= '" + WERKS + "' and I.LGORT= '" + LGORT + "'";
                            }
                        }
                    }
                    if (strInsmk != "")
                    {
                        strSQL += " and I.INSMK= '" + strInsmk + "'";
                    }

                    if (strCharg != "")
                    {
                        strSQL += " and I.CHARG = '" + strCharg + "'";
                    }

                    if (strMblnr != "")
                    {
                        strSQL += " and I.MBLNR= '" + strMblnr + "'";
                    }

                    if (strVendorCode != "")
                    {
                        strSQL += " and I.LIFNR= '" + strVendorCode + "'";
                    }

                    if (strRmano != "")
                    {
                        strSQL += " and I.RMANO= '" + strRmano + "'";
                    }

                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        strSQL += " and (I.LOCAT between '" + strStartLocat + "' and '" + strEndLocat + "')";
                    }
                    if (strRegon != "")
                    {
                        //strSQL += " and exists (select 'Y' from whhed where regon = '" + strRegon + "' and whhed.mandt = whitm.mandt and whhed.comcd = whitm.comcd and whhed.werks = whitm.werks and whhed.lgort = whitm.lgort and whhed.locat = whitm.locat )";
                        strSQL += " and exists (select 'Y' from whhed where regon = '" + strRegon + "' and whhed.mandt = I.mandt and whhed.comcd = I.comcd and whhed.werks = I.werks and whhed.lgort = I.lgort and whhed.locat = I.locat )";
                    }

                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        strSQL += " and I.LOCAT = '" + strEndLocat + "'";
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        strSQL += " and I.LOCAT = '" + strStartLocat + "'";
                    }

                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        strSQL += " and (I.MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        strSQL += " and I.MATNR = '" + strEndMatnr + "'";
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        strSQL += " and I.MATNR = '" + strStartMatnr + "'";
                    }

                    if (strDecitem != "")
                    {
                        strSQL += " and M.DECITEM = '" + strDecitem + "'";
                    }

                    if (strStartDate != "" && strEndDate != "")
                    {
                        strSQL += " and (I.CRDAT between '" + strStartDate + "' and '" + strEndDate + "  23:59:59')";
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        strSQL += " and I.CRDAT = '" + strEndDate + "'";
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        strSQL += " and I.CRDAT = '" + strEndDate + "'";
                    }

                    if (strIsmrg != "")//连板料号
                    {
                        if (strIsmrg == "Y")
                        {
                            strSQL += " and I.MRGID <> ''";
                        }
                        else
                        {
                            strSQL += "";
                        }
                    }
                    if (strConfig != "")//CONFIG
                    {
                         strSQL += " and I.CONFIG = '" + strConfig + "'";
                    }
                    //限定只帶出棧板滿板時間為24H以上的資料
                    if (str24H == "Y")
                    {
                        strSQL += " and (DATEDIFF(HH,I.PKDAT,GETDATE()) >= 24)";
                    }
                    if (blDacod == true)
                    {
                        if (strIsCombine == "Y")//料号合并
                        {
                            strSQL += " group by I.MANDT,I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, I.KDMAT,  ISNULL( I.RMANO,'') , I.PKDAT,I.DACOD,M.DECITEM order by I.LOCAT, I.MATNR";
                        }
                        else if (strCombineParts == "Y")//將相同的儲位、料号、PO No.客人料號的資料做合併(Spare Parts)  Smose Liao 20100803
                        {
                            strSQL += " group by I.MANDT,I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, I.KDMAT, I.EBELN,  ISNULL( I.RMANO,'') , I.PKDAT,I.DACOD,M.DECITEM order by I.LOCAT, I.MATNR";
                        }
                        else
                        {
                            strSQL += " order by I.LOCAT, I.MATNR";
                        }
                    }
                    else
                    {
                        if (strIsCombine == "Y")//料号合并
                        {
                            strSQL += " group by I.MANDT,I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, I.KDMAT, ISNULL( I.RMANO,'') , I.PKDAT,M.DECITEM ,ISNULL(I.CONFIG,'') order by I.LOCAT, I.MATNR";
                        }
                        else if (strCombineParts == "Y")//將相同的儲位、料号、PO No.客人料號的資料做合併(Spare Parts)  Smose Liao 20100803
                        {
                            strSQL += " group by I.MANDT,I.COMCD, I.WERKS, I.LGORT, I.LOCAT, I.MATNR, I.INSMK, I.CHARG, I.KDMAT, I.EBELN,  ISNULL( I.RMANO,'') , I.PKDAT,M.DECITEM ,ISNULL(I.CONFIG,'') order by I.LOCAT, I.MATNR";
                        }
                        else
                        {
                            strSQL += " order by I.LOCAT, I.MATNR";
                        }
                    }


                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                    //加總之後數量為0的資料不顯示  Add by Smose Liao  20091228 
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MENGE"].ToString()) == 0 && int.Parse(dtData.Rows[i]["BKQTY"].ToString()) == 0)
                        {
                            dtData.Rows.Remove(dtData.Rows[i]);
                        }
                    }

                    return dtData;
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

            # region 获得盘点前置作业数据 Add by Mandy Ma
            /// <summary>
            /// 获得盘点前置作业数据
            /// </summary> 
            /// <param name="strKostl">部门代码</param>
            /// <param name="strInsmk">库别</param>
            /// <param name="strIsmrg">是否连板料号</param>
            /// <param name="strIsCombine">是否按料号合并</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryCountingPrepareData(strKostl,strInsmk, strIsmrg,strIsCombine);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryCountingPrepareData(string strKostl, string strInsmk, string strIsmrg, string strIsCombine)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCountingPrepareData";
                this.ControlMethodParm = "('" + strKostl + "','" + strInsmk + "','" + strIsmrg + "','" + strIsCombine + "')";
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
                    alColumns.Add("MANDT");
                    alColumns.Add("COMCD");
                    alColumns.Add("'' AS CYCNO");
                    alColumns.Add("'" + strKostl + "' as KOSTL");
                    alColumns.Add("WERKS");
                    alColumns.Add("LGORT");
                    alColumns.Add("LOCAT");
                    alColumns.Add("MATNR");
                    alColumns.Add("INSMK");
                    alColumns.Add("CHARG");
                    if (strIsCombine == "Y")
                    {
                        alColumns.Add("sum(MENGE) as MENGE");

                    }
                    else
                    {
                        alColumns.Add("MENGE");

                    }
                    alColumns.Add("0 as CKQTY");
                    alColumns.Add("CONVERT(char(8), getdate(), 112) as CKDAT");

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + WERKS + "'");
                    alConditions.Add("LGORT='" + LGORT + "'");

                    if (strIsmrg == "Y")
                    {
                        if (strIsCombine == "Y")
                        {
                            alConditions.Add("INSMK='" + strInsmk + "' AND MRGID<>'' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG");
                        }
                        else
                        {
                            alConditions.Add("INSMK='" + strInsmk + "' AND MRGID<>''");
                        }

                    }
                    else
                    {
                        if (strIsCombine == "Y")
                        {
                            alConditions.Add("INSMK='" + strInsmk + "'  group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG");
                        }
                        else
                        {
                            alConditions.Add("INSMK='" + strInsmk + "'");
                        }

                    }

                    DataTable dtData = new DataTable();
                    dtData = objDataWhitm.EntityQuery(alColumns, alConditions, false);
                    dtData = CommonInfo.SortDataTable(dtData, " LOCAT, MATNR, INSMK, CHARG ");
                    return dtData;

                    # region old function
                    //string strSQL = "";
                    //if (strIsmrg == "Y")
                    //{
                    //    if (strIsCombine == "Y")
                    //    {
                    //        strSQL = "Select MANDT,COMCD, '' as CYCNO, '" + strKostl + "' as KOSTL, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE) as MENGE, 0 as CKQTY, CONVERT(char(8), getdate(), 112) as CKDAT from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + WERKS + "' and LGORT= '" + LGORT + "' and INSMK='" + strInsmk + "' and MRGID<>''  group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG order by LOCAT, MATNR, INSMK, CHARG";
                    //    }
                    //    else
                    //    {
                    //        strSQL = "Select MANDT,COMCD, '' as CYCNO, '" + strKostl + "' as KOSTL, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MENGE, 0 as CKQTY, CONVERT(char(8), getdate(), 112) as CKDAT from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + WERKS + "' and LGORT= '" + LGORT + "' and MRGID<>'' and INSMK='" + strInsmk + "' order by LOCAT, MATNR, INSMK, CHARG";
                    //    }
                    //}
                    //else
                    //{
                    //    if (strIsCombine == "Y")
                    //    {
                    //        strSQL = "Select MANDT,COMCD, '' as CYCNO, '" + strKostl + "' as KOSTL, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE) as MENGE, 0 as CKQTY, CONVERT(char(8), getdate(), 112) as CKDAT from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + WERKS + "' and LGORT= '" + LGORT + "' and INSMK='" + strInsmk + "' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG order by LOCAT, MATNR, INSMK, CHARG";
                    //    }
                    //    else
                    //    {
                    //        strSQL = "Select MANDT,COMCD, '' as CYCNO, '" + strKostl + "' as KOSTL, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MENGE, 0 as CKQTY, CONVERT(char(8), getdate(), 112) as CKDAT from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS = '" + WERKS + "' and LGORT= '" + LGORT + "' and INSMK='" + strInsmk + "' order by LOCAT, MATNR, INSMK, CHARG";
                    //    }
                    //}

                    //DataTable dtData = new DataTable();
                    //ControlHandleDB();
                    //dtData = ControlSqlAccess.GetDataTable(strSQL);
                    //ControlSqlAccess.CloseConnection();
                    //return dtData;
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

            # region 获得盘点前置作业数据 Add by Mandy Ma
            /// <summary>
            /// 获得盘点前置作业数据
            /// </summary> 
            /// <param name="strKostl">部门代码</param>
            /// <param name="strInsmk">库别</param>
            /// <param name="strIsmrg">是否连板料号</param>
            /// <param name="strIsCombine">是否按料号合并</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryCountingPrepareDataMore(string strKostl, string strInsmk, string strIsmrg, string strIsCombine)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCountingPrepareDataMore";
                this.ControlMethodParm = "('" + strKostl + "','" + strInsmk + "','" + strIsmrg + "','" + strIsCombine + "')";
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
                    alColumns.Add("MANDT");
                    alColumns.Add("'' AS CYCNO");
                    alColumns.Add("'" + strKostl + "' as KOSTL");
                    alColumns.Add("WERKS");
                    alColumns.Add("LGORT");
                    alColumns.Add("LOCAT");
                    alColumns.Add("MATNR");
                    alColumns.Add("INSMK");
                    alColumns.Add("CHARG");
                    if (strIsCombine == "Y")
                    {
                        alColumns.Add("sum(MENGE) as MENGE");
                    }
                    else
                    {
                        alColumns.Add("MENGE");
                    }

                    alColumns.Add("CONVERT(char(8), getdate(), 112) as CKDAT");
                    if (strIsCombine == "Y")
                    {
                        alColumns.Add("sum(MENGE)  as CKQTY");
                    }
                    else
                    {
                        alColumns.Add(" MENGE  as CKQTY");
                    }
                    alColumns.Add(" '3'  as STATS ");
                    alColumns.Add(" '" + UserData.UserId + "'  as CRNAM ");
                    alColumns.Add("  GETDATE()  as CRDAT ");
                    alColumns.Add(" '" + UserData.UserId + "'  as MONAM ");
                    alColumns.Add("  GETDATE()  as MODAT ");
                    alColumns.Add("COMCD");

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + WERKS + "'");
                    alConditions.Add("LGORT='" + LGORT + "'");

                    if (strIsmrg == "Y")
                    {
                        if (strIsCombine == "Y")
                        {
                            alConditions.Add("INSMK='" + strInsmk + "' AND MRGID<>'' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG");
                        }
                        else
                        {
                            alConditions.Add("INSMK='" + strInsmk + "' AND MRGID<>''");
                        }

                    }
                    else
                    {
                        if (strIsCombine == "Y")
                        {
                            alConditions.Add("INSMK='" + strInsmk + "'  group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG");
                        }
                        else
                        {
                            alConditions.Add("INSMK='" + strInsmk + "'");
                        }

                    }

                    DataTable dtData = new DataTable();
                    string strSql = objDataWhitm.ControlGetQuerySql("WHITM", alColumns, alConditions, true);
                    dtData = objDataWhitm.EntityQuery(alColumns, alConditions, false);
                    dtData = CommonInfo.SortDataTable(dtData, " LOCAT, MATNR, INSMK, CHARG ");
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

            # region 新增盘点票资料 Add by Mandy Ma
            /// <summary>
            /// 新增盘点票资料
            /// </summary> 
            /// <param name="strKostl">部门代码</param>
            /// <param name="dtCounting">盘点票资料table</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  bool  bolReturn = objCounting.AddCountingData(strKostl, dtCounting);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool AddCountingData(string strKostl, DataTable dtCounting)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddCountingData";
                this.ControlMethodParm = "('" + strKostl + "','" + dtCounting + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);

                    # region 清除先前资料
                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + WERKS + "'");
                    alConditions.Add("LGORT='" + LGORT + "'");
                    alConditions.Add("KOSTL='" + strKostl + "'");

                    bool bolFlage = objDataWhcyc.EntityDelete(alConditions);
                    # endregion

                    # region 循环插入资料
                    for (int i = 0; i < dtCounting.Rows.Count; i++)
                    {
                        objDataWhcyc.ResetField();
                        objDataWhcyc.Mandt = dtCounting.Rows[i]["MANDT"].ToString();
                        objDataWhcyc.Cycno = dtCounting.Rows[i]["CYCNO"].ToString();
                        objDataWhcyc.Kostl = dtCounting.Rows[i]["KOSTL"].ToString();
                        objDataWhcyc.Werks = dtCounting.Rows[i]["WERKS"].ToString();
                        objDataWhcyc.Lgort = dtCounting.Rows[i]["LGORT"].ToString();
                        objDataWhcyc.Locat = dtCounting.Rows[i]["LOCAT"].ToString();
                        objDataWhcyc.Matnr = dtCounting.Rows[i]["MATNR"].ToString();
                        objDataWhcyc.Insmk = dtCounting.Rows[i]["INSMK"].ToString();
                        objDataWhcyc.Charg = dtCounting.Rows[i]["CHARG"].ToString();
                        objDataWhcyc.Menge = dtCounting.Rows[i]["MENGE"].ToString();
                        objDataWhcyc.Ckdat = dtCounting.Rows[i]["CKDAT"].ToString();
                        objDataWhcyc.Ckqty = "0";
                        objDataWhcyc.Stats = "1";
                        objDataWhcyc.Crnam = CRNAM;
                        objDataWhcyc.Crdat = "getdate()";
                        objDataWhcyc.Monam = CRNAM;
                        objDataWhcyc.Modat = "getdate()";
                        objDataWhcyc.Comcd = dtCounting.Rows[i]["COMCD"].ToString();
                        arySQL.Add(objDataWhcyc.EntityGetInsertSql());

                    }
                    bool bolReturn = false;
                    ControlHandleDB();
                    bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                    return bolReturn;
                    # endregion


                    # region Old Function
                    //ArrayList arySQL = new ArrayList();
                    //string strSQL = "";
                    ////清除先前资料
                    //arySQL.Add("Delete from WHCYC where MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS='" + WERKS + "' and LGORT='" + LGORT + "' and KOSTL='" + strKostl + "'");
                    ////WHCYC
                    //for (int i = 0; i < dtCounting.Rows.Count; i++)
                    //{
                    //    strSQL = "Insert into WHCYC(MANDT, CYCNO, KOSTL, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MENGE, CKDAT, CKQTY, STATS, CRNAM, CRDAT, MONAM, MODAT,COMCD) " +
                    //        "values('" + dtCounting.Rows[i]["MANDT"].ToString() + "', '" + dtCounting.Rows[i]["CYCNO"].ToString() + "', '" + dtCounting.Rows[i]["KOSTL"].ToString() + "', '" + dtCounting.Rows[i]["WERKS"].ToString() + "','" + dtCounting.Rows[i]["LGORT"].ToString() + "','" +
                    //        dtCounting.Rows[i]["LOCAT"].ToString() + "','" + dtCounting.Rows[i]["MATNR"].ToString() + "','" + dtCounting.Rows[i]["INSMK"].ToString() + "','" +
                    //        dtCounting.Rows[i]["CHARG"].ToString() + "','" + dtCounting.Rows[i]["MENGE"].ToString() + "','" + dtCounting.Rows[i]["CKDAT"].ToString() + "','0','1','" + CRNAM + "', getdate() ,'" + CRNAM + "', getdate(),'" + dtCounting.Rows[i]["COMCD"].ToString() + "')";

                    //    arySQL.Add(strSQL);
                    //}

                    //bool bolReturn = false;
                    //ControlHandleDB();
                    //bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
                    //ControlSqlAccess.CloseConnection();
                    //return bolReturn;
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

            # region 批量新增盘点票资料----Refun
            /// <summary>
            /// 批量新增盘点票资料
            /// </summary> 
            /// <param name="dtCounting">盘点票资料table</param>
            /// <returns>
            /// bool
            /// </returns>
            /////////////////////////////////////////////////////////////////////////////	
            public bool AddCountingDataMore(string strKostl, DataTable dtCounting)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "AddCountingDataMore";
                this.ControlMethodParm = "('" + strKostl + "','" + dtCounting + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arySQL = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);

                    # region 清除先前资料
                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + WERKS + "'");
                    alConditions.Add("LGORT='" + LGORT + "'");
                    alConditions.Add("KOSTL='" + strKostl + "'");

                    bool bolFlage = objDataWhcyc.EntityDelete(alConditions);
                    # endregion

                    ControlHandleDB();
                    bolFlage = ControlSqlAccess.ExecSqlBulkCopy("WHCYC", dtCounting);
                    ControlSqlAccess.CloseConnection();
                    return bolFlage;

                    //System.Collections.IList<string> lst = new System.Collections.List<string>();



                    //lst.Add("CS12 TW20  DDFEF    DDE");


                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            # region 查询盘点调账数据 Add by Mandy Ma
            /// <summary>
            /// 查询盘点调账数据
            /// </summary> 
            /// <param name="strKostl">部门代码</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryCountingData(strKostl);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryCountingData(string strKostl)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCountingData";
                this.ControlMethodParm = "('" + strKostl + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    ArrayList alColumns = new ArrayList();
                    ArrayList alConditions = new ArrayList();
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);

                    alColumns.Clear();
                    alColumns.Add("*");

                    alConditions.Clear();
                    alConditions.Add("MANDT='" + MANDT + "'");
                    alConditions.Add("COMCD='" + COMCD + "'");
                    alConditions.Add("WERKS='" + WERKS + "'");
                    alConditions.Add("LGORT='" + LGORT + "'");
                    alConditions.Add("KOSTL='" + strKostl + "'");

                    DataTable dtData = new DataTable();
                    dtData = objDataWhcyc.EntityQuery(alColumns, alConditions, false);
                    dtData = CommonInfo.SortDataTable(dtData, " LOCAT, MATNR, INSMK, CHARG ");
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

            # region 系统自动调账 Add by Mandy Ma
            /// <summary>
            /// 系统自动调账
            /// </summary> 
            /// <param name="strKostl">部门代码</param>
            /// <param name="dtCounting">调账后的资料</param>
            /// <returns>
            /// bool
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  bool  bolReturn = objCounting.AddCountingData(strKostl, dtCounting);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public bool UpdateCountingData(string strKostl, DataTable dtCounting)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateCountingData";
                this.ControlMethodParm = "('" + strKostl + "','" + dtCounting + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    ArrayList alConditions = new ArrayList();
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);

                    ArrayList arySQL = new ArrayList();
                    //WHCYC
                    for (int i = 0; i < dtCounting.Rows.Count; i++)
                    {
                        objDataWhcyc.Ckqty = dtCounting.Rows[i]["CKQTY"].ToString();
                        objDataWhcyc.Stats = dtCounting.Rows[i]["STATS"].ToString();
                        objDataWhcyc.Monam = CRNAM;
                        objDataWhcyc.Modat = "getdate()";

                        alConditions.Clear();
                        alConditions.Add("MANDT='" + MANDT + "'");
                        alConditions.Add("COMCD='" + COMCD + "'");
                        alConditions.Add("WERKS='" + WERKS + "'");
                        alConditions.Add("LGORT='" + LGORT + "'");
                        alConditions.Add("KOSTL='" + dtCounting.Rows[i]["KOSTL"].ToString() + "'");
                        alConditions.Add("LOCAT='" + dtCounting.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add("CYCNO='" + dtCounting.Rows[i]["CYCNO"].ToString() + "'");
                        arySQL.Add(objDataWhcyc.EntityGetUpdateSql(alConditions));
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

            # region 查询盘点异动明细资料 Add by Mandy Ma
            /// <summary>
            ///查询盘点异动明细资料
            /// </summary> 
            /// <param name="strInsmk">库别</param>
            /// <param name="strStartLocat">储位起</param>
            /// <param name="strEndLocat">储位止</param>
            /// <param name="strStartMatnr">料号起</param>
            /// <param name="strEndMatnr">料号止</param>
            /// <param name="strStartDate">创建日期起</param>
            /// <param name="strEndDate">创建日期止</param>
            /// <param name="strCharg">料号版本</param>
            /// <param name="strMblnr">扣帐单号</param>
            /// <param name="strIsmrg">是否连板料号</param>
            /// <param name="strIsCombine">是否合并料号</param>
            /// <param name="strRegon">洲别</param>
            /// <param name="strStartDate_Change">Storage更改时间起</param>
            /// <param name="strEndDate_Change">Storage更改时间止</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public DataTable QueryDetailChangedData(string strInsmk, string strStartLocat, string strEndLocat, string strStartMatnr, string strEndMatnr, string strStartDate, string strEndDate, string strCharg, string strMblnr, string strIsmrg, string strIsCombine, string strRegon, string strStartDate_Change, string strEndDate_Change)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetailCountingData";
                this.ControlMethodParm = "('" + strInsmk + "','" + strStartLocat + "','" + strEndLocat + "','" + strStartMatnr + "','" + strEndMatnr + "','" + strStartDate + "','" + strEndDate + "','" + strCharg + "','" + strMblnr + "','" + strIsmrg + "','" + strIsCombine + "','" + strRegon + "','" + strStartDate_Change + "','" + strEndDate_Change + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string strSQL = "";
                    //string strSubSQL = "";
                    string strSubSQL_where = " where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + WERKS + "' AND LGORT='" + LGORT + "' ";

                    if (strIsCombine == "Y")
                    {

                        strSQL = "Select MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE) as MENGE, '' as MBLNR, '' as INDAT, KDMAT , ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX  from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                    }
                    else
                    {
                        strSQL = "Select *, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX from WHITM where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                    }
                    #region 库别
                    if (strInsmk != "")
                    {
                        strSQL += " and INSMK= '" + strInsmk + "'";
                        strSubSQL_where += " and INSMK= '" + strInsmk + "'";
                    }
                    #endregion
                    #region 料号版本
                    if (strCharg != "")
                    {
                        strSQL += " and CHARG = '" + strCharg + "'";
                        strSubSQL_where += " and CHARG = '" + strCharg + "'";
                    }
                    #endregion
                    #region 扣帐单号
                    if (strMblnr != "")
                    {
                        strSQL += " and MBLNR= '" + strMblnr + "'";
                        strSubSQL_where += " and MBLNR= '" + strMblnr + "'";
                    }
                    #endregion
                    #region 储位
                    if (strStartLocat != "" && strEndLocat != "")
                    {
                        strSQL += " and (LOCAT between '" + strStartLocat + "' and '" + strEndLocat + "')";
                        strSubSQL_where += " and ((OLOCA between '" + strStartLocat + "' and '" + strEndLocat + "')  or (NLOCA between '" + strStartLocat + "' and '" + strEndLocat + "'))";
                    }
                    else if (strStartLocat == "" && strEndLocat != "")
                    {
                        strSQL += " and LOCAT = '" + strEndLocat + "'";
                        strSubSQL_where += " and(( OLOCA = '" + strEndLocat + "') OR ( NLOCA = '" + strEndLocat + "') )";
                    }
                    else if (strStartLocat != "" && strEndLocat == "")
                    {
                        strSQL += " and LOCAT = '" + strStartLocat + "'";
                        strSubSQL_where += " and(( OLOCA = '" + strStartLocat + "') OR ( NLOCA = '" + strStartLocat + "') )";
                    }
                    #endregion

                    if (strRegon != "")
                    {
                        strSQL += " and exists (select 'Y' from whhed where regon = '" + strRegon + "' and whhed.mandt = whitm.mandt and whhed.COMCD = whitm.COMCD and whhed.werks = whitm.werks and whhed.lgort = whitm.lgort and whhed.locat = whitm.locat )";
                    }





                    #region 料号
                    if (strStartMatnr != "" && strEndMatnr != "")
                    {
                        strSQL += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                        strSubSQL_where += " and (MATNR between '" + strStartMatnr + "' and '" + strEndMatnr + "')";
                    }
                    else if (strStartMatnr == "" && strEndMatnr != "")
                    {
                        strSQL += " and MATNR = '" + strEndMatnr + "'";
                        strSubSQL_where += " and MATNR = '" + strEndMatnr + "'";
                    }
                    else if (strStartMatnr != "" && strEndMatnr == "")
                    {
                        strSQL += " and MATNR = '" + strStartMatnr + "'";
                        strSubSQL_where += " and MATNR = '" + strStartMatnr + "'";
                    }
                    #endregion
                    #region  WHITM.CKDATE
                    if (strStartDate != "" && strEndDate != "")
                    {
                        strSQL += " and (CRDAT between '" + strStartDate + "' and '" + strEndDate + "  23:59:59')";
                    }
                    else if (strStartDate == "" && strEndDate != "")
                    {
                        strSQL += " and CRDAT = '" + strEndDate + "'";
                    }
                    else if (strStartDate != "" && strEndDate == "")
                    {
                        strSQL += " and CRDAT = '" + strStartDate + "'";
                    }
                    #endregion





                    if (strIsmrg != "")
                    {
                        if (strIsmrg == "Y")
                        {
                            strSQL += " and MRGID <> ''";
                        }
                        else
                        {
                            strSQL += "";
                        }
                    }

                    #region  whlog.CKDATE
                    if (strStartDate_Change != "" && strEndDate_Change != "")
                    {

                        strSubSQL_where += " AND (convert(varchar(20),WHLOG.CRDAT,120) between '" + strStartDate_Change + "' and '" + strEndDate_Change + "')";
                    }
                    else if (strStartDate_Change == "" && strEndDate_Change != "")
                    {
                        strSubSQL_where += " AND convert(varchar(8),WHLOG.CRDAT,112) = '" + strEndDate_Change.Replace(",", "").Replace("-", "").Replace(" ", "").Substring(0, 8) + "'";
                    }
                    else if (strStartDate_Change != "" && strEndDate_Change == "")
                    {
                        strSubSQL_where += " AND convert(varchar(8),WHLOG.CRDAT,112) = '" + strStartDate_Change.Replace(",", "").Replace("-", "").Replace(" ", "").Substring(0, 8) + "'";
                    }
                    #endregion
                    strSQL += " AND (MATNR IN (SELECT DISTINCT MATNR FROM WHLOG " + strSubSQL_where + "  ) )";



                    if (strIsCombine == "Y")
                    {

                        strSQL += " group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, KDMAT order by LOCAT, MATNR";
                    }
                    else
                    {
                        strSQL += " order by LOCAT, MATNR";
                    }

                    DataTable dtData = new DataTable();
                    //try
                    //{
                    //    objSQLAccess.ConnectionTimeOut = 1200000;
                    //    dtData = this.objSQLAccess.GetDataTable(strSQL);
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    ERRMSG = ex.Message + "<- QueryDetailCountingData()";
                    //}
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
            # endregion

            #region 更新barcode盤點庫存
            /// <summary>
            /// 获得储位明细盘点资料
            /// </summary> 
            /// <param name="dtData">Data Table</param>
            /// <returns>
            /// bool bolResult
            /// </returns>

            public bool UpdateCountingData(DataTable dtUpdateData)
            {
                bool bolReturn = false;
                string strSQL = "";
                DataTable dtData;
                DataWhitm objWhitm = new DataWhitm(UserData);
                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                ArrayList arySQL = new ArrayList();

                dtData = dtUpdateData;
                this.ControlMethodName = "UpdateCountingData";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    alColumns.Clear();
                    alConditions.Clear();
                    objWhitm.ResetField();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        objWhitm.Reqty = dtData.Rows[i]["Alqty"].ToString();

                        alConditions.Add("MANDT= '" + MANDT + "'");
                        alConditions.Add("COMCD= '" + COMCD + "'");
                        alConditions.Add("WERKS= '" + dtData.Rows[i]["WERKS"].ToString() + "'");
                        alConditions.Add("LGORT= '" + dtData.Rows[i]["LGORT"].ToString() + "'");
                        alConditions.Add("LOCAT= '" + dtData.Rows[i]["LOCAT"].ToString() + "'");
                        alConditions.Add("MATNR= '" + dtData.Rows[i]["MATNR"].ToString() + "'");
                        alConditions.Add("INSMK= '" + dtData.Rows[i]["INSMK"].ToString() + "'");
                        alConditions.Add("CHARG= '" + dtData.Rows[i]["CHARG"].ToString() + "'");

                        arySQL.Add(objWhitm.EntityGetUpdateSql(alConditions));
                    }

                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                }
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

            #region 同步盘点信息到Pad
            /// <summary>
            /// 同步盘点信息到Pad
            /// </summary> 
            /// <param name="dtData">Data Table</param>
            /// <returns>
            /// bool bolResult
            /// </returns>

            public bool AddStoragePad(DataTable dtData)
            {
                bool bolReturn = false;


                ArrayList alColumns = new ArrayList();
                ArrayList alConditions = new ArrayList();
                StringBuilder sbSql = new StringBuilder();
                ArrayList arySQL = new ArrayList();


                this.ControlMethodName = "UpdateCountingData";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    if (dtData.Rows.Count > 0)
                    {
                        string strID = DateTime.Now.ToFileTime().ToString().Trim();
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            StringBuilder strsql = new StringBuilder();
                            strsql.AppendFormat(" INSERT INTO PADST([MANDT] ,[WERKS],[LGORT],[LOCAT],[MATNR],[KDMAT],[INSMK] ,[CHARG] ,[MENGE],[BKQTY],[MBLNR],[INDAT],ID)  VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                                dtData.Rows[i]["MANDT"].ToString().Trim(), dtData.Rows[i]["WERKS"].ToString().Trim(), dtData.Rows[i]["LGORT"].ToString().Trim(), dtData.Rows[i]["LOCAT"].ToString().Trim(), dtData.Rows[i]["MATNR"].ToString().Trim(), dtData.Rows[i]["KDMAT"].ToString().Trim(),
                                dtData.Rows[i]["INSMK"].ToString().Trim(), dtData.Rows[i]["CHARG"].ToString().Trim(), dtData.Rows[i]["MENGE"].ToString().Trim(), dtData.Rows[i]["BKQTY"].ToString().Trim(), dtData.Rows[i]["MBLNR"].ToString().Trim(), dtData.Rows[i]["INDAT"].ToString().Trim(), strID);
                            arySQL.Add(strsql.ToString().Trim());
                        }
                    }

                    ControlHandleDB();
                    bolReturn = this.ControlSqlAccess.ExecSqlArray(arySQL);
                    ControlSqlAccess.CloseConnection();
                }
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

            # region 通过拆分二维码获得储位明细Scan资料 Add by Freeman
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 通过拆分二维码获得储位明细Scan资料
            /// </summary> 
            /// <param name="strInsmk">库别</param>
            /// <param name="strStartLocat">储位起</param>
            /// <param name="strEndLocat">储位止</param>
            /// <param name="strStartMatnr">料号起</param>
            /// <param name="strEndMatnr">料号止</param>
            /// <param name="strStartDate">创建时间起</param>
            /// <param name="strEndDate">创建时间止</param>
            /// <param name="strCharg">料号版本</param>
            /// <param name="strMblnr">扣帐单号</param>
            /// <param name="strIsmrg">是否连板料号</param>
            /// <param name="strIsCombine">是否合并料号</param>
            /// <param name="strRegon">洲别</param>
            /// <param name="strVendorCode">厂商编码</param>
            /// <param name="strRmano">RMA No.</param>
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strVendorCode,strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryQRCode(string strLocat, string strMatnr, string strInsmk)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetailCountingData";
                this.ControlMethodParm = "('" + strLocat + "','" + strMatnr + "','" + strInsmk + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = "";

                    strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG,LIFNR, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY,MBLNR, INDAT, KDMAT, RMANO,LOCOD,DACOD ,ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '"
                        + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT='" + strLocat + "' and MATNR='" + strMatnr + "' and INSMK='" + strInsmk + "'  group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG,MBLNR, LIFNR,INDAT, KDMAT, RMANO, LOCOD,DACOD,PKDAT order by LOCAT, MATNR";

                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                    //加總之後數量為0的資料不顯示  Add by Smose Liao  20091228 
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MENGE"].ToString()) == 0 && int.Parse(dtData.Rows[i]["BKQTY"].ToString()) == 0)
                        {
                            dtData.Rows.Remove(dtData.Rows[i]);
                        }
                    }

                    return dtData;
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

            # region 获得最新储位明细资料 Add by Freeman
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 获得最新储位明细资料
            /// </summary> 
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strVendorCode,strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryLocationScanAdjustData(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetailCountingData";
                this.ControlMethodParm = "('" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    DataTable dt = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataRow drRow;
                    string strSQL = "";
                    dt2 = dtData.Clone();
                    ControlHandleDB();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strSQL = @"Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, LIFNR,CRDAT,MBLNR,sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, INDAT, LIFNR,CRDAT,KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '" +
                                MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT +
                                "' and LOCAT='" + dtData.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "' and MBLNR='" + dtData.Rows[i]["MBLNR"].ToString() + "' and INSMK='" +
                                dtData.Rows[i]["INSMK"].ToString() +
                                "' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, LIFNR,CRDAT,MBLNR,INDAT, KDMAT, RMANO, PKDAT order by LOCAT, MATNR";

                        dt = ControlSqlAccess.GetDataTable(strSQL);
                        drRow = dt2.NewRow();
                        drRow["MANDT"] = dt.Rows[0]["MANDT"].ToString();
                        drRow["COMCD"] = dt.Rows[0]["COMCD"].ToString();
                        drRow["WERKS"] = dt.Rows[0]["WERKS"].ToString();
                        drRow["LGORT"] = dt.Rows[0]["LGORT"].ToString();
                        drRow["LOCAT"] = dt.Rows[0]["LOCAT"].ToString();
                        drRow["MATNR"] = dt.Rows[0]["MATNR"].ToString();
                        drRow["INSMK"] = dt.Rows[0]["INSMK"].ToString();
                        drRow["CHARG"] = dt.Rows[0]["CHARG"].ToString();
                        drRow["MENGE"] = dt.Rows[0]["MENGE"].ToString();
                        drRow["SCQTY"] = dtData.Rows[i]["SCQTY"].ToString();
                        if (dtData.Rows[i]["MENGE"].ToString() != "" && dtData.Rows[i]["SCQTY"].ToString() != "")
                        {
                            drRow["DIFQTY"] = Convert.ToInt32(dt.Rows[0]["MENGE"]) -
                                              Convert.ToInt32(dtData.Rows[i]["SCQTY"]);
                        }
                        drRow["CRDAT"] = dt.Rows[0]["CRDAT"].ToString();
                        dt2.Rows.Add(drRow);
                    }


                    ControlSqlAccess.CloseConnection();

                    //加總之後數量為0的資料不顯示  Add by Smose Liao  20091228 
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MENGE"].ToString()) == 0 && int.Parse(dtData.Rows[i]["BKQTY"].ToString()) == 0)
                        {
                            dtData.Rows.Remove(dtData.Rows[i]);
                        }
                    }

                    return dt2;
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

            # region 根据DIDNO前11码或PalletID查询料号资料 Add by Freeman
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 根据DIDNO前11码或PalletID查询料号资料
            /// </summary> 
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strVendorCode,strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryDIDPIDData(string strLocat, string strMblnr, string strInsmk, string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDIDnoData";
                this.ControlMethodParm = "('" + strLocat + "','" + strMblnr + "','" + strInsmk + "','" + strType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = "";
                    if (strType == "DIDNO")
                    {
                        if (strMblnr.Length == 11)
                        {
                            strSQL =
                                "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR,LIFNR,sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, INDAT, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '" +
                                MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT +
                                "' and LOCAT='" + strLocat + "' and MATNR='" + strMblnr + "' and INSMK='" + strInsmk +
                                "' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR,LIFNR,INDAT, KDMAT, RMANO, PKDAT order by LOCAT, MATNR";
                        }
                    }
                    if (strType == "PALID")
                    {
                        strSQL =
                            "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR,LIFNR,sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, INDAT, KDMAT, RMANO, ISNULL((select MAKTX from whpat where whpat.matnr = WHITM.matnr),'') as MAKTX, PKDAT  from WHITM where MANDT= '" +
                            MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT +
                            "' and MBLNR='" + strMblnr + "' and INSMK='" + strInsmk + "' group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, MBLNR,LIFNR,INDAT, KDMAT, RMANO, PKDAT order by LOCAT, MATNR";
                    }



                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();

                    //加總之後數量為0的資料不顯示  Add by Smose Liao  20091228 
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MENGE"].ToString()) == 0 && int.Parse(dtData.Rows[i]["BKQTY"].ToString()) == 0)
                        {
                            dtData.Rows.Remove(dtData.Rows[i]);
                        }
                    }

                    return dtData;
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

            # region 盘点查询

            //实时盘点厂区仓别
            public DataTable InvWerks(string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvWerks";
                //this.ControlMethodParm = "('" + strStartDate + "," + strEndDate + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtWerks = new DataTable();
                    if (strType == "BoxID")
                    {
                        sbSql.AppendFormat(@"SELECT CTRLNM AS WERKS,CTRLC1 AS LGORT FROM WHCTRL WITH(NOLOCK)
                                    WHERE MANDT = '{0}' AND COMCD = '{1}' AND CTRLC4='ScanBoxid' AND CTRLID='RTINV'AND CTRLC5=N'盘点对比' InterSect SELECT CTRLNM,CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLNM+CTRLC1 IN (Select WERKS+LGORT from WHAUT WITH(NOLOCK) where MANDT='{0}' and COMCD='{1}' and USRNM='{2}')",
                        MANDT, COMCD, UserData.UserId);
                    }
                    else if (strType == "RInv")
                    {
                        sbSql.AppendFormat(@"SELECT CTRLNM FROM WHCTRL WITH(NOLOCK)
                                    WHERE MANDT = '{0}' AND COMCD = '{1}' AND CTRLID='RTINV'AND CTRLC5=N'盘点对比' InterSect SELECT CTRLNM FROM WHCTRL WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND SOLDTO='QWMS' AND CTRLID='WERKS' AND CTRLNM IN (Select WERKS from WHAUT WITH(NOLOCK) where MANDT='{0}' and COMCD='{1}' and USRNM='{2}')",
                        MANDT, COMCD, UserData.UserId);

                    }
                    else if (strType == "SN")
                    {
                        sbSql.AppendFormat(@"SELECT CTRLNM AS WERKS,CTRLC1 AS LGORT FROM WHCTRL WITH(NOLOCK)
                                    WHERE MANDT = '{0}' AND COMCD = '{1}' AND CTRLC4='ScanBoxid_SN' AND CTRLID='RTINV'AND CTRLC5=N'盘点对比' InterSect SELECT CTRLNM,CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT='{0}'AND COMCD='{1}'AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLNM+CTRLC1 IN (Select WERKS+LGORT from WHAUT WITH(NOLOCK) where MANDT='{0}' and COMCD='{1}' and USRNM='{2}')",
                        MANDT, COMCD, UserData.UserId);
                    }
                    ControlHandleDB();
                    dtWerks = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtWerks;
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

            public DataTable InvLgort(string strWerks)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvLgort";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    StringBuilder sbSql = new StringBuilder();
                    DataTable dtLgort = new DataTable();
                    sbSql.AppendFormat(@"SELECT CTRLNM,CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT = '{0}' AND COMCD = '{1}' AND CTRLID='RTINV'AND CTRLC5=N'盘点对比' INTERSECT
                                         SELECT WHCTRL.CTRLNM,WHAUT.LGORT as CTRLC1 FROM WHAUT WITH(NOLOCK) inner join WHCTRL WITH(NOLOCK) on WHAUT.MANDT=WHCTRL.MANDT and WHAUT.COMCD=WHCTRL.COMCD and WHCTRL.SOLDTO='QWMS' and WHCTRL.CTRLID='LGORT' and WHCTRL.CTRLC2=WHAUT.LGORT and WHCTRL.CTRLNM=WHAUT.WERKS AND WHAUT.MANDT='{0}'AND WHAUT.COMCD='{1}' AND WHAUT.USRNM='{2}'",
                                       MANDT, COMCD, UserData.UserId);
                    if (strWerks != "")
                    {
                        sbSql.AppendFormat(" AND WHAUT.WERKS='" + strWerks + "' ");
                    }                     
                    ControlHandleDB();
                    dtLgort = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtLgort;
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

            //查询盘点票
            public DataTable ShowInv(string strStartDate, string strEndDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ShowInv";
                this.ControlMethodParm = "('" + strStartDate + "," + strEndDate + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT DISTINCT INVNO FROM IVHED WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND CRTIM BETWEEN '" + strStartDate + " 00:00:00.000' AND '" + strEndDate + " 23:59:59.999'");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            //显示dtError
            public DataTable LocatInfo( string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "LocatInfo";
                this.ControlMethodParm = "('" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,DACOD,SUM(MENGE) AS MENGE,''AS SCDAC,'' AS SCQTY,'' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY LOCAT,MATNR,DACOD");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            #region  非管控仓实物盘点储位明细查询
            public DataTable UnDCLocatData(string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UnDCLocatData";
                this.ControlMethodParm = "('" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,SUM(MENGE) AS MENGE,'' AS DACOD,''AS SCDAC,'' AS SCQTY,'' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY MATNR,LOCAT");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            public DataTable LocatInfoPCB(string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "LocatInfo";
                this.ControlMethodParm = "('" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,CHARG,DACOD,SUM(MENGE) AS MENGE,''AS SCDAC,'' AS SCQTY,'' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY LOCAT,MATNR,CHARG,DACOD");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            #region  非管控仓实物盘点储位明细查询
            public DataTable UnDCLocatDataPCB(string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UnDCLocatData";
                this.ControlMethodParm = "('" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,CHARG,SUM(MENGE) AS MENGE,'' AS DACOD,''AS SCDAC,'' AS SCQTY,'' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY MATNR,LOCAT,CHARG");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            //DID数量
            public DataTable DIDMenge( string strLocat)
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
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,MBLNR,SUM(MENGE) AS didMENGE FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY LOCAT,MATNR,MBLNR");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            #region 查询DID
            public DataTable Query_WHRID(string strDIDNO)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryIQC_WHRID";
                this.ControlMethodParm = " '" + strDIDNO + "' ";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    StringBuilder sbSQL = new StringBuilder();

                    if (COMCD == "9200")
                    {
                        sbSQL.AppendFormat(@"SELECT MATNR,DACOD,LIFNR,LOCOD,MENGE FROM WHRID WHERE DIDNO = '{0}'
                                            union
                                            SELECT MATNR,DACOD,LIFNR,LOCOD,MENGE FROM [qwmsbak].[dbo].[WHRID_2020] WHERE DIDNO = '{0}'", strDIDNO);
                    }
                    else if (COMCD == "9100")
                    {
                        sbSQL.AppendFormat(@"SELECT MATNR,DACOD,LIFNR,LOCOD,MENGE FROM WHRID WHERE DIDNO = '{0}'
                                            union
                                            SELECT MATNR,DACOD,LIFNR,LOCOD,MENGE FROM [qwmsbak].[dbo].[WHRID9100_2020] WHERE DIDNO = '{0}'", strDIDNO);
                    }
                    else
                    {
                        sbSQL.AppendFormat(@"SELECT MATNR,DACOD,LIFNR,LOCOD,MENGE FROM WHRID WHERE DIDNO = '{0}'", strDIDNO);
                    }

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

            //获取盘点票号对应的储位
            public DataTable getLocat(string strInvNo)
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
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT DISTINCT LOCAT FROM IVITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            //获得盘点票号总笔数
            public int InvITMCount(string strInvNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvITMCount";
                this.ControlMethodParm = "('" + strInvNo + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT COUNT(*) AS Menge FROM WHITM WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT IN (SELECT  DISTINCT(LOCAT) FROM IVITM WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INVNO='" + strInvNo + "')");
                    
                 
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    int itmCount = Convert.ToInt32(dtData.Rows[0]["Menge"]);
                    ControlSqlAccess.CloseConnection();
                    return itmCount;
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

            //查询料号在该仓的储位
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
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MENGE FROM WHITM WITH(NOLOCK) WHERE");
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

            //查询DID在该仓的储位
            public DataTable getLocatByDID(string strDID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getLocatByDID";
                this.ControlMethodParm = "('" + strDID + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MENGE FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND MBLNR='" + strDID + "'");
                    //strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            #region 更新盘点数据

            //更新IVITM和IVHED
            public bool InventoryLoad(int type, string strInvNo, string setsLocat, DataTable dtData,string strType)
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
                                string.Format(@"DELETE FROM IVITM WHERE MANDT='{0}'AND COMCD='{1}' AND WERKS='{2}' AND LGORT='{3}' AND INVNO='{5}' AND LOCAT = '{4}'", MANDT, COMCD, WERKS, LGORT, setsLocat, strInvNo);
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

            //更新WHLOG
            public bool InvLogLoad(string setsLocat, string strInvNo, DataTable dtData,string strType)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvLogLoad";
                this.ControlMethodParm = "('" + setsLocat + "','" + strInvNo + "','" + dtData + "','" + strType + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtCloneLog = new DataTable();
                    bool boolFlag = false;
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);
                    ControlHandleDB();
                    string CopyLogSQL;
                    DataTable dtCopyLog = new DataTable();
                    if (setsLocat != "")
                    { 
                        CopyLogSQL =
                            string.Format(@"SELECT TOP 1 LOGID,MANDT,WERKS,LGORT,CGCLS,OLOCA,NLOCA,MATNR,CHARG,LIFNR,TRNTP,MBLNR,OMBLN,EBELN,MENGE,INSMK,KOSTL ,ARBPL ,MRGID ,INDAT ,CRNAM ,CRDAT ,RMAK1 ,RMAK2,RMAK3 ,SERNO,COMCD ,LOCOD,INSPT ,DACOD ,VEDAT ,BOXID ,KDMAT ,GRPID ,QWQTY ,RMANO  FROM WHLOG WITH (NOLOCK)");
                        dtCopyLog = ControlSqlAccess.GetDataTable(CopyLogSQL);
                        dtCloneLog = dtCopyLog.Clone();
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                                DataRow dr = dtCloneLog.NewRow();
                                dr["MANDT"] = MANDT;
                                dr["COMCD"] = COMCD;
                                dr["WERKS"] = WERKS;
                                dr["LGORT"] = LGORT;
                                dr["CGCLS"] = "27";
                                dr["MBLNR"] = strInvNo;
                                dr["TRNTP"] = dtData.Rows[i]["STATS"].ToString();
                                dr["OLOCA"] = dtData.Rows[i]["LOCAT"].ToString();
                                dr["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                                if (strType == "BoxID")
                                {
                                    dr["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                                }
                                dr["QWQTY"] = Convert.ToInt32(dtData.Rows[i]["MENGE"]);
                                dr["MENGE"] = Convert.ToInt32(dtData.Rows[i]["SCQTY"]);
                                dr["RMAK1"] = dtData.Rows[i]["RMARK"].ToString();
                                dr["SERNO"] = "";
                                dr["CRDAT"] = DateTime.Now.ToLocalTime().ToString();
                                dr["CRNAM"] = UserData.UserId;
                                dtCloneLog.Rows.Add(dr);
                        }
                        if (dtCloneLog.Rows.Count == 0)
                        {
                            DataRow dr = dtCloneLog.NewRow();
                            dr["MANDT"] = MANDT;
                            dr["COMCD"] = COMCD;
                            dr["WERKS"] = WERKS;
                            dr["LGORT"] = LGORT;
                            dr["CGCLS"] = "27";
                            dr["MBLNR"] = strInvNo;
                            dr["TRNTP"] = "Y";
                            dr["OLOCA"] = setsLocat;
                            dr["MATNR"] = "";
                            dr["QWQTY"] = 0;
                            dr["MENGE"] = 0;
                            dr["RMAK1"] = "";
                            dr["SERNO"] = "";
                            dr["CRDAT"] = DateTime.Now.ToLocalTime().ToString();
                            dr["CRNAM"] = UserData.UserId;
                            dtCloneLog.Rows.Add(dr);
                        }
                        boolFlag = ControlSqlAccess.ExecSqlBulkCopy("WHLOG", dtCloneLog);
                        ControlSqlAccess.CloseConnection();
                    }
                    return boolFlag;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 处理D/C异常
            public  DataTable InvDateCodeDT(string strInvNo, string strlocat, string strmatnr)
            {
                this.ControlMethodName = "InvDateCodeDT";
                this.ControlMethodParm = "('" + strInvNo + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try 
                {
                    # region Code here
                    DataTable dtRemark = new DataTable();                   
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT RMARK FROM IVITM WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strlocat + "'");
                    strSQL.AppendFormat(" AND MATNR='" + strmatnr + "'");
                    strSQL.AppendFormat(" AND  RMARK LIKE N'%DateCode不一致%'");                  
                    ControlHandleDB();
                    dtRemark = ControlSqlAccess.GetDataTable(strSQL.ToString());                
                    ControlSqlAccess.CloseConnection();
                    return dtRemark;
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

            //插入异常WHLOG,更新IVITM
            public bool InvLogEx(DataTable dtData)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvLogEx";
                this.ControlMethodParm = "('" + dtData + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtCloneLog = new DataTable();
                    bool boolFlag = false;
                    bool flag = false;
                    DataWhcyc objDataWhcyc = new DataWhcyc(UserData);
                    ControlHandleDB();
                    string CopyLogSQL;
                    DataTable dtCopyLog = new DataTable();
                    string exIvitmSQL;
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        exIvitmSQL =
                            string.Format(@"UPDATE IVITM SET MGDIF='{0}',RMARK=N'{1}' WHERE MANDT='{2}' AND COMCD='{3}' AND WERKS='{4}' AND LGORT='{5}' AND LOCAT='{6}' AND INVNO='{7}' AND MATNR='{8}'",
                            dtData.Rows[i]["MGDIF"].ToString(), dtData.Rows[i]["RMARK"].ToString(), MANDT, COMCD, WERKS, LGORT,dtData.Rows[i]["LOCAT"].ToString(),dtData.Rows[i]["INVNO"].ToString(),dtData.Rows[i]["MATNR"].ToString().Trim());
                        flag = ControlSqlAccess.ExecSql(exIvitmSQL);
                    }
                    if (flag)
                    {
                        CopyLogSQL =
                            string.Format(@"SELECT TOP 1 LOGID,MANDT,WERKS,LGORT,CGCLS,OLOCA,NLOCA,MATNR,CHARG,LIFNR,TRNTP,MBLNR,OMBLN,EBELN,MENGE,INSMK,KOSTL ,ARBPL ,MRGID ,INDAT ,CRNAM ,CRDAT ,RMAK1 ,RMAK2,RMAK3 ,SERNO,COMCD ,LOCOD,INSPT ,DACOD ,VEDAT ,BOXID ,KDMAT ,GRPID ,QWQTY ,RMANO  FROM WHLOG WITH (NOLOCK)");
                        dtCopyLog = ControlSqlAccess.GetDataTable(CopyLogSQL);
                        dtCloneLog = dtCopyLog.Clone();
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            DataRow dr = dtCloneLog.NewRow();
                            dr["MANDT"] = MANDT;
                            dr["COMCD"] = COMCD;
                            dr["WERKS"] = WERKS;
                            dr["LGORT"] = LGORT;
                            dr["CGCLS"] = "27";
                            dr["MBLNR"] = dtData.Rows[i]["INVNO"].ToString();
                            dr["TRNTP"] = dtData.Rows[i]["STATS"].ToString();
                            dr["OLOCA"] = dtData.Rows[i]["LOCAT"].ToString();
                            dr["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                            dr["QWQTY"] = Convert.ToInt32(dtData.Rows[i]["MENGE"]);
                            dr["MENGE"] = Convert.ToInt32(dtData.Rows[i]["SCMENGE"]);
                            dr["RMAK1"] = dtData.Rows[i]["RMARK"].ToString();
                            dr["SERNO"] = "";
                            dr["CRDAT"] = DateTime.Now.ToLocalTime().ToString();
                            dr["CRNAM"] = UserData.UserId;
                            dtCloneLog.Rows.Add(dr);
                        }
                        boolFlag = ControlSqlAccess.ExecSqlBulkCopy("WHLOG", dtCloneLog);
                    }
                    ControlSqlAccess.CloseConnection();
                    return boolFlag;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 盘点进度页面相关查询

            #region 盘点进度表格
            public DataTable InvProgressDT(string strInvNo, string strStartDate, string strEndDate)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvProgressDT";
                this.ControlMethodParm = "('" + strInvNo + "," + strStartDate + "," + strEndDate + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    DataTable dtProgress = new DataTable();                   
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT I.WERKS,I.LGORT,IH.STATM,IH.ENDTM,I.INVNO,COUNT(DISTINCT I.LOCAT) AS CountInvLocat,INV.INVLOCAT AS InvLocat,COUNT(DISTINCT I.LOCAT)-INV.INVLOCAT AS NoInvLocat,CONVERT(VARCHAR, CONVERT(int, CONVERT(DECIMAL(18, 2), INVlOCAT)*100/ CONVERT(DECIMAL(18, 2),COUNT(DISTINCT I.LOCAT))))+'%' AS Progress ,'' AS CRWHO FROM (IVITM I LEFT JOIN (SELECT INVNO,COUNT(DISTINCT LOCAT) AS INVlOCAT FROM IVITM WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                     if (strInvNo != "")
                    {
                        strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    }
                    strSQL.AppendFormat(" AND STATS='Y'");
                    strSQL.AppendFormat(" AND CRTIM >='" + strStartDate + " 00:00:00.000' AND CRTIM <='" + strEndDate + " 23:59:59.999' ");
                    strSQL.AppendFormat(" GROUP BY INVNO) INV ON I.INVNO=INV.INVNO) LEFT JOIN IVHED IH ON I.INVNO=IH.INVNO WHERE");
                    strSQL.AppendFormat(" I.MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND I.COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND I.WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND I.LGORT='" + LGORT + "'");
                    if (strInvNo != "")
                    {
                        strSQL.AppendFormat(" AND I.INVNO='" + strInvNo + "'");
                    }
                    strSQL.AppendFormat(" AND I.CRTIM >='" + strStartDate + " 00:00:00.000' AND I.CRTIM <='" + strEndDate + " 23:59:59.999' ");
                    strSQL.AppendFormat(" GROUP BY I.WERKS,I.LGORT,IH.STATM,IH.ENDTM,I.INVNO,INVLOCAT");
               
                    DataTable dtCrwho = new DataTable();
                    StringBuilder strWho = new StringBuilder();
                    strWho.AppendFormat("SELECT INVNO,CRWHO FROM IVITM WITH(NOLOCK) WHERE");
                    strWho.AppendFormat(" MANDT='" + MANDT + "'");
                    strWho.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strWho.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strWho.AppendFormat(" AND LGORT='" + LGORT + "'");
                    if (strInvNo != "")
                    {
                        strWho.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    }
                    strWho.AppendFormat(" AND STATS='Y'");
                    strWho.AppendFormat(" AND CRTIM >='" + strStartDate + " 00:00:00.000' AND CRTIM <='" + strEndDate + " 23:59:59.999' ");                   
                    strWho.AppendFormat("GROUP BY INVNO,CRWHO");
                    ControlHandleDB();
                    dtProgress = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    dtCrwho = ControlSqlAccess.GetDataTable(strWho.ToString());
                    string strCrwho = "";
                    for (int i = 0; i < dtProgress.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtCrwho.Rows.Count; j++)
                        {
                            if ( dtCrwho.Rows[j]["INVNO"].ToString().Trim()==dtProgress.Rows[i]["INVNO"].ToString().Trim() )
                            {
                                strCrwho = strCrwho+ ","+dtCrwho.Rows[j]["CRWHO"];
                            }
                        }
                        if (strCrwho.Trim().Length > 1)
                        {
                            strCrwho = strCrwho.Substring(1);
                            dtProgress.Rows[i]["CRWHO"] = strCrwho;
                        }                       
                        strCrwho = "";
                    }
                    ControlSqlAccess.CloseConnection();
                    return dtProgress;
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

            #region  盘点票号盘点明细
            public DataTable InvContent(string strInvNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvContent";
                this.ControlMethodParm = "('" + strInvNo + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    #region code here
                    DataTable dtInvContent = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,CHARG,MENGE,MENGE-MGDIF AS SCMENGE,MGDIF,STATS,RMARK,CFTIM,CRWHO FROM IVITM WITH(NOlOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    strSQL.AppendFormat(" AND STATS='Y'");
                    strSQL.AppendFormat(" AND MGDIF=0 ");
                    ControlHandleDB();
                    dtInvContent= ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    //for (int i = 0; i < dtInvContent.Rows.Count; i++)
                    //{
                    //    if (dtInvContent.Rows[i]["MGDIF"].ToString().Trim() == "0" && dtInvContent.Rows[i]["RMARK"].ToString().Trim() == "未刷入数量")
                    //    {
                    //        dtInvContent.Rows[i]["SCMENGE"] = 0;
                    //    }
                    //}

                    return dtInvContent;
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

            #region  盘点票号未盘储位
            public DataTable NInvLocat(string strInvNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "NInvLocat";
                this.ControlMethodParm = "('" + strInvNo + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    #region code here
                    DataTable dtNInvLocat = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT DISTINCT LOCAT FROM IVITM WITH(NOlOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    strSQL.AppendFormat(" AND STATS='N'");
                    ControlHandleDB();
                    dtNInvLocat = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtNInvLocat;
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

            #region 盘点异常查询

            public DataTable InvNOResult(string strInvNo, string strStartDate, string strEndDate, string strFromLocat, string strToLocat, string strMatnr)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "InvNOResult";
                this.ControlMethodParm = "('" + strInvNo + "," + strStartDate + "," + strEndDate + "," + strFromLocat + "," + strToLocat + "," + strMatnr + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    #region code here
                    DataTable dtInvResult = new DataTable();
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT INVNO,LOCAT,MATNR,MENGE,MENGE-MGDIF AS SCMENGE,MGDIF,STATS,RMARK,CFTIM,CRWHO FROM IVITM WITH(NOlOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    if (strFromLocat != "" && strToLocat != "")
                    {
                        strSQL.AppendFormat(" AND LOCAT BETWEEN '" + strFromLocat + "' AND '" + strToLocat + "' ");
                    }
                    if (strFromLocat != "" && strToLocat == "")
                    {
                        strSQL.AppendFormat(" AND LOCAT = '" + strFromLocat + "'");
                    }
                    if (strFromLocat == "" && strToLocat != "")
                    {
                        strSQL.AppendFormat(" AND LOCAT = '" + strToLocat + "'");
                    }
                    if (strInvNo != "")
                    {
                        strSQL.AppendFormat(" AND INVNO='" + strInvNo + "'");
                    }
                    if (strMatnr != "")
                    {
                        strSQL.AppendFormat(" AND MATNR='" + strMatnr + "'");
                    }
                    strSQL.AppendFormat(" AND CRTIM >='" + strStartDate + " 00:00:00.000' AND CRTIM <='" + strEndDate + " 23:59:59.999' ");
                    strSQL.AppendFormat("AND (MGDIF<>0 OR RMARK like N'%DateCode不一致%') ");
                    strSQL.AppendFormat("ORDER BY INVNO,LOCAT,MATNR");

                    ControlHandleDB();
                    dtInvResult = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    //for (int i = 0; i < dtInvResult.Rows.Count; i++)
                    //{
                    //    if (dtInvResult.Rows[i]["RMARK"].ToString().Trim() == "未刷入数量")
                    //    {
                    //        dtInvResult.Rows[i]["SCMENGE"] = 0;
                    //    }
                    //}
                    return dtInvResult;
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
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT LOCAT,MATNR,CHARG,DACOD,SUM(MENGE) AS MENGE,'' AS SCQTY, '' AS MGDIF,'' AS STATS, '' AS RMARK FROM WHITM WITH(NOLOCK) WHERE");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY LOCAT,MATNR,CHARG,DACOD");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            #region 查询该储位以料号版本分组的sum(menge)
            /// <summary>
            /// 查询该储位以料号版本分组的sum(menge)
            /// </summary>
            /// <param name="strLocat">储位</param>
            /// <returns></returns>
            public DataTable QueryDetail(string strBoxid)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetail";
                this.ControlMethodParm = "('" + strBoxid + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT BOXID,MATNR,CHARG,SUM(MENGE) AS MENGE FROM PAL_DETAIL WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND BOXID='" + strBoxid + "'");
                    strSQL.AppendFormat("GROUP BY BOXID,MATNR,CHARG");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            #region 查询该储位以料号版本分组的sum(menge)
            /// <summary>
            /// 查询该储位以料号版本分组的sum(menge)
            /// </summary>
            /// <param name="strLocat">储位</param>
            /// <returns></returns>
            public DataTable QueryDetail_SN(string strBoxid, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetail";
                this.ControlMethodParm = "('" + strBoxid + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.AppendFormat("SELECT BOXID,MATNR,CHARG,SUM(MENGE) AS MENGE FROM WHBOX WITH(NOLOCK) WHERE ");
                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND BOXID='" + strBoxid + "'");
                    strSQL.AppendFormat(" AND LOCAT='" + strLocat + "'");
                    strSQL.AppendFormat("GROUP BY BOXID,MATNR,CHARG");
                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            #region 更新数据 UpdateDetail(strScdac, strVendorcode, strDacod)
            public bool UpdateDetail(string strScdac, string DC_After, string strDacod, string strMatnr, string strLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "UpdateDetail";
                this.ControlMethodParm = "('" + strScdac + "')";
                this.ControlMethodParm = "('" + DC_After + "')";
                this.ControlMethodParm = "('" + strDacod + "')";
                this.ControlMethodParm = "('" + strMatnr + "')";
                this.ControlMethodParm = "('" + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                 # region Code here
                    ControlHandleError("000", "", "");
                }
                    bool blResult=false;
                    string strsql = string.Empty;
                    strsql = "UPDATE WHITM SET DACOD='" + strScdac + "',VEDAT='" + DC_After + "' WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "'AND WERKS='" + WERKS + "'AND LGORT='" + LGORT + "' AND DACOD='" + strDacod + "' AND MATNR='" + strMatnr + "' AND LOCAT='"+strLocat+"'";
                    try
                    {
                        ControlHandleDB();
                        blResult = ControlSqlAccess.ExecSql(strsql);
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
                    return blResult;
                    # endregion
            }
            #endregion

            #region 二次扫描 查询更改后的DACOD
            public string SelectDetail(string strMatnr,string strLocat)
            {
                string strSQL = "";
                string strDacod = "";
                try
                {
                    strSQL = "SELECT DACOD FROM WHITM WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "'AND WERKS='" + WERKS + "'AND LGORT='" + LGORT + "'AND LOCAT='" + strLocat + "'  AND MATNR='" + strMatnr + "'";
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

            #region 更改后的VEDAT
            public string WHDCR_Query(string strScdac, string strVendorcode)
            {
                string strSQL = "";
                string DC_After = "";

                try
                {
                    strSQL = "select DC_After from WHDCR WITH(NOLOCK) WHERE DC_Before = '" + strScdac + "' AND LIFNR= '" + strVendorcode + "'";
                    ControlHandleDB();
                    DC_After = ControlSqlAccess.GetFieldValue(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return DC_After;
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


            #region 获取DateCode转换后的值
            /// <summary>
            /// 获取DateCode转换后的值
            /// </summary>
            /// <param name="strVendor">厂商代码</param>
            /// <param name="strDC_before">转换之前的DateCode</param>
            /// <returns></returns>
            public string getDCTrans(string strVendor, string strDC_before)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "getDCTrans";
                this.ControlMethodParm = "(" + strVendor + strDC_before + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strResult = string.Empty;
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("EXEC SP_DateCodeTransfer '{0}','{1}'", strVendor, strDC_before);
                try
                {
                    ControlHandleDB();
                    DataTable dtResult;
                    dtResult = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    if (dtResult.Rows[0]["FLG"].ToString ().Trim ()=="Y")
                    {
                        strResult = dtResult.Rows[0]["DC_After"].ToString().Trim();
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
                return strResult;
            }
            #endregion


            #region WHDCR(新增)
            public void WHDCR_DML(DataTable dt, string Type)
            {

                this.ControlMethodName = "WHDCR_DML";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T")
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    string strSQL = "";
                    string DC_Before = "";
                    string DC_After = "";
                    string LIFNR = "";
                    #region ADD NEW
                    if (Type == "NEW")
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DC_Before = dt.Rows[i][1].ToString();
                            DC_After = dt.Rows[i][2].ToString();
                            LIFNR = dt.Rows[i][0].ToString();
                            strSQL = strSQL + "INSERT INTO WHDCR (DC_Before, DC_After, LIFNR, USNAM, CRDAT, MODAT, COMCD) VALUES('" + DC_Before + "','" + DC_After + "','" + LIFNR + "','" + UserData.UserId + "','" + Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss") + "','" + Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss") + "','" + COMCD + "');";
                        }
                    }
                    #endregion
                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
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

            # region 查询库存数据 Add by Freeman
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 查询库存数据
            /// </summary> 
            /// <returns>
            /// DataTable
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  Counting objCounting =new QCI.QWMS.Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            ///  DataTable  dtData = objCounting.QueryDetailCountingData(strInsmk,strStartLocat,strEndLocat,strStartMatnr,strEndMatnr,strStartDate,strEndDate,strCharg,strMblnr,strIsmrg,strVendorCode,strRmano);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            public DataTable QueryStorageInDataBySMT(string strInsmk, string strFromLocat, string strToLocat)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryDetailCountingData";
                this.ControlMethodParm = "('" + strInsmk + "," + strFromLocat + "," + strToLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    StringBuilder strSQL = new StringBuilder();

                    strSQL.AppendFormat(
                        "SELECT MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,INSMK,SUM(MENGE) AS MENGE FROM WHITM WHERE");

                    strSQL.AppendFormat(" MANDT='" + MANDT + "'");
                    strSQL.AppendFormat(" AND COMCD='" + COMCD + "'");
                    strSQL.AppendFormat(" AND WERKS='" + WERKS + "'");
                    strSQL.AppendFormat(" AND LGORT='" + LGORT + "'");
                    strSQL.AppendFormat(" AND INSMK='" + strInsmk + "'");
                    if (strFromLocat != "" && strToLocat != "")
                    {
                        strSQL.AppendFormat(" AND LOCAT BETWEEN '" + strFromLocat + "' AND '" + strToLocat + "' ");
                    }
                    if (strFromLocat != "" && strToLocat == "")
                    {
                        strSQL.AppendFormat(" AND LOCAT = '" + strFromLocat + "'");
                    }
                    if (strFromLocat == "" && strToLocat != "")
                    {
                        strSQL.AppendFormat(" AND LOCAT = '" + strToLocat + "'");
                    }
                    strSQL.AppendFormat(" GROUP BY MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,INSMK");


                    DataTable dtData = new DataTable();
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    //加總之後數量為0的資料不顯示  Add by Smose Liao  20091228 
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MENGE"].ToString()) == 0 && int.Parse(dtData.Rows[i]["BKQTY"].ToString()) == 0)
                        {
                            dtData.Rows.Remove(dtData.Rows[i]);
                        }
                    }

                    return dtData;
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

            # region 查询仓别第一笔库存数据的库别 Add by Refun
            public string QueryStorageInsmk(string strWerks, string strLgort)
            {
                string strInsmk = "";
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryStorageInsmk";
                this.ControlMethodParm = "('" + strLgort + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = " SELECT TOP 1 INSMK FROM WHITM WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ";
                    ControlHandleDB();
                    strInsmk = ControlSqlAccess.GetFieldValue(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return strInsmk;
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

            # region 查询OA盘点票号 Add by Refun 不用
            public DataTable QueryCycNoFromOA(string strWerks, string strLgort, string strSubStorage)
            {
                DataTable dtData = new DataTable();
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCycNoFromOA";
                this.ControlMethodParm = "('" + strLgort + "," + strWerks + "," + strSubStorage + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = string.Empty;
                    if (string.IsNullOrEmpty(strSubStorage))
                    {
                        strSQL = @" SELECT TICKETFROM,TICKETTO ,CheckStatus ,PLANT AS WERKS,  Storage AS LGORT,subStorage,UID FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC]  WHERE PLANT='" + strWerks + "' AND STORAGE='" + strLgort + "'  AND CheckStatus='2'  "; // 
                    }
                    else
                    {
                        strSQL = @" SELECT TICKETFROM,TICKETTO,UID  FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC]  WHERE PLANT='" + strWerks + "' AND STORAGE='" + strLgort + "' AND SUBSTORAGE='" + strSubStorage + "' AND CheckStatus='2'  ";//AND CheckStatus='2'  
                    }
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            # region 记录压力测试的log
            public void ExecLog(string strWerks, string strLgort, string strKostl, int RowCount, string strType, string strErr, string strMethod, string strNow)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ExecLog";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = string.Empty;
                    if (strType == "Begin")
                    {
                        strSQL = "INSERT INTO CYCExecLog(WERKS,LGORT,Kostl,BeginTime,MTYPE) SELECT '" + strWerks + "','" + strLgort + "','" + strKostl + "','" + strNow + "','" + strMethod + "' ";
                    }
                    else if (strType == "OA")
                    {
                        strSQL = "UPDATE  CYCExecLog SET EndTime=GETDATE(),ERRMSG=N'" + strErr + "' , DataCount=" + RowCount + "   WHERE MTYPE='" + strMethod + "' AND  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND  BeginTime='" + strNow + "'";
                        if (strKostl != "")
                        {
                            strSQL += "  AND Kostl='" + strKostl + "' ";
                        }
                    }
                    else if (strType == "ERR")
                    {
                        strSQL = "UPDATE  CYCExecLog SET ERRMSG=N'" + strErr + "' , DataCount=" + RowCount + "   WHERE MTYPE='" + strMethod + "' AND  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND  BeginTime='" + strNow + "'";
                    }
                    else if (strType == "End" || strKostl != "")
                    {
                        strSQL = "UPDATE  CYCExecLog SET QWMSExecEnd=GETDATE(), ERRMSG=N'" + strErr + "'  , DataCount=" + RowCount + "   WHERE  MTYPE='" + strMethod + "' AND  BeginTime='" + strNow + "'  AND  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ";
                        if (strKostl != "")
                        {
                            strSQL += "  AND Kostl='" + strKostl + "' ";
                        }
                    }


                    //else
                    //{
                    //    strSQL = "UPDATE  CYCExecLog SET EndTime=GETDATE(), ERRMSG=N'" + strErr + "'   WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ";
                    //}

                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
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

            #region 从OA获取盘点的厂区仓别 不用
            public DataTable QueryCycFromOA(string strWerks, string strLgort, string strSubStorage)
            {
                DataTable dtData = new DataTable();
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCycFromOA";
                this.ControlMethodParm = "('" + strWerks + "," + strLgort + "," + strSubStorage + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = string.Empty;
                    if (string.IsNullOrEmpty(strLgort))
                    {
                        strSQL = @"  SELECT TICKETFROM,TICKETTO ,CheckStatus ,PLANT AS WERKS,  Storage AS LGORT,subStorage,UID FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC] WHERE CheckStatus='2' AND STORAGE !='AS10' AND PLANT='" + strWerks + "'";
                        //+ " WHERE CheckStatus='4'   AND STORAGE !='AS10'  AND PLANT IN (" + strWerks + ")"
                        //+ " ORDER BY Storage,subStorage "; //  AND STORAGE='" + strLgort + "'     WHERE PLANT IN ('CS20','CS30','CS31','CS32','CS41','CS42','CS50','CS51','CS52')  AND   AND subStorage  IN ( 'TW23')   


                        //SELECT  TICKETFROM,TICKETTO ,CheckStatus ,PLANT AS WERKS,  Storage AS LGORT,subStorage,UID 
                        //    FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC_TEST]   WHERE PLANT IN ('CS20','CS30','CS31','CS32','CS41','CS42','CS50','CS51','CS52')  AND CheckStatus IN ('2','3','4')  
                        //    ORDER BY Storage,subStorage

                    }
                    else if (string.IsNullOrEmpty(strSubStorage))
                    {
                        strSQL = @"SELECT TICKETFROM,TICKETTO ,CheckStatus ,PLANT AS WERKS,  Storage AS LGORT,subStorage,UID 
                           FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC] 
                           WHERE PLANT='" + strWerks + "' AND STORAGE IN ('" + strLgort + "')  AND CheckStatus='2' ";
                    }
                    else
                    {
                        strSQL = @" SELECT TICKETFROM,TICKETTO,UID 
                            FROM [qsmcoards.sqlserver.rds.aliyuncs.com,1433].[CommonDB].[dbo].[View_tdsICInventory_EC] 
                            WHERE PLANT='" + strWerks + "' AND STORAGE='" + strLgort + "' AND SUBSTORAGE='" + strSubStorage + "' AND CheckStatus='2'  ";//AND CheckStatus='2'  
                    }
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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
            #endregion

            /// <summary>
            /// 按厂区获取盘点资料
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strKostl"></param>
            /// <param name="dtCounting"></param>
            public DataTable QueryWHCYC(string strWerks)
            {
                //DataTable dtData = new DataTable();
                //string strSQL = " SELECT WERKS,STATS AS STATUS,CYCNO AS TICKET,INSMK AS BSTAR,LGORT,LOCAT,MATNR,MENGE AS BUCHM,CKQTY AS RIMENGE ,"
                //+ " CKDAT AS CUNT_DTE,CHARG,KOSTL AS DEPT_NO,'' AS  AREA_NO,'' AS DCODE,CRNAM AS SNAME  FROM WHCYC WHERE WERKS='" + strWerks + "' AND  INSMK='0' ";
                //dtData = GetDataTable(strSQL);
                //return dtData;
                try
                {

                    DataTable dtData = new DataTable();
                    string strSQL = "  exec [dbo].[SP_GetWHCYCData] '" + strMandt + "','" + strComcd + "','" + strWerks + "' ";
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

            public DataTable QueryWHCYC_Lgort(string strWerks, string strLgort)
            {
                //DataTable dtData = new DataTable();
                //string strSQL = " SELECT WERKS,STATS AS STATUS,CYCNO AS TICKET,INSMK AS BSTAR,LGORT,LOCAT,MATNR,MENGE AS BUCHM,CKQTY AS RIMENGE ,"
                //+ " CKDAT AS CUNT_DTE,CHARG,KOSTL AS DEPT_NO,'' AS  AREA_NO,'' AS DCODE,CRNAM AS SNAME  FROM WHCYC WHERE WERKS='" + strWerks + "' AND  INSMK='0' ";
                //dtData = GetDataTable(strSQL);
                //return dtData;
                try
                {

                    DataTable dtData = new DataTable();
                    string strSQL = "  exec [dbo].[SP_GetWHCYCData_Lgort] '" + strMandt + "','" + strComcd + "','" + strWerks + "','" + strLgort + "' ";
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

            #region 记录压力测试的log_CYC
            public void ExecLog_CYC(string strWerks, string strLgort, string strKostl, int RowCount, string strType, string strErr, string strMethod, string strNow)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ExecLog";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = string.Empty;
                    if (strType == "Begin")
                    {
                        strSQL = "INSERT INTO CYCExecLog(WERKS,LGORT,Kostl,BeginTime,MTYPE) SELECT '" + strWerks + "','" + strLgort + "','" + strKostl + "','" + strNow + "','" + strMethod + "' ";
                    }
                    else if (strType == "OA")
                    {
                        strSQL = "UPDATE  CYCExecLog SET EndTime=GETDATE(),ERRMSG=N'" + strErr + "' , DataCount=" + RowCount + "   WHERE MTYPE='" + strMethod + "' AND  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND  BeginTime='" + strNow + "'";
                        if (strKostl != "")
                        {
                            strSQL += "  AND Kostl='" + strKostl + "' ";
                        }
                    }
                    else if (strType == "End" || strKostl != "")
                    {
                        strSQL = "UPDATE  CYCExecLog SET QWMSExecEnd=GETDATE(), ERRMSG=N'" + strErr + "'  , DataCount=" + RowCount + "   WHERE  MTYPE='" + strMethod + "' AND  BeginTime='" + strNow + "'  AND  WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ";
                        if (strKostl != "")
                        {
                            strSQL += "  AND Kostl='" + strKostl + "' ";
                        }
                    }
                    ControlHandleDB();
                    ControlSqlAccess.ExecSql(strSQL);
                    ControlSqlAccess.CloseConnection();
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
            #endregion

            #region 从OA盘点管理系统获取盘点仓别

            public DataTable QueryCountingLgortFromOA(string Werks)
            {
                DataTable dtData = new DataTable();
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryCountingLgortFromOA";
                this.ControlMethodParm = "('" + Werks + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    string strSQL = string.Empty;
                    strSQL = @"SELECT distinct PLANT AS WERKS, 0 AS isChecked,  Storage AS LGORT
                           FROM [172.20.166.53].[CommonDB].[dbo].[View_tdsICInventory_EC] 
                           WHERE PLANT='" + Werks + "'  AND CheckStatus='2' ";
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL);
                    ControlSqlAccess.CloseConnection();
                    return dtData;
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

            #endregion

            #region 从WHCYC_OA中获取盘点数据
            public DataTable GetWhcycOA(string strWerks, string strLgort)
            {
                DataTable dtData = new DataTable();
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWhcycOA";
                this.ControlMethodParm = "('" + strWerks + "," + strLgort + " ')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT Plant AS WERKS,Storage AS LGORT,subStorage,TicketFrom,TicketTo,CheckStatus,UID FROM WHCYC_OA WITH(NOLOCK) WHERE Plant='{0}' ", strWerks);
                if(!strWerks.Equals("CS41"))
                    sbSql.AppendFormat("AND Storage!='AS10' ");
                sbSql.AppendFormat("AND CheckStatus='2' ");
                if (strLgort != "")
                {
                    sbSql.AppendFormat(" AND Storage='{0}'", strLgort);
                }               
                try
                {
                    # region Code here
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
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

                return dtData;
            }
            #endregion

            #region 从OA获取到的盘点数据插入到WHCYC_OA表中
            public bool insertWhcycOA(DataTable dtOA, string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetWhcycOA";
                this.ControlMethodParm = "('" + dtOA + ")";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    # region Code here
                    //string strSql = "DELETE FROM WHCYC_OA WHERE Plant='" + strWerks + "' AND Storage='" + strLgort + "'";
                    string strSql = "DELETE FROM WHCYC_OA WHERE Plant='" + strWerks + "'";
                    if (strLgort != "")
                    {
                        strSql = strSql + " AND Storage='" + strLgort + "'";
                    }
                    bool blResult = false;
                    ArrayList arySQL = new ArrayList();
                    ControlHandleDB();
                    bool bl = ControlSqlAccess.ExecSql(strSql);
                    if (bl)
                    {

                        for (int i = 0; i < dtOA.Rows.Count; i++)
                        {
                            StringBuilder strSql2 = new StringBuilder();
                            strSql2.AppendFormat(" INSERT INTO WHCYC_OA(Plant,Storage,subStorage,TicketFrom,TicketTo,CheckStatus,UID) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            dtOA.Rows[i]["Plant"], dtOA.Rows[i]["Storage"], dtOA.Rows[i]["subStorage"], dtOA.Rows[i]["TicketFrom"], dtOA.Rows[i]["TicketTo"], dtOA.Rows[i]["CheckStatus"], dtOA.Rows[i]["UID"]);
                            arySQL.Add(strSql2.ToString());
                        }
                        blResult = ControlSqlAccess.ExecSqlArray(arySQL);
                        //blResult = ControlSqlAccess.ExecSqlBulkCopy("WHCYC_OA", dtOA);
                    }
                    ControlSqlAccess.CloseConnection();
                    return blResult;
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
            #endregion

            #endregion
        }
    }
}
