using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using PointofSale.DAL;

namespace PointofSale
{
    public partial class ItemBulkUpload : KryptonForm
    {
        public ItemBulkUpload()
        {
            InitializeComponent();
        }

        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        // private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.8.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel10ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public string categoryID
        {
            set { lblRows.Text = value; }
            get { return lblRows.Text; }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                lblRows.Text = "Total ID = " + grdCategory.RowCount.ToString();
            }
            catch (Exception xe)
            {

                MessageBox.Show(xe.Message);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void txtReferNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void categorybind()
        {
            string sql = " select ID, category_name as 'Category' from tbl_category ";
            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdCategory.DataSource = dt1;
        }

        private void addCategory_Load(object sender, EventArgs e)
        {
            
        }

        private void grdCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            string extension = Path.GetExtension(filePath);
            string header = rbHeaderYes.Checked ? "YES" : "NO";
            string conStr, sheetName;

            conStr = string.Empty;
            switch (extension)
            {

                case ".xls": //Excel 97-03
                    conStr = string.Format(Excel03ConString, filePath, header);
                    break;

                case ".xlsx": //Excel 07
                    conStr = string.Format(Excel10ConString, filePath, header);
                    break;

                case ".csv": //Excel 07
                    conStr = string.Format(Excel10ConString, filePath, header);
                    break;
            }

            //Get the name of the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    con.Close();
                }
            }

            //Read Data from the First Sheet.
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    using (OleDbDataAdapter oda = new OleDbDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        cmd.CommandText = "SELECT * From [" + sheetName + "]";
                        cmd.Connection = con;
                        con.Open();
                        oda.SelectCommand = cmd;
                        oda.Fill(dt);
                        con.Close();

                        //Populate DataGridView.
                        grdCategory.DataSource = dt;
                        btnSave.Enabled = true;
                    }
                }
            }
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            lblwaiting.Text = "Please Wait ...";
            try
            {

                int rows = grdCategory.Rows.Count;
                for (int i = 0; i < rows; i++)
                {
                    string product_id = grdCategory.Rows[i].Cells[0].Value.ToString();
                    string product_name = grdCategory.Rows[i].Cells[1].Value.ToString();
                    double product_quantity = Convert.ToDouble(grdCategory.Rows[i].Cells[2].Value.ToString());
                    double cost_price = Convert.ToDouble(grdCategory.Rows[i].Cells[3].Value.ToString());
                    double retail_price = Convert.ToDouble(grdCategory.Rows[i].Cells[4].Value.ToString());
                    double total_cost_price = cost_price * product_quantity;
                    double total_retail_price = retail_price * product_quantity;
                    string category = grdCategory.Rows[i].Cells[5].Value.ToString();
                    string supplier = grdCategory.Rows[i].Cells[6].Value.ToString();
                    string imagename = product_id + ".png"; // dtgridviewImportPreview.Rows[i].Cells[7].Value.ToString();
                    double discount = Convert.ToDouble(grdCategory.Rows[i].Cells[7].Value.ToString());
                    int taxapply = Convert.ToInt32(grdCategory.Rows[i].Cells[8].Value.ToString());
                    string Shopid = grdCategory.Rows[i].Cells[9].Value.ToString();
                    int kitchendisplay = Convert.ToInt32(grdCategory.Rows[i].Cells[10].Value.ToString());


                    string CheckItem = "Select * FROM purchase WHERE product_id = '"+ product_id + "'";
                    DataTable DSitemCheck = DAL.DataAccessManager.GetDataTable(CheckItem);
                    if (DSitemCheck.Rows.Count > 0)
                    {
                        string UpdateQty = "UPDATE [purchase] SET [product_quantity] = '"+product_quantity+"' WHERE [product_id] = '"+product_id+"'";
                        DAL.DataAccessManager.ExecuteSQL(UpdateQty);
                        insertpurchasehistory(product_id, product_name, product_quantity, cost_price, retail_price, category, supplier, Shopid);
                    }
                    else { 


                        string sqlCmd = " insert into purchase (product_id , product_name , product_quantity , cost_price , retail_price , total_cost_price , " +
                                        " total_retail_price , category , supplier , imagename, discount, taxapply, Shopid, status ) " +
                                        "  values ('" + product_id + "' , '" + product_name + "', '" + product_quantity + "', '" + cost_price + "' , '" + retail_price + "' , " +
                                        "  '" + total_cost_price + "' , '" + total_retail_price + "' , '" + category + "', '" + supplier + "',0xFFD8FFE000104A46494600010101006000600000FFE1004E4578696600004D4D002A00000008000403010005000000010000003E51100001000000010100000051110004000000010000000051120004000000010000000000000000000186A00000B18FFFDB004300080606070605080707070909080A0C140D0C0B0B0C1912130F141D1A1F1E1D1A1C1C20242E2720222C231C1C2837292C30313434341F27393D38323C2E333432FFDB0043010909090C0B0C180D0D1832211C213232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232FFC00011080080008003012200021101031101FFC4001F0000010501010101010100000000000000000102030405060708090A0BFFC400B5100002010303020403050504040000017D01020300041105122131410613516107227114328191A1082342B1C11552D1F02433627282090A161718191A25262728292A3435363738393A434445464748494A535455565758595A636465666768696A737475767778797A838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE1E2E3E4E5E6E7E8E9EAF1F2F3F4F5F6F7F8F9FAFFC4001F0100030101010101010101010000000000000102030405060708090A0BFFC400B51100020102040403040705040400010277000102031104052131061241510761711322328108144291A1B1C109233352F0156272D10A162434E125F11718191A262728292A35363738393A434445464748494A535455565758595A636465666768696A737475767778797A82838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE2E3E4E5E6E7E8E9EAF2F3F4F5F6F7F8F9FAFFDA000C03010002110311003F00F9FE8A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A00FFFD9, " +
                                        " '" + discount + "' , '" + taxapply + "' , '" + Shopid + "', '" + kitchendisplay + "')";
                        DAL.DataAccessManager.ExecuteSQL(sqlCmd);

                        //Same time Purchase history insert
                        insertpurchasehistory(product_id, product_name, product_quantity, cost_price, retail_price, category, supplier, Shopid);


                        //Serial image upload
                        //string path = Application.StartupPath + @"\ITEMIMAGE\";
                        //string filename = path + @"\" + picItemimage.Image;
                        //picItemimage.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        //System.IO.File.Move(path + @"\" + picItemimage.Image , path + @"\" + imagename);

                        ///  MessageBox.Show("Successfully Added ");
                        btnSave.Enabled = false;
                        lblmsg.Text = "Successfully Added Bulk items and purchase history record | Total Records :" + i;
                        lblwaiting.Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Sorry\r\n this id already added \n Duplicate value \n  " + exp.Message);
            }


        }

        public void insertpurchasehistory(string pid, string pname, double pQty, double cprice, double sprice, string category, string supplier, string shopid)
        {
            try
            {
                string pdate = DateTime.Now.ToString("yyyy-MM-dd");
                string sql1 = " insert into tbl_purchase_history (product_id, product_name, product_quantity, cost_price, retail_price, category, " +
                                " supplier, purchase_date, Shopid, ptype ) " +
                                " values ('" + pid + "', '" + pname + "', '" + pQty + "', '" + cprice + "', '" + sprice + "', '" + category + "', " +
                                "  '" + supplier + "', '" + pdate + "' ,'" + shopid + "', 'NEW' )";
                DAL.DataAccessManager.ExecuteSQL(sql1);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // System.Diagnostics.Process.Start("Calc");
                // SendKeys.SendWait(lblTotal.Text);
                Process p = new Process();
                p.StartInfo.FileName = "items.xls";
                p.Start();
                p.WaitForInputIdle();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
