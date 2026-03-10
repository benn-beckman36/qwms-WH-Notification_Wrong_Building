using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;
using System.Data;
using System.Collections;
using System.Net;
using System.IO;

namespace QCI.QWMS
{
    public class AlimStorageInSMT : ControlBase
    {
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";

        UserInfo UserData = new UserInfo();

        #region
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
        public string WERKS
        {
            get { return strWerks; }
            set { strWerks = value; }
        }
        public string LGORT
        {
            get { return strLgort; }
            set { strLgort = value; }
        }
        public string PROGID
        {
            get { return strProgid; }
            set { strProgid = value; }
        }
        #endregion

        #region 构造函数
        public AlimStorageInSMT(UserInfo varUserData, string strProgid)
               : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
        {
        }


        public AlimStorageInSMT(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            MANDT = varUserData.Client;
            COMCD = varUserData.CompanyCode;
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
            ControlErrorInfo.Owner = "Jason Peng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;
        }
        #endregion

        #region 获取厂区信息
        public DataTable GetPlant()
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetPlant";
            this.ControlMethodParm = "('" + strMandt + strComcd + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" select distinct WERKS from ALHED where MANDT='{0}' and COMCD='{1}'  ", strMandt, strComcd);
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
        #endregion

        #region 获取厂区对应仓别信息
        public DataTable GetLgort(string strWerks)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetLgort";
            this.ControlMethodParm = "('" + strMandt + strComcd + strWerks + "')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" select distinct LGORT from ALHED where MANDT='{0}' and COMCD='{1}' ", strMandt, strComcd);
                if (!string.IsNullOrEmpty(strWerks))
                {
                    sbSql.AppendFormat(" and WERKS='{0}' ", strWerks);
                }
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
        #endregion

        #region 获取流道储位表中主流道信息
        public DataTable GetMainRun(string strWerks, string strLgort)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMainRun";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" select distinct MAINRUN from ALHED where MANDT='{0}' and COMCD='{1}' ", strMandt, strComcd);
                if (!string.IsNullOrEmpty(strWerks))
                {
                    sbSql.AppendFormat(" and WERKS='{0}' ", strWerks);
                }
                if (!string.IsNullOrEmpty(strLgort))
                {
                    sbSql.AppendFormat(" and LGORT='{0}' ", strLgort);
                }
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
        #endregion

        #region 获取绑定中间表中主流道信息
        public DataTable GetMainRunCtr()
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMainRunCtr";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" select distinct MAINRUN from ALRIN ");
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
        #endregion

        #region 绑定中间表,生成虚拟单号
        public DataTable GetMainRun(string strClient, string strComcd, string strWerks, string strLgort, string strProgid)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMainRun";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" select MANDT,WERKS,LGORT,MAINRUN,MBLNR,CRNAM,COMCD from ALRIN where MANDT='{0}' and COMCD='{1}' and WERKS='{2}' and LGORT='{3}' and CGCLS='{4}' ", strClient, strComcd, strWerks, strLgort, strProgid);
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
        #endregion

        #region 生成虚拟单号
        public string GetMblnr(string strClient, string strComcd, string strWerks, string strLgort, string strMainRun, string strProgid, string strUserId)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetMblnr";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("EXEC SP_CreateAlrinMblnr_AlimIn '{0}','{1}','{2}','{3}','{4}','{5}','{6}' ", strClient, strComcd, strWerks, strLgort, strMainRun, strProgid, strUserId);
                ControlHandleDB();
                dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                return dt.Rows[0][0].ToString();
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

        #region 释放流道信息
        public bool DeleteAlrinMainRunInfo(string strClient, string strComcd, string strWerks, string strLgort, string strMainRun)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "DeleteAlrinMainRunInfo";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                bool bolresult = false;
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" delete from ALRIN where MANDT='{0}' and COMCD='{1}' and  WERKS='{2}' and LGORT='{3}' and MAINRUN='{4}' ", @strClient, @strComcd, @strWerks, @strLgort, @strMainRun);
                ControlHandleDB();
                bolresult = ControlSqlAccess.ExecSql(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                return bolresult;
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

        #region 联机出库(加扣)
        #region 获取WHDWN出库数据
        public DataTable GetStorageOutDataWhdwn(string strMandt, string strComcd, string strWerks, string strLgort, string strMblnr)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetStorageOutDataWhdwn";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,MAX(SUBSTRING(MBLNR,1,10)) AS MBLNR,MATNR,CHARG,SUM(MENGE) AS MENGE,OTQTY,'' AS RELMENGE,INSMK,BWART,LIFNR,KOSTL,DACOD,LOADID,SERNO FROM WHDWN with(nolock) WHERE ");
                sbSql.AppendFormat(" MANDT='{0}' ", strMandt);
                sbSql.AppendFormat(" AND MTYPE='SAP' ");
                sbSql.AppendFormat(" AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat(" AND COMCD='{0}' ", strComcd);
                sbSql.AppendFormat(" AND TRNTP in ('G-','T-','M-') ");
                sbSql.AppendFormat(" AND BWART IN ('201','309','311','322') ");
                sbSql.AppendFormat(" AND MENGE>OTQTY ");
                sbSql.AppendFormat(" AND MBLNR LIKE '{0}%' ", strMblnr);
                sbSql.AppendFormat(" GROUP BY MANDT,COMCD,WERKS,LGORT,MATNR,CHARG,OTQTY,INSMK,BWART,LIFNR,KOSTL,DACOD,LOADID,SERNO ");
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
        #endregion

        #region 获取ALITM出库库存 REMAK BULK 散料
        public DataTable GetStorageOutDataAlitmBulk(string strMandt, string strComcd, string strWerks, string strLgort, string strMatnr)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetStorageOutDataAlitmBulk";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,'6' AS PRI,'' AS SUBPRI,MATNR,MBLNR,LOCAT,CHARG,LIFNR,DACOD,LOCOD,MENGE,'' AS ADDQTY,CRNAM,CONVERT(VARCHAR(16),CRDAT,120) AS CRDAT FROM ALITM with(nolock) WHERE ");
                sbSql.AppendFormat(" MANDT='{0}' ", strMandt);
                sbSql.AppendFormat(" AND COMCD='{0}' ", strComcd);
                sbSql.AppendFormat(" AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat(" AND ITEMSTATES='N' ");
                sbSql.AppendFormat(" AND STOCSTATES='N' ");
                sbSql.AppendFormat(" AND REMAK='BULK' ");
                sbSql.AppendFormat(" AND MATNR in ({0}) ", strMatnr);
                sbSql.AppendFormat("ORDER BY INDAT ");
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
        #endregion

        #region 获取ALITM出库库存 REMAK DACOD材料
        public DataTable GetStorageOutDataAlitmDacod(string strMandt, string strComcd, string strWerks, string strLgort, string strMatnr)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetStorageOutDataAlitmDacod";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,'6' AS PRI,'' AS SUBPRI,MATNR,MBLNR,LOCAT,CHARG,LIFNR,DACOD,LOCOD,MENGE,'' AS ADDQTY,CRNAM,CONVERT(VARCHAR(16),CRDAT,120) AS CRDAT FROM ALITM with(nolock) WHERE ");
                sbSql.AppendFormat(" MANDT='{0}' ", strMandt);
                sbSql.AppendFormat(" AND COMCD='{0}' ", strComcd);
                sbSql.AppendFormat(" AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat(" AND ITEMSTATES='N' ");
                sbSql.AppendFormat(" AND STOCSTATES='N' ");
                sbSql.AppendFormat(" AND REMAK='DACOD' ");
                sbSql.AppendFormat(" AND MATNR in ({0}) ", strMatnr);
                sbSql.AppendFormat("ORDER BY DACOD ");
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
        #endregion

        #region 获取ALITM出库库存 REMAK空值 普通料号
        public DataTable GetStorageOutDataAlitm(string strMandt, string strComcd, string strWerks, string strLgort, string strMatnr)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetStorageOutDataAlitm";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat("SELECT MANDT,COMCD,WERKS,LGORT,'6' AS PRI,'' AS SUBPRI,MATNR,MBLNR,LOCAT,CHARG,LIFNR,DACOD,LOCOD,MENGE,'' AS ADDQTY,CRNAM,CONVERT(VARCHAR(16),CRDAT,120) AS CRDAT FROM ALITM with(nolock) WHERE ");
                sbSql.AppendFormat(" MANDT='{0}' ", strMandt);
                sbSql.AppendFormat(" AND COMCD='{0}' ", strComcd);
                sbSql.AppendFormat(" AND WERKS='{0}' ", strWerks);
                sbSql.AppendFormat(" AND LGORT='{0}' ", strLgort);
                sbSql.AppendFormat(" AND ITEMSTATES='N' ");
                sbSql.AppendFormat(" AND STOCSTATES='N' ");
                sbSql.AppendFormat(" AND REMAK='' ");
                sbSql.AppendFormat(" AND MATNR in ({0}) ", strMatnr);
                sbSql.AppendFormat(" ORDER BY INDAT ");
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
        #endregion

        #region 更新加扣扣账编号到出库中间表
        public bool UpdateAgoutMblnr(string strMblnr, string strZappid)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "UpdateAgoutMblnr";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                bool strresult = false;
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" UPDATE AGOUT SET REMAK1='{0}' where MBLNR='{1}' ", strMblnr, strZappid);
                ControlHandleDB();
                strresult = ControlSqlAccess.ExecSql(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                return strresult;
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

        #region 更新加扣扣账编号到出库中间表
        public bool UpdateAgoutMblnrError(string strMESSAGE, string strZappid)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "UpdateAgoutMblnrError";
            this.ControlMethodParm = " ";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            try
            {
                bool strresult = false;
                DataTable dt = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendFormat(" UPDATE AGOUT SET REMAK2='{0}' where MBLNR='{1}' ", strMESSAGE, strZappid);
                ControlHandleDB();
                strresult = ControlSqlAccess.ExecSql(sbSql.ToString());
                ControlSqlAccess.CloseConnection();
                return strresult;
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

        public bool StorageOut_OnLineOut_Add(DataTable dtDataAdd, bool bolChecked)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "StorageOut_OnLineOut_Add";
            this.ControlMethodParm = "(" + dtDataAdd + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            #region 變數宣告
            StringBuilder sbSql = new StringBuilder();
            ArrayList arySQL = new ArrayList();
            #endregion

            #region Insert AGOUT 出库中间表
            int intCheck = 6;
            if(bolChecked)
            {
                intCheck = 3;
            }
            for (int i = 0; i < dtDataAdd.Rows.Count; i++)
            {
                string sql = "INSERT INTO AGOUT(MANDT,COMCD,WERKS,LGORT,PRI,SUBPRI,DIDNO,MBLNR,ZEILE,LOCAT,MATNR,CHARG,MENGE,INSMK,LIFNR,DACOD,LOCOD,SERNO,CRNAM,CRDAT,FLAGE)" +
                        "VALUES('" + dtDataAdd.Rows[i]["MANDT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["COMCD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["WERKS"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["LGORT"].ToString() + "'" +
                        ",'" + intCheck + "'" +
                        ",'" + dtDataAdd.Rows[i]["SUBPRI"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["DIDNO"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["MBLNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["ZEILE"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["LOCAT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["MATNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["CHARG"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["MENGE"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["INSMK"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["LIFNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["DACOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["LOCOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[i]["SERNO"].ToString() + "'" +
                        ",'" + UserData.UserId + "'" +
                        ",GETDATE()" +
                        ",'N')";
                arySQL.Add(sql.ToString());
            }
            #endregion

            #region Insert ALAddAccount 加扣记录表
            for (int f = 0; f < dtDataAdd.Rows.Count; f++)
            {
                string srAddQty = dtDataAdd.Rows[f]["ADDQTY"].ToString();
                if (srAddQty != "0")
                {
                    string sql = "INSERT INTO ALAddAccount(MANDT,COMCD,WERKS,LGORT,DIDNO,MBLNR,ZEILE,LOCAT,MATNR,CHARG,MENGE,ADDQTY,INSMK,LIFNR,DACOD,LOCOD,SERNO,CRNAM,CRDAT,FLAGE)" +
                        "VALUES('" + dtDataAdd.Rows[f]["MANDT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["COMCD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["WERKS"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["LGORT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["DIDNO"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["MBLNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["ZEILE"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["LOCAT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["MATNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["CHARG"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["MENGE"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["ADDQTY"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["INSMK"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["LIFNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["DACOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["LOCOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["SERNO"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["CRNAM"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[f]["CRDAT"].ToString() + "'" +
                        ",'N')";
                    arySQL.Add(sql.ToString());
                }                
            }
            #endregion

            #region Update ALITM 阿里山库存表
            for (int j = 0; j < dtDataAdd.Rows.Count; j++)
            {
                string sql = "UPDATE ALITM SET STOCSTATES='D' WHERE WERKS=" +
                "'" + dtDataAdd.Rows[j]["WERKS"].ToString() + "'" + "AND LGORT=" +
                "'" + dtDataAdd.Rows[j]["LGORT"].ToString() + "'" + "AND LOCAT=" +
                "'" + dtDataAdd.Rows[j]["LOCAT"].ToString() + "'" + "AND MATNR=" +
                "'" + dtDataAdd.Rows[j]["MATNR"].ToString() + "'" + "AND ITEMSTATES='N' AND STOCSTATES='N'";
                arySQL.Add(sql.ToString());
            }
            #endregion
            
            #region Update WHDWN Sap单据表
            sbSql.AppendFormat("UPDATE WHDWN SET OTQTY=OTQTY+(MENGE-OTQTY) WHERE MBLNR LIKE '{0}%' ", dtDataAdd.Rows[0]["MBLNR"].ToString());
            arySQL.Add(sbSql.ToString());
            #endregion

            #region Insert WHLOG 日志记录表
            for (int k = 0; k < dtDataAdd.Rows.Count; k++)
            {
                string sql = "INSERT INTO WHLOG(MANDT,WERKS,LGORT,CGCLS,OLOCA,MATNR,CHARG,LIFNR,TRNTP,MENGE,INSMK,CRNAM,CRDAT,SERNO,COMCD,LOCOD,DACOD,MBLNR,OMBLN,RMAK1)" +
                        "VALUES('" + dtDataAdd.Rows[k]["MANDT"].ToString() + "'" +                        
                        ",'" + dtDataAdd.Rows[k]["WERKS"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["LGORT"].ToString() + "'" +
                        ",'" + strProgid + "'" +
                        ",'" + dtDataAdd.Rows[k]["LOCAT"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["MATNR"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["CHARG"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["LIFNR"].ToString() + "'" +
                        ",'G-'" +
                        ",'" + dtDataAdd.Rows[k]["MENGE"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["INSMK"].ToString() + "'" +
                        ",'" + UserData.UserId + "'" +
                        ",GETDATE()" +
                        ",'" + dtDataAdd.Rows[k]["SERNO"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["COMCD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["LOCOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["DACOD"].ToString() + "'" +
                        ",'" + dtDataAdd.Rows[k]["MBLNR"].ToString() + "'" +
                        ",''" +
                        ",N'联机出库(加扣)')";                        
                arySQL.Add(sql.ToString());
            }
            #endregion

            bool bolReturn = false;
            try
            {
                StringBuilder alSQL = new StringBuilder();
                for (int i = 0; i < arySQL.Count; i++)
                {
                    alSQL.AppendLine(" " + arySQL[i].ToString() + "");
                }
                ControlHandleDB();
                bolReturn = ControlSqlAccess.ExecSql(alSQL.ToString());
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

        #region 调用QMS API接口请求方式
        public string HttpPostByHttpWebRequest(string Url, object PostData)
        {
            try
            {
                //POST参数
                string strPostData = PostData.ToString().Replace("\r\n", "");
                byte[] bytPostData = Encoding.UTF8.GetBytes(strPostData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Timeout = 1000000;
                request.ContentType = "application/json";   //"application/x-www-form-urlencoded";
                request.ContentLength = bytPostData.Length;
                System.IO.Stream objPostStream = request.GetRequestStream();
                objPostStream.Write(bytPostData, 0, bytPostData.Length);
                objPostStream.Close();
                //获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader objResponseStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string strResult = objResponseStreamReader.ReadToEnd();
                objResponseStreamReader.Close();
                return strResult;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}
