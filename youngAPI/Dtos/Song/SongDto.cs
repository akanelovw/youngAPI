using youngAPI.Models;

namespace youngAPI.Dtos.Song
{
    public class SongDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Audio { get; set; }
        public string User { get; set; } = string.Empty;
    }
}
