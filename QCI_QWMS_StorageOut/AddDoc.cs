using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using QWMS.Entity;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace QCI_QWMS_StorageOut
{
    
    /// <summary>
        /// StorageData 的摘要描述。 
        /// </summary>
    public class AddDoc : ControlBase
    {
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strDLgort = "";
        private string strMblnr = "";
        private string strProgid = "";
        private string strCrnam = "";
        private string strErrmsg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        DataTable dtData = new DataTable();
        bool flg = false;
        

        #region Constructer

            public AddDoc()
            {
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="varDBType"></param>
        /// <param name="varDBCode"></param>
        /// <param name="varErrorType"></param>
        /// <param name="varErrorCode"></param>
        /// <param name="varUserData"></param>
        /// <param name="strWerks"></param>
        /// <param name="strLgort"></param>
            public AddDoc(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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
                ControlErrorInfo.ObjectName = "QWMS.AddDoc";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Refun Zhan";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            public AddDoc(UserInfo varUserData, string strWerks, string strLgort)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
            {
            }

            public AddDoc(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "")
            {
            }

            public AddDoc(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
                : this(varDBType, varDBCode, varErrorType, varErrorCode, varUserData, "", "")
            {
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

        #region 查询需要加扣单据号

        public DataTable QueryAddDoc(string werks,string lgort,string mblnr)
        {
            this.ControlMethodName = "QueryAddDoc";
            this.ControlMethodParm = "(" + werks + "," + lgort + "," + mblnr + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" SELECT  'false' AS cSelect,  MANDT,MTYPE,MBLNR,ZEILE,WERKS,LGORT,MATNR,INSMK,CHARG,LIFNR,EBELN,"
           + "  MENGE,0 AS AddQTY,PRCDE,PUTYP,KOSTL,TRNTP,BWART,UMLGO,WO  ,CONVERT(VARCHAR(16),CRDAT,120) AS CRDAT  FROM WHDWN WITH(NOLOCK) "
           + "WHERE SUBSTRING(TRNTP,2,1)='-' AND BWART IN ('261','311')AND DATEDIFF(DAY,CRDAT,GETDATE())<2 AND OTQTY>0 "
           //+ " AND MTYPE='SAP' AND MANDT= 'QA2'   ");//SAP测试公司别
           + " AND MTYPE='SAP' AND MANDT= '" + MANDT + "'  ");


            if (werks != "")
            {
                sbSql.Append("AND WERKS = '" + werks + "'");
            }
            if (lgort != "")
            {
                sbSql.Append("AND LGORT='" + lgort + "'");
            }
            if (mblnr != "")
            {
                sbSql.Append("AND MBLNR LIKE '" + mblnr + "%'");
            }
            sbSql.Append("   ORDER BY WERKS,LGORT,MBLNR,CRDAT");


            try
            {
                //ControlHandleDB();
                SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                dtData = sqlAccess.GetDataTable(sbSql.ToString());
                sqlAccess.CloseConnection();
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

        #region 新增加扣信息

        public bool  InsertAddDoc(string mblnr,string zeile ,string ADQTY )
        {
            this.ControlMethodName = "InsertAddDoc";
            this.ControlMethodParm = "(" + mblnr + "," + zeile + "," + ADQTY + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            bool bolResult = false;

            sbSql.Append(" INSERT INTO WHDWN_ADD SELECT MANDT, MTYPE, MBLNR, ZEILE, WERKS, LGORT, MATNR, INSMK, CHARG, LIFNR, EBELN, "+ADQTY+" AS MENGE, 0 AS OTQTY, "
                                + "PRCDE, PUTYP, KOSTL, RESLT, BUDAT, PRITY, ARBPL, TRNTP, BWART, UMLGO,'" + strCrnam + "' AS USNAM , KDMAT,GETDATE()AS CRDAT , GETDATE()AS MODAT,"
                                +"SERNO, OMBLN, UPDAT, INSPT, RMANO, COMCD, REFID, BXQTY, DACOD, VEDAT, BOXID, GRLOC, WKORD, FMATN, EBELP, INTID, FLAGE, SEDTM, QWQTY, "
                                +"GRLNR, DELNO, DEITM, ADFLG, ODTYP, OTLGT,''AS REMAK1, PKDAT, PROCESSING, PROCESSING_START, WO, LOADID, TestResult, DBNAME, IPNAME,ULFLG, "
                                +"MODEL, REGION, PALQTY, IOFLAGE,FLGRT "
                                +" FROM WHDWN WHERE MTYPE='SAP' AND MBLNR ='"+mblnr+"' AND ZEILE='"+zeile+"'");

            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

        #region 查询加扣信息

        public DataTable QueryAddDocInfo(string werks, string lgort,string mblnr,string addMblnr, string dateFrom ,string dateTo)
        {
            this.ControlMethodName = "QueryAddDocInfo";
            this.ControlMethodParm = "(" + werks + "," + lgort + "," + mblnr + ","+addMblnr+","+dateFrom+","+dateTo+")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            //扣账单号\t加扣单号\t料号\t版本\t出库仓别\t目的仓别\t加扣数量\t时间
            sbSql.Append("  SELECT OMBLN AS MBLNR,REMAK1 AS MBLNR311,MATNR AS PARTNO,LGORT,CHARG,UMLGO,MENGE,CONVERT(VARCHAR(16),CRDAT,120)AS CRDAT"
                                +" FROM WHDWN_ADD "
                                +" WHERE 1=1 ");
            if (werks != "")
            {
                sbSql.Append(" AND WERKS = '" + werks + "'");
            }
            if (lgort != "")
            {
                sbSql.Append(" AND LGORT='" + lgort + "'");
            }
            if (!string.IsNullOrEmpty(mblnr) )
            {
                sbSql.Append(" AND   OMBLN LIKE '" + mblnr + "%' ");
            }
            if ( ! string.IsNullOrEmpty(dateFrom) || !string.IsNullOrEmpty( dateTo) )
            {
                sbSql.Append(" AND  CRDAT BETWEEN '" + dateFrom + "' AND '"+dateTo+"'   ");
            }
            if (!string.IsNullOrEmpty(addMblnr))
            {
                sbSql.Append(" AND  REMAK1= '" + addMblnr + "'   ");
            }
  
            sbSql.Append(" ORDER BY WERKS, LGORT, MBLNR");
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

        #region 加扣信息同步SAP后回执更改Remark栏位

        public bool UpdateAddDocReturn(string mblnr311, string zappid)
        {
            this.ControlMethodName = "UpdateAddDocReturn";
            this.ControlMethodParm = "(" + mblnr311 + "," + zappid + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            flg = false;
            sbSql.Append("  UPDATE WHDWN_ADD SET REMAK1='" + mblnr311 + "'  "
                +"WHERE SUBSTRING(TRNTP,2,1)='-' AND BWART IN ('261','311') AND DATEDIFF(DAY,CRDAT,GETDATE())<2  "
                +"AND MTYPE='SAP'  AND MBLNR ='"+zappid+"' ");
        
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sbSql.ToString());
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


        #region 加扣信息同步SAP后回执错误信息更改TestResult栏位

        public bool UpdateAddDocReturnErr(string msg, string zappid)
        {
            this.ControlMethodName = "UpdateAddDocReturnErr";
            this.ControlMethodParm = "(" + msg + "," + zappid + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            flg = false;
            sbSql.Append("  UPDATE WHDWN_ADD SET TestResult='" + msg + "'  "
                + "WHERE SUBSTRING(TRNTP,2,1)='-' AND BWART IN ('261','311') AND DATEDIFF(DAY,CRDAT,GETDATE())<2  "
                + "AND MTYPE='SAP'  AND MBLNR ='" + zappid + "' ");

            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sbSql.ToString());
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


        #region 增加加扣数据
        public void InsertAddDoc(string mblnrNew, string mblnr,string zeile,string umlgo, int qty)
        {
            this.ControlMethodName = "InsertAddDoc";
            this.ControlMethodParm = "(" + mblnr + "," + zeile + "," + qty + "," + umlgo + "," + mblnrNew + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("EXEC [dbo].[sp_CreateAddDocData]  '"+mblnrNew+"', '" + mblnr + "' ,'" + zeile + "' ,'" + CRNAM + "',"+qty+" ,"+umlgo+" ");

            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sbSQL.ToString());
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
        }

        #endregion

        #region 增加加扣Log数据
        public void InsertAddDocLog(string mblnr, string flow, string Result, string msg)
        {
            this.ControlMethodName = "InsertAddDocLog";
            this.ControlMethodParm = "(" + mblnr +  ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            if (flow=="BEGIN")
            {
                sbSQL.AppendFormat("INSERT INTO QWMS_LOG(MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME) VALUES('{0}','{1}','{2}',N'{3}','{4}','{5}',GETDATE())",
                                   mblnr, "QWMS", "IN", "开始加扣", Result, msg);
            }
            else if (flow == "SAP")
            {
                sbSQL.AppendFormat("INSERT INTO QWMS_LOG(MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME) VALUES('{0}','{1}','{2}',N'{3}','{4}','{5}',GETDATE())",
                                   mblnr, "SAP", "Z_MM_RFC_POSTYCN", "开始扣账", Result, msg);
            }
            else if (flow == "END")
            {
                sbSQL.AppendFormat("INSERT INTO QWMS_LOG(MBLNR,MTYPE,FLAGE,OPERATION,RESULT,REMARK,CREATETIME) VALUES('{0}','{1}','{2}','{3}','{4}',N'{5}',GETDATE())",
                                   mblnr, "SAP", "Z_MM_RFC_POSTYCN", "Y", Result, msg);
            }
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sbSQL.ToString());
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
        }

        #endregion

        #region 查询WHDWN_ADD表数据
        public string getWhdwn_Add(string mblnr, string ZEILE)
        {
            this.ControlMethodName = "getWhdwn_Add";
            this.ControlMethodParm = "(" + mblnr + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            string OMBLN="";
            sbSQL.AppendFormat("SELECT OMBLN FROM WHDWN_ADD WITH(NOLOCK) WHERE SUBSTRING(TRNTP,2,1)='-' AND BWART IN ('261','311')"+
                "AND MBLNR='{0}' AND ZEILE='{1}' ", mblnr, ZEILE);

            try
            {
                //ControlHandleDB();
                SqlAccess sqlAccess = new SqlAccess(CommonInfo.Instance.DBCode);
                dtData = sqlAccess.GetDataTable(sbSQL.ToString());
                if (dtData.Rows.Count>0)
                     OMBLN=dtData.Rows[0]["OMBLN"].ToString();
                sqlAccess.CloseConnection();
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
            return OMBLN;
        }

        #endregion

        #region 更新收件人信息
        
        public bool updateMailInfo(string strWerks, string strUmlgo, string strMailType, string strMail,string strMailClass)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "updateMailInfo";
            this.ControlMethodParm = "(" + strWerks + ","+strUmlgo+","+strMailType+","+strMail+")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" UPDATE WHMAL SET EMAIL='"+strMail+"' WHERE WERKS='"+strWerks+"' AND LGORT='"+strUmlgo+"' AND MANDT='"+MANDT+"'  AND MTYPE=N'"+strMailType+"'  AND FUNCT='"+strMailClass+"' ");

            bool bolResult = false;
            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

        #region 删除收件人信息

        public bool DeleteMailInfo(string strWerks, string strUmlgo, string strMailType,string strMailClass,string strMail)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "updateMailInfo";
            this.ControlMethodParm = "(" + strWerks + "," + strUmlgo + "," + strMailType + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" DELETE FROM  WHMAL  WHERE WERKS='" + strWerks + "' AND LGORT='" + strUmlgo + "' AND MANDT='" + MANDT + "'  AND MTYPE=N'"+strMailType+"'  AND FUNCT='"+strMailClass+"' AND EMAIL='"+strMail+"'  ");

            bool bolResult = false;
            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

         #region 新增收件人信息

        public bool InsertMailInfo(string strWerks, string strUmlgo,string strMail, string strMailType,string strMailClass)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "InsertMailInfo";
            this.ControlMethodParm = "(" + strWerks + "," + strUmlgo + "," + strMailType + "," + strMailType + ","+strMailClass+")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" INSERT INTO WHMAL VALUES ('"+MANDT+"','"+strWerks+"','"+strUmlgo+"','"+strMailClass+"',N'"+strMailType+"','"+strMail+"','"+UserData.UserId+"',GETDATE(),'','','"+COMCD+"') ");

            bool bolResult = false;
            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSql(sbSql.ToString());
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

        #region 查询邮件收件类型
        public DataTable GetMailType()
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMailType";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            DataTable dtData = new DataTable();

            sbSql.AppendLine(" SELECT CTRLNM  AS 'F_TEXT' FROM WHCTRL  WITH(NOLOCK)  WHERE CTRLID ='MailType' AND COMCD='" + COMCD + "' AND MANDT='" + MANDT + "' AND SOLDTO='QWMS'   ");
            sbSql.AppendLine(" union select '' AS 'F_TEXT' ");

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

       #region 查询收件人信息
        public DataTable GetMailInfo(string strWerks,string strUmlgo,string strMailType,string strMailClass)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMailInfo";
            this.ControlMethodParm = "("+strWerks+","+strUmlgo+","+strMailType+","+strMailClass+")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            DataTable dtData = new DataTable();

            sbSql.AppendLine(" SELECT MANDT,WERKS,LGORT,FUNCT,CASE WHEN MTYPE='0' THEN N'收件' ELSE N'抄送' END AS MTYPE ,EMAIL,CRNAM,CRDAT,MONAM,MODAT,COMCD FROM [dbo].[WHMAL] WHERE 1=1 ");
            if (!string.IsNullOrEmpty(strWerks))
            {
                sbSql.AppendLine(" AND WERKS = '" + strWerks + "' ");
            }
            if (!string.IsNullOrEmpty(strUmlgo))
            {
                sbSql.AppendLine(" AND LGORT = '" + strUmlgo + "' ");
            }
            if (!string.IsNullOrEmpty(strMailType))
            {
                sbSql.AppendLine(" AND MTYPE = '" + strMailType + "' ");
            }
            if (!string.IsNullOrEmpty(strMailClass))
            {
                sbSql.AppendLine(" AND FUNCT = '" + strMailClass + "' ");
            }

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

        #region 查询加扣多个仓别收件人信息
        public string GetMail(string strWerks, string strUmlgos, string strMailType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMailInfo";
            this.ControlMethodParm = "(" + strWerks + "," + strUmlgos + "," + strMailType + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            DataTable dtData = new DataTable();
            string strAddress = "";

            sbSql.AppendLine(" SELECT EMAIL FROM [dbo].[WHMAL] WHERE  FUNCT='AddDoc Mail Notice'  AND MTYPE=N'"+strMailType+"'  AND  MANDT='"+MANDT+"' AND WERKS='"+strWerks+"' AND LGORT IN ("+strUmlgos+" ) ");
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                if (dtData.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtData.Rows)
                    {
                        strAddress += dr["EMAIL"].ToString() + ";";
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
            return strAddress;
        }

        #endregion

        #region 确认加扣仓别是否已有收件人信息
        public bool CheckUmExist(string varWerks, string varUMLGO, string varMailType)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckUmExist";
            this.ControlMethodParm = "(" + varWerks + "," + varMailType + "," + varUMLGO + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendLine(" SELECT count(*) AS COUNT FROM WHMAL WHERE MANDT='" + MANDT + "' AND  WERKS='" + varWerks + "' AND LGORT='" + varUMLGO + "' AND MTYPE=N'" + varMailType + "' ");

            bool bolResult = false;
            try
            {
                ControlHandleDB();
                DataTable dtData = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                if (dtData.Rows[0]["COUNT"].ToString() !="0")
                {
                    bolResult = true;
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
            return bolResult;
        }

        #endregion



        #endregion
    }
}
