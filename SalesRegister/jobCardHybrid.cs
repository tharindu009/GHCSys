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
using Microsoft.Azure.Pipelines.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace PointofSale
{
    public partial class jobCardHybrid : KryptonForm
    {
        public jobCardHybrid()
        {
            InitializeComponent();
        }


        

        private void jobCard_Load(object sender, EventArgs e)
        {
            pnlItemStock.Visible = false;
            generate_inv();
            BindCurrentJobs();
            BindFinishedJobs();
            ClearField();
            BindEmployee();
            BindEmployee2();
            btnItemList.Visible = false;
            pnlService.Visible = false;
            //cmbDiff.Visible = false;
            //ComboCategory.Visible = false;


            //Job Edit
            DataGridViewButtonColumn Edit = new DataGridViewButtonColumn();
            dgrvSalesItemList.Columns.Add(Edit);
            Edit.HeaderText = "Edit";
            Edit.Text = "Edit";
            Edit.Name = "Edit";
            Edit.ToolTipText = "Edit Job";
            Edit.UseColumnTextForButtonValue = true;
            Edit.Width = 50;
            DataGridViewColumn ColID = dgrvSalesItemList.Columns[0];
            //ColID.Width = 31;
            DataGridViewColumn ColName = dgrvSalesItemList.Columns[1];
            //ColName.Width = 220;
        }

        private void BindEmployee()
        {
            string sql3 = "select * from usermgt";
            DAL.DataAccessManager.ExecuteSQL(sql3);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql3);
            cmbEmployee.DataSource = dt1;
            cmbEmployee.DisplayMember = "Name";

            //cmbEmployee2.DataSource = dt1;
            //cmbEmployee2.DisplayMember = "Name";

        }

        private void BindEmployee2()
        {
            //string sql3 = "select * from usermgt";
            //DAL.DataAccessManager.ExecuteSQL(sql3);
            //DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql3);

            //cmbEmployee2.DataSource = dt1;
            //cmbEmployee2.DisplayMember = "Name";

        }


        private void BindCurrentJobs()
        {
            DataSet dsOpenJobs = DAL.jobCard.BindOpenJobs();
            dgrvSalesItemList.DataSource = dsOpenJobs.Tables[0];
        }


        private void BindFinishedJobs()
        {
            DataSet dsFinishedJob = DAL.jobCard.BindFinishedJobs();
            grdFinisehedJobs.DataSource = dsFinishedJob.Tables[0];
        }


        private void ClearField()
        {
            txtMilage.Text = "";
            txtVehicleType.Text = "";
            txtRegNo.Text = "";
            txtOther.Text = "";
            txtCustomerName.Text = "";
            txtVModel.Text = "";
            txtActionTaken.Text = "";
            grdCategory.Rows.Clear();
            lblPhoneNo.Text = "...";
            lblJobNo.Text = "000";
            lblCusID.Text = ".";


            btnFinished.Enabled = false;            
            btnInvoice.Enabled = false;
            pnlItemStock.Visible = false;
            panel1.Visible = false;
            pnlCustomer.Visible = false;
            pnlVehicle2.Visible = false;
            lblPreviousMileage.Text = "0";
            lvlPrevious.Visible = false;
            grdTech.Rows.Clear();
        }

        public void generate_inv()
        {
            int id_tmp;
            string Query = "SELECT top 1 JobNo FROM JobHybridMaster order by JobNo DESC";

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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblJobNo.Text == "000" || btnSave.Text != "Update")
                {
                    if (grdTech.Rows.Count == 0)
                    {
                        MessageBox.Show("Please Selct at leat one technician to the Job !!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //Insert Job Master
                    int JobNo = Convert.ToInt32(txtJobNo.Text);
                    string customerName = txtCustomerName.Text;
                    string vehicleReg = txtRegNo.Text;
                    string currentMilage = txtMilage.Text.ToString();
                    DateTime DateIn = dtDateIn.Value;
                    string ActionTaken = txtActionTaken.Text;
                    string otherIssues = "";
                    otherIssues = txtOther.Text;
                    string JobMasterQuery = "INSERT INTO[dbo].[JobHybridMaster] ([JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status],[Invoice],Make,Model,[CustomerID],ProblemDesc)" +
                                             "VALUES ('" + JobNo + "','" + customerName + "','" + vehicleReg + "','" + currentMilage + "','" + DateIn + "','" + ActionTaken + "','Open','00','" + txtVehicleType.Text + "','" + txtVModel.Text + "','"+ lblCusID.Text + "','"+otherIssues+"')";
                    DAL.DataAccessManager.ExecuteSQL(JobMasterQuery);

                    #region CommentDetail


                    //Insert Job Detail
                    //Dictionary<string, string> vehicleComponents = new Dictionary<string, string>
                    //{
                    //    { "hybridSystem", "No" },
                    //    { "engine", "No" },
                    //    { "gearBox", "No" },
                    //    { "breakSystem", "No" },
                    //    { "dashBoard", "No" },
                    //    { "battery12", "No" },
                    //    { "fuseBox", "No" },
                    //    { "oil", "No" },
                    //    { "electrical", "No" },
                    //    { "sound", "No" },
                    //    { "belts", "No" },
                    //    { "coolant", "No" },
                    //    { "alternator", "No" },
                    //    { "shock", "No" },

                    //};
                    #endregion

                    #region Insert JobDetail
                    //string hybridSystem = "No", engine = "No", gearBox = "No", breakSystem = "No", dashBoard = "No", battery12 = "No", fuseBox = "No", oil = "No",
                    //    electrical = "No", sound = "No", belts = "No", coolent = "No", alternator = "No", shock = "No";

                    ////if (chkBattery.Checked) hybridSystem = "Yes";
                    ////if (chkEngine.Checked) engine = "Yes";
                    ////if (chkGear.Checked) gearBox = "Yes";
                    ////if(chkBreak.Checked) breakSystem = "Yes";
                    ////if (chkDashboard.Checked) dashBoard = "Yes";
                    ////if (chk12v.Checked) battery12 = "Yes";
                    ////if (chkFuse.Checked) fuseBox = "Yes";
                    ////if (chkOil.Checked) oil = "Yes";
                    ////if (chkElectric.Checked) electrical = "Yes";
                    ////if (chkSound.Checked) sound = "Yes";
                    ////if (chkBelt.Checked) belts = "Yes";
                    ////if (chkCoolant.Checked) coolent = "Yes";
                    ////if (chkAlternator.Checked) alternator = "Yes";
                    ////if (chkBoot.Checked) shock = "Yes";


                    //otherIssues = txtOther.Text;

                    //string JobCardDetailQuery = "INSERT INTO[dbo].[JobHybridDetail] ([JobNo],[HybridSystem],[Engine],[GearBox],[BreakSystem],[DashBoard],[Battery12v],[FuseBox],[Oil],[Electrical],[Sound],[Belts],[Coolant],[Alternator],[Shock],[OtherIssues]) " +
                    //                                    "VALUES ('" + JobNo + "','" + hybridSystem + "','" + engine + "','" + gearBox + "','" + breakSystem + "','" + dashBoard + "'" +
                    //                                            ",'" + battery12 + "','" + fuseBox + "','" + oil + "','" + electrical + "','" + sound + "','" + belts + "'" +
                    //                                            ",'" + coolent + "','" + alternator + "','" + shock + "','" + otherIssues + "')";
                    //DAL.DataAccessManager.ExecuteSQL(JobCardDetailQuery);
                    #endregion

                    #region SaveTech
                    SaveAssignTech();
                    #endregion


                    #region Insert Item     
                    //Insert Job Cart Item
                    int itmCount = grdCategory.Rows.Count;
                    if (itmCount != 0)
                    {
                        for (int i = 0; i < itmCount - 1; i++)
                        {
                            string ItemNo = grdCategory.Rows[i].Cells[0].Value.ToString();
                            double Qty = Convert.ToDouble(grdCategory.Rows[i].Cells[3].Value.ToString());
                            double Cost = Convert.ToDouble(grdCategory.Rows[i].Cells[2].Value.ToString());
                            string ItemLoc = grdCategory.Rows[i].Cells["Loc"].Value.ToString();
                            string ItemDesc = grdCategory.Rows[i].Cells[1].Value.ToString();

                            string ItemQuery = "INSERT INTO [dbo].[JobCardItems] ([JobNo],[ReplaceItemNo],[ItemDesc],[Qty],[Cost],[ItemLocation]) " +
                                                "VALUES ('" + JobNo + "','" + ItemNo + "','" + ItemDesc + "','" + Qty + "','" + Cost + "','" + ItemLoc + "')";

                            DAL.DataAccessManager.ExecuteSQL(ItemQuery);
                        }
                    }
                    #endregion

                    SaveNextServiceDetail();

                    MessageBox.Show("Job Save Successful", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindCurrentJobs();
                    generate_inv();
                    ClearField();
                    tabSRcontrol.SelectTab(tabPage2);
                    //btnSave.Visible = false;
                }
                else //Update
                {
                    #region Update Job Master
                    string JobMasterUpdate = "UPDATE [JobHybridMaster] SET [CustomerName] = '" + txtCustomerName.Text + "',[VehicleReg] = '" + txtRegNo.Text + "',[CurrentMilage] = '" + txtMilage.Text + "'" +
                                             ",[ActionTaken] = '" + txtActionTaken.Text + "',[Make] = '" + txtVehicleType.Text + "',[Model] = '" + txtVModel.Text + "',ProblemDesc='"+txtOther.Text +"' WHERE JobNo = '" + lblJobNo.Text + "'";
                    DAL.DataAccessManager.ExecuteSQL(JobMasterUpdate);
                    #endregion

                    #region Update Job Detail
                    //string JoDetailUpdate = "UPDATE [JobHybridDetail] SET [OtherIssues] = '" + txtOther.Text + "' WHERE[JobNo] = '" + lblJobNo.Text + "'";
                    //DAL.DataAccessManager.ExecuteSQL(JoDetailUpdate);
                    #endregion

                    #region Update Job Items
                    string DeleteItems = "DELETE FROM [JobCardItems] WHERE JobNo = '" + lblJobNo.Text + "'";
                    DAL.DataAccessManager.ExecuteSQL(DeleteItems);

                    #region Insert Item     
                    //Insert Job Cart Item
                    int itmCount = grdCategory.Rows.Count;
                    if (itmCount != 0)
                    {
                        for (int i = 0; i < itmCount - 1; i++)
                        {
                            string ItemNo = grdCategory.Rows[i].Cells[0].Value.ToString();
                            double Qty = Convert.ToDouble(grdCategory.Rows[i].Cells[3].Value.ToString());
                            double Cost = Convert.ToDouble(grdCategory.Rows[i].Cells[2].Value.ToString());
                            string ItemLoc = grdCategory.Rows[i].Cells["Loc"].Value.ToString();
                            string ItemDesc = grdCategory.Rows[i].Cells[1].Value.ToString();

                            string ItemQuery = "INSERT INTO [dbo].[JobCardItems] ([JobNo],[ReplaceItemNo],[ItemDesc],[Qty],[Cost],[ItemLocation]) " +
                                                "VALUES ('" + lblJobNo.Text + "','" + ItemNo + "','" + ItemDesc + "','" + Qty + "','" + Cost + "','" + ItemLoc + "')";

                            DAL.DataAccessManager.ExecuteSQL(ItemQuery);
                        }
                    }
                    #endregion

                    #endregion


                    #region updateAssignTech

                    string Deletetech = "DELETE FROM [JobTechAssign] WHERE JobNo = '" + lblJobNo.Text + "'";
                    DAL.DataAccessManager.ExecuteSQL(Deletetech);

                    SaveAssignTech();

                    #endregion

                    UpdateNextServiceDetail();
                    

                    BindCurrentJobs();
                    generate_inv();
                    ClearField();
                    lblJobNo.Text = "000";
                    btnSave.Text = "Save";
                    tabSRcontrol.SelectTab(tabPage2);
                    //btnSave.Visible = false;
                    MessageBox.Show("Job Update Successful", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveNextServiceDetail()
        {
            string Milage = txtNextMileage.Text;
            DateTime NextServiceDate = dtNextService.Value;
            string NextServiceComment = txtServiceComments.Text;

            int RowAffected = DAL.jobCard.InsertNextServiceDetail(txtJobNo.Text, Milage, NextServiceDate, NextServiceComment, txtRegNo.Text);
        }


        private void UpdateNextServiceDetail()
        {
            string Milage = txtNextMileage.Text;
            DateTime NextServiceDate = dtNextService.Value;
            string NextServiceComment = txtServiceComments.Text;

            int RowAffected = DAL.jobCard.UpdateNextServiceDetail(txtJobNo.Text, Milage, NextServiceDate, NextServiceComment, txtRegNo.Text);
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            //pnlInvoice.Visible = true;
            //pnlInvoice.BringToFront();
            string JobNo = lblSelectJob.Text;
            if(JobNo == "")
            {
                MessageBox.Show("Please Select Job to create Invoice", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            DialogResult result = MessageBox.Show("Do you want to Complete the Job Number: " + lblSelectJob.Text + "? ", "Workshop management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int JobNoUpdate = Convert.ToInt32(lblSelectJob.Text);
                string JobUpdtQuery = "UPDATE [dbo].[JobHybridMaster] SET [Status] = 'Finished', FinishedDate = GETDATE() WHERE [JobNo] = '" + JobNoUpdate + "'";
                int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
                btnFinished.Enabled = false;

                BindCurrentJobs();

                if (JobNo != "")
                {
                    //RegisterQ regQ = new RegisterQ(JobNo);
                    //regQ.MdiParent = Application.OpenForms["dashboard"];
                    //regQ.FormBorderStyle = FormBorderStyle.FixedSingle;
                    //regQ.WindowState = FormWindowState.Maximized;

                    //regQ.Show();

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

        private void btnFinished_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Do you want to Complete the Job Number: "+ lblSelectJob.Text +"? ", "Workshop management", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                   
                        
                        int JobNoUpdate = Convert.ToInt32(lblSelectJob.Text);
                        string JobUpdtQuery = "UPDATE [dbo].[JobHybridMaster] SET [Status] = 'Finished' WHERE [JobNo] = '" + JobNoUpdate + "'";
                        int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
                        btnFinished.Enabled = false;

                        BindCurrentJobs();

                        MessageBox.Show("Job Number:" + JobNoUpdate + " Successfully Completed", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Suspend the Job Number: " + lblSelectJob.Text + "? ", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int JobNoUpdate = Convert.ToInt32(lblSelectJob.Text);
                string JobUpdtQuery = "UPDATE [dbo].[JobHybridMaster] SET [Status] = 'Suspend' WHERE [JobNo] = '" + JobNoUpdate + "'";
                int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
                btnFinished.Enabled = false;

                BindCurrentJobs();

                MessageBox.Show("Job Number:" + JobNoUpdate + " Successfully Suspended", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            generate_inv();
            ClearField();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnItemList_Click(object sender, EventArgs e)
        {
            //pnlItemStock.Visible = true;
            if(rdoOutside.Checked == true)
            {
                Random rnd = new Random();
                int itemNoOut = rnd.Next(1234, 9999);
                label61.Text = itemNoOut.ToString();
                rdoStock.Checked = false;
                pnlOutside.Visible = true;
                pnlItemStock.Visible = false;
            }
            else if(rdoStock.Checked == true)
            {
                rdoOutside.Checked = false;
                pnlItemStock.Visible = true;
                pnlOutside.Visible = false;
                //string ItemQuery = "SELECT [product_id],[product_name],[product_quantity],[retail_price] FROM [purchase] where [product_quantity] > 0 and [category] != 'Service'";
                //DataTable dt = DAL.DataAccessManager.GetDataTable(ItemQuery);
                DataSet dsStockItem = DAL.jobCard.BindStockItems();
                grdStockItem.DataSource = dsStockItem.Tables[0];

            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (tabSRcontrol.TabPages.Contains(tabPageSR_Payment))//tab already present
            {
                if (txtMilage.Text == "") { MessageBox.Show("Please Fill Current Mileage"); txtMilage.Focus(); }
                else
                {
                    tabSRcontrol.SelectTab(tabPage1);
                    btnSave.Visible = true;
                }
            }
            else
            {
                //tabControlMdi.TabPages.Add(tabProduct); // add removed tab
                //tabControlMdi.SelectTab(tabProduct);    // select by name
            }
        }


        string ItemLoc = "";

        private void rdoOutside_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoOutside.Checked == true)
            {
                rdoStock.Checked = false;
            }
            btnItemList.Visible = true;
            ItemLoc = "OutSide";
        }

        private void rdoStock_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoStock.Checked == true)
            {
                rdoOutside.Checked = false;
            }
            btnItemList.Visible = true;
           
            ItemLoc = "InHouse Stock";
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            lblItemNo.Text = label61.Text.ToString();
            txtItemName.Text = txtOutsideItemName.Text.ToString();
            lblQty.Text = txtOutsideItemQty.Text.ToString();
            
            this.grdCategory.Rows.Add(lblItemNo.Text, txtItemName.Text, txtCost.Text,lblQty.Text,ItemLoc,"");

            if(txtOutsideItemNo.Text != "")
            {
                this.grdCategory.Rows.Add(lblItemNo.Text, "Transport Cost", txtOutsideItemNo.Text, "1", ItemLoc, "");
            }
            pnlOutside.Visible = false;
            txtOutsideItemName.Text = "";
            txtOutsideItemQty.Text = "";
            txtCost.Text = "";
            txtOutsideItemNo.Text = "";
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            pnlItemStock.Visible = false;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            pnlOutside.Visible = false;
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string ItemQuery = "SELECT [product_id],[product_name],[product_quantity],[retail_price] FROM [purchase] where category != 'Service' AND [product_quantity] > 0 and ([product_name] like '%" + txtItemSearch.Text + "%' or [product_id] like '%" + txtItemSearch.Text + "%')";
                //DataTable dtItem = DAL.DataAccessManager.GetDataTable(ItemQuery);

                DataSet dsItem = DAL.jobCard.GetSearchItems(txtItemSearch.Text);
                DataTable dtItem = dsItem.Tables[0];

                grdStockItem.DataSource = dtItem;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grdStockItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }


        private void grdStockItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdStockItem.Rows[e.RowIndex];
                lblProID.Text = row.Cells[0].Value.ToString();
                lblProName.Text = row.Cells[1].Value.ToString();
                lblSelPrice.Text = row.Cells[3].Value.ToString();
                //lbpaidamt.Text = row.Cells[4].Value.ToString();
                //lbDueAmount.Text = row.Cells[6].Value.ToString();
                //lbcontact.Text = row.Cells[9].Value.ToString();
                //pnlReceiveDue.Visible = true;
                this.grdCategory.Rows.Add(lblProID.Text, lblProName.Text, lblSelPrice.Text, "1", ItemLoc);
                pnlItemStock.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgrvSalesItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgrvSalesItemList.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                //dgrvSalesItemList.CurrentRow.DefaultCellStyle.ForeColor = Color.White;

                if (e.ColumnIndex == dgrvSalesItemList.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row1 in dgrvSalesItemList.SelectedRows)
                    {
                        DataGridViewRow row = dgrvSalesItemList.Rows[e.RowIndex];
                        string JobNo = row.Cells["JobNo"].Value.ToString();
                        lblJobNo.Text = JobNo;
                        GetJobMaster(JobNo);
                        GetJobCardItems(JobNo);

                        GetAssignTech(JobNo);

                        btnSave.Text = "Update";

                        if (tabSRcontrol.TabPages.Contains(tabPage2))//tab already present
                        {
                            tabSRcontrol.SelectTab(tabPageSR_Payment);     
                        }
                        else{
                        }
                    }
                }
                else
                {
                    DataGridViewRow row = dgrvSalesItemList.Rows[e.RowIndex];
                    string jobNo = row.Cells["JobNo"].Value.ToString();
                    lblSelectJob.Text = jobNo;
                    btnFinished.Enabled = true;
                    btnInvoice.Enabled = true;
                    dashboard dash = new dashboard();
                    dash.lblJobNo.Text = jobNo;
                    dash.btnInvoice.Visible = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Get Job Master
        private void GetJobMaster(string JobNo)
        {
            try
            {
                //Get Job Details
                string JobMaster = "SELECT [JobNo],[CustomerName],[VehicleReg],[CurrentMilage],[DateIn],[ActionTaken],[Status],[CustomerID],"+
                    "[Make],[Model],[ProblemDesc] FROM [JobHybridMaster] where JobNo = '" + JobNo + "'";
                DataSet JobMasterDS = DAL.DataAccessManager.GetDataSet(JobMaster);

                txtJobNo.Text = JobMasterDS.Tables[0].Rows[0][0].ToString();
                txtCustomerName.Text = JobMasterDS.Tables[0].Rows[0][1].ToString();
                pnlCustomer.Visible = false;
                txtRegNo.Text = JobMasterDS.Tables[0].Rows[0][2].ToString();
                txtMilage.Text = JobMasterDS.Tables[0].Rows[0][3].ToString();
                txtActionTaken.Text = JobMasterDS.Tables[0].Rows[0][5].ToString();
                lblCusID.Text = JobMasterDS.Tables[0].Rows[0]["CustomerID"].ToString();
                txtVehicleType.Text = JobMasterDS.Tables[0].Rows[0]["Make"].ToString();
                txtVModel.Text = JobMasterDS.Tables[0].Rows[0]["Model"].ToString();

                //string JobDetailSQL = "SELECT [JobNo],[OtherIssues] FROM [JobHybridDetail] WHERE [JobNo] = '" + JobNo + "'";
                //DataTable dtDetail = DAL.DataAccessManager.GetDataTable(JobDetailSQL);
                txtOther.Text = JobMasterDS.Tables[0].Rows[0]["ProblemDesc"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Get Job Card Items
        private void GetJobCardItems(string JobNo)
        {
            try
            {
                string ItemQuery = "SELECT [JobItemID],[JobNo],[ReplaceItemNo],[Qty],[Cost],[ItemLocation],[ItemDesc] FROM [JobCardItems] WHERE JobNo = '" + JobNo + "'";
                DataSet JobItemDS = DAL.DataAccessManager.GetDataSet(ItemQuery);

                int rows = JobItemDS.Tables[0].Rows.Count;
                for (int i = 0; i < rows; i++)
                {
                    string ItemNo = JobItemDS.Tables[0].Rows[i][2].ToString();
                    string ItemDesc = JobItemDS.Tables[0].Rows[i][6].ToString();
                    double Qty = Convert.ToDouble(JobItemDS.Tables[0].Rows[i][3].ToString());
                    string Location = JobItemDS.Tables[0].Rows[i][5].ToString();
                    string Price = JobItemDS.Tables[0].Rows[i][4].ToString();

                    this.grdCategory.Rows.Add(ItemNo, ItemDesc, Price, Qty, Location);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            string JobUpdtQuery = "UPDATE [dbo].[JobHybridMaster] SET [Status] = 'Suspend' WHERE [JobNo] = '" + lblSelectJob + "'";
            int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
            btnFinished.Enabled = false;

            BindCurrentJobs();
        }

        private void dgrvSalesItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == dgrvSalesItemList.Columns["Edit"].Index && e.RowIndex >= 0)
            //    {
            //        foreach (DataGridViewRow row1 in dgrvSalesItemList.SelectedRows)
            //        {
            //            DataGridViewRow row = dgrvSalesItemList.Rows[e.RowIndex];
            //            string JobNo = row.Cells["JobNo"].Value.ToString();
            //            lblJobNo.Text = JobNo;
            //            GetJobMaster(JobNo);
            //            GetJobCardItems(JobNo);
            //            btnSave.Text = "Update";

            //            if (tabSRcontrol.TabPages.Contains(tabPage2))//tab already present
            //            {
            //                tabSRcontrol.SelectTab(tabPageSR_Payment);
            //            }
            //            else
            //            {
            //            }
            //        }
            //    }
            //    else
            //    {
            //        DataGridViewRow row = dgrvSalesItemList.Rows[e.RowIndex];
            //        string jobNo = row.Cells["JobNo"].Value.ToString();
            //        lblSelectJob.Text = jobNo;
            //        btnFinished.Enabled = true;
            //        btnInvoice.Enabled = true;
            //        dashboard dash = new dashboard();
            //        dash.lblJobNo.Text = jobNo;
            //        dash.btnInvoice.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            //dgrvSalesItemList.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
            //dgrvSalesItemList.CurrentRow.DefaultCellStyle.ForeColor = Color.White;
        }

        private void kryptonButton6_Click_1(object sender, EventArgs e)
        {
            pnlService.Visible = true;
            string ItemQuery = "SELECT DISTINCT [product_id],[product_name],[product_quantity],[retail_price] FROM [purchase] where [Category] = 'Service'";
            DataTable dt = DAL.DataAccessManager.GetDataTable(ItemQuery);
            grdServiceList.DataSource = dt;
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            pnlService.Visible = false;
        }

        private void grdServiceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdServiceList.Rows[e.RowIndex];
                lblProID.Text = row.Cells[0].Value.ToString();
                lblProName.Text = row.Cells[1].Value.ToString();
                lblSelPrice.Text = row.Cells[3].Value.ToString();
                //lbpaidamt.Text = row.Cells[4].Value.ToString();
                //lbDueAmount.Text = row.Cells[6].Value.ToString();
                //lbcontact.Text = row.Cells[9].Value.ToString();
                //pnlReceiveDue.Visible = true;
                this.grdCategory.Rows.Add(lblProID.Text, lblProName.Text, lblSelPrice.Text, "1", "InHouse Stock");
                pnlService.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        private void kryptonTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            //string ItemQuery = "SELECT [product_id],[product_name],[product_quantity],[retail_price] FROM [purchase] where [product_quantity] > 0 and ([product_name] like '%" + kryptonTextBox1.Text + "%' AND  [Category] = 'Service')";

            //DataTable dtItem = DAL.DataAccessManager.GetDataTable(ItemQuery);
            DataSet dsItem = DAL.jobCard.GetServices(kryptonTextBox1.Text);
            DataTable dtItem = dsItem.Tables[0];

            grdServiceList.DataSource = dtItem;
            if(dtItem.Rows.Count == 0)
            {
                lnkNewService.Visible = true;
            }
            else
            {
                lnkNewService.Visible = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtRegNo.Enabled = true;
        }

        private void kryptonButton5_Click_2(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            lblItemNo.Text = txtOutsideItemNo.Text.ToString();
            txtItemName.Text = txtOutsideItemName.Text.ToString();
            lblQty.Text = txtOutsideItemQty.Text.ToString();
            pnlOutside.Visible = false;
            this.grdCategory.Rows.Add(txtSerial.Text, txtDesc.Text, txtBatteryCost.Text, txtQtyBattery.Text, "OutSide", cmbWarranty.Text);
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            tabSRcontrol.SelectTab(tabPageSR_Payment);
            ClearField();
            generate_inv();
            
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            pnlCustomer.Visible = false;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            pnlCustomer.Visible = true;
            string CusDetail = "SELECT [Name],[Phone],[City],[ID] FROM [tbl_customer] WHERE ([Name] LIKE '%" + txtCustomerName.Text + "%' OR [Phone] like '%" + txtCustomerName.Text + "%') AND [PeopleType] = 'Customer'";
            DataTable dtCusDetail = DAL.DataAccessManager.GetDataTable(CusDetail);
            grdCustomer.DataSource = dtCusDetail;
            if(dtCusDetail.Rows.Count == 0)
            {
                lnkAddnewCustomer.Visible = true;
            }
            else
            {
                lnkAddnewCustomer.Visible = false;
            }
        }

        private void grdCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdCustomer.Rows[e.RowIndex];
                lblCustomer.Text = row.Cells[0].Value.ToString();
                lblPhoneNo.Text = row.Cells[1].Value.ToString();
                //txtRegNo.Text = row.Cells[2].Value.ToString();
                //txtVehicleType.Text = row.Cells[3].Value.ToString();
                //txtVModel.Text = row.Cells[4].Value.ToString();        
                lblCusID.Text = row.Cells["ID"].Value.ToString();
                txtCustomerName.Text = lblCustomer.Text;
                pnlCustomer.Visible = false;
                LoadVehicleDetail(lblCusID.Text);
                pnlVehicle.Visible = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVehicleDetail(string CusID)
        {
            string VehicleSQl = "SELECT [RegNo],[Make],[Model],[Year],[CustomerID] FROM [dbo].[tblVehicle] WHERE CustomerID='"+CusID+"'";
            DataTable dtVehicle = DAL.DataAccessManager.GetDataTable(VehicleSQl);
            grdVehicle.DataSource = dtVehicle;
            
            if (grdVehicle.Rows.Count ==0)
            {
                lblveh.Visible = true;
            }
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAddNewService.Visible = true;
            string ServiceID = checkServiceCode();
            lblServiceID.Text = ServiceID;
            lnkNewService.Visible = false;
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            pnlAddNewService.Visible = false;
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            //string pid = checkServiceCode();
            try
            {
                string sql1 = "INSERT INTO purchase (product_id,product_name,product_quantity,cost_price,retail_price,total_cost_price,total_retail_price,category,supplier,imagename,discount,taxapply,Shopid,status) " +
                                           " VALUES ('" + lblServiceID.Text + "','" + txtServiceName.Text + "','999','" + txtServiceCost.Text + "','" + txtServiceCost.Text + "','" + txtServiceCost.Text + "','" + txtServiceCost.Text + "','Service','Green Hybrid',0xFFD8FFE000104A46494600010101006000600000FFE1004E4578696600004D4D002A00000008000403010005000000010000003E51100001000000010100000051110004000000010000000051120004000000010000000000000000000186A00000B18FFFDB004300080606070605080707070909080A0C140D0C0B0B0C1912130F141D1A1F1E1D1A1C1C20242E2720222C231C1C2837292C30313434341F27393D38323C2E333432FFDB0043010909090C0B0C180D0D1832211C213232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232FFC00011080080008003012200021101031101FFC4001F0000010501010101010100000000000000000102030405060708090A0BFFC400B5100002010303020403050504040000017D01020300041105122131410613516107227114328191A1082342B1C11552D1F02433627282090A161718191A25262728292A3435363738393A434445464748494A535455565758595A636465666768696A737475767778797A838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE1E2E3E4E5E6E7E8E9EAF1F2F3F4F5F6F7F8F9FAFFC4001F0100030101010101010101010000000000000102030405060708090A0BFFC400B51100020102040403040705040400010277000102031104052131061241510761711322328108144291A1B1C109233352F0156272D10A162434E125F11718191A262728292A35363738393A434445464748494A535455565758595A636465666768696A737475767778797A82838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE2E3E4E5E6E7E8E9EAF2F3F4F5F6F7F8F9FAFFDA000C03010002110311003F00F9FE8A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A00FFFD9,'0','0','MTQC02','1')";
                int a = DAL.DataAccessManager.ExecuteSQL(sql1);

                this.grdCategory.Rows.Add(lblServiceID.Text, txtServiceName.Text, txtServiceCost.Text, "1", "InHouse Stock");
                pnlService.Visible = false;
                pnlAddNewService.Visible = false;
                lblServiceID.Text = ""; ;
                txtServiceName.Text = "";
                txtServiceCost.Text = "";
            }
            catch (Exception ex)
            {

            }
        }

        private string checkServiceCode()
        {
            string serviceCode = "";
            string ServiceQuery = "SELECT TOP 1 * FROM purchase where category = 'Service' ORDER BY product_id DESC";
            DataTable dtService = DAL.DataAccessManager.GetDataTable(ServiceQuery);
            if (dtService.Rows.Count == 0)
            {
                serviceCode = "SE001";
            }
            else
            {
                string LastServiceID = dtService.Rows[0][0].ToString();
                string LastDigit = LastServiceID.Substring(LastServiceID.Length - 3);
                int ID = Convert.ToInt32(LastDigit);
                int NextID = ID + 1;
                if (NextID < 100)
                {
                    serviceCode = "SE0" + NextID.ToString();
                }
                else
                {
                    serviceCode = "SE" + NextID.ToString();
                }
            }
            return serviceCode;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlCustomer.Visible = false;
            pnlAddNewCustomer.Visible = true;
            BindVehicleMake();
        }

        private void kryptonButton13_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewName.Text == "") { MessageBox.Show("Please Fill Name"); txtCustomerName.Focus(); }
                else if (txtPhone.Text == "") { MessageBox.Show("Please Fill Phone"); txtPhone.Focus(); }
                                
                else
                {
                    string sqlCmd = "insert into tbl_customer (Name, EmailAddress, Phone, address, City, PeopleType,[RegDate])  values ('" + txtNewName.Text + "', '" + txtEmailAddress.Text + "', '" + txtPhone.Text + "', '" + txtCustomerAddress.Text + "', '" + txtCustomerAddress.Text + "', 'Customer',GETDATE())";

                    int i = DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                    if (i != 0)
                    {
                        string customersql = "Select ID from tbl_customer where Phone = '" + txtPhone.Text + "'";
                        DataTable dtCus = DAL.DataAccessManager.GetDataTable(customersql);
                        string NewCustomerID = "";
                        if (dtCus.Rows.Count != 0)
                        {
                            NewCustomerID = dtCus.Rows[0][0].ToString();
                        }
                        string VehicleQuery = "INSERT INTO [dbo].[tblVehicle] ([RegNo],[Make],[Model],[CustomerID]) " +
                                    "VALUES ('" + txtCity.Text + "','" + cmbMake.Text + "','" + cmbModel.Text + "','" + NewCustomerID + "')";
                        DAL.DataAccessManager.ExecuteSQL(VehicleQuery);

                        MessageBox.Show("Successfully saved");
                        txtCustomerName.Text = txtNewName.Text;
                        pnlAddNewCustomer.Visible = false;
                        txtNewName.Text = "";
                        txtCity.Text = string.Empty;
                        txtCustomerAddress.Text = string.Empty;
                        txtPhone.Text = string.Empty;
                        txtEmailAddress.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Please try again", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            pnlAddNewCustomer.Visible = false;
        }

        private void lnkNewService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAddNewService.Visible = true;
            string ServiceID = checkServiceCode();
            lblServiceID.Text = ServiceID;
            lnkNewService.Visible = false;
        }

        public void changetab()
        {
            tabSRcontrol.SelectTab(tabPage2);
        }

        private void kryptonButton14_Click(object sender, EventArgs e)
        {
            pnlVehicle.Visible = false;
        }

        private void grdVehicle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdVehicle.Rows[e.RowIndex];
                txtRegNo.Text = row.Cells[0].Value.ToString();
                txtVehicleType.Text = row.Cells[1].Value.ToString();
                txtVModel.Text = row.Cells[2].Value.ToString();
                //txtRegNo.Text = row.Cells[2].Value.ToString();
                //txtVehicleType.Text = row.Cells[3].Value.ToString();
                //txtVModel.Text = row.Cells[4].Value.ToString();        
                pnlVehicle.Visible = false;

                //Get Previous Job Details
                string PreviousJob = "SELECT CurrentMilage FROM [JobHybridMaster] where VehicleReg = '" + txtRegNo.Text + "' order by DateIn DESC";
                DataTable dtJobDetail = DAL.DataAccessManager.GetDataTable(PreviousJob);

                if (dtJobDetail.Rows.Count != 0)
                {
                    lblPreviousMileage.Text = dtJobDetail.Rows[0]["CurrentMilage"].ToString();
                    lvlPrevious.Visible = true;
                }
                else
                {
                    lvlPrevious.Visible = false;
                }

                if(pnlVehicle2.Visible == true)
                {
                    pnlVehicle2.Visible = false;
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblveh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lblProblem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtRegNo_TextChanged(object sender, EventArgs e)
        {
            
            if (txtCustomerName.Text == "")
            {
                pnlVehicle2.Visible = true;
                string VehicleDetail = "SELECT [RegNo],[Make],[Model],[Year],[CustomerID] FROM [tblVehicle] WHERE [RegNo] LIKE '%" + txtRegNo.Text + "%'";
                DataTable dtVDetail = DAL.DataAccessManager.GetDataTable(VehicleDetail);
                grdVehicle2.DataSource = dtVDetail;
                if (dtVDetail.Rows.Count == 0)
                {
                    linkVehicle2.Visible = true;
                }
                else
                {
                    linkVehicle2.Visible = false;
                }
            }
        }

        private void btnpnl2Close_Click(object sender, EventArgs e)
        {
            pnlVehicle2.Visible = false;
        }

        private void grdVehicle2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grdVehicle2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = grdVehicle2.Rows[e.RowIndex];

                txtRegNo.Text = row.Cells[0].Value.ToString();
                txtVehicleType.Text = row.Cells[1].Value?.ToString();
                txtVModel.Text = row.Cells[2].Value?.ToString();

                pnlVehicle2.Visible = false;

                //Get Previous Job Details
                string PreviousJob = "SELECT CurrentMilage FROM [JobHybridMaster] where VehicleReg = '" + txtRegNo.Text + "' order by DateIn DESC";
                DataTable dtJobDetail = DAL.DataAccessManager.GetDataTable(PreviousJob);

                if (dtJobDetail.Rows.Count != 0)
                {
                    lblPreviousMileage.Text = dtJobDetail.Rows[0]["CurrentMilage"].ToString();
                    lvlPrevious.Visible = true;
                }

                //Customer Name
                //string CusName = "SELECT c.Name,v.[Make],v.[Model] FROM [dbo].[tblVehicle] v inner join [dbo].[tbl_customer] c on v.CustomerID = c.ID WHERE v.RegNo = '" + txtRegNo.Text + "'";
                //DataTable dsCus = DAL.DataAccessManager.GetDataTable(CusName);

                DataSet dsCus = DAL.jobCard.GetCustomerDetail(txtRegNo.Text);

                txtCustomerName.Text = dsCus.Tables[0].Rows[0]["Name"].ToString();
                txtVehicleType.Text = dsCus.Tables[0].Rows[0]["Make"].ToString();
                txtVModel.Text = dsCus.Tables[0].Rows[0]["Model"].ToString();
                lblCusID.Text = dsCus.Tables[0].Rows[0]["ID"].ToString();
                lblPhoneNo.Text = dsCus.Tables[0].Rows[0]["Phone"].ToString();

                if (pnlCustomer.Visible == true)
                {
                    pnlCustomer.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkVehicle2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAddVehicle.Visible = true;
            pnlVehicle2.Visible = false;
        }

        

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            //pnlInvoice.Visible = true;
            //pnlInvoice.BringToFront();
            string JobNo = lblSelectJob.Text;
            if (JobNo == "")
            {
                MessageBox.Show("Please Select Job to create Invoice", "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            DialogResult result = MessageBox.Show("Do you want to Create Invoice for the Job Number: " + lblSelectJob.Text + "? ", "Auto Chat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //int JobNoUpdate = Convert.ToInt32(lblSelectJob.Text);
                //string JobUpdtQuery = "UPDATE [dbo].[JobHybridMaster] SET [Status] = 'Finished' WHERE [JobNo] = '" + JobNoUpdate + "'";
                //int a = DAL.DataAccessManager.ExecuteSQL(JobUpdtQuery);
                //btnFinished.Enabled = false;

                BindCurrentJobs();

                if (JobNo != "")
                {
                    //RegisterQ regQ = new RegisterQ(JobNo);
                    //regQ.MdiParent = Application.OpenForms["dashboard"];
                    //regQ.FormBorderStyle = FormBorderStyle.FixedSingle;
                    //regQ.WindowState = FormWindowState.Maximized;

                    //regQ.Show();

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

        private void grdFinisehedJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                //grdFinisehedJobs.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                //grdFinisehedJobs.CurrentRow.DefaultCellStyle.ForeColor = Color.White;


                DataGridViewRow row = grdFinisehedJobs.Rows[e.RowIndex];
                string jobNo = row.Cells["JobNo"].Value.ToString();
                lblSelectJob.Text = jobNo;
                //btnFinished.Enabled = true;
                //btnInvoice.Enabled = true;
                dashboard dash = new dashboard();
                dash.lblJobNo.Text = jobNo;
                dash.btnInvoice.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Workshop management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        

        private void pnlCustomer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvlPrevious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            JobHistory _jobHistory = new JobHistory(txtRegNo.Text);
            _jobHistory.ShowDialog();
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.Rows.Add(txtReg.Text, cmbMake1.Text, cmbModel1.Text, txtYear.Text);
                try
                {
                    int itmCount = dataGridView1.Rows.Count;
                    if (itmCount != 0)
                    {

                        for (int i = 0; i < itmCount; i++)
                        {
                            string RegNo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            string Make = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string Model = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            string year = dataGridView1.Rows[i].Cells[3].Value.ToString();

                            if (RegNo != "")
                            {
                                string VehicleQuery = "INSERT INTO [dbo].[tblVehicle] ([RegNo],[Make],[Model],[Year],[CustomerID]) " +
                                    "VALUES ('" + RegNo + "','" + Make + "','" + Model + "','" + year + "','" + lblCusID.Text + "')";
                                DAL.DataAccessManager.ExecuteSQL(VehicleQuery);
                                MessageBox.Show("Successfully saved");
                            }

                        }
                    }
                }
                catch
                {

                }


                LoadVehicleDetail(lblCusID.Text);

                txtReg.Text = "";
                //txtMake1.Text = "";
                //txtModel1.Text = "";
                txtYear.Text = "";
                dataGridView1.Rows.Clear();

                pnlAddVehicle.Visible = false;
                pnlVehicle.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void linkVehicle2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAddVehicle.Visible = true;
            pnlVehicle.Visible = false;
            BindVehicleMake1();
        }

        private void grdFinisehedJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string MakeID = cmbMake.SelectedValue.ToString();
                DataRowView oDataRowView = cmbMake.SelectedItem as DataRowView;
                string sValue = string.Empty;

                //if (oDataRowView != null)
                //{
                //    sValue = oDataRowView.Row["ID"] as string;
                //}
                sValue = cmbMake.SelectedValue.ToString();
                if (sValue != null || sValue.Length < 2)
                {
                    BindVehicleModel(sValue);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void BindVehicleMake()
        {
            DataSet dsMake = DAL.customer.GetVehicleMake();
            cmbMake.DataSource = dsMake.Tables[0];
            cmbMake.DisplayMember = "Make";
            cmbMake.ValueMember = "ID";
        }


        private void BindVehicleModel(string MakeID)
        {
            string sqlModel = "SELECT ModelID,[Model] FROM[dbo].[tblVehicleModel] where[MakeID] = '" + MakeID + "'";
            DataSet dsModel = DAL.DataAccessManager.GetDataSet(sqlModel);//DAL.customer.GetVehicleModel(MakeID);

            if (dsModel != null)
            {
                cmbModel.DataSource = dsModel.Tables[0];
                cmbModel.DisplayMember = "Model";
                cmbModel.ValueMember = "ModelID";
            }
        }

        private void BindVehicleMake1()
        {
            DataSet dsMake = DAL.customer.GetVehicleMake();
            cmbMake1.DataSource = dsMake.Tables[0];
            cmbMake1.DisplayMember = "Make";
            cmbMake1.ValueMember = "ID";
        }


        private void BindVehicleModel1(string MakeID)
        {
            string sqlModel = "SELECT ModelID,[Model] FROM[dbo].[tblVehicleModel] where[MakeID] = '" + MakeID + "'";
            DataSet dsModel = DAL.DataAccessManager.GetDataSet(sqlModel);//DAL.customer.GetVehicleModel(MakeID);

            if (dsModel != null)
            {
                cmbModel1.DataSource = dsModel.Tables[0];
                cmbModel1.DisplayMember = "Model";
                cmbModel1.ValueMember = "ModelID";
            }
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            pnlAddVehicle.Visible = false;
            pnlVehicle.Visible = true;
        }

        private void cmbMake1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string MakeID = cmbMake1.SelectedValue.ToString();
                //DataRowView oDataRowView = cmbMake1.SelectedItem as DataRowView;
                string sValue = string.Empty;

                //if (oDataRowView != null)
                //{
                //    sValue = oDataRowView.Row["ID"] as string;
                //}
                sValue = cmbMake1.SelectedValue.ToString();
                if (sValue != null || sValue.Length < 2)
                {
                    BindVehicleModel1(sValue);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void kryptonButton16_Click(object sender, EventArgs e)
        {
            tabSRcontrol.SelectTab(tabPageSR_Payment);
            ClearField();
            generate_inv();
        }

        private void btnAssignTech_Click(object sender, EventArgs e)
        {
            
            

            #region Save Tech
            //Insert Technician
            string Tec1Name = "";
            DateTime assignDate;
            string assignTime = "";
            if (cmbEmployee.Text != "-Select-")
            {

                Tec1Name = cmbEmployee.Text;
                assignDate = Convert.ToDateTime(AssignDt1.Value.ToString());
                assignTime = AssignTime1.Value.ToString();

                this.grdTech.Rows.Add(txtJobNo.Text, Tec1Name, assignDate, assignTime,"X");

                //string tecQuery1 = "INSERT INTO [dbo].[JobTechAssign] ([JobNo],[TechName],[AssignDate],[AssignTime]) " +
                //"VALUES ('" + txtJobNo.Text + "','" + Tec1Name + "','" + assignDate + "','" + assignTime + "')";

                //DAL.DataAccessManager.ExecuteSQL(tecQuery1);
            }
            
            //GetAssignTech(txtJobNo.Text);
            
            #endregion

        }

        private void SaveAssignTech()
        {
            int itmCount = grdTech.Rows.Count;
            if (itmCount != 0)
            {
                for (int i = 0; i < itmCount - 1; i++)
                {
                    string jobNo1 = grdTech.Rows[i].Cells[0].Value.ToString();  
                    string Tec1Name = grdTech.Rows[i].Cells[1].Value.ToString();
                    if (Tec1Name == "")
                    {
                        return;
                    }
                    DateTime assignDate = Convert.ToDateTime(grdTech.Rows[i].Cells[2].Value.ToString());
                    string assignTime= grdTech.Rows[i].Cells[3].Value.ToString();
                    
                    string tecQuery1 = "INSERT INTO [dbo].[JobTechAssign] ([JobNo],[TechName],[AssignDate],[AssignTime]) " +
                    "VALUES ('" + jobNo1 + "','" + Tec1Name + "','" + assignDate + "','" + assignTime + "')";

                    DAL.DataAccessManager.ExecuteSQL(tecQuery1);
                }
            }
        }


        private void GetAssignTech(string jobNo)
        {
            try
            {
                DataTable dt = DAL.DataAccessManager.GetDataTable("SELECT [JobNo],[TechName],[AssignDate],[AssignTime] FROM [dbo].[JobTechAssign] WHERE [JobNo] = '" + jobNo + "'");
                //listBox1.Items.Clear();
                //listBox1.DataSource = null;
                //listBox1.DataSource = dt;
                //listBox1.DisplayMember = "TechName";

                int rows = dt.Rows.Count;
                for (int i = 0; i < rows; i++)
                {
                    string jobNo1 = dt.Rows[i][0].ToString();
                    string Tec1Name = dt.Rows[i][1].ToString();
                    DateTime assignDate = Convert.ToDateTime(dt.Rows[i][2].ToString());
                    string assignTime = dt.Rows[i][3].ToString();

                this.grdTech.Rows.Add(jobNo1, Tec1Name, assignDate, assignTime,"X");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgrvSalesItemList_SelectionChanged(object sender, EventArgs e)
        {
            //(sender as DataGridView).CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Red;
            //(sender as DataGridView).CurrentRow.DefaultCellStyle.ForeColor = Color.White;
        }

        private void grdFinisehedJobs_SelectionChanged(object sender, EventArgs e)
        {
            //(sender as DataGridView).CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Red;
            //(sender as DataGridView).CurrentRow.DefaultCellStyle.ForeColor = Color.White;
        }

        private void tabSRcontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSRcontrol.SelectedTab == tabSRcontrol.TabPages["tabPage3"])//your specific tabname
            {
                // your stuff
                BindFinishedJobs();
            }
        }

        private void grdTech_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == grdTech.Columns["delete"].Index && e.RowIndex >= 0)
                {
                    grdTech.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindFinishedJobs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindCurrentJobs();
        }
    }
}