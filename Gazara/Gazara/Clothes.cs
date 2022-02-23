using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gazara
{
    class Clothes : Product
    {
        string size;
        string textile;
        public Clothes()
        {

        }
        public Clothes(string name, int id, int quantity, float price, string size, string textile)  :
           base(name, id, quantity, price)
        {
            this.size = size;   
            this.textile = textile; 
        }
        public override double GetPrice()
        {
            if (size == "S" || size == "M")
                return base.GetPrice();
            else

                return base.GetPrice() * 1.1;
        }
        public override string ToString()
        {
            return string.Format("{0}\n{1}:{2}\n{3}:{4}", base.ToString(), "Size", size, "Textile", textile);

        }
    }
}
