namespace TRANSVERSAL.DTOs
{
    public class SongDto
    {
        public int? Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}