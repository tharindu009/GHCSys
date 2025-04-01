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
    public partial class dashboard : KryptonForm
    {
        public dashboard()
        {
            InitializeComponent();
            customizeDesign();
        }


        public void customizeDesign()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panel1.Visible == true)
                panel1.Visible = false;
            if (panel2.Visible == true)
                panel2.Visible = false;
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;
            if (panel5.Visible == true)
                panel5.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //showSubMenu(panel1);
            //openChildForm(new Customer.addNewCustomer());
            hideSubMenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new addNewCustomer());
            hideSubMenu();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            showSubMenu(panel2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new addItem());
            hideSubMenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new addCategory());
            hideSubMenu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new stockList());
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            showSubMenu(panel3);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            openChildForm(new addEmployee());
            hideSubMenu();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //openChildForm(new SalesRegister.salesRegister());
            hideSubMenu();

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            showSubMenu(panel4);
        }

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            //childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            childForm.WindowState = FormWindowState.Maximized;
            //childForm.Dock = DockStyle.Fill;
            //panelChildForm.Controls.Add(childForm);
            //panelChildForm.Tag = childForm;
            //childForm.BringToFront();
            childForm.MdiParent = this;
            childForm.Show();
        }

        public void openChildForm12(Form childForm)
        {
            //if (activeForm != null)
            //{
            //    activeForm.Close();
            //}
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //openChildForm(new Expences.addExpences());
            hideSubMenu();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dashboard));
            this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button34 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button33 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button24 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblJobNo = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelSideMenu.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPalette1
            // 
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Width = 0;
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Width = 0;
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Width = 0;
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Width = 0;
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Width = 0;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 12;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
            this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, -1, -1, -1);
            // 
            // panelChildForm
            // 
            this.panelChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.Location = new System.Drawing.Point(214, 0);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(857, 668);
            this.panelChildForm.TabIndex = 3;
            this.panelChildForm.Visible = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button7.Dock = System.Windows.Forms.DockStyle.Top;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(0, 105);
            this.button7.Name = "button7";
            this.button7.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button7.Size = new System.Drawing.Size(197, 35);
            this.button7.TabIndex = 3;
            this.button7.Text = "Bulk Import";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button8.Dock = System.Windows.Forms.DockStyle.Top;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(0, 70);
            this.button8.Name = "button8";
            this.button8.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button8.Size = new System.Drawing.Size(197, 35);
            this.button8.TabIndex = 2;
            this.button8.Text = "Stock Detail";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button9.Dock = System.Windows.Forms.DockStyle.Top;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(0, 35);
            this.button9.Name = "button9";
            this.button9.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button9.Size = new System.Drawing.Size(197, 35);
            this.button9.TabIndex = 1;
            this.button9.Text = "Add Category";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button10.Dock = System.Windows.Forms.DockStyle.Top;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Name = "button10";
            this.button10.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button10.Size = new System.Drawing.Size(197, 35);
            this.button10.TabIndex = 0;
            this.button10.Text = "Add Item";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.Controls.Add(this.button34);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 175);
            this.panel2.TabIndex = 4;
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button34.Dock = System.Windows.Forms.DockStyle.Top;
            this.button34.FlatAppearance.BorderSize = 0;
            this.button34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button34.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button34.ForeColor = System.Drawing.Color.White;
            this.button34.Location = new System.Drawing.Point(0, 140);
            this.button34.Name = "button34";
            this.button34.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button34.Size = new System.Drawing.Size(197, 35);
            this.button34.TabIndex = 4;
            this.button34.Text = "Stock List";
            this.button34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button34.UseVisualStyleBackColor = false;
            this.button34.Click += new System.EventHandler(this.button34_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button13.Dock = System.Windows.Forms.DockStyle.Top;
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.White;
            this.button13.Location = new System.Drawing.Point(0, 70);
            this.button13.Name = "button13";
            this.button13.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button13.Size = new System.Drawing.Size(197, 35);
            this.button13.TabIndex = 2;
            this.button13.Text = "Worksheet";
            this.button13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button14.Dock = System.Windows.Forms.DockStyle.Top;
            this.button14.FlatAppearance.BorderSize = 0;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button14.ForeColor = System.Drawing.Color.White;
            this.button14.Location = new System.Drawing.Point(0, 35);
            this.button14.Name = "button14";
            this.button14.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button14.Size = new System.Drawing.Size(197, 35);
            this.button14.TabIndex = 1;
            this.button14.Text = "Manage Employee";
            this.button14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click_1);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button15.Dock = System.Windows.Forms.DockStyle.Top;
            this.button15.FlatAppearance.BorderSize = 0;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button15.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.ForeColor = System.Drawing.Color.White;
            this.button15.Location = new System.Drawing.Point(0, 0);
            this.button15.Name = "button15";
            this.button15.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button15.Size = new System.Drawing.Size(197, 35);
            this.button15.TabIndex = 0;
            this.button15.Text = "Add Employee";
            this.button15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.Controls.Add(this.button33);
            this.panel4.Controls.Add(this.button31);
            this.panel4.Controls.Add(this.button30);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button18);
            this.panel4.Controls.Add(this.button19);
            this.panel4.Controls.Add(this.button20);
            this.panel4.Controls.Add(this.button21);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 510);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(197, 280);
            this.panel4.TabIndex = 9;
            // 
            // button33
            // 
            this.button33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button33.Dock = System.Windows.Forms.DockStyle.Top;
            this.button33.FlatAppearance.BorderSize = 0;
            this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button33.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button33.ForeColor = System.Drawing.Color.White;
            this.button33.Location = new System.Drawing.Point(0, 245);
            this.button33.Name = "button33";
            this.button33.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button33.Size = new System.Drawing.Size(197, 35);
            this.button33.TabIndex = 7;
            this.button33.Text = "Add Sale";
            this.button33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button33.UseVisualStyleBackColor = false;
            // 
            // button31
            // 
            this.button31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button31.Dock = System.Windows.Forms.DockStyle.Top;
            this.button31.FlatAppearance.BorderSize = 0;
            this.button31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button31.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button31.ForeColor = System.Drawing.Color.White;
            this.button31.Location = new System.Drawing.Point(0, 210);
            this.button31.Name = "button31";
            this.button31.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button31.Size = new System.Drawing.Size(197, 35);
            this.button31.TabIndex = 6;
            this.button31.Text = "Bank Deposit";
            this.button31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button31.UseVisualStyleBackColor = false;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // button30
            // 
            this.button30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button30.Dock = System.Windows.Forms.DockStyle.Top;
            this.button30.FlatAppearance.BorderSize = 0;
            this.button30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button30.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button30.ForeColor = System.Drawing.Color.White;
            this.button30.Location = new System.Drawing.Point(0, 175);
            this.button30.Name = "button30";
            this.button30.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button30.Size = new System.Drawing.Size(197, 35);
            this.button30.TabIndex = 5;
            this.button30.Text = "Due Payment History";
            this.button30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button30.UseVisualStyleBackColor = false;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(0, 140);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(197, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Expences";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button18.Dock = System.Windows.Forms.DockStyle.Top;
            this.button18.FlatAppearance.BorderSize = 0;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.ForeColor = System.Drawing.Color.White;
            this.button18.Location = new System.Drawing.Point(0, 105);
            this.button18.Name = "button18";
            this.button18.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button18.Size = new System.Drawing.Size(197, 35);
            this.button18.TabIndex = 3;
            this.button18.Text = "Due List";
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button19.Dock = System.Windows.Forms.DockStyle.Top;
            this.button19.FlatAppearance.BorderSize = 0;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button19.ForeColor = System.Drawing.Color.White;
            this.button19.Location = new System.Drawing.Point(0, 70);
            this.button19.Name = "button19";
            this.button19.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button19.Size = new System.Drawing.Size(197, 35);
            this.button19.TabIndex = 2;
            this.button19.Text = "Returns";
            this.button19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button20.Dock = System.Windows.Forms.DockStyle.Top;
            this.button20.FlatAppearance.BorderSize = 0;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button20.ForeColor = System.Drawing.Color.White;
            this.button20.Location = new System.Drawing.Point(0, 35);
            this.button20.Name = "button20";
            this.button20.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button20.Size = new System.Drawing.Size(197, 35);
            this.button20.TabIndex = 1;
            this.button20.Text = "Job Card";
            this.button20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button21.Dock = System.Windows.Forms.DockStyle.Top;
            this.button21.FlatAppearance.BorderSize = 0;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button21.ForeColor = System.Drawing.Color.White;
            this.button21.Location = new System.Drawing.Point(0, 0);
            this.button21.Name = "button21";
            this.button21.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button21.Size = new System.Drawing.Size(197, 35);
            this.button21.TabIndex = 0;
            this.button21.Text = "Quotation";
            this.button21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.button13);
            this.panel3.Controls.Add(this.button14);
            this.panel3.Controls.Add(this.button15);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 365);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(197, 106);
            this.panel3.TabIndex = 6;
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.AutoScroll = true;
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelSideMenu.Controls.Add(this.btnInvoice);
            this.panelSideMenu.Controls.Add(this.button32);
            this.panelSideMenu.Controls.Add(this.button26);
            this.panelSideMenu.Controls.Add(this.panel5);
            this.panelSideMenu.Controls.Add(this.button25);
            this.panelSideMenu.Controls.Add(this.panel1);
            this.panelSideMenu.Controls.Add(this.button3);
            this.panelSideMenu.Controls.Add(this.button22);
            this.panelSideMenu.Controls.Add(this.panel4);
            this.panelSideMenu.Controls.Add(this.button17);
            this.panelSideMenu.Controls.Add(this.panel3);
            this.panelSideMenu.Controls.Add(this.button11);
            this.panelSideMenu.Controls.Add(this.panel2);
            this.panelSideMenu.Controls.Add(this.button6);
            this.panelSideMenu.Controls.Add(this.button1);
            this.panelSideMenu.Controls.Add(this.panelLogo);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(214, 668);
            this.panelSideMenu.TabIndex = 2;
            // 
            // btnInvoice
            // 
            this.btnInvoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInvoice.FlatAppearance.BorderSize = 0;
            this.btnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoice.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.ForeColor = System.Drawing.Color.Lime;
            this.btnInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoice.Location = new System.Drawing.Point(0, 1299);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnInvoice.Size = new System.Drawing.Size(197, 39);
            this.btnInvoice.TabIndex = 17;
            this.btnInvoice.Text = "Create Invoice";
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Visible = false;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel5.Controls.Add(this.button27);
            this.panel5.Controls.Add(this.button28);
            this.panel5.Controls.Add(this.button29);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 1117);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(197, 104);
            this.panel5.TabIndex = 14;
            // 
            // button27
            // 
            this.button27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button27.Dock = System.Windows.Forms.DockStyle.Top;
            this.button27.Enabled = false;
            this.button27.FlatAppearance.BorderSize = 0;
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button27.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button27.ForeColor = System.Drawing.Color.White;
            this.button27.Location = new System.Drawing.Point(0, 70);
            this.button27.Name = "button27";
            this.button27.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button27.Size = new System.Drawing.Size(197, 35);
            this.button27.TabIndex = 2;
            this.button27.Text = "User Profile";
            this.button27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button27.UseVisualStyleBackColor = false;
            // 
            // button28
            // 
            this.button28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button28.Dock = System.Windows.Forms.DockStyle.Top;
            this.button28.Enabled = false;
            this.button28.FlatAppearance.BorderSize = 0;
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button28.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button28.ForeColor = System.Drawing.Color.White;
            this.button28.Location = new System.Drawing.Point(0, 35);
            this.button28.Name = "button28";
            this.button28.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button28.Size = new System.Drawing.Size(197, 35);
            this.button28.TabIndex = 1;
            this.button28.Text = "Database Settings";
            this.button28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button28.UseVisualStyleBackColor = false;
            // 
            // button29
            // 
            this.button29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button29.Dock = System.Windows.Forms.DockStyle.Top;
            this.button29.FlatAppearance.BorderSize = 0;
            this.button29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button29.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button29.ForeColor = System.Drawing.Color.White;
            this.button29.Location = new System.Drawing.Point(0, 0);
            this.button29.Name = "button29";
            this.button29.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button29.Size = new System.Drawing.Size(197, 35);
            this.button29.TabIndex = 0;
            this.button29.Text = "System Settings";
            this.button29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button29.UseVisualStyleBackColor = false;
            this.button29.Click += new System.EventHandler(this.button29_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.button24);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button23);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 868);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 210);
            this.panel1.TabIndex = 12;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button24.Dock = System.Windows.Forms.DockStyle.Top;
            this.button24.FlatAppearance.BorderSize = 0;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button24.ForeColor = System.Drawing.Color.White;
            this.button24.Location = new System.Drawing.Point(0, 175);
            this.button24.Name = "button24";
            this.button24.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button24.Size = new System.Drawing.Size(197, 35);
            this.button24.TabIndex = 5;
            this.button24.Text = "Job History";
            this.button24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(0, 140);
            this.button4.Name = "button4";
            this.button4.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button4.Size = new System.Drawing.Size(197, 35);
            this.button4.TabIndex = 4;
            this.button4.Text = "Sales Chart";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(0, 105);
            this.button5.Name = "button5";
            this.button5.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button5.Size = new System.Drawing.Size(197, 35);
            this.button5.TabIndex = 3;
            this.button5.Text = "Overview";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button12.Dock = System.Windows.Forms.DockStyle.Top;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.Color.White;
            this.button12.Location = new System.Drawing.Point(0, 70);
            this.button12.Name = "button12";
            this.button12.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button12.Size = new System.Drawing.Size(197, 35);
            this.button12.TabIndex = 2;
            this.button12.Text = "General Ledger";
            this.button12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button16.Dock = System.Windows.Forms.DockStyle.Top;
            this.button16.FlatAppearance.BorderSize = 0;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.ForeColor = System.Drawing.Color.White;
            this.button16.Location = new System.Drawing.Point(0, 35);
            this.button16.Name = "button16";
            this.button16.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button16.Size = new System.Drawing.Size(197, 35);
            this.button16.TabIndex = 1;
            this.button16.Text = "Profit and Lost Report";
            this.button16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button16_Click_1);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(178)))), ((int)(((byte)(219)))));
            this.button23.Dock = System.Windows.Forms.DockStyle.Top;
            this.button23.FlatAppearance.BorderSize = 0;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.White;
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Name = "button23";
            this.button23.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.button23.Size = new System.Drawing.Size(197, 35);
            this.button23.TabIndex = 0;
            this.button23.Text = "Sales Report";
            this.button23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button32
            // 
            this.button32.Dock = System.Windows.Forms.DockStyle.Top;
            this.button32.FlatAppearance.BorderSize = 0;
            this.button32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button32.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button32.ForeColor = System.Drawing.Color.White;
            this.button32.Image = ((System.Drawing.Image)(resources.GetObject("button32.Image")));
            this.button32.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button32.Location = new System.Drawing.Point(0, 1260);
            this.button32.Name = "button32";
            this.button32.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button32.Size = new System.Drawing.Size(197, 39);
            this.button32.TabIndex = 16;
            this.button32.Text = ".....";
            this.button32.UseVisualStyleBackColor = true;
            // 
            // button26
            // 
            this.button26.Dock = System.Windows.Forms.DockStyle.Top;
            this.button26.FlatAppearance.BorderSize = 0;
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button26.ForeColor = System.Drawing.Color.White;
            this.button26.Image = ((System.Drawing.Image)(resources.GetObject("button26.Image")));
            this.button26.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button26.Location = new System.Drawing.Point(0, 1221);
            this.button26.Name = "button26";
            this.button26.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button26.Size = new System.Drawing.Size(197, 39);
            this.button26.TabIndex = 15;
            this.button26.Text = "Log Off";
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.button26_Click);
            // 
            // button25
            // 
            this.button25.Dock = System.Windows.Forms.DockStyle.Top;
            this.button25.FlatAppearance.BorderSize = 0;
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button25.ForeColor = System.Drawing.Color.White;
            this.button25.Image = ((System.Drawing.Image)(resources.GetObject("button25.Image")));
            this.button25.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button25.Location = new System.Drawing.Point(0, 1078);
            this.button25.Name = "button25";
            this.button25.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button25.Size = new System.Drawing.Size(197, 39);
            this.button25.TabIndex = 13;
            this.button25.Text = "Settings";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(0, 829);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(197, 39);
            this.button3.TabIndex = 11;
            this.button3.Text = "Reports";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button22
            // 
            this.button22.Dock = System.Windows.Forms.DockStyle.Top;
            this.button22.FlatAppearance.BorderSize = 0;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button22.ForeColor = System.Drawing.Color.White;
            this.button22.Image = ((System.Drawing.Image)(resources.GetObject("button22.Image")));
            this.button22.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button22.Location = new System.Drawing.Point(0, 790);
            this.button22.Name = "button22";
            this.button22.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button22.Size = new System.Drawing.Size(197, 39);
            this.button22.TabIndex = 10;
            this.button22.Text = "Quick Sale";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click_1);
            // 
            // button17
            // 
            this.button17.Dock = System.Windows.Forms.DockStyle.Top;
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.ForeColor = System.Drawing.Color.White;
            this.button17.Image = ((System.Drawing.Image)(resources.GetObject("button17.Image")));
            this.button17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button17.Location = new System.Drawing.Point(0, 471);
            this.button17.Name = "button17";
            this.button17.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button17.Size = new System.Drawing.Size(197, 39);
            this.button17.TabIndex = 8;
            this.button17.Text = "Transaction";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click_1);
            // 
            // button11
            // 
            this.button11.Dock = System.Windows.Forms.DockStyle.Top;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button11.Image")));
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(0, 326);
            this.button11.Name = "button11";
            this.button11.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button11.Size = new System.Drawing.Size(197, 39);
            this.button11.TabIndex = 5;
            this.button11.Text = "Employee";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Top;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(0, 112);
            this.button6.Name = "button6";
            this.button6.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button6.Size = new System.Drawing.Size(197, 39);
            this.button6.TabIndex = 3;
            this.button6.Text = "Item";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Poppins SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 73);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(197, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Customer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panelLogo
            // 
            this.panelLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLogo.BackgroundImage")));
            this.panelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelLogo.Controls.Add(this.lblJobNo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(197, 73);
            this.panelLogo.TabIndex = 0;
            this.panelLogo.Click += new System.EventHandler(this.panelLogo_Click);
            this.panelLogo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLogo_Paint);
            // 
            // lblJobNo
            // 
            this.lblJobNo.AutoSize = true;
            this.lblJobNo.Location = new System.Drawing.Point(159, 9);
            this.lblJobNo.Name = "lblJobNo";
            this.lblJobNo.Size = new System.Drawing.Size(35, 13);
            this.lblJobNo.TabIndex = 0;
            this.lblJobNo.Text = "label1";
            this.lblJobNo.Visible = false;
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1071, 668);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panelSideMenu);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.IsMdiContainer = true;
            this.Name = "dashboard";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dashboard_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.dashboard_FormClosed);
            this.Load += new System.EventHandler(this.dashboard_Load);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelSideMenu.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.ResumeLayout(false);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openChildForm(new addNewCustomer());
            //openChildForm(new addvehicle());
            hideSubMenu();
        }

        private void dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openChildForm(new dueList());
            hideSubMenu();
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panel4);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            openChildForm(new duePaymentHistory());
            hideSubMenu();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openChildForm(new addExpences());
            hideSubMenu();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            openChildForm(new addCategory());
            hideSubMenu();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panel2);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panel3);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panel1);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            openChildForm(new addBankTransfer());
            hideSubMenu();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            openChildForm(new quotation());
            hideSubMenu();
        }

        public void button22_Click_1(object sender, EventArgs e)
        {
            openChildForm(new RegisterQ());
            hideSubMenu();
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            openChildForm(new jobCardHybrid());
            hideSubMenu();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Sign out?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
               // timer1.Stop();
                //workRecords();
                //Login go = new Login();
                // go = new LoginNew();
                //go.Show();
                this.Close();
            }
        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            button32.Text = "Welcome " + BAL.UserInfo.UserName;
            openChildForm(new dashboardOne());
            hideSubMenu();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            openChildForm(new SalesRegister.salesReturn());
            hideSubMenu();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            openChildForm(new JobDetailSummary());
            hideSubMenu();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            openChildForm(new ReportDialog());
            hideSubMenu();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            openChildForm(new ItemBulkUpload());
            hideSubMenu();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            openChildForm(new GeneralLedger());
            hideSubMenu();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            openChildForm(new MonthlySalesChart());
            hideSubMenu();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            openChildForm(new RegisterQ(lblJobNo.Text));
            hideSubMenu();
            btnInvoice.Visible = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            openChildForm(new stockDetail());
            hideSubMenu();

        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelLogo_Click(object sender, EventArgs e)
        {
            openChildForm(new dashboardOne());
            hideSubMenu();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            openChildForm(new systemSettings());
            hideSubMenu();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            openChildForm(new ManageEmployee());
            hideSubMenu();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            openChildForm(new ProftLostReport());
            hideSubMenu();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            showSubMenu(panel5);
        }
    }
}
