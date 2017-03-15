using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectFingerTracking
{
    public partial class Adjust : Form
    {
        public Adjust()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (my_ok != null)
            {
                my_ok(rotate_x,rotate_y,rotate_z,scale_x,scale_y,scale_z);
            }
        }

        private void Complect_Click(object sender, EventArgs e)
        {
            if(my_comp != null)
            {
                Close();
                my_comp();
            }
        }

        private void Rotatex_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Rotatex_UpDown.Value;
            rotate_x = Convert.ToInt32(temp);
        }

        private void Rotatey_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Rotatey_UpDown.Value;
            rotate_y = Convert.ToInt32(temp);
        }

        private void Rotatez_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Rotatez_UpDown.Value;
            rotate_z = Convert.ToInt32(temp);
        }

        private void Scalex_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Scalex_UpDown.Value;
            scale_x = Convert.ToInt32(temp);
        }

        private void Scaley_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Scaley_UpDown.Value;
            scale_y = Convert.ToInt32(temp);
        }

        private void Scalez_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Scalez_UpDown.Value;
            scale_z = Convert.ToInt32(temp);
        }


    }
}
