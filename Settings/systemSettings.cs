using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PointofSale
{
    public partial class systemSettings : KryptonForm
    {
        public systemSettings()
        {
            InitializeComponent();
        } 

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

      
        //bind terminal
        public void terminallist()
        {
            string sqlterminallist = "select Shopid as 'ID', Branchname	 ,Location ,Phone ,  " +
                                     " VAT as 'TAX %' ,Dis as 'Discount %'    from tbl_terminallocation";
            DAL.DataAccessManager.ExecuteSQL(sqlterminallist);
            DataTable dtterminallist = DAL.DataAccessManager.GetDataTable(sqlterminallist);
            dtgrdViewTerminallist.DataSource = dtterminallist;
        }

        private void systemSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //Bind store info 
                string sql3 = "select * from storeconfig";
                DAL.DataAccessManager.ExecuteSQL(sql3);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql3);

                txtCompanyName.Text = dt1.Rows[0].ItemArray[1].ToString();
                txtCompanyAddress.Text = dt1.Rows[0].ItemArray[2].ToString();
                txtPhone.Text = dt1.Rows[0].ItemArray[3].ToString();
                txtVatRegiNo.Text = dt1.Rows[0].ItemArray[4].ToString();
                txtWebSite.Text = dt1.Rows[0].ItemArray[5].ToString();
                lblid.Text = dt1.Rows[0].ItemArray[0].ToString();
                txtVATRate.Text = dt1.Rows[0].ItemArray[6].ToString();
                txtDiscountRate.Text = dt1.Rows[0].ItemArray[7].ToString();
                txtFootermsg.Text = dt1.Rows[0].ItemArray[8].ToString();

                txtTrweb.Text = dt1.Rows[0].ItemArray[5].ToString();
                txtTrFootermsg.Text = dt1.Rows[0].ItemArray[8].ToString();
                terminallist();
                dtgrdViewTerminallist.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (lblShopID.Text == "-")
                {
                    btnAddnew.Visible = false;
                    btnDelete.Visible = false;
                }
                else
                {
                    btnAddnew.Visible = true;
                    btnDelete.Visible = true;
                }
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCompanyName.Text == "" || txtCompanyAddress.Text == "" || txtPhone.Text == "" || txtVATRate.Text == "" || txtDiscountRate.Text == "")
            {
                // MessageBox.Show("You are Not able to Update");
                MessageBox.Show("You are Not able to Update", "Button3 Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    string sql = "update storeconfig set companyname= '" + txtCompanyName.Text + "', companyaddress = '" + txtCompanyAddress.Text + "', " +
                        " companyphone = '" + txtPhone.Text + "', vatno = '" + txtVatRegiNo.Text + "' , web = '" + txtWebSite.Text + "' ,    " +
                         " vatrate = '" + txtVATRate.Text + "', disrate = '" + txtDiscountRate.Text + "' , footermsg = '" + txtFootermsg.Text + "'   " +
                        "  where id = '" + lblid.Text + "'";
                    DAL.DataAccessManager.ExecuteSQL(sql);
                    MessageBox.Show("Configuation has been Saved");
                    

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Sorry\r\n You have to Check the Data " + exp.Message);
                }
            }
        }

        private void btnTrSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtterminalname.Text == "" || txtTerminaladdress.Text == "" || txtTerminalPhone.Text == "" || txtTrVAT.Text == "" || txtTrDis.Text == "")
                {
                    MessageBox.Show("Please fill Terminal info", "Button3 Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Add new Terminal Info
                    if (lblShopID.Text == "-")
                    {
                        string Shopid = txtterminalname.Text.Substring(0, 2) + txtTrVATregino.Text.Substring(0, 2);
                        string sqlinsert = " insert into tbl_terminallocation  (Shopid, CompanyName, Branchname , Location ,Phone , Email ,  Web, VAT , Dis , VATRegiNo , Footermsg ) " +
                                           " values ('" + Shopid + "' , '" + txtCompanyName.Text + "' , '" + txtterminalname.Text + "' , " +
                                            " '" + txtTerminaladdress.Text + "' , '" + txtTerminalPhone.Text + "' , '" + txtTremail.Text + "' ," +
                                            " '" + txtTrweb.Text + "',  '" + txtTrVAT.Text + "', " +
                                            " '" + txtTrDis.Text + "' , '" + txtTrVATregino.Text + "',  '" + txtTrFootermsg.Text + "')";
                        DAL.DataAccessManager.ExecuteSQL(sqlinsert);
                        MessageBox.Show("New Terminal Saved Successfuly");
                        
                        terminallist();
                        tabSRcontrol.SelectedTab = tabterminallist;
                    }
                    else // Update selected 
                    {
                        string sql = "update tbl_terminallocation set Branchname = '" + txtterminalname.Text + "', Location = '" + txtTerminaladdress.Text + "', " +
                        " Email = '" + txtTremail.Text + "' , Phone = '" + txtTerminalPhone.Text + "', VAT = '" + txtTrVAT.Text + "' , Web = '" + txtTrweb.Text + "' ,    " +
                        " Dis = '" + txtTrDis.Text + "', VATRegiNo = '" + txtTrVATregino.Text + "' , Footermsg = '" + txtTrFootermsg.Text + "'   " +
                        " , CompanyName = '" + txtCompanyName.Text + "'  where Shopid = '" + lblShopID.Text + "' ";
                        DAL.DataAccessManager.ExecuteSQL(sql);
                        MessageBox.Show("Terminal info has been Saved");                 
                        terminallist();
                        tabSRcontrol.SelectedTab = tabterminallist;
                    }
                }

            }
            catch
            {
            }
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            txtterminalname.Text = string.Empty;
            txtTerminaladdress.Text = string.Empty;
            txtVatRegiNo.Text = string.Empty;
            txtTrweb.Text = txtWebSite.Text;
            txtTrFootermsg.Text = txtFootermsg.Text;
            lblShopID.Text = "-";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete Terminal "+txtterminalname.Text+"?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                if (lblShopID.Text == "-")
                {
                    // MessageBox.Show("You are Not able to Update");
                    MessageBox.Show("You are Not able to Delete", "Button3 Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        string sql = "delete from tbl_terminalLocation where Shopid ='" + lblShopID.Text + "'";
                        DAL.DataAccessManager.ExecuteSQL(sql);
                        MessageBox.Show("successfully Data Delete !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        terminallist();
                        tabSRcontrol.SelectedTab = tabterminallist;


                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Sorry\r\n You have to Check the Data" + exp.Message);
                    }
                }
            }
        }

        private void dtgrdViewTerminallist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dtgrdViewTerminallist.Rows[e.RowIndex];
                string terminalid = row.Cells[0].Value.ToString();


                string sqlterminallist = "select Shopid  , Branchname , Location ,Phone , Email , " +
                                         " Web, VAT , Dis , VATRegiNo , Footermsg    from tbl_terminalLocation " +
                                         " where Shopid = '" + terminalid + "' ";
                DAL.DataAccessManager.ExecuteSQL(sqlterminallist);
                DataTable dtterminallist = DAL.DataAccessManager.GetDataTable(sqlterminallist);

                lblShopID.Text = dtterminallist.Rows[0].ItemArray[0].ToString();
                txtterminalname.Text = dtterminallist.Rows[0].ItemArray[1].ToString();
                txtTerminaladdress.Text = dtterminallist.Rows[0].ItemArray[2].ToString();
                txtTerminalPhone.Text = dtterminallist.Rows[0].ItemArray[3].ToString();
                txtTremail.Text = dtterminallist.Rows[0].ItemArray[4].ToString();
                txtTrweb.Text = dtterminallist.Rows[0].ItemArray[5].ToString();
                txtTrVAT.Text = dtterminallist.Rows[0].ItemArray[6].ToString();
                txtTrDis.Text = dtterminallist.Rows[0].ItemArray[7].ToString();
                txtTrVATregino.Text = dtterminallist.Rows[0].ItemArray[8].ToString();
                txtTrFootermsg.Text = dtterminallist.Rows[0].ItemArray[9].ToString();
                tabSRcontrol.SelectedTab = tabterminal;
                btnAddnew.Visible = true;
                btnDelete.Visible = true;
            }
            catch
            {
            }
        }

        private void txtVATRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool ignoreKeyPress = false;

                bool matchString = Regex.IsMatch(txtVATRate.Text.ToString(), @"\.\d\d\d");

                if (e.KeyChar == '\b') // Always allow a Backspace
                    ignoreKeyPress = false;
                else if (matchString)
                    ignoreKeyPress = true;
                else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    ignoreKeyPress = true;
                else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    ignoreKeyPress = true;

                e.Handled = ignoreKeyPress;
                //using System.Text.RegularExpressions;
            }
            catch
            {
            }
        }

        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool ignoreKeyPress = false;

                bool matchString = Regex.IsMatch(txtDiscountRate.Text.ToString(), @"\.\d\d\d");

                if (e.KeyChar == '\b') // Always allow a Backspace
                    ignoreKeyPress = false;
                else if (matchString)
                    ignoreKeyPress = true;
                else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    ignoreKeyPress = true;
                else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    ignoreKeyPress = true;

                e.Handled = ignoreKeyPress;
                //using System.Text.RegularExpressions;
            }
            catch
            {
            }
        }

        private void txtTrVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool ignoreKeyPress = false;

                bool matchString = Regex.IsMatch(txtTrVAT.Text.ToString(), @"\.\d\d\d");

                if (e.KeyChar == '\b') // Always allow a Backspace
                    ignoreKeyPress = false;
                else if (matchString)
                    ignoreKeyPress = true;
                else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    ignoreKeyPress = true;
                else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    ignoreKeyPress = true;

                e.Handled = ignoreKeyPress;
                //using System.Text.RegularExpressions;
            }
            catch
            {
            }
        }

        private void txtTrDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool ignoreKeyPress = false;

                bool matchString = Regex.IsMatch(txtTrDis.Text.ToString(), @"\.\d\d\d");

                if (e.KeyChar == '\b') // Always allow a Backspace
                    ignoreKeyPress = false;
                else if (matchString)
                    ignoreKeyPress = true;
                else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    ignoreKeyPress = true;
                else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    ignoreKeyPress = true;

                e.Handled = ignoreKeyPress;
                //using System.Text.RegularExpressions;
            }
            catch
            {
            }
        }
    }
}
