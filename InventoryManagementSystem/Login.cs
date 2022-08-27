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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PKay\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30 ");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==false)
                textPassword.UseSystemPasswordChar = true;
            else 
                textPassword.UseSystemPasswordChar = false;
        }

        private void clearFieldLabel_Click(object sender, EventArgs e)
        {
            textName.Clear();
            textPassword.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        { // To ask the admin whether he would like to exit the login window via a messageBox
            // A yes will exit the app and a no will cancel the event
            if (MessageBox.Show("Would you like to Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try 
            {//admin and users get to login
                cm = new SqlCommand("SELECT * FROM tbUser WHERE username=@username AND password=@password", con);
                cm.Parameters.AddWithValue("@username", textName.Text);
                cm.Parameters.AddWithValue("@password", textPassword.Text);
                con.Open();
                dr = cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    MessageBox.Show("Hello " + dr["fullname"].ToString() +"!","Welcome",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    MainForms main = new MainForms();
                    this.Hide();

                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect. Please contact Admin!", "Illegal entry is prohibited!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();


            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
