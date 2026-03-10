using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Collections;

namespace QWMS
{
    public partial class Manage_ReceivePlatformReport : Form
    {
        //public Manage_ReceivePlatformReport()
        //{
        //    InitializeComponent();
        //}

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        //private string strLgort = "";
        private string strProgid = "";
        private DataTable dtCarECSource = new DataTable();
        DataTable dtSum = new DataTable();

        #endregion

        #region DataMember

        public string Mandt
        {
            get
            {
                return strMandt;
            }
            set
            {
                strMandt = value;
            }
        }

        public string Comcd
        {
            get
            {
                return strComcd;
            }
            set
            {
                strComcd = value;
            }
        }

        public string Usrnm
        {
            get
            {
                return strUsrnm;
            }
            set
            {
                strUsrnm = value;
            }
        }

        public string Progid
        {
            get
            {
                return strProgid;
            }
            set
            {
                strProgid = value;
            }
        }

        public string Werks
        {
            get
            {
                return strWerks;
            }
            set
            {
                strWerks = value;
            }
        }

        #endregion

        #region Constructor

        public Manage_ReceivePlatformReport(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            //dtECSource.Columns.Add("ITEM");
            //dtECSource.Columns.Add("VBELN");
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            //lblCompany.Text = Comcd;
            //lblUserid.Text = Usrnm;

            try
            {
                //QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                //objCarData = new CarData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                //if (false)
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    //ShowDdlLgort();
                    //ShowPrintCheckBox();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    //if (cmbLgort.Items.Count > 0)
                    //{
                    //    this.cmbLgort.SelectedIndex = 0;
                    //}

                    // ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                cmbWerks.Items.Add("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                cmbWerks.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            ECNumber.AllowUserToAddRows = false;
            ECNumber.AutoGenerateColumns = false;
            ECNumber.Columns.Clear();

            try
            {
                //设置字体属性
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                Font f = new Font("宋体", 10, FontStyle.Bold);
                style.Font = f;
                style.ForeColor = Color.Red;
                


                //WERKS  厂区
                DataGridViewTextBoxColumn dgvWERKS = new DataGridViewTextBoxColumn();
                dgvWERKS.DataPropertyName = "WERKS";
                dgvWERKS.HeaderText = "";
                dgvWERKS.Width = 60;
                dgvWERKS.ReadOnly = true;
                ECNumber.Columns.Add(dgvWERKS);

                //DayEc  一天进行数量 
                DataGridViewTextBoxColumn DayEc = new DataGridViewTextBoxColumn();
                DayEc.DataPropertyName = "DayEc";
                DayEc.HeaderText = "进行中数量(天)";
                DayEc.Width = 90;
                DayEc.ReadOnly = true;
                ECNumber.Columns.Add(DayEc);

                //DayEx  一天超时数量 
                DataGridViewTextBoxColumn DayEx = new DataGridViewTextBoxColumn();
                DayEx.DataPropertyName = "DayEx";
                DayEx.HeaderText = "超时数量(天)";
                DayEx.Width = 100;
                DayEx.ReadOnly = true;
                ECNumber.Columns.Add(DayEx);

                //DayNum  一天完成数量 
                DataGridViewTextBoxColumn DayNum = new DataGridViewTextBoxColumn();
                DayNum.DataPropertyName = "DayNum";
                DayNum.HeaderText = "已完成数量(天)";
                DayNum.Width = 100;
                DayNum.ReadOnly = true;
                ECNumber.Columns.Add(DayNum);

                //DayEx  一天总数量 
                DataGridViewTextBoxColumn DayTotal = new DataGridViewTextBoxColumn();
                DayTotal.DataPropertyName = "DayTotal";
                DayTotal.HeaderText = "总数量(天)";
                DayTotal.Width = 100;
                DayTotal.ReadOnly = true;
                DayTotal.DefaultCellStyle = style; 
                ECNumber.Columns.Add(DayTotal);

                //AWeekNum  一周进行数量 
                DataGridViewTextBoxColumn AWeekNum = new DataGridViewTextBoxColumn();
                AWeekNum.DataPropertyName = "AWeekNum";
                AWeekNum.HeaderText = "进行中数量(周)";
                AWeekNum.Width = 90;
                AWeekNum.ReadOnly = true;
                ECNumber.Columns.Add(AWeekNum);

                //AWeekException  一周超时数量 
                DataGridViewTextBoxColumn AWeekException = new DataGridViewTextBoxColumn();
                AWeekException.DataPropertyName = "AWeekException";
                AWeekException.HeaderText = "超时数量(周)";
                AWeekException.Width = 100;
                AWeekException.ReadOnly = true;
                ECNumber.Columns.Add(AWeekException);

                //AWeekException  一周完成数量 
                DataGridViewTextBoxColumn AWeekEnd = new DataGridViewTextBoxColumn();
                AWeekEnd.DataPropertyName = "AWeekEnd";
                AWeekEnd.HeaderText = "已完成数量(周)";
                AWeekEnd.Width = 100;
                AWeekEnd.ReadOnly = true;
                ECNumber.Columns.Add(AWeekEnd);

                //AWeekToTal  一周总数量 
                DataGridViewTextBoxColumn AWeekToTal = new DataGridViewTextBoxColumn();
                AWeekToTal.DataPropertyName = "AWeekToTal";
                AWeekToTal.HeaderText = "总数量(周)";
                AWeekToTal.Width = 100;
                AWeekToTal.ReadOnly = true;
                AWeekToTal.DefaultCellStyle = style; 
                ECNumber.Columns.Add(AWeekToTal);

                //MonEc  一月进行数量 
                DataGridViewTextBoxColumn MonEc = new DataGridViewTextBoxColumn();
                MonEc.DataPropertyName = "MonEc";
                MonEc.HeaderText = "进行中数量(月)";
                MonEc.Width = 90;
                MonEc.ReadOnly = true;
                ECNumber.Columns.Add(MonEc);

                //MonEx  一月超时数量 
                DataGridViewTextBoxColumn MonEx = new DataGridViewTextBoxColumn();
                MonEx.DataPropertyName = "MonEx";
                MonEx.HeaderText = "超时数量(月)";
                MonEx.Width = 100;
                MonEx.ReadOnly = true;
                ECNumber.Columns.Add(MonEx);

                //MonNum  一月完成数量 
                DataGridViewTextBoxColumn MonNum = new DataGridViewTextBoxColumn();
                MonNum.DataPropertyName = "MonNum";
                MonNum.HeaderText = "已完成数量(月)";
                MonNum.Width = 100;
                MonNum.ReadOnly = true;
                ECNumber.Columns.Add(MonNum);

                //MonTotal  一月完成数量 
                DataGridViewTextBoxColumn MonTotal = new DataGridViewTextBoxColumn();
                MonTotal.DataPropertyName = "MonTotal";
                MonTotal.HeaderText = "总数量(月)";
                MonTotal.Width = 100;
                MonTotal.ReadOnly = true;
                MonTotal.DefaultCellStyle = style; 
                ECNumber.Columns.Add(MonTotal);


                //DataGridViewTextBoxColumn TotalNum = new DataGridViewTextBoxColumn();
                //TotalNum.DataPropertyName = "TotalNum";
                //TotalNum.HeaderText = "进行中总数量";
                //TotalNum.Width = 100;
                //TotalNum.ReadOnly = true;
                //ECNumber.Columns.Add(TotalNum);
                
                ////TotalException  总计异常 
                //DataGridViewTextBoxColumn TotalException = new DataGridViewTextBoxColumn();
                //TotalException.DataPropertyName = "TotalException";
                //TotalException.HeaderText = "总超时数量";
                //TotalException.Width = 100;
                //TotalException.ReadOnly = true;
                //ECNumber.Columns.Add(TotalException);

                ////TotalNum  总计数量 
                //DataGridViewTextBoxColumn TotalEnd = new DataGridViewTextBoxColumn();
                //TotalEnd.DataPropertyName = "TotalEnd";
                //TotalEnd.HeaderText = "已完成总数量";
                //TotalEnd.Width = 100;
                //TotalEnd.ReadOnly = true;
                //ECNumber.Columns.Add(TotalEnd);



                ECNumber.DataSource = dtSum;

                
                //endOutSource.Text = dtCarECSource.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOrderNoDataGrid()");
            }

        }
         #endregion
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = cmbWerks.Text.ToString();
            //if(checkAll.Checked==true)
            //{
            //    strWerks = "";
            //}
            QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

            dtCarECSource = StorageIn.SelectWerks(strWerks);
            //添加一个状态，便于查询作业的进程
            dtCarECSource.Columns.Add("STATUS");
            //int ECNum = 0;
            //int exNum = 0;
            //int ECTotal = 0;
            //int exTotal = 0;
            //ArrayList OrderNoList=new ArrayList();
            //ArrayList exList=new ArrayList();
            //ArrayList TotalList = new ArrayList();
            //ArrayList TotalexList = new ArrayList();
            ArrayList WerksList = new ArrayList();
            
            //DataRow dr1 = dtSum.NewRow();
            //DataRow dr2 = dtSum.NewRow();
            dtSum.Clear();
            if (dtSum.Columns.Count == 0)
            {
                
                dtSum.Columns.Add("WERKS");
                dtSum.Columns.Add("DayEc");
                dtSum.Columns.Add("DayEx");
                dtSum.Columns.Add("DayNum");
                dtSum.Columns.Add("DayTotal");
                dtSum.Columns.Add("AWeekNum");
                dtSum.Columns.Add("AWeekException");
                dtSum.Columns.Add("AWeekEnd");
                dtSum.Columns.Add("AWeekTotal");
                dtSum.Columns.Add("MonEc");
                dtSum.Columns.Add("MonEx");
                dtSum.Columns.Add("MonNum");
                dtSum.Columns.Add("MonTotal");
                //dtSum.Columns.Add("TotalNum");
                //dtSum.Columns.Add("TotalException");
                //dtSum.Columns.Add("TotalEnd");
            }
            if (dtCarECSource.Rows.Count > 0)
            {
                for (int i = 0; i < dtCarECSource.Rows.Count; i++)
                {
                    WerksList.Add(dtCarECSource.Rows[i]["WERKS"]);
                }
                WerksList = DelRepetitive(WerksList);

                RportLogic(WerksList);
                //for (int j = 0; j < WerksList.Count; j++)
                //{
 
                //}


                //DateTime NowTime = DateTime.Now;
                
                
                //for (int i = 0; i < dtCarECSource.Rows.Count; i++)
                //{
                    
                //    TimeSpan fts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString());
                    
                //    if (dtCarECSource.Rows[i]["ENDAT"].ToString() == "")
                //    {
                //        dtCarECSource.Rows[i]["STATUS"] = "已报到";
                //        //TimeSpan fts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString());
                //        if (fts.Days <7)
                //        {
                //            if (fts.Days > 0 || fts.Hours > 2)
                //            {
                //                exNum = exNum + 1;
                //                exList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //            }
                //            else
                //            {
                //                ECNum = ECNum + 1;
                //                OrderNoList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //            }
                //        }
                //        if (fts.Days > 0 || fts.Hours > 2)
                //        {
                //            exTotal = exTotal + 1;
                //            TotalexList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        }
                //        else
                //        {
                //            ECTotal = ECTotal + 1;
                //            TotalList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        }
                //        //dtCarECSource.Rows[i]["INTERVAL"] = DateTime.Now.ToString() - dtCarECSource.Rows[i]["CRDAT"].ToString();
                //    }
                //    else if (dtCarECSource.Rows[i]["SAPDAT"].ToString() == "")
                //    {
                //        dtCarECSource.Rows[i]["STATUS"] = "已离厂";
                //        TimeSpan sts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["ENDAT"].ToString());

                //        if (fts.Days <7)
                //        {
                //            if (sts.Days > 0 || sts.Hours > 2)
                //            {
                //                exNum = exNum + 1;
                //                exList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //            }
                //            else
                //            {
                //                ECNum = ECNum + 1;
                //                OrderNoList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //            }
                //        }
                //        if (sts.Days > 0 || sts.Hours > 5)
                //        {
                //            exTotal = exTotal + 1;
                //            TotalexList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        }
                //        else
                //        {
                //            ECTotal = ECTotal + 1;
                //            TotalList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        }
                            
                //    }
                //    else
                //    {
                //        if (fts.Days <7)
                //        {
                //            ECNum = ECNum + 1;
                //            OrderNoList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        }
                //        ECTotal = ECTotal + 1;
                //        TotalList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                //        //dtCarECSource.Rows[i]["INTERVAL"] = "";
                //    }

                //}
                //dr1["WERKS"] = dtCarECSource.Rows[0]["WERKS"].ToString() + "车辆数";
                //dr1["AWeekNum"] = DelRepetitive(OrderNoList).Count;
                //dr1["AWeekException"] = DelRepetitive(exList).Count;
                //dr1["TotalNum"] = DelRepetitive(TotalList).Count;
                //dr1["TotalException"] = DelRepetitive(TotalexList).Count;
                //dtSum.Rows.Add(dr1);
                //dr2["WERKS"] = dtCarECSource.Rows[0]["WERKS"].ToString() + "EC单数";
                //dr2["AWeekNum"] = ECNum;
                //dr2["AWeekException"] = exNum;
                //dr2["TotalNum"] = ECTotal;
                //dr2["TotalException"] = exTotal;
                //dtSum.Rows.Add(dr2);
                //dtCarECSource = SortDataTable(dtCarECSource);
                ShowDataGrid();
                stsWarning.Text = "";
            }
            else
            {
                stsWarning.Text = "No Data!!!";
                ECNumber.DataSource = null;
            }
            
        }

        #region 去重
        private ArrayList DelRepetitive(ArrayList list)
        {
            ArrayList list2=new ArrayList();
            foreach(string orderno in list)
            {
                if(!list2.Contains(orderno))
                {
                    list2.Add(orderno);
                }
            }
            return list2;
        }
        #endregion

        #region 查询主要逻辑
        private void RportLogic(ArrayList WerksList)
        {
            DateTime NowTime = DateTime.Now;
            DateTime NowTime2 = DateTime.Now.AddDays(-7).Date;
            for (int j = 0; j < WerksList.Count; j++)
            {
                int DayEc = 0;
                int DayEx = 0;
                int DayNum = 0;

                int ECNum = 0;
                int exNum = 0;
                int endNum = 0;

                int MonEc = 0;
                int MonEx = 0;
                int MonNum = 0;

                //int ECTotal = 0;
                //int exTotal = 0;
                //int endTotal = 0;

                ArrayList DayList = new ArrayList();
                ArrayList DayExList = new ArrayList();
                ArrayList DayEndList = new ArrayList();

                ArrayList OrderNoList = new ArrayList();
                ArrayList exList = new ArrayList();
                ArrayList EndList = new ArrayList();

                ArrayList MonList = new ArrayList();
                ArrayList MonExList = new ArrayList();
                ArrayList MonEndList = new ArrayList();

                //ArrayList TotalECList = new ArrayList();
                //ArrayList TotalexList = new ArrayList();
                //ArrayList TotalList = new ArrayList();
                DataRow dr1 = dtSum.NewRow();
                DataRow dr2 = dtSum.NewRow();
                
                for (int i = 0; i < dtCarECSource.Rows.Count; i++)
                {
                    if (WerksList[j].ToString() == dtCarECSource.Rows[i]["WERKS"].ToString())
                    {
                        TimeSpan fts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString());

                        if (dtCarECSource.Rows[i]["ENDAT"].ToString() == "")
                        {
                            dtCarECSource.Rows[i]["STATUS"] = "已报到";
                            //TimeSpan fts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString());

                            if (NowTime.Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (fts.Days > 0 || fts.Hours > 2)
                                {
                                    DayEx = DayEx + 1;
                                    DayExList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    DayEc = DayEc + 1;
                                    DayList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            if (NowTime.AddDays(-6).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (fts.Days > 0 || fts.Hours > 2)
                                {
                                    exNum = exNum + 1;
                                    exList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    MonEc = MonEc + 1;
                                    OrderNoList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            if (NowTime.AddDays(-29).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (fts.Days > 0 || fts.Hours > 2)
                                {
                                    MonEx = MonEx + 1;
                                    MonExList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    ECNum = ECNum + 1;
                                    MonList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            //if (fts.Days > 0 || fts.Hours > 2)
                            //{
                            //    exTotal = exTotal + 1;
                            //    TotalexList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            //}
                            //else
                            //{
                            //    ECTotal = ECTotal + 1;
                            //    TotalECList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            //}
                            //dtCarECSource.Rows[i]["INTERVAL"] = DateTime.Now.ToString() - dtCarECSource.Rows[i]["CRDAT"].ToString();
                        }
                        else if (dtCarECSource.Rows[i]["SAPDAT"].ToString() == "")
                        {
                            dtCarECSource.Rows[i]["STATUS"] = "已离厂";
                            TimeSpan sts = NowTime - Convert.ToDateTime(dtCarECSource.Rows[i]["ENDAT"].ToString());

                            if (NowTime.Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (sts.Days > 0 || sts.Hours > 5)
                                {
                                    DayEx = DayEx + 1;
                                    DayExList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    DayEc = DayEc + 1;
                                    DayList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            if (NowTime.AddDays(-6).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (sts.Days > 0 || sts.Hours > 5)
                                {
                                    exNum = exNum + 1;
                                    exList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    ECNum = ECNum + 1;
                                    OrderNoList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            if (NowTime.AddDays(-29).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                if (sts.Days > 0 || sts.Hours > 5)
                                {
                                    MonEx = MonEx + 1;
                                    MonExList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                                else
                                {
                                    MonEc = MonEc + 1;
                                    MonList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                                }
                            }

                            //if (sts.Days > 0 || sts.Hours > 5)
                            //{
                            //    exTotal = exTotal + 1;
                            //    TotalexList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            //}
                            //else
                            //{
                            //    ECTotal = ECTotal + 1;
                            //    TotalECList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            //}

                        }
                        else
                        {
                            if (NowTime.Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                DayNum = DayNum + 1;
                                DayEndList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            }
                            if (NowTime.AddDays(-6).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                endNum = endNum + 1;
                                EndList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            }

                            if (NowTime.AddDays(-29).Date < Convert.ToDateTime(dtCarECSource.Rows[i]["CRDAT"].ToString()))
                            {
                                MonNum = MonNum + 1;
                                MonEndList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            }
                            //endTotal = endTotal + 1;
                            //TotalList.Add(dtCarECSource.Rows[i]["ORDERNO"]);
                            //dtCarECSource.Rows[i]["INTERVAL"] = "";
                        }

                    }
                }

                dr1["WERKS"] = WerksList[j].ToString() + "车辆数";
                dr1["DayEc"] = DelRepetitive(DayList).Count;
                dr1["DayEx"] = DelRepetitive(DayExList).Count;
                dr1["DayNum"] = DelRepetitive(DayEndList).Count;
                dr1["DayTotal"] = DelRepetitive(DayList).Count + DelRepetitive(DayExList).Count + DelRepetitive(DayEndList).Count;

                dr1["AWeekNum"] = DelRepetitive(OrderNoList).Count;
                dr1["AWeekException"] = DelRepetitive(exList).Count;
                dr1["AWeekEnd"] = DelRepetitive(EndList).Count;
                dr1["AWeekTotal"] = DelRepetitive(OrderNoList).Count + DelRepetitive(exList).Count + DelRepetitive(EndList).Count;

                dr1["MonEc"] = DelRepetitive(MonList).Count;
                dr1["MonEx"] = DelRepetitive(MonExList).Count;
                dr1["MonNum"] = DelRepetitive(MonEndList).Count;
                dr1["MonTotal"] = DelRepetitive(MonList).Count + DelRepetitive(MonExList).Count + DelRepetitive(MonEndList).Count;

                //dr1["TotalNum"] = DelRepetitive(TotalECList).Count;
                //dr1["TotalException"] = DelRepetitive(TotalexList).Count;
                //dr1["TotalEnd"] = DelRepetitive(TotalList).Count;
                dtSum.Rows.Add(dr1);

                dr2["WERKS"] = WerksList[j].ToString() + "EC单数";
                dr2["DayEc"] = DayEc;
                dr2["DayEx"] = DayEx;
                dr2["DayNum"] = DayNum;
                dr2["DayTotal"] = DayEc + DayEx + DayNum;

                dr2["AWeekNum"] = ECNum;
                dr2["AWeekException"] = exNum;
                dr2["AWeekEnd"] = endNum;
                dr2["AWeekTotal"] = ECNum + exNum + endNum;

                dr2["MonEc"] = MonEc;
                dr2["MonEx"] = MonEx;
                dr2["MonNum"] = MonNum;
                dr2["MonTotal"] = MonEc + MonEx + MonNum;

                //dr2["TotalNum"] = ECTotal;
                //dr2["TotalException"] = exTotal;
                //dr2["TotalEnd"] = endTotal;
                
                dtSum.Rows.Add(dr2);
                //dtCarECSource = SortDataTable(dtCarECSource);
            }
        }
        #endregion
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtSum.Clear();
            
        }

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
