namespace PointofSale.SalesRegister
{
    partial class POSPrintRpt
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.POSPrintPageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.psodbDataSet = new PointofSale.SalesRegister.psodbDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolsaleno = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolstrpProgressCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnstopPrint = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timerpregress = new System.Windows.Forms.Timer(this.components);
            this.POSPrintPageTableAdapter = new PointofSale.SalesRegister.psodbDataSetTableAdapters.POSPrintPageTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.POSPrintPageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psodbDataSet)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // POSPrintPageBindingSource
            // 
            this.POSPrintPageBindingSource.DataMember = "POSPrintPage";
            this.POSPrintPageBindingSource.DataSource = this.psodbDataSet;
            // 
            // psodbDataSet
            // 
            this.psodbDataSet.DataSetName = "psodbDataSet";
            this.psodbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "POSPRINTDataSet";
            reportDataSource1.Value = this.POSPrintPageBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PointofSale.SalesRegister.RptPOS.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 22);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(731, 832);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.reportViewer1_RenderingComplete);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsaleno,
            this.toolStripProgressBar1,
            this.toolstrpProgressCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(731, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolsaleno
            // 
            this.toolsaleno.Name = "toolsaleno";
            this.toolsaleno.Size = new System.Drawing.Size(57, 17);
            this.toolsaleno.Text = "----------";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolstrpProgressCount
            // 
            this.toolstrpProgressCount.Name = "toolstrpProgressCount";
            this.toolstrpProgressCount.Size = new System.Drawing.Size(23, 17);
            this.toolstrpProgressCount.Text = "1%";
            // 
            // btnstopPrint
            // 
            this.btnstopPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstopPrint.Location = new System.Drawing.Point(310, 0);
            this.btnstopPrint.Name = "btnstopPrint";
            this.btnstopPrint.Size = new System.Drawing.Size(75, 23);
            this.btnstopPrint.TabIndex = 2;
            this.btnstopPrint.Text = "Stop Print";
            this.btnstopPrint.UseVisualStyleBackColor = true;
            this.btnstopPrint.Click += new System.EventHandler(this.btnstopPrint_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(478, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // timerpregress
            // 
            this.timerpregress.Tick += new System.EventHandler(this.timerpregress_Tick);
            // 
            // POSPrintPageTableAdapter
            // 
            this.POSPrintPageTableAdapter.ClearBeforeFill = true;
            // 
            // POSPrintRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 854);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnstopPrint);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "POSPrintRpt";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.POSPrintRpt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.POSPrintPageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psodbDataSet)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolsaleno;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolstrpProgressCount;
        private System.Windows.Forms.Button btnstopPrint;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timerpregress;
        private System.Windows.Forms.BindingSource POSPrintPageBindingSource;
        private psodbDataSet psodbDataSet;
        private psodbDataSetTableAdapters.POSPrintPageTableAdapter POSPrintPageTableAdapter;
    }
}