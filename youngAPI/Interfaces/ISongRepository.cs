using youngAPI.Models;
using youngAPI.Dtos.Song;

namespace youngAPI.Interfaces
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllAsync();
        Task<Song> GetByIdAsync(int id);
        Task<Song> UpdateAsync(int id, UpdateSongRequestDto songDto, string userId);
        Task<Song> CreateAsync(Song songModel);
        Task<Song> DeleteByIdAsync(int id, string userId);
    }
}
