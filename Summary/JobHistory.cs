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
using PointofSale.BAL;
using PointofSale.SalesRegister;
using PointofSale.Warranty;

namespace PointofSale
{
    public partial class JobHistory : KryptonForm
    {
        public JobHistory()
        {
            InitializeComponent();
        }


        string VehicleReg = "";

        public JobHistory(string vehicleReg)
        {
            InitializeComponent();
            VehicleReg = vehicleReg;
        }

       

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void duePaymentHistory_Load(object sender, EventArgs e)
        {
            try
            {

                //string sqlCmd = @"SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status] FROM [dbo].[JobHybridMaster]
                //                  WHERE [VehicleReg] = '"+VehicleReg+ "' OREDER BY DateIn DESC";

                // = txtCustomerSearch.Text ";// or Phone  like  '%" + txtCustomerSearch.Text + "%'  or City  like  '%" + txtCustomerSearch.Text + "%'  or emailAddress  like  '%" + txtCustomerSearch.Text + "%'";
                //DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                DataSet dtJobHistory = DAL.jobCard.BindJobHistory(VehicleReg);
                //DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                grdJobDetail.DataSource = dtJobHistory.Tables[0];

                lblCusName.Text = dtJobHistory.Tables[0].Rows[0]["CustomerName"].ToString();

                lblVehicleReg.Text = VehicleReg;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdJobDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdJobDetail.Rows[e.RowIndex];
                string jobNo = row.Cells[0].Value.ToString();

                string SQLJobMaster = "SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status],[Invoice],[ProblemDesc],[Model],[Make] FROM [JobHybridMaster] WHERE JobNo = '" + jobNo + "'";
                DataSet dsMaster = DAL.DataAccessManager.GetDataSet(SQLJobMaster);
                lblJobNo.Text = jobNo;
                lblCus.Text = dsMaster.Tables[0].Rows[0][1].ToString();
                lblVeh.Text = dsMaster.Tables[0].Rows[0][2].ToString();
                lblMilage.Text = dsMaster.Tables[0].Rows[0][3].ToString();
                lblDateIn.Text = dsMaster.Tables[0].Rows[0][4].ToString();
                lblAction.Text = dsMaster.Tables[0].Rows[0][5].ToString().Replace(System.Environment.NewLine, ", ");
                lblStatus.Text = dsMaster.Tables[0].Rows[0][6].ToString();
                lblInvNo.Text = dsMaster.Tables[0].Rows[0][7].ToString();
                //lblAction.Text = dsMaster.Tables[0].Rows[0][6].ToString();
                lblProblem.Text = dsMaster.Tables[0].Rows[0]["ProblemDesc"].ToString();
                lblModel.Text = dsMaster.Tables[0].Rows[0]["Make"].ToString() + "|" + dsMaster.Tables[0].Rows[0]["Model"].ToString();

                if (lblStatus.Text != "Finished")
                {
                    kryptonButton4.Enabled = false;
                }


                string SQLTech = "SELECT [JobNo],[AssignTechID],[TechName],[AssignDate],[AssignTime] FROM [JobTechAssign] WHERE JobNo = '" + jobNo + "'";
                DataSet dsTech = DAL.DataAccessManager.GetDataSet(SQLTech);
                int rowCount = dsTech.Tables[0].Rows.Count;
                if (rowCount != 0)
                {
                    string TechNames = "";
                    for (int i = 0; i < rowCount; i++)
                    {
                        TechNames = dsTech.Tables[0].Rows[i][2].ToString();
                        if (rowCount > 1)
                        {
                            TechNames += " / ";
                        }
                    }
                    lblTech.Text = TechNames;
                }


                string SQLJobItem = @"SELECT dbo.JobCardItems.ItemDesc, dbo.JobCardItems.Qty, dbo.JobCardItems.Cost, dbo.JobCardItems.ItemLocation
                                    FROM dbo.JobCardItems LEFT OUTER JOIN dbo.purchase ON dbo.JobCardItems.ReplaceItemNo = dbo.purchase.product_id 
                                    WHERE(dbo.JobCardItems.JobNo = '" + jobNo + "')";
                DataSet dsItemList = DAL.DataAccessManager.GetDataSet(SQLJobItem);
                dataGridView1.DataSource = dsItemList.Tables[0];


                tabControl1.SelectTab(tabPage2);

                if(lblInvNo.Text == "00")
                {
                    kryptonButton4.Text = "Create Invoice";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdJobDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            ///// // Open Print Invoice
            if (lblInvNo.Text == "00")
            {
                //pnlInvoice.Visible = true;
                //pnlInvoice.BringToFront();
                string JobNo = lblJobNo.Text;
                if (JobNo == "")
                {
                    MessageBox.Show("Please Select Job to create Invoice", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                DialogResult result = MessageBox.Show("Do you want to Create Invoice for the Job Number: " + lblJobNo.Text + "? ", "Workshop management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    if (JobNo != "")
                    {
                        saleRegister regQ = new saleRegister(JobNo);
                        regQ.MdiParent = Application.OpenForms["dashboard"];
                        regQ.FormBorderStyle = FormBorderStyle.FixedSingle;
                        regQ.WindowState = FormWindowState.Maximized;

                        regQ.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Job to create Invoice", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                parameter.autoprintid = "0";
                POSPrintRpt go = new POSPrintRpt(lblInvNo.Text);
                go.ShowDialog();
            }
        }

    }
}
