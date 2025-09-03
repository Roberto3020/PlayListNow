using TRANSVERSAL.DTOs;

namespace DAL.Abstract
{
    public interface ISongRepository
    {
        IEnumerable<SongDto> GetAll();
        SongDto GetById(int id);
        SongDto Add(CreateSongDto dto);
        SongDto Create(SongDto dto);
        bool Update(int id, SongDto dto);
        bool Delete(int id);
    }
}