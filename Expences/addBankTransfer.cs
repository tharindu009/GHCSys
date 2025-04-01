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
    public partial class addBankTransfer : KryptonForm
    {
        public addBankTransfer()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string BankName = txtBankName.Text;
                string BankBranch = txtBranch.Text;
                DateTime DepositDate = Convert.ToDateTime(dtReceiveDate.Text.ToString());
                double DepositAmount = Convert.ToDouble(txtAmount.Text);
                string depositBy = txtDepositBy.Text;
                string Note = txtNote.Text;


                string Query = "INSERT INTO [dbo].[tbl_BankDeposit] ([BankName],[Branch],[DepositAmount],[DepositDate],[DepositBy],[Note])" +
                                "VALUES ('" + BankName + "','" + BankBranch + "','" + DepositAmount + "','" + DepositDate + "','" + depositBy + "','" + Note + "')";

                DAL.DataAccessManager.ExecuteSQL(Query);

                MessageBox.Show("Sucessfuly Saved");

                string sql = "SELECT [Txn_ID],[BankName],[Branch],[DepositAmount],[DepositDate],[DepositBy],[Note] FROM[dbo].[tbl_BankDeposit] ORDER By DepositDate DESC";

                //DataAccess.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdDeposit.DataSource = dt1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addBankTransfer_Load(object sender, EventArgs e)
        {
            dtReceiveDate.Format = DateTimePickerFormat.Custom;
            dtReceiveDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";

            try
            {
                string sql = "SELECT [Txn_ID],[BankName],[Branch],[DepositAmount],[DepositDate],[DepositBy],[Note] FROM[dbo].[tbl_BankDeposit] ORDER By DepositDate DESC";

                //DataAccess.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdDeposit.DataSource = dt1;
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
