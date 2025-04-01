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
    public partial class Overview : KryptonForm
    {
        public Overview()
        {
            InitializeComponent();
        }

        private void dtEndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string sql5 = " select sales_time,  SUM(profit * Qty) as Profit from sales_item " +
                               " where sales_time   like  '%" + dtyearmonth.Text + "%' and status = 1  or status = 3 GROUP BY  sales_time";
                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                chartbarProfit.DataSource = dt5;
                chartbarProfit.Visible = true;
                chartbarProfit.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                chartbarProfit.Series["Profit"].XValueMember = "sales_time";
                chartbarProfit.Series["Profit"].YValueMembers = "Profit";
                chartbarProfit.DataBind();



                string sql2 = "select   SUM(profit * Qty) as Profit, sales_time from sales_item " +
                                " where sales_time   like  '%" + dtyearmonth.Text + "%' and status = 1  or status = 3 GROUP BY  sales_time";

                DAL.DataAccessManager.ExecuteSQL(sql2);
                DataTable dt2 = DAL.DataAccessManager.GetDataTable(sql2);
                chartPieProfit.DataSource = dt2;
                chartPieProfit.Visible = true;
                chartPieProfit.Series["Profit"].XValueMember = "sales_time";
                chartPieProfit.Series["Profit"].YValueMembers = "Profit";
                chartPieProfit.DataBind();

                string sql3 = " select sales_time, SUM(total) as Total from sales_item " +
                                " where sales_time   like  '%" + dtyearmonth.Text + "%' and status = 1  or status = 3 GROUP BY  sales_time";

                DAL.DataAccessManager.ExecuteSQL(sql3);
                DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);
                chartPieSales.DataSource = dt3;
                chartPieSales.Visible = true;
                chartPieSales.Series["Total"].XValueMember = "sales_time";
                chartPieSales.Series["Total"].YValueMembers = "Total";
                chartPieSales.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
               

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Overview_Load(object sender, EventArgs e)
        {
            dtyearmonth.Format = DateTimePickerFormat.Custom;
            dtyearmonth.CustomFormat = "yyyy-MM";

            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM");
            try
            {
                //Profit Chart
                string sql5 = " select sales_time, SUM(profit * Qty) as Profit from sales_item " +
                                " where sales_time   like  '%" + date + "%' and status = 1  or status = 3 GROUP BY  sales_time ";
                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                chartbarProfit.DataSource = dt5;
                chartbarProfit.Visible = true;
                chartbarProfit.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                chartbarProfit.Series["Profit"].XValueMember = "sales_time";
                chartbarProfit.Series["Profit"].YValueMembers = "Profit";
                chartbarProfit.DataBind();

                //Profit Pie chart 
                string sql2 = "select  SUM(profit * Qty) as Profit , sales_time from sales_item " +
                            " where sales_time   like  '%" + date + "%' and status = 1  or status = 3  GROUP BY  sales_time ";
                DAL.DataAccessManager.ExecuteSQL(sql2);
                DataTable dt2 = DAL.DataAccessManager.GetDataTable(sql2);
                chartPieProfit.DataSource = dt2;
                chartPieProfit.Visible = true;
                chartPieProfit.Series["Profit"].XValueMember = "sales_time";
                chartPieProfit.Series["Profit"].YValueMembers = "Profit";
                chartPieProfit.DataBind();

                // Sales Pie chart
                string sql3 = " select sales_time, SUM(total) as Total from sales_item where sales_time " +
                                "  like  '%" + date + "%' and status = 1  or status = 3  GROUP BY  sales_time ";
                DAL.DataAccessManager.ExecuteSQL(sql3);
                DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);
                chartPieSales.DataSource = dt3;
                chartPieSales.Visible = true;
                chartPieSales.Series["Total"].XValueMember = "sales_time";
                chartPieSales.Series["Total"].YValueMembers = "Total";
                chartPieSales.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //chart1.Dock = DockStyle.Fill;
                chartPieProfit.Dock = DockStyle.Fill;
                chartPieSales.Dock = DockStyle.Fill;
                chartbarProfit.Printing.PrintDocument.DefaultPageSettings.Landscape = true;
                chartPieProfit.Printing.PrintDocument.DefaultPageSettings.Landscape = true;
                chartPieSales.Printing.PrintDocument.DefaultPageSettings.Landscape = true;
                // Print preview chart

                chartbarProfit.Printing.PrintPreview();
                chartPieProfit.Printing.PrintPreview();
                chartPieSales.Printing.PrintPreview();

            }
            catch
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
