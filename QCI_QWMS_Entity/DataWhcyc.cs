using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhcyc 針對 WHCYC Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhcyc : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhcyc物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhcyc物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhcyc objWhcyc = new DataWhcyc();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhcyc(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhcyc物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhcyc物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhcyc objWhcyc = new DataWhcyc(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhcyc(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhcyc";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHCYC";

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

        #region Cycno
        //  <summary>
        // Cycno
        //  <summary>
        public string Cycno
        {
            get
            {
                if (htFields["CYCNO"] == null)
                {
                    return null;
                }
                return htFields["CYCNO"].ToString();
            }
            set
            {
                htFields["CYCNO"] = value;
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

        #region Locat
        //  <summary>
        // Locat
        //  <summary>
        public string Locat
        {
            get
            {
                if (htFields["LOCAT"] == null)
                {
                    return null;
                }
                return htFields["LOCAT"].ToString();
            }
            set
            {
                htFields["LOCAT"] = value;
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

        #region Ckdat
        //  <summary>
        // Ckdat
        //  <summary>
        public string Ckdat
        {
            get
            {
                if (htFields["CKDAT"] == null)
                {
                    return null;
                }
                return htFields["CKDAT"].ToString();
            }
            set
            {
                htFields["CKDAT"] = value;
            }
        }
        #endregion

        #region Ckqty
        //  <summary>
        // Ckqty
        //  <summary>
        public string Ckqty
        {
            get
            {
                if (htFields["CKQTY"] == null)
                {
                    return null;
                }
                return htFields["CKQTY"].ToString();
            }
            set
            {
                htFields["CKQTY"] = value;
            }
        }
        #endregion

        #region Stats
        //  <summary>
        // Stats
        //  <summary>
        public string Stats
        {
            get
            {
                if (htFields["STATS"] == null)
                {
                    return null;
                }
                return htFields["STATS"].ToString();
            }
            set
            {
                htFields["STATS"] = value;
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

        #region Monam
        //  <summary>
        // Monam
        //  <summary>
        public string Monam
        {
            get
            {
                if (htFields["MONAM"] == null)
                {
                    return null;
                }
                return htFields["MONAM"].ToString();
            }
            set
            {
                htFields["MONAM"] = value;
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
            htFields.Add("CYCNO", null);
            htFields.Add("KOSTL", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MATNR", null);
            htFields.Add("INSMK", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("CKDAT", null);
            htFields.Add("CKQTY", null);
            htFields.Add("STATS", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("COMCD", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("CYCNO", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("CKDAT", DBDataType.DBString);
            htFieldType.Add("CKQTY", DBDataType.DBNumber);
            htFieldType.Add("STATS", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("COMCD", DBDataType.DBString);
        }
        #endregion
    }
}
