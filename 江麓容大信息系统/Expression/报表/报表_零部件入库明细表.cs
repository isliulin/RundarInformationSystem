﻿/******************************************************************************
 * 版权所有 (c) 2006-2010, 小康工业集团容大有限责任公司
 *
 * 文件名称:  FormReportAccessoryInDepotBillDetailBill.cs
 * 作者    :  夏石友    版本: v1.00    日期: 2009/06/15
 * 开发平台:  vs2005(c#)
 * 用于    :  生产线管理信息系统
 *----------------------------------------------------------------------------
 * 描述 : 关于界面
 * 其它 :
 *----------------------------------------------------------------------------
 * 历史记录:
 *     1. 日期时间: 2009/07/03 08:02:08 作者: 夏石友 当前版本: V1.00
 *        修改说明: 创建
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Expression
{
    /// <summary>
    /// 零件入库明细表报表界面
    /// </summary>
    public partial class 报表_零部件入库明细表 : Form
    {
        string m_showOrder = "";
        DateTime m_beginTime;
        DateTime m_endTime;

        public 报表_零部件入库明细表(string showOrder, DateTime beginTime, DateTime endTime)
        {
            InitializeComponent();
            m_showOrder = showOrder;
            m_beginTime = beginTime;
            m_endTime = endTime;
        }

        private void FormReportAccessoryInDepotBillDetailBill_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GlobalObject.GlobalParameter.StorehouseConnectionString;
                conn.Open();

                string sql = "";

                if (m_showOrder == "按日期汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 单据日期,图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按图号汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按名称汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 名称,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按供应商汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 供应商,图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按库房汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 库房名称,图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按材料类别汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 材料类别,图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }
                else if (m_showOrder == "按单据编号汇总")
                {
                    sql = string.Format("select * from dbo.View_S_InDepotDetailBill where 单据日期 >= '{0}' and 单据日期 <= '{1}' "+
                                        " order by 单据编号,图号型号,规格", m_beginTime.Date.ToShortDateString(), 
                                        m_endTime.Date.ToShortDateString() + " " + m_endTime.ToLongTimeString());
                }

                SqlCommand command = new SqlCommand(sql);
                command.Connection = conn;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(this.DepotManagementDataSet.View_S_InDepotDetailBill);

                ReportDataSource rds = new ReportDataSource("DepotManagementDataSet_View_S_InDepotDetailBill", 
                    this.DepotManagementDataSet.View_S_InDepotDetailBill);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
