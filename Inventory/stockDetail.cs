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
using Microsoft.Azure.Pipelines.WebApi;

namespace PointofSale
{
    public partial class stockDetail : KryptonForm
    {
        public stockDetail()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void txtReferNo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void stockList_Load(object sender, EventArgs e)
        {

            try
            {
                ItemList_with_images("");
            }
            catch
            {
            }
        }


        #region Data bind
        //Show Products image
        public void ItemList_with_images(string value)
        {
            flowLayoutPanelUserList.Controls.Clear();
            //string img_directory = Application.StartupPath + @"\ITEMIMAGE\";
            //string[] files = Directory.GetFiles(img_directory, "*.png *.jpg");
            try
            {
                //string sql = "select * from purchase where  ( LOWER(product_name) like LOWER('" + value + "%') ) " +
                //" OR ( product_id like '" + value + "%' ) " +
                //" OR (LOWER(category) = LOWER('" + value + "')) order by product_quantity ";

                //DAL.DataAccessManager.ExecuteSQL(sql);
                DataSet ds = DAL.register.BindItemStockList(value);
                DataTable dt = ds.Tables[0];
                //DataTable dt = DAL.DataAccessManager.GetDataTable(sql);
                lblRows.Text = "Total Rows " + dt.Rows.Count.ToString() + " Found";

                int currentImage = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataReader = dt.Rows[i];

                    Button b = new Button();
                    //Image i = Image.FromFile(img_directory + dataReader["name"]);
                    b.Tag = dataReader["product_id"];
                    b.Click += new EventHandler(b_Click);

                    string taxapply;
                    if (dataReader["taxapply"].ToString() == "1")
                    {
                        taxapply = "YES";
                    }
                    else
                    {
                        taxapply = "NO";
                    }

                    string KitchenDisplay;
                    if (dataReader["status"].ToString() == "3")
                    {
                        KitchenDisplay = "YES";
                    }
                    else
                    {
                        KitchenDisplay = "NO";
                    }

                    string Qty = dataReader["product_quantity"].ToString();
                    //double intQty = Convert.ToDouble(dataReader["product_quantity"].ToString());
                    //if(intQty == 0.00)
                    //{
                    //    b.BackColor = Color.MediumVioletRed;
                    //}
                    //if(intQty > 0.00 && intQty < 5.00)
                    //{
                    //    b.BackColor = Color.Yellow;
                    //}

                    string details =
                        "====================================" +
                        "\n ID: " + dataReader["product_id"] +
                        "\n Name: " + dataReader["product_name"].ToString() +
                        "\n Buy price: " + dataReader["cost_price"].ToString() +
                        "\n Stock Qty: " + dataReader["product_quantity"].ToString() +
                        "\n Retail price: " + Qty +
                        "\n Discount: " + dataReader["discount"].ToString() + "%" +
                        "\n Category: " + dataReader["category"].ToString() +
                        "\n Supplier: " + dataReader["supplier"].ToString() +
                        "\n Branch: " + dataReader["Shopid"].ToString() +
                        "\n Tax Apply: " + taxapply +
                        "\n Kitchen Display  : " + KitchenDisplay +
                        "\n ====================================";
                    b.Name = details;
                    toolTip1.ToolTipTitle = "Item Details";
                    toolTip1.AutoPopDelay = 32766;
                    toolTip1.SetToolTip(b, details);


                    //byte[] MyData = new byte[0];
                    //MyData = (byte[])dataReader["imagename"];
                    //MemoryStream stream = new MemoryStream(MyData);

                    //pictureBox2.Image = Image.FromStream(stream);

                    ImageList il = new ImageList();
                    il.ColorDepth = ColorDepth.Depth32Bit;
                    il.TransparentColor = Color.Transparent;
                    il.ImageSize = new Size(80, 80);
                    //il.Images.Add(Image.FromFile(img_directory + dataReader["imagename"]));
                    string strCategory = dataReader["category"].ToString();
                    if (strCategory == "Service")
                    {
                        il.Images.Add(PointofSale.Properties.Resources.service);
                    }
                    if (strCategory.IndexOf("oil", 0, StringComparison.OrdinalIgnoreCase) != -1 && strCategory.IndexOf("filter", 0, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        il.Images.Add(PointofSale.Properties.Resources.oil);
                    }
                    if (strCategory == "Air Filter")
                    {
                        il.Images.Add(PointofSale.Properties.Resources.airfilter);
                    }
                    if (strCategory == "Oil Filter")
                    {
                        il.Images.Add(PointofSale.Properties.Resources.oilfilter);
                    }
                    else
                    {
                        il.Images.Add(PointofSale.Properties.Resources.product);
                    }

                    //il.Images.Add(Image.FromStream(stream));

                    //if (dataReader["imagename"].ToString() == img_directory + dataReader["imagename"])
                    //{
                    //    //8940000000002.png

                    //}
                    //else
                    //{
                    //    il.Images.Add(Image.FromFile(img_directory + "/8940000000002.png"));    
                    //}





                    b.Image = il.Images[0];
                    b.Margin = new Padding(3, 3, 3, 3);

                    b.Size = new Size(220, 100);
                    b.Text.PadRight(4);

                    b.Text += " " + dataReader["product_id"] + "\n ";
                    b.Text += dataReader["product_name"].ToString();
                    b.Text += "\n Buy: " + dataReader["cost_price"];
                    if (strCategory != "Service")
                    {
                        b.Text += "\n Stock: " + dataReader["product_quantity"];
                    }
                    b.Text += "\n R.Price: " + dataReader["retail_price"];
                    b.Text += "\n Dis: " + dataReader["discount"] + "% Tax: " + taxapply;

                    b.Font = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
                    b.TextAlign = ContentAlignment.TopLeft;
                    b.TextImageRelation = TextImageRelation.ImageBeforeText;
                    flowLayoutPanelUserList.Controls.Add(b);
                    currentImage++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        protected void b_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string s;
            s = b.Tag.ToString();

            
            
            addItem regQ = new addItem();
            //regQ.TopLevel = false;
            //regQ.FormBorderStyle = FormBorderStyle.None;
            //regQ.Dock = DockStyle.Fill;
            //panel1.Controls.Add(regQ);
            //panel1.Tag = regQ;
            regQ.itemCode = s;
            //regQ.BringToFront();
            //regQ.Show();
            //tabControl1.SelectTab(tabPage2);

            //saleRegister regQ = new saleRegister(JobNo);
            regQ.MdiParent = Application.OpenForms["dashboard"];
            regQ.FormBorderStyle = FormBorderStyle.FixedSingle;
            regQ.WindowState = FormWindowState.Maximized;

            regQ.Show();



        }
        #endregion

        private void grdStockList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //using (SolidBrush b = new SolidBrush(grdStockList.RowHeadersDefaultCellStyle.ForeColor))
            //{
            //    e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            //}
        }

        private void grdStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ItemList_with_images(txtSearch.Text);
        }

        private void kryptonButton2_Click_2(object sender, EventArgs e)
        {
            //BarcodeMachine regQ = new BarcodeMachine();
            //regQ.TopLevel = false;
            //regQ.FormBorderStyle = FormBorderStyle.None;
            //regQ.Dock = DockStyle.Fill;
            //panel1.Controls.Add(regQ);
            //panel1.Tag = regQ;
            //regQ.BringToFront();
            //regQ.Show();
            //tabControl1.SelectTab(tabPage2);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemList_with_images("");
        }
    }
}
