using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{

    //  <summary>
    // DataWhqdp 針對 WHQDP Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhqdp : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhqdp物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhqdp物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhqdp objWhqdp = new DataWhqdp();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhqdp(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhqdp物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhqdp物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhqdp objWhqdp = new DataWhqdp(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhqdp(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhqdp";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHQDP";

            SetFields();
        }

        #endregion
        #endregion

        #region Mandt
        //  <summary>
        // Mandt
        //  <summary>
        public string Mandt
        {
            get
            {
                if (htFields["MANDT"] == null)
                {
                    return null;
                }
                return htFields["MANDT"].ToString();
            }
            set
            {
                htFields["MANDT"] = value;
            }
        }
        #endregion

        #region Comcd
        //  <summary>
        // Comcd
        //  <summary>
        public string Comcd
        {
            get
            {
                if (htFields["COMCD"] == null)
                {
                    return null;
                }
                return htFields["COMCD"].ToString();
            }
            set
            {
                htFields["COMCD"] = value;
            }
        }
        #endregion

        #region Werks
        //  <summary>
        // Werks
        //  <summary>
        public string Werks {
            get {
if (htFields["WERKS"] == null)
{
                return null;
}
                return htFields["WERKS"].ToString();
            }
            set {
                htFields["WERKS"] = value;
            }
        }
        #endregion

        #region Lgort
        //  <summary>
        // Lgort
        //  <summary>
        public string Lgort
        {
            get
            {
                if (htFields["LGORT"] == null)
                {
                    return null;
                }
                return htFields["LGORT"].ToString();
            }
            set
            {
                htFields["LGORT"] = value;
            }
        }
        #endregion

        #region Grpid
        //  <summary>
        // Grpid
        //  <summary>
        public string Grpid
        {
            get
            {
                if (htFields["GRPID"] == null)
                {
                    return null;
                }
                return htFields["GRPID"].ToString();
            }
            set
            {
                htFields["GRPID"] = value;
            }
        }
        #endregion

        #region Didno
        //  <summary>
        // Didno
        //  <summary>
        public string Didno
        {
            get
            {
                if (htFields["DIDNO"] == null)
                {
                    return null;
                }
                return htFields["DIDNO"].ToString();
            }
            set
            {
                htFields["DIDNO"] = value;
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

        #region Mblnr
        //  <summary>
        // Mblnr
        //  <summary>
        public string Mblnr
        {
            get
            {
                if (htFields["MBLNR"] == null)
                {
                    return null;
                }
                return htFields["MBLNR"].ToString();
            }
            set
            {
                htFields["MBLNR"] = value;
            }
        }
        #endregion

        #region Zeile
        //  <summary>
        // Zeile
        //  <summary>
        public string Zeile
        {
            get
            {
                if (htFields["ZEILE"] == null)
                {
                    return null;
                }
                return htFields["ZEILE"].ToString();
            }
            set
            {
                htFields["ZEILE"] = value;
            }
        }
        #endregion
        
        #region Cosct
        //  <summary>
        // Cosct
        //  <summary>
        public string Cosct {
            get {
if (htFields["COSCT"] == null)
{
                return null;
}
                return htFields["COSCT"].ToString();
            }
            set {
                htFields["COSCT"] = value;
            }
        }
        #endregion   
        
        #region Menge
        //  <summary>
        // Menge
        //  <summary>
        public string Menge {
            get {
if (htFields["MENGE"] == null)
{
                return null;
}
                return htFields["MENGE"].ToString();
            }
            set {
                htFields["MENGE"] = value;
            }
        }
        #endregion
              
        #region Crdat
        //  <summary>
        // Crdat
        //  <summary>
        public string Crdat {
            get {
if (htFields["CRDAT"] == null)
{
                return null;
}
                return htFields["CRDAT"].ToString();
            }
            set {
                htFields["CRDAT"] = value;
            }
        }
        #endregion

        #region Stype
        //  <summary>
        // Stype
        //  <summary>
        public string Stype
        {
            get
            {
                if (htFields["STYPE"] == null)
                {
                    return null;
                }
                return htFields["STYPE"].ToString();
            }
            set
            {
                htFields["STYPE"] = value;
            }
        }
        #endregion

        #region Crnam
        //  <summary>
        // Crnam
        //  <summary>
        public string Crnam
        {
            get
            {
                if (htFields["CRNAM"] == null)
                {
                    return null;
                }
                return htFields["CRNAM"].ToString();
            }
            set
            {
                htFields["CRNAM"] = value;
            }
        }
        #endregion

        #region Umlgo
        //  <summary>
        // Umlgo
        //  <summary>
        public string Umlgo
        {
            get
            {
                if (htFields["UMLGO"] == null)
                {
                    return null;
                }
                return htFields["UMLGO"].ToString();
            }
            set
            {
                htFields["UMLGO"] = value;
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
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("GRPID", null);
            htFields.Add("DIDNO", null);
            htFields.Add("MATNR", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("COSCT", null);
            htFields.Add("MENGE", null);
            htFields.Add("CRDAT", null);
            htFields.Add("STYPE", null);
            htFields.Add("CRNAM", null);

            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("DIDNO", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("COSCT", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("STYPE", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("UMLGO", DBDataType.DBString);
        }
        #endregion
    }
}
