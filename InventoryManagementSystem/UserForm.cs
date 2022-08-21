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
    public partial class UserForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public UserForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser",con);
            con.Open();
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            UserModuleForm userModule = new UserModuleForm();
            userModule.buttonSave.Enabled = true;
            userModule.buttonUpdate.Enabled = false;
            userModule.ShowDialog();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModuleForm userModule = new UserModuleForm();
                userModule.textUserName.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModule.textFullName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModule.textPassword.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModule.textContact.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModule.buttonSave.Enabled = false;
                userModule.buttonUpdate.Enabled = true;
                userModule.ShowDialog();

            }
            else if (colName == "Delete")
                    {
                if(MessageBox.Show("Affirm whether you would like to delete this user...","Delete User?",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbUser WHERE contact LIKE '"+ dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted!");
                }
            }
        }
    }
}
