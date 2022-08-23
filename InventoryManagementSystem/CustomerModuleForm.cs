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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            try
            {   //creatig a messageBox to confirm clicking the save button
                if (MessageBox.Show("Please confirm saving this customer...", "Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //SQL command to insert information into database
                    cm = new SqlCommand("INSERT INTO tbCustomer(customerName,customerContact)VALUES(@customerName,@customerContact)", con);
                cm.Parameters.AddWithValue("@customerName", textCustomerName.Text);
                cm.Parameters.AddWithValue("@customerContact", textCustomerContact.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer has been saved successfully!");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          }

        public void Clear()
        {
            textCustomerName.Clear();
            textCustomerContact.Clear();
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            buttonSave.Enabled = true;
            buttonUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try { 
            //creating a messageBox to confirm clicking the update button
             if (MessageBox.Show("Please confirm updating this customer...", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                  { //SQL command to update information into database
                cm = new SqlCommand("UPDATE tbCustomer SET customerName = @customerName, customerContact = @customerContact WHERE  customerId LIKE '" + labelCId.Text + "'", con);
                cm.Parameters.AddWithValue("@customerName", textCustomerName.Text);
                cm.Parameters.AddWithValue("@customerContact", textCustomerContact.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer has been updated successfully!");
                this.Dispose();
            }
               }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}
    }
}
