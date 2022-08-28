using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class AppWelcomeForm : Form
    {
        public AppWelcomeForm()
        {
            InitializeComponent();
            timer1.Start();
        }
        int start = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 2;
            progress.Value = start;
            if(progress.Value == 100)
            {
                progress.Value = 0;
                timer1.Stop();
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
            }
           
        }
    }
}
