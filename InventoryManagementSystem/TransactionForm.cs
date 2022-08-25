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
            moduleForm.buttonUpdate.Enabled = false;
            moduleForm.buttonAddTransac.Enabled = true;
            moduleForm.ShowDialog();
        }
    }
}
