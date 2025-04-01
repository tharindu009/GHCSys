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

namespace PointofSale
{
    public partial class dashboardOne : KryptonForm
    {
        public dashboardOne()
        {
            InitializeComponent();
        }

        DataGridViewPrinter MyDataGridViewPrinter;

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdJobDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }




        

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void kryptonButton2_Click_2(object sender, EventArgs e)
        {
            try
            {

                chartBarSale.Printing.PrintDocument.DefaultPageSettings.Landscape = true;
                // Print preview chart
                chartBarSale.Printing.PrintPreview();
            }
            catch
            {

            }
        }

        private void GetDashboardNumbers()
        {
            //Get open job Count
            string jobCount = "SELECT count(*) FROM [JobHybridMaster] where Status = 'Open'";
            DataTable dtjob = DAL.DataAccessManager.GetDataTable(jobCount);
            lblopen.Text = dtjob.Rows[0][0].ToString();

            //Get Customers
            string CustomerCount = "SELECT count(*) FROM [tbl_customer]";
            DataTable dtCustomer = DAL.DataAccessManager.GetDataTable(CustomerCount);
            lblCustomer.Text = dtCustomer.Rows[0][0].ToString();

        }


        private void BindCurrentJobs()
        {
            string JobQuery = @"SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status]
                                FROM [dbo].[JobHybridMaster] where [Status] = 'Open'";

            DataTable dtJob = DAL.DataAccessManager.GetDataTable(JobQuery);
            dtgviewCusttrxHistory.DataSource = dtJob;
        }

        private void Todaysales()
        {
            string DTtoday = DateTime.Now.ToString("yyyy-MM-dd");
            string sql3 = "select SUM(payment_amount), SUM(vat) , SUM(due_amount), SUM(dis) " +
                       " from sales_payment  where sales_time   >='" + DTtoday + "' AND  sales_time <='" + DTtoday + "' ";
            DAL.DataAccessManager.ExecuteSQL(sql3);
            DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);

            //dr2[1] = "Sub Total";
            lblTodaySales.Text = "Rs." + (Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString())).ToString();
        }

        private void GeneralLedger_Load(object sender, EventArgs e)
        {
            GetDashboardNumbers();
            BindCurrentJobs();
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM");
            try
            {

                string sql5 = "select sales_time, SUM(total) as Total from sales_item " +
                                " where sales_time   like  '%" + date + "%' and status = 1  or status = 3  GROUP BY  sales_time ";


                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                chartBarSale.DataSource = dt5;
                chartBarSale.Visible = true;
                chartBarSale.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                chartBarSale.Series["Sale"].XValueMember = "sales_time";
                chartBarSale.Series["Sale"].YValueMembers = "Total";
                chartBarSale.DataBind();

                string sql2 = "select sales_time, SUM(total) as Total , SUM(profit * Qty) as Profit from sales_item " +
                            " where sales_time   like  '%" + date + "%' and status = 1  or status = 3  GROUP BY  sales_time ";
                DAL.DataAccessManager.ExecuteSQL(sql2);
                DataTable dt2 = DAL.DataAccessManager.GetDataTable(sql2);
                chartBarSalesProfitCom.DataSource = dt2;
                chartBarSalesProfitCom.Visible = true;
                chartBarSalesProfitCom.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                chartBarSalesProfitCom.Series["Sale"].XValueMember = "sales_time";
                chartBarSalesProfitCom.Series["Sale"].YValueMembers = "Total";

                chartBarSalesProfitCom.Series["Profit"].XValueMember = "sales_time";
                chartBarSalesProfitCom.Series["Profit"].YValueMembers = "Profit";
                chartBarSalesProfitCom.DataBind();

            }
            catch
            {

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            jobCardHybrid jbh = new jobCardHybrid();
            jbh.changetab();
            jbh.MdiParent = Application.OpenForms["dashboard"];
            jbh.FormBorderStyle = FormBorderStyle.FixedSingle;
            jbh.WindowState = FormWindowState.Maximized;

            jbh.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            addNewCustomer customer = new addNewCustomer();
            customer.MdiParent = Application.OpenForms["dashboard"];
            customer.FormBorderStyle = FormBorderStyle.FixedSingle;
            customer.WindowState = FormWindowState.Maximized;

            customer.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            todaySales td = new todaySales();
            td.DTtoday = DateTime.Now.ToString("yyyy-MM-dd");
            td.MdiParent = Application.OpenForms["dashboard"];
            td.FormBorderStyle = FormBorderStyle.FixedSingle;
            td.WindowState = FormWindowState.Maximized;

            td.Show();
        }
    }
}
