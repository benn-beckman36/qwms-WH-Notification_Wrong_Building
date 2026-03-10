using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.Services;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;
using QWMS.Entity;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SqlClient;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// MaterialPic 的摘要描述。
        /// </summary>
        public class MaterialPic : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strMblnr = "";
            private string strProgid = "";
            private string strCrnam = "";
            private string strErrmsg = "";
            private string strSttyp = "";
            private string strLotyp = "";
            private string strException = "";
            string strFileName = ""; //記錄產生查詢文檔的檔名


            #region Constructer

            #region 不傳入參數產生StorageIn物件
            public MaterialPic()
            {

            }

            ////////////Summary by Donald Chen////////////////////////////////////////////
            /// <summary>
            /// 不傳入任何參數產生StorageIn物件。
            /// </summary>
            /// <param name="varUserData"></param>
            public MaterialPic(UserInfo varUserData, string strProgid)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, strProgid)
            {
            }
            #endregion

            #endregion

            #region 利用傳入參數產生MaterialPic物件

            ////////////Summary by Rock Teng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.StorageIn物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public MaterialPic(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string strProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();


                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                PROGID = strProgid;
                CRNAM = varUserData.UserId;

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.StorageData";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Rock Tzeng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            ////////////Summary by Eric Chou////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.StorageIn物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="strConnectionString">連結SQL Server的Connection String。</param>
            /// <param name="strMandt">SAP CLIENT。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strMblnr">單據號碼。</param>
            /// <param name="strProgid">程式代碼。</param>
            /// <param name="strCrnam">建立者。</param>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.StorageIn objStorageIn =new QCI.QWMS.StorageIn(strConnectionString,strMandt,strWerks,strLgort,strMblnr,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////

            #endregion

            #region DataMember

            UserInfo UserData = new UserInfo();
            #region 變數
            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// SAP Client。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string MANDT
            {
                get { return strMandt; }
                set { strMandt = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Company code。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
            }
            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 廠區。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string WERKS
            {
                get { return strWerks; }
                set { strWerks = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 倉別。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string LGORT
            {
                get { return strLgort; }
                set { strLgort = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 單據號碼。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string MBLNR
            {
                get { return strMblnr; }
                set { strMblnr = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 程式代碼。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string PROGID
            {
                get { return strProgid; }
                set { strProgid = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 建立者。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string CRNAM
            {
                get { return strCrnam; }
                set { strCrnam = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 錯誤訊息。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 倉別類型。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string STTYP
            {
                get { return strSttyp; }
                set { strSttyp = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 儲位類型。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string LOTYP
            {
                get { return strLotyp; }
                set { strLotyp = value; }
            }


            #endregion

            #endregion 變數

            public bool CheckWHPAT(string strMaterialNo)
            {
                string strSQL = "";
                bool bolChecked = false;
                DataTable dtData = new DataTable();

                strSQL = "select * from WHPAT where MATNR='" + strMaterialNo + "'";
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtData.Rows.Count == 0)
                    {
                        bolChecked = false;
                    }
                    else
                    {
                        bolChecked = true;
                    }
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex)
                {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    ControlExceptionType = ex.GetType().FullName;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw new Exception("999");
                }
                return bolChecked;
            }

            public bool CheckWHPIC(string strMaterialNo)
            {
                string strSQL = "";
                bool bolChecked = false;
                DataTable dtData = new DataTable();

                strSQL = "select * from WHPIC where MATNR='" + strMaterialNo + "'";
                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(strSQL.ToString());
                    ControlSqlAccess.CloseConnection();

                    if (dtData.Rows.Count == 0)
                    {
                        bolChecked = false;
                    }
                    else
                    {
                        bolChecked = true;
                    }
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
                catch (Exception ex)
                {
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.Message;
                    ControlExceptionType = ex.GetType().FullName;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw new Exception("999");
                }
                return bolChecked;
            }

            public DataTable GetMaterialPic(string strMaterialNo, string strChineseName, string strOriginalCountry)
            {
                StringBuilder sbSQL = new StringBuilder();
                DataTable dtData = new DataTable();

                sbSQL.Append("select MATNR,[DescriptionCn/En],OriginCountry,PicURL,UpdateBy,UpdateTime from WHPIC where 1=1 ");
                if (strMaterialNo != "")
                {
                    sbSQL.AppendFormat("and MATNR like '%{0}%' ", strMaterialNo);
                }
                if (strChineseName != "")
                {
                    sbSQL.AppendFormat("and [DescriptionCn/En] like '%{0}%' ", strChineseName);
                }
                if (strOriginalCountry != "")
                {
                    sbSQL.AppendFormat("and OriginCountry like '%{0}%' ", strOriginalCountry);
                }
                sbSQL.AppendFormat("order by MATNR desc");

                try
                {
                    ControlHandleDB();
                    dtData = ControlSqlAccess.GetDataTable(sbSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dtData;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
            }

            public bool InsertWHPIC(string MATNR, string DescriptionCnEn, string OriginCountry, string Usrnm, string PicURL)
            {

                bool bolChecked = false;
                DataTable dtData = new DataTable();
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append("insert into WHPIC([MATNR],[DescriptionCn/En],[OriginCountry],[UpdateTime],[UpdateBy],[CreateBy],[DeleteMark],[PicURL]) ")
                     .AppendFormat("values ('{0}','{1}','{2}',getdate(),'{3}','','','{4}')", MATNR, DescriptionCnEn, OriginCountry, Usrnm, PicURL);
                try
                {
                    ControlHandleDB();
                    bolChecked = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return bolChecked;

                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
                // return bolChecked;

            }

            public bool DeleteMaterialPic(string strMaterialNo)
            {
                StringBuilder sbSQL = new StringBuilder();
                bool result = true;
                sbSQL.AppendFormat("delete from WHPIC where MATNR='{0}' ", strMaterialNo);

                try
                {
                    ControlHandleDB();
                    result = ControlSqlAccess.ExecSql(sbSQL.ToString());
                    ControlSqlAccess.CloseConnection();
                    return result;
                }
                catch (CommonObjectsException ex)
                {
                    //讀取Common Object的真正錯誤訊息，例如ControlSqlAccess.ErrorMessage
                    ControlErrorDescription = ControlErrorDescription + ";" + ex.SourceErrMsg;
                    ControlExceptionType = ex.SourceExceptionType;
                    this.ControlPriority = "1";
                    ControlHandleError(ex.Message, ex.Source, ex.StackTrace);
                    throw ex;
                }//可自行增加要handle的Exception  
            }
        }
    }
}
