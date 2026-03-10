using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;





namespace QWMS.Entity
{


    //  <summary>
    // DataWhsid 針對 WHSID Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhsid : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhsid物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhsid物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhsid objWhsid = new DataWhsid();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhsid(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhsid物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhsid物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhsid objWhsid = new DataWhsid(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhsid(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhsid";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.Whsid";

            SetFields();
        }

        #endregion
        #endregion

        #region Senid
        //  <summary>
        // Senid
        //  <summary>
        public string Senid
        {
            get
            {
                if (htFields["SENID"] == null)
                {
                    return null;
                }
                return htFields["SENID"].ToString();
            }
            set
            {
                htFields["SENID"] = value;
            }
        }
        #endregion

        #region Seqno
        //  <summary>
        // Seqno
        //  <summary>
        public string Seqno
        {
            get
            {
                if (htFields["SEQNO"] == null)
                {
                    return null;
                }
                return htFields["SEQNO"].ToString();
            }
            set
            {
                htFields["SEQNO"] = value;
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

        #region Werks
        //  <summary>
        // Werks
        //  <summary>
        public string Werks
        {
            get
            {
                if (htFields["WERKS"] == null)
                {
                    return null;
                }
                return htFields["WERKS"].ToString();
            }
            set
            {
                htFields["WERKS"] = value;
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

        #region Arbpl
        //  <summary>
        // Arbpl
        //  <summary>
        public string Arbpl
        {
            get
            {
                if (htFields["ARBPL"] == null)
                {
                    return null;
                }
                return htFields["ARBPL"].ToString();
            }
            set
            {
                htFields["ARBPL"] = value;
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
        protected override void SettingFields()
        {
            htFields.Add("SENID", null);
            htFields.Add("SEQNO", null);
            htFields.Add("WERKS", null);
            htFields.Add("MATNR", null);
            htFields.Add("ARBPL", null);
            htFields.Add("MENGE", null);
            htFieldType.Add("SENID", DBDataType.DBString);
            htFieldType.Add("SEQNO", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("ARBPL", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
        }
        #endregion
    }
}
