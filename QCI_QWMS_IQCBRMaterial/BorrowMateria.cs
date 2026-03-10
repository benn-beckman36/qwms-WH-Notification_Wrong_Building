using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using QWMS.Common;
using Qci.Base.Common;
using QWMS.Entity;
using System.Data;
using System.Collections;

namespace QCI
{
    namespace QWMS
    {
        public class BorrowMateria : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strCrnam = "";
            private string strProgid = "";
            private string strErrmsg = "";

            #region Constructer

            public BorrowMateria(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {

            }

            public BorrowMateria(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
            {

                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                CRNAM = varUserData.UserId;
                PROGID = strProgid;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.BorrowMateria";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Hannah Liu";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            #endregion

            #region DataMember

            UserInfo UserData = new UserInfo();

            /// <summary>
            /// SAP Client
            /// </summary>
            public string MANDT
            {
                get { return strMandt; }
                set { strMandt = value; }
            }
            /// <summary>
            ///Cmpany Code
            /// </summary>
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
            }
            /// <summary>
            /// 建立者。
            /// </summary>
            public string CRNAM
            {
                get { return strCrnam; }
                set { strCrnam = value; }
            }
            /// <summary>
            /// 程式代碼。
            /// </summary>
            public string PROGID
            {
                get { return strProgid; }
                set { strProgid = value; }
            }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }


            #endregion

            #region MemberFunction

            #region 获取有扣账编号
            public DataTable GetMblnrInfo(string strWerks, string strLgort)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMblnrInfo";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendFormat(@"select distinct MBLNR from [dbo].[WHDWN] with(nolock) where MANDT='218' and MTYPE='SAP' and INSMK='0'and MENGE>OTQTY and WERKS='{0}'", strWerks);
                    if (!string.IsNullOrEmpty(strLgort))
                    {
                        sbSQL.AppendFormat(" and LGORT='{0}'", strLgort);
                    }

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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

            #region 获取SAP料号信息
            public DataTable QueryMaterialInfo(string strWerks, string strLgort, string strPN, string strMBLNR)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMaterialInfo";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strPN + "','" + strMBLNR + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append("select row_number() over (order by BUDAT) as Item,BUDAT,MATNR,MBLNR,WERKS,LGORT,GRLOC,MENGE,OTQTY,'0' AS BorrowQTY from [dbo].[WHDWN] with(nolock) where 1=1 and MANDT='218' and MTYPE='SAP' and INSMK='0'and MENGE>OTQTY");
                    if (!string.IsNullOrEmpty(strWerks))
                    {
                        sbSQL.AppendFormat(" and WERKS='{0}'", strWerks);
                    }
                    if (!string.IsNullOrEmpty(strLgort))
                    {
                        sbSQL.AppendFormat(" and LGORT='{0}'", strLgort);
                    }
                    if (!string.IsNullOrEmpty(strPN))
                    {
                        sbSQL.AppendFormat(" and MATNR='{0}'", strPN);
                    }
                    if (!string.IsNullOrEmpty(strMBLNR))
                    {
                        sbSQL.AppendFormat("  and MBLNR='{0}'", strMBLNR);
                    }
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

            //#region 获取HR信息
            ///// <summary>
            ///// 通过工号获取HR信息
            ///// </summary>
            ///// <param name="varUserID">工号</param>
            ///// <remarks>Mike Deng 20160426</remarks>
            ///// <returns>str</returns>
            //public DataTable GetHRData(string varUserID)
            //{
            //    //設定要記錄Error Message的相關訊息
            //    this.ControlMethodName = "GetHRData";
            //    this.ControlMethodParm = "('" + varUserID + "')";
            //    if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            //    {
            //        ControlHandleError("000", "", "");
            //    }

            //    try
            //    {
            //        string varSite = "";
            //        DataTable dt = new DataTable();
            //        DataSet ds = new DataSet();
            //        QCI_QWMS_IQCBRMaterial.OAHRInfo.QueryEmployeeData objHR = new QCI_QWMS_IQCBRMaterial.OAHRInfo.QueryEmployeeData();
            //        switch (UserData.CompanyCode)
            //        {
            //            case "9110":
            //                varSite = "QCMC";
            //                break;
            //            case "9210":
            //                varSite = "QCMC";
            //                break;
            //            case "9700":
            //                varSite = "CSMC";
            //                break;
            //            default:
            //                varSite = "QSMC";
            //                break;
            //        }
            //        //带出员工信息
            //        ds = objHR.GetEmployeeDataByCardNoAndSite(varSite, varUserID);
            //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //        {
            //            dt = ds.Tables[0];
            //        }
            //        return dt;
            //    }
            //    catch (CommonObjectsException ex)
            //    {
            //        //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
            //        ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
            //        ControlExceptionType = ex.SourceExceptionType;
            //        this.ControlPriority = "1";
            //        ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
            //        throw ex;
            //    }//可自行增加要handle的Exception  
            //    catch (Exception ex)
            //    {
            //        ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
            //        ControlExceptionType = ex.GetType().FullName;
            //        this.ControlPriority = "1";
            //        ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
            //        throw new Exception("999");
            //    }
            //}
            //#endregion

            #region 获取最新已借数量信息
            public string GetNewBorrowedQTY(string strWerks, string strLgort, string strPN, string strMBLNR)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetNewBorrowedQTY";
                this.ControlMethodParm = "('" + strWerks + "','" + strLgort + "','" + strPN + "','" + strMBLNR + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append("select OTQTY from [dbo].[WHDWN] with(nolock) where 1=1 and MANDT='218' and MTYPE='SAP' and INSMK='0'");
                    if (!string.IsNullOrEmpty(strWerks))
                    {
                        sbSQL.AppendFormat(" and WERKS='{0}'", strWerks);
                    }
                    if (!string.IsNullOrEmpty(strLgort))
                    {
                        sbSQL.AppendFormat(" and LGORT='{0}'", strLgort);
                    }
                    if (!string.IsNullOrEmpty(strPN))
                    {
                        sbSQL.AppendFormat(" and MATNR='{0}'", strPN);
                    }
                    if (!string.IsNullOrEmpty(strMBLNR))
                    {
                        sbSQL.AppendFormat("  and MBLNR='{0}'", strMBLNR);
                    }
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData.Rows[0][0].ToString().Trim();
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 借料功能
            public string BorrowMaterial(List<DataRow> lstTask, string strBorrowIQCID, string strBorrowWHID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "BorrowMaterial";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    ArrayList arlSql = new ArrayList();
                    string varBrNo = "";

                    for (int i = 0; i < lstTask.Count; i++)
                    {
                        string strBorrowNo = GetBorrowNo();
                        string strMBLNR = lstTask[i]["MBLNR"].ToString().Trim();
                        string strMATNR = lstTask[i]["MATNR"].ToString().Trim();
                        string strAccountDAT = lstTask[i]["BUDAT"].ToString().Trim();
                        string strPlant = lstTask[i]["WERKS"].ToString().Trim();
                        string strStorage = lstTask[i]["LGORT"].ToString().Trim();
                        string strLocationID = lstTask[i]["GRLOC"].ToString().Trim();
                        string strMENGE = lstTask[i]["MENGE"].ToString().Trim();
                        string strBorrowedQTY = lstTask[i]["OTQTY"].ToString().Trim();
                        string strBorrowQTY = lstTask[i]["BorrowQTY"].ToString().Trim();

                        if (varBrNo == "")
                        {
                            varBrNo = strBorrowNo;
                        }
                        else
                        {
                            varBrNo = varBrNo + "," + strBorrowNo;
                        }

                        arlSql.Add("insert into WHBM(MBLNR,BorrowNo,MATNR,AccountDAT,Plant,Storage,LocationID,MENGE,BorrowedQTY,BorrowQTY,BorrowTime,BorrowIQCID,BorrowWHID,CreateName,CreateTime)" +
                                 "values('" + strMBLNR + "','" + strBorrowNo + "','" + strMATNR + "','" + strAccountDAT + "','" + strPlant + "','" + strStorage + "','" + strLocationID + "','" + strMENGE + "','" + strBorrowedQTY + "','" + strBorrowQTY + "',GETDATE(),'" + strBorrowIQCID + "','" + strBorrowWHID + "','" + UserData.UserId + "',GETDATE())");

                        arlSql.Add("insert into  WHBMLog(MBLNR,BorrowNo,ReturnNo,MATNR,AccountDAT,Plant,Storage,LocationID,MENGE,BorrowedQTY,BorrowQTY,BorrowTime,BorrowIQCID,BorrowWHID,CreateName,CreateTime)" +
                                "values('" + strMBLNR + "','" + strBorrowNo + "','000','" + strMATNR + "','" + strAccountDAT + "','" + strPlant + "','" + strStorage + "','" + strLocationID + "','" + strMENGE + "','" + strBorrowedQTY + "','" + strBorrowQTY + "',GETDATE(),'" + strBorrowIQCID + "','" + strBorrowWHID + "','" + UserData.UserId + "', GETDATE())");

                        arlSql.Add("update WHDWN set OTQTY=OTQTY+'" + lstTask[i]["BorrowQTY"].ToString().Trim() + "' where MANDT='218' and MTYPE='SAP' and INSMK='0' and WERKS='" + lstTask[i]["WERKS"].ToString().Trim() + "' and LGORT='" + lstTask[i]["LGORT"].ToString().Trim() + "' and MBLNR='" + lstTask[i]["MBLNR"].ToString().Trim() + "' and MATNR='" + lstTask[i]["MATNR"].ToString().Trim() + "'");
                    }
                    ControlHandleDB();
                    bool bolResult = ControlSqlAccess.ExecSqlArray(arlSql);
                    ControlSqlAccess.CloseConnection();
                    if (bolResult)
                    {
                        return varBrNo;
                    }
                    else
                    {
                        return null;
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

            # region 新建借料单号
            public string GetBorrowNo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetBorrowNo";
                this.ControlMethodParm = "()";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dtData = new DataTable();
                    StringBuilder sbSql = new StringBuilder();

                    sbSql.Append("EXEC [dbo].[GetBorrowNo]");
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    if (dtData.Rows.Count > 0)
                    {
                        return dtData.Rows[0][0].ToString().Trim();
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

            #region 发送邮件
            public void SendMail(string strBorrowNo)
            {
                try
                {
                    string strMailTo = GetCtrlInfo();
                    string strMailCc = "";
                    string strMailBcc = "";
                    string strMessage = "";

                    string strMailBody = GetMailBody(strBorrowNo);

                    QCI_QWMS_IQCBRMaterial.MailService.SendMailService objSendMail = new QCI_QWMS_IQCBRMaterial.MailService.SendMailService();
                    objSendMail.SendMail("975A8056C8DF50786FB680A96E0CCCBF", true, "Web_Notice@quantacn.com", strMailTo, strMailCc, strMailBcc, "IQC借料OK信息", strMailBody, "", out strMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "SendMail()");
                }
            }
            #endregion

            #region 获取邮件收件人
            public string GetCtrlInfo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetCtrlInfo";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    string str = "";
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append("SELECT * FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='IQCBMReport' AND CTRLNM='BorrowMaterialReport'");

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dt.Rows.Count > 0)
                    {
                        str = dt.Rows[0]["REMAK"].ToString().Trim();
                    }
                    return str;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
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

            #region 获取邮件内容
            public string GetMailBody(string varBorrowNo)
            {
                DataTable dt = new DataTable();
                string strMailBody = "";
                StringBuilder sbSql = new StringBuilder();

                sbSql.Append("select AccountDAT,MATNR,MBLNR,BorrowNo,Plant,Storage,LocationID,MENGE,CONVERT(varchar(16),BorrowTime,120) as BorrowTime,BorrowedQTY,BorrowQTY as BorrowQTY,BorrowIQCID,BorrowWHID");
                sbSql.AppendFormat(" from WHBM where BorrowNo in ('{0}')", varBorrowNo);

                ControlHandleDB();
                dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();

                strMailBody = strMailBody + "<body> <style type='text/css'> td {text-align:left; font-family:'Arial'; font-size:15px; }</style> ";
                strMailBody = strMailBody + "<p><table><tr><td>Dear All,</td></tr> <tr><td>&nbsp;</td></tr> <tr><td>如下为IQC借料信息，请参阅，谢谢！</td></tr></table></p>";

                #region Create HTML
                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<TABLE width=100% border=1 align=center cellpadding=4 bordercolor=#3366cc style='border-collapse: collapse'><TR style=\"background-color:#6699cc;height: 34px; color: #FFFFFF;\"><TD>入账日期</TD><TD>料号</TD><TD>扣账编号</TD><TD>借料单号</TD><TD>厂区</TD><TD>仓别</TD><TD>储位/人</TD><TD>总数量</TD><TD>借料时间</TD><TD>已借数量</TD><TD>借料数量</TD><TD>IQC</TD><TD>WH</TD></TR>");
                foreach (DataRow row in dt.Rows)
                {

                    sbMailContent.AppendFormat("<TR><TD>{0}</TD><TD>{1}</TD><TD>{2}</TD><TD>{3}</TD><TD>{4}</TD><TD>{5}</TD><TD>{6}</TD><TD>{7}</TD><TD>{8}</TD><TD>{9}</TD><TD>{10}</TD><TD>{11}</TD><TD>{12}</TD></TR>",
                        row[0].ToString().Trim(), row[1].ToString().Trim(), row[2].ToString().Trim(), row[3].ToString().Trim(), row[4].ToString().Trim(),
                        row[5].ToString().Trim(), row[6].ToString().Trim(), row[7].ToString().Trim(), row[8].ToString().Trim(), row[9].ToString().Trim(),
                        row[10].ToString().Trim(), row[11].ToString().Trim(), row[12].ToString().Trim());
                }
                sbMailContent.Append("</TABLE>");
                sbMailContent.Append("<br/>");
                sbMailContent.Append("<br/>");
                #endregion

                strMailBody = strMailBody + sbMailContent.ToString().Trim();
                return strMailBody;
            }
            #endregion

            #endregion
        }
    }
}
