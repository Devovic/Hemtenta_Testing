using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.webshop
{
    public class Webshop : IWebshop
    {
        public decimal _Totalcost { get; set; }
        private IBasket _basket;

        public IBasket Basket
        {
            get
            {
                return _basket; 
            }
        }

        public void Checkout(IBilling billing)
        {
            if (billing == null)
            {
                throw new Exception();
            }
            else if (billing.Balance >= Basket.TotalCost)
            {
                billing.Pay(_Totalcost);
            }

            
        }
        public void SetBasket(IBasket basket)
        {
            _basket = basket;
        }
    }
}
