using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.bank
{
    public class OperationNotPermittedException : Exception
    {
        public OperationNotPermittedException(string error) : base(error)
        {

        }
    }
}
