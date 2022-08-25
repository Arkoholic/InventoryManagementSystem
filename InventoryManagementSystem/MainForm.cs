using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //showing the subform inside the mainform
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            /*childForm.ShowInTaskbar = false; */
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void buttonCustomers_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        private void buttonCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoriesForm());
        }

        private void buttonProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void buttonTransactions_Click(object sender, EventArgs e)
        {
            openChildForm(new TransactionForm());
        }
    }
}
