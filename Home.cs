using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace PointofSale
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                //MessageBox.Show("");
                MessageBox.Show("Please insert User Name", "Not match", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();

            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Please  insert Password", "Not match", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else
            {
                try
                {
                    string tkhan = "Select Username , password , usertype, Shopid  from  usermgt  " +
                                    " where Username   = '" + txtUserName.Text + "' and password = '" + txtPassword.Text + "'";
                    DataSet ds = DAL.DataAccessManager.GetDataSet(tkhan);
                    DataTable dt = ds.Tables[0];

                    string username = dt.Rows[0].ItemArray[0].ToString();
                    string password = dt.Rows[0].ItemArray[1].ToString();
                    string usertype = dt.Rows[0].ItemArray[2].ToString();
                    string Shopid = dt.Rows[0].ItemArray[3].ToString();

                    if (txtUserName.Text == username && txtPassword.Text == password)
                    {
                        if (usertype == "1")   //usertype usertype
                        {
                            BAL.UserInfo.UserName = txtUserName.Text;
                            BAL.UserInfo.usertype = "1"; // 1= admin
                            BAL.UserInfo.Shopid = Shopid;
                            //workRecords();
                            dashboard go = new dashboard();
                            go.Show();
                            this.Hide();

                        }
                        if (usertype == "2")
                        {
                            BAL.UserInfo.UserName = txtUserName.Text;
                            BAL.UserInfo.usertype = "2"; //2 = Manager
                            BAL.UserInfo.Shopid = Shopid;
                            //workRecords();
                            dashboard go = new dashboard();
                            go.Show();
                            this.Hide();
                        }

                        if (usertype == "3")
                        {
                            BAL.UserInfo.UserName = txtUserName.Text;
                            BAL.UserInfo.usertype = "3"; //3 = salesman
                            BAL.UserInfo.Shopid = Shopid;
                            //workRecords();
                            dashboard go = new dashboard();
                            go.Show();
                            this.Hide();
                        }
                        if (usertype == "0") // Block user
                        {

                            MessageBox.Show("\n This user (" + txtUserName.Text + ") has been blocked. \n Please contact to administrator.", "Block - Inactive", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        // MessageBox.Show("Username or Password not match", "Not match", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        lblmsg.Visible = true;
                        lblmsg.Text = "Username or Password does not match";

                    }
                }
                catch (Exception exe)
                {
                    // MessageBox.Show(exe.Message);
                    // MessageBox.Show("User ID not exist   \n\n " + exe.Message, "Not match", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // MessageBox.Show("User ID or Password not match", "Not match", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblmsg.Visible = true;
                    lblmsg.Text = "System Error Occured!";

                }

            }
            //pictureBox2.Visible = true;
            //prg();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public string VersionLabel
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label9.Text = VersionLabel;
        }
    }
}
