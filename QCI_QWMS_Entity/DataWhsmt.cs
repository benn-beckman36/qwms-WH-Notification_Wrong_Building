using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{

    //  <summary>
    // DataWhsmt 針對 WHCPA Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhsmt : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhsmt物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhsmt物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhsmt objWhsmt = new DataWhsmt();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhsmt(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhcpa物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhcpa物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhcpa objWhcpa = new DataWhcpa(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhsmt(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhsmt";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHSMT";

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
        
        #region Rlqty
        //  <summary>
        // Rlqty
        //  <summary>
        public string Rlqty {
            get {
if (htFields["RLQTY"] == null)
{
                return null;
}
                return htFields["RLQTY"].ToString();
            }
            set {
                htFields["RLQTY"] = value;
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
        
        #region Wodat
        //  <summary>
        // Wodat
        //  <summary>
        public string Wodat {
            get {
if (htFields["WODAT"] == null)
{
                return null;
}
                return htFields["WODAT"].ToString();
            }
            set {
                htFields["WODAT"] = value;
            }
        }
        #endregion
        
        #region Shift
        //  <summary>
        // Shift
        //  <summary>
        public string Shift {
            get {
if (htFields["SHIFT"] == null)
{
                return null;
}
                return htFields["SHIFT"].ToString();
            }
            set {
                htFields["SHIFT"] = value;
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
        
        #region Trdat
        //  <summary>
        // Trdat
        //  <summary>
        public string Trdat {
            get {
if (htFields["TRDAT"] == null)
{
                return null;
}
                return htFields["TRDAT"].ToString();
            }
            set {
                htFields["TRDAT"] = value;
            }
        }
        #endregion
        
        #region Roval
        //  <summary>
        // Roval
        //  <summary>
        public string Roval {
            get {
if (htFields["ROVAL"] == null)
{
                return null;
}
                return htFields["ROVAL"].ToString();
            }
            set {
                htFields["ROVAL"] = value;
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
            htFields.Add("COSCT", null);
            htFields.Add("MATNR", null);
            htFields.Add("RLQTY", null);
            htFields.Add("MENGE", null);
            htFields.Add("WODAT", null);
            htFields.Add("SHIFT", null);
            htFields.Add("GRPID", null);
            htFields.Add("TRDAT", null);
            htFields.Add("ROVAL", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFields.Add("MBLNR", null);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("COSCT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("RLQTY", DBDataType.DBNumber);
            htFieldType.Add("MENGE", DBDataType.DBFunc);
            htFieldType.Add("WODAT", DBDataType.DBString);
            htFieldType.Add("SHIFT", DBDataType.DBString);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("TRDAT", DBDataType.DBString);
            htFieldType.Add("ROVAL", DBDataType.DBNumber);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("MBLNR", DBDataType.DBString);
        }
        #endregion
    }
}
