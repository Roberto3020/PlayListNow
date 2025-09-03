using DAL.Abstract;
using TRANSVERSAL.DTOs;
using TRANSVERSAL.Models;

namespace DAL.Implements
{
    public class SongRepository : ISongRepository
    {
        private readonly List<Song> _songs = new();
        private int _nextId = 1;

        // Métodos para la entidad Song (no forman parte de la interfaz ISongRepository)
        public IEnumerable<Song> GetAll() => _songs;

        public Song Add(Song song)
        {
            song.Id = _nextId++;
            _songs.Add(song);
            return song;
        }

        // Implementación explícita de ISongRepository

        IEnumerable<SongDto> ISongRepository.GetAll()
        {
            return _songs.Select(s => new SongDto
            {
                Id = s.Id,
                Title = s.Title,
                Artist = s.Artist,
                Duration = s.Duration
            });
        }

        SongDto ISongRepository.GetById(int id)
        {
            var song = _songs.FirstOrDefault(s => s.Id == id);
            if (song == null) return null;
            return new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Duration = song.Duration
            };
        }

        SongDto ISongRepository.Add(CreateSongDto dto)
        {
            var song = new Song
            {
                Id = _nextId++,
                Title = dto.Title,
                Artist = dto.Artist,
                Duration = dto.Duration
            };
            _songs.Add(song);
            return new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Duration = song.Duration
            };
        }

        SongDto ISongRepository.Create(SongDto dto)
        {
            var song = new Song
            {
                Id = _nextId++,
                Title = dto.Title,
                Artist = dto.Artist,
                Duration = dto.Duration
            };
            _songs.Add(song);
            return new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Duration = song.Duration
            };
        }

        bool ISongRepository.Update(int id, SongDto dto)
        {
            var song = _songs.FirstOrDefault(s => s.Id == id);
            if (song == null) return false;
            song.Title = dto.Title;
            song.Artist = dto.Artist;
            song.Duration = dto.Duration;
            return true;
        }

        bool ISongRepository.Delete(int id)
        {
            var song = _songs.FirstOrDefault(s => s.Id == id);
            if (song == null) return false;
            _songs.Remove(song);
            return true;
        }
    }
}