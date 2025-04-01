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
    public partial class salesRegisterQ : KryptonForm
    {
        public salesRegisterQ()
        {
            InitializeComponent();
        }

        public salesRegisterQ(string JobNo)
        {
            InitializeComponent();
            lbluser.Text = BAL.UserInfo.UserName;
            //  this.tabPageSR_Payment.Parent = null; //Hide payment tab
            // tabSRcontrol.TabPages.Remove(tabPageSR_Payment);
            txtBarcodeReaderBox.Focus();


            //formFunctionPointer += new functioncall(Replicate); // Coin and papernotes
            //currency_Shortcuts1.CoinandNotesFunctionPointer = formFunctionPointer;

            //numformFunctionPointer += new numvaluefunctioncall(NumaricKeypad);
            //currency_Shortcuts1.NumaricKeypad = numformFunctionPointer;

            //CurrentJobNo = JobNo;

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPrintDirect_Click(object sender, EventArgs e)
        {

        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (lblTotalPayable.Text == "")
            {
                // MessageBox.Show("please insert Amount ");
            }
            else
            {
                try
                {
                    if (Convert.ToDouble(txtPaidAmount.Text) >= Convert.ToDouble(lblTotalPayable.Text))
                    {
                        double changeAmt = Convert.ToDouble(txtPaidAmount.Text) - Convert.ToDouble(lblTotalPayable.Text);
                        changeAmt = Math.Round(changeAmt, 2);
                        txtChangeAmount.Text = changeAmt.ToString();
                        txtDueAmount.Text = "0";
                        this.AcceptButton = btnPrintDirect;
                    }
                    if (Convert.ToDouble(txtPaidAmount.Text) <= Convert.ToDouble(lblTotalPayable.Text))
                    {
                        double changeAmt = Convert.ToDouble(lblTotalPayable.Text) - Convert.ToDouble(txtPaidAmount.Text);
                        changeAmt = Math.Round(changeAmt, 2);
                        txtDueAmount.Text = changeAmt.ToString();
                        txtChangeAmount.Text = "0";
                        this.AcceptButton = btnPrintDirect;
                    }

                }
                catch //(Exception exp)
                {
                    // MessageBox.Show(exp.Message);
                }

            }
        }
    }
}
