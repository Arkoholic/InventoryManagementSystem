using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class TransactionForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public TransactionForm()
        {
            InitializeComponent();
            LoadTransaction();
        }

        public void LoadTransaction()
        { 
                int i = 0;
                dgvUser.Rows.Clear();
                cm = new SqlCommand("SELECT * FROM tbTransaction", con);
                con.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvUser.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
                dr.Close();
                con.Close();
            
          

        }  

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TransactionModuleForm moduleForm = new TransactionModuleForm();
            /*moduleForm.buttonUpdate.Enabled = false;
            moduleForm.buttonAddTransac.Enabled = true;*/
            moduleForm.ShowDialog();
            LoadTransaction();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            
            if (colName == "Delete")
            {
                if (MessageBox.Show("Affirm whether you would like to delete this transaction...", "Delete User?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                        
                    cm = new SqlCommand("DELETE FROM tbTransaction WHERE transactionId LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");
                }
            }
            LoadTransaction();
        }
    }
}
