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
    public partial class AdminProduct : Form
    {
        public AdminProduct()
        {
            InitializeComponent();
            DisplayElements("ProductTbl");
        }
        SqlConnection connection = new SqlConnection(Program.Database_Connection_String);
        private void AdminHome_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            DisplayElements("ProductTbl");
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void PQuantify_OnValueChanged(object sender, EventArgs e)
        {

        }
        private void DisplayElements(string name)
        {
            connection.Open();
            string q = "select * from " + name+ "";
            SqlDataAdapter    s = new SqlDataAdapter(q, connection);
            SqlCommandBuilder Bld = new SqlCommandBuilder(s);
            var dataset = new DataSet();
            s.Fill(dataset);
            PDGV.DataSource = dataset.Tables[0];
            connection.Close();
        }


        private void label3_Click(object sender, EventArgs e)
        {
            AdminCustomer cut = new AdminCustomer();
            cut.Show();
            this.Hide();


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


        int key = 0;

        






        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            AdminBilling cut = new AdminBilling();
            cut.Show();
            this.Hide();
        }



        private void label5_Click_1(object sender, EventArgs e)
        {
            Program.isOwner = false;
            Login cut = new Login();
            cut.Show();
            this.Hide();
        }

        private void PDGV_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            PName.Text = PDGV.SelectedRows[0].Cells[1].Value.ToString();
            PCategory.SelectedIndex = Convert.ToInt16(PDGV.SelectedRows[0].Cells[6].Value);
            PPrice.Text = PDGV.SelectedRows[0].Cells[2].Value.ToString();
            PQuantity.Text = PDGV.SelectedRows[0].Cells[3].Value.ToString();
            PTextile.Text = PDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditButton_Click_1(object sender, EventArgs e)

        {
            if (PName.Text == "" || PPrice.Text == "" || PQuantity.Text == "" || PCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (PCategory.SelectedIndex == 0)
                {
                    try
                    {

                        SqlCommand command = new SqlCommand("Update ProductTbl set Name=@PN, Category=@PC, Price=@PP, Quantity=@PQ , Textile=@PT where Id=@PKey", connection);
                        command.Parameters.AddWithValue("@PN", PName.Text);
                        command.Parameters.AddWithValue("@PC", PCategory.SelectedIndex.ToString());
                        command.Parameters.AddWithValue("@PP", PPrice.Text);
                        command.Parameters.AddWithValue("@PQ", PQuantity.Text);
                        command.Parameters.AddWithValue("@PT", PTextile.Text);
                        command.Parameters.AddWithValue("@PKey", key);
                        connection.Open();
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Updated!!");

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
                else
                {
                    try
                    {

                        SqlCommand command = new SqlCommand("Update ProductTbl set Name=@PN, Category=@PC, Price=@PP, Quantity=@PQ , ExpDt=@PE where Id=@PKey", connection);
                        command.Parameters.AddWithValue("@PN", PName.Text);
                        command.Parameters.AddWithValue("@PC", PCategory.SelectedIndex.ToString());
                        command.Parameters.AddWithValue("@PP", PPrice.Text);
                        command.Parameters.AddWithValue("@PQ", PQuantity.Text);
                        command.Parameters.AddWithValue("@PE", PExp.Value);
                        command.Parameters.AddWithValue("@PKey", key);
                        connection.Open();
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Updated!!");

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


            }
            DisplayElements("ProductTbl");
        }

        private void AddProduct_Click(object sender, EventArgs e)

        {
            if (PName.Text == "" || PPrice.Text == "" || PQuantity.Text == "" || PCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (PCategory.SelectedIndex == 0)
                {

                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("Insert into ProductTbl(Name, Category, Price, Quantity, Textile) values(@PN, @PC, @PP, @PQ, @PT)", connection);
                        command.Parameters.AddWithValue("@PN", PName.Text);
                        command.Parameters.AddWithValue("@PC", PCategory.SelectedIndex.ToString());
                        command.Parameters.AddWithValue("@PP", PPrice.Text);
                        command.Parameters.AddWithValue("@PQ", PQuantity.Text);
                        command.Parameters.AddWithValue("@PT", PTextile.Text);
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Added!!");

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
                else
                {


                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("Insert into ProductTbl(Name, Category, Price, Quantity, ExpDt) values(@PN, @PC, @PP, @PQ, @PE)", connection);
                        command.Parameters.AddWithValue("@PN", PName.Text);
                        command.Parameters.AddWithValue("@PC", PCategory.SelectedIndex.ToString());
                        command.Parameters.AddWithValue("@PP", PPrice.Text);
                        command.Parameters.AddWithValue("@PQ", PQuantity.Text);
                        command.Parameters.AddWithValue("@PE", PExp.Value);
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Added!!");

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

            }
            DisplayElements("ProductTbl");
        }

        private void DeleteProduct_Click(object sender, EventArgs e)
        {

            {
                if (key == 0)
                {
                    MessageBox.Show("Select a product");
                }
                else
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("delete from ProductTbl where Id=@PKey", connection);
                        command.Parameters.AddWithValue("@PKey", key);


                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Deleted!!");

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
                DisplayElements("ProductTbl");
            }
        }
    }
}
