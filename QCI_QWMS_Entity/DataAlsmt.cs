using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Entity
{
    public class DataAlsmt : EntityBase
    {
        #region Constructor
        #region
        public DataAlsmt(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 构造
        public DataAlsmt(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataAlsmt";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.ALSMT";

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
        public string Werks {
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

        #region Kostl
        //  <summary>
        // Kostl
        //  <summary>
        public string Kostl {
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

        #region Grpid
        //  <summary>
        // Grpid
        //  <summary>
        public string Grpid
        {
            get
            {
                if (htFields["GRPID"] == null)
                {
                    return null;
                }
                return htFields["GRPID"].ToString();
            }
            set
            {
                htFields["GRPID"] = value;
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
        
        #region Dacod
        //  <summary>
        // Dacod
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
        
        #region Zeile
        //  <summary>
        // Zeile
        //  <summary>
        public string Zeile
        {
            get
            {
                if (htFields["ZEILE"] == null)
                {
                    return null;
                }
                return htFields["ZEILE"].ToString();
            }
            set
            {
                htFields["ZEILE"] = value;
            }
        }
        #endregion

        #region Umlgo
        //  <summary>
        // Umlgo
        //  <summary>
        public string Umlgo
        {
            get
            {
                if (htFields["UMLGO"] == null)
                {
                    return null;
                }
                return htFields["UMLGO"].ToString();
            }
            set
            {
                htFields["UMLGO"] = value;
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
            set {
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

        #region Modat
        //  <summary>
        // Crdat
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

        #region Line
        //  <summary>
        // Line
        //  <summary>
        public string Line
        {
            get
            {
                if (htFields["Line"] == null)
                {
                    return null;
                }
                return htFields["Line"].ToString();
            }
            set
            {
                htFields["Line"] = value;
            }
        }
        #endregion

        #region Side
        //  <summary>
        // Side
        //  <summary>
        public string Side
        {
            get
            {
                if (htFields["Side"] == null)
                {
                    return null;
                }
                return htFields["Side"].ToString();
            }
            set
            {
                htFields["Side"] = value;
            }
        }
        #endregion

        #region Machine
        //  <summary>
        // Machine
        //  <summary>
        public string Machine
        {
            get
            {
                if (htFields["Machine"] == null)
                {
                    return null;
                }
                return htFields["Machine"].ToString();
            }
            set
            {
                htFields["Machine"] = value;
            }
        }
        #endregion

        #region SDTSlot
        //  <summary>
        // SDTSlot
        //  <summary>
        public string SDTSlot
        {
            get
            {
                if (htFields["SDTSlot"] == null)
                {
                    return null;
                }
                return htFields["SDTSlot"].ToString();
            }
            set
            {
                htFields["SDTSlot"] = value;
            }
        }
        #endregion

        #region SDTLr
        //  <summary>
        // SDTLr
        //  <summary>
        public string SDTLr
        {
            get
            {
                if (htFields["SDTLr"] == null)
                {
                    return null;
                }
                return htFields["SDTLr"].ToString();
            }
            set
            {
                htFields["SDTLr"] = value;
            }
        }
        #endregion


        #region DataMember
        UserInfo UserData = new UserInfo();
        protected override void SettingFields() {
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("KOSTL", null);
            htFields.Add("GRPID", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MATNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("SERNO", null);
            htFields.Add("LIFER", null);
            htFields.Add("LOCOD", null);
            htFields.Add("DACOD", null);
            htFields.Add("DIDNO", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("UMLGO", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFields.Add("FLAGE", null);
            htFields.Add("Line", null);
            htFields.Add("Side", null);
            htFields.Add("Machine", null);
            htFields.Add("SDTSlot", null);
            htFields.Add("SDTLr", null);

            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("DIDNO", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("UMLGO", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("FLAGE", DBDataType.DBString);
            htFieldType.Add("Line", DBDataType.DBString);
            htFieldType.Add("Side", DBDataType.DBString);
            htFieldType.Add("Machine", DBDataType.DBString);
            htFieldType.Add("SDTSlot", DBDataType.DBString);
            htFieldType.Add("SDTLr", DBDataType.DBString);
        }
        #endregion
    }
}
