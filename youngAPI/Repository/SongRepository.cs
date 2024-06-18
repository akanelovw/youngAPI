using Microsoft.EntityFrameworkCore;
using youngAPI.Data;
using youngAPI.Interfaces;
using youngAPI.Models;
using youngAPI.Dtos.Song;
using youngAPI.Migrations;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace youngAPI.Repository
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _context;
        public SongRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<List<Song>> GetAllAsync()
        {
            return await _context.Song.Include(a => a.User).ToListAsync();
        }
        public async Task<Song> GetByIdAsync(int id)
        {
            return await _context.Song.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Song> CreateAsync(Song songModel)
        {
            await _context.Song.AddAsync(songModel);
            await _context.SaveChangesAsync();
            return songModel;
        }
        

        public async Task<Song?> UpdateAsync(int id, UpdateSongRequestDto songDto, string userId)
        {
            var existingSong = await _context.Song.Include(a => a.User).FirstOrDefaultAsync(x => x.Id == id);
            
            if (userId != existingSong.UserId)
            {
                throw new Exception("You have no rights to edit this post");
            }
            if (existingSong == null)
            {
                return null;
            }

            existingSong.Title = songDto.Title;
            existingSong.Description = songDto.Description;
            existingSong.Image = songDto.Image;
            existingSong.Audio = songDto.Audio;


            await _context.SaveChangesAsync();

            return existingSong;
        }
        public async Task<Song> DeleteByIdAsync(int id, string userId)
        {
            var songModel = await _context.Song.FindAsync(id);

            if (songModel == null)
            {
                return null;
            }

            if (userId != songModel.UserId)
            {
                throw new Exception("You have no rights to delete this post");
            }
            _context.Song.Remove(songModel);
            await _context.SaveChangesAsync();
            return songModel;
        }
    }
}
