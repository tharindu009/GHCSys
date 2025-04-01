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
using PointofSale.BAL;

namespace PointofSale
{
    public partial class addItem : KryptonForm
    {
        public addItem()
        {
            InitializeComponent();
        }

        byte[] ItemImage;

        // Get Item bar-code from Stock List form
        public string itemCode
        {
            set { lblItemcode.Text = value; }
            get { return lblItemcode.Text; }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void txtReferNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //  openFileDialog1.InitialDirectory = @"C:\";
            //  openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = ".jpg";
            // openFileDialog1.Filter = "GIF files (*.gif)|*.gif| jpg files (*.jpg)|*.jpg| PNG files (*.png)|*.png| All files (*.*)|*.*";
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg| PNG files (*.png)|*.png";

            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            //openFileDialog1.ReadOnlyChecked = true;
            //openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // textBox1.Text = openFileDialog1.FileName;
                picItemimage.ImageLocation = openFileDialog1.FileName;
                lblFileExtension.Text = Path.GetExtension(openFileDialog1.FileName);
                ItemImage = imageToByteArray(Image.FromFile(openFileDialog1.FileName));
            }
        }


        // convert image to byte array
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text == "")
            {
                MessageBox.Show("Please Insert Product Code/ Item Bar-code");
                txtProductCode.Focus();
            }
            else if (txtProductName.Text == "")
            {
                MessageBox.Show("Please Insert  Product Name");
                txtProductName.Focus();
            }
            else if (txtdiscount.Text == "")
            {
                txtdiscount.Text = "0";
                txtdiscount.Focus();
            }
            else if (txtProductQty.Text == "")
            {

                MessageBox.Show("Please Insert Product Quantity");
                txtProductQty.Focus();
            }
            else if (txtCostPrice.Text == "")
            {
                MessageBox.Show("Please Insert Product Cost Price / Buy price ");
                txtCostPrice.Focus();
            }

            else if (txtSalesPrice.Text == "")
            {
                MessageBox.Show("Please Insert Product  Sales Price");
                txtSalesPrice.Focus();
            }
            else if (ComboCategory.Text == "")
            {
                MessageBox.Show("Please Insert Product Category");
                ComboCategory.Focus();
            }
            else if (cmboShopid.Text == "")
            {
                MessageBox.Show("Please Select Branch name ");
                cmboShopid.Focus();
            }
            else if (cmbSupplier.Text == "")
            {
                MessageBox.Show("Please Select Supplier Name");
                cmbSupplier.Focus();
            }
            else
            {
                try
                {
                    string pid = txtProductCode.Text;
                    string pname = txtProductName.Text;
                    double quan = Convert.ToDouble(txtProductQty.Text);
                    double cprice = Convert.ToDouble(txtCostPrice.Text);
                    double sprice = Convert.ToDouble(txtSalesPrice.Text);

                    double ctotalpri = quan * cprice;
                    double rtotalpri = quan * sprice;
                    double discount = Convert.ToDouble(txtdiscount.Text);

                    int taxapply;
                    if (chktaxapply.Checked)
                    {
                        taxapply = 1;  //1 = Tax apply
                    }
                    else
                    {
                        taxapply = 0; // 0 = Tax not apply
                    }

                    int kitchenDisplaythisitem;
                    if (chkkitchenDisplay.Checked)
                    {
                        kitchenDisplaythisitem = 3; // 3 = It's show display on kitchen display
                    }
                    else
                    {
                        kitchenDisplaythisitem = 1; // 1 = it's not show on ditcken display.
                    }
                    //New Insert / New Entry
                    if (lblItemcode.Text == "-")
                    {
                        string imageName = pid + lblFileExtension.Text;
                        ItemImage = imageToByteArray(picItemimage.Image);

                        string sql1 = "INSERT INTO purchase (product_id,product_name,product_quantity,cost_price,retail_price,total_cost_price,total_retail_price,category,supplier,imagename,discount,taxapply,Shopid,status) " +
                                   " VALUES ('" + pid + "','" + pname + "','" + quan + "','" + cprice + "','" + sprice + "','" + ctotalpri + "','" + rtotalpri + "','" + ComboCategory.Text + "','" + cmbSupplier.Text + "',0xFFD8FFE000104A46494600010101006000600000FFE1004E4578696600004D4D002A00000008000403010005000000010000003E51100001000000010100000051110004000000010000000051120004000000010000000000000000000186A00000B18FFFDB004300080606070605080707070909080A0C140D0C0B0B0C1912130F141D1A1F1E1D1A1C1C20242E2720222C231C1C2837292C30313434341F27393D38323C2E333432FFDB0043010909090C0B0C180D0D1832211C213232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232323232FFC00011080080008003012200021101031101FFC4001F0000010501010101010100000000000000000102030405060708090A0BFFC400B5100002010303020403050504040000017D01020300041105122131410613516107227114328191A1082342B1C11552D1F02433627282090A161718191A25262728292A3435363738393A434445464748494A535455565758595A636465666768696A737475767778797A838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE1E2E3E4E5E6E7E8E9EAF1F2F3F4F5F6F7F8F9FAFFC4001F0100030101010101010101010000000000000102030405060708090A0BFFC400B51100020102040403040705040400010277000102031104052131061241510761711322328108144291A1B1C109233352F0156272D10A162434E125F11718191A262728292A35363738393A434445464748494A535455565758595A636465666768696A737475767778797A82838485868788898A92939495969798999AA2A3A4A5A6A7A8A9AAB2B3B4B5B6B7B8B9BAC2C3C4C5C6C7C8C9CAD2D3D4D5D6D7D8D9DAE2E3E4E5E6E7E8E9EAF2F3F4F5F6F7F8F9FAFFDA000C03010002110311003F00F9FE8A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A0028A28A00FFFD9,'" + discount + "','" + taxapply + "','" + cmboShopid.SelectedValue + "','" + kitchenDisplaythisitem + "')";
                        int a = DAL.DataAccessManager.ExecuteSQL(sql1);

                        //int row = DataAccess.AddItem(pid, pname, quan, cprice, sprice, ctotalpri, rtotalpri, ComboCategory.Text, cmbSupplier.Text, ItemImage,
                        //             discount, taxapply, cmboShopid.SelectedValue.ToString(), kitchenDisplaythisitem);

                        //Add to purchase history - New item history
                        insertpurchasehistory("NEW", quan, DateTime.Now.ToString("yyyy-MM-dd"));

                        //picture upload  /////////////////
                        //  if (openFileDialog1.FileName != string.Empty)
                        // {
                        //string path = Application.StartupPath + @"\ITEMIMAGE\";
                        //System.IO.File.Delete(path + @"\" + imageName);
                        //if (!System.IO.Directory.Exists(path))
                        //    System.IO.Directory.CreateDirectory(Application.StartupPath + @"\ITEMIMAGE\");
                        //string filename = path + @"\" + openFileDialog1.SafeFileName;
                        //picItemimage.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        //System.IO.File.Move(path + @"\" + openFileDialog1.SafeFileName, path + @"\" + imageName);
                        //   }

                        MessageBox.Show("Item hase been saved Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemList();

                        if (UserInfo.usertype == "1")
                        {
                            //stockItem go = new stockItem();
                            //go.MdiParent = this.ParentForm;
                            //go.Show();
                            //this.Close();
                        }
                        else
                        {
                            // btnItemLink.Visible = false;
                        }


                        // ClearForm();
                    }
                    else  //Update
                    {

                        string imageName;
                        //if (lblFileExtension.Text == "item.png") //if not select image
                        //{
                        //    imageName = lblimagename.Text;
                        //}
                        //else  // select image
                        //{
                        //    imageName = lblItemcode.Text + lblFileExtension.Text;
                        //}

                        string sql = " update purchase set product_name = '" + txtProductName.Text + "', product_quantity= '" + txtProductQty.Text + "', " +
                                    " cost_price = '" + txtCostPrice.Text + "', retail_price= '" + txtSalesPrice.Text + "', total_cost_price = '" + ctotalpri + "', " +
                                    " total_retail_price= '" + rtotalpri + "', category = '" + ComboCategory.Text + "', supplier = '" + cmbSupplier.Text + "',  " +
                                    " discount   = '" + discount + "' , taxapply = '" + taxapply + "' , " +
                                    " Shopid = '" + cmboShopid.SelectedValue + "' , status =  '" + kitchenDisplaythisitem + "' " +
                                    " where product_id = '" + lblItemcode.Text + "'";
                        DAL.DataAccessManager.ExecuteSQL(sql);

                        /////////////////////////////////////////////Update image //////////////////////////////////////////////////////
                        //if (lblFileExtension.Text != "item.png") // if select image
                        //{
                        //    picItemimage.InitialImage.Dispose();
                        //    string path = Application.StartupPath + @"\ITEMIMAGE\";                                                 
                        //    System.IO.File.Delete(path + @"\" + lblimagename.Text);
                        //    if (!System.IO.Directory.Exists(path))
                        //        System.IO.Directory.CreateDirectory(Application.StartupPath + @"\ITEMIMAGE\");
                        //    string filename = path + @"\" + openFileDialog1.SafeFileName;
                        //    picItemimage.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        //    System.IO.File.Move(path + @"\" + openFileDialog1.SafeFileName, path + @"\" + imageName);
                        //}


                        MessageBox.Show("Successfully Data Updated!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //loadData();    
                        if (UserInfo.usertype == "1")
                        {
                            //stockItem go = new stockItem();
                            //go.MdiParent = this.ParentForm;
                            //go.Show();
                            //this.Close();
                        }
                        else
                        {
                            // btnItemLink.Visible = false;
                        }
                    }


                }
                catch (Exception exp)
                {
                    MessageBox.Show("Sorry\r \n" + exp.Message);
                }
            }
            //this.Hide();

        }

        public void insertpurchasehistory(string ptype, double pQty, string pdate)
        {
            string pid = txtProductCode.Text;
            string pname = txtProductName.Text;
            double cprice = Convert.ToDouble(txtCostPrice.Text);
            double sprice = Convert.ToDouble(txtSalesPrice.Text);


            string sql1 = " insert into tbl_purchase_history (product_id, product_name, product_quantity, cost_price, retail_price, category, " +
                            " supplier, purchase_date, Shopid, ptype ) " +
                            " values ('" + pid + "', '" + pname + "', '" + pQty + "', '" + cprice + "', '" + sprice + "', '" + ComboCategory.Text + "', " +
                            "  '" + cmbSupplier.Text + "', '" + pdate + "' ,'" + cmboShopid.SelectedValue + "', '" + ptype + "' )";
            DAL.DataAccessManager.ExecuteSQL(sql1);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                if (lblItemcode.Text == "-")
                {
                    // MessageBox.Show("You are Not able to Update");
                    MessageBox.Show("You are Not able to Delete", "Button3 Title", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        string sql = "delete from purchase where product_id ='" + lblItemcode.Text + "'";
                        DAL.DataAccessManager.ExecuteSQL(sql);

                        //picItemimage.InitialImage.Dispose();
                        //string path = Application.StartupPath + @"\ITEMIMAGE\";
                        //System.IO.File.Delete(path + @"\" + lblimagename.Text);
                        MessageBox.Show("Successfully Data Delete !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        //Stock_List go = new Stock_List();
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


        private void ClearForm()
        {
            txtProductCode.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductQty.Text = string.Empty;
            txtCostPrice.Text = string.Empty;
            txtSalesPrice.Text = string.Empty;
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtNewpQty.Text == "")
                {
                    MessageBox.Show("Please Insert Purchase Quantity");
                    txtNewpQty.Focus();
                }
                else
                {
                    insertpurchasehistory("OLD", Convert.ToDouble(txtNewpQty.Text), dtpurchaseDate.Text);
                    updatestockqty();

                    DialogResult result = MessageBox.Show("Purchase history hase been saved Successfully. \n\n Do you want to see Item List?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                        //Items.Purchase_History go = new Items.Purchase_History();
                        //go.MdiParent = this.ParentForm;
                        //go.Show();
                    }
                    else
                    {
                        // MessageBox.Show("", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        kryptonButton2.Enabled = false;
                    }

                }

            }
            catch
            {
            }
        }


        public void updatestockqty()
        {
            string pid = txtProductCode.Text;
            double StockQty = Convert.ToDouble(txtProductQty.Text) + Convert.ToDouble(txtNewpQty.Text);
            string sql = " update purchase set " +
                                    " product_quantity = '" + StockQty + "' " +
                                    " where product_id = '" + pid + "' ";
            DAL.DataAccessManager.ExecuteSQL(sql);
        }

        private void addItem_Load(object sender, EventArgs e)
        {
            try
            {
                dtpurchaseDate.Format = DateTimePickerFormat.Custom;
                dtpurchaseDate.CustomFormat = "yyyy-MM-dd";
                
                //Supplier Info
                string sqlCust = "select   DISTINCT  *   from tbl_customer where PeopleType = 'Supplier'";
                DAL.DataAccessManager.ExecuteSQL(sqlCust);
                DataTable dtCust = DAL.DataAccessManager.GetDataTable(sqlCust);
                cmbSupplier.DataSource = dtCust;
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.Text = "Unknown";

                //Category list
                string sqlcate = "select DISTINCT category_name from tbl_category";
                DAL.DataAccessManager.ExecuteSQL(sqlcate);
                DataTable dtcate = DAL.DataAccessManager.GetDataTable(sqlcate);
                ComboCategory.DataSource = dtcate;
                ComboCategory.DisplayMember = "category_name";

                LoadItemList();
                Bindshopbranch();
                //Update data | If user id has
                if (lblItemcode.Text != "-")
                {
                    loadData();
                    txtProductCode.ReadOnly = true;
                    btnSave.Text = "Update";
                    //button1.Visible = true;
                    grpboxPurchasehistory.Visible = true;
                    grdItem.Visible = false;   
                }

            }
            catch
            {
            }
        }

        private void LoadItemList()
        {
            try
            {
                string sql = " select  product_id as 'Item Code' , product_name as 'Item Name' , product_quantity as 'Quantity',  " +
                             " cost_price as 'Buy Price' , retail_price as 'Sales Price' ,   category as 'Category' , supplier as 'Supplier'   " +
                             " from purchase order by product_quantity ";
                //DAL.DataAccessManager.ExecuteSQL(sql);
                DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
                grdItem.DataSource = dt1;
            }
            catch
            {
            }
        }

        public void Bindshopbranch()
        {
            string sql5 = "select   BranchName , Shopid from tbl_terminalLocation";
            DAL.DataAccessManager.ExecuteSQL(sql5);
            DataTable dt5 = DAL.DataAccessManager.GetDataTable(sql5);
            cmboShopid.DataSource = dt5;
            cmboShopid.DisplayMember = "Branchname";
            cmboShopid.ValueMember = "Shopid";

        }


        private void loadData()
        {
            string sql = "select product_id, product_name, product_quantity, cost_price , retail_price, category ,  " +
                        " supplier, imagename , discount, Shopid , taxapply , status " +
                        " from purchase where product_id = '" + lblItemcode.Text + "'";
            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);

            txtProductCode.Text = dt1.Rows[0].ItemArray[0].ToString();
            txtProductName.Text = dt1.Rows[0].ItemArray[1].ToString();
            txtProductQty.Text = dt1.Rows[0].ItemArray[2].ToString();
            txtCostPrice.Text = dt1.Rows[0].ItemArray[3].ToString();
            txtSalesPrice.Text = dt1.Rows[0].ItemArray[4].ToString();
            ComboCategory.Text = dt1.Rows[0].ItemArray[5].ToString();
            cmbSupplier.Text = dt1.Rows[0].ItemArray[6].ToString();
            lblimagename.Text = dt1.Rows[0].ItemArray[7].ToString();

            string path = Application.StartupPath + @"\ITEMIMAGE\" + dt1.Rows[0].ItemArray[7].ToString() + "";
            picItemimage.ImageLocation = path;
            picItemimage.InitialImage.Dispose();

            txtdiscount.Text = dt1.Rows[0].ItemArray[8].ToString();
            cmboShopid.SelectedValue = dt1.Rows[0].ItemArray[9].ToString();

            if (dt1.Rows[0].ItemArray[10].ToString() == "1")
            {
                chktaxapply.Checked = true;
            }
            else
            {
                chktaxapply.Checked = false;
            }

            if (dt1.Rows[0].ItemArray[11].ToString() == "3")  // 3 = show kitchen display 
            {
                chkkitchenDisplay.Checked = true;
            }
            else
            {
                chkkitchenDisplay.Checked = false;
            }
        }

        private void chktaxapply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                label4.Visible = false;
                txtCostPrice.Visible = false;
                label5.Visible = false;
                cmbSupplier.Visible = false;
                label3.Visible = false;
                txtProductQty.Visible = false;
                string ServiceID = checkServiceCode();
                txtProductCode.Text = ServiceID;
                txtProductCode.Enabled = false;
                txtCostPrice.Text = "0.0";
                txtProductQty.Text = "999";
                ComboCategory.Text = "Service";
                ComboCategory.Enabled = false;
            }
            else
            {
                label4.Visible = true;
                txtCostPrice.Visible = true;
                label5.Visible = true;
                cmbSupplier.Visible = true;
                label3.Visible = true;
                txtProductQty.Visible = true;
                txtProductCode.Text = "";
                txtProductCode.Enabled = true;
                ComboCategory.Enabled = true;
            }
        }

        private string checkServiceCode()
        {
            string serviceCode = "";
            string ServiceQuery = "SELECT TOP 1 * FROM purchase where category = 'Service' ORDER BY product_id DESC";
            DataTable dtService = DAL.DataAccessManager.GetDataTable(ServiceQuery);
            if (dtService.Rows.Count == 0)
            {
                serviceCode = "SE001";
            }
            else
            {
                string LastServiceID = dtService.Rows[0][0].ToString();
                string LastDigit = LastServiceID.Substring(LastServiceID.Length - 3);
                int ID = Convert.ToInt32(LastDigit);
                int NextID = ID + 1;
                if (NextID < 100 && NextID >10)
                {
                    serviceCode = "SE0" + NextID.ToString();
                }
                else if(NextID < 10 && NextID <100)
                {
                    serviceCode = "SE00" + NextID.ToString();
                }
                else
                {
                    serviceCode = "SE" + NextID.ToString();
                }
            }
            return serviceCode;
        }
    }
}
