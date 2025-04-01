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
    public partial class MonthlySalesChart : KryptonForm
    {
        public MonthlySalesChart()
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

       
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);

            //this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            //e.Graphics.DrawImage(bm, 0, 0);

            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }

        
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // saveFileDialog1.Title = "Save text Files";
            // saveFileDialog1.CheckFileExists = true;
            // saveFileDialog1.CheckPathExists = true;
            //// saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.FileName = "SalesReport_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            saveFileDialog1.ShowDialog();
        }

        private void dtEndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string sql5 = "select sales_time, SUM(total) as Total from sales_item " +
                    " where sales_time   like  '%" + dtyearmonth.Text + "%' and status = 1  or status = 3 GROUP BY  sales_time ";

                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                chartBarSale.DataSource = dt5;
                chartBarSale.Visible = true;
                chartBarSale.ChartAreas[0].AxisX.LabelStyle.Angle = 45;
                chartBarSale.Series["Sale"].XValueMember = "sales_time";
                chartBarSale.Series["Sale"].YValueMembers = "Total";
                chartBarSale.DataBind();

                string sql2 = "select sales_time, SUM(total) as Total , SUM(profit * Qty) as Profit from sales_item " +
                    "  where sales_time   like  '%" + dtyearmonth.Text + "%'  and status = 1  or status = 3 GROUP BY  sales_time ";
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

        private void GeneralLedger_Load(object sender, EventArgs e)
        {
            dtyearmonth.Format = DateTimePickerFormat.Custom;
            dtyearmonth.CustomFormat = "yyyy-MM";
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
    }
}
