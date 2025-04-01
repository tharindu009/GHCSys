using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using ComponentFactory.Krypton.Toolkit;
using Microsoft.VisualStudio.Services.WebApi;
using static Microsoft.TeamFoundation.Common.Internal.NativeMethods;

namespace PointofSale
{
    public partial class addvehicle : KryptonForm
    {
        public addvehicle()
        {
            InitializeComponent();
        }

        public addvehicle(string ID,string Name,string Address,string City,string Email,string Type,string phone)
        {
            InitializeComponent();
            CustID = ID;
            CustName = Name;
            CustAddress = Address;
            CustPhone=phone;
            CustCity = City;
            CustEmail = Email;
            PeopleType = Type;
        }

        public string CustID
        {
            set
            {
                lblCustID.Text = value;
                //lnkCustomers.Visible = false;
                //dtgviewCusttrxHistory.Visible = true;
                //lblcuthistorylabel.Visible = true;
                //lbltoplabel.Visible = true;

            }
            get
            {
                return lblCustID.Text;

            }
        }

        public string CustName
        {
            set
            {
                txtCustomerName.Text = value;
                btnSave.Text = "Update";
            }
            get
            {
                return txtCustomerName.Text;
            }
        }

        public string CustPhone
        {
            set
            {
                txtPhone.Text = value;

            }
            get
            {
                return txtPhone.Text;

            }
        }

        public string CustCity
        {
            set
            {
                txtCity.Text = value;
            }
            get
            {
                return txtCity.Text;
            }
        }


        public string CustEmail
        {
            set
            {
                txtEmailAddress.Text = value;
            }
            get
            {
                return txtEmailAddress.Text;
            }
        }

        public string CustAddress
        {
            set
            {
                txtCustomerAddress.Text = value;
            }
            get
            {
                return txtCustomerAddress.Text;
            }
        }

        public string PeopleType
        {
            set
            {

                CombPeopleType.Text = value;
            }
            get
            {
                return CombPeopleType.Text;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void clearform()
        {
            CombPeopleType.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            lblVehicleReg.Text = "...";
            grdVehicle.Rows.Clear();
            lblCustID.Text = ".";
            panel1.Visible = false;
            txtReg.Text = "";
            addNewCustomer cus = new addNewCustomer();
            cus.BindCustomers();

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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCustID.Text == ".")
                {
                    // if (txtPeopleID.Text == "") { MessageBox.Show("Please Fill ID"); txtPeopleID.Focus(); } else
                    if (txtCustomerName.Text == "") { MessageBox.Show("Please Fill Name"); txtCustomerName.Focus(); }
                    else if (txtPhone.Text == "") { MessageBox.Show("Please Fill Phone"); txtPhone.Focus(); }
                    else if (CombPeopleType.Text == "") { MessageBox.Show("Please Fill People Type"); CombPeopleType.Focus(); }
                    else if (txtCity.Text == "" && CombPeopleType.Text == "Customer") { MessageBox.Show("Please Fill Vehicle Reg No"); txtCity.Focus(); }
                    else if (txtCustomerAddress.Text == "") { MessageBox.Show("Please Fill  Address"); txtCustomerAddress.Focus(); }
                    else
                    {
                        string sqlCmd = "insert into tbl_customer (Name, EmailAddress, Phone, address, City, PeopleType,[RegDate])  values ('" + txtCustomerName.Text + "', '" + txtEmailAddress.Text + "', '" + txtPhone.Text + "', '" + txtCustomerAddress.Text + "', '" + txtCity.Text + "', '" + CombPeopleType.Text + "',GETDATE())";
                        DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                        
                        
                        string customersql = "Select ID from tbl_customer where Phone = '" + txtPhone.Text+"'";
                        DataTable dtCus = DAL.DataAccessManager.GetDataTable(customersql);
                        string NewCustomerID = "";
                        if (dtCus.Rows.Count != 0)
                        {
                            NewCustomerID = dtCus.Rows[0][0].ToString();
                        }



                        //Save Vehicle Detail
                        int itmCount = grdVehicle.Rows.Count;
                        if (itmCount != 0)
                        {
                            for (int i = 0; i < itmCount; i++)
                            {
                                string RegNo = grdVehicle.Rows[i].Cells[0].Value.ToString();
                                string Make = grdVehicle.Rows[i].Cells[1].Value.ToString();
                                string Model = grdVehicle.Rows[i].Cells[2].Value.ToString();
                                string year = grdVehicle.Rows[i].Cells[3].Value.ToString();
                                

                                string VehicleQuery = "INSERT INTO [dbo].[tblVehicle] ([RegNo],[Make],[Model],[Year],[CustomerID]) " +
                                    "VALUES ('"+RegNo+"','"+Make+"','"+Model+"','"+year+"','"+NewCustomerID+"')";
;

                                DAL.DataAccessManager.ExecuteSQL(VehicleQuery);
                            }
                        }
                        MessageBox.Show("Successfully saved");
                        clearform();
                        this.Close();
                    }
                }
                else  //Update 
                {
                    string sqlUpdateCmd = "update tbl_customer set Name = '" + txtCustomerName.Text + "', EmailAddress= '" + txtEmailAddress.Text + "', address = '" + txtCustomerAddress.Text + "', Phone = '" + txtPhone.Text + "', City = '" + txtCity.Text + "' , PeopleType = '" + CombPeopleType.Text + "'   where ID = '" + lblCustID.Text + "'";
                    DAL.DataAccessManager.ExecuteSQL(sqlUpdateCmd);

                    //Update Vehicle Detail

                    string DeleteVehicle = "DELETE FROM [dbo].[tblVehicle] WHERE CustomerID = '"+lblCustID.Text+"'";
                    DAL.DataAccessManager.ExecuteSQL(DeleteVehicle);

                    int itmCount = grdVehicle.Rows.Count;
                    if (itmCount != 0)
                    {
                        for (int i = 0; i < itmCount; i++)
                        {
                            string RegNo = grdVehicle.Rows[i].Cells[0].Value.ToString();
                            string Make = grdVehicle.Rows[i].Cells[1].Value.ToString();
                            string Model = grdVehicle.Rows[i].Cells[2].Value.ToString();
                            string year = grdVehicle.Rows[i].Cells[3].Value.ToString();


                            string VehicleQuery = "INSERT INTO [dbo].[tblVehicle] ([RegNo],[Make],[Model],[Year],[CustomerID]) " +
                                "VALUES ('" + RegNo + "','" + Make + "','" + Model + "','" + year + "','" + lblCustID.Text + "')";
                            DAL.DataAccessManager.ExecuteSQL(VehicleQuery);
                        }
                    }
                    //---------------------

                    clearFields();
                    MessageBox.Show("Successfully Updated");
                    this.Close();
                }


            }
            catch (Exception exp)
            {
                MessageBox.Show("Sorry\r\n this id already added \n\n " + exp.Message);
            }

        }


        private void GetVehicleDetails(string CusID)
        {
            try
            {
                if (grdVehicle.Rows.Count == 0)
                {
                    //string ItemQuery = "SELECT [ID],[RegNo],[Make],[Model],[Year],[CustomerID] FROM [dbo].[tblVehicle] WHERE CustomerID = '" + CusID + "'";
                    //DataSet VehicleDS = DAL.DataAccessManager.GetDataSet(ItemQuery);

                    DataSet VehicleDS = DAL.customer.GetCustomerVehicle(CusID);

                    int rows = VehicleDS.Tables[0].Rows.Count;
                    if (rows > 0)
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            string RegNo = VehicleDS.Tables[0].Rows[i][1].ToString();
                            string Make = VehicleDS.Tables[0].Rows[i][2].ToString();
                            string Model = VehicleDS.Tables[0].Rows[i][3].ToString();
                            string Year = VehicleDS.Tables[0].Rows[i][4].ToString();

                            this.grdVehicle.Rows.Add(RegNo, Make, Model, Year);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearFields()
        {
            txtCity.Text = "";
            txtCustomerAddress.Text = "";
            txtCustomerName.Text = "";
            txtEmailAddress.Text = "";
            txtPhone.Text = "";
            lblCustID.Text = ".";
            btnSave.Text = "Save";
            panel1.Visible = false;
            txtReg.Text = "";
            txtYear.Text = "";
            grdVehicle.Rows.Clear();
            lblQR.Visible = false;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        

        private void addNewCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                //checkBox1.Checked = true;
                //BindCustomers();
                //BindVehicleModel();
                //BindVehicleMake();
                ToolTipText();
                label10.Visible = false;
                if(CustID!="")
                {
                    GetVehicleDetails(CustID);
                    label1.Text = "Update Customer/ Supplier";
                }
                else
                {
                    label1.Text = "Add New Customer/ Supplier";
                }
                //------
                //DataGridViewButtonColumn Edit = new DataGridViewButtonColumn();
                //dtgviewCusttrxHistory.Columns.Add(Edit);
                //Edit.HeaderText = "Edit";
                //Edit.Text = "Edit";
                //Edit.Name = "Edit";
                //Edit.ToolTipText = "Edit Customer/Supplier";
                //Edit.UseColumnTextForButtonValue = true;
                //Edit.Width = 30;

                //DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                //dtgviewCusttrxHistory.Columns.Add(del);
                //del.HeaderText = "Del";
                //del.Text = "X";
                //del.Name = "del";
                //del.ToolTipText = "Delete Customer.Supplier";
                //del.UseColumnTextForButtonValue = true;
                //del.Width = 30;

                //DataGridViewColumn ColID = dtgviewCusttrxHistory.Columns[0];
                ////ColID.Width = 31;
                //DataGridViewColumn ColName = dtgviewCusttrxHistory.Columns[1];
                ////ColName.Width = 220;
            }
            catch (Exception ex)
            {
            }
        }

        private void ToolTipText()
        {
            toolTip1.SetToolTip(btnAddMake, "Add New Vehicle Make");
            toolTip1.SetToolTip(btnAddModel, "Add New Vehicle Model");
        }

        private void dtgviewCusttrxHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == dtgviewCusttrxHistory.Columns["Edit"].Index && e.RowIndex >= 0)
            //    {
            //        foreach (DataGridViewRow row1 in dtgviewCusttrxHistory.SelectedRows)
            //        {
            //            DataGridViewRow row = dtgviewCusttrxHistory.Rows[e.RowIndex];
            //            string Name = row.Cells["Name"].Value.ToString();
            //            string Contact = row.Cells["Phone"].Value.ToString();
            //            string Email = row.Cells["EmailAddress"].Value.ToString();
            //            string Address = row.Cells["Address"].Value.ToString();
            //            string City = row.Cells["City"].Value.ToString();
            //            string CusID = row.Cells["ID"].Value.ToString();
            //            string Type = row.Cells["PeopleType"].Value.ToString();


            //            txtCustomerName.Text = Name;
            //            txtPhone.Text = Contact;
            //            txtEmailAddress.Text = Email;
            //            txtCity.Text = City;
            //            CombPeopleType.Text = Type;
            //            lblCustID.Text = CusID;
            //            txtCustomerAddress.Text = Address;


            //            //Get Vehicle Detail
            //            GetVehicleDetails(CusID);
            //            //------------------
            //            //lblQR.Visible = true;

            //            btnSave.Text = "Update";
            //        }
            //    }

            //    //Delete
            //    if (e.ColumnIndex == dtgviewCusttrxHistory.Columns["del"].Index && e.RowIndex >= 0)
            //    {
            //        foreach (DataGridViewRow rowdel in dtgviewCusttrxHistory.SelectedRows)
            //        {
            //            DialogResult result = MessageBox.Show("Do you want to Delete the record?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //            if (result == DialogResult.Yes)
            //            {

            //                string sqldel = " delete from tbl_customer where ID = '" + rowdel.Cells[2].Value.ToString() + "'";
            //                DAL.DataAccessManager.ExecuteSQL(sqldel);
            //                MessageBox.Show("Customer/Supplier Deleted");
            //                //string sql = "SELECT [ID],[Name],[EmailAddress],[Phone],[Address],[City],[PeopleType],[RegDate] FROM [tbl_customer] order by Name asc";
            //                //DAL.DataAccessManager.ExecuteSQL(sql);
            //                //DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            //                //dtgviewCusttrxHistory.DataSource = dt1;
            //                BindCustomers();
            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dtgviewCusttrxHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    if (e.ColumnIndex == dtgviewCusttrxHistory.Columns["Edit"].Index && e.RowIndex >= 0)
            //    {
            //        foreach (DataGridViewRow row1 in dtgviewCusttrxHistory.SelectedRows)
            //        {
            //            DataGridViewRow row = dtgviewCusttrxHistory.Rows[e.RowIndex];
            //            string Name = row.Cells["Name"].Value.ToString();
            //            string Contact = row.Cells["Phone"].Value.ToString();
            //            string Email = row.Cells["EmailAddress"].Value.ToString();
            //            string Address = row.Cells["Address"].Value.ToString();
            //            string City = row.Cells["City"].Value.ToString();
            //            string CusID = row.Cells["ID"].Value.ToString();
            //            string Type = row.Cells["PeopleType"].Value.ToString();


            //            txtCustomerName.Text = Name;
            //            txtPhone.Text = Contact;
            //            txtEmailAddress.Text = Email;
            //            txtCity.Text = City;
            //            CombPeopleType.Text = Type;
            //            lblCustID.Text = CusID;
            //            txtCustomerAddress.Text = Address;

            //            btnSave.Text = "Update";

                        

            //            if (checkBox2.Checked)
            //            {
            //                lblQR.Visible = false;
            //            }
            //            else
            //            {
            //                //Get Vehicle Detail
            //                GetVehicleDetails(CusID);
            //                //------------------
            //                //lblQR.Visible = true;
            //            }
            //        }
            //    }

            //}
            //catch
            //{

            //}
        }

        

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            BindVehicleMake();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            //string CusDetail = "SELECT [Name],[Phone],[City] as [Vehicle Reg],VMake as Make,VModel as Model,[ID] FROM [tbl_customer] WHERE [Name] LIKE '%" + txtCustomerName.Text + "%' OR [Phone] like '%" + txtCustomerName.Text + "%' OR [City] like '%" + txtCustomerName.Text + "%'";
            //DataTable dtCusDetail = DAL.DataAccessManager.GetDataTable(CusDetail);
            
            //dtgviewCusttrxHistory.DataSource = dtCusDetail;

            //------

            //if (dtCusDetail.Rows.Count == 0)
            //{
            //    label10.Visible = true;
            //}
            //else
            //{
            //    label10.Visible = false;
            //}
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.grdVehicle.Rows.Add(txtReg.Text, cmbMake.Text, cmbModel.Text, txtYear.Text);
                panel1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblQR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            if (lblCustID.Text != ".")
            {
                QRcode frmQR = new QRcode(lblCustID.Text);
                frmQR.ShowDialog();
            }
        }

        private void grdVehicle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //foreach (DataGridViewRow row1 in grdVehicle.SelectedRows)
            //{
            //    DataGridViewRow row = grdVehicle.Rows[e.RowIndex];
            //    string vehicleReg = row.Cells["RegNo"].Value.ToString();
            //    lblVehicleReg.Text = vehicleReg;
            //}
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            clearform();
            btnSave.Text = "Save";
        }

        private void CombPeopleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CombPeopleType.Text == "Supplier")
            {
                linkLabel1.Visible = false;
            }
            else
            {
                linkLabel1.Visible = true;
            }
        }

        

       

        private void cmbMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbMake_SelectedValueChanged(object sender, EventArgs e)
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

        private void btnAddMake_Click(object sender, EventArgs e)
        {
            if (pnlAddMake.Visible)
            {
                pnlAddMake.Visible = false;
            }
            else
            {
                pnlAddMake.Visible = true;
            }
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            if (pnlAddModel.Visible)
            {
                pnlAddModel.Visible = false;
            }
            else
            {
                pnlAddModel.Visible = true; 
                LblMake.Text = cmbMake.Text;
                lblMakeID.Text = cmbMake.SelectedValue.ToString();
            }
        }

        private void btnAddVehicleMake_Click(object sender, EventArgs e)
        {
            try
            {
                string NewMake = txtAddMake.Text;
                DAL.DataAccessManager.ExecuteSQL("INSERT INTO [dbo].[tblVehicleMake] ([Make])  VALUES ('" + NewMake + "')");
                pnlAddMake.Visible = false;
                BindVehicleMake();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddVehicleModel_Click(object sender, EventArgs e)
        {
            try
            {
                string NewModel = txtAddModel.Text;
                DAL.DataAccessManager.ExecuteSQL("INSERT INTO [dbo].[tblVehicleModel] ([MakeID], [Model]) VALUES ('"+lblMakeID.Text+"','"+txtAddModel.Text+"')");
                pnlAddModel.Visible = false;
                BindVehicleModel(lblMakeID.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtAddMake_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtAddModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtAddMake_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
