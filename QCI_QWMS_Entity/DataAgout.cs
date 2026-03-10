using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Entity
{
    public class DataAgout : EntityBase
    {
        #region Constructor
        #region
        public DataAgout(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 构造
        public DataAgout(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataAgout";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.AGOUT";

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

        #region Pri
        //  <summary>
        // Pri
        //  <summary>
        public string Pri
        {
            get
            {
                if (htFields["PRI"] == null)
                {
                    return null;
                }
                return htFields["PRI"].ToString();
            }
            set
            {
                htFields["PRI"] = value;
            }
        }
        #endregion

        #region Subpri
        //  <summary>
        // Subpri
        //  <summary>
        public string Subpri
        {
            get
            {
                if (htFields["SUBPRI"] == null)
                {
                    return null;
                }
                return htFields["SUBPRI"].ToString();
            }
            set
            {
                htFields["SUBPRI"] = value;
            }
        }
        #endregion

        #region DIDNO
        //  <summary>
        // DIDNO
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

        #region Contrno
        //  <summary>
        // Contrno
        //  <summary>
        public string Contrno
        {
            get
            {
                if (htFields["CONTRNO"] == null)
                {
                    return null;
                }
                return htFields["CONTRNO"].ToString();
            }
            set
            {
                htFields["CONTRNO"] = value;
            }
        }
        #endregion

        #region X
        //  <summary>
        // X
        //  <summary>
        public string X
        {
            get
            {
                if (htFields["X"] == null)
                {
                    return null;
                }
                return htFields["X"].ToString();
            }
            set
            {
                htFields["X"] = value;
            }
        }
        #endregion

        #region Y
        //  <summary>
        // Y
        //  <summary>
        public string Y
        {
            get
            {
                if (htFields["Y"] == null)
                {
                    return null;
                }
                return htFields["Y"].ToString();
            }
            set
            {
                htFields["Y"] = value;
            }
        }
        #endregion

        #region Z
        //  <summary>
        // Z
        //  <summary>
        public string Z
        {
            get
            {
                if (htFields["Z"] == null)
                {
                    return null;
                }
                return htFields["Z"].ToString();
            }
            set
            {
                htFields["Z"] = value;
            }
        }
        #endregion

        #region R
        //  <summary>
        // R
        //  <summary>
        public string R
        {
            get
            {
                if (htFields["R"] == null)
                {
                    return null;
                }
                return htFields["R"].ToString();
            }
            set
            {
                htFields["R"] = value;
            }
        }
        #endregion

        #region Diameter
        //  <summary>
        // Diameter
        //  <summary>
        public string Diameter
        {
            get
            {
                if (htFields["Diameter"] == null)
                {
                    return null;
                }
                return htFields["Diameter"].ToString();
            }
            set
            {
                htFields["Diameter"] = value;
            }
        }
        #endregion

        #region R
        //  <summary>
        // Thickness
        //  <summary>
        public string Thickness
        {
            get
            {
                if (htFields["Thickness"] == null)
                {
                    return null;
                }
                return htFields["Thickness"].ToString();
            }
            set
            {
                htFields["Thickness"] = value;
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

        #region Insmk
        //  <summary>
        // Alqty
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

        #region Dacod_before
        //  <summary>
        // Dacod_before
        //  <summary>
        public string DACOD_before
        {
            get
            {
                if (htFields["DACOD_before"] == null)
                {
                    return null;
                }
                return htFields["DACOD_before"].ToString();
            }
            set
            {
                htFields["DACOD_before"] = value;
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

        #region Serno
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

        #region DataMember
        UserInfo UserData = new UserInfo();
        protected override void SettingFields()
        {
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("PRI", null);
            htFields.Add("SUBPRI", null);
            htFields.Add("DIDNO", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("LOCAT", null);
            htFields.Add("CONTRNO", null);
            htFields.Add("X", null);
            htFields.Add("Y", null);
            htFields.Add("Z", null);
            htFields.Add("R", null);
            htFields.Add("Diameter", null);
            htFields.Add("Thickness", null);
            htFields.Add("MATNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("INSMK", null);
            htFields.Add("LIFNR", null);
            htFields.Add("DACOD", null);
            htFields.Add("DACOD_before", null);
            htFields.Add("LOCOD", null);
            htFields.Add("SERNO", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
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
            htFieldType.Add("PRI", DBDataType.DBNumber);
            htFieldType.Add("SUBPRI", DBDataType.DBNumber);
            htFieldType.Add("DIDNO", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("CONTRNO", DBDataType.DBString);
            htFieldType.Add("X", DBDataType.DBNumber);
            htFieldType.Add("Y", DBDataType.DBNumber);
            htFieldType.Add("Z", DBDataType.DBNumber);
            htFieldType.Add("R", DBDataType.DBNumber);
            htFieldType.Add("Diameter", DBDataType.DBNumber);
            htFieldType.Add("Thickness", DBDataType.DBNumber);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("DACOD_before", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
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
