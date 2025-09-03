using BLL.Abstract;
using DAL.Abstract;
using TRANSVERSAL.DTOs;

namespace BLL.Implements
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _repo;

        public SongService(ISongRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<SongDto> GetAll()
        {
            return _repo.GetAll();
        }

        public SongDto GetById(int id)
        {
            return _repo.GetById(id);
        }

        public SongDto Add(CreateSongDto dto)
        {
            return _repo.Add(dto);
        }

        public SongDto Create(SongDto dto)
        {
            return _repo.Create(dto);
        }

        public bool Update(int id, SongDto dto)
        {
            return _repo.Update(id, dto);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}