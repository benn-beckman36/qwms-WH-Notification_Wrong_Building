using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{

    //  <summary>
    // DataWhcpa 針對 WHCPA Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhcpa : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhcpa物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhcpa物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhsmt objWhcpa = new DataWhcpa();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhcpa(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhmtr物件 by Rock Tzeng
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
        public DataWhcpa(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhcpa";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHCPA";

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
            htFields.Add("MENGE", null);
            htFields.Add("GRPID", null);
            htFields.Add("CRDAT", null);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("COSCT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBFunc);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
        }
        #endregion
    }
}
