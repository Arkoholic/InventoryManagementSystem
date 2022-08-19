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
    public partial class CustomerButton : PictureBox
    {
        public CustomerButton()
        {
            InitializeComponent();

        }

        private Image normalImage;
        private Image hoveredImage;

        public Image ImageNormal 
        {
            get { return normalImage; }
            set { normalImage = value; }
        }

        public Image ImageHover
        {
            get { return hoveredImage; }
            set { hoveredImage = value; }
        }

        private void CustomerButton_MouseHover(object sender, EventArgs e)
        {
            this.Image = hoveredImage;
        }

        private void CustomerButton_MouseLeave(object sender, EventArgs e)
        {
            this.Image = normalImage;
        }
    }
}
