using TRANSVERSAL.Models;
using TRANSVERSAL.DTOs;

namespace TRANSVERSAL.Mappers
{
    public static class SongMapper
    {
        public static SongDto ToDto(Song song) =>
            new SongDto { Id = song.Id, Title = song.Title, Artist = song.Artist };

        public static Song ToModel(CreateSongDto dto, int id) =>
            new Song { Id = id, Title = dto.Title, Artist = dto.Artist };
    }
}