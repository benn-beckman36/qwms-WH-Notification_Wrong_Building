using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using QWMS.Common;
using QCI.QWMS;
using System.IO;

namespace QWMS
{
    public partial class Manage_LocationPallet_Info : Form
    {
        string MANDT,COMCD,WERKS,LGORT,FLOOR,AREA;
        DataTable dtTemp = new DataTable();
        ArrayList a1 = new ArrayList();

        public Manage_LocationPallet_Info(string M1,string C1,string W1,string L1,string F1,string A1,DataTable dt)
        {
            MANDT = M1;
            COMCD = C1;
            WERKS = W1;
            LGORT = L1;
            FLOOR = F1;
            AREA = A1;

            dtTemp = dt;

            InitializeComponent();
            this.Text = "Location Deatil";
            Initial_Control();            
        }

        //控制項初始化
        private void Initial_Control()
        {
            #region 控制項控制

            labWerks.Text = WERKS;
            labLgort.Text = LGORT;

            if (FLOOR.Equals(""))
                labFloor.Text = "NULL";
            else
                labFloor.Text = FLOOR;
            
            if(AREA.Equals(""))
                labArea.Text = "NULL";
            else
                labArea.Text = AREA;

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (dtTemp.Rows[i]["TYPE"] != null)
                {
                    if (!dtTemp.Rows[i]["TYPE"].ToString().Equals(""))//有儲位類型資訊
                    {
                        a1.Add(dtTemp.Rows[i]["LOCAT"].ToString().Trim()+"("+dtTemp.Rows[i]["TYPE"].ToString().Trim()+")");
                    }
                    else
                        a1.Add(dtTemp.Rows[i]["LOCAT"].ToString().Trim());
                }
                else
                    a1.Add(dtTemp.Rows[i]["LOCAT"].ToString().Trim());                
            }

            dtTemp.Columns.Remove("LOCAT");
            dtTemp.Columns.Remove("TYPE");
            gvData.DataSource = dtTemp; 

            #endregion
        }

        private void gvdata_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.LightGray;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            gvData.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            gvData.RowHeadersDefaultCellStyle = columnHeaderStyle;
            gvData.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (int i = 0; i < gvData.Columns.Count; i++)
                    gvData.Columns[i].Width = 100;

            #region 設置DataGridView的行名

            this.gvData.RowHeadersWidth = 120;

            for (int i = 0; i < a1.Count; i++)
            {
                this.gvData.Rows[i].HeaderCell.Value = a1[i].ToString();
            }

            #endregion
        }
    }
}
