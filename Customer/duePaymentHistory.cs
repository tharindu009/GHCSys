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
    public partial class duePaymentHistory : KryptonForm
    {
        public duePaymentHistory()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            lblcustid.Text = cmbCustomer.SelectedValue.ToString();
            string sql = "  select  sales_id as 'Invo No' , receivedate as Date ,[dueamt] as 'Due Amount', receiveamt as 'Receive Amount'  " +
                                "  from tbl_duepayment where custid = '" + lblcustid.Text + "' order by Date desc ";

            DataSet ds1 = new DataSet();
            ds1 = DAL.DataAccessManager.GetDataSet(sql);
            dtgviewCustDueHistory.DataSource = ds1.Tables[0];

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

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblcustid.Text = cmbCustomer.SelectedValue.ToString();
            string sql = "  select  sales_id as 'Invo No' , receivedate as Date ,[dueamt] as 'Due Amount', receiveamt as 'Receive Amount'  " +
                                "  from tbl_duepayment where custid = '" + lblcustid.Text + "' order by Date desc ";

            DataSet ds1 = new DataSet();
            ds1 = DAL.DataAccessManager.GetDataSet(sql);
            dtgviewCustDueHistory.DataSource = ds1.Tables[0];
        }

        private void duePaymentHistory_Load(object sender, EventArgs e)
        {
            try
            {
                BindCustomer();
                if (lblcustid.Text == "CustID" && lblinvoNo.Text == "00")
                {
                    //string sql = "  select  sales_id as 'Invo No' , receivedate as 'Rec Date' ,[dueamt] as 'Due Amount', receiveamt as 'Receive Amount'  " +
                    //"  from tbl_duepayment order by  id desc ";

                    //DataSet ds1 = new DataSet();
                    //ds1 = DataAccess.GetDataSet(sql);
                    //dtgviewCustDueHistory.DataSource = ds1.Tables[0];
                }
                else
                {
                    string sql = "  select  sales_id as 'Invo No' , receivedate as Date , receiveamt as 'Receive Amount'  " +
                                "  from tbl_duepayment where custid = '" + lblcustid.Text + "' and sales_id = '" + lblinvoNo.Text + "'  order by  id desc ";

                    DAL.DataAccessManager.ExecuteSQL(sql);
                    DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                    dtgviewCustDueHistory.DataSource = dt1;
                }
            }
            catch
            {
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
