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
    public partial class AdminBilling : Form
    {

        public Product sel_Product;

        public AdminBilling()
        {
            InitializeComponent();
            DisplayElements("ProductTbl", BProductDGV);
            DisplayElements("SalesTbl", BillingListDGV);
            Get_Customer();
            Get_Employee();
        }
        SqlConnection connection = new SqlConnection(Program.Database_Connection_String);
        private void DisplayElements(string name, System.Windows.Forms.DataGridView DGV)
        {

            connection.Open();
            string q = "select * from " + name + "";
            SqlDataAdapter s = new SqlDataAdapter(q, connection);
            SqlCommandBuilder Bld = new SqlCommandBuilder(s);
            var dataset = new DataSet();
            s.Fill(dataset);
            DGV.DataSource = dataset.Tables[0];

            connection.Close();

        }
        int Bkey = 0;
        int type = 1;
        string price;
        private void BProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            BPName.Text = BProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            price = BProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            type = Convert.ToInt16( BProductDGV.SelectedRows[0].Cells[6].Value);
            if (BPName.Text == "")
            {
                stock = 0;
                Bkey = 0;
                
            }
            else
            {
                stock= Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[3 ].Value.ToString());
                Bkey = Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        int n = 0;
        int stock = 0;
        float GT = 0;
        
       


        private void Get_Customer()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select Id from UserTbl", connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            BCustomer.ValueMember = "Id";
            BCustomer.DataSource = dt;

            
            connection.Close();

        }

        private void Get_Employee()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select Id from EmployeeTbl", connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            BEmployee.ValueMember = "Id";
            BEmployee.DataSource = dt;


            connection.Close();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DisplayElements("ProductTbl", BProductDGV);
            DisplayElements("SalesTbl", BillingListDGV);
            Get_Customer();
            Get_Employee();
        }

        private void SaveBill_Click(object sender, EventArgs e)
        {
            if (BCustomer.SelectedIndex==-1) 
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Insert into SalesTbl(SCustomer, SAmount, SDate) values(@CN, @SA, @SD)", connection);
                    command.Parameters.AddWithValue("@CN", BCustomer.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@SA", GT);
                    command.Parameters.AddWithValue("@SD", DateTime.Today.Date);

                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bill Added!!");

                    connection.Close();
                    price = "";
                    PQuantity.Text = "";
                    BPName.Text = "";
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
            DisplayElements("SalesTbl", BillingListDGV);

        }
        float csales = 0;
        
        private float update_customer(float s)
        {
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from UserTbl where Id=" + BCustomer.SelectedValue.ToString(), connection);
            SqlDataReader rdr = cmd1.ExecuteReader();




            if (rdr.Read())
            csales = Convert.ToSingle(rdr["Sales"]);

            // make a customer object to calculate loyality discount
            Customer x = new Customer(122, "", "", csales);
            float rate = x.LoyalityDiscount();
            csales += s- s*(rate/100);
            connection.Close();

            connection.Open();
            SqlCommand cmd = new SqlCommand("Update UserTbl set Sales=@CS where Id=@CKey", connection);
            cmd.Parameters.AddWithValue("@CKey", BCustomer.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CS", csales);
            cmd.ExecuteNonQuery();
            connection.Close();
            return rate;
        }

        private void update_employee(float s)
        {
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("Select * from EmployeeTbl where Id=" + BEmployee.SelectedValue.ToString(), connection);
            SqlDataReader rdr = cmd1.ExecuteReader();


            float comRate=0;
            float Sal=0;

            if (rdr.Read()) {
                csales = Convert.ToSingle(rdr["Sales"]);
                if(rdr["ComRate"]!= DBNull.Value)
                {
                    comRate = Convert.ToSingle(rdr["ComRate"]);
                    Sal = Convert.ToSingle(rdr["Salary"]);
                    Sal += (comRate/100) * s;
                }
                else
                {
                    Sal = Convert.ToSingle(rdr["Salary"]);
                }
               
                csales += s;
                
            }
                

            // make a customer object to calculate loyality discount
            
            
            connection.Close();

            connection.Open();
            SqlCommand cmd = new SqlCommand("Update EmployeeTbl set Sales=@CS, Salary=@CSY where Id=@CKey", connection);
            cmd.Parameters.AddWithValue("@CKey", BEmployee.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CS", csales);
            cmd.Parameters.AddWithValue("@CSY", Sal);
            cmd.ExecuteNonQuery();
            connection.Close();


        }
        private void AdminBilling_Load(object sender, EventArgs e)
        {

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

        private void label9_Click(object sender, EventArgs e)
        {
            Program.isOwner = false;
            Login cut = new Login();
            cut.Show();
            this.Hide();
        }



        private void CDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BPName.Text = BProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            price = BProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            type = Convert.ToInt16(BProductDGV.SelectedRows[0].Cells[6].Value);
            if (BPName.Text == "")
            {
                stock = 0;
                Bkey = 0;

            }
            else
            {
                stock = Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[3].Value.ToString());
                Bkey = Convert.ToInt32(BProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }



        private void AddProduct_Click(object sender, EventArgs e)

        {
            if (PQuantity.Text == "" || BPName.Text == "" || price == "" || PQuantity.Text == "Quantity")
            {
                MessageBox.Show("Missing data!!");

            }
            else if (type == 0 && (BSize.SelectedIndex == -1))
            {
                MessageBox.Show("Please enter a size");
            }
            else if (Convert.ToInt32(PQuantity.Text) > stock)
            {
                MessageBox.Show("Not Enough Stock !!");
            }
            else
            {

                if (type == 0)
                {
                    sel_Product = new Clothes("", 0, Convert.ToInt32(PQuantity.Text), Convert.ToSingle(price), BSize.SelectedItem.ToString(), "");
                }
                else
                {
                    sel_Product = new FruitsandVegs("", 1, 0, Convert.ToInt32(price), Convert.ToInt32(PQuantity.Text), new DateTime(20));
                }


                // Geting the price of the product by using polymorphism ********************************************
                float t = Convert.ToSingle(sel_Product.GetPrice());     
                
                update_employee(t);
                float rate = update_customer(t);
                DataGridViewRow nr = new DataGridViewRow();
                nr.CreateCells(YourBillDGV);
                nr.Cells[0].Value = n + 1;
                nr.Cells[1].Value = BPName.Text;
                nr.Cells[2].Value = PQuantity.Text;
                nr.Cells[3].Value = t / Convert.ToInt32(PQuantity.Text);
                nr.Cells[4].Value = t;
                YourBillDGV.Rows.Add(nr);
                ++n;
                GT = GT + (t - t * (rate / 100));
                GTotal.Text = "Total: " + GT + "EGP";


                SqlCommand command = new SqlCommand("Update ProductTbl set Quantity=@PQ where Name=@PN", connection);
                command.Parameters.AddWithValue("@PQ", stock - Convert.ToInt32(PQuantity.Text));
                command.Parameters.AddWithValue("@PN", BPName.Text);
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
                stock -= Convert.ToInt32(PQuantity.Text);
                DisplayElements("ProductTbl", BProductDGV);


            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            price = "";
            PQuantity.Text = "";
            BPName.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)

        {
            if (BCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!!");

            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Insert into SalesTbl(SCustomer, SAmount, SDate) values(@CN, @SA, @SD)", connection);
                    command.Parameters.AddWithValue("@CN", BCustomer.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@SA", GT);
                    command.Parameters.AddWithValue("@SD", DateTime.Today.Date);

                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bill Added!!");

                    connection.Close();
                    price = "";
                    PQuantity.Text = "";
                    BPName.Text = "";
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
            DisplayElements("SalesTbl", BillingListDGV);

        }


    }
}
