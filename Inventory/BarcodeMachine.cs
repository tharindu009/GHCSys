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
    public partial class BarcodeMachine : KryptonForm
    {
        public BarcodeMachine()
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
                string sql5 = "select    product_id  from purchase ";
                DAL.DataAccessManager.ExecuteSQL(sql5);
                DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
                cmbitems.DataSource = dt5;
                cmbitems.DisplayMember = "product_id";

            }
            catch
            {
            }
        }

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

        }

        private void btnCreatebarcode_Click(object sender, EventArgs e)
        {
            this.barCodeControl1.TopText = "\n" + txtcompany.Text + "\n ================= \n" + txttoptext.Text + "\n\nPrice: " + txtCurrency.Text + txtPrice.Text;
            this.barCodeControl1.SupSpace = 22;



            //generate the barcode use the settings
            BarCodeGenerator generator = new BarCodeGenerator(barCodeControl1);
            Image barcode = generator.GenerateImage();

            this.barCodeControl1.SaveToFile("barcodeImage.png");
            btnSaveimage.Enabled = true;

            string barcodeimagestore = Application.StartupPath + @"\barcodeImage.png";
            //save the barcode as an image
            // barcode.Save(@"..\..\..\BarCode\barcode.png");
            //   barcode.Save(barcodeimagestore);
            //launch the generated barcode image            
            //   string path = "..\\..\\..\\BarCode\\barcode.png";

            picbarcode1.ImageLocation = barcodeimagestore;
            picbarcode2.ImageLocation = barcodeimagestore;
            picbarcode3.ImageLocation = barcodeimagestore;
            picbarcode4.ImageLocation = barcodeimagestore;
            picbarcode5.ImageLocation = barcodeimagestore;
            picbarcode6.ImageLocation = barcodeimagestore;

        }
    }
}
