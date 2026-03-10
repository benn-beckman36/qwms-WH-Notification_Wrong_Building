using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{
    //  <summary>
    // DataWhecd 針對 WHECD Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhecd : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhecd物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhecd物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhecd objWhecd = new DataWhecd();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhecd(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhecd物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhecd物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhecd objWhecd = new DataWhecd(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhecd(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhecd";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHECD";

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

        #region Vbeln
        //  <summary>
        // Vbeln
        //  <summary>
        public string Vbeln
        {
            get
            {
                if (htFields["VBELN"] == null)
                {
                    return null;
                }
                return htFields["VBELN"].ToString();
            }
            set
            {
                htFields["VBELN"] = value;
            }
        }
        #endregion

        #region Pakid
        //  <summary>
        // Pakid
        //  <summary>
        public string Pakid
        {
            get
            {
                if (htFields["PAKID"] == null)
                {
                    return null;
                }
                return htFields["PAKID"].ToString();
            }
            set
            {
                htFields["PAKID"] = value;
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

        #region Batch
        //  <summary>
        // Batch
        //  <summary>
        public string Batch
        {
            get
            {
                if (htFields["BATCH"] == null)
                {
                    return null;
                }
                return htFields["BATCH"].ToString();
            }
            set
            {
                htFields["BATCH"] = value;
            }
        }
        #endregion

        #region Ebeln
        //  <summary>
        // Ebeln
        //  <summary>
        public string Ebeln
        {
            get
            {
                if (htFields["EBELN"] == null)
                {
                    return null;
                }
                return htFields["EBELN"].ToString();
            }
            set
            {
                htFields["EBELN"] = value;
            }
        }
        #endregion

        #region Alqty
        //  <summary>
        // Alqty
        //  <summary>
        public string Alqty
        {
            get
            {
                if (htFields["ALQTY"] == null)
                {
                    return null;
                }
                return htFields["ALQTY"].ToString();
            }
            set
            {
                htFields["ALQTY"] = value;
            }
        }
        #endregion

        #region Exqty
        //  <summary>
        // Exqty
        //  <summary>
        public string Exqty
        {
            get
            {
                if (htFields["EXQTY"] == null)
                {
                    return null;
                }
                return htFields["EXQTY"].ToString();
            }
            set
            {
                htFields["EXQTY"] = value;
            }
        }
        #endregion

        #region Extyp
        //  <summary>
        // Extyp
        //  <summary>
        public string Extyp
        {
            get
            {
                if (htFields["EXTYP"] == null)
                {
                    return null;
                }
                return htFields["EXTYP"].ToString();
            }
            set
            {
                htFields["EXTYP"] = value;
            }
        }
        #endregion

        #region Exrmk
        //  <summary>
        // Exrmk
        //  <summary>
        public string Exrmk
        {
            get
            {
                if (htFields["EXRMK"] == null)
                {
                    return null;
                }
                return htFields["EXRMK"].ToString();
            }
            set
            {
                htFields["EXRMK"] = value;
            }
        }
        #endregion

        #region Qctyp
        //  <summary>
        // Qctyp
        //  <summary>
        public string Qctyp
        {
            get
            {
                if (htFields["QCTYP"] == null)
                {
                    return null;
                }
                return htFields["QCTYP"].ToString();
            }
            set
            {
                htFields["QCTYP"] = value;
            }
        }
        #endregion

        #region Qcusr
        //  <summary>
        // Qcusr
        //  <summary>
        public string Qcusr
        {
            get
            {
                if (htFields["QCUSR"] == null)
                {
                    return null;
                }
                return htFields["QCUSR"].ToString();
            }
            set
            {
                htFields["QCUSR"] = value;
            }
        }
        #endregion

        #region Whusr
        //  <summary>
        // Whusr
        //  <summary>
        public string Whusr
        {
            get
            {
                if (htFields["WHUSR"] == null)
                {
                    return null;
                }
                return htFields["WHUSR"].ToString();
            }
            set
            {
                htFields["WHUSR"] = value;
            }
        }
        #endregion

        #region Remak
        //  <summary>
        // Remak
        //  <summary>
        public string Remak
        {
            get
            {
                if (htFields["REMAK"] == null)
                {
                    return null;
                }
                return htFields["REMAK"].ToString();
            }
            set
            {
                htFields["REMAK"] = value;
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
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("VBELN", null);
            htFields.Add("PAKID", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MATNR", null);
            htFields.Add("MENGE", null);
            htFields.Add("BATCH", null);
            htFields.Add("EBELN", null);
            htFields.Add("ALQTY", null);
            htFields.Add("EXQTY", null);
            htFields.Add("EXTYP", null);
            htFields.Add("EXRMK", null);
            htFields.Add("CRDAT", null);
            htFields.Add("QCTYP", null);
            htFields.Add("QCUSR", null);
            htFields.Add("WHUSR", null);
            htFields.Add("REMAK", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("VBELN", DBDataType.DBString);
            htFieldType.Add("PAKID", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("BATCH", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("ALQTY", DBDataType.DBString);
            htFieldType.Add("EXQTY", DBDataType.DBString);
            htFieldType.Add("EXTYP", DBDataType.DBString);
            htFieldType.Add("EXRMK", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("QCTYP", DBDataType.DBString);
            htFieldType.Add("QCUSR", DBDataType.DBString);
            htFieldType.Add("WHUSR", DBDataType.DBString);
            htFieldType.Add("REMAK", DBDataType.DBString);

        }
        #endregion
    }
}
