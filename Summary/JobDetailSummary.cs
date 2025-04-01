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
    public partial class JobDetailSummary : KryptonForm
    {
        public JobDetailSummary()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {

                string sqlCmd = " Select * from  [JobHybridMaster] " +
                                " where [VehicleReg]  like  '%" + txtsearch.Text + "%' or " +
                                " JobNo like  '%" + txtsearch.Text + "%' or CustomerName like '%"+txtsearch.Text+"%'";
                // = txtCustomerSearch.Text ";// or Phone  like  '%" + txtCustomerSearch.Text + "%'  or City  like  '%" + txtCustomerSearch.Text + "%'  or emailAddress  like  '%" + txtCustomerSearch.Text + "%'";
                DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                grdJobDetail.DataSource = dt1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void duePaymentHistory_Load(object sender, EventArgs e)
        {
            try
            {

                string sqlCmd = "SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status] FROM [dbo].[JobHybridMaster]";

                // = txtCustomerSearch.Text ";// or Phone  like  '%" + txtCustomerSearch.Text + "%'  or City  like  '%" + txtCustomerSearch.Text + "%'  or emailAddress  like  '%" + txtCustomerSearch.Text + "%'";
                DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                grdJobDetail.DataSource = dt1;

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

                //string SQLJobMaster = "SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status],[Invoice] FROM [JobHybridMaster] WHERE JobNo = '" + jobNo + "'";

                string SQLJobMaster = "SELECT [JobNo],[CustomerName],[VehicleReg] ,[CurrentMilage] ,[DateIn] ,[ActionTaken] ,[Status] ,[Invoice]  ,[Make] ,[Model] ,[CustomerID] ,[ProblemDesc] ,[FinishedDate] FROM [JobHybridMaster] WHERE JobNo = '" + jobNo + "'";

                DataSet dsMaster = DAL.DataAccessManager.GetDataSet(SQLJobMaster);
                lblJobNo.Text = jobNo;
                lblCus.Text = dsMaster.Tables[0].Rows[0]["CustomerName"].ToString();
                lblVeh.Text = dsMaster.Tables[0].Rows[0]["VehicleReg"].ToString();
                lblMilage.Text = dsMaster.Tables[0].Rows[0]["CurrentMilage"].ToString();
                lblDateIn.Text = dsMaster.Tables[0].Rows[0]["DateIn"].ToString();
                lblAction.Text = dsMaster.Tables[0].Rows[0]["ActionTaken"].ToString().Replace(System.Environment.NewLine, ", ");
                lblStatus.Text = dsMaster.Tables[0].Rows[0]["Status"].ToString();
                lblInvNo.Text = dsMaster.Tables[0].Rows[0]["Invoice"].ToString();
                //lblAction.Text = dsMaster.Tables[0].Rows[0][6].ToString();
                lblModel.Text = dsMaster.Tables[0].Rows[0]["Make"].ToString() + " /" + dsMaster.Tables[0].Rows[0]["Model"].ToString();
                lblProblem.Text = dsMaster.Tables[0].Rows[0]["ProblemDesc"].ToString();
                lblAction.Text = dsMaster.Tables[0].Rows[0]["ActionTaken"].ToString();
                lblCusID.Text = dsMaster.Tables[0].Rows[0]["CustomerID"].ToString();


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
                        string TechName1 = dsTech.Tables[0].Rows[i][2].ToString();
                        if (i == 0)
                        {
                            TechNames = TechName1;
                        }
                        else
                        {
                            TechNames = TechNames + " / " + TechName1;
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
            }
            catch
            {

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
            parameter.autoprintid = "0";
            POSPrintRpt go = new POSPrintRpt(lblInvNo.Text);
            go.ShowDialog();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //string sqlCmd = " Select * from  [JobHybridMaster] " +
                //                " where ([VehicleReg]  like  '%" + txtsearch.Text + "%' or " +
                //                " JobNo like  '%" + txtsearch.Text + "%' or CustomerName like '%" + txtsearch.Text + "%')";
                //// = txtCustomerSearch.Text ";// or Phone  like  '%" + txtCustomerSearch.Text + "%'  or City  like  '%" + txtCustomerSearch.Text + "%'  or emailAddress  like  '%" + txtCustomerSearch.Text + "%'";
                //DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                //DataTable dt1 = DAL.DataAccessManager.GetDataTable(sqlCmd);
                //grdJobDetail.DataSource = dt1;
                //string searchString = String.Format("[VehicleReg] LIKE '" + txtsearch.Text.ToString() + "%' AND [" + searchBy + "] LIKE '" + "%" + results[1].ToString() + "'");
                (grdJobDetail.DataSource as DataTable).DefaultView.RowFilter = string.Format("VehicleReg like '%{0}%' OR CustomerName like '%{0}%'", txtsearch.Text.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            try
            {
                grdWarrantyItem.Rows.Clear();
                //CheckWarranty Table
                string WarrantySQL = @"SELECT [WarrantyID],[InvoiceID],[ItemDesc],[WarrantyPeriod],[DateRange],[CreateDate],SerialNo
                                    FROM [dbo].[tblWarrantyDetail] WHERE InvoiceID = '" + lblInvNo.Text + "'";
                DataTable dt = DAL.DataAccessManager.GetDataTable(WarrantySQL);

                //if record found
                if (dt.Rows.Count > 0)
                {
                    RptWarranty warr = new RptWarranty(lblInvNo.Text, lblJobNo.Text, lblCusID.Text,dt.Rows[0][6].ToString());
                    warr.ShowDialog();
                }
                else //No records
                {
                    
                    GenerateWarrantyCert();
                    pnlWarranty.Visible = true;
                }
            }
            catch 
            {

                
            }
            
        }

        private void GenerateWarrantyCert()
        {
            try
            {
                pnlWarranty.Visible = true;
                DataTable dt = new DataTable();
                //for (int i = 0; i < dgrvSalesItemList.Columns.Count; i++)
                //{
                dt.Columns.Add("itm");
                //}
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    //for (int j = 0; j < dgrvSalesItemList.Columns.Count; j++)
                    //{
                    dr["itm"] = row.Cells[0].Value;
                    //}

                    dt.Rows.Add(dr);
                }

                DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();
                dgvCmb.ValueType = typeof(bool);
                dgvCmb.Name = "Chk";
                dgvCmb.HeaderText = "Select";
                dgvCmb.Width = 15;
                grdWarrantyItem.Columns.Add(dgvCmb);
                this.grdWarrantyItem.Columns.Add("itm", "Description");

                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.Items.Add("1 Month");
                cmb.Items.Add("3 Months");
                cmb.Items.Add("6 Months");
                cmb.Items.Add("1 Year");
                cmb.Items.Add("2 Years");
                cmb.Items.Add("3 Years");
                cmb.Items.Add("5 Years");
                cmb.HeaderText = "Warranty";
                grdWarrantyItem.Columns.Add(cmb);

                //this.dataGridView1.Columns.Add("range", "Period");
                //dataGridView1.Columns[0].Width = 15;
                //dataGridView1.Columns[1].Width = 55;
                int dtRowCount = dt.Rows.Count;
                for (int i = 0; i < dtRowCount; i++)
                {
                    grdWarrantyItem.Rows.Add(false, dt.Rows[i][0].ToString(), "");
                }
            }
            catch
            {

            }
        }

        private void btnPrintCert_Click(object sender, EventArgs e)
        {
            try
            {
                int row = grdWarrantyItem.Rows.Count;
                int select = 0;

                foreach (DataGridViewRow grdrow in grdWarrantyItem.Rows)
                {
                    if (Convert.ToBoolean(grdrow.Cells[0].Value))
                    {
                        // what you want to do
                        select++;
                        string ItemDesc = grdrow.Cells[1].Value.ToString();
                        string warranty = grdrow.Cells[2].Value.ToString();
                        string DatePeriod = "";
                        if (warranty == "")
                        {
                            MessageBox.Show("Please select Warranty Period");
                            return;
                        }
                        else
                        {
                            DateTime startDate = DateTime.Now;
                            DateTime endDate = DateTime.Now;
                            if (warranty == "1 Month")
                            {
                                endDate = startDate.AddMonths(1);
                            }
                            else if (warranty == "3 Months")
                            {
                                endDate = startDate.AddMonths(3);
                            }
                            else if (warranty == "6 Months")
                            {
                                endDate = startDate.AddMonths(6);
                            }
                            else if (warranty == "1 Year")
                            {
                                endDate = startDate.AddYears(1);
                            }
                            else if (warranty == "2 Years")
                            {
                                endDate = startDate.AddYears(2);
                            }
                            else if (warranty == "3 Years")
                            {
                                endDate = startDate.AddYears(3);
                            }
                            else if (warranty == "5 Years")
                            {
                                endDate = startDate.AddYears(5);
                            }
                            else
                            {
                                endDate = startDate;
                            }

                            DatePeriod = startDate.ToShortDateString() + "-" + endDate.ToShortDateString();
                        }

                        string SQLWarranty = @"INSERT INTO [dbo].[tblWarrantyDetail] ([InvoiceID],[ItemDesc],[WarrantyPeriod],[DateRange],SerialNo)
                                            VALUES ('" + lblInvNo.Text + "','" + ItemDesc + "','" + warranty + "','" + DatePeriod + "','"+txtSN.Text+"')";

                        DAL.DataAccessManager.ExecuteSQL(SQLWarranty);

                        
                    }

                    

                }
                RptWarranty warr = new RptWarranty(lblInvNo.Text, lblJobNo.Text, lblCusID.Text,txtSN.Text);
                warr.ShowDialog();

                pnlWarranty.Visible = false;
                grdWarrantyItem.Rows.Clear();

                if (select == 0)
                {
                    MessageBox.Show("Please select at least One Item!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            pnlWarranty.Visible=false;
        }
    }
}
