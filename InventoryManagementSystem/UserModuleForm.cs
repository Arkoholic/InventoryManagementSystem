using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace InventoryManagementSystem
{

    
    public partial class UserModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PKay\\Documents\\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {   //creatig a messageBox to confirm clicking the save button
                if(MessageBox.Show("Please confirm saving this user...", "Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
                   //SQL command to insert information into database
                    cm = new SqlCommand("INSERT INTO tableUser(username,fullname,password,contact)VALUES(@username,@fullname,@password,@contact)", con);
                cm.Parameters.AddWithValue("@username", textUserName.Text);
                cm.Parameters.AddWithValue("@fullname", textFullName.Text);
                cm.Parameters.AddWithValue("@password", textPassword.Text);
                cm.Parameters.AddWithValue("@contact", textContact.Text);

                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User has been saved successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
