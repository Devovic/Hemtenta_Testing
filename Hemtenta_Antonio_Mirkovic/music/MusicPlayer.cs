using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Antonio_Mirkovic.music
{
    public class MusicPlayer : IMusicPlayer
    {
        IMediaDatabase _database;
        ISoundMaker _soundMaker = new SoundMaker();
        List<ISong> _playList = new List<ISong>();

        public MusicPlayer(IMediaDatabase database)
        {
            _database = database;
        }

        public int NumSongsInQueue
        {
            get
            {
                return _playList.Count;
            }
        }

        public void LoadSongs(string search)
        {
            if (!_database.IsConnected)
            {
                throw new DatabaseClosedException("Database closed!");
            }
            var songs = _database.FetchSongs(search).Where(x => x.Title.Contains(search));

            _playList.AddRange(songs);
        }

        public void NextSong()
        {
            if (NumSongsInQueue > 0)
            {
                _playList.RemoveAt(0);
                _soundMaker.Play(_playList.FirstOrDefault());
            }
            else
            {
                Stop();
            }
        }

        public string NowPlaying()
        {
            if (string.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                return "Tystnad råder";
            }
            else
            {
                return _soundMaker.NowPlaying;
            }
        }

        public void Play()
        {
            if (string.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                _soundMaker.Play(_playList.FirstOrDefault());
            }
        }

        public void Stop()
        {
            _soundMaker.Stop();
        }
    }
}
