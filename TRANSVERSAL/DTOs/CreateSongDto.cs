namespace TRANSVERSAL.DTOs
{
    public class CreateSongDto
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int Duration { get; set; }
    }
}