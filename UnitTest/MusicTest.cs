using Hemtenta_Antonio_Mirkovic.music;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class MusicTest
    {
        Mock<IMediaDatabase> IDatabase;
        MusicPlayer musicPlayer;

        public MusicTest()
        {
            IDatabase = new Mock<IMediaDatabase>();
            IDatabase.Setup(x => x.IsConnected).Returns(true);
            musicPlayer = new MusicPlayer(IDatabase.Object);
        }
        [Fact]
        public void LoadSongs_added_to_playlist_Success()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(string.Empty)).Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);
            
            musicPlayer.LoadSongs(string.Empty);

            Assert.Equal(2, musicPlayer.NumSongsInQueue);
        }
        
        [Fact]
        public void LoadSongs_find_by_searchword()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(It.IsAny<string>()))
                .Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);

            musicPlayer.LoadSongs("g2");

            Assert.Equal(1, musicPlayer.NumSongsInQueue);
        }

        [Fact]
        public void LoadSongs_search_zero_results()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(It.IsAny<string>()))
                .Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);

            musicPlayer.LoadSongs("hej");

            Assert.Equal(0, musicPlayer.NumSongsInQueue);
        }

        [Fact]
        public void Play_Success()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(It.IsAny<string>()))
                .Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);

            musicPlayer.LoadSongs("song1");
            musicPlayer.Play();


            Assert.Equal("song1", musicPlayer.NowPlaying());


        }

        [Fact]
        public void MusicPlaying_NextSong_Success()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(string.Empty)).Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);

            musicPlayer.LoadSongs(string.Empty);
            musicPlayer.NextSong();

            Assert.Equal("song2", musicPlayer.NowPlaying());

        }

        [Fact]
        public void MusicPlaying_Stop_Success()
        {
            var mockSongs = new List<ISong> { new Song { Title = "song1" }, new Song { Title = "song2" } };

            IDatabase.Setup(x => x.FetchSongs(string.Empty)).Returns(mockSongs);

            musicPlayer = new MusicPlayer(IDatabase.Object);

            musicPlayer.LoadSongs(string.Empty);
            musicPlayer.Play();
            musicPlayer.Stop();

            Assert.Equal("Tystnad råder", musicPlayer.NowPlaying());

        }

        [Fact]
        public void Database_Closed_LoadSongs_Throw()
        {
            IDatabase.Setup(x => x.IsConnected).Returns(false);
            musicPlayer = new MusicPlayer(IDatabase.Object);

            Assert.Throws<DatabaseClosedException>(() => musicPlayer.LoadSongs(string.Empty));
        }

        [Fact]
        public void Database_Open_LoadSongs_Throw()
        {
            IDatabase.Setup(x => x.IsConnected).Returns(true);
            IDatabase.Setup(x => x.OpenConnection()).Callback(() => { if (IDatabase.Object.IsConnected) throw new DatabaseAlreadyOpenException("Database is already open!"); });

            Assert.Throws<DatabaseAlreadyOpenException>(() => IDatabase.Object.OpenConnection());
        }
    }
}
