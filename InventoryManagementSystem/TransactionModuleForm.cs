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
    public partial class TransactionModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public TransactionModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();

        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT customerId, customerName FROM tbCustomer WHERE CONCAT(customerId,customerName) LIKE '%"+textCustomerSearch.Text+"%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            //For the searchBox
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(productId,productName,productPrice,productDescription,productCategory) LIKE  '%" + textProductSearch.Text + "%' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textProductSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        int quantity = 0;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {//In case the quantity selected is more than what is in stock:
             if(Convert.ToInt16(numericUpDown1.Value) > quantity)
            {
                MessageBox.Show("Quantity selected is more than quantity available", "Caution!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                numericUpDown1.Value = numericUpDown1.Value - 1;
                return; 
            }

            if (Convert.ToInt16(numericUpDown1.Value) > 0)
            {
                //calculate for the total
                int total = Convert.ToInt16(textPrice.Text) * Convert.ToInt16(numericUpDown1.Value);
                textTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textCustomerId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            textCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            textProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            textPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            quantity = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void buttonAddTransac_Click(object sender, EventArgs e)
        {
                try
            {
               if (textCustomerId.Text == "")
                {
                    MessageBox.Show("Select a customer from the table.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } 
                if (textProductId.Text == "")
                {
                    MessageBox.Show("Select a product from the table.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                {   
                    if (MessageBox.Show("Please confirm adding this transaction...", "Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                       
                        cm = new SqlCommand("INSERT INTO tbTransaction(transactionDate,productId,customerId,quantity,price,total)VALUES(@transactionDate,@productId,@customerId,@quantity,@price,@total)", con);
                    cm.Parameters.AddWithValue("@transactionDate", dateTimeTransaction.Value);
                    cm.Parameters.AddWithValue("@productId", Convert.ToInt16(textProductId.Text));
                    cm.Parameters.AddWithValue("@customerId", Convert.ToInt16(textCustomerId.Text));
                    cm.Parameters.AddWithValue("@quantity", Convert.ToInt16(numericUpDown1.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(textPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt16(textTotal.Text));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Transaction has been saved successfully!");

                    cm = new SqlCommand("UPDATE tbProduct SET productQuantity = (productQuantity-@productQuantity)  WHERE productId LIKE '" + textProductId.Text + "'", con);
                    cm.Parameters.AddWithValue("@productQuantity", Convert.ToInt16(numericUpDown1.Text));
                  
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textCustomerId.Clear(); 
            textCustomerName.Clear();
            textProductId.Clear();
            textProductName.Clear();
            textPrice.Clear();
            numericUpDown1.Value = 0;
            textTotal.Clear(); 
            dateTimeTransaction.Value = DateTime.Now;
            
           
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            buttonUpdate.Enabled = false;
            buttonAddTransac.Enabled = true;
        }
    }
}
