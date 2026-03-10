using System;
using System.Data;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using System.Text;
using QWMS.Entity;
using System.Data.SqlClient;

namespace QCI
{
    namespace QWMS
    {
        public class Alim : ControlBase
        {
            #region 变量
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strCrnam = "";
            private string strProgid = "";
            private string strErrmsg = "";


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
            /// CompanyCode
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
            /// 錯誤訊息。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }
            #endregion

            #region 构造函数
            public Alim(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
                //PROGID = strProgid;

            }


            public Alim(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
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
                ControlErrorInfo.ObjectName = "QWMS.Alim";
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
                sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLNM='{0}' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE WERKS='{0}' AND USRNM='{1}') ORDER BY CTRLC1 ", varWerks, UserData.UserId);


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
                sbSQL.AppendFormat(" SELECT DISTINCT CTRLC1 FROM WHCTRL WITH(NOLOCK) WHERE MANDT= '218' AND SOLDTO='QWMS' AND CTRLID='LGORT' AND CTRLC4='Electronic' AND CTRLC1 IN (SELECT LGORT FROM WHAUT WHERE  USRNM='{0}') ORDER BY CTRLC1 ", UserData.UserId);


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

            #region 获取电子仓储位尺寸信息
            /// <summary>
            /// 获取电子仓储位尺寸信息
            /// </summary>
            /// <returns></returns>
            public DataTable CheckSize()
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "CheckSize";
                this.ControlMethodParm = "('')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendFormat(" SELECT CTRLC1,CTRLC2,CTRLNM,CTRLC3,CTRLC4 FROM WHCTRL WITH(NOLOCK) WHERE MANDT='QCI' AND SOLDTO='QWMS' AND CTRLID='ALHED'  AND CTRLN3='1' ORDER BY CTRLN1 ");


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

            #region 查询电子仓储位信息
            /// <summary>
            /// 查询电子仓储位信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strContron">柜号</param>
            /// <param name="strMainRun">主流道</param>
            /// <param name="strSubRun">支流道</param>
            /// <returns></returns>
            public DataTable QuaryAlimALHED(string strWerks, string strLgort, string strContron, string strMainRun, string strSubRun)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryAlimALHED";
                this.ControlMethodParm = "('" + strWerks + strLgort + strContron + strMainRun + strSubRun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                DataTable dtData = new DataTable();
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendFormat(" SELECT WERKS,LGORT,LOCAT,MAINRUN,SUBRUN,CONTRNO,WHCTRL.CTRLC1+N'寸*'+WHCTRL.CTRLC2+'mm' AS SIZE,COLNUM,ROWNUM,DEPTH,MONAM,MODAT,LOSTS FROM ALHED WITH(NOLOCK) LEFT JOIN WHCTRL ON WHCTRL.MANDT='QCI' AND WHCTRL.SOLDTO='QWMS' AND CTRLID='ALHED' AND ALHED.SIZE=WHCTRL.CTRLNM WHERE WERKS='{0}' AND LGORT='{1}' ", strWerks, strLgort);
                if (strContron != "")
                {
                    sbSQL.AppendFormat(" AND CONTRNO='{0}' ", strContron);
                }
                if (strMainRun != "")
                {
                    sbSQL.AppendFormat(" AND MAINRUN='{0}' ", strMainRun);
                    if (strSubRun != "")
                    {
                        sbSQL.AppendFormat(" AND SUBRUN='{0}' ", strMainRun);
                    }
                }
                sbSQL.AppendFormat(" ORDER BY CONTRNO,LOCAT ");

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

            #region 增加电子仓储位信息
            /// <summary>
            /// 增加电子仓储位信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strContron">柜号</param>
            /// <param name="strMainRun">主流道</param>
            /// <param name="strSubRun">支流道</param>
            /// <param name="strSize">尺寸代码</param>
            /// <param name="Colnum">列</param>
            /// <param name="Rownum">行</param>
            /// <param name="Depth">深</param>
            /// <param name="X">中心点X坐标</param>
            /// <param name="Y">中心点Y坐标</param>
            /// <returns></returns>
            public bool AddAlimALHED(string strWerks, string strLgort, string strContron, string strMainRun, string strSubRun, string strSize, int Colnum, int Rownum, int Depth,int X,int Y)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryAlimALHED";
                this.ControlMethodParm = "('" + strWerks + strLgort + strContron + strMainRun + strSubRun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                //数据在之前就已经检查完毕，直接insert 
                bool bolResult = false;

                StringBuilder sbSql = new StringBuilder();
                for (int i = 1; i <= Colnum; i++)
                {
                    for (int j = 1; j <= Rownum; j++)
                    {
                        for (int k = 1; k <= Depth; k++)
                        {
                            //计算优先级 修改优先级，先放A010105，再放A010101
                            int PRI = 0;
                            PRI = Math.Abs(i - X) + Math.Abs(j - Y) + Depth - k;

                            sbSql.AppendFormat(" INSERT INTO ALHED (MANDT,WERKS,LGORT,LOCAT,MAINRUN,SUBRUN,CONTRNO,SIZE,COLNUM,ROWNUM,DEPTH,CRNAM,CRDAT,MONAM,MODAT,COMCD,PRI) VALUES('218','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',GETDATE(),'{10}',GETDATE(),'{11}',{12}); ", strWerks, strLgort, strContron + i.ToString("00") + j.ToString("00") + k.ToString("00"), strMainRun, strSubRun, strContron, strSize, i.ToString(), j.ToString(), k.ToString(), UserData.UserId, UserData.CompanyCode, PRI);
                        }
                    }
                }
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

            #region 修改电子仓储位尺寸信息
            /// <summary>
            /// 修改电子仓储位尺寸信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strContron">柜号</param>
            /// <param name="OldStrSize">之前的具体尺寸信息</param>
            /// <param name="NewStrSize">修改后的具体尺寸信息</param>
            /// <param name="strSize">修改后的尺寸代码</param>
            /// <returns></returns>
            public bool ModifyAlimALHED(string strWerks, string strLgort, string strContron, string strMainRun, string strSubRun, string OldStrSize, string NewStrSize, string strSize)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryAlimALHED";
                this.ControlMethodParm = "('" + strWerks + strLgort + strContron + OldStrSize + NewStrSize + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool bolResult = false;
                StringBuilder sbSql = new StringBuilder();
                //LOG               //select TOP 100 * from WHLOG where RMAK1='Modify ALHED' 
                sbSql.AppendFormat(" INSERT INTO WHLOG (MANDT,COMCD,TRNTP,MENGE,WERKS,LGORT,MATNR,CRNAM,CRDAT,RMAK1,RMAK2,RMAK3,CGCLS) VALUES('218','{0}','',0,'{1}','{2}','{3}','{4}',GETDATE(),'Modify ALHED',N'{5}',N'{6}','{7}') ", UserData.CompanyCode, strWerks, strLgort, strContron, UserData.UserId, "将" + OldStrSize, "修改为" + NewStrSize, PROGID);

                //更新
                sbSql.AppendFormat(" UPDATE ALHED SET SIZE='" + strSize + "',MONAM='" + UserData.UserId + "',MODAT=GETDATE() ");
                if (strMainRun != "")
                {
                    sbSql.AppendFormat(" ,MAINRUN='" + strMainRun + "' ");
                }
                if (strSubRun != "")
                {
                    sbSql.AppendFormat(" ,SUBRUN='" + strSubRun + "' ");
                }
                sbSql.AppendFormat(" WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND CONTRNO='" + strContron + "' AND LOSTS=0 ");
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

            #region 删除电子仓储位信息
            /// <summary>
            /// 删除电子仓储位信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strContron">柜号</param>
            /// <returns></returns>
            public bool DeleteAlimALHED(string strWerks, string strLgort, string strContron)
            {
                //設定要記錄Error Message的相關訊息
                this.ControlMethodName = "QuaryAlimALHED";
                this.ControlMethodParm = "('" + strWerks + strLgort + strContron + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                bool bolResult = false;
                StringBuilder sbSql = new StringBuilder();
                //LOG               //select TOP 100 * from WHLOG where RMAK1='Delete ALHED' 
                sbSql.AppendFormat(" INSERT INTO WHLOG (MANDT,COMCD,TRNTP,MENGE,WERKS,LGORT,MATNR,CRNAM,CRDAT,RMAK1,CGCLS) VALUES('218','{0}','',0,'{1}','{2}','{3}','{4}',GETDATE(),'Delete ALHED','{5}') ", UserData.CompanyCode, strWerks, strLgort, strContron, UserData.UserId, PROGID);

                //删除
                sbSql.AppendFormat(" DELETE FROM ALHED WHERE WERKS='{0}' AND LGORT='{1}' AND CONTRNO='{2}' AND LOSTS=0 ", strWerks, strLgort, strContron);
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

            #region 绑定流道关系
            /// <summary>
            /// 绑定流道关系
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strMblnr"></param>
            /// <param name="strMainrun"></param>
            /// <returns></returns>
            public bool lockRunner(string strWerks, string strLgort, string strMblnr, string strMainrun)
            {
                this.ControlMethodName = "lockRunner";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMblnr + strMainrun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                string strSql = " INSERT INTO ALRIN(MANDT,WERKS,LGORT,MAINRUN,MBLNR,CGCLS,CRNAM,CRDAT,COMCD) VALUES('" + MANDT + "','" + strWerks + "','" + strLgort + "','" + strMainrun + "','" + strMblnr + "','" + PROGID + "','" + UserData.UserId + "',GETDATE(),'" + COMCD + "') ";
                bool blResult;
                try
                {
                    ControlHandleDB();
                    blResult = ControlSqlAccess.ExecSql(strSql);
                    ControlSqlAccess.CloseConnection();
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

            #region 检查主流道是否在别的厂区仓别已存在
            /// <summary>
            /// 检查主流道是否在别的厂区仓别已存在
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strMainrun">主流道</param>
            /// <returns></returns>
            public bool CheckMainRun(string strWerks, string strLgort, string strMainrun)
            {
                this.ControlMethodName = "lockRunner";
                this.ControlMethodParm = "('" + strWerks + strLgort + strMainrun + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }

                string strSql = " SELECT TOP 1 UID FROM ALHED WHERE (WERKS<>'" + strWerks + "' OR LGORT<>'" + strLgort + "') AND MAINRUN='" + strMainrun + "' ";
                DataTable dtData = new DataTable();
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

            #region 查询Alim库存信息
            /// <summary>
            /// 查询Alim库存信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strPN">料号</param>
            /// <param name="strDateCode"></param>
            /// <param name="strLocat"></param>
            /// <param name="ITEMSTATES">库存状态</param>
            /// <param name="STOCSTATES">库位状态</param>
            /// <returns></returns>
            public DataTable QuaryAlimAlitm(string strWerks, string strLgort, string strPN, string strDateCode, string strLocat, string ITEMSTATES="'N'", string STOCSTATES="'N'")
            {
                this.ControlMethodName = "QuaryAlimAlitm";
                this.ControlMethodParm = "('" + strWerks + strLgort + strPN + strDateCode + strLocat + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                //所有字段，无用数据无视掉就好
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" SELECT MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,INSMK,ITEMSTATES,STOCSTATES,MBLNR,CHARG,LIFNR,RMANO,EBELN,INDAT,MENGE-REQTY AS MENGE,QCQTY,REFNO,MRGID,ISPTM,REQTY,KDMAT,CRNAM,CRDAT,MONAM,MODAT,SERNO,LOCOD,INSPT,BKQTY,DACOD_before,DACOD,VEDAT,BOXID,NLOCA,SIDNO,REFID,SEQNO,PKDAT,CASE REMAK WHEN 'BULK' THEN N'散料' ELSE REMAK END AS REMAK,REMAK1,FLAGE FROM ALITM WITH(NOLOCK) ");
                strSql.AppendFormat(" WHERE  ITEMSTATES IN (" + ITEMSTATES + ") AND STOCSTATES IN (" + STOCSTATES + ") ");
                strSql.AppendFormat(" AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                if (strPN!="")
                {
                    strSql.AppendFormat(" AND MATNR LIKE '" + strPN + "%' ");
                    if (strDateCode!="")
                    {
                        strSql.AppendFormat(" AND DACOD= '" + strDateCode + "' ");
                    }
                }
                if (strLocat!="")
                {
                    strSql.AppendFormat(" AND LOCAT= '" + strLocat + "' ");
                }

                DataTable dtData = new DataTable();
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

            #region Alim离线出库
            /// <summary>
            /// Alim离线出库
            /// </summary>
            /// <param name="drOut"></param>
            /// <param name="bolChecked">是否插队</param>
            /// <param name="strNum">流水号</param>
            /// <returns></returns>
            public bool StorageOut_Offline(DataRow[] drOut, bool bolChecked,string strNum)
            {
                this.ControlMethodName = "StorageOut_Offline";
                this.ControlMethodParm = "('" + strWerks + strLgort + strNum + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                string strDIDNo = string.Empty;
                foreach(DataRow dr in drOut)
                {
                    //for test
                    if (dr["REMAK"].ToString().Equals("散料"))
                    {
                        strDIDNo = dr["MBLNR"].ToString();
                    }
                    #region WHLOG
                    strSql.AppendFormat(" INSERT INTO WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,CHARG,LIFNR,TRNTP,MENGE,INSMK,CRNAM,CRDAT,SERNO,COMCD,LOCOD,DACOD,MBLNR,OMBLN,RMAK1) ");
                    strSql.AppendFormat(" VALUES('218','" + dr["WERKS"].ToString() + "','" + dr["LGORT"].ToString() + "','" + strProgid + "','" + dr["LOCAT"].ToString() + "','" + dr["MATNR"].ToString() + "','" + dr["CHARG"].ToString() + "','" + dr["LIFNR"].ToString() + "','G-','" + dr["MENGE"].ToString() + "','" + dr["INSMK"].ToString() + "','" + UserData.UserId + "',GETDATE(),'" + dr["SERNO"].ToString() + "','" + dr["COMCD"].ToString() + "','" + dr["LOCOD"].ToString() + "','" + dr["DACOD"].ToString() + "','" + strNum + "','" + strDIDNo + "', N'离线出库') ");
                    #endregion

                    #region ALITM
                    strSql.AppendFormat(" UPDATE ALITM SET STOCSTATES='D' WHERE ITEMSTATES='N' AND STOCSTATES='N' AND WERKS='" + dr["WERKS"].ToString() + "' AND LGORT='" + dr["LGORT"].ToString() + "' AND LOCAT='" + dr["LOCAT"].ToString() + "' AND MATNR='" + dr["MATNR"].ToString() + "' AND CHARG='" + dr["CHARG"].ToString() + "' AND MENGE='" + dr["MENGE"].ToString() + "' AND DACOD='" + dr["DACOD"].ToString() + "' ");
                    #endregion

                    #region AGOUT--Alim出库中间表
                    strSql.AppendFormat(" INSERT INTO AGOUT(COMCD,WERKS,LGORT,PRI,SUBPRI,DIDNO,MBLNR,ZEILE,LOCAT,CONTRNO,MATNR,CHARG,MENGE,INSMK,LIFNR,DACOD,VEDAT,LOCOD,SERNO,CRNAM,CRDAT,X,Y,Z,R,Diameter,Thickness) ");
                    if (bolChecked)//是否插队
                    {
                        strSql.AppendFormat(" SELECT I.COMCD,I.WERKS,I.LGORT,'4',PRI,'" + strDIDNo + "','" + strNum + "','',I.LOCAT,CONTRNO,MATNR,CHARG,MENGE,INSMK,LIFNR,DACOD,VEDAT,LOCOD,SERNO,'" + UserData.UserId + "',GETDATE(),X,Y,Z,R,Diameter,Thickness ");
                    }
                    else
                    {
                        strSql.AppendFormat(" SELECT I.COMCD,I.WERKS,I.LGORT,'7',PRI,'" + strDIDNo + "','" + strNum + "','',I.LOCAT,CONTRNO,MATNR,CHARG,MENGE,INSMK,LIFNR,DACOD,VEDAT,LOCOD,SERNO,'" + UserData.UserId + "',GETDATE(),X,Y,Z,R,Diameter,Thickness ");
                    }
                    strSql.AppendFormat(" FROM ALITM I WITH(NOLOCK) INNER JOIN ALHED A WITH(NOLOCK) ON A.WERKS=I.WERKS AND A.LGORT=I.LGORT AND A.LOCAT=I.LOCAT ");
                    strSql.AppendFormat(" INNER JOIN HedSite with(nolock) ON A.LOCAT=HedSite.LOCAT AND HedSite.HTYPE='OUT'");
                    strSql.AppendFormat(" WHERE I.STOCSTATES='D' AND ITEMSTATES='N' AND I.WERKS='" + dr["WERKS"].ToString() + "' AND I.LGORT='" + dr["LGORT"].ToString() + "' AND I.LOCAT='" + dr["LOCAT"].ToString() + "' AND I.MATNR='" + dr["MATNR"].ToString() + "' AND I.CHARG='" + dr["CHARG"].ToString() + "' AND I.DACOD='" + dr["DACOD"].ToString() + "' AND I.MENGE='" + dr["MENGE"].ToString() + "' ");
                    #endregion
                }

                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion

            #region 获取Alim离线出库和离线入库的流水号
            /// <summary>
            /// 获取Alim离线出库和离线入库的流水号
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <returns></returns>
            public string GetAlimNo(string strWerks)
            {
                this.ControlMethodName = "GetAlimNo";
                this.ControlMethodParm = "('" + strWerks + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //strSql.AppendFormat(@" Declare @Return_Message VARCHAR(100) ");
                //strSql.AppendFormat(@" EXEC SP_CreateAlimSerno '{0}',@Return_Message OUTPUT ", strWerks.Substring(strWerks.Length - 2, 2));
                //strSql.AppendFormat(@" SELECT @Return_Message ");
                strSql.AppendFormat(@" SELECT RIGHT(CONVERT(VARCHAR(8),GETDATE(),112),6)+RIGHT('00000'+ CAST(NEXT VALUE FOR GetSerialSeq AS VARCHAR),4) ");
                DataTable dtData = new DataTable();
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
                return dtData.Rows[0][0].ToString();
            }
            #endregion

            #region 获取禁用料号信息
            /// <summary>
            /// 获取禁用料号信息
            /// </summary>
            /// <param name="strWerks">厂区</param>
            /// <param name="strLgort">仓别</param>
            /// <param name="strPN">料号</param>
            /// <param name="strRemak">备注</param>
            /// <returns></returns>
            public DataTable QueryAlim_LockPN(string strWerks,string strLgort,string strPN,string strRemak,string strCharg,string strLifnr,string strDacod,string strLocod)
            {
                this.ControlMethodName = "QueryAlim_LockPN";
                this.ControlMethodParm = "('" + strWerks + strLgort + strPN  + strRemak + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" SELECT MANDT,COMCD,WERKS,LGORT,MATNR,CHARG,LIFNR,DACOD,LOCOD,CRNAM,CRDAT,SERNO,REMAK FROM Alim_LockPN WITH(NOLOCK) ");
                strSql.AppendFormat(" WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                if (strPN != "")
                {
                    strSql.AppendFormat(" AND MATNR='" + strPN + "' ");
                }

                if (strRemak != "")
                {
                    strSql.AppendFormat(" AND REMAK='" + strRemak + "' ");
                }
                if (strCharg != "")
                {
                    strSql.AppendFormat(" AND CHARG='" + strCharg + "' ");
                }
                if (strLifnr != "")
                {
                    strSql.AppendFormat(" AND LIFNR='" + strLifnr + "' ");
                }
                if (strDacod != "")
                {
                    strSql.AppendFormat(" AND DACOD='" + strDacod + "' ");
                }
                if (strLocod != "")
                {
                    strSql.AppendFormat(" AND LOCOD='" + strLocod + "' ");
                }
                DataTable dtData = new DataTable();
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

            #region 增加禁用料号或储位信息
            /// <summary>
            /// 增加禁用料号或储位信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strPN"></param>
            /// <param name="strCharg"></param>
            /// <param name="strLIFNR"></param>
            /// <param name="strDateCode"></param>
            /// <param name="strLotCode"></param>
            /// <param name="strRemak"></param>
            /// <returns></returns>
            public bool ADDAlim_LockPN(string strWerks, string strLgort, string strPN, string strCharg, string strLIFNR, string strDateCode, string strLotCode, string strRemak)
            {
                this.ControlMethodName = "ADDAlim_LockPN";
                this.ControlMethodParm = "('" + strWerks + strLgort + strPN + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat(" INSERT INTO Alim_LockPN(WERKS,LGORT,MATNR,CHARG,LIFNR,DACOD,LOCOD,CRNAM,CRDAT,REMAK) ");
                strSql.AppendFormat(" VALUES('" + strWerks + "','" + strLgort + "','" + strPN + "','" + strCharg + "','" + strLIFNR + "','" + strDateCode + "','" + strLotCode + "','" + UserData.UserId + "',GETDATE(),N'" + strRemak + "') ");
                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion

            
            #region 删除禁用料号信息
            /// <summary>
            /// 删除禁用料号信息
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <param name="strPN"></param>
            /// <param name="strCharg"></param>
            /// <param name="strLIFNR"></param>
            /// <param name="strDateCode"></param>
            /// <param name="strLotCode"></param>
            /// <param name="strRemak"></param>
            /// <returns></returns>
            public bool DeleteAlim_LockPN(string strWerks, string strLgort, string strPN, string strCharg, string strLIFNR, string strDateCode, string strLotCode)
            {
                this.ControlMethodName = "DeleteAlim_LockPN";
                this.ControlMethodParm = "('" + strWerks + strLgort + strPN + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //WHLOG
                strSql.AppendFormat(" INSERT INTO WHLOG (MANDT,COMCD,TRNTP,MATNR,CHARG,LIFNR,DACOD,LOCOD,MENGE,WERKS,LGORT,CRNAM,CRDAT,RMAK1,CGCLS) VALUES('218','" + UserData.CompanyCode + "','','" + strPN + "','" + strCharg + "','" + strLIFNR + "','" + strDateCode + "','" + strLotCode + "',0,'','" + strWerks + "','" + strLgort + "','" + UserData.UserId + "',GETDATE(),'Delete Alim_LockPN','" + PROGID + "') ");
                //删除信息
                strSql.AppendFormat(" DELETE Alim_LockPN WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND MATNR='" + strPN + "' AND CHARG='" + strCharg + "' AND LIFNR='" + strLIFNR + "' AND DACOD='" + strDateCode + "' AND LOCOD='" + strLotCode + "' ");
                //解锁
                strSql.AppendFormat(" UPDATE ALITM SET ITEMSTATES='N' WHERE ITEMSTATES='L' AND STOCSTATES='N' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND MATNR='" + strPN + "' ");
                if (strCharg != "")
                {
                    strSql.AppendFormat(" AND CHARG='" + strCharg + "' ");
                }
                if (strLIFNR != "")
                {
                    strSql.AppendFormat(" AND LIFNR='" + strLIFNR + "' ");
                }
                if (strDateCode != "")
                {
                    strSql.AppendFormat(" AND DACOD='" + strDateCode + "' ");
                }
                if (strLotCode != "")
                {
                    strSql.AppendFormat(" AND LOCOD='" + strLotCode + "' ");
                }

                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion
                        
            #region 立即执行禁用逻辑
            /// <summary>
            /// 立即执行禁用逻辑
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <returns></returns>
            public bool Run_NowAlim_LockPN(string strWerks, string strLgort)
            {
                this.ControlMethodName = "Run_NowAlim_LockPN";
                this.ControlMethodParm = "('" + strWerks + strLgort  + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //WHLOG                 //select top 10 * from whlog where rmak1='Run_NowAlim_LockPN'
                strSql.AppendFormat(" INSERT INTO WHLOG (MANDT,COMCD,TRNTP,MENGE,WERKS,LGORT,MATNR,CRNAM,CRDAT,RMAK1,CGCLS)  VALUES('218','{0}','',0,'{1}','{2}','','{3}',GETDATE(),'Run_NowAlim_LockPN','{4}') ", UserData.CompanyCode, strWerks, strLgort, UserData.UserId, PROGID);
                //查询
                DataTable dtTemp=QueryAlim_LockPN(strWerks,strLgort,"","","","","","");
                foreach(DataRow dr in dtTemp.Rows)
                {
                    //执行
                    strSql.AppendFormat(" UPDATE ALITM SET ITEMSTATES='L' WHERE ITEMSTATES='N' AND STOCSTATES='N' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND MATNR='" + dr["MATNR"].ToString() + "' ");
                    if (dr["CHARG"].ToString() != "")
                    {
                        strSql.AppendFormat(" AND CHARG='" + dr["CHARG"].ToString() + "' ");
                    }
                    if (dr["LIFNR"].ToString() != "")
                    {
                        strSql.AppendFormat(" AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                    }
                    if (dr["DACOD"].ToString() != "")
                    {
                        strSql.AppendFormat(" AND DACOD='" + dr["DACOD"].ToString() + "' ");
                    }
                    if (dr["LOCOD"].ToString() != "")
                    {
                        strSql.AppendFormat(" AND LOCOD='" + dr["LOCOD"].ToString() + "' ");
                    }
                }
                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion

            
            #region 查询禁用储位
            /// <summary>
            /// 查询禁用储位
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <returns></returns>
            public DataTable QuaryAlimALHED_BanLocat(string strWerks, string strLgort)
            {
                this.ControlMethodName = "QuaryAlimALHED_BanLocat";
                this.ControlMethodParm = "('" + strWerks + strLgort  + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //查询
                strSql.AppendFormat(" SELECT WERKS,LGORT,LOCAT FROM ALHED WITH(NOLOCK) WHERE LOSTS='2' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' ");
                DataTable dtData = new DataTable();
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

            
            #region 禁用储位
            /// <summary>
            /// 禁用储位
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <returns></returns>
            public bool AddAlimAlhed_BanLocat(string strWerks, string strLgort,string strLocat,string strRemak)
            {
                this.ControlMethodName = "AddAlimAlhed_BanLocat";
                this.ControlMethodParm = "('" + strWerks + strLgort  + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //WHLOG                 //select top 10 * from whlog where rmak1='LOCK ALIM LOCAT'
                strSql.AppendFormat(" INSERT INTO WHLOG(MANDT,COMCD,WERKS,LGORT,CGCLS,OLOCA,CRNAM,CRDAT,RMAK1,RMAK2,MATNR,TRNTP,MENGE) VALUES('218','{0}','{1}','{2}','{3}','{4}','{5}',GETDATE(),N'LOCK ALIM LOCAT','{6}','','',0) ", UserData.CompanyCode, strWerks, strLgort, PROGID, strLocat, UserData.UserId, strRemak);
                //禁用
                strSql.AppendFormat(" UPDATE ALHED SET LOSTS='2' WHERE LOSTS='0' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' ");
                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion

            
            #region 恢复储位
            /// <summary>
            /// 恢复储位
            /// </summary>
            /// <param name="strWerks"></param>
            /// <param name="strLgort"></param>
            /// <returns></returns>
            public bool DeleteAlimAlhed_BanLocat(string strWerks, string strLgort, string strLocat)
            {
                this.ControlMethodName = "DeleteAlimAlhed_BanLocat";
                this.ControlMethodParm = "('" + strWerks + strLgort  + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //WHLOG                 //select top 10 * from whlog where rmak1='Delete LOCK ALIM LOCAT'
                strSql.AppendFormat(" INSERT INTO WHLOG(MANDT,COMCD,WERKS,LGORT,CGCLS,OLOCA,CRNAM,CRDAT,RMAK1,MATNR,TRNTP,MENGE) VALUES('218','{0}','{1}','{2}','{3}','{4}','{5}',GETDATE(),N'Delete LOCK ALIM LOCAT','','',0) ", UserData.CompanyCode, strWerks, strLgort, PROGID, strLocat, UserData.UserId);
                //恢复
                strSql.AppendFormat(" UPDATE ALHED SET LOSTS='0' WHERE LOSTS='2' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' ");
                bool bolResult = false;
                try
                {
                    ControlHandleDB();
                    bolResult = ControlSqlAccess.ExecSql(strSql.ToString());
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
                return bolResult;
            }
            #endregion

            #region 获取储位的X,Y,Z,R数值
            public DataTable GetHedSize(string strWerks, string strLgort,string strLocat)
            {
                this.ControlMethodName = "GetHedSize";
                this.ControlMethodParm = "('" + strWerks + strLgort + "')";
                if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
                {
                    ControlHandleError("000", "", "");
                }
                StringBuilder strSql = new StringBuilder();
                //查询
                strSql.AppendFormat(" SELECT X,Y,Z,R FROM HedSite WITH(NOLOCK) WHERE MANDT='" + MANDT + "' AND COMCD='" + COMCD + "' AND WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND HTYPE='OUT' ");
                DataTable dtData = new DataTable();
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

        }


    }
}
