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
    public partial class addCategory : KryptonForm
    {
        public addCategory()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
                this.Close();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public string categoryID
        {
            set { lblID.Text = value; }
            get { return lblID.Text; }
        }
        public string categoryName
        {
            set { txtCategoryName.Text = value; btnSave.Text = "Update"; }
            get { return txtCategoryName.Text; }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text == "")
                {
                    MessageBox.Show("Please Fill  Category Name");
                    txtCategoryName.Focus();
                }
                else
                {
                    if (lblID.Text == "-")
                    {
                        string sqlCmd = " insert into tbl_category (category_name)  values ('" + txtCategoryName.Text + "'  )";
                        DAL.DataAccessManager.ExecuteSQL(sqlCmd);
                        txtCategoryName.Text = "";
                        MessageBox.Show("Successfuly Saved");

                    }
                    else  //Update 
                    {
                        string sqlUpdateCmd = " update tbl_category set category_name = '" + txtCategoryName.Text + "'   where ID = '" + lblID.Text + "'";
                        DAL.DataAccessManager.ExecuteSQL(sqlUpdateCmd);
                        // lblMsg.Visible = true;
                        // lblMsg.Text = "Successfully Updated";
                        lblID.Text = "";
                        txtCategoryName.Text = "";
                        MessageBox.Show("Successfuly Update the Category");
                    }
                }
                categorybind();

            }
            catch (Exception exp)
            {
                MessageBox.Show("Sorry\r\n this id already added \n\n " + exp.Message);
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void txtReferNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void categorybind()
        {
            string sql = " select ID, category_name as 'Category' from tbl_category ";
            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdCategory.DataSource = dt1;
        }

        private void addCategory_Load(object sender, EventArgs e)
        {
            try
            {

                categorybind();
                DataGridViewButtonColumn Edit = new DataGridViewButtonColumn();
                grdCategory.Columns.Add(Edit);
                Edit.HeaderText = "Edit";
                Edit.Text = "Edit";
                Edit.Name = "Edit";
                Edit.ToolTipText = "Edit this category";
                Edit.UseColumnTextForButtonValue = true;
                Edit.Width = 30;

                DataGridViewButtonColumn del = new DataGridViewButtonColumn();
                grdCategory.Columns.Add(del);
                del.HeaderText = "Del";
                del.Text = "X";
                del.Name = "del";
                del.ToolTipText = "Delete this category";
                del.UseColumnTextForButtonValue = true;
                del.Width = 30;

                DataGridViewColumn ColID = grdCategory.Columns[0];
                //ColID.Width = 31;
                DataGridViewColumn ColName = grdCategory.Columns[1];
                //ColName.Width = 220;
            }
            catch
            {
            }
        }

        private void grdCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Delete category  
                if (e.ColumnIndex == grdCategory.Columns["del"].Index && e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow rowdel in grdCategory.SelectedRows)
                    {
                        DialogResult result = MessageBox.Show("Do you want to Delete?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (result == DialogResult.Yes)
                        {

                            string sqldel = " delete from tbl_category     where ID = '" + rowdel.Cells[2].Value.ToString() + "'";
                            DAL.DataAccessManager.ExecuteSQL(sqldel);
                            MessageBox.Show("Deleted");
                            categorybind();
                        }
                    }
                }

                // Delete items From Gridview
                if (e.ColumnIndex == grdCategory.Columns["Edit"].Index && e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in grdCategory.SelectedRows)
                    {
                        //this.Hide();
                        //Items.Add_Category mkc = new Items.Add_Category();
                        //mkc.categoryID = row.Cells[2].Value.ToString();
                        //mkc.categoryName = row.Cells[3].Value.ToString();
                        txtCategoryName.Text = row.Cells[3].Value.ToString();
                        lblID.Text = row.Cells[2].Value.ToString();
                        //mkc.MdiParent = this.ParentForm;
                        //mkc.Show();
                    }


                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}
