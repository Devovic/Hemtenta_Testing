using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.webshop
{
    public interface IBasket
    {
        void AddProduct(Product p, int amount);
        void RemoveProduct(Product p, int amount);
        decimal TotalCost { get; }
    }
}
