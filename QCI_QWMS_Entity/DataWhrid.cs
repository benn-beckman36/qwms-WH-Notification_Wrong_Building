using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;





namespace QWMS.Entity
{


    //  <summary>
    // DataWhrid 針對 WHRID Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhrid : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhrid物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhrid物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhrid objWhrid = new DataWhrid();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhrid(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhrid物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhrid物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhrid objWhrid = new DataWhrid(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhrid(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhrid";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHRID";

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

        #region Refid
        //  <summary>
        // Refid
        //  <summary>
        public string Refid
        {
            get
            {
                if (htFields["REFID"] == null)
                {
                    return null;
                }
                return htFields["REFID"].ToString();
            }
            set
            {
                htFields["REFID"] = value;
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

        #region Insmk
        //  <summary>
        // Insmk
        //  <summary>
        public string Insmk
        {
            get
            {
                if (htFields["INSMK"] == null)
                {
                    return null;
                }
                return htFields["INSMK"].ToString();
            }
            set
            {
                htFields["INSMK"] = value;
            }
        }
        #endregion

        #region Charg
        //  <summary>
        // Charg
        //  <summary>
        public string Charg
        {
            get
            {
                if (htFields["CHARG"] == null)
                {
                    return null;
                }
                return htFields["CHARG"].ToString();
            }
            set
            {
                htFields["CHARG"] = value;
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

        #region Otqty
        //  <summary>
        // Otqty
        //  <summary>
        public string Otqty
        {
            get
            {
                if (htFields["OTQTY"] == null)
                {
                    return null;
                }
                return htFields["OTQTY"].ToString();
            }
            set
            {
                htFields["OTQTY"] = value;
            }
        }
        #endregion

        #region Otwrk
        //  <summary>
        // Otwrk
        //  <summary>
        public string Otwrk
        {
            get
            {
                if (htFields["OTWRK"] == null)
                {
                    return null;
                }
                return htFields["OTWRK"].ToString();
            }
            set
            {
                htFields["OTWRK"] = value;
            }
        }
        #endregion

        #region Otlgt
        //  <summary>
        // Otlgt
        //  <summary>
        public string Otlgt
        {
            get
            {
                if (htFields["OTLGT"] == null)
                {
                    return null;
                }
                return htFields["OTLGT"].ToString();
            }
            set
            {
                htFields["OTLGT"] = value;
            }
        }
        #endregion

        #region Lifnr
        //  <summary>
        // Lifnr
        //  <summary>
        public string Lifnr
        {
            get
            {
                if (htFields["LIFNR"] == null)
                {
                    return null;
                }
                return htFields["LIFNR"].ToString();
            }
            set
            {
                htFields["LIFNR"] = value;
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

        #region Ombln
        //  <summary>
        // Ombln
        //  <summary>
        public string Ombln
        {
            get
            {
                if (htFields["OMBLN"] == null)
                {
                    return null;
                }
                return htFields["OMBLN"].ToString();
            }
            set
            {
                htFields["OMBLN"] = value;
            }
        }
        #endregion

        #region Kostl
        //  <summary>
        // Kostl
        //  <summary>
        public string Kostl
        {
            get
            {
                if (htFields["KOSTL"] == null)
                {
                    return null;
                }
                return htFields["KOSTL"].ToString();
            }
            set
            {
                htFields["KOSTL"] = value;
            }
        }
        #endregion

        #region Updat
        //  <summary>
        // Updat
        //  <summary>
        public string Updat
        {
            get
            {
                if (htFields["UPDAT"] == null)
                {
                    return null;
                }
                return htFields["UPDAT"].ToString();
            }
            set
            {
                htFields["UPDAT"] = value;
            }
        }
        #endregion

        #region Serno
        //  <summary>
        // Serno
        //  <summary>
        public string Serno
        {
            get
            {
                if (htFields["SERNO"] == null)
                {
                    return null;
                }
                return htFields["SERNO"].ToString();
            }
            set
            {
                htFields["SERNO"] = value;
            }
        }
        #endregion

        #region Locod
        //  <summary>
        // Locod
        //  <summary>
        public string Locod
        {
            get
            {
                if (htFields["LOCOD"] == null)
                {
                    return null;
                }
                return htFields["LOCOD"].ToString();
            }
            set
            {
                htFields["LOCOD"] = value;
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
            htFields.Add("REFID", null);
            htFields.Add("DIDNO", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("MATNR", null);
            htFields.Add("INSMK", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("OTQTY", null);
            htFields.Add("OTWRK", null);
            htFields.Add("OTLGT", null);
            htFields.Add("LIFNR", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFields.Add("OMBLN", null);
            htFields.Add("KOSTL", null);
            htFields.Add("UPDAT", null);
            htFields.Add("SERNO", null);
            htFields.Add("LOCOD", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("REFID", DBDataType.DBString);
            htFieldType.Add("DIDNO", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("OTQTY", DBDataType.DBNumber);
            htFieldType.Add("OTWRK", DBDataType.DBString);
            htFieldType.Add("OTLGT", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("OMBLN", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("UPDAT", DBDataType.DBCreateDate);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
        }
        #endregion
    }
}
