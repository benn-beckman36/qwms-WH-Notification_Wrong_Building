using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhstg 針對 WHSTG Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhstg : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhstg物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhstg物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhstg objWhstg = new DataWhstg();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhstg(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhstg物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhstg物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhstg objWhstg = new DataWhstg(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhstg(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhstg";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHSTG";

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

        #region Stset
        //  <summary>
        // Stset
        //  <summary>
        public string Stset
        {
            get
            {
                if (htFields["STSET"] == null)
                {
                    return null;
                }
                return htFields["STSET"].ToString();
            }
            set
            {
                htFields["STSET"] = value;
            }
        }
        #endregion

        #region Sthgh
        //  <summary>
        // Sthgh
        //  <summary>
        public string Sthgh
        {
            get
            {
                if (htFields["STHGH"] == null)
                {
                    return null;
                }
                return htFields["STHGH"].ToString();
            }
            set
            {
                htFields["STHGH"] = value;
            }
        }
        #endregion

        #region Stlen
        //  <summary>
        // Stlen
        //  <summary>
        public string Stlen
        {
            get
            {
                if (htFields["STLEN"] == null)
                {
                    return null;
                }
                return htFields["STLEN"].ToString();
            }
            set
            {
                htFields["STLEN"] = value;
            }
        }
        #endregion

        #region Stwid
        //  <summary>
        // Stwid
        //  <summary>
        public string Stwid
        {
            get
            {
                if (htFields["STWID"] == null)
                {
                    return null;
                }
                return htFields["STWID"].ToString();
            }
            set
            {
                htFields["STWID"] = value;
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
            htFields.Add("MANDT", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("STSET", null);
            htFields.Add("STHGH", null);
            htFields.Add("STLEN", null);
            htFields.Add("STWID", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("COMCD", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("STSET", DBDataType.DBNumber);
            htFieldType.Add("STHGH", DBDataType.DBNumber);
            htFieldType.Add("STLEN", DBDataType.DBNumber);
            htFieldType.Add("STWID", DBDataType.DBNumber);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("COMCD", DBDataType.DBString);
        }
        #endregion
    }
}
