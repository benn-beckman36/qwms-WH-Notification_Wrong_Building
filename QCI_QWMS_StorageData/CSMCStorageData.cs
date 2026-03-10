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
        /// StorageData 的摘要描述。
        /// </summary>
        public class CSMCStorageData : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strErrmsg = "";

            #region Constructer

            public CSMCStorageData()
            {
            }

            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Admin物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
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
            public CSMCStorageData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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

            public CSMCStorageData(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
            {
            }

            public CSMCStorageData(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "")
            {
            }

            public CSMCStorageData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
                : this(varDBType, varDBCode, varErrorType, varErrorCode, varUserData, "", "")
            {
            }

            #endregion

            #region DataMember

            UserInfo UserData = new UserInfo();

            public string MANDT
            {
                get { return strMandt; }
                set { strMandt = value; }
            }

            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
            }

            public string LGORT
            {
                get { return strLgort; }
                set { strLgort = value; }
            }

            public string WERKS
            {
                get { return strWerks; }
                set { strWerks = value; }
            }

            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }

            #endregion

            #region MemberFunction

            #region 依据SN/BOXID/储位/料号/库存状况等,查询相关信息功能
            public DataTable QueryStorageDataCSMC(string strWerks, string strLgort, string strSerno, string strBoxid, string strLocat, string strMatnr,string strBoxFlage,string strStatus)
            {
                StringBuilder strSQL = new StringBuilder();
                if (strStatus != "未入库")
                {
                    if (strBoxFlage == "") //没有勾选by Box Id 汇总
                    {
                        //strSQL.AppendFormat(" SELECT (CASE WHEN D.IOFLAGE='Y' OR I.LOCAT<>'' THEN N'在库' WHEN D.IOFLAGE='T' THEN N'已出库' ELSE N'未入库' END) AS STATU,I.LOCAT,B.CRTDAT,B.DELDAT,D.*  FROM WHDWN  AS D WITH (NOLOCK) LEFT JOIN WHBOX AS B  WITH (NOLOCK)  ON (D.MANDT=B.MANDT AND D.WERKS=B.WERKS AND D.LGORT=B.LGORT AND D.MBLNR=B.MBLNR AND D.MATNR=B.MATNR AND D.SERNO=B.SERNO AND D.BOXID=B.BOXID) INNER JOIN WHITM AS I  WITH (NOLOCK)  ON (D.MANDT=I.MANDT AND D.WERKS=I.WERKS AND D.LGORT=I.LGORT AND D.MATNR=I.MATNR AND D.INSMK=I.INSMK AND D.MBLNR=I.MBLNR) WHERE 1=1 AND D.MTYPE='QMS'  ");
                        //strSQL.AppendFormat(" SELECT  B.* ,N'在库' AS STATU,I.LOCAT  FROM WHBOX AS B  WITH (NOLOCK)  INNER JOIN WHITM AS I  WITH (NOLOCK)  ON (B.MANDT=I.MANDT AND B.WERKS=I.WERKS  AND B.LGORT=I.LGORT AND B.MATNR=I.MATNR AND B.INSMK=I.INSMK AND B.MBLNR=I.MBLNR) WHERE 1=1   ");

                        strSQL.AppendFormat(" SELECT DISTINCT  B.* ,N'在库' AS STATU,I.LOCAT  FROM WHBOX AS B  WITH (NOLOCK)  INNER JOIN WHITM AS I  WITH (NOLOCK)  ON (B.MANDT=I.MANDT AND B.WERKS=I.WERKS  AND B.LGORT=I.LGORT AND B.LOCAT = I.LOCAT AND B.MATNR=I.MATNR AND B.INSMK=I.INSMK) WHERE B.MANDT='" + MANDT + "' AND B.COMCD='" + COMCD + "' AND I.MBLNR='GB' "); //Add by Jason 20180428


                        if (strWerks != "")
                        {
                            strSQL.AppendFormat(" AND B.WERKS='{0}' ", strWerks);
                        }
                        if (strLgort != "")
                        {
                            strSQL.AppendFormat(" AND B.LGORT='{0}' ", strLgort);
                        }
                        if (strSerno != "")
                        {
                            strSQL.AppendFormat(" AND B.SERNO in('{0}') ", strSerno);
                        }
                        if (strBoxid != "")
                        {
                            strSQL.AppendFormat(" AND B.BOXID in('{0}')  ", strBoxid);
                        }
                        if (strLocat != "")
                        {
                            strSQL.AppendFormat(" AND B.LOCAT='{0}' ", strLocat);
                        }
                        if (strMatnr != "")
                        {
                            strSQL.AppendFormat(" AND B.MATNR='{0}' ", strMatnr);
                        }
                        //if (strStatus == "在库")
                        //{
                        //    strSQL.Append(" AND (D.IOFLAGE='Y' OR I.LOCAT<>'') ");
                        //}
                        //else if (strStatus == "已出库")
                        //{
                        //    strSQL.Append(" AND D.IOFLAGE='T' ");
                        //}
                        //else if (strStatus == "未入库")
                        //{
                        //    strSQL.Append(" AND  ISNULL(D.IOFLAGE,'')=''");
                        //}

                    }
                    else
                    {   //勾选by Box Id汇总
                        //strSQL.AppendFormat(" SELECT I.LOCAT,D.MTYPE,D.MBLNR,D.WERKS,D.LGORT,D.MATNR,D.INSMK,D.CHARG,D.LIFNR,D.BOXID, D.IOFLAGE,(CASE WHEN D.IOFLAGE='Y'  THEN N'在库' WHEN D.IOFLAGE='T' THEN N'已出库' ELSE N'未入库' END) AS STATU,I.LOCAT,D.MTYPE,D.MBLNR,D.WERKS,D.LGORT,D.MATNR,D.INSMK,D.CHARG,D.LIFNR,sum(D.MENGE) AS MENGE,SUM(D.OTQTY) AS OTQTY,D.BOXID FROM WHDWN AS D  WITH (NOLOCK) LEFT JOIN WHBOX AS B  WITH (NOLOCK) ON (D.MANDT=B.MANDT AND D.WERKS=B.WERKS AND D.LGORT=B.LGORT AND D.MBLNR=B.MBLNR AND D.MATNR=B.MATNR AND D.SERNO=B.SERNO AND D.BOXID=B.BOXID) INNER JOIN WHITM AS I  WITH (NOLOCK) ON (D.MANDT=I.MANDT AND D.WERKS=I.WERKS AND D.LGORT=I.LGORT AND D.MATNR=I.MATNR AND D.INSMK=I.INSMK AND D.MBLNR=I.MBLNR) WHERE 1=1 AND D.MTYPE='QMS'  ");
                        //strSQL.AppendFormat(" SELECT   I.LOCAT,'QMS' AS MTYPE,B.MBLNR,B.WERKS,B.LGORT,B.MATNR,B.INSMK,B.CHARG,B.BOXID, '' AS IOFLAGE, N'在库'  AS STATU,I.LOCAT,B.MBLNR,B.WERKS,B.LGORT,B.MATNR, B.INSMK,B.CHARG,'' AS LIFNR,sum(B.MENGE) AS MENGE,SUM(B.MENGE) AS OTQTY,B.BOXID  FROM WHBOX AS B  WITH (NOLOCK)   INNER JOIN  WHITM AS I  WITH (NOLOCK) ON (B.MANDT=I.MANDT AND B.WERKS=I.WERKS AND B.LGORT=I.LGORT AND B.MATNR=I.MATNR AND B.INSMK=I.INSMK AND B.MBLNR=I.MBLNR)  WHERE 1=1  ");

                        strSQL.AppendFormat(" SELECT   I.LOCAT,'QMS' AS MTYPE,B.MBLNR,B.WERKS,B.LGORT,B.MATNR,B.INSMK,B.CHARG,B.BOXID, '' AS IOFLAGE, N'在库'  AS STATU,I.LOCAT,B.MBLNR,B.WERKS,B.LGORT,B.MATNR, B.INSMK,B.CHARG,'' AS LIFNR,sum(B.MENGE) AS MENGE,SUM(B.MENGE) AS OTQTY,B.BOXID,B.CRTDAT FROM WHBOX AS B  WITH (NOLOCK)   INNER JOIN  (SELECT DISTINCT MANDT,COMCD,WERKS,LGORT,LOCAT,MBLNR,MATNR,CHARG,INSMK FROM WHITM WITH(NOLOCK)) AS I ON (B.MANDT=I.MANDT AND B.WERKS=I.WERKS AND B.LOCAT = I.LOCAT AND B.LGORT=I.LGORT AND B.MATNR=I.MATNR AND B.INSMK=I.INSMK )  WHERE B.MANDT='" + MANDT + "' AND B.COMCD='" + COMCD + "' AND I.MBLNR='GB'  "); //Modife by Jason 20180503


                        if (strWerks != "")
                        {
                            strSQL.AppendFormat(" AND B.WERKS='{0}' ", strWerks);
                        }
                        if (strLgort != "")
                        {
                            strSQL.AppendFormat(" AND B.LGORT='{0}' ", strLgort);
                        }

                        if (strBoxid != "")
                        {
                            strSQL.AppendFormat(" AND B.BOXID in('{0}')  ", strBoxid);
                        }
                        if (strLocat != "")
                        {
                            strSQL.AppendFormat(" AND B.LOCAT='{0}' ", strLocat);
                        }
                        if (strMatnr != "")
                        {
                            strSQL.AppendFormat(" AND B.MATNR='{0}' ", strMatnr);
                        }
                        //if (strStatus == "在库")
                        //{
                        //    strSQL.Append(" AND (D.IOFLAGE='Y' OR I.LOCAT<>'') ");
                        //}
                        //else if (strStatus == "已出库")
                        //{
                        //    strSQL.Append(" AND D.IOFLAGE='T' ");
                        //}
                        //else if (strStatus == "未入库")
                        //{
                        //    strSQL.Append(" AND  ISNULL(D.IOFLAGE,'')=''");
                        //}

                        //strSQL.Append(" GROUP BY B.LOCAT,D.MTYPE,D.MBLNR,D.WERKS,D.LGORT,D.MATNR,D.INSMK,D.CHARG,D.LIFNR,D.BOXID, D.IOFLAGE,I.LOCAT ");
                        strSQL.Append("  GROUP BY B.LOCAT,B.MBLNR,B.WERKS,B.LGORT,B.MATNR,B.INSMK,B.CHARG,B.BOXID, I.LOCAT,B.CRTDAT ");
                    }
                }
                else
                {   //选择"未入库"
                    //strSQL.AppendFormat("   SELECT * ,N'未入库' AS STATU,'' AS LOCAT FROM WHDWN WHERE 1=1 AND OTQTY=0 ");
                    strSQL.AppendFormat("   SELECT * ,N'未入库' AS STATU,'' AS LOCAT,N'未入库' AS CRTDAT FROM WHDWN WITH(NOLOCK) WHERE MANDT ='218' AND MTYPE IN ('QMS','QMS_311') AND OTQTY=0 "); //Modife by Jason 20180503


                    if (strWerks != "")
                    {
                        strSQL.AppendFormat(" AND WERKS='{0}' ", strWerks);
                    }
                    if (strLgort != "")
                    {
                        strSQL.AppendFormat(" AND LGORT='{0}' ", strLgort);
                    }
                    if (strSerno != "")
                    {
                        strSQL.AppendFormat(" AND SERNO in('{0}')  ", strSerno);
                    }
                    if (strBoxid != "")
                    {
                        strSQL.AppendFormat(" AND BOXID in('{0}')  ", strBoxid);
                    }
                    //if (strLocat != "")
                    //{
                    //    strSQL.AppendFormat(" AND B.LOCAT='{0}' ", strLocat);
                    //}
                    if (strMatnr != "")
                    {
                        strSQL.AppendFormat(" AND MATNR='{0}' ", strMatnr);
                    }
                }
                

                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString().Trim());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryStorageDataCSMC()";
                }
                return dtData;
            }
            #endregion
            #region 正常出库备料信息查询 by Jack 20150507
            public DataTable QueryIsseuProgress(string strWerks, string strLgort, string strMblnr, string strMatnr, string strDtpFrom, string strDtpTo)
            {
                //StringBuilder sbLog = new StringBuilder();
                StringBuilder sbDwn = new StringBuilder();
                //DataTable dtLog = new DataTable();
                DataTable dtDwn = new DataTable();
                ////1.先查询WHLOG里面有无出库日志
                //sbLog.AppendFormat(" SELECT DISTINCT WERKS,LGORT,MBLNR,MATNR,MENGE,CRDAT FROM WHLOG WITH(NOLOCK) WHERE TRNTP LIKE 'G-' AND (LEFT(MATNR,4)='20JH' OR LEFT(MATNR,4)='2LJH') AND LEFT(MBLNR,2)='49' AND WERKS='{0}' AND LGORT='{1}' AND (CRDAT>'{2}' AND CRDAT<'{3}') ", strWerks, strLgort, strDtpFrom + " 00:00:00", strDtpTo + " 23:59:59");
                //if (!string.IsNullOrEmpty(strMblnr))
                //{
                //    sbLog.AppendFormat("AND MBLNR LIKE '{0}%'", strMblnr);
                //}
                //if (!string.IsNullOrEmpty(strMatnr))
                //{
                //    sbLog.AppendFormat("AND MATNR='{0}'", strMatnr);
                //}
                //try
                //{
                //    ControlHandleDB();
                //    dtLog = ControlSqlAccess.GetDataTable(sbLog.ToString().Trim());
                //    ControlSqlAccess.CloseConnection();
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception(ex.Message + "<--CSMCStorageData.QueryIsseuProgress().sbLog语句");
                //}

                ////2.若有此厂区、仓别、扣帐编号、料号、时间段的日志,则抓取出库时间与扣帐时间做比对                
                //if (dtLog.Rows.Count > 0)
                //{
                //    sbDwn.AppendFormat(" SELECT DISTINCT (CASE WHEN DATEDIFF(MINUTE,D.CRDAT,L.CRDAT)>120 THEN 'Red' WHEN DATEDIFF(MINUTE,D.CRDAT,L.CRDAT)>60 THEN 'Yellow' ELSE 'Green' END) AS FLAGE,D.WERKS,D.LGORT,D.MBLNR,D.MATNR,D.CRDAT SAPTM,L.MENGE,L.CRDAT OUTTM FROM (SELECT WERKS,LGORT,MBLNR,MATNR,CRDAT FROM WHDWN WITH(NOLOCK) WHERE MTYPE='SAP' AND (LEFT(MATNR,4)='20JH' OR LEFT(MATNR,4)='2LJH') AND LEFT(MBLNR,2)='49' AND WERKS='{0}' AND LGORT='{1}' AND (CRDAT>'{2}' AND CRDAT<'{3}')) AS D INNER JOIN (SELECT WERKS,LGORT,MBLNR,MATNR,MENGE,CRDAT FROM WHLOG WITH(NOLOCK) WHERE (TRNTP LIKE 'G-' AND LEFT(MATNR,4)='20JH' OR LEFT(MATNR,4)='2LJH') AND LEFT(MBLNR,2)='49' AND WERKS='{0}' AND LGORT='{1}') AS L ON D.WERKS=L.WERKS AND D.LGORT=L.LGORT ", strWerks, strLgort, strDtpFrom + " 00:00:00",strDtpTo+" 23:59:59");
                //    //AND D.MBLNR=L.MBLNR AND D.MATNR=L.MATNR 
                //    if (!string.IsNullOrEmpty(strMblnr))
                //    {
                //        sbDwn.AppendFormat(" AND D.MBLNR=L.MBLNR AND L.MBLNR='{0}%' ", strMblnr);
                //    }
                //    if (!string.IsNullOrEmpty(strMatnr))
                //    {
                //        sbDwn.AppendFormat(" AND D.MATNR=L.MATNR AND L.MATNR='{0}' ", strMatnr);
                //    }
                //    sbDwn.AppendFormat(" ORDER BY L.CRDAT DESC ");

                //}
                ////3.若无此厂区、仓别、扣帐编号、料号、时间段的日志,则抓取WHDWN表的数据与当前时间做比对
                //if (dtLog.Rows.Count <= 0)
                //{
                //    if (!string.IsNullOrEmpty(strMblnr))
                //    {                    
                //        sbDwn.AppendFormat("SELECT DISTINCT (CASE WHEN DATEDIFF(MINUTE,CRDAT,GETDATE())>120 THEN 'Red' when DATEDIFF(MINUTE,CRDAT,GETDATE())<60 THEN 'Green' ELSE 'Yellow' END) AS FLAGE,WERKS,LGORT,MBLNR,MATNR,CRDAT SAPTM,MENGE,'' AS OUTTM FROM WHDWN WITH(NOLOCK) WHERE MTYPE='SAP' AND MENGE<>OTQTY AND WERKS='{0}' AND LGORT='{1}' (CRDAT>'{2}' AND CRDAT<'{3}') ", strWerks, strLgort, strDtpFrom + " 00:00:00", strDtpTo + " 23:59:59");
                //        sbDwn.AppendFormat(" AND MBLNR LIKE '{0}%' ", strMblnr);
                //        if (!string.IsNullOrEmpty(strMatnr))
                //        {
                //            sbDwn.AppendFormat(" AND MATNR='{0}' ", strMatnr);
                //        }
                //        sbDwn.AppendFormat(" ORDER BY CRDAT DESC ");
                //    }
                //    return dtDwn;
                //}
                sbDwn.AppendFormat(" SELECT TOP 1000 (CASE WHEN (MENGE<>OTQTY AND DATEDIFF(MINUTE,CRDAT,MODAT)>120) OR (MENGE<>OTQTY AND CRDAT=MODAT AND DATEDIFF(MINUTE,CRDAT,GETDATE())>120) THEN 'RED' WHEN (MENGE<>OTQTY AND DATEDIFF(MINUTE,CRDAT,MODAT)>60) OR (MENGE<>OTQTY AND CRDAT=MODAT AND DATEDIFF(MINUTE,CRDAT,GETDATE())>60) THEN 'YELLOW' ELSE 'GREEN' END) FLAGE,WERKS,LGORT,TRNTP,MBLNR,MATNR,MENGE,OTQTY,CRDAT SAPTM,MODAT OUTTM,DATEDIFF(MINUTE,CRDAT,MODAT) AS PRETM FROM WHDWN WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND MTYPE='SAP' AND TRNTP LIKE '%-' AND WERKS='{0}' AND LGORT='{1}' AND (CRDAT>'{2}' AND CRDAT<'{3}') AND COMCD='" + COMCD + "' ", strWerks, strLgort, strDtpFrom + " 00:00:00", strDtpTo + " 23:59:59");
                if (!string.IsNullOrEmpty(strMblnr))
                {
                    sbDwn.AppendFormat(" AND MBLNR LIKE '{0}%' ", strMblnr);
                }
                if (!string.IsNullOrEmpty(strMatnr))
                {
                    sbDwn.AppendFormat(" AND MATNR='{0}' ", strMatnr);
                }
                sbDwn.AppendFormat(" AND COMCD='" + COMCD + "' ");
                sbDwn.AppendFormat(" ORDER BY CRDAT DESC ");
                try
                {
                    ControlHandleDB();
                    dtDwn = ControlSqlAccess.GetDataTable(sbDwn.ToString().Trim());
                    ControlSqlAccess.CloseConnection();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "<--CSMCStorageData.QueryIsseuProgress().sbDwn语句");
                }
                return dtDwn;
            }

            #endregion
            #region 查询库存数量与SN数目对比报表  // Add by Barry 20171010 for [GB]储位库存/SN差异报表
            public DataTable QueryInventoryContrastbyStorageLocation(string strWerks, string strLgort, string strLocat)
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.AppendFormat("DECLARE @WERKS VARCHAR(5);DECLARE @LGORT UDT_LGORT;SET @WERKS='{0}' ;INSERT INTO @LGORT VALUES('{1}') ;EXEC usp_inv_diff_report_new @WERKS, @LGORT", strWerks, strLgort);
                //strSQL.AppendFormat("DECLARE @WERKS VARCHAR(5);DECLARE @LGORT UDT_LGORT;SET @WERKS='{0}' ;INSERT INTO @LGORT VALUES('{1}') ;EXEC usp_inv_diff_report @WERKS, @LGORT", strWerks, strLgort);
                DataTable dtData = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString().Trim());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryInventoryContrastbyStorageLocation()";
                }
                return dtData;
            }
            #endregion 


            #endregion
        }
    }
}
