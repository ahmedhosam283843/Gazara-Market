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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
namespace Gazara
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            Sales_Total();
            Count_Products();
            Get_Products();
            Count_Customer();
            Count_Employees();
        }
        SqlConnection connection = new SqlConnection(Program.Database_Connection_String);

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminProduct x = new AdminProduct();
            x.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminEmployees x = new AdminEmployees();
            x.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AdminCustomer cut = new AdminCustomer();
            cut.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Sales_Total();
            Count_Customer();
            Count_Products();
            Get_Products();
            Count_Employees();
        }
        private void Count_Customer()
        {
            connection.Open();
            SqlDataAdapter s = new SqlDataAdapter("select Count(*) from UserTbl", connection);
            DataTable dt = new DataTable();
            s.Fill(dt);
            CNum.Text = dt.Rows[0][0].ToString() + " Customers"; 
            connection.Close();
        }
        private void Count_Employees()
        {
            connection.Open();
            SqlDataAdapter s = new SqlDataAdapter("select Count(*) from EmployeeTbl", connection);
            DataTable dt = new DataTable();
            s.Fill(dt);
            ENum.Text = dt.Rows[0][0].ToString() + " Employees";
            connection.Close();
        }
        private void Count_Products()
        {
            connection.Open();
            SqlDataAdapter s = new SqlDataAdapter("select Count(*) from ProductTbl", connection);
            DataTable dt = new DataTable();
            s.Fill(dt);
            PNum.Text = dt.Rows[0][0].ToString() + " Products";
            connection.Close();
        }
        int exp = 0;
        private void Get_Products()
        {

            connection.Open();
            SqlCommand s = new SqlCommand("select Quantity from ProductTbl", connection);
            SqlCommand s1 = new SqlCommand("select Name from ProductTbl", connection);
            SqlCommand s2 = new SqlCommand("select ExpDt from ProductTbl", connection);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            SqlDataAdapter sd = new SqlDataAdapter(s);
            SqlDataAdapter sd1 = new SqlDataAdapter(s1);
            SqlDataAdapter sd2 = new SqlDataAdapter(s2);
            sd.Fill(dt);
            sd1.Fill(dt1);
            sd2.Fill(dt2);

            List<double> data = new List<double>(); 
            List<String> Plabeles = new List<String>();
            List<DateTime> exp_dts = new List<DateTime>();
            foreach (DataRow dr in dt.Rows)
            {
                data.Add(Convert.ToDouble(dr["Quantity"].ToString()));
            }

            foreach (DataRow dr1 in dt1.Rows)
            {
                Plabeles.Add(Convert.ToString(dr1["Name"].ToString()));
            }
            foreach (DataRow dr in dt2.Rows)
            {
                if (dr["ExpDt"] == DBNull.Value) continue;
                exp_dts.Add(Convert.ToDateTime(dr["ExpDt"]));
            }

            Chart1.Series[0].LegendText = "Product Stock";
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY( Plabeles.ToArray(), data.ToArray());

            
            foreach(var x in exp_dts)
            {
                FruitsandVegs f = new FruitsandVegs("", 1, 0, 0, 0, x); //object with arbitary values to check date
                if (f.expired()) exp++;
            }
            EPNum.Text = exp + " Products";
            connection.Close();
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {
            AdminBilling cut = new AdminBilling();
            cut.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Program.isOwner = false;
            Login cut = new Login();
            cut.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void Sales_Total()
        {
            connection.Open();
            SqlDataAdapter s = new SqlDataAdapter("select Sum(SAmount) from SalesTbl", connection);
            DataTable dt = new DataTable();
            s.Fill(dt);
            SumTB.Text = dt.Rows[0][0].ToString() + " EGP";
            connection.Close();
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void Chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ENum_Click(object sender, EventArgs e)
        {

        }
    }
}
