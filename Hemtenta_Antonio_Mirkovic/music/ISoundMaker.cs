using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.music
{
    public interface ISoundMaker
    {
        // Titeln på sången som spelas just nu. Ska vara
        // tom sträng om ingen sång spelas.
        string NowPlaying { get; }

        void Play(ISong song);
        void Stop();
    }
}
