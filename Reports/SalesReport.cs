using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Drawing.Printing;
using System.IO;
using PointofSale.BAL;
using Microsoft.Reporting.WinForms;

namespace PointofSale
{
    public partial class SalesReport : KryptonForm
    {
        public SalesReport()
        {
            InitializeComponent();
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            try
            {
                dtStartDate.Format = DateTimePickerFormat.Custom;
                dtStartDate.CustomFormat = "yyyy-MM-dd";
                dtEndDate.Format = DateTimePickerFormat.Custom;
                dtEndDate.CustomFormat = "yyyy-MM-dd";

                string sql5 = "   select     DISTINCT '' as Username    from usermgt  union all " +
                                " select   DISTINCT  Username   from usermgt ";
                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                cmbEmp.DataSource = dt5;
                cmbEmp.DisplayMember = "Username";


                string sqltr = " select  DISTINCT '' as BranchName ,'' as Shopid from tbl_terminalLocation  union all" +
                               " select   BranchName , Shopid from tbl_terminalLocation   ";
                DAL.DataAccessManager.ExecuteSQL(sqltr);
                DataTable dttr = DAL.DataAccessManager.GetDataTable(sqltr);
                cmbTerminal.DataSource = dttr;
                cmbTerminal.DisplayMember = "BranchName";
                cmbTerminal.ValueMember = "Shopid";
            }
            catch
            {
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                ReportValue.StartDate = dtStartDate.Text; // dtStartDate.Value.ToShortDateString();
                ReportValue.EndDate = dtEndDate.Text; // dtEndDate.Value.ToShortDateString();
                ReportValue.emp = cmbEmp.Text;
                ReportValue.Terminal = cmbTerminal.SelectedValue.ToString();
                LoadReport();
            }
            catch
            {
            }
        }

        private void LoadReport()
        {
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            try
            {
                if (ReportValue.emp == "" && ReportValue.Terminal == "")   //Report by Every transaction -  Only Date to Date 
                {
                    ReportParameter parReportParam1 = new ReportParameter("Dates", ReportValue.StartDate + "  To  " + ReportValue.EndDate);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { parReportParam1 });
                    string sql = "select storeconfig.* , sales_payment.payment_amount AS Payamount,  sales_payment.due_amount AS due, " +
                     " sales_payment.dis, sales_payment.vat, sales_payment.sales_time AS sales_time , sales_payment.emp_id AS empID , sales_payment.sales_id AS salesid   " +
                     " from sales_payment , storeconfig " +
                     " where sales_payment.sales_time BETWEEN '" + ReportValue.StartDate + "' AND    '" + ReportValue.EndDate + "' " +
                     "  Order  by sales_payment.sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                    ReportDataSource reportDSDetail = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                }
                else if (ReportValue.emp != "" && ReportValue.Terminal == "")   //Report by Every transaction -  Employee with date to date 
                {
                    string paravalue = ReportValue.StartDate + "  To  " + ReportValue.EndDate + " and " + ReportValue.emp;
                    ReportParameter parReportParam1 = new ReportParameter("Dates", "Report by : " + paravalue);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { parReportParam1 });
                    string sql = "select storeconfig.* , sales_payment.payment_amount AS Payamount,  sales_payment.due_amount AS due, " +
                     " sales_payment.dis, sales_payment.vat, sales_payment.sales_time AS sales_time , sales_payment.emp_id AS empID , sales_payment.sales_id AS salesid   " +
                     " from sales_payment , storeconfig " +
                     " where sales_payment.sales_time BETWEEN '" + ReportValue.StartDate + "' AND    '" + ReportValue.EndDate + "' " +
                     " AND sales_payment.emp_id = '" + ReportValue.emp + "' " +
                     "  Order  by sales_payment.sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                    ReportDataSource reportDSDetail = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                }
                else if (ReportValue.emp == "" && ReportValue.Terminal != "")     //Report by Every transaction -    Terminal with date to date
                {
                    string paravalue = ReportValue.StartDate + "  To  " + ReportValue.EndDate + " and " + ReportValue.Terminal;
                    ReportParameter parReportParam1 = new ReportParameter("Dates", "Report by : " + paravalue);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { parReportParam1 });
                    string sql = "select storeconfig.* , sales_payment.payment_amount AS Payamount,  sales_payment.due_amount AS due, " +
                     " sales_payment.dis, sales_payment.vat, sales_payment.sales_time AS sales_time , sales_payment.emp_id AS empID , sales_payment.sales_id AS salesid   " +
                     " from sales_payment , storeconfig " +
                     " where sales_payment.sales_time BETWEEN '" + ReportValue.StartDate + "' AND    '" + ReportValue.EndDate + "' " +
                     " AND sales_payment.Shopid = '" + ReportValue.Terminal + "' " +
                     "  Order  by sales_payment.sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                    ReportDataSource reportDSDetail = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                }
                else if (ReportValue.emp != "" && ReportValue.Terminal != "")   //Report by Every transaction -  Employee and  Terminal with date to date  -- All
                {
                    string empterminal = ReportValue.StartDate + "  To  " + ReportValue.EndDate + " and " + ReportValue.emp + " - " + ReportValue.Terminal;
                    ReportParameter parReportParam1 = new ReportParameter("Dates", "Report by : " + empterminal);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { parReportParam1 });
                    string sql = "select storeconfig.* , sales_payment.payment_amount AS Payamount,  sales_payment.due_amount AS due, " +
                     " sales_payment.dis, sales_payment.vat, sales_payment.sales_time AS sales_time , sales_payment.emp_id AS empID , sales_payment.sales_id AS salesid   " +
                     " from sales_payment , storeconfig " +
                     " where sales_payment.sales_time BETWEEN '" + ReportValue.StartDate + "'  AND    '" + ReportValue.EndDate + "' " +
                     " AND sales_payment.emp_id = '" + ReportValue.emp + "' AND sales_payment.Shopid = '" + ReportValue.Terminal + "' " +
                     "  Order  by sales_payment.sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                    ReportDataSource reportDSDetail = new ReportDataSource("DataSet1", dt);
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                }
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                // this.reportViewer1.ZoomPercent = 35;
                this.reportViewer1.RefreshReport();
            }
            catch
            {
            }
        }
    }
}
