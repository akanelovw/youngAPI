using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace youngAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Audio { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
    }
}
