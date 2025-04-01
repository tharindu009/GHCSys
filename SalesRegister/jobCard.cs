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

namespace PointofSale
{
    public partial class jobCard : KryptonForm
    {
        public jobCard()
        {
            InitializeComponent();
        }

        string OilFilter = "None";
        string AirFilter = "None";
        string FuelFilter = "None";
        string CabinFilter = "None";
        string DiifOil = "None";
        string EngOil = "None";
        string GearOil = "None";

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void jobCard_Load(object sender, EventArgs e)
        {
            BindEngineOil();
            BindAirFilter();
            BindCabinFilter();
            BindFuelFilter();
            BindOilFilter();
            generate_inv();
            BindCurrentJobs();
            ClearField();
            BindDiffOil();
            BindGearOil();
            BindCustomer();
            cmbDiff.Visible = false;
            ComboCategory.Visible = false;
        }

        private void BindCustomer()
        {
            string sqlCust = "select   DISTINCT  *   from tbl_customer where PeopleType = 'Customer'";
            DAL.DataAccessManager.ExecuteSQL(sqlCust);
            DataTable dtCust = DAL.DataAccessManager.GetDataTable(sqlCust);
            comboBox1.DataSource = dtCust;
            comboBox1.DisplayMember = "Name";
            comboBox1.Text = "Guest";
        }

        private void BindEngineOil()
        {
            string OilQuery = @"SELECT product_id,product_name FROM purchase where category = 'Engine Oil' and product_quantity > 0 order by product_id ASC";

            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilQuery);

            ComboCategory.DataSource = dtEO;
            ComboCategory.DisplayMember = "product_name";
            ComboCategory.ValueMember = "product_id";
        }

        private void BindGearOil()
        {
            string OilQuery = @"SELECT product_id,product_name FROM purchase where category = 'Gear Oil and product_quantity > 0'";

            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilQuery);

            cmbGearOil.DataSource = dtEO;
            cmbGearOil.DisplayMember = "product_name";
            cmbGearOil.ValueMember = "product_id";
        }

        private void BindOilFilter()
        {
            string OilFilterQuery = @"SELECT product_id,product_name FROM purchase where category = 'Oil Filter' and product_quantity > 0";
            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilFilterQuery);

            cmbEngFilter.DataSource = dtEO;
            cmbEngFilter.DisplayMember = "product_name";
            cmbEngFilter.ValueMember = "product_id";
        }

        private void BindFuelFilter()
        {
            string OilFilterQuery = @"SELECT product_id,product_name FROM purchase where category = 'Fuel Filter' and product_quantity > 0";
            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilFilterQuery);

            cmbFuelFilter.DataSource = dtEO;
            cmbFuelFilter.DisplayMember = "product_name";
            cmbFuelFilter.ValueMember = "product_id";
        }


        private void BindAirFilter()
        {
            string OilFilterQuery = @"SELECT product_id,product_name FROM purchase where category = 'Air Filter' and product_quantity > 0";
            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilFilterQuery);

            cmbAirFilter.DataSource = dtEO;
            cmbAirFilter.DisplayMember = "product_name";
            cmbAirFilter.ValueMember = "product_id";
        }


        private void BindCabinFilter()
        {
            string OilFilterQuery = @"SELECT product_id,product_name FROM purchase where category = 'Cabin Filter' and product_quantity > 0";
            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilFilterQuery);

            cmbCabinFilter.DataSource = dtEO;
            cmbCabinFilter.DisplayMember = "product_name";
            cmbCabinFilter.ValueMember = "product_id";
        }

        private void BindCurrentJobs()
        {
            string JobQuery = @"SELECT JobNo,VehicleNo,Model,Mileage,EngineOil,GearOil,OilFilter,AirFilter,FuelFilter,ATFFilter,CabinFilter,DiffOil,Other,JobDate,Status
                                FROM job_card
                                where CONVERT(varchar,[JobDate],112)  = CONVERT(varchar,getdate(),112) ";

            DataTable dtJob = DAL.DataAccessManager.GetDataTable(JobQuery);
            dgrvSalesItemList.DataSource = dtJob;
        }

        private void BindDiffOil()
        {
            string OilFilterQuery = @"SELECT product_id,product_name FROM purchase where category = 'Differential Oil'";
            DataTable dtEO = DAL.DataAccessManager.GetDataTable(OilFilterQuery);

            cmbDiff.DataSource = dtEO;
            cmbDiff.DisplayMember = "product_name";
            cmbDiff.ValueMember = "product_id";
        }

        private void ClearField()
        {
            txtMilage.Text = "";
            txtModel.Text = "";
            txtVehicleNo.Text = "";
            txtOther.Text = "";
            chkAirFilter.Checked = false;
            chkFuelFilter.Checked = false;
            chkOilFilter.Checked = false;
            checkBox2.Checked = false;
            btnFinished.Enabled = false;
            cmbAirFilter.Visible = false;
            cmbCabinFilter.Visible = false;
            cmbEngFilter.Visible = false;
            cmbFuelFilter.Visible = false;
            btnInvoice.Enabled = false;

        }

        public void generate_inv()
        {
            int id_tmp;
            string Query = "SELECT top 1 JobNo FROM job_card order by JobNo DESC";

            DataTable dt = DAL.DataAccessManager.GetDataTable(Query);

            if (dt.Rows.Count == 0)
            {
                id_tmp = 1000;
            }
            else
            {
                id_tmp = Convert.ToInt32(dt.Rows[0][0]) + 1;
            }

            txtJobNo.Text = id_tmp.ToString();
            cmbGearOil.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string JobSaveQ = "INSERT INTO job_card" +
                                       " (JobNo,VehicleNo,Model,Mileage,EngineOil,GearOil,OilFilter,AirFilter,FuelFilter,ATFFilter,CabinFilter,Other,JobDate,Status,NextMilage,DiffOil,Customer)" +
                                       " VALUES ('" + txtJobNo.Text + "','" + txtVehicleNo.Text + "','" + txtModel.Text + "','" + txtMilage.Text + "','" + EngOil + "','" + GearOil + "','" + OilFilter + "','" + AirFilter + "','" + FuelFilter + "','ATFFilter','" + CabinFilter + "','" + txtOther.Text + "',GETDATE(),'Pending','" + textBox1.Text + "','" + DiifOil + "','" + comboBox1.Text + "')";

                int a = DAL.DataAccessManager.ExecuteSQL(JobSaveQ);
                if (a > 0)
                {
                    MessageBox.Show("Job Creation Successful");
                }
                BindCurrentJobs();
                generate_inv();
                ClearField();

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            this.Hide();
            string JobNo = txtJobNo.Text;
            salesRegisterQ qc = new salesRegisterQ(JobNo);
            //SalesRegister qc = new SalesRegister(JobNo);
            qc.MdiParent = this.ParentForm;
            qc.Show();
        }

        private void btnFinished_Click(object sender, EventArgs e)
        {
            string JobUpdtQuery = "UPDATE job_card SET Model = '" + txtModel.Text + "',Mileage = '" + txtMilage.Text + "',Status = 'Finished' WHERE JobNo = '" + txtJobNo.Text + "'";
            int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
            btnFinished.Enabled = false;

            BindCurrentJobs();
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            generate_inv();
            ClearField();
        }
    }
}
