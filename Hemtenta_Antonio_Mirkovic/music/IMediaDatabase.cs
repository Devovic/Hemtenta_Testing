using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.music
{
    public interface IMediaDatabase
    {
        bool IsConnected { get; }

        // Ansluter till databasen
        void OpenConnection();

        // Stänger anslutning till databasen
        void CloseConnection();

        // Returnerar alla sånger i databasen som
        // matchar söksträngen.
        // Tips: använd string.Contains(string)
        List<ISong> FetchSongs(string search);
    }
}
