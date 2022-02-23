using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazara
{
     public class BaseEmployee:Person
    {
        public string password;

        public float base_salry;
        public BaseEmployee(int id, string name, string phonenumber, float base_salary,string password)
            : base(id, name, phonenumber)
        {
            this.password = password;
            this.base_salry = base_salary;
        }

        public virtual float CalculateSalary()  
        {
            return base_salry;
        }
    }
}
