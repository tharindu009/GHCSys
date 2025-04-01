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
    public partial class ReportDialog : KryptonForm
    {
        public ReportDialog()
        {
            InitializeComponent();
        }

        DataGridViewPrinter MyDataGridViewPrinter;


        private bool SetupThePrinting()
        {
            string sql3 = "select * from tbl_terminallocation where Shopid = '" + UserInfo.Shopid + "'";
            DAL.DataAccessManager.ExecuteSQL(sql3);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql3);
            DateTime dt = DateTime.Now;
            string printdate = dt.ToString("MMMM dd, yyyy    hh:mm:ss tt");
            string Companyname = dt1.Rows[0].ItemArray[1].ToString();
            string branchname = dt1.Rows[0].ItemArray[2].ToString();
            string Location = dt1.Rows[0].ItemArray[3].ToString();
            string phone = dt1.Rows[0].ItemArray[4].ToString();
            string email = dt1.Rows[0].ItemArray[5].ToString();
            string web = dt1.Rows[0].ItemArray[6].ToString();

            string Header = Companyname + "\n" + Location + "." + "\n" + email + "\n" + branchname + " ph: " + phone + "\n" + printdate + "\n";

            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;


            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDocument1.DocumentName = "SalesReport_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            printDocument1.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDocument1.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDocument1.DefaultPageSettings.Margins = new Margins(10, 10, 20, 20);

            //  if (MessageBox.Show("Do you want the report to be centered on the page",   "InvoiceManager - Center on Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            MyDataGridViewPrinter = new DataGridViewPrinter(dtgrdViewSalesReport,
            printDocument1, true, true, Header + " Sales Report \n", new Font("Baskerville Old Face", 13, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);


            //else

            //    MyDataGridViewPrinter = new DataGridViewPrinter(dtgrdViewSalesReport,
            //    printDocument1, false, true, Header + "   Sales Report   \n", new Font("Times New Roman", 14, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);

            return true;
        }


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            foreach (DataGridViewColumn column in dtgrdViewSalesReport.Columns)
            {
                csv += column.HeaderText + ',';
            }

            //Add new line.
            csv += "\r\n";

            //Adding the Rows
            foreach (DataGridViewRow row in dtgrdViewSalesReport.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //Add the Data rows.
                    csv += cell.Value.ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Exporting to CSV.
            //  string targetPath = "D:\\";
            string fileName = "SalesReport_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location: 
            // Create a new target folder, if necessary. 
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);

            }

            File.WriteAllText(destFile, csv);
            MessageBox.Show(" Successfully Exported !!! ", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void duePaymentHistory_Load(object sender, EventArgs e)
        {
            Dateformat();
            dtgrdViewSalesReport.EnableHeadersVisualStyles = false;
            dtgrdViewSalesReport.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dtgrdViewSalesReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dtgrdViewSalesReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dtDailyPaymentReport_ValueChanged((object)sender, (EventArgs)e);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdJobDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void Dateformat()
        {
            dtDailyPaymentReport.Format = DateTimePickerFormat.Custom;
            dtDailyPaymentReport.CustomFormat = "yyyy-MM-dd";

            dtStartDate.Format = DateTimePickerFormat.Custom;
            dtStartDate.CustomFormat = "yyyy-MM-dd";

            dtEndDate.Format = DateTimePickerFormat.Custom;
            dtEndDate.CustomFormat = "yyyy-MM-dd";

            dtSalesItemEND.Format = DateTimePickerFormat.Custom;
            dtSalesItemEND.CustomFormat = "yyyy-MM-dd";

            dtSalesItemStart.Format = DateTimePickerFormat.Custom;
            dtSalesItemStart.CustomFormat = "yyyy-MM-dd";

            //dtReturnEndDate.Format = DateTimePickerFormat.Custom;
            //dtReturnEndDate.CustomFormat = "yyyy-MM-dd";

            //dtReturnStartDate.Format = DateTimePickerFormat.Custom;
            //dtReturnStartDate.CustomFormat = "yyyy-MM-dd";
            DateTime dt = DateTime.Now;
        }

        private void dtDailyPaymentReport_ValueChanged(object sender, EventArgs e)
        {
            if (dtDailyPaymentReport.Text == "")
            {

                // MessageBox.Show("Please Select Date ");
            }
            else
            {
                try
                {
                    dtgrdViewSalesReport.Refresh();
                    string sql = "select  sales_id as 'Rpt No' , sales_time as 'Date' , payment_amount as 'Total' , emp_id as 'Sold by',  dis as 'Dis' , " +
                                    " vat as 'TAX' ,  payment_type as 'Pay type' ,  due_amount as 'Due', c.[Name] as 'Cust Name' , Comment as 'Comments' " +
                                    " from sales_payment p inner join [tbl_customer] c ON c.ID = p.[c_id] where sales_time   like  '%" + dtDailyPaymentReport.Text + "%' order by sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                    dtgrdViewSalesReport.DataSource = dt1;
                    dtgrdViewSalesReport.DefaultCellStyle.Font = new Font("Times New Roman", 8.5F);

                    string sql3 = "select SUM(payment_amount),  SUM(vat) , SUM(due_amount) , SUM(dis) from sales_payment  where sales_time like  '%" + dtDailyPaymentReport.Text + "%'";
                    DAL.DataAccessManager.ExecuteSQL(sql3);
                    DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);
                    // label5.Text = "Total Sales= " + dt3.Rows[0].ItemArray[0].ToString();
                    //  toolStripStatusLabel1.Text = "Total Profit = " + dt3.Rows[0].ItemArray[1].ToString();

                    DataRow dr = dt1.NewRow();
                    dr[1] = " ";
                    dt1.Rows.Add(dr);

                    /// Sub total = total - Discount
                    DataRow dr2 = dt1.NewRow();
                    dr2[1] = "Sub Total";
                    dr2[2] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString());
                    // dr2[4] = dt3.Rows[0].ItemArray[2].ToString();
                    dt1.Rows.Add(dr2);

                    DataRow discount = dt1.NewRow();
                    discount[1] = "Total Discount";
                    discount[2] = dt3.Rows[0].ItemArray[3].ToString();
                    dt1.Rows.Add(discount);


                    DataRow dr4 = dt1.NewRow();
                    dr4[1] = "Total TAX ";
                    dr4[2] = dt3.Rows[0].ItemArray[1].ToString();
                    dt1.Rows.Add(dr4);

                    DataRow dr6 = dt1.NewRow();
                    dr6[1] = " ";
                    dt1.Rows.Add(dr6);

                    //Payable amount
                    DataRow dr5 = dt1.NewRow();
                    dr5[1] = "Total Sales + TAX ";
                    dr5[2] = dt3.Rows[0].ItemArray[0].ToString();
                    dt1.Rows.Add(dr5);

                    //DataRow profit = dt1.NewRow();
                    //profit[1] = "Total Profit =";
                    //profit[6] = dt3.Rows[0].ItemArray[1].ToString();
                    //dt1.Rows.Add(profit);


                    DataRow dr8 = dt1.NewRow();
                    dr8[1] = "Total Due ";
                    dr8[2] = dt3.Rows[0].ItemArray[2].ToString();
                    dt1.Rows.Add(dr8);

                    DataRow dr17 = dt1.NewRow();
                    dr17[1] = " ";
                    dt1.Rows.Add(dr17);

                    ////In Cash = Payable amount - Due amount
                    //DataRow dr9 = dt1.NewRow();
                    //dr9[1] = "Total in Cash ";
                    //dr9[2] = (Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString())) - Convert.ToDouble(dt3.Rows[0].ItemArray[2].ToString());
                    //dt1.Rows.Add(dr9);

                    DataRow dr7 = dt1.NewRow();
                    dr7[1] = " ";
                    dt1.Rows.Add(dr7);

                    DataRow rep = dt1.NewRow();
                    rep[1] = "Daily Report For ";
                    rep[3] = dtDailyPaymentReport.Text;
                    //rep[4] = dt3.Rows[0].ItemArray[5].ToString();
                    dt1.Rows.Add(rep);
                }
                catch
                {
                    // MessageBox.Show("There is no Data in this date");
                }
            }
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

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.dtgrdViewSalesReport.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtgrdViewSalesReport.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                if (SetupThePrinting())
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                    // MyPrintPreviewDialog.ClientSize = new System.Drawing.Size(990, 630);
                    MyPrintPreviewDialog.WindowState = FormWindowState.Maximized;
                    MyPrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                    // MyPrintPreviewDialog.UseAntiAlias = true;
                    MyPrintPreviewDialog.Document = printDocument1;
                    MyPrintPreviewDialog.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("!!! Please Print Preview or Setup Print only for First time " + exp.Message);
            }
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
            if (dtEndDate.Text == "")
            {

                // MessageBox.Show("Please Select Date ");
            }
            else
            {
                try
                {
                    dtgrdViewSalesReport.Refresh();
                    string sql = "select   sales_time as 'Date' , SUM(payment_amount) as 'Total' , SUM(dis) as 'Discount' , SUM(vat) as 'TAX'  ,  " +
                                "SUM(due_amount)  as 'Due'  from sales_payment where sales_time BETWEEN '" + dtStartDate.Text + "' AND    '" + dtEndDate.Text + "' " +
                                " GROUP BY  sales_time  Order  by sales_time"; //order by salesdate
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                    dtgrdViewSalesReport.DataSource = dt1;
                    dtgrdViewSalesReport.DefaultCellStyle.Font = new Font("Times New Roman", 13.0F);

                    string sql3 = "select SUM(payment_amount), SUM(vat) , SUM(due_amount), SUM(dis)  from sales_payment " +
                                  " where sales_time   >='" + dtStartDate.Text + "' AND  sales_time <='" + dtEndDate.Text + "' ";
                    DAL.DataAccessManager.ExecuteSQL(sql3);
                    DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);


                    DataRow dr = dt1.NewRow();
                    dr[0] = " ";
                    dt1.Rows.Add(dr);


                    DataRow discount = dt1.NewRow();
                    discount[0] = "Total Discount";
                    discount[2] = Convert.ToDouble(dt3.Rows[0].ItemArray[3].ToString());
                    dt1.Rows.Add(discount);



                    DataRow dr4 = dt1.NewRow();
                    dr4[0] = "Total TAX ";
                    dr4[3] = Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString());
                    dt1.Rows.Add(dr4);

                    DataRow dr6 = dt1.NewRow();
                    dr6[0] = " ";
                    dt1.Rows.Add(dr6);

                    DataRow dr5 = dt1.NewRow();
                    dr5[0] = "Total Sales+TAX ";
                    dr5[1] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString());
                    dt1.Rows.Add(dr5);

                    DataRow dr8 = dt1.NewRow();
                    dr8[0] = "Total Due ";
                    dr8[4] = Convert.ToDouble(dt3.Rows[0].ItemArray[2].ToString());
                    dt1.Rows.Add(dr8);

                    DataRow dr17 = dt1.NewRow();
                    dr17[0] = " ";
                    dt1.Rows.Add(dr17);

                    //DataRow dr9 = dt1.NewRow();
                    //dr9[0] = "Total in Cash ";
                    //dr9[1] = (Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString())) - Convert.ToDouble(dt3.Rows[0].ItemArray[2].ToString());
                    //dt1.Rows.Add(dr9);

                    DataRow dr7 = dt1.NewRow();
                    dr7[0] = "_________________________________ ";
                    dt1.Rows.Add(dr7);


                    DataRow rep = dt1.NewRow();
                    rep[0] = "Payment Report ";
                    // rep[3] = dateTimePicker1.Text;                  
                    dt1.Rows.Add(rep);

                    DataRow repdt = dt1.NewRow();
                    // rep[3] = "Daily Report For ";
                    repdt[0] = "From : " + dtStartDate.Text;
                    //rep[4] = dt3.Rows[0].ItemArray[5].ToString();
                    dt1.Rows.Add(repdt);

                    DataRow repdt2 = dt1.NewRow();
                    // rep[3] = "Daily Report For ";
                    repdt2[0] = "To : " + dtEndDate.Text;
                    //rep[4] = dt3.Rows[0].ItemArray[5].ToString();
                    dt1.Rows.Add(repdt2);
                }
                catch
                {
                    // MessageBox.Show("There is no Data in this date");
                }
            }
        }

        private void dtSalesItemStart_ValueChanged(object sender, EventArgs e)
        {

            if (dtSalesItemEND.Text == "")
            {

                // MessageBox.Show("Please Select Date ");
            }
            else
            {
                try
                {
                    dtgrdViewSalesReport.Columns.Clear();
                    string sql = " select   sales_time as 'Date' , itemName as 'Name', RetailsPrice  as 'Price', Qty,  Total,  " +
                                    " ((profit * Qty) * 1.00) as 'Profit'   , discount as 'Dis Rate', sales_id as 'Rpt.No' " +
                                    " from sales_item where sales_time BETWEEN '" + dtSalesItemStart.Text + "' AND    '" + dtSalesItemEND.Text + "' " +
                                    " and status != 2  Order  by sales_time";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                    dtgrdViewSalesReport.DataSource = dt1;
                    dtgrdViewSalesReport.DefaultCellStyle.Font = new Font("Trebuchet MS", 12.0F);

                    string sql3 = " select SUM(Total) , SUM(profit * Qty) as Profit , SUM(discount)   " +
                                    " from sales_item where sales_time BETWEEN '" + dtSalesItemStart.Text + "' AND    '" + dtSalesItemEND.Text + "' " +
                                    " and status != 2  Order  by sales_time";
                    // " from sales_item  where sales_time   >='" + dtSalesItemStart.Text + "' AND  sales_time <='" + dtSalesItemEND.Text + "' and status != 2    ";
                    DAL.DataAccessManager.ExecuteSQL(sql3);
                    DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);

                    //double sum = 0;
                    //for (int i = 0; i < dtgrdViewSalesReport.Rows.Count; ++i)
                    //{
                    //    sum += Convert.ToDouble(dtgrdViewSalesReport.Rows[i].Cells[5].Value);
                    //}

                    DataRow dr = dt1.NewRow();
                    dr[0] = " ";
                    dt1.Rows.Add(dr);

                    //DataRow dr2 = dt1.NewRow();
                    //dr2[0] = "Total Sales ";
                    //dr2[1] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString());
                    //dt1.Rows.Add(dr2);



                    ////Total cost
                    //DataRow lnTC = dt1.NewRow();
                    //lnTC[0] = "Total Cost: ";
                    //lnTC[4] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString());
                    //dt1.Rows.Add(lnTC);

                    //DataRow dr8 = dt1.NewRow();
                    //dr8[0] = "______ ";
                    //dt1.Rows.Add(dr8);


                    ////Total  Sales
                    //DataRow dr4 = dt1.NewRow();
                    //dr4[0] = "Total  Sales:";
                    //dr4[4] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString());
                    //dt1.Rows.Add(dr4);

                    DataRow dr6 = dt1.NewRow();
                    dr6[0] = " ";
                    dt1.Rows.Add(dr6);

                    //Totla Profit
                    DataRow dr5 = dt1.NewRow();
                    dr5[0] = "Total Profit :";
                    dr5[5] = Convert.ToDouble(dt3.Rows[0].ItemArray[1].ToString()); //Convert.ToDouble(sum.ToString()); //
                    dt1.Rows.Add(dr5);

                    //DataRow dr8 = dt1.NewRow();
                    //dr8[0] = "Total Due ";
                    //dr8[3] = dt3.Rows[0].ItemArray[2].ToString();
                    //dt1.Rows.Add(dr8);

                    //DataRow dr17 = dt1.NewRow();
                    //dr17[0] = " ";
                    //dt1.Rows.Add(dr17);

                    //DataRow dr9 = dt1.NewRow();
                    //dr9[0] = "Total in Cash ";
                    //dr9[1] = Convert.ToDouble(dt3.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dt3.Rows[0].ItemArray[2].ToString());
                    //dt1.Rows.Add(dr9);

                    DataRow dr7 = dt1.NewRow();
                    dr7[0] = "______ ";
                    dt1.Rows.Add(dr7);

                    DataRow rep = dt1.NewRow();
                    rep[0] = "Sales Report ";
                    dt1.Rows.Add(rep);



                    DataRow repdt = dt1.NewRow();
                    repdt[0] = "From : " + dtSalesItemStart.Text;
                    repdt[1] = "To : " + dtSalesItemEND.Text;
                    dt1.Rows.Add(repdt);
                }
                catch
                {
                    // MessageBox.Show("There is no Data in this date");
                }
            }
        }
    }
}
