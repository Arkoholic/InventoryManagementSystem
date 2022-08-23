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
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCustomer", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            con.Close();

        }

      

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CustomerModuleForm moduleForm = new CustomerModuleForm();
            moduleForm.buttonSave.Enabled = true;
            moduleForm.buttonUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.labelCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.textCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule .textCustomerContact.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();

                customerModule.buttonSave.Enabled = false;
                customerModule.buttonUpdate.Enabled = true;
                customerModule.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Affirm whether you would like to delete this customer...", "Delete Customer?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    //username is the primary key
                    cm = new SqlCommand("DELETE FROM tbCustomer WHERE customerId LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");
                }
            }
            LoadCustomer();
        }
    }
}
