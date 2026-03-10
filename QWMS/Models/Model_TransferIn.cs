using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class Model_TransferIn : Form
    {


        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strType = "";

        private string strMblnr = "";
        private DataTable dtSource = new DataTable();
        private ModelsData objModelsData;
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

        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }
        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        #endregion
        public Model_TransferIn(ref UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;


            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
               
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();

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

        #region 設定State Bar中的日期

        private void ShowStatusData()
        {

            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;

        }

        #endregion

        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                Model_OrderSelect objModelOrderSelect = new Model_OrderSelect(UserData, Werks, Lgort, Progid, "MODEL_I");
                objModelOrderSelect.ShowDialog();
                Mblnr = objModelOrderSelect.Mblnr;             
                if (Mblnr != "")
                {
                    txtMblnr.Text = Mblnr.Substring(0, Mblnr.Length - 1);
                }
                else
                {
                    txtMblnr.Text = Mblnr;
                }

                if (!string.IsNullOrEmpty(Mblnr))
                {
                    this.txtMblnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string location = txtLocat.Text.Trim();
                if (string.IsNullOrEmpty(location))
                {
                    stsWarning.Text = "未选择储位";
                    return;
                }
                stsWarning.Text = "";
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] {','});


                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                //單號不為空
                Mblnr = txtMblnr.Text.Trim();
                Mblnr=Mblnr.Replace(";", "','");
                if (string.IsNullOrEmpty(Mblnr))
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }
                objModelsData = new ModelsData(UserData, Werks, Lgort, Progid);
                dtSource = objModelsData.ListOrderData(Mblnr, "", "MODEL_I");


                if (dtSource.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }
                ShowSourceDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        private void ShowSourceDataGrid()
        {

            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcZeile);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "模号";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);



                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);



                //KOSTL
                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "BU";
                dgvcKostl.HeaderText = "PU";
                dgvcKostl.Width = 90;
                dgvcKostl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcKostl);

                //ARBPL
                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ItemName";
                dgvcArbpl.HeaderText = "品名";
                dgvcArbpl.Width = 90;
                dgvcArbpl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 90;
                dgvcMachine.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMachine);

                //Remark
                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "备注";
                dgvcRemark.Width = 90;
                dgvcRemark.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRemark);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "TRNTP";
                dgvcTrntp.Width = 90;
                dgvcTrntp.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTrntp);


                dgvOutSource.DataSource = dtSource;
                lblStorage.Text = dtSource.Rows.Count.ToString() + " records";

                if (dtSource.Rows.Count > 0)
                {
                    this.panel1.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
           #region DoubleClick Locat(儲位)
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
		{
            try
            {
                if (string.IsNullOrEmpty(Type))
                {
                    stsWarning.Text = "请选择入库方式！";
                    return;
                }

                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                Lgort = "";
                }

            if(Werks == "" || Lgort == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				else
				{
					StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
					objStorageIn_LocationSelect.ShowDialog();
					txtLocat.Text = objStorageIn_LocationSelect.Locat;
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
        }
        #endregion

        #region 選擇入庫方式-新板入庫(New Pallet)
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            txtLocat.Text = "";
            btnConfirm.Enabled = true;
        }
        #endregion
        #region 選擇入庫方式-加料入庫(Add In)
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            txtLocat.Text = "";
            btnConfirm.Enabled = true;
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                stsWarning.Text = "";
                string location = txtLocat.Text.Trim();
                Mblnr =Mblnr.Replace("','",";");

                if (string.IsNullOrEmpty(location))
                {
                    stsWarning.Text = "未选择储位";
				    return;
                }
                objModelsData.ModelTransforInByOrder(dtSource, Mblnr, location);
                
                    if (string.IsNullOrEmpty(objModelsData.ERRMSG))
                    {
                        stsWarning.Text = "保存成功！";
                        btnSave.Enabled = false;
                        btnConfirm.Enabled = false;
                    }
                    else
                    {
                        stsWarning.Text = "保存失败！" + objModelsData.ERRMSG;
                        btnSave.Enabled = true ;
                        return;
                    }
            }
            catch (Exception ex)
            {
               stsWarning.Text = ex.Message;
				return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.panel1.Enabled = true;
            this.btnSave.Enabled = false ;
            btnConfirm.Enabled = true;
            txtMblnr.Enabled = true;
            txtMblnr.Text = "";
            txtLocat.Text = "";
            dtSource.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtMblnr_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("双击输入框显示调拨单选择表", txtMblnr);
        }

        private void txtMblnr_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.Show("双击输入框显示调拨单选择表", txtMblnr);
        }

        }

    }
