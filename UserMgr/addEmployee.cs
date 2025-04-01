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
using PointofSale.UserMgr;

namespace PointofSale
{
    public partial class addEmployee : KryptonForm
    {
        public addEmployee()
        {
            InitializeComponent();
        }

        public string Uid
        {
            set { lblUid.Text = value; }
            get { return lblUid.Text; }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please Add User full Name", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Focus();
            }
            else if (txtLastName.Text == "")
            {
                MessageBox.Show("Please fill Last name", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastName.Focus();
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Please Add Address", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
            }
            else if (txtContact.Text == "")
            {
                MessageBox.Show("Please Add Contact Number", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContact.Focus();
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Please Add Username \n Username should be unique", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Please Add  Email address", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Please Add  Password", "Fill Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
            }
            else
            {
                try
                {

                    int flag;
                    if (rdoAdmin.Checked)
                    {
                        flag = 1;
                    }
                    else if (rdoManager.Checked)
                    {
                        flag = 2;
                    }
                    else if (rdoSale.Checked)
                    {
                        flag = 3;
                    }
                    else if (rdoBlock.Checked)
                    {
                        flag = 0;
                    }
                    else
                    {
                        flag = 0;
                    }
                    string posi;
                    if (rdoAdmin.Checked)
                    {
                        posi = "Admin";
                    }
                    else if (rdoManager.Checked)
                    {
                        posi = "Manager";
                    }
                    else if (rdoSale.Checked)
                    {
                        posi = "Salesman";
                    }
                    else if (rdoBlock.Checked)
                    {
                        posi = "Block";
                    }
                    else
                    {
                        posi = "0";
                    }

                    //New Insert / New Entry
                    if (lblUid.Text == ".")
                    {
                        //string selno = txtUid.Text;
                        string imageName = "";//txtUid.Text + lblFileExtension.Text; //System.IO.Path.GetFileName(openFileDialog1.FileName);
                        //UserImage = DataAccess.imageToByteArray(picUserimage.Image);
                        string dob = "";


                        string sql1 = "insert into usermgt (Name, Father_name, Address, Email , Contact, DOB , Username , password , usertype , position , imagename, Shopid ) " +
                                      "  values('" + txtFirstName.Text + "', '" + txtLastName.Text + "', '" + txtAddress.Text + "', '" + txtEmail.Text + "', " +
                                      " '" + txtContact.Text + "',  '" + dob+ "', '" + txtUserName.Text + "', '" + txtPassword.Text + "', " +
                                      " '" + flag + "', '" + posi + "' , ' ' , '" + cmbLocation.SelectedValue + "')";


                        DAL.DataAccessManager.ExecuteSQL(sql1);


                        /////////////////////picture upload  /////////////////
                        //string path = Application.StartupPath + @"\IMAGE\";
                        //System.IO.File.Delete(path + @"\" + imageName);
                        //if (!System.IO.Directory.Exists(path))
                        //    System.IO.Directory.CreateDirectory(Application.StartupPath + @"\IMAGE\");
                        //string filename = path + @"\" + openFileDialog1.SafeFileName;
                        //picUserimage.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        //System.IO.File.Move(path + @"\" + openFileDialog1.SafeFileName, path + @"\" + imageName);
                        MessageBox.Show("User hase been Created Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblEmailerrormsg.Visible = false;

                        //User_mgt.Manage_user go = new User_mgt.Manage_user();
                        //go.MdiParent = this.ParentForm;
                        //go.Show();
                        //this.Close();
                    }
                    else // Update info
                    {
                        string imageName="";
                        string dtDOB = "2023-01-01";
                        //if (lblFileExtension.Text == "user.png")
                        //{
                        //    imageName = lblimagename.Text;  //Unchange pictures
                        //}
                        //else  //When change 
                        //{
                        //    imageName = lblUid.Text + lblFileExtension.Text;
                        //}

                        string sql = "UPDATE usermgt set  Name = '" + txtFirstName.Text + "', Father_name = '" + txtLastName.Text + "', " +
                        " Address = '" + txtAddress.Text + "', Email = '" + txtEmail.Text + "', Contact = '" + txtContact.Text + "', " +
                        " DOB = '" + dtDOB + "' , Username= '" + txtUserName.Text + "', password = '" + txtPassword.Text + "',imagename = '" + imageName + "' ,    " +
                        " usertype    = '" + flag + "', position = '" + posi + "', Shopid = '" + cmbLocation.SelectedValue + "' where (id = '" + lblUid.Text + "' )";
                        DAL.DataAccessManager.ExecuteSQL(sql);


                        /////////////////////////////////////////////Update image //////////////////////////////////////////////////////
                        //if (lblFileExtension.Text != "user.png")
                        //{
                        //    picUserimage.InitialImage.Dispose();
                        //    string path = Application.StartupPath + @"\IMAGE\";
                        //    System.IO.File.Delete(path + @"\" + lblimagename.Text);
                        //    if (!System.IO.Directory.Exists(path))
                        //        System.IO.Directory.CreateDirectory(Application.StartupPath + @"\IMAGE\");
                        //    string filename = path + @"\" + openFileDialog1.SafeFileName;
                        //    picUserimage.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        //    System.IO.File.Move(path + @"\" + openFileDialog1.SafeFileName, path + @"\" + imageName);
                        //}

                        MessageBox.Show("Successfully Data Updated!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //lblEmailerrormsg.Visible = false;
                        loadData(lblUid.Text);
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Sorry\r\n" + exp.Message);
                }
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

        public void Bindshopbranch()
        {
            string sql5 = "select   BranchName , Shopid from tbl_terminallocation";
            DAL.DataAccessManager.ExecuteSQL(sql5);
            DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
            cmbLocation.DataSource = dt5;
            cmbLocation.DisplayMember = "BranchName";
            cmbLocation.ValueMember = "Shopid";
        }

        private void addCategory_Load(object sender, EventArgs e)
        {
            try
            {
                //dtDOB.Format = DateTimePickerFormat.Custom;
                //dtDOB.CustomFormat = "yyyy-MM-dd";
                Bindshopbranch();
                //Update data | If user id has
                if (lblUid.Text != ".")
                {
                    loadData(lblUid.Text);
                    txtUserName.Enabled = false;
                    btnSave.Enabled = true;
                    btnSave.Text = "Update";
                    lnkDelete.Visible = true;
                }
                else
                {
                    showincrement();
                    //lnkAddnew.Visible = false;
                    lnkDelete.Visible = false;
                }

            }
            catch
            {
            }
        }


        // Load UID No
        private void showincrement()
        {
            string sql = "select id from usermgt order by id desc";
            DataTable dt = DAL.DataAccessManager.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                int id = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString()) + 1;
                txtUid.Text = id.ToString();
            }
            else
            {
                int id = 1;
                txtUid.Text = id.ToString();
            }
        }


        // Load User Info for Update 
        public void loadData(string Uid)
        {
            string sql3 = "select * from usermgt where id = '" + Uid + "'";
            DAL.DataAccessManager.ExecuteSQL(sql3);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql3);

            //  lblUid.Text = dt1.Rows[0].ItemArray[0].ToString();
            txtFirstName.Text = dt1.Rows[0].ItemArray[1].ToString();
            txtLastName.Text = dt1.Rows[0].ItemArray[2].ToString();
            txtAddress.Text = dt1.Rows[0].ItemArray[3].ToString();
            txtEmail.Text = dt1.Rows[0].ItemArray[4].ToString();
            txtContact.Text = dt1.Rows[0].ItemArray[5].ToString();
            //dtDOB.Value = Convert.ToDateTime(dt1.Rows[0].ItemArray[6].ToString());
            txtUserName.Text = dt1.Rows[0].ItemArray[7].ToString();
            txtPassword.Text = dt1.Rows[0].ItemArray[8].ToString();
            //lblimagename.Text = dt1.Rows[0].ItemArray[11].ToString();

            //string path = Application.StartupPath + @"\IMAGE\" + dt1.Rows[0].ItemArray[11].ToString() + "";
            //picUserimage.ImageLocation = path;
            //picUserimage.InitialImage.Dispose();

            if (dt1.Rows[0].ItemArray[9].ToString() == "1")
            {
                rdoAdmin.Checked = true;
            }
            else if (dt1.Rows[0].ItemArray[9].ToString() == "2")
            {
                rdoAdmin.Visible = false;
                rdoManager.Checked = true;
            }
            else if (dt1.Rows[0].ItemArray[9].ToString() == "3")
            {
                rdoSale.Checked = true;
            }
            else if (dt1.Rows[0].ItemArray[9].ToString() == "0")
            {
                rdoBlock.Checked = true;
            }
            else
            {
                // rdbtnInactive.Checked = true;
            }
            cmbLocation.SelectedValue = dt1.Rows[0].ItemArray[12].ToString();
        }


        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtEmail.Text = string.Empty;
            //dtDOB.Text = string.Empty;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                if (lblUid.Text == "-")
                {
                    // MessageBox.Show("You are Not able to Update");
                    MessageBox.Show("You are Not able to Delete", "Button3 Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        string sql = "delete from usermgt where   (id = '" + lblUid.Text + "' )";
                        DAL.DataAccessManager.ExecuteSQL(sql);

                        //picUserimage.InitialImage.Dispose();
                        //string path = Application.StartupPath + @"\IMAGE\";
                        //System.IO.File.Delete(path + @"\" + lblimagename.Text);

                        MessageBox.Show("User has been Deleted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //User_mgt.Manage_user go = new User_mgt.Manage_user();
                        //go.MdiParent = this.ParentForm;
                        //go.Show();
                        //this.Close();
                        ClearForm();

                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Sorry\r\n You have to Check the Data" + exp.Message);
                    }
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearForm();
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (txtEmail.Text.Length > 0 && txtEmail.Text.Trim().Length != 0)
            {
                if (!rEmail.IsMatch(txtEmail.Text.Trim()))
                {
                    lblEmailerrormsg.Visible = true;
                    lblEmailerrormsg.Text = "Invalid Email address";
                    txtEmail.SelectAll();
                    // e.Cancel = true;

                }
                else
                {
                    btnSave.Enabled = true;
                    lblEmailerrormsg.Visible = false;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string myPWD = PWDGenerator.GeneratePWD();
            txtPassword.Text = myPWD;
        }
    }
}
