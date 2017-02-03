using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.webshop
{
    public class Basket : IBasket
    {
        public int _amount;
        public Product _P { get; set; }
        public decimal TotalCost
        {
            get
            {
                return _P.Price * _amount;
            }
        }

        public void AddProduct(Product p, int amount)
        {
            if (p.Price <= 0 || amount <= 0)
            {
                throw new Exception();
            }
            _P = p;
            _amount = amount;
        }

        public void RemoveProduct(Product p, int amount)
        {

            if (amount > _amount || amount <= 0)
            {
                throw new Exception();
            }

            else
            {
                _P = p;
                _amount -= amount;
            }
        }
    }
}
