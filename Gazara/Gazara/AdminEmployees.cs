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
    public partial class AdminEmployees : Form
    {
        public AdminEmployees()
        {
            InitializeComponent();
            DisplayElements("EmployeeTbl");
        }
        SqlConnection connection = new SqlConnection(Program.Database_Connection_String);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DisplayElements(string name)
        {

            connection.Open();
            string q = "select * from " + name + "";
            SqlDataAdapter s = new SqlDataAdapter(q, connection);
            SqlCommandBuilder Bld = new SqlCommandBuilder(s);
            var dataset = new DataSet();
            s.Fill(dataset);
            EDGV.DataSource = dataset.Tables[0];

            connection.Close();

        }
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

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DisplayElements("EmployeeTbl");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            AdminBilling cut = new AdminBilling();
            cut.Show();
            this.Hide();
        }


        int key = 0;




        private void label5_Click_1(object sender, EventArgs e)
        {
            Program.isOwner = false;
            Login cut = new Login();
            cut.Show();
            this.Hide();
        }

        private void PDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EName.Text = EDGV.SelectedRows[0].Cells[1].Value.ToString();
            EPhone.Text = EDGV.SelectedRows[0].Cells[2].Value.ToString();
            EBSalary.Text = EDGV.SelectedRows[0].Cells[3].Value.ToString();
            ECommision.Text = EDGV.SelectedRows[0].Cells[4].Value.ToString();

            EType.SelectedIndex = Convert.ToInt32(EDGV.SelectedRows[0].Cells[7].Value);
            if (EName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Edit_Click_1(object sender, EventArgs e)

        {
            if (EName.Text == "" || EPhone.Text == "" || EBSalary.Text == "" || EType.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (EType.SelectedIndex == 0)
                {

                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("Update EmployeeTbl set Name = @EN, Phone= @EP, BaseSalary=@EBS,  Type=@ET where Id=@Ekey", connection);
                        command.Parameters.AddWithValue("@EN", EName.Text);
                        command.Parameters.AddWithValue("@EP", EPhone.Text);

                        command.Parameters.AddWithValue("@EBS", EBSalary.Text);
                        command.Parameters.AddWithValue("@Ekey", key);
                        command.Parameters.AddWithValue("@ES", EBSalary.Text);
                        command.Parameters.AddWithValue("@ET", EType.SelectedIndex.ToString());
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Employee Updated!!");

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
                    if (ECommision.Text == "" || ECommision.Text == "Commision Rate")
                    {
                        MessageBox.Show("Enter a Commision Rate");
                    }
                    else
                    {

                        try
                        {

                            connection.Open();
                            SqlCommand command = new SqlCommand("Update EmployeeTbl set Name = @EN, Phone= @EP,  BaseSalary=@EBS,  ComRate=@EC,Type=@ET where Id=@Ekey", connection);
                            command.Parameters.AddWithValue("@EN", EName.Text);
                            command.Parameters.AddWithValue("@EP", EPhone.Text);

                            command.Parameters.AddWithValue("@EBS", EBSalary.Text);

                            command.Parameters.AddWithValue("@ES", EBSalary.Text);
                            command.Parameters.AddWithValue("@EC", ECommision.Text);
                            command.Parameters.AddWithValue("@ET", EType.SelectedIndex.ToString());
                            command.Parameters.AddWithValue("@Ekey", key);
                            command.Connection = connection;
                            command.ExecuteNonQuery();
                            MessageBox.Show("Employee Updated!!");

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
            }
            DisplayElements("EmployeeTbl");
        }

        private void Delete_Click(object sender, EventArgs e)

        {
            if (key == 0)
            {
                MessageBox.Show("Select an Employee");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("delete from EmployeeTbl where Id=@EKey", connection);
                    command.Parameters.AddWithValue("@EKey", key);


                    command.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted!!");

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
            DisplayElements("EmployeeTbl");
        }

        private void Add_Click_1(object sender, EventArgs e)

        {
            if (EName.Text == "" || EPhone.Text == "" || EBSalary.Text == "" || EType.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                if (EType.SelectedIndex == 0)
                {

                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("Insert into EmployeeTbl(Name, Phone, BaseSalary, Sales, Salary, Type) " +
                            "values(@EN, @EP, @EBS, @ESl, @ES, @ET)", connection);
                        command.Parameters.AddWithValue("@EN", EName.Text);
                        command.Parameters.AddWithValue("@EP", EPhone.Text);

                        command.Parameters.AddWithValue("@EBS", EBSalary.Text);
                        command.Parameters.AddWithValue("@ESL", "0");
                        command.Parameters.AddWithValue("@ES", EBSalary.Text);
                        command.Parameters.AddWithValue("@ET", EType.SelectedIndex.ToString());

                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Employee Added!!");

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
                    if (ECommision.Text == "" || ECommision.Text == "Commision Rate")
                    {
                        MessageBox.Show("Enter a Commision Rate");
                    }
                    else
                    {

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("Insert into EmployeeTbl(Name, Phone,  BaseSalary, Sales, Salary, Type, ComRate) " +
                                "values(@EN, @EP,  @EBS, @ESl, @ES, @ET, @ECR)", connection);
                            command.Parameters.AddWithValue("@EN", EName.Text);
                            command.Parameters.AddWithValue("@EP", EPhone.Text);

                            command.Parameters.AddWithValue("@EBS", EBSalary.Text);
                            command.Parameters.AddWithValue("@ESL", "0");
                            command.Parameters.AddWithValue("@ES", EBSalary.Text);
                            command.Parameters.AddWithValue("@ECR", ECommision.Text);
                            command.Parameters.AddWithValue("@ET", EType.SelectedIndex.ToString());

                            command.Connection = connection;
                            command.ExecuteNonQuery();
                            MessageBox.Show("Employee Added!!");

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
                DisplayElements("EmployeeTbl");
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
