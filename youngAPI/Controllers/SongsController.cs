using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using youngAPI.Data;
using youngAPI.Models;
using youngAPI.Dtos.Song;
using youngAPI.Mappers;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using youngAPI.Interfaces;
using youngAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using youngAPI.Migrations;
using System.Collections;

namespace youngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISongRepository _songRepository;

        public SongsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ISongRepository songRepository)
        {
            _songRepository = songRepository;
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var songs = await _songRepository.GetAllAsync();

            var songsDto = songs.Select(s => s.ToSongDto()).ToList();

            return Ok(songsDto);
        }

        // GET: api/Songs/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _songRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToSongDto());
        }

        // PUT: api/Songs/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSongRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = new string(User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var songModel = await _songRepository.UpdateAsync(id, updateDto, user);
                if (songModel == null)
                {
                    return NotFound();
                }
                return Ok(songModel.ToSongDto());
            }
            catch (Exception ex) 
            {
                {
                    return Problem(
                        type: "/docs/errors/forbidden",
                        title: $"{ex.Message}",
                        detail: $"User '{user}' doesn't have right to delete this post.",
                        statusCode: StatusCodes.Status403Forbidden,
                        instance: HttpContext.Request.Path
                    );
                }
            }
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSongRequestDto songDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = new string(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("You need to login first");

            var songModel = songDto.ToSongFromCreateDto();
            songModel.Title = songDto.Title;
            songModel.Description = songDto.Description;
            songModel.UserId = user.Id;
            await _songRepository.CreateAsync(songModel);
            return CreatedAtAction(nameof(GetById), new { id = songModel.Id }, songModel.ToSongDto());
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var user = new string(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var song = await _songRepository.DeleteByIdAsync(id, user);
                if (song == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    type: "/docs/errors/forbidden",
                    title: "Authenticated user is not authorized.",
                    detail: $"User '{user}' doesn't have right to delete this post.",
                    statusCode: StatusCodes.Status403Forbidden,
                    instance: HttpContext.Request.Path
                );
            }
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
