using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.bank
{
    public class IllegalAmountException : Exception
    {
        public IllegalAmountException(string error) : base(error)
        {

        }

    }
}
