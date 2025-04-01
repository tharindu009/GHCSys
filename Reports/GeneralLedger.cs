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
    public partial class GeneralLedger : KryptonForm
    {
        public GeneralLedger()
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
            MyDataGridViewPrinter = new DataGridViewPrinter(dtGrdLedgerReport,
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
            foreach (DataGridViewColumn column in dtGrdLedgerReport.Columns)
            {
                csv += column.HeaderText + ',';
            }

            //Add new line.
            csv += "\r\n";

            //Adding the Rows
            foreach (DataGridViewRow row in dtGrdLedgerReport.Rows)
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
                this.dtGrdLedgerReport.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtGrdLedgerReport.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

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
            ReportByDate(dtStartDate.Text, dtEndDate.Text);
        }


        public void ReportByDate(string StartDate, string EndDate)
        {
            try
            {
                string sqlCmd = "Select * from  vw_general_ledger where  Date BETWEEN '" + StartDate + "' AND    '" + EndDate + "'   order by Date desc ";
                DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                dtGrdLedgerReport.DataSource = dt1;

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

        public void Databind()
        {
            string sqlCmd = "SELECT * from vw_general_ledger  order by Date desc";
            DAL.DataAccessManager.ExecuteSQL(sqlCmd);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
            dtGrdLedgerReport.DataSource = dt1;

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
            // Balance[2] = dtSUM.Rows[0].ItemArray[1].ToString();
            dt1.Rows.Add(Balance);
        }

        private void kryptonButton2_Click_2(object sender, EventArgs e)
        {
            try
            {
                this.dtGrdLedgerReport.RowsDefaultCellStyle.BackColor = Color.White;
                this.dtGrdLedgerReport.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

                if (SetupThePrinting())
                {
                    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
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
                Databind();

                dtStartDate.Format = DateTimePickerFormat.Custom;
                dtStartDate.CustomFormat = "yyyy-MM-dd";

                dtEndDate.Format = DateTimePickerFormat.Custom;
                dtEndDate.CustomFormat = "yyyy-MM-dd";
            }
            catch
            {
            }
        }
    }
}
