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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            //For the searchBox
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(productId,productName,productPrice,productDescription,productCategory) LIKE  '%"+textSearch.Text+"%' ", con);
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm moduleForm = new ProductModuleForm();
            moduleForm.buttonSave.Enabled = true;
            moduleForm.buttonUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadProduct();
        }
        
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productForm = new ProductModuleForm();
                productForm.labelPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productForm.textProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productForm.textProductQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productForm.textProductprice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productForm.textProductDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productForm.comboCat.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();


                productForm.buttonSave.Enabled = false;
                productForm.buttonUpdate.Enabled = true;
                productForm.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Affirm whether you would like to delete this item...", "Delete Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    //username is the primary key
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE productId LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");
                }
            }
            LoadProduct(); 
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
