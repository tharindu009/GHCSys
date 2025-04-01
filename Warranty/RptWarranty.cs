using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointofSale.Warranty
{
    public partial class RptWarranty : Form
    {
        public RptWarranty()
        {
            InitializeComponent();
        }


        
        public RptWarranty(string _InvoiceNo, string _JobNo,string _CustomerID,string _SerialNo)
        {
            InitializeComponent();
            InvoiceNo = _InvoiceNo;
            CurrentJobNo = _JobNo;
            CustomerID = _CustomerID;
            SerialNo = _SerialNo;
        }

        public string InvoiceNo
        {
            set
            {
                lblInvoice.Text = value;
            }
            get
            {
                return lblInvoice.Text;

            }
        }

        public string SerialNo
        {
            set
            {
                lblSN.Text = value;
            }
            get
            {
                return lblSN.Text;

            }
        }

        public string CurrentJobNo
        {
            set 
            {
                lblJobNo.Text = value;
            }
            get 
            {
                return lblJobNo.Text;
            }
        }


        string CustomerID = "";
        string CustomerName = "";
        string VehicleReg = "";
        string VehiMake = "";
        string VehModel = "";
        string CusPhone = "";
        string CusEmail = "";
        string VMakeModel = "";

        private void RptWarranty_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = @"SELECT [WarrantyID],[InvoiceID],[ItemDesc],[WarrantyPeriod],[DateRange],[CreateDate]
                            FROM [dbo].[tblWarrantyDetail] WHERE InvoiceID = '" + InvoiceNo + "'";


                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt = DAL.DataAccessManager.GetDataTable(sql);


                string CusSql = @"SELECT [ID],[Name],[EmailAddress],[Phone],[Address],[City],[PeopleType]
                                FROM [dbo].[tbl_customer] WHERE ID='" + CustomerID + "'";
                DataTable Cusdt = DAL.DataAccessManager.GetDataTable(CusSql);
                if (Cusdt.Rows.Count > 0)
                {
                    CustomerName = Cusdt.Rows[0]["Name"].ToString();
                    CusPhone = Cusdt.Rows[0]["Phone"].ToString();
                    CusEmail = Cusdt.Rows[0]["EmailAddress"].ToString();
                }

                if (CurrentJobNo != ".")
                {
                    string JobDetail = @"SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status],[Invoice],[CustomerID],[ProblemDesc],[FinishedDate]
                                    FROM [dbo].[JobHybridMaster] WHERE JobNo = '" + CurrentJobNo + "'";

                    DataTable Jobdt = DAL.DataAccessManager.GetDataTable(JobDetail);

                    if (Jobdt.Rows.Count > 0)
                    {
                        VehicleReg = Jobdt.Rows[0]["VehicleReg"].ToString();

                        string Vsql = @"SELECT [RegNo],[Make],[Model],[Year],[CustomerID]
                                    FROM [dbo].[tblVehicle] WHERE RegNo = '" + VehicleReg + "' AND CustomerID='" + CustomerID + "'";
                        DataTable Vdt = DAL.DataAccessManager.GetDataTable(Vsql);

                        if (Vdt.Rows.Count > 0)
                        {
                            VehiMake = Vdt.Rows[0]["Make"].ToString();
                            VehModel = Vdt.Rows[0]["Model"].ToString();
                            VMakeModel = VehiMake + "/" + VehModel;
                        }
                    }
                }

                ReportParameter[] parameters = new ReportParameter[7];
                parameters[0] = new ReportParameter("CustomerName", CustomerName);
                parameters[1] = new ReportParameter("CustomerPhone", CusPhone);
                parameters[2] = new ReportParameter("CustomerEmail", CusEmail);
                parameters[3] = new ReportParameter("VehicleNo", VehicleReg);
                parameters[4] = new ReportParameter("VehicleMake", VMakeModel);
                parameters[5] = new ReportParameter("InvoiceNo", InvoiceNo);
                parameters[6] = new ReportParameter("SerialNumber", SerialNo);
                this.reportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource reportDSDetail = new ReportDataSource("WarrantyDS", dt);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(reportDSDetail);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
