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
    public partial class dueList : KryptonForm
    {
        public dueList()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void dueList_Load(object sender, EventArgs e)
        {
            pnlReceiveDue.Visible = false;
            dateTimeDue.Format = DateTimePickerFormat.Custom;
            dateTimeDue.CustomFormat = "yyyy-MM-dd";
            BindCustomer();

            grdDueList.EnableHeadersVisualStyles = false;
            grdDueList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grdDueList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            grdDueList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total , " +
                         " (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' , " +
                         "  due_amount as Due, emp_id as 'Sold by' ,   c.Name  as Customer , Comment as 'Comment' " +
                         " from sales_payment p, tbl_customer c where convert(nvarchar, c.ID)=p.C_id and due_amount !='0'  ";

            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdDueList.DataSource = dt1;
            grdDueList.Columns[5].DefaultCellStyle.ForeColor = Color.DarkViolet;



            //this.datagridDueList.EnableHeadersVisualStyles = false;
            //this.datagridDueList.Columns[5].HeaderCell.Style.BackColor = Color.Red;


            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            grdDueList.Columns.Add(btn);
            btn.HeaderText = "Receive";
            btn.Text = "+";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

            double sum = 0.00;
            foreach (DataRow dr in dt1.Rows)
            {
                sum = sum + Convert.ToDouble(dr.ItemArray[5]);
            }
            label5.Text = sum.ToString();
        }

        private void UpdateDueList()
        {
            pnlReceiveDue.Visible = false;

            grdDueList.EnableHeadersVisualStyles = false;
            grdDueList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grdDueList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            grdDueList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total , " +
                         " (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' , " +
                         "  due_amount as Due, emp_id as 'Sold by' ,   C_id  as CustID , Comment as 'Cust Name/Comment' " +
                         " from sales_payment where due_amount !='0'  ";
            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdDueList.DataSource = dt1;
            grdDueList.Columns[5].DefaultCellStyle.ForeColor = Color.DarkViolet;



            //this.datagridDueList.EnableHeadersVisualStyles = false;
            //this.datagridDueList.Columns[5].HeaderCell.Style.BackColor = Color.Red;


            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            //grdDueList.Columns.Add(btn);
            //btn.HeaderText = "Receive";
            //btn.Text = "+";
            //btn.Name = "btn";
            //btn.UseColumnTextForButtonValue = true;

            double sum = 0.00;
            foreach (DataRow dr in dt1.Rows)
            {
                sum = sum + Convert.ToDouble(dr.ItemArray[5]);
            }
            label5.Text = sum.ToString();
        }

        private void BindCustomer()
        {
            string sqlCust = "select   DISTINCT  *   from tbl_customer where PeopleType = 'Customer'";
            DAL.DataAccessManager.ExecuteSQL(sqlCust);
            DataTable dtCust = DAL.DataAccessManager.GetDataTable(sqlCust);
            cmbCustomer.DataSource = dtCust;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "ID";
            //cmbCustomer.Text = "Guest";
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grdDueList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                DataGridViewRow row = grdDueList.Rows[e.RowIndex];
                lbsalesid.Text = row.Cells[1].Value.ToString();
                lbdate.Text = row.Cells[2].Value.ToString();
                lbtotalamt.Text = row.Cells[3].Value.ToString();
                lbpaidamt.Text = row.Cells[4].Value.ToString();
                lbDueAmount.Text = row.Cells[6].Value.ToString();
                lbcontact.Text = row.Cells[9].Value.ToString();
                pnlReceiveDue.Visible = true;
            }
            catch
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtReceive.Text == "")
            {
                // MessageBox.Show("You are Not able to Update");
                MessageBox.Show("You are Not able to Update", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (Convert.ToDouble(txtReceive.Text) <= Convert.ToDouble(lbDueAmount.Text))
                    {
                        double Receiveamt = Convert.ToDouble(lbDueAmount.Text) - Convert.ToDouble(txtReceive.Text);
                        string sql = "UPDATE sales_payment set due_amount = '" + Receiveamt + "'   where (sales_id = '" + lbsalesid.Text + "')";
                        DAL.DataAccessManager.ExecuteSQL(sql);

                        //Insert Due payment history
                        double remainingdeu = Convert.ToDouble(lbDueAmount.Text) - Convert.ToDouble(txtReceive.Text);
                        string sqlreceivedue = " insert into tbl_duepayment (receivedate, sales_id, totalamt , dueamt, receiveamt , custid) " +
                                                " values ('" + dtReceiveDate.Text + "' , '" + lbsalesid.Text + "', '" + lbtotalamt.Text + "', " +
                                                " '" + remainingdeu + "', '" + txtReceive.Text + "', '" + lbcontact.Text + "') ";
                        DAL.DataAccessManager.ExecuteSQL(sqlreceivedue);

                        MessageBox.Show("Successfully Data Updated!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReceive.Text = string.Empty;
                        pnlReceiveDue.Visible = false;
                        UpdateDueList();
                    }
                    else
                    {
                        MessageBox.Show("You are Not able to Update \n\n Excced Due amount ", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch
                {
                }
            }
                
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total ,  " +
                             " (payment_amount - due_amount) as 'Paid Amount' , payment_type as 'Payment Type' , " +
                             "  due_amount as Due, emp_id as 'Sold by' ,    C_id  as Contact , Comment as 'Cust Name/Comment' " +
                             "  from sales_payment where sales_id = '" + txtsearch.Text + "' and due_amount !='0'  ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdDueList.DataSource = dt1;

                double sum = 0.00;
                foreach (DataRow dr in dt1.Rows)
                {
                    sum = sum + Convert.ToDouble(dr.ItemArray[5]);
                }
                label5.Text = sum.ToString();
            }
            catch
            {
            }
        }

        private void dateTimeDue_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total , " +
                            " (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' ,  " +
                            " due_amount as Due, emp_id as 'Sold by' ,    C_id  as Contact , Comment   " +
                            " from sales_payment where sales_time = '" + dateTimeDue.Text + "' and due_amount !='0'  ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdDueList.DataSource = dt1;

                double sum = 0.00;
                foreach (DataRow dr in dt1.Rows)
                {
                    sum = sum + Convert.ToDouble(dr.ItemArray[5]);
                }
                label5.Text = sum.ToString();
            }
            catch
            {
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Checked = false;
            }
            string custID = cmbCustomer.SelectedValue.ToString();
            string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total , " +
                           " (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' ,  " +
                           " due_amount as Due, emp_id as 'Sold by' ,    C_id  as Contact , Comment   " +
                           " from sales_payment where c_id = '" + custID + "' and due_amount !='0'  ";

            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdDueList.DataSource = dt1;

            double sum = 0.00;
            foreach (DataRow dr in dt1.Rows)
            {
                sum = sum + Convert.ToDouble(dr.ItemArray[5]);
            }
            label5.Text = sum.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                string sql = "select  sales_id as 'Invoice No' , sales_time as Date , payment_amount as Total , " +
                             " (payment_amount - due_amount) as 'Paid Amount' ,  payment_type as 'Payment Type' , " +
                             "  due_amount as Due, emp_id as 'Sold by' ,   C_id  as CustID , Comment as 'Cust Name/Comment' " +
                             " from sales_payment where due_amount !='0'  ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdDueList.DataSource = dt1;


                //this.datagridDueList.EnableHeadersVisualStyles = false;
                //this.datagridDueList.Columns[5].HeaderCell.Style.BackColor = Color.Red;


                double sum = 0.00;
                foreach (DataRow dr in dt1.Rows)
                {
                    sum = sum + Convert.ToDouble(dr.ItemArray[5]);
                }
                label5.Text = sum.ToString();

            }
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            pnlReceiveDue.Visible = false;
        }
    }
}
