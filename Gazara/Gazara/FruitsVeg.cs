using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazara
{
    class FruitsandVegs : Product
    {
        float weight;

        DateTime expiryDate;

        public FruitsandVegs()
        {

        }
        public FruitsandVegs(string name, int id, int quantity, float price, float weight, DateTime exp) :
            base(name, id, quantity, price)
        {
            this.weight = weight;
            this.expiryDate = exp;  
        }
        public override double GetPrice()
        {
            return weight * price;
        }
        public override string ToString()
        {
            return string.Format("{0}\n{1}:{2}\n{3}:{4}\n{5}:{6}\n", base.ToString(), "Weight", weight, "Price", GetPrice(),  "expiry Date", expiryDate);
        }
        public bool expired()
        {
            if (DateTime.Today.Year > expiryDate.Year)
                return true;
            else if (DateTime.Today.Month > expiryDate.Month)
                return true;
            else if (DateTime.Today.Day > expiryDate.Day)
                return true;    
            else
                return false;   

        }
    }
}
