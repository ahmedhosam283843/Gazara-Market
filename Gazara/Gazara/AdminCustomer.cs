using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Gazara
{
    public partial class AdminCustomer : Form
    {
        public AdminCustomer()
        {
            InitializeComponent();
            DisplayElements("UserTbl");
        }
        SqlConnection connection = new SqlConnection(Program.Database_Connection_String);
        private void label1_Click(object sender, EventArgs e)
        {
            if (Program.isOwner)
            {
                AdminProduct cut = new AdminProduct();
                cut.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Access not granted");
            }

        }
        private void DisplayElements(string name)
        {
            
            connection.Open();
            string q = "select * from " + name + "";
            SqlDataAdapter s = new SqlDataAdapter(q, connection);
            SqlCommandBuilder Bld = new SqlCommandBuilder(s);
            var dataset = new DataSet();
            s.Fill(dataset);
            CDGV.DataSource = dataset.Tables[0];

            connection.Close();
            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (Program.isOwner)
            {
                AdminEmployees cut = new AdminEmployees();

                cut.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Access not granted");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (Program.isOwner)
            {
                AdminDashboard cut = new AdminDashboard();
                cut.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Access not granted");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            DisplayElements("UserTbl");
        }
        int key = 0;
        private void CDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CName.Text = CDGV.SelectedRows[0].Cells[1].Value.ToString();
            CPhone.Text = CDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (CName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }




        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label10_Click(object sender, EventArgs e)
        {
            AdminBilling adminDashboard = new AdminBilling();
            adminDashboard.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Program.isOwner = false;
            Login cut = new Login();
            cut.Show();
            this.Hide();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (CName.Text == "" || CPhone.Text == "")
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                try
                {

                    SqlCommand command = new SqlCommand("Update UserTbl set Name=@CN, Phone=@CP where Id=@CKey", connection);
                    command.Parameters.AddWithValue("@CN", CName.Text);
                    command.Parameters.AddWithValue("@CP", CPhone.Text);
                    command.Parameters.AddWithValue("@CKey", key);
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated!!");

                    connection.Close();
                }
                catch (Exception Exc)
                {

                    MessageBox.Show(Exc.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            DisplayElements("UserTbl");
        }

        private void DeleteProduct_Click(object sender, EventArgs e)

        {
            if (key == 0)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("delete from UserTbl where Id=@UKey", connection);
                    command.Parameters.AddWithValue("@UKey", key);


                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted!!");

                    connection.Close();
                }
                catch (Exception Exc)
                {

                    MessageBox.Show(Exc.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            DisplayElements("UserTbl");
        }

        private void AddProduct_Click(object sender, EventArgs e)

        {
            if (CName.Text == "" || CPhone.Text == "")
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Insert into UserTbl(Name, Phone, Sales) values(@CN, @CP, @CS)", connection);
                    command.Parameters.AddWithValue("@CN", CName.Text);
                    command.Parameters.AddWithValue("@CP", CPhone.Text);
                    command.Parameters.AddWithValue("@CS", "0");

                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer Added!!");

                    connection.Close();

                }
                catch (Exception Exc)
                {

                    MessageBox.Show(Exc.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            DisplayElements("UserTbl");
        }

        private void CDGV_CellContentClick_2(object sender, DataGridViewCellEventArgs e)

        {
            CName.Text = CDGV.SelectedRows[0].Cells[1].Value.ToString();
            CPhone.Text = CDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (CName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
