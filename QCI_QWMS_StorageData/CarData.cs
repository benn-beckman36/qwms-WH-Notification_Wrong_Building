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

namespace QCI_QWMS_StorageData
{
    public class CarData : ControlBase
    {
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLgortNew = "";
        private string strErrmsg = "";

        bool flg = false;
        DataTable dtData = new DataTable();
        DataTable dtResult = new DataTable();

        #region Constructer

        public CarData()
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
        public CarData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strWerks, string strLgort)
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
            ControlErrorInfo.ObjectName = "QWMS.CarData";
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

        public CarData(UserInfo varUserData, string strWerks, string strLgort)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strWerks, strLgort)
        {
        }

        public CarData(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, "", "")
        {
        }

        public CarData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
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
        /// Company code。
        /// </summary>
        /////////////////////////////////////////////////////////////////////////////
        public string COMCD
        {
            get { return strComcd; }
            set { strComcd = value; }
        }

        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 倉別。--Old
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

        #endregion

        #region MemberFunction

        #region 新增车辆信息

        public bool AddCar(string name, string carID, string driverID, string tel, string imgPath, string jobID)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "AddCar";
            this.ControlMethodParm = "(" + name + "," + carID + "," + driverID + "," + tel + "," + imgPath + "," + jobID + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = "INSERT INTO WHCTRL (MANDT,SOLDTO,CTRLID,CTRLNM,CTRLC1,CTRLC2,CTRLC3,CTRLC4,CTRLC5,COMCD,REMAK)VALUES('218','QWMS','CarInfo','Driver',N'" + name + "','" + tel + "','" + jobID + "','" + driverID + "',N'" + carID + "','" + COMCD + "',N'" + imgPath + "')";
            bool flg;
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 删除车辆信息
        public bool DeleteCar(string jobID)
        {

            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "DeleteCar";
            this.ControlMethodParm = "(" + jobID + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string sql;
            bool flg;
            sql = "DELETE FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='CarInfo'  AND CTRLNM='Driver' AND CTRLC3='" + jobID + "'";
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 修改车辆信息
        public bool ModifyCar(string carID, string name, string dirverID, string tel, string imgPath, string jobID)
        {

            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "ModifyCar";
            this.ControlMethodParm = "(" + name + "," + carID + "," + dirverID + "," + tel + "," + imgPath + "," + jobID + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();
            bool flg;
            sbSql.AppendFormat("UPDATE WHCTRL SET   CTRLC1=N'{0}' , CTRLC2='{1}',CTRLC5=N'{2}' ,CTRLC4='{3}' ,REMAK=N'{4}' WHERE  MANDT='218' AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver' AND CTRLC3='" + jobID + "' ", name, tel, carID, dirverID, imgPath, jobID);
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

        #region 查询车辆信息

        public DataTable QueryCarInfo(string carNo, string name, string dirverID, string tel, string jobID)
        {
            this.ControlMethodName = "QueryCarInfo";
            this.ControlMethodParm = "(" + name + "," + carNo + "," + dirverID + "," + tel + "," + jobID + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" SELECT CTRLC1 AS DRINAME,CTRLC2 AS TEL,CTRLC3 AS JOBID,CTRLC4 AS DRID ,CTRLC5 AS CARNO,REMAK AS IMGPATH,COMCD FROM WHCTRL"
                + " WHERE  MANDT='218' AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver'  ");
            if (carNo != "")
            {
                sbSql.Append("AND CTRLC5 = N'" + carNo + "'");
            }
            if (name != "")
            {
                sbSql.Append("AND CTRLC1=N'" + name + "'");
            }
            if (dirverID != "")
            {
                sbSql.Append("AND CTRLC4='" + dirverID + "'");
            }
            if (tel != "")
            {
                sbSql.Append("AND CTRLC2='" + tel + "'");
            }
            if (jobID != "")
            {
                sbSql.Append("AND CTRLC3='" + jobID + "'");
            }
            sbSql.Append("ORDER BY CTRLC5,CTRLC1");
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

        #region 下拉框车辆信息

        public DataTable GetCarData()
        {

            this.ControlMethodName = "GetCarData";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string sql;

            sql = "Select distinct CTRLC5 as F_TEXT, CTRLC1 as F_VALUE from WHCTRL WHERE MANDT='218'AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver' ";
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sql.ToString());
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

        #region 确认新增车辆与司机信息是否已经存在
        public bool CheckExist(string jobID, string CarNo)
        {
            //設定要記錄Error Message的相關訊息
            this.ControlMethodName = "CheckExist";
            this.ControlMethodParm = "(" + jobID + "," + CarNo + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = "SELECT CTRLC3,CTRLC5 FROM WHCTRL WHERE  MANDT='218' AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver' AND CTRLC3='" + jobID + "'  AND CTRLC5=N'" + CarNo + "'";
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sql.ToString());
                ControlSqlAccess.CloseConnection();
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

        #region 查询厂房信息

        public DataTable QueryBuilding(string werks)
        {
            this.ControlMethodName = "QueryBuilding";
            this.ControlMethodParm = "(" + werks + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("Select distinct CTRLC1 as F_TEXT, CTRLNM as F_VALUE from WHCTRL where MANDT= '" + strMandt + "' and COMCD='" + strComcd + "'  and SOLDTO='QWMS' and CTRLID='Building' ");

            if (werks != "")
            {
                sbSQL.Append("AND CTRLNM='" + werks + "'");
            }

            sbSQL.Append(" ORDER BY CTRLNM,CTRLC1");

            try
            {
                ControlHandleDB();
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

        #region 查询码头信息

        public DataTable QueryPort(string werks, string house)
        {
            this.ControlMethodName = "QueryPort";
            this.ControlMethodParm = "(" + werks + "," + house + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT SOLDTO,CTRLID,CTRLNM,CTRLC1,CTRLC2  AS  F_TEXT FROM WHCTRL  WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='Port' ");

            if (werks != "")
            {
                sbSQL.Append("AND CTRLNM='" + werks + "'");
            }
            if (house != "")
            {
                sbSQL.Append("AND CTRLC1='" + house + "'");
            }
            sbSQL.Append(" ORDER BY CTRLNM,CTRLC1,CTRLC2");

            try
            {
                ControlHandleDB();
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


        #region 确认新增码头是否已经存在

        public bool CheckPort(string werks, string house, string port)
        {
            this.ControlMethodName = "CheckPort";
            this.ControlMethodParm = "(" + werks + "," + house + "," + port + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT * FROM WHCTRL  WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='Port' AND CTRLNM='" + werks + "' AND CTRLC1='" + house + "' AND CTRLC2='" + port + "'");
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                ControlSqlAccess.CloseConnection();
                if (dtData.Rows.Count <= 0)
                {
                    flg = true;
                }
                else
                {
                    flg = false;
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
            return flg;
        }

        #endregion

        #region 新增码头信息

        public bool AddPort(string werks, string house, string port)
        {
            this.ControlMethodName = "AddPort";
            this.ControlMethodParm = "(" + port + "," + house + "," + werks + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = "INSERT INTO WHCTRL (MANDT,SOLDTO,CTRLID,CTRLNM,CTRLC1,CTRLC2,COMCD)VALUES('218','QWMS','Port','" + werks + "','" + house + "','" + port + "','" + COMCD + "') ";
            bool flg;
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 删除码头信息
        public bool DeletePort(string werks, string house, string port)
        {
            this.ControlMethodName = "DeletePort";
            this.ControlMethodParm = "(" + port + "," + house + "," + werks + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = "DELETE FROM WHCTRL WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='Port' AND CTRLNM='" + werks + "' AND CTRLC1='" + house + "' AND CTRLC2='" + port + "'";
            bool flg;
            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 根据车牌号获取司机名字


        public DataTable QueryDriverByCarNo(string carno)
        {
            this.ControlMethodName = "QueryDriverByCarNo";
            this.ControlMethodParm = "(" + carno + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }

            string sql;

            sql = "Select CTRLC1 AS DriverName  from WHCTRL WHERE MANDT='218'AND SOLDTO='QWMS' AND CTRLID='CarInfo' AND CTRLNM='Driver'  AND CTRLC5=N'" + carno + "' ORDER BY  CTRLC1";
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sql.ToString());
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

        #region 查询调拨单号绑定码头信息

        public DataTable QueryBindPortInfo(string mblnr, string fwerks, string dwerks, string fPort, string dPort, string reqTime, string cartons, string pallets)
        {
            this.ControlMethodName = "QueryBindPortInfo";
            this.ControlMethodParm = "(" + mblnr + "," + fwerks + "," + dwerks + "," + fPort + "," + dPort + "," + reqTime + "," + cartons + "," + pallets + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT ROW_NUMBER()OVER(ORDER BY A.MBLNR)AS Item, A.MANDT, A.COMCD, A.FWERKS, A.FLGORT, A.DWERKS, A.DLGORT, A.MBLNR, A.ZEILE, A.MATNR,"
+ "A.PO,A.BWART,A.MATNR, A.CHARG, A.MENGE, A.CARTONS, A.PALLETS,A.STATUS, A.FPORT,A.DPORT,A.REQTIM,A.CARNO,A.DIRVER,A.SEALNO,A.SEDTIM,A.CREWHO,A.CRETIME,A.BINDNO "
+ "from [dbo].[TRDWN] A  WITH (NOLOCK) WHERE A.STATUS IN('N','WP') AND A.ZEILE =(SELECT MIN(ZEILE) FROM TRDWN b  WITH (NOLOCK) WHERE b.MBLNR= a.MBLNR GROUP BY b.MBLNR)");

            if (mblnr != "")
            {
                sbSQL.Append("AND MBLNR='" + mblnr + "'");
            }
            if (fwerks != "")
            {
                sbSQL.Append("AND FWERKS='" + fwerks + "'");
            }
            if (dwerks != "")
            {
                sbSQL.Append("AND DWERKS='" + dwerks + "'");
            }
            if (fPort != "")
            {
                sbSQL.Append("AND FPORT='" + fPort + "'");
            }
            if (dPort != "")
            {
                sbSQL.Append("AND DPORT='" + dPort + "'");
            }
            if (reqTime != "")
            {
                sbSQL.Append("AND REQTIM='" + reqTime + "'");
            }
            if (cartons != "")
            {
                sbSQL.Append("AND CARTONS='" + cartons + "'");
            }
            if (pallets != "")
            {
                sbSQL.Append("AND PALLETS='" + pallets + "'");
            }

            try
            {
                ControlHandleDB();
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

        #region 删除码头绑定信息
        public bool DeleteTransferDataForPortInfo(string mblnrs)
        {
            bool flg = true;
            this.ControlMethodName = "DeleteTransferDataForPortInfo";
            this.ControlMethodParm = "(" + mblnrs + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("UPDATE TRDWN SET STATUS='N',FPORT='' ,DPORT='' ,PALLETS='' ,REQTIM=NULL,CARTONS=''  WHERE MBLNR IN(" + mblnrs + ") AND  STATUS IN ('WP') ");

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
            return flg;
        }


        #endregion

        #region 查询调拨单号绑定车辆信息

        public DataTable QueryBindCarInfo(string truckOrder, string mblnr, string sealNo, string sendTime, string carNo, string driver)
        {
            this.ControlMethodName = "QueryBindCarInfo";
            this.ControlMethodParm = "(" + mblnr + "," + sealNo + "," + sendTime + "," + carNo + "," + driver + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT ROW_NUMBER()OVER( ORDER BY A.MBLNR)AS Item, A.MANDT, A.COMCD, A.FWERKS, A.FLGORT, A.DWERKS,A.FPORT+'-'+ A.DPORT AS Transferroute,"
+ "A.DLGORT,A.ORDERNO,A.MBLNR, A.ZEILE, A.MATNR,A.PO,A.BWART,A.MATNR, A.CHARG, A.MENGE, A.CARTONS, A.PALLETS,A.STATUS, A.FPORT,A.DPORT,"
+ "A.REQTIM,A.CARNO,A.DIRVER,A.SEALNO,A.SEDTIM,A.CREWHO,A.CRETIME,'" + UserData.UserId.ToString() + "' as SPACOUNT,A.BINDNO"
+ " from [dbo].[TRDWN] A WITH (NOLOCK) WHERE A.STATUS IN('WP','AT') AND ZEILE =(SELECT MIN(ZEILE) FROM TRDWN B WITH (NOLOCK) WHERE B.MBLNR= A.MBLNR GROUP BY B.MBLNR)");

            if (truckOrder != "")
            {
                sbSQL.Append(" AND ORDERNO='" + truckOrder + "'");
            }

            if (mblnr != "")
            {
                sbSQL.Append(" AND MBLNR='" + mblnr + "'");
            }
            if (sealNo != "")
            {
                sbSQL.Append("AND SEALNO='" + sealNo + "'");
            }
            if (sendTime != "")
            {
                sbSQL.Append("AND SEDTIM='" + sendTime + "'");
            }
            if (carNo != "")
            {
                sbSQL.Append("AND CARNO='" + carNo + "'");
            }
            if (driver != "")
            {
                sbSQL.Append(" AND DIRVER='" + driver + "'");
            }

            try
            {
                ControlHandleDB();
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

        #region 修改绑定车辆信息
        public bool UpdateTransferCarInfo(string truckOrder, string sealNo, string sendTime, string carNo, string driver)
        {
            this.ControlMethodName = "UpdateTransferCarInfo";
            this.ControlMethodParm = "(" + truckOrder + "," + sealNo + "," + sendTime + "," + carNo + "," + driver + ")";
            bool flg;

            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("UPDATE TRDWN SET STATUS='AT', CARNO=N'" + carNo + "',DIRVER=N'" + driver + "',SEDTIM='" + sendTime + "',SEALNO='" + sealNo + "' WHERE ORDERNO='" + truckOrder + "' AND STATUS IN ('AT','WP')");

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
            return flg;
        }
        #endregion


        #region 打印完调拨单修改调拨单的状态
        public bool UpdateTransferForPrint(string mblnr, string printer)
        {
            this.ControlMethodName = "UpdateTransferForPrint";
            this.ControlMethodParm = "(" + mblnr + ")";
            bool flg;

            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("UPDATE TRDWN SET STATUS='WW',SPACOUNT='" + printer + "',SPRINT=GETDATE() WHERE MBLNR='" + mblnr + "'");

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
            return flg;
        }
        #endregion

        #region 追加绑定车辆信息
        public bool AddToBindTransferDataForCarInfo(string truckOrder, string mblnr, string sealNo, string sendTime, string carNo, string driver)
        {
            this.ControlMethodName = "AddToBindTransferDataForCarInfo";
            this.ControlMethodParm = "(" + truckOrder + "," + mblnr + "," + sealNo + "," + sendTime + "," + carNo + "," + driver + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("UPDATE TRDWN SET STATUS='AT', CARNO=N'" + carNo + "',DIRVER=N'" + driver + "',SEDTIM='" + sendTime + "',SEALNO='" + sealNo + "', ORDERNO='" + truckOrder + "'  WHERE  STATUS IN ('WP') AND MBLNR='" + mblnr + "'");

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
            return flg;
        }
        #endregion


        #region 绑定车辆信息
        public DataTable BindTransferDataForCarInfo(string mblnrs, string sealNo, string sendTime, string carNo, string driver, string orderNum)
        {
            this.ControlMethodName = "BindTransferDataForCarInfo";
            this.ControlMethodParm = "(" + mblnrs + "," + sealNo + "," + sendTime + "," + carNo + "," + driver + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            try
            {
                ControlHandleDB();
                sbSQL.Append("EXEC sp_Transfer_BindCarInfo  '" + mblnrs + "','" + sealNo + "','" + sendTime + "',N'" + carNo + "',N'" + driver + "','" + orderNum + "'");
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
            return dtResult;
        }
        #endregion

        #region 删除车辆绑定信息
        public bool DeleteTransferDataForCarInfo(string truckOrder)
        {
            bool flg = true;
            this.ControlMethodName = "DeleteTransferDataForCarInfo";
            this.ControlMethodParm = "(" + truckOrder + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append("UPDATE TRDWN SET STATUS='WP',CARNO='',DIRVER='',SEALNO='',SEDTIM='' ,ORDERNO=''  WHERE ORDERNO='" + truckOrder + "' AND STATUS IN ('AT') ");

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
            return flg;
        }


        #endregion

        #region 绑定码头信息

        public DataTable UpdateTransferDataForPortInfo(string mblnr, string fromPort, string destinationPort, string cartons, string reqTime, string pallets, string bindNo)
        {
            this.ControlMethodName = "UpdateTransferDataForPortInfo";
            this.ControlMethodParm = "(" + mblnr + "," + fromPort + "," + destinationPort + "," + cartons + "," + reqTime + "," + pallets + "," + bindNo + ")";

            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();

            try
            {
                ControlHandleDB();
                sbSQL.Append("EXEC sp_Transfer_BindPortInfo_new  '" + mblnr + "','" + fromPort + "','" + destinationPort + "'," + cartons + "," + pallets + ",'" + reqTime + "' , '" + bindNo + "'");
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
            return dtResult;

        }
        #endregion

        #region 查询46PO信息

        public DataTable Query46PO(string werks, string lgort, string po, string type, bool check, string PODate1, string PODate2)
        {
            this.ControlMethodName = "Query46PO";
            this.ControlMethodParm = "(" + werks + "," + lgort + "," + po + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql="";
            if (type == "SAP_351")
            {
                if (!check)//error文件导致扣账成功但是不更新
                {
                    sql += " UPDATE PODWN SET OTQTY=MENGE,FLAGE='Y',REMAK1=(SELECT TOP 1 LEFT(MBLNR,10) FROM WHDWN WITH(NOLOCK) WHERE WERKS=PODWN.WERKS AND MTYPE='SAP' AND TRNTP='T-' AND RIGHT(WHDWN.REFID,10)=PODWN.MBLNR ),MODAT=GETDATE(),MBLNR351=(SELECT TOP 1 LEFT(MBLNR,10) FROM WHDWN WITH(NOLOCK) WHERE WERKS=PODWN.WERKS AND MTYPE='SAP' AND TRNTP='T-' AND RIGHT(WHDWN.REFID,10)=PODWN.MBLNR ) WHERE  OTQTY=0 AND BWART ='351' AND FLAGE='N' AND CRDAT>DATEADD(DAY,-2,GETDATE()) AND MBLNR=( SELECT TOP 1 RIGHT(REFID,10) FROM WHDWN WITH(NOLOCK) WHERE WERKS=PODWN.WERKS AND MTYPE='SAP' AND TRNTP='T-' AND RIGHT(WHDWN.REFID,10)=PODWN.MBLNR ) ";
                }
                sql += " SELECT DISTINCT W.ZEILE AS Item,W.EBELN,W.EBELP,W.LIFNR,W.USNAM,W.MANDT,W.WERKS AS FWERKS,W.LGORT AS FLGORT,W.DWERKS,W.DLGORT ,W.MBLNR,W.MATNR,W.CHARG,W.MENGE,W.CRDAT,CASE WHEN T.OTQTY=0 AND ISNULL(W.REMAK1,'')<>'' AND W.OTQTY>0 THEN N'PO已扣帐,仓库未做出库' WHEN T.OTQTY>0 AND ISNULL(W.REMAK1,'')<>'' AND  W.OTQTY>0  THEN N'已开立调拨' ELSE N'PO未扣帐'  END MSG,W.REMAK1,W.OTQTY FROM PODWN AS W WITH(NOLOCK) LEFT JOIN WHDWN AS T WITH(NOLOCK) ON W.MANDT=T.MANDT AND W.WERKS=T.WERKS AND W.LGORT=T.LGORT AND W.MBLNR351=SUBSTRING(T.MBLNR,1,10) AND W.EBELN=T.EBELN  WHERE W.BWART='" + type.Substring(type.Length - 3, 3) + "' ";
            }
            else
            {
                if (!check)//error文件导致扣账成功但是不更新
                {
                    sql += " UPDATE T1 SET T1.OTQTY=T1.MENGE,T1.FLAGE='Y',T1.MODAT=GETDATE(),T1.REMAK1=(SELECT TOP 1 LEFT(MBLNR,10) FROM WHDWN WITH(NOLOCK) WHERE WERKS=T1.WERKS AND LGORT=T1.LGORT AND MTYPE='SAP' AND TRNTP='T-' AND RIGHT(REFID,10)=T1.MBLNR ) FROM WHDWN T1 WHERE T1.OTQTY=0 AND T1.MTYPE IN ('SAP_313','SAP_303') AND FLAGE='N' AND T1.CRDAT>DATEADD(DAY,-2,GETDATE()) AND MBLNR=( SELECT TOP 1 RIGHT(REFID,10) FROM WHDWN WITH(NOLOCK) WHERE WERKS=T1.WERKS AND MTYPE='SAP' AND TRNTP='T-' AND RIGHT(WHDWN.REFID,10)=T1.MBLNR ) ";
                }
                sql += " SELECT  DISTINCT W.ZEILE AS Item,W.LIFNR,W.USNAM,W.MANDT,WERKS AS FWERKS,LGORT AS FLGORT,KOSTL AS DWERKS,UMLGO AS DLGORT,W.MBLNR,W.MATNR,W.CHARG,W.MENGE,CRDAT,CASE WHEN ISNULL(T.MBLNR,'')='' AND ISNULL(W.REMAK1,'')<>'' AND W.OTQTY>0  THEN N'PO已扣帐,仓库未做出库' WHEN ISNULL(T.MBLNR,'')<>'' AND ISNULL(W.REMAK1,'')<>'' AND  W.OTQTY>0  THEN N'已开立调拨' ELSE N'PO未扣帐'  END MSG,W.REMAK1,W.OTQTY  FROM WHDWN AS W WITH(NOLOCK) LEFT JOIN TRDWN AS T WITH(NOLOCK) ON ISNULL(W.REMAK1,'')=T.MBLNR AND W.MBLNR=T.PO  WHERE MTYPE='" + type + "' ";
            }
            if (check)
            {
                sql += " AND W.OTQTY>0 AND W.FLAGE='Y'";
            }
            else
            {
                sql += " AND  W.OTQTY=0 ";
            }
            if (werks != "")
            {
                sql += " AND W.WERKS='" + werks + "'";
            }

            if (lgort != "")
            {
                sql += " AND W.LGORT='" + lgort + "'";
            }

            if (po != "")
            {
                sql += " AND W.MBLNR='" + po + "'";
            }
            sql += " AND W.CRDAT BETWEEN '" + PODate1 + " 00:00:01 ' AND '" + PODate2 + " 23:59:59'";
            sql += " ORDER BY  W.MANDT,W.WERKS,W.LGORT,W.MBLNR,W.ZEILE ";
            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sql.ToString());
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

        #region 手动同步46PO信息

        public DataTable synchronize46POByHand(string mblnr)
        {
            this.ControlMethodName = "synchronize46POByHand";
            this.ControlMethodParm = "(" + mblnr + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            //string sql = "EXEC [sp_CreateTranserDataManual] '" + mblnr + "','" + UserData.UserId.ToString() + "'";

            string sql = "EXEC [sp_CreateTranserDataManual]  '" + mblnr + "'";

            try
            {
                ControlHandleDB();
                dtData = ControlSqlAccess.GetDataTable(sql.ToString());
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

        #region 更新46PO仓别信息

        public bool UpdateLgort(string lgort, string pos)
        {
            this.ControlMethodName = "UpdateLgort";
            this.ControlMethodParm = "(" + lgort + "," + pos + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = " UPDATE WHDWN SET LGORT='" + lgort + "'  WHERE MTYPE='SAP_46P'   AND ( ISNULL(LGORT,'')='' OR LGORT IN('TWEE','TWEJ','TW80','TW50','RMRM','TW21') ) AND MBLNR IN (" + pos + ")";

            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 删除46PO信息

        public bool Del46PO(string po)
        {
            this.ControlMethodName = "Del46PO";
            this.ControlMethodParm = "(" + po + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            string sql = "DELETE  FROM WHDWN  WHERE MTYPE='SAP_46P'  AND  MBLNR IN (" + po + ") ";

            try
            {
                ControlHandleDB();
                flg = ControlSqlAccess.ExecSql(sql.ToString());
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

        #region 通过派车单号查询已绑定派车单获取派车信息
        public DataTable AddToCarOrder(string order)
        {
            this.ControlMethodName = "AddToCarOrder";
            this.ControlMethodParm = "(" + order + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append(" SELECT ROW_NUMBER()OVER(ORDER BY MBLNR,ZEILE) AS Item, A.MBLNR,A.ZEILE,A.MATNR,A.FWERKS,"
+ "A.DWERKS,A.FPORT,A.DPORT,A.CARNO,A.DIRVER,A.ORDERNO,A.SEDTIM FROM TRDWN  A  WITH (NOLOCK)"
+ " WHERE A.STATUS IN ('AT') AND ZEILE =(SELECT MIN(ZEILE) FROM TRDWN B WITH (NOLOCK) WHERE B.MBLNR= A.MBLNR GROUP BY B.MBLNR) AND A.ORDERNO='" + order + "'");

            try
            {
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
            return dtResult;
        }


        #endregion


        #region 查询可以删除的密码
        public DataTable GetPassword()
        {
            this.ControlMethodName = "GetPassword";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT PASWD FROM [dbo].[WHUSR]  WITH (NOLOCK) WHERE USRNM IN ('LIUY','SCS','ZWD')");

            try
            {
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
            return dtResult;
        }


        #endregion


        #region 查询调拨单的状态
        public DataTable GetTransferStatus()
        {
            this.ControlMethodName = "GetTransferStatus";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT STATUS AS F_TEXT FROM TRDWN  WITH (NOLOCK)");

            try
            {
                ControlHandleDB();
                dtResult = ControlSqlAccess.GetDataTable(sbSQL.ToString());
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
            return dtResult;
        }


        #endregion


        #region 查询所有调拨单号

        public DataTable QueryTransfer(string truckOrder, string mblnr, string fWerks, string fport, string timeFrom, string timeTo, string carNo, bool flage)
        {
            this.ControlMethodName = "QueryTransfer";
            this.ControlMethodParm = "(" + truckOrder + "," + mblnr + "," + fWerks + "," + fport + "," + timeFrom + "," + timeTo + "," + carNo + "," + flage + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("SELECT * FROM [dbo].[View_TransferQuery] WITH (NOLOCK) WHERE 1=1");

            if (truckOrder != "")
            {
                sbSQL.Append(" AND ORDERNO='" + truckOrder + "'");
            }

            if (mblnr != "")
            {
                sbSQL.Append(" AND MBLNR='" + mblnr + "'");
            }
            if (fWerks != "")
            {
                sbSQL.Append(" AND (FWERKS='" + fWerks + "' OR DWERKS='" + fWerks + "') ");
            }
            if (fport != "")
            {
                sbSQL.Append(" AND (FPORT='" + fport + "'  OR DPORT='" + fport + "' ) ");
            }

            if (carNo != "")
            {
                sbSQL.Append("AND CARNO =N'" + carNo + "'");
            }
            if (flage)
            {
                if (timeFrom != "" || timeTo != "")
                {
                    if (timeFrom == "" && timeTo != "")
                    {
                        sbSQL.Append(" AND SEDTIM <= '" + timeTo + "'");
                    }
                    if (timeFrom != "" && timeTo == "")
                    {
                        sbSQL.Append(" AND SEDTIM >='" + timeFrom + "' ");
                    }
                    else
                    {
                        sbSQL.Append(" AND SEDTIM BETWEEN '" + timeFrom + "' AND '" + timeTo + "'");
                    }
                }
            }

            sbSQL.Append("ORDER BY  CJWdate,CWHdate,ORDERNO");

            try
            {
                ControlHandleDB();
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

        #region 补印调拨单号

        public DataTable TransferAdditionalPrint(string mblnr)
        {
            this.ControlMethodName = "TransferAdditionalPrint";
            this.ControlMethodParm = "(" + mblnr + ")";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            //             sbSQL.Append(" SELECT ROW_NUMBER()OVER(PARTITION BY MBLNR ORDER BY MBLNR,ZEILE)AS Item, MANDT, COMCD, FWERKS, FLGORT, DWERKS, DLGORT, '*'+MBLNR+'*' AS MBLNR, ZEILE, MATNR,PO,BWART,CHARG, MENGE, CARTONS, PALLETS,STATUS, "
            //+"FPORT+'-'+ DPORT AS Transferroute,FPORT,DPORT,REQTIM,CARNO,DIRVER,SEALNO,SEDTIM,CREWHO,SPACOUNT,CRETIME,'*'+ORDERNO+'*' AS  ORDERNO from [dbo].[TRDWN]  A "
            //                                        + "WHERE A.MBLNR='" + mblnr + "'");
            sbSQL.Append("SELECT * FROM [View_PrintTransferData] WHERE MBLNR='*" + mblnr + "*' ");
            try
            {
                ControlHandleDB();
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

        #region 生成派车单号

        public string CreateCarOrder()
        {
            this.ControlMethodName = "CreateCarOrder";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            string order;
            sbSQL.Append("EXEC SP_CreateOrderNO 'S'");
            try
            {
                ControlHandleDB();
                order = ControlSqlAccess.GetFieldValue(sbSQL.ToString());
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
            return order;
        }


        #endregion

        #region 生成绑定单号

        public string CreateBindNo()
        {
            this.ControlMethodName = "CreateBindNo";
            this.ControlMethodParm = "()";
            if (ControlTraceCode == "T") //若要記錄呼叫此WebMethod的log時...
            {
                ControlHandleError("000", "", "");
            }
            StringBuilder sbSQL = new StringBuilder();
            string order;
            sbSQL.Append("EXEC SP_CreateOrderNO 'B'");
            try
            {
                ControlHandleDB();
                order = ControlSqlAccess.GetFieldValue(sbSQL.ToString());
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
            return order;
        }


        #endregion






        #endregion

    }
}
