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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==false)
                textPassword.UseSystemPasswordChar = true;
            else 
                textPassword.UseSystemPasswordChar = false;
        }

        private void clearFieldLabel_Click(object sender, EventArgs e)
        {
            textName.Clear();
            textPassword.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        { // To ask the admin whether he would like to exit the login window via a messageBox
            // A yes will exit the app and a no will cancel the event
            if (MessageBox.Show("Would you like to Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
