using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;





namespace QWMS.Entity
{


    //  <summary>
    // DataWhcrs 針對 WHCRS Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhcrs : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhcrs物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhcrs物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhcrs objWhcrs = new DataWhcrs();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhcrs(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhcrs物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhcrs物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhcrs objWhcrs = new DataWhcrs(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhcrs(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhcrs";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHCRS";

            SetFields();
        }

        #endregion
        #endregion

        #region Crsno
        //  <summary>
        // Crsno
        //  <summary>
        public string Crsno
        {
            get
            {
                if (htFields["CRSNO"] == null)
                {
                    return null;
                }
                return htFields["CRSNO"].ToString();
            }
            set
            {
                htFields["CRSNO"] = value;
            }
        }
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

        #region Trntp
        //  <summary>
        // Trntp
        //  <summary>
        public string Trntp
        {
            get
            {
                if (htFields["TRNTP"] == null)
                {
                    return null;
                }
                return htFields["TRNTP"].ToString();
            }
            set
            {
                htFields["TRNTP"] = value;
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

        #region Commd
        //  <summary>
        // Commd
        //  <summary>
        public string Commd
        {
            get
            {
                if (htFields["COMMD"] == null)
                {
                    return null;
                }
                return htFields["COMMD"].ToString();
            }
            set
            {
                htFields["COMMD"] = value;
            }
        }
        #endregion

        #region Flage
        //  <summary>
        // Flage
        //  <summary>
        public string Flage
        {
            get
            {
                if (htFields["FLAGE"] == null)
                {
                    return null;
                }
                return htFields["FLAGE"].ToString();
            }
            set
            {
                htFields["FLAGE"] = value;
            }
        }
        #endregion

        #region Delet
        //  <summary>
        // Delet
        //  <summary>
        public string Delet
        {
            get
            {
                if (htFields["DELET"] == null)
                {
                    return null;
                }
                return htFields["DELET"].ToString();
            }
            set
            {
                htFields["DELET"] = value;
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

        #region Sddat
        //  <summary>
        // Sddat
        //  <summary>
        public string Sddat
        {
            get
            {
                if (htFields["SDDAT"] == null)
                {
                    return null;
                }
                return htFields["SDDAT"].ToString();
            }
            set
            {
                htFields["SDDAT"] = value;
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
            htFields.Add("CRSNO", null);
            htFields.Add("MANDT", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null);
            htFields.Add("TRNTP", null);
            htFields.Add("STSET", null);
            htFields.Add("STHGH", null);
            htFields.Add("STLEN", null);
            htFields.Add("STWID", null);
            htFields.Add("MATNR", null);
            htFields.Add("MENGE", null);
            htFields.Add("KOSTL", null);
            htFields.Add("COMMD", null);
            htFields.Add("FLAGE", null);
            htFields.Add("DELET", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("SDDAT", null);
            htFieldType.Add("CRSNO", DBDataType.DBNumber);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("TRNTP", DBDataType.DBString);
            htFieldType.Add("STSET", DBDataType.DBNumber);
            htFieldType.Add("STHGH", DBDataType.DBNumber);
            htFieldType.Add("STLEN", DBDataType.DBNumber);
            htFieldType.Add("STWID", DBDataType.DBNumber);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("COMMD", DBDataType.DBString);
            htFieldType.Add("FLAGE", DBDataType.DBString);
            htFieldType.Add("DELET", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("SDDAT", DBDataType.DBCreateDate);
        }
        #endregion
    }
}
