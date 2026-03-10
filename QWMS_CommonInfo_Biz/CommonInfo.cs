using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;

namespace QWMS.Common
{
    public sealed class CommonInfo
    {
        public readonly static CommonInfo Instance = new CommonInfo();
               
         
        #region Constructor
        private CommonInfo()
        {

        }
        #endregion

        #region DataMember

        #region DBType
        private int strDBType = 1;
        /// <summary>
        /// DBType
        /// </summary>
        public int DBType
        {
            get { return strDBType; }
            set { strDBType = value; }
        }
        #endregion

        #region DBCode
        private string strDBCode = "9200";
        /// <summary>
        /// DBCode
        /// </summary>
        public string DBCode
        {
            get { return strDBCode; }
            set { strDBCode = value; }
        }
        #endregion

        #region DBRCode
        private string strDBCode_R = "9200";
        /// <summary>
        /// DBCode
        /// </summary>
        public string DBRCode
        {
            get { return strDBCode_R; }
            set { strDBCode_R = value; }
        }
        #endregion

        #region ErrType
        private int strErrType = 2;
        /// <summary>
        /// ErrType
        /// </summary>
        public int ErrType
        {
            get { return strErrType; }
            set { strErrType = value; }
        }
        #endregion

        #region ErrCode
        private string strErrCode = "ERR";
        /// <summary>
        /// ErrCode
        /// </summary>
        public string ErrCode
        {
            get { return strErrCode; }
            set { strErrCode = value; }
        }
        #endregion

        #region DBCode_Qec
        private string strDBCode_Qec = "QEC";
        /// <summary>
        /// DBCode_Qec
        /// </summary>
        public string DBCode_Qec
        {
            get { return strDBCode_Qec; }
            set { strDBCode_Qec = value; }
        }
        #endregion


        #region DBCode_SF
        private string strDBCode_SF = "WIN";
        /// <summary>
        /// DBCode_SF
        /// </summary>
        public string DBCode_SF
        {
            get { return strDBCode_SF; }
            set { strDBCode_SF = value; }
        }
        #endregion

        #region DBServerName
        private string strDBServerName = "qcihrdbtest";
        /// <summary>
        /// ServerName
        /// </summary>
        public string DBServerName
        {
            get { return strDBServerName; }
            set { strDBServerName = value; }
        }
        #endregion



        #region DBName
        private string strDBName = "QWMS";
        /// <summary>
        /// DBName
        /// </summary>
        public string DBName
        {
            get { return strDBName; }
            set { strDBName = value; }
        }
        #endregion





        #endregion

        #region MemberFunction


        #region SortDataTable 依傳入的欄位順序排序DataTable的內容 by Davis
        /// <summary>
        /// SortDataTable 依傳入的欄位順序排序DataTable的內容 by Marc
        /// </summary>
        /// <param name="varSourceDataTable">欲進行排序的DataTable。</param>
        /// <param name="varSort">Sort String。</param>
        /// <returns>
        /// 回傳值型態為bool。
        /// </returns>
        /// <example>
        /// <code>
        ///  ClaCommon objClaCommon = new ClaCommon(1, "TEST", 2, "ERR");
        ///  DataTable dtResult = new DataTable();
        ///  dtResult = objClaCommon.GetOriginCountry(dtResult,"CTRNAM,CTRLID desc");
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public static DataTable SortDataTable(DataTable varSourceDataTable, string varSort)
        {
                #region Code Here................

                DataTable dtResult = varSourceDataTable.Clone();
                //大於1才需要排序, 因為等於1可能只有一個欄位值->Select,  by Davis
                if (varSourceDataTable.Rows.Count > 1)
                {
                    DataRow[] arrTmpRow = varSourceDataTable.Select("", varSort);
                    for (int i = 0; i < arrTmpRow.Length; i++)
                    {
                        dtResult.Rows.Add(arrTmpRow[i].ItemArray);
                    }
                }
                else
                {
                    dtResult = varSourceDataTable;
                }
                return dtResult;

                #endregion

        }
        #endregion

        public static string GetWhereEqual(string varFieldName, string varFieldValue1,bool varAllowEmptyValue)
        {
            string strTempWhereStr = "";
            if (varAllowEmptyValue == true)
            {
                strTempWhereStr = " ( " + varFieldName + " = '" + varFieldValue1 + "') ";
            }
            else
            {
                if (varFieldValue1.Trim() != "")
                {
                    strTempWhereStr = " ( " + varFieldName + " = '" + varFieldValue1.Trim() + "') ";
                }
            }            
            return strTempWhereStr;

        }


        public static string GetWhereBetween(string varFieldName, string varFieldValue1, string varFieldValue2)
        {
            string strTempWhereStr = "";
            if (varFieldValue1.Trim() == "" && varFieldValue2.Trim() == "")
            {
                strTempWhereStr = "";
            }
            else if (varFieldValue1.Trim() != "" && varFieldValue2.Trim() == "")
            {
                strTempWhereStr = " (" + varFieldName + "= '" + varFieldValue1 + "') ";
            }
            else if (varFieldValue1.Trim() == "" && varFieldValue2.Trim() != "")
            {
                strTempWhereStr= " (" + varFieldName + "<= '" + varFieldValue2 + "') ";
            }
            else if (varFieldValue1.Trim() != "" && varFieldValue2.Trim() != "")
            {
                strTempWhereStr = " (" + varFieldName + " Between '" + varFieldValue1 + "' and '" + varFieldValue2 + "') ";
            }
            return strTempWhereStr;

         }


        



        #endregion
    }
}
