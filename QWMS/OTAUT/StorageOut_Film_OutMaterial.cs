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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace QWMS
{
    public partial class StorageOut_Film_OutMaterial : Form
    {

        #region DataMember

        #region 變數宣告
        public string strMandt = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strProgid = "";
        public string strMatnr = "";
        public string strMblnr = "";
        public string strType = "";
        public string strInsmk = "";
        public string strSttyp = "";
        public string strLotyp = "";
        public string strComcd = "";
        UserInfo UserData = new UserInfo();
        ArrayList aryMblnr = new ArrayList();
        ArrayList aryMatnr = new ArrayList();
        private QCI.QWMS.LogData objLogData;

        //出库信息
        DataTable dtStorage = new DataTable();
        //Sap单据信息
        DataTable dtData = new DataTable();
        //合并出库记录
        DataTable dtCombineStorage = new DataTable();
        #endregion

        #region 变量
        public ArrayList Mblnrs
        {
            get
            {
                return aryMblnr;
            }
            set
            {
                aryMblnr = value;
            }
        }

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

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
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

        public ArrayList Matnrs
        {
            get
            {
                return aryMatnr;
            }
            set
            {
                aryMatnr = value;
            }
        }

        #endregion

        //构造函数
        public StorageOut_Film_OutMaterial(UserInfo _UserData, string strProgid)
        {
            InitializeComponent();
            UserData = _UserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);

                //检查权限
                if (!StorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //Status
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

                    //控件初始化
                    txtBoxID.Enabled = false;
                    btnQuery.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region MemberFunction

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }

        //厂区
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        //仓别
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        //出库信息
        public void ShowDataGrid()
        {
            this.dgvBox.AutoGenerateColumns = false;
            this.dgvBox.Columns.Clear();
            this.dgvBox.ColumnHeadersHeight = 30;
            try
            {
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.Width = 60;
                dgvcChecked.ReadOnly = true;
                this.dgvBox.Columns.Add(dgvcChecked);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 120;
                dgvcLocat.ReadOnly = true;
                this.dgvBox.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvBox.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.Width = 120;
                boxidStyle.ReadOnly = true;
                this.dgvBox.Columns.Add(boxidStyle);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "BOX Qty(G)";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                dgvBox.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                dgvBox.Columns.Add(dgvcAlqty);

                //BLACE
                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Blance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvBox.Columns.Add(dgvcBlace);

                //SQTY
                DataGridViewTextBoxColumn dgvcSQty = new DataGridViewTextBoxColumn();
                dgvcSQty.DataPropertyName = "SQTY";
                dgvcSQty.HeaderText = "BOX Qty(S)";
                dgvcSQty.Width = 80;
                dgvcSQty.ReadOnly = true;
                dgvBox.Columns.Add(dgvcSQty);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvBox.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.Width = 150;
                dgvcrmak1.ReadOnly = true;
                this.dgvBox.Columns.Add(dgvcrmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.Width = 90;
                dgvcIndat.ReadOnly = true;
                this.dgvBox.Columns.Add(dgvcIndat);

                dgvBox.DataSource = dtStorage;
                lblBoxCount.Text = dtStorage.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        //Sap信息
        public void ShoOutSourceDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcBwart = new DataGridViewTextBoxColumn();
                dgvcBwart.DataPropertyName = "BWART";
                dgvcBwart.HeaderText = "Type";
                dgvcBwart.Width = 90;
                dgvcBwart.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBwart);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEbeln);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcAlqty);


                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowInSourceDataGrid()");
            }
        }

        //拼接所选GR单号
        private string GetMblnrData()
        {
            try
            {
                StringBuilder sbMblnr = new StringBuilder();
                sbMblnr.Remove(0, sbMblnr.Length);
                for (int i = 0; i < Mblnrs.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(",");
                    sbMblnr.Append(Mblnrs[i].ToString().Trim());
                }
                return sbMblnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }

        //Send Mail 
        private bool SendMail()
        {
            bool blResult = false;
            try
            {
                //MailService.SendMailService objSendMail = new MailService.SendMailService();
                ClaHttpHelper clahttpHelper = new ClaHttpHelper();
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                string strUrl = objStorageData.GetSendMailUrl();
                string strMailPSD = "975A8056C8DF50786FB680A96E0CCCBF";
                string strMailFrom = "Web_Notice@quantacn.com";
                string strMailTo = string.Empty;
                string strMailSubject = string.Empty;

                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                strMailTo = objPlantData.GetMailConfig_Film();
                if (string.IsNullOrEmpty(strMailTo))
                {
                    stsWarning.Text = "请先配置邮件相关人员";
                    return blResult;
                }

                if (Comcd == "2280")
                {
                    //strMailTo = "Zaixin.wang@quantacn.com;Mars.Guo@qm-technology.com;Lili.Liu@qm-technology.com;WH@qm-technology.com;Jinfeng.Wang@qm-technology.com;Andy.Tian@qm-technology.com;Mild.Wu@qm-tech.co;Material.team@qm-technology.com;Jeff.Huang@qm-technology.com;fevin.wang@qm-technology.com;eric.chan@qm-technology.com;Steven.Wu@qm-technology.com";
                    strMailSubject = "Film出库Returns信息";
                }
                else
                {
                    //strMailTo = "Sam.Zhang@quantacn.com;Juju.yang@quantacn.com;Bob.Bo2@quantacn.com;Terry.Cao@quantacn.com;Paul.Wang2@quantacn.com;L.F.Che@quantacn.com;Vicky.Shi@quantacn.com;WHKeyinTGC@quantacn.com;ReceivingKeyinTGC@quantacn.com;Judy.Tang@quantacn.com;Hank.Zheng@quantacn.com;MLMCMaterialCenter@quantacn.com;Qbus-Areceive.Team@quantacn.com; RTK.Shipment@quantacn.com;Ada.Li@quantacn.com";
                    strMailSubject = "Film出库Returns信息";
                }
                string strMailCC = "";
                string strMges = "";

                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<HTML><BODY leftmargin=1 topmargin=1><CENTER>");
                sbMailContent.Append("<FONT style='COLOR: #333333; FONT-FAMILY: &quot;宋体&quot;; FONT-SIZE: 9pt; position: relative; filter: blur(add=1, direction=45, strength=3)'>Film出库Returns详细信息</FONT></a>");
                sbMailContent.Append("</CENTER><HR SIZE=1 width=100%><BR>");
                sbMailContent.Append("<TABLE width=100% border=1 align=center cellpadding=4 bordercolor=#3366cc style='border-collapse: collapse'>");
                sbMailContent.Append(" <tr style=\"text-align: center; background-color:#6699cc;height: 34px; color: #FFFFFF;\"><td>厂区</td><td>仓别</td><td>储位</td><td>料号</td><td>箱号</td><td>数量</td></tr>");
                foreach(DataRow dr in dtCombineStorage.Rows)
                {
                    sbMailContent.Append("<tr style=\" color:Red;\"><td>  " + dr["WERKS"].ToString() + "</td><td>" + dr["LGORT"].ToString()
                                      + "</td><td>" + dr["LOCAT"].ToString() + "</td><td> " + dr["MATNR"].ToString()
                                      + "</td><td>" + dr["BOXID"].ToString() + "</td><td> " + dr["SQTY"].ToString() 
                                      + "</td></tr>");
                }
                sbMailContent.Append("</TD></TR></TABLE><BR><CENTER><br><HR SIZE=1 width=100%>");
                sbMailContent.Append("</BODY></HTML>");
                var Data = new
                {
                    Site = "QSMC",
                    TeamPwd = strMailPSD,
                    From = strMailFrom,
                    To = strMailTo,
                    Cc = strMailCC,
                    Bcc = "",
                    Subject = strMailSubject,
                    Body = sbMailContent.ToString(),
                    IsBodyHtml = true,
                    Attachments = "",
                };
                string strData = JsonConvert.SerializeObject(Data);
                JObject result = (JObject)JsonConvert.DeserializeObject(clahttpHelper.HttpPostByHttpWebRequest(strUrl, strData));
                if (result["Result"].ToString().ToUpper() == "TRUE")
                {
                    blResult = true;
                }
                //blResult = objSendMail.SendMail(strMailPSD, true, strMailFrom, strMailTo, strMailCC, "", strMailSubject, sbMailContent.ToString(), "", out strMges);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return blResult;
            }
            return blResult;
        }

        private bool SendMail_GR(DataTable dtMail)
        {
            bool blResult = false;
            try
            {
                //MailService.SendMailService objSendMail = new MailService.SendMailService();
                ClaHttpHelper clahttpHelper = new ClaHttpHelper();
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                string strUrl = objStorageData.GetSendMailUrl();
                string strMailPSD = "975A8056C8DF50786FB680A96E0CCCBF";
                string strMailFrom = "Web_Notice@quantacn.com";
                string strMailTo = string.Empty;
                string strMailSubject = string.Empty;

                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                strMailTo = objPlantData.GetMailConfig_Film();
               // strMailTo = "A6225013";
                if (string.IsNullOrEmpty(strMailTo))
                {
                    stsWarning.Text = "请先配置邮件相关人员";
                    return blResult;
                }

                if (Comcd == "2280")
                {
                    //strMailTo = "Zaixin.wang@quantacn.com;Mars.Guo@qm-technology.com;Lili.Liu@qm-technology.com;WH@qm-technology.com;Jinfeng.Wang@qm-technology.com;Andy.Tian@qm-technology.com;Mild.Wu@qm-tech.co;Material.team@qm-technology.com;Jeff.Huang@qm-technology.com;fevin.wang@qm-technology.com;eric.chan@qm-technology.com;Steven.Wu@qm-technology.com";
                    strMailSubject="Film良品出库待加扣MS10  TW10-MA10(1A101)";
                }
                else
                {
                    //strMailTo = "Sam.Zhang@quantacn.com;Juju.yang@quantacn.com;Bob.Bo2@quantacn.com;Terry.Cao@quantacn.com;Paul.Wang2@quantacn.com;L.F.Che@quantacn.com;Vicky.Shi@quantacn.com;WHKeyinTGC@quantacn.com;ReceivingKeyinTGC@quantacn.com;Judy.Tang@quantacn.com;Hank.Zheng@quantacn.com;MLMCMaterialCenter@quantacn.com;Qbus-Areceive.Team@quantacn.com; RTK.Shipment@quantacn.com;Ada.Li@quantacn.com";
                    strMailSubject = "Film良品出库待加扣CS32  TWMC-MA66(0MJ03)";
                }
                string strMailCC = "";
                string strMges = "";
                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<HTML><BODY leftmargin=1 topmargin=1><CENTER>");
                sbMailContent.Append("<FONT style='COLOR: #333333; FONT-FAMILY: &quot;宋体&quot;; FONT-SIZE: 9pt; position: relative; filter: blur(add=1, direction=45, strength=3)'>" + strMailSubject + "</FONT></a>");
                sbMailContent.Append("</CENTER><HR SIZE=1 width=80%><BR>");
                sbMailContent.Append("<TABLE width=80% border=1 align=center cellpadding=4 bordercolor=#3366cc style='border-collapse: collapse'>");
                sbMailContent.Append(" <tr style=\"text-align: center; background-color:#6699cc;height: 34px; color: #FFFFFF;\"><td>厂区</td><td>仓别</td><td>料号</td><td>箱号</td><td>数量</td></tr>");
                foreach (DataRow dr in dtMail.Rows)
                {
                    sbMailContent.Append("<tr style=\" color:Red;\"><td>  " + dr["WERKS"].ToString() + "</td><td>" + dr["LGORT"].ToString()
                                      + "</td><td> " + dr["MATNR"].ToString()
                                      + "</td><td>" + dr["BOXID"].ToString() + "</td><td> " + dr["QTY"].ToString()
                                      + "</td></tr>");
                }
                sbMailContent.Append("</TD></TR></TABLE><BR><CENTER><br><HR SIZE=1 width=100%>");
                sbMailContent.Append("</BODY></HTML>");

                // blResult = objSendMail.SendMail(strMailPSD, true, strMailFrom, strMailTo, "", "", strMailSubject, sbMailContent.ToString(), "", out strMges);
                var Data = new
                {
                    Site = "QSMC",
                    TeamPwd = strMailPSD,
                    From = strMailFrom,
                    To = strMailTo,
                    Cc = strMailCC,
                    Bcc = "",
                    Subject = strMailSubject,
                    Body = sbMailContent.ToString(),
                    IsBodyHtml = true,
                    Attachments = "",
                };
                string strData = JsonConvert.SerializeObject(Data);
                JObject result = (JObject)JsonConvert.DeserializeObject(clahttpHelper.HttpPostByHttpWebRequest(strUrl, strData));
                if (result["Result"].ToString().ToUpper() == "TRUE")
                {
                    blResult = true;
                }
                //blResult = objSendMail.SendMail(strMailPSD, true, strMailFrom, strMailTo, strMailCC, "", strMailSubject, sbMailContent.ToString(), "", out strMges);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return blResult;
            }
            return blResult;
        }

        private void InitMail(ref DataTable dtMail)
        {
            try
            {
                dtMail.Columns.Add("WERKS",typeof(string));
                dtMail.Columns.Add("LGORT", typeof(string));
                dtMail.Columns.Add("MATNR", typeof(string));
                dtMail.Columns.Add("BOXID", typeof(string));
                dtMail.Columns.Add("QTY", typeof(string));
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion

        #region Button Event

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (txtMblnr.Text.Trim() != "")
                {
                    string[] aryTempMblnr;
                    aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] { ',' });
                    aryMblnr.Clear();
                    if (aryMblnr.Count == 0)
                    {
                        for (int i = 0; i < aryTempMblnr.Length; i++)
                        {
                            aryMblnr.Add(aryTempMblnr[i].ToString().Trim());
                        }
                    }
                }

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
                //单号不为空
                if (Mblnrs.Count == 0)
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }

                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (Matnrs.Count == 0)
                {
                    dtData = objSapData.QuerySapLineOutData(Mblnrs, "Film_OutMaterial");
                }
                else
                {
                    dtData = objSapData.QuerySapLineOutData(Mblnrs, "Film_OutMaterial", Matnrs);
                }

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }

                ShoOutSourceDataGrid();

                #region 設定按鈕状态
                cmbWerks.Enabled = false;
                cmbLgort.Enabled = false;
                txtMblnr.Enabled = false;
                btnConfirm.Enabled = false;
                btnQuery.Enabled = true;
                #endregion

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow[] combineRow;
                DataRow drRow;
                string strOrderBy = "";
                btnPrint.Enabled = false;
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();

                DataTable dtTempStorage = new DataTable();
                DataSet dsData = new DataSet();
                //Order by
                strOrderBy = "LOCAT, MATNR, INDAT";
                //出库记录
                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                dsData = objStorageData.QueryOnLineOutData_Film(dtData, "", "", false, "", false, false); 

                dtData = dsData.Tables[0].Copy();
                dtTempStorage = dsData.Tables[1].Copy();
                dtTempStorage.Columns.Add("BLACE");

                #region 合并出库记录

                int intCombineLocatOut = 0;
                dtCombineStorage = dtTempStorage.Clone();
                for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                    sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                    sbCombineIndex.Append(" and BOXID='" + dtTempStorage.Rows[i]["BOXID"].ToString() + "'");
                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                    {
                        intCombineLocatOut = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtTempStorage.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }
                        drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                        drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        drRow["MENGE"] = dtTempStorage.Rows[i]["MENGE"].ToString();
                        drRow["ALQTY"] = intCombineLocatOut.ToString();
                        drRow["BLACE"] = Convert.ToInt32(dtTempStorage.Rows[i]["MENGE"].ToString()) - intCombineLocatOut;
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["LIFNR"] = "";
                        drRow["RMANO"] = "";
                        drRow["OMBLNR"] = "";
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString(); //線別
                        drRow["TRNTP"] = "";
                        drRow["RMAK1"] = "";
                        drRow["INDAT"] = "";
                        //客人料號 KDMAT
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                        drRow["BOXID"] = dtTempStorage.Rows[i]["BOXID"].ToString();
                        //DateCode
                        drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                        drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                        drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();
                        drRow["PKDAT"] = dtTempStorage.Rows[i]["PKDAT"].ToString();
                        drRow["SQTY"] = dtTempStorage.Rows[i]["SQTY"].ToString();
                        dtCombineStorage.Rows.Add(drRow);
                    }
                }

                #endregion

                #region 出库记录

                dtStorage = dtTempStorage.Clone();
                dtStorage.Columns.Add("MENGE1");
                //用于Scan BoxID
                dtStorage.Columns.Add("CHKED", typeof(Boolean));
                if (strOrderBy != "")
                {
                    foundRow = dtTempStorage.Select("", strOrderBy);
                    for (int i = 0; i < foundRow.Length; i++)
                    {
                        drRow = dtStorage.NewRow();
                        drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                        drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                        drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                        drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                        drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                        drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                        drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                        drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                        drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                        drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                        drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                        drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                        drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                        drRow["RMANO"] = foundRow[i]["RMANO"].ToString();
                        drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                        drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                        drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                        drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                        drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                        drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                        drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                        drRow["BLACE"] = foundRow[i]["BLACE"].ToString();
                        //客人料號 KDMAT
                        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                        drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                        drRow["BOXID"] = foundRow[i]["BOXID"].ToString();
                        //DateCode
                        drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                        drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                        drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                        drRow["PKDAT"] = foundRow[i]["PKDAT"].ToString();
                        drRow["SQTY"] = foundRow[i]["SQTY"].ToString();
                        dtStorage.Rows.Add(drRow);
                    }
                }
                else
                {
                    dtStorage = dtTempStorage;
                }

                #endregion

                this.btnPrint.Enabled = true;
                this.btnQuery.Enabled = false;
                ShowDataGrid();
                ShoOutSourceDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //数据校验
                if (dtStorage == null || dtStorage.Rows.Count == 0)
                {
                    MessageBox.Show("没有出库的数据，请确认！");
                    return;
                }
                StringBuilder strMsg = new StringBuilder();
                //用于邮件加扣（dtMail）
                DataTable dtMail = new DataTable();
                InitMail(ref dtMail);
                foreach (DataRow dr in dtCombineStorage.Rows)
                {
                    int iMengeQty = Convert.ToInt32(dr["MENGE"].ToString());
                    int iAlqty = Convert.ToInt32(dr["ALQTY"].ToString());
                    if (iMengeQty > iAlqty && iAlqty > 0)
                    {
                        strMsg.Append("料号：" + dr["MATNR"].ToString() + " BOXID：" + dr["BOXID"].ToString() + "未全部出库，需加扣Qty：" + (iMengeQty-iAlqty) + "\r");
                        #region dtMail赋值
                        DataRow drMail = dtMail.NewRow();
                        drMail["WERKS"] = dr["WERKS"].ToString();
                        drMail["LGORT"] = dr["LGORT"].ToString();
                        drMail["MATNR"] = dr["MATNR"].ToString();
                        drMail["BOXID"] = dr["BOXID"].ToString();
                        drMail["QTY"] = iMengeQty - iAlqty;
                        dtMail.Rows.Add(drMail);                       
                        #endregion
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(strMsg.ToString()))
                {
                    MessageBox.Show(strMsg.ToString());
                    SendMail_GR(dtMail);
                    return;
                }

                //Order by
                string strOrderBy = "";
                strOrderBy = "LOCAT,MATNR,INDAT";

                DataTable dtPrint = CommonInfo.SortDataTable(dtStorage, strOrderBy);
                ReportPrint objReportPrint;
                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                objReportPrint = new ReportPrint(UserData, "FILMOUT", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();

                //设置控件状态
                txtBoxID.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtStorage == null || dtStorage.Rows.Count == 0)
                {
                    MessageBox.Show("无出库数据，请确认！");
                    return;
                }
                DataRow[] drScan = dtStorage.Select("CHKED = true");
                if (drScan.Length != dtStorage.Rows.Count)
                {
                    MessageBox.Show("请先刷完BOX！！");
                    return;   
                }

                stsWarning.Text = "";
                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                //出QWMS
                if (objStorageOut.AddOnLineOutData_Film(dtData, dtStorage)) 
                {
                    //Send Mail
                    bool blReuslt = SendMail();
                    btnRefresh_Click(sender, e);
                    if (blReuslt)
                        stsWarning.Text = "Update OK!!";
                    else
                        stsWarning.Text = "Send Mail Fail!!";          
                    Sound.Play(@"Sound\OO1.wav");
                }
                else
                {
                    stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
                    Sound.Play(@"Sound\ERROR.wav");
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
            stsWarning.Text = string.Empty;
            //查询条件
            btnConfirm.Enabled = true;
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            txtMblnr.Enabled = true;
            txtMblnr.Text = "";

            txtBoxID.Enabled = false;
            txtBoxID.Text = "";
            btnQuery.Enabled = false;
            btnPrint.Enabled = false;
            btnSave.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;

            aryMblnr.Clear();
            aryMatnr.Clear();

            //DataGirdView
            dgvData.DataSource = null;
            this.lblCount.Text = "0 records";
            dgvBox.DataSource = null;
            this.lblBoxCount.Text = "0 records";
            dtData = new DataTable();
            dtStorage = new DataTable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Event

        private void txtBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    if (dtStorage == null || dtStorage.Rows.Count == 0)
                    {
                        MessageBox.Show("无出库数据，请确认！");
                        return;
                    }
                    //刷BOX
                    int tempQty = 0;
                    foreach (DataGridViewRow dataGridRow in dgvBox.Rows)
                    {
                        dataGridRow.DefaultCellStyle.BackColor = Color.Silver;
                        if (dataGridRow.Cells[3].Value == null)
                            continue;
                        if (txtBoxID.Text.Trim().ToUpper() == dataGridRow.Cells[3].Value.ToString().ToUpper())
                        {
                            if (dataGridRow.Cells[0].Value.ToString() == "True")
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                throw new Exception("BOX ID 已经刷过!!");
                            }
                            else
                            {
                                tempQty += Int32.Parse(dataGridRow.Cells[5].Value.ToString());
                                dataGridRow.Cells[0].Value = true;
                                dataGridRow.DefaultCellStyle.BackColor = Color.Blue;
                            }
                        }
                    }
                    if (tempQty == 0)
                    {
                        MessageBox.Show("Box ID 不存在!!!");
                        return;
                    }
                    //用于记录刷BOX的箱数
                    int iLen = 0;
                    foreach (DataRow drScan in dtStorage.Rows)
                    {
                        if (drScan["CHKED"].ToString() == "True")
                            iLen++;
                    }
                    //显示记录数
                    lblBoxCount.Text = string.Format("Scanned {0} of {1} records", iLen, dtStorage.Rows.Count.ToString());
                    if (iLen == dtStorage.Rows.Count)
                    {
                        btnSave.Enabled = true;
                        txtBoxID.Enabled = false;
                    }
                    else
                    {
                        txtBoxID.Text = "";
                        txtBoxID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                aryMblnr.Clear();
                if (txtMblnr.Text.Trim() != "")
                {
                    aryMblnr.Add(txtMblnr.Text.Trim());
                }
                StorageOut_SapDataSelect objStorageOut_SapDataSelect = new StorageOut_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "Film_OutMaterial");
                objStorageOut_SapDataSelect.ShowDialog();

                Mblnrs = objStorageOut_SapDataSelect.Mblnr;
                Matnrs = objStorageOut_SapDataSelect.Matnrs;
                txtMblnr.Text = GetMblnrData();
                if (Mblnrs.Count > 0)
                {
                    this.txtMblnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        #endregion

    }
}
