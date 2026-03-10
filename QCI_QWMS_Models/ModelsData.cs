using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;
using System.Data;
using QWMS.Entity;
using QCI.QWMS;


namespace QCI_QWMS_Models
{

    public class ModelsData : ControlBase
    {
        #region Data
        private string strMandt = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strErrmsg = "";
        private string strComcd = "";
        private string strProgid = "";

        #region Constructer


        public ModelsData()
        {
        }

        public ModelsData(UserInfo varUserData, string strWerks, string strLgort, string strProgid)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort, strProgid)
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
        ///  QCI.QWMS.SapData objSapData =new QCI.QWMS.SapData(strConnectionString,strMandt,strWerks,strLgort);
        ///  Your Code Here......
        /// </remarks>
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public ModelsData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort, string strProgid)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();

            MANDT = varUserData.Client;
            COMCD = varUserData.CompanyCode;
            WERKS = strWerks;
            LGORT = strLgort;
            Progid = strProgid;

            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QCI_QWMS_Models.ModelsData";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Johnny";

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
        /// Company Code。
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
        /// 錯誤訊息。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string ERRMSG
        {
            get { return strErrmsg; }
            set { strErrmsg = value; }
        }

        public string Progid
        {
            get { return strProgid; }
            set { strProgid = value; }
        }

        #endregion
        #endregion
        #region function
        #region 从WHITM表获取模号列表
        public DataTable GetModelsListInWhitm(string ModelNO)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "GetModelsListInWhitm";
            this.ControlMethodParm = "(" + Progid + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            DataTable dtData = new DataTable();
            string strSql = string.Format(@"SELECT M_Models.*,M_Assets.*, MANDT,COMCD ,LGORT,LOCAT,MBLNR,MATNR,CHARg,MENGE FROM dbo.WHITM   WITH(NOLOCK)
 inner join  dbo.M_Models  WITH(NOLOCK) on M_Models.ModelNO=WHITM.MATNR left JOIN dbo.M_Assets   WITH(NOLOCK) ON M_Models. ModelNO=M_Assets.ModelNo
 WHERE   MANDT ='{0}' AND COMCD ='{1}'  AND WERKS ='{2}' AND LGORT ='{3}'  AND    NOT  EXISTS  (SELECT 'Y' FROM dbo.WHDWN W WHERE W. MATNR=WHITM. MATNR AND MENGE >OTQTY )    ", MANDT, COMCD, WERKS, LGORT);
            if (!string.IsNullOrEmpty(ModelNO))
            {
                strSql += string.Format(" and MATNR in('{0}') ", ModelNO);
            }

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

        #region 生成进出调拨单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="fWerks"></param>
        /// <param name="fLogrt"></param>
        /// <param name="tWerks"></param>
        /// <param name="tLogrt"></param>
        /// <param name="strRmark"></param>
        /// <returns></returns>
        public DataTable CreateModelOrder(DataTable dtSource, string fWerks, string fLogrt, string tWerks, string tLogrt, string strRmark)
        {
            bool bolReturn = false;
            DataTable dtData = new DataTable();
            DataTable dtCheck = new DataTable();
            try
            {
                if (dtSource.Rows.Count <= 0)
                {
                    throw new Exception("保存时异常，无数据！");
                }
                string strMatnrs = "";
                foreach (DataRow dr in dtSource.Rows)
                {
                    strMatnrs += dr["MATNR"].ToString().Trim() + ",";
                }
                strMatnrs = strMatnrs.Substring(0, strMatnrs.Length - 1);

                ControlHandleDB();
                string strSql = string.Format("EXEC SP_CreateModelOrder '{0}','{1}','{2}','{3}','{4}','{5}','{6}' ", fWerks, fLogrt,
                    strMatnrs.Replace("'", "''"), strRmark.Replace("'", "''"), tWerks, tLogrt, UserData.UserId);
                dtData = ControlSqlAccess.GetDataTable(strSql.ToString());

                string strCheckSQL =
                    string.Format(@"SELECT DISCTINC REFID  FROM dbo.WHDWN WITH(NOLOCK)  WHERE REFID ='{0}' AND MTYPE ='MODEL_E'
UNION ALL  SELECT  REFID FROM dbo.WHDWN  WITH(NOLOCK)  WHERE REFID ='{1}' AND MTYPE ='MODEL_I'",
                        dtData.Rows[0]["OUTMBLNR"].ToString(), dtData.Rows[0]["INMBLNR"].ToString());
                dtCheck = ControlSqlAccess.GetDataTable(strCheckSQL.ToString());
                ControlSqlAccess.CloseConnection();

                if (dtCheck.Rows.Count < 2)
                {
                    throw new Exception("保存时异常，未成功生成调拨单号");
                }
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- CreateModelOrder()";
            }
            return dtData;
        }
        #endregion

        #region 检查是否有重复的模号
        public DataTable CheckModelNo(DataTable dtSource)
        {
            bool bolReturn = false;
            DataTable dtData = new DataTable();
            try
            {
                if (dtSource.Rows.Count <= 0)
                {
                    throw new Exception("保存时异常，无数据！");
                }
                string strMatnrs = "";
                foreach (DataRow dr in dtSource.Rows)
                {
                    strMatnrs += dr["MATNR"].ToString().Trim() + "',' ";
                }
                strMatnrs = "'" + strMatnrs.Substring(0, strMatnrs.Length - 4) + "'";


                string strSql = string.Format(@"SELECT * FROM (
                            SELECT MATNR  FROM dbo.WHITM  WHERE MATNR IN ({0})
                            UNION ALL 
                            SELECT MATNR  FROM whtrm  WHERE MATNR IN ({0})
                            )AS t
                            GROUP BY t.MATNR
                            HAVING COUNT( MATNR)>1", strMatnrs);
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(strSql.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- CheckModelNo()";
            }
            return dtData;
        }
        #endregion

        #region 列取调拨单号
        public DataTable ListOrderData(string varMblnr, string varCrdat, string varOutType)
        {
            DataTable dtResult = new DataTable();
            string strSQL = "";
            if (varOutType.ToUpper() == "MODEL_E")
            {
                strSQL = string.Format(@"SELECT * FROM dbo.WHDWN INNER JOIN dbo.M_Models ON ModelNO=MATNR LEFT JOIN dbo.M_Assets ON dbo.M_Models.ModelNO = dbo.M_Assets.ModelNO
INNER JOIN WHITM ON  dbo.WHDWN.MANDT = WHITM.MANDT AND dbo.WHDWN.COMCD =WHITM.COMCD
AND dbo.WHDWN.WERKS = dbo.WHITM.WERKS AND dbo.WHDWN.LGORT = dbo.WHITM.LGORT AND dbo.WHDWN.MATNR = dbo.WHITM.MATNR
WHERE WHDWN.MANDT ='{0}' AND WHDWN.COMCD ='{1}' AND WHDWN.WERKS ='{2}' AND WHDWN.LGORT ='{3}'
AND (WHDWN.MENGE>WHDWN.OTQTY) AND   WHDWN.MTYPE ='{4}'", MANDT, COMCD, WERKS, LGORT, varOutType);

                if (!string.IsNullOrEmpty(varMblnr))
                {
                    strSQL += string.Format(@" and WHDWN.REFID =('{0}')", varMblnr);
                }
            }
            if (varOutType.ToUpper() == "MODEL_I")
            {
                strSQL =
                    string.Format(
                        @"SELECT WHDWN.*,M_Models.*,M_Assets.*  FROM dbo.WHTRM INNER JOIN dbo.WHDWN ON   dbo.WHTRM.MANDT = dbo.WHDWN.MANDT AND dbo.WHTRM.COMCD = dbo.WHDWN.COMCD
		AND DWERK =dbo.WHDWN.WERKS AND DLGOR =dbo.WHDWN.LGORT AND DMBLN =REFID  and WHDWN.matnr =whtrm.matnr  
 INNER JOIN dbo.M_Models ON ModelNO=WHDWN.MATNR  LEFT JOIN dbo.M_Assets ON dbo.M_Models.ModelNO = dbo.M_Assets.ModelNO 
WHERE WHDWN.MANDT ='{0}' AND WHDWN.COMCD ='{1}' AND WHDWN. WERKS ='{2}' AND WHDWN.LGORT ='{3}'
AND (WHDWN.MENGE>WHDWN.OTQTY) AND   WHDWN.MTYPE ='{4}'", MANDT, COMCD, WERKS, LGORT, varOutType);

                if (!string.IsNullOrEmpty(varMblnr))
                {
                    if (varMblnr.ToString().Length <= 16)
                    {
                        strSQL += string.Format(@" and WHDWN.REFID =('{0}')", varMblnr);
                    }
                    else
                    {
                        strSQL += string.Format(@" and WHDWN.MBLNR IN('{0}')", varMblnr);
                    }
                }
            }
            if (!string.IsNullOrEmpty(varCrdat))
            {
                strSQL += string.Format(@" and (WHDWN.CRDAT Between '" + varCrdat + " 00:00:00.000' and '" + varCrdat + " 23:59:59.999')");
            }

            try
            {
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(strSQL.ToString());
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ListOrderData()";
            }
            return dtResult;
        }
        #endregion

        #region 处理调拨出
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="fWerks"></param>
        /// <param name="fLogrt"></param>
        /// <param name="tWerks"></param>
        /// <param name="tLogrt"></param>
        /// <param name="strRmark"></param>
        /// <returns></returns>
        public bool ModelTransforOutByOrder(DataTable dtSource, string MBLNR)
        {
            QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
            bool bolReturn = false;
            DataTable dtData = new DataTable();
            try
            {
                if (dtSource.Rows.Count <= 0)
                {
                    throw new Exception("保存时异常，无数据！");
                }
                ArrayList alSql = objLogData.AddLogData("", "", dtSource);
                string ModelOrder = "";
                string strSql = string.Format("SELECT REFID FROM dbo.WHDWN WHERE  MANDT ='218' AND COMCD ='9110' AND WERKS ='{0}' AND LGORT ='{1}' and MBLNR LIKE '{2}%'", WERKS, LGORT, MBLNR);

                ControlHandleDB();

                ModelOrder = ControlSqlAccess.GetDataTable(strSql.ToString()).Rows[0]["REFID"].ToString();
                if (string.IsNullOrEmpty(ModelOrder))
                {
                    throw new Exception("保存时异常，未获取到调拨出单号！");
                }
                string strspSql = string.Format("; EXEC sp_model_TransferOUT '{0}','{1}','{2}','{3}'", WERKS, LGORT, ModelOrder, UserData.UserId);
                alSql.Add(strspSql);
                bolReturn = ControlSqlAccess.ExecSqlArray(alSql);
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ModelTransforOutByOrder()";
            }
            return bolReturn;
        }
        #endregion

        #region 处理调拨入
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="fWerks"></param>
        /// <param name="fLogrt"></param>
        /// <param name="tWerks"></param>
        /// <param name="tLogrt"></param>
        /// <param name="strRmark"></param>
        /// <returns></returns>
        public bool ModelTransforInByOrder(DataTable dtSource, string MBLNR, string Location)
        {
            QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
            bool bolReturn = false;
            try
            {
                if (dtSource.Rows.Count <= 0)
                {
                    throw new Exception("保存时异常，无数据！");
                }
                DataColumn dcLOCAT = new DataColumn("LOCAT", typeof(string));
                dtSource.Columns.Add(dcLOCAT);
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    dtSource.Rows[i]["LOCAT"] = Location;
                }

                ArrayList alSql = objLogData.AddLogData("", "", dtSource);
                //string ModelOrder = "";
                //string strSql = string.Format("SELECT REFID FROM dbo.WHDWN WHERE  MANDT ='218' AND COMCD ='9110' AND WERKS ='{0}' AND LGORT ='{1}' and MBLNR LIKE '{2}%'", WERKS, LGORT, MBLNR);

                ControlHandleDB();

                //ModelOrder = ControlSqlAccess.GetDataTable(strSql.ToString()).Rows[0]["REFID"].ToString();
                //if (string.IsNullOrEmpty(ModelOrder))
                //{
                //    throw new Exception("保存时异常，未获取到调拨出单号！");
                //}
                string strspSql = string.Format("; EXEC sp_model_TransferIN '{0}','{1}','{2}','{3}','{4}' ", WERKS, LGORT, MBLNR, Location, UserData.UserId);
                alSql.Add(strspSql);
                bolReturn = ControlSqlAccess.ExecSqlArray(alSql);
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ModelTransforInByOrder()";
            }
            return bolReturn;
        }
        #endregion

        #region 查询调拨单

        public DataTable QueryTransferOrder(string varStartDate, string varEndDate, string varInOrder,
            string varOutOrder)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryTransferOrder";
            this.ControlMethodParm = "(" + varStartDate + ',' + varEndDate + ',' + varInOrder + ',' + varOutOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            string strSQL = string.Format(@"SELECT 'FALSE' Selected,I.MANDT IMANDT,I.COMCD ICOMCD,I.WERKS IWERKS,I.LGORT ILGORT,I.MBLNR IMBLNR,E.MBLNR EMBLNR,I.MATNR,I.CRDAT ICRDAT,
                E.COMCD ECOMCD,E.WERKS EWERKS,E.LGORT ELGORT
                from WHDWN  E INNER JOIN  WHDWN I on SUBSTRING(I.REFID,3,14)= SUBSTRING(E.REFID,3,14)
                AND I.MTYPE='MODEL_I' AND E.MTYPE='MODEL_E' AND I.MATNR=E.MATNR AND I.ZEILE=E.ZEILE WHERE I.CRDAT BETWEEN '" + varStartDate + " 00:00:00.000' and '" + varEndDate +
                                  " 23:59:59.999'");


            if (!string.IsNullOrEmpty(varInOrder) && !string.IsNullOrEmpty(varOutOrder))
            {
                strSQL += string.Format(@"AND E.MBLNR LIKE '" + varInOrder + "%' OR E.MBLNR LIKE'" + varOutOrder + "%'");
            }

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

        #region 检查选定的调拨单
        public DataTable CheckTransferOrder(string varOrder)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryTransferOrder";
            this.ControlMethodParm = "(" + varOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            string strSQL = string.Format(@"SELECT * FROM WHDWN WHERE SUBSTRING(MBLNR,3,14) IN ('" + varOrder + "')");

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

        #region 查询已回冲调拨单

        public DataTable QueryReturnTransferOrder(string varStartDate, string varEndDate, string varInOrder,
            string varOutOrder)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryReturnTransferOrder";
            this.ControlMethodParm = "(" + varStartDate + ',' + varEndDate + ',' + varInOrder + ',' + varOutOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            string strSQL = string.Format(@"SELECT MANDT,COMCD,WERKS,LGORT,LOCAT,MBLNR,MATNR,MENGE,DWERK,DLGOR,DMBLN,CRNAM,CRDAT,REMARK FROM WHRTN WHERE CRDAT BETWEEN '" + varStartDate + " 00:00:00.000' and '" + varEndDate +
                                  " 23:59:59.999' ");


            if (!string.IsNullOrEmpty(varInOrder) && !string.IsNullOrEmpty(varOutOrder))
            {
                strSQL += string.Format(@"AND MBLNR LIKE '" + varOutOrder + "%' OR DMBLN LIKE'" + varInOrder + "%'");
            }

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

        #region 查询可回冲调拨单

        public DataTable QueryTransferOrderReturnAble(string varStartDate, string varEndDate, string varInOrder,
            string varOutOrder)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryTransferOrderReturnAble";
            this.ControlMethodParm = "(" + varStartDate + ',' + varEndDate + ',' + varInOrder + ',' + varOutOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            string strSQL = string.Format(@"SELECT 'FALSE' SELECTED,I.MANDT IMANDT,I.COMCD ICOMCD,I.WERKS IWERKS,I.LGORT ILGORT,I.MBLNR IMBLNR,E.MBLNR EMBLNR,I.MATNR,I.CRDAT ICRDAT,
                E.COMCD ECOMCD,E.WERKS EWERKS,E.LGORT ELGORT
                from WHDWN  E INNER JOIN  WHDWN I on SUBSTRING(I.REFID,3,14)= SUBSTRING(E.REFID,3,14)
                AND I.MTYPE='MODEL_I' AND E.MTYPE='MODEL_E' AND I.MATNR=E.MATNR AND I.ZEILE=E.ZEILE WHERE I.CRDAT BETWEEN '" + varStartDate + " 00:00:00.000' and '" + varEndDate +
                                  " 23:59:59.999' AND I.OTQTY=0 AND E.OTQTY=1");


            if (!string.IsNullOrEmpty(varInOrder) && !string.IsNullOrEmpty(varOutOrder))
            {
                strSQL += string.Format(@"AND E.MBLNR LIKE '" + varInOrder + "%' OR E.MBLNR LIKE'" + varOutOrder + "%'");
            }

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

        #region 删除调拨单
        public bool DeleteTransferOrder(string varOrder)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryTransferOrder";
            this.ControlMethodParm = "(" + varOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool bolResult = false;
            DataTable dtData = new DataTable();
            string strSQL = string.Format(@"DELETE FROM WHDWN WHERE SUBSTRING(MBLNR,3,14) IN ('" + varOrder + "') AND OTQTY=0");
            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSql(strSQL.ToString());
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

        #region 回冲调拨单
        public bool ReturnTransferOrder(DataTable dtData, string strRemark)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "ReturnTransferOrder";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            bool bolResult = false;
            ArrayList arySQL = new ArrayList();
            StringBuilder sbSQL = new StringBuilder();

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                //删除WHLOG异动数据
                arySQL.Add("DELETE FROM WHLOG WHERE SUBSTRING(MBLNR,3,14) = ('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "')");

                //恢复WHHED储位状态
                arySQL.Add(
                    "update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whtrm WITH(NOLOCK) where SUBSTRING(MBLNR,3,14) " +
                    "=('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "') ) as T where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= (select distinct WERKS " +
                    "from WHTRM where SUBSTRING(MBLNR,3,14) =('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "'))  and LGORT= (select distinct LGORT from WHTRM where SUBSTRING(MBLNR,3,14) " +
                    "=('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "')) and LOCAT= (select distinct LOCAT from WHTRM where SUBSTRING(MBLNR,3,14) =('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "'))");

                //恢复WHITM库存数据
                arySQL.Add("insert into whitm(MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,MBLNR,MENGE,RMAK1,CRNAM,CRDAT,MONAM,MODAT,INDAT) SELECT dbo.WHDWN.MANDT,dbo.WHDWN.COMCD,dbo.WHDWN.WERKS," +
                             "dbo.WHDWN.LGORT,WHTRM.LOCAT,dbo.WHTRM.MATNR,''as MBLNR,dbo.WHTRM.MENGE,REMARK ,'" + UserData.UserId + "',GETDATE(),'" + UserData.UserId + "' ,GETDATE(),GETDATE() FROM dbo.WHTRM INNER JOIN dbo.WHDWN ON   dbo.WHTRM.MANDT " +
                             "= dbo.WHDWN.MANDT AND dbo.WHTRM.COMCD = dbo.WHDWN.COMCD AND WHTRM.WERKS =dbo.WHDWN.WERKS AND WHTRM.LGORT =dbo.WHDWN.LGORT AND WHTRM.MBLNR =WHDWN.MBLNR  and " +
                             "WHDWN.matnr =whtrm.matnr  WHERE WHDWN.MANDT ='" + MANDT + "' AND WHDWN.COMCD ='" + COMCD + "' AND  MTYPE ='MODEL_E' AND   WHDWN.WERKS ='" + dtData.Rows[i]["EWERKS"].ToString() + "' AND" +
                             " WHDWN.LGORT ='" + dtData.Rows[i]["ELGORT"].ToString() + "' AND  SUBSTRING(WHDWN.MBLNR,3,14)  LIKE'" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "%'");

                //向WHRTN表中添加回冲信息
                arySQL.Add(
                    "insert into WHRTN(MANDT,COMCD,WERKS,LGORT,LOCAT,MBLNR,MATNR,MENGE,DWERK,DLGOR,DMBLN,FLAGE,CRNAM,CRDAT,REMARK)" +
                    "SELECT MANDT,COMCD,WERKS,LGORT,LOCAT,MBLNR,MATNR,MENGE,DWERK,DLGOR,DMBLN,FLAGE,CRNAM,GETDATE() AS CRDAT,'" + strRemark + "' AS REMARK from WHTRM where SUBSTRING(MBLNR,3,14) LIKE '" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "%'");

                //删除WHTRM表中间数据
                arySQL.Add("DELETE FROM WHTRM WHERE SUBSTRING(MBLNR,3,14) = ('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "')");

                //删除WHDWN表已调拨出但未接收数据
                string strSQL = string.Format(@"DELETE FROM WHDWN WHERE SUBSTRING(MBLNR,3,14) = ('" + dtData.Rows[i]["EMBLNR"].ToString().Substring(2, 14) + "')");
                arySQL.Add(strSQL);
            }
            try
            {
                ControlHandleDB();
                bolResult = ControlSqlAccess.ExecSqlArray(arySQL);
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

        #region 根据调拨明细单号获取唯一的调拨单号
        public string GetModerOrderBYItems(string MBLNR)
        {
            string ModelOrder = "";
            try
            {
                string strSql = string.Format("SELECT MBLNR FROM dbo.WHDWN WHERE  MANDT ='218' AND COMCD ='9110' AND WERKS ='{0}' AND LGORT ='{1}' and MBLNR LIKE '{2}%'", WERKS, LGORT, MBLNR);

                ControlHandleDB();
                ModelOrder = ControlSqlAccess.GetDataTable(strSql.ToString()).Rows[0]["MBLNR"].ToString();
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- GetModerOrderNoItems()";
            }
            return ModelOrder;
        }
        #endregion

        #region 获取打印模具调拨单数据
        /// <summary>
        /// 获取打印模具调拨单数据
        /// </summary>
        /// <param name="OUTWERKS"></param>
        /// <param name="OUTLGORT"></param>
        /// <param name="OUTMBLNR"></param>
        /// <param name="INWERKS"></param>
        /// <param name="INLGORT"></param>
        /// <param name="INMBLNR"></param>
        /// <returns></returns>
        public DataTable GetPrintModelData(string OUTWERKS, string OUTLGORT, string OUTMBLNR, string INWERKS, string INLGORT, string INMBLNR)
        {
            DataTable dtData = new DataTable();
            try
            {

                string strSql = string.Format("SELECT * FROM  [dbo].[V_Print_ModelOrder] ");

                if (!string.IsNullOrEmpty(OUTMBLNR) && !string.IsNullOrEmpty(OUTWERKS) &&
                    !string.IsNullOrEmpty(OUTLGORT))
                {
                    strSql += string.Format(@" WHERE  OUTWERKS ='{0}' AND OUTLGORT='{1}' AND OUTMBLNR='{2}' order by ZEILE", OUTWERKS,
                        OUTLGORT, OUTMBLNR);
                }
                else
                {
                    if (!string.IsNullOrEmpty(INWERKS) && !string.IsNullOrEmpty(INLGORT) &&
                        !string.IsNullOrEmpty(INMBLNR))
                    {
                        strSql += string.Format(@" WHERE  INWERKS ='{0}' AND INLGORT='{1}' AND INMBLNR='{2}' order by ZEILE", INWERKS,
                        INLGORT, INMBLNR);
                    }
                    else
                    {
                        throw new Exception("打印异常，缺少参数！");
                    }
                }
                ControlHandleDB();

                dtData = ControlSqlAccess.GetDataTable(strSql.ToString());

                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ModelTransforOutByOrder()";
            }
            return dtData;
        }

        #endregion

        #region 模具库存查询 by Jack 20150729
        public DataTable ModelInventoryQuery(string strWerks, string strLgort, string strLocat, string strMatnr, string strMblnr)
        {
            DataTable dtResult = new DataTable();
            StringBuilder strSql = new StringBuilder();
            try
            {
                strSql.AppendFormat(" SELECT i.MANDT,i.WERKS,i.LGORT,i.LOCAT,i.MATNR,i.MBLNR,i.MENGE,i.COMCD,i.MODAT,m.*,M_Assets.AssetsNo FROM WHITM i WITH(NOLOCK) inner join M_Models m on m.ModelNO=i.MATNR left join M_Assets on i.MATNR=M_Assets.ModelNO WHERE WERKS='{0}' AND LGORT='{1}' ", strWerks, strLgort);
                if (!string.IsNullOrEmpty(strLocat))
                {
                    strSql.AppendFormat(" AND LOCAT='{0}' ", strLocat);
                }
                if (!string.IsNullOrEmpty(strMatnr))
                {
                    strSql.AppendFormat(" AND MATNR='{0}' ", strMatnr);
                }
                if (!string.IsNullOrEmpty(strMblnr))
                {
                    strSql.AppendFormat(" AND MBLNR='{0}' ", strMblnr);
                }

                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(strSql.ToString().Trim());
                ControlSqlAccess.CloseConnection();
            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ModelInventoryQuery()";
            }
            return dtResult;
        }
        #endregion

        #region 模具库存查询--批量 by Freeman
        public DataTable ModelInventoryQuery(string strWerks, string strLgort, string strLocat, DataTable dtData)
        {
            DataTable dtResult = new DataTable();
            string strMatnr = "";

            try
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strMatnr += dtData.Rows[i]["ModelNo"].ToString().Trim() + "','";
                }
                strMatnr = strMatnr.Substring(0, strMatnr.Length - 3);

                string strSql = string.Format(@" SELECT i.MANDT,i.WERKS,i.LGORT,i.LOCAT,i.MATNR,i.MBLNR,i.MENGE,i.COMCD,i.MODAT,m.*,M_Assets.AssetsNo FROM WHITM i WITH(NOLOCK) inner join M_Models m on m.ModelNO=i.MATNR left join M_Assets on i.MATNR=M_Assets.ModelNO 
                                WHERE WERKS='" + strWerks + "' AND LGORT='" + strLgort + "' AND LOCAT='" + strLocat + "' AND MATNR IN('" + strMatnr + "')");

                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(strSql.ToString().Trim());
                ControlSqlAccess.CloseConnection();

            }
            catch (System.Exception ex)
            {
                ERRMSG = ex.Message + "<- ModelInventoryQuery()";
            }
            return dtResult;
        }
        #endregion

        #region Add By Michael 20150730 for 查询
        //查询异动记录
        public DataTable QueryLogData(string strCgcls, string strUsrnm, string strStartDate, string strEndDate, DataTable dtData, string strStartLocat, string strEndLocat, string strStartMblnr, string strEndMblnr, string strQueryTable)
        {
            try
            {
                StringBuilder strSQL = new StringBuilder();
                string strMatnr = "";

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strMatnr += dtData.Rows[i]["ModelNo"].ToString().Trim() + "','";
                }
                strMatnr = strMatnr.Substring(0, strMatnr.Length - 3);

                strSQL.AppendFormat("SELECT LOG.*, ITEMNAME FROM {0} LOG WITH(NOLOCK) ", strQueryTable);
                strSQL.Append("  LEFT JOIN M_MODELS ON MATNR = MODELNO ");
                strSQL.AppendFormat("WHERE MANDT = '{0}' AND COMCD = '{1}' AND WERKS = '{2}' AND LGORT = '{3}' ", UserData.Client, UserData.CompanyCode, WERKS, LGORT);

                if (strCgcls != "")
                {
                    strSQL.AppendFormat("  AND CGCLS = '{0}' ", strCgcls);
                }

                if (strUsrnm != "")
                {
                    strSQL.AppendFormat("  AND LOG.CRNAM = '{0}' ", strUsrnm);
                }

                if (strStartDate != "" && strEndDate != "")
                {
                    strSQL.AppendFormat("  AND (LOG.CRDAT BETWEEN '{0}' AND '{1}') ", strStartDate, strEndDate);
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    strSQL.AppendFormat("  AND LOG.CRDAT = '{0}' ", strEndDate);
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    strSQL.AppendFormat("  AND LOG.CRDAT = '{0}' ", strStartDate);
                }

                if (strMatnr != "")
                {
                    strSQL.AppendFormat("  AND MATNR IN('" + strMatnr + "')");
                }

                if (strStartLocat != "" && strEndLocat != "")
                {
                    strSQL.AppendFormat("  AND ((OLOCA BETWEEN '{0}' AND '{1}') OR (NLOCA BETWEEN '{0}' AND '{1}'))", strStartLocat, strEndLocat);
                }
                else if (strStartLocat == "" && strEndLocat != "")
                {
                    strSQL.AppendFormat("  AND (OLOCA = '{0}' OR NLOCA = '{0}') ", strEndLocat);
                }
                else if (strStartLocat != "" && strEndLocat == "")
                {
                    strSQL.AppendFormat("  AND (OLOCA = '{0}' OR NLOCA = '{0}') ", strStartLocat);
                }

                if (strStartMblnr != "" && strEndMblnr != "")
                {
                    strSQL.AppendFormat("  AND (MBLNR BETWEEN '{0}' AND '{1}') ", strStartMblnr, strEndMblnr);
                }
                else if (strStartMblnr == "" && strEndMblnr != "")
                {
                    strSQL.AppendFormat("  AND MBLNR LIKE '{0}%' ", strEndMblnr);
                }
                else if (strStartMblnr != "" && strEndMblnr == "")
                {
                    strSQL.AppendFormat("  AND MBLNR LIKE '{0}%' ", strStartMblnr);
                }

                DataTable dtResult = new DataTable();
                try
                {
                    ControlHandleDB();
                    dtResult = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    ERRMSG = ex.Message + "<- QueryLogData()";
                }
                return dtResult;
            }
            catch (Exception ex)
            {
                ERRMSG = ex.Message + "<- QueryLogData()";
                throw new Exception("999");
            }
        }

        //查询储位资料
        public DataTable QueryDetailCountingData(string strInsmk, string strStartLocat, string strEndLocat, string strStartMatnr, string strEndMatnr, string strStartDate, string strEndDate, string strMblnr, string strIsCombine, string strRegon, string strVendorCode)
        {
            try
            {
                # region Code here
                string strSQL = "";
                //if (strIsCombine == "Y")//按料号合并
                //{
                //    strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, KDMAT, RMANO, PKDAT, ITEMNAME  from WHITM WITH(NOLOCK) LEFT JOIN M_MODELS ITM WITH(NOLOCK) ON MATNR = MODELNO where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                //}
                //else
                //{
                //    strSQL = "Select ITM.*, PKDAT, ITEMNAME from WHITM ITM WITH(NOLOCK) LEFT JOIN M_MODELS WITH(NOLOCK) ON MATNR = MODELNO where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                //}
                if (strIsCombine == "Y")//按料号合并
                {
                    strSQL = "Select MANDT, COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, sum(MENGE+BKQTY) as MENGE, sum(BKQTY) as BKQTY, '' as MBLNR, '' as INDAT, KDMAT, RMANO, PKDAT, ITEMNAME, BU, ASSETSNO  from WHITM ITM WITH(NOLOCK) LEFT JOIN M_MODELS MOD WITH(NOLOCK) ON MATNR = MOD.MODELNO LEFT JOIN M_ASSETS ASS WITH(NOLOCK) ON MATNR = ASS.MODELNO where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                }
                else
                {
                    strSQL = "Select ITM.*, PKDAT, ITEMNAME, BU, ASSETSNO from WHITM ITM WITH(NOLOCK) LEFT JOIN M_MODELS MOD WITH(NOLOCK) ON MATNR = MOD.MODELNO LEFT JOIN M_ASSETS ASS WITH(NOLOCK) ON MATNR = ASS.MODELNO where MANDT= '" + MANDT + "' AND COMCD='" + COMCD + "' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "'";
                }

                if (strInsmk != "")
                {
                    strSQL += " and INSMK= '" + strInsmk + "'";
                }

                if (strMblnr != "")
                {
                    strSQL += " and MBLNR= '" + strMblnr + "'";
                }

                if (strVendorCode != "")
                {
                    strSQL += " and LIFNR= '" + strVendorCode + "'";
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
                    strSQL += " and (ITM.CRDAT between '" + strStartDate + "' and '" + strEndDate + "  23:59:59')";
                }
                else if (strStartDate == "" && strEndDate != "")
                {
                    strSQL += " and ITM.CRDAT = '" + strEndDate + "'";
                }
                else if (strStartDate != "" && strEndDate == "")
                {
                    strSQL += " and ITM.CRDAT = '" + strEndDate + "'";
                }
                if (strIsCombine == "Y")//料号合并
                {
                    strSQL += " group by MANDT,COMCD, WERKS, LGORT, LOCAT, MATNR, INSMK, CHARG, KDMAT, RMANO, PKDAT, ITEMNAME order by LOCAT, MATNR";
                }
                else
                {
                    strSQL += " order by LOCAT, MATNR";
                }

                DataTable dtData = new DataTable();
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                ControlSqlAccess.CloseConnection();

                //加总数量
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
        #endregion

        # region 检查模具是否存在
        public bool CheckExistedAssetsModel(string strModels, string strAssets)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckExistedAssetsModel";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();

            try
            {
                if (strAssets != "")
                {
                    string strSQL =
                        string.Format("Select * from dbo.M_Assets where ModelNO= '{0}'and AssetsNO='{1}'", strModels,
                            strAssets);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                else
                {
                    string strSQL =
                        string.Format("Select * from dbo.M_Assets where ModelNO= '{0}'", strModels);

                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
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
            if (dtData.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        # endregion

        # region 检查重复是否存在重复模具
        public bool CheckRepeatAssetsModel(string strLocat, DataTable dtStorage)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckExistedAssetsModel";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            DataTable dtData = new DataTable();
            DataTable dtData1 = new DataTable();

            bool boolreturn = false;

            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {

                string strSQL = string.Format("Select * from dbo.WHITM where MATNR= '{0}'", dtStorage.Rows[i]["ModelNO"].ToString());
                string strSQL2 = string.Format("Select * from dbo.WHTRM where MATNR= '{0}'", dtStorage.Rows[i]["ModelNO"].ToString());

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    dtData1 = ControlSqlAccess.GetDataTable(strSQL2.ToString());
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
            if (dtData.Rows.Count > 0 || dtData1.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        # endregion

        # region 查询模具--批量
        public DataTable QueryModelData(string strLocat, DataTable dtImport)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryModelData";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {

                DataTable dtData = new DataTable();
                string ModelNoList = "";
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {

                    ModelNoList += "'" + dtImport.Rows[i]["ModelNo"].ToString().Trim() + "',";


                }
                if (ModelNoList != "")
                {
                    string strSQL =
                        string.Format("select '" + WERKS + "'as WERKS,'" + LGORT + "'as LGORT,'" + strLocat +
                                      "'as LOCAT,a.AssetsNo,m.ModelNO,m.ItemName,m.BU,m.Machine,m.Quantity,m.Nweight,m.PoNo,m.Remark,m.CRNAM,m.CRDAT,m.MODNM,m.MODTM from dbo.M_Models m left outer join dbo.M_Assets a on m.ModelNO=a.ModelNO where a.ModelNO in(" +
                                      ModelNoList.Substring(0, ModelNoList.Length - 1) + ")");

                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                }
                else
                {
                    string strSQL =
                        string.Format("select '" + WERKS + "'as WERKS,'" + LGORT + "'as LGORT,'" + strLocat +
                                      "'as LOCAT,a.AssetsNo,m.ModelNO,m.ItemName,m.BU,m.Machine,m.Quantity,m.Nweight,m.PoNo,m.Remark,m.CRNAM,m.CRDAT,m.MODNM,m.MODTM from dbo.M_Models m left outer join dbo.M_Assets a on m.ModelNO=a.ModelNO where a.ModelNO='" +
                                      ModelNoList + "'");
                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                }
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

        # region 查询模具
        public DataTable QueryModelData(string strLocat, string strModels, string strAssets)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryModelData";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataTable dtData = new DataTable();

                if (strAssets != "")
                {
                    string strSQL =
                        string.Format("select '" + WERKS + "'as WERKS,'" + LGORT + "'as LGORT,'" + strLocat +
                                      "'as LOCAT,a.AssetsNo,m.ModelNO,m.ItemName,m.BU,m.Machine,m.Quantity,m.Nweight,m.PoNo,m.Remark,m.CRNAM,m.CRDAT,m.MODNM,m.MODTM from dbo.M_Models m left outer join dbo.M_Assets a on m.ModelNO=a.ModelNO where a.ModelNO in(" + strModels + ") and a.AssetsNo='" + strAssets + "'");
                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                }
                else
                {
                    string strSQL =
                        string.Format("select '" + WERKS + "'as WERKS,'" + LGORT + "'as LGORT,'" + strLocat +
                                      "'as LOCAT,a.AssetsNo,m.ModelNO,m.ItemName,m.BU,m.Machine,m.Quantity,m.Nweight,m.PoNo,m.Remark,m.CRNAM,m.CRDAT,m.MODNM,m.MODTM from dbo.M_Models m left outer join dbo.M_Assets a on m.ModelNO=a.ModelNO where a.ModelNO='" +
                                      strModels + "'");
                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();

                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                }
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

        #region  離線入庫

        public bool ModelOffLineInData(string strWerks, string strLgort, string strLocat, DataTable dtStorage)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "AddOffLineInData";
            this.ControlMethodParm = "(" + strLocat + "," + strWerks + "," + strLgort + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            ArrayList arySQL = new ArrayList();

            //WHLOG
            QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
            arySQL = objLogData.AddLogData("", "", dtStorage);



            //WHITM
            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {


                string strSQL = string.Format(@"insert into dbo.WHITM(MANDT,COMCD,WERKS,LGORT,LOCAT,MATNR,INDAT,MENGE,QCQTY,RMAK1,CRNAM,CRDAT,MONAM,MODAT) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}',N'{9}','{10}',{11},'{12}',{13})",
                    MANDT, COMCD, strWerks, strLgort, strLocat, dtStorage.Rows[i]["ModelNO"].ToString(), "GetDate()", dtStorage.Rows[i]["Quantity"].ToString(), "0", dtStorage.Rows[i]["Remark"].ToString(), dtStorage.Rows[i]["CRNAM"].ToString(), "GetDate()", UserData.UserId, "GetDate()");

                arySQL.Add(strSQL.ToString());

            }

            #region "Update WHHED Table"
            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {

                sbSql.Append("Update WHHED set  ");
                sbSql.AppendFormat("  LOSTS=T.TOTAL from ");
                sbSql.AppendFormat(" (Select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + strLocat + "') as T  ");
                sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                sbSql.AppendFormat(" and WERKS='{0}' ", WERKS);
                sbSql.AppendFormat(" and LGORT='{0}' ", LGORT);
                sbSql.AppendFormat(" and LOCAT='{0}' ", strLocat);
                arySQL.Add(sbSql.ToString());

                //sbSql.Remove(0, sbSql.Length);
                //sbSql.Append("Update WHHED set  ");
                //sbSql.AppendFormat("  ISMRG=T.ISMRG from ");
                //sbSql.AppendFormat(" (Select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + strLocat + "' and MRGID<>'') as T  ");
                //sbSql.AppendFormat(" Where MANDT='{0}' ", MANDT);
                //sbSql.AppendFormat(" and COMCD='{0}' ", COMCD);
                //sbSql.AppendFormat(" and WERKS='{0}' ", WERKS);
                //sbSql.AppendFormat(" and LGORT='{0}' ", LGORT);
                //sbSql.AppendFormat(" and LOCAT='{0}' ", strLocat);
                //arySQL.Add(sbSql.ToString());
                // arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case when count(*)>0 then '1' else '0' end  from whitm where MANDT= '" + MANDT + "' and COMCD='"+COMCD+"'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "') as T where MANDT= '" + MANDT + "' and COMCD='"+COMCD+"' and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
                //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case when count(*)>0 then 'Y' else 'N' end  from whitm where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'  and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "' and MRGID<>'') as T where MANDT= '" + MANDT + "' and COMCD='" + COMCD + "'and WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + aryLocat[i].ToString() + "'");
            }
            #endregion

            bool bolReturn = false;
            try
            {
                ControlHandleDB();
                bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
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

        #endregion

        # region 查询库中模具
        /// <summary>
        /// 查询库中模具
        /// </summary> 	
        public DataTable QueryStorageModelData(string strWerks, string strLgort, DataTable dtStorage)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryModelData";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataTable dtData = new DataTable();

                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {

                    string strSQL = string.Format(@"select * from WHITM inner join M_Assets on WHITM.MATNR=M_Assets.ModelNO where WHITM.MATNR='" + dtStorage.Rows[i]["ModelNO"].ToString() + "' and M_Assets.AssetsNo='" + dtStorage.Rows[i]["AssetsNo"].ToString() + "'and WHITM.WERKS='" + strWerks + "'and WHITM.LGORT='" + strLgort + "'");

                    try
                    {
                        ControlHandleDB();
                        dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                        ControlSqlAccess.CloseConnection();
                    }
                    catch (System.Exception ex)
                    {
                        //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                        //throw new System.Exception(ex.Message +"<- QueryLogData " );

                        ERRMSG = ex.Message + "<- QueryLogData()";
                    }
                }
                //DataSet dsData = new DataSet();

                //dsData.Tables.Add(dtData);
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

        #region 離線出庫

        public bool ModelOffLineOutData(DataTable dtOutSource, DataTable dtStorage)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "AddOffLineOutData_DateCode";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            bool bolReturn = false;
            try
            {

                ArrayList arySQL = new ArrayList();
                ArrayList aryLocat = new ArrayList();
                string strTempLocat = "";

                //WHLOG
                QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, WERKS, LGORT, strProgid);
                arySQL = objLogData.AddLogData("", "", dtStorage);

                //WHITM
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    //記錄所有的異動儲位
                    if (strTempLocat.IndexOf(dtStorage.Rows[i]["LOCAT"].ToString()) < 0)
                    {
                        aryLocat.Add(dtStorage.Rows[i]["LOCAT"].ToString());
                        strTempLocat += "++" + dtStorage.Rows[i]["LOCAT"].ToString();

                    }
                    //全部出庫完畢
                    if (dtStorage.Rows[i]["MENGE"].ToString() == dtStorage.Rows[i]["ALQTY"].ToString())
                    {
                        string strSQL = string.Format(@"Delete from WHITM where WERKS= '" + WERKS + "' and LGORT= '" + LGORT + "' and LOCAT= '" + dtStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR= '" + dtStorage.Rows[i]["MATNR"].ToString() + "'and INSMK= '" + dtStorage.Rows[i]["INSMK"].ToString() + "' and MBLNR= '" + dtStorage.Rows[i]["MBLNR"].ToString() + "' and CHARG= '" + dtStorage.Rows[i]["CHARG"].ToString() + "'");


                        arySQL.Add(strSQL.ToString());
                    }

                }
                //WHHED
                for (int i = 0; i < aryLocat.Count; i++)
                {
                    arySQL.Add("update WHHED set  LOSTS=T.TOTAL from (select TOTAL=case  when count(*)>0 then '1' else '0' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'  ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                    //arySQL.Add("update WHHED set  ISMRG=T.ISMRG from (select ISMRG=case  when count(*)>0 then 'Y' else 'N' end  from whitm WITH(NOLOCK) where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "' and MRGID<>''   ) as T where MANDT= '" + strMandt + "' and COMCD='" + COMCD + "'  and WERKS= '" + strWerks + "'  and LGORT= '" + strLgort + "' and LOCAT= '" + aryLocat[i] + "'");
                }

                //bolReturn = this.objSQLAccess.ExecSQLArray(arySQL);
                ControlHandleDB();
                bolReturn = ControlSqlAccess.ExecSqlArray(arySQL);
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

        #endregion

        #region 储位调整查询
        public DataTable QueryStorageDetailData(string strLocat, string strIsmrg)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "QueryModelData";
            this.ControlMethodParm = "('')";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            try
            {
                DataTable dtData = new DataTable();

                string strSQL = string.Format(@"select MANDT,WERKS,LGORT,LOCAT,MATNR,INSMK,MBLNR AS OMBLNR,'' as MBLNR,'' as ZEILE,'' as KOSTL,'' as ARBPL,'' as TRNTP,CHARG,LIFNR,RMANO,EBELN,INDAT,MENGE,MENGE AS ALQTY,QCQTY,REFNO,MRGID,ISPTM,REQTY,KDMAT,RMAK1,WHITM.CRNAM,Convert(varchar(20), WHITM.CRDAT, 120) as CRDAT,MONAM,MODAT,SERNO,LOCOD,INSPT,COMCD,BKQTY,DACOD,VEDAT,BOXID,NLOCA,SIDNO,REFID,SEQNO,PKDAT,M_Assets.AssetsNo from WHITM inner join M_Assets on WHITM.MATNR=M_Assets.ModelNO where WERKS = '" + WERKS + "' and LGORT = '" + LGORT + "'");

                if (strLocat != "")
                {
                    strSQL += " and LOCAT= '" + strLocat + "'";
                }

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                }
                catch (System.Exception ex)
                {
                    //this.objSQLAccess.ErrorMessage = ex.Message + "<- QueryLogData ";
                    //throw new System.Exception(ex.Message +"<- QueryLogData " );

                    ERRMSG = ex.Message + "<- QueryLogData()";
                }

                //DataSet dsData = new DataSet();

                //dsData.Tables.Add(dtData);
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
        #endregion


    }

}
