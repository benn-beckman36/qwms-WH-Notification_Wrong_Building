using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;






namespace QWMS.Entity
{


    //  <summary>
    // DataWhpat 針對 WHPAT Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhpat : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhpat物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhpat物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhpat objWhpat = new DataWhpat();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhpat(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhpat物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhpat物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhpat objWhpat = new DataWhpat(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhpat(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhpat";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHPAT";

            SetFields();
        }

        #endregion
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

        #region Maktx
        //  <summary>
        // Maktx
        //  <summary>
        public string Maktx
        {
            get
            {
                if (htFields["MAKTX"] == null)
                {
                    return null;
                }
                return htFields["MAKTX"].ToString();
            }
            set
            {
                htFields["MAKTX"] = value;
            }
        }
        #endregion

        #region Crdat
        //  <summary>
        // Crdat
        //  <summary>
        public string Crdat
        {
            get
            {
                if (htFields["CRDAT"] == null)
                {
                    return null;
                }
                return htFields["CRDAT"].ToString();
            }
            set
            {
                htFields["CRDAT"] = value;
            }
        }
        #endregion

        #region Modat
        //  <summary>
        // Modat
        //  <summary>
        public string Modat
        {
            get
            {
                if (htFields["MODAT"] == null)
                {
                    return null;
                }
                return htFields["MODAT"].ToString();
            }
            set
            {
                htFields["MODAT"] = value;
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
            htFields.Add("MATNR", null);
            htFields.Add("MAKTX", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFieldType.Add("MATNR", DBDataType.DbunString);
            htFieldType.Add("MAKTX", DBDataType.DbunString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
        }
        #endregion
    }
}
