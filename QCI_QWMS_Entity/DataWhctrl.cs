using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;





namespace QWMS.Entity
{


    //  <summary>
    // DataWhctrl 針對 WHCTRL Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhctrl : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhctrl物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhctrl物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhctrl objWhctrl = new DataWhctrl();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhctrl(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhctrl物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhctrl物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhctrl objWhctrl = new DataWhctrl(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhctrl(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhctrl";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHCTRL";

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

        #region Soldto
        //  <summary>
        // Soldto
        //  <summary>
        public string Soldto
        {
            get
            {
                if (htFields["SOLDTO"] == null)
                {
                    return null;
                }
                return htFields["SOLDTO"].ToString();
            }
            set
            {
                htFields["SOLDTO"] = value;
            }
        }
        #endregion

        #region Ctrlid
        //  <summary>
        // Ctrlid
        //  <summary>
        public string Ctrlid
        {
            get
            {
                if (htFields["CTRLID"] == null)
                {
                    return null;
                }
                return htFields["CTRLID"].ToString();
            }
            set
            {
                htFields["CTRLID"] = value;
            }
        }
        #endregion

        #region Ctrlnm
        //  <summary>
        // Ctrlnm
        //  <summary>
        public string Ctrlnm
        {
            get
            {
                if (htFields["CTRLNM"] == null)
                {
                    return null;
                }
                return htFields["CTRLNM"].ToString();
            }
            set
            {
                htFields["CTRLNM"] = value;
            }
        }
        #endregion

        #region Ctrlc1
        //  <summary>
        // Ctrlc1
        //  <summary>
        public string Ctrlc1
        {
            get
            {
                if (htFields["CTRLC1"] == null)
                {
                    return null;
                }
                return htFields["CTRLC1"].ToString();
            }
            set
            {
                htFields["CTRLC1"] = value;
            }
        }
        #endregion

        #region Ctrlc2
        //  <summary>
        // Ctrlc2
        //  <summary>
        public string Ctrlc2
        {
            get
            {
                if (htFields["CTRLC2"] == null)
                {
                    return null;
                }
                return htFields["CTRLC2"].ToString();
            }
            set
            {
                htFields["CTRLC2"] = value;
            }
        }
        #endregion

        #region Ctrlc3
        //  <summary>
        // Ctrlc3
        //  <summary>
        public string Ctrlc3
        {
            get
            {
                if (htFields["CTRLC3"] == null)
                {
                    return null;
                }
                return htFields["CTRLC3"].ToString();
            }
            set
            {
                htFields["CTRLC3"] = value;
            }
        }
        #endregion

        #region Ctrlc4
        //  <summary>
        // Ctrlc4
        //  <summary>
        public string Ctrlc4
        {
            get
            {
                if (htFields["CTRLC4"] == null)
                {
                    return null;
                }
                return htFields["CTRLC4"].ToString();
            }
            set
            {
                htFields["CTRLC4"] = value;
            }
        }
        #endregion

        #region Ctrlc5
        //  <summary>
        // Ctrlc5
        //  <summary>
        public string Ctrlc5
        {
            get
            {
                if (htFields["CTRLC5"] == null)
                {
                    return null;
                }
                return htFields["CTRLC5"].ToString();
            }
            set
            {
                htFields["CTRLC5"] = value;
            }
        }
        #endregion

        #region Ctrln1
        //  <summary>
        // Ctrln1
        //  <summary>
        public string Ctrln1
        {
            get
            {
                if (htFields["CTRLN1"] == null)
                {
                    return null;
                }
                return htFields["CTRLN1"].ToString();
            }
            set
            {
                htFields["CTRLN1"] = value;
            }
        }
        #endregion

        #region Ctrln2
        //  <summary>
        // Ctrln2
        //  <summary>
        public string Ctrln2
        {
            get
            {
                if (htFields["CTRLN2"] == null)
                {
                    return null;
                }
                return htFields["CTRLN2"].ToString();
            }
            set
            {
                htFields["CTRLN2"] = value;
            }
        }
        #endregion

        #region Ctrln3
        //  <summary>
        // Ctrln3
        //  <summary>
        public string Ctrln3
        {
            get
            {
                if (htFields["CTRLN3"] == null)
                {
                    return null;
                }
                return htFields["CTRLN3"].ToString();
            }
            set
            {
                htFields["CTRLN3"] = value;
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

        
        #region PAPM
        //  <summary>
        //   PAPM
        //  <summary>
        public string PAPM
        {
            get
            {
                if (htFields["PAPM"] == null)
                {
                    return null;
                }
                return htFields["PAPM"].ToString();
            }
            set
            {
                htFields["PAPM"] = value;
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
            htFields.Add("SOLDTO", null);
            htFields.Add("CTRLID", null);
            htFields.Add("CTRLNM", null);
            htFields.Add("CTRLC1", null);
            htFields.Add("CTRLC2", null);
            htFields.Add("CTRLC3", null);
            htFields.Add("CTRLC4", null);
            htFields.Add("CTRLC5", null);
            htFields.Add("CTRLN1", null);
            htFields.Add("CTRLN2", null);
            htFields.Add("CTRLN3", null);
            htFields.Add("REMAK", null);
            htFields.Add("COMCD", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("SOLDTO", DBDataType.DBString);
            htFieldType.Add("CTRLID", DBDataType.DBString);
            htFieldType.Add("CTRLNM", DBDataType.DBString);
            htFieldType.Add("CTRLC1", DBDataType.DbunString);
            htFieldType.Add("CTRLC2", DBDataType.DBString);
            htFieldType.Add("CTRLC3", DBDataType.DBString);
            htFieldType.Add("CTRLC4", DBDataType.DBString);
            htFieldType.Add("CTRLC5", DBDataType.DbunString);
            htFieldType.Add("CTRLN1", DBDataType.DBNumber);
            htFieldType.Add("CTRLN2", DBDataType.DBNumber);
            htFieldType.Add("CTRLN3", DBDataType.DBNumber);
            htFieldType.Add("REMAK", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("PAPM", DBDataType.DBString);
        }
        #endregion
    }
}
