using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace youngAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Playlist>? Playlists { get; set; }
        public List<Song>? Songs { get; set; }
    }
}
