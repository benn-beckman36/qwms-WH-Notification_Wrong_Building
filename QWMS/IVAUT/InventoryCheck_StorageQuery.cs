using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.Formula.Functions;
using QWMS.Common;
using QCI.QWMS;
using System.Collections;
using System.Linq;

namespace QWMS
{
    public partial class InventoryCheck_StorageQuery : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strRegon = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageData objStorageData;

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

        public string Lgort
        {
            get
            {
                return strLgort;
            }
            set
            {
                strLgort = value;
            }
        }
        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
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

        # endregion

        public InventoryCheck_StorageQuery()
        {
            InitializeComponent();
        }

        public InventoryCheck_StorageQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objCounting = new Counting(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageData = new StorageData(UserData);

                //檢查權限
                if (!objCounting.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlRegon();
                    ShowDdlLgort();
                    ShowDdlInsmk();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        # endregion

        # region ShowDdlRegon
        private void ShowDdlRegon()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbRegon.Items.Clear();
                dtTemp = objAuthority.CheckRegonAuthority();

                cmbRegon.DataSource = dtTemp;
                cmbRegon.DisplayMember = "F_TEXT";
                cmbRegon.ValueMember = "F_VALUE";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlRegon()");
            }
        }
        # endregion

        # region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        # endregion

        # region ShowDdlInsmk
        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();

        }
        # endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 90;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 60;
                dgvData.Columns.Add(dgvcCharg);

                //Config FIFO 
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.CheckStorageInType(strWerks, strLgort, "Config FIFO"))
                {
                    DataGridViewTextBoxColumn dgvcConfig = new DataGridViewTextBoxColumn();
                    dgvcConfig.DataPropertyName = "CONFIG";
                    dgvcConfig.HeaderText = "CFG Name";
                    dgvcConfig.ReadOnly = true;
                    dgvcConfig.Width = 150;
                    dgvData.Columns.Add(dgvcConfig);
                }

                //料號說明欄位
                DataGridViewTextBoxColumn dgvcMaktx = new DataGridViewTextBoxColumn();
                dgvcMaktx.DataPropertyName = "MAKTX";
                dgvcMaktx.HeaderText = "Part# Description";
                dgvcMaktx.Width = 300;
                dgvcMaktx.ReadOnly = true;
                dgvData.Columns.Add(dgvcMaktx);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBkqty = new DataGridViewTextBoxColumn();
                dgvcBkqty.DataPropertyName = "BKQTY";
                dgvcBkqty.HeaderText = "Block Qty";
                dgvcBkqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcBkqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 90;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvData.Columns.Add(dgvcIndat);

                if (chkIsDateCode.Checked == true)
                {
                    //Vendor Manufacture Date
                    DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                    dgvcVedat.DataPropertyName = "VEDAT";
                    dgvcVedat.HeaderText = "Date Code";
                    dgvcVedat.ReadOnly = true;
                    dgvcVedat.Width = 120;
                    dgvData.Columns.Add(dgvcVedat);
                }

                DataGridViewTextBoxColumn dgvcDic = new DataGridViewTextBoxColumn();
                dgvcDic.DataPropertyName = "DECITEM";
                dgvcDic.HeaderText = "Decitem";
                dgvcDic.ReadOnly = true;
                dgvcDic.Width = 140;
                dgvData.Columns.Add(dgvcDic);

                if (chkIsCombine.Checked == false)
                {
                    DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                    dgvcLifnr.DataPropertyName = "LIFNR";
                    dgvcLifnr.HeaderText = "Vendor";
                    dgvcLifnr.ReadOnly = true;
                    dgvData.Columns.Add(dgvcLifnr);
                }

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No.";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 110;
                dgvData.Columns.Add(dgvcEbeln);

                if (chkIsDateCode.Checked == true)
                {

                    //Inspt Lot No.
                    DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                    dgvcInspt.DataPropertyName = "INSPT";
                    dgvcInspt.HeaderText = "Inspt Lot No.";
                    dgvcInspt.ReadOnly = true;
                    dgvcInspt.Width = 100;
                    dgvData.Columns.Add(dgvcInspt);

                    //DateCode
                    DataGridViewTextBoxColumn dgvcDateCode = new DataGridViewTextBoxColumn();
                    dgvcDateCode.DataPropertyName = "DACOD";
                    dgvcDateCode.HeaderText = "Remark(Vendor Date Code)";
                    dgvcDateCode.ReadOnly = true;
                    dgvcDateCode.Width = 90;
                    dgvData.Columns.Add(dgvcDateCode);

                    //LockCode
                    DataGridViewTextBoxColumn dgvcLockCode = new DataGridViewTextBoxColumn();
                    dgvcLockCode.DataPropertyName = "LOCOD";
                    dgvcLockCode.HeaderText = "Lot Code";
                    dgvcLockCode.ReadOnly = true;
                    dgvcLockCode.Width = 90;
                    dgvData.Columns.Add(dgvcLockCode);
                }

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Width = 90;
                dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 140;
                dgvData.Columns.Add(dgvcRmano);

                if (chkIsCombine.Checked == false)
                {
                    //序號欄位
                    DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                    dgvcSerno.DataPropertyName = "SERNO";
                    dgvcSerno.HeaderText = "Serial No.";
                    dgvcSerno.ReadOnly = true;
                    dgvcSerno.Width = 90;
                    dgvData.Columns.Add(dgvcSerno);

                    //BoxID欄位
                    DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                    dgvcBoxid.DataPropertyName = "BOXID";
                    dgvcBoxid.HeaderText = "BoxID.";
                    dgvcBoxid.ReadOnly = true;
                    dgvcBoxid.Width = 90;
                    dgvData.Columns.Add(dgvcBoxid);

                }

                if (chkIsCombine.Checked == false)
                {
                    DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                    dgvcMrgid.DataPropertyName = "MRGID";
                    dgvcMrgid.HeaderText = "Mixed Material ID";
                    dgvcMrgid.ReadOnly = true;
                    dgvcMrgid.Width = 120;
                    dgvData.Columns.Add(dgvcMrgid);
                }
                //退料时间
                if (Werks=="CS31" && (Lgort.Substring(0,2)=="TW" || Lgort.Substring(0, 2) == "TC"))
                {
                    DataGridViewTextBoxColumn dgvcMcdat = new DataGridViewTextBoxColumn();
                    dgvcMcdat.DataPropertyName = "MCDAT";
                    dgvcMcdat.HeaderText = "MC Date";
                    dgvcMcdat.ReadOnly = true;
                    dgvcMcdat.Width = 140;
                    dgvData.Columns.Add(dgvcMcdat);
                }

                //PKDAT: 棧板滿板時間
                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "Pallet Time";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 140;
                dgvData.Columns.Add(dgvcPkdat);
              
                if (chkIsCombine.Checked == false)
                {
                    if (objStorageData.CheckStorageInType(strWerks, strLgort, "MQC PE"))
                    {
                    }
                    else
                    {
                        DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                        dgvcRmak1.DataPropertyName = "RMAK1";
                        dgvcRmak1.HeaderText = "Remark";
                        dgvcRmak1.Width = 110;
                        dgvcRmak1.ReadOnly = true;
                        dgvData.Columns.Add(dgvcRmak1);
                    }
                }
                if (objStorageData.CheckStorageInType(strWerks, strLgort, "MQC PE"))
                {
                    DataGridViewTextBoxColumn dgvcMQCID = new DataGridViewTextBoxColumn();
                    dgvcMQCID.DataPropertyName = "MQCID";
                    dgvcMQCID.HeaderText = "MQCID";
                    if (strWerks == "CS20" && (strLgort == "TWFG" ))
                    {
                        dgvcMQCID.HeaderText = "MEMPF";
                    }
                    else if (strWerks == "CS20" && (strLgort == "TWDA"))
                    {
                        dgvcMQCID.HeaderText = "MRP BU";
                    }
                    dgvcMQCID.Width = 70;
                    dgvcMQCID.ReadOnly = true;
                    dgvData.Columns.Add(dgvcMQCID);

                    DataGridViewTextBoxColumn dgvcMQCNM = new DataGridViewTextBoxColumn();
                    dgvcMQCNM.DataPropertyName = "MQCNM";
                    dgvcMQCNM.HeaderText = "MQCNM";
                    if (strWerks == "CS20" && (strLgort == "TWFG"))
                    {
                        dgvcMQCNM.HeaderText = "PUGRP";
                    }
                    else if (strWerks == "CS20" && (strLgort == "TWDA"))
                    {
                        dgvcMQCNM.HeaderText = "MEMPF";
                    }
                    dgvcMQCNM.Width = 90;
                    dgvcMQCNM.ReadOnly = true;
                    dgvData.Columns.Add(dgvcMQCNM);

                    DataGridViewTextBoxColumn dgvcPEID = new DataGridViewTextBoxColumn();
                    dgvcPEID.DataPropertyName = "PEID";
                    dgvcPEID.HeaderText = "PEID";
                    if (strWerks == "CS20" && (strLgort == "TWFG"))
                    {
                        dgvcPEID.HeaderText = "EKNAM";
                    }
                    else if (strWerks == "CS20" && (strLgort == "TWDA"))
                    {
                        dgvcPEID.HeaderText = "PUGRP";
                    }
                    dgvcPEID.Width = 70;
                    dgvcPEID.ReadOnly = true;
                    dgvData.Columns.Add(dgvcPEID);

                    DataGridViewTextBoxColumn dgvcPENM = new DataGridViewTextBoxColumn();
                    dgvcPENM.DataPropertyName = "PENM";
                    dgvcPENM.HeaderText = "PENM";
                    if (strWerks == "CS20" && (strLgort == "TWFG"))
                    {
                        dgvcPENM.HeaderText = "PUGRP BU";
                    }
                    else if (strWerks == "CS20" && (strLgort == "TWDA"))
                    {
                        dgvcPENM.HeaderText = "PUGRP BU";
                    }
                    dgvcPENM.Width = 90;
                    dgvcPENM.ReadOnly = true;
                    dgvData.Columns.Add(dgvcPENM);

                }
                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        #region ShowPartNoLocationDataGrid
        private void ShowPartNoLocationDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "廠區";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "倉別";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "儲位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 90;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料號";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcModel = new DataGridViewTextBoxColumn();
                dgvcModel.DataPropertyName = "MODEL";
                dgvcModel.HeaderText = "機種";
                dgvcModel.ReadOnly = true;
                dgvcModel.Width = 60;
                dgvData.Columns.Add(dgvcModel);

                DataGridViewTextBoxColumn dgvcRegion = new DataGridViewTextBoxColumn();
                dgvcRegion.DataPropertyName = "REGON";
                dgvcRegion.HeaderText = "洲別";
                dgvcRegion.ReadOnly = true;
                dgvcRegion.Width = 60;
                dgvData.Columns.Add(dgvcRegion);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 60;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcPalqty = new DataGridViewTextBoxColumn();
                dgvcPalqty.DataPropertyName = "PALQTY";
                dgvcPalqty.HeaderText = "滿板數量";
                dgvcPalqty.ReadOnly = true;
                dgvcPalqty.Width = 60;
                dgvData.Columns.Add(dgvcPalqty);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "庫存數量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBalance = new DataGridViewTextBoxColumn();
                dgvcBalance.DataPropertyName = "MENGE";
                dgvcBalance.HeaderText = "未滿板數量";
                dgvcBalance.ReadOnly = true;
                dgvcBalance.Width = 60;
                dgvData.Columns.Add(dgvcBalance);

                dgvData.DataSource = dtInventory;
                lblCount.Text = dtInventory.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            string strStartDate = "";
            string strEndDate = "";
            string strInsmk = "";
            string strIsmrg = "N";
            string strIsCombine = "N";
            string strCombineParts = "N";
            string str24H = "N";
            stsWarning.Text = "";

            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                if (cmbRegon.SelectedIndex != -1)
                {
                    strRegon = cmbRegon.SelectedValue.ToString();
                }
                if (chkIsmrg.Checked)
                {
                    strIsmrg = "Y";
                }
                if (chkIsCombine.Checked)
                {
                    strIsCombine = "Y";
                }
                if (chkCombineParts.Checked)
                {
                    strCombineParts = "Y";
                }
                if (chk24H.Checked)
                {
                    str24H = "Y";
                }

                if (chkDate.Checked)
                {
                    strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                    strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
                }
                else
                {
                    strStartDate = "";
                    strEndDate = "";
                }

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                /*
                if (chk1.Checked || chk2.Checked)//有開啟抽盤功能
                {
                    if (N2.Value == null)
                    {
                        stsWarning.Text = "Sample Rate can't be empty!!";
                        return;
                    }
                    else
                    {
                        if (N2.Value.ToString().Trim() == "")
                        {
                            stsWarning.Text = "Sample Rate can't be empty!!";
                            return;
                        }
                    }

                }
                */


                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                    CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);

                bool blMqcPe = objStorageData.CheckStorageInType(strWerks, strLgort, "MQC PE");

                if (chkIsDateCode.Checked == true)
                {
                    dtData = objCounting.QueryDetailCountingData(strInsmk, txtStartLocat.Text.Trim(),
                    txtEndLocat.Text.Trim(), txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), strStartDate,
                    strEndDate, txtCharg.Text.Trim(), txtMblnr.Text.Trim(), strIsmrg, strIsCombine, strRegon,
                    txtVendorCode.Text.ToString().Trim(), strCombineParts, txtRmano.Text.Trim(), str24H, true, txtDecitem.Text.Trim(), blMqcPe, txtConfig.Text.Trim());
                }
                else
                {
                    dtData = objCounting.QueryDetailCountingData(strInsmk, txtStartLocat.Text.Trim(),
                    txtEndLocat.Text.Trim(), txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), strStartDate,
                    strEndDate, txtCharg.Text.Trim(), txtMblnr.Text.Trim(), strIsmrg, strIsCombine, strRegon,
                    txtVendorCode.Text.ToString().Trim(), strCombineParts, txtRmano.Text.Trim(), str24H, false, txtDecitem.Text.Trim(), blMqcPe, txtConfig.Text.Trim());

                }

                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    //不顯示待出貨儲位的資料
                    if (chkNloca.Checked == true)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dtData.Rows[i]["LOCAT"].ToString() == "" ||
                                dtData.Rows[i]["LOCAT"].ToString().Substring(0, 1) == "N")
                            {
                                dtData.Rows[i].Delete();
                            }
                        }
                        dtData.AcceptChanges();
                    }

                    #region 抽盤功能

                    double count_Total = 0;
                    double count_Sample = 0;
                    double rate = (double)N2.Value / 100; //抽盤比例

                    DataTable dtData_temp = dtData.Clone();
                    dtData_temp.Rows.Clear();

                    DataTable dtData_temp2 = new DataTable();
                    ArrayList a2 = new ArrayList();

                    if (chk1.Checked) //依筆數抽盤
                    {
                        count_Total = dtData.Rows.Count; //資料筆數
                        count_Sample = System.Math.Round(count_Total * rate, MidpointRounding.AwayFromZero); //抽盤筆數
                        Array a1 = RandomNO(Convert.ToInt32(count_Sample), Convert.ToInt32(count_Total));

                        for (int i = 0; i < a1.Length; i++)
                        {
                            int NO = Convert.ToInt32(a1.GetValue(i).ToString());
                            dtData_temp.ImportRow(dtData.Rows[NO - 1]);
                        }

                        if (dtData_temp.Rows.Count != 0)
                        {
                            //將datatable依location做排序
                            IEnumerable<DataRow> results = (from row in dtData_temp.AsEnumerable()
                                                            orderby row["LOCAT"], row["MATNR"]
                                                            select row);

                            dtData_temp = results.CopyToDataTable<DataRow>();
                            dtData.Rows.Clear();
                            dtData = dtData_temp;
                        }
                        else
                        {
                            dtData.Rows.Clear();
                        }
                    }

                    if (chk2.Checked) //依料號抽盤
                    {
                        //取出所包含料號
                        var distinctRows = (from DataRow dRow in dtData.Rows
                                            select new { col1 = dRow["MATNR"] }).Distinct();
                        foreach (var row in distinctRows)
                            a2.Add(row.col1.ToString());

                        count_Total = a2.Count; //料號總數
                        count_Sample = System.Math.Round(count_Total * rate, MidpointRounding.AwayFromZero);
                        Array a1 = RandomNO(Convert.ToInt32(count_Sample), Convert.ToInt32(count_Total));

                        for (int i = 0; i < a1.Length; i++)
                        {
                            int NO = Convert.ToInt32(a1.GetValue(i).ToString());
                            string MATNR = a2[NO - 1].ToString();

                            /*
                            var results = from myRow in dtData.AsEnumerable()
                                          where myRow.Field<int>("RowNo") == 1
                                          select myRow;
                            */

                            IEnumerable<DataRow> query = from row in dtData.AsEnumerable()
                                                         where row.Field<string>("MATNR") == MATNR
                                                         select row;
                            dtData_temp2 = query.CopyToDataTable<DataRow>();
                            dtData_temp.Merge(dtData_temp2);

                        }


                        if (dtData_temp.Rows.Count != 0)
                        {
                            //將datatable依location做排序
                            IEnumerable<DataRow> results = (from row in dtData_temp.AsEnumerable()
                                                            orderby row["LOCAT"], row["MATNR"]
                                                            select row);

                            dtData_temp = results.CopyToDataTable<DataRow>();
                            dtData.Rows.Clear();
                            dtData = dtData_temp;
                        }
                        else
                        {
                            dtData.Rows.Clear();
                        }
                    }

                    #endregion

                    btnPrint.Enabled = true;
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
            finally
            {
                btnConfirm.Enabled = true;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                this.cmbInsmk.SelectedIndex = -1;
                this.txtStartLocat.Text = "";
                this.txtEndLocat.Text = "";
                this.txtStartMatnr.Text = "";
                this.txtEndMatnr.Text = "";
                this.txtCharg.Text = "";
                this.txtMblnr.Text = "";
                this.txtVendorCode.Text = "";
                this.dtpStartDate.Value = DateTime.Now;
                this.dtpEndDate.Value = DateTime.Now;
                this.btnPrint.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.chkIsCombine.Checked = false;
                this.chkIsmrg.Checked = false;
                this.chkCombineParts.Checked = false;
                this.lblCount.Text = "";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Resize
        private void InventoryCheck_StorageQuery_Resize(object sender, EventArgs e)
        {
            //panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
        }
        # endregion

        # region Download
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        # endregion

        # region Export
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (this.chkIsCombine.Checked)
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tDecItem\tPart No Description\tCUST Mat\tStock\tVersion\tQty\tDocument No\tStore In Date\tRMA No.";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["DECITEM"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MAKTX"].ToString() + "\t";
                        strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["RMANO"].ToString() + "\t";
                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tDecItem\tPart No Description\tCUST Mat\tStock\tVersion\tSerial No\tQty\tDocument No\tVendor\tMixed Material ID\tStore In Date\tRemark\tDateCode\tVendor Manufacture Date\tInspt Lot No.\tLockCode\tRMA No.";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["DECITEM"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MAKTX"].ToString() + "\t";
                        strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtData.Rows[i]["SERNO"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MRGID"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["RMAK1"].ToString() + "\t";
                        strLine += dtData.Rows[i]["DACOD"].ToString() + "\t";
                        strLine += dtData.Rows[i]["VEDAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSPT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCOD"].ToString() + "\t";
                        strLine += dtData.Rows[i]["RMANO"].ToString();
                        sw.WriteLine(strLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }
        # endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //string strLocat = "";
                DataTable dtTmpPrint = new DataTable();
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }

                //Order by
                string strOrderBy = "WERKS, LGORT, LOCAT";
                dtData = CommonInfo.SortDataTable(dtData, strOrderBy);
                if (chkSplitPrint.Checked == true)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "DETAILCOUNT", dtData);//分储打印
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                else
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "DETAILCOUNTALL", dtData);//打印所有
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                //无纸化需求导入盘点明细
                //if (UserData.CompanyCode == "9200")
                //{
                //    //objCounting.AddStoragePad(dtData);
                //}
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        #region btnLabelPrint_Click
        private void btnLabelPrint_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Label to print!!";
                    return;
                }
                ReportPrint objReportPrint = new ReportPrint(UserData, "COUNTINGLABELLOCAT", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 抽盤

        //隨機產生不重複編號
        private Array RandomNO(int count_Sample, int count_Total)
        {
            //參考:http://www.dotblogs.com.tw/ouch1978/archive/2011/10/14/sl-random-with-linq.aspx
            var result = Enumerable.Range(1, count_Total).OrderBy(n => n * n * (new Random()).Next()).Take(count_Sample);
            return result.ToArray();
        }

        //筆數
        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                chk2.Checked = false;
            }
        }

        //料號
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked)
            {
                chk1.Checked = false;
            }
        }

        protected void RecoverNum(object s, EventArgs e)
        {
            var n = (NumericUpDown)s;
            if (n.Text == "") n.Text = n.Value.ToString();
        }

        #endregion

        #region 查詢可併板的儲位明細
        private void btnCombineQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                dtInventory = objPlantData.GetPartNoLocationInventoryData(Werks, Lgort);
                dtInventory = CommonInfo.SortDataTable(dtInventory, "MODEL,MATNR,CHARG,LOCAT ASC");
                if (dtInventory.Rows.Count > 0)
                {
                    //for (int i = 0; i < dtInventory.Rows.Count; i++)
                    //{
                    //    dtLocat = objPlantData.GetLocationData(Werks, Lgort, dtInventory.Rows[i]["LOCAT"].ToString().Trim(), dtInventory.Rows[i]["MATNR"].ToString().Trim(), dtInventory.Rows[i]["CHARG"].ToString().Trim(), "");
                    //}
                    //dtData = CommonInfo.SortDataTable(dtLocat, "MODEL,MATNR,CHARG,LOCAT ASC");
                    ShowPartNoLocationDataGrid();
                }
                else
                {
                    ShowPartNoLocationDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion


        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Location = dgvData.CurrentRow.Cells[2].Value.ToString().Trim();
            string Mandt = "218";
            string Werks = dgvData.CurrentRow.Cells[0].Value.ToString().Trim();
            string Lgort = dgvData.CurrentRow.Cells[1].Value.ToString().Trim();
            string Boxid = dgvData.CurrentRow.Cells[11].Value.ToString().Trim();
            string SN = "";// dgvData.CurrentRow.Cells[10].Value.ToString().Trim();
            string Qty = dgvData.CurrentRow.Cells[10].Value.ToString().Trim();
            string Insmk = dgvData.CurrentRow.Cells[7].Value.ToString().Trim();
            string Kdmat = dgvData.CurrentRow.Cells[6].Value.ToString().Trim();
            string Matno = dgvData.CurrentRow.Cells[3].Value.ToString().Trim();
            string Charg = dgvData.CurrentRow.Cells[8].Value.ToString().Trim();
            if (chkIsCombine.Checked)
            {
                //Manage_BoxSn fm = new Manage_BoxSn(UserData, strProgid, Location, Mandt, Werks, Lgort, Boxid, SN, Qty, Insmk, Matno, Kdmat,Charg);
                //fm.ShowDialog();
                //if (fm.Flage)
                //{
                //    btnConfirm_Click(null, null);
                //}
            }
        }
        //料号扫描
        private void txtStartMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                if (txtStartMatnr.Text.ToString().Trim().Length <13)
                {
                    MessageBox.Show ("料号信息不正确！");
                    return;
                }
                txtStartMatnr.Text = txtStartMatnr.Text.ToString().Substring(0,11);
            }
        }

        private void txtEndMatnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtEndMatnr.Text.ToString().Trim().Length < 13)
                {
                    MessageBox.Show("料号信息不正确！");
                    return;
                }
                string strLotCode = txtEndMatnr.Text.ToString().ToUpper().Replace("；", ";");
                string[] str = strLotCode.Split(';');
                txtEndMatnr.Text = str[0].ToString().Trim();
               // txtEndMatnr.Text = txtEndMatnr.Text.ToString().Substring(0, 11);
            }
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            strLgort = cmbLgort.Text;
            //判断是否为Config FIFO
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.CheckStorageInType(strWerks, strLgort, "Config FIFO"))
            {
                label18.Visible = true;
                txtConfig.Visible = true;
                txtConfig.Enabled = true;
            }
            else
            {
                label18.Visible = false;
                txtConfig.Visible = false;
                txtConfig.Enabled = false;
            }
        }



        //private void dgvData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    string Location = dgvData.CurrentRow.Cells[2].Value.ToString().Trim();
        //    string Mandt = "218";
        //    string Werks = dgvData.CurrentRow.Cells[0].Value.ToString().Trim();
        //    string Lgort = dgvData.CurrentRow.Cells[1].Value.ToString().Trim();
        //    Mange_BoxSn fm = new Mange_BoxSn(UserData, strProgid, Location, Mandt, Werks, Lgort);
        //    fm.ShowDialog();
        //    if (fm.Flage)
        //    {
        //        btnConfirm_Click(null, null);
        //    }
        //}
    }
}
