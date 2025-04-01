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
    public partial class addNewCustomer : KryptonForm
    {
        public addNewCustomer()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            addvehicle veh = new addvehicle();
            veh.ShowDialog();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void BindCustomers()
        {
            try
            {
                //string sql = "  select  sales_id as 'Invo_No' , sales_time as Date , payment_amount as Total , " +
                //            "   (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' , " +
                //            "   due_amount as Due, emp_id as 'Sold by' ,    C_id  as Contact , Comment as 'Cust Name/Comment' " +
                //            "   from sales_payment   where C_id = '" + lblCustID.Text + "' order by  sales_id desc";
                string customer = "";
                if(radioButton1.Checked)
                {
                    customer = "Customer";
                }
                else if(radioButton2.Checked)
                {
                    customer = "Supplier";
                }
                //string sql = "SELECT [ID],[Name],[EmailAddress],[Phone],[Address],[City],[PeopleType],[RegDate] FROM [tbl_customer] WHERE [PeopleType] = '" + customer+"' order by Name asc";
                //DAL.DataAccessManager.ExecuteSQL(sql);
                //DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                //dtgviewCusttrxHistory.DataSource = dt1;
                DataSet dtCustomer = DAL.customer.GetCustomer(customer);
                dtgviewCusttrxHistory.DataSource = dtCustomer.Tables[0];

            }
            catch
            {
            }
        }

        private void addNewCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                BindCustomers();
                label10.Visible = false;
                //------
                DataGridViewButtonColumn Edit = new DataGridViewButtonColumn();
                dtgviewCusttrxHistory.Columns.Add(Edit);
                Edit.HeaderText = "Edit";
                Edit.Text = "Edit";
                Edit.Name = "Edit";
                Edit.ToolTipText = "Edit Customer/Supplier";
                Edit.UseColumnTextForButtonValue = true;
                Edit.Width = 30;

                DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                dtgviewCusttrxHistory.Columns.Add(del);
                del.HeaderText = "Del";
                del.Text = "X";
                del.Name = "del";
                del.ToolTipText = "Delete Customer.Supplier";
                del.UseColumnTextForButtonValue = true;
                del.Width = 30;

                DataGridViewColumn ColID = dtgviewCusttrxHistory.Columns[0];
                //ColID.Width = 31;
                DataGridViewColumn ColName = dtgviewCusttrxHistory.Columns[1];
                //ColName.Width = 220;
            }
            catch
            {
            }
        }

        private void dtgviewCusttrxHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgviewCusttrxHistory.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row1 in dtgviewCusttrxHistory.SelectedRows)
                    {
                        DataGridViewRow row = dtgviewCusttrxHistory.Rows[e.RowIndex];
                        string Name = row.Cells["Name"].Value.ToString();
                        string Contact = row.Cells["Phone"].Value.ToString();
                        string Email = row.Cells["EmailAddress"].Value.ToString();
                        string Address = row.Cells["Address"].Value.ToString();
                        string City = row.Cells["City"].Value.ToString();
                        string CusID = row.Cells["ID"].Value.ToString();
                        string Type = row.Cells["PeopleType"].Value.ToString();

                        addvehicle veh = new addvehicle(CusID,Name,Address,City,Email,Type,Contact);
                        veh.ShowDialog();
                        //txtCustomerName.Text = Name;

                        
                    }
                }

                //Delete
                if (e.ColumnIndex == dtgviewCusttrxHistory.Columns["del"].Index && e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow rowdel in dtgviewCusttrxHistory.SelectedRows)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete the record?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (result == DialogResult.Yes)
                        {

                            string sqldel = " delete from tbl_customer where ID = '" + rowdel.Cells[2].Value.ToString() + "'";
                            DAL.DataAccessManager.ExecuteSQL(sqldel);
                            MessageBox.Show("Customer/Supplier Deleted");
                            string sql = "SELECT [ID],[Name],[EmailAddress],[Phone],[Address],[City] as [Vehicle_Reg],[PeopleType],[RegDate] FROM [tbl_customer] order by Name asc";
                            DAL.DataAccessManager.ExecuteSQL(sql);
                            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                            dtgviewCusttrxHistory.DataSource = dt1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            //            btnSave.Text = "Update";
            //        }
            //    }

            //}
            //catch
            //{

            //}
        }


        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            //string CusDetail = "SELECT SELECT [ID],[Name],[EmailAddress],[Phone],[Address],[City],[PeopleType],[RegDate] FROM [tbl_customer] WHERE [Name] LIKE '%" + txtCustomerName.Text + "%' OR [Phone] like '%" + txtCustomerName.Text + "%' OR [City] like '%" + txtCustomerName.Text + "%'";
            //DataTable dtCusDetail = DAL.DataAccessManager.GetDataTable(CusDetail);

            //dtgviewCusttrxHistory.DataSource = dtCusDetail;

            (dtgviewCusttrxHistory.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name like '%{0}%' OR Phone like '%{0}%'", txtCustomerName.Text);

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
            //if (dtCusDetail.Rows.Count == 0)
            //{
            //    label10.Visible = true;
            //}
            //else
            //{
            //    label10.Visible = false;
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton1.Checked = true;
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;
                BindCustomers();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //radioButton2.Checked= true;
            if (radioButton2.Checked)
            {
                radioButton1.Checked= false;
                BindCustomers();
            }
        }
    }
}
