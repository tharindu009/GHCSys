using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Reporting.WinForms;
using QRCoder;

namespace PointofSale
{
    public partial class QRcode : KryptonForm
    {
        public QRcode()
        {
            InitializeComponent();
        }

        string CustomerID = "None";

        public QRcode(string CusID)
        {
            InitializeComponent();
            CustomerID = CusID;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            string qrText = txtCustomerName.Text;
             
            string qrText1 = "http://autochat.somee.com/Service/VehicleDetail/5?VehicelReg=" + txtVehicleReg.Text;
            byte[] qrCodeImage = GenerateQRCode(qrText1);

            DataTable dt = new DataTable();
            dt.Columns.Add("Image", typeof(byte[]));
            dt.Rows.Add(qrCodeImage);

            ReportDataSource rds = new ReportDataSource("ReportData", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            //reportViewer1.LocalReport.ReportPath = "QRRpt.rdlc";
            reportViewer1.RefreshReport();

        }


        private byte[] GenerateQRCode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void addNewCustomer_Load(object sender, EventArgs e)
        {
            if (CustomerID != "None")
            {
                lblCustID.Text = CustomerID;
                string CusDetail = "SELECT [Name],[Phone],[City],[ID] FROM [tbl_customer] WHERE [ID] = '" + lblCustID.Text + "'";
                DataTable dtCusDetail = DAL.DataAccessManager.GetDataTable(CusDetail);
                txtCustomerName.Text = dtCusDetail.Rows[0]["Name"].ToString();
                pnlCustomer.Visible = false;
                LoadVehicleDetail(lblCustID.Text);
                pnlVehicle.Visible = true;

            }
            this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.EnableExternalImages = true;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            string CusDetail = "SELECT [Name],[Phone],[City],[ID] FROM [tbl_customer] WHERE [Name] LIKE '%" + txtCustomerName.Text + "%' OR [Phone] like '%" + txtCustomerName.Text + "%' OR [City] like '%" + txtCustomerName.Text + "%'";
            DataTable dtCusDetail = DAL.DataAccessManager.GetDataTable(CusDetail);
            grdCustomer.DataSource = dtCusDetail;
            if (dtCusDetail.Rows.Count == 0)
            {
                //lnkAddnewCustomer.Visible = true;
            }
            else
            {
                //lnkAddnewCustomer.Visible = false;
            }
        }

        private void grdCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdCustomer.Rows[e.RowIndex];
                lblCustomer.Text = row.Cells[0].Value.ToString();
                //txtRegNo.Text = row.Cells[2].Value.ToString();
                //txtVehicleType.Text = row.Cells[3].Value.ToString();
                //txtVModel.Text = row.Cells[4].Value.ToString();        
                lblCustID.Text = row.Cells["ID"].Value.ToString();
                txtCustomerName.Text = lblCustomer.Text;
                pnlCustomer.Visible = false;
                LoadVehicleDetail(lblCustID.Text);
                pnlVehicle.Visible = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadVehicleDetail(string CusID)
        {
            string VehicleSQl = "SELECT [RegNo],[Make],[Model],[Year],[CustomerID] FROM [dbo].[tblVehicle] WHERE CustomerID='" + CusID + "'";
            DataTable dtVehicle = DAL.DataAccessManager.GetDataTable(VehicleSQl);
            grdVehicle.DataSource = dtVehicle;
            if (grdVehicle.Rows.Count == 0)
            {
                lblveh.Visible = true;
            }
        }

        private void grdVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdVehicle.Rows[e.RowIndex];
                txtVehicleReg.Text = row.Cells[0].Value.ToString();      
                pnlVehicle.Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton14_Click(object sender, EventArgs e)
        {
            pnlVehicle.Visible = false;
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
        }
    }
}
