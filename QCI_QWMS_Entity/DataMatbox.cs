using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Entity
{
    public class DataMatbox : EntityBase
    {
        #region 构造
        #region 
        public DataMatbox(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 
        public DataMatbox(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataMatbox";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "MATBOX";

            SetFields();
        }

        #endregion
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

        #region Boxid
        //  <summary>
        // Boxid
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

        #region Boxitem
        //  <summary>
        // Boxitem
        //  <summary>
        public string Boxitem
        {
            get
            {
                if (htFields["BOXITEM"] == null)
                {
                    return null;
                }
                return htFields["BOXITEM"].ToString();
            }
            set
            {
                htFields["BOXITEM"] = value;
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

        #region Vedat
        //  <summary>
        // Vedat
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

        #region Cretime
        //  <summary>
        // Cretime
        //  <summary>
        public string Cretime
        {
            get
            {
                if (htFields["CRETIME"] == null)
                {
                    return null;
                }
                return htFields["CRETIME"].ToString();
            }
            set
            {
                htFields["CRETIME"] = value;
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

        #region DataMember
        UserInfo UserData = new UserInfo();
        protected override void SettingFields()
        {
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("BOXID", null);
            htFields.Add("BOXITEM", null);
            htFields.Add("SERNO", null);
            htFields.Add("MATNR", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MENGE", null);
            htFields.Add("OTQTY", null);
            htFields.Add("LIFNR", null);
            htFields.Add("DACOD", null);
            htFields.Add("LOCOD", null);
            htFields.Add("VEDAT", null);
            htFields.Add("INDAT", null);
            htFields.Add("CRETIME", null);
            htFields.Add("REMAK", null);

            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("BOXITEM", DBDataType.DBString);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("OTQTY", DBDataType.DBNumber);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("INDAT", DBDataType.DBString);
            htFieldType.Add("CRETIME", DBDataType.DBCreateDate);
            htFieldType.Add("REMAK", DBDataType.DBString);
            
        }
        #endregion
    }
}
