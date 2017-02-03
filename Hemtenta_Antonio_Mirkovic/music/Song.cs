using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.music
{
    public class Song : ISong
    {
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }
    }
}
