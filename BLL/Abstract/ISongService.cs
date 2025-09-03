using TRANSVERSAL.DTOs;

namespace BLL.Abstract
{
    public interface ISongService
    {
        IEnumerable<SongDto> GetAll();
        SongDto GetById(int id);
        SongDto Add(CreateSongDto dto);
        SongDto Create(SongDto dto);
        bool Update(int id, SongDto dto);
        bool Delete(int id);
    }
}