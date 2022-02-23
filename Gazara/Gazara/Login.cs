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
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void label4_Click(object sender, EventArgs e)
        {
            if (LPassword.Text == "" || LUser.Text == "")
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (LPassword.Text == "Admin" && LUser.Text == "Admin")
                {
                    AdminProduct x = new AdminProduct();
                    x.Show();
                    this.Hide();
                    Program.isOwner = true;
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (LPassword.Text == "" || LUser.Text == "")
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (LPassword.Text == "Employee" && LUser.Text == "Employee")
                {
                    AdminCustomer x = new AdminCustomer();
                    x.Show();
                    this.Hide();
                    Program.isOwner = false;
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
        }

        private void LUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
