using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using PointofSale.UserMgr;

namespace PointofSale
{
    public partial class ManageEmployee : KryptonForm
    {
        public ManageEmployee()
        {
            InitializeComponent();
        }

        //Click add to cart
        protected void b_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string s;
            s = b.Tag.ToString();

            //this.Hide();
            //.User_regi go = new User_mgt.User_regi();
            addEmployee go = new addEmployee();
            go.Uid = s;
            //go.MdiParent = this.ParentForm;
            go.ShowDialog();
        }

        private void addCategory_Load(object sender, EventArgs e)
        {
            try
            {
                list_images();
            }
            catch
            {
            }
        }

        private void txtsearchUser_TextChanged(object sender, EventArgs e)
        {
            flowLayoutPanelUserList.Controls.Clear();
            string img_directory = Application.StartupPath + @"\IMAGE\";
            //dir_image.Text = img_directory;
            //string[] files = Directory.GetFiles(img_directory, "*.jpg *.png"); // "*.png"

            try
            {
                string sql = "select * from usermgt where Name like '" + txtsearchUser.Text + "%' OR Username like '" + txtsearchUser.Text + "%' " +
                            " OR Contact like '" + txtsearchUser.Text + "%' OR position like '" + txtsearchUser.Text + "%' ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                //int count = dataReader.FieldCount;
                //image_count.Text = count.ToString();
                int currentImage = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataReader = dt.Rows[i];

                    Button b = new Button();
                    //Image i = Image.FromFile(img_directory + dataReader["name"]);
                    b.Tag = dataReader["id"];
                    b.Click += new EventHandler(b_Click);
                    b.Name = dataReader["Name"].ToString() + "\n Contact: " + dataReader["Contact"].ToString() + "\n Position: " + dataReader["position"].ToString();


                    ImageList il = new ImageList();
                    il.ColorDepth = ColorDepth.Depth32Bit;
                    il.TransparentColor = Color.Transparent;
                    il.ImageSize = new Size(150, 120);
                    il.Images.Add(Image.FromFile(img_directory + dataReader["imagename"]));

                    b.Image = il.Images[0];
                    b.Margin = new Padding(4, 4, 4, 4);

                    b.Size = new Size(330, 130);
                    b.Text.PadRight(4);

                    // ilabel.BackgroundImage = il.Images[currentImage];
                    // ilabel.BackgroundImageLayout = ImageLayout.Stretch;

                    //  b.Text = "ID: ";
                    b.Text += "\n UID: " + dataReader["Username"];
                    b.Text += "\n Name: " + dataReader["Name"].ToString();
                    b.Text += "\n Contact: " + dataReader["Contact"].ToString();
                    b.Text += "\n Position: " + dataReader["position"];
                    b.Text += "\n " + dataReader["Email"];
                    b.Text += "\n " + dataReader["Shopid"];


                    b.Font = new Font("Poppins", 9, FontStyle.Regular, GraphicsUnit.Point);
                    b.TextAlign = ContentAlignment.TopLeft;
                    b.TextImageRelation = TextImageRelation.ImageBeforeText;
                    //b.FlatStyle = FlatStyle.Flat;
                    //b.FlatAppearance.BorderSize = 1;

                    flowLayoutPanelUserList.Controls.Add(b);
                    currentImage++;


                }
            }
            catch //(Exception)
            {

                // throw;
            }
        }

        private void btnCreateLink_Click(object sender, EventArgs e)
        {
            addEmployee go = new addEmployee();
            go.MdiParent = this.ParentForm;
            go.ShowDialog();
        }

        //Show Use List with image
        public void list_images()
        {
            string img_directory = Application.StartupPath + @"\IMAGE\";
            //dir_image.Text = img_directory;
            //string[] files = Directory.GetFiles(img_directory, "*.jpg *.png"); // "*.png"

            try
            {
                string sql = "select * from usermgt ";
                DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt = DAL.DataAccessManager.GetDataTable(sql);

                //int count = dataReader.FieldCount;
                //image_count.Text = count.ToString();
                int currentImage = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataReader = dt.Rows[i];

                    //Button click event
                    Button b = new Button();
                    //Image i = Image.FromFile(img_directory + dataReader["name"]);
                    b.Tag = dataReader["id"];
                    b.Click += new EventHandler(b_Click);
                    b.Name = dataReader["Name"].ToString() + "\n Contact: " + dataReader["Contact"].ToString() + "\n Position: " + dataReader["position"].ToString();


                    ImageList il = new ImageList();
                    il.ColorDepth = ColorDepth.Depth32Bit;
                    il.TransparentColor = Color.Transparent;
                    il.ImageSize = new Size(150, 120);
                    //il.Images.Add(Image.FromFile(img_directory + dataReader["imagename"]));   
                    il.Images.Add(PointofSale.Properties.Resources.user);


                    b.Image = il.Images[0];
                    b.Margin = new Padding(4, 4, 4, 4);

                    b.Size = new Size(330, 130);
                    b.Text.PadRight(4);

                    // ilabel.BackgroundImage = il.Images[currentImage];
                    // ilabel.BackgroundImageLayout = ImageLayout.Stretch;

                    //// Tile View
                    //  b.Text = "ID: ";
                    b.Text += "\n UID: " + dataReader["Username"];
                    b.Text += "\n Name: " + dataReader["Name"].ToString();
                    b.Text += "\n Contact: " + dataReader["Contact"].ToString();
                    b.Text += "\n Position: " + dataReader["position"];
                    b.Text += "\n " + dataReader["Email"];
                    b.Text += "\n " + dataReader["Shopid"];



                    b.Font = new Font("Poppins", 9, FontStyle.Regular, GraphicsUnit.Point);
                    b.TextAlign = ContentAlignment.TopLeft;
                    b.TextImageRelation = TextImageRelation.ImageBeforeText;
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    flowLayoutPanelUserList.Controls.Add(b);
                    currentImage++;
                }
            }
            catch //(Exception)
            {

                // throw;
            }
        }
    }
}
