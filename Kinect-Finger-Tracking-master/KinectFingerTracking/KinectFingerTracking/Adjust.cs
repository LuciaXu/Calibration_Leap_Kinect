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

        }

        private void Complect_Click(object sender, EventArgs e)
        {

        }

        private void Rotatex_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Rotatex_UpDown.Value;
            rotate_x += Convert.ToInt32(temp);
        }

        private void Rotatey_UpDown_ValueChanged(object sender, EventArgs e)
        {
            var temp = Rotatey_UpDown.Value;
            rotate_y += Convert.ToInt32(temp);
        }

    }
}
