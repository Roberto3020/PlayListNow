using BLL.Abstract;
using Microsoft.AspNetCore.Mvc;
using TRANSVERSAL.DTOs;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<SongDto>>> GetAll()
        {
            try
            {
                var songs = _songService.GetAll();
                return Ok(new ApiResponse<IEnumerable<SongDto>>
                {
                    Success = true,
                    Data = songs,
                    Message = "Listado de canciones obtenido correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<SongDto>>
                {
                    Success = false,
                    Message = "Error al obtener las canciones",
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<SongDto>> GetById(int id)
        {
            try
            {
                var song = _songService.GetById(id);
                if (song == null)
                {
                    return NotFound(new ApiResponse<SongDto>
                    {
                        Success = false,
                        Message = "Canción no encontrada"
                    });
                }
                return Ok(new ApiResponse<SongDto>
                {
                    Success = true,
                    Data = song,
                    Message = "Canción obtenida correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<SongDto>
                {
                    Success = false,
                    Message = "Error al obtener la canción",
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpPost]
        public ActionResult<ApiResponse<SongDto>> Create([FromBody] SongDto song)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ApiResponse<SongDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Errors = errors
                });
            }

            try
            {
                var createdSong = _songService.Create(song);
                return CreatedAtAction(nameof(GetById), new { id = createdSong.Id }, new ApiResponse<SongDto>
                {
                    Success = true,
                    Data = createdSong,
                    Message = "Canción creada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<SongDto>
                {
                    Success = false,
                    Message = "Error al crear la canción",
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse<bool>> Update(int id, [FromBody] SongDto song)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Errors = errors
                });
            }

            try
            {
                var updated = _songService.Update(id, song);
                if (!updated)
                {
                    return NotFound(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Canción no encontrada para actualizar"
                    });
                }
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Canción actualizada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error al actualizar la canción",
                    Errors = new[] { ex.Message }
                });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<bool>> Delete(int id)
        {
            try
            {
                var deleted = _songService.Delete(id);
                if (!deleted)
                {
                    return NotFound(new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Canción no encontrada para eliminar"
                    });
                }
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Canción eliminada correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Error al eliminar la canción",
                    Errors = new[] { ex.Message }
                });
            }
        }
    }
}