using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.music
{
    public class DatabaseAlreadyOpenException : Exception
    {
        public DatabaseAlreadyOpenException(string error) : base(error)
        {

        }
    }
}
