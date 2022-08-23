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
    public partial class CategoriesForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public CategoriesForm()
        {
            InitializeComponent();
            LoadCategories();
        }

        public void LoadCategories()
        {
            int i = 0;
            dgvCategories.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbCategories", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategories.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CategoriesModuleForm moduleForm = new CategoriesModuleForm();
            moduleForm.buttonSave.Enabled = true;
            moduleForm.buttonUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCategories();
        }

        private void dgvCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategories.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoriesModuleForm moduleForm = new CategoriesModuleForm();
                moduleForm.labelCatId.Text = dgvCategories.Rows[e.RowIndex].Cells[1].Value.ToString();
                moduleForm.textCategoryName.Text = dgvCategories.Rows[e.RowIndex].Cells[2].Value.ToString();

                moduleForm.buttonSave.Enabled = false;
                moduleForm.buttonUpdate.Enabled = true;
                moduleForm.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Affirm whether you would like to delete this category...", "Delete Category?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    //username is the primary key
                    cm = new SqlCommand("DELETE FROM tbCategories WHERE categoryId LIKE '" + dgvCategories.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");
                }
            }
            LoadCategories();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
