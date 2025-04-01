using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PointofSale
{
    public partial class Purchase_History : KryptonForm
    {
        public Purchase_History()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void txtReferNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = " select  product_id as 'Item Code' , product_name as 'Item Name' , product_quantity as 'Quantity',  " +
                             " cost_price as 'Buy Price' , retail_price as 'Sales Price' ,   category as 'Category' , supplier as 'Supplier'   " +
                             " from purchase where product_id like  '" + txtSearch.Text + "%' or product_name like '" + txtSearch.Text + "%' or " +
                             " category like '" + txtSearch.Text + "%' or supplier like '" + txtSearch.Text + "%'";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdStockList.DataSource = dt1;
                lblRow.Text = "Total item :" + grdStockList.Rows.Count.ToString();
            }
            catch
            {
            }
        }

        private void stockList_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = " select  product_id as 'Item Code' , product_name as 'Item Name' , product_quantity as 'Quantity',  " +
                             " cost_price as 'Buy Price' , retail_price as 'Sales Price' ,   category as 'Category' , supplier as 'Supplier'   " +
                             " from purchase order by product_quantity ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdStockList.DataSource = dt1;
                lblRow.Text = "Total items :" + grdStockList.Rows.Count.ToString();

                //~ double totalqty = 0;
                //~ for (int i = 0; i < datagridItemList.Rows.Count; ++i)
                //~ {
                //~ totalqty += Convert.ToDouble(datagridItemList.Rows[i].Cells[2].Value);
                //~ }
                //~ lblRow.Text  = totalqty.ToString();
            }
            catch
            {
            }
        }

        private void grdStockList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(grdStockList.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void grdStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in grdStockList.Rows)
            {
                // if less than equal 0 Red alter 
                if (Convert.ToDouble(Myrow.Cells[2].Value) <= 0)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Red;
                    Myrow.DefaultCellStyle.ForeColor = Color.Lime;
                }
                // if less than  10 yellow alarming 
                else if (Convert.ToDouble(Myrow.Cells[2].Value) < 10)
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Yellow;
                    Myrow.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    Myrow.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "StockDetails_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            foreach (DataGridViewColumn column in grdStockList.Columns)
            {
                csv += column.HeaderText + ',';
            }

            //Add new line.
            csv += "\r\n";

            //Adding the Rows
            foreach (DataGridViewRow row in grdStockList.Rows)
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
            string fileName = "StockDetails_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + ".csv";
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location: 
            // Create a new target folder, if necessary. 
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);

            }

            // Get file name.
            string name = saveFileDialog1.FileName;
            File.WriteAllText(name, csv);
        }
    }
}
