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
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textPassword.Text != textRePassword.Text)
            {
                MessageBox.Show("Credentials dont match!","Caution",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            try
            {   //creatig a messageBox to confirm clicking the save button
                if(MessageBox.Show("Please confirm saving this user...", "Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                   //SQL command to insert information into database
                    cm = new SqlCommand("INSERT INTO tbUser(username,fullname,password,contact)VALUES(@username,@fullname,@password,@contact)", con);
                cm.Parameters.AddWithValue("@username", textUserName.Text);
                cm.Parameters.AddWithValue("@fullname", textFullName.Text);
                cm.Parameters.AddWithValue("@password", textPassword.Text);
                cm.Parameters.AddWithValue("@contact", textContact.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User has been saved successfully!");
                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            buttonSave.Enabled = true;
            buttonUpdate.Enabled = false;
        }

        public void Clear()
        {
            textUserName.Clear();
            textFullName.Clear();
            textPassword.Clear();
            textRePassword.Clear();
            textContact.Clear();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textPassword.Text != textRePassword.Text)
            {
                MessageBox.Show("Credentials dont match!", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {   //creatig a messageBox to confirm clicking the update button
                if (MessageBox.Show("Please confirm updating this user...", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //SQL command to update information into database
                    cm = new SqlCommand("UPDATE tbUser SET fullname = @fullname, password = @password, contact = @contact WHERE username LIKE '"+textUserName.Text+"'", con);
                cm.Parameters.AddWithValue("@fullname", textFullName.Text);
                cm.Parameters.AddWithValue("@password", textPassword.Text);
                cm.Parameters.AddWithValue("@contact", textContact.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("User has been updated successfully!");
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
