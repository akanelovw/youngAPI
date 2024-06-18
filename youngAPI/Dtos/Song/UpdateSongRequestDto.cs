using youngAPI.Models;

namespace youngAPI.Dtos.Song
{
    public class UpdateSongRequestDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Audio { get; set; }
    }
}
