using Microsoft.AspNetCore.Mvc;
using TRANSVERSAL.DTOs;
using TRANSVERSAL.Services;

namespace Controllers
{
    [ApiController]
    [Route("api")]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _service;

        public SongsController(ISongService service)
        {
            _service = service;
        }

        // GET /api/health
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "Healthy" });
        }

        // GET /api/songs
        [HttpGet("songs")]
        public IActionResult GetSongs()
        {
            var songs = _service.GetAll();
            return Ok(songs);
        }

        // POST /api/songs
        [HttpPost("songs")]
        public IActionResult CreateSong([FromBody] CreateSongDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Artist))
                return BadRequest(new { error = "Title and Artist are required." });

            var song = _service.Add(dto);
            return CreatedAtAction(nameof(GetSongs), new { id = song.Id }, song);
        }
    }
}