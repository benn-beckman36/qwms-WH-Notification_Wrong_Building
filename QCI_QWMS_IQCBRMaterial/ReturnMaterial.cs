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
        public class ReturnMaterial : ControlBase
        {
            #region DataMember

            UserInfo UserData = new UserInfo();
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strErrmsg = "";

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
            /// 厂区
            /// </summary>
            public string WERKS
            {
                get { return strWerks; }
                set { strWerks = value; }
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

            #region Constructer

            public ReturnMaterial(UserInfo varUserData, string strWerks)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks)
            {

            }

            public ReturnMaterial(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks)
            {

                UserData = varUserData;
                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = strWerks;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.ReturnMaterial";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Mike Deng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            #endregion

            #region MemberFunction

            #region 验证非零的正整数
            /// <summary>
            /// 验证非零的正整数
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public bool IsNumeric(string str)
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\+?[1-9][0-9]*$");
                return reg.IsMatch(str);
            }
            #endregion

            #region 生成还料单号
            /// <summary>
            /// 生成还料单号
            /// </summary>
            /// <remarks>Mike Deng 20160425</remarks>
            /// <returns>str</returns>
            public string GetReturnNo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetReturnNo";
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

                    sbSQL.Append("exec [SP_GetReturnNo]");

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dt.Rows.Count > 0)
                    {
                        str = dt.Rows[0][0].ToString().Trim();
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

            #region 获取有借料记录的扣账编号
            /// <summary>
            /// 获取有借料记录的扣账编号
            /// </summary>
            /// <param name="strPlant">厂区</param>
            /// <param name="strStorage">仓别</param>
            /// <remarks>Mike Deng 20160419</remarks>
            /// <returns></returns>
            public DataTable QueryMblnrInfo(string strPlant, string strStorage)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryMblnrInfo";
                this.ControlMethodParm = "('" + strPlant + "','" + strStorage + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendFormat(@"select distinct MBLNR from [dbo].[WHBM] with(nolock) where Plant='{0}' ", strPlant);
                    if (!string.IsNullOrEmpty(strStorage))
                    {
                        sbSQL.AppendFormat("and Storage='{0}';", strStorage);
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

            #region 获取借料信息
            /// <summary>
            /// 获取借料信息
            /// </summary>
            /// <param name="strPlant">厂区</param>
            /// <param name="strStorage">仓别</param>
            /// <param name="strMBLNR">扣账编号</param>
            /// <param name="strMATNR">料号</param>
            /// <param name="strBorrowNo">借料单号</param>
            /// <param name="strID">IQC借料人或WH借出人</param>
            /// <remarks>Mike Deng 20160419</remarks>
            /// <returns></returns>
            public DataTable QueryBorrowMaterialInfo(string strPlant, string strStorage, string strMBLNR, string strMATNR, string strBorrowNo, string strID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryBorrowMaterialInfo";
                this.ControlMethodParm = "('" + strPlant + "','" + strStorage + "','" + strMBLNR + "','" + strMATNR + "','" + strBorrowNo + "','" + strID + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append(@" select row_number() over(order by BorrowNo ) as Item,'' as QTY,AccountDAT,MATNR,MBLNR,BorrowNo,Plant,Storage,LocationID,MENGE,
                        BorrowQTY,BorrowTime,ISNULL(ReturnedQTY,0) ReturnedQTY  from [WHBM] with(nolock) where  ISNULL(BorrowQTY,0)<>ISNULL(ReturnedQTY,0) ");
                    if (!string.IsNullOrEmpty(strPlant))
                    {
                        sbSQL.AppendFormat(" and Plant='{0}'", strPlant);
                    }
                    if (!string.IsNullOrEmpty(strStorage))
                    {
                        sbSQL.AppendFormat(" and Storage='{0}'", strStorage);
                    }
                    if (!string.IsNullOrEmpty(strMBLNR))
                    {
                        sbSQL.AppendFormat("  and MBLNR='{0}'", strMBLNR);
                    }
                    if (!string.IsNullOrEmpty(strMATNR))
                    {
                        sbSQL.AppendFormat(" and MATNR='{0}'", strMATNR);
                    }
                    if (!string.IsNullOrEmpty(strBorrowNo))
                    {
                        sbSQL.AppendFormat(" and BorrowNo='{0}'", strBorrowNo);
                    }
                    if (!string.IsNullOrEmpty(strID))
                    {
                        sbSQL.AppendFormat(" and (BorrowWHID='{0}' OR BorrowIQCID='{0}') ", strID);
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

            #region 还料功能
            /// <summary>
            /// 还料功能
            /// </summary>
            /// <param name="strMBLNR">扣账编号</param>
            /// <param name="strBorrowNo">借料单号</param>
            /// <param name="strReturnNo">还料单号(新产生的)</param>
            /// <param name="qty">还料数量</param>
            /// <param name="strReturnIQCID">QIC还料人员工号</param>
            /// <param name="strReturnWHID">WH接收人员工号</param>
            /// <param name="strUserID">登录用户</param>
            /// <remarks>Mike Deng 20160419</remarks>
            /// <returns></returns>
            public bool ReturnMaterialDebit(string strMBLNR, string strBorrowNo, string strReturnNo, Int32 qty, string strReturnIQCID, string strReturnWHID, string strUserID)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "ReturnMaterialDebit";
                this.ControlMethodParm = "('" + strMBLNR + "','" + strBorrowNo + "','" + strReturnNo + "','" + qty + "','" + strReturnIQCID + "','" + strReturnWHID + "','" + strUserID + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {

                    bool bl = false;
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();
                    ArrayList arrSQL = new ArrayList();

                    strUserID = UserData.UserId;

                    //新增还料交易记录
                    sbSQL.Remove(0, sbSQL.ToString().Length);
                    sbSQL.AppendFormat(@" Insert into [dbo].[WHBMLog](MBLNR,BorrowNo,ReturnNo,MATNR,AccountDAT,Plant,Storage,LocationID,MENGE,BorrowedQTY,BorrowQTY,BorrowTime,BorrowIQCID,
                                            BorrowWHID,ReturnedQTY,ReturnQTY,ReturnTime,ReturnIQCID,ReturnWHID,CreateName,CreateTime,Remark)
                                            select B.MBLNR,B.BorrowNo,'{2}',B.MATNR,B.AccountDAT,B.Plant,B.Storage,B.LocationID,B.MENGE,B.BorrowedQTY,B.BorrowQTY,B.BorrowTime,B.BorrowIQCID,
                                            B.BorrowWHID,B.ReturnedQTY,{3},Getdate(),'{4}','{5}','{6}',Getdate(),B.Remark
                                            from [dbo].[WHBM] B with(nolock) 
                                            where B.MBLNR='{0}' and B.BorrowNo='{1}';
                                            ", strMBLNR, strBorrowNo, strReturnNo, qty, strReturnIQCID, strReturnWHID, strUserID);
                    arrSQL.Add(sbSQL.ToString());

                    //新增还料记录
                    sbSQL.Remove(0, sbSQL.ToString().Length);
                    sbSQL.AppendFormat(@" Insert into [dbo].[WHRM](MBLNR,BorrowNo,ReturnNo,MATNR,AccountDAT,Plant,Storage,LocationID,MENGE,
                                            ReturnQTY,ReturnTime,ReturnIQCID,ReturnWHID,CreateName,CreateTime)
                                            select B.MBLNR,B.BorrowNo,'{2}',B.MATNR,B.AccountDAT,B.Plant,B.Storage,B.LocationID,B.MENGE,
                                            {3},Getdate(),'{4}','{5}','{6}',Getdate()
                                            from [dbo].[WHBM] B with(nolock) 
                                            where B.MBLNR='{0}' and B.BorrowNo='{1}';
                                            ", strMBLNR, strBorrowNo, strReturnNo, qty, strReturnIQCID, strReturnWHID, strUserID);
                    arrSQL.Add(sbSQL.ToString());

                    //更新借料信息表的已还数量
                    sbSQL.Remove(0, sbSQL.ToString().Length);
                    sbSQL.AppendFormat(@"update WHBM set ReturnedQTY=ISNULL(ReturnedQTY,0)+{2} where MBLNR='{0}' and BorrowNo='{1}';", strMBLNR, strBorrowNo, qty);
                    arrSQL.Add(sbSQL.ToString());

                    //更新扣账资料表WHDWN的已处理数量（OTQTY），减法
                    sbSQL.Remove(0, sbSQL.ToString().Length);
                    sbSQL.AppendFormat(@"Update WHDWN set OTQTY = OTQTY-{0} where MTYPE='SAP' and INSMK='0' and MANDT='218' and MBLNR='{1}';", qty, strMBLNR);
                    arrSQL.Add(sbSQL.ToString());

                    ControlHandleDB();
                    bl = ControlSqlAccess.ExecSqlArray(arrSQL);
                    ControlSqlAccess.CloseConnection();
                    return bl;
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

            #region 获取借还料历史记录
            /// <summary>
            /// 获取借还料历史记录
            /// </summary>
            /// <param name="strPlant">厂区</param>
            /// <param name="strStorage">仓别</param>
            /// <param name="strMBLNR">扣账编号</param>
            /// <param name="strMATNR">料号</param>
            /// <param name="strBorrowNo">借料单号</param>
            /// <param name="strID">IQC借料人/WH借出人/IQC还料人/WH接收人</param>
            /// <param name="strStatus">查询状态：已借未还，未还完，已借已还</param>
            /// <remarks>Mike Deng 20160419</remarks>
            /// <returns></returns>
            public DataTable QueryBMHistoryInfo(string strPlant, string strStorage, string strMBLNR, string strMATNR, string strBorrowNo, string strID, string strStatus)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QueryBMHistoryInfo";
                this.ControlMethodParm = "('" + strPlant + "','" + strStorage + "','" + strMBLNR + "','" + strMATNR + "','" + strBorrowNo + "','" + strID + "','" + strStatus + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append(@"select row_number() over(order by MBLNR ) as Item,* from [View_QueryBMHistoryInfo] where 1=1");
                    if (!string.IsNullOrEmpty(strPlant))
                    {
                        sbSQL.AppendFormat(" and Plant='{0}'", strPlant);
                    }
                    if (!string.IsNullOrEmpty(strStorage))
                    {
                        sbSQL.AppendFormat(" and Storage='{0}'", strStorage);
                    }
                    if (!string.IsNullOrEmpty(strMBLNR))
                    {
                        sbSQL.AppendFormat("  and MBLNR='{0}'", strMBLNR);
                    }
                    if (!string.IsNullOrEmpty(strMATNR))
                    {
                        sbSQL.AppendFormat(" and MATNR='{0}'", strMATNR);
                    }
                    if (!string.IsNullOrEmpty(strBorrowNo))
                    {
                        sbSQL.AppendFormat(" and BorrowNo='{0}'", strBorrowNo);
                    }
                    if (!string.IsNullOrEmpty(strID))
                    {
                        sbSQL.AppendFormat(" and (BorrowWHID='{0}' OR BorrowIQCID='{0}' OR ReturnWHID='{0}' OR ReturnIQCID='{0}')", strID);
                    }
                    if (!string.IsNullOrEmpty(strStatus))
                    {
                        sbSQL.AppendFormat(" and Type in (N'{0}') ", strStatus);
                    }
                    sbSQL.AppendLine(" ORDER BY Type DESC ");

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

            #region 获取还料OK邮件的主题、收件人、抄送人
            /// <summary>
            /// 获取还料OK邮件的主题、收件人、抄送人
            /// </summary>
            /// <remarks>Mike Deng 20160425</remarks>
            /// <returns>dt</returns>
            public DataTable GetMailInfo()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetMailInfo";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.Append("SELECT CTRLC1 AS MailSubject,CTRLC2 as MailCc,REMAK as MailTo FROM WHCTRL with(nolock) WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='IQCBMReport' AND CTRLNM='ReturnMaterialMail';");

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

            #region 获取还料邮件内容
            /// <summary>
            /// 获取还料邮件内容
            /// </summary>
            /// <param name="varMBLNR">扣账编号</param>
            /// <param name="varBorrowNo">借料单号</param>
            /// <param name="varReturnNo">还料单号</param>
            /// <returns></returns>
            public string MailBody(string varMBLNR, string varBorrowNo, string varReturnNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "MailBody";
                this.ControlMethodParm = "";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                try
                {
                    DataTable dt = new DataTable();
                    string strMailBody = "";
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat(@"
select B.AccountDAT,B.MATNR,B.MBLNR,B.BorrowNo,B.Plant,B.Storage,B.LocationID,B.MENGE,(B.BorrowedQTY+B.BorrowQTY) AS BorrowedQTY,B.ReturnedQTY,B.BorrowTime,
R.ReturnTime,R.ReturnNo,R.ReturnQTY,R.ReturnIQCID,R.ReturnWHID
from [dbo].[WHBM] B with(nolock)
inner join WHRM R WITH(NOLOCK) on B.MBLNR=R.MBLNR and B.BorrowNo=R.BorrowNo
where B.MBLNR='{0}' and B.BorrowNo='{1}' and R.ReturnNo='{2}';", varMBLNR, varBorrowNo, varReturnNo);

                    ControlHandleDB();
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();

                    strMailBody = strMailBody + "<body> <style type='text/css'> td {text-align:left; font-family:'Arial'; font-size:15px; }</style> ";
                    strMailBody = strMailBody + "<p><table><tr><td>Dear All,</td></tr> <tr><td>&nbsp;</td></tr> <tr><td>    如下为IQC还料信息，请参阅，谢谢！</td></tr></table></p>";

                    #region Create HTML
                    StringBuilder sbMailContent = new StringBuilder();
                    sbMailContent.Append("<TABLE cellSpacing=0 cellPadding=0 align=left border=1><TR bgColor=#808080><TD>入账日期</TD><TD>料号</TD><TD>扣账编号</TD><TD>借料单号</TD><TD>厂区</TD><TD>仓别</TD><TD>储位/人</TD><TD>总数量</TD><TD>已借数量</TD><TD>已还数量</TD><TD>借料时间</TD><TD>还料时间</TD><TD>还料单号</TD><TD>还料数量</TD><TD>IQC</TD><TD>WH</TD></TR>");
                    foreach (DataRow row in dt.Rows)
                    {

                        sbMailContent.AppendFormat("<TR><TD>{0}</TD><TD>{1}</TD><TD>{2}</TD><TD>{3}</TD><TD>{4}</TD><TD>{5}</TD><TD>{6}</TD><TD>{7}</TD><TD>{8}</TD><TD>{9}</TD><TD>{10}</TD><TD>{11}</TD><TD>{12}</TD><TD>{13}</TD><TD>{14}</TD><TD>{15}</TD></TR>",
                            row[0].ToString().Trim(), row[1].ToString().Trim(), row[2].ToString().Trim(), row[3].ToString().Trim(), row[4].ToString().Trim(),
                            row[5].ToString().Trim(), row[6].ToString().Trim(), row[7].ToString().Trim(), row[8].ToString().Trim(), row[9].ToString().Trim(),
                            row[10].ToString().Trim(), row[11].ToString().Trim(), row[12].ToString().Trim(), row[13].ToString().Trim(), row[14].ToString().Trim(), row[15].ToString().Trim());

                    }
                    sbMailContent.Append("</TABLE>");
                    sbMailContent.Append("<br/>");
                    sbMailContent.Append("<br/>");
                    #endregion

                    strMailBody = strMailBody + sbMailContent.ToString().Trim();
                    return strMailBody;
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

            #region 发送邮件
            /// <summary>
            /// 发送邮件
            /// </summary>
            /// <param name="strMailTo">收件人</param>
            /// <param name="strMailCc">抄送人</param>
            /// <param name="strMailBcc">密抄人</param>
            /// <param name="strSubject">邮件主题</param>
            /// <param name="strMailBody">邮件内容</param>
            /// <param name="strMges">发送结果</param>
            /// <remarks>Mike Deng 20160422</remarks>
            /// <returns></returns>
            public void SendMail(string strMailTo, string strMailCc, string strMailBcc, string strSubject, string strMailBody, out string strMges)
            {
                try
                {
                    string strPassWord = "975A8056C8DF50786FB680A96E0CCCBF";//邮件发送密码
                    QCI_QWMS_IQCBRMaterial.MailService.SendMailService objSendMail = new QCI_QWMS_IQCBRMaterial.MailService.SendMailService();
                    objSendMail.SendMail(strPassWord, true, "Web_Notice@quantacn.com", strMailTo, strMailCc, strMailBcc, strSubject, strMailBody, "", out strMges);
                }
                catch (Exception ex)
                {

                    strMges = ex.Message;
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

            #region 获取DB中的已还数量
            /// <summary>
            /// 获取DB中的已还数量
            /// </summary>
            /// <param name="strMBLNR">扣账编号</param>
            /// <param name="strBorrowNo">借料单号</param>
            /// <remarks>Mike Deng 20160428</remarks>
            /// <returns></returns>
            public DataTable GetDBReturnedQTY(string strMBLNR, string strBorrowNo)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "GetDBReturnedQTY";
                this.ControlMethodParm = "('" + strMBLNR + "','" + strBorrowNo + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                try
                {
                    DataTable dt = new DataTable();
                    StringBuilder sbSQL = new StringBuilder();

                    sbSQL.AppendFormat(@"select distinct isnull(ReturnedQTY,0) as ReturnedQTY from WHBM with(nolock) where MBLNR='{0}' and BorrowNo='{1}';", strMBLNR, strBorrowNo);

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

            #endregion
        }
    }
}
