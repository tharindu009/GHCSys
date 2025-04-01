namespace PointofSale.Warranty
{
    partial class RptWarranty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.lblJobNo = new System.Windows.Forms.Label();
            this.lblSN = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PointofSale.Warranty.RptWarranty.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(728, 771);
            this.reportViewer1.TabIndex = 0;
            // 
            // lblInvoice
            // 
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Location = new System.Drawing.Point(632, 13);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(35, 13);
            this.lblInvoice.TabIndex = 1;
            this.lblInvoice.Text = "label1";
            this.lblInvoice.Visible = false;
            // 
            // lblJobNo
            // 
            this.lblJobNo.AutoSize = true;
            this.lblJobNo.Location = new System.Drawing.Point(681, 13);
            this.lblJobNo.Name = "lblJobNo";
            this.lblJobNo.Size = new System.Drawing.Size(10, 13);
            this.lblJobNo.TabIndex = 2;
            this.lblJobNo.Text = ".";
            this.lblJobNo.Visible = false;
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(12, 31);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(35, 13);
            this.lblSN.TabIndex = 3;
            this.lblSN.Text = "label1";
            this.lblSN.Visible = false;
            // 
            // RptWarranty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 771);
            this.Controls.Add(this.lblSN);
            this.Controls.Add(this.lblJobNo);
            this.Controls.Add(this.lblInvoice);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RptWarranty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RptWarranty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.Label lblJobNo;
        private System.Windows.Forms.Label lblSN;
    }
}