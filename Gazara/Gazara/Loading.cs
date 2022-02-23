using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gazara
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int start_pos = 0  ;
        private void timer1_Tick(object sender, EventArgs e)
        {
            start_pos = start_pos + 2;
            MyProgress.Value = start_pos;
            perc.Text = start_pos + "%";
            if(MyProgress.Value== 100)
            {
                MyProgress.Value  =   0;
                timer1.Stop();

                Login log;
                log = new Login();
                log.Show();
                this.Hide();

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
