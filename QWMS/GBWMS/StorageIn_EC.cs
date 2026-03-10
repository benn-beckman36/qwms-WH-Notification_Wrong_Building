using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
namespace QWMS
{
    public partial class StorageIn_EC : Form
    {
        //public StorageIn_EC()
        //{
        //    InitializeComponent();
        //}

        #region DataMember

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;

        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtECSource = new DataTable();
        private DataTable dtStorageLocation = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtBox = new DataTable();

        #endregion

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

        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
            }
        }

        #endregion

        #region Constructor

        public StorageIn_EC(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            lblCompany.Text = Comcd;
            lblUserid.Text = Usrnm;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority())
                //if (false)
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    // ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    //ShowPrintCheckBox();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }

                    // ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                // stsWarning.Text = string.Empty;
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
                    strLgort = string.Empty;
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
        #endregion

        private void StorageIn_EC_Resize(object sender, EventArgs e)
        {
            //splitContainer2.Panel1.Size = new System.Drawing.Size((int)(this.Size.Width * 0.6), 80);
            //if (splitContainer2.Panel1.Height < 80)
            //{
            //    splitContainer2.SplitterDistance = 80;
            //}
        }

        private void StorageIn_EC_Load(object sender, EventArgs e)
        {


        }

        private void txtEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

                DataTable dtstatus = new DataTable();
                dtstatus = StorageIn.GetPacingStatus(txtEC.Text.Trim());
                if (dtstatus.Rows.Count > 0)
                {
                    if (dtstatus.Rows[0]["status"].ToString().Trim() == "Y")
                    {
                        MessageBox.Show("已经完成入库，不能重新作业！");
                        txtEC.Text = "";
                        return;
                    }
                }

                //dtECSource = StorageIn.GetECInfo(cmbWerks.Text.ToString(), txtEC.Text.Trim());
                if (!StorageIn.GetECFromQEC(txtEC.Text.Trim()))
                {
                    txtEC.Text = string.Empty;
                    MessageBox.Show("EC单不存在，请确认！");
                    return;
                }
                dtECSource = StorageIn.GetECHead(cmbWerks.Text.ToString(), txtEC.Text.Trim());

                ShowStorageDataGrid();
                if (dtECSource.Rows.Count > 0)
                {
                    if (dtECSource.Rows[0]["lgort"].ToString() != "")
                    {
                        cmbLgort.Text = dtECSource.Rows[0]["lgort"].ToString();
                    }
                    lblCustomno.Text = dtECSource.Rows[0]["BKTXT1"].ToString();
                    lblType.Text = dtECSource.Rows[0]["CLRNAM"].ToString();
                }
                else
                {
                    MessageBox.Show("EC单不存在，请确认！");
                    return;
                }

            }
        }

        #region ShowStorageDataGrid
        private void ShowStorageDataGrid()
        {
            dgvEC.AutoGenerateColumns = false;
            dgvEC.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcPITEM = new DataGridViewTextBoxColumn();
                dgvcPITEM.DataPropertyName = "PITEM";
                dgvcPITEM.HeaderText = "序号";
                dgvcPITEM.Width = 130;
                dgvcPITEM.ReadOnly = true;
                dgvEC.Columns.Add(dgvcPITEM);

                //BOXID
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvEC.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG1 = new DataGridViewTextBoxColumn();
                dgvcCHARG1.DataPropertyName = "CHARG1";
                dgvcCHARG1.HeaderText = "版本";
                dgvcCHARG1.Width = 90;
                dgvcCHARG1.ReadOnly = true;
                dgvEC.Columns.Add(dgvcCHARG1);

                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                dgvEC.Columns.Add(dgvcMENGE);

                dgvEC.DataSource = dtECSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowBoxDataGrid

        private void ShowBoxDataGrid()
        {
            dvBox.AutoGenerateColumns = false;
            dvBox.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvBoxid = new DataGridViewTextBoxColumn();
                dgvBoxid.DataPropertyName = "BOXID";
                dgvBoxid.HeaderText = "Boxid";
                dgvBoxid.Width = 130;
                dgvBoxid.ReadOnly = true;
                dvBox.Columns.Add(dgvBoxid);

                //BOXID
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dvBox.Columns.Add(dgvcMATNR);


                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                dvBox.Columns.Add(dgvcMENGE);

                dvBox.DataSource = dtBox;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        private void txtBoxID_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (txtBoxID.Text.Trim().Split(';').Length > 5)
                {
                    QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                    dtBox = StorageIn.ScanBoxid(txtEC.Text.Trim(), txtBoxID.Text.Trim());
                    txtBoxID.Text = "";
                    lblboxqty.Text = dtBox.Rows.Count.ToString() + "箱";
                }
                else
                {
                    MessageBox.Show("二维码识别异常！");
                    txtBoxID.Text = "";
                    return;
                }
                ShowBoxDataGrid();



            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLocat.Text.Trim() == "")
            {
                MessageBox.Show("储位不能为空！");
                return;
            }
            if (txtPallet.Text.Trim() == "")
            {
                MessageBox.Show("栈板\\箱不能为空！");
                return;
            }
            if (txtStaffNo.Text.Trim() == "")
            {
                MessageBox.Show("工号不能为空！");
                return;
            }
            if (lblCustomno.Text.Trim() == "逐票报关" && lblType.Text.Trim() == "")
            {
                MessageBox.Show("逐票报关未报关完成，不能扣帐！");
                return;
            }
            QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
            if (dvBox.Rows.Count > 0)//刷boxid
            {

                if (StorageIn.CheckBoxQty(txtEC.Text.Trim()).Rows.Count > 0)
                {
                    MessageBox.Show("EC数量与扫描Box数量不一致，请检查！");
                    return;
                }
                else
                {
                    StorageIn.UpdateStorageinWay(txtEC.Text.Trim(), "Y");
                }

            }
            else
            {
                StorageIn.UpdateStorageinWay(txtEC.Text.Trim(), "N");
            }

            DataSet ds = new DataSet();
            //ds = StorageIn.SAPDataInfo(txtEC.Text.Trim(), cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), txtLocat.Text.Trim() + " " + UserData.UserId.ToString() + " " + txtStaffNo.Text.Trim(), txtPallet.Text.Trim());
            DataTable dthead = StorageIn.GetTAB_ZM000(txtEC.Text.Trim(), cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), txtLocat.Text.Trim() + " " + txtStaffNo.Text.Trim() + " " + txtPallet.Text.Trim(), txtPallet.Text.Trim());
            DataTable dtitem = StorageIn.GetTAB_ZM001(txtEC.Text.Trim(), cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), txtLocat.Text.Trim() + " " + " " + txtStaffNo.Text.Trim() + " " + txtPallet.Text.Trim(), txtPallet.Text.Trim());
            dthead.TableName = "TAB_ZM000";
            dtitem.TableName = "TAB_ZM001";
            ds.Tables.Add(dthead.Copy());
            ds.Tables.Add(dtitem.Copy());

            ArrayList sqlarr = new ArrayList();
            DataSet dsreturn = new DataSet();

            MM.MM_Service objMM = new QWMS.MM.MM_Service();
            dsreturn = objMM.Z_RFC_PACKING_POST(txtStaffNo.Text.Trim(), txtEC.Text.Trim(), "SDS_PDA", "", ds);

            DataTable dtTAB_ZM025 = dsreturn.Tables["TAB_ZM025"];
            if (dtTAB_ZM025.Rows.Count > 0)
            {
                if (dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() != "")
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat("UPDATE EC_HEAD SET STATUS='Y' WHERE PNUM='{0}'", txtEC.Text.Trim());
                    sqlarr.Add(strsql);
                }
                for (int i = 0; i < dtTAB_ZM025.Rows.Count; i++)
                {
                    if (dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim() != "")
                    {
                        StringBuilder strsql = new StringBuilder();
                        strsql.AppendFormat("UPDATE EC_ITEM SET WERKS='{0}',LGORT='{1}',SGTXT='{2}',GDREC='{3}',BKTXT='{4}',GJAHR='{5}',BELNR='{6}',BUZEI='{7}',BUDAT='{8}',UNAME='{9}',MESSAGE=N'{10}' WHERE PNUM='{11}' AND PITEM='{12}'",
                            dtTAB_ZM025.Rows[i]["WERKS"].ToString().Trim(), dtTAB_ZM025.Rows[i]["LGORT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["SGTXT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["GDREC"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BKTXT"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["MJAHR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["ZEILE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BUDAT"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["UNAME"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MESSAGE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["PNUM"].ToString().Trim(),
                            dtTAB_ZM025.Rows[i]["PITEM"].ToString().Trim());
                        sqlarr.Add(strsql);
                    }
                }
            }
            if (sqlarr.Count > 0)
            {
                if (StorageIn.updateZM025(sqlarr))
                {
                    MessageBox.Show("扣账成功，扣账编号为：" + dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() + "！");
                }
            }
            else
            {
                MessageBox.Show("扣账失败，失败原因为：" + dtTAB_ZM025.Rows[0]["MESSAGE"].ToString().Trim() + "！");
            }
            //MessageBox.Show("比对成功，待与SAP对接！");
            return;


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEC.Text.Trim() != "")
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                dtBox = StorageIn.ReScanBoxid(txtEC.Text.Trim());
                txtBoxID.Text = "";
                lblboxqty.Text = dtBox.Rows.Count.ToString() + "箱";
            }
            else
            {
                MessageBox.Show("请先扫EC单！");
                txtBoxID.Text = "";
                return;
            }
            ShowBoxDataGrid();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEC.Text = "";
            txtLocat.Text = "";
            txtPallet.Text = "";
            txtStaffNo.Text = "";

            dtBox = null;
            dtECSource = null;
            dvBox.DataSource = null;
            dgvEC.DataSource = null;

            StorageIn_QueryEC ec = new StorageIn_QueryEC(UserData, "B07");
            ec.Show();


        }


    }
}
