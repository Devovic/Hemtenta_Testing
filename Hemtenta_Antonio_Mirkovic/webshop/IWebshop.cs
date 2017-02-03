using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.webshop
{
    public interface IWebshop
    {
        IBasket Basket { get; }

        void Checkout(IBilling billing);
    }
}
