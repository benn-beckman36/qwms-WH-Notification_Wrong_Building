using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{

    //  <summary>
    // DataWhmtr 針對 WHMTR Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhfin : EntityBase
    {    
        #region Constructor
        #region 不傳入任何參數產生DataWhfin物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhfin物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhfin objWhfin = new DataWhfin();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhfin(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhfin物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhfin物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhmtr objWhmtr = new DataWhmtr(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhfin(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhfin";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHFIN";

            SetFields();
        }

        #endregion
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
        
        #region Grpid
        //  <summary>
        // Grpid
        //  <summary>
        public string Grpid {
            get {
if (htFields["GRPID"] == null)
{
                return null;
}
                return htFields["GRPID"].ToString();
            }
            set {
                htFields["GRPID"] = value;
            }
        }
        #endregion
        
        #region Fmatn
        //  <summary>
        // Fmatn
        //  <summary>
        public string Fmatn {
            get {
if (htFields["FMATN"] == null)
{
                return null;
}
                return htFields["FMATN"].ToString();
            }
            set {
                htFields["FMATN"] = value;
            }
        }
        #endregion
        
        #region Matnr
        //  <summary>
        // Matnr
        //  <summary>
        public string Matnr {
            get {
if (htFields["MATNR"] == null)
{
                return null;
}
                return htFields["MATNR"].ToString();
            }
            set {
                htFields["MATNR"] = value;
            }
        }
        #endregion
        
        #region Charg
        //  <summary>
        // Charg
        //  <summary>
        public string Charg {
            get {
if (htFields["CHARG"] == null)
{
                return null;
}
                return htFields["CHARG"].ToString();
            }
            set {
                htFields["CHARG"] = value;
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
        
        #region Wkord
        //  <summary>
        // Wkord
        //  <summary>
        public string Wkord {
            get {
if (htFields["WKORD"] == null)
{
                return null;
}
                return htFields["WKORD"].ToString();
            }
            set {
                htFields["WKORD"] = value;
            }
        }
        #endregion
        
        #region Stats
        //  <summary>
        // Stats
        //  <summary>
        public string Stats {
            get {
if (htFields["STATS"] == null)
{
                return null;
}
                return htFields["STATS"].ToString();
            }
            set {
                htFields["STATS"] = value;
            }
        }
        #endregion
        
        #region Mtype
        //  <summary>
        // Mtype
        //  <summary>
        public string Mtype {
            get {
if (htFields["MTYPE"] == null)
{
                return null;
}
                return htFields["MTYPE"].ToString();
            }
            set {
                htFields["MTYPE"] = value;
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
        
        #region Modat
        //  <summary>
        // Modat
        //  <summary>
        public string Modat {
            get {
if (htFields["MODAT"] == null)
{
                return null;
}
                return htFields["MODAT"].ToString();
            }
            set {
                htFields["MODAT"] = value;
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
        
        #region DataMember
        UserInfo UserData = new UserInfo();
        //  <summary>
        // 建立htFields,設定所有的欄位並加到 hashtable中
        //  <summary>
        //  <returns>
        // 回傳值型態為void。
        //  <returns>
        protected override void SettingFields() {
            htFields.Add("WERKS", null);
            htFields.Add("GRPID", null);
            htFields.Add("FMATN", null);
            htFields.Add("MATNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("WKORD", null);
            htFields.Add("STATS", null);
            htFields.Add("MTYPE", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFields.Add("MBLNR", null);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("FMATN", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBFunc);
            htFieldType.Add("WKORD", DBDataType.DBString);
            htFieldType.Add("STATS", DBDataType.DBString);
            htFieldType.Add("MTYPE", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("MBLNR", DBDataType.DBString);
        }
        #endregion
    }
}
