using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazara
{
    public class CommisionBaseEmployee:BaseEmployee
    {
        public float commision_rate;
        public float total_sales;
        public CommisionBaseEmployee(int id, string name, string phonenumber, float base_salary, string password, float commision_rate, float total_sales)
           : base(id, name, phonenumber, base_salary, password)
        {
            this.commision_rate = commision_rate;
            this.total_sales = total_sales; 
        }

        public override float CalculateSalary()
        {
            return base.CalculateSalary() + commision_rate*total_sales;
        }
    }
}
