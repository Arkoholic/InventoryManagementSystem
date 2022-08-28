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
            double total = 0;
            double totalChange = 1000;
            
                int i = 0;
            
                dgvTransaction.Rows.Clear();
                cm = new SqlCommand("SELECT transactionId,transactionDate, T.productId, P.productName, T.customerId, C.customerName, quantity, price,total   FROM tbTransaction AS T  JOIN tbCustomer AS C ON T.customerId = C.customerId JOIN tbProduct AS P ON T.productId = P.productId WHERE CONCAT (transactionId,transactionDate, T.productId, P.productName, T.customerId, C.customerName, quantity, price) LIKE '%" +textSearch.Text+"%'", con);
                con.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvTransaction.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
                
            }
                dr.Close();
                con.Close();
            double changeTill = total - totalChange;
                labelTotal.Text =  total.ToString();
                labelChange.Text = changeTill.ToString();
                
          

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
            string colName = dgvTransaction.Columns[e.ColumnIndex].Name;
            
            if (colName == "Delete")
            {
                if (MessageBox.Show("Affirm whether you would like to delete this transaction...", "Delete User?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                        
                    cm = new SqlCommand("DELETE FROM tbTransaction WHERE transactionId LIKE '" + dgvTransaction.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");

                    cm = new SqlCommand("UPDATE tbProduct SET productQuantity = (productQuantity+@productQuantity)  WHERE productId LIKE '" + dgvTransaction.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con);
                    cm.Parameters.AddWithValue("@productQuantity", Convert.ToInt16(dgvTransaction.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                }
            }
            LoadTransaction();
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadTransaction();
        }
    }
}
