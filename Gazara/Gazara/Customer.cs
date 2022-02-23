using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazara
{
     class Customer:Person
    {
        public float TotalSales;
        public Customer(int id, string name, string phonenumber, float TotalSales) : base(id, name, phonenumber)
        {
            this.TotalSales = TotalSales;   
        }

        public int LoyalityDiscount()       //for each 5000 EGP the customer gets 2% discount  
        {
            
            return (int)Math.Floor(TotalSales / 5000) * (int)2   ;
        }
    }
}
