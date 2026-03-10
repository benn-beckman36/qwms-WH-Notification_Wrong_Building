using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{
    
    
    //  <summary>
    // DataErrlog 針對 ERRLOG Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataErrlog : EntityBase {
        
        #region Constructor
        #region 不傳入任何參數產生DataErrlog物件 by Smose Liao
        /// <summary>
        /// 不傳入任何參數產生DataErrlog物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhlog objErrlog = new DataErrlog();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataErrlog(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataErrlog物件 by Smose Liao
        /// <summary>
        /// 利用傳入參數產生DataErrlog物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataErrlog objErrlog = new DataErrlog(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataErrlog(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataErrlog";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Smose Liao";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.ERRLOG";

            SetFields();
        }

        #endregion
        #endregion
        
        #region Logids
        //  <summary>
        // Logids
        //  <summary>
        public string Logids {
            get {
if (htFields["LOGIDS"] == null)
{
                return null;
}
                return htFields["LOGIDS"].ToString();
            }
            set {
                htFields["LOGIDS"] = value;
            }
        }
        #endregion
        
        #region Mandt
        //  <summary>
        // Mandt
        //  <summary>
        public string Mandt {
            get {
if (htFields["MANDT"] == null)
{
                return null;
}
                return htFields["MANDT"].ToString();
            }
            set {
                htFields["MANDT"] = value;
            }
        }
        #endregion
        
        #region Refid
        //  <summary>
        // Refid
        //  <summary>
        public string Refid {
            get {
if (htFields["REFID"] == null)
{
                return null;
}
                return htFields["REFID"].ToString();
            }
            set {
                htFields["REFID"] = value;
            }
        }
        #endregion
        
        #region Mblnr
        //  <summary>
        // Mblnr
        //  <summary>
        public string Mblnr {
            get {
            if (htFields["MBLNR"] == null)
            {
                            return null;
            }
                return htFields["MBLNR"].ToString();
            }
            set {
                htFields["MBLNR"] = value;
            }
        }
        #endregion

        #region Matnr
        //  <summary>
        // Matnr
        //  <summary>
        public string Matnr
        {
            get
            {
                if (htFields["MATNR"] == null)
                {
                    return null;
                }
                return htFields["MATNR"].ToString();
            }
            set
            {
                htFields["MATNR"] = value;
            }
        }
        #endregion
        
        #region Logsql
        //  <summary>
        // Logsql
        //  <summary>
        public string Logsql {
            get {
if (htFields["LOGSQL"] == null)
{
                return null;
}
                return htFields["LOGSQL"].ToString();
            }
            set {
                htFields["LOGSQL"] = value;
            }
        }
        #endregion
        
        #region Logtim
        //  <summary>
        // Logtim
        //  <summary>
        public string Logtim {
            get {
if (htFields["LOGTIM"] == null)
{
                return null;
}
                return htFields["LOGTIM"].ToString();
            }
            set {
                htFields["LOGTIM"] = value;
            }
        }
        #endregion
        
        #region Malflg
        //  <summary>
        // Malflg
        //  <summary>
        public string Malflg {
            get {
if (htFields["MALFLG"] == null)
{
                return null;
}
                return htFields["MALFLG"].ToString();
            }
            set {
                htFields["MALFLG"] = value;
            }
        }
        #endregion
        
        #region Maltim
        //  <summary>
        // Maltim
        //  <summary>
        public string Maltim {
            get {
if (htFields["MALTIM"] == null)
{
                return null;
}
                return htFields["MALTIM"].ToString();
            }
            set {
                htFields["MALTIM"] = value;
            }
        }
        #endregion
        
        #region Prcflg
        //  <summary>
        // Prcflg
        //  <summary>
        public string Prcflg {
            get {
if (htFields["PRCFLG"] == null)
{
                return null;
}
                return htFields["PRCFLG"].ToString();
            }
            set {
                htFields["PRCFLG"] = value;
            }
        }
        #endregion
        
        #region Prctim
        //  <summary>
        // Prctim
        //  <summary>
        public string Prctim {
            get {
if (htFields["PRCTIM"] == null)
{
                return null;
}
                return htFields["PRCTIM"].ToString();
            }
            set {
                htFields["PRCTIM"] = value;
            }
        }
        #endregion
        
        #region Comcd
        //  <summary>
        // Comcd
        //  <summary>
        public string Comcd {
            get {
if (htFields["COMCD"] == null)
{
                return null;
}
                return htFields["COMCD"].ToString();
            }
            set {
                htFields["COMCD"] = value;
            }
        }
        #endregion

        #region Menge
        //  <summary>
        // Menge
        //  <summary>
        public string Menge
        {
            get
            {
                if (htFields["MENGE"] == null)
                {
                    return null;
                }
                return htFields["MENGE"].ToString();
            }
            set
            {
                htFields["MENGE"] = value;
            }
        }
        #endregion

        #region Usrnm
        //  <summary>
        // Usrnm
        //  <summary>
        public string Usrnm
        {
            get
            {
                if (htFields["USRNM"] == null)
                {
                    return null;
                }
                return htFields["USRNM"].ToString();
            }
            set
            {
                htFields["USRNM"] = value;
            }
        }
        #endregion
        
        #region DataMember
        UserInfo UserData = new UserInfo();
        //  <summary>
        // 建立htFields,設定所有的欄位並加到 hashtable中
        //  <summary>
        //  <returns>
        // 回傳值型態為void。
        //  <returns>
        protected override void SettingFields() {
            htFields.Add("LOGIDS", null);
            htFields.Add("MANDT", null);
            htFields.Add("REFID", null);
            htFields.Add("MBLNR", null);
            htFields.Add("MATNR", null);
            htFields.Add("LOGSQL", null);
            htFields.Add("LOGTIM", null);
            htFields.Add("MALFLG", null);
            htFields.Add("MALTIM", null);
            htFields.Add("PRCFLG", null);
            htFields.Add("PRCTIM", null);
            htFields.Add("COMCD", null);
            htFields.Add("MENGE", null);
            htFields.Add("USRNM", null);
            htFieldType.Add("LOGIDS", DBDataType.DBNumber);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("REFID", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("LOGSQL", DBDataType.DBString);
            htFieldType.Add("LOGTIM", DBDataType.DBCreateDate);
            htFieldType.Add("MALFLG", DBDataType.DBString);
            htFieldType.Add("MALTIM", DBDataType.DBCreateDate);
            htFieldType.Add("PRCFLG", DBDataType.DBString);
            htFieldType.Add("PRCTIM", DBDataType.DBCreateDate);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("USRNM", DBDataType.DBString);
        }
        #endregion
    }
}
