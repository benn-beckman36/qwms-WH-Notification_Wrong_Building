using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{
    public class DataWhitmAGV : EntityBase
    {

        #region Constructor
        #region ぃ肚ヴ把计玻ネDataWhitmン by Rock Tzeng
        /// <summary>
        /// ぃ肚ヴ把计玻ネDataWhitmン
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhitm objWhitm = new DataWhitm();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhitmAGV(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region ノ肚把计玻ネDataWhitmン by Rock Tzeng
        /// <summary>
        /// ノ肚把计玻ネDataWhitmン
        /// </summary>
        /// <param name="varDBType">DB Type</param>
        /// <param name="varDBCode">DB Code</param>
        /// <param name="varErrorType">Error Type</param>
        /// <param name="varErrorCode">Error Code</param>
        /// <example>
        /// <code>
        ///  DataWhitm objWhitm = new DataWhitm(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhitmAGV(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhitmAGV";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHITM_AGV";

            SetFields();
        }

        #endregion
        #endregion

        #region UniqueID
        //  <summary>
        // UniqueID
        //  <summary>
        public string UniqueID
        {
            get
            {
                if (htFields["UniqueID"] == null)
                {
                    return null;
                }
                return htFields["UniqueID"].ToString();
            }
            set
            {
                htFields["UniqueID"] = value;
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

        #region Rmano
        //  <summary>
        // Rmano
        //  <summary>
        public string Rmano
        {
            get
            {
                if (htFields["RMANO"] == null)
                {
                    return null;
                }
                return htFields["RMANO"].ToString();
            }
            set
            {
                htFields["RMANO"] = value;
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

        #region Reqty
        //  <summary>
        // Reqty
        //  <summary>
        public string Reqty
        {
            get
            {
                if (htFields["REQTY"] == null)
                {
                    return null;
                }
                return htFields["REQTY"].ToString();
            }
            set
            {
                htFields["REQTY"] = value;
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

        #region Bkqty
        //  <summary>
        // Bkqty
        //  <summary>
        public string Bkqty
        {
            get
            {
                if (htFields["BKQTY"] == null)
                {
                    return null;
                }
                return htFields["BKQTY"].ToString();
            }
            set
            {
                htFields["BKQTY"] = value;
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

        #region Boxid
        //  <summary>
        // Box id
        //  <summary>
        public string Boxid
        {
            get
            {
                if (htFields["BOXID"] == null)
                {
                    return null;
                }
                return htFields["BOXID"].ToString();
            }
            set
            {
                htFields["BOXID"] = value;
            }
        }
        #endregion

        #region Nloca
        //  <summary>
        // Nloca
        //  <summary>
        public string Nloca
        {
            get
            {
                if (htFields["NLOCA"] == null)
                {
                    return null;
                }
                return htFields["NLOCA"].ToString();
            }
            set
            {
                htFields["NLOCA"] = value;
            }
        }
        #endregion

        #region Sidno
        //  <summary>
        // Sidno
        //  <summary>
        public string Sidno
        {
            get
            {
                if (htFields["SIDNO"] == null)
                {
                    return null;
                }
                return htFields["SIDNO"].ToString();
            }
            set
            {
                htFields["SIDNO"] = value;
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

        #region Pkdat
        //  <summary>
        // Pkdat
        //  <summary>
        public string Pkdat
        {
            get
            {
                if (htFields["PKDAT"] == null)
                {
                    return null;
                }
                return htFields["PKDAT"].ToString();
            }
            set
            {
                htFields["PKDAT"] = value;
            }
        }
        #endregion

        #region ExpiryDate
        //  <summary>
        // ExpiryDate
        //  <summary>
        public string ExpiryDate
        {
            get
            {
                if (htFields["ExpiryDate"] == null)
                {
                    return null;
                }
                return htFields["ExpiryDate"].ToString();
            }
            set
            {
                htFields["ExpiryDate"] = value;
            }
        }
        #endregion

        #region TASKID
        //  <summary>
        // TASKID
        //  <summary>
        public string TASKID
        {
            get
            {
                if (htFields["TASKID"] == null)
                {
                    return null;
                }
                return htFields["TASKID"].ToString();
            }
            set
            {
                htFields["TASKID"] = value;
            }
        }
        #endregion

        #region MAXEXP
        //  <summary>
        // MAXEXP
        //  <summary>
        public string MAXEXP 
        {
            get {
                if (htFields["MAXEXP"] == null) {
                    return null;
                }
                return htFields["MAXEXP"].ToString();
            }
            set {
                htFields["MAXEXP"] = value;
            }
        }
        #endregion

        #region MRBNO
        //  <summary>
        // MRBNO
        //  <summary>
        public string MRBNO
        {
            get
            {
                if (htFields["MRBNO"] == null)
                {
                    return null;
                }
                return htFields["MRBNO"].ToString();
            }
            set
            {
                htFields["MRBNO"] = value;
            }
        }
        #endregion

        #region LOCKED
        //  <summary>
        // LOCKED
        //  <summary>
        public string LOCKED
        {
            get
            {
                if (htFields["LOCKED"] == null)
                {
                    return null;
                }
                return htFields["LOCKED"].ToString();
            }
            set
            {
                htFields["LOCKED"] = value;
            }
        }
        #endregion

        #region LKDAT
        //  <summary>
        // LKDAT
        //  <summary>
        public string LKDAT
        {
            get
            {
                if (htFields["LKDAT"] == null)
                {
                    return null;
                }
                return htFields["LKDAT"].ToString();
            }
            set
            {
                htFields["LKDAT"] = value;
            }
        }
        #endregion

        #region MARNO
        //  <summary>
        // MARNO
        //  <summary>
        public string MARNO
        {
            get
            {
                if (htFields["MARNO"] == null)
                {
                    return null;
                }
                return htFields["MARNO"].ToString();
            }
            set
            {
                htFields["MARNO"] = value;
            }
        }
        #endregion

        #region ITEMSTATES
        //  <summary>
        // ITEMSTATES
        //  <summary>
        public string ITEMSTATES
        {
            get
            {
                if (htFields["ITEMSTATES"] == null)
                {
                    return null;
                }
                return htFields["ITEMSTATES"].ToString();
            }
            set
            {
                htFields["ITEMSTATES"] = value;
            }
        }
        #endregion

        #region STOCSTATES
        //  <summary>
        // STOCSTATES
        //  <summary>
        public string STOCSTATES
        {
            get
            {
                if (htFields["STOCSTATES"] == null)
                {
                    return null;
                }
                return htFields["STOCSTATES"].ToString();
            }
            set
            {
                htFields["STOCSTATES"] = value;
            }
        }
        #endregion

        #region CONFIG
        //  <summary>
        // CONFIG
        //  <summary>
        public string CONFIG
        {
            get
            {
                if (htFields["CONFIG"] == null)
                {
                    return null;
                }
                return htFields["CONFIG"].ToString();
            }
            set
            {
                htFields["CONFIG"] = value;
            }
        }
        #endregion

        #region MCDAT
        //  <summary>
        // MCDAT
        //  <summary>
        public string MCDAT
        {
            get
            {
                if (htFields["MCDAT"] == null)
                {
                    return null;
                }
                return htFields["MCDAT"].ToString();
            }
            set
            {
                htFields["MCDAT"] = value;
            }
        }
        #endregion

        #region DataMember
        UserInfo UserData = new UserInfo();
        protected override void SettingFields()
        {
            htFields.Add("UniqueID", null);
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MATNR", null);
            htFields.Add("INSMK", null);
            htFields.Add("MBLNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("LIFNR", null);
            htFields.Add("RMANO", null);
            htFields.Add("EBELN", null);
            htFields.Add("INDAT", null);
            htFields.Add("MENGE", null);
            htFields.Add("QCQTY", null);
            htFields.Add("REFNO", null);
            htFields.Add("MRGID", null);
            htFields.Add("ISPTM", null);
            htFields.Add("REQTY", null);
            htFields.Add("KDMAT", null);
            htFields.Add("RMAK1", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("SERNO", null);
            htFields.Add("LOCOD", null);
            htFields.Add("INSPT", null);
            htFields.Add("BKQTY", null);
            htFields.Add("DACOD", null);
            htFields.Add("BOXID", null);
            htFields.Add("VEDAT", null);
            htFields.Add("NLOCA", null);
            htFields.Add("SIDNO", null);
            htFields.Add("REFID", null);
            htFields.Add("SEQNO", null);
            htFields.Add("PKDAT", null);
            htFields.Add("ExpiryDate", null);
            htFields.Add("TASKID", null);
            htFields.Add("MAXEXP", null);
            htFields.Add("MRBNO", null);
            htFields.Add("LOCKED", null);
            htFields.Add("LKDAT", null);
            htFields.Add("MARNO", null);
            htFields.Add("ITEMSTATES", null);
            htFields.Add("STOCSTATES", null);
            htFields.Add("CONFIG", null);
            htFields.Add("MCDAT", null);
            htFieldType.Add("UniqueID", DBDataType.DBString);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("RMANO", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("INDAT", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBFunc);
            htFieldType.Add("QCQTY", DBDataType.DBNumber);
            htFieldType.Add("REFNO", DBDataType.DBString);
            htFieldType.Add("MRGID", DBDataType.DBString);
            htFieldType.Add("ISPTM", DBDataType.DBString);
            htFieldType.Add("REQTY", DBDataType.DBNumber);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("RMAK1", DBDataType.DbunString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("INSPT", DBDataType.DBString);
            htFieldType.Add("BKQTY", DBDataType.DBNumber);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("NLOCA", DBDataType.DBString);
            htFieldType.Add("SIDNO", DBDataType.DBString);
            htFieldType.Add("REFID", DBDataType.DBString);
            htFieldType.Add("SEQNO", DBDataType.DBString);
            htFieldType.Add("PKDAT", DBDataType.DBString);
            htFieldType.Add("ExpiryDate", DBDataType.DBString);
            htFieldType.Add("TASKID", DBDataType.DBString);
            htFieldType.Add("MAXEXP", DBDataType.DBString);
            htFieldType.Add("MRBNO", DBDataType.DBString);
            htFieldType.Add("LOCKED", DBDataType.DBString);
            htFieldType.Add("LKDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MARNO", DBDataType.DBString);
            htFieldType.Add("ITEMSTATES", DBDataType.DBString);
            htFieldType.Add("STOCSTATES", DBDataType.DBString);
            htFieldType.Add("CONFIG", DBDataType.DBString);
            htFieldType.Add("MCDAT", DBDataType.DBCreateDate);
        }
        #endregion
    }
}
