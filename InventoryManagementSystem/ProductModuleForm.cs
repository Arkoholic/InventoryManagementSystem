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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cm = new SqlCommand("SELECT categoryName FROM tbCategories",con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboCat.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try { 
           

                //creatig a messageBox to confirm clicking the save button
                if (MessageBox.Show("Do you want to save this item...", "Confirm Saving", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //SQL command to insert information into database
                {
                    cm = new SqlCommand("INSERT INTO tbProduct(productName,productQuantity,productPrice,productDescription,productCategory)VALUES(@productName,@productQuantity,@productPrice,@productDescription,@productCategory)", con);
                    cm.Parameters.AddWithValue("@productname", textProductName.Text);
                    cm.Parameters.AddWithValue("@productQuantity",Convert.ToInt16(textProductQuantity.Text));
                    cm.Parameters.AddWithValue("@productPrice", Convert.ToInt16(textProductprice.Text));
                    cm.Parameters.AddWithValue("@productDescription", textProductDescription.Text);
                    cm.Parameters.AddWithValue("@productCategory", comboCat.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item has been saved successfully!");
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
            textProductName.Clear();
            textProductQuantity.Clear();
            textProductprice.Clear();
            textProductDescription.Clear();
            comboCat.Text = ""; 
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
            buttonSave.Enabled = true;
            buttonUpdate.Enabled = false;

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               
               
                if (MessageBox.Show("Please confirm updating this item...", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { //SQL command to update information into database
                    cm = new SqlCommand("UPDATE tbProduct SET productName = @productName, productQuantity = @productQuantity, productPrice = @productPrice, productDescription = @productDescription, productCategory = @productCategory WHERE productId LIKE '" + labelPId.Text + "'", con);
                    cm.Parameters.AddWithValue("@productname", textProductName.Text);
                    cm.Parameters.AddWithValue("@productQuantity", Convert.ToInt16(textProductQuantity.Text));
                    cm.Parameters.AddWithValue("@productPrice", Convert.ToInt16(textProductprice.Text));
                    cm.Parameters.AddWithValue("@productDescription", textProductDescription.Text);
                    cm.Parameters.AddWithValue("@productCategory", comboCat.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been updated successfully!");
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
