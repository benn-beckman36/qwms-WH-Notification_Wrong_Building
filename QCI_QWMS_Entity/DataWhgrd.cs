using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhgrd 針對 WHGRD Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhgrd : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhgrd物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhgrd物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhgrd objWhgrd = new DataWhgrd();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhgrd(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhgrd物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhgrd物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhgrd objWhgrd = new DataWhgrd(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhgrd(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhgrd";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHGRD";

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

        #region Indat
        //  <summary>
        // Indat
        //  <summary>
        public string Indat
        {
            get
            {
                if (htFields["INDAT"] == null)
                {
                    return null;
                }
                return htFields["INDAT"].ToString();
            }
            set
            {
                htFields["INDAT"] = value;
            }
        }
        #endregion

        #region Lodat
        //  <summary>
        // Lodat
        //  <summary>
        public string Lodat
        {
            get
            {
                if (htFields["LODAT"] == null)
                {
                    return null;
                }
                return htFields["LODAT"].ToString();
            }
            set
            {
                htFields["LODAT"] = value;
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

        #region Qcqty
        //  <summary>
        // Qcqty
        //  <summary>
        public string Qcqty
        {
            get
            {
                if (htFields["QCQTY"] == null)
                {
                    return null;
                }
                return htFields["QCQTY"].ToString();
            }
            set
            {
                htFields["QCQTY"] = value;
            }
        }
        #endregion

        #region Refno
        //  <summary>
        // Refno
        //  <summary>
        public string Refno
        {
            get
            {
                if (htFields["REFNO"] == null)
                {
                    return null;
                }
                return htFields["REFNO"].ToString();
            }
            set
            {
                htFields["REFNO"] = value;
            }
        }
        #endregion

        #region Mrgid
        //  <summary>
        // Mrgid
        //  <summary>
        public string Mrgid
        {
            get
            {
                if (htFields["MRGID"] == null)
                {
                    return null;
                }
                return htFields["MRGID"].ToString();
            }
            set
            {
                htFields["MRGID"] = value;
            }
        }
        #endregion

        #region Isptm
        //  <summary>
        // Isptm
        //  <summary>
        public string Isptm
        {
            get
            {
                if (htFields["ISPTM"] == null)
                {
                    return null;
                }
                return htFields["ISPTM"].ToString();
            }
            set
            {
                htFields["ISPTM"] = value;
            }
        }
        #endregion

        #region Kdmat
        //  <summary>
        // Kdmat
        //  <summary>
        public string Kdmat
        {
            get
            {
                if (htFields["KDMAT"] == null)
                {
                    return null;
                }
                return htFields["KDMAT"].ToString();
            }
            set
            {
                htFields["KDMAT"] = value;
            }
        }
        #endregion

        #region Rmak1
        //  <summary>
        // Rmak1
        //  <summary>
        public string Rmak1
        {
            get
            {
                if (htFields["RMAK1"] == null)
                {
                    return null;
                }
                return htFields["RMAK1"].ToString();
            }
            set
            {
                htFields["RMAK1"] = value;
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

        #region Inspt
        //  <summary>
        // Inspt
        //  <summary>
        public string Inspt
        {
            get
            {
                if (htFields["INSPT"] == null)
                {
                    return null;
                }
                return htFields["INSPT"].ToString();
            }
            set
            {
                htFields["INSPT"] = value;
            }
        }
        #endregion

        #region Dacod
        //  <summary>
        // Date Code
        //  <summary>
        public string Dacod
        {
            get
            {
                if (htFields["DACOD"] == null)
                {
                    return null;
                }
                return htFields["DACOD"].ToString();
            }
            set
            {
                htFields["DACOD"] = value;
            }
        }
        #endregion

        #region Vedat
        //  <summary>
        // Vendor Manufactured Date
        //  <summary>
        public string Vedat
        {
            get
            {
                if (htFields["VEDAT"] == null)
                {
                    return null;
                }
                return htFields["VEDAT"].ToString();
            }
            set
            {
                htFields["VEDAT"] = value;
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
            htFields.Add("MATNR", null);
            htFields.Add("INSMK", null);
            htFields.Add("MBLNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("LIFNR", null);
            htFields.Add("EBELN", null);
            htFields.Add("INDAT", null);
            htFields.Add("LODAT", null);
            htFields.Add("MENGE", null);
            htFields.Add("QCQTY", null);
            htFields.Add("REFNO", null);
            htFields.Add("MRGID", null);
            htFields.Add("ISPTM", null);
            htFields.Add("KDMAT", null);
            htFields.Add("RMAK1", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("SERNO", null);
            htFields.Add("LOCOD", null);
            htFields.Add("INSPT", null);
            htFields.Add("DACOD", null);
            htFields.Add("VEDAT", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("INDAT", DBDataType.DBString);
            htFieldType.Add("LODAT", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("QCQTY", DBDataType.DBNumber);
            htFieldType.Add("REFNO", DBDataType.DBString);
            htFieldType.Add("MRGID", DBDataType.DBString);
            htFieldType.Add("ISPTM", DBDataType.DBString);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("RMAK1", DBDataType.DbunString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("INSPT", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
        }
        #endregion
    }
}
