using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.webshop
{
    public interface IBilling
    {
        decimal Balance { get; set; }
        void Pay(decimal amount);
    }
}
