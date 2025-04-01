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

namespace PointofSale
{
    public partial class addExpences : KryptonForm
    {
        public addExpences()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text == string.Empty)
                {
                    MessageBox.Show("Please Insert Expense Amount");
                    txtAmount.Focus();
                }
                else if (cmboCategory.Text == string.Empty)
                {
                    MessageBox.Show("Please Select category");
                    cmboCategory.Focus();
                }
                else
                {
                    string Filename;
                    if (txtAttachmentFileName.Text != string.Empty)
                    {
                        Filename = DateTime.Now.ToString("yyyyMMddhhmmss") + lblFileExtension.Text;
                    }
                    else
                    {
                        Filename = "";
                    }
                    string sql1 = " insert into tbl_expense (Date , ReferenceNo , Category ,	Amount ,	Attachment , fileextension, Note ,	Createdby) " +
                                " values ('" + dtStartDate.Text + "', '" + txtReferNo.Text + "','" + cmboCategory.Text + "', '" + txtAmount.Text + "',  " +
                                " '" + Filename + "', '" + lblFileExtension.Text + "', '" + txtNote.Text + "' , '" + BAL.UserInfo.UserName + "')";
                    DAL.DataAccessManager.ExecuteSQL(sql1);

                    if (txtAttachmentFileName.Text != string.Empty)
                    {
                        //Attachment upload  /////////////////
                        string path = Application.StartupPath + @"\ExpenseAttachment\";
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(Application.StartupPath + @"\ExpenseAttachment\");
                        string copyfile = lblcopyfile.Text; //Source file
                        string pastefile = path + @"\" + Filename;  //destination file
                        System.IO.File.Copy(copyfile, pastefile);
                    }

                    MessageBox.Show("Saved Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult result = MessageBox.Show("Do you want to add a new Expense?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        txtAmount.Text = string.Empty;
                        txtNote.Text = string.Empty;
                        txtReferNo.Text = string.Empty;
                    }
                    else
                    {
                        //this.Hide();
                        //Expenses.ExpensesList go = new Expenses.ExpensesList();
                        //go.MdiParent = this.ParentForm;
                        //go.Show();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addExpences_Load(object sender, EventArgs e)
        {
            try
            {
                dtStartDate.Format = DateTimePickerFormat.Custom;
                dtStartDate.CustomFormat = "yyyy-MM-dd";
                txtAmount.Focus();
                Expensebind();

            }
            catch
            {
            }
        }

        public void Expensebind()
        {
            string sql = " select  ID, Date , ReferenceNo as 'Refer No' , Category ,	Amount , Note ,	Createdby as 'Posted by', Attachment , fileextension from tbl_expense ";
            DAL.DataAccessManager.ExecuteSQL(sql);
            DataTable dt1 = DAL.DataAccessManager.GetDataTable(sql);
            grdExpences.DataSource = dt1;
            //lblRow.Text = grdExpences.RowCount.ToString() + " Records Found";

            double sum = 0;
            for (int i = 0; i < grdExpences.Rows.Count; ++i)
            {
                sum += Convert.ToDouble(grdExpences.Rows[i].Cells[4].Value);
            }
            //lblSum.Text = "Total amount: " + sum.ToString();

        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = ".pdf";
            openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf|jpg files (*.jpg)|*.jpg|PNG files (*.png)|*.png";

            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;



            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // textBox1.Text = openFileDialog1.FileName;
                txtAttachmentFileName.Text = openFileDialog1.SafeFileName;
                lblcopyfile.Text = openFileDialog1.FileName;
                lblFileExtension.Text = Path.GetExtension(openFileDialog1.FileName);
                FileInfo fi1 = new FileInfo(openFileDialog1.FileName);
                lblfileinfo.Text = "File Name: " + fi1.Name + "\n" + fi1.Length.ToString() + " Bytes \nFile type:" + fi1.Extension + "";
            }
        }
    }
}
