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
    public partial class ProftLostReport : KryptonForm
    {
        public ProftLostReport()
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
            MyDataGridViewPrinter = new DataGridViewPrinter(dtgrdViewProfitLoss,
            printDocument1, true, true, Header + " Sales Report \n", new Font("Baskerville Old Face", 13, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);


            //else

            //    MyDataGridViewPrinter = new DataGridViewPrinter(dtgrdViewSalesReport,
            //    printDocument1, false, true, Header + "   Sales Report   \n", new Font("Times New Roman", 14, FontStyle.Regular, GraphicsUnit.Point), Color.Black, true);

            return true;
        }


        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }



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

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.dtgrdViewProfitLoss.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtgrdViewProfitLoss.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

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
            //ReportByDate(dtStartDate.Text, dtEndDate.Text);
        }


        public void ReportByDate(string StartDate, string EndDate)
        {
            try
            {
                string sqlCmd = "Select * from  vw_general_ledger where  Date BETWEEN '" + StartDate + "' AND    '" + EndDate + "'   order by Date desc ";
                DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                dtgrdViewProfitLoss.DataSource = dt1;

                string sqlSUM = "SELECT   Sum(Credit), Sum(Debit) from vw_general_ledger";
                DAL.DataAccessManager.ExecuteSQL(sqlSUM);
                DataTable dtSUM = DAL.DataAccessManager.GetDataTable(sqlSUM);

                DataRow dr = dt1.NewRow();
                dr[0] = "______________________________________________ ";
                dt1.Rows.Add(dr);

                DataRow Total = dt1.NewRow();
                Total[0] = "Total = ";
                Total[1] = dtSUM.Rows[0].ItemArray[0].ToString();
                Total[2] = dtSUM.Rows[0].ItemArray[1].ToString();
                dt1.Rows.Add(Total);

                DataRow Balance = dt1.NewRow();
                Balance[0] = "Balance = ";
                Balance[1] = Convert.ToDouble(dtSUM.Rows[0].ItemArray[0].ToString()) - Convert.ToDouble(dtSUM.Rows[0].ItemArray[1].ToString());
                dt1.Rows.Add(Balance);

                DataRow dr3 = dt1.NewRow();
                dr3[0] = "______________________________________________ ";
                dt1.Rows.Add(dr3);
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
                this.dtgrdViewProfitLoss.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtgrdViewProfitLoss.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                if (SetupThePrinting())
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                    MyPrintPreviewDialog.WindowState = FormWindowState.Maximized;
                    MyPrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                    MyPrintPreviewDialog.Document = printDocument1;
                    MyPrintPreviewDialog.ShowDialog();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("!!! Please Print Preview or Setup Print only for First time " + exp.Message);
            }
        }

        private void GeneralLedger_Load(object sender, EventArgs e)
        {
            try
            {
                //Databind();
                pnlReportDate.Visible = true;
                dtStartDate.Format = DateTimePickerFormat.Custom;
                dtStartDate.CustomFormat = "yyyy-MM-dd";

                dtEndDate.Format = DateTimePickerFormat.Custom;
                dtEndDate.CustomFormat = "yyyy-MM-dd";
            }
            catch
            {
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ReportValue.StartDate = dtStartDate.Text; // dtStartDate.Value.ToShortDateString();
            ReportValue.EndDate = dtEndDate.Text;

            pnlReportDate.Visible = false;
            GenerateReport();
        }

        private void GenerateReport()
        {
            try
            {

                // dtgrdViewProfitLoss.Refresh(); //.Columns.Clear();
                this.dtgrdViewProfitLoss.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtgrdViewProfitLoss.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                dtgrdViewProfitLoss.ColumnCount = 3;


                string sql3 = " select SUM(Total) ,  SUM((( profit + ((RetailsPrice  * discount) / 100.00)) * Qty)) as Profit     from sales_item  " +
                    " where sales_time   >='" + ReportValue.StartDate + "' AND  sales_time <='" + ReportValue.EndDate + "' ";
                DAL.DataAccessManager.ExecuteSQL(sql3);
                DataTable dt3 = DAL.DataAccessManager.GetDataTable(sql3);
                string Totalsales = dt3.Rows[0].ItemArray[0].ToString();
                string grossprofit = dt3.Rows[0].ItemArray[1].ToString();


                string[] row = new string[] { "  ", "Profit Loss Report", " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Date Between ", ReportValue.StartDate.ToString(), ReportValue.EndDate };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "_______________________", "__________________", "___________________" };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { " ", " ", " " };
                dtgrdViewProfitLoss.Rows.Add(row);


                string sqlPayment = " select SUM(payment_amount), SUM(dis), SUM(vat) , SUM(due_amount)  from sales_payment " +
                                  " where sales_time   >='" + ReportValue.StartDate + "' AND  sales_time <='" + ReportValue.EndDate + "' ";
                DAL.DataAccessManager.ExecuteSQL(sqlPayment);
                DataTable dtPayment = DAL.DataAccessManager.GetDataTable(sqlPayment);

                string totalpaidbycustomer = dtPayment.Rows[0].ItemArray[0].ToString(); // total paid by customer with vat
                string dis = dtPayment.Rows[0].ItemArray[1].ToString();
                string vat = dtPayment.Rows[0].ItemArray[2].ToString();
                string due = dtPayment.Rows[0].ItemArray[3].ToString();
                double salesminusdis = Convert.ToDouble(Totalsales) - Convert.ToDouble(dis);
                string totalcost = (salesminusdis - Convert.ToDouble(grossprofit)).ToString();

                row = new string[] { "Sub Total ", Totalsales, " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Total Discount ", dis, " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Total Sales after discount ", salesminusdis.ToString(), " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Total TAX ", vat, " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Total Due Amount ", due, " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { " ", " ", " " };
                dtgrdViewProfitLoss.Rows.Add(row);

                //  row = new string[] { "Total buy Cost ", totalcost, " " };
                //  dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { "Total Sales ", totalpaidbycustomer, " " };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { " ", " ", " " };
                dtgrdViewProfitLoss.Rows.Add(row);


                double Netprofit = Convert.ToDouble(grossprofit) - Convert.ToDouble(dis);
                row = new string[] { "Net profit ", "After discount ", Netprofit.ToString() };
                dtgrdViewProfitLoss.Rows.Add(row);
                row = new string[] { " ", " ", " " };
                dtgrdViewProfitLoss.Rows.Add(row);

                //Return  Start
                string sqlReturn = " select SUM(Total) , SUM(disamt), SUM(vatamt)  from return_item " +
                                 " where return_time   >='" + ReportValue.StartDate + "' AND  return_time <='" + ReportValue.EndDate + "' ";
                DAL.DataAccessManager.ExecuteSQL(sqlReturn);
                DataTable dtReturn = DAL.DataAccessManager.GetDataTable(sqlReturn);
                double totalreturnedamt = 0.0;
                if (dtReturn.Rows.Count > 1)
                {
                    double totalreturn = Convert.ToDouble(dtReturn.Rows[0].ItemArray[0].ToString());
                    double totaldis = Convert.ToDouble(dtReturn.Rows[0].ItemArray[1].ToString());
                    double totalvat = Convert.ToDouble(dtReturn.Rows[0].ItemArray[2].ToString());
                    totalreturnedamt = (totalreturn - totaldis) + totalvat;


                    row = new string[] { "Total Return ", totalreturnedamt.ToString(), " " };
                    dtgrdViewProfitLoss.Rows.Add(row);
                }
                //// Return END

                //Expenses Start                
                string sqlExpenses = " select SUM(Amount)   from tbl_expense " +
                                 " where Date   >='" + ReportValue.StartDate + "' AND  Date <='" + ReportValue.EndDate + "' ";
                DAL.DataAccessManager.ExecuteSQL(sqlExpenses);
                DataTable dtExpenses = DAL.DataAccessManager.GetDataTable(sqlExpenses);
                if (dtExpenses.Rows.Count > 0)
                {
                    double totalExpenses = Convert.ToDouble(dtExpenses.Rows[0].ItemArray[0].ToString());
                    row = new string[] { "Total Expenses ", totalExpenses.ToString(), " " };
                    dtgrdViewProfitLoss.Rows.Add(row);
                    // Expenses END

                    double incash = (Convert.ToDouble(totalpaidbycustomer) - Convert.ToDouble(due)) - Convert.ToDouble(dis) - totalreturnedamt - totalExpenses;
                    row = new string[] { " ", " ", " " };
                    dtgrdViewProfitLoss.Rows.Add(row);
                    row = new string[] { "In cash ", incash.ToString(), " " };
                    dtgrdViewProfitLoss.Rows.Add(row);
                    row = new string[] { " ", " ", " " };
                    dtgrdViewProfitLoss.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "ProfitLossReport_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            saveFileDialog1.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
